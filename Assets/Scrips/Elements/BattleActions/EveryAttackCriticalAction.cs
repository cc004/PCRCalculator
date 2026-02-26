using System;
using System.Collections.Generic;
using UnityEngine;
using PCRCaculator;
using Elements.Battle;

namespace Elements
{
    public class EveryAttackCriticalAction : ActionParameter 
    {
        public enum eEveryAtkCriticalType
        {
            PHYSICAL = 1,
            MAGICAL = 2,
            BOTH = 3
        }

        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController) 
        { 
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        public override void ReadyAction(UnitCtrl _source, UnitActionController _sourceActionController, Skill _skill) 
        { 
            // empty
        }

        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> callBack = null) 
        { 
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary, callBack);

            if (_target == null)
                return;

            var owner = _target.Owner;
            if (owner == null)
                return;

            var data = new EveryAttackCriticalData();
            if (_valueDictionary != null && _valueDictionary.TryGetValue(eValueNumber.VALUE_1, out DoubleWithEx timer))
            {
                data.Timer = (float)timer;
            }
            data.IconType = (eStateIconType)ActionDetail2;
            data.Enable = true;

            owner.EnchantEveryAttackCritical((eEveryAtkCriticalType)ActionDetail1, data);
        }

        public override void SetLevel(float _level) 
        { 
            base.SetLevel(_level);

            Value[eValueNumber.VALUE_1] = MasterData.action_value_1 + MasterData.action_value_2 * _level;
        }
    }

}