using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OfficeOpenXml;
using PCRCaculator.Calc;
using PCRCaculator.Guild;
using PCRCaculator.SQL;
using PCRCalcularor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CompressionLevel = System.IO.Compression.CompressionLevel;
using Object = UnityEngine.Object;

namespace PCRCaculator
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;
        public GameObject MassagePerferb;//弹出消息条底板
        public GameObject SystemWindowMessagePerferb;//二次确认底板
        public GameObject asyncPrefab;
        public GameObject SystemInputPrefab;//弹窗输入面板
        public GameObject LoadingPagePrefab;//加载面板
        public GameObject LoadingPagePrefab_2;//加载面板2
        public GameObject WaitingPrefab;//等待面板
        //public TextAsset db;
        //public TextAsset unitTimeTxt;
        //public TextAsset unitPrefabData;
        public enum StayPage { home = 0, character = 1, battle = 2, gamble = 3,calculator = 4 }
        public StayPage stayPage;
        //public int loadCharacterMax;//最多加载到的角色序号
        public int levelMax { get => playerSetting.playerLevel; }
        public bool UseNewBattleSystem = true;

        private Dictionary<string, Sprite> spriteCacheDic = new Dictionary<string, Sprite>();//图片缓存
        private Dictionary<int, EquipmentData> equipmentDic = new Dictionary<int, EquipmentData>();//装备类与装备id的对应字典
        public Dictionary<int, UnitData> unitDataDic = new Dictionary<int, UnitData>();//角色类可更改数据与角色id的对应字典(临时)
        public Dictionary<int, UnitData> unitDataDic_save = new Dictionary<int, UnitData>();//角色类可更改数据与角色id的对应字典(已保存)
        private Dictionary<int, UnitRarityData> unitRarityDic = new Dictionary<int, UnitRarityData>();//角色基础数据与角色id的对应字典 
        private Dictionary<int, UnitStoryData> unitStoryDic = new Dictionary<int, UnitStoryData>();//角色羁绊奖励列表
        private Dictionary<int, List<int>> unitStoryEffectDic = new Dictionary<int, List<int>>();//角色的马甲列表
        private Dictionary<int, SkillData> skillDataDic = new Dictionary<int, SkillData>();//所有的技能列表
        private Dictionary<int, SkillAction> skillActionDic = new Dictionary<int, SkillAction>();//所有小技能列表
        private Dictionary<int, string> unitName_cn = new Dictionary<int, string>();//角色中文名字
        private Dictionary<int, string[]> skillNameAndDescribe_cn = new Dictionary<int, string[]>();//技能中文名字和描述
        private Dictionary<int, string> skillActionDescribe_cn = new Dictionary<int, string>();//技能片段中文描述
        private Dictionary<int, UnitSkillTimeData> allUnitSkillTimeDataDic;//所有角色的技能时间数据
        private Dictionary<int, UnitAttackPattern> allUnitAttackPatternDic;//所有角色技能循环数据
        private Dictionary<int, UniqueEquipmentData> uniqueEquipmentDataDic = new Dictionary<int, UniqueEquipmentData>();//角色专武字典
        private AllUnitFirearmData firearmData = new AllUnitFirearmData();
        private Elements.MasterUnitSkillDataRf masterUnitSkillDataRf = new Elements.MasterUnitSkillDataRf();//未来可期
        private List<int> enemy_ignore_skill_rf = new List<int>();//未来可期

        private Dictionary<int, string> unitNickNameDic = new Dictionary<int, string>();

        private Dictionary<int, string> unitNickNameDic2 = new Dictionary<int, string>();
        private Dictionary<int, Guild.GuildEnemyData> guildEnemyDatas;
        private Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts> enemyMPartsDic;


        private CharacterManager characterManager;
        private AdventureManager battleManager;
        //private SQLiteHelper sql;

        private Coroutine windowmassageIE;
        private PlayerSetting playerSetting;

        private List<UnitData> playerDataForBattle;
        private List<UnitData> enemyDataForBattle;
        private bool isGuildBattle;
        private GuildBattleData guildBattleData;
        private bool isAutoMode;
        private bool forceAutoMode;
        private static byte[] Keys = { 0x20, 0x20, 0x78, 0x25, 0xCE, 0x37, 0x66, 0xFF };

        public Dictionary<int, EquipmentData> EquipmentDic { get => equipmentDic; }
        public Dictionary<int, UnitRarityData> UnitRarityDic { get => unitRarityDic; }
        public Dictionary<int, UnitStoryData> UnitStoryDic { get => unitStoryDic; }
        public Dictionary<int, List<int>> UnitStoryEffectDic { get => unitStoryEffectDic; }
        public Dictionary<int, SkillData> SkillDataDic { get => skillDataDic; }
        public Dictionary<int, SkillAction> SkillActionDic { get => skillActionDic; }
        public Dictionary<int, string> UnitName_cn { get => unitName_cn; }
        public Dictionary<int, string[]> SkillNameAndDescribe_cn { get => skillNameAndDescribe_cn; }
        public Dictionary<int, string> SkillActionDescribe_cn { get => skillActionDescribe_cn; }
        public PlayerSetting PlayerSetting { get => playerSetting; }
        public GameObject LatestUIback
        {
            get
            {
                if (BaseBackManager.Instance != null) { return BaseBackManager.Instance.latestUIback; }

                return null;
            }
        }

        public Text Debugtext { get => BaseBackManager.Instance.debugtext; }
        public TextMeshProUGUI PlayerLevelText { get => BaseBackManager.Instance.playerLevelText; }
        public CharacterManager CharacterManager { get => CharacterManager.Instance; set => characterManager = value; }
        public AdventureManager BattleManager { get => AdventureManager.Instance; set => battleManager = value; }
        public Dictionary<int, UnitSkillTimeData> AllUnitSkillTimeDataDic { get => allUnitSkillTimeDataDic;}
        public bool IsGuildBattle { get => isGuildBattle;}
        public List<UnitData> PlayerDataForBattle { get => playerDataForBattle; }
        public List<UnitData> EnemyDataForBattle { get => enemyDataForBattle; }
        public Dictionary<int, UnitAttackPattern> AllUnitAttackPatternDic { get => allUnitAttackPatternDic; }
        public bool IsAutoMode { get => isAutoMode;}
        public bool ForceAutoMode { get => forceAutoMode; }
        public GuildBattleData GuildBattleData { get => guildBattleData;}
        public Dictionary<int, UniqueEquipmentData> UniqueEquipmentDataDic { get => uniqueEquipmentDataDic;}
        //public float PlayerBodyWidth { get => playerSetting.bodyWidth; }
        public AllUnitFirearmData FirearmData { get => firearmData;}
        public List<int> Enemy_ignore_skill_rf { get => enemy_ignore_skill_rf;}
        public Elements.MasterUnitSkillDataRf MasterUnitSkillDataRf { get => masterUnitSkillDataRf;}

        public readonly List<int> showUnitIDs = new List<int>();
        public List<int> showSummonIDs;
        public AutoCalculatorData AutoCalculatorData = new AutoCalculatorData();
        public int MaxTPUpValue => playerSetting.maxTPUpValue;
        public int RBRank => playerSetting.RBRank_max;
        public int RBValue => playerSetting.RBValue1;
        public int RBTpValue => playerSetting.RBTpValue;
        public bool LoadFinished { get; private set; }
        public Dictionary<int, GuildEnemyData> GuildEnemyDatas { get => guildEnemyDatas;}
        public Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts> EnemyMPartsDic { get => enemyMPartsDic;}
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Application.targetFrameRate = 60;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            try
            {
#if PLATFORM_ANDROID
                var dir = Application.persistentDataPath + "/AB";
#else
                var dir = Application.streamingAssetsPath + "/../.ABExt";
#endif
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                StartCoroutine(Load());
                CreateShowUnitIDS();
            }
            catch (Exception e)
            {
                Debugtext.text += e.Message;
                Debug.LogError(e.Message);
            }

            //CharacterManager = CharacterManager.Instance;
            //BattleManager = AdventureManager.Instance;
        }

        public Dictionary<int, float[]> execTimePatch;

        private IEnumerator Load()
        {
            LoadFinished = false;
            var wait = OpenWaitUI();
            LoadPlayerSettings();
            //Thread thread = new Thread(() => LoadAsync());
            //await Task.Run(LoadAsync);
            yield return LoadAsync();
            Debugtext.text += "\n数据加载完毕！";
            CreateShowUnitIDS();
            wait.Close();
        }
        private IEnumerator LoadAsync()
        {
            execTimePatch = JsonConvert.DeserializeObject<Dictionary<int, float[]>>(LoadJsonDatas("Datas/ExecTimes"));
            //string jsonStr = db.text;
            //string jsonStr = Resources.Load<TextAsset>("Datas/AllData").text;
            /*string jsonStr = LoadJsonDatas("Datas/AllData");
            yield return null;
            if (jsonStr != "")
            {
                AllData allData = JsonConvert.DeserializeObject<AllData>(jsonStr);
                //equipmentDic = allData.equipmentDic;
                //unitRarityDic = allData.unitRarityDic;
                //unitStoryEffectDic = allData.unitStoryEffectDic;
                //unitStoryDic = allData.unitStoryDic;
                //equipmentDic.Add(999999, EquipmentData.EMPTYDATA);
                //skillDataDic = allData.skillDataDic;
                //skillActionDic = allData.skillActionDic;
                unitName_cn = allData.unitName_cn;
                skillNameAndDescribe_cn = allData.skillNameAndDescribe_cn;
                skillActionDescribe_cn = allData.skillActionDescribe_cn;
            }
            yield return null;*/

            SQLiteTool dbTool = SQLiteTool.OpenDB();

            equipmentDic = dbTool.GetEquipmentData();
            equipmentDic.Add(999999, EquipmentData.EMPTYDATA);
            yield return null;
            unitRarityDic = new Dictionary<int, UnitRarityData>();
            yield return dbTool.GetUnitRarityData(unitRarityDic);
            unitStoryDic = new Dictionary<int, UnitStoryData>();
            unitStoryEffectDic = new Dictionary<int, List<int>>();
            dbTool.GetUnitStoryData(unitStoryDic, unitStoryEffectDic);
            yield return null;

            skillDataDic = dbTool.GetSkillDataDic();
            yield return null;
            skillActionDic = dbTool.GetSkillActionDic();
            yield return null;

            allUnitAttackPatternDic = dbTool.GetUnitAttackPatternDic();
            yield return null;

            guildEnemyDatas = dbTool.GetGuildEnemyData();
            yield return null;

            enemyMPartsDic = dbTool.GetMPartsData();
            yield return null;

            //Guild.GuildManager.EnemyDataDic = dbTool.GetEnemyDataDic();
            Guild.GuildManager.EnemyDataDic = new Dictionary<int, EnemyData>();
            yield return dbTool.GetEnemyDataDic(Guild.GuildManager.EnemyDataDic);

            uniqueEquipmentDataDic = dbTool.GetUEQData();

            dbTool.CloseDB();

            dbTool = SQLiteTool.OpenDB(true);
            unitName_cn = dbTool.GetUnitName_cn();
            yield return null;
            skillNameAndDescribe_cn = dbTool.GetSkillName_cn();
            yield return null;
            skillActionDescribe_cn = dbTool.GetSkillActionDes_cn();
            yield return null;

#if true
            SQLData.ClearCache();
            Extensions.OverrideWith(equipmentDic, dbTool.GetEquipmentData());
            yield return null;
            var unitRarityDic2 = new Dictionary<int, UnitRarityData>();
            yield return dbTool.GetUnitRarityData(unitRarityDic2);
            Extensions.OverrideWith(unitRarityDic, unitRarityDic2);
            var unitStoryDic2 = new Dictionary<int, UnitStoryData>();
            var unitStoryEffectDic2 = new Dictionary<int, List<int>>();
            dbTool.GetUnitStoryData(unitStoryDic2, unitStoryEffectDic2);
            Extensions.OverrideWith(unitStoryDic, unitStoryDic2);
            Extensions.OverrideWith(unitStoryEffectDic, unitStoryEffectDic2);
            yield return null;
            
            Extensions.OverrideWith(skillDataDic, dbTool.GetSkillDataDic());
            yield return null;
            Extensions.OverrideWith(skillActionDic, dbTool.GetSkillActionDic());
            yield return null;
            Extensions.OverrideWith(allUnitAttackPatternDic, dbTool.GetUnitAttackPatternDic());
            yield return null;
            Extensions.OverrideWith(guildEnemyDatas, dbTool.GetGuildEnemyData());
            yield return null;
            Extensions.OverrideWith(enemyMPartsDic, dbTool.GetMPartsData());
            yield return null;

            //Guild.GuildManager.EnemyDataDic = dbTool.GetEnemyDataDic();
            var EnemyDataDic = new Dictionary<int, EnemyData>();
            yield return dbTool.GetEnemyDataDic(EnemyDataDic);
            Extensions.OverrideWith(Guild.GuildManager.EnemyDataDic, EnemyDataDic);
            yield return null;
            Extensions.OverrideWith(uniqueEquipmentDataDic, dbTool.GetUEQData());
#endif

            dbTool.CloseDB();

            /*string attackPatternStr = LoadJsonDatas("Datas/UnitAtttackPatternDic");
            allUnitAttackPatternDic = JsonConvert.DeserializeObject<Dictionary<int, UnitAttackPattern>>(attackPatternStr);

            string guildenemyDatasStr = MainManager.Instance.LoadJsonDatas("Datas/GuildEnemyDatas");
            if (!string.IsNullOrEmpty(guildenemyDatasStr))
            {
                gm.guildEnemyDatas = JsonConvert.DeserializeObject<Dictionary<int, Guild.GuildEnemyData>>(guildenemyDatasStr);
            }
            string enemyMParts = MainManager.Instance.LoadJsonDatas("Datas/EnemyMPartsDic");
            gm.EnemyMPartsDic = JsonConvert.DeserializeObject<Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts>>(enemyMParts);

            string enemyDataDicTxt = MainManager.Instance.LoadJsonDatas("Datas/EnemyDataDic");
            //Guild.GuildManager.EnemyDataDic = JsonConvert.DeserializeObject<Dictionary<int, EnemyData>>(enemyDataDicTxt);

            Debug.LogError($"读取DB失败！{ex.Message}");*/

            yield return null;
            LoadUnitData();
            //LoadPlayerSettings();
            //string prefabData = unitPrefabData.text;
            //AllUnitPrefabData allUnitPrefabData = JsonConvert.DeserializeObject<AllUnitPrefabData>(prefabData);
            //allUnitFirearmDatas = allUnitPrefabData.allUnitFirearmDatas;
            //Debugtext.text += "\n成功加载" + allUnitFirearmDatas.Count + "个技能特效数据！";
            //allUnitActionControllerDatas = allUnitPrefabData.allUnitActionControllerDatas;
            //Debugtext.text += "\n成功加载" + allUnitActionControllerDatas.Count + "个角色预制体数据！";
            yield return null;
            string skillTimeStr = LoadJsonDatas("Datas/unitSkillTimeDic");
            //string skillTimeStr = LoadJsonDatas("Datas/unitSkillTimeDic");
            allUnitSkillTimeDataDic = JsonConvert.DeserializeObject<Dictionary<int, UnitSkillTimeData>>(skillTimeStr);
            //Debugtext.text += "\n成功加载" + allUnitSkillTimeDataDic.Count + "个技能时间数据！";
            //string attackPatternStr = Resources.Load<TextAsset>("Datas/UnitAtttackPatternDic").text;
            //string attackPatternStr = LoadJsonDatas("Datas/UnitAtttackPatternDic");
            //allUnitAttackPatternDic = JsonConvert.DeserializeObject<Dictionary<int, UnitAttackPattern>>(attackPatternStr);
            //string uniqueStr = Resources.Load<TextAsset>("Datas/UniqueEquipmentDataDic").text;
            yield return null;
            //string uniqueStr = LoadJsonDatas("Datas/UniqueEquipmentDataDic");
            //uniqueEquipmentDataDic = JsonConvert.DeserializeObject<Dictionary<int, UniqueEquipmentData>>(uniqueStr);
            //yield return null;
            string nickNameDic = LoadJsonDatas("Datas/UnitNickNameDic");
            //string nickNameDic = LoadJsonDatas("Datas/UnitNickNameDic");
            unitNickNameDic = JsonConvert.DeserializeObject<Dictionary<int, string>>(nickNameDic);
            unitNickNameDic2 = JsonConvert.DeserializeObject<Dictionary<int, string>>(LoadJsonDatas("Datas/nickname"));
            string firearmStr = LoadJsonDatas("Datas/AllUnitFirearmData");
            yield return null;
            if (!string.IsNullOrEmpty(firearmStr))
                firearmData = JsonConvert.DeserializeObject<AllUnitFirearmData>(firearmStr);

            LoadFinished = true;
        }
        private void LoadUnitData()
        {
            try
            {
                unitDataDic = SaveManager.Load<Dictionary<int, UnitData>>();
                unitDataDic_save = SaveManager.Load<Dictionary<int, UnitData>>();
            }
            catch
            {
                unitDataDic = new Dictionary<int, UnitData>();
                unitDataDic_save = new Dictionary<int, UnitData>();
            }

            foreach (int id in UnitRarityDic.Keys)
            {
                if (!unitDataDic.ContainsKey(id))
                {
                    unitDataDic.Add(id, new UnitData(id, UnitRarityDic[id].detailData.minrarity));
                    unitDataDic_save.Add(id, new UnitData(id, UnitRarityDic[id].detailData.minrarity));
                }
            }
        }

        private void LoadPlayerSettings()
        {
            try
            {
                playerSetting = SaveManager.Load<PlayerSetting>();
                if (PlayerLevelText != null)
                {
                    PlayerLevelText.text = playerSetting.playerLevel + "";
                }
            }
            catch
            {
                playerSetting = new PlayerSetting();
                playerSetting.playerProcess = 12;
                playerSetting.playerLevel = 100;
                if (PlayerLevelText != null)
                {
                    PlayerLevelText.text = playerSetting.playerLevel + "";
                }
            }
        }

        /// <summary>
        /// 保存玩家设置到json
        /// </summary>
        public void SaveUnitData()
        {
            SaveManager.Save(unitDataDic);
        }
        /// <summary>
        /// 从json读取玩家设置
        /// </summary>
        public void ReLoad()
        {
            try
            {
                unitDataDic = SaveManager.Load<Dictionary<int, UnitData>>();
                unitDataDic_save = SaveManager.Load<Dictionary<int, UnitData>>();
            }
            catch
            {

            }
        }

        public void SavePlayerSetting()
        {
            SaveManager.Save(playerSetting);
        }
        public void DeletePlayerData()
        {
            WindowMessage("删除失败！（功能已删除）");
        }
        public void WindowMessage(string word)
        {
            if (windowmassageIE == null)
            {
                windowmassageIE = StartCoroutine(WindowMessage_start(word));
            }
        }
        public void WindowConfigMessage(string word, SystemWindowMessage.configDelegate configDelegate,SystemWindowMessage.configDelegate cancelDelegate = null)
        {
            GameObject a = Instantiate(SystemWindowMessagePerferb);
            a.transform.SetParent(LatestUIback.transform,false);
            a.transform.localPosition = new Vector3();
            a.transform.localScale = new Vector3(1, 1, 1);
            a.GetComponent<SystemWindowMessage>().SetWindowMassage(word, configDelegate,cancelDelegate);
        }
        public SystemWindowMessage WindowAsyncMessage(string word, SystemWindowMessage.configDelegate configDelegate = null, SystemWindowMessage.configDelegate cancelDelegate = null)
        {
            GameObject a = Instantiate(asyncPrefab);
            a.transform.SetParent(LatestUIback.transform, false);
            a.transform.localPosition = new Vector3();
            a.transform.localScale = new Vector3(1, 1, 1);
            var b = a.GetComponent<SystemWindowMessage>();
            b.SetWindowMassage(word, configDelegate, cancelDelegate);
            return b;
        }
        public void WindowInputMessage(string helpword,Action<string> finishAction,Action cancelAction = null)
        {
            GameObject a = Instantiate(SystemInputPrefab);
            a.transform.SetParent(LatestUIback.transform, false);
            a.transform.localPosition = new Vector3();
            a.transform.localScale = new Vector3(1, 1, 1);
            a.GetComponent<SystemWindowMessage>().SetWindowInputMassage(helpword,finishAction,cancelAction);
        }
        private IEnumerator WindowMessage_start(string word)
        {
            GameObject a = Instantiate(MassagePerferb);
            a.transform.SetParent(LatestUIback.transform);
            a.transform.localPosition = new Vector3();
            a.transform.localScale = new Vector3(1, 1, 1);
            a.GetComponentInChildren<Text>().text = word;
            yield return new WaitForSecondsRealtime(1.5f);
            Destroy(a);
            windowmassageIE = null;
        }
        public void HomeButton()
        {
            TurnAllPageOff();
            stayPage = StayPage.home;
        }

        public void CharacterButton()
        {
            TurnAllPageOff();
            CharacterManager.SwitchPage(1);
            stayPage = StayPage.character;
        }

        public void BattleButton()
        {
            TurnAllPageOff();
            BattleManager.SwitchPage(1);
            stayPage = StayPage.battle;
        }
        public void GambleButton()
        {
            WindowMessage("咕咕咕！");
        }
        public void CalculatorButton()
        {
            TurnAllPageOff();
            CalculatorManager.Instance.SwitchPage(1);
            stayPage = StayPage.calculator;
        }
        private void TurnAllPageOff()
        {
            CharacterManager.SwitchPage(0);
            BattleManager.SwitchPage(0);
        }
        public void GetBattleData(out List<UnitData> playerdata, out List<UnitData> enemydata)
        {
            playerdata = PlayerDataForBattle;
            enemydata = EnemyDataForBattle;
        }
        public void ChangeSceneToBalttle(List<int> my, AddedPlayerData other,bool isAutoMode = true,bool forceAutoMode = true)
        {
            List<UnitData> d1 = new List<UnitData>();
            foreach (int id in my)
            {
                d1.Add(unitDataDic[id]);
            }
            playerDataForBattle = d1;
            enemyDataForBattle = other.playrCharacters;
            isGuildBattle = false;
            this.isAutoMode = isAutoMode;
            this.forceAutoMode = forceAutoMode;
            bool isok = CheckIsAllCharacterAble(out string mess);
            if (isok)
            {
                StartCoroutine(LoadScene());
            }
            else
            {
                mess += "是否继续？";
                WindowConfigMessage(mess, Config_1);
            }
        }
        public void ChangeSceneToBalttle(GuildBattleData data)
        {
            playerDataForBattle = data.players.playrCharacters;
            isGuildBattle = true;
            isAutoMode = data.isAutoMode;
            forceAutoMode = data.forceAutoMode;
            guildBattleData = data;
            bool isok = CheckIsAllCharacterAble(out string mess);
            if (isok)
            {
                StartCoroutine(LoadScene());
            }
            else
            {
                mess += "是否继续？";
                WindowConfigMessage(mess, Config_1);
            }
        }
        public void Config_1()
        {
            StartCoroutine(LoadScene());
        }
        private bool CheckIsAllCharacterAble(out string message)
        {
            message = "";
            bool result = true;
            return result;
        }
        public bool JudgeWeatherShowThisUnit(int unitid)
        {
            return showUnitIDs.Contains(unitid);
        }
        public bool JudgeWeatherAllowUniqueEq(int unitid)
        {
            return UniqueEquipmentDataDic.ContainsKey(unitid);
        }
        public string GetUnitNickName(int unitid)
        {
            if (unitid >= 400000 && unitid <= 499999)
            {
                if (UnitRarityDic.TryGetValue(unitid, out var data))
                    return data.unitName;
                return "召唤物";
            }
            if (unitid >= 200000)
                return "敌方单位";
            if (unitNickNameDic.ContainsKey(unitid))
                return unitNickNameDic[unitid];
            if (unitNickNameDic2.ContainsKey(unitid))
                return unitNickNameDic2[unitid];
            if (unitDataDic.ContainsKey(unitid))
                return unitDataDic[unitid].GetUnitNameInternal();
            return "???";
        }
        private void CreateShowUnitIDS()
        {
            if (!PlayerSetting.showAllUnits)
            {
                for (int i = 100101; i <= 106301; i++)
                {
                    showUnitIDs.Add(i);
                }
                showUnitIDs.Remove(101901);
                showUnitIDs.Remove(102401);
                showUnitIDs.Remove(103501);
                showUnitIDs.Remove(103901);
                showUnitIDs.Remove(104101);
                showUnitIDs.Remove(106201);
                showUnitIDs.Add(107101);
                for (int i = 107501; i <= 109301; i++)
                {
                    showUnitIDs.Add(i);
                }
                showUnitIDs.Add(170101);
                showUnitIDs.Add(170201);
            }
            else
            {
                foreach (var unit in UnitRarityDic)
                {
                    if (unit.Key < 200000)
                    {
                        showUnitIDs.Add(unit.Key);
                    }
                }
                //showUnitIDs.Remove(170201);
            }
            showSummonIDs = new List<int> { 403101, 404201, 407001, 407701, 408401, 408402, 408403 };
            /*foreach (var unit in UnitRarityDic)
            {
                if (unit.Key>400000 && unit.Key < 500000)
                {
                    showSummonIDs.Add(unit.Key);
                }
            */


        }
        private void CopyDataInAndraid()
        {
            if (PlayerPrefs.HasKey("Copyed"))
            {
                return;
            }
            DirectoryCopy(Application.streamingAssetsPath, Application.persistentDataPath);
            PlayerPrefs.SetInt("Copyed", 1);
            PlayerPrefs.Save();
        }
        public void DirectoryCopy(string sourceDirectory, string targetDirectory)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirectory);
                //获取目录下（不包含子目录）的文件和子目录
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(targetDirectory + "\\" + i.Name))
                        {
                            //目标目录下不存在此文件夹即创建子文件夹
                            Directory.CreateDirectory(targetDirectory + "\\" + i.Name);
                        }
                        //递归调用复制子文件夹
                        DirectoryCopy(i.FullName, targetDirectory + "\\" + i.Name);
                    }
                    else
                    {
                        //不是文件夹即复制文件，true表示可以覆盖同名文件
                        File.Copy(i.FullName, targetDirectory + "\\" + i.Name, true);
                    }
                }
            }
            catch (Exception ex)
            {
                WindowConfigMessage("初始化存档文件时出现异常:" + ex.Message,null);
            }
        }
        IEnumerator LoadScene()
        {
            GameObject a = IsGuildBattle?Instantiate(LoadingPagePrefab_2): Instantiate(LoadingPagePrefab);
            a.transform.SetParent(LatestUIback.transform);
            a.transform.localScale = new Vector3(1, 1, 1);
            a.GetComponent<RectTransform>().offsetMax = new Vector2(5, 5);
            a.GetComponent<RectTransform>().offsetMin = new Vector2(-5, -5);

            yield return new WaitForSeconds(1.5f);
            var async = SceneManager.LoadSceneAsync("BattleScene");
            async.allowSceneActivation = true;
            while (!async.isDone)
            {
                yield return null;
            }
        }
        public void SaveAllUnitFriearmData()
        {
            string filePath = GetSaveDataPath() + "/Datas/AllUnitFirearmData.json";
            string saveJsonStr = JsonConvert.SerializeObject(firearmData);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
        }

        public string LoadJsonDatas(string path,bool forceStreaming = true)
        {
            string filePath = GetSaveDataPath() + "/" + path + ".json";
#if PLATFORM_ANDROID
            var reader = new WWW(filePath);
            while (!reader.isDone) {Thread.Sleep(0);}

            return reader.text;
#else
            return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
#endif
        }

        public WaitUI OpenWaitUI()
        {
            GameObject p = Instantiate(WaitingPrefab, GameObject.Find("Canvas")?.transform);
            return p.GetComponent<WaitUI>();
        }
        public static Sprite LoadSourceSprite(string relativePath)
        {
            if (Instance.spriteCacheDic.ContainsKey(relativePath))
            {
                return Instance.spriteCacheDic[relativePath];
            }

            Object Preb = Resources.Load(relativePath, typeof(Sprite));
            Sprite tmpsprite = null;
            if (Preb != null)
            {
                try
                {
                    tmpsprite = Instantiate(Preb) as Sprite;
                    Instance.spriteCacheDic.Add(relativePath, tmpsprite);
                }
                catch (Exception ex)
                {
                    Debug.Log(relativePath + "图片加载失败，原因：" + ex.Message);
                }

            }

            //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
            return tmpsprite;
        }
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串 </returns>
        public static string EncryptDES(string encryptString, string encryptKey = "PCRGuild")  
        {
            var ms = new MemoryStream();
            var stream = new GZipStream(ms, CompressionLevel.Optimal);
            var bytes = Encoding.UTF8.GetBytes(encryptString);
            stream.Write(bytes, 0, bytes.Length);
            stream.Dispose();
            var res = Convert.ToBase64String(ms.ToArray());
            ms.Dispose();
            return res;

        }
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey = "PCRGuild")
        {
            try
            {
                var stream = new GZipStream(new MemoryStream(Convert.FromBase64String(decryptString)), CompressionMode.Decompress);
                var ms = new MemoryStream();
                byte[] buf = new byte[1024];
                int len;
                while ((len = stream.Read(buf, 0, buf.Length)) > 0)
                    ms.Write(buf, 0, len);
                var res = Encoding.UTF8.GetString(ms.ToArray());
                ms.Dispose();
                stream.Dispose();
                return res;
            }
            catch
            {

            }
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception e)
            {
                return decryptString;
            }
        }
        public static string GetSaveDataPath()
        {
            return Application.streamingAssetsPath;
        }
        [ContextMenu("生成排刀器用unitdetail文件")]
        public void CreateUnitDetailDic()
        {
            Dictionary<int, UnitDetail_other> dic = new Dictionary<int, UnitDetail_other>();
            foreach(var unit in UnitRarityDic.Values)
            {
                dic.Add(unit.unitId, new UnitDetail_other(unit.unitId, unit.detailData.searchAreaWidth, GetUnitNickName(unit.unitId)));
            }
            string filePath = GetSaveDataPath() + "/Datas/UnitDetailDic.txt";
            string saveJsonStr = JsonConvert.SerializeObject(dic);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            Debug.Log("成功！");
        }
        /// <summary>
        ///闪避时加TP的比率
        /// </summary>
        /// <returns></returns>
        internal float GetDodgeTPRecoveryRatio()
        {
            return 0;
        }
    }
    public class UnitData_other
    {
        public string name = "未定义";
        public int type;
        public bool hasRarity6;

        public UnitData_other(string name, int type, bool hasRarity6)
        {
            this.name = name;
            this.type = type;
            this.hasRarity6 = hasRarity6;
        }
    }
    public class UnitDetail_other
    {
        public int unitid;
        public int pos;
        public string name;
        public UnitDetail_other()
        {

        }

        public UnitDetail_other(int unitid, int pos, string name)
        {
            this.unitid = unitid;
            this.pos = pos;
            this.name = name;
        }
    }
    public class AutoCalculatorData
    {
        public bool isCalculating;
        public bool isPaues;
        public bool isCanceled;
        public int execTime;
        public bool isConfig;
        public List<OnceResultData> resultDatas = new List<OnceResultData>();
        public int execedTime => resultDatas.Count;
        public bool isFinish => execTime <= resultDatas.Count || isCanceled;
        public bool isGoing => isCalculating && !isFinish&&!isCanceled;
        public long Average
        {
            get
            {
                long result = 0;
                foreach(var data in resultDatas)
                {
                    result += data.currentDamage;
                }
                if (resultDatas.Count > 0)
                    result /= resultDatas.Count;
                return result;
            }
        }
        public void Reset()
        {
            isCalculating = false;
            isPaues = false;
            isCanceled = false;
            execTime = 0;
            resultDatas.Clear();
            isConfig = false;
        }
    }
    public class OnceResultData
    {
        public int id;
        public int randomSeed;
        public long currentDamage;
        public long criticalEX;
        public long exceptDamage;
        public int backTime;

        public List<string> warnings = new List<string>();

        public string GetDetail()
        {
            string result = "伤害：" + currentDamage + "\n暴击额外伤害：" + criticalEX + "\n期望伤害（触盾偏高）：" + exceptDamage + "\n随机种子：" + randomSeed;
            if (backTime > 0) result += $"返\n{backTime}s";
            if (warnings.Count > 0)
            {
                for(int i=0;i<3;i++)
                {
                    if(i<warnings.Count)
                    result +="\n"+ warnings[i];
                }
                if (warnings.Count > 4)
                    result += "\n等" + warnings.Count + "个错误";
            }
            return result;
        }

    }
}