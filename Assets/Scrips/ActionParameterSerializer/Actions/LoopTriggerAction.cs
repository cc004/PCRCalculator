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
            getCriticalDamagedWithSummon = 6
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
            switch (triggerType)
            {
                case TriggerType.damaged:
                    return Utils.JavaFormat(Utils.GetString("Condition_s1_chance_use_d2_when_takes_damage_within_s3_sec"),
                        buildExpression(level, property), actionDetail2 % 10, actionValue4.valueString());
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
