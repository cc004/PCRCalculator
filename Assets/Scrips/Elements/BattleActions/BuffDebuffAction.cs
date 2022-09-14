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
            Dictionary<BasePartsData, FloatWithEx> buffParam = new Dictionary<BasePartsData, FloatWithEx>();
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

                _source.SetBuffParam(UnitCtrl.BuffParamKind.NUM, buffParamKind, buffParam, 0f, 0, null, _despelable: true, eEffectType.COMMON, !isDebuff, getIsAdditional(), isShowIcon());
                _source.EnableBuffParam(buffParamKind, buffParam, _enable: true, _source, !isDebuff, getIsAdditional());

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
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
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
            Dictionary<BasePartsData, FloatWithEx> dictionary = new Dictionary<BasePartsData, FloatWithEx>();
            if (_target.Owner.IsPartsBoss && changeParamKind != UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE && (changeParamKind != UnitCtrl.BuffParamKind.MOVE_SPEED && changeParamKind != UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE) && (changeParamKind != UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE && changeParamKind != UnitCtrl.BuffParamKind.MAX_HP))
            {
                for (int index = 0; index < _target.Owner.BossPartsListForBattle.Count; ++index)
                    dictionary.Add(_target.Owner.BossPartsListForBattle[index], CalculateBuffDebuffParam(_target.Owner.BossPartsListForBattle[index], _valueDictionary[eValueNumber.VALUE_2], (eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, ActionDetail1 % 10 == 1));
            }
            else
                dictionary.Add(_target.Owner.DummyPartsData, CalculateBuffDebuffParam(_target, _valueDictionary[eValueNumber.VALUE_2], (eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, ActionDetail1 % 10 == 1));
			bool despelable = _valueDictionary[eValueNumber.VALUE_7] != 2f;
            //_target.Owner.SetBuffParam(changeParamKind, dictionary, _valueDictionary[eValueNumber.VALUE_4], _skill.SkillId, _source, _despelable, EffectType, ActionDetail1 % 10 != 1, getIsAdditional(),action);
            _target.Owner.SetBuffParam(changeParamKind, changeParamKind, dictionary, _valueDictionary[eValueNumber.VALUE_4], _skill.SkillId, _source, despelable, base.EffectType, base.ActionDetail1 % 10 != 1, getIsAdditional(), isShowIcon(), _skill.BonusId,action);

        }

        public static FloatWithEx CalculateBuffDebuffParam(
          BasePartsData _target,
          FloatWithEx _value,
          eChangeParameterType _changeParamType,
          UnitCtrl.BuffParamKind _targetChangeParamKind,
          bool _isDebuf)
        {
            FloatWithEx f = 0.0f;
            if (_targetChangeParamKind == UnitCtrl.BuffParamKind.MAX_HP)
                _isDebuf = true;
            switch (_changeParamType)
            {
                case eChangeParameterType.FIXED:
                    f = BattleUtil.FloatToIntReverseTruncate(_value);
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
                            f = (float)(int)_target.Owner.StartReceiveCriticalDamageRate * num1;
                            break;
                        case UnitCtrl.BuffParamKind.RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT:
                            f = (float)(int)_target.Owner.StartPhysicalAndMagicReceiveDamagePercent * num1;
                            break;
                        case UnitCtrl.BuffParamKind.RECEIVE_PHYSICAL_DAMAGE_PERCENT:
                            f = (float)(int)_target.Owner.StartPhysicalReceiveDamagePercent * num1;
                            break;
                        case UnitCtrl.BuffParamKind.RECEIVE_MAGIC_DAMAGE_PERCENT:
                            f = (float)(int)_target.Owner.StartMagicReceiveDamagePercent * num1;
                            break;
                        case UnitCtrl.BuffParamKind.MAX_HP:
                            f = (long)_target.Owner.StartMaxHP * num1;
                            break;
                    }
                    break;
            }

            // f = f.Select(x => Mathf.CeilToInt(Mathf.Abs(x)));
            //if (_isDebuf)
            //    f *= -1f;
            int num2 = Mathf.CeilToInt(Mathf.Abs(f));
            if (_isDebuf)
            {
                num2 = BattleUtil.FloatToInt((float)num2 * (1f - (float)_target.GetDebuffResistPercent(_targetChangeParamKind) / 100f));
            }
            if (_isDebuf)
            {
                num2 *= -1;
            }
            return num2;
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
            Value[eValueNumber.VALUE_2] = (float)(MasterData.action_value_2 + MasterData.action_value_3 * _level);
            Value[eValueNumber.VALUE_4] = (float)(MasterData.action_value_4 + MasterData.action_value_5 * _level);
        }

        public enum eChangeParameterType
        {
            FIXED = 1,
            PERCENTAGE = 2,
        }

        public enum eBuffDebuffStartReleaseType
        {
            NORMAL = 1,
            BREAK = 2,
        }
    }
}
