﻿// Decompiled with JetBrains decompiler
// Type: Elements.LoopMotionRepeatAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class LoopMotionRepeatAction : ActionParameter
  {
    private const int LOOP_MOTION_NUMBER = 1;
    private const int NORMAL_END = 2;
    private const int CANCEL_MOTION = 3;
    private ActionParameter repeatAction;
    private ActionParameter failAction;
    private ActionParameter successAction;
    private float totalDamage;
    private float triggerDamage;
    private bool isLoopMotion;
    private bool cancelAction;
    private float duration;
    private float repeatTime;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      if (this.ActionDetail1 != 0)
        this.repeatAction = _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail1));
      if (this.ActionDetail3 != 0)
        this.failAction = _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail3));
      if (this.ActionDetail2 != 0)
        this.successAction = _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail2));
      _source.OnDamageForLoopRepeat += (Action<float>) (_damage =>
      {
        if (!this.isLoopMotion)
          return;
        this.totalDamage += _damage;
        if ((double) this.totalDamage < (double) this.triggerDamage)
          return;
        this.cancelAction = true;
        this.isLoopMotion = false;
        this.totalDamage = 0.0f;
      });
    }

    public override void ReadyAction(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      base.ReadyAction(_source, _sourceActionController, _skill);
      if (this.repeatAction != null)
        this.repeatAction.CancelByIfForAll = true;
      if (this.failAction != null)
        this.failAction.CancelByIfForAll = true;
      if (this.successAction == null)
        return;
      this.successAction.CancelByIfForAll = true;
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
      this.duration = _valueDictionary[eValueNumber.VALUE_1];
      this.repeatTime = _valueDictionary[eValueNumber.VALUE_2];
      this.triggerDamage = _valueDictionary[eValueNumber.VALUE_3];
      this.isLoopMotion = true;
      this.cancelAction = false;
      _sourceActionController.AppendCoroutine(this.updateStartMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
    }

    private IEnumerator updateStartMotion(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      while (!this.isActionCancel(_source, _sourceActionController, _skill))
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
      LoopMotionRepeatAction motionRepeatAction = this;
      yield return (object) null;
      float timer = motionRepeatAction.duration;
      float repeatTimer = 0.0f;
      while (!motionRepeatAction.isActionCancel(_source, _sourceActionController, _skill))
      {
        repeatTimer -= motionRepeatAction.battleManager.DeltaTime_60fps;
        if ((double) repeatTimer < 0.0 && !motionRepeatAction.cancelAction)
        {
          repeatTimer = motionRepeatAction.repeatTime;
          if (motionRepeatAction.repeatAction != null)
          {
            motionRepeatAction.repeatAction.ReadyAction(_source, _sourceActionController, _skill);
            motionRepeatAction.repeatAction.CancelByIfForAll = false;
            _sourceActionController.ExecUnitActionWithDelay(motionRepeatAction.repeatAction, _skill, false, (uint) motionRepeatAction.repeatAction.DepenedActionId > 0U);
          }
        }
        if (motionRepeatAction.cancelAction)
        {
          for (int index = 0; index < _skill.LoopEffectObjs.Count; ++index)
            _skill.LoopEffectObjs[index].SetTimeToDie(true);
          _skill.LoopEffectObjs.Clear();
          _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 3, false);
          /*foreach (ShakeEffect shakeEffect in _skill.ShakeEffects)
          {
            if (shakeEffect.TargetMotion == 3)
              _sourceActionController.AppendCoroutine(_sourceActionController.StartShakeWithDelay(shakeEffect, _skill, true), ePauseType.VISUAL, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
          }*/
          if (motionRepeatAction.failAction != null)
          {
            motionRepeatAction.failAction.ReadyAction(_source, _sourceActionController, _skill);
            motionRepeatAction.failAction.CancelByIfForAll = false;
            _sourceActionController.ExecUnitActionWithDelay(motionRepeatAction.failAction, _skill, false, (uint) motionRepeatAction.failAction.DepenedActionId > 0U);
          }
          _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 3);
          _sourceActionController.AppendCoroutine(motionRepeatAction.updateEndMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
          motionRepeatAction.isLoopMotion = false;
          break;
        }
        timer -= motionRepeatAction.battleManager.DeltaTime_60fps;
        if ((double) timer < 0.0)
        {
          _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 2, false);
          /*foreach (ShakeEffect shakeEffect in _skill.ShakeEffects)
          {
            if (shakeEffect.TargetMotion == 2)
              _sourceActionController.AppendCoroutine(_sourceActionController.StartShakeWithDelay(shakeEffect, _skill, true), ePauseType.VISUAL, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
          }*/
          if (motionRepeatAction.successAction != null)
          {
            motionRepeatAction.successAction.ReadyAction(_source, _sourceActionController, _skill);
            motionRepeatAction.successAction.CancelByIfForAll = false;
            _sourceActionController.ExecUnitActionWithDelay(motionRepeatAction.successAction, _skill, false, (uint) motionRepeatAction.failAction.DepenedActionId > 0U);
          }
          _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 2);
          _sourceActionController.AppendCoroutine(motionRepeatAction.updateEndMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
          motionRepeatAction.isLoopMotion = false;
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
      while (!this.isActionCancel(_source, _sourceActionController, _skill))
      {
        if (!_source.UnitSpineCtrl.IsPlayAnimeBattle)
        {
          _source.SetState(UnitCtrl.ActionState.IDLE);
          break;
        }
        yield return (object) null;
      }
    }

    public override void SetLevel(float _level) => base.SetLevel(_level);

    private bool isActionCancel(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      if (!_source.IsUnableActionState() && !_source.IsCancelActionState(_skill.SkillId == _source.UnionBurstSkillId))
        return _skill.Cancel;
      _source.CancelByAwake = false;
      _source.CancelByConvert = false;
      _source.CancelByToad = false;
      _sourceActionController.CancelAction(_skill.SkillId);
      return true;
    }
  }
}