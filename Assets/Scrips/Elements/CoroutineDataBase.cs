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
      for (int index = this.CoroutineList.Count - 1; index > 0; --index)
      {
        this.CoroutineList[index] = (IEnumerator) null;
        this.CoroutineList.RemoveAt(index);
      }
      this.CoroutineList.Clear();
    }

    public virtual void Update()
    {
      if (this.CoroutineList.Count <= 0)
        return;
      for (int index = this.CoroutineList.Count - 1; index >= 0; --index)
      {
        if (!this.CoroutineList[index].MoveNext())
        {
          this.CoroutineList[index] = (IEnumerator) null;
          this.CoroutineList.RemoveAt(index);
        }
      }
    }
  }
}
