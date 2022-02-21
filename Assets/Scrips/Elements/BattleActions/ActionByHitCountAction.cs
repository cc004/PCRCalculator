// Decompiled with JetBrains decompiler
// Type: Elements.ActionByHitCountAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Cute;

namespace Elements
{
  public class ActionByHitCountAction : ActionParameter
  {
    private ActionParameter execAction1;
    private ActionParameter execAction2;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      execAction1 = _skill.ActionParameters.Find(e => e.ActionId == ActionDetail2);
      execAction2 = _skill.ActionParameters.Find(e => e.ActionId == ActionDetail3);
    }

    public override void ReadyAction(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      base.ReadyAction(_source, _sourceActionController, _skill);
      if (execAction1 != null)
        execAction1.CancelByIfForAll = true;
      if (execAction2 == null)
        return;
      execAction2.CancelByIfForAll = true;
    }

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
      AppendIsAlreadyExeced(_target.Owner, _num);
      if (_target.Owner != _source)
        return;
      Action action1 = null;
      ActionByHitCountData actionByHitCountData = new ActionByHitCountData
      {
        LimitTime = _valueDictionary[eValueNumber.VALUE_3],
        ConditionCount = BattleUtil.FloatToInt((float)_valueDictionary[eValueNumber.VALUE_1]),
        ExecLimit = BattleUtil.FloatToInt((float)_valueDictionary[eValueNumber.VALUE_5]),
        StateIconType = (eStateIconType)(float)_valueDictionary[eValueNumber.VALUE_2],
        Enable = true
      };
      if (actionByHitCountData.StateIconType != eStateIconType.NONE)
        _source.OnChangeState.Call(_source, actionByHitCountData.StateIconType, true);
      Action action2 = action1 + (() =>
      {
          if (actionByHitCountData.LimitTime < 0.0 || !actionByHitCountData.Enable)
              return;
          ++actionByHitCountData.HitCounter;
          if (actionByHitCountData.ConditionCount != actionByHitCountData.HitCounter || actionByHitCountData.ExecLimit != 0 && actionByHitCountData.ExecLimit == actionByHitCountData.ExecCounter)
              return;
          ++actionByHitCountData.ExecCounter;
          if (actionByHitCountData.ExecLimit != 0 && actionByHitCountData.ExecLimit == actionByHitCountData.ExecCounter)
          {
              if (actionByHitCountData.StateIconType != eStateIconType.NONE)
                  _source.OnChangeState.Call(_source, actionByHitCountData.StateIconType, false);
              actionByHitCountData.Enable = false;
          }
          _sourceActionController.SearchTargetByAction(_skill.SkillId);
          actionByHitCountData.HitCounter = 0;
          if (execAction1 != null)
          {
              execAction1.CancelByIfForAll = false;
              execAction1.ResetHitData();
              _sourceActionController.ExecUnitActionNoDelay(execAction1, _skill);
          }
          if (execAction2 == null)
              return;
          execAction2.CancelByIfForAll = false;
          execAction2.ResetHitData();
          _sourceActionController.ExecUnitActionNoDelay(execAction2, _skill);
      });
      switch (ActionDetail1)
      {
        case 1:
          _source.OnActionByDamageOnce += action2;
          break;
        case 3:
          _source.OnActionByDamage += action2;
          break;
        case 4:
          _source.OnActionByCritical += action2;
          break;
      }
      _sourceActionController.AppendCoroutine(updateAttackSealData(actionByHitCountData, _source), ePauseType.SYSTEM, _source);
    }

    private IEnumerator updateAttackSealData(
      ActionByHitCountData _actionByHitCountData,
      UnitCtrl _unitCtrl)
    {
      while (!_unitCtrl.IdleOnly)
      {
        _actionByHitCountData.LimitTime -= _unitCtrl.DeltaTimeForPause;
        if (_actionByHitCountData.LimitTime < 0.0)
        {
          if (_actionByHitCountData.StateIconType != eStateIconType.NONE)
            _unitCtrl.OnChangeState.Call(_unitCtrl, _actionByHitCountData.StateIconType, false);
          _actionByHitCountData.Enable = false;
          yield break;
        }

        yield return null;
      }
      if (_actionByHitCountData.StateIconType != eStateIconType.NONE)
        _unitCtrl.OnChangeState.Call(_unitCtrl, _actionByHitCountData.StateIconType, false);
      _actionByHitCountData.Enable = false;
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
    }

    public enum eExecConditionType
    {
      DAMAGE_ONCE = 1,
      TARGET = 2,
      HIT = 3,
      CRITICAL = 4,
    }
  }
}
