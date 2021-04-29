// Decompiled with JetBrains decompiler
// Type: Elements.ActionParameterOnPrefab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  [Serializable]
  public class ActionParameterOnPrefab
  {
    public bool Visible;
    public eActionType ActionType = eActionType.ATTACK;
    public List<ActionParameterOnPrefabDetail> Details = new List<ActionParameterOnPrefabDetail>();
    public AnimationCurve KnockAnimationCurve = new AnimationCurve();
    public AnimationCurve KnockDownAnimationCurve = new AnimationCurve();
    public eEffectType EffectType;
  }
}
