using AssetsTools.NET.Extra;
using Mono.Data.Sqlite;
using Org.BouncyCastle.Utilities;
using PCRApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace PCRCaculator
{

    public class ABExTool
    {
        public static string DMMpath = "C:/Users/user/AppData/LocalLow/Cygames/PrincessConnectReDive/";
        //const string OUTpath = "F:/apk/DMMOut/"; 
        public static string OUTpath = "D:/PCRCalculator/PCRCalculator/PCRCalculator/Assets/StreamingAssets/AB/";
        private static SHA1CryptoServiceProvider sha1 = null;
        private static UTF8Encoding utf8 = null;

        static Dictionary<string, string> ABNameDic = new Dictionary<string, string>();
        public static Dictionary<string, AssetBundle> AssetBundleDic = new Dictionary<string, AssetBundle>();
        static Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

/*#if UNITY_EDITOR
        [MenuItem("PCRTools/DMM/从DMM客户端导出所有角色的.unity3d资源")]
#endif*/
        public static void Db2json()
        {
            //CreateNameDic();
            /*CopyByID(403101, 1);
            CopyByID(404201, 1);
            CopyByID(407001, 1);
            CopyByID(407701, 1);
            CopyByID(408401, 1);
            CopyByID(408402, 1);
            CopyByID(408403, 1);*/

            //CopyByID(115701, 0);
            //return;
            /*for (int i = 100101; i < 116001; i += 100)
            {
                CopyByID(i, 0);
            }*/
            /*for (int i = 300000; i < 399999; i += 100)
            {
                for (int j = 0; j < 6; j++)
                    CopyByID(i + j, 0);
            }*/
            /*CopyByID(180201, 0);
            CopyByID(180401, 0);
            CopyByID(180501, 0);
            CopyByID(109801, 0);*/
            /*string conn = "redive_jp.db";
            SQLiteHelper sql = new SQLiteHelper(conn);
            SqliteDataReader reader = sql.ReadFullTable("unit_data");

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("unit_id"));
                if (id <= 189999)
                    CopyByID(109801, 0);
            }
            sql.CloseConnection();*/


        }
        /*
#if UNITY_EDITOR
        [MenuItem("PCRTools/DMM/从DMM客户端导出会战BOSS数据")]
#endif

        public static void LoadEnemyData()
        {
            CreateNameDic();
            //CopyByID(302600, 1);
            //CopyByID(302601, 1);
            CopyByID(207001, 1);

            //return;
            
        }

#if UNITY_EDITOR

        [MenuItem("PCRTools/DMM/从DMM客户端导出图标")]
#endif

        public static void LoadIconData()
        {
            CopyByID(7, 7);
        }
#if UNITY_EDITOR

        [MenuItem("PCRTools/DMM/从DMM客户端导出Manifest")]
#endif

        public static void LoadManifestData()
        {
            CreateNameDic();
            CopyByID(0, 6);
        }
#if UNITY_EDITOR

        [MenuItem("PCRTools/DMM/从DMM客户端导出所有通用特效资源")]
#endif

        public static void LoadAllDependence()
        {
            string dependPath = "all_fxsk_";
            List<string> vs = new List<string>();
            for (int i = 1; i < 121; i++)
            {
                string num = "0";
                if (i < 10) { num = "000" + i; }
                else if (i < 100) { num = "00" + i; }
                else
                {
                    num += i;
                }
                num += ".unity3d";
                vs.Add(dependPath + num);
            }
            vs.Add("all_fxsk_0500.unity3d");
            vs.Add("all_fxsk_0501.unity3d");
            vs.Add("all_fxsk_0600.unity3d");
            CopyAndRename(vs);
            Debug.Log("成功！");
        }
        //a/icon_unit_plate_100131.unity3d
#if UNITY_EDITOR

        [MenuItem("PCRTools/DMM/从DMM客户端导出角色图标资源")]
#endif

        public static void LoadAllUnitPlane()
        {
            string dependPath = "icon_unit_plate_";
            List<string> vs = new List<string>();
            for (int i = 100111; i < 114311; i += 100)
            {
                vs.Add(dependPath + i + ".unity3d");
            }
            vs.Add("icon_unit_plate_180211.unity3d");
            vs.Add("icon_unit_plate_180411.unity3d");
            vs.Add("icon_unit_plate_180511.unity3d");
            CopyAndRename(vs);
            Debug.Log("成功！");
        }
        //icon_equipment_102071
        //a/icon_icon_equipment_105193.unity3d
#if UNITY_EDITOR

        [MenuItem("PCRTools/DMM/从DMM客户端导出装备图标资源")]
#endif

        public static void LoadAllEquipments()
        {
            string dependPath = "icon_icon_equipment_";
            List<string> vs = new List<string>();
            string conn = "redive_jp.db";
            SQLiteHelper sql = new SQLiteHelper(conn);
            SqliteDataReader reader = sql.ReadFullTable("equipment_data");

            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("equipment_id"));
                vs.Add(dependPath + id + ".unity3d");
            }
            sql.CloseConnection();

            CopyAndRename(vs);
            Debug.Log("成功！");
        }
        //[MenuItem("PCRTools/DMM/导出通用弹道数据")]

        */

        public static void CopyByID(int unitid, int type,Action<string> callBack=null)
        {
            List<string> oldPath = new List<string>();
            /*string[] oldPath = new string[] 
            { 
                "all_battleunit_100101.unity3d" ,
                "all_battleunitprefab_100101.unity3d",
                "icon_fav_push_notif_100101.unity3d" ,
                "spine_100101_battle.cysp.unity3d",
                "spine_sdnormal_100131.unity3d"
            };*/
            switch (type)
            {
                case 0:
                    oldPath.Add("a/all_battleunit_" + unitid + ".unity3d");
                    oldPath.Add("a/all_battleunitprefab_" + unitid + ".unity3d");
                    //oldPath.Add("a/icon_fav_push_notif_" + unitid + ".unity3d");
                    oldPath.Add("a/spine_" + unitid + "_battle.cysp.unity3d");
                    int skinID = unitid >= 200000 ? unitid : unitid + 30;
                    oldPath.Add("a/spine_sdnormal_" + skinID + ".unity3d");
                    oldPath.Add("a/unit_icon_unit_" + skinID + ".unity3d");//a/icon_unit_plate_100131.unity3d
                    oldPath.Add("a/icon_unit_plate_" + skinID + ".unity3d");
                    break;
                case 1:
                    /*string name = "a/spine_sdnormal_" + unitid + ".unity3d";
                    if (!ABNameDic.ContainsKey(name))
                    {
                        return;
                    }*/
                    oldPath.Add("a/all_battleunit_" + unitid + ".unity3d");
                    oldPath.Add("a/all_battleunitprefab_" + unitid + ".unity3d");
                    oldPath.Add("a/spine_" + unitid + "_battle.cysp.unity3d");//a/spine_300101_battle.cysp.unity3d
                    oldPath.Add("a/spine_" + unitid + "_chara_base.cysp.unity3d");//a/spine_300202_chara_base.cysp.unity3d
                    oldPath.Add("a/spine_sdnormal_" + unitid + ".unity3d");//a/spine_sdnormal_302600.unity3d
                    oldPath.Add("a/unit_icon_unit_" + unitid +".unity3d");

                    break;
                case 2:
                    oldPath.Add("a/all_abnormalstateicon.unity3d");//a/all_abnormalstateicon1.unity3d
                    oldPath.Add("a/all_abnormalstateicon1.unity3d");
                    oldPath.Add("a/all_abnormalstateicon2.unity3d");
                    oldPath.Add("a/all_atlasabnormalstate.unity3d");

                    break;
                case 3:
                    oldPath.Add("a/spine_sdnormal_190811.unity3d");
                    oldPath.Add("a/room_roomsetup_001908.unity3d");//a/roomitem_00190801.unity3d
                    oldPath.Add("a/roomitem_00190801.unity3d");//a/room_spineunit_190811.unity3d
                    oldPath.Add("a/room_spineunit_190811.unity3d");
                    break;
                case 4:
                    oldPath.Add("manifest/arcade_assetmanifest");
                    oldPath.Add("manifest/consttext_assetmanifest");
                    oldPath.Add("manifest/font_assetmanifest");
                    oldPath.Add("manifest/howtoplay_assetmanifest");
                    oldPath.Add("manifest/loginbonus_assetmanifest");
                    oldPath.Add("manifest/shader_assetmanifest");
                    oldPath.Add("manifest/manifest_assetmanifest");
                    oldPath.Add("manifest/bdl_assetmanifest");
                    oldPath.Add("manifest/banner_assetmanifest");
                    oldPath.Add("manifest/animation_assetmanifest");
                    oldPath.Add("manifest/all_assetmanifest");
                    oldPath.Add("manifest/clanbattle_assetmanifest");
                    oldPath.Add("manifest/comic_assetmanifest");
                    oldPath.Add("manifest/event_assetmanifest");
                    oldPath.Add("manifest/jukebox_assetmanifest");
                    oldPath.Add("manifest/bg_assetmanifest");
                    oldPath.Add("manifest/masterdata_assetmanifest");
                    oldPath.Add("manifest/lipsyncothers_assetmanifest");
                    oldPath.Add("manifest/minigame_assetmanifest");
                    oldPath.Add("manifest/resourcedefine_assetmanifest");
                    oldPath.Add("manifest/icon_assetmanifest");
                    oldPath.Add("manifest/room_assetmanifest");
                    oldPath.Add("manifest/spine_assetmanifest");
                    oldPath.Add("manifest/storydata_assetmanifest");
                    oldPath.Add("manifest/movie2manifest");
                    oldPath.Add("manifest/unit_assetmanifest");
                    oldPath.Add("manifest/soundmanifest");
                    oldPath.Add("manifest/seven2_assetmanifest");

                    break;
                case 5:
                    oldPath.Add("a/bdl_resourcedefine.unity3d");
                    oldPath.Add("a/resourcedefine_resourcedefine.unity3d");
                    oldPath.Add("manifest/resourcedefine_assetmanifest");
                    oldPath.Add("a/resourcedefine_bundlerelation.unity3d");
                    break;
                case 6:
                    int count0 = FindAll("all_fxsk_", null, out List<int> l00, out List<string> l01);
                    for (int i = 0; i < count0; i++)
                    {
                        oldPath.Add(l01[i]);
                    }
                    break;
                case 7:
                    oldPath.Add("a/all_atlascommon.unity3d");
                    break;
                case 8:
                    oldPath.Add("a/spine_000000_chara_base.cysp.unity3d");
                    //oldPath.Add("a/spine_000000_posing.cysp.unity3d");
                    //oldPath.Add("a/spine_000000_no_weapon.cysp.unity3d");
                    //oldPath.Add("a/spine_000000_race.cysp.unity3d");
                    //oldPath.Add("a/spine_000000_dear.cysp.unity3d");
                    //oldPath.Add("a/spine_000000_run_jump.cysp.unity3d");
                    //oldPath.Add("a/spine_000000_smile.cysp.unity3d");
                    break;
                case 9:
                    oldPath.Add("a/spine_000000_chara_base.cysp.unity3d");
                    string[] commonBattle = new string[2] { "a/spine_", "_common_battle.cysp.unity3d" };
                    int count = FindAll("_common_battle.cysp.unity3d", commonBattle, out List<int> l1, out List<string> l2);
                    for (int i = 0; i < count; i++)
                    {
                        //if (l1[i] < 100)
                        //{
                        oldPath.Add(l2[i]);
                        //}
                        //else
                        //{

                        //}
                    }
                    break;
                case 10:
                    int count2 = FindAll("icon_icon_equipment_", null, out List<int> l3, out List<string> l4);
                    for (int i = 0; i < count2; i++)
                    {
                        oldPath.Add(l4[i]);
                    }
                    oldPath.Add("a/icon_icon_equipment_999999.unity3d");
                    break;
                case 11:
                    int count3 = FindAll("icon_icon_skill_", null, out List<int> l5, out List<string> l6);
                    for (int i = 0; i < count3; i++)
                    {
                        oldPath.Add(l6[i]);
                    }
                    break;

                default:
                    oldPath.Add("a/spine_407001_chara_base.cysp.unity3d");
                    break;
            }
            CopyAndRename(oldPath,callBack);
        }
        static void CopyAndRename(List<string> vs,Action<string> callBack = null)
        {
            int i = 0;
            foreach (string path in vs)
            {
                string[] path_all = path.Split('/');
                if (path_all.Length < 2)
                    return;
                string path0 = ComputeSHA1(path_all[1]);
                string oldPath0 = DMMpath + path_all[0] + "/" + path0;
                string newPath = OUTpath + path0;
                string newPath2 = OUTpath + path_all[1];
                if (File.Exists(oldPath0))//ABNameDic.ContainsKey(path0))
                {
                    File.Copy(oldPath0, newPath, true);
                    if (!File.Exists(newPath2))
                        File.Move(newPath, newPath2);
                    else
                    {
                        File.Delete(newPath2);
                        File.Move(newPath, newPath2);

                    }
                    i++;
                }
            }
            callBack?.Invoke("成功移动" + i + "个文件！");
#if UNITY_EDITOR
            Debug.Log("成功移动" + i + "个文件！");
#endif
        }

        /*static void CreateNameDic()
        {
            ABNameDic.Clear();
            string conn = "manifest.db";
            SQLiteHelper sql = new SQLiteHelper(conn);
            SqliteDataReader reader = sql.ReadFullTable("t");

            while (reader.Read())
            {
                string orName = reader.GetString(reader.GetOrdinal("k"));
                string hashName = reader.GetString(reader.GetOrdinal("v"));
                ABNameDic.Add(orName, hashName);
            }
            sql.CloseConnection();

        }*/
        public static string ComputeSHA1(string fileName)
        {
            if (sha1 == null)
                sha1 = new SHA1CryptoServiceProvider();
            if (utf8 == null)
                utf8 = new UTF8Encoding();
            if (string.IsNullOrEmpty(fileName))
                return "";
            byte[] bytes = utf8.GetBytes(fileName);
            byte[] hash = sha1.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            int length = hash.Length;
            for (int index = 0; index < length; ++index)
                stringBuilder.Append(Convert.ToString(hash[index], 16).PadLeft(2, '0'));
            sha1.Initialize();
            return stringBuilder.ToString();
        }
        public static int FindAll(string containStr, string[] removeStrs, out List<int> listInt, out List<string> listStr)
        {
            int count = 0;
            listInt = new List<int>();
            listStr = new List<string>();
            foreach (string key in ABNameDic.Keys)
            {
                if (key.Contains(containStr))
                {
                    listStr.Add(key);
                    if (removeStrs != null)
                    {
                        string s0 = key.Replace(removeStrs[0], "");
                        string s1 = s0.Replace(removeStrs[1], "");
                        int k = int.Parse(s1);
                        listInt.Add(k);
                    }
                    count++;                    
                }
            }
            return count;
        }

        public static AssetManager mgrCharacter = new AssetManager();
        public static AssetManager mgrDataJP = new AssetManager();
        public static PCRApi.CN.AssetManager mgrDataCN = new PCRApi.CN.AssetManager();

        //private const string KEY_VER_ID = "VER_ID";
        //private const string KEY_VER_ID_OLD = "VER_ID_OLD";
        /*
        public static int GetVer(bool old,int? defaultVal = null)
        {
            if (!(MainManager.Instance?.useJapanData ?? false))
            {
                return defaultVal ?? (old ? 10038900 : 10046200);
            }

            return PlayerPrefs.GetInt(old ? KEY_VER_ID_OLD : KEY_VER_ID,
                defaultVal ?? (10046200));
        }*/

        public static async Task StaticInitialize(Tuple<int?, int?> _ = default)
        {
            var ver = MainManager.Instance.Version;

            if (ver.BossVersionJP == ver.CharacterVersionJP)
            {
                mgrCharacter = mgrDataJP;
                await Task.WhenAll(
                    Task.Run(() => mgrDataJP.Initialize(ver.BossVersionJP.ToString())),
                    Task.Run(() => mgrDataCN.Initialize(ver.BossVersionCN.ToString(),
                        ver.useQA))
                );
                VerifyPersistentBundleCaches();
                return;
            }

            await Task.WhenAll(
                Task.Run(() => mgrCharacter.Initialize(ver.CharacterVersionJP.ToString())),
                Task.Run(() => mgrDataJP.Initialize(ver.BossVersionJP.ToString())),
                Task.Run(() => mgrDataCN.Initialize(ver.BossVersionCN.ToString(),
                    ver.useQA))
            );
            VerifyPersistentBundleCaches();
        }

        private static readonly object bundleCachePreflightLock = new object();
        private static readonly HashSet<string> verifiedBundleCacheDirectories =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        private static void VerifyPersistentBundleCaches()
        {
            VerifyPersistentBundleCache(mgrCharacter);

            if (!ReferenceEquals(mgrCharacter, mgrDataJP))
                VerifyPersistentBundleCache(mgrDataJP);
        }

        private static void VerifyPersistentBundleCache(AssetManager mgr)
        {
            if (mgr?.registries == null || mgr.registries.Count == 0)
                return;

            string cacheDir;
            try
            {
                cacheDir = Path.GetFullPath(GetBundleCacheDirectory(mgr));
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"asset bundle cache preflight skipped: failed to resolve cache directory: {ex.Message}");
                return;
            }

            if (!Directory.Exists(cacheDir))
                return;

            lock (bundleCachePreflightLock)
            {
                if (!verifiedBundleCacheDirectories.Add(cacheDir))
                    return;
            }

            var stopwatch = Stopwatch.StartNew();
            int checkedCount = 0;
            int noManifestCount = 0;
            int quarantinedCount = 0;
            int errorCount = 0;

            foreach (var file in Directory.EnumerateFiles(cacheDir, "*.unity3d", SearchOption.TopDirectoryOnly))
            {
                var fullname = Path.GetFileName(file);
                if (!TryGetManifestContent(fullname, mgr, out var content))
                {
                    noManifestCount++;
                    continue;
                }

                checkedCount++;
                try
                {
                    if (!IsExpectedBundleFile(file, fullname, content, out var reason))
                    {
                        Debug.LogWarning($"asset bundle {fullname} {reason} in preflight cache scan: {file}");
                        if (QuarantineCorruptBundleFile(file, fullname))
                            quarantinedCount++;
                    }
                }
                catch (Exception ex)
                {
                    errorCount++;
                    Debug.LogWarning($"asset bundle {fullname} preflight cache scan failed: {ex}");
                }
            }

            stopwatch.Stop();
            Debug.Log(
                $"asset bundle cache preflight completed for {cacheDir}: checked={checkedCount}, " +
                $"quarantined={quarantinedCount}, noManifest={noManifestCount}, errors={errorCount}, " +
                $"elapsedMs={stopwatch.ElapsedMilliseconds}");
        }

        public static IEnumerable<(AssetManager mgr, Content content)> CacheAllFiles(Regex regex, bool useOld)
        {
            var m = (useOld ? mgrDataJP : mgrCharacter);
            foreach (var pair in m.registries)
                if (regex.IsMatch(pair.Key))
                    yield return (m, pair.Value);
        }

        public enum BattleResourcePrepareStatus
        {
            Success,
            ManifestMissing,
            CacheUnavailable,
            DependencyUnavailable,
            DependencyParseFailed,
            UnexpectedError
        }

        public sealed class BattleResourcePrepareFailure
        {
            public BattleResourcePrepareStatus Status { get; }
            public string BundleName { get; }
            public string Stage { get; }
            public string Message { get; }
            public Exception Exception { get; }

            public BattleResourcePrepareFailure(
                BattleResourcePrepareStatus status,
                string bundleName,
                string stage,
                string message,
                Exception exception = null)
            {
                Status = status;
                BundleName = bundleName;
                Stage = stage;
                Message = message;
                Exception = exception;
            }

            public override string ToString()
            {
                var exceptionText = Exception == null
                    ? string.Empty
                    : $", exception={Exception.GetType().Name}: {Exception.Message}";
                return $"status={Status}, bundle={BundleName}, stage={Stage}, message={Message}{exceptionText}";
            }
        }

        public sealed class BattleResourcePrepareSummary
        {
            public List<BattleResourcePrepareFailure> Failures { get; } = new List<BattleResourcePrepareFailure>();
            public bool Success => Failures.Count == 0;
            public int RequestedBundleCount { get; internal set; }
            public int EnsuredBundleCount { get; internal set; }
            public int SkippedManifestMissingCount { get; internal set; }
            public int OptionalDependencyMissingCount { get; internal set; }
            public long ElapsedMilliseconds { get; internal set; }

            internal void AddFailure(
                BattleResourcePrepareStatus status,
                string bundleName,
                string stage,
                string message,
                Exception exception = null)
            {
                Failures.Add(new BattleResourcePrepareFailure(status, bundleName, stage, message, exception));
            }

            public override string ToString()
            {
                var failureText = Failures.Count == 0
                    ? "none"
                    : string.Join(" | ", Failures.Select(f => f.ToString()));
                return $"success={Success}, requested={RequestedBundleCount}, ensured={EnsuredBundleCount}, " +
                       $"skippedManifestMissing={SkippedManifestMissingCount}, " +
                       $"optionalDependencyMissing={OptionalDependencyMissingCount}, elapsedMs={ElapsedMilliseconds}, " +
                       $"failures={failureText}";
            }
        }

        public static BattleResourcePrepareSummary PrepareBattleUnitResources(IEnumerable<int> unitIds)
        {
            var summary = new BattleResourcePrepareSummary();
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var bundleNames = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var unitId in unitIds ?? Enumerable.Empty<int>())
                {
                    if (unitId <= 0)
                        continue;

                    foreach (var bundleName in GetRequiredBattleUnitBundleNames(unitId))
                        bundleNames.Add(bundleName);
                }

                summary.RequestedBundleCount = bundleNames.Count;
                var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var bundleName in bundleNames)
                    EnsureBundleAndDependenciesCachedStrict(bundleName, mgrCharacter, true, visited, summary);
            }
            catch (Exception ex)
            {
                summary.AddFailure(
                    BattleResourcePrepareStatus.UnexpectedError,
                    string.Empty,
                    "prepare",
                    "unexpected battle resource prepare failure",
                    ex);
            }
            finally
            {
                stopwatch.Stop();
                summary.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            }

            Debug.Log($"battle resource prepare completed: {summary}");
            return summary;
        }

        private static IEnumerable<string> GetRequiredBattleUnitBundleNames(int unitid)
        {
            var prefabId = GetPrefabId(unitid);
            var battleunitBundleName = ResolveBattleunitBundleNameForPreload(prefabId);
            if (!string.IsNullOrEmpty(battleunitBundleName))
                yield return battleunitBundleName;
            yield return "all_battleunitprefab_" + prefabId + ".unity3d";
        }

        private static string GetBattleunitBundleName(int prefabId)
        {
            return "all_battleunit_" + prefabId + ".unity3d";
        }

        private static string GetFallbackBattleunitBundleName(int prefabId)
        {
            return "all_battleunit_" + (prefabId / 100 * 100 + 99) + ".unity3d";
        }

        private static string ResolveBattleunitBundleNameForPreload(int prefabId)
        {
            var battleunitBundleName = GetBattleunitBundleName(prefabId);
            if (TryGetManifestContent(battleunitBundleName, mgrCharacter, out _))
                return battleunitBundleName;

            var fallbackBattleunitBundleName = GetFallbackBattleunitBundleName(prefabId);
            return TryGetManifestContent(fallbackBattleunitBundleName, mgrCharacter, out _)
                ? fallbackBattleunitBundleName
                : null;
        }

        private static bool EnsureBundleAndDependenciesCachedStrict(
            string fullname,
            AssetManager mgr,
            bool required,
            HashSet<string> visited,
            BattleResourcePrepareSummary summary)
        {
            if (string.IsNullOrEmpty(fullname))
                return true;
            if (!visited.Add(fullname))
                return true;

            if (!TryGetManifestContent(fullname, mgr, out _))
            {
                if (required)
                {
                    summary.SkippedManifestMissingCount++;
                    Debug.LogWarning(
                        $"asset bundle {fullname} has no manifest entry during battle resource prepare; " +
                        "skipping cache precheck because resource version ownership is outside prepare");
                }
                else
                {
                    summary.OptionalDependencyMissingCount++;
                    Debug.LogWarning(
                        $"asset bundle dependency {fullname} has no manifest entry during battle resource prepare; " +
                        "keeping legacy optional dependency behavior");
                }
                return true;
            }

            byte[] bytes;
            try
            {
                bytes = ReadOrRepairBundleCacheFile(fullname, mgr);
            }
            catch (Exception ex)
            {
                summary.AddFailure(
                    required
                        ? BattleResourcePrepareStatus.CacheUnavailable
                        : BattleResourcePrepareStatus.DependencyUnavailable,
                    fullname,
                    "cache",
                    "download/cache validation failed",
                    ex);
                return false;
            }

            if (bytes == null)
            {
                summary.AddFailure(
                    required
                        ? BattleResourcePrepareStatus.CacheUnavailable
                        : BattleResourcePrepareStatus.DependencyUnavailable,
                    fullname,
                    "cache",
                    "download/cache validation returned no bytes");
                return false;
            }

            summary.EnsuredBundleCount++;
            return EnsureBundleDependenciesCachedStrict(fullname, bytes, mgr, visited, summary);
        }

