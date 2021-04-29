// Decompiled with JetBrains decompiler
// Type: Elements.PassiveSealData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using System.Collections;

namespace Elements
{
  public class PassiveSealData
  {
    public float LifeTime { get; set; }

    public UnitCtrl Source { get; set; }

    public float SealDuration { get; set; }

    public eStateIconType TargetStateIcon { get; set; }

    public UnitCtrl Target { get; set; }

    public bool DisplayCount { get; set; } = true;

    public PassiveSealAction.eSealTarget SealTarget { get; set; }

    public int SealNumLimit { get; set; }

    public IEnumerator Update()
    {
      while (true)
      {
        this.LifeTime -= this.Target.DeltaTimeForPause;
        if ((double) this.LifeTime >= 0.0)
          yield return (object) null;
        else
          break;
      }
    }
  }
}
