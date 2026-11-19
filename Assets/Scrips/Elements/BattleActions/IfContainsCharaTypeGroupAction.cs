using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    /// <summary>
    /// action_type 131 IF_CONTAINS_CHARA_TYPE_GROUP。
    /// 游戏里用 reference_chara_type_group(group_id → chara_type) 去判断场上是否有指定角色类型组，
    /// 本项目没有把 chara_identity / reference_chara_type_group 载入 DatabaseCache
    /// （jpdb10071600 里 reference_chara_type_group 目前是空表，也没有任何 action 使用它），
    /// 因此这里做成「条件恒成立 + 只告警一次」，保证不会再抛「类型 131 未实现」，
    /// 将来把这两张表接进来后只要替换 IsConditionSatisfied 即可。
    /// </summary>
    public class IfContainsCharaTypeGroupAction : ActionParameter
    {
        private static bool warned;

        private List<int> targetActionIds { get; set; } = new List<int>();

        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);

            targetActionIds.Clear();
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
        }

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
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);

            if (!warned)
            {
                warned = true;
                Debug.LogWarning($"[IfContainsCharaTypeGroup] action {ActionId} 使用了角色类型组判断（group={ActionDetail1}），本项目尚未载入 reference_chara_type_group，按条件恒成立处理。");
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
        }
    }
}
