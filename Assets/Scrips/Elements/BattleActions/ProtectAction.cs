using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ProtectAction : ActionParameter 
    {
        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController) 
        { 
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary, Action<string> callBack = null) 
        { 
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary, callBack);

            if (_target == null)
                return;

            if (!UnityEngine.Object.Equals(_source, _target.Owner))
            {
                if (_source != null && _source.ProtectionSourceData != null && _source.ProtectionSourceData.Enable)
                {
                    _source.ProtectionSourceData.Disable();
                }

                GameObject effectPrefab = null;
                GameObject effectPrefabLeft = null;
                // if (this.ActionEffectList != null && this.ActionEffectList.Count > 0)
                // {
                //     effectPrefab = this.ActionEffectList[0]?.effectPrefab;
                //     effectPrefabLeft = this.ActionEffectList[0]?.effectPrefabLeft;
                // }

                var protectionData = new ProtectionData
                {
                    Target = _target.Owner,
                    Timer = _valueDictionary != null && _valueDictionary.TryGetValue(eValueNumber.VALUE_3, out var timerVal) ? (float)timerVal : 0f,
                    Counter = _valueDictionary != null && _valueDictionary.TryGetValue(eValueNumber.VALUE_1, out var counterVal)
                        ? (double.IsPositiveInfinity(counterVal) ? int.MinValue : (int)counterVal)
                        : -1,
                    IconType = (eStateIconType)this.ActionDetail1,
                    Enable = true,
                    ProtectionParts = _source.GetFirstParts(),
                    EffectPrefab = effectPrefab,
                    EffectPrefabLeft = effectPrefabLeft,
                    EffectMaxCount = _valueDictionary != null && _valueDictionary.TryGetValue(eValueNumber.VALUE_5, out var maxCountVal)
                        ? (double.IsPositiveInfinity(maxCountVal) ? int.MinValue : (int)maxCountVal)
                        : 0,
                    EffectDuration = _valueDictionary != null && _valueDictionary.TryGetValue(eValueNumber.VALUE_6, out var durationVal) ? (float)durationVal : 0f,
                    Source = _source,
                };

                _source.ProtectionSourceData = protectionData;

                if (_target.Owner != null)
                {
                    _target.Owner.EnchantProtection(_source.ProtectionSourceData);
                }
            }

        }

        public override void SetLevel(float _level) 
        { 
            base.SetLevel(_level);

            if (MasterData == null || Value == null)
                return;

            double v1 = MasterData.action_value_1;
            double v2 = MasterData.action_value_2;
            float value1 = (float)(v1 + v2 * _level);
            Value[eValueNumber.VALUE_1] = value1;

            double v3 = MasterData.action_value_3;
            double v4 = MasterData.action_value_4;
            float value2 = (float)(v3 + v4 * _level);
            Value[eValueNumber.VALUE_3] = value2;
        }
    }
}