namespace ActionParameterSerializer.Actions
{
    public class TauntAction : ActionParameter
    {
        public override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            if (actionDetail1 != 1)
                return base.localizedDetail(level, property);

            return Utils.JavaFormat(Utils.GetString("Taunt_s1_to_target_source_for_s2_sec"),
                targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.UNNECESSARY, property));
        }
    }
}
