// Decompiled with JetBrains decompiler
// Type: Elements.ScaleChanger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;

namespace Elements
{
  [Serializable]
  public class ScaleChanger
  {
    public float Time;
    public float Scale = 1f;
    public float Duration;
    public CustomEasing.eType Easing = CustomEasing.eType.outQuad;
  }
}
