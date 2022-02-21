// Decompiled with JetBrains decompiler
// Type: Elements.ContinuousAttackAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class ContinuousAttackAction : ActionParameter
  {
    private bool continuousEnd { get; set; }

    private float continuousDeltaHp { get; set; }

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
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      _sourceActionController.AppendCoroutine(updateContinuousAttack(_target, _skill, _source, _sourceActionController, ActionDetail2 == 0 ? null : _skill.ActionParameters.Find(e => e.ActionId == ActionDetail2)), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
    }

    private IEnumerator updateContinuousAttack(
      BasePartsData _target,
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      ActionParameter _childAction = null)
    {
      ContinuousAttackAction continuousAttackAction = this;
      continuousAttackAction.continuousDeltaHp = (float) (long) _source.MaxHp / continuousAttackAction.Value[eValueNumber.VALUE_3];
      continuousAttackAction.ContinuousTargetCount++;
      if (_childAction != null)
      {
        if (_childAction.OnActionEnd != null)
          continuousAttackAction.OnActionEnd = _childAction.OnActionEnd;
        if (continuousAttackAction.OnDamageHit != null)
          _childAction.OnDamageHit = continuousAttackAction.OnDamageHit;
      }
      float time = continuousAttackAction.Value[eValueNumber.VALUE_1];
      float lastAttacktime = 0.0f;
      while (true)
      {
        time += _source.DeltaTimeForPause;
        if (time - (double) lastAttacktime > (double) continuousAttackAction.Value[eValueNumber.VALUE_1])
        {
          if ((long) _target.Owner.Hp > 0L)
          {
            if (_childAction != null)
              _sourceActionController.ExecAction(_childAction, _skill, _target, 0, 0.0f);
            lastAttacktime = time;
          }
          else
            break;
        }
        switch (continuousAttackAction.ActionDetail1)
        {
          case 1:
            if (continuousAttackAction.continuousDeltaHp != 0.0)
              break;
            goto label_20;
          case 2:
            if (time <= (double) continuousAttackAction.Value[eValueNumber.VALUE_3])
              break;
            goto label_26;
          case 3:
            if (time <= (double) continuousAttackAction.Value[eValueNumber.VALUE_3])
              break;
            goto label_29;
        }
        yield return null;
      }
      continuousAttackAction.ContinuousTargetCount--;
      if (continuousAttackAction.ContinuousTargetCount <= 0)
      {
        if (continuousAttackAction.ActionDetail1 == 3)
        {
          if (continuousAttackAction.OnActionEnd != null && !_sourceActionController.ContinuousActionEndDone)
          {
            continuousAttackAction.OnActionEnd();
            _sourceActionController.ContinuousActionEndDone = true;
          }
        }
        else
        {
          continuousAttackAction.ContinuousTargetCount = 0;
          continuousAttackAction.continuousEnd = true;
        }
      }
      for (int index = 0; index < _skill.LoopEffectObjs.Count; ++index)
      {
        if (_skill.LoopEffectObjs[index] != null && _skill.LoopEffectObjs[index].GetComponent<SkillEffectCtrl>().SortTarget == _target.Owner)
        {
          _skill.LoopEffectObjs[index].SetTimeToDie(true);
          _skill.LoopEffectObjs.RemoveAt(index);
          --index;
        }
      }
      if (continuousAttackAction.ActionDetail1 != 3)
      {
        yield break;
      }

      long int64 = Convert.ToInt64(continuousAttackAction.ActionId);
      _target.Owner.ActionsTargetOnMe.Remove(int64 * 100L + continuousAttackAction.IdOffsetDictionary[_target]);
      continuousAttackAction.battleManager.CallbackActionEnd(int64 * 100L + continuousAttackAction.IdOffsetDictionary[_target]);
      yield break;
      label_20:
      yield break;
label_26:
      yield break;
label_29:
      if (continuousAttackAction.OnActionEnd != null && !_sourceActionController.ContinuousActionEndDone)
      {
        continuousAttackAction.OnActionEnd();
        _sourceActionController.ContinuousActionEndDone = true;
      }
      long int64_1 = Convert.ToInt64(continuousAttackAction.ActionId);
      _target.Owner.ActionsTargetOnMe.Remove(int64_1 * 100L + continuousAttackAction.IdOffsetDictionary[_target]);
      continuousAttackAction.battleManager.CallbackActionEnd(int64_1 * 100L + continuousAttackAction.IdOffsetDictionary[_target]);
    }

    public IEnumerator UpdateMotionRoopForContinuousDamage(
      Skill skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      ContinuousAttackAction continuousAttackAction = this;
      bool flag = false;
      while (true)
      {
        if (continuousAttackAction.ActionDetail1 == 1)
        {
          if (continuousAttackAction.continuousDeltaHp > 0.0)
            _source.SetDamage(new DamageData
            {
              Target = null,
              Damage = (long) BattleUtil.FloatToInt(Math.Max(continuousAttackAction.continuousDeltaHp * _source.DeltaTimeForPause, 1f)),
              DamageType = DamageData.eDamageType.NONE,
              ActionType = eActionType.CONTINUOUS_ATTACK
            }, false, continuousAttackAction.ActionId, _hasEffect: false, _energyAdd: false, _energyChargeMultiple: continuousAttackAction.EnergyChargeMultiple);
          if ((long) _source.Hp <= 0L)
            break;
        }
        if (!_source.GetCurrentSpineCtrl().IsPlayAnimeBattle)
        {
          if (!flag)
          {
            flag = true;
            _sourceActionController.CreateNormalPrefabWithTargetMotion(skill, 1, false);
          }
          _source.PlayAnime(skill.AnimId, skill.SkillNum, 1, _isLoop: false);
        }
        if (continuousAttackAction.continuousEnd)
        {
          for (int index = 0; index < skill.LoopEffectObjs.Count; ++index)
            skill.LoopEffectObjs[index].SetTimeToDie(true);
          skill.LoopEffectObjs.Clear();
          if (continuousAttackAction.ContinuousTargetCount == 0)
            goto label_18;
        }
        yield return null;
      }
      for (int index = 0; index < skill.LoopEffectObjs.Count; ++index)
        skill.LoopEffectObjs[index].SetTimeToDie(true);
      skill.LoopEffectObjs.Clear();
      continuousAttackAction.continuousDeltaHp = 0.0f;
      continuousAttackAction.continuousEnd = false;
      yield break;
label_18:
      continuousAttackAction.continuousDeltaHp = 0.0f;
      continuousAttackAction.continuousEnd = false;
      _source.SetState(UnitCtrl.ActionState.IDLE);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      Value[eValueNumber.VALUE_1] = (float) (MasterData.action_value_1 + MasterData.action_value_2 * _level);
      Value[eValueNumber.VALUE_3] = (float) (MasterData.action_value_3 + MasterData.action_value_4 * _level);
    }

    public enum eContinuouAttackType
    {
      HP_RESUCE = 1,
      ENERGY_REDUCE = 2,
      NONE = 3,
    }
  }
}
