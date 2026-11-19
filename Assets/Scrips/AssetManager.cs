using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Data.Sqlite;
using Newtonsoft.Json;
using UnityEngine;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using PCRCaculator;
using Random = System.Random;
using LibConeshell;
using PCRCaculator.SQL;

namespace PCRApi
{
    [JsonObject]
    public class Content
    {
        public string url, md5;
        public string type;
        public string @class;
        public string path;
        public int size;
        public List<Content> children;

        private const int DownloadMaxAttempts = 3;
        private static readonly TimeSpan DownloadAttemptTimeout = TimeSpan.FromSeconds(120);
        private static readonly HttpClient client = new HttpClient
        {
            Timeout = DownloadAttemptTimeout + TimeSpan.FromSeconds(10)
        };

        public static string CalcMd5(byte[] content)
        {
            using var path = System.Security.Cryptography.MD5.Create();
            return string.Concat(path.ComputeHash(content).Select(b => $"{b:x2}"));
        }
        public static string CalcMd5(string content) => CalcMd5(Encoding.UTF8.GetBytes(content));

        public string MD5 => modified == null ? path : CalcMd5(modified);
        [JsonIgnore]
        // "every" is also used by asset bundles; only manifest paths can contain child manifests.
        public bool IsAssets => type != "every" || !url.StartsWith("manifest/", StringComparison.Ordinal) ||
                                url.EndsWith(".cdb") || url == "manifest/seven2_assetmanifest";

        [JsonIgnore]
        public byte[] modified;

        public static Content FromLine(string line, string @class)
        {
            var splits = line.Split(',');
            if (splits.Length == 5)
            {
                return new Content
                {
                    url = splits[0],
                    @class = @class,
                    md5 = splits[1],
                    path = splits[1],
                    type = splits[2],
                    size = int.Parse(splits[3]),
                    children = new List<Content>()
                };
            }
            else
            {
                return new Content
                {
                    url = splits[0],
                    @class = @class,
                    md5 = splits[1],
                    path = splits[2],
                    type = splits[3],
                    size = int.Parse(splits[4]),
                    children = new List<Content>()
                };
            }

        }

        public static List<Content> FromUrl(string urlroot, string url, string @class)
            => AssetDownloadUtil.RunSync(() => FromUrlAsync(urlroot, url, @class));

