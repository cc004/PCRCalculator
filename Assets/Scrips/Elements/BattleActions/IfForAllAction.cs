using System;
using System.Collections;
using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;

namespace Elements
{
    public class IfForAllAction : ActionParameter
    {
        public const int IF_DIGIT = 100;
        public const int IF_DIGIT_COUNT = 10;

        public static bool JudgeCountableUnit(UnitCtrl _unit) => (long)_unit.Hp != 0L && !_unit.IsStealth;

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            switch ((eIfType)ActionDetail1)
            {
                case eIfType.DEFEAT:
                    ActionParameter actionParameter1 = _skill.ActionParameters[0];
                    int index = 0;
                    for (int count = _skill.ActionParameters.Count; index < count; ++index)
                    {
                        ActionParameter actionParameter2 = _skill.ActionParameters[index];
                        actionParameter2.OnDefeatEnemy += () =>
                        {
                            _skill.DefeatByThisSkill = true;
                            _skill.EffectBranchId = (int)Value[eValueNumber.VALUE_1];
                            if (ActionDetail3 == 0)
                                return;
                            _skill.ActionParameters.Find(e => e.ActionId == ActionDetail3).CancelByIfForAll = true;
                        };
                        if (actionParameter2.ActionId != ActionDetail2 && actionParameter2.ActionId != ActionDetail3 && actionParameter2 != this)
                            actionParameter1 = actionParameter1.ExecTime[actionParameter1.ExecTime.Length - 1] > (double)actionParameter2.ExecTime[actionParameter2.ExecTime.Length - 1] ? actionParameter1 : actionParameter2;
                    }
                    actionParameter1.OnActionEnd += () => _source.AppendCoroutine(waitFrameEnd(_skill), ePauseType.SYSTEM, _source);
                    actionParameter1.OnInitWhenNoTarget += () =>
                    {
                        if (ActionDetail2 == 0)
                            return;
                        _skill.EffectBranchId = (int)Value[eValueNumber.VALUE_2];
                        _skill.ActionParameters.Find(e => e.ActionId == ActionDetail2).CancelByIfForAll = true;
                    };
                    break;
                case eIfType.CRITICAL:
                    _skill.CriticalPartsList = new List<BasePartsData>();
                    break;
            }
        }

        private IEnumerator waitFrameEnd(Skill _skill)
        {
            IfForAllAction ifForAllAction = this;
            yield return null;
            if (!_skill.DefeatByThisSkill && ifForAllAction.ActionDetail2 != 0)
            {
                _skill.EffectBranchId = (int)ifForAllAction.Value[eValueNumber.VALUE_2];
                // ISSUE: reference to a compiler-generated method
                _skill.ActionParameters.Find(a => a.ActionId == ActionDetail2).CancelByIfForAll = true;
            }
        }

