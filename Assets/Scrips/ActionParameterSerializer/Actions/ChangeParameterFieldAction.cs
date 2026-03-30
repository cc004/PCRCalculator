using System.Text;

namespace ActionParameterSerializer.Actions
{
    public class ChangeParameterFieldAction : AuraAction
    {

        public
            override void childInit()
        {
            base.childInit();
            actionValues.Clear();
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Clear();
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
            base.percentModifier = actionDetail2 == 2 ? PercentModifier.percent : PercentModifier.number;
        }

        public
            override string localizedDetail(int level, Property property)
        {
            int talentId = (int)actionValue7.value;
            StringBuilder builder = new StringBuilder();
            if (targetParameter.targetType == TargetType.absolute)
            {
                builder.Append(Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_to_s2_s3_s4_s5_for_s6_sec"),
                    (int)actionValue5.value,
                    auraActionType.description(),
                    targetParameter.buildTargetClause(),
                    buildExpression(level, RoundingMode.UP, property),
                    auraType.description(),
                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property),
                    percentModifier.description()));
            }
            else
            {
                builder.Append(Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_at_position_of_s2_to_s3_s4_s5_for_s6_sec"),
                    (int)actionValue5.value,
                    targetParameter.buildTargetClause(),
                    auraActionType.description(),
                    buildExpression(level, RoundingMode.UP, property),
                    auraType.description(),
                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property),
                    percentModifier.description()));
            }
            if (talentId != 0)
            {
                builder.Append(" ");
                builder.Append(Utils.JavaFormat(Utils.GetString("effect_is_effect_by_s1_talent_unit"), ((PCRCaculator.Battle.Talent)talentId).GetDescription()));
            }
            return builder.ToString();
        }
    }
}
