using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class KnightGuardAction : ActionParameter
    {

        public enum GuardType
        {
            physics = 1,
            magic = 2
        }

        public GuardType guardType;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            base.childInit();
            guardType = (GuardType)((int)actionValue1.value);
            switch (guardType)
            {
                case GuardType.magic:
                    actionValues.Add(new ActionValue(actionValue4, actionValue5, PropertyKey.magicStr));
                    actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
                    break;
                case GuardType.physics:
                    actionValues.Add(new ActionValue(actionValue4, actionValue5, PropertyKey.atk));
                    actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
                    break;
            }
            durationValues.Add(new ActionValue(actionValue6, actionValue7, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("When_s1_HP_reaches_0_restore_s2_HP_once_in_next_s3_sec"),
                targetParameter.buildTargetClause(),
                buildExpression(level, property),
                buildExpression(level, durationValues, null, property));
        }
    }
}
