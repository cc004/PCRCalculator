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

    public static Dictionary<eSpineCharacterAnimeId, SpineConverterDefine.ConverterData> DataDic { get; set; } = new Dictionary<eSpineCharacterAnimeId, SpineConverterDefine.ConverterData>()
    {
      {
        eSpineCharacterAnimeId.RARITYUP_POSING,
        new SpineConverterDefine.ConverterData("rarityup_posing", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.POSING_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.MANA_IDLE,
        new SpineConverterDefine.ConverterData("mana_idle", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.POSING_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.MANA_JUMP,
        new SpineConverterDefine.ConverterData("mana_jump", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.POSING_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STAMINA_BOW,
        new SpineConverterDefine.ConverterData("stamina_karin_ojigi", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.POSING_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.FRIEND_HIGHTOUCH,
        new SpineConverterDefine.ConverterData("friend_hightouch", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.POSING_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.DEAR_IDOL,
        new SpineConverterDefine.ConverterData("dear_idol", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEAR_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.DEAR_JUMP,
        new SpineConverterDefine.ConverterData("dear_jump", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEAR_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.DEAR_SMILE,
        new SpineConverterDefine.ConverterData("dear_smile", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEAR_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RACE_EAT_JUN,
        new SpineConverterDefine.ConverterData("eat_jun", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RACE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RACE_EAT_NORMAL,
        new SpineConverterDefine.ConverterData("eat_normal", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RACE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RACE_RUN_SPRING_JUMP,
        new SpineConverterDefine.ConverterData("run_springJump", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RACE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_FLYING_DOWN,
        new SpineConverterDefine.ConverterData("balloon_flying_down", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RACE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_FLYING_LOOP,
        new SpineConverterDefine.ConverterData("balloon_flying_loop", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RACE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_FLYING_UP,
        new SpineConverterDefine.ConverterData("balloon_flying_up", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RACE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_IN,
        new SpineConverterDefine.ConverterData("balloon_in", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RACE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RACE_BALLOON_OUT,
        new SpineConverterDefine.ConverterData("balloon_out", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RACE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RACE_RUN,
        new SpineConverterDefine.ConverterData("run_race", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RACE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RUN_JUMP,
        new SpineConverterDefine.ConverterData("run_jump", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RUN_JUMP_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RUN_HIGH_JUMP,
        new SpineConverterDefine.ConverterData("run_highJump", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.RUN_JUMP_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.NO_WEAPON_IDLE,
        new SpineConverterDefine.ConverterData("noWeapon_idle", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.NO_WEAPON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.NO_WEAPON_JOY_SHORT,
        new SpineConverterDefine.ConverterData("noWeapon_joy_short", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.NO_WEAPON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.NO_WEAPON_RUN,
        new SpineConverterDefine.ConverterData("noWeapon_run", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.NO_WEAPON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.PCT,
        new SpineConverterDefine.ConverterData("pct_", eSpineConvertCategoryType.MINIGAME, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.MINIGAME_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.FKE_IDLE,
        new SpineConverterDefine.ConverterData("fke_kuka_idle", eSpineConvertCategoryType.MINIGAME, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.MINIGAME_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.FKE,
        new SpineConverterDefine.ConverterData("fke_", eSpineConvertCategoryType.MINIGAME, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.MINIGAME_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.PKB,
        new SpineConverterDefine.ConverterData("pkb_", eSpineConvertCategoryType.MINIGAME, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.MINIGAME_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_01,
        new SpineConverterDefine.ConverterData("ekd_gld_01", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_02,
        new SpineConverterDefine.ConverterData("ekd_gld_02", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_03,
        new SpineConverterDefine.ConverterData("ekd_gld_03", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_04,
        new SpineConverterDefine.ConverterData("ekd_gld_04", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_05,
        new SpineConverterDefine.ConverterData("ekd_gld_05", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_GLD_06,
        new SpineConverterDefine.ConverterData("ekd_gld_06", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_ANGER,
        new SpineConverterDefine.ConverterData("ekd_idle_anger", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_JOY,
        new SpineConverterDefine.ConverterData("ekd_idle_joy", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_JOY2,
        new SpineConverterDefine.ConverterData("ekd_idle_joy2", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_NORMAL,
        new SpineConverterDefine.ConverterData("ekd_idle_normal", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_SAD,
        new SpineConverterDefine.ConverterData("ekd_idle_sad", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_SURPRISE1,
        new SpineConverterDefine.ConverterData("ekd_idle_surprise1", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_IDLE_SURPRISE2,
        new SpineConverterDefine.ConverterData("ekd_idle_surprise2", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_01,
        new SpineConverterDefine.ConverterData("ekd_other_01", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_02,
        new SpineConverterDefine.ConverterData("ekd_other_02", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_03,
        new SpineConverterDefine.ConverterData("ekd_other_03", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_04,
        new SpineConverterDefine.ConverterData("ekd_other_04", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_05,
        new SpineConverterDefine.ConverterData("ekd_other_05", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_06,
        new SpineConverterDefine.ConverterData("ekd_other_06", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_07,
        new SpineConverterDefine.ConverterData("ekd_other_07", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_08,
        new SpineConverterDefine.ConverterData("ekd_other_08", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_09,
        new SpineConverterDefine.ConverterData("ekd_other_09", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_OTHER_10,
        new SpineConverterDefine.ConverterData("ekd_other_10", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_01,
        new SpineConverterDefine.ConverterData("ekd_party_01", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_02,
        new SpineConverterDefine.ConverterData("ekd_party_02", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_03,
        new SpineConverterDefine.ConverterData("ekd_party_03", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_04,
        new SpineConverterDefine.ConverterData("ekd_party_04", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_05,
        new SpineConverterDefine.ConverterData("ekd_party_05", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_06,
        new SpineConverterDefine.ConverterData("ekd_party_06", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_07,
        new SpineConverterDefine.ConverterData("ekd_party_07", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_08,
        new SpineConverterDefine.ConverterData("ekd_party_08", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_09,
        new SpineConverterDefine.ConverterData("ekd_party_09", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PARTY_10,
        new SpineConverterDefine.ConverterData("ekd_party_10", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_01,
        new SpineConverterDefine.ConverterData("ekd_psn_01", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_02,
        new SpineConverterDefine.ConverterData("ekd_psn_02", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_03,
        new SpineConverterDefine.ConverterData("ekd_psn_03", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_04,
        new SpineConverterDefine.ConverterData("ekd_psn_04", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_05,
        new SpineConverterDefine.ConverterData("ekd_psn_05", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_06,
        new SpineConverterDefine.ConverterData("ekd_psn_06", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_07,
        new SpineConverterDefine.ConverterData("ekd_psn_07", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_08,
        new SpineConverterDefine.ConverterData("ekd_psn_08", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_09,
        new SpineConverterDefine.ConverterData("ekd_psn_09", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_PSN_10,
        new SpineConverterDefine.ConverterData("ekd_psn_10", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_RUN,
        new SpineConverterDefine.ConverterData("ekd_run", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_SMILE,
        new SpineConverterDefine.ConverterData("ekd_smile", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_SURPRISE,
        new SpineConverterDefine.ConverterData("ekd_surprise", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_ANGER,
        new SpineConverterDefine.ConverterData("ekd_talk_anger", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_JOY,
        new SpineConverterDefine.ConverterData("ekd_talk_joy", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_JOY2,
        new SpineConverterDefine.ConverterData("ekd_talk_joy2", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_NORMAL,
        new SpineConverterDefine.ConverterData("ekd_talk_normal", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_SAD,
        new SpineConverterDefine.ConverterData("ekd_talk_sad", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_SURPRISE1,
        new SpineConverterDefine.ConverterData("ekd_talk_surprise1", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EKD_TALK_SURPRISE2,
        new SpineConverterDefine.ConverterData("ekd_talk_surprise2", eSpineConvertCategoryType.EKD, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EKD_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.OMP,
        new SpineConverterDefine.ConverterData("omp_", eSpineConvertCategoryType.OMP, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.OMP_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.UEK,
        new SpineConverterDefine.ConverterData("uek_", eSpineConvertCategoryType.UEK, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.UEK_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.NYX,
        new SpineConverterDefine.ConverterData("nyx_", eSpineConvertCategoryType.NYX, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.NYX_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.IDLE_MAP_S,
        new SpineConverterDefine.ConverterData("idle_Map_s", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.MAP_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.IDLE_MAP,
        new SpineConverterDefine.ConverterData("idle_Map", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.MAP_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.AWAKE_MAP_RELEASE,
        new SpineConverterDefine.ConverterData("awake_Map_release", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.MAP_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.AWAKE_MAP,
        new SpineConverterDefine.ConverterData("awake_Map", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.MAP_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.IDLE,
        new SpineConverterDefine.ConverterData("idle", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.IDLE_MULTI_TARGET,
        new SpineConverterDefine.ConverterData("idle_multi_target_", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.DAMAGE_MULTI_TARGET,
        new SpineConverterDefine.ConverterData("damage_multi_target_", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.WALK,
        new SpineConverterDefine.ConverterData("walk", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.JOY_SHORT,
        new SpineConverterDefine.ConverterData("joy_short", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.JOY_LONG,
        new SpineConverterDefine.ConverterData("joy_long", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.IDLE_STAND_BY,
        new SpineConverterDefine.ConverterData("multi_idle_standBy", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.IDLE_NO_WEAPON,
        new SpineConverterDefine.ConverterData("multi_idle_noWeapon", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RUN_GAME_START,
        new SpineConverterDefine.ConverterData("run_gamestart", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.COOP_STAND_BY,
        new SpineConverterDefine.ConverterData("multi_standBy", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.RUN,
        new SpineConverterDefine.ConverterData("run", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STAND_BY,
        new SpineConverterDefine.ConverterData("standBy", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.LANDING,
        new SpineConverterDefine.ConverterData("landing", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.IDLE_SKIP,
        new SpineConverterDefine.ConverterData("idle_Skip", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.DAMAGE_SKIP,
        new SpineConverterDefine.ConverterData("damage_Skip", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.LOADING,
        new SpineConverterDefine.ConverterData("loading", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.LOADING_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ANNIHILATION,
        new SpineConverterDefine.ConverterData("annihilation", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SLEEP,
        new SpineConverterDefine.ConverterData("sleep", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EVENT_BANNER_MOTION,
        new SpineConverterDefine.ConverterData("event_banner_motion", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EVENT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EVENT_BANNER_MOTION_CIRCLE,
        new SpineConverterDefine.ConverterData("event_banner_motion_circle", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EVENT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SMILE,
        new SpineConverterDefine.ConverterData("smile", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.SMILE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.OWN_DIE,
        new SpineConverterDefine.ConverterData("die", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ATTACK,
        new SpineConverterDefine.ConverterData("attack", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.DAMEGE,
        new SpineConverterDefine.ConverterData("damage", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.DIE,
        new SpineConverterDefine.ConverterData("die", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.DEFEAT,
        new SpineConverterDefine.ConverterData("defeat", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFEAT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.DIE_LOOP,
        new SpineConverterDefine.ConverterData("die_loop", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.COMBINE_JOY_RESULT,
        new SpineConverterDefine.ConverterData("_joy_combine_", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMBINE_JOY_RESULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SKILL,
        new SpineConverterDefine.ConverterData("skill", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.PRINCESS_SKILL,
        new SpineConverterDefine.ConverterData("p", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.PRINCESS_SKILL_EVOLUTION,
        new SpineConverterDefine.ConverterData("p", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SKILL_EVOLUTION,
        new SpineConverterDefine.ConverterData("skill_evolution", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SPECIAL_SKILL_EVOLUTION,
        new SpineConverterDefine.ConverterData("skillSp_evolution", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.JOY_RESULT,
        new SpineConverterDefine.ConverterData("joyResult", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SPECIAL_SKILL,
        new SpineConverterDefine.ConverterData("skillSp", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.OWN_IDLE,
        new SpineConverterDefine.ConverterData("idle", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.OWN_WALK,
        new SpineConverterDefine.ConverterData("walk", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.OWN_JOY,
        new SpineConverterDefine.ConverterData("joy", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.OWN_ATTACK,
        new SpineConverterDefine.ConverterData("attack", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.OWN_DAMEGE,
        new SpineConverterDefine.ConverterData("damage", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SUMMON,
        new SpineConverterDefine.ConverterData("summon", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.GAME_START,
        new SpineConverterDefine.ConverterData("gamestart", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.AWAKE,
        new SpineConverterDefine.ConverterData("awake", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ATTACK_SKIPQUEST,
        new SpineConverterDefine.ConverterData("attack_skipQuest", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.IDLE_RESULT,
        new SpineConverterDefine.ConverterData("idle_Result", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.AWAKE_RESULT,
        new SpineConverterDefine.ConverterData("awake_Result", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.BATTLE_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.EVENT_STORY,
        new SpineConverterDefine.ConverterData("event_", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.EVENT_STORY_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_IDOL,
        new SpineConverterDefine.ConverterData("cmn_idol", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_JUMP,
        new SpineConverterDefine.ConverterData("cmn_jump", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_WALK,
        new SpineConverterDefine.ConverterData("cmn_walk", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_RUN,
        new SpineConverterDefine.ConverterData("cmn_run", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_APPEAR,
        new SpineConverterDefine.ConverterData("cmn_appear", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_DISAPPEAR,
        new SpineConverterDefine.ConverterData("cmn_disappear", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_DISAPPEAR2,
        new SpineConverterDefine.ConverterData("cmn_disappear2", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE01,
        new SpineConverterDefine.ConverterData("cmn_pose01", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE02,
        new SpineConverterDefine.ConverterData("cmn_pose02", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE03,
        new SpineConverterDefine.ConverterData("cmn_pose03", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE04,
        new SpineConverterDefine.ConverterData("cmn_pose04", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE05,
        new SpineConverterDefine.ConverterData("cmn_pose05", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE06,
        new SpineConverterDefine.ConverterData("cmn_pose06", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE07,
        new SpineConverterDefine.ConverterData("cmn_pose07", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE08,
        new SpineConverterDefine.ConverterData("cmn_pose08", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE09,
        new SpineConverterDefine.ConverterData("cmn_pose09", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE10,
        new SpineConverterDefine.ConverterData("cmn_pose10", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE11,
        new SpineConverterDefine.ConverterData("cmn_pose11", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE12,
        new SpineConverterDefine.ConverterData("cmn_pose12", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE13,
        new SpineConverterDefine.ConverterData("cmn_pose13", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE14,
        new SpineConverterDefine.ConverterData("cmn_pose14", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE15,
        new SpineConverterDefine.ConverterData("cmn_pose15", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE16,
        new SpineConverterDefine.ConverterData("cmn_pose16", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE17,
        new SpineConverterDefine.ConverterData("cmn_pose17", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE18,
        new SpineConverterDefine.ConverterData("cmn_pose18", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE19,
        new SpineConverterDefine.ConverterData("cmn_pose19", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE20,
        new SpineConverterDefine.ConverterData("cmn_pose20", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE21,
        new SpineConverterDefine.ConverterData("cmn_pose21", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE22,
        new SpineConverterDefine.ConverterData("cmn_pose22", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE23,
        new SpineConverterDefine.ConverterData("cmn_pose23", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE24,
        new SpineConverterDefine.ConverterData("cmn_pose24", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE25,
        new SpineConverterDefine.ConverterData("cmn_pose25", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE26,
        new SpineConverterDefine.ConverterData("cmn_pose26", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE27,
        new SpineConverterDefine.ConverterData("cmn_pose27", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE28,
        new SpineConverterDefine.ConverterData("cmn_pose28", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE29,
        new SpineConverterDefine.ConverterData("cmn_pose29", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_POSE30,
        new SpineConverterDefine.ConverterData("cmn_pose30", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_CALL,
        new SpineConverterDefine.ConverterData("cmn_call", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_NOTICE,
        new SpineConverterDefine.ConverterData("cmn_notice", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_SHOCK,
        new SpineConverterDefine.ConverterData("cmn_shock", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_NOD,
        new SpineConverterDefine.ConverterData("cmn_nod", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_HANDCLAP,
        new SpineConverterDefine.ConverterData("cmn_handclap", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_GRAB,
        new SpineConverterDefine.ConverterData("cmn_grab01", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_GRAB_PANIC,
        new SpineConverterDefine.ConverterData("cmn_grab02", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE01,
        new SpineConverterDefine.ConverterData("cmn_smile01", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE02,
        new SpineConverterDefine.ConverterData("cmn_smile02", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE03,
        new SpineConverterDefine.ConverterData("cmn_smile03", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE04,
        new SpineConverterDefine.ConverterData("cmn_smile04", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_SMILE05,
        new SpineConverterDefine.ConverterData("cmn_smile05", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_NOREACTION01,
        new SpineConverterDefine.ConverterData("cmn_noReaction01", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_HIGHTOUCH01,
        new SpineConverterDefine.ConverterData("cmn_highTouch01", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_SHAKEHAND01,
        new SpineConverterDefine.ConverterData("cmn_shakeHand01", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_SWING01,
        new SpineConverterDefine.ConverterData("cmn_swing01", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK01,
        new SpineConverterDefine.ConverterData("cmn_prank01", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK02,
        new SpineConverterDefine.ConverterData("cmn_prank02", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK03,
        new SpineConverterDefine.ConverterData("cmn_prank03", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK04,
        new SpineConverterDefine.ConverterData("cmn_prank04", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK05,
        new SpineConverterDefine.ConverterData("cmn_prank05", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK06,
        new SpineConverterDefine.ConverterData("cmn_prank06", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK07,
        new SpineConverterDefine.ConverterData("cmn_prank07", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK08,
        new SpineConverterDefine.ConverterData("cmn_prank08", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK09,
        new SpineConverterDefine.ConverterData("cmn_prank09", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK10,
        new SpineConverterDefine.ConverterData("cmn_prank10", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK11,
        new SpineConverterDefine.ConverterData("cmn_prank11", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK12,
        new SpineConverterDefine.ConverterData("cmn_prank12", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK13,
        new SpineConverterDefine.ConverterData("cmn_prank13", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK14,
        new SpineConverterDefine.ConverterData("cmn_prank14", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK15,
        new SpineConverterDefine.ConverterData("cmn_prank15", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK16,
        new SpineConverterDefine.ConverterData("cmn_prank16", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK17,
        new SpineConverterDefine.ConverterData("cmn_prank17", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK18,
        new SpineConverterDefine.ConverterData("cmn_prank18", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK19,
        new SpineConverterDefine.ConverterData("cmn_prank19", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK20,
        new SpineConverterDefine.ConverterData("cmn_prank20", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK21,
        new SpineConverterDefine.ConverterData("cmn_prank21", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK22,
        new SpineConverterDefine.ConverterData("cmn_prank22", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK23,
        new SpineConverterDefine.ConverterData("cmn_prank23", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK24,
        new SpineConverterDefine.ConverterData("cmn_prank24", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK25,
        new SpineConverterDefine.ConverterData("cmn_prank25", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK26,
        new SpineConverterDefine.ConverterData("cmn_prank26", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK27,
        new SpineConverterDefine.ConverterData("cmn_prank27", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK28,
        new SpineConverterDefine.ConverterData("cmn_prank28", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK29,
        new SpineConverterDefine.ConverterData("cmn_prank29", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_PRANK30,
        new SpineConverterDefine.ConverterData("cmn_prank30", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_ITEM,
        new SpineConverterDefine.ConverterData("itm_", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_SYNC,
        new SpineConverterDefine.ConverterData("sync_", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.ROOM_TRACK,
        new SpineConverterDefine.ConverterData("track_", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.NAVI,
        new SpineConverterDefine.ConverterData("navi_", eSpineConvertCategoryType.ROOM, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.COMMON_ROOM_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STORY_EYE_IDLE,
        new SpineConverterDefine.ConverterData("eye_idle", eSpineConvertCategoryType.STILL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STORY_EYE_BLINK,
        new SpineConverterDefine.ConverterData("eye_blink", eSpineConvertCategoryType.STILL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STORY_EYE_OPEN,
        new SpineConverterDefine.ConverterData("eye_open", eSpineConvertCategoryType.STILL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STORY_EYE_CLOSE,
        new SpineConverterDefine.ConverterData("eye_close", eSpineConvertCategoryType.STILL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STORY_MOUTH_IDLE,
        new SpineConverterDefine.ConverterData("mouth_idle", eSpineConvertCategoryType.STILL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STORY_MOUTH_TALK,
        new SpineConverterDefine.ConverterData("mouth_talk", eSpineConvertCategoryType.STILL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STORY_MOUTH_OPEN,
        new SpineConverterDefine.ConverterData("mouth_open", eSpineConvertCategoryType.STILL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.STORY_MOUTH_CLOSE,
        new SpineConverterDefine.ConverterData("mouth_close", eSpineConvertCategoryType.STILL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT,
        new SpineConverterDefine.ConverterData("effect", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_ATTACK,
        new SpineConverterDefine.ConverterData("effect_attack", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_WALK,
        new SpineConverterDefine.ConverterData("effect_walk", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_IDLE,
        new SpineConverterDefine.ConverterData("effect_idle", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_SUMMON,
        new SpineConverterDefine.ConverterData("effect_summon", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.SUMMON_SKILL_EFFECT_DIE,
        new SpineConverterDefine.ConverterData("effect_die", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT01,
        new SpineConverterDefine.ConverterData("effect", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT02,
        new SpineConverterDefine.ConverterData("effect_2", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT03,
        new SpineConverterDefine.ConverterData("effect_3", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT04,
        new SpineConverterDefine.ConverterData("effect_4", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT05,
        new SpineConverterDefine.ConverterData("effect_5", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_EFFECT06,
        new SpineConverterDefine.ConverterData("effect_6", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT01,
        new SpineConverterDefine.ConverterData("effect_out", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT02,
        new SpineConverterDefine.ConverterData("effect_2_out", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT03,
        new SpineConverterDefine.ConverterData("effect_3_out", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT04,
        new SpineConverterDefine.ConverterData("effect_4_out", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT05,
        new SpineConverterDefine.ConverterData("effect_4_out", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      },
      {
        eSpineCharacterAnimeId.TREASURE_OUT_EFFECT06,
        new SpineConverterDefine.ConverterData("effect_4_out", eSpineConvertCategoryType.NORMAL, new SpineConverterDefine.CONVERT_FUNC(SpineConverterDefine.DEFAULT_CONVERT_FUNC))
      }
    };

    public static string GetFileName(eSpineBinaryAnimationId binaryId, int index1, int index2)
    {
      switch (binaryId)
      {
        case eSpineBinaryAnimationId.COMMON_BATTLE:
          return string.Format("{0:D2}_COMMON_BATTLE", (object) index1);
        case eSpineBinaryAnimationId.COMMON_ROOM:
          return "ROOM_SPINEUNIT_ANIMATION_COMMON";
        case eSpineBinaryAnimationId.UNIQUE_ROOM:
          return string.Format("ROOM_SPINEUNIT_ANIMATION_{0:D6}", (object) index1);
        case eSpineBinaryAnimationId.BATTLE:
          return string.Format("{0:D2}_BATTLE", (object) index1);
        case eSpineBinaryAnimationId.ROOM_ITEM:
          return string.Format("ROOM_SPINEUNIT_ANIMATION_{0:D6}", (object) index1);
        case eSpineBinaryAnimationId.ROOM_ITEM_UNIQUE:
          return string.Format("ROOM_SPINEUNIT_ANIMATION_{0:D6}_{1:D6}", (object) index1, (object) index2);
        case eSpineBinaryAnimationId.ROOM_SYNC:
          return string.Format("ROOM_SYNC_ANIME_{0:D6}", (object) index1);
        case eSpineBinaryAnimationId.ROOM_SYNC_UNIQUE:
          return string.Format("ROOM_SYNC_ANIME_{0:D6}_{1:D6}", (object) index1, (object) index2);
        case eSpineBinaryAnimationId.ROOM_TRACK:
          return string.Format("ROOM_TRACK_ANIME_{0:D6}", (object) index1);
        case eSpineBinaryAnimationId.ROOM_TRACK_UNIQUE:
          return string.Format("ROOM_TRACK_ANIME_{0:D6}_{1:D6}", (object) index1, (object) index2);
        case eSpineBinaryAnimationId.POSING:
          return string.Format("{0:D6}_POSING", (object) index1);
        case eSpineBinaryAnimationId.SMILE:
          return string.Format("{0:D6}_SMILE", (object) index1);
        case eSpineBinaryAnimationId.LOADING:
          return string.Format("{0:D2}_LOADING", (object) index1);
        case eSpineBinaryAnimationId.EVENT:
          return string.Format("EVENT", (object) index1);
        case eSpineBinaryAnimationId.EVENT_STORY:
          return string.Format("EVENT_STORY_{0:D5}", (object) index1);
        case eSpineBinaryAnimationId.DEFEAT:
          return string.Format("{0:D6}_DEFEAT", (object) index1);
        case eSpineBinaryAnimationId.MAP:
          return string.Format("{0:D6}_MAP", (object) index1);
        case eSpineBinaryAnimationId.MINIGAME:
          return string.Format("MINIGAME_{0:D4}", (object) index1);
        case eSpineBinaryAnimationId.RUN_JUMP:
          return string.Format("{0:D6}_RUN_JUMP", (object) index1);
        case eSpineBinaryAnimationId.NO_WEAPON:
          return string.Format("{0:D6}_NO_WEAPON", (object) index1);
        case eSpineBinaryAnimationId.RACE:
          return string.Format("{0:D6}_RACE", (object) index1);
        case eSpineBinaryAnimationId.COMBINE_JOY_RESULT:
          return string.Format("COMBINE_JOY_RESULT_{0:D6}", (object) index1);
        case eSpineBinaryAnimationId.DEAR:
          return string.Format("{0:D6}_DEAR", (object) index1);
        case eSpineBinaryAnimationId.EKD:
          return "SPINE_ANIME_EKD";
        case eSpineBinaryAnimationId.NAVI:
          return string.Format("NAVI_{0:D3}", (object) index1);
        case eSpineBinaryAnimationId.OMP:
          return "SPINE_ANIME_OMP";
        case eSpineBinaryAnimationId.UEK:
          return string.Format("SPINE_ANIME_UEK_{0:D2}", (object) index1);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      int num1 = SpineConverterDefine.extractionPrefixId6(_checkStr);
      int num2 = SpineConverterDefine.extractionPrefixId2(_checkStr);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      int num1 = SpineConverterDefine.extractionPrefixId6(_checkStr);
      int num2 = SpineConverterDefine.extractionPrefixId2(_checkStr);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      int num1 = SpineConverterDefine.extractionPrefixId6(_checkStr);
      int num2 = SpineConverterDefine.extractionPrefixId2(_checkStr);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.POSING;
      int num = SpineConverterDefine.extractionPrefixId6(_checkStr);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.DEFEAT;
      int num = SpineConverterDefine.extractionPrefixId6(_checkStr);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.SMILE;
      int num = SpineConverterDefine.extractionPrefixId6(_checkStr);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.UEK;
      _id = SpineConverterDefine.extractionPrefixId2(_checkStr);
    }

    private static void NYX_CONVERT_FUNC(
      out eSpineBinaryAnimationId _binaryId,
      out int _id,
      int _atlasFileUnitId,
      string _checkStr,
      string _animeName,
      Func<int, bool> _isUniqueMotion)
    {
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.RUN_JUMP;
      int num = SpineConverterDefine.extractionPrefixId6(_checkStr);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.NO_WEAPON;
      int num = SpineConverterDefine.extractionPrefixId6(_checkStr);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.RACE;
      int num = SpineConverterDefine.extractionPrefixId6(_checkStr);
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
      SpineConverterDefine.DEFAULT_CONVERT_FUNC(out _binaryId, out _id);
      if (!_checkStr.Contains(_animeName))
        return;
      _binaryId = eSpineBinaryAnimationId.DEAR;
      int num = SpineConverterDefine.extractionPrefixId6(_checkStr);
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

      private SpineConverterDefine.CONVERT_FUNC animationCheck { get; set; }

      public ConverterData(
        string _setAnimeName,
        eSpineConvertCategoryType _setSpineConvertCategoryType,
        SpineConverterDefine.CONVERT_FUNC _setAnimationCheck)
      {
        this.AnimeName = _setAnimeName;
        this.SpineConvertCategoryType = _setSpineConvertCategoryType;
        this.animationCheck = _setAnimationCheck;
      }

      public void Check(
        out eSpineBinaryAnimationId _spineBinaryAnimationId,
        out int _id,
        int _atlasFileUnitId,
        string _checkStr,
        Func<int, bool> _isUniqueMotion) => this.animationCheck(out _spineBinaryAnimationId, out _id, _atlasFileUnitId, _checkStr, this.AnimeName, _isUniqueMotion);
    }
  }
}
