using System;
using System.Collections.Generic;
using UnityEngine;
using PCRCaculator;

namespace Elements
{
    public class IfContainsUnitGroupAction : ActionParameter 
    {
        private List<int> targetActionIds { get; set; }
        private List<int> targetUnitIds { get; set; }

        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController) 
        { 
            base.ExecActionOnStart(_skill, _source, _sourceActionController);

            targetActionIds = new List<int>();
            targetUnitIds = new List<int>();

            if (this.ActionChildrenIndexes != null && _skill != null && _skill.ActionParameters != null)
            {
                foreach (var idx in this.ActionChildrenIndexes)
                {
                    if (idx >= 0 && idx < _skill.ActionParameters.Count)
                    {
                        var ap = _skill.ActionParameters[idx];
                        if (ap != null)
                            targetActionIds.Add(ap.ActionId);
                    }
                }
            }

            if (this.ActionDetail1 != 0)
            {
                var groupList = MainManager.Instance.GetListWithGroupId(this.ActionDetail1);
                if (groupList != null)
                {
                    foreach (var group in groupList)
                    {
                        targetUnitIds.Add(group.unit_id);
                    }
                }
            }

            if (this.ActionDetail2 != 0)
            {
                var groupList = MainManager.Instance.GetListWithGroupId(this.ActionDetail1);
                if (groupList != null)
                {
                    foreach (var group in groupList)
                    {
                        targetUnitIds.Add(group.unit_id);
                    }
                }
            }
        }

        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> callBack = null) 
        { 
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);

            if (_target == null || _target.Owner == null || targetUnitIds == null)
                return;

            if (!targetUnitIds.Contains(_target.Owner.OriginalUnitId))
            {
                if (targetActionIds != null && _enabledChildAction != null)
                {
                    foreach (var actionId in targetActionIds)
                    {
                        _enabledChildAction[actionId] = false;
                    }
                }
            }

        }

        public override void SetLevel(float _level) 
        { 
            base.SetLevel(_level);
        }
    }
}