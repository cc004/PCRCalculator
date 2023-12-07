using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class ActionByHitCountAction : ActionParameter
    {

        public enum ConditionType
        {
            unknown = 0,
            damage = 1,
            target = 2,
            hit = 3,
            critical = 4
        }

        public ConditionType conditionType;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            conditionType = (ConditionType)(actionDetail1);
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string limitation;
            if (actionValue5.value > 0)
            {
                limitation = Utils.JavaFormat(Utils.GetString("max_s_times"), Utils.roundIfNeed(actionValue5.value));
            }
            else
            {
                limitation = "";
            }
            switch (conditionType)
            {
                case ConditionType.hit:
                    return Utils.JavaFormat(Utils.GetString("Use_d1_s2_every_s3_hits_in_next_s4_sec"),
                        actionDetail2 % 10,
                        limitation,
                        Utils.roundIfNeed(actionValue1.value),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
                    );
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
