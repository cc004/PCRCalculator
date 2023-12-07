using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class ChangeEnergyFieldAction : ActionParameter
    {
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            base.childInit();
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (actionDetail1)
            {
                case 1:
                    return Utils.JavaFormat(Utils.GetString("Summon_a_field_with_radius_d1_at_position_s2_which_continuous_restore_tp_s3_of_units_within_the_field_for_s4_sec"),
                        (int)actionValue5.value,
                        targetParameter.buildTargetClause(),
                        buildExpression(level, actionValues, RoundingMode.CEILING, property, false, targetParameter.targetType == TargetType.self, false),
                        buildExpression(level, durationValues, null, property));
                case 2:
                    return Utils.JavaFormat(Utils.GetString("Summon_a_field_with_radius_d1_at_position_s2_which_continuous_lose_tp_s3_of_units_within_the_field_for_s4_sec"),
                        (int)actionValue5.value,
                        targetParameter.buildTargetClause(),
                        buildExpression(level, actionValues, RoundingMode.CEILING, property, false, targetParameter.targetType == TargetType.self, false),
                        buildExpression(level, durationValues, null, property));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
