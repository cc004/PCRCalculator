namespace ActionParameterSerializer.Actions
{
    public class HpLowerLimitAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(
                Utils.GetString("Keep_s1_HP_at_or_above_d2_for_s3_sec"),
                targetParameter.buildTargetClause(),
                actionDetail1,
                actionValue1.value);
        }
    }
}
