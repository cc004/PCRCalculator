// Decompiled with JetBrains decompiler
// Type: Elements.ActionExecTimeCombo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Elements
{
    [Serializable]
    public class ActionExecTimeCombo
    {
        public float StartTime;
        public float OffsetTime;
        public float Weight = 1f;
        public int Count;
        public eComboInterporationType InterporationType;
        [JsonIgnore]
        public AnimationCurve Curve = new AnimationCurve();
    }
}
