// Decompiled with JetBrains decompiler
// Type: Elements.SpineDefine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Spine;
using System;
using System.Collections.Generic;

namespace Elements
{
  public class SpineDefine
  {
    public const int SPINE_UNIT_ANIME_COMMON_BATTLE_INDEX_START = 1;
    public const int SPINE_UNIT_ANIME_COMMON_BATTLE_INDEX_END = 8;
    public const float SPINE_POSITION_EPSILON = 0.01f;
    public const int EYE_ANIMATION_PLAY_TRACK = 0;
    public const int MOUTH_ANIMATION_PLAY_TRACK = 1;
    public const int INVALID_VALUE = -1;
    private static Dictionary<eSpineBinaryAnimationId, eResourceId> spineBinaryAnimationDic = new Dictionary<eSpineBinaryAnimationId, eResourceId>()
    {
      {
        eSpineBinaryAnimationId.COMMON_BATTLE,
        eResourceId.SPINE_UNIT_ANIME_COMMON_BATTLE
      },
      {
        eSpineBinaryAnimationId.COMMON_ROOM,
        eResourceId.ROOM_SPINEUNIT_ANIMATION_COMMON
      },
      {
        eSpineBinaryAnimationId.UNIQUE_ROOM,
        eResourceId.ROOM_SPINEUNIT_ANIMATION_UNIQUE
      },
      {
        eSpineBinaryAnimationId.BATTLE,
        eResourceId.SPINE_UNIT_ANIME_BATTLE
      },
      {
        eSpineBinaryAnimationId.ROOM_ITEM,
        eResourceId.ROOM_SPINEUNIT_ANIMATION_EACH
      },
      {
        eSpineBinaryAnimationId.ROOM_ITEM_UNIQUE,
        eResourceId.ROOM_SPINEUNIT_ANIMATION_EACH_UNIQUE
      },
      {
        eSpineBinaryAnimationId.ROOM_SYNC,
        eResourceId.ROOM_SYNC_ANIME
      },
      {
        eSpineBinaryAnimationId.ROOM_SYNC_UNIQUE,
        eResourceId.ROOM_SYNC_ANIME_UNIQUE
      },
      {
        eSpineBinaryAnimationId.ROOM_TRACK,
        eResourceId.ROOM_TRACK_ANIME
      },
      {
        eSpineBinaryAnimationId.ROOM_TRACK_UNIQUE,
        eResourceId.ROOM_TRACK_ANIME_UNIQUE
      },
      {
        eSpineBinaryAnimationId.EVENT,
        eResourceId.SPINE_UNIT_ANIME_EVENT
      },
      {
        eSpineBinaryAnimationId.EVENT_STORY,
        eResourceId.SPINE_UNIT_ANIME_EVENT_STORY
      },
      {
        eSpineBinaryAnimationId.POSING,
        eResourceId.SPINE_UNIT_ANIME_POSING
      },
      {
        eSpineBinaryAnimationId.SMILE,
        eResourceId.SPINE_UNIT_ANIME_SMILE
      },
      {
        eSpineBinaryAnimationId.LOADING,
        eResourceId.SPINE_UNIT_ANIME_LOADING
      },
      {
        eSpineBinaryAnimationId.DEFEAT,
        eResourceId.SPINE_UNIT_ANIME_DEFEAT
      },
      {
        eSpineBinaryAnimationId.MAP,
        eResourceId.SPINE_UNIT_ANIME_MAP
      },
      {
        eSpineBinaryAnimationId.MINIGAME,
        eResourceId.SPINE_UNIT_ANIME_MINIGAME
      },
      {
        eSpineBinaryAnimationId.RUN_JUMP,
        eResourceId.SPINE_UNIT_ANIME_RUN_JUMP
      },
      {
        eSpineBinaryAnimationId.NO_WEAPON,
        eResourceId.SPINE_UNIT_ANIME_NO_WEAPON
      },
      {
        eSpineBinaryAnimationId.RACE,
        eResourceId.SPINE_UNIT_ANIME_RACE
      },
      {
        eSpineBinaryAnimationId.COMBINE_JOY_RESULT,
        eResourceId.SPINE_UNIT_ANIME_COMBINE_JOY_RESULT
      },
      {
        eSpineBinaryAnimationId.DEAR,
        eResourceId.SPINE_UNIT_ANIME_DEAR
      },
      {
        eSpineBinaryAnimationId.EKD,
        eResourceId.SPINE_UNIT_ANIME_EKD
      },
      {
        eSpineBinaryAnimationId.NAVI,
        eResourceId.SPINE_UNIT_ANIME_NAVI
      },
      {
        eSpineBinaryAnimationId.OMP,
        eResourceId.SPINE_UNIT_ANIME_OMP
      },
      {
        eSpineBinaryAnimationId.UEK,
        eResourceId.SPINE_UNIT_ANIME_UEK
      },
      {
        eSpineBinaryAnimationId.NYX,
        eResourceId.SPINE_UNIT_ANIME_NYX
      },
      {
        eSpineBinaryAnimationId.PKB,
        eResourceId.SPINE_UNIT_ANIME_PKB
      }
    };
    public static readonly eSpineSkinId[] ProductQuantitySkinArray = new eSpineSkinId[3]
    {
      eSpineSkinId.QUANTITY_EMPTY,
      eSpineSkinId.QUANTITY_HALF,
      eSpineSkinId.QUANTITY_MAX
    };
    private static Dictionary<eSpineSkinId, string> spineSkinDic = new Dictionary<eSpineSkinId, string>()
    {
      {
        eSpineSkinId.ANGER,
        "anger"
      },
      {
        eSpineSkinId.JOY,
        "joy"
      },
      {
        eSpineSkinId.NORMAL,
        "normal"
      },
      {
        eSpineSkinId.SAD,
        "sad"
      },
      {
        eSpineSkinId.SHY,
        "shy"
      },
      {
        eSpineSkinId.SURPRISED,
        "surprised"
      },
      {
        eSpineSkinId.SPECIAL_A,
        "special_a"
      },
      {
        eSpineSkinId.SPECIAL_B,
        "special_b"
      },
      {
        eSpineSkinId.SPECIAL_C,
        "special_c"
      },
      {
        eSpineSkinId.SPECIAL_D,
        "special_d"
      },
      {
        eSpineSkinId.SPECIAL_E,
        "special_e"
      },
      {
        eSpineSkinId.DEFAULT,
        "default"
      },
      {
        eSpineSkinId.QUANTITY_MAX,
        "max"
      },
      {
        eSpineSkinId.QUANTITY_EMPTY,
        "empty"
      },
      {
        eSpineSkinId.QUANTITY_HALF,
        "half"
      }
    };
    private static readonly string[] roomAnimePersonalityStrings = new string[9]
    {
      "_def",
      "_nml",
      "_kyapi",
      "_kpt",
      "_styk",
      "_knmn",
      "_jun",
      "_nebbia",
      "_track"
    };
    private static readonly string[] roomAnimeTimingStrings = new string[4]
    {
      "",
      "_in",
      "_mid",
      "_out"
    };
    private static readonly string[] roomAnimeDirStrings = new string[4]
    {
      "_N",
      "_B",
      "_L",
      "_R"
    };
    private static readonly string[] roomAnimePatternStrings = new string[10]
    {
      "_A",
      "_B",
      "_C",
      "_D",
      "_E",
      "_F",
      "_G",
      "_H",
      "_I",
      "_J"
    };

