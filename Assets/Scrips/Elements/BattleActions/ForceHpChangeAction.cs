// Decompiled with JetBrains decompiler
// Type: Elements.ForceHpChangeAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class ForceHpChangeAction : ActionParameter
  {
    private bool isEnergyGainEnabled;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      _skill.IsLifeStealEnabled = false;
      isEnergyGainEnabled = (uint) (int) MasterData.action_detail_1 > 0U;
    }

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
      if (_target.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION))
        return;
      long _hpDiff = (long) _target.Owner.Hp - (long) _valueDictionary[eValueNumber.VALUE_1];
      if (_hpDiff >= 0L)
        damageTarget(_source, _target, _num, _skill, _hpDiff);
      else
        recoverTarget(_source, _target, (int) -_hpDiff);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_1] = (float) (double) MasterData.action_value_1;
    }

    private void damageTarget(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      Skill _skill,
      long _hpDiff)
    {
      DamageData _damageData = new DamageData
      {
        Damage = _hpDiff,
        Target = _target,
        Source = _source,
        DamageType = DamageData.eDamageType.NONE,
        CriticalRate = -1f,
        IgnoreDef = true,
        CriticalDamageRate = 0.0f,
        ActionType = eActionType.FORCE_HP_CHANGE
      };
      _target.Owner.SetDamage(_damageData, true, ActionId, OnDamageHit, _skill: _skill, _energyAdd: isEnergyGainEnabled, _onDefeat: OnDefeatEnemy, _damageWeight: ActionExecTimeList[_num].Weight, _damageWeightSum: ActionWeightSum, _energyChargeMultiple: EnergyChargeMultiple);
    }

        private void recoverTarget(UnitCtrl _source, BasePartsData _target, int _hpDiff)
        {
            int num = Mathf.Min(_hpDiff, (int)(_target.Owner.MaxHp - (long)_target.Owner.Hp));
            _target.Owner.SetRecovery(num, UnitCtrl.eInhibitHealType.NO_EFFECT, _source, _isEffect: (EffectType == eEffectType.COMMON), _target: _target);
        }
    }
}
