namespace ActionParameterSerializer.Actions
{
    public class EnchantTriggerAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            if (actionDetail1 != 1 && actionDetail1 != 2)
                return base.localizedDetail(level, property);

            string condition = Utils.GetString(actionDetail1 == 1 ? "when_receives_buff" : "when_deals_critical");
            string detail = Utils.JavaFormat(Utils.GetString("Enchant_s1_to_trigger_d2_on_s3"),
                targetParameter.buildTargetClause(), describeActionSlot(actionDetail2), condition);
            if (actionValue3.value != 0)
                detail += Utils.JavaFormat(Utils.GetString("Trigger_limit_s_times"), actionValue3.valueString());

            return detail;
        }
    }
}
