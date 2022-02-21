// Decompiled with JetBrains decompiler
// Type: Elements.UnitDefine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class UnitDefine
  {
    public const int SKIN_CHANGE_RARITY = 3;
    public const int SKIN_CHANGE_RARITY_6 = 6;
    public const int INVALID_CRAFT_COST = -1;
    public const int INVALID_UNIT_ID = 0;
    public const int INVALID_UNIT_INDEX = 0;
    public const int INVALID_SKIN_ID = 0;
    public const int INVALID_RARITY = 0;
    public const int USER_DATA_RARITY = -2;
    public const int PARTY_UNIT_NUM = 5;
    public const int PERSON_CHARA_BASE_ANIMATION_NUMBER = 0;
    public const int DISPATCH_UNIT_ID = 1;
    public const int BULK_EQUIP_SLOT_ID = -1;
    public const int MINIMUM_PARTY_COUNT = 2;
    public const int UNKNOWN_EQUIPMENT_ID = 999999;
    public const int EQUIP_MAX_SLOT_NUM = 6;
    public const int HIGH_RARITY_EQUIP_MAX_SLOT_NUM = 3;
    public const int RARITY_SKIN_ID_OFFSET_1_2 = 10;
    public const int RARITY_SKIN_ID_OFFSET_3_4_5 = 30;
    public const int RARITY_SKIN_ID_OFFSET_6 = 60;
    public const int UNIT_WIN_LOSE_VOICE_START_INDEX = 1;
    public const int UNIT_WIN_LOSE_VOICE_NUM = 3;
    public const int UNIT_ID_LENGTH = 6;
    public const string UNIT_ICON_ORGANIZE_FIXED_SPRITE_NAME = "common_icon_unit_lock";
    public const int INVALID_RARITY_EQUIP_SLOT_ID = -1;
    public const int SUPPORT_UNIT_MAX_HP_VALUE = -1;
    /*public static readonly Dictionary<UnitSort.eSortType, eTextId> SORT_TYPE_NAME_DICTIONARY = new Dictionary<UnitSort.eSortType, eTextId>()
    {
      {
        UnitSort.eSortType.LV,
        eTextId.DIALOG_UNIT_SORT_TYPE_LEVEL
      },
      {
        UnitSort.eSortType.POWER,
        eTextId.DIALOG_UNIT_SORT_TYPE_POWER
      },
      {
        UnitSort.eSortType.HP,
        eTextId.DIALOG_UNIT_SORT_TYPE_HP
      },
      {
        UnitSort.eSortType.ATK,
        eTextId.DIALOG_UNIT_SORT_TYPE_ATK
      },
      {
        UnitSort.eSortType.DEF,
        eTextId.DIALOG_UNIT_SORT_TYPE_DEF
      },
      {
        UnitSort.eSortType.MAGIC_ATK,
        eTextId.DIALOG_UNIT_SORT_TYPE_MAGIC_ATK
      },
      {
        UnitSort.eSortType.MAGIC_DEF,
        eTextId.DIALOG_UNIT_SORT_TYPE_MAGIC_DEF
      },
      {
        UnitSort.eSortType.RANK,
        eTextId.DIALOG_UNIT_SORT_TYPE_RANK
      },
      {
        UnitSort.eSortType.RARITY,
        eTextId.DIALOG_UNIT_SORT_TYPE_RARETY
      },
      {
        UnitSort.eSortType.JAPANESE_SYLLABARY,
        eTextId.DIALOG_UNIT_SORT_TYPE_JAPANESE_SYLLABARY
      },
      {
        UnitSort.eSortType.AFFECTION_RANK,
        eTextId.DIALOG_UNIT_SORT_TYPE_AFFECTION_RANK
      },
      {
        UnitSort.eSortType.OBTAIN,
        eTextId.DIALOG_UNIT_SORT_TYPE_OBTAIN
      },
      {
        UnitSort.eSortType.PRICE,
        eTextId.DIALOG_UNIT_SORT_TYPE_PRICE
      },
      {
        UnitSort.eSortType.UNIQUE,
        eTextId.DIALOG_UNIT_SORT_TYPE_UNIQUE_EQUIP
      },
      {
        UnitSort.eSortType.MY_SELECT,
        eTextId.MY_SELECT
      },
      {
        UnitSort.eSortType.HIGH_RARITY_RELEASED,
        eTextId.DIALOG_UNIT_SORT_TYPE_HIGH_RARITY_RELEASED
      },
      {
        UnitSort.eSortType.UNOWNED_UNIT,
        eTextId.UNOWNED_UNIT_SORT
      }
    };*/
    /*public static readonly List<UnitSort.eSortType> DEFAULT_SORT_TYPES = new List<UnitSort.eSortType>()
    {
      UnitSort.eSortType.LV,
      UnitSort.eSortType.POWER,
      UnitSort.eSortType.RANK,
      UnitSort.eSortType.RARITY,
      UnitSort.eSortType.ATK,
      UnitSort.eSortType.MAGIC_ATK,
      UnitSort.eSortType.DEF,
      UnitSort.eSortType.MAGIC_DEF,
      UnitSort.eSortType.HP,
      UnitSort.eSortType.AFFECTION_RANK,
      UnitSort.eSortType.JAPANESE_SYLLABARY,
      UnitSort.eSortType.UNIQUE,
      UnitSort.eSortType.MY_SELECT,
      UnitSort.eSortType.HIGH_RARITY_RELEASED
    };*/
    /*public static readonly List<UnitSort.eSortType> SUPPORT_SORT_TYPES = new List<UnitSort.eSortType>()
    {
      UnitSort.eSortType.LV,
      UnitSort.eSortType.POWER,
      UnitSort.eSortType.RANK,
      UnitSort.eSortType.RARITY,
      UnitSort.eSortType.ATK,
      UnitSort.eSortType.MAGIC_ATK,
      UnitSort.eSortType.DEF,
      UnitSort.eSortType.MAGIC_DEF,
      UnitSort.eSortType.HP,
      UnitSort.eSortType.JAPANESE_SYLLABARY,
      UnitSort.eSortType.UNIQUE,
      UnitSort.eSortType.HIGH_RARITY_RELEASED
    };*/
    /*public static readonly List<UnitSort.eSortType> CHARA_EXCHANGE_TICKET_SORT_TYPES = new List<UnitSort.eSortType>()
    {
      UnitSort.eSortType.POWER,
      UnitSort.eSortType.RARITY,
      UnitSort.eSortType.ATK,
      UnitSort.eSortType.MAGIC_ATK,
      UnitSort.eSortType.DEF,
      UnitSort.eSortType.MAGIC_DEF,
      UnitSort.eSortType.HP,
      UnitSort.eSortType.JAPANESE_SYLLABARY,
      UnitSort.eSortType.HIGH_RARITY_RELEASED,
      UnitSort.eSortType.UNOWNED_UNIT
    };*/
    /*public static readonly Dictionary<UnitSort.ePriorityType, eTextId> PRIORITY_TYPE_NAME_DICTIONARY = new Dictionary<UnitSort.ePriorityType, eTextId>()
    {
      {
        UnitSort.ePriorityType.USE,
        eTextId.ON
      },
      {
        UnitSort.ePriorityType.NOT_USE,
        eTextId.OFF
      }
    };*/
    /*public static readonly List<UnitSort.ePriorityType> PRIORITY_SORT_TYPES = new List<UnitSort.ePriorityType>()
    {
      UnitSort.ePriorityType.USE,
      UnitSort.ePriorityType.NOT_USE
    };*/
    public const int SKILL_UPDATE_USE_GOLD = -1;
    public const int CHANGE_CHARACTER_ID = 100;
    public const int CHANGE_CLASS_ID_MOD_VALUE = 10;
    public const int SKIN_ID_NUMBER_OF_DIGIT = 10;
    public const int MIN_STAMINA = 60;
    public static readonly float UNIT_POS_FRONT_THRESHOLD = 250f;
    public static readonly float UNIT_POS_BACK_THRESHOLD = 550f;
    public static readonly int MAX_ENERGY = 1000;
    public static readonly int SKILL_RECOVERY_VALUE = 10;
    public static readonly int FREE_SKILL_MAX_NUM = 3;
    public const int PASSIVE_SKILL_ACTION_TYPE = 99;
    public const int CHECK_FOR_QUEST_MONSTER_ID = 1000000;
    public const int SHADOW_UNIT_ADD_ID = 500000;
    public const float CHARA_TEXT_EDGE_ALPHA = 0.5f;
    public const int MINIMUM_ENHANCE_USE_GOLD = 120;
    public const int UNIT_INITIALIZE_POWER = -1;
    public const int UNIT_BIRTHDAY_VOICE_ID = 901;
    /*public static Dictionary<eParamType, eTextId> ParameterKindDictionary = new Dictionary<eParamType, eTextId>()
    {
      {
        eParamType.ATK,
        eTextId.ATTACK
      },
      {
        eParamType.PHYSICAL_CRITICAL,
        eTextId.CRITICAL_RATE
      },
      {
        eParamType.DEF,
        eTextId.DEFENSE
      },
      {
        eParamType.DODGE,
        eTextId.DODGE_RATE
      },
      {
        eParamType.HP,
        eTextId.HP
      },
      {
        eParamType.WAVE_HP_RECOVERY,
        eTextId.HP_AUTO_RECOVERY
      },
      {
        eParamType.LIFE_STEAL,
        eTextId.HP_DRAIN
      },
      {
        eParamType.HP_RECOVERY_RATE,
        eTextId.HP_RECOVERY_RATE
      },
      {
        eParamType.MAGIC_ATK,
        eTextId.MAGIC_ATTACK
      },
      {
        eParamType.MAGIC_CRITICAL,
        eTextId.MAGIC_CRITICAL_RATE
      },
      {
        eParamType.MAGIC_DEF,
        eTextId.MAGIC_DEFENSE
      },
      {
        eParamType.MAGIC_PENETRATE,
        eTextId.MAGIC_PENETRATION
      },
      {
        eParamType.WAVE_ENERGY_RECOVERY,
        eTextId.MP_AUTO_RECOVERY
      },
      {
        eParamType.ENERGY_RECOVERY_RATE,
        eTextId.MP_RECOVERY_RATE
      },
      {
        eParamType.PHYSICAL_PENETRATE,
        eTextId.PENETRATION
      },
      {
        eParamType.ENERGY_REDUCE_RATE,
        eTextId.ENERGY_REDUCE_RATE
      },
      {
        eParamType.ACCURACY,
        eTextId.ACCURACY
      }
    };*/
    public static readonly List<UnitBgFaceType> BG_FACE_TYPE_LIST = new List<UnitBgFaceType>
    {
      new UnitBgFaceType(1, "eye_open", "mouth_open"),
      new UnitBgFaceType(3, "eye_open", "mouth_open"),
      new UnitBgFaceType(1, "eye_open", "mouth_close"),
      new UnitBgFaceType(1, "eye_open", "mouth_idle"),
      new UnitBgFaceType(1, "eye_open", "mouth_open"),
      new UnitBgFaceType(4, "eye_open", "mouth_open"),
      new UnitBgFaceType(5, "eye_open", "mouth_open"),
      new UnitBgFaceType(7, "eye_close", "mouth_close"),
      new UnitBgFaceType(7, "eye_close", "mouth_idle"),
      new UnitBgFaceType(7, "eye_close", "mouth_open"),
      new UnitBgFaceType(6, "eye_open", "mouth_open"),
      new UnitBgFaceType(1, "eye_close", "mouth_close"),
      new UnitBgFaceType(3, "eye_open", "mouth_close"),
      new UnitBgFaceType(3, "eye_open", "mouth_idle"),
      new UnitBgFaceType(4, "eye_open", "mouth_idle"),
      new UnitBgFaceType(4, "eye_open", "mouth_close"),
      new UnitBgFaceType(5, "eye_open", "mouth_idle"),
      new UnitBgFaceType(5, "eye_open", "mouth_close"),
      new UnitBgFaceType(2, "eye_open", "mouth_open"),
      new UnitBgFaceType(2, "eye_open", "mouth_idle"),
      new UnitBgFaceType(2, "eye_open", "mouth_close"),
      new UnitBgFaceType(6, "eye_open", "mouth_idle"),
      new UnitBgFaceType(6, "eye_open", "mouth_close"),
      new UnitBgFaceType(1, "eye_close", "mouth_close")
    };
    /*public static readonly List<StoryDefine.CharacterStorySort> CHARASTORY_SORT_TYPE_LIST = new List<StoryDefine.CharacterStorySort>()
    {
      StoryDefine.CharacterStorySort.ASA,
      StoryDefine.CharacterStorySort.TAHA,
      StoryDefine.CharacterStorySort.MAWA
    };*/
    /*public static readonly eTextId[,] PEOPLEBOOK_SET_FILTER_TYPE_LIST = new eTextId[3, 2]
    {
      {
        eTextId.CHARACTER_KANA_A,
        eTextId.CHARACTER_KANA_SO
      },
      {
        eTextId.CHARACTER_KANA_TA,
        eTextId.CHARACTER_KANA_HO
      },
      {
        eTextId.CHARACTER_KANA_MA,
        eTextId.CHARACTER_KANA_N
      }
    };*/
    public const int ALL_BATTLEUNIT_MONSTER_COMMON_NUMBER = 99;
    public const int ALL_BATTLE_UNIT_MONSTER_COMMON_DIVISOR = 100;
    public static readonly List<eUnitBattlePos> UNIT_TOP_TAB_POSITION_LIST = new List<eUnitBattlePos>(new eUnitBattlePos[4]
    {
        eUnitBattlePos.ALL,
        eUnitBattlePos.FRONT,
        eUnitBattlePos.MIDDLE,
        eUnitBattlePos.BACK
    });

    /*public static UnitData CopyUnitData(UnitData _original)
    {
      UnitData unitData = new UnitData();
      unitData.SetId((int) _original.Id);
      unitData.SetUnitExp((int) _original.UnitExp);
      unitData.SetUnitLevel((int) _original.UnitLevel);
      unitData.SetUnitParam(_original.UnitParam);
      unitData.SetUnitRarity((int) _original.UnitRarity);
      unitData.SetBattleRarity((int) _original.BattleRarity);
      unitData.SetEquipSlot(_original.EquipSlot);
      unitData.SetPromotionLevel(_original.PromotionLevel);
      unitData.SetUniqueEquipSlot(_original.UniqueEquipSlot);
      unitData.SetUnionBurst(_original.UnionBurst);
      unitData.SetMainSkill(_original.MainSkill);
      unitData.SetExSkill(_original.ExSkill);
      unitData.SetFreeSkill(_original.FreeSkill);
      unitData.SetBonusParam(_original.BonusParam);
      unitData.SetUnlockRarity6Item(_original.UnlockRarity6Item);
      return unitData;
    }*/

    public enum UnitEquipmentStatus
    {
      NO_POSSESION,
      NO_POSSESION_CANNOT_CRAFT,
      CAN_EQUIP,
      CAN_CRAFT,
      LV_SHORTAGE,
      EQUIPPED,
      CAN_EQUIP_LV_SHORTAGE,
      CAN_CRAFT_LV_SHORTAGE,
      UNKNOWN,
      RANK_SHORTAGE,
      STAR_SHORTAGE,
    }

    public enum eUnitClassId
    {
      INVALID_CLASS_ID,
      CLASS_1,
      CLASS_2,
      CLASS_3,
    }

    public enum eUnitRarity
    {
      INVALID_RARITY,
      RARITY_1,
      RARITY_2,
      RARITY_3,
      RARITY_4,
      RARITY_5,
      RARITY_6,
    }

    public enum eSkinType
    {
      Icon,
      Sd,
      Still,
      Motion,
    }

    public enum FreeSkillReleaseLevel
    {
      FREE_SKILL_1 = 3,
      FREE_SKILL_2 = 4,
      FREE_SKILL_3 = 5,
    }

    public enum UnitType
    {
      NONE = 0,
      PERSON = 1,
      MONSTER = 2,
      BOSS = 3,
      SUMMON_PERSON = 4,
      SUMMON_MONSTER = 5,
      GUEST = 9,
      SKILL_EFFECT = 10, // 0x0000000A
    }

    public enum UnitBaseID
    {
      NONE = 0,
      PERSON = 100000, // 0x000186A0
      MONSTER = 200000, // 0x00030D40
      BOSS = 300000, // 0x000493E0
      SUMMON_PERSON = 400000, // 0x00061A80
      SUMMON_MONSTER = 500000, // 0x0007A120
      GUEST = 900000, // 0x000DBBA0
    }

    public enum SupportUnitStatus
    {
      NOT_USE,
      CURRENT,
      USED,
    }

    public enum UniqueEquipOverLimitStatus
    {
      IMPOSSIBLE,
      POSSIBLE,
      MAX,
    }

    public enum UnitContentsCategory
    {
      NONE,
      EQUIP,
      PROMOTION,
      EVOLUTION,
      UNLOCK,
      SKILL,
      ENHANCE,
      EQUIP_ALL,
      LEVEL_UP,
      NORMAL_EQUIP,
      UNIQUE_EQUIP,
      UNIQUE_ENHANCE,
      UNIQUE_OVER_LIMIT,
      UNIQUE_ALL,
      HIGH_RARITY_EQUIP_UNLOCK,
      HIGH_RARITY_ENHANCE,
      HIGH_RARITY_QUEST_CHALLENGE,
      HIGH_RARITY_EVOLUTION,
      HIGH_RARITY_ALL,
    }

    public enum UnitNotificationType
    {
      ALL,
      EQUIP,
      SPECIFIC_EQUIP,
      RARITY,
      UNLOCK,
      SKILL,
      SKILL_TARGET_UNIT,
      LEVEL_UP,
      HIGH_RARITY,
    }

    public struct UnitLoveData
    {
      private int unitId;
      private int loveLevel;
      private Vector2 position;
      private GameObject gameObject;

      public int UnitId => unitId;

      public int LoveLevel => loveLevel;

      public Vector2 Position => !(gameObject != null) ? position : (Vector2)gameObject.transform.position;

      public UnitLoveData(int _unitId, int _loveLevel, Vector2 _position)
      {
        unitId = _unitId;
        loveLevel = _loveLevel;
        position = _position;
        gameObject = null;
      }

      public UnitLoveData(int _unitId, int _loveLevel, GameObject _gameObject)
      {
        unitId = _unitId;
        loveLevel = _loveLevel;
        position = Vector2.zero;
        gameObject = _gameObject;
      }
    }

    public enum eFaceId
    {
      INVALID,
      NORMAL,
      JOY,
      ANGER,
      SAD,
      SHY,
      SURPRISED,
      SPECIAL_A,
      SPECIAL_B,
      SPECIAL_C,
      SPECIAL_D,
      SPECIAL_E,
      NO_FACE,
    }

    public struct UnitBgFaceType
    {
      public int FaceId;
      public string EyeAnimation;
      public string MouthAnimation;

      public UnitBgFaceType(int _faceId, string _eyeAnimation, string _mouthAnimation)
      {
        FaceId = _faceId;
        EyeAnimation = _eyeAnimation;
        MouthAnimation = _mouthAnimation;
      }
    }

    public enum eCharacterHeightNumber
    {
      INVALID,
      HIGH,
      MIDDLE,
      LOW,
      SUPER_HIGH,
    }

    public enum eResultMotion
    {
      INVALID,
      COMMON,
      CHARACTER,
    }
  }
}
