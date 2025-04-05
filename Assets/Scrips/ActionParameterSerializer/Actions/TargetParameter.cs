using System;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace ActionParameterSerializer.Actions
{
    public class TargetParameter
    {

        public TargetAssignment targetAssignment;
        public TargetNumber targetNumber;
        public int rawTargetType;
        public TargetType targetType;
        public TargetRange targetRange;
        public DirectionType direction;
        public TargetCount targetCount;

        private readonly SkillAction dependAction;

        public TargetParameter(int targetAssignment, int targetNumber, int targetType, int targetRange, int targetArea, int targetCount, SkillAction dependAction)
        {
            this.targetAssignment = (TargetAssignment)(targetAssignment);
            this.targetNumber = (TargetNumber)(targetNumber);
            rawTargetType = targetType;
            this.targetType = (TargetType)(targetType);
            this.targetRange = new TargetRange(targetRange);
            direction = (DirectionType)(targetArea);
            this.targetCount = (TargetCount)(targetCount);
            if (!Enum.IsDefined(typeof(TargetCount), this.targetCount)) this.targetCount = TargetCount.all;
            this.dependAction = dependAction;
            setBooleans();
        }

        private bool hasRelationPhrase;
        private bool hasCountPhrase;
        private bool hasRangePhrase;
        private bool hasNthModifier;
        private bool hasDirectionPhrase;
        private bool hasTargetType;
        private bool hasDependAction()
        {
            return dependAction != null && (
                dependAction.getActionId() != 0
                && targetType != TargetType.absolute
                && dependAction.parameter.actionType != ActionType.chooseArea
            );
        }

        private void setBooleans()
        {
            hasRelationPhrase = targetType != TargetType.self
                                && targetType != TargetType.absolute;
            hasCountPhrase = targetType != TargetType.self
                             && !(targetType == TargetType.none && targetCount == TargetCount.zero);

            hasRangePhrase = targetRange.rangeType == TargetRange.FINITE;

            hasNthModifier = targetNumber == TargetNumber.second
                             || targetNumber == TargetNumber.third
                             || targetNumber == TargetNumber.fourth
                             || targetNumber == TargetNumber.fifth;
            hasDirectionPhrase = direction == DirectionType.front
                                 && (hasRangePhrase || targetCount == TargetCount.all);
            hasTargetType = !(targetType.exclusiveWithAll() == ExclusiveAllType.exclusive && targetCount == TargetCount.all);
        }


        public string buildTargetClause(bool anyOfModifier)
        {
            if (targetCount.pluralModifier() == PluralModifier.many && anyOfModifier)
            {
                return Utils.JavaFormat(Utils.GetString("any_of_s"), buildTargetClause());
            }
            else
            {
                return buildTargetClause();
            }
        }

        public string buildTargetClause()
        {
            if (hasDependAction())
            {
                if (dependAction.parameter.actionType == ActionType.damage)
                {
                    return Utils.JavaFormat(Utils.GetString("targets_those_damaged_by_effect_d"), dependAction.getActionId() % 100);
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("targets_of_effect_d"), dependAction.getActionId() % 100);
                }
            }
            else if (!hasRelationPhrase)
            {
                return targetType.description();
            }
            else if (!hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase)
            {
                return Utils.JavaFormat(Utils.GetString("targets_of_last_effect"));
            }
            else if (hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
            {
                if (targetCount == TargetCount.all)
                {
                    if (targetType.exclusiveWithAll() == ExclusiveAllType.exclusive)
                    {
                        return Utils.JavaFormat(Utils.GetString("all_s_targets"), targetAssignment.description());
                    }
                    else if (targetType.exclusiveWithAll() == ExclusiveAllType.not)
                    {
                        return Utils.JavaFormat(Utils.GetString("all_s_s_targets"), targetAssignment.description(), targetType.description());
                    }
                    else if (targetType.exclusiveWithAll() == ExclusiveAllType.halfExclusive)
                    {
                        return Utils.JavaFormat(Utils.GetString("all_s_targets"), targetAssignment.description()) + Utils.JavaFormat(Utils.GetString("except_self"));
                    }
                }
                else if (targetCount == TargetCount.one && targetType.ignoresOne())
                {
                    return Utils.JavaFormat(Utils.GetString("s_s_target"), targetType.description(), targetAssignment.description());
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("s_s_s"), targetType.description(), targetAssignment.description(), targetCount.description());
                }
            }
            else if (hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase && hasDirectionPhrase && targetType.exclusiveWithAll() == ExclusiveAllType.exclusive)
            {
                switch (targetAssignment)
                {
                    case TargetAssignment.enemy:
                        return Utils.JavaFormat(Utils.GetString("all_front_enemy_targets"));
                    case TargetAssignment.friendly:
                        return Utils.JavaFormat(Utils.GetString("all_front_including_self_friendly_targets"));
                    default:
                        return Utils.JavaFormat(Utils.GetString("all_front_targets"));
                }
            }
            else if (hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase && hasDirectionPhrase && targetType.exclusiveWithAll() == ExclusiveAllType.not)
            {
                switch (targetAssignment)
                {
                    case TargetAssignment.enemy:
                        return Utils.JavaFormat(Utils.GetString("all_front_s_enemy_targets"), targetType.description());
                    case TargetAssignment.friendly:
                        return Utils.JavaFormat(Utils.GetString("all_front_including_self_s_friendly_targets"), targetType.description());
                    default:
                        return Utils.JavaFormat(Utils.GetString("all_front_s_targets"), targetType.description());
                }
            }
            else if (hasCountPhrase && !hasNthModifier && !hasRangePhrase && hasRelationPhrase && hasDirectionPhrase && targetType.exclusiveWithAll() == ExclusiveAllType.halfExclusive)
            {
                switch (targetAssignment)
                {
                    case TargetAssignment.enemy:
                        return Utils.JavaFormat(Utils.GetString("all_front_enemy_targets")) + Utils.JavaFormat(Utils.GetString("except_self"));
                    case TargetAssignment.friendly:
                        return Utils.JavaFormat(Utils.GetString("all_front_including_self_friendly_targets"));
                    default:
                        return Utils.JavaFormat(Utils.GetString("all_front_targets")) + Utils.JavaFormat(Utils.GetString("except_self"));
                }
            }
            else if (!hasCountPhrase && !hasNthModifier && hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
            {
                return Utils.JavaFormat(Utils.GetString("s1_targets_in_range_d2"), targetAssignment.description(), targetRange.rawRange);
            }
            else if (!hasCountPhrase && !hasNthModifier && hasRangePhrase && hasRelationPhrase && hasDirectionPhrase)
            {
                return Utils.JavaFormat(Utils.GetString("front_s1_targets_in_range_d2"), targetAssignment.description(), targetRange.rawRange);
            }
            else if (!hasCountPhrase && hasNthModifier && hasRangePhrase && hasRelationPhrase)
            {
                return Utils.JavaFormat(Utils.GetString("targets_of_last_effect"));
            }
            else if (hasCountPhrase && !hasNthModifier && hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
            {
                if (targetCount == TargetCount.all)
                {
                    if (targetType.exclusiveWithAll() == ExclusiveAllType.exclusive)
                    {
                        return Utils.JavaFormat(Utils.GetString("s1_targets_in_range_d2"), targetAssignment.description(), targetRange.rawRange);
                    }
                    else if (targetType.exclusiveWithAll() == ExclusiveAllType.not)
                    {
                        return Utils.JavaFormat(Utils.GetString("s1_s2_target_in_range_d3"), targetAssignment.description(), targetType.description(), targetRange.rawRange);
                    }
                    else if (targetType.exclusiveWithAll() == ExclusiveAllType.halfExclusive)
                    {
                        return Utils.JavaFormat(Utils.GetString("s1_targets_in_range_d2"), targetAssignment.description() + Utils.JavaFormat(Utils.GetString("except_self")), targetRange.rawRange);
                    }
                }
                else if (targetCount == TargetCount.one && targetType.ignoresOne())
                {
                    return Utils.JavaFormat(Utils.GetString("s1_s2_target_in_range_d3"), targetType.description(), targetAssignment.description(), targetRange.rawRange);
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("s1_s2_s3_in_range_d4"), targetType.description(), targetAssignment.description(), targetCount.description(), targetRange.rawRange);
                }
            }
            else if (hasCountPhrase && !hasNthModifier && hasRangePhrase && hasRelationPhrase && hasDirectionPhrase)
            {
                if (targetCount == TargetCount.all)
                {
                    if (targetType.exclusiveWithAll() == ExclusiveAllType.exclusive)
                    {
                        return Utils.JavaFormat(Utils.GetString("front_s1_targets_in_range_d2"), targetAssignment.description(), targetRange.rawRange);
                    }
                    else if (targetType.exclusiveWithAll() == ExclusiveAllType.not)
                    {
                        return Utils.JavaFormat(Utils.GetString("front_s1_s2_targets_in_range_d3"), targetAssignment.description(), targetType.description(), targetRange.rawRange);
                    }
                    else if (targetType.exclusiveWithAll() == ExclusiveAllType.halfExclusive)
                    {
                        return Utils.JavaFormat(Utils.GetString("front_s1_targets_in_range_d2"), targetAssignment.description() + Utils.JavaFormat(Utils.GetString("except_self")), targetRange.rawRange);
                    }
                }
                else if (targetCount == TargetCount.one && targetType.ignoresOne())
                {
                    return Utils.JavaFormat(Utils.GetString("s1_front_s2_target_in_range_d3"), targetType.description(), targetAssignment.description(), targetRange.rawRange);
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("s1_front_s2_s3_in_range_d4"), targetType.description(), targetAssignment.description(), targetCount.description(), targetRange.rawRange);
                }
            }
            else if (hasCountPhrase && hasNthModifier && !hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
            {
                if (targetCount == TargetCount.one && targetType.ignoresOne())
                {
                    return Utils.JavaFormat(Utils.GetString("s_s_target"), targetType.description(targetNumber, null), targetAssignment.description());
                }
                else
                {
                    string modifier = Utils.JavaFormat(Utils.GetString("s1_to_s2"), targetNumber.description(), getUntilNumber().description());
                    return Utils.JavaFormat(Utils.GetString("s_s_s"), targetType.description(targetNumber, modifier), targetAssignment.description(), targetCount.pluralModifier().description());
                }
            }
            else if (hasCountPhrase && hasNthModifier && !hasRangePhrase && hasRelationPhrase && hasDirectionPhrase)
            {
                string modifier = Utils.JavaFormat(Utils.GetString("s1_to_s2"), targetNumber.description(), getUntilNumber().description());
                return Utils.JavaFormat(Utils.GetString("s1_front_s2_s3"), targetType.description(targetNumber, modifier), targetAssignment.description(), targetCount.pluralModifier().description());
            }
            else if (hasCountPhrase && hasNthModifier && hasRangePhrase && hasRelationPhrase && !hasDirectionPhrase)
            {
                if (targetCount == TargetCount.one && targetType.ignoresOne())
                {
                    return Utils.JavaFormat(Utils.GetString("s1_s2_target_in_range_d3"), targetType.description(targetNumber, null), targetAssignment.description(), targetRange.rawRange);
                }
                else
                {
                    string modifier = Utils.JavaFormat(Utils.GetString("s1_to_s2"), targetNumber.description(), getUntilNumber().description());
                    return Utils.JavaFormat(Utils.GetString("s1_s2_s3_in_range_d4"), targetType.description(targetNumber, modifier), targetAssignment.description(), targetCount.pluralModifier().description(), targetRange.rawRange);
                }
            }
            else if (hasCountPhrase && hasNthModifier && hasRangePhrase && hasRelationPhrase && hasDirectionPhrase)
            {
                if (targetCount == TargetCount.one && targetType.ignoresOne())
                {
                    return Utils.JavaFormat(Utils.GetString("s1_front_s2_target_in_range_d3"), targetType.description(targetNumber, null), targetAssignment.description(), targetRange.rawRange);
                }
                else
                {
                    string modifier = Utils.JavaFormat(Utils.GetString("s1_to_s2"), targetNumber.description(), getUntilNumber().description());
                    return Utils.JavaFormat(Utils.GetString("s1_front_s2_s3_in_range_d4"), targetType.description(targetNumber, modifier), targetAssignment.description(), targetCount.pluralModifier().description(), targetRange.rawRange);
                }
            }
            return "";
        }

        private TargetNumber getUntilNumber()
        {
            TargetNumber tUntil = targetNumber + (int)targetCount;
            if (tUntil == TargetNumber.other || !Enum.IsDefined(typeof(TargetNumber), tUntil))
            {
                return TargetNumber.fifth;
            }
            else
            {
                return tUntil;
            }
        }

    }

    public enum PluralModifier
    {
        [Description("target")]
        one,
        [Description("targets")]
        many
    }

    public enum TargetCount
    {
        zero, one, two, three, four, all = 99
    }

    public enum TargetNumber
    {
        first,
        second,
        third,
        fourth,
        fifth,
        other
    }
    public enum ExclusiveAllType
    {
        not, exclusive, halfExclusive
    }

    public enum TargetAssignment
    {
        none = 0,
        enemy = 1,
        friendly = 2,
        all = 3
    }

    public class TargetRange
    {
        public const int ZERO = 0;
        public const int ALL = 1;
        public const int FINITE = 2;
        public const int INFINITE = 3;
        public const int UNKNOWN = 4;

        public int rawRange;
        public int rangeType;

        public TargetRange(int range)
        {
            if (range == -1)
            {
                rangeType = INFINITE;
            }
            else if (range == 0)
            {
                rangeType = ZERO;
            }
            else if (range > 0 && range < 2160)
            {
                rangeType = FINITE;
            }
            else if (range >= 2160)
            {
                rangeType = ALL;
                rawRange = 2160;
                return;
            }
            else
            {
                rangeType = UNKNOWN;
            }
            rawRange = range;
        }
    }


    public enum TargetType
    {
        unknown = -1,
        zero = 0,
        none = 1,
        random = 2,
        near = 3,
        far = 4,
        hpAscending = 5,
        hpDescending = 6,
        self = 7,
        randomOnce = 8,
        forward = 9,
        backward = 10,
        absolute = 11,
        tpDescending = 12,
        tpAscending = 13,
        atkDescending = 14,
        atkAscending = 15,
        magicSTRDescending = 16,
        magicSTRAscending = 17,
        summon = 18,
        tpReducing = 19,
        physics = 20,
        magic = 21,
        allSummonRandom = 22,
        selfSummonRandom = 23,
        boss = 24,
        hpAscendingOrNear = 25,
        hpDescendingOrNear = 26,
        tpDescendingOrNear = 27,
        tpAscendingOrNear = 28,
        atkDescendingOrNear = 29,
        atkAscendingOrNear = 30,
        magicSTRDescendingOrNear = 31,
        magicSTRAscendingOrNear = 32,
        shadow = 33,
        nearWithoutSelf = 34,
        hpDescendingOrNearForward = 35,
        hpAscendingOrNearForward = 36,
        tpDescendingOrMaxForward = 37,
        bothAtkDescending = 38,
        bothAtkAscending = 39,
        energyAscBackWithoutOwner = 41,
        parentTargetParts = 42,
        atkDecForwardWithoutOwner = 43,
        hpAscWithoutOwner,
        atkDefAscForward,
        magicDefAscForward,
        flightOnly
    }

    public enum DirectionType
    {
        front = 1,
        frontAndBack = 2,
        all = 3,
        FRONT_INCLUDE_FLIGHT = 4,
        FRONT_AND_BACK_INCLUDE_FLIGHT = 5,
        ALL_INCLUDE_FLIGHT = 6,
        FRONT_WITHOUT_SUMMON = 7,
        FRONT_AND_BACK_WITHOUT_SUMMON = 8,
        ALL_WITHOUT_SUMMON = 9,
    }
}