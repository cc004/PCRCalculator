using AssetsTools.NET.Extra;
using Mono.Data.Sqlite;
using Org.BouncyCastle.Utilities;
using PCRApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        // DMM 客户端数据目录的兜底默认值；实际优先取 PlayerSetting.dmmPath（见 GetDmmRoot）
        // DMM 客户端数据目录的兜底默认值；实际优先取 PlayerSetting.dmmPath（见 GetDmmRoots）

        // DMM 客户端数据目录的兜底默认值；实际优先取 PlayerSetting.dmmPath（见 GetDmmRoots）
        // 指向 LocalLow\Cygames 这一级即可，GetDmmRoots 会自动往下找游戏子目录
        public static string DMMpath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "AppData/LocalLow/Cygames/");
        //const string OUTpath = "F:/apk/DMMOut/"; 
        public static string OUTpath = ""; // 由 GetDmmOutPath() 决定
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
        static void CopyAndRename(List<string> vs, Action<string> callBack = null)
        {
            int copied = 0;
            int missing = 0;
            var roots = GetDmmRoots();
            string outPath = GetDmmOutPath();

            if (roots.Count == 0)
            {
                callBack?.Invoke("没有找到 DMM 数据目录，请在设置里填写 DMM 路径");
                return;
            }

            foreach (string path in vs)
            {
                string[] path_all = path.Replace('\\', '/').Split('/');
                if (path_all.Length < 2)
                    continue;

                string group = path_all[0];
                string name = path_all[path_all.Length - 1];
                string sha1Name = ComputeSHA1(name);

                string source = null;
                foreach (var root in roots)
                {
                    // DMM 客户端把 bundle 存成 <root>/<class>/<sha1(文件名)>，也兼容已改回原名的副本
                    foreach (var candidate in new[]
                    {
                        Path.Combine(root, group, sha1Name),
                        Path.Combine(root, group, name),
                        Path.Combine(root, name)
                    })
                    {
                        if (!File.Exists(candidate))
                            continue;
                        source = candidate;
                        break;
                    }

                    if (source != null)
                        break;
                }

                if (source == null)
                {
                    missing++;
                    continue;
                }

                try
                {
                    Directory.CreateDirectory(outPath);
                    File.Copy(source, Path.Combine(outPath, name), true);
                    copied++;
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"copy bundle {source} -> {name} failed: {ex.Message}");
                }
            }

            string message = "成功移动" + copied + "个文件！" +
                             (missing > 0 ? $"（{missing} 个在 DMM 目录里没找到）" : string.Empty);
            callBack?.Invoke(message);
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }

        // 「bundle 名 → DMM 客户端里的文件名（SHA1）」映射。
        // 原来读客户端 manifest.db 的 t 表，但那个库往往不存在；当前 manifest 本身就带全部名字。
        public static void CreateNameDic()
        {
            ABNameDic.Clear();
            foreach (var mgr in new[] { mgrCharacter, mgrDataJP })
            {
                if (mgr == null || mgr.registries == null)
                    continue;

                foreach (var key in mgr.registries.Keys)
                {
                    if (string.IsNullOrEmpty(key) || !key.StartsWith("a/", StringComparison.Ordinal))
                        continue;

                    var name = key.Substring(2);
                    if (ABNameDic.ContainsKey(name))
                        continue;

                    ABNameDic[name] = ComputeSHA1(name);
                }
            }

            Debug.Log($"DMM name dic rebuilt from manifest, {ABNameDic.Count} bundles");
        }
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

            // 以前 CreateNameDic 被注释掉，ABNameDic 恒为空，6/9/10/11 类永远找不到文件
            if (ABNameDic.Count == 0)
                CreateNameDic();

            foreach (string key in ABNameDic.Keys)
            {
                if (!key.Contains(containStr))
                    continue;

                listStr.Add(key);
                if (removeStrs != null && removeStrs.Length >= 2)
                {
                    string s0 = key.Replace(removeStrs[0], "");
                    string s1 = s0.Replace(removeStrs[1], "");
                    int k;
                    listInt.Add(int.TryParse(s1, out k) ? k : -1);
                }

                count++;
            }

            return count;
        }

        public static AssetManager mgrCharacter = new AssetManager();
        public static AssetManager mgrDataJP = new AssetManager();
        // 国服资源管理器已移除：日服 master 数据库改为镜像下载（见 PCRCalcularor/DataSources/JpDatabaseManager.cs）

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

            // 台服 master 与日服资源清单使用不同的版本号。
            if (ver.JpDatabaseVersion <= 0 && ver.JpDatabaseSource == (int)PCRCaculator.DataSources.JpDatabaseMirror.Taiwan)
            {
                ver.JpDatabaseVersion = await PCRCaculator.DataSources.JpDatabaseDownloader.FetchLatestVersionAsync();
                SaveManager.Save(ver);
            }
            mgrCharacter = mgrDataJP;
            await Task.Run(() => mgrDataJP.Initialize(ver.JpDatabaseVersion.ToString()));
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

        /// <summary>
        /// 战斗资源下载进度。下载线程原地更新，UI 线程只读，字段用 Interlocked/Volatile 保护。
        /// Total 会随依赖发现而增长，所以 Done/Total 比纯比例更可信。
        /// </summary>
        public sealed class BattleResourcePrepareProgress
        {
            private int done;
            private int total;
            private string current;

            public int Done => Volatile.Read(ref done);

            public int Total => Volatile.Read(ref total);

            /// <summary>最近处理的 bundle 名，用于给用户看「正在下载什么」。</summary>
            public string Current => Volatile.Read(ref current);

            public double Fraction => Total > 0 ? Math.Min(1d, (double)Done / Total) : 0d;

            internal void AddTotal(int value)
            {
                if (value > 0)
                    Interlocked.Add(ref total, value);
            }

            internal void Advance(string bundleName)
            {
                if (!string.IsNullOrEmpty(bundleName))
                    Volatile.Write(ref current, bundleName);
                Interlocked.Increment(ref done);
            }
        }

        /// <summary>同步版：阻塞调用线程直到全部资源就绪。新代码请用 <see cref="PrepareBattleUnitResourcesAsync"/>。</summary>
        public static BattleResourcePrepareSummary PrepareBattleUnitResources(IEnumerable<int> unitIds)
            => PrepareBattleUnitResourcesAsync(unitIds).GetAwaiter().GetResult();

        /// <summary>
        /// 并发预下载战斗资源（含依赖），不阻塞主线程。
        /// 按「依赖波次」推进：同一波内并发下载，波与波的依赖解析在协调线程上串行进行，
        /// 所以 visited/summary 只有协调线程会改，不需要加锁。
        /// </summary>
        public static Task<BattleResourcePrepareSummary> PrepareBattleUnitResourcesAsync(
            IEnumerable<int> unitIds,
            BattleResourcePrepareProgress progress = null,
            CancellationToken cancellationToken = default)
            => Task.Run(() => PrepareBattleUnitResourcesCoreAsync(unitIds, progress, cancellationToken), cancellationToken);

        private static async Task<BattleResourcePrepareSummary> PrepareBattleUnitResourcesCoreAsync(
            IEnumerable<int> unitIds,
            BattleResourcePrepareProgress progress,
            CancellationToken cancellationToken)
        {
            var summary = new BattleResourcePrepareSummary();
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var bundleNames = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                var optionalNames = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var unitId in unitIds ?? Enumerable.Empty<int>())
                {
                    if (unitId <= 0)
                        continue;

                    foreach (var bundleName in GetRequiredBattleUnitBundleNames(unitId))
                        bundleNames.Add(bundleName);

                    // spine 系列不在 prefab 依赖里，运行期由 SpineCreator / Cysp2Skel / modechange
                    // 第一次用到时在主线程同步下载（就是卡死的那一下），这里提前取好。
                    // 名字猜错只会被 manifest 过滤掉，不影响正确性。
                    var prefabId = GetPrefabId(unitId);
                    AddOptionalBundleNames(optionalNames, bundleNames, GetSpineBundleNames(prefabId));
                    AddOptionalBundleNames(optionalNames, bundleNames, GetIconBundleNames(prefabId));

                    // 召唤物：技能里的 SUMMON 动作会在战斗中才 new 出来，prefab 依赖里没有，
                    // 不预取就会在放技能那一帧卡主线程。这里顺着技能表把召唤物 id 找出来。
                    foreach (var summonId in CollectSummonUnitIdsForPrepare(prefabId))
                    {
                        var summonPrefabId = GetPrefabId(summonId);
                        if (summonPrefabId <= 0)
                            continue;

                        AddOptionalBundleNames(optionalNames, bundleNames, new[]
                        {
                            $"all_battleunit_{summonPrefabId}.unity3d",
                            $"all_battleunitprefab_{summonPrefabId}.unity3d",
                        });
                        AddOptionalBundleNames(optionalNames, bundleNames, GetSpineBundleNames(summonPrefabId));
                        AddOptionalBundleNames(optionalNames, bundleNames, GetIconBundleNames(summonPrefabId));
                    }
                }

                summary.RequestedBundleCount = bundleNames.Count + optionalNames.Count;

                var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var pending = bundleNames.Select(name => new PendingBundle(name, true))
                    .Concat(optionalNames.Select(name => new PendingBundle(name, false)))
                    .ToList();
                progress?.AddTotal(pending.Count);

                while (pending.Count > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    // 1) 先在协调线程上过滤：已访问、manifest 缺失的都出局
                    var wave = new List<PendingBundle>(pending.Count);
                    foreach (var item in pending)
                    {
                        if (!visited.Add(item.Name))
                            continue;

                        if (!TryGetManifestContent(item.Name, mgrCharacter, out _))
                        {
                            if (item.Required)
                            {
                                summary.SkippedManifestMissingCount++;
                                Debug.LogWarning(
                                    $"asset bundle {item.Name} has no manifest entry during battle resource prepare; " +
                                    "skipping cache precheck because resource version ownership is outside prepare");
                            }
                            else
                            {
                                summary.OptionalDependencyMissingCount++;
                                // 预取名单里带了不少「猜的」运行期资源名，缺失属正常情况，用 Log 以免刷屏
                                Debug.Log(
                                    $"optional asset bundle {item.Name} has no manifest entry during battle resource prepare; " +
                                    "keeping legacy on-demand behavior");
                            }

                            progress?.Advance(item.Name);
                            continue;
                        }

                        wave.Add(item);
                    }

                    pending = new List<PendingBundle>();
                    if (wave.Count == 0)
                        continue;

                    // 2) 同一波并发下载（每个 bundle 在线程池线程上做校验/复用/下载/写盘）
                    var results = await AssetDownloadUtil.RunBoundedAsync(
                            wave,
                            item => Task.Run(() => FetchBundleForPrepare(item), cancellationToken),
                            AssetDownloadUtil.DefaultConcurrency,
                            cancellationToken)
                        .ConfigureAwait(false);

                    // 3) 依赖解析与计数再回到协调线程，保持单线程语义
                    foreach (var result in results)
                    {
                        progress?.Advance(result.Name);

                        if (result.Error != null || result.Bytes == null)
                        {
                            summary.AddFailure(
                                result.Required
                                    ? BattleResourcePrepareStatus.CacheUnavailable
                                    : BattleResourcePrepareStatus.DependencyUnavailable,
                                result.Name,
                                "cache",
                                result.Error != null
                                    ? "download/cache validation failed"
                                    : "download/cache validation returned no bytes",
                                result.Error);
                            continue;
                        }

                        summary.EnsuredBundleCount++;

                        foreach (var depName in GetBundleDependencyNames(result.Name, result.Bytes, summary))
                        {
                            if (visited.Contains(depName))
                                continue;

                            pending.Add(new PendingBundle(depName, false));
                            progress?.AddTotal(1);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
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

        private readonly struct PendingBundle
        {
            public PendingBundle(string name, bool required)
            {
                Name = name;
                Required = required;
            }

            public string Name { get; }
            public bool Required { get; }
        }

        private sealed class BundleFetchResult
        {
            public string Name;
            public bool Required;
            public byte[] Bytes;
            public Exception Error;
        }

        /// <summary>在工作线程上执行：读本地缓存 / 复用其它版本 / 从池下载，最后写入缓存。</summary>
        private static BundleFetchResult FetchBundleForPrepare(PendingBundle item)
        {
            var result = new BundleFetchResult { Name = item.Name, Required = item.Required };
            try
            {
                result.Bytes = ReadOrRepairBundleCacheFile(item.Name, mgrCharacter);
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return result;
        }

#if UNITY_5_3_OR_NEWER
        /// <summary>解析 bundle 的依赖名（只解析，不递归下载）。调用方需保证在单线程上顺序调用。</summary>
        private static List<string> GetBundleDependencyNames(
            string fullname,
            byte[] raw,
            BattleResourcePrepareSummary summary)
        {
            var dependencies = new List<string>();
            if (raw == null)
                return dependencies;

            try
            {
                using var ms = new MemoryStream(raw);
                var ab = am.LoadBundleFile(ms, fullname);
                var assetsFileInstance = am.LoadAssetsFileFromBundle(ab, 0);
                var info = am.GetBaseField(assetsFileInstance,
                    assetsFileInstance.file.GetAssetsOfType(AssetClassID.AssetBundle).Single());

                foreach (var val in info["m_Dependencies"]["Array"])
                {
                    var depName = val.AsString;
                    if (string.IsNullOrEmpty(depName))
                        continue;

                    Debug.Log($"preparing asset bundle dependency: {fullname} => {depName}");
                    dependencies.Add(depName);
                }
            }
            catch (Exception ex)
            {
                summary.AddFailure(
                    BattleResourcePrepareStatus.DependencyParseFailed,
                    fullname,
                    "dependency-parse",
                    "failed to read bundle dependency list",
                    ex);
            }

            return dependencies;
        }
#else
        private static List<string> GetBundleDependencyNames(
            string fullname,
            byte[] raw,
            BattleResourcePrepareSummary summary)
            => new List<string>();
#endif

        private static IEnumerable<string> GetRequiredBattleUnitBundleNames(int unitid)
        {
            var prefabId = GetPrefabId(unitid);
            var battleunitBundleName = ResolveBattleunitBundleNameForPreload(prefabId);
            if (!string.IsNullOrEmpty(battleunitBundleName))
                yield return battleunitBundleName;
            yield return "all_battleunitprefab_" + prefabId + ".unity3d";
        }

        /// <summary>
        /// 运行期其它地方会按需同步下载的 spine 资源（SpineCreator 的贴图/atlas、
        /// Cysp2Skel 的 chara_base/battle/common_battle、modechange 立绘）。
        /// 这些都是可选的：manifest 里没有就跳过，不会让 prepare 失败。
        /// </summary>
        private static IEnumerable<string> GetSpineBundleNames(int prefabId)
        {
            if (prefabId <= 0)
                yield break;

            yield return $"spine_{prefabId:D6}_battle.cysp.unity3d";
            yield return $"spine_{prefabId:D6}_chara_base.cysp.unity3d";
            yield return "spine_000000_chara_base.cysp.unity3d";
            yield return $"spine_sdnormal_{prefabId}.unity3d";

            // 角色皮肤：UnitUtility.GetSkinId 对人物用 unitId + 10（★0-2）/ +30（★3-5），
            // 敌人与召唤物直接用 unitId。三种 skin 都列出来，manifest 里没有的会自动跳过。
            yield return $"spine_sdnormal_{prefabId + 10}.unity3d";
            yield return $"spine_sdnormal_{prefabId + 30}.unity3d";
            yield return $"spine_sdmodechange_{prefabId}_1.unity3d";
            yield return $"spine_sdmodechange_{prefabId + 10}_1.unity3d";
            yield return $"spine_sdmodechange_{prefabId + 30}_1.unity3d";

            var motionType = ResolveMotionTypeForPrepare(prefabId);
            yield return $"spine_{(motionType == 0 ? prefabId : motionType):D2}_common_battle.cysp.unity3d";
        }

        private static int ResolveMotionTypeForPrepare(int prefabId)
        {
#if UNITY_5_3_OR_NEWER
            try
            {
                var main = MainManager.Instance;
                if (main != null)
                    return main.GetMotionType(prefabId, prefabId);
            }
            catch (Exception ex)
            {
                Debug.Log($"prepare could not resolve motionType for {prefabId}: {ex.Message}");
            }
#endif
            return 0;
        }

        /// <summary>图标（角色头像/名牌）也是运行期同步下载的，顺手一起取。</summary>
        private static IEnumerable<string> GetIconBundleNames(int prefabId)
        {
            if (prefabId <= 0)
                yield break;

            yield return $"unit_icon_unit_{prefabId}.unity3d";
            yield return $"unit_icon_unit_{prefabId + 10}.unity3d";
            yield return $"unit_icon_unit_{prefabId + 30}.unity3d";
        }

        private static void AddOptionalBundleNames(
            ISet<string> optionalNames,
            ISet<string> requiredNames,
            IEnumerable<string> names)
        {
            if (optionalNames == null || names == null)
                return;

            foreach (var name in names)
            {
                if (string.IsNullOrEmpty(name))
                    continue;
                if (requiredNames != null && requiredNames.Contains(name))
                    continue;
                optionalNames.Add(name);
            }
        }

        /// <summary>
        /// 顺着技能表找出该单位会召唤出来的 unitId（SUMMON 动作的 ActionDetail2）。
        /// 召唤物不在 tempData 的 prefab 列表里，所以以前只能在战斗中临时下载。
        /// </summary>
        private const int SummonActionTypeId = 15; // Elements.eActionType.SUMMON
        private const int DivisionActionTypeId = 52; // Elements.eActionType.DIVISION（分身也是召出来的单位）

        private static IEnumerable<int> CollectSummonUnitIdsForPrepare(int prefabId)
        {
#if UNITY_5_3_OR_NEWER
            var main = MainManager.Instance;
            if (main == null || prefabId <= 0)
                yield break;

            var skillIds = new List<int>();
            try
            {
                if (main.TryGetEnemyParameter(prefabId, out var enemyData) && enemyData?.skillData != null)
                {
                    skillIds.Add(enemyData.skillData.UB);
                    if (enemyData.skillData.MainSkills != null)
                        skillIds.AddRange(enemyData.skillData.MainSkills);
                }
                else if (main.UnitRarityDic != null && main.UnitRarityDic.TryGetValue(prefabId, out var rarity)
                         && rarity?.skillData != null)
                {
                    var list = rarity.skillData.GetSkillList();
                    if (list != null)
                        skillIds.AddRange(list);
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"prepare could not collect skills for {prefabId}: {ex.Message}");
                yield break;
            }

            var seen = new HashSet<int>();
            foreach (var skillId in skillIds)
            {
                if (skillId <= 0)
                    continue;
                if (main.SkillDataDic == null || !main.SkillDataDic.TryGetValue(skillId, out var skill) || skill?.skillactions == null)
                    continue;

                foreach (var actionId in skill.skillactions)
                {
                    if (actionId <= 0)
                        continue;
                    if (main.SkillActionDic == null || !main.SkillActionDic.TryGetValue(actionId, out var action) || action?.details == null)
                        continue;
                    if (action.type != SummonActionTypeId && action.type != DivisionActionTypeId)
                        continue;
                    if (action.details.Length < 2)
                        continue;

                    var summonId = action.type == SummonActionTypeId
                        ? action.details[1]                                   // SummonAction: SummonId = ActionDetail2
                        : action.type == DivisionActionTypeId
                            ? action.details[0]                               // DivisionAction: DivisionId = ActionDetail1（分身）
                            : 0;
                    if (summonId > 0 && seen.Add(summonId))
                        yield return summonId;
                }
            }
#else
            // server 工程不定义 UNITY_5_3_OR_NEWER，也没有 MainManager：这里给一个空序列，
            // 让迭代器方法在两条编译路径下都有合法实现。
            yield break;
#endif
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

            // 本地来源按代价从低到高尝试，最后才联网
            var reusedBytes = TryReadFromDmmPath(fullname, mgr, file);
            if (reusedBytes != null)
                return reusedBytes;

            reusedBytes = TryReuseBundleFromOtherVersion(fullname, mgr, file);
            if (reusedBytes != null)
                return reusedBytes;

            reusedBytes = TryReuseFromPool(fullname, mgr, file);
            if (reusedBytes != null)
                return reusedBytes;
            var resolvedBytes = ResolveVerifiedBundleBytes(fullname, mgr);
            if (resolvedBytes == null)
                return null;

            return TryWriteBundleCacheFile(file, fullname, resolvedBytes, mgr) ? resolvedBytes : null;
        }

        // ------------------------------------------------------------------
        // 本地来源优先级（勾选「使用DMM路径」时生效）：
        //   1. DMM 客户端目录 <root>/<class>/<SHA1(文件名)>（不用再手动点「更新AB」）
        //   2. 其它 jp{Ver} 版本缓存目录（硬链接，同卷零额外空间）
        //   3. Pool 内容寻址缓存（按 md5 存，换版本后内容没变就直接复用）
        //   4. 都没有才联网下载
        // ------------------------------------------------------------------
        private static bool IsDmmSourceEnabled()
        {
            try
            {
                var setting = MainManager.Instance == null ? null : MainManager.Instance.PlayerSetting;
                return setting == null || setting.useDMMpath;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // DMM 客户端数据根目录（不带结尾分隔符）
        public static string GetDmmRoot()
        {
            try
            {
                var setting = MainManager.Instance == null ? null : MainManager.Instance.PlayerSetting;
                if (setting != null && !string.IsNullOrEmpty(setting.dmmPath))
                    return setting.dmmPath.TrimEnd('/', '\\');
            }
            catch (Exception)
            {
            }

            return (DMMpath ?? string.Empty).TrimEnd('/', '\\');
        }

        // 候选 DMM 根目录（按优先级，去重、只保留存在的）

        public static List<string> GetDmmRoots()
        {
            var roots = new List<string>();

            void Add(string path)
            {
                if (string.IsNullOrEmpty(path)) return;
                var trimmed = path.TrimEnd('/', '\\');
                if (trimmed.Length == 0) return;

                // 允许把路径配置到 LocalLow\Cygames 这一级或更上一层，实际数据目录是它下面的
                // PrincessConnectReDive 子目录；两种都接受，按配置原样优先。
                foreach (var candidate in new[]
                {
                    trimmed,
                    Path.Combine(trimmed, "PrincessConnectReDive")
                })
                {
                    if (!Directory.Exists(candidate)) continue;
                    if (roots.Exists(r => string.Equals(r, candidate, StringComparison.OrdinalIgnoreCase))) continue;
                    roots.Add(candidate);
                }
            }

            try
            {
                var setting = MainManager.Instance == null ? null : MainManager.Instance.PlayerSetting;
                if (setting != null && setting.useDMMpath)
                    Add(setting.dmmPath);
            }
            catch (Exception)
            {
            }

            Add(DMMpath);
            return roots;
        }

        // 「更新AB」输出目录：优先 StreamingAssets/AB，不可写时退到 persistentDataPath/AB
        public static string GetDmmOutPath()
        {
            if (!string.IsNullOrEmpty(OUTpath))
                return OUTpath;

            try
            {
                var streaming = Path.Combine(Application.streamingAssetsPath, "AB");
                Directory.CreateDirectory(streaming);
                return OUTpath = streaming;
            }
            catch (Exception)
            {
            }

            var fallback = Path.Combine(Application.persistentDataPath, "AB");
            Directory.CreateDirectory(fallback);
            return OUTpath = fallback;
        }

        private static string GetPoolCacheRoot()
        {
            var root = string.IsNullOrWhiteSpace(persistentDataPath) ? Application.persistentDataPath : persistentDataPath;
            return Path.Combine(root, "Pool");
        }

        // 直接从 DMM 客户端目录取 bundle，校验通过后硬链接进当前版本缓存。
        // 直接从 DMM 客户端目录取 bundle，校验通过后硬链接进当前版本缓存。
        // DMM 客户端布局：<root>/<class>/<SHA1(文件名)>（class 例如 a / b / m / s / v）。
        private static byte[] TryReadFromDmmPath(string fullname, AssetManager mgr, string targetFile)
        {
            try
            {
                var roots = GetDmmRoots();
                if (roots.Count == 0)
                    return null;

                if (!IsDmmSourceEnabled())
                    return null;
                if (!TryGetManifestContent(fullname, mgr, out var content))
                    return null;

                var sha1Name = ComputeSHA1(fullname);
                var className = string.IsNullOrEmpty(content.@class) ? "a" : content.@class;
                bool presentButMismatched = false;

                foreach (var root in roots)
                {
                    var candidates = new[]
                    {
                        Path.Combine(root, className, sha1Name),
                        Path.Combine(root, "a", sha1Name),
                        Path.Combine(root, className, fullname),
                        Path.Combine(root, "a", fullname),
                        Path.Combine(root, fullname)
                    };

                    foreach (var candidate in candidates)
                    {
                        if (!File.Exists(candidate))
                            continue;

                        var info = new FileInfo(candidate);
                        if (content.size > 0 && info.Length != content.size)
                        {
                            presentButMismatched = true;
                            continue;
                        }

                        var bytes = File.ReadAllBytes(candidate);
                        if (!IsExpectedBundleBytes(fullname, bytes, mgr, candidate))
                        {
                            presentButMismatched = true;
                            continue;
                        }
                        if (!TryLinkOrCopy(candidate, targetFile))
                            continue;

                        Debug.Log($"asset bundle {fullname} reused from DMM path: {candidate} -> {targetFile}");
                        return bytes;
                    }
                }

                if (presentButMismatched)
                    Debug.LogWarning($"asset bundle {fullname} found in DMM path but size/md5 does not match current version, skipped");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"asset bundle {fullname} read from DMM path failed: {ex.Message}");
            }

            return null;
        }

        // Pool 是内容寻址（文件名就是 bundle 的 md5），版本号变了但内容没变就能直接复用。
        private static byte[] TryReuseFromPool(string fullname, AssetManager mgr, string targetFile)
        {
            try
            {
                if (!TryGetManifestContent(fullname, mgr, out var content))
                    return null;

                var md5 = content.MD5;
                if (string.IsNullOrEmpty(md5))
                    return null;

                var pooled = Path.Combine(GetPoolCacheRoot(), md5);
                if (!File.Exists(pooled))
                    return null;

                var info = new FileInfo(pooled);
                if (content.size > 0 && info.Length != content.size)
                    return null;

                var bytes = File.ReadAllBytes(pooled);
                if (!IsExpectedBundleBytes(fullname, bytes, mgr, pooled))
                    return null;
                if (!TryLinkOrCopy(pooled, targetFile))
                    return null;

                Debug.Log($"asset bundle {fullname} reused from pool cache: {pooled} -> {targetFile}");
                return bytes;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"asset bundle {fullname} pool reuse failed: {ex.Message}");
                return null;
            }
        }

        // 跨版本复用：同名 bundle 若已存在于其它 jp{Ver} 缓存目录且 md5 与当前 manifest 一致，
        // 直接硬链接（同卷零开销）到当前版本目录，省掉一次下载。
        private static byte[] TryReuseBundleFromOtherVersion(string fullname, AssetManager mgr, string targetFile)
        {
            try
            {
                var cacheDir = Path.GetDirectoryName(targetFile);
                var root = string.IsNullOrEmpty(cacheDir) ? null : Path.GetDirectoryName(cacheDir);
                if (string.IsNullOrEmpty(root) || !Directory.Exists(root)) return null;

                if (!TryGetManifestContent(fullname, mgr, out var content))
                    return null;

                var dirs = Directory.GetDirectories(root)
                    .Where(dir => !Path.GetFileName(dir).StartsWith(".", StringComparison.Ordinal))
                    .Where(dir => !string.Equals(dir, cacheDir, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(dir => Path.GetFileName(dir), StringComparer.Ordinal);

                foreach (var dir in dirs)
                {
                    var candidate = Path.Combine(dir, fullname);
                    if (!File.Exists(candidate))
                        continue;

                    // 大小不符就不必再算 md5
                    var info = new FileInfo(candidate);
                    if (content.size > 0 && info.Length != content.size)
                        continue;

                    var candidateBytes = File.ReadAllBytes(candidate);
                    if (!IsExpectedBundleBytes(fullname, candidateBytes, mgr, candidate))
                        continue;
                    if (!TryLinkOrCopy(candidate, targetFile))
                        continue;

                    Debug.Log($"asset bundle {fullname} reused from other version cache: {candidate} -> {targetFile}");
                    return candidateBytes;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"asset bundle {fullname} cross-version reuse failed: {ex.Message}");
            }

            return null;
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CreateHardLink(string lpFileName, string lpExistingFileName,
            IntPtr lpSecurityAttributes);

        private static bool TryLinkOrCopy(string source, string target)
        {
            try
            {
                EnsureParentDirectory(target);
                if (File.Exists(target)) File.Delete(target);
#if UNITY_ANDROID || PLATFORM_ANDROID
                File.Copy(source, target, true);
#else
                if (!CreateHardLink(target, source, IntPtr.Zero))
                    File.Copy(source, target, true);
#endif
                return File.Exists(target);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"bundle cache link/copy failed {source} -> {target}: {ex.Message}");
                return false;
            }
        }

#if UNITY_5_3_OR_NEWER
        private static AssetsManager am = new AssetsManager();
        private static HashSet<string> loadedDep = new HashSet<string>();

        /// <summary>
        /// 运行期依赖预载。老实现是一条依赖一个来回地串行下载（主线程一等就是几百毫秒），
        /// 现在按「依赖波次」推进：同一层的所有依赖一起并发下载，下一层再并发，
        /// 单层耗时约为串行的 1/并发数。方法本身仍是同步的（调用方要的是已就绪的 bundle），
        /// 但等待期间下载是真的多线程在跑。
        /// </summary>
        private static void LoadDependency(string path, byte[] raw, AssetManager mgr)
        {
            var wave = new List<KeyValuePair<string, byte[]>>();
            if (raw != null)
                wave.Add(new KeyValuePair<string, byte[]>(path, raw));

            while (wave.Count > 0)
            {
                var nextNames = new List<string>();
                var seenThisRound = new HashSet<string>();

                foreach (var item in wave)
                {
                    if (!loadedDep.Add(item.Key))
                        continue;

                    var dependencies = ParseBundleDependencyNames(item.Key, item.Value);
                    foreach (var depName in dependencies)
                    {
                        Debug.Log($"resolving asset bundle dependency: {item.Key} => {depName} " +
                                  $"(layer={wave.Count} bundle(s), parallel={AssetDownloadUtil.DefaultConcurrency})");
                        if (loadedDep.Contains(depName) || !seenThisRound.Add(depName))
                            continue;
                        nextNames.Add(depName);
                    }
                }

                if (nextNames.Count == 0)
                    break;

                var fetched = AssetDownloadUtil.RunSync(() =>
                    AssetDownloadUtil.RunBoundedAsync(
                        nextNames,
                        name => Task.Run(() => ReadOrRepairBundleCacheFile(name, mgr)),
                        AssetDownloadUtil.DefaultConcurrency));

                wave = new List<KeyValuePair<string, byte[]>>(nextNames.Count);
                for (var i = 0; i < nextNames.Count; i++)
                {
                    if (fetched != null && i < fetched.Length && fetched[i] != null)
                        wave.Add(new KeyValuePair<string, byte[]>(nextNames[i], fetched[i]));
                }
            }
        }

        /// <summary>只解析 bundle 里的依赖名，不做任何下载。</summary>
        private static List<string> ParseBundleDependencyNames(string fullname, byte[] raw)
        {
            var dependencies = new List<string>();
            if (raw == null)
                return dependencies;

            try
            {
                using var ms = new MemoryStream(raw);
                var ab = am.LoadBundleFile(ms, fullname);
                var assetsFileInstance = am.LoadAssetsFileFromBundle(ab, 0);
                var info = am.GetBaseField(assetsFileInstance,
                    assetsFileInstance.file.GetAssetsOfType(AssetClassID.AssetBundle).Single());

                foreach (var val in info["m_Dependencies"]["Array"])
                {
                    var depName = val.AsString;
                    if (!string.IsNullOrEmpty(depName))
                        dependencies.Add(depName);
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"failed to read bundle dependency list of {fullname}: {ex.Message}");
            }

            return dependencies;
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
                if (!File.Exists(GetBundleCachePath(fullname, mgr)))
                {
                    // 走到这里说明预取没覆盖到：接下来会在主线程同步下载，画面会顿一下。
                    // 把名字打出来，方便把漏掉的资源加进 GetSpineBundleNames / 预取名单。
                    Debug.LogWarning(
                        $"asset bundle {fullname} was not prefetched; downloading synchronously on the main thread");
                }

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
        public enum SpriteType { 角色图标=0,角色长条=1,装备图标=2,技能图标=3,装备碎片=4,EX装备=5,EX装备类型=6,黎明界遗物=7,黎明界科技树=8,道具图标=9,道具图标无效=10 }
        /// <summary>
        /// 计算某个精灵图所在的 AB 包名（只算名字，不加载）。
        /// </summary>
        public static string GetSpriteBundleName(SpriteType type, int id)
        {
            switch (type)
            {
                case SpriteType.角色图标:
                    return "unit_icon_unit_" + id + ".unity3d";
                case SpriteType.角色长条:
                    return "icon_unit_plate_" + (id >= 200000 ? id : id + 30) + ".unity3d";
                case SpriteType.装备图标:
                    return "icon_icon_equipment_" + id + ".unity3d";
                case SpriteType.装备碎片:
                    return "icon_icon_equipment_invalid_" + id + ".unity3d";
                case SpriteType.技能图标:
                    return "icon_icon_skill_" + id + ".unity3d";
                case SpriteType.EX装备:
                    return "icon_icon_extra_equip_" + id + ".unity3d";
                case SpriteType.EX装备类型:
                    return "icon_icon_extra_equip_category_" + id + ".unity3d";
                case SpriteType.黎明界遗物:
                    return "icon_labyrinth_icon_relic_" + id + ".unity3d";
                case SpriteType.黎明界科技树:
                    return "labyrinth_node_icon_0" + id + ".unity3d";
                case SpriteType.道具图标:
                    return "icon_icon_item_" + id + ".unity3d";
                case SpriteType.道具图标无效:
                    return "icon_icon_item_invalid_" + id + ".unity3d";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 预取一批精灵图所在的 AB 包：只保证包文件已经落到本地缓存（并行下载 + 校验），
        /// 不解析、不解码。这样界面第一次显示图标时只需要读本地文件，不会在主线程同步走网络。
        /// </summary>
        public static void PrefetchSpriteBundles(SpriteType type, IEnumerable<int> ids)
        {
#if UNITY_5_3_OR_NEWER
            if (ids == null)
                return;
            var bundleNames = new HashSet<string>();
            foreach (int id in ids)
            {
                string bundleName = GetSpriteBundleName(type, id);
                if (!string.IsNullOrEmpty(bundleName))
                    bundleNames.Add(bundleName);
            }
            if (bundleNames.Count == 0)
                return;

            var names = bundleNames.ToArray();
            AssetManager manager = mgrCharacter;
            AssetDownloadUtil.RunSync(() => AssetDownloadUtil.RunBoundedAsync(
                names,
                name => Task.Run(() =>
                {
                    if (!TryGetManifestContent(name, manager, out _))
                        return false;
                    ReadOrRepairBundleCacheFile(name, manager);
                    return true;
                }),
                AssetDownloadUtil.DefaultConcurrency));
#endif
        }

        /// <summary>
        /// 取一个 AB 包的字节：有缓存读缓存，没有就下载并校验 md5。取不到（manifest 里没有 / 下载失败）返回 null。
        /// 供 BuffIconAtlasUpdater 这类需要在运行期替换内置图集的功能使用。
        /// </summary>
        public static byte[] GetCachedBundleBytes(string fullname)
        {
            if (string.IsNullOrEmpty(fullname))
                return null;

            if (!TryGetManifestContent(fullname, mgrCharacter, out _))
            {
                Debug.Log($"asset bundle {fullname} is not in the current manifest");
                return null;
            }

            try
            {
                return ReadOrRepairBundleCacheFile(fullname, mgrCharacter);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"failed to read asset bundle {fullname}: {ex.Message}");
                return null;
            }
        }

#if UNITY_5_3_OR_NEWER
        private static bool spriteWarmupStarted;
        /// <summary>预热阶段后台下载的总时限（秒）；超时就把剩下的留给按需加载。</summary>
        private const float MaxWarmupDownloadSeconds = 180f;
        /// <summary>单个 AB 包异步载入的超时（秒）。</summary>
        private const float SingleBundleLoadTimeoutSeconds = 20f;

        /// <summary>
        /// 启动后台「精灵图包体预热」：把界面里会用到的一大批图标 AB 包，按优先级慢慢载进来。
        /// 每帧只处理一个包、用 LoadFromFileAsync 异步载入，不阻塞主线程；预热完成后
        /// <see cref="GetSprites"/> 基本都是纯内存命中，不会再出现点开界面卡住的情况。
        /// </summary>
        public static void StartSpriteWarmup()
        {
            if (spriteWarmupStarted)
                return;
            var host = MainManager.Instance;
            if (host == null)
                return;
            spriteWarmupStarted = true;
            host.StartCoroutine(WarmSpriteBundlesRoutine(BuildSpriteWarmupList(), null));
        }

        /// <summary>
        /// 预热名单：按「用户最常点开」的顺序排。EX 装备和装备图标最容易卡，放最前。
        /// </summary>
        public static List<KeyValuePair<SpriteType, int>> BuildSpriteWarmupList()
        {
            var list = new List<KeyValuePair<SpriteType, int>>();
            var main = MainManager.Instance;
            if (main == null)
                return list;

            // 顺序 = 用户最常点开的顺序：EX 装备 > 角色头像 > 技能图标 > BOSS 头像。
            // 只预热前 MaxWarmupBundles 个，避免整个图标库都常驻内存。

            // 1. EX 装备（点角色弹出装备下拉时最费时）
            if (main.databaseCache.exEquipmentName_cn != null)
                foreach (int id in main.databaseCache.exEquipmentName_cn.Keys)
                    list.Add(new KeyValuePair<SpriteType, int>(SpriteType.EX装备, id));
            // 分类图标的 id 来自数据库 ex_equipment_data.category（101..110 / 201..204 / 301..305），不是 1..9
            var exCategories = new SortedSet<int>();
            if (main.UnitExEquips != null)
            {
                foreach (var slots in main.UnitExEquips.Values)
                {
                    if (slots == null)
                        continue;
                    foreach (var slot in slots)
                    {
                        if (slot == null)
                            continue;
                        foreach (var equip in slot.Values)
                        {
                            if (equip != null && equip.category > 0)
                                exCategories.Add(equip.category);
                        }
                    }
                }
            }
            foreach (int category in exCategories)
                list.Add(new KeyValuePair<SpriteType, int>(SpriteType.EX装备类型, category));

            // 2. 角色头像（选人界面 / 编队 / 编队详情）
            if (main.UnitRarityDic != null && main.unitDataDic != null)
            {
                foreach (int unitId in main.UnitRarityDic.Keys)
                {
                    UnitData data = main.unitDataDic.TryGetValue(unitId, out UnitData saved) ? saved : new UnitData(unitId, 1);
                    list.Add(new KeyValuePair<SpriteType, int>(SpriteType.角色图标, CharacterPageButton.GetCharacterIconId(data)));
                }
            }

            const int MaxWarmupBundles = 900;
            if (list.Count >= MaxWarmupBundles)
            {
                list.RemoveRange(MaxWarmupBundles, list.Count - MaxWarmupBundles);
                return list;
            }

            // 3. 技能图标
            if (main.SkillDataDic != null)
            {
                var icons = new HashSet<int>();
                foreach (var pair in main.SkillDataDic)
                {
                    if (pair.Value != null && pair.Value.icon > 0 && icons.Add(pair.Value.icon))
                        list.Add(new KeyValuePair<SpriteType, int>(SpriteType.技能图标, pair.Value.icon));
                }
            }

            // 4. BOSS / 敌人头像（切换 BOSS 时复用的也是这套 icon bundle）
            if (main.EnemyDataDic != null)
            {
                foreach (var pair in main.EnemyDataDic)
                {
                    EnemyData enemyData = pair.Value;
                    if (enemyData?.detailData == null)
                        continue;
                    int prefabId = enemyData.detailData.prefab_id;
                    if (prefabId <= 0)
                        continue;
                    list.Add(new KeyValuePair<SpriteType, int>(SpriteType.角色图标, prefabId));
                    if (enemyData.detailData.visual_change_flag == 1)
                        list.Add(new KeyValuePair<SpriteType, int>(SpriteType.角色图标,
                            prefabId + (enemyData.rarity > 5 ? 60 : 30)));
                }
            }

            if (list.Count > MaxWarmupBundles)
                list.RemoveRange(MaxWarmupBundles, list.Count - MaxWarmupBundles);

            // 最后按 manifest 过滤一遍：像 4xxxxx 的敌人 prefab 根本没有对应头像包，
            // 留在名单里只会让每次启动白跑失败请求。
            int dropped = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                string bundleName = GetSpriteBundleName(list[i].Key, list[i].Value);
                if (string.IsNullOrEmpty(bundleName) || !TryGetManifestContent(bundleName, mgrCharacter, out _))
                {
                    list.RemoveAt(i);
                    dropped++;
                }
            }
            if (dropped > 0)
                Debug.Log($"sprite warmup candidate filtered: dropped={dropped}, kept={list.Count}");

            return list;
        }

        /// <summary>
        /// 逐帧预热一批精灵图 AB 包（协程版）。每个包：缺文件就先在后台线程下载，再异步载入，
        /// 全程不阻塞主线程；已经载过的包直接跳过。
        /// </summary>
        public static IEnumerator WarmSpriteBundlesRoutine(List<KeyValuePair<SpriteType, int>> items,
            Action<int, int> onProgress)
        {
            if (items == null || items.Count == 0)
                yield break;

            var names = new List<string>(items.Count);
            var seen = new HashSet<string>();
            for (int i = 0; i < items.Count; i++)
            {
                string name = GetSpriteBundleName(items[i].Key, items[i].Value);
                if (string.IsNullOrEmpty(name) || !seen.Add(name))
                    continue;
                if (AssetBundleDic.ContainsKey(name))
                    continue;
                names.Add(name);
            }

            // 第一遍：本地已经有包体的，逐帧异步载入
            var notDownloaded = new List<string>();
            for (int i = 0; i < names.Count; i++)
            {
                string name = names[i];
                string path = GetBundleCachePath(name, mgrCharacter);
                if (!File.Exists(path))
                {
                    notDownloaded.Add(name);
                    continue;
                }
                yield return LoadSpriteBundleAsync(name, path);
                onProgress?.Invoke(i + 1, names.Count);
            }

            if (notDownloaded.Count == 0)
            {
                Debug.Log($"sprite bundle warmup finished: warmed={AssetBundleDic.Count}, listed={names.Count}, downloaded=0");
                yield break;
            }

            // 第二遍：本地没有的，在后台线程静默下载（不阻塞主线程），下完谁先到就先载谁；
            // 用户如果在这之前点开某个界面，GetSprites 仍会走原来的按需同步下载兜底。
            Debug.Log($"sprite bundle warmup: {notDownloaded.Count} bundles not cached yet, downloading silently in background");
            var downloadTask = Task.Run(() => AssetDownloadUtil.RunBoundedAsync(
                notDownloaded,
                name => Task.Run(() =>
                {
                    try
                    {
                        if (TryGetManifestContent(name, mgrCharacter, out _))
                            ReadOrRepairBundleCacheFile(name, mgrCharacter);
                    }
                    catch (Exception downloadException)
                    {
                        Debug.LogWarning($"sprite bundle warmup download failed for {name}: {downloadException.Message}");
                    }
                    return true;
                }),
                AssetDownloadUtil.DefaultConcurrency).GetAwaiter().GetResult());

            var loaded = new HashSet<string>();
            float downloadDeadline = Time.realtimeSinceStartup + MaxWarmupDownloadSeconds;
            while (true)
            {
                bool progressed = false;
                for (int i = 0; i < notDownloaded.Count; i++)
                {
                    string name = notDownloaded[i];
                    if (loaded.Contains(name) || AssetBundleDic.ContainsKey(name))
                    {
                        loaded.Add(name);
                        continue;
                    }
                    string path = GetBundleCachePath(name, mgrCharacter);
                    if (!File.Exists(path))
                        continue;
                    yield return LoadSpriteBundleAsync(name, path);
                    loaded.Add(name);
                    progressed = true;
                    break;
                }

                if (downloadTask.IsCompleted && !progressed)
                    break;
                if (Time.realtimeSinceStartup > downloadDeadline)
                {
                    Debug.LogWarning($"sprite bundle warmup download phase exceeded {MaxWarmupDownloadSeconds}s, " +
                                     "剩下没下完的包留到下次启动或点开时再按需加载");
                    break;
                }
                if (!progressed)
                    yield return null;
            }

            var stillMissing = new List<string>();
            for (int i = 0; i < notDownloaded.Count; i++)
            {
                if (!AssetBundleDic.ContainsKey(notDownloaded[i]))
                    stillMissing.Add(notDownloaded[i]);
            }
            Debug.Log($"sprite bundle warmup finished: warmed={AssetBundleDic.Count}, listed={names.Count}, " +
                      $"downloaded={notDownloaded.Count - stillMissing.Count}, stillMissing={stillMissing.Count}");
            if (stillMissing.Count > 0)
            {
                try
                {
                    string dump = Path.Combine(persistentDataPath, "warmup-missing.txt");
                    File.WriteAllLines(dump, stillMissing.ToArray());
                    Debug.LogWarning($"sprite bundle warmup still missing list written to {dump}");
                }
                catch (Exception dumpException)
                {
                    Debug.LogWarning($"写出预热缺失清单失败: {dumpException.Message}");
                }
            }
        }

        /// <summary>异步载入一个精灵图 AB 包（每帧只推进一个，不阻塞主线程）。</summary>
        private static IEnumerator LoadSpriteBundleAsync(string name, string path)
        {
            if (AssetBundleDic.ContainsKey(name))
                yield break;
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path);
            if (request == null)
                yield break;
            float deadline = Time.realtimeSinceStartup + SingleBundleLoadTimeoutSeconds;
            while (!request.isDone)
            {
                if (Time.realtimeSinceStartup > deadline)
                {
                    Debug.LogWarning($"sprite bundle warmup load timeout: {name}");
                    yield break;
                }
                yield return null;
            }
            AssetBundle bundle = request.assetBundle;
            if (bundle == null)
                yield break;
            if (!AssetBundleDic.ContainsKey(name))
                AssetBundleDic.Add(name, bundle);
            else
                bundle.Unload(false);
            yield return null;
        }
#endif

        public static Sprite GetSprites(SpriteType type, int id)
        {
#if UNITY_5_3_OR_NEWER
            string assetName = GetSpriteBundleName(type, id);
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

        public static int GetSummonSkinId(int prefabId, int rarity)
        {
            int skinId = Elements.UnitUtility.GetSkinId(prefabId, rarity);
            if (TryGetManifestContent($"spine_sdnormal_{skinId}.unity3d", mgrCharacter, out _))
                return skinId;
            int defaultSkin = Elements.UnitUtility.GetSkinId(prefabId, 3);
            return TryGetManifestContent($"spine_sdnormal_{defaultSkin}.unity3d", mgrCharacter, out _)
                ? defaultSkin : skinId;
        }

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
            else if (MainManager.Instance.TryGetEnemyParameter(unitid, out var enemy))
            {
                return enemy.detailData.prefab_id;
            }
            return unitid;
        }

        public static void FreeDeleteAndReset(int ver, int verold)
        {
            MainManager.Instance.Version.JpDatabaseVersion = ver;
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