        /// <summary>
        /// 抓一份 manifest，并发展开它的子 manifest。子 manifest 的并发由
        /// <see cref="AssetDownloadUtil.ManifestGate"/> 统一限流，不会随递归层数爆炸。
        /// </summary>
        public static async Task<List<Content>> FromUrlAsync(string urlroot, string url, string @class,
            CancellationToken cancellationToken = default)
        {
            UnityEngine.Debug.Log($"downloading from {urlroot}{url}");
            string manifestText;
            try
            {
                manifestText = Encoding.UTF8.GetString(
                    await DownloadManifestTextAsync($"{urlroot}{url}", cancellationToken).ConfigureAwait(false));
            }
            catch (Exception exception)
            {
                throw new IOException($"Failed to fetch JP manifest {urlroot}{url}; check the configured version and CDN connectivity, then retry initialization.", exception);
            }

            var lines = manifestText.Split('\n').Where(l => !string.IsNullOrEmpty(l)).ToArray();
            var res = new Content[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
                res[i] = FromLine(lines[i], @class);

            await AssetDownloadUtil.RunBoundedAsync(res,
                    async content =>
                    {
                        await content.DownloadChildrenAsync(urlroot, cancellationToken).ConfigureAwait(false);
                        return true;
                    },
                    AssetDownloadUtil.DefaultConcurrency, cancellationToken)
                .ConfigureAwait(false);

            return res.ToList();
        }

        /// <summary>manifest 抓取专用：只把 HTTP 期间计入并发闸门，递归等待时不占名额（否则会死锁）。</summary>
        private static async Task<byte[]> DownloadManifestTextAsync(string url, CancellationToken cancellationToken)
        {
            await AssetDownloadUtil.ManifestGate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                return await AssetDownloadUtil.DownloadAsync(url, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                AssetDownloadUtil.ManifestGate.Release();
            }
        }

        public byte[] GetByteArray(Func<string, string> urlgetter, string cache = null)
            => GetByteArray(urlgetter, cache, null);

        /// <summary>同步下载（阻塞调用线程）。新代码请用 <see cref="GetByteArrayAsync"/>。</summary>
        public byte[] GetByteArray(Func<string, string> urlgetter, string cache, AssetDownloadUtil.ByteProgress progress)
            => GetByteArrayAsync(urlgetter, cache, progress).GetAwaiter().GetResult();

        public async Task<byte[]> GetByteArrayAsync(Func<string, string> urlgetter, string cache = null,
            AssetDownloadUtil.ByteProgress progress = null, CancellationToken cancellationToken = default)
        {
            if (cache != null && File.Exists(cache))
            {
                var cached = await ReadAllBytesAsync(cache).ConfigureAwait(false);
                if (CalcMd5(cached) == path) return cached;
            }
            path ??= md5;
            var url = urlgetter(path);
            var result = await AssetDownloadUtil.DownloadAsync(url, progress, cancellationToken).ConfigureAwait(false);
            if (cache != null)
                await WriteAllBytesAsync(cache, result).ConfigureAwait(false);
            return result;
        }

        internal static Task<byte[]> ReadAllBytesAsync(string file)
            => Task.Run(() => File.ReadAllBytes(file));

        internal static Task WriteAllBytesAsync(string file, byte[] bytes)
            => Task.Run(() => File.WriteAllBytes(file, bytes));

        private static byte[] DownloadByteArrayWithRetry(string url)
        {
            Exception lastException = null;
            for (var attempt = 1; attempt <= DownloadMaxAttempts; attempt++)
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                try
                {
                    using var cts = new CancellationTokenSource(DownloadAttemptTimeout);
                    using var response = client.GetAsync(url, cts.Token).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                    {
                        var message = $"download http {(int)response.StatusCode} {response.ReasonPhrase} for {url}";
                        if (IsTransientStatusCode(response.StatusCode))
                            throw new HttpRequestException(message);

                        throw new InvalidOperationException(message);
                    }

                    return response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex) when (attempt < DownloadMaxAttempts && IsTransientDownloadException(ex))
                {
                    stopwatch.Stop();
                    lastException = ex;
                    var delayMs = GetRetryDelayMs(attempt);
                    UnityEngine.Debug.LogWarning(
                        $"download attempt {attempt}/{DownloadMaxAttempts} failed for {url} " +
                        $"after {stopwatch.ElapsedMilliseconds}ms, retrying in {delayMs}ms: " +
                        $"{ex.GetType().Name}: {ex.Message}");
                    Thread.Sleep(delayMs);
                }
            }

            throw lastException ?? new InvalidOperationException($"download failed for {url}");
        }

        private static bool IsTransientStatusCode(HttpStatusCode statusCode)
        {
            var code = (int)statusCode;
            return code == 408 || code == 425 || code == 429 || code >= 500;
        }

        private static bool IsTransientDownloadException(Exception ex)
        {
            if (ex is HttpRequestException || ex is TaskCanceledException || ex is IOException)
                return true;
            if (ex is WebException webException)
                return IsTransientWebException(webException);
            if (ex is AggregateException aggregateException)
                return aggregateException.Flatten().InnerExceptions.Any(IsTransientDownloadException);
            return false;
        }