#if UNITY_5_3_OR_NEWER
        private static bool EnsureBundleDependenciesCachedStrict(
            string fullname,
            byte[] raw,
            AssetManager mgr,
            HashSet<string> visited,
            BattleResourcePrepareSummary summary)
        {
            if (raw == null)
                return true;

            try
            {
                using var ms = new MemoryStream(raw);
                var ab = am.LoadBundleFile(ms, fullname);
                var assetsFileInstance = am.LoadAssetsFileFromBundle(ab, 0);
                var info = am.GetBaseField(assetsFileInstance,
                    assetsFileInstance.file.GetAssetsOfType(AssetClassID.AssetBundle).Single());

                var success = true;
                foreach (var val in info["m_Dependencies"]["Array"])
                {
                    var depName = val.AsString;
                    if (string.IsNullOrEmpty(depName))
                        continue;

                    Debug.Log($"preparing asset bundle dependency: {fullname} => {depName}");
                    if (!EnsureBundleAndDependenciesCachedStrict(depName, mgr, false, visited, summary))
                        success = false;
                }

                return success;
            }
            catch (Exception ex)
            {
                summary.AddFailure(
                    BattleResourcePrepareStatus.DependencyParseFailed,
                    fullname,
                    "dependency-parse",
                    "failed to parse asset bundle dependencies",
                    ex);
                return false;
            }
        }
