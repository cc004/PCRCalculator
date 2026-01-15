using System.Text;

namespace ActionParameterSerializer.Actions
{
    public class RatioDamageAction : ActionParameter
    {

        public enum HPtype
        {
            unknown = 0,
            max = 1,
            current = 2,
            originalMax = 3
        }

        public HPtype hptype;

        public
            override void childInit()
        {
            base.childInit();
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            hptype = (HPtype)(actionDetail1);
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string r = buildExpression(level, RoundingMode.UNNECESSARY, property);
            if (UserSettings.get().getExpression() != UserSettings.EXPRESSION_VALUE)
            {
                r = Utils.JavaFormat("(%s)", r);
            }
            StringBuilder str = new StringBuilder();
            switch (hptype)
            {
                case HPtype.max:
                    str.Append(Utils.JavaFormat(Utils.GetString("Deal_damage_equal_to_s1_of_target_max_HP_to_s2"),
                        r, targetParameter.buildTargetClause()));
                    break;
                case HPtype.current:
                    str.Append(Utils.JavaFormat(Utils.GetString("Deal_damage_equal_to_s1_of_target_current_HP_to_s2"),
                        r, targetParameter.buildTargetClause()));
                    break;
                case HPtype.originalMax:
                    str.Append(Utils.JavaFormat(Utils.GetString("Deal_damage_equal_to_s1_of_targets_original_max_HP_to_s2"),
                        r, targetParameter.buildTargetClause()));
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
