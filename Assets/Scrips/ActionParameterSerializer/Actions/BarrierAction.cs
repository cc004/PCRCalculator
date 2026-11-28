using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class BarrierAction : ActionParameter
    {

        public enum BarrierType
        {
            unknown = 0,
            physicalGuard = 1,
            magicalGuard = 2,
            physicalDrain = 3,
            magicalDrain = 4,
            bothGuard = 5,
            bothDrain = 6
        }

        public BarrierType barrierType;
        private readonly List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            barrierType = (BarrierType)(actionDetail1);
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            durationValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string duration = buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property);
            switch (barrierType)
            {
                case BarrierType.physicalGuard:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_nullify_s2_physical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        duration);
                case BarrierType.magicalGuard:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_nullify_s2_magical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        duration);
                case BarrierType.physicalDrain:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_absorb_s2_physical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        duration);
                case BarrierType.magicalDrain:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_absorb_s2_magical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        duration);
                case BarrierType.bothDrain:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_absorb_s2_physical_and_magical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        duration);
                case BarrierType.bothGuard:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_nullify_s2_physical_and_magical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        duration);
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
