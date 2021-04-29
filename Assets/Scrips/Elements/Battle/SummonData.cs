// Decompiled with JetBrains decompiler
// Type: Elements.Battle.NAPBDMBAAEM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using UnityEngine;

namespace Elements.Battle
{
  public class SummonData
  {
    public int SummonId { get; set; }

    public Vector3 TargetPosition { get; set; }

    public Vector3 FromPosition { get; set; }

    public UnitCtrl Owner { get; set; }

    public Skill Skill { get; set; }

    public GameObject Prefab { get; set; }

    public float MoveSpeed { get; set; }

    public SummonAction.eSummonSide SummonSide { get; set; }

    public SummonAction.eSummonType SummonType { get; set; }

    public bool UseRespawnPos { get; set; }

    public eUnitRespawnPos RespawnPos { get; set; }

    public bool ConsiderEquipmentAndBonus { get; set; }
  }
}
