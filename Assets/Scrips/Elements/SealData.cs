// Decompiled with JetBrains decompiler
// Type: Elements.SealData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using Cute;
using UnityEngine;

namespace Elements
{
    public class SealData
    {
        public SkillEffectCtrl Effect { get; set; }

        public int GetCurrentCount() => Mathf.Min(Count, Max);

        public int Count { get; set; }

        public bool DisplayCount { get; set; }

        public int Max { get; set; }

        private int currentSealId { get; set; }

        private List<int> enableSealIdList { get; set; }

        public void RemoveSeal(int _count)
        {
            if (enableSealIdList == null)
                return;
            _count = Mathf.Min(_count, enableSealIdList.Count);
            for (int index = 0; index < _count; ++index)
                enableSealIdList.RemoveAt(0);
        }

        public void AddSeal(float _limitTime, UnitCtrl _target, eStateIconType _iconType, int _count)
        {
            RemoveSeal(Mathf.Max(0, GetCurrentCount() + _count - Max));
            for (int index = 0; index < _count; ++index)
                addSealImpl(_limitTime, _target, _iconType);
        }

        private void addSealImpl(float _limitTime, UnitCtrl _target, eStateIconType _iconType)
        {
            if (currentSealId == 0)
                enableSealIdList = new List<int>();
            enableSealIdList.Add(currentSealId);
            ++Count;
            if (DisplayCount)
                _target.OnChangeStateNum.Call(_target, _iconType, GetCurrentCount());
            _target.AppendCoroutine(updateData(_limitTime, _target, _iconType, currentSealId), ePauseType.SYSTEM);
            ++currentSealId;
        }

        private IEnumerator updateData(
          float _limitTime,
          UnitCtrl _target,
          eStateIconType _iconType,
          int _sealId)
        {
            while (_limitTime > 0.0 && !_target.IdleOnly && (!_target.IsDead && enableSealIdList.Contains(_sealId)))
            {
                _limitTime -= _target.DeltaTimeForPause;
                yield return null;
            }
            enableSealIdList.Remove(_sealId);
            Count--;
            if (DisplayCount)
                _target.OnChangeStateNum.Call(_target, _iconType, GetCurrentCount());
            if (Count == 0 && GetCurrentCount() == 0)
            {
                //if ((Object) this.Effect != (Object) null)
                //  this.Effect.SetTimeToDie(true);
                _target.OnChangeState.Call(_target, _iconType, false);
                _target.MyOnChangeAbnormalState?.Invoke(_target, _iconType, false, 0, "NaN");
            }
        }
    }
}
