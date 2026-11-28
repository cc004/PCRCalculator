using System;

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
                    var checkId = actionDetail1;
                    if (actionDetail1 == 1700)
                        checkId += (int)actionValue3.value;
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_s3"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true), ((IfType)checkId).description());
                }
                else if (actionDetail1 >= 0 && actionDetail1 < 100)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("d1_chance_use_d2"),
                        actionDetail1, describeActionSlot(actionDetail2));
                }
                else if (actionDetail1 >= 500 && actionDetail1 <= 512)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_has_s3"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(), ifType.description());
                }
                else if (actionDetail1 == 599)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_has_any_dot_debuff"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause());
                }
                else if (actionDetail1 >= 600 && actionDetail1 < 700)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_in_state_of_ID_d3_with_stacks_greater_than_or_equal_to_d4"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true), actionDetail1 - 600, (int)actionValue3.value);
                }
                else if (actionDetail1 == 700)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_alone"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 >= 701 && actionDetail1 < 710)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_the_count_of_s2_except_stealth_units_is_d3"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(), actionDetail1 - 700);
                }
                else if (actionDetail1 == 720)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_among_s2_exists_unit_ID_d3"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(), (int)actionValue3.value);
                }
                else if (actionDetail1 == 721)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_marked_target_count_is_at_least_d3"),
                        describeActionSlot(actionDetail2), (int)actionValue3.value, (int)actionValue4.value, targetParameter.buildTargetClause());
                }
                else if (actionDetail1 == 900)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_HP_is_full"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 >= 901 && actionDetail1 < 1000)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_HP_is_below_d3"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true), actionDetail1 - 900);
                }
                else if (actionDetail1 == 1000)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("if_target_is_defeated_by_the_last_effect_then_use_d"),
                        describeActionSlot(actionDetail2));
                }
                else if (actionDetail1 == 1001)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("Use_s_if_this_skill_get_critical"),
                        describeActionSlot(actionDetail2));
                }
                else if (actionDetail1 > 1200 && actionDetail1 < 1600 && actionDetail1 != 1300 && actionDetail1 != 1400)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("counter_d3_equals_d1_then_use_d2"),
                        actionDetail1 % 10, describeActionSlot(actionDetail2), actionDetail1 % 100 / 10);
                }
                else if (actionDetail1 == 1300 || actionDetail1 == 1400)
                {
                    string key = actionDetail1 == 1300 ? "use_d1_to_s2_if_target_is_physical_type" : "use_d1_to_s2_if_s3";
                    trueClause = Utils.JavaFormat(Utils.GetString(key), describeActionSlot(actionDetail2),
                        targetParameter.buildTargetClause(true), IfType.polymorphed.description());
                }
                else if (actionDetail1 == 1800)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("Performs_d1_if_s2_is_a_multi_target_unit"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause());
                }
                else if (actionDetail1 == 1900)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("Performs_d1_on_s2_if_the_target_possesses_a_barrier"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 == 2000 || actionDetail1 == 2001)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_attack_type_count_is_at_least_d3"),
                        describeActionSlot(actionDetail2), ((ClassModifier)(actionDetail1 - 1999)).description(), (int)actionValue3.value, targetParameter.buildTargetClause());
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000 && actionValue3.value == 0)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true), actionDetail1 - 6000);
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_in_state_of_ID_d3_with_stacks_greater_than_or_equal_to_d4"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true), actionDetail1 - 6000, (int)actionValue3.value);
                }
                else if (actionDetail1 >= 4000)
                {
                    int talentId = actionDetail1 - 4000;
                    var talentDescription = ((PCRCaculator.Battle.Talent)talentId).description();
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_in_talent_s3"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true), talentDescription);
                }
                else if (actionDetail1 >= 3000)
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_environment_enabled"),
                        describeActionSlot(actionDetail2), EnvironmentAction.describeEnvironment(actionDetail1 - 3000));
                }
                else if (actionDetail1 >= 1600 && actionDetail1 % 100 < 3)
                {
                    var arr = new[] { 35, 67, 71 };
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                        describeActionSlot(actionDetail2), targetParameter.buildTargetClause(true), arr[actionDetail1 % 100]);
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
                            describeActionSlot(actionDetail3), targetParameter.buildTargetClause(true), ifType.description());
                }
                else if (actionDetail1 >= 0 && actionDetail1 < 100)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("d1_chance_use_d2"),
                        100 - actionDetail1, describeActionSlot(actionDetail3));
                }
                else if (actionDetail1 >= 500 && actionDetail1 <= 512)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_does_not_have_s3"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(), ifType.description());
                }
                else if (actionDetail1 == 599)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_has_no_dot_debuff"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause());
                }
                else if (actionDetail1 >= 600 && actionDetail1 < 700)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_not_in_state_of_ID_d3_with_stacks_greater_than_or_equal_to_d4"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(true), actionDetail1 - 600, (int)actionValue3.value);
                }
                else if (actionDetail1 == 700)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_not_alone"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 >= 701 && actionDetail1 < 710)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_the_count_of_s2_except_stealth_units_is_not_d3"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(), actionDetail1 - 700);
                }
                else if (actionDetail1 == 720)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_among_s2_does_not_exists_unit_ID_d3"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(), (int)actionValue3.value);
                }
                else if (actionDetail1 == 721)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_marked_target_count_is_not_at_least_d3"),
                        describeActionSlot(actionDetail3), (int)actionValue3.value, (int)actionValue4.value, targetParameter.buildTargetClause());
                }
                else if (actionDetail1 >= 901 && actionDetail1 < 1000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_HP_is_not_below_d3"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(true), actionDetail1 - 900);
                }
                else if (actionDetail1 == 1000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("if_target_is_not_defeated_by_the_last_effect_then_use_d"),
                        describeActionSlot(actionDetail3));
                }
                else if (actionDetail1 == 1001)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("Use_s_if_this_skill_not_get_critical"),
                        describeActionSlot(actionDetail3));
                }
                else if (actionDetail1 > 1200 && actionDetail1 < 1600 && actionDetail1 != 1300 && actionDetail1 != 1400)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("counter_d3_does_not_equal_d1_then_use_d2"),
                        actionDetail1 % 10, describeActionSlot(actionDetail3), actionDetail1 % 100 / 10);
                }
                else if (actionDetail1 == 1300 || actionDetail1 == 1400)
                {
                    string key = actionDetail1 == 1300 ? "use_d1_to_s2_if_target_is_not_physical_type" : "use_d1_to_s2_if_not_s3";
                    falseClause = Utils.JavaFormat(Utils.GetString(key), describeActionSlot(actionDetail3),
                        targetParameter.buildTargetClause(true), IfType.polymorphed.description());
                }
                else if (actionDetail1 == 1800)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("Performs_d1_if_s2_is_not_a_multi_target_unit"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause());
                }
                else if (actionDetail1 == 1900)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("Performs_d1_on_s2_if_the_target_does_not_possess_a_barrier"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(true));
                }
                else if (actionDetail1 == 2000 || actionDetail1 == 2001)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_attack_type_count_is_not_at_least_d3"),
                        describeActionSlot(actionDetail3), ((ClassModifier)(actionDetail1 - 1999)).description(), (int)actionValue3.value, targetParameter.buildTargetClause());
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000 && actionValue3.value == 0)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(true), actionDetail1 - 6000);
                }
                else if (actionDetail1 >= 6000 && actionDetail1 < 7000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_not_in_state_of_ID_d3_with_stacks_greater_than_or_equal_to_d4"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(true), actionDetail1 - 6000, (int)actionValue3.value);
                }
                else if (actionDetail1 >= 4000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_is_not_in_talent_s3"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(true),
                        ((PCRCaculator.Battle.Talent)(actionDetail1 - 4000)).description());
                }
                else if (actionDetail1 >= 3000)
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_environment_not_enabled"),
                        describeActionSlot(actionDetail3), EnvironmentAction.describeEnvironment(actionDetail1 - 3000));
                }
                else if (actionDetail1 >= 1600 && actionDetail1 % 100 < 3)
                {
                    var arr = new[] { 35, 67, 71 };
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                        describeActionSlot(actionDetail3), targetParameter.buildTargetClause(true), arr[actionDetail1 % 100]);
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
