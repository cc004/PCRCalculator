// Decompiled with JetBrains decompiler
// Type: Elements.CoroutineData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class CoroutineData : CoroutineDataBase
  {
    public Dictionary<UnitCtrl, CoroutineDataBase> CoroutineDataDictionary = new Dictionary<UnitCtrl, CoroutineDataBase>();
    private List<UnitCtrl> blackOutUnitList;
    private List<UnitCtrl> coroutineDataRemoveKeyList = new List<UnitCtrl>();

    private List<UnitCtrl> keyList { get; set; }

    public void Init(List<UnitCtrl> _blackOutUnitList) => blackOutUnitList = _blackOutUnitList;

    public override void Update()
    {
      if (blackOutUnitList.Count != 0)
      {
        for (int index = blackOutUnitList.Count - 1; index >= 0; --index)
        {
          CoroutineDataBase coroutineDataBase = null;
          if (CoroutineDataDictionary.TryGetValue(blackOutUnitList[index], out coroutineDataBase))
            coroutineDataBase.Update();
          while (blackOutUnitList.Count <= index - 1)
            --index;
        }
      }
      else
      {
        base.Update();
        if (keyList == null)
        {
          keyList = new List<UnitCtrl>(CoroutineDataDictionary.Keys);
        }
        else
        {
          foreach (UnitCtrl key in CoroutineDataDictionary.Keys)
          {
            if (!keyList.Contains(key))
              keyList.Add(key);
          }
          for (int index = keyList.Count - 1; index >= 0; --index)
          {
            if (!CoroutineDataDictionary.ContainsKey(keyList[index]))
              keyList.RemoveAt(index);
          }
        }
        for (int index = 0; index < keyList.Count; ++index)
        {
          UnitCtrl key = keyList[index];
          if (key == null)
            coroutineDataRemoveKeyList.Add(key);
          else
            CoroutineDataDictionary[key].Update();
        }
        for (int index = coroutineDataRemoveKeyList.Count - 1; index >= 0; --index)
        {
          CoroutineDataDictionary.Remove(coroutineDataRemoveKeyList[index]);
          coroutineDataRemoveKeyList.RemoveAt(index);
        }
      }
    }
  }
}
