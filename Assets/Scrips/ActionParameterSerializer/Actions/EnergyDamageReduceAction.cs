using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class EnergyDamageReduceAction : ActionParameter
    {
        private enum EnergyDamageReduceType
        {
            Normal = 0,
            Limit = 1
        }

        private EnergyDamageReduceType reduceType;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            reduceType = (EnergyDamageReduceType)actionDetail1;
            durationValues.Add(new ActionValue(actionValue2, actionValue3, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (reduceType)
            {
                case EnergyDamageReduceType.Normal:
                    return Utils.JavaFormat(Utils.GetString("Reduces_incoming_energy_damage_down_to_s1_percent_of_s2_for_s3_sec"),
                        actionValue1.valueString(),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                case EnergyDamageReduceType.Limit:
                    return Utils.JavaFormat(Utils.GetString("Limit_incoming_energy_damage_taken_by_s1_to_s2_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        actionValue1.valueString(),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
