// Decompiled with JetBrains decompiler
// Type: Elements.InhibitHealAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
    public class InhibitHealAction : ActionParameter
    {
        private enum eInhibitType
        {
            INHIBIT,
            DECREASE
        }
        private Dictionary<eInhibitType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<eInhibitType, UnitCtrl.eAbnormalState>
        {
            {
                eInhibitType.INHIBIT,
                UnitCtrl.eAbnormalState.INHIBIT_HEAL
            },
            {
                eInhibitType.DECREASE,
                UnitCtrl.eAbnormalState.DECREASE_HEAL
            }
        };
        public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController) => base.ExecActionOnStart(_skill, _source, _sourceActionController);

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());

            if (BattleManager.Random(0.0f, 1f,
                    new RandomData(_source, _target.Owner, ActionId, 13, (float)pp)) < (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
            {
                AppendIsAlreadyExeced(_target.Owner, _num);
                _target.Owner.SetAbnormalState(_source, abnormalStateDic[(eInhibitType)base.ActionDetail1], (base.AbnormalStateFieldAction == null) ? (float)_valueDictionary[eValueNumber.VALUE_2] : 9999f, this, _skill, _valueDictionary[eValueNumber.VALUE_1]);

                //_target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.INHIBIT_HEAL, AbnormalStateFieldAction == null ? (float)_valueDictionary[eValueNumber.VALUE_2] : 9999f, this, _skill, _valueDictionary[eValueNumber.VALUE_1]);
            }
            else
            {
                ActionExecedData actionExecedData = AlreadyExecedData[_target.Owner][_num];
                if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
                    return;
                if (actionExecedData.TargetPartsNumber == 1)
                    _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SLIP_DAMAGE, _parts: _target);
                else
                    _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SLIP_DAMAGE);
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            base.Value[eValueNumber.VALUE_1] = Mathf.Max(0f, (float)((double)base.MasterData.action_value_1 + (double)base.MasterData.action_value_4 * (double)_level));
            Value[eValueNumber.VALUE_2] = (float)(MasterData.action_value_2 + MasterData.action_value_3 * _level);
        }
    }
}
