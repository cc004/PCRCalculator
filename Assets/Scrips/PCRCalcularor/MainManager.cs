using Elements;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using PCRApi.CN;
using PCRCaculator.Calc;
using PCRCaculator.Guild;
using PCRCaculator.SQL;
using PCRCaculator.Update;
using PCRCalcularor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
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
        public enum StayPage { home = 0, character = 1, battle = 2, gamble = 3, calculator = 4 }
        public StayPage stayPage;
        //public int loadCharacterMax;//最多加载到的角色序号
        public int GetLevelMax(int battleMode, int connectRank = 0) => playerSetting.GetLevelMax(battleMode, connectRank);
        public int EffectiveLevelMax => GetLevelMax(IsGuildBattle ? 0 : 2);
        public int levelMax { get => EffectiveLevelMax; }
        public bool UseNewBattleSystem = true;
        public bool useJapanData {get => Version.useJP;}
        public bool useVerification;
        public int PlayerProcessLimit => databaseCache.playerProcessLimit;
        public int PlayerLevelLimit => databaseCache.playerLevelLimit;
        public int UniqueEqLevelLimit => databaseCache.uniqueEqLevelLimit;

        public DatabaseCache databaseCache = new DatabaseCache();

        public class DatabaseCache
        {
            public int cacheSchemaVersion;
            public int playerProcessLimit;
            public int playerLevelLimit;
            public int uniqueEqLevelLimit;
            public bool cacheBuildComplete;
            public string databaseSourceFingerprint;
            public Dictionary<int, ailment_data> ailmentDataDic = new Dictionary<int, ailment_data>();
            public Dictionary<int, int[]> resistDataDic = new Dictionary<int, int[]>();//抵抗数据
            public Dictionary<int, EquipmentData> equipmentDic = new Dictionary<int, EquipmentData>();//装备类与装备id的对应字典
            public Dictionary<int, UnitRarityData> unitRarityDic = new Dictionary<int, UnitRarityData>();//角色基础数据与角色id的对应字典 
            public Dictionary<int, List<extra_effect_unit_group>> extraEffectUnitGroupDic = new Dictionary<int, List<extra_effect_unit_group>>();//特别角色组(技能角色存在判断)
            public Dictionary<int, UnitStoryData> unitStoryDic = new Dictionary<int, UnitStoryData>();//角色羁绊奖励列表
            public Dictionary<int, List<int>> unitStoryEffectDic = new Dictionary<int, List<int>>();//角色的马甲列表
            public Dictionary<int, Dictionary<int, promotion_bonus>> rbs = new Dictionary<int, Dictionary<int, promotion_bonus>>();
            public List<EReduction> ereductionTable = new List<EReduction>();
            public Dictionary<int, talent_quest_battle_effect> talentQuestBattleEffectDic = new Dictionary<int, talent_quest_battle_effect>();
            public Dictionary<int, abyss_battle_effect> abyssBattleEffectDic = new Dictionary<int, abyss_battle_effect>();
            public Dictionary<int, int> abyssQuestFilterDic = new Dictionary<int, int>();
            public Dictionary<int, int> abyssBossFilterDic = new Dictionary<int, int>();
            public Dictionary<int, mirage_battle_effect> mirageBattleEffectDic = new Dictionary<int, mirage_battle_effect>();
            public Dictionary<int, mirage_nemesis_area> mirageNemesisAreaDic = new Dictionary<int, mirage_nemesis_area>();
            public Dictionary<int, extra_effect_data> extraEffectDataDic = new Dictionary<int, extra_effect_data>();
            public Dictionary<int, talentFormationData> talentFormationDic = new Dictionary<int, talentFormationData>();
            public Dictionary<int, int[]> weaknessDic = new Dictionary<int, int[]>();//角色的弱点列表
            public Dictionary<int, string> skillActionDescribe_cn = new Dictionary<int, string>();//技能片段中文描述
            public Dictionary<int, string[]> skillNameAndDescribe_cn = new Dictionary<int, string[]>();//技能中文名字和描述
            public Dictionary<int, string> unitName_cn = new Dictionary<int, string>();//角色中文名字
            public Dictionary<int, string> exEquipmentName_cn = new Dictionary<int, string>();//Ex装备中文名字
            public Dictionary<int, List<int>> unitStoryEffectDic2 = new Dictionary<int, List<int>>();//角色的马甲列表
            public Dictionary<int, param_type> paramTypeDic = new Dictionary<int, param_type>();
            public Dictionary<int, TeamSkillEnhanceData> teamSkillEnhanceDataDic = new Dictionary<int, TeamSkillEnhanceData>();
            public Dictionary<int, TeamSkillNode> teamSkillNodeDic = new Dictionary<int, TeamSkillNode>();
            public Dictionary<int, TalentSkillEnhanceData> talentSkillEnhanceDataDic = new Dictionary<int, TalentSkillEnhanceData>();
            public Dictionary<int, TalentSkillNode> talentSkillNodeDic = new Dictionary<int, TalentSkillNode>();
            public Dictionary<int, unique_equip_enhance_rate[]> uniqueEquipment2DataDic = new Dictionary<int, unique_equip_enhance_rate[]>();//角色专武2字典
            public Dictionary<int, unique_equip_enhance_rate[]> uniqueEquipmentDataDic = new Dictionary<int, unique_equip_enhance_rate[]>();//角色专武字典
            public Dictionary<int, WaveGroupDataBase> secretDungeonWaveGroupDataDic = new Dictionary<int, WaveGroupDataBase>();
            public Dictionary<int, WaveGroupDataBase> talentQuestWaveGroupDataDic = new Dictionary<int, WaveGroupDataBase>();
            public Dictionary<int, WaveGroupDataBase> abyssWaveGroupDataDic = new Dictionary<int, WaveGroupDataBase>();
            public Dictionary<int, WaveGroupDataBase> mirageWaveGroupDataDic = new Dictionary<int, WaveGroupDataBase>();
            public Dictionary<int, WaveGroupDataBase> LabyrinthWaveGroupDataDic = new Dictionary<int, WaveGroupDataBase>();
            public Dictionary<int, EnemyData> enemyDataDic = new Dictionary<int, EnemyData>();
            public Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts> enemyMPartsDic = new Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts>();
            public Dictionary<int, int> unitStoryLoveDic = new Dictionary<int, int>();
            public Dictionary<int, SkillData> skillDataDic = new Dictionary<int, SkillData>();//所有的技能列表
            public Elements.MasterUnitSkillDataRf masterUnitSkillDataRf;//未来可期
            public Dictionary<int, SkillAction> skillActionDic = new Dictionary<int, SkillAction>();//所有小技能列表
            public Dictionary<int, UnitAttackPattern> allUnitAttackPatternDic = new Dictionary<int, UnitAttackPattern>();//所有角色技能循环数据
            public Dictionary<int, Guild.GuildEnemyData> guildEnemyDatas = new Dictionary<int, Guild.GuildEnemyData>();
            public Dictionary<int, Dictionary<int, ex_equipment_data>[]> unitExEquips = new Dictionary<int, Dictionary<int, ex_equipment_data>[]>();
            public Dictionary<int, EnemyData> towerEnemyParameterDic = new Dictionary<int, EnemyData>();
            public Dictionary<int, EnemyData> talentQuestEnemyParameterDic = new Dictionary<int, EnemyData>();
            public Dictionary<int, EnemyData> abyssEnemyParameterDic = new Dictionary<int, EnemyData>();
            public Dictionary<int, EnemyData> labyrinthEnemyParameterDic = new Dictionary<int, EnemyData>();
            public Dictionary<int, EnemyData> mirageEnemyParameterDic = new Dictionary<int, EnemyData>();
            public Dictionary<int, unit_role_data> UnitRoleDataDic = new();
            public Dictionary<int, unit_role_mastery_id> UnitRoleMasteryIdDic = new();
            public Dictionary<int, unit_role_mastery_slot_data> UnitRoleSlotDataDic = new();
            public Dictionary<int, UnitRoleMasterySlotEnhanceData> UnitRoleMasterySlotEnhanceDataDic = new();
            public Dictionary<int, List<connect_rank_status>> ConnectRankStatusDic = new();
            public Dictionary<int, connect_rank_chart> ConnectRankChartDic = new();
            public List<connect_rank_bonus> ConnectRankBonusList = new();
            public Dictionary<int, connect_rank_boost> ConnectRankBoostDic = new();
            public Dictionary<int, connect_rank_limit_common> ConnectRankLimitCommonDic = new();
            public Dictionary<int, List<connect_rank_limit_item>> ConnectRankLimitItemDic = new();
            public Dictionary<int, List<connect_rank_enhance>> ConnectRankEnhanceDic = new();
            public Dictionary<int, labyrinth_enhance_node> LabyrinthEnhanceNodeDic = new();
            public Dictionary<int, labyrinth_enhance_data> LabyrinthEnhanceDataDic = new();
            public Dictionary<int, labyrinth_enhance_information> LabyrinthEnhanceInformationDic = new();
            public Dictionary<int, labyrinth_quest_data> LabyrinthQuestDataDic = new();
            public Dictionary<int, labyrinth_difficulty_enemy_data> LabyrinthDifficultyEnemyDataDic = new();
            public Dictionary<int, labyrinth_difficulty_effect> LabyrinthDifficultyEffectDic = new();
            public Dictionary<int, labyrinth_relic> LabyrinthRelicDic = new();
            public Dictionary<int, labyrinth_relic_effect_group> LabyrinthRelicEffectGroupDic = new();
            public Dictionary<int, labyrinth_relic_effect> LabyrinthRelicEffectDic = new();
            public Dictionary<int, List<labyrinth_relic_mark_effect>> LabyrinthRelicMarkEffectDic = new();
            public Dictionary<int, List<relic_action_exec_time>> RelicActionExecTimeDic = new();
            public Dictionary<int, List<relic_skill_exec_time>> RelicSkillExecTimeDic = new();
        }

        private Dictionary<string, Sprite> spriteCacheDic = new Dictionary<string, Sprite>();//图片缓存
        public Dictionary<int, UnitData> unitDataDic = new Dictionary<int, UnitData>();//角色类可更改数据与角色id的对应字典(临时)
        public Dictionary<int, UnitData> unitDataDic_save = new Dictionary<int, UnitData>();//角色类可更改数据与角色id的对应字典(已保存)
        private Dictionary<int, UnitSkillTimeData> allUnitSkillTimeDataDic;//所有角色的技能时间数据
        // public Dictionary<int, unit_talent> unitTalentDic = new Dictionary<int, unit_talent>();//角色属性字典
        private AllUnitFirearmData firearmData = new AllUnitFirearmData();
        private List<int> enemy_ignore_skill_rf = new List<int>();//未来可期

        private Dictionary<int, string> unitNickNameDic = new Dictionary<int, string>();

        private Dictionary<int, string> unitNickNameDic2 = new Dictionary<int, string>();
        private CharacterManager characterManager;
        private AdventureManager battleManager;
        //private SQLiteHelper sql;

        private Coroutine windowmassageIE;
        private PlayerSetting playerSetting;

        private List<UnitData> playerDataForBattle;
        private List<UnitData> enemyDataForBattle;
        // private bool isGuildBattle;
        public GuildBattleData guildBattleData;
        public bool isAutoMode;
        public bool forceAutoMode;
        public bool isSetMode;
        public bool isSemanMode;
