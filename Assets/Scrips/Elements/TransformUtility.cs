// Decompiled with JetBrains decompiler
// Type: Elements.TransformUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using UnityEngine;

namespace Elements
{
  public static class TransformUtility
  {
    public static void ResetLocal(this Transform _transform)
    {
      _transform.localPosition = Vector3.zero;
      _transform.localRotation = Quaternion.identity;
      _transform.localScale = Vector3.one;
    }

    public static void ResetLocal(
      this FixedTransformMonoBehavior.FixedTransform _transform)
    {
      _transform.localPosition = Vector3.zero;
      _transform.TargetTransform.localRotation = Quaternion.identity;
      _transform.TargetTransform.localScale = Vector3.one;
      BattleManager.Instance.shouldUpdateSkillTarget = true;
    }
  }
}
