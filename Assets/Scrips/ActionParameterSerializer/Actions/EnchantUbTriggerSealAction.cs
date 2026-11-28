namespace ActionParameterSerializer.Actions
{
    public class EnchantUbTriggerSealAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(
                Utils.GetString("Set_s1_mark_d2_count_s3_and_trigger_on_UB"),
                targetParameter.buildTargetClause(),
                actionValue1.valueString(),
                actionValue3.valueString(),
                actionValue4.valueString(),
                describeActionSlot(actionDetail1));
        }
    }
}
