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

	public class SealCancelData // TypeDefIndex: 20468
	{
		// Fields
		protected Action CancelSealAction; // 0x10

		public SealCancelData(Action _cancelSealAction)
		{
			CancelSealAction = _cancelSealAction;
		}

		// RVA: 0x2817270 Offset: 0x2817270 VA: 0x2817270 Slot: 4
		public virtual void OnActivate() { }
	}

	public class SealDamageCountCancelData : SealCancelData // TypeDefIndex: 20469
	{
		// Fields
		private int releaseDamageCount; // 0x18
		private int damageCount; // 0x1C


		// RVA: 0x2817274 Offset: 0x2817274 VA: 0x2817274
		public SealDamageCountCancelData(int _releaseDamageCount, Action _cancelSealAction): base(_cancelSealAction)
		{
			releaseDamageCount = _releaseDamageCount;
			damageCount = 0;
		}

		// RVA: 0x28172BC Offset: 0x28172BC VA: 0x28172BC Slot: 4
		public override void OnActivate()
		{
			damageCount = 0;
		}

		// RVA: 0x28172C4 Offset: 0x28172C4 VA: 0x28172C4
		public void OnDamage()
		{
			damageCount++;
			if (damageCount % releaseDamageCount == 0)
			{
				CancelSealAction();
			}
		}
	}

	public class SealBuffDebuffData // TypeDefIndex: 20474
	{
		private bool isEnable; // 0x54

		public UnitCtrl Source { get; set; }
		public UnitCtrl Target { get; set; }
		public SealData ParentSealData { get; set; }
		public UnitCtrl.BuffParamKind TargetParameter { get; set; }
		public Dictionary<BasePartsData, LongWithEx> ValuePerSealDictionary { get; set; }
		private double timer { get; set; }
		public double Duration { get; set; }
		private int currentSealCount { get; set; }
		
		// Methods

		// RVA: 0x2816ADC Offset: 0x2816ADC VA: 0x2816ADC
		public void UpdateValue(int _count)
		{
			if (currentSealCount == _count)
				return;

			if (Target == null)
				return;

			int deltaCount;
			bool isPositive;

			if (currentSealCount >= _count)
			{
				// 减少封印值
				deltaCount = currentSealCount - _count;
				isPositive = false;
			}
			else
			{
				// 增加封印值
				deltaCount = _count - currentSealCount;
				isPositive = true;
			}

			// 调用目标组件设置Buff/Debuff乘法值
			Target.SetBuffDebuffWithMultiplyValue(
				TargetParameter,
				ValuePerSealDictionary,
				isPositive,
				Source,
				deltaCount);

			currentSealCount = _count;
		}

		// RVA: 0x2817F74 Offset: 0x2817F74 VA: 0x2817F74
		public void DeleteValue()
		{
			isEnable = false;
			
			if (Target == null)
				return;

			// 移除所有封印效果
			Target.SetBuffDebuffWithMultiplyValue(
				TargetParameter,
				ValuePerSealDictionary,
				false, // 移除效果
				Source,
				currentSealCount);
		}

		// RVA: 0x2817FB0 Offset: 0x2817FB0 VA: 0x2817FB0
		public void StartTimer()
		{
			var target = Target;
			if (target == null)
				return;

			var timerCoroutine = updateTimer();
			target.AppendCoroutine(timerCoroutine, ePauseType.SYSTEM);
		}

		// RVA: 0x2817FEC Offset: 0x2817FEC VA: 0x2817FEC
		private IEnumerator updateTimer()
		{
			// 基于Duration属性递减timer
			timer = Duration;
			
			while (timer > 0 && isEnable && Target != null && !Target.IsDead)
			{
				timer -= Target.DeltaTimeForPause;
				yield return null;
			}
			
			// 时间到或目标死亡时，删除封印效果
			if (isEnable)
			{
				DeleteValue();
			}
		}
	}


    public class SealData
	{
		public Dictionary<int, SealBuffDebuffData> SealBuffDebuffDataDictionary { get; set; }
		public bool IsEtherealization { get; set; }

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

		public SealCancelData SealCancelData { get; set; }
		public Action<int, bool> OnAddAction { get; set; }
		public Action<int, bool> OnRemoveAction { get; set; }


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
