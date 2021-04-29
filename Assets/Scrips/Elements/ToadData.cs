// Decompiled with JetBrains decompiler
// Type: Elements.ToadData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Spine;
using UnityEngine;

namespace Elements
{
  public class ToadData
  {
    public eStateIconType StateIconType;

    public float Timer { get; set; }

    public bool Enable { get; set; }

    public bool DisableByNextToad { get; set; }

    public BattleSpineController BattleSpineController { get; set; }

    public Vector3 LeftDirScale { get; set; } = new Vector3(0.0f, 0.0f, 0.0f);

    public Vector3 RightDirScale { get; set; } = new Vector3(0.0f, 0.0f, 0.0f);

    public UnitActionController UnitActionController { get; set; }

    public Bone StateBone { get; set; }
        public bool ReleaseByHeal { get; set; }

    }
}
