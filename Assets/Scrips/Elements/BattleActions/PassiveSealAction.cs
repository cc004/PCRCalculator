using System.Collections.Generic;

namespace Elements
{
  public class PassiveSealAction : ActionParameter
  {
    private const int DISPLAY_COUNT_VALUE = 1;

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
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      PassiveSealData _data = new PassiveSealData
      {
        DisplayCount = ActionDetail2 == 1,
        SealTarget = (eSealTarget) ActionDetail3,
        Source = _source,
        Target = _target.Owner,
        TargetStateIcon = (eStateIconType)(float)_valueDictionary[eValueNumber.VALUE_2],
        SealDuration = _valueDictionary[eValueNumber.VALUE_3],
        LifeTime = _valueDictionary[eValueNumber.VALUE_5],
        SealNumLimit = (int) _valueDictionary[eValueNumber.VALUE_1],
                        SealNum = (int)_valueDictionary[eValueNumber.VALUE_7]

      };
      AppendIsAlreadyExeced(_target.Owner, _num);
      _source.AppendCoroutine(_data.Update(), ePauseType.SYSTEM);
      _target.Owner.AddPassiveSeal((ePassiveTiming) ActionDetail1, _data);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
      Value[eValueNumber.VALUE_5] = (float) (MasterData.action_value_5 + MasterData.action_value_6 * _level);
    }

    public enum ePassiveTiming
    {
      BUFF = 1,
            DAMAGED

        }

        public enum eSealTarget
    {
      SOURCE,
    }
  }
}
