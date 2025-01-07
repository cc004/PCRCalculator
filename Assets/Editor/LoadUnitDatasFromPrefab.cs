using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Elements;
using PCRCaculator;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

public class LoadUnitDatasFromPrefab:MonoBehaviour
{
    static string unitPackageName = "all_battleunit_";
    static string unitPackagePrefabName = "all_battleunitprefab_";
    string[] dependPackageNameList = new string[] { "all_fxsk_0028", "all_fxsk_0029", "all_fxsk_0030", "all_fxsk_0031", "all_fxsk_0043", "all_fxsk_0044" };
    int unitId = 101201;

    Dictionary<string, GameObject> allUnitSkillPrefabs = new Dictionary<string, GameObject>();

    private AssetBundle AB;

    [MenuItem("PCRTools/下载旧版Boss数据")]
    public static void DownloadAllFilesOld()
    {
        var regexes = new (string, bool)[]
        {
            (@"a/all_battleunitprefab_30260.\.unity3d", false),
        };

        ABExTool.StaticInitialize(new Tuple<int?, int?>(10028800, 10028800)).Wait();
        var client = new HttpClient();
        
        foreach (var (reg, useNew) in regexes)
        {
            foreach (var (mgr, content) in ABExTool.CacheAllFiles(new Regex(reg), !useNew))
            {
                var filePath = Application.streamingAssetsPath + "/AB/" + content.url.Split('/').Last();
                if (File.Exists(filePath)) continue;
                Debug.Log($"resolving: {content.url}");
                var resp = mgr.ResolveFile(content.url);
                File.WriteAllBytes(filePath,
                    resp);
            }
        }
    }

    [MenuItem("PCRTools/下载全部数据")]
    public static void DownloadAllFiles()
    {
        var regexes = new (string, bool)[]
        {
            (@"a/all_abnormalstateicon\.unity3d", true),
            (@"a/all_abnormalstateicon.\.unity3d", true),
            (@"a/all_battleunit_1\d\d\d\d\d(_\d*)?\.unity3d", true),
            (@"a/all_battleunit_[2-9]\d\d\d\d\d(_\d*)?\.unity3d", false),
            (@"a/all_battleunitprefab_1\d\d\d\d\d(_\d*)?\.unity3d", true),
            (@"a/all_battleunitprefab_[2-9]\d\d\d\d\d(_\d*)?\.unity3d", false),
            (@"a/all_fxsk_\d\d\d\d\.unity3d", true),
            (@"a/icon_icon_equipment_.*\.unity3d", true),
            (@"a/icon_icon_skill_.*\.unity3d", true),
            (@"a/icon_unity_plate_.*\.unity3d", true),
            (@"a/spine_.*\.cysp\.unity3d", true),
            (@"a/spine_sdmodechange_.*\.unity3d", true),
            (@"a/spine_sdnormal_.*\.unity3d", true),
            (@"a/unit_icon_unit_.*\.unity3d", true),
        };

        ABExTool.StaticInitialize().Wait();
        var client = new HttpClient();
        
        foreach (var (reg, useNew) in regexes)
        {
            foreach (var (mgr, content) in ABExTool.CacheAllFiles(new Regex(reg), !useNew))
            {
                var filePath = ABExTool.persistentDataPath + "/.ABExt2/" + content.url.Split('/').Last();
                if (File.Exists(filePath)) continue;
                Debug.Log($"resolving: {content.url}");
                var resp = mgr.ResolveFile(content.url);
                File.WriteAllBytes(filePath,
                    resp);
            }
        }
    }
    
