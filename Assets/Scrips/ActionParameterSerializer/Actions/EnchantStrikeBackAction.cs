namespace ActionParameterSerializer.Actions
{
    public class EnchantStrikeBackAction : BarrierAction
    {
        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (barrierType)
            {
                case BarrierType.physicalGuard:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_when_taking_physical_damage"),
                        targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.CEILING, property), actionValue3.valueString());
                case BarrierType.magicalGuard:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_when_taking_magical_damage"),
                        targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.CEILING, property), actionValue3.valueString());
                case BarrierType.physicalDrain:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_and_recover_the_same_HP_when_taking_physical_damage"),
                        targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.CEILING, property), actionValue3.valueString());
                case BarrierType.magicalDrain:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_and_recover_the_same_HP_when_taking_magical_damage"),
                        targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.CEILING, property), actionValue3.valueString());
                case BarrierType.bothGuard:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_when_taking_damage"),
                        targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.CEILING, property), actionValue3.valueString());
                case BarrierType.bothDrain:
                    return Utils.JavaFormat(Utils.GetString("Cast_a_barrier_on_s1_to_strike_back_s2_damage_and_recover_the_same_HP_when_taking_damage"),
                        targetParameter.buildTargetClause(), buildExpression(level, RoundingMode.CEILING, property), actionValue3.valueString());
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
