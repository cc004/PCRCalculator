// Decompiled with JetBrains decompiler
// Type: Elements.eParamType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll
using System.ComponentModel;

namespace Elements
{
  public enum eParamType
  {
    INVALID_VALUE = -1, // 0xFFFFFFFF
    NONE = 0,
    [Description("HP")]
    HP = 1,
    [Description("物攻")]
    ATK = 2,
    [Description("物防")]
    DEF = 3,
    [Description("魔攻")]
    MAGIC_ATK = 4,
    [Description("魔防")]
    MAGIC_DEF = 5,
    [Description("物暴")]
    PHYSICAL_CRITICAL = 6,
    [Description("魔暴")]
    MAGIC_CRITICAL = 7,
    [Description("闪避")]
    DODGE = 8,
    [Description("生命吸收")]
    LIFE_STEAL = 9,
    [Description("HP自动回复")]
    WAVE_HP_RECOVERY = 10, // 0x0000000A
    [Description("TP自动回复")]
    WAVE_ENERGY_RECOVERY = 11, // 0x0000000B
    [Description("物理贯通伤害")]
    PHYSICAL_PENETRATE = 12, // 0x0000000C
    [Description("魔法贯通伤害")]
    MAGIC_PENETRATE = 13, // 0x0000000D
    [Description("TP上升")]
    ENERGY_RECOVERY_RATE = 14, // 0x0000000E
    [Description("回复量上升")]
    HP_RECOVERY_RATE = 15, // 0x0000000F
    [Description("TP减轻")] 
    ENERGY_REDUCE_RATE = 16, // 0x00000010
    [Description("命中")]
    ACCURACY = 17, // 0x00000011
    BASE_PARAMETER_NUM = 18,
    [Description("物暴伤害提升")]
    PHYSICAL_CRITICAL_DAMAGE_RATE = 100,
    [Description("魔暴伤害提升")]
    MAGIC_CRITICAL_DAMAGE_RATE = 101,
    [Description("物理伤害提升")]
    PHYSICAL_DAMAGE_UP_PERCENT = 102,
    [Description("魔法伤害提升")]
    MAGIC_DAMAGE_UP_PERCENT = 103,
    [Description("属性加成")]
    TALENT_BONUS = 104,
    ABNORMAL_STATE_DODGE_HIT = 105,
    ABNORMAL_STATE_DODGE_RESIST = 106,
    ATTACK_ENHANCE = 107,
    BUFF_ENHANCE = 108,
    DEBUFF_ENHANCE = 109,
    HEAL_ENHANCE = 110,
    ENERGY_CHARGE = 111,
  }
}
