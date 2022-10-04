using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCRCaculator.SQL;
using UnityEngine;
using UnityEngine.Networking;

namespace PCRCaculator.Update
{
    public static class CheckUpdate
    {
        /*#if true //这里应该加个define
                public const string Owner = @"cc004";
        #else
                public const string Owner = @"acygen";
        #endif*/
        public static string Owner => MainManager.Instance.useJapanData ? @"acygen" : @"cc004";
        public const string Repo = @"PCRCalculator";
        public const string Salt = "abcd1234";

        public static int Version = 5;

        private static Release LatestRelease = null;
        private static List<Release> Releases = new List<Release>();

        public static string DownloadPath;// => Application.StartupPath;
        private static string TempDir
        {
            get
            {
                if(Application.platform == RuntimePlatform.Android)
                {
                    return Application.persistentDataPath;
                }
                return Application.streamingAssetsPath;
            }
        }
        public static string HashJsonName = "Hash.json";
        private static string GameVersionName = "version";

        private static ReceivedHashData2 HashData;
        //public static Dictionary<string, MyFileInfo> ExistFileDic = new Dictionary<string, MyFileInfo>();
        //private static List<MyFileInfo> needUpdateFileList = new List<MyFileInfo>();
        private static string nowGameVersion = "0.0.0";
        private static string password = "";

        public static int StateCode { get; private set; }

