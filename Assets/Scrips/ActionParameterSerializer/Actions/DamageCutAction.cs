using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class DamageCutAction : ActionParameter
    {

        public enum DamageType
        {
            Physical = 1,
            Magical = 2,
            All = 3
        }

        public DamageType damageType;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            damageType = (DamageType)actionDetail1;
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (damageType)
            {
                case DamageType.Physical:
                    return Utils.JavaFormat(Utils.GetString("Reduce_s1_physical_damage_taken_by_s2_for_s3_sec"),
                        buildExpression(level, actionValues, null, property),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, durationValues, null, property));
                case DamageType.Magical:
                    return Utils.JavaFormat(Utils.GetString("Reduce_s1_magical_damage_taken_by_s2_for_s3_sec"),
                        buildExpression(level, actionValues, null, property),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, durationValues, null, property));
                case DamageType.All:
                    return Utils.JavaFormat(Utils.GetString("Reduce_s1_all_damage_taken_by_s2_for_s3_sec"),
                        buildExpression(level, actionValues, null, property),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, durationValues, null, property));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
