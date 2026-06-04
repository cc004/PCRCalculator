using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class GutsAction : ActionParameter
    {
        private readonly List<ActionValue> durationValues = new();
        private readonly List<ActionValue> countValues = new();
        private readonly List<ActionValue> maxCountValues = new();

        public override void childInit()
        {
            PropertyKey attackKey = actionDetail1 == 1 ? PropertyKey.atk : PropertyKey.magicStr;
            actionValues.Add(new ActionValue(actionValue4.value, 0, eActionValue.VALUE4, eActionValue.VALUE4, attackKey));
            actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
            durationValues.Add(new ActionValue(actionValue5.value, 0, eActionValue.VALUE5, eActionValue.VALUE5, null));
            countValues.Add(new ActionValue(actionValue6.value, 0, eActionValue.VALUE6, eActionValue.VALUE6, null));
            maxCountValues.Add(new ActionValue(actionValue7.value, 0, eActionValue.VALUE7, eActionValue.VALUE7, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            string target = targetParameter.buildTargetClause();
            string recover = buildExpression(level, RoundingMode.DOWN, property);
            string duration = buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property);
            if ((int)actionValue6.value == 0)
            {
                if ((int)actionValue1.value == 2)
                {
                    return Utils.JavaFormat(Utils.GetString("Give_s1_guts_restore_s2_percent_max_HP_for_s3_sec_unlimited_times"),
                        target,
                        recover,
                        duration);
                }

                return Utils.JavaFormat(Utils.GetString("Give_s1_guts_restore_s2_HP_for_s3_sec_unlimited_times"),
                    target,
                    recover,
                    duration);
            }

            string count = buildExpression(level, countValues, RoundingMode.DOWN, property);
            string maxCount = buildExpression(level, maxCountValues, RoundingMode.DOWN, property);
            if ((int)actionValue1.value == 2)
            {
                return Utils.JavaFormat(Utils.GetString("Give_s1_guts_restore_s2_percent_max_HP_for_s3_sec_s4_times_max_s5"),
                    target,
                    recover,
                    duration,
                    count,
                    maxCount);
            }

            return Utils.JavaFormat(Utils.GetString("Give_s1_guts_restore_s2_HP_for_s3_sec_s4_times_max_s5"),
                target,
                recover,
                duration,
                count,
                maxCount);
        }
    }
}
