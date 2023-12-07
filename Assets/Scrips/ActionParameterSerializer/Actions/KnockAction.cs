namespace ActionParameterSerializer.Actions
{
    public class KnockAction : ActionParameter
    {

        public enum KnockType
        {
            unknown = 0,
            upDown = 1,
            up = 2,
            back = 3,
            moveTarget = 4,
            moveTargetParaboric = 5,
            backLimited = 6,
            dragForwardCaster = 8,
            knockBackGiveValue = 9
        }

        private KnockType knockType;

        public
            override void childInit()
        {
            knockType = (KnockType)(actionDetail1);
            actionValues.Add(new ActionValue(actionValue1, actionValue2, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (knockType)
            {
                case KnockType.upDown:
                    return Utils.JavaFormat(Utils.GetString("Knock_s1_up_d2"), targetParameter.buildTargetClause(), (int)actionValue1.value);
                case KnockType.back:
                case KnockType.backLimited:
                    if (actionValue1.value >= 0)
                    {
                        return Utils.JavaFormat(Utils.GetString("Knock_s1_away_d2"), targetParameter.buildTargetClause(), (int)actionValue1.value);
                    }
                    else
                    {
                        return Utils.JavaFormat(Utils.GetString("Draw_s1_toward_self_d2"), targetParameter.buildTargetClause(), (int)-actionValue1.value);
                    }

                case KnockType.dragForwardCaster:
                    return Utils.JavaFormat(Utils.GetString("drag_s1_to_a_position_s2_forward_of_the_caster"), targetParameter.buildTargetClause(), (int)actionValue1.value);
                case KnockType.knockBackGiveValue:
                    return Utils.JavaFormat(Utils.GetString("Knock_s1_away_s2"), targetParameter.buildTargetClause(), buildExpression(level, property));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}
