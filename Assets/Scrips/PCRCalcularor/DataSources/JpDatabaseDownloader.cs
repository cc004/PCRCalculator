using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PCRCaculator.DataSources
{
    /// <summary>日服 master 数据库的下载来源。</summary>
    public enum JpDatabaseMirror
    {
        /// <summary>来源1：CialloWorld 镜像 API（pcr.cialloworld.com，日文库）。</summary>
        CialloWorld = 1,

        /// <summary>来源2：roboninon.win 镜像（英文文本库）。</summary>
        Roboninon = 2,
        Taiwan = 3,
    }

    /// <summary>
    /// 日服 master 数据库下载器。镜像库都是「已反哈希」的 sqlite，落地后不需要再 Unhash。
    /// 只依赖 System.* 与 Newtonsoft，便于 server 工程一起编译。
    /// </summary>
    public static class JpDatabaseDownloader
    {
        public const string LatestVersionUrl = "https://roboninon.win/db/version";

        private const string RoboninonCompressedUrl = "https://roboninon.win/db/download?compressed=true";
        private const string RoboninonRawUrl = "https://roboninon.win/db/download";
        private const string GitHubProxyPrefix = "https://gh.rem.asia/";
        private const string CialloWorldApiPrefix = "https://pcr.cialloworld.com/api/databases";
        private const string GitHubReleaseApiPrefix =
            "https://api.github.com/repos/SonderXiaoming/priconne-database/releases/tags/database-jp-";

        private const int MinDatabaseBytes = 256 * 1024;
        private static readonly byte[] SqliteMagic = Encoding.ASCII.GetBytes("SQLite format 3\0");

        public static string GetDisplayName(JpDatabaseMirror mirror)
            => mirror == JpDatabaseMirror.Taiwan ? "台服 CialloWorld" :
                mirror == JpDatabaseMirror.Roboninon ? "来源2 roboninon（英文文本）" : "来源1 CialloWorld";

        private static async Task<JToken> TaiwanReleaseAsync(long version, CancellationToken cancellationToken)
        {
            var bytes = await AssetDownloadUtil.DownloadAsync(CialloWorldApiPrefix, null, cancellationToken).ConfigureAwait(false);
            var catalog = JObject.Parse(Encoding.UTF8.GetString(bytes));
            var latest = catalog["latest"]?["tw"];
            var entry = version <= 0 || (long?)latest?["version"] == version ? latest :
                (catalog["history"] as JArray)?.FirstOrDefault(row =>
                    (string)row["region"] == "tw" && (long?)row["version"] == version);
            if ((string)entry?["region"] != "tw" || !long.TryParse((string)entry["version"], out var found) || found <= 0)
                throw new InvalidDataException($"台服数据库列表中没有请求的版本（{version}）。");
            return entry;
        }

        public static async Task<long> FetchLatestVersionAsync(JpDatabaseMirror mirror, CancellationToken cancellationToken = default)
        {
            if (mirror == JpDatabaseMirror.Taiwan)
                return (long)(await TaiwanReleaseAsync(0, cancellationToken).ConfigureAwait(false))["version"];
            return await FetchLatestVersionAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>取日服最新 master 数据库版本号（roboninon 的 /db/version）。</summary>
        public static async Task<long> FetchLatestVersionAsync(CancellationToken cancellationToken = default)
        {
            var bytes = await AssetDownloadUtil.DownloadAsync(LatestVersionUrl, null, cancellationToken)
                .ConfigureAwait(false);
            var text = Encoding.UTF8.GetString(bytes ?? Array.Empty<byte>()).Trim();
            if (string.IsNullOrEmpty(text))
                throw new InvalidDataException("日服数据库版本接口返回空内容。");

            long version;
            if (long.TryParse(text, out version))
                return version;

            var token = JObject.Parse(text);
            foreach (var key in new[] { "TruthVersion", "truthVersion", "truth_version", "version" })
            {
                var value = token[key];
                if (value != null && long.TryParse(value.ToString(), out version))
                    return version;
            }

            throw new InvalidDataException($"无法从版本接口响应中解析版本号: {Truncate(text, 200)}");
        }

        /// <summary>下载日服 master 数据库并返回 sqlite 字节。version &lt;= 0 表示按来源自身的「最新」处理。</summary>
        public static async Task<byte[]> DownloadAsync(JpDatabaseMirror mirror, long version,
            AssetDownloadUtil.ByteProgress progress = null, CancellationToken cancellationToken = default)
        {
            byte[] payload;
            string origin;
            if (mirror == JpDatabaseMirror.Taiwan)
            {
                var release = await TaiwanReleaseAsync(version, cancellationToken).ConfigureAwait(false);
                var compressed = (string)release["br_url"];
                var urls = new[] { string.IsNullOrEmpty(compressed) ? null : GitHubProxyPrefix + compressed,
                        compressed, (string)release["url"], (string)release["urls"]?["github"] }
                    .Where(url => !string.IsNullOrWhiteSpace(url)).Distinct().ToArray();
                payload = await DownloadFirstAvailableAsync(urls, progress, cancellationToken).ConfigureAwait(false);
                origin = $"cialloworld database-tw-{release["version"]}";
                var databaseBytes = DecompressIfNeeded(payload, origin);
                ValidateDatabase(databaseBytes, origin, (long)release["version"]);
                using (var sha = SHA256.Create())
                {
                    string hash = BitConverter.ToString(sha.ComputeHash(databaseBytes)).Replace("-", "");
                    if (!string.Equals(hash, (string)release["sha256"], StringComparison.OrdinalIgnoreCase))
                        throw new InvalidDataException("台服数据库 SHA-256 校验失败。");
                }
                return databaseBytes;
            }
            if (mirror == JpDatabaseMirror.Roboninon)
            {
                long published = await FetchLatestVersionAsync(cancellationToken).ConfigureAwait(false);
                if (version > 0 && version != published)
                    throw new InvalidOperationException($"roboninon 当前提供版本 {published}，请求的版本为 {version}。");
                origin = "roboninon";
                payload = await DownloadFirstAvailableAsync(new[] { RoboninonCompressedUrl, RoboninonRawUrl },
                    progress, cancellationToken).ConfigureAwait(false);
                if (published != await FetchLatestVersionAsync(cancellationToken).ConfigureAwait(false))
                    throw new InvalidDataException("下载期间 roboninon 数据库已更新，请重新获取版本。");
            }
            else
            {
                if (version <= 0)
                    version = await FetchLatestVersionAsync(cancellationToken).ConfigureAwait(false);
                origin = $"cialloworld database-jp-{version}";
                try
                {
                    payload = await DownloadFirstAvailableAsync(
                        new[] { CialloWorldDownloadUrl(version, true), CialloWorldDownloadUrl(version, false) },
                        progress, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception cialloError)
                {
                    Note($"CialloWorld 镜像下载失败，回退 GitHub Release: {cialloError.Message}");
                    origin = $"github database-jp-{version}";
                    var assetUrl = await ResolveGitHubAssetUrlAsync(version, cancellationToken).ConfigureAwait(false);
                    payload = await DownloadFirstAvailableAsync(
                        new[] { GitHubProxyPrefix + assetUrl, assetUrl }, progress, cancellationToken).ConfigureAwait(false);
                }
            }

            var database = DecompressIfNeeded(payload, origin);
            ValidateDatabase(database, origin, version);
            return database;
        }

        /// <summary>
        /// CialloWorld 镜像 API 的直接下载地址（服务端 302 到 gh.rem.asia 代理 / GitHub Release）。
        /// compression=br 时返回 brotli 压缩的 .db.br。
        /// </summary>
        private static string CialloWorldDownloadUrl(long version, bool compressed)
        {
            var builder = new StringBuilder(CialloWorldApiPrefix);
            builder.Append("?region=jp");
            if (version > 0)
                builder.Append("&version=").Append(version);
            if (compressed)
                builder.Append("&compression=br");
            builder.Append("&download=1");
            return builder.ToString();
        }

        private static async Task<byte[]> DownloadFirstAvailableAsync(string[] urls,
            AssetDownloadUtil.ByteProgress progress, CancellationToken cancellationToken)
        {
            Exception lastError = null;
            foreach (var url in urls)
            {
                try
                {
                    progress?.Reset();
                    var bytes = await AssetDownloadUtil.DownloadAsync(url, progress, cancellationToken)
                        .ConfigureAwait(false);
                    if (bytes != null && bytes.Length > 0)
                        return bytes;
                    lastError = new InvalidDataException($"下载内容为空: {url}");
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    lastError = ex;
                    Note($"下载失败，尝试下一个地址: {url} ({ex.Message})");
                }
            }

            throw new InvalidOperationException("数据库下载失败。", lastError);
        }

        private static async Task<string> ResolveGitHubAssetUrlAsync(long version, CancellationToken cancellationToken)
        {
            var apiUrl = GitHubReleaseApiPrefix + version;
            var bytes = await AssetDownloadUtil.DownloadAsync(apiUrl, null, cancellationToken).ConfigureAwait(false);
            var text = Encoding.UTF8.GetString(bytes ?? Array.Empty<byte>());
            JObject release;
            try
            {
                release = JObject.Parse(text);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"GitHub Release 响应无法解析: {Truncate(text, 200)}", ex);
            }

            string compressed = null;
            string plain = null;
            var assets = release["assets"] as JArray;
            if (assets != null)
            {
                foreach (var asset in assets)
                {
                    var url = asset?["browser_download_url"]?.ToString();
                    var name = asset?["name"]?.ToString() ?? string.Empty;
                    if (string.IsNullOrEmpty(url))
                        continue;
                    if (name.EndsWith(".db.br", StringComparison.OrdinalIgnoreCase))
                    {
                        compressed = url;
                        break;
                    }
                    if (plain == null && name.EndsWith(".db", StringComparison.OrdinalIgnoreCase))
                        plain = url;
                }
            }

            var result = compressed ?? plain;
            if (string.IsNullOrEmpty(result))
                throw new InvalidDataException($"GitHub Release database-jp-{version} 中找不到数据库资产。");
            return result;
        }

        private static byte[] DecompressIfNeeded(byte[] payload, string origin)
        {
            if (payload == null || payload.Length == 0)
                throw new InvalidDataException($"下载内容为空: {origin}");
            if (StartsWithSqliteMagic(payload))
                return payload;

            if (payload.Length > 2 && payload[0] == 0x1F && payload[1] == 0x8B)
            {
                using (var input = new MemoryStream(payload))
                using (var gzip = new GZipStream(input, CompressionMode.Decompress))
                using (var output = new MemoryStream())
                {
                    gzip.CopyTo(output);
                    return output.ToArray();
                }
            }

            try
            {
                using (var input = new MemoryStream(payload))
                using (var brotli = new BrotliStream(input, CompressionMode.Decompress))
                using (var output = new MemoryStream())
                {
                    brotli.CopyTo(output);
                    return output.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidDataException(
                    $"数据库解压失败（既不是 sqlite，也无法按 brotli/gzip 解压）: {origin}, {payload.Length} 字节", ex);
            }
        }

        private static bool StartsWithSqliteMagic(byte[] data)
        {
            if (data.Length < SqliteMagic.Length)
                return false;
            for (var i = 0; i < SqliteMagic.Length; i++)
                if (data[i] != SqliteMagic[i])
                    return false;
            return true;
        }

        private static void ValidateDatabase(byte[] database, string origin, long version)
        {
            if (database == null || database.Length < MinDatabaseBytes)
                throw new InvalidDataException($"数据库内容过小，疑似下载不完整: {origin}, {database?.Length ?? 0} 字节");
            if (!StartsWithSqliteMagic(database))
                throw new InvalidDataException($"数据库内容不是 sqlite: {origin}");
            Note($"数据库就绪: {origin}, version={version}, {database.Length} 字节");
        }

        private static string Truncate(string text, int max)
            => string.IsNullOrEmpty(text) || text.Length <= max ? text : text.Substring(0, max) + "...";

        private static void Note(string message)
        {
#if UNITY_5_3_OR_NEWER
            UnityEngine.Debug.Log("[JpDatabase] " + message);
#else
            System.Console.WriteLine("[JpDatabase] " + message);
#endif
        }
    }
}
