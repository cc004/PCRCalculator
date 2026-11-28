using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class ChangeSpeedOverwriteFieldAction : ActionParameter
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
            base.childInit();
            speedChangeType = (SpeedChangeType)(actionDetail1);
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            if (speedChangeType != SpeedChangeType.haste && speedChangeType != SpeedChangeType.slow)
                return base.localizedDetail(level, property);

            string side = Utils.GetString(speedChangeType == SpeedChangeType.haste ? "friendly" : "enemy");
            return Utils.JavaFormat(Utils.GetString("Speed_field_sets_s1_speed_to_s2_percent"),
                side, describePercent(level, actionValues, property), actionValue5.valueString(),
                targetParameter.buildTargetClause(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
