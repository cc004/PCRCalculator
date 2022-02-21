// Decompiled with JetBrains decompiler
// Type: Elements.HealFieldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;

namespace Elements
{
  public class HealFieldData : AbnormalStateDataBase
  {
    public eValueType ValueType { get; set; }

    public eEffectType EffectType { get; set; }

    public float Value { get; set; }

    public bool IsMagic { get; set; }

    public override void StartField()
    {
      /*if (this.EffectType == HealFieldData.eEffectType.HEAL)
      {
        this.skillEffect = this.IPEGKPHMBBN.GetEffect(Singleton<LCEGKJFKOPD>.Instance.MFOOOGCBKGJ);
        this.skillEffect.transform.parent = ExceptNGUIRoot.Transform;
        this.skillEffect.transform.localPosition = new Vector3(this.FNFIJFAIPNE / 540f, (float) ((5000.0 + (double) this.BGAGEJBMAMH.GetRespawnPos(eUnitRespawnPos.MAIN_POS_3)) / 540.0), 0.0f);
        this.skillEffect.transform.localScale = Vector3.one * this.HIKKPKEKLDA / 350f;
        this.skillEffect.InitializeSort();
        if (this.BGAGEJBMAMH.GetBlackOutUnitLength() > 0)
          this.skillEffect.SetSortOrderFront();
        else
          this.skillEffect.SetSortOrderBack();
      }*/
      base.StartField();
    }

    public override void OnRepeat()
    {
      int index = 0;
      for (int count = TargetList.Count; index < count; ++index)
                TargetList[index].Owner.SetRecovery((int)Value, IsMagic ? UnitCtrl.eInhibitHealType.MAGIC : UnitCtrl.eInhibitHealType.PHYSICS, TargetList[index].Owner, UnitCtrl.GetHealDownValue(PPOJKIDHGNJ), _target: TargetList[index], _releaseToad: true);

            //this.TargetList[index].Owner.SetRecovery((int) this.Value, this.IsMagic ? UnitCtrl.eInhibitHealType.MAGIC : UnitCtrl.eInhibitHealType.PHYSICS, this.TargetList[index].Owner, _target: this.TargetList[index]);
    }

    public enum eValueType
    {
      FIXED = 1,
      PERCENTAGE = 2,
    }

    public enum eEffectType
    {
      NONE,
      HEAL,
    }
  }
}
