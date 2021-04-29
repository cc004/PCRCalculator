// Decompiled with JetBrains decompiler
// Type: Elements.ePauseType_DictComparer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class ePauseType_DictComparer : IEqualityComparer<ePauseType>
  {
    public bool Equals(ePauseType _x, ePauseType _y) => _x == _y;

    public int GetHashCode(ePauseType _obj) => (int) _obj;
  }
}
