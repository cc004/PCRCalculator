using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

namespace Elements
{
  public class LimitEnergyRecoveryRateAction : ActionParameter // TypeDefIndex: 20399
  {
    // Fields
    private const int DISABLE_ICON_VALUE = 1;

    // Methods

    // RVA: 0x2807854 Offset: 0x2807854 VA: 0x2807854 Slot: 6
    public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
    }

    // RVA: 0x280785C Offset: 0x280785C VA: 0x280785C Slot: 8
    public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);

      if (_target?.Owner == null || _valueDictionary == null)
        return;

      AppendIsAlreadyExeced(_target.Owner, _num);

      bool disableIcon = _valueDictionary[eValueNumber.VALUE_4] == 1.0;

      var limitData = _target.Owner.LimitEnergyRecoveryRateData;

      if (limitData == null)
      {
        limitData = new LimitEnergyRecoveryRateData();
        limitData.Value = Math.Max(0.0, _valueDictionary[eValueNumber.VALUE_1]);
        limitData.Timer = (float)_valueDictionary[eValueNumber.VALUE_3];
        limitData.Enable = true;
        limitData.DisableIcon = disableIcon;
        _target.Owner.LimitEnergyRecoveryRateData = limitData;

        if (!disableIcon)
          _target.Owner.NoSkipOnChangeAbnormalState?.Invoke(_target.Owner, (eStateIconType)150, true, limitData.Timer, limitData.Value.ToString());
        limitData.StartTimer(_target.Owner);
      }
      else
      {
        limitData.Value = Math.Max(0.0, _valueDictionary[eValueNumber.VALUE_1]);
        limitData.Timer = (float)_valueDictionary[eValueNumber.VALUE_3];

        if (!limitData.Enable)
        {
          limitData.DisableIcon = disableIcon;
          if (!disableIcon)
            _target.Owner.NoSkipOnChangeAbnormalState(_target.Owner, (eStateIconType)150, true, limitData.Timer, limitData.Value.ToString());
          limitData.Enable = true;
          limitData.StartTimer(_target.Owner);
        }
        else
        {
          if (disableIcon)
          {
            if (!limitData.DisableIcon)
              _target.Owner.button?.SetAbnormalIcons(_target.Owner, (eStateIconType)150, false);
          }
          else
          {
            if (limitData.DisableIcon)
              _target.Owner.NoSkipOnChangeAbnormalState(_target.Owner, (eStateIconType)150, true, limitData.Timer, limitData.Value.ToString());
          }
          limitData.DisableIcon = disableIcon;
        }
      }
    }

    // RVA: 0x2807B68 Offset: 0x2807B68 VA: 0x2807B68 Slot: 9
    public override void SetLevel(float _level)
    {
      Value[eValueNumber.VALUE_1] = MasterData.action_value_1 + MasterData.action_value_2 * _level;
      Value[eValueNumber.VALUE_3] = MasterData.action_value_3 + MasterData.action_value_4 * _level;
    }
  }

}
