// Decompiled with JetBrains decompiler
// Type: Elements.ModeChangeAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class ModeChangeAction : ActionParameter
    {
        public bool ReleaseReady;

        private int oldUnionBurstId
        {
            get;
            set;
        }
        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            if (ActionDetail1 != 3)
            {
                eSpineType spineType = eSpineType.SD_MODE_CHANGE;
                /*
                BattleSpineController.LoadCreate(_source.IsShadow ? eSpineType.SD_SHADOW_MODE_CHANGE : eSpineType.SD_MODE_CHANGE, _source.SoundUnitId, _source.SkinRarity, _source.transform.TargetTransform, (Action<BattleSpineController>)(_obj =>
               {
                   _obj.gameObject.SetActive(false);
                   _obj.transform.localScale = new Vector3(_source.Scale, _source.Scale, 1f);
                   _obj.Owner = _source;
                   _obj.SetAnimeEventDelegateForBattle((Action)(() => _obj.IsStopState = true), 1);
                   _source.UnitSpineCtrlModeChange = _obj;
               }));*/
                MyGameCtrl.CreateModeChangeSpine(_source.IsShadow ? eSpineType.SD_SHADOW_MODE_CHANGE : eSpineType.SD_MODE_CHANGE, _source.SoundUnitId, 5 /* force use 3x skin */, _source.transform.TargetTransform, _obj =>
                {
                    _obj.gameObject.SetActive(false);
                    _obj.transform.localScale = new Vector3(_source.Scale, _source.Scale, 1f);
                    _obj.Owner = _source;
                    _obj.SetAnimeEventDelegateForBattle(() => _obj.IsStopState = true, 1);
                    _source.UnitSpineCtrlModeChange = _obj;
                    _source.StateBoneModeChange = _obj.skeleton.FindBone("State");
                    _source.CenterBoneModeChange = _obj.skeleton.FindBone("Center");

                });
                _skill.IsModeChange = true;
            }
            _source.CreateAttackPattern(
                //ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData[_source.SoundUnitId], 
                _source.unitParameter.SkillData,
                ActionDetail2);
            if (_sourceActionController.Skill1IsChargeTime)
                return;
            _sourceActionController.Skill1IsChargeTime = _skill.SkillId == _source.UnionBurstSkillId && ActionDetail1 == 2;
        }
        public override void ReadyAction(UnitCtrl _source, UnitActionController _sourceActionController, Skill _skill)
        {
            base.ReadyAction(_source, _sourceActionController, _skill);
            eModeChangeType actionDetail = (eModeChangeType)base.ActionDetail1;
            if (actionDetail == eModeChangeType.RELEASE)
            {
                ReleaseReady = false;
            }
        }
        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
          System.Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            switch ((eModeChangeType)ActionDetail1)
            {
                case eModeChangeType.TIME:
                case eModeChangeType.ENERGY:
                    if (_valueDictionary[eValueNumber.VALUE_4] == 1f)
                    {
                        _source.ModeChangeUnableStateBarrier = true;
                    }
                    if (ActionDetail3 == 0)
                        _sourceActionController.DisableUBByModeChange = true;
                    _sourceActionController.ModeChanging = true;
                    ActionParameter _action = null;
                    int index = 0;
                    for (int count = _skill.ActionParameters.Count; index < count; ++index)
                    {
                        ActionParameter actionParameter = _skill.ActionParameters[index];
                        if (actionParameter.ActionType == eActionType.MODE_CHANGE && actionParameter.ActionDetail1 == 3)
                        {
                            _action = actionParameter;
                            _action.TargetList = new List<BasePartsData>(TargetList);
                        }
                    }
                    _source.OnMotionPrefixChanged = () =>
                    {
                        if (_valueDictionary[eValueNumber.VALUE_3] != 0f)
                        {
                            oldUnionBurstId = _source.UnionBurstSkillId;
                            _source.ChangeChargeSkill((int)_valueDictionary[eValueNumber.VALUE_3], 0f);
                            _source.IsSubUnionBurstMode = true;
                        }
                        _source.ChangeAttackPattern(ActionDetail2, _skill.Level);
                        _source.OnMotionPrefixChanged = null;
                    };
                    if (_action == null)
                        break;
                    _sourceActionController.AppendCoroutine(updateModeChange(_valueDictionary[eValueNumber.VALUE_1], (eModeChangeType)ActionDetail1, _action, _skill, _source, _sourceActionController), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
                    break;
                case eModeChangeType.RELEASE:
                    if (ReleaseReady)
                    {
                        _source.ChangeAttackPattern(base.ActionDetail2, _skill.Level);
                    }
                    else
                    {
                        ReleaseReady = true;
                    }
                    break;
            }
            action("改变攻击模式");
        }

        /*private IEnumerator updateModeChange(
          float _value,
          eModeChangeType _type,
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
                    yield return null;
                }
                else
                {
                    while (true)
                    {
                        switch (_type)
                        {
                            case eModeChangeType.TIME:
                                timer += _source.DeltaTimeForPause;
                                if (timer < (double)_value || _source.CurrentState != UnitCtrl.ActionState.IDLE)
                                    break;
                                goto label_7;
                            case eModeChangeType.ENERGY:
                                _source.IsReduceEnergyDictionary[UnitCtrl.eReduceEnergyType.MODE_CHANGE] = true;
                                _source.SetEnergy((float)_source.Energy - _source.DeltaTimeForPause * _value, eSetEnergyType.BY_MODE_CHANGE);
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
                                yield return null;
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
                    _source.ActionsTargetOnMe.Remove(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                    modeChangeAction.battleManager.CallbackActionEnd(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                    yield break;
                label_17:
                    modeChangeAction.modeChangeEnd(_skill, _action, _source, _sourceActionController);
                    yield break;
                }
            }
            _source.ActionsTargetOnMe.Remove(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            modeChangeAction.battleManager.CallbackActionEnd(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            _sourceActionController.DisableUBByModeChange = false;
            _sourceActionController.ModeChanging = false;
        }*/
        private IEnumerator updateModeChange(float _value, eModeChangeType _type, ActionParameter _action, Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            float timer = 0f;
            bool endFlag = false;
            while (true)
            {
                if ((long)_source.Hp == 0L || _source.IsUnableActionState())
                {
                    _source.ActionsTargetOnMe.Remove((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    base.battleManager.CallbackActionEnd((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    endModeChangeFlags(_sourceActionController, _source);
                    yield break;
                }
                if (_source.MotionPrefix == 1)
                {
                    break;
                }
                yield return null;
            }
            while (true)
            {
                switch (_type)
                {
                    case eModeChangeType.TIME:
                        if (base.battleManager.GetBlackOutUnitLength() == 0)
                        {
                            timer += _source.DeltaTimeForPause;
                        }
                        if (timer >= _value && _source.CurrentState == UnitCtrl.ActionState.IDLE)
                        {
                            modeChangeEnd(_skill, _action, _source, _sourceActionController);
                            yield break;
                        }
                        if (_source.ToadDatas.Count > 0)
                        {
                            modeChangeEnd(_skill, _action, _source, _sourceActionController);
                            yield break;
                        }
                        break;
                    case eModeChangeType.ENERGY:
                        _source.IsReduceEnergyDictionary[UnitCtrl.eReduceEnergyType.MODE_CHANGE] = true;
                        _source.SetEnergy(_source.Energy - _source.DeltaTimeForPause * _value, eSetEnergyType.BY_MODE_CHANGE);
                        if (_source.Energy == 0f)
                        {
                            endFlag = true;
                        }
                        if (_source.ToadDatas.Count > 0)
                        {
                            modeChangeEnd(_skill, _action, _source, _sourceActionController);
                            yield break;
                        }
                        if (_source.CurrentState == UnitCtrl.ActionState.IDLE && endFlag)
                        {
                            modeChangeEnd(_skill, _action, _source, _sourceActionController);
                            yield break;
                        }
                        break;
                }
                if ((long)_source.Hp == 0L)
                {
                    _source.ActionsTargetOnMe.Remove((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    base.battleManager.CallbackActionEnd((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    yield break;
                }
                if (_source.IdleOnly && (long)_source.Hp > 0)
                {
                    break;
                }
                yield return null;
            }
            modeChangeEnd(_skill, _action, _source, _sourceActionController);
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
                _source.ActionsTargetOnMe.Remove(ActionId * 100L + IdOffsetDictionary[_source.GetFirstParts(true)]);
                battleManager.CallbackActionEnd(ActionId * 100L + IdOffsetDictionary[_source.GetFirstParts(true)]);
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
                _sourceUnitActionController.AppendCoroutine(updateModeChangeEnd(_skill, _source, _sourceUnitActionController), ePauseType.SYSTEM);
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

        /*private IEnumerator updateModeChangeEnd(
          Skill skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            ModeChangeAction modeChangeAction = this;
            while ((long)_source.Hp != 0L)
            {
                if (_source.ToadDatas.Count > 0)
                {
                    _source.ActionsTargetOnMe.Remove(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                    modeChangeAction.battleManager.CallbackActionEnd(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
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

                if (_source.IsUnableActionState())
                    modeChangeAction.stopEndEffects(_source);
                if (!_source.UnitSpineCtrlModeChange.IsPlayAnimeBattle)
                {
                    _source.ActionsTargetOnMe.Remove(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                    modeChangeAction.battleManager.CallbackActionEnd(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
                    _source.MotionPrefix = -1;
                    _source.UnitSpineCtrl.gameObject.SetActive(true);
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

                    _source.PlayAnimeNoOverlap(skill.AnimId, skill.SkillNum, 1);
                    _sourceActionController.AppendCoroutine(modeChangeAction.updateModeChangeEnd2(_source, _sourceActionController), ePauseType.VISUAL);
                    yield break;
                }

                yield return null;
            }
            _source.ActionsTargetOnMe.Remove(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            modeChangeAction.battleManager.CallbackActionEnd(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            _sourceActionController.DisableUBByModeChange = false;
            _sourceActionController.ModeChanging = false;
            _source.CurrentState = UnitCtrl.ActionState.IDLE;
            _source.SetState(UnitCtrl.ActionState.DIE);
        }*/
        private IEnumerator updateModeChangeEnd(Skill skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            while (true)
            {
                if ((long)_source.Hp == 0L)
                {
                    _source.ActionsTargetOnMe.Remove((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    base.battleManager.CallbackActionEnd((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    endModeChangeFlags(_sourceActionController, _source);
                    _source.CurrentState = UnitCtrl.ActionState.IDLE;
                    _source.SetState(UnitCtrl.ActionState.DIE);
                    yield break;
                }
                if (_source.ToadDatas.Count > 0)
                {
                    _source.ActionsTargetOnMe.Remove((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    base.battleManager.CallbackActionEnd((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    _source.MotionPrefix = -1;
                    if (_source.IsSubUnionBurstMode)
                    {
                        _source.ChangeChargeSkill(oldUnionBurstId, 0f);
                    }
                    _source.IsSubUnionBurstMode = false;
                    _source.UnitSpineCtrl.CurColor = _source.UnitSpineCtrl.CurColor;
                    Color curColor = _source.UnitSpineCtrl.CurColor;
                    curColor.a = 1f;
                    _source.UnitSpineCtrl.CurColor = curColor;
                    _source.UnitSpineCtrl.SetActive(_isActive: false);
                    if (_source.IsFront)
                    {
                        _source.SetSortOrderFront();
                    }
                    else
                    {
                        _source.SetSortOrderBack();
                    }
                    _source.ModeChangeEnd = false;
                    _source.CancelByConvert = false;
                    _source.CancelByToad = false;
                    _sourceActionController.DisableUBByModeChange = false;
                    stopEndEffects(_source, _sourceActionController);
                    yield break;
                }
                if (_source.IsUnableActionState())
                {
                    stopEndEffects(_source, _sourceActionController);
                }
                if (!_source.UnitSpineCtrlModeChange.IsPlayAnimeBattle)
                {
                    break;
                }
                yield return null;
            }
            _source.ActionsTargetOnMe.Remove((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
            base.battleManager.CallbackActionEnd((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
            _source.MotionPrefix = -1;
            if (_source.IsSubUnionBurstMode)
            {
                _source.ChangeChargeSkill(oldUnionBurstId, 0f);
            }
            _source.IsSubUnionBurstMode = false;
            _source.UnitSpineCtrlModeChange.gameObject.SetActive(value: false);
            //_source.UnitSpineCtrl.IsAlphaZero = false;
            //_source.UnitSpineCtrl.SetCurColor(_source.UnitSpineCtrlModeChange.CurColor);
            if (_source.IsFront)
            {
                _source.SetSortOrderFront();
            }
            else
            {
                _source.SetSortOrderBack();
            }
            if (!_source.UnitSpineCtrl.IsAnimation(_source.UnitSpineCtrl.ConvertAnimeIdToAnimeName(skill.AnimId, skill.SkillNum, 1)))
            {
                _source.ModeChangeEnd = false;
                _source.SkillEndProcess();
                _source.PlayAnime(eSpineCharacterAnimeId.IDLE, _source.MotionPrefix);
                yield break;
            }
            if (!_source.Pause)
            {
                _source.GetCurrentSpineCtrl().Resume();
            }
            _source.PlayAnimeNoOverlap(skill.AnimId, skill.SkillNum, 1);
            _sourceActionController.StopModeChangeEndEffectCalled = false;
            _sourceActionController.AppendCoroutine(updateModeChangeEnd2(_source, _sourceActionController), ePauseType.VISUAL);
        }


        private void stopEndEffects(UnitCtrl _source, UnitActionController _unitActionController)
        {
            _unitActionController.StopModeChangeEndEffectCalled = true;
            for (int num = _source.ModeChangeEndEffectList.Count - 1; num >= 0; num--)
            {
                _source.ModeChangeEndEffectList[num].SetTimeToDie(value: true);
                _source.ModeChangeEndEffectList.RemoveAt(num);
            }
        }
        private IEnumerator updateModeChangeEnd2(UnitCtrl _source, UnitActionController _sourceActionController)
        {
            while (true)
            {
                if ((long)_source.Hp == 0L)
                {
                    _source.ActionsTargetOnMe.Remove((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    base.battleManager.CallbackActionEnd((long)base.ActionId * 100L + base.IdOffsetDictionary[_source.GetFirstParts(_owner: true)]);
                    endModeChangeFlags(_sourceActionController, _source);
                    _source.CurrentState = UnitCtrl.ActionState.IDLE;
                    _source.SetState(UnitCtrl.ActionState.DIE);
                    yield break;
                }
                if (_source.IsUnableActionState())
                {
                    stopEndEffects(_source, _sourceActionController);
                }
                if (!_source.UnitSpineCtrl.IsPlayAnimeBattle || _source.ToadDatas.Count > 0)
                {
                    break;
                }
                yield return null;
            }
            _source.ModeChangeEnd = false;
            _source.SkillEndProcess();
            _source.PlayAnime(eSpineCharacterAnimeId.IDLE, _source.MotionPrefix);
            stopEndEffects(_source, _sourceActionController);
        }
        private void endModeChangeFlags(UnitActionController _unitActionController, UnitCtrl _unit)
        {
            _unitActionController.ModeChanging = false;
            _unit.ModeChangeUnableStateBarrier = false;
            _unitActionController.DisableUBByModeChange = false;
        }
        /*private IEnumerator updateModeChangeEnd2(
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

                yield return null;
            }
            _source.ActionsTargetOnMe.Remove(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            modeChangeAction.battleManager.CallbackActionEnd(modeChangeAction.ActionId * 100L + modeChangeAction.IdOffsetDictionary[_source.GetFirstParts(true)]);
            _sourceActionController.DisableUBByModeChange = false;
            _sourceActionController.ModeChanging = false;
            _source.CurrentState = UnitCtrl.ActionState.IDLE;
            _source.SetState(UnitCtrl.ActionState.DIE);
        }*/

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
        }

        public enum eModeChangeType
        {
            TIME = 1,
            ENERGY = 2,
            RELEASE = 3,
        }
    }
}
