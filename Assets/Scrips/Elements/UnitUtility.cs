// Decompiled with JetBrains decompiler
// Type: Elements.UnitUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

//using Cute.Cri;
//using Elements.Data;

namespace Elements
{
  public static class UnitUtility
  {
    private const int DEFAULT_RARITY_MAX = 5;
        /*private static MasterEquipmentData.EquipmentData unknownEquipmentData = (MasterEquipmentData.EquipmentData) null;
        private static readonly SoundManager.eVoiceType[] charaDetailVoiceTypes = new SoundManager.eVoiceType[3]
        {
          SoundManager.eVoiceType.GACHA,
          SoundManager.eVoiceType.DETAIL,
          SoundManager.eVoiceType.MYPAGE
        };*/
        public static int GetSkinIdFromRarity(int _rarity)
        {
            int result = 1;
            switch (_rarity)
            {
                case 0:
                case 1:
                case 2:
                    result = 1;
                    break;
                case 3:
                case 4:
                case 5:
                    result = 3;
                    break;
                case 6:
                    result = 6;
                    break;
            }
            return result;
        }
        //public static UserChara SearchCharaParamByUnitId(int _unitId) => Singleton<UserData>.Instance == null ? (UserChara) null : Singleton<UserData>.Instance.SearchCharaParam(UnitUtility.GetCharaId(_unitId));
        public static int GetSkinId(eSpineType _spineType, int _unitId, int _rarity, int _unitIdForParameter = 0, UnitDefine.eSkinType _skin = UnitDefine.eSkinType.Sd, bool _isOther = false)
        {
            if (_rarity == -2)
            {
                _unitIdForParameter = ((_unitIdForParameter == 0) ? _unitId : _unitIdForParameter);
            }
            if (ResourceDefineSpine.IsSkinIdSpineType(_spineType))
            {
                return GetSkinId(_unitId, _rarity);
            }
            if (_spineType == eSpineType.SD_SHADOW && _rarity == 6)
            {
                return GetSkinIdFromRarity(_rarity);
            }
            return _unitId;
        }

        public static eUnitBattlePos GetUnitPosType(float _searchAreaWidth)
    {
      //MasterPositionSetting.PositionSetting positionSetting = ManagerSingleton<MasterDataManager>.Instance.masterPositionSetting.Get(1);
      if (_searchAreaWidth <= (double) 299)//positionSetting.front)
        return eUnitBattlePos.FRONT;
      return _searchAreaWidth > (double) 599//positionSetting.middle 
                ? eUnitBattlePos.BACK : eUnitBattlePos.MIDDLE;
    }

    //public static string GetUnitPosSpriteName(float _searchAreaWidth) => UnitUtility.getUnitPosSpriteName(UnitUtility.GetUnitPosType(_searchAreaWidth));

    /*private static string getUnitPosSpriteName(eUnitBattlePos _unitPosType)
    {
      switch (_unitPosType)
      {
        case eUnitBattlePos.FRONT:
          return ResourceDefine.COMMON_ATLAS[ResourceDefine.eCommonAtlas.IMG_COMMON_POSITION_FRONT];
        case eUnitBattlePos.MIDDLE:
          return ResourceDefine.COMMON_ATLAS[ResourceDefine.eCommonAtlas.IMG_COMMON_POSITION_MIDDLE];
        case eUnitBattlePos.BACK:
          return ResourceDefine.COMMON_ATLAS[ResourceDefine.eCommonAtlas.IMG_COMMON_POSITION_BACK];
        default:
          return "";
      }
    }*/
    /*
    public static string GetUnitBattlePosIconSpriteName(float _searchAreaWidth) => UnitUtility.getUnitBattlePosIconSpriteName(UnitUtility.GetUnitPosType(_searchAreaWidth));

    private static string getUnitBattlePosIconSpriteName(eUnitBattlePos _unitBattlePos)
    {
      switch (_unitBattlePos)
      {
        case eUnitBattlePos.FRONT:
          return ResourceDefine.COMMON_ATLAS[ResourceDefine.eCommonAtlas.IMG_COMMON_BATTLESTYLE_FRONT];
        case eUnitBattlePos.MIDDLE:
          return ResourceDefine.COMMON_ATLAS[ResourceDefine.eCommonAtlas.IMG_COMMON_BATTLESTYLE_MIDDLE];
        case eUnitBattlePos.BACK:
          return ResourceDefine.COMMON_ATLAS[ResourceDefine.eCommonAtlas.IMG_COMMON_BATTLESTYLE_BACK];
        default:
          return "";
      }
    }

    public static string GetPromotionLevelFrameSpriteName(int _promotionLevel) => string.Format(ResourceDefine.CommonIconUnitGrade((ePromotionLevel) _promotionLevel), (object[]) Array.Empty<object>());
    */
    public static int GetUnitResourceId(int _unitId)
    {
      /*if (_unitId >= 1000000)
      {
        MasterEnemyParameter.EnemyParameter fromAllKind = ManagerSingleton<MasterDataManager>.Instance.masterEnemyParameter.GetFromAllKind(_unitId);
        if (fromAllKind != null)
          return (int) fromAllKind.unit_id;
      }*/
      return _unitId;
    }
/*
    public static int GetShadowUnitId(int _unitId)
    {
      _unitId /= 100;
      _unitId = 500000 + _unitId * 100 + 1;
      return _unitId;
    }
    */
    public static int GetCharaId(int _unitId)
    {
      _unitId = GetUnitResourceId(_unitId);
      return _unitId / 100;
    }

    public static int GetUnitId(int _characterId, int _jobId) => _characterId * 100 + _jobId;

    public static int GetClassId(int _unitId)
    {
      _unitId = GetUnitResourceId(_unitId);
      return _unitId % 100;
    }
        public static int GetOriginalUnitId(int _unitId)
        {
            if (!IsConversionUnit(_unitId))
            {
                return _unitId;
            }
            throw new System.Exception("咕咕咕！");
        }
        public static bool IsConversionUnit(int _unitId)
        {
            return _unitId / 10000 == 17;
        }
        /**
            public static UnitDefine.eUnitClassId GetMaxClassId(int _unitId) => UnitUtility.GetMaxClassIdFromCharaId(UnitUtility.GetCharaId(_unitId));

            public static UnitDefine.eUnitClassId GetMaxClassIdFromCharaId(int _charaId)
            {
              int _jobId = 1;
              UserData instance = Singleton<UserData>.Instance;
              while (instance.IsMyUnit(UnitUtility.GetUnitId(_charaId, _jobId)))
                ++_jobId;
              return (UnitDefine.eUnitClassId) (_jobId - 1);
            }*/

        //public static int GetSummonResourceId(int _unitId) => (int) ManagerSingleton<MasterDataManager>.Instance.masterUnitData[UnitUtility.GetUnitResourceId(_unitId)].PrefabId;
        /*
            public static bool IsPersonUnit(int _unitId)
            {
              _unitId = UnitUtility.GetUnitResourceId(_unitId);
              UnitDefine.UnitType unitType = UnitUtility.getUnitType(_unitId);
              return unitType == UnitDefine.UnitType.PERSON || unitType == UnitDefine.UnitType.GUEST;
            }

            public static bool IsPlayableUnit(int _unitId)
            {
              MasterUnitData.UnitData unitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData.Get(_unitId);
              return unitData != null && UnitUtility.IsPersonUnit(_unitId) && (!UnitUtility.JudgeIsGuest(_unitId) && (int) unitData.Rarity > 0) && (uint) (int) unitData.CutIn1 > 0U;
            }
            */
        public static bool IsUnitTypePersonOrSummonPerson(int _unitId)
    {
      UnitDefine.UnitType unitType = getUnitType(_unitId);
      return unitType == UnitDefine.UnitType.PERSON || unitType == UnitDefine.UnitType.SUMMON_PERSON;
    }

    public static bool IsUnitTypeSummonPersonOnly(int _unitId) => getUnitType(_unitId) == UnitDefine.UnitType.SUMMON_PERSON;

    public static bool IsMonsterUnit(int _unitId)
    {
      _unitId = GetUnitResourceId(_unitId);
      return getUnitType(_unitId) == UnitDefine.UnitType.MONSTER;
    }

    public static bool JudgeIsBoss(int _unitId)
    {
      _unitId = GetUnitResourceId(_unitId);
      return getUnitType(_unitId) == UnitDefine.UnitType.BOSS;
    }

    public static bool JudgeIsSummon(int _unitId)
    {
      _unitId = GetUnitResourceId(_unitId);
      UnitDefine.UnitType unitType = getUnitType(_unitId);
      return unitType == UnitDefine.UnitType.SUMMON_PERSON || unitType == UnitDefine.UnitType.SUMMON_MONSTER;
    }
    
    public static bool JudgeIsGuest(int _unitId) => getUnitType(_unitId) == UnitDefine.UnitType.GUEST;

    public static bool IsEnemyUnit(int _unitId)
    {
      UnitDefine.UnitType unitType = getUnitType(_unitId);
      return _unitId >= 1000000 || unitType == UnitDefine.UnitType.MONSTER || unitType == UnitDefine.UnitType.BOSS;
    }

