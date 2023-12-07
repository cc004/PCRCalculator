namespace ActionParameterSerializer.Actions
{
    public class UpperLimitAttackAction : ActionParameter
    {
        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("s_Damage_is_reduced_on_low_level_players"),
                base.localizedDetail(level, property));
        }
    }
}
