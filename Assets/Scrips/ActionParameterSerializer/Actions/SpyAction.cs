using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class SpyAction : ActionParameter
    {
        public enum CancelType
        {
            None = 0,
            Damaged = 1
        }

        public CancelType cancelType;
        public List<ActionValue> durationValues = new();

        public
            override void childInit()
        {
            base.childInit();
            cancelType = (CancelType)actionDetail2;
            durationValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (cancelType)
            {
                case CancelType.Damaged:
                    return Utils.JavaFormat(Utils.GetString("Make_s1_invisible_for_s2_cancels_on_taking_damage"),
                        targetParameter.buildTargetClause(),
                        buildExpression(level, actionValues, null, property));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
