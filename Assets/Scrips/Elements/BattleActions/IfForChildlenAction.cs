﻿using System;
using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;

namespace Elements
{
    public class IfForChildlenAction : ActionParameter
    {
        public const int IF_DIGIT = 100;

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

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            if (ActionDetail1 != 1001)
                return;
            _skill.CriticalPartsList = new List<BasePartsData>();
        }

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictinary,
          System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictinary);
            if (!_enabledChildAction.ContainsKey(ActionDetail2))
                _enabledChildAction.Add(ActionDetail2, false);
            if (!_enabledChildAction.ContainsKey(ActionDetail3))
                _enabledChildAction.Add(ActionDetail3, false);
            bool flag = false;
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
                    flag = _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.CURSE);
                    break;
                case eIfType.POISON:
                    flag = _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.POISON);
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
                    flag = TargetList.FindAll(e => IfForAllAction.JudgeCountableUnit(e.Owner)).Exists(e => e.Owner.UnitId == (double)_valueDictinary[eValueNumber.VALUE_3]);
                    break;
                case eIfType.CRITICAL:
                    flag = _skill.CriticalPartsList.Contains(_target);
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
                    else if (base.ActionDetail1 >= 1600)
                    {
                        flag = isAbnormalState((eIfAbnormalState)(base.ActionDetail1 % 100), _target);
                        break;
                    }
                    else if (base.ActionDetail1 > 1500)
                    {
                        UnitCtrl.eAbnormalState abnormalState = ChangeSpeedAction.AbnormalStateDic[(ChangeSpeedAction.eChangeSpeedType)(base.ActionDetail1 % 100)];
                        flag = _target.Owner.IsAbnormalState(abnormalState);
                        break;
                    }
                    if (ActionDetail1 > 900)
                    {
                        flag = (long)_target.Owner.Hp / (double)(long)_target.Owner.MaxHp < ActionDetail1 % 100 / 100.0;

                        var now = _target.Owner.Hp / _target.Owner.MaxHp;
                        var bound = ActionDetail1 % 100 / 100.0;

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
                    if (ActionDetail1 > 700)
                    {
                        flag = TargetList.FindAll(e => IfForAllAction.JudgeCountableUnit(e.Owner)).Count == ActionDetail1 - 700;
                        break;
                    }
                    /*if (ActionDetail1 > 600)
                    {
                        eStateIconType key = (eStateIconType)(ActionDetail1 - 600);
                        flag = _target.Owner.SealDictionary.ContainsKey(key) && ((double)_valueDictinary[eValueNumber.VALUE_3] != 0.0 ? _target.Owner.SealDictionary[key].GetCurrentCount() >= (double)_valueDictinary[eValueNumber.VALUE_3] : _target.Owner.SealDictionary[key].GetCurrentCount() > 0);
                    }*/
                    break;
            }
            if (flag)
            {
                _enabledChildAction[ActionDetail2] = true;
                _skill.EffectBranchId = (int)Value[eValueNumber.VALUE_1];
            }
            else
            {
                _enabledChildAction[ActionDetail3] = true;
                _skill.EffectBranchId = (int)Value[eValueNumber.VALUE_2];
            }
            _sourceActionController.AppendCoroutine(_sourceActionController.UpdateBranchMotion(this, _skill), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
            action($"条件判断类{((eIfType)ActionDetail1).GetDescription()}");
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
    }
    public enum eIfAbnormalState
    {
        FEAR,
        SPY
    }
}
