// Decompiled with JetBrains decompiler
// Type: Elements.ChargeAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class ChargeAction : ActionParameter
  {
    private const int LOOP_MOTION_NUMBER = 1;
    private const int END_MOTION_NUMBER = 2;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      if (_sourceActionController.Skill1IsChargeTime)
        return;
      _sourceActionController.Skill1IsChargeTime = _skill.SkillId == _source.UnionBurstSkillId && this.ActionDetail1 == 1;
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
      if (this.ActionDetail1 == 1)
      {
        _sourceActionController.Skill1Charging = true;
        _source.SetEnergy((float) UnitDefine.MAX_ENERGY, eSetEnergyType.BY_CHARGE_SKILL);
      }
      _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 1, false);
      _sourceActionController.AppendCoroutine(this.playMotionLoopForCharge(_skill.AnimId, _skill, _source, _sourceActionController), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
    }

    private IEnumerator playMotionLoopForCharge(
      eSpineCharacterAnimeId _animeId,
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      ChargeAction chargeAction = this;
      float timer = 0.0f;
      Dictionary<eValueNumber, FloatWithEx> additionalValue = (Dictionary<eValueNumber, FloatWithEx>) null;
      if (chargeAction.ActionDetail2 != 0)
      {
        // ISSUE: reference to a compiler-generated method
        additionalValue = _skill.ActionParameters.Find(a=>a.ActionId == ActionDetail2).AdditionalValue;
      }
      eValueNumber targetValue = (eValueNumber) ((int) chargeAction.Value[eValueNumber.VALUE_4] + 1);
      additionalValue = new Dictionary<eValueNumber, FloatWithEx>();
      additionalValue.Add(targetValue, 0.0f);
      while (true)
      {
        if (!_source.GetCurrentSpineCtrl().IsPlayAnimeBattle)
          _source.PlayAnime(_skill.AnimId, _skill.SkillNum, 1);
        if (!_skill.Cancel)
        {
          switch ((ChargeAction.eChargeActionType) chargeAction.ActionDetail1)
          {
            case ChargeAction.eChargeActionType.TAP:
              timer += chargeAction.battleManager.DeltaTime_60fps;
              if (chargeAction.battleManager.BattleCategory == eBattleCategory.ARENA || chargeAction.battleManager.BattleCategory == eBattleCategory.ARENA_REPLAY || (chargeAction.battleManager.BattleCategory == eBattleCategory.GRAND_ARENA || chargeAction.battleManager.BattleCategory == eBattleCategory.GRAND_ARENA_REPLAY) || chargeAction.battleManager.BattleCategory == eBattleCategory.FRIEND)
                additionalValue[targetValue] = chargeAction.Value[eValueNumber.VALUE_1] * chargeAction.Value[eValueNumber.VALUE_3];
              else
                additionalValue[targetValue] += chargeAction.battleManager.DeltaTime_60fps * chargeAction.Value[eValueNumber.VALUE_1];
              if ((double) timer > (double) chargeAction.Value[eValueNumber.VALUE_3] || !_sourceActionController.Skill1Charging)
                goto label_14;
              else
                break;
            case ChargeAction.eChargeActionType.MOVE:
              if (!_sourceActionController.MoveEnd)
                break;
              goto label_8;
          }
          yield return (object) null;
        }
        else
          break;
      }
      yield break;
label_8:
      chargeAction.chargeEnd(_animeId, _skill, _sourceActionController, _source);
      yield break;
label_14:
      for (int index = 0; index < _skill.LoopEffectObjs.Count; ++index)
        _skill.LoopEffectObjs[index].SetTimeToDie(true);
      _skill.LoopEffectObjs.Clear();
      _sourceActionController.Skill1Charging = false;
      _source.SetEnergy(0.0f, eSetEnergyType.BY_CHARGE_SKILL);
      chargeAction.chargeEnd(_animeId, _skill, _sourceActionController, _source);
    }

    private void chargeEnd(
      eSpineCharacterAnimeId _animeId,
      Skill _skill,
      UnitActionController _sourceUnitActionController,
      UnitCtrl _source)
    {
      if (this.ActionDetail2 == 0)
        return;
      ActionParameter _action = _skill.ActionParameters.Find((Predicate<ActionParameter>) (e => e.ActionId == this.ActionDetail2));
      _sourceUnitActionController.ExecUnitActionWithDelay(_action, _skill, false, false);
      _source.PlayAnime(_animeId, _skill.SkillNum, 2, _isLoop: false);
      _sourceUnitActionController.CreateNormalPrefabWithTargetMotion(_skill, 2, false);
      //for (int index = 0; index < _skill.ShakeEffects.Count; ++index)
      //{
      //  if (_skill.ShakeEffects[index].TargetMotion == 2)
      //    _sourceUnitActionController.AppendCoroutine(_sourceUnitActionController.StartShakeWithDelay(_skill.ShakeEffects[index], _skill), ePauseType.VISUAL, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
      //}
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
    }

    private enum eChargeActionType
    {
      TAP = 1,
      MOVE = 2,
    }
  }
}
