// Decompiled with JetBrains decompiler
// Type: Elements.eSetEnergyType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.ComponentModel;

namespace Elements
{
    public enum eSetEnergyType
    {
        INVALID_VALUE = -1, // 0xFFFFFFFF
        [Description("剧情")]
        BY_STORY_TIME_LINE = 0,
        [Description("技能开始")]
        BY_ATK = 1,
        [Description("技能开始2")]
        BY_CHARGE_SKILL = 2,
        [Description("改变行动模式")]
        BY_MODE_CHANGE = 3,
        [Description("初始化")]
        INITIALIZE = 4,
        [Description("受伤")]
        BY_SET_DAMAGE = 5,
        [Description("放UB")]
        BY_USE_SKILL = 6,
        [Description("战斗结束回复")]
        BATTLE_RECOVERY = 7,
        [Description("击杀单位")]
        KILL_BONUS = 8,
        [Description("充能")]
        BY_CHANGE_ENERGY = 9,
    }
}
