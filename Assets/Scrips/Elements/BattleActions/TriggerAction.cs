// Decompiled with JetBrains decompiler
// Type: Elements.TriggerAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Elements.Battle;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
    public class TriggerAction : ActionParameter
    {
        private const int TOUGH_VALUE = 99;
        private const int NO_MOTION_VALUE = 1;

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            PartsData parts = _source.BossPartsListForBattle.Find((Predicate<PartsData>)(e => e.Index == _skill.ParameterTarget));
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            UnitCtrl target = _source;
            if (this.ActionDetail2 != 0)
            {
                target = (!_source.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList).Find((Predicate<UnitCtrl>)(e => e.UnitId == this.ActionDetail2));
                if ((UnityEngine.Object)target == (UnityEngine.Object)null)
                    return;
            }
            bool actionDone = false;
            int actionCount = 0;
            switch (this.ActionDetail1)
            {
                case 1:
                    target.OnDodge += (Action)(() =>
                   {
                       float num = BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f));
                       if (_source.TargetEnemyList.Count == 0 || this.judgeSilenceOrToad(_source) || _source.CurrentState == UnitCtrl.ActionState.SKILL && _source.CurrentSkillId == _skill.SkillId || (double)num > (double)this.Value[eValueNumber.VALUE_1] / 100.0)
                           return;
                       if (_source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum))
                           _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                       else
                           _sourceActionController.StartAction(_skill.SkillId);
                   });
                    break;
                case 2:
                    target.OnDamage += (UnitCtrl.OnDamageDelegate)((byAtack, damage, critical) =>
                   {
                       if (!byAtack)
                           return;
                       float num = BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source,null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f));
                       if (_source.TargetEnemyList.Count == 0 || this.judgeSilenceOrToad(_source) || _source.CurrentState == UnitCtrl.ActionState.SKILL && _source.CurrentSkillId == _skill.SkillId || (double)num > (double)this.Value[eValueNumber.VALUE_1] / 100.0)
                           return;
                       if (_source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum))
                           _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                       else
                           _sourceActionController.StartAction(_skill.SkillId);
                   });
                    break;
                case 3:
                    if ((double)this.Value[eValueNumber.VALUE_4] == 99.0)
                        target.IsTough = true;
                    if ((double)this.Value[eValueNumber.VALUE_3] == 0.0)
                        target.HasUnDeadTime = true;
                    target.OnHpChange = (UnitCtrl.OnDamageDelegate)((byAttack, damage, critical) =>
                   {
                       float num = BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f));
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
                       if (this.judgeSilenceOrToad(_source) || _source.IsUnableActionState() || (_source.IsDead || (double)num > (double)this.Value[eValueNumber.VALUE_1] / 100.0) || (actionDone || (double)(long)_source.Hp > (double)this.Value[eValueNumber.VALUE_3] / 100.0 * (double)(long)_source.MaxHp))
                           return;
                       actionDone = true;
                       Action<bool> action = (Action<bool>)(_waitUbTurn =>
             {
                         if (this.judgeSilenceOrToad(_source) || _source.IsUnableActionState())
                         {
                             actionDone = false;
                         }
                         else
                         {
                             if ((double)_skill.BlackOutTime > 0.0 & _waitUbTurn)
                                 this.battleManager.OCAMDIOPEFP = true;
                             _source.SkillUseCount[_skill.SkillId]++;
                             _source.CancelByAwake = true;
                             _source.CurrentTriggerSkillId = _skill.SkillId;
                             if ((double)_skill.BlackOutTime > 0.0)
                             {
                                 this.battleManager.GamePause(true, false);
                                 this.battleManager.SetSkillExeScreen(_source, _skill.BlackOutTime, _skill.BlackoutColor, _skill.BlackoutEndWithMotion);
                                 _source.SetSortOrderFront();
                             }
                             _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                         }
                     });
                       if (_source.CurrentState == UnitCtrl.ActionState.SKILL_1)
                           _source.AppendCoroutine(this.waitStateIdle(action, _source), ePauseType.SYSTEM, _source);
                       else if (this.battleManager.ChargeSkillTurn == eChargeSkillTurn.NONE)
                           action.Call<bool>(false);
                       else
                           _source.AppendCoroutine(this.waitChargeSkillTurnNone(action, _source), ePauseType.SYSTEM, _source);
                   });
                    break;
                case 4:
                    if (this.ActionDetail3 != 0 && target.SkillUseCount[_skill.SkillId] >= this.ActionDetail3)
                        break;
                    target.OnDeadForRevival += (UnitCtrl.OnDeadDelegate)(owner =>
                   {
              //if (target.EnemyPoint != 0 && this.battleManager.FICLPNJNOEP >= (int) HatsuneUtility.GetHatsuneSpecialBattle().purpose_count || this.judgeSilenceOrToad(_source))
              // return;
              _source.SkillUseCount[_skill.SkillId]++;
                       if ((double)BattleManager.Random(0.0f, 1f,
                           new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) <= (double)this.Value[eValueNumber.VALUE_1] / 100.0)
                       {
                           if (this.ActionDetail3 != 0 && actionCount >= this.ActionDetail3)
                               return;
                           int skillId = _skill.SkillId;
                           ++actionCount;
                           if (this.ActionDetail3 != 0 && actionCount == this.ActionDetail3)
                               target.OnDeadForRevival = (UnitCtrl.OnDeadDelegate)null;
                           if (skillId == owner.UnionBurstSkillId)
                               _sourceActionController.AppendCoroutine(owner.SetStateWithDelayForRevival(this.ExecTime[0], UnitCtrl.ActionState.SKILL_1, skillId: skillId), ePauseType.SYSTEM, owner);
                           else
                               _sourceActionController.AppendCoroutine(owner.SetStateWithDelayForRevival(this.ExecTime[0], UnitCtrl.ActionState.SKILL, skillId: skillId), ePauseType.SYSTEM, (double)_skill.BlackOutTime > 0.0 ? owner : (UnitCtrl)null);
                       }
                       else
                       {
                           _source.IsDead = true;
                           this.battleManager.CallbackDead(_source);
                           _source.gameObject.SetActive(false);
                           this.battleManager.CallbackFadeOutDone(_source);
                       }
                   });
                    break;
                case 5:
                    target.OnDamage += (UnitCtrl.OnDamageDelegate)((byAtack, damage, critical) =>
                   {
                       if (_source.IsDead || _source.IdleOnly || (this.judgeSilenceOrToad(_source) || !critical) || (double)BattleManager.Random(0.0f, 1f,
                           new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)this.Value[eValueNumber.VALUE_1] / 100.0)
                           return;
                       if ((double)this.Value[eValueNumber.VALUE_4] != 1.0)
                       {
                           _source.CancelByAwake = true;
                           _source.CurrentTriggerSkillId = _skill.SkillId;
                           _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                       }
                       else
                           _sourceActionController.StartAction(_skill.SkillId);
                   });
                    break;
                case 6:
                    target.OnDamage += (UnitCtrl.OnDamageDelegate)((byAtack, damage, critical) =>
                   {
                       bool flag = false;
                       foreach (KeyValuePair<int, UnitCtrl> summonUnit in target.SummonUnitDictionary)
                       {
                           if (!summonUnit.Value.IsDead)
                               flag = true;
                       }
                       if (!flag || _source.IsDead || (_source.IdleOnly || this.judgeSilenceOrToad(_source)) || (!critical || (double)BattleManager.Random(0.0f, 1f,
                           new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)this.Value[eValueNumber.VALUE_1] / 100.0))
                           return;
                       _source.CancelByAwake = true;
                       _source.CurrentTriggerSkillId = _skill.SkillId;
                       _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                   });
                    break;
                case 7:
                    bool used1 = false;
                    target.OnUpdateWhenIdle += (Action<float>)(_limitTime =>
                   {
                       if (this.judgeSilenceOrToad(_source) || used1 || (double)_limitTime > (double)this.Value[eValueNumber.VALUE_3])
                           return;
                       used1 = true;
                       if ((double)BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)this.Value[eValueNumber.VALUE_1] / 100.0)
                           return;
                       _source.CancelByAwake = true;
                       _source.CurrentTriggerSkillId = _skill.SkillId;
                       _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                   });
                    break;
                case 8:
                    bool used2 = false;
                    _source.IsTough = (double)this.Value[eValueNumber.VALUE_3] == 0.0;
                    _source.OnUpdateWhenIdle += (Action<float>)(_limitTime =>
                   {
                       if (this.judgeSilenceOrToad(_source) || used2 || (!_source.IsStealth || _source.ActionsTargetOnMe.Count != 0))
                           return;
                       used2 = true;
                       if ((double)BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)this.Value[eValueNumber.VALUE_1] / 100.0)
                           return;
                       _source.CancelByAwake = true;
                       _source.CurrentTriggerSkillId = _skill.SkillId;
                       _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                   });
                    break;
                case 9:
                    parts.BreakEffectList = _skill.SkillEffects;
                    _source.OnUpdateWhenIdle += (Action<float>)(_limitTime =>
                   {
                       if ((int)parts.BreakPoint != 0 || parts.IsBreak || (this.judgeSilenceOrToad(_source) || this.battleManager.ChargeSkillTurn != eChargeSkillTurn.NONE) || (double)BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)this.Value[eValueNumber.VALUE_1] / 100.0)
                           return;
                       _source.AppendBreakLog(parts.BreakSource);
                       parts.IsBreak = true;
                       parts.OnBreak.Call();
                       parts.SetBreak(true,
                  //this.battleManager.UnitUiCtrl.transform
                  battleManager.transform);
                       _source.AppendCoroutine(_source.UpdateBreak(this.Value[eValueNumber.VALUE_3], parts), ePauseType.SYSTEM);
                   });
                    break;
                case 10:
                    target.OnSlipDamage += (Action)(() =>
                   {
                       if (_source.TargetEnemyList.Count == 0 || this.judgeSilenceOrToad(_source) || _source.CurrentState == UnitCtrl.ActionState.SKILL && _source.CurrentSkillId == _skill.SkillId || (double)BattleManager.Random(0.0f, 1f,
                           new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)this.Value[eValueNumber.VALUE_1] / 100.0)
                           return;
                       if ((double)this.Value[eValueNumber.VALUE_4] != 1.0 && _source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum))
                           _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                       else
                           _sourceActionController.StartAction(_skill.SkillId);
                   });
                    break;
                case 11:
                    target.OnBreakAll += (Action)(() =>
                   {
                       if (this.judgeSilenceOrToad(_source) || (double)BattleManager.Random(0.0f, 1f,
                           new PCRCaculator.Guild.RandomData(_source, null, ActionId, 17, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)this.Value[eValueNumber.VALUE_1] / 100.0)
                           return;
                       if ((double)this.Value[eValueNumber.VALUE_4] != 1.0 && _source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum))
                           _source.SetState(UnitCtrl.ActionState.SKILL, _skillId: _skill.SkillId);
                       else
                           _sourceActionController.StartAction(_skill.SkillId);
                   });
                    break;
            }
        }

        private bool judgeSilenceOrToad(UnitCtrl _unitCtrl) => _unitCtrl.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE) || _unitCtrl.ToadDatas.Count > 0;

        private IEnumerator waitStateIdle(Action<bool> _callback, UnitCtrl _source)
        {
            while (_source.CurrentState != UnitCtrl.ActionState.IDLE)
                yield return (object)null;
            _callback.Call<bool>(false);
        }

        private IEnumerator waitChargeSkillTurnNone(Action<bool> _callback, UnitCtrl _source)
        {
            TriggerAction triggerAction = this;
            do
            {
                yield return (object)null;
            }
            while (triggerAction.battleManager.ChargeSkillTurn != eChargeSkillTurn.NONE);
            if ((double)triggerAction.Value[eValueNumber.VALUE_3] == 0.0 || (long)_source.Hp != 0L)
                _callback.Call<bool>(true);
        }

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, float> _valueDictionary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            if (this.ActionDetail1 != 8)
                return;
            _source.IdleOnly = true;
            _source.CurrentState = UnitCtrl.ActionState.IDLE;
            this.battleManager.CallbackIdleOnlyDone(_source);
            List<UnitCtrl> unitCtrlList = _source.IsOther ? this.battleManager.EnemyList : this.battleManager.UnitList;
            if (unitCtrlList.Contains(_source))
                unitCtrlList.Remove(_source);
            _source.IsDead = true;
            _source.CureAllAbnormalState();
            /*foreach (SkillEffectCtrl repeatEffect in _source.RepeatEffectList)
            {
              if (!(repeatEffect is ChangeColorEffect))
                repeatEffect.SetTimeToDie(true);
            }*/
            this.battleManager.CallbackFadeOutDone(_source);
            this.battleManager.CallbackDead(_source);
            _source.gameObject.SetActive(false);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
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
        }
    }
}