#else
        private static bool EnsureBundleDependenciesCachedStrict(
            string fullname,
            byte[] raw,
            AssetManager mgr,
            HashSet<string> visited,
            BattleResourcePrepareSummary summary)
        {
            return true;
        }
#endif

#if ENABLE_THREADING
        private static object loadBundleLock = new();
        public static AssetBundle TryGetAssetBundleByNameWithLock(string fullname, bool useOldManifest)
        {
            lock (loadBundleLock)
                return TryGetAssetBundleByName(fullname, useOldManifest);
        }
#endif
        private static string GetBundleCacheDirectory(AssetManager mgr)
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            return Path.Combine(Application.persistentDataPath, "AB", mgr.SubFolder);
#else
            var root = string.IsNullOrWhiteSpace(persistentDataPath)
                ? Application.persistentDataPath
                : persistentDataPath;
            return Path.Combine(root, ".ABExt2", mgr.SubFolder);
#endif
        }

        private static string GetBundleCachePath(string fullname, AssetManager mgr)
        {
            return Path.Combine(GetBundleCacheDirectory(mgr), fullname);
        }

        private static void EnsureParentDirectory(string file)
        {
            var directory = Path.GetDirectoryName(file);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);
        }

        private static bool TryGetManifestContent(string fullname, AssetManager mgr, out Content content)
        {
            content = null;
            return mgr?.registries != null && mgr.registries.TryGetValue("a/" + fullname, out content);
        }

        private static bool IsExpectedBundleBytes(string fullname, byte[] bytes, AssetManager mgr, string source)
        {
            if (bytes == null) return false;
            if (!TryGetManifestContent(fullname, mgr, out var content))
            {
                Debug.LogWarning($"asset bundle {fullname} has no manifest entry while checking {source}; keeping legacy load path");
                return true;
            }

            if (content.size > 0 && bytes.Length != content.size)
            {
                Debug.LogWarning(
                    $"asset bundle {fullname} size mismatch in {source}, expected {content.size}, actual {bytes.Length}");
                return false;
            }

            var actualMd5 = Content.CalcMd5(bytes);
            if (!string.Equals(actualMd5, content.md5, StringComparison.OrdinalIgnoreCase))
            {
                Debug.LogWarning(
                    $"asset bundle {fullname} md5 mismatch in {source}, expected {content.md5}, actual {actualMd5}");
                return false;
            }

            return true;
        }

        private static bool IsExpectedBundleFile(string file, string fullname, Content content, out string reason)
        {
            reason = null;
            var fileInfo = new FileInfo(file);
            if (content.size > 0 && fileInfo.Length != content.size)
            {
                reason = $"size mismatch, expected {content.size}, actual {fileInfo.Length}";
                return false;
            }

            var actualMd5 = CalcFileMd5(file);
            if (!string.Equals(actualMd5, content.md5, StringComparison.OrdinalIgnoreCase))
            {
                reason = $"md5 mismatch, expected {content.md5}, actual {actualMd5}";
                return false;
            }

            return true;
        }

        private static string CalcFileMd5(string file)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(file);
            return string.Concat(md5.ComputeHash(stream).Select(b => $"{b:x2}"));
        }

        private static bool QuarantineCorruptBundleFile(string file, string fullname)
        {
            try
            {
                if (!File.Exists(file)) return true;

                var cacheDir = Path.GetDirectoryName(file);
                var cacheRoot = Path.GetDirectoryName(cacheDir) ?? Path.GetTempPath();
                var versionName = Path.GetFileName(cacheDir);
                var quarantineDir = Path.Combine(cacheRoot, ".corrupt", versionName);
                Directory.CreateDirectory(quarantineDir);

                var stamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss_fff");
                var processId = Process.GetCurrentProcess().Id;
                var target = Path.Combine(quarantineDir, $"{Path.GetFileName(file)}.{stamp}.{processId}.corrupt");
                var counter = 1;
                while (File.Exists(target))
                {
                    target = Path.Combine(
                        quarantineDir,
                        $"{Path.GetFileName(file)}.{stamp}.{processId}.{counter}.corrupt");
                    counter++;
                }

                File.Move(file, target);
                Debug.LogWarning($"asset bundle {fullname} corrupt cache quarantined: {file} -> {target}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"asset bundle {fullname} corrupt cache quarantine failed: {ex}");
                return false;
            }
        }

        private static bool TryWriteBundleCacheFile(string file, string fullname, byte[] bytes, AssetManager mgr)
        {
            if (!IsExpectedBundleBytes(fullname, bytes, mgr, "resolved bytes")) return false;

            EnsureParentDirectory(file);
            var temp = $"{file}.tmp.{Process.GetCurrentProcess().Id}.{Guid.NewGuid():N}";
            try
            {
                File.WriteAllBytes(temp, bytes);
                var savedBytes = File.ReadAllBytes(temp);
                if (!IsExpectedBundleBytes(fullname, savedBytes, mgr, "temporary cache file"))
                {
                    File.Delete(temp);
                    return false;
                }

                if (File.Exists(file))
                    File.Delete(file);
                File.Move(temp, file);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"asset bundle {fullname} cache write failed: {ex}");
                if (File.Exists(temp))
                    File.Delete(temp);
                return false;
            }
        }

        private static byte[] ResolveVerifiedBundleBytes(string fullname, AssetManager mgr)
        {
            var bytes = mgr.ResolveFile("a/" + fullname);
            if (bytes == null)
            {
                Debug.LogWarning($"asset bundle {fullname} could not be resolved from manifest");
                return null;
            }

            return IsExpectedBundleBytes(fullname, bytes, mgr, "ResolveFile") ? bytes : null;
        }

        private static byte[] ReadOrRepairBundleCacheFile(string fullname, AssetManager mgr)
        {
            var file = GetBundleCachePath(fullname, mgr);
            EnsureParentDirectory(file);

            if (File.Exists(file))
            {
                var existingBytes = File.ReadAllBytes(file);
                if (IsExpectedBundleBytes(fullname, existingBytes, mgr, file))
                    return existingBytes;

                if (!QuarantineCorruptBundleFile(file, fullname))
                    return null;
            }

            var resolvedBytes = ResolveVerifiedBundleBytes(fullname, mgr);
            if (resolvedBytes == null)
                return null;

            return TryWriteBundleCacheFile(file, fullname, resolvedBytes, mgr) ? resolvedBytes : null;
        }

