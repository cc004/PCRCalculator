using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{


    public class AddAbnormalEffectTimeData // TypeDefIndex: 21472
    {
        public enum eValueType // TypeDefIndex: 21471
        {
            // Fields
            FIXED = 0,
            PERCENTAGE = 1
        }

        // Properties
        public AddAbnormalEffectTimeData.eValueType ValueType { get; set; }
        public float Value { get; set; }

        // RVA: 0x3555CBC Offset: 0x3555CBC VA: 0x3555CBC
        public float GetResultTime(float _value)
        {
            switch (ValueType)
            {
                case eValueType.FIXED:
                    return Value + _value;
                case eValueType.PERCENTAGE:
                    return (Value + 100f) * _value / 100f;
                default:
                    return _value;
            }
        }
    }


    public class AddAbnormalEffectTimeAction : ActionParameter 
    {
        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController) 
        { 
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        // RVA: 0x3556118 Offset: 0x3556118 VA: 0x3556118
        private List<UnitCtrl.eAbnormalState> createTargetStateList(ValueDictionary _valueDictionary)
        {
            List<UnitCtrl.eAbnormalState> result = new List<UnitCtrl.eAbnormalState>();

            Action<DoubleWithEx> addTargetState = value =>
            {
                if (BattleUtil.Approximately(value, -1.0))
                {
                    return;
                }

                int targetValue = BattleUtil.DoubleToInt(value);
                switch ((eActionType)ActionDetail1)
                {
                    case eActionType.CHANGE_SPEED:
                        result.Add(ChangeSpeedAction.AbnormalStateDic[(ChangeSpeedAction.eChangeSpeedType)targetValue]);
                        break;
                    case eActionType.SLIP_DAMAGE:
                        result.Add(SlipDamageAction.abnormalStateDic[(SlipDamageAction.eSlipDamageType)targetValue]);
                        break;
                    case eActionType.CHARM:
                        switch (targetValue)
                        {
                            case 0:
                                result.Add(UnitCtrl.eAbnormalState.CONVERT);
                                break;
                            case 1:
                                result.Add(UnitCtrl.eAbnormalState.CONFUSION);
                                break;
                            case 2:
                                result.Add(UnitCtrl.eAbnormalState.CONFUSION2);
                                break;
                        }
                        break;
                    case eActionType.BLIND:
                        result.Add(BlindAction.AbnormalStateDic[(BlindAction.eBlindType)targetValue]);
                        break;
                    case eActionType.SILENCE:
                        switch (targetValue)
                        {
                            case 0:
                                result.Add(UnitCtrl.eAbnormalState.SILENCE);
                                break;
                            case 1:
                                result.Add(UnitCtrl.eAbnormalState.UB_SILENCE);
                                break;
                        }
                        break;
                }
            };

            addTargetState(_valueDictionary[eValueNumber.VALUE_4]);
            addTargetState(_valueDictionary[eValueNumber.VALUE_5]);
            addTargetState(_valueDictionary[eValueNumber.VALUE_6]);
            addTargetState(_valueDictionary[eValueNumber.VALUE_7]);

            return result;
        }


        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> callBack = null) 
        { 
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary, callBack);
            List<UnitCtrl.eAbnormalState> targetStateList = createTargetStateList(_valueDictionary);
            foreach (UnitCtrl.eAbnormalState abnormalState in targetStateList)
            {
                AddAbnormalEffectTimeData data = new AddAbnormalEffectTimeData
                {
                    Value = (float)_valueDictionary[eValueNumber.VALUE_2],
                    ValueType = (AddAbnormalEffectTimeData.eValueType)BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_1])
                };
                _target.Owner.AddAbnormalEffectTimeDataDictionary[abnormalState] = data;
            }
        }

        public override void SetLevel(float _level) 
        { 
            base.SetLevel(_level);

            Value[eValueNumber.VALUE_2] = MasterData.action_value_2 + MasterData.action_value_3 * _level;
        }
    }
}
