﻿// Decompiled with JetBrains decompiler
// Type: Elements.AttackSealAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Cute;

namespace Elements
{
    public class AccumulativeDamageActionForAllEnemy : ActionParameter
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
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            AppendIsAlreadyExeced(_target.Owner, _num);
            eAccumulativeDamageType accumulativeDamageType = (eAccumulativeDamageType)(float)_valueDictionary[eValueNumber.VALUE_1];
            _target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.ACCUMULATIVE_DAMAGE_FOR_ALL_ENEMY, battleManager.BattleLeftTime, this, _skill);
            if (_target.Owner.AccumulativeDamageSourceDataDictionary.ContainsKey(ActionId))
                return;
            AccumulativeDamageSourceData accumulativeDamageData = new AccumulativeDamageSourceData(
                accumulativeDamageType,
                _valueDictionary[eValueNumber.VALUE_2],
                (int)_valueDictionary[eValueNumber.VALUE_4]);
            _target.Owner.AccumulativeDamageSourceDataDictionary.Add(ActionId, accumulativeDamageData);
            //add scripts
            string describe = "使目标受到的伤害逐渐提升";
            action(describe);
            //end add

        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_2] = (float)(MasterData.action_value_2 + MasterData.action_value_3 * _level);
            Value[eValueNumber.VALUE_4] = (float)(MasterData.action_value_4 + MasterData.action_value_5 * _level);
        }
    }

    public class AttackSealActionForAllEnemy : ActionParameter
    {
        private const int DISPLAY_COUNT_NUM = 1;

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
          System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            AppendIsAlreadyExeced(_target.Owner, _num);
            eStateIconType key = (eStateIconType)(float)_valueDictionary[eValueNumber.VALUE_2];
            var dataDictionary = _target.Owner.AttackSealDataDictionary;

            if (dataDictionary.ContainsKey(ActionId))
            {
                dataDictionary[ActionId].LimitTime = _valueDictionary[eValueNumber.VALUE_3];
            }
            else
            {
                var _attackSealData = new AttackSealDataForAllEnemy
                {
                    maxCount = (int)_valueDictionary[eValueNumber.VALUE_1],
                    isDisplayCount = ActionDetail2 == 1,
                    LimitTime = _valueDictionary[eValueNumber.VALUE_3],
                    iconType = key,
                    ExecConditionType = (AttackSealData.eExecConditionType)ActionDetail1
                };
                dataDictionary.Add(ActionId, _attackSealData);
                _sourceActionController.AppendCoroutine(
                    updateAttackSealData(_attackSealData, _source, () => dataDictionary.Remove(ActionId)), ePauseType.SYSTEM, _source);
            }
            action($"每{(AttackSealData.eExecConditionType)ActionDetail1}增加标记");
        }

        private IEnumerator updateAttackSealData(
            AttackSealDataForAllEnemy _attackSealData,
          UnitCtrl _unitCtrl,
          Action _callback)
        {
            while (!_unitCtrl.IdleOnly)
            {
                _attackSealData.LimitTime -= _unitCtrl.DeltaTimeForPause;
                if (_attackSealData.LimitTime < 0.0)
                {
                    _callback.Call();
                    break;
                }
                yield return null;
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_3] = (float)(MasterData.action_value_3 + MasterData.action_value_4 * _level);
            Value[eValueNumber.VALUE_4] = (float)(MasterData.action_value_5 + MasterData.action_value_6 * _level);
        }

        public enum eExecConditionType
        {
            DAMAGE_ONCE = 1,
            TARGET = 2,
            HIT = 3,
            CRITICAL = 4,
        }

        public enum eSealTarget
        {
            TARGET,
            OWNER,
        }
    }
}
