using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class LogBarrierAction : ActionParameter
    {

        public enum BarrierType
        {
            physics = 1,
            magic = 2,
            all = 3
        }

        public BarrierType barrierType;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            base.childInit();
            barrierType = (BarrierType)((int)actionValue1.value);
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_reduce_damage_over_s2_with_coefficient_s3_the_greater_the_less_reduced_amount_for_s4_s"),
                targetParameter.buildTargetClause(),
                Utils.roundDouble(actionValue5.value),
                buildExpression(level, RoundingMode.UNNECESSARY, property),
                buildExpression(level, durationValues, null, property));
        }
    }
}
