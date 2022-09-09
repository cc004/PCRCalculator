using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Guild;

namespace Elements
{
	public class BuffDebuffClearAction : ActionParameter
	{
		private const float CLEAR_SUCCESS_RATE_BASE = 100f;

		public override void ExecActionOnStart(
		  Skill _skill,
		  UnitCtrl _source,
		  UnitActionController _sourceActionController) => base.ExecActionOnStart(_skill, _source, _sourceActionController);

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
			bool flag = BattleManager.Random(0.0f, 1f, new RandomData(_source, _target.Owner, ActionId, 18, BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))) >= (double)BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()) && ActionDetail1 == 1;
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
				switch (base.ActionDetail1)
				{
					case 1:
						_target.Owner.DespeleBuffDebuff(_isBuff: true, CreateAbnormalEffectData());
						break;
					case 2:
						_target.Owner.DespeleBuffDebuff(_isBuff: false, CreateAbnormalEffectData());
						break;
					case 10:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_ATK);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_MGC);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_BOTH);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_BOTH);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_ATK);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_MGC);
						break;
					case 11:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_ATK);
						break;
					case 12:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_MGC);
						break;
					case 13:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_ATK);
						break;
					case 14:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_MGC);
						break;
					case 15:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_BOTH);
						break;
					case 16:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_BOTH);
						break;
					case 17:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_ATK);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_ATK);
						break;
					case 18:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_MGC);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_MGC);
						break;
					case 19:
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.GUARD_BOTH);
						_target.Owner.DispelAbnormalState(UnitCtrl.eAbnormalState.DRAIN_BOTH);
						break;
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
						break;
				}
			}
		}

		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
		}

		private enum eDetail1Type
		{
			BUFF = 1,
			DEBUFF = 2,
			ALL_BARRIER = 10,
			GUARD_ATK_BARRIER = 11,
			GUARD_MGC_BARRIER = 12,
			DRAIN_ATK_BARRIER = 13,
			DRAIN_MGC_BARRIER = 14,
			GUARD_BOTH_BARRIER = 0xF,
			DRAIN_BOTH_BARRIER = 0x10,
			ALL_ATK_BARRIER = 17,
			ALL_MGC_BARRIER = 18,
			ALL_BOTH_BARRIER = 19
		}
	}
}
