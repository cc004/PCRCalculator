using System.Collections;
using System.Collections.Generic;

namespace Elements
{
    public class StealthAction : ActionParameter
    {
        private const int LOOP_MOTION_NUMBER = 1;
        private const int END_MOTION_NUMBER = 2;

        private ActionParameter action { get; set; }

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            action = _skill.ActionParameters.Find(e => e.ActionId == ActionDetail1);
        }

        public override void ReadyAction(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            base.ReadyAction(_source, _sourceActionController, _skill);
            if (action == null)
                return;
            action.CancelByIfForAll = true;
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
            _target.Owner.IsStealth = true;
            battleManager.shouldUpdateSkillTarget = true;
            _target.Owner.AppendCoroutine(updateStealth(_valueDictionary[eValueNumber.VALUE_1], _target.Owner), ePauseType.SYSTEM);
            if (_skill.AnimId != 0 && _source.GetCurrentSpineCtrl().IsAnimation(_skill.AnimId, _skill.SkillNum, 1))
            {
                _sourceActionController.AppendCoroutine(updateStartMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, (_skill.BlackOutTime > 0f) ? _source : null);
            }
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
                    _sourceActionController.AppendCoroutine(updateLoopMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
                    break;
                }
                yield return null;
            }
        }

        private IEnumerator updateLoopMotion(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            yield return null;
            while (!isActionCancel(_source, _sourceActionController, _skill))
            {
                if (_source.ActionsTargetOnMe.Count == 0)
                {
                    for (int index = 0; index < _skill.LoopEffectObjs.Count; ++index)
                        _skill.LoopEffectObjs[index].SetTimeToDie(true);
                    _skill.LoopEffectObjs.Clear();
                    action.CancelByIfForAll = false;
                    _sourceActionController.CreateNormalPrefabWithTargetMotion(_skill, 2, false);
                    _sourceActionController.ExecUnitActionWithDelay(action, _skill, false, false);
                    _source.PlayAnimeNoOverlap(_skill.AnimId, _skill.SkillNum, 2);
                    _sourceActionController.AppendCoroutine(updateEndMotion(_source, _sourceActionController, _skill), ePauseType.SYSTEM, _skill.BlackOutTime > 0.0 ? _source : null);
                    break;
                }
                yield return null;
            }
        }

        private IEnumerator updateEndMotion(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            StealthAction stealthAction = this;
            while (!stealthAction.isActionCancel(_source, _sourceActionController, _skill))
            {
                if (!_source.UnitSpineCtrl.IsPlayAnimeBattle)
                {
                    _source.IdleOnly = true;
                    _source.CurrentState = UnitCtrl.ActionState.IDLE;
                    stealthAction.battleManager.CallbackIdleOnlyDone(_source);
                    List<UnitCtrl> unitCtrlList = _source.IsOther ? stealthAction.battleManager.EnemyList : stealthAction.battleManager.UnitList;
                    if (unitCtrlList.Contains(_source))
                        unitCtrlList.Remove(_source);
                    _source.IsDead = true;
                    _source.CureAllAbnormalState();
                    stealthAction.battleManager.CallbackFadeOutDone(_source);
                    stealthAction.battleManager.CallbackDead(_source);
                    _source.gameObject.SetActive(false);
                    break;
                }
                yield return null;
            }
        }

        private IEnumerator updateStealth(float _duration, UnitCtrl _target)
        {
            StealthAction stealthAction = this;
            float timer = _duration;
            while (timer > 0.0)
            {
                timer -= stealthAction.battleManager.DeltaTime_60fps;
                yield return null;
            }
            _target.IsStealth = false;
            battleManager.shouldUpdateSkillTarget = true;
        }

        public override void SetLevel(float _level) => base.SetLevel(_level);

        private bool isActionCancel(
          UnitCtrl _source,
          UnitActionController _sourceActionController,
          Skill _skill)
        {
            if (!_source.IsUnableActionState() && !_source.IsCancelActionState(_skill.SkillId == _source.UnionBurstSkillId))
                return _skill.Cancel;
            _source.CancelByAwake = false;
            _source.CancelByConvert = false;
            _source.CancelByToad = false;
            _sourceActionController.CancelAction(_skill.SkillId);
            return true;
        }
    }
}
