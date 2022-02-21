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
        [Description("暴击")]
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
        SLIENCE = 16,
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
        [Description("40")]
        NONE = 0,
        [Description("0")]
        BUFF_PHYSICAL_ATK = 1,
        [Description("2")]
        BUFF_PHYSICAL_DEF = 2,
        [Description("1")]
        BUFF_MAGIC_ATK = 3,
        [Description("3")]
        BUFF_MAGIC_DEF = 4,
        [Description("8")]
        BUFF_DODGE = 5,
        [Description("4")]
        BUFF_CRITICAL = 6,
        [Description("7")]
        BUFF_ENERGY_RECOVERY = 7,
        [Description("6")]
        BUFF_HP_RECOVERY = 8,
        [Description("9")]
        HASTE = 9,
        [Description("29")]
        NO_DAMAGE = 10,
        [Description("5")]
        BUFF_LIFE_STEAL = 11,
        [Description("5")]
        BUFF_ADD_LIFE_STEAL = 12,
        [Description("10")]
        DEBUFF_PHYSICAL_ATK = 13,
        [Description("12")]
        DEBUFF_PHYSICAL_DEF = 14,
        [Description("11")]
        DEBUFF_MAGIC_ATK = 15,
        [Description("13")]
        DEBUFF_MAGIC_DEF = 16,
        [Description("18")]
        DEBUFF_DODGE = 17,
        [Description("14")]
        DEBUFF_CRITICAL = 18,
        [Description("17")]
        DEBUFF_ENERGY_RECOVERY = 19,
        [Description("16")]
        DEBUFF_HP_RECOVERY = 20,
        [Description("15")]
        DEBUFF_LIFE_STEAL = 21,
        [Description("19")]
        SLOW = 22,
        [Description("26")]
        UB_DISABLE = 23,
        [Description("35")]
        PHYSICS_BARRIAR = 24,
        [Description("36")]
        MAGIC_BARRIAR = 25,
        [Description("35")]
        PHYSICAS_DRAIN_BARRIAR = 26,
        [Description("36")]
        MAGIC_DRAIN_BARRIAR = 27,
        [Description("35")]
        BOTH_BARRIAR = 28,
        [Description("37")]
        BOTH_DRAIN_BARRIAR = 29,
        [Description("40")]//???
        DEBUF_BARRIAR = 30,
        [Description("33")]
        STRIKE_BACK = 31,
        [Description("20")]
        PARALISYS = 32,
        [Description("29")]
        SLIP_DAMAGE = 33,
        [Description("25")]
        DARK = 34,
        [Description("26")]
        SILENCE = 35,
        [Description("40")]//???
        CONVER = 36,
        [Description("27")]
        DECOY = 37,
        [Description("30")]
        BURN = 38,
        [Description("31")]
        CURSE = 39,
        [Description("23")]
        FREEZE = 40,
        [Description("22")]
        CHAINED = 41,
        [Description("24")]
        SLEEP = 42,
        [Description("21")]
        STUN = 43,
        [Description("21")]
        STONE = 44,
        [Description("22")]
        DETAIN = 45,
        [Description("40")]
        REGENERATION = 46,
        [Description("40")]
        DEBUFF_MOVE_SPEED = 47,
        [Description("35")]
        PHYSICS_DODGE = 48,
        [Description("28")]
        CONFUSION = 49, 
        HEROIC_SPIRIT_SEAL = 50, 
        VENOM = 51, 
        COUNT_BLIND = 52, 
        INHIBIT_HEAL = 53,
        [Description("45")]
        FEAR = 54,
        ERROR = 55, 
        SOUL_EAT = 56, 
        KUROE = 57, 
        FIRE_NUTS = 58,
        AWE = 59,
        [Description("46")]
        LUNA = 60,
        CHRISTINA = 61,
        [Description("44")]
        TP_REGENERATION = 62,
        SAGITTARIUS = 63, 
        TONAKAI = 64, 
        HEX = 65, 
        FAINT = 66,
        [Description("41")]
        BUFF_PHYSICAL_CRITICAL_DAMAGE = 67,
        [Description("42")]
        DEBUFF_PHYSICAL_CRITICAL_DAMAGE = 68,
        [Description("41")]
        BUFF_MAGIC_CRITICAL_DAMAGE = 69,
        [Description("42")]
        DEBUFF_MAGIC_CRITICAL_DAMAGE = 70,
        COMPENSATION = 71, 
        KNIGHT_GUARD = 72, 
        CUT_ATK_DAMAGE = 73,
        CUT_MGC_DAMAGE = 74,
        CUT_ALL_DAMAGE = 75, 
        CHIERU = 76, 
        REI = 77,
        [Description("43")]
        LOG_ATK_BARRIER = 78,
        [Description("43")]
        LOG_MGC_BARRIER = 79,
        [Description("43")]
        LOG_ALL_BARRIER = 80,
        PAUSE_ACTION = 81, // 0x00000051
        BUFF_ACCURACY = 83, // 0x00000053
        DEBUFF_ACCURACY = 84, // 0x00000054
        RUKA = 85, // 0x00000055
        UB_SILENCE = 86, // 0x00000056
        CUPID = 87, // 0x00000057
        DEBUFF_MAX_HP = 88, // 0x00000058
        MAGIC_DARK = 89, // 0x00000059
        MATSURI = 90, // 0x0000005A
        HEAL_DOWN = 91, // 0x0000005B
        [Description("40")]
        AKINO_CHRISTMAS = 92, // 0x0000005C
        NPC_STUN = 93, // 0x0000005D
        NUM = 94, // 0x0000005E
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
