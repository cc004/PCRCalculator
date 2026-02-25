using System.Collections.Generic;
using System.Linq;
using PCRCaculator.Battle;

namespace ActionParameterSerializer.Actions
{
    public class DispelAction : ActionParameter
    {

        public enum DispelType
        {
            unknown = 0,
            buff = 1,
            debuff = 2,
            statusUpBuff = 3,
            barriers = 10,
			GUARD_ATK_BARRIER = 11,
			GUARD_MGC_BARRIER = 12,
			DRAIN_ATK_BARRIER = 13,
			DRAIN_MGC_BARRIER = 14,
			GUARD_BOTH_BARRIER = 15,
			DRAIN_BOTH_BARRIER = 16,
			ALL_ATK_BARRIER = 17,
			ALL_MGC_BARRIER = 18,
			ALL_BOTH_BARRIER = 19,
			BUFF_DEBUFF_TYPE = 20
        }

        public DispelType dispelType;
        public List<ActionValue> chanceValues = new();

        public
            override void childInit()
        {
            base.childInit();
            dispelType = (DispelType)(actionDetail1);
            chanceValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string buffType = dispelType.description();
            if (dispelType == DispelType.BUFF_DEBUFF_TYPE)
            {
                int[] buffDebuffTypes = new List<DoubleValue>{ actionValue4, actionValue5, actionValue6, actionValue7 }.Where(v => v.value != 0).Select(v => (int)v.value).ToArray();
                buffType = string.Join(", ", buffDebuffTypes.Select(t => 
                    $"{(t % 1000 == 1 ? "额外的" : "")}{((BuffParamKind)(t / 10 % 100)).description()} {(t % 10 == 0 ? "Buff" : "Debuff")}"
                ));
            }
            return Utils.JavaFormat(Utils.GetString("Clear_all_s1_on_s2_with_chance_s3"),
                buffType,
                targetParameter.buildTargetClause(),
                buildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
