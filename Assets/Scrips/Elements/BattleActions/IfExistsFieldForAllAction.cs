// Decompiled with JetBrains decompiler
// Type: Elements.IfExistsFieldForAllAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Elements
{
  public class IfExistsFieldForAllAction : ActionParameter
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
      Dictionary<eValueNumber, float> _valueDictinary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictinary);
      if (!this.battleManager.ExistsField(this.ActionDetail1, _source.IsOther))
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