    public static bool IsQuestMonsterUnit(int _unitId) => _unitId >= 1000000;
/*
    public static int GetSpineCharaBaseAnimationNumber(int _unitId)
    {
      if (UnitUtility.JudgeIsGuest(_unitId))
        return _unitId;
      MasterUnitData.UnitData unitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData.Get(_unitId);
      int num = 0;
      if (unitData == null)
      {
        MasterUnitEnemyData.UnitEnemyData unitEnemyData = ManagerSingleton<MasterDataManager>.Instance.masterUnitEnemyData.Get(_unitId);
        if (unitEnemyData != null)
          num = (int) unitEnemyData.motion_type;
      }
      else
        num = (int) unitData.MotionType;
      return num != 0 ? 0 : _unitId;
    }

    public static float CompensationStatusParameterString(eParamType _type)
    {
      float num = 1f;
      switch (_type)
      {
        case eParamType.PHYSICAL_CRITICAL:
        case eParamType.MAGIC_CRITICAL:
        case eParamType.HP_RECOVERY_RATE:
          num = 0.7f;
          break;
        case eParamType.WAVE_ENERGY_RECOVERY:
        case eParamType.MAGIC_PENETRATE:
          num = 0.9f;
          break;
      }
      return num;
    }

    public static List<ContinuousUnit> ConvertDungeonUnitListToContinuous(
      List<DungeonUnit> _dungeonUnitList)
    {
      List<ContinuousUnit> continuousUnitList = new List<ContinuousUnit>();
      for (int index = 0; index < _dungeonUnitList.Count; ++index)
        continuousUnitList.Add(new ContinuousUnit(_dungeonUnitList[index]));
      return continuousUnitList;
    }

    public static List<ContinuousUnit> ConvertTowerUnitListToContinuous(
      List<TowerUnit> _towerUnitList)
    {
      List<ContinuousUnit> continuousUnitList = new List<ContinuousUnit>();
      for (int index = 0; index < _towerUnitList.Count; ++index)
        continuousUnitList.Add(new ContinuousUnit(_towerUnitList[index]));
      return continuousUnitList;
    }

    private static int round(float _f) => (double) _f - (double) (int) _f >= 0.5 || Mathf.Approximately((float) (int) _f + 0.5f, _f) ? (int) _f + 1 : (int) _f;
    */
    private static UnitDefine.UnitType getUnitType(int _unitId) => (UnitDefine.UnitType) (_unitId / 100000);
/*
    public static int GetUnitMaxRank(int _unitId)
    {
      int num = 0;
      foreach (MasterUnitPromotion.UnitPromotion unitPromotion in ManagerSingleton<MasterDataManager>.Instance.masterUnitPromotion.GetAllUnitPromotion(_unitId))
      {
        if (num < (int) unitPromotion.promotion_level)
          num = (int) unitPromotion.promotion_level;
      }
      return num;
    }

    public static int CalcOverall(
      int _unitId,
      int _level,
      int _rarity,
      int _rank,
      UnitData _unitUniqueData)
    {
      float num1 = 0.0f;
      MasterUnitStatusCoefficient statusCoefficient = ManagerSingleton<MasterDataManager>.Instance.masterUnitStatusCoefficient;
      Array values = Enum.GetValues(typeof (eParamType));
      int index1 = 0;
      for (int length = values.Length; index1 < length; ++index1)
      {
        eParamType eParamType = (eParamType) values.GetValue(index1);
        switch (eParamType)
        {
          case eParamType.INVALID_VALUE:
          case eParamType.NONE:
            continue;
          default:
            long num2 = UnitUtility.CalcUnitParameter(eParamType, _unitId, _level, _rarity, _rank, _unitUniqueData.UnitParam.EquipParam, _unitUniqueData.BonusParam, _unitUniqueData.UnlockRarity6Item);
            num1 += (float) num2 * statusCoefficient.GetParameterCoefficient(eParamType);
            continue;
        }
      }
      float num3 = 0.0f;
      List<SkillLevelInfo> unionBurst = _unitUniqueData.UnionBurst;
      int index2 = 0;
      for (int count = unionBurst.Count; index2 < count; ++index2)
      {
        bool flag = SkillDefine.IsEvolutionSkill((int) unionBurst[index2].SkillId);
        num3 += (float) (int) unionBurst[index2].SkillLevel * (flag ? statusCoefficient.GetUbEvolutionCoefficientForSkillLevel() : 1f);
        if (flag)
          num3 += (float) statusCoefficient.GetUbEvolutionCoefficient();
      }
      List<SkillLevelInfo> mainSkill = _unitUniqueData.MainSkill;
      int index3 = 0;
      for (int count = mainSkill.Count; index3 < count; ++index3)
      {
        bool flag = SkillDefine.IsEvolutionSkill((int) mainSkill[index3].SkillId);
        num3 += (float) (int) mainSkill[index3].SkillLevel * (flag ? statusCoefficient.GetSkill1EvolutionCoefficientForSkillLevel() : 1f);
        if (flag)
          num3 += (float) statusCoefficient.GetSkill1EvolutionCoefficient();
      }
      List<SkillLevelInfo> exSkill = _unitUniqueData.ExSkill;
      int index4 = 0;
      for (int count = exSkill.Count; index4 < count; ++index4)
      {
        num3 += (float) (int) exSkill[index4].SkillLevel;
        if ((int) exSkill[index4].SkillLevel > 0 && _rarity >= 5)
          num3 += (float) statusCoefficient.GetExSkillEvolutionCoefficient();
      }
      List<SkillLevelInfo> freeSkill = _unitUniqueData.FreeSkill;
      int index5 = 0;
      for (int count = freeSkill.Count; index5 < count; ++index5)
        num3 += (float) (int) freeSkill[index5].SkillLevel;
      float num4 = num3 * statusCoefficient.GetSkillLevelCoefficient();
      return UnitUtility.round(Mathf.Pow(num1 + num4, statusCoefficient.GetOverallCoefficient()));
    }

    public static bool isPromotionLevelAllEquip(int _unitId, int _promotionLevel, int _unitLevel)
    {
      MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
      MasterUnitPromotion.UnitPromotion unitPromotion = instance.masterUnitPromotion.Get(_unitId, _promotionLevel);
      if (unitPromotion == null)
        return false;
      for (int index = 0; index < unitPromotion.equip_slot.Count; ++index)
      {
        int Id = unitPromotion.equip_slot[index];
        if (Id == 999999)
          return false;
        MasterEquipmentData.EquipmentData equipmentData = instance.masterEquipmentData.Get(Id);
        if (equipmentData == null || _unitLevel < (int) equipmentData.RequireLevel)
          return false;
      }
      return true;
    }

    public static int CalcMaxPromotionLevelByLevel(int _unitId, int _unitLevel)
    {
      List<MasterUnitPromotion.UnitPromotion> allUnitPromotion = ManagerSingleton<MasterDataManager>.Instance.masterUnitPromotion.GetAllUnitPromotion(_unitId);
      int num = 1;
      for (int index = 0; index < allUnitPromotion.Count; ++index)
      {
        MasterUnitPromotion.UnitPromotion unitPromotion = allUnitPromotion[index];
        List<int> equipSlot = unitPromotion.equip_slot;
        if (UnitUtility.isPromotionLevelAllEquip(_unitId, (int) unitPromotion.promotion_level, _unitLevel))
          num = (int) unitPromotion.promotion_level + 1;
      }
      return num;
    }

    public static int CalcUnitBaseParameter(
      eParamType _paramType,
      int _unitId,
      int _level,
      int _rarity,
      int _rank)
    {
      if (_paramType == eParamType.INVALID_VALUE || _paramType == eParamType.NONE)
        return 0;
      MasterDataManager instance1 = ManagerSingleton<MasterDataManager>.Instance;
      UserData instance2 = Singleton<UserData>.Instance;
      float num1 = 0.0f;
      if (_rank > 1)
        num1 = instance1.masterUnitPromotionStatus.Get(_unitId, _rank).GetFloatValue(_paramType);
      float growthParam = UnitUtility.getGrowthParam(_paramType, _unitId, _rarity);
      float initialParam = UnitUtility.getInitialParam(_paramType, _unitId, _rarity);
      float num2 = 0.0f;
      if (UnitUtility.isRankUpBonusParameter(_paramType))
        num2 = growthParam * (float) _rank;
      return UnitUtility.round((float) _level * growthParam + initialParam + num1 + num2);
    }

    public static long CalcUnitParameter(
      eParamType _paramType,
      int _unitId,
      int _level,
      int _rarity,
      int _rank,
      StatusParam _equipParam = null,
      StatusParamShort _bonusParam = null,
      UnlockRarity6Slot _highRaritySlotData = null)
    {
      long num = (long) UnitUtility.CalcUnitBaseParameter(_paramType, _unitId, _level, _rarity, _rank);
      if (_equipParam != null)
        num += _equipParam.GetParam(_paramType);
      if (_bonusParam != null)
        num += _bonusParam.GetParam(_paramType);
      if (_highRaritySlotData != null && _rarity == 5)
        num += (long) UnitUtility.calcHighRarityParameter(_paramType, _unitId, _highRaritySlotData);
      return num;
    }

    private static int calcHighRarityParameter(
      eParamType _paramType,
      int _unitId,
      UnlockRarity6Slot _slotData)
    {
      MasterUnlockRarity6 masterUnlockRarity6 = ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6;
      int num = 0;
      if ((int) _slotData.Slot1 > 0)
        num += masterUnlockRarity6.Get(_unitId, (byte) 1, (byte) (int) _slotData.Slot1).GetParam(_paramType);
      if ((int) _slotData.Slot2 > 0)
        num += masterUnlockRarity6.Get(_unitId, (byte) 2, (byte) (int) _slotData.Slot2).GetParam(_paramType);
      if ((int) _slotData.Slot3 > 0)
        num += masterUnlockRarity6.Get(_unitId, (byte) 3, (byte) (int) _slotData.Slot3).GetParam(_paramType);
      return num;
    }

    public static int CalcParameterNotHaveUnit(
      eParamType _paramType,
      int _unitId,
      int _level,
      int _rarity,
      int _rank)
    {
      if (_paramType < eParamType.HP)
        return 0;
      MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
      float num1 = 0.0f;
      foreach (MasterUnitPromotion.UnitPromotion unitPromotion in instance.masterUnitPromotion.GetAllUnitPromotion(_unitId))
      {
        if ((int) unitPromotion.promotion_level <= _rank)
        {
          for (int index = 0; index < unitPromotion.equip_slot.Count; ++index)
          {
            int num2 = unitPromotion.equip_slot[index];
            switch (num2)
            {
              case 0:
              case 999999:
                continue;
              default:
                InterfaceEquipmentData equipmentData = ItemUtil.GetEquipmentData(num2);
                if (equipmentData != null)
                {
                  if ((int) unitPromotion.promotion_level == _rank)
                  {
                    num1 += UnitUtility.GetEquipmentDataLvMaxValue(num2, _paramType);
                    continue;
                  }
                  num1 += equipmentData.GetFloatValue(_paramType);
                  continue;
                }
                continue;
            }
          }
        }
      }
      float growthParam = UnitUtility.getGrowthParam(_paramType, _unitId, _rarity);
      float initialParam = UnitUtility.getInitialParam(_paramType, _unitId, _rarity);
      float num3 = 0.0f;
      if (UnitUtility.isRankUpBonusParameter(_paramType))
        num3 = growthParam * (float) _rank;
      return UnitUtility.round((float) _level * growthParam + initialParam + num1 + num3);
    }

    public static float GetEquipmentDataLvMaxValue(int _equipId, eParamType _type)
    {
      MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
      InterfaceEquipmentData equipmentData = ItemUtil.GetEquipmentData(_equipId);
      MasterEquipmentEnhanceRate.EquipmentEnhanceRate equipmentEnhanceRate = instance.masterEquipmentEnhanceRate.Get(_equipId);
      int count = instance.masterEquipmentEnhanceData[equipmentData.Promotion - 1].Count;
      if (_type == eParamType.INVALID_VALUE || _type == eParamType.NONE)
        return 0.0f;
      float floatValue1 = equipmentData.GetFloatValue(_type);
      float floatValue2 = equipmentEnhanceRate.GetFloatValue(_type);
      return (double) floatValue1 == 0.0 ? 0.0f : floatValue1 + Mathf.Ceil((float) count * floatValue2);
    }

    private static bool isRankUpBonusParameter(eParamType _paramType)
    {
      switch (_paramType)
      {
        case eParamType.HP:
        case eParamType.ATK:
        case eParamType.DEF:
        case eParamType.MAGIC_ATK:
        case eParamType.MAGIC_DEF:
        case eParamType.PHYSICAL_CRITICAL:
          return true;
        default:
          return false;
      }
    }

    public static int CalcEquipParameter(
      eParamType _paramType,
      List<EquipSlot> _equipSlotList,
      List<EquipSlot> _uniqueEquipSlotList)
    {
      MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
      MasterEquipmentEnhanceRate equipmentEnhanceRate1 = instance.masterEquipmentEnhanceRate;
      MasterUniqueEquipmentEnhanceRate equipmentEnhanceRate2 = instance.masterUniqueEquipmentEnhanceRate;
      MasterEquipmentData masterEquipmentData = instance.masterEquipmentData;
      MasterUniqueEquipmentData uniqueEquipmentData1 = instance.masterUniqueEquipmentData;
      float _f = 0.0f;
      int index1 = 0;
      for (int count = _equipSlotList.Count; index1 < count; ++index1)
      {
        EquipSlot equipSlot = _equipSlotList[index1];
        if ((bool) equipSlot.IsSlot)
        {
          int id = (int) equipSlot.Id;
          if (id != 999999)
          {
            MasterEquipmentData.EquipmentData equipmentData = masterEquipmentData.Get(id);
            if (equipmentData != null)
            {
              float floatValue = equipmentData.GetFloatValue(_paramType);
              MasterEquipmentEnhanceRate.EquipmentEnhanceRate equipmentEnhanceRate3 = equipmentEnhanceRate1.Get(id);
              float f = 0.0f;
              if (equipmentEnhanceRate3 != null)
                f = equipmentEnhanceRate3.GetFloatValue(_paramType) * (float) (int) equipSlot.EnhancementLevel;
              _f += floatValue + Mathf.Ceil(f);
            }
          }
        }
      }
      int index2 = 0;
      for (int count = _uniqueEquipSlotList.Count; index2 < count; ++index2)
      {
        EquipSlot uniqueEquipSlot = _uniqueEquipSlotList[index2];
        if ((bool) uniqueEquipSlot.IsSlot)
        {
          int id = (int) uniqueEquipSlot.Id;
          if (id != 999999)
          {
            MasterUniqueEquipmentData.UniqueEquipmentData uniqueEquipmentData2 = uniqueEquipmentData1.Get(id);
            if (uniqueEquipmentData2 != null)
            {
              float floatValue = uniqueEquipmentData2.GetFloatValue(_paramType);
              MasterUniqueEquipmentEnhanceRate.UniqueEquipmentEnhanceRate equipmentEnhanceRate3 = equipmentEnhanceRate2.Get(id);
              float f = 0.0f;
              if (equipmentEnhanceRate3 != null)
                f = equipmentEnhanceRate3.GetFloatValue(_paramType) * (float) ((int) uniqueEquipSlot.EnhancementLevel + equipmentEnhanceRate3.EnhanceLvOffset);
              _f += floatValue + Mathf.Ceil(f);
            }
          }
        }
      }
      return UnitUtility.round(_f);
    }

    private static float getInitialParam(eParamType _paramType, int _unitId, int _rarity)
    {
      MasterUnitRarity.UnitRarity unitRarity = ManagerSingleton<MasterDataManager>.Instance.masterUnitRarity.Get(_unitId, _rarity);
      if (unitRarity == null)
        return 0.0f;
      double num = 0.0;
      switch (_paramType)
      {
        case eParamType.HP:
          num = (double) unitRarity.hp;
          break;
        case eParamType.ATK:
          num = (double) unitRarity.atk;
          break;
        case eParamType.DEF:
          num = (double) unitRarity.def;
          break;
        case eParamType.MAGIC_ATK:
          num = (double) unitRarity.magic_str;
          break;
        case eParamType.MAGIC_DEF:
          num = (double) unitRarity.magic_def;
          break;
        case eParamType.PHYSICAL_CRITICAL:
          num = (double) unitRarity.physical_critical;
          break;
        case eParamType.MAGIC_CRITICAL:
          num = (double) unitRarity.magic_critical;
          break;
        case eParamType.DODGE:
          num = (double) unitRarity.dodge;
          break;
        case eParamType.LIFE_STEAL:
          num = (double) unitRarity.life_steal;
          break;
        case eParamType.WAVE_HP_RECOVERY:
          num = (double) unitRarity.wave_hp_recovery;
          break;
        case eParamType.WAVE_ENERGY_RECOVERY:
          num = (double) unitRarity.wave_energy_recovery;
          break;
        case eParamType.PHYSICAL_PENETRATE:
          num = (double) unitRarity.physical_penetrate;
          break;
        case eParamType.MAGIC_PENETRATE:
          num = (double) unitRarity.magic_penetrate;
          break;
        case eParamType.ENERGY_RECOVERY_RATE:
          num = (double) unitRarity.energy_recovery_rate;
          break;
        case eParamType.HP_RECOVERY_RATE:
          num = (double) unitRarity.hp_recovery_rate;
          break;
        case eParamType.ENERGY_REDUCE_RATE:
          num = (double) unitRarity.energy_reduce_rate;
          break;
        case eParamType.ACCURACY:
          num = (double) unitRarity.accuracy;
          break;
      }
      return (float) num;
    }

    private static float getGrowthParam(eParamType _paramType, int _unitId, int _rarity)
    {
      MasterUnitRarity.UnitRarity unitRarity = ManagerSingleton<MasterDataManager>.Instance.masterUnitRarity.Get(_unitId, _rarity);
      if (unitRarity == null)
        return 0.0f;
      double num = 0.0;
      switch (_paramType)
      {
        case eParamType.HP:
          num = (double) unitRarity.hp_growth;
          break;
        case eParamType.ATK:
          num = (double) unitRarity.atk_growth;
          break;
        case eParamType.DEF:
          num = (double) unitRarity.def_growth;
          break;
        case eParamType.MAGIC_ATK:
          num = (double) unitRarity.magic_str_growth;
          break;
        case eParamType.MAGIC_DEF:
          num = (double) unitRarity.magic_def_growth;
          break;
        case eParamType.PHYSICAL_CRITICAL:
          num = (double) unitRarity.physical_critical_growth;
          break;
        case eParamType.MAGIC_CRITICAL:
          num = (double) unitRarity.magic_critical_growth;
          break;
        case eParamType.DODGE:
          num = (double) unitRarity.dodge_growth;
          break;
        case eParamType.LIFE_STEAL:
          num = (double) unitRarity.life_steal_growth;
          break;
        case eParamType.WAVE_HP_RECOVERY:
          num = (double) unitRarity.wave_hp_recovery_growth;
          break;
        case eParamType.WAVE_ENERGY_RECOVERY:
          num = (double) unitRarity.wave_energy_recovery_growth;
          break;
        case eParamType.PHYSICAL_PENETRATE:
          num = (double) unitRarity.physical_penetrate_growth;
          break;
        case eParamType.MAGIC_PENETRATE:
          num = (double) unitRarity.magic_penetrate_growth;
          break;
        case eParamType.ENERGY_RECOVERY_RATE:
          num = (double) unitRarity.energy_recovery_rate_growth;
          break;
        case eParamType.HP_RECOVERY_RATE:
          num = (double) unitRarity.hp_recovery_rate_growth;
          break;
        case eParamType.ENERGY_REDUCE_RATE:
          num = (double) unitRarity.energy_reduce_rate_growth;
          break;
        case eParamType.ACCURACY:
          num = (double) unitRarity.accuracy_growth;
          break;
      }
      return (float) num;
    }

    public static void UpdateAllUnitEquipmentSlot()
    {
      foreach (KeyValuePair<int, UnitParameter> unitParameter in Singleton<UserData>.Instance.UnitParameterDictionary)
      {
        UnitUtility.UpdateUnitEquipmentSlot(unitParameter.Value);
        UnitUtility.UpdateHighRarityEquipmentSlot(unitParameter.Value);
      }
    }

    public static void UpdateUnitEquipmentSlot(int unitId)
    {
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[unitId];
      UnitUtility.UpdateUnitEquipmentSlot(unitParameter);
      UnitUtility.UpdateHighRarityEquipmentSlot(unitParameter);
    }

    public static UnitDefine.UnitEquipmentStatus EquipmentStatusCalc(
      int _unitId,
      EquipSlot _slot)
    {
      UserData instance = Singleton<UserData>.Instance;
      UnitParameter unitParameter = instance.UnitParameterDictionary[_unitId];
      int id = (int) _slot.Id;
      if (id == 999999)
        return UnitDefine.UnitEquipmentStatus.UNKNOWN;
      if ((bool) _slot.IsSlot)
        return UnitDefine.UnitEquipmentStatus.EQUIPPED;
      InterfaceEquipmentData equipmentData = ItemUtil.GetEquipmentData(id);
      if (instance.SearchPossession(eInventoryType.Equip, id) > 0)
      {
        if ((int) unitParameter.UniqueData.UnitLevel >= equipmentData.Require)
          return UnitDefine.UnitEquipmentStatus.CAN_EQUIP;
        return UnitUtility.CanEquipByLevelUp((int) unitParameter.UniqueData.Id, equipmentData.Require) ? UnitDefine.UnitEquipmentStatus.CAN_EQUIP_LV_SHORTAGE : UnitDefine.UnitEquipmentStatus.LV_SHORTAGE;
      }
      UnitDefine.UnitEquipmentStatus unitEquipmentStatus = UnitUtility.getRecipe(id);
      if (unitEquipmentStatus == UnitDefine.UnitEquipmentStatus.CAN_CRAFT)
      {
        if (equipmentData.IsUnique)
          return unitParameter.UniqueData.PromotionLevel < (ePromotionLevel) (int) instance.InitSettingParameter.UniqueEquipSetting.LimitList[0].Promotion ? UnitDefine.UnitEquipmentStatus.RANK_SHORTAGE : unitEquipmentStatus;
        if ((int) unitParameter.UniqueData.UnitLevel >= equipmentData.Require)
          return unitEquipmentStatus;
        if (UnitUtility.CanEquipByLevelUp((int) unitParameter.UniqueData.Id, equipmentData.Require))
          unitEquipmentStatus = UnitDefine.UnitEquipmentStatus.CAN_CRAFT_LV_SHORTAGE;
        else if ((int) unitParameter.UniqueData.UnitLevel < equipmentData.Require)
          unitEquipmentStatus = UnitDefine.UnitEquipmentStatus.LV_SHORTAGE;
      }
      return unitEquipmentStatus;
    }

    private static void CalcHighRarityEquipmentStatus(
      UnitData _unitData,
      MasterUnlockRarity6.UnlockRarity6 _unlockData)
    {
      int unitId = (int) _unlockData.unit_id;
      int slotId = (int) (byte) _unlockData.slot_id;
      int num = UnitUtility.IsUnlockRaritySix(unitId) ? 1 : 0;
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[unitId];
      if (num == 0 || (int) unitParameter.UniqueData.UnitRarity < 5)
      {
        _unitData.UpdateUnlockRarityStatus(slotId, 10);
      }
      else
      {
        MasterUnlockRarity6 masterUnlockRarity6 = ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6;
        int unlockRarityLevel = UnitUtility.GetUnlockRarityLevel(unitParameter.UniqueData.UnlockRarity6Item, slotId);
        if (unlockRarityLevel >= masterUnlockRarity6.GetListWithUnitIdAndSlotIdOrderByUnlockLevelAsc(unitId, (byte) slotId).Count)
          _unitData.UpdateUnlockRarityStatus(slotId, 5);
        else if (unlockRarityLevel == 0)
        {
          if (UnitUtility.isEnoughHighRarityItemNum(unitId, slotId, unlockRarityLevel + 1))
            _unitData.UpdateUnlockRarityStatus(slotId, 2);
          else
            _unitData.UpdateUnlockRarityStatus(slotId, 1);
        }
        else
          _unitData.UpdateUnlockRarityStatus(slotId, 5);
      }
    }

    public static void UpdateUnitEquipmentSlot(UnitParameter _unitInfo)
    {
      int id = (int) _unitInfo.UniqueData.Id;
      for (int index = 0; index < _unitInfo.UniqueData.EquipSlot.Count; ++index)
      {
        EquipSlot _slot = _unitInfo.UniqueData.EquipSlot[index];
        _slot.SetStatus((int) UnitUtility.EquipmentStatusCalc(id, _slot));
      }
      for (int index = 0; index < _unitInfo.UniqueData.UniqueEquipSlot.Count; ++index)
      {
        EquipSlot _slot = _unitInfo.UniqueData.UniqueEquipSlot[index];
        _slot.SetStatus((int) UnitUtility.EquipmentStatusCalc(id, _slot));
      }
    }

    public static void UpdateHighRarityEquipmentSlot(UnitParameter _unitInfo)
    {
      List<MasterUnlockRarity6.UnlockRarity6> orderBySlotIdAsc = ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.GetListWithUnitIdAndUnlockLevelOrderBySlotIdAsc((int) _unitInfo.UniqueData.Id, (byte) 1);
      for (int index = 0; index < orderBySlotIdAsc.Count; ++index)
      {
        MasterUnlockRarity6.UnlockRarity6 _unlockData = orderBySlotIdAsc[index];
        UnitUtility.CalcHighRarityEquipmentStatus(_unitInfo.UniqueData, _unlockData);
      }
    }

    public static Dictionary<int, InventoryData> CalcNeededItems(
      int _unitId,
      EquipSlot _slot)
    {
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId];
      int id = (int) _slot.Id;
      InterfaceEquipmentData equipmentData = ItemUtil.GetEquipmentData(id);
      if ((bool) _slot.IsSlot)
        return new Dictionary<int, InventoryData>();
      Dictionary<int, InventoryData> dictionary = UnitUtility.calcNeededCraftMaterials(id);
      if ((int) unitParameter.UniqueData.UnitLevel < equipmentData.Require)
      {
        foreach (KeyValuePair<int, InventoryData> neededStrengthItem in UnitUtility.CalcNeededStrengthItems(_unitId, equipmentData.Require))
          dictionary.Add(neededStrengthItem.Key, new InventoryData(eInventoryType.Item, neededStrengthItem.Key, neededStrengthItem.Value.Stock));
      }
      return dictionary;
    }

    public static List<UserEquipParameterIdCount> MakeCraftEquipList(
      int _equipmentId) => UnitUtility.EquipDictionaryToList(UnitUtility.calcNeededCraftMaterials(_equipmentId));

    public static List<UserEquipParameterIdCount> MakeCraftEquipList(
      int _equipmentId,
      Dictionary<int, int> _possessionEquipNumberDic,
      out int _goldCost) => UnitUtility.EquipDictionaryToList(UnitUtility.makeNeededCraftMaterials(_equipmentId, _possessionEquipNumberDic, out _goldCost));

    public static List<UserEquipParameterIdCount> CreateEquipmentCraftPostData(
      int _equipmentId) => UnitUtility.EquipDictionaryToList(UnitUtility.calcNeededCraftMaterials(_equipmentId, true));

    public static List<List<UserEquipParameterIdCount>> CreateUniqueEquipmentRankupPostData(
      int _equipmentId,
      int _rank)
    {
      MasterUniqueEquipmentRankup.UniqueEquipmentRankup uniqueEquipmentRankup = ManagerSingleton<MasterDataManager>.Instance.masterUniqueEquipmentRankup.Get(_equipmentId, _rank);
      UserData instance = Singleton<UserData>.Instance;
      Dictionary<int, int> dictionary = Singleton<UserData>.Instance.ListUpPossession(eInventoryType.Equip).Concat<KeyValuePair<int, int>>((IEnumerable<KeyValuePair<int, int>>) instance.ListUpPossession(eInventoryType.Item)).ToDictionary<KeyValuePair<int, int>, int, int>((Func<KeyValuePair<int, int>, int>) (i => i.Key), (Func<KeyValuePair<int, int>, int>) (i => i.Value));
      int num = 0;
      Stack<int> _needIdStack = new Stack<int>();
      int conditionEquipmentCount = uniqueEquipmentRankup.GetConditionEquipmentCount();
      for (int _index = 0; _index < conditionEquipmentCount; ++_index)
      {
        int conditionEquipmentId = uniqueEquipmentRankup.GetConditionEquipmentId(_index);
        int conditionConsumeNum = uniqueEquipmentRankup.GetConditionConsumeNum(_index);
        for (int index = 0; index < conditionConsumeNum; ++index)
          _needIdStack.Push(conditionEquipmentId);
      }
      int _goldCost = num + uniqueEquipmentRankup.GetCraftCost;
      Dictionary<int, InventoryData> _dictionary = UnitUtility.makeNeededCraftMaterials(_needIdStack, dictionary, ref _goldCost);
      return _dictionary == null ? (List<List<UserEquipParameterIdCount>>) null : UnitUtility.EquipDictionaryToMultiList(_dictionary);
    }

    public static List<UserEquipParameterIdCount> ForRankUpEquipList(
      int _unitId)
    {
      int _goldCost = 0;
      Dictionary<int, EquipSlot> _outEquipSlotDic = (Dictionary<int, EquipSlot>) null;
      return UnitUtility.EquipDictionaryToList(UnitUtility.CalcBulkEquipment(_unitId, false, false, out _outEquipSlotDic, out _goldCost));
    }

    public static List<UnitDamageInfo> UpdateUnitDamageInfoRarity(
      List<UnitDamageInfo> _unitDamageInfoList,
      List<UnitDataForView> _unitDataForViewPlayer,
      List<UnitDataForView> _unitDataForViewEnemy)
    {
      List<UnitDamageInfo> unitDamageInfoList = _unitDamageInfoList;
      int viewerId = (int) Singleton<UserData>.Instance.UserInfo.ViewerId;
      int unitId = 0;
      for (int index = 0; index < unitDamageInfoList.Count; ++index)
      {
        unitId = unitDamageInfoList[index].unit_id;
        UnitDataForView unitDataForView = (unitDamageInfoList[index].viewer_id == viewerId ? _unitDataForViewPlayer : _unitDataForViewEnemy).Find((Predicate<UnitDataForView>) (x => (int) x.Id == unitId));
        unitDamageInfoList[index].SetRarity((int) unitDataForView.UnitRarity);
        unitDamageInfoList[index].SetSkinData(unitDataForView.SkinData);
      }
      return unitDamageInfoList;
    }

    public static List<UserEquipParameterIdCount> EquipDictionaryToList(
      Dictionary<int, InventoryData> _dictionary)
    {
      if (_dictionary == null || _dictionary.Count == 0)
        return (List<UserEquipParameterIdCount>) null;
      List<UserEquipParameterIdCount> returnList = new List<UserEquipParameterIdCount>();
      _dictionary.ForEachValue<int, InventoryData>((System.Action<InventoryData>) (_equip => returnList.Add(new UserEquipParameterIdCount(_equip.Id, _equip.Stock))));
      return returnList;
    }

    public static List<List<UserEquipParameterIdCount>> EquipDictionaryToMultiList(
      Dictionary<int, InventoryData> _dictionary)
    {
      if (_dictionary == null || _dictionary.Count == 0)
        return (List<List<UserEquipParameterIdCount>>) null;
      List<UserEquipParameterIdCount> equipList = new List<UserEquipParameterIdCount>();
      List<UserEquipParameterIdCount> itemList = new List<UserEquipParameterIdCount>();
      _dictionary.ForEachValue<int, InventoryData>((System.Action<InventoryData>) (_item =>
      {
        switch (ItemUtil.DecisionItemType(_item.Id))
        {
          case eInventoryType.Item:
            itemList.Add(new UserEquipParameterIdCount(_item.Id, _item.Stock));
            break;
          case eInventoryType.Equip:
            equipList.Add(new UserEquipParameterIdCount(_item.Id, _item.Stock));
            break;
        }
      }));
      return new List<List<UserEquipParameterIdCount>>()
      {
        equipList,
        itemList
      };
    }

    public static void AddNeededStrengthItems(
      int _unitId,
      int _maxRequiredLevel,
      Dictionary<int, InventoryData> _refInventoryDic)
    {
      UserData instance = Singleton<UserData>.Instance;
      if (_maxRequiredLevel <= 0 || (int) instance.UnitParameterDictionary[_unitId].UniqueData.UnitLevel >= _maxRequiredLevel || (int) instance.UserInfo.TeamLevel < _maxRequiredLevel)
        return;
      foreach (KeyValuePair<int, InventoryData> neededStrengthItem in UnitUtility.CalcNeededStrengthItems(_unitId, _maxRequiredLevel))
        _refInventoryDic.Add(neededStrengthItem.Key, neededStrengthItem.Value);
    }

    public static int CalcNonEquipSlotCount(int _unitId, bool _statusCalcFlag)
    {
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId];
      int num = 0;
      if (_statusCalcFlag)
        UnitUtility.UpdateUnitEquipmentSlot(unitParameter);
      List<EquipSlot> equipSlot = unitParameter.UniqueData.EquipSlot;
      for (int index = 0; index < equipSlot.Count; ++index)
      {
        if ((int) equipSlot[index].Status != 5)
          ++num;
      }
      return num;
    }

    public static bool IsBulkEquipment(int _unitId, bool _statusCalcFlag)
    {
      int _goldCost = 0;
      Dictionary<int, EquipSlot> _outEquipSlotDic = (Dictionary<int, EquipSlot>) null;
      return UnitUtility.CalcBulkEquipment(_unitId, _statusCalcFlag, false, out _outEquipSlotDic, out _goldCost).Count > 0;
    }

    public static bool CanRankup(int _unitId, bool _statusCalcFlag)
    {
      int _goldCost = 0;
      Dictionary<int, EquipSlot> _outEquipSlotDic = (Dictionary<int, EquipSlot>) null;
      UnitUtility.CalcBulkEquipment(_unitId, _statusCalcFlag, false, out _outEquipSlotDic, out _goldCost);
      int num = UnitUtility.CalcNonEquipSlotCount(_unitId, false);
      return _outEquipSlotDic.Count == num;
    }

    public static List<UserEquipParameterIdCount> CalcBulkEquipmentIdCount(
      int _unitId,
      bool _statusCalcFlag,
      bool _potionCalcFlag,
      out Dictionary<int, EquipSlot> _outEquipSlotDic)
    {
      int _goldCost = 0;
      return UnitUtility.EquipDictionaryToList(UnitUtility.CalcBulkEquipment(_unitId, _statusCalcFlag, _potionCalcFlag, out _outEquipSlotDic, out _goldCost));
    }

    private static Dictionary<int, List<UserEquipParameterIdCount>> makeEquipSlotNeededItemDictionary(
      ref Dictionary<int, EquipSlot> _equipSlotDic,
      out int _goldCost)
    {
      UserData instance = Singleton<UserData>.Instance;
      Dictionary<int, int> dictionary1 = instance.ListUpPossession(eInventoryType.Equip).Concat<KeyValuePair<int, int>>((IEnumerable<KeyValuePair<int, int>>) instance.ListUpPossession(eInventoryType.Item)).ToDictionary<KeyValuePair<int, int>, int, int>((Func<KeyValuePair<int, int>, int>) (i => i.Key), (Func<KeyValuePair<int, int>, int>) (i => i.Value));
      Dictionary<int, List<UserEquipParameterIdCount>> dictionary2 = new Dictionary<int, List<UserEquipParameterIdCount>>();
      Dictionary<int, EquipSlot> dictionary3 = new Dictionary<int, EquipSlot>();
      _goldCost = 0;
      foreach (KeyValuePair<int, EquipSlot> keyValuePair in _equipSlotDic)
      {
        int _goldCost1 = 0;
        List<UserEquipParameterIdCount> parameterIdCountList = UnitUtility.MakeCraftEquipList((int) keyValuePair.Value.Id, dictionary1, out _goldCost1);
        if (instance.TotalGold - instance.SkillupCost >= _goldCost + _goldCost1)
        {
          dictionary3.Add(keyValuePair.Key, keyValuePair.Value);
          dictionary2.Add(keyValuePair.Key, parameterIdCountList);
          _goldCost += _goldCost1;
        }
      }
      _equipSlotDic.Clear();
      foreach (KeyValuePair<int, EquipSlot> keyValuePair in dictionary3)
        _equipSlotDic.Add(keyValuePair.Key, keyValuePair.Value);
      return dictionary2;
    }

    private static bool IsEquipNeededItemCraftEnable(
      List<UserEquipParameterIdCount> _needItems,
      Dictionary<int, int> _checkHaveItems)
    {
      for (int index = 0; index < _needItems.Count; ++index)
      {
        UserEquipParameterIdCount needItem = _needItems[index];
        if (_checkHaveItems[needItem.id] < needItem.count)
          return false;
      }
      return true;
    }

    public static void ConcatInventoryDataDictionary(
      Dictionary<int, InventoryData> _a,
      Dictionary<int, InventoryData> _b)
    {
      if (_a == null || _b == null)
        return;
      _b.ForEach<int, InventoryData>((System.Action<int, InventoryData>) ((_key, _value) => _a.AddIfNoContains<int, InventoryData>(_key, _value, (System.Action<int, InventoryData>) ((_key2, _value2) => _a[_key2].AddStock(_value2.Stock)))));
    }

    public static UnitUtility.MaterialsRequiredForRankUp CalculationOfMaterialsRequiredForRankUp(
      int _unitId,
      int _rank,
      Dictionary<int, InventoryData> _used)
    {
      UnitUtility.MaterialsRequiredForRankUp requiredForRankUp = new UnitUtility.MaterialsRequiredForRankUp();
      UserData instance = Singleton<UserData>.Instance;
      UnitParameter unitParameter = instance.UnitParameterDictionary[_unitId];
      List<EquipSlot> _equipSlots = unitParameter.UniqueData.EquipSlot;
      if (unitParameter.UniqueData.PromotionLevel != (ePromotionLevel) _rank)
      {
        MasterUnitPromotion.UnitPromotion unitPromotion = ManagerSingleton<MasterDataManager>.Instance.masterUnitPromotion.GetAllUnitPromotion(_unitId)[_rank - 1];
        int count = _equipSlots.Count;
        _equipSlots = new List<EquipSlot>();
        for (int index = 0; index < count; ++index)
        {
          _equipSlots.Add(new EquipSlot());
          _equipSlots[index].SetId(unitPromotion.equip_slot[index]);
          _equipSlots[index].SetIsSlot(false);
        }
      }
      Dictionary<int, int> possessionNumberDic = instance.ListUpPossession(eInventoryType.Equip);
      Dictionary<int, int> dictionary1 = instance.ListUpPossession(eInventoryType.Item);
      possessionNumberDic = possessionNumberDic.Concat<KeyValuePair<int, int>>((IEnumerable<KeyValuePair<int, int>>) dictionary1).ToDictionary<KeyValuePair<int, int>, int, int>((Func<KeyValuePair<int, int>, int>) (i => i.Key), (Func<KeyValuePair<int, int>, int>) (i => i.Value));
      _used.ForEach<int, InventoryData>((System.Action<int, InventoryData>) ((_key, _value) =>
      {
        if (!possessionNumberDic.ContainsKey(_key))
          return;
        possessionNumberDic[_key] -= _used[_key].Stock;
      }));
      for (int index = 0; index < _equipSlots.Count; ++index)
      {
        EquipSlot equipSlot = _equipSlots[index];
        if (!(bool) equipSlot.IsSlot && (int) equipSlot.Id != 0 && (int) equipSlot.Id != 999999)
        {
          int _goldCost = 0;
          Dictionary<int, int> _possessionEquipNumberDic = new Dictionary<int, int>((IDictionary<int, int>) possessionNumberDic);
          Dictionary<int, InventoryData> _b = UnitUtility.makeNeededCraftMaterials((int) equipSlot.Id, _possessionEquipNumberDic, out _goldCost);
          if (_b == null)
          {
            requiredForRankUp.CanNotEquip.Add((int) equipSlot.Id);
          }
          else
          {
            UnitUtility.ConcatInventoryDataDictionary(requiredForRankUp.Materials, _b);
            Dictionary<int, InventoryData>.Enumerator enumerator = _b.GetEnumerator();
            while (enumerator.MoveNext())
            {
              Dictionary<int, int> dictionary2 = possessionNumberDic;
              KeyValuePair<int, InventoryData> current = enumerator.Current;
              int key1 = current.Key;
              Dictionary<int, int> dictionary3 = dictionary2;
              int key2 = key1;
              int num1 = dictionary2[key1];
              current = enumerator.Current;
              int stock = current.Value.Stock;
              int num2 = num1 - stock;
              dictionary3[key2] = num2;
            }
          }
          requiredForRankUp.Gold += _goldCost;
          _possessionEquipNumberDic.Clear();
        }
      }
      requiredForRankUp.Level = UnitUtility.calcMaxRequireLevelNoFilter(_unitId, _equipSlots);
      return requiredForRankUp;
    }

    public static Dictionary<int, InventoryData> CalcBulkEquipment(
      int _unitId,
      bool _statusCalcFlag,
      bool _isAddStrengthItems,
      out Dictionary<int, EquipSlot> _outEquipSlotDic,
      out int _goldCost)
    {
      UserData instance = Singleton<UserData>.Instance;
      UnitParameter unitParameter = instance.UnitParameterDictionary[_unitId];
      _outEquipSlotDic = new Dictionary<int, EquipSlot>();
      Dictionary<int, InventoryData> _refInventoryDic = new Dictionary<int, InventoryData>();
      List<EquipSlot> equipSlot = unitParameter.UniqueData.EquipSlot;
      _goldCost = 0;
      if (_statusCalcFlag)
      {
        UnitUtility.UpdateUnitEquipmentSlot(unitParameter);
        UnitUtility.UpdateHighRarityEquipmentSlot(unitParameter);
      }
      for (int index = 0; index < equipSlot.Count; ++index)
      {
        int key = index + 1;
        switch ((UnitDefine.UnitEquipmentStatus) (int) equipSlot[index].Status)
        {
          case UnitDefine.UnitEquipmentStatus.CAN_EQUIP:
          case UnitDefine.UnitEquipmentStatus.CAN_CRAFT:
          case UnitDefine.UnitEquipmentStatus.CAN_EQUIP_LV_SHORTAGE:
          case UnitDefine.UnitEquipmentStatus.CAN_CRAFT_LV_SHORTAGE:
            _outEquipSlotDic.Add(key, equipSlot[index]);
            break;
        }
      }
      Dictionary<int, List<UserEquipParameterIdCount>> dictionary = UnitUtility.makeEquipSlotNeededItemDictionary(ref _outEquipSlotDic, out _goldCost);
      Dictionary<int, int> _checkHaveItems = new Dictionary<int, int>();
      foreach (KeyValuePair<int, List<UserEquipParameterIdCount>> keyValuePair in dictionary)
      {
        if (keyValuePair.Value != null)
        {
          foreach (UserEquipParameterIdCount parameterIdCount in keyValuePair.Value)
          {
            if (parameterIdCount != null && !_checkHaveItems.ContainsKey(parameterIdCount.id))
              _checkHaveItems.Add(parameterIdCount.id, instance.SearchPossession(eInventoryType.Equip, parameterIdCount.id));
          }
        }
      }
      List<int> intList = new List<int>();
      foreach (KeyValuePair<int, EquipSlot> keyValuePair in _outEquipSlotDic)
      {
        List<UserEquipParameterIdCount> _needItems = dictionary[keyValuePair.Key];
        if (_needItems == null || !UnitUtility.IsEquipNeededItemCraftEnable(_needItems, _checkHaveItems))
        {
          intList.Add(keyValuePair.Key);
        }
        else
        {
          for (int index = 0; index < _needItems.Count; ++index)
          {
            UserEquipParameterIdCount parameterIdCount = _needItems[index];
            if (!_refInventoryDic.ContainsKey(parameterIdCount.id))
              _refInventoryDic.Add(parameterIdCount.id, new InventoryData(eInventoryType.Equip, parameterIdCount.id, parameterIdCount.count));
            else
              _refInventoryDic[parameterIdCount.id].AddStock(parameterIdCount.count);
            _checkHaveItems[parameterIdCount.id] -= parameterIdCount.count;
          }
        }
      }
      for (int index = intList.Count - 1; index >= 0; --index)
        _outEquipSlotDic.Remove(intList[index]);
      if (_isAddStrengthItems)
      {
        int _maxRequiredLevel = UnitUtility.calcMaxRequireLevel(_unitId, _outEquipSlotDic.Values.ToList<EquipSlot>());
        UnitUtility.AddNeededStrengthItems(_unitId, _maxRequiredLevel, _refInventoryDic);
      }
      return _refInventoryDic;
    }

    public static int CalcNeededWholeCost(int _unitId)
    {
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId];
      int num = 0;
      ePromotionLevel promotionLevel = unitParameter.UniqueData.PromotionLevel;
      List<int> equipSlot = ManagerSingleton<MasterDataManager>.Instance.masterUnitPromotion.Get(_unitId, (int) promotionLevel).equip_slot;
      int index = 0;
      for (int count = equipSlot.Count; index < count; ++index)
        num += UnitUtility.CalcNeededCraftCost(equipSlot[index]);
      return num;
    }

    private static UnitDefine.UnitEquipmentStatus getRecipe(int _equipmentId)
    {
      MasterDataManager instance1 = ManagerSingleton<MasterDataManager>.Instance;
      UserData instance2 = Singleton<UserData>.Instance;
      if (_equipmentId == 0)
        return UnitDefine.UnitEquipmentStatus.NO_POSSESION;
      Dictionary<int, int> dictionary = instance2.ListUpPossession(eInventoryType.Equip).Concat<KeyValuePair<int, int>>((IEnumerable<KeyValuePair<int, int>>) instance2.ListUpPossession(eInventoryType.Item)).ToDictionary<KeyValuePair<int, int>, int, int>((Func<KeyValuePair<int, int>, int>) (i => i.Key), (Func<KeyValuePair<int, int>, int>) (i => i.Value));
      Stack<int> intStack = new Stack<int>();
      intStack.Push(_equipmentId);
      bool flag = true;
      while (intStack.Count > 0)
      {
        int num = intStack.Pop();
        if (dictionary.ContainsKey(num) && dictionary[num] >= 1)
        {
          dictionary[num]--;
        }
        else
        {
          InterfaceEquipmentData equipmentData = ItemUtil.GetEquipmentData(num);
          if (equipmentData == null || !equipmentData.IsEnableCraft)
            return flag ? UnitDefine.UnitEquipmentStatus.NO_POSSESION_CANNOT_CRAFT : UnitDefine.UnitEquipmentStatus.NO_POSSESION;
          InterfaceEquipmentCraft equipmentCraft = ItemUtil.GetEquipmentCraft(num);
          int conditionEquipmentCount = equipmentCraft.GetConditionEquipmentCount();
          flag = false;
          for (int _index = 0; _index < conditionEquipmentCount; ++_index)
          {
            int conditionEquipmentId = equipmentCraft.GetConditionEquipmentId(_index);
            int conditionConsumeNum = equipmentCraft.GetConditionConsumeNum(_index);
            for (int index = 0; index < conditionConsumeNum; ++index)
              intStack.Push(conditionEquipmentId);
          }
        }
      }
      return UnitDefine.UnitEquipmentStatus.CAN_CRAFT;
    }

    private static Dictionary<int, InventoryData> makeNeededCraftMaterials(
      int _itemId,
      Dictionary<int, int> _possessionEquipNumberDic,
      out int _goldCost,
      bool _passRootEquipment = false)
    {
      Stack<int> _needIdStack = new Stack<int>();
      _needIdStack.Push(_itemId);
      _goldCost = 0;
      return UnitUtility.makeNeededCraftMaterials(_needIdStack, _possessionEquipNumberDic, ref _goldCost, _passRootEquipment);
    }

    private static void pushEquipmentCraft(
      InterfaceEquipmentCraft _equipmentCraft,
      Stack<int> _needIdStack,
      ref int _goldCost)
    {
      int conditionEquipmentCount = _equipmentCraft.GetConditionEquipmentCount();
      for (int _index = 0; _index < conditionEquipmentCount; ++_index)
      {
        int conditionEquipmentId = _equipmentCraft.GetConditionEquipmentId(_index);
        int conditionConsumeNum = _equipmentCraft.GetConditionConsumeNum(_index);
        for (int index = 0; index < conditionConsumeNum; ++index)
          _needIdStack.Push(conditionEquipmentId);
      }
      _goldCost += _equipmentCraft.GetCraftCost;
    }

    private static Dictionary<int, InventoryData> makeNeededCraftMaterials(
      Stack<int> _needIdStack,
      Dictionary<int, int> _possessionEquipNumberDic,
      ref int _goldCost,
      bool _passRootEquipment = false)
    {
      Dictionary<int, InventoryData> dictionary = new Dictionary<int, InventoryData>();
      while (_needIdStack.Count > 0)
      {
        int num = _needIdStack.Pop();
        if (!_passRootEquipment && _possessionEquipNumberDic.ContainsKey(num) && _possessionEquipNumberDic[num] >= 1)
        {
          _possessionEquipNumberDic[num]--;
          if (dictionary.ContainsKey(num))
            dictionary[num].AddStock(1);
          else
            dictionary.Add(num, new InventoryData(ItemUtil.DecisionItemType(num), num, 1));
        }
        else
        {
          MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
          InterfaceEquipmentData equipmentData = ItemUtil.GetEquipmentData(num);
          if (equipmentData == null || !equipmentData.IsEnableCraft)
          {
            _goldCost = 0;
            return (Dictionary<int, InventoryData>) null;
          }
          UnitUtility.pushEquipmentCraft(ItemUtil.GetEquipmentCraft(num), _needIdStack, ref _goldCost);
        }
        _passRootEquipment = false;
      }
      return dictionary;
    }

    private static Dictionary<int, InventoryData> calcNeededCraftMaterials(
      int _equipmentId,
      bool _passRootEquipment = false)
    {
      if (_equipmentId == 0)
        return new Dictionary<int, InventoryData>();
      UserData instance = Singleton<UserData>.Instance;
      Dictionary<int, int> dictionary = Singleton<UserData>.Instance.ListUpPossession(eInventoryType.Equip).Concat<KeyValuePair<int, int>>((IEnumerable<KeyValuePair<int, int>>) instance.ListUpPossession(eInventoryType.Item)).ToDictionary<KeyValuePair<int, int>, int, int>((Func<KeyValuePair<int, int>, int>) (i => i.Key), (Func<KeyValuePair<int, int>, int>) (i => i.Value));
      int _goldCost = 0;
      return UnitUtility.makeNeededCraftMaterials(_equipmentId, dictionary, out _goldCost, _passRootEquipment);
    }

    public static int CalcNeededCraftCost(int _equipmentId)
    {
      bool calcError = false;
      return UnitUtility.CalcNeededCraftCost(_equipmentId, out calcError);
    }

    public static Dictionary<int, Dictionary<int, List<int>>> GetUnitObtainEquipmentQuestList(
      int _unitId,
      bool _isIgnoreEnoughItem = true)
    {
      Dictionary<int, Dictionary<int, List<int>>> dictionary = new Dictionary<int, Dictionary<int, List<int>>>();
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId];
      for (int index = 0; index < unitParameter.UniqueData.EquipSlot.Count; ++index)
      {
        EquipSlot _slot = unitParameter.UniqueData.EquipSlot[index];
        int id = (int) _slot.Id;
        switch (UnitUtility.EquipmentStatusCalc(_unitId, _slot))
        {
          case UnitDefine.UnitEquipmentStatus.NO_POSSESION:
          case UnitDefine.UnitEquipmentStatus.NO_POSSESION_CANNOT_CRAFT:
            if (!dictionary.ContainsKey(id))
            {
              Dictionary<int, List<int>> equipmentObtainQuestList = UnitUtility.getEquipmentObtainQuestList(id, _isIgnoreEnoughItem);
              bool flag = false;
              foreach (KeyValuePair<int, List<int>> keyValuePair in equipmentObtainQuestList)
              {
                if (keyValuePair.Value.Count == 0)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                dictionary.Add(id, equipmentObtainQuestList);
                break;
              }
              break;
            }
            break;
        }
      }
      return dictionary;
    }

    public static Dictionary<int, Dictionary<int, List<int>>> GetUnitObtainUniqueEquipmentQuestList(
      int _unitId)
    {
      Dictionary<int, Dictionary<int, List<int>>> dictionary = new Dictionary<int, Dictionary<int, List<int>>>();
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId];
      for (int index = 0; index < unitParameter.UniqueData.UniqueEquipSlot.Count; ++index)
      {
        EquipSlot _slot = unitParameter.UniqueData.UniqueEquipSlot[index];
        int id = (int) _slot.Id;
        switch (UnitUtility.EquipmentStatusCalc(_unitId, _slot))
        {
          case UnitDefine.UnitEquipmentStatus.NO_POSSESION:
          case UnitDefine.UnitEquipmentStatus.NO_POSSESION_CANNOT_CRAFT:
            if (!dictionary.ContainsKey(id))
            {
              Dictionary<int, List<int>> equipmentObtainQuestList = UnitUtility.getEquipmentObtainQuestList(id);
              bool flag = false;
              foreach (KeyValuePair<int, List<int>> keyValuePair in equipmentObtainQuestList)
              {
                if (keyValuePair.Value.Count == 0)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                dictionary.Add(id, equipmentObtainQuestList);
                break;
              }
              break;
            }
            break;
        }
      }
      return dictionary;
    }

    private static Dictionary<int, List<int>> getEquipmentObtainQuestList(
      int _getId,
      bool _isIgnoreEnoughItem = true)
    {
      UserData userData = Singleton<UserData>.Instance;
      MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
      InterfaceEquipmentData equipmentData = ItemUtil.GetEquipmentData(_getId);
      Dictionary<int, List<int>> questDic = new Dictionary<int, List<int>>();
      System.Action<int> action = (System.Action<int>) (itemId =>
      {
        if (questDic.ContainsKey(_getId))
          return;
        Dictionary<int, UserQuestInfo> parameterDictionary = userData.QuestMapParameterDictionary;
        List<int> intList = new List<int>();
        if (parameterDictionary != null)
        {
          List<int> rewardItemList = QuestUtility.CreateRewardItemList(itemId);
          int teamLevel = (int) userData.UserInfo.TeamLevel;
          for (int index = 0; index < rewardItemList.Count; ++index)
          {
            int num = rewardItemList[index];
            if (parameterDictionary.ContainsKey(num) && teamLevel >= QuestUtility.GetNextAreaReleaseCondition(QuestUtility.GetAreaId(num)))
              intList.Add(num);
          }
        }
        questDic.Add(_getId, intList);
      });
      if (equipmentData == null)
        return questDic;
      InterfaceEquipmentCraft equipmentCraft = ItemUtil.GetEquipmentCraft(equipmentData.EquipId);
      if (equipmentCraft != null)
      {
        int conditionEquipmentCount = equipmentCraft.GetConditionEquipmentCount();
        for (int _index = 0; _index < conditionEquipmentCount; ++_index)
        {
          int conditionEquipmentId = equipmentCraft.GetConditionEquipmentId(_index);
          int conditionConsumeNum = equipmentCraft.GetConditionConsumeNum(_index);
          eInventoryType _type = ItemUtil.GetEquipmentData(conditionEquipmentId) != null ? eInventoryType.Equip : eInventoryType.Item;
          int num = userData.SearchPossession(_type, conditionEquipmentId);
          if (!_isIgnoreEnoughItem || conditionConsumeNum > num)
          {
            if (_type == eInventoryType.Equip)
            {
              foreach (KeyValuePair<int, List<int>> equipmentObtainQuest in UnitUtility.getEquipmentObtainQuestList(conditionEquipmentId, _isIgnoreEnoughItem))
              {
                if (!questDic.ContainsKey(equipmentObtainQuest.Key))
                  questDic.Add(equipmentObtainQuest.Key, equipmentObtainQuest.Value);
              }
            }
            else
              action(conditionEquipmentId);
          }
        }
      }
      else
        action(_getId);
      return questDic;
    }

    public static void GetCraftRequireNum(ref Dictionary<int, int> _requireDic, int _materialId)
    {
      UserData instance1 = Singleton<UserData>.Instance;
      MasterDataManager instance2 = ManagerSingleton<MasterDataManager>.Instance;
      MasterEquipmentData masterEquipmentData = instance2.masterEquipmentData;
      MasterEquipmentCraft masterEquipmentCraft = instance2.masterEquipmentCraft;
      MasterEquipmentData.EquipmentData equipmentData = masterEquipmentData.Get(_materialId);
      int id = (int) equipmentData.Id;
      MasterEquipmentCraft.EquipmentCraft equipmentCraft = masterEquipmentCraft.Get(id);
      if (equipmentCraft != null)
      {
        int conditionEquipmentCount = equipmentCraft.GetConditionEquipmentCount();
        for (int _index = 0; _index < conditionEquipmentCount; ++_index)
        {
          int conditionEquipmentId = equipmentCraft.GetConditionEquipmentId(_index);
          int conditionConsumeNum = equipmentCraft.GetConditionConsumeNum(_index);
          if (_requireDic.ContainsKey(conditionEquipmentId))
            _requireDic[conditionEquipmentId] += conditionConsumeNum;
          else
            _requireDic.Add(conditionEquipmentId, conditionConsumeNum);
          if ((instance2.masterEquipmentData.Get(conditionEquipmentId) != null ? 4 : 2) == 4)
            UnitUtility.GetCraftRequireNum(ref _requireDic, conditionEquipmentId);
        }
      }
      else
      {
        if ((int) equipmentData.CraftFlag != 0 || _requireDic.ContainsKey(_materialId))
          return;
        _requireDic.Add(_materialId, 1);
      }
    }

    public static bool IsObtainEquipment(int _unitId)
    {
      Dictionary<int, Dictionary<int, List<int>>> equipmentQuestList = UnitUtility.GetUnitObtainEquipmentQuestList(_unitId);
      int num = 0;
      foreach (KeyValuePair<int, Dictionary<int, List<int>>> keyValuePair1 in equipmentQuestList)
      {
        foreach (KeyValuePair<int, List<int>> keyValuePair2 in keyValuePair1.Value)
          num += keyValuePair2.Value.Count;
      }
      return num > 0;
    }

    public static bool IsObtainEquipment(int _unitId, int _equipId)
    {
      Dictionary<int, Dictionary<int, List<int>>> equipmentQuestList = UnitUtility.GetUnitObtainEquipmentQuestList(_unitId);
      Dictionary<int, List<int>> dictionary = (Dictionary<int, List<int>>) null;
      int key = _equipId;
      ref Dictionary<int, List<int>> local = ref dictionary;
      if (!equipmentQuestList.TryGetValue(key, out local))
        return false;
      foreach (List<int> intList in dictionary.Values)
      {
        if (intList.Count == 0)
          return false;
      }
      return true;
    }

    public static bool IsObtainUniqueEquipment(int _unitId, int _equipId)
    {
      Dictionary<int, Dictionary<int, List<int>>> equipmentQuestList = UnitUtility.GetUnitObtainUniqueEquipmentQuestList(_unitId);
      Dictionary<int, List<int>> dictionary = (Dictionary<int, List<int>>) null;
      int key = _equipId;
      ref Dictionary<int, List<int>> local = ref dictionary;
      if (!equipmentQuestList.TryGetValue(key, out local))
        return false;
      foreach (List<int> intList in dictionary.Values)
      {
        if (intList.Count == 0)
          return false;
      }
      return true;
    }

    public static int CalcNeededCraftCost(int _equipmentId, out bool calcError)
    {
      calcError = false;
      if (_equipmentId == 0 || _equipmentId == 999999)
      {
        calcError = true;
        return 0;
      }
      int num1 = 0;
      UserData instance = Singleton<UserData>.Instance;
      Dictionary<int, int> dictionary = instance.ListUpPossession(eInventoryType.Equip).Concat<KeyValuePair<int, int>>((IEnumerable<KeyValuePair<int, int>>) instance.ListUpPossession(eInventoryType.Item)).ToDictionary<KeyValuePair<int, int>, int, int>((Func<KeyValuePair<int, int>, int>) (i => i.Key), (Func<KeyValuePair<int, int>, int>) (i => i.Value));
      Stack<int> intStack = new Stack<int>();
      intStack.Push(_equipmentId);
      while (intStack.Count > 0)
      {
        int num2 = intStack.Pop();
        if (dictionary.ContainsKey(num2) && dictionary[num2] >= 1)
        {
          dictionary[num2]--;
        }
        else
        {
          InterfaceEquipmentCraft equipmentCraft = ItemUtil.GetEquipmentCraft(num2);
          if (equipmentCraft == null)
          {
            calcError = true;
            return 0;
          }
          int conditionEquipmentCount = equipmentCraft.GetConditionEquipmentCount();
          if (conditionEquipmentCount <= 0)
          {
            calcError = true;
            return 0;
          }
          for (int _index = 0; _index < conditionEquipmentCount; ++_index)
          {
            int conditionEquipmentId = equipmentCraft.GetConditionEquipmentId(_index);
            int conditionConsumeNum = equipmentCraft.GetConditionConsumeNum(_index);
            for (int index = 0; index < conditionConsumeNum; ++index)
              intStack.Push(conditionEquipmentId);
          }
          num1 += equipmentCraft.GetCraftCost;
        }
      }
      return num1;
    }

    public static Dictionary<int, InventoryData> CalcNeededStrengthItems(
      int _unitId,
      int _requiredLevel,
      Dictionary<int, InventoryData> _configured = null)
    {
      MasterItemData itemData = ManagerSingleton<MasterDataManager>.Instance.masterItemData;
      List<UserItemParameter> userItemParameterList = Singleton<UserData>.Instance.ListUpExpItems();
      Dictionary<int, InventoryData> dictionary = new Dictionary<int, InventoryData>();
      if (userItemParameterList.Count == 0)
        return new Dictionary<int, InventoryData>();
      userItemParameterList.Sort((Comparison<UserItemParameter>) ((a, b) => (int) itemData.Get((int) a.ItemId).Value - (int) itemData.Get((int) b.ItemId).Value));
      int index1 = userItemParameterList.Count - 1;
      int requiredExp = UnitUtility.getRequiredExp(_unitId, _requiredLevel);
      if (_configured != null)
      {
        foreach (KeyValuePair<int, InventoryData> keyValuePair in _configured)
        {
          dictionary.Add(keyValuePair.Value.Id, new InventoryData(eInventoryType.Item, keyValuePair.Value.Id, keyValuePair.Value.Stock));
          requiredExp -= (int) itemData.Get(keyValuePair.Value.Id).Value * keyValuePair.Value.Stock;
        }
      }
      for (; index1 >= 0; --index1)
      {
        int num1 = 0;
        UserItemParameter userItemParameter = userItemParameterList[index1];
        int[] numArray = new int[userItemParameterList.Count];
        for (int index2 = 0; index2 < userItemParameterList.Count; ++index2)
        {
          numArray[index2] = 0;
          int itemId = (int) userItemParameterList[index2].ItemId;
          int num2 = (int) itemData.Get((int) userItemParameterList[index2].ItemId).Value;
          int itemCount = (int) userItemParameterList[index2].ItemCount;
          if (dictionary.ContainsKey(itemId))
            itemCount -= dictionary[itemId].Stock;
          for (; numArray[index2] < itemCount && requiredExp >= num1; num1 += num2)
            ++numArray[index2];
        }
        if (numArray[index1] > 0)
        {
          if (dictionary.ContainsKey((int) userItemParameter.ItemId))
            dictionary[(int) userItemParameter.ItemId].AddStock(numArray[index1]);
          else
            dictionary.Add((int) userItemParameter.ItemId, new InventoryData(eInventoryType.Item, (int) userItemParameter.ItemId, numArray[index1]));
          requiredExp -= (int) itemData.Get((int) userItemParameter.ItemId).Value * numArray[index1];
        }
      }
      return dictionary;
    }

    private static int calcStrengthItemExp()
    {
      List<UserItemParameter> userItemParameterList = Singleton<UserData>.Instance.ListUpExpItems();
      MasterItemData masterItemData = ManagerSingleton<MasterDataManager>.Instance.masterItemData;
      int num = 0;
      for (int index = 0; index < userItemParameterList.Count; ++index)
        num += (int) masterItemData.Get((int) userItemParameterList[index].ItemId).Value * (int) userItemParameterList[index].ItemCount;
      return num;
    }

    public static bool CanEquipByLevelUp(
      int _unitId,
      int _requiredLevel,
      Dictionary<int, UserItemParameter> _usedItem = null) => (int) Singleton<UserData>.Instance.UserInfo.TeamLevel >= _requiredLevel && UnitUtility.calcStrengthItemExp() > UnitUtility.getRequiredExp(_unitId, _requiredLevel);

    private static List<SkillLevelInfo> createSkillLevelInfoList(int _unitId)
    {
      UnitData uniqueData = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId].UniqueData;
      List<SkillLevelInfo> skillLevelInfoList1 = new List<SkillLevelInfo>();
      List<SkillLevelInfo>[] skillLevelInfoListArray = new List<SkillLevelInfo>[3]
      {
        uniqueData.UnionBurst,
        uniqueData.MainSkill,
        uniqueData.ExSkill
      };
      int[] numArray = new int[3]{ 101, 201, 301 };
      for (int index1 = 0; index1 < skillLevelInfoListArray.Length; ++index1)
      {
        List<SkillLevelInfo> skillLevelInfoList2 = skillLevelInfoListArray[index1];
        for (int index2 = 0; index2 < skillLevelInfoList2.Count; ++index2)
        {
          SkillLevelInfo skillLevelInfo1 = skillLevelInfoList2[index2];
          SkillLevelInfo skillLevelInfo2 = new SkillLevelInfo();
          skillLevelInfo2.SetSkillId((int) skillLevelInfo1.SkillId);
          skillLevelInfo2.SetSkillLevel((int) skillLevelInfo1.SkillLevel);
          skillLevelInfo2.SetSlotNumber(numArray[index1] + index2);
          skillLevelInfoList1.Add(skillLevelInfo2);
        }
      }
      return skillLevelInfoList1;
    }

    public static List<SkillLevelUpDetail> CalcMaxSkillLevelup(
      int _unitId,
      int _maxLevel,
      List<SkillLevelUpDetail> _configured,
      ref int _usedCost)
    {
      if (!ManagerSingleton<MasterDataManager>.Instance.masterContentReleaseData.CheckReleaseContents(eSystemId.UNIT_SKILL_LVUP))
        return _configured;
      MasterSkillCost masterSkillCost = ManagerSingleton<MasterDataManager>.Instance.masterSkillCost;
      MasterUnlockSkillData masterUnlockSkillData = ManagerSingleton<MasterDataManager>.Instance.masterUnlockSkillData;
      UnitData uniqueData = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId].UniqueData;
      List<SkillLevelInfo> skillLevelInfoList = UnitUtility.createSkillLevelInfoList(_unitId);
      for (int index = skillLevelInfoList.Count - 1; index >= 0; --index)
      {
        SkillLevelInfo skillInfo = skillLevelInfoList[index];
        MasterUnlockSkillData.UnlockSkillData unlockSkillData = (MasterUnlockSkillData.UnlockSkillData) null;
        if (masterUnlockSkillData.dictionary.TryGetValue((int) skillInfo.SlotNumber, out unlockSkillData))
        {
          if (uniqueData.PromotionLevel < (ePromotionLevel) (int) unlockSkillData.promotion_level)
            skillLevelInfoList.RemoveAt(index);
          else if (_configured.Find((Predicate<SkillLevelUpDetail>) (val => val.location == (int) skillInfo.SlotNumber)) == null)
            _configured.Add(new SkillLevelUpDetail((int) skillInfo.SlotNumber, 0, (int) skillInfo.SkillLevel));
        }
      }
      _configured.Reverse();
      while (true)
      {
        int num1 = Singleton<UserData>.Instance.TotalGold - _usedCost;
        int index1 = 0;
        int num2 = int.MaxValue;
        for (int index2 = 0; index2 < _configured.Count; ++index2)
        {
          int num3 = _configured[index2].current_level + _configured[index2].step;
          if (num3 < num2)
          {
            index1 = index2;
            num2 = num3;
          }
        }
        MasterSkillCost.SkillCost skillCost = masterSkillCost.Get(num2 + 1);
        if (_maxLevel > num2 && num1 >= (int) skillCost.cost)
        {
          _usedCost += (int) skillCost.cost;
          _configured[index1].SetStep(_configured[index1].step + 1);
        }
        else
          break;
      }
      _configured.RemoveAll((Predicate<SkillLevelUpDetail>) (match => match.step == 0));
      return _configured;
    }

    public static bool IsRarityUpEnable(
      int _unitId,
      out bool _isRarityMax,
      out bool _materialWarningFlag,
      out bool _goldWarningFlag)
    {
      UserData instance1 = Singleton<UserData>.Instance;
      MasterDataManager instance2 = ManagerSingleton<MasterDataManager>.Instance;
      UnitParameter unitParameter = instance1.UnitParameterDictionary[_unitId];
      List<MasterUnitRarity.UnitRarity> source = instance2.masterUnitRarity[_unitId];
      int unitRarity1 = (int) unitParameter.UniqueData.UnitRarity;
      int num1 = source.Select<MasterUnitRarity.UnitRarity, int>((Func<MasterUnitRarity.UnitRarity, int>) (data => (int) data.rarity)).Max();
      MasterUnitRarity.UnitRarity unitRarity2 = source[unitRarity1];
      int totalGold = instance1.TotalGold;
      int consumeGold = (int) unitRarity2.consume_gold;
      int unitMaterialId = (int) unitRarity2.unit_material_id;
      int num2 = instance1.SearchPossession(eInventoryType.Item, unitMaterialId);
      int consumeNum = (int) unitRarity2.consume_num;
      _isRarityMax = num1 == (int) unitParameter.UniqueData.UnitRarity;
      _materialWarningFlag = num2 < consumeNum;
      _goldWarningFlag = totalGold < consumeGold;
      return !_isRarityMax && !_goldWarningFlag && !_materialWarningFlag;
    }

    public static bool IsRarityUpEnable(int _unitId) => UnitUtility.IsRarityUpEnable(_unitId, out bool _, out bool _, out bool _);

    public static int CalcUnitLevel(int _unitId, int _addExp, bool _checkLimit = true)
    {
      UserData instance = Singleton<UserData>.Instance;
      UnitParameter unitParameter = instance.UnitParameterDictionary[_unitId];
      MasterExperienceUnit masterExperienceUnit = ManagerSingleton<MasterDataManager>.Instance.masterExperienceUnit;
      UnitData uniqueData = unitParameter.UniqueData;
      _addExp += (int) uniqueData.UnitExp - masterExperienceUnit[(int) uniqueData.UnitLevel].total_exp;
      int unitLevel;
      for (unitLevel = (int) unitParameter.UniqueData.UnitLevel; _addExp > 0 && masterExperienceUnit.dictionary.ContainsKey(unitLevel); ++unitLevel)
      {
        int nextExp = masterExperienceUnit[unitLevel].next_exp;
        if (_addExp - nextExp >= 0)
          _addExp -= nextExp;
        else
          break;
      }
      return !_checkLimit ? unitLevel : Math.Min(unitLevel, (int) instance.UserInfo.TeamLevel);
    }

    private static int getRequiredExp(int _unitId, int _requiredLevel)
    {
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId];
      return ManagerSingleton<MasterDataManager>.Instance.masterExperienceUnit[_requiredLevel].total_exp - (int) unitParameter.UniqueData.UnitExp;
    }

    public static int CalcRemainingUnitExp(int _level, int _exp)
    {
      int key = _level + 1;
      MasterExperienceUnit masterExperienceUnit = ManagerSingleton<MasterDataManager>.Instance.masterExperienceUnit;
      MasterExperienceUnit.ExperienceUnit experienceUnit = (MasterExperienceUnit.ExperienceUnit) null;
      return masterExperienceUnit.dictionary.TryGetValue(key, out experienceUnit) ? (int) experienceUnit.total_exp - _exp : 0;
    }

    public static bool IsLoveRankEnable(int _unitId, int _loveRank)
    {
      MasterLoveChara masterLoveChara = ManagerSingleton<MasterDataManager>.Instance.masterLoveChara;
      MasterLoveChara.LoveChara loveChara = (MasterLoveChara.LoveChara) null;
      if (masterLoveChara.dictionary.TryGetValue(_loveRank, out loveChara))
      {
        UnitParameter unitParameter = (UnitParameter) null;
        if (Singleton<UserData>.Instance.UnitParameterDictionary.TryGetValue(UnitUtility.GetUnitId(UnitUtility.GetCharaId(_unitId), (int) loveChara.unlocked_class), out unitParameter) && (int) unitParameter.UniqueData.UnitRarity >= (int) loveChara.rarity)
          return true;
      }
      return false;
    }

    public static bool IsLoveRankMax(int _unitId, int _exp)
    {
      foreach (MasterLoveChara.LoveChara loveChara in ManagerSingleton<MasterDataManager>.Instance.masterLoveChara.dictionary.Values)
      {
        if (_exp < (int) loveChara.total_love)
          return !UnitUtility.IsLoveRankEnable(_unitId, (int) loveChara.love_level);
      }
      return true;
    }

    public static int CalcRemainingLoveRankExp(int _unitId, int _level, int _exp)
    {
      int num = _level + 1;
      if (!UnitUtility.IsLoveRankEnable(_unitId, num))
        return 0;
      MasterLoveChara masterLoveChara = ManagerSingleton<MasterDataManager>.Instance.masterLoveChara;
      MasterLoveChara.LoveChara loveChara = (MasterLoveChara.LoveChara) null;
      return masterLoveChara.dictionary.TryGetValue(num, out loveChara) ? (int) loveChara.total_love - _exp : 0;
    }

    public static int GetUpperLimitLoveRank(int _unitId, int _rarity)
    {
      MasterLoveChara masterLoveChara = ManagerSingleton<MasterDataManager>.Instance.masterLoveChara;
      int num = 0;
      int maxClassId = (int) UnitUtility.GetMaxClassId(_unitId);
      foreach (KeyValuePair<int, MasterLoveChara.LoveChara> keyValuePair in masterLoveChara.dictionary)
      {
        MasterLoveChara.LoveChara loveChara = keyValuePair.Value;
        if (maxClassId >= (int) loveChara.unlocked_class && _rarity >= (int) loveChara.rarity)
          num = (int) loveChara.love_level;
      }
      return num;
    }

    public static float CalcExpRatio(HLHNEABHDIC _tempParam)
    {
      MasterExperienceUnit masterExperienceUnit = ManagerSingleton<MasterDataManager>.Instance.masterExperienceUnit;
      int num = _tempParam.IJDDHLNLPKB > 1 ? masterExperienceUnit[_tempParam.IJDDHLNLPKB].total_exp : 0;
      return (float) (_tempParam.IGJBMPDBOEI - num) / (float) masterExperienceUnit[_tempParam.IJDDHLNLPKB].next_exp;
    }

    public static float CalcLoveExpRatio(HLHNEABHDIC _tempParam)
    {
      MasterLoveChara masterLoveChara = ManagerSingleton<MasterDataManager>.Instance.masterLoveChara;
      int num = _tempParam.BHLBGBPCJNJ > 1 ? masterLoveChara[_tempParam.BHLBGBPCJNJ].total_exp : 0;
      return (float) (_tempParam.DEPEFDBDPMH - num) / (float) masterLoveChara[_tempParam.BHLBGBPCJNJ].next_exp;
    }

    public static List<ItemInfo> CalcPortionInfo(int _unitId, int _slotId = -1)
    {
      UserData instance = Singleton<UserData>.Instance;
      int _requiredLevel = _slotId >= 0 ? (int) ManagerSingleton<MasterDataManager>.Instance.masterEquipmentData.Get((int) instance.UnitParameterDictionary[_unitId].UniqueData.EquipSlot[_slotId].Id).RequireLevel : UnitUtility.calcMaxRequireLevel(_unitId);
      List<ItemInfo> itemInfoList = new List<ItemInfo>();
      if ((int) instance.UnitParameterDictionary[_unitId].UniqueData.UnitLevel < _requiredLevel && (int) instance.UserInfo.TeamLevel >= _requiredLevel)
      {
        foreach (KeyValuePair<int, InventoryData> neededStrengthItem in UnitUtility.CalcNeededStrengthItems(_unitId, _requiredLevel))
        {
          ItemInfo itemInfo = new ItemInfo();
          itemInfo.SetItemId(neededStrengthItem.Key);
          itemInfo.SetItemNum(neededStrengthItem.Value.Stock);
          itemInfoList.Add(itemInfo);
        }
      }
      return itemInfoList;
    }

    public static List<ItemInfo> CalcMaxPortionInfo(int _unitId)
    {
      List<ItemInfo> itemInfoList = new List<ItemInfo>();
      UserData instance = Singleton<UserData>.Instance;
      UnitData uniqueData = instance.UnitParameterDictionary[_unitId].UniqueData;
      int teamLevel = (int) instance.UserInfo.TeamLevel;
      foreach (KeyValuePair<int, InventoryData> neededStrengthItem in UnitUtility.CalcNeededStrengthItems(_unitId, teamLevel))
      {
        ItemInfo itemInfo = new ItemInfo();
        itemInfo.SetItemId(neededStrengthItem.Key);
        itemInfo.SetItemNum(neededStrengthItem.Value.Stock);
        itemInfoList.Add(itemInfo);
      }
      return itemInfoList;
    }

    private static int calcMaxRequireLevel(int _unitId, List<EquipSlot> _equipSlots = null)
    {
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId];
      int num = 0;
      List<EquipSlot> equipSlotList = _equipSlots != null ? _equipSlots : unitParameter.UniqueData.EquipSlot;
      int index = 0;
      for (int count = equipSlotList.Count; index < count; ++index)
      {
        if (equipSlotList[index] != null)
        {
          int id = (int) equipSlotList[index].Id;
          if (id != 999999)
          {
            MasterEquipmentData.EquipmentData equipmentData = ManagerSingleton<MasterDataManager>.Instance.masterEquipmentData.Get(id);
            if (num < (int) equipmentData.RequireLevel && (int) equipmentData.RequireLevel <= (int) Singleton<UserData>.Instance.UserInfo.TeamLevel)
              num = (int) equipmentData.RequireLevel;
          }
        }
      }
      return num;
    }

    private static int calcMaxRequireLevelNoFilter(int _unitId, List<EquipSlot> _equipSlots = null)
    {
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId];
      int val1 = 0;
      List<EquipSlot> equipSlotList = _equipSlots != null ? _equipSlots : unitParameter.UniqueData.EquipSlot;
      int index = 0;
      for (int count = equipSlotList.Count; index < count; ++index)
      {
        if (equipSlotList[index] != null)
        {
          int id = (int) equipSlotList[index].Id;
          if (id != 999999)
          {
            MasterEquipmentData.EquipmentData equipmentData = ManagerSingleton<MasterDataManager>.Instance.masterEquipmentData.Get(id);
            val1 = Math.Max(val1, (int) equipmentData.RequireLevel);
          }
        }
      }
      return val1;
    }

    public static int GetUnitLimitLevel(int _teamLevel) => _teamLevel;

    public static bool CanReleaseUnit(int _unitId)
    {
      if (Singleton<UserData>.Instance.UnitParameterDictionary.ContainsKey(_unitId) || UnitUtility.IsLimitedUnit(_unitId))
        return false;
      MasterUnlockUnitCondition.UnlockUnitCondition unlockUnitCondition = ManagerSingleton<MasterDataManager>.Instance.masterUnlockUnitCondition[_unitId];
      bool flag = true;
      if (unlockUnitCondition.ConsumeMaterialNum > 0)
      {
        int consumeMaterialNum = unlockUnitCondition.ConsumeMaterialNum;
        if (Singleton<UserData>.Instance.SearchPossession(eInventoryType.Item, unlockUnitCondition.MaterialId) < consumeMaterialNum)
          flag = false;
      }
      for (int index = 0; index < unlockUnitCondition.Item.Count; ++index)
      {
        MasterUnlockUnitCondition.UnlockUnitCondition.Pair pair = unlockUnitCondition.Item[index];
        int num = pair.Num;
        if (Singleton<UserData>.Instance.SearchPossession(eInventoryType.Item, pair.Id) < num)
          flag = false;
      }
      for (int index = 0; index < unlockUnitCondition.Equip.Count; ++index)
      {
        MasterUnlockUnitCondition.UnlockUnitCondition.Pair pair = unlockUnitCondition.Equip[index];
        int num = pair.Num;
        if (Singleton<UserData>.Instance.SearchPossession(eInventoryType.Equip, pair.Id) < num)
          flag = false;
      }
      return flag;
    }

    public static int GetUnlockUnitIdFromMaterial(int _material)
    {
      foreach (KeyValuePair<int, MasterUnlockUnitCondition.UnlockUnitCondition> keyValuePair in ManagerSingleton<MasterDataManager>.Instance.masterUnlockUnitCondition.dictionary)
      {
        MasterUnlockUnitCondition.UnlockUnitCondition unlockUnitCondition = keyValuePair.Value;
        if (unlockUnitCondition.ConsumeMaterialNum != 0 && unlockUnitCondition.MaterialId == _material)
          return keyValuePair.Key;
      }
      return 0;
    }

    public static int GetUnitRarityMaxLevel(int _unitId) => ManagerSingleton<MasterDataManager>.Instance.masterUnitRarity[_unitId].Count == 0 ? 5 : ManagerSingleton<MasterDataManager>.Instance.masterUnitRarity[_unitId].Count;

    public static long GetEquipmentParam(StatusParam _equipParam, eParamType _type)
    {
      switch (_type)
      {
        case eParamType.HP:
          return (long) _equipParam.Hp;
        case eParamType.ATK:
          return (long) (int) _equipParam.Atk;
        case eParamType.DEF:
          return (long) (int) _equipParam.Def;
        case eParamType.MAGIC_ATK:
          return (long) (int) _equipParam.MagicStr;
        case eParamType.MAGIC_DEF:
          return (long) (int) _equipParam.MagicDef;
        case eParamType.PHYSICAL_CRITICAL:
          return (long) (int) _equipParam.PhysicalCritical;
        case eParamType.MAGIC_CRITICAL:
          return (long) (int) _equipParam.MagicCritical;
        case eParamType.DODGE:
          return (long) (int) _equipParam.Dodge;
        case eParamType.LIFE_STEAL:
          return (long) (int) _equipParam.LifeSteal;
        case eParamType.WAVE_HP_RECOVERY:
          return (long) (int) _equipParam.WaveHpRecovery;
        case eParamType.WAVE_ENERGY_RECOVERY:
          return (long) (int) _equipParam.WaveEnergyRecovery;
        case eParamType.PHYSICAL_PENETRATE:
          return (long) (int) _equipParam.PhysicalPenetrate;
        case eParamType.MAGIC_PENETRATE:
          return (long) (int) _equipParam.MagicPenetrate;
        case eParamType.ENERGY_RECOVERY_RATE:
          return (long) (int) _equipParam.EnergyRecoveryRate;
        case eParamType.HP_RECOVERY_RATE:
          return (long) (int) _equipParam.HpRecoveryRate;
        case eParamType.ENERGY_REDUCE_RATE:
          return (long) (int) _equipParam.EnergyReduceRate;
        case eParamType.ACCURACY:
          return (long) (int) _equipParam.Accuracy;
        default:
          return 0;
      }
    }

    public static List<int> GetOpenMainSkill(int _unitId)
    {
      List<int> intList = new List<int>();
      UnitParameter unitParameter = Singleton<UserData>.Instance.UnitParameterDictionary[_unitId];
      if (0 < (int) unitParameter.UniqueData.UnionBurst[0].SkillLevel)
        intList.Add((int) unitParameter.UniqueData.UnionBurst[0].SkillId);
      List<SkillLevelInfo> unionBurst = unitParameter.UniqueData.UnionBurst;
      int index1 = 0;
      for (int count = unionBurst.Count; index1 < count; ++index1)
        intList.Add((int) unionBurst[index1].SkillId);
      List<SkillLevelInfo> mainSkill = unitParameter.UniqueData.MainSkill;
      int index2 = 0;
      for (int count = mainSkill.Count; index2 < count; ++index2)
        intList.Add((int) mainSkill[index2].SkillId);
      return intList;
    }

    public static List<int> GetOpenExSkill(int unitId)
    {
      List<int> intList = new List<int>();
      List<SkillLevelInfo> exSkill = Singleton<UserData>.Instance.UnitParameterDictionary[unitId].UniqueData.ExSkill;
      int index = 0;
      for (int count = exSkill.Count; index < count; ++index)
        intList.Add((int) exSkill[index].SkillId);
      return intList;
    }

    public static eDialogTextColor GetRightTextColor(int _previous, int _next)
    {
      eDialogTextColor eDialogTextColor = eDialogTextColor.Gray;
      int num = _next - _previous;
      if (num > 0)
        eDialogTextColor = eDialogTextColor.Increment;
      else if (num < 0)
        eDialogTextColor = eDialogTextColor.Decrement;
      return eDialogTextColor;
    }
    */
    public static int GetDefaultActionPatternId(int _id) => GetUnitResourceId(_id) * 100 + 1;
        /*
            public static int CalcUnitsTotalPower()
            {
              int num = 0;
              foreach (UnitParameter unitParameter in Singleton<UserData>.Instance.UnitParameterDictionary.Values)
              {
                if (unitParameter.UniqueData != null)
                  num += (int) unitParameter.UniqueData.Power;
                else if (unitParameter.UniqueDataForView != null)
                  num += (int) unitParameter.UniqueDataForView.Power;
              }
              return num;
            }

            public static int CalcUnitsTotalPower(List<UnitParameter> _unitParameters)
            {
              int num = 0;
              for (int index = 0; index < _unitParameters.Count; ++index)
              {
                UnitParameter unitParameter = _unitParameters[index];
                if (unitParameter.UniqueData != null)
                  num += (int) unitParameter.UniqueData.Power;
                else if (unitParameter.UniqueDataForView != null)
                  num += (int) unitParameter.UniqueDataForView.Power;
              }
              return num;
            }

            public static int ConversionClass1UnitId(int _baseUnitId)
            {
              int _unitId = _baseUnitId;
              int _jobId = 1;
              if (UnitUtility.GetClassId(_unitId) > _jobId)
                _unitId = UnitUtility.GetUnitId(UnitUtility.GetCharaId(_unitId), _jobId);
              return _unitId;
            }

            public static void RemoveSupportUnitFromParty(ePartyType _partyType)
            {
              int[] partyUnitIdArray = Singleton<UserData>.Instance.FindPartyUnitIdArray(_partyType);
              int[] _partyUnitIdArray = new int[5];
              int index1 = 0;
              for (int index2 = 0; index2 < partyUnitIdArray.Length; ++index2)
              {
                if (partyUnitIdArray[index2] != 1)
                {
                  _partyUnitIdArray[index1] = partyUnitIdArray[index2];
                  ++index1;
                }
              }
              Singleton<UserData>.Instance.SetPartyUnitIdArray(_partyType, _partyUnitIdArray);
              ApiManager instance = ManagerSingleton<ApiManager>.Instance;
              if (_partyType == ePartyType.CLAN_BATTLE)
                return;
              instance.AddDeckUpdatePostParam((int) _partyType, _partyUnitIdArray[0], _partyUnitIdArray[1], _partyUnitIdArray[2], _partyUnitIdArray[3], _partyUnitIdArray[4]);
              instance.Exec((System.Action) (() => {}));
            }

            public static void RemoveSupportUnitFromParty(ePartyType[] _partyTypeArray)
            {
              ApiManager instance = ManagerSingleton<ApiManager>.Instance;
              List<DeckListData> postDeckList = new List<DeckListData>();
              UserData userData = Singleton<UserData>.Instance;
              System.Action<ePartyType, int[]> action = (System.Action<ePartyType, int[]>) ((_partyType, _unitIdArray) =>
              {
                DeckListData deckListData = new DeckListData();
                deckListData.SetDeckNumber((int) _partyType);
                deckListData.SetUnitList(_unitIdArray);
                postDeckList.Add(deckListData);
                userData.SetPartyUnitIdArray(_partyType, _unitIdArray);
              });
              for (int index1 = 0; index1 < _partyTypeArray.Length; ++index1)
              {
                ePartyType partyType = _partyTypeArray[index1];
                int[] partyUnitIdArray = Singleton<UserData>.Instance.FindPartyUnitIdArray(partyType);
                int[] numArray = new int[5];
                int index2 = 0;
                for (int index3 = 0; index3 < partyUnitIdArray.Length; ++index3)
                {
                  if (partyUnitIdArray[index3] != 1)
                  {
                    numArray[index2] = partyUnitIdArray[index3];
                    ++index2;
                  }
                }
                action(partyType, numArray);
              }
              instance.AddDeckUpdateListPostParam(postDeckList);
              instance.Exec((System.Action) (() => {}));
            }

            public static InterfaceEquipmentData GetEquipmentDataIncludedUnknownData(
              int _equipmentId)
            {
              if (_equipmentId != 999999)
                return ItemUtil.GetEquipmentData(_equipmentId);
              if (UnitUtility.unknownEquipmentData == null)
                UnitUtility.unknownEquipmentData = new MasterEquipmentData.EquipmentData(999999, eTextId.UNKNOWN_EQUIPMENT_NAME.Name());
              return (InterfaceEquipmentData) UnitUtility.unknownEquipmentData;
            }

            public static string GetSortTypeParameterValueString(
              UnitParameter _unitParameter,
              UnitSort.eSortType _sortType)
            {
              int unitId = (int) _unitParameter.MasterData.UnitId;
              UnitData uniqueData = _unitParameter.UniqueData;
              UnitParam unitParam = uniqueData.UnitParam;
              switch (_sortType)
              {
                case UnitSort.eSortType.LV:
                  return _unitParameter.UniqueData.UnitLevel.ToString();
                case UnitSort.eSortType.POWER:
                  return uniqueData.Power.ToString();
                case UnitSort.eSortType.HP:
                  return uniqueData.TotalHp.ToString();
                case UnitSort.eSortType.ATK:
                  return uniqueData.TotalAtk.ToString();
                case UnitSort.eSortType.MAGIC_ATK:
                  return uniqueData.TotalMagicAtk.ToString();
                case UnitSort.eSortType.DEF:
                  return uniqueData.TotalDef.ToString();
                case UnitSort.eSortType.MAGIC_DEF:
                  return uniqueData.TotalMagicDef.ToString();
                case UnitSort.eSortType.RARITY:
                  return ((int) uniqueData.PromotionLevel).ToString();
                case UnitSort.eSortType.JAPANESE_SYLLABARY:
                  return (string) _unitParameter.MasterData.UnitName;
                case UnitSort.eSortType.AFFECTION_RANK:
                  return ((int) Singleton<UserData>.Instance.CharaParameterDictionary[UnitUtility.GetCharaId(unitId)].LoveLevel).ToString();
                default:
                  return "";
              }
            }

            public static bool IsEnoughEnhanceUseGold()
            {
              int totalGold = Singleton<UserData>.Instance.TotalGold;
              if (totalGold < 120)
                return false;
              MasterItemData masterItemData = ManagerSingleton<MasterDataManager>.Instance.masterItemData;
              MasterEquipmentData masterEquipmentData = ManagerSingleton<MasterDataManager>.Instance.masterEquipmentData;
              foreach (UserItemParameter upStrengthenItem in Singleton<UserData>.Instance.ListUpStrengthenItems())
              {
                int itemId = (int) upStrengthenItem.ItemId;
                MasterItemData.ItemData itemData = masterItemData.Get(itemId);
                if ((int) itemData.Type == 3 && (int) upStrengthenItem.ItemCount > 0)
                {
                  int num = (int) itemData.Value;
                  if (num > 0 && totalGold >= num * 120)
                    return true;
                }
              }
              foreach (KeyValuePair<int, int> keyValuePair in Singleton<UserData>.Instance.ListUpPossession(eInventoryType.Equip))
              {
                if (keyValuePair.Value > 0)
                {
                  MasterEquipmentData.EquipmentData equipmentData = masterEquipmentData.Get(keyValuePair.Key);
                  if (equipmentData != null)
                  {
                    int enhancementPoint = (int) equipmentData.EnhancementPoint;
                    if (enhancementPoint > 0 && totalGold >= enhancementPoint * 120)
                      return true;
                  }
                }
              }
              return false;
            }

            public static bool IsEnoughUniqueEnhanceUseGold(int _slotIndex, int _nowPoint)
            {
              List<UserItemParameter> userItemParameterList = Singleton<UserData>.Instance.ListUpStrengthenItems();
              MasterItemData masterItemData = ManagerSingleton<MasterDataManager>.Instance.masterItemData;
              MasterEquipmentData masterEquipmentData = ManagerSingleton<MasterDataManager>.Instance.masterEquipmentData;
              int _minPoint = 0;
              int num1 = 0;
              for (int index = 0; index < userItemParameterList.Count; ++index)
              {
                if ((int) userItemParameterList[index].ItemCount > 0)
                {
                  int itemId = (int) userItemParameterList[index].ItemId;
                  MasterItemData.ItemData itemData = masterItemData.Get(itemId);
                  if ((int) itemData.Type == 3)
                  {
                    if ((int) itemData.Value > 0)
                    {
                      int num2 = (int) itemData.Value;
                      if (_minPoint == 0)
                        _minPoint = num2;
                      else if (_minPoint > num2)
                        _minPoint = num2;
                    }
                    if (_minPoint == 1)
                      break;
                  }
                }
              }
              if (_minPoint == 1)
                return UnitUtility.checkUniqueEnhanceCost(_slotIndex, _nowPoint, _minPoint);
              num1 = 0;
              foreach (KeyValuePair<int, int> keyValuePair in Singleton<UserData>.Instance.ListUpPossession(eInventoryType.Equip))
              {
                if (keyValuePair.Value > 0)
                {
                  MasterEquipmentData.EquipmentData equipmentData = masterEquipmentData.Get(keyValuePair.Key);
                  if (equipmentData != null)
                  {
                    int enhancementPoint = (int) equipmentData.EnhancementPoint;
                    if (enhancementPoint > 0)
                    {
                      int num2 = enhancementPoint;
                      if (_minPoint == 0)
                        _minPoint = num2;
                      else if (_minPoint > num2)
                        _minPoint = num2;
                    }
                    if (_minPoint == 1)
                      break;
                  }
                }
              }
              return _minPoint != 0 && UnitUtility.checkUniqueEnhanceCost(_slotIndex, _nowPoint, _minPoint);
            }

            private static bool checkUniqueEnhanceCost(int _slotIndex, int _nowPoint, int _minPoint) => Singleton<UserData>.Instance.TotalGold >= ManagerSingleton<MasterDataManager>.Instance.masterUniqueEquipmentEnhanceData.RequireCost(_slotIndex, _nowPoint, _minPoint);

            public static bool IsPromotionPossible(Dictionary<int, int> _needEquipItems)
            {
              MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
              Dictionary<int, int> dictionary = new Dictionary<int, int>();
              foreach (KeyValuePair<int, int> needEquipItem in _needEquipItems)
              {
                int num1 = Singleton<UserData>.Instance.SearchPossession(eInventoryType.Equip, needEquipItem.Key);
                if (num1 < needEquipItem.Value)
                {
                  int num2 = needEquipItem.Value - num1;
                  MasterEquipmentCraft.EquipmentCraft equipmentCraft = instance.masterEquipmentCraft.Get(needEquipItem.Key);
                  if (equipmentCraft == null)
                    return false;
                  int conditionEquipmentCount = equipmentCraft.GetConditionEquipmentCount();
                  if (conditionEquipmentCount <= 0)
                    return false;
                  for (int _index = 0; _index < conditionEquipmentCount; ++_index)
                  {
                    int conditionEquipmentId = equipmentCraft.GetConditionEquipmentId(_index);
                    int conditionConsumeNum = equipmentCraft.GetConditionConsumeNum(_index);
                    if (dictionary.ContainsKey(conditionEquipmentId))
                      dictionary[conditionEquipmentId] += conditionConsumeNum * num2;
                    else
                      dictionary.Add(conditionEquipmentId, conditionConsumeNum * num2);
                  }
                }
              }
              foreach (KeyValuePair<int, int> keyValuePair in dictionary)
              {
                if (Singleton<UserData>.Instance.SearchPossession(eInventoryType.Equip, keyValuePair.Key) < keyValuePair.Value)
                  return false;
              }
              return true;
            }

            public static void UpdateUnitNotification(
              UnitDefine.UnitNotificationType _type = UnitDefine.UnitNotificationType.ALL,
              int _unitId = 0,
              int _calcLocalGold = -1,
              bool _isUpdateEquip = false,
              int _levelOffset = 0)
            {
              if (!ManagerSingleton<MasterDataManager>.Instance.IsSetupFinished || TutorialManager.IsStartTutorial)
                return;
              bool _isEnhanceRelease = ManagerSingleton<MasterDataManager>.Instance.masterContentReleaseData.CheckReleaseContents(eSystemId.UNIT_EQUIP_ENHANCE);
              switch (_type)
              {
                case UnitDefine.UnitNotificationType.ALL:
                  UnitUtility.updateUnitEquipNotificationInfo(_isUpdateEquip, _isEnhanceRelease);
                  UnitUtility.updateUnitRarityNotificationInfo();
                  UnitUtility.updateUnlockUnitNotificationInfo();
                  UnitUtility.updateSkillEnhanceNotification();
                  UnitUtility.updateLevelUpNotification();
                  break;
                case UnitDefine.UnitNotificationType.EQUIP:
                  UnitUtility.updateUnitEquipNotificationInfo(_isUpdateEquip, _isEnhanceRelease);
                  break;
                case UnitDefine.UnitNotificationType.SPECIFIC_EQUIP:
                  UnitUtility.updateSpecificUnitEquipNotificationInfo(_isUpdateEquip, _unitId, _isEnhanceRelease);
                  break;
                case UnitDefine.UnitNotificationType.SKILL:
                  UnitUtility.updateSkillEnhanceNotification();
                  break;
                case UnitDefine.UnitNotificationType.SKILL_TARGET_UNIT:
                  UnitUtility.updateSkillEnhanceLocalNotification(_unitId, _calcLocalGold, _levelOffset);
                  break;
                case UnitDefine.UnitNotificationType.LEVEL_UP:
                  UnitUtility.updateLevelUpNotification(_unitId);
                  UnitUtility.updateUniqueEquipNotificationInfo(_isUpdateEquip, _unitId, _isEnhanceRelease);
                  break;
                case UnitDefine.UnitNotificationType.HIGH_RARITY:
                  UnitUtility.updateHighRarityEquipNotificationInfo(_isUpdateEquip, _unitId);
                  break;
              }
            }

            private static void updateUnitEquipNotificationInfo(bool _isUpdateEquip, bool _isEnhanceRelease)
            {
              foreach (KeyValuePair<int, UnitParameter> unitParameter in Singleton<UserData>.Instance.UnitParameterDictionary)
              {
                UnitUtility.setUnitEquipNotificationInfo(_isUpdateEquip, unitParameter.Value, _isEnhanceRelease);
                UnitUtility.setUnitUniqueEquipNotificationInfo(false, unitParameter.Value, _isEnhanceRelease);
                UnitUtility.setUnitHighRarityEquipNotificationInfo(false, unitParameter.Value);
              }
            }

            private static void updateSpecificUnitEquipNotificationInfo(
              bool _isUpdateEquip,
              int _unitId,
              bool _isEnhanceRelease)
            {
              UnitParameter _unitParam = (UnitParameter) null;
              if (!Singleton<UserData>.Instance.UnitParameterDictionary.TryGetValue(_unitId, out _unitParam))
                return;
              UnitUtility.setUnitEquipNotificationInfo(_isUpdateEquip, _unitParam, _isEnhanceRelease);
              UnitUtility.setUnitUniqueEquipNotificationInfo(false, _unitParam, _isEnhanceRelease);
            }

            private static void updateUniqueEquipNotificationInfo(
              bool _isUpdateEquip,
              int _unitId,
              bool _isEnhanceRelease,
              int _temporaryAddLv = 0)
            {
              UnitParameter _unitParam = (UnitParameter) null;
              if (!Singleton<UserData>.Instance.UnitParameterDictionary.TryGetValue(_unitId, out _unitParam))
                return;
              UnitUtility.setUnitUniqueEquipNotificationInfo(_isUpdateEquip, _unitParam, _isEnhanceRelease, _temporaryAddLv);
            }

            private static void updateHighRarityEquipNotificationInfo(bool _isUpdateEquip, int _unitId)
            {
              UnitParameter _unitParam = (UnitParameter) null;
              if (!Singleton<UserData>.Instance.UnitParameterDictionary.TryGetValue(_unitId, out _unitParam))
                return;
              UnitUtility.setUnitHighRarityEquipNotificationInfo(_isUpdateEquip, _unitParam);
            }

            private static void setUnitEquipNotificationInfo(
              bool _isUpdateEquip,
              UnitParameter _unitParam,
              bool _isEnhanceRelease)
            {
              int id1 = (int) _unitParam.UniqueData.Id;
              Dictionary<int, UnitNotificationInfo> unitNotification = Singleton<UserData>.Instance.UnitNotification;
              bool flag = true;
              if (_isUpdateEquip)
                UnitUtility.UpdateUnitEquipmentSlot(id1);
              UnitNotificationInfo notificationInfo;
              if (!unitNotification.TryGetValue(id1, out notificationInfo))
              {
                notificationInfo = new UnitNotificationInfo(id1, _level: ((int) _unitParam.UniqueData.UnitLevel));
                unitNotification.Add(id1, notificationInfo);
              }
              else
                notificationInfo.IsNoticeUnlock = false;
              List<EquipSlot> equipSlot = _unitParam.UniqueData.EquipSlot;
              Dictionary<int, int> _needEquipItems = new Dictionary<int, int>();
              for (int index = 0; index < equipSlot.Count; ++index)
              {
                int id2 = (int) equipSlot[index].Id;
                UnitDefine.UnitEquipmentStatus status = (UnitDefine.UnitEquipmentStatus) (int) equipSlot[index].Status;
                if (status == UnitDefine.UnitEquipmentStatus.NO_POSSESION || status == UnitDefine.UnitEquipmentStatus.LV_SHORTAGE || (status == UnitDefine.UnitEquipmentStatus.NO_POSSESION_CANNOT_CRAFT || status == UnitDefine.UnitEquipmentStatus.UNKNOWN))
                {
                  flag = false;
                  notificationInfo.SlotList[index].SetEquip(false);
                  notificationInfo.SlotList[index].SetEnhance(false);
                  notificationInfo.SlotList[index].SetCraftCost(-1);
                }
                else
                {
                  switch (status)
                  {
                    case UnitDefine.UnitEquipmentStatus.CAN_EQUIP:
                    case UnitDefine.UnitEquipmentStatus.CAN_CRAFT:
                    case UnitDefine.UnitEquipmentStatus.CAN_EQUIP_LV_SHORTAGE:
                    case UnitDefine.UnitEquipmentStatus.CAN_CRAFT_LV_SHORTAGE:
                      notificationInfo.SlotList[index].SetEquip(true);
                      notificationInfo.SlotList[index].SetEnhance(false);
                      if (_needEquipItems.ContainsKey(id2))
                        _needEquipItems[id2]++;
                      else
                        _needEquipItems.Add(id2, 1);
                      if (status == UnitDefine.UnitEquipmentStatus.CAN_CRAFT || status == UnitDefine.UnitEquipmentStatus.CAN_CRAFT_LV_SHORTAGE)
                      {
                        notificationInfo.SlotList[index].SetCraftCost(UnitUtility.CalcNeededCraftCost(id2));
                        continue;
                      }
                      notificationInfo.SlotList[index].SetCraftCost(-1);
                      continue;
                    case UnitDefine.UnitEquipmentStatus.EQUIPPED:
                      notificationInfo.SlotList[index].SetEquip(false);
                      if (_isEnhanceRelease)
                        notificationInfo.SlotList[index].SetEnhance(UnitUtility.settingUnitEquipEnhanceStatus(equipSlot[index]));
                      else
                        notificationInfo.SlotList[index].SetEnhance(false);
                      notificationInfo.SlotList[index].SetCraftCost(-1);
                      continue;
                    default:
                      flag = false;
                      continue;
                  }
                }
              }
              if ((ePromotionLevel) UnitUtility.GetUnitMaxRank(id1) <= _unitParam.UniqueData.PromotionLevel)
                flag = false;
              else if (flag)
                flag = UnitUtility.IsPromotionPossible(_needEquipItems);
              notificationInfo.IsNoticePromotion = flag;
              unitNotification[id1] = notificationInfo;
            }

            private static void setUnitUniqueEquipNotificationInfo(
              bool _isUpdateEquip,
              UnitParameter _unitParam,
              bool _isEnhanceRelease,
              int _temporaryAddLv = 0)
            {
              int id1 = (int) _unitParam.UniqueData.Id;
              Dictionary<int, UnitNotificationInfo> unitNotification = Singleton<UserData>.Instance.UnitNotification;
              if (_isUpdateEquip)
                UnitUtility.UpdateUnitEquipmentSlot(id1);
              UnitNotificationInfo notificationInfo;
              if (!unitNotification.TryGetValue(id1, out notificationInfo))
              {
                notificationInfo = new UnitNotificationInfo(id1, _level: ((int) _unitParam.UniqueData.UnitLevel));
                unitNotification.Add(id1, notificationInfo);
              }
              else
              {
                notificationInfo.IsNoticeUnlock = false;
                notificationInfo.CheckAndCreateUniqueSlot(id1);
              }
              if (UnitUtility.GetUniqueEquipSlotNum(id1) == 0)
                return;
              List<EquipSlot> uniqueEquipSlot = _unitParam.UniqueData.UniqueEquipSlot;
              Dictionary<int, int> dictionary = new Dictionary<int, int>();
              for (int index = 0; index < uniqueEquipSlot.Count; ++index)
              {
                int id2 = (int) uniqueEquipSlot[index].Id;
                UnitDefine.UnitEquipmentStatus status = (UnitDefine.UnitEquipmentStatus) (int) uniqueEquipSlot[index].Status;
                switch (status)
                {
                  case UnitDefine.UnitEquipmentStatus.NO_POSSESION:
                  case UnitDefine.UnitEquipmentStatus.NO_POSSESION_CANNOT_CRAFT:
                  case UnitDefine.UnitEquipmentStatus.LV_SHORTAGE:
                  case UnitDefine.UnitEquipmentStatus.UNKNOWN:
                  case UnitDefine.UnitEquipmentStatus.RANK_SHORTAGE:
                    notificationInfo.UniqueSlotList[index].SetEquip(false);
                    notificationInfo.UniqueSlotList[index].SetEnhance(false);
                    notificationInfo.UniqueSlotList[index].SetCraftCost(-1);
                    notificationInfo.UniqueSlotList[index].SetOverLimit(UnitDefine.UniqueEquipOverLimitStatus.IMPOSSIBLE);
                    break;
                  case UnitDefine.UnitEquipmentStatus.CAN_EQUIP:
                  case UnitDefine.UnitEquipmentStatus.CAN_CRAFT:
                  case UnitDefine.UnitEquipmentStatus.CAN_EQUIP_LV_SHORTAGE:
                  case UnitDefine.UnitEquipmentStatus.CAN_CRAFT_LV_SHORTAGE:
                    notificationInfo.UniqueSlotList[index].SetEquip(true);
                    notificationInfo.UniqueSlotList[index].SetEnhance(false);
                    if (dictionary.ContainsKey(id2))
                      dictionary[id2]++;
                    else
                      dictionary.Add(id2, 1);
                    if (status == UnitDefine.UnitEquipmentStatus.CAN_CRAFT || status == UnitDefine.UnitEquipmentStatus.CAN_CRAFT_LV_SHORTAGE)
                    {
                      notificationInfo.UniqueSlotList[index].SetCraftCost(UnitUtility.CalcNeededCraftCost(id2));
                      break;
                    }
                    notificationInfo.UniqueSlotList[index].SetCraftCost(-1);
                    break;
                  case UnitDefine.UnitEquipmentStatus.EQUIPPED:
                    if (_isEnhanceRelease)
                    {
                      notificationInfo.UniqueSlotList[index] = UnitUtility.settingUnitUniqueEquipEnhanceStatus(uniqueEquipSlot[index], index, (int) _unitParam.UniqueData.UnitLevel + _temporaryAddLv);
                      break;
                    }
                    notificationInfo.UniqueSlotList[index].SetEquip(false);
                    notificationInfo.UniqueSlotList[index].SetEnhance(false);
                    notificationInfo.UniqueSlotList[index].SetCraftCost(-1);
                    notificationInfo.UniqueSlotList[index].SetOverLimit(UnitDefine.UniqueEquipOverLimitStatus.IMPOSSIBLE);
                    break;
                }
              }
              unitNotification[id1] = notificationInfo;
            }

            private static void setUnitHighRarityEquipNotificationInfo(
              bool _isUpdateEquip,
              UnitParameter _unitParam)
            {
              int id = (int) _unitParam.UniqueData.Id;
              if (!UnitUtility.IsUnlockRaritySix(id))
                return;
              UserData instance = Singleton<UserData>.Instance;
              Dictionary<int, UnitNotificationInfo> unitNotification = instance.UnitNotification;
              if (_isUpdateEquip)
                UnitUtility.UpdateUnitEquipmentSlot(id);
              UnitNotificationInfo notificationInfo;
              if (!unitNotification.TryGetValue(id, out notificationInfo))
              {
                notificationInfo = new UnitNotificationInfo(id, _level: ((int) _unitParam.UniqueData.UnitLevel));
                unitNotification.Add(id, notificationInfo);
              }
              else
                notificationInfo.IsNoticeUnlock = false;
              List<EquipSlot> uniqueEquipSlot = _unitParam.UniqueData.UniqueEquipSlot;
              bool flag1 = uniqueEquipSlot.Count > 0;
              if (flag1)
                flag1 = (bool) uniqueEquipSlot[0].IsSlot;
              bool flag2 = true;
              MasterUnlockRarity6 masterUnlockRarity6 = ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6;
              for (int index = 0; index < 3; ++index)
              {
                switch ((UnitDefine.UnitEquipmentStatus) UnitUtility.GetUnlockRarityStatus(_unitParam.UniqueData.UnlockRarity6Item, index + 1))
                {
                  case UnitDefine.UnitEquipmentStatus.NO_POSSESION:
                  case UnitDefine.UnitEquipmentStatus.NO_POSSESION_CANNOT_CRAFT:
                    notificationInfo.HighRaritySlotList[index].SetEquip(false);
                    notificationInfo.HighRaritySlotList[index].SetEnhance(false);
                    notificationInfo.HighRaritySlotList[index].SetCraftCost(-1);
                    notificationInfo.HighRaritySlotList[index].SetOverLimit(UnitDefine.UniqueEquipOverLimitStatus.IMPOSSIBLE);
                    flag2 = false;
                    break;
                  case UnitDefine.UnitEquipmentStatus.CAN_EQUIP:
                    int unlockRarityLevel1 = UnitUtility.GetUnlockRarityLevel(_unitParam.UniqueData.UnlockRarity6Item, index + 1);
                    int consumeGold1 = (int) masterUnlockRarity6.Get(id, (byte) (index + 1), (byte) (unlockRarityLevel1 + 1)).consume_gold;
                    bool flag3 = instance.TotalGold >= consumeGold1 & flag1;
                    notificationInfo.HighRaritySlotList[index].SetEquip(flag3);
                    notificationInfo.HighRaritySlotList[index].SetEnhance(false);
                    notificationInfo.HighRaritySlotList[index].SetCraftCost(flag3 ? consumeGold1 : -1);
                    notificationInfo.HighRaritySlotList[index].SetOverLimit(UnitDefine.UniqueEquipOverLimitStatus.IMPOSSIBLE);
                    flag2 = false;
                    break;
                  case UnitDefine.UnitEquipmentStatus.EQUIPPED:
                    notificationInfo.HighRaritySlotList[index].SetEquip(false);
                    int unlockRarityLevel2 = UnitUtility.GetUnlockRarityLevel(_unitParam.UniqueData.UnlockRarity6Item, index + 1);
                    if (unlockRarityLevel2 >= masterUnlockRarity6.GetListWithUnitIdAndSlotIdOrderByUnlockLevelAsc(id, (byte) (index + 1)).Count)
                    {
                      notificationInfo.HighRaritySlotList[index].SetEnhance(false);
                      notificationInfo.HighRaritySlotList[index].SetCraftCost(-1);
                      notificationInfo.HighRaritySlotList[index].SetOverLimit(UnitDefine.UniqueEquipOverLimitStatus.MAX);
                      break;
                    }
                    if (UnitUtility.isEnoughHighRarityItemNum(id, index + 1, unlockRarityLevel2 + 1))
                    {
                      int consumeGold2 = (int) masterUnlockRarity6.Get(id, (byte) (index + 1), (byte) (unlockRarityLevel2 + 1)).consume_gold;
                      bool flag4 = instance.TotalGold >= consumeGold2;
                      notificationInfo.HighRaritySlotList[index].SetEnhance(flag4);
                      notificationInfo.HighRaritySlotList[index].SetCraftCost(flag4 ? consumeGold2 : -1);
                      notificationInfo.HighRaritySlotList[index].SetOverLimit(flag4 ? UnitDefine.UniqueEquipOverLimitStatus.POSSIBLE : UnitDefine.UniqueEquipOverLimitStatus.IMPOSSIBLE);
                      flag2 = false;
                      break;
                    }
                    notificationInfo.HighRaritySlotList[index].SetEnhance(false);
                    notificationInfo.HighRaritySlotList[index].SetCraftCost(-1);
                    notificationInfo.HighRaritySlotList[index].SetOverLimit(UnitDefine.UniqueEquipOverLimitStatus.IMPOSSIBLE);
                    flag2 = false;
                    break;
                  case UnitDefine.UnitEquipmentStatus.STAR_SHORTAGE:
                    notificationInfo.HighRaritySlotList[index].SetEquip(false);
                    notificationInfo.HighRaritySlotList[index].SetEnhance(false);
                    notificationInfo.HighRaritySlotList[index].SetCraftCost(-1);
                    notificationInfo.HighRaritySlotList[index].SetOverLimit(UnitDefine.UniqueEquipOverLimitStatus.IMPOSSIBLE);
                    flag2 = false;
                    break;
                }
              }
              if ((int) _unitParam.UniqueData.UnitRarity == 5)
              {
                UnitData uniqueData = instance.UnitParameterDictionary[id].UniqueData;
                notificationInfo.IsNoticeHighRarityEvolution = (int) uniqueData.UnlockRarity6Item.QuestClear > 0;
              }
              else if ((int) _unitParam.UniqueData.UnitRarity == 6)
              {
                flag2 = false;
                notificationInfo.IsNoticeHighRarityEvolution = false;
              }
              notificationInfo.IsNoticeHighRarityQuest = flag2;
              unitNotification[id] = notificationInfo;
            }

            private static bool isEnoughHighRarityItemNum(int _unitId, int _slotId, int _checkItemLevel)
            {
              MasterUnlockRarity6.UnlockRarity6 unlockRarity6 = ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6.Get(_unitId, (byte) _slotId, (byte) _checkItemLevel);
              return Singleton<UserData>.Instance.SearchPossession(eInventoryType.Item, (int) unlockRarity6.material_id) >= (int) unlockRarity6.material_count;
            }

            private static bool settingUnitEquipEnhanceStatus(EquipSlot _equipSlot)
            {
              InterfaceEquipmentData includedUnknownData = UnitUtility.GetEquipmentDataIncludedUnknownData((int) _equipSlot.Id);
              int count = ManagerSingleton<MasterDataManager>.Instance.masterEquipmentEnhanceData[includedUnknownData.Promotion - 1].Count;
              return includedUnknownData.Promotion > 1 && (int) _equipSlot.EnhancementLevel < count;
            }

            private static NoticeEquipStatus settingUnitUniqueEquipEnhanceStatus(
              EquipSlot _equipSlot,
              int _slotIndex,
              int _unitLevel)
            {
              NoticeEquipStatus noticeEquipStatus = new NoticeEquipStatus(_isUnique: true);
              MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
              MasterUniqueEquipmentEnhanceData equipmentEnhanceData = instance.masterUniqueEquipmentEnhanceData;
              MasterUniqueEquipmentRankup uniqueEquipmentRankup1 = instance.masterUniqueEquipmentRankup;
              if ((int) _equipSlot.EnhancementLevel >= equipmentEnhanceData.LevelMax(_slotIndex))
              {
                noticeEquipStatus.SetOverLimit(UnitDefine.UniqueEquipOverLimitStatus.MAX);
                return noticeEquipStatus;
              }
              int num = equipmentEnhanceData.LimitLevel(_slotIndex, (int) _equipSlot.Rank);
              if ((int) _equipSlot.EnhancementLevel < num)
                noticeEquipStatus.SetEnhance(UnitUtility.IsEnoughUniqueEnhanceUseGold(_slotIndex, (int) _equipSlot.EnhancementPt));
              int maxEquipRank = uniqueEquipmentRankup1.GetMaxEquipRank((int) _equipSlot.Id);
              MasterUniqueEquipmentRankup.UniqueEquipmentRankup uniqueEquipmentRankup2 = uniqueEquipmentRankup1.Get((int) _equipSlot.Id, (int) _equipSlot.Rank);
              if ((int) _equipSlot.Rank > maxEquipRank)
              {
                noticeEquipStatus.SetOverLimit(UnitDefine.UniqueEquipOverLimitStatus.MAX);
                return noticeEquipStatus;
              }
              if (_unitLevel < (int) uniqueEquipmentRankup2.UnitLevel || UnitUtility.CreateUniqueEquipmentRankupPostData((int) _equipSlot.Id, (int) _equipSlot.Rank) == null)
                return noticeEquipStatus;
              noticeEquipStatus.SetOverLimit(Singleton<UserData>.Instance.TotalGold >= uniqueEquipmentRankup2.GetCraftCost ? UnitDefine.UniqueEquipOverLimitStatus.POSSIBLE : UnitDefine.UniqueEquipOverLimitStatus.IMPOSSIBLE);
              return noticeEquipStatus;
            }

            private static void updateUnitRarityNotificationInfo()
            {
              Dictionary<int, UnitParameter> parameterDictionary = Singleton<UserData>.Instance.UnitParameterDictionary;
              Dictionary<int, UnitNotificationInfo> unitNotification = Singleton<UserData>.Instance.UnitNotification;
              foreach (KeyValuePair<int, UnitParameter> keyValuePair in parameterDictionary)
              {
                int key = keyValuePair.Key;
                UnitNotificationInfo notificationInfo;
                if (!unitNotification.TryGetValue(key, out notificationInfo))
                {
                  notificationInfo = new UnitNotificationInfo(key, _level: ((int) keyValuePair.Value.UniqueData.UnitLevel));
                  unitNotification.Add(key, notificationInfo);
                }
                else
                  notificationInfo.IsNoticeUnlock = false;
                List<MasterUnitRarity.UnitRarity> unitRarityList = ManagerSingleton<MasterDataManager>.Instance.masterUnitRarity[key];
                if (((int) keyValuePair.Value.UniqueData.UnitRarity >= 5 ? 1 : 0) == 0)
                {
                  MasterUnitRarity.UnitRarity unitRarity = unitRarityList[(int) keyValuePair.Value.UniqueData.UnitRarity];
                  notificationInfo.IsNoticeEvolution = Singleton<UserData>.Instance.SearchPossession(eInventoryType.Item, (int) unitRarity.unit_material_id) >= (int) unitRarity.consume_num & Singleton<UserData>.Instance.TotalGold >= (int) unitRarity.consume_gold;
                }
                else
                  notificationInfo.IsNoticeEvolution = false;
                unitNotification[key] = notificationInfo;
              }
            }

            private static void updateUnlockUnitNotificationInfo()
            {
              MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
              Dictionary<int, MasterUnlockUnitCondition.UnlockUnitCondition> dictionary = instance.masterUnlockUnitCondition.dictionary;
              Dictionary<int, UnitNotificationInfo> unitNotification = Singleton<UserData>.Instance.UnitNotification;
              foreach (KeyValuePair<int, MasterUnlockUnitCondition.UnlockUnitCondition> keyValuePair in dictionary)
              {
                int key = keyValuePair.Key;
                if ((int) instance.masterUnitData[key].OnlyDispOwned <= 0 && !Singleton<UserData>.Instance.IsMyUnit(key))
                {
                  UnitNotificationInfo notificationInfo;
                  if (!unitNotification.TryGetValue(key, out notificationInfo))
                  {
                    notificationInfo = new UnitNotificationInfo(key);
                    unitNotification.Add(key, notificationInfo);
                  }
                  MasterUnlockUnitCondition.UnlockUnitCondition unlockUnitCondition = instance.masterUnlockUnitCondition[key];
                  bool flag1 = false;
                  bool flag2 = false;
                  if (unlockUnitCondition.ConsumeMaterialNum != 0)
                  {
                    int materialId = unlockUnitCondition.MaterialId;
                    flag1 = unlockUnitCondition.ConsumeMaterialNum > Singleton<UserData>.Instance.SearchPossession(eInventoryType.Item, materialId) || flag1;
                  }
                  for (int index = 0; index < unlockUnitCondition.Equip.Count; ++index)
                  {
                    MasterUnlockUnitCondition.UnlockUnitCondition.Pair pair = unlockUnitCondition.Equip[index];
                    flag1 = pair.Num > Singleton<UserData>.Instance.SearchPossession(eInventoryType.Equip, pair.Id) || flag1;
                  }
                  for (int index = 0; index < unlockUnitCondition.Item.Count; ++index)
                  {
                    MasterUnlockUnitCondition.UnlockUnitCondition.Pair pair = unlockUnitCondition.Item[index];
                    flag1 = pair.Num > Singleton<UserData>.Instance.SearchPossession(eInventoryType.Item, pair.Id) || flag1;
                  }
                  int consumeGold = unlockUnitCondition.ConsumeGold;
                  if (consumeGold > 0)
                  {
                    int totalGold = Singleton<UserData>.Instance.TotalGold;
                    flag2 = consumeGold >= totalGold || flag2;
                  }
                  notificationInfo.IsNoticeUnlock = !flag1 && !flag2 && !UnitUtility.IsLimitedUnit(key);
                  unitNotification[key] = notificationInfo;
                }
              }
            }

            private static void updateLevelUpNotification()
            {
              foreach (KeyValuePair<int, UnitParameter> unitParameter in Singleton<UserData>.Instance.UnitParameterDictionary)
                UnitUtility.updateLevelUpNotification(unitParameter.Key);
            }

            public static void UpdateLevelUpNotification(
              int _unitId,
              Dictionary<int, UserItemParameter> _usedItem = null)
            {
              if (!ManagerSingleton<MasterDataManager>.Instance.IsSetupFinished || TutorialManager.IsStartTutorial)
                return;
              UnitUtility.updateLevelUpNotification(_unitId, _usedItem);
            }

            private static void updateLevelUpNotification(
              int _unitId,
              Dictionary<int, UserItemParameter> _usedItem = null)
            {
              UserData instance = Singleton<UserData>.Instance;
              Dictionary<int, UnitParameter> parameterDictionary = instance.UnitParameterDictionary;
              Dictionary<int, UnitNotificationInfo> unitNotification = instance.UnitNotification;
              MasterItemData masterItemData = ManagerSingleton<MasterDataManager>.Instance.masterItemData;
              MasterExperienceUnit masterExperienceUnit = ManagerSingleton<MasterDataManager>.Instance.masterExperienceUnit;
              UnitParameter unitParameter = (UnitParameter) null;
              int key = _unitId;
              ref UnitParameter local = ref unitParameter;
              if (!parameterDictionary.TryGetValue(key, out local))
                return;
              UnitData uniqueData = unitParameter.UniqueData;
              UnitNotificationInfo notificationInfo;
              if (!unitNotification.TryGetValue((int) uniqueData.Id, out notificationInfo))
              {
                notificationInfo = new UnitNotificationInfo((int) uniqueData.Id, _level: ((int) uniqueData.UnitLevel));
                unitNotification.Add((int) uniqueData.Id, notificationInfo);
              }
              int num1 = (int) uniqueData.UnitLevel;
              int num2 = 0;
              if (_usedItem != null)
              {
                foreach (KeyValuePair<int, UserItemParameter> keyValuePair in _usedItem)
                  num2 += (int) masterItemData.Get((int) keyValuePair.Value.ItemId).Value * (int) keyValuePair.Value.ItemCount;
              }
              int num3 = (int) uniqueData.UnitExp + num2;
              for (int _level = (int) uniqueData.UnitLevel + 1; _level <= (int) instance.UserInfo.TeamLevel && masterExperienceUnit[_level].total_exp <= num3; ++_level)
                num1 = _level;
              if (_usedItem != null)
                UnitUtility.updateTemporaryLvUniqueEquipNotification(_unitId, num1 - (int) uniqueData.UnitLevel);
              if (num1 >= (int) instance.UserInfo.TeamLevel)
              {
                notificationInfo.IsNoticeLevelUp = false;
                unitNotification[(int) uniqueData.Id] = notificationInfo;
              }
              else
              {
                int num4 = UnitUtility.calcStrengthItemExp() - num2;
                if (masterExperienceUnit[num1 + 1].total_exp <= num3 + num4)
                {
                  notificationInfo.IsNoticeLevelUp = true;
                  unitNotification[(int) uniqueData.Id] = notificationInfo;
                }
                else
                {
                  notificationInfo.IsNoticeLevelUp = false;
                  unitNotification[(int) uniqueData.Id] = notificationInfo;
                }
              }
            }

            private static void updateTemporaryLvUniqueEquipNotification(int _unitId, int _temporaryAddLv = 0)
            {
              bool _isEnhanceRelease = ManagerSingleton<MasterDataManager>.Instance.masterContentReleaseData.CheckReleaseContents(eSystemId.UNIT_EQUIP_ENHANCE);
              UnitUtility.updateUniqueEquipNotificationInfo(false, _unitId, _isEnhanceRelease, _temporaryAddLv);
            }

            private static void updateSkillNoticeInfo(
              List<SkillLevelInfo> _list,
              Dictionary<int, SkillNoticeInfo> _dic,
              UnitParameter _value,
              int _gold = -1,
              int _levelOffset = 0)
            {
              for (int index = 0; index < _list.Count; ++index)
              {
                if (_dic.ContainsKey((int) _list[index].SkillId))
                {
                  SkillNoticeInfo skillNoticeInfo = _dic[(int) _list[index].SkillId];
                  skillNoticeInfo.SkillLevel = (int) _list[index].SkillLevel;
                  skillNoticeInfo.IsEnhance = UnitUtility.IsSkillEnhancePossible(_list[index], _value, _gold, _levelOffset);
                  _dic[(int) _list[index].SkillId] = skillNoticeInfo;
                }
                else
                  _dic.Add((int) _list[index].SkillId, new SkillNoticeInfo((int) _list[index].SkillLevel, UnitUtility.IsSkillEnhancePossible(_list[index], _value, _gold, _levelOffset)));
              }
            }

            private static void updateSkillEnhanceNotification()
            {
              Dictionary<int, UnitParameter> parameterDictionary = Singleton<UserData>.Instance.UnitParameterDictionary;
              Dictionary<int, UnitNotificationInfo> unitNotification = Singleton<UserData>.Instance.UnitNotification;
              UserData instance = Singleton<UserData>.Instance;
              bool flag = ManagerSingleton<MasterDataManager>.Instance.masterContentReleaseData.CheckReleaseContents(eSystemId.UNIT_SKILL_LVUP);
              foreach (KeyValuePair<int, UnitParameter> keyValuePair in parameterDictionary)
              {
                int key = keyValuePair.Key;
                UnitNotificationInfo notificationInfo;
                if (!unitNotification.TryGetValue(key, out notificationInfo))
                {
                  notificationInfo = new UnitNotificationInfo(key, _level: ((int) keyValuePair.Value.UniqueData.UnitLevel));
                  unitNotification.Add(key, notificationInfo);
                }
                else
                  notificationInfo.IsNoticeUnlock = false;
                if (!flag)
                {
                  if (notificationInfo.SkillInfo.Count > 0)
                    notificationInfo.SkillInfo.Clear();
                }
                else
                {
                  UnitUtility.updateSkillNoticeInfo(keyValuePair.Value.UniqueData.UnionBurst, notificationInfo.SkillInfo, keyValuePair.Value);
                  UnitUtility.updateSkillNoticeInfo(keyValuePair.Value.UniqueData.MainSkill, notificationInfo.SkillInfo, keyValuePair.Value);
                  UnitUtility.updateSkillNoticeInfo(keyValuePair.Value.UniqueData.ExSkill, notificationInfo.SkillInfo, keyValuePair.Value);
                }
                unitNotification[key] = notificationInfo;
              }
            }

            public static void CheckSkillNoticeInfo(UnitData _uniqueData)
            {
              Dictionary<int, UnitNotificationInfo> unitNotification = Singleton<UserData>.Instance.UnitNotification;
              if (!unitNotification.ContainsKey((int) _uniqueData.Id))
                return;
              bool isUpdate = false;
              UnitNotificationInfo noticeInfo = unitNotification[(int) _uniqueData.Id];
              System.Action<List<SkillLevelInfo>> action = (System.Action<List<SkillLevelInfo>>) (_list =>
              {
                for (int index = 0; index < _list.Count; ++index)
                {
                  if (noticeInfo.SkillInfo.ContainsKey((int) _list[index].SkillId))
                    isUpdate = true;
                }
              });
              action(_uniqueData.UnionBurst);
              action(_uniqueData.MainSkill);
              action(_uniqueData.ExSkill);
              if (!isUpdate)
                return;
              unitNotification[(int) _uniqueData.Id].SkillInfo.Clear();
              UnitUtility.updateSkillEnhanceNotification();
            }

            private static bool IsSkillEnhancePossible(
              SkillLevelInfo _skillInfo,
              UnitParameter _unit,
              int _calcLocalGold = -1,
              int _levelOffset = 0)
            {
              UserData instance = Singleton<UserData>.Instance;
              if ((int) _skillInfo.SkillLevel >= (int) _unit.UniqueData.UnitLevel + _levelOffset)
                return false;
              int num = _calcLocalGold == -1 ? Singleton<UserData>.Instance.TotalGold : _calcLocalGold;
              return (int) ManagerSingleton<MasterDataManager>.Instance.masterSkillCost.Get((int) _skillInfo.SkillLevel + 1).cost <= num;
            }

            private static void updateSkillEnhanceLocalNotification(
              int _unitId,
              int _calcLocalGold,
              int _levelOffset = 0)
            {
              UnitNotificationInfo notificationInfo;
              UnitParameter unitParameter;
              if (!Singleton<UserData>.Instance.UnitNotification.TryGetValue(_unitId, out notificationInfo) || !Singleton<UserData>.Instance.UnitParameterDictionary.TryGetValue(_unitId, out unitParameter))
                return;
              UnitUtility.updateSkillNoticeInfo(unitParameter.UniqueData.UnionBurst, notificationInfo.SkillInfo, unitParameter, _calcLocalGold, _levelOffset);
              UnitUtility.updateSkillNoticeInfo(unitParameter.UniqueData.MainSkill, notificationInfo.SkillInfo, unitParameter, _calcLocalGold, _levelOffset);
              UnitUtility.updateSkillNoticeInfo(unitParameter.UniqueData.ExSkill, notificationInfo.SkillInfo, unitParameter, _calcLocalGold, _levelOffset);
            }

            public static UnitParameter CreateUnitParameterFromEnemyParameter(
              int _enemyParameterId)
            {
              UnitData _unitData = new UnitData();
              _unitData.SetId(_enemyParameterId);
              UnitParameter unitParameter = new UnitParameter(_unitData);
              MasterEnemyParameter.EnemyParameter fromAllKind = ManagerSingleton<MasterDataManager>.Instance.masterEnemyParameter.GetFromAllKind(_enemyParameterId);
              _unitData.SetUnitLevel((int) fromAllKind.level);
              StatusParam _baseParam = new StatusParam();
              _unitData.SetUnitRarity((int) fromAllKind.rarity);
              _unitData.SetPromotionLevel((ePromotionLevel) (int) fromAllKind.promotion_level);
              _baseParam.SetHp((long) (int) fromAllKind.hp);
              _baseParam.SetAtk((int) fromAllKind.atk);
              _baseParam.SetDef((int) fromAllKind.def);
              _baseParam.SetMagicStr((int) fromAllKind.magic_str);
              _baseParam.SetMagicDef((int) fromAllKind.magic_def);
              _baseParam.SetPhysicalCritical((int) fromAllKind.physical_critical);
              _baseParam.SetMagicCritical((int) fromAllKind.magic_critical);
              _baseParam.SetDodge((int) fromAllKind.dodge);
              _baseParam.SetLifeSteal((int) fromAllKind.life_steal);
              _baseParam.SetWaveHpRecovery((int) fromAllKind.wave_hp_recovery);
              _baseParam.SetWaveEnergyRecovery((int) fromAllKind.wave_energy_recovery);
              _baseParam.SetPhysicalPenetrate((int) fromAllKind.physical_penetrate);
              _baseParam.SetMagicPenetrate((int) fromAllKind.magic_penetrate);
              _baseParam.SetEnergyRecoveryRate((int) fromAllKind.energy_recovery_rate);
              _baseParam.SetHpRecoveryRate((int) fromAllKind.hp_recovery_rate);
              _baseParam.SetEnergyReduceRate((int) fromAllKind.energy_reduce_rate);
              _baseParam.SetAccuracy((int) fromAllKind.accuracy);
              _unitData.SetResistStatusId((int) fromAllKind.resist_status_id);
              _unitData.SetMainSkill(UnitUtility.createSkillLevelFromEnemyParameter(unitParameter.SkillData.MainSkillIds, UnitUtility.getMainSkillLevels(fromAllKind), unitParameter.SkillData.MainSkillEvolutionIds, UnitUtility.getMainSkillIsEvolved(fromAllKind)));
              _unitData.SetExSkill(UnitUtility.createSkillLevelFromEnemyParameter(unitParameter.SkillData.ExSkillIds, UnitUtility.getExSkillLevels(fromAllKind), (List<int>) null, (List<bool>) null));
              _unitData.SetUnionBurst(UnitUtility.createSkillLevelFromEnemyParameter(unitParameter.SkillData.UnionBurstIds, UnitUtility.getUnionBurstLevels(fromAllKind), unitParameter.SkillData.UnionBurstEvolutionIds, UnitUtility.getUnionBurstIsEvolved(fromAllKind)));
              StatusParam _equipParam = new StatusParam();
              UnitParam _unitParam = new UnitParam();
              _unitParam.SetBaseParam(_baseParam);
              _unitParam.SetEquipParam(_equipParam);
              _unitData.SetUnitParam(_unitParam);
              int _power = UnitUtility.calcOverallEnemy(_baseParam, _unitData);
              _unitData.SetPower(_power);
              return unitParameter;
            }

            public static UnitParameter CreateUnitParameterFromEnemyParameterForSekai(
              int _enemyParameterId)
            {
              UnitData _unitData = new UnitData();
              _unitData.SetId(_enemyParameterId);
              MasterSekaiEnemyParameter.SekaiEnemyParameter _enemyParameter = ManagerSingleton<MasterDataManager>.Instance.masterSekaiEnemyParameter.Get(_enemyParameterId);
              UnitParameter unitParameter = new UnitParameter(_unitData);
              _unitData.SetUnitLevel((int) _enemyParameter.level);
              StatusParam _baseParam = new StatusParam();
              _baseParam.SetHp(long.Parse((string) _enemyParameter.hp));
              _baseParam.SetAtk((int) _enemyParameter.atk);
              _baseParam.SetDef((int) _enemyParameter.def);
              _baseParam.SetMagicStr((int) _enemyParameter.magic_str);
              _baseParam.SetMagicDef((int) _enemyParameter.magic_def);
              _baseParam.SetPhysicalCritical((int) _enemyParameter.physical_critical);
              _baseParam.SetMagicCritical((int) _enemyParameter.magic_critical);
              _baseParam.SetDodge((int) _enemyParameter.dodge);
              _baseParam.SetLifeSteal((int) _enemyParameter.life_steal);
              _baseParam.SetWaveHpRecovery((int) _enemyParameter.wave_hp_recovery);
              _baseParam.SetWaveEnergyRecovery((int) _enemyParameter.wave_energy_recovery);
              _baseParam.SetPhysicalPenetrate((int) _enemyParameter.physical_penetrate);
              _baseParam.SetMagicPenetrate((int) _enemyParameter.magic_penetrate);
              _baseParam.SetEnergyRecoveryRate((int) _enemyParameter.energy_recovery_rate);
              _baseParam.SetHpRecoveryRate((int) _enemyParameter.hp_recovery_rate);
              _baseParam.SetEnergyReduceRate((int) _enemyParameter.energy_reduce_rate);
              _baseParam.SetAccuracy((int) _enemyParameter.accuracy);
              _unitData.SetResistStatusId((int) _enemyParameter.resist_status_id);
              _unitData.SetMainSkill(UnitUtility.createSkillLevelFromEnemyParameter(unitParameter.SkillData.MainSkillIds, UnitUtility.getMainSkillLevels(_enemyParameter), unitParameter.SkillData.MainSkillEvolutionIds, UnitUtility.getMainSkillIsEvolved((MasterEnemyParameter.EnemyParameter) _enemyParameter)));
              _unitData.SetExSkill(UnitUtility.createSkillLevelFromEnemyParameter(unitParameter.SkillData.ExSkillIds, UnitUtility.getExSkillLevels(_enemyParameter), (List<int>) null, (List<bool>) null));
              _unitData.SetUnionBurst(UnitUtility.createSkillLevelFromEnemyParameter(unitParameter.SkillData.UnionBurstIds, UnitUtility.getUnionBurstLevels(_enemyParameter), unitParameter.SkillData.UnionBurstEvolutionIds, UnitUtility.getUnionBurstIsEvolved((MasterEnemyParameter.EnemyParameter) _enemyParameter)));
              StatusParam _equipParam = new StatusParam();
              UnitParam _unitParam = new UnitParam();
              _unitParam.SetBaseParam(_baseParam);
              _unitParam.SetEquipParam(_equipParam);
              _unitData.SetUnitParam(_unitParam);
              int _power = UnitUtility.calcOverallEnemy(_baseParam, _unitData);
              _unitData.SetPower(_power);
              return unitParameter;
            }

            private static List<int> getMainSkillLevels(
              MasterEnemyParameter.EnemyParameter _enemyParameter) => new List<int>()
            {
              (int) _enemyParameter.main_skill_lv_1,
              (int) _enemyParameter.main_skill_lv_2,
              (int) _enemyParameter.main_skill_lv_3,
              (int) _enemyParameter.main_skill_lv_4,
              (int) _enemyParameter.main_skill_lv_5,
              (int) _enemyParameter.main_skill_lv_6,
              (int) _enemyParameter.main_skill_lv_7,
              (int) _enemyParameter.main_skill_lv_8,
              (int) _enemyParameter.main_skill_lv_9,
              (int) _enemyParameter.main_skill_lv_10
            };

            private static List<bool> getMainSkillIsEvolved(
              MasterEnemyParameter.EnemyParameter _enemyParameter) => new List<bool>()
            {
              (int) _enemyParameter.unique_equipment_flag_1 == 1
            };

            private static List<int> getMainSkillLevels(
              MasterSekaiEnemyParameter.SekaiEnemyParameter _enemyParameter) => new List<int>()
            {
              (int) _enemyParameter.main_skill_lv_1,
              (int) _enemyParameter.main_skill_lv_2,
              (int) _enemyParameter.main_skill_lv_3,
              (int) _enemyParameter.main_skill_lv_4,
              (int) _enemyParameter.main_skill_lv_5,
              (int) _enemyParameter.main_skill_lv_6,
              (int) _enemyParameter.main_skill_lv_7,
              (int) _enemyParameter.main_skill_lv_8,
              (int) _enemyParameter.main_skill_lv_9,
              (int) _enemyParameter.main_skill_lv_10
            };

            private static List<int> getExSkillLevels(
              MasterEnemyParameter.EnemyParameter _enemyParameter) => new List<int>()
            {
              (int) _enemyParameter.ex_skill_lv_1,
              (int) _enemyParameter.ex_skill_lv_2,
              (int) _enemyParameter.ex_skill_lv_3,
              (int) _enemyParameter.ex_skill_lv_4,
              (int) _enemyParameter.ex_skill_lv_5
            };

            private static List<int> getExSkillLevels(
              MasterSekaiEnemyParameter.SekaiEnemyParameter _enemyParameter) => new List<int>()
            {
              (int) _enemyParameter.ex_skill_lv_1,
              (int) _enemyParameter.ex_skill_lv_2,
              (int) _enemyParameter.ex_skill_lv_3,
              (int) _enemyParameter.ex_skill_lv_4,
              (int) _enemyParameter.ex_skill_lv_5
            };

            private static List<int> getUnionBurstLevels(
              MasterEnemyParameter.EnemyParameter _enemyParameter) => new List<int>()
            {
              (int) _enemyParameter.union_burst_level
            };

            private static List<int> getUnionBurstLevels(
              MasterSekaiEnemyParameter.SekaiEnemyParameter _enemyParameter) => new List<int>()
            {
              (int) _enemyParameter.union_burst_level
            };

            private static List<bool> getUnionBurstIsEvolved(
              MasterEnemyParameter.EnemyParameter _enemyParameter) => new List<bool>()
            {
              (int) _enemyParameter.rarity == 6
            };

            private static List<SkillLevelInfo> createSkillLevelFromEnemyParameter(
              List<int> _mainSkills,
              List<int> _levels,
              List<int> _plusSkills,
              List<bool> _plusSkillFlags)
            {
              List<SkillLevelInfo> skillLevelInfoList = new List<SkillLevelInfo>();
              int index = 0;
              for (int count = _mainSkills.Count; index < count; ++index)
              {
                int _skillId = _mainSkills[index];
                if (_skillId != 0)
                {
                  if (_plusSkillFlags != null && _plusSkillFlags.Count > index && _plusSkillFlags[index])
                    _skillId = _plusSkills[index];
                  SkillLevelInfo skillLevelInfo = new SkillLevelInfo();
                  skillLevelInfo.
        Id(_skillId);
                  skillLevelInfo.SetSkillLevel(_levels[index]);
                  skillLevelInfoList.Add(skillLevelInfo);
                }
              }
              return skillLevelInfoList;
            }

            public static void CalcParamAndSkill(
              UnitData _unitData,
              UnitUtility.SummonSkillData _summonSkillData = null,
              bool _isPowerLocal = false,
              bool _useBattleRarity = true)
            {
              if ((UnityEngine.Object) ManagerSingleton<MasterDataManager>.Instance == (UnityEngine.Object) null || Singleton<UserData>.Instance == null || Singleton<UserData>.Instance.UnitParameterDictionary == null)
                return;
              int id = (int) _unitData.Id;
              int _level = (int) _unitData.UnitLevel;
              int _rarity = (int) _unitData.UnitRarity;
              if (_useBattleRarity)
                _rarity = _unitData.GetCurrentRarity();
              int promotionLevel = (int) _unitData.PromotionLevel;
              if (UnitUtility.IsQuestMonsterUnit((int) _unitData.Id))
              {
                UnitUtility.GetUnitResourceId((int) _unitData.Id);
                MasterEnemyParameter.EnemyParameter fromAllKind = ManagerSingleton<MasterDataManager>.Instance.masterEnemyParameter.GetFromAllKind((int) _unitData.Id);
                StatusParam _baseParam = new StatusParam();
                _baseParam.SetHp((long) (int) fromAllKind.hp);
                _baseParam.SetAtk((int) fromAllKind.atk);
                _baseParam.SetDef((int) fromAllKind.def);
                _baseParam.SetMagicStr((int) fromAllKind.magic_str);
                _baseParam.SetMagicDef((int) fromAllKind.magic_def);
                _baseParam.SetPhysicalCritical((int) fromAllKind.physical_critical);
                _baseParam.SetMagicCritical((int) fromAllKind.magic_critical);
                _baseParam.SetDodge((int) fromAllKind.dodge);
                _baseParam.SetLifeSteal((int) fromAllKind.life_steal);
                _baseParam.SetWaveHpRecovery((int) fromAllKind.wave_hp_recovery);
                _baseParam.SetWaveEnergyRecovery((int) fromAllKind.wave_energy_recovery);
                _baseParam.SetPhysicalPenetrate((int) fromAllKind.physical_penetrate);
                _baseParam.SetMagicPenetrate((int) fromAllKind.magic_penetrate);
                _baseParam.SetEnergyRecoveryRate((int) fromAllKind.energy_recovery_rate);
                _baseParam.SetHpRecoveryRate((int) fromAllKind.hp_recovery_rate);
                _baseParam.SetEnergyReduceRate((int) fromAllKind.energy_reduce_rate);
                _baseParam.SetAccuracy((int) fromAllKind.accuracy);
                _unitData.SetResistStatusId((int) fromAllKind.resist_status_id);
                StatusParam _equipParam = new StatusParam();
                UnitParam _unitParam = new UnitParam();
                _unitParam.SetBaseParam(_baseParam);
                _unitParam.SetEquipParam(_equipParam);
                _unitData.SetUnitParam(_unitParam);
                if (!_isPowerLocal)
                  return;
                int _power = UnitUtility.calcOverallEnemy(_baseParam, _unitData);
                _unitData.SetPower(_power);
              }
              else
              {
                if (_summonSkillData != null)
                {
                  _level = _summonSkillData.SkillLevel;
                  _rarity = _summonSkillData.Rarity;
                  promotionLevel = _summonSkillData.PromotionLevel;
                }
                StatusParam _baseParam = new StatusParam();
                _baseParam.SetHp((long) UnitUtility.CalcUnitBaseParameter(eParamType.HP, id, _level, _rarity, promotionLevel));
                _baseParam.SetAtk(UnitUtility.CalcUnitBaseParameter(eParamType.ATK, id, _level, _rarity, promotionLevel));
                _baseParam.SetDef(UnitUtility.CalcUnitBaseParameter(eParamType.DEF, id, _level, _rarity, promotionLevel));
                _baseParam.SetMagicStr(UnitUtility.CalcUnitBaseParameter(eParamType.MAGIC_ATK, id, _level, _rarity, promotionLevel));
                _baseParam.SetMagicDef(UnitUtility.CalcUnitBaseParameter(eParamType.MAGIC_DEF, id, _level, _rarity, promotionLevel));
                _baseParam.SetPhysicalCritical(UnitUtility.CalcUnitBaseParameter(eParamType.PHYSICAL_CRITICAL, id, _level, _rarity, promotionLevel));
                _baseParam.SetMagicCritical(UnitUtility.CalcUnitBaseParameter(eParamType.MAGIC_CRITICAL, id, _level, _rarity, promotionLevel));
                _baseParam.SetDodge(UnitUtility.CalcUnitBaseParameter(eParamType.DODGE, id, _level, _rarity, promotionLevel));
                _baseParam.SetLifeSteal(UnitUtility.CalcUnitBaseParameter(eParamType.LIFE_STEAL, id, _level, _rarity, promotionLevel));
                _baseParam.SetWaveHpRecovery(UnitUtility.CalcUnitBaseParameter(eParamType.WAVE_HP_RECOVERY, id, _level, _rarity, promotionLevel));
                _baseParam.SetWaveEnergyRecovery(UnitUtility.CalcUnitBaseParameter(eParamType.WAVE_ENERGY_RECOVERY, id, _level, _rarity, promotionLevel));
                _baseParam.SetPhysicalPenetrate(UnitUtility.CalcUnitBaseParameter(eParamType.PHYSICAL_PENETRATE, id, _level, _rarity, promotionLevel));
                _baseParam.SetMagicPenetrate(UnitUtility.CalcUnitBaseParameter(eParamType.MAGIC_PENETRATE, id, _level, _rarity, promotionLevel));
                _baseParam.SetEnergyRecoveryRate(UnitUtility.CalcUnitBaseParameter(eParamType.ENERGY_RECOVERY_RATE, id, _level, _rarity, promotionLevel));
                _baseParam.SetHpRecoveryRate(UnitUtility.CalcUnitBaseParameter(eParamType.HP_RECOVERY_RATE, id, _level, _rarity, promotionLevel));
                _baseParam.SetEnergyReduceRate(UnitUtility.CalcUnitBaseParameter(eParamType.ENERGY_REDUCE_RATE, id, _level, _rarity, promotionLevel));
                _baseParam.SetAccuracy(UnitUtility.CalcUnitBaseParameter(eParamType.ACCURACY, id, _level, _rarity, promotionLevel));
                StatusParam _equipParam = new StatusParam();
                List<EquipSlot> equipSlot = _unitData.EquipSlot;
                List<EquipSlot> uniqueEquipSlot = _unitData.UniqueEquipSlot;
                _equipParam.SetHp((long) UnitUtility.CalcEquipParameter(eParamType.HP, equipSlot, uniqueEquipSlot));
                _equipParam.SetAtk(UnitUtility.CalcEquipParameter(eParamType.ATK, equipSlot, uniqueEquipSlot));
                _equipParam.SetDef(UnitUtility.CalcEquipParameter(eParamType.DEF, equipSlot, uniqueEquipSlot));
                _equipParam.SetMagicStr(UnitUtility.CalcEquipParameter(eParamType.MAGIC_ATK, equipSlot, uniqueEquipSlot));
                _equipParam.SetMagicDef(UnitUtility.CalcEquipParameter(eParamType.MAGIC_DEF, equipSlot, uniqueEquipSlot));
                _equipParam.SetPhysicalCritical(UnitUtility.CalcEquipParameter(eParamType.PHYSICAL_CRITICAL, equipSlot, uniqueEquipSlot));
                _equipParam.SetMagicCritical(UnitUtility.CalcEquipParameter(eParamType.MAGIC_CRITICAL, equipSlot, uniqueEquipSlot));
                _equipParam.SetDodge(UnitUtility.CalcEquipParameter(eParamType.DODGE, equipSlot, uniqueEquipSlot));
                _equipParam.SetLifeSteal(UnitUtility.CalcEquipParameter(eParamType.LIFE_STEAL, equipSlot, uniqueEquipSlot));
                _equipParam.SetWaveHpRecovery(UnitUtility.CalcEquipParameter(eParamType.WAVE_HP_RECOVERY, equipSlot, uniqueEquipSlot));
                _equipParam.SetWaveEnergyRecovery(UnitUtility.CalcEquipParameter(eParamType.WAVE_ENERGY_RECOVERY, equipSlot, uniqueEquipSlot));
                _equipParam.SetPhysicalPenetrate(UnitUtility.CalcEquipParameter(eParamType.PHYSICAL_PENETRATE, equipSlot, uniqueEquipSlot));
                _equipParam.SetMagicPenetrate(UnitUtility.CalcEquipParameter(eParamType.MAGIC_PENETRATE, equipSlot, uniqueEquipSlot));
                _equipParam.SetEnergyRecoveryRate(UnitUtility.CalcEquipParameter(eParamType.ENERGY_RECOVERY_RATE, equipSlot, uniqueEquipSlot));
                _equipParam.SetHpRecoveryRate(UnitUtility.CalcEquipParameter(eParamType.HP_RECOVERY_RATE, equipSlot, uniqueEquipSlot));
                _equipParam.SetEnergyReduceRate(UnitUtility.CalcEquipParameter(eParamType.ENERGY_REDUCE_RATE, equipSlot, uniqueEquipSlot));
                _equipParam.SetAccuracy(UnitUtility.CalcEquipParameter(eParamType.ACCURACY, equipSlot, uniqueEquipSlot));
                if (_unitData.ExSkill.Count > 0 && (int) _unitData.ExSkill[0].SkillLevel > 0)
                {
                  MasterUnitSkillData.UnitSkillData unitSkillData = ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData.Get((int) _unitData.Id);
                  int _skillId = _unitData.GetCurrentRarity() >= 5 ? unitSkillData.ExSkillEvolutionIds[0] : unitSkillData.ExSkillIds[0];
                  _unitData.ExSkill[0].SetSkillId(_skillId);
                }
                UnitParam _unitParam = new UnitParam();
                _unitParam.SetBaseParam(_baseParam);
                _unitParam.SetEquipParam(_equipParam);
                _unitData.SetUnitParam(_unitParam);
                if (!_isPowerLocal)
                  return;
                int _power = UnitUtility.CalcOverall(id, _level, _rarity, promotionLevel, _unitData);
                _unitData.SetPower(_power);
              }
            }

            public static UnitParameter CreateSummonUnitParameter(
              UnitParameter _owner,
              int _id,
              int _level,
              bool _considerEquipment,
              bool _mainSkill1Evolved)
            {
              UnitData _unitData = new UnitData();
              _unitData.SetId(_id);
              _unitData.SetUnitLevel(_level);
              UnitParameter _target = new UnitParameter(_unitData);
              UnitUtility.SummonSkillData _summonSkillData = UnitUtility.searchSummonSkill(_owner).FindAll((Predicate<UnitUtility.SummonSkillData>) (_it => _it.SummonTargetUnitId == _id))[0];
              bool _exSkillEvolved = _owner.UniqueData.GetCurrentRarity() >= 5;
              UnitUtility.calcurateSkillLevelInfo(_target, _summonSkillData.PromotionLevel, _level, _considerEquipment, _mainSkill1Evolved, _exSkillEvolved);
              UnitUtility.CalcParamAndSkill(_target.UniqueData, _summonSkillData, true);
              if (_considerEquipment)
              {
                _target.UniqueData.SetBonusParam(_owner.UniqueData.BonusParam);
                _target.UniqueData.UnitParam.SetEquipParam(_owner.UniqueData.UnitParam.EquipParam);
              }
              return _target;
            }

            public static void calcurateSkillLevelInfo(
              UnitParameter _target,
              int _promotionLevel,
              int _level,
              bool _considerExSkill,
              bool _mainSkill1Evolved,
              bool _exSkillEvolved)
            {
              List<SkillLevelInfo> skillLevel1 = UnitUtility.createSkillLevel(_target.SkillData.MainSkillIds, _promotionLevel, _level, true, _mainSkill1Evolved, _target.SkillData.MainSkillEvolutionIds);
              _target.UniqueData.SetMainSkill(skillLevel1);
              List<SkillLevelInfo> skillLevel2 = UnitUtility.createSkillLevel(_target.SkillData.UnionBurstIds, _promotionLevel, _level);
              _target.UniqueData.SetUnionBurst(skillLevel2);
              if (!_considerExSkill)
                return;
              int key = 301;
              MasterUnlockSkillData.UnlockSkillData unlockSkillData = (MasterUnlockSkillData.UnlockSkillData) null;
              if (!ManagerSingleton<MasterDataManager>.Instance.masterUnlockSkillData.dictionary.TryGetValue(key, out unlockSkillData) || (int) unlockSkillData.promotion_level > _promotionLevel)
                return;
              List<int> _mainSkills;
              if (!_exSkillEvolved)
              {
                _mainSkills = _target.SkillData.ExSkillIds;
              }
              else
              {
                _mainSkills = new List<int>();
                _mainSkills.Add((int) _target.SkillData.ex_skill_evolution_1);
              }
              List<SkillLevelInfo> skillLevel3 = UnitUtility.createSkillLevel(_mainSkills, _promotionLevel, _level);
              _target.UniqueData.SetExSkill(skillLevel3);
            }

            private static List<SkillLevelInfo> createSkillLevel(
              List<int> _mainSkills,
              int _promotionLevel,
              int _level,
              bool _isMainSkill = false,
              bool _mainSkill1Evolved = false,
              List<int> _mainSkillEvolvedIds = null)
            {
              List<SkillLevelInfo> skillLevelInfoList = new List<SkillLevelInfo>();
              int index = 0;
              for (int count = _mainSkills.Count; index < count; ++index)
              {
                int _skillId = _mainSkills[index];
                if (_isMainSkill & _mainSkill1Evolved && index == 0)
                  _skillId = _mainSkillEvolvedIds[index] == 0 ? _skillId : _mainSkillEvolvedIds[0];
                if (_skillId != 0)
                {
                  SkillLevelInfo skillLevelInfo = new SkillLevelInfo();
                  skillLevelInfo.SetSkillId(_skillId);
                  if (_isMainSkill)
                  {
                    int key = 201 + index;
                    MasterUnlockSkillData.UnlockSkillData unlockSkillData = (MasterUnlockSkillData.UnlockSkillData) null;
                    if (ManagerSingleton<MasterDataManager>.Instance.masterUnlockSkillData.dictionary.TryGetValue(key, out unlockSkillData))
                    {
                      int promotionLevel = (int) unlockSkillData.promotion_level;
                      skillLevelInfo.SetSkillLevel(_promotionLevel >= promotionLevel ? _level : 0);
                    }
                  }
                  else
                    skillLevelInfo.SetSkillLevel(_level);
                  skillLevelInfoList.Add(skillLevelInfo);
                }
              }
              return skillLevelInfoList;
            }

            private static List<UnitUtility.SummonSkillData> searchSummonSkill(
              UnitParameter _unitParameter)
            {
              UnitData unitData = _unitParameter.UniqueData;
              MasterDataManager masterDatamanager = ManagerSingleton<MasterDataManager>.Instance;
              List<UnitUtility.SummonSkillData> summonSkillList = new List<UnitUtility.SummonSkillData>();
              System.Action<List<SkillLevelInfo>> action = (System.Action<List<SkillLevelInfo>>) (_skillLevelList =>
              {
                for (int index1 = 0; index1 < _skillLevelList.Count; ++index1)
                {
                  SkillLevelInfo skillLevel = _skillLevelList[index1];
                  if ((int) skillLevel.SkillId != 0)
                  {
                    SkillData skillData = SkillUtility.GetSkillData((int) skillLevel.SkillId, (int) skillLevel.SkillLevel);
                    for (int index2 = 0; index2 < skillData.ActionInfoList.Count; ++index2)
                    {
                      ActionData actionInfo = skillData.ActionInfoList[index2];
                      MasterSkillAction.SkillAction skillAction = masterDatamanager.masterSkillAction.Get(actionInfo.ActionId);
                      if ((byte) skillAction.action_type == (byte) 15)
                      {
                        int _rarity = (int) actionInfo.ActionValue.Value4 / 100;
                        if (_rarity == 0)
                          _rarity = unitData.GetCurrentRarity();
                        int _promotionLevel = (int) actionInfo.ActionValue.Value4 % 100;
                        if (_promotionLevel == 0)
                          _promotionLevel = (int) unitData.PromotionLevel;
                        int actionDetail2 = skillAction.action_detail_2;
                        summonSkillList.Add(new UnitUtility.SummonSkillData((int) skillLevel.SkillId, (int) skillLevel.SkillLevel, _rarity, _promotionLevel, (int) actionDetail2));
                      }
                    }
                  }
                }
              });
              action(unitData.UnionBurst);
              action(unitData.MainSkill);
              action(unitData.ExSkill);
              List<SkillLevelInfo> skillLevelInfoList = new List<SkillLevelInfo>();
              for (int index = 0; index < _unitParameter.SkillData.SpSkillIds.Count; ++index)
              {
                SkillLevelInfo skillLevelInfo = new SkillLevelInfo();
                skillLevelInfo.SetSkillId(_unitParameter.SkillData.SpSkillIds[index]);
                skillLevelInfoList.Add(skillLevelInfo);
              }
              action(skillLevelInfoList);
              action(unitData.FreeSkill);
              return summonSkillList;
            }

            public static void SortPartyByPosition(List<UnitDataForView> _partyList)
            {
              if (_partyList == null)
                return;
              MasterUnitData masterUnitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData;
              _partyList.Sort((Comparison<UnitDataForView>) ((_a, _b) => UnitUtility.compareUnitOrder(masterUnitData.Get((int) _a.Id), masterUnitData.Get((int) _b.Id))));
            }

            public static void SortPartyByPosition(List<ChangeRarityUnit> _partyList)
            {
              if (_partyList == null)
                return;
              MasterUnitData masterUnitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData;
              _partyList.Sort((Comparison<ChangeRarityUnit>) ((_a, _b) => UnitUtility.compareUnitOrder(masterUnitData.Get(_a.unit_id), masterUnitData.Get(_b.unit_id))));
            }

            public static void SortPartyByPosition(List<UnitData> _partyList)
            {
              if (_partyList == null)
                return;
              MasterUnitData masterUnitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData;
              _partyList.Sort((Comparison<UnitData>) ((_a, _b) => UnitUtility.compareUnitOrder(masterUnitData.Get((int) _a.Id), masterUnitData.Get((int) _b.Id))));
            }

            public static void SortPartyByPosition(List<ReplayUnitDataForView> _partyList)
            {
              if (_partyList == null)
                return;
              MasterUnitData masterUnitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData;
              _partyList.Sort((Comparison<ReplayUnitDataForView>) ((_a, _b) => UnitUtility.compareUnitOrder(masterUnitData.Get((int) _a.Id), masterUnitData.Get((int) _b.Id))));
            }

            public static void SortPartyByPosition(List<int> _partyList)
            {
              if (_partyList == null)
                return;
              MasterUnitData masterUnitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData;
              _partyList.Sort((Comparison<int>) ((_a, _b) => UnitUtility.compareUnitOrder(masterUnitData.Get(_a), masterUnitData.Get(_b))));
            }

            public static void SortPartyByPosition(List<UnitParameter> _partyList) => _partyList?.Sort((Comparison<UnitParameter>) ((_a, _b) => UnitUtility.compareUnitOrder(_a.MasterData, _b.MasterData)));

            public static void SortPartyByPosition(List<DungeonUnit> _partyList)
            {
              if (_partyList == null)
                return;
              MasterUnitData masterUnitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData;
              _partyList.Sort((Comparison<DungeonUnit>) ((_a, _b) => UnitUtility.compareUnitOrder(masterUnitData.Get((int) _a.UnitId), masterUnitData.Get((int) _b.UnitId))));
            }

            public static void SortPartyByPosition(List<TowerUnit> _partyList)
            {
              if (_partyList == null)
                return;
              MasterUnitData masterUnitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData;
              _partyList.Sort((Comparison<TowerUnit>) ((_a, _b) => UnitUtility.compareUnitOrder(masterUnitData.Get((int) _a.UnitId), masterUnitData.Get((int) _b.UnitId))));
            }

            public static void SortPartyByPosition(List<ClanChatUnitData> _partyList, bool _isEnemy)
            {
              if (_partyList == null)
                return;
              if (_isEnemy)
              {
                MasterEnemyParameter masterEnemyParameter = ManagerSingleton<MasterDataManager>.Instance.masterEnemyParameter;
                MasterUnitEnemyData masterUnitEnemyData = ManagerSingleton<MasterDataManager>.Instance.masterUnitEnemyData;
                _partyList.Sort((Comparison<ClanChatUnitData>) ((_a, _b) => UnitUtility.compareUnitEnemyOrder(masterUnitEnemyData.Get((int) masterEnemyParameter.GetFromAllKind(_a.id).unit_id), masterUnitEnemyData.Get((int) masterEnemyParameter.GetFromAllKind(_b.id).unit_id))));
              }
              else
              {
                MasterUnitData masterUnitData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData;
                _partyList.Sort((Comparison<ClanChatUnitData>) ((_a, _b) => UnitUtility.compareUnitOrder(masterUnitData.Get(_a.id), masterUnitData.Get(_b.id))));
              }
            }

            public static ClanChatUnitData[] SortPartyByPosition(
              ClanChatUnitData[] _partyArray,
              bool _isEnemy)
            {
              List<ClanChatUnitData> list = ((IEnumerable<ClanChatUnitData>) _partyArray).ToList<ClanChatUnitData>();
              UnitUtility.SortPartyByPosition(list, _isEnemy);
              return list.ToArray();
            }

            public static ClanChatUnitData[] SortPartyEnemyTowerOrder(
              ClanChatUnitData[] _partyList,
              bool _isEx,
              int _questId)
            {
              if (_partyList == null)
                return (ClanChatUnitData[]) null;
              MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
              MasterUnitEnemyData masterUnitEnemyData = instance.masterUnitEnemyData;
              int wave_group_id = (int) (_isEx ? instance.masterTowerExQuestData.Get(_questId).wave_group_id : instance.masterTowerQuestData.Get(_questId).wave_group_id);
              MasterTowerWaveGroupData.TowerWaveGroupData towerWaveGroupData = instance.masterTowerWaveGroupData.Get(wave_group_id);
              List<int> enemyIdList = new List<int>();
              if ((int) towerWaveGroupData.enemy_id_1 > 0)
                enemyIdList.Add((int) towerWaveGroupData.enemy_id_1);
              if ((int) towerWaveGroupData.enemy_id_2 > 0)
                enemyIdList.Add((int) towerWaveGroupData.enemy_id_2);
              if ((int) towerWaveGroupData.enemy_id_3 > 0)
                enemyIdList.Add((int) towerWaveGroupData.enemy_id_3);
              if ((int) towerWaveGroupData.enemy_id_4 > 0)
                enemyIdList.Add((int) towerWaveGroupData.enemy_id_4);
              if ((int) towerWaveGroupData.enemy_id_5 > 0)
                enemyIdList.Add((int) towerWaveGroupData.enemy_id_5);
              List<ClanChatUnitData> list = ((IEnumerable<ClanChatUnitData>) _partyList).ToList<ClanChatUnitData>();
              List<ClanChatUnitData> clanChatUnitDataList = new List<ClanChatUnitData>();
              for (int i = 0; i < enemyIdList.Count; i++)
              {
                int index = list.FindIndex((Predicate<ClanChatUnitData>) (dt => dt.id == enemyIdList[i]));
                if (index >= 0)
                {
                  clanChatUnitDataList.Add(list[index]);
                  list.RemoveAt(index);
                }
              }
              return clanChatUnitDataList.ToArray();
            }

            public static int CalcTotalPower(List<UnitDataForView> _unitParamList)
            {
              int num = 0;
              for (int index = 0; index < _unitParamList.Count; ++index)
                num += (int) _unitParamList[index].Power;
              return num;
            }

            private static int calcOverallEnemy(StatusParam _baseParam, UnitData _unitData)
            {
              MasterUnitStatusCoefficient masterUnitStatusValue = ManagerSingleton<MasterDataManager>.Instance.masterUnitStatusCoefficient;
              int skillLevelSum = 0;
              _unitData.UnionBurst.ForEach((System.Action<SkillLevelInfo>) (_it => skillLevelSum += (int) _it.SkillLevel));
              _unitData.MainSkill.ForEach((System.Action<SkillLevelInfo>) (_it => skillLevelSum += (int) _it.SkillLevel));
              _unitData.ExSkill.ForEach((System.Action<SkillLevelInfo>) (_it =>
              {
                skillLevelSum += (int) _it.SkillLevel;
                if ((int) _it.SkillLevel <= 0 || (int) _unitData.UnitRarity < 5)
                  return;
                skillLevelSum += masterUnitStatusValue.GetExSkillEvolutionCoefficient();
              }));
              _unitData.FreeSkill.ForEach((System.Action<SkillLevelInfo>) (_it => skillLevelSum += (int) _it.SkillLevel));
              return UnitUtility.round(Mathf.Pow((float) (0.0 + (double) (long) _baseParam.Hp * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.HP) + (double) (int) _baseParam.Atk * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.ATK) + (double) (int) _baseParam.Def * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.DEF) + (double) (int) _baseParam.MagicStr * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.MAGIC_ATK) + (double) (int) _baseParam.MagicDef * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.MAGIC_DEF) + (double) (int) _baseParam.PhysicalCritical * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.PHYSICAL_CRITICAL) + (double) (int) _baseParam.MagicCritical * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.MAGIC_CRITICAL) + (double) (int) _baseParam.Dodge * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.DODGE) + (double) (int) _baseParam.Accuracy * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.ACCURACY) + (double) (int) _baseParam.LifeSteal * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.LIFE_STEAL) + (double) (int) _baseParam.WaveHpRecovery * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.WAVE_HP_RECOVERY) + (double) (int) _baseParam.WaveEnergyRecovery * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.WAVE_ENERGY_RECOVERY) + (double) (int) _baseParam.PhysicalPenetrate * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.PHYSICAL_PENETRATE) + (double) (int) _baseParam.MagicPenetrate * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.MAGIC_PENETRATE) + (double) (int) _baseParam.EnergyRecoveryRate * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.ENERGY_RECOVERY_RATE) + (double) (int) _baseParam.HpRecoveryRate * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.HP) + (double) (int) _baseParam.EnergyReduceRate * (double) masterUnitStatusValue.GetParameterCoefficient(eParamType.ENERGY_REDUCE_RATE) + (double) skillLevelSum * (double) masterUnitStatusValue.GetSkillLevelCoefficient()), masterUnitStatusValue.GetOverallCoefficient()));
            }

            public static void OpenDialogCharacterDetail(
              int _id,
              bool _trialStoryView = false,
              System.Action _onBeforePlayVoice = null)
            {
              MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
              MasterUnitPromotion.UnitPromotion maxUnitPromotion = instance.masterUnitPromotion.GetMaxUnitPromotion(_id);
              int levelMax = instance.masterExperienceUnit.LevelMax;
              int promotionLevel = (int) maxUnitPromotion.promotion_level;
              MasterUnitData.UnitData unitData = instance.masterUnitData.Get(_id);
              UnitData _unitData = new UnitData();
              _unitData.SetId(_id);
              _unitData.SetUnitLevel(levelMax);
              _unitData.SetUnitRarity((int) unitData.Rarity);
              _unitData.SetPromotionLevel((ePromotionLevel) promotionLevel);
              ManagerSingleton<DialogManager>.Instance.OpenDialogCharacterDetail(_unitData, _trialStoryView, _onBeforePlayVoice);
            }

            private static NIBGCIIGOMC playCharaDetailVoice(
              int _unitId,
              int _rarity,
              ref int _playIndex)
            {
              int skinId = UnitUtility.GetSkinId(_unitId, _rarity);
              SoundManager.eVoiceType charaDetailVoiceType = UnitUtility.charaDetailVoiceTypes[_playIndex];
              _playIndex = _playIndex + 1 < UnitUtility.charaDetailVoiceTypes.Length ? _playIndex + 1 : 0;
              SoundManager instance = ManagerSingleton<SoundManager>.Instance;
              instance.StopVoice();
              return instance.PlayVoice(skinId, charaDetailVoiceType, 1);
            }

            public static void PlayCharaDetailVoiceWithCheckDataExist(
              int _unitId,
              int _rarity,
              int _playIndex,
              System.Action<NIBGCIIGOMC, int> _audioPlayback)
            {
              int playIndex = _playIndex;
              if (QualityManager.GetVoiceDataAvailable())
              {
                if (DownloadUtil.isCharaCmnVoiceDataDownloaded(_unitId))
                {
                  NIBGCIIGOMC JEOCPILJNAD = UnitUtility.playCharaDetailVoice(_unitId, _rarity, ref playIndex);
                  _audioPlayback.Call<NIBGCIIGOMC, int>(JEOCPILJNAD, playIndex);
                }
                else
                  ManagerSingleton<DialogManager>.Instance.OpenConfirm(eTextId.VOICE_DOWNLOAD.Name(), eTextId.VOICEDATA_DOWNLOAD.Name(), true, (System.Action) (() => ManagerSingleton<ViewManager>.Instance.StartCoroutine(DownloadUtil.DownloadCharaCmnVoice(new int[1]
                  {
                    _unitId
                  }, (System.Action<bool>) (_downloadOccurred => _audioPlayback.Call<NIBGCIIGOMC, int>(UnitUtility.playCharaDetailVoice(_unitId, _rarity, ref playIndex), playIndex))))));
              }
              else
              {
                ManagerSingleton<DialogManager>.Instance.OpenConfirm(ParamDialogCommonText.eDialogSize.Small, eTextId.SETTING_CONFIRM.Name(), eTextId.VOICE_DOWNLOAD_SETTING_IS_OFF.Name(), eTextId.CLOSE.Name(), eTextId.TO_SETTING.Name(), (System.Action) null, (System.Action) (() => ManagerSingleton<DialogManager>.Instance.OpenSystemSettingDialog()));
                _audioPlayback.Call<NIBGCIIGOMC, int>(NIBGCIIGOMC.Error(IEGILGIGDDK.Voice), _playIndex);
              }
            }

            public static bool IsBirthDayVoiceEnable(int _unitId, int _rarity, bool _isStrict = false)
            {
              if (!_isStrict)
              {
                List<MasterUnitComments.UnitComments> orderByVoiceIdAsc = ManagerSingleton<MasterDataManager>.Instance.masterUnitComments.GetListWithUnitIdAndUseTypeOrderByVoiceIdAsc(_unitId, (byte) 2);
                return orderByVoiceIdAsc != null && orderByVoiceIdAsc.Count > 0;
              }
              int unit_id = _unitId;
              if (_rarity >= 3)
                unit_id = UnitUtility.GetSkinId(_unitId, _rarity);
              List<MasterUnitComments.UnitComments> orderByVoiceIdAsc1 = ManagerSingleton<MasterDataManager>.Instance.masterUnitComments.GetListWithUnitIdAndUseTypeOrderByVoiceIdAsc(unit_id, (byte) 2);
              return orderByVoiceIdAsc1 != null && orderByVoiceIdAsc1.Count > 0;
            }

            private static string playCharaBirthDayVoice(int _unitId, int _rarity, int _playIndex)
            {
              SoundManager.VoiceCueParam voiceCueParam = SoundManager.GetVoiceCueParam(SoundManager.eVoiceType.BIRTH_DAY);
              int skinId = UnitUtility.GetSkinId(_unitId, _rarity);
              string _cueSheet = string.Format(voiceCueParam.CueSheetNameFormat, (object) skinId);
              List<MasterUnitComments.UnitComments> orderByVoiceIdAsc = ManagerSingleton<MasterDataManager>.Instance.masterUnitComments.GetListWithUnitIdAndUseTypeOrderByVoiceIdAsc(_unitId, (byte) 2);
              if (orderByVoiceIdAsc == null || orderByVoiceIdAsc.Count == 0)
                return string.Empty;
              int voiceId = (int) (byte) orderByVoiceIdAsc[_playIndex % orderByVoiceIdAsc.Count].voice_id;
              string _cueName = string.Format(voiceCueParam.CueNameFormat, (object) skinId, (object) voiceId);
              SoundManager instance = ManagerSingleton<SoundManager>.Instance;
              instance.StopVoice();
              instance.PlayVoice(_cueSheet, _cueName);
              return _cueSheet;
            }

            private static string playCharaBirthDayVoiceWithRarityCheck(
              int _unitId,
              int _rarity,
              int _playIndex)
            {
              if (UnitUtility.IsBirthDayVoiceEnable(_unitId, _rarity, true))
                return UnitUtility.playCharaBirthDayVoice(_unitId, _rarity, _playIndex);
              switch (_rarity)
              {
                case 1:
                case 2:
                  return string.Empty;
                case 3:
                case 4:
                case 5:
                  return UnitUtility.playCharaBirthDayVoiceWithRarityCheck(_unitId, 1, _playIndex);
                case 6:
                  return UnitUtility.playCharaBirthDayVoiceWithRarityCheck(_unitId, 3, _playIndex);
                default:
                  return string.Empty;
              }
            }

            public static string PlayCharaBirthDayVoiceWithCheckDataExist(
              int _unitId,
              int _rarity,
              int _playIndex)
            {
              if (QualityManager.GetVoiceDataAvailable())
              {
                if (UnitUtility.IsBirthDayVoiceEnable(_unitId, _rarity))
                  return UnitUtility.playCharaBirthDayVoiceWithRarityCheck(_unitId, _rarity, _playIndex);
              }
              else
                ManagerSingleton<DialogManager>.Instance.OpenConfirm(ParamDialogCommonText.eDialogSize.Small, eTextId.SETTING_CONFIRM.Name(), eTextId.VOICE_DOWNLOAD_SETTING_IS_OFF.Name(), eTextId.CLOSE.Name(), eTextId.TO_SETTING.Name(), (System.Action) null, (System.Action) (() => ManagerSingleton<DialogManager>.Instance.OpenSystemSettingDialog()));
              return string.Empty;
            }

            public static void GotoCharacterAlbum(int _unitId)
            {
              Singleton<EHPLBCOOOPK>.Instance.DGLHJHFMGJD = _unitId;
              ManagerSingleton<ViewManager>.Instance.ChangeView(eViewId.CHARACTER_ALBUM);
            }

            public static void GotoMemoryAlbum(int _unitId)
            {
              EHPLBCOOOPK instance1 = Singleton<EHPLBCOOOPK>.Instance;
              MasterDataManager instance2 = ManagerSingleton<MasterDataManager>.Instance;
              instance1.BJBGGDPJAHB = ViewMemoryList.eMemoryTabType.CHARACTER_STORY;
              instance1.GBMPJLBLDEA = UnitUtility.GetCharaId(_unitId);
              string strA = ((string) instance2.masterUnitData.Get(_unitId).Kana).Substring(0, 1);
              for (int index = 0; index < UnitDefine.PEOPLEBOOK_SET_FILTER_TYPE_LIST.Length; ++index)
              {
                string strB1 = UnitDefine.PEOPLEBOOK_SET_FILTER_TYPE_LIST[index, 0].Name();
                string strB2 = UnitDefine.PEOPLEBOOK_SET_FILTER_TYPE_LIST[index, 1].Name();
                int num1 = string.Compare(strA, strB1, StringComparison.CurrentCulture);
                int num2 = string.Compare(strA, strB2, StringComparison.CurrentCulture);
                if (num1 >= 0 && num2 < 0)
                {
                  instance1.NNFPFKBEKHJ = UnitDefine.CHARASTORY_SORT_TYPE_LIST[index];
                  break;
                }
              }
              ManagerSingleton<ViewManager>.Instance.ChangeView(eViewId.MENU_ALBUM_STILL_AND_MOVIE);
            }

            private static int compareUnitOrder(
              MasterUnitData.UnitData _aUnitData,
              MasterUnitData.UnitData _bUnitData)
            {
              int searchAreaWidth1 = (int) _aUnitData.SearchAreaWidth;
              int searchAreaWidth2 = (int) _bUnitData.SearchAreaWidth;
              return searchAreaWidth1 == searchAreaWidth2 ? (int) _aUnitData.UnitId - (int) _bUnitData.UnitId : searchAreaWidth1 - searchAreaWidth2;
            }

            private static int compareUnitEnemyOrder(
              MasterUnitEnemyData.UnitEnemyData _aUnitData,
              MasterUnitEnemyData.UnitEnemyData _bUnitData)
            {
              int searchAreaWidth1 = (int) _aUnitData.search_area_width;
              int searchAreaWidth2 = (int) _bUnitData.search_area_width;
              return searchAreaWidth1 == searchAreaWidth2 ? (int) _aUnitData.unit_id - (int) _bUnitData.unit_id : searchAreaWidth1 - searchAreaWidth2;
            }

            public static Material LoadUnitProfileThumbnailMaterial(
              int _unitId,
              int _rarity,
              eResourceId _mask = eResourceId.UNIT_PROFILE_ALPHA) => ManagerSingleton<ResourceManager>.Instance.LoadThumbUnitProfileImmediately(UnitUtility.GetSkinId(_unitId, _rarity), _mask);

            public static Texture LoadUnitProfileThumbnailTexture(int _unitId, int _rarity) => ManagerSingleton<ResourceManager>.Instance.LoadResourceImmediately(eResourceId.UNIT_PROFILE_TEXTURE, (long) UnitUtility.GetSkinId(_unitId, _rarity)) as Texture;

            private static int GetNextSkinId(int _rarity, int _practical)
            {
              UnitDefine.eUnitRarity eUnitRarity = (UnitDefine.eUnitRarity) _rarity;
              switch (_rarity)
              {
                case 1:
                case 2:
                  eUnitRarity = UnitDefine.eUnitRarity.RARITY_3;
                  break;
                case 3:
                case 4:
                case 5:
                  eUnitRarity = _practical < 6 ? UnitDefine.eUnitRarity.RARITY_1 : UnitDefine.eUnitRarity.RARITY_6;
                  break;
                case 6:
                  eUnitRarity = UnitDefine.eUnitRarity.RARITY_1;
                  break;
              }
              return (int) eUnitRarity;
            }

            public static int SwitchUnitProfileThumbnail(
              UITexture _thumbnail,
              int _unitId,
              int _rarity,
              int _practicalRarity,
              eResourceId _mask = eResourceId.UNIT_PROFILE_ALPHA)
            {
              if ((UnityEngine.Object) _thumbnail == (UnityEngine.Object) null)
                return _rarity;
              int nextSkinId = UnitUtility.GetNextSkinId(_rarity, _practicalRarity);
              _thumbnail.material = UnitUtility.LoadUnitProfileThumbnailMaterial(_unitId, nextSkinId, _mask);
              return nextSkinId;
            }
            */
        public static int GetSkinId(int _unitId, int _rarity)
        {
            if (_unitId == 418101)
                return 418131;
            if (_unitId >= 403101 && _unitId <= 403109)
                return _unitId + 30;
            if (!IsUnitTypePersonOrSummonPerson(_unitId) || IsUnitTypeSummonPersonOnly(_unitId) && _rarity == 0)
                return _unitId;
            int num = _unitId;
            switch (_rarity)
            {
                case 0:
                case 1:
                case 2:
                    num = _unitId + 10;
                    break;
                case 3:
                case 4:
                case 5:
                    num = _unitId + 30;
                    break;
                case 6:
                    num = _unitId + 60;
                    break;
            }
            return num;
        }
    /*
    public static int GetSkinId(
      eSpineType _spineType,
      int _unitId,
      int _rarity,
      UnitDefine.eSkinType _skin = UnitDefine.eSkinType.Sd,
      bool _isOther = false)
    {
      if (_rarity == -2)
        _rarity = UnitUtility.GetSetSkinNo(Singleton<UserData>.Instance.UnitParameterDictionary[_unitId].UniqueData, _skin, _isOther);
      if (ResourceDefineSpine.IsSkinIdSpineType(_spineType))
        return UnitUtility.GetSkinId(_unitId, _rarity);
      return _spineType == eSpineType.SD_SHADOW && _rarity == 6 ? UnitUtility.GetSkinIdFromRarity(_rarity) : _unitId;
    }

    public static int GetSkinId(int _unitId, UnitDefine.eSkinType _skinType)
    {
      int setSkinNo = UnitUtility.GetSetSkinNo(_unitId, _skinType);
      return UnitUtility.GetSkinId(_unitId, setSkinNo);
    }*/
    
