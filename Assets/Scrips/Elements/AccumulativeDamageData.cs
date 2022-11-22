// Decompiled with JetBrains decompiler
// Type: Elements.AccumulativeDamageData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using UnityEngine;

namespace Elements
{
  public class AccumulativeDamageData
  {
    public eAccumulativeDamageType AccumulativeDamageType { get; set; }

    public float FixedValue { get; set; }

    public float PercentageValue { get; set; }

    public int DamagedCount { get; set; }

    public int CountLimit { get; set; }
		public FloatWithEx CalcAdditionalDamage(int _count, FloatWithEx _baseDamage)
		{
			int num = Mathf.Min(_count, CountLimit);
			return AccumulativeDamageType switch
			{
				eAccumulativeDamageType.FIXED => (num * FixedValue),
				eAccumulativeDamageType.PERCENTAGE => ((_baseDamage * num) * PercentageValue / 100f),
				_ => 0,
			};
		}
	}
}
