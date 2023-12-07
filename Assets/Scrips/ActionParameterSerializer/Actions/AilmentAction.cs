using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class AilmentAction : ActionParameter
    {

        private Ailment ailment;
        private readonly List<ActionValue> chanceValues = new();
        private List<ActionValue> durationValues;

        public
            override void childInit()
        {
            ailment = new Ailment(rawActionType, actionDetail1);
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            chanceValues.Add(new ActionValue(actionValue3, actionValue4, null));
            durationValues = chanceValues;
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (ailment.ailmentType)
            {
                case Ailment.AilmentType.action:
                    string str;
                    switch ((Ailment.ActionDetail)ailment.ailmentDetail.detail)
                    {
                        //                    case haste:
                        //                        str = Utils.JavaFormat(Utils.GetString("Raise_s1_d2_attack_speed_for_s3_sec"),
                        //                                targetParameter.buildTargetClause(), Math.Round((actionValue1 - 1) * 100), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                        //                        break;
                        //                    case slow:
                        //                        str = Utils.JavaFormat(Utils.GetString("Reduce_s1_d2_attack_speed_for_s3_sec"),
                        //                                targetParameter.buildTargetClause(), Math.Round((1 - actionValue1) * 100), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                        //                        break;
                        case Ailment.ActionDetail.haste:
                        case Ailment.ActionDetail.slow:
                            if (UserSettings.get().getExpression() == UserSettings.EXPRESSION_ORIGINAL)
                            {
                                str = Utils.JavaFormat(Utils.GetString("Multiple_attack_speed_of_s1_with_s2_for_s3_sec"),
                                    targetParameter.buildTargetClause(),
                                    buildExpression(level, actionValues, RoundingMode.UNNECESSARY, property) + " * 100",
                                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
                                );
                            }
                            else
                            {
                                str = Utils.JavaFormat(Utils.GetString("Multiple_attack_speed_of_s1_with_s2_for_s3_sec"),
                                    targetParameter.buildTargetClause(),
                                    Utils.roundIfNeed(double.Parse(buildExpression(level, actionValues, RoundingMode.UNNECESSARY, property)) * 100),
                                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
                                );
                            }
                            break;
                        case Ailment.ActionDetail.sleep:
                            str = Utils.JavaFormat(Utils.GetString("Make_s1_fall_asleep_for_s2_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                            break;
                        case Ailment.ActionDetail.faint:
                            str = Utils.JavaFormat(Utils.GetString("Make_s1_fall_into_faint_for_s2_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                            break;
                        case Ailment.ActionDetail.timeStop:
                            str = Utils.JavaFormat(Utils.GetString("Stop_s1_for_s2_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                            break;
                        default:
                            str = Utils.JavaFormat(Utils.GetString("s1_s2_for_s3_sec"),
                                ailment.description(), targetParameter.buildTargetClause(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                            break;
                    }
                    if (actionDetail2 == 1)
                    {
                        str += Utils.JavaFormat(Utils.GetString("This_effect_will_be_released_when_taking_damaged"));
                    }
                    return str;
                case Ailment.AilmentType.dot:
                    string r;
                    switch ((Ailment.DotDetail)ailment.ailmentDetail.detail)
                    {
                        case Ailment.DotDetail.poison:
                            r = Utils.JavaFormat(Utils.GetString("Poison_s1_and_deal_s2_damage_per_second_for_s3_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, property), buildExpression(level, durationValues, RoundingMode.HALF_UP, property));
                            break;
                        case Ailment.DotDetail.violentPoison:
                            r = Utils.JavaFormat(Utils.GetString("Poison_s1_violently_and_deal_s2_damage_per_second_for_s3_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, property), buildExpression(level, durationValues, RoundingMode.HALF_UP, property));
                            break;
                        default:
                            r = Utils.JavaFormat(Utils.GetString("s1_s2_and_deal_s3_damage_per_second_for_s4_sec"),
                                ailment.description(), targetParameter.buildTargetClause(), buildExpression(level, property), buildExpression(level, durationValues, RoundingMode.HALF_UP, property));
                            break;
                    }
                    if (actionValue5.value > 0)
                    {
                        r += Utils.JavaFormat(Utils.GetString("DMG_shall_be_increased_by_s_percents_of_base_DMG_through_each_tick"), actionValue5.valueString());
                    }
                    return r;
                case Ailment.AilmentType.silence:
                    return Utils.JavaFormat(Utils.GetString("Silence_s1_with_s2_chance_for_s3_sec"),
                        targetParameter.buildTargetClause(), buildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property), buildExpression(level, property));
                case Ailment.AilmentType.darken:
                    return Utils.JavaFormat(Utils.GetString("Blind_s1_with_s2_chance_for_s3_sec_physical_attack_has_d4_chance_to_miss"),
                        targetParameter.buildTargetClause(), buildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property), buildExpression(level, RoundingMode.UNNECESSARY, property), 100 - actionDetail1);
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
