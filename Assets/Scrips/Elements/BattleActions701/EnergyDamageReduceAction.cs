using System;
using System.Collections.Generic;

namespace Elements
{
	// Token: 0x02000798 RID: 1944
	public class EnergyDamageReduceAction : ActionParameter
	{
		// Token: 0x06003E96 RID: 16022 RVA: 0x00105EA4 File Offset: 0x00105EA4
		public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
		{
			base.ExecActionOnStart(_skill, _source, _sourceActionController);
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x00105EB0 File Offset: 0x00105EB0
		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			base.AppendIsAlreadyExeced(_target.Owner, _num);
			_target.Owner.SetAbnormalState(_source, UnitCtrl.eAbnormalState.ENERGY_DAMAGE_REDUCE, _valueDictionary[eValueNumber.VALUE_2], this, _skill, _valueDictionary[eValueNumber.VALUE_1], 0f, false, false, 1f, _valueDictionary[eValueNumber.VALUE_4] == 0f);
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x00105F1C File Offset: 0x00105F1C
		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			base.Value[eValueNumber.VALUE_2] = (float)(base.MasterData.action_value_2 + base.MasterData.action_value_3 * (double)_level);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x00105F58 File Offset: 0x00105F58
		public EnergyDamageReduceAction()
		{
		}
	}
}
