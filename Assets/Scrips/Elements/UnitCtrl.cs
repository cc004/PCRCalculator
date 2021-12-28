// Decompiled with JetBrains decompiler
// Type: Elements.UnitCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
using Cute;
//using Cute.Cri.Movie;
using Elements.Battle;
//using Elements.Data;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

namespace Elements
{
    public partial class UnitCtrl : FixedTransformMonoBehavior, ISingletonField
    {
        private PrincessFormProcessor princessFormProcessor;
        public const float SLOW_ANIM_SPEED = 0.5f;
        public const float HASTE_ANIM_SPEED = 2f;
        public const float START_CAST_TIME = 0.3f;
        public const float START_CAST_TIME_STAND_BY = 2.5f;
        public const int DIE_MOTION1 = 1;
        public const int MODE_CHANGE1 = 1;
        private static readonly Color32 DAMAGE_FLASH_COLOR_32 = new Color32(byte.MaxValue, (byte)60, (byte)30, byte.MaxValue);
        private static readonly Color DAMAGE_FLASH_COLOR = (Color)UnitCtrl.DAMAGE_FLASH_COLOR_32;
        public const int DAMAGE_FLASH_FRAME = 4;
        public const int FLASH_DELAY_FRAME = 0;
        private const float SORT_OFFSET_RESET_TIME = 0.6f;
        private const int ATTACK_PATTERN_TYPE_DIGIT = 1000;
        private const int SUMMON_LOOP_MOTION_SUFFIX = 1;
        private const int SUMMON_LOOP_END_MOTION_SUFFIX = 2;
        private const float VOICE_FADE_DURATION = 0.3f;
        private const float BOSS_SHADOW_BASE_X = 100f;
        private const float PERCENT_DIGIT = 100f;
        public Dictionary<int, int> SkillUseCount = new Dictionary<int, int>();
        public int CurrentRotateCoroutineId;
        /*[SerializeField]
        private List<ShakeEffect> gameStartShakes;
        [SerializeField]
        private List<PrefabWithTime> gameStartEffects;
        [SerializeField]
        private List<PrefabWithTime> dieEffects;
        [SerializeField]
        private List<ShakeEffect> dieShakes;
        [SerializeField]
        private List<PrefabWithTime> damageEffects;
        [SerializeField]
        private List<ShakeEffect> damageShakes;
        public List<PrefabWithTime> SummonEffects;
        [SerializeField]
        private List<PrefabWithTime> idleEffects;
        [SerializeField]
        private List<PrefabWithTime> auraEffects;*/
        [SerializeField]
        public float ShowTitleDelay = 0.5f;
        [SerializeField]
        public float UnitAppearDelay = 3f;
        [SerializeField]
        public float BossAppearDelay = 0.8f;
        [SerializeField]
        public float BattleCameraSize = 1f;
        public float Scale = 1.35f;
        public float BossDeltaX;
        public float BossDeltaY;
        public float AllUnitCenter;
        public float BossBodyWidthOffset;
        public string SummonTargetAttachmentName = "";
        public string SummonAppliedAttachmentName = "";
        public bool IsGameStartDepthBack;
        public bool BossSortIsBack;
        public bool DisableFlash;
        public List<AttachmentChangeData> SortFrontDiappearAttachmentChangeDataList = new List<AttachmentChangeData>();
        public bool IsForceLeftDir;
        public static int DamageFlashFrame = 0;
        public static int FlashDelayFrame = 0;
        //private LifeGaugeController lifeGauge;
        private Transform bottomTransform;
        private bool m_bPause;
        private int m_sSortOrder;
        private UnitActionController unitActionController;
        //private int hitEffectSortOffset;
        //private float sortOffsetResetTimer = 0.6f;
        //private PrefabWithTime.eEffectDifficulty effectDifficulty;
        //private static Yggdrasil<UnitCtrl> staticSingletonTree = (Yggdrasil<UnitCtrl>)null;
        private static BattleLogIntreface staticBattleLog = (BattleLogIntreface)null;
        //private static INADFELJLMH staticBattleCameraEffect = (INADFELJLMH)null;
        //private static BattleEffectPoolInterface staticBattleEffectPool = (BattleEffectPoolInterface)null;
        private static BattleSpeedInterface staticBattleTimeScale = (BattleSpeedInterface)null;
        private float resultMoveDistance;
        public System.Action<UnitCtrl> OnDieForZeroHp;
        public System.Action<UnitCtrl> OnDieFadeOutEnd;
        public System.Action<UnitCtrl> EnergyChange;
        public UnitCtrl.OnDamageDelegate OnDamage;
        public System.Action OnSlipDamage;
        public UnitCtrl.OnDamageDelegate OnHpChange;
        public UnitCtrl.OnDeadDelegate OnDeadForRevival;
        public Dictionary<int, int> SkillLevels = new Dictionary<int, int>();
        public Dictionary<int, float> SkillAreaWidthList = new Dictionary<int, float>();
        //private Dictionary<int, UnitCtrl.VoiceTypeAndSkillNumber> voiceTypeDictionary = new Dictionary<int, UnitCtrl.VoiceTypeAndSkillNumber>();
        public Dictionary<int, eSpineCharacterAnimeId> animeIdDictionary = new Dictionary<int, eSpineCharacterAnimeId>();
        public List<int> MainSkillIdList = new List<int>();
        public List<int> SpecialSkillIdList = new List<int>();
        public List<int> MainSkillEvolutionIdList = new List<int>();
        public List<int> SpecialSkillEvolutionIdList = new List<int>();
        public List<eSpineCharacterAnimeId> TreasureAnimeIdList = new List<eSpineCharacterAnimeId>();
        private bool isDeadBySetCurrentHp;
        private static BattleManager staticBattleManager = (BattleManager)null;
        private Dictionary<UnitCtrl.eAbnormalState, bool> m_abnormalState = new Dictionary<UnitCtrl.eAbnormalState, bool>((IEqualityComparer<UnitCtrl.eAbnormalState>)new UnitCtrl.eAbnormalState_DictComparer());
        private Dictionary<UnitCtrl.eAbnormalStateCategory, AbnormalStateCategoryData> abnormalStateCategoryDataDictionary = new Dictionary<UnitCtrl.eAbnormalStateCategory, AbnormalStateCategoryData>((IEqualityComparer<UnitCtrl.eAbnormalStateCategory>)new UnitCtrl.eAbnormalStateCategory_DictComparer());
        private Dictionary<GameObject, int> statusEffectOrderDictionary = new Dictionary<GameObject, int>();
        public static readonly Dictionary<UnitCtrl.eAbnormalState, AbnormalConstData> ABNORMAL_CONST_DATA = new Dictionary<UnitCtrl.eAbnormalState, AbnormalConstData>((IEqualityComparer<UnitCtrl.eAbnormalState>)new UnitCtrl.eAbnormalState_DictComparer())
    {
      {
        UnitCtrl.eAbnormalState.GUARD_ATK,
        new AbnormalConstData()
        {
          IconType = eStateIconType.PHYSICS_BARRIAR,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.GUARD_MGC,
        new AbnormalConstData()
        {
          IconType = eStateIconType.MAGIC_BARRIAR,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.DRAIN_ATK,
        new AbnormalConstData()
        {
          IconType = eStateIconType.PHYSICS_DRAIN_BARRIAR,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.DRAIN_MGC,
        new AbnormalConstData()
        {
          IconType = eStateIconType.MAGIC_DRAIN_BARRIAR,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.GUARD_BOTH,
        new AbnormalConstData()
        {
          IconType = eStateIconType.BOTH_BARRIAR,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.DRAIN_BOTH,
        new AbnormalConstData()
        {
          IconType = eStateIconType.BOTH_DRAIN_BARRIAR,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.HASTE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.HASTE,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.POISON,
        new AbnormalConstData()
        {
          IconType = eStateIconType.SLIP_DAMAGE,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.BURN,
        new AbnormalConstData()
        {
          IconType = eStateIconType.BURN,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.CURSE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.CURSE,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.SLOW,
        new AbnormalConstData()
        {
          IconType = eStateIconType.SLOW,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.PARALYSIS,
        new AbnormalConstData()
        {
          IconType = eStateIconType.PARALISYS,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.FREEZE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.FREEZE,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.CONVERT,
        new AbnormalConstData()
        {
          IconType = eStateIconType.CONVERT,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.PHYSICS_DARK,
        new AbnormalConstData()
        {
          IconType = eStateIconType.PHYSICS_DARK,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.MAGIC_DARK,
        new AbnormalConstData()
        {
          IconType = eStateIconType.MAGIC_DARK,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.SILENCE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.SILENCE,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.CHAINED,
        new AbnormalConstData()
        {
          IconType = eStateIconType.CHAINED,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.SLEEP,
        new AbnormalConstData()
        {
          IconType = eStateIconType.SLEEP,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.STUN,
        new AbnormalConstData()
        {
          IconType = eStateIconType.STUN,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.DETAIN,
        new AbnormalConstData()
        {
          IconType = eStateIconType.DETAIN,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.NO_EFFECT_SLIP_DAMAGE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.SLIP_DAMAGE,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION,
        new AbnormalConstData()
        {
          IconType = eStateIconType.NO_DAMAGE,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.NO_ABNORMAL,
        new AbnormalConstData()
        {
          IconType = eStateIconType.DEBUF_BARRIAR,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.NO_DEBUF,
        new AbnormalConstData()
        {
          IconType = eStateIconType.DEBUF_BARRIAR,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.PARTS_NO_DAMAGE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.NONE,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.ACCUMULATIVE_DAMAGE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.NONE,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.DECOY,
        new AbnormalConstData()
        {
          IconType = eStateIconType.DECOY,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.MIFUYU,
        new AbnormalConstData()
        {
          IconType = eStateIconType.NONE,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.STONE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.STONE,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.REGENERATION,
        new AbnormalConstData()
        {
          IconType = eStateIconType.REGENERATION,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.PHYSICS_DODGE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.PHYSICS_DODGE,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.CONFUSION,
        new AbnormalConstData()
        {
          IconType = eStateIconType.CONFUSION,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.VENOM,
        new AbnormalConstData()
        {
          IconType = eStateIconType.VENOM,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.COUNT_BLIND,
        new AbnormalConstData()
        {
          IconType = eStateIconType.COUNT_BLIND,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.INHIBIT_HEAL,
        new AbnormalConstData()
        {
          IconType = eStateIconType.INHIBIT_HEAL,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.FEAR,
        new AbnormalConstData()
        {
          IconType = eStateIconType.FEAR,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.TP_REGENERATION,
        new AbnormalConstData()
        {
          IconType = eStateIconType.TP_REGENERATION,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.HEX,
        new AbnormalConstData()
        {
          IconType = eStateIconType.HEX,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.FAINT,
        new AbnormalConstData()
        {
          IconType = eStateIconType.FAINT,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.COMPENSATION,
        new AbnormalConstData()
        {
          IconType = eStateIconType.COMPENSATION,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.CUT_ATK_DAMAGE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.CUT_ATK_DAMAGE,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.CUT_MGC_DAMAGE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.CUT_MGC_DAMAGE,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.CUT_ALL_DAMAGE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.CUT_ALL_DAMAGE,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.LOG_ATK_BARRIR,
        new AbnormalConstData()
        {
          IconType = eStateIconType.LOG_ATK_BARRIER,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.LOG_MGC_BARRIR,
        new AbnormalConstData()
        {
          IconType = eStateIconType.LOG_MGC_BARRIER,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.LOG_ALL_BARRIR,
        new AbnormalConstData()
        {
          IconType = eStateIconType.LOG_ALL_BARRIER,
          IsBuff = true
        }
      },
      {
        UnitCtrl.eAbnormalState.PAUSE_ACTION,
        new AbnormalConstData()
        {
          IconType = eStateIconType.PAUSE_ACTION,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.UB_SILENCE,
        new AbnormalConstData()
        {
          IconType = eStateIconType.UB_SILENCE,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.HEAL_DOWN,
        new AbnormalConstData()
        {
          IconType = eStateIconType.HEAL_DOWN,
          IsBuff = false
        }
      },
      {
        UnitCtrl.eAbnormalState.NPC_STUN,
        new AbnormalConstData()
        {
          IconType = eStateIconType.NPC_STUN,
          IsBuff = false
        }
      }
    };
        private Dictionary<UnitCtrl.eAbnormalStateCategory, int> slipDamageIdDictionary = new Dictionary<UnitCtrl.eAbnormalStateCategory, int>()
    {
      {
        UnitCtrl.eAbnormalStateCategory.POISON,
        0
      },
      {
        UnitCtrl.eAbnormalStateCategory.CURSE,
        0
      },
      {
        UnitCtrl.eAbnormalStateCategory.BURN,
        0
      },
      {
        UnitCtrl.eAbnormalStateCategory.NO_EFFECT_SLIP_DAMAGE,
        0
      },
      {
        UnitCtrl.eAbnormalStateCategory.VENOM,
        0
      },
      {
        UnitCtrl.eAbnormalStateCategory.HEX,
        0
      },
      {
        UnitCtrl.eAbnormalStateCategory.COMPENSATION,
        0
      }
    };
        private static readonly Color WEAK_COLOR = new Color(0.5411f, 0.6274f, 0.996f);
        private const float RUNOUT_COEFFICIENT = 1.5f;
        private const float RESULT_POSITION_COEFFICIENT_X = 1.5f;
        private const float ZOOM_COEFFICIENT = 1.15f;
        private const float ZOOM_SPAN = 0.3f;
        private const int CHARACTER_DISTANCE = 220;
        private const int CHARACTER_OFFSET_Y = -165;
        private float scaleValue;
        private const float RESULT_UNIT_SCALE = 0.875f;
        public static readonly Dictionary<UnitCtrl.BuffParamKind, BuffDebuffConstData> BUFF_DEBUFF_ICON_DIC = new Dictionary<UnitCtrl.BuffParamKind, BuffDebuffConstData>((IEqualityComparer<UnitCtrl.BuffParamKind>)new UnitCtrl.BuffParamKind_DictComparer())
    {
      {
        UnitCtrl.BuffParamKind.NUM,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.NONE,
          DebuffIcon = eStateIconType.NONE
        }
      },
      {
        UnitCtrl.BuffParamKind.ATK,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_PHYSICAL_ATK,
          DebuffIcon = eStateIconType.DEBUFF_PHYSICAL_ATK
        }
      },
      {
        UnitCtrl.BuffParamKind.DEF,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_PHYSICAL_DEF,
          DebuffIcon = eStateIconType.DEBUFF_PHYSICAL_DEF
        }
      },
      {
        UnitCtrl.BuffParamKind.MAGIC_STR,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_MAGIC_ATK,
          DebuffIcon = eStateIconType.DEBUFF_MAGIC_ATK
        }
      },
      {
        UnitCtrl.BuffParamKind.MAGIC_DEF,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_MAGIC_DEF,
          DebuffIcon = eStateIconType.DEBUFF_MAGIC_DEF
        }
      },
      {
        UnitCtrl.BuffParamKind.DODGE,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_DODGE,
          DebuffIcon = eStateIconType.DEBUFF_DODGE
        }
      },
      {
        UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_CRITICAL,
          DebuffIcon = eStateIconType.DEBUFF_CRITICAL
        }
      },
      {
        UnitCtrl.BuffParamKind.MAGIC_CRITICAL,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_CRITICAL,
          DebuffIcon = eStateIconType.DEBUFF_CRITICAL
        }
      },
      {
        UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_ENERGY_RECOVERY,
          DebuffIcon = eStateIconType.DEBUFF_ENERGY_RECOVERY
        }
      },
      {
        UnitCtrl.BuffParamKind.LIFE_STEAL,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_LIFE_STEAL,
          DebuffIcon = eStateIconType.DEBUFF_LIFE_STEAL
        }
      },
      {
        UnitCtrl.BuffParamKind.MOVE_SPEED,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.NONE,
          DebuffIcon = eStateIconType.DEBUFF_MOVE_SPEED
        }
      },
      {
        UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_PHYSICAL_CRITICAL_DAMAGE,
          DebuffIcon = eStateIconType.DEBUFF_PHYSICAL_CRITICAL_DAMAGE
        }
      },
      {
        UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_MAGIC_CRITICAL_DAMAGE,
          DebuffIcon = eStateIconType.DEBUFF_MAGIC_CRITICAL_DAMAGE
        }
      },
      {
        UnitCtrl.BuffParamKind.ACCURACY,
        new BuffDebuffConstData()
        {
          BuffIcon = eStateIconType.BUFF_ACCURACY,
          DebuffIcon = eStateIconType.DEBUFF_ACCURACY
        }
      }
    };
        private List<int> buffDebuffSkilIds = new List<int>();
        public const float PREFAB_CREATE_OFFSET_TIME = 1f;
        private const float PERCENTAGE_DIGIT = 100f;
        public List<PartsData> BossPartsList = new List<PartsData>();
        public bool UseTargetCursorOver;
        private const float COMBO_DAMAGE_INTERVAL = 0.45f;
        private const float COMBO_EFFECT_OFFSET_ADDITION = 0.1f;
        private const int WEIGHT_SIGNIFICANT_DIGITS = 100;
        private const float ENERGY_CHARGE_RATE_BASE = 100f;
        private const int LIFE_STEAL_BASE = 100;
        private const int DAMAGE_BASE = 100;
        private const float PINCH_HP_RATE = 0.2f;
        private int comboCount;
        private float lastHitTime;
        private int hpHealComboCount;
        private float lastHpHealTime;
        private int energyHealComboCount;
        private float lastEnergyHealTime;
        private long unionburstLifeStealNum;
        [SerializeField]
        private bool damageNumCenterBone;
        private int changeScaleId;
        private int changeSortOrderId;
        private const float AVOID_OVERRAP_TIME = 0.45f;
        //private Dictionary<SoundManager.eVoiceType, int> voiceSuffixDic = new Dictionary<SoundManager.eVoiceType, int>((IEqualityComparer<SoundManager.eVoiceType>)new SoundManager.eVoiceType_DictComparer());
        private const int VOICE_SUF_MAX = 3;
        private int enemyVoiceId;
        public bool UseUbVoice3and4;
        private bool isIdleDamage;
        public const int ATTACK_ID = 1;
        private const int SUMMON_INDEX_INVALID_VALUE = -1;
        private float resumeTime;
        private bool resumeIsStopState;
        private bool animeLoop;
        private string animeName = "";
        private SkillEffectCtrl runSmokeEffect;
        public System.Action<bool> OnStartErrorUndoDivision;
        /*
    public VoiceTimingGroup UnionBurstPlusTimingWithCutin;
    public VoiceTimingGroup UnionBurstPlusTimingNoCutin;
        
    public List<VoiceDelayAndEnable> SpeedUpSkillNameVoiceDelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = true }
    };
    public List<VoiceDelayAndEnable> NormalSkillNameVoiceDelay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 2f, Enable = true }
    };
    public List<VoiceDelayAndEnable> NormalSkillNameVoiceDelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 2f, Enable = true }
    };
    public List<VoiceDelayAndEnable> SpeedUpSkillNameVoiceDelay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = true }
    };
    public List<VoiceDelayAndEnable> NormalSkillVoiceDelay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = true }
    };
    public List<VoiceDelayAndEnable> NormalSkillVoiceDelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = true }
    };
    public List<VoiceDelayAndEnable> SpeedUpSkillVoiceDelay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> SpeedUpSkillVoiceDelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> NormalSkillVoice3Delay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> NormalSkillVoice3DelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> SpeedUpSkillVoice3Delay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> SpeedUpSkillVoice3DelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> NormalSkillVoice4Delay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> NormalSkillVoice4DelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> SpeedUpSkillVoice4Delay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> SpeedUpSkillVoice4DelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> SpeedUpCutInVoiceDelay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> NormalCutInVoiceDelay = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = false }
    };
    public List<VoiceDelayAndEnable> SpeedUpCutInVoiceDelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = true }
    };
    public List<VoiceDelayAndEnable> NormalCutInVoiceDelayWithCutIn = new List<VoiceDelayAndEnable>()
    {
      new VoiceDelayAndEnable() { Delay = 0.0f, Enable = true }
    };
        private CutinVoiceStatus cutinVoiceStatus;
        public const int CUT_IN_VOICE_ID = 1;
        public const int CUT_IN_VOICE_ID2 = 2;
        private const int UB_VOICE_ID = 100;
        private const int UB_NAME_VOICE_ID = 200;
        private const int UB_VOICE3_ID = 300;
        private const int UB_VOICE4_ID = 400;
        private const int SKILL_VOICE_ID = 100;
        private const int SKILL_VOICE_SAME_TIME_MAX = 2;
        private const int ATTACK_VOICE_SAME_TIME_MAX = 1;
        private const float VOICE_RATE_OFFSET_SKILL = 0.5f;
        private const float VOICE_RATE_OFFSET_ATTACK = 0.0f;
        private static readonly string CUT_IN_CUE_NAME = "se_cutin_{0}";
        private const float ENEGY_REDUCE_RATE_BASE = 100f;*/
        public bool IsUbExecTrying;
        public Dictionary<UnitCtrl.eReduceEnergyType, bool> IsReduceEnergyDictionary = new Dictionary<UnitCtrl.eReduceEnergyType, bool>((IEqualityComparer<UnitCtrl.eReduceEnergyType>)new UnitCtrl.eReduceEnergyType_DictComparer())
    {
      {
        UnitCtrl.eReduceEnergyType.MODE_CHANGE,
        false
      },
      {
        UnitCtrl.eReduceEnergyType.SEARCH_AREA,
        false
      },
      {
        UnitCtrl.eReduceEnergyType.CONTINUOUS_DAMAGE_NEARBY,
        false
      },
      {
        UnitCtrl.eReduceEnergyType.CONTINUOUS_DAMAGE,
        false
      }
    };

        public Bone StateBone { get; set; }

        public Bone CenterBone { get; set; }

        public List<SkillEffectCtrl> RepeatEffectList { get; private set; }

        public List<SkillEffectCtrl> AuraEffectList { get; private set; }

        public float BodyWidth { get; set; }

        public SystemIdDefine.eWeaponSeType WeaponSeType { set; get; }

        public SystemIdDefine.eWeaponMotionType WeaponMotionType { set; get; }

        public BattleSpineController UnitSpineCtrl { set; get; }

        public BattleSpineController UnitSpineCtrlModeChange { set; get; }

        //public UnitDamageInfo UnitDamageInfo { get; set; }

        //public List<CircleEffectController> CircleEffectList { get; private set; }

        public bool IsDead { get; set; }

        public bool IsRecreated { get; set; }

        public bool MainSkill1Evolved { get; set; }

        public float DeltaTimeForPause => this.PlayingNoCutinMotion || !((UnityEngine.Object)this.battleManager == (UnityEngine.Object)null) && !this.m_bPause ? this.battleManager.DeltaTime_60fps : 0.0f;

        //public CriAtomSource VoiceSource { get; private set; }

        //public CriAtomSource SeSource { get; private set; }

        public int SoundUnitId { get; private set; }

        public int PrefabId { get; private set; }

        public int UnitInstanceId { get; set; }

        public bool TimeToDie { get; set; }

        public List<UnitCtrl> TargetEnemyList { get; set; }

        public bool IsLeftDir { get; set; }

        public eUnitRespawnPos RespawnPos { get; set; }

        public eUnitRespawnPos SummonRespawnPos { get; set; }

        public bool IdleOnly { get; set; }

        public bool HasUnDeadTime { get; set; }

        public int UnDeadTimeHitCount { get; set; }

        public UnitParameter UnitParameter { get; set; }

        public bool IsOther { get; set; }

        public bool JoyFlag { get; set; }

        public SummonAction.eSummonType SummonType { get; set; }

        public bool IsSummonOrPhantom => this.SummonType == SummonAction.eSummonType.SUMMON || this.SummonType == SummonAction.eSummonType.PHANTOM;

        public bool IsDivision => this.SummonType == SummonAction.eSummonType.DIVISION;

        public bool IsDivisionSourceForDamage { get; set; }

        public bool IsDivisionSourceForDie { get; set; }

        public bool IsPhantom => this.SummonType == SummonAction.eSummonType.PHANTOM;

        public bool IsShadow { get; set; }

        public float OverlapPosX { get; set; }

        public bool MoviePlayed { get; set; }

        public List<long> ActionsTargetOnMe { get; set; }

        public List<FirearmCtrl> FirearmCtrlsOnMe { get; set; }

        private int motionPrefix { get; set; }

        public int MotionPrefix
        {
            get => this.ToadDatas.Count <= 0 ? this.motionPrefix : -1;
            set => this.motionPrefix = value;
        }

        public int PartsMotionPrefix { get; set; }

        public bool SupportSkillEnd { get; set; }

        public int MovieId { get; set; }

        public bool CancelByConvert { get; set; }

        public bool CancelByToad { get; set; }

        public bool ToadRelease { get; set; }

        public bool ToadReleaseDamage { get; set; }

        public bool DieInToad { get; set; }

        private bool idleStartAfterWaitFrame { get; set; }

        public bool CancelByAwake { get; set; }

        public int CurrentTriggerSkillId { get; set; }

        public bool IsBoss { get; set; }

        public bool IsClanBattleOrSekaiEnemy { get; set; }

        public Dictionary<int, UnitCtrl> SummonUnitDictionary { get; set; }

        public UnitCtrl SummonSource { get; set; }

        public CutInFrameData CutInFrameSet { set; get; }

        public int SkillEnableFrame { get; set; }

        public bool IsFront { get; set; }

        public Transform RotateCenter { get; set; }

        public Vector3 FixedCenterPos => new Vector3(this.IsLeftDir || this.IsForceLeftDirOrPartsBoss ? -this.fixedCenterPos.x : this.fixedCenterPos.x, this.fixedCenterPos.y, this.fixedCenterPos.z);

        private Vector3 fixedCenterPos { get; set; }

        public Vector3 FixedStatePos { get; set; }

        public Vector3 ColliderCenter { get; private set; }

        public Vector3 ColliderSize { get; private set; }

        public int CurrentSkillId { get; set; }

        public System.Action OnIsFrontFalse { get; set; }

        public bool HasDieLoop => !((UnityEngine.Object)this.GetCurrentSpineCtrl() == (UnityEngine.Object)null) && this.GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.DIE_LOOP);

        //public VoiceTimingData SpecialVoiceTimingData { get; private set; }

        public bool DisableSortOrderFrontOnBlackoutTarget { get; set; }

        public int SkinRarity { get; set; }

        public List<BattleSpineController> EffectSpineControllerList { get; private set; } = new List<BattleSpineController>();

        public bool IsDepthBack { get; set; }

        public bool IsForceLeftDirOrPartsBoss => this.IsForceLeftDir || this.IsPartsBoss;

        private ActionState currentState;
        public UnitCtrl.ActionState CurrentState
        {
            get 
            { 
                return currentState; 
            }
            set 
            {
                currentState = value;
                //MyOnChangeState?.Invoke(UnitId, currentState, BattleHeaderController.CurrentFrameCount);

            }
        }

        private List<UnitCtrl> targetPlayerList { get; set; }

        private ObscuredFloat m_fCastTimer { get; set; }

        private long accumulateDamage { get; set; }

        private ObscuredFloat skillStackValDmg { get; set; }

        private float skillStackVal { get; set; }

        private List<UnitCtrl> skillTargetList { get; set; }

        private Vector2 leftDirScale { get; set; }

        private Vector2 rightDirScale { get; set; }

        private int attackPatternIndex { get; set; }

        private bool attackPatternIsLoop { get; set; }

        private int currentActionPatternId { get; set; }

        private bool isRunForCatchUp { get; set; }

        private List<SkillEffectCtrl> idleEffectsObjs { get; set; }

        private float moveRate { get; set; }

        private bool isAwakeMotion { get; set; }

        public bool StandByDone { get; set; }

        public bool StartDashDone { get; set; }

        //protected Yggdrasil<UnitCtrl> singletonTree => UnitCtrl.staticSingletonTree;

        protected BattleLogIntreface battleLog => UnitCtrl.staticBattleLog;

        //protected INADFELJLMH battleCameraEffect => UnitCtrl.staticBattleCameraEffect;

        //protected BattleEffectPoolInterface battleEffectPool => UnitCtrl.staticBattleEffectPool;

        protected BattleSpeedInterface battleTimeScale => UnitCtrl.staticBattleTimeScale;

        public System.Action OnDodge { get; set; }

        public System.Action<bool, float, bool> OnDamageForLoopTrigger { get; set; }

        public System.Action<float> OnDamageForLoopRepeat { get; set; }

        public System.Action<bool, float, bool> OnDamageForDivision { get; set; }

        public System.Action<bool> OnDamageForSpecialSleepRelease { get; set; }

        public Dictionary<int, System.Action<bool>> OnDamageListForChangeSpeedDisableByAttack { get; set; }

        public System.Action OnDamageForUIShake { set; get; }

        public bool IsOnDamageCharge { get; set; }

        public int Index { get; set; }

        public int IdentifyNum { get; set; }

        public int UnitId { get; protected set; }

        public int CharacterUnitId { get; protected set; }

        public int SDSkin { get; private set; }

        public int IconSkin { get; private set; }

        public int Rarity { get; private set; }

        public int BattleRarity { get; private set; }

        public ePromotionLevel PromotionLevel { get; private set; }

        public int AtkType { get; protected set; }

        public float MoveSpeed { get; set; }

        public bool StartStateIsWalk { get; set; }

        private float m_fAtkRecastTime { get; set; }

        public float SearchAreaSize { get; set; }

        public float StartSearchAreaSize { get; set; }

        private Dictionary<int, List<int>> attackPatternDictionary { get; set; }

        private Dictionary<int, List<int>> attackPatternLoopDictionary { get; set; }

        public ObscuredLong StartMaxHP { get; private set; }

        public ObscuredInt StartAtk { get; protected set; }

        public ObscuredInt StartMagicStr { get; protected set; }

        public ObscuredInt StartDef { get; protected set; }

        public ObscuredInt StartMagicDef { get; protected set; }

        public ObscuredInt StartDodge { get; protected set; }

        public ObscuredInt StartAccuracy { get; protected set; }

        public ObscuredInt StartPhysicalCritical { get; protected set; }

        public ObscuredInt StartMagicCritical { get; protected set; }

        public ObscuredInt StartEnergyRecoveryRate { get; private set; }

        public ObscuredInt StartLifeSteal { get; private set; }

        public ObscuredFloat StartMoveSpeed { get; private set; }

        public ObscuredInt StartWaveHpRecovery { get; protected set; }

        public ObscuredInt StartWaveEnergyRecovery { get; protected set; }

        public ObscuredInt StartEneryReduceRate { get; private set; }

        public ObscuredInt StartPhysicalPenetrate { get; private set; }

        public ObscuredInt StartMagicPenetrate { get; private set; }

        public ObscuredInt StartHpRecoveryRate { get; private set; }

        public ObscuredInt StartEnergyReduceRate { get; private set; }

        public ObscuredInt StartPhysicalCriticalDamageRate { get; private set; }

        public ObscuredInt StartMagicCriticalDamageRate { get; private set; }

        public ObscuredInt Level { get; protected set; }

        public ObscuredLong Hp { get; internal protected set; }

        public ObscuredLong MaxHp { get; set; }

        public FloatWithEx Atk { get; set; }

        public FloatWithEx MagicStr { get; set; }
        private int def;
        public ObscuredInt Def 
        {
            get { return def; }
            set
            {
                //MyOnBaseValueChanged?.Invoke(UnitId, 1, value, BattleHeaderController.CurrentFrameCount);
                def = value;
            }
        }
        private int magicDef;
        public ObscuredInt MagicDef 
        {
            get { return magicDef; }
            set
            {
                //MyOnBaseValueChanged?.Invoke(UnitId, 2, value, BattleHeaderController.CurrentFrameCount);
                magicDef = value;
            }
        }

        public ObscuredInt Dodge { get; set; }

        public ObscuredInt Accuracy { get; set; }

        public ObscuredInt PhysicalCritical { get; set; }

        public ObscuredInt MagicCritical { get; set; }

        public ObscuredInt WaveHpRecovery { get; protected set; }

        public ObscuredInt WaveEnergyRecovery { get; protected set; }

        public ObscuredInt EnergyRecoveryRate { get; set; }

        public ObscuredInt LifeSteal { get; set; }

        public ObscuredInt PhysicalPenetrate { get; private set; }

        public ObscuredInt MagicPenetrate { get; private set; }

        public ObscuredInt HpRecoveryRate { get; private set; }

        public ObscuredInt EnergyReduceRate { get; private set; }

        public ObscuredInt Rupee { get; set; }

        public ObscuredInt RewardCount { get; set; }

        public ObscuredInt PhysicalCriticalDamageRate { get; set; }

        public ObscuredInt MagicCriticalDamageRate { get; set; }

        public FloatWithEx AtkZero => (MathfPlus.Max(0f, this.Atk) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.ATK));

        public FloatWithEx MagicStrZero => (MathfPlus.Max(0f, this.MagicStr) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.MAGIC_STR));

        public ObscuredInt DefZero => (int)(Mathf.Max(0, (int)this.Def) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.DEF));

        public ObscuredInt MagicDefZero => (int)(Mathf.Max(0, (int)this.MagicDef) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.MAGIC_DEF));

        public ObscuredInt DodgeZero => (int)(Mathf.Max(0, (int)this.Dodge) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.DODGE));

        public ObscuredInt AccuracyZero => (int)(Mathf.Max(0, (int)this.Accuracy) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.ACCURACY));

        public ObscuredInt PhysicalCriticalZero => (int)(Mathf.Max(0, (int)this.PhysicalCritical) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL));

        public ObscuredInt MagicCriticalZero => (int)(Mathf.Max(0, (int)this.MagicCritical) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.MAGIC_CRITICAL));

        public ObscuredInt WaveHpRecoveryZero => (ObscuredInt)Mathf.Max(0, (int)this.WaveHpRecovery);

        public ObscuredInt WaveEnergyRecoveryZero => (ObscuredInt)Mathf.Max(0, (int)this.WaveEnergyRecovery);

        public ObscuredInt EnergyRecoveryRateZero => (ObscuredInt)Mathf.Max(0, (int)this.EnergyRecoveryRate + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE));

        public ObscuredInt LifeStealZero => (int)(Mathf.Max(0, (int)this.LifeSteal) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.LIFE_STEAL));

        public ObscuredInt PhysicalPenetrateZero => (ObscuredInt)Mathf.Max(0, (int)this.PhysicalPenetrate);

        public ObscuredInt MagicPenetrateZero => (ObscuredInt)Mathf.Max(0, (int)this.MagicPenetrate);

        public ObscuredInt HpRecoveryRateZero => (ObscuredInt)Mathf.Max(0, (int)this.HpRecoveryRate);

        public ObscuredInt EnergyReduceRateZero => (ObscuredInt)Mathf.Max(0, (int)this.EnergyReduceRate);

        public ObscuredFloat MoveSpeedZero => (ObscuredFloat)(Mathf.Max(0.0f, this.MoveSpeed) + (float)this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.MOVE_SPEED));

        public ObscuredInt PhysicalCriticalDamageRateOrMin => (int)(Mathf.Max(50, (int)this.PhysicalCriticalDamageRate) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE));

        public ObscuredInt MagicCriticalDamageRateOrMin => (int)(Mathf.Max(50, (int)this.MagicCriticalDamageRate) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE));

        public float StartHpPercent { get; private set; }

        public ObscuredBool IsMoveSpeedForceZero { get; set; }

        //private SoundManager soundManager { get; set; }

        private BattleManager battleManager => UnitCtrl.staticBattleManager;

        public float Energy
        {
            get => (float)this.energy;
            private set
            {
                this.energy = (ObscuredFloat)Mathf.Min((float)UnitDefine.MAX_ENERGY, Mathf.Max(0.0f, value));
                if (this.EnergyChange == null)
                    return;
                this.EnergyChange(this);
            }
        }

        private ObscuredFloat energy { get; set; }

        public bool HasSkillTarget => this.skillTargetList.Count > 0;

        public float EnegryAmount => this.Energy / 1000f;

        public float LifeAmount => (float)(long)this.Hp / (float)(long)this.MaxHp;

        public Transform BottomTransform
        {
            get
            {
                if ((UnityEngine.Object)this.bottomTransform == (UnityEngine.Object)null)
                    this.bottomTransform = (UnityEngine.Object)this.GetCurrentSpineCtrl() == (UnityEngine.Object)null ? (Transform)null : this.GetCurrentSpineCtrl().transform;
                return this.bottomTransform;
            }
        }

        public static void StaticRelease()
        {
            //UnitCtrl.staticSingletonTree = (Yggdrasil<UnitCtrl>)null;
            UnitCtrl.staticBattleManager = (BattleManager)null;
            UnitCtrl.staticBattleLog = (BattleLogIntreface)null;
            //UnitCtrl.staticBattleCameraEffect = (INADFELJLMH)null;
            //UnitCtrl.staticBattleEffectPool = (BattleEffectPoolInterface)null;
            UnitCtrl.staticBattleTimeScale = (BattleSpeedInterface)null;
        }

        private bool judgeRarity6() => this.Rarity == 6;

        public BattleSpineController GetCurrentSpineCtrl()
        {
            if (this.ToadDatas.Count > 0)
                return this.ToadDatas[0].BattleSpineController;
            return this.MotionPrefix != -1 ? this.UnitSpineCtrlModeChange : this.UnitSpineCtrl;
        }

        public bool GetSkill1Charging() => this.unitActionController.Skill1Charging;
    
/*public void SetEnableColor()
{
this.curColor = Color.white;
this.updateCurColor();
}*/

                /*public void ResetColor(ChangeColorEffect _colorEffect)
                {
                    if ((UnityEngine.Object)_colorEffect != (UnityEngine.Object)null)
                    {
                        this.curColorOffsetChannel.Remove(_colorEffect);
                        this.curColorChannel.Remove(_colorEffect);
                    }
                    if (this.IsAbnormalState(UnitCtrl.eAbnormalState.SLOW))
                        this.setWeakColor();
                    else
                        this.SetEnableColor();
                }*/

                private void Awake()
        {
            this.OnAwake();
            this.TimeToDie = false;
            this.ActionsTargetOnMe = new List<long>();
            this.FirearmCtrlsOnMe = new List<FirearmCtrl>();
            this.SummonUnitDictionary = new Dictionary<int, UnitCtrl>();
            this.skillTargetList = new List<UnitCtrl>();
            this.TargetEnemyList = new List<UnitCtrl>();
            this.targetPlayerList = new List<UnitCtrl>();
            this.OnDamageListForChangeSpeedDisableByAttack = new Dictionary<int, System.Action<bool>>();
            this.LifeStealQueueList = new List<Queue<int>>();
            this.StrikeBackDictionary = new Dictionary<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet>((IEqualityComparer<EnchantStrikeBackAction.eStrikeBackEffectType>)new EnchantStrikeBackAction.eStrikeBackEffectType_DictComparer());
            this.AccumulativeDamageDataDictionary = new Dictionary<UnitCtrl, AccumulativeDamageData>();
            this.DamageSealDataDictionary = new Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>();
            this.SealDictionary = new Dictionary<eStateIconType, SealData>();
            this.UbAbnormalDataList = new List<UbAbnormalData>();
            //this.CircleEffectList = new List<CircleEffectController>();
            this.SkillExecCountDictionary = new Dictionary<int, int>();
            //this.curColorChannel = new Dictionary<ChangeColorEffect, Color>();
            //this.curColorOffsetChannel = new Dictionary<ChangeColorEffect, Color>();
            this.curColor = Color.white;
            for (UnitCtrl.eAbnormalStateCategory key = UnitCtrl.eAbnormalStateCategory.NONE; key < UnitCtrl.eAbnormalStateCategory.NUM; ++key)
            {
                AbnormalStateCategoryData stateCategoryData = new AbnormalStateCategoryData();
                this.abnormalStateCategoryDataDictionary.Add(key, stateCategoryData);
            }
            for (UnitCtrl.eAbnormalState key = UnitCtrl.eAbnormalState.GUARD_ATK; key < UnitCtrl.eAbnormalState.NUM; ++key)
                this.m_abnormalState.Add(key, false);
            this.BossPartsListForBattle = new List<PartsData>();
        }

        /*protected override void DestructByOnDestroy()
        {
            base.DestructByOnDestroy();
            this.StateBone = (Bone)null;
            this.CenterBone = (Bone)null;
            this.RepeatEffectList = (List<SkillEffectCtrl>)null;
            this.AuraEffectList = (List<SkillEffectCtrl>)null;
            this.UnitSpineCtrl = (BattleSpineController)null;
            this.UnitSpineCtrlModeChange = (BattleSpineController)null;
            //this.UnitDamageInfo = (UnitDamageInfo)null;
            //this.CircleEffectList = (List<CircleEffectController>)null;
            //this.VoiceSource = (CriAtomSource)null;
            //this.SeSource = (CriAtomSource)null;
            this.TargetEnemyList = (List<UnitCtrl>)null;
            this.UnitParameter = (UnitParameter)null;
            this.ActionsTargetOnMe = (List<long>)null;
            this.FirearmCtrlsOnMe = (List<FirearmCtrl>)null;
            this.SummonUnitDictionary = (Dictionary<int, UnitCtrl>)null;
            this.CutInFrameSet = (CutInFrameData)null;
            this.SkillUseCount = (Dictionary<int, int>)null;
            this.OnIsFrontFalse = (System.Action)null;
            this.gameStartShakes = (List<ShakeEffect>)null;
            this.gameStartEffects = (List<PrefabWithTime>)null;
            this.dieEffects = (List<PrefabWithTime>)null;
            this.dieShakes = (List<ShakeEffect>)null;
            this.damageEffects = (List<PrefabWithTime>)null;
            this.damageShakes = (List<ShakeEffect>)null;
            this.SummonEffects = (List<PrefabWithTime>)null;
            this.idleEffects = (List<PrefabWithTime>)null;
            this.lifeGauge = (LifeGaugeController)null;
            this.bottomTransform = (Transform)null;
            this.targetPlayerList = (List<UnitCtrl>)null;
            this.skillTargetList = (List<UnitCtrl>)null;
            this.unitActionController = (UnitActionController)null;
            this.idleEffectsObjs = (List<SkillEffectCtrl>)null;
            this.OnDieForZeroHp = (System.Action<UnitCtrl>)null;
            this.OnDieFadeOutEnd = (System.Action<UnitCtrl>)null;
            this.EnergyChange = (System.Action<UnitCtrl>)null;
            this.OnDamage = (UnitCtrl.OnDamageDelegate)null;
            this.OnDodge = (System.Action)null;
            this.OnDamageForLoopTrigger = (System.Action<bool, float, bool>)null;
            this.OnDamageForLoopRepeat = (System.Action<float>)null;
            this.OnDamageListForChangeSpeedDisableByAttack = (Dictionary<int, System.Action<bool>>)null;
            this.OnHpChange = (UnitCtrl.OnDamageDelegate)null;
            this.OnDamageForUIShake = (System.Action)null;
            this.OnDeadForRevival = (UnitCtrl.OnDeadDelegate)null;
            this.OnDamageForSpecialSleepRelease = (System.Action<bool>)null;
            this.attackPatternDictionary = (Dictionary<int, List<int>>)null;
            this.attackPatternLoopDictionary = (Dictionary<int, List<int>>)null;
            this.SkillLevels = (Dictionary<int, int>)null;
            this.SkillAreaWidthList = (Dictionary<int, float>)null;
            //this.voiceTypeDictionary = (Dictionary<int, UnitCtrl.VoiceTypeAndSkillNumber>)null;
            this.animeIdDictionary = (Dictionary<int, eSpineCharacterAnimeId>)null;
            this.MainSkillIdList = (List<int>)null;
            this.MainSkillEvolutionIdList = (List<int>)null;
            this.SpecialSkillIdList = (List<int>)null;
            this.SpecialSkillEvolutionIdList = (List<int>)null;
            //this.soundManager = (SoundManager)null;
            this.m_abnormalState = (Dictionary<UnitCtrl.eAbnormalState, bool>)null;
            this.abnormalStateCategoryDataDictionary = (Dictionary<UnitCtrl.eAbnormalStateCategory, AbnormalStateCategoryData>)null;
            this.statusEffectOrderDictionary = (Dictionary<GameObject, int>)null;
            this.OnChangeState = (System.Action<UnitCtrl, eStateIconType, bool>)null;
            this.OnChangeStateNum = (System.Action<UnitCtrl, eStateIconType, int>)null;
            this.buffDebuffSkilIds = (List<int>)null;
            this.LifeStealQueueList = (List<Queue<int>>)null;
            this.StrikeBackDictionary = (Dictionary<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet>)null;
            this.AccumulativeDamageDataDictionary = (Dictionary<UnitCtrl, AccumulativeDamageData>)null;
            this.DamageSealDataDictionary = (Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>)null;
            this.DamageOnceOwnerSealDateDictionary = (Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>)null;
            this.DamageOwnerSealDataDictionary = (Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>)null;
            this.SealDictionary = (Dictionary<eStateIconType, SealData>)null;
            this.UbAbnormalDataList = (List<UbAbnormalData>)null;
            this.SkillExecCountDictionary = (Dictionary<int, int>)null;
            //this.OwnerPassiveAction = (Dictionary<eParamType, PassiveActionValue>)null;
            this.KillBonusTarget = (UnitCtrl)null;
            this.OnLifeAmmountChange = (System.Action<float>)null;
            //this.voiceSuffixDic = (Dictionary<SoundManager.eVoiceType, int>)null;
            this.castTimeDictionary = (Dictionary<int, float>)null;
            this.runSmokeEffect = (SkillEffectCtrl)null;
            this.OnUpdateWhenIdle = (System.Action<float>)null;
            this.slipDamageIdDictionary = (Dictionary<UnitCtrl.eAbnormalStateCategory, int>)null;
            //this.SpecialVoiceTimingData = (VoiceTimingData)null;
            this.BossPartsListForBattle = (List<PartsData>)null;
            //this.curColorOffsetChannel = (Dictionary<ChangeColorEffect, Color>)null;
            //this.curColorChannel = (Dictionary<ChangeColorEffect, Color>)null;
            this.buffDebuffEffectList = (List<SkillEffectCtrl>)null;
            this.aweDatas = (List<AweAction.AweData>)null;
            this.aweCoroutine = (IEnumerator)null;
            this.ToadDatas = (List<ToadData>)null;
            this.OnSlipDamage = (System.Action)null;
            this.OnBreakAll = (System.Action)null;
            this.knightGuardDatas = (Dictionary<eStateIconType, List<KnightGuardData>>)null;
            this.knightGuardCoroutine = (IEnumerator)null;
            //this.princessFormProcessor = (PrincessFormProcessor)null;
            this.EffectSpineControllerList = (List<BattleSpineController>)null;
            this.ModeChangeEndEffectList = (List<SkillEffectCtrl>)null;
        }*/

        public void DestroyAndCoroutineRemove()
        {
            this.battleManager.RemoveCoroutine(this);
            UnityEngine.Object.Destroy((UnityEngine.Object)this.gameObject);
        }

        private void instantiateResources(bool _isOther, bool _isGaugeAlwaysVisible)
        {
            return;
            //this.lifeGauge = ManagerSingleton<ResourceManager>.Instance.LoadImmediately(eResourceId.UNIT_LIFE_GAUGE).GetComponent<LifeGaugeController>();
            if (_isOther != !this.battleManager.IsDefenceReplayMode || this.IsBoss)
                return;
           /* this.OnChangeState += (System.Action<UnitCtrl, eStateIconType, bool>)((_unit, _iconType, _enable) =>
           {
               if (_enable)
                   this.lifeGauge.AddIcon(_iconType);
               else
                   this.lifeGauge.DeleteIcon(_iconType);
           });*/
            //this.OnChangeStateNum += (System.Action<UnitCtrl, eStateIconType, int>)((_unit, _iconType, _num) => this.lifeGauge.SetIconNum(_iconType, _num));
        }

        public void Initialize(
          UnitParameter _data,
          PCRCaculator.UnitData unitData_my,
          bool _isOther,
          bool _isFirstWave,
          bool _isGaugeAlwaysVisible = false,
          PCRCaculator.BaseData additional = null)
        {
            //this.soundManager = ManagerSingleton<SoundManager>.Instance;
            /*if (UnitCtrl.staticSingletonTree == null)
            {
                UnitCtrl.staticSingletonTree = this.CreateSingletonTree<UnitCtrl>();
                UnitCtrl.staticBattleManager = this.singletonTree.Get<BattleManager>();
                UnitCtrl.staticBattleLog = (BattleLogIntreface)this.singletonTree.Get<GJNIHENNINA>();
                //UnitCtrl.staticBattleCameraEffect = (INADFELJLMH)this.singletonTree.Get<CMMLKFHCEPD>();
                //UnitCtrl.staticBattleEffectPool = (BattleEffectPoolInterface)this.singletonTree.Get<BattleEffectPool>();
                UnitCtrl.staticBattleTimeScale = (BattleSpeedInterface)this.singletonTree.Get<BattleSpeedManager>();
                UnitCtrl.FunctionalComparer<BasePartsData>.CreateInstance();
            }*/
            
                UnitCtrl.staticBattleManager = BattleManager.Instance;
                UnitCtrl.staticBattleLog = battleManager;
                UnitCtrl.staticBattleTimeScale = battleManager.battleTimeScale;
                UnitCtrl.FunctionalComparer<BasePartsData>.CreateInstance();
            
            this.OwnerPassiveAction = new Dictionary<eParamType, PassiveActionValue>((IEqualityComparer<eParamType>)new eParamType_DictComparer());
            UnitCtrl.InitializeExAndFreeSkill(_data.UniqueData, ePassiveInitType.OWNER, 0, this);
            this.RepeatEffectList = new List<SkillEffectCtrl>();
            this.AuraEffectList = new List<SkillEffectCtrl>();
            this.CutInFrameSet = new CutInFrameData();
            this.idleEffectsObjs = new List<SkillEffectCtrl>();
            //Scale = Scale *50;//1.35f;
            //if (_data.UniqueData.Id >= 400000)
            Scale *= 0.5f;
            this.leftDirScale = (Vector2)new Vector3(-this.Scale, Mathf.Abs(this.Scale), 1f);
            this.rightDirScale = (Vector2)Vector3.Scale((Vector3)this.leftDirScale, new Vector3(-1f, 1f, 1f));

            //this.leftDirScale = (Vector2)new Vector3(-this.Scale, Mathf.Abs(this.Scale), 1f);
            //this.leftDirScale = (Vector2)new Vector3(-1*scaleDir, 1, 1);
            //this.rightDirScale = (Vector2)Vector3.Scale((Vector3)this.leftDirScale, new Vector3(-1f, 1f, 1f));
            //if ((UnityEngine.Object)this.SeSource == (UnityEngine.Object)null)
            //    this.SeSource = this.gameObject.AddComponent<CriAtomSource>();
            //if ((UnityEngine.Object)this.VoiceSource == (UnityEngine.Object)null)
            //    this.VoiceSource = this.gameObject.AddComponent<CriAtomSource>();
            eBattleCategory jiliicmhlch = this.battleManager.BattleCategory;
            this.UnitParameter = _data;
            this.IsOther = _isOther;
            this.WeaponSeType = (SystemIdDefine.eWeaponSeType)(int)_data.MasterData.SeType;
            this.WeaponMotionType = (SystemIdDefine.eWeaponMotionType)(int)_data.MasterData.MotionType;
            this.UnitId = (int)_data.UniqueData.Id;
            this.CharacterUnitId = (int)_data.MasterData.UnitId;
            this.SoundUnitId = (int)_data.MasterData.PrefabId;
            //MasterEnemyEnableVoice.EnemyEnableVoice enemyEnableVoice = ManagerSingleton<MasterDataManager>.Instance.masterEnemyEnableVoice.Get(this.CharacterUnitId);
            //if (enemyEnableVoice != null)
            //    this.enemyVoiceId = (int)enemyEnableVoice.voice_id;
            //this.SDSkin = UnitUtility.GetSetSkinNo(_data.UniqueData, UnitDefine.eSkinType.Sd, this.IsOther == !this.battleManager.IsDefenceReplayMode);
            this.Rarity = (int)_data.UniqueData.UnitRarity;
            this.BattleRarity = (int)_data.UniqueData.BattleRarity;
            //this.IconSkin = UnitUtility.GetSetSkinNo(_data.UniqueData, UnitDefine.eSkinType.Icon);
            this.PromotionLevel = _data.UniqueData.PromotionLevel;
            this.PrefabId = (int)_data.MasterData.PrefabId;
            this.IsBoss = UnitUtility.JudgeIsBoss(this.UnitId);
            this.IsClanBattleOrSekaiEnemy = _isOther && (jiliicmhlch == eBattleCategory.CLAN_BATTLE || jiliicmhlch == eBattleCategory.GLOBAL_RAID);
            if (jiliicmhlch == eBattleCategory.CLAN_BATTLE && !this.IsBoss)
                this.IsClanBattleOrSekaiEnemy = false;
            this.Level = _data.UniqueData.UnitLevel;
            this.AtkType = (int)_data.MasterData.AtkType;
            this.MotionPrefix = -1;
            this.accumulateDamage = 0L;
            this.SearchAreaSize = (float)(int)_data.MasterData.SearchAreaWidth;
            this.StartSearchAreaSize = (float)(int)_data.MasterData.SearchAreaWidth;
            this.IsDead = false;
            float num = (float)(double)_data.MasterData.AtkCastTime;
            /*if (jiliicmhlch == eBattleCategory.CLAN_BATTLE && this.IsBoss)
            {
                ClanBattleTopInfo clanBattleTopInfo = Singleton<ClanBattleTempData>.Instance.ClanBattleTopInfo;
                MasterClanBattleParamAdjust.ClanBattleParamAdjust paramAdjustData = ClanBattleUtil.GetParamAdjustData(clanBattleTopInfo.ClanBattleId, clanBattleTopInfo.LapNum);
                if (paramAdjustData != null)
                    num = (float)((double)num * (double)paramAdjustData.NormalAtkCastTime / 1000.0);
            }*/
            this.m_fAtkRecastTime = num;
            //this.SpecialVoiceTimingData = BattleVoiceUtility.GetVoiceTimingData(this.UnitId);
            this.instantiateResources(_isOther, _isGaugeAlwaysVisible);
            this.StandByDone = this.IsSummonOrPhantom || (this.battleManager.BattleCategory == eBattleCategory.TUTORIAL || (this.battleManager.CurrentWave != 0 || !this.GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.STAND_BY)));
            this.TargetEnemyList.Clear();
            this.targetPlayerList.Clear();
            this.SetEnergy(0.0f, eSetEnergyType.INITIALIZE);
            //this.lifeGauge.Initialize(_isOther, (float)((double)this.StateBone.WorldY * (double)Math.Abs(this.Scale) + (UnitUtility.IsPersonUnit(this.SoundUnitId) ? (double)BattleDefine.HEAD_OFFSET : 0.0)), this, _isGaugeAlwaysVisible);
            /*if (this.battleManager.DMALFANMBMM && this.IsBoss)
            {
                switch (HatsuneUtility.GetCurrentEventBossDifficulty(Singleton<EHPLBCOOOPK>.Instance.DEBFKNDECIN.GFADDFNBDAJ))
                {
                    case eHatsuneBossDifficulity.NORMAL:
                        this.effectDifficulty = PrefabWithTime.eEffectDifficulty.NORMAL;
                        break;
                    case eHatsuneBossDifficulity.HARD:
                        this.effectDifficulty = PrefabWithTime.eEffectDifficulty.HARD;
                        break;
                    case eHatsuneBossDifficulity.VERY_HARD:
                        this.effectDifficulty = PrefabWithTime.eEffectDifficulty.VERY_HARD;
                        break;
                }
            }
            else if (this.battleManager.BKLOEOBLEDB && this.IsBoss)
            {
                ClanBattleTempData instance = Singleton<ClanBattleTempData>.Instance;
                switch (ClanBattleUtil.GetCurrentEventBossDifficulity(instance.BattleId, instance.BattleLapNum))
                {
                    case ClanBattleDefine.eClanAuraEffectType.NORMAL:
                        this.effectDifficulty = PrefabWithTime.eEffectDifficulty.NORMAL;
                        break;
                    case ClanBattleDefine.eClanAuraEffectType.HARD:
                        this.effectDifficulty = PrefabWithTime.eEffectDifficulty.HARD;
                        break;
                }
            }
            else if (this.battleManager.BattleCategory != eBattleCategory.KAISER_BATTLE_MAIN) { }
                //this.auraEffects.Clear();*/
            /*if (this.IsBoss)
            {
                //this.battleManager.SetBattleCameraScale(this.BattleCameraSize);
                //this.battleManager.OnChangeBattleCameraScale(false);
            }*/
            this.UnitSpineCtrl.AnimationName = this.UnitSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE);
            this.UnitSpineCtrl.transform.localScale = (Vector3)this.rightDirScale;
            this.fixedCenterPos = Vector3.Scale(new Vector3(this.CenterBone.worldX, this.CenterBone.worldY, 0.0f), this.UnitSpineCtrl.transform.lossyScale);
            this.FixedStatePos = Vector3.Scale(new Vector3(this.StateBone.worldX, this.StateBone.worldY, 0.0f), this.UnitSpineCtrl.transform.lossyScale);
            this.DummyPartsData = new BasePartsData()
            {
                PositionX = 0.0f,
                Owner = this
            };
            this.DummyPartsData.SetBattleManager(this.battleManager);
            this.DummyPartsData.InitializeResistStatus((int)_data.UniqueData.ResistStatusId);
            /*if (this.IsBoss && _data != null && (int)_data.break_durability > 0)
            {
                PartsData partsData = new PartsData();
                partsData.Owner = this;
                partsData.PositionX = 0.0f;
                this.MultiBossPartsData = partsData;
                this.MultiBossPartsData.BreakPoint = fromAllKind.break_durability;
                this.MultiBossPartsData.SetBattleManager(this.battleManager);
                this.battleManager.LOGNEDLPEIJ = true;
            }*/
            if (this.IsPartsBoss)
            {
                //MasterEnemyMParts.EnemyMParts _enemyMParts = ManagerSingleton<MasterDataManager>.Instance.masterEnemyMParts.Get(this.UnitId);
                MasterEnemyMParts.EnemyMParts _enemyMParts = MyGameCtrl.Instance.tempData.mParts;
                for (int index = 0; index < this.BossPartsList.Count; ++index)
                {
                    /*PartsData bossParts = this.BossPartsList[index];
                    PartsData partsData1 = new PartsData();
                    partsData1.BodyWidthValue = bossParts.BodyWidthValue;
                    partsData1.PositionX = bossParts.PositionX;
                    partsData1.InitialPositionX = bossParts.PositionX;
                    partsData1.EnemyId = bossParts.EnemyId;
                    partsData1.Index = bossParts.Index;
                    partsData1.Owner = this;
                    partsData1.PositionY = bossParts.PositionY;
                    partsData1.AttachmentNamePairList = bossParts.AttachmentNamePairList;
                    partsData1.ChangeAttachmentStartTime = bossParts.ChangeAttachmentStartTime;
                    partsData1.ChangeAttachmentEndTime = bossParts.ChangeAttachmentEndTime;
                    PartsData partsData2 = partsData1;
                    partsData2.Initialize(_enemyMParts);
                    partsData2.SetBattleManager(this.battleManager);
                    this.BossPartsListForBattle.Add(partsData2);*/
                    PartsData bossParts = this.BossPartsList[index];
                    bossParts.Owner = this;
                    bossParts.Initialize(_enemyMParts);
                    bossParts.SetBattleManager(this.battleManager);
                    this.BossPartsListForBattle.Add(bossParts);
                }
                this.battleManager.LOGNEDLPEIJ = true;
            }
            this.castTimeDictionary = new Dictionary<int, float>();
            this.attackPatternDictionary = new Dictionary<int, List<int>>();
            this.attackPatternLoopDictionary = new Dictionary<int, List<int>>();
            //MasterSkillData masterSkillParameter = ;
            /*System.Action<List<int>, List<SkillLevelInfo>, eSpineCharacterAnimeId, bool> action = (System.Action<List<int>, List<SkillLevelInfo>, eSpineCharacterAnimeId, bool>)((idList, skillLevelInfoList, animeId, isSpecial) =>
           {
               int index = 0;
               for (int count = idList.Count; index < count; ++index)
               {
                   int skillId = idList[index];
                   if (skillId != 0)
                   {
                       //this.voiceTypeDictionary.Add(skillId, new UnitCtrl.VoiceTypeAndSkillNumber()
                       //{
                       //    VoiceType = isSpecial ? SoundManager.eVoiceType.SP_SKILL : SoundManager.eVoiceType.SKILL,
                       //    SkillNumber = index + 1
                       //});
                       PCRCaculator.SkillData data = PCRCaculator.MainManager.Instance.SkillDataDic[skillId];
                       MasterSkillData.SkillData skillData = new MasterSkillData.SkillData(data);
                       if ((int)skillData.skill_area_width < 1)
                           this.SkillAreaWidthList.Add(skillId, (float)(int)_data.MasterData.SearchAreaWidth);
                       else
                           this.SkillAreaWidthList.Add(skillId, (float)(int)skillData.skill_area_width);
                       SkillLevelInfo skillLevelInfo = skillLevelInfoList.Find((Predicate<SkillLevelInfo>)(e => (int)e.SkillId == skillId));
                       this.SkillLevels.Add(skillId, skillLevelInfo == null ? 0 : (int)skillLevelInfo.SkillLevel);
                       if (!this.SkillUseCount.ContainsKey(skillId))
                           this.SkillUseCount.Add(skillId, 0);
                       this.castTimeDictionary.Add(skillId, (float)(double)skillData.skill_cast_time);
                       this.animeIdDictionary.Add(skillId, animeId);
                   }
               }
           });
            action(_data.SkillData.UnionBurstIds, _data.UniqueData.UnionBurst, eSpineCharacterAnimeId.SKILL, false);
            action(_data.SkillData.UnionBurstEvolutionIds, _data.UniqueData.UnionBurst, eSpineCharacterAnimeId.SKILL, false);
            action(_data.SkillData.MainSkillIds, _data.UniqueData.MainSkill, eSpineCharacterAnimeId.SKILL, false);
            action(_data.SkillData.MainSkillEvolutionIds, _data.UniqueData.MainSkill, eSpineCharacterAnimeId.SKILL, false);*/
            SetSkill(_data.SkillData.UnionBurstIds, _data.UniqueData.UnionBurst, eSpineCharacterAnimeId.SKILL, false, _data);
            SetSkill(_data.SkillData.UnionBurstEvolutionIds, _data.UniqueData.UnionBurst, eSpineCharacterAnimeId.SKILL, false, _data);
            SetSkill(_data.SkillData.MainSkillIds, _data.UniqueData.MainSkill, eSpineCharacterAnimeId.SKILL, false, _data);
            SetSkill(_data.SkillData.MainSkillEvolutionIds, _data.UniqueData.MainSkill, eSpineCharacterAnimeId.SKILL,false, _data);

            List<SkillLevelInfo> skillLevelInfoList1 = new List<SkillLevelInfo>();
            for (int index = 0; index < _data.SkillData.SpSkillIds.Count; ++index)
            {
                SkillLevelInfo skillLevelInfo = new SkillLevelInfo();
                skillLevelInfo.SetSkillId(_data.SkillData.SpSkillIds[index]);
                skillLevelInfo.SetSkillLevel((int)this.Level);
                skillLevelInfoList1.Add(skillLevelInfo);
            }
            //action(_data.SkillData.SpSkillIds, skillLevelInfoList1, eSpineCharacterAnimeId.SPECIAL_SKILL, true);
            //action(_data.SkillData.SpSkillEvolutionIds, skillLevelInfoList1, eSpineCharacterAnimeId.SPECIAL_SKILL, true);
            SetSkill(_data.SkillData.SpSkillIds, skillLevelInfoList1, eSpineCharacterAnimeId.SPECIAL_SKILL, true, _data);
            SetSkill(_data.SkillData.SpSkillEvolutionIds, skillLevelInfoList1, eSpineCharacterAnimeId.SPECIAL_SKILL, true, _data);
            this.MainSkillIdList = _data.SkillData.MainSkillIds;
            this.SpecialSkillIdList = _data.SkillData.SpSkillIds;
            this.SpecialSkillEvolutionIdList = _data.SkillData.SpSkillEvolutionIds;
            this.MainSkillEvolutionIdList = _data.SkillData.MainSkillEvolutionIds;
            int burstEvolutionId = _data.SkillData.UnionBurstEvolutionIds[0];
            this.UnionBurstSkillId = burstEvolutionId == 0 || this.SkillLevels[burstEvolutionId] == 0 ? _data.SkillData.UnionBurstIds[0] : burstEvolutionId;
            if (this.UnionBurstSkillId != 0)
            {
                PCRCaculator.SkillData data = PCRCaculator.MainManager.Instance.SkillDataDic[UnionBurstSkillId];
                MasterSkillData.SkillData skillData = new MasterSkillData.SkillData(data);
                this.unionBurstSkillAreaWidth = (float)(int)skillData.skill_area_width;
            }
            int defaultActionPatternId = UnitUtility.GetDefaultActionPatternId(this.UnitId);
            this.CreateAttackPattern(_data.SkillData, defaultActionPatternId);
            this.currentActionPatternId = defaultActionPatternId;
            //UnitData uniqueData = _data.UniqueData;
            PCRCaculator.BaseData baseData = new PCRCaculator.BaseData();
            PCRCaculator.BaseData baseDataEX = new PCRCaculator.BaseData();
            if (MyGameCtrl.Instance.tempData.guildEnemy?.unit_id == unitData_my.unitId)
            {
                baseData = MyGameCtrl.Instance.tempData.guildEnemy.baseData;
            }
            else if (PCRCaculator.MainManager.Instance.unitDataDic.ContainsKey(UnitId))/* if (UnitId <= 200000 || UnitId >= 400000) */
            {
                baseData = PCRCaculator.MainManager.Instance.UnitRarityDic[UnitId].GetBaseData(unitData_my);//,MyGameCtrl.Instance.tempData.isGuildBattle);
                baseDataEX = PCRCaculator.MainManager.Instance.UnitRarityDic[UnitId].GetEXSkillValue(unitData_my);//,MyGameCtrl.Instance.tempData.isGuildBattle);
            }
            else
            {
                baseData = _data.EnemyData.baseData;
            }

            if (additional != null) baseData += additional;

            if (UnitId >= 300000 && UnitId <= 399999)
            {
                var data = MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup();
                if (MyGameCtrl.Instance.tempData.isGuildEnemyViolent)
                {
                    this.MaxHp = this.StartMaxHP = (ObscuredLong)baseData.Hp;
                    this.Hp = MaxHp / 2 - 1;
                }
                else if (MyGameCtrl.Instance.tempData.isGuildBattle && data.usePlayerSettingHP && data.playerSetingHP > 0)
                {
                    this.MaxHp = this.StartMaxHP = (ObscuredLong)baseData.Hp;
                    this.Hp = data.playerSetingHP;
                }
                else
                    this.Hp = this.MaxHp = this.StartMaxHP = (ObscuredLong)baseData.Hp;
                useLogBarrier = data.useLogBarrier;
            }
            else
                this.Hp = this.MaxHp = this.StartMaxHP = (ObscuredLong)baseData.Hp;


            //this.Def = this.StartDef = (ObscuredInt)Mathf.RoundToInt(baseData.Def);
            //this.Atk = this.StartAtk = (ObscuredInt)Mathf.RoundToInt(baseData.Atk);
            //this.MagicStr = this.StartMagicStr = (ObscuredInt)Mathf.RoundToInt(baseData.Magic_str);
            //this.MagicDef = this.StartMagicDef = (ObscuredInt)Mathf.RoundToInt(baseData.Magic_def);
            this.StartDef = (ObscuredInt)Mathf.RoundToInt(baseData.Def);
            this.StartAtk = (ObscuredInt)Mathf.RoundToInt(baseData.Atk);
            this.StartMagicStr = (ObscuredInt)Mathf.RoundToInt(baseData.Magic_str);
            this.StartMagicDef = (ObscuredInt)Mathf.RoundToInt(baseData.Magic_def);
            this.Def = (ObscuredInt)(StartDef + baseDataEX.Def);
            this.Atk = (StartAtk + baseDataEX.Atk);
            this.MagicStr = (StartMagicStr + baseDataEX.Magic_str);
            this.MagicDef = (ObscuredInt)(StartMagicDef + baseDataEX.Magic_def);

            if (IsBoss && group.isSpecialBoss && (group.specialBossID == 666666 || group.specialBossID == 666667))
            {
                this.Hp = this.MaxHp = this.StartMaxHP = (ObscuredLong)99999999;
                Atk = 1;
                StartDef = Def = StartMagicDef = MagicDef = group.specialInputValue;
                Level = PCRCaculator.MainManager.Instance.PlayerSetting.playerLevel;
            }

            this.WaveHpRecovery = this.StartWaveHpRecovery = (ObscuredInt)Mathf.RoundToInt(baseData.Wave_hp_recovery);
            this.WaveEnergyRecovery = this.StartWaveEnergyRecovery = (ObscuredInt)Mathf.RoundToInt(baseData.Wave_energy_recovery);
            this.PhysicalCritical = this.StartPhysicalCritical = (ObscuredInt)Mathf.RoundToInt(baseData.Physical_critical);
            this.MagicCritical = this.StartMagicCritical = (ObscuredInt)Mathf.RoundToInt(baseData.Magic_critical);
            this.Dodge = this.StartDodge = (ObscuredInt)Mathf.RoundToInt(baseData.Dodge);
            this.Accuracy = this.StartAccuracy = (ObscuredInt)Mathf.RoundToInt(baseData.Accuracy);
            this.LifeSteal = this.StartLifeSteal = (ObscuredInt)Mathf.RoundToInt(baseData.Life_steal);
            this.PhysicalPenetrate = this.StartPhysicalPenetrate = (ObscuredInt)Mathf.RoundToInt(baseData.Physical_penetrate);
            this.MagicPenetrate = this.StartMagicPenetrate = (ObscuredInt)Mathf.RoundToInt(baseData.Magic_penetrate);
            this.EnergyRecoveryRate = this.StartEnergyRecoveryRate = (ObscuredInt)Mathf.RoundToInt(baseData.Energy_recovery_rate);
            this.HpRecoveryRate = this.StartHpRecoveryRate = (ObscuredInt)Mathf.RoundToInt(baseData.Hp_recovery_rate);
            this.EnergyReduceRate = this.StartEnergyReduceRate = (ObscuredInt)Mathf.RoundToInt(baseData.Enerey_reduce_rate);
            this.PhysicalCriticalDamageRate = this.StartPhysicalCriticalDamageRate = (ObscuredInt)100;
            this.MagicCriticalDamageRate = this.StartMagicCriticalDamageRate = (ObscuredInt)100;
            this.MoveSpeed = (float)(this.StartMoveSpeed = (ObscuredFloat)_data.MasterData.MoveSpeed);
            this.moveRate = (bool)this.IsMoveSpeedForceZero ? 0.0f : (float)this.MoveSpeedZero * (_isOther ? -1f : 1f);
            BattleLogIntreface battleLog = this.battleLog;
            UnitCtrl unitCtrl = this;
            long hp = (long)this.Hp;
            UnitCtrl LIMEKPEENOB = unitCtrl;
            battleLog.AppendBattleLog(eBattleLogType.SET_DAMAGE, 3, 0L, hp, 0, 0, LIMEKPEENOB: LIMEKPEENOB);
            if (_data.EnemyData != null && _data.EnemyData.virtual_hp != 0)
                this.skillStackValDmg = (ObscuredFloat)(1000f / (float)(long)_data.EnemyData.virtual_hp);
            else
                this.skillStackValDmg = (ObscuredFloat)(1000f / (float)(long)this.MaxHp);

            switch (UnitUtility.GetUnitPosType((float)(int)_data.MasterData.SearchAreaWidth))
            {
                case eUnitBattlePos.FRONT:
                    this.skillStackValDmg = (ObscuredFloat)((float)this.skillStackValDmg * this.battleManager.EnergyStackRatioDamagedFront);
                    this.skillStackVal = this.battleManager.EnergyGainValueSkillFront;
                    break;
                case eUnitBattlePos.MIDDLE:
                    this.skillStackValDmg = (ObscuredFloat)((float)this.skillStackValDmg * this.battleManager.EnergyStackRatioDamagedMiddle);
                    this.skillStackVal = this.battleManager.EnergyGainValueSkillMiddle;
                    break;
                case eUnitBattlePos.BACK:
                    this.skillStackValDmg = (ObscuredFloat)((float)this.skillStackValDmg * this.battleManager.EnergyStackRationDamageBack);
                    this.skillStackVal = this.battleManager.EnergyGainValueSkillBack;
                    break;
            }
            this.unitActionController = this.GetComponent<UnitActionController>();
            this.unitActionController.Initialize(this, _data);
            Spine.Slot slot = this.GetCurrentSpineCtrl().skeleton.FindSlot("Center");
            Skin skin = this.GetCurrentSpineCtrl().skeleton.Data.Skins.Items[0];
            List<string> names = new List<string>();
            int slotIndex = this.GetCurrentSpineCtrl().skeleton.FindSlotIndex("Center");
            skin.FindNamesForSlot(slotIndex, names);
            foreach (string name in names)
            {
                /*if (skin.GetAttachment(slotIndex, name) is BoundingBoxAttachment attachment)
                {
                    PolygonCollider2D polygonCollider2D = SkeletonUtility.AddBoundingBoxAsComponent(attachment, slot, this.gameObject, false);
                    this.ColliderCenter = polygonCollider2D.bounds.center - this.transform.position;
                    //this.ColliderSize = polygonCollider2D.bounds.size;
                    //this.BodyWidth = this.ColliderSize.x / this.transform.lossyScale.x + this.BossBodyWidthOffset;
                    //float coSize = MyGameCtrl.Instance.tempData.isGuildBattle? Mathf.Max(10, MyGameCtrl.Instance.tempData.SettingData.BodyColliderWidth):100;
                    float coSize = PCRCaculator.MainManager.Instance.PlayerBodyWidth;
                    if (IsBoss && MyGameCtrl.Instance.tempData.SettingData.bossBodyWidthDic.TryGetValue(UnitId, out float value))
                    {
                        coSize = value;
                    }
                    this.ColliderSize = Vector3.one * coSize * Mathf.Abs(Scale);
                    this.BodyWidth = this.ColliderSize.x / this.transform.lossyScale.x + this.BossBodyWidthOffset;
                    

                    UnityEngine.Object.Destroy((UnityEngine.Object)polygonCollider2D);
                    UnityEngine.Object.Destroy((UnityEngine.Object)this.gameObject.GetComponent<Rigidbody2D>());
                }*/
                if (skin.GetAttachment(slotIndex, name) is BoundingBoxAttachment attachment)
                {
                    PolygonCollider2D polygonCollider2D = SkeletonUtility.AddBoundingBoxAsComponent(attachment, slot, this.gameObject, false);
                    this.ColliderCenter = polygonCollider2D.bounds.center - this.transform.position;
                    this.ColliderSize = polygonCollider2D.bounds.size;
                    if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.SettingData.usePhysics)
                    {
                        float coSize2 = this.ColliderSize.x / this.transform.lossyScale.x;
                        this.BodyWidth = coSize2 + this.BossBodyWidthOffset;
                    }
                    else
                    {
                        float coSize = MyGameCtrl.Instance.tempData.isGuildBattle ? Mathf.Max(10, MyGameCtrl.Instance.tempData.SettingData.BodyColliderWidth) : 112;
                        if (IsBoss && MyGameCtrl.Instance.tempData.SettingData.bossBodyWidthDic.TryGetValue(UnitId, out float value))
                        {
                            coSize = value;
                        }
                        this.BodyWidth = coSize + this.BossBodyWidthOffset;
                    }
                    UnityEngine.Object.Destroy((UnityEngine.Object)polygonCollider2D);
                    UnityEngine.Object.Destroy((UnityEngine.Object)this.gameObject.GetComponent<Rigidbody2D>());
                }

            }
            //this.BodyWidth = 62.5f / this.transform.lossyScale.x + this.BossBodyWidthOffset;
            //if (this.IsOther && this.battleManager.GetPurpose() == eHatsuneSpecialPurpose.GET_POINT)
            //    this.EnemyPoint = this.battleManager.GetEnemyPoint(this.UnitId);
            /*if (this.IsBoss && this.battleManager.IsSpecialBattle)
            {
                int triggerHp = this.battleManager.GetTriggerHp();
                if (triggerHp != 0)
                    this.specialBattlePurposeHp = (int)((long)triggerHp * (long)this.MaxHp / 100L);
            }*/
            for (int index = 0; index < this.SortFrontDiappearAttachmentChangeDataList.Count; ++index)
            {
                AttachmentChangeData data = this.SortFrontDiappearAttachmentChangeDataList[index];
                data.TargetIndex = this.UnitSpineCtrl.skeleton.slots.FindIndex((Predicate<Spine.Slot>)(e => e.data.Name == data.TargetAttachmentName));
                data.TargetAttachment = this.UnitSpineCtrl.skeleton.GetAttachment(data.TargetIndex, data.TargetAttachmentName);
                Attachment attachment = this.UnitSpineCtrl.skeleton.GetAttachment(this.UnitSpineCtrl.skeleton.slots.FindIndex((Predicate<Spine.Slot>)(e => e.data.Name == data.AppliedAttachmentName)), data.AppliedAttachmentName);
                data.AppliedAttachment = attachment;
            }
            /*if (this.IsShadow && this.Rarity == 6 && this.battleManager.GetEnableShadowEffect())
            {
                SkillEffectCtrl component = ManagerSingleton<ResourceManager>.Instance.LoadImmediately(eResourceId.FX_SHADOW_BATTLE, ExceptNGUIRoot.Transform).GetComponent<SkillEffectCtrl>();
                component.InitializeSort();
                component.PlaySe(this.SoundUnitId, this.IsLeftDir);
                component.transform.position = this.transform.position;
                component.transform.parent = ExceptNGUIRoot.Transform;
                component.SortTarget = this;
                component.ExecAppendCoroutine(this, true);
                this.battleManager.StartCoroutine(component.TrackTarget(this.GetFirstParts(true), Vector3.zero, bone: this.CenterBone));
                this.battleManager.StartCoroutine(component.TrackTargetSort(this));
                this.RepeatEffectList.Add(component);
                this.AuraEffectList.Add(component);
            }*/
            this.princessFormProcessor = new PrincessFormProcessor();
            this.princessFormProcessor.Initialize(this, this.unitActionController, this.battleManager, this.battleTimeScale);
            if(!IsBoss && group.isSpecialBoss && group.specialBossID == 666667)
            {
                AppendCoroutine(TPRecovery(), ePauseType.SYSTEM);
            }
        }

        //public void HideLifeGauge() => this.lifeGauge.SetActiveWithCheck(false);

       /* public void ShowLifeGauge()
        {
            this.lifeGauge.SetActiveWithCheck(true);
            this.lifeGauge.SetVisibleAlways();
        }*/

        public void ExecActionOnStartAndDetermineInstanceID()
        {
            this.UnitInstanceId = this.battleManager.UnitInstanceIdCount++;
            this.unitActionController.ExecActionOnStart();
        }

        public void ExecActionOnWaveStart() => this.unitActionController.ExecActionOnWaveStart();

        public float GetDodgeRate(int _accuracy)
        {
            int num = Mathf.Max((int)this.DodgeZero - _accuracy, 0);
            return (float)num / ((float)num + 100f);
        }

        public void CreateAttackPattern(
          MasterUnitSkillData.UnitSkillData _skillData,
          int _attackPatternId)
        {
            if (this.attackPatternDictionary.ContainsKey(_attackPatternId))
                return;
            /*if(_attackPatternId!= UnitId * 100 + 1)
            {
                Debug.LogError("特殊攻击模式数据丢失！");
            }
            PCRCaculator.UnitSkillData data = PCRCaculator.MainManager.Instance.UnitRarityDic[UnitId].skillData;
            MasterUnitAttackPattern.UnitAttackPattern unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern(UnitId,data);*/
            MasterUnitAttackPattern.UnitAttackPattern unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern();
            if (PCRCaculator.MainManager.Instance.AllUnitAttackPatternDic.TryGetValue(_attackPatternId,out PCRCaculator.UnitAttackPattern data))
            {
                unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern(data);
                if(IsBoss&&group.isSpecialBoss && (group.specialBossID == 666666 || group.specialBossID == 666667))
                {
                    unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern(loop_start: 1, loop_end: 1, atk_pattern_1: 1001);
                }
                else if(PCRCaculator.MainManager.Instance.IsGuildBattle && PCRCaculator.MainManager.Instance.GuildBattleData.SettingData.changedEnemyAttackPatternDic.TryGetValue(_attackPatternId,out PCRCaculator.UnitAttackPattern data2))
                {
                    unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern(data2);
                }
            }
            else
            {
                Debug.LogError("特殊攻击模式数据丢失！");
            }
            List<int> intList1 = new List<int>();
            for (int index1 = 0; index1 < (int)unitAttackPattern.loop_start - 1; ++index1)
            {
                int pattern = unitAttackPattern.PatternList[index1];
                switch (pattern)
                {
                    case 0:
                        continue;
                    case 1:
                        intList1.Add(1);
                        this.AttackWhenSilence = true;
                        continue;
                    default:
                        switch (pattern / 1000)
                        {
                            case 1:
                                int index2 = pattern % 1000 - 1;
                                int mainSkillId = _skillData.MainSkillIds[index2];
                                int key1 = 0;
                                if (_skillData.MainSkillEvolutionIds.Count > index2)
                                    key1 = _skillData.MainSkillEvolutionIds[index2];
                                if (this.SkillLevels[mainSkillId] == 0 && (key1 == 0 || this.SkillLevels[key1] == 0))
                                {
                                    intList1.Add(1);
                                    continue;
                                }
                                if (key1 != 0 && this.SkillLevels[key1] != 0)
                                {
                                    intList1.Add(key1);
                                    continue;
                                }
                                intList1.Add(mainSkillId);
                                continue;
                            case 2:
                                int index3 = pattern % 1000 - 1;
                                int num = 0;
                                if (_skillData.SpSkillEvolutionIds.Count > index3)
                                    num = _skillData.SpSkillEvolutionIds[index3];
                                int key2 = 0;
                                if (_skillData.MainSkillEvolutionIds.Count > index3)
                                    key2 = _skillData.MainSkillEvolutionIds[index3];
                                if (num != 0 && key2 != 0 && this.SkillLevels[key2] != 0)
                                {
                                    intList1.Add(num);
                                    continue;
                                }
                                intList1.Add(_skillData.SpSkillIds[index3]);
                                continue;
                            default:
                                continue;
                        }
                }
            }
            this.attackPatternDictionary.Add(_attackPatternId, intList1);
            List<int> intList2 = new List<int>();
            if ((int)unitAttackPattern.loop_start > 0)
            {
                for (int index1 = (int)unitAttackPattern.loop_start - 1; index1 < (int)unitAttackPattern.loop_end; ++index1)
                {
                    int pattern = unitAttackPattern.PatternList[index1];
                    switch (pattern)
                    {
                        case 0:
                            goto label_45;
                        case 1:
                            intList2.Add(1);
                            this.AttackWhenSilence = true;
                            break;
                        default:
                            switch (pattern / 1000)
                            {
                                case 1:
                                    int index2 = pattern % 1000 - 1;
                                    int mainSkillId = _skillData.MainSkillIds[index2];
                                    int key1 = 0;
                                    if (_skillData.MainSkillEvolutionIds.Count > index2)
                                        key1 = _skillData.MainSkillEvolutionIds[index2];
                                    if (this.SkillLevels[mainSkillId] == 0 && (key1 == 0 || this.SkillLevels[key1] == 0))
                                    {
                                        intList2.Add(1);
                                        continue;
                                    }
                                    if (key1 != 0 && this.SkillLevels[key1] != 0)
                                    {
                                        if (index2 == 0)
                                            this.MainSkill1Evolved = true;
                                        intList2.Add(key1);
                                        continue;
                                    }
                                    intList2.Add(mainSkillId);
                                    continue;
                                case 2:
                                    int index3 = pattern % 1000 - 1;
                                    int num = 0;
                                    if (_skillData.SpSkillEvolutionIds.Count > index3)
                                        num = _skillData.SpSkillEvolutionIds[index3];
                                    int key2 = 0;
                                    if (_skillData.MainSkillEvolutionIds.Count > index3)
                                        key2 = _skillData.MainSkillEvolutionIds[index3];
                                    if (num != 0 && key2 != 0 && this.SkillLevels[key2] != 0)
                                    {
                                        intList2.Add(num);
                                        continue;
                                    }
                                    intList2.Add(_skillData.SpSkillIds[index3]);
                                    continue;
                                default:
                                    continue;
                            }
                    }
                }
            }
        label_45:
            this.attackPatternLoopDictionary.Add(_attackPatternId, intList2);
        }

        public void BattleStartProcess(eUnitRespawnPos respawnPos)
        {
            this.RespawnPos = respawnPos;
            this.AppendCoroutine(this.updateAttackTarget(), ePauseType.SYSTEM, this);
            this.IsDepthBack = this.IsGameStartDepthBack;
            if (this.battleManager.BlackOutUnitList.Contains(this))
                this.SetSortOrderFront();
            else
                this.SetSortOrderBack();
        }

        public void WaveStartProcess(bool _first)
        {
            this.ApplyPassiveSkillValue(_first);
            this.resetActionPatternAndCastTime();
            if (this.IsOther)
                this.resetPosForEnemyUnit(this.RespawnPos);
            else
                this.ResetPosForUserUnit(BattleDefine.UnitRespawnPosList.IndexOf(this.RespawnPos));
            //this.CreateRunSmoke();
            this.ExecActionOnWaveStart();
        }

        public void ActivateInternalUnit()
        {
            if (this.IsDead || this.gameObject.activeSelf)
                return;
            this.gameObject.SetActive(true);
            this.MoveToNext();
        }

        private void resetActionPatternAndCastTime()
        {
            //this.currentActionPatternId = UnitUtility.GetDefaultActionPatternId(this.UnitId);
            this.attackPatternIndex = 0;
            this.attackPatternIsLoop = this.attackPatternDictionary[this.currentActionPatternId].Count == 0;
            switch (this.battleManager.BattleCategory)
            {
                case eBattleCategory.STORY:
                    this.m_fCastTimer = (ObscuredFloat)90f;
                    break;
                case eBattleCategory.TUTORIAL:
                    this.m_fCastTimer = (ObscuredFloat)0.3f;
                    break;
                default:
                    this.m_fCastTimer = (ObscuredFloat)(this.battleManager.CurrentWave != 0 ? 0.3f : 2.5f);
                    break;
            }
        }

        public void ResetPosForUserUnit(int index)
        {
            Vector2 localPosition = (Vector2)this.transform.localPosition;
            localPosition.x = -560f;
            if (this.battleManager.BattleCategory == eBattleCategory.TUTORIAL && this.battleManager.CurrentWave == 0)
            {
                //this.IdleOnly = true;
                //if (this.battleManager.BattleCategory == eBattleCategory.TUTORIAL)
                //    localPosition.x = TutorialDefine.UNIT_DEFAULT_POS[this.SoundUnitId];
            }
            else
                localPosition.x -= 200f * (float)(index + 1);
            if (this.battleManager.IsBossBattle)
                localPosition.x -= (float)((double)this.MoveSpeed * (double)this.battleManager.GetBossUnit().UnitAppearDelay * 1.6);
            this.transform.localPosition = (Vector3)localPosition;
        }

        private void resetPosForEnemyUnit(eUnitRespawnPos pos)
        {
            int num = BattleDefine.UnitRespawnPosList.IndexOf(pos);
            eBattleCategory jiliicmhlch = this.battleManager.BattleCategory;
            Vector3 vector3 = new Vector3(560f, this.battleManager.GetRespawnPos(pos), 0.0f);
            if (this.IsBoss)
            {
                vector3.y = this.battleManager.GetRespawnPos(eUnitRespawnPos.MAIN_POS_5);
                vector3.x += this.BossDeltaX * 540f;
                vector3.y += this.BossDeltaY * 540f;
                vector3.x -= -34f;
            }
            else if (jiliicmhlch == eBattleCategory.TUTORIAL && this.battleManager.CurrentWave == 0)
            {
                //vector3.x = TutorialDefine.HIKARITAKE_POS;
            }
            else
            {
                vector3.x += 200f * (float)num;
                if (this.battleManager.IsBossBattle)
                {
                    /*vector3.x += (float)((double)this.MoveSpeed * (double)this.battleManager.GetBossUnit().UnitAppearDelay * 1.60000002384186);
                    bool flag = true;
                    eHatsuneSpecialPurpose purpose = this.battleManager.GetPurpose();
                    if ((purpose == eHatsuneSpecialPurpose.DEFEAT_OTHERS ? 1 : (purpose == eHatsuneSpecialPurpose.GET_POINT ? 1 : 0)) != 0)
                    {
                        int initialPosition = this.battleManager.GetInitialPosition(this.UnitId);
                        vector3.x = 100f + (float)initialPosition;
                        if ((int)HatsuneUtility.GetHatsuneSpecialBattle().start_idle_trigger == 0)
                        {
                            this.SetState(UnitCtrl.ActionState.GAME_START);
                        }
                        else
                        {
                            this.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, 0.0f);
                            float moveSpeed = this.MoveSpeed;
                            this.MoveSpeed = 0.0f;
                            this.SetState(UnitCtrl.ActionState.IDLE);
                            this.AppendCoroutine(this.waitShadowAppear(moveSpeed), ePauseType.SYSTEM);
                        }
                        flag = false;
                    }
                    if (this.IsShadow & flag)
                    {
                        vector3.x = 100f + this.SearchAreaSize;
                        this.AppendCoroutine(this.waitBossMotionEnd(), ePauseType.SYSTEM);
                        this.IsMoveSpeedForceZero = (ObscuredBool)true;
                    }*/
                }
            }
            this.transform.localPosition = vector3;
        }

        /*private IEnumerator waitShadowAppear(float _oldMoveSpeed)
        {
            UnitCtrl _unit = this;
            float time = 0.0f;
            float appearTime = (float)(double)HatsuneUtility.GetHatsuneSpecialBattle().appear_time;
            while ((double)time < (double)appearTime)
            {
                time += _unit.DeltaTimeForPause;
                yield return (object)null;
            }
            float actionTime = (float)(double)HatsuneUtility.GetHatsuneSpecialBattle().action_start_second;
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Singleton<BattleUnitLoader>.Instance.LoadEffectImmidatly(eResourceId.FX_SP_SHADOW_SPAWN, (long)(int)HatsuneUtility.GetHatsuneSpecialBattle().start_idle_trigger) as GameObject);
            gameObject.transform.SetParent(ExceptNGUIRoot.Transform);
            SkillEffectCtrl component = gameObject.GetComponent<SkillEffectCtrl>();
            component.InitializeSort();
            component.SortTarget = _unit;
            component.SetSortOrderBack();
            component.ExecAppendCoroutine(_unit);
            _unit.StartCoroutine(component.TrackTarget(_unit.GetFirstParts(), Vector3.zero));
            component.PlaySe(_unit.SoundUnitId, _unit.IsLeftDir);
            while ((double)time < (double)appearTime + 0.100000001490116)
            {
                time += _unit.DeltaTimeForPause;
                yield return (object)null;
            }
            _unit.SetEnableColor();
            while ((double)time < (double)actionTime)
            {
                time += _unit.DeltaTimeForPause;
                yield return (object)null;
            }
            _unit.MoveSpeed = _oldMoveSpeed;
        }*/

        public void SetInitialOrder(int _order)
        {
            /*foreach (KeyValuePair<int, MasterHatsuneSpecialEnemy.HatsuneSpecialEnemy> keyValuePair in ManagerSingleton<MasterDataManager>.Instance.masterHatsuneSpecialEnemy.dictionary)
            {
                EHPLBCOOOPK.HGIEGEENPJO debfkndecin = Singleton<EHPLBCOOOPK>.Instance.DEBFKNDECIN;
                if ((int)keyValuePair.Value.event_id == debfkndecin.BMNOHBHNPEG && (int)keyValuePair.Value.mode == debfkndecin.FEEJMOIPPJI && (int)keyValuePair.Value.order == _order)
                {
                    Vector3 localPosition = this.transform.localPosition;
                    localPosition.x = (float)(int)keyValuePair.Value.initial_position;
                    this.transform.localPosition = localPosition;
                    break;
                }
            }*/
        }

        private IEnumerator waitBossMotionEnd()
        {
            while (!this.battleManager.GetBossUnit().GameStartDone)
                yield return (object)null;
            this.IsMoveSpeedForceZero = (ObscuredBool)false;
            this.SetLeftDirection(this.IsOther);
            this.SetState(UnitCtrl.ActionState.IDLE);
        }

        public void SetOverlapPos(float overlapPosX)
        {
            Vector2 localPosition1 = (Vector2)this.BottomTransform.transform.localPosition;
            localPosition1.x += overlapPosX;
            this.BottomTransform.transform.localPosition = (Vector3)localPosition1;
            this.OverlapPosX = overlapPosX;
            //Vector2 localPosition2 = (Vector2)this.lifeGauge.transform.localPosition;
           // localPosition2.x += overlapPosX;
            //this.lifeGauge.transform.localPosition = (Vector3)localPosition2;
        }

        public void UpdateSkillTarget()
        {
            this.skillTargetList.Clear();
            List<UnitCtrl> unitCtrlList = this.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList;
            for (int index = 0; index < unitCtrlList.Count; ++index)
            {
                if (this.judgeFrontAreaTarget(unitCtrlList[index], this.SkillAreaWidthList[this.UnionBurstSkillId]))
                    this.skillTargetList.Add(unitCtrlList[index]);
            }
        }

        private bool judgeFrontAreaTarget(UnitCtrl _target, float _distance)
        {
            if (_target.IsPartsBoss)
                return this.judgeFrontAreaTargetForBossParts(_target, _distance);
            float x = this.transform.parent.lossyScale.x;
            float _a = (float)((double)_target.transform.position.x / (double)x - (double)this.transform.position.x / (double)x);
            float num = _target.BodyWidth + this.BodyWidth;
            float _b1 = (float)((this.IsLeftDir ? -(double)_distance : 0.0) - (double)num * 0.5);
            float _b2 = (float)((this.IsLeftDir ? 0.0 : (double)_distance) + (double)num * 0.5);
            return ((double)_a >= (double)_b1 && (double)_a <= (double)_b2 || (BattleUtil.Approximately(_a, _b1) || BattleUtil.Approximately(_a, _b2))) && (!_target.IsDead && (long)_target.Hp > 0L || _target.HasUnDeadTime) && (!_target.IsPhantom && !_target.IsStealth);
        }

        private bool judgeFrontAreaTargetForBossParts(UnitCtrl _target, float _distance)
        {
            for (int index = 0; index < _target.BossPartsListForBattle.Count; ++index)
            {
                PartsData partsData = _target.BossPartsListForBattle[index];
                float _a = _target.transform.localPosition.x + partsData.PositionX - this.transform.localPosition.x;
                float bodyWidthValue = partsData.BodyWidthValue;
                if (partsData.GetTargetable())
                {
                    float num = bodyWidthValue + this.BodyWidth;
                    float _b1 = (float)((this.IsLeftDir ? -(double)_distance : 0.0) - (double)num * 0.5);
                    float _b2 = (float)((this.IsLeftDir ? 0.0 : (double)_distance) + (double)num * 0.5);
                    if (((double)_a >= (double)_b1 && (double)_a <= (double)_b2 || (BattleUtil.Approximately(_a, _b1) || BattleUtil.Approximately(_a, _b2))) && (!_target.IsDead && (long)_target.Hp > 0L || _target.HasUnDeadTime) && !_target.IsPhantom)
                        return true;
                }
            }
            return false;
        }

        private IEnumerator updateAttackTarget()
        {
            while (true)
            {
                this.updateAttackTargetImpl();
                yield return (object)null;
            }
        }

        private void updateAttackTargetImpl()
        {
            List<UnitCtrl> unitCtrlList1 = this.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList;
            float _distance = this.SearchAreaSize;
            if (!this.attackPatternIsLoop && this.attackPatternIndex == 0 && (this.attackPatternDictionary != null && this.attackPatternDictionary[this.currentActionPatternId].Count != 0))
            {
                int key = this.attackPatternDictionary[this.currentActionPatternId][this.attackPatternIndex];
                switch (key)
                {
                    case 0:
                    case 1:
                        break;
                    default:
                        _distance = this.SkillAreaWidthList[key];
                        break;
                }
            }
            for (int index = 0; index < unitCtrlList1.Count; ++index)
            {
                UnitCtrl _target = unitCtrlList1[index];
                if (this.judgeFrontAreaTarget(_target, _distance))
                {
                    if (!this.TargetEnemyList.Contains(_target))
                        this.TargetEnemyList.Add(_target);
                }
                else
                    this.TargetEnemyList.Remove(_target);
            }
            for (int index = this.TargetEnemyList.Count - 1; index >= 0; --index)
            {
                UnitCtrl targetEnemy = this.TargetEnemyList[index];
                if (!unitCtrlList1.Contains(targetEnemy))
                    this.TargetEnemyList.Remove(targetEnemy);
            }
            List<UnitCtrl> unitCtrlList2 = this.IsOther ? this.battleManager.EnemyList : this.battleManager.UnitList;
            for (int index = 0; index < unitCtrlList2.Count; ++index)
            {
                UnitCtrl _target = unitCtrlList2[index];
                if (!((UnityEngine.Object)_target == (UnityEngine.Object)null))
                {
                    if (this.judgeFrontAreaTarget(_target, _distance))
                    {
                        if (!this.targetPlayerList.Contains(_target))
                            this.targetPlayerList.Add(_target);
                    }
                    else
                        this.targetPlayerList.Remove(_target);
                }
            }
            for (int index = this.targetPlayerList.Count - 1; index >= 0; --index)
            {
                UnitCtrl targetPlayer = this.targetPlayerList[index];
                if (!unitCtrlList2.Contains(targetPlayer))
                    this.targetPlayerList.Remove(targetPlayer);
            }
        }

        public void _Update()
        {
            if (this.TimeToDie)
            {
                this.DestroyAndCoroutineRemove();
            }
            /*else
            {
                if ((double)this.sortOffsetResetTimer <= 0.0)
                    return;
                this.sortOffsetResetTimer -= this.battleManager.DeltaTime_60fps;
                if ((double)this.sortOffsetResetTimer > 0.0)
                    return;
                this.hitEffectSortOffset = 0;
            }*/
        }

        /*public void BattleRecovery(eBattleCategory _category)
        {
            if (this.IsDead)
                return;
            float num = 1f;
            float _recoveryRate = 1f;
            switch (_category)
            {
                case eBattleCategory.DUNGEON:
                    MasterDungeonAreaData.DungeonAreaData dungeonAreaData = ManagerSingleton<MasterDataManager>.Instance.masterDungeonAreaData.Get(Singleton<DungeonTempData>.Instance.EnterAreaId);
                    num = (float)(int)dungeonAreaData.recovery_hp_rate / 100f;
                    _recoveryRate = (float)(int)dungeonAreaData.recovery_tp_rate / 100f;
                    break;
                case eBattleCategory.TOWER:
                    MasterTowerQuestData.TowerQuestData towerQuestData = ManagerSingleton<MasterDataManager>.Instance.masterTowerQuestData.Get(this.CreateSingletonTree<UnitCtrl>().Get<TowerTempData>().CurrentQuestId);
                    num = (float)(int)towerQuestData.recovery_hp_rate / 100f;
                    _recoveryRate = (float)(int)towerQuestData.recovery_tp_rate / 100f;
                    break;
                case eBattleCategory.TOWER_CLOISTER:
                    MasterTowerCloisterQuestData.TowerCloisterQuestData cloisterQuestData1 = ManagerSingleton<MasterDataManager>.Instance.masterTowerCloisterQuestData.Get(this.CreateSingletonTree<UnitCtrl>().Get<TowerTempData>().CurrentCloisterQuestId);
                    num = (float)(int)cloisterQuestData1.recovery_hp_rate / 100f;
                    _recoveryRate = (float)(int)cloisterQuestData1.recovery_tp_rate / 100f;
                    break;
                case eBattleCategory.TOWER_CLOISTER_REPLAY:
                    MasterTowerCloisterQuestData.TowerCloisterQuestData cloisterQuestData2 = ManagerSingleton<MasterDataManager>.Instance.masterTowerCloisterQuestData.Get(this.CreateSingletonTree<UnitCtrl>().Get<ReplayTempData>().CurrentCloisterQuestId);
                    num = (float)(int)cloisterQuestData2.recovery_hp_rate / 100f;
                    _recoveryRate = (float)(int)cloisterQuestData2.recovery_tp_rate / 100f;
                    break;
            }
            bool flag = (long)this.Hp < (long)this.MaxHp && (int)this.WaveHpRecovery > 0;
            if (flag)
                this.SetRecovery((int)((double)(int)this.WaveHpRecoveryZero * (double)num), UnitCtrl.eInhibitHealType.NO_EFFECT, this);
            if ((double)this.Energy >= (double)UnitDefine.MAX_ENERGY || (int)this.WaveEnergyRecovery <= 0)
                return;
            if (flag)
                this.StartCoroutine(this.waitCargeEnergy(_recoveryRate));
            else
                this.ChargeEnergy(eSetEnergyType.BATTLE_RECOVERY, (float)(int)this.WaveEnergyRecoveryZero * _recoveryRate, true);
        }*/

        public IEnumerator WaitBattleRecovery()
        {
            UnitCtrl _source = this;
            if (_source.IsDead)
            {
                --_source.battleManager.KPLMNGFMBKF;
            }
            else
            {
                bool flag = (long)_source.Hp < (long)_source.MaxHp && (int)_source.WaveHpRecovery > 0;
                if (flag)
                    _source.SetRecovery((int)_source.WaveHpRecoveryZero, UnitCtrl.eInhibitHealType.NO_EFFECT, _source);
                if ((double)_source.Energy < (double)UnitDefine.MAX_ENERGY && (int)_source.WaveEnergyRecovery > 0)
                {
                    if (flag)
                        yield return (object)new WaitForSeconds(0.45f);
                    _source.ChargeEnergy(eSetEnergyType.BATTLE_RECOVERY, (float)(int)_source.WaveEnergyRecoveryZero, true);
                }
                --_source.battleManager.KPLMNGFMBKF;
            }
        }

        public void MoveToNext()
        {
            if (this.IsDead)
                return;
            /**if (!UnitUtility.JudgeIsSummon(this.UnitId))
            {
                this.IdleOnly = false;
                this.GetCurrentSpineCtrl().CurColor = Color.white;
            }*/
            this.ModeChangeEnd = false;
            this.SetState(UnitCtrl.ActionState.WALK);
            this.SetLeftDirection(false);
            //this.CreateRunSmoke();
        }

        private IEnumerator waitCargeEnergy(float _recoveryRate)
        {
            yield return (object)new WaitForSeconds(0.45f);
            this.ChargeEnergy(eSetEnergyType.BATTLE_RECOVERY, (float)(int)this.WaveEnergyRecoveryZero * _recoveryRate, true);
        }

        public void SetLeftDirection(bool bLeftDir)
        {
            this.IsLeftDir = bLeftDir;
            if (this.ToadDatas.Count > 0)
                this.GetCurrentSpineCtrl().transform.localScale = this.IsLeftDir || this.IsForceLeftDir ? this.ToadDatas[0].LeftDirScale : this.ToadDatas[0].RightDirScale;
            else
                this.GetCurrentSpineCtrl().transform.localScale = (Vector3)(this.IsLeftDir || this.IsForceLeftDirOrPartsBoss ? this.leftDirScale : this.rightDirScale);
            this.moveRate = (bool)this.IsMoveSpeedForceZero ? 0.0f : (float)this.MoveSpeedZero * (this.IsLeftDir ? -1f : 1f);
            foreach (KeyValuePair<UnitCtrl.eAbnormalStateCategory, AbnormalStateCategoryData> stateCategoryData in this.abnormalStateCategoryDataDictionary)
            {
                bool flag = bLeftDir || this.IsForceLeftDirOrPartsBoss;
                for (int index = 0; index < stateCategoryData.Value.Effects.Count; ++index)
                {
                    AbnormalStateEffectGameObject effect = stateCategoryData.Value.Effects[index];
                    if ((UnityEngine.Object)effect.LeftEffect != (UnityEngine.Object)null)
                    {
                        effect.LeftEffect.SetActive(flag && this.isAbnormalEffectEnable);
                        if ((UnityEngine.Object)effect.RightEffect != (UnityEngine.Object)null)
                            effect.RightEffect.SetActive(!flag && this.isAbnormalEffectEnable);
                    }
                }
            }
        }

        public void SetDirectionAuto() => this.SetLeftDirection(this.isNearestEnemyLeft());

        private bool isNearestEnemyLeft()
        {
            List<UnitCtrl> unitCtrlList = !this.IsConfusionOrConvert() ? (this.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList) : (!this.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList);
            float f = (float)((double)BattleDefine.BATTLE_FIELD_SIZE * 2.0 * (!this.IsOther ? -1.0 : 1.0));
            for (int index1 = 0; index1 < unitCtrlList.Count; ++index1)
            {
                UnitCtrl unitCtrl = unitCtrlList[index1];
                if (!unitCtrl.IsPhantom && !unitCtrl.IsDead && (!((UnityEngine.Object)unitCtrl == (UnityEngine.Object)this) && (long)unitCtrl.Hp != 0L) && !unitCtrl.IsStealth)
                {
                    if (!unitCtrl.IsPartsBoss)
                    {
                        if ((double)Mathf.Abs(f) > (double)Mathf.Abs(this.transform.localPosition.x - unitCtrl.transform.localPosition.x) || BattleUtil.Approximately(Mathf.Abs(f), Mathf.Abs(this.transform.localPosition.x - unitCtrl.transform.localPosition.x)))
                            f = this.transform.localPosition.x - unitCtrl.transform.localPosition.x;
                    }
                    else
                    {
                        for (int index2 = 0; index2 < unitCtrl.BossPartsListForBattle.Count; ++index2)
                        {
                            PartsData partsData = unitCtrl.BossPartsListForBattle[index2];
                            if ((double)Mathf.Abs(f) > (double)Mathf.Abs(this.transform.localPosition.x - unitCtrl.transform.localPosition.x - partsData.PositionX) || BattleUtil.Approximately(Mathf.Abs(f), Mathf.Abs(this.transform.localPosition.x - unitCtrl.transform.localPosition.x - partsData.PositionX)))
                                f = this.transform.localPosition.x - unitCtrl.transform.localPosition.x - partsData.PositionX;
                        }
                    }
                }
            }
            return (double)f > 0.0;
        }

        public bool Pause
        {
            get => this.m_bPause;
            set
            {
                this.m_bPause = value;
                if (this.m_bPause)
                    this.setMotionPause();
                else if (!this.IsUnableActionState() || this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.INVALID)
                    this.setMotionResume();
                //this.PauseSound(this.m_bPause);
                this.princessFormProcessor.Pause(this.m_bPause);
            }
        }

        private void setMotionResume()
        {
            if (!((UnityEngine.Object)this.GetCurrentSpineCtrl() != (UnityEngine.Object)null))
                return;
            this.GetCurrentSpineCtrl().Resume();
        }

        private void setMotionPause()
        {
            if ((UnityEngine.Object)this.battleManager == (UnityEngine.Object)null || this.battleManager.BlackoutUnitTargetList == null || (this.battleManager.BlackoutUnitTargetList.Contains(this) || !((UnityEngine.Object)this.GetCurrentSpineCtrl() != (UnityEngine.Object)null)))
                return;
            this.GetCurrentSpineCtrl().Pause();
        }

        public void ChangeAttackPattern(int _attackPatternId, int _spSkillLevel, float _limitTime = -1f)
        {
            int currentActionPatternId = this.currentActionPatternId;
            this.currentActionPatternId = _attackPatternId;
            this.attackPatternIndex = 0;
            this.attackPatternIsLoop = this.attackPatternDictionary[_attackPatternId].Count == 0;
            for (int index = 0; index < this.unitActionController.SpecialSkillList.Count; ++index)
                this.unitActionController.SpecialSkillList[index].SetLevel(_spSkillLevel);
            foreach (Skill specialSkillEvolution in this.unitActionController.SpecialSkillEvolutionList)
                specialSkillEvolution.SetLevel(_spSkillLevel);
            if ((double)_limitTime <= 0.0)
                return;
            this.AppendCoroutine(this.updateChangeActionPattern(currentActionPatternId, _limitTime), ePauseType.SYSTEM, this);
        }

        private IEnumerator updateChangeActionPattern(int oldIndex, float limitTime)
        {
            float time = 0.0f;
            while (true)
            {
                time += this.DeltaTimeForPause;
                if ((double)time <= (double)limitTime)
                    yield return (object)null;
                else
                    break;
            }
            this.currentActionPatternId = oldIndex;
            this.attackPatternIndex = 0;
        }

        public void ChangeChargeSkill(int skillNum, float limitTime)
        {
            this.isAwakeMotion = true;
            int unionBurstSkillId = this.UnionBurstSkillId;
            this.UnionBurstSkillId = skillNum;
            this.AppendCoroutine(this.updateChangeSkillNum(unionBurstSkillId, limitTime), ePauseType.SYSTEM, this);
        }

        private IEnumerator updateChangeSkillNum(int oldChargeSkillNum, float limitTime)
        {
            float time = 0.0f;
            while (true)
            {
                time += this.DeltaTimeForPause;
                if ((double)time > (double)limitTime)
                    this.UnionBurstSkillId = oldChargeSkillNum;
                yield return (object)null;
            }
        }

        public IEnumerator UpdateSummon(
          int _skillNum,
          eUnitRespawnPos _respawnPos,
          SummonAction.eMoveType _moveType,
          Vector3 _targetPosition,
          float _moveSpeed)
        {
            UnitCtrl unitCtrl = this;
            while (true)
            {
                if (!battleManager.CoroutineManager.VisualPause && GetCurrentSpineCtrl().state.TimeScale == 0f)
                {
                    GetCurrentSpineCtrl().Resume();
                }
                if (IsUnableActionState() || !GetCurrentSpineCtrl().IsPlayAnimeBattle)
                {
                    break;
                }
                yield return null;
            }
            Vector3 v;
            switch (_moveType)
            {
                case SummonAction.eMoveType.NORMAL:
                    unitCtrl.BattleStartProcess(_respawnPos);
                    unitCtrl.transform.SetLocalPosY(unitCtrl.battleManager.GetRespawnPos(_respawnPos));
                    unitCtrl.SetState(UnitCtrl.ActionState.WALK);
                    using (List<SkillEffectCtrl>.Enumerator enumerator = unitCtrl.RepeatEffectList.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        { }   //enumerator.Current.SetSortOrderBack();
                        break;
                    }
                case SummonAction.eMoveType.LINEAR:
                    if (unitCtrl.GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.SUMMON, _skillNum, 1))
                        unitCtrl.PlayAnime(eSpineCharacterAnimeId.SUMMON, _skillNum, 1);
                    _targetPosition.y = unitCtrl.battleManager.GetRespawnPos(_respawnPos);
                    v = (_targetPosition - unitCtrl.transform.localPosition).normalized * _moveSpeed;
                    unitCtrl.SetLeftDirection((double)v.x < 0.0);
                    float duration = (_targetPosition - unitCtrl.transform.localPosition).x / v.x;
                    float time = 0.0f;
                    while (true)
                    {
                        time += unitCtrl.battleManager.DeltaTime_60fps;
                        unitCtrl.transform.localPosition += v * unitCtrl.battleManager.DeltaTime_60fps;
                        if ((double)time <= (double)duration)
                            yield return (object)null;
                        else
                            break;
                    }
                    if (unitCtrl.GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.SUMMON, _skillNum, 2))
                        unitCtrl.PlayAnime(eSpineCharacterAnimeId.SUMMON, _skillNum, 2, _isLoop: false);
                    while (true)
                    {
                        if (!unitCtrl.battleManager.CoroutineManager.VisualPause && (double)unitCtrl.GetCurrentSpineCtrl().state.TimeScale == 0.0)
                            unitCtrl.GetCurrentSpineCtrl().Resume();
                        if (unitCtrl.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                            yield return (object)null;
                        else
                            break;
                    }
                    unitCtrl.BattleStartProcess(_respawnPos);
                    unitCtrl.transform.localPosition = _targetPosition;
                    unitCtrl.SetState(UnitCtrl.ActionState.WALK);
                    yield break;
            }
            v = new Vector3();
        }

        public void PlayAnime(
          eSpineCharacterAnimeId _animeId,
          int _index1 = -1,
          int _index2 = -1,
          int _index3 = -1,
          bool _isLoop = true,
          BattleSpineController _targetCtr = null,
          bool _quiet = false,
          float _startTime = 0.0f,
          bool _ignoreBlackout = false)
        {
            BattleSpineController controller = _targetCtr;
            if ((UnityEngine.Object)controller == (UnityEngine.Object)null)
                controller = this.GetCurrentSpineCtrl();
            controller.PlayAnime(_animeId, _index1, _index2, _index3, _isLoop, _startTime, _ignoreBlackout);
            controller.state.GetCurrent(0).lastTime = _startTime;
            controller.state.GetCurrent(0).time = _startTime;
            //controller.state.GetCurrent(0).animationLast = _startTime;
            //controller.state.GetCurrent(0).animationStart = _startTime;

            controller.state.Apply(controller.skeleton);
            if (_quiet)
                return;
            //this.playSeWithMotion(controller, _animeId, _index1, _index2, _index3, _isLoop, _startTime);
        }

        public void PlayJoyResult()
        {
           /* int num = UnitUtility.IsChangeMotion((int)this.UnitParameter.UniqueData.Id, this.IsOther) ? 1 : 0;
            int setSkinNo = UnitUtility.GetSetSkinNo(this.UnitParameter.UniqueData, UnitDefine.eSkinType.Motion, this.IsOther);
            if (num != 0 && setSkinNo != 1 && this.GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.JOY_RESULT, this.MotionPrefix))
                this.PlayAnime(eSpineCharacterAnimeId.JOY_RESULT, this.MotionPrefix, _isLoop: false);
            else
                this.PlayAnime(this.MotionPrefix == 1 ? eSpineCharacterAnimeId.JOY_RESULT : eSpineCharacterAnimeId.JOY_LONG, this.MotionPrefix, _isLoop: false);*/
        }

        public void RestartPlayAnimeCoroutine(
          float _startTime,
          eSpineCharacterAnimeId _animeId,
          int _index,
          int _prefix) => this.GetCurrentSpineCtrl().RestartPlayAnimeCoroutine(_startTime, _animeId, _index, _prefix);

        public void PlayAnimeNoOverlap(
          eSpineCharacterAnimeId _animeId,
          int _index1 = -1,
          int _index2 = -1,
          int _index3 = -1,
          bool _isLoop = false,
          BattleSpineController _targetCtr = null)
        {
            BattleSpineController controller = _targetCtr;
            if ((UnityEngine.Object)controller == (UnityEngine.Object)null)
                controller = this.GetCurrentSpineCtrl();
            controller.PlayAnimeNoOverlap(_animeId, _index1, _index2, _index3, _isLoop);
            //this.playSeWithMotion(controller, _animeId, _index1, _index2, _index3, _isLoop);
        }

        public void AppendCoroutine(IEnumerator _cr, ePauseType _pauseType, UnitCtrl _unit = null) => this.battleManager.AppendCoroutine(_cr, _pauseType, _unit);

        public void SetCurrentHp(long _hp)
        {
            this.Hp = (ObscuredLong)_hp;
            if (_hp <= 0L)
            {
                //this.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, 0.0f);
                this.OnDeadForRevival = (UnitCtrl.OnDeadDelegate)null;
                this.SetState(UnitCtrl.ActionState.DIE);
                this.StandByDone = true;
                this.isDeadBySetCurrentHp = true;
            }
            if ((double)this.StartHpPercent != 0.0)
                return;
            this.StartHpPercent = (float)_hp / (float)(long)this.MaxHp;
        }

        public void SetCurrentHpZero() => this.Hp = (ObscuredLong)0L;

        public void SetMaxHp(long _maxHp) => this.MaxHp = (ObscuredLong)_maxHp;

        /*public void SetCurrentHpForTowerTimeUp(int _hp)
        {
            this.Hp = (ObscuredLong)(long)_hp;
            if (!((UnityEngine.Object)this.lifeGauge != (UnityEngine.Object)null))
                return;
            float NormalizedHP = (float)(long)this.Hp / (float)(long)this.MaxHp;
            this.lifeGauge.SetNormalizedLifeAmount(NormalizedHP, _isTowerTimeUp: true);
            this.OnLifeAmmountChange.Call<float>(NormalizedHP);
            this.OnDamageForUIShake.Call();
            this.StartCoroutine(this.setColorOffsetDefaultWithDelay());
        }*/

        public void SetEnergy(float energy, eSetEnergyType type, UnitCtrl source = null)
        {
            BattleLogIntreface battleLog = this.battleLog;
            UnitCtrl unitCtrl1 = source;
            UnitCtrl unitCtrl2 = this;
            int HLIKLPNIOKJ = (int)type;
            long KDCBJHCMAOH = (long)(int)energy;
            UnitCtrl JELADBAMFKH = unitCtrl1;
            UnitCtrl LIMEKPEENOB = unitCtrl2;
            battleLog.AppendBattleLog(eBattleLogType.SET_ENERGY, HLIKLPNIOKJ, 0L, KDCBJHCMAOH, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
            this.Energy = energy;
            if(unitUI!= null)
                unitUI.SetTP((float)Energy / UnitDefine.MAX_ENERGY);
            MyOnTPChanged?.Invoke(UnitId,(float)Energy / UnitDefine.MAX_ENERGY, BattleHeaderController.CurrentFrameCount,type.GetDescription());
            if(uIManager!=null)
            uIManager.LogMessage("TP变更为：" + energy, PCRCaculator.Battle.eLogMessageType.CHANGE_TP, this);
        }

        public void IndicateSkillName(string _skillName) 
        {
            if(_skillName == "" || _skillName == null) { return; }
            UIManager.ShowSkillName(_skillName, gameObject.transform);
        }// => this.lifeGauge.IndicateSkillName(_skillName);

        //public List<PrefabWithTime> GetDieEffects() => this.dieEffects;

        //public List<PrefabWithTime> GetAuraEffects() => this.auraEffects;

        private int currentHpRegeneId { get; set; }

        private int currentTpRegeneId { get; set; }

        private bool isContinueIdleForPauseAction { get; set; }

        public UnitCtrl.eSpecialSleepStatus specialSleepStatus { get; set; } = UnitCtrl.eSpecialSleepStatus.INVALID;

        private bool isAbnormalEffectEnable { get; set; } = true;

        public bool AbnormalIconVisible { get; set; }

        public bool AttackWhenSilence { get; set; }

        public List<SkillEffectCtrl> ModeChangeEndEffectList { get; set; } = new List<SkillEffectCtrl>();

        public System.Action<UnitCtrl, eStateIconType, bool> OnChangeState { get; set; }
        

        public System.Action<UnitCtrl, eStateIconType, int> OnChangeStateNum { get; set; }

        public static UnitCtrl.eAbnormalStateCategory GetAbnormalStateCategory(
          UnitCtrl.eAbnormalState abnormalState)
        {
            UnitCtrl.eAbnormalStateCategory abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.NONE;
            switch (abnormalState)
            {
                case UnitCtrl.eAbnormalState.GUARD_ATK:
                case UnitCtrl.eAbnormalState.DRAIN_ATK:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK;
                    break;
                case UnitCtrl.eAbnormalState.GUARD_MGC:
                case UnitCtrl.eAbnormalState.DRAIN_MGC:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK;
                    break;
                case UnitCtrl.eAbnormalState.GUARD_BOTH:
                case UnitCtrl.eAbnormalState.DRAIN_BOTH:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH;
                    break;
                case UnitCtrl.eAbnormalState.HASTE:
                case UnitCtrl.eAbnormalState.SLOW:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.SPEED;
                    break;
                case UnitCtrl.eAbnormalState.POISON:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.POISON;
                    break;
                case UnitCtrl.eAbnormalState.BURN:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.BURN;
                    break;
                case UnitCtrl.eAbnormalState.CURSE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.CURSE;
                    break;
                case UnitCtrl.eAbnormalState.PARALYSIS:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.PARALYSIS;
                    break;
                case UnitCtrl.eAbnormalState.FREEZE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.FREEZE;
                    break;
                case UnitCtrl.eAbnormalState.CONVERT:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.CONVERT;
                    break;
                case UnitCtrl.eAbnormalState.PHYSICS_DARK:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.PHYSICAL_DARK;
                    break;
                case UnitCtrl.eAbnormalState.SILENCE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.SILENCE;
                    break;
                case UnitCtrl.eAbnormalState.CHAINED:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.CHAINED;
                    break;
                case UnitCtrl.eAbnormalState.SLEEP:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.SLEEP;
                    break;
                case UnitCtrl.eAbnormalState.STUN:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.STUN;
                    break;
                case UnitCtrl.eAbnormalState.DETAIN:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.DETAIN;
                    break;
                case UnitCtrl.eAbnormalState.NO_EFFECT_SLIP_DAMAGE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.NO_EFFECT_SLIP_DAMAGE;
                    break;
                case UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.NO_DAMAGE;
                    break;
                case UnitCtrl.eAbnormalState.NO_ABNORMAL:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.NO_ABNORMAL;
                    break;
                case UnitCtrl.eAbnormalState.NO_DEBUF:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.NO_DEBUF;
                    break;
                case UnitCtrl.eAbnormalState.ACCUMULATIVE_DAMAGE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.ACCUMULATIVE_DAMAGE;
                    break;
                case UnitCtrl.eAbnormalState.DECOY:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.DECOY;
                    break;
                case UnitCtrl.eAbnormalState.MIFUYU:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.MIFUYU;
                    break;
                case UnitCtrl.eAbnormalState.STONE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.STONE;
                    break;
                case UnitCtrl.eAbnormalState.REGENERATION:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.REGENERATION;
                    break;
                case UnitCtrl.eAbnormalState.PHYSICS_DODGE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.PHYSICS_DODGE;
                    break;
                case UnitCtrl.eAbnormalState.CONFUSION:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.CONFUSION;
                    break;
                case UnitCtrl.eAbnormalState.VENOM:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.VENOM;
                    break;
                case UnitCtrl.eAbnormalState.COUNT_BLIND:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.COUNT_BLIND;
                    break;
                case UnitCtrl.eAbnormalState.INHIBIT_HEAL:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.INHIBIT_HEAL;
                    break;
                case UnitCtrl.eAbnormalState.FEAR:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.FEAR;
                    break;
                case UnitCtrl.eAbnormalState.TP_REGENERATION:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.TP_REGENERATION;
                    break;
                case UnitCtrl.eAbnormalState.HEX:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.HEX;
                    break;
                case UnitCtrl.eAbnormalState.FAINT:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.FAINT;
                    break;
                case UnitCtrl.eAbnormalState.PARTS_NO_DAMAGE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.PARTS_NO_DAMAGE;
                    break;
                case UnitCtrl.eAbnormalState.COMPENSATION:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.COMPENSATION;
                    break;
                case UnitCtrl.eAbnormalState.CUT_ATK_DAMAGE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.CUT_ATK_DAMAGE;
                    break;
                case UnitCtrl.eAbnormalState.CUT_MGC_DAMAGE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.CUT_MGC_DAMAGE;
                    break;
                case UnitCtrl.eAbnormalState.CUT_ALL_DAMAGE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.CUT_ALL_DAMAGE;
                    break;
                case UnitCtrl.eAbnormalState.LOG_ATK_BARRIR:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.LOG_ATK_BARRIR;
                    break;
                case UnitCtrl.eAbnormalState.LOG_MGC_BARRIR:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.LOG_MGC_BARRIR;
                    break;
                case UnitCtrl.eAbnormalState.LOG_ALL_BARRIR:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.LOG_ALL_BARRIR;
                    break;
                case UnitCtrl.eAbnormalState.PAUSE_ACTION:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.PAUSE_ACTION;
                    break;
                case UnitCtrl.eAbnormalState.UB_SILENCE:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.UB_SILENCE;
                    break;
                case UnitCtrl.eAbnormalState.MAGIC_DARK:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.MAGIC_DARK;
                    break;
                case UnitCtrl.eAbnormalState.HEAL_DOWN:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.HEAL_DOWN;
                    break;
                case UnitCtrl.eAbnormalState.NPC_STUN:
                    abnormalStateCategory = UnitCtrl.eAbnormalStateCategory.NPC_STUN;
                    break;
            }
            return abnormalStateCategory;
        }

        /*private void setWeakColor()
        {
            this.curColor = UnitCtrl.WEAK_COLOR;
            this.updateCurColor();
        }*/

        public void SetAbnormalState(
          UnitCtrl _source,
          UnitCtrl.eAbnormalState _abnormalState,
          float _effectTime,
          ActionParameter _action,
          Skill _skill,
          float _value = 0.0f,
          float _value2 = 0.0f,
          bool _reduceEnergy = false,
          bool _isDamageRelease = false,
          float _reduceEnergyRate = 1f)
        {
            if (this.battleManager.GameState != eBattleGameState.PLAY)
                return;
            AbnormalStateEffectPrefabData abnormalEffectData = _action?.CreateAbnormalEffectData();
            if (_action != null && _action.AbnormalStateFieldAction != null)
                _action.AbnormalStateFieldAction.TargetAbnormalState = _abnormalState;
            if ((this.IsAbnormalState(UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION) || this.IsAbnormalState(UnitCtrl.eAbnormalState.NO_ABNORMAL)) && !UnitCtrl.ABNORMAL_CONST_DATA[_abnormalState].IsBuff)
            {
                BattleLogIntreface battleLog = this.battleLog;
                UnitCtrl unitCtrl1 = _source;
                UnitCtrl unitCtrl2 = this;
                long KGNFLOPBOMB = (long)(int)_value;
                int OJHBHHCOAGK = _action == null ? 0 : _action.ActionId;
                UnitCtrl JELADBAMFKH = unitCtrl1;
                UnitCtrl LIMEKPEENOB = unitCtrl2;
                battleLog.AppendBattleLog(eBattleLogType.MISS, 7, KGNFLOPBOMB, 0L, 0, OJHBHHCOAGK, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
                if (!this.IsAbnormalState(UnitCtrl.eAbnormalState.NO_ABNORMAL))
                    return;
                this.SetMissAtk(_source, eMissLogType.DODGE_BY_NO_DAMAGE_MOTION);
            }
            else
            {
                switch (_abnormalState)
                {
                    case UnitCtrl.eAbnormalState.POISON:
                    case UnitCtrl.eAbnormalState.BURN:
                    case UnitCtrl.eAbnormalState.CURSE:
                    case UnitCtrl.eAbnormalState.NO_EFFECT_SLIP_DAMAGE:
                    case UnitCtrl.eAbnormalState.VENOM:
                    case UnitCtrl.eAbnormalState.HEX:
                    case UnitCtrl.eAbnormalState.COMPENSATION:
                        this.OnSlipDamage.Call();
                        break;
                }
                BattleLogIntreface battleLog = this.battleLog;
                UnitCtrl unitCtrl1 = _source;
                UnitCtrl unitCtrl2 = this;
                int HLIKLPNIOKJ = (int)_abnormalState;
                long KGNFLOPBOMB = (long)(int)_value;
                long KDCBJHCMAOH = (long)(int)((double)_effectTime * (double)this.battleManager.FameRate);
                int OJHBHHCOAGK = _action == null ? 0 : _action.ActionId;
                UnitCtrl JELADBAMFKH = unitCtrl1;
                UnitCtrl LIMEKPEENOB = unitCtrl2;
                battleLog.AppendBattleLog(eBattleLogType.SET_ABNORMAL, HLIKLPNIOKJ, KGNFLOPBOMB, KDCBJHCMAOH, 0, OJHBHHCOAGK, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
                UnitCtrl.eAbnormalStateCategory abnormalStateCategory = UnitCtrl.GetAbnormalStateCategory(_abnormalState);
                AbnormalStateCategoryData stateCategoryData = this.abnormalStateCategoryDataDictionary[abnormalStateCategory];
                if (_abnormalState == UnitCtrl.eAbnormalState.GUARD_ATK && this.IsAbnormalState(UnitCtrl.eAbnormalState.DRAIN_ATK))
                { }  /*stateCategoryData.Effects.Add(new AbnormalStateEffectGameObject()
                    {
                        RightEffect = this.CreateAbnormalStateEffect(_abnormalState, true, abnormalEffectData),
                        LeftEffect = this.CreateAbnormalStateEffect(_abnormalState, false, abnormalEffectData)
                    });*/
                else if (_abnormalState == UnitCtrl.eAbnormalState.GUARD_MGC && this.IsAbnormalState(UnitCtrl.eAbnormalState.DRAIN_MGC))
                { }  /*stateCategoryData.Effects.Add(new AbnormalStateEffectGameObject()
                    {
                        RightEffect = this.CreateAbnormalStateEffect(_abnormalState, true, abnormalEffectData),
                        LeftEffect = this.CreateAbnormalStateEffect(_abnormalState, false, abnormalEffectData)
                    });*/
                else if (_abnormalState == UnitCtrl.eAbnormalState.GUARD_BOTH && this.IsAbnormalState(UnitCtrl.eAbnormalState.DRAIN_BOTH))
                {
                    /*stateCategoryData.Effects.Add(new AbnormalStateEffectGameObject()
                    {
                        RightEffect = this.CreateAbnormalStateEffect(_abnormalState, true, abnormalEffectData),
                        LeftEffect = this.CreateAbnormalStateEffect(_abnormalState, false, abnormalEffectData)
                    });*/
                }
                else
                {
                    if (this.IsDead)
                        return;
                    if (this.IsAbnormalState(abnormalStateCategory))
                        this.switchAbnormalState(_abnormalState, abnormalEffectData);
                    stateCategoryData.Duration = _effectTime;
                    stateCategoryData.Time = _effectTime;
                    stateCategoryData.MainValue = _value;
                    stateCategoryData.SubValue = _value2;
                    stateCategoryData.EnergyReduceRate = _reduceEnergyRate;
                    stateCategoryData.ActionId = _action == null ? 0 : _action.ActionId;
                    stateCategoryData.IsEnergyReduceMode = _reduceEnergy;
                    stateCategoryData.Skill = _skill;
                    stateCategoryData.Source = _source;
                    stateCategoryData.IsDamageRelease = _isDamageRelease;
                    stateCategoryData.IsReleasedByDamage = false;
                    if (_action != null)
                        stateCategoryData.EnergyChargeMultiple = _action.EnergyChargeMultiple;
                    stateCategoryData.AbsorberValue = this.battleManager.KIHOGJBONDH;
                    if (this.IsAbnormalState(abnormalStateCategory))
                        return;
                    stateCategoryData.CurrentAbnormalState = _abnormalState;
                    IEnumerator _cr = this.UpdateAbnormalState(_abnormalState, abnormalEffectData);
                    if (!_cr.MoveNext())
                        return;
                    this.AppendCoroutine(_cr, ePauseType.SYSTEM);
                }
            }
        }

        private void switchAbnormalState(
          UnitCtrl.eAbnormalState abnormalState,
          AbnormalStateEffectPrefabData _specialEffectData)
        {
            AbnormalStateCategoryData stateCategoryData = this.abnormalStateCategoryDataDictionary[UnitCtrl.GetAbnormalStateCategory(abnormalState)];
            this.EnableAbnormalState(stateCategoryData.CurrentAbnormalState, false, _switch: true);

            this.EnableAbnormalState(abnormalState, true,_switch_On: true);
            stateCategoryData.CurrentAbnormalState = abnormalState;
            StartCoroutine(waitReflashAbnormalStateUI(abnormalState));
            /*for (int index = 0; index < stateCategoryData.Effects.Count; ++index)
            {
                if ((UnityEngine.Object)stateCategoryData.Effects[index].RightEffect != (UnityEngine.Object)null)
                    stateCategoryData.Effects[index].RightEffect.SetTimeToDie(true);
                if ((UnityEngine.Object)stateCategoryData.Effects[index].LeftEffect != (UnityEngine.Object)null)
                    stateCategoryData.Effects[index].LeftEffect.SetTimeToDie(true);
            }*/
            /*stateCategoryData.Effects.Add(new AbnormalStateEffectGameObject()
            {
                RightEffect = this.CreateAbnormalStateEffect(abnormalState, true, _specialEffectData),
                LeftEffect = this.CreateAbnormalStateEffect(abnormalState, false, _specialEffectData)
            });*/
        }
        //added

            private IEnumerator waitReflashAbnormalStateUI(UnitCtrl.eAbnormalState abnormalState)
        {
            yield return null;
            AbnormalStateCategoryData stateCategoryData = this.abnormalStateCategoryDataDictionary[UnitCtrl.GetAbnormalStateCategory(abnormalState)];
            stateCategoryData.Duration -= 1 / 60.0f;
            string describe = stateCategoryData.MainValue + "";
            //this.EnableAbnormalState(abnormalState, true);
            //stateCategoryData.CurrentAbnormalState = abnormalState;
            this.MyOnChangeAbnormalState?.Invoke(this, UnitCtrl.ABNORMAL_CONST_DATA[abnormalState].IconType,
    true, stateCategoryData.Duration, describe);

        }
        //end add

        private IEnumerator UpdateAbnormalState(
          UnitCtrl.eAbnormalState _abnormalState,
          AbnormalStateEffectPrefabData _specialEffectData)
        {
            UnitCtrl.eAbnormalStateCategory abnormalStateCategory = UnitCtrl.GetAbnormalStateCategory(_abnormalState);
            AbnormalStateCategoryData abnormalStateCategoryData = this.abnormalStateCategoryDataDictionary[abnormalStateCategory];
            abnormalStateCategoryData.Time = abnormalStateCategoryData.Duration;
            /*abnormalStateCategoryData.Effects.Add(new AbnormalStateEffectGameObject()
            {
                RightEffect = this.CreateAbnormalStateEffect(_abnormalState, true, _specialEffectData),
                LeftEffect = this.CreateAbnormalStateEffect(_abnormalState, false, _specialEffectData)
            });*/
            this.EnableAbnormalState(_abnormalState, true, abnormalStateCategoryData.IsEnergyReduceMode);
            while (this.IsAbnormalState(abnormalStateCategory))
            {
                _abnormalState = abnormalStateCategoryData.CurrentAbnormalState;
                if (abnormalStateCategoryData.IsEnergyReduceMode)
                {
                    this.SetEnergy(this.Energy - this.DeltaTimeForPause * abnormalStateCategoryData.EnergyReduceRate, eSetEnergyType.BY_MODE_CHANGE);
                    if ((double)this.Energy == 0.0 || this.IsDead)
                    {
                        this.EnableAbnormalState(_abnormalState, false);
                        break;
                    }
                }
                else
                {
                    abnormalStateCategoryData.Time -= this.DeltaTimeForPause;
                    if ((double)abnormalStateCategoryData.Time <= 0.0 || this.IsDead)
                    {
                        this.EnableAbnormalState(_abnormalState, false);
                        break;
                    }
                }
                yield return (object)null;
            }
        }

        private void DestroyAbnormalEffect(
          UnitCtrl.eAbnormalStateCategory abnormalStateCategory)
        {
            /*for (int index = 0; index < this.abnormalStateCategoryDataDictionary[abnormalStateCategory].Effects.Count; ++index)
            {
                AbnormalStateEffectGameObject effect = this.abnormalStateCategoryDataDictionary[abnormalStateCategory].Effects[index];
                if ((UnityEngine.Object)effect.RightEffect != (UnityEngine.Object)null)
                    effect.RightEffect.SetTimeToDie(true);
                if ((UnityEngine.Object)effect.LeftEffect != (UnityEngine.Object)null)
                    effect.LeftEffect.SetTimeToDie(true);
            }*/
            this.abnormalStateCategoryDataDictionary[abnormalStateCategory].Effects.Clear();
        }

        public void DisableAbnormalStateById(
          UnitCtrl.eAbnormalState _abnormalState,
          int _actionId,
          bool _isReleasedByDamage)
        {
            UnitCtrl.eAbnormalStateCategory abnormalStateCategory = UnitCtrl.GetAbnormalStateCategory(_abnormalState);
            if (this.abnormalStateCategoryDataDictionary[abnormalStateCategory].ActionId != _actionId)
                return;
            this.abnormalStateCategoryDataDictionary[abnormalStateCategory].IsReleasedByDamage = _isReleasedByDamage;
            this.EnableAbnormalState(_abnormalState, false);
        }

        private void EnableAbnormalState(
          UnitCtrl.eAbnormalState _abnormalState,
          bool _enable,
          bool _reduceEnergy = false,
          bool _switch = false,
            bool _switch_On = false)
        {
            UnitCtrl.eAbnormalStateCategory abnormalStateCategory = UnitCtrl.GetAbnormalStateCategory(_abnormalState);
            if (!_enable)
            {
                this.DestroyAbnormalEffect(abnormalStateCategory);
                this.abnormalStateCategoryDataDictionary[abnormalStateCategory].MainValue = 0.0f;
                this.abnormalStateCategoryDataDictionary[abnormalStateCategory].Time = 0.0f;
                this.abnormalStateCategoryDataDictionary[abnormalStateCategory].Duration = 0.0f;
                this.abnormalStateCategoryDataDictionary[abnormalStateCategory].EnergyChargeMultiple = 1f;
            }
            this.abnormalStateCategoryDataDictionary[abnormalStateCategory].enable = _enable;
            this.m_abnormalState[_abnormalState] = _enable;
            string describe = this.abnormalStateCategoryDataDictionary[abnormalStateCategory].MainValue + "";
            switch (_abnormalState)
            {
                case UnitCtrl.eAbnormalState.HASTE:
                    if (_enable)
                    {
                        if (this.CurrentState == UnitCtrl.ActionState.IDLE)
                        {
                            this.GetCurrentSpineCtrl().SetTimeScale(2f);
                            break;
                        }
                        break;
                    }
                    if (!this.IsUnableActionState() && !this.m_bPause)
                    {
                        this.GetCurrentSpineCtrl().Resume();
                        break;
                    }
                    break;
                case UnitCtrl.eAbnormalState.POISON:
                case UnitCtrl.eAbnormalState.BURN:
                case UnitCtrl.eAbnormalState.CURSE:
                case UnitCtrl.eAbnormalState.NO_EFFECT_SLIP_DAMAGE:
                case UnitCtrl.eAbnormalState.VENOM:
                case UnitCtrl.eAbnormalState.HEX:
                case UnitCtrl.eAbnormalState.COMPENSATION:
                    if (_enable)
                    {
                        this.AppendCoroutine(this.UpdateSlipDamage(abnormalStateCategory, ++this.slipDamageIdDictionary[abnormalStateCategory]), ePauseType.SYSTEM);
                        break;
                    }
                    break;
                case UnitCtrl.eAbnormalState.SLOW:
                    if (_enable)
                    {
                        //this.setWeakColor();
                        if (this.CurrentState == UnitCtrl.ActionState.IDLE)
                        {
                            this.GetCurrentSpineCtrl().SetTimeScale(0.5f);
                            break;
                        }
                        break;
                    }
                    //this.SetEnableColor();
                    if (!this.IsUnableActionState())
                    {
                        this.GetCurrentSpineCtrl().Resume();
                        break;
                    }
                    break;
                case UnitCtrl.eAbnormalState.PARALYSIS:
                case UnitCtrl.eAbnormalState.FREEZE:
                case UnitCtrl.eAbnormalState.CHAINED:
                case UnitCtrl.eAbnormalState.STUN:
                case UnitCtrl.eAbnormalState.DETAIN:
                case UnitCtrl.eAbnormalState.FAINT:
                    if (_enable)
                    {
                        if (this.CurrentState != UnitCtrl.ActionState.DAMAGE)
                        {
                            this.SetState(UnitCtrl.ActionState.DAMAGE);
                            break;
                        }
                        break;
                    }
                    if (!this.IsUnableActionState() && !_switch)
                    {
                        this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                        BattleSpineController currentSpineCtrl = this.GetCurrentSpineCtrl();
                        currentSpineCtrl.IsPlayAnimeBattle = false;
                        currentSpineCtrl.IsStopState = false;
                        this.setMotionResume();
                        this.isContinueIdleForPauseAction = false;
                        break;
                    }
                    break;
                case UnitCtrl.eAbnormalState.CONVERT:
                case UnitCtrl.eAbnormalState.CONFUSION:
                    if (!this.IsAbnormalState(UnitCtrl.eAbnormalState.PAUSE_ACTION))
                        this.SetDirectionAuto();
                    switch (this.CurrentState)
                    {
                        case UnitCtrl.ActionState.ATK:
                        case UnitCtrl.ActionState.SKILL_1:
                        case UnitCtrl.ActionState.SKILL:
                            this.CancelByConvert = true;
                            this.idleStartAfterWaitFrame = (double)(float)this.m_fCastTimer <= (double)this.DeltaTimeForPause;
                            break;
                    }
                    if ((long)this.Hp > 0L && !this.IsUnableActionState() && this.CurrentState != UnitCtrl.ActionState.DAMAGE)
                    {
                        this.SetState(UnitCtrl.ActionState.IDLE);
                        break;
                    }
                    break;
                case UnitCtrl.eAbnormalState.SLEEP:
                    BattleSpineController currentSpineCtrl1 = this.GetCurrentSpineCtrl();
                    if (_enable)
                    {
                        bool _isDamageAnimBeforeSleep = currentSpineCtrl1.AnimationName == currentSpineCtrl1.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, this.MotionPrefix) || currentSpineCtrl1.AnimationName == currentSpineCtrl1.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, _index2: this.PartsMotionPrefix);
                        if (this.CurrentState != UnitCtrl.ActionState.DAMAGE)
                            this.SetState(UnitCtrl.ActionState.DAMAGE, _quiet: true);
                        if (!this.IsUnableActionState(UnitCtrl.eAbnormalState.SLEEP) && currentSpineCtrl1.HasSpecialSleepAnimatilon(this.MotionPrefix) && !currentSpineCtrl1.CheckPlaySpecialSleepAnimeExceptRelease(this.MotionPrefix))
                        {
                            this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.START;
                            this.battleManager.AppendCoroutine(this.playSleepAnime(_isDamageAnimBeforeSleep, this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.SLEEP].IsDamageRelease), ePauseType.IGNORE_BLACK_OUT);
                            break;
                        }
                        break;
                    }
                    if (!this.IsUnableActionState() && !_switch)
                    {
                        if (currentSpineCtrl1.HasSpecialSleepAnimatilon(this.MotionPrefix))
                        {
                            currentSpineCtrl1.IsStopState = false;
                            this.releaseSleepAnime();
                        }
                        this.shiftAbnormalColor();
                        this.isContinueIdleForPauseAction = false;
                        break;
                    }
                    break;
                case UnitCtrl.eAbnormalState.DECOY:
                    UnitCtrl unitCtrl = this.IsOther ? this.battleManager.DecoyEnemy : this.battleManager.DecoyUnit;
                    if (_enable)
                    {
                        if ((UnityEngine.Object)unitCtrl != (UnityEngine.Object)null && (UnityEngine.Object)unitCtrl != (UnityEngine.Object)this)
                            unitCtrl.EnableAbnormalState(UnitCtrl.eAbnormalState.DECOY, false);
                        if (this.IsOther)
                        {
                            this.battleManager.DecoyEnemy = this;
                            break;
                        }
                        this.battleManager.DecoyUnit = this;
                        break;
                    }
                    if ((UnityEngine.Object)unitCtrl == (UnityEngine.Object)this)
                    {
                        if (this.IsOther)
                        {
                            this.battleManager.DecoyEnemy = (UnitCtrl)null;
                            break;
                        }
                        this.battleManager.DecoyUnit = (UnitCtrl)null;
                        break;
                    }
                    break;
                case UnitCtrl.eAbnormalState.MIFUYU:
                    if (!_enable)
                        break;
                    break;
                case UnitCtrl.eAbnormalState.STONE:
                    this.GetCurrentSpineCtrl().IsColorStone = _enable;
                    if (_enable)
                    {
                        if (this.CurrentState != UnitCtrl.ActionState.DAMAGE)
                            this.SetState(UnitCtrl.ActionState.DAMAGE);
                        if (this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.INVALID)
                        {
                            this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                            break;
                        }
                        break;
                    }
                    if (!this.IsUnableActionState() && !_switch)
                        this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                    this.shiftAbnormalColor();
                    break;
                case UnitCtrl.eAbnormalState.REGENERATION:
                    if (_enable)
                    {
                        this.AppendCoroutine(this.UpdateHpRegeneration(++this.currentHpRegeneId), ePauseType.SYSTEM);
                        break;
                    }
                    break;
                case UnitCtrl.eAbnormalState.TP_REGENERATION:
                    if (_enable)
                    {
                        this.AppendCoroutine(this.UpdateTpRegeneration(++this.currentTpRegeneId), ePauseType.SYSTEM);
                        break;
                    }
                    break;
                case UnitCtrl.eAbnormalState.PAUSE_ACTION:
                    BattleSpineController currentSpineCtrl2 = this.GetCurrentSpineCtrl();
                    currentSpineCtrl2.IsColorPauseAction = _enable;
                    if (_enable)
                    {
                        if (this.CurrentState != UnitCtrl.ActionState.DAMAGE)
                            this.SetState(UnitCtrl.ActionState.DAMAGE);
                        currentSpineCtrl2.IsStopState = true;
                        this.setMotionResume();
                        this.PlayAnime(eSpineCharacterAnimeId.IDLE, this.MotionPrefix);
                        this.setMotionPause();
                        this.isContinueIdleForPauseAction = true;
                        this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                        break;
                    }
                    if (!this.IsUnableActionState() && !_switch)
                    {
                        this.isContinueIdleForPauseAction = false;
                        this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                        currentSpineCtrl2.IsPlayAnimeBattle = false;
                        currentSpineCtrl2.IsStopState = false;
                        currentSpineCtrl2.Resume();
                        this.SetDirectionAuto();
                        break;
                    }
                    this.shiftAbnormalColor();
                    break;
            }
            this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, UnitCtrl.ABNORMAL_CONST_DATA[_abnormalState].IconType, _enable);
            this.MyOnChangeAbnormalState?.Invoke(this, UnitCtrl.ABNORMAL_CONST_DATA[_abnormalState].IconType,
                _enable, this.abnormalStateCategoryDataDictionary[abnormalStateCategory].Duration, describe);
                CallBackAbnormalStateChanged(this.abnormalStateCategoryDataDictionary[abnormalStateCategory],_switch_On);
            if (_enable || _switch)
                return;
            this.battleManager.RestartAbnormalStateField(this, _abnormalState);
        }

        private void shiftAbnormalColor()
        {
            if (!this.IsUnableActionState())
                return;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.STONE))
            {
                this.GetCurrentSpineCtrl().IsColorStone = true;
            }
            else
            {
                if (!this.IsAbnormalState(UnitCtrl.eAbnormalState.PAUSE_ACTION))
                    return;
                this.GetCurrentSpineCtrl().IsColorPauseAction = true;
            }
        }

        private IEnumerator playSleepAnime(
          bool _isDamageAnimBeforeSleep,
          bool _isDamageRelease)
        {
            BattleSpineController battleSpineController = this.GetCurrentSpineCtrl();
            if (battleSpineController.HasSpecialSleepAnimatilon(this.MotionPrefix) && !(battleSpineController.AnimationName == battleSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, this.MotionPrefix, 1, 0)))
            {
                if (this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.START)
                    this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                else if (_isDamageAnimBeforeSleep)
                {
                    battleSpineController.PlayAnime(eSpineCharacterAnimeId.SLEEP, this.MotionPrefix, 1, 0);
                    this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.LOOP;
                }
                else
                {
                    battleSpineController.IsStopState = true;
                    battleSpineController.PlayAnime(eSpineCharacterAnimeId.SLEEP, this.MotionPrefix, 0, 0, false);
                    battleSpineController.Resume();
                    string sleepStartAnimName = battleSpineController.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, this.MotionPrefix, 0, 0);
                    TrackEntry trackEntry = battleSpineController.state.GetCurrent(0);
                    this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.WAIT_START_END;
                    while ((long)this.Hp > 0L)
                    {
                        if (this.specialSleepStatus == UnitCtrl.eSpecialSleepStatus.LOOP)
                            yield break;
                        else if (this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.WAIT_START_END)
                        {
                            this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.PAUSE_ACTION))
                            {
                                yield break;
                            }
                            else
                            {
                                battleSpineController.IsStopState = false;
                                yield break;
                            }
                        }
                        else if (!this.IsAbnormalState(UnitCtrl.eAbnormalState.SLEEP))
                        {
                            battleSpineController.IsStopState = false;
                            this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                            yield break;
                        }
                        else if (this.CurrentState != UnitCtrl.ActionState.DAMAGE)
                        {
                            battleSpineController.IsStopState = false;
                            this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                            yield break;
                        }
                        else
                        {
                            if (this.GetCurrentSpineCtrl().AnimeName != sleepStartAnimName)
                            {
                                if (!this.GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(this.MotionPrefix))
                                {
                                    battleSpineController.IsStopState = false;
                                    this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                                }
                                bool flag = this.GetCurrentSpineCtrl().AnimationName == this.GetCurrentSpineCtrl().ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, this.MotionPrefix) || this.GetCurrentSpineCtrl().AnimationName == this.GetCurrentSpineCtrl().ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, _index2: this.PartsMotionPrefix);
                                if (this.battleManager.ChargeSkillTurn == eChargeSkillTurn.PLAYER || this.battleManager.ChargeSkillTurn == eChargeSkillTurn.ENEMY)
                                {
                                    if (flag)
                                    {
                                        if (_isDamageRelease)
                                        {
                                            battleSpineController.IsStopState = false;
                                            this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                                            yield break;
                                        }
                                        else
                                        {
                                            this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.LOOP;
                                            yield break;
                                        }
                                    }
                                }
                                else if (flag)
                                {
                                    battleSpineController.PlayAnime(eSpineCharacterAnimeId.SLEEP, this.MotionPrefix, 1, 0);
                                    this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.LOOP;
                                    yield break;
                                }
                                else if (!(this.GetCurrentSpineCtrl().AnimeName != this.GetCurrentSpineCtrl().ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, this.MotionPrefix, 1, 0)))
                                {
                                    yield break;
                                }
                                else
                                {
                                    battleSpineController.IsStopState = false;
                                    this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                                    yield break;
                                }
                            }
                            if (trackEntry.IsComplete)
                            {
                                if (!this.GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(this.MotionPrefix))
                                {
                                    battleSpineController.IsStopState = false;
                                    this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                                }
                                battleSpineController.PlayAnime(eSpineCharacterAnimeId.SLEEP, this.MotionPrefix, 1, 0);
                                battleSpineController.Resume();
                                this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.LOOP;
                                yield break;
                            }
                            else
                                yield return (object)null;
                        }
                    }
                    battleSpineController.IsStopState = false;
                }
            }
        }

        private void releaseSleepAnime()
        {
            BattleSpineController battleSpineController = this.GetCurrentSpineCtrl();
            if (!battleSpineController.HasSpecialSleepAnimatilon(this.MotionPrefix))
            {
                battleSpineController.IsPlayAnimeBattle = false;
                this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
            }
            else if (this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.LOOP)
            {
                battleSpineController.IsPlayAnimeBattle = false;
                this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
            }
            else if (this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.SLEEP].IsReleasedByDamage)
            {
                battleSpineController.IsPlayAnimeBattle = false;
                this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                this.PlayAnime(eSpineCharacterAnimeId.DAMEGE, this.MotionPrefix, _isLoop: false, _ignoreBlackout: true);
            }
            else
            {
                this.OnDamageForSpecialSleepRelease = (System.Action<bool>)(_byAttack =>
               {
                   if (!_byAttack)
                       return;
                   battleSpineController.IsPlayAnimeBattle = false;
                   this.PlayAnime(eSpineCharacterAnimeId.DAMEGE, this.MotionPrefix, _isLoop: false, _ignoreBlackout: true);
                   this.OnDamageForSpecialSleepRelease = (System.Action<bool>)null;
               });
                this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                this.StartCoroutine(this.endSleepReleaseAnim());
            }
        }

        private IEnumerator endSleepReleaseAnim()
        {
            BattleSpineController currentSpineCtrl = this.GetCurrentSpineCtrl();
            currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.SLEEP, this.MotionPrefix, 2, 0, false);
            TrackEntry trackEntry = currentSpineCtrl.state.GetCurrent(0);
            while (!trackEntry.IsComplete && this.GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(this.MotionPrefix) && !(this.GetCurrentSpineCtrl().AnimeName != this.GetCurrentSpineCtrl().ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, this.MotionPrefix, 2, 0)))
                yield return (object)null;
            this.OnDamageForSpecialSleepRelease = (System.Action<bool>)null;
        }

        private IEnumerator UpdateHpRegeneration(int _regeneId)
        {
            float time = 0.0f;
            while (true)
            {
                do
                {
                    yield return (object)null;
                    if (!this.IsAbnormalState(UnitCtrl.eAbnormalState.REGENERATION) || _regeneId != this.currentHpRegeneId)
                        yield break;
                    else
                        time += this.DeltaTimeForPause;
                }
                while ((double)time <= 1.0 && !BattleUtil.Approximately(time, 1f));
                AbnormalStateCategoryData stateCategoryData = this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.REGENERATION];
                this.SetRecovery((int)stateCategoryData.MainValue, (int)stateCategoryData.SubValue == 1 ? UnitCtrl.eInhibitHealType.PHYSICS : UnitCtrl.eInhibitHealType.MAGIC, stateCategoryData.Source, UnitCtrl.GetHealDownValue(stateCategoryData.Source), _isRegenerate: true, _releaseToad: true);
                time = 0.0f;
            }
        }

        public static float GetHealDownValue(UnitCtrl _source) => !_source.IsAbnormalState(UnitCtrl.eAbnormalState.HEAL_DOWN) ? 1f : _source.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.HEAL_DOWN].MainValue;


        private IEnumerator UpdateTpRegeneration(int _regeneId)
        {
            UnitCtrl _source = this;
            float time = 0.0f;
            while (true)
            {
                do
                {
                    yield return (object)null;
                    if (!_source.IsAbnormalState(UnitCtrl.eAbnormalState.TP_REGENERATION) || _regeneId != _source.currentTpRegeneId)
                        yield break;
                    else
                        time += _source.DeltaTimeForPause;
                }
                while ((double)time <= 1.0 && !BattleUtil.Approximately(time, 1f));
                AbnormalStateCategoryData stateCategoryData = _source.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.TP_REGENERATION];
                _source.ChargeEnergy(eSetEnergyType.BY_USE_SKILL, stateCategoryData.MainValue, true, _source);
                time = 0.0f;
            }
        }

        private IEnumerator UpdateSlipDamage(
          UnitCtrl.eAbnormalStateCategory _category,
          int _slipDamageId)
        {
            UnitCtrl unitCtrl1 = this;
            AbnormalStateCategoryData categoryData = unitCtrl1.abnormalStateCategoryDataDictionary[_category];
            float time = 0.0f;
            int damage = (int)categoryData.MainValue;
            int incrementDamage = (int)((double)categoryData.MainValue * (double)categoryData.SubValue / 100.0);
            while (true)
            {
                do
                {
                    yield return (object)null;
                    if (!unitCtrl1.IsAbnormalState(_category) || unitCtrl1.slipDamageIdDictionary[_category] != _slipDamageId)
                        yield break;
                    else
                        time += unitCtrl1.DeltaTimeForPause;
                }
                while ((double)time <= 1.0 && !BattleUtil.Approximately(time, 1f));
                DamageData damageData = new DamageData()
                {
                    Target = unitCtrl1.GetFirstParts(),
                    Damage = (long)damage,
                    DamageType = DamageData.eDamageType.NONE,
                    Source = categoryData.Source,
                    DamageSoundType = DamageData.eDamageSoundType.SLIP,
                    IsSlipDamage = true,
                    ActionType = eActionType.SLIP_DAMAGE,
                    ExecAbsorber = (Func<int, int>)(_damage =>
                   {
                       if (_damage > categoryData.AbsorberValue)
                       {
                           int num = _damage - categoryData.AbsorberValue;
                           //this.battleManager.SubstructEnemyPoint(categoryData.AbsorberValue);
                           categoryData.AbsorberValue = 0;
                           return num;
                       }
                       categoryData.AbsorberValue -= _damage;
                       //this.battleManager.SubstructEnemyPoint(_damage);
                       return 0;
                   })
                };
                UnitCtrl unitCtrl2 = unitCtrl1;
                DamageData _damageData = damageData;
                Skill skill = categoryData.Skill;
                int actionId = categoryData.ActionId;
                Skill _skill = skill;
                double energyChargeMultiple = (double)categoryData.EnergyChargeMultiple;
                unitCtrl2.SetDamage(_damageData, false, actionId, _skill: _skill, _energyChargeMultiple: ((float)energyChargeMultiple));
                time = 0.0f;
                damage += incrementDamage;
            }
        }

        /*private SkillEffectCtrl CreateAbnormalStateEffect(
          UnitCtrl.eAbnormalState _abnormalState,
          bool _isRight,
          AbnormalStateEffectPrefabData _specialEffectData)
        {
            AbnormalStateEffectPrefabData effectPrefabData = (AbnormalStateEffectPrefabData)null;
            if (_specialEffectData == null)
            {
                switch (_abnormalState)
                {
                    case UnitCtrl.eAbnormalState.GUARD_ATK:
                    case UnitCtrl.eAbnormalState.GUARD_MGC:
                    case UnitCtrl.eAbnormalState.DRAIN_ATK:
                    case UnitCtrl.eAbnormalState.DRAIN_MGC:
                    case UnitCtrl.eAbnormalState.GUARD_BOTH:
                    case UnitCtrl.eAbnormalState.DRAIN_BOTH:
                    case UnitCtrl.eAbnormalState.NO_ABNORMAL:
                    case UnitCtrl.eAbnormalState.NO_DEBUF:
                        effectPrefabData = Singleton<LCEGKJFKOPD>.Instance.FNKHFKMEOKB[UnitCtrl.eAbnormalState.GUARD_ATK];
                        break;
                    default:
                        if (!Singleton<LCEGKJFKOPD>.Instance.FNKHFKMEOKB.TryGetValue(_abnormalState, out effectPrefabData))
                            return (SkillEffectCtrl)null;
                        break;
                }
            }
            else
                effectPrefabData = _specialEffectData;
            GameObject gameObject1 = _isRight || (UnityEngine.Object)effectPrefabData.LeftEffectPrefab == (UnityEngine.Object)null ? effectPrefabData.RightEffectPrefab : effectPrefabData.LeftEffectPrefab;
            if ((UnityEngine.Object)gameObject1 == (UnityEngine.Object)null)
                return (SkillEffectCtrl)null;
            GameObject gameObject2 = this.battleEffectPool.GetEffect(gameObject1).gameObject;
            gameObject2.transform.localScale = gameObject1.transform.localScale;
            gameObject2.transform.position = new Vector3(this.BottomTransform.transform.position.x, effectPrefabData.IsHead ? this.lifeGauge.transform.position.y : this.BottomTransform.transform.position.y, 0.0f) + gameObject1.transform.position;
            gameObject2.transform.parent = ExceptNGUIRoot.Transform;
            bool flag = _isRight != !this.IsLeftDir;
            SkillEffectCtrl component = gameObject2.GetComponent<SkillEffectCtrl>();
            if ((UnityEngine.Object)component != (UnityEngine.Object)null)
            {
                component.ResetParameter(gameObject1);
                component.InitializeSort();
                if (!flag)
                    component.PlaySe(this.SoundUnitId, this.IsLeftDir);
                int offset = 0;
                if (this.statusEffectOrderDictionary.Count > 0)
                    offset = this.statusEffectOrderDictionary.OrderByDescending<KeyValuePair<GameObject, int>, int>((Func<KeyValuePair<GameObject, int>, int>)(x => x.Value)).First<KeyValuePair<GameObject, int>>().Value + 1;
                if (this.statusEffectOrderDictionary.ContainsKey(gameObject2))
                    this.statusEffectOrderDictionary[gameObject2] = offset;
                else
                    this.statusEffectOrderDictionary.Add(gameObject2, offset);
                component.SortTarget = this;
                component.SetSortOrderStatus(offset);
                component.ExecAppendCoroutine(_isAbnormal: true);
                this.StartCoroutine(component.TrackTargetSort(this));
                switch (_abnormalState)
                {
                    case UnitCtrl.eAbnormalState.POISON:
                    case UnitCtrl.eAbnormalState.BURN:
                    case UnitCtrl.eAbnormalState.CURSE:
                    case UnitCtrl.eAbnormalState.CONVERT:
                    case UnitCtrl.eAbnormalState.PHYSICS_DARK:
                    case UnitCtrl.eAbnormalState.SILENCE:
                    case UnitCtrl.eAbnormalState.STUN:
                    case UnitCtrl.eAbnormalState.CONFUSION:
                    case UnitCtrl.eAbnormalState.VENOM:
                    case UnitCtrl.eAbnormalState.HEX:
                    case UnitCtrl.eAbnormalState.FAINT:
                    case UnitCtrl.eAbnormalState.COMPENSATION:
                    case UnitCtrl.eAbnormalState.UB_SILENCE:
                        this.StartCoroutine(component.TrackTarget(this.GetFirstParts(true), Vector3.zero, bone: (this.ToadDatas.Count > 0 ? this.ToadDatas[0].StateBone : this.StateBone)));
                        break;
                    case UnitCtrl.eAbnormalState.FREEZE:
                    case UnitCtrl.eAbnormalState.DETAIN:
                        this.StartCoroutine(component.TrackTarget(this.GetFirstParts(true), Vector3.zero));
                        break;
                    default:
                        this.StartCoroutine(component.TrackTarget(this.GetFirstParts(true), Vector3.zero, bone: (effectPrefabData.IsHead ? this.StateBone : this.CenterBone)));
                        break;
                }
            }
            if (flag)
                gameObject2.SetActive(false);
            return component;
        }*/

        public bool IsAbnormalState(UnitCtrl.eAbnormalState abnormalState) => this.m_abnormalState[abnormalState];

        public bool IsConfusionOrConvert() => this.m_abnormalState[UnitCtrl.eAbnormalState.CONFUSION] || this.m_abnormalState[UnitCtrl.eAbnormalState.CONVERT];

        public bool IsSlipDamageState() => this.m_abnormalState[UnitCtrl.eAbnormalState.BURN] || this.m_abnormalState[UnitCtrl.eAbnormalState.POISON] || (this.m_abnormalState[UnitCtrl.eAbnormalState.VENOM] || this.m_abnormalState[UnitCtrl.eAbnormalState.CURSE]) || (this.m_abnormalState[UnitCtrl.eAbnormalState.DETAIN] || this.m_abnormalState[UnitCtrl.eAbnormalState.HEX]) || this.m_abnormalState[UnitCtrl.eAbnormalState.COMPENSATION];

        public bool IsAbnormalState(
          UnitCtrl.eAbnormalStateCategory abnormalStateCategory) => this.abnormalStateCategoryDataDictionary[abnormalStateCategory].enable;

        public float GetAbnormalStateMainValue(
          UnitCtrl.eAbnormalStateCategory _abnormalStateCategory) => this.abnormalStateCategoryDataDictionary[_abnormalStateCategory].MainValue;

        public void DecreaseCountBlind()
        {
            --this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.COUNT_BLIND].MainValue;
            if ((double)this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.COUNT_BLIND].MainValue != 0.0)
                return;
            this.EnableAbnormalState(UnitCtrl.eAbnormalState.COUNT_BLIND, false);
        }

        public float GetAbnormalStateSubValue(
          UnitCtrl.eAbnormalStateCategory abnormalStateCategory) => this.abnormalStateCategoryDataDictionary[abnormalStateCategory].SubValue;

        public bool IsUnableActionState() => this.IsAbnormalState(UnitCtrl.eAbnormalState.PARALYSIS) || this.IsAbnormalState(UnitCtrl.eAbnormalState.FREEZE) || (this.IsAbnormalState(UnitCtrl.eAbnormalState.SLEEP) || this.IsAbnormalState(UnitCtrl.eAbnormalState.CHAINED)) || (this.IsAbnormalState(UnitCtrl.eAbnormalState.STUN) || this.IsAbnormalState(UnitCtrl.eAbnormalState.STONE) || (this.IsAbnormalState(UnitCtrl.eAbnormalState.DETAIN) || this.IsAbnormalState(UnitCtrl.eAbnormalState.FAINT))) || this.IsAbnormalState(UnitCtrl.eAbnormalState.PAUSE_ACTION) || this.KnockBackEnableCount > 0;

        public bool IsUnableActionState(UnitCtrl.eAbnormalState _removeCheckState) => this.IsAbnormalState(UnitCtrl.eAbnormalState.PARALYSIS) && _removeCheckState != UnitCtrl.eAbnormalState.PARALYSIS || this.IsAbnormalState(UnitCtrl.eAbnormalState.FREEZE) && _removeCheckState != UnitCtrl.eAbnormalState.FREEZE || (this.IsAbnormalState(UnitCtrl.eAbnormalState.SLEEP) && _removeCheckState != UnitCtrl.eAbnormalState.SLEEP || this.IsAbnormalState(UnitCtrl.eAbnormalState.CHAINED) && _removeCheckState != UnitCtrl.eAbnormalState.CHAINED) || (this.IsAbnormalState(UnitCtrl.eAbnormalState.STUN) && _removeCheckState != UnitCtrl.eAbnormalState.STUN || this.IsAbnormalState(UnitCtrl.eAbnormalState.STONE) && _removeCheckState != UnitCtrl.eAbnormalState.STONE || (this.IsAbnormalState(UnitCtrl.eAbnormalState.DETAIN) && _removeCheckState != UnitCtrl.eAbnormalState.DETAIN || this.IsAbnormalState(UnitCtrl.eAbnormalState.FAINT) && _removeCheckState != UnitCtrl.eAbnormalState.FAINT)) || this.IsAbnormalState(UnitCtrl.eAbnormalState.PAUSE_ACTION) && _removeCheckState != UnitCtrl.eAbnormalState.PAUSE_ACTION || this.KnockBackEnableCount > 0;

        public void CureAllAbnormalState()
        {
            for (UnitCtrl.eAbnormalState eAbnormalState = UnitCtrl.eAbnormalState.GUARD_ATK; eAbnormalState < UnitCtrl.eAbnormalState.NUM; ++eAbnormalState)
            {
                if (this.IsAbnormalState(eAbnormalState))
                    this.EnableAbnormalState(eAbnormalState, false);
            }
            for (int index = this.LifeStealQueueList.Count - 1; index >= 0; --index)
            {
                this.LifeStealQueueList[index].Dequeue();
                if (this.LifeStealQueueList[index].Count == 0)
                {
                    this.LifeStealQueueList.RemoveAt(index);
                    this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, eStateIconType.BUFF_ADD_LIFE_STEAL, false);
                    MyOnChangeAbnormalState?.Invoke(this, eStateIconType.BUFF_ADD_LIFE_STEAL, false, 90, "1次");

                }
            }
            foreach (KeyValuePair<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet> strikeBack in this.StrikeBackDictionary)
            {
                strikeBack.Value.SetTimeToDie();
                for (int index = 0; index < strikeBack.Value.DataList.Count; ++index)
                {
                    this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, eStateIconType.STRIKE_BACK, false);
                    MyOnChangeAbnormalState?.Invoke(this, eStateIconType.STRIKE_BACK, false, 90, "反击中");

                }
            }
            this.StrikeBackDictionary.Clear();
            foreach (KeyValuePair<eStateIconType, List<KnightGuardData>> knightGuardData in this.knightGuardDatas)
            {
                if (knightGuardData.Value.Count != 0)
                {
                    this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, knightGuardData.Key, false);
                    MyOnChangeAbnormalState?.Invoke(this, knightGuardData.Key, false, 90, "???");
                    knightGuardData.Value.Clear();
                }
            }
            this.StrikeBackDictionary.Clear();
            this.ClearKnightGuard();
            this.DamageSealDataDictionary.Clear();
            this.DamageOnceOwnerSealDateDictionary.Clear();
            this.DamageOwnerSealDataDictionary.Clear();
            this.UbIsDisableByChangePattern = false;
            this.passiveSealDictionary.Clear();

        }
        public void ClearKnightGuard()
        {
            foreach (KeyValuePair<eStateIconType, List<KnightGuardData>> knightGuardData in this.knightGuardDatas)
            {
                if (knightGuardData.Value.Count != 0)
                {
                    this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, knightGuardData.Key, false);
                    knightGuardData.Value.Clear();
                }
            }
        }

        public void CureAbnormalState(CureAction.eCureActionType type)
        {
            switch (type)
            {
                case CureAction.eCureActionType.SLOW:
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.SLOW, false);
                    break;
                case CureAction.eCureActionType.HASTE:
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.HASTE, false);
                    break;
                case CureAction.eCureActionType.PARALYSIS:
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.PARALYSIS, false);
                    break;
                case CureAction.eCureActionType.FREEZE:
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.FREEZE, false);
                    break;
                case CureAction.eCureActionType.CHAINED:
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.CHAINED, false);
                    break;
                case CureAction.eCureActionType.SLEEP:
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.SLEEP, false);
                    break;
                case CureAction.eCureActionType.POISONE:
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.POISON, false);
                    break;
                case CureAction.eCureActionType.BURN:
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.BURN, false);
                    break;
                case CureAction.eCureActionType.CURSE:
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.CURSE, false);
                    break;
            }
        }

        /*public void EnableAbnormalEffect(bool _active)
        {
            this.isAbnormalEffectEnable = _active;
            foreach (KeyValuePair<UnitCtrl.eAbnormalStateCategory, AbnormalStateCategoryData> stateCategoryData in this.abnormalStateCategoryDataDictionary)
            {
                if (this.IsAbnormalState(stateCategoryData.Key))
                {
                    for (int index = 0; index < stateCategoryData.Value.Effects.Count; ++index)
                    {
                        AbnormalStateEffectGameObject effect = stateCategoryData.Value.Effects[index];
                        if ((UnityEngine.Object)effect.LeftEffect != (UnityEngine.Object)null)
                            effect.LeftEffect.SetActive(_active && this.IsLeftDir);
                        if ((UnityEngine.Object)effect.RightEffect != (UnityEngine.Object)null)
                            effect.RightEffect.SetActive(_active && !this.IsLeftDir);
                    }
                }
            }
            for (int index = 0; index < this.buffDebuffEffectList.Count; ++index)
                this.buffDebuffEffectList[index].SetActive(_active);
            this.lifeGauge.SetActive(_active);
            if (!this.IsFront)
                return;
            Dictionary<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet>.Enumerator enumerator = this.StrikeBackDictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                CircleEffectController skillEffect = enumerator.Current.Value.SkillEffect as CircleEffectController;
                if (_active)
                {
                    for (int index = 0; index < skillEffect.Children.Count; ++index)
                        skillEffect.Children[index].transform.localPosition += new Vector3(0.0f, BattleDefine.BATTLE_FIELD_SIZE, 0.0f);
                }
                else
                {
                    for (int index = 0; index < skillEffect.Children.Count; ++index)
                        skillEffect.Children[index].transform.localPosition -= new Vector3(0.0f, BattleDefine.BATTLE_FIELD_SIZE, 0.0f);
                }
            }
        }*/

        public void EnableAuraEffect(bool _active)
        {
            if (this.IsDead)
                return;
            for (int index = 0; index < this.AuraEffectList.Count; ++index)
            {
                SkillEffectCtrl auraEffect = this.AuraEffectList[index];
                if ((UnityEngine.Object)auraEffect != (UnityEngine.Object)null)
                    auraEffect.SetActive(_active);
            }
        }

        /*public void StopAbnormalEffect()
        {
            foreach (KeyValuePair<UnitCtrl.eAbnormalStateCategory, AbnormalStateCategoryData> stateCategoryData in this.abnormalStateCategoryDataDictionary)
            {
                if (this.IsAbnormalState(stateCategoryData.Key))
                {
                    for (int index = 0; index < stateCategoryData.Value.Effects.Count; ++index)
                    {
                        AbnormalStateEffectGameObject effect = stateCategoryData.Value.Effects[index];
                        if ((UnityEngine.Object)effect.LeftEffect != (UnityEngine.Object)null)
                            effect.LeftEffect.Pause();
                        if ((UnityEngine.Object)effect.RightEffect != (UnityEngine.Object)null)
                            effect.RightEffect.Pause();
                    }
                }
            }
        }*/

        /*public void PlayLevelUpAnimation()
        {
            this.ResetResultPos();
            this.soundManager.PlaySe(eSE.SYS_UNIT_LEVEL_UP_03);
            this.PlayAnimeNoOverlap(eSpineCharacterAnimeId.JOY_SHORT);
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Singleton<LCEGKJFKOPD>.Instance.LDKILMDOFCH, ExceptNGUIRoot.Transform);
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.position = this.transform.position;
        }*/

        public void ResetResultPos()
        {
            if ((double)this.resultMoveDistance == 0.0)
                return;
            Vector3 localPosition = this.transform.localPosition;
            localPosition.x -= this.resultMoveDistance;
            this.resultMoveDistance = 0.0f;
            this.transform.localPosition = localPosition;
        }

        public void DisplayFrontOfDialog(float _offset)
        {/*
            eBattleCategory jiliicmhlch = this.battleManager.BattleCategory;
            eBattleResult aeamkdhplkn = this.battleManager.battleResult;
            float _posY;
            switch (jiliicmhlch)
            {
                case eBattleCategory.QUEST:
                case eBattleCategory.TRAINING:
                case eBattleCategory.HATSUNE_BATTLE:
                    _posY = 5000f;
                    break;
                case eBattleCategory.CLAN_BATTLE:
                    int resultBossPositionY1 = (int)ManagerSingleton<MasterDataManager>.Instance.masterClanBattle2BossData.Get(ClanBattleUtil.GetCurrentBossId(Singleton<ClanBattleTempData>.Instance.BossOrderNum)).result_boss_position_y;
                    this.SetSortOrderBack();
                    _posY = aeamkdhplkn == eBattleResult.WIN ? 5140f : 5000f + (float)resultBossPositionY1;
                    break;
                case eBattleCategory.HATSUNE_BOSS_BATTLE:
                case eBattleCategory.HATSUNE_SPECIAL_BATTLE:
                    EHPLBCOOOPK.HGIEGEENPJO debfkndecin = Singleton<EHPLBCOOOPK>.Instance.DEBFKNDECIN;
                    MasterHatsuneBoss.HatsuneBoss hatsuneBoss = debfkndecin.CHIKPEPKEAE[debfkndecin.GFADDFNBDAJ];
                    _posY = aeamkdhplkn == eBattleResult.WIN ? 5140f : 5000f + (float)(int)hatsuneBoss.result_boss_position_y;
                    BattleSpineController currentSpineCtrl1 = this.GetCurrentSpineCtrl();
                    if (currentSpineCtrl1.IsAnimation(eSpineCharacterAnimeId.IDLE_RESULT))
                    {
                        currentSpineCtrl1.PlayAnime(eSpineCharacterAnimeId.IDLE_RESULT);
                        break;
                    }
                    break;
                case eBattleCategory.GLOBAL_RAID:
                    MasterDataManager instance1 = ManagerSingleton<MasterDataManager>.Instance;
                    int resultBossPositionY2 = (int)instance1.masterSekaiBossMode.Get((int)instance1.masterSekaiTopData.Get(Singleton<LBFILPINNPJ>.Instance.AJCFOCJFCPE).sekai_boss_mode_id).result_boss_position_y;
                    this.SetSortOrderBack();
                    _posY = aeamkdhplkn == eBattleResult.WIN ? 5140f : 5000f + (float)resultBossPositionY2;
                    break;
                case eBattleCategory.KAISER_BATTLE_MAIN:
                    _posY = 5000f + (float)(double)ManagerSingleton<MasterDataManager>.Instance.masterKaiserQuestData.Get(2001).result_boss_position_y;
                    BattleSpineController currentSpineCtrl2 = this.GetCurrentSpineCtrl();
                    if (currentSpineCtrl2.IsAnimation(eSpineCharacterAnimeId.IDLE_RESULT))
                    {
                        currentSpineCtrl2.PlayAnime(eSpineCharacterAnimeId.IDLE_RESULT);
                        break;
                    }
                    break;
                case eBattleCategory.KAISER_BATTLE_SUB:
                    _posY = 5000f + (float)(double)ManagerSingleton<MasterDataManager>.Instance.masterKaiserQuestData.Get(Singleton<OIDMCPKDDDM>.Instance.OIPDBCEJAPK).result_boss_position_y;
                    break;
                default:
                    _posY = 5140f;
                    break;
            }
            this.transform.SetLocalPosY(this.transform.localPosition.y - 5000f);
            this.scaleValue = this.battleCameraEffect.orthographicSize;
            if (jiliicmhlch != eBattleCategory.QUEST && jiliicmhlch != eBattleCategory.TRAINING && jiliicmhlch != eBattleCategory.HATSUNE_BATTLE)
                this.scaleValue *= 0.875f;
            this.transform.localScale *= 1f / this.scaleValue;
            this.transform.position *= 1f / this.scaleValue;
            ViewManager instance2 = ManagerSingleton<ViewManager>.Instance;
            if (WrapperUnityEngineScreen.IsLongerThanBaseWidth())
            {
                _posY -= WrapperUnityEngineScreen.GetGameSafeArea().y;
                this.gameObject.transform.localScale *= (float)(1.77777779102325 / ((double)instance2.ActiveScreenWidthInUIRoot / (double)instance2.ActiveScreenHeightInUIRoot));
            }
            this.gameObject.transform.ChangeChildObjectLayer(LayerDefine.LAYER_UI);
            if (this.battleManager.IsAuraRemainBattle())
            {
                foreach (SkillEffectCtrl auraEffect in this.AuraEffectList)
                    auraEffect.SetLayerUI();
            }
            this.StartCoroutine(this.updateZoom(this.transform.localPosition, _offset, _posY));
            */
        }

        /*private IEnumerator updateZoom(Vector3 _startpos, float _offset, float _posY)
        {
            UnitCtrl unitCtrl = this;
            CustomEasing easing = new CustomEasing(CustomEasing.eType.outQuad, 0.0f, 1f, 0.3f);
            CustomEasing easingX = new CustomEasing(CustomEasing.eType.outQuad, _startpos.x, (float)((double)_offset * 220.0 * 1.5 * 1.14999997615814) / unitCtrl.battleCameraEffect.orthographicSize, 0.3f);
            CustomEasing easingY = new CustomEasing(CustomEasing.eType.outQuad, _startpos.y, -189.75f / unitCtrl.battleCameraEffect.orthographicSize - _posY, 0.3f);
            while (easing.IsMoving)
            {
                float curVal = easing.GetCurVal(Time.deltaTime);
                unitCtrl.transform.localScale = Vector3.one / unitCtrl.scaleValue * (float)(1.0 + (double)curVal * 0.100000001490116);
                unitCtrl.transform.localPosition = new Vector3(easingX.GetCurVal(Time.deltaTime), easingY.GetCurVal(Time.deltaTime));
                yield return (object)null;
            }
        }*/

        public void RunOutBattleResult()
        {
            this.SetLeftDirection(false);
            this.PlayAnime(eSpineCharacterAnimeId.RUN, this.MotionPrefix);
            this.StartCoroutine(this.updateRunOut());
        }

        private IEnumerator updateRunOut()
        {
            UnitCtrl unitCtrl = this;
            while (true)
            {
                unitCtrl.transform.SetLocalPosX(unitCtrl.transform.localPosition.x + 2400f * Time.deltaTime);
                if ((double)unitCtrl.transform.localPosition.x <= 1550.0)
                    yield return (object)null;
                else
                    break;
            }
        }

        public void PlaySetResult(ResultMotionInfo _combineMotion)
        {
            this.resultMoveDistance = 0.0f;
            this.PlayAnime(eSpineCharacterAnimeId.COMBINE_JOY_RESULT, _combineMotion.MotionId, _isLoop: false);
            this.SortOrder = _combineMotion.Depth;
            //this.GetCurrentSpineCtrl().SetAnimeEventDelegate(new System.Action<Spine.Event>(this.onMoveEventCallback));
        }

        /*private void onMoveEventCallback(Spine.Event _event)
        {
            if (!_event.Data.Name.Contains("move"))
                return;
            float num1 = (float)_event.Int;
            float _duration = _event.Float * this.battleManager.DeltaTime_60fps;
            if ((double)num1 == 0.0 || (double)_duration == 0.0)
                return;
            float num2 = this.battleCameraEffect.orthographicSize / 1.15f;
            float _distance = num1 / num2;
            switch (this.battleManager.BattleCategory)
            {
                case eBattleCategory.QUEST:
                case eBattleCategory.TRAINING:
                case eBattleCategory.HATSUNE_BATTLE:
                    this.resultMoveDistance += _distance;
                    if (this.battleManager.BattleCategory == eBattleCategory.FRIEND)
                    {
                        this.AppendCoroutine(this.updateSetResultMove(_distance, _duration), ePauseType.NO_DIALOG);
                        break;
                    }
                    this.AppendCoroutine(this.updateSetResultMove(_distance, _duration), ePauseType.SYSTEM, this);
                    break;
                case eBattleCategory.GRAND_ARENA:
                case eBattleCategory.GRAND_ARENA_REPLAY:
                    float num3 = _distance * num2 * 0.7378129f;
                    float num4 = (float)((double)this.BodyWidth * ((double)this.transform.localScale.x - 0.956521809101105) / 2.0);
                    if ((double)num3 > 0.0)
                    {
                        _distance = num3 - num4;
                        if ((double)_distance < 0.0)
                        {
                            _distance = 0.0f;
                            goto case eBattleCategory.QUEST;
                        }
                        else
                            goto case eBattleCategory.QUEST;
                    }
                    else
                    {
                        _distance = num3 + num4;
                        if ((double)_distance > 0.0)
                        {
                            _distance = 0.0f;
                            goto case eBattleCategory.QUEST;
                        }
                        else
                            goto case eBattleCategory.QUEST;
                    }
                default:
                    float num5 = (float)((double)this.BodyWidth * 0.125 * (double)(1.1f / this.scaleValue) / 2.0);
                    if ((double)_distance > 0.0)
                    {
                        _distance -= num5;
                        if ((double)_distance < 0.0)
                        {
                            _distance = 0.0f;
                            goto case eBattleCategory.QUEST;
                        }
                        else
                            goto case eBattleCategory.QUEST;
                    }
                    else
                    {
                        _distance += num5;
                        if ((double)_distance > 0.0)
                        {
                            _distance = 0.0f;
                            goto case eBattleCategory.QUEST;
                        }
                        else
                            goto case eBattleCategory.QUEST;
                    }
            }
        }*/

        /*private IEnumerator updateSetResultMove(float _distance, float _duration)
        {
            UnitCtrl unitCtrl = this;
            float num = _distance / _duration;
            float deltaTime = unitCtrl.battleManager.DeltaTime_60fps;
            float deltaPosX = num * deltaTime;
            float remainTime = _duration;
            float realMovedDistance = 0.0f;
            while ((double)remainTime > 0.0)
            {
                Vector3 localPosition = unitCtrl.transform.localPosition;
                localPosition.x += deltaPosX;
                unitCtrl.transform.localPosition = localPosition;
                realMovedDistance += deltaPosX;
                remainTime -= deltaTime;
                yield return (object)null;
            }
            Vector3 localPosition1 = unitCtrl.transform.localPosition;
            localPosition1.x += _distance - realMovedDistance;
            unitCtrl.transform.localPosition = localPosition1;
        }*/

        public void ForceFadeOut() => this.StartCoroutine(this.fadeOutCoroutine());

        private IEnumerator fadeOutCoroutine()
        {
            UnitCtrl _self = this;
            BattleSpineController battleSpineController = _self.GetCurrentSpineCtrl();
            battleSpineController.Resume();
            float fAlpha = 1;// battleSpineController.CurColor.a;
            while (true)
            {
                fAlpha -= Time.deltaTime;
                if ((double)fAlpha > 0.0 && !BattleUtil.Approximately(fAlpha, 0.0f))
                {
                   // battleSpineController.CurColor = new Color(1f, 1f, 1f, fAlpha);
                    yield return (object)null;
                }
                else
                    break;
            }
            _self.SetActive(false);
        }

        private int buffDebuffIndex { get; set; }

        private int clearedBuffIndex { get; set; }

        public int ClearedBuffFieldIndex { get; private set; }

        public int ClearedDebuffFieldIndex { get; private set; }

        private int clearedDebuffIndex { get; set; }

        private int buffCounter { get; set; }

        private int debuffCounter { get; set; }

        private List<SkillEffectCtrl> buffDebuffEffectList { get; set; } = new List<SkillEffectCtrl>();

        public Dictionary<UnitCtrl.BuffParamKind, FloatWithEx> AdditionalBuffDictionary { get; set; } = new Dictionary<UnitCtrl.BuffParamKind, FloatWithEx>();

        private FloatWithEx getAdditionalBuffDictionary(UnitCtrl.BuffParamKind _kind)
        {
            FloatWithEx num;
            return (int)(!this.AdditionalBuffDictionary.TryGetValue(_kind, out num) ? 0 : num);
        }

        public void SetBuffParam(
          UnitCtrl.BuffParamKind _kind,
          Dictionary<BasePartsData, FloatWithEx> _value,
          float _time,
          int _skillId,
          UnitCtrl _source,
          bool _despelable,
          eEffectType _effectType,
          bool _isBuff,
          bool _additional,
          Action<string> action = null)
        {
            if (!_isBuff && this.IsAbnormalState(UnitCtrl.eAbnormalState.NO_DEBUF))
            {
                this.SetMissAtk(_source, eMissLogType.DODGE_BY_NO_DAMAGE_MOTION);
                action?.Invoke("MISS");
            }
            else
            {
                if (_effectType == eEffectType.COMMON)
                { }  //this.CreateBuffDebuffEffect(_skillId, _isBuff, _source);
                string valueStr = "";
                foreach(int i in _value.Values)
                {
                    valueStr += i + ",";
                }
                action?.Invoke("对目标添加" + _kind.GetDescription() + (_isBuff?"BUFF":"DEBUFF") + ",值"+valueStr + "持续时间" + _time + "秒");
                IEnumerator _cr = this.UpdateBuffParam(_kind, _value, _time, _skillId, _source, _despelable, ++this.buffDebuffIndex, _isBuff, _additional);
                if (!_cr.MoveNext())
                    return;
                this.AppendCoroutine(_cr, ePauseType.SYSTEM);
            }
        }

        private IEnumerator UpdateBuffParam(
          UnitCtrl.BuffParamKind _kind,
          Dictionary<BasePartsData, FloatWithEx> _value,
          float _maxTime,
          int _skillId,
          UnitCtrl _source,
          bool _despelable,
          int _buffDebuffId,
          bool _isBuff,
          bool _additional)
        {
            this.EnableBuffParam(_kind, _value, true, _source, _isBuff, _additional,_maxTime);
            float time = 0.0f;
            while (true)
            {
                time += this.DeltaTimeForPause;
                if ((double)time > 1.0)
                    this.buffDebuffSkilIds.Remove(_skillId);
                bool flag = _despelable & _isBuff ? _buffDebuffId <= this.clearedBuffIndex : _buffDebuffId <= this.clearedDebuffIndex;
                if ((((double)time >= (double)_maxTime || this.IdleOnly ? 1 : ((long)this.Hp <= 0L ? 1 : 0)) | (flag ? 1 : 0)) == 0)
                    yield return (object)null;
                else
                    break;
            }
            this.buffDebuffSkilIds.Remove(_skillId);
            this.EnableBuffParam(_kind, _value, false, _source, _isBuff, _additional,_maxTime);
        }

        public void DespeleBuffDebuff(bool _isBuff, AbnormalStateEffectPrefabData _prefabData)
        {
            //this.createBuffDebuffClearEffect(_isBuff, _prefabData);
            if (_isBuff)
            {
                this.clearedBuffIndex = this.buffDebuffIndex;
                this.ClearedBuffFieldIndex = this.battleManager.MCLFFJEFMIF;
            }
            else
            {
                this.clearedDebuffIndex = this.buffDebuffIndex;
                this.ClearedDebuffFieldIndex = this.battleManager.MCLFFJEFMIF;
            }
        }

        /*private void createBuffDebuffClearEffect(
          bool _isBuff,
          AbnormalStateEffectPrefabData _prefabData)
        {
            if (_prefabData == null || _isBuff && this.buffCounter == 0 || !_isBuff && this.debuffCounter == 0)
                return;
            LCEGKJFKOPD instance = Singleton<LCEGKJFKOPD>.Instance;
            SkillEffectCtrl effect = this.battleEffectPool.GetEffect(this.IsLeftDir ? _prefabData.LeftEffectPrefab : _prefabData.RightEffectPrefab);
            effect.InitializeSort();
            effect.SortTarget = this;
            effect.SetSortOrderBack();
            effect.ExecAppendCoroutine(this);
            if (this.gameObject.activeSelf)
            {
                if (_prefabData.IsHead)
                    this.StartCoroutine(effect.TrackTarget(this.GetFirstParts(true), Vector3.zero, bone: this.StateBone));
                else
                    this.StartCoroutine(effect.TrackTarget(this.GetFirstParts(true), this.FixedCenterPos));
            }
            effect.PlaySe(this.SoundUnitId, this.IsLeftDir);
        }*/

        /*private void CreateBuffDebuffEffect(int skillId, bool isBuff, UnitCtrl source)
        {
            if (this.buffDebuffSkilIds.Contains(skillId))
                return;
            GameObject MDOJNMEMHLN = isBuff ? Singleton<LCEGKJFKOPD>.Instance.DFEEFEPBKAH : Singleton<LCEGKJFKOPD>.Instance.FDKHFGOGIDN;
            GameObject gameObject = this.battleEffectPool.GetEffect(MDOJNMEMHLN).gameObject;
            gameObject.transform.localScale = MDOJNMEMHLN.transform.localScale;
            gameObject.transform.parent = ExceptNGUIRoot.Transform;
            SkillEffectCtrl component = gameObject.GetComponent<SkillEffectCtrl>();
            this.battleManager.StartCoroutine(component.TrackTarget(this.GetFirstParts(true), Vector3.zero, bone: this.CenterBone));
            component.InitializeSort();
            component.SortTarget = this;
            component.PlaySe(this.SoundUnitId, this.IsLeftDir);
            component.ExecAppendCoroutine(this);
            this.buffDebuffEffectList.Add(component);
            component.OnEffectEnd += (System.Action<SkillEffectCtrl>)(obj => this.buffDebuffEffectList.Remove(obj));
            this.battleManager.StartCoroutine(component.TrackTargetSort(this));
            this.buffDebuffSkilIds.Add(skillId);
        }*/

        public void EnableBuffParam(
          UnitCtrl.BuffParamKind _kind,
          Dictionary<BasePartsData, FloatWithEx> _value,
          bool _enable,
          UnitCtrl _source,
          bool _isBuff,
          bool _additional,
          float buffTime)
        {
            BuffDebuffConstData buffDebuff = new BuffDebuffConstData()
            {
                BuffIcon = eStateIconType.NONE,
                DebuffIcon = eStateIconType.NONE
            };

            if (BUFF_DEBUFF_ICON_DIC.TryGetValue(_kind, out var value0))
            {
                buffDebuff = value0;
            }
            else
                Debug.LogError("角色技能图标" + _kind.GetDescription() + "丢失！");
            eStateIconType IDAFJHFJKOL = _isBuff ? buffDebuff.BuffIcon : buffDebuff.DebuffIcon;
            this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, IDAFJHFJKOL, _enable);
            string des = "";
            int MainValue = 0;
            foreach(var value in _value.Values)
            {
                des += value;
                MainValue = (int)value;
            }
            ///added script
            PCRCaculator.Guild.UnitAbnormalStateChangeData stateChangeData = new PCRCaculator.Guild.UnitAbnormalStateChangeData();
            //stateChangeData.AbsorberValue = data.AbsorberValue;
            //stateChangeData.ActionId = data.ActionId;
            //stateChangeData.CurrentAbnormalState = data.CurrentAbnormalState;
            //stateChangeData.Duration = data.Duration;
            stateChangeData.enable = _enable;
            //stateChangeData.EnergyReduceRate = data.EnergyReduceRate;
            //stateChangeData.IsDamageRelease = data.IsDamageRelease;
            //stateChangeData.IsEnergyReduceMode = data.IsEnergyReduceMode;
            //stateChangeData.IsReleasedByDamage = data.IsReleasedByDamage;
            stateChangeData.MainValue = MainValue;
            //stateChangeData.SubValue = data.SubValue;
            //stateChangeData.SkillName = data.Skill.SkillName;
            //stateChangeData.SourceName = data.Source.UnitName;
            stateChangeData.isBuff = true;
            stateChangeData.BUFF_Type = (int)_kind;
            MyOnAbnormalStateChange?.Invoke(UnitId, stateChangeData, BattleHeaderController.CurrentFrameCount);
            this.MyOnChangeAbnormalState?.Invoke(this, IDAFJHFJKOL, _enable,buffTime,des);
            ///finish add
            if (IDAFJHFJKOL != eStateIconType.NONE)
            {
                if (_enable)
                {
                    if (_isBuff)
                    {
                        ++this.buffCounter;
                        this.execPassiveSeal(PassiveSealAction.ePassiveTiming.BUFF);
                    }
                    else
                    {
                        this.debuffCounterDictionary[_kind]++;
                        ++this.debuffCounter;
                    }
                }
                else if (_isBuff)
                {
                    --this.buffCounter;
                }
                else
                {
                    this.debuffCounterDictionary[_kind]--;
                    --this.debuffCounter;
                }
            }
            Dictionary<BasePartsData, FloatWithEx>.Enumerator enumerator = _value.GetEnumerator();
            while (enumerator.MoveNext())
                enumerator.Current.Key.SetBuffDebuff(_enable, enumerator.Current.Value, _kind, _source, this.battleLog, _additional);
        }

        private Color curColor { get; set; }

        //private Dictionary<ChangeColorEffect, Color> curColorChannel { get; set; }

        //public Dictionary<ChangeColorEffect, Color> curColorOffsetChannel { get; set; }

        /*public void SetCurColor(ChangeColorEffect _key, Color _color)
        {
            if (!this.curColorChannel.ContainsKey(_key))
                this.curColorChannel.Add(_key, _color);
            else
                this.curColorChannel[_key] = _color;
            this.updateCurColor();
        }*/

       /* private void updateCurColor()
        {
            Dictionary<ChangeColorEffect, Color>.Enumerator enumerator = this.curColorChannel.GetEnumerator();
            Color curColor = this.curColor;
            while (enumerator.MoveNext())
                curColor *= enumerator.Current.Value;
            this.GetCurrentSpineCtrl().CurColor = curColor;
        }*/

        /*public void SetCurColorOffset(ChangeColorEffect _key, Color _color)
        {
            if (!this.curColorOffsetChannel.ContainsKey(_key))
                this.curColorOffsetChannel.Add(_key, _color);
            else
                this.curColorOffsetChannel[_key] = _color;
            Dictionary<ChangeColorEffect, Color>.Enumerator enumerator = this.curColorOffsetChannel.GetEnumerator();
            Color color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            while (enumerator.MoveNext())
                color += enumerator.Current.Value;
            this.GetCurrentSpineCtrl().CurColorOffset = color;
        }*/

        public static void QuickSort<T>(List<T> _array, Func<T, T, int> _compare)
        {
            if (_array.Count == 0)
                return;
            UnitCtrl.FunctionalComparer<T> instance = UnitCtrl.FunctionalComparer<T>.Instance;
            instance.SetComparer(_compare);
            UnitCtrl.quickSortImpl<T>(_array, 0, _array.Count - 1, instance);
        }

        private static void swap<T>(List<T> _list, int i, int j)
        {
            T obj = _list[i];
            _list[i] = _list[j];
            _list[j] = obj;
        }

        private static void quickSortImpl<T>(
          List<T> _array,
          int _left,
          int _right,
          UnitCtrl.FunctionalComparer<T> _compare)
        {
            if (_left >= _right)
                return;
            int num1 = _left;
            int num2 = _right;
            int index = num1 + (num2 - num1) / 2;
            T obj = _array[index];
            while (true)
            {
                while (num1 >= _right || _compare.Compare(_array[num1], obj) >= 0)
                {
                    while (num2 > _left && _compare.Compare(obj, _array[num2]) < 0)
                        --num2;
                    if (num1 <= num2)
                    {
                        UnitCtrl.swap<T>(_array, num1, num2);
                        ++num1;
                        --num2;
                    }
                    else
                    {
                        if (_left < num2)
                            UnitCtrl.quickSortImpl<T>(_array, _left, num2, _compare);
                        if (num1 >= _right)
                            return;
                        UnitCtrl.quickSortImpl<T>(_array, num1, _right, _compare);
                        return;
                    }
                }
                ++num1;
            }
        }
        private float baseX;
        public float BaseX { get => baseX; set { baseX = value * 60; } }

        public int CompareLeft(BasePartsData _a, BasePartsData _b) => _a.GetPosition().x.CompareTo(_b.GetPosition().x);

        public int CompareRight(BasePartsData _a, BasePartsData _b) => _b.GetPosition().x.CompareTo(_a.GetPosition().x);

        public static int CompareLifeAsc(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? ((float)(long)a.Owner.Hp / (float)(long)a.Owner.MaxHp).CompareTo((float)(long)b.Owner.Hp / (float)(long)b.Owner.MaxHp) : 1);

        public int CompareLifeAscNear(BasePartsData _a, BasePartsData _b) => BattleUtil.Approximately((float)(long)_a.Owner.Hp / (float)(long)_a.Owner.MaxHp, (float)(long)_b.Owner.Hp / (float)(long)_b.Owner.MaxHp) ? this.CompareDistanceAsc(_a, _b) : UnitCtrl.CompareLifeAsc(_a, _b);

        public int CompareLifeValueAscSameLeft(BasePartsData _a, BasePartsData _b) => (long)_a.Owner.Hp == (long)_b.Owner.Hp ? this.CompareLeft(_a, _b) : this.CompareLifeValueAsc(_a, _b);

        public int CompareLifeValueAscSameRight(BasePartsData _a, BasePartsData _b) => (long)_a.Owner.Hp == (long)_b.Owner.Hp ? this.CompareRight(_a, _b) : this.CompareLifeValueAsc(_a, _b);

        public int CompareLifeValueAsc(BasePartsData _a, BasePartsData _b) => _a == null ? (_b != null ? -1 : 0) : (_b != null ? _a.Owner.Hp.GetDecrypted().CompareTo(_b.Owner.Hp.GetDecrypted()) : 1);

        public static int CompareEnergyAsc(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? a.Owner.Energy.CompareTo(b.Owner.Energy) : 1);

        public int CompareEnergyAscNear(BasePartsData _a, BasePartsData _b) => BattleUtil.Approximately(_a.Owner.Energy, _b.Owner.Energy) ? this.CompareDistanceAsc(_a, _b) : UnitCtrl.CompareEnergyAsc(_a, _b);

        public static int CompareLifeDec(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? ((float)(long)b.Owner.Hp / (float)(long)b.Owner.MaxHp).CompareTo((float)(long)a.Owner.Hp / (float)(long)a.Owner.MaxHp) : -1);

        public int CompareLifeDecNear(BasePartsData _a, BasePartsData _b) => BattleUtil.Approximately((float)(long)_a.Owner.Hp / (float)(long)_a.Owner.MaxHp, (float)(long)_b.Owner.Hp / (float)(long)_b.Owner.MaxHp) ? this.CompareDistanceAsc(_a, _b) : UnitCtrl.CompareLifeDec(_a, _b);

        public int CompareLifeValueDecSameLeft(BasePartsData _a, BasePartsData _b) => (long)_a.Owner.Hp == (long)_b.Owner.Hp ? this.CompareLeft(_a, _b) : this.CompareLifeValueDec(_a, _b);

        public int CompareLifeValueDecSameRight(BasePartsData _a, BasePartsData _b) => (long)_a.Owner.Hp == (long)_b.Owner.Hp ? this.CompareRight(_a, _b) : this.CompareLifeValueDec(_a, _b);

        public int CompareLifeValueDec(BasePartsData _a, BasePartsData _b) => _a == null ? (_b != null ? -1 : 0) : (_b != null ? _b.Owner.Hp.GetDecrypted().CompareTo(_a.Owner.Hp.GetDecrypted()) : -1);

        public static int CompareEnergyDec(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? b.Owner.Energy.CompareTo(a.Owner.Energy) : -1);

        public int CompareEnergyDecNear(BasePartsData _a, BasePartsData _b) => BattleUtil.Approximately(_a.Owner.Energy, _b.Owner.Energy) ? this.CompareDistanceAsc(_a, _b) : UnitCtrl.CompareEnergyDec(_a, _b);

        public int CompareBoss(BasePartsData a, BasePartsData b)
        {
            bool flag1 = (UnityEngine.Object)a.Owner == (UnityEngine.Object)this;
            bool flag2 = (UnityEngine.Object)b.Owner == (UnityEngine.Object)this;
            if (flag1 | flag2)
                return flag1.CompareTo(flag2);
            return a.Owner.IsBoss == b.Owner.IsBoss ? this.CompareDistanceAsc(a, b) : b.Owner.IsBoss.CompareTo(a.Owner.IsBoss);
        }

        public int CompareShadow(BasePartsData _a, BasePartsData _b)
        {
            bool flag1 = (UnityEngine.Object)_a.Owner == (UnityEngine.Object)this;
            bool flag2 = (UnityEngine.Object)_b.Owner == (UnityEngine.Object)this;
            if (flag1 | flag2)
                return flag1.CompareTo(flag2);
            return _a.Owner.IsShadow == _b.Owner.IsShadow ? this.CompareDistanceAsc(_a, _b) : _b.Owner.IsShadow.CompareTo(_a.Owner.IsShadow);
        }

        public int CompareDistanceAsc(BasePartsData a, BasePartsData b)
        {
            float _a = a.GetBottomTransformPosition().x - this.BaseX;
            float _b = b.GetBottomTransformPosition().x - this.BaseX;
            return !BattleUtil.Approximately(_a, _b) ? Math.Abs(_a).CompareTo(Math.Abs(_b)) : a.Owner.UnitInstanceId.CompareTo(b.Owner.UnitInstanceId);
        }

        public int CompareDistanceDec(BasePartsData a, BasePartsData b)
        {
            float _a = a.GetBottomTransformPosition().x - this.BaseX;
            float _b = b.GetBottomTransformPosition().x - this.BaseX;
            return !BattleUtil.Approximately(_a, _b) ? Math.Abs(_b).CompareTo(Math.Abs(_a)) : a.Owner.UnitInstanceId.CompareTo(b.Owner.UnitInstanceId);
        }

        public static int CompareAtkAsc(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? a.GetAtkZero().CompareTo(b.GetAtkZero()) : 1);

        public static int CompareAtkDec(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? b.GetAtkZero().CompareTo(a.GetAtkZero()) : -1);

        public static int CompareMagicStrAsc(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? a.GetMagicStrZero().CompareTo(b.GetMagicStrZero()) : 1);

        public static int CompareMagicStrDec(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? b.GetMagicStrZero().CompareTo(a.GetMagicStrZero()) : -1);

        public int CompareAtkAscNear(BasePartsData _a, BasePartsData _b) => _a.GetAtkZero() == _b.GetAtkZero() ? this.CompareDistanceAsc(_a, _b) : UnitCtrl.CompareAtkAsc(_a, _b);

        public int CompareAtkDecNear(BasePartsData _a, BasePartsData _b) => _a.GetAtkZero() == _b.GetAtkZero() ? this.CompareDistanceAsc(_a, _b) : UnitCtrl.CompareAtkDec(_a, _b);

        public int CompareMagicStrAscNear(BasePartsData _a, BasePartsData _b) => _a.GetMagicStrZero() == _b.GetMagicStrZero() ? this.CompareDistanceAsc(_a, _b) : UnitCtrl.CompareMagicStrAsc(_a, _b);

        public int CompareMagicStrDecNear(BasePartsData _a, BasePartsData _b) => _a.GetMagicStrZero() == _b.GetMagicStrZero() ? this.CompareDistanceAsc(_a, _b) : UnitCtrl.CompareMagicStrDec(_a, _b);

        public List<Queue<int>> LifeStealQueueList { get; private set; }

        public Dictionary<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet> StrikeBackDictionary { get; private set; }

        public Dictionary<UnitCtrl, AccumulativeDamageData> AccumulativeDamageDataDictionary { get; private set; }

        public Dictionary<eStateIconType, SealData> SealDictionary { get; private set; }

        public List<UbAbnormalData> UbAbnormalDataList { get; private set; }

        //public eUbResponceVoiceType UbResponceVoiceType { get; set; }

        public int KnockBackEnableCount { get; set; }

        public Dictionary<int, int> SkillExecCountDictionary { get; set; }

        public bool IsStealth { get; set; }

        public bool IsTough { get; set; }

        public Dictionary<UnitCtrl, Dictionary<int, AttackSealData>> DamageSealDataDictionary { get; set; }

        public Dictionary<UnitCtrl, Dictionary<int, AttackSealData>> DamageOnceOwnerSealDateDictionary { get; set; } = new Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>();

        public Dictionary<UnitCtrl, Dictionary<int, AttackSealData>> DamageOwnerSealDataDictionary { get; set; } = new Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>();

        public System.Action OnActionByDamage { get; set; }

        public System.Action OnActionByCritical { get; set; }

        public System.Action OnActionByDamageOnce { get; set; }

        private List<AweAction.AweData> aweDatas { get; set; } = new List<AweAction.AweData>();

        private IEnumerator aweCoroutine { get; set; }

        public List<ToadData> ToadDatas { get; set; } = new List<ToadData>();

        private Dictionary<eStateIconType, List<KnightGuardData>> knightGuardDatas { get; set; } = new Dictionary<eStateIconType, List<KnightGuardData>>();

        private IEnumerator knightGuardCoroutine { get; set; }

        private Dictionary<PassiveSealAction.ePassiveTiming, List<PassiveSealData>> passiveSealDictionary { get; set; } = new Dictionary<PassiveSealAction.ePassiveTiming, List<PassiveSealData>>();

        private IEnumerator debuffDamageUpCoroutine { get; set; }

        private List<DebuffDamageUpData> debuffDamageUpDataList { get; set; } = new List<DebuffDamageUpData>();

        private Dictionary<UnitCtrl.BuffParamKind, int> debuffCounterDictionary { get; set; } = new Dictionary<UnitCtrl.BuffParamKind, int>()
    {
      {
        UnitCtrl.BuffParamKind.ATK,
        0
      },
      {
        UnitCtrl.BuffParamKind.DEF,
        0
      },
      {
        UnitCtrl.BuffParamKind.MAGIC_STR,
        0
      },
      {
        UnitCtrl.BuffParamKind.MAGIC_DEF,
        0
      },
      {
        UnitCtrl.BuffParamKind.DODGE,
        0
      },
      {
        UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL,
        0
      },
      {
        UnitCtrl.BuffParamKind.MAGIC_CRITICAL,
        0
      },
      {
        UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE,
        0
      },
      {
        UnitCtrl.BuffParamKind.LIFE_STEAL,
        0
      },
      {
        UnitCtrl.BuffParamKind.MOVE_SPEED,
        0
      },
      {
        UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE,
        0
      },
      {
        UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE,
        0
      },
      {
        UnitCtrl.BuffParamKind.ACCURACY,
        0
      },
      {
        UnitCtrl.BuffParamKind.MAX_HP,
        0
      }
    };


        public void AddAweData(AweAction.AweData _aweData)
        {
            if (this.aweDatas.Count == 0)
            {
                this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, eStateIconType.AWE, true);
                this.MyOnChangeAbnormalState?.Invoke(this, eStateIconType.AWE, true,_aweData.LifeTime,"awe");
            }
            this.aweDatas.Add(_aweData);
            if (this.aweCoroutine != null)
                return;
            this.aweCoroutine = this.updateAwe();
            this.AppendCoroutine(this.aweCoroutine, ePauseType.SYSTEM);
        }

        private IEnumerator updateAwe()
        {
            UnitCtrl JEOCPILJNAD = this;
            while (true)
            {
                while (JEOCPILJNAD.aweDatas.Count != 0)
                {
                    for (int index = JEOCPILJNAD.aweDatas.Count - 1; index >= 0; --index)
                    {
                        AweAction.AweData aweData = JEOCPILJNAD.aweDatas[index];
                        aweData.LifeTime -= JEOCPILJNAD.DeltaTimeForPause;
                        if ((double)aweData.LifeTime < 0.0)
                        {
                            //if ((UnityEngine.Object)aweData.Effect != (UnityEngine.Object)null)
                            //    aweData.Effect.SetTimeToDie(true);
                            JEOCPILJNAD.aweDatas.RemoveAt(index);
                        }
                    }
                    if (JEOCPILJNAD.aweDatas.Count == 0)
                    {
                        JEOCPILJNAD.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(JEOCPILJNAD, eStateIconType.AWE, false);
                        JEOCPILJNAD.MyOnChangeAbnormalState?.Invoke(JEOCPILJNAD, eStateIconType.AWE, false,0,"awe");
                    }
                    yield return (object)null;
                }
                yield return (object)null;
            }
        }

        public float CalcAweValue(bool _isUnionBurst, bool _isAttack)
        {
            float a = 1f;
            if (this.aweDatas.Count == 0)
                return a;
            for (int index = this.aweDatas.Count - 1; index >= 0; --index)
            {
                AweAction.AweData aweData = this.aweDatas[index];
                bool flag = false;
                switch (aweData.AweType)
                {
                    case AweAction.eAweType.UB_ONLY:
                        if (!_isAttack && _isUnionBurst)
                        {
                            flag = true;
                            break;
                        }
                        break;
                    case AweAction.eAweType.UB_AND_SKILL:
                        if (!_isAttack)
                        {
                            flag = true;
                            break;
                        }
                        break;
                }
                if (flag)
                {
                    a -= aweData.Value;
                    if (aweData.CountLimit > 0)
                    {
                        --aweData.CountLimit;
                        if (aweData.CountLimit == 0)
                        {
                            //if ((UnityEngine.Object)aweData.Effect != (UnityEngine.Object)null)
                            //    aweData.Effect.SetTimeToDie(true);
                            this.aweDatas.RemoveAt(index);
                        }
                    }
                }
            }
            if (this.aweDatas.Count == 0)
            {
                this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, eStateIconType.AWE, false);
                this.MyOnChangeAbnormalState?.Invoke(this, eStateIconType.AWE, false,90,"awe");
            }
            return Mathf.Max(a, 0.0f);
        }

        public void AddKnightGuard(KnightGuardData _knightGuardData)
        {
            List<KnightGuardData> knightGuardDataList = (List<KnightGuardData>)null;
            if (!this.knightGuardDatas.TryGetValue(_knightGuardData.StateIconType, out knightGuardDataList))
            {
                knightGuardDataList = new List<KnightGuardData>();
                this.knightGuardDatas.Add(_knightGuardData.StateIconType, knightGuardDataList);
            }
            if (knightGuardDataList.Count == 0)
            {
                this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, _knightGuardData.StateIconType, true);
                this.MyOnChangeAbnormalState?.Invoke(this, _knightGuardData.StateIconType, true,90,"???");
            }
            knightGuardDataList.Add(_knightGuardData);
            if (this.knightGuardCoroutine != null)
                return;
            this.knightGuardCoroutine = this.updateKnightGuard();
            this.AppendCoroutine(this.knightGuardCoroutine, ePauseType.SYSTEM);
        }

        public IEnumerator updateKnightGuard()
        {
            UnitCtrl JEOCPILJNAD = this;
            while (JEOCPILJNAD.knightGuardCoroutine != null)
            {
                foreach (KeyValuePair<eStateIconType, List<KnightGuardData>> knightGuardData1 in JEOCPILJNAD.knightGuardDatas)
                {
                    if (knightGuardData1.Value.Count != 0)
                    {
                        for (int index = knightGuardData1.Value.Count - 1; index >= 0; --index)
                        {
                            KnightGuardData knightGuardData2 = knightGuardData1.Value[index];
                            knightGuardData2.LifeTime -= JEOCPILJNAD.DeltaTimeForPause;
                            if ((double)knightGuardData2.LifeTime < 0.0)
                            {
                                knightGuardData1.Value.RemoveAt(index);
                                //if ((UnityEngine.Object)knightGuardData2.Effect != (UnityEngine.Object)null)
                                 //   knightGuardData2.Effect.SetTimeToDie(true);
                            }
                        }
                        if (knightGuardData1.Value.Count == 0)
                        {
                            JEOCPILJNAD.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(JEOCPILJNAD, knightGuardData1.Key, false);
                            JEOCPILJNAD.MyOnChangeAbnormalState?.Invoke(JEOCPILJNAD, knightGuardData1.Key, false,90,"???");
                        }
                    }
                }
                yield return (object)null;
            }
        }

        public bool ExecKnightGuard()
        {
            bool flag = false;
            foreach (KeyValuePair<eStateIconType, List<KnightGuardData>> knightGuardData1 in this.knightGuardDatas)
            {
                System.Action DMFGKJIEEBF = (System.Action)null;
                foreach (KnightGuardData knightGuardData2 in knightGuardData1.Value)
                {
                    KnightGuardData data = knightGuardData2;
                    flag = true;
                    DMFGKJIEEBF += (System.Action)(() => this.SetRecovery(data.Value, data.InhibitHealType, this, _isRevival: true));
                    if (data.ExecEffectData != null)
                    {
                        /*NormalSkillEffect execEffectData = data.ExecEffectData;
                        SkillEffectCtrl effect = this.battleEffectPool.GetEffect(this.IsLeftDir ? execEffectData.PrefabLeft : execEffectData.Prefab);
                        effect.transform.parent = ExceptNGUIRoot.Transform;
                        effect.SortTarget = this;
                        effect.InitializeSort();
                        effect.PlaySe(data.Source.SoundUnitId, data.Source.IsLeftDir);
                        effect.SetPossitionAppearanceType(execEffectData, this.GetFirstParts(), this, data.Skill);
                        effect.ExecAppendCoroutine();*/
                    }
                }
                knightGuardData1.Value.Clear();
                DMFGKJIEEBF.Call();
                this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, knightGuardData1.Key, false);
                MyOnChangeAbnormalState?.Invoke(this, knightGuardData1.Key, false, 90, "???");
            }
            return flag;
        }

        /*public void StartCountinuousAttackNearByAttack(
          ContinuousAttackNearByAction.ContinuousAttackNearByData _data)
        {
            if (_data.StateIconType != eStateIconType.NONE)
                this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, _data.StateIconType, true);
            if (_data.ReleaseType == ContinuousAttackNearByAction.eReleaseType.ENERGY)
                this.IsReduceEnergyDictionary[UnitCtrl.eReduceEnergyType.CONTINUOUS_DAMAGE_NEARBY] = true;
            this.AppendCoroutine(this.updateContinuousDamageNearBy(_data), ePauseType.SYSTEM, this);
        }*/

        /*public void FinishContinuousAttackNearByAttack(
          ContinuousAttackNearByAction.ContinuousAttackNearByData _data)
        {
            _data.Enable = false;
            if (_data.StateIconType != eStateIconType.NONE)
                this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, _data.StateIconType, false);
            if (_data.ReleaseType == ContinuousAttackNearByAction.eReleaseType.ENERGY)
                this.IsReduceEnergyDictionary[UnitCtrl.eReduceEnergyType.CONTINUOUS_DAMAGE_NEARBY] = false;
            if (!((UnityEngine.Object)_data.Effect != (UnityEngine.Object)null))
                return;
            _data.Effect.SetTimeToDie(true);
        }*/

        /*private IEnumerator updateContinuousDamageNearBy(
          ContinuousAttackNearByAction.ContinuousAttackNearByData _data)
        {
            UnitCtrl unitCtrl1 = this;
            float time = 0.0f;
            while (!unitCtrl1.IdleOnly && !unitCtrl1.IsDead)
            {
                yield return (object)null;
                switch (_data.ReleaseType)
                {
                    case ContinuousAttackNearByAction.eReleaseType.ENERGY:
                        unitCtrl1.SetEnergy(unitCtrl1.Energy - unitCtrl1.DeltaTimeForPause * _data.ReduceEnergy, eSetEnergyType.BY_MODE_CHANGE);
                        if ((double)unitCtrl1.Energy == 0.0 || unitCtrl1.IsDead)
                        {
                            unitCtrl1.FinishContinuousAttackNearByAttack(_data);
                            yield break;
                        }
                        else
                            break;
                    case ContinuousAttackNearByAction.eReleaseType.TIMER:
                        _data.Timer -= unitCtrl1.DeltaTimeForPause;
                        if ((double)_data.Timer < 0.0)
                        {
                            unitCtrl1.FinishContinuousAttackNearByAttack(_data);
                            yield break;
                        }
                        else
                            break;
                }
                time += unitCtrl1.DeltaTimeForPause;
                if ((double)time > 1.0)
                {
                    List<UnitCtrl> unitCtrlList = unitCtrl1.IsOther && !unitCtrl1.IsConfusionOrConvert() || !unitCtrl1.IsOther && unitCtrl1.IsConfusionOrConvert() ? unitCtrl1.battleManager.UnitList : unitCtrl1.battleManager.EnemyList;
                    int index = 0;
                    for (int count = unitCtrlList.Count; index < count; ++index)
                    {
                        UnitCtrl unitCtrl2 = unitCtrlList[index];
                        if (!((UnityEngine.Object)unitCtrl2 == (UnityEngine.Object)unitCtrl1) && !unitCtrl2.IsPhantom && !unitCtrl2.IsStealth)
                        {
                            System.Action<BasePartsData> action = (System.Action<BasePartsData>)(_parts =>
                           {
                               float _a = Mathf.Abs(_parts.GetLocalPosition().x - this.transform.localPosition.x);
                               float _b = (float)((double)_data.Size + (double)_parts.GetBodyWidth() / 2.0 + (double)this.BodyWidth / 2.0);
                               if ((double)_a > (double)_b && !BattleUtil.Approximately(_a, _b))
                                   return;
                               float num1 = _data.BaseDamage + _data.AttackEfficiency * (_data.AttackType == AttackActionBase.eAttackType.PHYSICAL ? (float)this.GetFirstParts().GetAtkZero() : (float)this.GetFirstParts().GetMagicStrZero());
                               DamageData damageData = new DamageData()
                               {
                                   Target = _parts,
                                   Damage = (long)(int)num1,
                                   DamageType = DamageData.eDamageType.NONE,
                                   Source = this,
                                   ActionType = eActionType.CONTINUOUS_ATTACK_NEARBY,
                                   ExecAbsorber = (Func<int, int>)(_damage =>
                     {
                                   if (_damage > _data.AbsorberValue)
                                   {
                                       int num2 = _damage - _data.AbsorberValue;
                                       this.battleManager.SubstructEnemyPoint(_data.AbsorberValue);
                                       _data.AbsorberValue = 0;
                                       return num2;
                                   }
                                   _data.AbsorberValue -= _damage;
                                   this.battleManager.SubstructEnemyPoint(_damage);
                                   return 0;
                               })
                               };
                               UnitCtrl owner = _parts.Owner;
                               DamageData _damageData = damageData;
                               Skill skill = _data.Skill;
                               int actionId = _data.ActionId;
                               Skill _skill = skill;
                               owner.SetDamage(_damageData, false, actionId, _skill: _skill);
                           });
                            if (unitCtrl2.IsPartsBoss)
                            {
                                foreach (PartsData partsData in unitCtrl2.BossPartsListForBattle)
                                    action((BasePartsData)partsData);
                            }
                            else
                                action(unitCtrl2.GetFirstParts());
                        }
                    }
                    time = 0.0f;
                }
            }
            unitCtrl1.FinishContinuousAttackNearByAttack(_data);
        }*/

        public void AddPassiveSeal(PassiveSealAction.ePassiveTiming _timing, PassiveSealData _data)
        {
            if (this.passiveSealDictionary.ContainsKey(_timing))
                this.passiveSealDictionary[_timing].Add(_data);
            else
                this.passiveSealDictionary.Add(_timing, new List<PassiveSealData>()
        {
          _data
        });
        }

        private void execPassiveSeal(PassiveSealAction.ePassiveTiming _timing)
        {
            List<PassiveSealData> passiveSealDataList = (List<PassiveSealData>)null;
            if (!this.passiveSealDictionary.TryGetValue(_timing, out passiveSealDataList))
                return;
            for (int index = passiveSealDataList.Count - 1; index >= 0; --index)
            {
                PassiveSealData passiveSealData = passiveSealDataList[index];
                if ((double)passiveSealData.LifeTime < 0.0)
                {
                    passiveSealDataList.RemoveAt(index);
                }
                else
                {
                    UnitCtrl unitCtrl = (UnitCtrl)null;
                    if (passiveSealData.SealTarget == PassiveSealAction.eSealTarget.SOURCE)
                        unitCtrl = passiveSealData.Source;
                    eStateIconType targetStateIcon = passiveSealData.TargetStateIcon;
                    if (!unitCtrl.SealDictionary.ContainsKey(targetStateIcon))
                    {
                        SealData sealData = new SealData()
                        {
                            Max = passiveSealData.SealNumLimit,
                            DisplayCount = passiveSealData.DisplayCount
                        };
                        unitCtrl.SealDictionary.Add(targetStateIcon, sealData);
                    }
                    else
                        unitCtrl.SealDictionary[targetStateIcon].Max = Mathf.Max(passiveSealData.SealNumLimit, unitCtrl.SealDictionary[targetStateIcon].Max);
                    SealData seal = unitCtrl.SealDictionary[targetStateIcon];
                    if (seal.GetCurrentCount() == 0)
                        unitCtrl.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(unitCtrl, targetStateIcon, true);
                    seal.AddSeal(passiveSealData.SealDuration, unitCtrl, targetStateIcon, 1);
                }
            }
        }

        public void AddDebuffDamageUpData(DebuffDamageUpData _debuffDamageUpData)
        {
            this.debuffDamageUpDataList.Add(_debuffDamageUpData);
            if (this.debuffDamageUpCoroutine != null)
                return;
            this.debuffDamageUpCoroutine = this.updateDebuffDamageUp();
            this.AppendCoroutine(this.debuffDamageUpCoroutine, ePauseType.SYSTEM);
        }

        private IEnumerator updateDebuffDamageUp()
        {
            while (true)
            {
                for (int index = this.debuffDamageUpDataList.Count - 1; index >= 0; --index)
                {
                    DebuffDamageUpData debuffDamageUpData = this.debuffDamageUpDataList[index];
                    debuffDamageUpData.DebuffDamageUpTimer -= this.DeltaTimeForPause;
                    if ((double)debuffDamageUpData.DebuffDamageUpTimer < 0.0)
                        this.debuffDamageUpDataList.RemoveAt(index);
                }
                yield return (object)null;
            }
        }

        public float GetDebuffDamageUpValue()
        {
            if (this.debuffDamageUpDataList.Count == 0)
                return 1f;
            int num1 = 0;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.SLOW))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.PARALYSIS))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.FREEZE))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.CHAINED))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.SLEEP))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.STUN))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.STONE))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.DETAIN))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.FAINT))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.PAUSE_ACTION))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.NPC_STUN))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.NO_EFFECT_SLIP_DAMAGE))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.POISON))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.BURN))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.CURSE))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.VENOM))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.HEX))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.COMPENSATION))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.CONVERT))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.CONFUSION))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.MAGIC_DARK))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.PHYSICS_DARK))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.UB_SILENCE))
                ++num1;
            if (this.UbAbnormalDataList.Count > 0)
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.COUNT_BLIND))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.INHIBIT_HEAL))
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.FEAR))
                ++num1;
            if (this.aweDatas.Count > 0)
                ++num1;
            if (this.ToadDatas.Count > 0)
                ++num1;
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.HEAL_DOWN))
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.ATK] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.DEF] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.MAGIC_STR] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.MAGIC_DEF] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.DODGE] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.MAGIC_CRITICAL] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.LIFE_STEAL] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.MOVE_SPEED] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.ACCURACY] > 0)
                ++num1;
            if (this.debuffCounterDictionary[UnitCtrl.BuffParamKind.MAX_HP] > 0)
                ++num1;
            float num2 = 1f;
            foreach (DebuffDamageUpData debuffDamageUpData in this.debuffDamageUpDataList)
            {
                float num3 = 0.0f;
                switch (debuffDamageUpData.EffectType)
                {
                    case DebuffDamageUpData.eEffectType.ADD:
                        num3 = Math.Min((float)(1.0 + (double)debuffDamageUpData.DebuffDamageUpValue * (double)num1), debuffDamageUpData.DebuffDamageUpLimitValue);
                        break;
                    case DebuffDamageUpData.eEffectType.SUBTRACT:
                        num3 = Mathf.Max((float)(1.0 - (double)debuffDamageUpData.DebuffDamageUpValue * (double)num1), debuffDamageUpData.DebuffDamageUpLimitValue);
                        break;
                }
                num2 *= num3;
            }
            return num2;
        }



        public Dictionary<eParamType, PassiveActionValue> OwnerPassiveAction { get; private set; }

        public static void InitializeExAndFreeSkill(
          UnitData _unitData,
          ePassiveInitType _passiveInitType,
          int _waveNumber,
          UnitCtrl _unit)
        {
            UnitCtrl.initializePassiveSkillList(_unitData.ExSkill, _passiveInitType, _waveNumber, _unit);
            UnitCtrl.initializePassiveSkillList(_unitData.FreeSkill, _passiveInitType, _waveNumber, _unit);
            UnitCtrl.initializePassiveSkillList(_unitData.MainSkill, _passiveInitType, _waveNumber, _unit);
        }

        private static void initializePassiveSkillList(
          List<SkillLevelInfo> _skillLevelInfoList,
          ePassiveInitType _passiveInitType,
          int _waveNumber,
          UnitCtrl _unit)
        {
            int index = 0;
            for (int count = _skillLevelInfoList.Count; index < count; ++index)
                UnitCtrl.initializePassiveSkill(_skillLevelInfoList[index], _passiveInitType, _waveNumber, _unit);
        }

        private static void initializePassiveSkill(
          SkillLevelInfo _skillLevelInfo,
          ePassiveInitType _passiveInitType,
          int _waveNumber,
          UnitCtrl _unit)
        {
            /*if ((int)_skillLevelInfo.SkillId == 0)
                return;
            MasterSkillData.SkillData skillData = ManagerSingleton<MasterDataManager>.Instance.masterSkillData[(int)_skillLevelInfo.SkillId];
            int index = 0;
            for (int count = skillData.ActionIds.Count; index < count; ++index)
                UnitCtrl.initializePassiveAction(skillData.ActionIds[index], (int)_skillLevelInfo.SkillLevel, _passiveInitType, _waveNumber, _unit);*/
        }

        private static void initializePassiveAction(
          int _actionId,
          int _level,
          ePassiveInitType _passiveInitType,
          int _waveNumber,
          UnitCtrl _unit)
        {
            /*if (_actionId == 0)
                return;
            MasterSkillAction.SkillAction _actionData = ManagerSingleton<MasterDataManager>.Instance.masterSkillAction[_actionId];
            if ((byte)_actionData.action_type != (byte)90)
                return;
            switch (_passiveInitType)
            {
                case ePassiveInitType.ALL_BY_ENEMY:
                    UnitCtrl.initializePassiveActionAllByEnemy(_actionData, _level, _waveNumber);
                    break;
                case ePassiveInitType.ALL_BY_UNIT:
                    UnitCtrl.initializePassiveActionAllByUnit(_actionData, _level, _waveNumber);
                    break;
                case ePassiveInitType.OWNER:
                    UnitCtrl.initializePassiveActionOwner(_actionData, _level, _unit);
                    break;
            }*/
        }

        private static void initializePassiveActionAllByEnemy(
          MasterSkillAction.SkillAction _actionData,
          int _level,
          int _waveNumber)
        {
            switch ((UnitCtrl.ePassiveSideType)(int)_actionData.target_assignment)
            {
                case UnitCtrl.ePassiveSideType.ALL:
                    UnitCtrl.appendPassiveSkill(UnitCtrl.staticBattleManager.PassiveDic_4[_waveNumber], _actionData, _level);
                    break;
                case UnitCtrl.ePassiveSideType.ALL_OTHER:
                    if ((int)_actionData.action_detail_1 == 1)
                        break;
                    UnitCtrl.appendPassiveSkill(UnitCtrl.staticBattleManager.PassiveDic_3[_waveNumber], _actionData, _level);
                    break;
            }
        }

        private static void initializePassiveActionAllByUnit(
          MasterSkillAction.SkillAction _actionData,
          int _level,
          int _waveNumber)
        {
            switch ((UnitCtrl.ePassiveSideType)(int)_actionData.target_assignment)
            {
                case UnitCtrl.ePassiveSideType.ALL:
                    UnitCtrl.appendPassiveSkill(UnitCtrl.staticBattleManager.PassiveDic_1[_waveNumber], _actionData, _level);
                    break;
                case UnitCtrl.ePassiveSideType.ALL_OTHER:
                    if ((int)_actionData.action_detail_1 == 1)
                        break;
                    UnitCtrl.appendPassiveSkill(UnitCtrl.staticBattleManager.PassiveDic_2[_waveNumber], _actionData, _level);
                    break;
            }
        }

        private static void initializePassiveActionOwner(
          MasterSkillAction.SkillAction _actionData,
          int _level,
          UnitCtrl _unit)
        {
            UnitCtrl.ePassiveSideType targetAssignment = (UnitCtrl.ePassiveSideType)(int)_actionData.target_assignment;
            if (targetAssignment != UnitCtrl.ePassiveSideType.OWNER)
            {
                int num = (int)(targetAssignment - 1);
            }
            else
            {
                if (!((UnityEngine.Object)_unit != (UnityEngine.Object)null))
                    return;
                UnitCtrl.appendPassiveSkill(_unit.OwnerPassiveAction, _actionData, _level);
            }
        }

        private static void appendPassiveSkill(
          Dictionary<eParamType, PassiveActionValue> _target,
          MasterSkillAction.SkillAction _actionData,
          int _level)
        {
            eParamType actionDetail1 = (eParamType)(int)_actionData.action_detail_1;
            PassiveActionValue passiveActionValue;
            bool flag = _target.TryGetValue(actionDetail1, out passiveActionValue);
            if (!flag)
                passiveActionValue = new PassiveActionValue();
            switch ((ePassiveSkillValueType)(double)_actionData.action_value_1)
            {
                case ePassiveSkillValueType.VALUE:
                    passiveActionValue.Value += UnitCtrl.calculateValue(_actionData, _level);
                    break;
                case ePassiveSkillValueType.PERCENTAGE:
                    passiveActionValue.Percentage += UnitCtrl.calculateValue(_actionData, _level);
                    break;
            }
            if (!flag)
                _target.Add(actionDetail1, passiveActionValue);
            else
                _target[actionDetail1] = passiveActionValue;
        }

        private static float calculateValue(MasterSkillAction.SkillAction _actionData, int _level)
        {
            float num = 0.0f;
            if (_level == 0)
                return 0.0f;
            switch ((ePassiveBuffDebuff)(double)_actionData.action_value_4)
            {
                case ePassiveBuffDebuff.BUFF:
                    num = (float)((double)_actionData.action_value_2 + (double)_actionData.action_value_3 * (double)_level);
                    break;
                case ePassiveBuffDebuff.DEBUFF:
                    num = -(float)((double)_actionData.action_value_2 + (double)_actionData.action_value_3 * (double)_level);
                    break;
            }
            return num;
        }

        public void ApplyPassiveSkillValue(bool _first)
        {
            if (this.IsSummonOrPhantom)
                return;
            int pokeaebgpib = this.battleManager.CurrentWave;
            int index = 0;
            foreach (KeyValuePair<eParamType, PassiveActionValue> _passiveActionKV in this.addPassiveValue(this.addPassiveValue(this.IsOther ? this.battleManager.PassiveDic_4[pokeaebgpib] : this.battleManager.PassiveDic_3[pokeaebgpib], this.IsOther ? this.battleManager.PassiveDic_2[index] : this.battleManager.PassiveDic_1[index]), this.OwnerPassiveAction))
            {
                switch (_passiveActionKV.Key)
                {
                    case eParamType.HP:
                        if (_first && !this.IsClanBattleOrSekaiEnemy)
                        {
                            this.MaxHp = (ObscuredLong)(long)(int)this.calculatePassiveSkillValue((float)(long)this.MaxHp, _passiveActionKV);
                            this.Hp = (ObscuredLong)(long)(int)((double)(long)this.Hp / (double)(long)this.StartMaxHP * (double)(long)this.MaxHp);
                            continue;
                        }
                        continue;
                    case eParamType.ATK:
                        this.Atk = this.calculatePassiveSkillValue((float)(int)this.StartAtk, _passiveActionKV);
                        continue;
                    case eParamType.DEF:
                        this.Def = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartDef, _passiveActionKV);
                        continue;
                    case eParamType.MAGIC_ATK:
                        this.MagicStr = this.calculatePassiveSkillValue((float)(int)this.StartMagicStr, _passiveActionKV);
                        continue;
                    case eParamType.MAGIC_DEF:
                        this.MagicDef = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartMagicDef, _passiveActionKV);
                        continue;
                    case eParamType.PHYSICAL_CRITICAL:
                        this.PhysicalCritical = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartPhysicalCritical, _passiveActionKV);
                        continue;
                    case eParamType.MAGIC_CRITICAL:
                        this.MagicCritical = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartMagicCritical, _passiveActionKV);
                        continue;
                    case eParamType.DODGE:
                        this.Dodge = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartDodge, _passiveActionKV);
                        continue;
                    case eParamType.LIFE_STEAL:
                        this.LifeSteal = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartLifeSteal, _passiveActionKV);
                        continue;
                    case eParamType.WAVE_HP_RECOVERY:
                        this.WaveHpRecovery = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartWaveHpRecovery, _passiveActionKV);
                        continue;
                    case eParamType.WAVE_ENERGY_RECOVERY:
                        this.WaveEnergyRecovery = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartWaveEnergyRecovery, _passiveActionKV);
                        continue;
                    case eParamType.PHYSICAL_PENETRATE:
                        this.PhysicalPenetrate = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartPhysicalPenetrate, _passiveActionKV);
                        continue;
                    case eParamType.MAGIC_PENETRATE:
                        this.MagicPenetrate = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartMagicPenetrate, _passiveActionKV);
                        continue;
                    case eParamType.ENERGY_RECOVERY_RATE:
                        this.EnergyRecoveryRate = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartEnergyRecoveryRate, _passiveActionKV);
                        continue;
                    case eParamType.HP_RECOVERY_RATE:
                        this.HpRecoveryRate = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartHpRecoveryRate, _passiveActionKV);
                        continue;
                    case eParamType.ENERGY_REDUCE_RATE:
                        this.EnergyReduceRate = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartEnergyReduceRate, _passiveActionKV);
                        continue;
                    case eParamType.ACCURACY:
                        this.Accuracy = (ObscuredInt)(int)this.calculatePassiveSkillValue((float)(int)this.StartAccuracy, _passiveActionKV);
                        continue;
                    default:
                        continue;
                }
            }
        }

        private Dictionary<eParamType, PassiveActionValue> addPassiveValue(
          Dictionary<eParamType, PassiveActionValue> _dic1,
          Dictionary<eParamType, PassiveActionValue> _dic2)
        {
            Dictionary<eParamType, PassiveActionValue> dictionary = new Dictionary<eParamType, PassiveActionValue>((IDictionary<eParamType, PassiveActionValue>)_dic1, (IEqualityComparer<eParamType>)new eParamType_DictComparer());
            foreach (KeyValuePair<eParamType, PassiveActionValue> keyValuePair in _dic2)
            {
                PassiveActionValue passiveActionValue1;
                int num1 = dictionary.TryGetValue(keyValuePair.Key, out passiveActionValue1) ? 1 : 0;
                if (num1 == 0)
                    passiveActionValue1 = new PassiveActionValue();
                ref PassiveActionValue local1 = ref passiveActionValue1;
                double num2 = (double)local1.Value;
                PassiveActionValue passiveActionValue2 = keyValuePair.Value;
                double num3 = (double)passiveActionValue2.Value;
                local1.Value = (float)(num2 + num3);
                ref PassiveActionValue local2 = ref passiveActionValue1;
                double percentage1 = (double)local2.Percentage;
                passiveActionValue2 = keyValuePair.Value;
                double percentage2 = (double)passiveActionValue2.Percentage;
                local2.Percentage = (float)(percentage1 + percentage2);
                if (num1 == 0)
                    dictionary.Add(keyValuePair.Key, passiveActionValue1);
                else
                    dictionary[keyValuePair.Key] = passiveActionValue1;
            }
            return dictionary;
        }

        private FloatWithEx calculatePassiveSkillValue(
            FloatWithEx _value,
          KeyValuePair<eParamType, PassiveActionValue> _passiveActionKV) => (float)((double)_value * ((double)_passiveActionKV.Value.Percentage + 100.0) / 100.0) + _passiveActionKV.Value.Value;

        public List<PartsData> BossPartsListForBattle { get; set; }

        public bool IsPartsBoss => this.BossPartsList.Count > 0;

        public BasePartsData DummyPartsData { get; set; }

        public BasePartsData GetFirstParts(bool _owner = false, float _basePos = 0.0f)
        {
            if (!this.IsPartsBoss || _owner)
                return this.DummyPartsData;
            List<PartsData> all = this.BossPartsListForBattle.FindAll((Predicate<PartsData>)(e => e.GetTargetable()));
            PartsData partsData1 = all[0];
            for (int index = 0; index < all.Count; ++index)
            {
                PartsData partsData2 = all[index];
                float _a = Mathf.Abs(partsData2.GetPosition().x - _basePos);
                float _b = Mathf.Abs(partsData1.GetPosition().x - _basePos);
                if ((double)_a < (double)_b || BattleUtil.Approximately(_a, _b))
                    partsData1 = partsData2;
            }
            return (BasePartsData)partsData1;
        }

        public void AppendBreakLog(UnitCtrl _source) => this.battleLog.AppendBattleLog(eBattleLogType.BREAK, 0, 0L, 0L, 0, 0, JELADBAMFKH: _source, LIMEKPEENOB: this);

        /*public void SetCountinuousPartsData(Dictionary<int, ContinuousPartsData> _data)
        {
            for (int index = 0; index < this.BossPartsListForBattle.Count; ++index)
            {
                PartsData partsData = this.BossPartsListForBattle[index];
                ContinuousPartsData continuousPartsData = _data[partsData.Index];
                partsData.BreakPoint = (ObscuredInt)continuousPartsData.BreakPoint;
                partsData.BreakTime = continuousPartsData.BreakTime;
                if ((int)partsData.BreakPoint == 0)
                    partsData.IsBreak = true;
            }
        }*/

        public IEnumerator UpdateBreak(float _duration, PartsData _data)
        {
            _data.BreakTime = _duration;
            while ((double)_data.BreakTime > 0.0)
            {
                if ((long)_data.Owner.Hp <= 0L)
                {
                    yield break;
                }
                else
                {
                    _data.BreakTime -= this.battleManager.DeltaTime_60fps;
                    yield return (object)null;
                }
            }
            _data.BreakTime = 0.0f;
            _data.SetBreak(false, (Transform)null);
        }

        private UnitCtrl KillBonusTarget { get; set; }

        public System.Action<float> OnLifeAmmountChange { get; set; }

        public long SetDamage(
          DamageData _damageData,
          bool _byAttack,
          int _actionId,
          ActionParameter.OnDamageHitDelegate _onDamageHit = null,
          bool _hasEffect = true,
          Skill _skill = null,
          bool _energyAdd = true,
          System.Action _onDefeat = null,
          bool _noMotion = false,
          float _damageWeight = 1f,
          float _damageWeightSum = 1f,
          Func<int, float, int> _upperLimitFunc = null,
          float _energyChargeMultiple = 1f,
          Action<string> callBack = null)
        {
            bool _critical = false;
            double random = BattleManager.Random(0.0f, 1f, 
                new PCRCaculator.Guild.RandomData(_damageData.Source,this, _actionId, 0, _damageData.CriticalRate));
            if (MyGameCtrl.Instance.tempData.isGuildBattle &&(MyGameCtrl.Instance.tempData.randomData.ForceNoCritical_player && IsOther || MyGameCtrl.Instance.tempData.randomData.ForceNoCritical_enemy && !IsOther))
                random = 1;
            if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.ForceCritical_player && IsOther)
                random = 0;
            //start add
            if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(_damageData.Source, this, _skill, eActionType.ATTACK, BattleHeaderController.CurrentFrameCount, out float fix))
            {
                random = fix;
            }

            if (random <= (double)_damageData.CriticalRate && (double)_damageData.CriticalDamageRate != 0.0)
                _critical = true;
            bool flag = false;
            if (_damageData.ActionType == eActionType.ATTACK)
            {
                if (_damageData.DamageType == DamageData.eDamageType.ATK && this.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_ATK_BARRIR))
                    flag = true;
                else if (_damageData.DamageType == DamageData.eDamageType.MGC && this.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_MGC_BARRIR))
                    flag = true;
                else if (this.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_ALL_BARRIR))
                    flag = true;
            }
            if (flag)
                _critical = _damageData.IsLogBarrierCritical;
            var (num1, expdmg) = this.SetDamageImpl(_damageData, _byAttack, _onDamageHit, _hasEffect, _skill, _energyAdd, _critical, _onDefeat, _noMotion, _upperLimitFunc, _energyChargeMultiple,callBack,_damageData.CriticalRate);
            if (_damageData.Target is PartsData)
            {
                if (!_damageData.IsSlipDamage)
                {
                    (_damageData.Target as PartsData).SetDamage((int)num1, _damageData.Source);
                }
                else
                {
                    for (int index = 0; index < this.BossPartsListForBattle.Count; ++index)
                        this.BossPartsListForBattle[index].SetDamage((int)num1, _damageData.Source);
                }
            }
            //if (this.MultiBossPartsData != null)
             //   this.MultiBossPartsData.SetDamage((int)num1, _damageData.Source);
            if ((UnityEngine.Object)_damageData.Source != (UnityEngine.Object)null && _damageData.ActionType != eActionType.DESTROY && _damageData.ActionType != eActionType.ATTACK_FIELD && (_skill == null || _skill.IsLifeStealEnabled))
            {
                int lifeSteal = _damageData.LifeSteal;
                if (_skill != null)
                    lifeSteal += _skill.LifeSteal;
                if (lifeSteal > 0)
                {
                    int num2 = (int)num1 * lifeSteal / (lifeSteal + (int)this.Level + 100);
                    if (num2 != 0)
                    {   //_damageData.Source.SetRecovery(num2, _damageData.DamageType == DamageData.eDamageType.MGC ? UnitCtrl.eInhibitHealType.MAGIC : UnitCtrl.eInhibitHealType.PHYSICS, _damageData.Source, false, _isUnionBurstLifeSteal: this.battleManager.BlackOutUnitList.Contains(_damageData.Source));
                        _damageData.Source.SetRecovery(num2, _damageData.DamageType == DamageData.eDamageType.MGC ? UnitCtrl.eInhibitHealType.MAGIC : UnitCtrl.eInhibitHealType.PHYSICS, _damageData.Source, UnitCtrl.GetHealDownValue(_damageData.Source), false, _isUnionBurstLifeSteal: this.battleManager.BlackOutUnitList.Contains(_damageData.Source));
                    }
                }
            }
            if ((UnityEngine.Object)_damageData.Source != (UnityEngine.Object)null && _damageData.Source.IsOther != this.IsOther)
            {
                UnitCtrl unitCtrl = _damageData.Source;
                if (unitCtrl.IsSummonOrPhantom || unitCtrl.IsDivision)
                    unitCtrl = unitCtrl.SummonSource;
                //if (unitCtrl.UnitDamageInfo != null)
                //    unitCtrl.UnitDamageInfo.SetDamage((int)((long)unitCtrl.UnitDamageInfo.damage + num1));
            }
            this.accumulateDamage += num1;
            BattleLogIntreface battleLog = this.battleLog;
            UnitCtrl source1 = _damageData.Source;
            UnitCtrl unitCtrl1 = this;
            int HLIKLPNIOKJ = (int)((_critical ? 1 : 2) * 10 + _damageData.DamageType);
            long KGNFLOPBOMB = num1;
            long hp = (long)this.Hp;
            int OJHBHHCOAGK = _actionId;
            int PFLDDMLAICG = (int)_damageWeight * 100;
            int PNJFIOPGCIC = (int)_damageWeightSum * 100;
            UnitCtrl JELADBAMFKH = source1;
            UnitCtrl LIMEKPEENOB = unitCtrl1;
            battleLog.AppendBattleLog(eBattleLogType.SET_DAMAGE, HLIKLPNIOKJ, KGNFLOPBOMB, hp, 0, OJHBHHCOAGK, PFLDDMLAICG, PNJFIOPGCIC, JELADBAMFKH, LIMEKPEENOB);
            if (_skill != null && (UnityEngine.Object)_damageData.Source != (UnityEngine.Object)null && _skill.SkillId == _damageData.Source.UnionBurstSkillId)
            {
                if (_byAttack)
                    _damageData.Target.PassiveUbIsMagic = _damageData.DamageType == DamageData.eDamageType.MGC;
                _damageData.Target.TotalDamage += (float)num1;
            }
            if (_skill != null && num1 > 0L && !_skill.DamagedPartsList.Contains(_damageData.Target))
                _skill.DamagedPartsList.Add(_damageData.Target);
            if ((UnityEngine.Object)_damageData.Source != (UnityEngine.Object)null && this.DamageSealDataDictionary.ContainsKey(_damageData.Source) && num1 > 0L)
            {
                Dictionary<int, AttackSealData>.Enumerator enumerator = this.DamageSealDataDictionary[_damageData.Source].GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.Value.OnlyCritical)
                    {
                        if (_critical)
                            enumerator.Current.Value.AddSeal(this);
                    }
                    else
                        enumerator.Current.Value.AddSeal(this);
                }
            }
            if ((UnityEngine.Object)_damageData.Source != (UnityEngine.Object)null && _damageData.Source.DamageOnceOwnerSealDateDictionary.ContainsKey(_damageData.Source) && (num1 > 0L && _skill != null) && !_skill.AlreadyAddAttackSelfSeal)
            {
                foreach (KeyValuePair<int, AttackSealData> keyValuePair in _damageData.Source.DamageOnceOwnerSealDateDictionary[_damageData.Source])
                    keyValuePair.Value.AddSeal(_damageData.Source);
                _skill.AlreadyAddAttackSelfSeal = true;
            }
            if ((UnityEngine.Object)_damageData.Source != (UnityEngine.Object)null && _damageData.Source.DamageOwnerSealDataDictionary.ContainsKey(_damageData.Source) && num1 > 0L)
            {
                UnitCtrl source2 = _damageData.Source;
                foreach (AttackSealData attackSealData in _damageData.Source.DamageOwnerSealDataDictionary[source2].Values)
                {
                    if (attackSealData.OnlyCritical)
                    {
                        if (_critical)
                            attackSealData.AddSeal(source2);
                    }
                    else
                        attackSealData.AddSeal(source2);
                }
            }
            if ((UnityEngine.Object)_damageData.Source != (UnityEngine.Object)null && num1 > 0L)
            {
                _damageData.Source.OnActionByDamage.Call();
                if (_skill != null && !_skill.AlreadyAddAttackSelfSeal)
                {
                    _damageData.Source.OnActionByDamageOnce.Call();
                    _skill.AlreadyExexActionByHit = true;
                }
                if (_critical)
                    _damageData.Source.OnActionByCritical.Call();
            }
            if (num1 > 0L & _critical && _skill.CriticalPartsList != null)
                _skill.CriticalPartsList.Add(_damageData.Target);
            if (_skill != null)
            {
                _skill.TotalDamage += num1;
                _skill.ExDamage += expdmg;
            }
            return num1;
        }

        public (long, long) SetDamageImpl(
          DamageData _damageData,
          bool _byAttack,
          ActionParameter.OnDamageHitDelegate _onDamageHit,
          bool _hasEffect,
          Skill _skill,
          bool _energyAdd,
          bool _critical,
          System.Action _onDefeat,
          bool _noMotion,
          Func<int, float, int> _upperLimitFunc,
          float _energyChargeMultiple,
          Action<string> callBack = null,
          float criticalRate = 0)
        {
            if (this.IdleOnly || this.IsDivisionSourceForDamage && !_damageData.IsDivisionDamage)
            {
                callBack?.Invoke("伤害无效,目标不是可攻击状态");
                return (0, 0);
            }
            //if (this.battleManager.GetPurpose() == eHatsuneSpecialPurpose.SHIELD && this.IsBoss)
            //    this.battleManager.SubstructEnemyPoint(1);
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION))
            {
                callBack?.Invoke("伤害无效，目标处于无敌状态");
                return (0, 0);
            }
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.PHYSICS_DODGE) && _damageData.DamageType == DamageData.eDamageType.ATK)
            {
                this.SetMissAtk(_damageData.Source, eMissLogType.DODGE_BY_NO_DAMAGE_MOTION, _parts: _damageData.Target);
                callBack?.Invoke("伤害无效，目标处于物理闪避状态");
                return (0, 0);
            }
            var a = _damageData.Damage;
            float num1 = 2f * _damageData.CriticalDamageRate;
            if (_critical)
                a.value *= num1;
            a.ex *= (1 + (num1 - 1) * Mathf.Clamp(_damageData.CriticalRate, 0f, 1f));
            if (this.debuffDamageUpDataList.Count > 0)
                a *= this.GetDebuffDamageUpValue();
            bool flag1 = false;
            if (_damageData.ActionType == eActionType.ATTACK)
            {
                if (_damageData.DamageType == DamageData.eDamageType.ATK && this.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_ATK_BARRIR))
                    flag1 = true;
                else if (_damageData.DamageType == DamageData.eDamageType.MGC && this.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_MGC_BARRIR))
                    flag1 = true;
                else if (this.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_ALL_BARRIR))
                    flag1 = true;
            }
            if (flag1)
                a = _damageData.LogBarrierExpectedDamage;
            if (_damageData.DamageType == DamageData.eDamageType.ATK && this.IsAbnormalState(UnitCtrl.eAbnormalState.CUT_ATK_DAMAGE))
            {
                float num2 = this.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.CUT_ATK_DAMAGE) / 100f;
                a *= 1f - num2;
            }
            else if (_damageData.DamageType == DamageData.eDamageType.MGC && this.IsAbnormalState(UnitCtrl.eAbnormalState.CUT_MGC_DAMAGE))
            {
                float num2 = this.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.CUT_MGC_DAMAGE) / 100f;
                a *= 1f - num2;
            }
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.CUT_ALL_DAMAGE))
            {
                float num2 = this.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.CUT_ALL_DAMAGE) / 100f;
                a *= 1f - num2;
            }
            if (_damageData.ActionType == eActionType.ATTACK)
            {
                if (_damageData.DamageType == DamageData.eDamageType.ATK && this.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_ATK_BARRIR))
                {
                    float abnormalStateSubValue = this.GetAbnormalStateSubValue(UnitCtrl.eAbnormalStateCategory.LOG_ATK_BARRIR);
                    if ((double)_damageData.TotalDamageForLogBarrier > (double)abnormalStateSubValue)
                    {
                        float abnormalStateMainValue = this.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.LOG_ATK_BARRIR);
                        var num2 = (MathfPlus.Log(((_damageData.TotalDamageForLogBarrier - abnormalStateSubValue) / abnormalStateMainValue + 1.0f)) * abnormalStateMainValue + abnormalStateSubValue) / _damageData.TotalDamageForLogBarrier;
                        a *= num2;
                    }
                }
                else if (_damageData.DamageType == DamageData.eDamageType.MGC && this.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_MGC_BARRIR))
                {
                    float abnormalStateSubValue = this.GetAbnormalStateSubValue(UnitCtrl.eAbnormalStateCategory.LOG_MGC_BARRIR);
                    if ((double)_damageData.TotalDamageForLogBarrier > (double)abnormalStateSubValue)
                    {
                        float abnormalStateMainValue = this.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.LOG_MGC_BARRIR);
                        var num2 = (MathfPlus.Log(((_damageData.TotalDamageForLogBarrier - abnormalStateSubValue) / abnormalStateMainValue + 1.0f)) * abnormalStateMainValue + abnormalStateSubValue) / _damageData.TotalDamageForLogBarrier;
                        a *= num2;
                    }
                }
                if (this.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_ALL_BARRIR))
                {
                    float abnormalStateSubValue = this.GetAbnormalStateSubValue(UnitCtrl.eAbnormalStateCategory.LOG_ALL_BARRIR);
                    if ((double)_damageData.TotalDamageForLogBarrier > (double)abnormalStateSubValue)
                    {
                        float abnormalStateMainValue = this.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.LOG_ALL_BARRIR);
                        var num2 = (MathfPlus.Log(((_damageData.TotalDamageForLogBarrier - abnormalStateSubValue) / abnormalStateMainValue + 1.0f)) * abnormalStateMainValue + abnormalStateSubValue) / _damageData.TotalDamageForLogBarrier;
                        a *= num2;
                    }
                }
            }
            if (_hasEffect)
            {
                this.OnDamageForUIShake.Call();
                //if (!this.DisableFlash)
                //    this.AppendCoroutine(this.setColorOffsetDefaultWithDelay(), ePauseType.NO_DIALOG);
            }
            if (!_damageData.IgnoreDef && !flag1)
            {
                switch (_damageData.DamageType)
                {
                    case DamageData.eDamageType.ATK:
                        float defZero = (float)_damageData.Target.GetDefZero();
                        float num3 = Mathf.Max(0.0f, defZero - (float)_damageData.DefPenetrate);
                        a *= (float)(1.0 - (double)num3 / ((double)defZero + 100.0));
                        break;
                    case DamageData.eDamageType.MGC:
                        float magicDefZero = (float)_damageData.Target.GetMagicDefZero();
                        float num4 = Mathf.Max(0.0f, magicDefZero - (float)_damageData.DefPenetrate);
                        a *= (float)(1.0 - (double)num4 / ((double)magicDefZero + 100.0));
                        break;
                }
            }
            var num5 = 0f-MathfPlus.Max(0f-a, 0f-999999f);
            if (this.CurrentState == UnitCtrl.ActionState.DIE)
            {
                //this.createDamageEffectFromSetDamageImpl(_damageData, _hasEffect, _skill, _critical, BattleUtil.FloatToInt(num5));
                callBack?.Invoke("伤害无效，目标已经死了");
                return (0, 0);
            }
            /*
            if (_upperLimitFunc != null)
                num5 = (float)_upperLimitFunc(BattleUtil.FloatToInt(num5), _critical ? num1 : 1f);*/
            if ((UnityEngine.Object)_damageData.Source != (UnityEngine.Object)null && _damageData.ActionType != eActionType.FORCE_HP_CHANGE)
            {
                foreach (KeyValuePair<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet> strikeBack1 in this.StrikeBackDictionary)
                {
                    for (int index = strikeBack1.Value.DataList.Count - 1; index >= 0; --index)
                    {
                        StrikeBackData strikeBack = strikeBack1.Value.DataList[index];
                        List<StrikeBackData> list = strikeBack1.Value.DataList;
                        if (!strikeBack.IsDieing && !strikeBack.Execed)
                        {
                            switch (strikeBack.StrikeBackType)
                            {
                                case StrikeBackData.eStrikeBackType.PHYSICAL_GUARD:
                                case StrikeBackData.eStrikeBackType.PHYSICAL_DRAIN:
                                    if (_damageData.DamageType == DamageData.eDamageType.ATK)
                                    {
                                        strikeBack.Exec(_damageData.Source, this, (int)BattleUtil.FloatToInt(num5), (System.Action)(() => list.Remove(strikeBack)));
                                        this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, eStateIconType.STRIKE_BACK, false);
                                        this.MyOnChangeAbnormalState?.Invoke(this, eStateIconType.STRIKE_BACK, false,90, "反击中");
                                        callBack?.Invoke("伤害无效，被目标格挡");
                                        return (0, 0);
                                    }
                                    continue;
                                case StrikeBackData.eStrikeBackType.MAGIC_GUARD:
                                case StrikeBackData.eStrikeBackType.MAGIC_DRAIN:
                                    if (_damageData.DamageType == DamageData.eDamageType.MGC)
                                    {
                                        strikeBack.Exec(_damageData.Source, this, (int)BattleUtil.FloatToInt(num5), (System.Action)(() => list.Remove(strikeBack)));
                                        this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, eStateIconType.STRIKE_BACK, false);
                                        this.MyOnChangeAbnormalState?.Invoke(this, eStateIconType.STRIKE_BACK, false, 90, "反击中");

                                        callBack?.Invoke("伤害无效，被目标格挡");
                                        return (0, 0);
                                    }
                                    continue;
                                case StrikeBackData.eStrikeBackType.BOTH_GUARD:
                                case StrikeBackData.eStrikeBackType.BOTH_DRAIN:
                                    strikeBack.Exec(_damageData.Source, this, (int)BattleUtil.FloatToInt(num5), (System.Action)(() => list.Remove(strikeBack)));
                                    this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, eStateIconType.STRIKE_BACK, false);
                                    this.MyOnChangeAbnormalState?.Invoke(this, eStateIconType.STRIKE_BACK, false, 90, "反击中");
                                    callBack?.Invoke("伤害无效，被目标格挡");
                                    return (0, 0);
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
            if (_skill != null && _damageData.ActionType == eActionType.ATTACK)
                num5 *= _skill.AweValue;
            if (_energyAdd)
                this.ChargeEnergy(eSetEnergyType.BY_SET_DAMAGE, num5 * (float)this.skillStackValDmg, _source: this, _hasNumberEffect: false, _multipleValue: _energyChargeMultiple);
            if ((double)num5 <= 0.0 && _damageData.ActionType == eActionType.FORCE_HP_CHANGE)
            {
                this.createDamageEffectFromSetDamageImpl(_damageData, _hasEffect, _skill, _critical, (int)BattleUtil.FloatToInt(num5));
                callBack?.Invoke("伤害无效，因为伤害为负数");
                return (0, 0);
            }
            var _fDamage = MathfPlus.Max(num5, 1f);
            /*if (this.battleManager.GetPurpose() == eHatsuneSpecialPurpose.ABSORBER && this.battleManager.KIHOGJBONDH != 0 && this.IsBoss)
            {
                int PBAANIPPIGL = BattleUtil.FloatToInt(_fDamage);
                switch (_damageData.ActionType)
                {
                    case eActionType.ATTACK:
                    case eActionType.CONTINUOUS_ATTACK:
                    case eActionType.RATIO_DAMAGE:
                    case eActionType.UPPER_LIMIT_ATTACK:
                        if (PBAANIPPIGL < _skill.AbsorberValue)
                        {
                            this.battleManager.SubstructEnemyPoint(PBAANIPPIGL);
                            _fDamage = 0.0f;
                            _skill.AbsorberValue -= PBAANIPPIGL;
                            break;
                        }
                        this.battleManager.SubstructEnemyPoint(_skill.AbsorberValue);
                        _fDamage -= (float)_skill.AbsorberValue;
                        _skill.AbsorberValue = 0;
                        break;
                    case eActionType.SLIP_DAMAGE:
                    case eActionType.CONTINUOUS_ATTACK_NEARBY:
                    case eActionType.ATTACK_FIELD:
                        _fDamage = (float)_damageData.ExecAbsorber(PBAANIPPIGL);
                        break;
                    case eActionType.ENCHANT_STRIKE_BACK:
                        if (PBAANIPPIGL < this.battleManager.KIHOGJBONDH)
                        {
                            this.battleManager.SubstructEnemyPoint(PBAANIPPIGL);
                            _fDamage = 0.0f;
                            break;
                        }
                        this.battleManager.SubstructEnemyPoint(this.battleManager.KIHOGJBONDH);
                        _fDamage -= (float)_skill.AbsorberValue;
                        break;
                }
            }*/
            int _overRecoverValue = 0;
            if (_damageData.ActionType != eActionType.INHIBIT_HEAL && _damageData.ActionType != eActionType.FORCE_HP_CHANGE)
                this.execBarrier(_damageData, ref _fDamage, ref _overRecoverValue);
            var num6 = BattleUtil.FloatToInt(_fDamage);
            bool flag2 = (long)this.Hp > 0L;
            int num7 = (double)(long)this.Hp > (double)(long)this.MaxHp * 0.200000002980232 ? 1 : 0;
            long hp = (long)this.Hp;
            this.Hp = (ObscuredLong)((long)this.Hp - (long)((int)num6 - (_overRecoverValue < 0 ? 0 : _overRecoverValue)));
            if (_onDamageHit != null & flag2)
                _onDamageHit((float)num6);
            if ((long)this.Hp < 0L)
                this.Hp = (ObscuredLong)0L;
            //if ((long)this.Hp == 0L && this.battleManager.BattleCategory == eBattleCategory.GLOBAL_RAID && (SekaiUtility.IsBossDead() && this.IsBoss))
            //    this.Hp = (ObscuredLong)1L;
            if ((long)this.Hp == 0L && (this.IsTough || this.ExecKnightGuard()) && (long)this.Hp == 0L)
                this.Hp = (ObscuredLong)1L;
            if ((long)this.Hp > (long)this.MaxHp)
                this.Hp = this.MaxHp;
            //if (num7 != 0 && (double)(long)this.Hp < (double)(long)this.MaxHp * 0.200000002980232)
            //    this.playDamageVoice();
            /*if ((UnityEngine.Object)this.lifeGauge != (UnityEngine.Object)null)
            {
                float NormalizedHP = (float)(long)this.Hp / (float)(long)this.MaxHp;
                if (this.IsClanBattleOrSekaiEnemy)
                {
                    bool flag3 = false;
                    if (_skill != null)
                    {
                        List<UnitCtrl> djjkgcfkjnj = this.battleManager.UnitList;
                        for (int index = 0; index < djjkgcfkjnj.Count; ++index)
                        {
                            if (djjkgcfkjnj[index].UnionBurstSkillId == _skill.SkillId)
                            {
                                flag3 = true;
                                break;
                            }
                        }
                    }
                    long num2 = num6;
                    int _damage;
                    if (this.battleManager.BattleCategory == eBattleCategory.GLOBAL_RAID)
                    {
                        if (num6 > hp && (!this.IsBoss))// || !SekaiUtility.IsBossDead()))
                            num2 = hp;
                        if (this.IsBoss)
                            this.battleManager.IJGJOGNIGLH += (int)num2;
                        _damage = (this.battleManager.EBDCFPAJFOK += (int)num2);
                    }
                    else
                        _damage = (int)(this.accumulateDamage + num6);
                    //if (flag3)
                   //     SingletonMonoBehaviour<BattleHeaderController>.Instance.BossTotalDamage.SetDamageUB(_damage, _damageData.DamegeEffectType, this);
                    //else
                    //    SingletonMonoBehaviour<BattleHeaderController>.Instance.BossTotalDamage.SetDamageNormal(_damage);
                    if (this.IsBoss)
                    {
                       // if (this.battleManager.BattleCategory == eBattleCategory.GLOBAL_RAID && SekaiUtility.IsBossDead())
                       //     SingletonMonoBehaviour<BattleHeaderController>.Instance.BossGauge.UpdateInvalidLifeAmount((long)this.MaxHp);
                       // else
                       //     SingletonMonoBehaviour<BattleHeaderController>.Instance.BossGauge.SetNormalizedLifeAmount((long)this.MaxHp, (long)this.Hp);
                    }
                    else
                    {
                        //this.lifeGauge.SetNormalizedLifeAmount(NormalizedHP);
                        this.OnLifeAmmountChange.Call<float>(NormalizedHP);
                    }
                }
                else if (this.IsBoss)
                {
                   // if (this.battleManager.BattleCategory == eBattleCategory.GLOBAL_RAID && SekaiUtility.IsBossDead())
                   //     SingletonMonoBehaviour<BattleHeaderController>.Instance.BossGauge.UpdateInvalidLifeAmount((long)this.MaxHp);
                   // else
                    //    SingletonMonoBehaviour<BattleHeaderController>.Instance.BossGauge.SetNormalizedLifeAmount((long)this.MaxHp, (long)this.Hp);
                }
                else
                {
                    //this.lifeGauge.SetNormalizedLifeAmount(NormalizedHP);
                    this.OnLifeAmmountChange.Call<float>(NormalizedHP);
                }
            }*/
            float NormalizedHP = (float)(long)this.Hp / (float)(long)this.MaxHp;
            this.OnLifeAmmountChange.Call<float>(NormalizedHP);
            string des;

            des = "受到来自" + (_damageData.Source == null ? "???" : _damageData.Source.UnitName) + "的<color=#FF0000>" + num6 + (_critical ? "</color>点<color=#FFEB00>暴击</color>伤害" : "</color>点伤害");
            MyOnLifeChanged?.Invoke(UnitId,NormalizedHP,(int)this.Hp, (int)num6, BattleHeaderController.CurrentFrameCount,des);
            uIManager.LogMessage(des,PCRCaculator.Battle.eLogMessageType.GET_DAMAGE, this);
            this.createDamageEffectFromSetDamageImpl(_damageData, _hasEffect, _skill, _critical, (int)num6);
            if (this.OnDamage != null)
                this.OnDamage(_byAttack, (float)num6, _critical);
            MyOnDamage?.Invoke(UnitId, _damageData.Source == null ? 0 : _damageData.Source.UnitId, (float)num6, BattleHeaderController.CurrentFrameCount);
            MyOnDamage2?.Invoke(_byAttack, num6, _critical, (long)(num6-num6/num1), num6.ex);
            this.OnDamageForLoopTrigger.Call<bool, float, bool>(_byAttack, (float)num6, _critical);
            this.OnDamageForLoopRepeat.Call<float>((float)num6);
            this.OnDamageForDivision.Call<bool, float, bool>(_byAttack, Mathf.Min((float)hp, (float)num6), _critical);
            this.OnDamageForSpecialSleepRelease.Call<bool>(_byAttack);
            foreach (KeyValuePair<int, System.Action<bool>> keyValuePair in this.OnDamageListForChangeSpeedDisableByAttack)
                keyValuePair.Value.Call<bool>(_byAttack);
            if (this.OnHpChange != null)
                this.OnHpChange(_byAttack, (float)num6, _critical);
           // if (this.battleManager.IsSpecialBattle && this.IsBoss && (this.specialBattlePurposeHp != 0 && (long)this.Hp < (long)this.specialBattlePurposeHp))
            //    this.battleManager.SpecialBattleModeChangeOnHpChange();
            if (!this.HasUnDeadTime)
            {
                if (!_noMotion)
                    this.PlayDamageWhenIdle(true, _skill != null && _skill.PauseStopState);
                if ((long)this.Hp <= 0L && !this.IsDead && this.CurrentState < UnitCtrl.ActionState.DIE)
                {
                    if ((UnityEngine.Object)_damageData.Source != (UnityEngine.Object)null & flag2)
                    {
                        this.KillBonusTarget = _damageData.Source.IsAbnormalState(UnitCtrl.eAbnormalState.FEAR) ? (UnitCtrl)null : _damageData.Source;
                        _onDefeat.Call();
                    }
                    if (flag2)
                        this.SetState(UnitCtrl.ActionState.DIE);
                }
            }
            string describe = "对目标造成<color=#FF0000>" + num6 + (_critical? "</color>点<color=#FFEB00>暴击</color>伤害" : "</color>点伤害");
            callBack?.Invoke(describe);
            return ((long)num6.value, (long)num6.ex);
        }

        private void execBarrier(DamageData _damageData, ref FloatWithEx _fDamage, ref int _overRecoverValue)
        {
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.GUARD_ATK) && _damageData.DamageType == DamageData.eDamageType.ATK)
            {
                var num = _fDamage - this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue;
                if ((double)num > 0.0)
                {
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.GUARD_ATK, false);
                    _fDamage = num;
                }
                else
                {
                    this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue -= (float)BattleUtil.FloatToInt(_fDamage);
                    _fDamage = 0.0f;
                }
            }
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.GUARD_MGC) && _damageData.DamageType == DamageData.eDamageType.MGC)
            {
                var num = _fDamage - this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue;
                if ((double)num > 0.0)
                {
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.GUARD_MGC, false);
                    _fDamage = num;
                }
                else
                {
                    this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue -= (float)BattleUtil.FloatToInt(_fDamage);
                    _fDamage = 0.0f;
                }
            }
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.DRAIN_ATK) && _damageData.DamageType == DamageData.eDamageType.ATK)
            {
                var num1 = _fDamage - this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue;
                if ((double)num1 > 0.0)
                {
                    _overRecoverValue += (int)this.setRecoveryAndGetOverRecovery(BattleUtil.FloatToInt(this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue), this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].Source);
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.DRAIN_ATK, false);
                    _fDamage = num1;
                }
                else
                {
                    var num2 = BattleUtil.FloatToInt(_fDamage);
                    _overRecoverValue += (int)this.setRecoveryAndGetOverRecovery((int)num2, this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].Source);
                    this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue -= (float)num2;
                    _fDamage = 0.0f;
                }
            }
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.DRAIN_MGC) && _damageData.DamageType == DamageData.eDamageType.MGC)
            {
                var num1 = _fDamage - this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue;
                if ((double)num1 > 0.0)
                {
                    _overRecoverValue += (int)this.setRecoveryAndGetOverRecovery(BattleUtil.FloatToInt(this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue), this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].Source);
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.DRAIN_MGC, false);
                    _fDamage = num1;
                }
                else
                {
                    int num2 = (int)BattleUtil.FloatToInt(_fDamage);
                    _overRecoverValue += (int)this.setRecoveryAndGetOverRecovery(num2, this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].Source);
                    this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue -= (float)num2;
                    _fDamage = 0.0f;
                }
            }
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.GUARD_BOTH) && _damageData.ActionType != eActionType.DESTROY)
            {
                var num = _fDamage - this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].MainValue;
                if ((double)num > 0.0)
                {
                    this.EnableAbnormalState(UnitCtrl.eAbnormalState.GUARD_BOTH, false);
                    _fDamage = num;
                }
                else
                {
                    this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].MainValue -= (float)BattleUtil.FloatToInt(_fDamage);
                    _fDamage = 0.0f;
                }
            }
            if (!this.IsAbnormalState(UnitCtrl.eAbnormalState.DRAIN_BOTH) || _damageData.ActionType == eActionType.DESTROY)
                return;
            var num3 = _fDamage - this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].MainValue;
            if ((double)num3 > 0.0)
            {
                _overRecoverValue += (int)this.setRecoveryAndGetOverRecovery(BattleUtil.FloatToInt(this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].MainValue), this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].Source);
                this.EnableAbnormalState(UnitCtrl.eAbnormalState.DRAIN_BOTH, false);
                _fDamage = num3;
            }
            else
            {
                int num1 = (int)BattleUtil.FloatToInt(_fDamage);
                _overRecoverValue += (int)this.setRecoveryAndGetOverRecovery(num1, this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].Source);
                this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].MainValue -= (float)num1;
                _fDamage = 0.0f;
            }
        }

        private void createDamageEffectFromSetDamageImpl(
          DamageData _damageData,
          bool _hasEffect,
          Skill _skill,
          bool _critical,
          int _damage)
        {
            if (((!((UnityEngine.Object)this.battleManager != (UnityEngine.Object)null) ? 0 : (_damage >= 0 ? 1 : 0)) & (_hasEffect ? 1 : 0)) == 0)
                return;
            PCRCaculator.Battle.eDamageType damageType = (PCRCaculator.Battle.eDamageType)(int)_damageData.DamageType;
            PCRCaculator.Battle.eDamageEffectType effectType = (PCRCaculator.Battle.eDamageEffectType)(int)_damageData.DamegeEffectType;
            UIManager.SetDamageNumber(gameObject.transform.position, _damage, damageType, effectType, _critical);
            //eDamageEffctTypeDetail key = (UnityEngine.Object)_damageData.Source == (UnityEngine.Object)null || _damageData.Source.IsLeftDir == !this.battleManager.IsDefenceReplayMode ? (_critical ? eDamageEffctTypeDetail.UNIT_CRITICAL : eDamageEffctTypeDetail.UNIT) : (_critical ? eDamageEffctTypeDetail.ENEMY_CRITICAL : eDamageEffctTypeDetail.ENEMY);
            //GameObject _pPrefab = this.battleManager.AMPANMGEBBM[_damageData.DamegeEffectType][key];
            //this.CreateDamageNumEffect(_damage, _pPrefab, true, _critical, _skill, _damageData.DamageNumMagic || _damageData.DamageType == DamageData.eDamageType.MGC, _damageData.Source, _damageData.DamageSoundType, _damageData.Target, _damageData.DamegeNumScale);
        }

        /*public DamageEffectCtrl CreateDamageNumEffect(
          int _damage,
          GameObject _pPrefab,
          bool _first,
          bool _critical,
          Skill _skill,
          bool _isMagic,
          UnitCtrl _source,
          DamageData.eDamageSoundType _soundType,
          BasePartsData _targetParts,
          float _scale,
          bool _isTowerTimeUp = false)
        {
            float time = Time.time;
            if ((double)this.lastHitTime + 0.449999988079071 < (double)time || _skill != null && (double)_skill.BlackOutTime > 0.0 && !_skill.ForceComboDamage)
                this.comboCount = 0;
            else
                ++this.comboCount;
            this.lastHitTime = time;
            DamageEffectCtrl damageEffectCtrl = this.battleEffectPool.LoadNumberEffect(_pPrefab) as DamageEffectCtrl;
            if (this.damageNumCenterBone)
            {
                Vector3 vector3 = BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(this.CenterBone, this.RotateCenter, this.BottomTransform.lossyScale) - this.transform.position;
                damageEffectCtrl.SetPosition(_targetParts, Vector3.up * 0.1f * (float)this.comboCount + Vector3.right * vector3.x, _scale);
            }
            else
            {
                if ((UnityEngine.Object)damageEffectCtrl == (UnityEngine.Object)null)
                    return (DamageEffectCtrl)null;
                if (!_isTowerTimeUp)
                    damageEffectCtrl.SetPosition(_targetParts, Vector3.up * 0.1f * (float)this.comboCount, _scale);
                else
                    damageEffectCtrl.transform.position = _targetParts.GetBottomTransformPosition() + _targetParts.GetFixedCenterPos() + new Vector3(0.0f, -9.259259f, 0.0f);
            }
            damageEffectCtrl.transform.localScale = _pPrefab.transform.localScale;
            ++this.hitEffectSortOffset;
            this.sortOffsetResetTimer = 0.6f;
            damageEffectCtrl.SetSortOrder((this.IsFront ? 11500 : 0) + 11250 + this.hitEffectSortOffset);
            damageEffectCtrl.SetDamageText(_damage.ToString(), _isMagic ? eDamageEffectColor.PURPLE : eDamageEffectColor.YELLOW);
            damageEffectCtrl.PlaySe(_skill, _source, _soundType);
            return damageEffectCtrl;
        }*/

        /*private IEnumerator setColorOffsetDefaultWithDelay()
        {
            UnitCtrl unitCtrl = this;
            int i;
            for (i = 0; i < UnitCtrl.FlashDelayFrame && !unitCtrl.battleManager.BlackOutUnitList.Contains(unitCtrl); ++i)
                yield return (object)null;
            for (i = 0; i < UnitCtrl.DamageFlashFrame && !unitCtrl.battleManager.BlackOutUnitList.Contains(unitCtrl); ++i)
            {
                unitCtrl.curColor = UnitCtrl.DAMAGE_FLASH_COLOR;
                unitCtrl.updateCurColor();
                yield return (object)null;
            }
            unitCtrl.ResetColor((ChangeColorEffect)null);
        }*/

        public void SetMissAtk(
          UnitCtrl _source,
          eMissLogType _missLogType,
          eDamageEffectType _type = eDamageEffectType.NORMAL,
          BasePartsData _parts = null,
          float _scale = 1f)
        {
            if (_parts == null)
                _parts = this.GetFirstParts(true);
            BattleLogIntreface battleLog = this.battleLog;
            UnitCtrl unitCtrl1 = _source;
            UnitCtrl unitCtrl2 = this;
            int HLIKLPNIOKJ = (int)_missLogType;
            UnitCtrl JELADBAMFKH = unitCtrl1;
            UnitCtrl LIMEKPEENOB = unitCtrl2;
            battleLog.AppendBattleLog(eBattleLogType.MISS, HLIKLPNIOKJ, 0L, 0L, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
            /*GameObject MDOJNMEMHLN = Singleton<LCEGKJFKOPD>.Instance.NBOBBOPFGCN;
            if (_type == eDamageEffectType.COMBO)
                MDOJNMEMHLN = _source.IsLeftDir != !this.battleManager.IsDefenceReplayMode ? Singleton<LCEGKJFKOPD>.Instance.LHELBNIHGIE : Singleton<LCEGKJFKOPD>.Instance.LEKFHCGCIBG;
            float time = Time.time;
            if ((double)this.lastHitTime + 0.449999988079071 < (double)time)
                this.comboCount = 0;
            else
                ++this.comboCount;
            this.lastHitTime = time;
            DamageEffectCtrlBase damageEffectCtrlBase = this.battleEffectPool.LoadNumberEffect(MDOJNMEMHLN);
            damageEffectCtrlBase.transform.localScale = MDOJNMEMHLN.transform.localScale;
            damageEffectCtrlBase.SetPosition(_parts, Vector3.up * 0.1f * (float)this.comboCount, _scale);
            this.sortOffsetResetTimer = 0.6f;
            ++this.hitEffectSortOffset;
            damageEffectCtrlBase.SetSortOrder((this.IsFront ? 11500 : 0) + 11250 + this.hitEffectSortOffset);*/
            UIManager.SetMissEffect(gameObject.transform.position,_type == eDamageEffectType.NORMAL?1:2);
        }

        /*public void ShowHitEffect(
          SystemIdDefine.eWeaponSeType _weaponType,
          Skill skill,
          bool _isLeft,
          BasePartsData _parts)
        {
            eResourceId key1 = BattleDefine.WEAPON_HIT_EFFECT_DIC[_weaponType];
            if (_isLeft)
            {
                eResourceId key2 = BattleDefine.WEAPON_HIT_EFFECT_DIC_L[key1];
                if ((UnityEngine.Object)Singleton<LCEGKJFKOPD>.Instance.IEALDDONAIL[key2] != (UnityEngine.Object)null)
                    key1 = key2;
            }
            eSE se = BattleDefine.WEAPON_SE_DIC[_weaponType];
            GameObject MDOJNMEMHLN = Singleton<LCEGKJFKOPD>.Instance.IEALDDONAIL[key1];
            GameObject gameObject = this.battleEffectPool.GetEffect(MDOJNMEMHLN).gameObject;
            gameObject.transform.parent = ExceptNGUIRoot.Transform;
            if (skill.TrackDamageNum)
                gameObject.GetComponent<TrackingObject>().SetTarget(_parts, _bone: this.CenterBone);
            gameObject.transform.position = BattleUnitBaseSpineController.BoneWorldToGlobalPosConsiderRotate(this.CenterBone, this.RotateCenter, this.BottomTransform.lossyScale) + MDOJNMEMHLN.transform.localPosition;
            gameObject.transform.localScale = MDOJNMEMHLN.transform.localScale;
            SkillEffectCtrl component = gameObject.GetComponent<SkillEffectCtrl>();
            component.InitializeSort();
            component.SortTarget = this;
            ++this.hitEffectSortOffset;
            this.sortOffsetResetTimer = 0.6f;
            component.ExecAppendCoroutine();
            component.SetSortOrderHit(this.hitEffectSortOffset);
            component.PlaySe(se, this.IsLeftDir);
        }*/

        private long setRecoveryAndGetOverRecovery(
          int _value,
          UnitCtrl _source,
          BasePartsData _target,
          bool _isMagic,
          UnitCtrl _healSource)
        {
            long num = (long)this.Hp + (long)_value - (long)this.MaxHp;
            this.SetRecovery(_value, _isMagic ? UnitCtrl.eInhibitHealType.MAGIC : UnitCtrl.eInhibitHealType.PHYSICS, _source, UnitCtrl.GetHealDownValue(_healSource), _target: _target);
            return num;
        }

        public void SetRecovery(
          int _value,
          UnitCtrl.eInhibitHealType _inhibitHealType,
          UnitCtrl _source,
                float _healDownValue = 1f,
          bool _isEffect = true,
          bool _isRevival = false,
          bool _isUnionBurstLifeSteal = false,
          bool _isRegenerate = false,
          bool _useNumberEffect = true,
          BasePartsData _target = null,
          bool _releaseToad = false,
          Action<string> action = null)
        {
            if (_target == null)
                _target = this.GetFirstParts(true);
            _value = BattleUtil.FloatToInt(_healDownValue * (float)_value);
            if ((this.IsDead || (double)(long)this.Hp <= 0.0) && !_isRevival || this.IsClanBattleOrSekaiEnemy)
                this.battleLog.AppendBattleLog(eBattleLogType.MISS, 8, 0L, 0L, 0, 0, JELADBAMFKH: _source, LIMEKPEENOB: this);
            else if (this.IsAbnormalState(UnitCtrl.eAbnormalState.INHIBIT_HEAL) && _inhibitHealType != UnitCtrl.eInhibitHealType.NO_EFFECT)
            {
                if ((double)this.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.INHIBIT_HEAL) == 0.0)
                    return;
                DamageData _damageData = new DamageData()
                {
                    Target = this.GetFirstParts((bool)(UnityEngine.Object)this),
                    Damage = (long)BattleUtil.FloatToInt(this.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.INHIBIT_HEAL) * (float)_value),
                    DamageType = DamageData.eDamageType.NONE,
                    Source = this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.INHIBIT_HEAL].Source,
                    DamageNumMagic = _inhibitHealType == UnitCtrl.eInhibitHealType.MAGIC,
                    ActionType = eActionType.INHIBIT_HEAL
                };
                Skill skill = this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.INHIBIT_HEAL].Skill;
                int actionId = this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.INHIBIT_HEAL].ActionId;
                Skill _skill = skill;
                this.SetDamage(_damageData, false, actionId, _skill: _skill);
                action?.Invoke("毒奶，对目标造成" + _damageData.Damage + "点伤害");
            }
            else
            {
                if (_releaseToad && this.ToadDatas.Count > 0 && this.ToadDatas[0].ReleaseByHeal)
                    this.ToadDatas[0].Enable = false;
                this.Hp = (ObscuredLong)((long)this.Hp + (long)_value);
                BattleLogIntreface battleLog = this.battleLog;
                UnitCtrl unitCtrl1 = _source;
                UnitCtrl unitCtrl2 = this;
                long KGNFLOPBOMB = (long)_value;
                long hp = (long)this.Hp;
                UnitCtrl JELADBAMFKH = unitCtrl1;
                UnitCtrl LIMEKPEENOB = unitCtrl2;
                battleLog.AppendBattleLog(eBattleLogType.SET_RECOVERY, 0, KGNFLOPBOMB, hp, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
                if ((long)this.Hp > (long)this.MaxHp)
                    this.Hp = this.MaxHp;
                /*if ((UnityEngine.Object)this.lifeGauge != (UnityEngine.Object)null)
                {
                    float NormalizedHP = (float)(long)this.Hp / (float)(long)this.MaxHp;
                    if (this.IsBoss)
                    {
                       // if (this.battleManager.BattleCategory == eBattleCategory.GLOBAL_RAID && SekaiUtility.IsBossDead())
                       //     SingletonMonoBehaviour<BattleHeaderController>.Instance.BossGauge.UpdateInvalidLifeAmount((long)this.MaxHp);
                       // else
                       //     SingletonMonoBehaviour<BattleHeaderController>.Instance.BossGauge.SetNormalizedLifeAmount((long)this.MaxHp, (long)this.Hp);
                    }
                    else if (!_isUnionBurstLifeSteal)
                        this.lifeGauge.SetNormalizedLifeAmount(NormalizedHP);
                    this.OnLifeAmmountChange.Call<float>(NormalizedHP);
                }*/
                float NormalizedHP = (float)(long)this.Hp / (float)(long)this.MaxHp;
                this.OnLifeAmmountChange.Call<float>(NormalizedHP);
                string des = "目标HP回复<color=#54FF4F>" + _value + "</color>点";
                action?.Invoke(des);
                MyOnLifeChanged?.Invoke(UnitId,NormalizedHP,(int)Hp,0, BattleHeaderController.CurrentFrameCount,des);
                if (_isUnionBurstLifeSteal)
                {
                    this.unionburstLifeStealNum += (long)_value;
                }
                else
                {
                    if (_useNumberEffect)
                    {
                        UIManager.SetHealNumber(gameObject.transform.position, (int)_value);
                    }   //this.createHealNumEffect(_value, _target);
                    if (!_isEffect)
                        return;
                    //_target.RecoveryEffect(_source, false, this.battleEffectPool, _isRegenerate);
                }
            }
        }

        /*private void createHealNumEffect(int _value, BasePartsData _targetParts)
        {
            GameObject nmjamhcpmdf = Singleton<LCEGKJFKOPD>.Instance.NMJAMHCPMDF;
            DamageEffectCtrl component = this.battleEffectPool.LoadNumberEffect(nmjamhcpmdf).GetComponent<DamageEffectCtrl>();
            float time = Time.time;
            if ((double)this.lastHpHealTime + 0.449999988079071 < (double)time)
                this.hpHealComboCount = 0;
            else
                ++this.hpHealComboCount;
            this.lastHpHealTime = time;
            component.transform.localScale = nmjamhcpmdf.transform.localScale;
            component.SetPosition(_targetParts, Vector3.up * 0.1f * (float)this.hpHealComboCount, 1f);
            this.sortOffsetResetTimer = 0.6f;
            ++this.hitEffectSortOffset;
            component.SetSortOrder((this.IsFront ? 11500 : 0) + 11250 + this.hitEffectSortOffset);
            component.SetDamageText(_value.ToString(), eDamageEffectColor.GREEN);
        }*/

        public void ChargeEnergy(
          eSetEnergyType _setEnergyType,
          float _energy,
          bool _hasEffect = false,
          UnitCtrl _source = null,
          bool _hasNumberEffect = true,
          eEffectType _effectType = eEffectType.COMMON,
          bool _useRecoveryRate = true,
          bool _isRegenerate = false,
          float _multipleValue = 1f,
          Action<string> action = null)
        {
            if (this.IsAbnormalState(UnitCtrl.eAbnormalState.FEAR) && (_setEnergyType == eSetEnergyType.BY_ATK || _setEnergyType == eSetEnergyType.KILL_BONUS))
                return;
            float num = ((double)_energy > 0.0 & _useRecoveryRate ? (float)(((double)(int)this.EnergyRecoveryRateZero + 100.0) / 100.0) * _energy : _energy) * _multipleValue;
            action?.Invoke("目标能量增加<color=#4C5FFF>" + num + "</color>点");
            this.SetEnergy(this.Energy + num, _setEnergyType, _source);
            //GameObject MDOJNMEMHLN = (double)_energy >= 0.0 ? Singleton<LCEGKJFKOPD>.Instance.NMJAMHCPMDF : Singleton<LCEGKJFKOPD>.Instance.OJCMBLJEGHF;
            if (_hasNumberEffect)
            {
                /*float time = Time.time;
                if ((double)this.lastEnergyHealTime + 0.449999988079071 < (double)time)
                    this.energyHealComboCount = 0;
                else
                    ++this.energyHealComboCount;
                this.lastEnergyHealTime = time;
                DamageEffectCtrl component = this.battleEffectPool.LoadNumberEffect(MDOJNMEMHLN).GetComponent<DamageEffectCtrl>();
                component.SetPosition(this.GetFirstParts(true), Vector3.up * 0.1f * (float)this.energyHealComboCount, 1f);
                component.SetSortOrder((this.IsFront ? 11500 : 0) + 11250);
                component.SetDamageText(((int)num).ToString(), eDamageEffectColor.BLUE);*/
                UIManager.SetEnergyNumber(gameObject.transform.position, (int)num);
            }
            if (!_hasEffect || _effectType != eEffectType.COMMON)
                return;
            //this.GetFirstParts(true).RecoveryEffect(this, true, this.battleEffectPool, _isRegenerate);
        }

        /*public void ShowLifeStealNum()
        {
            if (this.unionburstLifeStealNum == 0L)
                return;
            this.createHealNumEffect((int)this.unionburstLifeStealNum, this.GetFirstParts(true));
            if ((UnityEngine.Object)this.lifeGauge != (UnityEngine.Object)null)
                this.lifeGauge.SetNormalizedLifeAmount((float)(long)this.Hp / (float)(long)this.MaxHp);
            this.unionburstLifeStealNum = 0L;
        }*/

        public void ResetTotalDamage()
        {
            if (!this.IsPartsBoss)
            {
                this.GetFirstParts(true).ResetTotalDamage();
            }
            else
            {
                for (int index = 0; index < this.BossPartsListForBattle.Count; ++index)
                    this.BossPartsListForBattle[index].ResetTotalDamage();
            }
        }

        public bool IsScaleChangeTarget { get; set; }

        public void StartScaleChange(
          List<ScaleChanger> _scaleChangers,
          float _startTime,
          float _blackoutTime)
        {
            if (_scaleChangers.Count == 0)
                return;
            Vector3 _defaultScale = new Vector3((this.IsLeftDir || this.IsForceLeftDir ? -1f : 1f) * this.Scale, Mathf.Abs(this.Scale));
            this.AppendCoroutine(this.updateScaleChange(++this.changeScaleId, _scaleChangers, _startTime, _defaultScale, _blackoutTime), ePauseType.NO_DIALOG);
        }

        public IEnumerator updateScaleChange(
          int _changeScaleId,
          List<ScaleChanger> _scaleChangers,
          float _startTime,
          Vector3 _defaultScale,
          float _blackoutTime)
        {
            float timer = 0.0f;
            int currentIndex = 0;
            ScaleChanger currentData = _scaleChangers[currentIndex];
            CustomEasing currentEasing = new CustomEasing(currentData.Easing, 1f, currentData.Scale, currentData.Duration);
            while (true)
            {
                do
                {
                    while (this.battleManager.IsPlayingPrincessMovie || this.battleManager.isPrincessFormSkill)
                        yield return (object)null;
                    if (_changeScaleId != this.changeScaleId)
                    {
                        yield break;
                    }
                    else
                    {
                        timer += this.battleManager.DeltaTime_60fps;
                        if ((double)timer < (double)currentData.Time - (double)currentData.Duration)
                        {
                            if ((double)timer >= (double)_startTime)
                                yield return (object)null;
                        }
                        else if ((double)timer < (double)currentData.Time)
                        {
                            float curVal = currentEasing.GetCurVal(this.battleManager.DeltaTime_60fps);
                            this.GetCurrentSpineCtrl().transform.localScale = _defaultScale * curVal;
                        }
                        else
                            goto label_11;
                    }
                }
                while ((double)timer < (double)_startTime);
                yield return (object)null;
                continue;
            label_11:
                this.GetCurrentSpineCtrl().transform.localScale = _defaultScale * currentData.Scale;
                ++currentIndex;
                if (_scaleChangers.Count != currentIndex && (double)timer <= (double)_blackoutTime + (double)_startTime)
                {
                    float scale = currentData.Scale;
                    currentData = _scaleChangers[currentIndex];
                    currentEasing = new CustomEasing(currentData.Easing, scale, currentData.Scale, currentData.Duration);
                }
                else
                    break;
            }
            while (_changeScaleId == this.changeScaleId)
            {
                if ((double)timer > (double)_blackoutTime + (double)_startTime)
                {
                    this.GetCurrentSpineCtrl().transform.localScale = _defaultScale;
                    break;
                }
                yield return (object)null;
                timer += this.battleManager.DeltaTime_60fps;
            }
        }

        private bool searchAreaChangeRunning { get; set; }

        private float searchAreaChangeTimer { get; set; }

        private bool searchAreaChangeReduceEnergy { get; set; }

        private float searchAreaEnergyRate { get; set; }

        public void ChangeSearchArea(
          float value,
          float _time,
          bool _reduceEnergy,
          float _reduceEnergyRate)
        {
            this.SearchAreaSize = value;
            this.searchAreaChangeTimer = _time;
            this.searchAreaChangeReduceEnergy = _reduceEnergy;
            this.searchAreaEnergyRate = _reduceEnergyRate;
            this.IsReduceEnergyDictionary[UnitCtrl.eReduceEnergyType.SEARCH_AREA] = _reduceEnergy;
            if (this.searchAreaChangeRunning)
                return;
            this.searchAreaChangeRunning = true;
            this.AppendCoroutine(this.updateChangeSearchArea(), ePauseType.SYSTEM, this);
        }

        private IEnumerator updateChangeSearchArea()
        {
            while (true)
            {
                if (this.searchAreaChangeReduceEnergy)
                {
                    this.SetEnergy(this.Energy - this.DeltaTimeForPause * this.searchAreaEnergyRate, eSetEnergyType.BY_MODE_CHANGE);
                    if ((double)this.Energy == 0.0 || this.IsDead)
                        break;
                }
                else
                {
                    this.searchAreaChangeTimer -= this.DeltaTimeForPause;
                    if ((double)this.searchAreaChangeTimer < 0.0)
                        goto label_5;
                }
                yield return (object)null;
            }
            this.IsReduceEnergyDictionary[UnitCtrl.eReduceEnergyType.SEARCH_AREA] = false;
            this.SearchAreaSize = this.StartSearchAreaSize;
            this.searchAreaChangeRunning = false;
            yield break;
        label_5:
            this.SearchAreaSize = this.StartSearchAreaSize;
            this.searchAreaChangeRunning = false;
        }

        public int GetSortOrderConsiderBlackout()
        {
            int sSortOrder = this.m_sSortOrder;
            if (this.battleManager.IsSkillExeUnit(this))
                sSortOrder -= 11500;
            return sSortOrder;
        }

        public int SortOrder
        {
            get => this.m_sSortOrder;
            set
            {
                this.m_sSortOrder = value;
                if (!((UnityEngine.Object)this.GetCurrentSpineCtrl() != (UnityEngine.Object)null))
                    return;
                //this.GetCurrentSpineCtrl().Depth = value;
            }
        }

        public void SetSortOrderBack()
        {
            for (int index = 0; index < this.SortFrontDiappearAttachmentChangeDataList.Count; ++index)
            {
                AttachmentChangeData attachmentChangeData = this.SortFrontDiappearAttachmentChangeDataList[index];
                (this.UnitSpineCtrl.skeleton.skin == null ? this.UnitSpineCtrl.skeleton.data.defaultSkin : this.UnitSpineCtrl.skeleton.skin).AddAttachment(attachmentChangeData.TargetIndex, attachmentChangeData.TargetAttachmentName, attachmentChangeData.TargetAttachment);
                this.UnitSpineCtrl.skeleton.slots.Items[attachmentChangeData.TargetIndex].attachment = attachmentChangeData.TargetAttachment;
            }
            this.IsFront = false;
            this.SortOrder = BattleDefine.GetUnitSortOrder(this);
            //this.lifeGauge.SetSortOrderBack();
            //int index1 = 0;
            //for (int count = this.CircleEffectList.Count; index1 < count; ++index1)
            //    this.CircleEffectList[index1].SetSortOrderBack();
            for (int index2 = 0; index2 < this.BossPartsListForBattle.Count; ++index2)
                this.BossPartsListForBattle[index2].IsBlackoutTarget = false;
            this.OnIsFrontFalse.Call();
        }

        public void SetSortOrderFront()
        {
            for (int index = 0; index < this.SortFrontDiappearAttachmentChangeDataList.Count; ++index)
            {
                AttachmentChangeData attachmentChangeData = this.SortFrontDiappearAttachmentChangeDataList[index];
                (this.UnitSpineCtrl.skeleton.skin == null ? this.UnitSpineCtrl.skeleton.data.defaultSkin : this.UnitSpineCtrl.skeleton.skin).AddAttachment(attachmentChangeData.TargetIndex, attachmentChangeData.TargetAttachmentName, attachmentChangeData.AppliedAttachment);
                this.UnitSpineCtrl.skeleton.slots.Items[attachmentChangeData.TargetIndex].attachment = attachmentChangeData.AppliedAttachment;
            }
            this.IsFront = true;
            this.SortOrder = BattleDefine.GetUnitSortOrder(this) + 11500;
            //this.lifeGauge.SetSortOrderFront();
            //int index1 = 0;
            //for (int count = this.CircleEffectList.Count; index1 < count; ++index1)
            //    this.CircleEffectList[index1].SetSortOrderFront();
        }

        public void SetSortOrderSuperFront()
        {
            if (!this.IsFront)
                return;
            this.SortOrder = 22350;
            //this.lifeGauge.SetSortOrderFront();
            //int index = 0;
            //for (int count = this.CircleEffectList.Count; index < count; ++index)
            //    this.CircleEffectList[index].SetSortOrderFront();
        }

        public void SetSortOrderSuperBack()
        {
            if (!this.IsFront)
                return;
            this.SortOrder = 11850;
            //this.lifeGauge.SetSortOrderFront();
            //int index = 0;
           // for (int count = this.CircleEffectList.Count; index < count; ++index)
            //    this.CircleEffectList[index].SetSortOrderFront();
        }

        public void StartChangeSortOrder(
          List<ChangeSortOrderData> _changeSortOrderDatas,
          float _startTime)
        {
            if (_changeSortOrderDatas.Count == 0)
                return;
            this.AppendCoroutine(this.updateChangeSortOrder(_changeSortOrderDatas, _startTime, ++this.changeSortOrderId), ePauseType.NO_DIALOG);
        }

        private IEnumerator updateChangeSortOrder(
          List<ChangeSortOrderData> _changeSortOrderDatas,
          float _startTime,
          int _thisId)
        {
            float timer = _startTime;
            int currentIndex = 0;
            ChangeSortOrderData currentData = _changeSortOrderDatas[currentIndex];
            while (_thisId == this.changeSortOrderId)
            {
                timer += this.battleManager.DeltaTime_60fps;
                if ((double)timer < (double)currentData.Time)
                {
                    yield return (object)null;
                }
                else
                {
                    switch (currentData.SortType)
                    {
                        case ChangeSortOrderData.eSortType.DEFAULT:
                            if (this.IsFront)
                            {
                                this.SetSortOrderFront();
                                break;
                            }
                            this.SetSortOrderBack();
                            break;
                        case ChangeSortOrderData.eSortType.FRONT:
                            this.SetSortOrderSuperFront();
                            break;
                        case ChangeSortOrderData.eSortType.BACK:
                            this.SetSortOrderSuperBack();
                            break;
                    }
                    ++currentIndex;
                    if (_changeSortOrderDatas.Count == currentIndex)
                        break;
                    currentData = _changeSortOrderDatas[currentIndex];
                }
            }
        }

        public int LastVoiceFrame { get; set; }

        /*public void PlayVoice(
          SoundManager.eVoiceType _voiceType,
          int _index,
          bool _lastSuffixIsRandom,
          bool _useIndex2)
        {
            if (this.battleManager.FGFCBPPIMHG && _voiceType != SoundManager.eVoiceType.CUT_IN && _voiceType != SoundManager.eVoiceType.UNION_BURST)
                return;
            if (this.VoiceSource.status == CriAtomSource.Status.Playing || this.LastVoiceFrame == this.battleManager.JJCJONPDGIM)
                this.StartCoroutine(this.FadeoutVoice((System.Action)(() => this.PlayVoiceImpl(_voiceType, _index, _lastSuffixIsRandom, _useIndex2))));
            else
                this.PlayVoiceImpl(_voiceType, _index, _lastSuffixIsRandom, _useIndex2);
        }*/

        /*public IEnumerator FadeoutVoice(System.Action _callback)
        {
            CustomEasing easing = new CustomEasing(CustomEasing.eType.outQuad, 1f, 0.0f, 0.3f);
            while (easing.IsMoving)
            {
                this.VoiceSource.volume = easing.GetCurVal(Time.deltaTime);
                yield return (object)null;
            }
            _callback.Call();
        }*/

        /*public void PlayVoiceImpl(
          SoundManager.eVoiceType _voiceType,
          int _index,
          bool _lastSuffixIsRandom,
          bool _useIndex2)
        {
            if (this.ToadDatas.Count > 0 || this.IsShadow && !BattleDefine.VoiceJudgeDataDictionary[_voiceType].PlayIfShadow || !this.judgePlayVoice(_voiceType))
                return;
            if (this.VoiceSource.status == CriAtomSource.Status.Playing)
                this.VoiceSource.Stop();
            this.VoiceSource.volume = 1f;
            this.LastVoiceFrame = this.battleManager.JJCJONPDGIM;
            int num = this.SoundUnitId;
            if (this.judgeRarity6() && BattleDefine.VoiceJudgeDataDictionary[_voiceType].Star6Voice)
                num = UnitUtility.GetSkinId(this.SoundUnitId, 6);
            int _unitId = this.enemyVoiceId == 0 ? num : this.enemyVoiceId;
            if (_useIndex2)
                this.soundManager.PlaySkillVoice(this.VoiceSource, _unitId, _voiceType, _index, BattleManager.HeldRandom(1, 4));
            else if (_lastSuffixIsRandom)
                this.soundManager.PlayVoiceByOuterSource(this.VoiceSource, _unitId, _voiceType, this.getNextVoiceSuffix(_voiceType) + 1);
            else
                this.soundManager.PlayVoiceByOuterSource(this.VoiceSource, _unitId, _voiceType, _index);
        }*/

        //private bool judgePlayVoice(SoundManager.eVoiceType _voiceType) => !this.battleManager.IsDefenceReplayMode && this.IsOther && this.enemyVoiceId != 0 || (this.IsOther != !this.battleManager.IsDefenceReplayMode || BattleDefine.VoiceJudgeDataDictionary[_voiceType].PlayIfOther);

        /*private int getNextVoiceSuffix(SoundManager.eVoiceType _voiceType)
        {
            if (this.voiceSuffixDic.ContainsKey(_voiceType))
            {
                this.voiceSuffixDic[_voiceType]++;
                this.voiceSuffixDic[_voiceType] %= 3;
            }
            else
                this.voiceSuffixDic.Add(_voiceType, BattleManager.HeldRandom(0, 3));
            return this.voiceSuffixDic[_voiceType];
        }*/

        /*private void playSeWithMotion(
          BattleSpineController controller,
          eSpineCharacterAnimeId animeId,
          int index1 = -1,
          int index2 = -1,
          int index3 = -1,
          bool isLoop = false,
          float _startTime = 0.0f)
        {
            if ((UnityEngine.Object)this.SeSource != (UnityEngine.Object)null)
            {
                this.SeSource.Stop();
                this.SeSource.Pause(false);
            }
            string _cueSheetName = UnitCtrl.ConvertToSkillCueSheetName(this.SoundUnitId);
            string _cueName = controller.ConvertAnimeIdToAnimeName(animeId, index1, index2, index3);
            switch (animeId)
            {
                case eSpineCharacterAnimeId.ATTACK:
                    int num1 = _cueName.IndexOf("_");
                    if (0 <= num1 && num1 <= 2)
                    {
                        _cueSheetName = SoundManager.SE_BATTLE_CUESHEET;
                        break;
                    }
                    break;
                case eSpineCharacterAnimeId.DIE:
                    int num2 = _cueName.IndexOf("_");
                    if (0 <= num2 && num2 <= 2)
                    {
                        _cueSheetName = SoundManager.SE_BATTLE_CUESHEET;
                        _cueName = _cueName.Substring(num2 + 1);
                        break;
                    }
                    break;
            }
            UnitCtrl.EnchantSeDirectionToSeSource(this.SeSource, this.IsLeftDir);
            UnitCtrl.EnchantSeCutInToSeSource(this.SeSource, (double)_startTime != 0.0);
            UnitCtrl.EnchantStartTimeToSeSource(this.SeSource, _startTime);
            this.soundManager.PlaySeByOuterSource(this.SeSource, _cueSheetName: _cueSheetName, _cueName: _cueName);
        }*/

        /*public void PauseSound(bool _isPause)
        {
            if (!((UnityEngine.Object)this.SeSource != (UnityEngine.Object)null))
                return;
            this.SeSource.Pause(_isPause);
        }*/

        /*public void PauseVoice(bool _isPause)
        {
            if (!((UnityEngine.Object)this.VoiceSource != (UnityEngine.Object)null))
                return;
            this.VoiceSource.Pause(_isPause);
        }*/

        //public static string ConvertToSkillCueSheetName(int _unitId) => "se_btl_{0}".Format((object)_unitId);

        /*public static void EnchantSeDirectionToSeSource(CriAtomSource _soundSource, bool _isEnemySide)
        {
            if ((UnityEngine.Object)_soundSource == (UnityEngine.Object)null || _soundSource.player == null)
                return;
            _soundSource.player.SetSelectorLabel("Side", _isEnemySide ? "EnemySide" : "FriendSide");
        }*/

        /*public static void EnchantSeCutInToSeSource(CriAtomSource _soundSource, bool _isCutInPlayed)
        {
            if ((UnityEngine.Object)_soundSource == (UnityEngine.Object)null || _soundSource.player == null)
                return;
            _soundSource.player.SetSelectorLabel("Cutin", _isCutInPlayed ? "On" : "Off");
        }*/

        /*public static void EnchantStartTimeToSeSource(CriAtomSource _soundSource, float _startTime)
        {
            if ((UnityEngine.Object)_soundSource == (UnityEngine.Object)null || _soundSource.player == null)
                return;
            _soundSource.startTime = (int)((double)_startTime * 1000.0);
        }*/

        public void PlayUbChainVoice()
        {
            /*if (this.battleTimeScale.SpeedUpFlag || !((UnityEngine.Object)this.battleManager.HELHEEOHPFO == (UnityEngine.Object)this))
                return;
            List<UnitCtrl> all = (this.IsOther ? this.battleManager.EnemyList : this.battleManager.UnitList).FindAll((Predicate<UnitCtrl>)(unit => (UnityEngine.Object)unit != (UnityEngine.Object)this && (long)unit.Hp > 0L && !unit.IsSummonOrPhantom));
            if (this.UbResponceVoiceType == eUbResponceVoiceType.THANKS)
                all = all.FindAll((Predicate<UnitCtrl>)(e => e.ThanksTargetUnitId == this.UnitId));
            if (all.Count > 0)
            {
                switch (this.UbResponceVoiceType)
                {
                    case eUbResponceVoiceType.APPLOUD:
                        BattleVoiceUtility.PlayDialogueVoice(SoundManager.eVoiceType.APPLAUD, this, all, this.cutinVoiceStatus.GroupIdSet.GroupId, this.cutinVoiceStatus.GroupIdSet.GroupUnitId);
                        break;
                    case eUbResponceVoiceType.THANKS:
                        BattleVoiceUtility.PlayDialogueVoice(SoundManager.eVoiceType.THANKS, this, all, this.cutinVoiceStatus.GroupIdSet.GroupId, this.cutinVoiceStatus.GroupIdSet.GroupUnitId);
                        break;
                }
            }
            this.battleManager.HELHEEOHPFO = (UnitCtrl)null;*/
        }

        /*private void playRetireVoice()
        {
            bool flag = true;
            if (this.battleManager.IsSpecialBattle)
            {
                flag = false;
                if (this.IsOther || !this.battleManager.GetPlayNext())
                    flag = true;
                else if (this.battleManager.GetContinuousUnits().Find((Predicate<ContinuousUnit>)(e => e.UnitId == this.UnitId)).Hp > 0)
                    flag = true;
            }
            if (!flag || this.IsOther != this.battleManager.IsDefenceReplayMode && this.enemyVoiceId == 0 || (double)Time.time - (double)this.battleManager.IDFHINDNPKK <= 0.449999988079071 && this.enemyVoiceId == 0)
                return;
            this.battleManager.IDFHINDNPKK = Time.time;
            this.PlayVoice(SoundManager.eVoiceType.RETIRE, 0, true, false);
        }*/

        /*private void playDamageVoice()
        {
            if (this.IsOther != this.battleManager.IsDefenceReplayMode && this.enemyVoiceId == 0 || (double)Time.time - (double)this.battleManager.GGCJPDLOAKI <= 0.449999988079071 && this.enemyVoiceId == 0)
                return;
            this.battleManager.GGCJPDLOAKI = Time.time;
            this.PlayVoice(SoundManager.eVoiceType.DAMAGE, 0, true, false);
        }*/

        /*public void StopVoiceExceptUbVoice()
        {
            if (!((UnityEngine.Object)this.VoiceSource != (UnityEngine.Object)null) || !this.gameObject.activeSelf || this.VoiceSource.cueName.Contains("ub"))
                return;
            this.StartCoroutine(this.FadeoutVoice((System.Action)(() => this.VoiceSource.Stop())));
        }*/

        private Dictionary<int, float> castTimeDictionary { get; set; }

        public bool UnionBurstAnimeEndForIfAction { get; set; }

        public int UnionBurstSkillId { get; set; }

        public bool IsStartVoicePlay { get; set; }

        public bool ModeChangeEnd { get; set; }

        private float unionBurstSkillAreaWidth { get; set; }

        public bool GameStartDone { get; set; }

        private int dieCoroutineId { get; set; }

        public System.Action<float> OnUpdateWhenIdle { get; set; }

        public System.Action OnBreakAll { get; set; }

        private int damageCoroutineId { get; set; }

        private int walkCoroutineId { get; set; }

        public bool EnemyPointDone { get; set; }

        public int EnemyPoint { get; private set; }

        private int specialBattlePurposeHp { get; set; }

        public int PositionOrder { get; set; }

        public System.Action OnMotionPrefixChanged { get; set; }

        private bool multiTargetDone { get; set; }

        public void SetState(UnitCtrl.ActionState _state, int _nextSkillId = 0, int _skillId = 0, bool _quiet = false)
        {
            string des = _state == ActionState.SKILL ? (unitActionController.skillDictionary.TryGetValue(_skillId,out var value)?value.SkillName:"UnknownSkill") : "";

            MyOnChangeState?.Invoke(UnitId, _state, BattleHeaderController.CurrentFrameCount,des);
            /*switch (UnitId)
            {
                case 101701:
                    Debug.Log(UnitName + "--" + BattleHeaderController.CurrentFrameCount + "--" + _state.ToString());
                    break;
                case 102801:
                    Debug.LogWarning(UnitName + "--" + BattleHeaderController.CurrentFrameCount + "--" + _state.ToString());
                    break;
                case 104301:
                    Debug.LogError(UnitName + "--" + BattleHeaderController.CurrentFrameCount + "--" + _state.ToString());
                    break;

            }*/
            if (this.GameStartDone && this.IsPartsBoss && !this.multiTargetDone)
            {
                
                this.multiTargetDone = true;
                /*SaveDataManager instance = ManagerSingleton<SaveDataManager>.Instance;
                if ((instance.MultiTargetConfirm == 0 ? 1 : (instance.MultiTargetConfirm != 2 ? 0 : (!instance.MultiTargetFirst ? 1 : 0))) != 0)
                {
                    instance.MultiTargetFirst = true;
                    SingletonMonoBehaviour<UnitUiCtrl>.Instance.HidePopUp();
                    this.battleManager.SetSkillScreen(true);
                    this.battleManager.GamePause(true, false);
                    this.battleManager.BossUnit.SetSortOrderFront();
                    this.soundManager.PlaySe(eSE.MULTI_TARGETS_START);
                    ManagerSingleton<ResourceManager>.Instance.InstantiateAndGetComponent<Animator>(eResourceId.ANIM_MULTI_TARGET_APPEAR, this.battleManager.UnitUiCtrl.transform).transform.position = this.battleManager.BossUnit.BottomTransform.position + this.battleManager.BossUnit.FixedCenterPos;
                    this.Timer(1f, (System.Action)(() =>
                   {
                       if (this.IsPartsBoss)
                           this.battleManager.LOGNEDLPEIJ = false;
                       this.battleManager.SetSkillScreen(false);
                       this.battleManager.BossUnit.SetSortOrderBack();
                       if (SingletonMonoBehaviour<BattleHeaderController>.Instance.IsPaused)
                           return;
                       this.battleManager.GamePause(false, false);
                   }));
                }
                else if*/
                if (this.IsPartsBoss)
                    this.battleManager.LOGNEDLPEIJ = false;
                for (int index = 0; index < this.BossPartsListForBattle.Count; ++index)
                {
                    //MultiTargetCursor cursor = ManagerSingleton<ResourceManager>.Instance.InstantiateAndGetComponent<MultiTargetCursor>(this.UseTargetCursorOver ? eResourceId.ANIM_MULTI_TARGET_CURSOR_OVER : eResourceId.ANIM_MULTI_TARGET_CURSOR, this.battleManager.UnitUiCtrl.transform);
                    PartsData _data = this.BossPartsListForBattle[index];
                    //cursor.transform.position = _data.GetBottomTransformPosition();
                    //cursor.Panel.sortingOrder = this.GetCurrentSpineCtrl().Depth + (this.UseTargetCursorOver ? 100 : -100);
                    //_data.MultiTargetCursor = cursor;
                    //this.battleManager.AppendCoroutine(_data.TrackBottom(), ePauseType.SYSTEM, this);
                    if (_data.IsBreak)
                    {
                        this.AppendBreakLog(_data.BreakSource);
                        _data.IsBreak = true;
                        _data.OnBreak.Call();
                        _data.SetBreak(true, null);// this.battleManager.UnitUiCtrl.transform);
                        this.AppendCoroutine(this.UpdateBreak(_data.BreakTime, _data), ePauseType.SYSTEM);
                    }
                    this.OnIsFrontFalse += (System.Action)(() =>
                   {
                       if (this.UseTargetCursorOver)
                           return;
                       //cursor.Panel.sortingOrder = this.GetCurrentSpineCtrl().Depth - 100;
                   });
                }
            }
            if (_state != UnitCtrl.ActionState.IDLE && _state != UnitCtrl.ActionState.WALK)
            {
                this.CancelByConvert = false;
                this.CancelByToad = false;
            }
            if (_state == UnitCtrl.ActionState.SKILL_1)
            {
                _skillId = this.UnionBurstSkillId;
                BattleLogIntreface battleLog = this.battleLog;
                UnitCtrl unitCtrl1 = this;
                UnitCtrl unitCtrl2 = this;
                long KGNFLOPBOMB = (long)_skillId;
                long currentState = (long)this.CurrentState;
                UnitCtrl JELADBAMFKH = unitCtrl1;
                UnitCtrl LIMEKPEENOB = unitCtrl2;
                battleLog.AppendBattleLog(eBattleLogType.BUTTON_TAP, 0, KGNFLOPBOMB, currentState, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
            }
            BattleLogIntreface battleLog1 = this.battleLog;
            UnitCtrl unitCtrl3 = this;
            UnitCtrl unitCtrl4 = this;
            int HLIKLPNIOKJ = (int)_state;
            long KGNFLOPBOMB1 = (long)_skillId;
            long currentState1 = (long)this.CurrentState;
            UnitCtrl JELADBAMFKH1 = unitCtrl3;
            UnitCtrl LIMEKPEENOB1 = unitCtrl4;
            battleLog1.AppendBattleLog(eBattleLogType.SET_STATE, HLIKLPNIOKJ, KGNFLOPBOMB1, currentState1, 0, 0, JELADBAMFKH: JELADBAMFKH1, LIMEKPEENOB: LIMEKPEENOB1);
            /*for (int index = this.idleEffectsObjs.Count - 1; index >= 0; --index)
            {
                this.idleEffectsObjs[index].SetTimeToDie(true);
                this.idleEffectsObjs.RemoveAt(index);
            }*/
            if (!this.Pause)
            {
                if (_state == UnitCtrl.ActionState.IDLE && this.IsAbnormalState(UnitCtrl.eAbnormalState.HASTE))
                    this.GetCurrentSpineCtrl().SetTimeScale(2f);
                else
                    this.GetCurrentSpineCtrl().Resume();
            }
            this.setRecastTime(_nextSkillId);
            this.SetDirectionAuto();
            switch (_state)
            {
                case UnitCtrl.ActionState.IDLE:
                    this.setStateIdle();
                    break;
                case UnitCtrl.ActionState.ATK:
                    this.setStateAttack();
                    break;
                case UnitCtrl.ActionState.SKILL_1:
                    this.setStateSkill1();
                    break;
                case UnitCtrl.ActionState.SKILL:
                    this.setStateSkill(_skillId);
                    break;
                case UnitCtrl.ActionState.WALK:
                    this.setStateWalk();
                    break;
                case UnitCtrl.ActionState.DAMAGE:
                    this.setStateDamage(_quiet);
                    break;
                case UnitCtrl.ActionState.DIE:
                    this.setStateDie();
                    break;
                case UnitCtrl.ActionState.GAME_START:
                    this.isAwakeMotion = true;
                    this.SetLeftDirection(true);
                    this.PlayAnime(eSpineCharacterAnimeId.GAME_START, _isLoop: false, _quiet: true);
                    this.CurrentState = _state;
                    eBattleCategory jiliicmhlch = this.battleManager.BattleCategory;
                    /*if ((jiliicmhlch == eBattleCategory.TOWER_EX ? 1 : (jiliicmhlch == eBattleCategory.TOWER_EX_REPLAY ? 1 : 0)) != 0 && this.battleManager.GetCurrentTowerExPartyIndex() > 0)
                    {
                        this.AppendCoroutine(this.updateGameStartMotionIdle(), ePauseType.SYSTEM);
                        break;
                    }*/
                    this.AppendCoroutine(this.updateGameStart(), ePauseType.SYSTEM);
                    break;
            }
        }

        private void setRecastTime(int skillId)
        {
            switch (skillId)
            {
                case 0:
                    return;
                case 1:
                    this.m_fCastTimer = (ObscuredFloat)this.m_fAtkRecastTime;
                    break;
                default:
                    this.m_fCastTimer = (ObscuredFloat)this.castTimeDictionary[skillId];
                    break;
            }
            if (!this.IsAbnormalState(UnitCtrl.eAbnormalState.SLOW) && !this.IsAbnormalState(UnitCtrl.eAbnormalState.HASTE) || (double)this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.SPEED].MainValue <= 0.01)
                return;
            this.m_fCastTimer = (ObscuredFloat)((float)this.m_fCastTimer / this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.SPEED].MainValue);
        }

        private IEnumerator updateGameStartMotionIdle()
        {
            BattleSpineController currentSpineCtrl = this.GetCurrentSpineCtrl();
            float endTime = currentSpineCtrl.state.Data.skeletonData.FindAnimation(currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.GAME_START)).Duration + this.BossAppearDelay;
            currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.IDLE);
            while ((double)endTime > 0.0)
            {
                endTime -= this.DeltaTimeForPause;
                yield return (object)null;
            }
            this.GameStartDone = true;
            this.SetState(UnitCtrl.ActionState.IDLE);
        }

        private IEnumerator updateGameStart()
        {
            //MyOnChangeState?.Invoke(UnitId, CurrentState, BattleHeaderController.CurrentFrameCount);
            UnitCtrl unitCtrl = this;
            float time = 0.0f;
            //bool[] shakeStarted = new bool[unitCtrl.gameStartShakes.Count];
            //if (unitCtrl.battleManager.GetPurpose() == eHatsuneSpecialPurpose.SHIELD || unitCtrl.battleManager.GetPurpose() == eHatsuneSpecialPurpose.ABSORBER && unitCtrl.battleManager.FICLPNJNOEP < unitCtrl.battleManager.GetPurposeCount())
            // unitCtrl.AppendCoroutine(unitCtrl.CreatePrefabWithTime(unitCtrl.gameStartEffects, _isShieldEffect: true), ePauseType.SYSTEM);
            while (true)
            {
                unitCtrl.GetCurrentSpineCtrl().Pause();
                time += unitCtrl.DeltaTimeForPause;
                if ((double)time <= (double)unitCtrl.BossAppearDelay)
                    yield return (object)null;
                else
                    break;
            }
            //if (unitCtrl.enemyVoiceId != 0)
            //    BattleVoiceUtility.PlayWaveStartVoice(unitCtrl);
            unitCtrl.GetCurrentSpineCtrl().Resume();
            time = 0.0f;
            unitCtrl.PlayAnime(eSpineCharacterAnimeId.GAME_START, _isLoop: false);
            //unitCtrl.AppendCoroutine(unitCtrl.CreatePrefabWithTime(unitCtrl.gameStartEffects), ePauseType.SYSTEM);
            //unitCtrl.AppendCoroutine(unitCtrl.CreatePrefabWithTime(unitCtrl.auraEffects, _isAura: true), ePauseType.SYSTEM);
            while (true)
            {
                time += unitCtrl.DeltaTimeForPause;
                /*for (int index = 0; index < unitCtrl.gameStartShakes.Count; ++index)
                {
                    if (!shakeStarted[index] && (double)unitCtrl.gameStartShakes[index].StartTime < (double)time)
                    {
                        unitCtrl.battleCameraEffect.StartShake(unitCtrl.gameStartShakes[index], (Skill)null, unitCtrl);
                        shakeStarted[index] = true;
                    }
                }*/
                if (!unitCtrl.IsDead)
                {
                    if (unitCtrl.GetCurrentSpineCtrl().IsPlayAnimeBattle && !unitCtrl.CancelByAwake)
                        yield return (object)null;
                    else
                        goto label_18;
                }
                else
                    break;
            }
            unitCtrl.GameStartDone = true;
            if (!unitCtrl.IsDepthBack)
            {
                yield break;
            }
            else
            {
                unitCtrl.IsDepthBack = false;
                unitCtrl.SetSortOrderBack();
                yield break;
            }
        label_18:
            unitCtrl.GameStartDone = true;
            if (unitCtrl.IsDepthBack)
            {
                unitCtrl.IsDepthBack = false;
                unitCtrl.SetSortOrderBack();
            }
            if (!unitCtrl.CancelByAwake)
                unitCtrl.SetState(UnitCtrl.ActionState.IDLE);
        }

        private void setStateSkill(int skillId)
        {
            if (skillId == 0 || (UnityEngine.Object)this.unitActionController == (UnityEngine.Object)null)
                return;
            this.CurrentState = UnitCtrl.ActionState.SKILL;
            this.CurrentSkillId = skillId;
            if ((this.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE) || this.ToadDatas.Count > 0) && this.AttackWhenSilence)
                this.SetState(UnitCtrl.ActionState.ATK);
            else if (this.unitActionController.StartAction(skillId) || this.IsBoss)
            {
                this.ChargeEnergy(eSetEnergyType.BY_ATK, this.skillStackVal, _source: this, _hasNumberEffect: false);
                this.AppendCoroutine(this.updateSkill(skillId), ePauseType.SYSTEM, this);
                //if (!this.voiceTypeDictionary.ContainsKey(skillId) || !this.judgePlayVoice("skill", 2, true, 0.5f))
                 //   return;
                //this.PlayVoice(this.voiceTypeDictionary[skillId].VoiceType, this.voiceTypeDictionary[skillId].SkillNumber * 100, false, true);
            }
            else
                this.SetState(UnitCtrl.ActionState.IDLE);
        }

        private IEnumerator updateSkill(int skillId)
        {
            //string des = "准备释放技能" + unitActionController.skillDictionary[skillId].SkillName;
            //MyOnChangeState?.Invoke(UnitId, CurrentState, BattleHeaderController.CurrentFrameCount,des);
            UnitCtrl _unit = this;
            while (!_unit.CancelByConvert && !_unit.CancelByToad)
            {
                switch (_unit.CurrentState)
                {
                    case UnitCtrl.ActionState.SKILL_1:
                    case UnitCtrl.ActionState.DAMAGE:
                    case UnitCtrl.ActionState.DIE:
                        _unit.unitActionController.CancelAction(skillId);
                        _unit.CancelByConvert = false;
                        _unit.CancelByAwake = false;
                        yield break;
                    default:
                        if (_unit.CancelByAwake && skillId != _unit.CurrentTriggerSkillId)
                        {
                            _unit.unitActionController.CancelAction(skillId);
                            _unit.CancelByAwake = false;
                            _unit.CancelByConvert = false;
                            _unit.CancelByToad = false;
                            yield break;
                        }
                        else if (_unit.unitActionController.HasNextAnime(skillId) && _unit.unitActionController.IsLoopMotionPlaying(skillId))
                            yield break;
                        else if (!_unit.GetCurrentSpineCtrl().IsPlayAnimeBattle || _unit.unitActionController.GetSkillMotionType(skillId) == SkillMotionType.NONE)
                        {
                            if (_unit.unitActionController.IsModeChange(skillId))
                            {
                                _unit.UnitSpineCtrlModeChange.CurColor = _unit.UnitSpineCtrl.CurColor;
                                Color curColor = _unit.UnitSpineCtrl.CurColor;
                                curColor.a = 0.0f;
                                _unit.UnitSpineCtrl.CurColor = curColor;
                                _unit.UnitSpineCtrlModeChange.gameObject.SetActive(true);
                                _unit.MotionPrefix = 1;
                                _unit.OnMotionPrefixChanged.Call();
                                if (_unit.IsFront)
                                    _unit.SetSortOrderFront();
                                else
                                    _unit.SetSortOrderBack();
                                _unit.SetLeftDirection(_unit.IsLeftDir);
                                int skillNum = _unit.unitActionController.GetSkillNum(skillId);
                                if (_unit.UnitSpineCtrlModeChange.IsAnimation(_unit.UnitSpineCtrlModeChange.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SKILL, skillNum, _index3: 1)))
                                {
                                    _unit.PlayAnimeNoOverlap(eSpineCharacterAnimeId.SKILL, skillNum, _index3: 1, _targetCtr: _unit.UnitSpineCtrlModeChange);
                                    _unit.AppendCoroutine(_unit.updateModeChange(), ePauseType.SYSTEM, _unit);
                                    yield break;
                                }
                                else
                                {
                                    _unit.SetState(UnitCtrl.ActionState.IDLE);
                                    yield break;
                                }
                            }
                            else if (_unit.unitActionController.HasNextAnime(skillId))
                            {
                                yield break;
                            }
                            else
                            {
                                _unit.SetState(UnitCtrl.ActionState.IDLE);
                                _unit.CancelByConvert = false;
                                _unit.CancelByAwake = false;
                                _unit.CancelByToad = false;
                                yield break;
                            }
                        }
                        else
                        {
                            
                            yield return (object)null;
                            continue;
                        }
                }
            }
            _unit.unitActionController.CancelAction(skillId);
            _unit.CancelByConvert = false;
            _unit.CancelByAwake = false;
            _unit.CancelByToad = false;
        }

        private void setStateJoy()
        {
            this.PlayAnime(eSpineCharacterAnimeId.JOY_LONG, this.MotionPrefix, _isLoop: false);
            //this.SetEnableColor();
            this.CureAllAbnormalState();
            this.AppendCoroutine(this.updateStateJoy(), ePauseType.SYSTEM);
        }

        private IEnumerator updateStateJoy()
        {
            while (this.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                yield return (object)null;
            this.SetState(UnitCtrl.ActionState.IDLE);
        }

        private void setStateSkill1()
        {
            //this.playUbVoiceWithDelay();
            //this.playUbNameVoiceWithDelay();
            /*if (this.UseUbVoice3and4)
            {
                this.playUbVoice3WithDelay();
                this.playUbVoice4WithDelay();
            }*/
            this.CurrentState = UnitCtrl.ActionState.SKILL_1;
            this.CurrentSkillId = this.UnionBurstSkillId;
            this.battleManager.HELHEEOHPFO = this;
            if (this.unitActionController.GetIsSkillPrincessForm(this.UnionBurstSkillId)) 
            {
                //Debug.LogError("UB特效鸽了！");
                this.princessFormProcessor.StartPrincessFormSkill(this.unitActionController.GetPrincessFormMovieData(this.UnionBurstSkillId));
                switch (UnitId)
                {
                    case 180501:
                        float addtime = 4 * battleManager.DeltaTime_60fps;                        
                        //addtime = addtime / this.abnormalStateCategoryDataDictionary[UnitCtrl.eAbnormalStateCategory.SPEED].MainValue;
                        m_fCastTimer += addtime;
                        break;
                }
            }
            else if (this.unitActionController.StartAction(this.UnionBurstSkillId))
                this.AppendCoroutine(this.updateSkill1(), ePauseType.SYSTEM, this);
            else
                this.SetState(UnitCtrl.ActionState.IDLE);
        }

        private void playUbVoiceWithDelay() { }// => this.playVoiceDataList(!this.battleTimeScale.SpeedUpFlag ? (!this.PlayCutInFlag ? this.NormalSkillVoiceDelay : this.NormalSkillVoiceDelayWithCutIn) : (!this.PlayCutInFlag ? this.SpeedUpSkillVoiceDelay : this.SpeedUpSkillVoiceDelayWithCutIn), 100);

        private void playUbVoice3WithDelay() { }// => this.playVoiceDataList(!this.battleTimeScale.SpeedUpFlag ? (!this.PlayCutInFlag ? this.NormalSkillVoice3Delay : this.NormalSkillVoice3DelayWithCutIn) : (!this.PlayCutInFlag ? this.SpeedUpSkillVoice3Delay : this.SpeedUpSkillVoice3DelayWithCutIn), 300);

        private void playUbVoice4WithDelay() { }// => this.playVoiceDataList(!this.battleTimeScale.SpeedUpFlag ? (!this.PlayCutInFlag ? this.NormalSkillVoice4Delay : this.NormalSkillVoice4DelayWithCutIn) : (!this.PlayCutInFlag ? this.SpeedUpSkillVoice4Delay : this.SpeedUpSkillVoice4DelayWithCutIn), 400);

        /*private void playVoiceDataList(List<VoiceDelayAndEnable> _dataList, int _id)
        {
      for (int index = 0; index < _dataList.Count; ++index)
      {
        VoiceDelayAndEnable _data = _dataList[index];
        if (this.cutinVoiceStatus.IsUseSpecialVoice)
        {
          if (this.cutinVoiceStatus.TimingSet.UseBranch)
          {
            switch (_id)
            {
              case 100:
                _data = this.judgeRarity6() ? this.cutinVoiceStatus.TimingSet.Voice1Star6List[index] : this.cutinVoiceStatus.TimingSet.Voice1List[index];
                break;
              case 200:
                _data = this.judgeRarity6() ? this.cutinVoiceStatus.TimingSet.Voice2Star6List[index] : this.cutinVoiceStatus.TimingSet.Voice2List[index];
                break;
              case 300:
                _data = this.judgeRarity6() ? this.cutinVoiceStatus.TimingSet.Voice3Star6List[index] : this.cutinVoiceStatus.TimingSet.Voice3List[index];
                break;
              case 400:
                _data = this.judgeRarity6() ? this.cutinVoiceStatus.TimingSet.Voice4Star6List[index] : this.cutinVoiceStatus.TimingSet.Voice4List[index];
                break;
            }
          }
          else
          {
            switch (_id)
            {
              case 100:
                _data = this.judgeRarity6() ? this.cutinVoiceStatus.TimingSet.Voice1Star6 : this.cutinVoiceStatus.TimingSet.Voice1;
                break;
              case 200:
                _data = this.judgeRarity6() ? this.cutinVoiceStatus.TimingSet.Voice2Star6 : this.cutinVoiceStatus.TimingSet.Voice2;
                break;
              case 300:
                _data = this.judgeRarity6() ? this.cutinVoiceStatus.TimingSet.Voice3Star6 : this.cutinVoiceStatus.TimingSet.Voice3;
                break;
              case 400:
                _data = this.judgeRarity6() ? this.cutinVoiceStatus.TimingSet.Voice4Star6 : this.cutinVoiceStatus.TimingSet.Voice4;
                break;
            }
          }
        }
        else if (this.judgeRarity6())
        {
          if (this.battleTimeScale.SpeedUpFlag)
          {
            if (this.PlayCutInFlag)
            {
              if (this.UnionBurstPlusTimingWithCutin.DoubleSpeed.UseBranch)
              {
                switch (_id)
                {
                  case 100:
                    _data = this.UnionBurstPlusTimingWithCutin.DoubleSpeed.Voice1Star6List[index];
                    break;
                  case 200:
                    _data = this.UnionBurstPlusTimingWithCutin.DoubleSpeed.Voice2Star6List[index];
                    break;
                  case 300:
                    _data = this.UnionBurstPlusTimingWithCutin.DoubleSpeed.Voice3Star6List[index];
                    break;
                  case 400:
                    _data = this.UnionBurstPlusTimingWithCutin.DoubleSpeed.Voice4Star6List[index];
                    break;
                }
              }
              else
              {
                switch (_id)
                {
                  case 100:
                    _data = this.UnionBurstPlusTimingWithCutin.DoubleSpeed.Voice1Star6;
                    break;
                  case 200:
                    _data = this.UnionBurstPlusTimingWithCutin.DoubleSpeed.Voice2Star6;
                    break;
                  case 300:
                    _data = this.UnionBurstPlusTimingWithCutin.DoubleSpeed.Voice3Star6;
                    break;
                  case 400:
                    _data = this.UnionBurstPlusTimingWithCutin.DoubleSpeed.Voice4Star6;
                    break;
                }
              }
            }
            else if (this.UnionBurstPlusTimingNoCutin.DoubleSpeed.UseBranch)
            {
              switch (_id)
              {
                case 100:
                  _data = this.UnionBurstPlusTimingNoCutin.DoubleSpeed.Voice1Star6List[index];
                  break;
                case 200:
                  _data = this.UnionBurstPlusTimingNoCutin.DoubleSpeed.Voice2Star6List[index];
                  break;
                case 300:
                  _data = this.UnionBurstPlusTimingNoCutin.DoubleSpeed.Voice3Star6List[index];
                  break;
                case 400:
                  _data = this.UnionBurstPlusTimingNoCutin.DoubleSpeed.Voice4Star6List[index];
                  break;
              }
            }
            else
            {
              switch (_id)
              {
                case 100:
                  _data = this.UnionBurstPlusTimingNoCutin.DoubleSpeed.Voice1Star6;
                  break;
                case 200:
                  _data = this.UnionBurstPlusTimingNoCutin.DoubleSpeed.Voice2Star6;
                  break;
                case 300:
                  _data = this.UnionBurstPlusTimingNoCutin.DoubleSpeed.Voice3Star6;
                  break;
                case 400:
                  _data = this.UnionBurstPlusTimingNoCutin.DoubleSpeed.Voice4Star6;
                  break;
              }
            }
          }
          else if (this.PlayCutInFlag)
          {
            if (this.UnionBurstPlusTimingWithCutin.ConstantVelocity.UseBranch)
            {
              switch (_id)
              {
                case 100:
                  _data = this.UnionBurstPlusTimingWithCutin.ConstantVelocity.Voice1Star6List[index];
                  break;
                case 200:
                  _data = this.UnionBurstPlusTimingWithCutin.ConstantVelocity.Voice2Star6List[index];
                  break;
                case 300:
                  _data = this.UnionBurstPlusTimingWithCutin.ConstantVelocity.Voice3Star6List[index];
                  break;
                case 400:
                  _data = this.UnionBurstPlusTimingWithCutin.ConstantVelocity.Voice4Star6List[index];
                  break;
              }
            }
            else
            {
              switch (_id)
              {
                case 100:
                  _data = this.UnionBurstPlusTimingWithCutin.ConstantVelocity.Voice1Star6;
                  break;
                case 200:
                  _data = this.UnionBurstPlusTimingWithCutin.ConstantVelocity.Voice2Star6;
                  break;
                case 300:
                  _data = this.UnionBurstPlusTimingWithCutin.ConstantVelocity.Voice3Star6;
                  break;
                case 400:
                  _data = this.UnionBurstPlusTimingWithCutin.ConstantVelocity.Voice4Star6;
                  break;
              }
            }
          }
          else
          {
            switch (_id)
            {
              case 100:
                _data = this.UnionBurstPlusTimingNoCutin.ConstantVelocity.Voice1Star6;
                break;
              case 200:
                _data = this.UnionBurstPlusTimingNoCutin.ConstantVelocity.Voice2Star6;
                break;
              case 300:
                _data = this.UnionBurstPlusTimingNoCutin.ConstantVelocity.Voice3Star6;
                break;
              case 400:
                _data = this.UnionBurstPlusTimingNoCutin.ConstantVelocity.Voice4Star6;
                break;
            }
          }
        }
        if (_data.Enable)
        {
          if (this.unitActionController.GetIsSkillPrincessForm(this.UnionBurstSkillId) && _data.MovieIndex != 0)
          {
            this.princessFormProcessor.SetMovieVoiceData(_data, _id + index, this.judgeRarity6());
            break;
          }
          this.AppendCoroutine(this.playUbVoiceWithDelay(_data, _id + index), ePauseType.SYSTEM, this);
        }
      }
        }*/

        //private void playUbNameVoiceWithDelay() { }// => this.playVoiceDataList(!this.battleTimeScale.SpeedUpFlag ? (!this.PlayCutInFlag ? this.NormalSkillNameVoiceDelay : this.NormalSkillNameVoiceDelayWithCutIn) : (!this.PlayCutInFlag ? this.SpeedUpSkillNameVoiceDelay : this.SpeedUpSkillNameVoiceDelayWithCutIn), 200);

        /*private IEnumerator playUbVoiceWithDelay(VoiceDelayAndEnable _data, int _index)
        {

          float time = 0.0f;
          Skill skill = this.judgeRarity6() ? this.unitActionController.UnionBurstEvolutionList[0] : this.unitActionController.UnionBurstList[0];
          float delay = _data.Delay;
          while (true)
          {
            time += this.DeltaTimeForPause;
            if ((double) time < (double) delay)
              yield return (object) null;
            else
              break;
          }
          if (skill.EffectBranchId == _index % 100)
            this.PlayVoice(SoundManager.eVoiceType.UNION_BURST, _index, false, false);
        }*/

        /*private IEnumerator playCutInVoiceWithDelay(
          VoiceDelayAndEnable _data,
          int _voiceId,
          bool _withCutin)
        {
          UnitCtrl _targetUnit = this;
          VoiceDelayAndEnable data = _data;
          if (_targetUnit.cutinVoiceStatus.IsUseSpecialVoice)
            data = _targetUnit.judgeRarity6() ? _targetUnit.cutinVoiceStatus.TimingSet.CutinStar6 : _targetUnit.cutinVoiceStatus.TimingSet.Cutin;
          else if (_targetUnit.judgeRarity6())
          {
            VoiceTimingGroup voiceTimingGroup = _withCutin ? _targetUnit.UnionBurstPlusTimingWithCutin : _targetUnit.UnionBurstPlusTimingNoCutin;
            data = (_targetUnit.battleTimeScale.SpeedUpFlag ? voiceTimingGroup.DoubleSpeed : voiceTimingGroup.ConstantVelocity).CutinStar6;
          }
          if (data.Enable)
          {
            float time = 0.0f;
            while (true)
            {
              time += Time.deltaTime;
              if ((double) time < (double) data.Delay)
                yield return (object) null;
              else
                break;
            }
            BattleVoiceUtility.PlayCutinVoice(_targetUnit, _voiceId, _targetUnit.cutinVoiceStatus);
          }
        }*/

        private IEnumerator updateSkill1()
        {
            //MyOnChangeState?.Invoke(UnitId, CurrentState, BattleHeaderController.CurrentFrameCount);
            UnitCtrl _unit = this;
            while (!_unit.CancelByConvert && !_unit.CancelByToad)
            {
                switch (_unit.CurrentState)
                {
                    case UnitCtrl.ActionState.DAMAGE:
                    case UnitCtrl.ActionState.DIE:
                        _unit.unitActionController.CancelAction(_unit.UnionBurstSkillId);
                        _unit.CancelByConvert = false;
                        _unit.CancelByToad = false;
                        yield break;
                    default:
                        if (!_unit.GetCurrentSpineCtrl().IsPlayAnimeBattle || _unit.UnionBurstAnimeEndForIfAction)
                        {
                            _unit.UnionBurstAnimeEndForIfAction = false;
                            if (_unit.unitActionController.IsModeChange(_unit.UnionBurstSkillId))
                            {
                                _unit.UnitSpineCtrlModeChange.CurColor = _unit.UnitSpineCtrl.CurColor;
                                Color curColor = _unit.UnitSpineCtrl.CurColor;
                                curColor.a = 0.0f;
                                _unit.UnitSpineCtrl.CurColor = curColor;
                                _unit.UnitSpineCtrlModeChange.gameObject.SetActive(true);
                                _unit.MotionPrefix = 1;
                                _unit.OnMotionPrefixChanged.Call();
                                if (_unit.IsFront)
                                    _unit.SetSortOrderFront();
                                else
                                    _unit.SetSortOrderBack();
                                _unit.SetLeftDirection(_unit.IsLeftDir);
                                if (_unit.UnitSpineCtrlModeChange.IsAnimation(_unit.UnitSpineCtrlModeChange.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SKILL, 0, _index3: 1)))
                                {
                                    _unit.PlayAnimeNoOverlap(eSpineCharacterAnimeId.SKILL, 0, _index3: 1, _targetCtr: _unit.UnitSpineCtrlModeChange);
                                    _unit.AppendCoroutine(_unit.updateModeChange(), ePauseType.SYSTEM, _unit);
                                    yield break;
                                }
                                else
                                {
                                    _unit.SetState(UnitCtrl.ActionState.IDLE);
                                    yield break;
                                }
                            }
                            else if (!_unit.unitActionController.HasNextAnime(_unit.UnionBurstSkillId))
                            {
                                _unit.SkillEndProcess();
                                yield break;
                            }
                            else
                            {
                                while (_unit.CurrentState != UnitCtrl.ActionState.IDLE)
                                {
                                    switch (_unit.CurrentState)
                                    {
                                        case UnitCtrl.ActionState.DAMAGE:
                                        case UnitCtrl.ActionState.DIE:
                                            _unit.unitActionController.CancelAction(_unit.UnionBurstSkillId);
                                            _unit.CancelByConvert = false;
                                            _unit.CancelByToad = false;
                                            yield break;
                                        default:
                                            yield return (object)null;
                                            continue;
                                    }
                                }
                                yield break;
                            }
                        }
                        else
                        {
                            yield return (object)null;
                            continue;
                        }
                }
            }
            _unit.unitActionController.CancelAction(_unit.UnionBurstSkillId);
            _unit.CancelByConvert = false;
            _unit.CancelByToad = false;
        }

        public void SkillEndProcess()
        {
            this.SetState(UnitCtrl.ActionState.IDLE);
            this.CancelByConvert = false;
            this.CancelByToad = false;
        }

        private IEnumerator updateModeChange()
        {
            while (this.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                yield return (object)null;
            this.SetState(UnitCtrl.ActionState.IDLE);
        }

        private void setStateDie()
        {
            /*if (this.battleManager.GetPurpose() == eHatsuneSpecialPurpose.GET_POINT && !this.EnemyPointDone)
            {
                this.EnemyPointDone = true;
                if (this.EnemyPoint != 0)
                {
                    this.battleManager.NOMJJDDCBAN.Add(this.UnitId);
                    this.battleManager.SubstructEnemyPoint(this.EnemyPoint);
                }
            }*/
            switch (this.CurrentState)
            {
                case UnitCtrl.ActionState.DAMAGE:
                    if (this.OnDeadForRevival != null || this.IsDead || this.UnDeadTimeHitCount > 1)
                        break;
                    this.IsDead = true;
                    this.battleManager.CallbackDead(this);
                    if (!((UnityEngine.Object)this.runSmokeEffect != (UnityEngine.Object)null))
                        break;
                    //this.runSmokeEffect.SetTimeToDie(true);
                    break;
                case UnitCtrl.ActionState.DIE:
                    break;
                default:
                    if (this.OnDeadForRevival == null && !this.IsDead && this.UnDeadTimeHitCount <= 1)
                    {
                        this.IsDead = true;
                        this.battleManager.CallbackDead(this);
                        //if ((UnityEngine.Object)this.runSmokeEffect != (UnityEngine.Object)null)
                        //    this.runSmokeEffect.SetTimeToDie(true);
                    }
                    if ((this.ActionsTargetOnMe.Count > 0 || this.FirearmCtrlsOnMe.Count > 0) && this.UnDeadTimeHitCount == 0)
                    {
                        this.SetState(UnitCtrl.ActionState.DAMAGE, _quiet: true);
                        break;
                    }
                    if (this.ToadDatas.Count > 0)
                    {
                        this.DieInToad = true;
                        this.ToadDatas[0].Enable = false;
                        break;
                    }
                    this.CureAllAbnormalState();
                    if ((UnityEngine.Object)this.KillBonusTarget != (UnityEngine.Object)null)
                        this.KillBonusTarget.ChargeEnergy(eSetEnergyType.KILL_BONUS, this.battleManager.EnergyStackValueDefeat, _hasNumberEffect: false);
                    /*for (int count = this.RepeatEffectList.Count; count > 0; --count)
                    {
                        SkillEffectCtrl repeatEffect = this.RepeatEffectList[count - 1];
                        if ((UnityEngine.Object)repeatEffect != (UnityEngine.Object)null && !(repeatEffect is FirearmCtrl))
                        {
                            repeatEffect.SetTimeToDie(true);
                            repeatEffect.OnEffectEnd.Call<SkillEffectCtrl>(repeatEffect);
                        }
                    }*/
                    if (this.UnDeadTimeHitCount == 1)
                        this.PlayAnimeNoOverlap(eSpineCharacterAnimeId.DIE, 1);
                    else if (this.IsDivisionSourceForDie)
                        this.OnStartErrorUndoDivision.Call<bool>(false);
                    else
                        this.PlayAnime(eSpineCharacterAnimeId.DIE, _index2: this.MotionPrefix, _isLoop: false);
                    //if (this.dieEffects.Count > 0 && !this.IsDivisionSourceForDie)
                    //    this.AppendCoroutine(this.CreatePrefabWithTime(this.dieEffects, _ignorePause: true), ePauseType.IGNORE_BLACK_OUT);
                    //int index1 = 0;
                    //for (int count = this.dieShakes.Count; index1 < count; ++index1)
                    //    this.AppendCoroutine(this.startShakeWithDelay(this.dieShakes[index1], true), ePauseType.IGNORE_BLACK_OUT);
                    if (this.OnDieForZeroHp != null && this.OnDeadForRevival == null)
                        this.OnDieForZeroHp(this);
                    //this.playRetireVoice();
                    this.accumulateDamage = 0L;
                    this.TargetEnemyList.Clear();
                    this.targetPlayerList.Clear();
                    this.CurrentState = UnitCtrl.ActionState.DIE;
                    if (this.IsPartsBoss)
                    {
                        for (int index2 = 0; index2 < this.BossPartsListForBattle.Count; ++index2)
                            this.BossPartsListForBattle[index2].DisableCursor();
                    }
                    foreach (KeyValuePair<int, UnitCtrl> summonUnit in this.SummonUnitDictionary)
                    {
                        summonUnit.Value.IdleOnly = true;
                        summonUnit.Value.CureAllAbnormalState();
                        this.battleManager.CallbackDead(summonUnit.Value);
                    }
                    if (this.HasDieLoop)
                    {
                        this.RespawnPos = eUnitRespawnPos.MAIN_POS_1;
                        if (!this.IsFront)
                            this.SetSortOrderBack();
                    }
                    if (this.IsDivisionSourceForDie)
                    {
                        this.AppendCoroutine(this.updateUndoDivision(), ePauseType.SYSTEM);
                        break;
                    }
                    this.AppendCoroutine(this.updateDie(++this.dieCoroutineId), ePauseType.SYSTEM);
                    break;
            }
        }

        private IEnumerator updateDie(int _thisId)
        {
            UnitCtrl unitCtrl = this;
            //MyOnChangeState?.Invoke(UnitId,CurrentState, BattleHeaderController.CurrentFrameCount,"");
            while (_thisId == unitCtrl.dieCoroutineId)
            {
                BattleSpineController currentSpineCtrl = unitCtrl.GetCurrentSpineCtrl();
                if ((UnityEngine.Object)currentSpineCtrl == (UnityEngine.Object)null)
                    break;
                if (!currentSpineCtrl.IsPlayAnimeBattle)
                {
                    //unitCtrl.soundManager.PlaySeByOuterSource(unitCtrl.SeSource, eSE.BTL_DIE_FADEOUT);
                    if (unitCtrl.HasDieLoop)
                    {
                        unitCtrl.GetCurrentSpineCtrl().PlayAnime(eSpineCharacterAnimeId.DIE_LOOP);
                        unitCtrl.battleManager.CallbackFadeOutDone(unitCtrl);
                        break;
                    }
                    float fAlpha = unitCtrl.isDeadBySetCurrentHp ? unitCtrl.GetCurrentSpineCtrl().CurColor.a : 1f;
                    while (_thisId == unitCtrl.dieCoroutineId)
                    {
                        fAlpha -= unitCtrl.DeltaTimeForPause;
                        if ((double)fAlpha <= 0.0)
                        {
                            if (unitCtrl.OnDieFadeOutEnd != null && unitCtrl.OnDeadForRevival == null)
                            {
                                unitCtrl.OnDieFadeOutEnd(unitCtrl);
                                unitCtrl.OnDieFadeOutEnd = (System.Action<UnitCtrl>)null;
                            }
                            if (unitCtrl.OnDeadForRevival != null)
                            {
                                unitCtrl.OnDeadForRevival(unitCtrl);
                                break;
                            }
                            if (UnitUtility.JudgeIsSummon(unitCtrl.UnitId))
                            {
                                List<UnitCtrl> unitCtrlList = unitCtrl.IsOther ? unitCtrl.battleManager.EnemyList : unitCtrl.battleManager.UnitList;
                                if (unitCtrlList.Contains(unitCtrl))
                                    unitCtrlList.Remove(unitCtrl);
                                if (!unitCtrl.gameObject.activeSelf)
                                    break;
                                unitCtrl.gameObject.SetActive(false);
                                unitCtrl.battleManager.CallbackFadeOutDone(unitCtrl);
                                break;
                            }
                            if (unitCtrl.IsPartsBoss)
                            {
                                foreach (PartsData bossParts in unitCtrl.BossPartsList)
                                    bossParts.FixAttachment(unitCtrl);
                            }
                            unitCtrl.gameObject.SetActive(false);
                            unitCtrl.battleManager.CallbackFadeOutDone(unitCtrl);
                            break;
                        }
                        if ((UnityEngine.Object)unitCtrl.GetCurrentSpineCtrl() == (UnityEngine.Object)null)
                            break;
                        unitCtrl.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, fAlpha);
                        yield return (object)null;
                    }
                    break;
                }
                yield return (object)null;
            }
        }

        private IEnumerator updateUndoDivision()
        {
            UnitCtrl _unit = this;
            while (_unit.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                yield return (object)null;
            _unit.PlayAnime(eSpineCharacterAnimeId.DIE, _index2: _unit.MotionPrefix, _isLoop: false);
            //_unit.AppendCoroutine(_unit.CreatePrefabWithTime(_unit.dieEffects), ePauseType.VISUAL, _unit);
            _unit.AppendCoroutine(_unit.updateDie(++_unit.dieCoroutineId), ePauseType.SYSTEM);
        }

        private void setStateDamage(bool _quiet)
        {
            this.ModeChangeEnd = false;
            switch (this.CurrentState)
            {
                case UnitCtrl.ActionState.DAMAGE:
                case UnitCtrl.ActionState.DIE:
                    break;
                default:
                    if (this.ToadRelease)
                    {
                        this.ToadReleaseDamage = true;
                        break;
                    }
                    //if (this.damageEffects.Count > 0)
                    //    this.AppendCoroutine(this.CreatePrefabWithTime(this.damageEffects), ePauseType.SYSTEM);
                    //int index = 0;
                    //for (int count = this.damageShakes.Count; index < count; ++index)
                    //    this.AppendCoroutine(this.startShakeWithDelay(this.damageShakes[index]), ePauseType.SYSTEM);
                    //if (!_quiet)
                     //   this.playDamageVoice();
                    this.PlayAnime(eSpineCharacterAnimeId.DAMEGE, this.MotionPrefix, _isLoop: false, _ignoreBlackout: true);
                    this.CurrentState = UnitCtrl.ActionState.DAMAGE;
                    this.AppendCoroutine(this.updateDamage(++this.damageCoroutineId), ePauseType.IGNORE_BLACK_OUT);
                    break;
            }
        }

        private IEnumerator updateDamage(int _thisId)
        {
            //MyOnChangeState?.Invoke(UnitId,CurrentState, BattleHeaderController.CurrentFrameCount);
            float time = 0.0f;
            while (_thisId == this.damageCoroutineId)
            {
                if ((long)this.Hp <= 0L)
                {
                    if (this.ActionsTargetOnMe.Count > 0 || this.FirearmCtrlsOnMe.Count > 0 || this.KnockBackEnableCount > 0)
                    {
                        time += this.battleManager.DeltaTime_60fps;
                        if ((double)time > 10.0)
                        {
                            bool flag = false;
                            for (int index = this.ActionsTargetOnMe.Count - 1; index >= 0; --index)
                            {
                                flag = true;
                                this.ActionsTargetOnMe.RemoveAt(index);
                            }
                            for (int index = this.FirearmCtrlsOnMe.Count - 1; index >= 0; --index)
                            {
                                flag = true;
                                this.FirearmCtrlsOnMe.RemoveAt(index);
                            }
                            if (flag)
                                continue;
                        }
                        BattleSpineController currentSpineCtrl = this.GetCurrentSpineCtrl();
                        TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                        if (current != null && (double)current.animationLast >= (double)currentSpineCtrl.StopStateTime)
                        {
                            current.animationLast = currentSpineCtrl.StopStateTime;
                            current.animationStart = currentSpineCtrl.StopStateTime;
                            currentSpineCtrl.Pause();
                        }
                        yield return (object)null;
                    }
                    else
                    {
                        this.GetCurrentSpineCtrl().Resume();
                        this.CurrentState = UnitCtrl.ActionState.IDLE;
                        this.SetState(UnitCtrl.ActionState.DIE);
                        break;
                    }
                }
                else
                {
                    if (this.isPauseDamageMotion())
                    {
                        this.resumeIsStopState = true;
                        BattleSpineController currentSpineCtrl = this.GetCurrentSpineCtrl();
                        TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                        if (current != null)
                        {
                            current.lastTime = currentSpineCtrl.StopStateTime;
                            current.time = currentSpineCtrl.StopStateTime;
                            //current.animationLast = currentSpineCtrl.StopStateTime;
                            //current.animationStart = currentSpineCtrl.StopStateTime;

                        }
                        this.GetCurrentSpineCtrl().Pause();
                        while (this.IsUnableActionState() && (long)this.Hp != 0L)
                        {
                            if (_thisId != this.damageCoroutineId)
                                yield break;
                            else
                                yield return (object)null;
                        }
                    }
                    if (this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.INVALID && this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.RELEASE)
                        this.resumeIsStopState = true;
                    this.GetCurrentSpineCtrl().Resume();
                    if ((long)this.Hp == 0L)
                    {
                        this.CurrentState = UnitCtrl.ActionState.IDLE;
                        this.SetState(UnitCtrl.ActionState.DIE);
                        break;
                    }
                    if (!this.GetCurrentSpineCtrl().IsPlayAnimeBattle && !this.IsUnableActionState())
                    {
                        if (this.ToadRelease && this.IdleOnly)
                            break;
                        this.SetState(UnitCtrl.ActionState.IDLE);
                        break;
                    }
                    yield return (object)null;
                }
            }
        }

        private bool isPauseDamageMotion() => this.IsUnableActionState() && this.GetCurrentSpineCtrl().IsStopState && (this.specialSleepStatus == UnitCtrl.eSpecialSleepStatus.INVALID || this.specialSleepStatus == UnitCtrl.eSpecialSleepStatus.RELEASE);

        private void setStateAttack()
        {
            this.CurrentState = UnitCtrl.ActionState.ATK;
            this.CurrentSkillId = 1;
            UnitActionController actionController = this.unitActionController;
            if (this.ToadDatas.Count > 0)
                actionController = this.ToadDatas[0].UnitActionController;
            if (actionController.StartAction(1))
            {
                this.ChargeEnergy(eSetEnergyType.BY_ATK, this.skillStackVal, _source: this, _hasNumberEffect: false);
                //if (this.judgePlayVoice("attack", 1, false, 0.0f))
                //    this.PlayVoice(SoundManager.eVoiceType.ATTACK, 0, true, false);
                this.AppendCoroutine(this.updateAttack(actionController), ePauseType.SYSTEM, this);
            }
            else
                this.SetState(UnitCtrl.ActionState.IDLE);
        }

        /*private bool judgePlayVoice(
          string _voiceName,
          int _maxNum,
          bool _isSkill,
          float _voiceOffsetRate)
        {
            bool playVoice = true;
            int counter = 0;
            if (this.enemyVoiceId != 0)
                return true;
            if (this.IsOther != this.battleManager.IsDefenceReplayMode)
                return false;
            System.Action<List<UnitCtrl>> action = (System.Action<List<UnitCtrl>>)(list =>
           {
               int index = 0;
               for (int count = list.Count; index < count; ++index)
               {
                   if (list[index].VoiceSource.status == CriAtomSource.Status.Playing && list[index].VoiceSource.cueName.Contains(_voiceName))
                   {
                       ++counter;
                       if (counter != _maxNum)
                           break;
                       playVoice = false;
                       break;
                   }
               }
           });
            action(this.battleManager.UnitList);
            if (playVoice)
                action(this.battleManager.EnemyList);
            if (_isSkill && (double)Time.time - (double)this.battleManager.LatestSkillVoiceTime < 0.5)
                playVoice = false;
            if (playVoice)
            {
                if (this.battleManager.JudgeVoicePlay(this.SoundUnitId, _maxNum, _voiceOffsetRate))
                {
                    if (_isSkill)
                        this.battleManager.LatestSkillVoiceTime = Time.time;
                    return true;
                }
            }
            else
                this.battleManager.IncrementNoneVoiceAttackCount();
            return false;
        }*/

        private IEnumerator updateAttack(UnitActionController _unitActionController)
        {
            //MyOnChangeState?.Invoke(UnitId,CurrentState, BattleHeaderController.CurrentFrameCount,"");
            while (!this.IsCancelActionState(false))
            {
                if (!this.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                {
                    this.SetState(UnitCtrl.ActionState.IDLE);
                    this.CancelByAwake = false;
                    this.CancelByConvert = false;
                    this.CancelByToad = false;
                    yield break;
                }
                else
                    yield return (object)null;
            }
            _unitActionController.CancelAction(1);
            this.CancelByAwake = false;
            this.CancelByConvert = false;
            this.CancelByToad = false;
        }

        private void setStateWalk()
        {
            if (this.CurrentState != UnitCtrl.ActionState.IDLE)
                return;
            this.SetDirectionAuto();
            this.CurrentState = UnitCtrl.ActionState.WALK;
            this.AppendCoroutine(this.updateWalk(++this.walkCoroutineId), ePauseType.SYSTEM, this);
            this.PlayAnime((double)this.moveRate == 0.0 ? eSpineCharacterAnimeId.IDLE : (this.isRunForCatchUp || this.StandByDone ? eSpineCharacterAnimeId.RUN : eSpineCharacterAnimeId.RUN_GAME_START), this.MotionPrefix);
        }

        private IEnumerator updateWalk(int coroutineId)
        {
            UnitCtrl unitCtrl = this;
            //MyOnChangeState?.Invoke(UnitId,CurrentState, BattleHeaderController.CurrentFrameCount,"");
            while (coroutineId == unitCtrl.walkCoroutineId)
            {
                switch (unitCtrl.CurrentState)
                {
                    case UnitCtrl.ActionState.SKILL_1:
                    case UnitCtrl.ActionState.DAMAGE:
                    case UnitCtrl.ActionState.DIE:
                        unitCtrl.isRunForCatchUp = false;
                        yield break;
                    default:
                        if (!unitCtrl.IsConfusionOrConvert())
                        {
                            if (unitCtrl.TargetEnemyList.Count > 0 || unitCtrl.IdleOnly)
                            {
                                unitCtrl.isRunForCatchUp = false;
                                unitCtrl.SetState(UnitCtrl.ActionState.IDLE);
                                yield break;
                            }
                        }
                        else
                        {
                            int num = 0;
                            List<UnitCtrl> unitCtrlList = unitCtrl.IsOther ? unitCtrl.battleManager.EnemyList : unitCtrl.battleManager.UnitList;
                            for (int index = 0; index < unitCtrlList.Count; ++index)
                            {
                                if (!unitCtrlList[index].IsDead)
                                    ++num;
                            }
                            if (num == 1)
                            {
                                unitCtrl.isRunForCatchUp = false;
                                unitCtrl.SetState(UnitCtrl.ActionState.IDLE);
                                yield break;
                            }
                            else if (unitCtrl.targetPlayerList.Count > 1 || unitCtrl.IdleOnly)
                            {
                                unitCtrl.SetState(UnitCtrl.ActionState.IDLE);
                                yield break;
                            }
                        }
                        List<UnitCtrl> unitCtrlList1 = unitCtrl.IsOther ? unitCtrl.battleManager.UnitList : unitCtrl.battleManager.EnemyList;
                        bool flag1 = unitCtrlList1.FindIndex((Predicate<UnitCtrl>)(e => (long)e.Hp > 0L)) == -1;
                        bool flag2 = unitCtrlList1.TrueForAll((Predicate<UnitCtrl>)(e => e.IsStealth || (long)e.Hp == 0L));
                        if (unitCtrl.battleManager.GameState == eBattleGameState.NEXT_WAVE_PROCESS)
                        {
                            flag1 = false;
                            flag2 = false;
                        }
                        BattleSpineController currentSpineCtrl = unitCtrl.GetCurrentSpineCtrl();
                        Vector3 localPosition = unitCtrl.transform.localPosition;
                        localPosition.x += (float)((double)unitCtrl.moveRate * (double)unitCtrl.DeltaTimeForPause * (unitCtrl.isRunForCatchUp ? 1.0 : 1.60000002384186));
                        if (!(flag1 | flag2))
                        {
                            if (currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, unitCtrl.MotionPrefix) && (double)unitCtrl.moveRate != 0.0)
                                unitCtrl.PlayAnime(eSpineCharacterAnimeId.RUN, unitCtrl.MotionPrefix);
                            unitCtrl.transform.localPosition = localPosition;
                        }
                        else if (currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.RUN, unitCtrl.MotionPrefix))
                            unitCtrl.PlayAnime(eSpineCharacterAnimeId.IDLE, unitCtrl.MotionPrefix);
                        unitCtrl.SetDirectionAuto();
                        yield return (object)null;
                        continue;
                }
            }
        }

        private void setStateIdle()
        {
            /*if ((UnityEngine.Object)this.runSmokeEffect != (UnityEngine.Object)null)
            {
                this.runSmokeEffect.SetTimeToDie(true);
                this.runSmokeEffect = (SkillEffectCtrl)null;
            }*/
            if (this.MotionPrefix == 1 && !this.unitActionController.ModeChanging)
                return;
            this.isAwakeMotion = false;
            this.SetDirectionAuto();
            //if (this.idleEffects.Count > 0 && !this.IsDead)
            //    this.AppendCoroutine(this.CreatePrefabWithTime(this.idleEffects, true), ePauseType.VISUAL, this);
            if (this.CurrentSkillId == this.UnionBurstSkillId)
            {
                this.PlayUbChainVoice();
                for (int index = this.UbAbnormalDataList.Count - 1; index >= 0; --index)
                {
                    this.UbAbnormalDataList[index].Exec(this);
                    this.UbAbnormalDataList.RemoveAt(index);
                    this.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(this, eStateIconType.UB_DISABLE, false);
                    this.MyOnChangeAbnormalState?.Invoke(this, eStateIconType.UB_DISABLE, false, 90, "???");
                }
            }
            switch (this.CurrentState)
            {
                case UnitCtrl.ActionState.ATK:
                case UnitCtrl.ActionState.SKILL_1:
                case UnitCtrl.ActionState.SKILL:
                case UnitCtrl.ActionState.WALK:
                case UnitCtrl.ActionState.DAMAGE:
                case UnitCtrl.ActionState.GAME_START:
                    this.CurrentState = UnitCtrl.ActionState.IDLE;
                    if (this.IdleOnly && !this.ToadRelease)
                        this.battleManager.CallbackIdleOnlyDone(this);
                    if (this.StandByDone)
                    {
                        BattleSpineController currentSpineCtrl = this.GetCurrentSpineCtrl();
                        if (currentSpineCtrl.AnimationName != currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE) && !this.ToadRelease)
                            this.PlayAnime(eSpineCharacterAnimeId.IDLE, this.MotionPrefix);
                        this.battleManager.CallbackStartDashDone(this);
                    }
                    else
                    {
                        this.PlayAnime(eSpineCharacterAnimeId.STAND_BY, this.MotionPrefix, _isLoop: false);
                        //if (this.IsStartVoicePlay)
                        //    BattleVoiceUtility.PlayWaveStartVoice(this);
                        this.AppendCoroutine(this.updateStandBy(), ePauseType.VISUAL, this);
                    }
                    this.AppendCoroutine(this.updateIdle(), ePauseType.SYSTEM, this);
                    break;
            }
        }

        private IEnumerator updateStandBy()
        {
            UnitCtrl FNHGFDNICFG = this;
            while (FNHGFDNICFG.GetCurrentSpineCtrl().IsPlayAnimeBattle)
            {
                if (FNHGFDNICFG.CurrentState != UnitCtrl.ActionState.IDLE)
                {
                    FNHGFDNICFG.StandByDone = true;
                    FNHGFDNICFG.battleManager.CallbackStanbyDone(FNHGFDNICFG);
                    yield break;
                }
                else
                    yield return (object)null;
            }
            FNHGFDNICFG.StandByDone = true;
            FNHGFDNICFG.battleManager.CallbackStanbyDone(FNHGFDNICFG);
            if (FNHGFDNICFG.CurrentState == UnitCtrl.ActionState.IDLE)
                FNHGFDNICFG.PlayAnime(eSpineCharacterAnimeId.IDLE, FNHGFDNICFG.MotionPrefix);
        }

        private IEnumerator updateIdle()
        {
            UnitCtrl unitCtrl = this;
            //MyOnChangeState?.Invoke(UnitId,CurrentState, BattleHeaderController.CurrentFrameCount,"");
            if (unitCtrl.idleStartAfterWaitFrame)
            {
                unitCtrl.idleStartAfterWaitFrame = false;
                yield return (object)null;
            }
            float fAlpha = 1f;
            while (true)
            {
                while (!unitCtrl.ModeChangeEnd)
                {
                    if (unitCtrl.JoyFlag)
                    {
                        unitCtrl.setStateJoy();
                        unitCtrl.JoyFlag = false;
                        yield break;
                    }
                    else
                    {
                        //if (unitCtrl.IdleOnly && unitCtrl.EnemyPoint != 0 && unitCtrl.battleManager.FICLPNJNOEP >= (int)HatsuneUtility.GetHatsuneSpecialBattle().purpose_count)
                        //    unitCtrl.SetState(UnitCtrl.ActionState.DIE);
                        if (unitCtrl.IdleOnly)
                        {
                            if (unitCtrl.CurrentState != UnitCtrl.ActionState.IDLE)
                            {
                                yield break;
                            }
                            else
                            {
                                if (unitCtrl.IsSummonOrPhantom)
                                {
                                    List<UnitCtrl> unitCtrlList = unitCtrl.IsOther ? unitCtrl.battleManager.EnemyList : unitCtrl.battleManager.UnitList;
                                    if (unitCtrlList.Contains(unitCtrl))
                                    {
                                        unitCtrlList.Remove(unitCtrl);
                                        if (!unitCtrl.IsOther)
                                            unitCtrl.battleManager.LPBCBINDJLJ.Add(unitCtrl);
                                    }
                                    fAlpha -= unitCtrl.battleManager.DeltaTime_60fps;
                                    if ((double)fAlpha <= 0.0 || BattleUtil.Approximately(fAlpha, 0.0f))
                                    {
                                        if (unitCtrl.OnDieFadeOutEnd != null && unitCtrl.OnDeadForRevival == null)
                                        {
                                            unitCtrl.OnDieFadeOutEnd(unitCtrl);
                                            unitCtrl.OnDieFadeOutEnd = (System.Action<UnitCtrl>)null;
                                        }
                                        if (unitCtrl.OnDeadForRevival != null)
                                        {
                                            unitCtrl.OnDeadForRevival(unitCtrl);
                                            unitCtrl.OnDeadForRevival = (UnitCtrl.OnDeadDelegate)null;
                                            yield break;
                                        }
                                        else
                                        {
                                            /*for (int index = unitCtrl.idleEffectsObjs.Count - 1; index >= 0; --index)
                                            {
                                                unitCtrl.idleEffectsObjs[index].SetTimeToDie(true);
                                                unitCtrl.idleEffectsObjs.RemoveAt(index);
                                            }*/
                                            unitCtrl.CureAllAbnormalState();
                                            unitCtrl.battleManager.CallbackFadeOutDone(unitCtrl);
                                            unitCtrl.gameObject.SetActive(false);
                                            if (unitCtrl.IsOther)
                                            {
                                                yield break;
                                            }
                                            else
                                            {
                                                unitCtrl.battleManager.LPBCBINDJLJ.Remove(unitCtrl);
                                                yield break;
                                            }
                                        }
                                    }
                                    else
                                        unitCtrl.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, fAlpha);
                                }
                                yield return (object)null;
                            }
                        }
                        else if (unitCtrl.attackPatternLoopDictionary[unitCtrl.currentActionPatternId].Count == 0)
                        {
                            yield return (object)null;
                        }
                        else
                        {
                            if (unitCtrl.CurrentState == UnitCtrl.ActionState.IDLE)
                                unitCtrl.OnUpdateWhenIdle.Call<float>(unitCtrl.battleManager.BattleLeftTime);
                            if (unitCtrl.CurrentState != UnitCtrl.ActionState.IDLE)
                                yield break;
                            else if (unitCtrl.battleManager.BlackOutUnitList.Contains(unitCtrl))
                                yield return (object)null;
                            else if (unitCtrl.judgeStateIsWalk())
                            {
                                yield break;
                            }
                            else
                            {
                                if (((double)unitCtrl.battleManager.ActionStartTimeCounter <= 0.0 || unitCtrl.IsOther ? (!unitCtrl.ToadRelease ? 1 : 0) : 0) != 0)
                                {
                                    unitCtrl.m_fCastTimer = (ObscuredFloat)((float)unitCtrl.m_fCastTimer - unitCtrl.DeltaTimeForPause);
                                    MyOnSkillCD?.Invoke(m_fCastTimer);
                                }
                                if (unitCtrl.battleManager.LOGNEDLPEIJ)
                                    yield return (object)null;
                                else if (unitCtrl.ToadRelease)
                                    yield return (object)null;
                                else if ((double)(float)unitCtrl.m_fCastTimer <= 0.0 && unitCtrl.attackPatternDictionary != null)
                                {
                                    unitCtrl.updateAttackTargetImpl();
                                    if (unitCtrl.judgeStateIsWalk())
                                    {
                                        yield break;
                                    }
                                    else
                                    {
                                        int _skillId = unitCtrl.attackPatternIsLoop ? unitCtrl.attackPatternLoopDictionary[unitCtrl.currentActionPatternId][unitCtrl.attackPatternIndex] : unitCtrl.attackPatternDictionary[unitCtrl.currentActionPatternId][unitCtrl.attackPatternIndex];
                                        UnitCtrl.ActionState _state = _skillId != 1 ? UnitCtrl.ActionState.SKILL : UnitCtrl.ActionState.ATK;
                                        int _nextSkillId;
                                        if (!unitCtrl.attackPatternIsLoop)
                                        {
                                            if (unitCtrl.attackPatternIndex + 1 < unitCtrl.attackPatternDictionary[unitCtrl.currentActionPatternId].Count)
                                            {
                                                _nextSkillId = unitCtrl.attackPatternDictionary[unitCtrl.currentActionPatternId][unitCtrl.attackPatternIndex + 1];
                                                unitCtrl.attackPatternIndex++;
                                            }
                                            else
                                            {
                                                _nextSkillId = unitCtrl.attackPatternLoopDictionary[unitCtrl.currentActionPatternId][0];
                                                unitCtrl.attackPatternIsLoop = true;
                                                unitCtrl.attackPatternIndex = 0;
                                            }
                                        }
                                        else if (unitCtrl.attackPatternIndex + 1 < unitCtrl.attackPatternLoopDictionary[unitCtrl.currentActionPatternId].Count)
                                        {
                                            _nextSkillId = unitCtrl.attackPatternLoopDictionary[unitCtrl.currentActionPatternId][unitCtrl.attackPatternIndex + 1];
                                            unitCtrl.attackPatternIndex++;
                                        }
                                        else
                                        {
                                            _nextSkillId = unitCtrl.attackPatternLoopDictionary[unitCtrl.currentActionPatternId][0];
                                            unitCtrl.attackPatternIndex = 0;
                                        }
                                        unitCtrl.CancelByAwake = false;
                                        unitCtrl.SetState(_state, _nextSkillId, _skillId);
                                        MyOnChangeSkillID?.Invoke(_skillId, _nextSkillId, m_fCastTimer, 1);
                                        yield break;
                                    }
                                }
                                else if ((long)unitCtrl.Hp <= 0L)
                                {
                                    unitCtrl.SetState(UnitCtrl.ActionState.DIE);
                                    yield break;
                                }
                                else
                                    yield return (object)null;
                            }
                        }
                    }
                }
                yield return (object)null;
            }
        }

        private bool judgeStateIsWalk()
        {
            if (!this.IsConfusionOrConvert())
            {
                if (this.TargetEnemyList.Count <= 0)
                {
                    this.isRunForCatchUp = true;
                    this.SetState(UnitCtrl.ActionState.WALK);
                    return true;
                }
            }
            else
            {
                List<UnitCtrl> targetPlayerList = this.targetPlayerList;
                bool flag = true;
                for (int index = 0; index < targetPlayerList.Count; ++index)
                {
                    UnitCtrl unitCtrl = targetPlayerList[index];
                    if (!unitCtrl.IsDead && (UnityEngine.Object)unitCtrl != (UnityEngine.Object)this)
                        flag = false;
                }
                int num = 0;
                List<UnitCtrl> unitCtrlList = this.IsOther ? this.battleManager.EnemyList : this.battleManager.UnitList;
                for (int index = 0; index < unitCtrlList.Count; ++index)
                {
                    if (!unitCtrlList[index].IsDead)
                        ++num;
                }
                if (num == 1)
                    flag = false;
                if (flag)
                {
                    this.SetState(UnitCtrl.ActionState.WALK);
                    return true;
                }
            }
            return false;
        }

        public void PlayDamageWhenIdle(bool _damageMotionWhenUnionBurst = false, bool _pauseStopState = false)
        {
            if ((UnityEngine.Object)this.battleManager == (UnityEngine.Object)null || ((this.CurrentState != UnitCtrl.ActionState.IDLE || this.ModeChangeEnd ? (this.battleManager.BlackoutUnitTargetList.Contains(this) ? 1 : 0) : 1) == 0 || this.battleManager.BlackOutUnitList.Contains(this) || (this.IsAbnormalState(UnitCtrl.eAbnormalState.STONE) || this.IsAbnormalState(UnitCtrl.eAbnormalState.PAUSE_ACTION))))
                return;
            BattleSpineController currentSpineCtrl = this.GetCurrentSpineCtrl();
            TrackEntry current1 = currentSpineCtrl.state.GetCurrent(0);
            if (!this.isIdleDamage)
            {
                if (current1 != null)
                    this.resumeTime = current1.AnimationTime;
                this.animeLoop = currentSpineCtrl.loop;
                this.resumeIsStopState = currentSpineCtrl.IsStopState;
                this.animeName = currentSpineCtrl.AnimationName;
                this.isIdleDamage = true;
            }
            currentSpineCtrl.loop = false;
            string str = !this.IsPartsBoss || this.PartsMotionPrefix == 0 ? currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, this.MotionPrefix) : currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, _index2: this.PartsMotionPrefix);
            if (this.battleManager.BlackoutUnitTargetList.Contains(this))
            {
                if (_damageMotionWhenUnionBurst)
                {
                    if (currentSpineCtrl.AnimationName != str)
                    {
                        currentSpineCtrl.AnimationName = str;
                        currentSpineCtrl.state.TimeScale = 1f;
                        TrackEntry current2 = currentSpineCtrl.state.GetCurrent(0);
                        current2.lastTime = this.battleManager.DeltaTime_60fps;
                        current2.time = this.battleManager.DeltaTime_60fps;
                        //current2.animationLast = this.battleManager.DeltaTime_60fps;
                        //current2.animationStart = this.battleManager.DeltaTime_60fps;

                        current2.nextAnimationLast = this.battleManager.DeltaTime_60fps;
                        if (_pauseStopState)
                        {
                            current2.lastTime = currentSpineCtrl.StopStateTime;
                            current2.time = currentSpineCtrl.StopStateTime;
                            //current2.animationLast = currentSpineCtrl.StopStateTime;
                            //current2.animationStart = currentSpineCtrl.StopStateTime;

                            currentSpineCtrl.state.TimeScale = 0.0f;
                            return;
                        }
                    }
                    if (_pauseStopState)
                        return;
                }
                else
                {
                    currentSpineCtrl.loop = true;
                    if (this.IsPartsBoss && this.PartsMotionPrefix != 0)
                        currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE_MULTI_TARGET, _index2: this.PartsMotionPrefix);
                    else
                        currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, this.MotionPrefix);
                    currentSpineCtrl.state.TimeScale = 1f;
                    TrackEntry current2 = currentSpineCtrl.state.GetCurrent(0);
                    current2.lastTime = 0.0f;
                    current2.time = 0.0f;
                    //current2.animationLast = 0;
                    //current2.animationStart = 0;
                    currentSpineCtrl.state.Apply(currentSpineCtrl.skeleton);
                }
            }
            else
            {
                currentSpineCtrl.AnimationName = str;
                currentSpineCtrl.state.TimeScale = 1f;
            }
            this.AppendCoroutine(this.updateDamageWhenIdle(_damageMotionWhenUnionBurst), ePauseType.IGNORE_BLACK_OUT);
        }

        private IEnumerator updateDamageWhenIdle(bool _damageMotionWhenUnionBurst)
        {
            UnitCtrl unitCtrl = this;
            bool blackoutUnitContained = unitCtrl.battleManager.BlackoutUnitTargetList.Contains(unitCtrl);
            BattleSpineController currentSpineCtrl;
            while (true)
            {
                currentSpineCtrl = unitCtrl.GetCurrentSpineCtrl();
                if (!blackoutUnitContained || unitCtrl.battleManager.BlackoutUnitTargetList.Contains(unitCtrl) || (long)unitCtrl.Hp == 0L)
                {
                    if (unitCtrl.CurrentState == UnitCtrl.ActionState.IDLE || unitCtrl.battleManager.BlackoutUnitTargetList.Contains(unitCtrl))
                    {
                        if (!(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.JOY_LONG, unitCtrl.MotionPrefix)))
                        {
                            if (unitCtrl.CurrentState != UnitCtrl.ActionState.IDLE || currentSpineCtrl.IsPlayAnime)
                            {
                                if (!(!currentSpineCtrl.IsPlayAnime & _damageMotionWhenUnionBurst) || (long)unitCtrl.Hp == 0L || !(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, unitCtrl.MotionPrefix)))
                                    yield return (object)null;
                                else
                                    goto label_21;
                            }
                            else
                                goto label_16;
                        }
                        else
                            goto label_5;
                    }
                    else
                        goto label_13;
                }
                else
                    break;
            }
            unitCtrl.resumeSpineWithTime();
            if (unitCtrl.isContinueIdleForPauseAction)
            {
                currentSpineCtrl.IsStopState = true;
                currentSpineCtrl.Resume();
                currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.IDLE, unitCtrl.MotionPrefix);
                currentSpineCtrl.Pause();
            }
            else if (!unitCtrl.IsAbnormalState(UnitCtrl.eAbnormalState.STONE) && (unitCtrl.specialSleepStatus == UnitCtrl.eSpecialSleepStatus.START || unitCtrl.specialSleepStatus == UnitCtrl.eSpecialSleepStatus.WAIT_START_END || unitCtrl.specialSleepStatus == UnitCtrl.eSpecialSleepStatus.LOOP) && unitCtrl.GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(unitCtrl.MotionPrefix))
            {
                currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.SLEEP, unitCtrl.MotionPrefix, 1, 0);
                currentSpineCtrl.Resume();
                unitCtrl.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.LOOP;
            }
            else if (unitCtrl.GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(unitCtrl.MotionPrefix) && unitCtrl.IsAbnormalState(UnitCtrl.eAbnormalState.STONE) && unitCtrl.IsAbnormalState(UnitCtrl.eAbnormalState.SLEEP))
            {
                currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, unitCtrl.MotionPrefix);
                TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                if (current != null)
                {
                    current.lastTime = currentSpineCtrl.StopStateTime;
                    current.time = currentSpineCtrl.StopStateTime;
                    //current.animationLast = currentSpineCtrl.StopStateTime;
                    //current.animationStart = currentSpineCtrl.StopStateTime;

                }
                currentSpineCtrl.state.TimeScale = 0.0f;
                currentSpineCtrl.IsStopState = unitCtrl.resumeIsStopState;
            }
            unitCtrl.isIdleDamage = false;
            yield break;
        label_13:
            unitCtrl.isIdleDamage = false;
            yield break;
        label_5:
            yield break;
        label_16:
            currentSpineCtrl.loop = true;
            if (unitCtrl.IsPartsBoss && unitCtrl.PartsMotionPrefix != 0)
                currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE_MULTI_TARGET, _index2: unitCtrl.PartsMotionPrefix);
            else
                currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, unitCtrl.MotionPrefix);
            unitCtrl.isIdleDamage = false;
            yield break;
        label_21:
            currentSpineCtrl.loop = true;
            if (unitCtrl.IsPartsBoss && unitCtrl.PartsMotionPrefix != 0)
                currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE_MULTI_TARGET, _index2: unitCtrl.PartsMotionPrefix);
            else
                currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, unitCtrl.MotionPrefix);
        }

        private void resumeSpineWithTime()
        {
            BattleSpineController currentSpineCtrl = this.GetCurrentSpineCtrl();
            if (!(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, this.MotionPrefix)) && !(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, this.MotionPrefix)) && (!(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, _index2: this.PartsMotionPrefix)) && !(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE_MULTI_TARGET, _index2: this.PartsMotionPrefix))) && !currentSpineCtrl.AnimationName.IsNullOrEmpty())
                return;
            if (this.resumeIsStopState && this.IsUnableActionState() && !this.isContinueIdleForPauseAction && (this.IsAbnormalState(UnitCtrl.eAbnormalState.STONE) || this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.START && this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.WAIT_START_END && this.specialSleepStatus != UnitCtrl.eSpecialSleepStatus.LOOP || !this.GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(this.MotionPrefix)))
            {
                currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, this.MotionPrefix);
                TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                if (current != null)
                {
                    current.lastTime = currentSpineCtrl.StopStateTime;
                    current.time = currentSpineCtrl.StopStateTime;
                    //current.animationLast = currentSpineCtrl.StopStateTime;
                    //current.animationStart = currentSpineCtrl.StopStateTime;

                }
                currentSpineCtrl.state.TimeScale = 0.0f;
                currentSpineCtrl.IsStopState = this.resumeIsStopState;
                this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
            }
            else
            {
                bool flag = this.animeName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, this.MotionPrefix);
                if ((this.CurrentState == UnitCtrl.ActionState.IDLE ? 0 : (this.CurrentState != UnitCtrl.ActionState.DAMAGE ? 1 : 0)) != 0 || flag && !this.IsUnableActionState())
                {
                    currentSpineCtrl.loop = this.animeLoop;
                    currentSpineCtrl.AnimationName = this.animeName;
                    TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                    if (current != null)
                    {
                        current.lastTime = this.resumeTime;
                        current.time = this.resumeTime;
                        //current.animationLast = this.resumeTime;
                        //current.animationStart = this.resumeTime;

                    }
                    currentSpineCtrl.state.TimeScale = 1f;
                }
                currentSpineCtrl.IsStopState = this.resumeIsStopState;
            }
        }

        public IEnumerator SetStateWithDelayForRevival(
          float delay,
          UnitCtrl.ActionState state,
          int nextSkillId = 0,
          int skillId = 0)
        {
            float time = 0.0f;
            while (true)
            {
                time += this.DeltaTimeForPause;
                if ((double)time <= (double)delay && this.battleManager.GameState == eBattleGameState.PLAY)
                    yield return (object)null;
                else
                    break;
            }
            //this.SetEnableColor();
            this.SetState(state, nextSkillId, skillId);
        }

        /*public IEnumerator CreatePrefabWithTime(
          List<PrefabWithTime> _data,
          bool _isIdle = false,
          int _summonIndex = -1,
          bool _isAura = false,
          bool _isShieldEffect = false,
          bool _ignorePause = false)
        {
            UnitCtrl unitCtrl = this;
            // ISSUE: reference to a compiler-generated method
            _data = _data.FindAll(a =>
            {
                if (a.EffectDifficlty != PrefabWithTime.eEffectDifficulty.ALL)
                {
                    return effectDifficulty == a.EffectDifficlty;
                }
                else
                {
                    return true;
                }
            });
            if (_isShieldEffect)
                _data = _data.FindAll((Predicate<PrefabWithTime>)(e => (UnityEngine.Object)e.Prefab.GetComponent<SpBattleShieldEffect>() != (UnityEngine.Object)null));
            float time = 0.0f;
            bool[] effectCreated = new bool[_data.Count];
            while (true)
            {
                time += _ignorePause ? unitCtrl.battleManager.DeltaTime_60fps : unitCtrl.DeltaTimeForPause;
                int index = 0;
                for (int count = _data.Count; index < count; ++index)
                {
                    if (effectCreated[index])
                    {
                        if (index == count - 1)
                            yield break;
                    }
                    else
                    {
                        float time1 = _data[index].Time;
                        if (_summonIndex != -1 && _data[index].SummonSkillIndex != _summonIndex || !_isShieldEffect && (UnityEngine.Object)_data[index].Prefab.GetComponent<SpBattleShieldEffect>() != (UnityEngine.Object)null)
                        {
                            effectCreated[index] = true;
                        }
                        else
                        {
                            GameObject MDOJNMEMHLN = unitCtrl.IsLeftDir ? _data[index].PrefabLeft : _data[index].Prefab;
                            if ((double)time > (double)time1)
                            {
                                SkillEffectCtrl effect = unitCtrl.battleEffectPool.GetEffect(MDOJNMEMHLN, unitCtrl);
                                effect.InitializeSort();
                                effect.PlaySe(unitCtrl.SoundUnitId, unitCtrl.IsLeftDir);
                                effect.transform.position = unitCtrl.transform.position;
                                effect.transform.parent = ExceptNGUIRoot.Transform;
                                effect.SortTarget = unitCtrl;
                                effect.ExecAppendCoroutine(_isAura ? unitCtrl : (UnitCtrl)null, true);
                                effect.SetTimeToDieByStartHp(unitCtrl.StartHpPercent);
                                if (effect is SpBattleShieldEffect)
                                {
                                    if (unitCtrl.battleManager.FICLPNJNOEP >= unitCtrl.battleManager.GetPurposeCount())
                                    {
                                        effect.SetActive(false);
                                    }
                                    else
                                    {
                                        unitCtrl.battleManager.FICPCPIFKCD = effect as SpBattleShieldEffect;
                                        unitCtrl.battleManager.FICPCPIFKCD.SetEnemyPoint(unitCtrl.battleManager.FICLPNJNOEP, unitCtrl, true, (float)unitCtrl.battleManager.GetPurposeCount());
                                    }
                                }
                                if (_data[index].TargetBoneName.IsNullOrEmpty())
                                    unitCtrl.battleManager.StartCoroutine(effect.TrackTarget(unitCtrl.GetFirstParts(true), Vector3.zero));
                                else
                                    unitCtrl.battleManager.StartCoroutine(effect.TrackTarget(unitCtrl.GetFirstParts(true), Vector3.zero, bone: unitCtrl.GetCurrentSpineCtrl().skeleton.FindBone(_data[index].TargetBoneName), _trackRotation: _data[index].IsTrackRotation));
                                if (_summonIndex != -1)
                                    unitCtrl.battleManager.StartCoroutine(effect.TrackTargetSortForSummon(unitCtrl));
                                if (_isAura | _isShieldEffect)
                                {
                                    unitCtrl.battleManager.StartCoroutine(effect.TrackTargetSort(unitCtrl));
                                    unitCtrl.RepeatEffectList.Add(effect);
                                    unitCtrl.AuraEffectList.Add(effect);
                                    effect.IsAura = true;
                                }
                                effectCreated[index] = true;
                                if (_isIdle)
                                    unitCtrl.idleEffectsObjs.Add(effect);
                            }
                        }
                    }
                }
                yield return (object)null;
            }
        }*/

        /*public void CreateRunSmoke()
        {
            if ((double)this.MoveSpeed == 0.0 || (bool)this.IsMoveSpeedForceZero || (this.IsDead || (UnityEngine.Object)this.runSmokeEffect != (UnityEngine.Object)null) || !UnitUtility.IsPersonUnit(this.SoundUnitId))
                return;
            SkillEffectCtrl effect = this.battleEffectPool.GetEffect(this.IsOther ? Singleton<LCEGKJFKOPD>.Instance.IKCNCJAAHDD : Singleton<LCEGKJFKOPD>.Instance.PNBKBGIMHEO);
            effect.InitializeSort();
            effect.PlaySe(this.SoundUnitId, this.IsLeftDir);
            effect.transform.position = this.transform.position;
            effect.transform.parent = ExceptNGUIRoot.Transform;
            effect.SortTarget = this;
            effect.SetSortOrderBack();
            this.battleManager.StartCoroutine(effect.TrackTarget(this.GetFirstParts(true), Vector3.zero));
            effect.ExecAppendCoroutine();
            this.runSmokeEffect = effect;
        }*/

        /*private IEnumerator startShakeWithDelay(ShakeEffect shake, bool _ignorePause = false)
        {
            UnitCtrl GEDLBPMPOKB = this;
            float time = 0.0f;
            while (true)
            {
                time += _ignorePause ? GEDLBPMPOKB.battleManager.DeltaTime_60fps : GEDLBPMPOKB.DeltaTimeForPause;
                if ((double)time <= (double)shake.StartTime)
                    yield return (object)null;
                else
                    break;
            }
            GEDLBPMPOKB.battleCameraEffect.StartShake(shake, (Skill)null, GEDLBPMPOKB);
        }*/

        public bool IsCancelActionState(bool _isSkill1, int _skillId = -1)
        {
            bool flag = false;
            if (this.CancelByAwake && this.CurrentTriggerSkillId != _skillId || (this.CancelByConvert || this.CancelByToad))
                flag = true;
            switch (this.CurrentState)
            {
                case UnitCtrl.ActionState.SKILL_1:
                    flag = !_isSkill1;
                    break;
                case UnitCtrl.ActionState.DAMAGE:
                case UnitCtrl.ActionState.DIE:
                    flag = true;
                    break;
            }
            return flag;
        }

        public void DisappearForDivision(bool _fadeOut, bool _useCoroutine)
        {
            List<UnitCtrl> unitCtrlList = this.IsOther ? this.battleManager.EnemyList : this.battleManager.UnitList;
            if (unitCtrlList.Contains(this))
                unitCtrlList.Remove(this);
            this.IdleOnly = true;
            this.CureAllAbnormalState();
            if (!_fadeOut)
                return;
            if (_useCoroutine)
                this.StartCoroutine(this.updateDivisionDisappear());
            else
                this.AppendCoroutine(this.updateDivisionDisappear(), ePauseType.VISUAL);
        }

        private IEnumerator updateDivisionDisappear()
        {
            UnitCtrl unitCtrl = this;
            if ((long)unitCtrl.Hp <= 0L)
            {
                while (unitCtrl.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                    yield return (object)null;
            }
            float alpha = 1f;
            while ((double)alpha > 0.0)
            {
                alpha -= unitCtrl.battleManager.DeltaTime_60fps;
                unitCtrl.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, alpha);
                yield return (object)null;
            }
            unitCtrl.gameObject.SetActive(false);
        }

        public void StartUndoDivision(System.Action _callback, bool _isTimeOver)
        {
            if (_isTimeOver)
            {
                BattleSpineController currentSpineCtrl = this.GetCurrentSpineCtrl();
                currentSpineCtrl.Resume();
                currentSpineCtrl.CurColor = Color.white;
                this.SetSortOrderBack();
            }
            this.OnStartErrorUndoDivision(_isTimeOver);
            this.battleManager.StartCoroutine(this.waitUndoMotionEnd(_callback, _isTimeOver));
        }

        private IEnumerator waitUndoMotionEnd(System.Action _callback, bool _isTimeOver)
        {
            BattleSpineController unitSpineController = this.GetCurrentSpineCtrl();
            float deltaTimeAccumulated = 0.0f;
            while (true)
            {
                for (deltaTimeAccumulated += Time.deltaTime; (double)deltaTimeAccumulated > 0.0 & _isTimeOver; deltaTimeAccumulated -= this.battleManager.DeltaTime_60fps)
                {
                    unitSpineController.RealUpdate();
                    unitSpineController.RealLateUpdate();
                }
                if (unitSpineController.IsPlayAnime)
                    yield return (object)null;
                else
                    break;
            }
            _callback.Call();
        }

        //public void PlayDieEffect() => this.AppendCoroutine(this.CreatePrefabWithTime(this.dieEffects, _ignorePause: true), ePauseType.IGNORE_BLACK_OUT);

        public int ThanksTargetUnitId { get; set; }
        public bool UbIsDisableByChangePattern { get; set; }

        public int AnnihilationId { get; set; }

        public int UbCounter { get; set; }

        public bool IsCutInSkip { get; set; }

        public bool PlayCutInFlag { get; set; }

        public bool PlayingNoCutinMotion { get; set; }

        public int UbUsedCount { get; set; }

        public bool GetIsReduceEnergy()
        {
            foreach (KeyValuePair<UnitCtrl.eReduceEnergyType, bool> isReduceEnergy in this.IsReduceEnergyDictionary)
            {
                if (isReduceEnergy.Value)
                    return isReduceEnergy.Value;
            }
            return false;
        }

        public void InvokeSupportSkill(int _viewerId)
        {
            if (this.CutInFrameSet.SupportSkillUsed || this.battleManager.GameState != eBattleGameState.PLAY)
            {
                this.battleManager.CancelInvalidSupportSkill(this);
            }
            else
            {
                this.ResetPosForUserUnit(1);
                this.resetActionPatternAndCastTime();
                this.GetCurrentSpineCtrl().CurColor = Color.white;
                this.PlayAnime(eSpineCharacterAnimeId.RUN);
                this.SupportSkillEnd = false;
                this.SetLeftDirection(false);
                this.CutInFrameSet.CutInFrame = this.battleManager.JJCJONPDGIM + 1;
                this.CutInFrameSet.ServerCutInFrame = -2;
                this.CutInFrameSet.SupportSkillUsed = true;
            }
        }

        public void InvokeSupportSkillInResume(bool isOwner, int viewerId)
        {
            this.ResetPosForUserUnit(1);
            this.resetActionPatternAndCastTime();
            this.GetCurrentSpineCtrl().CurColor = Color.white;
            this.PlayAnime(eSpineCharacterAnimeId.RUN);
            this.SupportSkillEnd = false;
            this.SetLeftDirection(false);
            this.CutInFrameSet.SupportSkillUsed = true;
        }

        public eConsumeResult ConsumeEnergy()
        {
            if ((double)this.Energy < 1000.0)
                return eConsumeResult.FAILED;
            float energy = (float)(0.0 + 1000.0 * (double)(int)this.EnergyReduceRateZero / 100.0);
            if (!this.unitActionController.Skill1IsChargeTime)
            {
                this.SetEnergy(energy, eSetEnergyType.BY_USE_SKILL);
                return eConsumeResult.SKILL_OK;
            }
            if (this.unitActionController.DisableUBByModeChange)
                return eConsumeResult.FAILED;
            if (this.unitActionController.Skill1Charging)
            {
                this.SetEnergy(energy, eSetEnergyType.BY_USE_SKILL);
                this.unitActionController.Skill1Charging = false;
                return eConsumeResult.SKILL_RELEASE;
            }
            this.SetEnergy((float)(UnitDefine.MAX_ENERGY - 1), eSetEnergyType.BY_USE_SKILL);
            return eConsumeResult.SKILL_CHARGE;
        }

        public eConsumeResult ConsumeEnergySimulate()
        {
            if ((double)this.Energy < 1000.0)
                return eConsumeResult.FAILED;
            if (!this.unitActionController.Skill1IsChargeTime)
                return eConsumeResult.SKILL_OK;
            if (this.unitActionController.DisableUBByModeChange)
                return eConsumeResult.FAILED;
            return this.unitActionController.Skill1Charging ? eConsumeResult.SKILL_RELEASE : eConsumeResult.SKILL_CHARGE;
        }

        public void StartCutIn()
        {
            this.battleManager.ubmanager.UbExecCallback(this.battleManager.UnitList.IndexOf(this));
            ++this.UbCounter;
            //this.battleCameraEffect.DAHAALGOJNA = Vector3.zero;
            for (int index = 0; index < this.battleManager.UnitList.Count; ++index)
                this.battleManager.UnitList[index].ThanksTargetUnitId = 0;
            this.battleManager.FinishBlackFadeOut((UnitCtrl)null);
            this.SetSortOrderFront();
            this.battleManager.LPAAPDHAIIB = this;
            //this.soundManager.PlaySe(eSE.BTL_BUTTON_SKILL);
            //this.PlayCutInFlag = true;
            //MovieManager instance1 = ManagerSingleton<MovieManager>.Instance;
            bool skillPrincessForm = this.unitActionController.GetIsSkillPrincessForm(this.UnionBurstSkillId);
            //instance1.SetMirrorMode(skillPrincessForm && this.IsLeftDir == !this.battleManager.IsDefenceReplayMode);

            this.PlayCutInFlag = false;
            /*if (this.IsOther == this.battleManager.IsDefenceReplayMode)
                        {
                            SaveDataManager instance2 = ManagerSingleton<SaveDataManager>.Instance;
                            switch (instance2.CutInPlaybackType)
                            {
                                case ePlaybackType.EVERY_TIME:
                                    this.PlayCutInFlag = true;
                                    break;
                                case ePlaybackType.NONE:
                                    this.PlayCutInFlag = false;
                                    break;
                                case ePlaybackType.ONCE_A_DAY:
                                    if (instance2.IsCutInPlayedChara(this.UnitId) || this.MovieId == 0)
                                    {
                                        this.PlayCutInFlag = false;
                                        break;
                                    }
                                    this.PlayCutInFlag = true;
                                    instance2.AddCutInPlayedUnitId(this.UnitId);
                                    break;
                            }
                        }*/
            if (this.battleTimeScale.IsSpeedQuadruple)
                this.PlayCutInFlag = false;
            this.battleManager.FrameCountPause(this);
            if (skillPrincessForm)
            {
                //SingletonMonoBehaviour<BattleHeaderController>.Instance.PauseNoMoreTimeUp(true);
                BattleHeaderController.Instance.PauseNoMoreTimeUp(true);
                this.princessFormProcessor.Prepare();
                //Debug.LogError("UB特效鸽了！");
            }
            if (this.PlayCutInFlag && this.MovieId != 0)
            {
                /*this.battleManager.IsPlayCutin = true;
                int charaId = UnitUtility.GetCharaId(this.MovieId);
                this.cutinVoiceStatus = BattleVoiceUtility.SelectCutinVoice(this, this.battleManager.GetMyUnitList(), 1, this.battleTimeScale.SpeedUpFlag);
                //this.StartCoroutine(this.playCutInVoiceWithDelay(this.battleTimeScale.SpeedUpFlag ? this.SpeedUpCutInVoiceDelayWithCutIn[0] : this.NormalCutInVoiceDelayWithCutIn[0], 1, true));
                this.soundManager.PlaySeFromName(string.Format(UnitCtrl.CUT_IN_CUE_NAME, (object)charaId), string.Format(UnitCtrl.CUT_IN_CUE_NAME, (object)this.MovieId));
                this.battleManager.PauseAllSe();
                SingletonMonoBehaviour<UnitUiCtrl>.Instance.HidePopUp();
                this.battleManager.HideDamageNumBySkillScreen();
                bool changeFPS = this.battleManager.FameRate == 30 && this.battleTimeScale.SpeedUpFlag;
                DialogManager instance2 = ManagerSingleton<DialogManager>.Instance;
                if (instance2.IsUse(eDialogId.FLATOUT_PLAYER))
                    instance2.ForceCloseOne(eDialogId.FLATOUT_PLAYER);
                ViewManager instance3 = ManagerSingleton<ViewManager>.Instance;
                float num = 0.75f;
                instance1.SetScreenScale(eMovieType.CUT_IN, (long)this.MovieId, new Vector2(instance3.ActiveScreenWidthInUIRoot, instance3.ActiveScreenWidthInUIRoot * num), true);
                instance1.SetMoviePlayerLocalPosition(eMovieType.CUT_IN, (long)this.MovieId, WrapperUnityEngineScreen.GetRestoreNGUIOffsetYVector3());
                bool alreadyCalled = false;
                float _fadeStartTime = skillPrincessForm ? 0.0f : this.unitActionController.UnionBurstList[0].CutInMovieFadeStartTime;
                float _fadeDurationTime = skillPrincessForm ? 0.0f : this.unitActionController.UnionBurstList[0].CutInMovieFadeDurationTime;
                System.Action _callback = (System.Action)(() =>
               {
                   if (alreadyCalled)
                       return;
                   alreadyCalled = true;
                   if ((UnityEngine.Object)this.battleManager == (UnityEngine.Object)null)
                       return;
                   this.unitActionController.AddBlackoutTarget(this.UnionBurstSkillId);
                   this.MoviePlayed = true;
                   if (changeFPS)
                       PABCCELMCAJ.IKMGFNGHDPJ = 30;
                   this.battleManager.OnCutInEnd(this.IsOther || this.IsSummonOrPhantom, this);
               });
                instance1.Play(eMovieType.CUT_IN, (long)this.MovieId, eMoviePlayType.NORMAL, skillPrincessForm ? (System.Action)null : _callback, KNNABKOLJFI.Stop, _fadeStartTime, _fadeDurationTime);
                instance1.SetPlaySpeed(eMovieType.CUT_IN, (long)this.MovieId, this.battleTimeScale.SpeedUpFlag ? this.battleTimeScale.SpeedUpRate : 1f);
                if (changeFPS)
                    PABCCELMCAJ.IKMGFNGHDPJ = (int)((double)this.battleTimeScale.SpeedUpRate * 30.0);
                if (!skillPrincessForm)
                    return;
                this.battleManager.StartCoroutine(this.waitMovieEndFrame(_callback, this.MovieId));*/
            }
            else
            {
                //this.cutinVoiceStatus = BattleVoiceUtility.SelectCutinVoice(this, this.battleManager.GetMyUnitList(), 2, this.battleTimeScale.SpeedUpFlag);
                //this.StartCoroutine(this.playCutInVoiceWithDelay(this.battleTimeScale.SpeedUpFlag ? this.SpeedUpCutInVoiceDelay[0] : this.NormalCutInVoiceDelay[0], 2, false));
                this.PlayingNoCutinMotion = true;
                this.battleManager.isUBExecing = true;
                this.battleManager.isPrincessFormSkill = skillPrincessForm;
                //SingletonMonoBehaviour<BattleHeaderController>.Instance.PauseNoMoreTimeUp(true);
                BattleHeaderController.Instance.PauseNoMoreTimeUp(true);
                this.StartCoroutine(this.unitActionController.StartActionWithOutCutIn(this, this.UnionBurstSkillId, (System.Action)(() =>
               {
                   this.battleManager.isPrincessFormSkill = false;
                   this.PlayingNoCutinMotion = false;
                   this.battleManager.isUBExecing = false;
                   this.battleManager.OnCutInEnd(this.IsOther || this.IsSummonOrPhantom, this);
               })));
            }
        }

        /*public IEnumerator waitMovieEndFrame(System.Action _callback, int _movieId)
        {
            MovieManager movieManager = ManagerSingleton<MovieManager>.Instance;
            float oneFrame = 0.03333334f;
            float duration = 0.0f;
            while (true)
            {
                duration = movieManager.GetDurationTime(eMovieType.CUT_IN, (long)_movieId);
                if ((double)duration == 0.0)
                    yield return (object)null;
                else
                    break;
            }
            while ((double)movieManager.GetSeekPositionTime(eMovieType.CUT_IN, (long)_movieId) + (double)oneFrame + (double)oneFrame * 0.5 < (double)duration)
                yield return (object)null;
            _callback.Call();
        }*/

        public void StartAnnihilation() => this.StartCoroutine(this.unitActionController.StartAnnihilationSkillAnimation(++this.AnnihilationId));

        public bool IsSkillReady
        {
            get
            {
                if ((this.battleManager.BattleCategory == eBattleCategory.DUNGEON || this.battleManager.BattleCategory == eBattleCategory.TOWER || (this.battleManager.BattleCategory == eBattleCategory.TOWER_REHEARSAL || this.battleManager.BattleCategory == eBattleCategory.TOWER_EX) || (this.battleManager.BattleCategory == eBattleCategory.TOWER_REPLAY || this.battleManager.BattleCategory == eBattleCategory.TOWER_EX_REPLAY || this.battleManager.BattleCategory == eBattleCategory.TOWER_CLOISTER) ? 1 : (this.battleManager.BattleCategory == eBattleCategory.TOWER_CLOISTER_REPLAY ? 1 : 0)) != 0 && !this.StandByDone || this.battleManager.IsSpecialBattle && (double)this.battleManager.ActionStartTimeCounter > 0.0 || 
                    (this.battleManager.LOGNEDLPEIJ || this.skillTargetList.Count == 0 || (this.battleManager.GameState != eBattleGameState.PLAY || !this.battleManager.GetAdvPlayed())) || this.ModeChangeEnd)
                    return false;
                /*switch (this.battleManager.GetPurpose())
                {
                    case eHatsuneSpecialPurpose.SHIELD:
                    case eHatsuneSpecialPurpose.ABSORBER:
                        if (this.IdleOnly)
                            return false;
                        break;
                }*/
                return this.ToadDatas.Count <= 0 && !this.IsAbnormalState(UnitCtrl.eAbnormalState.UB_SILENCE) && !this.UbIsDisableByChangePattern && ((this.CurrentState != UnitCtrl.ActionState.SKILL_1 || this.unitActionController.Skill1IsChargeTime) && (!this.IsUnableActionState() && this.CurrentState != UnitCtrl.ActionState.DAMAGE)) && (this.UnionBurstSkillId != 0 && !this.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE) && (!this.IsDead && this.CurrentState != UnitCtrl.ActionState.DIE) && (!this.IsConfusionOrConvert() && (long)this.Hp > 0L && (!this.isAwakeMotion && !this.unitActionController.DisableUBByModeChange))) && ((double)this.Energy >= 1000.0 && this.SkillLevels[this.UnionBurstSkillId] != 0 && !this.GetIsReduceEnergy()) && this.battleManager.LAMHAIODABF == 0;
                //return this.ToadDatas.Count <= 0 && !this.IsAbnormalState(UnitCtrl.eAbnormalState.UB_SILENCE) && (this.CurrentState != UnitCtrl.ActionState.SKILL_1 || this.unitActionController.Skill1IsChargeTime) && (!this.IsUnableActionState() && this.CurrentState != UnitCtrl.ActionState.DAMAGE && (this.UnionBurstSkillId != 0 && !this.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE))) && (!this.IsDead && this.CurrentState != UnitCtrl.ActionState.DIE && (!this.IsConfusionOrConvert() && (long)this.Hp > 0L) && (!this.isAwakeMotion && !this.unitActionController.DisableUBByModeChange && ((double)this.Energy >= 1000.0 && this.SkillLevels[this.UnionBurstSkillId] != 0))) && !this.GetIsReduceEnergy() && this.battleManager.LAMHAIODABF == 0;
            }
        }

        public bool JudgeSkillReadyAndIsMyTurn() => this.isMyTurn() && this.IsSkillReady;

        public bool IsAutoOrUbExecTrying()
        {
            //if (this.IsUbExecTrying && (!this.battleManager.UnitUiCtrl.IsAutoMode || this.CurrentState != UnitCtrl.ActionState.IDLE) && this.JudgeSkillReadyAndIsMyTurn())
            if (this.IsUbExecTrying && (!MyGameCtrl.Instance.IsAutoMode || this.CurrentState != UnitCtrl.ActionState.IDLE) && this.JudgeSkillReadyAndIsMyTurn())
                this.battleManager.SetOnlyAutoClearFlag(false);
            if (this.IsUbExecTrying)
                return true;
            if (this.battleManager.ubmanager.IsUbExec(this.battleManager.UnitList.IndexOf(this)))
                return true;
            //return this.battleManager.UnitUiCtrl.IsAutoMode && this.CurrentState == UnitCtrl.ActionState.IDLE;
            return MyGameCtrl.Instance.IsAutoMode && this.CurrentState == UnitCtrl.ActionState.IDLE;
        }

        private bool isMyTurn() => this.battleManager.ChargeSkillTurn == eChargeSkillTurn.NONE || this.unitActionController.Skill1Charging;

        public enum ActionState
        {
            IDLE = 0,
            ATK = 1,
            SKILL_1 = 2,
            SKILL = 3,
            WALK = 4,
            DAMAGE = 5,
            DIE = 6,
            GAME_START = 7,
        }

        public delegate void OnDamageDelegate(bool byAttack, float damage, bool critical);

        public delegate void OnDeadDelegate(UnitCtrl owner);

        /*public struct VoiceTypeAndSkillNumber
        {
            public SoundManager.eVoiceType VoiceType;
            public int SkillNumber;
        }*/

        public enum eAbnormalState
        {
            NONE = 0,
            [Description("物伤减免")]
            GUARD_ATK = 1,
            TOP = 1,
            [Description("法伤减免")]
            GUARD_MGC = 2,
            [Description("物伤吸收")]
            DRAIN_ATK = 3,
            [Description("法伤吸收")]
            DRAIN_MGC = 4,
            [Description("伤害减免")]
            GUARD_BOTH = 5,
            [Description("伤害吸收")]
            DRAIN_BOTH = 6,
            [Description("加速")]
            HASTE = 7,
            [Description("中毒")]
            POISON = 8,
            [Description("烧伤")]
            BURN = 9,
            [Description("诅咒")]
            CURSE = 10, // 0x0000000A
            [Description("减速")]
            SLOW = 11, // 0x0000000B
            [Description("麻痹")]
            PARALYSIS = 12, // 0x0000000C
            [Description("冰冻")]
            FREEZE = 13, // 0x0000000D
            [Description("魅惑")]
            CONVERT = 14, // 0x0000000E
            [Description("黑暗")]
            PHYSICS_DARK = 15, // 0x0000000F
            [Description("沉默")]
            SILENCE = 16, // 0x00000010
            [Description("拘束")]
            CHAINED = 17, // 0x00000011
            [Description("睡眠")]
            SLEEP = 18, // 0x00000012
            [Description("击晕")]
            STUN = 19, // 0x00000013
            [Description("挽留")]
            DETAIN = 20, // 0x00000014
            [Description("DOT")]
            NO_EFFECT_SLIP_DAMAGE = 21, // 0x00000015
            [Description("无敌")]
            NO_DAMAGE_MOTION = 22, // 0x00000016
            [Description("免疫异常状态")]
            NO_ABNORMAL = 23, // 0x00000017
            [Description("免疫DEBUFF")]
            NO_DEBUF = 24, // 0x00000018
            [Description("伤害累加")]
            ACCUMULATIVE_DAMAGE = 25, // 0x00000019
            [Description("即死")]
            DECOY = 26, // 0x0000001A
            [Description("MIFUYU")]
            MIFUYU = 27, // 0x0000001B
            [Description("石化")]
            STONE = 28, // 0x0000001C
            [Description("反射")]
            REGENERATION = 29, // 0x0000001D
            [Description("物理闪避")]
            PHYSICS_DODGE = 30, // 0x0000001E
            [Description("混乱")]
            CONFUSION = 31, // 0x0000001F
            [Description("毒")]
            VENOM = 32, // 0x00000020
            [Description("黑暗")]
            COUNT_BLIND = 33, // 0x00000021
            [Description("禁疗")]
            INHIBIT_HEAL = 34, // 0x00000022
            [Description("恐惧")]
            FEAR = 35, // 0x00000023
            [Description("TP回复")]
            TP_REGENERATION = 36, // 0x00000024
            [Description("巫法")]
            HEX = 37, // 0x00000025
            [Description("虚弱")]
            FAINT = 38, // 0x00000026
            [Description("？？？")]
            PARTS_NO_DAMAGE = 39, // 0x00000027
            [Description("修正")]
            COMPENSATION = 40, // 0x00000028
            [Description("物伤减少")]
            CUT_ATK_DAMAGE = 41, // 0x00000029
            [Description("法伤减少")]
            CUT_MGC_DAMAGE = 42, // 0x0000002A
            [Description("伤害减少")]
            CUT_ALL_DAMAGE = 43, // 0x0000002B
            [Description("物理对数盾")]
            LOG_ATK_BARRIR = 44, // 0x0000002C
            [Description("魔法对数盾")]
            LOG_MGC_BARRIR = 45, // 0x0000002D
            [Description("对数盾")]
            LOG_ALL_BARRIR = 46, // 0x0000002E
            [Description("行动暂停")]
            PAUSE_ACTION = 47, // 0x0000002F
            [Description("不能放UB")]
            UB_SILENCE = 48, // 0x00000030
            [Description("魔法黑暗")]
            MAGIC_DARK = 49, // 0x00000031
            [Description("治疗下降")]
            HEAL_DOWN = 50, // 0x00000032
            [Description("击晕NPC")]
            NPC_STUN = 51, // 0x00000033
            NUM = 52, // 0x00000034


        }

        public class eAbnormalState_DictComparer : IEqualityComparer<UnitCtrl.eAbnormalState>
        {
            public bool Equals(UnitCtrl.eAbnormalState _x, UnitCtrl.eAbnormalState _y) => _x == _y;

            public int GetHashCode(UnitCtrl.eAbnormalState _obj) => (int)_obj;
        }

        public enum eAbnormalStateCategory
        {
            NONE = 0,
            TOP = 0,
            DAMAGE_RESISTANCE_MGK = 1,
            DAMAGE_RESISTANCE_ATK = 2,
            DAMAGE_RESISTANCE_BOTH = 3,
            POISON = 4,
            BURN = 5,
            CURSE = 6,
            SPEED = 7,
            FREEZE = 8,
            PARALYSIS = 9,
            STUN = 10, // 0x0000000A
            DETAIN = 11, // 0x0000000B
            CONVERT = 12, // 0x0000000C
            SILENCE = 13, // 0x0000000D
            PHYSICAL_DARK = 14, // 0x0000000E
            MAGIC_DARK = 15, // 0x0000000F
            MIFUYU = 16, // 0x00000010
            DECOY = 17, // 0x00000011
            NO_DAMAGE = 18, // 0x00000012
            NO_ABNORMAL = 19, // 0x00000013
            NO_DEBUF = 20, // 0x00000014
            SLEEP = 21, // 0x00000015
            CHAINED = 22, // 0x00000016
            ACCUMULATIVE_DAMAGE = 23, // 0x00000017
            NO_EFFECT_SLIP_DAMAGE = 24, // 0x00000018
            STONE = 25, // 0x00000019
            REGENERATION = 26, // 0x0000001A
            PHYSICS_DODGE = 27, // 0x0000001B
            CONFUSION = 28, // 0x0000001C
            VENOM = 29, // 0x0000001D
            COUNT_BLIND = 30, // 0x0000001E
            INHIBIT_HEAL = 31, // 0x0000001F
            FEAR = 32, // 0x00000020
            TP_REGENERATION = 33, // 0x00000021
            HEX = 34, // 0x00000022
            FAINT = 35, // 0x00000023
            PARTS_NO_DAMAGE = 36, // 0x00000024
            COMPENSATION = 37, // 0x00000025
            CUT_ATK_DAMAGE = 38, // 0x00000026
            CUT_MGC_DAMAGE = 39, // 0x00000027
            CUT_ALL_DAMAGE = 40, // 0x00000028
            LOG_ATK_BARRIR = 41, // 0x00000029
            LOG_MGC_BARRIR = 42, // 0x0000002A
            LOG_ALL_BARRIR = 43, // 0x0000002B
            PAUSE_ACTION = 44, // 0x0000002C
            UB_SILENCE = 45, // 0x0000002D
            HEAL_DOWN = 46, // 0x0000002E
            END = 47, // 0x0000002F
            NPC_STUN = 47, // 0x0000002F
            NUM = 48, // 0x00000030
        }

        public class eAbnormalStateCategory_DictComparer : IEqualityComparer<UnitCtrl.eAbnormalStateCategory>
        {
            public bool Equals(UnitCtrl.eAbnormalStateCategory _x, UnitCtrl.eAbnormalStateCategory _y) => _x == _y;

            public int GetHashCode(UnitCtrl.eAbnormalStateCategory _obj) => (int)_obj;
        }

        public enum eSpecialSleepStatus
        {
            INVALID = -1, // 0xFFFFFFFF
            START = 0,
            WAIT_START_END = 1,
            LOOP = 2,
            RELEASE = 3,
        }

        public enum BuffParamKind
        {
            [Description("物理攻击")]
            ATK = 1,
            [Description("物理防御")]
            DEF = 2,
            [Description("魔法攻击")]
            MAGIC_STR = 3,
            [Description("魔法防御")]
            MAGIC_DEF = 4,
            [Description("闪避")]
            DODGE = 5,
            [Description("物理暴击")]
            PHYSICAL_CRITICAL = 6,
            [Description("魔法暴击")]
            MAGIC_CRITICAL = 7,
            [Description("TP回复")]
            ENERGY_RECOVER_RATE = 8,
            [Description("生命偷取")]
            LIFE_STEAL = 9,
            [Description("移动速度")]
            MOVE_SPEED = 10, // 0x0000000A
            [Description("物理爆伤")]
            PHYSICAL_CRITICAL_DAMAGE_RATE = 11, // 0x0000000B
            [Description("魔法爆伤")]
            MAGIC_CRITICAL_DAMAGE_RATE = 12, // 0x0000000C
            [Description("命中")]
            ACCURACY = 13, // 0x0000000D
            MAX_HP = 100, // 0x00000064
            NONE = 101, // 0x00000065
            NUM = 101, // 0x00000065
        }

        public class BuffParamKind_DictComparer : IEqualityComparer<UnitCtrl.BuffParamKind>
        {
            public bool Equals(UnitCtrl.BuffParamKind _x, UnitCtrl.BuffParamKind _y) => _x == _y;

            public int GetHashCode(UnitCtrl.BuffParamKind _obj) => (int)_obj;
        }

        public class FunctionalComparer<T> : IComparer<T>
        {
            private Func<T, T, int> comparer;
            public static UnitCtrl.FunctionalComparer<T> Instance;

            public FunctionalComparer(Func<T, T, int> _comparer) => this.comparer = _comparer;

            public int Compare(T x, T y) => this.comparer(x, y);

            public void SetComparer(Func<T, T, int> _comparer) => this.comparer = _comparer;

            public static void CreateInstance() => UnitCtrl.FunctionalComparer<T>.Instance = new UnitCtrl.FunctionalComparer<T>((Func<T, T, int>)null);
        }

        public enum ePassiveSideType
        {
            OWNER,
            ALL,
            ALL_OTHER,
        }

        public enum eInhibitHealType
        {
            PHYSICS,
            MAGIC,
            NO_EFFECT,
        }

        public enum eReduceEnergyType
        {
            MODE_CHANGE,
            SEARCH_AREA,
            CONTINUOUS_DAMAGE_NEARBY,
            CONTINUOUS_DAMAGE,
        }

        public class eReduceEnergyType_DictComparer : IEqualityComparer<UnitCtrl.eReduceEnergyType>
        {
            public bool Equals(UnitCtrl.eReduceEnergyType _x, UnitCtrl.eReduceEnergyType _y) => _x == _y;

            public int GetHashCode(UnitCtrl.eReduceEnergyType _obj) => (int)_obj;
        }
    }
}
