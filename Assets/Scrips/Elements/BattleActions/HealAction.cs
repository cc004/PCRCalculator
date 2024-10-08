﻿// Decompiled with JetBrains decompiler
// Type: Elements.HealAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Elements
{
  public class HealAction : ActionParameter
  {
    private const float PERCENT_DIGIT = 100f;
    private const float HP_RECOVER_RATE_BASE = 100f;
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
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
      Action<string> action = null)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      int num1 = 0;
      bool flag = ActionDetail1 == 1;
      switch ((eValueType)(float)_valueDictionary[eValueNumber.VALUE_1])
      {
        case eValueType.FIXED:
          int num2 = _source.IsPartsBoss ? (flag ? parts.GetAtkZero() : parts.GetMagicStrZero()) : (int) (flag ? _source.AtkZero : _source.MagicStrZero);
          num1 = (int) _valueDictionary[eValueNumber.VALUE_2] + (int) (num2 * (double) _valueDictionary[eValueNumber.VALUE_4]);
          break;
        case eValueType.PERCENTAGE:
          num1 = (int) (_target.Owner.MaxHp * (double) _valueDictionary[eValueNumber.VALUE_2] / 100.0);
          break;
      }
      int num3 = (int) ((int) (num1 * ((_source.IsPartsBoss ? parts.GetHpRecoverRateZero() : (double) (int) _source.HpRecoveryRateZero) + 100.0) / 100.0) / ExecTime.Length * (double) _skill.AweValue);
            //_target.Owner.SetRecovery(num3, flag ? UnitCtrl.eInhibitHealType.PHYSICS : UnitCtrl.eInhibitHealType.MAGIC, _source, this.EffectType == eEffectType.COMMON, _target: _target,action:action);
            _target.Owner.SetRecovery(num3, flag ? UnitCtrl.eInhibitHealType.PHYSICS : UnitCtrl.eInhibitHealType.MAGIC, _source, UnitCtrl.GetHealDownValue(_source), EffectType == eEffectType.COMMON, _target: _target, _releaseToad: true, action: action);

        }

        public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_2] = (float) (MasterData.action_value_2 + MasterData.action_value_3 * _level);
      Value[eValueNumber.VALUE_4] = (float) (MasterData.action_value_4 + MasterData.action_value_5 * _level);
    }

    private enum eValueType
    {
      FIXED = 1,
      PERCENTAGE = 2,
    }
  }
}
