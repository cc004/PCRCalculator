using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    /// <summary>
    /// action_type 40 KETSUBAN（血判）。
    /// 当前 jp 主数据里没有任何 skill_action 使用该类型（eActionType 里原本就标注「忽略」），
    /// 参考工程 pcr_skill_action 也没有给出可核对的分支实现，因此这里只保证「不抛异常」：
    /// 记录一次告警后按无效动作处理。等实际出现数据时再按 ACTION_TYPE_MAP 的 40 号分支补齐。
    /// </summary>
    public class KetsubanAction : ActionParameter
    {
        private static bool warned;

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
                Debug.LogWarning($"[KetsubanAction] action {ActionId} 使用了 KETSUBAN(40)，该类型目前按空动作处理（当前主数据无使用时仍可正常战斗）。");
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
        }
    }
}
