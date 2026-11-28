namespace ActionParameterSerializer.Actions
{
    public class ChangeResistIdAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            if (actionDetail1 != 1 && actionDetail1 != 2)
                return base.localizedDetail(level, property);

            string resistType = Utils.GetString(actionDetail1 == 1 ? "abnormal_resist_table" : "debuff_resist_table");
            return Utils.JavaFormat(Utils.GetString("Set_s1_resist_table_s2_to_d3_for_s4_sec"),
                targetParameter.buildTargetClause(), resistType, actionDetail2, actionValue1.valueString());
        }
    }
}
