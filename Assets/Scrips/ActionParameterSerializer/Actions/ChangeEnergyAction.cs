namespace ActionParameterSerializer.Actions
{
    public class ChangeEnergyAction : ActionParameter
    {

        public
            override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (actionDetail1)
            {
                case 1:
                    if (targetParameter.targetType == TargetType.self)
                    {
                        return Utils.JavaFormat(Utils.GetString("Restore_s1_s2_TP"), targetParameter.buildTargetClause(), buildExpression(level, null, RoundingMode.CEILING, property, false, true, false));
                    }
                    else
                    {
                        return Utils.JavaFormat(Utils.GetString("Restore_s1_s2_TP"), targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.CEILING, property));
                    }
                default:
                    return Utils.JavaFormat(Utils.GetString("Make_s1_lose_s2_TP"), targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.CEILING, property));
            }
        }
    }
}
