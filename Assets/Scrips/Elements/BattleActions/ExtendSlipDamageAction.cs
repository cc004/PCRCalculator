using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ExtendSlipDamageAction : ActionParameter 
    {
        public int EnvironmentId { get; set; } = -1;

        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController) 
        { 
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> callBack = null)
        {
            if (_target.Owner.ExtendSlipDamageData != null)
            {
                _target.Owner.ExtendSlipDamageData.Enable = false;
            }
            _target.Owner.ExtendSlipDamageData = new ExtendSlipDamageData
            {
                Enable = true,
                Target = _target.Owner,
                ExtendValue = ((float)_valueDictionary[eValueNumber.VALUE_1] + 100.0f) / 100.0f,
                EnvironmentId = this.EnvironmentId,
                Duration = _valueDictionary[eValueNumber.VALUE_2]
            };
            _target.Owner.ExtendSlipDamageData.Initialize();
        }

        public override void SetLevel(float _level) 
        { 
            base.SetLevel(_level);
        }
    }
}
