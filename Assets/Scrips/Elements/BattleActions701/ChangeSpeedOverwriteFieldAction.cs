using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    // Token: 0x02000755 RID: 1877
    public class ChangeSpeedOverwriteFieldAction : ActionParameter
	{
		// Token: 0x06003D80 RID: 15744 RVA: 0x00101774 File Offset: 0x00101774
		public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
		{
			base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
			GameObject uniqueEffectPrefab = null;
			GameObject uniqueEffectPrefabLeft = null;
			if (base.ActionSubEffectList.Count > 0)
			{
				uniqueEffectPrefab = base.ActionSubEffectList[0].Prefab;
				uniqueEffectPrefabLeft = base.ActionSubEffectList[0].PrefabLeft;
			}
			UnitCtrl.eOverwriteAbnormalState eOverwriteAbnormalState = ChangeSpeedOverwriteFieldAction.abnormalStateDic[(ChangeSpeedAction.eChangeSpeedType)base.ActionDetail1];
			base.battleManager.AddChangeSpeedOverwriteFieldCounter();
			ChangeSpeedOverwriteFieldData pfdaefdobip = new ChangeSpeedOverwriteFieldData
			{
				KNLCAOOKHPP = eFieldType.HEAL,
				HKDBJHAIOMB = eFieldExecType.NORMAL,
				StayTime = _valueDictionary[eValueNumber.VALUE_3],
				CenterX = _target.GetLocalPosition().x + base.Position,
				Size = _valueDictionary[eValueNumber.VALUE_5],
				LCHLGLAFJED = ((_source.IsOther == (eOverwriteAbnormalState == UnitCtrl.eOverwriteAbnormalState.HASTE)) ? eFieldTargetType.ENEMY : eFieldTargetType.PLAYER),
				EGEPDDJBILL = ((_skill.BlackOutTime > 0f) ? _source : null),
				AbnormalState = eOverwriteAbnormalState,
				TargetList = new List<BasePartsData>(),
				EffectValue = base.Value[eValueNumber.VALUE_1],
				PPOJKIDHGNJ = _source,
				PMHDBOJMEAD = _skill,
				ActionParameter = this,
				ActionCounter = base.battleManager.CJJBDIJPHJE,
				HGMNJJBLJIO = uniqueEffectPrefab,
				LALMMFAOJDP = uniqueEffectPrefabLeft,
				IsShowUniqueEffect = (base.ActionDetail2 == 1)
			};
			base.battleManager.ExecField(pfdaefdobip, base.ActionId);
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x001018D8 File Offset: 0x001018D8
		public override void SetLevel(float _level)
		{
			base.SetLevel(_level);
			base.Value[eValueNumber.VALUE_1] = (float)(base.MasterData.action_value_1 + (double)_level * base.MasterData.action_value_2);
			base.Value[eValueNumber.VALUE_3] = (float)(base.MasterData.action_value_3 + (double)_level * base.MasterData.action_value_4);
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x00101950 File Offset: 0x00101950
		public ChangeSpeedOverwriteFieldAction()
		{
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x00101958 File Offset: 0x00101958
		// Note: this type is marked as 'beforefieldinit'.
		static ChangeSpeedOverwriteFieldAction()
		{
		}

		// Token: 0x04002B08 RID: 11016
		private static readonly Dictionary<ChangeSpeedAction.eChangeSpeedType, UnitCtrl.eOverwriteAbnormalState> abnormalStateDic = new Dictionary<ChangeSpeedAction.eChangeSpeedType, UnitCtrl.eOverwriteAbnormalState>
		{
			{
				ChangeSpeedAction.eChangeSpeedType.SLOW,
				UnitCtrl.eOverwriteAbnormalState.SLOW
			},
			{
				ChangeSpeedAction.eChangeSpeedType.HASTE,
				UnitCtrl.eOverwriteAbnormalState.HASTE
			}
		};

		// Token: 0x04002B09 RID: 11017
		private const int UNIQUE_EFFECT_SHOW_VALUE = 1;
	}
}
namespace Elements
{
}
