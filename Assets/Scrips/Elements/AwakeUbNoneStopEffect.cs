// Decompiled with JetBrains decompiler
// Type: Elements.AwakeUbNoneStopEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using UnityEngine;

namespace Elements
{
  //[RequireComponent(typeof (CriAtomSource))]
  [ExecuteAlways]
  public class AwakeUbNoneStopEffect : SkillEffectCtrl
  {
    [SerializeField]
    private AwakeUbNoneStopEffect.eDisplayType displayType;
    [SerializeField]
    private float startHpPercentage;

    public override void SetTimeToDieByStartHp(float _hpPercent)
    {
      if ((double) _hpPercent > (double) this.startHpPercentage)
      {
        if (this.displayType != AwakeUbNoneStopEffect.eDisplayType.DISPLAY_LOW)
          return;
        this.SetTimeToDie(true);
      }
      else
      {
        if (this.displayType != AwakeUbNoneStopEffect.eDisplayType.DISPLAY_HIGH)
          return;
        this.SetTimeToDie(true);
      }
    }

    public enum eDisplayType
    {
      ALWAYS,
      DISPLAY_HIGH,
      DISPLAY_LOW,
    }
  }
}
