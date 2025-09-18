using UnityEngine;

namespace Elements
{
    [System.Serializable]
    public class TeamSkillNodeInfo
    {
        // 属性
        public int node_id { get; private set; }
        public int enhance_level { get; private set; }

        // 构造函数
        public TeamSkillNodeInfo()
        {
            this.node_id = 0;
            this.enhance_level = 0;
        }

        // 修改构造函数参数为 string jsonText
        public TeamSkillNodeInfo(string jsonText)
        {
            this.node_id = 0;
            this.enhance_level = 0;
            ParseTeamSkillNodeInfo(jsonText);
        }

        // 解析 JSON 数据
        public void ParseTeamSkillNodeInfo(string jsonText)
        {
            JsonUtility.FromJsonOverwrite(jsonText, this);
        }

        // 设置方法
        public void SetNodeId(int _nodeId)
        {
            this.node_id = _nodeId;
        }

        public void SetEnhanceLevel(int _enhanceLevel)
        {
            this.enhance_level = _enhanceLevel;
        }

        // 初始化方法
        private void initializeTeamSkillNodeInfo()
        {
            this.node_id = 0;
            this.enhance_level = 0;
        }
    }
}