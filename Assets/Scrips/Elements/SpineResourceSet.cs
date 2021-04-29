// Decompiled with JetBrains decompiler
// Type: Elements.SpineResourceSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Spine.Unity;
using UnityEngine;

namespace Elements
{
  public class SpineResourceSet
  {
    public SkeletonDataAsset Skelton;
    public TextAsset Animation;
    public GameObject Controller;

    public SpineResourceInfo SpineResourceInfo { get; private set; }

    //public long ResourceIndex { get; private set; }

    public int SkinId { get; private set; }

    public int UnitId { get; private set; }

    public int TrgIdx0 { get; private set; }

    public int TrgIdx1 { get; private set; }

    public SpineResourceSet(
      SpineResourceInfo _spineResourceInfo,
      int _skinId,
      int _trgIdx0,
      int _trgIdx1)
    {
      this.SpineResourceInfo = _spineResourceInfo;
      this.SkinId = _skinId;
      this.UnitId = UnitUtility.SkinIdToUnitId(_skinId);
      this.TrgIdx0 = _trgIdx0;
      this.TrgIdx1 = _trgIdx1;
      //this.ResourceIndex = ResourceManager.CreateIndex((long) _trgIdx0, (long) _trgIdx1);
    }
  }
}