    public static int SkinIdToUnitId(int _skinId) => _skinId / 100 * 100 + SkinIdToClassId(_skinId);

    public static int SkinIdToRarity(int _skinId) => _skinId / 10 % 10;

    public static int SkinIdToSkinNo(int _skinId) => _skinId % 100 / 10;

    public static bool IsSkinId(int _id) => (uint) SkinIdToRarity(_id) > 0U;

    public static int SkinIdToClassId(int _skinId) => _skinId % 10;
/*
    public static Texture LoadUnitIconTexture(int _unitId, int _rarity = 1, bool _isShadow = false)
    {
      if (_unitId == 0)
        return ManagerSingleton<ResourceManager>.Instance.LoadIconUnitImmediately(_unitId);
      int _skinId = _unitId;
      if (UnitUtility.IsPersonUnit(_unitId))
        _skinId = UnitUtility.GetSkinId(_unitId, _rarity);
      else
        _isShadow = false;
      return !_isShadow ? ManagerSingleton<ResourceManager>.Instance.LoadIconUnitImmediately(_skinId) : ManagerSingleton<ResourceManager>.Instance.LoadIconUnitShadowImmediately(_skinId);
    }

    public static Texture LoadUnitIconTextureWithEnemyColor(int _unitId, int _enemyColorId) => _unitId == 0 ? ManagerSingleton<ResourceManager>.Instance.LoadIconUnitImmediately(_unitId) : ManagerSingleton<ResourceManager>.Instance.LoadIconUnitImmediatelyWithEnemyColor(_unitId, _enemyColorId);

    public static int GetSkinIdFromRarity(int _rarity)
    {
      int num = 1;
      switch (_rarity)
      {
        case 0:
        case 1:
        case 2:
          num = 1;
          break;
        case 3:
        case 4:
        case 5:
          num = 3;
          break;
        case 6:
          num = 6;
          break;
      }
      return num;
    }

    public static int GetSetSkinData(SkinData _skin, UnitDefine.eSkinType _type)
    {
      if (_skin == null)
        return 0;
      switch (_type)
      {
        case UnitDefine.eSkinType.Icon:
          return (int) _skin.IconSkinId;
        case UnitDefine.eSkinType.Sd:
          return (int) _skin.SdSkinId;
        case UnitDefine.eSkinType.Still:
          return (int) _skin.StillSkinId;
        case UnitDefine.eSkinType.Motion:
          return (int) _skin.MotionId;
        default:
          return 0;
      }
    }

    public static bool IsChangeSkinFromUnitId(int _unitId) => UnitUtility.IsChangeSkin((int) Singleton<UserData>.Instance.UnitParameterDictionary[_unitId].UniqueData.UnitRarity);

    public static bool IsChangeSkin(int _rarity, bool _otherUser = false) => (!_otherUser || ManagerSingleton<SaveDataManager>.Instance.ApplySkinSettingsFromOtherUsers) && _rarity >= 3;

    public static bool IsChangeMotion(int _unitId, bool _otherUser = false) => (!_otherUser || ManagerSingleton<SaveDataManager>.Instance.ApplySkinSettingsFromOtherUsers) && ManagerSingleton<MasterDataManager>.Instance.masterUnitMotionList[_unitId];

    public static int GetSetSkinNo(
      int _rarity,
      SkinData _skinData,
      UnitDefine.eSkinType _type,
      bool _otherUser = false,
      UnitCtrl _unit = null)
    {
      int setSkinNoImpl = UnitUtility.getSetSkinNoImpl(_rarity, _skinData, _type, _otherUser);
      if ((UnityEngine.Object) _unit != (UnityEngine.Object) null)
        _unit.SkinRarity = setSkinNoImpl;
      return setSkinNoImpl;
    }

    private static int getSetSkinNoImpl(
      int _rarity,
      SkinData _skinData,
      UnitDefine.eSkinType _type,
      bool _otherUser = false)
    {
      if (_type == UnitDefine.eSkinType.Motion)
        return _skinData == null ? 0 : UnitUtility.GetSetSkinData(_skinData, UnitDefine.eSkinType.Motion);
      int skinIdFromRarity = UnitUtility.GetSkinIdFromRarity(_rarity);
      if (!UnitUtility.IsChangeSkin(_rarity, _otherUser))
        return skinIdFromRarity;
      int setSkinData = UnitUtility.GetSetSkinData(_skinData, _type);
      return setSkinData == 0 ? skinIdFromRarity : setSkinData;
    }

    public static int GetSetSkinNo(
      UnitData _unitData,
      UnitDefine.eSkinType _type,
      bool _otherUser = false,
      UnitCtrl _unit = null) => UnitUtility.GetSetSkinNo((int) _unitData.UnitRarity, _unitData.SkinData, _type, _otherUser, _unit);

    public static int GetSetSkinNo(
      UnitDataForView _unitData,
      UnitDefine.eSkinType _type,
      bool _otherUser = false) => UnitUtility.GetSetSkinNo((int) _unitData.UnitRarity, _unitData.SkinData, _type, _otherUser);

    public static int GetSetSkinNo(DungeonUnit _unitData, UnitDefine.eSkinType _type) => UnitUtility.GetSetSkinNo((int) _unitData.Rarity, _unitData.SkinData, _type, DungeonUtility.IsOtherUser(_unitData));

    public static int GetSetSkinNo(TowerUnit _unitData, UnitDefine.eSkinType _type) => UnitUtility.GetSetSkinNo((int) _unitData.Rarity, _unitData.SkinData, _type, TowerUtility.IsOtherUser(_unitData));

    public static int GetSetSkinNo(
      UnitDataForClanMember _unitData,
      UnitDefine.eSkinType _type,
      bool _otherUser = false) => UnitUtility.GetSetSkinNo((int) _unitData.Evolution, _unitData.SkinData, _type, _otherUser);

    public static int GetSetSkinNo(int _unitId, UnitDefine.eSkinType _type) => UnitUtility.GetSetSkinNo(Singleton<UserData>.Instance.UnitParameterDictionary[_unitId].UniqueData, _type);

    public static void UpdateSkinData(List<SkinDataForRequest> _param)
    {
      if (_param == null)
        return;
      Dictionary<int, UnitParameter> parameterDictionary = Singleton<UserData>.Instance.UnitParameterDictionary;
      for (int index = 0; index < _param.Count; ++index)
      {
        SkinDataForRequest skinDataForRequest = _param[index];
        if (parameterDictionary.ContainsKey(skinDataForRequest.unit_id))
        {
          SkinData _skindata = new SkinData();
          _skindata.SetIconSkinId(skinDataForRequest.icon_skin_id);
          _skindata.SetStillSkinId(skinDataForRequest.still_skin_id);
          _skindata.SetSdSkinId(skinDataForRequest.sd_skin_id);
          _skindata.SetMotionId(skinDataForRequest.motion_id);
          parameterDictionary[skinDataForRequest.unit_id].UniqueData.UpdateSkinData(_skindata);
        }
      }
    }

    public static void UpdatePossibleChangeSkinList()
    {
      EHPLBCOOOPK tempData = Singleton<EHPLBCOOOPK>.Instance;
      if (tempData.IJEKLMEFGNE == null)
        tempData.IJEKLMEFGNE = new List<int>();
      tempData.IJEKLMEFGNE.Clear();
      tempData.MNHMADGDIHD.ForEach((System.Action<int>) (_unit =>
      {
        if (!UnitUtility.IsChangeSkinFromUnitId(_unit) && !UnitUtility.IsChangeMotion(_unit))
          return;
        tempData.IJEKLMEFGNE.Add(_unit);
      }));
    }

    public static bool IsUnlockRaritySix(int _unitId) => ManagerSingleton<MasterDataManager>.Instance.masterRarity6QuestData.Get(_unitId) != null;

    public static int GetMaxRarity(int _unitId) => UnitUtility.IsUnlockRaritySix(_unitId) ? 6 : 5;

    public static int GetUnlockRarityLevel(UnlockRarity6Slot _slot, int _index)
    {
      if (_slot == null)
        return 0;
      switch (_index)
      {
        case 1:
          return (int) _slot.Slot1;
        case 2:
          return (int) _slot.Slot2;
        case 3:
          return (int) _slot.Slot3;
        default:
          return 0;
      }
    }

    public static int GetUnlockRarityStatus(UnlockRarity6Slot _slot, int _index)
    {
      if (_slot == null)
        return 0;
      switch (_index)
      {
        case 1:
          return (int) _slot.Status1;
        case 2:
          return (int) _slot.Status2;
        case 3:
          return (int) _slot.Status3;
        default:
          return 0;
      }
    }

    public static bool IsHighRarityEquipEnhanceAll(int _unitId, UnlockRarity6Slot _slot)
    {
      MasterUnlockRarity6 masterUnlockRarity6 = ManagerSingleton<MasterDataManager>.Instance.masterUnlockRarity6;
      for (byte slot_id = 1; slot_id <= (byte) 3; ++slot_id)
      {
        List<MasterUnlockRarity6.UnlockRarity6> byUnlockLevelAsc = masterUnlockRarity6.GetListWithUnitIdAndSlotIdOrderByUnlockLevelAsc(_unitId, slot_id);
        if (UnitUtility.GetUnlockRarityLevel(_slot, (int) slot_id) < byUnlockLevelAsc.Count)
          return false;
      }
      return true;
    }

    public static string GetUnitDataTopLeftSpriteValue(
      eUnitIconTopLeftSprite _spriteType,
      UnitData _unitData)
    {
      string str = string.Empty;
      switch (_spriteType)
      {
        case eUnitIconTopLeftSprite.LEVEL:
          str = _unitData.UnitLevel.ToString();
          break;
        case eUnitIconTopLeftSprite.POWER:
          str = _unitData.Power.ToString();
          break;
        case eUnitIconTopLeftSprite.HP:
          str = _unitData.TotalHp.ToString();
          break;
        case eUnitIconTopLeftSprite.PHY_OFF:
          str = _unitData.TotalAtk.ToString();
          break;
        case eUnitIconTopLeftSprite.PHY_DEF:
          str = _unitData.TotalDef.ToString();
          break;
        case eUnitIconTopLeftSprite.MAG_OFF:
          str = _unitData.TotalMagicAtk.ToString();
          break;
        case eUnitIconTopLeftSprite.MAG_DEF:
          str = _unitData.TotalMagicDef.ToString();
          break;
        case eUnitIconTopLeftSprite.NAME:
          str = (string) ManagerSingleton<MasterDataManager>.Instance.masterUnitData.Get((int) _unitData.Id).UnitName;
          break;
        case eUnitIconTopLeftSprite.RANGE:
          str = ManagerSingleton<MasterDataManager>.Instance.masterUnitData.Get((int) _unitData.Id).SearchAreaWidth.ToString();
          break;
        case eUnitIconTopLeftSprite.RANK:
          str = ((int) _unitData.PromotionLevel).ToString();
          break;
      }
      return str;
    }

    public static string GetUnitDataTopLeftSpriteValue(
      UnitSort.eSortType _sortType,
      UnitData _unitData) => _unitData == null ? string.Empty : UnitUtility.GetUnitDataTopLeftSpriteValue(UnitUtility.SortTypeToTopLeftSprite(_sortType), _unitData);

    public static string GetClanDispatchUnitTopLeftSpriteValue(
      eUnitIconTopLeftSprite _spriteType,
      ClanDispatchUnit _clanDispatchUnit) => _clanDispatchUnit == null ? string.Empty : UnitUtility.GetUnitDataTopLeftSpriteValue(_spriteType, _clanDispatchUnit.UnitData);

    public static string GetClanDispatchUnitTopLeftSpriteValue(
      UnitSort.eSortType _sortType,
      ClanDispatchUnit _clanDispatchUnit) => UnitUtility.GetClanDispatchUnitTopLeftSpriteValue(UnitUtility.SortTypeToTopLeftSprite(_sortType), _clanDispatchUnit);

    public static eUnitIconTopLeftSprite SortTypeToTopLeftSprite(
      UnitSort.eSortType _sortType)
    {
      switch (_sortType)
      {
        case UnitSort.eSortType.LV:
          return eUnitIconTopLeftSprite.LEVEL;
        case UnitSort.eSortType.POWER:
          return eUnitIconTopLeftSprite.POWER;
        case UnitSort.eSortType.HP:
          return eUnitIconTopLeftSprite.HP;
        case UnitSort.eSortType.ATK:
          return eUnitIconTopLeftSprite.PHY_OFF;
        case UnitSort.eSortType.MAGIC_ATK:
          return eUnitIconTopLeftSprite.MAG_OFF;
        case UnitSort.eSortType.DEF:
          return eUnitIconTopLeftSprite.PHY_DEF;
        case UnitSort.eSortType.MAGIC_DEF:
          return eUnitIconTopLeftSprite.MAG_DEF;
        case UnitSort.eSortType.RANK:
          return eUnitIconTopLeftSprite.RANK;
        case UnitSort.eSortType.JAPANESE_SYLLABARY:
          return eUnitIconTopLeftSprite.NAME;
        case UnitSort.eSortType.AFFECTION_RANK:
          return eUnitIconTopLeftSprite.LOVE;
        default:
          return eUnitIconTopLeftSprite.NONE;
      }
    }

    public static UnitData GetUnitMaxParam(int _unitId)
    {
      MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
      MasterUnitPromotion.UnitPromotion maxUnitPromotion = instance.masterUnitPromotion.GetMaxUnitPromotion(_unitId);
      int levelMax = instance.masterExperienceUnit.LevelMax;
      int promotionLevel = (int) maxUnitPromotion.promotion_level;
      MasterUnitData.UnitData unitData1 = instance.masterUnitData.Get(_unitId);
      UnitData unitData2 = new UnitData();
      unitData2.SetId(_unitId);
      unitData2.SetUnitLevel(levelMax);
      unitData2.SetUnitRarity((int) unitData1.Rarity);
      unitData2.SetPromotionLevel((ePromotionLevel) promotionLevel);
      return unitData2;
    }

    public static void SetUnitRankLabel(CustomUILabel _label, int _rank, bool _insertSpace = false) => _label.SetText(UnitUtility.UnitRankLabel(_rank, _insertSpace), (object[]) Array.Empty<object>());

    public static string UnitRankLabel(int _rank, bool _insertSpace = false)
    {
      string str = string.Format("{0}{1}", !_insertSpace ? (object) eTextId.RANK_ROMAN.Name() : (object) eTextId.CHARA_LIST_RANK.Name(), (object) _rank);
      return PromotionDefine.PromotionGradationDic[(ePromotionLevel) _rank].EnchantCode((object) str);
    }

    public static int GetTargetConditionUnitCount(
      MissionDefine.UnitParameterType _type,
      int _conditon,
      int _unitCount)
    {
      int num = 0;
      foreach (UnitParameter unitParameter in Singleton<UserData>.Instance.UnitParameterDictionary.Values)
      {
        if ((_type == MissionDefine.UnitParameterType.PROMOTION ? (int) unitParameter.UniqueData.PromotionLevel : unitParameter.UniqueData.UnitRarity.GetDecrypted()) == _conditon)
        {
          ++num;
          if (num >= _unitCount)
            break;
        }
      }
      return num;
    }

    public static bool IsPartySameChara(
      List<int> _partyList,
      int _addUnitId,
      int supportUnitId = 0,
      bool _isSettingIgnore = false)
    {
      if (!Singleton<UserData>.Instance.IsSameCharaHave || ManagerSingleton<SaveDataManager>.Instance.PartySameCharaPossible && !_isSettingIgnore)
        return false;
      MasterCharaIdentity masterCharaIdentity = ManagerSingleton<MasterDataManager>.Instance.masterCharaIdentity;
      for (int index = 0; index < _partyList.Count; ++index)
      {
        int _originUnitId = _partyList[index];
        if (_originUnitId != _addUnitId && supportUnitId != _addUnitId)
        {
          if (_originUnitId == 1)
          {
            if (supportUnitId > 0)
              _originUnitId = supportUnitId;
            else
              continue;
          }
          if (masterCharaIdentity.IsSameChara(_originUnitId, _addUnitId))
            return true;
        }
      }
      return false;
    }

    public static void CheckPartySameCharaRemove(
      List<int> _partyList,
      ePartyType _partyType,
      out List<int> _newPartyList,
      int _supportUnitId = 0,
      System.Action<bool> _callback = null)
    {
      switch (_partyType)
      {
        case ePartyType.STORY:
        case ePartyType.FAVORITE:
        case ePartyType.REPLAY:
        case ePartyType.ROOM_GROUND_FLOOR:
        case ePartyType.ROOM_SECOND_FLOOR:
        case ePartyType.ROOM_THIRD_FLOOR:
          _newPartyList = (List<int>) null;
          break;
        default:
          UnitUtility.getRemovePartyList(_partyList, out _newPartyList, _supportUnitId, _callback);
          break;
      }
    }

    public static void CheckPartySameCharaRemove(
      List<int> _partyList,
      ePartyType _partyType,
      out List<int> _newPartyList,
      Dictionary<int, int> _supportUnitId = null,
      System.Action<bool> _callback = null)
    {
      if (_supportUnitId == null)
      {
        _newPartyList = (List<int>) null;
      }
      else
      {
        _newPartyList = new List<int>();
        List<int> sameCharaList = ManagerSingleton<MasterDataManager>.Instance.masterCharaIdentity.GetSameCharaList(_partyList, _supportUnitId);
        for (int index = 0; index < _partyList.Count; ++index)
        {
          if (!sameCharaList.Contains(_partyList[index]))
            _newPartyList.Add(_partyList[index]);
        }
        _callback.Call<bool>(sameCharaList.Contains(1));
      }
    }

    private static void getRemovePartyList(
      List<int> _partyList,
      out List<int> _newPartyList,
      int _supportUnitId = 0,
      System.Action<bool> _callback = null)
    {
      _newPartyList = new List<int>();
      List<int> sameCharaList = ManagerSingleton<MasterDataManager>.Instance.masterCharaIdentity.GetSameCharaList(_partyList, new Dictionary<int, int>()
      {
        {
          1,
          _supportUnitId
        }
      });
      for (int index = 0; index < _partyList.Count; ++index)
      {
        if (!sameCharaList.Contains(_partyList[index]))
          _newPartyList.Add(_partyList[index]);
      }
      _callback.Call<bool>(sameCharaList.Contains(1));
    }

    public static void RemoveLowLevelUnit(ref List<int> _partyList, int _level)
    {
      Dictionary<int, UnitParameter> unitParam = Singleton<UserData>.Instance.UnitParameterDictionary;
      _partyList.RemoveAll((Predicate<int>) (_match =>
      {
        UnitParameter unitParameter = (UnitParameter) null;
        return unitParam.TryGetValue(_match, out unitParameter) && (int) unitParameter.UniqueData.UnitLevel < _level;
      }));
    }

    public static void RemoveSameUnitPartyList(
      List<int> _partyList,
      out List<int> _newPartyList,
      int _targetUnit)
    {
      _newPartyList = new List<int>();
      if (!Singleton<UserData>.Instance.IsSameCharaHave)
        _newPartyList = _partyList;
      else if (ManagerSingleton<SaveDataManager>.Instance.PartySameCharaPossible)
      {
        _newPartyList = _partyList;
      }
      else
      {
        MasterCharaIdentity masterCharaIdentity = ManagerSingleton<MasterDataManager>.Instance.masterCharaIdentity;
        for (int index = 0; index < _partyList.Count; ++index)
        {
          if (_partyList[index] != _targetUnit && !masterCharaIdentity.IsSameChara(_partyList[index], _targetUnit))
            _newPartyList.Add(_partyList[index]);
        }
      }
    }

    public static void CheckAllPartySameCharaRemove(
      Dictionary<int, List<int>> _partyListDic,
      out Dictionary<int, List<int>> _newPartyListDic)
    {
      List<int> _newPartyList1 = new List<int>();
      _newPartyListDic = new Dictionary<int, List<int>>();
      Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
      foreach (KeyValuePair<int, List<int>> keyValuePair in _partyListDic)
      {
        List<int> _newPartyList2 = new List<int>();
        UnitUtility.getRemovePartyList(keyValuePair.Value, out _newPartyList2);
        if (!dictionary.ContainsKey(keyValuePair.Key))
          dictionary.Add(keyValuePair.Key, new List<int>());
        dictionary[keyValuePair.Key] = _newPartyList2;
      }
      foreach (KeyValuePair<int, List<int>> keyValuePair in dictionary.Reverse<KeyValuePair<int, List<int>>>())
      {
        UnitUtility.getAllCheckRemovePartyList(keyValuePair.Value, keyValuePair.Key, dictionary, out _newPartyList1);
        _newPartyListDic.Add(keyValuePair.Key, _newPartyList1);
      }
    }

    private static void getAllCheckRemovePartyList(
      List<int> _checkPartyList,
      int _currentKey,
      Dictionary<int, List<int>> _partyListDic,
      out List<int> _newPartyList)
    {
      _newPartyList = new List<int>();
      MasterCharaIdentity masterCharaIdentity = ManagerSingleton<MasterDataManager>.Instance.masterCharaIdentity;
      Dictionary<int, int> charaTypeToUnitId1 = masterCharaIdentity.GetCharaTypeToUnitId(_checkPartyList);
      List<int> intList = new List<int>();
      foreach (KeyValuePair<int, List<int>> keyValuePair1 in _partyListDic)
      {
        if (keyValuePair1.Key != _currentKey && keyValuePair1.Key <= _currentKey)
        {
          Dictionary<int, int> charaTypeToUnitId2 = masterCharaIdentity.GetCharaTypeToUnitId(keyValuePair1.Value);
          foreach (KeyValuePair<int, int> keyValuePair2 in charaTypeToUnitId1)
          {
            if (charaTypeToUnitId2.ContainsKey(keyValuePair2.Key) && !intList.Contains(keyValuePair2.Value))
              intList.Add(keyValuePair2.Value);
          }
        }
      }
      for (int index = 0; index < _checkPartyList.Count; ++index)
      {
        if (!intList.Contains(_checkPartyList[index]))
          _newPartyList.Add(_checkPartyList[index]);
      }
    }

    public static void OpenFirstTimeSameCharaSettingDialog(System.Action _closeCallback = null, bool _isForceExec = false)
    {
      if (TutorialManager.IsActive && !_isForceExec || (!Singleton<UserData>.Instance.IsSameCharaHave || !ManagerSingleton<SaveDataManager>.Instance.IsFirstTimeSameCharaSetting))
        return;
      ManagerSingleton<SaveDataManager>.Instance.IsFirstTimeSameCharaSetting = false;
      ManagerSingleton<SaveDataManager>.Instance.Save();
      ManagerSingleton<DialogManager>.Instance.OpenSameSettingCharaDialog(_closeCallback);
    }

    public static bool CheckPartyEdit(int[] _party)
    {
      for (int index = 0; index < _party.Length; ++index)
      {
        if (_party[index] > 0)
          return true;
      }
      return false;
    }

    public static List<int> CheckPartyListEdit(Dictionary<int, List<int>> _dicDeckList)
    {
      List<int> intList = new List<int>();
      bool flag = false;
      foreach (KeyValuePair<int, List<int>> dicDeck in _dicDeckList)
      {
        for (int index = 0; index < dicDeck.Value.Count; ++index)
        {
          if (dicDeck.Value[index] > 0)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          intList.Add(dicDeck.Key);
        flag = false;
      }
      return intList;
    }

    public static void UpdateSameCharaPossession(bool _isCheckUnit = false)
    {
      UserData instance = Singleton<UserData>.Instance;
      if (instance.IsSameCharaHave)
        return;
      if (!ManagerSingleton<SaveDataManager>.Instance.IsFirstTimeSameCharaSetting)
      {
        instance.IsSameCharaHave = true;
      }
      else
      {
        if (!_isCheckUnit)
          return;
        MasterCharaIdentity masterCharaIdentity = ManagerSingleton<MasterDataManager>.Instance.masterCharaIdentity;
        List<int> intList = new List<int>((IEnumerable<int>) instance.UnitParameterDictionary.Keys);
        for (int index1 = 0; index1 < intList.Count; ++index1)
        {
          int _targetUnitId = intList[index1];
          for (int index2 = 0; index2 < intList.Count; ++index2)
          {
            int _originUnitId = intList[index2];
            if (_originUnitId != _targetUnitId && masterCharaIdentity.IsSameChara(_originUnitId, _targetUnitId))
            {
              instance.IsSameCharaHave = true;
              return;
            }
          }
        }
        instance.IsSameCharaHave = false;
      }
    }

    public static void SaveOwnedCharaTypesForTitleCall()
    {
      try
      {
        UserData instance = Singleton<UserData>.Instance;
        MasterCharaIdentity masterCharaIdentity = ManagerSingleton<MasterDataManager>.Instance.masterCharaIdentity;
        ManagerSingleton<SaveDataManager>.Instance.TitleCallCharaTypeList = instance.UnitParameterDictionary.Keys.Select<int, MasterCharaIdentity.CharaIdentity>((Func<int, MasterCharaIdentity.CharaIdentity>) (id => masterCharaIdentity.Get(id))).Where<MasterCharaIdentity.CharaIdentity>((Func<MasterCharaIdentity.CharaIdentity, bool>) (chara => chara != null)).Select<MasterCharaIdentity.CharaIdentity, int>((Func<MasterCharaIdentity.CharaIdentity, int>) (chara => (int) chara.chara_type)).Distinct<int>().ToList<int>();
      }
      catch (Exception ex)
      {
      }
    }

    public static bool IsLevelUpperLimitBeyond(
      ePartyType _partyType,
      int _supportUnitLv,
      int _playerLv,
      out eUnitIconBottomLabel _buttomLabel)
    {
      UserData instance = Singleton<UserData>.Instance;
      int num;
      switch (_partyType)
      {
        case ePartyType.DUNGEON:
          num = (int) instance.InitSettingParameter.DungeonSetting.SupportLvBand;
          _buttomLabel = eUnitIconBottomLabel.UPPER_LIMIT_LEVEL_DUNGEON;
          break;
        case ePartyType.CLAN_BATTLE:
        case ePartyType.SEKAI:
          num = (int) instance.InitSettingParameter.ClanBattleSetting.SupportLvBand;
          _buttomLabel = eUnitIconBottomLabel.UPPER_LIMIT_LEVEL_CLAN_BATTLE;
          break;
        case ePartyType.TOWER:
        case ePartyType.TOWER_EX_1:
        case ePartyType.TOWER_EX_2:
        case ePartyType.TOWER_EX_3:
          num = (int) instance.InitSettingParameter.TowerSetting.SupportLvBand;
          _buttomLabel = eUnitIconBottomLabel.UPPER_LIMIT_LEVEL_TOWER;
          break;
        case ePartyType.KAISER_BATTLE_SUB_1:
        case ePartyType.KAISER_BATTLE_SUB_2:
        case ePartyType.KAISER_BATTLE_SUB_3:
        case ePartyType.KAISER_BATTLE_SUB_4:
        case ePartyType.KAISER_BATTLE_MAIN:
          num = (int) instance.InitSettingParameter.KaiserBattleIniSetting.SupportLvBand;
          _buttomLabel = eUnitIconBottomLabel.UPPER_LIMIT_LEVEL_KAISER;
          break;
        default:
          num = -1;
          _buttomLabel = eUnitIconBottomLabel.NONE;
          break;
      }
      if (_supportUnitLv > _playerLv + num && num > 0)
        return true;
      if (num != 0 || _supportUnitLv <= _playerLv)
        return false;
      _buttomLabel = eUnitIconBottomLabel.UPPER_LIMIT_LEVEL;
      return true;
    }

    public static bool IsOverSupportLevel(ePartyType _partyType, int _supportUnitLv, int _playerLv)
    {
      UserData instance = Singleton<UserData>.Instance;
      int num;
      switch (_partyType)
      {
        case ePartyType.DUNGEON:
          num = (int) instance.InitSettingParameter.DungeonSetting.SupportLvBand;
          break;
        case ePartyType.CLAN_BATTLE:
          num = (int) instance.InitSettingParameter.ClanBattleSetting.SupportLvBand;
          break;
        case ePartyType.TOWER:
        case ePartyType.TOWER_EX_1:
        case ePartyType.TOWER_EX_2:
        case ePartyType.TOWER_EX_3:
          num = (int) instance.InitSettingParameter.TowerSetting.SupportLvBand;
          break;
        case ePartyType.KAISER_BATTLE_SUB_1:
        case ePartyType.KAISER_BATTLE_SUB_2:
        case ePartyType.KAISER_BATTLE_SUB_3:
        case ePartyType.KAISER_BATTLE_SUB_4:
        case ePartyType.KAISER_BATTLE_MAIN:
          num = (int) instance.InitSettingParameter.TowerSetting.SupportLvBand;
          break;
        default:
          num = -1;
          break;
      }
      return _supportUnitLv > _playerLv + num && num > 0;
    }

    public static bool CheckEquippedUnique(UnitData _unit)
    {
      if (ManagerSingleton<MasterDataManager>.Instance.masterUnitUniqueEquip.Get((int) _unit.Id) == null)
        return false;
      UserData instance = Singleton<UserData>.Instance;
      return _unit.PromotionLevel >= (ePromotionLevel) (int) instance.InitSettingParameter.UniqueEquipSetting.LimitList[0].Promotion;
    }

    public static bool IsUniqueEquipmentStrength(UnitData _unit, int _idx)
    {
      if (_idx < 0 || _unit.UniqueEquipSlot.Count <= _idx)
        return false;
      EquipSlot equipSlot = _unit.UniqueEquipSlot[_idx];
      return ManagerSingleton<MasterDataManager>.Instance.masterUniqueEquipmentEnhanceData.LimitLevel(_idx, (int) equipSlot.Rank) > (int) equipSlot.EnhancementLevel && (bool) equipSlot.IsSlot;
    }

    public static int GetUniqueEquipSlotNum(int _unitId)
    {
      if (!Singleton<UserData>.Instance.IsMyUnit(_unitId) || Singleton<UserData>.Instance.UnitParameterDictionary[_unitId].UniqueData.PromotionLevel < ePromotionLevel.Gold1)
        return 0;
      MasterUnitUniqueEquip.UnitUniqueEquip unitUniqueEquip = ManagerSingleton<MasterDataManager>.Instance.masterUnitUniqueEquip.Get(_unitId);
      return unitUniqueEquip == null ? 0 : (int) unitUniqueEquip.equip_slot;
    }

    public static int GetUniqueEquipInfo(UnitData _unit)
    {
      if (_unit == null || _unit.UniqueEquipSlot == null)
        return 0;
      for (int index = 0; index < _unit.UniqueEquipSlot.Count; ++index)
      {
        if ((bool) _unit.UniqueEquipSlot[index].IsSlot)
          return 1;
      }
      return 0;
    }

    public static int GetUniqueEquipInfo(UnitParameter _unit)
    {
      if (_unit == null || _unit.UniqueData.UniqueEquipSlot == null)
        return 0;
      for (int index = 0; index < _unit.UniqueData.UniqueEquipSlot.Count; ++index)
      {
        if ((bool) _unit.UniqueData.UniqueEquipSlot[index].IsSlot)
          return 1;
      }
      return 0;
    }

    public static int GetUniqueEquipInfo(UnitDataForView _unit)
    {
      if (_unit == null || _unit.UniqueEquipSlot == null)
        return 0;
      for (int index = 0; index < _unit.UniqueEquipSlot.Count; ++index)
      {
        if ((bool) _unit.UniqueEquipSlot[index].IsSlot)
          return 1;
      }
      return 0;
    }

    public static int GetUniqueEquipInfo(DungeonUnit _unit)
    {
      if (_unit == null || _unit.UniqueEquippedList == null)
        return 0;
      for (int index = 0; index < _unit.UniqueEquippedList.Count; ++index)
      {
        if ((int) _unit.UniqueEquippedList[index] != 0)
          return 1;
      }
      return 0;
    }

    public static int GetUniqueEquipInfo(MyLogUnitData _unit)
    {
      if (_unit == null || _unit.UniqueEquipSlot == null)
        return 0;
      for (int index = 0; index < _unit.UniqueEquipSlot.Count; ++index)
      {
        if ((bool) _unit.UniqueEquipSlot[index].IsSlot)
          return 1;
      }
      return 0;
    }

    public static int GetUniqueEquipInfo(ReplayUnitDataForView _unit)
    {
      if (_unit == null || _unit.UniqueEquipSlot == null)
        return 0;
      for (int index = 0; index < _unit.UniqueEquipSlot.Count; ++index)
      {
        if ((bool) _unit.UniqueEquipSlot[index].IsSlot)
          return 1;
      }
      return 0;
    }

    public static int GetUnitMaxRarity(int _unitId) => ManagerSingleton<MasterDataManager>.Instance.masterUnitRarity[_unitId].Select<MasterUnitRarity.UnitRarity, int>((Func<MasterUnitRarity.UnitRarity, int>) (data => (int) data.rarity)).Max();

    public static List<SkillLimitCounter> CalcSkillLimitCount(UnitCtrl _unit)
    {
      List<SkillLimitCounter> skillLimitCounterList = new List<SkillLimitCounter>();
      Dictionary<int, int>.Enumerator enumerator = _unit.SkillUseCount.GetEnumerator();
      while (enumerator.MoveNext())
      {
        KeyValuePair<int, int> current = enumerator.Current;
        int key = current.Key;
        int _counter = current.Value;
        if (_counter != 0)
          skillLimitCounterList.Add(new SkillLimitCounter(key, _counter));
      }
      return skillLimitCounterList;
    }

    public static UnitDataForView ConvertReplayUnitData(
      ReplayUnitDataForView _towerUnitData) => new UnitDataForView((int) _towerUnitData.Id, (int) _towerUnitData.UnitLevel, UnitUtility.SelectRarity((int) _towerUnitData.UnitRarity, (int) _towerUnitData.BattleRarity), 0, _towerUnitData.PromotionLevel, 0, _towerUnitData.SkinData, _towerUnitData.UniqueEquipSlot);

    public static void CopyEquipIdFromUnitPromotion(ref UnitData _unitData, bool _init = false)
    {
      MasterUnitPromotion masterUnitPromotion = ManagerSingleton<MasterDataManager>.Instance.masterUnitPromotion;
      int id = (int) _unitData.Id;
      int promotionLevel = (int) _unitData.PromotionLevel;
      int unit_id = id;
      int promotion_level = promotionLevel;
      MasterUnitPromotion.UnitPromotion unitPromotion = masterUnitPromotion.Get(unit_id, promotion_level);
      List<EquipSlot> equipSlot = _unitData.EquipSlot;
      for (int index = 0; index < equipSlot.Count; ++index)
      {
        equipSlot[index].SetId(unitPromotion.equip_slot[index]);
        if (_init)
        {
          equipSlot[index].SetIsSlot(false);
          equipSlot[index].SetEnhancementLevel(0);
        }
      }
    }

    public static int GetLowestLevelInTheCurrentRank(UnitData _unitData)
    {
      MasterEquipmentData masterEquipmentData = ManagerSingleton<MasterDataManager>.Instance.masterEquipmentData;
      int val1 = int.MinValue;
      List<EquipSlot> equipSlot = _unitData.EquipSlot;
      for (int index = 0; index < equipSlot.Count; ++index)
      {
        if ((int) equipSlot[index].Id != 999999)
        {
          MasterEquipmentData.EquipmentData equipmentData = masterEquipmentData.Get((int) equipSlot[index].Id);
          val1 = Math.Max(val1, equipmentData.Require);
        }
      }
      return Math.Max(val1, (int) _unitData.UnitLevel);
    }

    public static void CalculateSkillInfo(UnitData _target)
    {
      MasterUnitSkillData.UnitSkillData unitSkillData = ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData[(int) _target.Id];
      List<SkillLevelInfo> _mainSkill = new List<SkillLevelInfo>();
      for (int index = 0; index < unitSkillData.MainSkillIds.Count; ++index)
      {
        int mainSkillId = unitSkillData.MainSkillIds[index];
        if (mainSkillId != 0)
        {
          if (index == 0 && _target.UniqueEquipSlot.Count > 0 && (bool) _target.UniqueEquipSlot[0].IsSlot)
            mainSkillId += 10;
          SkillLevelInfo skillLevelInfo = new SkillLevelInfo();
          skillLevelInfo.SetSkillId(mainSkillId);
          int key = 201 + index;
          MasterUnlockSkillData.UnlockSkillData unlockSkillData = (MasterUnlockSkillData.UnlockSkillData) null;
          if (ManagerSingleton<MasterDataManager>.Instance.masterUnlockSkillData.dictionary.TryGetValue(key, out unlockSkillData))
          {
            if ((ePromotionLevel) (int) unlockSkillData.promotion_level <= _target.PromotionLevel)
            {
              if (index < _target.MainSkill.Count)
                skillLevelInfo.SetSkillLevel((int) _target.MainSkill[index].SkillLevel);
              else
                skillLevelInfo.SetSkillLevel(1);
            }
            else
              break;
          }
          _mainSkill.Add(skillLevelInfo);
        }
      }
      _target.SetMainSkill(_mainSkill);
      List<SkillLevelInfo> _exSkill = new List<SkillLevelInfo>();
      for (int index = 0; index < unitSkillData.ExSkillIds.Count; ++index)
      {
        int exSkillId = unitSkillData.ExSkillIds[index];
        if (exSkillId != 0)
        {
          SkillLevelInfo skillLevelInfo = new SkillLevelInfo();
          skillLevelInfo.SetSkillId(exSkillId);
          int key = 301 + index;
          MasterUnlockSkillData.UnlockSkillData unlockSkillData = (MasterUnlockSkillData.UnlockSkillData) null;
          if (ManagerSingleton<MasterDataManager>.Instance.masterUnlockSkillData.dictionary.TryGetValue(key, out unlockSkillData))
          {
            if ((ePromotionLevel) (int) unlockSkillData.promotion_level <= _target.PromotionLevel)
            {
              if (index < _target.ExSkill.Count)
                skillLevelInfo.SetSkillLevel((int) _target.ExSkill[index].SkillLevel);
              else
                skillLevelInfo.SetSkillLevel(1);
            }
            else
              break;
          }
          _exSkill.Add(skillLevelInfo);
        }
      }
      _target.SetExSkill(_exSkill);
    }

    public static int GetLevelWhenUsingPotion(
      UnitData _unit,
      Dictionary<int, InventoryData> _configured = null,
      bool _checkLimit = true)
    {
      if (_configured == null)
        return UnitUtility.GetLowestLevelInTheCurrentRank(_unit);
      MasterItemData masterItemData1 = ManagerSingleton<MasterDataManager>.Instance.masterItemData;
      List<UserItemParameter> userItemParameterList = Singleton<UserData>.Instance.ListUpExpItems();
      int _addExp = 0;
      Dictionary<int, InventoryData>.Enumerator config = _configured.GetEnumerator();
      while (config.MoveNext())
      {
        if (userItemParameterList.Find((Predicate<UserItemParameter>) (_match => config.Current.Value.Id == (int) _match.ItemId)) != null)
        {
          int num1 = _addExp;
          MasterItemData masterItemData2 = masterItemData1;
          KeyValuePair<int, InventoryData> current = config.Current;
          int id = current.Value.Id;
          int num2 = (int) masterItemData2.Get(id).Value;
          current = config.Current;
          int stock = current.Value.Stock;
          int num3 = num2 * stock;
          _addExp = num1 + num3;
        }
      }
      return _addExp == 0 ? UnitUtility.GetLowestLevelInTheCurrentRank(_unit) : Math.Max(UnitUtility.CalcUnitLevel((int) _unit.Id, _addExp, _checkLimit), UnitUtility.GetLowestLevelInTheCurrentRank(_unit));
    }

    public static bool IsLimitedUnit(int _unitId) => (int) ManagerSingleton<MasterDataManager>.Instance.masterUnitData[_unitId].IsLimited == 1;

    public static bool IsSuperMaterialUnlocked(int _unitId)
    {
      UnitParameter unitParameter = (UnitParameter) null;
      if (!Singleton<UserData>.Instance.UnitParameterDictionary.TryGetValue(_unitId, out unitParameter))
        return false;
      UnitData uniqueData = unitParameter.UniqueData;
      if ((int) uniqueData.UnitRarity < 5)
        return false;
      List<EquipSlot> uniqueEquipSlot = uniqueData.UniqueEquipSlot;
      return (uniqueEquipSlot.Count <= 0 ? 0 : ((bool) uniqueEquipSlot[0].IsSlot ? 1 : 0)) != 0;
    }

    public static bool IsMonsterCommonNumber(long _unitNumber) => _unitNumber % 100L == 99L;

    public static bool IsEqMonsterType(long _unitNumberA, long _unitNumberB) => _unitNumberA / 100L == _unitNumberB / 100L;

    public static long MakeMonsterCommonNumber(long _unitNumber) => _unitNumber / 100L * 100L + 99L;

    public static bool IsStartPrincessFesGachaUnit(MasterUnitData.UnitData _unit, bool _inSession)
    {
      if ((int) _unit.IsLimited == 0 || !_inSession)
        return false;
      List<int> supplyUnitIdList = Singleton<UserData>.Instance.StartPrincessFesSupplyUnitIDList;
      return supplyUnitIdList.Count != 0 && supplyUnitIdList.Contains((int) _unit.UnitId);
    }

    public static bool IsReturnUserPrincessFesGachaUnit(
      MasterUnitData.UnitData _unit,
      bool _inSession)
    {
      if ((int) _unit.IsLimited == 0 || !_inSession)
        return false;
      List<int> supplyUnitIdList = Singleton<UserData>.Instance.ReturnUserPrincessFesSupplyUnitIDList;
      return supplyUnitIdList.Count != 0 && supplyUnitIdList.Contains((int) _unit.UnitId);
    }

    public static int SelectRarity(int _unitRarity, int _battleRarity) => _unitRarity != 5 || _battleRarity == 0 || (_battleRarity < 3 || 5 < _battleRarity) ? _unitRarity : _battleRarity;

    public static bool CheckDiffenceRarity(
      List<UnitDamageInfo> _damageInfos,
      ref List<ChangeRarityUnit> _changeRarityUnits) => UnitUtility.CheckDiffenceRarity(_damageInfos.ConvertAll<ChangeRarityUnit>((Converter<UnitDamageInfo, ChangeRarityUnit>) (_v => new ChangeRarityUnit(_v.unit_id, _v.rarity))), ref _changeRarityUnits);

    public static bool CheckDiffenceRarity(
      List<ReplayUnitDataForView> _unitInfos,
      ref List<ChangeRarityUnit> _changeRarityUnits) => UnitUtility.CheckDiffenceRarity(_unitInfos.ConvertAll<ChangeRarityUnit>((Converter<ReplayUnitDataForView, ChangeRarityUnit>) (_v => new ChangeRarityUnit((int) _v.Id, UnitUtility.SelectRarity((int) _v.UnitRarity, (int) _v.BattleRarity)))), ref _changeRarityUnits);

    public static bool CheckDiffenceRarity(
      List<UnitDataForUnitIcon> _unitData,
      ref List<ChangeRarityUnit> _changeRarityUnits) => UnitUtility.CheckDiffenceRarity(_unitData.ConvertAll<ChangeRarityUnit>((Converter<UnitDataForUnitIcon, ChangeRarityUnit>) (_v => new ChangeRarityUnit(_v.UnitId, _v.BattleRarity))), ref _changeRarityUnits);

    public static bool CheckDiffenceRarity(
      List<ChangeRarityUnit> _tempParty,
      ref List<ChangeRarityUnit> _changeRarityUnits)
    {
      if (_tempParty == null)
        return false;
      Dictionary<int, UnitParameter> parameterDictionary = Singleton<UserData>.Instance.UnitParameterDictionary;
      for (int index = 0; index < _tempParty.Count; ++index)
      {
        ChangeRarityUnit changeRarityUnit = _tempParty[index];
        if (changeRarityUnit.unit_id != 0 && parameterDictionary.ContainsKey(changeRarityUnit.unit_id))
        {
          UnitParameter unitParameter = parameterDictionary[changeRarityUnit.unit_id];
          if ((int) unitParameter.UniqueData.UnitRarity == 5 && (changeRarityUnit.battle_rarity == 0 || changeRarityUnit.battle_rarity >= 3 && 5 >= changeRarityUnit.battle_rarity) && unitParameter.UniqueData.GetCurrentRarity() != UnitUtility.SelectRarity((int) unitParameter.UniqueData.UnitRarity, changeRarityUnit.battle_rarity))
            _changeRarityUnits.Add(changeRarityUnit);
        }
      }
      return _changeRarityUnits.Count > 0;
    }

    public static void CheckChangeRarityPostParam(List<ChangeRarityUnit> _postData) => _postData.ForEach((System.Action<ChangeRarityUnit>) (_v =>
    {
      if (_v.battle_rarity != 0)
        return;
      _v.SetBattleRarity(5);
    }));

    public static List<ChangeRarityUnit> GetPartyCurrentRarity(
      ePartyType _partyType,
      List<ChangeRarityUnit> _overrideRarity = null)
    {
      List<ChangeRarityUnit> changeRarityUnitList = new List<ChangeRarityUnit>();
      UserData instance = Singleton<UserData>.Instance;
      foreach (int partyUnitId in instance.FindPartyUnitIdArray(_partyType))
      {
        int unitId = partyUnitId;
        UnitParameter unitParameter = (UnitParameter) null;
        if (instance.UnitParameterDictionary.TryGetValue(unitId, out unitParameter))
        {
          if (_overrideRarity != null)
          {
            ChangeRarityUnit changeRarityUnit = _overrideRarity.Find((Predicate<ChangeRarityUnit>) (_match => _match.unit_id == unitId));
            changeRarityUnitList.Add(new ChangeRarityUnit(unitId, UnitUtility.SelectRarity((int) unitParameter.UniqueData.UnitRarity, changeRarityUnit == null ? 0 : changeRarityUnit.battle_rarity)));
          }
          else
            changeRarityUnitList.Add(new ChangeRarityUnit(unitId, unitParameter.UniqueData.GetCurrentRarity()));
        }
        else
          changeRarityUnitList.Add(new ChangeRarityUnit(0, 0));
      }
      changeRarityUnitList.AddRange(Enumerable.Repeat<ChangeRarityUnit>(new ChangeRarityUnit(0, 0), changeRarityUnitList.Count - 5));
      return changeRarityUnitList;
    }

    public static int GetUnitRarity(int _unitId)
    {
      Dictionary<int, UnitParameter> parameterDictionary = Singleton<UserData>.Instance.UnitParameterDictionary;
      return parameterDictionary.ContainsKey(_unitId) ? (int) parameterDictionary[_unitId].UniqueData.UnitRarity : 0;
    }

    public static int GetBattleRarity(int _unitId)
    {
      Dictionary<int, UnitParameter> parameterDictionary = Singleton<UserData>.Instance.UnitParameterDictionary;
      return parameterDictionary.ContainsKey(_unitId) ? (int) parameterDictionary[_unitId].UniqueData.BattleRarity : 0;
    }

    public static void UseClearPartyConfirm(
      bool _isDifferenceRarity,
      System.Action _openUseClearParty,
      System.Action _openDifferenceRarity,
      System.Action _waitApiAction,
      System.Action _noWaitAction)
    {
      SaveDataManager instance1 = ManagerSingleton<SaveDataManager>.Instance;
      DialogManager instance2 = ManagerSingleton<DialogManager>.Instance;
      if (instance1.UseClearPartyConfirm)
      {
        if (instance1.DisplayChangeRarityConfirm & _isDifferenceRarity)
          _openDifferenceRarity.Call();
        else
          _openUseClearParty.Call();
      }
      else if (instance1.DisplayChangeRarityConfirm & _isDifferenceRarity)
        _openDifferenceRarity.Call();
      else if (instance1.UseOtherPartyRarity & _isDifferenceRarity)
        _waitApiAction.Call();
      else
        _noWaitAction.Call();
    }

    public class SummonSkillData
    {
      public int SkillId { get; private set; }

      public int SkillLevel { get; private set; }

      public int Rarity { get; private set; }

      public int PromotionLevel { get; private set; }

      public int SummonTargetUnitId { get; private set; }

      public SummonSkillData(
        int _skillId,
        int _skillLevel,
        int _rarity,
        int _promotionLevel,
        int _summonTargetUnitId)
      {
        this.SkillId = _skillId;
        this.SkillLevel = _skillLevel;
        this.Rarity = _rarity;
        this.PromotionLevel = _promotionLevel;
        this.SummonTargetUnitId = _summonTargetUnitId;
      }
    }

    public class MaterialsRequiredForRankUp
    {
      public Dictionary<int, InventoryData> Materials = new Dictionary<int, InventoryData>();
      public int Level = 1;
      public int Gold;
      public List<int> CanNotEquip = new List<int>();
    }*/
  }
}
