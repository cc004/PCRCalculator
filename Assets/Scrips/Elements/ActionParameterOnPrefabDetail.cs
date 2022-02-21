// Decompiled with JetBrains decompiler
// Type: Elements.ActionParameterOnPrefabDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Elements
{
    [Serializable]
    public class ActionParameterOnPrefabDetail
    {
        public bool Visible;
        public List<ActionExecTime> ExecTimeForPrefab = new List<ActionExecTime>();
        public List<ActionExecTimeCombo> ExecTimeCombo = new List<ActionExecTimeCombo>();
        public int ActionId;
        public List<NormalSkillEffect> ActionEffectList = new List<NormalSkillEffect>();
        public List<ActionExecTime> ExecTime { set; get; }
    }
}
