// Decompiled with JetBrains decompiler
// Type: Elements.StealthAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class StealthAction : ActionParameter
  {
    private const int LOOP_MOTION_NUMBER = 1;
    private const int END_MOTION_NUMBER = 2;

    private ActionParameter action { get; set; }

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      this.action = _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail1));
    }

    public override void ReadyAction(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      base.ReadyAction(_source, _sourceActionController, _skill);
      if (this.action == null)
        return;
      this.action.CancelByIfForAll = true;
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
      this.AppendIsAlreadyExeced(_target.Owner, _num);
      _target.Owner.IsStealth = true;
      _target.Owner.AppendCoroutine(this.updateStealth(_valueDictionary[eValueNumber.VALUE_1], _target.Owner), ePauseType.SYSTEM);
      if (_skill.AnimId == eSpineCharacterAnimeId.NONE || !_source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum, 1))
        return;
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
      yield return (object) null;
      while (!this.isActionCancel(_source, _sourceActionController, _skill))
      {
        if (_source.ActionsTargetOnMe.Count == 0)
        {
          for (int index = 0; index < _skill.LoopEffectObjs.Count; ++index)
            _skill.LoopEffectObjs[index].SetTimeToDie(true);
          _skill.LoopEffectObjs.Clear();
          this.action.CancelByIfForAll = false;
          _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 2, false);
          _sourceActionController.ExecUnitActionWithDelay(this.action, _skill, false, false);
          _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 2);
          _sourceActionController.AppendCoroutine(this.updateEndMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
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
      StealthAction stealthAction = this;
      while (!stealthAction.isActionCancel(_source, _sourceActionController, _skill))
      {
        if (!_source.UnitSpineCtrl.IsPlayAnimeBattle)
        {
          _source.IdleOnly = true;
          _source.CurrentState = UnitCtrl.ActionState.IDLE;
          stealthAction.battleManager.CallbackIdleOnlyDone(_source);
          List<UnitCtrl> unitCtrlList = _source.IsOther ? stealthAction.battleManager.EnemyList : stealthAction.battleManager.UnitList;
          if (unitCtrlList.Contains(_source))
            unitCtrlList.Remove(_source);
          _source.IsDead = true;
          _source.CureAllAbnormalState();
          stealthAction.battleManager.CallbackFadeOutDone(_source);
          stealthAction.battleManager.CallbackDead(_source);
          _source.gameObject.SetActive(false);
          break;
        }
        yield return (object) null;
      }
    }

    private IEnumerator updateStealth(float _duration, UnitCtrl _target)
    {
      StealthAction stealthAction = this;
      float timer = _duration;
      while ((double) timer > 0.0)
      {
        timer -= stealthAction.battleManager.DeltaTime_60fps;
        yield return (object) null;
      }
      _target.IsStealth = false;
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
