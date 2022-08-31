using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mono.Data.Sqlite;
using Newtonsoft0.Json;
using UnityEngine.Diagnostics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using PCRCaculator;

namespace PCRApi
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
        public bool IsAssets => type != "every" || url.EndsWith(".cdb");

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

        public static List<Content> FromUrl(string urlroot, string url, string @class)
        {
            UnityEngine.Debug.Log($"downloading from {urlroot}{url}");
            var lines = client.GetStringAsync($"{urlroot}{url}").Result.Split('\n')
                .Where(l => !string.IsNullOrEmpty(l)).ToArray();
            var res = new Content[lines.Length];

            for (int i = 0; i < lines.Length; ++i)
            {
                res[i] = FromLine(lines[i], @class);
                res[i].DownloadChildren(urlroot);
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

        public void DownloadChildren(string urlroot)
        {
            if (IsAssets) return;

            children = FromUrl(urlroot, url, @class);
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

    public class AssetManager : IContentHolder
    {
        public Dictionary<string, Content> registries = new Dictionary<string, Content>();
        private readonly Dictionary<string, Content> hashes = new Dictionary<string, Content>();

        public string ManifestVer { get; private set; }

        public void RegisterContent(string url, Content content)
        {
            registries.Add(url, content);
        }
        
        private string manifest => $"http://prd-priconne-redive.akamaized.net/dl/Resources/";
        private string pool => $"http://prd-priconne-redive.akamaized.net/dl/pool/";

        public string Ver { get; private set; }

        public void AddRoot(string @class, string ver, string rootpath)
        {
            new Content
            {
                url = rootpath,
                type = "every",
                children = Content.FromUrl($"{manifest}{ver}/Jpn/{@class}/Windows/", rootpath, @class)
            }.RegisterTo(this);

        }
        public void Initialize(string ver, string movie_ver = null, string sound_ver = null, bool patch=true)
        {
            Ver = ver;
            registries.Clear();

            try
            {
                registries = JsonConvert.DeserializeObject<Dictionary<string, Content>>(File.ReadAllText($"manifest_{ver}.json"));

                /* setup children */

                foreach (var pair in registries)
                    pair.Value.children = pair.Value.children.Select(c => registries[c.url]).ToList();

                UnityEngine.Debug.Log($"manifest version {ver} loaded from cache");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"asset manager load cache failed for manifest version {ver}");
                UnityEngine.Debug.LogError(e);

                AddRoot("AssetBundles", ver, "manifest/manifest_assetmanifest");
                if (movie_ver != null)
                    AddRoot("Movie", movie_ver + "/SP/High", "manifest/movie2manifest");
                if (sound_ver != null)
                    AddRoot("Sound", sound_ver, "manifest/sound2manifest");

                File.WriteAllText($"manifest_{ver}.json", JsonConvert.SerializeObject(registries));
            }

            if (patch)
            {
                CalcHash();
                ManifestVer = new Random().Next().ToString();
            }
            
        }
        
        public byte[] ResolveFile(string path)
        {
            if (registries.TryGetValue(path, out var content))
            {
                return content.GetByteArray(hash => $"{pool}{content.@class}/{hash.Substring(0, 2)}/{hash}");
            }
            return null;
        }
        
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
    }
}
