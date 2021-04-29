// Decompiled with JetBrains decompiler
// Type: Elements.MasterUnitDatabase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

//using Sqlite3Plugin;

namespace Elements
{
    public class MasterUnitDatabase : AbstractMasterDatabase
    {
        /*private NAOCHNBMGCB _selectQuery_masterUnitComments;
        private NAOCHNBMGCB _selectQuery_masterUnitData;
        private NAOCHNBMGCB _selectQuery_masterCharaIdentity;
        private NAOCHNBMGCB _selectQuery_masterUnitEnemyData;
        private NAOCHNBMGCB _selectQuery_masterUnitSkillData;
        private NAOCHNBMGCB _selectQuery_masterUnitAttackPattern;
        private NAOCHNBMGCB _selectQuery_masterUnitRarity;
        private NAOCHNBMGCB _selectQuery_masterUnitPromotion;
        private NAOCHNBMGCB _selectQuery_masterUnitPromotionStatus;
        private NAOCHNBMGCB _selectQuery_masterUnlockSkillData;
        private NAOCHNBMGCB _selectQuery_masterUnlockUnitCondition;
        private NAOCHNBMGCB _selectQuery_masterUnitMypagePos;
        private NAOCHNBMGCB _selectQuery_masterUnitProfile;
        private NAOCHNBMGCB _selectQuery_masterNaviComment;
        private NAOCHNBMGCB _selectQuery_masterEventNaviComment;
        private NAOCHNBMGCB _selectQuery_masterSdNaviComment;
        private NAOCHNBMGCB _selectQuery_masterUnitBackground;
        private NAOCHNBMGCB _selectQuery_masterGuild;
        private NAOCHNBMGCB _selectQuery_masterUnitStatusCoefficient;
        private NAOCHNBMGCB _selectQuery_masterPositionSetting;
        private NAOCHNBMGCB _selectQuery_masterUnitIntroduction;
        private NAOCHNBMGCB _selectQuery_masterUnitMotionList;
        private NAOCHNBMGCB _selectQuery_masterVoiceGroup;
        private NAOCHNBMGCB _selectQuery_masterVoiceGroupChara;
        private NAOCHNBMGCB _selectQuery_masterUnitUniqueEquip;
        private NAOCHNBMGCB _selectQuery_masterUnlockRarity6;
        private NAOCHNBMGCB _selectQuery_masterCombinedResultMotion;
        private NAOCHNBMGCB _indexedSelectQuery_unitComments_unitId;
        private NAOCHNBMGCB _indexedSelectQuery_unitComments_unitId_useType;
        private NAOCHNBMGCB _indexedSelectQuery_unitRarity_unitId;
        private NAOCHNBMGCB _indexedSelectQuery_unitRarity_unitMaterialId;
        private NAOCHNBMGCB _indexedSelectQuery_unitPromotion_unitId;
        private NAOCHNBMGCB _indexedSelectQuery_unitIntroduction_gachaId;
        private NAOCHNBMGCB _indexedSelectQuery_unlockRarity6_unitId_slotId;
        private NAOCHNBMGCB _indexedSelectQuery_unlockRarity6_unitId_unlockLevel;
        private NAOCHNBMGCB _indexedSelectQuery_unlockRarity6_materialId;
        private IOOJBIAKBHA _dbProxy;
        private const string _commonSelectPrefixUnitComments = "SELECT `id`,`unit_id`,`use_type`,`voice_id`,`face_id`,`change_time`,`change_face`,`description` FROM `unit_comments`";
        private const string _commonSelectPrefixUnitData = "SELECT `unit_id`,`unit_name`,`kana`,`prefab_id`,`is_limited`,`rarity`,`motion_type`,`se_type`,`move_speed`,`search_area_width`,`atk_type`,`normal_atk_cast_time`,`cutin_1`,`cutin_2`,`cutin1_star6`,`cutin2_star6`,`guild_id`,`exskill_display`,`comment`,`only_disp_owned`,`start_time`,`end_time` FROM `unit_data`";
        private const string _commonSelectPrefixCharaIdentity = "SELECT `unit_id`,`chara_type` FROM `chara_identity`";
        private const string _commonSelectPrefixUnitEnemyData = "SELECT `unit_id`,`unit_name`,`prefab_id`,`motion_type`,`se_type`,`move_speed`,`search_area_width`,`atk_type`,`normal_atk_cast_time`,`cutin`,`cutin_star6`,`visual_change_flag`,`comment` FROM `unit_enemy_data`";
        private const string _commonSelectPrefixUnitSkillData = "SELECT `unit_id`,`union_burst`,`main_skill_1`,`main_skill_2`,`main_skill_3`,`main_skill_4`,`main_skill_5`,`main_skill_6`,`main_skill_7`,`main_skill_8`,`main_skill_9`,`main_skill_10`,`ex_skill_1`,`ex_skill_evolution_1`,`ex_skill_2`,`ex_skill_evolution_2`,`ex_skill_3`,`ex_skill_evolution_3`,`ex_skill_4`,`ex_skill_evolution_4`,`ex_skill_5`,`ex_skill_evolution_5`,`sp_skill_1`,`sp_skill_2`,`sp_skill_3`,`sp_skill_4`,`sp_skill_5`,`union_burst_evolution`,`main_skill_evolution_1`,`main_skill_evolution_2`,`sp_skill_evolution_1`,`sp_skill_evolution_2` FROM `unit_skill_data`";
        private const string _commonSelectPrefixUnitAttackPattern = "SELECT `pattern_id`,`unit_id`,`loop_start`,`loop_end`,`atk_pattern_1`,`atk_pattern_2`,`atk_pattern_3`,`atk_pattern_4`,`atk_pattern_5`,`atk_pattern_6`,`atk_pattern_7`,`atk_pattern_8`,`atk_pattern_9`,`atk_pattern_10`,`atk_pattern_11`,`atk_pattern_12`,`atk_pattern_13`,`atk_pattern_14`,`atk_pattern_15`,`atk_pattern_16`,`atk_pattern_17`,`atk_pattern_18`,`atk_pattern_19`,`atk_pattern_20` FROM `unit_attack_pattern`";
        private const string _commonSelectPrefixUnitRarity = "SELECT `unit_id`,`rarity`,`hp`,`hp_growth`,`atk`,`atk_growth`,`magic_str`,`magic_str_growth`,`def`,`def_growth`,`magic_def`,`magic_def_growth`,`physical_critical`,`physical_critical_growth`,`magic_critical`,`magic_critical_growth`,`wave_hp_recovery`,`wave_hp_recovery_growth`,`wave_energy_recovery`,`wave_energy_recovery_growth`,`dodge`,`dodge_growth`,`physical_penetrate`,`physical_penetrate_growth`,`magic_penetrate`,`magic_penetrate_growth`,`life_steal`,`life_steal_growth`,`hp_recovery_rate`,`hp_recovery_rate_growth`,`energy_recovery_rate`,`energy_recovery_rate_growth`,`energy_reduce_rate`,`energy_reduce_rate_growth`,`unit_material_id`,`consume_num`,`consume_gold`,`accuracy`,`accuracy_growth` FROM `unit_rarity`";
        private const string _commonSelectPrefixUnitPromotion = "SELECT `unit_id`,`promotion_level`,`equip_slot_1`,`equip_slot_2`,`equip_slot_3`,`equip_slot_4`,`equip_slot_5`,`equip_slot_6` FROM `unit_promotion`";
        private const string _commonSelectPrefixUnitPromotionStatus = "SELECT `unit_id`,`promotion_level`,`hp`,`atk`,`magic_str`,`def`,`magic_def`,`physical_critical`,`magic_critical`,`wave_hp_recovery`,`wave_energy_recovery`,`dodge`,`physical_penetrate`,`magic_penetrate`,`life_steal`,`hp_recovery_rate`,`energy_recovery_rate`,`energy_reduce_rate`,`accuracy` FROM `unit_promotion_status`";
        private const string _commonSelectPrefixUnlockSkillData = "SELECT `promotion_level`,`unlock_skill` FROM `unlock_skill_data`";
        private const string _commonSelectPrefixUnlockUnitCondition = "SELECT `unit_id`,`unit_name`,`class_id`,`pre_unit_id`,`condition_type_1`,`condition_type_detail_1`,`condition_id_1`,`count_1`,`condition_type_2`,`condition_type_detail_2`,`condition_id_2`,`count_2`,`condition_type_3`,`condition_type_detail_3`,`condition_id_3`,`count_3`,`condition_type_4`,`condition_type_detail_4`,`condition_id_4`,`count_4`,`condition_type_5`,`condition_type_detail_5`,`condition_id_5`,`count_5`,`release_effect_type` FROM `unlock_unit_condition`";
        private const string _commonSelectPrefixUnitMypagePos = "SELECT `id`,`pos_x`,`pos_y`,`scale` FROM `unit_mypage_pos`";
        private const string _commonSelectPrefixUnitProfile = "SELECT `unit_id`,`unit_name`,`age`,`guild`,`race`,`height`,`weight`,`birth_month`,`birth_day`,`blood_type`,`favorite`,`voice`,`voice_id`,`catch_copy`,`self_text`,`guild_id` FROM `unit_profile`";
        private const string _commonSelectPrefixNaviComment = "SELECT `comment_id`,`where_type`,`character_id`,`face_type`,`character_name`,`description`,`voice_id`,`start_time`,`end_time`,`pos_x`,`pos_y`,`change_face_time`,`change_face_type`,`event_id` FROM `navi_comment`";
        private const string _commonSelectPrefixEventNaviComment = "SELECT `comment_id`,`where_type`,`character_id`,`face_type`,`character_name`,`description`,`voice_id`,`start_time`,`end_time`,`pos_x`,`pos_y`,`change_face_time`,`change_face_type`,`event_id` FROM `event_navi_comment`";
        private const string _commonSelectPrefixSdNaviComment = "SELECT `comment_id`,`where_type`,`character_id`,`motion_type`,`description`,`voice_id`,`start_time`,`end_time` FROM `sd_navi_comment`";
        private const string _commonSelectPrefixUnitBackground = "SELECT `unit_id`,`unit_name`,`bg_id`,`bg_name`,`position`,`face_type` FROM `unit_background`";
        private const string _commonSelectPrefixGuild = "SELECT `guild_id`,`guild_name`,`description`,`guild_master`,`member1`,`member2`,`member3`,`member4`,`member5`,`member6`,`member7`,`member8`,`member9`,`member10`,`member11`,`member12`,`member13`,`member14`,`member15`,`member16`,`member17`,`member18`,`member19`,`member20`,`member21`,`member22`,`member23`,`member24`,`member25`,`member26`,`member27`,`member28`,`member29`,`member30` FROM `guild`";
        private const string _commonSelectPrefixUnitStatusCoefficient = "SELECT `coefficient_id`,`hp_coefficient`,`atk_coefficient`,`magic_str_coefficient`,`def_coefficient`,`magic_def_coefficient`,`physical_critical_coefficient`,`magic_critical_coefficient`,`wave_hp_recovery_coefficient`,`wave_energy_recovery_coefficient`,`dodge_coefficient`,`physical_penetrate_coefficient`,`magic_penetrate_coefficient`,`life_steal_coefficient`,`hp_recovery_rate_coefficient`,`energy_recovery_rate_coefficient`,`energy_reduce_rate_coefficient`,`skill_lv_coefficient`,`exskill_evolution_coefficient`,`overall_coefficient`,`accuracy_coefficient`,`skill1_evolution_coefficient`,`skill1_evolution_slv_coefficient`,`ub_evolution_coefficient`,`ub_evolution_slv_coefficient` FROM `unit_status_coefficient`";
        private const string _commonSelectPrefixPositionSetting = "SELECT `position_setting_id`,`front`,`middle` FROM `position_setting`";
        private const string _commonSelectPrefixUnitIntroduction = "SELECT `id`,`gacha_id`,`introduction_number`,`start_time`,`end_time`,`maximum_chunk_size_1`,`maximum_chunk_size_loop_1`,`maximum_chunk_size_2`,`maximum_chunk_size_loop_2`,`maximum_chunk_size_3`,`maximum_chunk_size_loop_3` FROM `unit_introduction`";
        private const string _commonSelectPrefixUnitMotionList = "SELECT `unit_id`,`sp_motion` FROM `unit_motion_list`";
        private const string _commonSelectPrefixVoiceGroup = "SELECT `group_id`,`group_id_comment`,`group_unit_id_01`,`group_unit_id_02`,`group_unit_id_03`,`group_unit_id_04`,`group_unit_id_05` FROM `voice_group`";
        private const string _commonSelectPrefixVoiceGroupChara = "SELECT `group_unit_id`,`group_unit_id_comment`,`unit_id_01`,`unit_id_02`,`unit_id_03`,`unit_id_04`,`unit_id_05`,`unit_id_06`,`unit_id_07`,`unit_id_08`,`unit_id_09`,`unit_id_10` FROM `voice_group_chara`";
        private const string _commonSelectPrefixUnitUniqueEquip = "SELECT `unit_id`,`equip_slot`,`equip_id` FROM `unit_unique_equip`";
        private const string _commonSelectPrefixUnlockRarity6 = "SELECT `unit_id`,`slot_id`,`unlock_level`,`unlock_flag`,`consume_gold`,`material_type`,`material_id`,`material_count`,`hp`,`atk`,`magic_str`,`def`,`magic_def`,`physical_critical`,`magic_critical`,`wave_hp_recovery`,`wave_energy_recovery`,`dodge`,`physical_penetrate`,`magic_penetrate`,`life_steal`,`hp_recovery_rate`,`energy_recovery_rate`,`energy_reduce_rate`,`accuracy` FROM `unlock_rarity_6`";
        private const string _commonSelectPrefixCombinedResultMotion = "SELECT `result_id`,`unit_id_1`,`disp_order_1`,`unit_id_2`,`disp_order_2`,`unit_id_3`,`disp_order_3`,`unit_id_4`,`disp_order_4`,`unit_id_5`,`disp_order_5` FROM `combined_result_motion`";

        public MasterUnitComments masterUnitComments { get; private set; }

        public MasterUnitData masterUnitData { get; private set; }

        public MasterCharaIdentity masterCharaIdentity { get; private set; }

        public MasterUnitEnemyData masterUnitEnemyData { get; private set; }

        public MasterUnitSkillData masterUnitSkillData { get; private set; }

        public MasterUnitAttackPattern masterUnitAttackPattern { get; private set; }

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

        public MasterUnitDatabase(IOOJBIAKBHA dbProxy)
        {
          this._dbProxy = dbProxy;
          this.masterUnitComments = new MasterUnitComments(this);
          this.masterUnitData = new MasterUnitData(this);
          this.masterCharaIdentity = new MasterCharaIdentity(this);
          this.masterUnitEnemyData = new MasterUnitEnemyData(this);
          this.masterUnitSkillData = new MasterUnitSkillData(this);
          this.masterUnitAttackPattern = new MasterUnitAttackPattern(this);
          this.masterUnitRarity = new MasterUnitRarity(this);
          this.masterUnitPromotion = new MasterUnitPromotion(this);
          this.masterUnitPromotionStatus = new MasterUnitPromotionStatus(this);
          this.masterUnlockSkillData = new MasterUnlockSkillData(this);
          this.masterUnlockUnitCondition = new MasterUnlockUnitCondition(this);
          this.masterUnitMypagePos = new MasterUnitMypagePos(this);
          this.masterUnitProfile = new MasterUnitProfile(this);
          this.masterNaviComment = new MasterNaviComment(this);
          this.masterEventNaviComment = new MasterEventNaviComment(this);
          this.masterSdNaviComment = new MasterSdNaviComment(this);
          this.masterUnitBackground = new MasterUnitBackground(this);
          this.masterGuild = new MasterGuild(this);
          this.masterUnitStatusCoefficient = new MasterUnitStatusCoefficient(this);
          this.masterPositionSetting = new MasterPositionSetting(this);
          this.masterUnitIntroduction = new MasterUnitIntroduction(this);
          this.masterUnitMotionList = new MasterUnitMotionList(this);
          this.masterVoiceGroup = new MasterVoiceGroup(this);
          this.masterVoiceGroupChara = new MasterVoiceGroupChara(this);
          this.masterUnitUniqueEquip = new MasterUnitUniqueEquip(this);
          this.masterUnlockRarity6 = new MasterUnlockRarity6(this);
          this.masterCombinedResultMotion = new MasterCombinedResultMotion(this);
        }

        public override void Unload()
        {
          if (this._selectQuery_masterUnitComments != null)
          {
            this._selectQuery_masterUnitComments.Dispose();
            this._selectQuery_masterUnitComments = (NAOCHNBMGCB) null;
          }
          if (this._indexedSelectQuery_unitComments_unitId != null)
          {
            this._indexedSelectQuery_unitComments_unitId.Dispose();
            this._indexedSelectQuery_unitComments_unitId = (NAOCHNBMGCB) null;
          }
          if (this._indexedSelectQuery_unitComments_unitId_useType != null)
          {
            this._indexedSelectQuery_unitComments_unitId_useType.Dispose();
            this._indexedSelectQuery_unitComments_unitId_useType = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitData != null)
          {
            this._selectQuery_masterUnitData.Dispose();
            this._selectQuery_masterUnitData = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterCharaIdentity != null)
          {
            this._selectQuery_masterCharaIdentity.Dispose();
            this._selectQuery_masterCharaIdentity = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitEnemyData != null)
          {
            this._selectQuery_masterUnitEnemyData.Dispose();
            this._selectQuery_masterUnitEnemyData = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitSkillData != null)
          {
            this._selectQuery_masterUnitSkillData.Dispose();
            this._selectQuery_masterUnitSkillData = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitAttackPattern != null)
          {
            this._selectQuery_masterUnitAttackPattern.Dispose();
            this._selectQuery_masterUnitAttackPattern = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitRarity != null)
          {
            this._selectQuery_masterUnitRarity.Dispose();
            this._selectQuery_masterUnitRarity = (NAOCHNBMGCB) null;
          }
          if (this._indexedSelectQuery_unitRarity_unitId != null)
          {
            this._indexedSelectQuery_unitRarity_unitId.Dispose();
            this._indexedSelectQuery_unitRarity_unitId = (NAOCHNBMGCB) null;
          }
          if (this._indexedSelectQuery_unitRarity_unitMaterialId != null)
          {
            this._indexedSelectQuery_unitRarity_unitMaterialId.Dispose();
            this._indexedSelectQuery_unitRarity_unitMaterialId = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitPromotion != null)
          {
            this._selectQuery_masterUnitPromotion.Dispose();
            this._selectQuery_masterUnitPromotion = (NAOCHNBMGCB) null;
          }
          if (this._indexedSelectQuery_unitPromotion_unitId != null)
          {
            this._indexedSelectQuery_unitPromotion_unitId.Dispose();
            this._indexedSelectQuery_unitPromotion_unitId = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitPromotionStatus != null)
          {
            this._selectQuery_masterUnitPromotionStatus.Dispose();
            this._selectQuery_masterUnitPromotionStatus = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnlockSkillData != null)
          {
            this._selectQuery_masterUnlockSkillData.Dispose();
            this._selectQuery_masterUnlockSkillData = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnlockUnitCondition != null)
          {
            this._selectQuery_masterUnlockUnitCondition.Dispose();
            this._selectQuery_masterUnlockUnitCondition = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitMypagePos != null)
          {
            this._selectQuery_masterUnitMypagePos.Dispose();
            this._selectQuery_masterUnitMypagePos = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitProfile != null)
          {
            this._selectQuery_masterUnitProfile.Dispose();
            this._selectQuery_masterUnitProfile = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterNaviComment != null)
          {
            this._selectQuery_masterNaviComment.Dispose();
            this._selectQuery_masterNaviComment = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterEventNaviComment != null)
          {
            this._selectQuery_masterEventNaviComment.Dispose();
            this._selectQuery_masterEventNaviComment = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterSdNaviComment != null)
          {
            this._selectQuery_masterSdNaviComment.Dispose();
            this._selectQuery_masterSdNaviComment = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitBackground != null)
          {
            this._selectQuery_masterUnitBackground.Dispose();
            this._selectQuery_masterUnitBackground = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterGuild != null)
          {
            this._selectQuery_masterGuild.Dispose();
            this._selectQuery_masterGuild = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitStatusCoefficient != null)
          {
            this._selectQuery_masterUnitStatusCoefficient.Dispose();
            this._selectQuery_masterUnitStatusCoefficient = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterPositionSetting != null)
          {
            this._selectQuery_masterPositionSetting.Dispose();
            this._selectQuery_masterPositionSetting = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitIntroduction != null)
          {
            this._selectQuery_masterUnitIntroduction.Dispose();
            this._selectQuery_masterUnitIntroduction = (NAOCHNBMGCB) null;
          }
          if (this._indexedSelectQuery_unitIntroduction_gachaId != null)
          {
            this._indexedSelectQuery_unitIntroduction_gachaId.Dispose();
            this._indexedSelectQuery_unitIntroduction_gachaId = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitMotionList != null)
          {
            this._selectQuery_masterUnitMotionList.Dispose();
            this._selectQuery_masterUnitMotionList = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterVoiceGroup != null)
          {
            this._selectQuery_masterVoiceGroup.Dispose();
            this._selectQuery_masterVoiceGroup = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterVoiceGroupChara != null)
          {
            this._selectQuery_masterVoiceGroupChara.Dispose();
            this._selectQuery_masterVoiceGroupChara = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnitUniqueEquip != null)
          {
            this._selectQuery_masterUnitUniqueEquip.Dispose();
            this._selectQuery_masterUnitUniqueEquip = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterUnlockRarity6 != null)
          {
            this._selectQuery_masterUnlockRarity6.Dispose();
            this._selectQuery_masterUnlockRarity6 = (NAOCHNBMGCB) null;
          }
          if (this._indexedSelectQuery_unlockRarity6_unitId_slotId != null)
          {
            this._indexedSelectQuery_unlockRarity6_unitId_slotId.Dispose();
            this._indexedSelectQuery_unlockRarity6_unitId_slotId = (NAOCHNBMGCB) null;
          }
          if (this._indexedSelectQuery_unlockRarity6_unitId_unlockLevel != null)
          {
            this._indexedSelectQuery_unlockRarity6_unitId_unlockLevel.Dispose();
            this._indexedSelectQuery_unlockRarity6_unitId_unlockLevel = (NAOCHNBMGCB) null;
          }
          if (this._indexedSelectQuery_unlockRarity6_materialId != null)
          {
            this._indexedSelectQuery_unlockRarity6_materialId.Dispose();
            this._indexedSelectQuery_unlockRarity6_materialId = (NAOCHNBMGCB) null;
          }
          if (this._selectQuery_masterCombinedResultMotion != null)
          {
            this._selectQuery_masterCombinedResultMotion.Dispose();
            this._selectQuery_masterCombinedResultMotion = (NAOCHNBMGCB) null;
          }
          if (this.masterUnitComments != null)
            this.masterUnitComments.Unload();
          if (this.masterUnitData != null)
            this.masterUnitData.Unload();
          if (this.masterCharaIdentity != null)
            this.masterCharaIdentity.Unload();
          if (this.masterUnitEnemyData != null)
            this.masterUnitEnemyData.Unload();
          if (this.masterUnitSkillData != null)
            this.masterUnitSkillData.Unload();
          if (this.masterUnitAttackPattern != null)
            this.masterUnitAttackPattern.Unload();
          if (this.masterUnitRarity != null)
            this.masterUnitRarity.Unload();
          if (this.masterUnitPromotion != null)
            this.masterUnitPromotion.Unload();
          if (this.masterUnitPromotionStatus != null)
            this.masterUnitPromotionStatus.Unload();
          if (this.masterUnlockSkillData != null)
            this.masterUnlockSkillData.Unload();
          if (this.masterUnlockUnitCondition != null)
            this.masterUnlockUnitCondition.Unload();
          if (this.masterUnitMypagePos != null)
            this.masterUnitMypagePos.Unload();
          if (this.masterUnitProfile != null)
            this.masterUnitProfile.Unload();
          if (this.masterNaviComment != null)
            this.masterNaviComment.Unload();
          if (this.masterEventNaviComment != null)
            this.masterEventNaviComment.Unload();
          if (this.masterSdNaviComment != null)
            this.masterSdNaviComment.Unload();
          if (this.masterUnitBackground != null)
            this.masterUnitBackground.Unload();
          if (this.masterGuild != null)
            this.masterGuild.Unload();
          if (this.masterUnitStatusCoefficient != null)
            this.masterUnitStatusCoefficient.Unload();
          if (this.masterPositionSetting != null)
            this.masterPositionSetting.Unload();
          if (this.masterUnitIntroduction != null)
            this.masterUnitIntroduction.Unload();
          if (this.masterUnitMotionList != null)
            this.masterUnitMotionList.Unload();
          if (this.masterVoiceGroup != null)
            this.masterVoiceGroup.Unload();
          if (this.masterVoiceGroupChara != null)
            this.masterVoiceGroupChara.Unload();
          if (this.masterUnitUniqueEquip != null)
            this.masterUnitUniqueEquip.Unload();
          if (this.masterUnlockRarity6 != null)
            this.masterUnlockRarity6.Unload();
          if (this.masterCombinedResultMotion == null)
            return;
          this.masterCombinedResultMotion.Unload();
        }

        public override ODBKLOJPCHG Query(string sql) => this._dbProxy.Query(sql);

        public NAOCHNBMGCB GetSelectQuery_UnitComments()
        {
          if (this._selectQuery_masterUnitComments == null)
            this._selectQuery_masterUnitComments = this._dbProxy.PreparedQuery("SELECT `id`,`unit_id`,`use_type`,`voice_id`,`face_id`,`change_time`,`change_face`,`description` FROM `unit_comments` WHERE `id`=?");
          return this._selectQuery_masterUnitComments;
        }

        public NAOCHNBMGCB GetSelectQueryWithIndex_UnitComments_UnitId()
        {
          if (this._indexedSelectQuery_unitComments_unitId == null)
            this._indexedSelectQuery_unitComments_unitId = this._dbProxy.PreparedQuery("SELECT `id`,`unit_id`,`use_type`,`voice_id`,`face_id`,`change_time`,`change_face`,`description` FROM `unit_comments` WHERE `unit_id`=?");
          return this._indexedSelectQuery_unitComments_unitId;
        }

        public NAOCHNBMGCB GetSelectQueryWithIndex_UnitComments_UnitId_UseType()
        {
          if (this._indexedSelectQuery_unitComments_unitId_useType == null)
            this._indexedSelectQuery_unitComments_unitId_useType = this._dbProxy.PreparedQuery("SELECT `id`,`unit_id`,`use_type`,`voice_id`,`face_id`,`change_time`,`change_face`,`description` FROM `unit_comments` WHERE `unit_id`=? AND `use_type`=? ORDER BY `voice_id` ASC");
          return this._indexedSelectQuery_unitComments_unitId_useType;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitComments() => this._dbProxy.Query("SELECT `id`,`unit_id`,`use_type`,`voice_id`,`face_id`,`change_time`,`change_face`,`description` FROM `unit_comments`");

        public NAOCHNBMGCB GetSelectQuery_UnitData()
        {
          if (this._selectQuery_masterUnitData == null)
            this._selectQuery_masterUnitData = this._dbProxy.PreparedQuery("SELECT `unit_id`,`unit_name`,`kana`,`prefab_id`,`is_limited`,`rarity`,`motion_type`,`se_type`,`move_speed`,`search_area_width`,`atk_type`,`normal_atk_cast_time`,`cutin_1`,`cutin_2`,`cutin1_star6`,`cutin2_star6`,`guild_id`,`exskill_display`,`comment`,`only_disp_owned`,`start_time`,`end_time` FROM `unit_data` WHERE `unit_id`=?");
          return this._selectQuery_masterUnitData;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitData() => this._dbProxy.Query("SELECT `unit_id`,`unit_name`,`kana`,`prefab_id`,`is_limited`,`rarity`,`motion_type`,`se_type`,`move_speed`,`search_area_width`,`atk_type`,`normal_atk_cast_time`,`cutin_1`,`cutin_2`,`cutin1_star6`,`cutin2_star6`,`guild_id`,`exskill_display`,`comment`,`only_disp_owned`,`start_time`,`end_time` FROM `unit_data`");

        public NAOCHNBMGCB GetSelectQuery_CharaIdentity()
        {
          if (this._selectQuery_masterCharaIdentity == null)
            this._selectQuery_masterCharaIdentity = this._dbProxy.PreparedQuery("SELECT `unit_id`,`chara_type` FROM `chara_identity` WHERE `unit_id`=?");
          return this._selectQuery_masterCharaIdentity;
        }

        public ODBKLOJPCHG GetSelectAllQuery_CharaIdentity() => this._dbProxy.Query("SELECT `unit_id`,`chara_type` FROM `chara_identity`");

        public NAOCHNBMGCB GetSelectQuery_UnitEnemyData()
        {
          if (this._selectQuery_masterUnitEnemyData == null)
            this._selectQuery_masterUnitEnemyData = this._dbProxy.PreparedQuery("SELECT `unit_id`,`unit_name`,`prefab_id`,`motion_type`,`se_type`,`move_speed`,`search_area_width`,`atk_type`,`normal_atk_cast_time`,`cutin`,`cutin_star6`,`visual_change_flag`,`comment` FROM `unit_enemy_data` WHERE `unit_id`=?");
          return this._selectQuery_masterUnitEnemyData;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitEnemyData() => this._dbProxy.Query("SELECT `unit_id`,`unit_name`,`prefab_id`,`motion_type`,`se_type`,`move_speed`,`search_area_width`,`atk_type`,`normal_atk_cast_time`,`cutin`,`cutin_star6`,`visual_change_flag`,`comment` FROM `unit_enemy_data`");

        public NAOCHNBMGCB GetSelectQuery_UnitSkillData()
        {
          if (this._selectQuery_masterUnitSkillData == null)
            this._selectQuery_masterUnitSkillData = this._dbProxy.PreparedQuery("SELECT `unit_id`,`union_burst`,`main_skill_1`,`main_skill_2`,`main_skill_3`,`main_skill_4`,`main_skill_5`,`main_skill_6`,`main_skill_7`,`main_skill_8`,`main_skill_9`,`main_skill_10`,`ex_skill_1`,`ex_skill_evolution_1`,`ex_skill_2`,`ex_skill_evolution_2`,`ex_skill_3`,`ex_skill_evolution_3`,`ex_skill_4`,`ex_skill_evolution_4`,`ex_skill_5`,`ex_skill_evolution_5`,`sp_skill_1`,`sp_skill_2`,`sp_skill_3`,`sp_skill_4`,`sp_skill_5`,`union_burst_evolution`,`main_skill_evolution_1`,`main_skill_evolution_2`,`sp_skill_evolution_1`,`sp_skill_evolution_2` FROM `unit_skill_data` WHERE `unit_id`=?");
          return this._selectQuery_masterUnitSkillData;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitSkillData() => this._dbProxy.Query("SELECT `unit_id`,`union_burst`,`main_skill_1`,`main_skill_2`,`main_skill_3`,`main_skill_4`,`main_skill_5`,`main_skill_6`,`main_skill_7`,`main_skill_8`,`main_skill_9`,`main_skill_10`,`ex_skill_1`,`ex_skill_evolution_1`,`ex_skill_2`,`ex_skill_evolution_2`,`ex_skill_3`,`ex_skill_evolution_3`,`ex_skill_4`,`ex_skill_evolution_4`,`ex_skill_5`,`ex_skill_evolution_5`,`sp_skill_1`,`sp_skill_2`,`sp_skill_3`,`sp_skill_4`,`sp_skill_5`,`union_burst_evolution`,`main_skill_evolution_1`,`main_skill_evolution_2`,`sp_skill_evolution_1`,`sp_skill_evolution_2` FROM `unit_skill_data`");

        public NAOCHNBMGCB GetSelectQuery_UnitAttackPattern()
        {
          if (this._selectQuery_masterUnitAttackPattern == null)
            this._selectQuery_masterUnitAttackPattern = this._dbProxy.PreparedQuery("SELECT `pattern_id`,`unit_id`,`loop_start`,`loop_end`,`atk_pattern_1`,`atk_pattern_2`,`atk_pattern_3`,`atk_pattern_4`,`atk_pattern_5`,`atk_pattern_6`,`atk_pattern_7`,`atk_pattern_8`,`atk_pattern_9`,`atk_pattern_10`,`atk_pattern_11`,`atk_pattern_12`,`atk_pattern_13`,`atk_pattern_14`,`atk_pattern_15`,`atk_pattern_16`,`atk_pattern_17`,`atk_pattern_18`,`atk_pattern_19`,`atk_pattern_20` FROM `unit_attack_pattern` WHERE `pattern_id`=?");
          return this._selectQuery_masterUnitAttackPattern;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitAttackPattern() => this._dbProxy.Query("SELECT `pattern_id`,`unit_id`,`loop_start`,`loop_end`,`atk_pattern_1`,`atk_pattern_2`,`atk_pattern_3`,`atk_pattern_4`,`atk_pattern_5`,`atk_pattern_6`,`atk_pattern_7`,`atk_pattern_8`,`atk_pattern_9`,`atk_pattern_10`,`atk_pattern_11`,`atk_pattern_12`,`atk_pattern_13`,`atk_pattern_14`,`atk_pattern_15`,`atk_pattern_16`,`atk_pattern_17`,`atk_pattern_18`,`atk_pattern_19`,`atk_pattern_20` FROM `unit_attack_pattern`");

        public NAOCHNBMGCB GetSelectQuery_UnitRarity()
        {
          if (this._selectQuery_masterUnitRarity == null)
            this._selectQuery_masterUnitRarity = this._dbProxy.PreparedQuery("SELECT `unit_id`,`rarity`,`hp`,`hp_growth`,`atk`,`atk_growth`,`magic_str`,`magic_str_growth`,`def`,`def_growth`,`magic_def`,`magic_def_growth`,`physical_critical`,`physical_critical_growth`,`magic_critical`,`magic_critical_growth`,`wave_hp_recovery`,`wave_hp_recovery_growth`,`wave_energy_recovery`,`wave_energy_recovery_growth`,`dodge`,`dodge_growth`,`physical_penetrate`,`physical_penetrate_growth`,`magic_penetrate`,`magic_penetrate_growth`,`life_steal`,`life_steal_growth`,`hp_recovery_rate`,`hp_recovery_rate_growth`,`energy_recovery_rate`,`energy_recovery_rate_growth`,`energy_reduce_rate`,`energy_reduce_rate_growth`,`unit_material_id`,`consume_num`,`consume_gold`,`accuracy`,`accuracy_growth` FROM `unit_rarity` WHERE `unit_id`=? AND `rarity`=?");
          return this._selectQuery_masterUnitRarity;
        }

        public NAOCHNBMGCB GetSelectQueryWithIndex_UnitRarity_UnitId()
        {
          if (this._indexedSelectQuery_unitRarity_unitId == null)
            this._indexedSelectQuery_unitRarity_unitId = this._dbProxy.PreparedQuery("SELECT `unit_id`,`rarity`,`hp`,`hp_growth`,`atk`,`atk_growth`,`magic_str`,`magic_str_growth`,`def`,`def_growth`,`magic_def`,`magic_def_growth`,`physical_critical`,`physical_critical_growth`,`magic_critical`,`magic_critical_growth`,`wave_hp_recovery`,`wave_hp_recovery_growth`,`wave_energy_recovery`,`wave_energy_recovery_growth`,`dodge`,`dodge_growth`,`physical_penetrate`,`physical_penetrate_growth`,`magic_penetrate`,`magic_penetrate_growth`,`life_steal`,`life_steal_growth`,`hp_recovery_rate`,`hp_recovery_rate_growth`,`energy_recovery_rate`,`energy_recovery_rate_growth`,`energy_reduce_rate`,`energy_reduce_rate_growth`,`unit_material_id`,`consume_num`,`consume_gold`,`accuracy`,`accuracy_growth` FROM `unit_rarity` WHERE `unit_id`=?");
          return this._indexedSelectQuery_unitRarity_unitId;
        }

        public NAOCHNBMGCB GetSelectQueryWithIndex_UnitRarity_UnitMaterialId()
        {
          if (this._indexedSelectQuery_unitRarity_unitMaterialId == null)
            this._indexedSelectQuery_unitRarity_unitMaterialId = this._dbProxy.PreparedQuery("SELECT `unit_id`,`rarity`,`hp`,`hp_growth`,`atk`,`atk_growth`,`magic_str`,`magic_str_growth`,`def`,`def_growth`,`magic_def`,`magic_def_growth`,`physical_critical`,`physical_critical_growth`,`magic_critical`,`magic_critical_growth`,`wave_hp_recovery`,`wave_hp_recovery_growth`,`wave_energy_recovery`,`wave_energy_recovery_growth`,`dodge`,`dodge_growth`,`physical_penetrate`,`physical_penetrate_growth`,`magic_penetrate`,`magic_penetrate_growth`,`life_steal`,`life_steal_growth`,`hp_recovery_rate`,`hp_recovery_rate_growth`,`energy_recovery_rate`,`energy_recovery_rate_growth`,`energy_reduce_rate`,`energy_reduce_rate_growth`,`unit_material_id`,`consume_num`,`consume_gold`,`accuracy`,`accuracy_growth` FROM `unit_rarity` WHERE `unit_material_id`=?");
          return this._indexedSelectQuery_unitRarity_unitMaterialId;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitRarity() => this._dbProxy.Query("SELECT `unit_id`,`rarity`,`hp`,`hp_growth`,`atk`,`atk_growth`,`magic_str`,`magic_str_growth`,`def`,`def_growth`,`magic_def`,`magic_def_growth`,`physical_critical`,`physical_critical_growth`,`magic_critical`,`magic_critical_growth`,`wave_hp_recovery`,`wave_hp_recovery_growth`,`wave_energy_recovery`,`wave_energy_recovery_growth`,`dodge`,`dodge_growth`,`physical_penetrate`,`physical_penetrate_growth`,`magic_penetrate`,`magic_penetrate_growth`,`life_steal`,`life_steal_growth`,`hp_recovery_rate`,`hp_recovery_rate_growth`,`energy_recovery_rate`,`energy_recovery_rate_growth`,`energy_reduce_rate`,`energy_reduce_rate_growth`,`unit_material_id`,`consume_num`,`consume_gold`,`accuracy`,`accuracy_growth` FROM `unit_rarity`");

        public NAOCHNBMGCB GetSelectQuery_UnitPromotion()
        {
          if (this._selectQuery_masterUnitPromotion == null)
            this._selectQuery_masterUnitPromotion = this._dbProxy.PreparedQuery("SELECT `unit_id`,`promotion_level`,`equip_slot_1`,`equip_slot_2`,`equip_slot_3`,`equip_slot_4`,`equip_slot_5`,`equip_slot_6` FROM `unit_promotion` WHERE `unit_id`=? AND `promotion_level`=?");
          return this._selectQuery_masterUnitPromotion;
        }

        public NAOCHNBMGCB GetSelectQueryWithIndex_UnitPromotion_UnitId()
        {
          if (this._indexedSelectQuery_unitPromotion_unitId == null)
            this._indexedSelectQuery_unitPromotion_unitId = this._dbProxy.PreparedQuery("SELECT `unit_id`,`promotion_level`,`equip_slot_1`,`equip_slot_2`,`equip_slot_3`,`equip_slot_4`,`equip_slot_5`,`equip_slot_6` FROM `unit_promotion` WHERE `unit_id`=? ORDER BY `promotion_level` ASC");
          return this._indexedSelectQuery_unitPromotion_unitId;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitPromotion() => this._dbProxy.Query("SELECT `unit_id`,`promotion_level`,`equip_slot_1`,`equip_slot_2`,`equip_slot_3`,`equip_slot_4`,`equip_slot_5`,`equip_slot_6` FROM `unit_promotion`");

        public NAOCHNBMGCB GetSelectQuery_UnitPromotionStatus()
        {
          if (this._selectQuery_masterUnitPromotionStatus == null)
            this._selectQuery_masterUnitPromotionStatus = this._dbProxy.PreparedQuery("SELECT `unit_id`,`promotion_level`,`hp`,`atk`,`magic_str`,`def`,`magic_def`,`physical_critical`,`magic_critical`,`wave_hp_recovery`,`wave_energy_recovery`,`dodge`,`physical_penetrate`,`magic_penetrate`,`life_steal`,`hp_recovery_rate`,`energy_recovery_rate`,`energy_reduce_rate`,`accuracy` FROM `unit_promotion_status` WHERE `unit_id`=? AND `promotion_level`=?");
          return this._selectQuery_masterUnitPromotionStatus;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitPromotionStatus() => this._dbProxy.Query("SELECT `unit_id`,`promotion_level`,`hp`,`atk`,`magic_str`,`def`,`magic_def`,`physical_critical`,`magic_critical`,`wave_hp_recovery`,`wave_energy_recovery`,`dodge`,`physical_penetrate`,`magic_penetrate`,`life_steal`,`hp_recovery_rate`,`energy_recovery_rate`,`energy_reduce_rate`,`accuracy` FROM `unit_promotion_status`");

        public NAOCHNBMGCB GetSelectQuery_UnlockSkillData()
        {
          if (this._selectQuery_masterUnlockSkillData == null)
            this._selectQuery_masterUnlockSkillData = this._dbProxy.PreparedQuery("SELECT `promotion_level`,`unlock_skill` FROM `unlock_skill_data` WHERE `unlock_skill`=?");
          return this._selectQuery_masterUnlockSkillData;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnlockSkillData() => this._dbProxy.Query("SELECT `promotion_level`,`unlock_skill` FROM `unlock_skill_data`");

        public NAOCHNBMGCB GetSelectQuery_UnlockUnitCondition()
        {
          if (this._selectQuery_masterUnlockUnitCondition == null)
            this._selectQuery_masterUnlockUnitCondition = this._dbProxy.PreparedQuery("SELECT `unit_id`,`unit_name`,`class_id`,`pre_unit_id`,`condition_type_1`,`condition_type_detail_1`,`condition_id_1`,`count_1`,`condition_type_2`,`condition_type_detail_2`,`condition_id_2`,`count_2`,`condition_type_3`,`condition_type_detail_3`,`condition_id_3`,`count_3`,`condition_type_4`,`condition_type_detail_4`,`condition_id_4`,`count_4`,`condition_type_5`,`condition_type_detail_5`,`condition_id_5`,`count_5`,`release_effect_type` FROM `unlock_unit_condition` WHERE `unit_id`=?");
          return this._selectQuery_masterUnlockUnitCondition;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnlockUnitCondition() => this._dbProxy.Query("SELECT `unit_id`,`unit_name`,`class_id`,`pre_unit_id`,`condition_type_1`,`condition_type_detail_1`,`condition_id_1`,`count_1`,`condition_type_2`,`condition_type_detail_2`,`condition_id_2`,`count_2`,`condition_type_3`,`condition_type_detail_3`,`condition_id_3`,`count_3`,`condition_type_4`,`condition_type_detail_4`,`condition_id_4`,`count_4`,`condition_type_5`,`condition_type_detail_5`,`condition_id_5`,`count_5`,`release_effect_type` FROM `unlock_unit_condition`");

        public NAOCHNBMGCB GetSelectQuery_UnitMypagePos()
        {
          if (this._selectQuery_masterUnitMypagePos == null)
            this._selectQuery_masterUnitMypagePos = this._dbProxy.PreparedQuery("SELECT `id`,`pos_x`,`pos_y`,`scale` FROM `unit_mypage_pos` WHERE `id`=?");
          return this._selectQuery_masterUnitMypagePos;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitMypagePos() => this._dbProxy.Query("SELECT `id`,`pos_x`,`pos_y`,`scale` FROM `unit_mypage_pos`");

        public NAOCHNBMGCB GetSelectQuery_UnitProfile()
        {
          if (this._selectQuery_masterUnitProfile == null)
            this._selectQuery_masterUnitProfile = this._dbProxy.PreparedQuery("SELECT `unit_id`,`unit_name`,`age`,`guild`,`race`,`height`,`weight`,`birth_month`,`birth_day`,`blood_type`,`favorite`,`voice`,`voice_id`,`catch_copy`,`self_text`,`guild_id` FROM `unit_profile` WHERE `unit_id`=?");
          return this._selectQuery_masterUnitProfile;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitProfile() => this._dbProxy.Query("SELECT `unit_id`,`unit_name`,`age`,`guild`,`race`,`height`,`weight`,`birth_month`,`birth_day`,`blood_type`,`favorite`,`voice`,`voice_id`,`catch_copy`,`self_text`,`guild_id` FROM `unit_profile`");

        public NAOCHNBMGCB GetSelectQuery_NaviComment()
        {
          if (this._selectQuery_masterNaviComment == null)
            this._selectQuery_masterNaviComment = this._dbProxy.PreparedQuery("SELECT `comment_id`,`where_type`,`character_id`,`face_type`,`character_name`,`description`,`voice_id`,`start_time`,`end_time`,`pos_x`,`pos_y`,`change_face_time`,`change_face_type`,`event_id` FROM `navi_comment` WHERE `comment_id`=?");
          return this._selectQuery_masterNaviComment;
        }

        public ODBKLOJPCHG GetSelectAllQuery_NaviComment() => this._dbProxy.Query("SELECT `comment_id`,`where_type`,`character_id`,`face_type`,`character_name`,`description`,`voice_id`,`start_time`,`end_time`,`pos_x`,`pos_y`,`change_face_time`,`change_face_type`,`event_id` FROM `navi_comment`");

        public NAOCHNBMGCB GetSelectQuery_EventNaviComment()
        {
          if (this._selectQuery_masterEventNaviComment == null)
            this._selectQuery_masterEventNaviComment = this._dbProxy.PreparedQuery("SELECT `comment_id`,`where_type`,`character_id`,`face_type`,`character_name`,`description`,`voice_id`,`start_time`,`end_time`,`pos_x`,`pos_y`,`change_face_time`,`change_face_type`,`event_id` FROM `event_navi_comment` WHERE `comment_id`=?");
          return this._selectQuery_masterEventNaviComment;
        }

        public ODBKLOJPCHG GetSelectAllQuery_EventNaviComment() => this._dbProxy.Query("SELECT `comment_id`,`where_type`,`character_id`,`face_type`,`character_name`,`description`,`voice_id`,`start_time`,`end_time`,`pos_x`,`pos_y`,`change_face_time`,`change_face_type`,`event_id` FROM `event_navi_comment`");

        public NAOCHNBMGCB GetSelectQuery_SdNaviComment()
        {
          if (this._selectQuery_masterSdNaviComment == null)
            this._selectQuery_masterSdNaviComment = this._dbProxy.PreparedQuery("SELECT `comment_id`,`where_type`,`character_id`,`motion_type`,`description`,`voice_id`,`start_time`,`end_time` FROM `sd_navi_comment` WHERE `comment_id`=?");
          return this._selectQuery_masterSdNaviComment;
        }

        public ODBKLOJPCHG GetSelectAllQuery_SdNaviComment() => this._dbProxy.Query("SELECT `comment_id`,`where_type`,`character_id`,`motion_type`,`description`,`voice_id`,`start_time`,`end_time` FROM `sd_navi_comment`");

        public NAOCHNBMGCB GetSelectQuery_UnitBackground()
        {
          if (this._selectQuery_masterUnitBackground == null)
            this._selectQuery_masterUnitBackground = this._dbProxy.PreparedQuery("SELECT `unit_id`,`unit_name`,`bg_id`,`bg_name`,`position`,`face_type` FROM `unit_background` WHERE `unit_id`=?");
          return this._selectQuery_masterUnitBackground;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitBackground() => this._dbProxy.Query("SELECT `unit_id`,`unit_name`,`bg_id`,`bg_name`,`position`,`face_type` FROM `unit_background`");

        public NAOCHNBMGCB GetSelectQuery_Guild()
        {
          if (this._selectQuery_masterGuild == null)
            this._selectQuery_masterGuild = this._dbProxy.PreparedQuery("SELECT `guild_id`,`guild_name`,`description`,`guild_master`,`member1`,`member2`,`member3`,`member4`,`member5`,`member6`,`member7`,`member8`,`member9`,`member10`,`member11`,`member12`,`member13`,`member14`,`member15`,`member16`,`member17`,`member18`,`member19`,`member20`,`member21`,`member22`,`member23`,`member24`,`member25`,`member26`,`member27`,`member28`,`member29`,`member30` FROM `guild` WHERE `guild_id`=?");
          return this._selectQuery_masterGuild;
        }

        public ODBKLOJPCHG GetSelectAllQuery_Guild() => this._dbProxy.Query("SELECT `guild_id`,`guild_name`,`description`,`guild_master`,`member1`,`member2`,`member3`,`member4`,`member5`,`member6`,`member7`,`member8`,`member9`,`member10`,`member11`,`member12`,`member13`,`member14`,`member15`,`member16`,`member17`,`member18`,`member19`,`member20`,`member21`,`member22`,`member23`,`member24`,`member25`,`member26`,`member27`,`member28`,`member29`,`member30` FROM `guild`");

        public NAOCHNBMGCB GetSelectQuery_UnitStatusCoefficient()
        {
          if (this._selectQuery_masterUnitStatusCoefficient == null)
            this._selectQuery_masterUnitStatusCoefficient = this._dbProxy.PreparedQuery("SELECT `coefficient_id`,`hp_coefficient`,`atk_coefficient`,`magic_str_coefficient`,`def_coefficient`,`magic_def_coefficient`,`physical_critical_coefficient`,`magic_critical_coefficient`,`wave_hp_recovery_coefficient`,`wave_energy_recovery_coefficient`,`dodge_coefficient`,`physical_penetrate_coefficient`,`magic_penetrate_coefficient`,`life_steal_coefficient`,`hp_recovery_rate_coefficient`,`energy_recovery_rate_coefficient`,`energy_reduce_rate_coefficient`,`skill_lv_coefficient`,`exskill_evolution_coefficient`,`overall_coefficient`,`accuracy_coefficient`,`skill1_evolution_coefficient`,`skill1_evolution_slv_coefficient`,`ub_evolution_coefficient`,`ub_evolution_slv_coefficient` FROM `unit_status_coefficient` WHERE `coefficient_id`=?");
          return this._selectQuery_masterUnitStatusCoefficient;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitStatusCoefficient() => this._dbProxy.Query("SELECT `coefficient_id`,`hp_coefficient`,`atk_coefficient`,`magic_str_coefficient`,`def_coefficient`,`magic_def_coefficient`,`physical_critical_coefficient`,`magic_critical_coefficient`,`wave_hp_recovery_coefficient`,`wave_energy_recovery_coefficient`,`dodge_coefficient`,`physical_penetrate_coefficient`,`magic_penetrate_coefficient`,`life_steal_coefficient`,`hp_recovery_rate_coefficient`,`energy_recovery_rate_coefficient`,`energy_reduce_rate_coefficient`,`skill_lv_coefficient`,`exskill_evolution_coefficient`,`overall_coefficient`,`accuracy_coefficient`,`skill1_evolution_coefficient`,`skill1_evolution_slv_coefficient`,`ub_evolution_coefficient`,`ub_evolution_slv_coefficient` FROM `unit_status_coefficient`");

        public NAOCHNBMGCB GetSelectQuery_PositionSetting()
        {
          if (this._selectQuery_masterPositionSetting == null)
            this._selectQuery_masterPositionSetting = this._dbProxy.PreparedQuery("SELECT `position_setting_id`,`front`,`middle` FROM `position_setting` WHERE `position_setting_id`=?");
          return this._selectQuery_masterPositionSetting;
        }

        public ODBKLOJPCHG GetSelectAllQuery_PositionSetting() => this._dbProxy.Query("SELECT `position_setting_id`,`front`,`middle` FROM `position_setting`");

        public NAOCHNBMGCB GetSelectQuery_UnitIntroduction()
        {
          if (this._selectQuery_masterUnitIntroduction == null)
            this._selectQuery_masterUnitIntroduction = this._dbProxy.PreparedQuery("SELECT `id`,`gacha_id`,`introduction_number`,`start_time`,`end_time`,`maximum_chunk_size_1`,`maximum_chunk_size_loop_1`,`maximum_chunk_size_2`,`maximum_chunk_size_loop_2`,`maximum_chunk_size_3`,`maximum_chunk_size_loop_3` FROM `unit_introduction` WHERE `id`=?");
          return this._selectQuery_masterUnitIntroduction;
        }

        public NAOCHNBMGCB GetSelectQueryWithIndex_UnitIntroduction_GachaId()
        {
          if (this._indexedSelectQuery_unitIntroduction_gachaId == null)
            this._indexedSelectQuery_unitIntroduction_gachaId = this._dbProxy.PreparedQuery("SELECT `id`,`gacha_id`,`introduction_number`,`start_time`,`end_time`,`maximum_chunk_size_1`,`maximum_chunk_size_loop_1`,`maximum_chunk_size_2`,`maximum_chunk_size_loop_2`,`maximum_chunk_size_3`,`maximum_chunk_size_loop_3` FROM `unit_introduction` WHERE `gacha_id`=?");
          return this._indexedSelectQuery_unitIntroduction_gachaId;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitIntroduction() => this._dbProxy.Query("SELECT `id`,`gacha_id`,`introduction_number`,`start_time`,`end_time`,`maximum_chunk_size_1`,`maximum_chunk_size_loop_1`,`maximum_chunk_size_2`,`maximum_chunk_size_loop_2`,`maximum_chunk_size_3`,`maximum_chunk_size_loop_3` FROM `unit_introduction`");

        public NAOCHNBMGCB GetSelectQuery_UnitMotionList()
        {
          if (this._selectQuery_masterUnitMotionList == null)
            this._selectQuery_masterUnitMotionList = this._dbProxy.PreparedQuery("SELECT `unit_id`,`sp_motion` FROM `unit_motion_list` WHERE `unit_id`=?");
          return this._selectQuery_masterUnitMotionList;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitMotionList() => this._dbProxy.Query("SELECT `unit_id`,`sp_motion` FROM `unit_motion_list`");

        public NAOCHNBMGCB GetSelectQuery_VoiceGroup()
        {
          if (this._selectQuery_masterVoiceGroup == null)
            this._selectQuery_masterVoiceGroup = this._dbProxy.PreparedQuery("SELECT `group_id`,`group_id_comment`,`group_unit_id_01`,`group_unit_id_02`,`group_unit_id_03`,`group_unit_id_04`,`group_unit_id_05` FROM `voice_group` WHERE `group_id`=?");
          return this._selectQuery_masterVoiceGroup;
        }

        public ODBKLOJPCHG GetSelectAllQuery_VoiceGroup() => this._dbProxy.Query("SELECT `group_id`,`group_id_comment`,`group_unit_id_01`,`group_unit_id_02`,`group_unit_id_03`,`group_unit_id_04`,`group_unit_id_05` FROM `voice_group`");

        public NAOCHNBMGCB GetSelectQuery_VoiceGroupChara()
        {
          if (this._selectQuery_masterVoiceGroupChara == null)
            this._selectQuery_masterVoiceGroupChara = this._dbProxy.PreparedQuery("SELECT `group_unit_id`,`group_unit_id_comment`,`unit_id_01`,`unit_id_02`,`unit_id_03`,`unit_id_04`,`unit_id_05`,`unit_id_06`,`unit_id_07`,`unit_id_08`,`unit_id_09`,`unit_id_10` FROM `voice_group_chara` WHERE `group_unit_id`=?");
          return this._selectQuery_masterVoiceGroupChara;
        }

        public ODBKLOJPCHG GetSelectAllQuery_VoiceGroupChara() => this._dbProxy.Query("SELECT `group_unit_id`,`group_unit_id_comment`,`unit_id_01`,`unit_id_02`,`unit_id_03`,`unit_id_04`,`unit_id_05`,`unit_id_06`,`unit_id_07`,`unit_id_08`,`unit_id_09`,`unit_id_10` FROM `voice_group_chara`");

        public NAOCHNBMGCB GetSelectQuery_UnitUniqueEquip()
        {
          if (this._selectQuery_masterUnitUniqueEquip == null)
            this._selectQuery_masterUnitUniqueEquip = this._dbProxy.PreparedQuery("SELECT `unit_id`,`equip_slot`,`equip_id` FROM `unit_unique_equip` WHERE `unit_id`=?");
          return this._selectQuery_masterUnitUniqueEquip;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnitUniqueEquip() => this._dbProxy.Query("SELECT `unit_id`,`equip_slot`,`equip_id` FROM `unit_unique_equip`");

        public NAOCHNBMGCB GetSelectQuery_UnlockRarity6()
        {
          if (this._selectQuery_masterUnlockRarity6 == null)
            this._selectQuery_masterUnlockRarity6 = this._dbProxy.PreparedQuery("SELECT `unit_id`,`slot_id`,`unlock_level`,`unlock_flag`,`consume_gold`,`material_type`,`material_id`,`material_count`,`hp`,`atk`,`magic_str`,`def`,`magic_def`,`physical_critical`,`magic_critical`,`wave_hp_recovery`,`wave_energy_recovery`,`dodge`,`physical_penetrate`,`magic_penetrate`,`life_steal`,`hp_recovery_rate`,`energy_recovery_rate`,`energy_reduce_rate`,`accuracy` FROM `unlock_rarity_6` WHERE `unit_id`=? AND `slot_id`=? AND `unlock_level`=?");
          return this._selectQuery_masterUnlockRarity6;
        }

        public NAOCHNBMGCB GetSelectQueryWithIndex_UnlockRarity6_UnitId_SlotId()
        {
          if (this._indexedSelectQuery_unlockRarity6_unitId_slotId == null)
            this._indexedSelectQuery_unlockRarity6_unitId_slotId = this._dbProxy.PreparedQuery("SELECT `unit_id`,`slot_id`,`unlock_level`,`unlock_flag`,`consume_gold`,`material_type`,`material_id`,`material_count`,`hp`,`atk`,`magic_str`,`def`,`magic_def`,`physical_critical`,`magic_critical`,`wave_hp_recovery`,`wave_energy_recovery`,`dodge`,`physical_penetrate`,`magic_penetrate`,`life_steal`,`hp_recovery_rate`,`energy_recovery_rate`,`energy_reduce_rate`,`accuracy` FROM `unlock_rarity_6` WHERE `unit_id`=? AND `slot_id`=? ORDER BY `unlock_level` ASC");
          return this._indexedSelectQuery_unlockRarity6_unitId_slotId;
        }

        public NAOCHNBMGCB GetSelectQueryWithIndex_UnlockRarity6_UnitId_UnlockLevel()
        {
          if (this._indexedSelectQuery_unlockRarity6_unitId_unlockLevel == null)
            this._indexedSelectQuery_unlockRarity6_unitId_unlockLevel = this._dbProxy.PreparedQuery("SELECT `unit_id`,`slot_id`,`unlock_level`,`unlock_flag`,`consume_gold`,`material_type`,`material_id`,`material_count`,`hp`,`atk`,`magic_str`,`def`,`magic_def`,`physical_critical`,`magic_critical`,`wave_hp_recovery`,`wave_energy_recovery`,`dodge`,`physical_penetrate`,`magic_penetrate`,`life_steal`,`hp_recovery_rate`,`energy_recovery_rate`,`energy_reduce_rate`,`accuracy` FROM `unlock_rarity_6` WHERE `unit_id`=? AND `unlock_level`=? ORDER BY `slot_id` ASC");
          return this._indexedSelectQuery_unlockRarity6_unitId_unlockLevel;
        }

        public NAOCHNBMGCB GetSelectQueryWithIndex_UnlockRarity6_MaterialId()
        {
          if (this._indexedSelectQuery_unlockRarity6_materialId == null)
            this._indexedSelectQuery_unlockRarity6_materialId = this._dbProxy.PreparedQuery("SELECT `unit_id`,`slot_id`,`unlock_level`,`unlock_flag`,`consume_gold`,`material_type`,`material_id`,`material_count`,`hp`,`atk`,`magic_str`,`def`,`magic_def`,`physical_critical`,`magic_critical`,`wave_hp_recovery`,`wave_energy_recovery`,`dodge`,`physical_penetrate`,`magic_penetrate`,`life_steal`,`hp_recovery_rate`,`energy_recovery_rate`,`energy_reduce_rate`,`accuracy` FROM `unlock_rarity_6` WHERE `material_id`=?");
          return this._indexedSelectQuery_unlockRarity6_materialId;
        }

        public ODBKLOJPCHG GetSelectAllQuery_UnlockRarity6() => this._dbProxy.Query("SELECT `unit_id`,`slot_id`,`unlock_level`,`unlock_flag`,`consume_gold`,`material_type`,`material_id`,`material_count`,`hp`,`atk`,`magic_str`,`def`,`magic_def`,`physical_critical`,`magic_critical`,`wave_hp_recovery`,`wave_energy_recovery`,`dodge`,`physical_penetrate`,`magic_penetrate`,`life_steal`,`hp_recovery_rate`,`energy_recovery_rate`,`energy_reduce_rate`,`accuracy` FROM `unlock_rarity_6`");

        public NAOCHNBMGCB GetSelectQuery_CombinedResultMotion()
        {
          if (this._selectQuery_masterCombinedResultMotion == null)
            this._selectQuery_masterCombinedResultMotion = this._dbProxy.PreparedQuery("SELECT `result_id`,`unit_id_1`,`disp_order_1`,`unit_id_2`,`disp_order_2`,`unit_id_3`,`disp_order_3`,`unit_id_4`,`disp_order_4`,`unit_id_5`,`disp_order_5` FROM `combined_result_motion` WHERE `result_id`=?");
          return this._selectQuery_masterCombinedResultMotion;
        }

        public ODBKLOJPCHG GetSelectAllQuery_CombinedResultMotion() => this._dbProxy.Query("SELECT `result_id`,`unit_id_1`,`disp_order_1`,`unit_id_2`,`disp_order_2`,`unit_id_3`,`disp_order_3`,`unit_id_4`,`disp_order_4`,`unit_id_5`,`disp_order_5` FROM `combined_result_motion`");

      */
    }
}
