namespace ActionParameterSerializer.Actions
{
    public class RatioDamageAction : ActionParameter
    {

        public enum HPtype
        {
            unknown = 0,
            max = 1,
            current = 2,
            originalMax = 3
        }

        public HPtype hptype;

        public
            override void childInit()
        {
            base.childInit();
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            hptype = (HPtype)(actionDetail1);
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string r = buildExpression(level, RoundingMode.UNNECESSARY, property);
            if (UserSettings.get().getExpression() != UserSettings.EXPRESSION_VALUE)
            {
                r = Utils.JavaFormat("(%s)", r);
            }
            switch (hptype)
            {
                case HPtype.max:
                    return Utils.JavaFormat(Utils.GetString("Deal_damage_equal_to_s1_of_target_max_HP_to_s2"),
                        r, targetParameter.buildTargetClause());
                case HPtype.current:
                    return Utils.JavaFormat(Utils.GetString("Deal_damage_equal_to_s1_of_target_current_HP_to_s2"),
                        r, targetParameter.buildTargetClause());
                case HPtype.originalMax:
                    return Utils.JavaFormat(Utils.GetString("Deal_damage_equal_to_s1_of_targets_original_max_HP_to_s2"),
                        r, targetParameter.buildTargetClause());
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
