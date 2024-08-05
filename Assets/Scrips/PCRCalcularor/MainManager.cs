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

namespace PCRCaculator
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;
        public GameObject MassagePerferb;//������Ϣ���װ�
        public GameObject SystemWindowMessagePerferb;//����ȷ�ϵװ�
        public GameObject asyncPrefab;
        public GameObject SystemInputPrefab;//�����������
        public GameObject LoadingPagePrefab;//�������
        public GameObject LoadingPagePrefab_2;//�������2
        public GameObject WaitingPrefab;//�ȴ����
        //public TextAsset db;
        //public TextAsset unitTimeTxt;
        //public TextAsset unitPrefabData;
        public enum StayPage { home = 0, character = 1, battle = 2, gamble = 3,calculator = 4 }
        public StayPage stayPage;
        //public int loadCharacterMax;//�����ص��Ľ�ɫ���
        public int levelMax { get => playerSetting.playerLevel; }
        public bool UseNewBattleSystem = true;
        public bool useJapanData;
        public bool useVerification;
        public LoginTool loginTool;

        private Dictionary<string, Sprite> spriteCacheDic = new Dictionary<string, Sprite>();//ͼƬ����
        private Dictionary<int, EquipmentData> equipmentDic = new Dictionary<int, EquipmentData>();//װ������װ��id�Ķ�Ӧ�ֵ�
        public Dictionary<int, UnitData> unitDataDic = new Dictionary<int, UnitData>();//��ɫ��ɸ����������ɫid�Ķ�Ӧ�ֵ�(��ʱ)
        public Dictionary<int, UnitData> unitDataDic_save = new Dictionary<int, UnitData>();//��ɫ��ɸ����������ɫid�Ķ�Ӧ�ֵ�(�ѱ���)
        private Dictionary<int, UnitRarityData> unitRarityDic = new Dictionary<int, UnitRarityData>();//��ɫ�����������ɫid�Ķ�Ӧ�ֵ� 
        private Dictionary<int, UnitStoryData> unitStoryDic = new Dictionary<int, UnitStoryData>();//��ɫ������б�
        private Dictionary<int, List<int>> unitStoryEffectDic = new Dictionary<int, List<int>>();//��ɫ�������б�
        private Dictionary<int, SkillData> skillDataDic = new Dictionary<int, SkillData>();//���еļ����б�
        private Dictionary<int, SkillAction> skillActionDic = new Dictionary<int, SkillAction>();//����С�����б�
        private Dictionary<int, string> unitName_cn = new Dictionary<int, string>();//��ɫ��������
        private Dictionary<int, string[]> skillNameAndDescribe_cn = new Dictionary<int, string[]>();//�����������ֺ�����
        private Dictionary<int, string> skillActionDescribe_cn = new Dictionary<int, string>();//����Ƭ����������
        private Dictionary<int, UnitSkillTimeData> allUnitSkillTimeDataDic;//���н�ɫ�ļ���ʱ������
        private Dictionary<int, UnitAttackPattern> allUnitAttackPatternDic;//���н�ɫ����ѭ������
        public Dictionary<int, unique_equip_enhance_rate[]> UniqueEquipmentDataDic = new Dictionary<int, unique_equip_enhance_rate[]>();//��ɫר���ֵ�
        public List<EReduction> ereductionTable = new List<EReduction>();
        private AllUnitFirearmData firearmData = new AllUnitFirearmData();
        private Elements.MasterUnitSkillDataRf masterUnitSkillDataRf;//δ������
        private List<int> enemy_ignore_skill_rf = new List<int>();//δ������
        
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
        //public Dictionary<int, UniqueEquipmentData> UniqueEquipmentDataDic { get => uniqueEquipmentDataDic;}
        //public float PlayerBodyWidth { get => playerSetting.bodyWidth; }
        public AllUnitFirearmData FirearmData { get => firearmData;}
        public List<int> Enemy_ignore_skill_rf { get => enemy_ignore_skill_rf;}
        public Elements.MasterUnitSkillDataRf MasterUnitSkillDataRf { get => masterUnitSkillDataRf;}

        public readonly List<int> showUnitIDs = new List<int>();
        public List<int> showSummonIDs;
        public AutoCalculatorData AutoCalculatorData = new AutoCalculatorData();
        public int MaxTPUpValue => playerSetting.maxTPUpValue;

        public Dictionary<(int, int), promotion_bonus> rbs;

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
            public long BossVersionJP = Instance.useJapanData ? 10051600 : 10040900;
            public long BossVersionCN = 202408051714;
            public bool useQA = true;
            public bool useJP = true;
            public bool newAB = true;

        }

        public VersionData Version;
        public Dictionary<int, int> unitStoryLoveDic;

        private void Start()
        {
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
                var fs = new FileStream("/storage/emulated/0/Download/D4-�������ƻ�ˮ��ħʥǧ�沽-2170w.xlsx"
                    , FileMode.Open, FileAccess.Read);
#endif*/
                if (useVerification)
                {
                    loginTool?.gameObject.SetActive(true);
                }
#if PLATFORM_ANDROID
                var dir = Application.persistentDataPath + "/AB";
#else
                var dir = Application.streamingAssetsPath + "/../.ABExt2";
#endif
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                Load();
                CreateShowUnitIDS();
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

        private void LoadAsync(WaitUI wait)
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

            ABExTool.persistentDataPath = Application.persistentDataPath;
            ABExTool.dataPath = Application.dataPath;

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
                WindowMessage("�������ݷ����������ð汾");
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
                        unitExEquips = dbTool.unitExEquips;
                        //Guild.GuildManager.EnemyDataDic = dbTool.GetEnemyDataDic();
                        Guild.GuildManager.EnemyDataDic = dbTool.Dic2;
                        UniqueEquipmentDataDic = dbTool.Dic9;
                        
                        var (unitStoryDic2, unitStoryEffectDic2, unitStoryLoveDic2) = dbTool2.Pair;
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
                                    Task.Run(() => skillActionDic.OverrideWith(dbTool2.Dic4,
                                        k => k > 200000000 && k < 400000000)),
                                    Task.Run(() => allUnitAttackPatternDic.OverrideWith(dbTool2.Dic5)),
                                    Task.Run(() => guildEnemyDatas.OverrideWith(dbTool2.Dic6)),
                                    Task.Run(() => enemyMPartsDic.OverrideWith(dbTool2.Dic7)),
                                    Task.Run(() => GuildManager.EnemyDataDic.OverrideWith(dbTool2.Dic2))
                                    // Task.Run(() => uniqueEquipmentDataDic.OverrideWith(dbTool2.Dic9))
                                );


                            Task.WaitAll(
                                Task.Run(() =>
                                    unitRarityDic.OverrideWith(dbTool3.Dic1.Where(p => p.Key / 1000 == 17 || p.Key == 105701))),
                                Task.Run(() =>
                                    UniqueEquipmentDataDic.OverrideWith(dbTool3.Dic9.Where(p => p.Key / 10000 == 17))),
                                Task.Run(() =>
                                    skillDataDic.OverrideWith(dbTool3.Dic3.Where(p => p.Key / 100000 == 17))),
                                Task.Run(() =>
                                    skillActionDic.OverrideWith(dbTool3.Dic4.Where(p => p.Key / 10000000 == 17))),
                                Task.Run(() =>
                                    allUnitAttackPatternDic.OverrideWith(dbTool3.Dic5.Where(p => p.Key / 1000000 == 17)))
                            );

                            Task.WaitAll(
                                Task.Run(() => skillDataDic.Override2With(dbTool3.Dic3)),
                                Task.Run(() => equipmentDic.Override2With(dbTool3.Dic8)),
                                Task.Run(() => skillActionDic.Override2With(dbTool3.Dic4)),
                                Task.Run(() => unitRarityDic.Override2With(dbTool3.Dic1)),
                                Task.Run(() => GuildManager.EnemyDataDic.Override2With(dbTool3.Dic2))
                            );
                            
                            // unitRarityDic[170101].ChangeRankData(unitRarityDic[105701].GetRankData());

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

            Debug.LogError($"��ȡDBʧ�ܣ�{ex.Message}");*/

            
            //LoadPlayerSettings();
            //string prefabData = unitPrefabData.text;
            //AllUnitPrefabData allUnitPrefabData = JsonConvert.DeserializeObject<AllUnitPrefabData>(prefabData);
            //allUnitFirearmDatas = allUnitPrefabData.allUnitFirearmDatas;
            //Debugtext.text += "\n�ɹ�����" + allUnitFirearmDatas.Count + "��������Ч���ݣ�";
            //allUnitActionControllerDatas = allUnitPrefabData.allUnitActionControllerDatas;
            //Debugtext.text += "\n�ɹ�����" + allUnitActionControllerDatas.Count + "����ɫԤ�������ݣ�";
            
            string skillTimeStr = LoadJsonDatas("Datas/unitSkillTimeDic");
            //string skillTimeStr = LoadJsonDatas("Datas/unitSkillTimeDic");
            allUnitSkillTimeDataDic = JsonConvert.DeserializeObject<Dictionary<int, UnitSkillTimeData>>(skillTimeStr);
            //Debugtext.text += "\n�ɹ�����" + allUnitSkillTimeDataDic.Count + "������ʱ�����ݣ�";
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
            Debugtext.text += "\n���ݼ�����ϣ�";
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
                playerSetting.playerProcess = 61;
                playerSetting.playerLevel = 244;
                if (PlayerLevelText != null)
                {
                    PlayerLevelText.text = playerSetting.playerLevel + "";
                }
            }
        }

        /// <summary>
        /// ����������õ�json
        /// </summary>
        public void SaveUnitData()
        {
            SaveManager.Save(unitDataDic);
        }
        /// <summary>
        /// ��json��ȡ�������
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
            WindowMessage("ɾ��ʧ�ܣ���������ɾ����");
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
            WindowMessage("��������");
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
                mess += "�Ƿ������";
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
                mess += "�Ƿ������";
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
                return "�ٻ���";
            }
            if (unitid >= 200000)
                return "�з���λ";
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
                //��ȡĿ¼�£���������Ŀ¼�����ļ�����Ŀ¼
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //�ж��Ƿ��ļ���
                    {
                        if (!Directory.Exists(targetDirectory + "\\" + i.Name))
                        {
                            //Ŀ��Ŀ¼�²����ڴ��ļ��м��������ļ���
                            Directory.CreateDirectory(targetDirectory + "\\" + i.Name);
                        }
                        //�ݹ���ø������ļ���
                        DirectoryCopy(i.FullName, targetDirectory + "\\" + i.Name);
                    }
                    else
                    {
                        //�����ļ��м������ļ���true��ʾ���Ը���ͬ���ļ�
                        File.Copy(i.FullName, targetDirectory + "\\" + i.Name, true);
                    }
                }
            }
            catch (Exception ex)
            {
                WindowConfigMessage("��ʼ���浵�ļ�ʱ�����쳣:" + ex.Message,null);
            }
        }
        IEnumerator LoadScene()
        {
            GameObject a = IsGuildBattle?Instantiate(LoadingPagePrefab_2): Instantiate(LoadingPagePrefab);
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
                    Debug.Log(relativePath + "ͼƬ����ʧ�ܣ�ԭ��" + ex.Message);
                }

            }

            //�ü��صõ�����Դ����ʵ������Ϸ����ʵ����Ϸ����Ķ�̬����
            return tmpsprite;
        }
        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ�� </returns>
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
        /// DES�����ַ���
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>
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
        [ContextMenu("�����ŵ�����unitdetail�ļ�")]
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
            Debug.Log("�ɹ���");
        }
        /// <summary>
        ///����ʱ��TP�ı���
        /// </summary>
        /// <returns></returns>
        internal float GetDodgeTPRecoveryRatio()
        {
            return 0;
        }
        public int GetMotionType(int unitid,int skinid)
        {
            if (skinid == 170361 || unitid == 170301)
                return 34;
            return UnitRarityDic.TryGetValue(unitid, out UnitRarityData t) ? t.detailData.motionType : 0;
        }
    }
    public class UnitData_other
    {
        public string name = "δ����";
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
            string result = "�˺���" + currentDamage + "\n���������˺���" + criticalEX + "\n�����˺�������ƫ�ߣ���" + exceptDamage + "\n������ӣ�" + randomSeed;
            if (backTime > 0) result += $"��\n{backTime}s";
            if (warnings.Count > 0)
            {
                for(int i=0;i<3;i++)
                {
                    if(i<warnings.Count)
                    result +="\n"+ warnings[i];
                }
                if (warnings.Count > 4)
                    result += "\n��" + warnings.Count + "������";
            }
            return result;
        }

    }
}