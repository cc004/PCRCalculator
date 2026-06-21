// Decompiled with JetBrains decompiler
// Type: Elements.GiveValueAdditiveAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class GiveValueAdditiveAction : GiveValueAction
    {

        protected override DoubleWithEx calcDamageValue(
            UnitCtrl _source,
            ValueDictionary _valueDictionary) => (DoubleWithEx)(_source.MaxHp - _source.Hp) * _valueDictionary[eValueNumber.VALUE_2];

        protected override DoubleWithEx calcHpValue(
            UnitCtrl _source,
            ValueDictionary _valueDictionary) => (DoubleWithEx)_source.Hp * _valueDictionary[eValueNumber.VALUE_2];

        protected override DoubleWithEx calcMaxHpValue(
            UnitCtrl _source,
            ValueDictionary _valueDictionary) => _source.MaxHp * _valueDictionary[eValueNumber.VALUE_2];


        protected override void setValue(
      ValueDictionary _value,
      ActionParameter _action)
        {
            base.setValue(_value, _action);
            _action.AdditionalValue = _value;
        }
        protected override void createValue(
  UnitCtrl _source,
  Skill _skill,
  ValueDictionary _valueDictionary,
  ValueDictionary _addValue,
  eValueNumber _evalue,
  BasePartsData _target)
        {
            base.createValue(_source, _skill, _valueDictionary, _addValue, _evalue, _target);
            if (MasterData.action_value_2 >= 0)
                createPositiveValue(_addValue, _evalue);
            else
                createNegativeValue(_addValue, _evalue);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_2] = MasterData.action_value_2 + MasterData.action_value_3 * _level;
            Value[eValueNumber.VALUE_4] = MasterData.action_value_4 + MasterData.action_value_5 * _level;

        }
        private void createPositiveValue(ValueDictionary _addValue, eValueNumber _evalue)
        {
            if (MasterData.action_value_4 != 0.0 || MasterData.action_value_5 != 0.0)
            {
                _addValue[_evalue] = _addValue[_evalue].Min(Value[eValueNumber.VALUE_4]);
            }
            _addValue[_evalue] = _addValue[_evalue].Max(0);
        }

        private void createNegativeValue(ValueDictionary _addValue, eValueNumber _evalue)
        {
            if (MasterData.action_value_4 != 0.0 || MasterData.action_value_5 != 0.0)
            {
                _addValue[_evalue] = _addValue[_evalue].Max(Value[eValueNumber.VALUE_4]);
            }
            _addValue[_evalue] = _addValue[_evalue].Min(0);
        }
    }
}
