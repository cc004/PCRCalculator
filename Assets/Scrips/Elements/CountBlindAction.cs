// Decompiled with JetBrains decompiler
// Type: Elements.CountBlindAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections.Generic;

namespace Elements
{
  public class CountBlindAction : ActionParameter
  {
    private const int MAX_NUMBER = 99999;

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
      Dictionary<eValueNumber, float> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());

            if ((double) BattleManager.Random(0.0f, 1f,
                new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 19, (float)pp)) <= (double) BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
      {
        this.AppendIsAlreadyExeced(_target.Owner, _num);
        switch ((CountBlindAction.eCountBlindType) _valueDictionary[eValueNumber.VALUE_1])
        {
          case CountBlindAction.eCountBlindType.TIME:
            _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.COUNT_BLIND, _valueDictionary[eValueNumber.VALUE_2], (ActionParameter) this, _skill, 99999f, (float) this.ActionDetail1);
            break;
          case CountBlindAction.eCountBlindType.COUNT:
            _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.COUNT_BLIND, 90f, (ActionParameter) this, _skill, _valueDictionary[eValueNumber.VALUE_2], (float) this.ActionDetail1);
            break;
        }
      }
      else
      {
        ActionExecedData actionExecedData = this.AlreadyExecedData[_target.Owner][_num];
        if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
          return;
        if (actionExecedData.TargetPartsNumber == 1)
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_DARK, _parts: _target);
        else
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_DARK);
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_2] = (float) ((double) this.MasterData.action_value_2 + (double) this.MasterData.action_value_3 * (double) _level);
    }

    private enum eCountBlindType
    {
      TIME = 1,
      COUNT = 2,
    }
  }
}
