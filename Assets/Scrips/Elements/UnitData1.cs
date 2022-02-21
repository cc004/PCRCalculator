// Decompiled with JetBrains decompiler
// Type: Elements.UnitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

//using LitJson;

namespace Elements
{
    public class UnitData
    {
        public int Id { get; private set; }

        public DateTime GetTime { get; private set; }

        public int StartRarety { get; private set; }

        public int UnitRarity { get; private set; }

        public int BattleRarity { get; private set; }

        public int UnitLevel { get; private set; }

        public int UnitExp { get; private set; }

        public ePromotionLevel PromotionLevel { get; private set; }

        public List<SkillLevelInfo> UnionBurst { get; private set; }

        public List<SkillLevelInfo> MainSkill { get; private set; }

        public List<SkillLevelInfo> ExSkill { get; private set; }

        public List<SkillLevelInfo> FreeSkill { get; private set; }

        public List<EquipSlot> EquipSlot { get; private set; }

        public List<EquipSlot> UniqueEquipSlot { get; private set; }

        public UnitParam UnitParam { get; private set; }

        public StatusParamShort BonusParam { get; private set; }

        public int ResistStatusId { get; private set; }

        public int Power { get; private set; }

        //public SkinData SkinData { get; private set; }

        public int IdentifyNum { get; private set; }

        public int FavoriteFlag { get; private set; }

        //public UnlockRarity6Slot UnlockRarity6Item { get; private set; }
        public UnitData(int id, DateTime getTime, int startRarety, int unitRarity, int battleRarity, int unitLevel, int unitExp, ePromotionLevel promotionLevel, List<SkillLevelInfo> unionBurst, List<SkillLevelInfo> mainSkill, List<SkillLevelInfo> exSkill, List<SkillLevelInfo> freeSkill ,int resistStatusId)
        {
            Id = id;
            GetTime = getTime;
            StartRarety = startRarety;
            UnitRarity = unitRarity;
            BattleRarity = battleRarity;
            UnitLevel = unitLevel;
            UnitExp = unitExp;
            PromotionLevel = promotionLevel;
            UnionBurst = unionBurst;
            MainSkill = mainSkill;
            ExSkill = exSkill;
            FreeSkill = freeSkill;
            ResistStatusId = resistStatusId;
        }

        public void SetId(int _id) => Id = _id;

        public void SetUnitRarity(int _unitRarity) => UnitRarity = _unitRarity;

        public void SetBattleRarity(int _battleRarity) => BattleRarity = _battleRarity;

        public void SetUnitLevel(int _unitLevel) => UnitLevel = _unitLevel;

        public void SetUnitExp(int _unitExp) => UnitExp = _unitExp;

        public void SetPromotionLevel(ePromotionLevel _promotionLevel) => PromotionLevel = _promotionLevel;

        public void SetUnionBurst(List<SkillLevelInfo> _unionBurst) => UnionBurst = _unionBurst;

        public void SetMainSkill(List<SkillLevelInfo> _mainSkill) => MainSkill = _mainSkill;

        public void SetExSkill(List<SkillLevelInfo> _exSkill) => ExSkill = _exSkill;

        public void SetFreeSkill(List<SkillLevelInfo> _freeSkill) => FreeSkill = _freeSkill;

