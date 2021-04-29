// Decompiled with JetBrains decompiler
// Type: Elements.eBattleLogType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public enum eBattleLogType
  {
    INVALID_VALUE = -1, // 0xFFFFFFFF
    MISS = 1,
    SET_DAMAGE = 2,
    SET_ABNORMAL = 3,
    SET_RECOVERY = 4,
    SET_BUFF_DEBUFF = 5,
    SET_STATE = 6,
    BUTTON_TAP = 7,
    SET_ENERGY = 8,
    DAMAGE_CHARGE = 9,
    GIVE_VALUE_ADDITIONAL = 10, // 0x0000000A
    GIVE_VALUE_MULTIPLY = 11, // 0x0000000B
    WAVE_END_HP = 12, // 0x0000000C
    WAVE_END_DAMAGE_AMOUNT = 13, // 0x0000000D
    BREAK = 14, // 0x0000000E
  }
}