    public static eSpineCharacterAnimeId GetCharacterAnimeId(string _idName)
    {
      try
      {
        return (eSpineCharacterAnimeId) Enum.Parse(typeof (eSpineCharacterAnimeId), _idName);
      }
      catch (ArgumentException ex)
      {
        return eSpineCharacterAnimeId.NONE;
      }
    }

    public static eResourceId GetResourceId(
      eSpineBinaryAnimationId spineBinaryAnimationId) => SpineDefine.spineBinaryAnimationDic[spineBinaryAnimationId];

    public static string GetSkinName(eSpineSkinId spineSkinId) => SpineDefine.spineSkinDic[spineSkinId];

    public static eSpineSkinId GetSkinId(string _spineSkin)
    {
      foreach (KeyValuePair<eSpineSkinId, string> keyValuePair in SpineDefine.spineSkinDic)
      {
        if (keyValuePair.Value == _spineSkin.ToLower())
          return keyValuePair.Key;
      }
      return eSpineSkinId.NORMAL;
    }

    public static string GetAnimeName(
      eSpineCharacterAnimeId animeId,
      int unitId = -1,
      int weaponId = -1,
      int index1 = -1,
      int index2 = -1,
      int index3 = -1)
    {
      if (UnitUtility.JudgeIsGuest(unitId))
        weaponId = 0;
      if (animeId == eSpineCharacterAnimeId.RUN && UnitUtility.IsEnemyUnit(unitId))
        animeId = eSpineCharacterAnimeId.WALK;
      string animeName = SpineConverterDefine.DataDic[animeId].AnimeName;
      switch (animeId)
      {
        case eSpineCharacterAnimeId.IDLE:
        case eSpineCharacterAnimeId.IDLE_NO_WEAPON:
        case eSpineCharacterAnimeId.IDLE_STAND_BY:
        case eSpineCharacterAnimeId.RUN_GAME_START:
        case eSpineCharacterAnimeId.WALK:
        case eSpineCharacterAnimeId.JOY_SHORT:
        case eSpineCharacterAnimeId.JOY_LONG:
        case eSpineCharacterAnimeId.COOP_STAND_BY:
        case eSpineCharacterAnimeId.STAND_BY:
        case eSpineCharacterAnimeId.RUN:
        case eSpineCharacterAnimeId.LANDING:
        case eSpineCharacterAnimeId.LOADING:
        case eSpineCharacterAnimeId.IDLE_SKIP:
        case eSpineCharacterAnimeId.DAMAGE_SKIP:
        case eSpineCharacterAnimeId.ANNIHILATION:
        case eSpineCharacterAnimeId.IDLE_MAP:
        case eSpineCharacterAnimeId.IDLE_MAP_S:
        case eSpineCharacterAnimeId.DAMEGE:
        case eSpineCharacterAnimeId.GAME_START:
        case eSpineCharacterAnimeId.IDLE_RESULT:
          return index1 == -1 ? (weaponId == 0 ? string.Format("{0:D6}_" + animeName, (object) unitId) : string.Format("{0:D2}_" + animeName, (object) weaponId)) : (weaponId == 0 ? string.Format("{0:D6}_{1:D1}_" + animeName, (object) unitId, (object) index1) : string.Format("{0:D2}_{1:D1}_" + animeName, (object) unitId, (object) index1));
        case eSpineCharacterAnimeId.IDLE_MULTI_TARGET:
        case eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET:
          return string.Format("{0:D6}_{2}{1:D1}", (object) unitId, (object) index2, (object) animeName);
        case eSpineCharacterAnimeId.JOY_RESULT:
          return index1 == -1 ? string.Format("{0:D6}_" + animeName, (object) unitId) : string.Format("{0:D6}_{1:D1}_" + animeName, (object) unitId, (object) index1);
        case eSpineCharacterAnimeId.SMILE:
        case eSpineCharacterAnimeId.RARITYUP_POSING:
        case eSpineCharacterAnimeId.MANA_IDLE:
        case eSpineCharacterAnimeId.MANA_JUMP:
        case eSpineCharacterAnimeId.STAMINA_BOW:
        case eSpineCharacterAnimeId.FRIEND_HIGHTOUCH:
        case eSpineCharacterAnimeId.RUN_JUMP:
        case eSpineCharacterAnimeId.RUN_HIGH_JUMP:
        case eSpineCharacterAnimeId.NO_WEAPON_IDLE:
        case eSpineCharacterAnimeId.NO_WEAPON_JOY_SHORT:
        case eSpineCharacterAnimeId.NO_WEAPON_RUN:
        case eSpineCharacterAnimeId.RACE_EAT_JUN:
        case eSpineCharacterAnimeId.RACE_EAT_NORMAL:
        case eSpineCharacterAnimeId.RACE_RUN_SPRING_JUMP:
        case eSpineCharacterAnimeId.RACE_BALLOON_FLYING_DOWN:
        case eSpineCharacterAnimeId.RACE_BALLOON_FLYING_LOOP:
        case eSpineCharacterAnimeId.RACE_BALLOON_FLYING_UP:
        case eSpineCharacterAnimeId.RACE_BALLOON_IN:
        case eSpineCharacterAnimeId.RACE_BALLOON_OUT:
        case eSpineCharacterAnimeId.RACE_RUN:
        case eSpineCharacterAnimeId.DEAR_IDOL:
        case eSpineCharacterAnimeId.DEAR_JUMP:
        case eSpineCharacterAnimeId.DEAR_SMILE:
          return string.Format("{0:D6}_" + animeName, (object) (weaponId == 0 ? unitId : 0));
        case eSpineCharacterAnimeId.SLEEP:
          return string.Format("{0:D6}_{1}_{2:D1}", (object) unitId, (object) animeName, (object) index2);
        case eSpineCharacterAnimeId.EVENT_BANNER_MOTION:
        case eSpineCharacterAnimeId.EVENT_BANNER_MOTION_CIRCLE:
        case eSpineCharacterAnimeId.FKE_IDLE:
        case eSpineCharacterAnimeId.EKD_GLD_01:
        case eSpineCharacterAnimeId.EKD_GLD_02:
        case eSpineCharacterAnimeId.EKD_GLD_03:
        case eSpineCharacterAnimeId.EKD_GLD_04:
        case eSpineCharacterAnimeId.EKD_GLD_05:
        case eSpineCharacterAnimeId.EKD_GLD_06:
        case eSpineCharacterAnimeId.EKD_IDLE_ANGER:
        case eSpineCharacterAnimeId.EKD_IDLE_JOY:
        case eSpineCharacterAnimeId.EKD_IDLE_JOY2:
        case eSpineCharacterAnimeId.EKD_IDLE_NORMAL:
        case eSpineCharacterAnimeId.EKD_IDLE_SAD:
        case eSpineCharacterAnimeId.EKD_IDLE_SURPRISE1:
        case eSpineCharacterAnimeId.EKD_IDLE_SURPRISE2:
        case eSpineCharacterAnimeId.EKD_OTHER_01:
        case eSpineCharacterAnimeId.EKD_OTHER_02:
        case eSpineCharacterAnimeId.EKD_OTHER_03:
        case eSpineCharacterAnimeId.EKD_OTHER_04:
        case eSpineCharacterAnimeId.EKD_OTHER_05:
        case eSpineCharacterAnimeId.EKD_OTHER_06:
        case eSpineCharacterAnimeId.EKD_OTHER_07:
        case eSpineCharacterAnimeId.EKD_OTHER_08:
        case eSpineCharacterAnimeId.EKD_OTHER_09:
        case eSpineCharacterAnimeId.EKD_OTHER_10:
        case eSpineCharacterAnimeId.EKD_PARTY_01:
        case eSpineCharacterAnimeId.EKD_PARTY_02:
        case eSpineCharacterAnimeId.EKD_PARTY_03:
        case eSpineCharacterAnimeId.EKD_PARTY_04:
        case eSpineCharacterAnimeId.EKD_PARTY_05:
        case eSpineCharacterAnimeId.EKD_PARTY_06:
        case eSpineCharacterAnimeId.EKD_PARTY_07:
        case eSpineCharacterAnimeId.EKD_PARTY_08:
        case eSpineCharacterAnimeId.EKD_PARTY_09:
        case eSpineCharacterAnimeId.EKD_PARTY_10:
        case eSpineCharacterAnimeId.EKD_PSN_01:
        case eSpineCharacterAnimeId.EKD_PSN_02:
        case eSpineCharacterAnimeId.EKD_PSN_03:
        case eSpineCharacterAnimeId.EKD_PSN_04:
        case eSpineCharacterAnimeId.EKD_PSN_05:
        case eSpineCharacterAnimeId.EKD_PSN_06:
        case eSpineCharacterAnimeId.EKD_PSN_07:
        case eSpineCharacterAnimeId.EKD_PSN_08:
        case eSpineCharacterAnimeId.EKD_PSN_09:
        case eSpineCharacterAnimeId.EKD_PSN_10:
        case eSpineCharacterAnimeId.EKD_RUN:
        case eSpineCharacterAnimeId.EKD_SMILE:
        case eSpineCharacterAnimeId.EKD_SURPRISE:
        case eSpineCharacterAnimeId.EKD_TALK_ANGER:
        case eSpineCharacterAnimeId.EKD_TALK_JOY:
        case eSpineCharacterAnimeId.EKD_TALK_JOY2:
        case eSpineCharacterAnimeId.EKD_TALK_NORMAL:
        case eSpineCharacterAnimeId.EKD_TALK_SAD:
        case eSpineCharacterAnimeId.EKD_TALK_SURPRISE1:
        case eSpineCharacterAnimeId.EKD_TALK_SURPRISE2:
          return animeName;
        case eSpineCharacterAnimeId.AWAKE_MAP:
        case eSpineCharacterAnimeId.AWAKE_MAP_RELEASE:
        case eSpineCharacterAnimeId.AWAKE:
        case eSpineCharacterAnimeId.ATTACK_SKIPQUEST:
        case eSpineCharacterAnimeId.AWAKE_RESULT:
          return weaponId == 0 ? string.Format("{0:D6}_" + animeName, (object) unitId) : string.Format("{0:D2}_" + animeName, (object) weaponId);
        case eSpineCharacterAnimeId.ATTACK:
          if (index3 != -1)
            return string.Format("{0:D6}_{1:D1}_" + animeName, (object) unitId, (object) index3);
          return weaponId == 0 ? string.Format("{0:D6}_" + animeName, (object) unitId) : string.Format("{0:D2}_" + animeName, (object) weaponId);
        case eSpineCharacterAnimeId.DIE:
        case eSpineCharacterAnimeId.DEFEAT:
        case eSpineCharacterAnimeId.DIE_LOOP:
          if (index1 != -1 && index2 != -1)
            return string.Format("{0:D6}_{1:D1}_" + animeName + "_{2:D1}", (object) unitId, (object) index1, (object) index2);
          if (index1 != -1)
            return string.Format("{0:D6}_" + animeName + "_{1:D1}", (object) unitId, (object) index1);
          if (index2 != -1)
            return string.Format("{0:D6}_{1:D1}_" + animeName, (object) unitId, (object) index2);
          return weaponId == 0 ? string.Format("{0:D6}_" + animeName, (object) unitId) : string.Format("{0:D2}_" + animeName, (object) weaponId);
        case eSpineCharacterAnimeId.SKILL:
        case eSpineCharacterAnimeId.SPECIAL_SKILL:
        case eSpineCharacterAnimeId.SPECIAL_SKILL_EVOLUTION:
        case eSpineCharacterAnimeId.SKILL_EVOLUTION:
        case eSpineCharacterAnimeId.SUMMON:
          if (index3 == -1)
          {
            string str = string.Format("{0:D6}_" + animeName + "{1:D1}", (object) unitId, (object) index1);
            return index2 == -1 ? str : string.Format("{0}_{1:D1}", (object) str, (object) index2);
          }
          string str1 = string.Format("{0:D6}_{2:D1}_" + animeName + "{1:D1}", (object) unitId, (object) index1, (object) index3);
          return index2 == -1 ? str1 : string.Format("{0}_{1:D1}", (object) str1, (object) index2);
        case eSpineCharacterAnimeId.PRINCESS_SKILL:
          if (index2 == -1)
            return index3 == -1 ? string.Format("{0}_p_{1}_skill", (object) unitId, (object) index1) : string.Format("{0}_{1}_p_{2}_skill", (object) unitId, (object) index3, (object) index1);
          if (index3 == -1)
            return string.Format("{0}_p_{1}_skill_{2}", (object) unitId, (object) index1, (object) index2);
          return string.Format("{0}_{1}_p_{2}_skill_{3}", (object) unitId, (object) index3, (object) index1, (object) index2);
        case eSpineCharacterAnimeId.PRINCESS_SKILL_EVOLUTION:
          if (index2 == -1)
            return index3 == -1 ? string.Format("{0}_p_{1}_skill_evolution", (object) unitId, (object) index1) : string.Format("{0}_{1}_p_{2}_skill_evolution", (object) unitId, (object) index3, (object) index1);
          if (index3 == -1)
            return string.Format("{0}_p_{1}_skill_evolution_{2}", (object) unitId, (object) index1, (object) index2);
          return string.Format("{0}_{1}_p_{2}_skill_evolution_{3}", (object) unitId, (object) index3, (object) index1, (object) index2);
        case eSpineCharacterAnimeId.COMBINE_JOY_RESULT:
          return string.Format("{0:D6}" + animeName + "{1:D6}", (object) unitId, (object) index1);
        case eSpineCharacterAnimeId.STORY_EYE_IDLE:
        case eSpineCharacterAnimeId.STORY_EYE_BLINK:
        case eSpineCharacterAnimeId.STORY_EYE_OPEN:
        case eSpineCharacterAnimeId.STORY_EYE_CLOSE:
        case eSpineCharacterAnimeId.STORY_MOUTH_IDLE:
        case eSpineCharacterAnimeId.STORY_MOUTH_TALK:
        case eSpineCharacterAnimeId.STORY_MOUTH_OPEN:
        case eSpineCharacterAnimeId.STORY_MOUTH_CLOSE:
          return animeName;
        case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT:
        case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_WALK:
        case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_ATTACK:
        case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_IDLE:
        case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_SUMMON:
        case eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_DIE:
        case eSpineCharacterAnimeId.TREASURE_EFFECT01:
        case eSpineCharacterAnimeId.TREASURE_EFFECT02:
        case eSpineCharacterAnimeId.TREASURE_EFFECT03:
        case eSpineCharacterAnimeId.TREASURE_EFFECT04:
        case eSpineCharacterAnimeId.TREASURE_EFFECT05:
        case eSpineCharacterAnimeId.TREASURE_EFFECT06:
        case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT01:
        case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT02:
        case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT03:
        case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT04:
        case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT05:
        case eSpineCharacterAnimeId.TREASURE_OUT_EFFECT06:
          return string.Format("{0:D7}_{1}", (object) index1, (object) animeName);
        default:
          return string.Empty;
      }
    }

