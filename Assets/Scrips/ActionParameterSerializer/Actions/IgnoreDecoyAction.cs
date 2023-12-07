namespace ActionParameterSerializer.Actions
{
    public class IgnoreDecoyAction : ActionParameter
    {
        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Ignore_the_other_units_taunt_when_attacking_s"), targetParameter.buildTargetClause());
        }
    }
}
