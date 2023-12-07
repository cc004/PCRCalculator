namespace ActionParameterSerializer.Actions
{
    public class ChannelAction : AuraAction
    {

        public enum ReleaseType
        {
            damage = 1,
            unknown = 2
        }

        protected ReleaseType releaseType;

        public override void childInit()
        {
            base.childInit();
            releaseType = (ReleaseType)actionDetail2;
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (releaseType)
            {
                case ReleaseType.damage:
                    return Utils.JavaFormat(Utils.GetString("Channeling_for_s1_sec_disrupted_by_taking_damage_d2_times_s3_s4_s5_s6_s7"),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property),
                        actionDetail3,
                        auraActionType.description(),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, RoundingMode.UP, property),
                        percentModifier.description(),
                        auraType.description());
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
