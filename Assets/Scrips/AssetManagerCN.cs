using AssetsTools;
using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Newtonsoft.Json;
using PCRCaculator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace PCRApi.CN
{
    [JsonObject]
    public class Content
    {
        public string url, md5;
        public string type;
        public string @class;
        public int size;
        public List<Content> children;

        public static string CalcMd5(byte[] content)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            return string.Concat(md5.ComputeHash(content).Select(b => $"{b:x2}"));
        }
        public static string CalcMd5(string content) => CalcMd5(Encoding.UTF8.GetBytes(content));

        public string MD5 => modified == null ? md5 : CalcMd5(modified);

        [JsonIgnore]
        public bool IsAssets => !url.StartsWith("manifest/");

        [JsonIgnore]
        public byte[] modified;

        public static Content FromLine(string line, string @class)
        {
            var splits = line.Split(',');
            int offset = splits.Count() > 5 ? 1 : 0;
            return new Content
            {
                url = splits[0],
                @class = @class,
                md5 = splits[1],
                type = splits[2 + offset],
                size = int.Parse(splits[3 + offset]),
                children = new List<Content>()
            };
        }

        public static async Task<List<Content>> FromUrl(string urlroot, string url, string @class)
        {
            Console.WriteLine($"downloading from {urlroot}{url}");
            var manifestText = Encoding.UTF8.GetString(
                await DownloadManifestTextAsync($"{urlroot}{url}").ConfigureAwait(false));
            var lines = manifestText.Split('\n');
            var res = new Content[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
                res[i] = FromLine(lines[i], @class);

            // 子 manifest 并发抓，manifest 的 HTTP 由 AssetDownloadUtil.ManifestGate 统一限流
            await AssetDownloadUtil.RunBoundedAsync(res, async content =>
                {
                    await content.DownloadChildren(urlroot).ConfigureAwait(false);
                    return true;
                }, AssetDownloadUtil.DefaultConcurrency)
                .ConfigureAwait(false);

            return res.ToList();
        }

        private static async Task<byte[]> DownloadManifestTextAsync(string url)
        {
            await AssetDownloadUtil.ManifestGate.WaitAsync().ConfigureAwait(false);
            try
            {
                return await AssetDownloadUtil.DownloadAsync(url).ConfigureAwait(false);
            }
            finally
            {
                AssetDownloadUtil.ManifestGate.Release();
            }
        }

        public byte[] GetByteArray(Func<string, string> urlgetter, string cache = null)
            => GetByteArray(urlgetter, cache, null);

        public byte[] GetByteArray(Func<string, string> urlgetter, string cache, AssetDownloadUtil.ByteProgress progress)
        {
            if (cache != null && File.Exists(cache))
            {
                var bytes = File.ReadAllBytes(cache);
                if (CalcMd5(bytes) == md5) return bytes;
            }
            var result = AssetDownloadUtil.Download(urlgetter(md5), progress);
            if (cache != null)
                File.WriteAllBytes(cache, result);
            return result;
        }

        public async Task<byte[]> GetByteArrayAsync(Func<string, string> urlgetter, string cache = null,
            AssetDownloadUtil.ByteProgress progress = null)
        {
            if (cache != null && File.Exists(cache))
            {
                var bytes = await Content.ReadAllBytesAsync(cache).ConfigureAwait(false);
                if (CalcMd5(bytes) == md5) return bytes;
            }
            var result = await AssetDownloadUtil.DownloadAsync(urlgetter(md5), progress).ConfigureAwait(false);
            if (cache != null)
                await Content.WriteAllBytesAsync(cache, result).ConfigureAwait(false);
            return result;
        }

        internal static Task<byte[]> ReadAllBytesAsync(string file)
            => Task.Run(() => File.ReadAllBytes(file));

        internal static Task WriteAllBytesAsync(string file, byte[] bytes)
            => Task.Run(() => File.WriteAllBytes(file, bytes));

        public async Task DownloadChildren(string urlroot)
        {
            if (IsAssets) return;

            children = await FromUrl(urlroot, url, @class);
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
                return $"{url},{CalcMd5(content)},every,{content.Length},";
            }
            else if (modified != null)
                return $"{url},{CalcMd5(modified)},{type},{modified.Length},";
            else
                return $"{url},{md5},{type},{size},";
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

    public interface IDataBaseManager
    {
        public byte[] ResolveDatabase();
        public string DataVer { get; }
    }


    public class AssetManager : IContentHolder, IDataBaseManager
    {
        public Dictionary<string, Content> registries = new Dictionary<string, Content>();
        private readonly Dictionary<string, Content> hashes = new Dictionary<string, Content>();

        public string ManifestVer { get; private set; }

        public void RegisterContent(string url, Content content)
        {
            registries[url] = content;
        }
        
        private string manifest => $"https://{res}Manifest/";
        private string pool => $"https://{res}pool/";

        public string Ver { get; private set; }
        public string res { get; private set; }

        public async Task AddRoot(string @class, string ver, string rootpath)
        {
            new Content
            {
                url = rootpath,
                type = "every",
                children = await Content.FromUrl($"{manifest}{@class}/{ver}/", rootpath, @class)
            }.RegisterTo(this);

        }

        private string GetManifestJson()
        {
#if PLATFORM_ANDROID
            var file = ABExTool.persistentDataPath + $"/AB/manifest_{(qa ? "qa" : "ob")}{Ver}.json";
#else
            var file = $"{ABExTool.persistentDataPath}/manifest_{(qa ? "qa" : "ob")}{Ver}.json";
#endif
            return file;
        }

        public bool qa;

        public async Task Initialize(string ver, bool qa)
        {
            this.qa = qa;
            res = qa ? "l2-testgwd8nurr-patch-gzlj.bilibiligame.net/client_qa2_771/" :
                "l1-prod-patch-gzlj.bilibiligame.net/client_ob_771/";
            Ver = ver;
            registries.Clear();

            var file = GetManifestJson();

            try
            {
                var json = await Task.Run(() => File.ReadAllText(file)).ConfigureAwait(false);
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

                await AddRoot("AssetBundles/Android", ver, "manifest/manifest_assetmanifest");

                var payload = JsonConvert.SerializeObject(registries);
                await Task.Run(() => File.WriteAllText(file, payload)).ConfigureAwait(false);
            }
        }

        public byte[] ResolveFile(string path) => ResolveFile(path, null);

        public byte[] ResolveFile(string path, AssetDownloadUtil.ByteProgress progress)
        {
            if (registries.TryGetValue(path, out var content))
            {
                Debug.Log($"resolving file {path}");
                return content.GetByteArray(hash => $"{pool}{content.@class}/{hash.Substring(0, 2)}/{hash}", null, progress);
            }
            return null;
        }

        AssetsManager am = new AssetsManager();
        /*
        public AssetManager()
        {
#if PLATFORM_ANDROID
            am.LoadClassPackage(Path.Combine(Application.persistentDataPath, "classdata.tpk"));
#else
            am.LoadClassPackage(Path.Combine(Application.streamingAssetsPath, "classdata.tpk"));
#endif
        }
        */
        public BundleFileInstance ResolveAssetsBundle(string path, string cache = null)
            => ResolveAssetsBundle(path, cache, null);

        public BundleFileInstance ResolveAssetsBundle(string path, string cache, AssetDownloadUtil.ByteProgress progress)
        {
            if (registries.TryGetValue(path, out var content))
            {
                BundleFileInstance bundleFileInstance;
                bundleFileInstance = am.LoadBundleFile(new MemoryStream(
                        content.GetByteArray(hash => $"{pool}{content.@class}/{hash.Substring(0, 2)}/{hash}",
                            cache, progress)),
                    ABExTool.persistentDataPath, unpackIfPacked: false);
                bundleFileInstance.file = AssetExtensions.DecompressToMemory(bundleFileInstance);
                return bundleFileInstance;
            }
            return null;
        }

        private static bool patching;

        public byte[] ResolveDatabase() => ResolveDatabase(null);

        public byte[] ResolveDatabase(AssetDownloadUtil.ByteProgress progress)
        {
            var ab = ResolveAssetsBundle("a/masterdata_master.unity3d", null, progress);
            var assetsFileInstance = am.LoadAssetsFileFromBundle(ab, 0);
            var text = assetsFileInstance.file.GetAssetsOfType(AssetClassID.TextAsset).SingleOrDefault();
            var root = am.GetBaseField(assetsFileInstance, text);
            return root["m_Script"].AsByteArray;
        }

        /// <summary>数据库下载 + 解包都在线程池上跑，主线程只轮询 progress。</summary>
        public Task<byte[]> ResolveDatabaseAsync(AssetDownloadUtil.ByteProgress progress = null)
            => Task.Run(() => ResolveDatabase(progress));

        public string DataVer => $"{(qa ? "qa" : "prod")}{Ver}";
    }
}
