namespace ActionParameterSerializer.Actions
{
    public class LabyrinthAdditiveAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            if (actionDetail1 <= 0 || actionDetail2 < 1 || actionDetail2 > 7)
                return base.localizedDetail(level, property);

            return Utils.JavaFormat(Utils.GetString("Add_relic_series_d1_times_s2_to_action_d3_value_d4"),
                (int)actionValue1.value - 100, actionValue2.valueString(), describeActionSlot(actionDetail1), actionDetail2);
        }
    }
}
