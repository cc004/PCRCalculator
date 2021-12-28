// Decompiled with JetBrains decompiler
// Type: Elements.DamageChargeAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class DamageChargeAction : ActionParameter
    {
        private const int LOOP_MOTION_NUMBER = 1;
        private float defaultValue;
        private ActionParameter targetAction;

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            switch (this.ActionDetail1)
            {
                case 1:
                case 3:
                    _source.OnDamage += (UnitCtrl.OnDamageDelegate)((byAttack, damage, critical) =>
                   {
                       if (!byAttack || !_source.IsOnDamageCharge || this.ActionDetail2 == 0)
                           return;
                       this.targetAction.Value[eValueNumber.VALUE_1] += damage * this.Value[eValueNumber.VALUE_1];
                   });
                    if (this.ActionDetail2 == 0)
                        break;
                    this.targetAction = _skill.ActionParameters.Find((Predicate<ActionParameter>)(e => e.ActionId == this.ActionDetail2));
                    this.defaultValue = this.targetAction.Value[eValueNumber.VALUE_1];
                    break;
                case 2:
                    ActionParameter actionParameter = _skill.ActionParameters.Find((Predicate<ActionParameter>)(x => x.ActionId == this.ActionDetail3));
                    actionParameter.OnDamageHit += (ActionParameter.OnDamageHitDelegate)(damage =>
                   {
                       for (int index = 0; index < this.ActionChildrenIndexes.Count; ++index)
                           _skill.ActionParameters[this.ActionChildrenIndexes[index]].Value[eValueNumber.VALUE_1] += damage * this.Value[eValueNumber.VALUE_1];
                   });
                    actionParameter.OnActionEnd += (ActionParameter.OnActionEndDelegate)(() =>
                   {
                       for (int index = 0; index < this.ActionChildrenIndexes.Count; ++index)
                           _sourceActionController.ExecUnitActionWithDelay(_skill.ActionParameters[this.ActionChildrenIndexes[index]], _skill, false, false);
                   });
                    if (this.ActionDetail2 == 0)
                        break;
                    ActionParameter toAction = _skill.ActionParameters.Find((Predicate<ActionParameter>)(e => e.ActionId == this.ActionDetail2));
                    float defaultValue = toAction.Value[eValueNumber.VALUE_1];
                    toAction.OnActionEnd += (ActionParameter.OnActionEndDelegate)(() => toAction.Value[eValueNumber.VALUE_1] = defaultValue);
                    break;
            }
        }

        public override void ReadyAction(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            base.ReadyAction(_source, _sourceActionController, _skill);
            if (this.targetAction == null)
                return;
            this.targetAction.Value[eValueNumber.VALUE_1] = this.defaultValue;
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
            switch (this.ActionDetail1)
            {
                case 1:
                case 3:
                    _source.IsOnDamageCharge = true;
                    _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 1, false);
                    _sourceActionController.AppendCoroutine(this.PlayMotionLoopForDamageCharge(_skill.AnimId, _skill, _source, _sourceActionController, _skill), ePauseType.SYSTEM, _source);
                    break;
            }
        }

        private IEnumerator PlayMotionLoopForDamageCharge(
          eSpineCharacterAnimeId animeId,
          Skill skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            DamageChargeAction damageChargeAction = this;
            float timer = 0.0f;
            while (!damageChargeAction.isActionCancel(_source, _sourceActionController, _skill))
            {
                if (!_source.GetCurrentSpineCtrl().IsPlayAnimeBattle)
                {
                    _source.PlayAnime(skill.AnimId, skill.SkillNum, 1);
                    List<SkillEffectCtrl> skillEffectList = new List<SkillEffectCtrl>();
                    for (int index = 0; index < damageChargeAction.ActionEffectList.Count; ++index)
                    {
                        GameObject MDOJNMEMHLN = _source.IsLeftDir ? damageChargeAction.ActionEffectList[index].PrefabLeft : damageChargeAction.ActionEffectList[index].Prefab;
                        SkillEffectCtrl effect = damageChargeAction.battleEffectPool.GetEffect(MDOJNMEMHLN);
                        effect.transform.parent = ExceptNGUIRoot.Transform;
                        effect.InitializeSort();
                        //effect.PlaySe(_source.SoundUnitId, _source.IsLeftDir);
                        effect.SetPossitionAppearanceType(damageChargeAction.ActionEffectList[index], _source.GetFirstParts(true), _source, _skill);
                        effect.ExecAppendCoroutine(_source);
                        skillEffectList.Add(effect);
                    }
                    Action stopEffect = (Action)(() =>
                   {
                       for (int index = 0; index < skillEffectList.Count; ++index)
                           skillEffectList[index].SetTimeToDie(true);
                       for (int index = 0; index < _skill.LoopEffectObjs.Count; ++index)
                           _skill.LoopEffectObjs[index].SetTimeToDie(true);
                       _skill.LoopEffectObjs.Clear();
                   });
                    while (true)
                    {
                        timer += damageChargeAction.battleManager.DeltaTime_60fps;
                        if (!damageChargeAction.isActionCancel(_source, _sourceActionController, _skill))
                        {
                            if ((long)_source.Hp > 0L)
                            {
                                if ((double)timer <= (double)damageChargeAction.Value[eValueNumber.VALUE_3])
                                    yield return (object)null;
                                else
                                    goto label_14;
                            }
                            else
                                goto label_12;
                        }
                        else
                            break;
                    }
                    _source.IsOnDamageCharge = false;
                    //stopEffect.Call();
                    break;
                label_12:
                    _source.SetState(UnitCtrl.ActionState.IDLE);
                    _sourceActionController.CancelAction(_skill.SkillId);
                    //stopEffect.Call();
                    break;
                label_14:
                    //stopEffect.Call();
                    _source.PlayAnime(animeId, skill.SkillNum, 2, _isLoop: false);
                    _sourceActionController.AppendCoroutine(damageChargeAction.waitMotinEnd(_source, _skill, _sourceActionController), ePauseType.SYSTEM, _source);
                    _sourceActionController.CreateNormalPrefabWithTargetMotion(skill, 2, false);
                    /*for (int index = 0; index < skill.ShakeEffects.Count; ++index)
                    {
                      if (skill.ShakeEffects[index].TargetMotion == 2)
                        _sourceActionController.AppendCoroutine(_sourceActionController.StartShakeWithDelay(skill.ShakeEffects[index], skill, true), ePauseType.VISUAL, _source);
                    }*/
                    _source.IsOnDamageCharge = false;
                    break;
                }
                yield return (object)null;
            }
        }

        private IEnumerator waitMotinEnd(
          UnitCtrl _source,
          Skill _skill,
          UnitActionController _sourceActionController)
        {
            DamageChargeAction damageChargeAction = this;
            while (!damageChargeAction.isActionCancel(_source, _sourceActionController, _skill))
            {
                if (!_source.UnitSpineCtrl.IsPlayAnimeBattle)
                {
                    if (_skill.BlackoutEndWithMotion)
                        damageChargeAction.battleManager.SetBlackoutTimeZero();
                    _source.SkillEndProcess();
                    break;
                }
                yield return (object)null;
            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
            var group = MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup();
            if (ActionId>=300000000&& group.isSpecialBoss &&( group.specialBossID == 666666 ||group.specialBossID==666667))
            {
                Value[eValueNumber.VALUE_1] = 0f;
                Value[eValueNumber.VALUE_3] = 82.5f;
            }

        }

        private bool isActionCancel(
      UnitCtrl _source,
      UnitActionController _sourceActionController,
      Skill _skill)
        {
            if (!_source.IsUnableActionState() && !_source.IsCancelActionState(_skill.SkillId == _source.UnionBurstSkillId, _skill.SkillId))
                return false;
            _source.CancelByAwake = false;
            _source.CancelByConvert = false;
            _source.CancelByToad = false;
            _sourceActionController.CancelAction(_skill.SkillId);
            return true;
        }

        private enum eDamageChargeType
        {
            PASSIVE_DAMAGE = 1,
            ACTIVE_DAMAGE = 2,
            PASSIVE_DAMAGE_LOOP = 3,
        }
    }
}
