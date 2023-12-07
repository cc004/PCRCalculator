namespace ActionParameterSerializer.Actions
{
    public class DamageChargeAction : ActionParameter
    {
        public
            override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Charge_for_s1_sec_and_deal_s2_damage_taken_additional_damage_on_the_next_effect"),
                actionValue3.valueString(), buildExpression(level, RoundingMode.UNNECESSARY, property));
        }
    }
}
