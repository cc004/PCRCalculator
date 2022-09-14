using System.Collections.Generic;
using Cute;
using UnityEngine;

namespace Elements
{
	public class ChargeEnergyByDamage : ActionParameter
	{
		private enum eChargeType
		{
			INCREASE = 1
		}

		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			UnitCtrl targetUnit = _target.Owner;
			AppendIsAlreadyExeced(targetUnit, _num);
			eStateIconType actionDetail = (eStateIconType)base.ActionDetail2;
			if (!targetUnit.SealDictionary.ContainsKey(actionDetail))
			{
				targetUnit.SealDictionary.Add(actionDetail, new SealData
				{
					Max = (int)_valueDictionary[eValueNumber.VALUE_4],
					DisplayCount = true
				});
			}
			else
			{
				targetUnit.SealDictionary[actionDetail].Max = Mathf.Max((int)_valueDictionary[eValueNumber.VALUE_4], targetUnit.SealDictionary[actionDetail].Max);
			}
			SealData sealData = targetUnit.SealDictionary[actionDetail];
			if (sealData.GetCurrentCount() == 0)
			{
				targetUnit.OnChangeState.Call(targetUnit, actionDetail, ADIFIOLCOPN: true);
			}
			sealData.AddSeal(_valueDictionary[eValueNumber.VALUE_5], targetUnit, actionDetail, (int)_valueDictionary[eValueNumber.VALUE_3]);
			eChargeType chargeType = (eChargeType)base.ActionDetail1;
			float chargeValue = 0f;
			eChargeType eChargeType = chargeType;
			if (eChargeType == eChargeType.INCREASE)
			{
				chargeValue = Mathf.Min(1000f, _valueDictionary[eValueNumber.VALUE_1]);
			}
			targetUnit.ChargeEnergyByReceiveDamageDictionary[actionDetail] = delegate
			{
				if (sealData.GetCurrentCount() != 0)
				{
					targetUnit.ChargeEnergy(eSetEnergyType.BY_CHANGE_ENERGY, chargeValue, chargeType == eChargeType.INCREASE, _source);
					sealData.RemoveSeal(1, _isPassiveSeal: true);
				}
			};
		}

		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			base.Value[eValueNumber.VALUE_1] = (float)((double)base.MasterData.action_value_1 + (double)base.MasterData.action_value_2 * (double)_level);
		}
	}
}
