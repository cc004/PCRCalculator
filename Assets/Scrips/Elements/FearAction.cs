// Decompiled with JetBrains decompiler
// Type: Elements.FearAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections.Generic;

namespace Elements
{
  public class FearAction : ActionParameter
  {
    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _starttime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, float> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      int num1 = (double) BattleManager.Random(0.0f, 1f,
          new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 20, (float)_valueDictionary[eValueNumber.VALUE_3])) <= (double) _valueDictionary[eValueNumber.VALUE_3] ? 1 : 0;
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());

            float num2 = BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 20, (float)pp));
      if (num1 != 0 && (double) num2 <= (double) BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
      {
        this.AppendIsAlreadyExeced(_target.Owner, _num);
        _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.FEAR, _valueDictionary[eValueNumber.VALUE_1], (ActionParameter) this, _skill);
      }
      else
      {
        ActionExecedData actionExecedData = this.AlreadyExecedData[_target.Owner][_num];
        if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
          return;
        if (actionExecedData.TargetPartsNumber == 1)
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_CHARM, _parts: _target);
        else
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_CHARM);
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
    }
  }
}
