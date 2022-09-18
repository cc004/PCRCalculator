// Decompiled with JetBrains decompiler
// Type: Elements.CoroutineDataBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;

namespace Elements
{
  public class CoroutineDataBase
  {
    public List<IEnumerator> CoroutineList = new List<IEnumerator>();

    public void Destroy()
    {
      for (int index = CoroutineList.Count - 1; index > 0; --index)
      {
        CoroutineList[index] = null;
        CoroutineList.RemoveAt(index);
      }
      CoroutineList.Clear();
    }

    public virtual void Update()
    { 
      if (CoroutineList.Count <= 0)
        return;
      for (int index = CoroutineList.Count - 1; index >= 0; --index)
      {
        if (!CoroutineList[index].MoveNext())
        {
          CoroutineList[index] = null;
          CoroutineList.RemoveAt(index);
        }
      }
    }
  }
}
