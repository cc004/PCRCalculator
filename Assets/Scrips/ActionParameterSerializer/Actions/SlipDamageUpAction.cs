using static ActionParameterSerializer.Actions.EveryAttackCriticalAction;

namespace ActionParameterSerializer.Actions
{
    public class SlipDamageUpAction : ActionParameter
    {
        public override void childInit()
        {
            base.childInit();
        }

        public enum eSlipDamageType
        {
            NO_EFFECT,
            POISONE,
            BURN,
            CURSE,
            VENOM,
            HEX,
            COMPENSATION,
            POISON2,
            CURSE2,
            BLACK_FRAME,
            WORLD_LIGHTNING
        }

        public override string localizedDetail(int level, Property property)
        {
            if (actionValue3.value == -1)
            {
                return Utils.JavaFormat(Utils.GetString("all_slip_damage_up"),
                        (int)(1 + actionValue1.value / 100),
                        actionValue7.value,
                        actionValue2.value);
            }
            else
            {
                string slipDamageType = "";
                if (actionValue3.value != -1)
                {
                    slipDamageType += ((eSlipDamageType)actionValue3.value).ToString();
                }
                if (actionValue4.value != -1)
                {
                    slipDamageType += ", ";
                    slipDamageType += ((eSlipDamageType)actionValue4.value).ToString();
                }
                if (actionValue5.value != -1)
                {
                    slipDamageType += ", ";
                    slipDamageType += ((eSlipDamageType)actionValue5.value).ToString();
                }
                if (actionValue6.value != -1)
                {
                    slipDamageType += ", ";
                    slipDamageType += ((eSlipDamageType)actionValue6.value).ToString();
                }
                return Utils.JavaFormat(Utils.GetString("special_slip_damage_up"),
                                slipDamageType,
                                (int)(1 + actionValue1.value / 100),
                                actionValue7.value,
                                actionValue2.value);
            }
        }
    }
}