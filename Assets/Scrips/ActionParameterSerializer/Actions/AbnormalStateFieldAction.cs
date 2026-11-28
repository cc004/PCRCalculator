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
            return Utils.JavaFormat(Utils.GetString("Abnormal_field_at_s2_with_radius_d1"),
                (int)actionValue3.value,
                targetParameter.buildTargetClause(),
                describeActionSlot(actionDetail1),
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
