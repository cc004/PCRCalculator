using System;
using System.Collections.Generic;

namespace Elements
{
    public class HpLowerLimitAction : ActionParameter
    {
        public override void ExecAction(
            UnitCtrl _source,
            BasePartsData _target,
            int _num,
            UnitActionController _sourceActionController,
            Skill _skill,
            float _starttime,
            Dictionary<int, bool> _enabledChildAction,
            ValueDictionary _valueDictionary,
            Action<string> callBack = null)
        {
            base.ExecAction(
                _source,
                _target,
                _num,
                _sourceActionController,
                _skill,
                _starttime,
                _enabledChildAction,
                _valueDictionary);

            _target.Owner.SetAbnormalState(
                _source,
                UnitCtrl.eAbnormalState.HP_LOWER_LIMIT,
                (float)_valueDictionary[eValueNumber.VALUE_1],
                this,
                _skill,
                ActionDetail1);

            callBack?.Invoke($"HP不会低于{ActionDetail1}，持续{_valueDictionary[eValueNumber.VALUE_1]}秒");
        }
    }
}
