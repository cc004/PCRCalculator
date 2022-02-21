// Decompiled with JetBrains decompiler
// Type: Elements.eDamageEffectType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.ComponentModel;

namespace Elements
{
    public enum eDamageEffectType
    {
        INVALID_VALUE = -1, // 0xFFFFFFFF
        [Description("普通")]
        NORMAL = 0,
        [Description("连击")]
        COMBO = 1,
        [Description("巨大")]
        LARGE = 2,
    }
}