    [MenuItem("PCRTools/生成技能时间数据到txt")]
    public static void CreateUnitTimeDic()
    {
        Dictionary<int, UnitSkillTimeData> unitSkillTimeDic = new Dictionary<int, UnitSkillTimeData>();
        for (int unitId = 100101; unitId < 110001; unitId += 100)
        {
            string filePath = Application.dataPath + "/unitSkillTime/" + unitId + ".json";
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string jsonStr = sr.ReadToEnd();
                sr.Close();
                if (jsonStr != "")
                {
                    PCRCaculator.UnitSkillTimeData skillTimeData = JsonConvert.DeserializeObject<PCRCaculator.UnitSkillTimeData>(jsonStr);
                    unitSkillTimeDic.Add(skillTimeData.unitId, skillTimeData);
                }
            }
        }
        string path = Application.dataPath + "/Txts/unitSkillTimeDic.txt";
        string saveJsonStr = JsonConvert.SerializeObject(unitSkillTimeDic);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(saveJsonStr);
        sw.Close();
        Debug.Log("成功! " + path);
    }

    //[MenuItem("PCRTools/从角色Prefab加载技能特效数据到json")]
    public static void LoadUnitDatas2json()
    {
        LoadUnitDatasFromPrefab st = new LoadUnitDatasFromPrefab();
        st.LoadUnitFirearmCtrls();
    }
    private void LoadUnitFirearmCtrls()
    {
        foreach(string path in dependPackageNameList)
        {
            LoadDependPrefabs(path);
        }
        //StartCoroutine(LoadScene());
        //LoadUnitEffectDatas();
        LoadUnitActionControllers();
       // SaveToJson();
    }
    void LoadDependPrefabs(string path)
    {
        WWW www = new WWW("file:///" + Application.dataPath + "/AB/" + unitPackageName + unitId + ".unity3d");
        //www.assetBundle.Unload(true);
        foreach (string path_0 in www.assetBundle.GetAllAssetNames())
        {
            try
            {
                if (path.Contains(".prefab"))
                {
                    GameObject a = Instantiate(www.assetBundle.LoadAsset<GameObject>(path));
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("load " + path + " failed beacuse " + e.Message);
            }
        }

    }
    /*void LoadUnitEffectDatas()
    {         //文件路径，也就是我们打包的那个         
        WWW www = new WWW("file:///" + Application.dataPath + "/AB/" + unitPackageName + unitId + ".unity3d");
        Dictionary<string, FirearmCtrlData> dic = new Dictionary<string, FirearmCtrlData>();
        foreach (string path in www.assetBundle.GetAllAssetNames())
        {
            try
            {
                if (path.Contains(".mat"))
                {
                    //www.assetBundle.LoadAsset<Material>(path);
                }
                else if (path.Contains(".prefab"))
                {
                    //Instantiate(www.assetBundle.LoadAsset<GameObject>(path));
                    //www.assetBundle.LoadAssetWithSubAssets<GameObject>(path);
                    GameObject a = Instantiate(www.assetBundle.LoadAsset<GameObject>(path));
                    allUnitSkillPrefabs.Add(a.name, a);
                    if (a.TryGetComponent<FirearmCtrl>(out FirearmCtrl firearmCtrl))
                    {
                        string name_A = "default";
                        if (a.name.Contains("skill0"))
                        {
                            name_A = "skill0";
                        }
                        else if (a.name.Contains("skill1"))
                        {
                            name_A = "skill1";
                        }
                        else if (a.name.Contains("skill2"))
                        {
                            name_A = "skill2";
                        }
                        if (!dic.ContainsKey(name_A))
                        {
                            dic.Add(name_A, firearmCtrl.GetPrefabData());
                        }
                        else
                        {
                            Debug.Log(unitId + "的技能数据重复！");
                        }

                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("load " + path + " failed beacuse " + e.Message);
            }
        }
        allUnitFirearmDatas.Add(unitId,dic);
        foreach(GameObject a in allUnitSkillPrefabs.Values)
        {
            DestroyImmediate(a);
        }

    }*/
    void LoadUnitActionControllers()
    {
        WWW www = new WWW("file:///" + Application.dataPath + "/AB/" + unitPackagePrefabName + unitId + ".unity3d");
        //www.assetBundle.Unload(true);
        foreach (string path in www.assetBundle.GetAllAssetNames())
        {
            try
            {
                GameObject[] allDependPrefabs = www.assetBundle.LoadAssetWithSubAssets<GameObject>(path);
                foreach(GameObject p in allDependPrefabs)
                {
                    Instantiate(p);
                }/*
                if (path.Contains(".prefab"))
                {
                    //Instantiate(www.assetBundle.LoadAsset<GameObject>(path));
                    //www.assetBundle.LoadAssetWithSubAssets<GameObject>(path);
                    GameObject a = Instantiate(www.assetBundle.LoadAsset<GameObject>(path));
                    allUnitSkillPrefabs.Add(a.name, a);
                    if (a.TryGetComponent<Elements.UnitActionController>(out Elements.UnitActionController actionController))
                    {
                        allUnitActionControllerDatas.Add(unitId, actionController.GetUnitActionControllerData());
                    }
                    DestroyImmediate(a);
                }*/
            }
            catch (System.Exception e)
            {
                Debug.LogError("load " + path + " failed beacuse " + e.Message);
            }
        }

    }

}