        /*public void SetEquipSlot(List<Elements.EquipSlot> _equipSlot) => this.EquipSlot = _equipSlot;

        public void SetUniqueEquipSlot(List<Elements.EquipSlot> _uniqueEquipSlot) => this.UniqueEquipSlot = _uniqueEquipSlot;

        public void SetUnitParam(UnitParam _unitParam) => this.UnitParam = _unitParam;

        public void SetBonusParam(StatusParamShort _bonusParam) => this.BonusParam = _bonusParam;

        public void SetResistStatusId(int _resistStatusId) => this.ResistStatusId = (int) _resistStatusId;

        public void SetPower(int _power) => this.Power = (int) _power;

        public void SetSkinData(SkinData _skinData) => this.SkinData = _skinData;

        public void SetFavoriteFlag(int _favoriteFlag) => this.FavoriteFlag = (int) _favoriteFlag;

        public void SetUnlockRarity6Item(UnlockRarity6Slot _unlockRarity6Item) => this.UnlockRarity6Item = _unlockRarity6Item;

        private void initializeUnitData()
        {
          this.Id = (int) 0;
          this.GetTime = new DateTime();
          this.StartRarety = (int) 0;
          this.UnitRarity = (int) 0;
          this.BattleRarity = (int) 0;
          this.UnitLevel = (int) 0;
          this.UnitExp = (int) 0;
          this.PromotionLevel = ePromotionLevel.INVALID_VALUE;
          this.UnionBurst = new List<SkillLevelInfo>();
          this.MainSkill = new List<SkillLevelInfo>();
          this.ExSkill = new List<SkillLevelInfo>();
          this.FreeSkill = new List<SkillLevelInfo>();
          this.EquipSlot = new List<Elements.EquipSlot>();
          this.UniqueEquipSlot = new List<Elements.EquipSlot>();
          this.UnitParam = (UnitParam) null;
          this.BonusParam = (StatusParamShort) null;
          this.ResistStatusId = (int) 0;
          this.Power = (int) -1;
          this.SkinData = (SkinData) null;
          this.IdentifyNum = (int) 0;
          this.FavoriteFlag = (int) 0;
          this.UnlockRarity6Item = (UnlockRarity6Slot) null;
        }

        public UnitData() => this.initializeUnitData();

        public UnitData(JsonData _json)
        {
          this.initializeUnitData();
          this.ParseUnitData(_json);
          this.parseHookUnitData();
        }

        public void ParseUnitData(JsonData _json)
        {
          if (_json.Count == 0)
            return;
          this.Id = (int) _json["id"].ToInt();
          if (_json.Keys.Contains("get_time"))
            this.GetTime = JMFOFKDCHEC.FromUnixTime(_json["get_time"].ToLong());
          if (_json.Keys.Contains("start_rarety"))
            this.StartRarety = (int) _json["start_rarety"].ToInt();
          this.UnitRarity = (int) _json["unit_rarity"].ToInt();
          if (_json.Keys.Contains("battle_rarity"))
            this.BattleRarity = (int) _json["battle_rarity"].ToInt();
          this.UnitLevel = (int) _json["unit_level"].ToInt();
          if (_json.Keys.Contains("unit_exp"))
            this.UnitExp = (int) _json["unit_exp"].ToInt();
          this.PromotionLevel = (ePromotionLevel) _json["promotion_level"].ToInt();
          JsonData jsonData1 = _json["union_burst"];
          this.UnionBurst = new List<SkillLevelInfo>();
          if (jsonData1.IsArray)
          {
            int index = 0;
            for (int count = jsonData1.Count; index < count; ++index)
              this.UnionBurst.Add(new SkillLevelInfo(jsonData1[index]));
          }
          JsonData jsonData2 = _json["main_skill"];
          this.MainSkill = new List<SkillLevelInfo>();
          if (jsonData2.IsArray)
          {
            int index = 0;
            for (int count = jsonData2.Count; index < count; ++index)
              this.MainSkill.Add(new SkillLevelInfo(jsonData2[index]));
          }
          JsonData jsonData3 = _json["ex_skill"];
          this.ExSkill = new List<SkillLevelInfo>();
          if (jsonData3.IsArray)
          {
            int index = 0;
            for (int count = jsonData3.Count; index < count; ++index)
              this.ExSkill.Add(new SkillLevelInfo(jsonData3[index]));
          }
          JsonData jsonData4 = _json["free_skill"];
          this.FreeSkill = new List<SkillLevelInfo>();
          if (jsonData4.IsArray)
          {
            int index = 0;
            for (int count = jsonData4.Count; index < count; ++index)
              this.FreeSkill.Add(new SkillLevelInfo(jsonData4[index]));
          }
          JsonData jsonData5 = _json["equip_slot"];
          this.EquipSlot = new List<Elements.EquipSlot>();
          if (jsonData5.IsArray)
          {
            int index = 0;
            for (int count = jsonData5.Count; index < count; ++index)
              this.EquipSlot.Add(new Elements.EquipSlot(jsonData5[index]));
          }
          if (_json.Keys.Contains("unique_equip_slot"))
          {
            JsonData jsonData6 = _json["unique_equip_slot"];
            this.UniqueEquipSlot = new List<Elements.EquipSlot>();
            if (jsonData6.IsArray)
            {
              int index = 0;
              for (int count = jsonData6.Count; index < count; ++index)
                this.UniqueEquipSlot.Add(new Elements.EquipSlot(jsonData6[index]));
            }
          }
          if (_json.Keys.Contains("unit_param"))
          {
            JsonData _json1 = _json["unit_param"];
            if (_json1 != null)
              this.UnitParam = new UnitParam(_json1);
          }
          if (_json.Keys.Contains("bonus_param"))
          {
            JsonData _json1 = _json["bonus_param"];
            if (_json1 != null)
              this.BonusParam = new StatusParamShort(_json1);
          }
          if (_json.Keys.Contains("resist_status_id"))
            this.ResistStatusId = (int) _json["resist_status_id"].ToInt();
          if (_json.Keys.Contains("power"))
            this.Power = (int) _json["power"].ToInt();
          if (_json.Keys.Contains("skin_data"))
          {
            JsonData _json1 = _json["skin_data"];
            if (_json1 != null)
              this.SkinData = new SkinData(_json1);
          }
          if (_json.Keys.Contains("identify_num"))
            this.IdentifyNum = (int) _json["identify_num"].ToInt();
          if (_json.Keys.Contains("favorite_flag"))
            this.FavoriteFlag = (int) _json["favorite_flag"].ToInt();
          if (!_json.Keys.Contains("unlock_rarity_6_item"))
            return;
          JsonData _json2 = _json["unlock_rarity_6_item"];
          if (_json2 == null)
            return;
          this.UnlockRarity6Item = new UnlockRarity6Slot(_json2);
        }

        private void parseHookUnitData() => UnitUtility.CalcParamAndSkill(this, _isPowerLocal: ((int) this.Power == -1));
        
        public long GetTotalParam(eParamType _paramType)
        {
          switch (_paramType)
          {
            case eParamType.HP:
              return this.TotalHp;
            case eParamType.ATK:
              return (long) this.TotalAtk;
            case eParamType.DEF:
              return (long) this.TotalDef;
            case eParamType.MAGIC_ATK:
              return (long) this.TotalMagicAtk;
            case eParamType.MAGIC_DEF:
              return (long) this.TotalMagicDef;
            case eParamType.PHYSICAL_CRITICAL:
              return (long) this.TotalCritical;
            case eParamType.MAGIC_CRITICAL:
              return (long) this.TotalMagicCritical;
            case eParamType.DODGE:
              return (long) this.TotalDodge;
            case eParamType.LIFE_STEAL:
              return (long) this.TotalLifeSteal;
            case eParamType.WAVE_HP_RECOVERY:
              return (long) this.TotalWaveHpRecovery;
            case eParamType.WAVE_ENERGY_RECOVERY:
              return (long) this.TotalWaveEnergyRecovery;
            case eParamType.PHYSICAL_PENETRATE:
              return (long) this.TotalPhysicalPenetrate;
            case eParamType.MAGIC_PENETRATE:
              return (long) this.TotalMagicPenetrate;
            case eParamType.ENERGY_RECOVERY_RATE:
              return (long) this.TotalEnergyRecoveryRate;
            case eParamType.HP_RECOVERY_RATE:
              return (long) this.TotalHpRecoveryRate;
            case eParamType.ENERGY_REDUCE_RATE:
              return (long) this.TotalEnergyReduceRate;
            case eParamType.ACCURACY:
              return (long) this.TotalAccuracy;
            default:
              return 0;
          }
        }

        public long TotalHp
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            long num = (long) this.UnitParam.BaseParam.Hp + (long) this.UnitParam.EquipParam.Hp;
            if (this.BonusParam != null)
              num += (long) this.BonusParam.Hp;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += (long) ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.HP, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalAtk
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.Atk + (int) this.UnitParam.EquipParam.Atk;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Atk;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.ATK, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalDef
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.Def + (int) this.UnitParam.EquipParam.Def;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Def;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.DEF, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalMagicAtk
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.MagicStr + (int) this.UnitParam.EquipParam.MagicStr;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Matk;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.MAGIC_ATK, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalMagicDef
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.MagicDef + (int) this.UnitParam.EquipParam.MagicDef;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Mdef;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.MAGIC_DEF, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalCritical
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.PhysicalCritical + (int) this.UnitParam.EquipParam.PhysicalCritical;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Crt;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.PHYSICAL_CRITICAL, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalMagicCritical
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.MagicCritical + (int) this.UnitParam.EquipParam.MagicCritical;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Mcrt;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.MAGIC_CRITICAL, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalWaveHpRecovery
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.WaveHpRecovery + (int) this.UnitParam.EquipParam.WaveHpRecovery;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Hrec;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.WAVE_HP_RECOVERY, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalWaveEnergyRecovery
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.WaveEnergyRecovery + (int) this.UnitParam.EquipParam.WaveEnergyRecovery;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Erec;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.WAVE_ENERGY_RECOVERY, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalHpRecoveryRate
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.HpRecoveryRate + (int) this.UnitParam.EquipParam.HpRecoveryRate;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.HrecRate;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.HP_RECOVERY_RATE, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalPhysicalPenetrate
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.PhysicalPenetrate + (int) this.UnitParam.EquipParam.PhysicalPenetrate;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Pnt;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.PHYSICAL_PENETRATE, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalMagicPenetrate
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.MagicPenetrate + (int) this.UnitParam.EquipParam.MagicPenetrate;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Mpnt;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.MAGIC_PENETRATE, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalLifeSteal
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.LifeSteal + (int) this.UnitParam.EquipParam.LifeSteal;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.LifeSteal;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.LIFE_STEAL, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalDodge
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.Dodge + (int) this.UnitParam.EquipParam.Dodge;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Dodge;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.DODGE, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalAccuracy
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.Accuracy + (int) this.UnitParam.EquipParam.Accuracy;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.Accuracy;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.ACCURACY, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalEnergyRecoveryRate
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.EnergyRecoveryRate + (int) this.UnitParam.EquipParam.EnergyRecoveryRate;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.ErecRate;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.ENERGY_RECOVERY_RATE, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public int TotalEnergyReduceRate
        {
          get
          {
            if (this.UnitParam == null)
              return 0;
            int num = (int) this.UnitParam.BaseParam.EnergyReduceRate + (int) this.UnitParam.EquipParam.EnergyReduceRate;
            if (this.BonusParam != null)
              num += (int) this.BonusParam.EredRate;
            if (this.UnlockRarity6Item != null && this.GetCurrentRarity() == 5)
              num += ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetTotalParam(eParamType.ENERGY_REDUCE_RATE, (int) this.Id, this.UnlockRarity6Item);
            return num;
          }
        }

        public UnitDataForView CreateUnitDataForView() => new UnitDataForView((int) this.Id, (int) this.UnitLevel, (int) this.UnitRarity, (int) this.BattleRarity, this.PromotionLevel, (int) this.Power, this.SkinData, this.UniqueEquipSlot);

        private UnitData createCopyData()
        {
          UnitData unitData = this.MemberwiseClone() as UnitData;
          List<SkillLevelInfo> exSkill = unitData.ExSkill;
          if (exSkill != null)
          {
            unitData.SetExSkill(new List<SkillLevelInfo>());
            for (int index = 0; index < exSkill.Count; ++index)
            {
              unitData.ExSkill.Add(new SkillLevelInfo());
              unitData.ExSkill[index].SetSkillId((int) exSkill[index].SkillId);
              unitData.ExSkill[index].SetSkillLevel((int) exSkill[index].SkillLevel);
              unitData.ExSkill[index].SetSlotNumber((int) exSkill[index].SlotNumber);
            }
          }
          return unitData;
        }

        public UnitData CreateRarityUpUnitDataForView(int _nextRarity, int _battleRarity = 0)
        {
          UnitData copyData = this.createCopyData();
          copyData.SetUnitRarity(_nextRarity);
          copyData.SetBattleRarity(_battleRarity);
          UnlockRarity6Slot unlockRarity6Item = copyData.UnlockRarity6Item;
          copyData.SetUnlockRarity6Item(new UnlockRarity6Slot());
          if (_nextRarity == 5 && unlockRarity6Item != null)
          {
            copyData.UnlockRarity6Item.SetQuestClear((int) unlockRarity6Item.QuestClear);
            copyData.UnlockRarity6Item.SetSlot1((int) unlockRarity6Item.Slot1);
            copyData.UnlockRarity6Item.SetSlot2((int) unlockRarity6Item.Slot2);
            copyData.UnlockRarity6Item.SetSlot3((int) unlockRarity6Item.Slot3);
            copyData.UnlockRarity6Item.SetStatus1((int) unlockRarity6Item.Status1);
            copyData.UnlockRarity6Item.SetStatus2((int) unlockRarity6Item.Status2);
            copyData.UnlockRarity6Item.SetStatus3((int) unlockRarity6Item.Status3);
          }
          if (_nextRarity == 6)
          {
            SkillLevelInfo skillLevelInfo1 = copyData.UnionBurst[0];
            copyData.UnionBurst = new List<SkillLevelInfo>((IEnumerable<SkillLevelInfo>) copyData.UnionBurst);
            int unionBurstEvolution = (int) ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData.Get((int) this.Id).union_burst_evolution;
            SkillLevelInfo skillLevelInfo2 = new SkillLevelInfo();
            skillLevelInfo2.SetSkillId(unionBurstEvolution);
            skillLevelInfo2.SetSkillLevel((int) skillLevelInfo1.SkillLevel);
            skillLevelInfo2.SetSlotNumber((int) skillLevelInfo1.SlotNumber);
            copyData.UnionBurst[0] = skillLevelInfo2;
          }
          UnitUtility.CalcParamAndSkill(copyData, _isPowerLocal: true);
          return copyData;
        }

        public void UpdateSkinData(SkinData _skindata)
        {
          if (this.SkinData == null)
            this.SetSkinData(new SkinData());
          this.SkinData.SetIconSkinId((int) _skindata.IconSkinId);
          this.SkinData.SetStillSkinId((int) _skindata.StillSkinId);
          this.SkinData.SetSdSkinId((int) _skindata.SdSkinId);
          this.SkinData.SetMotionId((int) _skindata.MotionId);
        }

        public int GetCurrentRarity()
        {
          if ((int) this.UnitRarity != 5)
            return (int) this.UnitRarity;
          return !Singleton<UserData>.Instance.IsReleaseChangeRarityFlag ? (int) this.UnitRarity : (int) ((int) this.BattleRarity == 0 ? this.UnitRarity : this.BattleRarity);
        }

        public void UpdateUnlockRarityLevel(int _index, int _level)
        {
          if (this.UnlockRarity6Item == null)
            return;
          switch (_index)
          {
            case 1:
              this.UnlockRarity6Item.SetSlot1(_level);
              break;
            case 2:
              this.UnlockRarity6Item.SetSlot2(_level);
              break;
            case 3:
              this.UnlockRarity6Item.SetSlot3(_level);
              break;
          }
        }

        public int GetUnlockRarityLevel(int _index)
        {
          if (this.UnlockRarity6Item == null)
            return 0;
          switch (_index)
          {
            case 1:
              return (int) this.UnlockRarity6Item.Slot1;
            case 2:
              return (int) this.UnlockRarity6Item.Slot2;
            case 3:
              return (int) this.UnlockRarity6Item.Slot3;
            default:
              return 0;
          }
        }

        public void UpdateUnlockRarityStatus(int _index, int _status)
        {
          if (this.UnlockRarity6Item == null)
            return;
          switch (_index)
          {
            case 1:
              this.UnlockRarity6Item.SetStatus1(_status);
              break;
            case 2:
              this.UnlockRarity6Item.SetStatus2(_status);
              break;
            case 3:
              this.UnlockRarity6Item.SetStatus3(_status);
              break;
          }
        }

        public void EquipEnhancedEquipment()
        {
          MasterUnitPromotion.UnitPromotion unitPromotion = ManagerSingleton<MasterDataManager>.Instance.masterUnitPromotion.Get((int) this.Id, (int) this.PromotionLevel);
          for (int index = 0; index < unitPromotion.equip_slot.Count; ++index)
          {
            int num = unitPromotion.equip_slot[index];
            if (this.EquipSlot.Count <= index)
              this.EquipSlot.Add(this.createNotEquipSlotData(num));
            else
              this.EquipSlot[index].SetId(num);
            Elements.EquipSlot equipSlot = this.EquipSlot[index];
            if ((int) equipSlot.Id == 999999)
            {
              equipSlot.SetIsSlot(false);
              equipSlot.SetStatus(8);
            }
            else
            {
              equipSlot.SetIsSlot(true);
              InterfaceEquipmentData equipmentData = ItemUtil.GetEquipmentData((int) equipSlot.Id);
              InterfaceEquipmentEnhanceData equipmentEnhanceData = ItemUtil.GetEquipmentEnhanceData((int) equipSlot.Id);
              int _colorGrade = equipmentData.Promotion - 1;
              if (_colorGrade != 0)
                equipSlot.SetEnhancementLevel(equipmentEnhanceData.LevelMax(_colorGrade));
            }
          }
        }

        private Elements.EquipSlot createNotEquipSlotData(int _equipId)
        {
          Elements.EquipSlot equipSlot = new Elements.EquipSlot();
          equipSlot.SetId(_equipId);
          equipSlot.SetIsSlot(false);
          equipSlot.SetEnhancementPt(0);
          equipSlot.SetRank(0);
          equipSlot.SetEnhancementLevel(0);
          return equipSlot;
        }

        public UnitData(UnitDataLight _unitDataLight, bool _isDownwardRevision = false)
        {
          MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
          int id = (int) _unitDataLight.Id;
          MasterUnitSkillData.UnitSkillData unitSkillData = instance.masterUnitSkillData[id];
          List<int> unionBurstIds = unitSkillData.UnionBurstIds;
          List<int> mainSkillIds = unitSkillData.MainSkillIds;
          List<int> skillEvolutionIds1 = unitSkillData.MainSkillEvolutionIds;
          List<int> exSkillIds = unitSkillData.ExSkillIds;
          List<int> skillEvolutionIds2 = unitSkillData.ExSkillEvolutionIds;
          int num1 = int.MaxValue;
          if (_isDownwardRevision)
            num1 = (int) Singleton<UserData>.Instance.UserInfo.TeamLevel;
          this.initializeUnitData();
          this.Id = _unitDataLight.Id;
          this.GetTime = _unitDataLight.GetTime;
          this.StartRarety = _unitDataLight.StartRarety;
          this.UnitRarity = _unitDataLight.UnitRarity;
          this.BattleRarity = _unitDataLight.BattleRarity;
          this.UnitLevel = (int) Mathf.Min((int) _unitDataLight.UnitLevel, num1);
          this.UnitExp = _unitDataLight.UnitExp;
          this.PromotionLevel = _unitDataLight.PromotionLevel;
          this.setSkillLevelInfo(this.UnionBurst, _unitDataLight.UnionBurst, unionBurstIds, num1);
          if (this.UnionBurst != null && this.UnionBurst.Count > 0 && (int) this.UnitRarity >= 6)
            this.UnionBurst[0].SetSkillId((int) unitSkillData.union_burst_evolution);
          this.setSkillLevelInfo(this.MainSkill, _unitDataLight.MainSkill, mainSkillIds, num1);
          this.setSkillLevelInfo(this.ExSkill, _unitDataLight.ExSkill, exSkillIds, num1);
          if (this.ExSkill != null && this.ExSkill.Count > 0 && this.GetCurrentRarity() >= 5)
            this.ExSkill[0].SetSkillId(skillEvolutionIds2[0]);
          if (this.GetCurrentRarity() == 5)
            this.UnlockRarity6Item = _unitDataLight.UnlockRarity6Item;
          int unitLevel = (int) this.UnitLevel;
          int num2 = UnitUtility.CalcMaxPromotionLevelByLevel(id, (int) this.UnitLevel);
          if ((!_isDownwardRevision ? 0 : (this.PromotionLevel > (ePromotionLevel) num2 ? 1 : 0)) == 0)
          {
            MasterUnitPromotion.UnitPromotion unitPromotion = instance.masterUnitPromotion.Get(id, (int) this.PromotionLevel);
            for (int index = 0; index < unitPromotion.equip_slot.Count; ++index)
            {
              int num3 = unitPromotion.equip_slot[index];
              Elements.EquipSlot equipSlot;
              if (num3 == 999999)
              {
                equipSlot = this.createNotEquipSlotData(999999);
              }
              else
              {
                EquipSlotLight equipSlotLight = _unitDataLight.EquipSlot[index];
                MasterEquipmentData.EquipmentData equipmentData = instance.masterEquipmentData.Get(num3);
                if (equipmentData == null || _isDownwardRevision && unitLevel < (int) equipmentData.RequireLevel)
                {
                  equipSlot = this.createNotEquipSlotData(num3);
                }
                else
                {
                  int _promotion = equipmentData.Promotion - 1;
                  int enhancementPt = (int) equipSlotLight.EnhancementPt;
                  bool isSlot = (bool) equipSlotLight.IsSlot;
                  equipSlot = new Elements.EquipSlot();
                  equipSlot.SetId(num3);
                  equipSlot.SetIsSlot(isSlot);
                  equipSlot.SetEnhancementPt(enhancementPt);
                  equipSlot.SetRank((int) equipSlotLight.Rank);
                  int _enhancementLevel = instance.masterEquipmentEnhanceData.ResultLevel(_promotion, enhancementPt);
                  equipSlot.SetEnhancementLevel(_enhancementLevel);
                }
              }
              this.EquipSlot.Add(equipSlot);
            }
          }
          else
          {
            this.PromotionLevel = (ePromotionLevel) num2;
            MasterUnitPromotion.UnitPromotion unitPromotion = instance.masterUnitPromotion.Get(id, (int) this.PromotionLevel);
            for (int index = 0; index < unitPromotion.equip_slot.Count; ++index)
            {
              int num3 = unitPromotion.equip_slot[index];
              Elements.EquipSlot equipSlot;
              if (num3 == 999999)
              {
                equipSlot = this.createNotEquipSlotData(999999);
              }
              else
              {
                MasterEquipmentData.EquipmentData equipmentData = instance.masterEquipmentData.Get(num3);
                if (equipmentData == null || unitLevel < (int) equipmentData.RequireLevel)
                {
                  equipSlot = this.createNotEquipSlotData(num3);
                }
                else
                {
                  equipSlot = new Elements.EquipSlot();
                  equipSlot.SetId(num3);
                  equipSlot.SetIsSlot(true);
                  equipSlot.SetEnhancementPt(0);
                  equipSlot.SetRank(0);
                  equipSlot.SetEnhancementLevel(0);
                }
              }
              this.EquipSlot.Add(equipSlot);
            }
          }
          if (_unitDataLight.UniqueEquipSlot != null)
          {
            for (int index1 = 0; index1 < _unitDataLight.UniqueEquipSlot.Count; ++index1)
            {
              int num3 = index1 + 1;
              EquipSlotLight equipSlotLight = _unitDataLight.UniqueEquipSlot[index1];
              MasterUnitUniqueEquip.UnitUniqueEquip unitUniqueEquip = instance.masterUnitUniqueEquip.Get(id);
              if (unitUniqueEquip != null && (int) unitUniqueEquip.equip_slot == num3)
              {
                Elements.EquipSlot equipSlot = new Elements.EquipSlot();
                int equipId = (int) unitUniqueEquip.equip_id;
                bool isSlot = (bool) equipSlotLight.IsSlot;
                equipSlot.SetId(equipId);
                equipSlot.SetIsSlot(isSlot);
                if (isSlot)
                {
                  int enhancementPt = (int) equipSlotLight.EnhancementPt;
                  equipSlot.SetEnhancementPt(enhancementPt);
                  equipSlot.SetRank((int) equipSlotLight.Rank);
                  int a = instance.masterUniqueEquipmentEnhanceData.ResultLevel(num3 - 1, enhancementPt);
                  equipSlot.SetEnhancementLevel(Mathf.Min(a, num1));
                  int index2 = num3 - 1;
                  if (index2 >= 0 && index2 < this.MainSkill.Count && index2 < skillEvolutionIds1.Count)
                  {
                    int _skillId = skillEvolutionIds1[index2];
                    this.MainSkill[index2].SetSkillId(_skillId);
                  }
                }
                else
                {
                  equipSlot.SetEnhancementPt(0);
                  equipSlot.SetRank(0);
                  equipSlot.SetEnhancementLevel(0);
                }
                this.UniqueEquipSlot.Add(equipSlot);
              }
            }
          }
          this.BonusParam = _unitDataLight.BonusParam;
          this.ResistStatusId = _unitDataLight.ResistStatusId;
          this.Power = (int) -1;
          this.SkinData = _unitDataLight.SkinData;
          this.IdentifyNum = _unitDataLight.IdentifyNum;
          this.FavoriteFlag = _unitDataLight.FavoriteFlag;
          this.parseHookUnitData();
        }

        private void setSkillLevelInfo(
          List<SkillLevelInfo> _outSkillLevelInfo,
          List<SkillLevelInfoLight> _skillInfoLights,
          List<int> _skillIds,
          int _limitLevel = 2147483647)
        {
          if (_skillInfoLights == null || _skillIds == null)
            return;
          for (int index = 0; index < _skillInfoLights.Count && index < _skillIds.Count; ++index)
          {
            int skillId = _skillIds[index];
            SkillLevelInfoLight skillInfoLight = _skillInfoLights[index];
            if (skillId != 0 && (int) skillInfoLight.SkillLevel != 0)
            {
              SkillLevelInfo skillLevelInfo = new SkillLevelInfo();
              skillLevelInfo.SetSkillId(_skillIds[index]);
              skillLevelInfo.SetSkillLevel(Mathf.Min((int) skillInfoLight.SkillLevel, _limitLevel));
              _outSkillLevelInfo.Add(skillLevelInfo);
            }
          }
        }

        public UnitData(
          MasterEnemyParameter.EnemyParameter _enemyParameter)
        {
          MasterUnitSkillData.UnitSkillData unitSkillData = ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData.Get((int) _enemyParameter.unit_id);
          if (unitSkillData == null)
            return;
          this.initializeUnitData();
          this.Id = _enemyParameter.enemy_id;
          this.UnitRarity = _enemyParameter.rarity;
          this.BattleRarity = (int) 0;
          this.UnitLevel = _enemyParameter.level;
          this.PromotionLevel = (ePromotionLevel) (int) _enemyParameter.promotion_level;
          this.UnitExp = (int) 0;
          this.GetTime = DateTime.MinValue;
          this.UnionBurst = this.createSkillLevelInfo((int) unitSkillData.union_burst, (int) _enemyParameter.union_burst_level);
          this.MainSkill = this.createSkillLevelInfo((int) unitSkillData.main_skill_1, (int) _enemyParameter.main_skill_lv_1, (int) unitSkillData.main_skill_2, (int) _enemyParameter.main_skill_lv_2, (int) unitSkillData.main_skill_3, (int) _enemyParameter.main_skill_lv_3, (int) unitSkillData.main_skill_4, (int) _enemyParameter.main_skill_lv_4, (int) unitSkillData.main_skill_5, (int) _enemyParameter.main_skill_lv_5, (int) unitSkillData.main_skill_6, (int) _enemyParameter.main_skill_lv_6, (int) unitSkillData.main_skill_7, (int) _enemyParameter.main_skill_lv_7, (int) unitSkillData.main_skill_8, (int) _enemyParameter.main_skill_lv_8, (int) unitSkillData.main_skill_9, (int) _enemyParameter.main_skill_lv_9, (int) unitSkillData.main_skill_10, (int) _enemyParameter.main_skill_lv_10);
          this.ExSkill = this.createSkillLevelInfo((int) unitSkillData.ex_skill_1, (int) _enemyParameter.ex_skill_lv_1, (int) unitSkillData.ex_skill_2, (int) _enemyParameter.ex_skill_lv_2, (int) unitSkillData.ex_skill_3, (int) _enemyParameter.ex_skill_lv_3, (int) unitSkillData.ex_skill_4, (int) _enemyParameter.ex_skill_lv_4, (int) unitSkillData.ex_skill_5, (int) _enemyParameter.ex_skill_lv_5);
          this.parseHookUnitData();
        }

        private List<SkillLevelInfo> createSkillLevelInfo(params int[] _args)
        {
          List<SkillLevelInfo> skillLevelInfoList = new List<SkillLevelInfo>();
          for (int index = 0; index < _args.Length; index += 2)
          {
            int _skillId = _args[0];
            int _skillLevel = _args[1];
            if (_skillId != 0 && _skillLevel != 0)
            {
              SkillLevelInfo skillLevelInfo = new SkillLevelInfo();
              skillLevelInfo.SetSkillId(_skillId);
              skillLevelInfo.SetSkillLevel(_skillLevel);
              skillLevelInfoList.Add(skillLevelInfo);
            }
          }
          return skillLevelInfoList;
        }*/
    }
}