        public static IEnumerator GetGithubData(int nowToolVer)
        {
            StateCode = 0;
            var updater = new GitHubRelease(Owner, Repo);
            string url = updater.AllReleaseUrl;
            var web = UnityWebRequest.Get(url);
            web.timeout = 30;
            yield return web.SendWebRequest();
            bool needReLaunch = false;
            if(web.result == UnityWebRequest.Result.Success)
            {
                string json = web.downloadHandler.text;
                Releases = JsonConvert.DeserializeObject<List<Release>>(json);
                LatestRelease = GetLatestRelease(Releases, true);
                Log.SetLog($"最新版本为：{ LatestRelease.tag_name},发布时间：{LatestRelease.published_at}", Color.blue);
                yield return DownloadNewFile(HashJsonName, TempDir, null);
                if (StateCode == 100)
                {
                    HashData = JsonConvert.DeserializeObject<ReceivedHashData2>(File.ReadAllText(Path.Combine(TempDir,HashJsonName)));
                }
                else
                {
                    Log.Error($"下载{HashJsonName}出错:{StateCode}");
                    yield break;
                }
                if (HashData == null)
                {
                    Log.Error($"下载{HashJsonName}出错:{StateCode}");
                    yield break;
                }
                if(nowToolVer < HashData.Version)
                {
                    Log.SetLog($"当前版本{nowToolVer}不是最新版，建议使用最新版。");
                }
                else
                {
                    Log.SetLog($"当前版本{nowToolVer}是最新版。");
                }
                int ver = ABExTool.GetVer(false);
                int verold = ABExTool.GetVer(true);
                if(ver != HashData.AssestVer || verold != HashData.AssestVerOld)
                {
                    Log.SetLog($"正在更新资源版本 {ver}->{HashData.AssestVer}并删除缓存...");
                    ABExTool.FreeDeleteAndReset(HashData.AssestVer, HashData.AssestVerOld);
                    needReLaunch = true;
                }
                string dbMD5 = CalcMd5(File.ReadAllBytes(SQL.SQLiteTool.GetDBPath()));
                if(!string.IsNullOrEmpty(HashData.DBMD5) && HashData.DBMD5 == dbMD5)
                {
                    Log.SetLog($"当前数据库{dbMD5}已是最新版。");
                }
                else
                {
                    if (HashData.DBMD5 != null)
                        yield return DownloadNewFile(HashData.DBName, TempDir, HashData.DBMD5);
                    else
                    {
                        yield return DownloadNewFile(SQLiteTool.DatabaseName, TempDir, null);
                        var t = StateCode;
                        yield return DownloadNewFile(SQLiteTool.DatabaseName_cn, TempDir, null);
                        if (t != 100) StateCode = t;
                    }

                    if(StateCode == 100)
                    {
                        Log.SetLog($"数据库已更新，请重启游戏。");
                        needReLaunch = true;
                    }
                    else
                    {
                        Log.Error($"下载{HashJsonName}出错:{StateCode}");
                        yield break;
                    }
                }
                if (needReLaunch)
                    StateCode = -100;
                else
                    StateCode = 300;
            }
            else
            {
                Log.Error($"网络出错:{web.error}");
                StateCode = 400;
                yield break;
            }
        }/*
        public static async Task<int> CheckAsync(bool isPreRelease)
        {
            int result = 0;
            string url = "";
            try
            {
                var updater = new GitHubRelease(Owner, Repo);
                url = updater.AllReleaseUrl;

                var (_, json) = await UnityWebRequest. .DownloadStringAsync(WebUtility.CreateRequest(url));

                Releases = JsonSerializer.Deserialize<List<Release>>(json)!;
                LatestRelease = GetLatestRelease(Releases, isPreRelease);
                Log.SetLog($"最新版本为：{ LatestRelease.tag_name},发布时间：{LatestRelease.published_at}", Color.Blue);
                string? path = await DownloadNewFile(HashJsonName, TempDir, null);
                if (path != null)
                {
                    HashData = JsonSerializer.Deserialize<ReceivedHashData>(File.ReadAllText(path));
                }
                if (HashData == null)
                    return 7;
                if (HashData.Version > Version)
                {
                    Log.Warning($"当前下载器版本{Version}，最新版本{HashData.Version}，\n需要更新下载器！");
                    return 10;
                }
                if (!ChechGameVersion(HashData.GameVersion))
                {
                    Log.Warning($"当前版本{nowGameVersion}过低，最新版本{HashData.GameVersion}，请下载完整新版！");
                    return 15;
                }
                needUpdateFileList.Clear();
                foreach (var data in HashData.upLoadFiles)
                {
                    MyFileInfo old = await FindTargetFile(data);
                    if (string.IsNullOrEmpty(old.hash) || old.hash != data.hash)
                    {
                        old.hash = data.hash;
                        needUpdateFileList.Add(old);
                    }
                }
                if (needUpdateFileList.Count > 0)
                {
                    Log.SetLog($"需要更新{needUpdateFileList.Count}个文件，点击更新按钮以继续...");
                }
                else
                {
                    Log.SetLog("当前不需要更新！");
                }
            }
            catch (Exception e)
            {
                if (e is WebException)
                    Log.Warning($"获取失败：{e.Message}");
                else
                    Log.Error($"获取失败：{e.Message}");

                Log.Error($"请稍后重试，或手动前往{url}下载并更新。");
                result = 1;
            }
            return result;
        }*/
        private static Release GetLatestRelease(IEnumerable<Release> releases, bool isPreRelease)
        {
            if (!isPreRelease)
                releases = releases.Where(release => !release.prerelease);

            releases = releases.OrderByDescending(release => release.published_at);
            return releases.ElementAt(0);
        }
        private static IEnumerator DownloadNewFile(string fileName, string downloadDirectory, string MD5)
        {
            if (LatestRelease == null)
            {
                Log.Error("未获取到更新数据！");
                StateCode = 404;
                yield break;
            }

            string updateFileFullName = null;

            if (!Directory.Exists(downloadDirectory))
            {
                Directory.CreateDirectory(downloadDirectory);
            }
            var updateFileUrl = FindByFileName(fileName)?.browser_download_url;
            if (updateFileUrl == null)
            {
                StateCode = 404;
                Log.Error($"文件{fileName}不存在！");
                yield break;
            }
            updateFileFullName = Path.Combine(downloadDirectory, fileName);

            var downloaded = false;
            bool checkHash = !string.IsNullOrEmpty(MD5);
            if (File.Exists(updateFileFullName))
            {
                if (checkHash)
                {
                    var fileHash = CalcMd5(File.ReadAllBytes(updateFileFullName));
                    if (fileHash == MD5)
                        downloaded = true;
                    else
                        File.Delete(updateFileFullName);
                }
                else
                {
                    File.Delete(updateFileFullName);
                }
            }
            if (!downloaded)
            {

                string url = updateFileUrl;
                var web = UnityWebRequest.Get(url);
                web.timeout = 30;
                yield return web.SendWebRequest();
                if (web.result == UnityWebRequest.Result.Success)
                {
                    File.WriteAllBytes(updateFileFullName, web.downloadHandler.data);
                }
                else
                {
                    Log.Error($"下载文件失败！");
                    StateCode = 500;
                    yield break;
                }
                if (checkHash)
                {
                    var fileHash = CalcMd5(File.ReadAllBytes(updateFileFullName));
                    if (fileHash != MD5)
                    {
                        Log.Error($"下载文件校验失败！");
                        File.Delete(updateFileFullName);
                        StateCode = 406;
                        yield break;
                    }
                }
            }


            StateCode = 100;
        }
        private static Asset FindByFileName(string fileName)
        {
            foreach (Asset a in LatestRelease.assets)
            {
                if (a.name.Contains(fileName))
                    return a;
            }
            return FindByFileName2(fileName);
        }
        private static Asset FindByFileName2(string fileName)
        {
            foreach (Release release in Releases)
            {
                foreach (Asset a in release.assets)
                {
                    if (a.name.Contains(fileName))
                        return a;
                }
            }
            return null;
        }
        /*private static async Task<MyFileInfo> FindTargetFile(MyFileInfo file)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(DownloadPath);
            FileInfo[] fileInfos = dirInfo.GetFiles(file.fileName, SearchOption.AllDirectories);
            MyFileInfo myFileInfo = new MyFileInfo();
            if (fileInfos.Length == 0)
            {
                Log.Warning($"在{dirInfo.FullName}目录里找不到文件{file.fileName}，将下载新的文件！");
                var dirs = dirInfo.GetDirectories(file.targetDirName, SearchOption.AllDirectories);
                if (dirs.Length == 0)
                {
                    Log.Error($"在{dirInfo.FullName}目录里找不到目标文件夹{file.targetDirName}，请将下载器放在正确的位置！");
                    throw new Exception("下载器找不到目标文件或文件夹！");
                }
                myFileInfo.fileName = file.fileName;
                myFileInfo.path = Path.Combine(dirs[0].FullName, file.fileName);
                return myFileInfo;
            }
            if (fileInfos.Length > 1)
            {
                Log.Warning($"找到{fileInfos.Length}个文件，但只会更新第一个文件，请删除多余的文件。");
                foreach (FileInfo file0 in fileInfos)
                {
                    Log.Warning($"{file0.FullName}");
                }
            }
            myFileInfo.fileName = fileInfos[0].Name;
            myFileInfo.path = fileInfos[0].FullName;
            myFileInfo.zipCompressed = file.zipCompressed;
            myFileInfo.hash = await Tool.Sha256CheckSumAsync(myFileInfo.path);
            return myFileInfo;
        }*/

