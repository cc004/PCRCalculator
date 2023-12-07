using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace ActionParameterSerializer.Actions
{
    public class EveryAttackCriticalAction : ActionParameter
    {

        private enum eEveryAtkCriticalType
        {
            physical = 1,
            magical,
            both
        }

        private readonly List<ActionValue> durationValues = new();
        private eEveryAtkCriticalType atkType;

        public override void childInit()
        {
            base.childInit();
            atkType = (eEveryAtkCriticalType) actionDetail1;
            durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Enchant_self_with_s1_attack_critical_for_s2_sec"),
                atkType.description(),
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}