#if UNITY_5_3_OR_NEWER
        private static AssetsManager am = new AssetsManager();
        private static HashSet<string> loadedDep = new HashSet<string>();

        private static void LoadDependency(string path, byte[] raw, AssetManager mgr)
        {
            if (raw == null) return;
            if (loadedDep.Contains(path)) return;
            loadedDep.Add(path);

            using var ms = new MemoryStream(raw);
            var ab = am.LoadBundleFile(ms, path);
            var assetsFileInstance = am.LoadAssetsFileFromBundle(ab, 0);
            var info = am.GetBaseField(assetsFileInstance, 
                assetsFileInstance.file.GetAssetsOfType(AssetClassID.AssetBundle).Single());

            foreach (var val in info["m_Dependencies"]["Array"])
            {
                var depName = val.AsString;
                Debug.Log($"resolving asset bundle dependency: {path} => {depName}");
                var bytes = ReadOrRepairBundleCacheFile(depName, mgr);
                LoadDependency(depName, bytes, mgr);
            }
        }
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LoadDependency(string path, byte[] raw, AssetManager mgr) { }
#endif

        public static AssetBundle TryGetAssetBundleByName(string fullname, bool useOldManifest)
        {
            if (AssetBundleDic.TryGetValue(fullname, out var val)) return val;

            useOldManifest &= !MainManager.Instance.Version.newAB;

            //internal packed
            var mgr = useOldManifest ? mgrDataJP : mgrCharacter;
            var streamingPath = Path.Combine(Application.streamingAssetsPath, "AB", fullname);
            AssetBundle asset = null;
#if PLATFORM_ANDROID
            var www = new WWW(streamingPath);
            while (!www.isDone) { Thread.Sleep(0); }
#else
            var www = new WWW("file://" + streamingPath);
            while (!www.isDone)
            {
                Thread.Sleep(0);
            }
#endif

            if (IsExpectedBundleBytes(fullname, www.bytes, mgr, streamingPath))
            {
                LoadDependency(fullname, www.bytes, mgr);
                asset = www.getAssetBundle();
            }
            else if (www.bytes != null)
            {
                Debug.LogWarning($"asset bundle {fullname} ignored from stale or corrupt streaming asset: {streamingPath}");
            }

            if (asset == null)
            {
                var bytes = ReadOrRepairBundleCacheFile(fullname, mgr);
                var path = GetBundleCachePath(fullname, mgr);

                if (bytes != null)
                {
                    www = new WWW("file://" + path);
                    while (!www.isDone)
                    {
                        Thread.Sleep(0);
                    }

                    LoadDependency(fullname, www.bytes, mgr);

                    asset = www.assetBundle;
                }
            }

            if (asset != null)
                AssetBundleDic.Add(fullname, asset);

            return asset;
        }

        private static ConcurrentCache<(Type, string, string, bool), object>
            GetAssetBundleByNameCache = new ConcurrentCache<(Type, string, string, bool), object>();

        public static string persistentDataPath;

        public static string dataPath;

        public static T GetAssetBundleByName<T>(string fullname, string fit = "", bool useOldManifest = false) where T : Object
        {
            var key = (typeof(T), fullname, fit, useOldManifest);
            if (GetAssetBundleByNameCache.TryGetValue(key, out var val))
                return (T)val;
#if ENABLE_THREADING
            var asset = TryGetAssetBundleByNameWithLock(fullname, useOldManifest);
#else
            var asset = TryGetAssetBundleByName(fullname, useOldManifest);
#endif
            T result = default;
            if (asset != null)
            {
                foreach (string path_0 in asset.GetAllAssetNames())
                {
                    if (path_0.Contains(fit))
                        result = asset.LoadAsset<T>(path_0);
                    if (result != null) break;
                }
            }
            GetAssetBundleByNameCache.Add(key, result);
            return result;
        }
        public static List<T> GetAllAssetBundleByName<T>(string fullname, string fit = "", bool useOldManifest = false) where T : Object
        {
#if ENABLE_THREADING
            var asset = TryGetAssetBundleByNameWithLock(fullname, useOldManifest);
#else
            var asset = TryGetAssetBundleByName(fullname, useOldManifest);
#endif
            List<T> result = new List<T>();
            if (asset != null)
            {
                foreach (string path_0 in asset.GetAllAssetNames())
                {
                    if (path_0.Contains(fit))
                        result.Add(asset.LoadAsset<T>(path_0));
                }
            }
            return result;
        }
        public enum SpriteType { 角色图标=0,角色长条=1,装备图标=2,技能图标=3,装备碎片=4,EX装备=5,EX装备类型=6,黎明界遗物=7,黎明界科技树=8 }
        public static Sprite GetSprites(SpriteType type, int id)
        {
#if UNITY_5_3_OR_NEWER
            string assetName = "";
            switch (type)
            {
                case SpriteType.角色图标:
                    assetName = "unit_icon_unit_" + id + ".unity3d";
                    break;
                case SpriteType.角色长条:
                    assetName = "icon_unit_plate_" + (id >= 200000 ? id : id + 30) + ".unity3d";
                    break;
                case SpriteType.装备图标:
                    assetName = "icon_icon_equipment_" + id + ".unity3d";
                    break;
                case SpriteType.装备碎片:
                    assetName = "icon_icon_equipment_invalid_" + id + ".unity3d";
                    break;
                case SpriteType.技能图标:
                    assetName = "icon_icon_skill_" + id + ".unity3d";
                    break;
                case SpriteType.EX装备:
                    assetName = "icon_icon_extra_equip_" + id + ".unity3d";
                    break;
                case SpriteType.EX装备类型:
                    assetName = "icon_icon_extra_equip_category_" + id + ".unity3d";
                    break;
                case SpriteType.黎明界遗物:
                    assetName = "icon_labyrinth_icon_relic_" + id + ".unity3d";
                    break;
                case SpriteType.黎明界科技树:
                    assetName = "labyrinth_node_icon_0" + id + ".unity3d";
                    break;
            }
            if (spriteDic.TryGetValue(assetName, out Sprite value))
            {
                return value;
            }
            Texture2D texture = GetAssetBundleByName<Texture2D>(assetName);
            Sprite sprite = null;
            if (texture != null)
            {
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                sprite = Resources.Load<Sprite>("pictures/"+id + "_" + (int)type);
        }
        spriteDic.Add(assetName, sprite);
            return sprite;
#else
            return null;
#endif

        }

        private static ConcurrentCache<int, GameObject> prefabCache = new ConcurrentCache<int, GameObject>();
        public static GameObject LoadUnitPrefab(int unitid)
        {
            if (prefabCache.TryGetValue(unitid, out var val)) return val;
            //if (unitid >= 200000 && unitid <= 299999)
            //    unitid = (int)(unitid / 100) * 100;
            // preload battleunit
            var prefabId = GetPrefabId(unitid);
            var battleunitBundleName = GetBattleunitBundleName(prefabId);
            var fallbackBattleunitBundleName = GetFallbackBattleunitBundleName(prefabId);
            var loadedBattleunitBundleName = battleunitBundleName;
            var primaryBattleunitInManifest = TryGetManifestContent(battleunitBundleName, mgrCharacter, out _);
#if ENABLE_THREADING
            var battleunitBundle = TryGetAssetBundleByNameWithLock(battleunitBundleName, false);
            if (battleunitBundle == null)
            {
                if (primaryBattleunitInManifest)
                {
                    Debug.LogError(
                        $"asset bundle {battleunitBundleName} failed to load and will not fall back to {fallbackBattleunitBundleName}");
                }
                else
                {
                    loadedBattleunitBundleName = fallbackBattleunitBundleName;
                    battleunitBundle = TryGetAssetBundleByNameWithLock(fallbackBattleunitBundleName, false);
                }
            }
#else
            var battleunitBundle = TryGetAssetBundleByName(battleunitBundleName, false);
            if (battleunitBundle == null)
            {
                if (primaryBattleunitInManifest)
                {
                    Debug.LogError(
                        $"asset bundle {battleunitBundleName} failed to load and will not fall back to {fallbackBattleunitBundleName}");
                }
                else
                {
                    loadedBattleunitBundleName = fallbackBattleunitBundleName;
                    battleunitBundle = TryGetAssetBundleByName(fallbackBattleunitBundleName, false);
                }
            }
#endif
            var prefabBundleName = "all_battleunitprefab_" + prefabId + ".unity3d";
            GameObject a = GetAssetBundleByName<GameObject>(prefabBundleName, "prefab", unitid > 200000);
            /*if(a==null)
            {
                unitid = (int)(unitid / 100) * 100;
                a = GetAssetBundleByName<GameObject>("all_battleunitprefab_" + unitid + ".unity3d", "prefab");
            }*/
            prefabCache[unitid] = a;
#if DONT_USE_EX
            if (a != null && battleunitBundle != null)
            {
                CompactServerBattleunitDependencies(prefabBundleName);
                CompactServerBattleunitBundleData(loadedBattleunitBundleName);
            }
#endif
            return a;
        }

