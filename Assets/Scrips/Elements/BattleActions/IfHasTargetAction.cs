// Decompiled with JetBrains decompiler
// Type: Elements.IfHasTargetAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Elements
{
  public class IfHasTargetAction : ActionParameter
  {
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
      if (_skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail2)).TargetList.Count == 0)
      {
        this.cancelAction(_skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail1)), _skill);
        _skill.EffectBranchId = (int) this.Value[eValueNumber.VALUE_2];
      }
      else
        _skill.EffectBranchId = (int) this.Value[eValueNumber.VALUE_1];
    }

    private void cancelAction(ActionParameter _action, Skill _skill)
    {
      _action.CancelByIfForAll = true;
      if (!(_action is IfForAllAction))
        return;
      (_action as IfForAllAction).CancelBoth(_skill);
    }
  }
}
