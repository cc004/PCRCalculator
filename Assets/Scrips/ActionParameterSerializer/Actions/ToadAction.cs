using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class ToadAction : ActionParameter
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
            return Utils.JavaFormat(Utils.GetString("Polymorph_s1_for_s2_sec"),
                targetParameter.buildTargetClause(),
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
