using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    internal class HealDownAction : ActionParameter
    {

        public PercentModifier percentModifier;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            percentModifier = (int)actionValue1.value == 2 ? PercentModifier.percent : PercentModifier.number;
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Multiple_heal_effects_from_s1_with_s2_for_s3_sec"),
                targetParameter.buildTargetClause(),
                buildExpression(level, RoundingMode.UNNECESSARY, property),
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
