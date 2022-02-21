// Decompiled with JetBrains decompiler
// Type: Elements.PassiveDamageUpAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class PassiveDamageUpAction : ActionParameter
  {
    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _starttime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      if (ActionDetail1 != 1)
        return;
      DebuffDamageUpData _debuffDamageUpData = new DebuffDamageUpData
      {
        DebuffDamageUpLimitValue = _valueDictionary[eValueNumber.VALUE_2],
        DebuffDamageUpValue = _valueDictionary[eValueNumber.VALUE_1],
        DebuffDamageUpTimer = _valueDictionary[eValueNumber.VALUE_3],
        EffectType = (DebuffDamageUpData.eEffectType) ActionDetail2
      };
      _target.Owner.AddDebuffDamageUpData(_debuffDamageUpData);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
    }

    public enum eCountType
    {
      DEBUFF = 1,
    }
  }
}
