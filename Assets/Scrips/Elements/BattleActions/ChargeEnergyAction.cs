﻿// Decompiled with JetBrains decompiler
// Type: Elements.ChargeEnergyAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Elements
{
  public class ChargeEnergyAction : ActionParameter
  {
    private const float PERCENT_DIGIT = 100f;

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
      Dictionary<eValueNumber, float> _valueDictionary,
      System.Action<string> action)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      this.AppendIsAlreadyExeced(_target.Owner, _num);
      float _energy = 0.0f;
      switch (this.ActionDetail1)
      {
        case 1:
          _energy = _valueDictionary[eValueNumber.VALUE_1];
          break;
        case 2:
          _energy = -_valueDictionary[eValueNumber.VALUE_1];
          break;
        case 3:
          _energy = (float) ((double) _target.Owner.Energy * (double) _valueDictionary[eValueNumber.VALUE_1] / 100.0) - _target.Owner.Energy;
          break;
      }
      _target.Owner.ChargeEnergy(eSetEnergyType.BY_CHANGE_ENERGY, _energy, this.ActionDetail1 == 1, _source, _effectType: this.EffectType,action:action);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + Math.Ceiling((double) this.MasterData.action_value_2 * (double) _level));
    }
  }
}