    /*public static string GetRoomAnimeName(
      RoomSpineController _roomSpineController,
      eSpineCharacterAnimeId _animeId,
      eRoomPersonality _personality,
      eRoomAnimeTiming _animeTiming,
      eRoomAnimeDir _animeDir,
      int _animeIndex = -1)
    {
      string animeName = SpineConverterDefine.DataDic[_animeId].AnimeName;
      string str1 = _animeIndex != -1 ? string.Format("_{0:D6}", (object) _animeIndex) : SpineDefine.roomAnimePersonalityStrings[(int) _personality];
      string animeTimingString = SpineDefine.roomAnimeTimingStrings[(int) _animeTiming];
      string roomAnimeDirString = SpineDefine.roomAnimeDirStrings[(int) _animeDir];
      switch (_animeId)
      {
        case eSpineCharacterAnimeId.ROOM_IDOL:
        case eSpineCharacterAnimeId.ROOM_NOREACTION01:
        case eSpineCharacterAnimeId.ROOM_JUMP:
        case eSpineCharacterAnimeId.ROOM_WALK:
        case eSpineCharacterAnimeId.ROOM_APPEAR:
        case eSpineCharacterAnimeId.ROOM_DISAPPEAR:
        case eSpineCharacterAnimeId.ROOM_DISAPPEAR2:
        case eSpineCharacterAnimeId.ROOM_POSE01:
        case eSpineCharacterAnimeId.ROOM_POSE02:
        case eSpineCharacterAnimeId.ROOM_POSE03:
        case eSpineCharacterAnimeId.ROOM_POSE04:
        case eSpineCharacterAnimeId.ROOM_POSE05:
        case eSpineCharacterAnimeId.ROOM_POSE06:
        case eSpineCharacterAnimeId.ROOM_POSE07:
        case eSpineCharacterAnimeId.ROOM_POSE08:
        case eSpineCharacterAnimeId.ROOM_POSE09:
        case eSpineCharacterAnimeId.ROOM_POSE10:
        case eSpineCharacterAnimeId.ROOM_POSE11:
        case eSpineCharacterAnimeId.ROOM_POSE12:
        case eSpineCharacterAnimeId.ROOM_POSE13:
        case eSpineCharacterAnimeId.ROOM_POSE14:
        case eSpineCharacterAnimeId.ROOM_POSE15:
        case eSpineCharacterAnimeId.ROOM_POSE16:
        case eSpineCharacterAnimeId.ROOM_POSE17:
        case eSpineCharacterAnimeId.ROOM_POSE18:
        case eSpineCharacterAnimeId.ROOM_POSE19:
        case eSpineCharacterAnimeId.ROOM_POSE20:
        case eSpineCharacterAnimeId.ROOM_POSE21:
        case eSpineCharacterAnimeId.ROOM_POSE22:
        case eSpineCharacterAnimeId.ROOM_POSE23:
        case eSpineCharacterAnimeId.ROOM_POSE24:
        case eSpineCharacterAnimeId.ROOM_POSE25:
        case eSpineCharacterAnimeId.ROOM_POSE26:
        case eSpineCharacterAnimeId.ROOM_POSE27:
        case eSpineCharacterAnimeId.ROOM_POSE28:
        case eSpineCharacterAnimeId.ROOM_POSE29:
        case eSpineCharacterAnimeId.ROOM_POSE30:
        case eSpineCharacterAnimeId.ROOM_NOTICE:
        case eSpineCharacterAnimeId.ROOM_SHOCK:
        case eSpineCharacterAnimeId.ROOM_NOD:
        case eSpineCharacterAnimeId.ROOM_HANDCLAP:
        case eSpineCharacterAnimeId.ROOM_HIGHTOUCH01:
        case eSpineCharacterAnimeId.ROOM_SHAKEHAND01:
        case eSpineCharacterAnimeId.ROOM_SWING01:
        case eSpineCharacterAnimeId.ROOM_PRANK01:
        case eSpineCharacterAnimeId.ROOM_PRANK02:
        case eSpineCharacterAnimeId.ROOM_PRANK03:
        case eSpineCharacterAnimeId.ROOM_PRANK04:
        case eSpineCharacterAnimeId.ROOM_PRANK05:
        case eSpineCharacterAnimeId.ROOM_PRANK06:
        case eSpineCharacterAnimeId.ROOM_PRANK07:
        case eSpineCharacterAnimeId.ROOM_PRANK08:
        case eSpineCharacterAnimeId.ROOM_PRANK09:
        case eSpineCharacterAnimeId.ROOM_PRANK10:
        case eSpineCharacterAnimeId.ROOM_PRANK11:
        case eSpineCharacterAnimeId.ROOM_PRANK12:
        case eSpineCharacterAnimeId.ROOM_PRANK13:
        case eSpineCharacterAnimeId.ROOM_PRANK14:
        case eSpineCharacterAnimeId.ROOM_PRANK15:
        case eSpineCharacterAnimeId.ROOM_PRANK16:
        case eSpineCharacterAnimeId.ROOM_PRANK17:
        case eSpineCharacterAnimeId.ROOM_PRANK18:
        case eSpineCharacterAnimeId.ROOM_PRANK19:
        case eSpineCharacterAnimeId.ROOM_PRANK20:
        case eSpineCharacterAnimeId.ROOM_PRANK21:
        case eSpineCharacterAnimeId.ROOM_PRANK22:
        case eSpineCharacterAnimeId.ROOM_PRANK23:
        case eSpineCharacterAnimeId.ROOM_PRANK24:
        case eSpineCharacterAnimeId.ROOM_PRANK25:
        case eSpineCharacterAnimeId.ROOM_PRANK26:
        case eSpineCharacterAnimeId.ROOM_PRANK27:
        case eSpineCharacterAnimeId.ROOM_PRANK28:
        case eSpineCharacterAnimeId.ROOM_PRANK29:
        case eSpineCharacterAnimeId.ROOM_PRANK30:
        case eSpineCharacterAnimeId.ROOM_SMILE01:
        case eSpineCharacterAnimeId.ROOM_SMILE02:
        case eSpineCharacterAnimeId.ROOM_SMILE03:
        case eSpineCharacterAnimeId.ROOM_SMILE04:
        case eSpineCharacterAnimeId.ROOM_SMILE05:
          return animeName + str1 + roomAnimeDirString;
        case eSpineCharacterAnimeId.ROOM_RUN:
        case eSpineCharacterAnimeId.ROOM_CALL:
        case eSpineCharacterAnimeId.ROOM_GRAB:
        case eSpineCharacterAnimeId.ROOM_GRAB_PANIC:
          return animeName + str1 + animeTimingString + roomAnimeDirString;
        case eSpineCharacterAnimeId.NAVI:
          string str2 = animeName + string.Format("{0:D3}", (object) _animeIndex);
          using (Dictionary<string, Animation>.Enumerator enumerator = _roomSpineController.state.Data.SkeletonData.Animations.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<string, Animation> current = enumerator.Current;
              if (current.Key.Contains(str2))
                return current.Key;
            }
            break;
          }
        case eSpineCharacterAnimeId.PCT:
        case eSpineCharacterAnimeId.EKD_GLD_01:
        case eSpineCharacterAnimeId.EKD_GLD_02:
        case eSpineCharacterAnimeId.EKD_GLD_03:
        case eSpineCharacterAnimeId.EKD_GLD_04:
        case eSpineCharacterAnimeId.EKD_GLD_05:
        case eSpineCharacterAnimeId.EKD_GLD_06:
        case eSpineCharacterAnimeId.EKD_IDLE_ANGER:
        case eSpineCharacterAnimeId.EKD_IDLE_JOY:
        case eSpineCharacterAnimeId.EKD_IDLE_JOY2:
        case eSpineCharacterAnimeId.EKD_IDLE_NORMAL:
        case eSpineCharacterAnimeId.EKD_IDLE_SAD:
        case eSpineCharacterAnimeId.EKD_IDLE_SURPRISE1:
        case eSpineCharacterAnimeId.EKD_IDLE_SURPRISE2:
        case eSpineCharacterAnimeId.EKD_OTHER_01:
        case eSpineCharacterAnimeId.EKD_OTHER_02:
        case eSpineCharacterAnimeId.EKD_OTHER_03:
        case eSpineCharacterAnimeId.EKD_OTHER_04:
        case eSpineCharacterAnimeId.EKD_OTHER_05:
        case eSpineCharacterAnimeId.EKD_OTHER_06:
        case eSpineCharacterAnimeId.EKD_OTHER_07:
        case eSpineCharacterAnimeId.EKD_OTHER_08:
        case eSpineCharacterAnimeId.EKD_OTHER_09:
        case eSpineCharacterAnimeId.EKD_OTHER_10:
        case eSpineCharacterAnimeId.EKD_PARTY_01:
        case eSpineCharacterAnimeId.EKD_PARTY_02:
        case eSpineCharacterAnimeId.EKD_PARTY_03:
        case eSpineCharacterAnimeId.EKD_PARTY_04:
        case eSpineCharacterAnimeId.EKD_PARTY_05:
        case eSpineCharacterAnimeId.EKD_PARTY_06:
        case eSpineCharacterAnimeId.EKD_PARTY_07:
        case eSpineCharacterAnimeId.EKD_PARTY_08:
        case eSpineCharacterAnimeId.EKD_PARTY_09:
        case eSpineCharacterAnimeId.EKD_PARTY_10:
        case eSpineCharacterAnimeId.EKD_PSN_01:
        case eSpineCharacterAnimeId.EKD_PSN_02:
        case eSpineCharacterAnimeId.EKD_PSN_03:
        case eSpineCharacterAnimeId.EKD_PSN_04:
        case eSpineCharacterAnimeId.EKD_PSN_05:
        case eSpineCharacterAnimeId.EKD_PSN_06:
        case eSpineCharacterAnimeId.EKD_PSN_07:
        case eSpineCharacterAnimeId.EKD_PSN_08:
        case eSpineCharacterAnimeId.EKD_PSN_09:
        case eSpineCharacterAnimeId.EKD_PSN_10:
        case eSpineCharacterAnimeId.EKD_RUN:
        case eSpineCharacterAnimeId.EKD_SMILE:
        case eSpineCharacterAnimeId.EKD_SURPRISE:
        case eSpineCharacterAnimeId.EKD_TALK_ANGER:
        case eSpineCharacterAnimeId.EKD_TALK_JOY:
        case eSpineCharacterAnimeId.EKD_TALK_JOY2:
        case eSpineCharacterAnimeId.EKD_TALK_NORMAL:
        case eSpineCharacterAnimeId.EKD_TALK_SAD:
        case eSpineCharacterAnimeId.EKD_TALK_SURPRISE1:
        case eSpineCharacterAnimeId.EKD_TALK_SURPRISE2:
          return animeName;
      }
      return string.Empty;
    }

    public static string GetRoomItemAnimeName(
      int _animeId,
      eRoomPersonality _personality,
      int _animeIdx,
      eRoomAnimeTiming _animeTiming,
      eRoomAnimeDir _dirType,
      bool _isRandom,
      bool _isUniqueAnime)
    {
      string animeName = SpineConverterDefine.DataDic[eSpineCharacterAnimeId.ROOM_ITEM].AnimeName;
      string empty = string.Empty;
      string str = !_isUniqueAnime ? (!_isRandom ? SpineDefine.roomAnimePersonalityStrings[(int) _personality] : string.Format("{0}{1:D2}", (object) SpineDefine.roomAnimePersonalityStrings[(int) _personality], (object) _animeIdx)) : string.Format("_{0:D2}_unique", (object) _animeIdx);
      string animeTimingString = SpineDefine.roomAnimeTimingStrings[(int) _animeTiming];
      string roomAnimeDirString = SpineDefine.roomAnimeDirStrings[(int) _dirType];
      return string.Format("{0}{1:D6}{2}{3}{4}", (object) animeName, (object) _animeId, (object) str, (object) animeTimingString, (object) roomAnimeDirString);
    }

    public static string GetRoomSyncItemAnimeName(
      int _animeId,
      eRoomPersonality _personality,
      int _patternIndex,
      int _animeIdx,
      eRoomAnimeTiming _animeTiming,
      bool _isRandom)
    {
      string animeName = SpineConverterDefine.DataDic[eSpineCharacterAnimeId.ROOM_SYNC].AnimeName;
      string empty = string.Empty;
      string str = !_isRandom ? SpineDefine.roomAnimePersonalityStrings[(int) _personality] : string.Format("{0}{1:D2}", (object) SpineDefine.roomAnimePersonalityStrings[(int) _personality], (object) _animeIdx);
      string animeTimingString = SpineDefine.roomAnimeTimingStrings[(int) _animeTiming];
      string animePatternString = SpineDefine.roomAnimePatternStrings[_patternIndex];
      return string.Format("{0}{1:D6}{2}{3}{4}", (object) animeName, (object) _animeId, (object) animePatternString, (object) str, (object) animeTimingString);
    }

    public static string GetRoomTrackItemAnimeName(
      int _animeId,
      eRoomPersonality _personality,
      int _animeIdx,
      bool _isRandom)
    {
      string animeName = SpineConverterDefine.DataDic[eSpineCharacterAnimeId.ROOM_TRACK].AnimeName;
      string empty = string.Empty;
      string str = !_isRandom ? SpineDefine.roomAnimePersonalityStrings[(int) _personality] : string.Format("{0}{1:D2}", (object) SpineDefine.roomAnimePersonalityStrings[(int) _personality], (object) _animeIdx);
      return string.Format("{0}{1:D6}{2}", (object) animeName, (object) _animeId, (object) str);
    }*/

    public enum eSpecialSleepAnimeId
    {
      START,
      LOOP,
      RELEASE,
    }
  }
}
