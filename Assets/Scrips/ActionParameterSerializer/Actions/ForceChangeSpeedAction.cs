using System;
using System.Collections.Generic;
using System.Text;
using Elements;

namespace ActionParameterSerializer.Actions
{
    public class ForceChangeSpeedAction : ActionParameter
    {
        private readonly List<ActionValue> durationValues = new();

        public override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                ChangeSpeedAction.eChangeSpeedType speedType = (ChangeSpeedAction.eChangeSpeedType)actionDetail1;
                switch (speedType)
                {
                    case ChangeSpeedAction.eChangeSpeedType.HASTE:
                    case ChangeSpeedAction.eChangeSpeedType.SLOW:
                        if (UserSettings.get().getExpression() == UserSettings.EXPRESSION_ORIGINAL)
                        {
                            str.Append(Utils.JavaFormat(Utils.GetString("Force_change_speed_of_s1_with_s2_for_s3_sec"),
                                targetParameter.buildTargetClause(),
                                buildExpression(level, actionValues, RoundingMode.UNNECESSARY, property) + " * 100",
                                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)));
                        }
                        else
                        {
                            str.Append(Utils.JavaFormat(Utils.GetString("Force_change_speed_of_s1_with_s2_for_s3_sec"),
                                targetParameter.buildTargetClause(),
                                Utils.roundIfNeed(double.Parse(buildExpression(level, actionValues, RoundingMode.UNNECESSARY, property)) * 100),
                                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)));
                        }
                        break;
                    default:
                        str.Append(Utils.JavaFormat(Utils.GetString("Force_s1_s2_for_s3_sec"),
                            speedType.GetDescription(),
                            targetParameter.buildTargetClause(),
                            buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)));
                        break;
                }

                if (actionDetail2 == 1)
                    str.Append(Utils.JavaFormat(Utils.GetString("This_effect_will_be_released_when_taking_damaged")));
                return str.ToString();
            }
            catch (Exception)
            {
                return base.localizedDetail(level, property);
            }
        }
    }
}
