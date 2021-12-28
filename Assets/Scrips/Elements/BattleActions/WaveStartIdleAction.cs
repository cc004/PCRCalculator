// Decompiled with JetBrains decompiler
// Type: Elements.WaveStartIdleAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class WaveStartIdleAction : ActionParameter
  {
    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController) => base.ExecActionOnStart(_skill, _source, _sourceActionController);

    public override void ExecActionOnWaveStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      if ((_source.IsOther ? this.battleManager.EnemyList : this.battleManager.UnitList).FindAll((Predicate<UnitCtrl>) (e => (UnityEngine.Object) e != (UnityEngine.Object) _source && (long) e.Hp > 0L)).Count == 0)
      {
        _source.SkillAreaWidthList[_skill.SkillId] = _source.SearchAreaSize;
      }
      else
      {
        _source.IdleOnly = true;
        _source.transform.SetLocalPosX((float) ((_source.IsOther ? -1.0 : 1.0) * -1400.0));
        _source.GetCurrentSpineCtrl().CurColor = new Color(1f, 1f, 1f, 0.0f);
        _sourceActionController.AppendCoroutine(this.updateIdle(_source), ePauseType.SYSTEM, _source);
      }
    }

    private IEnumerator updateIdle(UnitCtrl _source)
    {
      WaveStartIdleAction waveStartIdleAction = this;
      _source.IsMoveSpeedForceZero = (ObscuredBool) true;
      _source.SetLeftDirection(_source.IsOther);
      float time = waveStartIdleAction.Value[eValueNumber.VALUE_1] + (waveStartIdleAction.battleManager.IsBossBattle ? waveStartIdleAction.battleManager.GetBossUnit().UnitAppearDelay : 0.0f);
      while (true)
      {
        time -= _source.DeltaTimeForPause;
        if (waveStartIdleAction.battleManager.GameState != eBattleGameState.WAIT_WAVE_END)
        {
          if ((double) time >= 0.0)
            yield return (object) null;
          else
            goto label_4;
        }
        else
          break;
      }
      _source.GetCurrentSpineCtrl().CurColor = Color.white;
      _source.IsMoveSpeedForceZero = (ObscuredBool) false;
      yield break;
label_4:
      _source.IsMoveSpeedForceZero = (ObscuredBool) false;
      _source.SetLeftDirection(_source.IsOther);
      _source.IdleOnly = false;
      _source.SetState(UnitCtrl.ActionState.WALK);
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
      BattleSpineController currentSpineCtrl = _source.GetCurrentSpineCtrl();
      if ((double) currentSpineCtrl.CurColor.a != 0.0)
        return;
      currentSpineCtrl.CurColor = Color.white;
    }
  }
}
