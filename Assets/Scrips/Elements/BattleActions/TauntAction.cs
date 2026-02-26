using System;
using System.Collections;
using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
    public class TauntAction : ActionParameter
    {

        enum eTauntType // TypeDefIndex: 20506
        {
            NORMAL = 1,
        }

        private BasePartsData parts; // 0x1C8
        private static readonly Dictionary<TauntAction.eTauntType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<TauntAction.eTauntType, UnitCtrl.eAbnormalState>()
        {
            {
                TauntAction.eTauntType.NORMAL,
                UnitCtrl.eAbnormalState.TAUNT
            }
        };

        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            parts = _source.BossPartsListForBattle.Find(e => e.Index == _skill.ParameterTarget);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = MasterData.action_value_1 + MasterData.action_value_2 * _level;
        }


        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          ValueDictionary _valueDictionary,Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            UnitCtrl.eAbnormalState state = abnormalStateDic[(TauntAction.eTauntType)ActionDetail1];
            float pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target);
            float num = BattleManager.Random(0.0f, 1f, new RandomData(_source, _target.Owner, ActionId, 21, pp));

            if (!_source.IsAbnormalState(UnitCtrl.eAbnormalState.SPY))
            {
                if (num >= pp)
                {
                    var data = AlreadyExecedData[_target.Owner][_num];
                    if (data.ExecedPartsNumber == data.TargetPartsNumber)
                    {
                        if (data.ExecedPartsNumber == 1)
                        {
                            _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_TAUNT, eDamageEffectType.NORMAL, _target, 1);
                        }
                        else
                        {
                            _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_TAUNT, eDamageEffectType.NORMAL, null, 1);
                        }
                    }
                }
                else
                {
                    AppendIsAlreadyExeced(_target.Owner, _num);
                    if (_target.Owner.IsAbnormalState(UnitCtrl.eAbnormalStateCategory.TAUNT))
                    {
                        _target.Owner.TauntSource.Owner.TauntTargetList.Remove(_target.Owner);
                    }
                    _target.Owner.SetAbnormalState(_source, state, (float)_valueDictionary[eValueNumber.VALUE_1], this, _skill, 0, 0f, _reduceEnergy: false, _isDamageRelease: false, 1f, true);
                    var p_part = parts;
                    if (!_source.IsPartsBoss)
                    {
                        p_part = _source.DummyPartsData;
                    }
                    _target.Owner.TauntSource = p_part;
                    _source.TauntTargetList.Add(_target.Owner);
                    _target.Owner.SetDirectionAuto();
                    _target.Owner.updateAttackTargetImpl(_target.Owner.TauntSource);
                    battleManager.QueueUpdateSkillTarget();
                }
            }
        }

    }
}
