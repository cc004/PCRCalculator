using System.Collections.Generic;
using Elements.Battle;

namespace Elements
{
	public class ChangeSpeedOverlapAction : ActionParameter
	{
		public static readonly Dictionary<ChangeSpeedAction.eChangeSpeedType, UnitCtrl.eAbnormalState> AbnormalStateDic = new Dictionary<ChangeSpeedAction.eChangeSpeedType, UnitCtrl.eAbnormalState>
		{
			{
				ChangeSpeedAction.eChangeSpeedType.SLOW,
				UnitCtrl.eAbnormalState.SLOW_OVERLAP
			},
			{
				ChangeSpeedAction.eChangeSpeedType.HASTE,
				UnitCtrl.eAbnormalState.HASTE_OVERLAP
			}
		};

		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			if (BattleManager.Random(0f, 1f,new PCRCaculator.Guild.RandomData(_source,_target.Owner,ActionId,93,0)) < BattleUtil.GetDodgeByLevelDiff(_skill.Level, _target.GetLevel()) || base.TargetAssignment == eTargetAssignment.OWNER_SITE)
			{
				AppendIsAlreadyExeced(_target.Owner, _num);
				UnitCtrl.eAbnormalState abnormalState = AbnormalStateDic[(ChangeSpeedAction.eChangeSpeedType)base.ActionDetail1];
				float num = _valueDictionary[eValueNumber.VALUE_1];
				if (abnormalState == UnitCtrl.eAbnormalState.SLOW_OVERLAP)
				{
					num *= -1f;
				}
				else if (abnormalState == UnitCtrl.eAbnormalState.HASTE_OVERLAP && !BattleUtil.LessThanOrApproximately(num + _target.Owner.CalcAbnormalStateSpeed(), 1f))
				{
					_target.Owner.SetBuffParam(UnitCtrl.BuffParamKind.NUM, UnitCtrl.BuffParamKind.NUM, new Dictionary<BasePartsData, FloatWithEx>
					{
						{
							_target,
							0
						}
					}, 0.5f, _skill.SkillId, _source, _despelable: true, eEffectType.COMMON, _isBuff: true, _additional: false, _isShowIcon: false);
				}
				ChangeSpeedAction.eCancelTriggerType actionDetail = (ChangeSpeedAction.eCancelTriggerType)base.ActionDetail2;
				if (actionDetail == ChangeSpeedAction.eCancelTriggerType.DAMAGED && !_target.Owner.OnDamageListForChangeSpeedDisableByAttack.ContainsKey(base.ActionId))
				{
					_target.Owner.OnDamageListForChangeSpeedDisableByAttack.Add(base.ActionId, delegate (bool byAttack)
					{
						if (byAttack)
						{
							_target.Owner.DisableAbnormalStateById(abnormalState, base.ActionId, _isReleasedByDamage: true);
						}
					});
				}
				_target.Owner.SetAbnormalState(_source, abnormalState, (base.AbnormalStateFieldAction == null) ? (float)_valueDictionary[eValueNumber.VALUE_3] : 90f, this, _skill, num, 0f, _reduceEnergy: false, base.ActionDetail2 == 1, 1f, _showsIcon: false);
				return;
			}
			ActionExecedData actionExecedData = base.AlreadyExecedData[_target.Owner][_num];
			if (actionExecedData.ExecedPartsNumber == actionExecedData.TargetPartsNumber)
			{
				if (actionExecedData.TargetPartsNumber == 1)
				{
					_target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SLIP_DAMAGE, eDamageEffectType.NORMAL, _target);
				}
				else
				{
					_target.Owner.SetMissAtk(_source, eMissLogType.DODGE_SLIP_DAMAGE);
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
