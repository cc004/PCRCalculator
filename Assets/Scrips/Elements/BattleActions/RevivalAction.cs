// Decompiled with JetBrains decompiler
// Type: Elements.RevivalAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class RevivalAction : ActionParameter
  {
    private bool revivalEnd;

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      if (this.ActionDetail1 != 2)
        return;
      this.Value[eValueNumber.VALUE_1] -= (float) _source.SkillUseCount[_skill.SkillId];
      if ((double) this.Value[eValueNumber.VALUE_1] != 0.0)
        return;
      _source.HasUnDeadTime = false;
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
      _target.Owner.EnemyPointDone = false;
      switch ((RevivalAction.eRevivalType) this.ActionDetail1)
      {
        case RevivalAction.eRevivalType.NORMAL:
                    _target.Owner.SetRecovery((int)((double)(long)_target.Owner.MaxHp * (double)_valueDictionary[eValueNumber.VALUE_2]), UnitCtrl.eInhibitHealType.NO_EFFECT, _source, _isEffect: (this.EffectType == eEffectType.COMMON), _isRevival: true, _useNumberEffect: (this.ActionDetail2 == 1));
                    break;
        case RevivalAction.eRevivalType.PHOENIX:
          _sourceActionController.AppendCoroutine(this.updateRevival(_skill, (int) _valueDictionary[eValueNumber.VALUE_1], _valueDictionary[eValueNumber.VALUE_2], _target.Owner, _valueDictionary[eValueNumber.VALUE_4], _source, _sourceActionController), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
          _source.UnDeadTimeHitCount = (int) _valueDictionary[eValueNumber.VALUE_1] + 1;
          --this.Value[eValueNumber.VALUE_1];
          break;
      }
    }

    private IEnumerator updateRevival(
      Skill _skill,
      int _value1,
      float _value2,
      UnitCtrl _target,
      float _time,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      RevivalAction revivalAction = this;
      while (_source.UnDeadTimeHitCount != 1)
      {
        if (!_source.GetCurrentSpineCtrl().IsPlayAnimeBattle)
          _source.PlayAnime(_skill.AnimId, _skill.SkillNum, 1, _isLoop: false);
        if (revivalAction.revivalEnd || revivalAction.battleManager.GameState != eBattleGameState.PLAY)
        {
          if (revivalAction.battleManager.GameState != eBattleGameState.PLAY)
            _target.SetRecovery((int) ((double) _value2 * (double) (long) _target.MaxHp), UnitCtrl.eInhibitHealType.NO_EFFECT, _source, _isRevival: true);
          revivalAction.revivalEnd = false;
          _source.PlayAnime(_skill.AnimId, _skill.SkillNum, 2, _isLoop: false);
          _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 2, false);
          _sourceActionController.AppendCoroutine(revivalAction.waitRevivalEnd(_source), ePauseType.SYSTEM, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
          break;
        }
        _time -= _source.DeltaTimeForPause;
        if ((double) _time < 0.0)
        {
          if (revivalAction.battleManager.GameState == eBattleGameState.PLAY)
            _target.SetRecovery((int) ((double) (long) _target.MaxHp * (double) _value2), UnitCtrl.eInhibitHealType.NO_EFFECT, _source, _isRevival: true);
          revivalAction.revivalEnd = true;
          _source.HasUnDeadTime = (double) revivalAction.Value[eValueNumber.VALUE_1] != 0.0;
          _source.UnDeadTimeHitCount = 0;
        }
        yield return (object) null;
      }
    }

    private IEnumerator waitRevivalEnd(UnitCtrl _source)
    {
      while (_source.GetCurrentSpineCtrl().IsPlayAnimeBattle)
        yield return (object) null;
      _source.SetState(UnitCtrl.ActionState.IDLE);
    }

    public override void SetLevel(float _level)
    {
      base.SetLevel(_level);
      this.Value[eValueNumber.VALUE_1] = (float) ((double) this.MasterData.action_value_1 + (double) this.MasterData.action_value_2 * (double) _level);
      this.Value[eValueNumber.VALUE_3] = (float) ((double) this.MasterData.action_value_3 + (double) this.MasterData.action_value_4 * (double) _level);
    }

    private enum eRevivalType
    {
      NORMAL = 1,
      PHOENIX = 2,
    }

    private enum eDisplayType
    {
      USE_NUMBER = 1,
      UNUSE_NUMBER = 2,
    }
  }
}
