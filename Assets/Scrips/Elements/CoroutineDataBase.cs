// Decompiled with JetBrains decompiler
// Type: Elements.CoroutineDataBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Elements
{
#if ENABLE_THREADING
  internal struct CoroutineDiagnosticsCounters
  {
    public int TopLevelCoroutineCount;
    public int UnitBoundBucketCount;
    public int MoveNextCount;
    public int CompletedCount;

    public void Add(CoroutineDiagnosticsCounters other)
    {
      TopLevelCoroutineCount += other.TopLevelCoroutineCount;
      UnitBoundBucketCount += other.UnitBoundBucketCount;
      MoveNextCount += other.MoveNextCount;
      CompletedCount += other.CompletedCount;
    }
  }
#endif

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update()
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

#if ENABLE_THREADING
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal CoroutineDiagnosticsCounters UpdateWithDiagnostics()
    {
      var counters = new CoroutineDiagnosticsCounters
      {
        TopLevelCoroutineCount = CoroutineList.Count
      };

      if (CoroutineList.Count <= 0)
        return counters;

      for (int index = CoroutineList.Count - 1; index >= 0; --index)
      {
        counters.MoveNextCount++;
        if (!CoroutineList[index].MoveNext())
        {
          counters.CompletedCount++;
          CoroutineList[index] = null;
          CoroutineList.RemoveAt(index);
        }
      }

      return counters;
    }
#endif
  }
}
