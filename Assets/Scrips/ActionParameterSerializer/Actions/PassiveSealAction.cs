using System;
using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    internal class PassiveSealAction : ActionParameter
    {
        public int sealNumLimit;
        public List<ActionValue> sealDuration = new();
        public List<ActionValue> lifeTime = new();
        public ePassiveTiming passiveTiming;
        public eSealTarget sealTarget;
        public double sealNum;

        public
            override void childInit()
        {
            sealNumLimit = (int)actionValue1.value;
            sealDuration.Add(new ActionValue(actionValue3, actionValue4, null));
            lifeTime.Add(new ActionValue(actionValue5, actionValue6, null));
            passiveTiming = (ePassiveTiming)(actionDetail1);
            sealTarget = (eSealTarget)(actionDetail3);
            sealNum = actionValue7.value;
        }

        public
            override string localizedDetail(int level, Property property)
        {
            if (sealNum > 0)
            {
                return Utils.JavaFormat(Utils.GetString("Passive_Whenever_s1_s2_add_s3_mark_stacks_ID_s4_to_s5_for_s6_sec_caps_at_s7_listens_for_s8_sec"),
                    targetParameter.buildTargetClause(),
                    passiveTimingDescription(),
                    Utils.roundDownDouble(sealNum),
                    (int)actionValue2.value,
                    sealTargetDescription(),
                    buildExpression(level, sealDuration, RoundingMode.UNNECESSARY, property),
                    sealNumLimit,
                    buildExpression(level, lifeTime, RoundingMode.UNNECESSARY, property)
                );
            }

            return Utils.JavaFormat(Utils.GetString("Passive_Whenever_s1_s2_remove_s3_mark_stacks_ID_s4_from_s5_listens_for_s6_sec"),
                targetParameter.buildTargetClause(),
                passiveTimingDescription(),
                Utils.roundDownDouble(Math.Abs(sealNum)),
                (int)actionValue2.value,
                sealTargetDescription(),
                buildExpression(level, lifeTime, RoundingMode.UNNECESSARY, property)
            );
        }

        private string passiveTimingDescription()
        {
            switch (passiveTiming)
            {
                case ePassiveTiming.Buff:
                    return Utils.GetString("Passive_seal_timing_buff");
                case ePassiveTiming.Damage:
                    return Utils.GetString("Passive_seal_timing_damaged");
                case ePassiveTiming.Debuff:
                    return Utils.GetString("Passive_seal_timing_debuff");
                case ePassiveTiming.Team_UB:
                    return Utils.GetString("Passive_seal_timing_team_ub");
                default:
                    return Utils.GetString("Unknown");
            }
        }

        private string sealTargetDescription()
        {
            switch (sealTarget)
            {
                case eSealTarget.Source:
                    return Utils.GetString("Passive_seal_source");
                case eSealTarget.Target:
                    return Utils.GetString("Passive_seal_target");
                default:
                    return Utils.GetString("Unknown");
            }
        }

        public enum ePassiveTiming
        {
            Unknown = -1,
            Buff = 1,
            Damage = 2,
            Debuff = 3,
            Team_UB = 4
        }

        public enum eSealTarget
        {
            Unknown = -1,
            Source = 0,
            Target = 1
        }
    }
}
