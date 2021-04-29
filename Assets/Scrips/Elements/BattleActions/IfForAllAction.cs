// Decompiled with JetBrains decompiler
// Type: Elements.IfForAllAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class IfForAllAction : ActionParameter
  {
    public const int IF_DIGIT = 100;
    public const int IF_DIGIT_COUNT = 10;

    public static bool JudgeCountableUnit(UnitCtrl _unit) => (long) _unit.Hp != 0L && !_unit.IsStealth;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      switch ((eIfType) this.ActionDetail1)
      {
        case eIfType.DEFEAT:
          ActionParameter actionParameter1 = _skill.ActionParameters[0];
          int index = 0;
          for (int count = _skill.ActionParameters.Count; index < count; ++index)
          {
            ActionParameter actionParameter2 = _skill.ActionParameters[index];
            actionParameter2.OnDefeatEnemy += (Action) (() =>
            {
              _skill.DefeatByThisSkill = true;
              _skill.EffectBranchId = (int) this.Value[eValueNumber.VALUE_1];
              if (this.ActionDetail3 == 0)
                return;
              _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail3)).CancelByIfForAll = true;
            });
            if (actionParameter2.ActionId != this.ActionDetail2 && actionParameter2.ActionId != this.ActionDetail3 && actionParameter2 != this)
              actionParameter1 = (double) actionParameter1.ExecTime[actionParameter1.ExecTime.Length - 1] > (double) actionParameter2.ExecTime[actionParameter2.ExecTime.Length - 1] ? actionParameter1 : actionParameter2;
          }
          actionParameter1.OnActionEnd += (ActionParameter.OnActionEndDelegate) (() => _source.AppendCoroutine(this.waitFrameEnd(_skill), ePauseType.SYSTEM, _source));
          actionParameter1.OnInitWhenNoTarget += (Action) (() =>
          {
            if (this.ActionDetail2 == 0)
              return;
            _skill.EffectBranchId = (int) this.Value[eValueNumber.VALUE_2];
            _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail2)).CancelByIfForAll = true;
          });
          break;
        case eIfType.CRITICAL:
          _skill.CriticalPartsList = new List<BasePartsData>();
          break;
      }
    }

    private IEnumerator waitFrameEnd(Skill _skill)
    {
      IfForAllAction ifForAllAction = this;
      yield return (object) null;
      if (!_skill.DefeatByThisSkill && ifForAllAction.ActionDetail2 != 0)
      {
        _skill.EffectBranchId = (int) ifForAllAction.Value[eValueNumber.VALUE_2];
        // ISSUE: reference to a compiler-generated method
        _skill.ActionParameters.Find(a=>a.ActionId == ActionDetail2).CancelByIfForAll = true;
      }
    }

    public override void ReadyAction(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      base.ReadyAction(_source, _sourceActionController, _skill);
      if (this.ActionDetail1 != 1001)
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
      Dictionary<eValueNumber, float> _valueDictinary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictinary);
      bool flag = false;
      if (this.ActionDetail1 == 1000)
        return;
      switch ((eIfType) this.ActionDetail1)
      {
        case eIfType.STOP:
          flag = _target.Owner.IsUnableActionState();
          break;
        case eIfType.BLIND:
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
        case eIfType.POISON_OR_VENOM:
          flag = _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.POISON) || _target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.VENOM);
          break;
        case eIfType.SLIP_DAMAGE:
          flag = _target.Owner.IsSlipDamageState();
          break;
        case eIfType.ALONE:
          List<UnitCtrl> unitCtrlList = _source.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList;
          flag = !unitCtrlList.Exists((Predicate<UnitCtrl>) (e => e.IsPartsBoss && !e.IsDead)) && unitCtrlList.FindAll((Predicate<UnitCtrl>) (e => (!e.IsDead && (long) e.Hp > 0L || e.HasUnDeadTime) && !e.IsStealth)).Count == 1;
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
            break;
          }
          break;
        case eIfType.UNIT_ID:
          flag = this.TargetList.FindAll((Predicate<BasePartsData>) (e => IfForAllAction.JudgeCountableUnit(e.Owner))).Exists((Predicate<BasePartsData>) (e => (double) e.Owner.SoundUnitId == (double) _valueDictinary[eValueNumber.VALUE_3]));
          break;
        case eIfType.CRITICAL:
          flag = _skill.CriticalPartsList.Count > 0;
          break;
        case eIfType.ATK_TYPE:
          flag = _target.Owner.AtkType == 1;
          break;
        default:
          if (this.ActionDetail1 < 100)
          {
            flag = (double) BattleManager.Random(0.0f, 100f, new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 12, ActionDetail1)) < (double) this.ActionDetail1;
            break;
          }
          if (this.ActionDetail1 > 1200)
          {
            int num = 0;
            _target.Owner.SkillExecCountDictionary.TryGetValue(this.ActionDetail1 % 100 / 10, out num);
            flag = num == this.ActionDetail1 % 10;
            break;
          }
          if (this.ActionDetail1 > 900)
          {
            flag = (double) (long) _target.Owner.Hp / (double) (long) _target.Owner.MaxHp < (double) (this.ActionDetail1 % 100) / 100.0;
            break;
          }
          if (this.ActionDetail1 > 700)
          {
            flag = this.TargetList.FindAll((Predicate<BasePartsData>) (e => IfForAllAction.JudgeCountableUnit(e.Owner))).Count == this.ActionDetail1 - 700;
            break;
          }
          if (this.ActionDetail1 > 600)
          {
            eStateIconType key = (eStateIconType) (this.ActionDetail1 - 600);
            flag = _target.Owner.SealDictionary.ContainsKey(key) && ((double) _valueDictinary[eValueNumber.VALUE_3] != 0.0 ? (double) _target.Owner.SealDictionary[key].GetCurrentCount() >= (double) _valueDictinary[eValueNumber.VALUE_3] : _target.Owner.SealDictionary[key].GetCurrentCount() > 0);
            break;
          }
          break;
      }
      if (!flag)
      {
        _skill.EffectBranchId = (int) this.Value[eValueNumber.VALUE_2];
        if (this.ActionDetail2 != 0)
          this.cancelAction(_skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail2)), _skill);
      }
      else
      {
        _skill.EffectBranchId = (int) this.Value[eValueNumber.VALUE_1];
        if (this.ActionDetail3 != 0)
          this.cancelAction(_skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail3)), _skill);
      }
      _sourceActionController.AppendCoroutine(_sourceActionController.UpdateBranchMotion((ActionParameter) this, _skill), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
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
      if (this.ActionDetail2 != 0)
        this.cancelAction(_skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail2)), _skill);
      if (this.ActionDetail3 == 0)
        return;
      this.cancelAction(_skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail3)), _skill);
    }
  }
}
