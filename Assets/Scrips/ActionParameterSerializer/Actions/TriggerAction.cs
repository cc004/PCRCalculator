using System;

namespace ActionParameterSerializer.Actions
{
    public class TriggerAction : ActionParameter
    {

        public enum TriggerType
        {
            unknown = 0,
            dodge = 1,
            damage = 2,
            hp = 3,
            dead = 4,
            critical = 5,
            criticalWithSummon = 6,
            limitTime = 7,
            stealthFree = 8,
            Break = 9,
            dot = 10,
            allBreak = 11
        }

        private TriggerType triggerType;

        public
            override void childInit()
        {
            triggerType = (TriggerType)(actionDetail1);
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (triggerType)
            {
                case TriggerType.hp:
                    return Utils.JavaFormat(Utils.GetString("Trigger_HP_is_below_d"), Math.Round(actionValue3.value));
                case TriggerType.limitTime:
                    return Utils.JavaFormat(Utils.GetString("Trigger_Left_time_is_below_s_sec"), Math.Round(actionValue3.value));
                case TriggerType.damage:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_damaged"), Math.Round(actionValue1.value));
                case TriggerType.dead:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_dead"), Math.Round(actionValue1.value));
                case TriggerType.critical:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_critical_damaged"), Math.Round(actionValue1.value));
                case TriggerType.stealthFree:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_stealth"), Math.Round(actionValue1.value));
                case TriggerType.Break:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d1_on_break_and_last_for_s2_sec"), Math.Round(actionValue1.value), actionValue3.value);
                case TriggerType.dot:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_dot_damaged"), Math.Round(actionValue1.value));
                case TriggerType.allBreak:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_all_targets_break"),
                        Math.Round(actionValue1.value));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
