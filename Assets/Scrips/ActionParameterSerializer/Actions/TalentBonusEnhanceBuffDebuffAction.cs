namespace ActionParameterSerializer.Actions
{
    public class TalentBonusEnhanceBuffDebuffAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(
                Utils.GetString("talent_bonus_enhance_s1_by_s2_percent_in_s3_time"),
                targetParameter.buildTargetClause(),
                actionValue1.value,
                actionValue2.value);
        }
    }
}
