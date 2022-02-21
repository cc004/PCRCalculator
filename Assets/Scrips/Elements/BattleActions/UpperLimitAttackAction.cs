// Decompiled with JetBrains decompiler
// Type: Elements.UpperLimitAttackAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class UpperLimitAttackAction : AttackActionBase
  {
    private const int CRITICAL_RATE_MAX = 1;

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
      ++_target.UbAttackHitCount;
      eAttackType actionDetail1 = (eAttackType) ActionDetail1;
      if (!HitOnceDic.ContainsKey(_target))
        HitOnceDic.Add(_target, false);
      if (judgeMiss(_target, _source, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, actionDetail1))
        return;
      DamageData damageData = createDamageData(_source, _target, _num, _valueDictionary, actionDetail1, false, _skill, eActionType.UPPER_LIMIT_ATTACK);
      if (_target.Owner.SetDamage(damageData, true, ActionId, OnDamageHit, _skill: _skill, _onDefeat: OnDefeatEnemy, _damageWeight: ActionExecTimeList[_num].Weight, _damageWeightSum: ActionWeightSum, _upperLimitFunc: (_damage, _criticalRate) =>
          {
              float num = ActionExecTimeList[_num].Weight / ActionWeightSum;
              _damage = (int) (_damage / (double) num / _criticalRate);
              int actionDetail2 = ActionDetail2;
              int actionDetail3 = ActionDetail3;
              if (_damage > actionDetail3)
                  _damage = (int) (actionDetail2 + (actionDetail3 - actionDetail2) / (double) _valueDictionary[eValueNumber.VALUE_5] + (_damage - actionDetail3) / (double) _valueDictionary[eValueNumber.VALUE_6]);
              else if (_damage > actionDetail2)
                  _damage = (int) (actionDetail2 + (_damage - actionDetail2) / (double) _valueDictionary[eValueNumber.VALUE_5]);
              return (int) (_criticalRate * (double) Mathf.Min(_damage, (int) _valueDictionary[eValueNumber.VALUE_7]) * num);
          }, _energyChargeMultiple: EnergyChargeMultiple) != 0L)
        HitOnceDic[_target] = true;
      if (_skill.AnimId != eSpineCharacterAnimeId.ATTACK)
        return;
      //_target.ShowHitEffect(_source.WeaponSeType, _skill, _source.IsLeftDir);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_1] = (float) (MasterData.action_value_1 + MasterData.action_value_2 * _level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
    }
  }
}
