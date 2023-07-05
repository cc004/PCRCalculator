using Cute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCRCaculator;

namespace Elements
{
    public partial class UnitActionController
    {

        private ActionParameter createActionParameter(MasterSkillAction.SkillAction _action, object _exSkillScriptable, ExSkillData _exSkillData)
        {
            ActionParameter actionParameter = Activator.CreateInstance(SkillDefine.SkillActionTypeDictionary[(eActionType)(ushort)_action.action_type]) as ActionParameter;
            actionParameter.TargetSort = (PriorityPattern)(int)_action.target_type;
            actionParameter.TargetNth = _action.target_number;
            actionParameter.TargetNum = _action.target_count;
            actionParameter.TargetWidth = (((int)_action.target_range <= 0) ? Owner.SearchAreaSize : ((float)(int)_action.target_range));
            actionParameter.ActionType = (eActionType)(ushort)_action.action_type;
            actionParameter.ActionDetail1 = _action.action_detail_1;
            actionParameter.ActionDetail2 = _action.action_detail_2;
            actionParameter.ActionDetail3 = _action.action_detail_3;
            actionParameter.Value = new Dictionary<eValueNumber, FloatWithEx>(new eValueNumber_DictComparer());
            actionParameter.Value.Add(eValueNumber.VALUE_1, (float)(double)_action.action_value_1);
            actionParameter.Value.Add(eValueNumber.VALUE_2, (float)(double)_action.action_value_2);
            actionParameter.Value.Add(eValueNumber.VALUE_3, (float)(double)_action.action_value_3);
            actionParameter.Value.Add(eValueNumber.VALUE_4, (float)(double)_action.action_value_4);
            actionParameter.Value.Add(eValueNumber.VALUE_5, (float)(double)_action.action_value_5);
            actionParameter.Value.Add(eValueNumber.VALUE_6, (float)(double)_action.action_value_6);
            actionParameter.Value.Add(eValueNumber.VALUE_7, (float)(double)_action.action_value_7);
            actionParameter.MasterData = _action;
            actionParameter.ActionEffectList = new List<NormalSkillEffect>();
            actionParameter.ActionExecTimeList = new List<ActionExecTime>
    {
        new ActionExecTime
        {
            Weight = 1f,
            DamageNumType = eDamageEffectType.NORMAL
        }
    };
            actionParameter.ActionWeightSum = 1f;
            actionParameter.ExecTime = new float[1];
            // actionParameter.KnockAnimationCurve = _exSkillScriptable.KnockAnimationCurve;
            // actionParameter.KnockDownAnimationCurve = _exSkillScriptable.KnockDownAnimationCurve;
            // actionParameter.EffectType = _exSkillScriptable.EffectType;
            actionParameter.DepenedActionId = _action.DependActionId;
            actionParameter.ActionId = _action.action_id;
            actionParameter.TargetList = new List<BasePartsData>();
            actionParameter.ActionChildrenIndexes = new List<int>();
            actionParameter.TargetAssignment = (eTargetAssignment)(int)_action.target_assignment;
            actionParameter.Direction = (DirectionType)(int)_action.target_area;
            if (actionParameter.ActionType == eActionType.EX_START_PASSIVE)
            {
                _exSkillData.ExSkillConditionData.Add(eExSkillCondition.START, new ExConditionPassiveData());
            }
            else if (actionParameter.ActionType == eActionType.EX_CONDITION_PASSIVE)
            {
                _exSkillData.ExSkillConditionData.Add((eExSkillCondition)actionParameter.ActionDetail1, new ExConditionPassiveData
                {
                    EffectType = (ExConditionPassiveData.eEffectType)actionParameter.ActionDetail2,
                    LimitNum = actionParameter.ActionDetail3,
                    HitRate = actionParameter.Value[eValueNumber.VALUE_1] + actionParameter.Value[eValueNumber.VALUE_2] * (float)(int)Owner.Level,
                    Value = actionParameter.Value[eValueNumber.VALUE_3],
                    CoolTime = actionParameter.Value[eValueNumber.VALUE_4],
                    IconType = (eStateIconType)(int)actionParameter.Value[eValueNumber.VALUE_5]
                });
            }
            return actionParameter;
        }

