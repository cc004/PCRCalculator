// Decompiled with JetBrains decompiler
// Type: Elements.CoroutineManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class CoroutineManager : MonoBehaviour
  {
    private Dictionary<ePauseType, CoroutineData> coroutineDataDictionary = new Dictionary<ePauseType, CoroutineData>((IEqualityComparer<ePauseType>) new ePauseType_DictComparer())
    {
      {
        ePauseType.VISUAL,
        new CoroutineData()
      },
      {
        ePauseType.SYSTEM,
        new CoroutineData()
      },
      {
        ePauseType.IGNORE_BLACK_OUT,
        new CoroutineData()
      },
      {
        ePauseType.NO_DIALOG,
        new CoroutineData()
      }
    };

    public bool SystemPause { get; set; }

    public bool VisualPause { get; set; }

    public void Init(List<UnitCtrl> _blackOutUnitList)
    {
      Dictionary<ePauseType, CoroutineData>.Enumerator enumerator = this.coroutineDataDictionary.GetEnumerator();
      while (enumerator.MoveNext())
        enumerator.Current.Value.Init(_blackOutUnitList);
    }

    public void AppendCoroutine(IEnumerator cr, ePauseType pauseType, UnitCtrl unit = null)
    {
      if ((UnityEngine.Object) unit == (UnityEngine.Object) null)
      {
        this.coroutineDataDictionary[pauseType].CoroutineList.Add(cr);
      }
      else
      {
        if (!this.coroutineDataDictionary[pauseType].CoroutineDataDictionary.ContainsKey(unit))
          this.coroutineDataDictionary[pauseType].CoroutineDataDictionary.Add(unit, new CoroutineDataBase());
        this.coroutineDataDictionary[pauseType].CoroutineDataDictionary[unit].CoroutineList.Add(cr);
      }
    }

    public void RemoveCoroutine(UnitCtrl _unit)
    {
      bool isRemoved = false;
      this.coroutineDataDictionary.ForEachValue<ePauseType, CoroutineData>((Action<CoroutineData>) (_typeDic =>
      {
        if (!_typeDic.CoroutineDataDictionary.Remove(_unit))
          return;
        isRemoved = true;
      }));
      int num = isRemoved ? 1 : 0;
    }

    private void OnDestroy()
    {
      foreach (KeyValuePair<ePauseType, CoroutineData> coroutineData in this.coroutineDataDictionary)
      {
        coroutineData.Value.Destroy();
        coroutineData.Value.CoroutineDataDictionary.Clear();
      }
    }
        int lastFrame = 0;
    public void _Update()
    {
      if (!this.SystemPause)
      {
        this.coroutineDataDictionary[ePauseType.SYSTEM].Update();
                /*if (lastFrame == BattleHeaderController.CurrentFrameCount)
                {
                    Debug.LogError("同帧重复计算！" + lastFrame);
                }
                lastFrame = BattleHeaderController.CurrentFrameCount;*/
        List<IEnumerator> coroutineList = this.coroutineDataDictionary[ePauseType.IGNORE_BLACK_OUT].CoroutineList;
        for (int index = coroutineList.Count - 1; index >= 0; --index)
        {
          if (!coroutineList[index].MoveNext())
            coroutineList.RemoveAt(index);
        }
      }
      if (this.VisualPause)
        return;
      this.coroutineDataDictionary[ePauseType.VISUAL].Update();
    }
        /// <summary>
        /// 每个渲染帧由gameManager调用多次（对齐逻辑帧），一直持续不受暂停影响
        /// </summary>
    public void NoDialogUpdate()
    {
      List<IEnumerator> coroutineList = this.coroutineDataDictionary[ePauseType.NO_DIALOG].CoroutineList;
      for (int index = coroutineList.Count - 1; index >= 0; --index)
      {
        if (!coroutineList[index].MoveNext())
          coroutineList.RemoveAt(index);
      }
    }
  }
}
