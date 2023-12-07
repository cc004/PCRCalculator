namespace ActionParameterSerializer.Actions
{
    public class ChargeEnergyByDamageAction : ActionParameter
    {
        public
            override void childInit()
        {
            base.childInit();
            actionValues.Add(new ActionValue(actionValue1.value, actionValue2.value, eActionValue.VALUE1, eActionValue.VALUE2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (actionDetail1)
            {
                case 1:
                    return Utils.JavaFormat(Utils.GetString("Adds_s1_marks_to_s5_max_s2_id_d3_lasts_for_s4_sec_removes_1_mark_while_taking_dmg_and_restores_s6_tp"),
                        actionValue3.valueString(),
                        actionValue4.valueString(),
                        actionDetail2,
                        actionValue5.valueString(),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, actionValues, null, property)
                    );
            }
            return base.localizedDetail(level, property);
        }
    }
}