        public Skill CreateExSkill(int _id, object _exSkillScriptable, ExSkillData _exSkillData)
        {
            Skill skill = new Skill();
            skill.ActionParameters = new List<ActionParameter>();
            var skillData = new MasterSkillData.SkillData(MainManager.Instance.SkillDataDic[_id]);


            foreach (var action in skillData.ActionDataList)
            {
                skill.ActionParameters.Add(createActionParameter(action, _exSkillScriptable, _exSkillData));
            }

            foreach (KeyValuePair<eExSkillCondition, ExConditionPassiveData> kv in _exSkillData.ExSkillConditionData)
            {
                _exSkillData.LimitNumMax = Math.Max(_exSkillData.LimitNumMax, kv.Value.LimitNum);
                Owner.OnChangeState.Call(Owner, kv.Value.IconType, true);
                switch (kv.Key)
                {
                    case eExSkillCondition.DODGE:
                    {
                        UnitCtrl owner4 = Owner;
                        owner4.OnDodge = (Action) Delegate.Combine(owner4.OnDodge,
                            (Action) delegate { _exSkillData.TryExSkill(eExSkillCondition.DODGE, this, true); });
                        break;
                    }
                    case eExSkillCondition.DAMAGED:
                    {
                        UnitCtrl owner5 = Owner;
                        owner5.OnDamage = (UnitCtrl.OnDamageDelegate) Delegate.Combine(owner5.OnDamage,
                            (UnitCtrl.OnDamageDelegate) delegate(bool byAttack, FloatWithEx damage, bool critical)
                            {
                                if (byAttack)
                                {
                                    _exSkillData.TryExSkill(eExSkillCondition.DAMAGED, this, true);
                                }
                            });
                        break;
                    }
                    case eExSkillCondition.CRITICAL_DAMAGE:
                    {
                        UnitCtrl owner3 = Owner;
                        owner3.OnDamage = (UnitCtrl.OnDamageDelegate) Delegate.Combine(owner3.OnDamage,
                            (UnitCtrl.OnDamageDelegate) delegate(bool byAttack, FloatWithEx damage, bool critical)
                            {
                                if (byAttack && critical)
                                {
                                    _exSkillData.TryExSkill(eExSkillCondition.CRITICAL_DAMAGE, this, true);
                                }
                            });
                        break;
                    }
                    case eExSkillCondition.TRIGGER_HP:
                    {
                        UnitCtrl owner2 = Owner;
                        owner2.OnHpChange = (UnitCtrl.OnDamageDelegate) Delegate.Combine(owner2.OnHpChange,
                            (UnitCtrl.OnDamageDelegate) delegate
                            {
                                if ((float) (long) Owner.Hp <= kv.Value.Value / 100f * (float) (long) Owner.MaxHp)
                                {
                                    _exSkillData.TryExSkill(eExSkillCondition.TRIGGER_HP, this, true);
                                }
                            });
                        break;
                    }
                    case eExSkillCondition.LIMIT_TIME:
                    {
                        UnitCtrl owner = Owner;
                        owner.OnUpdateWhenIdle = (Action<float>) Delegate.Combine(owner.OnUpdateWhenIdle,
                            (Action<float>) delegate(float _limitTime)
                            {
                                if (_limitTime <= kv.Value.Value)
                                {
                                    _exSkillData.TryExSkill(eExSkillCondition.LIMIT_TIME, this, true);
                                }
                            });
                        break;
                    }
                }
            }

            _exSkillData.LimitNum = _exSkillData.LimitNumMax;
            dependActionSolve(skill);
            initializeAction(skill);
            execActionOnStart(skill);
            return skill;
        }
    }
}
