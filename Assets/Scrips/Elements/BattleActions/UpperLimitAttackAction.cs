// Decompiled with JetBrains decompiler
// Type: Elements.UpperLimitAttackAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
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
      Dictionary<eValueNumber, float> _valueDictionary)
    {
      ++_target.UbAttackHitCount;
      AttackActionBase.eAttackType actionDetail1 = (AttackActionBase.eAttackType) this.ActionDetail1;
      if (!this.HitOnceDic.ContainsKey(_target))
        this.HitOnceDic.Add(_target, false);
      if (this.judgeMiss(_target, _source, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, actionDetail1))
        return;
      DamageData damageData = this.createDamageData(_source, _target, _num, _valueDictionary, actionDetail1, false, _skill, eActionType.UPPER_LIMIT_ATTACK);
      if (_target.Owner.SetDamage(damageData, true, this.ActionId, this.OnDamageHit, _skill: _skill, _onDefeat: this.OnDefeatEnemy, _damageWeight: this.ActionExecTimeList[_num].Weight, _damageWeightSum: this.ActionWeightSum, _upperLimitFunc: ((Func<int, float, int>) ((_damage, _criticalRate) =>
      {
        float num = this.ActionExecTimeList[_num].Weight / this.ActionWeightSum;
        _damage = (int) ((double) _damage / (double) num / (double) _criticalRate);
        int actionDetail2 = this.ActionDetail2;
        int actionDetail3 = this.ActionDetail3;
        if (_damage > actionDetail3)
          _damage = (int) ((double) actionDetail2 + (double) (actionDetail3 - actionDetail2) / (double) _valueDictionary[eValueNumber.VALUE_5] + (double) (_damage - actionDetail3) / (double) _valueDictionary[eValueNumber.VALUE_6]);
        else if (_damage > actionDetail2)
          _damage = (int) ((double) actionDetail2 + (double) (_damage - actionDetail2) / (double) _valueDictionary[eValueNumber.VALUE_5]);
        return (int) ((double) _criticalRate * (double) Mathf.Min(_damage, (int) _valueDictionary[eValueNumber.VALUE_7]) * (double) num);
      })), _energyChargeMultiple: this.EnergyChargeMultiple) != 0L)
        this.HitOnceDic[_target] = true;
      if (_skill.AnimId != eSpineCharacterAnimeId.ATTACK)
        return;
      //_target.ShowHitEffect(_source.WeaponSeType, _skill, _source.IsLeftDir);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
    }
  }
}
