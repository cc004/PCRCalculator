namespace ActionParameterSerializer.Actions
{
    public class EffectAction : ActionParameter
    {
        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Implement_some_visual_effects_to_s1"), targetParameter.buildTargetClause());
        }
    }
}
