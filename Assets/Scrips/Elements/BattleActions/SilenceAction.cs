// Decompiled with JetBrains decompiler
// Type: Elements.SilenceAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;

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
      ValueDictionary _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      // double pp = _valueDictionary[eValueNumber.VALUE_3] * (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target);
      double pp = BattleUtil.CalcDodgeByLevelDiffUseParam(_skill.Level, _source, _target, IsExEquipSkillAction, (float)_valueDictionary[eValueNumber.VALUE_3]);
      var randomData = new RandomData(_source, _target.Owner, ActionId, 14, (float)pp);
      bool forceHit = _target.Owner.ConsumeForceHit(ActionType, ActionDetail1)
          || ((eSilenceType)ActionDetail1 == eSilenceType.UB && ActionDetail2 == 1);
      if (BattleManager.Random(0.0f, 1f, ref randomData) < pp || forceHit)
      {
        UnitCtrl.eAbnormalState _abnormalState = UnitCtrl.eAbnormalState.SILENCE;
        switch ((eSilenceType)ActionDetail1)
        {
          case eSilenceType.NORMAL:
            _abnormalState = UnitCtrl.eAbnormalState.SILENCE;
            break;
          case eSilenceType.UB:
            _abnormalState = UnitCtrl.eAbnormalState.UB_SILENCE;
            break;
          case eSilenceType.BERSERK:
            _abnormalState = UnitCtrl.eAbnormalState.BERSERK;
            break;
        }
        AppendIsAlreadyExeced(_target.Owner, _num);
        _target.Owner.SetAbnormalState(_source, _abnormalState, AbnormalStateFieldAction == null ? (float)_valueDictionary[eValueNumber.VALUE_1] : 90f, this, _skill, _isIgnoreDebuffImmune: IsIgnoreDebuffImmune());
      }
      else
      {
        ActionExecedData actionExecedData = AlreadyExecedData[_target.Owner][_num];
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
      Value[eValueNumber.VALUE_1] = MasterData.action_value_1 + MasterData.action_value_2 * _level;
      Value[eValueNumber.VALUE_3] = MasterData.action_value_3 + MasterData.action_value_4 * _level;
    }

    /// <summary>action_value_6 ≈ 1 时无视「debuff 免疫」强制生效（对应官方 SilenceAction.IsIgnoreDebuffImmune）。</summary>
    public override bool IsIgnoreDebuffImmune()
    {
      return BattleUtil.Approximately(Value[eValueNumber.VALUE_6], 1.0);
    }

    private enum eSilenceType
    {
      NORMAL,
      UB,
      BERSERK,
    }
  }
}
