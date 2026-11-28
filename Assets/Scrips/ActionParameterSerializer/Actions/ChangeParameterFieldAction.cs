namespace ActionParameterSerializer.Actions
{
    public class ChangeParameterFieldAction : AuraAction
    {

        protected override int ChangeParameterType => actionDetail2;

        public
            override void childInit()
        {
            base.childInit();
            actionValues.Clear();
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Clear();
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            int talentId = (int)actionValue7.value;
            string side = Utils.GetString(actionDetail1 % 10 == 1 ? "enemy" : "friendly");
            if (talentId != 0)
            {
                side = Utils.JavaFormat(Utils.GetString("Field_s1_targets_with_s2_talent"),
                    side, ((PCRCaculator.Battle.Talent)talentId).description());
            }

            string duration = buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property);
            string center = targetParameter.targetType == TargetType.absolute
                ? Utils.JavaFormat(Utils.GetString("Field_with_radius_s"), actionValue5.valueString(), duration)
                : Utils.JavaFormat(Utils.GetString("Field_at_s1_with_radius_s2"), targetParameter.buildTargetClause(),
                    actionValue5.valueString(), duration);
            return center + Utils.JavaFormat(Utils.GetString("Field_changes_s1_s2_s3"),
                auraActionType.description(), side, describeValueReference(buildExpression(level, RoundingMode.UP, property)),
                percentModifier.description(), auraType.description());
        }
    }
}
