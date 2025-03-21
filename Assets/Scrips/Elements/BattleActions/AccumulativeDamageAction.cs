﻿// Decompiled with JetBrains decompiler
// Type: Elements.AccumulativeDamageAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Elements
{
  public class AccumulativeDamageAction : ActionParameter
  {
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
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
      Action<string> action)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      AppendIsAlreadyExeced(_target.Owner, _num);
      eAccumulativeDamageType accumulativeDamageType = (eAccumulativeDamageType)(float)_valueDictionary[eValueNumber.VALUE_1];
      _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.ACCUMULATIVE_DAMAGE, battleManager.BattleLeftTime, this, _skill);
      AccumulativeDamageData accumulativeDamageData = new AccumulativeDamageData
      {
        AccumulativeDamageType = accumulativeDamageType,
        FixedValue = accumulativeDamageType == eAccumulativeDamageType.FIXED ? (float)_valueDictionary[eValueNumber.VALUE_2] : 0.0f,
        PercentageValue = accumulativeDamageType == eAccumulativeDamageType.PERCENTAGE ? (float)_valueDictionary[eValueNumber.VALUE_2] : 0.0f,
        CountLimit = (int) _valueDictionary[eValueNumber.VALUE_4]
      };
      if (_target.Owner.AccumulativeDamageDataDictionary.ContainsKey(_source))
        return;
      _target.Owner.AccumulativeDamageDataDictionary.Add(_source, accumulativeDamageData);
            //add scripts
            string describe = "使目标受到的伤害逐渐提升";
            action(describe);
            //end add

        }

        public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_2] = (float) (MasterData.action_value_2 + MasterData.action_value_3 * _level);
      Value[eValueNumber.VALUE_4] = (float) (MasterData.action_value_4 + MasterData.action_value_5 * _level);
    }
  }
}
