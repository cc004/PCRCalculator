namespace ActionParameterSerializer.Actions
{
    public class LoopMotionRepeatAction : ActionParameter
    {

        public string successClause;
        public string failureClause;

        public
            override void childInit()
        {
            base.childInit();
            if (actionDetail2 != 0)
            {
                successClause = Utils.JavaFormat(Utils.GetString("use_d_after_time_up"), actionDetail2 % 10);
            }

            if (actionDetail3 != 0)
            {
                failureClause = Utils.JavaFormat(Utils.GetString("use_d_after_break"), actionDetail3 % 10);
            }
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string mainClause = Utils.JavaFormat(Utils.GetString("Repeat_effect_d1_every_s2_sec_up_to_s3_sec_break_if_taken_more_than_s4_damage"),
                actionDetail1 % 10, actionValue2.valueString(), actionValue1.valueString(), actionValue3.valueString());
            if (successClause != null && failureClause != null)
            {
                return mainClause + successClause + failureClause;
            }
            else if (successClause != null)
            {
                return mainClause + successClause;
            }
            else if (failureClause != null)
            {
                return mainClause + failureClause;
            }
            else
            {
                return mainClause;
            }
        }
    }
}
