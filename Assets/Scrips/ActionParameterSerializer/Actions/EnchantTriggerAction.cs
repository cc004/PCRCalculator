namespace ActionParameterSerializer.Actions
{
    public class EnchantTriggerAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(
                Utils.GetString("Enchant_s1_to_trigger_action_d2"),
                targetParameter.buildTargetClause(),
                actionDetail2);
        }
    }
}
