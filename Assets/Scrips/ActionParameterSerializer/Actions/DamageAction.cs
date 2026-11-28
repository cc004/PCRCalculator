using System;
using System.Text;

namespace ActionParameterSerializer.Actions
{
    public class DamageAction : ActionParameter
    {
        public enum DecideTargetAtkType
        {
            bySource = 0,
            lowerDef = 1,
        }

        public ClassModifier damageClass;
        public CriticalModifier criticalModifier;
        public DecideTargetAtkType decideTargetAtkType;

        public
            override void childInit()
        {
            damageClass = (ClassModifier)(actionDetail1);
            criticalModifier = (actionValue5.value < 0 || actionValue5.value > 0 && actionValue5.value == Math.Truncate(actionValue5.value)) ? CriticalModifier.critical : CriticalModifier.normal;
            decideTargetAtkType = (DecideTargetAtkType)actionDetail2;

            switch (damageClass)
            {
                case ClassModifier.magical:
                    actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
                    actionValues.Add(new ActionValue(actionValue3, actionValue4, PropertyKey.magicStr));
                    break;
                case ClassModifier.physical:
                case ClassModifier.inevitablePhysical:
                    actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
                    actionValues.Add(new ActionValue(actionValue3, actionValue4, PropertyKey.atk));
                    break;
                default:
                    break;
            }
        }

        public
            override string localizedDetail(int level, Property property)
        {
            StringBuilder str = new StringBuilder();
            str.Append(Utils.JavaFormat(Utils.GetString("Deal_s1_s2_damage_to_s3"),
                buildExpression(level, property), damageClass.description(), targetParameter.buildTargetClause()));
            if (criticalModifier == CriticalModifier.critical)
            {
                str.Append(actionValue5.value < 0
                    ? Utils.GetString("All_hits_are_ensured_critical")
                    : Utils.JavaFormat(Utils.GetString("Hit_d_is_ensured_critical"), actionValue5.valueString()));
            }
            if (actionValue6.value != 0)
            {
                str.Append(Utils.JavaFormat(Utils.GetString("Critical_damage_is_s_times_as_normal_damage"), 2 * actionValue6.value));
            }
            if (decideTargetAtkType == DecideTargetAtkType.lowerDef)
            {
                str.Append(Utils.JavaFormat(Utils.GetString("This_damage_type_is_judged_by_the_lower_defence_value_of_targeted_enemy")));
            }
            if (actionValue7.value != 0)
            {
                str.Append(Utils.JavaFormat(Utils.GetString("This_damage_ignores_s1_s2_defense"), actionValue7.value, damageClass.description()));
            }
            if (actionDetail3 != 0)
            {
                str.Append(Utils.JavaFormat(Utils.GetString("recover_tp_by_damage_divided_by_s1"), 100 - actionDetail3));
            }
            return str.ToString();
        }
    }
}
