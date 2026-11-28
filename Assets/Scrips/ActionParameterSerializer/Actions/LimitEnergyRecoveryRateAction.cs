using System;
using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class LimitEnergyRecoveryRateAction : ActionParameter
    {
        private readonly List<ActionValue> durationValues = new();

        public override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            string limit = UserSettings.expression == UserSettings.EXPRESSION_VALUE
                ? Utils.roundIfNeed(Math.Max(0, actionValue1.value + actionValue2.value * level))
                : Utils.JavaFormat("max(0, %s)", buildExpression(level, RoundingMode.UNNECESSARY, property));
            return Utils.JavaFormat(Utils.GetString("Limit_s1_energy_recovery_rate_to_s2_for_s3_sec"),
                targetParameter.buildTargetClause(), limit, buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
