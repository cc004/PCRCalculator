using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    /// <summary>
    /// action_type 91 PASSIVE_INTERMITTENT（间歇型被动）。
    /// 当前 jp 主数据里没有使用该类型（eActionType 里原本标注「忽略」），
    /// 参考工程里也没有可核对的分支，因此先保证「不抛异常」：
    /// 首次出现时告警一次，其余按空动作处理。以后有数据时在这里补周期性触发逻辑。
    /// </summary>
    public class PassiveIntermittentAction : ActionParameter
    {
        private static bool warned;

        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);

            if (!warned)
            {
                warned = true;
                Debug.LogWarning($"[PassiveIntermittentAction] skill {(_skill != null ? _skill.SkillId : 0)} 使用了 PASSIVE_INTERMITTENT(91)，该类型目前按空动作处理。");
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
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
        }
    }
}
