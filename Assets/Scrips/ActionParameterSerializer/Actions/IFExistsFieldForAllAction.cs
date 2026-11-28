namespace ActionParameterSerializer.Actions
{
    public class IFExistsFieldForAllAction : ActionParameter
    {
        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            if (actionDetail2 != 0 && actionDetail3 != 0)
            {
                return Utils.JavaFormat(Utils.GetString("Condition_field_d1_exists_then_use_d2_otherwise_d3"),
                    actionDetail1, describeActionSlot(actionDetail2), describeActionSlot(actionDetail3));
            }
            else if (actionDetail2 != 0)
            {
                return Utils.JavaFormat(Utils.GetString("Condition_field_d1_exists_then_use_d2"),
                    actionDetail1, describeActionSlot(actionDetail2));
            }
            else if (actionDetail3 != 0)
            {
                return Utils.JavaFormat(Utils.GetString("Condition_field_d1_does_not_exist_then_use_d2"),
                    actionDetail1, describeActionSlot(actionDetail3));
            }
            else
            {
                return base.localizedDetail(level, property);
            }
        }
    }
}
