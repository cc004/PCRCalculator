namespace ActionParameterSerializer.Actions
{
    public class UpperLimitAttackAction : ActionParameter
    {
        public
            override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            var key = actionDetail1 == (int)ClassModifier.magical ? PropertyKey.magicStr : PropertyKey.atk;
            actionValues.Add(new ActionValue(actionValue3, actionValue4, key));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string damage = Utils.JavaFormat(Utils.GetString("Base_s1_s2_damage_to_s3"), buildExpression(level, property),
                ((ClassModifier)actionDetail1).description(), targetParameter.buildTargetClause());
            return damage + Utils.JavaFormat(Utils.GetString("Damage_base_is_reduced_in_two_steps"),
                actionDetail2, actionDetail3, actionValue5.valueString(), actionValue6.valueString(), actionValue7.valueString());
        }
    }
}
