// Decompiled with JetBrains decompiler
// Type: Elements.MasterDataManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

//using Coneshell;
using Cute;
//using Sqlite3Plugin;
using System;
using System.Collections.Generic;
using System.IO;

namespace Elements
{
  public class MasterDataManager //: ManagerSingleton<MasterDataManager>
  {
    private Dictionary<string, AbstractMasterDatabase> _databases = new Dictionary<string, AbstractMasterDatabase>();
    private const string masterFileName = "manifest/master.mdb";
    //private IOOJBIAKBHA _masterDbProxy;
    private bool isSetupFinished;
    //public UnitFacePosition unitFacePosition;
    public const string CDB_ASSET_NAME = "a/masterdata_master_0003.cdb";

    /*public MasterEnemyParameter masterEnemyParameter { get; private set; }

    public MasterEnemyRewardData masterEnemyRewardData { get; private set; }

    public MasterResistData masterResistData { get; private set; }

    public MasterAilmentData masterAilmentData { get; private set; }

    public MasterEnemyMParts masterEnemyMParts { get; private set; }

    public MasterEnemyEnableVoice masterEnemyEnableVoice { get; private set; }

    public MasterTips masterTips { get; private set; }

    public MasterBanner masterBanner { get; private set; }

    public MasterSpecialfesBanner masterSpecialfesBanner { get; private set; }

    public MasterReturnSpecialfesBanner masterReturnSpecialfesBanner { get; private set; }

    public MasterContentReleaseData masterContentReleaseData { get; private set; }

    public MasterBgData masterBgData { get; private set; }

    public MasterEventBgData masterEventBgData { get; private set; }

    public MasterNotifData masterNotifData { get; private set; }

    public MasterVisualCustomize masterVisualCustomize { get; private set; }

    public MasterMyprofileContent masterMyprofileContent { get; private set; }

    public MasterClanprofileContent masterClanprofileContent { get; private set; }

    public MasterProfileFrame masterProfileFrame { get; private set; }

    public MasterExperienceUnit masterExperienceUnit { get; private set; }

    public MasterExperienceTeam masterExperienceTeam { get; private set; }

    public MasterLoveChara masterLoveChara { get; private set; }

    public MasterCharacterLoveRankupText masterCharacterLoveRankupText { get; private set; }

    public MasterEquipmentData masterEquipmentData { get; private set; }

    public MasterUniqueEquipmentData masterUniqueEquipmentData { get; private set; }

    public MasterEquipmentCraft masterEquipmentCraft { get; private set; }

    public MasterUniqueEquipmentCraft masterUniqueEquipmentCraft { get; private set; }

    public MasterEquipmentEnhanceData masterEquipmentEnhanceData { get; private set; }

    public MasterUniqueEquipmentEnhanceData masterUniqueEquipmentEnhanceData { get; private set; }

    public MasterEquipmentEnhanceRate masterEquipmentEnhanceRate { get; private set; }

    public MasterUniqueEquipmentEnhanceRate masterUniqueEquipmentEnhanceRate { get; private set; }

    public MasterUniqueEquipmentRankup masterUniqueEquipmentRankup { get; private set; }

    public MasterRewardCollectGuide masterRewardCollectGuide { get; private set; }

    public MasterItemData masterItemData { get; private set; }

    public MasterEquipmentDonation masterEquipmentDonation { get; private set; }

    public MasterCharaETicketData masterCharaETicketData { get; private set; }

    public MasterItemETicketData masterItemETicketData { get; private set; }

    public MasterDailyMissionData masterDailyMissionData { get; private set; }

    public MasterStationaryMissionData masterStationaryMissionData { get; private set; }

    public MasterMissionRewardData masterMissionRewardData { get; private set; }

    public MasterSeasonPack masterSeasonPack { get; private set; }

    public MasterClanBattleBattleMissionData masterClanBattleBattleMissionData { get; private set; }

    public MasterCampaignMissionData masterCampaignMissionData { get; private set; }

    public MasterCampaignMissionRewardData masterCampaignMissionRewardData { get; private set; }

    public MasterCampaignMissionSchedule masterCampaignMissionSchedule { get; private set; }

    public MasterCampaignMissionCategory masterCampaignMissionCategory { get; private set; }

    public MasterQuestData masterQuestData { get; private set; }

    public MasterTrainingQuestData masterTrainingQuestData { get; private set; }

    public MasterQuestConditionData masterQuestConditionData { get; private set; }

    public MasterQuestRewardData masterQuestRewardData { get; private set; }

    public MasterWaveGroupData masterWaveGroupData { get; private set; }

    public MasterCooperationQuestData masterCooperationQuestData { get; private set; }

    public MasterWorldmap masterWorldmap { get; private set; }

    public MasterQuestAreaData masterQuestAreaData { get; private set; }

    public MasterSkipMonsterData masterSkipMonsterData { get; private set; }

    public MasterSkipBossData masterSkipBossData { get; private set; }

    public MasterQuestDefeatNotice masterQuestDefeatNotice { get; private set; }

    public MasterContentMapData masterContentMapData { get; private set; }

    public MasterRarity6QuestData masterRarity6QuestData { get; private set; }

    public MasterQuestAnnihilation masterQuestAnnihilation { get; private set; }*/

    public MasterSkillAction masterSkillAction { get; private set; }

    public MasterSkillData masterSkillData { get; private set; }

    /*public MasterSkillCost masterSkillCost { get; private set; }

    public MasterStoryData masterStoryData { get; private set; }

    public MasterStoryDetail masterStoryDetail { get; private set; }

    public MasterEventStoryData masterEventStoryData { get; private set; }

    public MasterEventStoryDetail masterEventStoryDetail { get; private set; }

    public MasterTowerStoryData masterTowerStoryData { get; private set; }

    public MasterTowerStoryDetail masterTowerStoryDetail { get; private set; }

    public MasterDearStoryData masterDearStoryData { get; private set; }

    public MasterDearStoryDetail masterDearStoryDetail { get; private set; }

    public MasterStoryQuestData masterStoryQuestData { get; private set; }

    public MasterStoryCharacterMask masterStoryCharacterMask { get; private set; }

    public MasterCharaStoryStatus masterCharaStoryStatus { get; private set; }

    public MasterUnitComments masterUnitComments { get; private set; }*/

    public MasterUnitData masterUnitData { get; private set; }

    //public MasterCharaIdentity masterCharaIdentity { get; private set; }

    //public MasterUnitEnemyData masterUnitEnemyData { get; private set; }

    public MasterUnitSkillData masterUnitSkillData { get; private set; }

