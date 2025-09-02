using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using PCRCaculator.SQL;

namespace PCRCaculator
{
    public abstract class SkillTreeManager<TNode, TEnhanceData, TTreeNode> : MonoBehaviour
        where TNode : class
        where TEnhanceData : class
        where TTreeNode : Component
    {
        public static SkillTreeManager<TNode, TEnhanceData, TTreeNode> Instance;
        public GameObject talentSkillPrefab;
        public RectTransform skillTreeContainer;
        public ScrollRect scrollRect;
        
        protected Dictionary<int, TNode> nodeDataDict;
        protected Dictionary<int, TEnhanceData> enhanceDataDict;
        protected Dictionary<int, List<int>> nodeChildrenDict;
        protected Dictionary<int, TTreeNode> nodeUIDict;
        protected Dictionary<int, param_type> paramTypeDict;
        protected Dictionary<int, int> skillLevels = new Dictionary<int, int>();
        protected float maxNodeY = 0f;
        
        protected static readonly Dictionary<int, string> talentTypeMap = new Dictionary<int, string>
        {
            { 1, "火属性" },
            { 2, "水属性" },
            { 3, "风属性" },
            { 4, "光属性" },
            { 5, "暗属性" }
        };
        
        // 布局常量
        protected float SPACING_X = 50f; // 子类可以覆盖
        protected const float SPACING_Y = 80f;
        protected const float PADDING = 50f;
        protected bool isUpdatingSkills = false;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            InitializeData();
        }

        protected virtual void Start()
        {
            InitializeData();
            BuildSkillTree();
            SetScrollViewToBottom();
        }

        public abstract void InitializeData();
        protected abstract void BuildNodeRelationships();
        protected abstract string GetElementTypeText(List<int> talentIds);
        protected abstract string GetEnhanceValueText(int enhanceId);
        protected abstract List<int> GetRelatedNodes(int nodeId, bool getAncestors);
        protected abstract void BuildSkillTree();
        protected abstract void CreateSkillNode(TNode nodeData);
        protected abstract int GetMaxSkillLevel(int nodeId);
        protected abstract void UpdateAllNodeUI();
        protected abstract void UpdateTalentInfoData();

        protected void SetScrollViewToBottom()
        {
            if (scrollRect == null)
                scrollRect = GetComponentInParent<ScrollRect>();

            if (scrollRect != null)
                scrollRect.normalizedPosition = new Vector2(0, 0);
        }

        protected void AdjustContentSize()
        {
            if (skillTreeContainer == null) return;

            float maxX = 0f, maxY = 0f;

            foreach (var nodeUI in nodeUIDict.Values)
            {
                RectTransform nodeRect = nodeUI.GetComponent<RectTransform>();
                Vector2 pos = nodeRect.anchoredPosition;

                float rightX = pos.x + nodeRect.rect.width / 2;
                float topY = pos.y + nodeRect.rect.height / 2;

                maxX = Mathf.Max(maxX, rightX);
                maxY = Mathf.Max(maxY, topY);
            }

            skillTreeContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxX + PADDING);
            skillTreeContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxY + PADDING);
        }

        protected void ClearSkillTree()
        {
            foreach (var nodeUI in nodeUIDict.Values)
                Destroy(nodeUI.gameObject);

            nodeUIDict.Clear();
        }

        protected Sprite GetSkillIcon(int nodeId)
        {
            // 获取技能图标
            return null;
        }

        protected void SetSkillLevel(int nodeId, int level)
        {
            if (nodeId > 0)
                skillLevels[nodeId] = level;
        }

        protected bool IsNodeUnlocked(int nodeId)
        {
            // 实现解锁状态判断逻辑
            return true;
        }

        public int GetSkillLevel(int nodeId)
        {
            return nodeId > 0 && skillLevels.TryGetValue(nodeId, out int level) ? level : 0;
        }

        public abstract Dictionary<int, int> GetActiveSkills();
        public abstract List<int> GetActivatedEnhanceIds();
    }
}