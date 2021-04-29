// Decompiled with JetBrains decompiler
// Type: Elements.LoopTriggerAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class LoopTriggerAction : ActionParameter
  {
    private const int LOOP_MOTION_NUMBER = 1;
    private const int FAILURE_MOTION = 3;
    private const int SUCCESS_MOTION = 2;

    private bool triggerSuccess { get; set; }

    private float duration { get; set; }

    private ActionParameter actionParameter1 { get; set; }

    private ActionParameter actionParameter2 { get; set; }

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      if (this.ActionDetail2 != 0)
        this.actionParameter1 = _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail2));
      if (this.ActionDetail3 != 0)
        this.actionParameter2 = _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail3));
      if (this.ActionDetail1 != 2)
        return;
      _source.OnDamageForLoopTrigger = (Action<bool, float, bool>) ((_byAttack, _damage, _critical) =>
      {
        if (!_byAttack)
          return;
        this.triggerSuccess = true;
      });
    }

    public override void ReadyAction(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      base.ReadyAction(_source, _sourceActionController, _skill);
      if (this.actionParameter1 != null)
        this.actionParameter1.CancelByIfForAll = true;
      if (this.actionParameter2 == null)
        return;
      this.actionParameter2.CancelByIfForAll = true;
    }

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
      this.triggerSuccess = false;
      this.duration = _valueDictionary[eValueNumber.VALUE_4];
      _sourceActionController.AppendCoroutine(this.updateStartMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
    }

    private IEnumerator updateStartMotion(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      while (!this.isActionCancel(_source, _sourceActionController, _skill, true))
      {
        if (!_source.UnitSpineCtrl.IsPlayAnimeBattle)
        {
          _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 1, false);
          _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 1, _isLoop: true);
          _sourceActionController.AppendCoroutine(this.updateLoopMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
          break;
        }
        yield return (object) null;
      }
    }

    private IEnumerator updateLoopMotion(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      LoopTriggerAction loopTriggerAction = this;
      yield return (object) null;
      float timer = loopTriggerAction.duration;
      while (!loopTriggerAction.isActionCancel(_source, _sourceActionController, _skill, false))
      {
        if (loopTriggerAction.triggerSuccess)
        {
          if (loopTriggerAction.actionParameter1 != null)
          {
            loopTriggerAction.actionParameter1.CancelByIfForAll = false;
            _sourceActionController.ExecUnitActionWithDelay(loopTriggerAction.actionParameter1, _skill, false, (uint) loopTriggerAction.actionParameter1.DepenedActionId > 0U);
          }
          if (loopTriggerAction.actionParameter2 != null)
          {
            loopTriggerAction.actionParameter2.CancelByIfForAll = false;
            _sourceActionController.ExecUnitActionWithDelay(loopTriggerAction.actionParameter2, _skill, false, (uint) loopTriggerAction.actionParameter2.DepenedActionId > 0U);
          }
          for (int index = 0; index < _skill.LoopEffectObjs.Count; ++index)
            _skill.LoopEffectObjs[index].SetTimeToDie(true);
          _skill.LoopEffectObjs.Clear();
          _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 2, false);
          _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 2);
          _sourceActionController.AppendCoroutine(loopTriggerAction.updateEndMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _source);
          break;
        }
        timer -= loopTriggerAction.battleManager.DeltaTime_60fps;
        if ((double) timer < 0.0)
        {
          _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 3);
          _sourceActionController.AppendCoroutine(loopTriggerAction.updateEndMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _source);
          break;
        }
        yield return (object) null;
      }
    }

    private IEnumerator updateEndMotion(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      while (!this.isActionCancel(_source, _sourceActionController, _skill, false))
      {
        if (!_source.UnitSpineCtrl.IsPlayAnimeBattle)
        {
          _source.SetState(UnitCtrl.ActionState.IDLE);
          break;
        }
        yield return (object) null;
      }
    }

    private bool isActionCancel(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill,
      bool _isStartMotion)
    {
      if (!_source.IsUnableActionState() && !_source.IsCancelActionState(_skill.SkillId == _source.UnionBurstSkillId))
        return _skill.Cancel;
      if (!_isStartMotion)
      {
        _source.CancelByAwake = false;
        _source.CancelByConvert = false;
        _source.CancelByToad = false;
      }
      _sourceActionController.CancelAction(_skill.SkillId);
      return true;
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
    }

    private enum eTriggerType
    {
      DODGE = 1,
      DAMAGED = 2,
      HP = 3,
      DEAD = 4,
      CRITICAL_DAMAGED = 5,
      CRITICAL_DAMAGED_WITH_SUMMON = 6,
    }
  }
}
