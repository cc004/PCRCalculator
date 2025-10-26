using System;
using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator;

namespace Elements
{
	// Token: 0x02000754 RID: 1876
	public class ChangeSpeedOverwriteFieldData : AbnormalStateDataBase
	{
		// Token: 0x06003D7B RID: 15739 RVA: 0x001015A8 File Offset: 0x001015A8
		public override void StartField()
		{
			this.alreadyExecedTargetCount = new Dictionary<UnitCtrl, int>();
			/*
			if (base.HGMNJJBLJIO != null && this.IsShowUniqueEffect)
			{
				this.skillEffect = base.IPEGKPHMBBN.GetEffect(base.PPOJKIDHGNJ.IsLeftDir ? base.LALMMFAOJDP : base.HGMNJJBLJIO, null);
				base.initializeSkillEffect();
			}*/
			base.StartField();
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x00101610 File Offset: 0x00101610
		public override void OnEnter(BasePartsData _parts, UnitCtrl _source)
		{
			base.OnEnter(_parts, _source);
			UnitCtrl owner = _parts.Owner;
			int num;
			this.alreadyExecedTargetCount.TryGetValue(owner, out num);
			num++;
			this.alreadyExecedTargetCount[owner] = num;
			if (num != 1)
			{
				return;
			}
			if (PCRCaculator.MainManager.Instance.PlayerSetting.IsReleaseSkillHasteLogic)
				owner.AddOverwriteAbnormalState(this.ActionCounter, new OverwriteAbnormalStateData(_parts, base.PPOJKIDHGNJ, base.PMHDBOJMEAD, this.ActionParameter, this.AbnormalState, this.EffectValue));
			else
				owner.AddOverwriteAbnormalStateOld(this.ActionCounter, new OverwriteAbnormalStateData(_parts, base.PPOJKIDHGNJ, base.PMHDBOJMEAD, this.ActionParameter, this.AbnormalState, this.EffectValue));
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x00101680 File Offset: 0x00101680
		public override void OnExit(BasePartsData _parts, UnitCtrl _source)
		{
			base.OnExit(_parts, _source);
			UnitCtrl owner = _parts.Owner;
			Dictionary<UnitCtrl, int> dictionary = this.alreadyExecedTargetCount;
			UnitCtrl key = owner;
			int num = dictionary[key];
			dictionary[key] = num - 1;
			if (this.alreadyExecedTargetCount[owner] > 0)
			{
				return;
			}
			if (PCRCaculator.MainManager.Instance.PlayerSetting.IsReleaseSkillHasteLogic)
				owner.RemoveOverwriteAbnormalState(this.ActionCounter);
			else
				owner.RemoveOverwriteAbnormalStateOld(this.ActionCounter);
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x001016D0 File Offset: 0x001016D0
		public override void ResetTarget(UnitCtrl _unit, UnitCtrl.eAbnormalState _abnormalState, bool _strict)
		{
			base.ResetTarget(_unit, _abnormalState, _strict);
			if (_strict)
			{
				if (UnitCtrl.GetAbnormalState(this.AbnormalState) != _abnormalState)
				{
					return;
				}
			}
			else
			{
				if (UnitCtrl.GetAbnormalStateCategory(UnitCtrl.GetAbnormalState(this.AbnormalState)) != UnitCtrl.GetAbnormalStateCategory(_abnormalState))
				return;
			}
			List<BasePartsData> list = new List<BasePartsData>();
			if (!_unit.IsPartsBoss)
			{
				list.Add(_unit.GetFirstParts(false, 0f));
			}
			else
			{
				for (int i = 0; i < _unit.BossPartsListForBattle.Count; i++)
				{
					list.Add(_unit.BossPartsListForBattle[i]);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				BasePartsData basePartsData = list[j];
				if (base.TargetList.Contains(basePartsData))
				{
					if (MainManager.Instance.PlayerSetting.IsReleaseSkillHasteLogic)
						TargetList.Remove(basePartsData);
					this.OnExit(basePartsData, _unit);
				}
			}
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x0010176C File Offset: 0x0010176C
		public ChangeSpeedOverwriteFieldData()
		{
		}

		// Token: 0x04002B02 RID: 11010
		public ActionParameter ActionParameter;

		// Token: 0x04002B03 RID: 11011
		public int ActionCounter;

		// Token: 0x04002B04 RID: 11012
		public UnitCtrl.eOverwriteAbnormalState AbnormalState;

		// Token: 0x04002B05 RID: 11013
		public float EffectValue;

		// Token: 0x04002B06 RID: 11014
		public bool IsShowUniqueEffect;

		// Token: 0x04002B07 RID: 11015
		private Dictionary<UnitCtrl, int> alreadyExecedTargetCount;
	}
}
