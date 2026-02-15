using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class ReduceDamageHigherDefAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("reduce_s1_damage_higher_def_by_s2_time"),
                targetParameter.buildTargetClause(),
                actionValue1.value);
        }
    }
}