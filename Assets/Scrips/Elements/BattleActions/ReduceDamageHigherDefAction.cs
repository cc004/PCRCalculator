using System;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ReduceDamageHigherDefAction : ActionParameter 
    {
        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController) 
        { 
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> callBack = null) 
        { 
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary, callBack);

            AppendIsAlreadyExeced(_target.Owner, _num);

            if (_target.Owner.ReduceDamageByHigherDefData != null)
            {
                _target.Owner.ReduceDamageByHigherDefData.Disable();
            }
            _target.Owner.OnChangeState?.Invoke(_target.Owner, eStateIconType.REDUCE_DAMAGE_BY_HIGHER_DEF, true);
            _target.Owner.ReduceDamageByHigherDefData = new ReduceDamageByHigherDefData()
            {
                Enable = true,
                Timer = _valueDictionary[eValueNumber.VALUE_1],
                Target = _target.Owner
            };
            _target.Owner.ReduceDamageByHigherDefData.StartTimer();
        }

        public override void SetLevel(float _level) 
        { 
            base.SetLevel(_level);
        }
    }
}