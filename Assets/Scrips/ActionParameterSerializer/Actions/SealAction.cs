namespace ActionParameterSerializer.Actions
{
    public class SealAction : ActionParameter
    {
        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            if (actionValue4.value >= 0)
            {
                string detail = Utils.JavaFormat(Utils.GetString("Add_s1_mark_stacks_max_s2_ID_s3_on_s4_for_s5_sec"),
                    Utils.roundDownDouble(actionValue4.value),
                    Utils.roundDownDouble(actionValue1.value),
                    Utils.roundDownDouble(actionValue2.value),
                    targetParameter.buildTargetClause(),
                    actionValue3.valueString());
                if (actionDetail3 == 1)
                    detail += Utils.JavaFormat(Utils.GetString("Remove_mark_stack_every_s_damage_events"), actionValue5.valueString());
                return detail;
            }
            else
            {
                return Utils.JavaFormat(Utils.GetString("Remove_s1_mark_stacks_ID_s2_on_s3"),
                    Utils.roundDownDouble(-actionValue4.value),
                    Utils.roundDownDouble(actionValue2.value),
                    targetParameter.buildTargetClause());
            }
        }
    }
}
