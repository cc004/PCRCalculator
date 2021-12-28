// Decompiled with JetBrains decompiler
// Type: Elements.LoopMotionBuffDebuffAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class LoopMotionBuffDebuffAction : ActionParameter
  {
    private List<LoopMotionBuffDebuffAction> childActions = new List<LoopMotionBuffDebuffAction>();
    private List<LoopMotionBuffDebuffAction.BuffDebuffData> targetAndValueList = new List<LoopMotionBuffDebuffAction.BuffDebuffData>();
    private const float PERCENT_DIGIT = 100f;
    private const int LOOP_MOTION_NUMBER = 1;
    private const int RELEASE_TYPE_MAX = 10;
    private const int DETAIL_DIGIT = 10;
    private const int DETAIL_DEBUFF = 1;
    private const int BY_TIMER_MOTION_NUMBER = 3;
    private const int BY_TIRRIGER_MOTION_NUMBER = 2;

    private bool triggered { get; set; }

    private int hitCount { get; set; }

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      if (this.ActionDetail2 <= 10)
        return;
      ActionParameter actionParameter = _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail2 && e is LoopMotionBuffDebuffAction));
      if (actionParameter == null)
        return;
      (actionParameter as LoopMotionBuffDebuffAction).childActions.Add(this);
    }

    public override void ReadyAction(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      base.ReadyAction(_source, _sourceActionController, _skill);
      this.hitCount = 0;
      this.targetAndValueList.Clear();
      if (this.ActionDetail2 > 10)
        return;
      this.triggered = false;
      _source.AppendCoroutine(this.updateStartMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _source);
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
          _sourceActionController.AppendCoroutine(this.updateLoopMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _source);
          yield break;
        }
        else
          yield return (object) null;
      }
      this.ReleaseBuffDebuff(_source);
      for (int index = 0; index < this.childActions.Count; ++index)
        this.childActions[index].ReleaseBuffDebuff(_source);
    }

    private IEnumerator updateLoopMotion(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      LoopMotionBuffDebuffAction buffDebuffAction = this;
      float timer = buffDebuffAction.Value[eValueNumber.VALUE_4];
      bool enableTimer = (double) timer > 0.0;
      while (!buffDebuffAction.isActionCancel(_source, _sourceActionController, _skill))
      {
        if (enableTimer)
        {
          timer -= _source.DeltaTimeForPause;
          if ((double) timer < 0.0 || buffDebuffAction.battleManager.GameState != eBattleGameState.PLAY)
          {
            enableTimer = false;
            buffDebuffAction.startMotionEnd(true, _source, _skill, _sourceActionController);
            yield break;
          }
        }
        if (buffDebuffAction.triggered)
        {
          buffDebuffAction.startMotionEnd(false, _source, _skill, _sourceActionController);
          yield break;
        }
        else
          yield return (object) null;
      }
      buffDebuffAction.ReleaseBuffDebuff(_source);
      for (int index = 0; index < buffDebuffAction.childActions.Count; ++index)
        buffDebuffAction.childActions[index].ReleaseBuffDebuff(_source);
      for (int index = _skill.LoopEffectObjs.Count - 1; index >= 0; --index)
      {
        SkillEffectCtrl loopEffectObj = _skill.LoopEffectObjs[index];
        if (!((UnityEngine.Object) loopEffectObj == (UnityEngine.Object) null) && (UnityEngine.Object) loopEffectObj.GetComponent<SkillEffectCtrl>().SortTarget == (UnityEngine.Object) _source)
        {
          loopEffectObj.SetTimeToDie(true);
          _skill.LoopEffectObjs.RemoveAt(index);
        }
      }
    }

    private void startMotionEnd(
      bool _byTimer,
      UnitCtrl _source,
      Skill _skill,
      UnitActionController _sourceActionController)
    {
      _skill.LoopEffectAlreadyDone = true;
      for (int index = _skill.LoopEffectObjs.Count - 1; index >= 0; --index)
      {
        if (!((UnityEngine.Object) _skill.LoopEffectObjs[index] == (UnityEngine.Object) null) && (UnityEngine.Object) _skill.LoopEffectObjs[index].GetComponent<SkillEffectCtrl>().SortTarget == (UnityEngine.Object) _source)
        {
          _skill.LoopEffectObjs[index].SetTimeToDie(true);
          _skill.LoopEffectObjs.RemoveAt(index);
        }
      }
      this.ReleaseBuffDebuff(_source);
      for (int index = 0; index < this.childActions.Count; ++index)
        this.childActions[index].ReleaseBuffDebuff(_source);
      int num = _byTimer ? 3 : 2;
      _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, num, false);
      _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, num);
      _sourceActionController.AppendCoroutine(this.updateEndMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
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

    public void ReleaseBuffDebuff(UnitCtrl _source)
    {
      this.triggered = true;
      for (int index = 0; index < this.targetAndValueList.Count; ++index)
      {
        LoopMotionBuffDebuffAction.BuffDebuffData targetAndValue = this.targetAndValueList[index];
        targetAndValue.Target.EnableBuffParam(targetAndValue.BuffParamKind, targetAndValue.Value, false, _source, this.ActionDetail1 % 10 != 1, false,90);
      }
    }

    private bool isActionCancel(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      if (!_source.IsUnableActionState() && !_source.IsCancelActionState(_skill.SkillId == _source.UnionBurstSkillId))
        return false;
      _source.CancelByAwake = false;
      _source.CancelByConvert = false;
      _source.CancelByToad = false;
      _sourceActionController.CancelAction(_skill.SkillId);
      return true;
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
      if (this.triggered)
        return;
      UnitCtrl.BuffParamKind changeParamKind = BuffDebuffAction.GetChangeParamKind(this.ActionDetail1);
      Dictionary<BasePartsData, FloatWithEx> dictionary = new Dictionary<BasePartsData, FloatWithEx>();
      bool _isDebuf = this.ActionDetail1 % 10 == 1;
      if (_target.Owner.IsPartsBoss)
      {
        for (int index = 0; index < _target.Owner.BossPartsListForBattle.Count; ++index)
          dictionary.Add((BasePartsData) _target.Owner.BossPartsListForBattle[index], BuffDebuffAction.CalculateBuffDebuffParam((BasePartsData) _target.Owner.BossPartsListForBattle[index], _valueDictionary[eValueNumber.VALUE_2], (BuffDebuffAction.eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, _isDebuf));
      }
      else
        dictionary.Add(_target.Owner.DummyPartsData, BuffDebuffAction.CalculateBuffDebuffParam(_target, _valueDictionary[eValueNumber.VALUE_2], (BuffDebuffAction.eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, _isDebuf));
      _target.Owner.SetBuffParam(UnitCtrl.BuffParamKind.NUM, dictionary, 0.0f, 0, (UnitCtrl) null, true, eEffectType.COMMON, !_isDebuf, false);
      _target.Owner.EnableBuffParam(changeParamKind, dictionary, true, _source, !_isDebuf, false,90);
      if (this.ActionDetail2 == 1)
        _source.OnDamageForLoopTrigger = (Action<bool, float, bool>) ((_byAttack, _damage, _critical) =>
        {
          if (!_byAttack)
            return;
          ++this.hitCount;
          if (this.hitCount != this.ActionDetail3)
            return;
          this.triggered = true;
        });
      this.targetAndValueList.Add(new LoopMotionBuffDebuffAction.BuffDebuffData()
      {
        Target = _target.Owner,
        BuffParamKind = changeParamKind,
        Value = dictionary
      });
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_2] = (float) ((double) this.MasterData.action_value_2 + (double) this.MasterData.action_value_3 * (double) _level);
      this.Value[eValueNumber.VALUE_4] = (float) ((double) this.MasterData.action_value_4 + (double) this.MasterData.action_value_5 * (double) _level);
    }

    private class BuffDebuffData
    {
      public UnitCtrl Target { get; set; }

      public Dictionary<BasePartsData, FloatWithEx> Value { get; set; }

      public UnitCtrl.BuffParamKind BuffParamKind { get; set; }
    }

    private enum eReleaseType
    {
      DAMAGED = 1,
    }

    private enum eValueType
    {
      FIXED = 1,
      PERCENTAGE = 2,
    }
  }
}
