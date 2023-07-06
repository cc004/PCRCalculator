using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Mono.Data.Sqlite;
using PCRApi;
using UnityEditor;
using UnityEngine;
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
        static Dictionary<string, AssetBundle> AssetBundleDic = new Dictionary<string, AssetBundle>();
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
                    oldPath.Add("manifest/moviemanifest");
                    oldPath.Add("manifest/unit_assetmanifest");
                    oldPath.Add("manifest/sound2manifest");

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

        public static Task StaticInitialize(Tuple<int?, int?> _ = default)
        {
            var ver = MainManager.Instance.Version;

            return Task.WhenAll(
                Task.Run(() => mgrCharacter.Initialize(ver.CharacterVersionJP.ToString())),
                Task.Run(() => mgrDataJP.Initialize(ver.BossVersionJP.ToString())),
                Task.Run(() => mgrDataCN.Initialize(ver.BossVersionCN.ToString(),
                    ver.useQA))
            );
        }

        public static IEnumerable<(AssetManager mgr, Content content)> CacheAllFiles(Regex regex, bool useOld)
        {
            var m = (useOld ? mgrDataJP : mgrCharacter);
            foreach (var pair in m.registries)
                if (regex.IsMatch(pair.Key))
                    yield return (m, pair.Value);
        }

        public static AssetBundle TryGetAssetBundleByName(string fullname, bool useOldManifest)
        {
            if (AssetBundleDic.TryGetValue(fullname, out var val)) return val;

            //internal packed
            var path = Application.streamingAssetsPath + "/AB/" + fullname;
#if PLATFORM_ANDROID
            var www = new WWW(path);
            while (!www.isDone) { Thread.Sleep(0); }
            path = Application.persistentDataPath + "/AB/" + fullname;
#else
            var www = new WWW(path);
            while (!www.isDone) { Thread.Sleep(0); }
            path = Application.streamingAssetsPath + "/../.ABExt2/" + fullname;
#endif
            var asset = www.getAssetBundle();
            if (asset == null)
            {
                if (!File.Exists(path))
                {
                    var bytes = (useOldManifest ? mgrDataJP : mgrCharacter).ResolveFile("a/" + fullname);
                    if (bytes != null) File.WriteAllBytes(path, bytes);
                }
                www = new WWW("file://" + path);
                while (!www.isDone) { Thread.Sleep(0); }
                asset = www.assetBundle;
            }

            AssetBundleDic.Add(fullname, asset);

            return asset;
        }

        private static Dictionary<(Type, string, string, bool), object>
            GetAssetBundleByNameCache = new Dictionary<(Type, string, string, bool), object>();

        public static string persistentDataPath;

        public static string dataPath;

        public static T GetAssetBundleByName<T>(string fullname, string fit = "", bool useOldManifest = false) where T : Object
        {
            var key = (typeof(T), fullname, fit, useOldManifest);
            if (GetAssetBundleByNameCache.TryGetValue(key, out var val))
                return (T)val;
            var asset = TryGetAssetBundleByName(fullname, useOldManifest);
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
            var asset = TryGetAssetBundleByName(fullname, useOldManifest);
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
        public enum SpriteType { 角色图标=0,角色长条=1,装备图标=2,技能图标=3,装备碎片=4}
        public static Sprite GetSprites(SpriteType type, int id)
        {
            string assetName = "";
            switch (type)
            {
                case SpriteType.角色图标:
                    assetName = "unit_icon_unit_" + (id >= 200000 ? id : id + 30) + ".unity3d";
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

        }
        public static GameObject LoadUnitPrefab(int unitid)
        {
            //if (unitid >= 200000 && unitid <= 299999)
            //    unitid = (int)(unitid / 100) * 100;
            // preload battleunit
            TryGetAssetBundleByName("all_battleunit_" + GetPrefabId(unitid) + ".unity3d", false);
            GameObject a = GetAssetBundleByName<GameObject>("all_battleunitprefab_" + GetPrefabId(unitid) + ".unity3d", "prefab", unitid > 200000);
            /*if(a==null)
            {
                unitid = (int)(unitid / 100) * 100;
                a = GetAssetBundleByName<GameObject>("all_battleunitprefab_" + unitid + ".unity3d", "prefab");
            }*/
            return a;
        }
        public static int GetPrefabId(int unitid)
        {
            if (unitid == 207001)
                return 207000;
            if (unitid == 105701)
                return 170101;
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