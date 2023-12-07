namespace ActionParameterSerializer.Actions
{
    public class CountBlindAction : ActionParameter
    {

        public enum CountType
        {
            unknown = -1,
            time = 1,
            count = 2
        }

        public CountType countType;

        public
            override void childInit()
        {
            countType = (CountType)((int)actionValue1.value);
            actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (countType)
            {
                case CountType.time:
                    return Utils.JavaFormat(Utils.GetString("In_nex_s1_sec_s2_physical_attacks_will_miss"),
                        buildExpression(level, RoundingMode.UNNECESSARY, property), targetParameter.buildTargetClause());
                case CountType.count:
                    return Utils.JavaFormat(Utils.GetString("In_next_s1_attacks_s2_physical_attacks_will_miss"),
                        buildExpression(level, property), targetParameter.buildTargetClause());
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
