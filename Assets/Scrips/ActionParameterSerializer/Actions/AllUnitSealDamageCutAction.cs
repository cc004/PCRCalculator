using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class AllUnitSealDamageCutAction : ActionParameter
    {
        public override void childInit()
        {
            base.childInit();
        }

        public override string localizedDetail(int level, Property property)
        {
            var time = actionValue4.value;
            var damageCut = actionValue3.value;
            var startCount = actionValue2.value;
            List<int> targetActionIds = new List<int>();
            if (actionDetail1 != 0)
                targetActionIds.Add(actionDetail1);
            if (actionDetail2 != 0)
                targetActionIds.Add(actionDetail2);
            if (actionDetail3 != 0)
                targetActionIds.Add(actionDetail3);

            return Utils.JavaFormat(Utils.GetString("Reduce_s1_percent_damage_taken_from_sealed_attacks_for_s2_sec_up_to_s3_times_by_sealing_s4_actions"),
                        damageCut,
                        time,
                        startCount,
                        string.Join(", ", targetActionIds));

        }
    }
}