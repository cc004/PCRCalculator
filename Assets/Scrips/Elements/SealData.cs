// Decompiled with JetBrains decompiler
// Type: Elements.SealData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Cute;
using Elements.Battle;
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
        private Dictionary<int, IEnumerator> updateDataDic
        {
            get;
            set;
        } = new Dictionary<int, IEnumerator>();

		public void RemoveSeal(int _count, bool _isPassiveSeal)
		{
			if (enableSealIdList == null)
			{
				return;
			}
			_count = Mathf.Min(_count, enableSealIdList.Count);
			for (int i = 0; i < _count; i++)
			{
				int key = enableSealIdList[0];
				enableSealIdList.RemoveAt(0);
				if (_isPassiveSeal && updateDataDic.ContainsKey(key))
				{
					updateDataDic[key].MoveNext();
					updateDataDic.Remove(key);
				}
			}
		}

		public void AddSeal(float _limitTime, UnitCtrl _target, eStateIconType _iconType, int _count)
		{
			RemoveSeal(Mathf.Max(0, GetCurrentCount() + _count - Max), _isPassiveSeal: false);
			for (int i = 0; i < _count; i++)
			{
				addSealImpl(_limitTime, _target, _iconType);
            }
        }

		private void addSealImpl(float _limitTime, UnitCtrl _target, eStateIconType _iconType)
		{
			if (currentSealId == 0)
			{
				enableSealIdList = new List<int>();
			}
			enableSealIdList.Add(currentSealId);
			Count++;
			if (DisplayCount)
			{
				_target.OnChangeStateNum.Call(_target, _iconType, GetCurrentCount());

            }
            if (!BattleManager.Instance.skipping && _target.button != null)
            {
                _target.button.SetSealIcons(_target, _iconType);
                if (_target.button.battleBuffUIDic.TryGetValue(_iconType, out var val))
                {
                    val.detailText.text = $"{GetCurrentCount()}/{Max} [{(int)_iconType}]";
                }
            }
            IEnumerator enumerator = updateData(_limitTime, _target, _iconType, currentSealId);
			updateDataDic.Add(currentSealId, enumerator);
			_target.AppendCoroutine(enumerator, ePauseType.SYSTEM);
			currentSealId++;
		}

		private IEnumerator updateData(float _limitTime, UnitCtrl _target, eStateIconType _iconType, int _sealId)
        {
            var total = _limitTime;
			while (_limitTime > 0f && !_target.IdleOnly && !_target.IsDead && enableSealIdList.Contains(_sealId))
			{
				_limitTime -= _target.DeltaTimeForPause;

                if (_sealId == enableSealIdList[0] && _target.button != null && _target.button.battleBuffUIDic.TryGetValue(_iconType, out var val))
                {
                    val.timeCountSlider.value = 1f - Math.Clamp(_limitTime / total, 0f, 1f);
                }

                yield return null;
			}
			enableSealIdList.Remove(_sealId);
			Count--;
			if (DisplayCount)
			{
				_target.OnChangeStateNum.Call(_target, _iconType, GetCurrentCount());

            }
            if (!BattleManager.Instance.skipping && _target.button != null)
            {
                _target.button.SetSealIcons(_target, _iconType);
                if (_target.button.battleBuffUIDic.TryGetValue(_iconType, out var val))
                {
                    val.detailText.text = $"{GetCurrentCount()}/{Max} [{(int)_iconType}]";
                }
            }
            if (Count == 0 && GetCurrentCount() == 0)
			{
				if (Effect != null)
				{
					Effect.SetTimeToDie(value: true);
				}
				_target.OnChangeState.Call(_target, _iconType, ADIFIOLCOPN: false);
			}
		}
	}
}
