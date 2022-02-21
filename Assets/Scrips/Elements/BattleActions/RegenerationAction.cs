// Decompiled with JetBrains decompiler
// Type: Elements.RegenerationAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class RegenerationAction : ActionParameter
  {
    private BasePartsData parts;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      parts = _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget);
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
      AppendIsAlreadyExeced(_target.Owner, _num);
      int num = ActionDetail1 != 1 ? (_source.IsPartsBoss ? parts.GetMagicStrZero() : (int) _source.MagicStrZero) : (_source.IsPartsBoss ? parts.GetAtkZero() : (int) _source.AtkZero);
      switch ((eTargetType) ActionDetail2)
      {
        case eTargetType.HP:
          _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.REGENERATION, _valueDictionary[eValueNumber.VALUE_5], this, _skill, (int) ((double) _valueDictionary[eValueNumber.VALUE_1] + (double) _valueDictionary[eValueNumber.VALUE_3] * num), ActionDetail1);
          break;
        case eTargetType.TP:
          _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.TP_REGENERATION, _valueDictionary[eValueNumber.VALUE_5], this, _skill, (int) _valueDictionary[eValueNumber.VALUE_1], ActionDetail1);
          break;
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_1] = (float) (MasterData.action_value_1 + MasterData.action_value_2 * _level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
      Value[eValueNumber.VALUE_5] = (float) (MasterData.action_value_5 + MasterData.action_value_6 * _level);
    }

    public enum eParameterType
    {
      PHYSICS = 1,
      MAGIC = 2,
    }

    private enum eTargetType
    {
      HP = 1,
      TP = 2,
    }
  }
}
