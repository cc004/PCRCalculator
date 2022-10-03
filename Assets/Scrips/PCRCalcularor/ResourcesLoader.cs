using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elements;
using PCRCaculator.Guild;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class ResourcesLoader : MonoBehaviour
    {
        public static ResourcesLoader Instance;
        public InputField inputField_id;
        public InputField input_DMMPath;
        public Toggle useDMMToggle;

        public List<GameObject> unitEffects = new List<GameObject>();
        public GameObject unitprefab;
        public GuildExecTimeSetting GuildExecTimeSetting;



        private bool allDependsLoaded;
        private Dictionary<int, UnitActionController> loadedUnits = new Dictionary<int, UnitActionController>();
        private bool isCreating;
        private bool isInit;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            isInit = true;
            useDMMToggle.isOn = MainManager.Instance.PlayerSetting.useDMMpath;
            if (useDMMToggle.isOn)
                input_DMMPath.text = MainManager.Instance.PlayerSetting.dmmPath;
            isInit = false;
        }
        /*
        public async void UpdateSQLData_async()
        {
            var a = MainManager.Instance.WindowAsyncMessage("正在导入数据，请耐心等待...");
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                await Task.Run(() => SQL2Json.CreateAllSQLDataInPlayerMode_jp());
            }
            else
            {
                try
                {
                    await Task.Run(() => SQL2Json.CreateAllSQLDataInPlayerMode_jp());
                }
                catch (Exception e)
                {
                    a.Close();
                    PlayerPrefs.SetInt("UsePlayerSQL", 0);
                    MainManager.Instance.WindowConfigMessage("导入SQL时发生错误：" + e.Message, null);
                    return;
                }
            }
            a.Close();
            PlayerPrefs.SetInt("UsePlayerSQL", 1);
            MainManager.Instance.WindowConfigMessage("成功！", null);
        }
        public async void UpdateSQLData_async_cn()
        {
            var a = MainManager.Instance.WindowAsyncMessage("正在导入数据，请耐心等待...（不要点按钮！！！）");
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                await Task.Run(() => SQL2Json.CreateAllSQLDataInPlayerMode_cn());
            }
            else
            {
                try
                {
                    await Task.Run(() => SQL2Json.CreateAllSQLDataInPlayerMode_cn());
                }
                catch (Exception e)
                {
                    a.Close();
                    PlayerPrefs.SetInt("UsePlayerSQL", 0);
                    MainManager.Instance.WindowConfigMessage("导入SQL时发生错误：" + e.Message, null);
                    return;
                }
            }
            a.Close();
            PlayerPrefs.SetInt("UsePlayerSQL", 1);
            MainManager.Instance.WindowConfigMessage("成功！", null);
        }
        */
        public void DeleteSQLData()
        {
            PlayerPrefs.SetInt("UsePlayerSQL", 0);
            MainManager.Instance.WindowConfigMessage("删除成功！", null);
        }

        public void CreateUnitFirearmData()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                int unitid = int.Parse(inputField_id.text);
                if (unitid >= 100101 && unitid <= 999999)
                {
                    LoadAllFires();
                    //LoadUnitPrefab(unitid);
                    SaveFirearmData(unitid);
                }
                else
                {
                    MainManager.Instance.WindowConfigMessage("unitid错误！", null);
                }

            }
            else
                try
                {
                    int unitid = int.Parse(inputField_id.text);
                    if (unitid >= 100101 && unitid <= 999999)
                    {
                        LoadAllFires();
                        //LoadUnitPrefab(unitid);
                        SaveFirearmData(unitid);
                    }
                    else
                    {
                        MainManager.Instance.WindowConfigMessage("unitid错误！", null);
                    }
                }
                catch (Exception e)
                {
                    MainManager.Instance.WindowConfigMessage("发生错误：" + e.Message, null);
                }
        }
        public void ChangeUnitFirearmData()
        {
            try
            {
                int unitid = int.Parse(inputField_id.text);
                if (unitid >= 100101 && unitid <= 999999)
                {
                    LoadAllFires();
                    var a = LoadUnitPrefab(unitid);
                    GuildExecTimeSetting.ActionController = a;
                    if (unitid >= 199999)
                    {
                        if (GuildManager.EnemyDataDic.ContainsKey(unitid))
                        {
                            EnemyData enemyData = GuildManager.EnemyDataDic[unitid];
                            GuildExecTimeSetting.Init(enemyData);
                        }
                        else
                            MainManager.Instance.WindowConfigMessage("unitid错误！", null);
                    }
                    else
                        GuildExecTimeSetting.Init(unitid);
                }
                else
                {
                    MainManager.Instance.WindowConfigMessage("unitid错误！", null);
                }
            }
            catch (Exception e)
            {
                MainManager.Instance.WindowConfigMessage("发生错误：" + e.Message, null);
            }
        }
        public void CreateAllFirearmData()
        {
            if (isCreating)
                return;
            StartCoroutine(CreateAllFirearmData_0());
        }
        private IEnumerator CreateAllFirearmData_0()
        {
            isCreating = true;
            LoadAllFires();
            foreach(int unitid in MainManager.Instance.showUnitIDs)
            {
                try
                {
                    SaveFirearmData(unitid, false, false);
                }
                catch (Exception e)
                {
                    MainManager.Instance.WindowConfigMessage("生成" + unitid + "的弹道数据时发生错误：" + e.Message, null);
                }
                yield return null;
            }
            MainManager.Instance.SaveAllUnitFriearmData();
            MainManager.Instance.WindowConfigMessage("成功生成所有角色的弹道数据！", null);
            isCreating = false;
        }
        private void LoadAllFires()
        {
            if (allDependsLoaded)
                return;
            string folderFullName = Application.streamingAssetsPath + "/AB";
            List<string> vs = new List<string>();
            DirectoryInfo TheFolder = new DirectoryInfo(folderFullName);
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                string name = NextFile.Name;
                if (name.Contains("all_fxsk_")&&!name.Contains(".meta"))
                    vs.Add(NextFile.Name);
            }
            for (int i = 0; i < vs.Count; i++)
            {
                GameObject a = ABExTool.GetAssetBundleByName<GameObject>(vs[i], "prefab");
                var b= Instantiate(a, unitprefab.transform);
                b.SetActive(false);
                unitEffects.Add(b);
            }
            allDependsLoaded = true;
        }
        private UnitActionController LoadUnitPrefab(int unitid)
        {
            if (loadedUnits.TryGetValue(unitid, out var result))
            {
                return result;
            }

            var b = ABExTool.GetAllAssetBundleByName<GameObject>("all_battleunit_" + unitid + ".unity3d", "prefab");
            foreach (var i in b)
            {
                if (i != null)
                    Instantiate(i).SetActive(false);
            }
            GameObject a = ABExTool.GetAssetBundleByName<GameObject>("all_battleunitprefab_" + unitid + ".unity3d", "prefab");
            var c = Instantiate(a);
            var d = c.GetComponent<UnitActionController>();
            loadedUnits.Add(unitid, d);
            return d;
        }
        private void SaveFirearmData(int unitid, bool saveImmedately = true, bool showResult = true)
        {
            Dictionary<string, List<FirearmCtrlData>> result = null;
            var unitactioncontroller = LoadUnitPrefab(unitid);
            if (unitid >= 200000)
            {
                int otherID = Mathf.FloorToInt(unitid / 100) * 100 + 99;
                var otherPrefab = ABExTool.GetAllAssetBundleByName<GameObject>("all_battleunit_" + otherID + ".unity3d", "prefab");
            }

            result = unitactioncontroller.GetFirearmDatas();
            var unitSkilldata = unitactioncontroller.CreateUnitSkillEffectData();
            unitSkilldata.unitid = unitid;
            MainManager.Instance.FirearmData.SetData(unitid, result,unitSkilldata);
            if(saveImmedately)
            MainManager.Instance.SaveAllUnitFriearmData();
            if(showResult)
            MainManager.Instance.WindowConfigMessage("成功生成" + unitid + "的弹道数据！", null);
        }
        public void ExitButton()
        {
            SceneManager.LoadScene("GuildScene");
        }
        public void UpDateABFromDMM()
        {
            MainManager.Instance.WindowInputMessage("请输入unitid", CopyFromDmm);
        }
        private void CopyFromDmm(string unitidstr)
        {
            try
            {
                int unitid = int.Parse(unitidstr);
                ABExTool.DMMpath = MainManager.Instance.PlayerSetting.dmmPath + "/";
                ABExTool.OUTpath = Application.streamingAssetsPath + "/AB/";
                if (unitid <= 199999)
                    ABExTool.CopyByID(unitid, 0,MainManager.Instance.WindowMessage);
                else
                    ABExTool.CopyByID(unitid, 1, MainManager.Instance.WindowMessage);
            }
            catch(Exception e)
            {
                MainManager.Instance.WindowConfigMessage("发生错误：" + e.Message, null);
            }
        }
        
        public void OnDmmtoggleChoosed()
        {
            if (isInit)
                return;
            string path = input_DMMPath.text;
            if (Directory.Exists(path))
            {
                MainManager.Instance.PlayerSetting.useDMMpath = useDMMToggle.isOn;
                if (useDMMToggle.isOn)
                    MainManager.Instance.PlayerSetting.dmmPath = input_DMMPath.text;
                MainManager.Instance.SavePlayerSetting();
                MainManager.Instance.WindowMessage("设置成功！");
            }
            else
            {
                useDMMToggle.isOn = false;
                MainManager.Instance.WindowMessage("路径无效！");
            }
        }
    }
    public class AllUnitFirearmData
    {
        public Dictionary<int, Dictionary<string, List<FirearmCtrlData>>> AllUnitFirearmDic = new Dictionary<int, Dictionary<string, List<FirearmCtrlData>>>();
        public Dictionary<int, Dictionary<string, List<FirearmCtrlData>>> AllUnitFirearmDic_player = new Dictionary<int, Dictionary<string, List<FirearmCtrlData>>>();
        public Dictionary<int, UnitSkillEffectData> AllUnitSkillEffectDic = new Dictionary<int, UnitSkillEffectData>();

        private bool isInited;
        public void Init()
        {
            if (isInited)
                return;
            if (AllUnitFirearmDic == null)
                AllUnitFirearmDic = new Dictionary<int, Dictionary<string, List<FirearmCtrlData>>>();
            if (AllUnitFirearmDic_player == null)
                AllUnitFirearmDic_player = new Dictionary<int, Dictionary<string, List<FirearmCtrlData>>>();
            if (AllUnitSkillEffectDic == null)
                AllUnitSkillEffectDic = new Dictionary<int, UnitSkillEffectData>();
            isInited = true;
        }
        public Dictionary<string, List<FirearmCtrlData>> GetData(int unitid,bool forcePlayer=false)
        {
            Init();
            if (AllUnitFirearmDic_player.TryGetValue(unitid, out var data))
                return data;
            if (forcePlayer)
                return new Dictionary<string, List<FirearmCtrlData>>();
            if (AllUnitFirearmDic.TryGetValue(unitid, out var value))
                return value;
            return new Dictionary<string, List<FirearmCtrlData>>();
        }
        public void SetData(int unitid, Dictionary<string, List<FirearmCtrlData>> value,UnitSkillEffectData effectData,bool toPlayer = false)
        {
            Init();
            if (toPlayer)
            {
                if (AllUnitFirearmDic_player.ContainsKey(unitid))
                    AllUnitFirearmDic_player[unitid] = value;
                else
                    AllUnitFirearmDic_player.Add(unitid, value);
            }
            else
            {
                if (AllUnitFirearmDic.ContainsKey(unitid))
                    AllUnitFirearmDic[unitid] = value;
                else
                    AllUnitFirearmDic.Add(unitid, value);
                if (AllUnitSkillEffectDic.ContainsKey(unitid))
                    AllUnitSkillEffectDic[unitid] = effectData;
                else
                    AllUnitSkillEffectDic.Add(unitid, effectData);
            }
        }
        public bool ResetData(int unitid)
        {
            if (AllUnitFirearmDic_player.ContainsKey(unitid))
            {
                AllUnitFirearmDic_player.Remove(unitid);
            }
            else
                return false;
            return true;
        }
        public bool GetAddLoadPrefabNames(int unitid,List<string> results)
        {
            if(AllUnitSkillEffectDic.TryGetValue(unitid,out var data))
            {
                foreach (string name in data.requireFsxkList)
                    if (!results.Contains(name))
                        results.Add(name);
                return true;
            }
            return false;
        }
    }
    public class UnitSkillEffectData
    {
        public int unitid;
        public List<string> requireFsxkList = new List<string>();
    }
}