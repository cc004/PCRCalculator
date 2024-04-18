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

        private static readonly HttpClient client = new HttpClient();

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
            return new Content
            {
                url = splits[0],
                @class = @class,
                md5 = splits[1],
                type = splits[2],
                size = int.Parse(splits[3]),
                children = new List<Content>()
            };
        }

        public static async Task<List<Content>> FromUrl(string urlroot, string url, string @class)
        {
            Console.WriteLine($"downloading from {urlroot}{url}");
            var lines = (await client.GetStringAsync($"{urlroot}{url}")).Split('\n');
            var res = new Content[lines.Length];

            for (int i = 0; i < lines.Length; ++i)
            {
                res[i] = FromLine(lines[i], @class);
                await res[i].DownloadChildren(urlroot);
            }

            return res.ToList();
        }

        public byte[] GetByteArray(Func<string, string> urlgetter, string cache = null)
        {
            if (cache != null && File.Exists(cache))
            {
                var bytes = File.ReadAllBytes(cache);
                if (CalcMd5(bytes) == md5) return bytes;
            }
            var result = client.GetByteArrayAsync(urlgetter(md5)).Result;
            if (cache != null)
                File.WriteAllBytes(cache, result);
            return result;
        }

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
            var file = $"manifest_{(qa ? "qa" : "ob")}{Ver}.json";
#endif
            return file;
        }

        public bool qa;

        public async Task Initialize(string ver, bool qa)
        {
            this.qa = qa;
            res = qa ? "l2-dev-patch-gzlj.bilibiligame.net/client_qa2_771/" :
                "l1-prod-patch-gzlj.bilibiligame.net/client_ob_771/";
            Ver = ver;
            registries.Clear();

            var file = GetManifestJson();

            try
            {
                registries = JsonConvert.DeserializeObject<Dictionary<string, Content>>(File.ReadAllText(file));

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

                File.WriteAllText(file, JsonConvert.SerializeObject(registries));
            }
        }

        public byte[] ResolveFile(string path)
        {
            if (registries.TryGetValue(path, out var content))
            {
                Debug.Log($"resolving file {path}");
                return content.GetByteArray(hash => $"{pool}{content.@class}/{hash.Substring(0, 2)}/{hash}");
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
        {
            if (registries.TryGetValue(path, out var content))
            {
                BundleFileInstance bundleFileInstance;
                bundleFileInstance = am.LoadBundleFile(new MemoryStream(
                        content.GetByteArray(hash => $"{pool}{content.@class}/{hash.Substring(0, 2)}/{hash}",
                            cache)),
                    ABExTool.persistentDataPath, unpackIfPacked: false);
                bundleFileInstance.file = AssetExtensions.DecompressToMemory(bundleFileInstance);
                return bundleFileInstance;
            }
            return null;
        }

        private static bool patching;

        public byte[] ResolveDatabase()
        {
            var ab = ResolveAssetsBundle("a/masterdata_master.unity3d");
            var assetsFileInstance = am.LoadAssetsFileFromBundle(ab, 0);
            var text = assetsFileInstance.file.GetAssetsOfType(AssetClassID.TextAsset).SingleOrDefault();
            var root = am.GetBaseField(assetsFileInstance, text);
            return root["m_Script"].AsByteArray;
        }

        public string DataVer => $"{(qa ? "qa" : "prod")}{Ver}";
    }
}
