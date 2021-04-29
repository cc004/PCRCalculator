// Decompiled with JetBrains decompiler
// Type: Elements.CutInFrameData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public class CutInFrameData
  {
    public const int DEFAULT_FRAME = -1;
    public const int CONNECTING_FRAME = -2;
    public const int ERROR_FRAME = -3;
    public int CutInFrame = -1;
    public int ServerCutInFrame = -1;
    public bool SupportSkillUsed;
    public bool CatchSuccess;
  }
}
