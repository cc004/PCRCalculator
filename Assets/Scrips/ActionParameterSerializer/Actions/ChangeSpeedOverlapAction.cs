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
            if (speedChangeType != SpeedChangeType.haste && speedChangeType != SpeedChangeType.slow)
                return base.localizedDetail(level, property);

            string key = speedChangeType == SpeedChangeType.haste ? "Increase_s1_ATK_SPD_by_s2_for_s3_sec"
                : "Decrease_s1_ATK_SPD_by_s2_for_s3_sec";
            return Utils.JavaFormat(Utils.GetString(key), targetParameter.buildTargetClause(),
                describePercent(level, actionValues, property), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
