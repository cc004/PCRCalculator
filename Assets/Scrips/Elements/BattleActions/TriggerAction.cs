﻿// Decompiled with JetBrains decompiler
// Type: Elements.TriggerAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cute;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
    public class TriggerAction : ActionParameter
    {
        private const int TOUGH_VALUE = 99;
        private const int NO_MOTION_VALUE = 1;
        public override void ExecActionOnWaveStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            base.ExecActionOnWaveStart(_skill, _source, _sourceActionController);
            PartsData partsData = _source.BossPartsListForBattle.Find((PartsData e) => e.Index == _skill.ParameterTarget);
            eTriggerType actionDetail = (eTriggerType)base.ActionDetail1;
            if (actionDetail == eTriggerType.BREAK)
            {
                _source.PartsBreakTimeDictionary.Add(partsData.Index, base.Value[eValueNumber.VALUE_3]);
            }
        }
        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            PartsData parts = _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget);
            if (_source.MultiBossPartsData != null)
            {
                parts = _source.MultiBossPartsData;
            }
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            UnitCtrl target = _source;
            if (ActionDetail2 != 0)
            {
                target = (!_source.IsOther ? battleManager.UnitList : battleManager.EnemyList).Find(e => e.UnitId == ActionDetail2);
                if (target == null)
                    return;
            }
            bool actionDone = false;
            int actionCount = 0;
            switch (ActionDetail1)
            {
                case 1:
                    target.OnDodge += () =>
                    {
                        float num = BattleManager.Random(0.0f, 1f, new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f));
                        if (_source.TargetEnemyList.Count == 0 || judgeSilenceOrToad(_source) || _source.CurrentState == UnitCtrl.ActionState.SKILL && _source.CurrentSkillId == _skill.SkillId || num > (double)Value[eValueNumber.VALUE_1] / 100.0)
                            return;
                        if (_source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum))
                            _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                        else
                            _sourceActionController.StartAction(_skill.SkillId);
                    };
                    break;
                case 2:
                    target.OnDamage += (byAtack, damage, critical) =>
                    {
                        if (!byAtack)
                            return;
                        float num = BattleManager.Random(0.0f, 1f, new RandomData(_source,null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f));
                        if (_source.TargetEnemyList.Count == 0 || judgeSilenceOrToad(_source) || _source.CurrentState == UnitCtrl.ActionState.SKILL && _source.CurrentSkillId == _skill.SkillId || num > (double)Value[eValueNumber.VALUE_1] / 100.0)
                            return;
                        if (_source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum))
                            _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                        else
                            _sourceActionController.StartAction(_skill.SkillId);
                    };
                    break;
                case 3:
                    if ((double)Value[eValueNumber.VALUE_4] == 99.0)
                        target.IsTough = true;
                    if ((double)Value[eValueNumber.VALUE_3] == 0.0)
                        target.HasUnDeadTime = true;
                    target.OnHpChange = (byAttack, damage, critical) =>
                    {
                        float num = BattleManager.Random(0.0f, 1f, new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f));
                        if (_source.HasUnDeadTime)
                        {
                            if (_source.UnDeadTimeHitCount > 1)
                            {
                                --_source.UnDeadTimeHitCount;
                                if (_source.UnDeadTimeHitCount == 1)
                                    _source.SetState(UnitCtrl.ActionState.DIE);
                            }
                            else
                                actionDone = false;
                        }
                        if (judgeSilenceOrToad(_source) || _source.IsUnableActionState() || (_source.IsDead || num > (double)Value[eValueNumber.VALUE_1] / 100.0) || (actionDone || (long)_source.Hp > (double)Value[eValueNumber.VALUE_3] / 100.0 * (long)_source.MaxHp))
                            return;
                        actionDone = true;
                        Action<bool> action = _waitUbTurn =>
                        {
                            if (judgeSilenceOrToad(_source) || _source.IsUnableActionState())
                            {
                                actionDone = false;
                            }
                            else
                            {
                                if (_skill.BlackOutTime > 0.0 & _waitUbTurn)
                                    battleManager.OCAMDIOPEFP = true;
                                _source.SkillUseCount[_skill.SkillId]++;
                                _source.CancelByAwake = true;
                                _source.CurrentTriggerSkillId = _skill.SkillId;
                                if (_skill.BlackOutTime > 0.0)
                                {
                                    battleManager.GamePause(true);
                                    battleManager.SetSkillExeScreen(_source, _skill.BlackOutTime, _skill.BlackoutColor, _skill.BlackoutEndWithMotion);
                                    _source.SetSortOrderFront();
                                }
                                _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                            }
                        };
                        if (_source.CurrentState == UnitCtrl.ActionState.SKILL_1)
                            _source.AppendCoroutine(waitStateIdle(action, _source), ePauseType.SYSTEM, _source);
                        else if (battleManager.ChargeSkillTurn == eChargeSkillTurn.NONE)
                            action.Call(false);
                        else
                            _source.AppendCoroutine(waitChargeSkillTurnNone(action, _source), ePauseType.SYSTEM, _source);
                    };
                    break;
                case 4:
                    if (ActionDetail3 != 0 && target.SkillUseCount[_skill.SkillId] >= ActionDetail3)
                        break;
                    target.OnDeadForRevival += owner =>
                    {
                        //if (target.EnemyPoint != 0 && this.battleManager.FICLPNJNOEP >= (int) HatsuneUtility.GetHatsuneSpecialBattle().purpose_count || this.judgeSilenceOrToad(_source))
                        // return;
                        _source.SkillUseCount[_skill.SkillId]++;
                        if (BattleManager.Random(0.0f, 1f,
                                new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) <= (double)Value[eValueNumber.VALUE_1] / 100.0)
                        {
                            if (ActionDetail3 != 0 && actionCount >= ActionDetail3)
                                return;
                            int skillId = _skill.SkillId;
                            ++actionCount;
                            if (ActionDetail3 != 0 && actionCount == ActionDetail3)
                                target.OnDeadForRevival = null;
                            if (skillId == owner.UnionBurstSkillId)
                                _sourceActionController.AppendCoroutine(owner.SetStateWithDelayForRevival(ExecTime[0], UnitCtrl.ActionState.SKILL_1, skillId: skillId), ePauseType.SYSTEM, owner);
                            else
                                _sourceActionController.AppendCoroutine(owner.SetStateWithDelayForRevival(ExecTime[0], UnitCtrl.ActionState.SKILL, skillId: skillId), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? owner : null);
                        }
                        else
                        {
                            _source.IsDead = true;
                            battleManager.CallbackDead(_source);
                            _source.gameObject.SetActive(false);
                            battleManager.CallbackFadeOutDone(_source);
                        }
                    };
                    break;
                case 5:
                    target.OnDamage += (byAtack, damage, critical) =>
                    {
                        if (_source.IsDead || _source.IdleOnly || (judgeSilenceOrToad(_source) || !critical) || BattleManager.Random(0.0f, 1f,
                                new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)Value[eValueNumber.VALUE_1] / 100.0)
                            return;
                        if ((double)Value[eValueNumber.VALUE_4] != 1.0)
                        {
                            _source.CancelByAwake = true;
                            _source.CurrentTriggerSkillId = _skill.SkillId;
                            _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                        }
                        else
                            _sourceActionController.StartAction(_skill.SkillId);
                    };
                    break;
                case 6:
                    target.OnDamage += (byAtack, damage, critical) =>
                    {
                        bool flag = false;
                        foreach (KeyValuePair<int, UnitCtrl> summonUnit in target.SummonUnitDictionary)
                        {
                            if (!summonUnit.Value.IsDead)
                                flag = true;
                        }
                        if (!flag || _source.IsDead || (_source.IdleOnly || judgeSilenceOrToad(_source)) || (!critical || BattleManager.Random(0.0f, 1f,
                                new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)Value[eValueNumber.VALUE_1] / 100.0))
                            return;
                        _source.CancelByAwake = true;
                        _source.CurrentTriggerSkillId = _skill.SkillId;
                        _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                    };
                    break;
                case 7:
                    bool used = false;
                    target.OnUpdateWhenIdle = (Action<float>)Delegate.Combine(target.OnUpdateWhenIdle, (Action<float>)delegate(float _limitTime)
                    {
                        if (!judgeSilenceOrToad(_source) && !used && _limitTime <= Value[eValueNumber.VALUE_3])
                        {
                            used = true;
                        if (BattleManager.Random(0.0f, 1f, new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)Value[eValueNumber.VALUE_1] / 100.0)
                            return;
                        _source.CancelByAwake = true;
                        _source.CurrentTriggerSkillId = _skill.SkillId;
                        if (_skill.BlackOutTime > 0f)
                        {
                            battleManager.GamePause(true);
                            battleManager.SetSkillExeScreen(_source, _skill.BlackOutTime, _skill.BlackoutColor, _skill.BlackoutEndWithMotion);
                            _source.SetSortOrderFront();
                        }
                        _source.SetState(UnitCtrl.ActionState.SKILL, 0, _skill.SkillId);
                        }
                    });
                    break;
                case 8:
                    bool used2 = false;
                    _source.IsTough = (double)Value[eValueNumber.VALUE_3] == 0.0;
                    _source.OnUpdateWhenIdle += _limitTime =>
                    {
                        if (judgeSilenceOrToad(_source) || used2 || (!_source.IsStealth || _source.ActionsTargetOnMe.Count != 0))
                            return;
                        used2 = true;
                        if (BattleManager.Random(0.0f, 1f, new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)Value[eValueNumber.VALUE_1] / 100.0)
                            return;
                        _source.CancelByAwake = true;
                        _source.CurrentTriggerSkillId = _skill.SkillId;
                        _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                    };
                    break;
                case 9:
                    parts.BreakEffectList = _skill.SkillEffects;
                    _source.OnUpdateWhenIdle += _limitTime =>
                    {
                        var point = parts.BreakPoint;
                        if (parts.BreakPoint != 0 || parts.IsBreak ||
                            (judgeSilenceOrToad(_source) || battleManager.ChargeSkillTurn != eChargeSkillTurn.NONE) ||
                            BattleManager.Random(0.0f, 1f,
                                new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) >
                            (double) Value[eValueNumber.VALUE_1] / 100.0)
                        {
                            if (parts.IsBreak ||
                                (judgeSilenceOrToad(_source) ||
                                 battleManager.ChargeSkillTurn != eChargeSkillTurn.NONE))
                                return;

                            GuildCalculator.Instance.dmglist.Add(new ProbEvent
                            {
                                unit = _source.UnitNameEx,
                                predict = hash => point.Emulate(hash) <= 0f,
                                exp = hash => $"{point.ToExpression(hash)} <= 0",
                                description = $"({BattleHeaderController.CurrentFrameCount}){_source.UnitNameEx}的第{parts.Index}个部位提前break",
                                isProb = true
                            });
                            return;
                        }
                        GuildCalculator.Instance.dmglist.Add(new ProbEvent
                        {
                            unit = _source.UnitNameEx,
                            predict = hash => point.Emulate(hash) > 0f,
                            exp = hash => $"{point.ToExpression(hash)} > 0",
                            description = $"({BattleHeaderController.CurrentFrameCount}){_source.UnitNameEx}的第{parts.Index}个部位未break",
                            isProb = true
                        });
                        _source.AppendBreakLog(parts.BreakSource);
                        parts.IsBreak = true;
                        parts.OnBreak.Call();
                        parts.SetBreak(true,
                            //this.battleManager.UnitUiCtrl.transform
                            battleManager.transform);
                        _source.AppendCoroutine(_source.UpdateBreak(Value[eValueNumber.VALUE_3], parts), ePauseType.SYSTEM);
                    };
                    break;
                case 10:
                    target.OnSlipDamage += () =>
                    {
                        if (_source.TargetEnemyList.Count == 0 || judgeSilenceOrToad(_source) || _source.CurrentState == UnitCtrl.ActionState.SKILL && _source.CurrentSkillId == _skill.SkillId || BattleManager.Random(0.0f, 1f,
                                new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)Value[eValueNumber.VALUE_1] / 100.0)
                            return;
                        if ((double)Value[eValueNumber.VALUE_4] != 1.0 && _source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum))
                            _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                        else
                            _sourceActionController.StartAction(_skill.SkillId);
                    };
                    break;
                case 11:
                    target.OnBreakAll += () =>
                    {
                        if (judgeSilenceOrToad(_source) || BattleManager.Random(0.0f, 1f,
                                new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)Value[eValueNumber.VALUE_1] / 100.0)
                            return;
                        if ((double)Value[eValueNumber.VALUE_4] != 1.0 && _source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum))
                            _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                        else
                            _sourceActionController.StartAction(_skill.SkillId);
                    };
                    break;
                case 12:
                    {
                        UnitCtrl unitCtrl8 = target;
                        unitCtrl8.OnDamage = (UnitCtrl.OnDamageDelegate)Delegate.Combine(unitCtrl8.OnDamage, (UnitCtrl.OnDamageDelegate)delegate
                        {
                            if (!_source.IsDead && !_source.IdleOnly && !judgeSilenceOrToad(_source) && BattleManager.Random(0f, 1f,
                            new RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)Value[eValueNumber.VALUE_1] / 100.0)

                            {
                            if (base.Value[eValueNumber.VALUE_4] != 1f)
                                {
                                    _source.CancelByAwake = true;
                                    _source.CurrentTriggerSkillId = _skill.SkillId;
                                    _source.SetState(UnitCtrl.ActionState.SKILL, 0, _skill.SkillId);
                                }
                                else
                                {
                                    _sourceActionController.StartAction(_skill.SkillId);
                                }
                            }
                        });
                        break;

                    }
                case 13:
                    {
                        long num = (long)_source.Hp - (long)base.Value[eValueNumber.VALUE_3];
                        /*List<PrefabWithTime> list2 = new List<PrefabWithTime>();
                        foreach (NormalSkillEffect actionEffect in base.ActionEffectList)
                        {
                            PrefabWithTime item = new PrefabWithTime
                            {
                                Prefab = actionEffect.Prefab,
                                PrefabLeft = actionEffect.PrefabLeft,
                                Time = actionEffect.ExecTime[0],
                                TargetBoneName = actionEffect.TargetBoneName,
                                IsTrackRotation = actionEffect.TrackRotation
                            };
                            list2.Add(item);
                        }
                        if (list2.Count > 0)
                        {
                            _source.AppendCoroutine(_source.CreatePrefabWithTime(list2, _isIdle: false, -1, _isAura: true, _isShieldEffect: false, _ignorePause: false, base.ActionId), ePauseType.NO_DIALOG);
                        }*/
                        if (num > 0)
                        {
                            _source.DamagedTriggerHpAndActionDoneList.Add((num, false, base.ActionId, _skill));
                            _source.DamagedTriggerHpAndActionDoneList.Sort(((long triggerHp, bool isActionDone, int actionId, Skill skill) a, (long triggerHp, bool isActionDone, int actionId, Skill skill) b) => b.triggerHp.CompareTo(a.triggerHp));
                        }
                        if (_source.DamagedTriggerHpAndActionDoneList.Count != 1)
                        {
                            break;
                        }
                        bool actionDoing = false;
                        target.OnHpChangeForDamagedHP = delegate
                        {
                            float num5 = BattleManager.Random(0f, 1f,new RandomData(_source,target,ActionId,17, base.Value[eValueNumber.VALUE_1] / 100f, 0));
                            if (!judgeSilenceOrToad(_source) && !_source.IsUnableActionState() && !_source.IsDead && !(num5 > base.Value[eValueNumber.VALUE_1] / 100f))
                            {
                                int iForAction = default(int);
                                long triggerHp = default(long);
                                int actionId = default(int);
                                Skill skill = default(Skill);
                                for (int i = 0; i < _source.DamagedTriggerHpAndActionDoneList.Count; i++)
                                {
                                    iForAction = i;
                                    bool flag2;
                                    (triggerHp, flag2, actionId, skill) = _source.DamagedTriggerHpAndActionDoneList[i];
                                    if (!flag2)
                                    {
                                        if ((long)_source.Hp <= triggerHp && !actionDoing)
                                        {
                                            actionDoing = true;
                                            _source.DamagedTriggerHpAndActionDoneList[iForAction] = (triggerHp, true, actionId, skill);
                                            Action<bool> action2 = delegate (bool _waitUbTurn)
                                            {
                                                actionDoing = false;
                                                if (judgeSilenceOrToad(_source) || _source.IsUnableActionState())
                                                {
                                                    _source.DamagedTriggerHpAndActionDoneList[iForAction] = (triggerHp, false, actionId, skill);
                                                }
                                                else
                                                {
                                                    //_source.DeleteDamagedHpAuraEffect(actionId);
                                                    if (skill.BlackOutTime > 0f && _waitUbTurn)
                                                    {
                                                        base.battleManager.OCAMDIOPEFP = true;
                                                    }
                                                    _source.SkillUseCount[skill.SkillId]++;
                                                    _source.CancelByAwake = true;
                                                    _source.CurrentTriggerSkillId = skill.SkillId;
                                                    if (skill.BlackOutTime > 0f)
                                                    {
                                                        // _sourceActionController.StopSlowEffect();
                                                        //base.battleManager.SetSkillEffectSortOrderBack();
                                                        base.battleManager.GamePause(true, false);
                                                        base.battleManager.SetSkillExeScreen(_source, skill.BlackOutTime, skill.BlackoutColor, skill.BlackoutEndWithMotion);
                                                        _source.SetSortOrderFront();
                                                    }
                                                    _source.SetState(UnitCtrl.ActionState.SKILL, 0, skill.SkillId);
                                                }
                                            };
                                            if (_source.CurrentState == UnitCtrl.ActionState.SKILL_1)
                                            {
                                                _source.AppendCoroutine(waitStateIdle(action2, _source), ePauseType.SYSTEM, _source);
                                            }
                                            else if (base.battleManager.ChargeSkillTurn == eChargeSkillTurn.NONE)
                                            {
                                                action2.Call(JEOCPILJNAD: false);
                                            }
                                            else
                                            {
                                                _source.AppendCoroutine(waitChargeSkillTurnNone(action2, _source), ePauseType.SYSTEM, _source);
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        };
                        break;
                    }

            }
        }

        private bool judgeSilenceOrToad(UnitCtrl _unitCtrl) => _unitCtrl.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE) || _unitCtrl.ToadDatas.Count > 0;

        private IEnumerator waitStateIdle(Action<bool> _callback, UnitCtrl _source)
        {
            while (_source.CurrentState != UnitCtrl.ActionState.IDLE)
                yield return null;
            _callback.Call(false);
        }

        private IEnumerator waitChargeSkillTurnNone(Action<bool> _callback, UnitCtrl _source)
        {
            TriggerAction triggerAction = this;
            do
            {
                yield return null;
            }
            while (triggerAction.battleManager.ChargeSkillTurn != eChargeSkillTurn.NONE);
            if ((double)triggerAction.Value[eValueNumber.VALUE_3] == 0.0 || (long)_source.Hp != 0L)
                _callback.Call(true);
        }

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            eTriggerType actionDetail = (eTriggerType)base.ActionDetail1;
            if (actionDetail != eTriggerType.STEALTH_FREE)
            {
                return;
            }
            _source.IdleOnly = true;
            _source.CurrentState = UnitCtrl.ActionState.IDLE;
            battleManager.CallbackIdleOnlyDone(_source);
            List<UnitCtrl> unitCtrlList = _source.IsOther ? battleManager.EnemyList : battleManager.UnitList;
            if (unitCtrlList.Contains(_source))
                unitCtrlList.Remove(_source);
            BattleManager.Instance.QueueUpdateSkillTarget();
            _source.IsDead = true;
            _source.CureAllAbnormalState();
            /*foreach (SkillEffectCtrl repeatEffect in _source.RepeatEffectList)
            {
              if (!(repeatEffect is ChangeColorEffect))
                repeatEffect.SetTimeToDie(true);
            }*/
            battleManager.CallbackFadeOutDone(_source);
            battleManager.CallbackDead(_source);
            _source.gameObject.SetActive(false);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
        }

        private enum eTriggerType
        {
            DODGE = 1,
            DAMAGED = 2,
            HP = 3,
            DEAD = 4,
            CRITICAL_DAMAGED = 5,
            CRITICAL_DAMAGED_WITH_SUMMON = 6,
            LIMIT_TIME = 7,
            STEALTH_FREE = 8,
            BREAK = 9,
            SLIP_DAMAGE = 10, // 0x0000000A
            ALL_BREAK = 11, // 0x0000000B
            DAMAGED_2,
            DAMAGED_HP
        }
    }
}
