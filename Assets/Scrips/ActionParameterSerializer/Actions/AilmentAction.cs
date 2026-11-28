using System.Collections.Generic;
using System.Text;

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
            StringBuilder str = new StringBuilder();
            switch (ailment.ailmentType)
            {
                case Ailment.AilmentType.action:
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
                            str.Append(Utils.JavaFormat(Utils.GetString("Multiple_attack_speed_of_s1_with_s2_for_s3_sec"),
                                targetParameter.buildTargetClause(), describePercent(level, actionValues, property),
                                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)));
                            break;
                        case Ailment.ActionDetail.sleep:
                            str.Append(Utils.JavaFormat(Utils.GetString("Make_s1_fall_asleep_for_s2_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)));
                            break;
                        case Ailment.ActionDetail.faint:
                            str.Append(Utils.JavaFormat(Utils.GetString("Make_s1_fall_into_faint_for_s2_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)));
                            break;
                        case Ailment.ActionDetail.timeStop:
                            str.Append(Utils.JavaFormat(Utils.GetString("Stop_s1_for_s2_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)));
                            break;
                        default:
                            str.Append(Utils.JavaFormat(Utils.GetString("s1_s2_for_s3_sec"),
                                ailment.description(), targetParameter.buildTargetClause(), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)));
                            break;
                    }
                    if (actionDetail2 == 1)
                    {
                        str.Append(Utils.GetString("This_effect_will_be_released_when_taking_attack_damage"));
                    }
                    else if (actionDetail2 == 2)
                    {
                        str.Append(Utils.JavaFormat(Utils.GetString("Release_control_on_max_HP_change_at_s_percent"),
                            actionValue6.valueString()));
                    }
                    break;
                case Ailment.AilmentType.dot:
                    switch ((Ailment.DotDetail)ailment.ailmentDetail.detail)
                    {
                        case Ailment.DotDetail.poison:
                            str.Append(Utils.JavaFormat(Utils.GetString("Poison_s1_and_deal_s2_damage_per_second_for_s3_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, property), buildExpression(level, durationValues, RoundingMode.HALF_UP, property)));
                            break;
                        case Ailment.DotDetail.violentPoison:
                            str.Append(Utils.JavaFormat(Utils.GetString("Poison_s1_violently_and_deal_s2_damage_per_second_for_s3_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, property), buildExpression(level, durationValues, RoundingMode.HALF_UP, property)));
                            break;
                        case Ailment.DotDetail.raio_damage:
                            str.Append(Utils.JavaFormat(Utils.GetString("s1_and_deal_s2_present_damage_up_to_s3_per_second_for_s4_sec"),
                                targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.UNNECESSARY, property), actionValue5.value, buildExpression(level, durationValues, RoundingMode.HALF_UP, property)));
                            break;
                        case Ailment.DotDetail.stackable:
                            str.Append(Utils.JavaFormat(Utils.GetString("Stackable_slip_s1_and_deal_s2_damage_per_second_for_s3_sec_max_s4_stacks_each_stack_adds_s5_times_base_damage"),
                                targetParameter.buildTargetClause(),
                                buildExpression(level, property),
                                buildExpression(level, durationValues, RoundingMode.HALF_UP, property),
                                actionValue7.valueString(),
                                actionValue5.valueString()));
                            break;
                        default:
                            str.Append(Utils.JavaFormat(Utils.GetString("s1_s2_and_deal_s3_damage_per_second_for_s4_sec"),
                                ailment.description(), targetParameter.buildTargetClause(), buildExpression(level, property), buildExpression(level, durationValues, RoundingMode.HALF_UP, property)));
                            break;
                    }
                    if ((Ailment.DotDetail)ailment.ailmentDetail.detail != Ailment.DotDetail.raio_damage
                        && (Ailment.DotDetail)ailment.ailmentDetail.detail != Ailment.DotDetail.stackable
                        && actionValue5.value > 0)
                    {
                        str.Append(Utils.JavaFormat(Utils.GetString("DMG_shall_be_increased_by_s_percents_of_base_DMG_through_each_tick"), actionValue5.valueString()));
                    }
                    if (actionDetail2 == 2 && (Ailment.DotDetail)ailment.ailmentDetail.detail != Ailment.DotDetail.stackable)
                    {
                        str.Append(Utils.JavaFormat(Utils.GetString("Slip_damage_is_released_at_s_percent_HP"),
                            actionValue7.valueString()));
                    }
                    break;
                case Ailment.AilmentType.silence:
                    str.Append(Utils.JavaFormat(Utils.GetString("Apply_s2_to_s1_for_s3_sec"),
                        targetParameter.buildTargetClause(), Utils.GetString(actionDetail1 == 1 ? "UB_silence" : "Silence"),
                        buildExpression(level, RoundingMode.UNNECESSARY, property)));
                    if (actionValue3.value != 1 || actionValue4.value != 0)
                    {
                        str.Append(Utils.JavaFormat(Utils.GetString("Abnormal_base_coefficient_s"),
                            buildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property)));
                    }
                    break;
                case Ailment.AilmentType.darken:
                    str.Append(Utils.JavaFormat(Utils.GetString("Blind_s1_with_s2_chance_for_s3_sec_physical_attack_has_d4_chance_to_miss"),
                        targetParameter.buildTargetClause(), buildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property), buildExpression(level, RoundingMode.UNNECESSARY, property), 100 - actionDetail1));
                    break;
                default:
                    str.Append(base.localizedDetail(level, property));
                    break;
            }
            if (actionDetail3 != 0)
            {
                str.Append(Utils.JavaFormat(Utils.GetString("recover_tp_by_damage_divided_by_s1"), 100 - actionDetail3));
            }
            return str.ToString();
        }
    }
}
