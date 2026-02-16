using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class ConvertDamageOverTimeAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("convert_s1_s2_damages_to_s3_dot_in_s4_time"),
                targetParameter.buildTargetClause(),
                actionValue1.value,
                (int)actionValue3.value,
                actionValue2.value);
        }
    }
}