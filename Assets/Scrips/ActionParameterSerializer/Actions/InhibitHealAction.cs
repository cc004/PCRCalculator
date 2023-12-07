using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class InhibitHealAction : ActionParameter
    {
        public enum InhibitType
        {
            inhibit = 0,
            decrease = 1
        }

        public InhibitType inhibitType;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            base.childInit();
            actionValues.Add(new ActionValue(actionValue1, actionValue4, null));
            durationValues.Add(new ActionValue(actionValue2, actionValue3, null));
            inhibitType = (InhibitType)(actionDetail1);
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (inhibitType)
            {
                case InhibitType.inhibit:
                    return Utils.JavaFormat(Utils.GetString("When_s1_receive_healing_deal_s2_healing_amount_damage_instead_last_for_s3_sec_or_unlimited_time_if_triggered_by_field"),
                        targetParameter.buildTargetClause(),
                        actionValue1.valueString(),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                case InhibitType.decrease:
                    return Utils.JavaFormat(Utils.GetString("Decreases_s1_healing_received_by_s2_last_for_s3_sec_or_unlimited_time_if_triggered_by_field"),
                        Utils.roundIfNeed(actionValue1.value * 100),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
