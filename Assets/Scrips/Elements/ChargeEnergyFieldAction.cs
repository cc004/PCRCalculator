using System;
using System.Collections.Generic;
using Elements.Battle;
using UnityEngine;

namespace Elements
{
	public class ChargeEnergyFieldData : AbnormalStateDataBase
	{
		public enum eChargeType
		{
			INCREASE = 1,
			DECREASE
		}

		public enum eFieldEffectType
		{
			NONE,
			HEAL,
			DAMAGE
		}

		public eChargeType ChargeType = eChargeType.INCREASE;

		public eFieldEffectType FieldEffectType;

		public float Value;

		public override void StartField()
		{
			/*if (base.HGMNJJBLJIO != null)
			{
				skillEffect = base.IPEGKPHMBBN.GetEffect(base.PPOJKIDHGNJ.IsLeftDir ? base.LALMMFAOJDP : base.HGMNJJBLJIO);
				initializeSkillEffect();
			}
			else
			{
				switch (FieldEffectType)
				{
					case eFieldEffectType.HEAL:
						skillEffect = base.IPEGKPHMBBN.GetEffect(Singleton<LCEGKJFKOPD>.Instance.PBFNLBDEDIM);
						initializeSkillEffect();
						break;
					case eFieldEffectType.DAMAGE:
						skillEffect = base.IPEGKPHMBBN.GetEffect(Singleton<LCEGKJFKOPD>.Instance.IAHJLBIGDKN);
						initializeSkillEffect();
						break;
				}
			}*/
			base.StartField();
		}

		public override void OnRepeat()
		{
			List<UnitCtrl> list = new List<UnitCtrl>();
			int i = 0;
			for (int count = base.TargetList.Count; i < count; i++)
			{
				UnitCtrl owner = base.TargetList[i].Owner;
				if (!owner.IsDead && !((float)(long)owner.Hp <= 0f) && !list.Contains(owner))
				{
					owner.ChargeEnergy(eSetEnergyType.BY_CHANGE_ENERGY, Value, ChargeType == eChargeType.INCREASE, base.PPOJKIDHGNJ);
					list.Add(owner);
				}
			}
		}
	}

	public class ChargeEnergyFieldAction : ActionParameter
	{
		public override void Initialize()
		{
			base.Initialize();
			//Singleton<LCEGKJFKOPD>.Instance.LoadChargeEnergyFieldEffect();
		}

		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			ChargeEnergyFieldData.eChargeType actionDetail = (ChargeEnergyFieldData.eChargeType)base.ActionDetail1;
			float num = _valueDictionary[eValueNumber.VALUE_1];
			if (actionDetail == ChargeEnergyFieldData.eChargeType.DECREASE)
			{
				num *= -1f;
			}
			GameObject uniqueEffectPrefab = null;
			GameObject uniqueEffectPrefabLeft = null;
			if (base.ActionEffectList.Count > 0)
			{
				uniqueEffectPrefab = base.ActionEffectList[0].Prefab;
				uniqueEffectPrefabLeft = base.ActionEffectList[0].PrefabLeft;
			}
			ChargeEnergyFieldData pFDAEFDOBIP = new ChargeEnergyFieldData
			{
				KNLCAOOKHPP = eFieldType.HEAL,
				ChargeType = actionDetail,
				HKDBJHAIOMB = eFieldExecType.REPEAT,
				StayTime = _valueDictionary[eValueNumber.VALUE_3],
				CenterX = _target.GetLocalPosition().x + base.Position,
				Size = _valueDictionary[eValueNumber.VALUE_5],
				LCHLGLAFJED = ((_source.IsOther == (actionDetail == ChargeEnergyFieldData.eChargeType.INCREASE)) ? eFieldTargetType.ENEMY : eFieldTargetType.PLAYER),
				EGEPDDJBILL = ((_skill.BlackOutTime > 0f) ? _source : null),
				FieldEffectType = (ChargeEnergyFieldData.eFieldEffectType)base.ActionDetail3,
				TargetList = new List<BasePartsData>(),
				TargetSet = new HashSet<BasePartsData>(),
				Value = num,
				PPOJKIDHGNJ = _source,
				PMHDBOJMEAD = _skill,
				HGMNJJBLJIO = uniqueEffectPrefab,
				LALMMFAOJDP = uniqueEffectPrefabLeft
			};
			base.battleManager.ExecField(pFDAEFDOBIP, base.ActionId);
		}

		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			base.Value[eValueNumber.VALUE_1] = (float)((double)base.MasterData.action_value_1 + Math.Ceiling((double)base.MasterData.action_value_2 * (double)_level));
			base.Value[eValueNumber.VALUE_3] = (float)((double)base.MasterData.action_value_3 + (double)base.MasterData.action_value_4 * (double)_level);
		}
	}
}
