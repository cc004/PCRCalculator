// Decompiled with JetBrains decompiler
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
      ValueDictionary _valueDictionary,
      Action<string> action = null)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      long baseHeal = 0;
      bool flag = ActionDetail1 == 1;
      switch ((eValueType)(float)_valueDictionary[eValueNumber.VALUE_1])
      {
        case eValueType.FIXED:
          long attack = _source.IsPartsBoss ? (flag ? parts.GetAtkZero() : parts.GetMagicStrZero()) : (long)(flag ? _source.AtkZero : _source.MagicStrZero);
          baseHeal = BattleUtil.DoubleToLong((double)_valueDictionary[eValueNumber.VALUE_2])
              + BattleUtil.DoubleToLong(attack * (double)_valueDictionary[eValueNumber.VALUE_4]);
          break;
        case eValueType.PERCENTAGE:
          baseHeal = BattleUtil.DoubleToLong(_target.Owner.MaxHp * (double)_valueDictionary[eValueNumber.VALUE_2] / 100.0);
          break;
      }

      long healEnhance = 0;
      if (!IsExEquipSkillAction) 
      {
        healEnhance = _source.HealEnhanceZero;
      }
      long recoveryRate = _source.IsPartsBoss ? parts.GetHpRecoverRateZero() : _source.HpRecoveryRateZero;
      long totalHeal = BattleUtil.DoubleToLong((recoveryRate + 100.0) * baseHeal * (healEnhance + 10000.0) / 100.0 / 10000.0);
      long healPerHit = BattleUtil.DoubleToLong((totalHeal / ExecTime.Length) * _skill.AweValue);
      _target.Owner.SetRecovery(healPerHit, 0, flag ? UnitCtrl.eInhibitHealType.PHYSICS : UnitCtrl.eInhibitHealType.MAGIC, _source, UnitCtrl.GetHealDownValue(_source), EffectType == eEffectType.COMMON, _target: _target, _releaseToad: true, action: action);

    }

        public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_2] = MasterData.action_value_2 + MasterData.action_value_3 * _level;
      Value[eValueNumber.VALUE_4] = MasterData.action_value_4 + MasterData.action_value_5 * _level;
    }

    private enum eValueType
    {
      FIXED = 1,
      PERCENTAGE = 2,
    }
  }
}
