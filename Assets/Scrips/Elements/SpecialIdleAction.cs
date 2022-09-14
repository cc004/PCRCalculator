using System.Collections.Generic;

namespace Elements
{
	public class SpecialIdleAction : ActionParameter
	{
		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			_target.Owner.SpecialIdleMotionId = base.ActionDetail1;
		}
	}
}