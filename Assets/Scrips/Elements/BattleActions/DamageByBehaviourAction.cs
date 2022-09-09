using System.Collections.Generic;
using Elements.Battle;

namespace Elements
{
	public enum eSlipDamageType
	{
		NO_EFFECT,
		POISON,
		BURN,
		CURSE,
		VENOM,
		HEX,
		COMPENSATION,
		POISON2,
		CURSE2
	}
	public class DamageByBehaviourAction : ActionParameter
	{
		public static readonly Dictionary<eSlipDamageType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<eSlipDamageType, UnitCtrl.eAbnormalState>
		{
			{
				eSlipDamageType.POISON,
				UnitCtrl.eAbnormalState.POISON_BY_BEHAVIOUR
			}
		};

		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			if (BattleManager.Random(0f, 1f,new PCRCaculator.Guild.RandomData(_source,_target.Owner,ActionId,79,0)) < BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()))
			{
				AppendIsAlreadyExeced(_target.Owner, _num);
				_target.Owner.SetAbnormalState(_source, abnormalStateDic[(eSlipDamageType)base.ActionDetail1], (base.AbnormalStateFieldAction == null) ? (float)_valueDictionary[eValueNumber.VALUE_3] : 90f, this, _skill, (int)_valueDictionary[eValueNumber.VALUE_1], (float)_valueDictionary[eValueNumber.VALUE_5], _reduceEnergy: false, _isDamageRelease: false, 0f);
				return;
			}
			ActionExecedData actionExecedData = base.AlreadyExecedData[_target.Owner][_num];
			if (actionExecedData.ExecedPartsNumber == actionExecedData.TargetPartsNumber)
			{
				if (actionExecedData.TargetPartsNumber == 1)
				{
					_target.Owner.SetMissAtk(_source, eMissLogType.DODGE_DAMAGE_BY_BEHAVIOUR, eDamageEffectType.NORMAL, _target);
				}
				else
				{
					_target.Owner.SetMissAtk(_source, eMissLogType.DODGE_DAMAGE_BY_BEHAVIOUR);
				}
			}
		}

		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			base.Value[eValueNumber.VALUE_1] = (float)((double)base.MasterData.action_value_1 + (double)base.MasterData.action_value_2 * (double)_level);
			base.Value[eValueNumber.VALUE_3] = (float)((double)base.MasterData.action_value_3 + (double)base.MasterData.action_value_4 * (double)_level);
		}
	}
}
