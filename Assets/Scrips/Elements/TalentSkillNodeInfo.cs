using UnityEngine;

namespace Elements
{
    [System.Serializable]
    public class TalentSkillNodeInfo
    {
        // 属性
        public int node_id { get; private set; }
        public int enhance_level { get; private set; }

        // 方法
        public TalentSkillNodeInfo()
        {
            this.node_id = 0;
            this.enhance_level = 0;
        }

        // 修改构造函数参数为 string jsonText
        public TalentSkillNodeInfo(string jsonText)
        {
            this.node_id = 0;
            this.enhance_level = 0;
            ParseTalentSkillNodeInfo(jsonText);
        }

        public void ParseTalentSkillNodeInfo(string jsonText)
        {
            JsonUtility.FromJsonOverwrite(jsonText, this);
        }

        public void SetNodeId(int _nodeId)
        {
            this.node_id = _nodeId;
        }

        public void SetEnhanceLevel(int _enhanceLevel)
        {
            this.enhance_level = _enhanceLevel;
        }

        private void initializeTalentSkillNodeInfo()
        {
            this.node_id = 0;
            this.enhance_level = 0;
        }
    }
}