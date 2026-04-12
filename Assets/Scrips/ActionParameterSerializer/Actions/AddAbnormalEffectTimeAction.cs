using System.Collections.Generic;
using System.Linq;
using Elements;

namespace ActionParameterSerializer.Actions
{
    public class AddAbnormalEffectTimeAction : ActionParameter
    {
        private readonly List<ActionValue> effectTimeValues = new();

        private enum ValueType
        {
            FIXED = 0,
            PERCENTAGE = 1
        }

        public override void childInit()
        {
            base.childInit();
            effectTimeValues.Add(new ActionValue(actionValue2, actionValue3, null));
        }

        public override string localizedDetail(int level, Property property)
        {
            List<string> targetStates = createTargetStateList();
            if (targetStates.Count == 0)
            {
                return base.localizedDetail(level, property);
            }

            string targetStateDescription = string.Join("、", targetStates);
            switch ((ValueType)(int)actionValue1.value)
            {
                case ValueType.FIXED:
                    return Utils.JavaFormat(
                        Utils.GetString("add_abnormal_effect_time_fixed"),
                        targetParameter.buildTargetClause(),
                        targetStateDescription,
                        buildExpression(level, effectTimeValues, RoundingMode.UNNECESSARY, property)
                    );
                case ValueType.PERCENTAGE:
                    return Utils.JavaFormat(
                        Utils.GetString("add_abnormal_effect_time_percentage"),
                        targetParameter.buildTargetClause(),
                        targetStateDescription,
                        buildPercentageExpression(level, property)
                    );
                default:
                    return base.localizedDetail(level, property);
            }
        }

        private string buildPercentageExpression(int level, Property property)
        {
            if (UserSettings.get().getExpression() == UserSettings.EXPRESSION_VALUE)
            {
                return Utils.roundIfNeed(actionValue2.value + actionValue3.value * level + 100);
            }

            return $"100 + ({buildExpression(level, effectTimeValues, RoundingMode.UNNECESSARY, property)})";
        }

        private List<string> createTargetStateList()
        {
            List<string> result = new List<string>();
            addTargetState(result, actionValue4.value);
            addTargetState(result, actionValue5.value);
            addTargetState(result, actionValue6.value);
            addTargetState(result, actionValue7.value);
            return result.Distinct().ToList();
        }

        private void addTargetState(List<string> result, double value)
        {
            if (value == -1)
            {
                return;
            }

            UnitCtrl.eAbnormalState? abnormalState = getTargetState((int)value);
            if (abnormalState.HasValue)
            {
                result.Add(abnormalState.Value.GetDescription());
            }
        }

        private UnitCtrl.eAbnormalState? getTargetState(int targetValue)
        {
            switch (actionDetail1)
            {
                case 8:
                    if (ChangeSpeedAction.AbnormalStateDic.TryGetValue((ChangeSpeedAction.eChangeSpeedType)targetValue, out UnitCtrl.eAbnormalState speedState))
                    {
                        return speedState;
                    }
                    break;
                case 9:
                    if (SlipDamageAction.abnormalStateDic.TryGetValue((SlipDamageAction.eSlipDamageType)targetValue, out UnitCtrl.eAbnormalState dotState))
                    {
                        return dotState;
                    }
                    break;
                case 11:
                    switch (targetValue)
                    {
                        case 0:
                            return UnitCtrl.eAbnormalState.CONVERT;
                        case 1:
                            return UnitCtrl.eAbnormalState.CONFUSION;
                        case 2:
                            return UnitCtrl.eAbnormalState.CONFUSION2;
                    }
                    break;
                case 12:
                    if (BlindAction.AbnormalStateDic.TryGetValue((BlindAction.eBlindType)targetValue, out UnitCtrl.eAbnormalState blindState))
                    {
                        return blindState;
                    }
                    break;
                case 13:
                    switch (targetValue)
                    {
                        case 0:
                            return UnitCtrl.eAbnormalState.SILENCE;
                        case 1:
                            return UnitCtrl.eAbnormalState.UB_SILENCE;
                    }
                    break;
            }

            return null;
        }
    }
}
