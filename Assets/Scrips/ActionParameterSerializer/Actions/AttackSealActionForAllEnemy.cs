using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class AttackSealActionForAllEnemy : ActionParameter
    {
        public override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            string condition = actionDetail1 switch
            {
                1 => Utils.GetString("when_first_damaging_enemy"),
                3 => Utils.GetString("when_damages_enemy"),
                4 => Utils.GetString("when_deals_critical_to_enemy"),
                5 => Utils.GetString("when_hits_enemy"),
                _ => null
            };
            if (condition == null)
                return base.localizedDetail(level, property);

            return Utils.JavaFormat(Utils.GetString("Give_hit_enemy_one_mark"),
                targetParameter.buildTargetClause(), condition, actionValue1.valueString(), actionValue2.valueString(),
                buildExpression(level, RoundingMode.UNNECESSARY, property));
        }
    }
}