#if DONT_USE_EX
        private static void CompactServerBattleunitBundleData(string fullname)
        {
            if (!AssetBundle.UseServerCompactBattleunitBundleData ||
                !AssetBundle.UseServerLightweightRootEffectPrefabs)
            {
                return;
            }

#if ENABLE_THREADING
            lock (loadBundleLock)
            {
#endif
                if (AssetBundle.TryCompactServerBattleunitBundleData(fullname))
                    AssetBundleDic.Remove(fullname);
#if ENABLE_THREADING
            }
#endif
        }

        private static void CompactServerBattleunitDependencies(string ownerBundleName)
        {
            if (!AssetBundle.UseServerCompactBattleunitBundleData ||
                !AssetBundle.UseServerLightweightRootEffectPrefabs)
            {
                return;
            }

#if ENABLE_THREADING
            lock (loadBundleLock)
            {
#endif
                foreach (var compactedName in AssetBundle.TryCompactServerBattleunitDependencies(ownerBundleName))
                    AssetBundleDic.Remove(compactedName);
#if ENABLE_THREADING
            }
#endif
        }
#endif

        public static int GetPrefabId(int unitid)
        {
            if (unitid == 207001)
                return 207000;
            if (unitid == 115801)
                return 115801;
            if (MainManager.Instance.UnitRarityDic.TryGetValue(unitid, out var unit))
            {
                return unit.detailData.prefabIdBattle;
            }
            else if (MainManager.Instance.EnemyDataDic.TryGetValue(unitid, out var enemy))
            {
                return enemy.detailData.prefab_id;
            }
            else if (MainManager.Instance.LabyrinthEnemyParameterDic.TryGetValue(unitid, out var labyrinthEnemy))
            {
                return labyrinthEnemy.detailData.prefab_id;
            }
            else if (MainManager.Instance.MirageEnemyParameterDic.TryGetValue(unitid, out var mirageEnemy))
            {
                return mirageEnemy.detailData.prefab_id;
            }
            else if (MainManager.Instance.AbyssEnemyParameterDic.TryGetValue(unitid, out var abyssEnemy))
            {
                return abyssEnemy.detailData.prefab_id;
            }
            else if (MainManager.Instance.TalentQuestEnemyParameterDic.TryGetValue(unitid, out var talentEnemy))
            {
                return talentEnemy.detailData.prefab_id;
            }
            return unitid;
        }

        public static void FreeDeleteAndReset(int ver, int verold)
        {
            MainManager.Instance.Version.CharacterVersionJP = ver;
            MainManager.Instance.Version.BossVersionJP = verold;
            PlayerPrefs.Save();
            AssetBundleDic.Clear();
            spriteDic.Clear();
            AssetBundle.UnloadAllAssetBundles(true);
            string path = Application.streamingAssetsPath + "/../.ABExt2";
            if (Application.platform == RuntimePlatform.Android)
            {
                path = Application.persistentDataPath + "/AB";
            }

            Directory.Delete(path,true);
            Directory.CreateDirectory(path);

        }
    }
}
