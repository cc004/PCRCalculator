// Decompiled with JetBrains decompiler
// Type: Elements.BlindAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections.Generic;

namespace Elements
{
    public class BlindAction : ActionParameter
    {
        private const float PERCENTAGE_TO_RATE = 0.01f;

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
          Dictionary<eValueNumber, float> _valueDictionary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            double judgeNum = (double)(_valueDictionary[eValueNumber.VALUE_3] * 0.01f) * (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());
                        double randomNum = (double)BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 5, (float)judgeNum));

            if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(_source, _target.Owner, _skill, eActionType.BLIND, BattleHeaderController.CurrentFrameCount, out float fix))
            {
                randomNum = 1-fix;
            }
            if (randomNum <=judgeNum)
            {
                this.AppendIsAlreadyExeced(_target.Owner, _num);
                _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.PHYSICS_DARK, this.AbnormalStateFieldAction == null ? _valueDictionary[eValueNumber.VALUE_1] : 90f, (ActionParameter)this, _skill, (float)this.ActionDetail1);
            }
            else
            {
                ActionExecedData actionExecedData = this.AlreadyExecedData[_target.Owner][_num];
                if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
                    return;
                if (actionExecedData.TargetPartsNumber == 1)
                    _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_DARK, _parts: _target);
                else
                    _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_DARK);
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
            this.Value[eValueNumber.VALUE_3] = (float)((double)this.MasterData.action_value_3 + (double)this.MasterData.action_value_4 * (double)_level);
        }
    }
}
