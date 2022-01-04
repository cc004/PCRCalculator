// Decompiled with JetBrains decompiler
// Type: Elements.RatioDamageAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Elements
{
    public class RatioDamageAction : ActionParameter
    {
        private const float PERCENT_DIGIT = 100f;
        private BasePartsData parts;
        private Dictionary<BasePartsData, FloatWithEx> targetCurrentHps;

        public override void ReadyAction(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            base.ReadyAction(_source, _sourceActionController, _skill);
            for (int index = 0; index < this.HitOnceKeyList.Count; ++index)
                this.HitOnceDic[this.HitOnceKeyList[index]] = false;
        }

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            this.parts = (BasePartsData)_source.BossPartsListForBattle.Find((Predicate<PartsData>)(e => e.Index == _skill.ParameterTarget));
            if (this.targetCurrentHps != null)
                return;
            this.targetCurrentHps = new Dictionary<BasePartsData, FloatWithEx>();
        }

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            ++_target.UbAttackHitCount;
            int actionDetail1 = this.ActionDetail1;
            if (!this.HitOnceDic.ContainsKey(_target))
            {
                this.HitOnceDic.Add(_target, false);
                this.HitOnceKeyList.Add(_target);
            }
            if (!this.HitOnceDic[_target])
                this.targetCurrentHps[_target] = _target.Owner.Hp;
            FloatWithEx num = 0;
            switch ((RatioDamageAction.eTargetParameter)this.ActionDetail1)
            {
                case RatioDamageAction.eTargetParameter.MAX_HP:
                    num = BattleUtil.FloatToInt(((float)_target.Owner.MaxHp * _valueDictionary[eValueNumber.VALUE_1] / 100f) * this.ActionExecTimeList[_num].Weight / this.ActionWeightSum);
                    break;
                case RatioDamageAction.eTargetParameter.CURRENT_HP:
                    num = BattleUtil.FloatToInt((this.targetCurrentHps[_target] * _valueDictionary[eValueNumber.VALUE_1] / 100f) * this.ActionExecTimeList[_num].Weight / this.ActionWeightSum);
                    break;
            }
            DamageData damageData = new DamageData()
            {
                Target = _target,
                Damage = num,
                DamageType = (DamageData.eDamageType)this.ActionDetail2,
                Source = _source,
                DamageSoundType = DamageData.eDamageSoundType.HIT,
                DamegeEffectType = this.ActionExecTimeList[_num].DamageNumType,
                DamegeNumScale = this.ActionExecTimeList[_num].DamageNumScale,
                IgnoreDef = true,
                LifeSteal = _source.IsPartsBoss ? this.parts.GetLifeStealZero() : (int)_source.LifeStealZero,
                ActionType = eActionType.RATIO_DAMAGE
            };
            UnitCtrl owner = _target.Owner;
            DamageData _damageData = damageData;
            ActionParameter.OnDamageHitDelegate onDamageHit = this.OnDamageHit;
            Skill skill = _skill;
            int actionId = this.ActionId;
            ActionParameter.OnDamageHitDelegate _onDamageHit = onDamageHit;
            Skill _skill1 = skill;
            double weight = (double)this.ActionExecTimeList[_num].Weight;
            double actionWeightSum = (double)this.ActionWeightSum;
            double energyChargeMultiple = (double)this.EnergyChargeMultiple;
            action?.Invoke(((eTargetParameter)ActionDetail1).GetDescription() + "的" + _valueDictionary[eValueNumber.VALUE_1] + "%伤害\n");
            if (owner.SetDamage(_damageData, true, actionId, _onDamageHit, _skill: _skill1, _damageWeight: ((float)weight), _damageWeightSum: ((float)actionWeightSum), _energyChargeMultiple: ((float)energyChargeMultiple),callBack:action) == 0L)
                return;
            this.HitOnceDic[_target] = true;

        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
        }

        private enum eTargetParameter
        {
            [Description("最大HP")]
            MAX_HP = 1,
            [Description("当前HP")]
            CURRENT_HP = 2,
        }
    }
}
