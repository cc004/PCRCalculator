using System;
using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class ChangeSpeedOverlapAction : ActionParameter
    {

        protected enum SpeedChangeType
        {
            slow = 1,
            haste = 2
        }

        private SpeedChangeType speedChangeType;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            speedChangeType = (SpeedChangeType)(actionDetail1);
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            try
            {
                if (speedChangeType == SpeedChangeType.slow)
                {
                    return Utils.JavaFormat(Utils.GetString("Decrease_s1_ATK_SPD_by_s2_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        ((double.Parse(buildExpression(level, RoundingMode.UNNECESSARY, property))) * ((100.0))).ToString(),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                }
                else if (speedChangeType == SpeedChangeType.haste)
                {
                    return Utils.JavaFormat(Utils.GetString("Increase_s1_ATK_SPD_by_s2_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        ((double.Parse(buildExpression(level, RoundingMode.UNNECESSARY, property))) * ((100.0))).ToString(),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                }
                else
                {
                    return base.localizedDetail(level, property);
                }
            }
            catch (Exception)
            {
                return base.localizedDetail(level, property);
            }
        }
    }
}
