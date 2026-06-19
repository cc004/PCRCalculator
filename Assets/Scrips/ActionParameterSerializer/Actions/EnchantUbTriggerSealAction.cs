namespace ActionParameterSerializer.Actions
{
    public class EnchantUbTriggerSealAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(
                Utils.GetString("Add_s1_d2_mark_stacks_and_on_ub_remove_1_mark_stack_and_use_action_d3"),
                targetParameter.buildTargetClause(),
                Utils.roundDownDouble(actionValue3.value),
                actionDetail1 % 10);
        }
    }
}