#if ENABLE_THREADING
        [ThreadStatic]
        public static TeamEnhanceParameter teamEnhanceData;
        [ThreadStatic]
        private static TeamEnhanceParameter arenaEnemyTeamEnhanceData;
        [ThreadStatic]
        private static TeamEnhanceConfiguration arenaEnemyTeamEnhanceConfiguration;
        [ThreadStatic]
        private static TeamEnhanceConfiguration arenaEnemyTeamEnhanceOverride;
        [ThreadStatic]
        private static bool hasArenaEnemyTeamEnhanceOverride;
#else
        public TeamEnhanceParameter teamEnhanceData;
        private TeamEnhanceParameter arenaEnemyTeamEnhanceData;
        private TeamEnhanceConfiguration arenaEnemyTeamEnhanceConfiguration;
        private TeamEnhanceConfiguration arenaEnemyTeamEnhanceOverride;
        private bool hasArenaEnemyTeamEnhanceOverride;
#endif
        private static byte[] Keys = { 0x20, 0x20, 0x78, 0x25, 0xCE, 0x37, 0x66, 0xFF };
        
        // 优化：添加数据缓存字典，避免重复计算
        private static readonly Dictionary<string, object> _dataCache = new Dictionary<string, object>();

        public Dictionary<int, ailment_data> AilmentDataDic { get => databaseCache.ailmentDataDic; }
        public Dictionary<int, int[]> ResistDataDic { get => databaseCache.resistDataDic; }  // 抵抗
        public Dictionary<int, EquipmentData> EquipmentDic { get => databaseCache.equipmentDic; }  // 装备类与装备id的对应字典
        public Dictionary<int, UnitRarityData> UnitRarityDic { get => databaseCache.unitRarityDic; }  // 角色基础数据与角色id的对应字典 
        public Dictionary<int, List<extra_effect_unit_group>> ExtraEffectUnitGroupDic { get => databaseCache.extraEffectUnitGroupDic; }  // 特别角色组(技能角色存在判断)
        public Dictionary<int, UnitStoryData> UnitStoryDic { get => databaseCache.unitStoryDic; }  // 角色羁绊奖励列表
        public Dictionary<int, List<int>> UnitStoryEffectDic { get => databaseCache.unitStoryEffectDic; }  // 角色的马甲列表
        public Dictionary<int, Dictionary<int, promotion_bonus>> Rbs { get => databaseCache.rbs; }
        public List<EReduction> EreductionTable { get => databaseCache.ereductionTable; }
        public Dictionary<int, talent_quest_battle_effect> TalentQuestBattleEffectDic { get => databaseCache.talentQuestBattleEffectDic; }
        public Dictionary<int, abyss_battle_effect> AbyssBattleEffectDic { get => databaseCache.abyssBattleEffectDic; }
        public Dictionary<int, int> AbyssQuestFilterDic { get => databaseCache.abyssQuestFilterDic; }
        public Dictionary<int, int> AbyssBossFilterDic { get => databaseCache.abyssBossFilterDic; }
        public Dictionary<int, mirage_battle_effect> MirageBattleEffectDic { get => databaseCache.mirageBattleEffectDic; }
        public Dictionary<int, mirage_nemesis_area> MirageNemesisAreaDic { get => databaseCache.mirageNemesisAreaDic; }
        public Dictionary<int, extra_effect_data> ExtraEffectDataDic { get => databaseCache.extraEffectDataDic; }
        public Dictionary<int, talentFormationData> TalentFormationDic { get => databaseCache.talentFormationDic; }
        public Dictionary<int, int[]> WeaknessDic { get => databaseCache.weaknessDic; }  // 角色的弱点列表
        public Dictionary<int, string> SkillActionDescribeCn { get => databaseCache.skillActionDescribe_cn; }  // 技能片段中文描述
        public Dictionary<int, string[]> SkillNameAndDescribeCn { get => databaseCache.skillNameAndDescribe_cn; }  // 技能中文名字和描述
        public Dictionary<int, string> UnitNameCn { get => databaseCache.unitName_cn; }  // 角色中文名字
        public Dictionary<int, List<int>> UnitStoryEffectDic2 { get => databaseCache.unitStoryEffectDic2; }  // 角色的马甲列表
        public Dictionary<int, param_type> ParamTypeDic { get => databaseCache.paramTypeDic; }
        public Dictionary<int, TeamSkillEnhanceData> TeamSkillEnhanceDataDic { get => databaseCache.teamSkillEnhanceDataDic; }
        public Dictionary<int, TeamSkillNode> TeamSkillNodeDic { get => databaseCache.teamSkillNodeDic; }
        public Dictionary<int, TalentSkillEnhanceData> TalentSkillEnhanceDataDic { get => databaseCache.talentSkillEnhanceDataDic; }
        public Dictionary<int, TalentSkillNode> TalentSkillNodeDic { get => databaseCache.talentSkillNodeDic; }
        public Dictionary<int, unique_equip_enhance_rate[]> UniqueEquipment2DataDic { get => databaseCache.uniqueEquipment2DataDic; }  // 角色专武2字典
        public Dictionary<int, unique_equip_enhance_rate[]> UniqueEquipmentDataDic { get => databaseCache.uniqueEquipmentDataDic; }  // 角色专武字典
        public Dictionary<int, WaveGroupDataBase> SecretDungeonWaveGroupDataDic { get => databaseCache.secretDungeonWaveGroupDataDic; }
        public Dictionary<int, WaveGroupDataBase> TalentQuestWaveGroupDataDic { get => databaseCache.talentQuestWaveGroupDataDic; }
        public Dictionary<int, WaveGroupDataBase> AbyssWaveGroupDataDic { get => databaseCache.abyssWaveGroupDataDic; }
        public Dictionary<int, WaveGroupDataBase> MirageWaveGroupDataDic { get => databaseCache.mirageWaveGroupDataDic; }
        public Dictionary<int, WaveGroupDataBase> LabyrinthWaveGroupDataDic { get => databaseCache.LabyrinthWaveGroupDataDic; }
        public Dictionary<int, EnemyData> EnemyDataDic { get => databaseCache.enemyDataDic; }
        public Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts> EnemyMPartsDic { get => databaseCache.enemyMPartsDic; }
        public Dictionary<int, int> UnitStoryLoveDic { get => databaseCache.unitStoryLoveDic; }
        public Dictionary<int, SkillData> SkillDataDic { get => databaseCache.skillDataDic; }  // 所有的技能列表
        public Elements.MasterUnitSkillDataRf MasterUnitSkillDataRf { get => databaseCache.masterUnitSkillDataRf; }  // 未来可期
        public Dictionary<int, SkillAction> SkillActionDic { get => databaseCache.skillActionDic; }  // 所有小技能列表
        public Dictionary<int, UnitAttackPattern> AllUnitAttackPatternDic { get => databaseCache.allUnitAttackPatternDic; }  // 所有角色技能循环数据
        public Dictionary<int, Guild.GuildEnemyData> GuildEnemyDatas { get => databaseCache.guildEnemyDatas; }
        public Dictionary<int, Dictionary<int, ex_equipment_data>[]> UnitExEquips { get => databaseCache.unitExEquips; }
        public Dictionary<int, EnemyData> TowerEnemyParameterDic { get => databaseCache.towerEnemyParameterDic; }
        public Dictionary<int, EnemyData> TalentQuestEnemyParameterDic { get => databaseCache.talentQuestEnemyParameterDic; }
        public Dictionary<int, EnemyData> AbyssEnemyParameterDic { get => databaseCache.abyssEnemyParameterDic; }
        public Dictionary<int, EnemyData> LabyrinthEnemyParameterDic { get => databaseCache.labyrinthEnemyParameterDic; }
        public Dictionary<int, EnemyData> MirageEnemyParameterDic { get => databaseCache.mirageEnemyParameterDic; }
        public Dictionary<int, unit_role_data> UnitRoleDataDic { get => databaseCache.UnitRoleDataDic; }
        public Dictionary<int, unit_role_mastery_id> UnitRoleMasteryIdDic { get => databaseCache.UnitRoleMasteryIdDic; }
        public Dictionary<int, unit_role_mastery_slot_data> UnitRoleSlotDataDic { get => databaseCache.UnitRoleSlotDataDic; }
        public Dictionary<int, UnitRoleMasterySlotEnhanceData> UnitRoleMasterySlotEnhanceDataDic { get => databaseCache.UnitRoleMasterySlotEnhanceDataDic; }
        public Dictionary<int, List<connect_rank_status>> ConnectRankStatusDic { get => databaseCache.ConnectRankStatusDic; }
        public Dictionary<int, connect_rank_chart> ConnectRankChartDic { get => databaseCache.ConnectRankChartDic; }
        public List<connect_rank_bonus> ConnectRankBonusList { get => databaseCache.ConnectRankBonusList; }
        public Dictionary<int, connect_rank_boost> ConnectRankBoostDic { get => databaseCache.ConnectRankBoostDic; }
        public Dictionary<int, connect_rank_limit_common> ConnectRankLimitCommonDic { get => databaseCache.ConnectRankLimitCommonDic; }
        public Dictionary<int, List<connect_rank_limit_item>> ConnectRankLimitItemDic { get => databaseCache.ConnectRankLimitItemDic; }
        public Dictionary<int, List<connect_rank_enhance>> ConnectRankEnhanceDic { get => databaseCache.ConnectRankEnhanceDic; }
        public Dictionary<int, labyrinth_enhance_node> LabyrinthEnhanceNodeDic { get => databaseCache.LabyrinthEnhanceNodeDic; }
        public Dictionary<int, labyrinth_enhance_data> LabyrinthEnhanceDataDic { get => databaseCache.LabyrinthEnhanceDataDic; }
        public Dictionary<int, labyrinth_enhance_information> LabyrinthEnhanceInformationDic { get => databaseCache.LabyrinthEnhanceInformationDic; }
        public Dictionary<int, labyrinth_quest_data> LabyrinthQuestDataDic { get => databaseCache.LabyrinthQuestDataDic; }
        public Dictionary<int, labyrinth_difficulty_enemy_data> LabyrinthDifficultyEnemyDataDic { get => databaseCache.LabyrinthDifficultyEnemyDataDic; }
        public Dictionary<int, labyrinth_difficulty_effect> LabyrinthDifficultyEffectDic { get => databaseCache.LabyrinthDifficultyEffectDic; }
        public Dictionary<int, labyrinth_relic> LabyrinthRelicDic { get => databaseCache.LabyrinthRelicDic; }
        public Dictionary<int, labyrinth_relic_effect_group> LabyrinthRelicEffectGroupDic { get => databaseCache.LabyrinthRelicEffectGroupDic; }
        public Dictionary<int, labyrinth_relic_effect> LabyrinthRelicEffectDic { get => databaseCache.LabyrinthRelicEffectDic; }
        public Dictionary<int, List<labyrinth_relic_mark_effect>> LabyrinthRelicMarkEffectDic { get => databaseCache.LabyrinthRelicMarkEffectDic; }
        public Dictionary<int, List<relic_action_exec_time>> RelicActionExecTimeDic { get => databaseCache.RelicActionExecTimeDic; }
        public Dictionary<int, List<relic_skill_exec_time>> RelicSkillExecTimeDic { get => databaseCache.RelicSkillExecTimeDic; }

        public Dictionary<int, string> UnitName_cn { get => databaseCache.unitName_cn; }
        public Dictionary<int, string[]> SkillNameAndDescribe_cn { get => databaseCache.skillNameAndDescribe_cn; }
        public Dictionary<int, string> SkillActionDescribe_cn { get => databaseCache.skillActionDescribe_cn; }
        public PlayerSetting PlayerSetting { get => playerSetting; }
        public TeamEnhanceParameter TeamEnhanceData { get => teamEnhanceData ??= new TeamEnhanceParameter(); }
        public TeamEnhanceParameter ArenaEnemyTeamEnhanceData
        {
            get
            {
                var configuration = GetArenaEnemyTeamEnhanceConfiguration();
                if (configuration == null)
                    return null;

                var result = arenaEnemyTeamEnhanceData ??= new TeamEnhanceParameter();
                if (!ReferenceEquals(arenaEnemyTeamEnhanceConfiguration, configuration))
                {
                    arenaEnemyTeamEnhanceConfiguration = configuration;
                    result.LoadFromConfiguration(configuration);
                }
                return result;
            }
        }

        public void RefreshArenaEnemyTeamEnhanceData()
        {
            var configuration = GetArenaEnemyTeamEnhanceConfiguration();
            arenaEnemyTeamEnhanceConfiguration = configuration;
            if (configuration != null)
                (arenaEnemyTeamEnhanceData ??= new TeamEnhanceParameter()).LoadFromConfiguration(configuration);
        }

        public void SetArenaEnemyTeamEnhanceOverride(TeamEnhanceConfiguration configuration)
        {
            hasArenaEnemyTeamEnhanceOverride = true;
            arenaEnemyTeamEnhanceOverride = configuration;
            arenaEnemyTeamEnhanceConfiguration = configuration;
            if (configuration != null)
                (arenaEnemyTeamEnhanceData ??= new TeamEnhanceParameter()).LoadFromConfiguration(configuration);
        }

        private TeamEnhanceConfiguration GetArenaEnemyTeamEnhanceConfiguration()
        {
            return hasArenaEnemyTeamEnhanceOverride
                ? arenaEnemyTeamEnhanceOverride
                : GuildManager.Instance?.SettingData?.GetCurrentPlayerGroup()?.arenaEnemyTeamEnhance;
        }
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
        public bool IsGuildBattle = true;
        public List<UnitData> PlayerDataForBattle { get => playerDataForBattle; }
        public List<UnitData> EnemyDataForBattle { get => enemyDataForBattle; }
        // public bool IsAutoMode { get => isAutoMode;}
        // public bool ForceAutoMode { get => forceAutoMode; }
        public GuildBattleData GuildBattleData { get => guildBattleData; }
        //public Dictionary<int, UniqueEquipmentData> UniqueEquipmentDataDic { get => uniqueEquipmentDataDic;}
        //public float PlayerBodyWidth { get => playerSetting.bodyWidth; }
        public AllUnitFirearmData FirearmData { get => firearmData; }
        public List<int> Enemy_ignore_skill_rf { get => enemy_ignore_skill_rf; }

        public readonly List<int> showUnitIDs = new List<int>();
        public List<int> showSummonIDs;
        public AutoCalculatorData AutoCalculatorData = new AutoCalculatorData();
        public int MaxTPUpValue => playerSetting.maxTPUpValue;


        public bool LoadFinished { get; private set; }
        private const string Url = "https://wthee.xyz/pcr/api/v1/db/info/v2";
        private const string Url2 = "https://l3-prod-all-gs-gzlj.bilibiligame.net/source_ini/get_maintenance_status?format=json";
        public string truthVersion;
        public void Awake()
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
            public long CharacterVersionJP = 10066000;
            public long BossVersionJP = 10066000;
            public long BossVersionCN = 0;
            public bool useQA = false;
            public bool useJP = true;
            public bool newAB = false;
            public bool useLatestProd = true;

        }

        public VersionData Version;

        public void Start()
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