    public MasterUnitAttackPattern masterUnitAttackPattern { get; private set; }
/*
    public MasterUnitRarity masterUnitRarity { get; private set; }

    public MasterUnitPromotion masterUnitPromotion { get; private set; }

    public MasterUnitPromotionStatus masterUnitPromotionStatus { get; private set; }

    public MasterUnlockSkillData masterUnlockSkillData { get; private set; }

    public MasterUnlockUnitCondition masterUnlockUnitCondition { get; private set; }

    public MasterUnitMypagePos masterUnitMypagePos { get; private set; }

    public MasterUnitProfile masterUnitProfile { get; private set; }

    public MasterNaviComment masterNaviComment { get; private set; }

    public MasterEventNaviComment masterEventNaviComment { get; private set; }

    public MasterSdNaviComment masterSdNaviComment { get; private set; }

    public MasterUnitBackground masterUnitBackground { get; private set; }

    public MasterGuild masterGuild { get; private set; }

    public MasterUnitStatusCoefficient masterUnitStatusCoefficient { get; private set; }

    public MasterPositionSetting masterPositionSetting { get; private set; }

    public MasterUnitIntroduction masterUnitIntroduction { get; private set; }

    public MasterUnitMotionList masterUnitMotionList { get; private set; }

    public MasterVoiceGroup masterVoiceGroup { get; private set; }

    public MasterVoiceGroupChara masterVoiceGroupChara { get; private set; }

    public MasterUnitUniqueEquip masterUnitUniqueEquip { get; private set; }

    public MasterUnlockRarity6 masterUnlockRarity6 { get; private set; }

    public MasterCombinedResultMotion masterCombinedResultMotion { get; private set; }

    public MasterLoginBonusData masterLoginBonusData { get; private set; }

    public MasterLoginBonusDetail masterLoginBonusDetail { get; private set; }

    public MasterLoginBonusMessageData masterLoginBonusMessageData { get; private set; }

    public MasterLoginBonusAdv masterLoginBonusAdv { get; private set; }

    public MasterCharaFortuneSchedule masterCharaFortuneSchedule { get; private set; }

    public MasterCharaFortuneReward masterCharaFortuneReward { get; private set; }

    public MasterCharaFortuneScenario masterCharaFortuneScenario { get; private set; }

    public MasterCharaFortuneRail masterCharaFortuneRail { get; private set; }

    public MasterBirthdayLoginBonusData masterBirthdayLoginBonusData { get; private set; }

    public MasterBirthdayLoginBonusDetail masterBirthdayLoginBonusDetail { get; private set; }

    public MasterRoomItem masterRoomItem { get; private set; }

    public MasterRoomSetup masterRoomSetup { get; private set; }

    public MasterRoomChange masterRoomChange { get; private set; }

    public MasterRoomItemAnnouncement masterRoomItemAnnouncement { get; private set; }

    public MasterRoomItemDetail masterRoomItemDetail { get; private set; }

    public MasterRoomUnitComments masterRoomUnitComments { get; private set; }

    public MasterRoomChatInfo masterRoomChatInfo { get; private set; }

    public MasterRoomChatFormation masterRoomChatFormation { get; private set; }

    public MasterRoomChatScenario masterRoomChatScenario { get; private set; }

    public MasterRoomEmotionIcon masterRoomEmotionIcon { get; private set; }

    public MasterRoomCharacterPersonality masterRoomCharacterPersonality { get; private set; }

    public MasterRoomSkinColor masterRoomSkinColor { get; private set; }

    public MasterRoomCharacterSkinColor masterRoomCharacterSkinColor { get; private set; }

    public MasterRoomEffect masterRoomEffect { get; private set; }

    public MasterRoomEffectRewardGet masterRoomEffectRewardGet { get; private set; }

    public MasterRoomReleaseData masterRoomReleaseData { get; private set; }

    public MasterMovie masterMovie { get; private set; }

    public MasterStill masterStill { get; private set; }

    public MasterSpecialStill masterSpecialStill { get; private set; }

    public MasterAlbumProductionList masterAlbumProductionList { get; private set; }

    public MasterAlbumVoiceList masterAlbumVoiceList { get; private set; }

    public MasterActualUnitBackground masterActualUnitBackground { get; private set; }

    public MasterGlossaryDetail masterGlossaryDetail { get; private set; }

    public MasterCustomMypage masterCustomMypage { get; private set; }

    public MasterArenaMaxRankReward masterArenaMaxRankReward { get; private set; }

    public MasterArenaMaxSeasonRankReward masterArenaMaxSeasonRankReward { get; private set; }

    public MasterArenaDailyRankReward masterArenaDailyRankReward { get; private set; }

    public MasterArenaDefenceReward masterArenaDefenceReward { get; private set; }

    public MasterGrandArenaMaxRankReward masterGrandArenaMaxRankReward { get; private set; }

    public MasterGrandArenaMaxSeasonRankReward masterGrandArenaMaxSeasonRankReward { get; private set; }

    public MasterGrandArenaDailyRankReward masterGrandArenaDailyRankReward { get; private set; }

    public MasterGrandArenaDefenceReward masterGrandArenaDefenceReward { get; private set; }

    public MasterDungeonAreaData masterDungeonAreaData { get; private set; }

    public MasterDungeonQuestData masterDungeonQuestData { get; private set; }

    public MasterClanInviteLevelGroup masterClanInviteLevelGroup { get; private set; }

    public MasterClanBattleSchedule masterClanBattleSchedule { get; private set; }

    public MasterClanBattlePeriod masterClanBattlePeriod { get; private set; }

    public MasterClanBattle2MapData masterClanBattle2MapData { get; private set; }

    public MasterClanBattleSMapData masterClanBattleSMapData { get; private set; }

    public MasterClanBattle2BossData masterClanBattle2BossData { get; private set; }

    public MasterClanBattleSBossData masterClanBattleSBossData { get; private set; }

    public MasterClanBattleBossFixReward masterClanBattleBossFixReward { get; private set; }

    public MasterClanBattleSBossFixReward masterClanBattleSBossFixReward { get; private set; }

    public MasterClanBattleBossDamageRank masterClanBattleBossDamageRank { get; private set; }

    public MasterClanBattleOddsData masterClanBattleOddsData { get; private set; }

    public MasterClanBattlePeriodRankReward masterClanBattlePeriodRankReward { get; private set; }

    public MasterClanGrade masterClanGrade { get; private set; }

    public MasterClanCostGroup masterClanCostGroup { get; private set; }

    public MasterClanBattleLastAttackReward masterClanBattleLastAttackReward { get; private set; }

    public MasterClanBattleParamAdjust masterClanBattleParamAdjust { get; private set; }

    public MasterClanBattleSParamAdjust masterClanBattleSParamAdjust { get; private set; }

    public MasterClanBattlePeriodLapReward masterClanBattlePeriodLapReward { get; private set; }

    public MasterCampaignSchedule masterCampaignSchedule { get; private set; }

    public MasterCampaignLevelData masterCampaignLevelData { get; private set; }

    public MasterGiftMessage masterGiftMessage { get; private set; }

    public MasterGachaData masterGachaData { get; private set; }

    public MasterCampaignFreegachaData masterCampaignFreegachaData { get; private set; }

    public MasterCampaignFreegacha masterCampaignFreegacha { get; private set; }

    public MasterCampaignFreegachaSp masterCampaignFreegachaSp { get; private set; }

    public MasterGachaExchangeLineup masterGachaExchangeLineup { get; private set; }

    public MasterTicketGachaData masterTicketGachaData { get; private set; }

    public MasterPrizegachaData masterPrizegachaData { get; private set; }

    public MasterSekaiSchedule masterSekaiSchedule { get; private set; }

    public MasterSekaiEnemyParameter masterSekaiEnemyParameter { get; private set; }

    public MasterSekaiTopData masterSekaiTopData { get; private set; }

    public MasterSekaiBossMode masterSekaiBossMode { get; private set; }

    public MasterSekaiAddTimesData masterSekaiAddTimesData { get; private set; }

    public MasterSekaiUnlockStoryCondition masterSekaiUnlockStoryCondition { get; private set; }

    public MasterSekaiBossFixReward masterSekaiBossFixReward { get; private set; }

    public MasterSekaiBossDamageRankReward masterSekaiBossDamageRankReward { get; private set; }

    public MasterSekaiTopStoryData masterSekaiTopStoryData { get; private set; }

    public MasterShopStaticPriceGroup masterShopStaticPriceGroup { get; private set; }

    public MasterFixLineupGroupSet masterFixLineupGroupSet { get; private set; }

    public MasterStamp masterStamp { get; private set; }

    public MasterGoldsetData masterGoldsetData { get; private set; }

    public MasterGoldsetDataTeamlevel masterGoldsetDataTeamlevel { get; private set; }

    public MasterGoldsetData2 masterGoldsetData2 { get; private set; }

    public MasterHatsuneSchedule masterHatsuneSchedule { get; private set; }

    public MasterHatsuneItem masterHatsuneItem { get; private set; }

    public MasterHatsuneQuest masterHatsuneQuest { get; private set; }

    public MasterHatsuneQuestCondition masterHatsuneQuestCondition { get; private set; }

    public MasterHatsuneMap masterHatsuneMap { get; private set; }

    public MasterHatsuneQuestArea masterHatsuneQuestArea { get; private set; }

    public MasterHatsuneBoss masterHatsuneBoss { get; private set; }

    public MasterHatsuneBossCondition masterHatsuneBossCondition { get; private set; }

    public MasterHatsuneUnlockStoryCondition masterHatsuneUnlockStoryCondition { get; private set; }

    public MasterHatsuneDailyMissionData masterHatsuneDailyMissionData { get; private set; }

    public MasterHatsuneStationaryMissionData masterHatsuneStationaryMissionData { get; private set; }

    public MasterHatsuneSpecialMissionData masterHatsuneSpecialMissionData { get; private set; }

    public MasterHatsuneMissionRewardData masterHatsuneMissionRewardData { get; private set; }

    public MasterHatsuneDescription masterHatsuneDescription { get; private set; }

    public MasterEventIntroduction masterEventIntroduction { get; private set; }

    public MasterHatsuneUnlockUnitCondition masterHatsuneUnlockUnitCondition { get; private set; }

    public MasterHatsuneMultiRouteParameter masterHatsuneMultiRouteParameter { get; private set; }

    public MasterHatsuneMapEvent masterHatsuneMapEvent { get; private set; }

    public MasterHatsuneBgChange masterHatsuneBgChange { get; private set; }

    public MasterHatsuneBgChangeData masterHatsuneBgChangeData { get; private set; }

    public MasterHatsunePresent masterHatsunePresent { get; private set; }

    public MasterHatsuneSpecialBattle masterHatsuneSpecialBattle { get; private set; }

    public MasterHatsuneSpecialEnemy masterHatsuneSpecialEnemy { get; private set; }

    public MasterHatsuneSpecialBossTicketCount masterHatsuneSpecialBossTicketCount { get; private set; }

    public MasterHatsuneQuiz masterHatsuneQuiz { get; private set; }

    public MasterHatsuneQuizCondition masterHatsuneQuizCondition { get; private set; }

    public MasterHatsuneQuizReward masterHatsuneQuizReward { get; private set; }

    public MasterHatsuneBattleMissionData masterHatsuneBattleMissionData { get; private set; }

    public MasterHatsuneDiaryData masterHatsuneDiaryData { get; private set; }

    public MasterHatsuneDiaryScript masterHatsuneDiaryScript { get; private set; }

    public MasterHatsuneDiaryLetterScript masterHatsuneDiaryLetterScript { get; private set; }

    public MasterHatsuneDiarySetting masterHatsuneDiarySetting { get; private set; }

    public MasterHatsuneRelayData masterHatsuneRelayData { get; private set; }

    public MasterShioriEventList masterShioriEventList { get; private set; }

    public MasterShioriItem masterShioriItem { get; private set; }

    public MasterShioriQuest masterShioriQuest { get; private set; }

    public MasterShioriQuestCondition masterShioriQuestCondition { get; private set; }

    public MasterShioriQuestArea masterShioriQuestArea { get; private set; }

    public MasterShioriBoss masterShioriBoss { get; private set; }

    public MasterShioriBossCondition masterShioriBossCondition { get; private set; }

    public MasterShioriStationaryMissionData masterShioriStationaryMissionData { get; private set; }

    public MasterShioriBattleMissionData masterShioriBattleMissionData { get; private set; }

    public MasterShioriMissionRewardData masterShioriMissionRewardData { get; private set; }

    public MasterShioriEnemyParameter masterShioriEnemyParameter { get; private set; }

    public MasterShioriWaveGroupData masterShioriWaveGroupData { get; private set; }

    public MasterShioriDescription masterShioriDescription { get; private set; }

    public MasterShioriUnlockUnitCondition masterShioriUnlockUnitCondition { get; private set; }

    public MasterCampaignShioriGroup masterCampaignShioriGroup { get; private set; }

    public MasterEventBossTreasureBox masterEventBossTreasureBox { get; private set; }

    public MasterEventBossTreasureContent masterEventBossTreasureContent { get; private set; }

    public MasterEventGachaData masterEventGachaData { get; private set; }

    public MasterOddsNameData masterOddsNameData { get; private set; }

    public MasterEventEnemyParameter masterEventEnemyParameter { get; private set; }

    public MasterEventEnemyRewardGroup masterEventEnemyRewardGroup { get; private set; }

    public MasterEventWaveGroupData masterEventWaveGroupData { get; private set; }

    public MasterEventRevivalWaveGroupData masterEventRevivalWaveGroupData { get; private set; }

    public MasterEventSeriesWaveGroupData masterEventSeriesWaveGroupData { get; private set; }

    public MasterEventNaviCommentCondition masterEventNaviCommentCondition { get; private set; }

    public MasterEventTopAdv masterEventTopAdv { get; private set; }

    public MasterMinigame masterMinigame { get; private set; }

    public MasterUekMission masterUekMission { get; private set; }

    public MasterDearSetting masterDearSetting { get; private set; }

    public MasterDearChara masterDearChara { get; private set; }

    public MasterDearReward masterDearReward { get; private set; }

    public MasterUekBoss masterUekBoss { get; private set; }

    public MasterOmpStoryData masterOmpStoryData { get; private set; }

    public MasterOmpDrama masterOmpDrama { get; private set; }

    public MasterUekDrama masterUekDrama { get; private set; }

    public MasterUekSpineAnimLink masterUekSpineAnimLink { get; private set; }

    public MasterEventEffectSetting masterEventEffectSetting { get; private set; }

    public MasterNyxStoryData masterNyxStoryData { get; private set; }

    public MasterNyxPhaseData masterNyxPhaseData { get; private set; }

    public MasterNyxDramaData masterNyxDramaData { get; private set; }

    public MasterNyxStoryScript masterNyxStoryScript { get; private set; }

    public MasterNyxDramaScript masterNyxDramaScript { get; private set; }

    public MasterTmeMapData masterTmeMapData { get; private set; }

    public MasterEventReminder masterEventReminder { get; private set; }

    public MasterEventReminderCondition masterEventReminderCondition { get; private set; }

    public MasterTowerSchedule masterTowerSchedule { get; private set; }

    public MasterTowerAreaData masterTowerAreaData { get; private set; }

    public MasterTowerQuestData masterTowerQuestData { get; private set; }

    public MasterTowerExQuestData masterTowerExQuestData { get; private set; }

    public MasterTowerEnemyParameter masterTowerEnemyParameter { get; private set; }

    public MasterTowerQuestFixRewardGroup masterTowerQuestFixRewardGroup { get; private set; }

    public MasterTowerQuestOddsGroup masterTowerQuestOddsGroup { get; private set; }

    public MasterTowerWaveGroupData masterTowerWaveGroupData { get; private set; }

    public MasterTowerCloisterQuestData masterTowerCloisterQuestData { get; private set; }

    public MasterMusicContent masterMusicContent { get; private set; }

    public MasterMusicList masterMusicList { get; private set; }

    public MasterPctItempoint masterPctItempoint { get; private set; }

    public MasterPctReward masterPctReward { get; private set; }

    public MasterPctTapSpeed masterPctTapSpeed { get; private set; }

    public MasterPctEvaluation masterPctEvaluation { get; private set; }

    public MasterPctComboCoefficient masterPctComboCoefficient { get; private set; }

    public MasterPctSystem masterPctSystem { get; private set; }

    public MasterPctGamingMotion masterPctGamingMotion { get; private set; }

    public MasterPctSystemFruits masterPctSystemFruits { get; private set; }

    public MasterPctResult masterPctResult { get; private set; }

    public MasterFkeReward masterFkeReward { get; private set; }

    public MasterFkeHappeningList masterFkeHappeningList { get; private set; }

    public MasterKmkReward masterKmkReward { get; private set; }

    public MasterKmkNaviComment masterKmkNaviComment { get; private set; }

    public MasterSrtPanel masterSrtPanel { get; private set; }

    public MasterSrtTopTalk masterSrtTopTalk { get; private set; }

    public MasterSrtReward masterSrtReward { get; private set; }

    public MasterSrtScore masterSrtScore { get; private set; }

    public MasterSrtAction masterSrtAction { get; private set; }

    public MasterTtkDrama masterTtkDrama { get; private set; }

    public MasterTtkStoryScript masterTtkStoryScript { get; private set; }

    public MasterTtkEnemy masterTtkEnemy { get; private set; }

    public MasterTtkScore masterTtkScore { get; private set; }

    public MasterTtkNaviComment masterTtkNaviComment { get; private set; }

    public MasterTtkWeapon masterTtkWeapon { get; private set; }

    public MasterTtkStory masterTtkStory { get; private set; }

    public MasterTtkReward masterTtkReward { get; private set; }

    public MasterPkbReward masterPkbReward { get; private set; }

    public MasterPkbBatterCondition masterPkbBatterCondition { get; private set; }

    public MasterPkbPitcherBallType masterPkbPitcherBallType { get; private set; }

    public MasterPkbDramaData masterPkbDramaData { get; private set; }

    public MasterPkbDrama masterPkbDrama { get; private set; }

    public MasterPkbNaviComment masterPkbNaviComment { get; private set; }

    public MasterVoteData masterVoteData { get; private set; }

    public MasterVoteUnit masterVoteUnit { get; private set; }

    public MasterVoteInfo masterVoteInfo { get; private set; }

    public MasterEmblemData masterEmblemData { get; private set; }

    public MasterEmblemMissionData masterEmblemMissionData { get; private set; }

    public MasterEmblemMissionRewardData masterEmblemMissionRewardData { get; private set; }

    public MasterHatsuneEmblemMission masterHatsuneEmblemMission { get; private set; }

    public MasterHatsuneEmblemMissionReward masterHatsuneEmblemMissionReward { get; private set; }

    public MasterKaiserAddTimesData masterKaiserAddTimesData { get; private set; }

    public MasterKaiserQuestData masterKaiserQuestData { get; private set; }

    public MasterKaiserRestrictionGroup masterKaiserRestrictionGroup { get; private set; }

    public MasterKaiserSchedule masterKaiserSchedule { get; private set; }

    public MasterKaiserExterminationReward masterKaiserExterminationReward { get; private set; }

    public MasterKaiserSpecialBattle masterKaiserSpecialBattle { get; private set; }

    public MasterSpaceSchedule masterSpaceSchedule { get; private set; }

    public MasterSpaceTopData masterSpaceTopData { get; private set; }

    public MasterSpaceBattleData masterSpaceBattleData { get; private set; }

    public MasterArcadeList masterArcadeList { get; private set; }

    public MasterArcadeStoryList masterArcadeStoryList { get; private set; }

    public MasterSerialGroupData masterSerialGroupData { get; private set; }

    public MasterSerialCodeData masterSerialCodeData { get; private set; }*/

