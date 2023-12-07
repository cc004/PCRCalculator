namespace ActionParameterSerializer.Actions
{
    public class IfForAllAction : ActionParameter
    {

        private string trueClause;
        private string falseClause;
        private IfType ifType;

        public
            override void childInit()
        {
            ifType = (IfType)(actionDetail1);

            if (actionDetail2 != 0)
            {
                if (actionDetail1 == 710 || actionDetail1 == 100 || actionDetail1 == 1700 || actionDetail1 == 1601)
                {
                    IfType ifType = (IfType)actionDetail1;
                    if (ifType != null)
                        trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_s3"),
                            actionDetail2 % 100, targetParameter.buildTargetClause(true), ifType.description());
                }
                else if (actionDetail1 >= 0 && actionDetail1 < 100)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("d1_chance_use_d2"),
                        actionDetail1, actionDetail2 % 10);
                }
                else if (actionDetail1 >= 500 && actionDetail1 <= 512)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_has_s3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(), ifType.description());
                }
                else if (actionDetail1 == 599)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_has_any_dot_debuff"),
                        actionDetail2 % 10, targetParameter.buildTargetClause());
                }
                else if (actionDetail1 >= 600 && actionDetail1 < 700)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_in_state_of_ID_d3_with_stacks_greater_than_or_equal_to_d4"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 600, (int)actionValue3.value);
                }
                else if (actionDetail1 == 700)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_alone"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 >= 701 && actionDetail1 < 710)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_the_count_of_s2_except_stealth_units_is_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(), actionDetail1 - 700);
                }
                else if (actionDetail1 == 720)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_among_s2_exists_unit_ID_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(), (int)actionValue3.value);
                }
                else if (actionDetail1 == 721)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), (int)actionValue3.value);
                }
                else if (actionDetail1 >= 901 && actionDetail1 < 1000)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_HP_is_below_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 900);
                }
                else if (actionDetail1 == 1000)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("if_target_is_defeated_by_the_last_effect_then_use_d"),
                        actionDetail2 % 10);
                }
                else if (actionDetail1 == 1001)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("Use_s_if_this_skill_get_critical"),
                        actionDetail2 % 10);
                }
                else if (actionDetail1 >= 1200 && actionDetail1 < 1300)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("counter_d3_is_greater_than_or_equal_to_d1_then_use_d2"),
                        actionDetail1 % 10, actionDetail2 % 10, actionDetail1 % 100 / 10);
                }
                else if (actionDetail1 == 1800)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("Performs_d1_if_s2_is_a_multi_target_unit"),
                        actionDetail2 % 10, targetParameter.buildTargetClause());
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000 && actionValue3.value == 0)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 6000);
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_in_state_of_ID_d3_with_stacks_greater_than_or_equal_to_d4"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 6000, (int)actionValue3.value);
                }
                else if (actionDetail1 >= 3000)
                {
                    var env = (EnvironmentAction.EnvironmentType) (actionDetail1 - 3000);
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_environment_enabled"),
                        actionDetail2 % 10, env.description());
                }
                else if (actionDetail1 >= 1600)
                {
                    var arr = new[] { 35, 67, 71 };
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), arr[actionDetail1 % 100]);
                }
                else if (actionDetail1 >= 1500)
                {
                    var arr = new[] { 35, 67, 71 };
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 % 100);
                }
            }
            else if (actionDetail3 == 0)
            {
                trueClause = Utils.JavaFormat(Utils.GetString("no_effect"));
            }

            if (actionDetail3 != 0)
            {
                if (actionDetail1 == 710 || actionDetail1 == 100 || actionDetail1 == 1700 || actionDetail1 == 1601)
                {
                    IfType ifType = (IfType)actionDetail1;
                    if (ifType != null)
                        falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_s3"),
                            actionDetail3 % 100, targetParameter.buildTargetClause(true), ifType.description());
                }
                else if (actionDetail1 >= 0 && actionDetail1 < 100)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("d1_chance_use_d2"),
                        100 - actionDetail1, actionDetail3 % 10);
                }
                else if (actionDetail1 >= 500 && actionDetail1 <= 512)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_does_not_have_s3"),
                        actionDetail3 % 10, targetParameter.buildTargetClause(), ifType.description());
                }
                else if (actionDetail1 == 599)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_has_no_dot_debuff"),
                        actionDetail3 % 10, targetParameter.buildTargetClause());
                }
                else if (actionDetail1 >= 600 && actionDetail1 < 700)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_not_in_state_of_ID_d3_with_stacks_greater_than_or_equal_to_d4"),
                        actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 600, (int)actionValue3.value);
                }
                else if (actionDetail1 == 700)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_not_alone"),
                        actionDetail3 % 10, targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 >= 701 && actionDetail1 < 710)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_the_count_of_s2_except_stealth_units_is_not_d3"),
                        actionDetail3 % 10, targetParameter.buildTargetClause(), actionDetail1 - 700);
                }
                else if (actionDetail1 == 720)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_among_s2_does_not_exists_unit_ID_d3"),
                        actionDetail3 % 10, targetParameter.buildTargetClause(), (int)actionValue3.value);
                }
                else if (actionDetail1 == 721)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                        actionDetail3 % 10, targetParameter.buildTargetClause(true), (int)actionValue3.value);
                }
                else if (actionDetail1 >= 901 && actionDetail1 < 1000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_HP_is_not_below_d3"),
                        actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 900);
                }
                else if (actionDetail1 == 1000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("if_target_is_not_defeated_by_the_last_effect_then_use_d"),
                        actionDetail3 % 10);
                }
                else if (actionDetail1 == 1001)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("Use_s_if_this_skill_not_get_critical"),
                        actionDetail3 % 10);
                }
                else if (actionDetail1 >= 1200 && actionDetail1 < 1300)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("counter_d3_is_less_than_d1_then_use_d2"),
                        actionDetail1 % 10, actionDetail3 % 10, actionDetail1 % 100 / 10);
                }
                else if (actionDetail1 == 1800)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("Performs_d1_if_s2_is_not_a_multi_target_unit"),
                        actionDetail3 % 10, targetParameter.buildTargetClause());
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000 && actionValue3.value == 0)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                        actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 6000);
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_not_in_state_of_ID_d3_with_stacks_greater_than_or_equal_to_d4"),
                        actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 6000, (int)actionValue3.value);
                }
                else if (actionDetail1 >= 3000)
                {
                    var env = (EnvironmentAction.EnvironmentType)(actionDetail1 - 3000);
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_environment_not_enabled"),
                        actionDetail2 % 10, env.description());
                }
                else if (actionDetail1 >= 1600)
                {
                    var arr = new[] { 35, 67, 71 };
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), arr[actionDetail1 % 100]);
                }
                else if (actionDetail1 >= 1500)
                {
                    var arr = new[] { 35, 67, 71 };
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                        actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 % 100);
                }
            }
            else if (actionDetail2 == 0)
            {
                falseClause = Utils.JavaFormat(Utils.GetString("no_effect"));
            }

        }

        public
            override string localizedDetail(int level, Property property)
        {
            if (trueClause != null && falseClause != null)
            {
                return Utils.JavaFormat(Utils.GetString("Exclusive_condition_s"), trueClause + falseClause);
            }
            else if (trueClause != null)
            {
                return Utils.JavaFormat(Utils.GetString("Exclusive_condition_s"), trueClause);
            }
            else if (falseClause != null)
            {
                return Utils.JavaFormat(Utils.GetString("Exclusive_condition_s"), falseClause);
            }
            else
            {
                return base.localizedDetail(level, property);
            }
        }
    }
}
