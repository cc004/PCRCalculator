﻿// Decompiled with JetBrains decompiler
// Type: Elements.MoveAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class MoveAction : ActionParameter
    {
        private const int MOVE_BY_VELOCITY_LOOP_MOTION_SUFFIX = 1;
        private const int MOVE_BY_VELOCITY_LOOP_END_MOTION_SUFFIX = 2;
        private const int MOVE_BY_VELOCITY_RETURN_LOOP_MOTION_SUFFIX = 3;
        private const int MOVE_BY_VELOCITY_RETURN_LOOP_MOTION_END_SUFFIX = 4;
        private const float MOVE_POSITION_Y = 1f;

        private ActionParameter endAction { get; set; }

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            endAction = _skill.ActionParameters.Find(e => e.ActionId == ActionDetail2);
        }

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary, Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            if (ActionDetail2 != 0)
                endAction.CancelByIfForAll = true;
            FixedTransformMonoBehavior.FixedTransform sourceTransform = _sourceActionController.transform;
            switch (ActionDetail1)
            {
                case 1:
                    if (_num % 2 == 0)
                    {
                        sourceTransform.SetLocalPosX(_target.GetBottomTransformLocalPosition().x + _target.GetLocalPosition().x);
                        if ((double)_valueDictionary[eValueNumber.VALUE_3] == 1.0)
                            sourceTransform.SetLocalPosY(_target.GetLocalPosition().y);
                        sourceTransform.localPosition += calculatePosotion(_source, _target, _sourceActionController, _valueDictionary);
                        break;
                    }
                    sourceTransform.localPosition = _skill.OwnerReturnPosition;
                    break;
                case 2:
                    if (_num % 2 == 0)
                    {
                        sourceTransform.localPosition += new Vector3(_source.IsLeftDir ? -_valueDictionary[eValueNumber.VALUE_1] : (float)_valueDictionary[eValueNumber.VALUE_1], 0.0f, 0.0f);
                        OnActionEnd = () =>
                        {
                            sourceTransform.localPosition = _skill.OwnerReturnPosition;
                            OnActionEnd = null;
                        };
                        break;
                    }
                    sourceTransform.localPosition = _skill.OwnerReturnPosition;
                    OnActionEnd = null;
                    break;
                case 3:
                    _source.transform.SetLocalPosX(_target.GetBottomTransformLocalPosition().x + _target.GetLocalPosition().x);
                    _source.transform.localPosition += calculatePosotion(_source, _target, _sourceActionController, _valueDictionary);
                    _sourceActionController.AppendCoroutine(resetPositionY(_skill, _source), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
                    break;
                case 4:
                    _source.transform.localPosition += new Vector3(_source.IsLeftDir ? -_valueDictionary[eValueNumber.VALUE_1] : (float)_valueDictionary[eValueNumber.VALUE_1], 0.0f, 0.0f);
                    break;
                case 5:
                    _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 1, _isLoop: true);
                    _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 1, false);
                    _sourceActionController.AppendCoroutine(targetMoveByVerocity(_valueDictionary[eValueNumber.VALUE_1], _valueDictionary[eValueNumber.VALUE_2], _target, _sourceActionController, _source, _skill), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
                    break;
                case 6:
                    _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 1, _isLoop: true);
                    _sourceActionController.AppendCoroutine(absoluteMoveByVerocity(_valueDictionary[eValueNumber.VALUE_1], _valueDictionary[eValueNumber.VALUE_2], _sourceActionController, _source, _skill), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
                    break;
                case 7:
                    if (_num % 2 == 0)
                    {
                        sourceTransform.localPosition += new Vector3(_source.IsOther ? -_valueDictionary[eValueNumber.VALUE_1] : (float)_valueDictionary[eValueNumber.VALUE_1], 0.0f, 0.0f);
                        _source.SetDirectionAuto();
                        break;
                    }
                    sourceTransform.localPosition = _skill.OwnerReturnPosition;
                    break;
            }
            switch ((eMoveType)ActionDetail1)
            {
                case eMoveType.TARGET_POS_RETURN:
                case eMoveType.ABSOLUTE_POS_RETURN:
                case eMoveType.TARGET_POS:
                case eMoveType.ABSOLUTE_POS:
                case eMoveType.ABSOLUTE_MOVE_DONOT_USE_DIRECTION:
                    if (sourceTransform.localPosition.x > (double)BattleDefine.BATTLE_FIELD_SIZE)
                        sourceTransform.SetLocalPosX(BattleDefine.BATTLE_FIELD_SIZE);
                    if (sourceTransform.localPosition.x >= -(double)BattleDefine.BATTLE_FIELD_SIZE)
                        break;
                    sourceTransform.SetLocalPosX(-BattleDefine.BATTLE_FIELD_SIZE);
                    break;
            }
            action?.Invoke($"移动, {((eMoveType)ActionDetail1).GetDescription()}");
        }

        private IEnumerator absoluteMoveByVerocity(
          float _distance,
          float _speed,
          UnitActionController _sourceUnitActionController,
          UnitCtrl _source,
          Skill _skill)
        {
            MoveAction moveAction = this;
            _sourceUnitActionController.MoveEnd = false;
            FixedTransformMonoBehavior.FixedTransform sourceTransform = _sourceUnitActionController.transform;
            float startPosx = sourceTransform.localPosition.x;
            while (true)
            {
                sourceTransform.localPosition += new Vector3((!_source.IsLeftDir ? _speed : -_speed) * moveAction.battleManager.DeltaTime_60fps, 0.0f, 0.0f);
                if (Mathf.Abs(startPosx - sourceTransform.localPosition.x) < (double)_distance)
                    yield return null;
                else
                    break;
            }
            _sourceUnitActionController.MoveEnd = true;
            _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 2);
            _sourceUnitActionController.AppendCoroutine(moveAction.moveByVerocityEnd(_sourceUnitActionController, _source, _skill, _speed), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
        }

        private IEnumerator targetMoveByVerocity(
          float _main,
          float _sub,
          BasePartsData _target,
          UnitActionController _sourceUnitActionController,
          UnitCtrl _source,
          Skill _skill)
        {
            MoveAction moveAction = this;
            _main += _target.GetBodyWidth() / 2f;
            _sourceUnitActionController.MoveEnd = false;
            FixedTransformMonoBehavior.FixedTransform sourceTransform = _sourceUnitActionController.transform;
            bool isRight = sourceTransform.position.x < (double)_target.GetPosition().x;
            bool isUnionBurst = _skill.SkillId == _source.UnionBurstSkillId;
            while (!_source.IsCancelActionState(isUnionBurst) && (isUnionBurst || !_source.IsUnableActionState()))
            {
                sourceTransform.localPosition += new Vector3((!isRight ? -_sub : _sub) * moveAction.battleManager.DeltaTime_60fps, 0.0f, 0.0f);
                if ((_target.GetLocalPosition().x - (double)sourceTransform.localPosition.x) * (isRight ? 1.0 : -1.0) <= _main)
                {
                    _sourceUnitActionController.MoveEnd = true;
                    _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 2);
                    for (int index = 0; index < _skill.LoopEffectObjs.Count; ++index)
                        _skill.LoopEffectObjs[index].SetTimeToDie(true);
                    _skill.LoopEffectObjs.Clear();
                    _skill.LoopEffectAlreadyDone = true;
                    _sourceUnitActionController.AppendCoroutine(moveAction.moveByVerocityEnd(_sourceUnitActionController, _source, _skill, _sub), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
                    yield break;
                }

                yield return null;
            }
            for (int index = 0; index < _skill.LoopEffectObjs.Count; ++index)
                _skill.LoopEffectObjs[index].SetTimeToDie(true);
            _skill.LoopEffectObjs.Clear();
            _skill.LoopEffectAlreadyDone = true;
        }

        private IEnumerator moveByVerocityEnd(
          UnitActionController _sourceActionController,
          UnitCtrl _source,
          Skill _skill,
          float _velocity)
        {
            if (endAction != null)
            {
                endAction.CancelByIfForAll = false;
                _sourceActionController.ExecUnitActionWithDelay(endAction, _skill, false, false);
            }
            _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 2, false);
            while (_source.UnitSpineCtrl.IsPlayAnimeBattle)
                yield return null;
            if (_source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum, 3))
            {
                _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 3, _isLoop: true);
                _sourceActionController.AppendCoroutine(moveByVerocityReturn(_sourceActionController, _source, _skill, _velocity), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
            }
            else
                _source.SkillEndProcess();
        }

        private IEnumerator moveByVerocityReturn(
          UnitActionController _sourceActionController,
          UnitCtrl _source,
          Skill _skill,
          float _velocity)
        {
            MoveAction moveAction = this;
            FixedTransformMonoBehavior.FixedTransform sourceTransform = _sourceActionController.transform;
            bool isRight = sourceTransform.localPosition.x < (double)_skill.OwnerReturnPosition.x;
            while (true)
            {
                sourceTransform.localPosition += new Vector3((!isRight ? -_velocity : _velocity) * moveAction.battleManager.DeltaTime_60fps, 0.0f, 0.0f);
                if ((_skill.OwnerReturnPosition.x - (double)sourceTransform.localPosition.x) * (isRight ? 1.0 : -1.0) >= 0.0 && !BattleUtil.Approximately(_skill.OwnerReturnPosition.x, sourceTransform.localPosition.x))
                    yield return null;
                else
                    break;
            }
            Vector3 localPosition = sourceTransform.localPosition;
            localPosition.x = _skill.OwnerReturnPosition.x;
            sourceTransform.localPosition = localPosition;
            _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 4);
            _sourceActionController.AppendCoroutine(moveAction.moveType4ReturnEnd(_source, _skill), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
        }

        private IEnumerator moveType4ReturnEnd(UnitCtrl _source, Skill _skill)
        {
            MoveAction moveAction = this;
            while (_source.UnitSpineCtrl.IsPlayAnimeBattle)
                yield return null;
            _source.SkillEndProcess();
            if (_skill.BlackoutEndWithMotion)
                moveAction.battleManager.SetBlackoutTimeZero();
        }

        private IEnumerator resetPositionY(Skill _skill, UnitCtrl _source)
        {
            MoveAction moveAction = this;
            while (_source.GetCurrentSpineCtrl().IsPlayAnimeBattle && !_source.IsUnableActionState() && _source.CurrentState != UnitCtrl.ActionState.DAMAGE)
                yield return null;
            _source.transform.localPosition += new Vector3(0.0f, _skill.OwnerReturnPosition.y - _source.transform.localPosition.y, 0.0f);
            List<UnitCtrl> unitCtrlList = _source.IsOther ? moveAction.battleManager.UnitList : moveAction.battleManager.EnemyList;
            for (int index = 0; index < unitCtrlList.Count; ++index)
            {
                UnitCtrl unitCtrl = unitCtrlList[index];
                if (unitCtrl.CurrentState == UnitCtrl.ActionState.IDLE || unitCtrl.CurrentState == UnitCtrl.ActionState.WALK)
                    unitCtrl.SetDirectionAuto();
            }
        }

        private static Vector3 calculatePosotion(
          UnitCtrl _source,
          BasePartsData _target,
          UnitActionController _sourceActionController,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary) => new Vector3((float)((_source.IsLeftDir ? 1.0 : -1.0) * ((double)_valueDictionary[eValueNumber.VALUE_1] + Math.Sign(_valueDictionary[eValueNumber.VALUE_1]) * (_target.GetBodyWidth() / 2.0 + _source.BodyWidth / 2.0))), 0.0f, 0.0f);

        private enum eMoveType
        {
            [System.ComponentModel.Description("朝目标瞬移，会返回")]
            TARGET_POS_RETURN = 1,
            [System.ComponentModel.Description("朝绝对位置瞬移，会返回")]
            ABSOLUTE_POS_RETURN = 2,
            [System.ComponentModel.Description("朝目标瞬移")]
            TARGET_POS = 3,
            [System.ComponentModel.Description("朝绝对位置瞬移")]
            ABSOLUTE_POS = 4,
            [System.ComponentModel.Description("朝目标移动")]
            TARGET_MOVE_BY_VELOSITY = 5,
            [System.ComponentModel.Description("朝绝对位置移动")]
            ABSOLUTE_MOVE_BY_VELOCITY = 6,
            [System.ComponentModel.Description("朝绝对位置移动3")]
            ABSOLUTE_MOVE_DONOT_USE_DIRECTION = 7,
        }

        public static object GetActionDescription(int detail)
        {
            return ((eMoveType)detail).GetDescription();
        }
    }
}