#if PLATFORM_ANDROID
                var dir = Application.persistentDataPath + "/AB";
#else
                var dir = ABExTool.persistentDataPath + "/.ABExt2";
#endif
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                try
                {
                    var memoryType = typeof(ExcelPackage).Assembly
                        .GetType("OfficeOpenXml.Utils.RecyclableMemory");

                    var manager = new Microsoft.IO.RecyclableMemoryStreamManager();
                    memoryType.GetMethod("SetRecyclableMemoryStreamManager",
                        BindingFlags.Static | BindingFlags.Public)
                        .Invoke(null, new object[] { manager });
                }
                catch (Exception e)
                {
                    Debug.LogError($"强制初始化失败: {e}");
                }
                Load();
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
                Debugtext.text += e.Message;
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

        private string cacheKey
        {
            get => $"{Version.CharacterVersionJP}_{Version.BossVersionJP}_{Version.BossVersionCN}_{Version.useQA}_{Version.useJP}_{Version.newAB}";
        }

        private string cachePath()
        {
            return Path.Combine(ABExTool.persistentDataPath, $"cache_{cacheKey}.json");
        }

        private sealed class CacheLoadResult
        {
            public bool Loaded { get; set; }
            public bool CanPublishReplacement { get; set; } = true;
            public string Status { get; set; }
        }

        private DatabaseSourceDescriptor[] GetDatabaseSources()
        {
            return new[]
            {
                new DatabaseSourceDescriptor(
                    "character",
                    ABExTool.mgrCharacter?.DataVer,
                    SQLiteTool.ResolveSourceDatabasePath(ABExTool.mgrCharacter)),
                new DatabaseSourceDescriptor(
                    "jp",
                    ABExTool.mgrDataJP?.DataVer,
                    SQLiteTool.ResolveSourceDatabasePath(ABExTool.mgrDataJP)),
                new DatabaseSourceDescriptor(
                    "cn",
                    ABExTool.mgrDataCN?.DataVer,
                    SQLiteTool.ResolveSourceDatabasePath(ABExTool.mgrDataCN)),
            };
        }

        private CacheLoadResult LoadFromCache(string expectedSourceFingerprint, string fingerprintStatus)
        {
            var path = cachePath();
            if (!File.Exists(path))
            {
                return new CacheLoadResult { Status = "not_found" };
            }

            try
            {
                var jsonStr = File.ReadAllText(path);
                var cachedData = JsonConvert.DeserializeObject<DatabaseCache>(jsonStr);
                if (!DatabaseCacheContract.TryValidate(cachedData, expectedSourceFingerprint, out var reason))
                {
                    return RejectCache(path, $"{reason}; fingerprintStatus={fingerprintStatus}");
                }

                databaseCache = cachedData;
                return new CacheLoadResult { Loaded = true, Status = "loaded" };
            }
            catch (Exception e)
            {
                return RejectCache(path, $"缓存读取或反序列化失败: {e}");
            }
        }

        private CacheLoadResult RejectCache(string path, string reason)
        {
            if (DatabaseCacheContract.TryQuarantineRejectedCache(path, out var quarantinedPath, out var error))
            {
                Debug.LogWarning(
                    $"数据库缓存被拒绝并已保留，将从数据库重新加载: reason={reason}; " +
                    $"quarantine={Path.GetFileName(quarantinedPath)}");
                return new CacheLoadResult { Status = "rejected" };
            }

            Debug.LogError(
                $"数据库缓存被拒绝，但无法保留原文件；本次不会发布新缓存: reason={reason}; error={error}");
            return new CacheLoadResult { Status = "rejected_quarantine_failed", CanPublishReplacement = false };
        }

        private void SaveToCache(string sourceFingerprint)
        {
            databaseCache.cacheSchemaVersion = DatabaseCacheContract.CurrentSchemaVersion;
            databaseCache.cacheBuildComplete = true;
            databaseCache.databaseSourceFingerprint = sourceFingerprint;

            if (!DatabaseCacheContract.TryValidate(databaseCache, sourceFingerprint, out var validationReason))
            {
                databaseCache.cacheBuildComplete = false;
                throw new InvalidDataException($"拒绝保存不完整的数据库缓存: {validationReason}");
            }

            string jsonStr;
            try
            {
                jsonStr = JsonConvert.SerializeObject(databaseCache);
            }
            catch (Exception exception)
            {
                Debug.LogError($"数据库缓存序列化失败，本次继续使用内存数据: {exception}");
                return;
            }

            var path = cachePath();
            if (!DatabaseCacheContract.TryWriteAtomically(path, jsonStr, out var error))
            {
                Debug.LogError($"数据库缓存原子发布失败，本次继续使用内存数据: {error}");
            }
        }

        private async void LoadAsync(WaitUI wait)
        {
            // 计时开始
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            Debug.Log("LoadAsync 开始执行");
            
            try
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
                    // 更新 UI：正在获取远端版本
                    try { wait?.SetMessage("正在获取版本信息..."); } catch { }
                    await SendPostRequestAsync(() =>
                    {
                        Version.useQA = false;
                        Version.BossVersionCN = long.Parse(truthVersion);
                        SaveManager.Save(Version);
                    });
                }

                var tasks = new List<Task>();

                var abStopwatch = System.Diagnostics.Stopwatch.StartNew();
                try
                {
                    // 更新 UI：初始化 AB 资源
                    try { wait?.SetMessage("初始化 AB 资源"); } catch { }
                    await ABExTool.StaticInitialize();
                    abStopwatch.Stop();
                    Debug.Log($"AB资源初始化完成，耗时: {abStopwatch.ElapsedMilliseconds}ms");
                }
                catch (Exception ex)
                {
                    abStopwatch.Stop();
                    Debug.LogError($"AB资源初始化失败");
                    Debug.LogError(ex.ToString());
                    try { wait?.SetMessage($"出错啦！{ex} 已重置版本，请重新打开"); } catch { }
                    SaveManager.Save(new VersionData());
                    return;
                }

                string sourceFingerprint = null;
                string fingerprintStatus = null;
                var databaseSources = GetDatabaseSources();
                var fingerprintAvailable = await Task.Run(() =>
                    DatabaseCacheContract.TryComputeSourceFingerprint(
                        databaseSources,
                        out sourceFingerprint,
                        out fingerprintStatus));
                if (!fingerprintAvailable)
                    Debug.LogWarning($"数据库缓存无法验证来源，将从数据库重新加载: {fingerprintStatus}");
                var preparedSourceFingerprint = fingerprintAvailable ? sourceFingerprint : null;

                var cacheStopwatch = System.Diagnostics.Stopwatch.StartNew();
                var cacheLoadResult = LoadFromCache(sourceFingerprint, fingerprintStatus);
                var loadedCache = cacheLoadResult.Loaded;
                var canPublishReplacementCache = cacheLoadResult.CanPublishReplacement;
                if (loadedCache)
                {
                    cacheStopwatch.Stop();
                    Debug.Log($"缓存加载完成，耗时: {cacheStopwatch.ElapsedMilliseconds}ms");
                }
                else
                {
                    cacheStopwatch.Stop();
                    Debug.Log(
                        $"缓存未使用，status={cacheLoadResult.Status}，耗时: {cacheStopwatch.ElapsedMilliseconds}ms");
                    
                    // SQL补丁加载计时
                    var sqlStopwatch = System.Diagnostics.Stopwatch.StartNew();
#if UNITY_ANDROID
                    var loadsql =
                        new WWW("jar:file://" + ABExTool.dataPath + "!/assets/" + "dbdiff.sql");  // this is the path to your StreamingAssets in android
                    while (!loadsql.isDone) { }
                    var sql = Encoding.UTF8.GetString(loadsql.bytes);
#else
                    var patchFile = Path.Combine(Application.streamingAssetsPath, "dbdiff.sql");
                    var sql = File.ReadAllText(patchFile);
#endif
                    sqlStopwatch.Stop();
                    Debug.Log($"SQL补丁加载完成，耗时: {sqlStopwatch.ElapsedMilliseconds}ms");
                    
                    // 更新 UI：已加载 dbdiff.sql
                    try { wait?.SetMessage("加载 SQL 补丁"); } catch { }
                    SQLiteTool.sql = sql;
                    
                    // AB资源初始化计时
                    // 数据库初始化计时
                    var dbInitStopwatch = System.Diagnostics.Stopwatch.StartNew();
                    var dbTool = SQLData.OpenDB(ABExTool.mgrCharacter);
                    var dbTool3 = SQLData.OpenDB(ABExTool.mgrDataCN);
                    var dbTool2 = DatabaseCacheContract.ResolveSharedDatabase(
                        dbTool,
                        () => SQLData.OpenDB(ABExTool.mgrDataJP),
                        ReferenceEquals(ABExTool.mgrDataJP, ABExTool.mgrCharacter));
                    var dbTool2ForCleanup = dbTool2;
                    dbInitStopwatch.Stop();
                    Debug.Log($"数据库打开完成，耗时: {dbInitStopwatch.ElapsedMilliseconds}ms");

                    // 数据库准备计时
                    var dbPrepareStopwatch = System.Diagnostics.Stopwatch.StartNew();
                    // 更新 UI：准备数据库
                    try { wait?.SetMessage("正在准备数据库"); } catch { }
                    await Task.WhenAll(
                        Task.Run(() => dbTool.Prepare()),
                        Task.Run(() =>
                        {
                            if (ABExTool.mgrDataJP != ABExTool.mgrCharacter) dbTool2.Prepare();
                        }),
                        Task.Run(() => dbTool3.Prepare()));
                    dbPrepareStopwatch.Stop();
                    Debug.Log($"数据库准备完成，耗时: {dbPrepareStopwatch.ElapsedMilliseconds}ms");

                    if (string.IsNullOrWhiteSpace(preparedSourceFingerprint))
                    {
                        databaseSources = GetDatabaseSources();
                        var preparedFingerprintAvailable = await Task.Run(() =>
                            DatabaseCacheContract.TryComputeSourceFingerprint(
                                databaseSources,
                                out preparedSourceFingerprint,
                                out fingerprintStatus));
                        if (!preparedFingerprintAvailable)
                            throw new InvalidDataException($"数据库已准备，但无法确认来源: {fingerprintStatus}");
                    }

                    // 数据库数据加载计时
                    var dbDataStopwatch = System.Diagnostics.Stopwatch.StartNew();
                    var dbDataTask = DatabaseCacheContract.RunAfterSuccessAsync(
                        Task.WhenAll(
                            dbTool.ParallelGetAll(),
                            ABExTool.mgrDataJP != ABExTool.mgrCharacter
                                ? dbTool2.ParallelGetAll()
                                : Task.CompletedTask,
                            dbTool3.ParallelGetAll()),
                        () =>
                            {
                                dbDataStopwatch.Stop();
                                Debug.Log($"数据库数据加载完成，耗时: {dbDataStopwatch.ElapsedMilliseconds}ms");

                                var dbCN = dbTool3;

                                // BossVersionJP supplies progression and level limits even when CN boss data is selected.
                                databaseCache.playerProcessLimit = dbTool2.PlayerProcessLimit;
                                databaseCache.playerLevelLimit = dbTool2.PlayerLevelLimit;
                                databaseCache.uniqueEqLevelLimit = dbTool2.UniqueEqLevelLimit;
                                if (!Version.useJP) dbTool2 = dbTool3;

                                databaseCache.ailmentDataDic = dbTool.AilmentDataDic ?? databaseCache.ailmentDataDic;
                                databaseCache.resistDataDic = dbTool.resistDataDic ?? databaseCache.resistDataDic;
                                databaseCache.equipmentDic = dbTool.Dic8 ?? databaseCache.equipmentDic;
                                if (databaseCache.equipmentDic != null && !databaseCache.equipmentDic.ContainsKey(999999))
                                    databaseCache.equipmentDic.Add(999999, EquipmentData.EMPTYDATA);
                                databaseCache.unitRarityDic = dbTool.Dic1 ?? databaseCache.unitRarityDic;
                                databaseCache.extraEffectUnitGroupDic = dbTool.extraEffectUnitGroupDic ?? databaseCache.extraEffectUnitGroupDic;
                                if (dbTool.Pair.Item1 != null) (databaseCache.unitStoryDic, databaseCache.unitStoryEffectDic, databaseCache.unitStoryLoveDic) = dbTool.Pair;
                                databaseCache.skillDataDic = dbTool.Dic3 ?? databaseCache.skillDataDic;
                                databaseCache.skillActionDic = dbTool.Dic4 ?? databaseCache.skillActionDic;
                                databaseCache.allUnitAttackPatternDic = dbTool.Dic5 ?? databaseCache.allUnitAttackPatternDic;
                                databaseCache.guildEnemyDatas = dbTool.Dic6 ?? databaseCache.guildEnemyDatas;
                                databaseCache.enemyMPartsDic = dbTool.Dic7 ?? databaseCache.enemyMPartsDic;
                                databaseCache.unitExEquips = dbTool.unitExEquips ?? databaseCache.unitExEquips;
                                databaseCache.enemyDataDic = dbTool.Dic2 ?? databaseCache.enemyDataDic;
                                databaseCache.towerEnemyParameterDic = dbTool.TowerEnemyParameterDic ?? databaseCache.towerEnemyParameterDic;
                                databaseCache.talentQuestEnemyParameterDic = dbTool.TalentQuestEnemyParameterDic ?? databaseCache.talentQuestEnemyParameterDic;
                                databaseCache.abyssEnemyParameterDic = dbTool.AbyssEnemyParameterDic ?? databaseCache.abyssEnemyParameterDic;
                                databaseCache.labyrinthEnemyParameterDic = dbTool.LabyrinthEnemyParameterDic ?? databaseCache.labyrinthEnemyParameterDic;
                                databaseCache.mirageEnemyParameterDic = dbTool.MirageEnemyParameterDic ?? databaseCache.mirageEnemyParameterDic;
                                databaseCache.secretDungeonWaveGroupDataDic = dbTool.SecretDungeonWaveGroupDataDic ?? databaseCache.secretDungeonWaveGroupDataDic;
                                databaseCache.talentQuestWaveGroupDataDic = dbTool.TalentQuestWaveGroupDataDic ?? databaseCache.talentQuestWaveGroupDataDic;
                                databaseCache.abyssWaveGroupDataDic = dbTool.AbyssWaveGroupDataDic ?? databaseCache.abyssWaveGroupDataDic;
                                databaseCache.mirageWaveGroupDataDic = dbTool.MirageWaveGroupDataDic ?? databaseCache.mirageWaveGroupDataDic;
                                databaseCache.uniqueEquipmentDataDic = dbTool2.Dic9 ?? databaseCache.uniqueEquipmentDataDic;
                                databaseCache.uniqueEquipment2DataDic = dbTool2.Dic92 ?? databaseCache.uniqueEquipment2DataDic;
                                databaseCache.talentSkillNodeDic = dbTool.TalentSkillNodeDic ?? databaseCache.talentSkillNodeDic;
                                databaseCache.talentSkillEnhanceDataDic = dbTool.TalentSkillEnhanceDataDic ?? databaseCache.talentSkillEnhanceDataDic;
                                databaseCache.teamSkillNodeDic = dbTool.TeamSkillNodeDic ?? databaseCache.teamSkillNodeDic;
                                databaseCache.teamSkillEnhanceDataDic = dbTool.TeamSkillEnhanceDataDic ?? databaseCache.teamSkillEnhanceDataDic;
                                databaseCache.paramTypeDic = dbTool.paramTypeDic ?? databaseCache.paramTypeDic;
                                databaseCache.weaknessDic = dbTool.WeaknessDic ?? databaseCache.weaknessDic;
                                databaseCache.talentFormationDic = dbTool.TalentFormationDic ?? databaseCache.talentFormationDic;
                                databaseCache.extraEffectDataDic = dbTool.ExtraEffectDataDic ?? databaseCache.extraEffectDataDic;
                                databaseCache.talentQuestBattleEffectDic = dbTool.TalentQuestBattleEffectDic ?? databaseCache.talentQuestBattleEffectDic;
                                databaseCache.abyssBattleEffectDic = dbTool.AbyssBattleEffectDic ?? databaseCache.abyssBattleEffectDic;
                                databaseCache.abyssQuestFilterDic = dbTool.AbyssQuestFilterDic ?? databaseCache.abyssQuestFilterDic;
                                databaseCache.abyssBossFilterDic = dbTool.AbyssBossFilterDic ?? databaseCache.abyssBossFilterDic;
                                databaseCache.mirageBattleEffectDic = dbTool.MirageBattleEffectDic ?? databaseCache.mirageBattleEffectDic;
                                databaseCache.mirageNemesisAreaDic = dbTool.MirageNemesisAreaDic ?? databaseCache.mirageNemesisAreaDic;
                                databaseCache.UnitRoleDataDic = dbTool.UnitRoleDataDic ?? databaseCache.UnitRoleDataDic;
                                databaseCache.UnitRoleMasteryIdDic = dbTool3.UnitRoleMasteryIdDic ?? databaseCache.UnitRoleMasteryIdDic;
                                databaseCache.UnitRoleSlotDataDic = dbTool3.UnitRoleSlotDataDic ?? databaseCache.UnitRoleSlotDataDic;
                                databaseCache.UnitRoleMasterySlotEnhanceDataDic = dbTool3.UnitRoleMasterySlotEnhanceDataDic ?? databaseCache.UnitRoleMasterySlotEnhanceDataDic;

                                var connectRankDb = dbTool;
                                databaseCache.ConnectRankStatusDic = connectRankDb.ConnectRankStatusDic ?? databaseCache.ConnectRankStatusDic;
                                databaseCache.ConnectRankChartDic = connectRankDb.ConnectRankChartDic ?? databaseCache.ConnectRankChartDic;
                                databaseCache.ConnectRankBonusList = connectRankDb.ConnectRankBonusList ?? databaseCache.ConnectRankBonusList;
                                databaseCache.ConnectRankBoostDic = connectRankDb.ConnectRankBoostDic ?? databaseCache.ConnectRankBoostDic;
                                databaseCache.ConnectRankLimitCommonDic = connectRankDb.ConnectRankLimitCommonDic ?? databaseCache.ConnectRankLimitCommonDic;
                                databaseCache.ConnectRankLimitItemDic = connectRankDb.ConnectRankLimitItemDic ?? databaseCache.ConnectRankLimitItemDic;
                                databaseCache.ConnectRankEnhanceDic = connectRankDb.ConnectRankEnhanceDic ?? databaseCache.ConnectRankEnhanceDic;
                                ConnectRankTempData.Instance.ClearCache();

                                databaseCache.LabyrinthEnhanceNodeDic = dbTool.LabyrinthEnhanceNodeDic ?? databaseCache.LabyrinthEnhanceNodeDic;
                                databaseCache.LabyrinthEnhanceDataDic = dbTool.LabyrinthEnhanceDataDic ?? databaseCache.LabyrinthEnhanceDataDic;
                                databaseCache.LabyrinthEnhanceInformationDic = dbTool.LabyrinthEnhanceInformationDic ?? databaseCache.LabyrinthEnhanceInformationDic;
                                databaseCache.LabyrinthQuestDataDic = dbTool.LabyrinthQuestDataDic ?? databaseCache.LabyrinthQuestDataDic;
                                databaseCache.LabyrinthWaveGroupDataDic = dbTool.LabyrinthWaveGroupDataDic ?? databaseCache.LabyrinthWaveGroupDataDic;
                                databaseCache.LabyrinthDifficultyEnemyDataDic = dbTool.LabyrinthDifficultyEnemyDataDic ?? databaseCache.LabyrinthDifficultyEnemyDataDic;
                                databaseCache.LabyrinthDifficultyEffectDic = dbTool.LabyrinthDifficultyEffectDic ?? databaseCache.LabyrinthDifficultyEffectDic;
                                databaseCache.LabyrinthRelicDic = dbTool.LabyrinthRelicDic ?? databaseCache.LabyrinthRelicDic;
                                databaseCache.LabyrinthRelicEffectGroupDic = dbTool.LabyrinthRelicEffectGroupDic ?? databaseCache.LabyrinthRelicEffectGroupDic;
                                databaseCache.LabyrinthRelicEffectDic = dbTool.LabyrinthRelicEffectDic ?? databaseCache.LabyrinthRelicEffectDic;
                                databaseCache.LabyrinthRelicMarkEffectDic = dbTool.LabyrinthRelicMarkEffectDic ?? databaseCache.LabyrinthRelicMarkEffectDic;
                                databaseCache.RelicActionExecTimeDic = dbTool.RelicActionExecTimeDic ?? databaseCache.RelicActionExecTimeDic;
                                databaseCache.RelicSkillExecTimeDic = dbTool.RelicSkillExecTimeDic ?? databaseCache.RelicSkillExecTimeDic;

                                databaseCache.unitName_cn = dbTool3.Dic10 ?? databaseCache.unitName_cn;
                                databaseCache.exEquipmentName_cn = dbTool3.ExEquipmentName_cn ?? databaseCache.exEquipmentName_cn;
                                databaseCache.skillNameAndDescribe_cn = dbTool3.Dic11 ?? databaseCache.skillNameAndDescribe_cn;
                                databaseCache.skillActionDescribe_cn = dbTool3.Dic12 ?? databaseCache.skillActionDescribe_cn;

                                var masterRef = (Version.useJP ? dbTool2 : dbTool3);
                                databaseCache.masterUnitSkillDataRf = masterRef.masterUnitSkillDataRf ?? databaseCache.masterUnitSkillDataRf;
                                databaseCache.unitStoryEffectDic2 = (masterRef.Pair.Item2 ?? databaseCache.unitStoryEffectDic2);
                                databaseCache.ereductionTable = masterRef.eReductions ?? databaseCache.ereductionTable;
                                databaseCache.rbs = masterRef.rbs ?? databaseCache.rbs;

                                try
                                {
                                    Task.WaitAll(
                                        Task.Run(() =>
                                        {
                                            var filteredUnitRarity = dbTool3.Dic1?.Where(p => p.Key / 10000 == 17 || p.Key == 105701).ToList();
                                            if (filteredUnitRarity != null) databaseCache.unitRarityDic.OverrideWith(filteredUnitRarity);
                                        }),
                                        Task.Run(() =>
                                        {
                                            var filteredUniqueEquipment = dbTool3.Dic9?.Where(p => p.Key / 10000 == 17).ToList();
                                            if (filteredUniqueEquipment != null) databaseCache.uniqueEquipmentDataDic.OverrideWith(filteredUniqueEquipment);
                                        }),
                                        Task.Run(() =>
                                        {
                                            var filteredUniqueEquipment2 = dbTool3.Dic92?.Where(p => p.Key / 10000 == 17).ToList();
                                            if (filteredUniqueEquipment2 != null) databaseCache.uniqueEquipment2DataDic.OverrideWith(filteredUniqueEquipment2);
                                        }),
                                        Task.Run(() =>
                                        {
                                            var filteredSkillData = dbTool3.Dic3?.Where(p => p.Key / 100000 == 17).ToList();
                                            if (filteredSkillData != null) databaseCache.skillDataDic.OverrideWith(filteredSkillData);
                                        }),
                                        Task.Run(() =>
                                        {
                                            var filteredSkillAction = dbTool3.Dic4?.Where(p => p.Key / 10000000 == 17).ToList();
                                            if (filteredSkillAction != null) databaseCache.skillActionDic.OverrideWith(filteredSkillAction);
                                        }),
                                        Task.Run(() =>
                                        {
                                            if (dbTool3.masterUnitSkillDataRf?.dict != null)
                                            {
                                                var filteredMasterUnitSkill = dbTool3.masterUnitSkillDataRf.dict.Where(p => p.Key / 100000 == 17).ToList();
                                                databaseCache.masterUnitSkillDataRf.dict.Remove(1701003); // old jp data
                                                databaseCache.masterUnitSkillDataRf.dict.OverrideWith(filteredMasterUnitSkill);
                                            }
                                        }),
                                        Task.Run(() =>
                                        {
                                            var filteredAllUnitAttackPattern = dbTool3.Dic5?.Where(p => p.Key / 1000000 == 17).ToList();
                                            if (filteredAllUnitAttackPattern != null) databaseCache.allUnitAttackPatternDic.OverrideWith(filteredAllUnitAttackPattern);
                                        }),
                                        Task.Run(() =>
                                        {
                                            var filteredUnitRoleData = dbTool3.UnitRoleDataDic?.Where(p => p.Key / 10000 == 17).ToList();
                                            if (filteredUnitRoleData != null) databaseCache.UnitRoleDataDic.OverrideWith(filteredUnitRoleData);
                                        }),
                                        Task.Run(() =>
                                        {
                                            if (dbTool3.unitExEquips != null)
                                            {
                                                var jita = dbTool3.unitExEquips[170101];
                                                var filteredUnitExEquips = dbTool3.unitExEquips.Where(p => p.Key / 10000 == 17).ToList();
                                                databaseCache.unitExEquips.OverrideWith(filteredUnitExEquips);
                                                databaseCache.unitExEquips[170301] = jita;
                                            }
                                        }),
                                        Task.Run(() => { if (dbCN.Pair.Item1 != null) databaseCache.unitStoryDic.TryAdd(dbCN.Pair.Item1); }),
                                        Task.Run(() => { if (dbCN.Pair.Item2 != null) databaseCache.unitStoryEffectDic.TryAdd(dbCN.Pair.Item2); }),
                                        Task.Run(() => { if (dbCN.Pair.Item3 != null) databaseCache.unitStoryLoveDic.TryAdd(dbCN.Pair.Item3); }),
                                        Task.Run(() => { if (dbCN.TalentQuestBattleEffectDic != null) databaseCache.talentQuestBattleEffectDic.OverrideWith(dbCN.TalentQuestBattleEffectDic); }),
                                        Task.Run(() => { if (dbCN.AbyssBattleEffectDic != null) databaseCache.abyssBattleEffectDic.OverrideWith(dbCN.AbyssBattleEffectDic); }),
                                        Task.Run(() => { if (dbCN.MirageBattleEffectDic != null) databaseCache.mirageBattleEffectDic.OverrideWith(dbCN.MirageBattleEffectDic); }),
                                        Task.Run(() => { if (dbCN.ExtraEffectDataDic != null) databaseCache.extraEffectDataDic.TryAdd(dbCN.ExtraEffectDataDic); }),
                                        Task.Run(() => { if (dbCN.LabyrinthQuestDataDic != null) databaseCache.LabyrinthQuestDataDic.TryAdd(dbCN.LabyrinthQuestDataDic); }),
                                        Task.Run(() => { if (dbCN.LabyrinthWaveGroupDataDic != null) databaseCache.LabyrinthWaveGroupDataDic.TryAdd(dbCN.LabyrinthWaveGroupDataDic); }),
                                        Task.Run(() => { if (dbCN.LabyrinthDifficultyEnemyDataDic != null) databaseCache.LabyrinthDifficultyEnemyDataDic.TryAdd(dbCN.LabyrinthDifficultyEnemyDataDic); }),
                                        Task.Run(() => { if (dbCN.LabyrinthDifficultyEffectDic != null) databaseCache.LabyrinthDifficultyEffectDic.OverrideWith(dbCN.LabyrinthDifficultyEffectDic); }),
                                        Task.Run(() => { if (dbCN.LabyrinthRelicDic != null) databaseCache.LabyrinthRelicDic.OverrideWith(dbCN.LabyrinthRelicDic); }),
                                        Task.Run(() => { if (dbCN.LabyrinthRelicEffectGroupDic != null) databaseCache.LabyrinthRelicEffectGroupDic.TryAdd(dbCN.LabyrinthRelicEffectGroupDic); }),
                                        Task.Run(() => { if (dbCN.LabyrinthRelicEffectDic != null) databaseCache.LabyrinthRelicEffectDic.TryAdd(dbCN.LabyrinthRelicEffectDic); }),
                                        Task.Run(() => { if (dbCN.LabyrinthRelicMarkEffectDic != null) databaseCache.LabyrinthRelicMarkEffectDic.OverrideWith(dbCN.LabyrinthRelicMarkEffectDic); }),
                                        Task.Run(() => { if (dbCN.LabyrinthEnhanceInformationDic != null) databaseCache.LabyrinthEnhanceInformationDic.OverrideWith(dbCN.LabyrinthEnhanceInformationDic); }),
                                        Task.Run(() => { if (dbCN.RelicActionExecTimeDic != null) databaseCache.RelicActionExecTimeDic.TryAdd(dbCN.RelicActionExecTimeDic); }),
                                        Task.Run(() => { if (dbCN.RelicSkillExecTimeDic != null) databaseCache.RelicSkillExecTimeDic.TryAdd(dbCN.RelicSkillExecTimeDic); })
                                    );
                                }
                                catch (AggregateException ex)
                                {
                                    Debug.LogError($"补充数据加载部分失败（非致命）: {ex.InnerExceptions?.FirstOrDefault()?.Message}");
                                }

                                // databaseCache.unitRarityDic.TryAdd(dbCN.Dic1);
                                
                                
                                // databaseCache.unitExEquips.TryAdd(dbCN.unitExEquips);
                                // databaseCache.skillDataDic.TryAdd(dbCN.Dic3);
                                // databaseCache.skillActionDic.TryAdd(dbCN.Dic4);
                                // databaseCache.allUnitAttackPatternDic.TryAdd(dbCN.Dic5);

                                // databaseCache.unitRarityDic[170101].ChangeRankData(databaseCache.unitRarityDic[105701].GetRankData());
                                // databaseCache.unitRarityDic[170201].ChangeRankData(databaseCache.unitRarityDic[107601].GetRankData());

                                try
                                {
                                    if (dbTool != dbTool2)
                                        Task.WaitAll(
                                            Task.Run(() => { if (dbTool2.Dic3 != null) databaseCache.skillDataDic.OverrideWith(dbTool2.Dic3, k => k > 2000000); }),
                                            Task.Run(() => { if (dbTool2.Dic4 != null) databaseCache.skillActionDic.OverrideWith(dbTool2.Dic4, k => k > 200000000); }),
                                            Task.Run(() => { if (dbTool2.Dic5 != null) databaseCache.allUnitAttackPatternDic.OverrideWith(dbTool2.Dic5); }),
                                            Task.Run(() => { if (dbTool2.Dic6 != null) databaseCache.guildEnemyDatas.OverrideWith(dbTool2.Dic6); }),
                                            Task.Run(() => { if (dbTool2.Dic7 != null) databaseCache.enemyMPartsDic.OverrideWith(dbTool2.Dic7); }),
                                            Task.Run(() => { if (dbTool2.Dic2 != null) databaseCache.enemyDataDic.OverrideWith(dbTool2.Dic2); }),
                                            Task.Run(() => { if (dbTool2.TowerEnemyParameterDic != null) databaseCache.towerEnemyParameterDic.OverrideWith(dbTool2.TowerEnemyParameterDic); }),
                                            Task.Run(() => { if (dbTool2.TalentQuestEnemyParameterDic != null) databaseCache.talentQuestEnemyParameterDic.OverrideWith(dbTool2.TalentQuestEnemyParameterDic); }),
                                            Task.Run(() => { if (dbTool2.AbyssEnemyParameterDic != null) databaseCache.abyssEnemyParameterDic.OverrideWith(dbTool2.AbyssEnemyParameterDic); }),
                                            Task.Run(() => { if (dbTool2.LabyrinthEnemyParameterDic != null) databaseCache.labyrinthEnemyParameterDic.OverrideWith(dbTool2.LabyrinthEnemyParameterDic); }),
                                            Task.Run(() => { if (dbTool2.MirageEnemyParameterDic != null) databaseCache.mirageEnemyParameterDic.OverrideWith(dbTool2.MirageEnemyParameterDic); }),
                                            Task.Run(() => { if (dbTool2.ExtraEffectDataDic != null) databaseCache.extraEffectDataDic.TryAdd(dbTool2.ExtraEffectDataDic); }),
                                            Task.Run(() => { if (dbTool2.LabyrinthQuestDataDic != null) databaseCache.LabyrinthQuestDataDic.TryAdd(dbTool2.LabyrinthQuestDataDic); }),
                                            Task.Run(() => { if (dbTool2.LabyrinthDifficultyEnemyDataDic != null) databaseCache.LabyrinthDifficultyEnemyDataDic.TryAdd(dbTool2.LabyrinthDifficultyEnemyDataDic); }),
                                            Task.Run(() => { if (dbTool2.LabyrinthRelicDic != null) databaseCache.LabyrinthRelicDic.TryAdd(dbTool2.LabyrinthRelicDic); }),
                                            Task.Run(() => { if (dbTool2.LabyrinthRelicEffectGroupDic != null) databaseCache.LabyrinthRelicEffectGroupDic.TryAdd(dbTool2.LabyrinthRelicEffectGroupDic); }),
                                            Task.Run(() => { if (dbTool2.LabyrinthRelicEffectDic != null) databaseCache.LabyrinthRelicEffectDic.TryAdd(dbTool2.LabyrinthRelicEffectDic); }),
                                            Task.Run(() => { if (dbTool2.LabyrinthRelicMarkEffectDic != null) databaseCache.LabyrinthRelicMarkEffectDic.TryAdd(dbTool2.LabyrinthRelicMarkEffectDic); }),
                                            Task.Run(() => { if (dbTool2.RelicActionExecTimeDic != null) databaseCache.RelicActionExecTimeDic.TryAdd(dbTool2.RelicActionExecTimeDic); }),
                                            Task.Run(() => { if (dbTool2.RelicSkillExecTimeDic != null) databaseCache.RelicSkillExecTimeDic.TryAdd(dbTool2.RelicSkillExecTimeDic); })
                                        );
                                }
                                catch (AggregateException ex) { Debug.LogError($"补充数据覆盖部分失败（非致命）: {ex.InnerExceptions?.FirstOrDefault()?.Message}"); }
                                try
                                {
                                    Task.WaitAll(
                                        Task.Run(() => { if (dbTool3.Dic2 != null) databaseCache.enemyDataDic.Override2With(dbTool3.Dic2); }),
                                        Task.Run(() => { if (dbTool3.Dic3 != null) databaseCache.skillDataDic.Override2With(dbTool3.Dic3); }),
                                        Task.Run(() => { if (dbTool3.Dic4 != null) databaseCache.skillActionDic.Override2With(dbTool3.Dic4); }),
                                        Task.Run(() => { if (dbTool3.TowerEnemyParameterDic != null) databaseCache.towerEnemyParameterDic.Override2With(dbTool3.TowerEnemyParameterDic); }),
                                        Task.Run(() => { if (dbTool3.TalentQuestEnemyParameterDic != null) databaseCache.talentQuestEnemyParameterDic.Override2With(dbTool3.TalentQuestEnemyParameterDic); }),
                                        Task.Run(() => { if (dbTool3.AbyssEnemyParameterDic != null) databaseCache.abyssEnemyParameterDic.Override2With(dbTool3.AbyssEnemyParameterDic); }),
                                        Task.Run(() => { if (dbTool3.LabyrinthEnemyParameterDic != null) databaseCache.labyrinthEnemyParameterDic.Override2With(dbTool3.LabyrinthEnemyParameterDic); }),
                                        Task.Run(() => { if (dbTool3.MirageEnemyParameterDic != null) databaseCache.mirageEnemyParameterDic.Override2With(dbTool3.MirageEnemyParameterDic); }),
                                        Task.Run(() => { if (dbTool3.TalentSkillNodeDic != null) databaseCache.talentSkillNodeDic.Override2With(dbTool3.TalentSkillNodeDic); }),
                                        Task.Run(() => { if (dbTool3.TeamSkillNodeDic != null) databaseCache.teamSkillNodeDic.Override2With(dbTool3.TeamSkillNodeDic); }),
                                        Task.Run(() => { if (dbTool3.MirageNemesisAreaDic != null) databaseCache.mirageNemesisAreaDic.Override2With(dbTool3.MirageNemesisAreaDic); })
                                    );
                                }
                                catch (AggregateException ex) { Debug.LogError($"补充数据覆盖部分失败（非致命）: {ex.InnerExceptions?.FirstOrDefault()?.Message}"); }
                            },
                        () =>
                        {
                            dbTool.CloseDB();
                            if (!ReferenceEquals(dbTool2ForCleanup, dbTool))
                                dbTool2ForCleanup.CloseDB();
                            if (!ReferenceEquals(dbTool3, dbTool) && !ReferenceEquals(dbTool3, dbTool2ForCleanup))
                                dbTool3.CloseDB();
                        });
                    tasks.Add(dbDataTask);
                }

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

                // JSON数据加载计时
                var jsonLoadStopwatch = System.Diagnostics.Stopwatch.StartNew();
                // 更新 UI：开始加载 JSON 数据
                var jsonLoadTasks = new[]
                {
                    // Task.Run(() => new { Key = "skillTime", Data = LoadJsonDatas("Datas/unitSkillTimeDic") }),
                    Task.Run(() => new { Key = "nickName", Data = LoadJsonDatas("Datas/UnitNickNameDic") }),
                    Task.Run(() => new { Key = "nickName2", Data = LoadJsonDatas("Datas/nickname") }),
                    Task.Run(() => new { Key = "firearm", Data = LoadJsonDatas("Datas/AllUnitFirearmData") })
                };

                var jsonResults = await Task.WhenAll(jsonLoadTasks);

                // JSON反序列化计时
                var deserializationTasks = new List<Task>();

                foreach (var result in jsonResults)
                {
                    switch (result.Key)
                    {
                        case "skillTime":
                            deserializationTasks.Add(Task.Run(() =>
                                allUnitSkillTimeDataDic = JsonConvert.DeserializeObject<Dictionary<int, UnitSkillTimeData>>(result.Data)));
                            break;
                        case "nickName":
                            deserializationTasks.Add(Task.Run(() =>
                                unitNickNameDic = JsonConvert.DeserializeObject<Dictionary<int, string>>(result.Data)));
                            break;
                        case "nickName2":
                            deserializationTasks.Add(Task.Run(() =>
                                unitNickNameDic2 = JsonConvert.DeserializeObject<Dictionary<int, string>>(result.Data)));
                            break;
                        case "firearm":
                            if (!string.IsNullOrEmpty(result.Data))
                            {
                                deserializationTasks.Add(Task.Run(() =>
                                    firearmData = JsonConvert.DeserializeObject<AllUnitFirearmData>(result.Data)));
                            }
                            break;
                    }
                }

                tasks.AddRange(deserializationTasks);

                jsonLoadStopwatch.Stop();
                await Task.WhenAll(tasks);
                Debug.Log($"加载json完成，耗时: {jsonLoadStopwatch.ElapsedMilliseconds}ms");

                if (!loadedCache)
                {
                    databaseSources = GetDatabaseSources();
                    var rebuiltFingerprintAvailable = await Task.Run(() =>
                        DatabaseCacheContract.TryComputeSourceFingerprint(
                            databaseSources,
                            out sourceFingerprint,
                            out fingerprintStatus));
                    if (!rebuiltFingerprintAvailable)
                    {
                        throw new InvalidDataException($"数据库已加载，但无法建立缓存来源契约: {fingerprintStatus}");
                    }
                    if (!string.Equals(
                            preparedSourceFingerprint,
                            sourceFingerprint,
                            StringComparison.Ordinal))
                    {
                        throw new InvalidDataException("数据库来源在加载过程中发生变化，拒绝发布不一致缓存");
                    }

                    databaseCache.cacheSchemaVersion = DatabaseCacheContract.CurrentSchemaVersion;
                    databaseCache.cacheBuildComplete = true;
                    databaseCache.databaseSourceFingerprint = sourceFingerprint;
                    if (!DatabaseCacheContract.TryValidate(databaseCache, sourceFingerprint, out var validationReason))
                    {
                        databaseCache.cacheBuildComplete = false;
                        throw new InvalidDataException($"数据库加载结果不完整: {validationReason}");
                    }

                    if (canPublishReplacementCache)
                        SaveToCache(sourceFingerprint);
                    else
                        Debug.LogWarning("数据库已在内存中完整加载，但因原坏缓存无法保留，本次跳过新缓存发布");
                }


                // 角色数据加载计时
                try { wait?.SetMessage("加载角色数据..."); } catch { }
                LoadUnitData();

                ApplyPlayerSettingLimits(setLevelsToMaximum: true);
                LoadFinished = true;
                CreateShowUnitIDS();
                wait?.Close();
                
                // 计时结束并输出
                stopwatch.Stop();
                var elapsedTime = stopwatch.ElapsedMilliseconds;
                Debug.Log($"LoadAsync 执行完成，耗时: {elapsedTime}ms ({elapsedTime / 1000.0:F2}s)");
            }
            catch (Exception e)
            {
                // 异常情况下也要停止计时并输出
                stopwatch.Stop();
                var elapsedTime = stopwatch.ElapsedMilliseconds;
                Debug.LogError($"LoadAsync 执行失败，耗时: {elapsedTime}ms，错误: {e}");
                
                try { wait?.SetMessage($"加载时发生致命错误:{e}，请重新打开！"); } catch { }
                throw;
            }
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

        public void LoadPlayerSettings()
        {
            try
            {
                playerSetting = SaveManager.Load<PlayerSetting>() ?? new PlayerSetting();
            }
            catch
            {
                playerSetting = new PlayerSetting();
            }
            ApplyPlayerSettingLimits(setLevelsToMaximum: true);
        }
        private void ApplyPlayerSettingLimits(bool setLevelsToMaximum = false)
        {
            playerSetting.ApplyPlayerProcessLimit(PlayerProcessLimit);
            playerSetting.ApplyPlayerLevelLimit(PlayerLevelLimit, setLevelsToMaximum);
            playerSetting.ApplyUniqueEqLevelLimit(UniqueEqLevelLimit, setLevelsToMaximum);
            if (PlayerLevelText != null)
            {
                PlayerLevelText.text = playerSetting.playerLevel.ToString();
            }
        }
        public void ReLoadPlayerSetting()
        {
            playerSetting = SaveManager.Load<PlayerSetting>() ?? new PlayerSetting();
            ApplyPlayerSettingLimits(setLevelsToMaximum: true);
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
            ApplyPlayerSettingLimits();
            SaveManager.Save(playerSetting);
        }
        public void DeletePlayerData()
        {
            WindowMessage("删除失败！（功能已删除）");
        }
        public void WindowMessage(string word)
        {
#if UNITY_5_3_OR_NEWER
            if (windowmassageIE == null)
            {
                windowmassageIE = StartCoroutine(WindowMessage_start(word));
            }
#else
            Console.WriteLine(word);
#endif
        }
        public void WindowConfigMessage(string word, SystemWindowMessage.configDelegate configDelegate, SystemWindowMessage.configDelegate cancelDelegate = null)
        {
#if UNITY_5_3_OR_NEWER
            GameObject a = Instantiate(SystemWindowMessagePerferb);
            a.transform.SetParent(LatestUIback.transform, false);
            a.transform.localPosition = new Vector3();
            a.transform.localScale = new Vector3(1, 1, 1);
            a.GetComponent<SystemWindowMessage>().SetWindowMassage(word, configDelegate, cancelDelegate);
#else
            Console.WriteLine(word);
            configDelegate?.Invoke();
#endif
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
            IsGuildBattle = false;
            CharacterManager.SwitchPage(1);
            stayPage = StayPage.character;
        }

        public void BattleButton()
        {
            TurnAllPageOff();
            IsGuildBattle = false;
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
            IsGuildBattle = false;
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
            IsGuildBattle = true;
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
        public bool TryGetEnemyParameter(int enemyId, out EnemyData enemyData)
        {
            return EnemyDataDic.TryGetValue(enemyId, out enemyData)
                || LabyrinthEnemyParameterDic.TryGetValue(enemyId, out enemyData)
                || MirageEnemyParameterDic.TryGetValue(enemyId, out enemyData)
                || AbyssEnemyParameterDic.TryGetValue(enemyId, out enemyData)
                || TalentQuestEnemyParameterDic.TryGetValue(enemyId, out enemyData)
                || TowerEnemyParameterDic.TryGetValue(enemyId, out enemyData);
        }
        public string GetUnitNickName(int unitid)
        {
            if (unitid >= 400000 && unitid <= 499999)
            {
                if (UnitRarityDic.TryGetValue(unitid, out var data))
                    return data.unitName;
                return $"召唤物";
            }
            if (EnemyDataDic.TryGetValue(unitid, out var data2))
                return data2.name;
            if (TowerEnemyParameterDic.TryGetValue(unitid, out var data3))
                return data3.name;
            if (TalentQuestEnemyParameterDic.TryGetValue(unitid, out var data4))
                return data4.name;
            if (AbyssEnemyParameterDic.TryGetValue(unitid, out var data5))
                return data5.name;
            if (MirageEnemyParameterDic.TryGetValue(unitid, out var data6))
                return data6.name;
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
        public List<int> GetTalentIds(int unitid)
        {

            if (UnitRarityDic.TryGetValue(unitid, out var data))
                return data.detailData.talent_id;
            return new List<int>();
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
#if UNITY_5_3_OR_NEWER
            if (PlayerPrefs.HasKey("Copyed"))
            {
                return;
            }
            DirectoryCopy(Application.streamingAssetsPath, Application.persistentDataPath);
            PlayerPrefs.SetInt("Copyed", 1);
            PlayerPrefs.Save();
#endif
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

#if UNITY_5_3_OR_NEWER
            //yield return new WaitForSeconds(1.5f);
            var async = SceneManager.LoadSceneAsync("BattleScene");
            async.allowSceneActivation = true;
            while (!async.isDone)
            {
                yield return null;
            }
#else
            yield return null;
#endif
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
            // 优化：添加文件内容缓存，避免重复读取相同文件
            if (_dataCache.TryGetValue(path, out object cachedContent))
            {
                return (string)cachedContent;
            }

            string filePath = GetSaveDataPath() + "/" + path + ".json";
            string content;
            
#if PLATFORM_ANDROID
            var reader = new WWW(filePath);
            while (!reader.isDone) {Thread.Sleep(0);}
            content = reader.text;
#else
            content = File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
#endif
            
            // 缓存文件内容
            if (!string.IsNullOrEmpty(content))
            {
                lock (_dataCache)
                {
                    _dataCache.TryAdd(path, content);
                }
            }
            
            return content;
        }

        public WaitUI OpenWaitUI()
        {
#if UNITY_5_3_OR_NEWER
            GameObject p = Instantiate(LoadingPagePrefab, GameObject.Find("Canvas")?.transform);
            return p.GetComponent<WaitUI>();
#else
            return null;
#endif
        }
        public static Sprite LoadSourceSprite(string relativePath)
        {
#if UNITY_5_3_OR_NEWER
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
#else
            return null;
#endif
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
            catch (Exception)
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
        public List<extra_effect_unit_group> GetListWithGroupId(int groupId)
        {
            return ExtraEffectUnitGroupDic.ContainsKey(groupId) ? ExtraEffectUnitGroupDic[groupId] : null;
        }
#if UNITY_5_3_OR_NEWER
        public async Task SendPostRequestAsync2(Action onComplete)
        {
            const string app_ver = "11.7.2"; // change this when the app version changes
            using UnityWebRequest www = new UnityWebRequest(Url2, UnityWebRequest.kHttpVerbPOST);
            byte[] postData = System.Text.Encoding.UTF8.GetBytes("{\"viewer_id\": \"0\"}");

            // 设置必要请求头
            www.uploadHandler = new UploadHandlerRaw(postData);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept-Encoding", "gzip");
            www.SetRequestHeader("User-Agent", "Dalvik/2.1.0 (Linux, U, Android 5.1.1, PCRT00 Build/LMY48Z)");
            www.SetRequestHeader("X-Unity-Version", "2021.3.20f1c1");
            www.SetRequestHeader("APP-VER", app_ver);
            www.SetRequestHeader("BATTLE-LOGIC-VERSION", "4");
            www.SetRequestHeader("DEVICE", "2");
            www.SetRequestHeader("DEVICE-NAME", "OPPO PCRT00");
            www.SetRequestHeader("EXCEL-VER", "1.0.0");
            www.SetRequestHeader("GRAPHICS-DEVICE-NAME", "Adreno (TM) 640");
            www.SetRequestHeader("IP-ADDRESS", "10.0.2.15");
            www.SetRequestHeader("LOCALE", "CN");
            www.SetRequestHeader("PLATFORM", "2");
            www.SetRequestHeader("PLATFORM-ID", "2");
            www.SetRequestHeader("CHANNEL-ID", "1");
            www.SetRequestHeader("RES-KEY", "ab00a0a6dd915a052a2ef7fd649083e5");
            www.SetRequestHeader("RES-VER", "10002200");

            // 统一异步等待机制
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            www.SendWebRequest().completed += (AsyncOperation op) =>
            {
                try
                {
                    if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.LogError($"请求失败: {www.error}");
                        truthVersion = "获取失败（重试）";
                    }
                    else
                    {
                        var raw = www.downloadHandler.text;
                        // Debug.Log($"Response:\n{raw}");

                        try
                        {
                            var response = JsonConvert.DeserializeObject<BcrResponse>(raw);
                            truthVersion = response?.data?.manifest_ver ?? "获取失败（重试）";
                            // Debug.Log($"manifest_ver = {truthVersion}");
                        }
                        catch (Exception jsonEx)
                        {
                            // Debug.LogError($"JSON解析失败: {jsonEx}");
                            truthVersion = "获取失败（重试）";
                        }
                    }
                }
                finally
                {
                    onComplete?.Invoke();
                    tcs.TrySetResult(true);
                }
            };

            await tcs.Task;
        }

        public async Task SendPostRequestAsync(Action onComplete)
        {
            await SendPostRequestAsync2(onComplete);
            return;

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
#else
        public async Task SendPostRequestAsync(Action onComplete)
        {
            const string app_ver = "99.9.9";
            var httpClient = new HttpClient();

            // 构造 POST Body
            var jsonBody = "{\"viewer_id\": \"0\"}";
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // 添加 Headers
            content.Headers.ContentType.CharSet = "UTF-8";

            var request = new HttpRequestMessage(HttpMethod.Post, Url2)
            {
                Content = content
            };

            request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip");
            request.Headers.TryAddWithoutValidation("User-Agent", "Dalvik/2.1.0 (Linux, U, Android 5.1.1, PCRT00 Build/LMY48Z)");
            request.Headers.TryAddWithoutValidation("X-Unity-Version", "2021.3.20f1c1");
            request.Headers.TryAddWithoutValidation("APP-VER", app_ver);
            request.Headers.TryAddWithoutValidation("BATTLE-LOGIC-VERSION", "4");
            request.Headers.TryAddWithoutValidation("DEVICE", "2");
            request.Headers.TryAddWithoutValidation("DEVICE-NAME", "OPPO PCRT00");
            request.Headers.TryAddWithoutValidation("EXCEL-VER", "1.0.0");
            request.Headers.TryAddWithoutValidation("GRAPHICS-DEVICE-NAME", "Adreno (TM) 640");
            request.Headers.TryAddWithoutValidation("IP-ADDRESS", "10.0.2.15");
            request.Headers.TryAddWithoutValidation("LOCALE", "CN");
            request.Headers.TryAddWithoutValidation("PLATFORM", "2");
            request.Headers.TryAddWithoutValidation("PLATFORM-ID", "2");
            request.Headers.TryAddWithoutValidation("CHANNEL-ID", "1");
            request.Headers.TryAddWithoutValidation("RES-KEY", "ab00a0a6dd915a052a2ef7fd649083e5");
            request.Headers.TryAddWithoutValidation("RES-VER", "10002200");

            try
            {
                var responseMessage = await httpClient.SendAsync(request);
                var raw = await responseMessage.Content.ReadAsStringAsync();


                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception("bad status code: " + responseMessage.StatusCode);
                }
                else
                {
                    var response = JsonConvert.DeserializeObject<BcrResponse>(raw);
                    truthVersion = response.data.manifest_ver;

                    if (truthVersion == null) throw new Exception("manifest ver is null, resp: " + raw);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"请求异常: {ex}");
                truthVersion = "获取失败（重试）";
            }
            finally
            {
                onComplete?.Invoke();
            }
        }
#endif
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
        public bool useRemote = true;
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
        public long MaxDamage
        {
            get
            {
                if (resultDatas.Count == 0)
                    return 0;

                long max = resultDatas[0].currentDamage;
                for (int i = 1; i < resultDatas.Count; i++)
                {
                    if (resultDatas[i].currentDamage > max)
                        max = resultDatas[i].currentDamage;
                }
                return max;
            }
        }

        public long MinDamage
        {
            get
            {
                if (resultDatas.Count == 0)
                    return 0;

                long min = resultDatas[0].currentDamage;
                for (int i = 1; i < resultDatas.Count; i++)
                {
                    if (resultDatas[i].currentDamage < min)
                        min = resultDatas[i].currentDamage;
                }
                return min;
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
    [Serializable]
    public class OnceResultData
    {
        public int id { get; set; }
        public int randomSeed { get; set; }
        public long currentDamage { get; set; }
        public long criticalEX { get; set; }
        public long exceptDamage { get; set; }
        public int backTime { get; set; }
        public string backTimeText { get; set; }
        public List<string> warnings { get; set; } = new List<string>();
        public eBattleResult BattleResult { get; set; }

        public string GetDetail()
        {
            string result = "伤害：" + currentDamage + "\n暴击额外伤害：" + criticalEX + "\n期望伤害（触盾偏高）：" + exceptDamage + "\n随机种子：" + randomSeed;
            if (backTime > 0) result += backTimeText;
            if (warnings.Count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i < warnings.Count)
                        result += "\n" + warnings[i];
                }
                if (warnings.Count > 4)
                    result += "\n等" + warnings.Count + "个错误";
            }
            return result;
        }
    }
}
