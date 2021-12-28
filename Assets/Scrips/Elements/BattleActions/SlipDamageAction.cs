// Decompiled with JetBrains decompiler
// Type: Elements.SlipDamageAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections.Generic;

namespace Elements
{
    public class SlipDamageAction : ActionParameter
    {
        private static readonly Dictionary<SlipDamageAction.eSlipDamageType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<SlipDamageAction.eSlipDamageType, UnitCtrl.eAbnormalState>((IEqualityComparer<SlipDamageAction.eSlipDamageType>)new SlipDamageAction.eSlipDamageType_DictComparer())
    {
      {
        SlipDamageAction.eSlipDamageType.POISONE,
        UnitCtrl.eAbnormalState.POISON
      },
      {
        SlipDamageAction.eSlipDamageType.BURN,
        UnitCtrl.eAbnormalState.BURN
      },
      {
        SlipDamageAction.eSlipDamageType.CURSE,
        UnitCtrl.eAbnormalState.CURSE
      },
      {
        SlipDamageAction.eSlipDamageType.NO_EFFECT,
        UnitCtrl.eAbnormalState.NO_EFFECT_SLIP_DAMAGE
      },
      {
        SlipDamageAction.eSlipDamageType.VENOM,
        UnitCtrl.eAbnormalState.VENOM
      },
      {
        SlipDamageAction.eSlipDamageType.HEX,
        UnitCtrl.eAbnormalState.HEX
      },
      {
        SlipDamageAction.eSlipDamageType.COMPENSATION,
        UnitCtrl.eAbnormalState.COMPENSATION
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
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          System.Action<string> callback  )
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            //start add
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());
            float num = BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 15, (float)pp));
            if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(_source, _target.Owner, _skill, ActionType, BattleHeaderController.CurrentFrameCount, out float fix))
            {
                num = 1-fix;
            }
            //end add
            if ((double)num < (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
            {
                this.AppendIsAlreadyExeced(_target.Owner, _num);
                _target.Owner.SetAbnormalState(_source, SlipDamageAction.abnormalStateDic[(SlipDamageAction.eSlipDamageType)this.ActionDetail1], this.AbnormalStateFieldAction == null ? (float)_valueDictionary[eValueNumber.VALUE_3] : 90f, (ActionParameter)this, _skill, (float)(int)_valueDictionary[eValueNumber.VALUE_1], _valueDictionary[eValueNumber.VALUE_5], _reduceEnergyRate: 0.0f);
                callback?.Invoke("DOT伤害，参数1：" + (int)_valueDictionary[eValueNumber.VALUE_1] + "，参数2：" + (int)_valueDictionary[eValueNumber.VALUE_5]);
            }
            else
            {
                ActionExecedData actionExecedData = this.AlreadyExecedData[_target.Owner][_num];
                if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
                    return;
                if (actionExecedData.TargetPartsNumber == 1)
                    _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SLIP_DAMAGE, _parts: _target);
                else
                    _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SLIP_DAMAGE);
                callback?.Invoke("MISS！");
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
            this.Value[eValueNumber.VALUE_3] = (float)((double)this.MasterData.action_value_3 + (double)this.MasterData.action_value_4 * (double)_level);
            this.Value[eValueNumber.VALUE_5] = (float)(double)this.MasterData.action_value_5;
        }

        private enum eSlipDamageType
        {
            NO_EFFECT,
            POISONE,
            BURN,
            CURSE,
            VENOM,
            HEX,
            COMPENSATION,
        }

        private class eSlipDamageType_DictComparer : IEqualityComparer<SlipDamageAction.eSlipDamageType>
        {
            public bool Equals(SlipDamageAction.eSlipDamageType _x, SlipDamageAction.eSlipDamageType _y) => _x == _y;

            public int GetHashCode(SlipDamageAction.eSlipDamageType _obj) => (int)_obj;
        }
    }
}
