// Decompiled with JetBrains decompiler
// Type: Elements.BuffDebuffClearAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections.Generic;

namespace Elements
{
  public class BuffDebuffClearAction : ActionParameter
  {
    private const float CLEAR_SUCCESS_RATE_BASE = 100f;

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
      bool flag = (double) BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 18, BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))) >= (double) BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()) && this.ActionDetail1 == 1;
      if ((double) BattleManager.Random(0.0f, 1f,
          new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 18, Value[eValueNumber.VALUE_1] / 100.0f)) > (double) _valueDictionary[eValueNumber.VALUE_1] / 100.0 | flag && (double) this.Value[eValueNumber.VALUE_3] == 0.0)
      {
        ActionExecedData actionExecedData = this.AlreadyExecedData[_target.Owner][_num];
        if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
          return;
        if (actionExecedData.TargetPartsNumber == 1)
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_ATTACK, _parts: _target);
        else
          _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_ATTACK);
      }
      else
      {
        this.AppendIsAlreadyExeced(_target.Owner, _num);
        switch ((BuffDebuffClearAction.eDetail1Type) this.ActionDetail1)
        {
          case BuffDebuffClearAction.eDetail1Type.BUFF:
            _target.Owner.DespeleBuffDebuff(true, this.CreateAbnormalEffectData());
            break;
          case BuffDebuffClearAction.eDetail1Type.DEBUFF:
            _target.Owner.DespeleBuffDebuff(false, this.CreateAbnormalEffectData());
            break;
        }
      }
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
    }

    private enum eDetail1Type
    {
      BUFF = 1,
      DEBUFF = 2,
    }
  }
}
