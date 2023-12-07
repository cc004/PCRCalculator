namespace ActionParameterSerializer.Actions
{
    public class CountDownAction : ActionParameter
    {
        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Set_a_countdown_timer_on_s1_trigger_effect_d2_after_s3_sec"),
                targetParameter.buildTargetClause(), actionDetail1 % 10, actionValue1.valueString());
        }
    }
}