        private static bool ChechGameVersion(string requireVersion)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(DownloadPath);
            FileInfo[] fileInfos = dirInfo.GetFiles(GameVersionName, SearchOption.AllDirectories);
            if (fileInfos.Length == 0)
            {
                Log.Error($"在{dirInfo.FullName}目录里找不到目标文件{GameVersionName}，请将下载器放在正确的位置！");
                return false;
            }
            nowGameVersion = File.ReadAllText(fileInfos[0].FullName);
            return nowGameVersion == requireVersion;
        }
        
        public static string CalcMd5(byte[] content)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            return string.Concat(md5.ComputeHash(content).Select(b => $"{b:x2}"));
        }
        public static string CalcMd5(string content) => CalcMd5(Encoding.UTF8.GetBytes(content));

        public static void ExportUpdateHashDataInEditor(int ver,int as1,int asold)
        {
            string dic = Application.dataPath + "/Editor/Update/";
            string jsonPath = dic + HashJsonName;
            string dbPath = dic + SQL.SQLiteTool.DatabaseName;
            if (File.Exists(jsonPath))
                File.Delete(jsonPath);
            if (File.Exists(dbPath))
                File.Delete(dbPath);
            ReceivedHashData2 json = new ReceivedHashData2();
            json.Version = ver;
            json.DBMD5 = CalcMd5(File.ReadAllBytes(SQL.SQLiteTool.GetDBPath()));
            json.DBName = SQL.SQLiteTool.DatabaseName;
            json.AssestVer = as1;
            json.AssestVerOld = asold;
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(json));
            File.Copy(SQL.SQLiteTool.GetDBPath(), dbPath);
            Debug.Log($"成功生成{ver}/{as1}/{asold}!");
        }
    }
    public static class Log
    {
        public static Action<string, Color> LogAction;
        public static Action<string, Color> LogAction2;
        public static void SetLog(string msg, Color color)
        {
            LogAction?.Invoke(msg, color);
        }
        public static void SetLog(string msg)
        {
            LogAction?.Invoke(msg, Color.black);
        }
        public static void Warning(string msg)
        {
            LogAction?.Invoke(msg, Color.yellow);
        }
        public static void Error(string msg)
        {
            LogAction?.Invoke(msg, Color.red);
        }
        public static void SetLogSingle(string msg, Color color)
        {
            LogAction2?.Invoke(msg, color);
        }
        public static void SetLogSingle(string msg)
        {
            LogAction2?.Invoke(msg, Color.black);
        }
    }

    public class ReceivedHashData2
    {
        public int Version { get; set; }
        //public string DBHash { get; set; }
        public string DBName { get; set; }
        public string DBMD5 { get; set; }
        public int AssestVer { get; set; }
        public int AssestVerOld { get; set; }
    }
    /*public class ReceivedHashData
    {
        public int Version { get; set; }
        public string NewDName { get; set; }
        public string GameVersion { get; set; }
        public List<MyFileInfo> upLoadFiles { get; set; }
        public bool encryped { get; set; }
        public string passwordHash { get; set; }
    }
    public class MyFileInfo
    {
        public string fileName { get; set; }
        public string path { get; set; }
        public string hash { get; set; }
        public string targetDirName { get; set; }
        public bool zipCompressed { get; set; }
    }*/
}
