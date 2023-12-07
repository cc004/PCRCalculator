using System;

namespace ActionParameterSerializer.Actions
{
    public class DivideAction : ActionParameter
    {
        public
            override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            String result = base.localizedDetail(level, property);
            if(actionValue1.value == 0)
                result = Utils.JavaFormat(Utils.GetString("Modifier_multiple_s1_HP_max_HP_to_value_d2_of_effect_d3"),
                    buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                    actionDetail2, actionDetail1 % 10);
            else if(actionValue1.value == 1)
                result = Utils.JavaFormat(Utils.GetString("Modifier_multiple_s1_lost_HP_max_HP_to_value_d2_of_effect_d3"),
                    buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                    actionDetail2, actionDetail1 % 10);
            else if(actionValue1.value == 2)
                result = Utils.JavaFormat(Utils.GetString("Modifier_multiple_s1_count_of_defeated_enemies_to_value_d2_of_effect_d3"),
                    buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                    actionDetail2, actionDetail1 % 10);
            else if(actionValue1.value == 4)
                result = Utils.JavaFormat(Utils.GetString("Modifier_multiple_s1_count_of_targets_to_value_d2_of_effect_d3"),
                    buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                    actionDetail2, actionDetail1 % 10);
            else if(actionValue1.value >= 200 && actionValue1.value < 300)
                result = Utils.JavaFormat(Utils.GetString("Modifier_multiple_s1_stacks_of_mark_ID_d2_to_value_d3_of_effect_d4"),
                    buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                    ((int)actionValue1.value) % 200, actionDetail2, actionDetail1 % 10);
            return result;
        }
    }
}
