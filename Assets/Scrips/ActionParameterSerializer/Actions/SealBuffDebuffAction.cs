namespace ActionParameterSerializer.Actions
{
    public class SealBuffDebuffAction : AuraAction
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Change_s1_parameter_s2_per_mark_d3"),
                targetParameter.buildTargetClause(), auraType.description(), actionDetail2,
                auraActionType.description(), describeValueReference(buildExpression(level, RoundingMode.UNNECESSARY, property)),
                percentModifier.description(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
