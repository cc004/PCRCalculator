using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ChangeCriticalReferenceAction : ActionParameter 
    {
        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController) 
        { 
            base.ExecActionOnStart(_skill, _source, _sourceActionController);

            if (_skill == null)
                return;

            var attackAction = _skill.ActionParameters?.Find(ap => 
                ap is AttackActionBase && ap.ActionId == this.ActionDetail1
            ) as AttackActionBase;

            if (attackAction == null)
                return;

            attackAction.CriticalReference = (AttackActionBase.eCriticalReference)this.ActionDetail2;

            if (this.Value != null && this.Value.TryGetValue(0, out DoubleWithEx val))
            {
                long intVal = double.IsPositiveInfinity(val) ? long.MinValue : (long)val;
                attackAction.AdditionalCritical = intVal;
            }
        }

        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, DoubleWithEx> _valueDictionary, Action<string> callBack = null) 
        { 
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
        }

        public override void SetLevel(float _level) 
        { 
            base.SetLevel(_level);
        }

    }

}