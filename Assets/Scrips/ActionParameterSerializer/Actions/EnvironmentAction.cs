using static ActionParameterSerializer.Actions.EveryAttackCriticalAction;

namespace ActionParameterSerializer.Actions
{
    public class EnvironmentAction : ActionParameter
    {
        public enum EnvironmentType
        {
            thundering = 137
        }

        public static string describeEnvironment(int id)
        {
            return id == (int)EnvironmentType.thundering
                ? EnvironmentType.thundering.description()
                : Utils.JavaFormat(Utils.GetString("Environment_ID_d"), id);
        }

        public override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Summon_field_of_s1_environment_for_s2_sec"),
                describeEnvironment(actionDetail2), buildExpression(level, RoundingMode.UNNECESSARY, property))
                + Utils.GetString("Environment_ends_when_source_leaves");
        }
    }
}