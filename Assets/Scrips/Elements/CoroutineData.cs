// Decompiled with JetBrains decompiler
// Type: Elements.CoroutineData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class CoroutineData : CoroutineDataBase
  {
    public Dictionary<UnitCtrl, CoroutineDataBase> CoroutineDataDictionary = new Dictionary<UnitCtrl, CoroutineDataBase>();
    private List<UnitCtrl> blackOutUnitList;
    private List<UnitCtrl> coroutineDataRemoveKeyList = new List<UnitCtrl>();

    private List<UnitCtrl> keyList { get; set; }

    public void Init(List<UnitCtrl> _blackOutUnitList) => this.blackOutUnitList = _blackOutUnitList;

    public override void Update()
    {
      if (this.blackOutUnitList.Count != 0)
      {
        for (int index = this.blackOutUnitList.Count - 1; index >= 0; --index)
        {
          CoroutineDataBase coroutineDataBase = (CoroutineDataBase) null;
          if (this.CoroutineDataDictionary.TryGetValue(this.blackOutUnitList[index], out coroutineDataBase))
            coroutineDataBase.Update();
          while (this.blackOutUnitList.Count <= index - 1)
            --index;
        }
      }
      else
      {
        base.Update();
        if (this.keyList == null)
        {
          this.keyList = new List<UnitCtrl>((IEnumerable<UnitCtrl>) this.CoroutineDataDictionary.Keys);
        }
        else
        {
          foreach (UnitCtrl key in this.CoroutineDataDictionary.Keys)
          {
            if (!this.keyList.Contains(key))
              this.keyList.Add(key);
          }
          for (int index = this.keyList.Count - 1; index >= 0; --index)
          {
            if (!this.CoroutineDataDictionary.ContainsKey(this.keyList[index]))
              this.keyList.RemoveAt(index);
          }
        }
        for (int index = 0; index < this.keyList.Count; ++index)
        {
          UnitCtrl key = this.keyList[index];
          if ((Object) key == (Object) null)
            this.coroutineDataRemoveKeyList.Add(key);
          else
            this.CoroutineDataDictionary[key].Update();
        }
        for (int index = this.coroutineDataRemoveKeyList.Count - 1; index >= 0; --index)
        {
          this.CoroutineDataDictionary.Remove(this.coroutineDataRemoveKeyList[index]);
          this.coroutineDataRemoveKeyList.RemoveAt(index);
        }
      }
    }
  }
}
