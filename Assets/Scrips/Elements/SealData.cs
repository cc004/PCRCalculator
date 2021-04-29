// Decompiled with JetBrains decompiler
// Type: Elements.SealData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class SealData
    {
        public SkillEffectCtrl Effect { get; set; }

        public int GetCurrentCount() => Mathf.Min(this.Count, this.Max);

        public int Count { get; set; }

        public bool DisplayCount { get; set; }

        public int Max { get; set; }

        private int currentSealId { get; set; }

        private List<int> enableSealIdList { get; set; }

        public void RemoveSeal(int _count)
        {
            if (this.enableSealIdList == null)
                return;
            _count = Mathf.Min(_count, this.enableSealIdList.Count);
            for (int index = 0; index < _count; ++index)
                this.enableSealIdList.RemoveAt(0);
        }

        public void AddSeal(float _limitTime, UnitCtrl _target, eStateIconType _iconType, int _count)
        {
            this.RemoveSeal(Mathf.Max(0, this.GetCurrentCount() + _count - this.Max));
            for (int index = 0; index < _count; ++index)
                this.addSealImpl(_limitTime, _target, _iconType);
        }

        private void addSealImpl(float _limitTime, UnitCtrl _target, eStateIconType _iconType)
        {
            if (this.currentSealId == 0)
                this.enableSealIdList = new List<int>();
            this.enableSealIdList.Add(this.currentSealId);
            ++this.Count;
            if (this.DisplayCount)
                _target.OnChangeStateNum.Call<UnitCtrl, eStateIconType, int>(_target, _iconType, this.GetCurrentCount());
            _target.AppendCoroutine(this.updateData(_limitTime, _target, _iconType, this.currentSealId), ePauseType.SYSTEM);
            ++this.currentSealId;
        }

        private IEnumerator updateData(
          float _limitTime,
          UnitCtrl _target,
          eStateIconType _iconType,
          int _sealId)
        {
            while ((double)_limitTime > 0.0 && !_target.IdleOnly && (!_target.IsDead && this.enableSealIdList.Contains(_sealId)))
            {
                _limitTime -= _target.DeltaTimeForPause;
                yield return (object)null;
            }
            this.enableSealIdList.Remove(_sealId);
            this.Count--;
            if (this.DisplayCount)
                _target.OnChangeStateNum.Call<UnitCtrl, eStateIconType, int>(_target, _iconType, this.GetCurrentCount());
            if (this.Count == 0 && this.GetCurrentCount() == 0)
            {
                //if ((Object) this.Effect != (Object) null)
                //  this.Effect.SetTimeToDie(true);
                _target.OnChangeState.Call<UnitCtrl, eStateIconType, bool>(_target, _iconType, false);
                _target.MyOnChangeAbnormalState?.Invoke(_target, _iconType, false, 0, "NaN");
            }
        }
    }
}
