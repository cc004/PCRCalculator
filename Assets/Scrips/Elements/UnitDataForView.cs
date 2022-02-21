// Decompiled with JetBrains decompiler
// Type: Elements.UnitDataForView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll


//using LitJson;

namespace Elements
{
  public class UnitDataForView
  {
    public int Id { get; private set; }

    public int UnitLevel { get; private set; }

    public int UnitRarity { get; private set; }

    public int BattleRarity { get; private set; }

    public ePromotionLevel PromotionLevel { get; private set; }

    public int Power { get; private set; }

    //public SkinData SkinData { get; private set; }

    //public List<EquipSlot> UniqueEquipSlot { get; private set; }

    public void SetUnitLevel(int _unitLevel) => UnitLevel = _unitLevel;

    //public void SetSkinData(SkinData _skinData) => this.SkinData = _skinData;

    //public void SetUniqueEquipSlot(List<EquipSlot> _uniqueEquipSlot) => this.UniqueEquipSlot = _uniqueEquipSlot;
    /*
    private void initializeUnitDataForView()
    {
      this.Id = (int) 0;
      this.UnitLevel = (int) 0;
      this.UnitRarity = (int) 0;
      this.BattleRarity = (int) 0;
      this.PromotionLevel = ePromotionLevel.INVALID_VALUE;
      this.Power = (int) 0;
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
      this.Id = (int) _id;
      this.UnitLevel = (int) _unitLevel;
      this.UnitRarity = (int) _unitRarity;
      this.BattleRarity = (int) _battleRarity;
      this.PromotionLevel = _promotionLevel;
      this.Power = (int) _power;
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
      this.Id = (int) _json["id"].ToInt();
      if (_json.Keys.Contains("unit_level"))
        this.UnitLevel = (int) _json["unit_level"].ToInt();
      if (_json.Keys.Contains("unit_rarity"))
        this.UnitRarity = (int) _json["unit_rarity"].ToInt();
      if (_json.Keys.Contains("battle_rarity"))
        this.BattleRarity = (int) _json["battle_rarity"].ToInt();
      if (_json.Keys.Contains("promotion_level"))
        this.PromotionLevel = (ePromotionLevel) _json["promotion_level"].ToInt();
      if (_json.Keys.Contains("power"))
        this.Power = (int) _json["power"].ToInt();
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
