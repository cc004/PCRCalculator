namespace ActionParameterSerializer.Actions
{
    public class IfContainsUnitGroupAction : ActionParameter
    {
        // 7.9.1: MasterExtraEffectUnitGroup
        private int group1, group2;
        public override void childInit()
        {
            base.childInit();
            group1 = actionDetail1;
            group2 = actionDetail2;
        }

        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Enabled_if_s1_in_group_d2_or_d3"),
                targetParameter.buildTargetClause(true),
                group1,
                group2);
        }
    }
}