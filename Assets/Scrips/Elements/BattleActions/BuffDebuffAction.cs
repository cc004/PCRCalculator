// Decompiled with JetBrains decompiler
// Type: Elements.BuffDebuffAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class BuffDebuffAction : ActionParameter
    {
        private const int THOUSAND = 1000;
        private const float PERCENT_DIGIT = 100f;
        private const int UNDESPELABLE_NUMBER = 2;
        private const int DETAIL_DIGIT = 10;
        private const int DETAIL_DEBUFF = 1;
        public int EnvironmentId { get; set; } // TODO implement


        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            PartsData partsData = _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget);
            //if (ActionDetail2 != 2)
            //    return;
            eBuffDebuffStartReleaseType actionDetail = (eBuffDebuffStartReleaseType)base.ActionDetail2;
            if (actionDetail != eBuffDebuffStartReleaseType.BREAK)
            {
                return;
            }
            Dictionary<BasePartsData, LongWithEx> buffParam = new Dictionary<BasePartsData, LongWithEx>();
            bool isDebuff = ActionDetail1 % 10 == 1;
            UnitCtrl.BuffParamKind buffParamKind = GetChangeParamKind(ActionDetail1);
            partsData.OnBreak += () =>
            {
                if (_source.IsPartsBoss)
                {
                    for (int index = 0; index < _source.BossPartsListForBattle.Count; ++index)
                        buffParam.Add(_source.BossPartsListForBattle[index], CalculateBuffDebuffParam(_source.BossPartsListForBattle[index], Value[eValueNumber.VALUE_2], (eChangeParameterType)(float)Value[eValueNumber.VALUE_1], buffParamKind, isDebuff));
                }
                //_source.SetBuffParam(UnitCtrl.BuffParamKind.NUM, buffParam, 0.0f, 0, null, true, eEffectType.COMMON, !isDebuff, getIsAdditional());
                //_source.EnableBuffParam(buffParamKind, buffParam, true, _source, !isDebuff, getIsAdditional(),90);

                _source.SetBuffParam(UnitCtrl.BuffParamKind.NUM, buffParamKind, buffParam, 0f, 0, _source, _despelable: true, eEffectType.COMMON, !isDebuff, getIsAdditional(), isShowIcon());
                _source.EnableBuffParam(buffParamKind, buffParam, _enable: true, _source, !isDebuff, getIsAdditional(), _triggerCall: TriggerCall);

            };
            partsData.OnBreakEnd += () =>
            {
                //_source.EnableBuffParam(buffParamKind, buffParam, false, _source, !isDebuff, getIsAdditional(),90);
                _source.EnableBuffParam(buffParamKind, buffParam, _enable: false, _source, !isDebuff, getIsAdditional());

                buffParam.Clear();
            };
        }
        private bool isShowIcon()
        {
            return base.Value[eValueNumber.VALUE_6] == 0f;
        }
        private bool getIsAdditional() => ActionDetail1 / 1000 == 1;

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          ValueDictionary _valueDictionary,
          Action<string> action = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            AppendIsAlreadyExeced(_target.Owner, _num);
            //if (ActionDetail2 == 2)
            //    return;
            eBuffDebuffStartReleaseType actionDetail = (eBuffDebuffStartReleaseType)base.ActionDetail2;
            if (actionDetail == eBuffDebuffStartReleaseType.BREAK)
            {
                return;
            }
            UnitCtrl.BuffParamKind changeParamKind = GetChangeParamKind(ActionDetail1);
            Dictionary<BasePartsData, LongWithEx> dictionary = new Dictionary<BasePartsData, LongWithEx>();
            if (_target.Owner.IsPartsBoss && !AbnormalStateDefine.IS_EFFECT_BOSS_BODY_ONLY[changeParamKind])
            {
                for (int index = 0; index < _target.Owner.BossPartsListForBattle.Count; ++index)
                    dictionary.Add(_target.Owner.BossPartsListForBattle[index], CalculateBuffDebuffParam(_target.Owner.BossPartsListForBattle[index], _valueDictionary[eValueNumber.VALUE_2], (eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, ActionDetail1 % 10 == 1));
            }
            else
                dictionary.Add(_target.Owner.DummyPartsData, CalculateBuffDebuffParam(_target, _valueDictionary[eValueNumber.VALUE_2], (eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, ActionDetail1 % 10 == 1));
            bool despelable = _valueDictionary[eValueNumber.VALUE_7] != 2f;
            //_target.Owner.SetBuffParam(changeParamKind, dictionary, _valueDictionary[eValueNumber.VALUE_4], _skill.SkillId, _source, _despelable, EffectType, ActionDetail1 % 10 != 1, getIsAdditional(),action);
            _target.Owner.SetBuffParam(changeParamKind, changeParamKind, dictionary, (float)_valueDictionary[eValueNumber.VALUE_4], _skill.SkillId, _source, despelable, base.EffectType, base.ActionDetail1 % 10 != 1, getIsAdditional(), isShowIcon(), _skill.BonusId, EnvironmentId, TriggerCall, action);

        }

        public static readonly List<UnitCtrl.BuffParamKind> BUFF_DEBUFF_ENHANCE_TARGET_TYPE = new List<UnitCtrl.BuffParamKind>
        {
            UnitCtrl.BuffParamKind.ATK,
            UnitCtrl.BuffParamKind.DEF,
            UnitCtrl.BuffParamKind.MAGIC_STR,
            UnitCtrl.BuffParamKind.MAGIC_DEF,
            UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL,
            UnitCtrl.BuffParamKind.MAGIC_CRITICAL,
            UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE,
            UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE,
            UnitCtrl.BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE,
            UnitCtrl.BuffParamKind.RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT,
            UnitCtrl.BuffParamKind.RECEIVE_PHYSICAL_DAMAGE_PERCENT,
            UnitCtrl.BuffParamKind.RECEIVE_MAGIC_DAMAGE_PERCENT,
            UnitCtrl.BuffParamKind.PHYSICAL_DAMAGE_UP_PERCENT,
            UnitCtrl.BuffParamKind.MAGIC_DAMAGE_UP_PERCENT,
        };


        public override DoubleWithEx GetActionBaseValue(UnitCtrl _source, eValueNumber _type)
        {
            // 默认值
            DoubleWithEx value = 0.0;

            Value.TryGetValue(_type, out value);

            if (_type != eValueNumber.VALUE_2)
                return value;

            UnitCtrl.BuffParamKind paramKind = BuffDebuffAction.GetChangeParamKind(ActionDetail1);

            if (!BUFF_DEBUFF_ENHANCE_TARGET_TYPE.Contains(paramKind))
                return value;

            if (IsExEquipSkillAction)
                return value;

            long enhanceZero = 0;
            if (_source != null)
            {
                if (ActionDetail1 % 10 != 1)
                    enhanceZero = _source.BuffEnhanceZero;
                else
                    enhanceZero = _source.DebuffEnhanceZero;
            }

            double rate = enhanceZero / 10000.0 + 1.0;
            return value * rate;
        }


        public static double GetBuffDebuffValueForCurrentValuePercentage(
            BasePartsData target,
            double value,
            UnitCtrl.BuffParamKind kind)
        {
            if (target == null)
                return 0f;

            double ratio = value / 100f;
            double baseStat = 0f;

            var owner = target.Owner;

            switch (kind)
            {
                case UnitCtrl.BuffParamKind.ATK:
                    baseStat = target.GetAtkZero();
                    break;

                case UnitCtrl.BuffParamKind.DEF:
                    baseStat = target.GetDefZero();
                    break;

                case UnitCtrl.BuffParamKind.MAGIC_STR:
                    baseStat = target.GetMagicStrZero();
                    break;

                case UnitCtrl.BuffParamKind.MAGIC_DEF:
                    baseStat = target.GetMagicDefZero();
                    break;

                case UnitCtrl.BuffParamKind.DODGE:
                    if (owner != null)
                        baseStat = owner.DodgeZero;
                    break;

                case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL:
                    baseStat = target.GetPhysicalCriticalZero();
                    break;

                case UnitCtrl.BuffParamKind.MAGIC_CRITICAL:
                    baseStat = target.GetMagicCriticalZero();
                    break;

                case UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE:
                    if (owner != null)
                        baseStat = owner.EnergyRecoveryRateZero;
                    break;

                case UnitCtrl.BuffParamKind.LIFE_STEAL:
                    baseStat = target.GetLifeStealZero();
                    break;

                case UnitCtrl.BuffParamKind.MOVE_SPEED:
                    if (owner != null)
                        baseStat = owner.MoveSpeedZero;
                    break;

                case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE:
                    if (owner != null)
                        baseStat = owner.PhysicalCriticalDamageRateOrMin;
                    break;

                case UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE:
                    if (owner != null)
                        baseStat = owner.MagicCriticalDamageRateOrMin;
                    break;

                case UnitCtrl.BuffParamKind.ACCURACY:
                    if (owner != null)
                        baseStat = owner.AccuracyZero;
                    break;

                case UnitCtrl.BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE:
                    if (owner != null)
                        baseStat = owner.ReceiveCriticalDamageRateOrMin;
                    break;

                case UnitCtrl.BuffParamKind.PHYSICAL_DAMAGE_UP_PERCENT:
                    if (owner != null)
                        baseStat = owner.PhysicalDamageUpPercentOrMin;
                    break;

                case UnitCtrl.BuffParamKind.MAGIC_DAMAGE_UP_PERCENT:
                    if (owner != null)
                        baseStat = owner.MagicDamageUpPercentOrMin;
                    break;

                case UnitCtrl.BuffParamKind.MAX_HP:
                    if (owner != null)
                        baseStat = (double) owner.MaxHp;
                    break;

                default:
                    return 0f;
            }

            return ratio * baseStat;
        }

        public static LongWithEx CalculateBuffDebuffParam(
          BasePartsData _target,
          DoubleWithEx _value,
          eChangeParameterType _changeParamType,
          UnitCtrl.BuffParamKind _targetChangeParamKind,
          bool _isDebuf)
        {
            DoubleWithEx f = 0L;
            if (_targetChangeParamKind == UnitCtrl.BuffParamKind.MAX_HP)
                _isDebuf = true;
            switch (_changeParamType)
            {
                case eChangeParameterType.FIXED:
                    f = (DoubleWithEx)BattleUtil.DoubleToLongReverseTruncate(_value);
                    break;
                case eChangeParameterType.PERCENTAGE:
                    var num1 = _value / 100f;
                    switch (_targetChangeParamKind)
                    {
                        case UnitCtrl.BuffParamKind.ATK:
                            f = _target.GetStartAtk() * num1;
                            break;
                        case UnitCtrl.BuffParamKind.DEF:
                            f = _target.GetStartDef() * num1;
                            break;
                        case UnitCtrl.BuffParamKind.MAGIC_STR:
                            f = _target.GetStartMagicStr() * num1;
                            break;
                        case UnitCtrl.BuffParamKind.MAGIC_DEF:
                            f = _target.GetStartMagicDef() * num1;
                            break;
                        case UnitCtrl.BuffParamKind.DODGE:
                            f = _target.GetStartDodge() * num1;
                            break;
                        case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL:
                            f = _target.GetStartPhysicalCritical() * num1;
                            break;
                        case UnitCtrl.BuffParamKind.MAGIC_CRITICAL:
                            f = _target.GetStartMagicCritical() * num1;
                            break;
                        case UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE:
                            f = (int)_target.Owner.StartEnergyRecoveryRate * num1;
                            break;
                        case UnitCtrl.BuffParamKind.LIFE_STEAL:
                            f = _target.GetStartLifeSteal() * num1;
                            break;
                        case UnitCtrl.BuffParamKind.MOVE_SPEED:
                            f = _target.Owner.StartMoveSpeed * num1;
                            break;
                        case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE:
                            f = (int)_target.Owner.StartPhysicalCriticalDamageRate * num1;
                            break;
                        case UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE:
                            f = (int)_target.Owner.StartMagicCriticalDamageRate * num1;
                            break;
                        case UnitCtrl.BuffParamKind.ACCURACY:
                            f = (int)_target.Owner.StartAccuracy * num1;
                            break;
                        case UnitCtrl.BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE:
                            f = (double)(int)_target.Owner.StartReceiveCriticalDamageRate * num1;
                            break;
                        case UnitCtrl.BuffParamKind.RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT:
                            f = (double)(int)_target.Owner.StartPhysicalAndMagicReceiveDamagePercent * num1;
                            break;
                        case UnitCtrl.BuffParamKind.RECEIVE_PHYSICAL_DAMAGE_PERCENT:
                            f = (double)(int)_target.Owner.StartPhysicalReceiveDamagePercent * num1;
                            break;
                        case UnitCtrl.BuffParamKind.RECEIVE_MAGIC_DAMAGE_PERCENT:
                            f = (double)(int)_target.Owner.StartMagicReceiveDamagePercent * num1;
                            break;
                        case UnitCtrl.BuffParamKind.PHYSICAL_DAMAGE_UP_PERCENT:
                            f = (double)(int)_target.Owner.StartPhysicalDamageUpPercent * num1;
                            break;
                        case UnitCtrl.BuffParamKind.MAGIC_DAMAGE_UP_PERCENT:
                            f = (double)(int)_target.Owner.StartMagicDamageUpPercent * num1;
                            break;
                        case UnitCtrl.BuffParamKind.MAX_HP:
                            f = (long)_target.Owner.StartMaxHP * num1;
                            break;
                    }
                    break;
                case eChangeParameterType.CURRENT_VALUE_PERCENTAGE:
                    f = GetBuffDebuffValueForCurrentValuePercentage(_target, _value, _targetChangeParamKind);
                    break;
            }

            // f = f.Select(x => Mathf.CeilToInt(Mathf.Abs(x)));
            //if (_isDebuf)
            //    f *= -1f;
            var num2 = f.Abs().Ceil();
            if (_isDebuf)
            {
                num2 = (num2 * (1f - (float)_target.GetDebuffResistPercent(_targetChangeParamKind) / 100f)).Floor();
                num2 *= -1;
            }
            return (LongWithEx)num2;
        }

        public static UnitCtrl.BuffParamKind GetChangeParamKind(int value)
        {
            if (value == 1)
                return UnitCtrl.BuffParamKind.MAX_HP;

            value = value % 1000 / 10;
            return (UnitCtrl.BuffParamKind)value;
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_2] = MasterData.action_value_2 + MasterData.action_value_3 * _level;
            Value[eValueNumber.VALUE_4] = MasterData.action_value_4 + MasterData.action_value_5 * _level;
        }

        public enum eChangeParameterType
        {
            FIXED = 1,
            PERCENTAGE = 2,
            CURRENT_VALUE_PERCENTAGE = 3,
        }

        public enum eBuffDebuffStartReleaseType
        {
            NORMAL = 1,
            BREAK = 2,
        }
    }
}
