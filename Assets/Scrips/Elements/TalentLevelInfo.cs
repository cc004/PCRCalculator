using UnityEngine;

namespace Elements
{
    [System.Serializable]
    public class TalentLevelInfo
    {
        public int talent_id { get; private set; }
        public float total_point { get; private set; }

        // public TalentLevelInfo()
        // {
        //     this.talent_id = 0;
        //     this.total_point = 0;
        // }

        // public TalentLevelInfo(string jsonText)
        // {
        //     this.talent_id = 0;
        //     this.total_point = 0;
        //     JsonUtility.FromJsonOverwrite(jsonText, this);
        // }

        // public void ParseTalentLevelInfo(string jsonText)
        // {
        //     JsonUtility.FromJsonOverwrite(jsonText, this);
        // }

        public void SetTalentId(int _talentId)
        {
            this.talent_id = _talentId;
        }

        public void SetTotalPoint(float _totalPoint)
        {
            this.total_point = _totalPoint;
        }

        // private void initializeTalentLevelInfo()
        // {
        //     this.talent_id = 0;
        //     this.total_point = 0;
        // }
    }
}