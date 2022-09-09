using System.Collections.Generic;

namespace Elements
{
  public class StopFieldAction : ActionParameter
  {
    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _starttime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
      System.Action<string> action)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      battleManager.StopField(ActionDetail1, TargetAssignment, _source.IsOther);
            action($"移除{ActionDetail1}的领域");
    }

    public override void SetLevel(float _level) => base.SetLevel(_level);
  }
}
