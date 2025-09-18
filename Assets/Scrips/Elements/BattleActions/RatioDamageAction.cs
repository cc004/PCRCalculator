// Decompiled with JetBrains decompiler
// Type: Elements.RatioDamageAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Elements
{
    public class RatioDamageAction : ActionParameter
    {
        private const float PERCENT_DIGIT = 100f;
        private BasePartsData parts;
        private Dictionary<BasePartsData, LongWithEx> targetCurrentHps;

        public override void ReadyAction(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            base.ReadyAction(_source, _sourceActionController, _skill);
            for (int index = 0; index < HitOnceKeyList.Count; ++index)
                HitOnceDic[HitOnceKeyList[index]] = false;
        }

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            parts = _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget);
            if (targetCurrentHps != null)
                return;
            targetCurrentHps = new Dictionary<BasePartsData, LongWithEx>();
        }

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, DoubleWithEx> _valueDictionary,
          Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            if (ActionDetail3 != 0)
                EnergyChargeMultiple = 1f - ActionDetail3 / 100f;
            ++_target.UbAttackHitCount;
            int actionDetail1 = ActionDetail1;
            if (!HitOnceDic.ContainsKey(_target))
            {
                HitOnceDic.Add(_target, false);
                HitOnceKeyList.Add(_target);
            }
            if (!HitOnceDic[_target])
                targetCurrentHps[_target] = _target.Owner.Hp;
            LongWithEx num = 0;
            switch ((eTargetParameter)ActionDetail1)
            {
                case eTargetParameter.MAX_HP:
                    num = BattleUtil.DoubleToLong((_target.Owner.MaxHp * _valueDictionary[eValueNumber.VALUE_1] / 100.0) * ActionExecTimeList[_num].Weight / ActionWeightSum);
                    break;
                case eTargetParameter.CURRENT_HP:
                    num = BattleUtil.DoubleToLong(((DoubleWithEx)targetCurrentHps[_target] * _valueDictionary[eValueNumber.VALUE_1] / 100.0) * ActionExecTimeList[_num].Weight / ActionWeightSum);
                    break;
                case eTargetParameter.START_MAX_HP:
                    num = BattleUtil.DoubleToLong((long)_target.Owner.StartMaxHP * _valueDictionary[eValueNumber.VALUE_1] / 100.0 * base.ActionExecTimeList[_num].Weight / base.ActionWeightSum);
                    break;
            }
            long override_damage = -1;
            if (_valueDictionary[eValueNumber.VALUE_3] != 0)
            {
                var limit_damage = _valueDictionary[eValueNumber.VALUE_3];
                override_damage = BattleUtil.DoubleToLong(limit_damage * ActionExecTimeList[_num].Weight / ActionWeightSum);
            }
            DamageData damageData = new DamageData
            {
                Target = _target,
                Damage = num,
                DamageType = (DamageData.eDamageType)ActionDetail2,
                Source = _source,
                DamageSoundType = DamageData.eDamageSoundType.HIT,
                DamegeEffectType = ActionExecTimeList[_num].DamageNumType,
                DamegeNumScale = ActionExecTimeList[_num].DamageNumScale,
                IgnoreDef = true,
                LifeSteal = _source.IsPartsBoss ? parts.GetLifeStealZero() : (int)_source.LifeStealZero,
                ActionType = eActionType.RATIO_DAMAGE,
                IsAlwaysChargeEnegry = IsAlwaysChargeEnegry,
                OverrideDamageLimit = override_damage
            };
            UnitCtrl owner = _target.Owner;
            DamageData _damageData = damageData;
            OnDamageHitDelegate onDamageHit = OnDamageHit;
            Skill skill = _skill;
            int actionId = ActionId;
            OnDamageHitDelegate _onDamageHit = onDamageHit;
            Skill _skill1 = skill;
            double weight = ActionExecTimeList[_num].Weight;
            double actionWeightSum = ActionWeightSum;
            double energyChargeMultiple = EnergyChargeMultiple;
            action?.Invoke(((eTargetParameter)ActionDetail1).GetDescription() + "的" + _valueDictionary[eValueNumber.VALUE_1] + "%伤害\n");
            
            _target.Owner.lastBarrier = null;
            
            if (owner.SetDamage(_damageData, true, actionId, _onDamageHit, _skill: _skill1,
                    _damageWeight: ((float) weight), _damageWeightSum: ((float) actionWeightSum),
                    _energyChargeMultiple: ((float) energyChargeMultiple), callBack: action, _usedChargeEnergyByReceiveDamage: UsedChargeEnergyByReceiveDamage) !=
                0L) HitOnceDic[_target] = true;
            
            HitOnceProbDic[_target] = _target.Owner.lastBarrier;
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
        }

        private enum eTargetParameter
        {
            [Description("最大HP")]
            MAX_HP = 1,
            [Description("当前HP")]
            CURRENT_HP = 2,
            [Description("开局最大HP")]
            START_MAX_HP
        }
    }
}
