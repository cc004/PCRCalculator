// Decompiled with JetBrains decompiler
// Type: Elements.KnockAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class KnockAction : ActionParameter
    {
        private const float DURATION = 0.5f;
        private const float HEIGHT = 300f;
        private List<BasePartsData> unitListForSort = new List<BasePartsData>();

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          Dictionary<eValueNumber, FloatWithEx> _valueDictionary,Action<string> action)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            if (_target.Owner.ModeChangeUnableStateBarrier)
            {
                action($"处于MODECHANGE状态，击退无效!");
                return;
            }
            AppendIsAlreadyExeced(_target.Owner, _num);
            switch (ActionDetail1)
            {
                case 1:
                    _sourceActionController.AppendCoroutine(updateKnockUpDown(_target.Owner), ePauseType.IGNORE_BLACK_OUT);
                    setTargetDamageNoOverrap(_target.Owner);
                    break;
                case 3:
                    _sourceActionController.AppendCoroutine(updateKnockback(_target.Owner, _source, getKnockValue), ePauseType.IGNORE_BLACK_OUT);
                    setTargetDamageNoOverrap(_target.Owner);
                    break;
                case 4:
                case 5:
                    List<UnitCtrl> unitCtrlList = _source.IsOther ? battleManager.UnitList : battleManager.EnemyList;
                    unitListForSort.Clear();
                    for (int index1 = 0; index1 < unitCtrlList.Count; ++index1)
                    {
                        UnitCtrl unitCtrl = unitCtrlList[index1];
                        if (unitCtrl.IsPartsBoss)
                        {
                            for (int index2 = 0; index2 < unitCtrl.BossPartsListForBattle.Count; ++index2)
                                unitListForSort.Add(unitCtrl.BossPartsListForBattle[index2]);
                        }
                        else
                            unitListForSort.Add(unitCtrl.GetFirstParts());
                    }
                    _source.BaseX = _source.transform.position.x;
                    unitListForSort.Sort(_source.CompareDistanceAsc);
                    BasePartsData basePartsData = unitListForSort.Count > (double)_valueDictionary[eValueNumber.VALUE_2] ? unitListForSort[(int)_valueDictionary[eValueNumber.VALUE_2]] : unitListForSort[unitListForSort.Count - 1];
                    Vector3 _pos = new Vector3((_source.transform.position.x - (double)basePartsData.GetPosition().x > 0.0 ? 1f : -1f) * _valueDictionary[eValueNumber.VALUE_1] + basePartsData.GetLocalPosition().x, _target.GetLocalPosition().y, _target.GetLocalPosition().z);
                    if (ActionDetail1 == 5)
                    {
                        _sourceActionController.AppendCoroutine(updateParaboricKnock(_pos, _target.Owner), ePauseType.SYSTEM, _source);
                        break;
                    }
                    _target.Owner.transform.localPosition = _pos;
                    break;
                case 6:
                    _sourceActionController.AppendCoroutine(updateKnockback(_target.Owner, _source, getKnockValueLimited), ePauseType.IGNORE_BLACK_OUT);
                    setTargetDamageNoOverrap(_target.Owner);
                    break;
                case 8:
                    {
                        Vector3 pos = default(Vector3);
                        if ((int)_valueDictionary[eValueNumber.VALUE_2] == -1)
                        {
                            BasePartsData firstParts = _source.GetFirstParts();
                            pos = new Vector3((float)((!_source.IsLeftDir && !_source.IsForceLeftDir) ? 1 : (-1)) * _valueDictionary[eValueNumber.VALUE_1] + firstParts.GetLocalPosition().x, _target.GetLocalPosition().y, _target.GetLocalPosition().z);
                        }
                        else
                        {
                            List<UnitCtrl> list = (_source.IsOther ? base.battleManager.EnemyList : base.battleManager.UnitList);
                            unitListForSort.Clear();
                            for (int i = 0; i < list.Count; i++)
                            {
                                UnitCtrl unitCtrl = list[i];
                                if (unitCtrl.IsPartsBoss)
                                {
                                    for (int j = 0; j < unitCtrl.BossPartsListForBattle.Count; j++)
                                    {
                                        unitListForSort.Add(unitCtrl.BossPartsListForBattle[j]);
                                    }
                                }
                                else
                                {
                                    unitListForSort.Add(unitCtrl.GetFirstParts());
                                }
                            }
                            _source.BaseX = _source.transform.position.x;
                            unitListForSort.Sort(_source.CompareDistanceAsc);
                            BasePartsData basePartsData0 = (((float)unitListForSort.Count > _valueDictionary[eValueNumber.VALUE_2]) ? unitListForSort[(int)_valueDictionary[eValueNumber.VALUE_2]] : unitListForSort[unitListForSort.Count - 1]);
                            pos = new Vector3((float)((_source.transform.position.x - basePartsData0.GetPosition().x > 0f) ? 1 : (-1)) * _valueDictionary[eValueNumber.VALUE_1] + basePartsData0.GetLocalPosition().x, _target.GetLocalPosition().y, _target.GetLocalPosition().z);
                        }
                        Func<UnitCtrl, UnitCtrl, FloatWithEx> func = delegate
                        {
                            if (_target.Owner.IsBoss)
                            {
                                return 0f;
                            }
                            float x = _target.Owner.GetFirstParts().GetLocalPosition().x;
                            if (_source.IsLeftDir || _source.IsForceLeftDir)
                            {
                                if (x < pos.x)
                                {
                                    return pos.x - x;
                                }
                            }
                            else if (x > pos.x)
                            {
                                return pos.x - x;
                            }
                            return 0f;
                        };
                        startKnockBack(_sourceActionController, _target.Owner, _source, func);
                        setTargetDamageNoOverrap(_target.Owner);
                        break;
                    }
                case 9:
                    startKnockbackForGiveValue(_sourceActionController, _target.Owner, _source, getKnockValueForGiveValue, _valueDictionary);
                    setTargetDamageNoOverrap(_target.Owner);
                    break;
            }
            action($"击退，类型{((eKnockType)ActionDetail1).GetDescription()}");
        }

        private void setTargetDamageNoOverrap(UnitCtrl _target)
        {
            if (_target.CurrentState == UnitCtrl.ActionState.DAMAGE)
                return;
            if (battleManager.ChargeSkillTurn != eChargeSkillTurn.NONE && battleManager.BlackOutUnitList.Count != 0)
                battleManager.BlackOutUnitList.Add(_target);
            _target.SetState(UnitCtrl.ActionState.DAMAGE);
        }

        private float getKnockValue(UnitCtrl _target, UnitCtrl _source)
        {
            float a = (float)((_target.transform.localPosition.x > (double)_source.transform.localPosition.x ? 1.0 : -1.0) * (_target.IsBoss ? 0.0 : (double)Value[eValueNumber.VALUE_1]));
            return a <= 0.0 ? Mathf.Max(a, -BattleDefine.BATTLE_FIELD_SIZE - _target.transform.localPosition.x) : Mathf.Min(a, BattleDefine.BATTLE_FIELD_SIZE - _target.transform.localPosition.x);
        }
        private float getKnockValueForGiveValue(UnitCtrl _target, UnitCtrl _source, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
        {
            float num = ((_target.transform.localPosition.x > _source.transform.localPosition.x) ? 1f : (-1f)) * (_target.IsBoss ? 0f : (float)_valueDictionary[eValueNumber.VALUE_1]);
            if (num > 0f)
            {
                return Mathf.Min(num, BattleDefine.BATTLE_FIELD_SIZE - _target.transform.localPosition.x);
            }
            return Mathf.Max(num, 0f - BattleDefine.BATTLE_FIELD_SIZE - _target.transform.localPosition.x);
        }
        private float getKnockValueLimited(UnitCtrl _target, UnitCtrl _source)
        {
            bool flag = _target.transform.localPosition.x > (double)_source.transform.localPosition.x;
            List<UnitCtrl> unitCtrlList = new List<UnitCtrl>(_target.IsOther ? battleManager.EnemyList : (IEnumerable<UnitCtrl>)battleManager.UnitList);
            UnitCtrl unitCtrl = _target;
            if (unitCtrlList.Count != 0)
            {
                unitCtrlList.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
                unitCtrl = (double)Value[eValueNumber.VALUE_1] > 0.0 != flag ? unitCtrlList[0] : unitCtrlList[unitCtrlList.Count - 1];
            }
            float a1 = (float)((flag ? 1.0 : -1.0) * (_target.IsBoss ? 0.0 : (double)Value[eValueNumber.VALUE_1]));
            float num1 = (float)((flag ? 1.0 : -1.0) * (_target.IsBoss ? 0.0 : (double)Value[eValueNumber.VALUE_5]));
            float num2;
            if (a1 > 0.0)
            {
                float b = Mathf.Max(0.0f, unitCtrl.transform.localPosition.x + num1 - _target.transform.localPosition.x);
                num2 = Mathf.Min(a1, Mathf.Min(BattleDefine.BATTLE_FIELD_SIZE - _target.transform.localPosition.x, b));
            }
            else
            {
                float b = Mathf.Min(0.0f, unitCtrl.transform.localPosition.x + num1 - _target.transform.localPosition.x);
                num2 = Mathf.Max(a1, Mathf.Max(-BattleDefine.BATTLE_FIELD_SIZE - _target.transform.localPosition.x, b));
            }
            return num2;
        }
        private void startKnockBack(UnitActionController _sourceActionController, UnitCtrl _target, UnitCtrl _source, Func<UnitCtrl, UnitCtrl, FloatWithEx> _func)
        {
            float num = _func(_target, _source);
            float duration = Mathf.Max(Mathf.Abs(num) / base.Value[eValueNumber.VALUE_3], base.battleManager.DeltaTime_60fps);
            _sourceActionController.AppendCoroutine(updateKnockBackCoroutine(_target, _source, num, duration), ePauseType.IGNORE_BLACK_OUT);
        }
        private void startKnockbackForGiveValue(UnitActionController _sourceActionController, UnitCtrl _target, UnitCtrl _source, Func<UnitCtrl, UnitCtrl, Dictionary<eValueNumber, FloatWithEx>, float> _func, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
        {
            float num = _func(_target, _source, _valueDictionary);
            float num2 = _valueDictionary[eValueNumber.VALUE_3];
            float duration = 90f;
            if (!BattleUtil.Approximately(num2, 0f))
            {
                duration = Mathf.Max(Mathf.Abs(num) / num2, base.battleManager.FNHFJLAENPF);
            }
            _sourceActionController.AppendCoroutine(updateKnockBackCoroutine(_target, _source, num, duration), ePauseType.IGNORE_BLACK_OUT);
        }
        private IEnumerator updateKnockBackCoroutine(UnitCtrl _target, UnitCtrl _source, float _knockValue, float _duration)
        {
            float start = _target.transform.localPosition.x;
            float time = 0f;
            _target.KnockBackEnableCount++;
            Vector3 localPosition;
            while (true)
            {
                localPosition = _target.transform.localPosition;
                localPosition.x = start + _knockValue * base.KnockAnimationCurve.Evaluate(time / _duration);
                _target.transform.localPosition = localPosition;
                time += base.battleManager.FNHFJLAENPF;
                if (time > _duration)
                {
                    break;
                }
                yield return null;
            }
            localPosition.x = start + _knockValue;
            _target.transform.localPosition = localPosition;
            _target.KnockBackEnableCount--;
            if (!_target.IsUnableActionState())
            {
                _target.GetCurrentSpineCtrl().Resume();
            }
        }

        private IEnumerator updateKnockback(
          UnitCtrl _target,
          UnitCtrl _source,
          Func<UnitCtrl, UnitCtrl, float> _func)
        {
            KnockAction knockAction = this;
            float start = _target.transform.localPosition.x;
            float knockValue = _func(_target, _source);
            float time = 0.0f;
            float duration = Mathf.Max(Mathf.Abs(knockValue) / knockAction.Value[eValueNumber.VALUE_3], knockAction.battleManager.DeltaTime_60fps);
            ++_target.KnockBackEnableCount;
            Vector3 localPosition;
            while (true)
            {
                localPosition = _target.transform.localPosition;
                localPosition.x = start + knockValue * knockAction.KnockAnimationCurve.Evaluate(time / duration);
                _target.transform.localPosition = localPosition;
                time += knockAction.battleManager.DeltaTime_60fps;
                if (time <= (double)duration)
                    yield return null;
                else
                    break;
            }
            localPosition.x = start + knockValue;
            _target.transform.localPosition = localPosition;
            --_target.KnockBackEnableCount;
            if (!_target.IsUnableActionState())
                _target.GetCurrentSpineCtrl().Resume();
        }

        private IEnumerator updateKnockUpDown(UnitCtrl _target)
        {
            KnockAction knockAction = this;
            float start = _target.transform.localPosition.y;
            float knockValue = _target.IsBoss ? 0.0f : (float)knockAction.Value[eValueNumber.VALUE_1];
            float time = 0.0f;
            float duration = knockAction.Value[eValueNumber.VALUE_1] / knockAction.Value[eValueNumber.VALUE_3];
            ++_target.KnockBackEnableCount;
            while (duration > (double)time)
            {
                Vector3 localPosition = _target.transform.localPosition;
                time += knockAction.battleManager.DeltaTime_60fps;
                if (time > (double)duration)
                    time = duration;
                localPosition.y = start + knockValue * knockAction.KnockAnimationCurve.Evaluate(time / duration);
                _target.transform.localPosition = localPosition;
                yield return null;
            }
            duration = knockAction.Value[eValueNumber.VALUE_1] / knockAction.Value[eValueNumber.VALUE_4];
            start = _target.transform.localPosition.y;
            time = 0.0f;
            while (duration > (double)time)
            {
                Vector3 localPosition = _target.transform.localPosition;
                time += knockAction.battleManager.DeltaTime_60fps;
                if (time > (double)duration)
                    time = duration;
                localPosition.y = start - knockValue * knockAction.KnockDownAnimationCurve.Evaluate(time / duration);
                _target.transform.localPosition = localPosition;
                yield return null;
            }
            --_target.KnockBackEnableCount;
            if (!_target.IsUnableActionState())
                _target.GetCurrentSpineCtrl().Resume();
        }

        private IEnumerator updateParaboricKnock(Vector3 _pos, UnitCtrl _target)
        {
            KnockAction knockAction = this;
            float halfDuration = 0.25f;
            CustomEasing easingUpY = new CustomEasing(CustomEasing.eType.outCubic, _target.transform.localPosition.y, _target.transform.localPosition.y + 300f, halfDuration);
            CustomEasing easingDownY = new CustomEasing(CustomEasing.eType.inQuad, _target.transform.localPosition.y + 300f, _target.transform.localPosition.y, halfDuration);
            CustomEasing easingX = new CustomEasing(CustomEasing.eType.linear, _target.transform.localPosition.x, _pos.x, 0.5f);
            float time = 0.0f;
            do
            {
                yield return null;
                time += knockAction.battleManager.DeltaTime_60fps;
                float y = time >= (double)halfDuration ? easingDownY.GetCurVal(knockAction.battleManager.DeltaTime_60fps) : easingUpY.GetCurVal(knockAction.battleManager.DeltaTime_60fps);
                _target.transform.localPosition = new Vector3(easingX.GetCurVal(knockAction.battleManager.DeltaTime_60fps), y);
            }
            while (time <= 0.5);
        }

        private enum eKnockType
        {
            KNOCK_UP_DOWN = 1,
            KNOCK_UP = 2,
            KNOCK_BACK = 3,
            MOVE_TARGET = 4,
            MOVE_TARGET_PARABORIC = 5,
            KNOCK_BACK_LIMITED = 6,
            PULL_OWNER = 8,
            KNOCK_BACK_GIVE_VALUE = 9
        }
    }
}
