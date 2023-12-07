using System;
using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class AdditiveAction : ActionParameter
    {

        public PropertyKey keyType;
        public List<ActionValue> limitValues = new();

        public
            override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
            limitValues.Add(new ActionValue(actionValue4, actionValue5, null));
            switch ((int)actionValue1.value)
            {
                case 7:
                    keyType = PropertyKey.atk; break;
                case 8:
                    keyType = PropertyKey.magicStr; break;
                case 9:
                    keyType = PropertyKey.def; break;
                case 10:
                    keyType = PropertyKey.magicDef; break;
                default:
                    keyType = PropertyKey.unknown; break;
            }
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string result = base.localizedDetail(level, property);
            switch ((int)actionValue1.value)
            {
                case 0:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_HP_to_value_d2_of_effect_d3"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        actionDetail2, actionDetail1 % 10);
                    break;
                case 1:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_lost_HP_to_value_d2_of_effect_d3"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        actionDetail2, actionDetail1 % 10);
                    break;
                case 2:
                    /*
                     * TODO: 从表象出发，迎合游戏内数值手动乘个2，欢迎大佬提出有依据的解决方案。有关此bug详情请查看 issue#29
                     */
                    string s1 = buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true);
                    try
                    {
                        s1 = Utils.roundIfNeed(2.0 * double.Parse(s1));
                    }
                    catch (Exception)
                    {
                        s1 = "2 * " + s1;
                    }
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_count_of_defeated_enemies_to_value_d2_of_effect_d3"),
                        s1,
                        actionDetail2,
                        actionDetail1 % 10);
                    break;
                case 4:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_count_of_targets_to_value_d2_of_effect_d3"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        actionDetail2, actionDetail1 % 10);
                    break;
                case 5:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_count_of_damaged_to_value_d2_of_effect_d3"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        actionDetail2, actionDetail1 % 10);
                    break;
                case 6:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_total_damage_to_value_d2_of_effect_d3"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        actionDetail2, actionDetail1 % 10);
                    break;
                case 12:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_count_of_s2_behind_self_to_value_d3_of_effect_d4"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        targetParameter.buildTargetClause(), actionDetail2, actionDetail1 % 10);
                    break;
                case 13:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_lost_hp_total_hp_of_s2_behind_self_to_value_d3_of_effect_d4"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        targetParameter.buildTargetClause(), actionDetail2, actionDetail1 % 10);
                    break;
                case 14:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_max_hp_of_s2_to_value_d3_of_effect_d4"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        targetParameter.buildTargetClause(), actionDetail2, actionDetail1 % 10);
                    break;
                case 15:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_total_current_hp_of_s2_to_value_d3_of_effect_d4"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        targetParameter.buildTargetClause(), actionDetail2, actionDetail1 % 10);
                    break;
                case 102:
                    result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_count_of_omemes_value_d2_of_effect_d3"),
                        buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                        actionDetail2, actionDetail1 % 10);
                    break;
                default:
                    if (actionValue1.value >= 200 && actionValue1.value < 300)
                    {
                        result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_stacks_of_mark_ID_d2_to_value_d3_of_effect_d4"),
                            buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                            ((int)actionValue1.value) % 200, actionDetail2, actionDetail1 % 10);
                    }
                    else if (actionValue1.value >= 2000 && actionValue1.value < 3000)
                    {
                        result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_stacks_of_mark_ID_d2_to_value_d3_of_effect_d4"),
                            buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                            ((int)actionValue1.value) % 200, actionDetail2, actionDetail1 % 10);
                    }
                    else if (actionValue1.value >= 7 && actionValue1.value <= 10)
                    {
                        result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_s2_of_s3_to_value_d4_of_effect_d5"),
                            buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                            keyType.description(), targetParameter.buildTargetClause(), actionDetail2, actionDetail1 % 10);
                    }
                    else if (actionValue1.value >= 20 && actionValue1.value < 30)
                    {
                        result = Utils.JavaFormat(Utils.GetString("Modifier_add_s1_number_on_counter_d2_to_value_d3_of_effect_d4"),
                            buildExpression(level, null, RoundingMode.UNNECESSARY, property, false, false, true),
                            (int)actionValue1.value % 10, actionDetail2, actionDetail1 % 10);
                    }
                    break;
            }
            if (actionValue4.value != 0 && actionValue5.value != 0)
            {
                result += Utils.JavaFormat(Utils.GetString("The_upper_limit_of_this_effect_is_s"),
                    buildExpression(level, limitValues, null, property)
                );
            }
            return result;
        }
    }
}
