namespace Elements
{
    // Token: 0x02000753 RID: 1875
    public class OverwriteAbnormalStateData
	{
		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003D6E RID: 15726 RVA: 0x001014F8 File Offset: 0x001014F8
		// (set) Token: 0x06003D6F RID: 15727 RVA: 0x00101500 File Offset: 0x00101500
		public BasePartsData TargetParts { get; private set; }

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003D70 RID: 15728 RVA: 0x0010150C File Offset: 0x0010150C
		// (set) Token: 0x06003D71 RID: 15729 RVA: 0x00101514 File Offset: 0x00101514
		public UnitCtrl Source { get; private set; }

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x00101520 File Offset: 0x00101520
		// (set) Token: 0x06003D73 RID: 15731 RVA: 0x00101528 File Offset: 0x00101528
		public Skill Skill { get; private set; }

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x00101534 File Offset: 0x00101534
		// (set) Token: 0x06003D75 RID: 15733 RVA: 0x0010153C File Offset: 0x0010153C
		public ActionParameter ActionParameter { get; private set; }

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x00101548 File Offset: 0x00101548
		// (set) Token: 0x06003D77 RID: 15735 RVA: 0x00101550 File Offset: 0x00101550
		public UnitCtrl.eOverwriteAbnormalState AbnormalState { get; private set; }

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x0010155C File Offset: 0x0010155C
		// (set) Token: 0x06003D79 RID: 15737 RVA: 0x00101564 File Offset: 0x00101564
		public float EffectValue { get; private set; }

		// Token: 0x06003D7A RID: 15738 RVA: 0x00101570 File Offset: 0x00101570
		public OverwriteAbnormalStateData(BasePartsData _parts, UnitCtrl _source, Skill _skill, ActionParameter _actionParameter, UnitCtrl.eOverwriteAbnormalState _abnormalState, float _effectValue)
		{
			this.TargetParts = _parts;
			this.Source = _source;
			this.Skill = _skill;
			this.ActionParameter = _actionParameter;
			this.AbnormalState = _abnormalState;
			this.EffectValue = _effectValue;
		}
	}
}
