// Decompiled with JetBrains decompiler
// Type: Elements.BattleDefine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class BattleDefine
  {
    public const string BATTLE_LOGIC_VERSION = "4";
    public const int ARENA_BG_ID = 100001;
    public const int GRANDARENA_BG_ID = 100002;
    public const float MAIN_QUEST_ZOOM_POS = 4935f;
    public const float OTHER_QUEST_ZOOM_POS = 4780f;
    public const int LOGIC_FRAME_RATE = 60;
    /*public static readonly Dictionary<eBattleCategory, eBGM[]> BattleBgmDic = new Dictionary<eBattleCategory, eBGM[]>()
    {
      {
        eBattleCategory.QUEST,
        (eBGM[]) null
      },
      {
        eBattleCategory.TRAINING,
        (eBGM[]) null
      },
      {
        eBattleCategory.DUNGEON,
        (eBGM[]) null
      },
      {
        eBattleCategory.ARENA,
        new eBGM[1]{ eBGM.ARENA_BATTLE }
      },
      {
        eBattleCategory.ARENA_REPLAY,
        new eBGM[1]{ eBGM.ARENA_BATTLE }
      },
      {
        eBattleCategory.FRIEND,
        new eBGM[1]{ eBGM.ARENA_BATTLE }
      },
      {
        eBattleCategory.CLAN_BATTLE,
        (eBGM[]) null
      },
      {
        eBattleCategory.GLOBAL_RAID,
        (eBGM[]) null
      },
      {
        eBattleCategory.STORY,
        (eBGM[]) null
      },
      {
        eBattleCategory.TUTORIAL,
        (eBGM[]) null
      },
      {
        eBattleCategory.GRAND_ARENA,
        new eBGM[3]
        {
          eBGM.GRAND_ARENA_BATTLE_1,
          eBGM.GRAND_ARENA_BATTLE_2,
          eBGM.GRAND_ARENA_BATTLE_3
        }
      },
      {
        eBattleCategory.GRAND_ARENA_REPLAY,
        new eBGM[3]
        {
          eBGM.GRAND_ARENA_BATTLE_1,
          eBGM.GRAND_ARENA_BATTLE_2,
          eBGM.GRAND_ARENA_BATTLE_3
        }
      },
      {
        eBattleCategory.HATSUNE_BATTLE,
        (eBGM[]) null
      },
      {
        eBattleCategory.HATSUNE_BOSS_BATTLE,
        (eBGM[]) null
      },
      {
        eBattleCategory.HATSUNE_SPECIAL_BATTLE,
        (eBGM[]) null
      },
      {
        eBattleCategory.UEK_BOSS_BATTLE,
        (eBGM[]) null
      },
      {
        eBattleCategory.QUEST_REPLAY,
        (eBGM[]) null
      },
      {
        eBattleCategory.TOWER,
        (eBGM[]) null
      },
      {
        eBattleCategory.TOWER_REHEARSAL,
        (eBGM[]) null
      },
      {
        eBattleCategory.TOWER_EX,
        (eBGM[]) null
      },
      {
        eBattleCategory.TOWER_REPLAY,
        (eBGM[]) null
      },
      {
        eBattleCategory.TOWER_EX_REPLAY,
        (eBGM[]) null
      },
      {
        eBattleCategory.TOWER_CLOISTER,
        (eBGM[]) null
      },
      {
        eBattleCategory.TOWER_CLOISTER_REPLAY,
        (eBGM[]) null
      },
      {
        eBattleCategory.HIGH_RARITY,
        (eBGM[]) null
      },
      {
        eBattleCategory.KAISER_BATTLE_MAIN,
        (eBGM[]) null
      },
      {
        eBattleCategory.KAISER_BATTLE_SUB,
        (eBGM[]) null
      },
      {
        eBattleCategory.SPACE_BATTLE,
        (eBGM[]) null
      }
    };*/
    public static readonly List<eUnitRespawnPos> UnitRespawnPosList = new List<eUnitRespawnPos>
    {
      eUnitRespawnPos.MAIN_POS_3,
      eUnitRespawnPos.MAIN_POS_5,
      eUnitRespawnPos.MAIN_POS_1,
      eUnitRespawnPos.MAIN_POS_4,
      eUnitRespawnPos.MAIN_POS_2
    };
    public static readonly List<eUnitRespawnPos> UnitRespawnPosTopToBottom = new List<eUnitRespawnPos>
    {
      eUnitRespawnPos.MAIN_POS_1,
      eUnitRespawnPos.MAIN_POS_2,
      eUnitRespawnPos.MAIN_POS_3,
      eUnitRespawnPos.MAIN_POS_4,
      eUnitRespawnPos.MAIN_POS_5
    };
    private static readonly List<eUnitRespawnPos> enemyRespawnPosList = new List<eUnitRespawnPos>
    {
      eUnitRespawnPos.MAIN_POS_3,
      eUnitRespawnPos.MAIN_POS_5,
      eUnitRespawnPos.MAIN_POS_1,
      eUnitRespawnPos.MAIN_POS_4,
      eUnitRespawnPos.MAIN_POS_2
    };
    public const int BACKGROUND_ORDER = -1100;
    public const int FOREGROUND_ORDER = 10900;
    public const int MIDDLEGROUND_ORDER = 680;
    public const int BACK_CHARA_ORDER = 350;
    public const int FRONT_CHARA_ORDER = 10850;
    public const int CHANGE_WAVE_FRAME_NUM = 100000;
    public const string BATTLE_PATH = "Assets/_ElementsResources/Resources/All/Battle";
    private static readonly Dictionary<eUnitRespawnPos, int> unitSortOrderDictionary = new Dictionary<eUnitRespawnPos, int>
    {
      {
        eUnitRespawnPos.MAIN_POS_1,
        1500
      },
      {
        eUnitRespawnPos.MAIN_POS_2,
        3500
      },
      {
        eUnitRespawnPos.MAIN_POS_3,
        5500
      },
      {
        eUnitRespawnPos.MAIN_POS_4,
        7500
      },
      {
        eUnitRespawnPos.MAIN_POS_5,
        9500
      },
      {
        eUnitRespawnPos.SUB_POS_1,
        2500
      },
      {
        eUnitRespawnPos.SUB_POS_2,
        4500
      },
      {
        eUnitRespawnPos.SUB_POS_3,
        6500
      },
      {
        eUnitRespawnPos.SUB_POS_4,
        8500
      },
      {
        eUnitRespawnPos.SUB_POS_5,
        10500
      }
    };
    private const int RESULT_SORT_ORDER_OFFSET = 100;
    public const int STATUS_EFFECT_SORT_ORDER_OFFSET = 350;
    public const int EFFECT_UNIT_FRONT_SORT_ORDER_OFFSET = 300;
    public const int EFFECT_UNIT_BACK_SORT_ORDER_OFFSET = -300;
    public const int EFFECT_FRONT_SORT_ORDER = 11000;
    public const int EFFECT_BACK_SORT_ORDER = 690;
    public const int UI_SORT_ORDER = 11250;
    public const int BLACK_OUT_SORT_ORDER = 11500;
    public const int EFFECT_UNIT_FRONT_SORT_ORDER_OFFSET_FLATOUT = 200;
    public const float CRITICAL_ATK_RATE = 2f;
    public const int DEFAULT_CRITICAL_DAMAGE_RATE = 100;
    public const int MIN_CRITICAL_DAMAGE_RATE = 50;
    public const float MIN_DAMAGE = 1f;
    public const float SKILL_ENERGY_MAX = 1000f;
    private const float RESPAWN_POS_TOP_Y_1 = 40f;
    private const float RESPAWN_POS_BOTTOM_Y_1 = -186f;
    private const int LANE_COUNT_MINUS_ONE = 9;
    public const float BATTLE_POS_Y = 5000f;
    public const float BATTLE_BG_SIZE = 1.15f;
    public const float BATTLE_POS_Y_OTHER_THAN_QUEST = 5140f;
    public const float BATTLE_UNIT_SPACE_X = 200f;
    public const string SKILL_FX_FILENAME_PREFIX = "fxsk_";
    public const string SKILL_FX_FILENAME_PREFIX_SAMPLE = "fxsk_0000_CUT_";
    public const float QUEST_WIN_OFFSET = 280f;
    public const float QUEST_WIN_POS_Y = -100f;
    public static readonly Dictionary<eUnitRespawnPos, float> RESPAWN_POS = new Dictionary<eUnitRespawnPos, float>
    {
      {
        eUnitRespawnPos.MAIN_POS_1,
        40f
      },
      {
        eUnitRespawnPos.MAIN_POS_2,
        -10.22222f
      },
      {
        eUnitRespawnPos.MAIN_POS_3,
        -60.44444f
      },
      {
        eUnitRespawnPos.MAIN_POS_4,
        -110.6667f
      },
      {
        eUnitRespawnPos.MAIN_POS_5,
        -160.8889f
      },
      {
        eUnitRespawnPos.SUB_POS_1,
        14.88889f
      },
      {
        eUnitRespawnPos.SUB_POS_2,
        -35.33334f
      },
      {
        eUnitRespawnPos.SUB_POS_3,
        -85.55556f
      },
      {
        eUnitRespawnPos.SUB_POS_4,
        -135.7778f
      },
      {
        eUnitRespawnPos.SUB_POS_5,
        -186f
      }
    };
    public static readonly Dictionary<eUnitRespawnPos, List<eUnitRespawnPos>> SUMMON_RESPAWN_PRIORITY = new Dictionary<eUnitRespawnPos, List<eUnitRespawnPos>>
    {
      {
        eUnitRespawnPos.MAIN_POS_1,
        new List<eUnitRespawnPos>
        {
          eUnitRespawnPos.SUB_POS_5,
          eUnitRespawnPos.MAIN_POS_5,
          eUnitRespawnPos.SUB_POS_3,
          eUnitRespawnPos.MAIN_POS_3,
          eUnitRespawnPos.SUB_POS_4,
          eUnitRespawnPos.MAIN_POS_4,
          eUnitRespawnPos.SUB_POS_2,
          eUnitRespawnPos.MAIN_POS_2,
          eUnitRespawnPos.SUB_POS_1,
          eUnitRespawnPos.MAIN_POS_1
        }
      },
      {
        eUnitRespawnPos.MAIN_POS_2,
        new List<eUnitRespawnPos>
        {
          eUnitRespawnPos.SUB_POS_5,
          eUnitRespawnPos.MAIN_POS_5,
          eUnitRespawnPos.SUB_POS_3,
          eUnitRespawnPos.MAIN_POS_3,
          eUnitRespawnPos.SUB_POS_4,
          eUnitRespawnPos.MAIN_POS_4,
          eUnitRespawnPos.SUB_POS_1,
          eUnitRespawnPos.MAIN_POS_1,
          eUnitRespawnPos.SUB_POS_2,
          eUnitRespawnPos.MAIN_POS_2
        }
      },
      {
        eUnitRespawnPos.MAIN_POS_3,
        new List<eUnitRespawnPos>
        {
          eUnitRespawnPos.SUB_POS_5,
          eUnitRespawnPos.MAIN_POS_5,
          eUnitRespawnPos.SUB_POS_1,
          eUnitRespawnPos.MAIN_POS_1,
          eUnitRespawnPos.SUB_POS_4,
          eUnitRespawnPos.MAIN_POS_4,
          eUnitRespawnPos.SUB_POS_2,
          eUnitRespawnPos.MAIN_POS_2,
          eUnitRespawnPos.SUB_POS_3,
          eUnitRespawnPos.MAIN_POS_3
        }
      },
      {
        eUnitRespawnPos.MAIN_POS_4,
        new List<eUnitRespawnPos>
        {
          eUnitRespawnPos.MAIN_POS_1,
          eUnitRespawnPos.SUB_POS_1,
          eUnitRespawnPos.MAIN_POS_3,
          eUnitRespawnPos.SUB_POS_3,
          eUnitRespawnPos.MAIN_POS_4,
          eUnitRespawnPos.SUB_POS_4,
          eUnitRespawnPos.MAIN_POS_2,
          eUnitRespawnPos.SUB_POS_2,
          eUnitRespawnPos.MAIN_POS_1,
          eUnitRespawnPos.SUB_POS_1
        }
      },
      {
        eUnitRespawnPos.MAIN_POS_5,
        new List<eUnitRespawnPos>
        {
          eUnitRespawnPos.MAIN_POS_1,
          eUnitRespawnPos.SUB_POS_1,
          eUnitRespawnPos.MAIN_POS_3,
          eUnitRespawnPos.SUB_POS_3,
          eUnitRespawnPos.MAIN_POS_2,
          eUnitRespawnPos.SUB_POS_2,
          eUnitRespawnPos.MAIN_POS_5,
          eUnitRespawnPos.SUB_POS_5,
          eUnitRespawnPos.MAIN_POS_4,
          eUnitRespawnPos.SUB_POS_4
        }
      },
      {
        eUnitRespawnPos.SUB_POS_5,
        new List<eUnitRespawnPos>
        {
          eUnitRespawnPos.MAIN_POS_1,
          eUnitRespawnPos.SUB_POS_1,
          eUnitRespawnPos.MAIN_POS_3,
          eUnitRespawnPos.SUB_POS_3,
          eUnitRespawnPos.MAIN_POS_2,
          eUnitRespawnPos.SUB_POS_2,
          eUnitRespawnPos.MAIN_POS_5,
          eUnitRespawnPos.SUB_POS_5,
          eUnitRespawnPos.MAIN_POS_4,
          eUnitRespawnPos.SUB_POS_4
        }
      }
    };
    public const float BATTLE_START_POS = 560f;
    public const float BOSS_DEFAULT_DELTA_X = -34f;
    public static readonly float BATTLE_FIELD_SIZE = 1024f;
    public const float ARENA_TIME_LIMIT = 90f;
    public const float MAX_BATTLE_TIME = 9999f;
    public const float WIN_WAIT_TIME = 2.5f;
    public const float LOSE_WAIT_TIME = 1.3f;
    public const float GRAND_ARENA_LOSE_WAIT_TIME = 3f;
    public const float TIME_UP_FAILED_WAIT_TIME = 2f;
    public const float TIME_UP_WAIT_TIME = 0.5f;
    public const float BATTLE_START_WAIT_TIME = 0.5f;
    public const float BATTLE_START_ADV_WAIT_TIME = 2f;
    public const float MY_TURN_DISP_WAIT_TIME = 3f;
    public const float MY_TURN_DISP_FADE_OUT_TIME = 0.2f;
    public const float WIN_FLATOUT_WAIT_TIME = 0.5f;
    public const float LOSE_FLATOUT_WAIT_TIME = 0.5f;
    public const float MOVE_STAGE_WIDTH = -480f;
    public const float E_TO_E_COEFFICIENT = 2.044444f;
    public const float JOY_MOTION_PAUSE_DURATION = 0.3f;
    public const int JOY_MOTION_PAUSE_START_FRAME = 3;
    public const float INVISIBLE_X = -1400f;
    public const float RUN_MOVE_RATE = 1.6f;
    /*public static readonly Dictionary<SystemIdDefine.eWeaponSeType, eSE> WEAPON_SE_DIC = new Dictionary<SystemIdDefine.eWeaponSeType, eSE>()
    {
      {
        SystemIdDefine.eWeaponSeType.ARROW,
        eSE.BTL_HIT_ARROW_1
      },
      {
        SystemIdDefine.eWeaponSeType.AX,
        eSE.BTL_HIT_AX_1
      },
      {
        SystemIdDefine.eWeaponSeType.DEFAULT,
        eSE.BTL_DAMAGE
      },
      {
        SystemIdDefine.eWeaponSeType.KNUCKLE,
        eSE.BTL_HIT_KNUCKLE_1
      },
      {
        SystemIdDefine.eWeaponSeType.LONGSWORD,
        eSE.BTL_HIT_LONGSWORD_1
      },
      {
        SystemIdDefine.eWeaponSeType.SHORTSWORD,
        eSE.BTL_HIT_SHORTSWORD_1
      },
      {
        SystemIdDefine.eWeaponSeType.SPEAR,
        eSE.BTL_HIT_SPEAR_1
      },
      {
        SystemIdDefine.eWeaponSeType.SWORD,
        eSE.BTL_HIT_SWORD_1
      },
      {
        SystemIdDefine.eWeaponSeType.WAND,
        eSE.BTL_HIT_WAND_1
      },
      {
        SystemIdDefine.eWeaponSeType.SCRATCH_1,
        eSE.BTL_HIT_SCRATCH_1
      },
      {
        SystemIdDefine.eWeaponSeType.SCRATCH_2,
        eSE.BTL_HIT_SCRATCH_2
      },
      {
        SystemIdDefine.eWeaponSeType.BITE,
        eSE.BTL_HIT_BITE_1
      },
      {
        SystemIdDefine.eWeaponSeType.COMMON_SMALL,
        eSE.BTL_HIT_COMMON_1
      },
      {
        SystemIdDefine.eWeaponSeType.COMMON_LARGE,
        eSE.BTL_HIT_COMMON_2
      },
      {
        SystemIdDefine.eWeaponSeType.EXPLOSION,
        eSE.BTL_HIT_EXPLO_1
      },
      {
        SystemIdDefine.eWeaponSeType.HAMMER,
        eSE.BTL_HIT_HAMMER_1
      },
      {
        SystemIdDefine.eWeaponSeType.DUALSWORD,
        eSE.BTL_HIT_DUALSWORD_1
      },
      {
        SystemIdDefine.eWeaponSeType.FLAIL,
        eSE.BTL_HIT_AX_1
      },
      {
        SystemIdDefine.eWeaponSeType.WAND_3,
        eSE.BTL_HIT_WAND_1
      }
    };*/
    public static readonly Dictionary<SystemIdDefine.eWeaponSeType, eResourceId> WEAPON_HIT_EFFECT_DIC = new Dictionary<SystemIdDefine.eWeaponSeType, eResourceId>
    {
      {
        SystemIdDefine.eWeaponSeType.ARROW,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.AX,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.DEFAULT,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.KNUCKLE,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.LONGSWORD,
        eResourceId.FX_HIT2
      },
      {
        SystemIdDefine.eWeaponSeType.SHORTSWORD,
        eResourceId.FX_HIT3
      },
      {
        SystemIdDefine.eWeaponSeType.SPEAR,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.SWORD,
        eResourceId.FX_HIT2
      },
      {
        SystemIdDefine.eWeaponSeType.WAND,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.SCRATCH_1,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.SCRATCH_2,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.BITE,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.COMMON_SMALL,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.COMMON_LARGE,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.EXPLOSION,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.HAMMER,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.DUALSWORD,
        eResourceId.FX_HIT4
      },
      {
        SystemIdDefine.eWeaponSeType.FLAIL,
        eResourceId.FX_HIT1_CENTER
      },
      {
        SystemIdDefine.eWeaponSeType.WAND_3,
        eResourceId.FX_HIT1_CENTER
      }
    };
    public static readonly Dictionary<eResourceId, eResourceId> WEAPON_HIT_EFFECT_DIC_L = new Dictionary<eResourceId, eResourceId>
    {
      {
        eResourceId.FX_HIT3,
        eResourceId.FX_HIT3_L
      },
      {
        eResourceId.FX_HIT1_CENTER,
        eResourceId.FX_HIT1_CENTER_L
      },
      {
        eResourceId.FX_HIT2,
        eResourceId.FX_HIT2_L
      },
      {
        eResourceId.FX_HIT4,
        eResourceId.FX_HIT4_L
      }
    };
    public static readonly Dictionary<SystemIdDefine.eWeaponMotionType, float> WEAPON_HIT_DELAY_DIC = new Dictionary<SystemIdDefine.eWeaponMotionType, float>
    {
      {
        SystemIdDefine.eWeaponMotionType.ARROW,
        0.0f
      },
      {
        SystemIdDefine.eWeaponMotionType.AX,
        0.45f
      },
      {
        SystemIdDefine.eWeaponMotionType.KNUCKLE,
        0.4f
      },
      {
        SystemIdDefine.eWeaponMotionType.LONGSWORD,
        0.47f
      },
      {
        SystemIdDefine.eWeaponMotionType.SHORTSWORD,
        0.4f
      },
      {
        SystemIdDefine.eWeaponMotionType.SPEAR,
        0.35f
      },
      {
        SystemIdDefine.eWeaponMotionType.SWORD,
        0.35f
      },
      {
        SystemIdDefine.eWeaponMotionType.WAND,
        0.0f
      },
      {
        SystemIdDefine.eWeaponMotionType.DAGGER,
        0.0f
      },
      {
        SystemIdDefine.eWeaponMotionType.LONGSWORD_2,
        0.6f
      },
      {
        SystemIdDefine.eWeaponMotionType.SWORD_KIMONO,
        0.4f
      },
      {
        SystemIdDefine.eWeaponMotionType.WAND_KIMONO,
        0.0f
      },
      {
        SystemIdDefine.eWeaponMotionType.NO_WAND_WITCH,
        0.0f
      },
      {
        SystemIdDefine.eWeaponMotionType.WAND_2,
        0.0f
      },
      {
        SystemIdDefine.eWeaponMotionType.NO_WAND_WITCH_2,
        0.0f
      },
      {
        SystemIdDefine.eWeaponMotionType.DUALSWORD,
        0.3f
      },
      {
        SystemIdDefine.eWeaponMotionType.FLAIL,
        0.45f
      },
      {
        SystemIdDefine.eWeaponMotionType.WAND_3,
        0.0f
      }
    };
    public static readonly Dictionary<SystemIdDefine.eWeaponMotionType, float> WEAPON_EFFECT_DELAY_DIC = new Dictionary<SystemIdDefine.eWeaponMotionType, float>
    {
      {
        SystemIdDefine.eWeaponMotionType.ARROW,
        0.9f
      },
      {
        SystemIdDefine.eWeaponMotionType.WAND,
        0.3f
      },
      {
        SystemIdDefine.eWeaponMotionType.DAGGER,
        0.3f
      },
      {
        SystemIdDefine.eWeaponMotionType.WAND_3,
        0.3f
      }
    };
    public const float BATTLE_SCALE = 540f;
    public const float CAMERA_MIN_X = 1100f;
    public const int BG_WIDTH = 1440;
    public const int BG_HEIGHT = 540;
    public static readonly float HEAD_OFFSET = 100f;
    public static readonly float AUDIO_LISTENER_POS_Z = -9.25926f;
    public const float UNIT_MOVE_SPEED = 1600f;
    public const string FOOTER_SHAKE_NAME = "battle_footer_icon_shake";
    public const float GOLD_EFFECT_DELAY = 0.2f;
    public const float TREASURE_DROP_INTERVAL = 0.02f;
    public const float TREASURE_DROP_INTERVAL_RARE = 0.06f;
    public const float TREASURE_MOVE_START_INTERVAL = 0.05f;
    public static readonly float[] TREASURE_POS_OFFSET_X = new float[3]
    {
      0.0f,
      0.2777778f,
      -0.2777778f
    };
    public const float TREASURE_POS_OFFSET_Y = 0.08333334f;
    public static readonly Vector3 TREASURE_BOX_POS = new Vector3(-1.162f, -0.17726f);
    public const int WAVE_MAX = 3;
    /*public static readonly Dictionary<SoundManager.eVoiceType, BattleDefine.VoiceJudgeData> VoiceJudgeDataDictionary = new Dictionary<SoundManager.eVoiceType, BattleDefine.VoiceJudgeData>()
    {
      {
        SoundManager.eVoiceType.BATTLE_START,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.WIN,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.LAST_WAVE,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.WAVE_START,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.NEXT_WAVE,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.RETIRE,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.CUT_IN,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = true
        }
      },
      {
        SoundManager.eVoiceType.UNION_BURST,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = true
        }
      },
      {
        SoundManager.eVoiceType.SKILL,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SP_SKILL,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.APPLAUD,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.THANKS,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.DAMAGE,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.ATTACK,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_BATTLE_START,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_WAVE_START,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_NEXT_WAVE,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_LAST_WAVE,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_CUT_IN,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_APPLAUD,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_THANKS,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_CLEAR_WIN,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_WIN,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.SPECIAL_CLOSE_GAME,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = false,
          PlayIfShadow = false,
          Star6Voice = false
        }
      },
      {
        SoundManager.eVoiceType.OTHER,
        new BattleDefine.VoiceJudgeData()
        {
          PlayIfOther = true,
          PlayIfShadow = true,
          Star6Voice = false
        }
      }
    };*/
    public const float MAX_DAMAGE = 999999f;
    public const int MOTION_PREFIX_MODE_CHANGE = 1;
    public const float PERCENT_DIGID = 100f;
    public const int ENEMY_COLOR_DEFAULT = 0;
    public const int ENEMY_COLOR_START = 1;
    public const int ENEMY_COLOR_LAST = 5;
    public const float HEAL_INTERVAL = 0.45f;

    public static eUnitRespawnPos GetUnitRespawnPos(int index, bool isEnemySide = false)
    {
      if (index < 0 || index >= UnitRespawnPosList.Count)
        return eUnitRespawnPos.MAIN_POS_3;
      return !isEnemySide ? UnitRespawnPosList[index] : enemyRespawnPosList[index];
    }

    public static int GetUnitSortOrder(UnitCtrl _unit)
    {
      eUnitRespawnPos respawnPos = _unit.RespawnPos;
      int num = _unit.IsDepthBack ? 1 : 0;
      bool isOther = _unit.IsOther;
      if (num != 0)
        return 350;
      if (!unitSortOrderDictionary.ContainsKey(respawnPos))
        return 0;
      int unitSortOrder = unitSortOrderDictionary[respawnPos];
      return !isOther ? unitSortOrder : unitSortOrder - 500;
    }

    public static int GetResultUnitSortOrder(int _baseDepth, int _order) => _baseDepth + 100 * _order;

    [Serializable]
    public class ZoomEffect
    {
      public bool Enable;
      public bool ConsiderScreenSize = true;
      public float StartDelay;
      public float StartDuration = 0.15f;
      public float Duration = 1.55f;
      public float EndDuration = 0.1f;
      public float To = 0.9f;
      public eZoomTarget ZoomTarget;
      public float OffsetY;
      public float OffsetX;
      public bool UseYBottom;
      public CustomEasing StartEasing;
      public CustomEasing EndEasing;
      public CustomEasing StartPosXEasing;
      public CustomEasing EndPosXEasing;
      public CustomEasing StartPosYEasing;
      public CustomEasing EndPosYEasing;
      public List<ZoomEffectPlual> ZoomEffectList = new List<ZoomEffectPlual>();
      public CustomEasing.eType StartEasingType = CustomEasing.eType.outQuad;
      public CustomEasing.eType EndEasingType = CustomEasing.eType.outQuad;

      public float ToPosX { get; set; }

      public float ToPosY { get; set; }
    }

    [Serializable]
    public class ZoomEffectPlual
    {
      public float StartDuration = 0.15f;
      public float Duration = 1.55f;
      public float To = 0.5f;
      public float OffsetX;
      public float OffsetY;
      public eZoomTarget ZoomTarget;
      public CustomEasing StartEasing;
      public CustomEasing StartPosXEasing;
      public CustomEasing StartPosYEasing;
      public CustomEasing.eType StartEasingType = CustomEasing.eType.outQuad;
      public bool UseYBottom;

      public float ToPosX { get; set; }

      public float ToPosY { get; set; }
    }

    [Serializable]
    public class SlowEffect
    {
      public bool Enable;
      public float StartDelay;
      public float StartDuration;
      public float Duration = 1.55f;
      public float EndDuration = 0.1f;
      public float To = 0.9f;
      public CustomEasing StartEasing;
      public CustomEasing EndEasing;
    }

    public struct VoiceJudgeData
    {
      public bool PlayIfOther;
      public bool PlayIfShadow;
      public bool Star6Voice;
    }
  }
}
