// Decompiled with JetBrains decompiler
// Type: Elements.Battle.BattleManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scrips;
using Cute;

using PCRCaculator;
using PCRCaculator.Guild;
using UnityEngine;
using UnityEngine.Video;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;
//using Elements.Data;
//using Elements.Uek;

//using UnityStandardAssets.ImageEffects;

namespace Elements.Battle
{
    public class BattleManager : MonoBehaviour, ISingletonField, BattleManager_Time, BattleManagerForActionController
    {
        private int skillTargetCacheKey = Environment.TickCount;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void QueueUpdateSkillTarget()
        {
            ++skillTargetCacheKey;
        }

        public ScriptManager scriptMgr;
        public static int randomCounter;
        public AutoubManager ubmanager = new AutoubManager();
        public static BattleManager Instance;
        public const int POST_RESULT_TYPE_LOSE = 0;
        public const int POST_RESULT_TYPE_RESIGN = 1;
        public const int POST_RESULT_TYPE_WIN = 2;
        private const float ZOOM_SPAN = 0.3f;
        private const float ZOOM_SCALE = 1.45f;
        private const float RANDOM_DIGID = 1000f;
        private const float POINT_WAIT_TIME = 3f;

        public enum eSetStatus
        {
            MUST_NOT,
            MAY,
            MUST,
        }

        public List<(int, eSetStatus[])> setStatus = new List<(int, eSetStatus[])>();
        
        /*[SerializeField]
        private UITexture stageBgTex;
        [SerializeField]
        private UITexture stageForegroundTex;*/
        [SerializeField]
        private Transform unitParentTransform;
        [SerializeField]
        private Transform enemyParentTransform;
        [SerializeField]
        private Camera playCamera;
        [SerializeField]
        private Transform bgTransform;
        [SerializeField]
        private float energyGainValueSkillFront;
        [SerializeField]
        private float energyGainValueSkillMiddle;
        [SerializeField]
        private float energyGainValueSkillBack;
        [SerializeField]
        private float energyStackValueDefeat = 200;//300f;
        [SerializeField]
        private float energyStackRatioDamagedFront = 0.5F;//0.5f;
        [SerializeField]
        private float energyStackRatioDamagedMiddle = 0.5F;//0.75f;
        [SerializeField]
        private float energyStackRationDamageBack = 0.5f;
        /*[SerializeField]
        private MotionBlur motionBlur;
        [SerializeField]
        private BlurOptimized playCameraBlurOptimized;
        private ViewBattle viewBattle;*/
        //private BattleEffectManager battleEffectManager;
        public float deltaTimeAccumulated;
        private float cutinSkipTimeAccumulated;
        private bool isToPauseOnFrameEnd;
        private bool isToResumeOnFrameEnd;
        private bool idleonlyDone;
        private bool isAllActionDone = true;
        private bool playerAllDead;
        private bool otherAllDead;
        private List<SkillEffectCtrl> effectUpdateList = new List<SkillEffectCtrl>();
        private Vector3 playCameraPos0;
        private UnitCtrl voiceOnUnit;
        private bool isAdminChanging;
        private Random.State tempRandomState;
        private Dictionary<int, UnitCtrl> currentEnemyUnitCtrlDictionary = new Dictionary<int, UnitCtrl>();
        private List<UnitCtrl>[] allWaveEnemyUnitCtrlListArray;
        public UnitCtrl BossUnit;
        private List<Vector2> questWinPositionList = new List<Vector2>();
        private List<BattleSpineController> unitSpineControllerList = new List<BattleSpineController>();
        private bool isUpdateFrameExecuted;
        //private TowerTempData towerTempData;
        //private SpaceBattleTempData spaceBattleTempData;
        //private ReplayTempData replayTempData;
        private bool isSkipCoroutineRunning;

        private float startRemainTime;

        private bool isPauseTimeLimit;
        public bool IsPlayCutin;
        public float BattleStartPos = 560f;
        private int noneVoiceCount;
        private int lastVoiceId;
        public float LatestSkillVoiceTime = -0.5f;
        public const float SKILL_VOICE_MIN_DELAY = 0.5f;
        private eBattleClearType clearType;
        public bool IsPausingEffectSkippedInThisFrame;
        //[NonSerialized]
        //public bool OnlyAutoClearFlag;
        public float ActionStartTimeCounter;
        private int currentWaveOffset;
        /*private ViewManager viewManager = ManagerSingleton<ViewManager>.Instance;
        private BlockLayerManager blockLayerManager = ManagerSingleton<BlockLayerManager>.Instance;
        private ResourceManager resourceManager = ManagerSingleton<ResourceManager>.Instance;
        private ApiManager apiManager = ManagerSingleton<ApiManager>.Instance;
        private SoundManager soundManager = ManagerSingleton<SoundManager>.Instance;
        private DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
        private LoadingManager loadingManager = ManagerSingleton<LoadingManager>.Instance;*/
        private TempData tempData;// = Singleton<EHPLBCOOOPK>.Instance;
        /*[SerializeField]
        private UIPanel skillExeScreen;*/
        private bool isStarted;
        private float blackoutTime;
        private float blackoutTimeCounter;
        private float fadeoutDuration = 0.5f;
        private Action onEndFadeOut;
        /*[SerializeField]
        private UITexture skillExeScreenTexture;*/
        private float startalpha;
        private const float TOTAL_DELAY = 0.06f;
        /*private bool startBonusAppearCalled;

        private List<OMNGIBGDMFJ> bonusEnemy;

        private Dictionary<PBAIJIBJPCO, List<CNBALNPBPGF>> bonusDictionary;

        private bool isDefeatBonusOnly;*/
        private const float BOSS_TIME_UP_UNIT_FADEOUT_TIME = 0.5f;
        private const float PERCENT_DIGIT = 100f;
        public const int MILI_DIGID = 1000;
        private const float RESULT_POS_DIFF_MIN = 0.1f;
        private bool timeUpWin;
        private const int PERMILLE = 1000;
        private const float ALL_ACTION_DONE_MAX_COUNT = 10f;
        private const int VISUAL_CHANGE = 1;
        private const int STATE_ICON_NUM = 10;
        private const int STATE_ICON_NUM_FOR_SPECIAL_BATTLE = 6;
        /*[SerializeField]
        private UIPanel backgroundPanel;
        [SerializeField]
        private UIPanel foregroundPanel;*/
        //private MLDKPCCPIOC battleProcessor;
        //private GJNIHENNINA battleLog;
        public BattleCameraEffect battleCameraEffect;
        public BattleEffectPool battleEffectPool;
        public BattleSpeedManager battleTimeScale;
        //private Yggdrasil<BattleManager> singletonTree;
        private BattleUnionBurstController battleUnionBurstController;
        //private ISingletonNodeForInstanceEditing battleManagerNode;
        private bool isValid = true;
        //private StoryBattleData storyBattleDataPrefab;
        private bool forcePauseNoDialogUpdate;
        private GameObject focusObject;

        public GameObject princessFormMoviePlayer;

        public VideoPlayer PrincessFormMoviePlayer => princessFormMoviePlayer.GetComponent<VideoPlayer>();

        //private CustomUIButton focusButton;
        //private List<EventDelegate> cacheEventDelegate;

        public float EnergyGainValueSkillFront => energyGainValueSkillFront;

        public float EnergyGainValueSkillMiddle => energyGainValueSkillMiddle;

        public float EnergyGainValueSkillBack => energyGainValueSkillBack;

        public float EnergyStackValueDefeat => energyStackValueDefeat;

        public float EnergyStackRatioDamagedFront => energyStackRatioDamagedFront;

        public float EnergyStackRatioDamagedMiddle => energyStackRatioDamagedMiddle;

        public float EnergyStackRationDamageBack => energyStackRationDamageBack;

        public Vector3 LNKFACBHNBC => playCameraPos0;

        public bool PMPACJOAECP => isAdminChanging;

        public void SetAdminChanging(bool AAFJOOCLABG) => isAdminChanging = AAFJOOCLABG;

        public UnitCtrl GetBossUnit() => BossUnit;

        private float StoryStartTime { get; set; }

        private int StartStoryId { get; set; }

        private Dictionary<int, List<AbnormalStateDataBase>> FieldDataDictionary { get; set; }

        //private SaveDataManager.eBattleMaxSpeed ELFKGBFMOIE { get; set; } = SaveDataManager.eBattleMaxSpeed.QUADRUPLE;

        public int FICLPNJNOEP { get; private set; }

        //public SpBattleShieldEffect FICPCPIFKCD { get; set; }

        public List<int> NOMJJDDCBAN { get; private set; }

        public int FrameCount { get; private set; }

        public eBattleGameState GameState { get; private set; }

        //public UnitUiCtrl UnitUiCtrl => this.viewBattle.UnitUiCtrl;

        public float BattleLeftTime { get; set; }

        public int GetMiliLimitTime() => (int)(BattleLeftTime * 1000.0);

        public int CurrentWave { get; set; }

        public void SetPauseTimeLimit(bool AAFJOOCLABG) => isPauseTimeLimit = AAFJOOCLABG;

        public List<UnitCtrl> LPBCBINDJLJ { get; set; }

        public List<UnitCtrl> UnitList { get; set; }

        public int GetUnitCtrlLength() => UnitList.Count;

        public UnitCtrl GetUnitCtrl(int DNGOJHOHHMF) => UnitList[DNGOJHOHHMF];

        public List<UnitCtrl> EnemyList { get; set; }

        public int GetEnemyCtrlLength() => EnemyList.Count;

        public UnitCtrl GetEnemyCtrl(int DNGOJHOHHMF) => EnemyList[DNGOJHOHHMF];

        public UnitCtrl FindEnemy(int unitId) => EnemyList.Find((a => a.UnitId == unitId));

        public bool IsDefenceReplayMode { get; set; }

        public bool HasBoss { get; set; }

        public UnitCtrl DecoyUnit { get; set; }

        public UnitCtrl DecoyEnemy { get; set; }

        public UnitCtrl CKFFOKKCOLJ { get; set; }

        public UnitCtrl JLHBICJKJEJ { get; set; }

        public eBattleCategory BattleCategory { get; private set; }

        public CoroutineManager CoroutineManager { get; private set; }

        public eChargeSkillTurn ChargeSkillTurn { get; set; }

        public int PPNHIMOOKDD { get; set; }

        public int FameRate { get; private set; }

        public float DeltaTime_60fps { get; private set; }

        public bool GetIsPlayCutin() => IsPlayCutin;

        public HashSet<long> PBCLBKCKHAI { get; private set; }

        public eBattleResult battleResult { get; private set; }

        public void SetBattleResult(eBattleResult CCNMAFPNDNB) => battleResult = CCNMAFPNDNB;

        public int AMNJIJHNJGC { get; set; }

        public int NHFBEJMHJJJ { get; set; }

        public bool PJLABNLCPPE { get; set; }

        public int PPHMHBJAHBN { get; set; }

        public int OIHNMACDFBN { get; set; }

        public int KPLMNGFMBKF { get; set; }

        public bool IsFramePause { get; set; }

        public bool isUBExecing { get; set; }

        public bool isPrincessFormSkill { get; set; }

        public HashSet<UnitCtrl> BlackoutUnitTargetList { get; private set; }

        public int GetBlackoutUnitTargetLength() => BlackoutUnitTargetList.Count;

        public Dictionary<string, GameObject> DAIFDPFABCM { get; private set; }

        public bool AMLOLHFMOPP { get; private set; }

        //public List<RewardData> RewardList { get; private set; }

        public bool IsBossBattle { get; private set; }

        public bool BKLOEOBLEDB => BattleCategory == eBattleCategory.CLAN_BATTLE;

        public bool OPCIJEAJPAG => BattleCategory == eBattleCategory.GLOBAL_RAID || BattleCategory == eBattleCategory.CLAN_BATTLE;

        public int IJGJOGNIGLH { get; set; }

        public int EBDCFPAJFOK { get; set; }

        public bool IJBLCADFNJP => BattleCategory == eBattleCategory.HATSUNE_BATTLE;

        public bool DMALFANMBMM => BattleCategory == eBattleCategory.HATSUNE_BOSS_BATTLE;

        public bool OPODPPJNNJM => BattleCategory == eBattleCategory.UEK_BOSS_BATTLE;

        public bool NOMOBFGLCCO => BattleCategory == eBattleCategory.SPACE_BATTLE;

        public bool IsSpecialBattle => false;//this.battleProcessor.IsSpecialBattle();

        public bool EMKDFABDFAH => BattleCategory == eBattleCategory.TOWER || BattleCategory == eBattleCategory.TOWER_EX || BattleCategory == eBattleCategory.TOWER_REHEARSAL || BattleCategory == eBattleCategory.TOWER_CLOISTER;

        public bool NAIOBCOHILK => BattleCategory == eBattleCategory.HIGH_RARITY;

        public bool NFDBEJOPBIJ => BattleCategory == eBattleCategory.TOWER_REPLAY || BattleCategory == eBattleCategory.TOWER_EX_REPLAY || BattleCategory == eBattleCategory.TOWER_CLOISTER_REPLAY;

        public bool DOBCMFAJGCF => BattleCategory == eBattleCategory.KAISER_BATTLE_MAIN || BattleCategory == eBattleCategory.KAISER_BATTLE_SUB;

        public float IDFHINDNPKK { get; set; }

        public float GGCJPDLOAKI { get; set; }

        //public bool GetOnlyAutoClearFlag() => OnlyAutoClearFlag;

        //public void SetOnlyAutoClearFlag(bool IJLDFEJCCMO) => OnlyAutoClearFlag = IJLDFEJCCMO;
        public int KAOHIMNBPOK//多目标初始化相关
        {
            get;
            set;
        }
        public bool LOGNEDLPEIJ { get; set; }

        private bool FELHBMBCMPD => BattleCategory == eBattleCategory.DUNGEON;

        public bool IsPlayingPrincessMovie { get; set; }

        public bool CAOHLDNADPB { get; set; }

        public bool BFKPGHCBBKM { get; set; }

        private bool ANHHNJFCMDB => BattleCategory == eBattleCategory.ARENA || BattleCategory == eBattleCategory.ARENA_REPLAY || BattleCategory == eBattleCategory.GRAND_ARENA || BattleCategory == eBattleCategory.GRAND_ARENA_REPLAY;

        /*public bool BIAMINGPMMB
        {
            get
            {
                if (this.BattleCategory == eBattleCategory.HATSUNE_BATTLE)
                    return HatsuneUtility.IsSelectEventRevival();
                if (this.BattleCategory == eBattleCategory.CLAN_BATTLE)
                    return Singleton<ClanBattleTempData>.Instance.IsRehearsal && this.ELFKGBFMOIE == SaveDataManager.eBattleMaxSpeed.QUADRUPLE;
                if (TowerUtility.IsReleaseTowerBattleSpeedSetting() && (this.EMKDFABDFAH || this.NFDBEJOPBIJ))
                    return this.ELFKGBFMOIE == SaveDataManager.eBattleMaxSpeed.QUADRUPLE;
                return this.FELHBMBCMPD || this.ANHHNJFCMDB || this.BattleCategory == eBattleCategory.FRIEND;
            }
        }*/
        public void DisableAutoClear()
        {
            clearType = eBattleClearType.MANUAL;
        }
        public void GamePauseOnFrameEnd()
        {
            isToPauseOnFrameEnd = true;
            isToResumeOnFrameEnd = false;
        }

        public void GameResumeOnFrameEnd()
        {
            isToPauseOnFrameEnd = false;
            isToResumeOnFrameEnd = true;
        }

        /*private void incrementWave(System.Action ACGNCFFDNON)
        {
            if (++this.CurrentWave >= 3)
                this.CurrentWave = 2;
            this.LAMHAIODABF = 0;// this.battleProcessor.GetNextWaveStartStoryId();
            this.LKLFFOFDCHK = this.LAMHAIODABF == 0;
            this.StartCoroutine(this.incrementWaveCoroutin(ACGNCFFDNON));
        }*/

        /*private IEnumerator incrementWaveCoroutin(Action ACGNCFFDNON)
        {
            bool isCallbakCalled = false;
            isCallbakCalled = false;
            InitializeEnemyForIncliment(delegate
            {
                isCallbakCalled = true;
            });
            while (!isCallbakCalled)
            {
                yield return null;
            }
            checkBossBattle(currentEnemyUnitCtrlDictionary);
            for (int i = 0; i < DJJKGCFKJNJ.Count; i++)
            {
                DJJKGCFKJNJ[i].WaveStartProcess(_first: false);
            }
            sortAndSetPositionOneWave(currentEnemyUnitCtrlDictionary);
            setupEnemyProcess();
            int j = 0;
            for (int count = KKJHHMAAMEI.Count; j < count; j++)
            {
                KKJHHMAAMEI[j].WaveStartProcess(_first: false);
                KKJHHMAAMEI[j].ExecActionOnStartAndDetermineInstanceID();
                KKJHHMAAMEI[j].UnitDamageInfo = tempData.CreateDamageData(KKJHHMAAMEI[j].UnitId, 0, KKJHHMAAMEI[j].UnitParameter.UniqueData.GetCurrentRarity(), AOBBMGHCDMK: true);
                if (KKJHHMAAMEI[j].StartStateIsWalk)
                {
                    KKJHHMAAMEI[j].SetState(UnitCtrl.ActionState.WALK);
                }
            }
            SingletonMonoBehaviour<BattleHeaderController>.Instance.SetWaveNum(POKEAEBGPIB + currentWaveOffset + 1);
            isPauseTimeLimit = false;
            MiliLimitTime01 = tempData.PHDACAOAOMA.CAMIPEAOGNI;
            int num = -1;
            int num2 = -1;
            if (0 < POKEAEBGPIB && POKEAEBGPIB < 3)
            {
                num2 = tempData.PHDACAOAOMA.CurrentBattleBackgrounds[POKEAEBGPIB - 1];
                num = tempData.PHDACAOAOMA.CurrentBattleBackgrounds[POKEAEBGPIB];
                if (tempData.PHDACAOAOMA.CurrentBattleBGM[POKEAEBGPIB - 1] != tempData.PHDACAOAOMA.CurrentBattleBGM[POKEAEBGPIB])
                {
                    soundManager.PlayBGM(tempData.PHDACAOAOMA.CurrentBattleBGMSheet[POKEAEBGPIB], tempData.PHDACAOAOMA.CurrentBattleBGM[POKEAEBGPIB]);
                }
            }
            if (num != -1 && num2 != num)
            {
                stageBgTex.mainTexture = null;
                stageBgTex.material = null;
                stageForegroundTex.material = null;
                resourceManager.UnloadResourceId(eResourceId.BG_BATTLE, num2);
                resourceManager.UnloadResourceId(eResourceId.BATTLE_BG_MASK, num2);
                resourceManager.UnloadResourceId(eResourceId.BG_BATTLE_MATERIAL, num2);
                resourceManager.UnloadResourceId(eResourceId.FG_BATTLE, num2);
                resourceManager.KickUnloadUnusedAssets();
                Texture texture = resourceManager.LoadResourceImmediately(eResourceId.BG_BATTLE, num) as Texture;
                if (texture != null)
                {
                    stageBgTex.mainTexture = texture;
                }
                if (QualityManager.GetGPUQualityLevel() == QualityManager.KMCGOHDIENP.Level_4 && ResourceManager.IsExistResource(eResourceId.BG_BATTLE_MATERIAL, num))
                {
                    Material material = resourceManager.LoadResourceImmediately(eResourceId.BG_BATTLE_MATERIAL, num) as Material;
                    if (material != null)
                    {
                        stageBgTex.material = material;
                    }
                }
                if (ResourceManager.IsExistResource(eResourceId.FG_BATTLE, num))
                {
                    Texture mainTexture = resourceManager.LoadResourceImmediately(eResourceId.FG_BATTLE, num) as Texture;
                    stageForegroundTex.mainTexture = mainTexture;
                }
                UnityEngine.Object.Destroy(PAMLMPDGEIJ);
                if (ResourceManager.IsExistResource(eBundleId.BATTLE_FX_BG, num / 10))
                {
                    resourceManager.LoadAssetBundleImmediately(eBundleId.BATTLE_FX_BG, num / 10);
                }
                if (ResourceManager.IsExistResource(eResourceId.FX_BATTLE_BG, num))
                {
                    PAMLMPDGEIJ = resourceManager.LoadImmediately(eResourceId.FX_BATTLE_BG, ExceptNGUIRoot.Transform, num);
                }
            }
            if (MCEEFEKJDGM)
            {
                StartCoroutine(viewBattle.CoroutineShowBossBattleTitle(BossUnit.ShowTitleDelay));
            }
            MMBMBJNNACG = eBattleGameState.PLAY;
            if (POKEAEBGPIB > 0)
            {
                JJCJONPDGIM = POKEAEBGPIB * 100000;
            }
            ACGNCFFDNON();
        }*/

        private bool spacePressed = false;

        private Stopwatch stopWatch = new Stopwatch();

        private void Update()
        {
            if (battleFinished) return;

            if (Input.GetKey(KeyCode.Space))
            {
                if (!spacePressed) BattleHeaderController.Instance.OnClickPauseButton();
                spacePressed = true;
            }
            else spacePressed = false;

            isUpdateFrameExecuted = false;
            //int index1 = 0;
            /*for (int length = this.UnitUiCtrl.UnitCtrls.Length; index1 < length; ++index1)
            {
                UnitCtrl unitCtrl = this.UnitUiCtrl.UnitCtrls[index1];
                if (!((UnityEngine.Object)unitCtrl == (UnityEngine.Object)null))
                {
                    while (this.JJCJONPDGIM < unitCtrl.CutInFrameSet.ServerCutInFrame)
                        this.updateFrameWithSkip(unitCtrl);
                }
            }*/
            int index1 = 0;
            for (int length = MyGameCtrl.Instance.playerUnitCtrl.Count; index1 < length; ++index1)
            {
                UnitCtrl unitCtrl = MyGameCtrl.Instance.playerUnitCtrl[index1];
                if (!(unitCtrl == null))
                {
                    while (FrameCount < unitCtrl.CutInFrameSet.ServerCutInFrame)
                        updateFrameWithSkip(unitCtrl);
                }
            }

            if (!BattleHeaderController.Instance.IsPaused)
                cutinSkipTimeAccumulated += Time.deltaTime;
            for (; cutinSkipTimeAccumulated >= (double)DeltaTime_60fps; cutinSkipTimeAccumulated -= DeltaTime_60fps)
                CoroutineManager.NoDialogUpdate();
            if (IsPlayingPrincessMovie)
            {
                unitSpineControllerList.ForEach(ACFHIKDFIOJ => ACFHIKDFIOJ.UpdateIndependentBattleSync());
                unitSpineControllerList.ForEach(ACFHIKDFIOJ => ACFHIKDFIOJ.LateUpdateIndependentBattleSync());
            }
            else
            {
                if (!IsFramePause)
                    deltaTimeAccumulated += Time.deltaTime;
                
                if (skipping) stopWatch.Restart();
                
                do
                {
                    if (IsFramePause && !CoroutineManager.VisualPause)
                    {
                        CoroutineManager.VisualPause = true;
                    }

                    if ((IsFramePause || !(deltaTimeAccumulated >= 0.0)) && !skipping)
                        break;
                    
                    updateFrame();
                    deltaTimeAccumulated -= DeltaTime_60fps;

                    // UnityEngine.Debug.LogError(stopWatch.ElapsedMilliseconds);
                }
                while (!IsPlayingPrincessMovie && deltaTimeAccumulated >= (double)DeltaTime_60fps || skipping && stopWatch.ElapsedMilliseconds < 1000);

                while (FrameCount < PPNHIMOOKDD && !IsFramePause)
                {
                    updateFrame();
                    if (IsPlayingPrincessMovie)
                        break;
                }
                if (isToPauseOnFrameEnd && !isToResumeOnFrameEnd)
                    GamePause(true);
                else if (!isToPauseOnFrameEnd && isToResumeOnFrameEnd)
                    GamePause(false);
                if (isUpdateFrameExecuted)
                    goto eof;
                unitSpineControllerList.ForEach(ACFHIKDFIOJ => ACFHIKDFIOJ.UpdateIndependentBattleSync());
                unitSpineControllerList.ForEach(ACFHIKDFIOJ => ACFHIKDFIOJ.LateUpdateIndependentBattleSync());
            }

            eof:
            unitSpineControllerList.ForEach(ACFHIKDFIOJ => ACFHIKDFIOJ.VisualUpdate());
        }

        public bool stepping = false;
        public bool skipping = false;

        private void updateFrame()
        {
            if (battleFinished) return;
            
            bool _canUpdateTime = !isPauseTimeLimit && BlackOutUnitList.Count == 0;
            //BattleHeaderController instance = BattleHeaderController..Instance;

            BattleHeaderController.Instance.PauseNoMoreTimeUp((uint)BlackoutUnitTargetList.Count > 0U);
            if (ActionStartTimeCounter > 0.0)
                ActionStartTimeCounter -= DeltaTime_60fps;
            if (StoryStartTime > 0.0)
                StoryStartTime -= DeltaTime_60fps;
            else if (StartStoryId != 0)
            {
                PlayStory(null, StartStoryId, false);
                StartStoryId = 0;
            }
            if (_canUpdateTime)
            {
                BattleLeftTime -= DeltaTime_60fps;
                BattleHeaderController.CurrentFrameCount++;
                BattleHeaderController.Instance.SetRestTime(BattleLeftTime);
                if (BattleLeftTime <= 0.0)
                {
                    BattleLeftTime = 0.0f;
                    isPauseTimeLimit = true;
                    //this.stopAllAbnormalEffect(this.UnitList);
                    //this.stopAllAbnormalEffect(this.EnemyList);
                    //this.appendWaveEndLog();
                    BattleHeaderController.Instance.SetRestTime(BattleLeftTime);
                    GamePause(true);
                    if (PPNHIMOOKDD > 0)
                        PPNHIMOOKDD = FrameCount;
                    //BattleHeaderController.Instance.gameObject.SetActive(false);
                    //this.UnitUiCtrl.HidePopUp();
                    GameState = eBattleGameState.WAIT_WAVE_END;
                    //this.Timer(0.5f, (System.Action)(() => this.battleProcessor.PlayTimeup((System.Action)(() => this.finishBattle(eBattleResult.TIME_OVER)))));
                    finishBattle(eBattleResult.TIME_OVER);
                    //BattleHeaderController.Instance.gameObject.SetActive(false);
                    //this.UnitUiCtrl.HideSettingButtons();
                    return;
                }
            }
            ++FrameCount;
            if (BattleLeftTime <= 0.0 && battleResult != eBattleResult.WIN)
                return;
            //this.UnitUiCtrl._Update(_canUpdateTime);
            switch (GameState)
            {
                case eBattleGameState.PLAY:
                case eBattleGameState.WAIT_WAVE_END:
                    int index1 = 0;
                    for (int count = UnitList.Count; index1 < count; ++index1)
                        UnitList[index1]._Update();
                    for (int index2 = 0; index2 < EnemyList.Count; ++index2)
                        EnemyList[index2]._Update();
                    break;
            }
            this.battleCameraEffect.UpdateShake();
            for (int index2 = effectUpdateList.Count - 1; index2 >= 0; --index2)
            {
                if (!effectUpdateList[index2]._Update())
                    effectUpdateList.RemoveAt(index2);
            }
            //if (this.JJCJONPDGIM != 0 && this.UnitUiCtrl.UnitCtrls != null && (this.EnemyList != null && !this.callStartCutIn()))
            if (FrameCount != 0 && (EnemyList != null && !callStartCutIn() && !IsPausingEffectSkippedInThisFrame))
                CoroutineManager._Update();
            unitSpineControllerList.ForEach(ACFHIKDFIOJ => ACFHIKDFIOJ.RealUpdate());
            unitSpineControllerList.ForEach(ACFHIKDFIOJ => ACFHIKDFIOJ.RealLateUpdate());
            scriptMgr?.Update();
            battleUnionBurstController.TryExecUnionBurst();
            IsPausingEffectSkippedInThisFrame = false;
            isUpdateFrameExecuted = true;

            if (GuildManager.Instance.stoptime == BattleHeaderController.CurrentFrameCount)
            {
                GuildManager.Instance.stoptime = -1;
                MyGameCtrl.Instance.PauseButton();
            }
            else if (stepping)
            {
                MyGameCtrl.Instance.PauseButton();
                stepping = false;
            }
        }

        private bool callStartCutIn() => GameState == eBattleGameState.PLAY && (updateMainUnit() || updateEnemyUnit());

        //public void PauseAbnormalEffect() => this.battleEffectManager.PauseAbnormalEffect();

        //public void ResumeAbnormalEffect() => this.battleEffectManager.ResumeAbnormalEffect();

        public void GamePause(bool Pause, bool ignoreFamePause = false)
        {
            //if (ICBCHCGGHLB)
            //    this.battleEffectManager.Pause();
            //else
            //    this.battleEffectManager.Resume();
            if (isUBExecing)
                return;
            setPauseAllUnit(Pause);
            if (!ignoreFamePause)
                IsFramePause = Pause;
            CoroutineManager.SystemPause = Pause;
            CoroutineManager.VisualPause = Pause;
            //BattleHeaderController.Instance.Pause(Pause);
            isToPauseOnFrameEnd = false;
            isToResumeOnFrameEnd = false;
            //Debug.Log(BattleHeaderController.CurrentFrameCount + "--" + (Pause ? "STOP" : "RESUME"));
        }

        /*public void PauseAllSe()
        {
            for (int index = 0; index < this.UnitList.Count; ++index)
                this.UnitList[index].PauseSound(true);
            for (int index = 0; index < this.EnemyList.Count; ++index)
                this.EnemyList[index].PauseSound(true);
        }*/

