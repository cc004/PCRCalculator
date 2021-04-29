// Decompiled with JetBrains decompiler
// Type: Elements.UnitDataForView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using LitJson;
using System.Collections.Generic;

namespace Elements
{
  public class UnitDataForView
  {
    public ObscuredInt Id { get; private set; }

    public ObscuredInt UnitLevel { get; private set; }

    public ObscuredInt UnitRarity { get; private set; }

    public ObscuredInt BattleRarity { get; private set; }

    public ePromotionLevel PromotionLevel { get; private set; }

    public ObscuredInt Power { get; private set; }

    //public SkinData SkinData { get; private set; }

    //public List<EquipSlot> UniqueEquipSlot { get; private set; }

    public void SetUnitLevel(int _unitLevel) => this.UnitLevel = (ObscuredInt) _unitLevel;

    //public void SetSkinData(SkinData _skinData) => this.SkinData = _skinData;

    //public void SetUniqueEquipSlot(List<EquipSlot> _uniqueEquipSlot) => this.UniqueEquipSlot = _uniqueEquipSlot;
    /*
    private void initializeUnitDataForView()
    {
      this.Id = (ObscuredInt) 0;
      this.UnitLevel = (ObscuredInt) 0;
      this.UnitRarity = (ObscuredInt) 0;
      this.BattleRarity = (ObscuredInt) 0;
      this.PromotionLevel = ePromotionLevel.INVALID_VALUE;
      this.Power = (ObscuredInt) 0;
      this.SkinData = (SkinData) null;
      this.UniqueEquipSlot = new List<EquipSlot>();
    }

    public UnitDataForView() => this.initializeUnitDataForView();

    public UnitDataForView(
      int _id,
      int _unitLevel,
      int _unitRarity,
      int _battleRarity,
      ePromotionLevel _promotionLevel,
      int _power,
      SkinData _skinData,
      List<EquipSlot> _uniqueEquipSlot)
    {
      this.Id = (ObscuredInt) _id;
      this.UnitLevel = (ObscuredInt) _unitLevel;
      this.UnitRarity = (ObscuredInt) _unitRarity;
      this.BattleRarity = (ObscuredInt) _battleRarity;
      this.PromotionLevel = _promotionLevel;
      this.Power = (ObscuredInt) _power;
      this.SkinData = _skinData;
      this.UniqueEquipSlot = _uniqueEquipSlot;
    }

    public UnitDataForView(JsonData _json)
    {
      this.initializeUnitDataForView();
      this.ParseUnitDataForView(_json);
    }

    public void ParseUnitDataForView(JsonData _json)
    {
      if (_json.Count == 0)
        return;
      this.Id = (ObscuredInt) _json["id"].ToInt();
      if (_json.Keys.Contains("unit_level"))
        this.UnitLevel = (ObscuredInt) _json["unit_level"].ToInt();
      if (_json.Keys.Contains("unit_rarity"))
        this.UnitRarity = (ObscuredInt) _json["unit_rarity"].ToInt();
      if (_json.Keys.Contains("battle_rarity"))
        this.BattleRarity = (ObscuredInt) _json["battle_rarity"].ToInt();
      if (_json.Keys.Contains("promotion_level"))
        this.PromotionLevel = (ePromotionLevel) _json["promotion_level"].ToInt();
      if (_json.Keys.Contains("power"))
        this.Power = (ObscuredInt) _json["power"].ToInt();
      if (_json.Keys.Contains("skin_data"))
      {
        JsonData _json1 = _json["skin_data"];
        if (_json1 != null)
          this.SkinData = new SkinData(_json1);
      }
      if (!_json.Keys.Contains("unique_equip_slot"))
        return;
      JsonData jsonData = _json["unique_equip_slot"];
      this.UniqueEquipSlot = new List<EquipSlot>();
      if (!jsonData.IsArray)
        return;
      int index = 0;
      for (int count = jsonData.Count; index < count; ++index)
        this.UniqueEquipSlot.Add(new EquipSlot(jsonData[index]));
    }*/
  }
}
