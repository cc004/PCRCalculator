﻿// Decompiled with JetBrains decompiler
// Type: Elements.DebuffDamageUpData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

namespace Elements
{
  public class DebuffDamageUpData
  {
    public float DebuffDamageUpValue { get; set; }

    public float DebuffDamageUpLimitValue { get; set; }

    public float DebuffDamageUpTimer { get; set; }

    public eEffectType EffectType { get; set; }

    public enum eEffectType
    {
      ADD = 1,
      SUBTRACT = 2,
    }
  }
}
