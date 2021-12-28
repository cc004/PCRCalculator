namespace Elements
{
    using Elements.Battle;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class HealDownAction : ActionParameter
    {
        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());
            if (BattleManager.Random((float) 0f, (float) 1f,
                new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 11, (float)pp)) < BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
            {
                base.AppendIsAlreadyExeced(_target.Owner, _num);
                Dictionary<BasePartsData, FloatWithEx> dictionary1 = new Dictionary<BasePartsData, FloatWithEx> {
                    { 
                        _target,
                        0f
                    }
                };
                _target.Owner.SetBuffParam(UnitCtrl.BuffParamKind.NUM, dictionary1, 0.5f, _skill.SkillId, _source, true, eEffectType.COMMON, false, false);
                _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.HEAL_DOWN, (base.AbnormalStateFieldAction == null) ? (float)_valueDictionary[eValueNumber.VALUE_3] : 9999f, this, _skill, Mathf.Max(_valueDictionary[eValueNumber.VALUE_1], 0f), 0f, false, false, 1f);
            }
            else
            {
                ActionExecedData data = base.AlreadyExecedData[_target.Owner][_num];
                if (data.ExecedPartsNumber == data.TargetPartsNumber)
                {
                    if (data.TargetPartsNumber == 1)
                    {
                        _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SLIP_DAMAGE, eDamageEffectType.NORMAL, _target, 1f);
                    }
                    else
                    {
                        _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SLIP_DAMAGE, eDamageEffectType.NORMAL, null, 1f);
                    }
                }
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            base.Value[eValueNumber.VALUE_1] = (float) (base.MasterData.action_value_1 + (base.MasterData.action_value_2 * _level));
            base.Value[eValueNumber.VALUE_3] = (float) (base.MasterData.action_value_3 + (base.MasterData.action_value_4 * _level));
        }
    }
}
