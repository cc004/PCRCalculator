// Decompiled with JetBrains decompiler
// Type: Elements.SpineConverterDefine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Elements
{
  public class SpineConverterDefine
  {
    public const int NYXMK2_UNITID = 192101;

    public static Dictionary<eSpineCharacterAnimeId, ConverterData> DataDic { get; set; } = new Dictionary<eSpineCharacterAnimeId, ConverterData>
    {
      {
        eSpineCharacterAnimeId.RARITYUP_POSING,
        new ConverterData("rarityup_posing", eSpineConvertCategoryType.NORMAL, POSING_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.MANA_IDLE,
        new ConverterData("mana_idle", eSpineConvertCategoryType.NORMAL, POSING_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.MANA_JUMP,
        new ConverterData("mana_jump", eSpineConvertCategoryType.NORMAL, POSING_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STAMINA_BOW,
        new ConverterData("stamina_karin_ojigi", eSpineConvertCategoryType.NORMAL, POSING_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.FRIEND_HIGHTOUCH,
        new ConverterData("friend_hightouch", eSpineConvertCategoryType.NORMAL, POSING_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.DEAR_IDOL,
        new ConverterData("dear_idol", eSpineConvertCategoryType.NORMAL, DEAR_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.DEAR_JUMP,
        new ConverterData("dear_jump", eSpineConvertCategoryType.NORMAL, DEAR_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.DEAR_SMILE,
        new ConverterData("dear_smile", eSpineConvertCategoryType.NORMAL, DEAR_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RACE_EAT_JUN,
        new ConverterData("eat_jun", eSpineConvertCategoryType.NORMAL, RACE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RACE_EAT_NORMAL,
        new ConverterData("eat_normal", eSpineConvertCategoryType.NORMAL, RACE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RACE_RUN_SPRING_JUMP,
        new ConverterData("run_springJump", eSpineConvertCategoryType.NORMAL, RACE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_FLYING_DOWN,
        new ConverterData("balloon_flying_down", eSpineConvertCategoryType.NORMAL, RACE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_FLYING_LOOP,
        new ConverterData("balloon_flying_loop", eSpineConvertCategoryType.NORMAL, RACE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_FLYING_UP,
        new ConverterData("balloon_flying_up", eSpineConvertCategoryType.NORMAL, RACE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_IN,
        new ConverterData("balloon_in", eSpineConvertCategoryType.NORMAL, RACE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_OUT,
        new ConverterData("balloon_out", eSpineConvertCategoryType.NORMAL, RACE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RACE_RUN,
        new ConverterData("run_race", eSpineConvertCategoryType.NORMAL, RACE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RUN_JUMP,
        new ConverterData("run_jump", eSpineConvertCategoryType.NORMAL, RUN_JUMP_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RUN_HIGH_JUMP,
        new ConverterData("run_highJump", eSpineConvertCategoryType.NORMAL, RUN_JUMP_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.NO_WEAPON_IDLE,
        new ConverterData("noWeapon_idle", eSpineConvertCategoryType.NORMAL, NO_WEAPON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.NO_WEAPON_JOY_SHORT,
        new ConverterData("noWeapon_joy_short", eSpineConvertCategoryType.NORMAL, NO_WEAPON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.NO_WEAPON_RUN,
        new ConverterData("noWeapon_run", eSpineConvertCategoryType.NORMAL, NO_WEAPON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.PCT,
        new ConverterData("pct_", eSpineConvertCategoryType.MINIGAME, MINIGAME_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.FKE_IDLE,
        new ConverterData("fke_kuka_idle", eSpineConvertCategoryType.MINIGAME, MINIGAME_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.FKE,
        new ConverterData("fke_", eSpineConvertCategoryType.MINIGAME, MINIGAME_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.PKB,
        new ConverterData("pkb_", eSpineConvertCategoryType.MINIGAME, MINIGAME_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_01,
        new ConverterData("ekd_gld_01", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_02,
        new ConverterData("ekd_gld_02", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_03,
        new ConverterData("ekd_gld_03", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_04,
        new ConverterData("ekd_gld_04", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_05,
        new ConverterData("ekd_gld_05", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_06,
        new ConverterData("ekd_gld_06", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_ANGER,
        new ConverterData("ekd_idle_anger", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_JOY,
        new ConverterData("ekd_idle_joy", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_JOY2,
        new ConverterData("ekd_idle_joy2", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_NORMAL,
        new ConverterData("ekd_idle_normal", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_SAD,
        new ConverterData("ekd_idle_sad", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_SURPRISE1,
        new ConverterData("ekd_idle_surprise1", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_SURPRISE2,
        new ConverterData("ekd_idle_surprise2", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_01,
        new ConverterData("ekd_other_01", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_02,
        new ConverterData("ekd_other_02", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_03,
        new ConverterData("ekd_other_03", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_04,
        new ConverterData("ekd_other_04", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_05,
        new ConverterData("ekd_other_05", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_06,
        new ConverterData("ekd_other_06", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_07,
        new ConverterData("ekd_other_07", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_08,
        new ConverterData("ekd_other_08", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_09,
        new ConverterData("ekd_other_09", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_10,
        new ConverterData("ekd_other_10", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_01,
        new ConverterData("ekd_party_01", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_02,
        new ConverterData("ekd_party_02", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_03,
        new ConverterData("ekd_party_03", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_04,
        new ConverterData("ekd_party_04", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_05,
        new ConverterData("ekd_party_05", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_06,
        new ConverterData("ekd_party_06", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_07,
        new ConverterData("ekd_party_07", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_08,
        new ConverterData("ekd_party_08", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_09,
        new ConverterData("ekd_party_09", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_10,
        new ConverterData("ekd_party_10", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_01,
        new ConverterData("ekd_psn_01", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_02,
        new ConverterData("ekd_psn_02", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_03,
        new ConverterData("ekd_psn_03", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_04,
        new ConverterData("ekd_psn_04", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_05,
        new ConverterData("ekd_psn_05", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_06,
        new ConverterData("ekd_psn_06", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_07,
        new ConverterData("ekd_psn_07", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_08,
        new ConverterData("ekd_psn_08", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_09,
        new ConverterData("ekd_psn_09", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_10,
        new ConverterData("ekd_psn_10", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_RUN,
        new ConverterData("ekd_run", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_SMILE,
        new ConverterData("ekd_smile", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_SURPRISE,
        new ConverterData("ekd_surprise", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_ANGER,
        new ConverterData("ekd_talk_anger", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_JOY,
        new ConverterData("ekd_talk_joy", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_JOY2,
        new ConverterData("ekd_talk_joy2", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_NORMAL,
        new ConverterData("ekd_talk_normal", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_SAD,
        new ConverterData("ekd_talk_sad", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_SURPRISE1,
        new ConverterData("ekd_talk_surprise1", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_SURPRISE2,
        new ConverterData("ekd_talk_surprise2", eSpineConvertCategoryType.EKD, EKD_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.OMP,
        new ConverterData("omp_", eSpineConvertCategoryType.OMP, OMP_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.UEK,
        new ConverterData("uek_", eSpineConvertCategoryType.UEK, UEK_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.NYX,
        new ConverterData("nyx_", eSpineConvertCategoryType.NYX, NYX_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.IDLE_MAP_S,
        new ConverterData("idle_Map_s", eSpineConvertCategoryType.NORMAL, MAP_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.IDLE_MAP,
        new ConverterData("idle_Map", eSpineConvertCategoryType.NORMAL, MAP_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.AWAKE_MAP_RELEASE,
        new ConverterData("awake_Map_release", eSpineConvertCategoryType.NORMAL, MAP_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.AWAKE_MAP,
        new ConverterData("awake_Map", eSpineConvertCategoryType.NORMAL, MAP_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.IDLE,
        new ConverterData("idle", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.IDLE_MULTI_TARGET,
        new ConverterData("idle_multi_target_", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET,
        new ConverterData("damage_multi_target_", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.WALK,
        new ConverterData("walk", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.JOY_SHORT,
        new ConverterData("joy_short", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.JOY_LONG,
        new ConverterData("joy_long", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.IDLE_STAND_BY,
        new ConverterData("multi_idle_standBy", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.IDLE_NO_WEAPON,
        new ConverterData("multi_idle_noWeapon", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RUN_GAME_START,
        new ConverterData("run_gamestart", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.COOP_STAND_BY,
        new ConverterData("multi_standBy", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.RUN,
        new ConverterData("run", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STAND_BY,
        new ConverterData("standBy", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.LANDING,
        new ConverterData("landing", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.IDLE_SKIP,
        new ConverterData("idle_Skip", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.DAMAGE_SKIP,
        new ConverterData("damage_Skip", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.LOADING,
        new ConverterData("loading", eSpineConvertCategoryType.NORMAL, LOADING_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ANNIHILATION,
        new ConverterData("annihilation", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SLEEP,
        new ConverterData("sleep", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EVENT_BANNER_MOTION,
        new ConverterData("event_banner_motion", eSpineConvertCategoryType.NORMAL, EVENT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EVENT_BANNER_MOTION_CIRCLE,
        new ConverterData("event_banner_motion_circle", eSpineConvertCategoryType.NORMAL, EVENT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SMILE,
        new ConverterData("smile", eSpineConvertCategoryType.NORMAL, SMILE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.OWN_DIE,
        new ConverterData("die", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ATTACK,
        new ConverterData("attack", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.DAMEGE,
        new ConverterData("damage", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.DIE,
        new ConverterData("die", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.DEFEAT,
        new ConverterData("defeat", eSpineConvertCategoryType.NORMAL, DEFEAT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.DIE_LOOP,
        new ConverterData("die_loop", eSpineConvertCategoryType.NORMAL, COMMON_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.COMBINE_JOY_RESULT,
        new ConverterData("_joy_combine_", eSpineConvertCategoryType.NORMAL, COMBINE_JOY_RESULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SKILL,
        new ConverterData("skill", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.PRINCESS_SKILL,
        new ConverterData("p", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.PRINCESS_SKILL_EVOLUTION,
        new ConverterData("p", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SKILL_EVOLUTION,
        new ConverterData("skill_evolution", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SPECIAL_SKILL_EVOLUTION,
        new ConverterData("skillSp_evolution", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.JOY_RESULT,
        new ConverterData("joyResult", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SPECIAL_SKILL,
        new ConverterData("skillSp", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.OWN_IDLE,
        new ConverterData("idle", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.OWN_WALK,
        new ConverterData("walk", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.OWN_JOY,
        new ConverterData("joy", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.OWN_ATTACK,
        new ConverterData("attack", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.OWN_DAMEGE,
        new ConverterData("damage", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SUMMON,
        new ConverterData("summon", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.GAME_START,
        new ConverterData("gamestart", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.AWAKE,
        new ConverterData("awake", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ATTACK_SKIPQUEST,
        new ConverterData("attack_skipQuest", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.IDLE_RESULT,
        new ConverterData("idle_Result", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.AWAKE_RESULT,
        new ConverterData("awake_Result", eSpineConvertCategoryType.NORMAL, BATTLE_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.EVENT_STORY,
        new ConverterData("event_", eSpineConvertCategoryType.NORMAL, EVENT_STORY_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_IDOL,
        new ConverterData("cmn_idol", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_JUMP,
        new ConverterData("cmn_jump", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_WALK,
        new ConverterData("cmn_walk", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_RUN,
        new ConverterData("cmn_run", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_APPEAR,
        new ConverterData("cmn_appear", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_DISAPPEAR,
        new ConverterData("cmn_disappear", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_DISAPPEAR2,
        new ConverterData("cmn_disappear2", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE01,
        new ConverterData("cmn_pose01", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE02,
        new ConverterData("cmn_pose02", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE03,
        new ConverterData("cmn_pose03", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE04,
        new ConverterData("cmn_pose04", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE05,
        new ConverterData("cmn_pose05", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE06,
        new ConverterData("cmn_pose06", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE07,
        new ConverterData("cmn_pose07", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE08,
        new ConverterData("cmn_pose08", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE09,
        new ConverterData("cmn_pose09", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE10,
        new ConverterData("cmn_pose10", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE11,
        new ConverterData("cmn_pose11", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE12,
        new ConverterData("cmn_pose12", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE13,
        new ConverterData("cmn_pose13", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE14,
        new ConverterData("cmn_pose14", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE15,
        new ConverterData("cmn_pose15", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE16,
        new ConverterData("cmn_pose16", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE17,
        new ConverterData("cmn_pose17", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE18,
        new ConverterData("cmn_pose18", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE19,
        new ConverterData("cmn_pose19", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE20,
        new ConverterData("cmn_pose20", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE21,
        new ConverterData("cmn_pose21", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE22,
        new ConverterData("cmn_pose22", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE23,
        new ConverterData("cmn_pose23", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE24,
        new ConverterData("cmn_pose24", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE25,
        new ConverterData("cmn_pose25", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE26,
        new ConverterData("cmn_pose26", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE27,
        new ConverterData("cmn_pose27", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE28,
        new ConverterData("cmn_pose28", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE29,
        new ConverterData("cmn_pose29", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE30,
        new ConverterData("cmn_pose30", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_CALL,
        new ConverterData("cmn_call", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_NOTICE,
        new ConverterData("cmn_notice", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_SHOCK,
        new ConverterData("cmn_shock", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_NOD,
        new ConverterData("cmn_nod", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_HANDCLAP,
        new ConverterData("cmn_handclap", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_GRAB,
        new ConverterData("cmn_grab01", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_GRAB_PANIC,
        new ConverterData("cmn_grab02", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE01,
        new ConverterData("cmn_smile01", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE02,
        new ConverterData("cmn_smile02", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE03,
        new ConverterData("cmn_smile03", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE04,
        new ConverterData("cmn_smile04", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE05,
        new ConverterData("cmn_smile05", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_NOREACTION01,
        new ConverterData("cmn_noReaction01", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_HIGHTOUCH01,
        new ConverterData("cmn_highTouch01", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_SHAKEHAND01,
        new ConverterData("cmn_shakeHand01", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_SWING01,
        new ConverterData("cmn_swing01", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK01,
        new ConverterData("cmn_prank01", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK02,
        new ConverterData("cmn_prank02", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK03,
        new ConverterData("cmn_prank03", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK04,
        new ConverterData("cmn_prank04", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK05,
        new ConverterData("cmn_prank05", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK06,
        new ConverterData("cmn_prank06", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK07,
        new ConverterData("cmn_prank07", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK08,
        new ConverterData("cmn_prank08", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK09,
        new ConverterData("cmn_prank09", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK10,
        new ConverterData("cmn_prank10", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK11,
        new ConverterData("cmn_prank11", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK12,
        new ConverterData("cmn_prank12", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK13,
        new ConverterData("cmn_prank13", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK14,
        new ConverterData("cmn_prank14", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK15,
        new ConverterData("cmn_prank15", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK16,
        new ConverterData("cmn_prank16", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK17,
        new ConverterData("cmn_prank17", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK18,
        new ConverterData("cmn_prank18", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK19,
        new ConverterData("cmn_prank19", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK20,
        new ConverterData("cmn_prank20", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK21,
        new ConverterData("cmn_prank21", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK22,
        new ConverterData("cmn_prank22", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK23,
        new ConverterData("cmn_prank23", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK24,
        new ConverterData("cmn_prank24", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK25,
        new ConverterData("cmn_prank25", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK26,
        new ConverterData("cmn_prank26", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK27,
        new ConverterData("cmn_prank27", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK28,
        new ConverterData("cmn_prank28", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK29,
        new ConverterData("cmn_prank29", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK30,
        new ConverterData("cmn_prank30", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_ITEM,
        new ConverterData("itm_", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_SYNC,
        new ConverterData("sync_", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.ROOM_TRACK,
        new ConverterData("track_", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.NAVI,
        new ConverterData("navi_", eSpineConvertCategoryType.ROOM, COMMON_ROOM_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STORY_EYE_IDLE,
        new ConverterData("eye_idle", eSpineConvertCategoryType.STILL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STORY_EYE_BLINK,
        new ConverterData("eye_blink", eSpineConvertCategoryType.STILL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STORY_EYE_OPEN,
        new ConverterData("eye_open", eSpineConvertCategoryType.STILL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STORY_EYE_CLOSE,
        new ConverterData("eye_close", eSpineConvertCategoryType.STILL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STORY_MOUTH_IDLE,
        new ConverterData("mouth_idle", eSpineConvertCategoryType.STILL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STORY_MOUTH_TALK,
        new ConverterData("mouth_talk", eSpineConvertCategoryType.STILL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STORY_MOUTH_OPEN,
        new ConverterData("mouth_open", eSpineConvertCategoryType.STILL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.STORY_MOUTH_CLOSE,
        new ConverterData("mouth_close", eSpineConvertCategoryType.STILL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT,
        new ConverterData("effect", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_ATTACK,
        new ConverterData("effect_attack", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_WALK,
        new ConverterData("effect_walk", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_IDLE,
        new ConverterData("effect_idle", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_SUMMON,
        new ConverterData("effect_summon", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_DIE,
        new ConverterData("effect_die", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT01,
        new ConverterData("effect", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT02,
        new ConverterData("effect_2", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT03,
        new ConverterData("effect_3", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT04,
        new ConverterData("effect_4", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT05,
        new ConverterData("effect_5", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT06,
        new ConverterData("effect_6", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT01,
        new ConverterData("effect_out", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT02,
        new ConverterData("effect_2_out", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT03,
        new ConverterData("effect_3_out", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT04,
        new ConverterData("effect_4_out", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT05,
        new ConverterData("effect_4_out", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT06,
        new ConverterData("effect_4_out", eSpineConvertCategoryType.NORMAL, DEFAULT_CONVERT_FUNC)
      }
    };

    public static string GetFileName(eSpineBinaryAnimationId binaryId, int index1, int index2)
    {
      switch (binaryId)
      {
        case eSpineBinaryAnimationId.COMMON_BATTLE:
          return string.Format("{0:D2}_COMMON_BATTLE", index1);
        case eSpineBinaryAnimationId.COMMON_ROOM:
          return "ROOM_SPINEUNIT_ANIMATION_COMMON";
        case eSpineBinaryAnimationId.UNIQUE_ROOM:
          return string.Format("ROOM_SPINEUNIT_ANIMATION_{0:D6}", index1);
        case eSpineBinaryAnimationId.BATTLE:
          return string.Format("{0:D2}_BATTLE", index1);
        case eSpineBinaryAnimationId.ROOM_ITEM:
          return string.Format("ROOM_SPINEUNIT_ANIMATION_{0:D6}", index1);
        case eSpineBinaryAnimationId.ROOM_ITEM_UNIQUE:
          return string.Format("ROOM_SPINEUNIT_ANIMATION_{0:D6}_{1:D6}", index1, index2);
        case eSpineBinaryAnimationId.ROOM_SYNC:
          return string.Format("ROOM_SYNC_ANIME_{0:D6}", index1);
        case eSpineBinaryAnimationId.ROOM_SYNC_UNIQUE:
          return string.Format("ROOM_SYNC_ANIME_{0:D6}_{1:D6}", index1, index2);
        case eSpineBinaryAnimationId.ROOM_TRACK:
          return string.Format("ROOM_TRACK_ANIME_{0:D6}", index1);
        case eSpineBinaryAnimationId.ROOM_TRACK_UNIQUE:
          return string.Format("ROOM_TRACK_ANIME_{0:D6}_{1:D6}", index1, index2);
        case eSpineBinaryAnimationId.POSING:
          return string.Format("{0:D6}_POSING", index1);
        case eSpineBinaryAnimationId.SMILE:
          return string.Format("{0:D6}_SMILE", index1);
        case eSpineBinaryAnimationId.LOADING:
          return string.Format("{0:D2}_LOADING", index1);
        case eSpineBinaryAnimationId.EVENT:
          return string.Format("EVENT", index1);
        case eSpineBinaryAnimationId.EVENT_STORY:
          return string.Format("EVENT_STORY_{0:D5}", index1);
        case eSpineBinaryAnimationId.DEFEAT:
          return string.Format("{0:D6}_DEFEAT", index1);
        case eSpineBinaryAnimationId.MAP:
          return string.Format("{0:D6}_MAP", index1);
        case eSpineBinaryAnimationId.MINIGAME:
          return string.Format("MINIGAME_{0:D4}", index1);
        case eSpineBinaryAnimationId.RUN_JUMP:
          return string.Format("{0:D6}_RUN_JUMP", index1);
        case eSpineBinaryAnimationId.NO_WEAPON:
          return string.Format("{0:D6}_NO_WEAPON", index1);
        case eSpineBinaryAnimationId.RACE:
          return string.Format("{0:D6}_RACE", index1);
        case eSpineBinaryAnimationId.COMBINE_JOY_RESULT:
          return string.Format("COMBINE_JOY_RESULT_{0:D6}", index1);
        case eSpineBinaryAnimationId.DEAR:
          return string.Format("{0:D6}_DEAR", index1);
        case eSpineBinaryAnimationId.EKD:
          return "SPINE_ANIME_EKD";
        case eSpineBinaryAnimationId.NAVI:
          return string.Format("NAVI_{0:D3}", index1);
        case eSpineBinaryAnimationId.OMP:
          return "SPINE_ANIME_OMP";
        case eSpineBinaryAnimationId.UEK:
          return string.Format("SPINE_ANIME_UEK_{0:D2}", index1);
        case eSpineBinaryAnimationId.NYX:
          return "SPINE_ANIME_NYX";
        case eSpineBinaryAnimationId.PKB:
          return "SPINE_ANIME_PKB";
        default:
          return "NotCategory";
      }
    }

    private static int extractionPrefixId6(string _extractionString)
    {
      int result = -1;
      if (Regex.IsMatch(_extractionString, "^[0-9]{6,6}_"))
        int.TryParse(_extractionString.Substring(0, 6), out result);
      return result;
    }

    private static int extractionPrefixId2(string _extractionString)
    {
      int result = -1;
      if (Regex.IsMatch(_extractionString, "^[0-9]{2,2}_"))
        int.TryParse(_extractionString.Substring(0, 2), out result);
      return result;
    }

    private static void DEFAULT_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId = 0,
      string _checkStr = "",
      string _animeName = "",
      Func<int, bool> _isUniqueMotion = null)
    {
      _binaryId = eSpineBinaryAnimationId.NONE;
      _id = -1;
    }

    private static void COMMON_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      int num1 = extractionPrefixId6(_checkStr);
      int num2 = extractionPrefixId2(_checkStr);
      if (num1 == -1 && num2 == -1)
        return;
      if (UnitUtility.IsEnemyUnit(_atlasFileUnitId))
      {
        _id = _atlasFileUnitId;
        _binaryId = eSpineBinaryAnimationId.BATTLE;
      }
      else if (num2 != -1)
      {
        _id = num2;
        _binaryId = eSpineBinaryAnimationId.COMMON_BATTLE;
      }
      else if (_isUniqueMotion(num1))
      {
        _id = num1;
        _binaryId = eSpineBinaryAnimationId.COMMON_BATTLE;
      }
      else
      {
        _id = _atlasFileUnitId;
        _binaryId = eSpineBinaryAnimationId.BATTLE;
      }
    }

    private static void LOADING_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      int num1 = extractionPrefixId6(_checkStr);
      int num2 = extractionPrefixId2(_checkStr);
      if (num1 == -1 && num2 == -1 || UnitUtility.IsEnemyUnit(_atlasFileUnitId))
        return;
      if (num2 != -1)
      {
        _id = num2;
        _binaryId = eSpineBinaryAnimationId.LOADING;
      }
      else
      {
        if (num1 == -1)
          return;
        _id = num1;
        _binaryId = eSpineBinaryAnimationId.LOADING;
      }
    }

    private static void COMBINE_JOY_RESULT_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!Regex.IsMatch(_checkStr, "[0-9]{6,6}_joy_combine_[0-9]{6,6}") || !int.TryParse(_checkStr.Substring(19, 6), out _id))
        return;
      _binaryId = eSpineBinaryAnimationId.COMBINE_JOY_RESULT;
    }

    private static void BATTLE_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      int num1 = extractionPrefixId6(_checkStr);
      int num2 = extractionPrefixId2(_checkStr);
      if (num1 == -1 && num2 == -1)
        return;
      if (UnitUtility.IsEnemyUnit(_atlasFileUnitId))
      {
        _id = _atlasFileUnitId;
        _binaryId = eSpineBinaryAnimationId.BATTLE;
      }
      else if (num2 != -1)
      {
        _id = num2;
        _binaryId = eSpineBinaryAnimationId.COMMON_BATTLE;
      }
      else
      {
        _id = num1;
        _binaryId = eSpineBinaryAnimationId.BATTLE;
      }
    }

    private static void COMMON_ROOM_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      _id = 0;
      if (Regex.IsMatch(_checkStr, "sync_[0-9]{6,6}_[A-B]"))
      {
        if (!int.TryParse(_checkStr.Substring(5, 6), out _id))
          return;
        _binaryId = eSpineBinaryAnimationId.ROOM_SYNC;
      }
      else if (Regex.IsMatch(_checkStr, "track_[0-9]{6,6}"))
      {
        if (!int.TryParse(_checkStr.Substring(6, 6), out _id))
          return;
        _binaryId = eSpineBinaryAnimationId.ROOM_TRACK;
      }
      else if (Regex.IsMatch(_checkStr, "itm_[0-9]{6,6}_"))
      {
        if (!int.TryParse(_checkStr.Substring(4, 6), out _id))
          return;
        _binaryId = eSpineBinaryAnimationId.ROOM_ITEM;
      }
      else if (_checkStr.Contains("nebbia"))
      {
        _binaryId = eSpineBinaryAnimationId.UNIQUE_ROOM;
        _id = _atlasFileUnitId;
      }
      else if (_checkStr.Contains(192101.ToString()))
      {
        _binaryId = eSpineBinaryAnimationId.UNIQUE_ROOM;
        _id = 192101;
      }
      else if (_checkStr.Contains("cmn_"))
      {
        _binaryId = eSpineBinaryAnimationId.COMMON_ROOM;
      }
      else
      {
        if (!Regex.IsMatch(_checkStr, "navi_[0-9]{3,3}_") || !int.TryParse(_checkStr.Substring(5, 3), out _id))
          return;
        _binaryId = eSpineBinaryAnimationId.NAVI;
      }
    }

    private static void POSING_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.POSING;
      int num = extractionPrefixId6(_checkStr);
      _id = num == -1 ? 0 : num;
    }

    private static void DEFEAT_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.DEFEAT;
      int num = extractionPrefixId6(_checkStr);
      _id = num == -1 ? 0 : num;
    }

    private static void EVENT_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.EVENT;
      _id = 0;
    }

    private static void EVENT_STORY_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      _id = 0;
      if (!Regex.IsMatch(_checkStr, "event_[0-9]{2}_[0-9]{5}_") || !int.TryParse(_checkStr.Substring(9, 5), out _id))
        return;
      _binaryId = eSpineBinaryAnimationId.EVENT_STORY;
    }

    private static void SMILE_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.SMILE;
      int num = extractionPrefixId6(_checkStr);
      _id = num == -1 ? 0 : num;
    }

    private static void MAP_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.MAP;
      _id = _atlasFileUnitId;
    }

    private static void MINIGAME_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.MINIGAME;
      if (_checkStr.Contains("pct_"))
        _id = 1001;
      else if (_checkStr.Contains("fke_"))
      {
        _id = 1002;
      }
      else
      {
        if (!_checkStr.Contains("pkb_"))
          return;
        _id = 1006;
      }
    }

    private static void EKD_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.EKD;
      _id = 0;
    }

    private static void OMP_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.OMP;
      _id = 0;
    }

    private static void UEK_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.UEK;
      _id = extractionPrefixId2(_checkStr);
    }

    private static void NYX_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.NYX;
      _id = 0;
    }

    private static void RUN_JUMP_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.RUN_JUMP;
      int num = extractionPrefixId6(_checkStr);
      _id = num == -1 ? 0 : num;
    }

    private static void NO_WEAPON_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.NO_WEAPON;
      int num = extractionPrefixId6(_checkStr);
      _id = num == -1 ? 0 : num;
    }

    private static void RACE_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.RACE;
      int num = extractionPrefixId6(_checkStr);
      _id = num == -1 ? 0 : num;
    }

    private static void DEAR_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.DEAR;
      int num = extractionPrefixId6(_checkStr);
      _id = num == -1 ? 0 : num;
    }

    public delegate void CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion);

    public class ConverterData
    {
      public string AnimeName { get; private set; }

      public eSpineConvertCategoryType SpineConvertCategoryType { get; private set; }

      private CONVERT_FUNC animationCheck { get; set; }

      public ConverterData(
        string _setAnimeName,
        eSpineConvertCategoryType _setSpineConvertCategoryType,
        CONVERT_FUNC _setAnimationCheck)
      {
        AnimeName = _setAnimeName;
        SpineConvertCategoryType = _setSpineConvertCategoryType;
        animationCheck = _setAnimationCheck;
      }

      public void Check(
        out eSpineBinaryAnimationId _spineBinaryAnimationId,
        out int _id,
        int _atlasFileUnitId,
        string _checkStr,
        Func<int, bool> _isUniqueMotion) => animationCheck(out _spineBinaryAnimationId, out _id, _atlasFileUnitId, _checkStr, AnimeName, _isUniqueMotion);
    }
  }
}
