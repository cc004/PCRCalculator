using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class CharmAction : ActionParameter
    {

        public enum CharmType
        {
            unknown = -1,
            charm = 0,
            confusion = 1
        }

        private readonly List<ActionValue> chanceValues = new();
        private readonly List<ActionValue> durationValues = new();
        private CharmType charmType;

        public
            override void childInit()
        {
            charmType = (CharmType)(actionDetail1);
            durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
            switch (charmType)
            {
                case CharmType.charm:
                    chanceValues.Add(new ActionValue(actionValue3.value, actionValue4.value * 100, eActionValue.VALUE3, eActionValue.VALUE4, null));
                    break;
                default:
                    chanceValues.Add(new ActionValue(actionValue3.value * 100, actionValue4.value * 100, eActionValue.VALUE3, eActionValue.VALUE4, null));
                    break;
            }
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (charmType)
            {
                case CharmType.charm:
                    return Utils.JavaFormat(
                        Utils.GetString("Charm_s1_with_s2_chance_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property)
                    );
                case CharmType.confusion:
                    return Utils.JavaFormat(Utils.GetString("Confuse_s1_with_s2_chance_for_s3_sec"), targetParameter.buildTargetClause(), buildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property), buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
