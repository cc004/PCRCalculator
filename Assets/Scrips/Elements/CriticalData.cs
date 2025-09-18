// Decompiled with JetBrains decompiler
// Type: Elements.CriticalData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public class CriticalData
  {
    public DoubleWithEx critVar { get; set; }
    public bool IsCritical { get; set; }

    public double CriticalRate { get; set; }

    public LongWithEx ExpectedDamage;
        public LongWithEx ExpectedDamageNotCritical
        {
            get;
            set;
        }
        public LongWithEx ExpectedDamageForEnergyCalc { get; set; }
    }
}
