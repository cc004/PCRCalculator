// Decompiled with JetBrains decompiler
// Type: Elements.NoDamageAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class NoDamageAction : ActionParameter
  {
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
      AppendIsAlreadyExeced(_target.Owner, _num);
      switch (ActionDetail1)
      {
        case 1:
          _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
          break;
        case 2:
          _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.PHYSICS_DODGE, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
          break;
        case 4:
          _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.NO_ABNORMAL, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
          break;
        case 5:
          _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.NO_DEBUF, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
          break;
        case 6:
          _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.PARTS_NO_DAMAGE, _valueDictionary[eValueNumber.VALUE_1], this, _skill);
          break;
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_1] = (float) (MasterData.action_value_1 + MasterData.action_value_2 * _level);
    }

    private enum eNoDamageType
    {
      NO_DAMAGE = 1,
      PHYSICS_DODGE = 2,
      ALL_DODGE = 3,
      ABNORMAL = 4,
      DEBUFF = 5,
      BREAK_POINT = 6,
    }
  }
}
