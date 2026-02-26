using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ConvertDamageOverTimeAction : ActionParameter 
    {
        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController) 
        { 
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> callBack = null)
        {
            AppendIsAlreadyExeced(_target.Owner, _num);
            if (_target.Owner.ConvertDamageOverTimeData != null)
            {
                _target.Owner.ConvertDamageOverTimeData.Disable(true);
            }
            _target.Owner.ConvertDamageOverTimeData = new ConvertDamageOverTimeData
            {
                Enable = true,
                Target = _target.Owner,
                Source = _source,
                DamageCutValue = _valueDictionary[eValueNumber.VALUE_1] / 100.0,
                Timer = _valueDictionary[eValueNumber.VALUE_2],
                NumberOfDamage = BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_3]),
                OwnerAction = this,
            };
            _target.Owner.OnChangeState?.Invoke(_target.Owner, eStateIconType.CONVERT_DAMAGE_OVER_TIME, true);
            _target.Owner.ConvertDamageOverTimeData.StartTimer();
        }

        public override void SetLevel(float _level) 
        { 
            base.SetLevel(_level);
        }
    }
}