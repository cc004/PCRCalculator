// Decompiled with JetBrains decompiler
// Type: Elements.SystemIdDefine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public static class SystemIdDefine
  {
    public static readonly int AREA_ID_DEFAULT_MIN = 11001;
    public static readonly int AREA_ID_DEFAULT_MAX = 11999;
    public static readonly int AREA_ID_ELITE_MIN = 12001;
    public static readonly int AREA_ID_ELITE_MAX = 12999;
    public static readonly int AREA_ID_VERY_HARD_MIN = 13001;
    public static readonly int AREA_ID_VERY_HARD_MAX = 13999;
    public static readonly int AREA_ID_UNIQUE_EQUIP = 18001;
    public static readonly int AREA_ID_HIGH_RARITY_EQUIP = 19001;
    public static readonly int AREA_ID_RUPEE = 21001;
    public static readonly int AREA_ID_EXP = 21002;
    /*public static readonly Dictionary<eMissionStatusType, int> MissionStatusTypeOrderDictionary = new Dictionary<eMissionStatusType, int>()
    {
      {
        eMissionStatusType.EnableReceive,
        0
      },
      {
        eMissionStatusType.NoClear,
        1
      },
      {
        eMissionStatusType.AlreadyReceive,
        2
      },
      {
        eMissionStatusType.ChallengePeriodEnd,
        3
      }
    };
    public static readonly Dictionary<eMissionType, eTextId> MissionTypeTextDictionary = new Dictionary<eMissionType, eTextId>((IEqualityComparer<eMissionType>) new eMissionType_DictComparer())
    {
      {
        eMissionType.Daily,
        eTextId.MISSION_NAME_DAILY
      },
      {
        eMissionType.Always,
        eTextId.MISSION_NAME_ALWAYS
      },
      {
        eMissionType.Special,
        eTextId.MISSION_NAME_SPECIAL
      },
      {
        eMissionType.Emblem,
        eTextId.MISSION_NAME_EMBLEM
      },
      {
        eMissionType.ClanBattleDaily,
        eTextId.MISSION_NAME_CLAN_BATTLE_DAILY
      },
      {
        eMissionType.CLanBattleStationary,
        eTextId.MISSION_NAME_CLAN_BATTLE_ALWAYS
      },
      {
        eMissionType.FriendCampaignBeginner,
        eTextId.FRIEND_CAMPAIGN_BEGINNER_MISSION
      },
      {
        eMissionType.FriendCampaignFriend,
        eTextId.FRIEND_CAMPAIGN_FRIEND_MISSION
      },
      {
        eMissionType.INVALID_VALUE,
        eTextId.NONE
      }
    };
    public static readonly Dictionary<eSystemId, eTextId> SystemIdTextDictionary = new Dictionary<eSystemId, eTextId>((IEqualityComparer<eSystemId>) new eSystemId_DictComparer())
    {
      {
        eSystemId.ERROR,
        eTextId.NONE
      },
      {
        eSystemId.NORMAL_QUEST,
        eTextId.MAIN_QUEST
      },
      {
        eSystemId.HARD_QUEST,
        eTextId.MAIN_QUEST
      },
      {
        eSystemId.VERY_HARD,
        eTextId.MAIN_QUEST
      },
      {
        eSystemId.SPECIAL_QUEST,
        eTextId.TRAINING_QUEST
      },
      {
        eSystemId.EXPEDITION_QUEST,
        eTextId.DUNGEON
      },
      {
        eSystemId.STORY_QUEST,
        eTextId.QUEST
      },
      {
        eSystemId.CLAN_BATTLE,
        eTextId.CLAN_BATTLE
      },
      {
        eSystemId.TOWER,
        eTextId.TOWER
      },
      {
        eSystemId.UNIQUE_EQUIPMENT,
        eTextId.UNIQUE_EQUIP_QUEST
      },
      {
        eSystemId.HIGH_RARITY_EQUIPMENT,
        eTextId.HIGH_RARITY_EQUIP_QUEST
      },
      {
        eSystemId.NORMAL_SHOP,
        eTextId.SHOP
      },
      {
        eSystemId.ARENA_SHOP,
        eTextId.SHOP
      },
      {
        eSystemId.GRAND_ARENA_SHOP,
        eTextId.SHOP
      },
      {
        eSystemId.EXPEDITION_SHOP,
        eTextId.SHOP
      },
      {
        eSystemId.CLAN_BATTLE_SHOP,
        eTextId.SHOP
      },
      {
        eSystemId.LIMITED_SHOP,
        eTextId.SHOP
      },
      {
        eSystemId.MEMORY_PIECE_SHOP,
        eTextId.SHOP
      },
      {
        eSystemId.GOLD_SHOP,
        eTextId.PRESENT_GOLD_SHOP_TEXT
      },
      {
        eSystemId.NORMAL_GACHA,
        eTextId.NORMAL_GACHA_TITLE
      },
      {
        eSystemId.RARE_GACHA,
        eTextId.GACHA
      },
      {
        eSystemId.FESTIVAL_GACHA,
        eTextId.GACHA
      },
      {
        eSystemId.START_DASH_GACHA,
        eTextId.GACHA
      },
      {
        eSystemId.LEGEND_GACHA,
        eTextId.GACHA
      },
      {
        eSystemId.LIMITED_CHARA_GACHA,
        eTextId.GACHA
      },
      {
        eSystemId.NORMAL_ARENA,
        eTextId.BATTLE_ARENA
      },
      {
        eSystemId.GRAND_ARENA,
        eTextId.GRAND_ARENA
      },
      {
        eSystemId.UNIT_EQUIP,
        eTextId.NONE
      },
      {
        eSystemId.UNIT_LVUP,
        eTextId.DIALOG_CONFIRM_LV_UP_TITLE
      },
      {
        eSystemId.UNIT_SKILL_LVUP,
        eTextId.SKILL_REINFORCE_TITLE
      },
      {
        eSystemId.UNIT_RARITY_UP,
        eTextId.NONE
      },
      {
        eSystemId.UNIT_STATUS,
        eTextId.UNIT_DETAIL_TITLE
      },
      {
        eSystemId.UNIT_EQUIP_ENHANCE,
        eTextId.EQUIP_ENHANCE
      },
      {
        eSystemId.EQUIPMENT_DONATION,
        eTextId.EQUIPMENT_DONATION
      },
      {
        eSystemId.UNIT_GET,
        eTextId.GET_UNIT_TITLE
      },
      {
        eSystemId.ROOM_1F,
        eTextId.ROOM
      },
      {
        eSystemId.ROOM_2F,
        eTextId.ROOM
      },
      {
        eSystemId.ROOM_3F,
        eTextId.ROOM
      },
      {
        eSystemId.CLAN,
        eTextId.CLAN
      },
      {
        eSystemId.STORY,
        eTextId.PRESENT_STORY
      },
      {
        eSystemId.DATA_LINK,
        eTextId.VIEW_MENU_TOP_DATA
      },
      {
        eSystemId.HATSUNE_TOP,
        eTextId.STORY_EVENT
      },
      {
        eSystemId.HATSUNE_NORMAL_QUEST,
        eTextId.HATSUNE_EVENT_QUEST
      },
      {
        eSystemId.HATSUNE_HARD_QUEST,
        eTextId.HATSUNE_EVENT_QUEST
      },
      {
        eSystemId.HATSUNE_REVIVAL_TOP,
        eTextId.STORY_EVENT_REVIVAL
      },
      {
        eSystemId.HATSUNE_REVIVAL_NORMAL_QUEST,
        eTextId.HATSUNE_EVENT_REVIVAL_QUEST
      },
      {
        eSystemId.HATSUNE_REVIVAL_HARD_QUEST,
        eTextId.HATSUNE_EVENT_REVIVAL_QUEST
      },
      {
        eSystemId.SHIORI_EVENT_TOP,
        eTextId.SIDE_STORY
      },
      {
        eSystemId.SHIORI_EVENT_QUEST_NORMAL,
        eTextId.HATSUNE_SIDE_STORY_QUEST
      },
      {
        eSystemId.SHIORI_EVENT_QUEST_HARD,
        eTextId.HATSUNE_SIDE_STORY_QUEST
      },
      {
        eSystemId.BULK_SKIP,
        eTextId.BULK_SKIP
      }
    };
    public static readonly Dictionary<eSystemId, eTextId> SystemIdTextForRarityUpDictionary = new Dictionary<eSystemId, eTextId>((IEqualityComparer<eSystemId>) new eSystemId_DictComparer())
    {
      {
        eSystemId.NORMAL_ARENA,
        eTextId.BATTLE_ARENA
      },
      {
        eSystemId.ARENA_SHOP,
        eTextId.BATTLE_ARENA
      },
      {
        eSystemId.GRAND_ARENA,
        eTextId.GRAND_ARENA
      },
      {
        eSystemId.GRAND_ARENA_SHOP,
        eTextId.GRAND_ARENA
      },
      {
        eSystemId.EXPEDITION_QUEST,
        eTextId.DUNGEON
      },
      {
        eSystemId.EXPEDITION_SHOP,
        eTextId.DUNGEON
      },
      {
        eSystemId.CLAN_BATTLE_SHOP,
        eTextId.CLAN
      },
      {
        eSystemId.CLAN,
        eTextId.CLAN
      },
      {
        eSystemId.CLAN_BATTLE,
        eTextId.CLAN
      },
      {
        eSystemId.MEMORY_PIECE_SHOP,
        eTextId.EXTRA_COIN
      }
    };
    public static readonly Dictionary<eLineupGruopSetId, eSystemId> LineupGroupSetIdToSystemIdDictionary = new Dictionary<eLineupGruopSetId, eSystemId>((IEqualityComparer<eLineupGruopSetId>) new eLineupGruopSetId_DictComparer())
    {
      {
        eLineupGruopSetId.NORMAL_SHOP,
        eSystemId.NORMAL_SHOP
      },
      {
        eLineupGruopSetId.ARENA_SHOP,
        eSystemId.ARENA_SHOP
      },
      {
        eLineupGruopSetId.GRAND_ARENA_SHOP,
        eSystemId.GRAND_ARENA_SHOP
      },
      {
        eLineupGruopSetId.EXPEDITION_SHOP,
        eSystemId.EXPEDITION_SHOP
      },
      {
        eLineupGruopSetId.CLAN_BATTLE_SHOP,
        eSystemId.CLAN_BATTLE_SHOP
      },
      {
        eLineupGruopSetId.LIMITED_SHOP,
        eSystemId.LIMITED_SHOP
      }
    };
    public static readonly eTextId[] LoginBonusLotteryIdTexts = new eTextId[8]
    {
      eTextId.LOGIN_BONUS_LOTTERY_NAME_1,
      eTextId.LOGIN_BONUS_LOTTERY_NAME_2,
      eTextId.LOGIN_BONUS_LOTTERY_NAME_3,
      eTextId.LOGIN_BONUS_LOTTERY_NAME_4,
      eTextId.LOGIN_BONUS_LOTTERY_NAME_5,
      eTextId.LOGIN_BONUS_LOTTERY_NAME_6,
      eTextId.LOGIN_BONUS_LOTTERY_NAME_7,
      eTextId.LOGIN_BONUS_LOTTERY_NAME_8
    };
    public static readonly Dictionary<eSystemId, SystemIdDefine.ContentReleaseInfo> ContentReleaseInfoDictionary = new Dictionary<eSystemId, SystemIdDefine.ContentReleaseInfo>()
    {
      {
        eSystemId.HARD_QUEST,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.MAIN_QUEST_HARD_LEVEL)
      },
      {
        eSystemId.UNIT_SKILL_LVUP,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.SKILL_REINFORCE_TITLE)
      },
      {
        eSystemId.ROOM_1F,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.ROOM, _isTutorialEndCheck: true)
      },
      {
        eSystemId.SPECIAL_QUEST,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.TRAINING_QUEST)
      },
      {
        eSystemId.UNIT_EQUIP_ENHANCE,
        new SystemIdDefine.ContentReleaseInfo(_text: eTextId.EQUIP_ENHANCE)
      },
      {
        eSystemId.LIMITED_SHOP,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.LIMITED_SHOP)
      },
      {
        eSystemId.CLAN,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.CONTENT_RELEASE_TEXT_CLAN_AND_CLAN_BATTLE)
      },
      {
        eSystemId.ROOM_2F,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.ROOM_2F, _isTutorialEndCheck: true)
      },
      {
        eSystemId.NORMAL_ARENA,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.BATTLE_ARENA, _isTutorialEndCheck: true)
      },
      {
        eSystemId.ROOM_3F,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.ROOM_3F, _isTutorialEndCheck: true)
      },
      {
        eSystemId.GRAND_ARENA,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.GRAND_ARENA, _isTutorialEndCheck: true)
      },
      {
        eSystemId.TOWER,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.TOWER, true)
      },
      {
        eSystemId.UNIQUE_EQUIPMENT,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.UNIQUE_EQUIP_QUEST)
      },
      {
        eSystemId.VERY_HARD,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.MAIN_QUEST_VERY_HARD_LEVEL)
      },
      {
        eSystemId.HIGH_RARITY_EQUIPMENT,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.HIGH_RARITY_EQUIP_QUEST)
      },
      {
        eSystemId.FRIEND,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.FRIEND_TEXT)
      },
      {
        eSystemId.BULK_SKIP,
        new SystemIdDefine.ContentReleaseInfo(eViewId.QUEST_MAP, eTextId.BULK_SKIP)
      }
    };
    public static readonly Dictionary<eSystemId, eSystemId> ContentReleaseIconDictionary = new Dictionary<eSystemId, eSystemId>()
    {
      {
        eSystemId.NORMAL_ARENA,
        eSystemId.NORMAL_ARENA
      },
      {
        eSystemId.ARENA_SHOP,
        eSystemId.NORMAL_ARENA
      },
      {
        eSystemId.GRAND_ARENA,
        eSystemId.GRAND_ARENA
      },
      {
        eSystemId.GRAND_ARENA_SHOP,
        eSystemId.GRAND_ARENA
      },
      {
        eSystemId.EXPEDITION_QUEST,
        eSystemId.EXPEDITION_QUEST
      },
      {
        eSystemId.EXPEDITION_SHOP,
        eSystemId.EXPEDITION_QUEST
      },
      {
        eSystemId.CLAN_BATTLE_SHOP,
        eSystemId.CLAN
      },
      {
        eSystemId.UNIQUE_EQUIPMENT,
        eSystemId.UNIQUE_EQUIPMENT
      },
      {
        eSystemId.HIGH_RARITY_EQUIPMENT,
        eSystemId.HIGH_RARITY_EQUIPMENT
      },
      {
        eSystemId.NORMAL_SHOP,
        eSystemId.LIMITED_SHOP
      },
      {
        eSystemId.GOLD_SHOP,
        eSystemId.LIMITED_SHOP
      },
      {
        eSystemId.MEMORY_PIECE_SHOP,
        eSystemId.LIMITED_SHOP
      }
    };
    */
    public enum AreaType
    {
      NONE = 0,
      NORMAL = 11, // 0x0000000B
      HARD = 12, // 0x0000000C
      VERY_HARD = 13, // 0x0000000D
      UNIQUE_EQUIP = 18, // 0x00000012
      HIGH_RARITY_EQUIP = 19, // 0x00000013
      HATSUNE_NORMAL = 100, // 0x00000064
      HATSUNE_HARD = 200, // 0x000000C8
      RUPEE = 21001, // 0x00005209
      EXP = 21002, // 0x0000520A
    }

    public enum eMissionConditionType
    {
      UNIQUE = 1,
      COUNT = 2,
    }

    public enum eMultiLobbyType
    {
      ALL = 1,
      GUILD = 2,
    }

    public enum eWeaponSeType
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
      LONGSWORD_2 = 10, // 0x0000000A
      SCRATCH_1 = 11, // 0x0000000B
      SCRATCH_2 = 12, // 0x0000000C
      HAMMER = 13, // 0x0000000D
      BITE = 14, // 0x0000000E
      COMMON_SMALL = 15, // 0x0000000F
      COMMON_LARGE = 16, // 0x00000010
      EXPLOSION = 17, // 0x00000011
      WAND_KIMONO = 21, // 0x00000015
      SWORD_KIMONO = 22, // 0x00000016
      NO_WAND_WITCH = 23, // 0x00000017
      WAND_2 = 25, // 0x00000019
      NO_WAND_WITCH_2 = 26, // 0x0000001A
      DUALSWORD = 27, // 0x0000001B
      FLAIL = 28, // 0x0000001C
      WAND_3 = 29, // 0x0000001D
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
      LONGSWORD_2 = 10, // 0x0000000A
      WAND_KIMONO = 21, // 0x00000015
      SWORD_KIMONO = 22, // 0x00000016
      NO_WAND_WITCH = 23, // 0x00000017
      WAND_2 = 25, // 0x00000019
      NO_WAND_WITCH_2 = 26, // 0x0000001A
      DUALSWORD = 27, // 0x0000001B
      FLAIL = 28, // 0x0000001C
      WAND_3 = 29, // 0x0000001D
    }

    /*public class ContentReleaseInfo
    {
      public eViewId MoveTarget { get; private set; }

      public eTextId Text { get; private set; }

      public bool MoveButtonEnable { get; private set; }

      public eViewId ContentView { get; private set; }

      public bool IsTutorialEndCheck { get; private set; }

      public ContentReleaseInfo(
        eViewId _moveTargetView = eViewId.NONE,
        eTextId _text = eTextId.NONE,
        bool _moveButtonEnable = false,
        eViewId _contentTargetView = eViewId.NONE,
        bool _isTutorialEndCheck = false)
      {
        this.MoveTarget = _moveTargetView;
        this.Text = _text;
        this.MoveButtonEnable = _moveButtonEnable;
        this.ContentView = _contentTargetView;
        this.IsTutorialEndCheck = _isTutorialEndCheck;
      }
    }*/
  }
}
