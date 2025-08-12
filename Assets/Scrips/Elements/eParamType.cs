// Decompiled with JetBrains decompiler
// Type: Elements.eParamType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public enum eParamType
  {
    INVALID_VALUE = -1, // 0xFFFFFFFF
    NONE = 0,
    HP = 1,
    ATK = 2,
    DEF = 3,
    MAGIC_ATK = 4,
    MAGIC_DEF = 5,
    PHYSICAL_CRITICAL = 6,
    MAGIC_CRITICAL = 7,
    DODGE = 8,
    LIFE_STEAL = 9,
    WAVE_HP_RECOVERY = 10, // 0x0000000A
    WAVE_ENERGY_RECOVERY = 11, // 0x0000000B
    PHYSICAL_PENETRATE = 12, // 0x0000000C
    MAGIC_PENETRATE = 13, // 0x0000000D
    ENERGY_RECOVERY_RATE = 14, // 0x0000000E
    HP_RECOVERY_RATE = 15, // 0x0000000F
    ENERGY_REDUCE_RATE = 16, // 0x00000010
    ACCURACY = 17, // 0x00000011
    BASE_PARAMETER_NUM = 18,
    PHYSICAL_CRITICAL_DAMAGE_RATE = 100,
    MAGIC_CRITICAL_DAMAGE_RATE = 101,
    PHYSICAL_DAMAGE_UP_PERCENT = 102,
    MAGIC_DAMAGE_UP_PERCENT = 103,
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