        private static bool IsTransientWebException(WebException ex)
        {
            return ex.Status == WebExceptionStatus.Timeout ||
                   ex.Status == WebExceptionStatus.RequestCanceled ||
                   ex.Status == WebExceptionStatus.ConnectFailure ||
                   ex.Status == WebExceptionStatus.ConnectionClosed ||
                   ex.Status == WebExceptionStatus.ReceiveFailure ||
                   ex.Status == WebExceptionStatus.SendFailure ||
                   ex.Status == WebExceptionStatus.KeepAliveFailure ||
                   ex.Status == WebExceptionStatus.NameResolutionFailure ||
                   ex.Status == WebExceptionStatus.ProxyNameResolutionFailure ||
                   ex.Status == WebExceptionStatus.PipelineFailure ||
                   ex.Status == WebExceptionStatus.UnknownError;
        }

        private static int GetRetryDelayMs(int attempt)
        {
            return attempt switch
            {
                1 => 500,
                2 => 1500,
                _ => 3000
            };
        }

        public void DownloadChildren(string urlroot)
            => DownloadChildrenAsync(urlroot).GetAwaiter().GetResult();

        public async Task DownloadChildrenAsync(string urlroot, CancellationToken cancellationToken = default)
        {
            if (IsAssets) return;
            if (url == "manifest/mre2_assetmanifest") return; //佐贺僵尸联动活动报错
            children = await FromUrlAsync(urlroot, url, @class, cancellationToken).ConfigureAwait(false);
        }

        public byte[] GenerateCsv()
        {
            return Encoding.UTF8.GetBytes(string.Join("\n", children.Select(c => c.GenerateLine())));
        }

        public string GenerateLine()
        {
            if (!IsAssets)
            {
                var content = GenerateCsv();
                return $"{url},{CalcMd5(content)},{path},every,{content.Length},";
            }
            else if (modified != null)
                return $"{url},{CalcMd5(modified)},{path},{type},{modified.Length},";
            else
                return $"{url},{md5},{path},{type},{size},";
        }

        public void RegisterTo(IContentHolder holder)
        {
            holder.RegisterContent(url, this);
            foreach (var child in children)
                child.RegisterTo(holder);
        }
    }

    public interface IContentHolder
    {
        void RegisterContent(string url, Content content);
    }

    public class AssetManager : IContentHolder, IDataBaseManager
    {
        public string SubFolder => $"jp{Ver}";

        public Dictionary<string, Content> registries = new Dictionary<string, Content>();
        private readonly Dictionary<string, Content> hashes = new Dictionary<string, Content>();

        public string ManifestVer { get; private set; }

        public void RegisterContent(string url, Content content)
        {
            registries[url] = content;
        }
        
        private string manifest => $"http://prd-priconne-redive.akamaized.net/dl/Resources/";
        private string pool => $"http://prd-priconne-redive.akamaized.net/dl/pool/";

        public string Ver { get; private set; }

        private static string Platform
        {
            get
            {
#if PLATFORM_ANDROID
                return "Android";
#else
                return "Windows";
#endif
            }
        }
        public void AddRoot(string @class, string ver, string rootpath)
            => AssetDownloadUtil.RunSync(() => AddRootAsync(@class, ver, rootpath));

        public async Task AddRootAsync(string @class, string ver, string rootpath, CancellationToken cancellationToken = default)
        {
            var root = new Content
            {
                url = rootpath,
                type = "every",
                children = await Content
                    .FromUrlAsync($"{manifest}{ver}/Jpn/{@class}/{Platform}/", rootpath, @class, cancellationToken)
                    .ConfigureAwait(false)
            };
            root.RegisterTo(this);
        }
        private string GetManifestJson()
        {
#if PLATFORM_ANDROID
            var file = ABExTool.persistentDataPath + $"/AB/manifest_{Ver}.json";
#else
            var file = $"{ABExTool.persistentDataPath}/manifest_{Ver}.json";
#endif
            return file;
        }
        public void Initialize(string ver) => AssetDownloadUtil.RunSync(() => InitializeAsync(ver));

