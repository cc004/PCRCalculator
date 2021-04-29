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
          Dictionary<eValueNumber, float> _valueDictionary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            this.AppendIsAlreadyExeced(_target.Owner, _num);
            switch (this.ActionDetail1)
            {
                case 1:
                    _sourceActionController.AppendCoroutine(this.updateKnockUpDown(_target.Owner), ePauseType.IGNORE_BLACK_OUT);
                    this.setTargetDamageNoOverrap(_target.Owner);
                    break;
                case 3:
                    _sourceActionController.AppendCoroutine(this.updateKnockback(_target.Owner, _source, new Func<UnitCtrl, UnitCtrl, float>(this.getKnockValue)), ePauseType.IGNORE_BLACK_OUT);
                    this.setTargetDamageNoOverrap(_target.Owner);
                    break;
                case 4:
                case 5:
                    List<UnitCtrl> unitCtrlList = _source.IsOther ? this.battleManager.UnitList : this.battleManager.EnemyList;
                    this.unitListForSort.Clear();
                    for (int index1 = 0; index1 < unitCtrlList.Count; ++index1)
                    {
                        UnitCtrl unitCtrl = unitCtrlList[index1];
                        if (unitCtrl.IsPartsBoss)
                        {
                            for (int index2 = 0; index2 < unitCtrl.BossPartsListForBattle.Count; ++index2)
                                this.unitListForSort.Add((BasePartsData)unitCtrl.BossPartsListForBattle[index2]);
                        }
                        else
                            this.unitListForSort.Add(unitCtrl.GetFirstParts());
                    }
                    _source.BaseX = _source.transform.position.x;
                    this.unitListForSort.Sort(new Comparison<BasePartsData>(_source.CompareDistanceAsc));
                    BasePartsData basePartsData = (double)this.unitListForSort.Count > (double)_valueDictionary[eValueNumber.VALUE_2] ? this.unitListForSort[(int)_valueDictionary[eValueNumber.VALUE_2]] : this.unitListForSort[this.unitListForSort.Count - 1];
                    Vector3 _pos = new Vector3(((double)_source.transform.position.x - (double)basePartsData.GetPosition().x > 0.0 ? 1f : -1f) * _valueDictionary[eValueNumber.VALUE_1] + basePartsData.GetLocalPosition().x, _target.GetLocalPosition().y, _target.GetLocalPosition().z);
                    if (this.ActionDetail1 == 5)
                    {
                        _sourceActionController.AppendCoroutine(this.updateParaboricKnock(_pos, _target.Owner), ePauseType.SYSTEM, _source);
                        break;
                    }
                    _target.Owner.transform.localPosition = _pos;
                    break;
                case 6:
                    _sourceActionController.AppendCoroutine(this.updateKnockback(_target.Owner, _source, new Func<UnitCtrl, UnitCtrl, float>(this.getKnockValueLimited)), ePauseType.IGNORE_BLACK_OUT);
                    this.setTargetDamageNoOverrap(_target.Owner);
                    break;
            }
        }

        private void setTargetDamageNoOverrap(UnitCtrl _target)
        {
            if (_target.CurrentState == UnitCtrl.ActionState.DAMAGE)
                return;
            if (this.battleManager.ChargeSkillTurn != eChargeSkillTurn.NONE && this.battleManager.BlackOutUnitList.Count != 0)
                this.battleManager.BlackOutUnitList.Add(_target);
            _target.SetState(UnitCtrl.ActionState.DAMAGE);
        }

        private float getKnockValue(UnitCtrl _target, UnitCtrl _source)
        {
            float a = (float)(((double)_target.transform.localPosition.x > (double)_source.transform.localPosition.x ? 1.0 : -1.0) * (_target.IsBoss ? 0.0 : (double)this.Value[eValueNumber.VALUE_1]));
            return (double)a <= 0.0 ? Mathf.Max(a, -BattleDefine.BATTLE_FIELD_SIZE - _target.transform.localPosition.x) : Mathf.Min(a, BattleDefine.BATTLE_FIELD_SIZE - _target.transform.localPosition.x);
        }

        private float getKnockValueLimited(UnitCtrl _target, UnitCtrl _source)
        {
            bool flag = (double)_target.transform.localPosition.x > (double)_source.transform.localPosition.x;
            List<UnitCtrl> unitCtrlList = new List<UnitCtrl>(_target.IsOther ? (IEnumerable<UnitCtrl>)this.battleManager.EnemyList : (IEnumerable<UnitCtrl>)this.battleManager.UnitList);
            UnitCtrl unitCtrl = _target;
            if (unitCtrlList.Count != 0)
            {
                unitCtrlList.Sort((Comparison<UnitCtrl>)((a, b) => a.transform.position.x.CompareTo(b.transform.position.x)));
                unitCtrl = (double)this.Value[eValueNumber.VALUE_1] > 0.0 != flag ? unitCtrlList[0] : unitCtrlList[unitCtrlList.Count - 1];
            }
            float a1 = (float)((flag ? 1.0 : -1.0) * (_target.IsBoss ? 0.0 : (double)this.Value[eValueNumber.VALUE_1]));
            float num1 = (float)((flag ? 1.0 : -1.0) * (_target.IsBoss ? 0.0 : (double)this.Value[eValueNumber.VALUE_5]));
            float num2;
            if ((double)a1 > 0.0)
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
                if ((double)time <= (double)duration)
                    yield return (object)null;
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
            float knockValue = _target.IsBoss ? 0.0f : knockAction.Value[eValueNumber.VALUE_1];
            float time = 0.0f;
            float duration = knockAction.Value[eValueNumber.VALUE_1] / knockAction.Value[eValueNumber.VALUE_3];
            ++_target.KnockBackEnableCount;
            while ((double)duration > (double)time)
            {
                Vector3 localPosition = _target.transform.localPosition;
                time += knockAction.battleManager.DeltaTime_60fps;
                if ((double)time > (double)duration)
                    time = duration;
                localPosition.y = start + knockValue * knockAction.KnockAnimationCurve.Evaluate(time / duration);
                _target.transform.localPosition = localPosition;
                yield return (object)null;
            }
            duration = knockAction.Value[eValueNumber.VALUE_1] / knockAction.Value[eValueNumber.VALUE_4];
            start = _target.transform.localPosition.y;
            time = 0.0f;
            while ((double)duration > (double)time)
            {
                Vector3 localPosition = _target.transform.localPosition;
                time += knockAction.battleManager.DeltaTime_60fps;
                if ((double)time > (double)duration)
                    time = duration;
                localPosition.y = start - knockValue * knockAction.KnockDownAnimationCurve.Evaluate(time / duration);
                _target.transform.localPosition = localPosition;
                yield return (object)null;
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
                yield return (object)null;
                time += knockAction.battleManager.DeltaTime_60fps;
                float y = (double)time >= (double)halfDuration ? easingDownY.GetCurVal(knockAction.battleManager.DeltaTime_60fps) : easingUpY.GetCurVal(knockAction.battleManager.DeltaTime_60fps);
                _target.transform.localPosition = new Vector3(easingX.GetCurVal(knockAction.battleManager.DeltaTime_60fps), y);
            }
            while ((double)time <= 0.5);
        }

        private enum eKnockType
        {
            KNOCK_UP_DOWN = 1,
            KNOCK_UP = 2,
            KNOCK_BACK = 3,
            MOVE_TARGET = 4,
            MOVE_TARGET_PARABORIC = 5,
            KNOCK_BACK_LIMITED = 6,
        }
    }
}
