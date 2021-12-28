// Decompiled with JetBrains decompiler
// Type: Elements.DestroyAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Elements
{
  public class DestroyAction : ActionParameter
  {
    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController) => base.ExecActionOnStart(_skill, _source, _sourceActionController);

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
      this.AppendIsAlreadyExeced(_target.Owner, _num);
      switch ((DestroyAction.eDestroyType) this.ActionDetail1)
      {
        case DestroyAction.eDestroyType.NORMAL:
          UnitCtrl owner = _target.Owner;
          DamageData _damageData = new DamageData();
          _damageData.Damage = (long) _target.Owner.Hp;
          _damageData.DamageType = DamageData.eDamageType.NONE;
          _damageData.Source = _source;
          _damageData.ActionType = eActionType.DESTROY;
          int actionId = this.ActionId;
          Action onDefeatEnemy = this.OnDefeatEnemy;
          double energyChargeMultiple = (double) this.EnergyChargeMultiple;
          owner.SetDamage(_damageData, false, actionId, _hasEffect: false, _onDefeat: onDefeatEnemy, _energyChargeMultiple: ((float) energyChargeMultiple));
          break;
        case DestroyAction.eDestroyType.DELETE:
          _target.Owner.IdleOnly = true;
          _target.Owner.CurrentState = UnitCtrl.ActionState.IDLE;
          this.battleManager.CallbackIdleOnlyDone(_target.Owner);
          List<UnitCtrl> unitCtrlList = _target.Owner.IsOther ? this.battleManager.EnemyList : this.battleManager.UnitList;
          if (unitCtrlList.Contains(_target.Owner))
            unitCtrlList.Remove(_target.Owner);
          _target.Owner.IsDead = true;
          _target.Owner.CureAllAbnormalState();
          this.battleManager.CallbackFadeOutDone(_target.Owner);
          this.battleManager.CallbackDead(_target.Owner);
          _target.Owner.gameObject.SetActive(false);
          _target.Owner.SetCurrentHpZero();
                    /*using (List<SkillEffectCtrl>.Enumerator enumerator = _target.Owner.RepeatEffectList.GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                        enumerator.Current.SetTimeToDie(true);
                      break;
                    }*/
                    break;
      }
    }

    private enum eDestroyType
    {
      NORMAL,
      DELETE,
    }
  }
}
