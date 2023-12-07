namespace ActionParameterSerializer.Actions
{
    public class HealAction : ActionParameter
    {

        private ClassModifier healClass;
        private PercentModifier percentModifier;

        public
            override void childInit()
        {
            healClass = (ClassModifier)(actionDetail1);
            percentModifier = (int)actionValue1.value == 2 ? PercentModifier.percent : PercentModifier.number;
            switch (healClass)
            {
                case ClassModifier.magical:
                    actionValues.Add(new ActionValue(actionValue4, actionValue5, PropertyKey.magicStr));
                    actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
                    break;
                case ClassModifier.physical:
                    actionValues.Add(new ActionValue(actionValue4, actionValue5, PropertyKey.atk));
                    actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
                    break;
                default:
                    return;
            }
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Restore_s1_s2_s3_HP"), targetParameter.buildTargetClause(), buildExpression(level, null, null, property, true, false, false), percentModifier.description());
        }
    }
}
