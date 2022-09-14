// Decompiled with JetBrains decompiler
// Type: Elements.GiveValueMultipleAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
    public class GiveValueMultipleAction : GiveValueAction
    {
        protected override void setValue(
            Dictionary<eValueNumber, FloatWithEx> _value,
          ActionParameter _action)
        {
            base.setValue(_value, _action);
            _action.MultipleValue = _value;
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_2] = (float)(MasterData.action_value_2 + MasterData.action_value_3 * _level);
        }
        /*protected override float calcDamageValue(UnitCtrl _source, Dictionary<eValueNumber, float> _valueDictionary)
        {
            return (float)((long)_source.MaxHp - (long)_source.Hp) / (float)(long)_source.MaxHp * _valueDictionary[eValueNumber.VALUE_2];
        }

        protected override float calcHpValue(UnitCtrl _source, Dictionary<eValueNumber, float> _valueDictionary)
        {
            return (float)(long)_source.Hp / (float)(long)_source.MaxHp * _valueDictionary[eValueNumber.VALUE_2];
        }*/

    }
}