        public async Task InitializeAsync(string ver, CancellationToken cancellationToken = default)
        {
            Ver = ver;
            registries.Clear();

            var file = GetManifestJson();

            var poolPath = $"{ABExTool.persistentDataPath}/Pool";
            if (!Directory.Exists(poolPath))
                Directory.CreateDirectory(poolPath);

            try
            {
                var json = await Task.Run(() => File.ReadAllText(file), cancellationToken).ConfigureAwait(false);
                registries = JsonConvert.DeserializeObject<Dictionary<string, Content>>(json);

                /* setup children */

                foreach (var pair in registries)
                    pair.Value.children = pair.Value.children.Select(c => registries[c.url]).ToList();

                UnityEngine.Debug.Log($"manifest version {ver} loaded from cache");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log($"asset manager load cache failed for manifest version {ver}");
                UnityEngine.Debug.Log(e);

                await AddRootAsync("AssetBundles", ver, "manifest/manifest_assetmanifest", cancellationToken)
                    .ConfigureAwait(false);

                var payload = JsonConvert.SerializeObject(registries);
                await Task.Run(() => File.WriteAllText(file, payload), cancellationToken).ConfigureAwait(false);
            }

        }

        private static void WriteAllBytesAtomically(string file, byte[] bytes)
        {
            var directory = Path.GetDirectoryName(file);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            var temp = $"{file}.tmp.{Guid.NewGuid():N}";
            try
            {
                File.WriteAllBytes(temp, bytes);
                if (File.Exists(file))
                    File.Delete(file);
                File.Move(temp, file);
            }
            catch
            {
                if (File.Exists(temp))
                    File.Delete(temp);
                throw;
            }
        }

        public byte[] ResolveFile(string path) => ResolveFile(path, null);

