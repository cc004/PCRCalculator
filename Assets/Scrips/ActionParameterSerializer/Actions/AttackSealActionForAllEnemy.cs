namespace ActionParameterSerializer.Actions
{
    public class AttackSealActionForAllEnemy : AttackSealAction
    {
        public
            override string localizedDetail(int level, Property property)
        {
            if (condition == Condition.hit)
            {
                return Utils.JavaFormat(Utils.GetString("Make_s1_when_get_one_hit_by_the_caster_gain_one_mark_stack_max_s2_ID_s3_for_s4_sec_for_all_enemy"),
                    targetParameter.buildTargetClause(),
                    Utils.roundDownDouble(actionValue1.value),
                    Utils.roundDownDouble(actionValue2.value),
                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
            }
            else if (condition == Condition.damage && target == Target.owner)
            {
                return Utils.JavaFormat(Utils.GetString("Make_s1_when_deal_damage_gain_one_mark_stack_max_s2_ID_s3_for_s4_sec_for_all_enemy"),
                    targetParameter.buildTargetClause(),
                    Utils.roundDownDouble(actionValue1.value),
                    Utils.roundDownDouble(actionValue2.value),
                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
            }
            else if (condition == Condition.criticalHit && target == Target.owner)
            {
                return Utils.JavaFormat(Utils.GetString("Make_s1_when_deal_critical_damage_gain_one_mark_stack_max_s2_ID_s3_for_s4_sec_for_all_enemy"),
                    targetParameter.buildTargetClause(),
                    Utils.roundDownDouble(actionValue1.value),
                    Utils.roundDownDouble(actionValue2.value),
                    buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
            }
            else
            {
                return base.localizedDetail(level, property);
            }
        }
    }
}