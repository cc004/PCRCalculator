using System;
using System.Collections.Generic;

namespace Elements
{
    public class GiveValueAsLabyrinthAdditiveAction : ActionParameter
    {
        public override void ExecAction(
            UnitCtrl _source,
            BasePartsData _target,
            int _num,
            UnitActionController _sourceActionController,
            Skill _skill,
            float _starttime,
            Dictionary<int, bool> _enabledChildAction,
            ValueDictionary _valueDictionary)
        {
            ApplyLabyrinthRelicMarkValue(_skill, _valueDictionary);
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
            Action<string> action = null)
        {
            int relicMarkId = ApplyLabyrinthRelicMarkValue(_skill, _valueDictionary);
            action?.Invoke($"遗物系列{relicMarkId}累计数追加到动作{ActionDetail1}。");
        }

        private int ApplyLabyrinthRelicMarkValue(Skill skill, ValueDictionary valueDictionary)
        {
            var childAction = skill?.ActionParameters?.Find(parameter => parameter.ActionId == ActionDetail1);
            if (childAction == null || ActionDetail2 < 1 || ActionDetail2 > 7)
                return 0;

            int relicMarkId = (int)valueDictionary[eValueNumber.VALUE_1] - 100;
            int markCount = GetLabyrinthRelicMarkCount(relicMarkId);
            var additionalValue = new ValueDictionary();
            additionalValue.Add((eValueNumber)(ActionDetail2 - 1), (DoubleWithEx)markCount * valueDictionary[eValueNumber.VALUE_2]);
            childAction.AdditionalValue = additionalValue;
            return relicMarkId;
        }

        private static int GetLabyrinthRelicMarkCount(int relicMarkId)
        {
            if (relicMarkId <= 0)
                return 0;

            var markCounts = MyGameCtrl.Instance?.tempData?.LabyrinthRelicMarkCounts;
            return markCounts != null && markCounts.TryGetValue(relicMarkId, out int markCount)
                ? markCount
                : 0;
        }
    }
}
