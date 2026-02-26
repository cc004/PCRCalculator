using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class ExtendSlipDamageAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("extend_s1_slip_damage_last_and_interval_by_s2_in_s3_time"),
                targetParameter.buildTargetClause(),
                actionValue1.value / 100,
                actionValue2.value);
        }
    }
}