    /*protected override void onInit()
    {
    }
    */
    protected HashSet<string> GetKnownMasterGroups() => new HashSet<string>()
    {
      "master_enemy",
      "master_etc",
      "master_exp",
      "master_inventory",
      "master_mission",
      "master_quest",
      "master_skill",
      "master_story",
      "master_unit",
      "master_login_bonus",
      "master_room",
      "master_album",
      "master_arena",
      "master_dungeon",
      "master_clan",
      "master_clan_battle",
      "master_campaign",
      "master_gift",
      "master_gacha",
      "master_sekai",
      "master_shop",
      "master_stamp",
      "master_goldset",
      "master_hatsune",
      "master_shiori",
      "master_event",
      "master_tower",
      "master_jukebox",
      "master_pct",
      "master_fke",
      "master_kmk",
      "master_srt",
      "master_ttk",
      "master_pkb",
      "master_vote",
      "master_emblem",
      "master_kaiser",
      "master_space",
      "master_arcade",
      "master_serial_code"
    };

    /*public void SetMasterDB(IOOJBIAKBHA masterDbProxy) => this._masterDbProxy = masterDbProxy;

    protected bool SetupMasterGroup(string groupName)
    {
      bool flag = true;
      switch (groupName)
      {
        case "master_album":
          if (this._masterDbProxy != null)
          {
            MasterAlbumDatabase masterAlbumDatabase = new MasterAlbumDatabase(this._masterDbProxy);
            this.masterMovie = masterAlbumDatabase.masterMovie;
            this.masterStill = masterAlbumDatabase.masterStill;
            this.masterSpecialStill = masterAlbumDatabase.masterSpecialStill;
            this.masterAlbumProductionList = masterAlbumDatabase.masterAlbumProductionList;
            this.masterAlbumVoiceList = masterAlbumDatabase.masterAlbumVoiceList;
            this.masterActualUnitBackground = masterAlbumDatabase.masterActualUnitBackground;
            this.masterGlossaryDetail = masterAlbumDatabase.masterGlossaryDetail;
            this.masterCustomMypage = masterAlbumDatabase.masterCustomMypage;
            this._databases.Add("master_album", (AbstractMasterDatabase) masterAlbumDatabase);
            break;
          }
          break;
        case "master_arcade":
          if (this._masterDbProxy != null)
          {
            MasterArcadeDatabase masterArcadeDatabase = new MasterArcadeDatabase(this._masterDbProxy);
            this.masterArcadeList = masterArcadeDatabase.masterArcadeList;
            this.masterArcadeStoryList = masterArcadeDatabase.masterArcadeStoryList;
            this._databases.Add("master_arcade", (AbstractMasterDatabase) masterArcadeDatabase);
            break;
          }
          break;
        case "master_arena":
          if (this._masterDbProxy != null)
          {
            MasterArenaDatabase masterArenaDatabase = new MasterArenaDatabase(this._masterDbProxy);
            this.masterArenaMaxRankReward = masterArenaDatabase.masterArenaMaxRankReward;
            this.masterArenaMaxSeasonRankReward = masterArenaDatabase.masterArenaMaxSeasonRankReward;
            this.masterArenaDailyRankReward = masterArenaDatabase.masterArenaDailyRankReward;
            this.masterArenaDefenceReward = masterArenaDatabase.masterArenaDefenceReward;
            this.masterGrandArenaMaxRankReward = masterArenaDatabase.masterGrandArenaMaxRankReward;
            this.masterGrandArenaMaxSeasonRankReward = masterArenaDatabase.masterGrandArenaMaxSeasonRankReward;
            this.masterGrandArenaDailyRankReward = masterArenaDatabase.masterGrandArenaDailyRankReward;
            this.masterGrandArenaDefenceReward = masterArenaDatabase.masterGrandArenaDefenceReward;
            this._databases.Add("master_arena", (AbstractMasterDatabase) masterArenaDatabase);
            break;
          }
          break;
        case "master_campaign":
          if (this._masterDbProxy != null)
          {
            MasterCampaignDatabase campaignDatabase = new MasterCampaignDatabase(this._masterDbProxy);
            this.masterCampaignSchedule = campaignDatabase.masterCampaignSchedule;
            this.masterCampaignLevelData = campaignDatabase.masterCampaignLevelData;
            this._databases.Add("master_campaign", (AbstractMasterDatabase) campaignDatabase);
            break;
          }
          break;
        case "master_clan":
          if (this._masterDbProxy != null)
          {
            MasterClanDatabase masterClanDatabase = new MasterClanDatabase(this._masterDbProxy);
            this.masterClanInviteLevelGroup = masterClanDatabase.masterClanInviteLevelGroup;
            this._databases.Add("master_clan", (AbstractMasterDatabase) masterClanDatabase);
            break;
          }
          break;
        case "master_clan_battle":
          if (this._masterDbProxy != null)
          {
            MasterClanBattleDatabase clanBattleDatabase = new MasterClanBattleDatabase(this._masterDbProxy);
            this.masterClanBattleSchedule = clanBattleDatabase.masterClanBattleSchedule;
            this.masterClanBattlePeriod = clanBattleDatabase.masterClanBattlePeriod;
            this.masterClanBattle2MapData = clanBattleDatabase.masterClanBattle2MapData;
            this.masterClanBattleSMapData = clanBattleDatabase.masterClanBattleSMapData;
            this.masterClanBattle2BossData = clanBattleDatabase.masterClanBattle2BossData;
            this.masterClanBattleSBossData = clanBattleDatabase.masterClanBattleSBossData;
            this.masterClanBattleBossFixReward = clanBattleDatabase.masterClanBattleBossFixReward;
            this.masterClanBattleSBossFixReward = clanBattleDatabase.masterClanBattleSBossFixReward;
            this.masterClanBattleBossDamageRank = clanBattleDatabase.masterClanBattleBossDamageRank;
            this.masterClanBattleOddsData = clanBattleDatabase.masterClanBattleOddsData;
            this.masterClanBattlePeriodRankReward = clanBattleDatabase.masterClanBattlePeriodRankReward;
            this.masterClanGrade = clanBattleDatabase.masterClanGrade;
            this.masterClanCostGroup = clanBattleDatabase.masterClanCostGroup;
            this.masterClanBattleLastAttackReward = clanBattleDatabase.masterClanBattleLastAttackReward;
            this.masterClanBattleParamAdjust = clanBattleDatabase.masterClanBattleParamAdjust;
            this.masterClanBattleSParamAdjust = clanBattleDatabase.masterClanBattleSParamAdjust;
            this.masterClanBattlePeriodLapReward = clanBattleDatabase.masterClanBattlePeriodLapReward;
            this._databases.Add("master_clan_battle", (AbstractMasterDatabase) clanBattleDatabase);
            break;
          }
          break;
        case "master_dungeon":
          if (this._masterDbProxy != null)
          {
            MasterDungeonDatabase masterDungeonDatabase = new MasterDungeonDatabase(this._masterDbProxy);
            this.masterDungeonAreaData = masterDungeonDatabase.masterDungeonAreaData;
            this.masterDungeonQuestData = masterDungeonDatabase.masterDungeonQuestData;
            this._databases.Add("master_dungeon", (AbstractMasterDatabase) masterDungeonDatabase);
            break;
          }
          break;
        case "master_emblem":
          if (this._masterDbProxy != null)
          {
            MasterEmblemDatabase masterEmblemDatabase = new MasterEmblemDatabase(this._masterDbProxy);
            this.masterEmblemData = masterEmblemDatabase.masterEmblemData;
            this.masterEmblemMissionData = masterEmblemDatabase.masterEmblemMissionData;
            this.masterEmblemMissionRewardData = masterEmblemDatabase.masterEmblemMissionRewardData;
            this.masterHatsuneEmblemMission = masterEmblemDatabase.masterHatsuneEmblemMission;
            this.masterHatsuneEmblemMissionReward = masterEmblemDatabase.masterHatsuneEmblemMissionReward;
            this._databases.Add("master_emblem", (AbstractMasterDatabase) masterEmblemDatabase);
            break;
          }
          break;
        case "master_enemy":
          if (this._masterDbProxy != null)
          {
            MasterEnemyDatabase masterEnemyDatabase = new MasterEnemyDatabase(this._masterDbProxy);
            this.masterEnemyParameter = masterEnemyDatabase.masterEnemyParameter;
            this.masterEnemyRewardData = masterEnemyDatabase.masterEnemyRewardData;
            this.masterResistData = masterEnemyDatabase.masterResistData;
            this.masterAilmentData = masterEnemyDatabase.masterAilmentData;
            this.masterEnemyMParts = masterEnemyDatabase.masterEnemyMParts;
            this.masterEnemyEnableVoice = masterEnemyDatabase.masterEnemyEnableVoice;
            this._databases.Add("master_enemy", (AbstractMasterDatabase) masterEnemyDatabase);
            break;
          }
          break;
        case "master_etc":
          if (this._masterDbProxy != null)
          {
            MasterEtcDatabase masterEtcDatabase = new MasterEtcDatabase(this._masterDbProxy);
            this.masterTips = masterEtcDatabase.masterTips;
            this.masterBanner = masterEtcDatabase.masterBanner;
            this.masterSpecialfesBanner = masterEtcDatabase.masterSpecialfesBanner;
            this.masterReturnSpecialfesBanner = masterEtcDatabase.masterReturnSpecialfesBanner;
            this.masterContentReleaseData = masterEtcDatabase.masterContentReleaseData;
            this.masterBgData = masterEtcDatabase.masterBgData;
            this.masterEventBgData = masterEtcDatabase.masterEventBgData;
            this.masterNotifData = masterEtcDatabase.masterNotifData;
            this.masterVisualCustomize = masterEtcDatabase.masterVisualCustomize;
            this.masterMyprofileContent = masterEtcDatabase.masterMyprofileContent;
            this.masterClanprofileContent = masterEtcDatabase.masterClanprofileContent;
            this.masterProfileFrame = masterEtcDatabase.masterProfileFrame;
            this._databases.Add("master_etc", (AbstractMasterDatabase) masterEtcDatabase);
            break;
          }
          break;
        case "master_event":
          if (this._masterDbProxy != null)
          {
            MasterEventDatabase masterEventDatabase = new MasterEventDatabase(this._masterDbProxy);
            this.masterEventBossTreasureBox = masterEventDatabase.masterEventBossTreasureBox;
            this.masterEventBossTreasureContent = masterEventDatabase.masterEventBossTreasureContent;
            this.masterEventGachaData = masterEventDatabase.masterEventGachaData;
            this.masterOddsNameData = masterEventDatabase.masterOddsNameData;
            this.masterEventEnemyParameter = masterEventDatabase.masterEventEnemyParameter;
            this.masterEventEnemyRewardGroup = masterEventDatabase.masterEventEnemyRewardGroup;
            this.masterEventWaveGroupData = masterEventDatabase.masterEventWaveGroupData;
            this.masterEventRevivalWaveGroupData = masterEventDatabase.masterEventRevivalWaveGroupData;
            this.masterEventSeriesWaveGroupData = masterEventDatabase.masterEventSeriesWaveGroupData;
            this.masterEventNaviCommentCondition = masterEventDatabase.masterEventNaviCommentCondition;
            this.masterEventTopAdv = masterEventDatabase.masterEventTopAdv;
            this.masterMinigame = masterEventDatabase.masterMinigame;
            this.masterUekMission = masterEventDatabase.masterUekMission;
            this.masterDearSetting = masterEventDatabase.masterDearSetting;
            this.masterDearChara = masterEventDatabase.masterDearChara;
            this.masterDearReward = masterEventDatabase.masterDearReward;
            this.masterUekBoss = masterEventDatabase.masterUekBoss;
            this.masterOmpStoryData = masterEventDatabase.masterOmpStoryData;
            this.masterOmpDrama = masterEventDatabase.masterOmpDrama;
            this.masterUekDrama = masterEventDatabase.masterUekDrama;
            this.masterUekSpineAnimLink = masterEventDatabase.masterUekSpineAnimLink;
            this.masterEventEffectSetting = masterEventDatabase.masterEventEffectSetting;
            this.masterNyxStoryData = masterEventDatabase.masterNyxStoryData;
            this.masterNyxPhaseData = masterEventDatabase.masterNyxPhaseData;
            this.masterNyxDramaData = masterEventDatabase.masterNyxDramaData;
            this.masterNyxStoryScript = masterEventDatabase.masterNyxStoryScript;
            this.masterNyxDramaScript = masterEventDatabase.masterNyxDramaScript;
            this.masterTmeMapData = masterEventDatabase.masterTmeMapData;
            this.masterEventReminder = masterEventDatabase.masterEventReminder;
            this.masterEventReminderCondition = masterEventDatabase.masterEventReminderCondition;
            this._databases.Add("master_event", (AbstractMasterDatabase) masterEventDatabase);
            break;
          }
          break;
        case "master_exp":
          if (this._masterDbProxy != null)
          {
            MasterExpDatabase masterExpDatabase = new MasterExpDatabase(this._masterDbProxy);
            this.masterExperienceUnit = masterExpDatabase.masterExperienceUnit;
            this.masterExperienceTeam = masterExpDatabase.masterExperienceTeam;
            this.masterLoveChara = masterExpDatabase.masterLoveChara;
            this.masterCharacterLoveRankupText = masterExpDatabase.masterCharacterLoveRankupText;
            this._databases.Add("master_exp", (AbstractMasterDatabase) masterExpDatabase);
            break;
          }
          break;
        case "master_fke":
          if (this._masterDbProxy != null)
          {
            MasterFkeDatabase masterFkeDatabase = new MasterFkeDatabase(this._masterDbProxy);
            this.masterFkeReward = masterFkeDatabase.masterFkeReward;
            this.masterFkeHappeningList = masterFkeDatabase.masterFkeHappeningList;
            this._databases.Add("master_fke", (AbstractMasterDatabase) masterFkeDatabase);
            break;
          }
          break;
        case "master_gacha":
          if (this._masterDbProxy != null)
          {
            MasterGachaDatabase masterGachaDatabase = new MasterGachaDatabase(this._masterDbProxy);
            this.masterGachaData = masterGachaDatabase.masterGachaData;
            this.masterCampaignFreegachaData = masterGachaDatabase.masterCampaignFreegachaData;
            this.masterCampaignFreegacha = masterGachaDatabase.masterCampaignFreegacha;
            this.masterCampaignFreegachaSp = masterGachaDatabase.masterCampaignFreegachaSp;
            this.masterGachaExchangeLineup = masterGachaDatabase.masterGachaExchangeLineup;
            this.masterTicketGachaData = masterGachaDatabase.masterTicketGachaData;
            this.masterPrizegachaData = masterGachaDatabase.masterPrizegachaData;
            this._databases.Add("master_gacha", (AbstractMasterDatabase) masterGachaDatabase);
            break;
          }
          break;
        case "master_gift":
          if (this._masterDbProxy != null)
          {
            MasterGiftDatabase masterGiftDatabase = new MasterGiftDatabase(this._masterDbProxy);
            this.masterGiftMessage = masterGiftDatabase.masterGiftMessage;
            this._databases.Add("master_gift", (AbstractMasterDatabase) masterGiftDatabase);
            break;
          }
          break;
        case "master_goldset":
          if (this._masterDbProxy != null)
          {
            MasterGoldsetDatabase masterGoldsetDatabase = new MasterGoldsetDatabase(this._masterDbProxy);
            this.masterGoldsetData = masterGoldsetDatabase.masterGoldsetData;
            this.masterGoldsetDataTeamlevel = masterGoldsetDatabase.masterGoldsetDataTeamlevel;
            this.masterGoldsetData2 = masterGoldsetDatabase.masterGoldsetData2;
            this._databases.Add("master_goldset", (AbstractMasterDatabase) masterGoldsetDatabase);
            break;
          }
          break;
        case "master_hatsune":
          if (this._masterDbProxy != null)
          {
            MasterHatsuneDatabase masterHatsuneDatabase = new MasterHatsuneDatabase(this._masterDbProxy);
            this.masterHatsuneSchedule = masterHatsuneDatabase.masterHatsuneSchedule;
            this.masterHatsuneItem = masterHatsuneDatabase.masterHatsuneItem;
            this.masterHatsuneQuest = masterHatsuneDatabase.masterHatsuneQuest;
            this.masterHatsuneQuestCondition = masterHatsuneDatabase.masterHatsuneQuestCondition;
            this.masterHatsuneMap = masterHatsuneDatabase.masterHatsuneMap;
            this.masterHatsuneQuestArea = masterHatsuneDatabase.masterHatsuneQuestArea;
            this.masterHatsuneBoss = masterHatsuneDatabase.masterHatsuneBoss;
            this.masterHatsuneBossCondition = masterHatsuneDatabase.masterHatsuneBossCondition;
            this.masterHatsune
    StoryCondition = masterHatsuneDatabase.masterHatsuneUnlockStoryCondition;
            this.masterHatsuneDailyMissionData = masterHatsuneDatabase.masterHatsuneDailyMissionData;
            this.masterHatsuneStationaryMissionData = masterHatsuneDatabase.masterHatsuneStationaryMissionData;
            this.masterHatsuneSpecialMissionData = masterHatsuneDatabase.masterHatsuneSpecialMissionData;
            this.masterHatsuneMissionRewardData = masterHatsuneDatabase.masterHatsuneMissionRewardData;
            this.masterHatsuneDescription = masterHatsuneDatabase.masterHatsuneDescription;
            this.masterEventIntroduction = masterHatsuneDatabase.masterEventIntroduction;
            this.masterHatsuneUnlockUnitCondition = masterHatsuneDatabase.masterHatsuneUnlockUnitCondition;
            this.masterHatsuneMultiRouteParameter = masterHatsuneDatabase.masterHatsuneMultiRouteParameter;
            this.masterHatsuneMapEvent = masterHatsuneDatabase.masterHatsuneMapEvent;
            this.masterHatsuneBgChange = masterHatsuneDatabase.masterHatsuneBgChange;
            this.masterHatsuneBgChangeData = masterHatsuneDatabase.masterHatsuneBgChangeData;
            this.masterHatsunePresent = masterHatsuneDatabase.masterHatsunePresent;
            this.masterHatsuneSpecialBattle = masterHatsuneDatabase.masterHatsuneSpecialBattle;
            this.masterHatsuneSpecialEnemy = masterHatsuneDatabase.masterHatsuneSpecialEnemy;
            this.masterHatsuneSpecialBossTicketCount = masterHatsuneDatabase.masterHatsuneSpecialBossTicketCount;
            this.masterHatsuneQuiz = masterHatsuneDatabase.masterHatsuneQuiz;
            this.masterHatsuneQuizCondition = masterHatsuneDatabase.masterHatsuneQuizCondition;
            this.masterHatsuneQuizReward = masterHatsuneDatabase.masterHatsuneQuizReward;
            this.masterHatsuneBattleMissionData = masterHatsuneDatabase.masterHatsuneBattleMissionData;
            this.masterHatsuneDiaryData = masterHatsuneDatabase.masterHatsuneDiaryData;
            this.masterHatsuneDiaryScript = masterHatsuneDatabase.masterHatsuneDiaryScript;
            this.masterHatsuneDiaryLetterScript = masterHatsuneDatabase.masterHatsuneDiaryLetterScript;
            this.masterHatsuneDiarySetting = masterHatsuneDatabase.masterHatsuneDiarySetting;
            this.masterHatsuneRelayData = masterHatsuneDatabase.masterHatsuneRelayData;
            this._databases.Add("master_hatsune", (AbstractMasterDatabase) masterHatsuneDatabase);
            break;
          }
          break;
        case "master_inventory":
          if (this._masterDbProxy != null)
          {
            MasterInventoryDatabase inventoryDatabase = new MasterInventoryDatabase(this._masterDbProxy);
            this.masterEquipmentData = inventoryDatabase.masterEquipmentData;
            this.masterUniqueEquipmentData = inventoryDatabase.masterUniqueEquipmentData;
            this.masterEquipmentCraft = inventoryDatabase.masterEquipmentCraft;
            this.masterUniqueEquipmentCraft = inventoryDatabase.masterUniqueEquipmentCraft;
            this.masterEquipmentEnhanceData = inventoryDatabase.masterEquipmentEnhanceData;
            this.masterUniqueEquipmentEnhanceData = inventoryDatabase.masterUniqueEquipmentEnhanceData;
            this.masterEquipmentEnhanceRate = inventoryDatabase.masterEquipmentEnhanceRate;
            this.masterUniqueEquipmentEnhanceRate = inventoryDatabase.masterUniqueEquipmentEnhanceRate;
            this.masterUniqueEquipmentRankup = inventoryDatabase.masterUniqueEquipmentRankup;
            this.masterRewardCollectGuide = inventoryDatabase.masterRewardCollectGuide;
            this.masterItemData = inventoryDatabase.masterItemData;
            this.masterEquipmentDonation = inventoryDatabase.masterEquipmentDonation;
            this.masterCharaETicketData = inventoryDatabase.masterCharaETicketData;
            this.masterItemETicketData = inventoryDatabase.masterItemETicketData;
            this._databases.Add("master_inventory", (AbstractMasterDatabase) inventoryDatabase);
            break;
          }
          break;
        case "master_jukebox":
          if (this._masterDbProxy != null)
          {
            MasterJukeboxDatabase masterJukeboxDatabase = new MasterJukeboxDatabase(this._masterDbProxy);
            this.masterMusicContent = masterJukeboxDatabase.masterMusicContent;
            this.masterMusicList = masterJukeboxDatabase.masterMusicList;
            this._databases.Add("master_jukebox", (AbstractMasterDatabase) masterJukeboxDatabase);
            break;
          }
          break;
        case "master_kaiser":
          if (this._masterDbProxy != null)
          {
            MasterKaiserDatabase masterKaiserDatabase = new MasterKaiserDatabase(this._masterDbProxy);
            this.masterKaiserAddTimesData = masterKaiserDatabase.masterKaiserAddTimesData;
            this.masterKaiserQuestData = masterKaiserDatabase.masterKaiserQuestData;
            this.masterKaiserRestrictionGroup = masterKaiserDatabase.masterKaiserRestrictionGroup;
            this.masterKaiserSchedule = masterKaiserDatabase.masterKaiserSchedule;
            this.masterKaiserExterminationReward = masterKaiserDatabase.masterKaiserExterminationReward;
            this.masterKaiserSpecialBattle = masterKaiserDatabase.masterKaiserSpecialBattle;
            this._databases.Add("master_kaiser", (AbstractMasterDatabase) masterKaiserDatabase);
            break;
          }
          break;
        case "master_kmk":
          if (this._masterDbProxy != null)
          {
            MasterKmkDatabase masterKmkDatabase = new MasterKmkDatabase(this._masterDbProxy);
            this.masterKmkReward = masterKmkDatabase.masterKmkReward;
            this.masterKmkNaviComment = masterKmkDatabase.masterKmkNaviComment;
            this._databases.Add("master_kmk", (AbstractMasterDatabase) masterKmkDatabase);
            break;
          }
          break;
        case "master_login_bonus":
          if (this._masterDbProxy != null)
          {
            MasterLoginBonusDatabase loginBonusDatabase = new MasterLoginBonusDatabase(this._masterDbProxy);
            this.masterLoginBonusData = loginBonusDatabase.masterLoginBonusData;
            this.masterLoginBonusDetail = loginBonusDatabase.masterLoginBonusDetail;
            this.masterLoginBonusMessageData = loginBonusDatabase.masterLoginBonusMessageData;
            this.masterLoginBonusAdv = loginBonusDatabase.masterLoginBonusAdv;
            this.masterCharaFortuneSchedule = loginBonusDatabase.masterCharaFortuneSchedule;
            this.masterCharaFortuneReward = loginBonusDatabase.masterCharaFortuneReward;
            this.masterCharaFortuneScenario = loginBonusDatabase.masterCharaFortuneScenario;
            this.masterCharaFortuneRail = loginBonusDatabase.masterCharaFortuneRail;
            this.masterBirthdayLoginBonusData = loginBonusDatabase.masterBirthdayLoginBonusData;
            this.masterBirthdayLoginBonusDetail = loginBonusDatabase.masterBirthdayLoginBonusDetail;
            this._databases.Add("master_login_bonus", (AbstractMasterDatabase) loginBonusDatabase);
            break;
          }
          break;
        case "master_mission":
          if (this._masterDbProxy != null)
          {
            MasterMissionDatabase masterMissionDatabase = new MasterMissionDatabase(this._masterDbProxy);
            this.masterDailyMissionData = masterMissionDatabase.masterDailyMissionData;
            this.masterStationaryMissionData = masterMissionDatabase.masterStationaryMissionData;
            this.masterMissionRewardData = masterMissionDatabase.masterMissionRewardData;
            this.masterSeasonPack = masterMissionDatabase.masterSeasonPack;
            this.masterClanBattleBattleMissionData = masterMissionDatabase.masterClanBattleBattleMissionData;
            this.masterCampaignMissionData = masterMissionDatabase.masterCampaignMissionData;
            this.masterCampaignMissionRewardData = masterMissionDatabase.masterCampaignMissionRewardData;
            this.masterCampaignMissionSchedule = masterMissionDatabase.masterCampaignMissionSchedule;
            this.masterCampaignMissionCategory = masterMissionDatabase.masterCampaignMissionCategory;
            this._databases.Add("master_mission", (AbstractMasterDatabase) masterMissionDatabase);
            break;
          }
          break;
        case "master_pct":
          if (this._masterDbProxy != null)
          {
            MasterPctDatabase masterPctDatabase = new MasterPctDatabase(this._masterDbProxy);
            this.masterPctItempoint = masterPctDatabase.masterPctItempoint;
            this.masterPctReward = masterPctDatabase.masterPctReward;
            this.masterPctTapSpeed = masterPctDatabase.masterPctTapSpeed;
            this.masterPctEvaluation = masterPctDatabase.masterPctEvaluation;
            this.masterPctComboCoefficient = masterPctDatabase.masterPctComboCoefficient;
            this.masterPctSystem = masterPctDatabase.masterPctSystem;
            this.masterPctGamingMotion = masterPctDatabase.masterPctGamingMotion;
            this.masterPctSystemFruits = masterPctDatabase.masterPctSystemFruits;
            this.masterPctResult = masterPctDatabase.masterPctResult;
            this._databases.Add("master_pct", (AbstractMasterDatabase) masterPctDatabase);
            break;
          }
          break;
        case "master_pkb":
          if (this._masterDbProxy != null)
          {
            MasterPkbDatabase masterPkbDatabase = new MasterPkbDatabase(this._masterDbProxy);
            this.masterPkbReward = masterPkbDatabase.masterPkbReward;
            this.masterPkbBatterCondition = masterPkbDatabase.masterPkbBatterCondition;
            this.masterPkbPitcherBallType = masterPkbDatabase.masterPkbPitcherBallType;
            this.masterPkbDramaData = masterPkbDatabase.masterPkbDramaData;
            this.masterPkbDrama = masterPkbDatabase.masterPkbDrama;
            this.masterPkbNaviComment = masterPkbDatabase.masterPkbNaviComment;
            this._databases.Add("master_pkb", (AbstractMasterDatabase) masterPkbDatabase);
            break;
          }
          break;
        case "master_quest":
          if (this._masterDbProxy != null)
          {
            MasterQuestDatabase masterQuestDatabase = new MasterQuestDatabase(this._masterDbProxy);
            this.masterQuestData = masterQuestDatabase.masterQuestData;
            this.masterTrainingQuestData = masterQuestDatabase.masterTrainingQuestData;
            this.masterQuestConditionData = masterQuestDatabase.masterQuestConditionData;
            this.masterQuestRewardData = masterQuestDatabase.masterQuestRewardData;
            this.masterWaveGroupData = masterQuestDatabase.masterWaveGroupData;
            this.masterCooperationQuestData = masterQuestDatabase.masterCooperationQuestData;
            this.masterWorldmap = masterQuestDatabase.masterWorldmap;
            this.masterQuestAreaData = masterQuestDatabase.masterQuestAreaData;
            this.masterSkipMonsterData = masterQuestDatabase.masterSkipMonsterData;
            this.masterSkipBossData = masterQuestDatabase.masterSkipBossData;
            this.masterQuestDefeatNotice = masterQuestDatabase.masterQuestDefeatNotice;
            this.masterContentMapData = masterQuestDatabase.masterContentMapData;
            this.masterRarity6QuestData = masterQuestDatabase.masterRarity6QuestData;
            this.masterQuestAnnihilation = masterQuestDatabase.masterQuestAnnihilation;
            this._databases.Add("master_quest", (AbstractMasterDatabase) masterQuestDatabase);
            break;
          }
          break;
        case "master_room":
          if (this._masterDbProxy != null)
          {
            MasterRoomDatabase masterRoomDatabase = new MasterRoomDatabase(this._masterDbProxy);
            this.masterRoomItem = masterRoomDatabase.masterRoomItem;
            this.masterRoomSetup = masterRoomDatabase.masterRoomSetup;
            this.masterRoomChange = masterRoomDatabase.masterRoomChange;
            this.masterRoomItemAnnouncement = masterRoomDatabase.masterRoomItemAnnouncement;
            this.masterRoomItemDetail = masterRoomDatabase.masterRoomItemDetail;
            this.masterRoomUnitComments = masterRoomDatabase.masterRoomUnitComments;
            this.masterRoomChatInfo = masterRoomDatabase.masterRoomChatInfo;
            this.masterRoomChatFormation = masterRoomDatabase.masterRoomChatFormation;
            this.masterRoomChatScenario = masterRoomDatabase.masterRoomChatScenario;
            this.masterRoomEmotionIcon = masterRoomDatabase.masterRoomEmotionIcon;
            this.masterRoomCharacterPersonality = masterRoomDatabase.masterRoomCharacterPersonality;
            this.masterRoomSkinColor = masterRoomDatabase.masterRoomSkinColor;
            this.masterRoomCharacterSkinColor = masterRoomDatabase.masterRoomCharacterSkinColor;
            this.masterRoomEffect = masterRoomDatabase.masterRoomEffect;
            this.masterRoomEffectRewardGet = masterRoomDatabase.masterRoomEffectRewardGet;
            this.masterRoomReleaseData = masterRoomDatabase.masterRoomReleaseData;
            this._databases.Add("master_room", (AbstractMasterDatabase) masterRoomDatabase);
            break;
          }
          break;
        case "master_sekai":
          if (this._masterDbProxy != null)
          {
            MasterSekaiDatabase masterSekaiDatabase = new MasterSekaiDatabase(this._masterDbProxy);
            this.masterSekaiSchedule = masterSekaiDatabase.masterSekaiSchedule;
            this.masterSekaiEnemyParameter = masterSekaiDatabase.masterSekaiEnemyParameter;
            this.masterSekaiTopData = masterSekaiDatabase.masterSekaiTopData;
            this.masterSekaiBossMode = masterSekaiDatabase.masterSekaiBossMode;
            this.masterSekaiAddTimesData = masterSekaiDatabase.masterSekaiAddTimesData;
            this.masterSekaiUnlockStoryCondition = masterSekaiDatabase.masterSekaiUnlockStoryCondition;
            this.masterSekaiBossFixReward = masterSekaiDatabase.masterSekaiBossFixReward;
            this.masterSekaiBossDamageRankReward = masterSekaiDatabase.masterSekaiBossDamageRankReward;
            this.masterSekaiTopStoryData = masterSekaiDatabase.masterSekaiTopStoryData;
            this._databases.Add("master_sekai", (AbstractMasterDatabase) masterSekaiDatabase);
            break;
          }
          break;
        case "master_serial_code":
          if (this._masterDbProxy != null)
          {
            MasterSerialCodeDatabase serialCodeDatabase = new MasterSerialCodeDatabase(this._masterDbProxy);
            this.masterSerialGroupData = serialCodeDatabase.masterSerialGroupData;
            this.masterSerialCodeData = serialCodeDatabase.masterSerialCodeData;
            this._databases.Add("master_serial_code", (AbstractMasterDatabase) serialCodeDatabase);
            break;
          }
          break;
        case "master_shiori":
          if (this._masterDbProxy != null)
          {
            MasterShioriDatabase masterShioriDatabase = new MasterShioriDatabase(this._masterDbProxy);
            this.masterShioriEventList = masterShioriDatabase.masterShioriEventList;
            this.masterShioriItem = masterShioriDatabase.masterShioriItem;
            this.masterShioriQuest = masterShioriDatabase.masterShioriQuest;
            this.masterShioriQuestCondition = masterShioriDatabase.masterShioriQuestCondition;
            this.masterShioriQuestArea = masterShioriDatabase.masterShioriQuestArea;
            this.masterShioriBoss = masterShioriDatabase.masterShioriBoss;
            this.masterShioriBossCondition = masterShioriDatabase.masterShioriBossCondition;
            this.masterShioriStationaryMissionData = masterShioriDatabase.masterShioriStationaryMissionData;
            this.masterShioriBattleMissionData = masterShioriDatabase.masterShioriBattleMissionData;
            this.masterShioriMissionRewardData = masterShioriDatabase.masterShioriMissionRewardData;
            this.masterShioriEnemyParameter = masterShioriDatabase.masterShioriEnemyParameter;
            this.masterShioriWaveGroupData = masterShioriDatabase.masterShioriWaveGroupData;
            this.masterShioriDescription = masterShioriDatabase.masterShioriDescription;
            this.masterShioriUnlockUnitCondition = masterShioriDatabase.masterShioriUnlockUnitCondition;
            this.masterCampaignShioriGroup = masterShioriDatabase.masterCampaignShioriGroup;
            this._databases.Add("master_shiori", (AbstractMasterDatabase) masterShioriDatabase);
            break;
          }
          break;
        case "master_shop":
          if (this._masterDbProxy != null)
          {
            MasterShopDatabase masterShopDatabase = new MasterShopDatabase(this._masterDbProxy);
            this.masterShopStaticPriceGroup = masterShopDatabase.masterShopStaticPriceGroup;
            this.masterFixLineupGroupSet = masterShopDatabase.masterFixLineupGroupSet;
            this._databases.Add("master_shop", (AbstractMasterDatabase) masterShopDatabase);
            break;
          }
          break;
        case "master_skill":
          if (this._masterDbProxy != null)
          {
            MasterSkillDatabase masterSkillDatabase = new MasterSkillDatabase(this._masterDbProxy);
            this.masterSkillAction = masterSkillDatabase.masterSkillAction;
            this.masterSkillData = masterSkillDatabase.masterSkillData;
            this.masterSkillCost = masterSkillDatabase.masterSkillCost;
            this._databases.Add("master_skill", (AbstractMasterDatabase) masterSkillDatabase);
            break;
          }
          break;
        case "master_space":
          if (this._masterDbProxy != null)
          {
            MasterSpaceDatabase masterSpaceDatabase = new MasterSpaceDatabase(this._masterDbProxy);
            this.masterSpaceSchedule = masterSpaceDatabase.masterSpaceSchedule;
            this.masterSpaceTopData = masterSpaceDatabase.masterSpaceTopData;
            this.masterSpaceBattleData = masterSpaceDatabase.masterSpaceBattleData;
            this._databases.Add("master_space", (AbstractMasterDatabase) masterSpaceDatabase);
            break;
          }
          break;
        case "master_srt":
          if (this._masterDbProxy != null)
          {
            MasterSrtDatabase masterSrtDatabase = new MasterSrtDatabase(this._masterDbProxy);
            this.masterSrtPanel = masterSrtDatabase.masterSrtPanel;
            this.masterSrtTopTalk = masterSrtDatabase.masterSrtTopTalk;
            this.masterSrtReward = masterSrtDatabase.masterSrtReward;
            this.masterSrtScore = masterSrtDatabase.masterSrtScore;
            this.masterSrtAction = masterSrtDatabase.masterSrtAction;
            this._databases.Add("master_srt", (AbstractMasterDatabase) masterSrtDatabase);
            break;
          }
          break;
        case "master_stamp":
          if (this._masterDbProxy != null)
          {
            MasterStampDatabase masterStampDatabase = new MasterStampDatabase(this._masterDbProxy);
            this.masterStamp = masterStampDatabase.masterStamp;
            this._databases.Add("master_stamp", (AbstractMasterDatabase) masterStampDatabase);
            break;
          }
          break;
        case "master_story":
          if (this._masterDbProxy != null)
          {
            MasterStoryDatabase masterStoryDatabase = new MasterStoryDatabase(this._masterDbProxy);
            this.masterStoryData = masterStoryDatabase.masterStoryData;
            this.masterStoryDetail = masterStoryDatabase.masterStoryDetail;
            this.masterEventStoryData = masterStoryDatabase.masterEventStoryData;
            this.masterEventStoryDetail = masterStoryDatabase.masterEventStoryDetail;
            this.masterTowerStoryData = masterStoryDatabase.masterTowerStoryData;
            this.masterTowerStoryDetail = masterStoryDatabase.masterTowerStoryDetail;
            this.masterDearStoryData = masterStoryDatabase.masterDearStoryData;
            this.masterDearStoryDetail = masterStoryDatabase.masterDearStoryDetail;
            this.masterStoryQuestData = masterStoryDatabase.masterStoryQuestData;
            this.masterStoryCharacterMask = masterStoryDatabase.masterStoryCharacterMask;
            this.masterCharaStoryStatus = masterStoryDatabase.masterCharaStoryStatus;
            this._databases.Add("master_story", (AbstractMasterDatabase) masterStoryDatabase);
            break;
          }
          break;
        case "master_tower":
          if (this._masterDbProxy != null)
          {
            MasterTowerDatabase masterTowerDatabase = new MasterTowerDatabase(this._masterDbProxy);
            this.masterTowerSchedule = masterTowerDatabase.masterTowerSchedule;
            this.masterTowerAreaData = masterTowerDatabase.masterTowerAreaData;
            this.masterTowerQuestData = masterTowerDatabase.masterTowerQuestData;
            this.masterTowerExQuestData = masterTowerDatabase.masterTowerExQuestData;
            this.masterTowerEnemyParameter = masterTowerDatabase.masterTowerEnemyParameter;
            this.masterTowerQuestFixRewardGroup = masterTowerDatabase.masterTowerQuestFixRewardGroup;
            this.masterTowerQuestOddsGroup = masterTowerDatabase.masterTowerQuestOddsGroup;
            this.masterTowerWaveGroupData = masterTowerDatabase.masterTowerWaveGroupData;
            this.masterTowerCloisterQuestData = masterTowerDatabase.masterTowerCloisterQuestData;
            this._databases.Add("master_tower", (AbstractMasterDatabase) masterTowerDatabase);
            break;
          }
          break;
        case "master_ttk":
          if (this._masterDbProxy != null)
          {
            MasterTtkDatabase masterTtkDatabase = new MasterTtkDatabase(this._masterDbProxy);
            this.masterTtkDrama = masterTtkDatabase.masterTtkDrama;
            this.masterTtkStoryScript = masterTtkDatabase.masterTtkStoryScript;
            this.masterTtkEnemy = masterTtkDatabase.masterTtkEnemy;
            this.masterTtkScore = masterTtkDatabase.masterTtkScore;
            this.masterTtkNaviComment = masterTtkDatabase.masterTtkNaviComment;
            this.masterTtkWeapon = masterTtkDatabase.masterTtkWeapon;
            this.masterTtkStory = masterTtkDatabase.masterTtkStory;
            this.masterTtkReward = masterTtkDatabase.masterTtkReward;
            this._databases.Add("master_ttk", (AbstractMasterDatabase) masterTtkDatabase);
            break;
          }
          break;
        case "master_unit":
          if (this._masterDbProxy != null)
          {
            MasterUnitDatabase masterUnitDatabase = new MasterUnitDatabase(this._masterDbProxy);
            this.masterUnitComments = masterUnitDatabase.masterUnitComments;
            this.masterUnitData = masterUnitDatabase.masterUnitData;
            this.masterCharaIdentity = masterUnitDatabase.masterCharaIdentity;
            this.masterUnitEnemyData = masterUnitDatabase.masterUnitEnemyData;
            this.masterUnitSkillData = masterUnitDatabase.masterUnitSkillData;
            this.masterUnitAttackPattern = masterUnitDatabase.masterUnitAttackPattern;
            this.masterUnitRarity = masterUnitDatabase.masterUnitRarity;
            this.masterUnitPromotion = masterUnitDatabase.masterUnitPromotion;
            this.masterUnitPromotionStatus = masterUnitDatabase.masterUnitPromotionStatus;
            this.masterUnlockSkillData = masterUnitDatabase.masterUnlockSkillData;
            this.masterUnlockUnitCondition = masterUnitDatabase.masterUnlockUnitCondition;
            this.masterUnitMypagePos = masterUnitDatabase.masterUnitMypagePos;
            this.masterUnitProfile = masterUnitDatabase.masterUnitProfile;
            this.masterNaviComment = masterUnitDatabase.masterNaviComment;
            this.masterEventNaviComment = masterUnitDatabase.masterEventNaviComment;
            this.masterSdNaviComment = masterUnitDatabase.masterSdNaviComment;
            this.masterUnitBackground = masterUnitDatabase.masterUnitBackground;
            this.masterGuild = masterUnitDatabase.masterGuild;
            this.masterUnitStatusCoefficient = masterUnitDatabase.masterUnitStatusCoefficient;
            this.masterPositionSetting = masterUnitDatabase.masterPositionSetting;
            this.masterUnitIntroduction = masterUnitDatabase.masterUnitIntroduction;
            this.masterUnitMotionList = masterUnitDatabase.masterUnitMotionList;
            this.masterVoiceGroup = masterUnitDatabase.masterVoiceGroup;
            this.masterVoiceGroupChara = masterUnitDatabase.masterVoiceGroupChara;
            this.masterUnitUniqueEquip = masterUnitDatabase.masterUnitUniqueEquip;
            this.masterUnlockRarity6 = masterUnitDatabase.masterUnlockRarity6;
            this.masterCombinedResultMotion = masterUnitDatabase.masterCombinedResultMotion;
            this._databases.Add("master_unit", (AbstractMasterDatabase) masterUnitDatabase);
            break;
          }
          break;
        case "master_vote":
          if (this._masterDbProxy != null)
          {
            MasterVoteDatabase masterVoteDatabase = new MasterVoteDatabase(this._masterDbProxy);
            this.masterVoteData = masterVoteDatabase.masterVoteData;
            this.masterVoteUnit = masterVoteDatabase.masterVoteUnit;
            this.masterVoteInfo = masterVoteDatabase.masterVoteInfo;
            this._databases.Add("master_vote", (AbstractMasterDatabase) masterVoteDatabase);
            break;
          }
          break;
        default:
          flag = false;
          break;
      }
      return flag;
    }

    public void UnloadDatabases()
    {
      foreach (KeyValuePair<string, AbstractMasterDatabase> database in this._databases)
        database.Value.Unload();
      if (this._masterDbProxy == null)
        return;
      this._masterDbProxy.CloseDB();
    }

    public void ForceResetDatabases()
    {
      this.UnloadDatabases();
      this._databases.Clear();
      this._masterDbProxy = (IOOJBIAKBHA) null;
    }

    public bool IsSetupFinished => this.isSetupFinished;

    public bool Initialize()
    {
      if (this.isSetupFinished)
        return true;
      if (JFDEDPMIGGN.DNDJJBBMGLD.IsUseAssetBundle())
        this.SetupMasterdataDB();
      HashSet<string> knownMasterGroups = this.GetKnownMasterGroups();
      try
      {
        foreach (string groupName in knownMasterGroups)
          this.SetupMasterGroup(groupName);
      }
      catch (Exception ex)
      {
        return false;
      }
      this.masterUnitEnemyData.SetUp();
      this.masterUnitUniqueEquip.Setup();
      this.masterEquipmentEnhanceData.Setup();
      this.masterUniqueEquipmentEnhanceData.Setup();
      this.masterQuestAreaData.Setup();
      this.masterExperienceUnit.Setup();
      this.masterExperienceTeam.Setup();
      this.masterLoveChara.Setup();
      this.masterClanBattleOddsData.Setup();
      this.masterArenaDailyRankReward.SetUp();
      this.masterArenaMaxRankReward.SetUp();
      this.masterArenaMaxSeasonRankReward.SetUp();
      this.masterArenaDefenceReward.SetUp();
      this.masterGrandArenaDailyRankReward.SetUp();
      this.masterGrandArenaMaxRankReward.SetUp();
      this.masterGrandArenaMaxSeasonRankReward.SetUp();
      this.masterGrandArenaDefenceReward.SetUp();
      this.masterNaviComment.Setup();
      this.masterEventNaviComment.Setup();
      this.masterGuild.SetUp();
      this.masterDungeonAreaData.SetUp();
      this.masterEventStoryDetail.Setup();
      this.masterEventStoryData.Setup();
      this.masterDearStoryDetail.Setup();
      this.masterDearStoryData.Setup();
      this.masterTowerStoryDetail.Setup();
      this.masterTowerStoryData.Setup();
      this.masterStoryDetail.Setup();
      this.masterCampaignFreegacha.Setup();
      this.masterCampaignFreegachaData.Setup();
      this.masterSekaiSchedule.Setup();
      this.masterSekaiAddTimesData.Setup();
      this.masterSekaiBossDamageRankReward.Setup();
      this.masterKaiserAddTimesData.Setup();
      this.isSetupFinished = true;
      return true;
    }

    protected void SetupMasterdataDB()
    {
      string path = AssetManager.BuildAssetLocalCachePath("manifest/master.mdb");
      try
      {
        if (File.Exists(path))
          File.Delete(path);
      }
      catch (Exception ex)
      {
      }
      DKFNICOAOBC dkfnicoaobc = new DKFNICOAOBC();
      dkfnicoaobc.OpenCustomVFS("manifest/master.mdb", JFDEDPMIGGN.LKIOCAPGCHN.LoadFileToBytes("a/masterdata_master_0003.cdb"));
      this.SetMasterDB((IOOJBIAKBHA) dkfnicoaobc);
    }*/

    //protected override void onDestroy() => this.ForceResetDatabases();
  }
}
