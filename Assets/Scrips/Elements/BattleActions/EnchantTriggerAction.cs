// Decompiled with JetBrains decompiler
// Type: Elements.TriggerAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cute;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
	public class EnchantTriggerAction : ActionParameter
	{

		private enum eTriggerType // TypeDefIndex: 18904
		{
			SET_BUFF = 1,
			GIVE_CRITICAL = 2,
		}


		// Properties
		private int effectCounter { get; set; }
		private int effectMaxCount { get; set; }
		private float effectDuration { get; set; }
		private ActionParameter actionParameter { get; set; }

		// RVA: 0x30DDC14 Offset: 0x30DDC14 VA: 0x30DDC14 Slot: 6
		public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
		{
			base.ExecActionOnStart(_skill, _source, _sourceActionController);

			// 处理ActionDetail2
			if (ActionDetail2 != 0)
			{
				actionParameter = _skill.ActionParameters.Find(param => param.ActionId == ActionDetail2);
			}

			foreach (var param in _skill.ActionParameters)
            {
				// TODO check what is this
				// *((_BYTE *)&v28.fields._current->klass + (unsigned __int64)&stru_68.reloff) = 0;
				param.TriggerCall = false;
            }
		}

		// RVA: 0x30DDE80 Offset: 0x30DDE80 VA: 0x30DDE80 Slot: 10
		public override void ReadyAction(UnitCtrl _source, UnitActionController _sourceActionController, Skill _skill)
        {
			base.ReadyAction(_source, _sourceActionController, _skill);
			if (actionParameter != null)
				actionParameter.CancelByIfForAll = true;
			// TODO in new version
			// if (actionParameter != null)
            // {
			// 	actionParameter.NoDelayExecByTriggerOnly = 1;
			// 	foreach (var ind in actionParameter.ActionChildrenIndexes)
			// 	{
			// 		var childParam = _skill.ActionParameters[ind];
			// 		childParam.NoDelayExecByTriggerOnly = 1;
			// 	}
            // }
        }

		// RVA: 0x30DDF28 Offset: 0x30DDF28 VA: 0x30DDF28 Slot: 8
		public override void ExecAction(
			UnitCtrl _source,
			BasePartsData _target,
			int _num,
			UnitActionController _sourceActionController,
			Skill _skill,
			float _starttime,
			Dictionary<int, bool> _enabledChildAction,
			ValueDictionary _valueDictionary,
			Action<string> action = null)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);

			effectCounter = BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_1]);

			int counter = 0;

			effectMaxCount = (int)_valueDictionary[eValueNumber.VALUE_2];

			int effexecLimitNum = BattleUtil.DoubleToInt(_valueDictionary[eValueNumber.VALUE_3]);

			Action nxtAction = () =>
			{
				startEffect(_source);
				_source.ExecSkillWithTargetId(_skill, ActionDetail2);
			};

			Action callback = () =>
				{
					if (!judgeSilenceOrToad(_source))
					{
						if (effexecLimitNum == 0 || counter < effexecLimitNum)
						{
							counter++;
							if (BattleManager.Instance.ChargeSkillTurn == eChargeSkillTurn.NONE)
								nxtAction();
							else
							{
								_target.Owner.AppendCoroutine(waitChargeSkillTurnNone(nxtAction, _target.Owner), ePauseType.SYSTEM);
							}
						}
					}
				};

			// 当触发类型为设置增益时，针对目标拥有者启动效果
			if ((eTriggerType)ActionDetail1 == eTriggerType.SET_BUFF)
			{
				_target.Owner.OnSetBuffForEnchantTrigger += callback;
			}
			else if ((eTriggerType)ActionDetail1 == eTriggerType.GIVE_CRITICAL)
			{
				_target.Owner.OnGiveCriticalDamage += callback;
			}
			else
			{
				Debug.Log("EnchantTriggerAction: Unknown Trigger Type " + ActionDetail1);
			}
		}

		// RVA: 0x30DE200 Offset: 0x30DE200 VA: 0x30DE200
		public void startEffect(UnitCtrl _target)
        {
            // ignore effect
        }

		// RVA: 0x30DE658 Offset: 0x30DE658 VA: 0x30DE658
		public IEnumerator UpdateEffect(float _deltaTime)
		{
			// ignore effect
			yield break;
		}

		// RVA: 0x30DE750 Offset: 0x30DE750 VA: 0x30DE750
		private IEnumerator waitChargeSkillTurnNone(Action _callback, UnitCtrl _source)
		{
			while (BattleManager.Instance.ChargeSkillTurn != eChargeSkillTurn.NONE)
				yield return null;
			if (_source.Hp > 0)
				_callback?.Invoke();
		}

		// RVA: 0x30DE86C Offset: 0x30DE86C VA: 0x30DE86C Slot: 9
		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
		}

		// RVA: 0x30DE8DC Offset: 0x30DE8DC VA: 0x30DE8DC
		private bool judgeSilenceOrToad(UnitCtrl _unitCtrl)
		{
			return _unitCtrl.IsAbnormalState(UnitCtrl.eAbnormalState.SILENCE) || _unitCtrl.ToadDatas.Count > 0;
		}

	}
}
