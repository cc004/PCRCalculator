using Cute;
using Elements.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using PCRCaculator.Guild;

namespace Elements
{
    public class ExConditionPassiveData
    {
        public enum eEffectType
        {
            NORMAL,
            CANCEL,
            SHORTEN
        }

        public eEffectType EffectType { get; set; }

        public int LimitNum { get; set; }

        public float HitRate { get; set; }

        public float Value { get; set; }

        public float CoolTime { get; set; }

        public eStateIconType IconType { get; set; }
    }

    public enum eExSkillCondition
    {
        START = 0,
        DODGE = 1,
        DAMAGED = 2,
        TRIGGER_HP = 3,
        CRITICAL_DAMAGE = 5,
        LIMIT_TIME = 7,
        UNABLE_ACTION = 100,
        PARALYSIS = 103,
        FREEZE = 104,
        CHAINED = 105,
        SLEEP = 106,
        STUN = 107,
        STONE = 108,
        PAUSE_ACTION = 111,
        SLIP_DAMAGE = 120,
        POISON = 121,
        BURN = 122,
        CURSE = 123,
        VENOM = 124,
        HEX = 125,
        POISON_OR_VENOM = 141,
        CURSE_OR_HEX = 143,
        CONVERT = 150,
        CONFUSION = 151
    }

    public class ExSkillData
    {
        public enum eTryExResult
        {
            FAILED,
            SUCCESS,
            CANCEL,
            SHORTEN
        }

        public static readonly Dictionary<UnitCtrl.eAbnormalState, List<eExSkillCondition>> ABNORMAL_CONDITION_PAIR = new Dictionary<UnitCtrl.eAbnormalState, List<eExSkillCondition>>
    {
        {
            UnitCtrl.eAbnormalState.PARALYSIS,
            new List<eExSkillCondition>
            {
                eExSkillCondition.PARALYSIS,
                eExSkillCondition.UNABLE_ACTION
            }
        },
        {
            UnitCtrl.eAbnormalState.FREEZE,
            new List<eExSkillCondition>
            {
                eExSkillCondition.FREEZE,
                eExSkillCondition.UNABLE_ACTION
            }
        },
        {
            UnitCtrl.eAbnormalState.CHAINED,
            new List<eExSkillCondition>
            {
                eExSkillCondition.CHAINED,
                eExSkillCondition.UNABLE_ACTION
            }
        },
        {
            UnitCtrl.eAbnormalState.SLEEP,
            new List<eExSkillCondition>
            {
                eExSkillCondition.SLEEP,
                eExSkillCondition.UNABLE_ACTION
            }
        },
        {
            UnitCtrl.eAbnormalState.STUN,
            new List<eExSkillCondition>
            {
                eExSkillCondition.STUN,
                eExSkillCondition.UNABLE_ACTION
            }
        },
        {
            UnitCtrl.eAbnormalState.STONE,
            new List<eExSkillCondition>
            {
                eExSkillCondition.STONE,
                eExSkillCondition.UNABLE_ACTION
            }
        },
        {
            UnitCtrl.eAbnormalState.PAUSE_ACTION,
            new List<eExSkillCondition>
            {
                eExSkillCondition.PAUSE_ACTION,
                eExSkillCondition.UNABLE_ACTION
            }
        },
        {
            UnitCtrl.eAbnormalState.POISON,
            new List<eExSkillCondition>
            {
                eExSkillCondition.POISON,
                eExSkillCondition.SLIP_DAMAGE,
                eExSkillCondition.POISON_OR_VENOM
            }
        },
        {
            UnitCtrl.eAbnormalState.BURN,
            new List<eExSkillCondition>
            {
                eExSkillCondition.BURN,
                eExSkillCondition.SLIP_DAMAGE
            }
        },
        {
            UnitCtrl.eAbnormalState.CURSE,
            new List<eExSkillCondition>
            {
                eExSkillCondition.CURSE,
                eExSkillCondition.SLIP_DAMAGE,
                eExSkillCondition.CURSE_OR_HEX
            }
        },
        {
            UnitCtrl.eAbnormalState.VENOM,
            new List<eExSkillCondition>
            {
                eExSkillCondition.VENOM,
                eExSkillCondition.SLIP_DAMAGE,
                eExSkillCondition.POISON_OR_VENOM
            }
        },
        {
            UnitCtrl.eAbnormalState.HEX,
            new List<eExSkillCondition>
            {
                eExSkillCondition.HEX,
                eExSkillCondition.SLIP_DAMAGE,
                eExSkillCondition.CURSE_OR_HEX
            }
        },
        {
            UnitCtrl.eAbnormalState.CONVERT,
            new List<eExSkillCondition> { eExSkillCondition.CONVERT }
        },
        {
            UnitCtrl.eAbnormalState.CONFUSION,
            new List<eExSkillCondition> { eExSkillCondition.CONFUSION }
        }
    };

