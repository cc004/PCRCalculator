﻿// Decompiled with JetBrains decompiler
// Type: Elements.DamageData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Elements
{
  [Serializable]
  public class DamageData
  {
    public DamageData.eDamageSoundType DamageSoundType { get; set; }

    public BasePartsData Target { get; set; }

    public long TotalDamageForLogBarrier { get; set; }

    public bool IsLogBarrierCritical { get; set; }

    public long LogBarrierExpectedDamage { get; set; }

    public long Damage { get; set; }

    public UnitCtrl Source { get; set; }

    public float CriticalRate { get; set; }

    public DamageData.eDamageType DamageType { get; set; }

    public eDamageEffectType DamegeEffectType { get; set; }

    public int DefPenetrate { get; set; }

    public eActionType ActionType { get; set; }

    public bool IgnoreDef { get; set; }

    public bool IsDivisionDamage { get; set; }

    public int LifeSteal { get; set; }

    public bool IsSlipDamage { get; set; }

    public float CriticalDamageRate { get; set; }

    public bool DamageNumMagic { get; set; }

    public float DamegeNumScale { get; set; } = 1f;

    public Func<int, int> ExecAbsorber { get; set; }

    public enum eDamageType
    {
      ATK = 1,
      MGC = 2,
      NONE = 3,
    }

    public enum eDamageSoundType
    {
      HIT,
      SLIP,
    }

    public class eDamageSoundType_DictComparer : IEqualityComparer<DamageData.eDamageSoundType>
    {
      public bool Equals(DamageData.eDamageSoundType _x, DamageData.eDamageSoundType _y) => _x == _y;

      public int GetHashCode(DamageData.eDamageSoundType _obj) => (int) _obj;
    }
  }
}