using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;

namespace Elements
{
	public class BuffDebuffClearAction : ActionParameter
	{
		private const float CLEAR_SUCCESS_RATE_BASE = 100f;
		private List<int> BuffDebuffCancelType = new List<int>();


		public override void ExecActionOnStart(
		  Skill _skill,
		  UnitCtrl _source,
		  UnitActionController _sourceActionController)
		  {
			base.ExecActionOnStart(_skill, _source, _sourceActionController);
			if (base.ActionDetail1 == 20)
			{
				BuffDebuffCancelType.Add((int)base.Value[eValueNumber.VALUE_4]);
				BuffDebuffCancelType.Add((int)base.Value[eValueNumber.VALUE_5]);
				BuffDebuffCancelType.Add((int)base.Value[eValueNumber.VALUE_6]);
				BuffDebuffCancelType.Add((int)base.Value[eValueNumber.VALUE_7]);
				BuffDebuffCancelType.RemoveAll((int buffParam) => buffParam == 0);
			}

		  }

		public override void ExecAction(
		  UnitCtrl _source,
		  BasePartsData _target,
		  int _num,
		  UnitActionController _sourceActionController,
		  Skill _skill,
		  float _starttime,
		  Dictionary<int, bool> _enabledChildAction,
		  ValueDictionary _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			bool flag = BattleManager.Random(0.0f, 1f, new RandomData(_source, _target.Owner, ActionId, 18, BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target))) >= (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target) && ActionDetail1 == 1;
			if (BattleManager.Random(0.0f, 1f,
					new RandomData(_source, _target.Owner, ActionId, 18, Value[eValueNumber.VALUE_1] / 100.0f)) > (double)_valueDictionary[eValueNumber.VALUE_1] / 100.0 | flag && (double)Value[eValueNumber.VALUE_3] == 0.0)
			{
				ActionExecedData actionExecedData = AlreadyExecedData[_target.Owner][_num];
				if (actionExecedData.ExecedPartsNumber != actionExecedData.TargetPartsNumber)
					return;
				if (actionExecedData.TargetPartsNumber == 1)
					_target.Owner.SetMissAtk(_source, eMissLogType.DODGE_ATTACK, _parts: _target);
				else
					_target.Owner.SetMissAtk(_source, eMissLogType.DODGE_ATTACK);
			}
			else
			{
				AppendIsAlreadyExeced(_target.Owner, _num);
				switch ((eDetail1Type)base.ActionDetail1)
				{
					case eDetail1Type.BUFF:
						_target.Owner.DespeleBuffDebuff(_isBuff: true, CreateAbnormalEffectData());
						break;
					case eDetail1Type.DEBUFF:
						_target.Owner.DespeleBuffDebuff(_isBuff: false, CreateAbnormalEffectData());
						break;
					case eDetail1Type.STATUS_UP_CLEAR:
						_target.Owner.CancelStatusUpBuff();
						break;
					case eDetail1Type.ALL_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_ATK);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_MGC);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_BOTH);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_BOTH);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_ATK);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_MGC);
						break;
					case eDetail1Type.GUARD_ATK_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_ATK);
						break;
					case eDetail1Type.GUARD_MGC_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_MGC);
						break;
					case eDetail1Type.DRAIN_ATK_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_ATK);
						break;
					case eDetail1Type.DRAIN_MGC_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_MGC);
						break;
					case eDetail1Type.GUARD_BOTH_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_BOTH);
						break;
					case eDetail1Type.DRAIN_BOTH_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_BOTH);
						break;
					case eDetail1Type.ALL_ATK_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_ATK);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_ATK);
						break;
					case eDetail1Type.ALL_MGC_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_MGC);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_MGC);
						break;
					case eDetail1Type.ALL_BOTH_BARRIER:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_BOTH);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_BOTH);
						break;
					case eDetail1Type.BUFF_DEBUFF_TYPE:
					{
						for (int i = 0; i < BuffDebuffCancelType.Count; i++)
						{
							_target.Owner.CancelBuffDebuffType(BuffDebuffCancelType[i]);
						}
						break;
					}

					default:
						Debug.LogError("未対応のBuffDebuffClearActionの詳細1タイプです ActionId:" + ActionId + " Detail1:" + ActionDetail1);
						break;
				}
			}
		}

		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			Value[eValueNumber.VALUE_1] = MasterData.action_value_1 + MasterData.action_value_2 * _level;
		}

		private enum eDetail1Type
		{
			BUFF = 1,
			DEBUFF = 2,
			STATUS_UP_CLEAR = 3,
			ALL_BARRIER = 10,
			GUARD_ATK_BARRIER = 11,
			GUARD_MGC_BARRIER = 12,
			DRAIN_ATK_BARRIER = 13,
			DRAIN_MGC_BARRIER = 14,
			GUARD_BOTH_BARRIER = 15,
			DRAIN_BOTH_BARRIER = 16,
			ALL_ATK_BARRIER = 17,
			ALL_MGC_BARRIER = 18,
			ALL_BOTH_BARRIER = 19,
			BUFF_DEBUFF_TYPE = 20

		}
	}
}
