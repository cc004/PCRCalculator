using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class EnergyDamageReduceAction : ActionParameter
    {
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            durationValues.Add(new ActionValue(actionValue2, actionValue3, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Reduces_incoming_energy_damage_down_to_s1_percent_of_s2_for_s3_sec"),
                actionValue1.valueString(),
                targetParameter.buildTargetClause(),
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
