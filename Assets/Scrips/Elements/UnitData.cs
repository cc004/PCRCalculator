// Decompiled with JetBrains decompiler
// Type: Elements.SpineConvert.UnitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements.SpineConvert
{
  public class UnitData
  {
    public int UnitId { get; private set; }

    public string UnitName { get; private set; }

    public int MotionType { get; private set; }

    public UnitData(string[] _csvParam)
    {
      UnitId = int.Parse(_csvParam[0]);
      UnitName = _csvParam[1];
      MotionType = int.Parse(_csvParam[6]);
    }
  }
}
