
using System;
using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;

namespace Elements
{
  public class SlipDamageUpAction : ActionParameter
  {
    private const int APPLY_ALL_SLIP_VALUE = -1;

    // RVA: 0x2198C44 Offset: 0x2198C44 VA: 0x2198C44 Slot: 6
    public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
    {
        base.ExecActionOnStart(_skill, _source, _sourceActionController);
    }

    // RVA: 0x2198C4C Offset: 0x2198C4C VA: 0x2198C4C Slot: 8
    public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, DoubleWithEx> _valueDictionary)
    { 
        base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
        
        if (_target == null)
            return;
            
        AppendIsAlreadyExeced(_target.Owner, _num);
        
        SlipDamageUpData slipDamageUpData = new SlipDamageUpData();
        slipDamageUpData.Value = _valueDictionary[eValueNumber.VALUE_1] / 100.0;
        slipDamageUpData.Timer = (float)_valueDictionary[eValueNumber.VALUE_2];
        slipDamageUpData.LimitDamage = BattleUtil.DoubleToLong(_valueDictionary[eValueNumber.VALUE_7]);
        slipDamageUpData.Enable = true;
        slipDamageUpData.DisplayIcon = ActionDetail1 == 0;
        slipDamageUpData.StartTimer(_target.Owner);
        
        double value3 = _valueDictionary[eValueNumber.VALUE_3];
        if (value3 != double.PositiveInfinity && (int)value3 == APPLY_ALL_SLIP_VALUE)
        {
            _target.Owner?.EnchantAllSlipDamageUp(slipDamageUpData);
            return;
        }
        
        List<eSlipDamageType> slipDamageTypes = new List<eSlipDamageType>();
        
        // Add slip damage types based on action values
        if (value3 != double.PositiveInfinity)
            slipDamageTypes.Add((eSlipDamageType)(int)value3);
            
        double value4 = _valueDictionary[eValueNumber.VALUE_4];
        if (value4 != double.PositiveInfinity)
            slipDamageTypes.Add((eSlipDamageType)(int)value4);
            
        double value5 = _valueDictionary[eValueNumber.VALUE_5];
        if (value5 != double.PositiveInfinity)
            slipDamageTypes.Add((eSlipDamageType)(int)value5);
            
        double value6 = _valueDictionary[eValueNumber.VALUE_6];
        if (value6 != double.PositiveInfinity)
            slipDamageTypes.Add((eSlipDamageType)(int)value6);
        
        // Apply slip damage up for each specified type
        foreach (SlipDamageAction.eSlipDamageType slipDamageType in slipDamageTypes)
        {
            UnitCtrl.eAbnormalState abnormalState = SlipDamageAction.abnormalStateDic[slipDamageType];
            _target.Owner?.EnchantSlipDamageUp(slipDamageUpData, abnormalState);
        }
    }

    // RVA: 0x2199150 Offset: 0x2199150 VA: 0x2199150 Slot: 9
    public override void SetLevel(float _level)
    { 
      base.SetLevel(_level);
    }
  }
}
