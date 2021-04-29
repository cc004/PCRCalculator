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
    protected override void setValue(
      Dictionary<eValueNumber, float> _value,
      ActionParameter _action)
    {
      base.setValue(_value, _action);
      _action.AdditionalValue = _value;
    }
        protected override void createValue(
  UnitCtrl _source,
  Skill _skill,
  Dictionary<eValueNumber, float> _valueDictionary,
  Dictionary<eValueNumber, float> _addValue,
  eValueNumber _evalue,
  BasePartsData _target)
        {
            base.createValue(_source, _skill, _valueDictionary, _addValue, _evalue, _target);
            if ((double)this.MasterData.action_value_4 != 0.0 || (double)this.MasterData.action_value_5 != 0.0)
                _addValue[_evalue] = Mathf.Min(_addValue[_evalue], this.Value[eValueNumber.VALUE_4]);
            _addValue[_evalue] = Mathf.Max(_addValue[_evalue], 0.0f);
        }

        public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_2] = (float) ((double) this.MasterData.action_value_2 + (double) this.MasterData.action_value_3 * (double) _level);
            this.Value[eValueNumber.VALUE_4] = (float)((double)this.MasterData.action_value_4 + (double)this.MasterData.action_value_5 * (double)_level);

        }

        protected override float calcDamageValue(
      UnitCtrl _source,
      Dictionary<eValueNumber, float> _valueDictionary) => (float) ((long) _source.MaxHp - (long) _source.Hp) * _valueDictionary[eValueNumber.VALUE_2];

    protected override float calcHpValue(
      UnitCtrl _source,
      Dictionary<eValueNumber, float> _valueDictionary) => (float) (long) _source.Hp * _valueDictionary[eValueNumber.VALUE_2];
  }
}