        /// <summary>同步解析（阻塞调用线程）；progress 非空时会报告本次下载的字节进度。</summary>
        public byte[] ResolveFile(string path, AssetDownloadUtil.ByteProgress progress)
        {
            if (registries.TryGetValue(path, out var content))
            {
                Debug.Log($"resolving file {path}");

                var cacheFile = $"{ABExTool.persistentDataPath}/Pool/{content.MD5}";
                var expectedMd5 = content.md5;

                if (File.Exists(cacheFile))
                {
                    var bytes = File.ReadAllBytes(cacheFile);
                    var cacheMd5 = Content.CalcMd5(bytes);
                    if (string.Equals(cacheMd5, expectedMd5, StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.Log($"file {path} loaded from cache");
                        return bytes;
                    }

                    Debug.LogWarning(
                        $"file {path} cache md5 mismatch, expected {expectedMd5}, actual {cacheMd5}, redownloading");
                }

                var poolHash = string.IsNullOrEmpty(content.path) ? content.md5 : content.path;
                var downloadUrl = $"{pool}{content.@class}/{poolHash.Substring(0, 2)}/{poolHash}";
                Debug.Log(
                    $"file {path} downloading from {downloadUrl}, manifestVersion={Ver}, class={content.@class}, " +
                    $"type={content.type}, expectedMd5={expectedMd5}, expectedSize={content.size}, poolCache={cacheFile}");

                byte[] raw;
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                try
                {
                    raw = content.GetByteArray(_ => downloadUrl, null, progress);
                    stopwatch.Stop();
                    Debug.Log(
                        $"file {path} downloaded from {downloadUrl} in {stopwatch.ElapsedMilliseconds}ms, " +
                        $"bytes={raw?.Length ?? 0}");
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    Debug.LogError(
                        $"file {path} download failed from {downloadUrl} after {stopwatch.ElapsedMilliseconds}ms, " +
                        $"manifestVersion={Ver}, class={content.@class}, type={content.type}, " +
                        $"expectedMd5={expectedMd5}, expectedSize={content.size}, poolCache={cacheFile}: {ex}");
                    throw;
                }

                var downloadMd5 = Content.CalcMd5(raw);
                if (!string.Equals(downloadMd5, expectedMd5, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.LogError(
                        $"file {path} download md5 mismatch, expected {expectedMd5}, actual {downloadMd5}");
                    return null;
                }

                WriteAllBytesAtomically(cacheFile, raw);

                var bytesFromPool = File.ReadAllBytes(cacheFile);
                var savedMd5 = Content.CalcMd5(bytesFromPool);
                if (!string.Equals(savedMd5, expectedMd5, StringComparison.OrdinalIgnoreCase))
                {
                    Debug.LogError(
                        $"file {path} pool cache md5 mismatch after save, expected {expectedMd5}, actual {savedMd5}");
                    return null;
                }

                return bytesFromPool;
            }
            return null;
        }

        /// <summary>在线程池上解析文件，主线程不阻塞；progress 报告字节进度。</summary>
        public Task<byte[]> ResolveFileAsync(string path, AssetDownloadUtil.ByteProgress progress = null,
            CancellationToken cancellationToken = default)
            => Task.Run(() => ResolveFile(path, progress), cancellationToken);

        public byte[] ResolveDatabase() => ResolveDatabase(null);

        public byte[] ResolveDatabase(AssetDownloadUtil.ByteProgress progress)
        {
            var encrypted = ResolveFile("a/masterdata_master_0003.cdb", progress);

            var tally = Convert.FromBase64String(
                "tjXpXpnF6zSJjfNhgY64HHHhVcV8E9OmGbsCqqM1Nu8yKlilPpctVN82WoFFX3jNimWDrBSuv0IpSWEIaDoThhXE14msA7O4te58bns2x50gu3k1B5gcBs3/Eyxll0S+8xiRbbKoDJVk/PiZhAV/TeanZw2ltM05HYpAHr2uAjysuYbWO4giLmvZXLQaHzqgTqpzZOavOUSYg7xJ/uWt0eJLsC4PS9mIZPFMfxei2OSHkJuFUtyXo24nz3rnhauWpDA1Amus5v7+pvKAjy2fyKDJFYHomoXZR4O8So+IZfFHtQGFkW+uW5GO+O5xJdm26GHzaLCVYRpLn3P6AZuTXhguEc7lKHgmUmL7VbffIuhUe5tr6x660tB/c0sVt08YEglDPO2pM54JeYkS7koqvLs6P5XVNnFe272HUegwVKxdZM0DDCC6rMHt4+GssX0HuPDgkQ0jP5EI2xpBbnofAGY8TC9tZ7RIdq4NbahyrUj3SyIyzT1VTfKYzjX6wbiIxn6V+oiOyXZaW1BHGNCwMeX0spdv79z5viTLmbJ2vus7lbkwjF/5BzgwgXX0zVa1VZVfDIADqqy2qFOaMuFe6OR47f5hckkV5mPKNqHe7EfEIC2Rjvc6HDiVeGpJfDXSQ2LN056VyZCB1TPvuYQFuqIT5Pdf2ubrV9Y3E/J1Yus0DlCJ3+2Zfuimq+s=");

            var coneshell = ConeshellV3.FromTally(Guid.Empty, tally);

            return coneshell.DecryptVfs(encrypted);
        }

        /// <summary>数据库下载 + 解密都在线程池上跑，主线程只轮询 progress。</summary>
        public Task<byte[]> ResolveDatabaseAsync(AssetDownloadUtil.ByteProgress progress = null,
            CancellationToken cancellationToken = default)
            => Task.Run(() => ResolveDatabase(progress), cancellationToken);

        public string DataVer => $"jp{Ver}";

        /// <summary>日服官方库是 Coneshell 加密的，需要 rainbow 反哈希才能用。</summary>
        public bool RequiresUnhash => true;


        public void CalcHash()
        {
            hashes.Clear();
            foreach (var pair in registries)
                if (pair.Value.IsAssets)
                {
                    if (hashes.ContainsKey(pair.Value.MD5)) continue;
                    hashes.Add(pair.Value.MD5, pair.Value);
                }
        }
        public void DeleteCache()
        {
            var file = GetManifestJson();
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }
    }
}
