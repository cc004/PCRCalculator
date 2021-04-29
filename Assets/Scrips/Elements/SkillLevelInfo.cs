// Decompiled with JetBrains decompiler
// Type: Elements.SkillLevelInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using LitJson;

namespace Elements
{
    public class SkillLevelInfo
    {
        public ObscuredInt SkillId { get; private set; }

        public ObscuredInt SkillLevel { get; private set; }

        public ObscuredInt SlotNumber { get; private set; }

        public void SetSkillId(int _skillId) => this.SkillId = (ObscuredInt)_skillId;

        public void SetSkillLevel(int _skillLevel) => this.SkillLevel = (ObscuredInt)_skillLevel;

        public void SetSlotNumber(int _slotNumber) => this.SlotNumber = (ObscuredInt)_slotNumber;

        private void initializeSkillLevelInfo()
        {
            this.SkillId = (ObscuredInt)0;
            this.SkillLevel = (ObscuredInt)0;
            this.SlotNumber = (ObscuredInt)0;
        }

        public SkillLevelInfo() => this.initializeSkillLevelInfo();

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
          this.SkillId = (ObscuredInt) _json["skill_id"].ToInt();
          this.SkillLevel = (ObscuredInt) _json["skill_level"].ToInt();
          if (!_json.Keys.Contains("slot_number"))
            return;
          this.SlotNumber = (ObscuredInt) _json["slot_number"].ToInt();
        }*/
    }
}
