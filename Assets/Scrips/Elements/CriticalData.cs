// Decompiled with JetBrains decompiler
// Type: Elements.CriticalData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public class CriticalData
  {
      public FloatWithEx critVar { get; set; }
    public bool IsCritical { get; set; }

    public float CriticalRate { get; set; }

    public FloatWithEx ExpectedDamage;
        public FloatWithEx ExpectedDamageNotCritical
        {
            get;
            set;
        }
        public FloatWithEx ExpectedDamageForEnergyCalc { get; set; }
    }
}
