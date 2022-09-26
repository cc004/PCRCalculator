// Decompiled with JetBrains decompiler
// Type: Elements.CountDownAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class CountDownAction : ActionParameter
  {
    private const int SHAKE_TARGET = 2;

    private ActionParameter actionParameter { get; set; }

    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      actionParameter = _skill.ActionParameters.Find(e => e.ActionId == ActionDetail1);
            actionParameter.IsAlwaysChargeEnegry = true;

        }

    public override void ReadyAction(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
    {
      base.ReadyAction(_source, _sourceActionController, _skill);
      actionParameter.CancelByIfForAll = true;
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
        _target.Owner.AppendCoroutine(
            this.updateCountDown(_valueDictionary[eValueNumber.VALUE_1], null, _target.Owner, _skill,
                _sourceActionController, _source), ePauseType.SYSTEM);
        /*
        for (int index = 0; index < this.ActionEffectList.Count; ++index)
        {
          NormalSkillEffect actionEffect = this.ActionEffectList[index];
          SkillEffectCtrl effect = this.battleEffectPool.GetEffect(_target.Owner.IsLeftDir ? actionEffect.PrefabLeft : actionEffect.Prefab);
          effect.transform.parent = ExceptNGUIRoot.Transform;
          effect.SortTarget = _target.Owner;
          effect.InitializeSort();
          effect.SetPossitionAppearanceType(actionEffect, _target, _target.Owner, _skill);
          effect.ExecAppendCoroutine();
          if (effect is CountDownEffectCtrl)
          {
            CountDownEffectCtrl _countDownEffect = effect as CountDownEffectCtrl;
            _countDownEffect.SetDamageText((int) _valueDictionary[eValueNumber.VALUE_1]);
          }
        }*/
        throw new System.Exception("这个技能鸽了！");
    }

    private IEnumerator updateCountDown(
      float _duration,
      object countDownEffect,
      UnitCtrl _target,
      Skill _skill,
      UnitActionController _sourceUnitActionController,
      UnitCtrl _source)
    {
      CountDownAction countDownAction = this;
      float timer = _duration;
      while ((double) timer > 0.0)
      {
        if (_target.IsDead)
        {
          //_countDownEffect.SetTimeToDie(true);
          yield break;
        }
        else
        {
          timer -= countDownAction.battleManager.DeltaTime_60fps;
          //_countDownEffect.SetDamageText((int) timer);
          yield return null;
        }
      }
      //_countDownEffect.SetTimeToDie(true);
      countDownAction.actionParameter.CancelByIfForAll = false;
      for (int index = countDownAction.actionParameter.TargetList.Count - 1; index >= 0; --index)
      {
        UnitCtrl owner = countDownAction.actionParameter.TargetList[index].Owner;
        if (owner.IsStealth || (owner.IsDead || (long) owner.Hp <= 0L) && !owner.HasUnDeadTime)
          countDownAction.actionParameter.TargetList.RemoveAt(index);
      }
      _sourceUnitActionController.ExecUnitActionWithDelay(countDownAction.actionParameter, _skill, false, false, true);
      for (int index = 0; index < _skill.ShakeEffects.Count; ++index)
      {
        if (_skill.ShakeEffects[index].TargetMotion == 2)
          _sourceUnitActionController.AppendCoroutine(_sourceUnitActionController.StartShakeWithDelay(_skill.ShakeEffects[index], _skill), ePauseType.VISUAL, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
      }
      //for (int index = 0; index < _countDownEffect.CountEndEffectData.Count; ++index)
      //  _target.AppendCoroutine(countDownAction.createEndEffect(_countDownEffect.CountEndEffectData[index], _target, _source), ePauseType.SYSTEM);
    }
    
    /*private IEnumerator createEndEffect(
      CountEndEffectData _endEffect,
      UnitCtrl _target,
      UnitCtrl _source)
    {
      CountDownAction countDownAction = this;
      float time = _endEffect.Time;
      while ((double) time > 0.0)
      {
        time -= countDownAction.battleManager.DeltaTime_60fps;
        yield return (object) null;
      }
      GameObject MDOJNMEMHLN = _target.IsLeftDir ? _endEffect.PrefabLeft : _endEffect.Prefab;
      SkillEffectCtrl effect = countDownAction.battleEffectPool.GetEffect(MDOJNMEMHLN);
      effect.transform.parent = ExceptNGUIRoot.Transform;
      effect.transform.position = _target.BottomTransform.position;
      effect.SortTarget = _target;
      effect.InitializeSort();
      effect.PlaySe(_source.SoundUnitId, _source.IsLeftDir);
      effect.ExecAppendCoroutine();
    }*/

    public override void SetLevel(float _level) => base.SetLevel(_level);
  }
}
