// Decompiled with JetBrains decompiler
// Type: Elements.SkillLevelInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll



//using LitJson;

namespace Elements
{
    public class SkillLevelInfo
    {
        public int SkillId { get; private set; }

        public int SkillLevel { get; private set; }

        public int SlotNumber { get; private set; }

        public void SetSkillId(int _skillId) => SkillId = _skillId;

        public void SetSkillLevel(int _skillLevel) => SkillLevel = _skillLevel;

        public void SetSlotNumber(int _slotNumber) => SlotNumber = _slotNumber;

        private void initializeSkillLevelInfo()
        {
            SkillId = 0;
            SkillLevel = 0;
            SlotNumber = 0;
        }

        public SkillLevelInfo() => initializeSkillLevelInfo();

        public SkillLevelInfo(int skillId,int skillLevel,int slotNumber)
        {
            SkillId = skillId;
            SkillLevel = skillLevel;
            SlotNumber = slotNumber;
        }
        /*public SkillLevelInfo(JsonData _json)
    {
      this.initializeSkillLevelInfo();
      this.ParseSkillLevelInfo(_json);
    }*/

        /*public void ParseSkillLevelInfo(JsonData _json)
        {
          if (_json.Count == 0)
            return;
          this.SkillId = (int) _json["skill_id"].ToInt();
          this.SkillLevel = (int) _json["skill_level"].ToInt();
          if (!_json.Keys.Contains("slot_number"))
            return;
          this.SlotNumber = (int) _json["slot_number"].ToInt();
        }*/
    }
}