        public override void ReadyAction(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            base.ReadyAction(_source, _sourceActionController, _skill);
            if (ActionDetail1 != 1001)
                return;
            _skill.CriticalPartsList.Clear();
        }

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictinary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictinary);
            bool flag = false;
            if (ActionDetail1 == 1000)
                return;
            FloatWithEx prob = null;
            switch ((eIfType)ActionDetail1)
            {
                case eIfType.STOP:
                    flag = _target.Owner.IsUnableActionState();
                    break;
                case eIfType.HASTE:
                    flag = _target.Owner.IsHasteSpeed();
                    break;
                case eIfType.SLOW:
                    flag = _target.Owner.IsSlowSpeed();
                    break;
                case eIfType.PHYSICS_BLIND:
                    flag = _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.PHYSICS_DARK);
                    break;
                case eIfType.CONVERT:
                    flag = _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.CONVERT);
                    break;
                case eIfType.DECOY:
                    flag = _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.DECOY);
                    break;
                case eIfType.BURN:
                    flag = _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.BURN);
                    break;
                case eIfType.CURSE:
                    flag = _target.Owner.IsCurseState();
                    break;
                case eIfType.POISON:
                    flag = _target.Owner.IsPoisonState();
                    break;
                case eIfType.VENOM:
                    flag = _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.VENOM);
                    break;
                case eIfType.HEX:
                    flag = _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.HEX);
                    break;
                case eIfType.CURSE_OR_HEX:
                    flag = _target.Owner.IsCurseState() || _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.HEX);
                    break;
                case eIfType.POISON_OR_VENOM:
                    flag = _target.Owner.IsPoisonState() || _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.VENOM);
                    break;

                case eIfType.SLIP_DAMAGE:
                    flag = _target.Owner.IsSlipDamageState();
                    break;
                case eIfType.ALONE:
                    List<UnitCtrl> unitCtrlList = _source.IsOther ? battleManager.UnitList : battleManager.EnemyList;
                    flag = !unitCtrlList.Exists(e => e.IsPartsBoss && !e.IsDead) && unitCtrlList.FindAll(e => (!e.IsDead && (long)e.Hp > 0L || e.HasUnDeadTime) && !e.IsStealth).Count == 1;
                    break;
                case eIfType.BREAK:
                    if (_target.Owner.IsPartsBoss)
                    {
                        for (int index = 0; index < _target.Owner.BossPartsListForBattle.Count; ++index)
                        {
                            if (_target.Owner.BossPartsListForBattle[index].IsBreak)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    break;
                case eIfType.UNIT_ID:
                    flag = TargetList.FindAll(e => JudgeCountableUnit(e.Owner)).Exists(e => e.Owner.SoundUnitId == (double)_valueDictinary[eValueNumber.VALUE_3]);
                    break;
                case eIfType.SEAL_COUNT:
                    {
                        eStateIconType key = (eStateIconType)(int)base.Value[eValueNumber.VALUE_3];
                        HashSet<UnitCtrl> hashSet = new HashSet<UnitCtrl>();
                        for (int i = 0; i < base.TargetList.Count; i++)
                        {
                            UnitCtrl owner = base.TargetList[i].Owner;
                            if (owner.SealDictionary.TryGetValue(key, out var value2) && value2.GetCurrentCount() != 0)
                            {
                                hashSet.Add(owner);
                            }
                        }
                        flag = hashSet.Count >= BattleUtil.FloatToInt(base.Value[eValueNumber.VALUE_4]);
                        break;
                    }
                case eIfType.CRITICAL:
                    flag = _skill.CriticalPartsList.Count > 0;
                    break;
                case eIfType.ATK_TYPE:
                    flag = _target.Owner.AtkType == 1;
                    break;
                case eIfType.TOAD:
                    flag = _target.Owner.ToadDatas.Count > 0;
                    break;
                case eIfType.BUFF_DEBUFF:
                    {
                        UnitCtrl.BuffParamKind changeParamKind = BuffDebuffAction.GetChangeParamKind((int)_valueDictinary[eValueNumber.VALUE_3]);
                        flag = _target.Owner.IsBuffDebuff(changeParamKind, (int)_valueDictinary[eValueNumber.VALUE_3] % 10 != 1);
                        break;
                    }
                case eIfType.MULTI_BOSS:
                    flag = _target.Owner.IsPartsBoss;
                    break;
                case eIfType.BARRIER:
                    flag = _target.Owner.IsBarrierState();
                    break;
                default:
                    if (ActionDetail1 < 100)
                    {
                        flag = BattleManager.Random(0.0f, 100f, new RandomData(_source, _target.Owner, ActionId, 12, ActionDetail1)) < (double)ActionDetail1;
                        break;
                    }
                    else if ((base.ActionDetail1 > 600 && base.ActionDetail1 < 700) || base.ActionDetail1 > 6000)
                    {
                        eStateIconType eStateIconType = eStateIconType.INVALID_VALUE;
                        eStateIconType = (eStateIconType)((base.ActionDetail1 <= 6000) ? (base.ActionDetail1 - 600) : (base.ActionDetail1 - 6000));
                        flag = _target.Owner.SealDictionary.ContainsKey(eStateIconType) && ((_valueDictinary[eValueNumber.VALUE_3] != 0f) ? ((float)_target.Owner.SealDictionary[eStateIconType].GetCurrentCount() >= _valueDictinary[eValueNumber.VALUE_3]) : (_target.Owner.SealDictionary[eStateIconType].GetCurrentCount() > 0));
                        break;
                    }
                    else if ((ActionDetail1 & ~1) == 2000)
                    {
                        var filterUnit = new HashSet<UnitCtrl>();
                        int filterType = (ActionDetail1 == 2000 ? 1 : 2);
                        for (int index = 0; index < TargetList.Count; ++index)
                        {
                            if (TargetList[index].Owner.AtkType == filterType)
                                filterUnit.Add(TargetList[index].Owner);
                        }
                        int threshold = (int)_valueDictinary[eValueNumber.VALUE_3];
                        if (threshold <= filterUnit.Count)
                        {
                            flag = true;
                        }
                    }
                    else if (ActionDetail1 >= 3000)
                    {
                        flag = battleManager.ExistsEnvironment(ActionDetail1 - 3000);
                    }
                    else if (ActionDetail1 >= 1600)
                    {
                        flag = isAbnormalState((eIfAbnormalState)(ActionDetail1 % 100), _target);
                        break;
                    }
                    else if (ActionDetail1 > 1200)
                    {
                        int num = 0;
                        _target.Owner.SkillExecCountDictionary.TryGetValue(ActionDetail1 % 100 / 10, out num);
                        flag = num == ActionDetail1 % 10;
                        break;
                    }
                    else if (ActionDetail1 > 900)
                    {
                        flag = (long)_target.Owner.Hp / (double)(long)_target.Owner.MaxHp < ActionDetail1 % 100 / 100.0;
                        var now = _target.Owner.Hp / _target.Owner.MaxHp;
                        var bound = ActionDetail1 % 100 / 100.0;
                        prob = now.Select(x => x < bound ? 1 : 0, $"lt,{bound}");

                        if (flag)
                            GuildCalculator.Instance.dmglist.Add(new ProbEvent()
                            {
                                isProb = false,
                                unit = _source.UnitNameEx,
                                predict = hash => now.Emulate(hash) >= bound,
                                exp = hash => $"{now.ToExpression(hash)} > {bound}",
                                description = $"({BattleHeaderController.CurrentFrameCount})对角色{_target.Owner.UnitNameEx}血量判定条件分歧失败（实际小于{bound}）"
                            });
                        else
                            GuildCalculator.Instance.dmglist.Add(new ProbEvent()
                            {
                                isProb = false,
                                unit = _source.UnitNameEx,
                                predict = hash => now.Emulate(hash) < bound,
                                exp = hash => $"{now.ToExpression(hash)} < {bound}",
                                description = $"({BattleHeaderController.CurrentFrameCount})对角色{_target.Owner.UnitNameEx}血量判定条件分歧失败（实际大于等于{bound}）"
                            });

                        break;
                    }
                    else if (ActionDetail1 > 700)
                    {
                        flag = TargetList.FindAll(e => JudgeCountableUnit(e.Owner)).Count == ActionDetail1 - 700;
                        break;
                    }
                    /*if (ActionDetail1 > 600)
                    {
                        eStateIconType key = (eStateIconType)(ActionDetail1 - 600);
                        flag = _target.Owner.SealDictionary.ContainsKey(key) && ((double)_valueDictinary[eValueNumber.VALUE_3] != 0.0 ? _target.Owner.SealDictionary[key].GetCurrentCount() >= (double)_valueDictinary[eValueNumber.VALUE_3] : _target.Owner.SealDictionary[key].GetCurrentCount() > 0);
                    }*/
                    break;
            }

            if ((object)prob != null)
            {
                ActionParameter positiveAction = null, negativeAction = null;
                if (ActionDetail2 != 0)
                    positiveAction = _skill.ActionParameters.Find(e => e.ActionId == ActionDetail2);
                if (ActionDetail3 != 0)
                    negativeAction = _skill.ActionParameters.Find(e => e.ActionId == ActionDetail3);
                
                // we only deal with actions with same types here, for we can't virtually execute an action
                if (positiveAction == null || negativeAction == null ||
                    positiveAction.ActionType == negativeAction.ActionType &&
                    positiveAction.ActionChildrenIndexes.Count == 0 && negativeAction.ActionChildrenIndexes.Count == 0)
                {
                    if (positiveAction != null)
                    {
                        positiveAction.oppositeAction = negativeAction;
                        positiveAction.oppositeActionProb = 1 - prob; // assert (float)prob == 1
                    }
                    if (negativeAction != null)
                    {
                        negativeAction.oppositeAction = positiveAction;
                        negativeAction.oppositeActionProb = prob; // assert (float)prob == 0
                    }
                    
                    // for now, whether we cancel the positive action or the negative action should've have
                    // the same effect, so we can execute the non-null action to ensure our calculation is correct.
                    
                    // when the only action is cancelled
                    if ((flag ? ActionDetail2 : ActionDetail3) == 0)
                        // revert the branch
                        flag ^= true;
                }
            }
            
            if (!flag)
            {
                _skill.EffectBranchId = (int)Value[eValueNumber.VALUE_2];
                if (ActionDetail2 != 0)
                    cancelAction(_skill.ActionParameters.Find(e => e.ActionId == ActionDetail2), _skill);
            }
            else
            {
                _skill.EffectBranchId = (int)Value[eValueNumber.VALUE_1];
                if (ActionDetail3 != 0)
                    cancelAction(_skill.ActionParameters.Find(e => e.ActionId == ActionDetail3), _skill);
            }
            _sourceActionController.AppendCoroutine(_sourceActionController.UpdateBranchMotion(this, _skill), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);

        }
        private bool isAbnormalState(eIfAbnormalState _abnormalState, BasePartsData _target)
        {
            return _abnormalState switch
            {
                eIfAbnormalState.FEAR => _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.FEAR),
                eIfAbnormalState.SPY => _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.SPY),
                _ => false,
            };
        }
        private void cancelAction(ActionParameter _action, Skill _skill)
        {
            _action.CancelByIfForAll = true;
            if (!(_action is IfForAllAction))
                return;
            (_action as IfForAllAction).CancelBoth(_skill);
        }

        public void CancelBoth(Skill _skill)
        {
            if (ActionDetail2 != 0)
                cancelAction(_skill.ActionParameters.Find(e => e.ActionId == ActionDetail2), _skill);
            if (ActionDetail3 == 0)
                return;
            cancelAction(_skill.ActionParameters.Find(e => e.ActionId == ActionDetail3), _skill);
        }
    }
}
