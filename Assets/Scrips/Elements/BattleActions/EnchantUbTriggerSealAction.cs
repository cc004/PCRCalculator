using System;
using System.Collections.Generic;
using Cute;

namespace Elements
{
    public class EnchantUbTriggerSealAction : ActionParameter
    {
        private ActionParameter targetAction;

        public override void ExecActionOnStart(
            Skill _skill,
            UnitCtrl _source,
            UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);

            if (ActionDetail1 == 0 || _skill == null)
                return;

            targetAction = _skill.ActionParameters.Find(action => action.ActionId == ActionDetail1);
            if (targetAction != null)
                targetAction.TriggerCall = false;
        }

        public override void ReadyAction(
            UnitCtrl _source,
            UnitActionController _sourceActionController,
            Skill _skill)
        {
            base.ReadyAction(_source, _sourceActionController, _skill);

            if (targetAction == null || _skill == null)
                return;

            targetAction.NoDelayExecByTriggerOnly = true;
            if (targetAction.ActionChildrenIndexes == null)
                return;

            foreach (int childIndex in targetAction.ActionChildrenIndexes)
            {
                _skill.ActionParameters[childIndex].NoDelayExecByTriggerOnly = true;
            }
        }

        public override void ExecAction(
            UnitCtrl _source,
            BasePartsData _target,
            int _num,
            UnitActionController _sourceActionController,
            Skill _skill,
            float _starttime,
            Dictionary<int, bool> _enabledChildAction,
            ValueDictionary _valueDictionary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);

            if (_target == null || targetAction == null)
                return;

            UnitCtrl targetUnit = _target.Owner;
            AppendIsAlreadyExeced(targetUnit, _num);

            eStateIconType iconType = (eStateIconType)BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_1]);
            if (targetUnit.SealDictionary.TryGetValue(iconType, out SealData oldSealData))
            {
                oldSealData.RemoveSeal(oldSealData.Count, true);
            }

            double rawCount = _valueDictionary[eValueNumber.VALUE_3];
            int sealCount = double.IsPositiveInfinity(rawCount) ? int.MinValue : BattleUtil.DoubleToInt(rawCount);
            SealData sealData = new SealData
            {
                Max = sealCount,
                DisplayCount = !double.IsPositiveInfinity(rawCount) && BattleUtil.DoubleToInt(rawCount) > 1
            };

            targetUnit.SealDictionary[iconType] = sealData;
            targetUnit.OnChangeState.Call(targetUnit, iconType, true);
            sealData.AddSeal((float)_valueDictionary[eValueNumber.VALUE_4], targetUnit, iconType, sealCount);

            targetUnit.UbTriggerSealActionDictionary[iconType] = () =>
            {
                if (_source == null || _source.IsDead)
                {
                    if (sealData.GetCurrentCount() >= 1)
                        sealData.RemoveSeal(sealData.Count, true);
                    return;
                }

                if (sealData.GetCurrentCount() == 0)
                    return;

                _source.ExecSkillWithTargetPartsAndActionList(_skill, _target, new List<ActionParameter> { targetAction });
                sealData.RemoveSeal(1, true);
            };
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
        }
    }
}
