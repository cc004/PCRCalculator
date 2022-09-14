// Decompiled with JetBrains decompiler
// Type: Elements.LoopMotionBuffDebuffAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;

namespace Elements
{
    public class LoopMotionBuffDebuffAction : ActionParameter
    {
        private List<LoopMotionBuffDebuffAction> childActions = new List<LoopMotionBuffDebuffAction>();
        private List<BuffDebuffData> targetAndValueList = new List<BuffDebuffData>();
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
            if (ActionDetail2 <= 10)
                return;
            ActionParameter actionParameter = _skill.ActionParameters.Find(e => e.ActionId == ActionDetail2 && e is LoopMotionBuffDebuffAction);
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
            hitCount = 0;
            targetAndValueList.Clear();
            if (ActionDetail2 > 10)
                return;
            triggered = false;
            _source.AppendCoroutine(updateStartMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _source);
        }

        private IEnumerator updateStartMotion(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            while (!isActionCancel(_source, _sourceActionController, _skill))
            {
                if (!_source.UnitSpineCtrl.IsPlayAnimeBattle)
                {
                    _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 1, false);
                    _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 1, _isLoop: true);
                    _sourceActionController.AppendCoroutine(updateLoopMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _source);
                    yield break;
                }

                yield return null;
            }
            ReleaseBuffDebuff(_source);
            for (int index = 0; index < childActions.Count; ++index)
                childActions[index].ReleaseBuffDebuff(_source);
        }

        private IEnumerator updateLoopMotion(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            LoopMotionBuffDebuffAction buffDebuffAction = this;
            float timer = buffDebuffAction.Value[eValueNumber.VALUE_4];
            bool enableTimer = timer > 0.0;
            while (!buffDebuffAction.isActionCancel(_source, _sourceActionController, _skill))
            {
                if (enableTimer)
                {
                    timer -= _source.DeltaTimeForPause;
                    if (timer < 0.0 || buffDebuffAction.battleManager.GameState != eBattleGameState.PLAY)
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

                yield return null;
            }
            buffDebuffAction.ReleaseBuffDebuff(_source);
            for (int index = 0; index < buffDebuffAction.childActions.Count; ++index)
                buffDebuffAction.childActions[index].ReleaseBuffDebuff(_source);
            for (int index = _skill.LoopEffectObjs.Count - 1; index >= 0; --index)
            {
                SkillEffectCtrl loopEffectObj = _skill.LoopEffectObjs[index];
                if (!(loopEffectObj == null) && loopEffectObj.GetComponent<SkillEffectCtrl>().SortTarget == _source)
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
                if (!(_skill.LoopEffectObjs[index] == null) && _skill.LoopEffectObjs[index].GetComponent<SkillEffectCtrl>().SortTarget == _source)
                {
                    _skill.LoopEffectObjs[index].SetTimeToDie(true);
                    _skill.LoopEffectObjs.RemoveAt(index);
                }
            }
            ReleaseBuffDebuff(_source);
            for (int index = 0; index < childActions.Count; ++index)
                childActions[index].ReleaseBuffDebuff(_source);
            int num = _byTimer ? 3 : 2;
            _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, num, false);
            _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, num);
            _sourceActionController.AppendCoroutine(updateEndMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
        }

        private IEnumerator updateEndMotion(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            while (!isActionCancel(_source, _sourceActionController, _skill))
            {
                if (!_source.UnitSpineCtrl.IsPlayAnimeBattle)
                {
                    _source.SetState(UnitCtrl.ActionState.IDLE);
                    break;
                }
                yield return null;
            }
        }

        public void ReleaseBuffDebuff(UnitCtrl _source)
        {
            triggered = true;
            for (int i = 0; i < targetAndValueList.Count; i++)
            {
                BuffDebuffData buffDebuffData = targetAndValueList[i];
                buffDebuffData.Target.EnableBuffParam(buffDebuffData.BuffParamKind, buffDebuffData.Value, _enable: false, _source, base.ActionDetail1 % 10 != 1, _additional: false);
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
            AppendIsAlreadyExeced(_target.Owner, _num);
            if (triggered)
                return;
            UnitCtrl.BuffParamKind changeParamKind = BuffDebuffAction.GetChangeParamKind(ActionDetail1);
            Dictionary<BasePartsData, FloatWithEx> dictionary = new Dictionary<BasePartsData, FloatWithEx>();
            bool _isDebuf = ActionDetail1 % 10 == 1;
            if (_target.Owner.IsPartsBoss)
            {
                for (int index = 0; index < _target.Owner.BossPartsListForBattle.Count; ++index)
                    dictionary.Add(_target.Owner.BossPartsListForBattle[index], BuffDebuffAction.CalculateBuffDebuffParam(_target.Owner.BossPartsListForBattle[index], _valueDictionary[eValueNumber.VALUE_2], (BuffDebuffAction.eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, _isDebuf));
            }
            else
                dictionary.Add(_target.Owner.DummyPartsData, BuffDebuffAction.CalculateBuffDebuffParam(_target, _valueDictionary[eValueNumber.VALUE_2], (BuffDebuffAction.eChangeParameterType)(float)_valueDictionary[eValueNumber.VALUE_1], changeParamKind, _isDebuf));
            //_target.Owner.SetBuffParam(UnitCtrl.BuffParamKind.NUM, dictionary, 0.0f, 0, null, true, eEffectType.COMMON, !_isDebuf, false);
            _target.Owner.SetBuffParam(UnitCtrl.BuffParamKind.NUM, changeParamKind, dictionary, 0f, 0, null, _despelable: true, eEffectType.COMMON, !_isDebuf, _additional: false, _isShowIcon: false);
            _target.Owner.EnableBuffParam(changeParamKind, dictionary, _enable: true, _source, !_isDebuf, _additional: false);

            //_target.Owner.EnableBuffParam(changeParamKind, dictionary, true, _source, !_isDebuf, false, 90);
            if (ActionDetail2 == 1)
                _source.OnDamageForLoopTrigger = (_byAttack, _damage, _critical) =>
                {
                    if (!_byAttack)
                        return;
                    ++hitCount;
                    if (hitCount != ActionDetail3)
                        return;
                    triggered = true;
                };
            targetAndValueList.Add(new BuffDebuffData
            {
                Target = _target.Owner,
                BuffParamKind = changeParamKind,
                Value = dictionary
            });
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_2] = (float)(MasterData.action_value_2 + MasterData.action_value_3 * _level);
            Value[eValueNumber.VALUE_4] = (float)(MasterData.action_value_4 + MasterData.action_value_5 * _level);
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
