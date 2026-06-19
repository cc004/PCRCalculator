using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class TranscendDamageAction : ActionParameter
    {
        private readonly List<ActionValue> durationValues = new();

        public override void childInit()
        {
            base.childInit();
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue4, actionValue5, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            string rate = buildExpression(level, RoundingMode.UNNECESSARY, property);
            string duration = buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property);
            return Utils.JavaFormat(
                Utils.GetString("Make_s1_deal_additional_transcend_damage_up_to_s2_at_s3_percent_when_dealing_log_barrier_compressed_damage_for_s4_sec"),
                targetParameter.buildTargetClause(),
                actionValue3.valueString(),
                rate,
                duration);
        }
    }
}
