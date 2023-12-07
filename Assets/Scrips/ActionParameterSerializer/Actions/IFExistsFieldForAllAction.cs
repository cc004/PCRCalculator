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
                return Utils.JavaFormat(Utils.GetString("Condition_if_the_specific_field_exists_then_use_d1_otherwise_d2"),
                    actionDetail2 % 10, actionDetail3 % 10);
            }
            else if (actionDetail2 != 0)
            {
                return Utils.JavaFormat(Utils.GetString("Condition_if_the_specific_field_exists_then_use_d"),
                    actionDetail2 % 10);
            }
            else
            {
                return base.localizedDetail(level, property);
            }
        }
    }
}
