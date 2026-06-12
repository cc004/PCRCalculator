// Decompiled with JetBrains decompiler
// Type: Elements.CoroutineManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Elements
{
#if ENABLE_THREADING
  public struct CoroutineManagerDiagnosticsSnapshot
  {
    public long SystemTicks { get; set; }
    public long VisualTicks { get; set; }
    public long IgnoreBlackOutTicks { get; set; }
    public long NoDialogTicks { get; set; }

    public int SystemTopLevelMax { get; set; }
    public int VisualTopLevelMax { get; set; }
    public int IgnoreBlackOutTopLevelMax { get; set; }
    public int NoDialogTopLevelMax { get; set; }

    public int SystemUnitBucketMax { get; set; }
    public int VisualUnitBucketMax { get; set; }
    public int IgnoreBlackOutUnitBucketMax { get; set; }
    public int NoDialogUnitBucketMax { get; set; }

    public int SystemMoveNextCount { get; set; }
    public int VisualMoveNextCount { get; set; }
    public int IgnoreBlackOutMoveNextCount { get; set; }
    public int NoDialogMoveNextCount { get; set; }

    public int SystemCompletedCount { get; set; }
    public int VisualCompletedCount { get; set; }
    public int IgnoreBlackOutCompletedCount { get; set; }
    public int NoDialogCompletedCount { get; set; }

    public bool Recorded =>
      SystemTicks > 0 ||
      VisualTicks > 0 ||
      IgnoreBlackOutTicks > 0 ||
      NoDialogTicks > 0 ||
      SystemMoveNextCount > 0 ||
      VisualMoveNextCount > 0 ||
      IgnoreBlackOutMoveNextCount > 0 ||
      NoDialogMoveNextCount > 0 ||
      SystemCompletedCount > 0 ||
      VisualCompletedCount > 0 ||
      IgnoreBlackOutCompletedCount > 0 ||
      NoDialogCompletedCount > 0;
  }
#endif

  public class CoroutineManager : MonoBehaviour
  {
    private CoroutineData[] coroutineDataDictionary = new[]
    {
        new CoroutineData(),

        new CoroutineData(),

        new CoroutineData(),

        new CoroutineData()
    };

    public bool SystemPause { get; set; }

    public bool VisualPause { get; set; }

#if ENABLE_THREADING
    private readonly long[] diagnosticTicks = new long[4];
    private readonly int[] diagnosticTopLevelMax = new int[4];
    private readonly int[] diagnosticUnitBucketMax = new int[4];
    private readonly int[] diagnosticMoveNextCount = new int[4];
    private readonly int[] diagnosticCompletedCount = new int[4];
#endif

    public void Init(List<UnitCtrl> _blackOutUnitList)
    {
        coroutineDataDictionary[0].Init(_blackOutUnitList);
        coroutineDataDictionary[1].Init(_blackOutUnitList);
        coroutineDataDictionary[2].Init(_blackOutUnitList);
        coroutineDataDictionary[3].Init(_blackOutUnitList);
    }

    public void AppendCoroutine(IEnumerator cr, ePauseType pauseType, UnitCtrl unit = null)
    {
      var data = coroutineDataDictionary[(int)pauseType];
      if (unit == null)
      {
        data.CoroutineList.Add(cr);
      }
      else
      {
        if (!data.CoroutineDataDictionary.ContainsKey(unit))
          data.CoroutineDataDictionary.Add(unit, new CoroutineDataBase());
        data.CoroutineDataDictionary[unit].CoroutineList.Add(cr);
      }
    }

    public void RemoveCoroutine(UnitCtrl _unit)
    {
        coroutineDataDictionary[0].CoroutineDataDictionary.Remove(_unit);
        coroutineDataDictionary[1].CoroutineDataDictionary.Remove(_unit);
        coroutineDataDictionary[2].CoroutineDataDictionary.Remove(_unit);
        coroutineDataDictionary[3].CoroutineDataDictionary.Remove(_unit);
        }
    
    public void ClearAll()
    {
        coroutineDataDictionary[0].Destroy();
        coroutineDataDictionary[1].Destroy();
        coroutineDataDictionary[2].Destroy();
        coroutineDataDictionary[3].Destroy();

        coroutineDataDictionary[0].CoroutineDataDictionary.Clear();
        coroutineDataDictionary[1].CoroutineDataDictionary.Clear();
        coroutineDataDictionary[2].CoroutineDataDictionary.Clear();
        coroutineDataDictionary[3].CoroutineDataDictionary.Clear();
    }

    private void OnDestroy()
    {
      foreach (var coroutineData in coroutineDataDictionary)
      {
        coroutineData.Destroy();
        coroutineData.CoroutineDataDictionary.Clear();
      }
    }

#if ENABLE_THREADING
    public void ResetDiagnostics()
    {
      System.Array.Clear(diagnosticTicks, 0, diagnosticTicks.Length);
      System.Array.Clear(diagnosticTopLevelMax, 0, diagnosticTopLevelMax.Length);
      System.Array.Clear(diagnosticUnitBucketMax, 0, diagnosticUnitBucketMax.Length);
      System.Array.Clear(diagnosticMoveNextCount, 0, diagnosticMoveNextCount.Length);
      System.Array.Clear(diagnosticCompletedCount, 0, diagnosticCompletedCount.Length);
    }

    public CoroutineManagerDiagnosticsSnapshot GetDiagnosticsSnapshot()
    {
      return new CoroutineManagerDiagnosticsSnapshot
      {
        SystemTicks = diagnosticTicks[(int)ePauseType.SYSTEM],
        VisualTicks = diagnosticTicks[(int)ePauseType.VISUAL],
        IgnoreBlackOutTicks = diagnosticTicks[(int)ePauseType.IGNORE_BLACK_OUT],
        NoDialogTicks = diagnosticTicks[(int)ePauseType.NO_DIALOG],
        SystemTopLevelMax = diagnosticTopLevelMax[(int)ePauseType.SYSTEM],
        VisualTopLevelMax = diagnosticTopLevelMax[(int)ePauseType.VISUAL],
        IgnoreBlackOutTopLevelMax = diagnosticTopLevelMax[(int)ePauseType.IGNORE_BLACK_OUT],
        NoDialogTopLevelMax = diagnosticTopLevelMax[(int)ePauseType.NO_DIALOG],
        SystemUnitBucketMax = diagnosticUnitBucketMax[(int)ePauseType.SYSTEM],
        VisualUnitBucketMax = diagnosticUnitBucketMax[(int)ePauseType.VISUAL],
        IgnoreBlackOutUnitBucketMax = diagnosticUnitBucketMax[(int)ePauseType.IGNORE_BLACK_OUT],
        NoDialogUnitBucketMax = diagnosticUnitBucketMax[(int)ePauseType.NO_DIALOG],
        SystemMoveNextCount = diagnosticMoveNextCount[(int)ePauseType.SYSTEM],
        VisualMoveNextCount = diagnosticMoveNextCount[(int)ePauseType.VISUAL],
        IgnoreBlackOutMoveNextCount = diagnosticMoveNextCount[(int)ePauseType.IGNORE_BLACK_OUT],
        NoDialogMoveNextCount = diagnosticMoveNextCount[(int)ePauseType.NO_DIALOG],
        SystemCompletedCount = diagnosticCompletedCount[(int)ePauseType.SYSTEM],
        VisualCompletedCount = diagnosticCompletedCount[(int)ePauseType.VISUAL],
        IgnoreBlackOutCompletedCount = diagnosticCompletedCount[(int)ePauseType.IGNORE_BLACK_OUT],
        NoDialogCompletedCount = diagnosticCompletedCount[(int)ePauseType.NO_DIALOG]
      };
    }

    private void RecordDiagnostics(ePauseType pauseType, long ticks, CoroutineDiagnosticsCounters counters)
    {
      var index = (int)pauseType;
      diagnosticTicks[index] += ticks;
      if (counters.TopLevelCoroutineCount > diagnosticTopLevelMax[index])
        diagnosticTopLevelMax[index] = counters.TopLevelCoroutineCount;
      if (counters.UnitBoundBucketCount > diagnosticUnitBucketMax[index])
        diagnosticUnitBucketMax[index] = counters.UnitBoundBucketCount;
      diagnosticMoveNextCount[index] += counters.MoveNextCount;
      diagnosticCompletedCount[index] += counters.CompletedCount;
    }

    private void UpdateCoroutineDataWithDiagnostics(ePauseType pauseType, CoroutineData coroutineData)
    {
      var startTicks = Stopwatch.GetTimestamp();
      var counters = default(CoroutineDiagnosticsCounters);
      try
      {
        counters = coroutineData.UpdateWithDiagnostics();
      }
      finally
      {
        RecordDiagnostics(pauseType, Stopwatch.GetTimestamp() - startTicks, counters);
      }
    }

    private void UpdateCoroutineListWithDiagnostics(ePauseType pauseType, List<IEnumerator> coroutineList)
    {
      var startTicks = Stopwatch.GetTimestamp();
      var counters = new CoroutineDiagnosticsCounters
      {
        TopLevelCoroutineCount = coroutineList.Count
      };
      try
      {
        for (int index = coroutineList.Count - 1; index >= 0; --index)
        {
          counters.MoveNextCount++;
          if (!coroutineList[index].MoveNext())
          {
            counters.CompletedCount++;
            coroutineList.RemoveAt(index);
          }
        }
      }
      finally
      {
        RecordDiagnostics(pauseType, Stopwatch.GetTimestamp() - startTicks, counters);
      }
    }
#endif

    private static void UpdateCoroutineList(List<IEnumerator> coroutineList)
    {
      for (int index = coroutineList.Count - 1; index >= 0; --index)
      {
        if (!coroutineList[index].MoveNext())
          coroutineList.RemoveAt(index);
      }
    }

    int lastFrame = 0;
    public void _Update()
    {
#if ENABLE_THREADING
      var collectDiagnostics = Elements.Battle.BattleManager.CollectUpdateFrameDiagnostics;
#endif
      if (!SystemPause)
      {
#if ENABLE_THREADING
        if (collectDiagnostics)
        {
          UpdateCoroutineDataWithDiagnostics(ePauseType.SYSTEM, coroutineDataDictionary[(int)ePauseType.SYSTEM]);
        }
        else
#endif
        {
          coroutineDataDictionary[(int)ePauseType.SYSTEM].Update();
        }
                /*if (lastFrame == BattleHeaderController.CurrentFrameCount)
                {
                    Debug.LogError("同帧重复计算！" + lastFrame);
                }
                lastFrame = BattleHeaderController.CurrentFrameCount;*/
        List<IEnumerator> coroutineList = coroutineDataDictionary[(int)ePauseType.IGNORE_BLACK_OUT].CoroutineList;
#if ENABLE_THREADING
        if (collectDiagnostics)
        {
          UpdateCoroutineListWithDiagnostics(ePauseType.IGNORE_BLACK_OUT, coroutineList);
        }
        else
#endif
        {
          UpdateCoroutineList(coroutineList);
        }
      }
      if (VisualPause)
        return;
#if ENABLE_THREADING
      if (collectDiagnostics)
      {
        UpdateCoroutineDataWithDiagnostics(ePauseType.VISUAL, coroutineDataDictionary[(int)ePauseType.VISUAL]);
      }
      else
#endif
      {
        coroutineDataDictionary[(int)ePauseType.VISUAL].Update();
      }
    }
        /// <summary>
        /// 每个渲染帧由gameManager调用多次（对齐逻辑帧），一直持续不受暂停影响
        /// </summary>
    public void NoDialogUpdate()
    {
      List<IEnumerator> coroutineList = coroutineDataDictionary[(int)ePauseType.NO_DIALOG].CoroutineList;
#if ENABLE_THREADING
      var collectDiagnostics = Elements.Battle.BattleManager.CollectUpdateFrameDiagnostics;
      if (collectDiagnostics)
      {
        UpdateCoroutineListWithDiagnostics(ePauseType.NO_DIALOG, coroutineList);
      }
      else
#endif
      {
        UpdateCoroutineList(coroutineList);
      }
    }
  }
}
