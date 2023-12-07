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

        public
            override void childInit()
        {
            barrierType = (BarrierType)(actionDetail1);
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (barrierType)
            {
                case BarrierType.physicalGuard:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_nullify_s2_physical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        Utils.roundDouble(actionValue3.value));
                case BarrierType.magicalGuard:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_nullify_s2_magical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        Utils.roundDouble(actionValue3.value));
                case BarrierType.physicalDrain:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_absorb_s2_physical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        Utils.roundDouble(actionValue3.value));
                case BarrierType.magicalDrain:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_absorb_s2_magical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        Utils.roundDouble(actionValue3.value));
                case BarrierType.bothDrain:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_absorb_s2_physical_and_magical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        Utils.roundDouble(actionValue3.value));
                case BarrierType.bothGuard:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_nullify_s2_physical_and_magical_damage_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, property),
                        Utils.roundDouble(actionValue3.value));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
