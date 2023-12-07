using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class HealFieldAction : ActionParameter
    {

        private ClassModifier healClass;
        private PercentModifier percentModifier;
        private FieldType fieldType;
        private readonly List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            base.childInit();
            healClass = actionDetail1 % 2 == 0 ? ClassModifier.magical : ClassModifier.physical;
            percentModifier = actionDetail2 == 2 ? PercentModifier.percent : PercentModifier.number;
            if (actionDetail1 <= 2)
            {
                fieldType = FieldType.normal;
            }
            else
            {
                fieldType = FieldType.repeat;
            }

            switch (healClass)
            {
                case ClassModifier.magical:
                    actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
                    actionValues.Add(new ActionValue(actionValue3, actionValue4, PropertyKey.magicStr));
                    break;
                case ClassModifier.physical:
                    actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
                    actionValues.Add(new ActionValue(actionValue3, actionValue4, PropertyKey.atk));
                    break;
            }
            durationValues.Add(new ActionValue(actionValue5, actionValue6, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (fieldType)
            {
                case FieldType.repeat:
                    if (targetParameter.targetType == TargetType.absolute)
                    {
                        return Utils.JavaFormat(Utils.GetString("Summon_a_healing_field_of_radius_d1_to_heal_s2_s3_s4_HP_per_second_for_5s_sec"),
                            (int)actionValue7.value,
                            targetParameter.buildTargetClause(),
                            buildExpression(level, property),
                            percentModifier.description(),
                            buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                    }
                    else
                    {
                        return Utils.JavaFormat(Utils.GetString("Summon_a_healing_field_of_radius_d1_at_position_of_s2_to_heal_s3_s4_HP_per_second_for_s5_sec"),
                            (int)actionValue7.value,
                            targetParameter.buildTargetClause(),
                            buildExpression(level, property),
                            percentModifier.description(),
                            buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                    }
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }

    public enum FieldType
    {
        normal,
        repeat
    }
}