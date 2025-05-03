using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OfficeOpenXml;
using PCRApi.CN;
using PCRCaculator.Calc;
using PCRCaculator.Guild;
using PCRCaculator.SQL;
using PCRCalcularor;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CompressionLevel = System.IO.Compression.CompressionLevel;
using Object = UnityEngine.Object;
using PCRCaculator.Update;
using UnityEngine.Networking;

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
        public enum StayPage { home = 0, character = 1, battle = 2, gamble = 3, calculator = 4 }
        public StayPage stayPage;
        //public int loadCharacterMax;//最多加载到的角色序号
        public int levelMax { get => playerSetting.playerLevel; }
        public bool UseNewBattleSystem = true;
        public bool useJapanData;
        public bool useVerification;
        public LoginTool loginTool;

        private Dictionary<string, Sprite> spriteCacheDic = new Dictionary<string, Sprite>();//图片缓存
        private Dictionary<int, EquipmentData> equipmentDic = new Dictionary<int, EquipmentData>();//装备类与装备id的对应字典
        public Dictionary<int, UnitData> unitDataDic = new Dictionary<int, UnitData>();//角色类可更改数据与角色id的对应字典(临时)
        public Dictionary<int, UnitData> unitDataDic_save = new Dictionary<int, UnitData>();//角色类可更改数据与角色id的对应字典(已保存)
        private Dictionary<int, UnitRarityData> unitRarityDic = new Dictionary<int, UnitRarityData>();//角色基础数据与角色id的对应字典 
        private Dictionary<int, UnitStoryData> unitStoryDic = new Dictionary<int, UnitStoryData>();//角色羁绊奖励列表
        private Dictionary<int, List<int>> unitStoryEffectDic = new Dictionary<int, List<int>>();//角色的马甲列表
        private Dictionary<int, List<int>> unitStoryEffectDic2 = new Dictionary<int, List<int>>();//角色的马甲列表
        private Dictionary<int, SkillData> skillDataDic = new Dictionary<int, SkillData>();//所有的技能列表
        private Dictionary<int, SkillAction> skillActionDic = new Dictionary<int, SkillAction>();//所有小技能列表
        private Dictionary<int, string> unitName_cn = new Dictionary<int, string>();//角色中文名字
        private Dictionary<int, string[]> skillNameAndDescribe_cn = new Dictionary<int, string[]>();//技能中文名字和描述
        private Dictionary<int, string> skillActionDescribe_cn = new Dictionary<int, string>();//技能片段中文描述
        private Dictionary<int, UnitSkillTimeData> allUnitSkillTimeDataDic;//所有角色的技能时间数据
        private Dictionary<int, UnitAttackPattern> allUnitAttackPatternDic;//所有角色技能循环数据
        public Dictionary<int, unique_equip_enhance_rate[]> UniqueEquipmentDataDic = new Dictionary<int, unique_equip_enhance_rate[]>();//角色专武字典
        public Dictionary<int, unique_equip_enhance_rate[]> UniqueEquipment2DataDic = new Dictionary<int, unique_equip_enhance_rate[]>();//角色专武2字典
        public List<EReduction> ereductionTable = new List<EReduction>();
        private AllUnitFirearmData firearmData = new AllUnitFirearmData();
        private Elements.MasterUnitSkillDataRf masterUnitSkillDataRf;//未来可期
        private List<int> enemy_ignore_skill_rf = new List<int>();//未来可期

        private Dictionary<int, string> unitNickNameDic = new Dictionary<int, string>();

        private Dictionary<int, string> unitNickNameDic2 = new Dictionary<int, string>();
        private Dictionary<int, Guild.GuildEnemyData> guildEnemyDatas;
        private Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts> enemyMPartsDic;
        public Dictionary<int, Dictionary<int, ex_equipment_data>[]> unitExEquips;
        private CharacterManager characterManager;
        private AdventureManager battleManager;
        //private SQLiteHelper sql;

        private Coroutine windowmassageIE;
        private PlayerSetting playerSetting;

        private List<UnitData> playerDataForBattle;
        private List<UnitData> enemyDataForBattle;
        private bool isGuildBattle;
        public GuildBattleData guildBattleData;
        public bool isAutoMode;
        public bool forceAutoMode;
        public bool isSetMode;
        public bool isSemanMode;
        private static byte[] Keys = { 0x20, 0x20, 0x78, 0x25, 0xCE, 0x37, 0x66, 0xFF };

        public Dictionary<int, EquipmentData> EquipmentDic { get => equipmentDic; }
        public Dictionary<int, UnitRarityData> UnitRarityDic { get => unitRarityDic; }
        public Dictionary<int, UnitStoryData> UnitStoryDic { get => unitStoryDic; }
        public Dictionary<int, List<int>> UnitStoryEffectDic { get => unitStoryEffectDic; }
        public Dictionary<int, List<int>> UnitStoryEffectDic2 { get => unitStoryEffectDic2; }
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
        public Dictionary<int, UnitSkillTimeData> AllUnitSkillTimeDataDic { get => allUnitSkillTimeDataDic; }
        public bool IsGuildBattle { get => isGuildBattle; }
        public List<UnitData> PlayerDataForBattle { get => playerDataForBattle; }
        public List<UnitData> EnemyDataForBattle { get => enemyDataForBattle; }
        public Dictionary<int, UnitAttackPattern> AllUnitAttackPatternDic { get => allUnitAttackPatternDic; }
        // public bool IsAutoMode { get => isAutoMode;}
        // public bool ForceAutoMode { get => forceAutoMode; }
        public GuildBattleData GuildBattleData { get => guildBattleData; }
        //public Dictionary<int, UniqueEquipmentData> UniqueEquipmentDataDic { get => uniqueEquipmentDataDic;}
        //public float PlayerBodyWidth { get => playerSetting.bodyWidth; }
        public AllUnitFirearmData FirearmData { get => firearmData; }
        public List<int> Enemy_ignore_skill_rf { get => enemy_ignore_skill_rf; }
        public Elements.MasterUnitSkillDataRf MasterUnitSkillDataRf { get => masterUnitSkillDataRf; }

        public readonly List<int> showUnitIDs = new List<int>();
        public List<int> showSummonIDs;
        public AutoCalculatorData AutoCalculatorData = new AutoCalculatorData();
        public int MaxTPUpValue => playerSetting.maxTPUpValue;

        public Dictionary<(int, int), promotion_bonus> rbs;

        public bool LoadFinished { get; private set; }
        public Dictionary<int, GuildEnemyData> GuildEnemyDatas { get => guildEnemyDatas; }
        public Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts> EnemyMPartsDic { get => enemyMPartsDic; }
        private const string Url = "https://wthee.xyz/pcr/api/v1/db/info/v2";
        public string truthVersion;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                LoadFinished = false;
                DontDestroyOnLoad(gameObject);
                Application.targetFrameRate = 60;

                try
                {
                    Version = SaveManager.Load<VersionData>();
                }
                catch
                {
                    Version = new VersionData();
                    SaveManager.Save(Version);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public class VersionData
        {
            public long CharacterVersionJP = 10051600;
            public long BossVersionJP = Instance.useJapanData ? 10051600 : 10047900;
            public long BossVersionCN = 0;
            public bool useQA = false;
            public bool useJP = true;
            public bool newAB = true;
            public bool useLatestProd = true;

        }

        public VersionData Version;
        public Dictionary<int, int> unitStoryLoveDic;

        private void Start()
        {
            LoadFinished = false;
            try
            {
                /*
#if PLATFORM_ANDROID
                var callback = new PermissionCallbacks();
                callback.PermissionGranted += msg => Debug.Log($"permission granted: {msg}");
                callback.PermissionDenied += msg => Debug.Log($"permission denied: {msg}");
                Permission.RequestUserPermission("android.permission.READ_EXTERNAL_STORAGE",
                    callback);
                Permission.RequestUserPermission("android.permission.WRITE_EXTERNAL_STORAGE",
                    callback);
                var fs = new FileStream("/storage/emulated/0/Download/D4-蝶妈似似花水魅魔圣千真步-2170w.xlsx"
                    , FileMode.Open, FileAccess.Read);
#endif*/

                ABExTool.persistentDataPath = Application.persistentDataPath;
                ABExTool.dataPath = Application.dataPath;

                if (useVerification)
                {
                    loginTool?.gameObject.SetActive(true);
                }
#if PLATFORM_ANDROID
                var dir = Application.persistentDataPath + "/AB";
#else
                var dir = ABExTool.persistentDataPath + "/.ABExt2";
#endif
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                Load();
            }
            catch (Exception e)
            {
                Debugtext.text += e.Message;
                Debug.LogError(e.ToString());
            }

            //CharacterManager = CharacterManager.Instance;
            //BattleManager = AdventureManager.Instance;
        }

        private void Load()
        {
            Application.runInBackground = true;
            LoadFinished = false;
            var wait = OpenWaitUI();
            LoadPlayerSettings();
            //Thread thread = new Thread(() => LoadAsync());
            //await Task.Run(LoadAsync);
            LoadAsync(wait);
        }

        private async void LoadAsync(WaitUI wait)
        {
            //           execTimePatch = JsonConvert.DeserializeObject<Dictionary<int, float[]>>(LoadJsonDatas("Datas/ExecTimes"));
            //string jsonStr = db.text;
            //string jsonStr = Resources.Load<TextAsset>("Datas/AllData").text;
            /*string jsonStr = LoadJsonDatas("Datas/AllData");
            
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
            */
            if (Version.BossVersionCN == 0 || Version.useLatestProd == true)
            {
                await SendPostRequestAsync(() =>
                {
                    Version.useQA = false;
                    Version.BossVersionCN = long.Parse(truthVersion);
                    SaveManager.Save(Version);
                });
            }
#if UNITY_ANDROID
            var loadsql =
                new WWW("jar:file://" + ABExTool.dataPath + "!/assets/" + "dbdiff.sql");  // this is the path to your StreamingAssets in android
            while (!loadsql.isDone) { }
            var sql = Encoding.UTF8.GetString(loadsql.bytes);
#else
            var patchFile = Path.Combine(Application.streamingAssetsPath, "dbdiff.sql");
            var sql = File.ReadAllText(patchFile);
#endif
            SQLiteTool.sql = sql;
            try
            {
                ABExTool.StaticInitialize().Wait();
            }
            catch (Exception e)
            {
                WindowMessage("下载数据发生错误，重置版本");
                SaveManager.Save(new VersionData());
                return;
            }

            var tasks = new List<Task>();


            var dbTool = SQLData.OpenDB(ABExTool.mgrCharacter);
            var dbTool3 = SQLData.OpenDB(ABExTool.mgrDataCN);
            var dbTool2 = SQLData.OpenDB(ABExTool.mgrDataJP);

            Task.WhenAll(
                Task.Run(() => dbTool.Prepare()),
                Task.Run(() =>
                {
                    if (ABExTool.mgrDataJP != ABExTool.mgrCharacter) dbTool2.Prepare();
                }),
                Task.Run(() => dbTool3.Prepare())).Wait();

            tasks.Add(
                Task.WhenAll(
                        dbTool.ParallelGetAll(),
                        ABExTool.mgrDataJP != ABExTool.mgrCharacter ? dbTool2.ParallelGetAll() : Task.CompletedTask,
                        dbTool3.ParallelGetAll())
                    .ContinueWith(_ =>
                    {
                        dbTool.CloseDB();
                        if (ABExTool.mgrDataJP != ABExTool.mgrCharacter)
                            dbTool2.CloseDB();
                        else
                            dbTool2 = dbTool;
                        dbTool3.CloseDB();

                        var dbCN = dbTool3;

                        if (!Version.useJP) dbTool2 = dbTool3;

                        equipmentDic = dbTool.Dic8;
                        equipmentDic.Add(999999, EquipmentData.EMPTYDATA);
                        unitRarityDic = dbTool.Dic1;
                        (unitStoryDic, unitStoryEffectDic, unitStoryLoveDic) = dbTool.Pair;
                        skillDataDic = dbTool.Dic3;
                        masterUnitSkillDataRf = dbTool2.masterUnitSkillDataRf;
                        skillActionDic = dbTool.Dic4;
                        allUnitAttackPatternDic = dbTool.Dic5;
                        guildEnemyDatas = dbTool.Dic6;
                        enemyMPartsDic = dbTool.Dic7;
                        unitExEquips = dbTool3.unitExEquips;
                        //Guild.GuildManager.EnemyDataDic = dbTool.GetEnemyDataDic();
                        Guild.GuildManager.EnemyDataDic = dbTool.Dic2;
                        UniqueEquipmentDataDic = dbTool.Dic9;
                        UniqueEquipment2DataDic = dbTool.Dic92;

                        unitStoryEffectDic2 = dbTool2.Pair.Item2;
                        unitName_cn = dbTool3.Dic10;
                        skillNameAndDescribe_cn = dbTool3.Dic11;
                        skillActionDescribe_cn = dbTool3.Dic12;
                        /*
                        {
                            if (skillNameAndDescribe_cn.TryGetValue(1703001, out var val))
                                skillNameAndDescribe_cn[1701001] = val;
                            if (skillNameAndDescribe_cn.TryGetValue(1703002, out val))
                                skillNameAndDescribe_cn[1701002] = val;
                            if (skillNameAndDescribe_cn.TryGetValue(1703011, out val))
                                skillNameAndDescribe_cn[1701011] = val;
                            if (skillNameAndDescribe_cn.TryGetValue(1703012, out val))
                                skillNameAndDescribe_cn[1701012] = val;
                            if (skillNameAndDescribe_cn.TryGetValue(1703003, out val))
                                skillNameAndDescribe_cn[1701003] = val;
                        }*/

                        ereductionTable = (useJapanData ? dbTool : dbTool2).eReductions;
                        rbs = (useJapanData ? dbTool : dbTool2).rbs;

                        if (!useJapanData)
                        {
                            if (dbTool != dbTool2)
                                Task.WaitAll(
                                    //              Task.Run(() => Extensions.OverrideWith(equipmentDic, dbTool2.Dic8)),
                                    //              Task.Run(() => Extensions.OverrideWith(unitRarityDic, dbTool2.Dic1)),
                                    //              Task.Run(() => Extensions.OverrideWith(unitStoryDic, unitStoryDic2)),
                                    //              Task.Run(() => Extensions.OverrideWith(unitStoryEffectDic, unitStoryEffectDic2)),
                                    Task.Run(() => skillDataDic.OverrideWith(dbTool2.Dic3,
                                        k => k > 2000000 && k < 4000000)),
                                    Task.Run(() => UniqueEquipmentDataDic.OverrideWith(dbTool2.Dic9)),
                                    Task.Run(() => UniqueEquipment2DataDic.OverrideWith(dbTool2.Dic92)),
                                    Task.Run(() => skillActionDic.OverrideWith(dbTool2.Dic4,
                                        k => k > 200000000 && k < 400000000)),
                                    Task.Run(() => allUnitAttackPatternDic.OverrideWith(dbTool2.Dic5)),
                                    Task.Run(() => guildEnemyDatas.OverrideWith(dbTool2.Dic6)),
                                    Task.Run(() => enemyMPartsDic.OverrideWith(dbTool2.Dic7)),
                                    Task.Run(() => GuildManager.EnemyDataDic.OverrideWith(dbTool2.Dic2))
                                // Task.Run(() => uniqueEquipmentDataDic.OverrideWith(dbTool2.Dic9))
                                );

                            var filteredUnitRarity = dbTool3.Dic1.Where(p => p.Key / 1000 == 17 || p.Key == 105701).ToList();
                            var filteredUniqueEquipment = dbTool3.Dic9.Where(p => p.Key / 10000 == 17).ToList();
                            var filteredUniqueEquipment2 = dbTool3.Dic92.Where(p => p.Key / 10000 == 17).ToList();
                            var filteredSkillData = dbTool3.Dic3.Where(p => p.Key / 100000 == 17).ToList();
                            var filteredSkillAction = dbTool3.Dic4.Where(p => p.Key / 10000000 == 17).ToList();
                            var filteredMasterUnitSkill = dbTool3.masterUnitSkillDataRf.dict.Where(p => p.Key / 100000 == 17).ToList();
                            var filteredAllUnitAttackPattern = dbTool3.Dic5.Where(p => p.Key / 1000000 == 17).ToList();
                            Task.WaitAll(
                                Task.Run(() => unitRarityDic.OverrideWith(filteredUnitRarity)),
                                Task.Run(() => UniqueEquipmentDataDic.OverrideWith(filteredUniqueEquipment)),
                                Task.Run(() => UniqueEquipment2DataDic.OverrideWith(filteredUniqueEquipment2)),
                                Task.Run(() => skillDataDic.OverrideWith(filteredSkillData)),
                                Task.Run(() => skillActionDic.OverrideWith(filteredSkillAction)),
                                Task.Run(() =>
                                {
                                    masterUnitSkillDataRf.dict.Remove(1701003); // old jp data
                                    masterUnitSkillDataRf.dict.OverrideWith(filteredMasterUnitSkill);
                                }),
                                Task.Run(() => allUnitAttackPatternDic.OverrideWith(filteredAllUnitAttackPattern))
                            );

                            Task.WaitAll(
                                Task.Run(() => skillDataDic.Override2With(dbTool3.Dic3)),
                                Task.Run(() => equipmentDic.Override2With(dbTool3.Dic8)),
                                Task.Run(() => skillActionDic.Override2With(dbTool3.Dic4)),
                                Task.Run(() => unitRarityDic.Override2With(dbTool3.Dic1)),
                                Task.Run(() => GuildManager.EnemyDataDic.Override2With(dbTool3.Dic2))
                            );

                            unitRarityDic.TryAdd(dbCN.Dic1);
                            unitStoryDic.TryAdd(dbCN.Pair.Item1);
                            unitStoryEffectDic.TryAdd(dbCN.Pair.Item2);
                            unitStoryLoveDic.TryAdd(dbCN.Pair.Item3);
                            skillDataDic.TryAdd(dbCN.Dic3);
                            skillActionDic.TryAdd(dbCN.Dic4);
                            allUnitAttackPatternDic.TryAdd(dbCN.Dic5);

                            unitRarityDic[170101].ChangeRankData(unitRarityDic[105701].GetRankData());
                            unitRarityDic[170201].ChangeRankData(unitRarityDic[107601].GetRankData());
                        }
                    }));

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


            //LoadPlayerSettings();
            //string prefabData = unitPrefabData.text;
            //AllUnitPrefabData allUnitPrefabData = JsonConvert.DeserializeObject<AllUnitPrefabData>(prefabData);
            //allUnitFirearmDatas = allUnitPrefabData.allUnitFirearmDatas;
            //Debugtext.text += "\n成功加载" + allUnitFirearmDatas.Count + "个技能特效数据！";
            //allUnitActionControllerDatas = allUnitPrefabData.allUnitActionControllerDatas;
            //Debugtext.text += "\n成功加载" + allUnitActionControllerDatas.Count + "个角色预制体数据！";

            string skillTimeStr = LoadJsonDatas("Datas/unitSkillTimeDic");
            //string skillTimeStr = LoadJsonDatas("Datas/unitSkillTimeDic");
            allUnitSkillTimeDataDic = JsonConvert.DeserializeObject<Dictionary<int, UnitSkillTimeData>>(skillTimeStr);
            //Debugtext.text += "\n成功加载" + allUnitSkillTimeDataDic.Count + "个技能时间数据！";
            //string attackPatternStr = Resources.Load<TextAsset>("Datas/UnitAtttackPatternDic").text;
            //string attackPatternStr = LoadJsonDatas("Datas/UnitAtttackPatternDic");
            //allUnitAttackPatternDic = JsonConvert.DeserializeObject<Dictionary<int, UnitAttackPattern>>(attackPatternStr);
            //string uniqueStr = Resources.Load<TextAsset>("Datas/UniqueEquipmentDataDic").text;

            //string uniqueStr = LoadJsonDatas("Datas/UniqueEquipmentDataDic");
            //uniqueEquipmentDataDic = JsonConvert.DeserializeObject<Dictionary<int, UniqueEquipmentData>>(uniqueStr);
            //
            string nickNameDic = LoadJsonDatas("Datas/UnitNickNameDic");
            unitNickNameDic = JsonConvert.DeserializeObject<Dictionary<int, string>>(nickNameDic);
            unitNickNameDic2 = JsonConvert.DeserializeObject<Dictionary<int, string>>(LoadJsonDatas("Datas/nickname"));
            string firearmStr = LoadJsonDatas("Datas/AllUnitFirearmData");

            if (!string.IsNullOrEmpty(firearmStr))
                firearmData = JsonConvert.DeserializeObject<AllUnitFirearmData>(firearmStr);

            Task.WhenAll(tasks).Wait();
            LoadUnitData();

            LoadFinished = true;
            Debugtext.text += "\n数据加载完毕！";
            CreateShowUnitIDS();
            wait.Close();
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
                playerSetting.playerProcess = 70;
                playerSetting.playerLevel = 271;
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
        public void WindowConfigMessage(string word, SystemWindowMessage.configDelegate configDelegate, SystemWindowMessage.configDelegate cancelDelegate = null)
        {
            GameObject a = Instantiate(SystemWindowMessagePerferb);
            a.transform.SetParent(LatestUIback.transform, false);
            a.transform.localPosition = new Vector3();
            a.transform.localScale = new Vector3(1, 1, 1);
            a.GetComponent<SystemWindowMessage>().SetWindowMassage(word, configDelegate, cancelDelegate);
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
        public void WindowInputMessage(string helpword, Action<string> finishAction, Action cancelAction = null)
        {
            GameObject a = Instantiate(SystemInputPrefab);
            a.transform.SetParent(LatestUIback.transform, false);
            a.transform.localPosition = new Vector3();
            a.transform.localScale = new Vector3(1, 1, 1);
            a.GetComponent<SystemWindowMessage>().SetWindowInputMassage(helpword, finishAction, cancelAction);
        }
        private IEnumerator WindowMessage_start(string word)
        {
            GameObject a = Instantiate(MassagePerferb);
            a.transform.SetParent(LatestUIback.transform);
            a.transform.localPosition = new Vector3(0, -150, 0);
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
        public void ChangeSceneToBalttle(List<int> my, AddedPlayerData other, bool isAutoMode = true, bool forceAutoMode = true)
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
            this.isSetMode = false;
            this.isSemanMode = false;
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
            isSetMode = data.isSetMode;
            isSemanMode = data.isSemanMode;
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
        public bool JudgeWeatherAllowUniqueEq2(int unitid)
        {
            return UniqueEquipment2DataDic.ContainsKey(unitid);
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
                //showUnitIDs.Add(170101);
                //showUnitIDs.Add(170201);
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
                WindowConfigMessage("初始化存档文件时出现异常:" + ex.Message, null);
            }
        }
        IEnumerator LoadScene()
        {
            GameObject a = IsGuildBattle ? Instantiate(LoadingPagePrefab_2) : Instantiate(LoadingPagePrefab);
            a.transform.SetParent(LatestUIback.transform);
            a.transform.localScale = new Vector3(1, 1, 1);
            a.GetComponent<RectTransform>().offsetMax = new Vector2(5, 5);
            a.GetComponent<RectTransform>().offsetMin = new Vector2(-5, -5);

            //yield return new WaitForSeconds(1.5f);
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

        public string LoadJsonDatas(string path, bool forceStreaming = true)
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
            GameObject p = Instantiate(LoadingPagePrefab, GameObject.Find("Canvas")?.transform);
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
            foreach (var unit in UnitRarityDic.Values)
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
        public int GetMotionType(int unitid, int skinid)
        {
            if (skinid == 170361 || unitid == 170301)
                return 34;
            return UnitRarityDic.TryGetValue(unitid, out UnitRarityData t) ? t.detailData.motionType : 0;
        }
        public async Task SendPostRequestAsync(Action onComplete)
        {
            using UnityWebRequest www = new UnityWebRequest(Url, UnityWebRequest.kHttpVerbPOST);
            byte[] formdata = new System.Text.UTF8Encoding().GetBytes("{\"regionCode\":\"cn\"}");

            // 设置请求头部
            www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(formdata);
            www.downloadHandler = new DownloadHandlerBuffer();

            // 使用 TaskCompletionSource 包装 UnityWebRequest
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            www.SendWebRequest().completed += (AsyncOperation op) =>
            {
                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error: " + www.error);
                    truthVersion = "获取失败（重试）";
                }
                else
                {
                    var responseText = www.downloadHandler.text;
                    var response = JsonConvert.DeserializeObject<ApiResponse>(responseText);
                    // 输出truthVersion
                    truthVersion = response.data.truthVersion;
                }
                onComplete?.Invoke();
                tcs.SetResult(true);
            };

            // 等待任务完成
            await tcs.Task;
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