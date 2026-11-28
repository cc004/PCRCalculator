namespace ActionParameterSerializer.Actions
{
    public class AutoRevivalAction : ActionParameter
    {
        public override void childInit()
        {
            actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
            if (actionValue1.value == 1)
            {
                PropertyKey key = actionDetail1 == (int)ClassModifier.physical ? PropertyKey.atk : PropertyKey.magicStr;
                actionValues.Add(new ActionValue(actionValue4.value, 0, eActionValue.VALUE4, eActionValue.VALUE4, key));
            }
        }

        public override string localizedDetail(int level, Property property)
        {
            if (actionValue1.value != 1 && actionValue1.value != 2)
                return base.localizedDetail(level, property);

            string restore = buildExpression(level, RoundingMode.UNNECESSARY, property);
            string hp = actionValue1.value == 2
                ? Utils.JavaFormat(Utils.GetString("Restore_max_HP_percent_s"), restore)
                : Utils.JavaFormat(Utils.GetString("auto_revival_heal"), restore);
            string detail = Utils.JavaFormat(Utils.GetString("Auto_revival_on_lethal_damage_s1_s2_s3"),
                actionValue5.valueString(), actionValue6.valueString(), hp);
            if (actionDetail3 == 99)
                detail += Utils.GetString("auto_revival_unbeatable") + "。";

            return detail;
        }
    }
}