        private List<ActionParameter> actionParameterList = new List<ActionParameter>();

        private UnitCtrl owner;

        //private ExSkillScriptable exSkillScriptable;

        private Skill skill;

        private float coolTime;

        public int LimitNumMax;

        public int LimitNum;

        public Dictionary<eExSkillCondition, ExConditionPassiveData> ExSkillConditionData { get; private set; } = new Dictionary<eExSkillCondition, ExConditionPassiveData>();


        public void Initialize(int _id, UnitCtrl _owner, UnitActionController _actionController, BattleManager _battleManager)
        {
            owner = _owner;
            //exSkillScriptable = ManagerSingleton<ResourceManager>.Instance.LoadResourceImmediately(eResourceId.EQUIPMANT_SKILL, _id) as ExSkillScriptable;
            skill = _actionController.CreateExSkill(_id, null, this);
            _battleManager.AppendCoroutine(updateCoolTime(_battleManager), ePauseType.SYSTEM);
        }

        private IEnumerator updateCoolTime(BattleManager _battleManager)
        {
            while (true)
            {
                if (_battleManager.GetBlackOutUnitLength() == 0)
                {
                    if (coolTime > 0f)
                    {
                        coolTime -= _battleManager.FNHFJLAENPF;
                    }
                    if (coolTime <= 0f)
                    {
                        using (Dictionary<eExSkillCondition, ExConditionPassiveData>.Enumerator enumerator = ExSkillConditionData.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                CallFunctionExtend.Call(JEOCPILJNAD: enumerator.Current.Value.IconType, DMFGKJIEEBF: owner.OnChangeStatePassiveEnable, IDAFJHFJKOL: true);
                            }
                        }
                    }
                }
                yield return null;
            }
        }

        public void Exec(UnitActionController _actionController)
        {
            _actionController.ExecExSkill(skill);
        }

        public eTryExResult TryExSkill(eExSkillCondition _skillCondition, UnitActionController _actionController, bool _all, ExConditionPassiveData.eEffectType _targetEffectType = ExConditionPassiveData.eEffectType.NORMAL)
        {
            ExConditionPassiveData exConditionPassiveData = ExSkillConditionData[_skillCondition];
            if (!_all && _targetEffectType != exConditionPassiveData.EffectType)
            {
                return eTryExResult.FAILED;
            }
            if (coolTime > 0f)
            {
                return eTryExResult.FAILED;
            }
            if (LimitNum == 0)
            {
                return eTryExResult.FAILED;
            }
            if (BattleManager.Random(0f, 1f,
                    new RandomData(owner, owner, 0, 17, exConditionPassiveData.HitRate)
                    ) > exConditionPassiveData.HitRate)
            {
                return eTryExResult.FAILED;
            }
            LimitNum--;
            if (LimitNum == 0)
            {
                owner.OnChangeState.Call(owner, exConditionPassiveData.IconType, false);
            }
            coolTime = exConditionPassiveData.CoolTime;
            if (coolTime > 0f)
            {
                owner.OnChangeStatePassiveEnable.Call(exConditionPassiveData.IconType, false);
            }
            _actionController.ExecExSkill(skill);
            switch (exConditionPassiveData.EffectType)
            {
                case ExConditionPassiveData.eEffectType.CANCEL:
                    return eTryExResult.CANCEL;
                case ExConditionPassiveData.eEffectType.SHORTEN:
                    return eTryExResult.SHORTEN;
                default:
                    return eTryExResult.SUCCESS;
            }
        }

        private void OnChangeState(eStateIconType icon, bool flag)
        {
            owner.OnChangeState(owner, icon, flag);
        }

        public void ResetLimitNum(UnitCtrl _owner)
        {
            LimitNum = LimitNumMax;
            coolTime = 0f;
            using (Dictionary<eExSkillCondition, ExConditionPassiveData>.Enumerator enumerator = ExSkillConditionData.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CallFunctionExtend.Call(JEOCPILJNAD: enumerator.Current.Value.IconType, DMFGKJIEEBF: OnChangeState, IDAFJHFJKOL: true);
                }
            }
        }

        public void DeleteIcon(UnitCtrl _owner)
        {
            using (Dictionary<eExSkillCondition, ExConditionPassiveData>.Enumerator enumerator = ExSkillConditionData.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CallFunctionExtend.Call(JEOCPILJNAD: enumerator.Current.Value.IconType, DMFGKJIEEBF: OnChangeState, IDAFJHFJKOL: false);
                }
            }
        }
    }
}
