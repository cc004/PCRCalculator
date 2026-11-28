using System.Collections.Generic;
using System.Text;

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

            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            var attackKey = actionDetail1 == 5 ? PropertyKey.sourceAttack
                : damageClass == ClassModifier.magical ? PropertyKey.magicStr : PropertyKey.atk;
            actionValues.Add(new ActionValue(actionValue3, actionValue4, attackKey));
            durationValues.Add(new ActionValue(actionValue5, actionValue6, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            StringBuilder str = new StringBuilder();
            string damageType = actionDetail1 == 5 ? "" : damageClass.description();
            switch (fieldType)
            {
                case FieldType.repeat:
                    if (targetParameter.targetType == TargetType.absolute)
                    {
                        str.Append(Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_to_deal_s2_s3_damage_per_second_for_s4_sec_to_s5"),
                            (int)actionValue7.value,
                            buildExpression(level, property),
                            damageType,
                            buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property),
                            targetParameter.buildTargetClause()));
                    }
                    else
                    {
                        str.Append(Utils.JavaFormat(Utils.GetString("Summon_a_field_of_radius_d1_at_position_of_s2_to_deal_s3_s4_damage_per_second_for_s5_sec"),
                            (int)actionValue7.value,
                            targetParameter.buildTargetClause(),
                            buildExpression(level, property),
                            damageType,
                            buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)));
                    }
                    break;
                default:
                    str.Append(base.localizedDetail(level, property));
                    break;
            }
            if (actionDetail1 == 5)
                str.Append(Utils.GetString("Attack_field_uses_lower_defense"));

            if (actionDetail3 != 0)
            {
                str.Append(Utils.JavaFormat(Utils.GetString("recover_tp_by_damage_divided_by_s1"), 100 - actionDetail3));
            }
            return str.ToString();
        }
    }
}
