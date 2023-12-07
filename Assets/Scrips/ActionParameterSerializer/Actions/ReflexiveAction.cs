namespace ActionParameterSerializer.Actions
{
    public class ReflexiveAction : ActionParameter
    {

        public enum ReflexiveType
        {
            unknown = 0,
            normal = 1,
            search = 2,
            position = 3
        }

        private ReflexiveType reflexiveType;

        public
            override void childInit()
        {
            reflexiveType = (ReflexiveType)(actionDetail1);
        }

        public
            override string localizedDetail(int level, Property property)
        {
            if (targetParameter.targetType == TargetType.absolute)
            {
                return Utils.JavaFormat(Utils.GetString("Change_the_perspective_to_s1_d2"), targetParameter.buildTargetClause(), (int)actionValue1.value);
            }
            else if (reflexiveType == ReflexiveType.search)
            {
                return Utils.JavaFormat(Utils.GetString("Scout_and_change_the_perspective_on_s"), targetParameter.buildTargetClause());
            }
            else
            {
                return Utils.JavaFormat(Utils.GetString("Change_the_perspective_on_s"), targetParameter.buildTargetClause());
            }
        }
    }
}