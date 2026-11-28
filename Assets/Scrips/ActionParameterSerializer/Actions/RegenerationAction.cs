using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class RegenerationAction : ActionParameter
    {

        public enum RegenerationType
        {
            unknown = -1,
            hp = 1,
            tp = 2,
            hpSecond = 3,
            tpSecond = 4
        }

        public ClassModifier healClass;
        public RegenerationType regenerationType;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            healClass = (ClassModifier)(actionDetail1);
            regenerationType = (RegenerationType)(actionDetail2);
            durationValues.Add(new ActionValue(actionValue5, actionValue6, null));
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
            if (regenerationType == RegenerationType.tp || regenerationType == RegenerationType.tpSecond)
                return;

            switch (healClass)
            {
                case ClassModifier.magical:
                    actionValues.Add(new ActionValue(actionValue3, actionValue4, PropertyKey.magicStr));
                    break;
                case ClassModifier.physical:
                    actionValues.Add(new ActionValue(actionValue3, actionValue4, PropertyKey.atk));
                    break;
            }
        }

        public
            override string localizedDetail(int level, Property property)
        {
            if (actionDetail2 < 1 || actionDetail2 > 4)
                return base.localizedDetail(level, property);

            return Utils.JavaFormat(Utils.GetString("Restore_s1_s2_s3_per_second_for_s4_sec"),
                targetParameter.buildTargetClause(),
                buildExpression(level, property),
                regenerationType == RegenerationType.tp || regenerationType == RegenerationType.tpSecond ? Utils.GetString("TP") : Utils.GetString("HP"),
                buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property));
        }
    }
}
