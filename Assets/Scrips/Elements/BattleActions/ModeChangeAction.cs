// Decompiled with JetBrains decompiler
// Type: Elements.ModeChangeAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ModeChangeAction : ActionParameter
    {
        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            if (this.ActionDetail1 != 3)
            {
                /*
                BattleSpineController.LoadCreate(_source.IsShadow ? eSpineType.SD_SHADOW_MODE_CHANGE : eSpineType.SD_MODE_CHANGE, _source.SoundUnitId, _source.SkinRarity, _source.transform.TargetTransform, (Action<BattleSpineController>)(_obj =>
               {
                   _obj.gameObject.SetActive(false);
                   _obj.transform.localScale = new Vector3(_source.Scale, _source.Scale, 1f);
                   _obj.Owner = _source;
                   _obj.SetAnimeEventDelegateForBattle((Action)(() => _obj.IsStopState = true), 1);
                   _source.UnitSpineCtrlModeChange = _obj;
               }));*/
                MyGameCtrl.CreateModeChangeSpine(_source.IsShadow ? eSpineType.SD_SHADOW_MODE_CHANGE : eSpineType.SD_MODE_CHANGE, _source.SoundUnitId, _source.SkinRarity, _source.transform.TargetTransform, (Action<BattleSpineController>)(_obj =>
                {
                    _obj.gameObject.SetActive(false);
                    _obj.transform.localScale = new Vector3(_source.Scale, _source.Scale, 1f);
                    _obj.Owner = _source;
                    _obj.SetAnimeEventDelegateForBattle((Action)(() => _obj.IsStopState = true), 1);
                    _source.UnitSpineCtrlModeChange = _obj;
                }));
                _skill.IsModeChange = true;
            }
            _source.CreateAttackPattern(
                //ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData[_source.SoundUnitId], 
                _source.unitParameter.SkillData,
                this.ActionDetail2);
            if (_sourceActionController.Skill1IsChargeTime)
                return;
            _sourceActionController.Skill1IsChargeTime = _skill.SkillId == _source.UnionBurstSkillId && this.ActionDetail1 == 2;
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
            switch ((ModeChangeAction.eModeChangeType)this.ActionDetail1)
            {
                case ModeChangeAction.eModeChangeType.TIME:
                case ModeChangeAction.eModeChangeType.ENERGY:
                    if (this.ActionDetail3 == 0)
                        _sourceActionController.DisableUBByModeChange = true;
                    _sourceActionController.ModeChanging = true;
                    ActionParameter _action = (ActionParameter)null;
                    int index = 0;
                    for (int count = _skill.ActionParameters.Count; index < count; ++index)
                    {
                        ActionParameter actionParameter = _skill.ActionParameters[index];
                        if (actionParameter.ActionType == eActionType.MODE_CHANGE && actionParameter.ActionDetail1 == 3)
                        {
                            _action = actionParameter;
                            _action.TargetList = new List<BasePartsData>((IEnumerable<BasePartsData>)this.TargetList);
                        }
                    }
                    _source.OnMotionPrefixChanged = (Action)(() =>
                   {
                       _source.ChangeAttackPattern(this.ActionDetail2, _skill.Level);
                       _source.OnMotionPrefixChanged = (Action)null;
                   });
                    if (_action == null)
                        break;
                    _sourceActionController.AppendCoroutine(this.updateModeChange(_valueDictionary[eValueNumber.VALUE_1], (ModeChangeAction.eModeChangeType)this.ActionDetail1, _action, _skill, _source, _sourceActionController), ePauseType.SYSTEM, (double)_skill.BlackOutTime > 0.0 ? _source : (UnitCtrl)null);
                    break;
                case ModeChangeAction.eModeChangeType.RELEASE:
                    _source.ChangeAttackPattern(this.ActionDetail2, _skill.Level);
                    break;
            }
        }

        private IEnumerator updateModeChange(
          float _value,
          ModeChangeAction.eModeChangeType _type,
          ActionParameter _action,
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            ModeChangeAction modeChangeAction = this;
            float timer = 0.0f;
            bool endFlag = false;
            while ((long)_source.Hp != 0L && !_source.IsUnableActionState())
            {
                if (_source.MotionPrefix != 1)
                {
                    yield return (object)null;
                }
                else
                {
                    while (true)
                    {
                        switch (_type)
                        {
                            case ModeChangeAction.eModeChangeType.TIME:
                                timer += _source.DeltaTimeForPause;
                                if ((double)timer < (double)_value || _source.CurrentState != UnitCtrl.ActionState.IDLE)
                                    break;
                                goto label_7;
                            case ModeChangeAction.eModeChangeType.ENERGY:
                                _source.IsReduceEnergyDictionary[UnitCtrl.eReduceEnergyType.MODE_CHANGE] = true;
                                _source.SetEnergy(_source.Energy - _source.DeltaTimeForPause * _value, eSetEnergyType.BY_MODE_CHANGE);
                                if ((double)_source.Energy == 0.0)
                                    endFlag = true;
                                if (_source.ToadDatas.Count <= 0)
                                {
                                    if (!(_source.CurrentState == UnitCtrl.ActionState.IDLE & endFlag))
                                        break;
                                    goto label_13;
                                }
                                else
                                    goto label_11;
                        }
                        if ((long)_source.Hp != 0L)
                        {
                            if (!_source.IdleOnly || (long)_source.Hp <= 0L)
                                yield return (object)null;
                            else
                                goto label_17;
                        }
                        else
                            goto label_15;
                    }
                label_7:
                    modeChangeAction.modeChangeEnd(_skill, _action, _source, _sourceActionController);
                    yield break;
                label_11:
                    modeChangeAction.modeChangeEnd(_skill, _action, _source, _sourceActionController);
                    yield break;
                label_13:
                    modeChangeAction.modeChangeEnd(_skill, _action, _source, _sourceActionController);
                    yield break;
                label_15:
                    _source.ActionsTargetOnMe.Remove((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                    modeChangeAction.battleManager.CallbackActionEnd((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                    yield break;
                label_17:
                    modeChangeAction.modeChangeEnd(_skill, _action, _source, _sourceActionController);
                    yield break;
                }
            }
            _source.ActionsTargetOnMe.Remove((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            modeChangeAction.battleManager.CallbackActionEnd((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            _sourceActionController.DisableUBByModeChange = false;
            _sourceActionController.ModeChanging = false;
        }

        private void modeChangeEnd(
          Skill _skill,
          ActionParameter _action,
          UnitCtrl _source,
          UnitActionController _sourceUnitActionController)
        {
            _source.IsReduceEnergyDictionary[UnitCtrl.eReduceEnergyType.MODE_CHANGE] = false;
            if (_source.ToadDatas.Count > 0)
            {
                _source.ActionsTargetOnMe.Remove((long)this.ActionId * 100L + this.IdOffsetDictionary[_source.GetFirstParts(true)]);
                this.battleManager.CallbackActionEnd((long)this.ActionId * 100L + this.IdOffsetDictionary[_source.GetFirstParts(true)]);
                _source.MotionPrefix = -1;
                _source.UnitSpineCtrl.CurColor = _source.UnitSpineCtrl.CurColor;
                Color curColor = _source.UnitSpineCtrl.CurColor;
                curColor.a = 1f;
                _source.UnitSpineCtrl.CurColor = curColor;
                _source.UnitSpineCtrl.SetActive(false);
                if (_source.IsFront)
                    _source.SetSortOrderFront();
                else
                    _source.SetSortOrderBack();
                _source.CancelByToad = false;
                _source.CancelByConvert = false;
                _sourceUnitActionController.DisableUBByModeChange = false;
                _sourceUnitActionController.ExecUnitActionWithDelay(_action, _skill, false, false);
            }
            else
            {
                _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 1, 1);
                _source.ModeChangeEnd = true;
                _sourceUnitActionController.AppendCoroutine(this.updateModeChangeEnd(_skill, _source, _sourceUnitActionController), ePauseType.SYSTEM);
                _sourceUnitActionController.DisableUBByModeChange = false;
                _sourceUnitActionController.ModeChanging = false;
                _sourceUnitActionController.ExecUnitActionWithDelay(_action, _skill, false, false);
                int _targetmotion = 1;
                _source.ModeChangeEndEffectList.Clear();
                _sourceUnitActionController.CreateNormalPrefabWithTargetMotion(_skill, _targetmotion, false, _modechangeEndEffect: true);
                /*for (int index = 0; index < _skill.ShakeEffects.Count; ++index)
                {
                  if (_skill.ShakeEffects[index].TargetMotion == _targetmotion)
                    _sourceUnitActionController.AppendCoroutine(_sourceUnitActionController.StartShakeWithDelay(_skill.ShakeEffects[index], _skill), ePauseType.VISUAL, (double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
                }*/
            }
        }

        private IEnumerator updateModeChangeEnd(
          Skill skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            ModeChangeAction modeChangeAction = this;
            while ((long)_source.Hp != 0L)
            {
                if (_source.ToadDatas.Count > 0)
                {
                    _source.ActionsTargetOnMe.Remove((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                    modeChangeAction.battleManager.CallbackActionEnd((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                    _source.MotionPrefix = -1;
                    _source.UnitSpineCtrl.CurColor = _source.UnitSpineCtrl.CurColor;
                    Color curColor = _source.UnitSpineCtrl.CurColor;
                    curColor.a = 1f;
                    _source.UnitSpineCtrl.CurColor = curColor;
                    _source.UnitSpineCtrl.SetActive(false);
                    if (_source.IsFront)
                        _source.SetSortOrderFront();
                    else
                        _source.SetSortOrderBack();
                    _source.ModeChangeEnd = false;
                    _source.CancelByConvert = false;
                    _source.CancelByToad = false;
                    _sourceActionController.DisableUBByModeChange = false;
                    yield break;
                }
                else
                {
                    if (_source.IsUnableActionState())
                        modeChangeAction.stopEndEffects(_source);
                    if (!_source.UnitSpineCtrlModeChange.IsPlayAnimeBattle)
                    {
                        _source.ActionsTargetOnMe.Remove((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                        modeChangeAction.battleManager.CallbackActionEnd((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                        _source.MotionPrefix = -1;
                        _source.UnitSpineCtrlModeChange.gameObject.SetActive(false);
                        _source.UnitSpineCtrl.CurColor = _source.UnitSpineCtrlModeChange.CurColor;
                        Color curColor = _source.UnitSpineCtrl.CurColor;
                        curColor.a = 1f;
                        _source.UnitSpineCtrl.CurColor = curColor;
                        if (_source.IsFront)
                            _source.SetSortOrderFront();
                        else
                            _source.SetSortOrderBack();
                        if (!_source.UnitSpineCtrl.IsAnimation(_source.UnitSpineCtrl.ConvertAnimeIdToAnimeName(skill.AnimId, skill.SkillNum, 1)))
                        {
                            _source.ModeChangeEnd = false;
                            _source.SkillEndProcess();
                            _source.PlayAnime(eSpineCharacterAnimeId.IDLE, _source.MotionPrefix);
                            yield break;
                        }
                        else
                        {
                            _source.PlayAnimeNoOverlap(skill.AnimId, skill.SkillNum, 1);
                            _sourceActionController.AppendCoroutine(modeChangeAction.updateModeChangeEnd2(_source, _sourceActionController), ePauseType.VISUAL);
                            yield break;
                        }
                    }
                    else
                        yield return (object)null;
                }
            }
            _source.ActionsTargetOnMe.Remove((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            modeChangeAction.battleManager.CallbackActionEnd((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            _sourceActionController.DisableUBByModeChange = false;
            _sourceActionController.ModeChanging = false;
            _source.CurrentState = UnitCtrl.ActionState.IDLE;
            _source.SetState(UnitCtrl.ActionState.DIE);
        }

        private void stopEndEffects(UnitCtrl _source)
        {
            /*for (int index = _source.ModeChangeEndEffectList.Count - 1; index >= 0; --index)
            {
              _source.ModeChangeEndEffectList[index].SetTimeToDie(true);
              _source.ModeChangeEndEffectList.RemoveAt(index);
            }*/
        }

        private IEnumerator updateModeChangeEnd2(
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            ModeChangeAction modeChangeAction = this;
            while ((long)_source.Hp != 0L)
            {
                if (_source.IsUnableActionState())
                    modeChangeAction.stopEndEffects(_source);
                if (!_source.UnitSpineCtrl.IsPlayAnimeBattle || _source.ToadDatas.Count > 0)
                {
                    _source.ModeChangeEnd = false;
                    _source.SkillEndProcess();
                    _source.PlayAnime(eSpineCharacterAnimeId.IDLE, _source.MotionPrefix);
                    yield break;
                }
                else
                    yield return (object)null;
            }
            _source.ActionsTargetOnMe.Remove((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            modeChangeAction.battleManager.CallbackActionEnd((long)modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            _sourceActionController.DisableUBByModeChange = false;
            _sourceActionController.ModeChanging = false;
            _source.CurrentState = UnitCtrl.ActionState.IDLE;
            _source.SetState(UnitCtrl.ActionState.DIE);
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            this.Value[eValueNumber.VALUE_1] = (float)((double)this.MasterData.action_value_1 + (double)this.MasterData.action_value_2 * (double)_level);
        }

        public enum eModeChangeType
        {
            TIME = 1,
            ENERGY = 2,
            RELEASE = 3,
        }
    }
}
