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
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            triggerType = (TriggerType)(actionDetail1);
        }

        public
            override string localizedDetail(int level, Property property)
        {
            var use_action = new List<int>();
            if (actionDetail2 != 0)
            {
                use_action.Add(actionDetail2 % 10);
            }
            if (actionDetail3 != 0)
            {
                use_action.Add(actionDetail3 % 10);
            }
            var use_action_str = string.Join("、", use_action);

            switch (triggerType)
            {
                case TriggerType.damaged:
                    return Utils.JavaFormat(Utils.GetString("Condition_s1_chance_use_d2_when_takes_damage_within_s3_sec"),
                        buildExpression(level, property), use_action_str, actionValue4.valueString());
                case TriggerType.CROWD_CONTROL:
                    return Utils.JavaFormat(Utils.GetString("Condition_s1_chance_use_d2_when_takes_crown_within_s3_sec"),
                        buildExpression(level, property), use_action_str, actionValue4.valueString());
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