        public void FrameCountPause(UnitCtrl FNHGFDNICFG)
        {
            voiceOnUnit = FNHGFDNICFG;
            //this.skillExeScreenTexture.color = Color.black;
            //this.skillExeScreen.gameObject.SetActive(true);
            //this.skillExeScreen.transform.position = Vector3.zero;
            //this.skillExeScreen.sortingOrder = 4899;
            IsFramePause = true;
        }

        private IEnumerator resumeFrameCount()
        {
            BattleManager battleManager = this;
            // ISSUE: explicit non-virtual call
            if (!(battleManager == null))// && !((UnityEngine.Object)(battleManager.UnitUiCtrl) == (UnityEngine.Object)null))
            {
                // ISSUE: explicit non-virtual call
                //(battleManager.UnitUiCtrl).UpdateAllEnemyHp0();
                // ISSUE: explicit non-virtual call
                // ISSUE: explicit non-virtual call
                if ((battleManager.GameState) != eBattleGameState.PLAY)// || (battleManager.UnitUiCtrl).AllEnemyHp0)
                {
                    // ISSUE: explicit non-virtual call
                    battleManager.GamePause(false);
                }
                else
                {
                    while (true)
                    {
                        bool flag = true;
                        int index = 0;
                        // ISSUE: explicit non-virtual call
                        for (int count = battleManager.UnitList.Count; index < count; ++index)
                        {
                            // ISSUE: explicit non-virtual call
                            if (battleManager.UnitList[index].CutInFrameSet.ServerCutInFrame == -2)
                                flag = false;
                        }
                        if (!flag)
                            yield return null;
                        else
                            break;
                    }
                    //battleManager.blockLayerManager.SetBlockDialog(false);
                    //battleManager.skillExeScreen.transform.localPosition = Vector3.zero;
                    //battleManager.skillExeScreen.sortingOrder = 11500;
                    battleManager.IsFramePause = false;
                }
            }
        }

        public void RetireBattle()
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            //BattleManager.MIBNCMNBKGI mibncmnbkgi = new BattleManager.MIBNCMNBKGI();
            // ISSUE: reference to a compiler-generated field
            // mibncmnbkgi.4__this = this;
            /*if (this.battleProcessor.DisplayRetireFail())
            {
              // ISSUE: reference to a compiler-generated method
              this.ResultApiSendExec(()=> { 
                  //
              });
            }
            else
            {
              if (this.BLBNAOLHCHD)
                this.appendWaveEndLog();
              Time.timeScale = 1f;
              this.soundManager.DisableSoundListener();
              SupportUnitUtility.InitSupportUnitData();
              SupportUnitUtility.InitSupportUnitQuestStartParam();
              // ISSUE: reference to a compiler-generated field
              mibncmnbkgi.returnViewId = this.viewManager.BeforeViewId;
              if (this.tempData.ReturnViewOnRematch != eViewId.NONE)
              {
                // ISSUE: reference to a compiler-generated field
                mibncmnbkgi.returnViewId = this.tempData.ReturnViewOnRematch;
                this.tempData.ReturnViewOnRematch = eViewId.NONE;
              }
              // ISSUE: reference to a compiler-generated field
              this.battleProcessor.BattleRetire(ref mibncmnbkgi.returnViewId);
              // ISSUE: reference to a compiler-generated method
              this.ResultApiSendExec(new System.Action(mibncmnbkgi.RetireBattleb__1), (System.Action<int>) null);
            }*/
        }

        //public void SkipBattle() => this.battleProcessor.DrawTheCurtains();

        /*private void copyDataFromUserUnitToDungeonTemp(List<DungeonQueryUnit> DDNLKBBOKIN)
        {
            DungeonTempData instance = Singleton<DungeonTempData>.Instance;
            for (int index = 0; index < DDNLKBBOKIN.Count; ++index)
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              BattleManager.GDCLEDKHACG gdcledkhacg = new BattleManager.GDCLEDKHACG();
              // ISSUE: reference to a compiler-generated field
              gdcledkhacg.fromUnit = DDNLKBBOKIN[index];
              // ISSUE: reference to a compiler-generated field
              if (gdcledkhacg.fromUnit.owner_viewer_id != (int) Singleton<UserData>.Instance.UserInfo.ViewerId)
              {
                // ISSUE: reference to a compiler-generated method
                ClanDispatchUnit clanDispatchUnit = instance.DispatchUnitList.Find(new Predicate<ClanDispatchUnit>(gdcledkhacg.copyDataFromUserUnitToDungeonTempb__0));
                if (clanDispatchUnit == null)
                  return;
                // ISSUE: reference to a compiler-generated field
                clanDispatchUnit.SetHp(gdcledkhacg.fromUnit.hp);
                // ISSUE: reference to a compiler-generated field
                clanDispatchUnit.SetEnergy(gdcledkhacg.fromUnit.energy);
              }
            }
            for (int index = 0; index < DDNLKBBOKIN.Count; ++index)
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              BattleManager.PJMAICJCDGH pjmaicjcdgh = new BattleManager.PJMAICJCDGH();
              DungeonQueryUnit dungeonQueryUnit = DDNLKBBOKIN[index];
              if (dungeonQueryUnit.owner_viewer_id == (int) Singleton<UserData>.Instance.UserInfo.ViewerId)
              {
                // ISSUE: reference to a compiler-generated field
                if (!instance.UnitsDic.TryGetValue(dungeonQueryUnit.unit_id, out pjmaicjcdgh.toUnit))
                  break;
                // ISSUE: reference to a compiler-generated field
                pjmaicjcdgh.toUnit.SetHp(dungeonQueryUnit.hp);
                // ISSUE: reference to a compiler-generated field
                pjmaicjcdgh.toUnit.SetEnergy(dungeonQueryUnit.energy);
                // ISSUE: reference to a compiler-generated method
                dungeonQueryUnit.skill_limit_counter.ForEach<SkillLimitCounter>(new System.Action<SkillLimitCounter>(pjmaicjcdgh.copyDataFromUserUnitToDungeonTempb__1));
              }
            }
        }*/

        public bool IsSkillExeUnit(UnitCtrl AIMGFOAEPLO) => BlackOutUnitList.Contains(AIMGFOAEPLO) || LPAAPDHAIIB == AIMGFOAEPLO;

        public void AddUnitSpineControllerList(BattleSpineController NLLGNCKLGHL) => unitSpineControllerList.Add(NLLGNCKLGHL);

        public void RemoveUnitSpineControllerList(BattleSpineController NLLGNCKLGHL)
        {
            if (unitSpineControllerList == null)
                return;
            unitSpineControllerList.Remove(NLLGNCKLGHL);
        }

        public void AppendCoroutine(
          IEnumerator PGEADNDHDIL,
          ePauseType MHPJECIHDDL,
          UnitCtrl GEDLBPMPOKB = null) => CoroutineManager.AppendCoroutine(PGEADNDHDIL, MHPJECIHDDL, GEDLBPMPOKB);

        public void StartCoroutineIgnoreFps(IEnumerator DFMFLELLMCA) => StartCoroutine(updateIgnoreFps(DFMFLELLMCA));

        private IEnumerator updateIgnoreFps(IEnumerator HEINAOHCPPE)
        {
            float deltaTimeAccumulated = 0.0f;
            while (true)
            {
                for (deltaTimeAccumulated += Time.deltaTime; deltaTimeAccumulated > 0.0; deltaTimeAccumulated -= DeltaTime_60fps)
                {
                    if (!HEINAOHCPPE.MoveNext())
                        yield break;
                }
                yield return null;
            }
        }

        public void RemoveCoroutine(UnitCtrl FNHGFDNICFG) => CoroutineManager.RemoveCoroutine(FNHGFDNICFG);

        public void AppendEffect(SkillEffectCtrl NJDBDNAANNG, UnitCtrl FNHGFDNICFG = null, bool KFFAEDDEICK = false) { }// => this.battleEffectManager.AppendEffect(NJDBDNAANNG, FNHGFDNICFG, KFFAEDDEICK);

        //public void AppendUITweener(UITweener CHBBDEHNPGH, UnitCtrl GEDLBPMPOKB = null, bool HDJNNDDPDJI = false) => this.battleEffectManager.AppendTweener(CHBBDEHNPGH, GEDLBPMPOKB, HDJNNDDPDJI);

        public void AddEffectToUpdateList(SkillEffectCtrl FJOPFIJMADE)
        {
            if (effectUpdateList.Contains(FJOPFIJMADE))
                return;
            effectUpdateList.Add(FJOPFIJMADE);
        }

        public void RemoveEffectToUpdateList(SkillEffectCtrl FJOPFIJMADE)
        {
            if (effectUpdateList == null)
                return;
            effectUpdateList.Remove(FJOPFIJMADE);
        }
        public static Action<RandomData> OnCallRandom;        
        //public static float Random(float Min, float Max) => (float)BattleManager.Random((int)((double)Min * 1000.0), (int)((double)Max * 1000.0)) / 1000f;
        public static float Random(float Min, float Max, RandomData data)
        {
            return Random((int)(Min * 1000.0), (int)(Max * 1000.0),data) / 1000f;
        }

        //public static int Random(int Min, int Max) => Min == Max ? Min : UnityEngine.Random.Range(Min, Max);
        public static int Random(int Min, int Max, RandomData data)
        {
            if (Max == Min) return Min;
            int result = UnityEngine.Random.Range(Min, Max);
            //System.IO.File.AppendAllText("D:\\rnd.log", $"({BattleHeaderController.CurrentFrameCount})rnd is {result}, stack:\n{new StackTrace()}\n");
            data.randomResult = result;
            data.id = randomCounter++;
            OnCallRandom?.Invoke(data);
            return result;
        }

        public static int HeldRandom(int Min, int Max)
        {
            if (Min == Max)
                return Min;
            Random.State state = UnityEngine.Random.state;
            int num = UnityEngine.Random.Range(Min, Max);
            UnityEngine.Random.state = state;
            return num;
        }

        /*public void CreanteUnit(int Unitid, System.Action<UnitCtrl> callback, bool addUnitList = true)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            //BattleManager.OOHPFHCADAP oohpfhcadap = new BattleManager.OOHPFHCADAP();
            // ISSUE: reference to a compiler-generated field
            //oohpfhcadap._addUnitList = OBNGLLBJACE;
            // ISSUE: reference to a compiler-generated field
            //oohpfhcadap.4__this = this;
            // ISSUE: reference to a compiler-generated field
            // oohpfhcadap._callback = MELLIBIDKGI;
            //ResourceManager instance = ManagerSingleton<ResourceManager>.Instance;
            // ISSUE: reference to a compiler-generated method
            Singleton<BattleUnitLoader>.Instance.AddLoadResource(AGAPBBHBIJD,
                (gameObject) =>
                {
                    UnitCtrl component = gameObject.GetComponent<UnitCtrl>();
                    if (addUnitList)
                    {
                        this.UnitList.Add(component);
                    }
                    callback(component);
                }
                , _isInstantiate: true);
        }*/
        /// <summary>
        /// 生成战斗小人
        /// </summary>
        /// <param name="unitCtrl"></param>
        /// <param name="respawnPos"></param>
        /// <param name="pPararm"></param>
        /// <param name="callBack"></param>
        /// <param name="otherUser"></param>
        /*public void CreateUnitSpine(
          UnitCtrl unitCtrl,
          eUnitRespawnPos respawnPos,
          UnitParameter pPararm,
          System.Action<UnitCtrl> callBack = null,
          bool otherUser = false)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            BattleManager.LNDAHMIJGIF lndahmijgif = new BattleManager.LNDAHMIJGIF()
            {
              4__this = this,
              _unit = FNHGFDNICFG,
              _respawnPos = AAJOGMFIBGI,
              _pParam = AJNBNCCONNO,
              _callback = MELLIBIDKGI
            };
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            //lndahmijgif.unitId = (int) lndahmijgif._pParam.MasterData.PrefabId;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            int setSkinNo = UnitUtility.GetSetSkinNo(pPararm.UniqueData, UnitDefine.eSkinType.Sd, otherUser, unitCtrl);
            GameObject gameObject = new GameObject();
            // ISSUE: reference to a compiler-generated field
            gameObject.transform.SetParent(unitCtrl.transform.TargetTransform, false);
            gameObject.name = "rotate_center";
            // ISSUE: reference to a compiler-generated field
            //lndahmijgif._unit.RotateCenter = gameObject.transform;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated method
            BattleSpineController.LoadCreate(this.IsDefenceReplayMode ? eSpineType.SD_SHADOW_BOUND : eSpineType.SD_NORMAL_BOUND, pPararm.MasterData.UnitId, setSkinNo, gameObject.transform,
                (battleSpineController) => {
                    if (IsDefenceReplayMode)
                    {
                        unitCtrl.IsShadow = true;
                    }
                    unitCtrl.UnitSpineCtrl = battleSpineController;
                    battleSpineController.Owner = unitCtrl;
                    if (!UnitUtility.JudgeIsGuest(pPararm.MasterData.PrefabId))
                    {
                        battleSpineController.LoadAnimationIDImmediately(eSpineBinaryAnimationId.COMMON_BATTLE);
                    }
                    unitCtrl.CenterBone = battleSpineController.skeleton.FindBone("Center");
                    unitCtrl.StateBone = battleSpineController.skeleton.FindBone("State");
                    battleSpineController.SetAnimeEventDelegateForBattle(() => { battleSpineController.IsStopState = true; }, -1);
                    unitCtrl.transform.parent = unitParentTransform;
                    unitCtrl.transform.ResetLocal();
                    unitCtrl.transform.localPosition = new Vector3(-1400f, GetRespawnPos(respawnPos), 0);
                    battleProcessor.AddUnitSkillUseCount(ref unitCtrl.SkillUseCount, pPararm.MasterData.PrefabId);
                    unitCtrl.Initialize(pPararm, false, true, false);
                    unitCtrl.BattleStartProcess(respawnPos);
                    callBack.Call<UnitCtrl>(unitCtrl);
                });
        }*/

        /*private void appendWaveEndLog()
        {
            System.Action<List<UnitCtrl>> action = (System.Action<List<UnitCtrl>>)(MMGJMMGDPGM =>
           {
               if (MMGJMMGDPGM.Count <= 0)
                   return;
               for (int index = 0; index < MMGJMMGDPGM.Count; ++index)
               {
                   if ((UnityEngine.Object)MMGJMMGDPGM[index] != (UnityEngine.Object)null && MMGJMMGDPGM[index].UnitDamageInfo != null)
                   {
                       GJNIHENNINA battleLog1 = this.battleLog;
                       UnitCtrl unitCtrl1 = MMGJMMGDPGM[index];
                       UnitCtrl unitCtrl2 = MMGJMMGDPGM[index];
                       long hp = (long)MMGJMMGDPGM[index].Hp;
                       UnitCtrl JELADBAMFKH1 = unitCtrl1;
                       UnitCtrl LIMEKPEENOB1 = unitCtrl2;
                       battleLog1.AppendBattleLog(eBattleLogType.WAVE_END_HP, 0, hp, 0L, 0, 0, 1, 1, JELADBAMFKH1, LIMEKPEENOB1);
                       GJNIHENNINA battleLog2 = this.battleLog;
                       UnitCtrl unitCtrl3 = MMGJMMGDPGM[index];
                       UnitCtrl unitCtrl4 = MMGJMMGDPGM[index];
                       long damage = (long)MMGJMMGDPGM[index].UnitDamageInfo.damage;
                       UnitCtrl JELADBAMFKH2 = unitCtrl3;
                       UnitCtrl LIMEKPEENOB2 = unitCtrl4;
                       battleLog2.AppendBattleLog(eBattleLogType.WAVE_END_DAMAGE_AMOUNT, 0, damage, 0L, 0, 0, 1, 1, JELADBAMFKH2, LIMEKPEENOB2);
                   }
               }
           });
            action(this.UnitList);
            action(this.EnemyList);
        }*/

        /*public void NextWaveProcess()
        {
            this.GameState = eBattleGameState.NEXT_WAVE_PROCESS;
            BattleVoiceUtility.PlayNextStageVoice(this.UnitList, this.POKEAEBGPIB + 1 == 2);
            int index = 0;
            for (int count = this.UnitList.Count; index < count; ++index)
            {
                this.UnitList[index].BattleRecovery(this.BattleCategory);
                this.UnitList[index].MoveToNext();
            }
            this.StartCoroutine(this.updateNextBattleWalk());
        }*/

        public void StopResume() => GamePause(true);

        public void CancelInvalidSupportSkill(UnitCtrl FNHGFDNICFG)
        {
            FNHGFDNICFG.CutInFrameSet.CutInFrame = -1;
            FNHGFDNICFG.CutInFrameSet.ServerCutInFrame = -1;
            OnBlackOutEnd(FNHGFDNICFG, true);
        }

        /*private IEnumerator updateNextBattleWalk()
        {
            BattleManager battleManager = this;
            float time = 0.0f;
            while (true)
            {
                // ISSUE: explicit non-virtual call
                time += (battleManager.IsFramePause) ? 0.0f : Time.deltaTime;
                if ((double)time <= 1.35000002384186)
                    yield return (object)null;
                else
                    break;
            }
            int index1 = 0;
            // ISSUE: explicit non-virtual call
            for (int count = (battleManager.UnitList).Count; index1 < count; ++index1)
            {
                // ISSUE: explicit non-virtual call
                UnitCtrl unitCtrl = (battleManager.UnitList)[index1];
                if (unitCtrl.MoviePlayed)
                {
                    ManagerSingleton<MovieManager>.Instance.Load(eMovieType.CUT_IN, (long)unitCtrl.MovieId);
                    unitCtrl.MoviePlayed = false;
                }
            }
            // ISSUE: explicit non-virtual call
            int index2 = ((battleManager.POKEAEBGPIB) + 1) % 3;
            battleManager.checkBossBattle(battleManager.tempData.PHDACAOAOMA.AOPCDPIJOIE[index2]);
            // ISSUE: explicit non-virtual call
            if ((battleManager.MCEEFEKJDGM))
            {
                int battleBackground = battleManager.tempData.PHDACAOAOMA.CurrentBattleBackgrounds[index2];
                battleManager.viewBattle.TransformEffectCtrlForBoss.Play(battleBackground);
            }
            else
            {
                // ISSUE: reference to a compiler-generated method
                //battleManager.viewBattle.TransformEffectCtrl.Play(_endCallback: updateNextBattleWalk, _eSe: eSE.BTL_NEXT_WAVE);
            }
        }*/

        /*private void onBossStateChange(
          UnitCtrl IMDKENFNOMF,
          eStateIconType BBPKGJINFED,
          bool GABGIKMFNFG)
        {
            PartsBossGauge bossGauge = BattleHeaderController.Instance.BossGauge;
            if (GABGIKMFNFG)
                bossGauge.AbnormalStateIcon.AddIcon(BBPKGJINFED);
            else
                bossGauge.AbnormalStateIcon.DeleteIcon(BBPKGJINFED);
        }*/

        /*private void onBossStateNumChange(
          UnitCtrl IMDKENFNOMF,
          eStateIconType BBPKGJINFED,
          int HAJOKKCLPJL) => BattleHeaderController.Instance.BossGauge.AbnormalStateIcon.SetStateNum(BBPKGJINFED, HAJOKKCLPJL);*/

        private void sortAndSetPositionOneWave(Dictionary<int, UnitCtrl> enemyDic)
        {
            List<UnitCtrl> enemyList = new List<UnitCtrl>(enemyDic.Values);
            int count = enemyList.Count;
            float[] SearchAreaSizeArray = new float[count];
            int[] indexArray = new int[count];
            for (int index = 0; index < count; ++index)
            {
                UnitCtrl unitCtrl = enemyList[index];
                SearchAreaSizeArray[index] = unitCtrl.SearchAreaSize;
                indexArray[index] = index;
            }
            //this.battleProcessor.SortPosition(CNBJIKBCFIC, MHCFBOGJNIO, AAENDEGPKJE);
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            EnemyList.Clear();
            for (int index = 0; index < count; ++index)
            {
                int unitId = enemyList[indexArray[index]].UnitId;
                if (dictionary.ContainsKey(unitId))
                    ++dictionary[unitId];
                else
                    dictionary.Add(unitId, 0);
                float overlapPosX = 100f * dictionary[unitId];
                UnitCtrl unitCtrl = enemyList[indexArray[index]];
                unitCtrl.SetOverlapPos(overlapPosX);
                if (IsFramePause)
                    unitCtrl.Pause = true;
                EnemyList.Add(unitCtrl);
            }
        }

        private void setupEnemyProcess()
        {
            if (EnemyList.FindIndex(JNOIHMMFADD => JNOIHMMFADD.BossSortIsBack) != -1)
                HasBoss = true;
            for (int index = 0; index < EnemyList.Count; ++index)
            {
                UnitCtrl DAFOBMOGLKA = EnemyList[index];
                DAFOBMOGLKA.OnDieForZeroHp += onDieEnemy;
                SetupEnemyStartProcess(DAFOBMOGLKA, index);
            }
        }
        private void SetupEnemyStartProcess(UnitCtrl unit, int index)
        {
            eUnitRespawnPos respawnPos = getUnitRespawnPos(index, true);
            if (HasBoss && respawnPos == eUnitRespawnPos.MAIN_POS_1)
                respawnPos = eUnitRespawnPos.SUB_POS_1;
            if (unit.IsBoss)
            {
                respawnPos = unit.BossSortIsBack ? eUnitRespawnPos.MAIN_POS_1 : eUnitRespawnPos.MAIN_POS_5;
                unit.SetState(UnitCtrl.ActionState.GAME_START);
                unit.MoveSpeed = 0.0f;
                //PartsBossGauge bossGauge = SingletonMonoBehaviour<BattleHeaderController>.Instance.BossGauge;
                //bossGauge.gameObject.SetActive(true);
                //bossGauge.InitGauge((long)unit.Hp, (long)unit.MaxHp, this.battleCategory);
            }
            else
                unit.StartStateIsWalk = true;
            unit.BattleStartProcess(respawnPos);
        }
       private eUnitRespawnPos getUnitRespawnPos(  int index,  bool main) => BattleDefine.GetUnitRespawnPos(index, main);


        private void onDieEnemy(UnitCtrl IMDKENFNOMF)
        {
            Vector3 localPosition = IMDKENFNOMF.transform.localPosition;
            localPosition.y = GetRespawnPos(IMDKENFNOMF.RespawnPos);
            localPosition.x += IMDKENFNOMF.OverlapPosX;
            //this.AppendCoroutine(this.playGoldEffectWithDelay(IMDKENFNOMF), ePauseType.IGNORE_BLACK_OUT);
            //this.AppendCoroutine(this.dropTreasureBox(IMDKENFNOMF), ePauseType.IGNORE_BLACK_OUT);
            //BattleHeaderController instance = BattleHeaderController..Instance;
            // instance.SetGoldNum(instance.Gold + (int)IMDKENFNOMF.Rupee);
        }

        /*private IEnumerator playGoldEffectWithDelay(UnitCtrl IMDKENFNOMF)
        {
            float timer = 0.0f;
            while (true)
            {
                timer += this.DeltaTime_60fps;
                if ((double)timer <= 0.200000002980232)
                    yield return (object)null;
                else
                    break;
            }
            if ((int)IMDKENFNOMF.Rupee > 0)
                this.soundManager.PlaySeByOuterSource(this.createDropPrefab(IMDKENFNOMF, Singleton<LCEGKJFKOPD>.Instance.MDOCCDAIICB).SeSource, eSE.BTL_DROP_MANA);
        }*/

        /*private IEnumerator dropTreasureBox(UnitCtrl IMDKENFNOMF)
        {
            int dropCount = (int)IMDKENFNOMF.RewardCount;
            if (dropCount != 0)
            {
                float timer = 0.0f;
                while (true)
                {
                    timer += this.DeltaTime_60fps;
                    if ((double)timer <= (double)IMDKENFNOMF.UnitSpineCtrl.DropTreasureBoxTime)
                        yield return (object)null;
                    else
                        break;
                }
                IMDKENFNOMF.TreasureAnimeIdList.Sort();
                float offsetY = 0.0f;
                TreasureEffectController[] treasureEffectControllers = new TreasureEffectController[dropCount];
                int i;
                for (i = 0; i < dropCount; ++i)
                {
                    TreasureEffectController dropPrefab = this.createDropPrefab(IMDKENFNOMF, Singleton<LCEGKJFKOPD>.Instance.FJLDBIECPGP) as TreasureEffectController;
                    treasureEffectControllers[i] = dropPrefab;
                    int index = i % BattleDefine.TREASURE_POS_OFFSET_X.Length;
                    float x = BattleDefine.TREASURE_POS_OFFSET_X[index];
                    dropPrefab.transform.Translate(x, offsetY, (float)i);
                    offsetY += index == 1 ? 0.0f : 0.08333334f;
                    dropPrefab.RewardCount = 1;
                    dropPrefab.SetIsIndependentBattleSync();
                    dropPrefab.ChangeTreasureType(IMDKENFNOMF.TreasureAnimeIdList[i]);
                    if (i != dropCount - 1)
                    {
                        float dropInterval = IMDKENFNOMF.TreasureAnimeIdList[i + 1] < eSpineCharacterAnimeId.TREASURE_EFFECT05 ? 0.02f : 0.06f;
                        for (timer = 0.0f; (double)timer < (double)dropInterval; timer += this.battleProcessor.GetDropTreasureBoxDeltaTime())
                            yield return (object)null;
                    }
                    else
                        break;
                }
                while (!treasureEffectControllers[dropCount - 1].IsMotionFinished)
                    yield return (object)null;
                for (i = 0; i < dropCount; ++i)
                {
                    treasureEffectControllers[i].StartMove();
                    for (timer = 0.0f; (double)timer < 0.0500000007450581; timer += this.battleProcessor.GetDropTreasureBoxDeltaTime())
                        yield return (object)null;
                }
            }
        }*/

        /*private SkillEffectCtrl createDropPrefab(
          UnitCtrl IMDKENFNOMF,
          GameObject MDOJNMEMHLN)
        {
            GameObject gameObject = this.battleEffectPool.GetEffect(MDOJNMEMHLN, (UnitCtrl)null).gameObject;
            gameObject.transform.parent = ExceptNGUIRoot.Transform;
            gameObject.transform.position = IMDKENFNOMF.BottomTransform.position;
            SkillEffectCtrl component = gameObject.GetComponent<SkillEffectCtrl>();
            component.InitializeSort();
            component.SortTarget = IMDKENFNOMF;
            component.ExecAppendCoroutine();
            return component;
        }*/

