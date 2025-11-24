using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elements.Battle;
using System.Collections;
using UnityEngine;
using Org.BouncyCastle.Asn1.X509;
using PCRCaculator.Guild;
using System.Globalization;

namespace Elements
{
    public class SealBuffDebuffAction : ActionParameter // TypeDefIndex: 19048
    {
        // RVA: 0x3107694 Offset: 0x3107694 VA: 0x3107694 Slot: 6
        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        // RVA: 0x3107728 Offset: 0x3107728 VA: 0x3107728 Slot: 8
        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> action = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary, action);

            AppendIsAlreadyExeced(_target.Owner, _num);

            var paramKind = BuffDebuffAction.GetChangeParamKind(ActionDetail1);

            var partsDataDict = new Dictionary<BasePartsData, LongWithEx>();

            if (_target.Owner.IsPartsBoss && !AbnormalStateDefine.IS_EFFECT_BOSS_BODY_ONLY[paramKind])
            {
                foreach (var partsData in _target.Owner.BossPartsListForBattle)
                {
                    var value2 = _valueDictionary[eValueNumber.VALUE_2];
                    var value1 = BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_1]);

                    var param = BuffDebuffAction.CalculateBuffDebuffParam(partsData, value2, (BuffDebuffAction.eChangeParameterType)value1, paramKind, ActionDetail1 % 10 == 1);

                    partsDataDict.Add(partsData, param);
                }
            }
            else
            {
                var value2 = _valueDictionary[eValueNumber.VALUE_2];
                var value1 = BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_1]);

                var param = BuffDebuffAction.CalculateBuffDebuffParam(_target, value2, (BuffDebuffAction.eChangeParameterType)value1, paramKind, ActionDetail1 % 10 == 1);

                partsDataDict.Add(_target.Owner.DummyPartsData, param);
            }

            var sealBuffDebuffData = new SealBuffDebuffData();
            sealBuffDebuffData.Source = _source;
            sealBuffDebuffData.Target = _target.Owner;
            sealBuffDebuffData.TargetParameter = paramKind;
            sealBuffDebuffData.ValuePerSealDictionary = partsDataDict;
            sealBuffDebuffData.Duration = _valueDictionary[eValueNumber.VALUE_4];

            var sealDataDict = _target.Owner.SealDictionary;

            if (sealDataDict.ContainsKey((eStateIconType)ActionDetail2))
            {
                var sealData = sealDataDict[(eStateIconType)ActionDetail2];

                if (sealData.SealBuffDebuffDataDictionary != null)
                {
                    if (sealData.SealBuffDebuffDataDictionary.ContainsKey(ActionDetail1))
                    {
                        sealData.SealBuffDebuffDataDictionary[ActionDetail1].DeleteValue();
                    }
                }
                else
                {
                    sealData.SealBuffDebuffDataDictionary = new Dictionary<int, SealBuffDebuffData>();
                }

                sealData.SealBuffDebuffDataDictionary[ActionDetail1] = sealBuffDebuffData;
                sealBuffDebuffData.ParentSealData = sealData;
            }
            else
            {
                var sealData = new SealData();
                sealData.SealBuffDebuffDataDictionary = new Dictionary<int, SealBuffDebuffData>() {
                    { ActionDetail1, sealBuffDebuffData }
                };
                sealData.DisplayCount = true;

                sealDataDict[(eStateIconType)ActionDetail2] = sealData;

                sealBuffDebuffData.ParentSealData = sealData;
            }

            var parentSeal = sealDataDict[(eStateIconType)ActionDetail2];
            int displayCount = parentSeal.GetCurrentCount();

            sealBuffDebuffData.UpdateValue(displayCount);
            sealBuffDebuffData.StartTimer();
        }

        // RVA: 0x3107ED0 Offset: 0x3107ED0 VA: 0x3107ED0 Slot: 9
        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);

            Value[eValueNumber.VALUE_2] = MasterData.action_value_2 + MasterData.action_value_3 * _level;
        }
    }

}
