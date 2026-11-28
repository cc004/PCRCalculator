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
            allBreak = 11,
            DAMAGED_2 = 12,
            DAMAGED_HP = 13,
            DAMAGED_FOR_EXEC_TO_SOURCE = 14
        }

        private TriggerType triggerType;

        public
            override void childInit()
        {
            triggerType = (TriggerType)(actionDetail1);
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string chance = buildExpression(level, RoundingMode.UNNECESSARY, property);
            switch (triggerType)
            {
                case TriggerType.hp:
                    return Utils.JavaFormat(Utils.GetString("Trigger_HP_is_at_most_s1_with_s2_chance"), actionValue3.valueString(), chance);
                case TriggerType.limitTime:
                    return Utils.JavaFormat(Utils.GetString("Trigger_idle_time_is_at_most_s1_with_s2_chance"), actionValue3.valueString(), chance);
                case TriggerType.damage:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_damaged"), chance);
                case TriggerType.dead:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_dead"), chance);
                case TriggerType.critical:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_critical_damaged"), chance);
                case TriggerType.stealthFree:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_stealth"), chance);
                case TriggerType.Break:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d1_on_break_and_last_for_s2_sec"), chance, actionValue3.value);
                case TriggerType.dot:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_dot_damaged"), chance);
                case TriggerType.allBreak:
                    return Utils.JavaFormat(Utils.GetString("Trigger_d_on_all_targets_break"),
                        chance);
                case TriggerType.DAMAGED_HP:
                    return Utils.JavaFormat(Utils.GetString("Trigger_on_HP_loss_s"), actionValue3.valueString());
                case TriggerType.DAMAGED_FOR_EXEC_TO_SOURCE:
                    return Utils.GetString("Trigger_on_attack_hit_and_target_attacker");
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
