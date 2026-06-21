// Decompiled with JetBrains decompiler
// Type: Elements.FearAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;

namespace Elements
{
  public class FearAction : ActionParameter
  {
    private static readonly Dictionary<eFearType, UnitCtrl.eAbnormalState> abnormalStateDic =
      new Dictionary<eFearType, UnitCtrl.eAbnormalState>
      {
        { eFearType.FEAR, UnitCtrl.eAbnormalState.FEAR },
        { eFearType.ALL_FEAR, UnitCtrl.eAbnormalState.ALL_FEAR }
      };

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

      var firstRandomData = new RandomData(_source, _target.Owner, ActionId, 20, (float)_valueDictionary[eValueNumber.VALUE_3]);
      int num1 = BattleManager.Random(0.0f, 1f, ref firstRandomData) <= (double)_valueDictionary[eValueNumber.VALUE_3] ? 1 : 0;
      // double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target);
      double pp = BattleUtil.CalcDodgeByLevelDiffUseParam(_skill.Level, _source, _target, IsExEquipSkillAction, 1.0f);

      var randomData = new RandomData(_source, _target.Owner, ActionId, 20, (float)pp);
      float num2 = BattleManager.Random(0.0f, 1f, ref randomData);
      if (num1 != 0 && num2 <= pp)
      {
        AppendIsAlreadyExeced(_target.Owner, _num);
        _target.Owner.SetAbnormalState(_source, abnormalStateDic[(eFearType)ActionDetail1], (float)_valueDictionary[eValueNumber.VALUE_1], this, _skill);
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
      Value[eValueNumber.VALUE_1] = (MasterData.action_value_1 + MasterData.action_value_2 * _level);
      Value[eValueNumber.VALUE_3] = (MasterData.action_value_3 + MasterData.action_value_4 * _level);
    }

    private enum eFearType
    {
      FEAR = 0,
      ALL_FEAR = 1
    }
  }
}
