using System;
using System.Collections.Generic;
using System.Diagnostics;
using Elements.Battle;
using System.Collections;
using UnityEngine;
using Org.BouncyCastle.Asn1.X509;
using PCRCaculator.Guild;

namespace Elements
{
    public class AllUnitSealDamageCutData : ISingletonField // TypeDefIndex: 18752
    {
        public GameObject EffectPrefab { get; set; }
        public GameObject EffectPrefabLeft { get; set; }
        public UnitCtrl Source { get; set; }
        public eStateIconType IconType { get; set; }
        public int Priority { get; set; }
        public double DamageCutRate { get; set; }
        public int StartCount { get; set; }
        public float Duration { get; set; }
        public List<ActionParameter> TargetActions { get; set; }
        public Skill Skill { get; set; }
        // protected IBattleEffectPool battleEffectPool { get; }
        // private IBattleManagerForUnitActionController battleManager { get; }

        // RVA: 0x30BAE30 Offset: 0x30BAE30 VA: 0x30BAE30
        // public static void StaticRelease()
        // {
            // staticBattleManager = null;
            // staticBattleEffectPool = null;
            // staticBattleLog = null;
        // }

        // RVA: 0x30BAEEC Offset: 0x30BAEEC VA: 0x30BAEEC
        public void Start()
        {
            var sealDictionary = Source.SealDictionary;

            if (!sealDictionary.ContainsKey(IconType))
            {
                var sealData = new SealData();
                sealData.Max = StartCount;
                sealData.DisplayCount = true;

                sealDictionary[IconType] = sealData;
            }

            var currentSealData = sealDictionary[IconType];

            if (currentSealData.GetCurrentCount() == 0)
            {
                Source.OnChangeState?.Invoke(Source, IconType, true);
                // Source.NoSkipOnChangeAbnormalState?.Invoke(Source, IconType, true, 0.0, "减伤");
            }
            currentSealData.AddSeal(Duration, Source, IconType, StartCount);
        }

        // RVA: 0x30BB088 Offset: 0x30BB088 VA: 0x30BB088
        public void Exec(UnitCtrl _target, UnitCtrl _effectTarget)
        {
            IEnumerator routine = null;
            Action callback = () =>
            {
                // effect play ignore

                if (Source != null && _target != null)
                {
                    // it seems exec no matter multipart or not
                    // if (_target.IsPartsBoss)
                    // {
                        var smallestParts = _target.GetIndexSmallestParts();
                        Source.ExecSkillWithTargetPartsAndActionList(Skill, smallestParts, TargetActions);
                    // }
                }
            };
            if (_target.CurrentState == UnitCtrl.ActionState.SKILL_1)
            {
                routine = waitUnionBurstEnd(_target, callback);
            }
            else
            {
                routine = waitFrame(callback);
            }
            _target.AppendCoroutine(routine, ePauseType.SYSTEM, Source);
        }

        // RVA: 0x30BB2DC Offset: 0x30BB2DC VA: 0x30BB2DC
        private IEnumerator waitFrame(Action _callback)
        {
            yield return null;
            _callback?.Invoke();
        }

        // RVA: 0x30BB204 Offset: 0x30BB204 VA: 0x30BB204
        private IEnumerator waitUnionBurstEnd(UnitCtrl _target, Action _callback)
        {
            while (_target.CurrentState == UnitCtrl.ActionState.SKILL_1)
            {
                yield return null;
            }
            _callback?.Invoke();
        }

        // RVA: 0x30BB3F4 Offset: 0x30BB3F4 VA: 0x30BB3F4
        public void Clear()
        {
            var sealData = Source.SealDictionary[IconType];
            sealData.RemoveSeal(sealData.Count, true);
        }
    }

    public class AllUnitSealDamageCutAction : ActionParameter // TypeDefIndex: 18755
    {
        private List<ActionParameter> targetActions { get; set; } = new List<ActionParameter>();

        // RVA: 0x30BB990 Offset: 0x30BB990 VA: 0x30BB990 Slot: 6
        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);

            Action<int> action = (int _detail) =>
            {
                if (_detail == 0)
                    return;
                var parameter = _skill.ActionParameters.Find((ActionParameter p) => p.ActionId == _detail);
                if (parameter != null)
                {
                    parameter.IgnoreTriggerCausedByTrigger = true;
                    targetActions.Add(parameter);
                }
            };
            action(ActionDetail1);
            action(ActionDetail2);
            action(ActionDetail3);
        }

        // RVA: 0x30BBB28 Offset: 0x30BBB28 VA: 0x30BBB28 Slot: 10
        public override void ReadyAction(UnitCtrl _source, UnitActionController _sourceActionController, Skill _skill)
        {
            base.ReadyAction(_source, _sourceActionController, _skill);
            // foreach(var action in targetActions)
            // {
            //     battleEffectPool set flag
            // }
        }

        // RVA: 0x30BBCEC Offset: 0x30BBCEC VA: 0x30BBCEC Slot: 8
        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary,
        Action<string> action = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary, action);
            if (_target == null)
                return;

            var owner = _target.Owner;
            if (_source != owner)
            {
                return;
            }

            // ignore effect list

            var battleManager = base.battleManager;
            var sealData = new AllUnitSealDamageCutData();
            sealData.Source = _source;
            sealData.IconType = (eStateIconType)BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_1]);
            sealData.DamageCutRate = _valueDictionary[eValueNumber.VALUE_3] / 100.0;
            sealData.StartCount = BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_2]);
            sealData.TargetActions = targetActions;
            // sealData.EffectPrefab = effectPrefab;
            // sealData.EffectPrefabLeft = effectPrefabLeft;
            sealData.Skill = _skill;
            sealData.Duration = (float)_valueDictionary[eValueNumber.VALUE_4];
            battleManager.ExecAllUnitSealDamageCut(sealData, _source.IsOther);
        }

        // RVA: 0x30BC068 Offset: 0x30BC068 VA: 0x30BC068 Slot: 9
        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
        }

    }


}
