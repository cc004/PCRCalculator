// Decompiled with JetBrains decompiler
// Type: Elements.AbnormalStateCategoryData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class AbnormalStateCategoryData
  {
    public UnitCtrl.eAbnormalState CurrentAbnormalState;
    public bool enable;
    public FloatWithEx MainValue;
    public const float SLIP_DAMAGE_INTERVAL_TIME = 1f;
    public float Time;
    public float Duration;
    public List<AbnormalStateEffectGameObject> Effects = new List<AbnormalStateEffectGameObject>();
    public int ActionId;
    public float EnergyChargeMultiple = 1f;

    public float SubValue { get; set; }

    public float EnergyReduceRate { get; set; }

    public bool IsEnergyReduceMode { get; set; }

    public bool IsDamageRelease { get; set; }

    public bool IsReleasedByDamage { get; set; }

    public int AbsorberValue { get; set; }

    public Skill Skill { get; set; }

    public UnitCtrl Source { get; set; }
  }
}