        private void setPauseAllUnit(bool pause)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: reference to a compiler-generated method
            Action<List<UnitCtrl>> action = list =>
            {
                foreach (UnitCtrl unit in list)
                {
                    if (!pause && BlackOutUnitList.Count > 0)
                    {
                        if (BlackOutUnitList.Contains(unit))
                        {
                            unit.Pause = pause;
                            //unit.PauseVoice(pause);
                        }
                    }
                    else
                    {
                        unit.Pause = pause;
                        //unit.PauseVoice(pause);
                    }
                }
            };
            action(UnitList);
            action(EnemyList);
        }

        private void checkBossBattle(Dictionary<int, UnitCtrl> IGIJGFNEKHB)
        {
            IsBossBattle = false;
            List<UnitCtrl> unitCtrlList = new List<UnitCtrl>(IGIJGFNEKHB.Values);
            for (int index = 0; index < unitCtrlList.Count; ++index)
            {
                if (UnitUtility.JudgeIsBoss(unitCtrlList[index].UnitId))
                {
                    IsBossBattle = true;
                    break;
                }
            }
        }

        // private void checkBossBattle(List<UnitParameter> JCLJGFKMBJM) => this.MCEEFEKJDGM = JCLJGFKMBJM.FindIndex((Predicate<UnitParameter>)(ACFHIKDFIOJ => UnitUtility.JudgeIsBoss((int)ACFHIKDFIOJ.UniqueData.Id))) != -1;

        private bool updateMainUnit()
        {
            int index = 0;
            //for (int length = this.UnitUiCtrl.UnitCtrls.Length; index < length; ++index)
            for (int length = UnitList.Count; index < length; ++index)
            {
                UnitCtrl unitCtrl = UnitList[index];
                if (!(unitCtrl == null))
                {
                    if (FrameCount == unitCtrl.CutInFrameSet.CutInFrame)
                    {
                        if (!unitCtrl.IsSkillReady)
                        {
                            unitCtrl.CutInFrameSet.CutInFrame = -1;
                            unitCtrl.CutInFrameSet.ServerCutInFrame = -1;
                            ChargeSkillTurn = eChargeSkillTurn.NONE;
                            return false;
                        }
                        unitCtrl.StartCutIn();
                        return true;
                    }
                    if (FrameCount == unitCtrl.CutInFrameSet.ServerCutInFrame + 1)
                    {
                        onSyncEnd(unitCtrl);
                        if (unitCtrl.ConsumeEnergy() == eConsumeResult.FAILED || GameState != eBattleGameState.PLAY)
                            unitCtrl.CutInFrameSet.ServerCutInFrame = -3;
                        else
                            unitCtrl.SetState(UnitCtrl.ActionState.SKILL_1);
                        LJFFAACGDMF = false;
                    }
                    if (unitCtrl.CutInFrameSet.ServerCutInFrame == -3)
                    {
                        LJFFAACGDMF = false;
                        unitCtrl.CutInFrameSet.CutInFrame = -1;
                        unitCtrl.CutInFrameSet.ServerCutInFrame = -1;
                        OnBlackOutEnd(unitCtrl);
                    }
                }
            }
            return false;
        }

        private bool updateEnemyUnit()
        {
            int index = 0;
            for (int count = EnemyList.Count; index < count; ++index)
            {
                UnitCtrl enemy = EnemyList[index];
                if (!(enemy == null))
                {
                    if (FrameCount == enemy.CutInFrameSet.CutInFrame)
                    {
                        if (!enemy.IsSkillReady)
                        {
                            enemy.CutInFrameSet.CutInFrame = -1;
                            enemy.CutInFrameSet.ServerCutInFrame = -1;
                            ChargeSkillTurn = eChargeSkillTurn.NONE;
                            return false;
                        }
                        enemy.StartCutIn();
                        return true;
                    }
                    if (FrameCount == enemy.CutInFrameSet.CutInFrame + 1)
                    {
                        onSyncEnd(enemy);
                        LJFFAACGDMF = false;
                        if (enemy.ConsumeEnergy() == eConsumeResult.FAILED)
                        {
                            enemy.CutInFrameSet.CutInFrame = -1;
                            enemy.CutInFrameSet.ServerCutInFrame = -1;
                            OnBlackOutEnd(enemy);
                        }
                        else
                            enemy.SetState(UnitCtrl.ActionState.SKILL_1);
                    }
                    if (enemy.CutInFrameSet.ServerCutInFrame == -3)
                    {
                        LJFFAACGDMF = false;
                        enemy.CutInFrameSet.CutInFrame = -1;
                        enemy.CutInFrameSet.ServerCutInFrame = -1;
                        OnBlackOutEnd(enemy);
                    }
                }
            }
            return false;
        }

        private void onSyncEnd(UnitCtrl FNHGFDNICFG)
        {
            FNHGFDNICFG.SetSortOrderFront();
            setPauseAllUnit(true);
        }

        private void resetUnitFrame()
        {
            for (int index = 0; index < UnitList.Count; ++index)
            {
                UnitList[index].CutInFrameSet.CutInFrame = -1;
                UnitList[index].CutInFrameSet.ServerCutInFrame = -1;
            }
            for (int index = 0; index < EnemyList.Count; ++index)
            {
                EnemyList[index].CutInFrameSet.CutInFrame = -1;
                EnemyList[index].CutInFrameSet.ServerCutInFrame = -1;
            }
        }

        //public void DisplayUnitFrontOfDialog() => this.battleProcessor.DisplayUnitFront();

        /*public void DisplayEnemyFrontOfDialog()
        {
            this.battleProcessor.ComplementZoom();
            if (this.EnemyList.Count == 1)
            {
                this.EnemyList[0].DisplayFrontOfDialog(0.0f);
            }
            else
            {
                List<UnitCtrl> unitCtrlList = this.DMALFANMBMM || this.IsSpecialBattle ? this.EnemyList.FindAll((Predicate<UnitCtrl>)(EGDDLFIFPGE => EGDDLFIFPGE.IsBoss)) : this.EnemyList.FindAll((Predicate<UnitCtrl>)(EGDDLFIFPGE => !EGDDLFIFPGE.IsDead && !EGDDLFIFPGE.IsSummonOrPhantom));
                int index = 0;
                for (int count = unitCtrlList.Count; index < count; ++index)
                {
                    if ((long)unitCtrlList[index].Hp > 0L)
                    {
                        float num = (float)(unitCtrlList.Count - 1) * 0.5f;
                        unitCtrlList[index].DisplayFrontOfDialog(num - (float)index);
                    }
                }
            }
        }*/

        /*public IEnumerator UpdateZoom(float GNOCPLCBFFK, float DBOGPAGPINC)
        {
            CustomEasing easingScale = new CustomEasing(CustomEasing.eType.outQuad, this.bgTransform.localScale.x, GNOCPLCBFFK, 0.3f);
            CustomEasing easingY = new CustomEasing(CustomEasing.eType.outQuad, 0.0f, DBOGPAGPINC - this.bgTransform.localPosition.y, 0.3f);
            while (easingScale.IsMoving)
            {
                float num1 = 2.044444f * (float)Screen.height / (float)Screen.width;
                float num2 = 1.15f / easingScale.GetCurVal(Time.deltaTime);
                this.playCamera.orthographicSize = num1 * num2;
                this.playCamera.transform.SetLocalPosY((float)(5000.0 - (double)easingY.GetCurVal(Time.deltaTime) * (double)num2));
                yield return (object)null;
            }
        }*/

        /*public void DisplayAliveUnitFront()
        {
            int num1 = 0;
            List<UnitCtrl> unitCtrlList = new List<UnitCtrl>();
            for (int index = 0; index < this.UnitUiCtrl.UnitCtrls.Length; ++index)
            {
                if (!((UnityEngine.Object)this.UnitUiCtrl.UnitCtrls[index] == (UnityEngine.Object)null) && (long)this.UnitUiCtrl.UnitCtrls[index].Hp > 0L)
                {
                    ++num1;
                    unitCtrlList.Add(this.UnitUiCtrl.UnitCtrls[index]);
                }
            }
            if (num1 == 1)
            {
                unitCtrlList[0].DisplayFrontOfDialog(0.0f);
            }
            else
            {
                for (int index = 0; index < unitCtrlList.Count; ++index)
                {
                    float num2 = (float)(num1 - 1) * 0.5f;
                    unitCtrlList[index].DisplayFrontOfDialog(num2 - (float)index);
                }
            }
        }*/

        public void RunOutBattleResult()
        {
            int index = 0;
            for (int count = UnitList.Count; index < count; ++index)
                UnitList[index].RunOutBattleResult();
        }

        public IEnumerator RecreateDeadUnits(Action MELLIBIDKGI, eBattleResult JJHEBNEEHPB)
        {
            yield return null;
            /*// ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            BattleManager.GPBDDMDJFHA gpbddmdjfha = new BattleManager.GPBDDMDJFHA();
            // ISSUE: reference to a compiler-generated field
            gpbddmdjfha.4__this = this;
            if (JJHEBNEEHPB != eBattleResult.WIN)
            {
              MELLIBIDKGI.Call();
            }
            else
            {
              this.UnitUiCtrl.IsDisableUpdate = true;
              // ISSUE: reference to a compiler-generated field
              gpbddmdjfha.isResourceLoaded = false;
              // ISSUE: reference to a compiler-generated field
              gpbddmdjfha.recreatedUnitIndex = new List<int>();
              // ISSUE: reference to a compiler-generated field
              gpbddmdjfha.recreatedUnitId = new List<int>();
              this.UnitUiCtrl.SortUnitCtrlsByAliveness();
              this.resetUnitListOrder();
              for (int index = 0; index < this.PlayersList.Count; ++index)
              {
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                BattleManager.CBGCLOBPBDA cbgclobpbda = new BattleManager.CBGCLOBPBDA();
                // ISSUE: reference to a compiler-generated field
                cbgclobpbda.CS\u00248__locals1 = gpbddmdjfha;
                // ISSUE: reference to a compiler-generated field
                cbgclobpbda.unit = this.PlayersList[index];
                // ISSUE: reference to a compiler-generated field
                cbgclobpbda.index = index;
                // ISSUE: reference to a compiler-generated field
                if ((long) cbgclobpbda.unit.Hp > 0L)
                {
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  cbgclobpbda.unit.RespawnPos = BattleDefine.GetUnitRespawnPos(cbgclobpbda.index);
                  // ISSUE: reference to a compiler-generated field
                  cbgclobpbda.unit.SetSortOrderBack();
                }
                else
                {
                  // ISSUE: reference to a compiler-generated method
                  this.CreateUnit(UnitUtility.GetUnitResourceId(this.PlayersList[index].UnitId), new System.Action<UnitCtrl>(cbgclobpbda.RecreateDeadUnitsb__2), false);
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  cbgclobpbda.CS\u00248__locals1.recreatedUnitIndex.Add(cbgclobpbda.index);
                }
              }
              // ISSUE: reference to a compiler-generated field
              if (gpbddmdjfha.recreatedUnitIndex.Count == 0)
              {
                MELLIBIDKGI.Call();
              }
              else
              {
                // ISSUE: reference to a compiler-generated method
                this.resourceManager.StartLoad(new System.Action(gpbddmdjfha.RecreateDeadUnitsb__0));
                // ISSUE: reference to a compiler-generated method
                yield return (object) new WaitWhile(new Func<bool>(gpbddmdjfha.RecreateDeadUnitsb__1));
                MELLIBIDKGI.Call();
              }
            }*/
        }

        /*private void resetUnitListOrder()
        {
            this.UnitList.Clear();
            foreach (UnitCtrl unitCtrl in this.UnitUiCtrl.UnitCtrls)
            {
                if ((UnityEngine.Object)unitCtrl == (UnityEngine.Object)null)
                    break;
                this.UnitList.Add(unitCtrl);
            }
        }*/

        private void updateFrameWithSkip(UnitCtrl IMDKENFNOMF)
        {
            if (GameState == eBattleGameState.PLAY)
            {
                updateFrame();
            }
            else
            {
                IMDKENFNOMF.CutInFrameSet.CutInFrame = -1;
                IMDKENFNOMF.CutInFrameSet.ServerCutInFrame = -1;
                OnBlackOutEnd(IMDKENFNOMF);
            }
        }

        /*public void PlayBattleStart()
        {
            int num = this.battleTimeScale.IsSpeedQuadruple ? 1 : 0;
            float imdhejemlke = this.battleTimeScale.SpeedUpRate;
            this.Timer(0.5f / (num != 0 ? imdhejemlke : 1f), (System.Action)(() =>
           {
               this.soundManager.PlaySe(eSE.BTL_BATTLE_START);
               if (BootSystem.GetSkipBoot() || this.dialogManager.IsUse(eDialogId.BATTLE_MENU))
                   return;
               this.dialogManager.OpenFlatoutPlayer(DialogFlatoutPlayer.FlatoutType.BATTLE_START);
           }));
        }*/

        public void SetSpeedUpRate(float KBPCGPPDPLK)
        {
        }

        public void UpdateSpeedupFlag() => battleTimeScale.SpeedUpFlag = battleTimeScale.SpeedUpFlag;

        //public int GetLatestClearStarNum() => 3 - Mathf.Min(2, Array.FindAll<UnitCtrl>(this.UnitUiCtrl.UnitCtrls, (Predicate<UnitCtrl>)(JKIPJDENMKE => (UnityEngine.Object)JKIPJDENMKE != (UnityEngine.Object)null && JKIPJDENMKE.IsDead)).Length);

        public List<UnitCtrl> GetAliveUnitList() => UnitList.FindAll(IMDKENFNOMF => !IMDKENFNOMF.IsDead && !IMDKENFNOMF.IsSummonOrPhantom);

        public IEnumerator CallTimer(float EENBKIBPKLA, Action JMIONGNECLD) => this.Timer(EENBKIBPKLA, JMIONGNECLD);

        public IEnumerator CallTimer(Action JMIONGNECLD) => this.Timer(JMIONGNECLD);

        /* public IEnumerator CallFade(
           float OHPDCMHNOKJ,
           float KLPDMPIODKA,
           float FNGPHAODBAM,
           System.Action<float> EOGMKANIOKI,
           System.Action CCMFMOIIBEO) => this.Fade(OHPDCMHNOKJ, KLPDMPIODKA, FNGPHAODBAM, EOGMKANIOKI, CCMFMOIIBEO);*/

        //public int GetCurrentTowerExPartyIndex() => this.replayTempData.IsReplay ? this.replayTempData.CurrentPartyIndex : this.towerTempData.CurrentExPartyIndex;

        public UnitCtrl FindUnitForSoundUnitId(int id) => UnitList.Find(a => a.SoundUnitId == id);

        public UnitCtrl FindEnemyForResourceId(int id) => EnemyList.Find(a => a.UnitId == id);

        public void RestFrameCount() => FrameCount = 0;

        public void IncrementNoneVoiceAttackCount() => ++noneVoiceCount;

        public bool JudgeVoicePlay(int AGAPBBHBIJD, int LGFELHNKABN, float FMDLJBEBFOD)
        {
            int count = UnitList.FindAll(JNOIHMMFADD => (long)JNOIHMMFADD.Hp > 0L && !JNOIHMMFADD.IsSummonOrPhantom).Count;
            if (count == 1)
                return true;
            if (lastVoiceId != 0)
                --count;
            bool flag = HeldRandom(0, Mathf.Max(count - noneVoiceCount, 1) * 1000) <= (LGFELHNKABN - (double)FMDLJBEBFOD) * 1000.0;
            if (AGAPBBHBIJD != lastVoiceId && count == 0 | flag)
            {
                lastVoiceId = AGAPBBHBIJD;
                noneVoiceCount = 0;
                return true;
            }
            ++noneVoiceCount;
            return false;
        }

        public void TurnOffAllEffects()
        {
            //this.battleEffectManager.DisableAllEffect(false);
            battleEffectPool.DisableAllEffect(false);
        }

        //public void OffSkillExeScreen() => this.skillExeScreen.SetActive(false);

        public int MCLFFJEFMIF { get; set; }

        public void ExecField(AbnormalStateDataBase PFDAEFDOBIP, int OJHBHHCOAGK)
        {
            if (FieldDataDictionary.ContainsKey(OJHBHHCOAGK))
                FieldDataDictionary[OJHBHHCOAGK].Add(PFDAEFDOBIP);
            else
                FieldDataDictionary.Add(OJHBHHCOAGK, new List<AbnormalStateDataBase>
                {
          PFDAEFDOBIP
        });
            PFDAEFDOBIP.ALFDJACNNCL = PFDAEFDOBIP.LCHLGLAFJED == eFieldTargetType.ENEMY ? EnemyList : UnitList;
            PFDAEFDOBIP.StartField();
        }

        public void StopField(int OJHBHHCOAGK, eTargetAssignment OAHLOGOLMHD, bool MOBKHPNMEDM)
        {
            Dictionary<int, List<AbnormalStateDataBase>>.Enumerator enumerator = FieldDataDictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int key = enumerator.Current.Key;
                for (int index = 0; index < FieldDataDictionary[key].Count; ++index)
                {
                    if (OJHBHHCOAGK == 0 || OJHBHHCOAGK == key)
                        FieldDataDictionary[key][index].StopField(OAHLOGOLMHD, MOBKHPNMEDM);
                }
            }
        }

        public void RestartAbnormalStateField(UnitCtrl FNHGFDNICFG, UnitCtrl.eAbnormalState GMDFAELOLFL)
        {
            Dictionary<int, List<AbnormalStateDataBase>>.Enumerator enumerator = FieldDataDictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                List<AbnormalStateDataBase> plfcllhldooList = FieldDataDictionary[enumerator.Current.Key];
                for (int index = 0; index < plfcllhldooList.Count; ++index)
                    plfcllhldooList[index].ResetTarget(FNHGFDNICFG, GMDFAELOLFL);
            }
        }

        public bool ExistsField(int FieldDataId, bool isOther) => FieldDataDictionary.ContainsKey(FieldDataId) && FieldDataDictionary[FieldDataId].FindIndex(a => a.isPlaying && a.PPOJKIDHGNJ.IsOther == isOther) != -1;

        //public AEIBKBBBIHG GetBattleCameraEffectForBattleProcessor() => (AEIBKBBBIHG)this.battleCameraEffect;

        //public JNDPBIOCJPG GetIBattleTimeScaleForBattleProcessor() => (JNDPBIOCJPG)this.battleTimeScale;

        //public int GetSystemId() => this.battleProcessor.GetSystemId();

        /*public void OnChangeBattleCameraScale(bool JMHFDMPEPOK)
        {
            this.battleCameraEffect.orthographicSize = 2.044444f * (float)Screen.height / (float)Screen.width * this.battleCameraEffect.PNNMBKNLFCL;
            this.playCamera.orthographicSize = this.battleCameraEffect.orthographicSize;
            if (JMHFDMPEPOK)
                return;
            this.stageBgTex.transform.localScale = new Vector3(this.battleCameraEffect.PNNMBKNLFCL, this.battleCameraEffect.PNNMBKNLFCL);
        }*/

        //public void SetBattleCameraScale(float GNOCPLCBFFK) => this.battleCameraEffect.PNNMBKNLFCL = GNOCPLCBFFK;

        //public bool GetEnableShadowEffect() => this.battleProcessor.GetEnableShadowEffect();

        public void StartChangeScale(Skill COKKKOEFNAB, float KCLCLFDDAHK)
        {
            foreach (UnitCtrl unitCtrl in BlackoutUnitTargetList)
            {
                if (unitCtrl.IsScaleChangeTarget)
                {
                    unitCtrl.StartScaleChange(COKKKOEFNAB.ScaleChangers, KCLCLFDDAHK, COKKKOEFNAB.BlackOutTime);
                    unitCtrl.IsScaleChangeTarget = false;
                }
            }
        }
        public void StopScaleChange()
        {
            foreach (UnitCtrl blackoutUnitTarget in BlackoutUnitTargetList)
            {
                blackoutUnitTarget.StopScaleChange();
            }
        }
        public void SetForegroundEnable(bool GABGIKMFNFG) { }// => this.foregroundPanel.SetActive(GABGIKMFNFG);

        public float GetRespawnPos(eUnitRespawnPos OKDOFGMKNOJ) => BossUnit != null ? BattleDefine.RESPAWN_POS[OKDOFGMKNOJ] + BossUnit.AllUnitCenter : BattleDefine.RESPAWN_POS[OKDOFGMKNOJ];

        private void resetAllUnitY()
        {
            foreach (UnitCtrl unitCtrl in UnitList)
                unitCtrl.transform.SetLocalPosY(GetRespawnPos(unitCtrl.RespawnPos));
        }

        /*public void ChangeAllUnitDefaultShader()
        {
            for (int index = 0; index < this.UnitList.Count; ++index)
            {
                BattleSpineController currentSpineCtrl = this.UnitList[index].GetCurrentSpineCtrl();
                if ((UnityEngine.Object)currentSpineCtrl != (UnityEngine.Object)null)
                    currentSpineCtrl.ChangeDefaultShader();
            }
            for (int index = 0; index < this.EnemyList.Count; ++index)
            {
                BattleSpineController currentSpineCtrl = this.EnemyList[index].GetCurrentSpineCtrl();
                if ((UnityEngine.Object)currentSpineCtrl != (UnityEngine.Object)null)
                    currentSpineCtrl.ChangeDefaultShader();
            }
        }*/

        public List<UnitCtrl> BlackOutUnitList { get; private set; }

        public int GetBlackOutUnitLength() => BlackOutUnitList.Count;

        public UnitCtrl LPAAPDHAIIB { get; set; }

        public void SetStarted(bool AAFJOOCLABG) => isStarted = AAFJOOCLABG;

        public UnitCtrl HELHEEOHPFO { get; set; }

        public bool LJFFAACGDMF { get; set; }

        private UnitCtrl LNMLGGPGEIG { get; set; }

        public bool OCAMDIOPEFP { get; set; }

        private int FGBCOBGHGKE { get; set; }
        public UnitCtrl HBDACBKGHIK
        {
            get;
            set;
        }

        public void OnBlackOutEnd(UnitCtrl FNHGFDNICFG, bool ADJGECMDGDK = false)
        {
            if (FNHGFDNICFG == HBDACBKGHIK)
            {
                foreach (KeyValuePair<UnitCtrl.eAbnormalState, Action<bool>> item in FNHGFDNICFG.damageByBehaviourDictionary)
                {
                    item.Value.Call(JEOCPILJNAD: true);
                }
                HBDACBKGHIK = null;
            }
            ChargeSkillTurn = eChargeSkillTurn.NONE;
            //this.showTotalDamage();
            Action<IEnumerable<UnitCtrl>> action = list =>
            {
                foreach (var unitCtrl in list)
                {
                    // ISSUE: reference to a compiler-generated field
                    unitCtrl.CutInFrameSet.CutInFrame = -1;
                    // ISSUE: reference to a compiler-generated method
                    onEndFadeOut += () =>
                    {
                        if (!BlackOutUnitList.Contains(unitCtrl) && !BlackoutUnitTargetList.Contains(unitCtrl))
                        {
                            unitCtrl.SetSortOrderBack();
                        };
                    };
                }
            };
            action(BlackOutUnitList);
            action(BlackoutUnitTargetList);
            //this.onEndFadeOut += (System.Action)(() => this.battleEffectManager.SetSortOrderBack());
            BlackOutUnitList.Clear();
            BlackoutUnitTargetList.Clear();
            GamePause(false, true);
            AppendCoroutine(updateBlackOutFadeOut(FNHGFDNICFG, ADJGECMDGDK), ePauseType.IGNORE_BLACK_OUT);
        }

        public void OnCutInEnd(bool MOBKHPNMEDM, UnitCtrl FNHGFDNICFG)
        {
            LPAAPDHAIIB = null;
            //this.blockLayerManager.SetBlockDialog(true);
            if (this == null)
                return;
            StartCoroutine(resumeFrameCount());
            onSyncEnd(FNHGFDNICFG);
            if (FNHGFDNICFG.ConsumeEnergy() == eConsumeResult.FAILED || GameState != eBattleGameState.PLAY)
                FNHGFDNICFG.CutInFrameSet.ServerCutInFrame = -3;
            else
                FNHGFDNICFG.SetState(UnitCtrl.ActionState.SKILL_1);
            LJFFAACGDMF = false;
            IsPlayCutin = false;
            if (MOBKHPNMEDM)
                return;
            IsPlayCutin = false;
        }

        public void SetSkillExeScreen(
          UnitCtrl PKHLDNEDPBH,
          float blackOutTime,
          Color BHIJOLHCGKC,
          bool AJMFAOIFPKA)
        {
            ChargeSkillTurn = !PKHLDNEDPBH.IsOther ? eChargeSkillTurn.PLAYER : eChargeSkillTurn.ENEMY;
            LNMLGGPGEIG = PKHLDNEDPBH;
            FGBCOBGHGKE = ++PKHLDNEDPBH.UbUsedCount;
            //this.skillExeScreen.gameObject.SetActive(true);
            //this.skillExeScreenTexture.color = BHIJOLHCGKC;
            startalpha = BHIJOLHCGKC.a;
            if (!BlackOutUnitList.Contains(PKHLDNEDPBH))
                BlackOutUnitList.Add(PKHLDNEDPBH);
            if (blackoutTime - (double)blackoutTimeCounter < blackOutTime)
            {
                blackoutTimeCounter = 0.0f;
                blackoutTime = blackOutTime;
            }
            if (!isStarted)
            {
                isStarted = true;
                AppendCoroutine(updateBlackoutUnit(PKHLDNEDPBH, AJMFAOIFPKA), ePauseType.IGNORE_BLACK_OUT);
            }
            GamePause(false);
        }

        public void SetSkillExeScreenActive(UnitCtrl FNHGFDNICFG, Color BHIJOLHCGKC)
        {
            //this.skillExeScreen.sortingOrder = 11500;
            //this.skillExeScreen.transform.localPosition = Vector3.zero;
            //this.skillExeScreenTexture.color = BHIJOLHCGKC;
            LNMLGGPGEIG = FNHGFDNICFG;
            FGBCOBGHGKE = ++FNHGFDNICFG.UbUsedCount;
            //this.skillExeScreen.gameObject.SetActive(true);
        }
        /*public void StartBackgroundSpineAnimation(int DKLJDFCFCGH, float MLKPJFOGDGL)
        {
            if (IJDEOGJGADM != null)
            {
                string text = "battle_foreground_ub_" + DKLJDFCFCGH;
                if (IJDEOGJGADM.IsAnimation(text))
                {
                    IJDEOGJGADM.Depth = 22400;
                    IJDEOGJGADM.PlayAnime(text, _playLoop: false, 0f, _ignoreBlackout: true);
                    TrackEntry current = IJDEOGJGADM.state.GetCurrent(0);
                    current.lastTime = MLKPJFOGDGL;
                    current.time = MLKPJFOGDGL;
                }
            }
            if (MHBAMILFOMA != null)
            {
                string text2 = "battle_middleground_ub_" + DKLJDFCFCGH;
                if (MHBAMILFOMA.IsAnimation(text2))
                {
                    MHBAMILFOMA.Depth = 12180;
                    MHBAMILFOMA.PlayAnime(text2, _playLoop: false, 0f, _ignoreBlackout: true);
                    TrackEntry current2 = MHBAMILFOMA.state.GetCurrent(0);
                    current2.lastTime = MLKPJFOGDGL;
                    current2.time = MLKPJFOGDGL;
                }
            }
        }*/

        private IEnumerator updateBlackoutUnit(UnitCtrl FNHGFDNICFG, bool AJMFAOIFPKA)
        {
            UnitCtrl blackOutUnit = FNHGFDNICFG;
            while (true)
            {
                if (AJMFAOIFPKA && FNHGFDNICFG.IsDead)
                    blackoutTime = 0.0f;
                blackoutTimeCounter += DeltaTime_60fps;
                if (blackOutUnit != LNMLGGPGEIG)
                    blackOutUnit = LNMLGGPGEIG;
                if (blackoutTimeCounter <= (double)blackoutTime)
                    yield return null;
                else
                    break;
            }
            blackoutTime = 0.0f;
            OnBlackOutEnd(blackOutUnit);
            isStarted = false;
        }

        public void SetBlackoutTimeZero() => blackoutTime = 0.0f;

        private IEnumerator updateBlackOutFadeOut(UnitCtrl IMDKENFNOMF, bool ADJGECMDGDK)
        {
            int tmpUbUsedCount = FGBCOBGHGKE;
            float fadeoutTime = fadeoutDuration;
            while (true)
            {
                fadeoutTime -= DeltaTime_60fps;
                bool flag = IMDKENFNOMF != null && LNMLGGPGEIG == IMDKENFNOMF && tmpUbUsedCount != IMDKENFNOMF.UbUsedCount;
                if (!(LNMLGGPGEIG != IMDKENFNOMF | flag | ADJGECMDGDK) && !OCAMDIOPEFP)
                {
                    if (fadeoutTime > 0.0)
                    {
                        //this.skillExeScreenTexture.alpha = this.startalpha * fadeoutTime / this.fadeoutDuration;
                        yield return null;
                    }
                    else
                        goto label_7;
                }
                else
                    break;
            }
            OCAMDIOPEFP = false;
            FinishBlackFadeOut(IMDKENFNOMF);
            if (!ADJGECMDGDK)
            {
                yield break;
            }

            //this.skillExeScreen.gameObject.SetActive(false);
            yield break;
            label_7:
            //this.skillExeScreen.gameObject.SetActive(false);
            FinishBlackFadeOut(IMDKENFNOMF);
        }

        public void AddBlackOutTarget(
          UnitCtrl JELADBAMFKH,
          UnitCtrl LIMEKPEENOB,
          BasePartsData IMFNIHLGODB)
        {
            IMFNIHLGODB.IsBlackoutTarget = true;
            if (!LIMEKPEENOB.DisableSortOrderFrontOnBlackoutTarget)
                LIMEKPEENOB.SetSortOrderFront();
            LIMEKPEENOB.ResetTotalDamage();
            if (BlackoutUnitTargetList.Contains(LIMEKPEENOB))
                return;
            BlackoutUnitTargetList.Add(LIMEKPEENOB);
            //if (JELADBAMFKH.UbResponceVoiceType == eUbResponceVoiceType.THANKS)
            //    LIMEKPEENOB.ThanksTargetUnitId = JELADBAMFKH.UnitId;
            if (!(LIMEKPEENOB != JELADBAMFKH))
                return;
            LIMEKPEENOB.PlayDamageWhenIdle();
        }

        public void FinishBlackFadeOut(UnitCtrl FNHGFDNICFG)
        {
            //if ((UnityEngine.Object)this.FICPCPIFKCD != (UnityEngine.Object)null)
               //this.FICPCPIFKCD.SetSortOrderBack();
            if (onEndFadeOut == null)
                return;
            onEndFadeOut();
            onEndFadeOut = null;
            //this.skillExeScreenTexture.alpha = this.startalpha;
            //if ((double)this.battleCameraEffect.PNNMBKNLFCL == 1.0)
            //    return;
            //this.AppendCoroutine(this.cameraSizeUpdate(FNHGFDNICFG), ePauseType.NO_DIALOG);
        }

        /*private IEnumerator cameraSizeUpdate(UnitCtrl FNHGFDNICFG)
        {
            float time = 0.2f;
            float end = 2.044444f * (float)Screen.height / (float)Screen.width * this.battleCameraEffect.PNNMBKNLFCL;
            CustomEasing easing = new CustomEasing(CustomEasing.eType.outQuad, this.battleCameraEffect.orthographicSize, end, time);
            while ((double)time > 0.0)
            {
                if (this.ChargeSkillTurn != eChargeSkillTurn.NONE || (UnityEngine.Object)FNHGFDNICFG != (UnityEngine.Object)null && this.battleCameraEffect.JudgeStopEndZoom(FNHGFDNICFG))
                {
                    yield break;
                }
                else
                {
                    this.battleCameraEffect.orthographicSize = easing.GetCurVal(this.DeltaTime_60fps);
                    this.playCamera.orthographicSize = this.battleCameraEffect.orthographicSize;
                    time -= this.DeltaTime_60fps;
                    yield return (object)null;
                }
            }
            this.battleCameraEffect.orthographicSize = end;
            this.playCamera.orthographicSize = this.battleCameraEffect.orthographicSize;
        }*/

        /*private void showTotalDamage()
        {
            int num = 0;
            for (int index = 0; index < this.BlackOutUnitList.Count; ++index)
                this.BlackOutUnitList[index].ShowLifeStealNum();
            for (int index1 = 0; index1 < this.BlackoutUnitTargetList.Count; ++index1)
            {
                UnitCtrl unitCtrl = this.BlackoutUnitTargetList[index1];
                if (!unitCtrl.IsPartsBoss)
                {
                    num += unitCtrl.GetFirstParts(true).JudgeShowTotalDamage() ? 1 : 0;
                }
                else
                {
                    for (int index2 = 0; index2 < unitCtrl.BossPartsListForBattle.Count; ++index2)
                        num += unitCtrl.BossPartsListForBattle[index2].JudgeShowTotalDamage() ? 1 : 0;
                }
            }
            List<UnitCtrl> unitCtrlList = new List<UnitCtrl>((IEnumerable<UnitCtrl>)this.BlackoutUnitTargetList);
            unitCtrlList.Sort((Comparison<UnitCtrl>)((IPJGCOBNHLB, IMMPDMOKFGC) => IMMPDMOKFGC.RespawnPos.CompareTo((object)IPJGCOBNHLB.RespawnPos)));
            float _delay = 0.0f;
            for (int index1 = 0; index1 < unitCtrlList.Count; ++index1)
            {
                UnitCtrl unitCtrl = unitCtrlList[index1];
                if (!unitCtrl.IsPartsBoss)
                {
                    BasePartsData firstParts = unitCtrl.GetFirstParts(true);
                    if (firstParts.JudgeShowTotalDamage())
                    {
                        this.StartCoroutine(firstParts.StartTotalDamage(num == 1, _delay));
                        _delay += 0.06f;
                    }
                }
                else
                {
                    for (int index2 = 0; index2 < unitCtrl.BossPartsListForBattle.Count; ++index2)
                    {
                        PartsData partsData = unitCtrl.BossPartsListForBattle[index2];
                        if (partsData.JudgeShowTotalDamage())
                        {
                            this.StartCoroutine(partsData.StartTotalDamage(num == 1, _delay));
                            _delay += 0.06f;
                        }
                    }
                }
            }
        }*/

        /*public void HideDamageNumBySkillScreen()
        {
            this.LJFFAACGDMF = true;
            this.skillExeScreen.transform.localPosition = Vector3.zero;
            this.skillExeScreen.sortingOrder = 11500;
        }*/

        //public void SetSkillScreen(bool GABGIKMFNFG) => this.skillExeScreen.gameObject.SetActive(GABGIKMFNFG);

        public Dictionary<int, int> PFDJJPGCDCE { get; private set; } = new Dictionary<int, int>();

        public Dictionary<int, int> PKAEGMJPJAH { get; private set; } = new Dictionary<int, int>();

        //public void RevivalAtFinishBattle() => this.StartCoroutine(this.RecreateDeadUnits((System.Action)(() => this.CallbackRequestFinishBattle()), this.battleResult));

        private bool battleFinished;
        private void finishBattle(eBattleResult JJHEBNEEHPB)
        {
            battleFinished = true;
            skipping = false;
            /*if (BattleHeaderController.Instance.IsPaused && this.dialogManager.IsUse(eDialogId.BATTLE_MENU))
                this.dialogManager.ForceCloseOne(eDialogId.BATTLE_MENU);*/
            MyGameCtrl.Instance.OnBattleFinished((int)JJHEBNEEHPB);
            UnityEngine.Random.state = tempRandomState;
            battleResult = JJHEBNEEHPB;
            isPauseTimeLimit = true;
            battleTimeScale.SetBaseTimeScale(1f);
            battleTimeScale.SpeedUpFlag = false;
            for (int index = 0; index < EnemyList.Count; ++index)
                EnemyList[index].IdleOnly = true;
            for (int index = 0; index < UnitList.Count; ++index)
                UnitList[index].IdleOnly = true;
            createHpList();
            /*this.battleProcessor.FinishBattle();*/
        }

        /*private List<UnitDamageInfo> createDamageUnitInfoList(
          int OEBKGIDIPOH,
          int JJBBKJJPNEM)
        {
            for (int index = 0; index < this.tempData.KIDLPMAIICI.Count; ++index)
            {
                UnitDamageInfo unitDamageInfo = this.tempData.KIDLPMAIICI[index];
                int unitId = unitDamageInfo.unit_id;
                int EGJLOFBDBPP = 0;
                unitDamageInfo.SetViewerId(!BattleUtil.IsQuestSupportUnit(this.BattleCategory, unitId) ? this.battleProcessor.GetMainPlayerViewerId(EGJLOFBDBPP, unitId) : this.tempData.LBHMKBLOJOB.ViewerId);
            }
            List<UnitDamageInfo> unitDamageInfoList = new List<UnitDamageInfo>((IEnumerable<UnitDamageInfo>)this.tempData.KIDLPMAIICI);
            for (int index = 0; index < this.tempData.DDMKANMNCAA.Count; ++index)
                this.tempData.DDMKANMNCAA[index].SetViewerId(OEBKGIDIPOH);
            unitDamageInfoList.AddRange((IEnumerable<UnitDamageInfo>)this.tempData.DDMKANMNCAA);
            return unitDamageInfoList;
        }*/

        /*public List<UnitHpInfo> CreateUnitHpInfoList(int ViewerId1, int ViewerId2)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            //BattleManager.PDPGCADHPKD pdpgcadhpkd = new BattleManager.PDPGCADHPKD();
            // ISSUE: reference to a compiler-generated field
            List<UnitHpInfo> hpList = new List<UnitHpInfo>();
            // ISSUE: reference to a compiler-generated method
            System.Action<List<UnitCtrl>, int> action = (a, b) =>
            {
                foreach (UnitCtrl ctrl in a)
                {
                    if (!ctrl.IsSummonOrPhantom)
                    {
                        UnitHpInfo hpInfo = new UnitHpInfo();
                        hpInfo.SetUnitId(ctrl.UnitId);
                        hpInfo.SetViewerId(b);
                        hpInfo.SetHp((int)ctrl.Hp);
                        hpList.Add(hpInfo);
                    }
                }
            };
            action(this.UnitList, ViewerId1);
            action(this.EnemyList, ViewerId2);
            // ISSUE: reference to a compiler-generated field
            return hpList;
        }*/

        /*public List<UnitHpInfoForFriendBattle> CreateUnitHpInfoListForFriendBattle(
          int OEBKGIDIPOH,
          int JJBBKJJPNEM)
        {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BattleManager.JBNLMPNOBFG jbnlmpnobfg = new BattleManager.JBNLMPNOBFG();
      // ISSUE: reference to a compiler-generated field
      jbnlmpnobfg.hpList = new List<UnitHpInfoForFriendBattle>();
      // ISSUE: reference to a compiler-generated method
      System.Action<List<UnitCtrl>, int, Dictionary<int, int>> action = new System.Action<List<UnitCtrl>, int, Dictionary<int, int>>(jbnlmpnobfg.CreateUnitHpInfoListForFriendBattleb__0);
      action(this.PlayersList, JJBBKJJPNEM, this.PFDJJPGCDCE);
      action(this.EnemiseList, OEBKGIDIPOH, this.PKAEGMJPJAH);
      // ISSUE: reference to a compiler-generated field
      return jbnlmpnobfg.hpList;
            return new List<UnitHpInfoForFriendBattle>();
        }*/

        /*public void ResultApiSendExec(System.Action NOOKODFHPGN = null, System.Action<int> BFOHKHBAHNG = null)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: reference to a compiler-generated method
            System.Action action = () =>
            {
                if (BattleLogEnable)
                {
                    battleLog.AddSendBattleLogCsvPostParam();
                }
                if (apiManager.TaskCount <= 0)
                {
                    NOOKODFHPGN?.Invoke();
                }
                else
                {
                    //apiManager.Exec()
                }
            };
            if ((double)this.battleCameraEffect.PNNMBKNLFCL != 1.0)
                this.StartCoroutine(this.cameraZoomReset(action));
            else
                action.Call();
        }*/

        /*private IEnumerator cameraZoomReset(System.Action MELLIBIDKGI)
        {
            float time = 0.2f;
            CustomEasing easing = new CustomEasing(CustomEasing.eType.outQuad, this.battleCameraEffect.PNNMBKNLFCL, 1f, time);
            while ((double)time > 0.0)
            {
                this.battleCameraEffect.PNNMBKNLFCL = easing.GetCurVal(this.DeltaTime_60fps);
                this.OnChangeBattleCameraScale(true);
                time -= this.DeltaTime_60fps;
                yield return (object)null;
            }
            MELLIBIDKGI.Call();
        }*/

        /*public void CallbackRequestFinishBattle()
        {
            if ((this.battleResult == eBattleResult.LOSE ? 1 : (this.battleResult == eBattleResult.TIME_OVER ? 1 : 0)) != 0)
            {
                List<UnitCtrl> all = this.EnemyList.FindAll((Predicate<UnitCtrl>)(JNOIHMMFADD => JNOIHMMFADD.IsDivision));
                if (all.Count > 0)
                {
                    for (int index = 0; index < all.Count; ++index)
                        all[index].DisappearForDivision(true, true);
                    UnitCtrl summonSource = all[0].SummonSource;
                    this.EnemyList.Add(summonSource);
                    bool _isTimeOver = this.battleResult == eBattleResult.TIME_OVER;
                    summonSource.StartUndoDivision(new System.Action(this.waitAndStartUnitMove), _isTimeOver);
                    summonSource.gameObject.SetActive(true);
                    return;
                }
            }
            this.waitAndStartUnitMove();
        }*/

        private void createHpList()
        {
            if (BattleCategory != eBattleCategory.FRIEND)
                return;
            List<UnitCtrl> all1 = UnitList.FindAll(IEODDJAPMND => !IEODDJAPMND.IsSummonOrPhantom);
            List<UnitCtrl> all2 = EnemyList.FindAll(IEODDJAPMND => !IEODDJAPMND.IsSummonOrPhantom);
            PFDJJPGCDCE.Clear();
            PKAEGMJPJAH.Clear();
            foreach (UnitCtrl unitCtrl in all1)
                PFDJJPGCDCE.Add(unitCtrl.UnitId, Mathf.CeilToInt((float)((long)unitCtrl.Hp / (double)(long)unitCtrl.MaxHp * 1000.0)));
            foreach (UnitCtrl unitCtrl in all2)
                PKAEGMJPJAH.Add(unitCtrl.UnitId, Mathf.CeilToInt((float)((long)unitCtrl.Hp / (double)(long)unitCtrl.MaxHp * 1000.0)));
        }

        //private void waitAndStartUnitMove() => this.Timer(this.battleProcessor.GetWaitTimeWhenFinishBattle(), (System.Action)(() => this.startUnitMove()));

        /*public void SetupLosePlayVoiceUnitId()
        {
            if (this.NAIOBCOHILK)
            {
                this.AMNJIJHNJGC = (int)ManagerSingleton<MasterDataManager>.Instance.masterRarity6QuestData.GetWithRarity6QuestId(this.tempData.OIPDBCEJAPK).unit_id;
                this.PJLABNLCPPE = false;
            }
            else
                BattleVoiceUtility.SelectLoseVoiceUnit(this.UnitList);
        }*/

        /*public void StartCoroutineBossLose()
        {
            this.StartCoroutine(this.bossLoseCoroutine());
            if (!this.skillExeScreenTexture.gameObject.activeSelf)
                return;
            this.StartCoroutine(this.fadeOutSkillExeScreenWhenTimeUp());
        }*/

        //public void OpenTowerExResultDialog(bool BMIMCCODFLE, int JKMKALOABIB, bool EOGHNGJLKFF) => this.dialogManager.OpenTowerExResult(this.viewBattle, BMIMCCODFLE, JKMKALOABIB, EOGHNGJLKFF);

        //public void OpenResultWinDialog() => this.viewBattle.OpenResultWin(this.BattleCategory);

        /*private void startUnitMove()
        {
            this.UnitUiCtrl.gameObject.SetActive(false);
            this.tempData.PHDACAOAOMA.AFLFKLOLPNF = this.battleResult == eBattleResult.WIN;
            switch (this.battleResult)
            {
                case eBattleResult.WIN:
                    this.battleProcessor.WinProcess();
                    this.AppendCoroutine(this.initializeAndUpdateWalkPlayer((System.Action)(() =>
                   {
                       this.battleProcessor.PlayWinPerformance();
                       this.StartResult();
                   })), ePauseType.SYSTEM);
                    break;
                case eBattleResult.LOSE:
                    this.battleProcessor.LoseProcess();
                    break;
                case eBattleResult.TIME_OVER:
                    if (this.battleProcessor.JudgeWinWhenTimeUp())
                    {
                        List<UnitCtrl> all1 = this.UnitList.FindAll((Predicate<UnitCtrl>)(IEODDJAPMND => !IEODDJAPMND.IsDead && !IEODDJAPMND.IsSummonOrPhantom));
                        List<UnitCtrl> all2 = this.EnemyList.FindAll((Predicate<UnitCtrl>)(IEODDJAPMND => !IEODDJAPMND.IsDead && !IEODDJAPMND.IsSummonOrPhantom));
                        int count1 = all1.Count;
                        int count2 = all2.Count;
                        if (count1 == count2)
                        {
                            int num1 = 0;
                            foreach (KeyValuePair<int, int> keyValuePair in this.PFDJJPGCDCE)
                                num1 += keyValuePair.Value;
                            int num2 = 0;
                            foreach (KeyValuePair<int, int> keyValuePair in this.PKAEGMJPJAH)
                                num2 += keyValuePair.Value;
                            if (num1 == num2)
                            {
                                long num3 = 0;
                                long num4 = 0;
                                foreach (UnitCtrl unitCtrl in all1)
                                    num3 += (long)unitCtrl.Hp;
                                foreach (UnitCtrl unitCtrl in all2)
                                    num4 += (long)unitCtrl.Hp;
                                this.timeUpWin = num3 >= num4;
                            }
                            else
                                this.timeUpWin = num1 >= num2;
                        }
                        else
                            this.timeUpWin = count1 > count2;
                        if (this.timeUpWin)
                        {
                            this.tempData.PHDACAOAOMA.AFLFKLOLPNF = true;
                            this.battleProcessor.TimeUpWinProcess();
                            this.StartCoroutine(this.initializeAndUpdateWalkPlayer((System.Action)(() =>
                           {
                               this.battleProcessor.PlayWinPerformance();
                               this.StartResult();
                           })));
                            break;
                        }
                    }
                    this.battleProcessor.LoseProcess();
                    break;
                default:
                    this.battleResult = eBattleResult.LOSE;
                    break;
            }
        }*/

        //public void PlayFlatoutAfterMove() => this.viewBattle.PlayFlatoutAfterMove(new System.Action(this.PlayPartyUnitJoyMotion));

        /* private IEnumerator initializeAndUpdateWalkPlayer(System.Action KNMHJNLCKDH)
         {
             BattleManager battleManager = this;
             yield return (object)battleManager.StartCoroutine(battleManager.battleProcessor.LoadFlatOutResource());
             // ISSUE: explicit non-virtual call
             float[][] speedRandArray = new float[3][]
             {
         new float[ (battleManager.UnitUiCtrl).UnitCtrls.Length],
         null,
         null
             };
             int index1 = 0;
             // ISSUE: explicit non-virtual call
             for (int length = (battleManager.UnitUiCtrl).UnitCtrls.Length; index1 < length; ++index1)
             {
                 // ISSUE: explicit non-virtual call
                 UnitCtrl unitCtrl = (battleManager.UnitUiCtrl).UnitCtrls[index1];
                 battleManager.questWinPositionList.Add(new Vector2(0.0f, -100f));
                 if ((UnityEngine.Object)unitCtrl == (UnityEngine.Object)null || unitCtrl.IsDead)
                 {
                     if (index1 != 0)
                         battleManager.questWinPositionList[index1] = battleManager.questWinPositionList[index1 - 1];
                     else
                         battleManager.questWinPositionList[0] = new Vector2(140f, 0.0f);
                 }
                 else
                 {
                     for (int index2 = 0; index2 < battleManager.questWinPositionList.Count - 1; ++index2)
                     {
                         Vector2 questWinPosition = battleManager.questWinPositionList[index2];
                         questWinPosition.x += 140f;
                         battleManager.questWinPositionList[index2] = questWinPosition;
                     }
                     if (index1 != 0)
                     {
                         Vector2 questWinPosition = battleManager.questWinPositionList[battleManager.questWinPositionList.Count - 1];
                         questWinPosition.x += battleManager.questWinPositionList[battleManager.questWinPositionList.Count - 2].x - 280f;
                         battleManager.questWinPositionList[battleManager.questWinPositionList.Count - 1] = questWinPosition;
                     }
                 }
             }
             int index3 = 0;
             // ISSUE: explicit non-virtual call
             for (int length = (battleManager.UnitUiCtrl).UnitCtrls.Length; index3 < length; ++index3)
             {
                 // ISSUE: explicit non-virtual call
                 UnitCtrl unitCtrl = (battleManager.UnitUiCtrl).UnitCtrls[index3];
                 if ((UnityEngine.Object)unitCtrl != (UnityEngine.Object)null)
                 {
                     speedRandArray[0][index3] = BattleManager.Random(0.8f, 1.2f);
                     float x = battleManager.questWinPositionList[index3].x;
                     bool bLeftDir = (double)unitCtrl.transform.localPosition.x > (double)x;
                     unitCtrl.SetLeftDirection(bLeftDir);
                     unitCtrl.GetCurrentSpineCtrl().CurColor = Color.white;
                     unitCtrl.PlayAnime(eSpineCharacterAnimeId.RUN, unitCtrl.MotionPrefix);
                 }
             }
             while (true)
             {
                 bool BHAOIOGGDDC = false;
                 // ISSUE: explicit non-virtual call
                 if (battleManager.unitCtrlsWalk((battleManager.UnitUiCtrl).UnitCtrls, speedRandArray, BHAOIOGGDDC, 0))
                     yield return (object)null;
                 else
                     break;
             }
             float time = 0.0f;
             while (true)
             {
                 // ISSUE: explicit non-virtual call
                 time += (battleManager.DeltaTime_60fps);
                 if ((double)time <= 0.600000023841858)
                     yield return (object)null;
                 else
                     break;
             }
             KNMHJNLCKDH.Call();
         }*/

        /*private bool unitCtrlsWalk(
          UnitCtrl[] HCNCJBCMOFK,
          float[][] IPNNKFEIIGH,
          bool BHAOIOGGDDC,
          int NCEHAOBBDJC)
        {
            int index = 0;
            for (int length = HCNCJBCMOFK.Length; index < length; ++index)
            {
                UnitCtrl unitCtrl = HCNCJBCMOFK[index];
                if ((UnityEngine.Object)unitCtrl != (UnityEngine.Object)null)
                {
                    Vector2 questWinPosition = this.questWinPositionList[index];
                    Vector3 localPosition = unitCtrl.transform.localPosition;
                    if (!this.isReached(unitCtrl.IsLeftDir, (Vector2)localPosition, questWinPosition))
                    {
                        Vector2 vector2 = questWinPosition - (Vector2)localPosition;
                        Vector3 vector3 = localPosition + (Vector3)vector2.normalized * 1600f * this.DeltaTime_60fps * IPNNKFEIIGH[NCEHAOBBDJC][index];
                        if (this.isReached(unitCtrl.IsLeftDir, (Vector2)vector3, questWinPosition))
                            vector3 = (Vector3)questWinPosition;
                        unitCtrl.transform.localPosition = vector3;
                        BHAOIOGGDDC = true;
                    }
                    else if ((double)IPNNKFEIIGH[NCEHAOBBDJC][index] != 0.0)
                    {
                        unitCtrl.transform.localPosition = (Vector3)questWinPosition;
                        unitCtrl.PlayAnime(eSpineCharacterAnimeId.IDLE, unitCtrl.MotionPrefix);
                        IPNNKFEIIGH[NCEHAOBBDJC][index] = 0.0f;
                    }
                }
            }
            return BHAOIOGGDDC;
        }*/

        private bool isReached(bool IPLFAFOAGOA, Vector2 HBJAAHCJMMP, Vector2 AFLDBMLKLKF)
        {
            bool flag = IPLFAFOAGOA ? AFLDBMLKLKF.x >= (double)HBJAAHCJMMP.x : HBJAAHCJMMP.x >= (double)AFLDBMLKLKF.x;
            return Mathf.Abs(HBJAAHCJMMP.x - AFLDBMLKLKF.x) <= 0.100000001490116 | flag;
        }

        /*public void PlayPartyUnitJoyMotion()
        {
            Dictionary<int, ResultMotionInfo> motionDictionary = this.createCombineMotionDictionary();
            for (int index = 0; index < this.UnitList.Count; ++index)
            {
                UnitCtrl unitCtrl = this.UnitList[index];
                if (!unitCtrl.IsDead && !unitCtrl.IsSummonOrPhantom)
                {
                    unitCtrl.SetLeftDirection(false);
                    ResultMotionInfo _combineMotion = new ResultMotionInfo(0, 0);
                    if (motionDictionary.TryGetValue(unitCtrl.UnitId, out _combineMotion))
                    {
                        unitCtrl.UnitSpineCtrl.LoadAnimationIDImmediately(eSpineBinaryAnimationId.COMBINE_JOY_RESULT, _combineMotion.MotionId);
                        unitCtrl.PlaySetResult(_combineMotion);
                    }
                    else
                        unitCtrl.PlayJoyResult();
                }
            }
        }*/

        /*private Dictionary<int, ResultMotionInfo> createCombineMotionDictionary()
        {
            UnitCtrl[] unitCtrls = this.UnitUiCtrl.UnitCtrls;
            bool FCLOJKKGBGP = this.isRecreateDeadUnit();
            int[] HGHDDOEFAMP1 = new int[5];
            int[] FEGCKNIJDHO = new int[5];
            this.initCombineMotionArrays(unitCtrls, HGHDDOEFAMP1, FEGCKNIJDHO, FCLOJKKGBGP);
            int num1 = 0;
            for (int index = 0; index < HGHDDOEFAMP1.Length; ++index)
            {
                if (HGHDDOEFAMP1[index] != 0)
                    ++num1;
            }
            Dictionary<int, ResultMotionInfo> dictionary = new Dictionary<int, ResultMotionInfo>();
            MasterCombinedResultMotion combinedResultMotion1 = ManagerSingleton<MasterDataManager>.Instance.masterCombinedResultMotion;
            int[] HGHDDOEFAMP2 = new int[5];
            IEnumerator<KeyValuePair<int, MasterCombinedResultMotion.CombinedResultMotion>> enumerator = combinedResultMotion1.dictionary.OrderByDescending<KeyValuePair<int, MasterCombinedResultMotion.CombinedResultMotion>, int>((Func<KeyValuePair<int, MasterCombinedResultMotion.CombinedResultMotion>, int>)(PPJCKFEBNLN => PPJCKFEBNLN.Value.SetUnitNum)).GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<int, MasterCombinedResultMotion.CombinedResultMotion> current = enumerator.Current;
                MasterCombinedResultMotion.CombinedResultMotion combinedResultMotion2 = current.Value;
                int[] unitIdArray = combinedResultMotion2.UnitIdArray;
                int setUnitNum = combinedResultMotion2.SetUnitNum;
                if (setUnitNum <= num1)
                {
                    Array.Copy((Array)FEGCKNIJDHO, (Array)HGHDDOEFAMP2, 5);
                    int num2 = 5 - setUnitNum;
                    for (int sourceIndex = 0; sourceIndex <= num2; ++sourceIndex)
                    {
                        Array.Copy((Array)HGHDDOEFAMP1, sourceIndex, (Array)HGHDDOEFAMP2, 0, setUnitNum);
                        if (this.checkIsMatchCombination(HGHDDOEFAMP2, unitIdArray))
                        {
                            num1 -= setUnitNum;
                            int _baseDepth = int.MaxValue;
                            for (int index = 0; index < setUnitNum; ++index)
                            {
                                // ISSUE: object of a compiler-generated type is created
                                // ISSUE: reference to a compiler-generated method
                                int unitSortOrder = BattleDefine.GetUnitSortOrder(((IEnumerable<UnitCtrl>)unitCtrls).ToList<UnitCtrl>().Find(a => a.UnitId == unitIdArray[index]));
                                if (unitSortOrder < _baseDepth)
                                    _baseDepth = unitSortOrder;
                            }
                            for (int index = sourceIndex; index < sourceIndex + setUnitNum; ++index)
                            {
                                int dispOrder = current.Value.DispOrderArray[index - sourceIndex];
                                int resultUnitSortOrder = BattleDefine.GetResultUnitSortOrder(_baseDepth, dispOrder);
                                dictionary.Add(HGHDDOEFAMP1[index], new ResultMotionInfo(current.Key, resultUnitSortOrder));
                                HGHDDOEFAMP1[index] = 0;
                            }
                            break;
                        }
                    }
                    if (num1 < 2)
                        break;
                }
            }
            return dictionary;
        }*/

        /*private void initCombineMotionArrays(
          UnitCtrl[] MJKHFAIKNHD,
          int[] HGHDDOEFAMP,
          int[] FEGCKNIJDHO,
          bool FCLOJKKGBGP)
        {
            List<int> intList = new List<int>();
            for (int index = 0; index < 5; ++index)
            {
                UnitCtrl unitCtrl = MJKHFAIKNHD[index];
                if (FCLOJKKGBGP)
                {
                    if ((UnityEngine.Object)unitCtrl != (UnityEngine.Object)null)
                        intList.Add(unitCtrl.UnitId);
                }
                else if ((UnityEngine.Object)unitCtrl != (UnityEngine.Object)null && !unitCtrl.IsDead)
                    intList.Add(unitCtrl.UnitId);
                HGHDDOEFAMP[index] = 0;
                FEGCKNIJDHO[index] = 0;
            }
            intList.Reverse();
            for (int index = 0; index < intList.Count; ++index)
                HGHDDOEFAMP[index] = intList[index];
        }*/

        /*private bool checkIsMatchCombination(int[] HGHDDOEFAMP, int[] AFEBIHBMNPG)
        {
            for (int index = 0; index < 5; ++index)
            {
                if (HGHDDOEFAMP[index] != AFEBIHBMNPG[index])
                    return false;
            }
            return true;
        }*/

        private bool isRecreateDeadUnit()
        {
            switch (BattleCategory)
            {
                case eBattleCategory.DUNGEON:
                case eBattleCategory.GRAND_ARENA:
                case eBattleCategory.GRAND_ARENA_REPLAY:
                case eBattleCategory.QUEST_REPLAY:
                case eBattleCategory.TOWER:
                case eBattleCategory.TOWER_EX:
                case eBattleCategory.TOWER_REPLAY:
                case eBattleCategory.TOWER_EX_REPLAY:
                case eBattleCategory.TOWER_CLOISTER:
                    return false;
                default:
                    return true;
            }
        }

        /*private void playEnemyResultMotion()
        {
            for (int index = 0; index < this.EnemyList.Count; ++index)
            {
                UnitCtrl FNHGFDNICFG = this.EnemyList[index];
                if ((!this.DMALFANMBMM && !this.IsSpecialBattle || FNHGFDNICFG.IsBoss) && (!FNHGFDNICFG.IsDead && !FNHGFDNICFG.IsSummonOrPhantom))
                {
                    FNHGFDNICFG.SetLeftDirection(true);
                    this.StartCoroutine(this.waitResultMotion(FNHGFDNICFG));
                }
            }
        }*/

        /*private IEnumerator waitResultMotion(UnitCtrl FNHGFDNICFG)
        {
            BattleSpineController currentSpineCtrl = FNHGFDNICFG.GetCurrentSpineCtrl();
            eSpineCharacterAnimeId resultLoopAnimeId;
            eSpineCharacterAnimeId OALMMDFKEHJ;
            this.battleProcessor.GetResultAnimeId(currentSpineCtrl, out OALMMDFKEHJ, out resultLoopAnimeId);
            float duration;
            if (OALMMDFKEHJ == eSpineCharacterAnimeId.ATTACK)
            {
                FNHGFDNICFG.PlayAnime(OALMMDFKEHJ, _index3: FNHGFDNICFG.MotionPrefix, _isLoop: false);
                duration = currentSpineCtrl.state.Data.skeletonData.FindAnimation(currentSpineCtrl.ConvertAnimeIdToAnimeName(OALMMDFKEHJ, _index3: FNHGFDNICFG.MotionPrefix)).Duration;
            }
            else
            {
                FNHGFDNICFG.PlayAnime(OALMMDFKEHJ, FNHGFDNICFG.MotionPrefix, _isLoop: false);
                duration = currentSpineCtrl.state.Data.skeletonData.FindAnimation(currentSpineCtrl.ConvertAnimeIdToAnimeName(OALMMDFKEHJ, FNHGFDNICFG.MotionPrefix)).Duration;
            }
            yield return (object)new WaitForSeconds(duration);
            FNHGFDNICFG.PlayAnime(resultLoopAnimeId, FNHGFDNICFG.MotionPrefix);
        }*/

        //public void StartResult() => this.Timer(this.battleProcessor.GetResultWaitTime(), (System.Action)(() => this.battleProcessor.DrawTheCurtains()));

        /*public void ShowResultDialog()
        {
            this.battleTimeScale.SpeedUpFlag = false;
            bool _win = this.battleResult == eBattleResult.WIN;
            if (this.battleResult == eBattleResult.TIME_OVER && this.battleProcessor.JudgeWinWhenTimeUp())
                _win = this.timeUpWin;
            this.StartCoroutine(this.viewBattle.ShowResultDialog(this.BattleCategory, _win));
        }*/

        /*private IEnumerator fadeOutSkillExeScreenWhenTimeUp()
        {
            while (true)
            {
                float fadeoutDuration = this.fadeoutDuration;
                float alpha = this.skillExeScreenTexture.alpha;
                this.fadeoutDuration -= this.DeltaTime_60fps;
                if ((double)fadeoutDuration > 0.0)
                {
                    this.skillExeScreenTexture.alpha = alpha * fadeoutDuration / this.fadeoutDuration;
                    yield return (object)null;
                }
                else
                    break;
            }
            this.skillExeScreen.gameObject.SetActive(false);
        }*/

        /*public void ResetFieldTimeUp()
        {
            this.battleCameraEffect.orthographicSize = this.playCamera.orthographicSize = 2.044444f * (float)Screen.height / (float)Screen.width;
            this.playCamera.transform.position = this.LNKFACBHNBC;
            this.battleCameraEffect.HLMOOIHPKHO = true;
            this.battleTimeScale.SetBaseTimeScale(1f);
            this.battleEffectManager.DisableAllEffect(this.IsAuraRemainBattle());
            this.battleEffectPool.DisableAllEffect(this.IsAuraRemainBattle());
            this.skillExeScreen.SetActive(false);
        }*/

        /*private IEnumerator bossLoseCoroutine()
        {
            BattleManager battleManager = this;
            // ISSUE: explicit non-virtual call
            if ((battleManager.battleResult) == eBattleResult.TIME_OVER)
            {
                // ISSUE: explicit non-virtual call
                battleManager.ResetFieldTimeUp();
                // ISSUE: explicit non-virtual call
                battleManager.ForceFadeUnitOut();
                // ISSUE: explicit non-virtual call
                for (int index = 0; index < (battleManager.EnemyList).Count; ++index)
                {
                    // ISSUE: explicit non-virtual call
                    UnitCtrl unitCtrl = (battleManager.EnemyList)[index];
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    if ((!(battleManager.DMALFANMBMM) && !(battleManager.IsSpecialBattle) || unitCtrl.IsBoss) && (!unitCtrl.IsDead && !unitCtrl.IsSummonOrPhantom))
                    {
                        BattleSpineController currentSpineCtrl = unitCtrl.GetCurrentSpineCtrl();
                        currentSpineCtrl.Resume();
                        currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.IDLE, unitCtrl.MotionPrefix);
                        currentSpineCtrl.CurColor = Color.white;
                        // ISSUE: explicit non-virtual call
                        battleManager.StartCoroutine((battleManager.UpdateBossIdleMotion(currentSpineCtrl, unitCtrl.MotionPrefix)));
                        unitCtrl.SetSortOrderBack();
                    }
                }
                float time = Time.time;
                Color fadeColor = Color.white;
                while ((double)Time.time - (double)time < 0.5)
                {
                    fadeColor.a = (float)(1.0 - (double)(Time.time - time) / 0.5);
                    // ISSUE: explicit non-virtual call
                    for (int index = 0; index < (battleManager.UnitList).Count; ++index)
                    {
                        // ISSUE: explicit non-virtual call
                        UnitCtrl unitCtrl = (battleManager.UnitList)[index];
                        if (!unitCtrl.IsDead && !unitCtrl.IsSummonOrPhantom)
                            unitCtrl.GetCurrentSpineCtrl().CurColor = fadeColor;
                    }
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    if ((battleManager.DMALFANMBMM) || (battleManager.IsSpecialBattle))
                    {
                        // ISSUE: explicit non-virtual call
                        for (int index = 0; index < (battleManager.EnemyList).Count; ++index)
                        {
                            // ISSUE: explicit non-virtual call
                            UnitCtrl unitCtrl = (battleManager.EnemyList)[index];
                            if (!unitCtrl.IsBoss && !unitCtrl.IsDead)
                                unitCtrl.GetCurrentSpineCtrl().CurColor = fadeColor;
                        }
                    }
                    yield return (object)null;
                }
                // ISSUE: explicit non-virtual call
                for (int index = 0; index < (battleManager.UnitList).Count; ++index)
                {
                    // ISSUE: explicit non-virtual call
                    (battleManager.UnitList)[index].SetActive(false);
                }
                fadeColor = new Color();
            }
            // ISSUE: explicit non-virtual call
            // ISSUE: explicit non-virtual call
            if ((battleManager.DMALFANMBMM) || (battleManager.IsSpecialBattle))
            {
                // ISSUE: explicit non-virtual call
                for (int index = 0; index < (battleManager.EnemyList).Count; ++index)
                {
                    // ISSUE: explicit non-virtual call
                    if (!(battleManager.EnemyList)[index].IsBoss)
                    {
                        // ISSUE: explicit non-virtual call
                        (battleManager.EnemyList)[index].SetActive(false);
                    }
                    else
                    {
                        // ISSUE: explicit non-virtual call
                        (battleManager.EnemyList)[index].SetSortOrderBack();
                    }
                }
            }
            battleManager.battleCameraEffect.SetBgBlur(true);
            battleManager.viewBattle.PlayFlatoutAfterMove((System.Action)(() => { }));
            // ISSUE: reference to a compiler-generated method
            battleManager.Timer(0.3f, () => {
                playEnemyResultMotion();
                StartResult();
            });
        }*/

        public IEnumerator UpdateBossIdleMotion(
          BattleSpineController NLLGNCKLGHL,
          int FBBPPDMDEII)
        {
            float alpha = NLLGNCKLGHL.CurColor.a;
            while (true)
            {
                for (deltaTimeAccumulated += Time.deltaTime; deltaTimeAccumulated > 0.0; deltaTimeAccumulated -= DeltaTime_60fps)
                {
                    NLLGNCKLGHL.RealUpdate();
                    NLLGNCKLGHL.RealLateUpdate();
                }
                if (NLLGNCKLGHL.AnimationName == NLLGNCKLGHL.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DIE, _index2: FBBPPDMDEII) && !NLLGNCKLGHL.IsPlayAnime)
                {
                    alpha -= Time.deltaTime;
                    if (alpha < 0.0)
                        alpha = 0.0f;
                    NLLGNCKLGHL.CurColor = new Color(1f, 1f, 1f, alpha);
                }
                yield return null;
            }
        }

        public void ForceFadeUnitOut()
        {
            for (int index = 0; index < LPBCBINDJLJ.Count; ++index)
                LPBCBINDJLJ[index].ForceFadeOut();
        }

        /*private void stopAllAbnormalEffect(List<UnitCtrl> CNBJIKBCFIC)
        {
            for (int index = 0; index < CNBJIKBCFIC.Count; ++index)
                CNBJIKBCFIC[index].StopAbnormalEffect();
        }*/

        /*public List<TowerQueryUnit> CreateTowerQueryUnitList(
          bool JDBOMEHMOIA,
          bool IODJKFHFLKJ)
        {
            UserData instance = Singleton<UserData>.Instance;
            List<UnitCtrl> unitCtrlList = IODJKFHFLKJ ? this.UnitList : this.EnemyList;
            List<TowerQueryUnit> towerQueryUnitList = new List<TowerQueryUnit>();
            float num1 = (float)(int)ManagerSingleton<MasterDataManager>.Instance.masterTowerQuestData.Get(this.towerTempData.CurrentQuestId).recovery_tp_rate / 100f;
            bool flag1 = JDBOMEHMOIA && !IODJKFHFLKJ;
            bool flag2 = !IODJKFHFLKJ && !flag1;
            TowerSetting towerSetting = instance.InitSettingParameter.TowerSetting;
            int decrypted1 = towerSetting.ReduceEnemyEnergyValue.GetDecrypted();
            int decrypted2 = towerSetting.ReduceEnemyEnergyLowerLimit.GetDecrypted();
            for (int index = 0; index < unitCtrlList.Count; ++index)
            {
                UnitCtrl unitCtrl = unitCtrlList[index];
                int hp = (int)(long)unitCtrl.Hp;
                float b = unitCtrl.Energy;
                if (this.battleResult == eBattleResult.TIME_OVER & IODJKFHFLKJ)
                {
                    hp -= Mathf.Min((int)((double)((long)unitCtrl.MaxHp * (long)(int)towerSetting.TimeupHpPenalty) / 1000.0), hp);
                    b -= Mathf.Min((float)(int)towerSetting.TimeupEnergyPenalty, b);
                }
                if (!unitCtrl.IsSummonOrPhantom)
                {
                    if (IODJKFHFLKJ && this.battleResult == eBattleResult.WIN)
                    {
                        b += (float)(((double)(int)unitCtrl.EnergyRecoveryRateZero + 100.0) / 100.0) * (float)(int)unitCtrl.WaveEnergyRecoveryZero * num1;
                        unitCtrl.BattleRecovery(this.BattleCategory);
                        hp = (int)(long)unitCtrl.Hp;
                    }
                    List<SkillLimitCounter> skillLimitCounterList = new List<SkillLimitCounter>();
                    Dictionary<int, int>.Enumerator enumerator = unitCtrl.SkillUseCount.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<int, int> current = enumerator.Current;
                        int key = current.Key;
                        int _counter = current.Value;
                        if (_counter != 0)
                            skillLimitCounterList.Add(new SkillLimitCounter(key, _counter));
                    }
                    int num2 = 0;
                    int unitId = unitCtrl.UnitId;
                    if (IODJKFHFLKJ)
                    {
                        if (unitId == this.towerTempData.DispatchUnitId)
                        {
                            num2 = this.towerTempData.DispatchOwnerId;
                            ClanDispatchUnit dispatchUnit = TowerUtility.GetDispatchUnit(this.towerTempData.DispatchUnitList, unitId, num2);
                            dispatchUnit.SetHp(hp);
                            dispatchUnit.SetEnergy((int)b);
                        }
                        else
                            num2 = (int)instance.UserInfo.ViewerId;
                    }
                    bool flag3 = hp > 0;
                    int _hp = (int)((double)hp * ((double)(long)unitCtrl.StartMaxHP / (double)(long)unitCtrl.MaxHp));
                    if (_hp <= 0 & flag3)
                        _hp = 1;
                    if (flag2)
                    {
                        float a = b - (float)decrypted1;
                        b = (double)b <= (double)decrypted2 ? ((double)a > 0.0 ? a : 0.0f) : Mathf.Max(a, (float)decrypted2);
                    }
                    int _identifyNum = IODJKFHFLKJ ? 0 : unitCtrl.IdentifyNum;
                    TowerQueryUnit towerQueryUnit = new TowerQueryUnit(num2, unitId, _identifyNum, unitCtrl.UnitDamageInfo.damage, _hp, (int)b, skillLimitCounterList.ToArray());
                    towerQueryUnitList.Add(towerQueryUnit);
                }
            }
            return towerQueryUnitList;
        }*/

        //public void AddTempDataBattleLog() => this.battleLog.AddTempDataBattleLog();

        public bool IsAuraRemainBattle() => BattleCategory == eBattleCategory.KAISER_BATTLE_MAIN;

        private bool IIGBIBPPNDA { get; set; }

        private bool BKIIPLLBGBA { get; set; }

        private bool NCBEMPGABMP { get; set; }

        private bool PEGOKDOFFIL { get; set; }

        private ACIIMEDNDDJ NBLLPKDOANI { get; set; }

        public void CallbackActionEnd(long NBLAEJPILJM)
        {
            PBCLBKCKHAI.Remove(NBLAEJPILJM);
            if (isAllActionDone || PBCLBKCKHAI.Count != 0)
                return;
            isAllActionDone = true;
            onExecutedActionListCountZero();
        }

        private void onExecutedActionListCountZero()
        {
            if (GameState == eBattleGameState.PLAY || !NCBEMPGABMP || !isAllActionDone && !PEGOKDOFFIL || !idleonlyDone)
                return;
            finishWave(otherAllDead);
        }

        public void CallbackIdleOnlyDone(UnitCtrl GEDLBPMPOKB)
        {
            if (GameState != eBattleGameState.WAIT_WAVE_END || !(GEDLBPMPOKB.IsOther ? EnemyList : UnitList).Contains(GEDLBPMPOKB))
                return;
            int index1 = 0;
            for (int count = UnitList.Count; index1 < count; ++index1)
            {
                if (!UnitList[index1].IsDead)
                {
                    idleonlyDone = UnitList[index1].CurrentState == UnitCtrl.ActionState.IDLE;
                    if (!idleonlyDone)
                        return;
                }
            }
            int index2 = 0;
            for (int count = EnemyList.Count; index2 < count; ++index2)
            {
                if (!EnemyList[index2].IsDead)
                {
                    idleonlyDone = EnemyList[index2].CurrentState == UnitCtrl.ActionState.IDLE;
                    if (!idleonlyDone)
                        return;
                }
            }
            int num = idleonlyDone ? 1 : 0;
            if (!NCBEMPGABMP || !isAllActionDone && !PEGOKDOFFIL || !idleonlyDone)
                return;
            GameState = eBattleGameState.IDLE;
            this.Timer(() => finishWave(otherAllDead));
        }

        public void CallbackFadeOutDone(UnitCtrl GEDLBPMPOKB)
        {
            if (NCBEMPGABMP)
                return;
            if (IIGBIBPPNDA)
            {
                if (otherAllDead && GEDLBPMPOKB.IsOther || playerAllDead && !GEDLBPMPOKB.IsOther)
                    return;
                judgeWinUnitFadeOutDone(GEDLBPMPOKB.IsOther);
            }
            else
            {
                List<UnitCtrl> unitCtrlList = GEDLBPMPOKB.IsOther ? EnemyList : UnitList;
                int index = 0;
                for (int count = unitCtrlList.Count; index < count; ++index)
                {
                    UnitCtrl FNHGFDNICFG = unitCtrlList[index];
                    //if (!this.battleProcessor.JudgeIgnoreUnit(FNHGFDNICFG))
                    {
                        if (FNHGFDNICFG.HasDieLoop)
                        {
                            BattleSpineController currentSpineCtrl = FNHGFDNICFG.GetCurrentSpineCtrl();
                            BKIIPLLBGBA = currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DIE_LOOP);
                        }
                        else
                            BKIIPLLBGBA = !FNHGFDNICFG.gameObject.activeSelf;
                        if (!BKIIPLLBGBA)
                            return;
                    }
                }
                judgeWinUnitFadeOutDone(!GEDLBPMPOKB.IsOther);
            }
        }

        private void judgeWinUnitFadeOutDone(bool LFMEFHAMKLM)
        {
            List<UnitCtrl> unitCtrlList = LFMEFHAMKLM ? EnemyList : UnitList;
            for (int index = 0; index < unitCtrlList.Count; ++index)
            {
                UnitCtrl unitCtrl = unitCtrlList[index];
                if (unitCtrl.IsDead && unitCtrl.gameObject.activeSelf)
                {
                    IIGBIBPPNDA = true;
                    return;
                }
            }
            IIGBIBPPNDA = false;
            NCBEMPGABMP = true;
            if (!NCBEMPGABMP || !isAllActionDone && !PEGOKDOFFIL || !idleonlyDone)
                return;
            finishWave(!LFMEFHAMKLM);
        }

        public void CallbackDead(UnitCtrl unitctrl)
        {
            CallbackIdleOnlyDone(unitctrl);
            //this.UnitUiCtrl.UpdateAllEnemyHp0();
            List<UnitCtrl> unitCtrlList1 = unitctrl.IsOther ? EnemyList : UnitList;
            if (unitCtrlList1 == null)
                return;
            int index1 = 0;
            for (int count = unitCtrlList1.Count; index1 < count; ++index1)
            {
                UnitCtrl unit = unitCtrlList1[index1];
                if (!unit.IsSummonOrPhantom)// && !this.battleProcessor.JudgeIgnoreUnit(FNHGFDNICFG1))
                {
                    if (unitctrl.IsOther)
                    {
                        otherAllDead = unit.IsDead;
                        if (!otherAllDead)
                            return;
                    }
                    else
                    {
                        playerAllDead = unit.IsDead;
                        if (!playerAllDead)
                            return;
                    }
                }
            }
            //this.battleProcessor.OnDeadLastUnit(FNHGFDNICFG);
            int num = GameState == eBattleGameState.PLAY ? 1 : 0;
            GameState = eBattleGameState.WAIT_WAVE_END;
            if (PBCLBKCKHAI.Count > 0)
            {
                isAllActionDone = false;
                if (NBLLPKDOANI == ACIIMEDNDDJ.NOT_RUNNING)
                {
                    NBLLPKDOANI = ACIIMEDNDDJ.RUNNING;
                    StartCoroutine(updateIgnoreAllActionDoneCounter());
                }
            }
            CallbackIdleOnlyDone(unitctrl);
            List<UnitCtrl> unitCtrlList2 = unitctrl.IsOther ? UnitList : EnemyList;
            isPauseTimeLimit = true;
            if (num != 0)
            {
                int index2 = 0;
                for (int count = unitCtrlList2.Count; index2 < count; ++index2)
                {
                    UnitCtrl unitCtrl = unitCtrlList2[index2];
                    unitCtrl.IdleOnly = true;
                    unitCtrl.CureAllAbnormalState();
                }
            }
            //Debug.LogError("GameOver!");
            if (!unitctrl.IsOther)
                return;
            //UnitCtrl GEDLBPMPOKB = this.EnemyList.Find((Predicate<UnitCtrl>)(JNOIHMMFADD => this.battleProcessor.JudgeIgnoreUnit(JNOIHMMFADD)));
            //if (!((UnityEngine.Object)GEDLBPMPOKB != (UnityEngine.Object)null))
            //GEDLBPMPOKB.IdleOnly = true;
            //GEDLBPMPOKB.CureAllAbnormalState();
            //this.CallbackFadeOutDone(GEDLBPMPOKB);
        }

        private IEnumerator updateIgnoreAllActionDoneCounter()
        {
            float timer = 10f;
            while (true)
            {
                timer -= Time.deltaTime;
                if (NBLLPKDOANI != ACIIMEDNDDJ.STOP_PULSE)
                {
                    if (timer >= 0.0)
                        yield return null;
                    else
                        goto label_4;
                }
                else
                    break;
            }
            NBLLPKDOANI = ACIIMEDNDDJ.NOT_RUNNING;
            yield break;
        label_4:
            PEGOKDOFFIL = true;
            CallbackActionEnd(PBCLBKCKHAI.Count == 0 ? 0L : PBCLBKCKHAI.First());
            NBLLPKDOANI = ACIIMEDNDDJ.NOT_RUNNING;
        }

        /*public void UekOnDeadLastUnit(UnitCtrl FNHGFDNICFG, int MACAPGACEFI)
        {
            if (this.playerAllDead || FNHGFDNICFG.IsSummonOrPhantom)
                return;
            MasterUekBoss.UekBoss uekBoss = ManagerSingleton<MasterDataManager>.Instance.masterUekBoss.Get(MACAPGACEFI);
            int num = 0;
            foreach (int rewardType in uekBoss.RewardTypes)
            {
                if (rewardType != 0)
                {
                    ++num;
                    FNHGFDNICFG.TreasureAnimeIdList.Add((eSpineCharacterAnimeId)(158 + rewardType));
                }
            }
            FNHGFDNICFG.RewardCount = (int)num;
        }*/

        /*public void TowerOnDeadLastUnit(UnitCtrl FNHGFDNICFG)
        {
            if (this.playerAllDead || FNHGFDNICFG.IsSummonOrPhantom)
                return;
            List<eBattleTreasureBoxType> treasureTypeList = this.towerTempData.TreasureTypeList;
            FNHGFDNICFG.RewardCount = (int)treasureTypeList.Count;
            for (int index = 0; index < (int)FNHGFDNICFG.RewardCount; ++index)
            {
                int num = (int)(treasureTypeList[index] - 1);
                FNHGFDNICFG.TreasureAnimeIdList.Add((eSpineCharacterAnimeId)(158 + num));
            }
        }*/

        /*public void HighRarityOnDeadLastUnit(UnitCtrl FNHGFDNICFG, eBattleTreasureBoxType GGACEPBHGBE)
        {
            if (this.playerAllDead || FNHGFDNICFG.IsSummonOrPhantom)
                return;
            int num = (int)(GGACEPBHGBE - 1);
            FNHGFDNICFG.RewardCount = (int)1;
            FNHGFDNICFG.TreasureAnimeIdList.Add((eSpineCharacterAnimeId)(158 + num));
        }*/

        private void finishWave(bool GIMAJHFCKNE)
        {
            if (NBLLPKDOANI == ACIIMEDNDDJ.RUNNING)
                NBLLPKDOANI = ACIIMEDNDDJ.STOP_PULSE;
            PEGOKDOFFIL = false;
            for (int index = 0; index < UnitList.Count; ++index)
            {
                UnitCtrl unitCtrl = UnitList[index];
                unitCtrl.CureAllAbnormalState();
                unitCtrl.CutInFrameSet.CutInFrame = -1;
            }
            if (!GIMAJHFCKNE && otherAllDead)
                GIMAJHFCKNE = otherAllDead;
            ChargeSkillTurn = eChargeSkillTurn.NONE;
            //this.UnitUiCtrl.HidePopUp();
            playerAllDead = false;
            otherAllDead = false;
            NCBEMPGABMP = false;
            BKIIPLLBGBA = false;
            idleonlyDone = false;
            GameState = eBattleGameState.IDLE;
            //this.appendWaveEndLog();
            /*if (GIMAJHFCKNE)
            {
                int waveEndStoryId = this.battleProcessor.GetWaveEndStoryId();
                if (this.POKEAEBGPIB < this.tempData.PHDACAOAOMA.AOPCDPIJOIE.Count - 1)
                {
                    this.battleProcessor.FinishWave();
                }
                else
                {
                    BattleHeaderController..Instance.gameObject.SetActive(false);
                    this.UnitUiCtrl.HideSettingButtons();
                    this.PlayStory((System.Action)(() => this.finishBattle(eBattleResult.WIN)), waveEndStoryId, true);
                }
            }
            else*/
            {
                //BattleHeaderController.Instance.gameObject.SetActive(false);
                //this.UnitUiCtrl.HideSettingButtons();
                finishBattle(eBattleResult.LOSE);
            }
        }

        public List<Dictionary<eParamType, PassiveActionValue>> PassiveDic_1 { get; private set; }

        public List<Dictionary<eParamType, PassiveActionValue>> PassiveDic_2 { get; private set; }

        public List<Dictionary<eParamType, PassiveActionValue>> PassiveDic_4 { get; private set; }

        public List<Dictionary<eParamType, PassiveActionValue>> PassiveDic_3 { get; private set; }

        //public Dictionary<eDamageEffectType, Dictionary<eDamageEffctTypeDetail, GameObject>> AMPANMGEBBM { get; set; }

        public GameObject TotalDamageEffectPrefabLarge { get; set; }

        public GameObject TotalDamageEffectPrefabSmall { get; set; }

        public int UnitInstanceIdCount { get; set; }

        public bool BattleLogEnable { get; set; }

        //public BattleVoiceFlagManager CMINMBPDEOL { get; set; }

        private GameObject PAMLMPDGEIJ { get; set; }

        public bool BattleReady { get; private set; }

        public bool BOEDFIHEEOL => isValid;

        private void Awake()
        {
            Instance = this;
            isValid = true;
            //this.singletonTree = this.CreateSingletonTree<BattleManager>();
            //UekTempData PPNELKAAIKP = this.singletonTree.Get<UekTempData>();
            //this.towerTempData = this.singletonTree.Get<TowerTempData>();
            //this.spaceBattleTempData = this.singletonTree.Get<SpaceBattleTempData>();
            //this.replayTempData = this.singletonTree.Get<ReplayTempData>();
            // this.battleManagerNode = this.singletonTree.GetCurrentNodeForInstanceEditing();
            // this.battleLog = new GJNIHENNINA((LGFJGKOMLCN)this);
            battleUnionBurstController = new BattleUnionBurstController(this);
            //this.battleCameraEffect = new CMMLKFHCEPD((OKKJGOIAIFK)this, this.playCamera, this.motionBlur, this.playCameraBlurOptimized);
            battleEffectPool = new BattleEffectPool();
            battleCameraEffect = new BattleCameraEffect(this);
            battleTimeScale = new BattleSpeedManager(this);
            NOMJJDDCBAN = new List<int>();
            // this.battleManagerNode.AddInstance((object)this);
            //this.battleManagerNode.AddInstance((object)this.battleLog);
            //this.battleManagerNode.AddInstance((object)this.battleCameraEffect);
            //this.battleManagerNode.AddInstance((object)this.battleEffectPool);
            // this.battleManagerNode.AddInstance((object)this.battleTimeScale);
            // this.battleManagerNode.AddInstance((object)this.battleUnionBurstController);
            // DialogBase.StaticRelease();
            // this.singletonTree.AddNode(this.battleManagerNode, typeof(DialogBase));
            // SkillEffectCtrl.StaticInitSingletonTree();
            // BattleVoiceUtility.StaticInit((GIHNGAEIFFB)this);
            // this.dialogManager.SetBattleManager((AMAOCAMIEOG)this);
            // if (Singleton<UserData>.Instance.InitSettingParameter.BattleLogType != null)
            //     this.BattleLogEnable = this.battleLog.GetLogEnable(Singleton<UserData>.Instance.InitSettingParameter.BattleLogType);
            //this.BattleCategory = this.tempData.PHDACAOAOMA.CLCOKFOINCL;
            FEDKJAIEDGI = MainManager.Instance.GetDodgeTPRecoveryRatio();

            BattleCategory = eBattleCategory.ARENA;//设定战斗系统为JJC
                                                        //this.battleProcessor = this.createBattleProcessor(PPNELKAAIKP);
                                                        //this.battleEffectPool.BACHGMADMKC = this.battleProcessor.GetForceNormalSD();
                                                        //this.FICLPNJNOEP = this.battleProcessor.GetEnemyPoint();
                                                        // this.currentWaveOffset = this.battleProcessor.GetCurrentWaveOffset();
            battleTimeScale.SetBaseTimeScale(1f);
            UnitList = new List<UnitCtrl>();
            EnemyList = new List<UnitCtrl>();
            LPBCBINDJLJ = new List<UnitCtrl>();
            PBCLBKCKHAI = new HashSet<long>();
            BlackoutUnitTargetList = new HashSet<UnitCtrl>();
            BlackOutUnitList = new List<UnitCtrl>();
            FrameCount = 0;
            AMLOLHFMOPP = false;
            DAIFDPFABCM = new Dictionary<string, GameObject>();
            FieldDataDictionary = new Dictionary<int, List<AbnormalStateDataBase>>();
            /*switch (this.BattleCategory)
            {
                case eBattleCategory.CLAN_BATTLE:
                    this.ELFKGBFMOIE = ManagerSingleton<SaveDataManager>.Instance.ClanReharsalBattleMaxSpeed;
                    break;
                case eBattleCategory.TOWER:
                case eBattleCategory.TOWER_EX:
                case eBattleCategory.TOWER_REPLAY:
                case eBattleCategory.TOWER_EX_REPLAY:
                case eBattleCategory.TOWER_REHEARSAL:
                case eBattleCategory.TOWER_CLOISTER:
                case eBattleCategory.TOWER_CLOISTER_REPLAY:
                    this.ELFKGBFMOIE = ManagerSingleton<SaveDataManager>.Instance.TowerBattleMaxSpeed;
                    break;
            }*/

            BattleLogEnable = true;// !TutorialManager.IsStartTutorial && this.battleProcessor.GetBattleLogEnable(this.BattleLogEnable);
            FameRate = 60;// this.battleProcessor.GetFrameRate();
            IsDefenceReplayMode = false;// this.battleProcessor.GetIsDefenceReplayMode();
            DeltaTime_60fps = 1 / 60f;//0.0166666675f;
            UnitCtrl.DamageFlashFrame = 8;
            UnitCtrl.FlashDelayFrame = 0;
            CoroutineManager = gameObject.AddComponent<CoroutineManager>();
            CoroutineManager.Init(BlackOutUnitList);
            //this.battleEffectManager = this.gameObject.AddComponent<BattleEffectManager>();
            //this.battleEffectManager.Init(this.BlackOutUnitList, this.UnitList, this.EnemyList);
            //this.skillExeScreen.sortingOrder = 11500;
            //this.skillExeScreen.transform.localScale = new Vector3(2f, 2f);
            enabled = false;
            IsBossBattle = false;
            //this.motionBlur.enabled = false;
            //this.battleCameraEffect.SetBgBlur(false);
            battleResult = eBattleResult.INVALID_VALUE;
            //this.battleCameraEffect.PNNMBKNLFCL = 1f;
        }

        /*private MLDKPCCPIOC createBattleProcessor(UekTempData PPNELKAAIKP)
        {
            switch (this.BattleCategory)
            {
                case eBattleCategory.QUEST:
                    return (MLDKPCCPIOC)new OMFAEMGONHN((INCABDFCENP)this);
                case eBattleCategory.TRAINING:
                    return (MLDKPCCPIOC)new MOOOILFEGIP((MEFHDGPANFE)this);
                case eBattleCategory.DUNGEON:
                    return (MLDKPCCPIOC)new BAKKHOOGBDP((PIMGGDOGPBK)this);
                case eBattleCategory.ARENA:
                    return (MLDKPCCPIOC)new PNKIMJDJPIE((PFLHIHCFHMF)this);
                case eBattleCategory.ARENA_REPLAY:
                    return (MLDKPCCPIOC)new EMEEHHNIPMI((HAJECALHMPD)this);
                case eBattleCategory.STORY:
                    return (MLDKPCCPIOC)new POAMJNLDAGM((BBFPEBCLNLE)this);
                case eBattleCategory.TUTORIAL:
                    return (MLDKPCCPIOC)new OGEEOGFFGHN((NMAHGHCADMJ)this);
                case eBattleCategory.GRAND_ARENA:
                    return (MLDKPCCPIOC)new DCJHOEFMKJP((ILNEGBJKMDE)this);
                case eBattleCategory.GRAND_ARENA_REPLAY:
                    return (MLDKPCCPIOC)new CDJLBGNJDLN((CIGFFFAPBNE)this);
                case eBattleCategory.CLAN_BATTLE:
                    return (MLDKPCCPIOC)new AFNKPMKJMOI((NPFLBLEAHEH)this);
                case eBattleCategory.HATSUNE_BATTLE:
                    return (MLDKPCCPIOC)new AJFLJKCJHLC((BBFMCGCJMIH)this);
                case eBattleCategory.HATSUNE_BOSS_BATTLE:
                    return (MLDKPCCPIOC)new AILEKNELHOG((DNLANLDCIMM)this);
                case eBattleCategory.UEK_BOSS_BATTLE:
                    return (MLDKPCCPIOC)new GMNBAJAPMMA((FHFIGDDGGPG)this, PPNELKAAIKP);
                case eBattleCategory.QUEST_REPLAY:
                    return (MLDKPCCPIOC)new OOOOJFHNEMH((KNFLLJPPAIB)this);
                case eBattleCategory.TOWER:
                    return (MLDKPCCPIOC)new ODCIALOHFOJ((IJCAJMFNBAH)this, this.towerTempData);
                case eBattleCategory.TOWER_EX:
                    return (MLDKPCCPIOC)new CDJMOPKHFHN((ANINLFBGHOH)this, this.towerTempData);
                case eBattleCategory.HATSUNE_SPECIAL_BATTLE:
                    return (MLDKPCCPIOC)new BPOOMCKCGIF((DNLANLDCIMM)this);
                case eBattleCategory.TOWER_REPLAY:
                    return (MLDKPCCPIOC)new EOKKLOLBIBP((PIIDHCKABJE)this, this.replayTempData);
                case eBattleCategory.TOWER_EX_REPLAY:
                    return (MLDKPCCPIOC)new NEBBKIDHKIG((PGCPDEJEJJG)this, this.replayTempData);
                case eBattleCategory.TOWER_REHEARSAL:
                    return (MLDKPCCPIOC)new JIELBENICEH((IJCAJMFNBAH)this, this.towerTempData);
                case eBattleCategory.GLOBAL_RAID:
                    return (MLDKPCCPIOC)new KNIADOBBKNF((NPFLBLEAHEH)this);
                case eBattleCategory.TOWER_CLOISTER:
                    return (MLDKPCCPIOC)new IGBLFOFEJBK((IJCAJMFNBAH)this, this.towerTempData);
                case eBattleCategory.HIGH_RARITY:
                    return (MLDKPCCPIOC)new IENKDEKKBNM((APEAFMPOMBA)this);
                case eBattleCategory.FRIEND:
                    return (MLDKPCCPIOC)new FGKPFMAGBAM((PFLHIHCFHMF)this);
                case eBattleCategory.KAISER_BATTLE_MAIN:
                    return (MLDKPCCPIOC)new EGDHCMCKIBA((DNLANLDCIMM)this);
                case eBattleCategory.KAISER_BATTLE_SUB:
                    return (MLDKPCCPIOC)new MJOPJODLKCN((DEONDDDGFFK)this);
                case eBattleCategory.SPACE_BATTLE:
                    return (MLDKPCCPIOC)new ELIBGMOCJFN((BALKEPBPGPP)this, this.spaceBattleTempData);
                default:
                    return (MLDKPCCPIOC)null;
            }
        }*/

        /*private void OnDestroy()
        {
            this.tempData.KIDLPMAIICI.Clear();
            this.tempData.DDMKANMNCAA.Clear();
            this.tempData.MKLNOEGPBLE.Clear();
            if (this.viewManager.NextViewId != eViewId.BATTLE)
            {
                this.replayTempData.ResetData();
                this.replayTempData = (ReplayTempData)null;
            }
            this.skillExeScreen = (UIPanel)null;
            this.BlackOutUnitList = (List<UnitCtrl>)null;
            this.LPAAPDHAIIB = (UnitCtrl)null;
            this.onEndFadeOut = (System.Action)null;
            this.skillExeScreenTexture = (UITexture)null;
            this.HELHEEOHPFO = (UnitCtrl)null;
            this.LNMLGGPGEIG = (UnitCtrl)null;
            this.MLPEDNBHPLI = (List<Dictionary<eParamType, PassiveActionValue>>)null;
            this.JIMEDFPNMIO = (List<Dictionary<eParamType, PassiveActionValue>>)null;
            this.MMDOIKGNNFL = (List<Dictionary<eParamType, PassiveActionValue>>)null;
            this.ABEFDOAGFIP = (List<Dictionary<eParamType, PassiveActionValue>>)null;
            this.AMPANMGEBBM = (Dictionary<eDamageEffectType, Dictionary<eDamageEffctTypeDetail, GameObject>>)null;
            this.TotalDamageEffectPrefabLarge = (GameObject)null;
            this.TotalDamageEffectPrefabSmall = (GameObject)null;
            this.storyBattleDataPrefab = (StoryBattleData)null;
            this.motionBlur = (MotionBlur)null;
            this.playCameraBlurOptimized = (BlurOptimized)null;
            this.focusObject = (GameObject)null;
            this.focusButton = (CustomUIButton)null;
            this.stageBgTex = (UITexture)null;
            this.stageForegroundTex = (UITexture)null;
            this.unitParentTransform = (Transform)null;
            this.enemyParentTransform = (Transform)null;
            this.playCamera = (Camera)null;
            this.bgTransform = (Transform)null;
            this.viewBattle = (ViewBattle)null;
            this.battleEffectManager = (BattleEffectManager)null;
            this.effectUpdateList = (List<SkillEffectCtrl>)null;
            this.voiceOnUnit = (UnitCtrl)null;
            this.currentEnemyUnitCtrlDictionary = (Dictionary<int, UnitCtrl>)null;
            this.allWaveEnemyUnitCtrlListArray = (List<UnitCtrl>[])null;
            this.BossUnit = (UnitCtrl)null;
            this.questWinPositionList = (List<Vector2>)null;
            this.UnitList = (List<UnitCtrl>)null;
            this.EnemyList = (List<UnitCtrl>)null;
            this.LPBCBINDJLJ = (List<UnitCtrl>)null;
            this.CELPEHBLCHN = (UnitCtrl)null;
            this.ODKDGHFBAIJ = (UnitCtrl)null;
            this.CKFFOKKCOLJ = (UnitCtrl)null;
            this.JLHBICJKJEJ = (UnitCtrl)null;
            this.CoroutineManager = (CoroutineManager)null;
            this.PBCLBKCKHAI = (List<long>)null;
            this.BlackoutUnitTargetList = (List<UnitCtrl>)null;
            this.DAIFDPFABCM = (Dictionary<string, GameObject>)null;
            this.RewardList = (List<RewardData>)null;
            this.unitSpineControllerList = (List<BattleSpineController>)null;
            this.FieldDataDictionary = (Dictionary<int, List<AbnormalStateDataBase>>)null;
            this.viewManager = (ViewManager)null;
            this.blockLayerManager = (BlockLayerManager)null;
            this.resourceManager = (ResourceManager)null;
            this.apiManager = (ApiManager)null;
            this.soundManager = (SoundManager)null;
            this.loadingManager = (LoadingManager)null;
            this.CMINMBPDEOL = (BattleVoiceFlagManager)null;
            this.tempData = (EHPLBCOOOPK)null;
            this.NOMJJDDCBAN = (List<int>)null;
            this.dialogManager.ReleaseBattleManager();
            this.dialogManager = (DialogManager)null;
            if (this.battleProcessor != null)
            {
                this.battleProcessor.Release();
                this.battleProcessor = (MLDKPCCPIOC)null;
            }
            BlurEffect.StaticRelease();
            DamageEffectCtrlBase.StaticRelease();
            SpineCowFireArm.StaticRelease();
            SpineFireArm.StaticRelease();
            SpineNormalEffect.StaticRelease();
            PartsBossTotalDamage.StaticRelease();
            ActionParameter.StaticRelease();
            AbnormalStateDataBase.StaticRelease();
            UnitCtrl.StaticRelease();
            SkillEffectCtrl.StaticRelease();
            AbromalStateIconController.StaticRelease();
            LifeGaugeController.StaticRelease();
            UbAbnormalData.StaticRelease();
            UnitActionController.StaticRelease();
            BattleSpineController.StaticRelease();
            DialogBossResult.StaticRelease();
            DialogHatsuneBossResult.StaticRelease();
            DialogQuestFailed.StaticRelease();
            DialogQuestResultWin.StaticRelease();
            DialogTowerExResult.StaticRelease();
            PartsQuestResultLater.StaticRelease();
            PartsQuestResultFormer.StaticRelease();
            DialogNormalArenaResult.StaticRelease();
            DialogGrandArenaResult.StaticRelease();
            DialogSekaiResult.StaticRelease();
            this.singletonTree.RemoveNode(this.battleManagerNode, typeof(DialogBase));
            DialogBase.StaticRelease();
            this.battleManagerNode.RemoveInstance((object)this.battleLog);
            this.battleManagerNode.RemoveInstance((object)this.battleCameraEffect);
            this.battleManagerNode.RemoveInstance((object)this.battleEffectPool);
            this.battleManagerNode.RemoveInstance((object)this.battleTimeScale);
            this.battleManagerNode.RemoveInstance((object)this.battleUnionBurstController);
            this.battleManagerNode.RemoveInstance((object)this);
            this.battleLog.Release();
            this.battleLog = (GJNIHENNINA)null;
            this.battleCameraEffect.Release();
            this.battleCameraEffect = (CMMLKFHCEPD)null;
            this.battleEffectPool.Release();
            this.battleEffectPool = (BattleEffectPool)null;
            this.battleTimeScale.Release();
            this.battleTimeScale = (BattleSpeedManager)null;
            this.battleUnionBurstController.Release();
            this.battleUnionBurstController = (BattleUnionBurstController)null;
            this.battleManagerNode = (ISingletonNodeForInstanceEditing)null;
            BattleVoiceUtility.StaticRelease();
            this.singletonTree = (Yggdrasil<BattleManager>)null;
            this.isValid = false;
        }*/

        //public void Init(ViewBattle GJABBKPIEHH) => this.StartCoroutine(this.coroutineStartProcess(GJABBKPIEHH));
        public void Init(MyGameCtrl gameCtrl)
        {
            stepping = false;
            skipping = false;
            StartCoroutine(coroutineStartProcess(gameCtrl));
        }

        private IEnumerator coroutineStartProcess(MyGameCtrl gameCtrl)
        {
            setStatus.Clear();
            battleFinished = false;
            GuildCalculator.Instance.dmglist.Clear();
            GuildCalculator.Instance.bossValues.Clear();
            BattleManager battleManager = this;
            tempData = gameCtrl.tempData;
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            //BattleManager.FDEDCCJJGHM fdedccjjghm = new BattleManager.FDEDCCJJGHM();
            // ISSUE: reference to a compiler-generated field
            //fdedccjjghm.4__this = this;
            //battleManager.viewBattle = PCRbattleManager;
            // battleManager.battleCameraEffect.orthographicSize = 2.044444f * (float)Screen.height / (float)Screen.width;
            battleManager.playCamera.orthographicSize = 2.044444f * Screen.height / Screen.width;
            // battleManager.viewBattle.TransformEffectCtrl.SetHalfDurationCallback(new System.Action<System.Action>(battleManager.incrementWave));

            //battleManager.viewBattle.TransformEffectCtrlForBoss.SetHalfDurationCallback(new System.Action<System.Action>(battleManager.incrementWave));
            //battleManager.soundManager.EnableSoundListener(Vector3.zero);
            //battleManager.battleCameraEffect.UpdateSoundListenerPos(battleManager.playCamera.transform.position);
            //battleManager.playCamera.orthographicSize = battleManager.battleCameraEffect.orthographicSize;
            //battleManager.playCamera.orthographicSize = 15;
            battleManager.bgTransform.localScale = new Vector3(3.4f, 2, 1) * 1.5f;
            // battleManager.playCameraPos0 = battleManager.playCamera.transform.position;
            battleManager.tempRandomState = UnityEngine.Random.state;
            //int seed = 666;//battleManager.battleProcessor.GetSeed();
            int seed =(int)(Mathf.Sin(Time.realtimeSinceStartup) * 99999f);
            if (tempData.isGuildBattle && tempData.randomData.UseFixedRandomSeed)
            {
                UnityEngine.Random.InitState(tempData.randomData.RandomSeed);
                //UnityRandom.InitState(tempData.randomData.RandomSeed);
                gameCtrl.CurrentSeedForSave = tempData.randomData.RandomSeed;
            }
            else
            {
                UnityEngine.Random.InitState(seed);
                //UnityRandom.InitState(seed);
                gameCtrl.CurrentSeedForSave = seed;
            }

            /*
            // start calc exp

            var x = (uint)gameCtrl.CurrentSeedForSave;
            var y = 1812433253 * x + 1;
            var z = 1812433253 * y + 1;
            var w = 1812433253 * z + 1;
            var rlist = new List<int>();

            var splits = File.ReadAllText("exp.txt").Split('|');
            var n = splits.Length;
            var value = new float[n];
            for (var i = 0; i < n; ++i)
            {
                var exp = splits[i].Split(',');
                if (exp[0] == "pure") value[i] = float.Parse(exp[1]);
                else if (exp[0] == "add") value[i] = value[int.Parse(exp[1])] + value[int.Parse(exp[2])];
                else if (exp[0] == "sub") value[i] = value[int.Parse(exp[1])] - value[int.Parse(exp[2])];
                else if (exp[0] == "mul") value[i] = value[int.Parse(exp[1])] * value[int.Parse(exp[2])];
                else if (exp[0] == "div") value[i] = value[int.Parse(exp[1])] / value[int.Parse(exp[2])];
                else if (exp[0] == "floor") value[i] = Mathf.FloorToInt(value[int.Parse(exp[1])]);
                else if (exp[0] == "ceil") value[i] = Mathf.CeilToInt(value[int.Parse(exp[1])]);
                else if (exp[0] == "max")
                {
                    var a = float.Parse(exp[1]);
                    var b = value[int.Parse(exp[2])];
                    value[i] = Mathf.Max(a, b);
                }
                else if (exp[0] == "min")
                {
                    var a = float.Parse(exp[1]);
                    var b = value[int.Parse(exp[2])];
                    value[i] = Mathf.Min(a, b);
                }
                else if (exp[0] == "barrier")
                {
                    var main = float.Parse(exp[1]);
                    var sub = float.Parse(exp[2]);
                    var total = value[int.Parse(exp[3])];
                    if (total > sub)
                        value[i] = (Mathf.Log(((total - sub) / main + 1.0f)) * main + sub);
                    else value[i] = total;
                }
                else if (exp[0] == "rnd")
                {
                    var id = int.Parse(exp[1]);
                    var bound = int.Parse(exp[2]);
                    while (rlist.Count <= id)
                    {
                        uint t = x, s = w;
                        x = y;
                        y = z;
                        z = w;
                        t ^= t << 11;
                        t ^= t >> 8;
                        w = t ^ s ^ (s >> 19);
                        rlist.Add((int)(w % 1000));
                    }
                    value[i] = rlist[id] <= bound ? 1 : 0;
                }
                else throw new NotImplementedException();
            }
            UnityEngine.Debug.LogError($"exp value = {(int)value[n - 1]}");*/
            randomCounter = 0;
            battleManager.ActionStartTimeCounter = 0;
            // battleManager.battleProcessor.GetActionStartTime();
                                                     //battleManager.StoryStartTime = battleManager.battleProcessor.GetStoryStartTime();
                                                     //battleManager.StartStoryId = battleManager.battleProcessor.GetStartStoryId();
                                                     // ISSUE: reference to a compiler-generated method
                                                     //battleManager.resourceManager.AddLoadResourceId(eResourceId.UNIT_TOTAL_DAMAGE_EFFECT_L, (a) => { TotalDamageEffectPrefabLarge = a as GameObject; });
                                                     // ISSUE: reference to a compiler-generated method
                                                     //battleManager.resourceManager.AddLoadResourceId(eResourceId.UNIT_TOTAL_DAMAGE_EFFECT_S, (a) => { TotalDamageEffectPrefabSmall = a as GameObject; });
                                                     //battleManager.AMPANMGEBBM = new Dictionary<eDamageEffectType, Dictionary<eDamageEffctTypeDetail, GameObject>>((IEqualityComparer<eDamageEffectType>)new eDamageEffectType_DictComparer());
                                                     //battleManager.AMPANMGEBBM.Add(eDamageEffectType.COMBO, new Dictionary<eDamageEffctTypeDetail, GameObject>((IEqualityComparer<eDamageEffctTypeDetail>)new eDamageEffctTypeDetail_DictComparer()));
                                                     //battleManager.AMPANMGEBBM.Add(eDamageEffectType.NORMAL, new Dictionary<eDamageEffctTypeDetail, GameObject>((IEqualityComparer<eDamageEffctTypeDetail>)new eDamageEffctTypeDetail_DictComparer()));
                                                     //battleManager.AMPANMGEBBM.Add(eDamageEffectType.LARGE, new Dictionary<eDamageEffctTypeDetail, GameObject>((IEqualityComparer<eDamageEffctTypeDetail>)new eDamageEffctTypeDetail_DictComparer()));
                                                     // ISSUE: reference to a compiler-generated method
                                                     //battleManager.resourceManager.AddLoadResourceId(eResourceId.UNIT_DAMAGE_EFFECT, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__2));
                                                     // ISSUE: reference to a compiler-generated method
                                                     //battleManager.resourceManager.AddLoadResourceId(eResourceId.UNIT_DAMAGE_EFFECT_CRITICAL, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__3));
                                                     // ISSUE: reference to a compiler-generated method
                                                     //battleManager.resourceManager.AddLoadResourceId(eResourceId.UNIT_COMBO_DAMAGE_EFFECT, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__4));
                                                     // ISSUE: reference to a compiler-generated method
                                                     //battleManager.resourceManager.AddLoadResourceId(eResourceId.ENEMY_COMBO_DAMAGE_EFFECT, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__5));
                                                     // ISSUE: reference to a compiler-generated method
                                                     //battleManager.resourceManager.AddLoadResourceId(eResourceId.UNIT_COMBO_DAMAGE_EFFECT_CRITICAL, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__6));
                                                     // ISSUE: reference to a compiler-generated method
                                                     //battleManager.resourceManager.AddLoadResourceId(eResourceId.ENEMY_COMBO_DAMAGE_EFFECT_CRITICAL, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__7));
                                                     // ISSUE: reference to a compiler-generated method
                                                     //battleManager.resourceManager.AddLoadResourceId(eResourceId.UNIT_DAMAGE_LARGE_EFFECT, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__8));
                                                     // ISSUE: reference to a compiler-generated method
                                                     // battleManager.resourceManager.AddLoadResourceId(eResourceId.UNIT_DAMAGE_LARGE_EFFECT_CRITICAL, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__9));
            //int currentWave = 0;
            //battleManager.backgroundPanel.sortingOrder = -1100;
            //int battleBackground = battleManager.tempData.PHDACAOAOMA.CurrentBattleBackgrounds[currentWave];
            //battleManager.stageBgTex.SetActiveWithCheck(false);
            // battleManager.loadAnimationTexture();
            // ISSUE: reference to a compiler-generated method
            // battleManager.resourceManager.AddLoadResourceId(eResourceId.BG_BATTLE, (long) battleBackground, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__10));
            /*if (QualityManager.GetGPUQualityLevel() == QualityManager.KMCGOHDIENP.Level_4 && ResourceManager.IsExistResource(eResourceId.BG_BATTLE_MATERIAL, (long) battleBackground))
            {
              // ISSUE: reference to a compiler-generated method
              battleManager.resourceManager.AddLoadResourceId(eResourceId.BG_BATTLE_MATERIAL, (long) battleBackground, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__11));
            }*/
            /*battleManager.foregroundPanel.sortingOrder = 10900;
            if (ResourceManager.IsExistResource(eResourceId.FG_BATTLE, (long) battleBackground))
            {
              // ISSUE: reference to a compiler-generated method
              battleManager.resourceManager.AddLoadResourceId(eResourceId.FG_BATTLE, (long) battleBackground, new System.Action<object>(fdedccjjghm.coroutineStartProcessb__12));
            }*/
            /* if (ResourceManager.IsExistResource(eBundleId.BATTLE_FX_BG, (long) (battleBackground / 10)))
               battleManager.resourceManager.AddLoadBundleId(eBundleId.BATTLE_FX_BG, (long) (battleBackground / 10));*/
            /*if (ResourceManager.IsExistResource(eResourceId.FX_BATTLE_BG, (long) battleBackground))
            {
              battleManager.resourceManager.AddLoadResourceId(eResourceId.FX_BATTLE_BG, (long) battleBackground);
              // ISSUE: reference to a compiler-generated method
              battleManager.resourceManager.AddInstantiateGameObject(eResourceId.FX_BATTLE_BG, (long) battleBackground, ExceptNGUIRoot.Transform, new System.Action<GameObject>(fdedccjjghm.coroutineStartProcessb__13));
            }*/
            /*battleManager.battleProcessor.CreateVeryHardBackgroundEffect();
            if (ResourceManager.IsExistResource(eResourceId.SPINE_FOREGROUND_SKELETONDATA, (long) battleBackground))
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: reference to a compiler-generated method
              BattleSpineController.LoadCreateImmidiateBySkinId(eSpineType.BATTLE_FOREGROUND, battleBackground, battleBackground, 0, ExceptNGUIRoot.Transform, new System.Action<BattleSpineController>(new BattleManager.MAMPCKIODFO()
              {
                scale = (battleManager.resourceManager.LoadResourceImmediately(eResourceId.SPINE_FOREGROUND_SCALE, (long) battleBackground) as SpineGroundScale).Scale
              }.coroutineStartProcessb__15));
            }*/
            /*if (ResourceManager.IsExistResource(eResourceId.SPINE_MIDDLEGROUND_SKELETONDATA, (long) battleBackground))
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: reference to a compiler-generated method
              BattleSpineController.LoadCreateImmidiateBySkinId(eSpineType.BATTLE_MIDDLEGROUND, battleBackground, battleBackground, 0, ExceptNGUIRoot.Transform, new System.Action<BattleSpineController>(new BattleManager.DPMMMADMOCG()
              {
                scale = (battleManager.resourceManager.LoadResourceImmediately(eResourceId.SPINE_MIDDLEGROUND_SCALE, (long) battleBackground) as SpineGroundScale).Scale
              }.coroutineStartProcessb__16));
            }*/
            battleManager.BattleLeftTime = MyGameCtrl.Instance.tempData.SettingData.limitTime;// battleManager.tempData.PHDACAOAOMA.CAMIPEAOGNI;
            // ISSUE: explicit non-virtual call
            BattleHeaderController.Instance.SetRestTime(battleManager.BattleLeftTime);
            battleManager.initializePassiveSkill();
            // ISSUE: explicit non-virtual call
            //(battleManager.UnitUiCtrl).Initialize(battleManager.tempData.MDLPAOHBIJE);
            // ISSUE: explicit non-virtual call
            //while (!(battleManager.UnitUiCtrl).IsUnitCreated)
            //    yield return (object)null;
            MyGameCtrl.Instance.Initialize();
            while (!MyGameCtrl.Instance.IsUnitCreated)
            {
                yield return null;
            }
            UnitList = MyGameCtrl.Instance.playerUnitCtrl;
            yield return battleManager.InitializeEnemyFirst();
            battleManager.AMLOLHFMOPP = true;
            //battleManager.stageBgTex.SetActiveWithCheck(true);
            battleManager.sortAndSetPositionOneWave(battleManager.currentEnemyUnitCtrlDictionary);
            if (battleManager.BossUnit != null)
                battleManager.resetAllUnitY();
            // ISSUE: explicit non-virtual call
            /*if ((battleManager.IsDefenceReplayMode))
            {
                // ISSUE: explicit non-virtual call
                // ISSUE: explicit non-virtual call
                (battleManager.UnitUiCtrl).SetUnitListForDefenceMode((battleManager.EnemyList));
            }*/
            battleManager.setupEnemyProcess();
            battleManager.checkBossBattle(battleManager.currentEnemyUnitCtrlDictionary);
            if (IsBossBattle) { BossUnit = EnemyList[0]; }
            // ISSUE: explicit non-virtual call
            for (int index = 0; index < (battleManager.EnemyList).Count; ++index)
            {
                (battleManager.EnemyList)[index].IsOther = true;
                // ISSUE: explicit non-virtual call
                (battleManager.EnemyList)[index].WaveStartProcess(true);
            }
            //battleManager.battleProcessor.SetupHpAndEnergy();
            // ISSUE: explicit non-virtual call
            for (int index = 0; index < (battleManager.UnitList).Count; ++index)
            {
                // ISSUE: explicit non-virtual call
                UnitCtrl unitCtrl = (battleManager.UnitList)[index];
                unitCtrl.WaveStartProcess(true);
                if (unitCtrl.IdleOnly)
                    unitCtrl.SetState(UnitCtrl.ActionState.IDLE);
                else
                    unitCtrl.SetState(UnitCtrl.ActionState.WALK);
            }
            // ISSUE: explicit non-virtual call
            for (int index = 0; index < (battleManager.EnemyList).Count; ++index)
            {
                // ISSUE: explicit non-virtual call
                UnitCtrl unitCtrl = (battleManager.EnemyList)[index];
                if (unitCtrl.StartStateIsWalk)
                {
                    if (unitCtrl.IdleOnly)
                        unitCtrl.SetState(UnitCtrl.ActionState.IDLE);
                    else
                        unitCtrl.SetState(UnitCtrl.ActionState.WALK);
                }
            }
            //battleManager.battleProcessor.SetupHpAndEnergyAfterApplyPassiveSkill();
            //battleManager.tempData.InitXorShiftRandom(seed);
            //battleManager.CMINMBPDEOL = new BattleVoiceFlagManager(battleManager.GetMyUnitList(), battleManager.GetOpponentUnitList());
            // ISSUE: explicit non-virtual call
            // ISSUE: explicit non-virtual call
            (battleManager.UnitList)[HeldRandom(0, (battleManager.UnitList).Count)].IsStartVoicePlay = true;
            int index1 = 0;
            // ISSUE: explicit non-virtual call
            for (int count = (battleManager.UnitList).Count; index1 < count; ++index1)
            {
                // ISSUE: explicit non-virtual call
                (battleManager.UnitList)[index1].ExecActionOnStartAndDetermineInstanceID();
            }
            int index2 = 0;
            // ISSUE: explicit non-virtual call
            for (int count = (battleManager.EnemyList).Count; index2 < count; ++index2)
            {
                // ISSUE: explicit non-virtual call
                (battleManager.EnemyList)[index2].ExecActionOnStartAndDetermineInstanceID();
            }
            //battleManager.tempData = Singleton<EHPLBCOOOPK>.Instance;
            // ISSUE: reference to a compiler-generated field
            //fdedccjjghm.mainPlayerViewerId = (int) Singleton<UserData>.Instance.UserInfo.ViewerId;
            /*if (battleManager.battleProcessor.IsFirstWave())
            {
                //battleManager.tempData.KIDLPMAIICI.Clear();
                // ISSUE: explicit non-virtual call
                foreach (UnitCtrl unitCtrl in (battleManager.UnitUiCtrl).UnitCtrls)
                {
                    // ISSUE: object of a compiler-generated type is created
                    // ISSUE: variable of a compiler-generated type
                    //BattleManager.OMOFEMMLJCJ omofemmljcj = new BattleManager.OMOFEMMLJCJ();
                    // ISSUE: reference to a compiler-generated field
                    //omofemmljcj.unitCtrl = unitCtrl;
                    // ISSUE: reference to a compiler-generated field
                    if (!((UnityEngine.Object)unitCtrl == (UnityEngine.Object)null))
                    {
                        // ISSUE: reference to a compiler-generated field
                        int unitId = unitCtrl.UnitId;
                        // ISSUE: explicit non-virtual call
                        // ISSUE: reference to a compiler-generated field
                        // ISSUE: reference to a compiler-generated field
                        int DKIBIBEGOFG = !BattleUtil.IsQuestSupportUnit((battleManager.BattleCategory), unitId) ? battleManager.battleProcessor.GetMainPlayerViewerId((int)Singleton<UserData>.Instance.UserInfo.ViewerId, unitCtrl.UnitId) : battleManager.tempData.LBHMKBLOJOB.ViewerId;
                        // ISSUE: reference to a compiler-generated field
                        // ISSUE: reference to a compiler-generated field
                        // ISSUE: reference to a compiler-generated field
                        unitCtrl.UnitDamageInfo = battleManager.tempData.CreateDamageData(unitCtrl.UnitId, DKIBIBEGOFG, unitCtrl.UnitParameter.UniqueData.GetCurrentRarity());
                        // ISSUE: reference to a compiler-generated field
                        // ISSUE: reference to a compiler-generated field
                        unitCtrl.UnitDamageInfo.SetSkinData(unitCtrl.UnitParameter.UniqueData.SkinData);
                        if (battleManager.battleProcessor.GetPlayNext())
                        {
                            // ISSUE: reference to a compiler-generated field
                            // ISSUE: reference to a compiler-generated method
                            unitCtrl.UnitDamageInfo.SetDamage(battleManager.battleProcessor.GetUnitDamageInfos().Find(a => a.unit_id == unitCtrl.UnitId).damage);
                        }
                    }
                }
            }
            else
            {
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                //BattleManager.JGOHJOCPFDB jgohjocpfdb = new BattleManager.JGOHJOCPFDB();
                // ISSUE: reference to a compiler-generated field
                //jgohjocpfdb.CS\u00248__locals1 = fdedccjjghm;
                // ISSUE: reference to a compiler-generated field
                // ISSUE: explicit non-virtual call
                //jgohjocpfdb.unitCtrlList =  (battleManager.UnitUiCtrl).UnitCtrls;
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                //BattleManager.IAKIEKNFGNL iakieknfgnl = new BattleManager.IAKIEKNFGNL();
                // ISSUE: reference to a compiler-generated field
                //iakieknfgnl.CS\u00248__locals2 = jgohjocpfdb;
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                for (int i = 0; i < (battleManager.UnitUiCtrl).UnitCtrls.Length; i++)
                {
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    if (battleManager.UnitUiCtrl.UnitCtrls[i] != null)
                    {
                        // ISSUE: reference to a compiler-generated field
                        // ISSUE: reference to a compiler-generated field
                        // ISSUE: reference to a compiler-generated field
                        // ISSUE: reference to a compiler-generated method
                        battleManager.UnitUiCtrl.UnitCtrls[i].UnitDamageInfo = battleManager.tempData.KIDLPMAIICI.Find(a => (
                            a.unit_id == battleManager.UnitUiCtrl.UnitCtrls[i].UnitId && a.viewer_id == (int)Singleton<UserData>.Instance.UserInfo.ViewerId
                        ));
                    }
                }
            }*/
            //battleManager.tempData.DDMKANMNCAA.Clear();
            //battleManager.tempData.MKLNOEGPBLE.Clear();
            //battleManager.battleProcessor.PrepareBattle();
            // ISSUE: reference to a compiler-generated method
            //yield return (object)new WaitUntil(() => viewBattle.StartFadeInCalled);
            //battleManager.battleProcessor.CleanUpData();
            // ISSUE: explicit non-virtual call
            //int currentWave = 0;
            //if (BattleDefine.BattleBgmDic[battleManager.BattleCategory] != null)
            //{ battleManager.battleProcessor.PlayViewBGM(); }

            // else if (!battleManager.tempData.PHDACAOAOMA.CurrentBattleBGM[currentWave].IsNullOrEmpty())
            //    battleManager.soundManager.PlayBGM(battleManager.tempData.PHDACAOAOMA.CurrentBattleBGMSheet[currentWave], battleManager.tempData.PHDACAOAOMA.CurrentBattleBGM[currentWave]);
            //if (ManagerSingleton<ViewManager>.Instance.IsSnsBattlePause)
            //{
            // ISSUE: explicit non-virtual call
            //  battleManager.GamePause(true, false);
            //    ManagerSingleton<ViewManager>.Instance.IsSnsBattlePause = false;
            //}
            yield return null;
            gameCtrl.SetUI();


            try
            {
                var sh = GuildManager.Instance.SettingData.start_hp.Split(',').Select(float.Parse).ToArray();
                var st = GuildManager.Instance.SettingData.start_tp.Split(',').Select(float.Parse).ToArray();
                foreach (var unitCtrl in battleManager.UnitList)
                {
                    var idx = unitCtrl.posIdx;
                    unitCtrl.Hp = unitCtrl.MaxHp * sh[idx];
                    unitCtrl.unitUI.SetHP(sh[idx]);
                    unitCtrl.energy = UnitDefine.MAX_ENERGY * st[idx];
                    unitCtrl.unitUI.SetTP(st[idx]);
                }
            }
            catch (Exception e)
            {

            }

            if (ExcelHelper.ExcelHelper.AsmExportEnabled)
            {
                scriptMgr = new ScriptManager(this);
                scriptMgr.ParseScript();
            }

            battleManager.GameState = eBattleGameState.PLAY;
            battleManager.BattleReady = true;
            battleManager.enabled = true;
            battleManager.skipping = tempData.skipping;

        }

        public List<UnitCtrl> GetMyUnitList() => (BattleCategory == eBattleCategory.ARENA_REPLAY || BattleCategory == eBattleCategory.GRAND_ARENA_REPLAY ? (IsDefenceReplayMode ? 1 : 0) : 0) == 0 ? UnitList : EnemyList;

        public List<UnitCtrl> GetOpponentUnitList() => (BattleCategory == eBattleCategory.ARENA_REPLAY || BattleCategory == eBattleCategory.GRAND_ARENA_REPLAY ? (IsDefenceReplayMode ? 1 : 0) : 0) == 0 ? EnemyList : UnitList;

        private void initializePassiveSkill()
        {
            PassiveDic_1 = new List<Dictionary<eParamType, PassiveActionValue>>
            {
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer()),
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer()),
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer())
      };
            PassiveDic_2 = new List<Dictionary<eParamType, PassiveActionValue>>
            {
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer()),
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer()),
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer())
      };
            PassiveDic_3 = new List<Dictionary<eParamType, PassiveActionValue>>
            {
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer()),
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer()),
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer())
      };
            PassiveDic_4 = new List<Dictionary<eParamType, PassiveActionValue>>
            {
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer()),
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer()),
        new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer())
            };
            int num = 0;
            for (int count = tempData.AllUnitParameters.Count; num < count; ++num)
                InitializePassiveSkillUnitList(tempData.AllUnitParameters[num], ePassiveInitType.ALL_BY_ENEMY, num);
        }

        public void InitializePassiveSkillUnitList(
          List<UnitParameter> CNBJIKBCFIC,
          ePassiveInitType DPLDGBDGLDK,
          int OFEBCDLGGGL)
        {
            int index = 0;
            for (int count = CNBJIKBCFIC.Count; index < count; ++index)
                UnitCtrl.InitializeExAndFreeSkill(CNBJIKBCFIC[index].UniqueData, DPLDGBDGLDK, OFEBCDLGGGL, null);
        }

        private IEnumerator InitializeEnemyFirst()
        {
            //int count = this.tempData.enemyList.Count;
            int count = 1;
            allWaveEnemyUnitCtrlListArray = new List<UnitCtrl>[count];
            for (int index = 0; index < count; ++index)
                allWaveEnemyUnitCtrlListArray[index] = new List<UnitCtrl>();
            for (int i = 0; i < count; ++i)
            {
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                //BattleManager.MODNIPFEMJD modnipfemjd = new BattleManager.MODNIPFEMJD();
                // ISSUE: reference to a compiler-generated field
                bool isCallbackCalled = false;
                // ISSUE: reference to a compiler-generated method
                //this.loadEnemy(i, () => { isCallbackCalled = true; });
                MyGameCtrl.Instance.LoadEnemy(() => { isCallbackCalled = true; });
                // ISSUE: reference to a compiler-generated field
                while (!isCallbackCalled)
                    yield return null;
                allWaveEnemyUnitCtrlListArray[0] = MyGameCtrl.Instance.enemyUnitCtrl;
                //modnipfemjd = (BattleManager.MODNIPFEMJD) null;
            }
            setupCurrentEnemy(0);
        }

        public void InitializeEnemyForIncliment(Action ACGNCFFDNON)
        {
            setupCurrentEnemy(CurrentWave);
            ACGNCFFDNON();
        }

        private void setupCurrentEnemy(int currentWave)
        {
            for (int index1 = 0; index1 < allWaveEnemyUnitCtrlListArray.Length; ++index1)
            {
                if (index1 != currentWave)
                {
                    for (int index2 = 0; index2 < allWaveEnemyUnitCtrlListArray[index1].Count; ++index2)
                    {
                        UnitCtrl _self = allWaveEnemyUnitCtrlListArray[index1][index2];
                        if (_self != null)
                            _self.SetActive(false);
                    }
                }
            }
            Dictionary<int, UnitCtrl> dictionary = new Dictionary<int, UnitCtrl>();
            for (int index = 0; index < allWaveEnemyUnitCtrlListArray[currentWave].Count; ++index)
            {
                UnitCtrl _self = allWaveEnemyUnitCtrlListArray[currentWave][index];
                _self.SetActive(true);
                dictionary.Add(index, _self);
            }
            currentEnemyUnitCtrlDictionary = dictionary;
        }

        /*private void loadEnemy(int wave, System.Action EACMDCOJIPL)
        {
            //EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = this.tempData.PHDACAOAOMA;
            List<UnitParameter> enemyListdata = tempData.EnemyParameters;
            //this.battleProcessor.GetEnemyAtWaveIdx(wave, ref OELDGECMGDO);
            //this.loadEnemyPrime(wave, phdacaoaoma.AOPCDPIJOIE.Count, enemyListdata, phdacaoaoma.JPJCGOLIDOI, phdacaoaoma.NKBOAIJNLNO != null ? phdacaoaoma.NKBOAIJNLNO[wave] : (List<ContinuousUnit>)null, EACMDCOJIPL);
            this.loadEnemyPrime(wave, 1, enemyListdata, EACMDCOJIPL);

        }*/

        /*private void loadEnemyPrime(
          int waveIdx,
          int waveMax,
          List<UnitParameter> enemyDeck,
          //List<InventoryInfo> CNPLEHHDFEL,
          //List<ContinuousUnit> OAAFEOAPCMK,
          System.Action ACGNCFFDNON)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            BattleManager.KFBJPMFJNPK kfbjpmfjnpk = new BattleManager.KFBJPMFJNPK();
            // ISSUE: reference to a compiler-generated field
            kfbjpmfjnpk._enemyDeck = enemyDeck;
            // ISSUE: reference to a compiler-generated field
            kfbjpmfjnpk._this = this;
            // ISSUE: reference to a compiler-generated field
            kfbjpmfjnpk._continuousUnitList = OAAFEOAPCMK;
            // ISSUE: reference to a compiler-generated field
            kfbjpmfjnpk._waveIndex = waveIdx;
            // ISSUE: reference to a compiler-generated field
            kfbjpmfjnpk._waveMax = waveMax;
            // ISSUE: reference to a compiler-generated field
            kfbjpmfjnpk._questReward = CNPLEHHDFEL;
            // ISSUE: reference to a compiler-generated field
            kfbjpmfjnpk._finishCallback = ACGNCFFDNON;
            List<int> intList = new List<int>();
            // ISSUE: reference to a compiler-generated field
            for (int index = kfbjpmfjnpk._enemyDeck.Count - 1; index >= 0; --index)
            {
                // ISSUE: reference to a compiler-generated field
                UnitParameter unitParameter = kfbjpmfjnpk._enemyDeck[index];
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                if (kfbjpmfjnpk._continuousUnitList == null || !this.battleProcessor.CheckContinuousUnitDead(kfbjpmfjnpk._continuousUnitList, kfbjpmfjnpk._enemyDeck[index].UniqueData))
                    intList.Add(index);
            }
            // ISSUE: reference to a compiler-generated field
            List<int> enemyPrefabIdList = new List<int>();
            // ISSUE: reference to a compiler-generated method
            intList.ForEach((a) => { enemyPrefabIdList.Add(enemyDeck[a].MasterData.PrefabId); });
            // ISSUE: reference to a compiler-generated field
            kfbjpmfjnpk.currentEnemyUnitCtrlDictionay = new Dictionary<int, UnitCtrl>();
            // ISSUE: reference to a compiler-generated field
            kfbjpmfjnpk.currentEnemyList = new List<UnitCtrl>();
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.allWaveEnemyUnitCtrlListArray[kfbjpmfjnpk._waveIndex] = kfbjpmfjnpk.currentEnemyList;
            // ISSUE: reference to a compiler-generated method
            intList.ForEach(new System.Action<int>(kfbjpmfjnpk.loadEnemyPrimeb__1));
            // ISSUE: reference to a compiler-generated method
            this.resourceManager.StartLoad(new System.Action(kfbjpmfjnpk.loadEnemyPrimeb__2));
        }*/
        /*private sealed class KFBJPMFJNPK
        {
            public List<int> enemyPrefabIdList;
            public List<UnitParameter> _enemyDeck;
            public BattleManager _this;
            public List<ContinuousUnit> _continuousUnitList;
            public Dictionary<int, UnitCtrl> currentEnemyUnitCtrlDictionay;
            public List<UnitCtrl> currentEnemyList;
            public int _waveIndex;
            public int _waveMax;
            public List<InventoryInfo> _questReward;
            public Action _finishCallback;

            internal void loadEnemyPrimeb__0(int NEKDADGFOHE)
            {
                this.enemyPrefabIdList.Add((int)this._enemyDeck[NEKDADGFOHE].MasterData.PrefabId);
            }

            internal void loadEnemyPrimeb__1(int JKDOOHOMDHB)
            {
                BattleManager.DDGGDAFEFHP ddggdafefhp = new BattleManager.DDGGDAFEFHP
                {
                    __locals1 = this,
                    _enemyDeckIndex = JKDOOHOMDHB
                };
                ddggdafefhp.enemyDeckIndex = ddggdafefhp._enemyDeckIndex;
                int prefabId = (int)this._enemyDeck[ddggdafefhp._enemyDeckIndex].MasterData.PrefabId;
                ddggdafefhp.enemyIdx = 0;
                Singleton<BattleUnitLoader>.Instance.AddLoadResource(prefabId, new Action<GameObject>(ddggdafefhp.loadEnemyPrimeb__3), true, true);
            }

            internal void loadEnemyPrimeb__2()
            {
                if (this._waveIndex == (this._waveMax - 1))
                {
                    if (!this.currentEnemyUnitCtrlDictionay.TryGetValue(this._enemyDeck.FindIndex(JNOIHMMFADD => UnitUtility.JudgeIsBoss((int)JNOIHMMFADD.MasterData.UnitId)), out this._this.BossUnit))
                    {
                        this._this.BossUnit = this.currentEnemyList[0];
                    }
                    if ((this._questReward != null) && (this._questReward.Count > 0))
                    {
                        MasterItemData masterItemData = ManagerSingleton<MasterDataManager>.Instance.masterItemData;
                        this._this.RewardList = new List<RewardData>();
                        int num2 = 0;
                        int count = this._questReward.Count;
                        while (num2 < count)
                        {
                            InventoryInfo info = this._questReward[num2];
                            MasterItemData.ItemData data2 = null;
                            bool flag = masterItemData.dictionary.TryGetValue((int)info.Id, out data2) && (data2.Type == 0x12);
                            this._this.RewardList.Add(new RewardData((int)info.Id, (int)info.Type, (int)info.Count, flag));
                            num2++;
                        }
                    }
                }
                List<WaveEnemyInfo> waveEnemyInfo = Singleton<UserData>.Instance.QuestStageParameter.GetWaveEnemyInfo(this._waveIndex);
                foreach (KeyValuePair<int, UnitCtrl> pair in this.currentEnemyUnitCtrlDictionay)
                {
                    BattleManager.DDMEMABBHDB ddmemabbhdb = new BattleManager.DDMEMABBHDB
                    {
                        __locals2 = this
                    };
                    int key = pair.Key;
                    ddmemabbhdb.unit = this.currentEnemyUnitCtrlDictionay[key];
                    ddmemabbhdb.unitParameter = this._enemyDeck[key];
                    ddmemabbhdb.continuousUnit = null;
                    if (this._continuousUnitList != null)
                    {
                        ddmemabbhdb.continuousUnit = this._this.battleProcessor.GetContinuousUnit(this._continuousUnitList, ddmemabbhdb.unitParameter.UniqueData);
                    }
                    if ((waveEnemyInfo != null) && (waveEnemyInfo.Count > key))
                    {
                        this._this.battleProcessor.SetupDropItem(ddmemabbhdb.unit, waveEnemyInfo[key]);
                    }
                    this._this.battleProcessor.SetupTreasureBoxEffect(ddmemabbhdb.unit);
                    eSpineType type = eSpineType.SD_NORMAL_BOUND;
                    MasterUnitData.UnitData data3 = ManagerSingleton<MasterDataManager>.Instance.masterUnitData.Get(UnitUtility.GetUnitResourceId((int)ddmemabbhdb.unitParameter.MasterData.UnitId));
                    bool flag2 = ddmemabbhdb.unitParameter.UniqueData.UnitRarity >= 6;
                    int num5 = flag2 ? ((int)data3.cutin1_star6) : ((int)data3.CutIn1);
                    if (UnitUtility.IsPersonUnit((int)ddmemabbhdb.unitParameter.MasterData.UnitId) && !this._this.IsDefenceReplayMode)
                    {
                        type = (this._this.BattleCategory == eBattleCategory.FRIEND) ? eSpineType.SD_NORMAL_BOUND : eSpineType.SD_SHADOW_BOUND;
                        ddmemabbhdb.unit.IsShadow = true;
                        num5 = flag2 ? ((int)data3.cutin2_star6) : ((int)data3.CutIn2);
                    }
                    else if (ddmemabbhdb.unitParameter.MasterData.VisualChangeFlag == 1)
                    {
                        type = eSpineType.SD_SHADOW_BOUND;
                        ddmemabbhdb.unit.IsShadow = true;
                        num5 = flag2 ? ((int)data3.cutin2_star6) : ((int)data3.CutIn2);
                    }
                    if (ddmemabbhdb.unitParameter.EnemyColor != 0)
                    {
                        type = eSpineType.SD_COLOR_ENEMY_BOUND;
                    }
                    if (num5 != 0)
                    {
                        ManagerSingleton<MovieManager>.Instance.Load(eMovieType.CUT_IN, (long)num5, false, null, true, 0L, false);
                        ddmemabbhdb.unit.MovieId = num5;
                    }
                    bool flag3 = this._this.battleProcessor.IsNecessaryOtherUserInfo();
                    int num6 = UnitUtility.GetSetSkinNo((int)ddmemabbhdb.unitParameter.UniqueData.UnitRarity, ddmemabbhdb.unitParameter.UniqueData.SkinData, UnitDefine.eSkinType.Sd, flag3, ddmemabbhdb.unit);
                    int prefabId = (int)ddmemabbhdb.unitParameter.MasterData.PrefabId;
                    GameObject obj2 = new GameObject();
                    obj2.transform.SetParent(this.currentEnemyUnitCtrlDictionay[key].transform.TargetTransform, false);
                    obj2.name = "rotate_center";
                    this.currentEnemyUnitCtrlDictionay[key].RotateCenter = obj2.transform;
                    BattleSpineController.LoadCreate(type, UnitUtility.GetUnitResourceId(prefabId), num6, obj2.transform, new Action<BattleSpineController>(ddmemabbhdb.loadEnemyPrimeb__5), ddmemabbhdb.unitParameter.EnemyColor);
                    ManagerSingleton<ViewManager>.Instance.AddIgnoreRemoveCueSheet(UnitCtrl.ConvertToSkillCueSheetName(ddmemabbhdb.unit.SoundUnitId));
                }
                this._this.resourceManager.StartLoad(this._finishCallback);
            }
        }*/
        /*private sealed class DDGGDAFEFHP
        {
            public int enemyDeckIndex;
            public int enemyIdx;
            public int _enemyDeckIndex;
            public BattleManager.KFBJPMFJNPK __locals1;

            internal void loadEnemyPrimeb__3(GameObject ALALJBDKOPO)
            {
                ALALJBDKOPO.transform.parent = this.__locals1._this.enemyParentTransform;
                ALALJBDKOPO.transform.localScale = Vector3.one;
                ALALJBDKOPO.transform.localPosition = (Vector3)new Vector2(0f, -3000f);
                UnitCtrl component = ALALJBDKOPO.GetComponent<UnitCtrl>();
                if (this.__locals1._continuousUnitList != null)
                {
                    this.__locals1._this.battleProcessor.UpdateContinuousUnitSkillCounter(this.__locals1._continuousUnitList, this.__locals1._enemyDeck[this.enemyDeckIndex].UniqueData, component);
                }
                this.__locals1._this.battleProcessor.ModifyUnitCtrl(ref component, this.__locals1._enemyDeck[this.enemyDeckIndex].UniqueData);
                component.Index = this.enemyIdx;
                this.__locals1.currentEnemyUnitCtrlDictionay.Add(this._enemyDeckIndex, component);
                this.__locals1.currentEnemyList.Add(component);
            }
        }*/
        /*private sealed class DDMEMABBHDB
        {
            public UnitCtrl unit;
            public UnitParameter unitParameter;
            public ContinuousUnit continuousUnit;
            public BattleManager.KFBJPMFJNPK __locals2;

            internal void loadEnemyPrimeb__5(BattleSpineController bc)
            {
                
                this.unit.UnitSpineCtrl = bc;
                bc.Owner = this.unit;
                bc.LoadAnimationIDImmediately(eSpineBinaryAnimationId.COMMON_BATTLE);
                bc.LoadAnimationIDImmediately(eSpineBinaryAnimationId.BATTLE);
                this.unit.CenterBone = bc.skeleton.FindBone("Center");
                this.unit.StateBone = bc.skeleton.FindBone("State");
                bc.SetAnimeEventDelegateForBattle(()=> { bc.IsStopState = true; }, -1);
                bc.SetDropTreasureBoxTime();
                this.unit.Initialize(this.unitParameter, true, this.__locals2._waveIndex == 0, false);
                if (this.continuousUnit != null)
                {
                    this.unit.SetCurrentHp((long)this.continuousUnit.Hp);
                    this.unit.SetEnergy((float)this.continuousUnit.Energy, eSetEnergyType.INITIALIZE, null);
                }
                if (this.unit.IsBoss)
                {
                    //BattleManager.NGPJKGHFBME ngpjkghfbme = new BattleManager.NGPJKGHFBME
                    //{
                    //    __locals4 = hoebpjgpgip,
                    //    isDisplayBoosHpUi = this.__locals2._this.battleProcessor.IsDisplayBossHpUi(),
                    //    partsBossGauge = BattleHeaderController..Instance.BossGauge
                    //};
                    var a = BattleHeaderController..Instance.BossGauge;
                    a.LoadAndInstaciateAbnormalIcon(
                        () =>
                        {
                            a.AbnormalStateIcon.Initialize(this.__locals2._this.IsSpecialBattle ? 6 : 10,false, this.__locals2._this.battleProcessor.IsDisplayBossHpUi()) ;
                        }                        
                        , this.__locals2._this.battleProcessor.IsDisplayBossHpUi(), this.__locals2._this.IsSpecialBattle);
                    if (this.__locals2._this.BattleCategory == eBattleCategory.GLOBAL_RAID)
                    {
                        a.SetNameAndLevel((string)this.unitParameter.MasterData.UnitName, eTextId.SEKAI_BOSS_LEVEL_TEXT.Name());
                    }
                    else
                    {
                        a.SetNameAndLevel((string)this.unitParameter.MasterData.UnitName, (int)this.unitParameter.UniqueData.UnitLevel);
                    }
                    a.CreateUnitIcon(UnitUtility.GetUnitResourceId((int)this.unitParameter.UniqueData.Id), (int)this.unitParameter.UniqueData.PromotionLevel);
                }
            }
        }*/

        /*public void UpdateSkillCounter(ContinuousUnit FJBEGOJABAA, UnitCtrl IMDKENFNOMF)
        {
            if (FJBEGOJABAA.SkillLimitCounter == null)
                return;
            int index = 0;
            for (int count = FJBEGOJABAA.SkillLimitCounter.Count; index < count; ++index)
            {
                SkillLimitCounter skillLimitCounter = FJBEGOJABAA.SkillLimitCounter[index];
                IMDKENFNOMF.SkillUseCount.Add(skillLimitCounter.skill_id, skillLimitCounter.counter);
            }
        }*/

        //private void loadAnimationTexture() => this.resourceManager.RegistCommonAsset(eBundleId.BG_ANIMATION_TEXTURE);

        //public void UnloadAnimationTexture() => this.resourceManager.UnloadBundleId(eBundleId.BG_ANIMATION_TEXTURE);

        //public bool GetForceNormalSD() => this.battleProcessor.GetForceNormalSD();

        private bool LKLFFOFDCHK { get; set; }

        private bool GPFLCBONNMK { get; set; }

        public int LAMHAIODABF { get; set; }

        public bool GetAdvPlayed() => LKLFFOFDCHK;

        public void CallbackStanbyDone(UnitCtrl FNHGFDNICFG)
        {
            if (GPFLCBONNMK)
                return;
            for (int index = 0; index < UnitList.Count; ++index)
            {
                if (!UnitList[index].StandByDone)
                    return;
            }
            GPFLCBONNMK = true;
            PlayStory(() => LKLFFOFDCHK = true, 
                0,//this.battleProcessor.GetNextWaveStartStoryId(), 
                false);
        }

        public void CallbackStartDashDone(UnitCtrl FNHGFDNICFG)
        {
            if (LAMHAIODABF == 0)
                return;
            FNHGFDNICFG.StartDashDone = true;
            for (int index = 0; index < UnitList.Count; ++index)
            {
                if (!UnitList[index].IsDead && !UnitList[index].StartDashDone)
                    return;
            }
            for (int index = 0; index < UnitList.Count; ++index)
                UnitList[index].StartDashDone = false;
            PlayStory(() => LKLFFOFDCHK = true, LAMHAIODABF, false);
            LAMHAIODABF = 0;
        }

        public void PlayStory(Action callBack, int id, bool COJBLEMPHOC)
        {
            if (id == 0)
            {
                callBack.Call();
            }
            else
            {
                //this.UnitUiCtrl.HidePopUp();
                GamePause(true);
                //this.battleProcessor.PlayStory(MELLIBIDKGI, IDIPPNOCLLK, COJBLEMPHOC, this.viewBattle);
            }
        }

        public int KIHOGJBONDH { get; set; }

        //public int GetPurposeCount() => this.battleProcessor.GetPurposeCount();

        //public eHatsuneSpecialPurpose GetPurpose() => this.battleProcessor.GetPurpose();

        /*public void InstantiatePointCounter(
          Transform JOMMBAJFOFM,
          ref PartsHastunePointCounter BBEPEGPNNAP) => this.battleProcessor.InstantiatePointCounter(JOMMBAJFOFM, ref BBEPEGPNNAP);

        public int GetEnemyPoint(int AGAPBBHBIJD) => this.battleProcessor.GetEnemyPoint(AGAPBBHBIJD);

        public int GetInitialPosition(int AGAPBBHBIJD) => this.battleProcessor.GetInitialPosition(AGAPBBHBIJD);

        public int GetTriggerHp() => this.battleProcessor.GetTriggerHp();

        public int GetCurrentWaveOffset() => this.battleProcessor.GetCurrentWaveOffset();

        public bool GetPlayNext() => this.battleProcessor.GetPlayNext();

        public void SetPlayNext(bool KGNFLOPBOMB) => this.battleProcessor.SetPlayNext(KGNFLOPBOMB);

        public bool GetBossGaugeColorChange() => this.battleProcessor.GetBossGaugeColorChange();

        public float GetBossGaugeValue(int KPPPKDDLGDB) => this.battleProcessor.GetBossGaugeValue(KPPPKDDLGDB);

        public void SetSpecialBattleGaugeColor(UIProgressBar ELLJKNABJNB) => this.battleProcessor.SetSpecialBattleGaugeColor(ELLJKNABJNB);

        public List<ContinuousUnit> GetContinuousUnits() => this.battleProcessor.GetContinuousUnits();

        public void SetDamageGoldModeMaxHpAttackNum(
          ref int HFFGLNMNFKH,
          ref int KLFEGBADIOE,
          ref int BLAFDFNAEEC,
          ref int MAEOBJJBBBK,
          ref int PIKPGKDJBDO) => this.battleProcessor.SetDamageGoldModeMaxHpAttackNum(ref HFFGLNMNFKH, ref KLFEGBADIOE, ref BLAFDFNAEEC, ref MAEOBJJBBBK, ref PIKPGKDJBDO);
         */
        public void SubstructEnemyPoint(int PBAANIPPIGL)
        {
            /*this.FICLPNJNOEP += PBAANIPPIGL;
            int purposeCount = this.GetPurposeCount();
            this.FICLPNJNOEP = Mathf.Min(purposeCount, this.FICLPNJNOEP);
            BattleHeaderController..Instance.SetEnemyPoint(this.FICLPNJNOEP);
            if ((UnityEngine.Object)this.FICPCPIFKCD != (UnityEngine.Object)null)
                this.FICPCPIFKCD.SetEnemyPoint(this.FICLPNJNOEP, this.BossUnit, false, (float)this.GetPurposeCount());
            if (this.FICLPNJNOEP < purposeCount || this.battleResult == eBattleResult.WIN)
                return;
            bool flag = this.GetPurpose() == eHatsuneSpecialPurpose.ABSORBER;
            if (flag && this.KIHOGJBONDH == 0)
                return;
            if ((UnityEngine.Object)this.FICPCPIFKCD != (UnityEngine.Object)null)
                this.FICPCPIFKCD.PlayBreak(this.BossUnit);
            if (flag)
            {
                this.BossUnit.AuraEffectList.Remove((SkillEffectCtrl)this.FICPCPIFKCD);
                this.KIHOGJBONDH = 0;
            }
            else
            {
                for (int index = 0; index < this.EnemyList.Count; ++index)
                    this.EnemyList[index].IdleOnly = true;
                for (int index = 0; index < this.UnitList.Count; ++index)
                    this.UnitList[index].IdleOnly = true;
                this.isPauseTimeLimit = true;
                this.battleResult = eBattleResult.WIN;
                this.AppendCoroutine(this.waitAndFinishBattle(), ePauseType.SYSTEM);
            }*/
        }

        /*private IEnumerator waitAndFinishBattle()
        {
            BattleManager battleManager = this;
            float timer = 3f;
            while ((double)timer > 0.0)
            {
                // ISSUE: explicit non-virtual call
                timer -= (battleManager.DeltaTime_60fps);
                yield return (object)null;
            }
           // ISSUE: explicit non-virtual call
           (battleManager.UnitUiCtrl).HideSettingButtons();
            BattleHeaderController..Instance.gameObject.SetActive(false);
            // ISSUE: reference to a compiler-generated method
            // ISSUE: explicit non-virtual call
            // battleManager.PlayStory(new System.Action(battleManager.waitAndFinishBattleb__675_0), battleManager.battleProcessor.GetWaveEndStoryId(), true);
        }*/
        /*
        public void SpecialBattleModeChangeOnHpChange()
        {
            for (int index = 0; index < this.EnemyList.Count; ++index)
                this.EnemyList[index].IdleOnly = true;
            for (int index = 0; index < this.UnitList.Count; ++index)
                this.UnitList[index].IdleOnly = true;
            this.isPauseTimeLimit = true;
            this.battleResult = eBattleResult.WIN;
            this.GameState = eBattleGameState.NEXT_WAVE_PROCESS;
            this.UnitUiCtrl.HideSettingButtons();
            BattleHeaderController..Instance.gameObject.SetActive(false);
            this.battleTimeScale.SetBaseTimeScale(1f);
            this.battleTimeScale.SpeedUpFlag = false;
            this.PlayStory((System.Action)(() => this.battleProcessor.FinishBattle()), this.battleProcessor.GetWaveEndStoryId(), true);
        }

        public void DeterminePositionOrder()
        {
            List<UnitCtrl> all = this.EnemyList.FindAll((Predicate<UnitCtrl>)(JNOIHMMFADD => JNOIHMMFADD.EnemyPoint > 0));
            all.Sort(new Comparison<UnitCtrl>(this.comparePositionX));
            for (int index = 0; index < all.Count; ++index)
                all[index].PositionOrder = index + 1;
        }

        private int comparePositionX(UnitCtrl IPJGCOBNHLB, UnitCtrl IMMPDMOKFGC) => (long)IPJGCOBNHLB.Hp == 0L && (long)IMMPDMOKFGC.Hp != 0L || (long)IPJGCOBNHLB.Hp != 0L && (long)IMMPDMOKFGC.Hp == 0L ? ((long)IMMPDMOKFGC.Hp).CompareTo((long)IPJGCOBNHLB.Hp) : IPJGCOBNHLB.transform.position.x.CompareTo(IMMPDMOKFGC.transform.position.x);

        public bool FGFCBPPIMHG { get; set; }

        public bool EBEBMGJEHOE;// => throw new NotImplementedException();

        public void SetStoryBattleDataPrefab(StoryBattleData JGPAIFLBCMF) => this.storyBattleDataPrefab = JGPAIFLBCMF;

        public IEnumerator StoryBattleAnimationUpdate()
        {
            BattleManager battleManager = this;
            // ISSUE: explicit non-virtual call
            UnitCtrl unitCtrl1 = (battleManager.EnemyList)[0];
            // ISSUE: explicit non-virtual call
            UnitCtrl unitCtrl2 = (battleManager.UnitList)[0];
            UnitCtrl unitCtrl3 = (double)unitCtrl1.SearchAreaSize < (double)unitCtrl2.SearchAreaSize ? unitCtrl2 : unitCtrl1;
            unitCtrl3.SetLocalPosX(battleManager.storyBattleDataPrefab.StartPosition);
            if (battleManager.storyBattleDataPrefab.IsUseVoiceTimeLine)
                battleManager.StartCoroutine(battleManager.storyBattleVoiceTimeLineUpdate());
            if (unitCtrl3.IsOther)
            {
                float num1 = battleManager.storyBattleDataPrefab.StartPosition - unitCtrl3.BodyWidth / 2f;
                int index1 = 0;
                // ISSUE: explicit non-virtual call
                for (int count = (battleManager.UnitList).Count; index1 < count; ++index1)
                {
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    (battleManager.UnitList)[index1].SetLocalPosX((float)((double)num1 - (double)(battleManager.UnitList)[index1].SearchAreaSize - (double)(battleManager.UnitList)[index1].BodyWidth / 2.0));
                }
                // ISSUE: explicit non-virtual call
                // ISSUE: explicit non-virtual call
                float num2 = (battleManager.UnitList)[0].transform.localPosition.x + (battleManager.UnitList)[0].BodyWidth / 2f;
                int index2 = 1;
                // ISSUE: explicit non-virtual call
                for (int count = (battleManager.EnemyList).Count; index2 < count; ++index2)
                {
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    (battleManager.EnemyList)[index2].SetLocalPosX((float)((double)num2 + (double)(battleManager.EnemyList)[index2].SearchAreaSize + (double)(battleManager.EnemyList)[index2].BodyWidth / 2.0));
                }
            }
            else
            {
                float num1 = battleManager.storyBattleDataPrefab.StartPosition + unitCtrl3.BodyWidth / 2f;
                int index1 = 0;
                // ISSUE: explicit non-virtual call
                for (int count = (battleManager.EnemyList).Count; index1 < count; ++index1)
                {
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    (battleManager.EnemyList)[index1].SetLocalPosX((float)((double)num1 + (double)(battleManager.EnemyList)[index1].SearchAreaSize + (double)(battleManager.EnemyList)[index1].BodyWidth / 2.0));
                }
                // ISSUE: explicit non-virtual call
                // ISSUE: explicit non-virtual call
                float num2 = (battleManager.EnemyList)[0].transform.localPosition.x - (battleManager.EnemyList)[0].BodyWidth / 2f;
                int index2 = 1;
                // ISSUE: explicit non-virtual call
                for (int count = (battleManager.UnitList).Count; index2 < count; ++index2)
                {
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    // ISSUE: explicit non-virtual call
                    (battleManager.UnitList)[index2].SetLocalPosX((float)((double)num2 - (double)(battleManager.UnitList)[index2].SearchAreaSize - (double)(battleManager.UnitList)[index2].BodyWidth / 2.0));
                }
            }
            int index = 0;
            List<StoryBattleTimeLineData> timeLineData = new List<StoryBattleTimeLineData>((IEnumerable<StoryBattleTimeLineData>)battleManager.storyBattleDataPrefab.TimeLineData);
            timeLineData.Sort((Comparison<StoryBattleTimeLineData>)((IPJGCOBNHLB, IMMPDMOKFGC) => IPJGCOBNHLB.Time.CompareTo(IMMPDMOKFGC.Time)));
            while (index != timeLineData.Count)
            {
                // ISSUE: explicit non-virtual call
                // ISSUE: explicit non-virtual call
                if ((double)(battleManager.JJCJONPDGIM) * (double)(battleManager.DeltaTime_60fps) > (double)timeLineData[index].Time)
                {
                    battleManager.execTimeLineData(timeLineData[index]);
                    ++index;
                }
                yield return (object)null;
            }
        }

        private IEnumerator storyBattleVoiceTimeLineUpdate()
        {
            this.FGFCBPPIMHG = true;
            int index = 0;
            List<StoryBattleVoiceTimeLineData> timeLineData = new List<StoryBattleVoiceTimeLineData>((IEnumerable<StoryBattleVoiceTimeLineData>)this.storyBattleDataPrefab.VoiceTimeLineData);
            timeLineData.Sort((Comparison<StoryBattleVoiceTimeLineData>)((IPJGCOBNHLB, IMMPDMOKFGC) => IPJGCOBNHLB.Time.CompareTo(IMMPDMOKFGC.Time)));
            while (index != timeLineData.Count)
            {
                StoryBattleVoiceTimeLineData voiceTimeLineData = timeLineData[index];
                if ((double)this.JJCJONPDGIM * (double)this.DeltaTime_60fps > (double)voiceTimeLineData.Time)
                {
                    UnitCtrl unitCtrl = this.searchUnit(voiceTimeLineData.UnitId);
                    if ((UnityEngine.Object)unitCtrl == (UnityEngine.Object)null)
                        this.soundManager.PlayVoice(voiceTimeLineData.CueSheetName, voiceTimeLineData.CueName);
                    else
                        this.soundManager.PlayVoiceByOuterSource(unitCtrl.VoiceSource, voiceTimeLineData.CueSheetName, voiceTimeLineData.CueName);
                    ++index;
                }
                yield return (object)null;
            }
        }*/

        /*private void execTimeLineData(StoryBattleTimeLineData AEJCDIJBPLD)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            BattleManager.FGKBHAFHPKP fgkbhafhpkp = new BattleManager.FGKBHAFHPKP();
            // ISSUE: reference to a compiler-generated field
            fgkbhafhpkp.4__this = this;
            // ISSUE: reference to a compiler-generated field
            fgkbhafhpkp._timeLineData = AEJCDIJBPLD;
            // ISSUE: reference to a compiler-generated field
            switch (fgkbhafhpkp._timeLineData.dataType)
            {
                case eStoryBattleDataType.ADV:
                    this.GamePause(true, false);
                    EHPLBCOOOPK.OGEJKAHGHLD pclnmookcbg = this.tempData.PCLNMOOKCBG;
                    bool _isVoiceDownLoad = true;
                    if ((int)pclnmookcbg.PHPOANOKDDA != 0)
                        _isVoiceDownLoad = (bool)pclnmookcbg.EEDNFKEDLPM;
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated method
                    ManagerSingleton<DialogManager>.Instance.OpenTutorialAdventure(fgkbhafhpkp._timeLineData.Id, new System.Action(fgkbhafhpkp.execTimeLineDatab__0), _isVoiceDownLoad: _isVoiceDownLoad);
                    break;
                case eStoryBattleDataType.SKILL:
                    // ISSUE: reference to a compiler-generated field
                    UnitCtrl unitCtrl1 = this.searchUnit(fgkbhafhpkp._timeLineData.Id);
                    // ISSUE: reference to a compiler-generated field
                    int mainSkillId = unitCtrl1.MainSkillIdList[fgkbhafhpkp._timeLineData.SkillNum - 1];
                    unitCtrl1.CancelByAwake = true;
                    unitCtrl1.CurrentTriggerSkillId = mainSkillId;
                    unitCtrl1.SetState(UnitCtrl.ActionState.SKILL, _skillId: mainSkillId);
                    break;
                case eStoryBattleDataType.EXTRA_SKILL:
                    // ISSUE: reference to a compiler-generated field
                    UnitCtrl unitCtrl2 = this.searchUnit(fgkbhafhpkp._timeLineData.Id);
                    // ISSUE: reference to a compiler-generated field
                    unitCtrl2.SetState(UnitCtrl.ActionState.SKILL, _skillId: unitCtrl2.SpecialSkillIdList[fgkbhafhpkp._timeLineData.SkillNum]);
                    break;
                case eStoryBattleDataType.VOICE:
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    this.searchUnit(fgkbhafhpkp._timeLineData.Id).PlayVoice(SoundManager.eVoiceType.OTHER, fgkbhafhpkp._timeLineData.SkillNum, false, true);
                    break;
                case eStoryBattleDataType.UNION_BURST:
                    // ISSUE: reference to a compiler-generated field
                    this.searchUnit(fgkbhafhpkp._timeLineData.Id).SetEnergy(1000f, eSetEnergyType.BY_STORY_TIME_LINE);
                    break;
                case eStoryBattleDataType.ATTACK:
                    // ISSUE: reference to a compiler-generated field
                    this.searchUnit(fgkbhafhpkp._timeLineData.Id).SetState(UnitCtrl.ActionState.ATK);
                    break;
            }
        }*/

        private UnitCtrl searchUnit(int unitId)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            //BattleManager.ONNMAEBGCIB onnmaebgcib = new BattleManager.ONNMAEBGCIB();
            // ISSUE: reference to a compiler-generated field
            //onnmaebgcib._unitId = unitId;
            // ISSUE: reference to a compiler-generated method
            UnitCtrl unitCtrl = UnitList.Find(a => a.UnitId == unitId);
            // ISSUE: reference to a compiler-generated method
            return unitCtrl != null ? unitCtrl : EnemyList.Find(a => a.UnitId == unitId);
        }

        public UnitCtrl Summon(SummonData data)
        {
            //return new UnitCtrl();
            
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            DisplayClass1 defaultClass = new DisplayClass1
            {
                _summonData = data,
                _this = this
            };
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            //defaultClass.unitCtrl = UnityEngine.Object.Instantiate<GameObject>(defaultClass._summonData.Prefab).GetComponent<UnitCtrl>();
            defaultClass.unitCtrl = MyGameCtrl.Instance.LoadSummonPrefabImmediately(data);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            defaultClass.ucTransform = defaultClass.unitCtrl.transform;
            // ISSUE: reference to a compiler-generated field
            //defaultClass.ucTransform.parent = this.enemyParentTransform;
            // ISSUE: reference to a compiler-generated field
            defaultClass.ucTransform.localScale = Vector3.one;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            defaultClass.moveType = defaultClass._summonData.MoveSpeed <= 1.0 ? SummonAction.eMoveType.NORMAL : SummonAction.eMoveType.LINEAR;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            defaultClass.ucTransform.localPosition = defaultClass.moveType != SummonAction.eMoveType.NORMAL ? defaultClass._summonData.FromPosition : defaultClass._summonData.TargetPosition;
            int _rarity = 0;
            // ISSUE: reference to a compiler-generated field
            //int summonResourceId = UnitUtility.GetSummonResourceId(defaultClass._summonData.SummonId);
            //SpineResourceInfo resourceInfo1 = eSpineType.SD_NORMAL_BOUND.GetResourceInfo();
            // ISSUE: reference to a compiler-generated field
            //long index1 = ResourceManager.CreateIndex((long)UnitUtility.GetSkinId(eSpineType.SD_NORMAL_BOUND, summonResourceId, defaultClass._summonData.Owner.SDSkin), 0L);
            //bool flag1 = JFDEDPMIGGN.DNDJJBBMGLD.IsUseAssetBundle();
            //if (flag1 && ResourceManager.IsExistResource(resourceInfo1.bundleId, index1) || !flag1 && ResourceManager.IsExistResource(resourceInfo1.skelton, index1))
            //{
                // ISSUE: reference to a compiler-generated field
            //    _rarity = defaultClass._summonData.Owner.SDSkin;
            //}
            eSpineType _spineType = eSpineType.SD_NORMAL_BOUND;
            // ISSUE: reference to a compiler-generated field
            //if (defaultClass._summonData.Owner.IsShadow && !this.battleEffectPool.BACHGMADMKC)
            //{
             //   SpineResourceInfo resourceInfo2 = eSpineType.SD_SHADOW_BOUND.GetResourceInfo();
             //   long index2 = ResourceManager.CreateIndex((long)UnitUtility.GetSkinId(eSpineType.SD_SHADOW_BOUND, summonResourceId, _rarity), 0L);
            //    bool flag2 = JFDEDPMIGGN.DNDJJBBMGLD.IsUseAssetBundle();
            //    if (flag2 && ResourceManager.IsExistResource(resourceInfo2.bundleId, index2) || !flag2 && ResourceManager.IsExistResource(resourceInfo2.skelton, index2))
             //       _spineType = eSpineType.SD_SHADOW_BOUND;
            //}
            GameObject gameObject = new GameObject();
            // ISSUE: reference to a compiler-generated field
            gameObject.transform.SetParent(defaultClass.ucTransform.TargetTransform, false);
            gameObject.name = "rotate_center";
            // ISSUE: reference to a compiler-generated field
            defaultClass.unitCtrl.RotateCenter = gameObject.transform;
            // ISSUE: reference to a compiler-generated method
            //BattleSpineController.LoadCreateImmidiate(_spineType, summonResourceId, _rarity, gameObject.transform, new System.Action<BattleSpineController>(defaultClass.Summonb__0));
            MyGameCtrl.CreateSummonSpine(data, defaultClass.unitCtrl, gameObject, defaultClass.Summonb__0);
            //this.resourceManager.StartLoad();
            // ISSUE: reference to a compiler-generated field
            return defaultClass.unitCtrl;
        }
        private sealed class DisplayClass1
        {
            public UnitCtrl unitCtrl;
            public SummonData _summonData;
            public BattleManager _this;
            public SummonAction.eMoveType moveType;
            public FixedTransformMonoBehavior.FixedTransform ucTransform;

            internal void Summonb__0(BattleSpineController sa)
            {
                //BattleManager.BHNAODLILJF bhnaodliljf = new BattleManager.BHNAODLILJF
                //{
                //    _obj = sa
                //};
                unitCtrl.CurrentState = UnitCtrl.ActionState.SUMMON;
                unitCtrl.UnitSpineCtrl = sa;
                sa.Owner = unitCtrl;
                //sa.LoadAnimationIDImmediately(eSpineBinaryAnimationId.COMMON_BATTLE);
                //sa.LoadAnimationIDImmediately(eSpineBinaryAnimationId.BATTLE);
                sa.SetAnimeEventDelegateForBattle(()=> { sa.IsStopState = true; });
                unitCtrl.CenterBone = sa.skeleton.FindBone("Center");
                unitCtrl.StateBone = sa.skeleton.FindBone("State");
                UnitParameter parameter = null;
                PCRCaculator.UnitData unitData_my = new PCRCaculator.UnitData();
                if (UnitUtility.IsQuestMonsterUnit(_summonData.SummonId))
                {
                    //parameter = UnitUtility.CreateUnitParameterFromEnemyParameter(this._summonData.SummonId);
                    //Debug.LogError("特殊召唤物鸽了！");
                    EnemyData enemyData = GuildManager.EnemyDataDic[_summonData.SummonId];
                    unitData_my = enemyData.CreateUnitData();
                    parameter = TempData.CreateUnitParameter(enemyData, GuildPlayerGroupData.LogBarrierType.FullBarrier);
                }
                else
                {
                    //parameter = UnitUtility.CreateSummonUnitParameter(this._summonData.Owner.UnitParameter, this._summonData.SummonId, this._summonData.Skill.Level, this._summonData.ConsiderEquipmentAndBonus, this._summonData.Owner.MainSkill1Evolved);
                    PCRCaculator.UnitData ownerData = _summonData.Owner.unitData;
                    unitData_my = new PCRCaculator.UnitData(_summonData.SummonId, _summonData.Skill.Level,ownerData.rarity,ownerData.rank);
                    parameter = TempData.CreateUnitParameter(unitData_my);
                }
                bool flag = (_summonData.SummonSide == SummonAction.eSummonSide.OURS) ? _summonData.Owner.IsOther : !_summonData.Owner.IsOther;

                unitCtrl.Initialize(parameter,unitData_my, flag, true, false,
                    _summonData.ConsiderEquipmentAndBonus ? MainManager.Instance.UnitRarityDic[_summonData.Owner.UnitId].GetBonusData(_summonData.Owner.unitData) : null,
                    _summonData.ConsiderEquipmentAndBonus ? MainManager.Instance.UnitRarityDic[_summonData.Owner.UnitId].GetEXSkillValueNoEv(unitData_my) : null);
                unitCtrl.MaxHpAfterPassive = unitCtrl.MaxHp;

                if (flag)
                {
                    unitCtrl.OnDieForZeroHp = (Action<UnitCtrl>)Delegate.Combine(unitCtrl.OnDieForZeroHp, new Action<UnitCtrl>(_this.onDieEnemy));
                    _this.EnemyList.Add(unitCtrl);
                }
                else
                {
                    _this.UnitList.Add(unitCtrl);
                }
                BattleManager.Instance.QueueUpdateSkillTarget();
                unitCtrl.ExecActionOnStartAndDetermineInstanceID();
                unitCtrl.SummonType = _summonData.SummonType;
                unitCtrl.IsSummonOrPhantom = unitCtrl.SummonType == SummonAction.eSummonType.SUMMON || unitCtrl.SummonType == SummonAction.eSummonType.PHANTOM;
                unitCtrl.SetLeftDirection(_summonData.Owner.IsLeftDir);
                int skillNum = _summonData.Skill.SkillNum;
                //if (this.unitCtrl.SummonEffects.Count > 0)
                //{
                //    this.unitCtrl.AppendCoroutine(this.unitCtrl.CreatePrefabWithTime(this.unitCtrl.SummonEffects, false, skillNum, false, false, false), ePauseType.VISUAL, (this._summonData.Skill.BlackOutTime > 0f) ? this._summonData.Owner : null);
                //}
                eUnitRespawnPos respawnPos = eUnitRespawnPos.MAIN_POS_1;
                SummonAction.eSummonType type = _summonData.SummonType;
                if ((type - 1) > SummonAction.eSummonType.SUMMON)
                {
                    if (type == SummonAction.eSummonType.DIVISION)
                    {
                        respawnPos = _summonData.RespawnPos;
                    }
                }
                else if (_summonData.UseRespawnPos)
                {
                    respawnPos = _summonData.RespawnPos;
                }
                else
                {
                    respawnPos = _this.SearchRespawnPos(_summonData.Owner.RespawnPos, flag ? _this.EnemyList : _this.UnitList, _summonData.SummonId);
                }
                unitCtrl.SummonRespawnPos = respawnPos;
                if (unitCtrl.GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.SUMMON, skillNum) || (moveType == SummonAction.eMoveType.LINEAR))
                {
                    unitCtrl.RespawnPos = _summonData.Owner.RespawnPos;
                    if (unitCtrl.GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.SUMMON, skillNum))
                    {
                        unitCtrl.PlayAnime(eSpineCharacterAnimeId.SUMMON, skillNum, -1, -1, false);
                    }
                    if ((_summonData.Skill.BlackOutTime > 0f) && (_this.BlackOutUnitList.Count > 0))
                    {
                        unitCtrl.SetSortOrderFront();
                        _this.BlackOutUnitList.Add(unitCtrl);
                        _this.AppendCoroutine(unitCtrl.UpdateSummon(skillNum, unitCtrl.SummonRespawnPos, moveType, _summonData.TargetPosition, _summonData.MoveSpeed), ePauseType.SYSTEM, _summonData.Owner);
                    }
                    else
                    {
                        unitCtrl.SetSortOrderBack();
                        _this.AppendCoroutine(unitCtrl.UpdateSummon(skillNum, unitCtrl.SummonRespawnPos, moveType, _summonData.TargetPosition, _summonData.MoveSpeed), ePauseType.SYSTEM);
                    }
                    unitCtrl.SortOrder--;
                }
                else
                {
                    unitCtrl.BattleStartProcess(respawnPos);
                    ucTransform.SetLocalPosY(_this.GetRespawnPos(respawnPos));
                    unitCtrl.SetState(UnitCtrl.ActionState.WALK);
                }
            }
        }
        public eUnitRespawnPos SearchRespawnPos(
          eUnitRespawnPos KMCJKFEHABB,
          List<UnitCtrl> CNBJIKBCFIC,
          int AGAPBBHBIJD)
        {
            eUnitRespawnPos eUnitRespawnPos = eUnitRespawnPos.MAIN_POS_3;
            for (int index = 0; index < BattleDefine.SUMMON_RESPAWN_PRIORITY[KMCJKFEHABB].Count; ++index)
            {
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                //BattleManager.LBEJKIBCKBF lbejkibckbf = new BattleManager.LBEJKIBCKBF();
                // ISSUE: reference to a compiler-generated field
                var respawnPos = BattleDefine.SUMMON_RESPAWN_PRIORITY[KMCJKFEHABB][index];
                if (index % 2 == 1)
                {
                    // ISSUE: object of a compiler-generated type is created
                    // ISSUE: variable of a compiler-generated type
                    //BattleManager.IBEKHHBMNHH ibekhhbmnhh = new BattleManager.IBEKHHBMNHH();
                    // ISSUE: reference to a compiler-generated field
                    var evenRPos = BattleDefine.SUMMON_RESPAWN_PRIORITY[KMCJKFEHABB][index - 1];
                    // ISSUE: reference to a compiler-generated method
                    if (CNBJIKBCFIC.FindIndex(a => a.SummonRespawnPos == evenRPos) != -1)
                    {
                        // ISSUE: reference to a compiler-generated method
                        if (CNBJIKBCFIC.Find(a => a.SummonRespawnPos == evenRPos).UnitId == AGAPBBHBIJD)
                            continue;
                    }
                    else
                    {
                        // ISSUE: reference to a compiler-generated method
                        if (CNBJIKBCFIC.Find(a => a.SummonRespawnPos == evenRPos).UnitId == AGAPBBHBIJD)
                            continue;
                    }
                }
                // ISSUE: reference to a compiler-generated method
                // ISSUE: reference to a compiler-generated method
                if (CNBJIKBCFIC.FindIndex(a => a.SummonRespawnPos == respawnPos) == -1 && CNBJIKBCFIC.FindIndex(a => a.SummonRespawnPos == respawnPos) == -1)
                {
                    // ISSUE: reference to a compiler-generated field
                    eUnitRespawnPos = respawnPos;
                    break;
                }
            }
            return eUnitRespawnPos;
        }

        public void AppendBattleLog(eBattleLogType logType, int HLIKLPNIOKJ, long KGNFLOPBOMB, long KDCBJHCMAOH, int FNGPHAODBAM, int OJHBHHCOAGK, int PFLDDMLAICG = 1, int PNJFIOPGCIC = 1, UnitCtrl JELADBAMFKH = null, UnitCtrl LIMEKPEENOB = null)
        {
            //string word = "log:" + logType.GetDescription();
            //Debug.Log(word);
        }

        private bool LDOADNIPMLK { set; get; }

        //public bool EBEBMGJEHOE { get; private set; }

        //public void SetStartSkillExec(bool AAFJOOCLABG) => this.EBEBMGJEHOE = AAFJOOCLABG;

        /*public void TutorialSkillExec(GameObject MIJGBDJKAGJ)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            BattleManager.ILKPLOFFIKM ilkploffikm = new BattleManager.ILKPLOFFIKM();
            // ISSUE: reference to a compiler-generated field
            ilkploffikm._this = this;
            // ISSUE: reference to a compiler-generated field
            ilkploffikm._focus = MIJGBDJKAGJ;
            if (this.LDOADNIPMLK)
                return;
            this.LDOADNIPMLK = true;
            this.GamePause(true, false);
            this.EBEBMGJEHOE = true;
            // ISSUE: reference to a compiler-generated method
            ManagerSingleton<DialogManager>.Instance.OpenTutorialAdventure(22, new System.Action(ilkploffikm.TutorialSkillExecb__0), _isVoiceDownLoad: this.IsVoiceDownLoad());
        }*/
        /*private sealed class ILKPLOFFIKM
        {
            public BattleManager _this;
            public GameObject _focus;

            internal void TutorialSkillExecb__0()
            {
                ManagerSingleton<TutorialManager>.Instance.SetTutorialBlockObject(true);
                FrameRateUtil.ChangeRate(this._this.FameRate);
                this._this.focusObject = this._focus;
                if (this._this.focusButton == null)
                {
                    this._this.focusButton = this._this.focusObject.GetComponent<CustomUIButton>();
                    this._this.cacheEventDelegate = new List<EventDelegate>(this._this.focusButton.GetOnClickDelegate());
                    this._this.focusButton.SetOnClickDelegate(this._this, new Action(this._this.resetStartSkillExecFlag));
                }
                GameObject[] args = new GameObject[] { this._focus };
                this._this.dialogManager.OpenFocus(() => ManagerSingleton<TutorialManager>.Instance.SetTutorialBlockObject(false), args);
            }
        }*/
        /*public void TutorialOpenFocusDialog()
        {
            if ((UnityEngine.Object)this.focusObject == (UnityEngine.Object)null || this.dialogManager.IsUse(eDialogId.FOCUS_OBJECT))
                return;
            this.dialogManager.OpenFocus((System.Action)null, this.focusObject);
        }*/

        /*private void resetStartSkillExecFlag()
        {
            this.EBEBMGJEHOE = false;
            if (!((UnityEngine.Object)this.focusButton != (UnityEngine.Object)null))
                return;
            EventDelegate.Execute(this.cacheEventDelegate);
            this.focusButton.SetOnClickDelegate(this.cacheEventDelegate);
            this.cacheEventDelegate = (List<EventDelegate>)null;
        }*/

        /*public bool IsVoiceDownLoad()
        {
            EHPLBCOOOPK.OGEJKAHGHLD pclnmookcbg = this.tempData.PCLNMOOKCBG;
            return (int)pclnmookcbg.PHPOANOKDDA == 0 || (bool)pclnmookcbg.EEDNFKEDLPM;
        }*/
        /*
    Coroutine HJEGJNLKOML.Elements\u002EBattle\u002EIBattleManagerForSkillEffectCtrl\u002EStartCoroutine(
      IEnumerator MANJGLLICHN) => this.StartCoroutine(MANJGLLICHN);

    Coroutine PFKDOJCJOOG.Elements\u002EBattle\u002EIBattleManagerForAbromalStateIconController\u002EStartCoroutine(
      IEnumerator MANJGLLICHN) => this.StartCoroutine(MANJGLLICHN);

    void PFKDOJCJOOG.Elements\u002EBattle\u002EIBattleManagerForAbromalStateIconController\u002EStopCoroutine(
      IEnumerator MANJGLLICHN) => this.StopCoroutine(MANJGLLICHN);

    Coroutine FMHLMNGHNJG.Elements\u002EBattle\u002EIBattleManagerForLifeGaugeController\u002EStartCoroutine(
      IEnumerator MANJGLLICHN) => this.StartCoroutine(MANJGLLICHN);

    Coroutine ALBOCKOGNKI.Elements\u002EBattle\u002EIBattleManagerForActionParameter\u002EStartCoroutine(
      IEnumerator LJBCBFFGDDK) => this.StartCoroutine(LJBCBFFGDDK);

    Coroutine GHPNJFDPICH.Elements\u002EBattle\u002EIBattleManagerForUnitActionController\u002EStartCoroutine(
      IEnumerator LJBCBFFGDDK) => this.StartCoroutine(LJBCBFFGDDK);

    Coroutine FGOJJGIFFFJ.Elements\u002EBattle\u002EIBattleManagerForBaseBattleProcessor\u002EStartCoroutine(
      IEnumerator LJBCBFFGDDK) => this.StartCoroutine(LJBCBFFGDDK);
      */
        private enum ACIIMEDNDDJ
        {
            NOT_RUNNING,
            RUNNING,
            STOP_PULSE,
        }
        public float FEDKJAIEDGI
        {
            get;
            private set;
        }
        public float FNHFJLAENPF => DeltaTime_60fps;

        public bool PositionChanged(int cacheKey)
        {
            return cacheKey != skillTargetCacheKey;
        }

        public bool PositionChanged(ref int cacheKey)
        {
            var result = cacheKey != skillTargetCacheKey;
            cacheKey = skillTargetCacheKey;
            return result;
        }
    }
}
