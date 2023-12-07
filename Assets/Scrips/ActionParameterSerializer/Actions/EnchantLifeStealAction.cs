using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class EnchantLifeStealAction : ActionParameter
    {

        private readonly List<ActionValue> stackValues = new();

        public
            override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            stackValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Add_additional_s1_s2_to_s3_for_next_s4_attacks"),
                buildExpression(level, property), PropertyKey.lifeSteal.description(), targetParameter.buildTargetClause(), buildExpression(level, stackValues, RoundingMode.FLOOR, property));
        }
    }
}
