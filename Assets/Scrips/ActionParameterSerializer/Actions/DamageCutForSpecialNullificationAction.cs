using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class DamageCutForSpecialNullificationAction : ActionParameter
    {
        private readonly List<ActionValue> durationValues = new();

        public override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            string damageType = actionDetail1 switch
            {
                1 => ClassModifier.physical.description(),
                2 => ClassModifier.magical.description(),
                3 => Utils.GetString("both_damage_types"),
                _ => null
            };
            if (damageType == null || (int)actionValue5.value != 0 && (int)actionValue5.value != 1)
                return base.localizedDetail(level, property);

            string detail = Utils.JavaFormat(Utils.GetString("Reduce_s1_special_damage_s2_by_s3"),
                targetParameter.buildTargetClause(), damageType, buildExpression(level, RoundingMode.UNNECESSARY, property));
            return detail + ((int)actionValue5.value == 0
                ? Utils.JavaFormat(Utils.GetString("Effect_last_s_sec"), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property))
                : Utils.GetString("End_when_front_ally_condition_fails"));
        }
    }
}
