// Decompiled with JetBrains decompiler
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
    [ThreadStatic]
    private static Stack<DamageData> s_pool;

    public static DamageData Rent()
    {
        if (s_pool != null && s_pool.Count > 0)
            return s_pool.Pop();
        return new DamageData();
    }

    public void Return()
    {
        Target = null;
        Source = null;
        StrikeBackSource = null;
        SourceForDamageNumEffect = null;
        LimitDamageFunc = null;
        ExecAbsorber = null;
        OverrideDamageLimit = -1;
        DamegeNumScale = 1f;
        TargetIncludeMyTeam = false;
        IgnoreTalentBonusForRatioSlip = false;
        IgnoreStrikeBackAndAllUnitDamageCutTrigger = false;
        IgnoreLogBarrier = false;
        IsLogBarrierCritical = false;
        IgnoreDef = false;
        IsDivisionDamage = false;
        IsSlipDamage = false;
        DamageNumMagic = false;
        IsAlwaysChargeEnegry = false;
        CriticalRate = 0f;
        CriticalRateForLogBarrier = 0f;
        CriticalDamageRate = 0.0;
        DamageType = eDamageType.NONE;
        ActionType = eActionType.ATTACK;
        DefPenetrate = 0;
        LifeSteal = 0;
        s_pool ??= new Stack<DamageData>();
        s_pool.Push(this);
    }

    public eDamageSoundType DamageSoundType { get; set; }
    public DoubleWithEx critVar;
    public BasePartsData Target { get; set; }

    public LongWithEx TotalDamageForLogBarrier { get; set; }

    public bool IsLogBarrierCritical { get; set; }

    public bool TargetIncludeMyTeam { get; set; }

    public bool IgnoreTalentBonusForRatioSlip { get; set; }

    public UnitCtrl SourceForDamageNumEffect { get; set; } // TODO check where use

    public long OverrideDamageLimit { get; set; } = -1;

    public bool IgnoreStrikeBackAndAllUnitDamageCutTrigger { get; set; } // TODO check where set
    public bool IgnoreLogBarrier { get; set; }

    public LongWithEx LogBarrierExpectedDamage { get; set; }

    public LongWithEx LogBarrierExpectedDamageForEnergyCalc { get; set; }

    public LongWithEx Damage { get; set; }

    public UnitCtrl Source { get; set; }
        public UnitCtrl StrikeBackSource
        {
            get;
            set;
        }
        public float CriticalRate { get; set; }
        public float CriticalRateForLogBarrier
        {
            get;
            set;
        }
        public eDamageType DamageType { get; set; }

    public eDamageEffectType DamegeEffectType { get; set; }

    public long DefPenetrate { get; set; }

    public eActionType ActionType { get; set; }

    public bool IgnoreDef { get; set; }

    public bool IsDivisionDamage { get; set; }

    public long LifeSteal { get; set; }
    public Func<double, double> LimitDamageFunc
    {
        get;
        set;
    }
    public bool IsSlipDamage { get; set; }

    public double CriticalDamageRate { get; set; }

    public bool DamageNumMagic { get; set; }

    public float DamegeNumScale { get; set; } = 1f;

    public Func<long, long> ExecAbsorber { get; set; }
        public bool IsAlwaysChargeEnegry
        {
            get;
            set;
        }
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

    public class eDamageSoundType_DictComparer : IEqualityComparer<eDamageSoundType>
    {
      public bool Equals(eDamageSoundType _x, eDamageSoundType _y) => _x == _y;

      public int GetHashCode(eDamageSoundType _obj) => (int) _obj;
    }
  }
}
