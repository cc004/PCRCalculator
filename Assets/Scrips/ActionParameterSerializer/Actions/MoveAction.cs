namespace ActionParameterSerializer.Actions
{
    public class MoveAction : ActionParameter
    {

        public enum MoveType
        {
            unknown = 0,
            targetReturn = 1,
            absoluteReturn = 2,
            target = 3,
            absolute = 4,
            targetByVelocity = 5,
            absoluteByVelocity = 6,
            absoluteWithoutDirection = 7
        }

        private MoveType moveType;

        public
            override void childInit()
        {
            moveType = (MoveType)actionDetail1;
        }
        public
            override string localizedDetail(int level, Property property)
        {
            switch (moveType)
            {
                case MoveType.targetReturn:
                    return Utils.JavaFormat(Utils.GetString("Change_self_position_to_s_then_return"), targetParameter.buildTargetClause());
                case MoveType.absoluteReturn:
                    if (actionValue1.value > 0)
                    {
                        return Utils.JavaFormat(Utils.GetString("Change_self_position_s_forward_then_return"), Utils.roundDownDouble(actionValue1.value));
                    }
                    else
                    {
                        return Utils.JavaFormat(Utils.GetString("Change_self_position_s_backward_then_return"), Utils.roundDownDouble(-actionValue1.value));
                    }

                case MoveType.target:
                    return Utils.JavaFormat(Utils.GetString("Change_self_position_to_s"), targetParameter.buildTargetClause());
                case MoveType.absolute:
                case MoveType.absoluteWithoutDirection:
                    if (actionValue1.value > 0)
                    {
                        return Utils.JavaFormat(Utils.GetString("Change_self_position_s_forward"), Utils.roundDownDouble(actionValue1.value));
                    }
                    else
                    {
                        return Utils.JavaFormat(Utils.GetString("Change_self_position_s_backward"), Utils.roundDownDouble(-actionValue1.value));
                    }

                case MoveType.targetByVelocity:
                    if (actionValue1.value > 0)
                    {
                        return Utils.JavaFormat(Utils.GetString("Move_to_s1_in_front_of_s2_with_velocity_s3_sec"), Utils.roundDownDouble(actionValue1.value), targetParameter.buildTargetClause(), actionValue2.valueString());
                    }
                    else
                    {
                        return Utils.JavaFormat(Utils.GetString("Move_to_s1_behind_of_s2_with_velocity_s3_sec"), Utils.roundDownDouble(-actionValue1.value), targetParameter.buildTargetClause(), actionValue2.valueString());
                    }

                case MoveType.absoluteByVelocity:
                    if (actionValue1.value > 0)
                    {
                        return Utils.JavaFormat(Utils.GetString("Move_forward_s1_with_velocity_s2_sec"), Utils.roundDownDouble(actionValue1.value), actionValue2.valueString());
                    }
                    else
                    {
                        return Utils.JavaFormat(Utils.GetString("Move_backward_s1_with_velocity_s2_sec"), Utils.roundDownDouble(-actionValue1.value), actionValue2.valueString());
                    }

                default:
                    return base.localizedDetail(level, property);
            }
        }

    }
}
