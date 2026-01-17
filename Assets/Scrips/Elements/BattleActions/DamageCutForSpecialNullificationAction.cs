using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class DamageCutForSpecialNullificationAction : ActionParameter 
    {
        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController) 
        { 
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> callBack = null) 
        { 
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary, callBack);
            AppendIsAlreadyExeced(_target.Owner, _num);
            if (_target.Owner.DamageCutForSpecialNullificationData != null)
            {
                _target.Owner.DamageCutForSpecialNullificationData.Disable();
            }
            _target.Owner.DamageCutForSpecialNullificationData = new DamageCutForSpecialNullificationData(
                (DamageCutForSpecialNullificationData.eDamageCutType)ActionDetail1,
                _valueDictionary[eValueNumber.VALUE_1] / 100.0,
                _valueDictionary[eValueNumber.VALUE_3],
                _target.Owner,
                (eStateIconType)ActionDetail2,
                (DamageCutForSpecialNullificationData.eReleaseType)BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_5]),
                ActionEffectList,
                _skill
            );
        }

        public override void SetLevel(float _level) 
        { 
            base.SetLevel(_level);

            double v1 = MasterData.action_value_1;
            double v2 = MasterData.action_value_2;
            float value1 = (float)(v1 + v2 * _level);
            Value[eValueNumber.VALUE_1] = value1;

            double v3 = MasterData.action_value_3;
            double v4 = MasterData.action_value_4;
            float value2 = (float)(v3 + v4 * _level);
            Value[eValueNumber.VALUE_3] = value2;
        }
    }
}