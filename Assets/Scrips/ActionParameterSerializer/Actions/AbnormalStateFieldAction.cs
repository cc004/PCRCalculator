using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class AbnormalStateFieldAction : ActionParameter
    {

        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            base.childInit();
            durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_on_s2_to_cast_effect_d3_for_s4_sec"),
                (int)actionValue3.value,
                targetParameter.buildTargetClause(),
                actionDetail1 % 10,
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
