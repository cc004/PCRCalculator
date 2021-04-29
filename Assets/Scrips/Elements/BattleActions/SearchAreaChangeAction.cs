// Decompiled with JetBrains decompiler
// Type: Elements.SearchAreaChangeAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class SearchAreaChangeAction : ActionParameter
  {
    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      if (_sourceActionController.Skill1IsChargeTime)
        return;
      _sourceActionController.Skill1IsChargeTime = _skill.SkillId == _source.UnionBurstSkillId && (this.ActionType == eActionType.SEARCH_AREA_CHANGE && this.ActionDetail2 == 2);
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
      _target.Owner.ChangeSearchArea(_valueDictionary[eValueNumber.VALUE_1], _valueDictionary[eValueNumber.VALUE_2], this.ActionDetail2 == 2, _valueDictionary[eValueNumber.VALUE_4]);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_2] = (float) ((double) this.MasterData.action_value_2 + (double) this.MasterData.action_value_3 * (double) _level);
      this.Value[eValueNumber.VALUE_4] = (float) ((double) this.MasterData.action_value_4 + (double) this.MasterData.action_value_5 * (double) _level);
    }

    private enum eSearchAreaChangeType
    {
      TIME = 1,
      ENERGY = 2,
    }
  }
}
