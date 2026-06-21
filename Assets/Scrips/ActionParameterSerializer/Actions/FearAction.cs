using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class FearAction : ActionParameter
    {
        private enum FearType
        {
            Fear = 0,
            AllFear = 1
        }

        private FearType fearType;
        public List<ActionValue> durationValues = new();
        public List<ActionValue> chanceValues = new();

        public
            override void childInit()
        {
            base.childInit();
            fearType = (FearType)actionDetail1;
            durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
            chanceValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (fearType)
            {
                case FearType.Fear:
                    return Utils.JavaFormat(Utils.GetString("Fear_s1_with_s2_chance_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                case FearType.AllFear:
                    return Utils.JavaFormat(Utils.GetString("All_fear_s1_with_s2_chance_for_s3_sec"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, chanceValues, RoundingMode.UNNECESSARY, property),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
