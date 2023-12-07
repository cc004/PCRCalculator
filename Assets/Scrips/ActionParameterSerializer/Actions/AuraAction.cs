using System.Collections.Generic;

namespace ActionParameterSerializer.Actions
{
    public class AuraAction : ActionParameter
    {

        public enum AuraType
        {
            none = -1,
            atk = 1,
            def = 2,
            magicStr = 3,
            magicDef = 4,
            dodge = 5,
            physicalCritical = 6,
            magicalCritical = 7,
            energyRecoverRate = 8,
            lifeSteal = 9,
            moveSpeed = 10,
            physicalCriticalDamage = 11,
            magicalCriticalDamage = 12,
            accuracy = 13,
            receivedCriticalDamage = 14,
            receivedDamage = 15,
            receivedPhysicalDamage = 16,
            receivedMagicalDamage = 17,
            physicalDamageUpPercent = 18,
            magicDamageUpPercent = 19,
            maxHP = 100
        }

        public enum AuraActionType
        {
            raise,
            reduce
        }

        public enum BreakType
        {
            Unknown = -1,
            Normal = 1,
            Break = 2
        }

        public PercentModifier percentModifier;
        public List<ActionValue> durationValues = new();
        public AuraActionType auraActionType;
        public AuraType auraType;
        public BreakType breakType;
        public bool isConstant = false;

        public
            override void childInit()
        {
            percentModifier = (int)actionValue1.value == 2 ? PercentModifier.percent : PercentModifier.number;
            actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
            durationValues.Add(new ActionValue(actionValue4, actionValue5, null));
            auraActionType = (actionDetail1 % 10 == 1) ? AuraActionType.reduce : AuraActionType.raise;
            if (actionDetail1 == 1)
            {
                auraType = AuraType.maxHP;
            }
            else if (actionDetail1 >= 1000)
            {
                auraType = (AuraType)(actionDetail1 % 1000 / 10);
                isConstant = true;
            }
            else
            {
                auraType = (AuraType)(actionDetail1 / 10);
            }
            breakType = (BreakType)(actionDetail2);

            switch (auraType)
            {
                case AuraType.receivedCriticalDamage:
                case AuraType.receivedMagicalDamage:
                case AuraType.receivedPhysicalDamage:
                case AuraType.receivedDamage:
                    auraActionType = auraActionType.toggle();
                    percentModifier = PercentModifier.percent;
                    break;
                case AuraType.physicalDamageUpPercent:
                case AuraType.magicDamageUpPercent:
                    percentModifier = PercentModifier.percent;
                    break;
            }
        }

        public
            override string localizedDetail(int level, Property property)
        {
            string r = buildExpression(level, RoundingMode.UP, property);
            if (percentModifier == PercentModifier.percent && UserSettings.get().getExpression() != UserSettings.EXPRESSION_VALUE)
            {
                r = Utils.JavaFormat("(%s)", r);
            }
            switch (breakType)
            {
                case BreakType.Break:
                    return Utils.JavaFormat(Utils.GetString("s1_s2_s3_s4_s5_during_break"),
                        auraActionType.description(), targetParameter.buildTargetClause(), r, percentModifier.description(), auraType.description());
                default:
                {
                    return Utils.JavaFormat(Utils.GetString("s1_s2_s3_s4_s5_for_s6_sec"),
                        auraActionType.description(),
                        targetParameter.buildTargetClause(),
                        r,
                        percentModifier.description(),
                        auraType.description(),
                        buildExpression(level, durationValues, RoundingMode.UNNECESSARY, property),
                        isConstant ? Utils.JavaFormat(Utils.GetString("this_buff_is_constant")) : "");
                }
            }
        }
    }
}
