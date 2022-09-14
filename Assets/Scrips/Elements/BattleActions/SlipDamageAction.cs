// Decompiled with JetBrains decompiler
// Type: Elements.SlipDamageAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;

namespace Elements
{
    public class SlipDamageAction : ActionParameter
    {
        private static readonly Dictionary<eSlipDamageType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<eSlipDamageType, UnitCtrl.eAbnormalState>(new eSlipDamageType_DictComparer())
    {
      {
        eSlipDamageType.POISONE,
        UnitCtrl.eAbnormalState.POISON
      },
      {
        eSlipDamageType.BURN,
        UnitCtrl.eAbnormalState.BURN
      },
      {
        eSlipDamageType.CURSE,
        UnitCtrl.eAbnormalState.CURSE
      },
      {
        eSlipDamageType.NO_EFFECT,
        UnitCtrl.eAbnormalState.NO_EFFECT_SLIP_DAMAGE
      },
      {
        eSlipDamageType.VENOM,
        UnitCtrl.eAbnormalState.VENOM
      },
      {
        eSlipDamageType.HEX,
        UnitCtrl.eAbnormalState.HEX
      },
      {
        eSlipDamageType.COMPENSATION,
        UnitCtrl.eAbnormalState.COMPENSATION
      },
            {
                eSlipDamageType.POISON2,
                UnitCtrl.eAbnormalState.POISON2
            },
            {
                eSlipDamageType.CURSE2,
                UnitCtrl.eAbnormalState.CURSE2
            }
    };
        private enum eCancelTriggerType
        {
            NONE = 0,
            MAX_HP = 2
        }
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
          Action<string> callback  )
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            //start add
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());
            float num = BattleManager.Random(0.0f, 1f, new RandomData(_source, _target.Owner, ActionId, 15, (float)pp));
            if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(_source, _target.Owner, _skill, ActionType, BattleHeaderController.CurrentFrameCount, out float fix))
            {
                num = 1-fix;
            }
            bool flag = false;
            UnitCtrl targetOwner = _target.Owner;
            eCancelTriggerType actionDetail = (eCancelTriggerType)base.ActionDetail2;
            if (actionDetail == eCancelTriggerType.MAX_HP)
            {
                flag = BattleUtil.LessThanOrApproximately(_valueDictionary[eValueNumber.VALUE_7] / 100f, (float)(long)targetOwner.Hp / (float)(long)targetOwner.MaxHp);
            }
            //end add
            if (!flag && num < (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
            {
                AppendIsAlreadyExeced(_target.Owner, _num);
                //_target.Owner.SetAbnormalState(_source, abnormalStateDic[(eSlipDamageType)ActionDetail1], AbnormalStateFieldAction == null ? (float)_valueDictionary[eValueNumber.VALUE_3] : 90f, this, _skill, (int)_valueDictionary[eValueNumber.VALUE_1], _valueDictionary[eValueNumber.VALUE_5], _reduceEnergyRate: 0.0f);
                UnitCtrl.eAbnormalState eAbnormalState = abnormalStateDic[(eSlipDamageType)base.ActionDetail1];
                targetOwner.IsReleaseSlipDamageDic[eAbnormalState] = null;
                actionDetail = (eCancelTriggerType)base.ActionDetail2;
                if (actionDetail == eCancelTriggerType.MAX_HP)
                {
                    targetOwner.IsReleaseSlipDamageDic[eAbnormalState] = () => BattleUtil.LessThanOrApproximately(_valueDictionary[eValueNumber.VALUE_7] / 100f, (float)(long)targetOwner.Hp / (float)(long)targetOwner.MaxHp);
                }
                targetOwner.SetAbnormalState(_source, eAbnormalState, (base.AbnormalStateFieldAction == null) ? (float)_valueDictionary[eValueNumber.VALUE_3] : 90f, this, _skill, (int)_valueDictionary[eValueNumber.VALUE_1], _valueDictionary[eValueNumber.VALUE_5], _reduceEnergy: false, _isDamageRelease: false, 0f, _valueDictionary[eValueNumber.VALUE_6] == 0f);


                callback?.Invoke("DOT伤害，参数1：" + (int)_valueDictionary[eValueNumber.VALUE_1] + "，参数2：" + (int)_valueDictionary[eValueNumber.VALUE_5]);
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
                callback?.Invoke($"MISS！{(flag?$"目标血量高于{_valueDictionary[eValueNumber.VALUE_7] / 100f}":"")}");
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
            Value[eValueNumber.VALUE_3] = (float)(MasterData.action_value_3 + MasterData.action_value_4 * _level);
            Value[eValueNumber.VALUE_5] = (float)(double)MasterData.action_value_5;
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
            POISON2,
            CURSE2
        }

        private class eSlipDamageType_DictComparer : IEqualityComparer<eSlipDamageType>
        {
            public bool Equals(eSlipDamageType _x, eSlipDamageType _y) => _x == _y;

            public int GetHashCode(eSlipDamageType _obj) => (int)_obj;
        }
    }
}
