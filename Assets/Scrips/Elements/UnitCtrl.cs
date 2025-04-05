// Decompiled with JetBrains decompiler
// Type: Elements.UnitCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Cute;
using Elements.Battle;
using PCRCaculator;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using Spine;
using Spine.Unity;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using Attachment = Spine.Attachment;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
//using Cute.Cri.Movie;
//using Elements.Data;

namespace Elements
{
    public class AccumulativeDamageSourceData // TypeDefIndex: 2274
    {
        // Fields
        private eAccumulativeDamageType AccumulativeDamageType; // 0x10
        private float effectValue; // 0x14
        private int countLimit; // 0x18

        // Methods

        // RVA: 0x2ED65A4 Offset: 0x2ED65A4 VA: 0x2ED65A4
        public AccumulativeDamageSourceData(eAccumulativeDamageType _damageType, float _effectValue, int _countLimit)
        {
            this.AccumulativeDamageType = _damageType;
            this.effectValue = _effectValue;
            this.countLimit = _countLimit;
        }

        public AccumulativeDamageData CreateAccumulativeDamageData()
        {
            if (AccumulativeDamageType == eAccumulativeDamageType.FIXED)
            {
                return new AccumulativeDamageData
                {
                    AccumulativeDamageType = eAccumulativeDamageType.FIXED,
                    FixedValue = effectValue,
                    CountLimit = countLimit
                };
            }
            else
            {
                return new AccumulativeDamageData
                {
                    AccumulativeDamageType = eAccumulativeDamageType.PERCENTAGE,
                    PercentageValue = effectValue,
                    CountLimit = countLimit
                };
            }
        }
    }

    public class BasePartsDataEx
    {
        public FloatWithEx Energy, Hp, GetAtkZeroEx, GetMagicStrZeroEx;
        public long MaxHp;
        public string UnitName;
        public int hash;
    }

    public partial class UnitCtrl : FixedTransformMonoBehavior, ISingletonField
    {
        private PrincessFormProcessor princessFormProcessor;
        public const float SLOW_ANIM_SPEED = 0.5f;
        public const float HASTE_ANIM_SPEED = 2f;
        public const float START_CAST_TIME = 0.3f;
        public const float START_CAST_TIME_STAND_BY = 2.5f;
        public const int DIE_MOTION1 = 1;
        public const int MODE_CHANGE1 = 1;
        private static readonly Color32 DAMAGE_FLASH_COLOR_32 = new Color32(byte.MaxValue, 60, 30, byte.MaxValue);
        // Token: 0x060038DD RID: 14557 RVA: 0x000E7B64 File Offset: 0x000E7B64
        private Dictionary<int, OverwriteAbnormalStateData> overwriteSpeedData = new Dictionary<int, OverwriteAbnormalStateData>();
        public void AddOverwriteAbnormalState(int _counter, OverwriteAbnormalStateData _data)
        {
            this.overwriteSpeedData.Add(_counter, _data);
            int num = this.overwriteSpeedData.Keys.Max();
            if (_counter != num)
            {
                return;
            }
            this.activeOverwriteAbnormalState(_data);
        }
        public void RemoveOverwriteAbnormalState(int _counter)
        {
            Dictionary<int, OverwriteAbnormalStateData>.KeyCollection keys = this.overwriteSpeedData.Keys;
            if (keys.Count == 0)
            {
                return;
            }
            int num = keys.Max();
            OverwriteAbnormalStateData overwriteAbnormalStateData = this.overwriteSpeedData[_counter];
            this.overwriteSpeedData.Remove(_counter);
            if (_counter != num)
            {
                return;
            }
            if (this.OverwriteSpeedDataCount == 0)
            {
                UnitCtrl.eAbnormalState abnormalState = UnitCtrl.GetAbnormalState(overwriteAbnormalStateData.AbnormalState);
                this.DisableAbnormalStateById(abnormalState, overwriteAbnormalStateData.ActionParameter.ActionId, false);
                this.updateEffectForSpeedAbnormalState(false);
                return;
            }
            num = this.overwriteSpeedData.Keys.Max();
            this.activeOverwriteAbnormalState(this.overwriteSpeedData[num]);
        }
        // Token: 0x060038DF RID: 14559 RVA: 0x000E7C38 File Offset: 0x000E7C38
        private void activeOverwriteAbnormalState(OverwriteAbnormalStateData _data)
        {
            UnitCtrl.eAbnormalState abnormalState = UnitCtrl.GetAbnormalState(_data.AbnormalState);
            this.SetAbnormalState(_data.Source, abnormalState, 9999f, _data.ActionParameter, _data.Skill, _data.EffectValue, 0f, false, false, 1f, true);
            if (abnormalState == UnitCtrl.eAbnormalState.HASTE && !BattleUtil.LessThanOrApproximately(this.CalcAbnormalStateSpeed(), 1f))
            {
                this.SetBuffParam(UnitCtrl.BuffParamKind.NUM, UnitCtrl.BuffParamKind.NUM, new Dictionary<BasePartsData, FloatWithEx>
                {
                    {
                        _data.TargetParts,
                        0
                    }
                }, 0.5f, _data.Skill.SkillId, _data.Source, true, eEffectType.COMMON, true, false, false, 0);
            }
            this.updateEffectForSpeedAbnormalState(true);
        }

        private static readonly Color DAMAGE_FLASH_COLOR = DAMAGE_FLASH_COLOR_32;
        public const int DAMAGE_FLASH_FRAME = 4;
        public const int FLASH_DELAY_FRAME = 0;
        private const float SORT_OFFSET_RESET_TIME = 0.6f;
        private const int ATTACK_PATTERN_TYPE_DIGIT = 1000;
        public static UnitCtrl.eAbnormalState GetAbnormalState(UnitCtrl.eOverwriteAbnormalState _abnormalState)
        {
            if (_abnormalState == UnitCtrl.eOverwriteAbnormalState.SLOW)
            {
                return UnitCtrl.eAbnormalState.SLOW;
            }
            if (_abnormalState != UnitCtrl.eOverwriteAbnormalState.HASTE)
            {
                return UnitCtrl.eAbnormalState.NONE;
            }
            return UnitCtrl.eAbnormalState.HASTE;
        }

        private const int SUMMON_LOOP_MOTION_SUFFIX = 1;
        private const int SUMMON_LOOP_END_MOTION_SUFFIX = 2;
        private const float VOICE_FADE_DURATION = 0.3f;
        private const float BOSS_SHADOW_BASE_X = 100f;
        private const float PERCENT_DIGIT = 100f;
        public Dictionary<int, int> SkillUseCount = new Dictionary<int, int>();
        public int CurrentRotateCoroutineId;
        [SerializeField]
        private List<ShakeEffect> gameStartShakes;
        /*
        [SerializeField]
        private List<PrefabWithTime> gameStartEffects;
        [SerializeField]
        private List<PrefabWithTime> dieEffects;
        [SerializeField]
        private List<PrefabWithTime> damageEffects;
        public List<PrefabWithTime> SummonEffects;
        [SerializeField]
        private List<PrefabWithTime> idleEffects;
        [SerializeField]
        private List<PrefabWithTime> auraEffects;*/
        [SerializeField]
        private List<ShakeEffect> dieShakes;
        [SerializeField]
        private List<ShakeEffect> damageShakes;
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

        public float fieldTime;
        //private LifeGaugeController lifeGauge;
        private Transform bottomTransform;
        private bool m_bPause;
        private int m_sSortOrder;
        private UnitActionController unitActionController;
        //private int hitEffectSortOffset;
        //private float sortOffsetResetTimer = 0.6f;
        //private IAutoLogic[] mainUnionBurstAutoLogics;

        //private IAutoLogic[] subUnionBurstAutoLogics;
        //private PrefabWithTime.eEffectDifficulty effectDifficulty;
        //private static Yggdrasil<UnitCtrl> staticSingletonTree = (Yggdrasil<UnitCtrl>)null;
        private static BattleLogIntreface staticBattleLog;
        //private static INADFELJLMH staticBattleCameraEffect = (INADFELJLMH)null;
        //private static BattleEffectPoolInterface staticBattleEffectPool = (BattleEffectPoolInterface)null;
        private static BattleSpeedInterface staticBattleTimeScale;
        private float resultMoveDistance;
        public Action<UnitCtrl> OnDieForZeroHp;
        public Action<UnitCtrl> OnDieFadeOutEnd;
        public Action<UnitCtrl> EnergyChange;
        public OnDamageDelegate OnDamage;
        public Action OnSlipDamage;
        public OnDamageDelegate OnHpChange;
        public OnDeadDelegate OnDeadForRevival;
        public Dictionary<int, int> SkillLevels = new Dictionary<int, int>();
        public Dictionary<int, float> SkillAreaWidthList = new Dictionary<int, float>();
        //private Dictionary<int, UnitCtrl.VoiceTypeAndSkillNumber> voiceTypeDictionary = new Dictionary<int, UnitCtrl.VoiceTypeAndSkillNumber>();
        public Dictionary<int, eSpineCharacterAnimeId> animeIdDictionary = new Dictionary<int, eSpineCharacterAnimeId>();
        public List<int> MainSkillIdList = new List<int>();
        public List<int> SpecialSkillIdList = new List<int>();
        public List<int> MainSkillEvolutionIdList = new List<int>();
        public List<int> SpecialSkillEvolutionIdList = new List<int>();
        public List<int> SubUnionBurstIdList = new List<int>();
        public List<eSpineCharacterAnimeId> TreasureAnimeIdList = new List<eSpineCharacterAnimeId>();
        private bool isDeadBySetCurrentHp;
        private static BattleManager staticBattleManager;
        private List<ExSkillData> startExSkillList = new List<ExSkillData>();
        private Dictionary<eExSkillCondition, List<ExSkillData>> conditionExSkillDictionary = new Dictionary<eExSkillCondition, List<ExSkillData>>();

        private bool startSkillExeced;
        private Dictionary<eAbnormalState, bool> m_abnormalState = new Dictionary<eAbnormalState, bool>(new eAbnormalState_DictComparer());
        private Dictionary<eAbnormalStateCategory, AbnormalStateCategoryData> abnormalStateCategoryDataDictionary = new Dictionary<eAbnormalStateCategory, AbnormalStateCategoryData>(new eAbnormalStateCategory_DictComparer());
        private int overlapAbnormalStateCount;
        private Dictionary<eAbnormalState, List<int>> overlapAbnormalStateIndexList = new Dictionary<eAbnormalState, List<int>>
        {
            {
                eAbnormalState.SLOW_OVERLAP,
                new List<int>()
            },
            {
                eAbnormalState.HASTE_OVERLAP,
                new List<int>()
            }
        };
        private Dictionary<int, AbnormalStateCategoryData> overlapAbnormalStateData = new Dictionary<int, AbnormalStateCategoryData>();

        private Dictionary<GameObject, int> statusEffectOrderDictionary = new Dictionary<GameObject, int>();
        public static readonly Dictionary<eAbnormalState, AbnormalConstData> ABNORMAL_CONST_DATA = new Dictionary<eAbnormalState, AbnormalConstData>(new eAbnormalState_DictComparer())
        {
            {
                eAbnormalState.GUARD_ATK,
                new AbnormalConstData
                {
                    IconType = eStateIconType.PHYSICS_BARRIAR,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.GUARD_MGC,
                new AbnormalConstData
                {
                    IconType = eStateIconType.MAGIC_BARRIAR,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.DRAIN_ATK,
                new AbnormalConstData
                {
                    IconType = eStateIconType.PHYSICS_DRAIN_BARRIAR,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.DRAIN_MGC,
                new AbnormalConstData
                {
                    IconType = eStateIconType.MAGIC_DRAIN_BARRIAR,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.GUARD_BOTH,
                new AbnormalConstData
                {
                    IconType = eStateIconType.BOTH_BARRIAR,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.DRAIN_BOTH,
                new AbnormalConstData
                {
                    IconType = eStateIconType.BOTH_DRAIN_BARRIAR,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.HASTE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.HASTE,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.POISON,
                new AbnormalConstData
                {
                    IconType = eStateIconType.SLIP_DAMAGE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.POISON2,
                new AbnormalConstData
                {
                    IconType = eStateIconType.SLIP_DAMAGE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.BURN,
                new AbnormalConstData
                {
                    IconType = eStateIconType.BURN,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.CURSE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CURSE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.CURSE2,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CURSE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.SLOW,
                new AbnormalConstData
                {
                    IconType = eStateIconType.SLOW,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.PARALYSIS,
                new AbnormalConstData
                {
                    IconType = eStateIconType.PARALISYS,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.FREEZE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.FREEZE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.CONVERT,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CONVERT,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.PHYSICS_DARK,
                new AbnormalConstData
                {
                    IconType = eStateIconType.PHYSICS_DARK,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.MAGIC_DARK,
                new AbnormalConstData
                {
                    IconType = eStateIconType.MAGIC_DARK,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.SILENCE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.SILENCE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.CHAINED,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CHAINED,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.SLEEP,
                new AbnormalConstData
                {
                    IconType = eStateIconType.SLEEP,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.STUN,
                new AbnormalConstData
                {
                    IconType = eStateIconType.STUN,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.STUN2,
                new AbnormalConstData
                {
                    IconType = eStateIconType.STUN,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.DETAIN,
                new AbnormalConstData
                {
                    IconType = eStateIconType.DETAIN,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.NO_EFFECT_SLIP_DAMAGE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.SLIP_DAMAGE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.NO_DAMAGE_MOTION,
                new AbnormalConstData
                {
                    IconType = eStateIconType.NO_DAMAGE,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.NO_DAMAGE_MOTION2,
                new AbnormalConstData
                {
                    IconType = eStateIconType.NO_DAMAGE,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.NO_ABNORMAL,
                new AbnormalConstData
                {
                    IconType = eStateIconType.DEBUF_BARRIAR,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.NO_DEBUF,
                new AbnormalConstData
                {
                    IconType = eStateIconType.DEBUF_BARRIAR,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.PARTS_NO_DAMAGE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.NONE,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.ACCUMULATIVE_DAMAGE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.NONE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.ACCUMULATIVE_DAMAGE_FOR_ALL_ENEMY,
                new AbnormalConstData
                {
                    IconType = eStateIconType.NONE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.DECOY,
                new AbnormalConstData
                {
                    IconType = eStateIconType.DECOY,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.MIFUYU,
                new AbnormalConstData
                {
                    IconType = eStateIconType.NONE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.STONE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.STONE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.REGENERATION,
                new AbnormalConstData
                {
                    IconType = eStateIconType.REGENERATION,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.REGENERATION2,
                new AbnormalConstData
                {
                    IconType = eStateIconType.REGENERATION,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.PHYSICS_DODGE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.PHYSICS_DODGE,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.CONFUSION,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CONFUSION,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.CONFUSION2,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CONFUSION,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.VENOM,
                new AbnormalConstData
                {
                    IconType = eStateIconType.VENOM,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.COUNT_BLIND,
                new AbnormalConstData
                {
                    IconType = eStateIconType.COUNT_BLIND,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.INHIBIT_HEAL,
                new AbnormalConstData
                {
                    IconType = eStateIconType.INHIBIT_HEAL,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.FEAR,
                new AbnormalConstData
                {
                    IconType = eStateIconType.FEAR,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.TP_REGENERATION,
                new AbnormalConstData
                {
                    IconType = eStateIconType.TP_REGENERATION,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.TP_REGENERATION2,
                new AbnormalConstData
                {
                    IconType = eStateIconType.TP_REGENERATION,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.HEX,
                new AbnormalConstData
                {
                    IconType = eStateIconType.HEX,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.FAINT,
                new AbnormalConstData
                {
                    IconType = eStateIconType.FAINT,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.COMPENSATION,
                new AbnormalConstData
                {
                    IconType = eStateIconType.COMPENSATION,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.CUT_ATK_DAMAGE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CUT_ATK_DAMAGE,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.CUT_MGC_DAMAGE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CUT_MGC_DAMAGE,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.CUT_ALL_DAMAGE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CUT_ALL_DAMAGE,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.LOG_ATK_BARRIR,
                new AbnormalConstData
                {
                    IconType = eStateIconType.LOG_ATK_BARRIER,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.LOG_MGC_BARRIR,
                new AbnormalConstData
                {
                    IconType = eStateIconType.LOG_MGC_BARRIER,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.LOG_ALL_BARRIR,
                new AbnormalConstData
                {
                    IconType = eStateIconType.LOG_ALL_BARRIER,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.PAUSE_ACTION,
                new AbnormalConstData
                {
                    IconType = eStateIconType.PAUSE_ACTION,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.UB_SILENCE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.UB_SILENCE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.HEAL_DOWN,
                new AbnormalConstData
                {
                    IconType = eStateIconType.HEAL_DOWN,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.NPC_STUN,
                new AbnormalConstData
                {
                    IconType = eStateIconType.NPC_STUN,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.DECREASE_HEAL,
                new AbnormalConstData
                {
                    IconType = eStateIconType.DECREASE_HEAL,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.POISON_BY_BEHAVIOUR,
                new AbnormalConstData
                {
                    IconType = eStateIconType.POISON_BY_BEHAVIOUR,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.CRYSTALIZE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.CRYSTALIZE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.DAMAGE_LIMIT_ATK,
                new AbnormalConstData
                {
                    IconType = eStateIconType.DAMAGE_LIMIT,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.DAMAGE_LIMIT_MGC,
                new AbnormalConstData
                {
                    IconType = eStateIconType.DAMAGE_LIMIT,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.DAMAGE_LIMIT_ALL,
                new AbnormalConstData
                {
                    IconType = eStateIconType.DAMAGE_LIMIT,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.SLOW_OVERLAP,
                new AbnormalConstData
                {
                    IconType = eStateIconType.NONE,
                    IsBuff = false
                }
            },
            {
                eAbnormalState.HASTE_OVERLAP,
                new AbnormalConstData
                {
                    IconType = eStateIconType.NONE,
                    IsBuff = true
                }
            },
            {
                eAbnormalState.SPY,
                new AbnormalConstData
                {
                    IconType = eStateIconType.SPY,
                    IsBuff = true
                }
            },
            {
                UnitCtrl.eAbnormalState.ENERGY_DAMAGE_REDUCE,
                new AbnormalConstData
                {
                    IconType = eStateIconType.ENERGY_DAMAGE_REDUCE,
                    IsBuff = true
                }
            },
            {
                UnitCtrl.eAbnormalState.BLACK_FRAME,
                new AbnormalConstData
                {
                    IconType = eStateIconType.BLACK_FRAME,
                    IsBuff = false
                }
            },
            {
                UnitCtrl.eAbnormalState.UNABLE_STATE_GUARD,
                new AbnormalConstData
                {
                    IconType = eStateIconType.BLACK_FRAME,
                    IsBuff = true
                }
            },
            {
                UnitCtrl.eAbnormalState.FLIGHT,
                new AbnormalConstData
                {
                    IconType = eStateIconType.FLIGHT,
                    IsBuff = true
                }
            }
        };

        // Token: 0x040026C0 RID: 9920
        private Dictionary<UnitCtrl.eAbnormalStateCategory, int> slipDamageIdDictionary = new Dictionary<UnitCtrl.eAbnormalStateCategory, int>
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
            },
            {
                UnitCtrl.eAbnormalStateCategory.POISON2,
                0
            },
            {
                UnitCtrl.eAbnormalStateCategory.CURSE2,
                0
            },
            {
                UnitCtrl.eAbnormalStateCategory.BLACK_FRAME,
                0
            }
        };

        public Dictionary<eAbnormalState, Action<bool>> damageByBehaviourDictionary = new Dictionary<eAbnormalState, Action<bool>>();

        private Dictionary<eAbnormalState, int> abnormalStateToCurrentId = new Dictionary<eAbnormalState, int>();

        private bool isDamageReleaseSpecialSleepAnimForUnionBurst;

        private bool isPlayDamageAnimForAbnormal;

        private static readonly Color WEAK_COLOR = new Color(0.5411f, 0.6274f, 0.996f);
        private const float RUNOUT_COEFFICIENT = 1.5f;
        private const float RESULT_POSITION_COEFFICIENT_X = 1.5f;
        private const float ZOOM_COEFFICIENT = 1.15f;
        private const float ZOOM_SPAN = 0.3f;
        private const int CHARACTER_DISTANCE = 220;
        private const int CHARACTER_OFFSET_Y = -165;
        private float scaleValue;
        private const float RESULT_UNIT_SCALE = 0.875f;
        private float scaleForGuest = 1f;

        private int maxHpDebufCounter;

        public static readonly Dictionary<BuffParamKind, BuffDebuffConstData> BUFF_DEBUFF_ICON_DIC = new Dictionary<BuffParamKind, BuffDebuffConstData>(new BuffParamKind_DictComparer())
        {
            {
                BuffParamKind.NUM,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.NONE,
                    DebuffIcon = eStateIconType.NONE
                }
            },
            {
                BuffParamKind.ATK,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_PHYSICAL_ATK,
                    DebuffIcon = eStateIconType.DEBUFF_PHYSICAL_ATK
                }
            },
            {
                BuffParamKind.DEF,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_PHYSICAL_DEF,
                    DebuffIcon = eStateIconType.DEBUFF_PHYSICAL_DEF
                }
            },
            {
                BuffParamKind.MAGIC_STR,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_MAGIC_ATK,
                    DebuffIcon = eStateIconType.DEBUFF_MAGIC_ATK
                }
            },
            {
                BuffParamKind.MAGIC_DEF,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_MAGIC_DEF,
                    DebuffIcon = eStateIconType.DEBUFF_MAGIC_DEF
                }
            },
            {
                BuffParamKind.DODGE,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_DODGE,
                    DebuffIcon = eStateIconType.DEBUFF_DODGE
                }
            },
            {
                BuffParamKind.PHYSICAL_CRITICAL,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_CRITICAL,
                    DebuffIcon = eStateIconType.DEBUFF_CRITICAL
                }
            },
            {
                BuffParamKind.MAGIC_CRITICAL,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_CRITICAL,
                    DebuffIcon = eStateIconType.DEBUFF_CRITICAL
                }
            },
            {
                BuffParamKind.ENERGY_RECOVER_RATE,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_ENERGY_RECOVERY,
                    DebuffIcon = eStateIconType.DEBUFF_ENERGY_RECOVERY
                }
            },
            {
                BuffParamKind.LIFE_STEAL,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_LIFE_STEAL,
                    DebuffIcon = eStateIconType.DEBUFF_LIFE_STEAL
                }
            },
            {
                BuffParamKind.MOVE_SPEED,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.NONE,
                    DebuffIcon = eStateIconType.DEBUFF_MOVE_SPEED
                }
            },
            {
                BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_PHYSICAL_CRITICAL_DAMAGE,
                    DebuffIcon = eStateIconType.DEBUFF_PHYSICAL_CRITICAL_DAMAGE
                }
            },
            {
                BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_MAGIC_CRITICAL_DAMAGE,
                    DebuffIcon = eStateIconType.DEBUFF_MAGIC_CRITICAL_DAMAGE
                }
            },
            {
                BuffParamKind.ACCURACY,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_ACCURACY,
                    DebuffIcon = eStateIconType.DEBUFF_ACCURACY
                }
            },
            {
                BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.BUFF_RECEIVE_CRITICAL_DAMAGE,
                    DebuffIcon = eStateIconType.DEBUFF_RECEIVE_CRITICAL_DAMAGE
                }
            },
            {
                BuffParamKind.RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.NONE,
                    DebuffIcon = eStateIconType.DEBUFF_RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT
                }
            },
            {
                BuffParamKind.RECEIVE_PHYSICAL_DAMAGE_PERCENT,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.NONE,
                    DebuffIcon = eStateIconType.DEBUFF_RECEIVE_PHYSICAL_DAMAGE_PERCENT
                }
            },
            {
                BuffParamKind.RECEIVE_MAGIC_DAMAGE_PERCENT,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.NONE,
                    DebuffIcon = eStateIconType.DEBUFF_RECEIVE_MAGIC_DAMAGE_PERCENT
                }
            },
            {
                BuffParamKind.MAX_HP,
                new BuffDebuffConstData
                {
                    BuffIcon = eStateIconType.NONE,
                    DebuffIcon = eStateIconType.DEBUFF_MAX_HP
                }
            }
        };

        public static readonly Dictionary<BuffParamKind, eStateIconType> ADDITIONAL_BUFF_ICON_DIC = new Dictionary<BuffParamKind, eStateIconType>
        {
            {
                BuffParamKind.NUM,
                eStateIconType.NONE
            },
            {
                BuffParamKind.DEF,
                eStateIconType.ADDITIONAL_BUFF_PHYSICAL_DEF
            },
            {
                BuffParamKind.MAGIC_DEF,
                eStateIconType.ADDITIONAL_BUFF_MAGIC_DEF
            }
        };
        private List<int> buffDebuffSkilIds = new List<int>();
        public const float PREFAB_CREATE_OFFSET_TIME = 1f;
        private const float PERCENTAGE_DIGIT = 100f;
        public List<PartsData> BossPartsList = new List<PartsData>();
        public bool UseTargetCursorOver;
        public bool OneRemainingDisableEffect;
        public float OverCursorSize = 1f;
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
        public Action<bool> OnStartErrorUndoDivision;
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
        private bool setStateCalled;

        private bool isNotPartsBossReady;

        
        public bool IsUbExecTrying;
        public Dictionary<eReduceEnergyType, bool> IsReduceEnergyDictionary = new Dictionary<eReduceEnergyType, bool>(new eReduceEnergyType_DictComparer())
    {
      {
        eReduceEnergyType.MODE_CHANGE,
        false
      },
      {
        eReduceEnergyType.SEARCH_AREA,
        false
      },
      {
        eReduceEnergyType.CONTINUOUS_DAMAGE_NEARBY,
        false
      },
      {
        eReduceEnergyType.CONTINUOUS_DAMAGE,
        false
      }
    };

        public Bone StateBone { get; set; }
        public Bone StateBoneModeChange
        {
            get;
            set;
        }
        public Bone CenterBone { get; set; }
        public Bone CenterBoneModeChange
        {
            get;
            set;
        }
        public List<SkillEffectCtrl> RepeatEffectList { get; private set; }

        public List<SkillEffectCtrl> AuraEffectList { get; private set; }
        public Dictionary<int, List<(SkillEffectCtrl effect, string boneName)>> DamagedHpEffectDictionary
        {
            get;
            set;
        } = new Dictionary<int, List<(SkillEffectCtrl, string)>>();

        public float BodyWidth { get; set; }

        public SystemIdDefine.eWeaponSeType WeaponSeType { set; get; }

        public SystemIdDefine.eWeaponMotionType WeaponMotionType { set; get; }

        public BattleSpineController UnitSpineCtrl { set; get; }

        public BattleSpineController UnitSpineCtrlModeChange { set; get; }

        //public UnitDamageInfo UnitDamageInfo { get; set; }

        //public List<CircleEffectController> CircleEffectList { get; private set; }

        public bool IsDead
        {
            get => _isDead;
            set
            {
                battleManager.QueueUpdateSkillTarget();
                _isDead = value;
            }
        }

        public bool IsRecreated { get; set; }

        public bool MainSkill1Evolved { get; set; }

        //public float DeltaTimeForPause => PlayingNoCutinMotion || !(battleManager == null) && !m_bPause ? battleManager.DeltaTime_60fps : 0.0f;

        public float DeltaTimeForPause
        {
            get
            {
                if (PlayingNoCutinMotion)
                {
                    return battleManager.DeltaTime_60fps;
                }
                if (battleManager == null)
                {
                    return 0f;
                }
                if (battleManager.IsPausingEffectSkippedInThisFrame)
                {
                    return 0f;
                }
                if (!m_bPause)
                {
                    return battleManager.DeltaTime_60fps;
                }
                return 0f;
            }
        }
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

        public bool IdleOnly
        {
            get => _idleOnly;
            set
            {
                battleManager.QueueUpdateSkillTarget();
                _idleOnly = value;
            }
        }

        public bool HasUnDeadTime
        {
            get => _hasUnDeadTime;
            set
            {
                battleManager.QueueUpdateSkillTarget();
                _hasUnDeadTime = value;
            }
        }

        public int UnDeadTimeHitCount { get; set; }

        public UnitParameter UnitParameter { get; set; }

        public bool IsOther { get; set; }
        public bool IsBonusEnemy
        {
            get;
            set;
        }
        public bool IsPlayerUnit => !IsOther;// == battleManager.BJKKBMOLHDH;

        public bool JoyFlag { get; set; }

        public SummonAction.eSummonType SummonType { get; set; }

        public bool IsSummonOrPhantom;
        public bool IsGuest
        {
            get;
            set;
        }

        public bool IsGuestFadeout
        {
            get;
            set;
        }
        public bool IsDivision => SummonType == SummonAction.eSummonType.DIVISION;

        public bool IsDivisionSourceForDamage { get; set; }

        public bool IsDivisionSourceForDie { get; set; }

        public bool IsPhantom => SummonType == SummonAction.eSummonType.PHANTOM;

        public bool IsShadow { get; set; }

        public float OverlapPosX { get; set; }

        public bool MoviePlayed { get; set; }

        public List<long> ActionsTargetOnMe { get; set; }

        public List<FirearmCtrl> FirearmCtrlsOnMe { get; set; }

        private int motionPrefix { get; set; }

        public int MotionPrefix
        {
            get => ToadDatas.Count <= 0 ? motionPrefix : -1;
            set => motionPrefix = value;
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

        public Vector3 FixedCenterPos => new Vector3(IsLeftDir || IsForceLeftDirOrPartsBoss ? -fixedCenterPos.x : fixedCenterPos.x, fixedCenterPos.y, fixedCenterPos.z);

        private Vector3 fixedCenterPos { get; set; }

        public Vector3 FixedStatePos { get; set; }

        public Vector3 ColliderCenter { get; private set; }

        public Vector3 ColliderSize { get; private set; }

        public int CurrentSkillId { get; set; }

        public Action OnIsFrontFalse { get; set; }

        public bool HasDieLoop => !(GetCurrentSpineCtrl() == null) && GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.DIE_LOOP);

        //public VoiceTimingData SpecialVoiceTimingData { get; private set; }

        public bool DisableSortOrderFrontOnBlackoutTarget { get; set; }

        public int SkinRarity { get; set; }

        public List<BattleSpineController> EffectSpineControllerList { get; private set; } = new List<BattleSpineController>();

        public bool IsDepthBack { get; set; }

        public bool IsForceLeftDirOrPartsBoss => IsForceLeftDir || IsPartsBoss;

        private ActionState currentState;
        public ActionState CurrentState
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

        private int skillTargetCacheKey = -1;
        private int skillTargetCacheKeyForSkillReady = -1;

        private List<UnitCtrl> targetPlayerList { get; set; }

        private float m_fCastTimer { get; set; }

        private FloatWithEx accumulateDamage { get; set; }

        private float skillStackValDmg { get; set; }

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

        protected BattleLogIntreface battleLog => staticBattleLog;

        //protected INADFELJLMH battleCameraEffect => UnitCtrl.staticBattleCameraEffect;

        //protected BattleEffectPoolInterface battleEffectPool => UnitCtrl.staticBattleEffectPool;

        protected BattleSpeedInterface battleTimeScale => staticBattleTimeScale;

        public Action OnDodge { get; set; }

        public Action<bool, float, bool> OnDamageForLoopTrigger { get; set; }

        public Action<float> OnDamageForLoopRepeat { get; set; }

        public Action<bool, float, bool> OnDamageForDivision { get; set; }

        public Action<bool> OnDamageForSpecialSleepRelease { get; set; }

        public Dictionary<int, Action<bool>> OnDamageListForChangeSpeedDisableByAttack { get; set; }
        public Dictionary<int, Action<bool>> OnDamageListForSpyDisableByAttack
        {
            get;
            set;
        } = new Dictionary<int, Action<bool>>();
        public Dictionary<int, Action<float>> OnRecoverListForChangeSpeedDisableByMaxHp
        {
            get;
            set;
        } = new Dictionary<int, Action<float>>();
        public Dictionary<eAbnormalState, Func<bool>> IsReleaseSlipDamageDic
        {
            get;
            set;
        } = new Dictionary<eAbnormalState, Func<bool>>();
        public OnDamageDelegate OnHpChangeForDamagedHP
        {
            get;
            set;
        }
        public Action OnDamageForUIShake { set; get; }

        public bool IsOnDamageCharge { get; set; }
        public Dictionary<int, float> PartsBreakTimeDictionary
        {
            get;
            set;
        } = new Dictionary<int, float>();
        public List<(long triggerHp, bool isActionDone, int actionId, Skill skill)> DamagedTriggerHpAndActionDoneList
        {
            get;
            set;
        } = new List<(long, bool, int, Skill)>();


        public int Index = -2;

        public int IdentifyNum { get; set; }

        public int UnitId { get; protected set; }

        public int CharacterUnitId { get; protected set; }
        public int OriginalUnitId
        {
            get;
            private set;
        }
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
        private List<int> levelFixedSpSkillIdList
        {
            get;
            set;
        } = new List<int>();
        public long StartMaxHP { get; private set; }

        public int StartAtk { get; protected set; }

        public int StartMagicStr { get; protected set; }

        public int StartDef { get; protected set; }

        public int StartMagicDef { get; protected set; }

        public int StartDodge { get; protected set; }

        public int StartAccuracy { get; protected set; }

        public int StartPhysicalCritical { get; protected set; }

        public int StartMagicCritical { get; protected set; }

        public int StartEnergyRecoveryRate { get; private set; }

        public int StartLifeSteal { get; private set; }

        public float StartMoveSpeed { get; private set; }

        public int StartWaveHpRecovery { get; protected set; }

        public int StartWaveEnergyRecovery { get; protected set; }

        public int StartEneryReduceRate { get; private set; }

        public int StartPhysicalPenetrate { get; private set; }

        public int StartMagicPenetrate { get; private set; }

        public int StartHpRecoveryRate { get; private set; }

        public int StartEnergyReduceRate { get; private set; }

        public int StartPhysicalCriticalDamageRate { get; private set; }

        public int StartMagicCriticalDamageRate { get; private set; }
        public int StartReceiveCriticalDamageRate
        {
            get;
            private set;
        }

        public int StartPhysicalAndMagicReceiveDamagePercent
        {
            get;
            set;
        }

        public int StartPhysicalReceiveDamagePercent
        {
            get;
            set;
        }

        public int StartMagicReceiveDamagePercent
        {
            get;
            set;
        }

        public int Level { get; protected set; }

        private FloatWithEx _hp  = 0f;
        private long _maxhp  = 0;

        public FloatWithEx Hp
        {
            get => _hp;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal protected set
            {
                if ((long)value > 0 ^ (long)_hp > 0)
                    battleManager.QueueUpdateSkillTarget();
                _hp = value;
                if (!battleManager.skipping)
                    OnLifeAmmountChange?.Invoke(LifeAmount);
            }
        }
        
        public long MaxHp 
        { 
            get => _maxhp; 
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal protected set
            {
                _maxhp = value;
                if (!battleManager.skipping)
                    OnMaxLifeAmmountChange?.Invoke(MaxLifeAmount);
            }
        }

        public FloatWithEx Atk { get; set; }

        public FloatWithEx MagicStr { get; set; }
        private FloatWithEx def;
        public FloatWithEx Def 
        {
            get { return def; }
            set
            {
                //MyOnBaseValueChanged?.Invoke(UnitId, 1, value, BattleHeaderController.CurrentFrameCount);
                def = value;
            }
        }
        private FloatWithEx magicDef;
        public FloatWithEx MagicDef 
        {
            get { return magicDef; }
            set
            {
                //MyOnBaseValueChanged?.Invoke(UnitId, 2, value, BattleHeaderController.CurrentFrameCount);
                magicDef = value;
            }
        }

        public int Dodge { get; set; }

        public int Accuracy { get; set; }

        public int PhysicalCritical { get; set; }

        public int MagicCritical { get; set; }

        public int WaveHpRecovery { get; protected set; }

        public int WaveEnergyRecovery { get; protected set; }

        public int EnergyRecoveryRate { get; set; }

        public int LifeSteal { get; set; }

        public int PhysicalPenetrate { get; private set; }

        public int MagicPenetrate { get; private set; }

        public int HpRecoveryRate { get; private set; }

        public int EnergyReduceRate { get; private set; }

        public int Rupee { get; set; }

        public int RewardCount { get; set; }

        public int PhysicalCriticalDamageRate { get; set; }

        public int MagicCriticalDamageRate { get; set; }
        public int ReceiveCriticalDamageRate
        {
            get;
            set;
        }

        public int AdditionalPhysicalAndMagicReceiveDamagePercent
        {
            get;
            set;
        }

        public int AdditionalPhysicalReceiveDamagePercent
        {
            get;
            set;
        }

        public int AdditionalMagicReceiveDamagePercent
        {
            get;
            set;
        }

        public long MaxHpAfterPassive
        {
            get;
            set;
        }
        public FloatWithEx AtkZero => (Atk.Max(0f) + getAdditionalBuffDictionary(BuffParamKind.ATK));

        public FloatWithEx MagicStrZero => (MagicStr.Max(0f) + getAdditionalBuffDictionary(BuffParamKind.MAGIC_STR));

        public FloatWithEx DefZero => (Def.Max(0) + getAdditionalBuffDictionary(BuffParamKind.DEF)).Floor();

        public FloatWithEx MagicDefZero => (MagicDef.Max(0) + getAdditionalBuffDictionary(BuffParamKind.MAGIC_DEF)).Floor();

        public int DodgeZero => (int)(Mathf.Max(0, Dodge) + getAdditionalBuffDictionary(BuffParamKind.DODGE));

        public int AccuracyZero => (int)(Mathf.Max(0, Accuracy) + getAdditionalBuffDictionary(BuffParamKind.ACCURACY));

        public int PhysicalCriticalZero => (int)(Mathf.Max(0, PhysicalCritical) + getAdditionalBuffDictionary(BuffParamKind.PHYSICAL_CRITICAL));

        public int MagicCriticalZero => (int)(Mathf.Max(0, MagicCritical) + getAdditionalBuffDictionary(BuffParamKind.MAGIC_CRITICAL));

        public int WaveHpRecoveryZero => Mathf.Max(0, WaveHpRecovery);

        public int WaveEnergyRecoveryZero => Mathf.Max(0, WaveEnergyRecovery);

        public int EnergyRecoveryRateZero
        {
            get
            {
                int num = (int)Mathf.Max(0, (int)EnergyRecoveryRate + getAdditionalBuffDictionary(BuffParamKind.ENERGY_RECOVER_RATE));
                return num;
            }
        }

        public int EnergyRecoveryRateZeroCapped
        {
            get
            {
                int num = EnergyRecoveryRateZero;
                return (MainManager.Instance.MaxTPUpValue <= 0) ? num : Mathf.Min(num, MainManager.Instance.MaxTPUpValue);
            }
        }

        public int LifeStealZero => (int)(Mathf.Max(0, LifeSteal) + getAdditionalBuffDictionary(BuffParamKind.LIFE_STEAL));

        public int PhysicalPenetrateZero => Mathf.Max(0, PhysicalPenetrate);

        public int MagicPenetrateZero => Mathf.Max(0, MagicPenetrate);

        public int HpRecoveryRateZero => Mathf.Max(0, HpRecoveryRate);

        public int EnergyReduceRateZero => Mathf.Max(0, EnergyReduceRate);

        public float MoveSpeedZero => Mathf.Max(0.0f, MoveSpeed) + (float)getAdditionalBuffDictionary(BuffParamKind.MOVE_SPEED);

        public int PhysicalCriticalDamageRateOrMin => (int)(Mathf.Max(50, PhysicalCriticalDamageRate) + getAdditionalBuffDictionary(BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE));

        public int MagicCriticalDamageRateOrMin => (int)(Mathf.Max(50, MagicCriticalDamageRate) + getAdditionalBuffDictionary(BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE));

        public int ReceiveCriticalDamageRateOrMin =>(int)(Mathf.Max(50, ReceiveCriticalDamageRate) + (float)getAdditionalBuffDictionary(BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE));

        public float PhysicalReceiveDamagePercentOrMin => Mathf.Max(0f, (float)((int)AdditionalPhysicalReceiveDamagePercent + (int)AdditionalPhysicalAndMagicReceiveDamagePercent) + 100f);

        public float MagicReceiveDamagePercentOrMin => Mathf.Max(0f, (float)((int)AdditionalMagicReceiveDamagePercent + (int)AdditionalPhysicalAndMagicReceiveDamagePercent) + 100f);

        public float StartHpPercent { get; private set; }

        public bool IsMoveSpeedForceZero { get; set; }

        //private SoundManager soundManager { get; set; }

        public BattleManager battleManager => staticBattleManager;
        public bool energyjudged = false;

        public FloatWithEx Energy
        {
            get => energy;
            private set
            {
                energyjudged = false;
                energy = value.Min(UnitDefine.MAX_ENERGY).Max(0f);
                if (!battleManager.skipping && unitUI != null)
                    unitUI.SetTP((float)energy / UnitDefine.MAX_ENERGY);
                //this.energy = (float)Mathf.Min((float)UnitDefine.MAX_ENERGY, Mathf.Max(0.0f, value));
                /*if (EnergyChange == null)
                    return;
                EnergyChange(this);*/
            }
        }

        internal FloatWithEx energy { get; set; }

        public bool HasSkillTarget => skillTargetList.Count > 0;

        public float EnegryAmount => Energy / 1000f;

        public float LifeAmount => (float)(long)Hp / (float)(long)MaxHpAfterPassive;
        public float MaxLifeAmount => (float)(long)MaxHp / (float)MaxHpAfterPassive;

        public Transform BottomTransform
        {
            get
            {
                if (bottomTransform == null)
                    bottomTransform = GetCurrentSpineCtrl() == null ? null : GetCurrentSpineCtrl().transform;
                return bottomTransform;
            }
        }

        public static void StaticRelease()
        {
            //UnitCtrl.staticSingletonTree = (Yggdrasil<UnitCtrl>)null;
            staticBattleManager = null;
            staticBattleLog = null;
            //UnitCtrl.staticBattleCameraEffect = (INADFELJLMH)null;
            //UnitCtrl.staticBattleEffectPool = (BattleEffectPoolInterface)null;
            staticBattleTimeScale = null;
        }

        private bool judgeRarity6() => Rarity == 6;

        public BattleSpineController GetCurrentSpineCtrl()
        {
            if (ToadDatas.Count > 0)
                return ToadDatas[0].BattleSpineController;
            return MotionPrefix != -1 ? UnitSpineCtrlModeChange : UnitSpineCtrl;
        }

        public bool GetSkill1Charging() => unitActionController.Skill1Charging;
    
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
            OnAwake();
            TimeToDie = false;
            ActionsTargetOnMe = new List<long>();
            FirearmCtrlsOnMe = new List<FirearmCtrl>();
            SummonUnitDictionary = new Dictionary<int, UnitCtrl>();
            skillTargetList = new List<UnitCtrl>();
            TargetEnemyList = new List<UnitCtrl>();
            targetPlayerList = new List<UnitCtrl>();
            OnDamageListForChangeSpeedDisableByAttack = new Dictionary<int, Action<bool>>();
            LifeStealQueueList = new List<Queue<int>>();
            StrikeBackDictionary = new Dictionary<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet>(new EnchantStrikeBackAction.eStrikeBackEffectType_DictComparer());
            AccumulativeDamageDataDictionary = new Dictionary<UnitCtrl, AccumulativeDamageData>();
            AccumulativeDamageDataForAllEnemyDictionary =
                new Dictionary<AccumulativeDamageSourceData, AccumulativeDamageData>();
            AccumulativeDamageSourceDataDictionary = new Dictionary<int, AccumulativeDamageSourceData>();
            DamageSealDataDictionary = new Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>();
            SealDictionary = new Dictionary<eStateIconType, SealData>();
            UbAbnormalDataList = new List<UbAbnormalData>();
            //UnitUnionBurstTimelineList = new List<UnitUnionBurstTimeline>();
            //this.CircleEffectList = new List<CircleEffectController>();
            SkillExecCountDictionary = new Dictionary<int, int>();
            //this.curColorChannel = new Dictionary<ChangeColorEffect, Color>();
            //this.curColorOffsetChannel = new Dictionary<ChangeColorEffect, Color>();
            curColor = Color.white;
            for (eAbnormalStateCategory key = eAbnormalStateCategory.NONE; key < eAbnormalStateCategory.NUM; ++key)
            {
                AbnormalStateCategoryData stateCategoryData = new AbnormalStateCategoryData();
                abnormalStateCategoryDataDictionary.Add(key, stateCategoryData);
            }
            for (eAbnormalState key = eAbnormalState.GUARD_ATK; key < eAbnormalState.NUM; ++key)
                m_abnormalState.Add(key, false);
            BossPartsListForBattle = new List<PartsData>();
        }

        protected override void DestructByOnDestroy()
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
            /*this.gameStartEffects = (List<PrefabWithTime>)null;
            this.dieEffects = (List<PrefabWithTime>)null;
            this.dieShakes = (List<ShakeEffect>)null;
            this.damageEffects = (List<PrefabWithTime>)null;
            this.damageShakes = (List<ShakeEffect>)null;
            this.SummonEffects = (List<PrefabWithTime>)null;
            this.idleEffects = (List<PrefabWithTime>)null;
            this.lifeGauge = (LifeGaugeController)null;*/
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
            AccumulativeDamageDataForAllEnemyDictionary = null;
            AccumulativeDamageSourceDataDictionary = null;
            this.DamageSealDataDictionary = (Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>)null;
            this.DamageOnceOwnerSealDateDictionary = (Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>)null;
            this.DamageOwnerSealDataDictionary = (Dictionary<UnitCtrl, Dictionary<int, AttackSealData>>)null;
            this.SealDictionary = (Dictionary<eStateIconType, SealData>)null;
            this.UbAbnormalDataList = (List<UbAbnormalData>)null;
            this.SkillExecCountDictionary = (Dictionary<int, int>)null;
            //this.OwnerPassiveAction = (Dictionary<eParamType, PassiveActionValue>)null;
            this.KillBonusTarget = (UnitCtrl)null;
            this.OnLifeAmmountChange = (System.Action<float>)null;
            this.OnMaxLifeAmmountChange = (System.Action<float>)null;
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
            passiveSealDictionary = null;
            debuffDamageUpCoroutine = null;
            debuffDamageUpDataList = null;
            debuffCounterDictionary = null;
            buffCounterDictionary = null;
        }

        public void DestroyAndCoroutineRemove()
        {
            battleManager.RemoveCoroutine(this);
            Destroy(gameObject);
        }

        private void instantiateResources(bool _isOther, bool _isGaugeAlwaysVisible)
        {
            return;
            //this.lifeGauge = ManagerSingleton<ResourceManager>.Instance.LoadImmediately(eResourceId.UNIT_LIFE_GAUGE).GetComponent<LifeGaugeController>();
            if (_isOther != !battleManager.IsDefenceReplayMode || IsBoss)
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

        public FixedTransform transformCache;
        public Transform parentCache;
        public float lossyx;
        public void Initialize(
          UnitParameter _data,
          PCRCaculator.UnitData unitData_my,
          bool _isOther,
          bool _isFirstWave,
          bool _isGaugeAlwaysVisible = false,
          BaseData additional = null,
          BaseData exOverride = null)
        {
            ToadDatas = new List<ToadData>();
            transformCache = transform;
            m00 = transformCache.TargetTransform.parent.localToWorldMatrix.m00;
            lossyx = transformCache.parent.lossyScale.x;
            unitParameter = _data;
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

                staticBattleManager = BattleManager.Instance;
                staticBattleLog = BattleLogIntreface.Instance;
                staticBattleTimeScale = battleManager.battleTimeScale;
            FunctionalComparer<BasePartsData>.CreateInstance();
            FunctionalComparer<BasePartsDataEx>.CreateInstance();

            OwnerPassiveAction = new Dictionary<eParamType, PassiveActionValue>(new eParamType_DictComparer());
            InitializeExAndFreeSkill(_data.UniqueData, ePassiveInitType.OWNER, 0, this);
            RepeatEffectList = new List<SkillEffectCtrl>();
            AuraEffectList = new List<SkillEffectCtrl>();
            CutInFrameSet = new CutInFrameData();
            idleEffectsObjs = new List<SkillEffectCtrl>();
            //Scale = Scale *50;//1.35f;
            //if (_data.UniqueData.Id >= 400000)
            Scale *= 0.5f;
            leftDirScale = new Vector3(-Scale, Mathf.Abs(Scale), 1f);
            rightDirScale = Vector3.Scale(leftDirScale, new Vector3(-1f, 1f, 1f));

            //this.leftDirScale = (Vector2)new Vector3(-this.Scale, Mathf.Abs(this.Scale), 1f);
            //this.leftDirScale = (Vector2)new Vector3(-1*scaleDir, 1, 1);
            //this.rightDirScale = (Vector2)Vector3.Scale((Vector3)this.leftDirScale, new Vector3(-1f, 1f, 1f));
            //if ((UnityEngine.Object)this.SeSource == (UnityEngine.Object)null)
            //    this.SeSource = this.gameObject.AddComponent<CriAtomSource>();
            //if ((UnityEngine.Object)this.VoiceSource == (UnityEngine.Object)null)
            //    this.VoiceSource = this.gameObject.AddComponent<CriAtomSource>();
            eBattleCategory jiliicmhlch = battleManager.BattleCategory;
            UnitParameter = _data;
            IsOther = _isOther;
            WeaponSeType = (SystemIdDefine.eWeaponSeType)(int)_data.MasterData.SeType;
            WeaponMotionType = (SystemIdDefine.eWeaponMotionType)(int)_data.MasterData.MotionType;
            UnitId = _data.UniqueData.Id;
            //OriginalUnitId = UnitUtility.GetOriginalUnitId(UnitId);
            CharacterUnitId = _data.MasterData.UnitId;
            SoundUnitId = _data.MasterData.PrefabId;
            //MasterEnemyEnableVoice.EnemyEnableVoice enemyEnableVoice = ManagerSingleton<MasterDataManager>.Instance.masterEnemyEnableVoice.Get(this.CharacterUnitId);
            //if (enemyEnableVoice != null)
            //    this.enemyVoiceId = (int)enemyEnableVoice.voice_id;
            //this.SDSkin = UnitUtility.GetSetSkinNo(_data.UniqueData, UnitDefine.eSkinType.Sd, this.IsOther == !this.battleManager.IsDefenceReplayMode);
            Rarity = _data.UniqueData.UnitRarity;
            BattleRarity = _data.UniqueData.BattleRarity;
            //this.IconSkin = UnitUtility.GetSetSkinNo(_data.UniqueData, UnitDefine.eSkinType.Icon);
            PromotionLevel = _data.UniqueData.PromotionLevel;
            PrefabId = _data.MasterData.PrefabId;
            IsBoss = UnitUtility.JudgeIsBoss(UnitId);
            IsClanBattleOrSekaiEnemy = _isOther && (jiliicmhlch == eBattleCategory.CLAN_BATTLE || jiliicmhlch == eBattleCategory.GLOBAL_RAID);
            if (jiliicmhlch == eBattleCategory.CLAN_BATTLE && !IsBoss)
                IsClanBattleOrSekaiEnemy = false;
            Level = _data.UniqueData.UnitLevel;
            AtkType = _data.MasterData.AtkType;
            MotionPrefix = -1;
            accumulateDamage = 0L;
            SearchAreaSize = _data.MasterData.SearchAreaWidth;
            StartSearchAreaSize = _data.MasterData.SearchAreaWidth;
            IsDead = false;
            float num = (float)(double)_data.MasterData.AtkCastTime;
            /*if (jiliicmhlch == eBattleCategory.CLAN_BATTLE && this.IsBoss)
            {
                ClanBattleTopInfo clanBattleTopInfo = Singleton<ClanBattleTempData>.Instance.ClanBattleTopInfo;
                MasterClanBattleParamAdjust.ClanBattleParamAdjust paramAdjustData = ClanBattleUtil.GetParamAdjustData(clanBattleTopInfo.ClanBattleId, clanBattleTopInfo.LapNum);
                if (paramAdjustData != null)
                    num = (float)((double)num * (double)paramAdjustData.NormalAtkCastTime / 1000.0);
            }*/
            m_fAtkRecastTime = num;
            //this.SpecialVoiceTimingData = BattleVoiceUtility.GetVoiceTimingData(this.UnitId);
            instantiateResources(_isOther, _isGaugeAlwaysVisible);
            StandByDone = IsSummonOrPhantom || (battleManager.BattleCategory == eBattleCategory.TUTORIAL || (battleManager.CurrentWave != 0 || !GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.STAND_BY)));
            TargetEnemyList.Clear();
            targetPlayerList.Clear();
            SetEnergy(0.0f, eSetEnergyType.INITIALIZE);
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
            UnitSpineCtrl.AnimationName = UnitSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE);
            UnitSpineCtrl.transform.localScale = rightDirScale;
            fixedCenterPos = Vector3.Scale(new Vector3(CenterBone.worldX, CenterBone.worldY, 0.0f), UnitSpineCtrl.transform.lossyScale);
            FixedStatePos = Vector3.Scale(new Vector3(StateBone.worldX, StateBone.worldY, 0.0f), UnitSpineCtrl.transform.lossyScale);

            /*BattleSpineController unitSpineCtrl = UnitSpineCtrl;
            int count = unitSpineCtrl.skeleton.Slots.FindAll((Slot e) => e.data.name.StartsWith("FX_")).Count;
            if (unitSpineCtrl.skeleton.FindSlot("FX_1") != null)
            {
                unitSpineCtrl.separatorSlots.Clear();
                for (int i = 0; i < count; i++)
                {
                    Slot slot = unitSpineCtrl.skeleton.FindSlot($"FX_{i + 1}");
                    if (slot != null)
                    {
                        unitSpineCtrl.separatorSlots.Add(slot);
                    }
                }
                SkeletonRenderSeparator = SkeletonRenderSeparator.AddToSkeletonRenderer(unitSpineCtrl);
                SkeletonRenderSeparator.enabled = false;
                SkeletonRenderSeparator.enabled = true;
            }*/

            DummyPartsData = new BasePartsData
            {
                PositionX = 0.0f,
                Owner = this
            };
            DummyPartsData.SetBattleManager(battleManager);
            DummyPartsData.SetAbnormalResistId(_data.UniqueData.ResistStatusId, _isInit: true);
            DummyPartsData.SetDebuffResistId(_data.UniqueData.ResistVariationId, _isInit: true);

            //DummyPartsData.InitializeResistStatus(_data.UniqueData.ResistStatusId);
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
            if (IsPartsBoss)
            {
                //MasterEnemyMParts.EnemyMParts _enemyMParts = ManagerSingleton<MasterDataManager>.Instance.masterEnemyMParts.Get(this.UnitId);
                MasterEnemyMParts.EnemyMParts _enemyMParts = MyGameCtrl.Instance.tempData.mParts;
                for (int index = 0; index < BossPartsList.Count; ++index)
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
                    PartsData bossParts = BossPartsList[index];
                    // BossPartsList是序列化值，BossPartsListForBattle是运行时值
                    // 这里没有对InitialPositionX进行初始化（与原值不同）
                    bossParts.Owner = this;
                    bossParts.InitialPositionX = bossParts.PositionX;
                    bossParts.Initialize(_enemyMParts);
                    bossParts.SetBattleManager(battleManager);
                    BossPartsListForBattle.Add(bossParts);
                }
                battleManager.LOGNEDLPEIJ = true;
                battleManager.KAOHIMNBPOK++;
                isNotPartsBossReady = true;
            }
            castTimeDictionary = new Dictionary<int, float>();
            attackPatternDictionary = new Dictionary<int, List<int>>();
            attackPatternLoopDictionary = new Dictionary<int, List<int>>();
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
                skillLevelInfo.SetSkillLevel(Level);
                skillLevelInfoList1.Add(skillLevelInfo);
            }
            //action(_data.SkillData.SpSkillIds, skillLevelInfoList1, eSpineCharacterAnimeId.SPECIAL_SKILL, true);
            //action(_data.SkillData.SpSkillEvolutionIds, skillLevelInfoList1, eSpineCharacterAnimeId.SPECIAL_SKILL, true);
            SetSkill(_data.SkillData.SpSkillIds, skillLevelInfoList1, eSpineCharacterAnimeId.SPECIAL_SKILL, true, _data);
            SetSkill(_data.SkillData.SpSkillEvolutionIds, skillLevelInfoList1, eSpineCharacterAnimeId.SPECIAL_SKILL, true, _data);
            List<SkillLevelInfo> skillLevelInfoList2 = new List<SkillLevelInfo>();
            for (int index = 0; index < _data.SkillData.SubUnionBurstIds.Count; ++index)
            {
                SkillLevelInfo skillLevelInfo = new SkillLevelInfo();
                skillLevelInfo.SetSkillId(_data.SkillData.SubUnionBurstIds[index]);
                skillLevelInfo.SetSkillLevel(Level);
                skillLevelInfoList2.Add(skillLevelInfo);
            }
            SetSkill(_data.SkillData.SubUnionBurstIds, skillLevelInfoList2, eSpineCharacterAnimeId.SUB_UNION_BURST, true, _data);
            MainSkillIdList = _data.SkillData.MainSkillIds;
            SpecialSkillIdList = _data.SkillData.SpSkillIds;
            SpecialSkillEvolutionIdList = _data.SkillData.SpSkillEvolutionIds;
            MainSkillEvolutionIdList = _data.SkillData.MainSkillEvolutionIds;
            SubUnionBurstIdList = _data.SkillData.SubUnionBurstIds;
            int burstEvolutionId = _data.SkillData.UnionBurstEvolutionIds[0];
            UnionBurstSkillId = burstEvolutionId == 0 || SkillLevels[burstEvolutionId] == 0 ? _data.SkillData.UnionBurstIds[0] : burstEvolutionId;
            if (UnionBurstSkillId != 0)
            {
                SkillData data = MainManager.Instance.SkillDataDic[UnionBurstSkillId];
                MasterSkillData.SkillData skillData = new MasterSkillData.SkillData(data);
                unionBurstSkillAreaWidth = skillData.skill_area_width;
            }

            int defaultActionPatternId = UnitUtility.GetDefaultActionPatternId(UnitIdForActionPattern);
            CreateAttackPattern(_data.SkillData, defaultActionPatternId);
            currentActionPatternId = defaultActionPatternId;

            //UnitData uniqueData = _data.UniqueData;
            BaseData baseData = new BaseData();
            BaseData baseDataEX = new BaseData();
            BaseData baseDataForEnergyCalc = new BaseData();
            var enemy = MyGameCtrl.Instance.tempData.guildEnemy?.FirstOrDefault(e => e.unit_id == this.UnitId);
            if (enemy != null)
            {
                baseDataForEnergyCalc = baseData = enemy.baseData;
            }
            else if (MainManager.Instance.unitDataDic.ContainsKey(UnitId))/* if (UnitId <= 200000 || UnitId >= 400000) */
            {
                baseData = MainManager.Instance.UnitRarityDic[UnitId].GetBaseData(unitData_my);//,MyGameCtrl.Instance.tempData.isGuildBattle);
                baseDataEX = MainManager.Instance.UnitRarityDic[UnitId].GetEXSkillValue(unitData_my);//,MyGameCtrl.Instance.tempData.isGuildBattle);
                baseDataForEnergyCalc = MainManager.Instance.UnitRarityDic[UnitId].GetBaseDataForEnergyCalc(unitData_my);
            }
            else
            {
                baseData = _data.EnemyData.baseData;
            }

            baseDataEX = exOverride ?? baseDataEX;

            if (additional != null) baseData += additional;


            //this.Def = this.StartDef = (int)Mathf.RoundToInt(baseData.Def);
            //this.Atk = this.StartAtk = (int)Mathf.RoundToInt(baseData.Atk);
            //this.MagicStr = this.StartMagicStr = (int)Mathf.RoundToInt(baseData.Magic_str);
            //this.MagicDef = this.StartMagicDef = (int)Mathf.RoundToInt(baseData.Magic_def);
            StartDef = Mathf.RoundToInt(baseData.Def);
            StartAtk = Mathf.RoundToInt(baseData.Atk);
            StartMagicStr = Mathf.RoundToInt(baseData.Magic_str);
            StartMagicDef = Mathf.RoundToInt(baseData.Magic_def);

            var baseHp = Mathf.RoundToInt(baseData.Hp);
            DefForDamagedEnergy = StartDef;
            MagicDefForDamagedEnergy = StartMagicDef;

            if (MainManager.Instance.PlayerSetting.tpCalculationChanged)
            {
                baseHp = baseDataForEnergyCalc.RealHp;
                DefForDamagedEnergy = (int)baseDataForEnergyCalc.Def;
                MagicDefForDamagedEnergy = (int)baseDataForEnergyCalc.Magic_def;
            }

            baseData += baseDataEX;

            Def = Mathf.RoundToInt(baseData.Def);
            Atk = Mathf.RoundToInt(baseData.Atk);
            MagicStr = Mathf.RoundToInt(baseData.Magic_str);
            MagicDef = Mathf.RoundToInt(baseData.Magic_def);

            if (UnitId >= 300000 && UnitId <= 399999)
            {
                var data = MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup();
                if (MyGameCtrl.Instance.tempData.isGuildEnemyViolent)
                {
                    MaxHp = StartMaxHP = MaxHpAfterPassive = (long)baseData.RealHp;
                    Hp = MaxHp / 2 - 1;
                }
                else if (MyGameCtrl.Instance.tempData.isGuildBattle && data.usePlayerSettingHP && data.playerSetingHP > 0)
                {
                    MaxHp = StartMaxHP = MaxHpAfterPassive = (long)baseData.RealHp;
                    Hp = data.playerSetingHP;
                }
                else
                    Hp = (long)(MaxHp = StartMaxHP = MaxHpAfterPassive = (long)baseData.RealHp);
                useLogBarrier = data.useLogBarrierNew;
            }
            else
                Hp = (long)(MaxHp = StartMaxHP = MaxHpAfterPassive = (long)baseData.RealHp);

            /*if (IsBoss && group.isSpecialBoss && (group.specialBossID == 666666 || group.specialBossID == 666667))
            {
                Hp = (long)(MaxHp = StartMaxHP = 99999999);
                Atk = 1;
                StartDef = Def = StartMagicDef = MagicDef = group.specialInputValue;
                Level = MainManager.Instance.PlayerSetting.playerLevel;
            }*/

            WaveHpRecovery = StartWaveHpRecovery = Mathf.RoundToInt(baseData.Wave_hp_recovery);
            WaveEnergyRecovery = StartWaveEnergyRecovery = Mathf.RoundToInt(baseData.Wave_energy_recovery);
            PhysicalCritical = StartPhysicalCritical = Mathf.RoundToInt(baseData.Physical_critical);
            MagicCritical = StartMagicCritical = Mathf.RoundToInt(baseData.Magic_critical);
            Dodge = StartDodge = Mathf.RoundToInt(baseData.Dodge);
            Accuracy = StartAccuracy = Mathf.RoundToInt(baseData.Accuracy);
            LifeSteal = StartLifeSteal = Mathf.RoundToInt(baseData.Life_steal);
            PhysicalPenetrate = StartPhysicalPenetrate = Mathf.RoundToInt(baseData.Physical_penetrate);
            MagicPenetrate = StartMagicPenetrate = Mathf.RoundToInt(baseData.Magic_penetrate);
            EnergyRecoveryRate = StartEnergyRecoveryRate = Mathf.RoundToInt(baseData.Energy_recovery_rate);
            HpRecoveryRate = StartHpRecoveryRate = Mathf.RoundToInt(baseData.Hp_recovery_rate);
            EnergyReduceRate = StartEnergyReduceRate = Mathf.RoundToInt(baseData.Enerey_reduce_rate);
            PhysicalCriticalDamageRate = StartPhysicalCriticalDamageRate = 100;
            MagicCriticalDamageRate = StartMagicCriticalDamageRate = 100;

            ReceiveCriticalDamageRate = (StartReceiveCriticalDamageRate = 100);
            AdditionalPhysicalAndMagicReceiveDamagePercent = (StartPhysicalAndMagicReceiveDamagePercent = 0);
            AdditionalPhysicalReceiveDamagePercent = (StartPhysicalReceiveDamagePercent = 0);
            AdditionalMagicReceiveDamagePercent = (StartMagicReceiveDamagePercent = 0);

            MoveSpeed = StartMoveSpeed = _data.MasterData.MoveSpeed;
            moveRate = IsMoveSpeedForceZero ? 0.0f : MoveSpeedZero * (_isOther ? -1f : 1f);
            BattleLogIntreface battleLog = this.battleLog;
            UnitCtrl unitCtrl = this;
            long hp = (long)Hp;
            UnitCtrl LIMEKPEENOB = unitCtrl;
            battleLog.AppendBattleLog(eBattleLogType.SET_DAMAGE, 3, 0L, hp, 0, 0, LIMEKPEENOB: LIMEKPEENOB);
            if (_data.EnemyData != null && _data.EnemyData.virtual_hp != 0)
                skillStackValDmg = 1000f / _data.EnemyData.virtual_hp;
            else
                skillStackValDmg = 1000f / baseHp;
            //卡rank相关tp获取惩罚……
            if (UnitId < 200000) // 看起来小精灵和分身不吃
            {
                int promotionLevel = unitData_my.rank;
                double num5 = BattleManager.CalcPlayerDamageTpReduceRate(promotionLevel);
                skillStackValDmg = (float)skillStackValDmg * (float)num5;
            }

            switch (UnitUtility.GetUnitPosType((int)_data.MasterData.SearchAreaWidth))
            {
                case eUnitBattlePos.FRONT:
                    skillStackValDmg = skillStackValDmg * battleManager.EnergyStackRatioDamagedFront;
                    skillStackVal = battleManager.EnergyGainValueSkillFront;
                    break;
                case eUnitBattlePos.MIDDLE:
                    skillStackValDmg = skillStackValDmg * battleManager.EnergyStackRatioDamagedMiddle;
                    skillStackVal = battleManager.EnergyGainValueSkillMiddle;
                    break;
                case eUnitBattlePos.BACK:
                    skillStackValDmg = skillStackValDmg * battleManager.EnergyStackRationDamageBack;
                    skillStackVal = battleManager.EnergyGainValueSkillBack;
                    break;
            }
            unitActionController = GetComponent<UnitActionController>();
            unitActionController.Initialize(this, _data);
            Slot slot = GetCurrentSpineCtrl().skeleton.FindSlot("Center");
            Skin skin = GetCurrentSpineCtrl().skeleton.Data.Skins.Items[0];
            List<string> names = new List<string>();
            int slotIndex = GetCurrentSpineCtrl().skeleton.FindSlotIndex("Center");
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
                    PolygonCollider2D polygonCollider2D = SkeletonUtility.AddBoundingBoxAsComponent(attachment, slot, gameObject, false);
                    ColliderCenter = (polygonCollider2D.bounds.center - transform.position) / 2;
                    ColliderSize = polygonCollider2D.bounds.size / 2;
                    float coSize2 = ColliderSize.x / transform.lossyScale.x;
                    BodyWidth = coSize2 + BossBodyWidthOffset;

                    Destroy(polygonCollider2D);
                    Destroy(gameObject.GetComponent<Rigidbody2D>());
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
            for (int index = 0; index < SortFrontDiappearAttachmentChangeDataList.Count; ++index)
            {
                AttachmentChangeData data = SortFrontDiappearAttachmentChangeDataList[index];
                data.TargetIndex = UnitSpineCtrl.skeleton.slots.FindIndex(e => e.data.Name == data.TargetAttachmentName);
                data.TargetAttachment = UnitSpineCtrl.skeleton.GetAttachment(data.TargetIndex, data.TargetAttachmentName);
                Attachment attachment = UnitSpineCtrl.skeleton.GetAttachment(UnitSpineCtrl.skeleton.slots.FindIndex(e => e.data.Name == data.AppliedAttachmentName), data.AppliedAttachmentName);
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
            princessFormProcessor = new PrincessFormProcessor();
            princessFormProcessor.Initialize(this, unitActionController, battleManager, battleTimeScale);
            /*if(!IsBoss && group.isSpecialBoss && group.specialBossID == 666667)
            {
                AppendCoroutine(TPRecovery(), ePauseType.SYSTEM);
            }*/
            List<int> list4 = new List<int>();
            for (int n = 0; n < unitData.exEquip.Length; n++)
            {
                int exEquipId = unitData.exEquip[n];
                if (exEquipId == 0)
                {
                    continue;
                }
                var exEquipmentData = MainManager.Instance.unitExEquips[unitData.unitId][n][exEquipId];
                if (exEquipmentData.passive_skill_id_1 != 0)
                {
                    list4.Add(exEquipmentData.passive_skill_id_1);
                }
                if (exEquipmentData.passive_skill_id_2 != 0)
                {
                    list4.Add(exEquipmentData.passive_skill_id_2);
                }
            }
            foreach (int item2 in list4)
            {
                ExSkillData exSkillData = new ExSkillData();
                exSkillData.Initialize(item2, this, unitActionController, battleManager);
                if (exSkillData.ExSkillConditionData.ContainsKey(eExSkillCondition.START))
                {
                    startExSkillList.Add(exSkillData);
                    continue;
                }
                foreach (KeyValuePair<eExSkillCondition, ExConditionPassiveData> exSkillConditionDatum in exSkillData.ExSkillConditionData)
                {
                    if (conditionExSkillDictionary.TryGetValue(exSkillConditionDatum.Key, out var value))
                    {
                        value.Add(exSkillData);
                        continue;
                    }
                    conditionExSkillDictionary.Add(exSkillConditionDatum.Key, new List<ExSkillData> { exSkillData });
                }
            }

        }

        public int UnitIdForActionPattern
        {
            get
            {
                var uid = UnitId;
                if (UnitId == 105701 && Rarity == 6) uid = 170301;
                return uid;
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
            UnitInstanceId = battleManager.UnitInstanceIdCount++;
            unitActionController.ExecActionOnStart();
        }

        public void ExecActionOnWaveStart() => unitActionController.ExecActionOnWaveStart();

        public float GetDodgeRate(int _accuracy)
        {
            int num = Mathf.Max(DodgeZero - _accuracy, 0);
            return num / (num + 100f);
        }

        public void CreateAttackPattern(
          MasterUnitSkillData.UnitSkillData _skillData,
          int _attackPatternId)
        {
            if (attackPatternDictionary.ContainsKey(_attackPatternId))
                return;
            /*if(_attackPatternId!= UnitId * 100 + 1)
            {
                Debug.LogError("特殊攻击模式数据丢失！");
            }
            PCRCaculator.UnitSkillData data = PCRCaculator.MainManager.Instance.UnitRarityDic[UnitId].skillData;
            MasterUnitAttackPattern.UnitAttackPattern unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern(UnitId,data);*/
            MasterUnitAttackPattern.UnitAttackPattern unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern();
            if (MainManager.Instance.AllUnitAttackPatternDic.TryGetValue(_attackPatternId,out UnitAttackPattern data))
            {
                unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern(data);
                //if(IsBoss&&group.isSpecialBoss && (group.specialBossID == 666666 || group.specialBossID == 666667))
                //{
                //    unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern(loop_start: 1, loop_end: 1, atk_pattern_1: 1001);
                //}
                if(MainManager.Instance.IsGuildBattle && MainManager.Instance.GuildBattleData.SettingData.changedEnemyAttackPatternDic.TryGetValue(_attackPatternId,out UnitAttackPattern data2))
                {
                    unitAttackPattern = new MasterUnitAttackPattern.UnitAttackPattern(data2);
                }
            }
            else
            {
                Debug.LogError("特殊攻击模式数据丢失！");
            }
            List<int> intList1 = new List<int>();
            for (int index1 = 0; index1 < unitAttackPattern.loop_start - 1; ++index1)
            {
                int pattern = unitAttackPattern.PatternList[index1];
                switch (pattern)
                {
                    case 0:
                        continue;
                    case 1:
                        intList1.Add(1);
                        AttackWhenSilence = true;
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
                                if (SkillLevels[mainSkillId] == 0 && (key1 == 0 || SkillLevels[key1] == 0))
                                {
                                    intList1.Add(1);
                                    continue;
                                }
                                if (key1 != 0 && SkillLevels[key1] != 0)
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
                                if (num != 0 && key2 != 0 && SkillLevels[key2] != 0)
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
            attackPatternDictionary.Add(_attackPatternId, intList1);
            List<int> intList2 = new List<int>();
            if (unitAttackPattern.loop_start > 0)
            {
                for (int index1 = unitAttackPattern.loop_start - 1; index1 < unitAttackPattern.loop_end; ++index1)
                {
                    int pattern = unitAttackPattern.PatternList[index1];
                    switch (pattern)
                    {
                        case 0:
                            goto label_45;
                        case 1:
                            intList2.Add(1);
                            AttackWhenSilence = true;
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
                                    if (SkillLevels[mainSkillId] == 0 && (key1 == 0 || SkillLevels[key1] == 0))
                                    {
                                        intList2.Add(1);
                                        continue;
                                    }
                                    if (key1 != 0 && SkillLevels[key1] != 0)
                                    {
                                        if (index2 == 0)
                                            MainSkill1Evolved = true;
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
                                    if (num != 0 && key2 != 0 && SkillLevels[key2] != 0)
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
            attackPatternLoopDictionary.Add(_attackPatternId, intList2);
        }

        public void BattleStartProcess(eUnitRespawnPos respawnPos)
        {
            RespawnPos = respawnPos;
            AppendCoroutine(updateAttackTarget(), ePauseType.SYSTEM, this);
            IsDepthBack = IsGameStartDepthBack;
            if (battleManager.BlackOutUnitList.Contains(this))
                SetSortOrderFront();
            else
                SetSortOrderBack();
        }

        public void WaveStartProcess(bool _first)
        {
            ApplyPassiveSkillValue(_first);
            MaxHpAfterPassive = MaxHp;
            resetActionPatternAndCastTime();
            if (IsOther)
                resetPosForEnemyUnit(RespawnPos);
            else
                ResetPosForUserUnit(BattleDefine.UnitRespawnPosList.IndexOf(RespawnPos));
            //this.CreateRunSmoke();
            foreach (KeyValuePair<eExSkillCondition, List<ExSkillData>> item in conditionExSkillDictionary)
            {
                foreach (ExSkillData item2 in item.Value)
                {
                    item2.ResetLimitNum(this);
                }
            }
            ExecActionOnWaveStart();
        }

        /*public void ActivateInternalUnit()
        {
            if (IsDead || gameObject.activeSelf)
                return;
            gameObject.SetActive(true);
            MoveToNext();
        }*/

        private void resetActionPatternAndCastTime()
        {
            currentActionPatternId = UnitUtility.GetDefaultActionPatternId(this.UnitIdForActionPattern);
            attackPatternIndex = 0;
            attackPatternIsLoop = attackPatternDictionary[currentActionPatternId].Count == 0;
            switch (battleManager.BattleCategory)
            {
                case eBattleCategory.STORY:
                    m_fCastTimer = 90f;
                    break;
                case eBattleCategory.TUTORIAL:
                    m_fCastTimer = 0.3f;
                    break;
                default:
                    m_fCastTimer = battleManager.CurrentWave != 0 ? 0.3f : 2.5f;
                    /*
                    //XX: force make up for part boss additional 1 frame
                    if (battleManager.BossUnit.IsPartsBoss)
                    {
                        m_fCastTimer += battleManager.DeltaTime_60fps;
                    }*/
                    break;
            }
        }

        public void ResetPosForUserUnit(int index)
        {
            Vector2 localPosition = transform.localPosition;
            localPosition.x = -560f;
            if (battleManager.BattleCategory == eBattleCategory.TUTORIAL && battleManager.CurrentWave == 0)
            {
                //this.IdleOnly = true;
                //if (this.battleManager.BattleCategory == eBattleCategory.TUTORIAL)
                //    localPosition.x = TutorialDefine.UNIT_DEFAULT_POS[this.SoundUnitId];
            }
            else
                localPosition.x -= 200f * (index + 1);
            if (battleManager.IsBossBattle)
                localPosition.x -= (float)(MoveSpeed * (double)battleManager.GetBossUnit().UnitAppearDelay * 1.6);
            transform.localPosition = localPosition;
        }

        private void resetPosForEnemyUnit(eUnitRespawnPos pos)
        {
            int num = BattleDefine.UnitRespawnPosList.IndexOf(pos);
            eBattleCategory jiliicmhlch = battleManager.BattleCategory;
            Vector3 vector3 = new Vector3(560f, battleManager.GetRespawnPos(pos), 0.0f);
            if (IsBoss)
            {
                vector3.y = battleManager.GetRespawnPos(eUnitRespawnPos.MAIN_POS_5);
                vector3.x += BossDeltaX * 540f;
                vector3.y += BossDeltaY * 540f;
                vector3.x -= -34f;
            }
            else if (jiliicmhlch == eBattleCategory.TUTORIAL && battleManager.CurrentWave == 0)
            {
                //vector3.x = TutorialDefine.HIKARITAKE_POS;
            }
            else
            {
                vector3.x += 200f * num;
                if (battleManager.IsBossBattle)
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
            transform.localPosition = vector3;
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

        /*private IEnumerator waitBossMotionEnd()
        {
            while (!battleManager.GetBossUnit().GameStartDone)
                yield return null;
            IsMoveSpeedForceZero = false;
            SetLeftDirection(IsOther);
            SetState(ActionState.IDLE);
        }*/

        public void SetOverlapPos(float overlapPosX)
        {
            Vector2 localPosition1 = BottomTransform.transform.localPosition;
            localPosition1.x += overlapPosX;
            BottomTransform.transform.localPosition = localPosition1;
            OverlapPosX = overlapPosX;
            //Vector2 localPosition2 = (Vector2)this.lifeGauge.transform.localPosition;
           // localPosition2.x += overlapPosX;
            //this.lifeGauge.transform.localPosition = (Vector3)localPosition2;
        }

        public void UpdateSkillTarget()
        {
            var distance = SkillAreaWidthList[UnionBurstSkillId];

            if (distance == lastSearchDistanceForSkillReady &&
                !battleManager.PositionChanged(ref skillTargetCacheKeyForSkillReady))
            {
                return;
            }

            if (distance == lastSearchDistance && !battleManager.PositionChanged(skillTargetCacheKey))
            {
                skillTargetList = (IsOther ? targetPlayerList : TargetEnemyList).ToList();
                lastSearchDistanceForSkillReady = distance;
                return;
            }

            List<UnitCtrl> unitCtrlList = IsOther ? battleManager.UnitList : battleManager.EnemyList;
            skillTargetList.Clear();
            foreach (var unit in unitCtrlList)
            {
                if (judgeFrontAreaTarget(unit, distance))
                    skillTargetList.Add(unit);
            }

            lastSearchDistanceForSkillReady = distance;
        }

        public Vector3 positionCached => parentCache.TransformPoint(transformCache.localPosition);
        public float m00;

        /* hotspot */
        private bool judgeFrontAreaTarget(UnitCtrl _target, float _distance)
        {
            if (_target.IsPartsBoss)
                return judgeFrontAreaTargetForBossParts(_target, _distance);
            double x = lossyx;
            float _a = (float)((_target.transformCache.positionX - transformCache.positionX) / FixedTransform.DIGID * m00 / x);
            var num = (_target.BodyWidth + BodyWidth) * 0.5;
            /*
            if (this.UnitId == 209000 && _target.UnitId == 180501)
            {
                File.AppendAllText(@"D:\rnd.log", $"[{BattleHeaderController.CurrentFrameCount}] " +
                                                  $"target={_target.transformCache.positionX / FixedTransform.DIGID * m00 / x}, " +
                                                  $"source={transformCache.positionX / FixedTransform.DIGID * m00 / x}, " +
                                                  $"distance={_a}\n");
            }*/
            if (IsLeftDir)
            {
                var _b1 = (float)(-_distance - num);
                var _b2 = (float)(num);
                return (_a >= _b1 && _a <= _b2 || (BattleUtil.Approximately(_a, _b1) || BattleUtil.Approximately(_a, _b2))) &&
                    (!_target.IsDead && (long)_target.Hp > 0L || _target.HasUnDeadTime) && (!_target.IsPhantom && !_target.IsStealth);
            }
            else
            {
                var _b1 = (float)(- num);
                var _b2 = (float)( _distance + num);
                return (_a >= _b1 && _a <= _b2 || (BattleUtil.Approximately(_a, _b1) || BattleUtil.Approximately(_a, _b2))) &&
                    (!_target.IsDead && (long)_target.Hp > 0L || _target.HasUnDeadTime) && (!_target.IsPhantom && !_target.IsStealth);
            }
        }

        private bool judgeFrontAreaTargetForBossParts(UnitCtrl _target, float _distance)
        {
            for (int index = 0; index < _target.BossPartsListForBattle.Count; ++index)
            {
                PartsData partsData = _target.BossPartsListForBattle[index];
                float _a = _target.transform.localPosition.x + partsData.PositionX - transform.localPosition.x;
                float bodyWidthValue = partsData.BodyWidthValue;
                if (partsData.GetTargetable())
                {
                    float num = bodyWidthValue + BodyWidth;
                    float _b1 = (float)((IsLeftDir ? -(double)_distance : 0.0) - num * 0.5);
                    float _b2 = (float)((IsLeftDir ? 0.0 : _distance) + num * 0.5);
                    if ((_a >= (double)_b1 && _a <= (double)_b2 || (BattleUtil.Approximately(_a, _b1) || BattleUtil.Approximately(_a, _b2))) && (!_target.IsDead && (long)_target.Hp > 0L || _target.HasUnDeadTime) && !_target.IsPhantom)
                        return true;
                }
            }
            return false;
        }

        private IEnumerator updateAttackTarget()
        {
            while (true)
            {
                updateAttackTargetImpl();
                yield return null;
            }
        }

        private float lastSearchDistance = -1f;
        private float lastSearchDistanceForSkillReady = -1f;

        private void updateAttackTargetImpl()
        {
            List<UnitCtrl> unitCtrlList1 = IsOther ? battleManager.UnitList : battleManager.EnemyList;
            float _distance = SearchAreaSize;
            if (!attackPatternIsLoop && attackPatternIndex == 0 && (attackPatternDictionary != null && attackPatternDictionary[currentActionPatternId].Count != 0))
            {
                int key = attackPatternDictionary[currentActionPatternId][attackPatternIndex];
                switch (key)
                {
                    case 0:
                    case 1:
                        break;
                    default:
                        _distance = SkillAreaWidthList[key];
                        break;
                }
            }

            //var test = false;

            if (_distance == lastSearchDistance && !battleManager.PositionChanged(ref skillTargetCacheKey))
                return;
                //test = true;
            /*
            var t1 = TargetEnemyList.ToList();
            var t2 = targetPlayerList.ToList();
            */
            TargetEnemyList.Clear();
            targetPlayerList.Clear();

            List<UnitCtrl> list1, list2;

            if (IsOther)
            {
                list1 = TargetEnemyList;
                list2 = targetPlayerList;
            }
            else
            {
                list1 = targetPlayerList;
                list2 = TargetEnemyList;
            }

            foreach (var ctrl in battleManager.UnitList)
                if (judgeFrontAreaTarget(ctrl, _distance))
                    list1.Add(ctrl);
            foreach (var ctrl in battleManager.EnemyList)
                if (judgeFrontAreaTarget(ctrl, _distance))
                    list2.Add(ctrl);
            /*
            if (test && (!t1.SequenceEqual(TargetEnemyList) || !t2.SequenceEqual(targetPlayerList)))
                Debugger.Break();
            */
            lastSearchDistance = _distance;

            /*
            for (int index = 0; index < unitCtrlList1.Count; ++index)
            {
                UnitCtrl _target = unitCtrlList1[index];
                if (judgeFrontAreaTarget(_target, _distance))
                {
                    if (!TargetEnemyList.Contains(_target))
                        TargetEnemyList.Add(_target);
                }
                else
                    TargetEnemyList.Remove(_target);
            }
            for (int index = TargetEnemyList.Count - 1; index >= 0; --index)
            {
                UnitCtrl targetEnemy = TargetEnemyList[index];
                if (!unitCtrlList1.Contains(targetEnemy))
                    TargetEnemyList.Remove(targetEnemy);
            }
            List<UnitCtrl> unitCtrlList2 = IsOther ? battleManager.EnemyList : battleManager.UnitList;
            for (int index = 0; index < unitCtrlList2.Count; ++index)
            {
                UnitCtrl _target = unitCtrlList2[index];
                if (!(_target == null))
                {
                    if (judgeFrontAreaTarget(_target, _distance))
                    {
                        if (!targetPlayerList.Contains(_target))
                            targetPlayerList.Add(_target);
                    }
                    else
                        targetPlayerList.Remove(_target);
                }
            }
            for (int index = targetPlayerList.Count - 1; index >= 0; --index)
            {
                UnitCtrl targetPlayer = targetPlayerList[index];
                if (!unitCtrlList2.Contains(targetPlayer))
                    targetPlayerList.Remove(targetPlayer);
            }*/
        }

        // Token: 0x06003957 RID: 14679 RVA: 0x000EB21C File Offset: 0x000EB21C
        public int CompareEnergyAscSameLeft(BasePartsData _a, BasePartsData _b)
        {
            if (BattleUtil.Approximately(_a.Owner.Energy, _b.Owner.Energy))
            {
                return this.CompareLeft(_a, _b);
            }
            return UnitCtrl.CompareEnergyAsc(_a, _b);
        }

        // Token: 0x06003958 RID: 14680 RVA: 0x000EB24C File Offset: 0x000EB24C
        public int CompareEnergyAscSameRight(BasePartsData _a, BasePartsData _b)
        {
            if (BattleUtil.Approximately(_a.Owner.Energy, _b.Owner.Energy))
            {
                return this.CompareRight(_a, _b);
            }
            return UnitCtrl.CompareEnergyAsc(_a, _b);
        }
        // Token: 0x0600396B RID: 14699 RVA: 0x000EB7EC File Offset: 0x000EB7EC
        public int CompareAtkDecSameLeft(BasePartsData _a, BasePartsData _b)
        {
            int num = UnitCtrl.CompareAtkDec(_a, _b);
            if (num == 0)
            {
                return this.CompareLeft(_a, _b);
            }
            return num;
        }

        // Token: 0x0600396C RID: 14700 RVA: 0x000EB810 File Offset: 0x000EB810
        public int CompareAtkDecSameRight(BasePartsData _a, BasePartsData _b)
        {
            int num = UnitCtrl.CompareAtkDec(_a, _b);
            if (num == 0)
            {
                return this.CompareRight(_a, _b);
            }
            return num;
        }

        public void _Update()
        {
            if (TimeToDie)
            {
                DestroyAndCoroutineRemove();
            }
            if (UnionBurstCoolDownTime > 0f && battleManager.GetBlackOutUnitLength() == 0)
            {
                UnionBurstCoolDownTime -= battleManager.DeltaTime_60fps;
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

       /* public IEnumerator WaitBattleRecovery()
        {
            UnitCtrl _source = this;
            if (_source.IsDead)
            {
                --_source.battleManager.KPLMNGFMBKF;
            }
            else
            {
                bool flag = (long)_source.Hp < _source.MaxHp && _source.WaveHpRecovery > 0;
                if (flag)
                    _source.SetRecovery((int)_source.WaveHpRecoveryZero, eInhibitHealType.NO_EFFECT, _source);
                if ((double)_source.Energy < UnitDefine.MAX_ENERGY && _source.WaveEnergyRecovery > 0)
                {
                    if (flag)
                        yield return new WaitForSeconds(0.45f);
                    _source.ChargeEnergy(eSetEnergyType.BATTLE_RECOVERY, (float)(int)_source.WaveEnergyRecoveryZero, true);
                }
                --_source.battleManager.KPLMNGFMBKF;
            }
        }*/

       /* public void MoveToNext()
        {
            if (IsDead)
                return;
            if (!UnitUtility.JudgeIsSummon(this.UnitId))
            {
                this.IdleOnly = false;
                this.GetCurrentSpineCtrl().CurColor = Color.white;
            }
            ModeChangeEnd = false;
            SetState(ActionState.WALK);
            SetLeftDirection(false);
            //this.CreateRunSmoke();
			startSkillExeced = false;
        }*/

        /*private IEnumerator waitCargeEnergy(float _recoveryRate)
        {
            yield return new WaitForSeconds(0.45f);
            ChargeEnergy(eSetEnergyType.BATTLE_RECOVERY, WaveEnergyRecoveryZero * _recoveryRate, true);
        }*/

        public void SetLeftDirection(bool bLeftDir)
        {
            IsLeftDir = bLeftDir;
            battleManager.QueueUpdateSkillTarget();
            if (ToadDatas.Count > 0)
                GetCurrentSpineCtrl().transform.localScale = IsLeftDir || IsForceLeftDir ? ToadDatas[0].LeftDirScale : ToadDatas[0].RightDirScale;
            else
                GetCurrentSpineCtrl().transform.localScale = IsLeftDir || IsForceLeftDirOrPartsBoss ? leftDirScale : rightDirScale;
            moveRate = IsMoveSpeedForceZero ? 0.0f : MoveSpeedZero * (IsLeftDir ? -1f : 1f);
            foreach (KeyValuePair<eAbnormalStateCategory, AbnormalStateCategoryData> stateCategoryData in abnormalStateCategoryDataDictionary)
            {
                bool flag = bLeftDir || IsForceLeftDirOrPartsBoss;
                for (int index = 0; index < stateCategoryData.Value.Effects.Count; ++index)
                {
                    AbnormalStateEffectGameObject effect = stateCategoryData.Value.Effects[index];
                    if (effect.LeftEffect != null)
                    {
                        effect.LeftEffect.SetActive(flag && isAbnormalEffectEnable);
                        if (effect.RightEffect != null)
                            effect.RightEffect.SetActive(!flag && isAbnormalEffectEnable);
                    }
                }
            }
        }

        public void SetDirectionAuto() => SetLeftDirection(isNearestEnemyLeft());

        private bool isNearestEnemyLeft()
        {
            List<UnitCtrl> unitCtrlList = !IsConfusionOrConvert() ? (IsOther ? battleManager.UnitList : battleManager.EnemyList) : (!IsOther ? battleManager.UnitList : battleManager.EnemyList);
            float f = (float)(BattleDefine.BATTLE_FIELD_SIZE * 2.0 * (!IsOther ? -1.0 : 1.0));
            for (int index1 = 0; index1 < unitCtrlList.Count; ++index1)
            {
                UnitCtrl unitCtrl = unitCtrlList[index1];
                if (!unitCtrl.IsPhantom && !unitCtrl.IsDead && (!(unitCtrl == this) && (long)unitCtrl.Hp != 0L) && !unitCtrl.IsStealth)
                {
                    if (!unitCtrl.IsPartsBoss)
                    {
                        if (Mathf.Abs(f) > (double)Mathf.Abs(transform.localPosition.x - unitCtrl.transform.localPosition.x) || BattleUtil.Approximately(Mathf.Abs(f), Mathf.Abs(transform.localPosition.x - unitCtrl.transform.localPosition.x)))
                            f = transform.localPosition.x - unitCtrl.transform.localPosition.x;
                    }
                    else
                    {
                        for (int index2 = 0; index2 < unitCtrl.BossPartsListForBattle.Count; ++index2)
                        {
                            PartsData partsData = unitCtrl.BossPartsListForBattle[index2];
                            if (Mathf.Abs(f) > (double)Mathf.Abs(transform.localPosition.x - unitCtrl.transform.localPosition.x - partsData.PositionX) || BattleUtil.Approximately(Mathf.Abs(f), Mathf.Abs(transform.localPosition.x - unitCtrl.transform.localPosition.x - partsData.PositionX)))
                                f = transform.localPosition.x - unitCtrl.transform.localPosition.x - partsData.PositionX;
                        }
                    }
                }
            }
            return f > 0.0;
        }

        public bool Pause
        {
            get => m_bPause;
            set
            {
                m_bPause = value;
                if (m_bPause)
                    setMotionPause();
                else if (!IsUnableActionState() || specialSleepStatus != eSpecialSleepStatus.INVALID)
                    setMotionResume();
                //this.PauseSound(this.m_bPause);
                princessFormProcessor.Pause(m_bPause);
            }
        }

        private void setMotionResume()
        {
            if (!(GetCurrentSpineCtrl() != null))
                return;
            GetCurrentSpineCtrl().Resume();
        }

        private void setMotionPause()
        {
            if (battleManager == null || battleManager.BlackoutUnitTargetList == null || (battleManager.BlackoutUnitTargetList.Contains(this) || !(GetCurrentSpineCtrl() != null)))
                return;
            GetCurrentSpineCtrl().Pause();
        }

        public void ChangeAttackPattern(int _attackPatternId, int _spSkillLevel, float _limitTime = -1f)
        {
            // Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] attack pattern changed to {_attackPatternId}");
            int currentActionPatternId = this.currentActionPatternId;
            this.currentActionPatternId = _attackPatternId;
            attackPatternIndex = 0;
            attackPatternIsLoop = attackPatternDictionary[_attackPatternId].Count == 0;
            for (int index = 0; index < unitActionController.SpecialSkillList.Count; ++index)
                unitActionController.SpecialSkillList[index].SetLevel(_spSkillLevel);
            foreach (Skill specialSkillEvolution in unitActionController.SpecialSkillEvolutionList)
                specialSkillEvolution.SetLevel(_spSkillLevel);
            if (_limitTime <= 0.0)
                return;
            AppendCoroutine(updateChangeActionPattern(currentActionPatternId, _limitTime), ePauseType.SYSTEM, this);
        }

        private IEnumerator updateChangeActionPattern(int oldIndex, float limitTime)
        {
            float time = 0.0f;
            while (true)
            {
                time += DeltaTimeForPause;
                if (time <= (double)limitTime)
                    yield return null;
                else
                    break;
            }
            currentActionPatternId = oldIndex;
            attackPatternIndex = 0;
        }

        public void ChangeChargeSkill(int skillNum, float limitTime)
        {
            // Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] union burst changed to {skillNum}");
            isAwakeMotion = true;
            int unionBurstSkillId = UnionBurstSkillId;
            UnionBurstSkillId = skillNum;
            if (limitTime > 0f)
                AppendCoroutine(updateChangeSkillNum(unionBurstSkillId, limitTime), ePauseType.SYSTEM, this);
        }

        private IEnumerator updateChangeSkillNum(int oldChargeSkillNum, float limitTime)
        {
            float time = 0.0f;
            while (true)
            {
                time += DeltaTimeForPause;
                if (time > (double) limitTime)
                {
                    // Debug.Log($"[{BattleHeaderController.CurrentFrameCount}] union burst changed to {UnionBurstSkillId}");
                    UnionBurstSkillId = oldChargeSkillNum;
                    // NOTE: there is a critical bug here, if the update change skill is called, the union burst will be forced
                    // to the old charge skill
                }
                yield return null;
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
            string currentMotionName = GetCurrentSpineCtrl().AnimationName;
            while (true)
            {
                /*if (!battleManager.CoroutineManager.VisualPause && GetCurrentSpineCtrl().state.TimeScale == 0f)
                {
                    GetCurrentSpineCtrl().Resume();
                }
                if (IsUnableActionState() || !GetCurrentSpineCtrl().IsPlayAnimeBattle)
                {
                    break;
                }
                yield return null;*/
                if (!battleManager.CoroutineManager.VisualPause && GetCurrentSpineCtrl().state.TimeScale == 0f)
                {
                    GetCurrentSpineCtrl().Resume();
                }
                if (IsUnableActionState())
                {
                    break;
                }
                if (GetCurrentSpineCtrl().AnimationName != currentMotionName && setStateCalled)
                {
                    BattleStartProcess(_respawnPos);
                    yield break;
                }
                if (!GetCurrentSpineCtrl().IsPlayAnimeBattle)
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
                    unitCtrl.SetState(ActionState.WALK);
                    /*using (List<SkillEffectCtrl>.Enumerator enumerator = unitCtrl.RepeatEffectList.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        { }   //enumerator.Current.SetSortOrderBack();
                        break;
                    }*/
                    break;
                case SummonAction.eMoveType.LINEAR:
                    if (unitCtrl.GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.SUMMON, _skillNum, 1))
                        unitCtrl.PlayAnime(eSpineCharacterAnimeId.SUMMON, _skillNum, 1);
                    _targetPosition.y = unitCtrl.battleManager.GetRespawnPos(_respawnPos);
                    v = (_targetPosition - unitCtrl.transform.localPosition).normalized * _moveSpeed;
                    unitCtrl.SetLeftDirection(v.x < 0.0);
                    float duration = (_targetPosition - unitCtrl.transform.localPosition).x / v.x;
                    float time = 0.0f;
                    while (true)
                    {
                        time += unitCtrl.battleManager.DeltaTime_60fps;
                        unitCtrl.transform.localPosition += v * unitCtrl.battleManager.DeltaTime_60fps;
                        if (time <= (double)duration)
                            yield return null;
                        else
                            break;
                    }
                    if (unitCtrl.GetCurrentSpineCtrl().IsAnimation(eSpineCharacterAnimeId.SUMMON, _skillNum, 2))
                        unitCtrl.PlayAnime(eSpineCharacterAnimeId.SUMMON, _skillNum, 2, _isLoop: false);
                    while (true)
                    {
                        if (!unitCtrl.battleManager.CoroutineManager.VisualPause && unitCtrl.GetCurrentSpineCtrl().state.TimeScale == 0.0)
                            unitCtrl.GetCurrentSpineCtrl().Resume();
                        if (unitCtrl.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                            yield return null;
                        else
                            break;
                    }
                    unitCtrl.BattleStartProcess(_respawnPos);
                    unitCtrl.transform.localPosition = _targetPosition;
                    unitCtrl.SetState(ActionState.WALK);
                    yield break;
            }
            //v = new Vector3();
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
            if (controller == null)
                controller = GetCurrentSpineCtrl();
            controller.PlayAnime(_animeId, _index1, _index2, _index3, _isLoop, _startTime, _ignoreBlackout);
            controller.state.GetCurrent(0).lastTime = _startTime;
            controller.state.GetCurrent(0).time = _startTime;
            //controller.state.GetCurrent(0).animationLast = _startTime;
            //controller.state.GetCurrent(0).animationStart = _startTime;

            //controller.state.Apply(controller.skeleton);
            //if (_quiet)
            //    return;
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
          int _prefix) => GetCurrentSpineCtrl().RestartPlayAnimeCoroutine(_startTime, _animeId, _index, _prefix);

        public void PlayAnimeNoOverlap(
          eSpineCharacterAnimeId _animeId,
          int _index1 = -1,
          int _index2 = -1,
          int _index3 = -1,
          bool _isLoop = false,
          BattleSpineController _targetCtr = null)
        {
            BattleSpineController controller = _targetCtr;
            if (controller == null)
                controller = GetCurrentSpineCtrl();
            controller.PlayAnimeNoOverlap(_animeId, _index1, _index2, _index3, _isLoop);
            //this.playSeWithMotion(controller, _animeId, _index1, _index2, _index3, _isLoop);
        }

        public void AppendCoroutine(IEnumerator _cr, ePauseType _pauseType, UnitCtrl _unit = null) => battleManager.AppendCoroutine(_cr, _pauseType, _unit);

        public void SetCurrentHp(long _hp)
        {
            Hp = _hp;
            if (_hp <= 0L)
            {
                //this.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, 0.0f);
                OnDeadForRevival = null;
                SetState(ActionState.DIE);
                StandByDone = true;
                isDeadBySetCurrentHp = true;
            }
            if (StartHpPercent != 0.0)
                return;
            StartHpPercent = _hp / (float)(long)MaxHp;
        }

        public void SetCurrentHpZero() => Hp = 0L;

        public void SetMaxHp(long _maxHp) => MaxHp = _maxHp;

        /*public void SetCurrentHpForTowerTimeUp(int _hp)
        {
            this.Hp = (long)(long)_hp;
            if (!((UnityEngine.Object)this.lifeGauge != (UnityEngine.Object)null))
                return;
            float NormalizedHP = (float)(long)this.Hp / (float)(long)this.MaxHp;
            this.lifeGauge.SetNormalizedLifeAmount(NormalizedHP, _isTowerTimeUp: true);
            this.OnLifeAmmountChange.Call<float>(NormalizedHP);
            this.OnDamageForUIShake.Call();
            this.StartCoroutine(this.setColorOffsetDefaultWithDelay());
        }*/

        public void SetEnergy(FloatWithEx energy, eSetEnergyType type, UnitCtrl source = null)
        {
            BattleLogIntreface battleLog = this.battleLog;
            UnitCtrl unitCtrl1 = source;
            UnitCtrl unitCtrl2 = this;
            int HLIKLPNIOKJ = (int)type;
            long KDCBJHCMAOH = (int)energy;
            UnitCtrl JELADBAMFKH = unitCtrl1;
            UnitCtrl LIMEKPEENOB = unitCtrl2;
            battleLog.AppendBattleLog(eBattleLogType.SET_ENERGY, HLIKLPNIOKJ, 0L, KDCBJHCMAOH, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
            Energy = energy;
            SemanOnTPChanged?.Invoke(UnitId, (float)Energy);
            if (!battleManager.skipping)
                NoSkipOnTPChanged?.Invoke(UnitId,(float)Energy, BattleHeaderController.CurrentFrameCount,type.GetDescription());
            if(uIManager!=null)
                uIManager.LogMessage($"TP变更为：{(int)(energy * 1000) / 1000f:F3}", eLogMessageType.CHANGE_TP, this);
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

        public eSpecialSleepStatus specialSleepStatus { get; set; } = eSpecialSleepStatus.INVALID;
        private bool isSpecialSleepStatus
        {
            get
            {
                if (specialSleepStatus != 0 && specialSleepStatus != eSpecialSleepStatus.WAIT_START_END)
                {
                    return specialSleepStatus == eSpecialSleepStatus.LOOP;
                }
                return true;
            }
        }
        private bool isAbnormalEffectEnable { get; set; } = true;
        public bool IsCharaAuraEnable
        {
            get;
            set;
        } = true;

        public bool AbnormalIconVisible { get; set; }

        public bool AttackWhenSilence { get; set; }

        public List<SkillEffectCtrl> ModeChangeEndEffectList { get; set; } = new List<SkillEffectCtrl>();

        public Action<UnitCtrl, eStateIconType, bool> OnChangeState { get; set; }
        

        public Action<UnitCtrl, eStateIconType, int> OnChangeStateNum { get; set; }
        public void ExecSkillBySkillId(int _skillId, int _bonusId)
        {
            unitActionController.StartAction(_skillId);
            unitActionController.SetBonusId(_skillId, _bonusId);
        }
        /*public static eAbnormalStateCategory GetAbnormalStateCategory(
          eAbnormalState abnormalState)
        {
            eAbnormalStateCategory abnormalStateCategory = eAbnormalStateCategory.NONE;
            switch (abnormalState)
            {
                case eAbnormalState.GUARD_ATK:
                case eAbnormalState.DRAIN_ATK:
                    abnormalStateCategory = eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK;
                    break;
                case eAbnormalState.GUARD_MGC:
                case eAbnormalState.DRAIN_MGC:
                    abnormalStateCategory = eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK;
                    break;
                case eAbnormalState.GUARD_BOTH:
                case eAbnormalState.DRAIN_BOTH:
                    abnormalStateCategory = eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH;
                    break;
                case eAbnormalState.HASTE:
                case eAbnormalState.SLOW:
                    abnormalStateCategory = eAbnormalStateCategory.SPEED;
                    break;
                case eAbnormalState.POISON:
                    abnormalStateCategory = eAbnormalStateCategory.POISON;
                    break;
                case eAbnormalState.BURN:
                    abnormalStateCategory = eAbnormalStateCategory.BURN;
                    break;
                case eAbnormalState.CURSE:
                    abnormalStateCategory = eAbnormalStateCategory.CURSE;
                    break;
                case eAbnormalState.PARALYSIS:
                    abnormalStateCategory = eAbnormalStateCategory.PARALYSIS;
                    break;
                case eAbnormalState.FREEZE:
                    abnormalStateCategory = eAbnormalStateCategory.FREEZE;
                    break;
                case eAbnormalState.CONVERT:
                    abnormalStateCategory = eAbnormalStateCategory.CONVERT;
                    break;
                case eAbnormalState.PHYSICS_DARK:
                    abnormalStateCategory = eAbnormalStateCategory.PHYSICAL_DARK;
                    break;
                case eAbnormalState.SILENCE:
                    abnormalStateCategory = eAbnormalStateCategory.SILENCE;
                    break;
                case eAbnormalState.CHAINED:
                    abnormalStateCategory = eAbnormalStateCategory.CHAINED;
                    break;
                case eAbnormalState.SLEEP:
                    abnormalStateCategory = eAbnormalStateCategory.SLEEP;
                    break;
                case eAbnormalState.STUN:
                    abnormalStateCategory = eAbnormalStateCategory.STUN;
                    break;
                case eAbnormalState.DETAIN:
                    abnormalStateCategory = eAbnormalStateCategory.DETAIN;
                    break;
                case eAbnormalState.NO_EFFECT_SLIP_DAMAGE:
                    abnormalStateCategory = eAbnormalStateCategory.NO_EFFECT_SLIP_DAMAGE;
                    break;
                case eAbnormalState.NO_DAMAGE_MOTION:
                    abnormalStateCategory = eAbnormalStateCategory.NO_DAMAGE;
                    break;
                case eAbnormalState.NO_ABNORMAL:
                    abnormalStateCategory = eAbnormalStateCategory.NO_ABNORMAL;
                    break;
                case eAbnormalState.NO_DEBUF:
                    abnormalStateCategory = eAbnormalStateCategory.NO_DEBUF;
                    break;
                case eAbnormalState.ACCUMULATIVE_DAMAGE:
                    abnormalStateCategory = eAbnormalStateCategory.ACCUMULATIVE_DAMAGE;
                    break;
                case eAbnormalState.DECOY:
                    abnormalStateCategory = eAbnormalStateCategory.DECOY;
                    break;
                case eAbnormalState.MIFUYU:
                    abnormalStateCategory = eAbnormalStateCategory.MIFUYU;
                    break;
                case eAbnormalState.STONE:
                    abnormalStateCategory = eAbnormalStateCategory.STONE;
                    break;
                case eAbnormalState.REGENERATION:
                    abnormalStateCategory = eAbnormalStateCategory.REGENERATION;
                    break;
                case eAbnormalState.PHYSICS_DODGE:
                    abnormalStateCategory = eAbnormalStateCategory.PHYSICS_DODGE;
                    break;
                case eAbnormalState.CONFUSION:
                    abnormalStateCategory = eAbnormalStateCategory.CONFUSION;
                    break;
                case eAbnormalState.VENOM:
                    abnormalStateCategory = eAbnormalStateCategory.VENOM;
                    break;
                case eAbnormalState.COUNT_BLIND:
                    abnormalStateCategory = eAbnormalStateCategory.COUNT_BLIND;
                    break;
                case eAbnormalState.INHIBIT_HEAL:
                    abnormalStateCategory = eAbnormalStateCategory.INHIBIT_HEAL;
                    break;
                case eAbnormalState.FEAR:
                    abnormalStateCategory = eAbnormalStateCategory.FEAR;
                    break;
                case eAbnormalState.TP_REGENERATION:
                    abnormalStateCategory = eAbnormalStateCategory.TP_REGENERATION;
                    break;
                case eAbnormalState.HEX:
                    abnormalStateCategory = eAbnormalStateCategory.HEX;
                    break;
                case eAbnormalState.FAINT:
                    abnormalStateCategory = eAbnormalStateCategory.FAINT;
                    break;
                case eAbnormalState.PARTS_NO_DAMAGE:
                    abnormalStateCategory = eAbnormalStateCategory.PARTS_NO_DAMAGE;
                    break;
                case eAbnormalState.COMPENSATION:
                    abnormalStateCategory = eAbnormalStateCategory.COMPENSATION;
                    break;
                case eAbnormalState.CUT_ATK_DAMAGE:
                    abnormalStateCategory = eAbnormalStateCategory.CUT_ATK_DAMAGE;
                    break;
                case eAbnormalState.CUT_MGC_DAMAGE:
                    abnormalStateCategory = eAbnormalStateCategory.CUT_MGC_DAMAGE;
                    break;
                case eAbnormalState.CUT_ALL_DAMAGE:
                    abnormalStateCategory = eAbnormalStateCategory.CUT_ALL_DAMAGE;
                    break;
                case eAbnormalState.LOG_ATK_BARRIR:
                    abnormalStateCategory = eAbnormalStateCategory.LOG_ATK_BARRIR;
                    break;
                case eAbnormalState.LOG_MGC_BARRIR:
                    abnormalStateCategory = eAbnormalStateCategory.LOG_MGC_BARRIR;
                    break;
                case eAbnormalState.LOG_ALL_BARRIR:
                    abnormalStateCategory = eAbnormalStateCategory.LOG_ALL_BARRIR;
                    break;
                case eAbnormalState.PAUSE_ACTION:
                    abnormalStateCategory = eAbnormalStateCategory.PAUSE_ACTION;
                    break;
                case eAbnormalState.UB_SILENCE:
                    abnormalStateCategory = eAbnormalStateCategory.UB_SILENCE;
                    break;
                case eAbnormalState.MAGIC_DARK:
                    abnormalStateCategory = eAbnormalStateCategory.MAGIC_DARK;
                    break;
                case eAbnormalState.HEAL_DOWN:
                    abnormalStateCategory = eAbnormalStateCategory.HEAL_DOWN;
                    break;
                case eAbnormalState.NPC_STUN:
                    abnormalStateCategory = eAbnormalStateCategory.NPC_STUN;
                    break;
            }
            return abnormalStateCategory;
        }*/
        public static eAbnormalStateCategory GetAbnormalStateCategory(eAbnormalState abnormalState)
        {
            eAbnormalStateCategory result = eAbnormalStateCategory.NONE;
            switch (abnormalState)
            {
                case eAbnormalState.GUARD_ATK:
                case eAbnormalState.DRAIN_ATK:
                    result = eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK;
                    break;
                case eAbnormalState.GUARD_MGC:
                case eAbnormalState.DRAIN_MGC:
                    result = eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK;
                    break;
                case eAbnormalState.GUARD_BOTH:
                case eAbnormalState.DRAIN_BOTH:
                    result = eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH;
                    break;
                case eAbnormalState.POISON:
                    result = eAbnormalStateCategory.POISON;
                    break;
                case eAbnormalState.POISON2:
                    result = eAbnormalStateCategory.POISON2;
                    break;
                case eAbnormalState.VENOM:
                    result = eAbnormalStateCategory.VENOM;
                    break;
                case eAbnormalState.BURN:
                    result = eAbnormalStateCategory.BURN;
                    break;
                case eAbnormalState.CURSE:
                    result = eAbnormalStateCategory.CURSE;
                    break;
                case eAbnormalState.CURSE2:
                    result = eAbnormalStateCategory.CURSE2;
                    break;
                case eAbnormalState.HASTE:
                case eAbnormalState.SLOW:
                    result = eAbnormalStateCategory.SPEED;
                    break;
                case eAbnormalState.SLOW_OVERLAP:
                case eAbnormalState.HASTE_OVERLAP:
                    result = eAbnormalStateCategory.SPEED_OVERLAP;
                    break;
                case eAbnormalState.SILENCE:
                    result = eAbnormalStateCategory.SILENCE;
                    break;
                case eAbnormalState.PHYSICS_DARK:
                    result = eAbnormalStateCategory.PHYSICAL_DARK;
                    break;
                case eAbnormalState.MAGIC_DARK:
                    result = eAbnormalStateCategory.MAGIC_DARK;
                    break;
                case eAbnormalState.CONVERT:
                    result = eAbnormalStateCategory.CONVERT;
                    break;
                case eAbnormalState.CONFUSION:
                    result = eAbnormalStateCategory.CONFUSION;
                    break;
                case eAbnormalState.CONFUSION2:
                    result = eAbnormalStateCategory.CONFUSION2;
                    break;
                case eAbnormalState.MIFUYU:
                    result = eAbnormalStateCategory.MIFUYU;
                    break;
                case eAbnormalState.PARALYSIS:
                    result = eAbnormalStateCategory.PARALYSIS;
                    break;
                case eAbnormalState.STUN:
                    result = eAbnormalStateCategory.STUN;
                    break;
                case eAbnormalState.STUN2:
                    result = eAbnormalStateCategory.STUN2;
                    break;
                case eAbnormalState.NPC_STUN:
                    result = eAbnormalStateCategory.NPC_STUN;
                    break;
                case eAbnormalState.STONE:
                    result = eAbnormalStateCategory.STONE;
                    break;
                case eAbnormalState.REGENERATION:
                    result = eAbnormalStateCategory.REGENERATION;
                    break;
                case eAbnormalState.REGENERATION2:
                    result = eAbnormalStateCategory.REGENERATION2;
                    break;
                case eAbnormalState.DETAIN:
                    result = eAbnormalStateCategory.DETAIN;
                    break;
                case eAbnormalState.FREEZE:
                    result = eAbnormalStateCategory.FREEZE;
                    break;
                case eAbnormalState.DECOY:
                    result = eAbnormalStateCategory.DECOY;
                    break;
                case eAbnormalState.NO_DAMAGE_MOTION:
                    result = eAbnormalStateCategory.NO_DAMAGE;
                    break;
                case eAbnormalState.NO_DAMAGE_MOTION2:
                    result = eAbnormalStateCategory.NO_DAMAGE2;
                    break;
                case eAbnormalState.NO_ABNORMAL:
                    result = eAbnormalStateCategory.NO_ABNORMAL;
                    break;
                case eAbnormalState.NO_DEBUF:
                    result = eAbnormalStateCategory.NO_DEBUF;
                    break;
                case eAbnormalState.PARTS_NO_DAMAGE:
                    result = eAbnormalStateCategory.PARTS_NO_DAMAGE;
                    break;
                case eAbnormalState.ACCUMULATIVE_DAMAGE:
                    result = eAbnormalStateCategory.ACCUMULATIVE_DAMAGE;
                    break;
                case eAbnormalState.ACCUMULATIVE_DAMAGE_FOR_ALL_ENEMY:
                    result = eAbnormalStateCategory.ACCUMULATIVE_DAMAGE;
                    break;
                case eAbnormalState.SLEEP:
                    result = eAbnormalStateCategory.SLEEP;
                    break;
                case eAbnormalState.CHAINED:
                    result = eAbnormalStateCategory.CHAINED;
                    break;
                case eAbnormalState.NO_EFFECT_SLIP_DAMAGE:
                    result = eAbnormalStateCategory.NO_EFFECT_SLIP_DAMAGE;
                    break;
                case eAbnormalState.PHYSICS_DODGE:
                    result = eAbnormalStateCategory.PHYSICS_DODGE;
                    break;
                case eAbnormalState.COUNT_BLIND:
                    result = eAbnormalStateCategory.COUNT_BLIND;
                    break;
                case eAbnormalState.INHIBIT_HEAL:
                    result = eAbnormalStateCategory.INHIBIT_HEAL;
                    break;
                case eAbnormalState.FEAR:
                    result = eAbnormalStateCategory.FEAR;
                    break;
                case eAbnormalState.TP_REGENERATION:
                    result = eAbnormalStateCategory.TP_REGENERATION;
                    break;
                case eAbnormalState.TP_REGENERATION2:
                    result = eAbnormalStateCategory.TP_REGENERATION2;
                    break;
                case eAbnormalState.HEX:
                    result = eAbnormalStateCategory.HEX;
                    break;
                case eAbnormalState.FAINT:
                    result = eAbnormalStateCategory.FAINT;
                    break;
                case eAbnormalState.COMPENSATION:
                    result = eAbnormalStateCategory.COMPENSATION;
                    break;
                case eAbnormalState.CUT_ATK_DAMAGE:
                    result = eAbnormalStateCategory.CUT_ATK_DAMAGE;
                    break;
                case eAbnormalState.CUT_MGC_DAMAGE:
                    result = eAbnormalStateCategory.CUT_MGC_DAMAGE;
                    break;
                case eAbnormalState.CUT_ALL_DAMAGE:
                    result = eAbnormalStateCategory.CUT_ALL_DAMAGE;
                    break;
                case eAbnormalState.LOG_ATK_BARRIR:
                    result = eAbnormalStateCategory.LOG_ATK_BARRIR;
                    break;
                case eAbnormalState.LOG_MGC_BARRIR:
                    result = eAbnormalStateCategory.LOG_MGC_BARRIR;
                    break;
                case eAbnormalState.LOG_ALL_BARRIR:
                    result = eAbnormalStateCategory.LOG_ALL_BARRIR;
                    break;
                case eAbnormalState.PAUSE_ACTION:
                    result = eAbnormalStateCategory.PAUSE_ACTION;
                    break;
                case eAbnormalState.UB_SILENCE:
                    result = eAbnormalStateCategory.UB_SILENCE;
                    break;
                case eAbnormalState.HEAL_DOWN:
                    result = eAbnormalStateCategory.HEAL_DOWN;
                    break;
                case eAbnormalState.DECREASE_HEAL:
                    result = eAbnormalStateCategory.DECREASE_HEAL;
                    break;
                case eAbnormalState.POISON_BY_BEHAVIOUR:
                    result = eAbnormalStateCategory.POISON_BY_BEHAVIOUR;
                    break;
                case eAbnormalState.CRYSTALIZE:
                    result = eAbnormalStateCategory.CRYSTALIZE;
                    break;
                case eAbnormalState.DAMAGE_LIMIT_ALL:
                    result = eAbnormalStateCategory.DAMAGE_LIMIT_ALL;
                    break;
                case eAbnormalState.DAMAGE_LIMIT_ATK:
                    result = eAbnormalStateCategory.DAMAGE_LIMIT_ATK;
                    break;
                case eAbnormalState.DAMAGE_LIMIT_MGC:
                    result = eAbnormalStateCategory.DAMAGE_LIMIT_MGC;
                    break;
                case eAbnormalState.SPY:
                    result = eAbnormalStateCategory.SPY;
                    break;
                case UnitCtrl.eAbnormalState.ENERGY_DAMAGE_REDUCE:
                    result = UnitCtrl.eAbnormalStateCategory.ENERGY_DAMAGE_REDUCE;
                    break;
                case UnitCtrl.eAbnormalState.BLACK_FRAME:
                    result = UnitCtrl.eAbnormalStateCategory.BLACK_FRAME;
                    break;
            }
            return result;
        }

        /*private void setWeakColor()
        {
            this.curColor = UnitCtrl.WEAK_COLOR;
            this.updateCurColor();
        }*/

        public void SetAbnormalState(
          UnitCtrl _source,
          eAbnormalState _abnormalState,
          float _effectTime,
          ActionParameter _action,
          Skill _skill,
          FloatWithEx _value = default,
          FloatWithEx _value2 = default,
          bool _reduceEnergy = false,
          bool _isDamageRelease = false,
          float _reduceEnergyRate = 1f,
          bool _showsIcon = true)
        {
            if (battleManager.GameState != eBattleGameState.PLAY)
                return;
            _value = _value ?? 0f;
            _value2 = _value2 ?? 0f;
            AbnormalStateEffectPrefabData abnormalEffectData = _action?.CreateAbnormalEffectData();
            if (_action != null && _action.AbnormalStateFieldAction != null)
                _action.AbnormalStateFieldAction.TargetAbnormalState = _abnormalState;
            if ((IsNoDamageMotion() || IsAbnormalState(eAbnormalState.NO_ABNORMAL)) && !ABNORMAL_CONST_DATA[_abnormalState].IsBuff)
            {
                BattleLogIntreface battleLog = this.battleLog;
                UnitCtrl unitCtrl1 = _source;
                UnitCtrl unitCtrl2 = this;
                long KGNFLOPBOMB = (int)_value;
                int OJHBHHCOAGK = _action == null ? 0 : _action.ActionId;
                UnitCtrl JELADBAMFKH = unitCtrl1;
                UnitCtrl LIMEKPEENOB = unitCtrl2;
                battleLog.AppendBattleLog(eBattleLogType.MISS, 7, KGNFLOPBOMB, 0L, 0, OJHBHHCOAGK, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
                if (!IsAbnormalState(eAbnormalState.NO_ABNORMAL))
                    return;
                SetMissAtk(_source, eMissLogType.DODGE_BY_NO_DAMAGE_MOTION);
            }
            else
            {
                if (tryApplyConditionExSkill(_abnormalState, ref _effectTime))
                {
                    return;
                }
                switch (_abnormalState)
                {
                    case eAbnormalState.POISON:
                    case eAbnormalState.BURN:
                    case eAbnormalState.CURSE:
                    case eAbnormalState.NO_EFFECT_SLIP_DAMAGE:
                    case eAbnormalState.VENOM:
                    case eAbnormalState.HEX:
                    case eAbnormalState.COMPENSATION:
                    case eAbnormalState.POISON_BY_BEHAVIOUR:
                    case eAbnormalState.POISON2:
                    case eAbnormalState.CURSE2:
                        OnSlipDamage.Call();
                        break;
                }
                BattleLogIntreface battleLog = this.battleLog;
                UnitCtrl unitCtrl1 = _source;
                UnitCtrl unitCtrl2 = this;
                int HLIKLPNIOKJ = (int)_abnormalState;
                long KGNFLOPBOMB = (int)_value;
                long KDCBJHCMAOH = (int)(_effectTime * (double)battleManager.FameRate);
                int OJHBHHCOAGK = _action == null ? 0 : _action.ActionId;
                UnitCtrl JELADBAMFKH = unitCtrl1;
                UnitCtrl LIMEKPEENOB = unitCtrl2;
                battleLog.AppendBattleLog(eBattleLogType.SET_ABNORMAL, HLIKLPNIOKJ, KGNFLOPBOMB, KDCBJHCMAOH, 0, OJHBHHCOAGK, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
                eAbnormalStateCategory abnormalStateCategory = GetAbnormalStateCategory(_abnormalState);
                AbnormalStateCategoryData stateCategoryData = abnormalStateCategoryDataDictionary[abnormalStateCategory];
                if (_abnormalState == eAbnormalState.GUARD_ATK && IsAbnormalState(eAbnormalState.DRAIN_ATK))
                { }  /*stateCategoryData.Effects.Add(new AbnormalStateEffectGameObject()
                    {
                        RightEffect = this.CreateAbnormalStateEffect(_abnormalState, true, abnormalEffectData),
                        LeftEffect = this.CreateAbnormalStateEffect(_abnormalState, false, abnormalEffectData)
                    });*/
                else if (_abnormalState == eAbnormalState.GUARD_MGC && IsAbnormalState(eAbnormalState.DRAIN_MGC))
                { }  /*stateCategoryData.Effects.Add(new AbnormalStateEffectGameObject()
                    {
                        RightEffect = this.CreateAbnormalStateEffect(_abnormalState, true, abnormalEffectData),
                        LeftEffect = this.CreateAbnormalStateEffect(_abnormalState, false, abnormalEffectData)
                    });*/
                else if (_abnormalState == eAbnormalState.GUARD_BOTH && IsAbnormalState(eAbnormalState.DRAIN_BOTH))
                {
                    /*stateCategoryData.Effects.Add(new AbnormalStateEffectGameObject()
                    {
                        RightEffect = this.CreateAbnormalStateEffect(_abnormalState, true, abnormalEffectData),
                        LeftEffect = this.CreateAbnormalStateEffect(_abnormalState, false, abnormalEffectData)
                    });*/
                }
                else
                {
                    if (IsDead)
                        return;
                    if (CanOverlapAbnormalState(_abnormalState))
                    {
                        if (!BattleManager.Instance.skipping && button != null)
                        {
                            if (_abnormalState == eAbnormalState.SLOW_OVERLAP || _abnormalState == eAbnormalState.HASTE_OVERLAP)
                            {
                                button.SetAbnormalIcons(this, eStateIconType.SPEED_OVERLAP, true, _effectTime, $"{_value}");
                            }
                        }

                        battleManager.AppendCoroutine(updateOverlapAbnormalState(_abnormalState, new AbnormalStateCategoryData
                        {
                            CurrentAbnormalState = _abnormalState,
                            enable = true,
                            MainValue = _value,
                            SubValue = _value2,
                            Time = _effectTime,
                            ActionId = (_action?.ActionId ?? 0),
                            IsDamageRelease = _isDamageRelease,
                            ShowsIcon = false,
                            Skill = _skill,
                            Source = _source
                        }), ePauseType.SYSTEM);
                        return;
                    }
                    if (IsAbnormalState(abnormalStateCategory))
                        switchAbnormalState(_abnormalState, abnormalEffectData,_showsIcon);
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
                    stateCategoryData.ShowsIcon = _showsIcon;

                    if (_action != null)
                        stateCategoryData.EnergyChargeMultiple = _action.EnergyChargeMultiple;
                    stateCategoryData.AbsorberValue = battleManager.KIHOGJBONDH;
                    if (IsAbnormalState(abnormalStateCategory))
                        return;
                    stateCategoryData.CurrentAbnormalState = _abnormalState;
                    //if (barriers.ContainsKey(_abnormalState)) barriers[_abnormalState] = _value;
                    IEnumerator _cr = UpdateAbnormalState(_abnormalState, abnormalEffectData);
                    if (!_cr.MoveNext())
                        return;
                    AppendCoroutine(_cr, ePauseType.SYSTEM);
                }
            }
        }

        private bool tryApplyConditionExSkill(eAbnormalState _abnormalState, ref float _effectTime)
        {
            switch (_abnormalState)
            {
            case eAbnormalState.PARALYSIS:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.PARALYSIS], ref _effectTime);
            case eAbnormalState.FREEZE:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.FREEZE], ref _effectTime);
            case eAbnormalState.CHAINED:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.CHAINED], ref _effectTime);
            case eAbnormalState.SLEEP:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.SLEEP], ref _effectTime);
            case eAbnormalState.STUN:
            case eAbnormalState.NPC_STUN:
            case eAbnormalState.STUN2:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.STUN], ref _effectTime);
            case eAbnormalState.STONE:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.STONE], ref _effectTime);
            case eAbnormalState.PAUSE_ACTION:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.PAUSE_ACTION], ref _effectTime);
            case eAbnormalState.POISON:
            case eAbnormalState.POISON_BY_BEHAVIOUR:
            case eAbnormalState.POISON2:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.POISON], ref _effectTime);
            case eAbnormalState.BURN:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.BURN], ref _effectTime);
            case eAbnormalState.CURSE:
            case eAbnormalState.CURSE2:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.CURSE], ref _effectTime);
            case eAbnormalState.VENOM:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.VENOM], ref _effectTime);
            case eAbnormalState.HEX:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.HEX], ref _effectTime);
            case eAbnormalState.CONVERT:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.CONVERT], ref _effectTime);
            case eAbnormalState.CONFUSION:
            case eAbnormalState.CONFUSION2:
                return tryConditionExSkill(ExSkillData.ABNORMAL_CONDITION_PAIR[eAbnormalState.CONFUSION], ref _effectTime);
            default:
                return false;
            }
        }

        private bool tryConditionExSkill(List<eExSkillCondition> _conditions, ref float _effectTime)
        {
            tryConditionExSkillImpl(_conditions, ExConditionPassiveData.eEffectType.NORMAL, ref _effectTime);
            if (tryConditionExSkillImpl(_conditions, ExConditionPassiveData.eEffectType.CANCEL, ref _effectTime))
            {
                return true;
            }
            tryConditionExSkillImpl(_conditions, ExConditionPassiveData.eEffectType.SHORTEN, ref _effectTime);
            return false;
        }

        private bool tryConditionExSkillImpl(List<eExSkillCondition> _conditions, ExConditionPassiveData.eEffectType _targetEffectType, ref float _effectTime)
        {
            List<ExSkillData> value = null;
            foreach (eExSkillCondition _condition in _conditions)
            {
                if (!conditionExSkillDictionary.TryGetValue(_condition, out value))
                {
                    continue;
                }
                foreach (ExSkillData item in value)
                {
                    switch (_targetEffectType)
                    {
                    case ExConditionPassiveData.eEffectType.NORMAL:
                        item.TryExSkill(_condition, unitActionController, _all: false);
                        break;
                    case ExConditionPassiveData.eEffectType.CANCEL:
                    case ExConditionPassiveData.eEffectType.SHORTEN:
                        switch (item.TryExSkill(_condition, unitActionController, _all: false, _targetEffectType))
                        {
                        case ExSkillData.eTryExResult.CANCEL:
                            return true;
                        case ExSkillData.eTryExResult.SHORTEN:
                            _effectTime *= item.ExSkillConditionData[_condition].Value;
                            return false;
                        }
                        break;
                    }
                }
            }
            return false;
        }

        public static bool CanOverlapAbnormalState(eAbnormalState _abnormalState)
        {
            if ((uint)(_abnormalState - 65) <= 1u)
            {
                return true;
            }
            return false;
        }
        private IEnumerator updateOverlapAbnormalState(eAbnormalState _abnormalState, AbnormalStateCategoryData _data)
        {

            int currentBehaviourIndex = overlapAbnormalStateCount;
            overlapAbnormalStateCount++;
            overlapAbnormalStateIndexList[_abnormalState].Add(currentBehaviourIndex);
            overlapAbnormalStateData[currentBehaviourIndex] = _data;
            m_abnormalState[_abnormalState] = true;

            /*
            if (_abnormalState == eAbnormalState.SLOW_OVERLAP || _abnormalState == eAbnormalState.HASTE_OVERLAP)
            {
                updateEffectForSpeedAbnormalState(_isEnable: true);
            }*/
            float time = 0f;
            for (float effectTime = _data.Time; time < effectTime; time += DeltaTimeForPause)
            {
                yield return null;
                if (!overlapAbnormalStateIndexList[_abnormalState].Contains(currentBehaviourIndex))
                {
                    break;
                }
            }
            overlapAbnormalStateIndexList[_abnormalState].Remove(currentBehaviourIndex);
            overlapAbnormalStateData[currentBehaviourIndex].enable = false;
            overlapAbnormalStateData.Remove(currentBehaviourIndex);
            m_abnormalState[_abnormalState] = overlapAbnormalStateIndexList[_abnormalState].Count > 0;
            /*
            if (_abnormalState == eAbnormalState.SLOW_OVERLAP || _abnormalState == eAbnormalState.HASTE_OVERLAP)
            {
                updateEffectForSpeedAbnormalState(_isEnable: false);
            }*/
        }
        private void updateEffectForSpeedAbnormalState(bool _isEnable)
        {
            /*CalcAbnormalStateSpeed();
            if (_isEnable)
            {
                if (IsSlowSpeed())
                {
                    if (CurrentState == ActionState.IDLE)
                    {
                        GetCurrentSpineCtrl().SetTimeScale(0.5f);
                    }
                    setWeakColor();
                }
                else
                {
                    if (IsHasteSpeed() && CurrentState == ActionState.IDLE)
                    {
                        GetCurrentSpineCtrl().SetTimeScale(2f);
                    }
                    SetEnableColor();
                }
            }
            else if (IsSlowSpeed())
            {
                setWeakColor();
            }
            else
            {
                SetEnableColor();
            }*/
        }

        /*
        private readonly Dictionary<eAbnormalState, FloatWithEx> barriers = new Dictionary<eAbnormalState, FloatWithEx>()
        {
            [eAbnormalState.DRAIN_ATK] = 0f,
            [eAbnormalState.DRAIN_MGC] = 0f,
            [eAbnormalState.DRAIN_BOTH] = 0f,
            [eAbnormalState.GUARD_ATK] = 0f,
            [eAbnormalState.GUARD_MGC] = 0f,
            [eAbnormalState.GUARD_BOTH] = 0f,
        };*/
        public float CalcAbnormalStateSpeed()
        {
            float num = 1f;
            if (IsAbnormalState(eAbnormalState.SLOW) || IsAbnormalState(eAbnormalState.HASTE))
            {
                num = abnormalStateCategoryDataDictionary[eAbnormalStateCategory.SPEED].MainValue;
            }
            return Mathf.Max(0f, num + calcOverlapSpeed());
        }
        private float calcOverlapSpeed()
        {
            float num = 0f;
            foreach (KeyValuePair<eAbnormalState, List<int>> overlapAbnormalStateIndex in overlapAbnormalStateIndexList)
            {
                List<int> value = overlapAbnormalStateIndex.Value;
                for (int i = 0; i < value.Count; i++)
                {
                    num += overlapAbnormalStateData[value[i]].MainValue;
                }
            }
            return num;
        }
        public bool IsHasteSpeed()
        {
            return !BattleUtil.LessThanOrApproximately(CalcAbnormalStateSpeed(), 1f);
        }
        public bool IsSlowSpeed()
        {
            float num = CalcAbnormalStateSpeed();
            if (BattleUtil.Approximately(num, 1f))
            {
                return false;
            }
            return num < 1f;
        }
        private void setIdleCastTime()
        {
            float num = CalcAbnormalStateSpeed();
            if (BattleUtil.Approximately(num, 0f))
            {
                m_fCastTimer = 90f;
            }
            else
            {
                m_fCastTimer = (float)m_fCastTimer / num;
            }
        }
        private void switchAbnormalState(
          eAbnormalState abnormalState,
          AbnormalStateEffectPrefabData _specialEffectData, bool _showsIconAfterSwitch)
        {
            AbnormalStateCategoryData stateCategoryData = abnormalStateCategoryDataDictionary[GetAbnormalStateCategory(abnormalState)];
            EnableAbnormalState(stateCategoryData.CurrentAbnormalState, _enable: false, _reduceEnergy: false, _switch: true);
            stateCategoryData.ShowsIcon = _showsIconAfterSwitch;
            EnableAbnormalState(abnormalState, true);
            stateCategoryData.CurrentAbnormalState = abnormalState;
            StartCoroutine(waitRefreshAbnormalStateUI(abnormalState));
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

            private IEnumerator waitRefreshAbnormalStateUI(eAbnormalState abnormalState)
        {
            yield return null;
            AbnormalStateCategoryData stateCategoryData = abnormalStateCategoryDataDictionary[GetAbnormalStateCategory(abnormalState)];
            stateCategoryData.Duration -= 1 / 60.0f;
            string describe = stateCategoryData.MainValue + "";
            //this.EnableAbnormalState(abnormalState, true);
            //stateCategoryData.CurrentAbnormalState = abnormalState;
            NoSkipOnChangeAbnormalState?.Invoke(this, ABNORMAL_CONST_DATA[abnormalState].IconType,
    true, stateCategoryData.Duration, describe);

        }
        //end add

        private IEnumerator UpdateAbnormalState(
          eAbnormalState _abnormalState,
          AbnormalStateEffectPrefabData _specialEffectData)
        {
            eAbnormalStateCategory abnormalStateCategory = GetAbnormalStateCategory(_abnormalState);
            AbnormalStateCategoryData abnormalStateCategoryData = abnormalStateCategoryDataDictionary[abnormalStateCategory];
            abnormalStateCategoryData.Time = abnormalStateCategoryData.Duration;
            /*abnormalStateCategoryData.Effects.Add(new AbnormalStateEffectGameObject()
            {
                RightEffect = this.CreateAbnormalStateEffect(_abnormalState, true, _specialEffectData),
                LeftEffect = this.CreateAbnormalStateEffect(_abnormalState, false, _specialEffectData)
            });*/
            EnableAbnormalState(_abnormalState, true, abnormalStateCategoryData.IsEnergyReduceMode);
            while (IsAbnormalState(abnormalStateCategory))
            {
                _abnormalState = abnormalStateCategoryData.CurrentAbnormalState;
                if (abnormalStateCategoryData.IsEnergyReduceMode)
                {
                    SetEnergy(Energy - DeltaTimeForPause * abnormalStateCategoryData.EnergyReduceRate, eSetEnergyType.BY_MODE_CHANGE);
                    if ((double)Energy == 0.0 || IsDead)
                    {
                        EnableAbnormalState(_abnormalState, false);
                        break;
                    }
                }
                else
                {
                    abnormalStateCategoryData.Time -= DeltaTimeForPause;
                    if (abnormalStateCategoryData.Time <= 0.0 || IsDead)
                    {
                        EnableAbnormalState(_abnormalState, false);
                        break;
                    }
                }
                yield return null;
            }
        }

        private void DestroyAbnormalEffect(
          eAbnormalStateCategory abnormalStateCategory)
        {
            /*for (int index = 0; index < this.abnormalStateCategoryDataDictionary[abnormalStateCategory].Effects.Count; ++index)
            {
                AbnormalStateEffectGameObject effect = this.abnormalStateCategoryDataDictionary[abnormalStateCategory].Effects[index];
                if ((UnityEngine.Object)effect.RightEffect != (UnityEngine.Object)null)
                    effect.RightEffect.SetTimeToDie(true);
                if ((UnityEngine.Object)effect.LeftEffect != (UnityEngine.Object)null)
                    effect.LeftEffect.SetTimeToDie(true);
            }*/
            abnormalStateCategoryDataDictionary[abnormalStateCategory].Effects.Clear();
        }

        /*public void DisableAbnormalStateById(
          eAbnormalState _abnormalState,
          int _actionId,
          bool _isReleasedByDamage)
        {
            eAbnormalStateCategory abnormalStateCategory = GetAbnormalStateCategory(_abnormalState);
            if (abnormalStateCategoryDataDictionary[abnormalStateCategory].ActionId != _actionId)
                return;
            abnormalStateCategoryDataDictionary[abnormalStateCategory].IsReleasedByDamage = _isReleasedByDamage;
            EnableAbnormalState(_abnormalState, false);
        }*/
        public void DisableAbnormalStateById(eAbnormalState _abnormalState, int _actionId, bool _isReleasedByDamage)
        {
            if (CanOverlapAbnormalState(_abnormalState))
            {
                List<int> list = overlapAbnormalStateIndexList[_abnormalState];
                for (int num = list.Count - 1; num >= 0; num--)
                {
                    int key = list[num];
                    AbnormalStateCategoryData abnormalStateCategoryData = overlapAbnormalStateData[key];
                    if (abnormalStateCategoryData.enable && abnormalStateCategoryData.ActionId == _actionId)
                    {
                        overlapAbnormalStateData[key].IsReleasedByDamage = _isReleasedByDamage;
                        list.RemoveAt(num);
                    }
                }
            }
            else
            {
                eAbnormalStateCategory abnormalStateCategory = GetAbnormalStateCategory(_abnormalState);
                if (abnormalStateCategoryDataDictionary[abnormalStateCategory].ActionId == _actionId)
                {
                    abnormalStateCategoryDataDictionary[abnormalStateCategory].IsReleasedByDamage = _isReleasedByDamage;
                    EnableAbnormalState(_abnormalState, _enable: false);
                }
            }
        }


        private void EnableAbnormalState(
          eAbnormalState _abnormalState,
          bool _enable,
          bool _reduceEnergy = false,
          bool _switch = false
          //bool nobreak = false,
           // bool _switch_On = false
            )
        {
            eAbnormalStateCategory abnormalStateCategory = GetAbnormalStateCategory(_abnormalState);
            AbnormalStateCategoryData abnormalStateCategoryData = abnormalStateCategoryDataDictionary[abnormalStateCategory];
            if (!_enable)
            {
                DestroyAbnormalEffect(abnormalStateCategory);
                abnormalStateCategoryDataDictionary[abnormalStateCategory].MainValue = 0.0f;
                abnormalStateCategoryDataDictionary[abnormalStateCategory].Time = 0.0f;
                abnormalStateCategoryDataDictionary[abnormalStateCategory].Duration = 0.0f;
                abnormalStateCategoryDataDictionary[abnormalStateCategory].EnergyChargeMultiple = 1f;
                //if (!nobreak && barriers.ContainsKey(_abnormalState)) barriers[_abnormalState] = 0f;
            }
            abnormalStateCategoryDataDictionary[abnormalStateCategory].enable = _enable;
            m_abnormalState[_abnormalState] = _enable;
            string describe = abnormalStateCategoryDataDictionary[abnormalStateCategory].MainValue + "";
            /*switch (_abnormalState)
            {
                case eAbnormalState.HASTE:
                    if (_enable)
                    {
                        if (CurrentState == ActionState.IDLE)
                        {
                            GetCurrentSpineCtrl().SetTimeScale(2f);
                        }
                        break;
                    }
                    if (!IsUnableActionState() && !m_bPause)
                    {
                        GetCurrentSpineCtrl().Resume();
                        break;
                    }
                    break;
                case eAbnormalState.POISON:
                case eAbnormalState.BURN:
                case eAbnormalState.CURSE:
                case eAbnormalState.NO_EFFECT_SLIP_DAMAGE:
                case eAbnormalState.VENOM:
                case eAbnormalState.HEX:
                case eAbnormalState.COMPENSATION:
                    if (_enable)
                    {
                        AppendCoroutine(UpdateSlipDamage(abnormalStateCategory, ++slipDamageIdDictionary[abnormalStateCategory]), ePauseType.SYSTEM);
                    }
                    break;
                case eAbnormalState.SLOW:
                    if (_enable)
                    {
                        //this.setWeakColor();
                        if (CurrentState == ActionState.IDLE)
                        {
                            GetCurrentSpineCtrl().SetTimeScale(0.5f);
                            break;
                        }
                        break;
                    }
                    //this.SetEnableColor();
                    if (!IsUnableActionState())
                    {
                        GetCurrentSpineCtrl().Resume();
                        break;
                    }
                    break;
                case eAbnormalState.PARALYSIS:
                case eAbnormalState.FREEZE:
                case eAbnormalState.CHAINED:
                case eAbnormalState.STUN:
                case eAbnormalState.DETAIN:
                case eAbnormalState.FAINT:
                case eAbnormalState.NPC_STUN:
                    if (_enable)
                    {
                        if (CurrentState != ActionState.DAMAGE)
                        {
                            SetState(ActionState.DAMAGE);
                        }
                        break;
                    }
                    if (!IsUnableActionState() && !_switch)
                    {
                        //XX: temporary fix for damage spine resuming
                        //this.specialSleepStatus = UnitCtrl.eSpecialSleepStatus.INVALID;
                        BattleSpineController currentSpineCtrl3 = GetCurrentSpineCtrl();
                        if (currentSpineCtrl3.AnimationName == currentSpineCtrl3.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE))
                        {
                            currentSpineCtrl3.IsPlayAnimeBattle = false;
                            currentSpineCtrl3.IsStopState = false;
                        }
                        setMotionResume();
                        this.isContinueIdleForPauseAction = false;
                    }
                    break;
                case eAbnormalState.CONVERT:
                case eAbnormalState.CONFUSION:
                    if (!IsAbnormalState(eAbnormalState.PAUSE_ACTION))
                        SetDirectionAuto();
                    switch (CurrentState)
                    {
                        case ActionState.ATK:
                        case ActionState.SKILL_1:
                        case ActionState.SKILL:
                            CancelByConvert = true;
                            idleStartAfterWaitFrame = (float)m_fCastTimer <= (double)DeltaTimeForPause;
                            break;
                    }
                    if ((long)Hp > 0L && !IsUnableActionState() && CurrentState != ActionState.DAMAGE)
                    {
                        SetState(ActionState.IDLE);
                    }
                    break;
                case eAbnormalState.SLEEP:
                    BattleSpineController currentSpineCtrl1 = GetCurrentSpineCtrl();
                    if (_enable)
                    {
                        bool _isDamageAnimBeforeSleep = currentSpineCtrl1.AnimationName == currentSpineCtrl1.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix) || currentSpineCtrl1.AnimationName == currentSpineCtrl1.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, _index2: PartsMotionPrefix);
                        if (CurrentState != ActionState.DAMAGE)
                            SetState(ActionState.DAMAGE, _quiet: true);
                        if (!IsUnableActionState(eAbnormalState.SLEEP) && currentSpineCtrl1.HasSpecialSleepAnimatilon(MotionPrefix) && !currentSpineCtrl1.CheckPlaySpecialSleepAnimeExceptRelease(MotionPrefix))
                        {
                            specialSleepStatus = eSpecialSleepStatus.START;
                            battleManager.AppendCoroutine(playSleepAnime(_isDamageAnimBeforeSleep, abnormalStateCategoryDataDictionary[eAbnormalStateCategory.SLEEP].IsDamageRelease), ePauseType.IGNORE_BLACK_OUT);
                        }
                        break;
                    }
                    if (!IsUnableActionState() && !_switch)
                    {
                        if (currentSpineCtrl1.HasSpecialSleepAnimatilon(MotionPrefix))
                        {
                            currentSpineCtrl1.IsStopState = false;
                            releaseSleepAnime();
                        }
                        shiftAbnormalColor();
                        isContinueIdleForPauseAction = false;
                        break;
                    }
                    break;
                case eAbnormalState.DECOY:
                    UnitCtrl unitCtrl = IsOther ? battleManager.DecoyEnemy : battleManager.DecoyUnit;
                    if (_enable)
                    {
                        if (unitCtrl != null && unitCtrl != this)
                            unitCtrl.EnableAbnormalState(eAbnormalState.DECOY, false);
                        if (IsOther)
                        {
                            battleManager.DecoyEnemy = this;
                            break;
                        }
                        battleManager.DecoyUnit = this;
                        break;
                    }
                    if (unitCtrl == this)
                    {
                        if (IsOther)
                        {
                            battleManager.DecoyEnemy = null;
                            break;
                        }
                        battleManager.DecoyUnit = null;
                        break;
                    }
                    break;
                case eAbnormalState.MIFUYU:
                    if (!_enable)
                        break;
                    break;
                case eAbnormalState.STONE:
                    GetCurrentSpineCtrl().IsColorStone = _enable;
                    if (_enable)
                    {
                        if (CurrentState != ActionState.DAMAGE)
                            SetState(ActionState.DAMAGE);
                        if (specialSleepStatus != eSpecialSleepStatus.INVALID)
                        {
                            specialSleepStatus = eSpecialSleepStatus.INVALID;
                        }
                        break;
                    }
                    if (!IsUnableActionState() && !_switch)
                        specialSleepStatus = eSpecialSleepStatus.INVALID;
                    shiftAbnormalColor();
                    break;
                case eAbnormalState.REGENERATION:
                    if (_enable)
                    {
                        AppendCoroutine(UpdateHpRegeneration(++currentHpRegeneId), ePauseType.SYSTEM);
                    }
                    break;
                case eAbnormalState.TP_REGENERATION:
                    if (_enable)
                    {
                        AppendCoroutine(UpdateTpRegeneration(++currentTpRegeneId), ePauseType.SYSTEM);
                        break;
                    }
                    break;
                case eAbnormalState.PAUSE_ACTION:
                    BattleSpineController currentSpineCtrl2 = GetCurrentSpineCtrl();
                    currentSpineCtrl2.IsColorPauseAction = _enable;
                    if (_enable)
                    {
                        if (CurrentState != ActionState.DAMAGE)
                            SetState(ActionState.DAMAGE);
                        currentSpineCtrl2.IsStopState = true;
                        setMotionResume();
                        PlayAnime(eSpineCharacterAnimeId.IDLE, MotionPrefix);
                        setMotionPause();
                        isContinueIdleForPauseAction = true;
                        specialSleepStatus = eSpecialSleepStatus.INVALID;
                        break;
                    }
                    if (!IsUnableActionState() && !_switch)
                    {
                        isContinueIdleForPauseAction = false;
                        specialSleepStatus = eSpecialSleepStatus.INVALID;
                        currentSpineCtrl2.IsPlayAnimeBattle = false;
                        currentSpineCtrl2.IsStopState = false;
                        currentSpineCtrl2.Resume();
                        SetDirectionAuto();
                        break;
                    }
                    shiftAbnormalColor();
                    break;
            }*/
            switch (_abnormalState)
            {
                case eAbnormalState.SLOW:
                    if (!_enable)
                    {
                        if (!IsUnableActionState())
                        {
                            GetCurrentSpineCtrl().Resume();
                        }
                        updateEffectForSpeedAbnormalState(_isEnable: false);
                    }
                    break;
                case eAbnormalState.HASTE:
                    if (!_enable)
                    {
                        if (!IsUnableActionState() && !m_bPause)
                        {
                            GetCurrentSpineCtrl().Resume();
                        }
                        updateEffectForSpeedAbnormalState(_isEnable: false);
                    }
                    break;
                case eAbnormalState.PARALYSIS:
                case eAbnormalState.FREEZE:
                case eAbnormalState.CHAINED:
                case eAbnormalState.STUN:
                case eAbnormalState.DETAIN:
                case eAbnormalState.FAINT:
                case eAbnormalState.NPC_STUN:
                case eAbnormalState.CRYSTALIZE:
                case eAbnormalState.STUN2:
                    if (_enable)
                    {
                        if (CurrentState != ActionState.DAMAGE)
                        {
                            SetState(ActionState.DAMAGE);
                        }
                    }
                    else if (!IsUnableActionState() && !_switch)
                    {
                        BattleSpineController currentSpineCtrl2 = GetCurrentSpineCtrl();
                        if (currentSpineCtrl2.AnimationName == currentSpineCtrl2.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE))
                        {
                            currentSpineCtrl2.IsPlayAnimeBattle = false;
                            currentSpineCtrl2.IsStopState = false;
                        }
                        setMotionResume();
                        isContinueIdleForPauseAction = false;
                    }
                    break;
                case eAbnormalState.SLEEP:
                    {
                        BattleSpineController currentSpineCtrl3 = GetCurrentSpineCtrl();
                        if (_enable)
                        {
                            bool isDamageAnimBeforeSleep = currentSpineCtrl3.AnimationName == currentSpineCtrl3.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix) || currentSpineCtrl3.AnimationName == currentSpineCtrl3.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, -1, PartsMotionPrefix);
                            if (CurrentState != ActionState.DAMAGE)
                            {
                                SetState(ActionState.DAMAGE, 0, 0, _quiet: true);
                            }
                            if (!IsUnableActionState(eAbnormalState.SLEEP) && currentSpineCtrl3.HasSpecialSleepAnimatilon(MotionPrefix) && !currentSpineCtrl3.CheckPlaySpecialSleepAnimeExceptRelease(MotionPrefix))
                            {
                                specialSleepStatus = eSpecialSleepStatus.START;
                                battleManager.AppendCoroutine(playSleepAnime(isDamageAnimBeforeSleep, abnormalStateCategoryDataDictionary[eAbnormalStateCategory.SLEEP].IsDamageRelease), ePauseType.IGNORE_BLACK_OUT);
                            }
                        }
                        else if (!IsUnableActionState() && !_switch)
                        {
                            isContinueIdleForPauseAction = false;
                            if (abnormalStateCategoryDataDictionary[eAbnormalStateCategory.SLEEP].IsReleasedByDamage && battleManager.ChargeSkillTurn != 0)
                            {
                                isDamageReleaseSpecialSleepAnimForUnionBurst = true;
                            }
                        }
                        break;
                    }
                case eAbnormalState.STONE:
                    GetCurrentSpineCtrl().IsColorStone = _enable;
                    if (_enable)
                    {
                        if (CurrentState != ActionState.DAMAGE)
                        {
                            SetState(ActionState.DAMAGE);
                        }
                        if (isSpecialSleepStatus)
                        {
                            isPlayDamageAnimForAbnormal = true;
                        }
                        specialSleepStatus = eSpecialSleepStatus.INVALID;
                    }
                    else
                    {
                        if (!IsUnableActionState() && !_switch && isContinueIdleForPauseAction)
                        {
                            GetCurrentSpineCtrl().IsPlayAnimeBattle = false;
                            isContinueIdleForPauseAction = false;
                        }
                        shiftAbnormalColor();
                    }
                    break;
                case eAbnormalState.REGENERATION:
                case eAbnormalState.REGENERATION2:
                    if (_enable)
                    {
                        AppendCoroutine(UpdateHpRegeneration(_abnormalState), ePauseType.SYSTEM);
                    }
                    break;
                case eAbnormalState.TP_REGENERATION:
                case eAbnormalState.TP_REGENERATION2:
                    if (_enable)
                    {
                        AppendCoroutine(UpdateTpRegeneration(_abnormalState), ePauseType.SYSTEM);
                    }
                    break;
                case eAbnormalState.POISON:
                case eAbnormalState.BURN:
                case eAbnormalState.CURSE:
                case eAbnormalState.NO_EFFECT_SLIP_DAMAGE:
                case eAbnormalState.VENOM:
                case eAbnormalState.HEX:
                case eAbnormalState.COMPENSATION:
                case eAbnormalState.POISON2:
                case eAbnormalState.CURSE2:
                    if (_enable)
                    {
                        AppendCoroutine(UpdateSlipDamage(_abnormalState, ++slipDamageIdDictionary[abnormalStateCategory]), ePauseType.SYSTEM);
                    }
                    break;
                case eAbnormalState.POISON_BY_BEHAVIOUR:
                    if (_enable)
                    {
                        damageByBehaviourDictionary[_abnormalState] = delegate (bool _isForce)
                        {
                            damageByBehaviour(_abnormalState, _isForce);
                        };
                    }
                    break;
                case eAbnormalState.MIFUYU:
                    if (!_enable)
                    {
                    }
                    break;
                case eAbnormalState.CONVERT:
                case eAbnormalState.CONFUSION:
                case eAbnormalState.CONFUSION2:
                    {
                        if (!IsAbnormalState(eAbnormalState.PAUSE_ACTION))
                        {
                            SetDirectionAuto();
                        }
                        ActionState currentState = CurrentState;
                        if ((uint)(currentState - 1) <= 2u)
                        {
                            CancelByConvert = true;
                            idleStartAfterWaitFrame = (float)m_fCastTimer <= DeltaTimeForPause;
                        }
                        if ((long)Hp > 0 && !IsUnableActionState() && CurrentState != ActionState.DAMAGE)
                        {
                            SetState(ActionState.IDLE);
                        }
                        break;
                    }
                case eAbnormalState.DECOY:
                    {
                        UnitCtrl unitCtrl = (IsOther ? battleManager.DecoyEnemy : battleManager.DecoyUnit);
                        if (_enable)
                        {
                            if (unitCtrl != null && unitCtrl != this)
                            {
                                unitCtrl.EnableAbnormalState(eAbnormalState.DECOY, _enable: false);
                            }
                            if (IsOther)
                            {
                                battleManager.DecoyEnemy = this;
                            }
                            else
                            {
                                battleManager.DecoyUnit = this;
                            }
                        }
                        else if (unitCtrl == this)
                        {
                            if (IsOther)
                            {
                                battleManager.DecoyEnemy = null;
                            }
                            else
                            {
                                battleManager.DecoyUnit = null;
                            }
                        }
                        break;
                    }
                case eAbnormalState.PAUSE_ACTION:
                    {
                        BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
                        currentSpineCtrl.IsColorPauseAction = _enable;
                        if (_enable)
                        {
                            if (CurrentState != ActionState.DAMAGE)
                            {
                                SetState(ActionState.DAMAGE);
                            }
                            currentSpineCtrl.IsStopState = true;
                            setMotionResume();
                            PlayAnime(eSpineCharacterAnimeId.IDLE, MotionPrefix);
                            setMotionPause();
                            isContinueIdleForPauseAction = true;
                            specialSleepStatus = eSpecialSleepStatus.INVALID;
                        }
                        else if (!IsUnableActionState() && !_switch)
                        {
                            isContinueIdleForPauseAction = false;
                            currentSpineCtrl.IsPlayAnimeBattle = false;
                            currentSpineCtrl.IsStopState = false;
                            currentSpineCtrl.Resume();
                            SetDirectionAuto();
                        }
                        else
                        {
                            if (IsAbnormalState(eAbnormalState.SLEEP) && GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(MotionPrefix) && currentSpineCtrl.IsPlayAnimeBattle)
                            {
                                currentSpineCtrl.IsPlayAnimeBattle = false;
                            }
                            shiftAbnormalColor();
                        }
                        break;
                    }
            }

            eStateIconType iDAFJHFJKOL = (abnormalStateCategoryData.ShowsIcon ? ABNORMAL_CONST_DATA[_abnormalState].IconType : eStateIconType.NONE);
            OnChangeState.Call(this, iDAFJHFJKOL, _enable);
            NoSkipOnChangeAbnormalState?.Invoke(this, ABNORMAL_CONST_DATA[_abnormalState].IconType,
                _enable, abnormalStateCategoryDataDictionary[abnormalStateCategory].Duration, describe);
            CallBackAbnormalStateChanged(abnormalStateCategoryDataDictionary[abnormalStateCategory]);
            if (_enable || _switch)
                return;
            battleManager.RestartAbnormalStateField(this, _abnormalState);
        }

        private void shiftAbnormalColor()
        {
            if (!IsUnableActionState())
                return;
            if (IsAbnormalState(eAbnormalState.STONE))
            {
                GetCurrentSpineCtrl().IsColorStone = true;
            }
            else
            {
                if (!IsAbnormalState(eAbnormalState.PAUSE_ACTION))
                    return;
                GetCurrentSpineCtrl().IsColorPauseAction = true;
            }
        }

        private IEnumerator playSleepAnime(bool _isDamageAnimBeforeSleep, bool _isDamageRelease)
        {
            BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
            if (!currentSpineCtrl.HasSpecialSleepAnimatilon(MotionPrefix) || currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 1, 0))
            {
                yield break;
            }
            if (specialSleepStatus != 0)
            {
                specialSleepStatus = eSpecialSleepStatus.INVALID;
                yield break;
            }
            if (_isDamageAnimBeforeSleep)
            {
                currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 1, 0);
                specialSleepStatus = eSpecialSleepStatus.LOOP;
                yield break;
            }
            currentSpineCtrl.IsStopState = true;
            currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 0, 0, _playLoop: false);
            currentSpineCtrl.Resume();
            string sleepStartAnimName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 0, 0);
            TrackEntry trackEntry = currentSpineCtrl.state.GetCurrent(0);
            specialSleepStatus = eSpecialSleepStatus.WAIT_START_END;
            BattleSpineController currentSpineCtrl2;
            while (true)
            {
                currentSpineCtrl2 = GetCurrentSpineCtrl();
                if ((long)Hp <= 0)
                {
                    currentSpineCtrl2.IsStopState = false;
                    yield break;
                }
                if (!currentSpineCtrl2.HasSpecialSleepAnimatilon(MotionPrefix))
                {
                    specialSleepStatus = eSpecialSleepStatus.INVALID;
                    yield break;
                }
                if (specialSleepStatus == eSpecialSleepStatus.LOOP)
                {
                    yield break;
                }
                if (specialSleepStatus != eSpecialSleepStatus.WAIT_START_END)
                {
                    specialSleepStatus = eSpecialSleepStatus.INVALID;
                    if (!IsAbnormalState(eAbnormalState.PAUSE_ACTION))
                    {
                        currentSpineCtrl2.IsStopState = false;
                    }
                    yield break;
                }
                if (!IsAbnormalState(eAbnormalState.SLEEP))
                {
                    currentSpineCtrl2.IsStopState = false;
                    specialSleepStatus = eSpecialSleepStatus.INVALID;
                    if (_isDamageRelease)
                    {
                        PlayAnime(eSpineCharacterAnimeId.DAMEGE, MotionPrefix, -1, -1, _isLoop: false, null, _quiet: false, 0f, _ignoreBlackout: true);
                    }
                    yield break;
                }
                if (CurrentState != ActionState.DAMAGE)
                {
                    currentSpineCtrl2.IsStopState = false;
                    specialSleepStatus = eSpecialSleepStatus.INVALID;
                    yield break;
                }
                if (currentSpineCtrl2.AnimeName != sleepStartAnimName)
                {
                    if (!currentSpineCtrl2.HasSpecialSleepAnimatilon(MotionPrefix))
                    {
                        currentSpineCtrl2.IsStopState = false;
                        specialSleepStatus = eSpecialSleepStatus.INVALID;
                    }
                    bool flag = currentSpineCtrl2.AnimationName == currentSpineCtrl2.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix) || currentSpineCtrl2.AnimationName == currentSpineCtrl2.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, -1, PartsMotionPrefix);
                    if (battleManager.ChargeSkillTurn == eChargeSkillTurn.NONE)
                    {
                        if (flag)
                        {
                            currentSpineCtrl2.PlayAnime(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 1, 0);
                            specialSleepStatus = eSpecialSleepStatus.LOOP;
                        }
                        else if (currentSpineCtrl2.AnimeName != currentSpineCtrl2.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 1, 0))
                        {
                            currentSpineCtrl2.IsStopState = false;
                        }
                        yield break;
                    }
                    if (flag)
                    {
                        if (_isDamageRelease)
                        {
                            currentSpineCtrl2.IsStopState = false;
                            specialSleepStatus = eSpecialSleepStatus.INVALID;
                        }
                        else
                        {
                            specialSleepStatus = eSpecialSleepStatus.LOOP;
                        }
                        yield break;
                    }
                }
                if (trackEntry.IsComplete)
                {
                    break;
                }
                yield return null;
            }
            if (!currentSpineCtrl2.HasSpecialSleepAnimatilon(MotionPrefix))
            {
                currentSpineCtrl2.IsStopState = false;
                specialSleepStatus = eSpecialSleepStatus.INVALID;
            }
            else
            {
                currentSpineCtrl2.PlayAnime(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 1, 0);
                currentSpineCtrl2.Resume();
                specialSleepStatus = eSpecialSleepStatus.LOOP;
            }
        }

        private void releaseSleepAnime()
        {
            BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
            if (!currentSpineCtrl.HasSpecialSleepAnimatilon(MotionPrefix))
            {
                currentSpineCtrl.IsPlayAnimeBattle = false;
                specialSleepStatus = eSpecialSleepStatus.INVALID;
                return;
            }
            if (specialSleepStatus != eSpecialSleepStatus.LOOP)
            {
                currentSpineCtrl.IsPlayAnimeBattle = false;
                specialSleepStatus = eSpecialSleepStatus.INVALID;
                return;
            }
            if (abnormalStateCategoryDataDictionary[eAbnormalStateCategory.SLEEP].IsReleasedByDamage)
            {
                currentSpineCtrl.IsPlayAnimeBattle = false;
                specialSleepStatus = eSpecialSleepStatus.INVALID;
                PlayAnime(eSpineCharacterAnimeId.DAMEGE, MotionPrefix, -1, -1, _isLoop: false, null, _quiet: false, 0f, _ignoreBlackout: true);
                return;
            }
            OnDamageForSpecialSleepRelease = delegate (bool _byAttack)
            {
                if (_byAttack)
                {
                    GetCurrentSpineCtrl().IsPlayAnimeBattle = false;
                    PlayAnime(eSpineCharacterAnimeId.DAMEGE, MotionPrefix, -1, -1, _isLoop: false, null, _quiet: false, 0f, _ignoreBlackout: true);
                    OnDamageForSpecialSleepRelease = null;
                }
            };
            specialSleepStatus = eSpecialSleepStatus.INVALID;
            StartCoroutine(endSleepReleaseAnim());
        }

        private IEnumerator endSleepReleaseAnim()
        {
            BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
            currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 2, 0, _playLoop: false);
            TrackEntry trackEntry = currentSpineCtrl.state.GetCurrent(0);
            while (true)
            {
                BattleSpineController currentSpineCtrl2 = GetCurrentSpineCtrl();
                if (trackEntry.IsComplete || !currentSpineCtrl2.HasSpecialSleepAnimatilon(MotionPrefix) || currentSpineCtrl2.AnimeName != currentSpineCtrl2.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 2, 0))
                {
                    break;
                }
                yield return null;
            }
            OnDamageForSpecialSleepRelease = null;
        }

        private IEnumerator UpdateHpRegeneration(eAbnormalState _regenerationState)
        {
            abnormalStateToCurrentId.TryGetValue(_regenerationState, out var currentId);
            currentId++;
            abnormalStateToCurrentId[_regenerationState] = currentId;
            eAbnormalStateCategory category = GetAbnormalStateCategory(_regenerationState);
            float time = 0f;
            while (true)
            {
                yield return null;
                if (!IsAbnormalState(_regenerationState) || abnormalStateToCurrentId[_regenerationState] != currentId)
                {
                    break;
                }
                time += DeltaTimeForPause;
                if (time > 1f || BattleUtil.Approximately(time, 1f))
                {
                    AbnormalStateCategoryData abnormalStateCategoryData = abnormalStateCategoryDataDictionary[category];
                    SetRecovery((int)abnormalStateCategoryData.MainValue, ((int)abnormalStateCategoryData.SubValue != 1) ? eInhibitHealType.MAGIC : eInhibitHealType.PHYSICS, abnormalStateCategoryData.Source, GetHealDownValue(abnormalStateCategoryData.Source), _isEffect: true, _isRevival: false, _isUnionBurstLifeSteal: false, _isRegenerate: true, _useNumberEffect: true, null, _releaseToad: true);
                    time = 0f;
                }
            }
        }

        public static float GetHealDownValue(UnitCtrl _source)
        {
            if (!_source.IsAbnormalState(eAbnormalState.HEAL_DOWN))
            {
                return 1f;
            }
            return _source.abnormalStateCategoryDataDictionary[eAbnormalStateCategory.HEAL_DOWN].MainValue;
        }

        private IEnumerator UpdateTpRegeneration(eAbnormalState _regenerationState)
        {
            abnormalStateToCurrentId.TryGetValue(_regenerationState, out var currentId2);
            currentId2++;
            abnormalStateToCurrentId[_regenerationState] = currentId2;
            eAbnormalStateCategory category = GetAbnormalStateCategory(_regenerationState);
            float time = 0f;
            while (true)
            {
                yield return null;
                if (!IsAbnormalState(_regenerationState) || abnormalStateToCurrentId[_regenerationState] != currentId2)
                {
                    break;
                }
                time += DeltaTimeForPause;
                if (time > 1f || BattleUtil.Approximately(time, 1f))
                {
                    AbnormalStateCategoryData abnormalStateCategoryData = abnormalStateCategoryDataDictionary[category];
                    ChargeEnergy(eSetEnergyType.BY_USE_SKILL, abnormalStateCategoryData.MainValue, _hasEffect: true, this);
                    time = 0f;
                }
            }
        }

        private IEnumerator UpdateSlipDamage(eAbnormalState _abnormalState, int _slipDamageId)
        {
            eAbnormalStateCategory category = GetAbnormalStateCategory(_abnormalState);
            AbnormalStateCategoryData categoryData = abnormalStateCategoryDataDictionary[category];
            float time = 0f;
            int damage = (int)categoryData.MainValue;
            int incrementDamage = (int)(categoryData.MainValue * categoryData.SubValue / 100f);
            IsReleaseSlipDamageDic.TryGetValue(_abnormalState, out var isReleaseSlipDamage);
            while (true)
            {
                yield return null;
                if (!IsAbnormalState(category) || slipDamageIdDictionary[category] != _slipDamageId)
                {
                    yield break;
                }
                if (isReleaseSlipDamage != null && isReleaseSlipDamage())
                {
                    break;
                }
                time += DeltaTimeForPause;
                if (!(time > 1f) && !BattleUtil.Approximately(time, 1f))
                {
                    continue;
                }
                DamageData damageData = new DamageData
                {
                    Target = GetFirstParts(),
                    Damage = damage,
                    DamageType = DamageData.eDamageType.NONE,
                    Source = categoryData.Source,
                    DamageSoundType = DamageData.eDamageSoundType.SLIP,
                    IsSlipDamage = true,
                    ActionType = eActionType.SLIP_DAMAGE,
                    ExecAbsorber = delegate (int _damage)
                    {
                        if (_damage > categoryData.AbsorberValue)
                        {
                            int result = _damage - categoryData.AbsorberValue;
                            battleManager.SubstructEnemyPoint(categoryData.AbsorberValue);
                            categoryData.AbsorberValue = 0;
                            return result;
                        }
                        categoryData.AbsorberValue -= _damage;
                        battleManager.SubstructEnemyPoint(_damage);
                        return 0;
                    }
                };
                SetDamage(damageData, _byAttack: false, _skill: categoryData.Skill, _actionId: categoryData.ActionId, _onDamageHit: null, _hasEffect: true, _energyAdd: true, _onDefeat: null, _noMotion: false, _damageWeight: 1f, _damageWeightSum: 1f, _upperLimitFunc: null, _energyChargeMultiple: categoryData.EnergyChargeMultiple);
                time = 0f;
                damage += incrementDamage;
            }
            DisableAbnormalStateById(_abnormalState, categoryData.ActionId, _isReleasedByDamage: false);
        }

        private void damageByBehaviour(eAbnormalState _abnormalState, bool _isForce)
        {
            if (IsAbnormalState(_abnormalState) && !IsDead && !isRevivaling && (_isForce || ((CurrentState == ActionState.ATK || CurrentState == ActionState.SKILL) && battleManager.ChargeSkillTurn == eChargeSkillTurn.NONE)))
            {
                AppendCoroutine(damageByBehaviourCoroutine(_abnormalState), ePauseType.SYSTEM);
            }
        }

        private IEnumerator damageByBehaviourCoroutine(eAbnormalState _abnormalState)
        {
            if (IsDead)
            {
                yield break;
            }
            eAbnormalStateCategory abnormalStateCategory = GetAbnormalStateCategory(_abnormalState);
            AbnormalStateCategoryData categoryData = abnormalStateCategoryDataDictionary[abnormalStateCategory];
            int num = (int)categoryData.MainValue;
            DamageData damageData = new DamageData
            {
                Target = GetFirstParts(),
                Damage = num,
                DamageType = DamageData.eDamageType.NONE,
                Source = categoryData.Source,
                DamageSoundType = DamageData.eDamageSoundType.SLIP,
                IsSlipDamage = true,
                ActionType = eActionType.DAMAGE_BY_ATTACK,
                ExecAbsorber = delegate (int _damage)
                {
                    if (_damage > categoryData.AbsorberValue)
                    {
                        int result = _damage - categoryData.AbsorberValue;
                        battleManager.SubstructEnemyPoint(categoryData.AbsorberValue);
                        categoryData.AbsorberValue = 0;
                        return result;
                    }
                    categoryData.AbsorberValue -= _damage;
                    battleManager.SubstructEnemyPoint(_damage);
                    return 0;
                }
            };
            SetDamage(damageData, _byAttack: false, _skill: categoryData.Skill, _actionId: categoryData.ActionId, _onDamageHit: null, _hasEffect: true, _energyAdd: true, _onDefeat: null, _noMotion: false, _damageWeight: 1f, _damageWeightSum: 1f, _upperLimitFunc: null, _energyChargeMultiple: categoryData.EnergyChargeMultiple);
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

        public bool IsAbnormalState(eAbnormalState abnormalState) => m_abnormalState[abnormalState];

        public bool IsNoDamageMotion()
        {
            if (!IsAbnormalState(eAbnormalState.NO_DAMAGE_MOTION))
            {
                return IsAbnormalState(eAbnormalState.NO_DAMAGE_MOTION2);
            }
            return true;
        }
        //public bool IsConfusionOrConvert() => m_abnormalState[eAbnormalState.CONFUSION] || m_abnormalState[eAbnormalState.CONVERT];
        public bool IsConfusionOrConvert()
        {
            if (!m_abnormalState[eAbnormalState.CONFUSION] && !m_abnormalState[eAbnormalState.CONVERT])
            {
                return m_abnormalState[eAbnormalState.CONFUSION2];
            }
            return true;
        }
        public bool IsPoisonState()
        {
            if (!IsAbnormalState(eAbnormalState.POISON) && !IsAbnormalState(eAbnormalState.POISON2))
            {
                return IsAbnormalState(eAbnormalState.POISON_BY_BEHAVIOUR);
            }
            return true;
        }
        public bool IsCurseState()
        {
            if (!IsAbnormalState(eAbnormalState.CURSE))
            {
                return IsAbnormalState(eAbnormalState.CURSE2);
            }
            return true;
        }

        public bool IsStunState()
        {
            if (!IsAbnormalState(eAbnormalState.STUN) && !IsAbnormalState(eAbnormalState.STUN2))
            {
                return IsAbnormalState(eAbnormalState.NPC_STUN);
            }
            return true;
        }
        //public bool IsSlipDamageState() => m_abnormalState[eAbnormalState.BURN] || m_abnormalState[eAbnormalState.POISON] || (m_abnormalState[eAbnormalState.VENOM] || m_abnormalState[eAbnormalState.CURSE]) || (m_abnormalState[eAbnormalState.DETAIN] || m_abnormalState[eAbnormalState.HEX]) || m_abnormalState[eAbnormalState.COMPENSATION];
        public bool IsSlipDamageState()
        {
            if (!m_abnormalState[eAbnormalState.BURN] && !m_abnormalState[eAbnormalState.POISON] && !m_abnormalState[eAbnormalState.VENOM] && !m_abnormalState[eAbnormalState.CURSE] && !m_abnormalState[eAbnormalState.DETAIN] && !m_abnormalState[eAbnormalState.HEX] && !m_abnormalState[eAbnormalState.COMPENSATION] && !m_abnormalState[eAbnormalState.POISON_BY_BEHAVIOUR] && !m_abnormalState[eAbnormalState.POISON2])
            {
                return m_abnormalState[eAbnormalState.CURSE2];
            }
            return true;
        }
        public bool IsBarrierState()
        {
            if (!IsAbnormalState(eAbnormalState.GUARD_ATK) && !IsAbnormalState(eAbnormalState.GUARD_MGC) && !IsAbnormalState(eAbnormalState.DRAIN_ATK) && !IsAbnormalState(eAbnormalState.DRAIN_MGC) && !IsAbnormalState(eAbnormalState.GUARD_BOTH))
            {
                return IsAbnormalState(eAbnormalState.DRAIN_BOTH);
            }
            return true;
        }
        public bool IsAbnormalState(eAbnormalStateCategory abnormalStateCategory)
        {
            return abnormalStateCategoryDataDictionary[abnormalStateCategory].enable;
        }
        public float GetAbnormalStateMainValue(
          eAbnormalStateCategory _abnormalStateCategory) => abnormalStateCategoryDataDictionary[_abnormalStateCategory].MainValue;

        public void DecreaseCountBlind()
        {
            --abnormalStateCategoryDataDictionary[eAbnormalStateCategory.COUNT_BLIND].MainValue;
            if (abnormalStateCategoryDataDictionary[eAbnormalStateCategory.COUNT_BLIND].MainValue != 0.0)
                return;
            EnableAbnormalState(eAbnormalState.COUNT_BLIND, false);
        }

        public float GetAbnormalStateSubValue(
          eAbnormalStateCategory abnormalStateCategory) => abnormalStateCategoryDataDictionary[abnormalStateCategory].SubValue;

        public bool IsUnableActionState()
        {
            if (!IsAbnormalState(eAbnormalState.PARALYSIS) && !IsAbnormalState(eAbnormalState.FREEZE) && !IsAbnormalState(eAbnormalState.SLEEP) && !IsAbnormalState(eAbnormalState.CHAINED) && !IsAbnormalState(eAbnormalState.STUN) && !IsAbnormalState(eAbnormalState.STUN2) && !IsAbnormalState(eAbnormalState.NPC_STUN) && !IsAbnormalState(eAbnormalState.STONE) && !IsAbnormalState(eAbnormalState.DETAIN) && !IsAbnormalState(eAbnormalState.FAINT) && !IsAbnormalState(eAbnormalState.PAUSE_ACTION) && !IsAbnormalState(eAbnormalState.CRYSTALIZE))
            {
                return KnockBackEnableCount > 0;
            }
            return true;
        }

        public bool IsUnableActionState(eAbnormalState _removeCheckState)
        {
            if ((!IsAbnormalState(eAbnormalState.PARALYSIS) || _removeCheckState == eAbnormalState.PARALYSIS) && (!IsAbnormalState(eAbnormalState.FREEZE) || _removeCheckState == eAbnormalState.FREEZE) && (!IsAbnormalState(eAbnormalState.SLEEP) || _removeCheckState == eAbnormalState.SLEEP) && (!IsAbnormalState(eAbnormalState.CHAINED) || _removeCheckState == eAbnormalState.CHAINED) && (!IsAbnormalState(eAbnormalState.STUN) || _removeCheckState == eAbnormalState.STUN) && (!IsAbnormalState(eAbnormalState.STUN2) || _removeCheckState == eAbnormalState.STUN2) && (!IsAbnormalState(eAbnormalState.NPC_STUN) || _removeCheckState == eAbnormalState.NPC_STUN) && (!IsAbnormalState(eAbnormalState.STONE) || _removeCheckState == eAbnormalState.STONE) && (!IsAbnormalState(eAbnormalState.DETAIN) || _removeCheckState == eAbnormalState.DETAIN) && (!IsAbnormalState(eAbnormalState.FAINT) || _removeCheckState == eAbnormalState.FAINT) && (!IsAbnormalState(eAbnormalState.PAUSE_ACTION) || _removeCheckState == eAbnormalState.PAUSE_ACTION) && (!IsAbnormalState(eAbnormalState.CRYSTALIZE) || _removeCheckState == eAbnormalState.CRYSTALIZE))
            {
                return KnockBackEnableCount > 0;
            }
            return true;
        }
        public int OverwriteSpeedDataCount
        {
            get
            {
                return this.overwriteSpeedData.Count;
            }
        }
        public void CureAllAbnormalState()
        {
            for (eAbnormalState eAbnormalState = eAbnormalState.GUARD_ATK; eAbnormalState < eAbnormalState.NUM; ++eAbnormalState)
            {
                if (IsAbnormalState(eAbnormalState))
                    EnableAbnormalState(eAbnormalState, false);
            }
            overlapAbnormalStateIndexList[eAbnormalState.SLOW_OVERLAP].Clear();
            overlapAbnormalStateIndexList[eAbnormalState.HASTE_OVERLAP].Clear();
            damageByBehaviourDictionary.Clear();
            this.overwriteSpeedData.Clear();
            foreach (eStateIconType key in ChargeEnergyByReceiveDamageDictionary.Keys)
            {
                SealData sealData = SealDictionary[key];
                sealData.RemoveSeal(sealData.Count, _isPassiveSeal: true);
            }
            SealData unableStateGuardData = this.UnableStateGuardData;
            if (unableStateGuardData != null)
            {
                unableStateGuardData.RemoveSeal(this.UnableStateGuardData.Count, true);
            }
            ChargeEnergyByReceiveDamageDictionary.Clear();

            for (int index = LifeStealQueueList.Count - 1; index >= 0; --index)
            {
                LifeStealQueueList[index].Dequeue();
                if (LifeStealQueueList[index].Count == 0)
                {
                    LifeStealQueueList.RemoveAt(index);
                    OnChangeState.Call(this, eStateIconType.BUFF_ADD_LIFE_STEAL, false);
                    NoSkipOnChangeAbnormalState?.Invoke(this, eStateIconType.BUFF_ADD_LIFE_STEAL, false, 90, "1次");

                }
            }
            foreach (KeyValuePair<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet> strikeBack in StrikeBackDictionary)
            {
                strikeBack.Value.SetTimeToDie();
                for (int index = 0; index < strikeBack.Value.DataList.Count; ++index)
                {
                    OnChangeState.Call(this, eStateIconType.STRIKE_BACK, false);
                    NoSkipOnChangeAbnormalState?.Invoke(this, eStateIconType.STRIKE_BACK, false, 90, "反击中");

                }
            }
            /*StrikeBackDictionary.Clear();
            foreach (KeyValuePair<eStateIconType, List<KnightGuardData>> knightGuardData in knightGuardDatas)
            {
                if (knightGuardData.Value.Count != 0)
                {
                    OnChangeState.Call(this, knightGuardData.Key, false);
                    MyOnChangeAbnormalState?.Invoke(this, knightGuardData.Key, false, 90, "???");
                    knightGuardData.Value.Clear();
                }
            }*/
            StrikeBackDictionary.Clear();
            ClearKnightGuard();
            ClearAwe();
            DamageSealDataDictionary.Clear();
            DamageOnceOwnerSealDateDictionary.Clear();
            AttackSealDataDictionary.Clear();
            DamageOwnerSealDataDictionary.Clear();
            UbIsDisableByChangePattern = false;
            passiveSealDictionary.Clear();

        }
        public void ClearAwe()
        {
            if (aweDatas.Count != 0)
            {
                OnChangeState.Call(this, eStateIconType.AWE, ADIFIOLCOPN: false);
                aweDatas.Clear();
            }
        }
        public void ClearKnightGuard()
        {
            foreach (KeyValuePair<eStateIconType, List<KnightGuardData>> knightGuardData in knightGuardDatas)
            {
                if (knightGuardData.Value.Count != 0)
                {
                    OnChangeState.Call(this, knightGuardData.Key, false);
                    knightGuardData.Value.Clear();
                }
            }
        }

        public void CureAbnormalState(CureAction.eCureActionType type)
        {
            switch (type)
            {
                case CureAction.eCureActionType.SLOW:
                    EnableAbnormalState(eAbnormalState.SLOW, false);
                    break;
                case CureAction.eCureActionType.HASTE:
                    EnableAbnormalState(eAbnormalState.HASTE, false);
                    break;
                case CureAction.eCureActionType.PARALYSIS:
                    EnableAbnormalState(eAbnormalState.PARALYSIS, false);
                    break;
                case CureAction.eCureActionType.FREEZE:
                    EnableAbnormalState(eAbnormalState.FREEZE, false);
                    break;
                case CureAction.eCureActionType.CHAINED:
                    EnableAbnormalState(eAbnormalState.CHAINED, false);
                    break;
                case CureAction.eCureActionType.SLEEP:
                    EnableAbnormalState(eAbnormalState.SLEEP, false);
                    break;
                case CureAction.eCureActionType.POISONE:
                    EnableAbnormalState(eAbnormalState.POISON, false);
                    break;
                case CureAction.eCureActionType.BURN:
                    EnableAbnormalState(eAbnormalState.BURN, false);
                    break;
                case CureAction.eCureActionType.CURSE:
                    EnableAbnormalState(eAbnormalState.CURSE, false);
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

        /*public void EnableAuraEffect(bool _active)
        {
            if (IsDead)
                return;
            for (int index = 0; index < AuraEffectList.Count; ++index)
            {
                SkillEffectCtrl auraEffect = AuraEffectList[index];
                if (auraEffect != null)
                    auraEffect.SetActive(_active);
            }
        }*/

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
            if (resultMoveDistance == 0.0)
                return;
            Vector3 localPosition = transform.localPosition;
            localPosition.x -= resultMoveDistance;
            resultMoveDistance = 0.0f;
            transform.localPosition = localPosition;
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
            SetLeftDirection(false);
            PlayAnime(eSpineCharacterAnimeId.RUN, MotionPrefix);
            StartCoroutine(updateRunOut());
        }

        private IEnumerator updateRunOut()
        {
            UnitCtrl unitCtrl = this;
            while (true)
            {
                unitCtrl.transform.SetLocalPosX(unitCtrl.transform.localPosition.x + 2400f * Time.deltaTime);
                if (unitCtrl.transform.localPosition.x <= 1550.0)
                    yield return null;
                else
                    break;
            }
        }

        public void PlaySetResult(ResultMotionInfo _combineMotion)
        {
            resultMoveDistance = 0.0f;
            PlayAnime(eSpineCharacterAnimeId.COMBINE_JOY_RESULT, _combineMotion.MotionId, _isLoop: false);
            SortOrder = _combineMotion.Depth;
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

        public void ForceFadeOut() => StartCoroutine(fadeOutCoroutine());

        private IEnumerator fadeOutCoroutine()
        {
            UnitCtrl _self = this;
            BattleSpineController battleSpineController = _self.GetCurrentSpineCtrl();
            battleSpineController.Resume();
            float fAlpha = 1;// battleSpineController.CurColor.a;
            while (true)
            {
                fAlpha -= Time.deltaTime;
                if (fAlpha > 0.0 && !BattleUtil.Approximately(fAlpha, 0.0f))
                {
                   // battleSpineController.CurColor = new Color(1f, 1f, 1f, fAlpha);
                    yield return null;
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

        public Dictionary<BuffParamKind, FloatWithEx> AdditionalBuffDictionary { get; set; } = new Dictionary<BuffParamKind, FloatWithEx>();

        private FloatWithEx getAdditionalBuffDictionary(BuffParamKind _kind)
        {
            FloatWithEx num;
            return (int)(!AdditionalBuffDictionary.TryGetValue(_kind, out num) ? 0 : num);
        }

        public void SetBuffParam(
          BuffParamKind _kind,
          BuffParamKind _resistMissCheckParamKind,
          Dictionary<BasePartsData, FloatWithEx> _value,
          float _time,
          int _skillId,
          UnitCtrl _source,
          bool _despelable,
          eEffectType _effectType,
          bool _isBuff,
          bool _additional,
          bool _isShowIcon,
          int _bonusId = 0,
          Action<string> action = null)
        {
            if (!_isBuff && IsAbnormalState(eAbnormalState.NO_DEBUF))
            {
                SetMissAtk(_source, eMissLogType.DODGE_BY_NO_DAMAGE_MOTION);
                action?.Invoke("MISS");
                return;
            }
            if (!_isBuff && _value.All((KeyValuePair<BasePartsData, FloatWithEx> _val) => (float)_val.Key.GetDebuffResistPercent(_resistMissCheckParamKind) == 100f))
            {
                SetMissAtk(_source, eMissLogType.DODGE_BY_NO_DAMAGE_MOTION);
                return;
            }

            //if (_effectType == eEffectType.COMMON)
            //{ }  //this.CreateBuffDebuffEffect(_skillId, _isBuff, _source);
            string valueStr = "";
            foreach (var i in _value.Values)
            {
                valueStr += i + ",";
            }
            action?.Invoke("对目标添加" + _kind.GetDescription() + (_isBuff ? "BUFF" : "DEBUFF") + ",值" + valueStr + "持续时间" + _time + "秒");
            IEnumerator _cr = UpdateBuffParam(_kind, _value, _time, _skillId, _source, _despelable, ++buffDebuffIndex, _isBuff, _additional, _isShowIcon, _bonusId);

            buffDebuffCoroutineList.Add(_cr);

            if (!_cr.MoveNext())
                return;
            AppendCoroutine(_cr, ePauseType.SYSTEM);

        }

        private bool isStopBuffDebuffEffectTimer;

        private List<IEnumerator> buffDebuffCoroutineList = new List<IEnumerator>();
        private LinkedList<ChangeParameterFieldData> buffDebuffFieldDataList = new LinkedList<ChangeParameterFieldData>();

        // Elements.UnitCtrl
        private void dispelBuffDebuffImmediately()
        {
            isStopBuffDebuffEffectTimer = true;
            for (int num = buffDebuffCoroutineList.Count - 1; num >= 0; num--)
            {
                if (!buffDebuffCoroutineList[num].MoveNext())
                {
                    buffDebuffCoroutineList.RemoveAt(num);
                }
            }
            isStopBuffDebuffEffectTimer = false;

            foreach (var item in buffDebuffFieldDataList.ToArray())
            {
                item.DispelImmediately(this);
            }
        }

        private IEnumerator UpdateBuffParam(
          BuffParamKind _kind,
          Dictionary<BasePartsData, FloatWithEx> _value,
          float _maxTime,
          int _skillId,
          UnitCtrl _source,
          bool _despelable,
          int _buffDebuffId,
          bool _isBuff,
          bool _additional,
          bool _isShowIcon, 
          int _bonusId)
        {
            if (_kind == BuffParamKind.MAX_HP)
            {
                if (maxHpDebufCounter == 0)
                {
                    /*if (maxHpDebufEffectRight == null)
                    {
                        maxHpDebufEffectRight = createMaxHpDebufEffect(_left: false);
                        maxHpDebufEffectLeft = createMaxHpDebufEffect(_left: true);
                    }
                    if (IsLeftDir)
                    {
                        maxHpDebufEffectRight.SetActive(_isActive: false);
                    }
                    else
                    {
                        maxHpDebufEffectLeft.SetActive(_isActive: false);
                    }*/
                }
                maxHpDebufCounter++;
            }
            EnableBuffParam(_kind, _value, true, _source, _isBuff, _additional, _isShowIcon, _maxTime);
            float time = 0.0f;
            bool flag2;
            while (true)
            {
                if (!isStopBuffDebuffEffectTimer)
                {
                    time += DeltaTimeForPause;
                }
                if (time > 1f)
                {
                    buffDebuffSkilIds.Remove(_skillId);
                }
                bool flag = (_isBuff ? (_buffDebuffId <= clearedBuffIndex) : (_buffDebuffId <= clearedDebuffIndex));
                flag2 = _despelable && flag;
                if (time >= _maxTime || IdleOnly || (long)Hp <= 0 || flag2)
                {
                    break;
                }
                yield return null;
            }
            buffDebuffSkilIds.Remove(_skillId);
            if (_bonusId != 0 && (long)Hp > 0)
            {
                //battleManager.DeleteBonusIcon(_bonusId);
            }
            if (time >= _maxTime || flag2 || IdleOnly || _kind != BuffParamKind.MAX_HP)
            {
                EnableBuffParam(_kind, _value, _enable: false, _source, _isBuff, _additional, _isShowIcon, _maxTime);
            }
            else
            {
                OnChangeState.Call(this, eStateIconType.DEBUFF_MAX_HP, ADIFIOLCOPN: false);
                MaxHp = MaxHpAfterPassive;
            }
            if (_kind == BuffParamKind.MAX_HP)
            {
                maxHpDebufCounter--;
                /*if (maxHpDebufCounter == 0)
                {
                    maxHpDebufEffectRight.SetActive(_isActive: false);
                    maxHpDebufEffectLeft.SetActive(_isActive: false);
                }*/
            }
        }

        public void DespeleBuffDebuff(bool _isBuff, AbnormalStateEffectPrefabData _prefabData)
        {
            //this.createBuffDebuffClearEffect(_isBuff, _prefabData);
            if (_isBuff)
            {
                clearedBuffIndex = buffDebuffIndex;
                ClearedBuffFieldIndex = battleManager.MCLFFJEFMIF;
            }
            else
            {
                clearedDebuffIndex = buffDebuffIndex;
                ClearedDebuffFieldIndex = battleManager.MCLFFJEFMIF;
            }

            dispelBuffDebuffImmediately();
            battleManager.QueueUpdateSkillTarget();
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
          BuffParamKind _kind,
          Dictionary<BasePartsData, FloatWithEx> _value,
          bool _enable,
          UnitCtrl _source,
          bool _isBuff,
          bool _additional,
          bool _showsIcon = true,
          float buffTime = 0,
          object extraKey = null)
        {
            if (_value.Values.All((FloatWithEx _val) => _val == 0))
            {
                return;
            }

            extraKey = extraKey ?? _value;
            BuffDebuffConstData buffDebuff = new BuffDebuffConstData
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
            OnChangeState.Call(this, IDAFJHFJKOL, _enable);
            string des = "";
            FloatWithEx MainValue = 0;
            foreach(var value in _value.Values)
            {
                des += value;
                MainValue = value;
            }
            ///added script
            UnitAbnormalStateChangeData stateChangeData = new UnitAbnormalStateChangeData();
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
            stateChangeData.BUFF_Type = _kind;
            NoSkipOnAbnormalStateChange?.Invoke(UnitId, stateChangeData, BattleHeaderController.CurrentFrameCount);
            button?.SetAbnormalIcons(this, IDAFJHFJKOL, _enable,buffTime,des, extraKey);
            ///finish add
            if (IDAFJHFJKOL != eStateIconType.NONE)
            {
                if (_enable)
                {
                    if (_isBuff)
                    {
                        ++buffCounter;
                        execPassiveSeal(PassiveSealAction.ePassiveTiming.BUFF);
                    }
                    else
                    {
                        debuffCounterDictionary[_kind]++;
                        ++debuffCounter;
                    }
                }
                else if (_isBuff)
                {
                    --buffCounter;
                }
                else
                {
                    debuffCounterDictionary[_kind]--;
                    --debuffCounter;
                }
            }
            Dictionary<BasePartsData, FloatWithEx>.Enumerator enumerator = _value.GetEnumerator();
            while (enumerator.MoveNext())
                enumerator.Current.Key.SetBuffDebuff(_enable, enumerator.Current.Value, _kind, _source, battleLog, _additional);
        }
        public bool IsBuffDebuff(BuffParamKind _kind, bool _isBuff)
        {
            if (_isBuff)
            {
                return buffCounterDictionary[_kind] > 0;
            }
            return debuffCounterDictionary[_kind] > 0;
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


        private static void swap<T>(List<T> _list, int i, int j)
        {
            T obj = _list[i];
            _list[i] = _list[j];
            _list[j] = obj;
        }

        public static void quickSortImpl<T>(
          List<T> _array,
          int _left,
          int _right,
          FunctionalComparer<T> _compare)
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
                        swap(_array, num1, num2);
                        ++num1;
                        --num2;
                    }
                    else
                    {
                        if (_left < num2)
                            quickSortImpl(_array, _left, num2, _compare);
                        if (num1 >= _right)
                            return;
                        quickSortImpl(_array, num1, _right, _compare);
                        return;
                    }
                }
                ++num1;
            }
        }
        private float baseX;
        public float BaseX { get => baseX; set { baseX = value; } }

        public int CompareLeft(BasePartsData _a, BasePartsData _b) => _a.GetPosition().x.CompareTo(_b.GetPosition().x);

        public int CompareRight(BasePartsData _a, BasePartsData _b) => _b.GetPosition().x.CompareTo(_a.GetPosition().x);

        public int CompareLifeAscNear(BasePartsData _a, BasePartsData _b) => BattleUtil.Approximately((long)_a.Owner.Hp / (float)(long)_a.Owner.MaxHp, (long)_b.Owner.Hp / (float)(long)_b.Owner.MaxHp) ? CompareDistanceAsc(_a, _b) : CompareLifeAsc(_a, _b);

        public int CompareLifeValueAscSameLeft(BasePartsData _a, BasePartsData _b) => (long)_a.Owner.Hp == (long)_b.Owner.Hp ? CompareLeft(_a, _b) : CompareLifeValueAsc(_a, _b);

        public int CompareLifeValueAscSameRight(BasePartsData _a, BasePartsData _b) => (long)_a.Owner.Hp == (long)_b.Owner.Hp ? CompareRight(_a, _b) : CompareLifeValueAsc(_a, _b);

        public int CompareLifeValueAsc(BasePartsData _a, BasePartsData _b) => _a == null ? (_b != null ? -1 : 0) : (_b != null ? _a.Owner.Hp.CompareTo(_b.Owner.Hp) : 1);

        public int CompareEnergyAscNear(BasePartsData _a, BasePartsData _b) => BattleUtil.Approximately(_a.Owner.Energy, _b.Owner.Energy) ? CompareDistanceAsc(_a, _b) : CompareEnergyAsc(_a, _b);

        public int CompareLifeDecNear(BasePartsData _a, BasePartsData _b) => BattleUtil.Approximately((long)_a.Owner.Hp / (float)(long)_a.Owner.MaxHp, (long)_b.Owner.Hp / (float)(long)_b.Owner.MaxHp) ? CompareDistanceAsc(_a, _b) : CompareLifeDec(_a, _b);

        public int CompareLifeValueDecSameLeft(BasePartsData _a, BasePartsData _b) => (long)_a.Owner.Hp == (long)_b.Owner.Hp ? CompareLeft(_a, _b) : CompareLifeValueDec(_a, _b);

        public int CompareLifeValueDecSameRight(BasePartsData _a, BasePartsData _b) => (long)_a.Owner.Hp == (long)_b.Owner.Hp ? CompareRight(_a, _b) : CompareLifeValueDec(_a, _b);

        public int CompareLifeValueDec(BasePartsData _a, BasePartsData _b) => _a == null ? (_b != null ? -1 : 0) : (_b != null ? _b.Owner.Hp.CompareTo(_a.Owner.Hp) : -1);

        public int CompareEnergyDecNear(BasePartsData _a, BasePartsData _b) => BattleUtil.Approximately(_a.Owner.Energy, _b.Owner.Energy) ? CompareDistanceAsc(_a, _b) : CompareEnergyDec(_a, _b);

        public int CompareBoss(BasePartsData a, BasePartsData b)
        {
            bool flag1 = a.Owner == this;
            bool flag2 = b.Owner == this;
            if (flag1 | flag2)
                return flag1.CompareTo(flag2);
            return a.Owner.IsBoss == b.Owner.IsBoss ? CompareDistanceAsc(a, b) : b.Owner.IsBoss.CompareTo(a.Owner.IsBoss);
        }

        public int CompareShadow(BasePartsData _a, BasePartsData _b)
        {
            bool flag1 = _a.Owner == this;
            bool flag2 = _b.Owner == this;
            if (flag1 | flag2)
                return flag1.CompareTo(flag2);
            return _a.Owner.IsShadow == _b.Owner.IsShadow ? CompareDistanceAsc(_a, _b) : _b.Owner.IsShadow.CompareTo(_a.Owner.IsShadow);
        }

        public int CompareDistanceAsc(BasePartsData a, BasePartsData b)
        {
            float _a = a.GetBottomTransformPosition().x - BaseX;
            float _b = b.GetBottomTransformPosition().x - BaseX;
            return !BattleUtil.Approximately(_a, _b) ? Math.Abs(_a).CompareTo(Math.Abs(_b)) : a.Owner.UnitInstanceId.CompareTo(b.Owner.UnitInstanceId);
        }

        public int CompareDistanceDec(BasePartsData a, BasePartsData b)
        {
            float _a = a.GetBottomTransformPosition().x - BaseX;
            float _b = b.GetBottomTransformPosition().x - BaseX;
            return !BattleUtil.Approximately(_a, _b) ? Math.Abs(_b).CompareTo(Math.Abs(_a)) : a.Owner.UnitInstanceId.CompareTo(b.Owner.UnitInstanceId);
        }

        public static int CompareEnergyAsc(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? a.Owner.Energy.CompareTo(b.Owner.Energy) : 1);

        public static int CompareEnergyDec(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? b.Owner.Energy.CompareTo(a.Owner.Energy) : -1);

        public static int CompareLifeAsc(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? ((long)a.Owner.Hp / (float)(long)a.Owner.MaxHp).CompareTo((long)b.Owner.Hp / (float)(long)b.Owner.MaxHp) : 1);

        public static int CompareLifeDec(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? ((long)b.Owner.Hp / (float)(long)b.Owner.MaxHp).CompareTo((long)a.Owner.Hp / (float)(long)a.Owner.MaxHp) : -1);

        public static int CompareAtkAsc(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? a.GetAtkZero().CompareTo(b.GetAtkZero()) : 1);

        public static int CompareAtkDec(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? b.GetAtkZero().CompareTo(a.GetAtkZero()) : -1);

        public static int CompareMagicStrAsc(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? a.GetMagicStrZero().CompareTo(b.GetMagicStrZero()) : 1);

        public static int CompareMagicStrDec(BasePartsData a, BasePartsData b) => a == null ? (b != null ? -1 : 0) : (b != null ? b.GetMagicStrZero().CompareTo(a.GetMagicStrZero()) : -1);

        public static int CompareEnergyAsc(BasePartsDataEx a, BasePartsDataEx b, int hash) => a == null ? (b != null ? -1 : 0) : (b != null ? a.Energy.Emulate(hash).CompareTo(b.Energy.Emulate(hash)) : 1);

        public static int CompareEnergyDec(BasePartsDataEx a, BasePartsDataEx b, int hash) => a == null ? (b != null ? -1 : 0) : (b != null ? b.Energy.Emulate(hash).CompareTo(a.Energy.Emulate(hash)) : -1);

        public static int CompareLifeAsc(BasePartsDataEx a, BasePartsDataEx b, int hash) => a == null ? (b != null ? -1 : 0) : (b != null ? (a.Hp.Emulate(hash) / a.MaxHp).CompareTo(b.Hp.Emulate(hash) / b.MaxHp) : 1);

        public static int CompareLifeDec(BasePartsDataEx a, BasePartsDataEx b, int hash) => a == null ? (b != null ? -1 : 0) : (b != null ? (b.Hp.Emulate(hash) / b.MaxHp).CompareTo(a.Hp.Emulate(hash) / a.MaxHp) : -1);

        public static int CompareAtkAsc(BasePartsDataEx a, BasePartsDataEx b, int hash) => a == null ? (b != null ? -1 : 0) : (b != null ? a.GetAtkZeroEx.Emulate(hash).CompareTo(b.GetAtkZeroEx.Emulate(hash)) : 1);

        public static int CompareAtkDec(BasePartsDataEx a, BasePartsDataEx b, int hash) => a == null ? (b != null ? -1 : 0) : (b != null ? b.GetAtkZeroEx.Emulate(hash).CompareTo(a.GetAtkZeroEx.Emulate(hash)) : -1);

        public static int CompareMagicStrAsc(BasePartsDataEx a, BasePartsDataEx b, int hash) => a == null ? (b != null ? -1 : 0) : (b != null ? a.GetMagicStrZeroEx.Emulate(hash).CompareTo(b.GetMagicStrZeroEx.Emulate(hash)) : 1);

        public static int CompareMagicStrDec(BasePartsDataEx a, BasePartsDataEx b, int hash) => a == null ? (b != null ? -1 : 0) : (b != null ? b.GetMagicStrZeroEx.Emulate(hash).CompareTo(a.GetMagicStrZeroEx.Emulate(hash)) : -1);


        public int CompareEnergyDecNearForward(BasePartsData _a, BasePartsData _b)
        {
            if (BattleUtil.Approximately(_a.Owner.Energy, _b.Owner.Energy))
            {
                bool flag = _a.GetPosition().x <= GetFirstParts().GetPosition().x == IsLeftDir;
                bool flag2 = _b.GetPosition().x <= GetFirstParts().GetPosition().x == IsLeftDir;
                if (flag == flag2)
                {
                    return CompareDistanceAsc(_a, _b);
                }
                return flag2.CompareTo(flag);
            }
            return CompareEnergyDec(_a, _b);
        }
        public int CompareAtkAscNear(BasePartsData _a, BasePartsData _b) => _a.GetAtkZero() == _b.GetAtkZero() ? CompareDistanceAsc(_a, _b) : CompareAtkAsc(_a, _b);

        public int CompareAtkDecNear(BasePartsData _a, BasePartsData _b) => _a.GetAtkZero() == _b.GetAtkZero() ? CompareDistanceAsc(_a, _b) : CompareAtkDec(_a, _b);

        public int CompareMagicStrAscNear(BasePartsData _a, BasePartsData _b) => _a.GetMagicStrZero() == _b.GetMagicStrZero() ? CompareDistanceAsc(_a, _b) : CompareMagicStrAsc(_a, _b);

        public int CompareMagicStrDecNear(BasePartsData _a, BasePartsData _b) => _a.GetMagicStrZero() == _b.GetMagicStrZero() ? CompareDistanceAsc(_a, _b) : CompareMagicStrDec(_a, _b);

        public List<Queue<int>> LifeStealQueueList { get; private set; }

        public Dictionary<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet> StrikeBackDictionary { get; private set; }

        public Dictionary<UnitCtrl, AccumulativeDamageData> AccumulativeDamageDataDictionary { get; private set; }
        public Dictionary<int, AccumulativeDamageSourceData> AccumulativeDamageSourceDataDictionary { get; private set; }
        public Dictionary<AccumulativeDamageSourceData, AccumulativeDamageData> AccumulativeDamageDataForAllEnemyDictionary { get; private set; }

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

        public Action OnActionByDamage { get; set; }

        public Action OnActionByCritical { get; set; }

        public Action OnActionByDamageOnce { get; set; }
        public Dictionary<eStateIconType, Action> ChargeEnergyByReceiveDamageDictionary
        {
            get;
            set;
        } = new Dictionary<eStateIconType, Action>();
        private List<AweAction.AweData> aweDatas { get; set; } = new List<AweAction.AweData>();

        private IEnumerator aweCoroutine { get; set; }

        public List<ToadData> ToadDatas { get; set; }

        private Dictionary<eStateIconType, List<KnightGuardData>> knightGuardDatas { get; set; } = new Dictionary<eStateIconType, List<KnightGuardData>>();

        private IEnumerator knightGuardCoroutine { get; set; }

        private Dictionary<PassiveSealAction.ePassiveTiming, List<PassiveSealData>> passiveSealDictionary { get; set; } = new Dictionary<PassiveSealAction.ePassiveTiming, List<PassiveSealData>>();

        private IEnumerator debuffDamageUpCoroutine { get; set; }

        private List<DebuffDamageUpData> debuffDamageUpDataList { get; set; } = new List<DebuffDamageUpData>();

        private Dictionary<BuffParamKind, int> debuffCounterDictionary { get; set; } = new Dictionary<BuffParamKind, int>
        {
      {
        BuffParamKind.ATK,
        0
      },
      {
        BuffParamKind.DEF,
        0
      },
      {
        BuffParamKind.MAGIC_STR,
        0
      },
      {
        BuffParamKind.MAGIC_DEF,
        0
      },
      {
        BuffParamKind.DODGE,
        0
      },
      {
        BuffParamKind.PHYSICAL_CRITICAL,
        0
      },
      {
        BuffParamKind.MAGIC_CRITICAL,
        0
      },
      {
        BuffParamKind.ENERGY_RECOVER_RATE,
        0
      },
      {
        BuffParamKind.LIFE_STEAL,
        0
      },
      {
        BuffParamKind.MOVE_SPEED,
        0
      },
      {
        BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE,
        0
      },
      {
        BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE,
        0
      },
      {
                BuffParamKind.ACCURACY,
                0
            },
            {
                BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE,
                0
            },
            {
                BuffParamKind.RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT,
                0
            },
            {
                BuffParamKind.RECEIVE_PHYSICAL_DAMAGE_PERCENT,
                0
            },
            {
                BuffParamKind.RECEIVE_MAGIC_DAMAGE_PERCENT,
                0
            },
            {
                BuffParamKind.MAX_HP,
                0
            }
    };


        public void AddAweData(AweAction.AweData _aweData)
        {
            if (aweDatas.Count == 0)
            {
                OnChangeState.Call(this, eStateIconType.AWE, true);
                NoSkipOnChangeAbnormalState?.Invoke(this, eStateIconType.AWE, true,_aweData.LifeTime,"awe");
            }
            aweDatas.Add(_aweData);
            if (aweCoroutine != null)
                return;
            aweCoroutine = updateAwe();
            AppendCoroutine(aweCoroutine, ePauseType.SYSTEM);
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
                        if (aweData.LifeTime < 0.0)
                        {
                            //if ((UnityEngine.Object)aweData.Effect != (UnityEngine.Object)null)
                            //    aweData.Effect.SetTimeToDie(true);
                            JEOCPILJNAD.aweDatas.RemoveAt(index);
                        }
                    }
                    if (JEOCPILJNAD.aweDatas.Count == 0)
                    {
                        JEOCPILJNAD.OnChangeState.Call(JEOCPILJNAD, eStateIconType.AWE, false);
                        JEOCPILJNAD.NoSkipOnChangeAbnormalState?.Invoke(JEOCPILJNAD, eStateIconType.AWE, false,0,"awe");
                    }
                    yield return null;
                }
                yield return null;
            }
        }

        public float CalcAweValue(bool _isUnionBurst, bool _isAttack)
        {
            float a = 1f;
            if (aweDatas.Count == 0)
                return a;
            for (int index = aweDatas.Count - 1; index >= 0; --index)
            {
                AweAction.AweData aweData = aweDatas[index];
                bool flag = false;
                switch (aweData.AweType)
                {
                    case AweAction.eAweType.UB_ONLY:
                        if (!_isAttack && _isUnionBurst)
                        {
                            flag = true;
                        }
                        break;
                    case AweAction.eAweType.UB_AND_SKILL:
                        if (!_isAttack)
                        {
                            flag = true;
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
                            aweDatas.RemoveAt(index);
                        }
                    }
                }
            }
            if (aweDatas.Count == 0)
            {
                OnChangeState.Call(this, eStateIconType.AWE, false);
                NoSkipOnChangeAbnormalState?.Invoke(this, eStateIconType.AWE, false,90,"awe");
            }
            return Mathf.Max(a, 0.0f);
        }

        public void AddKnightGuard(KnightGuardData _knightGuardData)
        {
            List<KnightGuardData> knightGuardDataList = null;
            if (!knightGuardDatas.TryGetValue(_knightGuardData.StateIconType, out knightGuardDataList))
            {
                knightGuardDataList = new List<KnightGuardData>();
                knightGuardDatas.Add(_knightGuardData.StateIconType, knightGuardDataList);
            }
            if (knightGuardDataList.Count == 0)
            {
                OnChangeState.Call(this, _knightGuardData.StateIconType, true);
                NoSkipOnChangeAbnormalState?.Invoke(this, _knightGuardData.StateIconType, true,90,"???");
            }
            knightGuardDataList.Add(_knightGuardData);
            if (knightGuardCoroutine != null)
                return;
            knightGuardCoroutine = updateKnightGuard();
            AppendCoroutine(knightGuardCoroutine, ePauseType.SYSTEM);
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
                            if (knightGuardData2.LifeTime < 0.0)
                            {
                                knightGuardData1.Value.RemoveAt(index);
                                //if ((UnityEngine.Object)knightGuardData2.Effect != (UnityEngine.Object)null)
                                 //   knightGuardData2.Effect.SetTimeToDie(true);
                            }
                        }
                        if (knightGuardData1.Value.Count == 0)
                        {
                            JEOCPILJNAD.OnChangeState.Call(JEOCPILJNAD, knightGuardData1.Key, false);
                            JEOCPILJNAD.NoSkipOnChangeAbnormalState?.Invoke(JEOCPILJNAD, knightGuardData1.Key, false,90,"???");
                        }
                    }
                }
                yield return null;
            }
        }

        public bool ExecKnightGuard()
        {
            bool flag = false;
            foreach (KeyValuePair<eStateIconType, List<KnightGuardData>> knightGuardData1 in knightGuardDatas)
            {
                Action DMFGKJIEEBF = null;
                foreach (KnightGuardData knightGuardData2 in knightGuardData1.Value)
                {
                    KnightGuardData data = knightGuardData2;
                    if (!flag) _hp = Hp.Max(0);
                    flag = true;
                    DMFGKJIEEBF += () => SetRecovery(data.Value, data.InhibitHealType, this, _isRevival: true);
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
                OnChangeState.Call(this, knightGuardData1.Key, false);
                NoSkipOnChangeAbnormalState?.Invoke(this, knightGuardData1.Key, false, 90, "???");
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
            if (passiveSealDictionary.ContainsKey(_timing))
                passiveSealDictionary[_timing].Add(_data);
            else
                passiveSealDictionary.Add(_timing, new List<PassiveSealData>
                {
          _data
        });
        }

		private void execPassiveSeal(PassiveSealAction.ePassiveTiming _timing)
		{
			List<PassiveSealData> value = null;
			if (!passiveSealDictionary.TryGetValue(_timing, out value))
			{
				return;
			}
			for (int num = value.Count - 1; num >= 0; num--)
			{
				PassiveSealData passiveSealData = value[num];
				if (passiveSealData.LifeTime < 0f)
				{
					value.RemoveAt(num);
				}
				else
				{
					UnitCtrl unitCtrl = null;
					if (passiveSealData.SealTarget == PassiveSealAction.eSealTarget.SOURCE)
					{
						unitCtrl = passiveSealData.Source;
					}
					eStateIconType targetStateIcon = passiveSealData.TargetStateIcon;
					if (!unitCtrl.SealDictionary.ContainsKey(targetStateIcon))
					{
						SealData value2 = new SealData
						{
							Max = passiveSealData.SealNumLimit,
							DisplayCount = passiveSealData.DisplayCount
						};
						unitCtrl.SealDictionary.Add(targetStateIcon, value2);
					}
					else
					{
						unitCtrl.SealDictionary[targetStateIcon].Max = Mathf.Max(passiveSealData.SealNumLimit, unitCtrl.SealDictionary[targetStateIcon].Max);
					}
					SealData sealData = unitCtrl.SealDictionary[targetStateIcon];
					if (passiveSealData.SealNum > 0)
					{
						if (sealData.GetCurrentCount() == 0)
						{
							unitCtrl.OnChangeState.Call(unitCtrl, targetStateIcon, ADIFIOLCOPN: true);
						}
						sealData.AddSeal(passiveSealData.SealDuration, unitCtrl, targetStateIcon, passiveSealData.SealNum);
					}
					else
					{
						sealData.RemoveSeal(-passiveSealData.SealNum, _isPassiveSeal: true);
					}
				}
			}
		}

        public void AddDebuffDamageUpData(DebuffDamageUpData _debuffDamageUpData)
        {
            debuffDamageUpDataList.Add(_debuffDamageUpData);
            if (debuffDamageUpCoroutine != null)
                return;
            debuffDamageUpCoroutine = updateDebuffDamageUp();
            AppendCoroutine(debuffDamageUpCoroutine, ePauseType.SYSTEM);
        }

        private IEnumerator updateDebuffDamageUp()
        {
            while (true)
            {
                for (int index = debuffDamageUpDataList.Count - 1; index >= 0; --index)
                {
                    DebuffDamageUpData debuffDamageUpData = debuffDamageUpDataList[index];
                    debuffDamageUpData.DebuffDamageUpTimer -= DeltaTimeForPause;
                    if (debuffDamageUpData.DebuffDamageUpTimer < 0.0)
                        debuffDamageUpDataList.RemoveAt(index);
                }
                yield return null;
            }
        }

        public float GetDebuffDamageUpValue()
        {
            if (debuffDamageUpDataList.Count == 0)
                return 1f;
            int num1 = 0;
            if (IsAbnormalState(eAbnormalState.SLOW))
                ++num1;
            if (IsAbnormalState(eAbnormalState.PARALYSIS))
                ++num1;
            if (IsAbnormalState(eAbnormalState.FREEZE))
                ++num1;
            if (IsAbnormalState(eAbnormalState.CHAINED))
                ++num1;
            if (IsAbnormalState(eAbnormalState.SLEEP))
                ++num1;
            if (IsAbnormalState(eAbnormalState.STUN))
                ++num1;
            if (IsAbnormalState(eAbnormalState.STONE))
                ++num1;
            if (IsAbnormalState(eAbnormalState.DETAIN))
                ++num1;
            if (IsAbnormalState(eAbnormalState.FAINT))
                ++num1;
            if (IsAbnormalState(eAbnormalState.PAUSE_ACTION))
                ++num1;
            if (IsAbnormalState(eAbnormalState.NPC_STUN))
                ++num1;
            if (IsAbnormalState(eAbnormalState.NO_EFFECT_SLIP_DAMAGE))
                ++num1;
            if (IsAbnormalState(eAbnormalState.POISON))
                ++num1;
            if (IsAbnormalState(eAbnormalState.BURN))
                ++num1;
            if (IsAbnormalState(eAbnormalState.CURSE))
                ++num1;
            if (IsAbnormalState(eAbnormalState.VENOM))
                ++num1;
            if (IsAbnormalState(eAbnormalState.HEX))
                ++num1;
            if (IsAbnormalState(eAbnormalState.COMPENSATION))
                ++num1;
            if (IsAbnormalState(eAbnormalState.CONVERT))
                ++num1;
            if (IsAbnormalState(eAbnormalState.CONFUSION))
                ++num1;
            if (IsAbnormalState(eAbnormalState.MAGIC_DARK))
                ++num1;
            if (IsAbnormalState(eAbnormalState.PHYSICS_DARK))
                ++num1;
            if (IsAbnormalState(eAbnormalState.SILENCE))
                ++num1;
            if (IsAbnormalState(eAbnormalState.UB_SILENCE))
                ++num1;
            if (UbAbnormalDataList.Count > 0)
                ++num1;
            if (IsAbnormalState(eAbnormalState.COUNT_BLIND))
                ++num1;
            if (IsAbnormalState(eAbnormalState.INHIBIT_HEAL))
                ++num1;
            if (IsAbnormalState(eAbnormalState.FEAR))
                ++num1;
            if (aweDatas.Count > 0)
                ++num1;
            if (ToadDatas.Count > 0)
                ++num1;
            if (IsAbnormalState(eAbnormalState.HEAL_DOWN))
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.ATK] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.DEF] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.MAGIC_STR] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.MAGIC_DEF] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.DODGE] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.PHYSICAL_CRITICAL] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.MAGIC_CRITICAL] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.ENERGY_RECOVER_RATE] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.LIFE_STEAL] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.MOVE_SPEED] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.ACCURACY] > 0)
                ++num1;
            if (debuffCounterDictionary[BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE] > 0)
            {
                num1++;
            }
            if (debuffCounterDictionary[BuffParamKind.RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT] > 0)
            {
                num1++;
            }
            if (debuffCounterDictionary[BuffParamKind.RECEIVE_PHYSICAL_DAMAGE_PERCENT] > 0)
            {
                num1++;
            }
            if (debuffCounterDictionary[BuffParamKind.RECEIVE_MAGIC_DAMAGE_PERCENT] > 0)
            {
                num1++;
            }
            if (debuffCounterDictionary[BuffParamKind.MAX_HP] > 0)
                ++num1;
            float num2 = 1f;
            foreach (DebuffDamageUpData debuffDamageUpData in debuffDamageUpDataList)
            {
                float num3 = 0.0f;
                switch (debuffDamageUpData.EffectType)
                {
                    case DebuffDamageUpData.eEffectType.ADD:
                        num3 = Math.Min((float)(1.0 + debuffDamageUpData.DebuffDamageUpValue * (double)num1), debuffDamageUpData.DebuffDamageUpLimitValue);
                        break;
                    case DebuffDamageUpData.eEffectType.SUBTRACT:
                        num3 = Mathf.Max((float)(1.0 - debuffDamageUpData.DebuffDamageUpValue * (double)num1), debuffDamageUpData.DebuffDamageUpLimitValue);
                        break;
                }
                num2 *= num3;
            }
            return num2;
        }

        private Dictionary<BuffParamKind, int> buffCounterDictionary
        {
            get;
            set;
        } = new Dictionary<BuffParamKind, int>
        {
            {
                BuffParamKind.ATK,
                0
            },
            {
                BuffParamKind.DEF,
                0
            },
            {
                BuffParamKind.MAGIC_STR,
                0
            },
            {
                BuffParamKind.MAGIC_DEF,
                0
            },
            {
                BuffParamKind.DODGE,
                0
            },
            {
                BuffParamKind.PHYSICAL_CRITICAL,
                0
            },
            {
                BuffParamKind.MAGIC_CRITICAL,
                0
            },
            {
                BuffParamKind.ENERGY_RECOVER_RATE,
                0
            },
            {
                BuffParamKind.LIFE_STEAL,
                0
            },
            {
                BuffParamKind.MOVE_SPEED,
                0
            },
            {
                BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE,
                0
            },
            {
                BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE,
                0
            },
            {
                BuffParamKind.ACCURACY,
                0
            },
            {
                BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE,
                0
            },
            {
                BuffParamKind.RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT,
                0
            },
            {
                BuffParamKind.RECEIVE_PHYSICAL_DAMAGE_PERCENT,
                0
            },
            {
                BuffParamKind.RECEIVE_MAGIC_DAMAGE_PERCENT,
                0
            },
            {
                BuffParamKind.MAX_HP,
                0
            }
        };


        public Dictionary<eParamType, PassiveActionValue> OwnerPassiveAction { get; private set; }

        public static void InitializeExAndFreeSkill(
          UnitData _unitData,
          ePassiveInitType _passiveInitType,
          int _waveNumber,
          UnitCtrl _unit)
        {
            initializePassiveSkillList(_unitData.ExSkill, _passiveInitType, _waveNumber, _unit);
            initializePassiveSkillList(_unitData.FreeSkill, _passiveInitType, _waveNumber, _unit);
            initializePassiveSkillList(_unitData.MainSkill, _passiveInitType, _waveNumber, _unit);
        }

        private static void initializePassiveSkillList(
          List<SkillLevelInfo> _skillLevelInfoList,
          ePassiveInitType _passiveInitType,
          int _waveNumber,
          UnitCtrl _unit)
        {
            int index = 0;
            for (int count = _skillLevelInfoList.Count; index < count; ++index)
                initializePassiveSkill(_skillLevelInfoList[index], _passiveInitType, _waveNumber, _unit);
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
            switch ((ePassiveSideType)(int)_actionData.target_assignment)
            {
                case ePassiveSideType.ALL:
                    appendPassiveSkill(staticBattleManager.PassiveDic_4[_waveNumber], _actionData, _level);
                    break;
                case ePassiveSideType.ALL_OTHER:
                    if (_actionData.action_detail_1 == 1)
                        break;
                    appendPassiveSkill(staticBattleManager.PassiveDic_3[_waveNumber], _actionData, _level);
                    break;
            }
        }

        private static void initializePassiveActionAllByUnit(
          MasterSkillAction.SkillAction _actionData,
          int _level,
          int _waveNumber)
        {
            switch ((ePassiveSideType)(int)_actionData.target_assignment)
            {
                case ePassiveSideType.ALL:
                    appendPassiveSkill(staticBattleManager.PassiveDic_1[_waveNumber], _actionData, _level);
                    break;
                case ePassiveSideType.ALL_OTHER:
                    if (_actionData.action_detail_1 == 1)
                        break;
                    appendPassiveSkill(staticBattleManager.PassiveDic_2[_waveNumber], _actionData, _level);
                    break;
            }
        }

        private static void initializePassiveActionOwner(
          MasterSkillAction.SkillAction _actionData,
          int _level,
          UnitCtrl _unit)
        {
            ePassiveSideType targetAssignment = (ePassiveSideType)(int)_actionData.target_assignment;
            if (targetAssignment != ePassiveSideType.OWNER)
            {
                int num = (int)(targetAssignment - 1);
            }
            else
            {
                if (!(_unit != null))
                    return;
                appendPassiveSkill(_unit.OwnerPassiveAction, _actionData, _level);
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
                    passiveActionValue.Value += calculateValue(_actionData, _level);
                    break;
                case ePassiveSkillValueType.PERCENTAGE:
                    passiveActionValue.Percentage += calculateValue(_actionData, _level);
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
                    num = (float)(_actionData.action_value_2 + _actionData.action_value_3 * _level);
                    break;
                case ePassiveBuffDebuff.DEBUFF:
                    num = -(float)(_actionData.action_value_2 + _actionData.action_value_3 * _level);
                    break;
            }
            return num;
        }

        public void ApplyPassiveSkillValue(bool _first)
        {
            if (IsSummonOrPhantom)
                return;
            int pokeaebgpib = battleManager.CurrentWave;
            int index = 0;
            foreach (KeyValuePair<eParamType, PassiveActionValue> _passiveActionKV in addPassiveValue(
                         addPassiveValue(
                             IsOther
                                 ? battleManager.PassiveDic_4[pokeaebgpib]
                                 : battleManager.PassiveDic_3[pokeaebgpib],
                             IsOther ? battleManager.PassiveDic_2[index] : battleManager.PassiveDic_1[index]),
                         OwnerPassiveAction))
            {
                switch (_passiveActionKV.Key)
                {
                    case eParamType.HP:
                        if (_first && !IsClanBattleOrSekaiEnemy)
                        {
                            MaxHp = (int)calculatePassiveSkillValue((float)(long)MaxHp, _passiveActionKV);
                            Hp = (long)(int)((long)Hp / (double)(long)StartMaxHP * MaxHp);
                        }
                        continue;
                    case eParamType.ATK:
                        Atk = calculatePassiveSkillValue((float)(int)StartAtk, _passiveActionKV).Floor();
                        continue;
                    case eParamType.DEF:
                        Def = calculatePassiveSkillValue((float)(int)StartDef, _passiveActionKV).Floor();
                        continue;
                    case eParamType.MAGIC_ATK:
                        MagicStr = calculatePassiveSkillValue((float)(int)StartMagicStr, _passiveActionKV).Floor();
                        continue;
                    case eParamType.MAGIC_DEF:
                        MagicDef = calculatePassiveSkillValue((float)(int)StartMagicDef, _passiveActionKV).Floor();
                        continue;
                    case eParamType.PHYSICAL_CRITICAL:
                        PhysicalCritical = (int)calculatePassiveSkillValue((float)(int)StartPhysicalCritical, _passiveActionKV);
                        continue;
                    case eParamType.MAGIC_CRITICAL:
                        MagicCritical = (int)calculatePassiveSkillValue((float)(int)StartMagicCritical, _passiveActionKV);
                        continue;
                    case eParamType.DODGE:
                        Dodge = (int)calculatePassiveSkillValue((float)(int)StartDodge, _passiveActionKV);
                        continue;
                    case eParamType.LIFE_STEAL:
                        LifeSteal = (int)calculatePassiveSkillValue((float)(int)StartLifeSteal, _passiveActionKV);
                        continue;
                    case eParamType.WAVE_HP_RECOVERY:
                        WaveHpRecovery = (int)calculatePassiveSkillValue((float)(int)StartWaveHpRecovery, _passiveActionKV);
                        continue;
                    case eParamType.WAVE_ENERGY_RECOVERY:
                        WaveEnergyRecovery = (int)calculatePassiveSkillValue((float)(int)StartWaveEnergyRecovery, _passiveActionKV);
                        continue;
                    case eParamType.PHYSICAL_PENETRATE:
                        PhysicalPenetrate = (int)calculatePassiveSkillValue((float)(int)StartPhysicalPenetrate, _passiveActionKV);
                        continue;
                    case eParamType.MAGIC_PENETRATE:
                        MagicPenetrate = (int)calculatePassiveSkillValue((float)(int)StartMagicPenetrate, _passiveActionKV);
                        continue;
                    case eParamType.ENERGY_RECOVERY_RATE:
                        EnergyRecoveryRate = (int)calculatePassiveSkillValue((float)(int)StartEnergyRecoveryRate, _passiveActionKV);
                        continue;
                    case eParamType.HP_RECOVERY_RATE:
                        HpRecoveryRate = (int)calculatePassiveSkillValue((float)(int)StartHpRecoveryRate, _passiveActionKV);
                        continue;
                    case eParamType.ENERGY_REDUCE_RATE:
                        EnergyReduceRate = (int)calculatePassiveSkillValue((float)(int)StartEnergyReduceRate, _passiveActionKV);
                        continue;
                    case eParamType.ACCURACY:
                        Accuracy = (int)calculatePassiveSkillValue((float)(int)StartAccuracy, _passiveActionKV);
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
            Dictionary<eParamType, PassiveActionValue> dictionary = new Dictionary<eParamType, PassiveActionValue>(_dic1, new eParamType_DictComparer());
            foreach (KeyValuePair<eParamType, PassiveActionValue> keyValuePair in _dic2)
            {
                PassiveActionValue passiveActionValue1;
                int num1 = dictionary.TryGetValue(keyValuePair.Key, out passiveActionValue1) ? 1 : 0;
                if (num1 == 0)
                    passiveActionValue1 = new PassiveActionValue();
                ref PassiveActionValue local1 = ref passiveActionValue1;
                double num2 = local1.Value;
                PassiveActionValue passiveActionValue2 = keyValuePair.Value;
                double num3 = passiveActionValue2.Value;
                local1.Value = (float)(num2 + num3);
                ref PassiveActionValue local2 = ref passiveActionValue1;
                double percentage1 = local2.Percentage;
                passiveActionValue2 = keyValuePair.Value;
                double percentage2 = passiveActionValue2.Percentage;
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
          KeyValuePair<eParamType, PassiveActionValue> _passiveActionKV) => (float)((double)_value * (_passiveActionKV.Value.Percentage + 100.0) / 100.0) + _passiveActionKV.Value.Value;

        public List<PartsData> BossPartsListForBattle { get; set; }
        private static UnitCtrl _instance;

        public static UnitCtrl Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 寻找已存在的实例
                    _instance = FindObjectOfType<UnitCtrl>();
                    if (_instance == null)
                    {
                      // 如果没有找到，创建一个新的实例
                      var go = new GameObject("UnitCtrl");
                      _instance = go.AddComponent<UnitCtrl>();
                    }
                }
                return _instance;
            }
        }

        public bool IsPartsBoss => BossPartsList.Count > 0;

        public BasePartsData DummyPartsData { get; set; }

        public BasePartsData GetFirstParts(bool _owner = false, float _basePos = 0.0f)
        {
            if (!IsPartsBoss || _owner)
                return DummyPartsData;
            List<PartsData> all = BossPartsListForBattle.FindAll(e => e.GetTargetable());
            PartsData partsData1 = all[0];
            for (int index = 0; index < all.Count; ++index)
            {
                PartsData partsData2 = all[index];
                float _a = Mathf.Abs(partsData2.GetPosition().x - _basePos);
                float _b = Mathf.Abs(partsData1.GetPosition().x - _basePos);
                if (_a < (double)_b || BattleUtil.Approximately(_a, _b))
                    partsData1 = partsData2;
            }
            return partsData1;
        }

        public void AppendBreakLog(UnitCtrl _source)
        {
            battleLog.AppendBattleLog(eBattleLogType.BREAK, 0, 0L, 0L, 0, 0, JELADBAMFKH: _source, LIMEKPEENOB: this);
        }

        /*public void SetCountinuousPartsData(Dictionary<int, ContinuousPartsData> _data)
        {
            for (int index = 0; index < this.BossPartsListForBattle.Count; ++index)
            {
                PartsData partsData = this.BossPartsListForBattle[index];
                ContinuousPartsData continuousPartsData = _data[partsData.Index];
                partsData.BreakPoint = (int)continuousPartsData.BreakPoint;
                partsData.BreakTime = continuousPartsData.BreakTime;
                if ((int)partsData.BreakPoint == 0)
                    partsData.IsBreak = true;
            }
        }*/

        public IEnumerator UpdateBreak(float _duration, PartsData _data)
        {
            _data.BreakTime = _duration;
            while (_data.BreakTime > 0.0)
            {
                if ((long)_data.Owner.Hp <= 0L)
                {
                    yield break;
                }

                _data.BreakTime -= battleManager.DeltaTime_60fps;
                yield return null;
            }
            _data.BreakTime = 0.0f;
            _data.SetBreak(false, null);
        }
        public PartsData MultiBossPartsData
        {
            get;
            set;
        }
        private UnitCtrl KillBonusTarget { get; set; }

        public Action<float> OnLifeAmmountChange { get; set; }
        public Action<float> OnMaxLifeAmmountChange { get; set; }

        public void RecoverDodgeTP(DamageData.eDamageType _damageType, long _damage, eActionType _actionType, long _logBarrierExpectedDamage, long _totalDamageForLogBarrier, UnitCtrl _source, bool _ignoreDef, BasePartsData _target, int _defPenetrate, Func<int, float, int> _upperLimitFunc, Skill _skill, float _energyChargeMultiple)
        {
            if (IdleOnly || IsDivisionSourceForDamage || IsNoDamageMotion() || (IsAbnormalState(eAbnormalState.PHYSICS_DODGE) && _damageType == DamageData.eDamageType.ATK))
            {
                return;
            }
            float num = _damage;
            if (debuffDamageUpDataList.Count > 0)
            {
                num *= GetDebuffDamageUpValue();
            }
            bool flag = false;
            if (_actionType == eActionType.ATTACK)
            {
                if (_damageType == DamageData.eDamageType.ATK && IsAbnormalState(eAbnormalState.LOG_ATK_BARRIR))
                {
                    flag = true;
                }
                else if (_damageType == DamageData.eDamageType.MGC && IsAbnormalState(eAbnormalState.LOG_MGC_BARRIR))
                {
                    flag = true;
                }
                else if (IsAbnormalState(eAbnormalState.LOG_ALL_BARRIR))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                num = _logBarrierExpectedDamage;
            }
            if (_damageType == DamageData.eDamageType.ATK && IsAbnormalState(eAbnormalState.CUT_ATK_DAMAGE))
            {
                float num2 = GetAbnormalStateMainValue(eAbnormalStateCategory.CUT_ATK_DAMAGE) / 100f;
                num *= 1f - num2;
            }
            else if (_damageType == DamageData.eDamageType.MGC && IsAbnormalState(eAbnormalState.CUT_MGC_DAMAGE))
            {
                float num3 = GetAbnormalStateMainValue(eAbnormalStateCategory.CUT_MGC_DAMAGE) / 100f;
                num *= 1f - num3;
            }
            if (IsAbnormalState(eAbnormalState.CUT_ALL_DAMAGE))
            {
                float num4 = GetAbnormalStateMainValue(eAbnormalStateCategory.CUT_ALL_DAMAGE) / 100f;
                num *= 1f - num4;
            }
            if (_actionType == eActionType.ATTACK)
            {
                if (_damageType == DamageData.eDamageType.ATK && IsAbnormalState(eAbnormalState.LOG_ATK_BARRIR))
                {
                    float abnormalStateSubValue = GetAbnormalStateSubValue(eAbnormalStateCategory.LOG_ATK_BARRIR);
                    if ((float)_totalDamageForLogBarrier > abnormalStateSubValue)
                    {
                        float abnormalStateMainValue = GetAbnormalStateMainValue(eAbnormalStateCategory.LOG_ATK_BARRIR);
                        float num5 = (Mathf.Log(((float)_totalDamageForLogBarrier - abnormalStateSubValue) / abnormalStateMainValue + 1f) * abnormalStateMainValue + abnormalStateSubValue) / (float)_totalDamageForLogBarrier;
                        num *= num5;
                    }
                }
                else if (_damageType == DamageData.eDamageType.MGC && IsAbnormalState(eAbnormalState.LOG_MGC_BARRIR))
                {
                    float abnormalStateSubValue2 = GetAbnormalStateSubValue(eAbnormalStateCategory.LOG_MGC_BARRIR);
                    if ((float)_totalDamageForLogBarrier > abnormalStateSubValue2)
                    {
                        float abnormalStateMainValue2 = GetAbnormalStateMainValue(eAbnormalStateCategory.LOG_MGC_BARRIR);
                        float num6 = (Mathf.Log(((float)_totalDamageForLogBarrier - abnormalStateSubValue2) / abnormalStateMainValue2 + 1f) * abnormalStateMainValue2 + abnormalStateSubValue2) / (float)_totalDamageForLogBarrier;
                        num *= num6;
                    }
                }
                if (IsAbnormalState(eAbnormalState.LOG_ALL_BARRIR))
                {
                    float abnormalStateSubValue3 = GetAbnormalStateSubValue(eAbnormalStateCategory.LOG_ALL_BARRIR);
                    if ((float)_totalDamageForLogBarrier > abnormalStateSubValue3)
                    {
                        float abnormalStateMainValue3 = GetAbnormalStateMainValue(eAbnormalStateCategory.LOG_ALL_BARRIR);
                        float num7 = (Mathf.Log(((float)_totalDamageForLogBarrier - abnormalStateSubValue3) / abnormalStateMainValue3 + 1f) * abnormalStateMainValue3 + abnormalStateSubValue3) / (float)_totalDamageForLogBarrier;
                        num *= num7;
                    }
                }
            }
            Tuple<StrikeBackData, List<StrikeBackData>> tuple = searchStrikeBack(new DamageData
            {
                Source = _source,
                ActionType = _actionType,
                DamageType = _damageType
            });
            float num8 = 0f;
            float num9 = 0f;
            if (!_ignoreDef && !flag)
            {
                switch (_damageType)
                {
                    case DamageData.eDamageType.ATK:
                        num8 = _target.GetDefZero();
                        num9 = Mathf.Max(0f, num8 - (float)_defPenetrate);
                        num *= 1f - num9 / (num8 + 100f);
                        break;
                    case DamageData.eDamageType.MGC:
                        num8 = _target.GetMagicDefZero();
                        num9 = Mathf.Max(0f, num8 - (float)_defPenetrate);
                        num *= 1f - num9 / (num8 + 100f);
                        break;
                }
            }
            num = Mathf.Min(num, 999999f);
            ActionState currentState = CurrentState;
            if (currentState == ActionState.DIE)
            {
                return;
            }
            if (_upperLimitFunc != null)
            {
                num = _upperLimitFunc(BattleUtil.FloatToInt(num), 1f);
            }
            if (tuple == null)
            {
                if (_skill != null && _actionType == eActionType.ATTACK)
                {
                    num *= _skill.AweValue;
                }
                ChargeEnergy(eSetEnergyType.BY_SET_DAMAGE, num * (float)skillStackValDmg * battleManager.FEDKJAIEDGI, _hasEffect: false, this, _hasNumberEffect: false, eEffectType.COMMON, _useRecoveryRate: true, _isRegenerate: false, _energyChargeMultiple);
            }
        }
        

        public long SetDamage(
          DamageData _damageData,
          bool _byAttack,
          int _actionId,
          ActionParameter.OnDamageHitDelegate _onDamageHit = null,
          bool _hasEffect = true,
          Skill _skill = null,
          bool _energyAdd = true,
          Action _onDefeat = null,
          bool _noMotion = false,
          float _damageWeight = 1f,
          float _damageWeightSum = 1f,
          Func<int, float, int> _upperLimitFunc = null,
          float _energyChargeMultiple = 1f,
          Dictionary<eStateIconType, List<UnitCtrl>> _usedChargeEnergyByReceiveDamage = null,
          Action<string> callBack = null)
        {
            bool _critical = false;
            var data = new RandomData(_damageData.Source, this, _actionId, 0, _damageData.CriticalRate);
            double random = BattleManager.Random(0.0f, 1f, 
                data);
            if (MyGameCtrl.Instance.tempData.isGuildBattle &&(MyGameCtrl.Instance.tempData.randomData.ForceNoCritical_player && IsOther || MyGameCtrl.Instance.tempData.randomData.ForceNoCritical_enemy && !IsOther))
                random = 1;
            if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.ForceCritical_player && IsOther)
                random = 0;
            //start add
            if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(_damageData.Source, this, _skill, eActionType.ATTACK, BattleHeaderController.CurrentFrameCount, out float fix))
            {
                random = fix;
            }

            if (random <= _damageData.CriticalRate && _damageData.CriticalDamageRate != 0.0)
                _critical = true;
            bool flag = false;
            if (_damageData.ActionType == eActionType.ATTACK)
            {
                if (_damageData.DamageType == DamageData.eDamageType.ATK && IsAbnormalState(eAbnormalState.LOG_ATK_BARRIR))
                    flag = true;
                else if (_damageData.DamageType == DamageData.eDamageType.MGC && IsAbnormalState(eAbnormalState.LOG_MGC_BARRIR))
                    flag = true;
                else if (IsAbnormalState(eAbnormalState.LOG_ALL_BARRIR))
                    flag = true;
            }
            
            if (flag) _critical = _damageData.IsLogBarrierCritical;
            else if (_damageData.CriticalDamageRate != 0.0)
            {
                _damageData.critVar = FloatWithEx.Binomial(_damageData.CriticalRate, _critical,
                    $"rnd:{data.id}:{(int) (_damageData.CriticalRate * 1000)}", data.id);
            }
            else _damageData.critVar = 0f;

            var num1 = SetDamageImpl(_damageData, _byAttack, _onDamageHit, _hasEffect, _skill, _energyAdd, 
                _critical, _onDefeat, _noMotion, _upperLimitFunc, _energyChargeMultiple,callBack,_damageData.CriticalRate, out bool isHit);
            if (_damageData.Target is PartsData)
            {
                if (!_damageData.IsSlipDamage)
                {
                    (_damageData.Target as PartsData).SetDamage(num1, _damageData.Source);
                }
                else
                {
                    for (int index = 0; index < BossPartsListForBattle.Count; ++index)
                        BossPartsListForBattle[index].SetDamage(num1, _damageData.Source);
                }
            }
            //if (this.MultiBossPartsData != null)
             //   this.MultiBossPartsData.SetDamage((int)num1, _damageData.Source);
            if (_damageData.Source != null && _damageData.ActionType != eActionType.DESTROY && _damageData.ActionType != eActionType.ATTACK_FIELD && (_skill == null || _skill.IsLifeStealEnabled))
            {
                int lifeSteal = _damageData.LifeSteal;
                if (_skill != null)
                    lifeSteal += _skill.LifeSteal;
                if (lifeSteal > 0)
                {
                    var num2 = num1 * (float)lifeSteal / (float)(lifeSteal + Level + 100);
                    if (num2 != 0)
                    {   //_damageData.Source.SetRecovery(num2, _damageData.DamageType == DamageData.eDamageType.MGC ? UnitCtrl.eInhibitHealType.MAGIC : UnitCtrl.eInhibitHealType.PHYSICS, _damageData.Source, false, _isUnionBurstLifeSteal: this.battleManager.BlackOutUnitList.Contains(_damageData.Source));
                        _damageData.Source.SetRecovery(num2, _damageData.DamageType == DamageData.eDamageType.MGC ? eInhibitHealType.MAGIC : eInhibitHealType.PHYSICS, _damageData.Source, GetHealDownValue(_damageData.Source), false, _isUnionBurstLifeSteal: battleManager.BlackOutUnitList.Contains(_damageData.Source));
                    }
                }
            }
            /*if ((_damageData.Source != null && _damageData.Source.IsOther != IsOther) || _damageData.StrikeBackSource != null)
            {
                UnitCtrl unitCtrl = ((_damageData.Source != null) ? _damageData.Source : _damageData.StrikeBackSource);
                if (unitCtrl.IsSummonOrPhantom || unitCtrl.IsDivision)
                    unitCtrl = unitCtrl.SummonSource;
                //if (unitCtrl.UnitDamageInfo != null)
                //    unitCtrl.UnitDamageInfo.SetDamage((int)((long)unitCtrl.UnitDamageInfo.damage + num1));
            }*/
            accumulateDamage += num1;
            BattleLogIntreface battleLog = this.battleLog;
            UnitCtrl source1 = _damageData.Source;
            UnitCtrl unitCtrl1 = this;
            int HLIKLPNIOKJ = (int)((_critical ? 1 : 2) * 10 + _damageData.DamageType);
            long KGNFLOPBOMB = (long)num1;
            long hp = (long)Hp;
            int OJHBHHCOAGK = _actionId;
            int PFLDDMLAICG = (int)_damageWeight * 100;
            int PNJFIOPGCIC = (int)_damageWeightSum * 100;
            UnitCtrl JELADBAMFKH = source1;
            UnitCtrl LIMEKPEENOB = unitCtrl1;
            battleLog.AppendBattleLog(eBattleLogType.SET_DAMAGE, HLIKLPNIOKJ, KGNFLOPBOMB, hp, 0, OJHBHHCOAGK, PFLDDMLAICG, PNJFIOPGCIC, JELADBAMFKH, LIMEKPEENOB);
            if (_skill != null && _damageData.Source != null && _skill.SkillId == _damageData.Source.UnionBurstSkillId)
            {
                if (_byAttack)
                    _damageData.Target.PassiveUbIsMagic = _damageData.DamageType == DamageData.eDamageType.MGC;
                _damageData.Target.TotalDamage += (float)num1;
            }
            if (_skill != null && num1 > 0L && !_skill.DamagedPartsList.Contains(_damageData.Target))
                _skill.DamagedPartsList.Add(_damageData.Target);
            if (!this.IsDead)
                if (_damageData.Source != null && DamageSealDataDictionary.ContainsKey(_damageData.Source))
            {
                foreach (KeyValuePair<int, AttackSealData> keyValuePair in _damageData.Source.DamageSealDataDictionary[
                             _damageData.Source])
                {
                    if (keyValuePair.Value.ExecConditionType == AttackSealData.eExecConditionType.DAMAGE &&
                        num1 == 0)
                        continue;
                    if (keyValuePair.Value.ExecConditionType == AttackSealData.eExecConditionType.CRITICAL &&
                        (!_critical || num1 == 0))
                        continue;
                    if (keyValuePair.Value.ExecConditionType == AttackSealData.eExecConditionType.HIT &&
                        !isHit)
                        continue;

                    keyValuePair.Value.AddSeal(this);
                }
            }
            if (_damageData.Source != null && _damageData.Source.DamageOnceOwnerSealDateDictionary.ContainsKey(_damageData.Source) && (num1 > 0L && _skill != null) && !_skill.AlreadyAddAttackSelfSeal)
            {
                foreach (KeyValuePair<int, AttackSealData> keyValuePair in _damageData.Source.DamageOnceOwnerSealDateDictionary[_damageData.Source])
                    keyValuePair.Value.AddSeal(_damageData.Source);
                _skill.AlreadyAddAttackSelfSeal = true;
            }
            if (_damageData.Source != null && _damageData.Source.DamageOwnerSealDataDictionary.ContainsKey(_damageData.Source))
            {
                UnitCtrl source2 = _damageData.Source;
                foreach (AttackSealData attackSealData in _damageData.Source.DamageOwnerSealDataDictionary[source2].Values)
                {
                    if (attackSealData.ExecConditionType == AttackSealData.eExecConditionType.DAMAGE &&
                        num1 == 0)
                        continue;
                    if (attackSealData.ExecConditionType == AttackSealData.eExecConditionType.CRITICAL &&
                        (!_critical || num1 == 0))
                        continue;
                    if (attackSealData.ExecConditionType == AttackSealData.eExecConditionType.HIT &&
                        !isHit)
                        continue;

                    attackSealData.AddSeal(source2);
                }
            }

            if (!this.IsDead)
            {
                if (_damageData.Source != null && _damageData.Source.IsOther != this.IsOther)
                {
                    foreach (var keyValuePair in _damageData.Source.AttackSealDataDictionary)
                    {
                        if (keyValuePair.Value.ExecConditionType == AttackSealData.eExecConditionType.DAMAGE_ONCE &&
                            (num1 == 0 || _skill.AlreadyAddAttackSelfSeal))
                            continue;
                        if (keyValuePair.Value.ExecConditionType == AttackSealData.eExecConditionType.TARGET)
                            continue;
                        if (keyValuePair.Value.ExecConditionType == AttackSealData.eExecConditionType.DAMAGE &&
                            num1 == 0)
                            continue;
                        if (keyValuePair.Value.ExecConditionType == AttackSealData.eExecConditionType.CRITICAL &&
                            (!_critical || num1 == 0))
                            continue;
                        if (keyValuePair.Value.ExecConditionType == AttackSealData.eExecConditionType.HIT &&
                            !isHit)
                            continue;

                        keyValuePair.Value.AddSeal(this);
                    }
                }
            }

            if (_damageData.Source != null && num1 > 0L)
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
            applyChargeEnergyByReceiveDamage(_damageData.ActionType, (int)num1, _damageData.IsAlwaysChargeEnegry, _usedChargeEnergyByReceiveDamage);
            if (num1 > 0L & _critical && _skill.CriticalPartsList != null)
                _skill.CriticalPartsList.Add(_damageData.Target);
            if (_skill != null)
            {
                _skill.TotalDamage += num1;
            }
            return (long)num1;
        }

        private static readonly WaitForSeconds LARGE_NUM_SHOW_TIME = new WaitForSeconds(0.2f);
        public FloatWithEx SetDamageImpl(
          DamageData _damageData,
          bool _byAttack,
          ActionParameter.OnDamageHitDelegate _onDamageHit,
          bool _hasEffect,
          Skill _skill,
          bool _energyAdd,
          bool _critical,
          Action _onDefeat,
          bool _noMotion,
          Func<int, float, int> _upperLimitFunc,
          float _energyChargeMultiple,
          Action<string> callBack,
          float criticalRate,
          out bool isHit)
        {
            isHit = true;
            if (IdleOnly || IsDivisionSourceForDamage && !_damageData.IsDivisionDamage)
            {
                callBack?.Invoke("伤害无效,目标不是可攻击状态");
                isHit = false;
                return 0f;
            }
            //if (this.battleManager.GetPurpose() == eHatsuneSpecialPurpose.SHIELD && this.IsBoss)
            //    this.battleManager.SubstructEnemyPoint(1);
            execPassiveSeal(PassiveSealAction.ePassiveTiming.DAMAGED);
            if (IsNoDamageMotion())
            {
                callBack?.Invoke("伤害无效，目标处于无敌状态");
                isHit = false;
                return 0f;
            }
            if (IsAbnormalState(eAbnormalState.PHYSICS_DODGE) && _damageData.DamageType == DamageData.eDamageType.ATK)
            {
                SetMissAtk(_damageData.Source, eMissLogType.DODGE_BY_NO_DAMAGE_MOTION, _parts: _damageData.Target);
                callBack?.Invoke("伤害无效，目标处于物理闪避状态");
                isHit = false;
                return 0f;
            }
            var a = _damageData.Damage;
            float num1 = 2f * _damageData.CriticalDamageRate;
            a *= (1 + (num1 - 1) * _damageData.critVar);
            if (debuffDamageUpDataList.Count > 0)
                a *= GetDebuffDamageUpValue();
            bool flag1 = false;
            if (_damageData.ActionType == eActionType.ATTACK)
            {
                if (_damageData.DamageType == DamageData.eDamageType.ATK && IsAbnormalState(eAbnormalState.LOG_ATK_BARRIR))
                    flag1 = true;
                else if (_damageData.DamageType == DamageData.eDamageType.MGC && IsAbnormalState(eAbnormalState.LOG_MGC_BARRIR))
                    flag1 = true;
                else if (IsAbnormalState(eAbnormalState.LOG_ALL_BARRIR))
                    flag1 = true;
            }

            FloatWithEx b = a;

            if (flag1)
            {
                a = _damageData.LogBarrierExpectedDamage;
                b = _damageData.LogBarrierExpectedDamageForEnergyCalc;
            }
            if (_damageData.DamageType == DamageData.eDamageType.ATK && IsAbnormalState(eAbnormalState.CUT_ATK_DAMAGE))
            {
                float num2 = GetAbnormalStateMainValue(eAbnormalStateCategory.CUT_ATK_DAMAGE) / 100f;
                a *= 1f - num2;
                b *= 1f - num2;
            }
            else if (_damageData.DamageType == DamageData.eDamageType.MGC && IsAbnormalState(eAbnormalState.CUT_MGC_DAMAGE))
            {
                float num2 = GetAbnormalStateMainValue(eAbnormalStateCategory.CUT_MGC_DAMAGE) / 100f;
                a *= 1f - num2;
                b *= 1f - num2;
            }
            if (IsAbnormalState(eAbnormalState.CUT_ALL_DAMAGE))
            {
                float num2 = GetAbnormalStateMainValue(eAbnormalStateCategory.CUT_ALL_DAMAGE) / 100f;
                a *= 1f - num2;
                b *= 1f - num2;
            }

            float execBarrier(float total, float main, float sub)
            {
                if (total > sub)
                    return (Mathf.Log(((total - sub) / main + 1.0f)) * main + sub) / total;
                return 1;
            }

            if (_damageData.ActionType == eActionType.ATTACK)
            {
                if (_damageData.DamageType == DamageData.eDamageType.ATK && IsAbnormalState(eAbnormalState.LOG_ATK_BARRIR))
                {
                    float abnormalStateSubValue = GetAbnormalStateSubValue(eAbnormalStateCategory.LOG_ATK_BARRIR);
                    float abnormalStateMainValue = GetAbnormalStateMainValue(eAbnormalStateCategory.LOG_ATK_BARRIR);
                    var t = _damageData.TotalDamageForLogBarrier.Select(x =>
                        execBarrier(x, abnormalStateMainValue, abnormalStateSubValue), $"barrier:{abnormalStateMainValue}:{abnormalStateSubValue}");
                    a *= t;
                    b *= t;
                }
                else if (_damageData.DamageType == DamageData.eDamageType.MGC && IsAbnormalState(eAbnormalState.LOG_MGC_BARRIR))
                {
                    float abnormalStateSubValue = GetAbnormalStateSubValue(eAbnormalStateCategory.LOG_MGC_BARRIR);
                    float abnormalStateMainValue = GetAbnormalStateMainValue(eAbnormalStateCategory.LOG_MGC_BARRIR);
                    var t = _damageData.TotalDamageForLogBarrier.Select(x =>
                        execBarrier(x, abnormalStateMainValue, abnormalStateSubValue), $"barrier:{abnormalStateMainValue}:{abnormalStateSubValue}");
                    a *= t;
                    b *= t;
                }
                if (IsAbnormalState(eAbnormalState.LOG_ALL_BARRIR))
                {
                    float abnormalStateSubValue = GetAbnormalStateSubValue(eAbnormalStateCategory.LOG_ALL_BARRIR);
                    float abnormalStateMainValue = GetAbnormalStateMainValue(eAbnormalStateCategory.LOG_ALL_BARRIR);
                    var t = _damageData.TotalDamageForLogBarrier.Select(x =>
                        execBarrier(x, abnormalStateMainValue, abnormalStateSubValue), $"barrier:{abnormalStateMainValue}:{abnormalStateSubValue}");
                    a *= t;
                    b *= t;
                }
            }
            Tuple<StrikeBackData, List<StrikeBackData>> tuple = searchStrikeBack(_damageData);
            if (_hasEffect)
            {
                OnDamageForUIShake.Call();
                //if (!this.DisableFlash)
                //    this.AppendCoroutine(this.setColorOffsetDefaultWithDelay(), ePauseType.NO_DIALOG);
            }
            if (!_damageData.IgnoreDef && !flag1)
            {
                switch (_damageData.DamageType)
                {
                    case DamageData.eDamageType.ATK:
                        var defZero = _damageData.Target.GetDefZero();
                        var num3 = (defZero - _damageData.DefPenetrate).Max(0);
                        a *= (1.0f - num3 / (defZero + 100.0f));
                        break;
                    case DamageData.eDamageType.MGC:
                        var magicDefZero = _damageData.Target.GetMagicDefZero();
                        var num4 = (magicDefZero - _damageData.DefPenetrate).Max(0);
                        a *= (1.0f - num4 / (magicDefZero + 100.0f));
                        break;
                }
                if (MainManager.Instance.PlayerSetting.tpCalculationChanged)
                {
                    switch (_damageData.DamageType)
                    {
                        case DamageData.eDamageType.ATK:
                            var defZero = _damageData.Target.GetDefZeroForDamagedEnergy();
                            var num3 = (defZero - _damageData.DefPenetrate).Max(0);
                            b *= (1.0f - num3 / (defZero + 100.0f));
                            break;
                        case DamageData.eDamageType.MGC:
                            var magicDefZero = _damageData.Target.GetMagicDefZeroForDamagedEnergy();
                            var num4 = (magicDefZero - _damageData.DefPenetrate).Max(0);
                            b *= (1.0f - num4 / (magicDefZero + 100.0f));
                            break;
                    }
                }
            }

            var num5 = a.Min(999999f);
            b = b.Min(999999f);

            if (CurrentState == ActionState.DIE)
            {
                //this.createDamageEffectFromSetDamageImpl(_damageData, _hasEffect, _skill, _critical, BattleUtil.FloatToInt(num5));
                callBack?.Invoke("伤害无效，目标已经死了");
                return 0f;
            }
            
            if (_upperLimitFunc != null)
                num5 = (float)_upperLimitFunc((int)BattleUtil.FloatToInt(num5), _critical ? num1 : 1f);
            /*if (_damageData.Source != null && _damageData.ActionType != eActionType.FORCE_HP_CHANGE)
            {
                foreach (KeyValuePair<EnchantStrikeBackAction.eStrikeBackEffectType, StrikeBackDataSet> strikeBack1 in StrikeBackDictionary)
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
                                        strikeBack.Exec(_damageData.Source, this, (int)BattleUtil.FloatToInt(num5), () => list.Remove(strikeBack));
                                        OnChangeState.Call(this, eStateIconType.STRIKE_BACK, false);
                                        MyOnChangeAbnormalState?.Invoke(this, eStateIconType.STRIKE_BACK, false,90, "反击中");
                                        callBack?.Invoke("伤害无效，被目标格挡");
                                        return 0f;
                                    }
                                    continue;
                                case StrikeBackData.eStrikeBackType.MAGIC_GUARD:
                                case StrikeBackData.eStrikeBackType.MAGIC_DRAIN:
                                    if (_damageData.DamageType == DamageData.eDamageType.MGC)
                                    {
                                        strikeBack.Exec(_damageData.Source, this, (int)BattleUtil.FloatToInt(num5), () => list.Remove(strikeBack));
                                        OnChangeState.Call(this, eStateIconType.STRIKE_BACK, false);
                                        MyOnChangeAbnormalState?.Invoke(this, eStateIconType.STRIKE_BACK, false, 90, "反击中");

                                        callBack?.Invoke("伤害无效，被目标格挡");
                                        return 0f;
                                    }
                                    continue;
                                case StrikeBackData.eStrikeBackType.BOTH_GUARD:
                                case StrikeBackData.eStrikeBackType.BOTH_DRAIN:
                                    strikeBack.Exec(_damageData.Source, this, (int)BattleUtil.FloatToInt(num5), () => list.Remove(strikeBack));
                                    OnChangeState.Call(this, eStateIconType.STRIKE_BACK, false);
                                    MyOnChangeAbnormalState?.Invoke(this, eStateIconType.STRIKE_BACK, false, 90, "反击中");
                                    callBack?.Invoke("伤害无效，被目标格挡");
                                    return 0f;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }*/
            if (tuple != null)
            {
                StrikeBackData strikeBack = tuple.Item1;
                List<StrikeBackData> strikeBackList = tuple.Item2;
                strikeBack.Exec(_damageData.Source, this, (int)BattleUtil.FloatToInt(num5), delegate
                {
                    strikeBackList.Remove(strikeBack);
                    NoSkipOnChangeAbnormalState?.Invoke(this, eStateIconType.STRIKE_BACK, false, 90, "反击中");
                });
                OnChangeState.Call(this, eStateIconType.STRIKE_BACK, ADIFIOLCOPN: false);
                return 0L;
            }
            if (_skill != null && _damageData.ActionType == eActionType.ATTACK)
                num5 *= _skill.AweValue;
            if (_energyAdd)
            {
                if (MainManager.Instance.PlayerSetting.tpCalculationChanged)
                {
                    var energy = BattleUtil.FloatToInt(b * (float)(skillStackValDmg * 1000.0)) / 1000f;
                    // XXX: 这里删除了slipdamage项，因为其似乎是对受击tp限制的废弃案
                    ChargeEnergy(eSetEnergyType.BY_SET_DAMAGE, energy, false, this, false, eEffectType.COMMON, true, false, _energyChargeMultiple);
                }
                else
                ChargeEnergy(eSetEnergyType.BY_SET_DAMAGE, num5 * (float)skillStackValDmg, _source: this, _hasNumberEffect: false, _multipleValue: _energyChargeMultiple);
            }

            if (num5 <= 0.0 && _damageData.ActionType == eActionType.FORCE_HP_CHANGE)
            {
                if (!battleManager.skipping)
                    createDamageEffectFromSetDamageImpl(_damageData, _hasEffect, _skill, _critical, (int)BattleUtil.FloatToInt(num5));
                callBack?.Invoke("伤害无效，因为伤害为负数");
                return 0f;
            }

            var _fDamage = num5.Max(1f);
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
            FloatWithEx _overRecoverValue = 0;
            if (_damageData.ActionType != eActionType.INHIBIT_HEAL && _damageData.ActionType != eActionType.FORCE_HP_CHANGE)
                this.execBarrier(_damageData, ref _fDamage, ref _overRecoverValue);
            var num6 = BattleUtil.FloatToInt(_fDamage);
            bool flag2 = (long)Hp > 0L;
            int num7 = (long)Hp > MaxHp * 0.200000002980232 ? 1 : 0;
            long hp = (long)Hp;
            if (!IsDamageIgnore(_energyAdd))
            {
                _hp = (Hp - (num6 - _overRecoverValue.Max(0)));
            }
            if (_onDamageHit != null & flag2)
                _onDamageHit(num6);
            //if ((long)this.Hp == 0L && this.battleManager.BattleCategory == eBattleCategory.GLOBAL_RAID && (SekaiUtility.IsBossDead() && this.IsBoss))
            //    this.Hp = (long)1L;
            _hp = Hp.Min(MaxHp);
            var hp2 = Hp;
            
            if (!IsBoss)
            {
                if (Hp > 0)
                    GuildCalculator.Instance.dmglist.Add(new ProbEvent
                    {
                        unit = UnitNameEx,
                        predict = hash => hp2.Emulate(hash) <= 0f,
                        exp = hash => $"{hp2.ToExpression(hash)} <= 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}打死"
                    });
                else
                {
                    GuildCalculator.Instance.dmglist.Add(new ProbEvent
                    {
                        unit = UnitNameEx,
                        predict = hash => hp2.Emulate(hash) > 0f,
                        exp = hash => $"{hp2.ToExpression(hash)} > 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})没被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}打死"
                    });
                }
            }
            else
                GuildCalculator.Instance.bossValues.Add((BattleHeaderController.CurrentFrameCount, Hp));

            if (Hp <= 0)
            {
                if (IsTough || ExecKnightGuard())
                {
                    if (Hp <= 0)
                        Hp = 1;
                }
                else
                {
                    Hp = _hp;
                    _hp = _hp.ZeroCapForHp();
                }
            }
            else
            {
                Hp = Hp;
            }
            
            //if (num7 != 0 && (double)(long)this.Hp < (double)(long)this.MaxHp * 0.200000002980232)
            //    this.playDamageVoice();

            if (this.IsBoss)
            {
                bool flag3 = false;
                if (_skill != null)
                {
                    List<UnitCtrl> unitList = battleManager.UnitList;
                    for (int i = 0; i < unitList.Count; i++)
                    {
                        if (unitList[i].UnionBurstSkillId == _skill.SkillId)
                        {
                            flag3 = true;
                            break;
                        }
                    }
                }

                if (flag3)
                {
                    static IEnumerator moveParticle()
                    {
                        //float rand = UnityEngine.Random.Range(-2f, 2f);
                        yield return null;
                    }

                    IEnumerator playLargeUBEffect()
                    {
                        yield return LARGE_NUM_SHOW_TIME;
                        StartCoroutine(moveParticle());
                    }

                    switch (_damageData.DamegeEffectType)
                    {
                        case eDamageEffectType.NORMAL:
                            break;
                        case eDamageEffectType.COMBO:
                            break;
                        case eDamageEffectType.LARGE:
                            StartCoroutine(playLargeUBEffect());
                            break;
                    }
                }
            }
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
                    if (flag3)
                        SingletonMonoBehaviour<BattleHeaderController>.Instance.BossTotalDamage.SetDamageUB(_damage, _damageData.DamegeEffectType, this);
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
            if (OnDamage != null)
                OnDamage(_byAttack, num6, _critical);


            if (!battleManager.skipping)
            {
                float NormalizedHP = (long)Hp / (float)(long)MaxHp;
                /*
                this.OnLifeAmmountChange.Call<float>(NormalizedHP);*/
                string des;
                //var prob = Hp.Probability(x => x <= 0f);

                des = "受到来自" + (_damageData.Source == null ? "???" : _damageData.Source.UnitName) + "的<color=#FF0000>" + num6 + (_critical ? "</color>点<color=#FFEB00>暴击</color>伤害" : "</color>点伤害");
                if (_damageData.Target?.Owner?.IsPartsBoss ?? false)
                {
                    des = $"<color=#8040FF>部位{_damageData.Target.Index}</color>" + des;
                }
                des += $",剩余HP: {Hp}";

                NoSkipOnLifeChanged?.Invoke(UnitId, Hp, (int)MaxHp, (int)num6, BattleHeaderController.CurrentFrameCount, des, _damageData.Source);
                uIManager.LogMessage(des, eLogMessageType.GET_DAMAGE, this);
                createDamageEffectFromSetDamageImpl(_damageData, _hasEffect, _skill, _critical, (int)num6);
                NoSkipOnDamage?.Invoke(UnitId, _damageData.Source == null ? 0 : _damageData.Source.UnitId, (float)num6, BattleHeaderController.CurrentFrameCount);

            }

            NoSkipOnDamage2?.Invoke(_byAttack, num6, _critical, (long)((float)num6 * (1 - 1/num1)), num6);
            OnDamageForLoopTrigger.Call(_byAttack, (float)num6, _critical);
            OnDamageForLoopRepeat.Call((float)num6);
            OnDamageForDivision.Call(_byAttack, Mathf.Min(hp, (float)num6), _critical);
            OnDamageForSpecialSleepRelease.Call(_byAttack);
            foreach (KeyValuePair<int, Action<bool>> keyValuePair in OnDamageListForChangeSpeedDisableByAttack)
                keyValuePair.Value.Call(_byAttack);
            if (num5 > 0)
            {
                foreach (KeyValuePair<int, Action<bool>> item2 in OnDamageListForSpyDisableByAttack)
                {
                    if (_damageData.ActionType != eActionType.SLIP_DAMAGE)
                    {
                        item2.Value.Call(_byAttack);
                    }
                }
            }
            if (OnHpChange != null)
                OnHpChange(_byAttack, (float)num6, _critical);
            if (OnHpChangeForDamagedHP != null)
            {
                OnHpChangeForDamagedHP(_byAttack, (float)num6, _critical);
            }
            // if (this.battleManager.IsSpecialBattle && this.IsBoss && (this.specialBattlePurposeHp != 0 && (long)this.Hp < (long)this.specialBattlePurposeHp))
            //    this.battleManager.SpecialBattleModeChangeOnHpChange();
            if (!HasUnDeadTime)
            {
                if (!_noMotion)
                    PlayDamageWhenIdle(true, _skill != null && _skill.PauseStopState);
                if ((long)Hp <= 0L && !IsDead && CurrentState < ActionState.DIE)
                {
                    if (_damageData.Source != null & flag2)
                    {
                        KillBonusTarget = _damageData.Source.IsAbnormalState(eAbnormalState.FEAR) ? null : _damageData.Source;
                        _onDefeat.Call();
                    }
                    if (flag2)
                    {
                        if (CurrentState == ActionState.SKILL_1 && _damageData.ActionType == eActionType.INHIBIT_HEAL)
                        {
                            //battleCameraEffect.StopZoomEffect(this);
                            battleManager.StopScaleChange();
                            battleManager.SetBlackoutTimeZero();
                        }
                        SetState(ActionState.DIE);
                    }
                        
                }
            }

            string describe =
                $"对目标{((_damageData.Target?.Owner?.IsPartsBoss ?? false) ? $"<color=#8040FF>部位{_damageData.Target.Index}</color>" : "")}造成<color=#FF0000>{num6}{(_critical ? "</color>点<color=#FFEB00>暴击</color>伤害" : "</color>点伤害")}";

            callBack?.Invoke(describe);
            return num6;
        }
        private bool IsDamageIgnore(bool _energyAdd)
        {
            //if (!battleManager.IsBossInfinityHpMode() || !IsBoss)
            //{
                return false;
            //}
            //return true;
        }
        private Tuple<StrikeBackData, List<StrikeBackData>> searchStrikeBack(DamageData _damageData)
        {
            if (_damageData.Source == null)
            {
                return null;
            }
            if (_damageData.ActionType == eActionType.FORCE_HP_CHANGE)
            {
                return null;
            }
            foreach (StrikeBackDataSet value in StrikeBackDictionary.Values)
            {
                List<StrikeBackData> dataList = value.DataList;
                for (int num = dataList.Count - 1; num >= 0; num--)
                {
                    StrikeBackData strikeBackData = dataList[num];
                    if (!strikeBackData.IsDieing && !strikeBackData.Execed)
                    {
                        switch (strikeBackData.StrikeBackType)
                        {
                            case StrikeBackData.eStrikeBackType.BOTH_GUARD:
                            case StrikeBackData.eStrikeBackType.BOTH_DRAIN:
                                return Tuple.Create(strikeBackData, dataList);
                            case StrikeBackData.eStrikeBackType.MAGIC_GUARD:
                            case StrikeBackData.eStrikeBackType.MAGIC_DRAIN:
                                if (_damageData.DamageType == DamageData.eDamageType.MGC)
                                {
                                    return Tuple.Create(strikeBackData, dataList);
                                }
                                break;
                            case StrikeBackData.eStrikeBackType.PHYSICAL_GUARD:
                            case StrikeBackData.eStrikeBackType.PHYSICAL_DRAIN:
                                if (_damageData.DamageType == DamageData.eDamageType.ATK)
                                {
                                    return Tuple.Create(strikeBackData, dataList);
                                }
                                break;
                        }
                    }
                }
            }
            return null;
        }

        public ProbEvent lastBarrier;
        private void execBarrier(DamageData _damageData, ref FloatWithEx _fDamage, ref FloatWithEx _overRecoverValue)
        {
            //FloatWithEx _fDamage = __fDamage;
            if (IsAbnormalState(eAbnormalState.GUARD_ATK) && _damageData.DamageType == DamageData.eDamageType.ATK)
            {
                var num = _fDamage - abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue;
                if ((double)num > 0.0)
                {
                    EnableAbnormalState(eAbnormalState.GUARD_ATK, false);
                    _fDamage = num;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num.Emulate(hash) <= 0f,
                        exp = hash => $"{num.ToExpression(hash)} <= 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}穿盾（实际未穿盾）"
                    });
                }
                else
                {
                    abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue -= BattleUtil.FloatToInt(_fDamage);
                    _fDamage = 0.0f;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num.Emulate(hash) > 0f,
                        exp = hash => $"{num.ToExpression(hash)} > 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}未穿盾（实际穿盾）"
                    });
                }
            }
            if (IsAbnormalState(eAbnormalState.GUARD_MGC) && _damageData.DamageType == DamageData.eDamageType.MGC)
            {
                var num = _fDamage - abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue;
                if ((double)num > 0.0)
                {
                    EnableAbnormalState(eAbnormalState.GUARD_MGC, false);
                    _fDamage = num;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num.Emulate(hash) <= 0f,
                        exp = hash => $"{num.ToExpression(hash)} <= 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}穿盾（实际未穿盾）"
                    });
                }
                else
                {
                    abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue -= BattleUtil.FloatToInt(_fDamage);
                    _fDamage = 0.0f;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num.Emulate(hash) > 0f,
                        exp = hash => $"{num.ToExpression(hash)} > 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}未穿盾（实际穿盾）"
                    });
                }
            }
            if (IsAbnormalState(eAbnormalState.DRAIN_ATK) && _damageData.DamageType == DamageData.eDamageType.ATK)
            {
                var num1 = _fDamage - abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue;
                if ((double)num1 > 0.0)
                {
                    _overRecoverValue += setRecoveryAndGetOverRecovery((int)(float)BattleUtil.FloatToInt(abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue), this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].Source).Floor();
                    EnableAbnormalState(eAbnormalState.DRAIN_ATK, false);
                    _fDamage = num1;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num1.Emulate(hash) <= 0f,
                        exp = hash => $"{num1.ToExpression(hash)} <= 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}穿盾（实际未穿盾）"
                    });
                }
                else
                {
                    var num2 = BattleUtil.FloatToInt(_fDamage);
                    _overRecoverValue += setRecoveryAndGetOverRecovery((int)num2, this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].Source).Floor();
                    abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_ATK].MainValue -= (float)num2;
                    _fDamage = 0.0f;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num1.Emulate(hash) > 0f,
                        exp = hash => $"{num1.ToExpression(hash)} > 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}未穿盾（实际穿盾）"
                    });
                }
            }
            if (IsAbnormalState(eAbnormalState.DRAIN_MGC) && _damageData.DamageType == DamageData.eDamageType.MGC)
            {
                var num1 = _fDamage - abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue;
                if ((double)num1 > 0.0)
                {
                    _overRecoverValue += setRecoveryAndGetOverRecovery(BattleUtil.FloatToInt(abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue), this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].Source).Floor();
                    EnableAbnormalState(eAbnormalState.DRAIN_MGC, false);
                    _fDamage = num1;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num1.Emulate(hash) <= 0f,
                        exp = hash => $"{num1.ToExpression(hash)} <= 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}穿盾（实际未穿盾）"
                    });
                }
                else
                {
                    var num2 = BattleUtil.FloatToInt(_fDamage);
                    _overRecoverValue += setRecoveryAndGetOverRecovery(num2, this, _damageData.Target, _damageData.DamageType == DamageData.eDamageType.MGC, abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].Source).Floor();
                    abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_MGK].MainValue -= num2;
                    _fDamage = 0.0f;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num1.Emulate(hash) > 0f,
                        exp = hash => $"{num1.ToExpression(hash)} > 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}未穿盾（实际穿盾）"
                    });
                }
            }
            if (IsAbnormalState(eAbnormalState.GUARD_BOTH) && _damageData.ActionType != eActionType.DESTROY)
            {
                var num = _fDamage - abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].MainValue;
                if ((double)num > 0.0)
                {
                    EnableAbnormalState(eAbnormalState.GUARD_BOTH, false);
                    _fDamage = num;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num.Emulate(hash) <= 0f,
                        exp = hash => $"{num.ToExpression(hash)} <= 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}穿盾（实际未穿盾）"
                    });
                }
                else
                {
                    abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].MainValue -= BattleUtil.FloatToInt(_fDamage);
                    _fDamage = 0.0f;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num.Emulate(hash) > 0f,
                        exp = hash => $"{num.ToExpression(hash)} > 0",
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}未穿盾（实际穿盾）"
                    });
                }
            }

            /*
            // override the guard exec
            _fDamage = __fDamage;
            if (!barriers[eAbnormalState.GUARD_ATK].StrictlyEquals(0f) &&
                _damageData.DamageType == DamageData.eDamageType.ATK)
            {
                var val = _fDamage;
                _fDamage = (val - barriers[eAbnormalState.GUARD_ATK]).Max(0f);
                barriers[eAbnormalState.GUARD_ATK] = (barriers[eAbnormalState.GUARD_ATK] - BattleUtil.FloatToInt(val)).Max(0f);
            }
            if (!barriers[eAbnormalState.GUARD_MGC].StrictlyEquals(0f) &&
                _damageData.DamageType == DamageData.eDamageType.MGC)
            {
                var val = _fDamage;
                _fDamage = (val - barriers[eAbnormalState.GUARD_MGC]).Max(0f);
                barriers[eAbnormalState.GUARD_MGC] = (barriers[eAbnormalState.GUARD_MGC] - BattleUtil.FloatToInt(val)).Max(0f);
            }
            if (!barriers[eAbnormalState.GUARD_BOTH].StrictlyEquals(0f) &&
                _damageData.ActionType != eActionType.DESTROY)
            {
                var val = _fDamage;
                _fDamage = (val - barriers[eAbnormalState.GUARD_BOTH]).Max(0f);
                barriers[eAbnormalState.GUARD_BOTH] = (barriers[eAbnormalState.GUARD_BOTH] - BattleUtil.FloatToInt(val)).Max(0f);
            }
            
            */            
            if (IsAbnormalState(eAbnormalState.DRAIN_BOTH) && _damageData.ActionType != eActionType.DESTROY)
            {
                var num3 = _fDamage - abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH]
                    .MainValue;
                if ((double) num3 > 0.0)
                {
                    _overRecoverValue += setRecoveryAndGetOverRecovery(
                        BattleUtil.FloatToInt(
                            abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH]
                                .MainValue), this, _damageData.Target,
                        _damageData.DamageType == DamageData.eDamageType.MGC,
                        abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].Source).Floor();
                    EnableAbnormalState(eAbnormalState.DRAIN_BOTH, false);
                    _fDamage = num3;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num3.Emulate(hash) <= 0f,
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}穿盾（实际未穿盾）"
                    });
                }
                else
                {
                    var num1 = BattleUtil.FloatToInt(_fDamage);
                    _overRecoverValue += setRecoveryAndGetOverRecovery(num1, this, _damageData.Target,
                        _damageData.DamageType == DamageData.eDamageType.MGC,
                        abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].Source).Floor();
                    abnormalStateCategoryDataDictionary[eAbnormalStateCategory.DAMAGE_RESISTANCE_BOTH].MainValue -=
                        num1;
                    _fDamage = 0.0f;
                    GuildCalculator.Instance.dmglist.Add(lastBarrier = new ProbEvent
                    {
                        isProb = true,
                        unit = UnitNameEx,
                        predict = hash => num3.Emulate(hash) > 0f,
                        description = $"({BattleHeaderController.CurrentFrameCount})被{(_damageData.Source != null ? $"{_damageData.Source.UnitNameEx}的" + $"{(_damageData.Source.CurrentSkillId == 1 ? "普攻" : $"{_damageData.Source.unitActionController.skillDictionary[_damageData.Source.CurrentSkillId].SkillName}技能({_damageData.Source.CurrentSkillId})")}" : "领域")}未穿盾（实际穿盾）"
                    });
                }
            }
        }

        private void createDamageEffectFromSetDamageImpl(
          DamageData _damageData,
          bool _hasEffect,
          Skill _skill,
          bool _critical,
          int _damage)
        {
            if (((!(battleManager != null) ? 0 : (_damage >= 0 ? 1 : 0)) & (_hasEffect ? 1 : 0)) == 0)
                return;
            eDamageType damageType = (eDamageType)(int)_damageData.DamageType;
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
            if (battleManager.skipping) return;
            if (_parts == null)
                _parts = GetFirstParts(true);
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

        private FloatWithEx setRecoveryAndGetOverRecovery(
          FloatWithEx _value,
          UnitCtrl _source,
          BasePartsData _target,
          bool _isMagic,
          UnitCtrl _healSource)
        {
            var num = Hp + _value - MaxHp;
            SetRecovery(_value, _isMagic ? eInhibitHealType.MAGIC : eInhibitHealType.PHYSICS, _source, GetHealDownValue(_healSource), _target: _target);
            return num;
        }

        public void SetRecovery(
          FloatWithEx _value,
          eInhibitHealType _inhibitHealType,
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
                _target = GetFirstParts(true);
            _value = BattleUtil.FloatToInt(_healDownValue * _value);
            if ((IsDead || (long) Hp <= 0.0) && !_isRevival || IsClanBattleOrSekaiEnemy)
            {
                this.battleLog.AppendBattleLog(eBattleLogType.MISS, 8, 0L, 0L, 0, 0, JELADBAMFKH: _source, LIMEKPEENOB: this);
                return;
            }


            float num = 0f;
            if (IsAbnormalState(eAbnormalState.DECREASE_HEAL) && _inhibitHealType != eInhibitHealType.NO_EFFECT)
            {
                num = Mathf.Min(GetAbnormalStateMainValue(eAbnormalStateCategory.DECREASE_HEAL), 1f);
            }

            _value = BattleUtil.FloatToInt(_healDownValue * (1f - num) * _value);

            if (IsAbnormalState(eAbnormalState.INHIBIT_HEAL) && _inhibitHealType != eInhibitHealType.NO_EFFECT)
            {
                if (GetAbnormalStateMainValue(eAbnormalStateCategory.INHIBIT_HEAL) == 0.0)
                    return;
                DamageData _damageData = new DamageData
                {
                    Target = GetFirstParts((bool)(Object)this),
                    Damage = BattleUtil.FloatToInt(GetAbnormalStateMainValue(eAbnormalStateCategory.INHIBIT_HEAL) * _value),
                    DamageType = DamageData.eDamageType.NONE,
                    Source = abnormalStateCategoryDataDictionary[eAbnormalStateCategory.INHIBIT_HEAL].Source,
                    DamageNumMagic = _inhibitHealType == eInhibitHealType.MAGIC,
                    ActionType = eActionType.INHIBIT_HEAL
                };
                Skill skill = abnormalStateCategoryDataDictionary[eAbnormalStateCategory.INHIBIT_HEAL].Skill;
                int actionId = abnormalStateCategoryDataDictionary[eAbnormalStateCategory.INHIBIT_HEAL].ActionId;
                Skill _skill = skill;
                SetDamage(_damageData, false, actionId, _skill: _skill);
                action?.Invoke("毒奶，对目标造成" + _damageData.Damage + "点伤害");
            }
            else
            {
                if (_releaseToad && ToadDatas.Count > 0 && ToadDatas[0].ReleaseByHeal)
                    ToadDatas[0].Enable = false;
                Hp = (Hp + _value);
                BattleLogIntreface battleLog = this.battleLog;
                UnitCtrl unitCtrl1 = _source;
                UnitCtrl unitCtrl2 = this;
                long KGNFLOPBOMB = (long)_value;
                long hp = (long)Hp;
                UnitCtrl JELADBAMFKH = unitCtrl1;
                UnitCtrl LIMEKPEENOB = unitCtrl2;
                battleLog.AppendBattleLog(eBattleLogType.SET_RECOVERY, 0, KGNFLOPBOMB, hp, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
                Hp = Hp.Min(MaxHp);
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
                float NormalizedHP = (long)Hp / (float)(long)MaxHp;
                /*
                this.OnLifeAmmountChange.Call<float>(NormalizedHP);*/
                string des = $"目标HP回复<color=#54FF4F>{_value}</color>点，当前HP: {Hp}";
                action?.Invoke(des);
                uIManager?.LogMessage(des, eLogMessageType.HP_RECOVERY, _source);
                NoSkipOnLifeChanged?.Invoke(UnitId, Hp,(int)MaxHp,0, BattleHeaderController.CurrentFrameCount,des, _source);
                if (_isUnionBurstLifeSteal)
                {
                    unionburstLifeStealNum += (long)_value;
                }
                else
                {
                    if (_useNumberEffect && !battleManager.skipping)
                    {
                        UIManager.SetHealNumber(gameObject.transform.position, (int)_value);
                    }   //this.createHealNumEffect(_value, _target);
                    if (!_isEffect)
                        return;
                    //_target.RecoveryEffect(_source, false, this.battleEffectPool, _isRegenerate);
                }
            }
        }
        private void applyChargeEnergyByReceiveDamage(eActionType _actionType, long _damage, bool _isAlwaysChargeEnegry, Dictionary<eStateIconType, List<UnitCtrl>> _usedChargeEnergyByReceiveDamage = null)
        {
            if (_damage <= 0 || (long)Hp <= 0)
            {
                return;
            }
            eStateIconType _key;
            Action _value;
            switch (_actionType)
            {
                case eActionType.ATTACK:
                case eActionType.DESTROY:
                case eActionType.RATIO_DAMAGE:
                case eActionType.UPPER_LIMIT_ATTACK:
                    if (_usedChargeEnergyByReceiveDamage == null)
                    {
                        break;
                    }
                    foreach (KeyValuePair<eStateIconType, Action> item in ChargeEnergyByReceiveDamageDictionary)
                    {
                        ExtensionMethods.Deconstruct(item, out _key, out _value);
                        eStateIconType key = _key;
                        Action dMFGKJIEEBF = _value;
                        if (!_usedChargeEnergyByReceiveDamage.ContainsKey(key))
                        {
                            _usedChargeEnergyByReceiveDamage[key] = new List<UnitCtrl>();
                        }
                        if (_isAlwaysChargeEnegry || !_usedChargeEnergyByReceiveDamage[key].Contains(this))
                        {
                            dMFGKJIEEBF.Call();
                            if (!_isAlwaysChargeEnegry)
                            {
                                _usedChargeEnergyByReceiveDamage[key].Add(this);
                            }
                        }
                    }
                    break;
                case eActionType.ENCHANT_STRIKE_BACK:
                case eActionType.DIVISION:
                    foreach (KeyValuePair<eStateIconType, Action> item2 in ChargeEnergyByReceiveDamageDictionary)
                    {
                        ExtensionMethods.Deconstruct(item2, out _key, out _value);
                        _value.Call();
                    }
                    break;
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
          FloatWithEx _energy,
          bool _hasEffect = false,
          UnitCtrl _source = null,
          bool _hasNumberEffect = true,
          eEffectType _effectType = eEffectType.COMMON,
          bool _useRecoveryRate = true,
          bool _isRegenerate = false,
          float _multipleValue = 1f,
          Action<string> action = null)
        {
            if (IsAbnormalState(eAbnormalState.FEAR) && (_setEnergyType == eSetEnergyType.BY_ATK || _setEnergyType == eSetEnergyType.KILL_BONUS))
                return;
            var num = ((double)_energy > 0.0 & _useRecoveryRate ? (float)((EnergyRecoveryRateZeroCapped + 100.0) / 100.0) * _energy : _energy) * _multipleValue;
            //GameObject MDOJNMEMHLN = (double)_energy >= 0.0 ? Singleton<LCEGKJFKOPD>.Instance.NMJAMHCPMDF : Singleton<LCEGKJFKOPD>.Instance.OJCMBLJEGHF;
            if (_hasNumberEffect && !battleManager.skipping)
            {
                action?.Invoke("目标能量增加<color=#4C5FFF>" + num + $"</color>点");
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
            SetEnergy(Energy + num, _setEnergyType, _source);
            //if (!_hasEffect || _effectType != eEffectType.COMMON)
            //    return;
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
            if (!IsPartsBoss)
            {
                GetFirstParts(true).ResetTotalDamage();
            }
            else
            {
                for (int index = 0; index < BossPartsListForBattle.Count; ++index)
                    BossPartsListForBattle[index].ResetTotalDamage();
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
            Vector3 _defaultScale = new Vector3((IsLeftDir || IsForceLeftDir ? -1f : 1f) * Scale, Mathf.Abs(Scale));
            AppendCoroutine(updateScaleChange(++changeScaleId, _scaleChangers, _startTime, _defaultScale, _blackoutTime), ePauseType.NO_DIALOG);
        }
        public void StopScaleChange()
        {
            Vector3 localScale = new Vector3((float)((!IsLeftDir && !IsForceLeftDir) ? 1 : (-1)) * Scale, Mathf.Abs(Scale));
            //scaleChangeValue = 1f;
            GetCurrentSpineCtrl().transform.localScale = localScale;
            changeScaleId++;
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
                    while (battleManager.IsPlayingPrincessMovie || battleManager.isPrincessFormSkill)
                        yield return null;
                    if (_changeScaleId != changeScaleId)
                    {
                        yield break;
                    }

                    timer += battleManager.DeltaTime_60fps;
                    if (timer < currentData.Time - (double)currentData.Duration)
                    {
                        if (timer >= (double)_startTime)
                            yield return null;
                    }
                    else if (timer < (double)currentData.Time)
                    {
                        float curVal = currentEasing.GetCurVal(battleManager.DeltaTime_60fps);
                        GetCurrentSpineCtrl().transform.localScale = _defaultScale * curVal;
                    }
                    else
                        goto label_11;
                }
                while (timer < (double)_startTime);
                yield return null;
                continue;
            label_11:
                GetCurrentSpineCtrl().transform.localScale = _defaultScale * currentData.Scale;
                ++currentIndex;
                if (_scaleChangers.Count != currentIndex && timer <= _blackoutTime + (double)_startTime)
                {
                    float scale = currentData.Scale;
                    currentData = _scaleChangers[currentIndex];
                    currentEasing = new CustomEasing(currentData.Easing, scale, currentData.Scale, currentData.Duration);
                }
                else
                    break;
            }
            while (_changeScaleId == changeScaleId)
            {
                if (timer > _blackoutTime + (double)_startTime)
                {
                    GetCurrentSpineCtrl().transform.localScale = _defaultScale;
                    break;
                }
                yield return null;
                timer += battleManager.DeltaTime_60fps;
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
            SearchAreaSize = value;
            searchAreaChangeTimer = _time;
            searchAreaChangeReduceEnergy = _reduceEnergy;
            searchAreaEnergyRate = _reduceEnergyRate;
            IsReduceEnergyDictionary[eReduceEnergyType.SEARCH_AREA] = _reduceEnergy;
            if (searchAreaChangeRunning)
                return;
            searchAreaChangeRunning = true;
            AppendCoroutine(updateChangeSearchArea(), ePauseType.SYSTEM, this);
        }

        private IEnumerator updateChangeSearchArea()
        {
            while (true)
            {
                if (searchAreaChangeReduceEnergy)
                {
                    SetEnergy(Energy - DeltaTimeForPause * searchAreaEnergyRate, eSetEnergyType.BY_MODE_CHANGE);
                    if ((double)Energy == 0.0 || IsDead)
                        break;
                }
                else
                {
                    searchAreaChangeTimer -= DeltaTimeForPause;
                    if (searchAreaChangeTimer < 0.0)
                        goto label_5;
                }
                yield return null;
            }
            IsReduceEnergyDictionary[eReduceEnergyType.SEARCH_AREA] = false;
            SearchAreaSize = StartSearchAreaSize;
            searchAreaChangeRunning = false;
            yield break;
        label_5:
            SearchAreaSize = StartSearchAreaSize;
            searchAreaChangeRunning = false;
        }

        public int GetSortOrderConsiderBlackout()
        {
            int sSortOrder = m_sSortOrder;
            if (battleManager.IsSkillExeUnit(this))
                sSortOrder -= 11500;
            return sSortOrder;
        }

        public int SortOrder
        {
            get => m_sSortOrder;
            set
            {
                m_sSortOrder = value;
                if (!(GetCurrentSpineCtrl() != null))
                    return;
                //this.GetCurrentSpineCtrl().Depth = value;
            }
        }

        public void SetSortOrderBack()
        {
            for (int index = 0; index < SortFrontDiappearAttachmentChangeDataList.Count; ++index)
            {
                AttachmentChangeData attachmentChangeData = SortFrontDiappearAttachmentChangeDataList[index];
                (UnitSpineCtrl.skeleton.skin == null ? UnitSpineCtrl.skeleton.data.defaultSkin : UnitSpineCtrl.skeleton.skin).AddAttachment(attachmentChangeData.TargetIndex, attachmentChangeData.TargetAttachmentName, attachmentChangeData.TargetAttachment);
                UnitSpineCtrl.skeleton.slots.Items[attachmentChangeData.TargetIndex].attachment = attachmentChangeData.TargetAttachment;
            }
            IsFront = false;
            SortOrder = BattleDefine.GetUnitSortOrder(this);
            //this.lifeGauge.SetSortOrderBack();
            //int index1 = 0;
            //for (int count = this.CircleEffectList.Count; index1 < count; ++index1)
            //    this.CircleEffectList[index1].SetSortOrderBack();
            for (int index2 = 0; index2 < BossPartsListForBattle.Count; ++index2)
                BossPartsListForBattle[index2].IsBlackoutTarget = false;
            OnIsFrontFalse.Call();
        }

        public void SetSortOrderFront()
        {
            for (int index = 0; index < SortFrontDiappearAttachmentChangeDataList.Count; ++index)
            {
                AttachmentChangeData attachmentChangeData = SortFrontDiappearAttachmentChangeDataList[index];
                (UnitSpineCtrl.skeleton.skin == null ? UnitSpineCtrl.skeleton.data.defaultSkin : UnitSpineCtrl.skeleton.skin).AddAttachment(attachmentChangeData.TargetIndex, attachmentChangeData.TargetAttachmentName, attachmentChangeData.AppliedAttachment);
                UnitSpineCtrl.skeleton.slots.Items[attachmentChangeData.TargetIndex].attachment = attachmentChangeData.AppliedAttachment;
            }
            IsFront = true;
            SortOrder = BattleDefine.GetUnitSortOrder(this) + 11500;
            //this.lifeGauge.SetSortOrderFront();
            //int index1 = 0;
            //for (int count = this.CircleEffectList.Count; index1 < count; ++index1)
            //    this.CircleEffectList[index1].SetSortOrderFront();
        }

        public void SetSortOrderSuperFront()
        {
            if (!IsFront)
                return;
            SortOrder = 22350;
            //this.lifeGauge.SetSortOrderFront();
            //int index = 0;
            //for (int count = this.CircleEffectList.Count; index < count; ++index)
            //    this.CircleEffectList[index].SetSortOrderFront();
        }

        public void SetSortOrderSuperBack()
        {
            if (!IsFront)
                return;
            SortOrder = 11850;
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
            AppendCoroutine(updateChangeSortOrder(_changeSortOrderDatas, _startTime, ++changeSortOrderId), ePauseType.NO_DIALOG);
        }

        private IEnumerator updateChangeSortOrder(
          List<ChangeSortOrderData> _changeSortOrderDatas,
          float _startTime,
          int _thisId)
        {
            float timer = _startTime;
            int currentIndex = 0;
            ChangeSortOrderData currentData = _changeSortOrderDatas[currentIndex];
            while (_thisId == changeSortOrderId)
            {
                timer += battleManager.DeltaTime_60fps;
                if (timer < (double)currentData.Time)
                {
                    yield return null;
                }
                else
                {
                    switch (currentData.SortType)
                    {
                        case ChangeSortOrderData.eSortType.DEFAULT:
                            if (IsFront)
                            {
                                SetSortOrderFront();
                                break;
                            }
                            SetSortOrderBack();
                            break;
                        case ChangeSortOrderData.eSortType.FRONT:
                            SetSortOrderSuperFront();
                            break;
                        case ChangeSortOrderData.eSortType.BACK:
                            SetSortOrderSuperBack();
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
        private bool isRevivaling
        {
            get
            {
                if ((long)Hp <= 0)
                {
                    return OnDeadForRevival != null;
                }
                return false;
            }
        }
        private Dictionary<int, float> castTimeDictionary { get; set; }

        public bool UnionBurstAnimeEndForIfAction { get; set; }

        public int UnionBurstSkillId { get; set; }
        public bool IsSubUnionBurstMode
        {
            get;
            set;
        }
        public bool IsStartVoicePlay { get; set; }

        public bool ModeChangeEnd { get; set; }

        private float unionBurstSkillAreaWidth { get; set; }

        public bool GameStartDone { get; set; }

        private int dieCoroutineId { get; set; }

        public Action<float> OnUpdateWhenIdle { get; set; }

        public Action OnBreakAll { get; set; }
        public bool OnBreakAllCallWhenGameStartDone
        {
            get;
            set;
        }
        private int damageCoroutineId { get; set; }

        private int walkCoroutineId { get; set; }

        public bool EnemyPointDone { get; set; }

        public int EnemyPoint { get; private set; }

        private int specialBattlePurposeHp { get; set; }

        public int PositionOrder { get; set; }

        public Action OnMotionPrefixChanged { get; set; }

        private bool multiTargetDone { get; set; }

        public void SetState(ActionState _state, int _nextSkillId = 0, int _skillId = 0, bool _quiet = false)
        {
            string des = _state == ActionState.SKILL ? (unitActionController.skillDictionary.TryGetValue(_skillId,out var value)?value.SkillName:"UnknownSkill") : "";
            SemanOnChangeState?.Invoke(UnitId, _state);
            if (!battleManager.skipping)
            {
                uIManager?.LogMessage($"切换到{_state}状态", eLogMessageType.OTHER, this);
                NoSkipOnChangeState?.Invoke(UnitId, _state, BattleHeaderController.CurrentFrameCount, des, this);
            }
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
            setStateCalled = true;
            if (!MultiTargetByTime && GameStartDone && IsPartsBoss && !multiTargetDone)
            {
                PlayAndSetUpMultiTarget(_isFirst: true);
            }
            if (IsPartsBoss)
                battleManager.LOGNEDLPEIJ = false;
            if (_state != ActionState.IDLE && _state != ActionState.WALK)
            {
                CancelByConvert = false;
                CancelByToad = false;
            }
            if (_state == ActionState.SKILL_1)
            {
                _skillId = UnionBurstSkillId;
                BattleLogIntreface battleLog = this.battleLog;
                UnitCtrl unitCtrl1 = this;
                UnitCtrl unitCtrl2 = this;
                long KGNFLOPBOMB = _skillId;
                long currentState = (long)CurrentState;
                UnitCtrl JELADBAMFKH = unitCtrl1;
                UnitCtrl LIMEKPEENOB = unitCtrl2;
                battleLog.AppendBattleLog(eBattleLogType.BUTTON_TAP, 0, KGNFLOPBOMB, currentState, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
            }
            BattleLogIntreface battleLog1 = this.battleLog;
            UnitCtrl unitCtrl3 = this;
            UnitCtrl unitCtrl4 = this;
            int HLIKLPNIOKJ = (int)_state;
            long KGNFLOPBOMB1 = _skillId;
            long currentState1 = (long)CurrentState;
            UnitCtrl JELADBAMFKH1 = unitCtrl3;
            UnitCtrl LIMEKPEENOB1 = unitCtrl4;
            battleLog1.AppendBattleLog(eBattleLogType.SET_STATE, HLIKLPNIOKJ, KGNFLOPBOMB1, currentState1, 0, 0, JELADBAMFKH: JELADBAMFKH1, LIMEKPEENOB: LIMEKPEENOB1);
            /*for (int index = this.idleEffectsObjs.Count - 1; index >= 0; --index)
            {
                this.idleEffectsObjs[index].SetTimeToDie(true);
                this.idleEffectsObjs.RemoveAt(index);
            }*/
            if (!Pause)
            {
                if (_state == ActionState.IDLE && IsAbnormalState(eAbnormalState.HASTE))
                    GetCurrentSpineCtrl().SetTimeScale(2f);
                else
                    GetCurrentSpineCtrl().Resume();
            }
            setRecastTime(_nextSkillId);
            SetDirectionAuto();
            switch (_state)
            {
                case ActionState.IDLE:
                    setStateIdle();
                    break;
                case ActionState.ATK:
                    setStateAttack();
                    break;
                case ActionState.SKILL_1:
                    setStateSkill1();
                    break;
                case ActionState.SKILL:
                    setStateSkill(_skillId);
                    break;
                case ActionState.WALK:
                    setStateWalk();
                    break;
                case ActionState.DAMAGE:
                    setStateDamage(_quiet);
                    break;
                case ActionState.DIE:
                    setStateDie();
                    break;
                case ActionState.GAME_START:
                    isAwakeMotion = true;
                    SetLeftDirection(true);
                    PlayAnime(eSpineCharacterAnimeId.GAME_START, _isLoop: false, _quiet: true);
                    CurrentState = _state;
                    eBattleCategory jiliicmhlch = battleManager.BattleCategory;
                    /*if ((jiliicmhlch == eBattleCategory.TOWER_EX ? 1 : (jiliicmhlch == eBattleCategory.TOWER_EX_REPLAY ? 1 : 0)) != 0 && this.battleManager.GetCurrentTowerExPartyIndex() > 0)
                    {
                        this.AppendCoroutine(this.updateGameStartMotionIdle(), ePauseType.SYSTEM);
                        break;
                    }*/
                    AppendCoroutine(updateGameStart(), ePauseType.SYSTEM);
                    break;
            }
        }
        
        public void PlayAndSetUpMultiTarget(bool _isFirst)
        {
            multiTargetDone = true;
            // SaveDataManager instance = ManagerSingleton<SaveDataManager>.Instance;
            // if (instance.MultiTargetConfirm == 0 || (instance.MultiTargetConfirm == 2 && !instance.MultiTargetFirst))
            // {
            //     instance.MultiTargetFirst = _isFirst;
            //     SingletonMonoBehaviour<UnitUiCtrl>.Instance.HidePopUp();
            //     battleManager.SetSkillScreen(GABGIKMFNFG: true);
            //     battleManager.GamePause(ICBCHCGGHLB: true);
            //     SetSortOrderFront();
            //     battleManager.MIGBAMLOOKH = true;
            //     soundManager.PlaySe(eSE.MULTI_TARGETS_START);
            //     if (!OneRemainingDisableEffect || BossPartsListForBattle.FindAll((PartsData e) => !e.IsBreak).Count >= 2)
            //     {
            //         ((Component)ManagerSingleton<ResourceManager>.Instance.InstantiateAndGetComponent<Animator>(eResourceId.ANIM_MULTI_TARGET_APPEAR, ((Component)battleManager.OFMPGBKBOPM).transform, 0L)).transform.position = BottomTransform.position + FixedCenterPos;
            //     }
            //     ((MonoBehaviour)(object)this).Timer(1f, delegate
            //     {
            //         battleManager.SetSkillScreen(GABGIKMFNFG: false);
            //         SetSortOrderBack();
            //         battleManager.MIGBAMLOOKH = false;
            //         if (!SingletonMonoBehaviour<BattleHeaderController>.Instance.IsPaused)
            //         {
            //             battleManager.GamePause(ICBCHCGGHLB: false);
            //         }
            //     });
            // }
            // else
            // {
                battleManager.IsPausingEffectSkippedInThisFrame = true;
            // }
            for (int index = 0; index < BossPartsListForBattle.Count; ++index)
            {
                //MultiTargetCursor cursor = ManagerSingleton<ResourceManager>.Instance.InstantiateAndGetComponent<MultiTargetCursor>(this.UseTargetCursorOver ? eResourceId.ANIM_MULTI_TARGET_CURSOR_OVER : eResourceId.ANIM_MULTI_TARGET_CURSOR, this.battleManager.UnitUiCtrl.transform);
                PartsData _data = BossPartsListForBattle[index];
                //cursor.transform.position = _data.GetBottomTransformPosition();
                //cursor.Panel.sortingOrder = this.GetCurrentSpineCtrl().Depth + (this.UseTargetCursorOver ? 100 : -100);
                //_data.MultiTargetCursor = cursor;
                //this.battleManager.AppendCoroutine(_data.TrackBottom(), ePauseType.SYSTEM, this);
                if (_data.IsBreak)
                {
                    AppendBreakLog(_data.BreakSource);
                    _data.IsBreak = true;
                    _data.OnBreak.Call();
                    _data.SetBreak(true, null);// this.battleManager.UnitUiCtrl.transform);
                    AppendCoroutine(UpdateBreak(_data.BreakTime, _data), ePauseType.SYSTEM);
                }
                OnIsFrontFalse += () =>
                {
                    if (UseTargetCursorOver)
                        return;
                    //cursor.Panel.sortingOrder = this.GetCurrentSpineCtrl().Depth - 100;
                };
            }
		}

        private void setRecastTime(int skillId)
        {
            switch (skillId)
            {
                case 0:
                    return;
                case 1:
                    m_fCastTimer = m_fAtkRecastTime;
                    break;
                default:
                    m_fCastTimer = castTimeDictionary[skillId];
                    break;
            }
            setIdleCastTime();

            //if (!IsAbnormalState(eAbnormalState.SLOW) && !IsAbnormalState(eAbnormalState.HASTE) || abnormalStateCategoryDataDictionary[eAbnormalStateCategory.SPEED].MainValue <= 0.01)
            //    return;
            //m_fCastTimer = m_fCastTimer / abnormalStateCategoryDataDictionary[eAbnormalStateCategory.SPEED].MainValue;
        }

        private IEnumerator updateGameStartMotionIdle()
        {
            BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
            float endTime = currentSpineCtrl.state.Data.skeletonData.FindAnimation(currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.GAME_START)).Duration + BossAppearDelay;
            currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.IDLE);
            while (endTime > 0.0)
            {
                endTime -= DeltaTimeForPause;
                yield return null;
            }
            GameStartDone = true;
            SetState(ActionState.IDLE);
        }

        private IEnumerator updateGameStart()
        {
            //MyOnChangeState?.Invoke(UnitId, CurrentState, BattleHeaderController.CurrentFrameCount);
            UnitCtrl unitCtrl = this;
            float time = 0.0f; 
            bool[] shakeStarted = new bool[unitCtrl.gameStartShakes.Count];
            //if (unitCtrl.battleManager.GetPurpose() == eHatsuneSpecialPurpose.SHIELD || unitCtrl.battleManager.GetPurpose() == eHatsuneSpecialPurpose.ABSORBER && unitCtrl.battleManager.FICLPNJNOEP < unitCtrl.battleManager.GetPurposeCount())
            //unitCtrl.AppendCoroutine(unitCtrl.CreatePrefabWithTime(unitCtrl.gameStartEffects, _isShieldEffect: true), ePauseType.SYSTEM);
            while (true)
            {
                unitCtrl.GetCurrentSpineCtrl().Pause();
                time += unitCtrl.DeltaTimeForPause;
                if (time <= (double)unitCtrl.BossAppearDelay)
                    yield return null;
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
                for (int index = 0; index < unitCtrl.gameStartShakes.Count; ++index)
                {
                    if (!shakeStarted[index] && (double)unitCtrl.gameStartShakes[index].StartTime < (double)time)
                    {
                        unitCtrl.battleManager.battleCameraEffect.StartShake(unitCtrl.gameStartShakes[index], (Skill)null, unitCtrl);
                        shakeStarted[index] = true;
                    }
                }
                if (!unitCtrl.IsDead)
                {
                    if (unitCtrl.GetCurrentSpineCtrl().IsPlayAnimeBattle && !unitCtrl.CancelByAwake)
                        yield return null;
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

            unitCtrl.IsDepthBack = false;
            unitCtrl.SetSortOrderBack();
            yield break;
            label_18:
            unitCtrl.GameStartDone = true;
            if (unitCtrl.IsDepthBack)
            {
                unitCtrl.IsDepthBack = false;
                unitCtrl.SetSortOrderBack();
            }
            if (!unitCtrl.CancelByAwake)
                unitCtrl.SetState(ActionState.IDLE);
        }

        private void setStateSkill(int skillId)
        {
            if (skillId == 0 || unitActionController == null)
                return;
            CurrentState = ActionState.SKILL;
            CurrentSkillId = skillId;
            if ((IsAbnormalState(eAbnormalState.SILENCE) || ToadDatas.Count > 0) && AttackWhenSilence)
                SetState(ActionState.ATK);
            else if (unitActionController.StartAction(skillId) || IsBoss)
            {
                ChargeEnergy(eSetEnergyType.BY_ATK, skillStackVal, _hasEffect: false, this, _hasNumberEffect: false);
                AppendCoroutine(updateSkill(skillId), ePauseType.SYSTEM, this);
                //if (!this.voiceTypeDictionary.ContainsKey(skillId) || !this.judgePlayVoice("skill", 2, true, 0.5f))
                 //   return;
                //this.PlayVoice(this.voiceTypeDictionary[skillId].VoiceType, this.voiceTypeDictionary[skillId].SkillNumber * 100, false, true);
            }
            else
                SetState(ActionState.IDLE);
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
                    case ActionState.SKILL_1:
                    case ActionState.DAMAGE:
                    case ActionState.DIE:
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
                                _unit.UnitSpineCtrl.gameObject.SetActive(false);
                                _unit.UnitSpineCtrlModeChange.gameObject.SetActive(true);
                                _unit.MotionPrefix = 1;
                                _unit.OnMotionPrefixChanged.Call();
                                if (_unit.IsFront)
                                    _unit.SetSortOrderFront();
                                else
                                    _unit.SetSortOrderBack();
                                _unit.SetLeftDirection(_unit.IsLeftDir);
                                int skillNum = _unit.unitActionController.GetSkillNum(skillId);
                                eSpineCharacterAnimeId animeId = unitActionController.GetAnimeId(skillId);
                                if (_unit.UnitSpineCtrlModeChange.IsAnimation(_unit.UnitSpineCtrlModeChange.ConvertAnimeIdToAnimeName(animeId, skillNum, _index3: 1)))
                                {
                                    _unit.PlayAnimeNoOverlap(animeId, skillNum, _index3: 1, _targetCtr: _unit.UnitSpineCtrlModeChange);
                                    _unit.AppendCoroutine(_unit.updateModeChange(), ePauseType.SYSTEM, _unit);
                                    yield break;
                                }
                                else
                                    ModeChangeUnableStateBarrier = false;

                                _unit.SetState(ActionState.IDLE);
                                yield break;
                            }

                            if (_unit.unitActionController.HasNextAnime(skillId))
                            {
                                yield break;
                            }

                            _unit.SetState(ActionState.IDLE);
                            _unit.CancelByConvert = false;
                            _unit.CancelByAwake = false;
                            _unit.CancelByToad = false;
                            yield break;
                        }
                        else
                        {
                            
                            yield return null;
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
            PlayAnime(eSpineCharacterAnimeId.JOY_LONG, MotionPrefix, _isLoop: false);
            //this.SetEnableColor();
            CureAllAbnormalState();
            AppendCoroutine(updateStateJoy(), ePauseType.SYSTEM);
        }

        private IEnumerator updateStateJoy()
        {
            while (GetCurrentSpineCtrl().IsPlayAnimeBattle)
                yield return null;
            SetState(ActionState.IDLE);
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
            CurrentState = ActionState.SKILL_1;
            CurrentSkillId = UnionBurstSkillId;
            battleManager.HELHEEOHPFO = this;
            if (unitActionController.GetIsSkillPrincessForm(UnionBurstSkillId)) 
            {
                //Debug.LogError("UB特效鸽了！");
                princessFormProcessor.StartPrincessFormSkill(unitActionController.GetPrincessFormMovieData(UnionBurstSkillId));
            }
            else if (unitActionController.StartAction(UnionBurstSkillId))
                AppendCoroutine(updateSkill1(), ePauseType.SYSTEM, this);
            else
                SetState(ActionState.IDLE);

            lastCritPoint = new CritPoint
            {
                description = $"{UnitNameEx}释放ub",
                description2 = $"{UnitNameEx}ub后",
                priority = eCritPointPriority.StartSkill
            };
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
                    case ActionState.DAMAGE:
                    case ActionState.DIE:
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
                                _unit.UnitSpineCtrl.gameObject.SetActive(false);
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

                                _unit.SetState(ActionState.IDLE);
                                yield break;
                            }

                            if (!_unit.unitActionController.HasNextAnime(_unit.UnionBurstSkillId))
                            {
                                _unit.SkillEndProcess();
                                yield break;
                            }

                            while (_unit.CurrentState != ActionState.IDLE)
                            {
                                switch (_unit.CurrentState)
                                {
                                    case ActionState.DAMAGE:
                                    case ActionState.DIE:
                                        _unit.unitActionController.CancelAction(_unit.UnionBurstSkillId);
                                        _unit.CancelByConvert = false;
                                        _unit.CancelByToad = false;
                                        yield break;
                                    default:
                                        yield return null;
                                        continue;
                                }
                            }
                            yield break;
                        }
                        else
                        {
                            yield return null;
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
            SetState(ActionState.IDLE);
            CancelByConvert = false;
            CancelByToad = false;
        }

        private IEnumerator updateModeChange()
        {
            while (GetCurrentSpineCtrl().IsPlayAnimeBattle)
                yield return null;
            ModeChangeUnableStateBarrier = false;
            SetState(ActionState.IDLE);
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
            switch (CurrentState)
            {
                case ActionState.DAMAGE:
                    if (OnDeadForRevival != null || IsDead || UnDeadTimeHitCount > 1)
                        break;
                    IsDead = true;
                    battleManager.CallbackDead(this);
                    if (!(runSmokeEffect != null))
                        break;
                    //this.runSmokeEffect.SetTimeToDie(true);
                    break;
                case ActionState.DIE:
                    break;
                default:
                    if (OnDeadForRevival == null && !IsDead && UnDeadTimeHitCount <= 1)
                    {
                        IsDead = true;
                        battleManager.CallbackDead(this);
                        //if ((UnityEngine.Object)this.runSmokeEffect != (UnityEngine.Object)null)
                        //    this.runSmokeEffect.SetTimeToDie(true);
                    }
                    if ((ActionsTargetOnMe.Count > 0 || FirearmCtrlsOnMe.Count > 0) && UnDeadTimeHitCount == 0)
                    {
                        SetState(ActionState.DAMAGE, _quiet: true);
                        break;
                    }
                    if (ToadDatas.Count > 0)
                    {
                        DieInToad = true;
                        ToadDatas[0].Enable = false;
                        break;
                    }
                    CureAllAbnormalState();
                    if (KillBonusTarget != null)
                        KillBonusTarget.ChargeEnergy(eSetEnergyType.KILL_BONUS, battleManager.EnergyStackValueDefeat, _hasNumberEffect: false);
                    /*for (int count = this.RepeatEffectList.Count; count > 0; --count)
                    {
                        SkillEffectCtrl repeatEffect = this.RepeatEffectList[count - 1];
                        if ((UnityEngine.Object)repeatEffect != (UnityEngine.Object)null && !(repeatEffect is FirearmCtrl))
                        {
                            repeatEffect.SetTimeToDie(true);
                            repeatEffect.OnEffectEnd.Call<SkillEffectCtrl>(repeatEffect);
                        }
                    }*/
                    if (UnDeadTimeHitCount == 1)
                        PlayAnimeNoOverlap(eSpineCharacterAnimeId.DIE, 1);
                    else if (IsDivisionSourceForDie)
                        OnStartErrorUndoDivision.Call(false);
                    else
                        PlayAnime(eSpineCharacterAnimeId.DIE, _index2: MotionPrefix, _isLoop: false);
                    //if (this.dieEffects.Count > 0 && !this.IsDivisionSourceForDie)
                    //    this.AppendCoroutine(this.CreatePrefabWithTime(this.dieEffects, _ignorePause: true), ePauseType.IGNORE_BLACK_OUT);
                    int index1 = 0;
                    for (int count = this.dieShakes.Count; index1 < count; ++index1)
                        this.AppendCoroutine(this.startShakeWithDelay(this.dieShakes[index1], true), ePauseType.IGNORE_BLACK_OUT);
                    if (OnDieForZeroHp != null && OnDeadForRevival == null)
                        OnDieForZeroHp(this);
                    //this.playRetireVoice();
                    accumulateDamage = 0L;
                    TargetEnemyList.Clear();
                    targetPlayerList.Clear();
                    CurrentState = ActionState.DIE;
                    if (IsPartsBoss)
                    {
                        for (int index2 = 0; index2 < BossPartsListForBattle.Count; ++index2)
                            BossPartsListForBattle[index2].DisableCursor();
                    }
                    foreach (KeyValuePair<int, UnitCtrl> summonUnit in SummonUnitDictionary)
                    {
                        summonUnit.Value.IdleOnly = true;
                        summonUnit.Value.CureAllAbnormalState();
                        battleManager.CallbackDead(summonUnit.Value);
                    }
                    if (HasDieLoop)
                    {
                        RespawnPos = eUnitRespawnPos.MAIN_POS_1;
                        if (!IsFront)
                            SetSortOrderBack();
                    }
                    if (IsDivisionSourceForDie)
                    {
                        AppendCoroutine(updateUndoDivision(), ePauseType.SYSTEM);
                        break;
                    }
                    AppendCoroutine(updateDie(++dieCoroutineId), ePauseType.SYSTEM);
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
                if (currentSpineCtrl == null)
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
                        if (fAlpha <= 0.0)
                        {
                            if (unitCtrl.OnDieFadeOutEnd != null && unitCtrl.OnDeadForRevival == null)
                            {
                                unitCtrl.OnDieFadeOutEnd(unitCtrl);
                                unitCtrl.OnDieFadeOutEnd = null;
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
                                {
                                    BattleManager.Instance.QueueUpdateSkillTarget();
                                    unitCtrlList.Remove(unitCtrl);
                                }
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
                        if (unitCtrl.GetCurrentSpineCtrl() == null)
                            break;
                        unitCtrl.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, fAlpha);
                        yield return null;
                    }
                    break;
                }
                yield return null;
            }
        }

        private IEnumerator updateUndoDivision()
        {
            UnitCtrl _unit = this;
            while (_unit.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                yield return null;
            _unit.PlayAnime(eSpineCharacterAnimeId.DIE, _index2: _unit.MotionPrefix, _isLoop: false);
            //_unit.AppendCoroutine(_unit.CreatePrefabWithTime(_unit.dieEffects), ePauseType.VISUAL, _unit);
            _unit.AppendCoroutine(_unit.updateDie(++_unit.dieCoroutineId), ePauseType.SYSTEM);
        }

        private void setStateDamage(bool _quiet)
        {
            ModeChangeEnd = false;
            switch (CurrentState)
            {
                case ActionState.DAMAGE:
                case ActionState.DIE:
                    break;
                default:
                    if (ToadRelease)
                    {
                        ToadReleaseDamage = true;
                        break;
                    }
                    //if (this.damageEffects.Count > 0)
                    //    this.AppendCoroutine(this.CreatePrefabWithTime(this.damageEffects), ePauseType.SYSTEM);
                    int index = 0;
                    for (int count = this.damageShakes.Count; index < count; ++index)
                        this.AppendCoroutine(this.startShakeWithDelay(this.damageShakes[index]), ePauseType.SYSTEM);
                    //if (!_quiet)
                     //   this.playDamageVoice();
                    PlayAnime(eSpineCharacterAnimeId.DAMEGE, MotionPrefix, _isLoop: false, _ignoreBlackout: true);
                    CurrentState = ActionState.DAMAGE;
                    AppendCoroutine(updateDamage(++damageCoroutineId), ePauseType.IGNORE_BLACK_OUT);
                    break;
            }
        }

        private IEnumerator updateDamage(int _thisId)
        {
            //MyOnChangeState?.Invoke(UnitId,CurrentState, BattleHeaderController.CurrentFrameCount);
            float time = 0.0f;
            while (_thisId == damageCoroutineId)
            {
                if ((long)Hp <= 0L)
                {
                    if (ActionsTargetOnMe.Count > 0 || FirearmCtrlsOnMe.Count > 0 || KnockBackEnableCount > 0)
                    {
                        if (time == 0)
                        {
                            uIManager?.LogMessage($"由于被技能{string.Join("\n", ActionsTargetOnMe)}、{FirearmCtrlsOnMe.Count}个弹射物与{KnockBackEnableCount}个击退效果锁定，不会立即死亡", eLogMessageType.OTHER, this);
                        }
                        time += battleManager.DeltaTime_60fps;
                        if (time > 10.0)
                        {
                            bool flag = false;
                            for (int index = ActionsTargetOnMe.Count - 1; index >= 0; --index)
                            {
                                flag = true;
                                ActionsTargetOnMe.RemoveAt(index);
                            }
                            for (int index = FirearmCtrlsOnMe.Count - 1; index >= 0; --index)
                            {
                                flag = true;
                                FirearmCtrlsOnMe.RemoveAt(index);
                            }
                            if (flag)
                                continue;
                        }
                        BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
                        TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                        if (current != null && current.animationLast >= (double)currentSpineCtrl.StopStateTime)
                        {
                            current.animationLast = currentSpineCtrl.StopStateTime;
                            current.animationStart = currentSpineCtrl.StopStateTime;
                            currentSpineCtrl.Pause();
                        }
                        yield return null;
                    }
                    else
                    {
                        GetCurrentSpineCtrl().Resume();
                        CurrentState = ActionState.IDLE;
                        SetState(ActionState.DIE);
                        break;
                    }
                }
                else
                {
                    if (isPauseDamageMotion())
                    {
                        resumeIsStopState = true;
                        BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
                        TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                        if (current != null)
                        {
                            current.lastTime = currentSpineCtrl.StopStateTime;
                            current.time = currentSpineCtrl.StopStateTime;
                            //current.animationLast = currentSpineCtrl.StopStateTime;
                            //current.animationStart = currentSpineCtrl.StopStateTime;

                        }
                        bool isInitPause = !isSpecialSleepStatus;
                        if (isInitPause)
                        {
                            GetCurrentSpineCtrl().Pause();
                        } 
                        while (IsUnableActionState() && (long)Hp != 0L)
                        {
                            /*if (_thisId != damageCoroutineId)
                                yield break;
                            yield return null;*/
                            BattleSpineController currentSpineCtrl3 = GetCurrentSpineCtrl();
                            if (_thisId != damageCoroutineId)
                            {
                                yield break;
                            }
                            if (!isInitPause && !isSpecialSleepStatus)
                            {
                                currentSpineCtrl3.Pause();
                                isInitPause = true;
                            }
                            if (isPlayDamageAnimForAbnormal)
                            {
                                isPlayDamageAnimForAbnormal = false;
                                PlayAnime(eSpineCharacterAnimeId.DAMEGE, MotionPrefix, -1, -1, _isLoop: false, null, _quiet: false, 0f, _ignoreBlackout: true);
                                break;
                            }
                            yield return null;
                        }
                    }
                    //if (specialSleepStatus != eSpecialSleepStatus.INVALID && specialSleepStatus != eSpecialSleepStatus.RELEASE)
                    //    resumeIsStopState = true;
                    if (isSpecialSleepStatus)
                    {
                        resumeIsStopState = true;
                    }
                    GetCurrentSpineCtrl().Resume();
                    if ((long)Hp == 0L)
                    {
                        CurrentState = ActionState.IDLE;
                        SetState(ActionState.DIE);
                        break;
                    }
                    if (isSpecialSleepStatus && GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(MotionPrefix))
                    {
                        releaseSleepAnime();
                    }
                    if (!GetCurrentSpineCtrl().IsPlayAnimeBattle && !IsUnableActionState())
                    {
                        if (ToadRelease && IdleOnly)
                            break;
                        SetState(ActionState.IDLE);
                        break;
                    }
                    yield return null;
                }
            }
        }

        private bool isPauseDamageMotion() => IsUnableActionState() && GetCurrentSpineCtrl().IsStopState && (specialSleepStatus == eSpecialSleepStatus.INVALID || specialSleepStatus == eSpecialSleepStatus.RELEASE);

        private void setStateAttack()
        {
            CurrentState = ActionState.ATK;
            CurrentSkillId = 1;
            UnitActionController actionController = unitActionController;
            if (ToadDatas.Count > 0)
                actionController = ToadDatas[0].UnitActionController;
            if (actionController.StartAction(1))
            {
                ChargeEnergy(eSetEnergyType.BY_ATK, skillStackVal, _source: this, _hasNumberEffect: false);
                //if (this.judgePlayVoice("attack", 1, false, 0.0f))
                //    this.PlayVoice(SoundManager.eVoiceType.ATTACK, 0, true, false);
                AppendCoroutine(updateAttack(actionController), ePauseType.SYSTEM, this);
            }
            else
                SetState(ActionState.IDLE);
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
            while (!IsCancelActionState(false))
            {
                if (!GetCurrentSpineCtrl().IsPlayAnimeBattle)
                {
                    SetState(ActionState.IDLE);
                    CancelByAwake = false;
                    CancelByConvert = false;
                    CancelByToad = false;
                    yield break;
                }

                yield return null;
            }
            _unitActionController.CancelAction(1);
            CancelByAwake = false;
            CancelByConvert = false;
            CancelByToad = false;
        }

        private void setStateWalk()
        {
            /*if (CurrentState != ActionState.IDLE)
                return;
            SetDirectionAuto();
            CurrentState = ActionState.WALK;
            AppendCoroutine(updateWalk(++walkCoroutineId), ePauseType.SYSTEM, this);
            PlayAnime(moveRate == 0.0 ? eSpineCharacterAnimeId.IDLE : (isRunForCatchUp || StandByDone ? eSpineCharacterAnimeId.RUN : eSpineCharacterAnimeId.RUN_GAME_START), MotionPrefix);
        */
            if (CurrentState == ActionState.IDLE || CurrentState == ActionState.SUMMON)
            {
                ExecStartSkill();
                SetDirectionAuto();
                CurrentState = ActionState.WALK;
                AppendCoroutine(updateWalk(++walkCoroutineId), ePauseType.SYSTEM, this);
                eSpineCharacterAnimeId animeId = ((moveRate == 0f) ? eSpineCharacterAnimeId.IDLE : ((isRunForCatchUp || StandByDone) ? eSpineCharacterAnimeId.RUN : eSpineCharacterAnimeId.RUN_GAME_START));
                PlayAnime(animeId, MotionPrefix);
            }
        }

        private IEnumerator updateWalk(int coroutineId)
        {
            UnitCtrl unitCtrl = this;
            //MyOnChangeState?.Invoke(UnitId,CurrentState, BattleHeaderController.CurrentFrameCount,"");
            while (coroutineId == unitCtrl.walkCoroutineId)
            {
                switch (unitCtrl.CurrentState)
                {
                    case ActionState.SKILL_1:
                    case ActionState.DAMAGE:
                    case ActionState.DIE:
                        unitCtrl.isRunForCatchUp = false;
                        yield break;
                    default:
                        if (!unitCtrl.IsConfusionOrConvert())
                        {
                            if (unitCtrl.TargetEnemyList.Count > 0 || unitCtrl.IdleOnly)
                            {
                                unitCtrl.isRunForCatchUp = false;
                                unitCtrl.SetState(ActionState.IDLE);
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
                                unitCtrl.SetState(ActionState.IDLE);
                                yield break;
                            }

                            if (unitCtrl.targetPlayerList.Count > 1 || unitCtrl.IdleOnly)
                            {
                                unitCtrl.SetState(ActionState.IDLE);
                                yield break;
                            }
                        }
                        List<UnitCtrl> unitCtrlList1 = unitCtrl.IsOther ? unitCtrl.battleManager.UnitList : unitCtrl.battleManager.EnemyList;
                        bool flag1 = unitCtrlList1.FindIndex(e => (long)e.Hp > 0L) == -1;
                        bool flag2 = unitCtrlList1.TrueForAll(e => e.IsStealth || (long)e.Hp == 0L);
                        if (unitCtrl.battleManager.GameState == eBattleGameState.NEXT_WAVE_PROCESS)
                        {
                            flag1 = false;
                            flag2 = false;
                        }
                        BattleSpineController currentSpineCtrl = unitCtrl.GetCurrentSpineCtrl();
                        Vector3 localPosition = unitCtrl.transform.localPosition;
                        localPosition.x += (float)(unitCtrl.moveRate * (double)unitCtrl.DeltaTimeForPause * (unitCtrl.isRunForCatchUp ? 1.0 : 1.60000002384186));
                        if (!(flag1 | flag2))
                        {
                            if (currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, unitCtrl.MotionPrefix) && unitCtrl.moveRate != 0.0)
                                unitCtrl.PlayAnime(eSpineCharacterAnimeId.RUN, unitCtrl.MotionPrefix);
                            unitCtrl.transform.localPosition = localPosition;
                        }
                        else if (currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.RUN, unitCtrl.MotionPrefix))
                            unitCtrl.PlayAnime(eSpineCharacterAnimeId.IDLE, unitCtrl.MotionPrefix);
                        unitCtrl.SetDirectionAuto();
                        yield return null;
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
            if (MotionPrefix == 1 && !unitActionController.ModeChanging)
                return;
            isAwakeMotion = false;
            SetDirectionAuto();
            //if (this.idleEffects.Count > 0 && !this.IsDead)
            //    this.AppendCoroutine(this.CreatePrefabWithTime(this.idleEffects, true), ePauseType.VISUAL, this);
            if (CurrentSkillId == UnionBurstSkillId)
            {
                PlayUbChainVoice();
                for (int index = UbAbnormalDataList.Count - 1; index >= 0; --index)
                {
                    UbAbnormalDataList[index].Exec(this);
                    UbAbnormalDataList.RemoveAt(index);
                    OnChangeState.Call(this, eStateIconType.UB_DISABLE, false);
                    NoSkipOnChangeAbnormalState?.Invoke(this, eStateIconType.UB_DISABLE, false, 90, "???");
                }
            }
            foreach (KeyValuePair<eAbnormalState, Action<bool>> item in damageByBehaviourDictionary)
            {
                item.Value.Call(JEOCPILJNAD: false);
            }

            switch (CurrentState)
            {
                case ActionState.ATK:
                case ActionState.SKILL_1:
                case ActionState.SKILL:
                case ActionState.WALK:
                case ActionState.DAMAGE:
                case ActionState.GAME_START:
                case ActionState.SUMMON://新增
                    CurrentState = ActionState.IDLE;
                    if (IdleOnly && !ToadRelease)
                        battleManager.CallbackIdleOnlyDone(this);
                    if (StandByDone)
                    {
                        BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
                        //if (currentSpineCtrl.AnimationName != currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE) && !ToadRelease)
                        //    PlayAnime(eSpineCharacterAnimeId.IDLE, MotionPrefix);
                        if (SpecialIdleMotionId == 0)
                        {
                            if (currentSpineCtrl.AnimationName != currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE) && !ToadRelease)
                            {
                                PlayAnime(eSpineCharacterAnimeId.IDLE, MotionPrefix);
                            }
                        }
                        else if (currentSpineCtrl.AnimationName != currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.SPECIAL_IDLE, SpecialIdleMotionId) && !ToadRelease)
                        {
                            PlayAnime(eSpineCharacterAnimeId.SPECIAL_IDLE, SpecialIdleMotionId);
                        }
                        battleManager.CallbackStartDashDone(this);
                    }
                    else
                    {
                        PlayAnime(eSpineCharacterAnimeId.STAND_BY, MotionPrefix, _isLoop: false);
                        //if (this.IsStartVoicePlay)
                        //    BattleVoiceUtility.PlayWaveStartVoice(this);
                        AppendCoroutine(updateStandBy(), ePauseType.VISUAL, this);
                    }
                    ExecStartSkill();
                    AppendCoroutine(updateIdle(), ePauseType.SYSTEM, this);
                    break;
            }
        }

        public void ExecStartSkill()
        {
            if (startSkillExeced || IdleOnly)
            {
                return;
            }
            foreach (ExSkillData startExSkill in startExSkillList)
            {
                startExSkill.Exec(unitActionController);
            }
            startSkillExeced = true;
            GetCurrentSpineCtrl().SetTimeScale(1f);
        }

        private IEnumerator updateStandBy()
        {
            UnitCtrl FNHGFDNICFG = this;
            while (FNHGFDNICFG.GetCurrentSpineCtrl().IsPlayAnimeBattle)
            {
                if (FNHGFDNICFG.CurrentState != ActionState.IDLE)
                {
                    FNHGFDNICFG.StandByDone = true;
                    FNHGFDNICFG.battleManager.CallbackStanbyDone(FNHGFDNICFG);
                    yield break;
                }

                yield return null;
            }
            FNHGFDNICFG.StandByDone = true;
            FNHGFDNICFG.battleManager.CallbackStanbyDone(FNHGFDNICFG);
            if (FNHGFDNICFG.CurrentState == ActionState.IDLE)
                FNHGFDNICFG.PlayAnime(eSpineCharacterAnimeId.IDLE, FNHGFDNICFG.MotionPrefix);
        }

        private IEnumerator updateIdle()
        {
            UnitCtrl unitCtrl = this;
            //MyOnChangeState?.Invoke(UnitId,CurrentState, BattleHeaderController.CurrentFrameCount,"");
            if (unitCtrl.idleStartAfterWaitFrame)
            {
                unitCtrl.idleStartAfterWaitFrame = false;
                yield return null;
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

                    //if (unitCtrl.IdleOnly && unitCtrl.EnemyPoint != 0 && unitCtrl.battleManager.FICLPNJNOEP >= (int)HatsuneUtility.GetHatsuneSpecialBattle().purpose_count)
                    //    unitCtrl.SetState(UnitCtrl.ActionState.DIE);
                    if (unitCtrl.IdleOnly)
                    {
                        if (unitCtrl.CurrentState != ActionState.IDLE)
                        {
                            yield break;
                        }

                        if (unitCtrl.IsSummonOrPhantom)
                        {
                            List<UnitCtrl> unitCtrlList = unitCtrl.IsOther ? unitCtrl.battleManager.EnemyList : unitCtrl.battleManager.UnitList;
                            if (unitCtrlList.Contains(unitCtrl))
                            {
                                unitCtrlList.Remove(unitCtrl);
                                BattleManager.Instance.QueueUpdateSkillTarget();
                                if (!unitCtrl.IsOther)
                                    unitCtrl.battleManager.LPBCBINDJLJ.Add(unitCtrl);
                            }
                            fAlpha -= unitCtrl.battleManager.DeltaTime_60fps;
                            if (fAlpha <= 0.0 || BattleUtil.Approximately(fAlpha, 0.0f))
                            {
                                if (unitCtrl.OnDieFadeOutEnd != null && unitCtrl.OnDeadForRevival == null)
                                {
                                    unitCtrl.OnDieFadeOutEnd(unitCtrl);
                                    unitCtrl.OnDieFadeOutEnd = null;
                                }
                                if (unitCtrl.OnDeadForRevival != null)
                                {
                                    unitCtrl.OnDeadForRevival(unitCtrl);
                                    unitCtrl.OnDeadForRevival = null;
                                    yield break;
                                }

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

                                unitCtrl.battleManager.LPBCBINDJLJ.Remove(unitCtrl);
                                yield break;
                            }

                            unitCtrl.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, fAlpha);
                        }
                        yield return null;
                    }
                    else if (unitCtrl.attackPatternLoopDictionary[unitCtrl.currentActionPatternId].Count == 0)
                    {
                        yield return null;
                    }
                    else
                    {
                        if (unitCtrl.CurrentState == ActionState.IDLE)
                            unitCtrl.OnUpdateWhenIdle.Call(unitCtrl.battleManager.BattleLeftTime);
                        if (unitCtrl.CurrentState != ActionState.IDLE)
                            yield break;
                        if (unitCtrl.battleManager.BlackOutUnitList.Contains(unitCtrl))
                            yield return null;
                        else if (unitCtrl.judgeStateIsWalk())
                        {
                            yield break;
                        }
                        else
                        {
                            if ((unitCtrl.battleManager.ActionStartTimeCounter <= 0f || unitCtrl.IsOther ? (!unitCtrl.ToadRelease ? 1 : 0) : 0) != 0)
                            {
                                unitCtrl.m_fCastTimer = unitCtrl.m_fCastTimer - unitCtrl.DeltaTimeForPause;
                                if (unitCtrl.m_fCastTimer < 0.1 && unitCtrl.UnitNameEx.Contains("凯"))
                                {

                                }
                                if (!battleManager.skipping)
                                    NoSkipOnSkillCD?.Invoke(m_fCastTimer);
                            }
                            if (unitCtrl.battleManager.LOGNEDLPEIJ)
                                yield return null;
                            else if (unitCtrl.ToadRelease)
                                yield return null;
                            else if ((float)unitCtrl.m_fCastTimer <= 0f && unitCtrl.attackPatternDictionary != null)
                            {
                                unitCtrl.updateAttackTargetImpl();
                                if (unitCtrl.judgeStateIsWalk())
                                {
                                    yield break;
                                }

                                int _skillId = unitCtrl.attackPatternIsLoop ? unitCtrl.attackPatternLoopDictionary[unitCtrl.currentActionPatternId][unitCtrl.attackPatternIndex] : unitCtrl.attackPatternDictionary[unitCtrl.currentActionPatternId][unitCtrl.attackPatternIndex];
                                ActionState _state = _skillId != 1 ? ActionState.SKILL : ActionState.ATK;
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
                                if (!battleManager.skipping)
                                NoSkipOnChangeSkillID?.Invoke(_skillId, _nextSkillId, m_fCastTimer, 1);
                                yield break;
                            }
                            else if ((long)unitCtrl.Hp <= 0L)
                            {
                                unitCtrl.SetState(ActionState.DIE);
                                yield break;
                            }
                            else
                                yield return null;
                        }
                    }
                }
                yield return null;
            }
        }

        private bool judgeStateIsWalk()
        {
            if (!IsConfusionOrConvert())
            {
                if (TargetEnemyList.Count <= 0)
                {
                    isRunForCatchUp = true;
                    SetState(ActionState.WALK);
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
                    if (!unitCtrl.IsDead && unitCtrl != this)
                        flag = false;
                }
                int num = 0;
                List<UnitCtrl> unitCtrlList = IsOther ? battleManager.EnemyList : battleManager.UnitList;
                for (int index = 0; index < unitCtrlList.Count; ++index)
                {
                    if (!unitCtrlList[index].IsDead)
                        ++num;
                }
                if (num == 1)
                    flag = false;
                if (flag)
                {
                    SetState(ActionState.WALK);
                    return true;
                }
            }
            return false;
        }

        public void PlayDamageWhenIdle(bool _damageMotionWhenUnionBurst = false, bool _pauseStopState = false)
        {
            if (battleManager == null || ((CurrentState != ActionState.IDLE || ModeChangeEnd ? (battleManager.BlackoutUnitTargetList.Contains(this) ? 1 : 0) : 1) == 0 || battleManager.BlackOutUnitList.Contains(this) || (IsAbnormalState(eAbnormalState.STONE) || IsAbnormalState(eAbnormalState.PAUSE_ACTION))))
                return;
            BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
            TrackEntry current1 = currentSpineCtrl.state.GetCurrent(0);
            if (!isIdleDamage)
            {
                if (current1 != null)
                    resumeTime = current1.AnimationTime;
                animeLoop = currentSpineCtrl.loop;
                resumeIsStopState = currentSpineCtrl.IsStopState;
                animeName = currentSpineCtrl.AnimationName;
                isIdleDamage = true;
            }
            currentSpineCtrl.loop = false;
            string str = !IsPartsBoss || PartsMotionPrefix == 0 ? currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix) : currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, _index2: PartsMotionPrefix);
            if (battleManager.BlackoutUnitTargetList.Contains(this))
            {
                if (_damageMotionWhenUnionBurst)
                {
                    if (currentSpineCtrl.AnimationName != str)
                    {
                        currentSpineCtrl.AnimationName = str;
                        currentSpineCtrl.state.TimeScale = 1f;
                        TrackEntry current2 = currentSpineCtrl.state.GetCurrent(0);
                        current2.lastTime = battleManager.DeltaTime_60fps;
                        current2.time = battleManager.DeltaTime_60fps;
                        current2.animationLast = battleManager.DeltaTime_60fps;
                        //current2.animationLast = this.battleManager.DeltaTime_60fps;
                        //current2.animationStart = this.battleManager.DeltaTime_60fps;

                        current2.nextAnimationLast = battleManager.DeltaTime_60fps;
                        if (_pauseStopState)
                        {
                            current2.lastTime = currentSpineCtrl.StopStateTime;
                            current2.time = currentSpineCtrl.StopStateTime;
                            //current2.animationLast = currentSpineCtrl.StopStateTime;
                            //current2.animationStart = currentSpineCtrl.StopStateTime;

                            currentSpineCtrl.state.TimeScale = 0.0f;

                            AppendCoroutine(updateDamageWhenIdle(_damageMotionWhenUnionBurst), ePauseType.IGNORE_BLACK_OUT);

                            return;
                        }
                    }
                    if (_pauseStopState)
                        return;
                }
                else
                {
                    currentSpineCtrl.loop = true;
                    if (IsPartsBoss && PartsMotionPrefix != 0)
                        currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE_MULTI_TARGET, _index2: PartsMotionPrefix);
                    else
                        currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, MotionPrefix);
                    currentSpineCtrl.state.TimeScale = 1f;
                    TrackEntry current2 = currentSpineCtrl.state.GetCurrent(0);
                    current2.lastTime = 0.0f;
                    current2.time = 0.0f;
                    //current2.animationLast = 0;
                    //current2.animationStart = 0;
                    //currentSpineCtrl.state.Apply(currentSpineCtrl.skeleton);
                }
            }
            else
            {
                currentSpineCtrl.AnimationName = str;
                currentSpineCtrl.state.TimeScale = 1f;
            }
            AppendCoroutine(updateDamageWhenIdle(_damageMotionWhenUnionBurst), ePauseType.IGNORE_BLACK_OUT);
        }
        
        private IEnumerator updateDamageWhenIdle(bool _damageMotionWhenUnionBurst)
        {
            bool blackoutUnitContained = battleManager.BlackoutUnitTargetList.Contains(this);
            BattleSpineController currentSpineCtrl;
            while (true)
            {
                currentSpineCtrl = GetCurrentSpineCtrl();
                if (blackoutUnitContained && !battleManager.BlackoutUnitTargetList.Contains(this) && (long)Hp != 0L)
                {
                    resumeSpineWithTime();
                    if (isContinueIdleForPauseAction)
                    {
                        currentSpineCtrl.IsStopState = true;
                        currentSpineCtrl.Resume();
                        currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.IDLE, MotionPrefix);
                        currentSpineCtrl.Pause();
                    }
                    else if (isResumeSpecialSleepAnime())
                    {
                        currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.SLEEP, MotionPrefix, 1, 0);
                        currentSpineCtrl.Resume();
                        specialSleepStatus = eSpecialSleepStatus.LOOP;
                    }
                    else if (currentSpineCtrl.HasSpecialSleepAnimatilon(MotionPrefix) && IsAbnormalState(eAbnormalState.STONE) && IsAbnormalState(eAbnormalState.SLEEP))
                    {
                        isPlayDamageAnimForAbnormal = false;
                        currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix);
                        TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                        if (current != null)
                        {
                            current.lastTime = currentSpineCtrl.StopStateTime;
                            current.time = currentSpineCtrl.StopStateTime;
                        }
                        currentSpineCtrl.state.TimeScale = 0f;
                        currentSpineCtrl.IsStopState = resumeIsStopState;
                    }
                    isIdleDamage = false;
                    yield break;
                }
                if (CurrentState != 0 && !battleManager.BlackoutUnitTargetList.Contains(this))
                {
                    isIdleDamage = false;
                    yield break;
                }
                if (currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.JOY_LONG, MotionPrefix))
                {
                    yield break;
                }
                if (CurrentState == ActionState.IDLE && !currentSpineCtrl.IsPlayAnime)
                {
                    currentSpineCtrl.loop = true;
                    if (IsPartsBoss && PartsMotionPrefix != 0)
                    {
                        currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE_MULTI_TARGET, -1, PartsMotionPrefix);
                    }
                    else
                    {
                        currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, MotionPrefix);
                    }
                    isIdleDamage = false;
                    yield break;
                }
                if (!currentSpineCtrl.IsPlayAnime && _damageMotionWhenUnionBurst && (long)Hp != 0L && currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix))
                {
                    break;
                }
                yield return null;
            }
            currentSpineCtrl.loop = true;
            if (IsPartsBoss && PartsMotionPrefix != 0)
            {
                currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE_MULTI_TARGET, -1, PartsMotionPrefix);
            }
            else
            {
                currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, MotionPrefix);
            }
            
        }

        /*private void resumeSpineWithTime()
        {
            BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
            if (!(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, MotionPrefix)) && !(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix)) && (!(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, _index2: PartsMotionPrefix)) && !(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE_MULTI_TARGET, _index2: PartsMotionPrefix))) && !currentSpineCtrl.AnimationName.IsNullOrEmpty())
                return;
            if (resumeIsStopState && IsUnableActionState() && !isContinueIdleForPauseAction && (IsAbnormalState(eAbnormalState.STONE) || specialSleepStatus != eSpecialSleepStatus.START && specialSleepStatus != eSpecialSleepStatus.WAIT_START_END && specialSleepStatus != eSpecialSleepStatus.LOOP || !GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(MotionPrefix)))
            {
                currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix);
                TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                if (current != null)
                {
                    current.lastTime = currentSpineCtrl.StopStateTime;
                    current.time = currentSpineCtrl.StopStateTime;
                    //current.animationLast = currentSpineCtrl.StopStateTime;
                    //current.animationStart = currentSpineCtrl.StopStateTime;

                }
                currentSpineCtrl.state.TimeScale = 0.0f;
                currentSpineCtrl.IsStopState = resumeIsStopState;
                specialSleepStatus = eSpecialSleepStatus.INVALID;
            }
            else
            {
                bool flag = animeName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, MotionPrefix);
                if ((CurrentState == ActionState.IDLE ? 0 : (CurrentState != ActionState.DAMAGE ? 1 : 0)) != 0 || flag && !IsUnableActionState())
                {
                    currentSpineCtrl.loop = animeLoop;
                    currentSpineCtrl.AnimationName = animeName;
                    TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                    if (current != null)
                    {
                        current.lastTime = resumeTime;
                        current.time = resumeTime;
                        //current.animationLast = this.resumeTime;
                        //current.animationStart = this.resumeTime;

                    }
                    currentSpineCtrl.state.TimeScale = 1f;
                }
                currentSpineCtrl.IsStopState = resumeIsStopState;
            }
        }
        */
        private bool isResumeSpecialSleepAnime()
        {
            if (IsAbnormalState(eAbnormalState.STONE))
            {
                return false;
            }
            if (!isSpecialSleepStatus)
            {
                return false;
            }
            if (!GetCurrentSpineCtrl().HasSpecialSleepAnimatilon(MotionPrefix))
            {
                return false;
            }
            return true;
        }

        private bool isResumeDamageAnimationFromStopStateTime()
        {
            if (!resumeIsStopState && !isDamageReleaseSpecialSleepAnimForUnionBurst)
            {
                return false;
            }
            if (!IsUnableActionState())
            {
                return false;
            }
            if (isContinueIdleForPauseAction)
            {
                return false;
            }
            if (isResumeSpecialSleepAnime() && !isDamageReleaseSpecialSleepAnimForUnionBurst)
            {
                return false;
            }
            return true;
        }

        private void resumeSpineWithTime()
        {
            BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
            if (!(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, MotionPrefix)) && !(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix)) && !(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET, -1, PartsMotionPrefix)) && !(currentSpineCtrl.AnimationName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE_MULTI_TARGET, -1, PartsMotionPrefix)) && !currentSpineCtrl.AnimationName.IsNullOrEmpty())
            {
                return;
            }
            if (isResumeDamageAnimationFromStopStateTime())
            {
                if (isDamageReleaseSpecialSleepAnimForUnionBurst)
                {
                    currentSpineCtrl.PlayAnime(eSpineCharacterAnimeId.DAMEGE, MotionPrefix, _playLoop: false);
                }
                else
                {
                    currentSpineCtrl.AnimationName = currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.DAMEGE, MotionPrefix);
                }
                TrackEntry current = currentSpineCtrl.state.GetCurrent(0);
                if (current != null)
                {
                    current.lastTime = currentSpineCtrl.StopStateTime;
                    current.time = currentSpineCtrl.StopStateTime;
                }
                currentSpineCtrl.state.TimeScale = 0f;
                currentSpineCtrl.IsStopState = true;
                specialSleepStatus = eSpecialSleepStatus.INVALID;
                isDamageReleaseSpecialSleepAnimForUnionBurst = false;
                isPlayDamageAnimForAbnormal = false;
                return;
            }
            bool flag = animeName == currentSpineCtrl.ConvertAnimeIdToAnimeName(eSpineCharacterAnimeId.IDLE, MotionPrefix);
            if ((CurrentState != 0 && CurrentState != ActionState.DAMAGE) || (flag && !IsUnableActionState()))
            {
                currentSpineCtrl.loop = animeLoop;
                currentSpineCtrl.AnimationName = animeName;
                TrackEntry current2 = currentSpineCtrl.state.GetCurrent(0);
                if (current2 != null)
                {
                    current2.lastTime = resumeTime;
                    current2.time = resumeTime;
                }
                currentSpineCtrl.state.TimeScale = 1f;
            }
            currentSpineCtrl.IsStopState = resumeIsStopState;
        }
        public IEnumerator SetStateWithDelayForRevival(
          float delay,
          ActionState state,
          int nextSkillId = 0,
          int skillId = 0)
        {
            float time = 0.0f;
            while (true)
            {
                time += DeltaTimeForPause;
                if (time <= (double)delay && battleManager.GameState == eBattleGameState.PLAY)
                    yield return null;
                else
                    break;
            }
            //this.SetEnableColor();
            SetState(state, nextSkillId, skillId);
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

        private IEnumerator startShakeWithDelay(ShakeEffect shake, bool _ignorePause = false)
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
            battleManager.battleCameraEffect.StartShake(shake, (Skill)null, GEDLBPMPOKB);
        }

        public bool IsCancelActionState(bool _isSkill1, int _skillId = -1)
        {
            /*bool flag = false;
            if (CancelByAwake && CurrentTriggerSkillId != _skillId || (CancelByConvert || CancelByToad))
                flag = true;
            switch (CurrentState)
            {
                case ActionState.SKILL_1:
                    flag = !_isSkill1;
                    break;
                case ActionState.DAMAGE:
                case ActionState.DIE:
                    flag = true;
                    break;
            }
            return flag;*/
            bool result = false;
            if ((CancelByAwake && CurrentTriggerSkillId != _skillId) || CancelByConvert || CancelByToad)
            {
                result = true;
            }
            switch (CurrentState)
            {
                case ActionState.DAMAGE:
                case ActionState.DIE:
                case ActionState.LOSE:
                    result = true;
                    break;
                case ActionState.SKILL_1:
                    result = !_isSkill1;
                    break;
            }
            return result;
        }

        public void DisappearForDivision(bool _fadeOut, bool _useCoroutine)
        {
            List<UnitCtrl> unitCtrlList = IsOther ? battleManager.EnemyList : battleManager.UnitList;
            if (unitCtrlList.Contains(this))
            {
                BattleManager.Instance.QueueUpdateSkillTarget();
                unitCtrlList.Remove(this);
            }
            IdleOnly = true;
            CureAllAbnormalState();
            if (!_fadeOut)
                return;
            if (_useCoroutine)
                StartCoroutine(updateDivisionDisappear());
            else
                AppendCoroutine(updateDivisionDisappear(), ePauseType.VISUAL);
        }

        private IEnumerator updateDivisionDisappear()
        {
            UnitCtrl unitCtrl = this;
            if ((long)unitCtrl.Hp <= 0L)
            {
                while (unitCtrl.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                    yield return null;
            }
            float alpha = 1f;
            while (alpha > 0.0)
            {
                alpha -= unitCtrl.battleManager.DeltaTime_60fps;
                unitCtrl.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, alpha);
                yield return null;
            }
            unitCtrl.gameObject.SetActive(false);
        }

        public void StartUndoDivision(Action _callback, bool _isTimeOver)
        {
            if (_isTimeOver)
            {
                BattleSpineController currentSpineCtrl = GetCurrentSpineCtrl();
                currentSpineCtrl.Resume();
                currentSpineCtrl.CurColor = Color.white;
                SetSortOrderBack();
            }
            OnStartErrorUndoDivision(_isTimeOver);
            battleManager.StartCoroutine(waitUndoMotionEnd(_callback, _isTimeOver));
        }

        private IEnumerator waitUndoMotionEnd(Action _callback, bool _isTimeOver)
        {
            BattleSpineController unitSpineController = GetCurrentSpineCtrl();
            float deltaTimeAccumulated = 0.0f;
            while (true)
            {
                for (deltaTimeAccumulated += Time.deltaTime; deltaTimeAccumulated > 0.0 & _isTimeOver; deltaTimeAccumulated -= battleManager.DeltaTime_60fps)
                {
                    unitSpineController.RealUpdate();
                    unitSpineController.RealLateUpdate();
                }
                if (unitSpineController.IsPlayAnime)
                    yield return null;
                else
                    break;
            }
            _callback.Call();
        }

        //public void PlayDieEffect() => this.AppendCoroutine(this.CreatePrefabWithTime(this.dieEffects, _ignorePause: true), ePauseType.IGNORE_BLACK_OUT);
        public bool ModeChangeUnableStateBarrier
        {
            get;
            set;
        }

        public bool MultiTargetByTime
        {
            get;
            set;
        }

        public int SpecialIdleMotionId
        {
            get;
            set;
        }
        public int ThanksTargetUnitId { get; set; }
        public bool UbIsDisableByChangePattern { get; set; }
        public float UnionBurstCoolDownTime
        {
            get;
            set;
        }
        public int AnnihilationId { get; set; }
        
        public int UbCounter { get; set; }

        public bool IsCutInSkip { get; set; }

        public bool PlayCutInFlag { get; set; }

        public bool PlayingNoCutinMotion { get; set; }

        public int UbUsedCount { get; set; }

        public bool GetIsReduceEnergy()
        {
            foreach (KeyValuePair<eReduceEnergyType, bool> isReduceEnergy in IsReduceEnergyDictionary)
            {
                if (isReduceEnergy.Value)
                    return isReduceEnergy.Value;
            }
            return false;
        }

        public void InvokeSupportSkill(int _viewerId)
        {
            if (CutInFrameSet.SupportSkillUsed || battleManager.GameState != eBattleGameState.PLAY)
            {
                battleManager.CancelInvalidSupportSkill(this);
            }
            else
            {
                ResetPosForUserUnit(1);
                resetActionPatternAndCastTime();
                GetCurrentSpineCtrl().CurColor = Color.white;
                PlayAnime(eSpineCharacterAnimeId.RUN);
                SupportSkillEnd = false;
                SetLeftDirection(false);
                CutInFrameSet.CutInFrame = battleManager.FrameCount + 1;
                CutInFrameSet.ServerCutInFrame = -2;
                CutInFrameSet.SupportSkillUsed = true;
            }
        }

        public void InvokeSupportSkillInResume(bool isOwner, int viewerId)
        {
            ResetPosForUserUnit(1);
            resetActionPatternAndCastTime();
            GetCurrentSpineCtrl().CurColor = Color.white;
            PlayAnime(eSpineCharacterAnimeId.RUN);
            SupportSkillEnd = false;
            SetLeftDirection(false);
            CutInFrameSet.SupportSkillUsed = true;
        }

        public FloatWithEx lastEnergyBeforeUB;

        public eConsumeResult ConsumeEnergy()
        {
            if ((double)Energy < 1000.0)
                return eConsumeResult.FAILED;
            float energy = (float)(0.0 + 1000.0 * EnergyReduceRateZero / 100.0);
            if (!unitActionController.Skill1IsChargeTime)
            {
                lastEnergyBeforeUB = Energy;
                SetEnergy(energy, eSetEnergyType.BY_USE_SKILL);
                return eConsumeResult.SKILL_OK;
            }
            if (unitActionController.DisableUBByModeChange)
                return eConsumeResult.FAILED;
            if (unitActionController.Skill1Charging)
            {
                lastEnergyBeforeUB = Energy;
                SetEnergy(energy, eSetEnergyType.BY_USE_SKILL);
                unitActionController.Skill1Charging = false;
                return eConsumeResult.SKILL_RELEASE;
            }
            lastEnergyBeforeUB = Energy;
            SetEnergy((float)(UnitDefine.MAX_ENERGY - 1), eSetEnergyType.BY_USE_SKILL);
            return eConsumeResult.SKILL_CHARGE;
        }

        public eConsumeResult ConsumeEnergySimulate()
        {
            if ((double)Energy < 1000.0)
                return eConsumeResult.FAILED;
            if (!unitActionController.Skill1IsChargeTime)
                return eConsumeResult.SKILL_OK;
            if (unitActionController.DisableUBByModeChange)
                return eConsumeResult.FAILED;
            return unitActionController.Skill1Charging ? eConsumeResult.SKILL_RELEASE : eConsumeResult.SKILL_CHARGE;
        }

        public void StartCutIn()
        {
            battleManager.semanubmanager.UbExecCallback(battleManager.UnitList.IndexOf(this));
            battleManager.ubmanager.UbExecCallback(battleManager.UnitList.IndexOf(this));
            battleManager.scriptMgr?.UbExecCallback(UnitId);
            ++UbCounter;
            //this.battleCameraEffect.DAHAALGOJNA = Vector3.zero;
            for (int index = 0; index < battleManager.UnitList.Count; ++index)
                battleManager.UnitList[index].ThanksTargetUnitId = 0;
            battleManager.FinishBlackFadeOut(null);
            SetSortOrderFront();
            battleManager.CurrentCutinUnit = this;
            //this.soundManager.PlaySe(eSE.BTL_BUTTON_SKILL);
            //this.PlayCutInFlag = true;
            //MovieManager instance1 = ManagerSingleton<MovieManager>.Instance;
            bool skillPrincessForm = unitActionController.GetIsSkillPrincessForm(UnionBurstSkillId);
            //instance1.SetMirrorMode(skillPrincessForm && this.IsLeftDir == !this.battleManager.IsDefenceReplayMode);

            PlayCutInFlag = false;
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
            if (battleTimeScale.IsSpeedQuadruple)
                PlayCutInFlag = false;
            battleManager.FrameCountPause(this);
            if (skillPrincessForm)
            {
                //SingletonMonoBehaviour<BattleHeaderController>.Instance.PauseNoMoreTimeUp(true);
                BattleHeaderController.Instance.PauseNoMoreTimeUp(true);
                princessFormProcessor.Prepare();
                //Debug.LogError("UB特效鸽了！");
            }
            if (PlayCutInFlag && MovieId != 0)
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
                PlayingNoCutinMotion = true;
                battleManager.isUBExecing = true;
                battleManager.isPrincessFormSkill = skillPrincessForm;
                //SingletonMonoBehaviour<BattleHeaderController>.Instance.PauseNoMoreTimeUp(true);
                BattleHeaderController.Instance.PauseNoMoreTimeUp(true);
                //StartCoroutine(unitActionController.StartActionWithOutCutIn(this, UnionBurstSkillId, () =>
                //{
                    battleManager.isPrincessFormSkill = false;
                    PlayingNoCutinMotion = false;
                    battleManager.isUBExecing = false;
                    battleManager.OnCutInEnd(IsOther || IsSummonOrPhantom, this);
                //}));
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

        //public void StartAnnihilation() => StartCoroutine(unitActionController.StartAnnihilationSkillAnimation(++AnnihilationId));

        public bool IsSkillReady
        {
            get
            {
                if ((battleManager.BattleCategory == eBattleCategory.DUNGEON ||
                     battleManager.BattleCategory == eBattleCategory.TOWER ||
                     battleManager.BattleCategory == eBattleCategory.TOWER_REHEARSAL ||
                     battleManager.BattleCategory == eBattleCategory.TOWER_EX ||
                     battleManager.BattleCategory == eBattleCategory.TOWER_REPLAY ||
                     battleManager.BattleCategory == eBattleCategory.TOWER_EX_REPLAY ||
                     battleManager.BattleCategory == eBattleCategory.TOWER_CLOISTER ||
                     battleManager.BattleCategory == eBattleCategory.TOWER_CLOISTER_REPLAY) && !StandByDone || 
                     battleManager.IsSpecialBattle && battleManager.ActionStartTimeCounter > 0.0 || 
                     battleManager.LOGNEDLPEIJ || 
                     skillTargetList.Count == 0 || 
                     battleManager.GameState != eBattleGameState.PLAY || 
                     !battleManager.GetAdvPlayed() || 
                     ModeChangeEnd)
                    return false;
                /*switch (this.battleManager.GetPurpose())
                {
                    case eHatsuneSpecialPurpose.SHIELD:
                    case eHatsuneSpecialPurpose.ABSORBER:
                        if (this.IdleOnly)
                            return false;
                        break;
                }*/
                return ToadDatas.Count <= 0 && !IsAbnormalState(eAbnormalState.UB_SILENCE) &&
                       !UbIsDisableByChangePattern &&
                       ((CurrentState != ActionState.SKILL_1 ||
                         unitActionController.Skill1IsChargeTime) &&
                        (!IsUnableActionState() && CurrentState != ActionState.DAMAGE)) &&
                       (UnionBurstSkillId != 0 && !IsAbnormalState(eAbnormalState.SILENCE) &&
                        (!IsDead && CurrentState != ActionState.DIE) &&
                        (!IsConfusionOrConvert() && (long) Hp > 0L &&
                         (!isAwakeMotion && !unitActionController.DisableUBByModeChange))) &&
                       ((double) Energy >= 1000.0 && SkillLevels[UnionBurstSkillId] != 0 &&
                        !GetIsReduceEnergy()) && battleManager.LAMHAIODABF == 0;
                //return this.ToadDatas.Count <= 0 && !this.IsAbnormalState(UnitCtrl.eAbnormalState.UB_SILENCE) && (this.CurrentState != UnitCtrl.ActionState.SKILL_1 || this.unitActionController.Skill1IsChargeTime) && (!this.IsUnableActionState() && this.CurrentState != UnitCtrl.ActionState.DAMAGE && (this.UnionBurstSkillId != 0 && !this.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE))) && (!this.IsDead && this.CurrentState != UnitCtrl.ActionState.DIE && (!this.IsConfusionOrConvert() && (long)this.Hp > 0L) && (!this.isAwakeMotion && !this.unitActionController.DisableUBByModeChange && ((double)this.Energy >= 1000.0 && this.SkillLevels[this.UnionBurstSkillId] != 0))) && !this.GetIsReduceEnergy() && this.battleManager.LAMHAIODABF == 0;
            }
        }

        public bool IsSkillReadyExceptEnergy
        {
            get
            {
                if ((battleManager.BattleCategory == eBattleCategory.DUNGEON ||
                     battleManager.BattleCategory == eBattleCategory.TOWER ||
                     (battleManager.BattleCategory == eBattleCategory.TOWER_REHEARSAL ||
                      battleManager.BattleCategory == eBattleCategory.TOWER_EX) ||
                     (battleManager.BattleCategory == eBattleCategory.TOWER_REPLAY ||
                      battleManager.BattleCategory == eBattleCategory.TOWER_EX_REPLAY ||
                      battleManager.BattleCategory == eBattleCategory.TOWER_CLOISTER)
                        ? 1
                        : (battleManager.BattleCategory == eBattleCategory.TOWER_CLOISTER_REPLAY ? 1 : 0)) != 0 &&
                    !StandByDone || battleManager.IsSpecialBattle &&
                    battleManager.ActionStartTimeCounter > 0.0 ||
                    (battleManager.LOGNEDLPEIJ || skillTargetList.Count == 0 || (battleManager.GameState != eBattleGameState.PLAY || !battleManager.GetAdvPlayed())) || ModeChangeEnd)
                    return false;
                /*switch (this.battleManager.GetPurpose())
                {
                    case eHatsuneSpecialPurpose.SHIELD:
                    case eHatsuneSpecialPurpose.ABSORBER:
                        if (this.IdleOnly)
                            return false;
                        break;
                }*/
                return ToadDatas.Count <= 0 && !IsAbnormalState(eAbnormalState.UB_SILENCE) &&
                       !UbIsDisableByChangePattern &&
                       ((CurrentState != ActionState.SKILL_1 ||
                         unitActionController.Skill1IsChargeTime) &&
                        (!IsUnableActionState() && CurrentState != ActionState.DAMAGE)) &&
                       (UnionBurstSkillId != 0 && !IsAbnormalState(eAbnormalState.SILENCE) &&
                        (!IsDead && CurrentState != ActionState.DIE) &&
                        (!IsConfusionOrConvert() && (long)Hp > 0L &&
                         (!isAwakeMotion && !unitActionController.DisableUBByModeChange))) &&
                       ((double)1000.0 >= 1000.0 && SkillLevels[UnionBurstSkillId] != 0 &&
                        !GetIsReduceEnergy()) && battleManager.LAMHAIODABF == 0;
                //return this.ToadDatas.Count <= 0 && !this.IsAbnormalState(UnitCtrl.eAbnormalState.UB_SILENCE) && (this.CurrentState != UnitCtrl.ActionState.SKILL_1 || this.unitActionController.Skill1IsChargeTime) && (!this.IsUnableActionState() && this.CurrentState != UnitCtrl.ActionState.DAMAGE && (this.UnionBurstSkillId != 0 && !this.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE))) && (!this.IsDead && this.CurrentState != UnitCtrl.ActionState.DIE && (!this.IsConfusionOrConvert() && (long)this.Hp > 0L) && (!this.isAwakeMotion && !this.unitActionController.DisableUBByModeChange && ((double)this.Energy >= 1000.0 && this.SkillLevels[this.UnionBurstSkillId] != 0))) && !this.GetIsReduceEnergy() && this.battleManager.LAMHAIODABF == 0;
            }
        }

        public bool JudgeSkillReadyAndIsMyTurn() => isMyTurn() && IsSkillReady;
        public bool JudgeSkillReadyAndIsMyTurnExceptEnergy() => isMyTurn() && IsSkillReadyExceptEnergy;

        public bool pressing = false;
        private bool _isDead;
        private bool _hasUnDeadTime;
        private bool _idleOnly;
        public static Func<UnitCtrl, string> infoGetter;

        public void RebuildInfoGetter()
        {
            var infos = GuildManager.StaticsettingData.dispFields.Split('/');
            var propinfos = infos.Select(name => typeof(UnitCtrl).GetProperty(name)?.GetMethod).ToArray();
            var empty = new object[0];
            infoGetter = o => string.Join("/", propinfos.Select(p => p.Invoke(o, empty).ToString()));
        }

        public float Mid => transformCache.position.x / lossyx;
        public float Right => Mid + BodyWidth / 2;
        public float Left => Mid - BodyWidth / 2;

        public FloatWithEx DefForDamagedEnergy;
        public FloatWithEx MagicDefForDamagedEnergy;

        public FloatWithEx DefZeroForDamagedEnergy
        {
            get
            {
                return (Mathf.Max(0, this.DefForDamagedEnergy) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.DEF)).Floor();
            }
        }

        // Token: 0x17000BE4 RID: 3044
        // (get) Token: 0x0600386C RID: 14444 RVA: 0x000E3170 File Offset: 0x000E3170
        public FloatWithEx MagicDefZeroForDamagedEnergy
        {
            get
            {
                return (Mathf.Max(0, this.MagicDefForDamagedEnergy) + this.getAdditionalBuffDictionary(UnitCtrl.BuffParamKind.MAGIC_DEF)).Floor();
            }
        }

        public SealData UnableStateGuardData;

        public string GetInfo()
        {
            if (infoGetter == null) RebuildInfoGetter();
            return infoGetter(this);
        }
        public bool IsAutoOrUbExecTrying()
        {
            // Debug.Log($"frame = {battleManager.FrameCount} unit = {this.UnitId}, recast = {this.m_fCastTimer}");
            //if (this.IsUbExecTrying && (!this.battleManager.UnitUiCtrl.IsAutoMode || this.CurrentState != UnitCtrl.ActionState.IDLE) && this.JudgeSkillReadyAndIsMyTurn())
            if (IsUbExecTrying && (!MyGameCtrl.Instance.IsAutoMode || CurrentState != ActionState.IDLE) && JudgeSkillReadyAndIsMyTurn())
                //battleManager.SetOnlyAutoClearFlag(false);
                battleManager.DisableAutoClear();
            if (IsUbExecTrying)
                return true;
            if (pressing) return true;
            if (Index == -2) Index = battleManager.UnitList.IndexOf(this);
            if (battleManager.scriptMgr?.IsPressed(UnitId) ?? false)
                return true;
            if (battleManager.semanubmanager.IsUbExec(Index))
                return true;
            if (battleManager.ubmanager.IsUbExec(Index))
                return true;
            //return this.battleManager.UnitUiCtrl.IsAutoMode && this.CurrentState == UnitCtrl.ActionState.IDLE;
            
            if (Index >= 0 && Index <= 4 && Input.GetKey(KeyCode.Alpha5 - Index)) return true;

            if (CurrentState != ActionState.IDLE) return false;

            if (IsSummonOrPhantom && SummonSource != null && SummonSource.Hp < 0) return false;
            if (MyGameCtrl.Instance.IsAutoMode)
                return true;
            return IsSummonOrPhantom;
        }

        private bool isMyTurn() => battleManager.ChargeSkillTurn == eChargeSkillTurn.NONE || unitActionController.Skill1Charging;

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
            LOSE = 9,
            SUMMON = 10,

        }

        public delegate void OnDamageDelegate(bool byAttack, FloatWithEx damage, bool critical);

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
            //TOP = 1,
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
            [Description("嘲讽")]
            DECOY = 26, // 0x0000001A
            [Description("MIFUYU")]
            MIFUYU = 27, // 0x0000001B
            [Description("石化")]
            STONE = 28, // 0x0000001C
            [Description("再生")]
            REGENERATION = 29, // 0x0000001D
            [Description("物理闪避")]
            PHYSICS_DODGE = 30, // 0x0000001E
            [Description("混乱")]
            CONFUSION = 31, // 0x0000001F
            [Description("毒")]
            VENOM = 32, // 0x00000020
            [Description("黑暗次数")]
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
            [Description("UB沉默")]
            UB_SILENCE = 48, // 0x00000030
            [Description("魔法黑暗")]
            MAGIC_DARK = 49, // 0x00000031
            [Description("治疗下降")]
            HEAL_DOWN = 50, // 0x00000032
            [Description("击晕NPC")]
            NPC_STUN = 51, // 0x00000033
            DECREASE_HEAL = 52,
            POISON_BY_BEHAVIOUR = 53,
            CRYSTALIZE = 54,
            DAMAGE_LIMIT_ATK = 55,
            DAMAGE_LIMIT_MGC = 56,
            DAMAGE_LIMIT_ALL = 57,
            STUN2 = 58,
            POISON2 = 59,
            CURSE2 = 60,
            CONFUSION2 = 61,
            NO_DAMAGE_MOTION2 = 62,
            REGENERATION2 = 0x3F,
            TP_REGENERATION2 = 0x40,
            [Description("减速v2")]
            SLOW_OVERLAP = 65,
            [Description("加速v2")]
            HASTE_OVERLAP = 66,
            SPY = 67,
            // Token: 0x040027E3 RID: 10211
            ENERGY_DAMAGE_REDUCE,
            // Token: 0x040027E4 RID: 10212
            BLACK_FRAME,
            // Token: 0x040027E5 RID: 10213
            UNABLE_STATE_GUARD,

            FLIGHT = 71,
            ACCUMULATIVE_DAMAGE_FOR_ALL_ENEMY = 72,
            WORLD_LIGHTNING = 73,
            NUM,


        }

        public class eAbnormalState_DictComparer : IEqualityComparer<eAbnormalState>
        {
            public bool Equals(eAbnormalState _x, eAbnormalState _y) => _x == _y;

            public int GetHashCode(eAbnormalState _obj) => (int)_obj;
        }

        public enum eAbnormalStateCategory
        {
            NONE = 0,
            DAMAGE_RESISTANCE_MGK = 1,
            DAMAGE_RESISTANCE_ATK = 2,
            DAMAGE_RESISTANCE_BOTH = 3,
            POISON = 4,
            BURN = 5,
            CURSE = 6,
            SPEED = 7,
            FREEZE = 8,
            PARALYSIS = 9,
            STUN = 10,
            DETAIN = 11,
            CONVERT = 12,
            SILENCE = 13,
            PHYSICAL_DARK = 14,
            MAGIC_DARK = 0xF,
            MIFUYU = 0x10,
            DECOY = 17,
            NO_DAMAGE = 18,
            NO_ABNORMAL = 19,
            NO_DEBUF = 20,
            SLEEP = 21,
            CHAINED = 22,
            ACCUMULATIVE_DAMAGE = 23,
            NO_EFFECT_SLIP_DAMAGE = 24,
            STONE = 25,
            REGENERATION = 26,
            PHYSICS_DODGE = 27,
            CONFUSION = 28,
            VENOM = 29,
            COUNT_BLIND = 30,
            INHIBIT_HEAL = 0x1F,
            FEAR = 0x20,
            TP_REGENERATION = 33,
            HEX = 34,
            FAINT = 35,
            PARTS_NO_DAMAGE = 36,
            COMPENSATION = 37,
            CUT_ATK_DAMAGE = 38,
            CUT_MGC_DAMAGE = 39,
            CUT_ALL_DAMAGE = 40,
            LOG_ATK_BARRIR = 41,
            LOG_MGC_BARRIR = 42,
            LOG_ALL_BARRIR = 43,
            PAUSE_ACTION = 44,
            UB_SILENCE = 45,
            HEAL_DOWN = 46,
            NPC_STUN = 47,
            DECREASE_HEAL = 48,
            POISON_BY_BEHAVIOUR = 49,
            CRYSTALIZE = 50,
            DAMAGE_LIMIT_ALL = 51,
            DAMAGE_LIMIT_MGC = 52,
            DAMAGE_LIMIT_ATK = 53,
            STUN2 = 54,
            POISON2 = 55,
            CURSE2 = 56,
            CONFUSION2 = 57,
            NO_DAMAGE2 = 58,
            REGENERATION2 = 59,
            TP_REGENERATION2 = 60,
            SPEED_OVERLAP = 61,
            SPY = 62,
            // Token: 0x04002829 RID: 10281
            ENERGY_DAMAGE_REDUCE,
            // Token: 0x0400282A RID: 10282
            BLACK_FRAME,
            NUM,
            TOP = 0,
            END = NUM
        }
        public class eAbnormalStateCategory_DictComparer : IEqualityComparer<eAbnormalStateCategory>
        {
            public bool Equals(eAbnormalStateCategory _x, eAbnormalStateCategory _y) => _x == _y;

            public int GetHashCode(eAbnormalStateCategory _obj) => (int)_obj;
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
            [Description("降暴")]
            RECEIVE_CRITICAL_DAMAGE_RATE = 14,
            [Description("受到暴伤提升")]
            RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT = 0xF,
            [Description("受到物暴伤提升")]
            RECEIVE_PHYSICAL_DAMAGE_PERCENT = 0x10,
            [Description("受到魔暴伤提升")]
            RECEIVE_MAGIC_DAMAGE_PERCENT = 17,
            MAX_HP = 100, // 0x00000064
            NONE = 101, // 0x00000065
            NUM = 101, // 0x00000065
        }

        public class BuffParamKind_DictComparer : IEqualityComparer<BuffParamKind>
        {
            public bool Equals(BuffParamKind _x, BuffParamKind _y) => _x == _y;

            public int GetHashCode(BuffParamKind _obj) => (int)_obj;
        }

        public class FunctionalComparer<T> : IComparer<T>
        {
            private Func<T, T, int> comparer;
            public static FunctionalComparer<T> Instance;

            public FunctionalComparer(Func<T, T, int> _comparer) => comparer = _comparer;

            public int Compare(T x, T y) => comparer(x, y);

            public void SetComparer(Func<T, T, int> _comparer) => comparer = _comparer;

            public static void CreateInstance() => Instance = new FunctionalComparer<T>(null);
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

        public class eReduceEnergyType_DictComparer : IEqualityComparer<eReduceEnergyType>
        {
            public bool Equals(eReduceEnergyType _x, eReduceEnergyType _y) => _x == _y;

            public int GetHashCode(eReduceEnergyType _obj) => (int)_obj;
        }


        private static int compareHigherAtkOrMagicStrDec(BasePartsData _a, BasePartsData _b)
        {
            if (_a == null)
            {
                if (_b != null)
                {
                    return -1;
                }
                return 0;
            }
            if (_b != null)
            {
                return Math.Max(_b.GetAtkZero(), _b.GetMagicStrZero()).CompareTo(Math.Max(_a.GetAtkZero(), _a.GetMagicStrZero()));
            }
            return -1;
        }

        private static int compareHigherAtkOrMagicaStrAsc(BasePartsData _a, BasePartsData _b)
        {
            if (_a == null)
            {
                if (_b != null)
                {
                    return -1;
                }
                return 0;
            }
            if (_b != null)
            {
                return Math.Max(_a.GetAtkZero(), _a.GetMagicStrZero()).CompareTo(Math.Max(_b.GetAtkZero(), _b.GetMagicStrZero()));
            }
            return 1;
        }

        public int CompareHigherAtkOrMagicStrDecSameNear(BasePartsData _a, BasePartsData _b)
        {
            if (_a == null)
            {
                if (_b != null)
                {
                    return -1;
                }
                return 0;
            }
            if (_b == null)
            {
                return -1;
            }
            if (Math.Max(_a.GetAtkZero(), _a.GetMagicStrZero()) == Math.Max(_b.GetAtkZero(), _b.GetMagicStrZero()))
            {
                return CompareDistanceAsc(_a, _b);
            }
            return compareHigherAtkOrMagicStrDec(_a, _b);
        }

        public int CompareHigherAtkOrMagicStrAscSameNear(BasePartsData _a, BasePartsData _b)
        {
            if (_a == null)
            {
                if (_b != null)
                {
                    return -1;
                }
                return 0;
            }
            if (_b == null)
            {
                return 1;
            }
            if (Math.Max(_a.GetAtkZero(), _a.GetMagicStrZero()) == Math.Max(_b.GetAtkZero(), _b.GetMagicStrZero()))
            {
                return CompareDistanceAsc(_a, _b);
            }
            return compareHigherAtkOrMagicaStrAsc(_a, _b);
        }
        public void DispelAbnormalState(eAbnormalState _abnormalState)
        {
            if (IsAbnormalState(_abnormalState))
            {
                EnableAbnormalState(_abnormalState, _enable: false);
            }
        }

        public LinkedListNode<ChangeParameterFieldData> EnableBuffDebuffField(ChangeParameterFieldData _data)
        {
            return buffDebuffFieldDataList.AddLast(_data);
        }

        public void DisableBuffDebuffField(LinkedListNode<ChangeParameterFieldData> _data)
        {
            buffDebuffFieldDataList.Remove(_data);
        }

        public int CompareHpAscSameLeft(BasePartsData _a, BasePartsData _b)
        {
            if (BattleUtil.Approximately(_a.Owner.Hp / _a.Owner.MaxHp, _b.Owner.Hp / _b.Owner.MaxHp))
            {
                return this.CompareLeft(_a, _b);
            }

            return UnitCtrl.CompareLifeAsc(_a, _b);
        }

        public static int CompareAtkDefAsc(BasePartsData _a, BasePartsData _b) => _a.GetDefZero().CompareTo(_b.GetDefZero());
        public static int CompareMagicDefAsc(BasePartsData _a, BasePartsData _b) => _a.GetMagicDefZero().CompareTo(_b.GetMagicDefZero());


        // Token: 0x06003958 RID: 14680 RVA: 0x000EB24C File Offset: 0x000EB24C
        public int CompareHpAscSameRight(BasePartsData _a, BasePartsData _b)
        {
            if (BattleUtil.Approximately(_a.Owner.Hp / _a.Owner.MaxHp, _b.Owner.Hp / _b.Owner.MaxHp))
            {
                return this.CompareRight(_a, _b);
            }
            return UnitCtrl.CompareLifeAsc(_a, _b);
        }
        // Token: 0x0600396B RID: 14699 RVA: 0x000EB7EC File Offset: 0x000EB7EC
        public int CompareAtkDefAscSameLeft(BasePartsData _a, BasePartsData _b)
        {
            int num = UnitCtrl.CompareAtkDefAsc(_a, _b);
            if (num == 0)
            {
                return this.CompareLeft(_a, _b);
            }
            return num;
        }

        // Token: 0x0600396C RID: 14700 RVA: 0x000EB810 File Offset: 0x000EB810
        public int CompareAtkDefAscSameRight(BasePartsData _a, BasePartsData _b)
        {
            int num = UnitCtrl.CompareAtkDefAsc(_a, _b);
            if (num == 0)
            {
                return this.CompareRight(_a, _b);
            }
            return num;
        }

        // Token: 0x0600396B RID: 14699 RVA: 0x000EB7EC File Offset: 0x000EB7EC
        public int CompareMagicDefAscSameLeft(BasePartsData _a, BasePartsData _b)
        {
            int num = UnitCtrl.CompareMagicDefAsc(_a, _b);
            if (num == 0)
            {
                return this.CompareLeft(_a, _b);
            }
            return num;
        }

        // Token: 0x0600396C RID: 14700 RVA: 0x000EB810 File Offset: 0x000EB810
        public int CompareMagicDefAscSameRight(BasePartsData _a, BasePartsData _b)
        {
            int num = UnitCtrl.CompareMagicDefAsc(_a, _b);
            if (num == 0)
            {
                return this.CompareRight(_a, _b);
            }
            return num;
        }
        public Dictionary<int, AttackSealDataForAllEnemy> AttackSealDataDictionary { get; set; } = new Dictionary<int, AttackSealDataForAllEnemy>();

    }

}
