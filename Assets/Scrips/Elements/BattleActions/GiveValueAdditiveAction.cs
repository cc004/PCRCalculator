// Decompiled with JetBrains decompiler
// Type: Elements.GiveValueAdditiveAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class GiveValueAdditiveAction : GiveValueAction
    {

        protected override FloatWithEx calcDamageValue(
            UnitCtrl _source,
            Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => ((float)_source.MaxHp - _source.Hp) * _valueDictionary[eValueNumber.VALUE_2];

        protected override FloatWithEx calcHpValue(
            UnitCtrl _source,
            Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => _source.Hp * _valueDictionary[eValueNumber.VALUE_2];

        protected override void setValue(
      Dictionary<eValueNumber, FloatWithEx> _value,
      ActionParameter _action)
        {
            base.setValue(_value, _action);
            _action.AdditionalValue = _value;
        }
        protected override void createValue(
  UnitCtrl _source,
  Skill _skill,
  Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
  Dictionary<eValueNumber, FloatWithEx> _addValue,
  eValueNumber _evalue,
  BasePartsData _target)
        {
            base.createValue(_source, _skill, _valueDictionary, _addValue, _evalue, _target);
            //XX: ignore influence by clamp of add value
            if (MasterData.action_value_4 != 0.0 || MasterData.action_value_5 != 0.0)
                _addValue[_evalue] = _addValue[_evalue].Min(Value[eValueNumber.VALUE_4]);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_2] = (float)(MasterData.action_value_2 + MasterData.action_value_3 * _level);
            Value[eValueNumber.VALUE_4] = (float)(MasterData.action_value_4 + MasterData.action_value_5 * _level);

        }
        private void createPositiveValue(Dictionary<eValueNumber, float> _addValue, eValueNumber _evalue)
        {
            if ((double)base.MasterData.action_value_4 != 0.0 || (double)base.MasterData.action_value_5 != 0.0)
            {
                _addValue[_evalue] = Mathf.Min(_addValue[_evalue], base.Value[eValueNumber.VALUE_4]);
            }
            _addValue[_evalue] = Mathf.Max(_addValue[_evalue], 0f);
        }

        private void createNegativeValue(Dictionary<eValueNumber, float> _addValue, eValueNumber _evalue)
        {
            if ((double)base.MasterData.action_value_4 != 0.0 || (double)base.MasterData.action_value_5 != 0.0)
            {
                _addValue[_evalue] = Mathf.Max(_addValue[_evalue], base.Value[eValueNumber.VALUE_4]);
            }
            _addValue[_evalue] = Mathf.Min(_addValue[_evalue], 0f);
        }
    }
}
