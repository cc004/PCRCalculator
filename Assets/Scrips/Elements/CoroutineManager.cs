// Decompiled with JetBrains decompiler
// Type: Elements.CoroutineManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
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

    int lastFrame = 0;
    public void _Update()
    {
      if (!SystemPause)
      {
        coroutineDataDictionary[(int)ePauseType.SYSTEM].Update();
                /*if (lastFrame == BattleHeaderController.CurrentFrameCount)
                {
                    Debug.LogError("同帧重复计算！" + lastFrame);
                }
                lastFrame = BattleHeaderController.CurrentFrameCount;*/
        List<IEnumerator> coroutineList = coroutineDataDictionary[(int)ePauseType.IGNORE_BLACK_OUT].CoroutineList;
        for (int index = coroutineList.Count - 1; index >= 0; --index)
        {
          if (!coroutineList[index].MoveNext())
            coroutineList.RemoveAt(index);
        }
      }
      if (VisualPause)
        return;
      coroutineDataDictionary[(int)ePauseType.VISUAL].Update();
    }
        /// <summary>
        /// 每个渲染帧由gameManager调用多次（对齐逻辑帧），一直持续不受暂停影响
        /// </summary>
    public void NoDialogUpdate()
    {
      List<IEnumerator> coroutineList = coroutineDataDictionary[(int)ePauseType.NO_DIALOG].CoroutineList;
      for (int index = coroutineList.Count - 1; index >= 0; --index)
      {
        if (!coroutineList[index].MoveNext())
          coroutineList.RemoveAt(index);
      }
    }
  }
}
