// Decompiled with JetBrains decompiler
// Type: Elements.GiveValueDivideAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class GiveValueDivideAction : GiveValueAction
  {
    protected override void setValue(
      Dictionary<eValueNumber, FloatWithEx> _value,
      ActionParameter _action)
    {
      base.setValue(_value, _action);
      _action.DivideValue = _value;
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_2] = (float) ((double) this.MasterData.action_value_2 + (double) this.MasterData.action_value_3 * (double) _level);
    }

    protected override float calcDamageValue(
      UnitCtrl _source,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => (float) ((long) _source.MaxHp - (long) _source.Hp) / (float) (long) _source.MaxHp * _valueDictionary[eValueNumber.VALUE_2];

    protected override float calcHpValue(
      UnitCtrl _source,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => (float) (long) _source.Hp / (float) (long) _source.MaxHp * _valueDictionary[eValueNumber.VALUE_2];
  }
}
