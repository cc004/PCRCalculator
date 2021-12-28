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
            PartsData partsData = _source.BossPartsListForBattle.Find((Predicate<PartsData>)(e => e.Index == _skill.ParameterTarget));
            if (this.ActionDetail2 != 2)
                return;
            Dictionary<BasePartsData, FloatWithEx> buffParam = new Dictionary<BasePartsData, FloatWithEx>();
            bool isDebuff = this.ActionDetail1 % 10 == 1;
            UnitCtrl.BuffParamKind buffParamKind = BuffDebuffAction.GetChangeParamKind(this.ActionDetail1);
            partsData.OnBreak += (Action)(() =>
           {
               if (_source.IsPartsBoss)
               {
                   for (int index = 0; index < _source.BossPartsListForBattle.Count; ++index)
                       buffParam.Add((BasePartsData)_source.BossPartsListForBattle[index], BuffDebuffAction.CalculateBuffDebuffParam((BasePartsData)_source.BossPartsListForBattle[index], this.Value[eValueNumber.VALUE_2], (BuffDebuffAction.eChangeParameterType)(float)this.Value[eValueNumber.VALUE_1], buffParamKind, isDebuff));
               }
               _source.SetBuffParam(UnitCtrl.BuffParamKind.NUM, buffParam, 0.0f, 0, (UnitCtrl)null, true, eEffectType.COMMON, !isDebuff, this.getIsAdditional());
               _source.EnableBuffParam(buffParamKind, buffParam, true, _source, !isDebuff, this.getIsAdditional(),90);
           });
            partsData.OnBreakEnd += (Action)(() =>
           {
               _source.EnableBuffParam(buffParamKind, buffParam, false, _source, !isDebuff, this.getIsAdditional(),90);
               buffParam.Clear();
           });
        }

        private bool getIsAdditional() => this.ActionDetail1 / 1000 == 1;

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          System.Action<string> action = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            this.AppendIsAlreadyExeced(_target.Owner, _num);
            if (this.ActionDetail2 == 2)
                return;
            UnitCtrl.BuffParamKind changeParamKind = BuffDebuffAction.GetChangeParamKind(this.ActionDetail1);
            Dictionary<BasePartsData, FloatWithEx> dictionary = new Dictionary<BasePartsData, FloatWithEx>();
            if (_target.Owner.IsPartsBoss && changeParamKind != UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE && (changeParamKind != UnitCtrl.BuffParamKind.MOVE_SPEED && changeParamKind != UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE) && (changeParamKind != UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE && changeParamKind != UnitCtrl.BuffParamKind.MAX_HP))
            {
                for (int index = 0; index < _target.Owner.BossPartsListForBattle.Count; ++index)
                    dictionary.Add((BasePartsData)_target.Owner.BossPartsListForBattle[index], BuffDebuffAction.CalculateBuffDebuffParam((BasePartsData)_target.Owner.BossPartsListForBattle[index], _valueDictionary[eValueNumber.VALUE_2], (BuffDebuffAction.eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, this.ActionDetail1 % 10 == 1));
            }
            else
                dictionary.Add(_target.Owner.DummyPartsData, BuffDebuffAction.CalculateBuffDebuffParam(_target, _valueDictionary[eValueNumber.VALUE_2], (BuffDebuffAction.eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, this.ActionDetail1 % 10 == 1));
            bool _despelable = (double)_valueDictionary[eValueNumber.VALUE_7] != 2.0;
            _target.Owner.SetBuffParam(changeParamKind, dictionary, _valueDictionary[eValueNumber.VALUE_4], _skill.SkillId, _source, _despelable, this.EffectType, this.ActionDetail1 % 10 != 1, this.getIsAdditional(),action);
        }

        public static FloatWithEx CalculateBuffDebuffParam(
          BasePartsData _target,
          FloatWithEx _value,
          BuffDebuffAction.eChangeParameterType _changeParamType,
          UnitCtrl.BuffParamKind _targetChangeParamKind,
          bool _isDebuf)
        {
            FloatWithEx f = 0.0f;
            if (_targetChangeParamKind == UnitCtrl.BuffParamKind.MAX_HP)
                _isDebuf = true;
            switch (_changeParamType)
            {
                case BuffDebuffAction.eChangeParameterType.FIXED:
                    f = BattleUtil.FloatToIntReverseTruncate(_value);
                    break;
                case BuffDebuffAction.eChangeParameterType.PERCENTAGE:
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
                        case UnitCtrl.BuffParamKind.MAX_HP:
                            f = (long)_target.Owner.StartMaxHP * num1;
                            break;
                    }
                    break;
            }
            f.value = Mathf.CeilToInt(Mathf.Abs(f.value));
            f.ex = Mathf.CeilToInt(Mathf.Abs(f.ex));
            if (_isDebuf)
                f = f * -1f;
            return f;
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
            this.Value[eValueNumber.VALUE_2] = (float)((double)this.MasterData.action_value_2 + (double)this.MasterData.action_value_3 * (double)_level);
            this.Value[eValueNumber.VALUE_4] = (float)((double)this.MasterData.action_value_4 + (double)this.MasterData.action_value_5 * (double)_level);
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
