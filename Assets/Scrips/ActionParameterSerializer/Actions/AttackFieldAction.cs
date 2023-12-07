using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class AttackFieldAction : ActionParameter
    {

        private ClassModifier damageClass;
        private FieldType fieldType;
        private readonly List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            base.childInit();
            damageClass = actionDetail1 % 2 == 0 ? ClassModifier.magical : ClassModifier.physical;
            if (actionDetail1 <= 2)
            {
                fieldType = FieldType.normal;
            }
            else
            {
                fieldType = FieldType.repeat;
            }

            switch (damageClass)
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
                        return Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_to_deal_s2_s3_damage_per_second_for_s4_sec_to_s5"),
                            (int)actionValue7.value,
                            buildExpression(level, property),
                            damageClass.description(),
                            buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property),
                            targetParameter.buildTargetClause());
                    }
                    else
                    {
                        return Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_at_position_of_s2_to_deal_s3_s4_damage_per_second_for_s5_sec"),
                            (int)actionValue7.value,
                            targetParameter.buildTargetClause(),
                            buildExpression(level, property),
                            damageClass.description(),
                            buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                    }
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
