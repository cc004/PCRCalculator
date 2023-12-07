namespace ActionParameterSerializer.Actions
{
    public class DestroyAction : ActionParameter
    {
        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Kill_s_instantly"), targetParameter.buildTargetClause());
        }
    }
}
