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
      _sourceActionController.Skill1IsChargeTime = _skill.SkillId == _source.UnionBurstSkillId && (ActionType == eActionType.SEARCH_AREA_CHANGE && ActionDetail2 == 2);
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
      _target.Owner.ChangeSearchArea(_valueDictionary[eValueNumber.VALUE_1], _valueDictionary[eValueNumber.VALUE_2], ActionDetail2 == 2, _valueDictionary[eValueNumber.VALUE_4]);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_2] = (float) (MasterData.action_value_2 + MasterData.action_value_3 * _level);
      Value[eValueNumber.VALUE_4] = (float) (MasterData.action_value_4 + MasterData.action_value_5 * _level);
    }

    private enum eSearchAreaChangeType
    {
      TIME = 1,
      ENERGY = 2,
    }
  }
}
