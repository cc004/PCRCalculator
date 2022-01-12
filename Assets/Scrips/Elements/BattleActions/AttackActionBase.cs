// Decompiled with JetBrains decompiler
// Type: Elements.AttackActionBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Elements.Battle;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
            for (int index = 0; index < this.HitOnceKeyList.Count; ++index)
                this.HitOnceDic[this.HitOnceKeyList[index]] = false;
            this.CriticalDataDictionary.Clear();
            this.TotalDamageDictionary.Clear();
        }

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            this.parts = (BasePartsData)_source.BossPartsListForBattle.Find((Predicate<PartsData>)(e => e.Index == _skill.ParameterTarget));
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
          AttackActionBase.eAttackType _actionDetail1)
        {
            //start add
            if (MyGameCtrl.Instance.tempData.isGuildBattle && (MyGameCtrl.Instance.tempData.randomData.ForceIgnoreDodge_player && !_source.IsOther || MyGameCtrl.Instance.tempData.randomData.ForceIgnoreDodge_enemy && _source.IsOther))
                return false;
            //end add
            int _accuracy = _source.IsPartsBoss ? this.parts.GetAccuracyZero() : (int)_source.AccuracyZero;

            float num = BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 2, _target.GetDodgeRate(_accuracy)));
            //start add
            if(MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(_source,_target.Owner,_skill,ActionType,BattleHeaderController.CurrentFrameCount,out float fix))
            {
                num = fix;
            }
            //end add
            if (_skill.CountBlind && _skill.CountBlindType == _actionDetail1)
            {
                if ((double)num < (double)_target.GetDodgeRate(_accuracy) && _actionDetail1 == AttackActionBase.eAttackType.PHYSICAL)
                    _target.Owner.OnDodge.Call();
                _target.SetMissAtk(_source, eMissLogType.DODGE_ATTACK_DARK, this.ActionExecTimeList[_num].DamageNumType, this.ActionExecTimeList[_num].DamageNumScale);
                return true;
            }
            if (_source.IsAbnormalState(UnitCtrl.eAbnormalState.COUNT_BLIND))
            {
                _skill.CountBlindType = (AttackActionBase.eAttackType)_source.GetAbnormalStateSubValue(UnitCtrl.eAbnormalStateCategory.COUNT_BLIND);
                if (_skill.CountBlindType == _actionDetail1)
                {
                    if ((double)num < (double)_target.GetDodgeRate(_accuracy) && _actionDetail1 == AttackActionBase.eAttackType.PHYSICAL)
                        _target.Owner.OnDodge.Call();
                    _skill.CountBlind = true;
                    _source.DecreaseCountBlind();
                    _target.SetMissAtk(_source, eMissLogType.DODGE_ATTACK_DARK, this.ActionExecTimeList[_num].DamageNumType, this.ActionExecTimeList[_num].DamageNumScale);
                    return true;
                }
            }
            if ((double)num < (double)_target.GetDodgeRate(_accuracy) && _actionDetail1 == AttackActionBase.eAttackType.PHYSICAL)
            {
                _target.SetMissAtk(_source, eMissLogType.DODGE_ATTACK, this.ActionExecTimeList[_num].DamageNumType, this.ActionExecTimeList[_num].DamageNumScale);
                _target.Owner.OnDodge.Call();
                return true;
            }
            return this.judgeDarkMiss(UnitCtrl.eAbnormalState.PHYSICS_DARK, _actionDetail1, _target, _source, _num) || this.judgeDarkMiss(UnitCtrl.eAbnormalState.MAGIC_DARK, _actionDetail1, _target, _source, _num);
        }
        private bool judgeDarkMiss(  UnitCtrl.eAbnormalState _abnormalState,  AttackActionBase.eAttackType _attackType,  BasePartsData _target,UnitCtrl _source,  int _num)
        {
            if (!_source.IsAbnormalState(_abnormalState))
                return false;
            bool flag = _abnormalState == UnitCtrl.eAbnormalState.PHYSICS_DARK;
            AttackActionBase.eAttackType eAttackType = flag ? AttackActionBase.eAttackType.PHYSICAL : AttackActionBase.eAttackType.MAGIC;
            if (_attackType != eAttackType)
                return false;
            UnitCtrl.eAbnormalStateCategory _abnormalStateCategory = flag ? UnitCtrl.eAbnormalStateCategory.PHYSICAL_DARK : UnitCtrl.eAbnormalStateCategory.MAGIC_DARK;
            double pp = 1.0 - (double)_source.GetAbnormalStateMainValue(_abnormalStateCategory) / 100.0;
            if ((double)BattleManager.Random(0.0f, 1f,
                new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 3, (float)pp)) >= 1.0 - (double)_source.GetAbnormalStateMainValue(_abnormalStateCategory) / 100.0)
                return false;
            ActionExecTime actionExecTime = this.ActionExecTimeList[_num];
            _target.SetMissAtk(_source, eMissLogType.DODGE_ATTACK_DARK, actionExecTime.DamageNumType, actionExecTime.DamageNumScale);
            return true;
        }


        protected DamageData createDamageData(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          AttackActionBase.eAttackType _actionDetail1,
          bool _isCritical,
          Skill _skill,
          eActionType _actionType)
        {

            bool flag = this.judgeIsPhysical(_actionDetail1);
            FloatWithEx num1;
            int num2;
            int level;
            if (!_source.IsPartsBoss)
            {
                num1 = (flag ? _source.AtkZero : _source.MagicStrZero);
                num2 = (int)(flag ? _source.PhysicalCriticalZero : _source.MagicCriticalZero);
                level = (int)_source.Level;
            }
            else
            {
                num1 = flag ? this.parts.GetAtkZero() : this.parts.GetMagicStrZero();
                num2 = flag ? this.parts.GetPhysicalCriticalZero() : this.parts.GetMagicCriticalZero();
                level = this.parts.GetLevel();
            }
            var num3 = BattleUtil.FloatToInt(_valueDictionary[eValueNumber.VALUE_1] + num1 * _valueDictionary[eValueNumber.VALUE_3]);
            var num4 = BattleUtil.FloatToInt((_valueDictionary[eValueNumber.VALUE_1] + num1 * _valueDictionary[eValueNumber.VALUE_3]) * this.ActionExecTimeList[_num].Weight / this.ActionWeightSum);
            if (_target.Owner.AccumulativeDamageDataDictionary.ContainsKey(_source))
            {
                AccumulativeDamageData accumulativeDamageData = _target.Owner.AccumulativeDamageDataDictionary[_source];
                switch (accumulativeDamageData.AccumulativeDamageType)
                {
                    case eAccumulativeDamageType.FIXED:
                        num4 = num4 + (float)(int)((double)accumulativeDamageData.DamagedCount * (double)accumulativeDamageData.FixedValue);
                        break;
                    case eAccumulativeDamageType.PERCENTAGE:
                        num4 = num4 + (float)(int)((double)(num4 * (float)accumulativeDamageData.DamagedCount) * (double)accumulativeDamageData.PercentageValue / 100.0);
                        break;
                }
                accumulativeDamageData.DamagedCount = Mathf.Min(accumulativeDamageData.DamagedCount + 1, accumulativeDamageData.CountLimit);
            }
            float num5 = this.getCriticalDamageRate(_valueDictionary) * (flag ? (float)(int)_source.PhysicalCriticalDamageRateOrMin / 100f : (float)(int)_source.MagicCriticalDamageRateOrMin / 100f);
            return new DamageData()
            {
                TotalDamageForLogBarrier = num3,
                Damage = num4,
                Target = _target,
                Source = _source,
                DamageType = flag ? DamageData.eDamageType.ATK : DamageData.eDamageType.MGC,
                CriticalRate = _isCritical ? 1f : BattleUtil.GetCriticalRate((float)num2, level, _target.GetLevel()),
                DamegeEffectType = this.ActionExecTimeList[_num].DamageNumType,
                DamegeNumScale = this.ActionExecTimeList[_num].DamageNumScale,
                DefPenetrate = (int)(flag ? _source.PhysicalPenetrateZero : _source.MagicPenetrateZero),
                LifeSteal = _source.IsPartsBoss ? this.parts.GetLifeStealZero() : (int)_source.LifeStealZero,
                CriticalDamageRate = num5,
                ActionType = _actionType
            };
        }

        protected virtual float getCriticalDamageRate(Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => 1f;

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
            this.Value[eValueNumber.VALUE_3] = (float)((double)this.MasterData.action_value_3 + (double)this.MasterData.action_value_4 * (double)_level);
        }

        protected bool judgeIsPhysical(AttackActionBase.eAttackType _attackType) => _attackType == AttackActionBase.eAttackType.INEVITABLE_PHYSICAL || _attackType == AttackActionBase.eAttackType.PHYSICAL;

        public enum eAttackType
        {
            PHYSICAL = 1,
            MAGIC = 2,
            INEVITABLE_PHYSICAL = 3,
        }
    }
}
