// Decompiled with JetBrains decompiler
// Type: Elements.SilenceAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections.Generic;

namespace Elements
{
  public class SilenceAction : ActionParameter
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
      Dictionary<eValueNumber, float> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            double pp = _valueDictionary[eValueNumber.VALUE_3] * (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());
            if ((double) BattleManager.Random(0.0f, 1f,
                new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 14, (float)pp)) < (double) _valueDictionary[eValueNumber.VALUE_3] * (double) BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
      {
        UnitCtrl.eAbnormalState _abnormalState = UnitCtrl.eAbnormalState.SILENCE;
        switch ((SilenceAction.eSilenceType) this.ActionDetail1)
        {
          case SilenceAction.eSilenceType.NORMAL:
            _abnormalState = UnitCtrl.eAbnormalState.SILENCE;
            break;
          case SilenceAction.eSilenceType.UB:
            _abnormalState = UnitCtrl.eAbnormalState.UB_SILENCE;
            break;
        }
        this.AppendIsAlreadyExeced(_target.Owner, _num);
        _target.Owner.SetAbnormalState(_source, _abnormalState, this.AbnormalStateFieldAction == null ? _valueDictionary[eValueNumber.VALUE_1] : 90f, (ActionParameter) this, _skill);
      }
      else
      {
        ActionExecedData actionExecedData = this.AlreadyExecedData[_target.Owner][_num];
        if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
          return;
        if (actionExecedData.TargetPartsNumber == 1)
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SILENCE, _parts: _target);
        else
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SILENCE);
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
    }

    private enum eSilenceType
    {
      NORMAL,
      UB,
    }
  }
}
