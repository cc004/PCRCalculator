namespace ActionParameterSerializer.Actions
{
    public class SpecialIdleAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Set_s1_special_idle_d2"), targetParameter.buildTargetClause(), actionDetail1);
        }
    }
}
