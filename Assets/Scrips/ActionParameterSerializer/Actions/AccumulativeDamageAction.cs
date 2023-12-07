using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class AccumulativeDamageAction : ActionParameter
    {

        protected readonly List<ActionValue> stackValues = new();

        public
            override void childInit()
        {
            base.childInit();
            actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
            stackValues.Add(new ActionValue(actionValue4, actionValue5, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Add_additional_s1_damage_per_attack_with_max_s2_stacks_to_current_target"),
                buildExpression(level, property), buildExpression(level, stackValues, RoundingMode.FLOOR, property));
        }
    }
}
