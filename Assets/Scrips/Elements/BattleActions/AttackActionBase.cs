// Decompiled with JetBrains decompiler
// Type: Elements.AttackActionBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using Cute;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
    public class AttackActionBase : ActionParameter
    {
        private const int CRITICAL_RATE_MAX = 1;
        private BasePartsData parts;

        public override void ReadyAction(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            base.ReadyAction(_source, _sourceActionController, _skill);
            for (int index = 0; index < HitOnceKeyList.Count; ++index)
                HitOnceDic[HitOnceKeyList[index]] = false;
            CriticalDataDictionary.Clear();
            TotalDamageDictionary.Clear();
        }

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            parts = _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget);
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
        }

        protected bool judgeMiss(
          BasePartsData _target,
          UnitCtrl _source,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          eAttackType _actionDetail1)
        {
            //start add
            if (MyGameCtrl.Instance.tempData.isGuildBattle && (MyGameCtrl.Instance.tempData.randomData.ForceIgnoreDodge_player && !_source.IsOther || MyGameCtrl.Instance.tempData.randomData.ForceIgnoreDodge_enemy && _source.IsOther))
                return false;
            //end add
            var _accuracy = _source.IsPartsBoss ? parts.GetAccuracyZero() : (int)_source.AccuracyZero;

            float num = BattleManager.Random(0.0f, 1f, new RandomData(_source, _target.Owner, ActionId, 2, _target.GetDodgeRate(_accuracy)));
            //start add
            if(MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(_source,_target.Owner,_skill,ActionType,BattleHeaderController.CurrentFrameCount,out float fix))
            {
                num = fix;
            }
            //end add
            if (_skill.CountBlind && _skill.CountBlindType == _actionDetail1)
            {
                if (num < (double)_target.GetDodgeRate(_accuracy) && _actionDetail1 == eAttackType.PHYSICAL)
                    _target.Owner.OnDodge.Call();
                _target.SetMissAtk(_source, eMissLogType.DODGE_ATTACK_DARK, ActionExecTimeList[_num].DamageNumType, ActionExecTimeList[_num].DamageNumScale);
                return true;
            }
            if (_source.IsAbnormalState(UnitCtrl.eAbnormalState.COUNT_BLIND))
            {
                _skill.CountBlindType = (eAttackType)_source.GetAbnormalStateSubValue(UnitCtrl.eAbnormalStateCategory.COUNT_BLIND);
                if (_skill.CountBlindType == _actionDetail1)
                {
                    if (num < (double)_target.GetDodgeRate(_accuracy) && _actionDetail1 == eAttackType.PHYSICAL)
                        _target.Owner.OnDodge.Call();
                    _skill.CountBlind = true;
                    _source.DecreaseCountBlind();
                    _target.SetMissAtk(_source, eMissLogType.DODGE_ATTACK_DARK, ActionExecTimeList[_num].DamageNumType, ActionExecTimeList[_num].DamageNumScale);
                    return true;
                }
            }
            if (num < (double)_target.GetDodgeRate(_accuracy) && _actionDetail1 == eAttackType.PHYSICAL)
            {
                _target.SetMissAtk(_source, eMissLogType.DODGE_ATTACK, ActionExecTimeList[_num].DamageNumType, ActionExecTimeList[_num].DamageNumScale);
                _target.Owner.OnDodge.Call();
                return true;
            }
            return judgeDarkMiss(UnitCtrl.eAbnormalState.PHYSICS_DARK, _actionDetail1, _target, _source, _num) || judgeDarkMiss(UnitCtrl.eAbnormalState.MAGIC_DARK, _actionDetail1, _target, _source, _num);
        }
        private bool judgeDarkMiss(  UnitCtrl.eAbnormalState _abnormalState,  eAttackType _attackType,  BasePartsData _target,UnitCtrl _source,  int _num)
        {
            if (!_source.IsAbnormalState(_abnormalState))
                return false;
            bool flag = _abnormalState == UnitCtrl.eAbnormalState.PHYSICS_DARK;
            eAttackType eAttackType = flag ? eAttackType.PHYSICAL : eAttackType.MAGIC;
            if (_attackType != eAttackType)
                return false;
            UnitCtrl.eAbnormalStateCategory _abnormalStateCategory = flag ? UnitCtrl.eAbnormalStateCategory.PHYSICAL_DARK : UnitCtrl.eAbnormalStateCategory.MAGIC_DARK;
            double pp = 1.0 - _source.GetAbnormalStateMainValue(_abnormalStateCategory) / 100.0;
            if (BattleManager.Random(0.0f, 1f,
                    new RandomData(_source, _target.Owner, ActionId, 3, (float)pp)) >= 1.0 - _source.GetAbnormalStateMainValue(_abnormalStateCategory) / 100.0)
                return false;
            ActionExecTime actionExecTime = ActionExecTimeList[_num];
            _target.SetMissAtk(_source, eMissLogType.DODGE_ATTACK_DARK, actionExecTime.DamageNumType, actionExecTime.DamageNumScale);
            return true;
        }


        /*protected DamageData createDamageData(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          eAttackType _actionDetail1,
          bool _isCritical,
          Skill _skill,
          eActionType _actionType)
        {

            bool flag = judgeIsPhysical(_actionDetail1);
            FloatWithEx num1;
            int num2;
            int level;
            if (!_source.IsPartsBoss)
            {
                num1 = (flag ? _source.AtkZero : _source.MagicStrZero);
                num2 = flag ? _source.PhysicalCriticalZero : _source.MagicCriticalZero;
                level = _source.Level;
            }
            else
            {
                num1 = flag ? parts.GetAtkZero() : parts.GetMagicStrZero();
                num2 = flag ? parts.GetPhysicalCriticalZero() : parts.GetMagicCriticalZero();
                level = parts.GetLevel();
            }
            var num3 = BattleUtil.FloatToInt(_valueDictionary[eValueNumber.VALUE_1] + num1 * _valueDictionary[eValueNumber.VALUE_3]);
             var num4 = BattleUtil.FloatToInt((_valueDictionary[eValueNumber.VALUE_1] + num1 * _valueDictionary[eValueNumber.VALUE_3]) * ActionExecTimeList[_num].Weight / ActionWeightSum);
             if (_target.Owner.AccumulativeDamageDataDictionary.ContainsKey(_source))
             {
                 AccumulativeDamageData accumulativeDamageData = _target.Owner.AccumulativeDamageDataDictionary[_source];
                 switch (accumulativeDamageData.AccumulativeDamageType)
                 {
                     case eAccumulativeDamageType.FIXED:
                         num4 = num4 + (float)(int)(accumulativeDamageData.DamagedCount * (double)accumulativeDamageData.FixedValue);
                         break;
                     case eAccumulativeDamageType.PERCENTAGE:
                         num4 = num4 + (float)(int)((double)(num4 * (float)accumulativeDamageData.DamagedCount) * accumulativeDamageData.PercentageValue / 100.0);
                         break;
                 }
                 accumulativeDamageData.DamagedCount = Mathf.Min(accumulativeDamageData.DamagedCount + 1, accumulativeDamageData.CountLimit);
             }           

            float num5 = getCriticalDamageRate(_valueDictionary) * (flag ? _source.PhysicalCriticalDamageRateOrMin / 100f : (int)_source.MagicCriticalDamageRateOrMin / 100f);
            return new DamageData
            {
                TotalDamageForLogBarrier = num3,
                Damage = num4,
                Target = _target,
                Source = _source,
                DamageType = flag ? DamageData.eDamageType.ATK : DamageData.eDamageType.MGC,
                CriticalRate = _isCritical ? 1f : BattleUtil.GetCriticalRate(num2, level, _target.GetLevel()),
                DamegeEffectType = ActionExecTimeList[_num].DamageNumType,
                DamegeNumScale = ActionExecTimeList[_num].DamageNumScale,
                DefPenetrate = flag ? _source.PhysicalPenetrateZero : _source.MagicPenetrateZero,
                LifeSteal = _source.IsPartsBoss ? parts.GetLifeStealZero() : (int)_source.LifeStealZero,
                CriticalDamageRate = num5,
                ActionType = _actionType
            };
        }*/
        protected DamageData createDamageData(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          eAttackType _actionDetail1,
          bool _isCritical,
          Skill _skill,
          eActionType _actionType,
          bool _isPhysicalForTarget)
        {
            bool flag = judgeIsPhysical(_actionDetail1);
            FloatWithEx num = 0;
            int num2 = 0;
            int num3 = 0;
            if (!_source.IsPartsBoss)
            {
                num = (flag ? _source.AtkZero : _source.MagicStrZero);
                num2 = (flag ? _source.PhysicalCriticalZero : _source.MagicCriticalZero);
                num3 = _source.Level;
            }
            else
            {
                num = (flag ? parts.GetAtkZero() : parts.GetMagicStrZero());
                num2 = (flag ? parts.GetPhysicalCriticalZero() : parts.GetMagicCriticalZero());
                num3 = parts.GetLevel();
            }
            FloatWithEx num4 = calcTotalDamage(_source, _target, _num, _valueDictionary, num);
            FloatWithEx num5 = calcDamage(_source, _target, _num, _valueDictionary, num);
            float num6 = 0f;
            num6 = ((!_isPhysicalForTarget) ? ((float)_target.Owner.MagicReceiveDamagePercentOrMin / 100f) : ((float)_target.Owner.PhysicalReceiveDamagePercentOrMin / 100f));
            num4 = BattleUtil.FloatToInt(num4 * num6);
            num5 = BattleUtil.FloatToInt(num5 * num6);
            if (_target.Owner.AccumulativeDamageDataDictionary.TryGetValue(_source, out var value))
            {
                value.DamagedCount++;
            }
            float criticalDamageRate = getCriticalDamageRate(_valueDictionary);
            float num7 = (flag ? ((float)(int)_source.PhysicalCriticalDamageRateOrMin / 100f) : ((float)(int)_source.MagicCriticalDamageRateOrMin / 100f));
            float num8 = (float)(int)_target.Owner.ReceiveCriticalDamageRateOrMin / 100f;
            criticalDamageRate *= Mathf.Max(0.5f, num7 * num8);
            DamageData damageData = new DamageData
            {
                TotalDamageForLogBarrier = num4,
                Damage = num5,
                Target = _target,
                Source = _source,
                DamageType = (_isPhysicalForTarget ? DamageData.eDamageType.ATK : DamageData.eDamageType.MGC),
                CriticalRate = (_isCritical ? 1f : BattleUtil.GetCriticalRate(num2, num3, _target.GetLevel())),
                CriticalRateForLogBarrier = BattleUtil.GetCriticalRate(num2, num3, _target.GetLevel()),
                DamegeEffectType = base.ActionExecTimeList[_num].DamageNumType,
                DamegeNumScale = base.ActionExecTimeList[_num].DamageNumScale,
                DefPenetrate = (_isPhysicalForTarget ? _source.PhysicalPenetrateZero : _source.MagicPenetrateZero),
                LifeSteal = (_source.IsPartsBoss ? parts.GetLifeStealZero() : ((int)_source.LifeStealZero)),
                CriticalDamageRate = criticalDamageRate,
                ActionType = _actionType,
                IsAlwaysChargeEnegry = IsAlwaysChargeEnegry
            };
            if (_isPhysicalForTarget)
            {
                if (base.LimitDamageDictionaryAtk.ContainsKey(_target))
                {
                    damageData.LimitDamageFunc = delegate (float _damage)
                    {
                        long num10 = base.LimitDamageDictionaryAtk[_target];
                        if (num10 == 0L)
                        {
                            return 0f;
                        }
                        if (num10 > 0 && (float)num10 < _damage)
                        {
                            base.LimitDamageDictionaryAtk[_target] = 0L;
                            return num10;
                        }
                        base.LimitDamageDictionaryAtk[_target] -= (long)_damage;
                        return _damage;
                    };
                }
            }
            else if (base.LimitDamageDictionaryMgc.ContainsKey(_target))
            {
                damageData.LimitDamageFunc = delegate (float _damage)
                {
                    long num9 = base.LimitDamageDictionaryMgc[_target];
                    if (num9 == 0L)
                    {
                        return 0f;
                    }
                    if (num9 > 0 && (float)num9 < _damage)
                    {
                        base.LimitDamageDictionaryMgc[_target] = 0L;
                        return num9;
                    }
                    base.LimitDamageDictionaryMgc[_target] -= (long)_damage;
                    return _damage;
                };
            }
            return damageData;
        }
        private FloatWithEx calcTotalDamage(UnitCtrl _source, BasePartsData _target, int _num, Dictionary<eValueNumber, FloatWithEx> _valueDictionary, FloatWithEx _atk)
        {
            var num = _valueDictionary[eValueNumber.VALUE_1] + _atk * _valueDictionary[eValueNumber.VALUE_3];
            num *= _target.Owner.GetDebuffDamageUpValue();
            if (!_target.Owner.AccumulativeDamageDataDictionary.TryGetValue(_source, out var value))
            {
                return BattleUtil.FloatToInt(num);
            }
            int num2 = value.DamagedCount - _num;
            FloatWithEx num3 = 0;
            for (int i = 0; i < base.ActionExecTimeList.Count; i++)
            {
                FloatWithEx num4 = BattleUtil.FloatToInt(num * base.ActionExecTimeList[i].Weight / base.ActionWeightSum);
                num4 += value.CalcAdditionalDamage(num2 + i, num4);
                num3 += num4;
            }
            return num3;
        }

        private FloatWithEx calcDamage(UnitCtrl _source, BasePartsData _target, int _num, Dictionary<eValueNumber, FloatWithEx> _valueDictionary, FloatWithEx _atk)
        {
            FloatWithEx num = BattleUtil.FloatToInt((_valueDictionary[eValueNumber.VALUE_1] + _atk * _valueDictionary[eValueNumber.VALUE_3]) * base.ActionExecTimeList[_num].Weight / base.ActionWeightSum);
            if (_target.Owner.AccumulativeDamageDataDictionary.TryGetValue(_source, out var value))
            {
                num += value.CalcAdditionalDamage(value.DamagedCount, num);
            }
            return num;
        }

        protected virtual float getCriticalDamageRate(Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => 1f;

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
            Value[eValueNumber.VALUE_3] = (float)(MasterData.action_value_3 + MasterData.action_value_4 * _level);
        }

        protected bool judgeIsPhysical(eAttackType _attackType) => _attackType == eAttackType.INEVITABLE_PHYSICAL || _attackType == eAttackType.PHYSICAL;

        public enum eAttackType
        {
            PHYSICAL = 1,
            MAGIC = 2,
            INEVITABLE_PHYSICAL = 3,
        }
    }
}
