using System;
using System.Collections.Generic;
using Cute;

namespace Elements
{
	// Token: 0x02000836 RID: 2102
	public class UnableStateGuardAction : ActionParameter
	{
		// Token: 0x0600415F RID: 16735 RVA: 0x001141C4 File Offset: 0x001141C4
		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			UnitCtrl owner = _target.Owner;
			bool flag = (int)_valueDictionary[eValueNumber.VALUE_1] == -1;
			int num = flag ? 1 : ((int)_valueDictionary[eValueNumber.VALUE_1]);
			if (owner.UnableStateGuardData == null)
			{
				owner.UnableStateGuardData = new SealData
				{
					Max = num,
					DisplayCount = !flag
				};
			}
			else
			{
				SealData unableStateGuardData = owner.UnableStateGuardData;
				unableStateGuardData.RemoveSeal(unableStateGuardData.Count, true);
				unableStateGuardData.Max = num;
				unableStateGuardData.DisplayCount = !flag;
			}
			owner.OnChangeState.Call(owner, eStateIconType.UNABLE_STATE_GUARD, true);
			owner.UnableStateGuardData.AddSeal(_valueDictionary[eValueNumber.VALUE_3], owner, eStateIconType.UNABLE_STATE_GUARD, num);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x0011427C File Offset: 0x0011427C
		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			base.Value[eValueNumber.VALUE_1] = (float)(base.MasterData.action_value_1 + base.MasterData.action_value_2 * (double)_level);
			base.Value[eValueNumber.VALUE_3] = (float)(base.MasterData.action_value_3 + base.MasterData.action_value_4 * (double)_level);
		}

		// Token: 0x04002E90 RID: 11920
		private const int RELEASE_TIME_LIMIT_ONLY_VALUE = -1;

		// Token: 0x04002E91 RID: 11921
		private const int RELEASE_TIME_LIMIT_ONLY_MAX_COUNT = 1;
	}
}
