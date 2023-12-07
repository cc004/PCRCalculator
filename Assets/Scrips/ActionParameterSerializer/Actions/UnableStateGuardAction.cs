using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class UnableStateGuardAction : ActionParameter
    {

        protected List<ActionValue> durationValues = new();

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
            if (actionValue1.value == -1 && actionValue2.value == 0)
            {
                return Utils.JavaFormat(Utils.GetString("Enable_s1_to_resist_all_sorts_of_incapacity_efficacies_in_a_period_of_s2_sec"),
                    targetParameter.buildTargetClause(),
                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
                );
            }
        
            return Utils.JavaFormat(Utils.GetString("Enable_s1_to_resist_all_sorts_of_incapacity_efficacies_up_to_s2_times_in_a_period_of_s3_sec"),
                targetParameter.buildTargetClause(),
                buildExpression(level, property),
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
            );
        }
    }
}
