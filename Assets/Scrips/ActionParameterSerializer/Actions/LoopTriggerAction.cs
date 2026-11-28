using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class LoopTriggerAction : ActionParameter
    {

        public enum TriggerType
        {
            unknown = 0,
            dodge = 1,
            damaged = 2,
            hp = 3,
            dead = 4,
            criticalDamaged = 5,
            getCriticalDamagedWithSummon = 6,
            CROWD_CONTROL = 14
        }

        public TriggerType triggerType;

        public
            override void childInit()
        {
            base.childInit();
            triggerType = (TriggerType)(actionDetail1);
        }

        public
            override string localizedDetail(int level, Property property)
        {
            var actionRefs = new List<string>();
            if (actionDetail2 != 0)
            {
                actionRefs.Add(Utils.JavaFormat(Utils.GetString("Action_d"), describeActionSlot(actionDetail2)));
            }
            if (actionDetail3 != 0)
            {
                actionRefs.Add(Utils.JavaFormat(Utils.GetString("Action_d"), describeActionSlot(actionDetail3)));
            }
            if (actionRefs.Count == 0)
                return base.localizedDetail(level, property);

            var actionList = string.Join("、", actionRefs);

            switch (triggerType)
            {
                case TriggerType.damaged:
                    return Utils.JavaFormat(Utils.GetString("Loop_trigger_on_attack_damage"),
                        actionList, actionValue4.valueString());
                case TriggerType.CROWD_CONTROL:
                    return Utils.JavaFormat(Utils.GetString("Loop_trigger_on_crowd_control"),
                        actionList, actionValue4.valueString());
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
