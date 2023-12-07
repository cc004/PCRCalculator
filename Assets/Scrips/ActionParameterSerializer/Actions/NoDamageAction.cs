namespace ActionParameterSerializer.Actions
{
    public class NoDamageAction : ActionParameter
    {

        public enum NoDamageType
        {
            unknown = 0,
            noDamage = 1,
            dodgePhysics = 2,
            dodgeAll = 3,
            abnormal = 4,
            debuff = 5,
            Break = 6
        }

        private NoDamageType noDamageType;

        public
            override void childInit()
        {
            noDamageType = (NoDamageType)(actionDetail1);
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (noDamageType)
            {
                case NoDamageType.noDamage:
                    return Utils.JavaFormat(Utils.GetString("Make_s1_to_be_invulnerable_for_s2_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, RoundingMode.UNNECESSARY, property));
                case NoDamageType.dodgePhysics:
                    return Utils.JavaFormat(Utils.GetString("Make_s1_to_be_invulnerable_to_physical_damage_for_s2_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, RoundingMode.UNNECESSARY, property));
                case NoDamageType.Break:
                    return Utils.JavaFormat(Utils.GetString("Make_s1_to_be_invulnerable_to_break_for_s2_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, RoundingMode.UNNECESSARY, property));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
