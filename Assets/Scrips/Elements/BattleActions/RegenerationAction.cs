// Decompiled with JetBrains decompiler
// Type: Elements.RegenerationAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
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
      this.parts = (BasePartsData) _source.BossPartsListForBattle.Find((Predicate<PartsData>) (e => e.Index == _skill.ParameterTarget));
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
      this.AppendIsAlreadyExeced(_target.Owner, _num);
      int num = this.ActionDetail1 != 1 ? (_source.IsPartsBoss ? this.parts.GetMagicStrZero() : (int) _source.MagicStrZero) : (_source.IsPartsBoss ? this.parts.GetAtkZero() : (int) _source.AtkZero);
      switch ((RegenerationAction.eTargetType) this.ActionDetail2)
      {
        case RegenerationAction.eTargetType.HP:
          _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.REGENERATION, _valueDictionary[eValueNumber.VALUE_5], (ActionParameter) this, _skill, (float) (int) ((double) _valueDictionary[eValueNumber.VALUE_1] + (double) _valueDictionary[eValueNumber.VALUE_3] * (double) num), (float) this.ActionDetail1);
          break;
        case RegenerationAction.eTargetType.TP:
          _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.TP_REGENERATION, _valueDictionary[eValueNumber.VALUE_5], (ActionParameter) this, _skill, (float) (int) _valueDictionary[eValueNumber.VALUE_1], (float) this.ActionDetail1);
          break;
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
      this.Value[eValueNumber.VALUE_5] = (float) ((double) this.MasterData.action_value_5 + (double) this.MasterData.action_value_6 * (double) _level);
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
