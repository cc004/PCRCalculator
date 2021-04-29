// Decompiled with JetBrains decompiler
// Type: Elements.BarrierAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using System.ComponentModel;

namespace Elements
{
    public class BarrierAction : ActionParameter
    {
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
          Dictionary<eValueNumber, float> _valueDictionary,
          System.Action<string> action = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            this.AppendIsAlreadyExeced(_target.Owner, _num);
            int intReverseTruncate = BattleUtil.FloatToIntReverseTruncate(_valueDictionary[eValueNumber.VALUE_1]);
            switch (this.ActionDetail1)
            {
                case 1:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.GUARD_ATK, _valueDictionary[eValueNumber.VALUE_3], (ActionParameter)this, _skill, (float)intReverseTruncate);
                    break;
                case 2:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.GUARD_MGC, _valueDictionary[eValueNumber.VALUE_3], (ActionParameter)this, _skill, (float)intReverseTruncate);
                    break;
                case 3:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.DRAIN_ATK, _valueDictionary[eValueNumber.VALUE_3], (ActionParameter)this, _skill, (float)intReverseTruncate);
                    break;
                case 4:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.DRAIN_MGC, _valueDictionary[eValueNumber.VALUE_3], (ActionParameter)this, _skill, (float)intReverseTruncate);
                    break;
                case 5:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.GUARD_BOTH, _valueDictionary[eValueNumber.VALUE_3], (ActionParameter)this, _skill, (float)intReverseTruncate);
                    break;
                case 6:
                    _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.DRAIN_BOTH, _valueDictionary[eValueNumber.VALUE_3], (ActionParameter)this, _skill, (float)intReverseTruncate);
                    break;
            }
            action?.Invoke("生成可以" + ((eBarrierType)ActionDetail1).GetDescription() + "的护盾，数值" + intReverseTruncate + "持续时间" + _valueDictionary[eValueNumber.VALUE_3]);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
            this.Value[eValueNumber.VALUE_3] = (float)((double)this.MasterData.action_value_3 + (double)this.MasterData.action_value_4 * (double)_level);
        }

        private enum eBarrierType
        {
            [Description("抵挡物理伤害")]
            GUARD_ATK = 1,
            [Description("抵挡魔法伤害")]
            GUARD_MGC = 2,
            [Description("吸收物理伤害")]
            DRAIN_ATK = 3,
            [Description("吸收魔法伤害")]
            DRAIN_MGC = 4,
            [Description("抵挡伤害")]
            GUARD_BOTH = 5,
            [Description("吸收伤害")]
            DRAIN_BOTH = 6,
        }
    }
}
