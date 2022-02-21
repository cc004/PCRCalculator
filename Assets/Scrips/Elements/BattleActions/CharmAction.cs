// Decompiled with JetBrains decompiler
// Type: Elements.CharmAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;

namespace Elements
{
  public class CharmAction : ActionParameter
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
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            double pp = (double)_valueDictionary[eValueNumber.VALUE_3] * BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());
            if (BattleManager.Random(0.0f, 1f,
                    new RandomData(_source, _target.Owner, ActionId, 7, (float)pp)) < (double) _valueDictionary[eValueNumber.VALUE_3] * BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
      {
        AppendIsAlreadyExeced(_target.Owner, _num);
        UnitCtrl.eAbnormalState _abnormalState = UnitCtrl.eAbnormalState.CONVERT;
        switch ((eEffectType) ActionDetail1)
        {
          case eEffectType.CONVERT:
            _abnormalState = UnitCtrl.eAbnormalState.CONVERT;
            break;
          case eEffectType.CONFUSION:
            _abnormalState = UnitCtrl.eAbnormalState.CONFUSION;
            break;
        }
        _target.Owner.SetAbnormalState(_source, _abnormalState, AbnormalStateFieldAction == null ? (float)_valueDictionary[eValueNumber.VALUE_1] : 90f, this, _skill);
      }
      else
      {
        ActionExecedData actionExecedData = AlreadyExecedData[_target.Owner][_num];
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
      Value[eValueNumber.VALUE_1] = (float) (MasterData.action_value_1 + MasterData.action_value_2 * _level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
    }

    private enum eEffectType
    {
      CONVERT,
      CONFUSION,
    }
  }
}
