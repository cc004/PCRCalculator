using System.Collections.Generic;

namespace Elements
{
	public class DamageLimitAction : ActionParameter
	{
		private enum eDamageCutType
		{
			ATK = 1,
			MGC,
			ALL
		}

		private static readonly Dictionary<eDamageCutType, UnitCtrl.eAbnormalState> abnormalStateDic = new Dictionary<eDamageCutType, UnitCtrl.eAbnormalState>
		{
			{
				eDamageCutType.ATK,
				UnitCtrl.eAbnormalState.DAMAGE_LIMIT_ATK
			},
			{
				eDamageCutType.MGC,
				UnitCtrl.eAbnormalState.DAMAGE_LIMIT_MGC
			},
			{
				eDamageCutType.ALL,
				UnitCtrl.eAbnormalState.DAMAGE_LIMIT_ALL
			}
		};

		public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
		{
			base.ExecActionOnStart(_skill, _source, _sourceActionController);
		}

		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			AppendIsAlreadyExeced(_target.Owner, _num);
			_target.Owner.SetAbnormalState(_source, abnormalStateDic[(eDamageCutType)base.ActionDetail1], _valueDictionary[eValueNumber.VALUE_3], this, _skill, (int)_valueDictionary[eValueNumber.VALUE_1]);
		}

		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			base.Value[eValueNumber.VALUE_1] = (float)((double)base.MasterData.action_value_1 + (double)base.MasterData.action_value_2 * (double)_level);
			base.Value[eValueNumber.VALUE_3] = (float)((double)base.MasterData.action_value_3 + (double)base.MasterData.action_value_4 * (double)_level);
		}
	}
}
