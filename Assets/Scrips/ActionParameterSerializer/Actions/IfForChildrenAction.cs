using System;

namespace ActionParameterSerializer.Actions
{
    public class IfForChildrenAction : ActionParameter
    {

        private string trueClause;
        private string falseClause;
        private IfType ifType;

        public
            override void childInit()
        {

            if (actionDetail2 != 0)
            {
                ifType = (IfType)actionDetail1;
                if (Enum.IsDefined(typeof(IfType), ifType))
                {
                    trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_s3"),
                        actionDetail2 % 100, targetParameter.buildTargetClause(true), ifType.description());
                }
                else
                {
                    if ((actionDetail1 >= 600 && actionDetail1 < 700) || actionDetail1 == 710)
                    {
                        trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 600);
                    }
                    else if (actionDetail1 == 700)
                    {
                        trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_it_is_alone"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true));
                    }
                    else if (actionDetail1 >= 901 && actionDetail1 < 1000)
                    {
                        trueClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_HP_is_below_d3"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 900);
                    }
                    else if (actionDetail1 == 1300)
                    {
                        trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_target_is_magical_type"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true));
                    }
                    else if (actionDetail1 == 1800)
                    {
                        trueClause = Utils.JavaFormat(Utils.GetString("Performs_d1_to_s2_if_it_is_a_multi_target_unit"),
                            actionDetail2 % 10, targetParameter.buildTargetClause());
                    }
                    else if (actionDetail1 == 1900)
                    {
                        trueClause = Utils.JavaFormat(Utils.GetString("Performs_d1_on_s2_if_the_target_possesses_a_barrier"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true));
                    }
                    else if (actionDetail1 >= 6000 && actionDetail1 < 7000)
                    {
                        trueClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_in_state_of_ID_d3"),
                            actionDetail2 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 6000);
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
                    else if (actionDetail1 >= 3000)
                    {
                        var env = (EnvironmentAction.EnvironmentType)(actionDetail1 - 3000);
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
            }

            if (actionDetail3 != 0)
            {
                ifType = (IfType)actionDetail1;
                if (Enum.IsDefined(typeof(IfType), ifType))
                {
                    falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_s3"),
                        actionDetail3 % 100, targetParameter.buildTargetClause(true), ifType.description());
                }
                else
                {
                    if ((actionDetail1 >= 600 && actionDetail1 < 700) || actionDetail1 == 710)
                    {
                        falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 600);
                    }
                    else if (actionDetail1 == 700)
                    {
                        falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_it_is_not_alone"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true));
                    }
                    else if (actionDetail1 >= 901 && actionDetail1 < 1000)
                    {
                        falseClause = Utils.JavaFormat(Utils.GetString("use_d1_if_s2_HP_is_not_below_d3"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 900);
                    }
                    else if (actionDetail1 == 1300)
                    {
                        falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_target_is_not_magical_type"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true));
                    }
                    else if (actionDetail1 == 1800)
                    {
                        trueClause = Utils.JavaFormat(Utils.GetString("Performs_d1_to_s2_if_it_is_not_a_multi_target_unit"),
                            actionDetail3 % 10, targetParameter.buildTargetClause());
                    }
                    else if (actionDetail1 == 1900)
                    {
                        trueClause = Utils.JavaFormat(Utils.GetString("Performs_d1_on_s2_if_the_target_does_not_possess_a_barrier"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true));
                    }
                    else if (actionDetail1 >= 6000 && actionDetail1 < 7000)
                    {
                        falseClause = Utils.JavaFormat(Utils.GetString("use_d1_to_s2_if_not_in_state_of_ID_d3"),
                            actionDetail3 % 10, targetParameter.buildTargetClause(true), actionDetail1 - 6000);
                    }
                }
            }

        }

        public
            override string localizedDetail(int level, Property property)
        {
            if (actionDetail1 >= 0 && actionDetail1 < 100)
            {
                if (actionDetail2 != 0 && actionDetail3 != 0)
                {
                    return Utils.JavaFormat(Utils.GetString("Random_event_d1_chance_use_d2_otherwise_d3"), actionDetail1, actionDetail2 % 10, actionDetail3 % 10);
                }
                else if (actionDetail2 != 0)
                {
                    return Utils.JavaFormat(Utils.GetString("Random_event_d1_chance_use_d2"), actionDetail1, actionDetail2 % 10);
                }
                else if (actionDetail3 != 0)
                {
                    return Utils.JavaFormat(Utils.GetString("Random_event_d1_chance_use_d2"), 100 - actionDetail1, actionDetail3 % 10);
                }
            }
            else
            {
                if (trueClause != null && falseClause != null)
                    return Utils.JavaFormat(Utils.GetString("Condition_s"), trueClause + falseClause);
                else if (trueClause != null)
                    return Utils.JavaFormat(Utils.GetString("Condition_s"), trueClause);
                else if (falseClause != null)
                    return Utils.JavaFormat(Utils.GetString("Condition_s"), falseClause);
            }
            return base.localizedDetail(level, property);
        }
    }

    public enum IfType
    {
        controlled = 100,
        hastened = 101,
        blinded = 200,
        charmed_or_confused = 300,
        decoying = 400,
        burned = 500,
        cursed = 501,
        poisoned = 502,
        venomed = 503,
        hexed = 504,
        cursed_or_hexed = 511,
        poisoned_or_venomed = 512,
        breaking = 710,
        polymorphed = 1400,
        feared = 1600,
        is_invisible = 1601,
        magic_defence_decreased = 1700,
    }
}