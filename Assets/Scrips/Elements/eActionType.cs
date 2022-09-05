// Decompiled with JetBrains decompiler
// Type: Elements.eActionType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.ComponentModel;

namespace Elements
{
    public enum eActionType
    {
        [Description("攻击")]
        ATTACK = 1,
        [Description("移动")]
        MOVE = 2,
        [Description("击飞、击退")]
        KNOCK = 3,
        [Description("治疗")]
        HEAL = 4,
        CURE = 5,//忽略 
        [Description("护盾")]
        BARRIER = 6,
        [Description("视角切换")]
        REFLEXIVE = 7,
        [Description("改变行动速度")]
        CHANGE_SPEED = 8,
        [Description("DOT伤害")]
        SLIP_DAMAGE = 9,
        [Description("BUFF/DEBUFF")]
        BUFF_DEBUFF = 10,
        [Description("魅惑")]
        CHARM = 11,
        [Description("致盲")]
        BLIND = 12,
        SILENCE = 13, //忽略
        MODE_CHANGE = 14, //我方只有muimi有，暂时忽略
        SUMMON = 15,
        [Description("改变能量")]
        CHARGE_ENERGY = 16,
        TRIGER = 17, //只有中二有
        [Description("抵挡伤害类")]
        DAMAGE_CHARGE = 18, //怜和宫子有
        CHARGE = 19, //只有一个怪有，忽略
        [Description("嘲讽")]
        DECOY = 20,//20
        [Description("闪避无敌类")]
        NO_DAMAGE = 21,
        CHANGE_PATTERN = 22, //忽略，只有怪物有
        IF_FOR_CHILDREN = 23, //选择条件分支
        REVIVAL = 24, //忽略，只有怪有
        CONTINUOUS_ATTACK = 25,//忽略，没有角色有这个技能
        [Description("加法赋值")]
        GIVE_VALUE_AS_ADDITIVE = 26,
        [Description("乘法赋值")]
        GIVE_VALUE_AS_MULTIPLE = 27,//27
        [Description("排他条件分支")]
        IF_FOR_ALL = 28,
        SEARCH_AREA_CHANGE = 29, //忽略，没有角色有这个技能
        DESTROY = 30, //只有中二有的即死
        CONTINUOUS_ATTACK_NEARBY = 31, //忽略，没有角色有这个技能
        [Description("生命偷取")]
        ENCHANT_LIFE_STEAL = 32, //只有妹法大招有这个技能
        ENCHANT_STRIKE_BACK = 33,//33
        [Description("伤害累加")]
        ACCUMULATIVE_DAMAGE = 34, //只有狗拳有
        SEAL = 35,
        ATTACK_FIELD = 36,
        HEAL_FIELD = 37,
        [Description("场地BUFF")]
        CHANGE_PARAMETER_FIELD = 38,
        ABNORMAL_STATE_FIELD = 39,//忽略，只有两个怪有这个 
        KETSUBAN = 40,//忽略，没有角色有这个技能
        UB_CHANGE_TIME = 41, //忽略，没有角色有这个技能
        LOOP_TRIGGER = 42,
        IF_HAS_TARGET = 43, //忽略，没有角色有这个技能
        [Description("出场等待")]
        WAVE_START_IDLE = 44, //只有羊驼有
        [Description("计数器")]
        SKILL_EXEC_COUNT = 45,
        [Description("百分比伤害")]
        RATIO_DAMAGE = 46, //只有圣诞伊利亚有
        UPPER_LIMIT_ATTACK = 47,//忽略
        [Description("回复")]
        REGENERATION = 48,
        BUFF_DEBUFF_CLEAR = 49, //忽略
        LOOP_MOTION_BUFF_DEBUFF = 50, //只有クルミ（クリスマス）有
        DIVISION = 51, //忽略，没有角色有这个技能
        CHANGE_BODY_WIDTH = 52, //忽略
        IF_EXISTS_FIELD_FOR_ALL = 53,//シズル（バレンタイン）专属
        STEALTH = 54, // 0x00000036
        MOVE_PARTS = 55, //忽略
        COUNT_BLIND = 56, //ラム专属
        COUNT_DOWN = 57, //ミソギ（ハロウィン）和一个怪有
        STOP_FIELD = 58, //忽略
        INHIBIT_HEAL = 59, //忽略
        ATTACK_SEAL = 60,
        FEAR = 61,//ミヤコ（ハロウィン）和黑猫有
        AWE = 62, //忽略
        LOOP_MOTION_REPEAT = 63, //忽略
        TOAD = 69, //忽略，这个属性就一个怪有，判定代码却到处都有，贼烦
        FORCE_HP_CHANGE = 70,//忽略，没有角色有这个技能
        KNGHT_GUARD = 71,//ペコリーヌ（プリンセス）专属 
        DAMAGE_CUT = 72, //忽略，没有角色有这个技能
        LOG_BARRIER = 73, //忽略，工会战boss专属的对数盾
        GIVE_VALUE_AS_DIVIDE = 74,//忽略，没有角色有这个技能
        ACTION_BY_HIT_COUNT = 75, // 0x0000004B
        HEAL_DOWN = 76,
        PASSIVE_SEAL = 77,
        PASSIVE_DAMAGE_UP = 78,

        DAMAGE_BY_ATTACK = 79,
        DAMAGE_LIMIT = 80,
        SPECIAL_IDLE = 81,
        CHANGE_RESIST_ID = 82,
        CHANGE_SPEED_OVERLAP = 83,



        [Description("被动技能")]
        PASSIVE = 90,//被动
        PASSIVE_INTERMITTENT = 91,//忽略，没有角色有这个技能
        CHANGE_ENERGY_RECOVERY_RATIO_BY_DAMAGE = 92, // 0x0000005C
        IGNORE_DECOY = 93, // 0x0000005D
        EFFECT = 94,
        SPY = 95,
        CHARGE_ENERGY_FIELD = 96,
        CHARGE_ENERGY_BY_DAMAGE = 97

    }

}
