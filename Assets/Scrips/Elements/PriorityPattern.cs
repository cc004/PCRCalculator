// Decompiled with JetBrains decompiler
// Type: Elements.PriorityPattern
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public enum PriorityPattern
  {
    NONE = 1,
    RANDOM = 2,
    NEAR = 3,
    FAR = 4,
    HP_ASC = 5,
    HP_DEC = 6,
    OWNER = 7,
    RANDOM_ONCE = 8,
    FORWARD = 9,
    BACK = 10, // 0x0000000A
    ABSOLUTE_POSITION = 11, // 0x0000000B
    ENERGY_DEC = 12, // 0x0000000C
    ENERGY_ASC = 13, // 0x0000000D
    ATK_DEC = 14, // 0x0000000E
    ATK_ASC = 15, // 0x0000000F
    MAGIC_STR_DEC = 16, // 0x00000010
    MAGIC_STR_ASC = 17, // 0x00000011
    SUMMON = 18, // 0x00000012
    ENERGY_REDUCING = 19, // 0x00000013
    ATK_PHYSICS = 20, // 0x00000014
    ATK_MAGIC = 21, // 0x00000015
    ALL_SUMMON_RANDOM = 22, // 0x00000016
    OWN_SUMMON_RANDOM = 23, // 0x00000017
    BOSS = 24, // 0x00000018
    HP_ASC_NEAR = 25, // 0x00000019
    HP_DEC_NEAR = 26, // 0x0000001A
    ENERGY_DEC_NEAR = 27, // 0x0000001B
    ENERGY_ASC_NEAR = 28, // 0x0000001C
    ATK_DEC_NEAR = 29, // 0x0000001D
    ATK_ASC_NEAR = 30, // 0x0000001E
    MAGIC_STR_DEC_NEAR = 31, // 0x0000001F
    MAGIC_STR_ASC_NEAR = 32, // 0x00000020
    SHADOW = 33, // 0x00000021
    NEAR_MY_TEAM_WITHOUT_OWNER = 34, // 0x00000022
    HP_VALUE_DEC_NEAR_FORWARD = 35, // 0x00000023
    HP_VALUE_ASC_NEAR_FORWARD = 36, // 0x00000024
    ENERGY_DEC_NEAR_MAX = 37, // 0x00000025
  }
}
