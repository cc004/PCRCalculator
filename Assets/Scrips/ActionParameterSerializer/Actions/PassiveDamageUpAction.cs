namespace ActionParameterSerializer.Actions
{
    internal class PassiveDamageUpAction : ActionParameter
    {
        public double debuffDamageUpValue;
        public double debuffDamageUpLimitValue;
        public double debuffDamageUpTimer;
        public eCountType countType;
        public eEffectType effectType;

        public
            override void childInit()
        {
            debuffDamageUpLimitValue = actionValue2.value;
            debuffDamageUpValue = actionValue1.value;
            debuffDamageUpTimer = actionValue3.value;
            countType = (eCountType)(actionDetail1);
            effectType = (eEffectType)(actionDetail2);
            actionValues.Add(new ActionValue(actionValue3, actionValue4, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (countType)
            {
                case eCountType.Debuff:
                    return Utils.JavaFormat(Utils.GetString("Make_s1_damage_changes_into_s234_times_caps_to_s5_times_dur_s6_sec"),
                        targetParameter.buildTargetClause(),
                        effectType.description(),
                        Utils.roundIfNeed(debuffDamageUpValue),
                        countType.description(),
                        Utils.roundIfNeed(debuffDamageUpLimitValue),
                        buildExpression(level, property)
                    );
                default:
                    return localizedDetail(level, property);
            }
        }

        public enum eCountType
        {
            Unknown = -1,
            Debuff = 1
        }

        public enum eEffectType
        {
            Unknown = -1,
            Add = 1,
            Subtract = 2
        }
    }
}
