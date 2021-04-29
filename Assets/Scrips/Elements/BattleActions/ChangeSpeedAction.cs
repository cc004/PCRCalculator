// Decompiled with JetBrains decompiler
// Type: Elements.ChangeSpeedAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Elements
{
    public class ChangeSpeedAction : ActionParameter
    {
        private static readonly Dictionary<ChangeSpeedAction.eChangeSpeedType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<ChangeSpeedAction.eChangeSpeedType, UnitCtrl.eAbnormalState>((IEqualityComparer<ChangeSpeedAction.eChangeSpeedType>)new ChangeSpeedAction.eChangeSpeedType_DictComparer())
    {
      {
        ChangeSpeedAction.eChangeSpeedType.SLOW,
        UnitCtrl.eAbnormalState.SLOW
      },
      {
        ChangeSpeedAction.eChangeSpeedType.HASTE,
        UnitCtrl.eAbnormalState.HASTE
      },
      {
        ChangeSpeedAction.eChangeSpeedType.PARALYSIS,
        UnitCtrl.eAbnormalState.PARALYSIS
      },
      {
        ChangeSpeedAction.eChangeSpeedType.FREEZE,
        UnitCtrl.eAbnormalState.FREEZE
      },
      {
        ChangeSpeedAction.eChangeSpeedType.CHAINED,
        UnitCtrl.eAbnormalState.CHAINED
      },
      {
        ChangeSpeedAction.eChangeSpeedType.SLEEP,
        UnitCtrl.eAbnormalState.SLEEP
      },
      {
        ChangeSpeedAction.eChangeSpeedType.STUN,
        UnitCtrl.eAbnormalState.STUN
      },
      {
        ChangeSpeedAction.eChangeSpeedType.STONE,
        UnitCtrl.eAbnormalState.STONE
      },
      {
        ChangeSpeedAction.eChangeSpeedType.DETAIN,
        UnitCtrl.eAbnormalState.DETAIN
      },
      {
        ChangeSpeedAction.eChangeSpeedType.FAINT,
        UnitCtrl.eAbnormalState.FAINT
      },
      {
        ChangeSpeedAction.eChangeSpeedType.PAUSE_ACTION,
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
          Dictionary<eValueNumber, float> _valueDictionary,
          System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            double pp = BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel());
            float num = BattleManager.Random(0.0f, 1f, new PCRCaculator.Guild.RandomData(_source, _target.Owner, ActionId, 6, (float)pp));
            //start add
            if (MyGameCtrl.Instance.tempData.isGuildBattle && MyGameCtrl.Instance.tempData.randomData.TryJudgeRandomSpecialSetting(_source, _target.Owner, _skill, ActionType, BattleHeaderController.CurrentFrameCount, out float fix))
            {
                num = 1 - fix;
            }
            if ((double)num < (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()) || this.TargetAssignment == eTargetAssignment.OWNER_SITE)
            {
                this.AppendIsAlreadyExeced(_target.Owner, _num);
                UnitCtrl.eAbnormalState abnormalState = ChangeSpeedAction.abnormalStateDic[(ChangeSpeedAction.eChangeSpeedType)this.ActionDetail1];
                if (abnormalState == UnitCtrl.eAbnormalState.HASTE)
                {
                    UnitCtrl owner = _target.Owner;
                    Dictionary<BasePartsData, int> dictionary = new Dictionary<BasePartsData, int>();
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
                        _valueDictionary[eValueNumber.VALUE_3] *= myGameCtrl.tempData.SettingData.BossAbnormalMultValue;
                        _valueDictionary[eValueNumber.VALUE_3] += myGameCtrl.tempData.SettingData.BossAbnormalAddValue;
                    }
                }
                _target.Owner.SetAbnormalState(_source, abnormalState, this.AbnormalStateFieldAction == null ? _valueDictionary[eValueNumber.VALUE_3] : 90f, (ActionParameter)this, _skill, _valueDictionary[eValueNumber.VALUE_1], _isDamageRelease: (this.ActionDetail2 == 1));
                string describe = ((ChangeSpeedAction.eChangeSpeedType)this.ActionDetail1).GetDescription() + ",值" + _valueDictionary[eValueNumber.VALUE_1] + ",持续时间：" + _valueDictionary[eValueNumber.VALUE_3] + "秒";
                
                if (this.ActionDetail2 != 1 || _target.Owner.OnDamageListForChangeSpeedDisableByAttack.ContainsKey(this.ActionId))
                {
                    action(describe);
                    return;
                }
                describe += ",受到伤害后取消";
                action(describe);

                _target.Owner.OnDamageListForChangeSpeedDisableByAttack.Add(this.ActionId, (Action<bool>)(byAttack =>
               {
                   if (!byAttack || !_target.Owner.IsAbnormalState(abnormalState))
                       return;
                   _target.Owner.DisableAbnormalStateById(abnormalState, this.ActionId, true);
               }));

            }
            else
            {
                ActionExecedData actionExecedData = this.AlreadyExecedData[_target.Owner][_num];
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
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
            this.Value[eValueNumber.VALUE_3] = (float)((double)this.MasterData.action_value_3 + (double)this.MasterData.action_value_4 * (double)_level);
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

        public class eChangeSpeedType_DictComparer : IEqualityComparer<ChangeSpeedAction.eChangeSpeedType>
        {
            public bool Equals(
              ChangeSpeedAction.eChangeSpeedType _x,
              ChangeSpeedAction.eChangeSpeedType _y) => _x == _y;

            public int GetHashCode(ChangeSpeedAction.eChangeSpeedType _obj) => (int)_obj;
        }
    }
}
