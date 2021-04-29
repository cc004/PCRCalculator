// Decompiled with JetBrains decompiler
// Type: Elements.ResourceDefineSpine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

namespace Elements
{
  public static class ResourceDefineSpine
  {
    private static readonly SpineResourceInfo[] resouceInfoArray = new SpineResourceInfo[23]
    {
      new SpineResourceInfo(eSpineType.SD_NORMAL, eBundleId.SPINE_SD_NORMAL, eResourceId.SPINE_UNIT_SD_NORMAL_SKELETONDATA, eResourceId.UNIT_SPINE_CONTROLLER, eResourceId.SPINE_UNIT_BINARY, 0.5f, true),
      new SpineResourceInfo(eSpineType.SD_NORMAL_BOUND, eBundleId.SPINE_SD_NORMAL, eResourceId.SPINE_UNIT_SD_NORMAL_SKELETONDATA, eResourceId.BATTLE_SPINE_CONTROLLER, eResourceId.SPINE_UNIT_BINARY, 0.5f, true),
      new SpineResourceInfo(eSpineType.SD_NORMAL_NGUI, eBundleId.SPINE_SD_NORMAL, eResourceId.SPINE_UNIT_SD_NORMAL_SKELETONDATA, eResourceId.NGUI_SPINE_CONTROLLER, eResourceId.SPINE_UNIT_BINARY, 0.5f, true),
      new SpineResourceInfo(eSpineType.SD_SHADOW, eBundleId.SPINE_SD_SHADOW, eResourceId.SPINE_UNIT_SD_SHADOW_SKELETONDATA, eResourceId.UNIT_SPINE_CONTROLLER, eResourceId.SPINE_UNIT_BINARY, 0.5f, true),
      new SpineResourceInfo(eSpineType.SD_SHADOW_BOUND, eBundleId.SPINE_SD_SHADOW, eResourceId.SPINE_UNIT_SD_SHADOW_SKELETONDATA, eResourceId.BATTLE_SPINE_CONTROLLER, eResourceId.SPINE_UNIT_BINARY, 0.5f, true),
      new SpineResourceInfo(eSpineType.SD_COLOR_ENEMY, eBundleId.SPINE_SD_COLOR_ENEMY, eResourceId.SPINE_UNIT_SD_COLOR_ENEMY_SKELETONDATA, eResourceId.UNIT_SPINE_CONTROLLER, eResourceId.SPINE_UNIT_BINARY, 0.5f, true),
      new SpineResourceInfo(eSpineType.SD_COLOR_ENEMY_BOUND, eBundleId.SPINE_SD_COLOR_ENEMY, eResourceId.SPINE_UNIT_SD_COLOR_ENEMY_SKELETONDATA, eResourceId.BATTLE_SPINE_CONTROLLER, eResourceId.SPINE_UNIT_BINARY, 0.5f, true),
      new SpineResourceInfo(eSpineType.SD_FULL, eBundleId.SPINE_FULL, eResourceId.SPINE_UNIT_FULL_SKELETONDATA, eResourceId.UNIT_SPINE_CONTROLLER, _isSkinId: true),
      new SpineResourceInfo(eSpineType.SD_FULL_NGUI, eBundleId.SPINE_FULL, eResourceId.SPINE_UNIT_FULL_SKELETONDATA, eResourceId.NGUI_SPINE_CONTROLLER, _isSkinId: true),
      new SpineResourceInfo(eSpineType.SD_MODE_CHANGE, eBundleId.SPINE_SDMODECHANGE, eResourceId.SPINE_UNIT_MODE_CHANGE_SKELETONDATA, eResourceId.BATTLE_SPINE_CONTROLLER, _animationScale: 0.5f, _isSkinId: true),
      new SpineResourceInfo(eSpineType.SD_SHADOW_MODE_CHANGE, eBundleId.SPINE_SDSHADOWMODECHANGE, eResourceId.SPINE_UNIT_SHADOW_MODE_CHANGE_SKELETONDATA, eResourceId.BATTLE_SPINE_CONTROLLER, _animationScale: 0.5f, _isSkinId: true),
      new SpineResourceInfo(eSpineType.SD_ROOM, eBundleId.ROOM_SPINEUNIT, eResourceId.ROOM_SPINEUNIT_SKELETONDATA, eResourceId.ROOM_SPINE_CONTROLLER, eResourceId.ROOM_SPINEUNIT_ANIMATION_BASE, _isSkinId: true),
      new SpineResourceInfo(eSpineType.SD_ROOM_UNIQUE, eBundleId.ROOM_SPINEUNIT, eResourceId.ROOM_SPINEUNIT_SKELETONDATA, eResourceId.ROOM_SPINE_CONTROLLER, eResourceId.ROOM_SPINEUNIT_ANIMATION_BASE_UNIQUE, _isSkinId: true),
      new SpineResourceInfo(eSpineType.SKILL_EFFECT, eBundleId.SPINE_SKILLEFFECT, eResourceId.SPINE_SUMMON_SKILL_EFFECT_SKELETONDATA, eResourceId.BATTLE_SPINE_CONTROLLER, _animationScale: 0.01f),
      new SpineResourceInfo(eSpineType.STILL, eBundleId.SPINE_STILL, eResourceId.SPINE_UNIT_STILL_SKELETONDATA, eResourceId.UNIT_SPINE_CONTROLLER),
      new SpineResourceInfo(eSpineType.STILL_NUGI, eBundleId.SPINE_STILL, eResourceId.SPINE_UNIT_STILL_SKELETONDATA, eResourceId.NGUI_SPINE_CONTROLLER),
      new SpineResourceInfo(eSpineType.STORY_FULL_NGUI, eBundleId.SPINE_STORY_FULL, eResourceId.SPINE_UNIT_STORY_FULL_SKELETONDATA, eResourceId.NGUI_SPINE_CONTROLLER),
      new SpineResourceInfo(eSpineType.CHARACTER_EMOTION, eBundleId.STORYDATA_EMOTION, eResourceId.STORY_CHARACTER_EMOTION_SKELETONDATA, eResourceId.UNIT_SPINE_CONTROLLER, _animationScale: 0.6f),
      new SpineResourceInfo(eSpineType.ROOM_BALLOON_ICON, eBundleId.ROOM_BALLOON_ICON, eResourceId.ROOM_BALLOON_ICON, eResourceId.NGUI_SPINE_CONTROLLER),
      new SpineResourceInfo(eSpineType.UI_ANIMATION, eBundleId.SPINE_UI_ANIMATION, eResourceId.SPINE_UI_ANIMATION_SKELETONDATA, eResourceId.NGUI_SPINE_CONTROLLER),
      new SpineResourceInfo(eSpineType.BATTLE_FOREGROUND, eBundleId.SPINE_FOREGROUND, eResourceId.SPINE_FOREGROUND_SKELETONDATA, eResourceId.BATTLE_SPINE_CONTROLLER, _animationScale: 0.005185185f),
      new SpineResourceInfo(eSpineType.BATTLE_MIDDLEGROUND, eBundleId.SPINE_MIDDLEGROUND, eResourceId.SPINE_MIDDLEGROUND_SKELETONDATA, eResourceId.BATTLE_SPINE_CONTROLLER, _animationScale: 0.005185185f),
      new SpineResourceInfo(eSpineType.GACHA_PRIZE_RANK, eBundleId.PRIZE_GACHA_RANK, eResourceId.SPINE_GACHA_PRIZE_RANK_SKELETONDATA, eResourceId.SPINE_CONTROLLER)
    };

    public static SpineResourceInfo GetResource(eSpineType _spineType) => ResourceDefineSpine.resouceInfoArray[(int) _spineType];

    public static bool IsSkinIdSpineType(eSpineType _spineType) => ResourceDefineSpine.resouceInfoArray[(int) _spineType].IsSkinId;
  }
}
