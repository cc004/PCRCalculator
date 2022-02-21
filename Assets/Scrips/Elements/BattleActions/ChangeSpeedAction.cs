// Decompiled with JetBrains decompiler
// Type: Elements.ChangeSpeedAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Elements.Battle;
using PCRCaculator.Guild;

namespace Elements
{
    public class ChangeSpeedAction : ActionParameter
    {
        private static readonly Dictionary<eChangeSpeedType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<eChangeSpeedType, UnitCtrl.eAbnormalState>(new eChangeSpeedType_DictComparer())
    {
      {
        eChangeSpeedType.SLOW,
        UnitCtrl.eAbnormalState.SLOW
      },
      {
        eChangeSpeedType.HASTE,
        UnitCtrl.eAbnormalState.HASTE
      },
      {
        eChangeSpeedType.PARALYSIS,
        UnitCtrl.eAbnormalState.PARALYSIS
      },
      {
        eChangeSpeedType.FREEZE,
        UnitCtrl.eAbnormalState.FREEZE
      },
      {
        eChangeSpeedType.CHAINED,
        UnitCtrl.eAbnormalState.CHAINED
      },
      {
        eChangeSpeedType.SLEEP,
        UnitCtrl.eAbnormalState.SLEEP
      },
      {
        eChangeSpeedType.STUN,
        UnitCtrl.eAbnormalState.STUN
      },
      {
        eChangeSpeedType.STONE,
        UnitCtrl.eAbnormalState.STONE
      },
      {
        eChangeSpeedType.DETAIN,
        UnitCtrl.eAbnormalState.DETAIN
      },
      {
        eChangeSpeedType.FAINT,
        UnitCtrl.eAbnormalState.FAINT
      },
      {
        eChangeSpeedType.PAUSE_ACTION,
        UnitCtrl.eAbnormalState.PAUSE_ACTION
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
          Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());
            float num = BattleManager.Random(0.0f, 1f, new RandomData(_source, _target.Owner, ActionId, 6, (float)pp));
            //start add
            if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(_source, _target.Owner, _skill, ActionType, BattleHeaderController.CurrentFrameCount, out float fix))
            {
                num = 1 - fix;
            }
            if (num < (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()) || TargetAssignment == eTargetAssignment.OWNER_SITE)
            {
                AppendIsAlreadyExeced(_target.Owner, _num);
                UnitCtrl.eAbnormalState abnormalState = abnormalStateDic[(eChangeSpeedType)ActionDetail1];
                if (abnormalState == UnitCtrl.eAbnormalState.HASTE)
                {
                    UnitCtrl owner = _target.Owner;
                    Dictionary<BasePartsData, FloatWithEx> dictionary = new Dictionary<BasePartsData, FloatWithEx>();
                    dictionary.Add(_target, 0);
                    int skillId = _skill.SkillId;
                    UnitCtrl _source1 = _source;
                    owner.SetBuffParam(UnitCtrl.BuffParamKind.NUM, dictionary, 0.5f, skillId, _source1, true, eEffectType.COMMON, true, false);
                }
                MyGameCtrl myGameCtrl = MyGameCtrl.Instance;
                if(myGameCtrl.tempData.isGuildBattle)// && _target.Owner.IsBoss)
                {
                    if (ActionDetail1 >= 3)
                    {
                        //_valueDictionary[eValueNumber.VALUE_3] *= myGameCtrl.tempData.SettingData.BossAbnormalMultValue;
                        //_valueDictionary[eValueNumber.VALUE_3] += myGameCtrl.tempData.SettingData.BossAbnormalAddValue;
                    }
                }
                _target.Owner.SetAbnormalState(_source, abnormalState, AbnormalStateFieldAction == null ? (float)_valueDictionary[eValueNumber.VALUE_3] : 90f, this, _skill, _valueDictionary[eValueNumber.VALUE_1], _isDamageRelease: (ActionDetail2 == 1));
                string describe = ((eChangeSpeedType)ActionDetail1).GetDescription() + ",值" + _valueDictionary[eValueNumber.VALUE_1] + ",持续时间：" + _valueDictionary[eValueNumber.VALUE_3] + "秒";
                
                if (ActionDetail2 != 1 || _target.Owner.OnDamageListForChangeSpeedDisableByAttack.ContainsKey(ActionId))
                {
                    action(describe);
                    return;
                }
                describe += ",受到伤害后取消";
                action(describe);

                _target.Owner.OnDamageListForChangeSpeedDisableByAttack.Add(ActionId, byAttack =>
                {
                    if (!byAttack || !_target.Owner.IsAbnormalState(abnormalState))
                        return;
                    _target.Owner.DisableAbnormalStateById(abnormalState, ActionId, true);
                });

            }
            else
            {
                ActionExecedData actionExecedData = AlreadyExecedData[_target.Owner][_num];
                if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
                    return;
                if (actionExecedData.TargetPartsNumber == 1)
                    _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_CHANGE_SPEED, _parts: _target);
                else
                    _target.Owner.SetMissAtk(_source, eMissLogType.DODGE_CHANGE_SPEED);
                action("MISS");
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
            Value[eValueNumber.VALUE_3] = (float)(MasterData.action_value_3 + MasterData.action_value_4 * _level);
        }

        public enum eChangeSpeedType
        {
            [Description("减速")]
            SLOW = 1,
            [Description("加速")]
            HASTE = 2,
            [Description("麻痹")]
            PARALYSIS = 3,
            [Description("冰冻")]
            FREEZE = 4,
            [Description("拘留")]
            CHAINED = 5,
            [Description("睡眠")]
            SLEEP = 6,
            [Description("击晕")]
            STUN = 7,
            [Description("石化")]
            STONE = 8,
            [Description("？？？")]
            DETAIN = 9,
            [Description("恐惧")]
            FAINT = 10, // 0x0000000A
            [Description("暂停")]
            PAUSE_ACTION = 11, // 0x0000000B
        }

        private enum eCancelTriggerType
        {
            NONE,
            DAMAGED,
        }

        public class eChangeSpeedType_DictComparer : IEqualityComparer<eChangeSpeedType>
        {
            public bool Equals(
              eChangeSpeedType _x,
              eChangeSpeedType _y) => _x == _y;

            public int GetHashCode(eChangeSpeedType _obj) => (int)_obj;
        }
    }
}
