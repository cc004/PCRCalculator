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
            return Utils.JavaFormat(Utils.GetString("Deploys_a_filed_of_radius_d1_which_s2_attack_speed_of_s3_for_s4_sec"),
                (int)actionValue5.value,
                speedChangeType.description(),
                Utils.roundDouble(double.Parse(buildExpression(level, RoundingMode.UNNECESSARY, property, true)) * 100),
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
