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
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
        {
            ++_target.UbAttackHitCount;
            eAttackType actionDetail1 = (eAttackType)ActionDetail1;
            if (!HitOnceDic.ContainsKey(_target))
                HitOnceDic.Add(_target, false);
            bool flag = judgeMiss(_target, _source, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, actionDetail1);
            if (flag && base.battleManager.FEDKJAIEDGI == 0f)
            {
                return;
            }
            //if (judgeMiss(_target, _source, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, actionDetail1))
            //    return;
            //DamageData damageData = createDamageData(_source, _target, _num, _valueDictionary, actionDetail1, false, _skill, eActionType.UPPER_LIMIT_ATTACK);
            DamageData damageData = createDamageData(_source, _target, _num, _valueDictionary, actionDetail1, _isCritical: false, _skill, eActionType.UPPER_LIMIT_ATTACK, judgeIsPhysical(actionDetail1));
            Func<int, float, int> upperLimitFunc = delegate (int _damage, float _criticalRate)
            {
                float num = base.ActionExecTimeList[_num].Weight / base.ActionWeightSum;
                _damage = (int)((float)_damage / num / _criticalRate);
                int actionDetail2 = base.ActionDetail2;
                int actionDetail3 = base.ActionDetail3;
                if (_damage > actionDetail3)
                {
                    _damage = (int)((float)actionDetail2 + (float)(actionDetail3 - actionDetail2) / _valueDictionary[eValueNumber.VALUE_5] + (float)(_damage - actionDetail3) / _valueDictionary[eValueNumber.VALUE_6]);
                }
                else if (_damage > actionDetail2)
                {
                    _damage = (int)((float)actionDetail2 + (float)(_damage - actionDetail2) / _valueDictionary[eValueNumber.VALUE_5]);
                }
                return (int)(_criticalRate * (float)Mathf.Min(_damage, (int)_valueDictionary[eValueNumber.VALUE_7]) * num);
            };
            if (flag)
            {
                _target.Owner.RecoverDodgeTP(damageData.DamageType, (long)damageData.Damage, damageData.ActionType, (long)damageData.LogBarrierExpectedDamage, (long)damageData.TotalDamageForLogBarrier, damageData.Source, damageData.IgnoreDef, damageData.Target, damageData.DefPenetrate, upperLimitFunc, _skill, EnergyChargeMultiple);
                return;
            }
            /*if (_target.Owner.SetDamage(damageData, true, ActionId, OnDamageHit, _skill: _skill, _onDefeat: OnDefeatEnemy, _damageWeight: ActionExecTimeList[_num].Weight, _damageWeightSum: ActionWeightSum, _upperLimitFunc: (_damage, _criticalRate) =>
                {
                    float num = ActionExecTimeList[_num].Weight / ActionWeightSum;
                    _damage = (int)(_damage / (double)num / _criticalRate);
                    int actionDetail2 = ActionDetail2;
                    int actionDetail3 = ActionDetail3;
                    if (_damage > actionDetail3)
                        _damage = (int)(actionDetail2 + (actionDetail3 - actionDetail2) / (double)_valueDictionary[eValueNumber.VALUE_5] + (_damage - actionDetail3) / (double)_valueDictionary[eValueNumber.VALUE_6]);
                    else if (_damage > actionDetail2)
                        _damage = (int)(actionDetail2 + (_damage - actionDetail2) / (double)_valueDictionary[eValueNumber.VALUE_5]);
                    return (int)(_criticalRate * (double)Mathf.Min(_damage, (int)_valueDictionary[eValueNumber.VALUE_7]) * num);
                }, _energyChargeMultiple: EnergyChargeMultiple) != 0L)
                HitOnceDic[_target] = true;*/
            _target.Owner.lastBarrier = null;
            if (_target.Owner.SetDamage(damageData, _byAttack: true, base.ActionId, base.OnDamageHit, _hasEffect: true, _skill, _energyAdd: true, base.OnDefeatEnemy, _noMotion: false, base.ActionExecTimeList[_num].Weight, base.ActionWeightSum, upperLimitFunc, EnergyChargeMultiple, base.UsedChargeEnergyByReceiveDamage) != 0L)
            {
                base.HitOnceDic[_target] = true;
            }
            HitOnceProbDic[_target] = _target.Owner.lastBarrier;

            //if (_skill.AnimId != eSpineCharacterAnimeId.ATTACK)
            //    return;
            //_target.ShowHitEffect(_source.WeaponSeType, _skill, _source.IsLeftDir);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
            Value[eValueNumber.VALUE_3] = (float)(MasterData.action_value_3 + MasterData.action_value_4 * _level);
        }
    }
}
