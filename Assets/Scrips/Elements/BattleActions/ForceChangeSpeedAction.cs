using System;
using System.Collections.Generic;
using Elements.Battle;

namespace Elements
{
    public class ForceChangeSpeedAction : ActionParameter
    {
        public override void ExecActionOnStart(
            Skill _skill,
            UnitCtrl _source,
            UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        public override void ExecAction(
            UnitCtrl _source,
            BasePartsData _target,
            int _num,
            UnitActionController _sourceActionController,
            Skill _skill,
            float _starttime,
            Dictionary<int, bool> _enabledChildAction,
            ValueDictionary _valueDictionary,
            Action<string> action = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);

            if (_target == null || _target.Owner == null)
                return;

            UnitCtrl.eAbnormalState abnormalState = ChangeSpeedAction.AbnormalStateDic[(ChangeSpeedAction.eChangeSpeedType)ActionDetail1];
            UnitCtrl targetOwner = _target.Owner;
            ChangeSpeedAction.eChangeSpeedType actionDetail = (ChangeSpeedAction.eChangeSpeedType)ActionDetail1;

            if (ChangeSpeedAction.IsUnableAbnormalState(abnormalState) && targetOwner.ModeChangeUnableStateBarrier)
            {
                action?.Invoke($"目标处于MODECHANGE状态，技能{actionDetail.GetDescription()}无效！");
                return;
            }

            AppendIsAlreadyExeced(targetOwner, _num);

            float effectTime = AbnormalStateFieldAction == null ? (float)_valueDictionary[eValueNumber.VALUE_3] : 90f;
            targetOwner.SetAbnormalState(
                _source,
                abnormalState,
                effectTime,
                this,
                _skill,
                _valueDictionary[eValueNumber.VALUE_1],
                0f,
                _reduceEnergy: false,
                _isDamageRelease: ActionDetail2 == (int)ChangeSpeedAction.eCancelTriggerType.DAMAGED,
                _reduceEnergyRate: 1f,
                _showsIcon: _valueDictionary[eValueNumber.VALUE_5] == 0f,
                _isEnvironmentAbnormalState: false,
                _isIgnoreDebuffImmune: true);

            string describe = actionDetail.GetDescription() + ",值" + _valueDictionary[eValueNumber.VALUE_1] + ",持续时间：" + _valueDictionary[eValueNumber.VALUE_3] + "秒";
            if (ActionDetail2 == (int)ChangeSpeedAction.eCancelTriggerType.DAMAGED)
            {
                if (!targetOwner.OnDamageListForChangeSpeedDisableByAttack.ContainsKey(ActionId))
                {
                    targetOwner.OnDamageListForChangeSpeedDisableByAttack.Add(ActionId, byAttack =>
                    {
                        if (byAttack && targetOwner.IsAbnormalState(abnormalState))
                        {
                            targetOwner.DisableAbnormalStateById(abnormalState, ActionId, _isReleasedByDamage: true);
                        }
                    });
                }
                describe += ",受到伤害后取消";
            }

            action?.Invoke(describe);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = MasterData.action_value_1 + MasterData.action_value_2 * _level;
            Value[eValueNumber.VALUE_3] = MasterData.action_value_3 + MasterData.action_value_4 * _level;
        }

        public override bool IsIgnoreDebuffImmune()
        {
            return true;
        }
    }
}
