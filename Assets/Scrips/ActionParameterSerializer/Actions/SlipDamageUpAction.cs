using System.Linq;

namespace ActionParameterSerializer.Actions
{
    public class SlipDamageUpAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            string damageType = Utils.GetString("all_abnormal_damage");
            if (actionValue3.value != -1)
            {
                var types = new[] { actionValue3.value, actionValue4.value, actionValue5.value, actionValue6.value };
                damageType = string.Join("、", types.Where(value => value != -1)
                    .Select(value => ((Ailment.DotDetail)(int)value).description()));
            }

            string detail = Utils.JavaFormat(Utils.GetString("Multiply_s1_slip_damage_s2_by_s3_for_s4_sec"),
                targetParameter.buildTargetClause(), damageType, Utils.roundIfNeed(1 + actionValue1.value / 100),
                actionValue2.valueString());
            if (actionValue7.value != 0)
                detail += Utils.JavaFormat(Utils.GetString("Slip_damage_effect_limit_s"), actionValue7.valueString());
            return detail;
        }
    }
}