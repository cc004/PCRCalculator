// Decompiled with JetBrains decompiler
// Type: Elements.SpineResourceInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public class SpineResourceInfo
  {
    public eSpineType spineType { get; private set; }

    public eBundleId bundleId { get; private set; }

    public eResourceId skelton { get; private set; }

    public eResourceId controller { get; private set; }

    public eResourceId animation { get; private set; }

    public float animationScale { get; private set; }

    public bool IsSkinId { get; private set; }

    public SpineResourceInfo(
      eSpineType _spineType,
      eBundleId _bundleId,
      eResourceId _skelton,
      eResourceId _controller,
      eResourceId _animation = eResourceId.NONE,
      float _animationScale = 1f,
      bool _isSkinId = false)
    {
      spineType = _spineType;
      bundleId = _bundleId;
      skelton = _skelton;
      controller = _controller;
      animation = _animation;
      animationScale = _animationScale;
      IsSkinId = _isSkinId;
    }
  }
}
