using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace PCRCaculator.Battle
{
    public enum BuffParamKind
    {
        [Description("攻击")]
        ATK = 1,
        [Description("防御")]
        DEF = 2,
        [Description("魔法攻击")]
        MAGIC_STR = 3,
        [Description("魔法防御")]
        MAGIC_DEF = 4,
        [Description("闪避")]
        DODGE = 5,
        [Description("物理暴击")]
        PHYSICAL_CRITICAL = 6,
        [Description("魔法暴击")]
        MAGIC_CRITICAL = 7,
        [Description("TP回复率")]
        ENERGY_RECOVER_RATE = 8,
        [Description("生命偷取")]
        LIFE_STEAL = 9,
        [Description("移动速度")]
        MOVE_SPEED = 10,
        [Description("暴击伤害")]
        PHYSICAL_CRITICAL_DAMAGE_RATE = 11,
        [Description("魔法暴击伤害")]
        MAGIC_CRITICAL_DAMAGE_RATE = 12,
        
        [Description("NONE")]
        NONE = 13
    }
    public enum DirectionType 
    {
        [Description("前方")]
        FRONT = 1,
        [Description("前方和后方")]
        FRONT_AND_BACK = 2,
        [Description("所有")]
        ALL = 3,
        [Description("前方包含飞行")]
        FRONT_INCLUDE_FLIGHT = 4,
        [Description("前方和后方包含飞行")]
        FRONT_AND_BACK_INCLUDE_FLIGHT = 5,
        [Description("所有包含飞行")]
        ALL_INCLUDE_FLIGHT = 6,
        [Description("前方不包含召唤物")]
        FRONT_WITHOUT_SUMMON = 7,
        [Description("前方和后方不包含召唤物")]
        FRONT_AND_BACK_WITHOUT_SUMMON = 8,
        [Description("所有不包含召唤物")]
        ALL_WITHOUT_SUMMON = 9,
        [Description("未知")]
        UNKNOWN = 0 }
    public enum eAbnormalState
    {
        [Description("空状态")]
        NONE = 0,
        [Description("抵挡物理伤害")]
        GUARD_ATK = 1,
        [Description("抵挡魔法伤害")]
        GUARG_MGC = 2,
        [Description("吸收物理伤害")]
        DRAIN_ATK = 3,
        [Description("吸收魔法伤害")]
        DRAIN_MGC = 4,
        [Description("抵挡伤害")]
        GUANG_BOTH = 5,
        [Description("吸收伤害")]
        DRAIN_BOTH = 6,
        [Description("加速")]
        HASTE = 7,
        [Description("中毒")]
        POISON = 8,
        [Description("燃烧")]
        BURN = 9,
        [Description("诅咒")]
        CURSE = 10,
        [Description("减速")]
        SLOW = 11,
        [Description("麻痹")]
        PARALYSIS = 12,
        [Description("冰冻")]
        FREEZE = 13,
        [Description("侵占")]
        CONVERT = 14,
        [Description("黑暗")]
        DARK = 15,
        [Description("沉默")]
        SILENCE = 16,
        [Description("束缚")]
        CHAINED = 17,
        [Description("睡眠")]
        SLEEP = 18,
        [Description("击晕")]
        STUN = 19,
        [Description("扣留")]
        DETAIN = 20,
        [Description("无特效DOT伤害")]
        NO_EFFECT_SLIP_DAMAGE = 21,
        [Description("无敌状态")]
        NO_DAMAGE_MOTION = 22,
        [Description("无异常状态")]
        NO_ABNORMAL = 23,
        [Description("无DEBUFF")]
        NO_DEBUFF = 24,
        [Description("连续攻击附近")]
        CONTINUOUS_ATTACK_NEARBY = 25,
        [Description("受伤增加")]
        ACCUMULATIVE_DAMAGE = 26,
        [Description("嘲讽")]
        DECOY = 27,
        [Description("MIFUYU")]
        MIFUYU = 28,
        [Description("石化")]
        STONE = 29,
        [Description("再生")]
        REGENERATION = 30,
        [Description("物理闪避")]
        PHYSICS_DODGE = 31,
        [Description("混乱")]
        CONFUSION = 32,
        [Description("毒液")]
        VENOM = 33,
        [Description("次数黑暗")]
        COUNT_BLIND = 34,
        [Description("减疗")]
        INHIBIT_HEAL = 35,
        [Description("恐惧")]
        FEAR = 36,
        [Description("TP回复")]
        TP_REGENERATION = 37,
        [Description("妖法")]
        HEX = 38,
        [Description("虚弱")]
        FAINT = 39,
        [Description("伤害无效")]
        PARTS_NO_DAMAGE = 40,
        [Description("补偿")]
        COMPENSATION = 41,
        [Description("减少物理伤害")]
        CUT_ATK_DAMAGE = 42,
        [Description("减少魔法伤害")]
        CUT_MGC_DAMAGE = 43,
        [Description("减少伤害")]
        CUT_ALL_DAMAGE = 44,
        [Description("物理对数盾")]
        LOG_ATK_BARRIR = 45,
        [Description("魔法对数盾")]
        LOG_MGC_BARRIR = 46,
        [Description("对数盾")]
        LOG_ALL_BARRIR = 47,
        //NUM = 48,
        //TOP = 1,
        //END = 47
    }
    public enum eAbnormalStateCategory
    {

        NONE = 0,

        DAMAGE_RESISTANCE_MGK = 1,

        DAMAGE_RESISTANCE_ATK = 2,

        DAMAGE_RESISTANCE_BOTH = 3,

        POISON = 4,

        BURN = 5,

        CURSE = 6,

        SPEED = 7,

        FREEZE = 8,

        PARALYSIS = 9,

        STUN = 10,

        DETAIN = 11,

        CONVERT = 12,

        SILENCE = 13,

        DARK = 14,

        MIFUYU = 15,

        DECOY = 16,

        NO_DAMAGE = 17,

        NO_ABNORMAL = 18,

        NO_DEBUF = 19,

        SLEEP = 20,

        CHAINED = 21,

        CONTINUOUS_ATTACK_NEARBY = 22,

        ACCUMULATIVE_DAMAGE = 23,

        NO_EFFECT_SLIP_DAMAGE = 24,

        STONE = 25,

        REGENERATION = 26,

        PHYSICS_DODGE = 27,

        CONFUSION = 28,

        VENOM = 29,

        COUNT_BLIND = 30,

        INHIBIT_HEAL = 31,

        FEAR = 32,

        TP_REGENERATION = 33,

        HEX = 34,

        FAINT = 35,

        PARTS_NO_DAMAGE = 36,

        COMPENSATION = 37,

        CUT_ATK_DAMAGE = 38,

        CUT_MGC_DAMAGE = 39,

        CUT_ALL_DAMAGE = 40,

        LOG_ATK_BARRIR = 41,

        LOG_MGC_BARRIR = 42,

        LOG_ALL_BARRIR = 43,

        NUM = 44,

        TOP = 0,

        END = 43
    }
    public enum eAccumulativeDamageType { FIXED = 1, PERCENTAGE = 2 }
    public enum eActionState
    {
        IDLE = 0,
        ATK = 1,
        SKILL1 = 2,
        SKILL = 3,
        WALK = 4,
        DAMAGE = 5,
        DIE = 6,
        GAMESTART = 7
    }
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
        IF_FOR_AL = 28, 
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
        STEALT = 54, //忽略
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
        [Description("被动技能")]
        PASSIVE = 90,//被动
        PASSIVE_INTERMITTENT = 91,//忽略，没有角色有这个技能
        UNKNOWN = 92//台服代码还没有的未知技能，有两个工会战boss有
    }
    public enum eAttacktype { PHYSICAL = 1, MAGIC = 2, INEVITABLE_PHYSICAL = 3 }
    public enum eConsumeResult
    {
        FAILED = 0,
        SKILL_OK = 1,
        SKILL_CHARGE = 2,
        SKILL_RELEASE = 3,
        INVALID_VALUE = -1
    }
    public enum eComboInterporationType
    {
        LINEAR = 0,
        CURVE = 1
    }
    public enum eDamageEffectType { NORMAL = 0, COMBO = 1, LARGE = 2 }
    public enum eDamageType { ATK = 1, MGC = 2, NONE = 3 }
    public enum eEffectType { COMMON = 0, NONE = 1 }
    public enum eEffectBehavior
    {
        NORMAL = 0,
        FIREARM = 1,
        WORLD_POS_CENTER = 2,
        TARGET_CENTER = 3,
        SKILL_AREA_RANDOM = 4,
        SERIES = 5,
        SERIES_FIREARM = 6
    }
    public enum eEffectTarget
    {
        OWNER = 0,
        ALL_TARGET = 1,
        FORWARD_TARGET = 2,
        BACK_TARGET = 3,
        ALL_OTHER = 4,
        ALL_UNIT_EXCEPT_OWNER = 5
    }
    public enum eFieldType
    {
        ATTACK = 0,
        HEAL = 1,
        INVALID_VALUE = -1
    }
    public enum eFieldExecType
    {
        NORMAL = 0,
        REPEAT = 1,
        INVALID_VALUE = -1
    }
    public enum eFieldTargetType
    {
        PLAYER = 0,
        ENEMY = 1,
        INVALID_VALUE = -1
    }
    public enum eTargetBone
    {
        BOTTOM = 0,
        HEAD = 1,
        CENTER = 2,
        FIXED_CENETER = 3,
        ANY_BONE = 4
    }
    public enum eTrackType
    {
        BONE = 0,
        BOTTOM = 1,
        NONE = 2
    }
    public enum eTrackDimension
    {
        XY = 0,
        X = 1,
        Y = 2,
        NONE = 3
    }
    public enum eInhibitHealType
    {
        PHYSICS = 0,
        MAGIC = 1,
        NO_EFFECT = 2
    }
    public enum eMissLogType
    {
        DODGE_ATTACK = 0,
        DODGE_ATTACK_DARK = 1,
        DODGE_CHANGE_SPEED = 2,
        DODGE_CHARM = 3,
        DODGE_DARK = 4,
        DODGE_SLIP_DAMAGE = 5,
        DODGE_SILENCE = 6,
        DODGE_BY_NO_DAMAGE_MOTION = 7,
        MISS_HP0_RECOVER = 8,
        DODGE_KNOCK = 9,
        INVALID_VALUE = -1
    }
    public enum eSetEnergyType
    {
        [Description("剧情添加")]
        BY_STORY_TIME_LINE,
        [Description("行动前恢复")]
        BY_ATK,
        [Description("技能")]
        BY_CHARGE_SKILL,
        [Description("战斗模式改变")]
        BY_MODE_CHANGE,
        [Description("初始化")]
        INITIALIZE,
        [Description("受伤")]
        BY_SET_DAMAGE,
        [Description("使用UB")]
        BY_USE_SKILL,
        [Description("战斗结束回复")]
        BATTLE_RECOVERY,
        [Description("击杀单位")]
        KILL_BONUS,
        [Description("调用改变能量函数")]
        BY_CHANGE_ENERGY
    }
    public enum eSkillMotionType { DEFAULT = 0, AWAKE = 1, ATTACK = 2, NONE = 3, EVOLUTION = 4 }
    public enum eStateIconType
    {
        [Description("0")]
        NONE = 0,
        [Description("1")]
        BUFF_PHYSICAL_ATK = 1,//物攻BUFF
        [Description("2")]
        BUFF_PHYSICAL_DEF = 2,//物防BUFF
        [Description("3")]
        BUFF_MAGIC_ATK = 3,//魔攻BUFF
        [Description("4")]
        BUFF_MAGIC_DEF = 4,//魔防BUFF
        [Description("5")]
        BUFF_DODGE = 5,//闪避BUFF
        [Description("6")]
        BUFF_CRITICAL = 6,//暴击BUFF
        [Description("7")]
        BUFF_ENERGY_RECOVERY = 7,//TP上升BUFF
        [Description("8")]
        BUFF_HP_RECOVERY = 8,//HP回复BUFF
        [Description("9")]
        HASTE = 9,///加速BUFF
        [Description("10")]
        NO_DAMAGE = 10,//无敌状态
        [Description("11")]
        BUFF_LIFE_STEAL = 11,//生命吸收
        [Description("12")]
        BUFF_ADD_LIFE_STEAL = 12,//生命吸收+1次\n妹法/松鼠
        [Description("13")]
        DEBUFF_PHYSICAL_ATK = 13,//物攻DEBUFF
        [Description("14")]
        DEBUFF_PHYSICAL_DEF = 14,//物防DEBUFF
        [Description("15")]
        DEBUFF_MAGIC_ATK = 15,//魔攻DEBUFF
        [Description("16")]
        DEBUFF_MAGIC_DEF = 16,//魔防DEBUFF
        [Description("17")]
        DEBUFF_DODGE = 17,//闪避DEBUFF
        [Description("18")]
        DEBUFF_CRITICAL = 18,///暴击DEBUFF
        [Description("19")]
        DEBUFF_ENERGY_RECOVERY = 19,//TP上升DEBUFF
        [Description("20")]
        DEBUFF_HP_RECOVERY = 20,//HP回复DEBUFF
        [Description("21")]
        DEBUFF_LIFE_STEAL = 21,//生命吸收DEBUFF
        [Description("22")]
        SLOW = 22,//减速DEBUFF
        [Description("23")]
        UB_DISABLE = 23,//禁用UB
        [Description("24")]
        PHYSICS_BARRIAR = 24,//物理屏障
        [Description("25")]
        MAGIC_BARRIAR = 25,//魔法屏障
        [Description("26")]
        PHYSICAS_DRAIN_BARRIAR = 26,//吸收物理伤害屏障
        [Description("27")]
        MAGIC_DRAIN_BARRIAR = 27,//吸收魔法伤害屏障
        [Description("28")]
        BOTH_BARRIAR = 28,//物理/魔法伤害无效化屏障
        [Description("29")]
        BOTH_DRAIN_BARRIAR = 29,//吸收物理/魔法伤害屏障
        [Description("30")]//???
        DEBUF_BARRIAR = 30,//不明，无用
        [Description("31")]
        STRIKE_BACK = 31,//反射伤害\n露/圣电
        [Description("32")]
        PARALISYS = 32,//麻痹
        [Description("33")]
        SLIP_DAMAGE = 33,//中毒
        [Description("34")]
        DARK = 34,//致盲
        [Description("35")]
        SILENCE = 35,//沉默
        [Description("36")]
        CONVER = 36,//魅惑
        [Description("37")]
        DECOY = 37,//挑衅
        [Description("38")]
        BURN = 38,//烧伤
        [Description("39")]
        CURSE = 39,//诅咒
        [Description("40")]
        FREEZE = 40,//冻结
        [Description("41")]
        CHAINED = 41,//束缚
        [Description("42")]
        SLEEP = 42,//睡眠
        [Description("43")]
        STUN = 43,//眩晕
        [Description("44")]
        STONE = 44,//石化
        [Description("45")]
        DETAIN = 45,//拘留
        [Description("46")]
        REGENERATION = 46, //持续生命回复
        [Description("47")]
        DEBUFF_MOVE_SPEED = 47,//移动速度DEBUFF
        [Description("48")]
        PHYSICS_DODGE = 48,//克总物理闪避
        [Description("49")]
        CONFUSION = 49, //混乱
        [Description("50")]
        HEROIC_SPIRIT_SEAL = 50,//安的英灵的加护
        [Description("51")]
        VENOM = 51, //猛毒状态
        [Description("52")]
        COUNT_BLIND = 52,//拉姆闪避次数
        [Description("53")]
        INHIBIT_HEAL = 53,//回复生命无效并造成伤害
        [Description("54")]
        FEAR = 54,//恐慌
        // ERROR = 55,
        // [Description("56")]
        // SOUL_EAT = 56,//奄美魔物吸魂
        [Description("57")]
        CHLOE = 57,//华哥畏缩
        // [Description("58")]
        // FIRE_NUTS = 58,//类龙生物火焰果
        [Description("59")]
        AWE = 59,//水瑚麟畏惧
        [Description("60")]
        LUNA = 60,//露娜朋友
        [Description("61")]
        CHRISTINA = 61,//圣克硬币
        [Description("62")]
        TP_REGENERATION = 62, //持续回复TP
        // [Description("63")]
        // CHEATING_STAR = 63,//外遇星异心
        // [Description("64")]
        // TONAKAI = 64,//变身驯鹿
        [Description("65")]
        HEX = 65,//大眼赌咒状态
        [Description("66")]
        FAINT = 66,//昏迷（类似击晕）
        [Description("67")]
        BUFF_PHYSICAL_CRITICAL_DAMAGE = 67,//物理爆伤BUFF
        [Description("68")]
        DEBUFF_PHYSICAL_CRITICAL_DAMAGE = 68,//物理爆伤DEBUFF
        [Description("69")]
        BUFF_MAGIC_CRITICAL_DAMAGE = 69,//魔法爆伤BUFF
        [Description("70")]
        DEBUFF_MAGIC_CRITICAL_DAMAGE = 70,//魔法爆伤DEBUFF
        [Description("71")]
        COMPENSATION = 71,
        [Description("72")]
        KNIGHT_GUARD = 72,//公吃骑士的加护
        [Description("73")]
        CUT_ATK_DAMAGE = 73,//减轻物理伤害
        [Description("74")]
        CUT_MGC_DAMAGE = 74,//减轻魔法伤害
        [Description("75")]
        CUT_ALL_DAMAGE = 75,//减轻所有伤害
        [Description("76")]
        CHIERU = 76,//切噜
        [Description("77")]
        REI = 77,//剑圣风之刃
        [Description("78")]
        LOG_ATK_BARRIER = 78,//物理对数盾
        [Description("79")]
        LOG_MGC_BARRIER = 79,//魔法对数盾
        [Description("80")]
        LOG_ALL_BARRIER = 80,//对数盾
        [Description("81")]
        PAUSE_ACTION = 81, //路人兔行动时间停止
        [Description("83")]
        BUFF_ACCURACY = 83, //命中BUFF
        [Description("84")]
        DEBUFF_ACCURACY = 84, // 命中DEBUFF
        // BOSS_BUFF = 85, // 0x00000055
        [Description("86")]
        UB_SILENCE = 86, // UB沉默
        // [Description("87")]
        // CUPID = 87, //变身天使
        [Description("88")]
        DEBUFF_MAX_HP = 88, // 最大生命值DEBUFF
        [Description("89")]
        // MAGIC_DARK = 89, // 0x00000059
        // [Description("90")]
        MATSURI = 90, // 瓜虎标记
        // [Description("91")]
        // HEAL_DOWN = 91, // 0x0000005B
        [Description("92")]
        AKINO_CHRISTMAS = 92, // 圣哈圣夜的光辉
        // NPC_STUN = 93, // 鱼叉大叔NPC击晕
        [Description("94")]
        BUFF_RECEIVE_CRITICAL_DAMAGE = 94,//爆伤抗性BUFF
        [Description("95")]
        DEBUFF_RECEIVE_CRITICAL_DAMAGE = 95,//爆伤抗性DEBUFF
        [Description("96")]
        DECREASE_HEAL = 96,//瓜七降低受到的回复量
        [Description("97")]
        SHEFI = 97,//雪菲冰龙之印
        [Description("98")]
        SCHOOL_FESTIVAL_YUNI = 98,//学优标记
        // [Description("99")]
        // SCHOOL_FESTIVAL_CHLOE = 99,
        [Description("100")]
        POISON_BY_BEHAVIOUR = 100,//工菜行动中毒
        [Description("101")]
        ADDITIONAL_BUFF_PHYSICAL_DEF = 101,//固定物理白甲
        // [Description("102")]
        // CRYSTALIZE = 102,//愤怒军团的兰法结晶化
        [Description("103")]
        DAMAGE_LIMIT = 103,//伤害限制
        [Description("104")]
        ADDITIONAL_BUFF_MAGIC_DEF = 104,//固定魔法白甲
        // MAGIC_CHARACTER_OF_WISDOM = 105,
        [Description("106")]
        MAGIC_CHARACTER_OF_POWER = 106,//绿魔标记
        [Description("107")]
        DETECT_WEAKNESS = 107,//龙妈龙之眼
        [Description("108")]
        DEBUFF_RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT = 108,//百分比提升其受到的伤害
        [Description("109")]
        DEBUFF_RECEIVE_PHYSICAL_DAMAGE_PERCENT = 109,//百分比提升其受到的物理伤害
        [Description("110")]
        DEBUFF_RECEIVE_MAGIC_DAMAGE_PERCENT = 110,//百分比提升其受到的魔法伤害
        [Description("111")]
        LABYRISTA_OVERLOAD = 111,//超晶标记
        [Description("112")]
        SWORD_SEAL = 112,//春流剑之刻印
        [Description("113")]
        PHANTOMCORE_WEDGE = 113,//海忍灵锚刻印
        [Description("114")]
        SPY = 114,//盗妹盗锤隐秘状态
        [Description("115")]
        HAPPY_MOMENT = 115,//野骑幸福一刻
        [Description("116")]
        SEA_GOD_PROTECTION = 116,//水怜水刃加护标记
        [Description("117")]
        BLUE_MAGIC_SEAL = 117,//scw蝶舞烙印标记
        [Description("118")]
        SHEEP = 118,//屁狐软绵绵羊毛标记
        [Description("119")]
        TWILIGHT_GUARD = 119,//伊莉雅永夜加护
        [Description("120")]
        PSYCHIC_POWER = 120,//忍的灵力
        // [Description("121")]
        // CELESTIAL_BODIES = 121,//月之光印
        [Description("122")]
        KAISER_INSIGHT_CARVED_SEAL = 122,//霸瞳标记
        // [Description("123")]
        // LIKE = 123,//狂乱的主播·镜的点赞
        [Description("124")]
        ENERGY_DAMAGE_REDUCE = 124,//减轻TP减少效果
        [Description("125")]
        SAGITTARIUS_CARVED_SEAL = 125,//611标记
        [Description("126")]
        ANNE_AND_GLARE_CARVED_SEAL = 126,//龙安友情魔印标记
        [Description("127")]
        MITSUKI_NY_CARVED_SEAL = 127,//春月朦胧状态
        [Description("128")]
        BLACK_FRAME = 128,
        [Description("129")]
        IMMUNE_STATE = 129,//611免疫行动不能状态
        [Description("130")]
        MUIMI_ANNIVERSARY_CARVED_SEAL = 130,//513情谊的证明标记
        [Description("131")]
        MISORA_CARVED_SEAL = 131,//美空标记
        [Description("132")]
        FLIGHT = 132,//飞行状态
        [Description("133")]
        DJEETA_WITCH = 133,//法鸡万象印记
        [Description("134")]
        LIXUE = 134,//礼雪美貌标记
        [Description("135")]
        JINIANG = 135,//机娘电池
        [Description("137")]
        WORLD_LIGHTNING = 137,//莱莱界雷
        [Description("139")]
        WOLF = 139,//狼牙咆哮
        [Description("140")]
        HUODIAN = 140,//火电标记
        [Description("141")]
        DUOTIANSHI = 141,//可璃亚堕天使的加护
        [Description("142")]
        ZAISHENG = 142,//ELS再生（不确定）
        [Description("143")]
        SHESHOUZUO = 143,//射手座D5标记
        [Description("145")]
        SHENGCHUI = 145,//圣锤噗吉靠垫
        [Description("150")]
        LIMIT_ENERGY_RECOVER_RATE = 150,//限制TP回复
        [Description("152")]
        EWAIJIASU = 152,//额外加速
        [Description("153")]
        EWAIJIANSU = 153,//额外减速
        [Description("154")]
        SLIP_DAMAGE_UP = 154,//毒伤提升

        // 分割线,属性没实装应该用不到

        [Description("156")]
        SHUIYOUNI = 156,//水优妮水祭的安宁
        [Description("157")]
        SHUIYUE = 157,//水华水月
        [Description("158")]
        WANJUZHIHUA = 158,//银莲婉拒之花
        [Description("159")]
        KABANJUN = 159,//库露露卡班君
        [Description("160")]
        NIANCHOU = 160,//水涅娅粘稠
        [Description("161")]
        TIEQISHOUHU = 161,//涅妃铁气守护
        [Description("162")]
        FANGMOCHEN = 162,//涅妃防魔尘
        [Description("168")]
        SOUHUI = 168,//六凶·魔蜘蒐輝
        [Description("171")]
        PSIFENGBAO = 171,//水美空PSI风暴
        [Description("173")]
        RISHI = 173,//水龙妈日食
        [Description("175")]
        JUEBING = 175,//公菲绝冰
        [Description("177")]
        GUIHUA = 177,//水蕾姆鬼化
        [Description("178")]
        KUANGSAO = 178,//水蕾姆狂骚
        [Description("179")]
        BUFF_PHYSICAL_DAMAGE = 179,//增加造成的物理伤害
        [Description("180")]
        BUFF_MAGIC_DAMAGE = 180,//增加造成的魔法伤害
        [Description("181")]
        DEBUFF_PHYSICAL_DAMAGE = 181,//减少造成的物理伤害
        [Description("182")]
        DEBUFF_MAGIC_DAMAGE = 182,//减少造成的魔法伤害
        [Description("183")]
        XIEMU = 183,//梦狐谢幕
        [Description("184")]
        MAGIC_DODGE = 184,//魔法攻击无效
        [Description("187")]
        RUOHUABUDING = 187,//潜入阿斯特朗BOSS弱化补丁
        [Description("188")]
        EMT = 188,//emt热情的花束
        [Description("189")]
        CHAHU = 189,//女仆茶壶
        [Description("190")]
        LIANJIN = 190,//炼望炼金素材
        [Description("191")]
        XIANGSHUI = 191,//炼望香水
        [Description("192")]
        TOULAN = 192,//龙妹偷懒的诱惑
        [Description("193")]
        YOUJUNHUDUN = 193,//吹雪友军护盾
        [Description("194")]
        TOUZI = 194,//大和蛮贼骰子
        [Description("195")]
        JIANCIDUNPAI = 195,//若菜尖刺盾牌
        [Description("196")]
        EMOQINWEN = 196,//圣老师恶魔的亲吻
        [Description("197")]
        SHUIQIELU = 197,//水切噜
        [Description("198")]
        WANGLINGQUTI = 198,//格蕾斯亡灵躯体
        [Description("199")]
        HUONIAO = 199,//春电火鸟
        [Description("200")]
        MOYAN = 200,//美杜莎魔眼
        [Description("201")]
        XUSHU = 201,//原克虚数
        [Description("0")]
        EX_PASSIVE_1 = 999,
        [Description("0")]
        NUM = 1000,
        [Description("0")]
        SPEED_OVERLAP,
        [Description("0")]
        INVALID_VALUE = -1,

    }
    public enum eSummonType { NONE = 0, SUMMON = 1, PHANTOM = 2, DIVISION = 1001 }
    public enum eTargetAssignment 
    { 
        [Description("敌方")]
        OTHER_SIDE = 1,
        [Description("己方")]
        OWNER_SIDE = 2,
        [Description("所有人")]
        ALL = 3 }
    public enum eWeaponSeType
    {
        DEAULT = 0, KNUCKLE = 1, SHORTSWORD = 2, AX = 3, SWORD = 4, LONGSWORD = 5, SPEAR = 6, WAND = 7, ARROW = 8, DAGGER = 9,
        LONGSWORD_2 = 10, SCRATCH_1 = 11, SCRATCH_2 = 12, HAMMER = 13, BITE = 14, COMMON_SMALL = 15,
        COMMON_LARGE = 16, EXPLOSION = 17, WAND_KIMONO = 18, SWORD_KIMONO = 19, NO_WAND_WITCH = 20,
        WAND_2 = 21, NO_WANO_WITCH_2 = 22
    }
    public enum eWeaponMotionType
    {
        DEFAULT = 0, 
        KNUCKLE = 1, 
        SHORTSWORD = 2, 
        AX = 3, 
        SWORD = 4, 
        LONGSWORD = 5, 
        SPEAR = 6,
        WAND = 7, 
        ARROW = 8, 
        DAGGER = 9, 
        LONGSWORD_2 = 10, 
        WAND_KIMONO = 21, 
        SWOAD_KIMONO = 22,
        NO_WAND_WITCH = 23, 
        WAND_2 = 25, 
        NO_WAND_WITCH_2 = 26
    }
    public enum PirorityPattern
    {
        [Description("距离升序")]
        NONE = 1,
        [Description("随机")]
        RANDOM = 2,
        [Description("距离升序")]
        NEAR = 3,
        [Description("距离降序")]
        FAR = 4,
        [Description("HP升序")]
        HP_ASC = 5,
        [Description("HP降序")]
        HP_DEC = 6,
        [Description("自己")]
        OWNER = 7,
        [Description("随机一次")]
        RANDOM_ONCE = 8,
        [Description("前方距离升序")]
        FORWARD = 9,
        [Description("后方距离升序")]
        BACK = 10,
        [Description("领域类")]//如深月技能
        ABSOLUTE_POSITION = 11,
        [Description("TP降序")]
        ENERGY_DEC = 12,
        [Description("TP升序")]//如黄骑
        ENERGY_ASC = 13,
        [Description("攻击降序")]
        ATK_DEC = 14,
        [Description("攻击升序")]//没有实例
        ATK_ASC = 15,
        [Description("魔攻降序")]
        MAGIC_STR_DEC = 16,
        [Description("魔攻升序")]//没有实例
        MAGIC_STR_ASC = 17,
        [Description("召唤物")]
        SUMMON = 18,
        [Description("TP特殊排列")]//没有实例
        ENERGY_REDUCING = 19,
        [Description("物理攻击者距离升序")]
        ATK_PHYSICS = 20,
        [Description("魔法攻击者距离升序")]
        ATK_MAGIC = 21,
        [Description("所有召唤物随机")]//没有实例
        ALL_SUMMON_RANDOM = 22,
        [Description("己方召唤物随机")]//没有实例
        OWN_SUMMON_RANDOM = 23,
        [Description("BOSS")]
        BOSS = 24,
        [Description("HP升序，同HP距离近优先")]//如艾米利亚
        HP_ASC_NEAR = 25,
        [Description("HP降序，同HP距离近优先")]//没有实例
        HP_DEC_NEAR = 26,
        [Description("TP降序，同TP距离近优先")]//没有实例
        ENERGY_DEC_NEAR = 27,
        [Description("TP升序，同TP距离近优先")]//没有实例
        ENERGY_ASC_NEAR = 28,
        [Description("攻击降序，同攻击距离近优先")]//没有实例
        ATK_DEC_NEAR = 29,
        [Description("攻击升序，同攻击距离近优先")]//没有实例
        ATK_ASC_NEAR = 30,
        [Description("魔攻降序，同魔攻距离近优先")]//如圣诞小仓唯
        MAGIC_STR_DEC_NEAR = 31,
        [Description("魔攻升序，同魔攻距离近优先")]//没有实例
        MAGIC_STR_ASC_NEAR = 32,
        [Description("SHADOW")]//？？？
        SHADOW = 33
    }
    public static class BattleDefine
    {
        public static float MAX_ENERGY = 1000;
        public static Dictionary<eWeaponMotionType, float> WEAPON_HIT_DELAY_DIC;
        public static readonly Dictionary<eWeaponMotionType, float> WEAPON_EFFECT_DELAY_DIC;
        static BattleDefine()
        {
            WEAPON_HIT_DELAY_DIC = new Dictionary<eWeaponMotionType, float>
            {
                { (eWeaponMotionType)8, 0 },
                { (eWeaponMotionType)3, 0.45f },
                { (eWeaponMotionType)1, 0.4f },
                { (eWeaponMotionType)5, 0.47f },
                { (eWeaponMotionType)2, 0.4f },
                { (eWeaponMotionType)6, 0.35f },
                { (eWeaponMotionType)4, 0.35f },
                { (eWeaponMotionType)7, 0 },
                { (eWeaponMotionType)9, 0 },
                { (eWeaponMotionType)10, 0.6f },
                { (eWeaponMotionType)22, 0.4f },
                { (eWeaponMotionType)21, 0 },
                { (eWeaponMotionType)23, 0 },
                { (eWeaponMotionType)25, 0 },
                { (eWeaponMotionType)26, 0 }
            };
            WEAPON_EFFECT_DELAY_DIC = new Dictionary<eWeaponMotionType, float>
            {
                {(eWeaponMotionType)8,0.9f },
                {(eWeaponMotionType)7,0.3f },
                {(eWeaponMotionType)9,0.3f },

            };

        }
    }
    public static class BattleUtil
    {
        public static int FloatToIntReverseTruncate(float num)
        {
            if(Mathf.Abs((int)num - num) <= 0.001)
            {
                return (int)num;
            }
            if (num < 0)
            {
                return Mathf.FloorToInt(num);
            }

            return Mathf.CeilToInt(num);
        }
        /// <summary>
        /// 返回等级差的概率补正
        /// </summary>
        /// <param name="level">己方等级</param>
        /// <param name="targetlevel">对方等级</param>
        /// <returns>0-100整数</returns>
        public static int GetDodgeByLevelDiff(int level,int targetlevel)
        {
            return 100 - Mathf.Max(0, targetlevel - level);
        }
    }
}
