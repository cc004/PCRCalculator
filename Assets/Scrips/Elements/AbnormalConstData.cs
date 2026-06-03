// Decompiled with JetBrains decompiler
// Type: Elements.AbnormalConstData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using static Elements.UnitCtrl;

namespace Elements
{
  public class AbnormalConstData
  {
    public bool IsBuff { get; set; }

    public eStateIconType IconType { get; set; }
  }

  public static class AbnormalStateDefine // TypeDefIndex: 20121
  {
    // Fields
    public const float SLOW_ANIM_SPEED = 0.5f;
    public const float HASTE_ANIM_SPEED = 2;
    public const float NORMAL_ANIM_SPEED = 1;
    public const float SLIP_DAMAGE_INTERVAL_TIME = 1;
    public const float SLIP_DAMAGE_INTERVAL_EXTEND_DEFAULT = 1;
    public static readonly Dictionary<eAbnormalState, AbnormalConstData> ABNORMAL_CONST_DATA; // TODO
    public static readonly Dictionary<BuffParamKind, bool> IS_EFFECT_BOSS_BODY_ONLY = new Dictionary<BuffParamKind, bool>()
    {
        { BuffParamKind.ATK, false },
        { BuffParamKind.DEF, false },
        { BuffParamKind.MAGIC_STR, false },
        { BuffParamKind.MAGIC_DEF, false },
        { BuffParamKind.DODGE, false },
        { BuffParamKind.PHYSICAL_CRITICAL, false },
        { BuffParamKind.MAGIC_CRITICAL, false },
        { BuffParamKind.ENERGY_RECOVER_RATE, true },
        { BuffParamKind.LIFE_STEAL, false },
        { BuffParamKind.MOVE_SPEED, true },
        { BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE, true },
        { BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE, true },
        { BuffParamKind.ACCURACY, false },
        { BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE, true },
        { BuffParamKind.RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT, true },
        { BuffParamKind.RECEIVE_PHYSICAL_DAMAGE_PERCENT, true },
        { BuffParamKind.RECEIVE_MAGIC_DAMAGE_PERCENT, true },
        { BuffParamKind.PHYSICAL_DAMAGE_UP_PERCENT, true },
        { BuffParamKind.MAGIC_DAMAGE_UP_PERCENT, true },
        { BuffParamKind.MAX_HP, true },
        { BuffParamKind.BUFF_ENHANCE, true },
        { BuffParamKind.DEBUFF_ENHANCE, true }
    };

  }

}
