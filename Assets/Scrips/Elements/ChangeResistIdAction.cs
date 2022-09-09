using System.Collections;
using System.Collections.Generic;

namespace Elements
{
	public class ChangeResistIdAction : ActionParameter
	{
		private enum eResistType
		{
			ABNORMAL_STATE = 1,
			DEBUFF
		}

		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			setResistId(_target, base.ActionDetail2);
			_sourceActionController.AppendCoroutine(updateResistId(_target, getResistSetCount(_target)), ePauseType.SYSTEM);
		}

		private int getResistSetCount(BasePartsData _target)
		{
			return base.ActionDetail1 switch
			{
				1 => _target.AbnormalResistIdSetCount,
				2 => _target.DebuffResistIdSetCount,
				_ => 0,
			};
		}

		private IEnumerator updateResistId(BasePartsData _target, int _resistSetCount)
		{
			float passedTime = 0f;
			float effectTime = base.Value[eValueNumber.VALUE_1];
			do
			{
				yield return null;
				passedTime += _target.Owner.DeltaTimeForPause;
				if (_resistSetCount != getResistSetCount(_target))
				{
					yield break;
				}
			}
			while (!BattleUtil.LessThanOrApproximately(effectTime, passedTime));
			setResistId(_target, getBattleStartResistId(_target));
		}

		private int getBattleStartResistId(BasePartsData _target)
		{
			return base.ActionDetail1 switch
			{
				1 => _target.StartAbnormalResistId,
				2 => _target.StartDebuffResistId,
				_ => 0,
			};
		}

		private void setResistId(BasePartsData _target, int _resistId)
		{
			switch (base.ActionDetail1)
			{
				case 1:
					_target.SetAbnormalResistId(_resistId, _isInit: false);
					break;
				case 2:
					_target.SetDebuffResistId(_resistId, _isInit: false);
					break;
			}
		}
	}
}
