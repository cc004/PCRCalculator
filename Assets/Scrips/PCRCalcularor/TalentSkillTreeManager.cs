using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Elements;
using PCRCaculator.SQL;
namespace PCRCaculator
{
    public class TalentSkillTreeManager : MonoBehaviour
    {
        public static TalentSkillTreeManager Instance;
        public GameObject talentSkillPrefab;
        public RectTransform skillTreeContainer;
        public ScrollRect scrollRect;
        public int pageNumb = 1;
        public Button prePageButton;
        public Button nextPageButton;
        public Text PageText;

        private Dictionary<int, TalentSkillNode> nodeDataDict;
        private Dictionary<int, TalentSkillEnhanceData> enhanceDataDict;
        private Dictionary<int, List<int>> nodeChildrenDict;
        private Dictionary<int, TalentSkillTreeNode> nodeUIDict;
        private Dictionary<int, param_type> paramTypeDict;

        private Dictionary<int, int> skillLevels = new Dictionary<int, int>();
        private float maxNodeY = 0f;
        private static readonly Dictionary<int, string> talentTypeMap = new Dictionary<int, string>
    {
        { 1, "火属性" },
        { 2, "水属性" },
        { 3, "风属性" },
        { 4, "光属性" },
        { 5, "暗属性" }
    };
        // 布局常量
        private const float SPACING_X = 35f;
        private const float SPACING_Y = 80f;
        private const float PADDING = 50f;
        private bool isUpdatingSkills = false;
        private void Awake()
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
        void Start()
        {
            InitializeData();
            BuildSkillTree();
            SetScrollViewToBottom();
            UpdatePageText();
        }

        public void InitializeData()
        {
            nodeDataDict = MainManager.Instance.TalentSkillNodeDic;
            enhanceDataDict = MainManager.Instance.TalentSkillEnhanceDataDic;
            nodeUIDict = new Dictionary<int, TalentSkillTreeNode>();
            paramTypeDict = MainManager.Instance.paramTypeDic;
            if (MainManager.Instance.PlayerSetting.talentSkillDic != null)
            {
                foreach (var kvp in MainManager.Instance.PlayerSetting.talentSkillDic)
                {
                    // 确保节点存在后再添加
                    if (nodeDataDict.ContainsKey(kvp.Key))
                    {
                        skillLevels[kvp.Key] = kvp.Value;
                    }
                }
            }
            BuildNodeRelationships();
            UpdateTalentInfoData();
        }

        private void BuildNodeRelationships()
        {
            nodeChildrenDict = new Dictionary<int, List<int>>();

            // 初始化子节点列表
            foreach (var node in nodeDataDict.Values)
            {
                if (node.node_id > 0)
                    nodeChildrenDict[node.node_id] = new List<int>();
            }

            // 建立父子关系
            foreach (var node in nodeDataDict.Values)
            {
                if (node.node_id == 0) continue;

                foreach (int parentId in node.pre_node_ids)
                {
                    if (parentId > 0 && nodeChildrenDict.ContainsKey(parentId))
                        nodeChildrenDict[parentId].Add(node.node_id);
                }
            }
        }
        private List<int> GetAllNodesFromPreviousPages(int currentPage)
        {
            return nodeDataDict.Values
                .Where(n => n.page_num < currentPage && n.node_id > 0)
                .Select(n => n.node_id)
                .ToList();
        }
        private List<int> GetAllNodesFromNextPages(int currentPage)
        {
            return nodeDataDict.Values
                .Where(n => n.page_num > currentPage && n.node_id > 0)
                .Select(n => n.node_id)
                .ToList();
        }
        private string GetElementTypeText(List<int> talentIds)
        {
            if (talentIds == null || talentIds.Count == 0) return "";

            // 去重并过滤无效 ID
            var uniqueTypes = talentIds.Where(id => id > 0).Distinct().ToList();

            // 判断是否包含全部五种属性
            if (uniqueTypes.Count == 5 && uniqueTypes.All(id => talentTypeMap.ContainsKey(id)))
                return "全属性";

            // 否则返回第一个有效属性
            if (uniqueTypes.Count > 0 && talentTypeMap.TryGetValue(uniqueTypes[0], out string text))
                return text;

            return "";
        }
        private string GetEnhanceValueText(int enhanceId)
        {
            if (!enhanceDataDict.TryGetValue(enhanceId, out var data))
                return "?????";

            int parameterType = data.parameter_type;

            // 从 paramTypeDict 获取对应的参数配置
            if (!paramTypeDict.TryGetValue(parameterType, out var param))
                return "?????";

            int ratio = param.ratio;
            int isPercentage = param.is_percentage;

            float value;

            // 根据比率计算数值
            if (ratio > 0)
                value = (float)data.enhance_value / ratio;
            else
                value = data.enhance_value;

            // 判断是否为百分比格式
            if (isPercentage == 1)
                return $"{value * 100}%"; // 百分比保留两位小数

            // 非百分比格式，直接返回数值（可根据需要控制小数位）
            return value.ToString("F0");
        }
        private List<int> GetRelatedNodes(int nodeId, bool getAncestors)
        {
            List<int> relatedNodes = new List<int>();
            if (nodeId == 0) return relatedNodes;

            Queue<int> queue = new Queue<int>();

            // 初始化队列
            if (getAncestors)
            {
                if (nodeDataDict.TryGetValue(nodeId, out TalentSkillNode node))
                {
                    foreach (int parentId in node.pre_node_ids)
                    {
                        if (parentId > 0)
                            queue.Enqueue(parentId);
                    }
                }
            }
            else // 获取后代节点
            {
                if (nodeChildrenDict.TryGetValue(nodeId, out List<int> children))
                {
                    foreach (int child in children)
                    {
                        if (child > 0)
                            queue.Enqueue(child);
                    }
                }
            }

            // BFS遍历
            while (queue.Count > 0)
            {
                int currentId = queue.Dequeue();
                if (currentId == 0) continue;

                if (!relatedNodes.Contains(currentId))
                {
                    relatedNodes.Add(currentId);

                    if (getAncestors)
                    {
                        if (nodeDataDict.TryGetValue(currentId, out TalentSkillNode parentNode))
                        {
                            foreach (int parentId in parentNode.pre_node_ids)
                            {
                                if (parentId > 0 && !relatedNodes.Contains(parentId))
                                    queue.Enqueue(parentId);
                            }
                        }
                    }
                    else
                    {
                        if (nodeChildrenDict.TryGetValue(currentId, out List<int> subChildren))
                        {
                            foreach (int child in subChildren)
                            {
                                if (child > 0 && !relatedNodes.Contains(child))
                                    queue.Enqueue(child);
                            }
                        }
                    }
                }
            }

            return relatedNodes;
        }
        private int GetTotalPages()
        {
            if (nodeDataDict == null || nodeDataDict.Count == 0)
                return 1;

            // 取所有节点中的最大 page_num
            return nodeDataDict.Values
                .Where(n => n.page_num > 0)
                .Max(n => n.page_num);
        }
        private void UpdatePageText()
        {
            int totalPages = GetTotalPages();
            PageText.text = $"{pageNumb}/{totalPages}";
        }
        private void BuildSkillTree()
        {
            ClearSkillTree();

            var nodesToShow = nodeDataDict.Values
                .Where(n => n.page_num == pageNumb)
                .OrderBy(n => n.pos_x)
                .ThenBy(n => n.pos_y)
                .ToList();

            foreach (var node in nodesToShow)
                CreateSkillNode(node);

            AdjustContentSize();
            Canvas.ForceUpdateCanvases();
            SetScrollViewToBottom();
        }

        private void SetScrollViewToBottom()
        {
            if (scrollRect == null)
                scrollRect = GetComponentInParent<ScrollRect>();

            if (scrollRect != null)
                scrollRect.normalizedPosition = new Vector2(0, 0);
        }

        private void AdjustContentSize()
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

        private void ClearSkillTree()
        {
            foreach (var nodeUI in nodeUIDict.Values)
                Destroy(nodeUI.gameObject);

            nodeUIDict.Clear();
        }

        private void CreateSkillNode(TalentSkillNode nodeData)
        {
            GameObject nodeObj = Instantiate(talentSkillPrefab, skillTreeContainer);
            TalentSkillTreeNode nodeUI = nodeObj.GetComponent<TalentSkillTreeNode>();

            if (nodeUI != null)
            {
                RectTransform rectTransform = nodeObj.GetComponent<RectTransform>();
                Vector2 position = new Vector2(nodeData.pos_x * SPACING_X, nodeData.pos_y * SPACING_Y);
                rectTransform.anchoredPosition = position;

                // 更新最大 Y 坐标
                float nodeBottomY = position.y - rectTransform.sizeDelta.y / 2;
                if (nodeBottomY < -maxNodeY)
                    maxNodeY = -nodeBottomY;

                // 设置节点数据
                int currentLevel = GetSkillLevel(nodeData.node_id);
                int maxLevel = GetMaxSkillLevel(nodeData.node_id);
                string title_text = nodeData.title_text;
                int firstEnhanceLevelId = nodeData.enhance_level_ids.Keys.FirstOrDefault();
                enhanceDataDict.TryGetValue(nodeData.enhance_level_ids[firstEnhanceLevelId][0], out var enhanceData);
                string TalentType = GetElementTypeText(enhanceData.talent_ids);
                int enhanceId = currentLevel == 0 ? nodeData.enhance_level_ids[firstEnhanceLevelId][0] : nodeData.enhance_level_ids[firstEnhanceLevelId][0];
                string enhanceValue = currentLevel == 0 ? "0%" : GetEnhanceValueText(enhanceId);
                nodeUI.SetNodeData(nodeData.node_id, GetSkillIcon(nodeData.node_id), currentLevel, maxLevel, TalentType, title_text, enhanceValue);

                // 添加到字典中
                nodeUIDict[nodeData.node_id] = nodeUI;
            }
        }

        private Sprite GetSkillIcon(int nodeId)
        {
            // 获取技能图标
            return null;
        }

        private int GetMaxSkillLevel(int nodeId)
        {
            if (nodeId == 0 || !nodeDataDict.TryGetValue(nodeId, out TalentSkillNode node) || node.enhance_level_ids == null)
                return 0;

            return node.enhance_level_ids.Count;
        }

        public void EnhanceSkill(int nodeId, int newLevel)
        {
            if (isUpdatingSkills) return;

            isUpdatingSkills = true;

            try
            {
                if (!nodeDataDict.TryGetValue(nodeId, out var node))
                    return;

                int currentPage = node.page_num;

                List<int> previousPageNodes = GetAllNodesFromPreviousPages(currentPage);
                foreach (int prevNodeId in previousPageNodes)
                {
                    int maxLevel = GetMaxSkillLevel(prevNodeId);
                    SetSkillLevel(prevNodeId, maxLevel);
                }

                List<int> nextPageNodes = GetAllNodesFromNextPages(currentPage);
                foreach (int nextNodeId in nextPageNodes)
                {
                    SetSkillLevel(nextNodeId, 0);
                }

                foreach (int ancestorId in GetRelatedNodes(nodeId, true))
                {
                    int maxLevel = GetMaxSkillLevel(ancestorId);
                    SetSkillLevel(ancestorId, maxLevel);
                }

                SetSkillLevel(nodeId, newLevel);

                foreach (int descendantId in GetRelatedNodes(nodeId, false))
                {
                    SetSkillLevel(descendantId, 0);
                }

                UpdateAllNodeUI();
                UpdateTalentInfoData();
            }
            finally
            {
                isUpdatingSkills = false;
            }
        }
        private void UpdateAllNodeUI()
        {
            foreach (var kvp in nodeUIDict)
            {
                int nodeId = kvp.Key;
                TalentSkillTreeNode nodeUI = kvp.Value;

                int currentLevel = GetSkillLevel(nodeId);
                int maxLevel = GetMaxSkillLevel(nodeId);
                nodeDataDict.TryGetValue(nodeId, out var nodeData);
                int firstEnhanceLevelId = nodeData.enhance_level_ids.Keys.FirstOrDefault();
                int enhanceId = 0;

                if (nodeData.enhance_level_ids != null && nodeData.enhance_level_ids.Count > 0)
                {
                    var sortedLevelIds = nodeData.enhance_level_ids
                        .OrderBy(pair => pair.Key)
                        .ToList();

                    int levelIndex = currentLevel <= 0
                        ? 0
                        : Mathf.Clamp(currentLevel - 1, 0, sortedLevelIds.Count - 1);

                    int[] enhanceIds = sortedLevelIds[levelIndex].Value;
                    enhanceId = enhanceIds.Length > 0 ? enhanceIds[0] : 0;
                }
                string enhanceValue = currentLevel == 0 ? "0%" : GetEnhanceValueText(enhanceId);
                nodeUI.SetNodeData(currentLevel, maxLevel, enhanceValue);
                nodeUI.UpdateUIState(IsNodeUnlocked(nodeId), currentLevel > 0);
            }
        }

        private void SetSkillLevel(int nodeId, int level)
        {
            if (nodeId > 0)
                skillLevels[nodeId] = level;
        }

        private bool IsNodeUnlocked(int nodeId)
        {
            // 实现解锁状态判断逻辑
            return true;
        }

        public int GetSkillLevel(int nodeId)
        {
            return nodeId > 0 && skillLevels.TryGetValue(nodeId, out int level) ? level : 0;
        }
        public void PrePage()
        {
            if (pageNumb > 1)
            {
                pageNumb--;
                BuildSkillTree();
                UpdatePageText();
            }
        }
        public void NextPage()
        {
            int totalPages = GetTotalPages();
            if (pageNumb < totalPages)
            {
                pageNumb++;
                BuildSkillTree();
                UpdatePageText();
            }
        }
        public Dictionary<int, int> GetActiveTalentSkills()
        {
            Dictionary<int, int> activeSkills = new Dictionary<int, int>();

            foreach (var kvp in skillLevels)
            {
                int nodeId = kvp.Key;
                int level = kvp.Value;

                if (level > 0)
                {
                    activeSkills[nodeId] = level;
                }
            }

            return activeSkills;
        }
        public List<int> GetActivatedEnhanceIds()
        {
            List<int> activatedEnhanceIds = new List<int>();

            foreach (var nodeId in skillLevels.Keys)
            {
                int level = skillLevels[nodeId];
                if (level <= 0 || !nodeDataDict.TryGetValue(nodeId, out var nodeData))
                    continue;

                // 将 enhance_level_ids 转换为有序列表
                var sortedEnhanceLevelIds = nodeData.enhance_level_ids.OrderBy(kvp => kvp.Key).ToList();

                // 检查是否包含第 (level-1) 个元素
                if (sortedEnhanceLevelIds.Count >= level)
                {
                    // 获取第 (level-1) 个键值对
                    var kvp = sortedEnhanceLevelIds[level - 1];
                    // kvp.Value 是对应的 enhance_id 数组
                    int[] enhanceIds = kvp.Value;

                    // 将该数组中所有元素添加到激活列表中
                    foreach (int enhanceId in enhanceIds)
                    {
                        if (enhanceId > 0) // 确保 enhanceId 有效
                        {
                            activatedEnhanceIds.Add(enhanceId);
                        }
                    }
                }
            }

            return activatedEnhanceIds;
        }
        public void UpdateTalentInfoData()
        {
            // 获取当前 TalentSkillEnhance 数据
            List<int> activatedEnhanceIds = GetActivatedEnhanceIds();

            // 准备新数据字典
            var newData = new Dictionary<int, Dictionary<int, int>>();
            for (int i = 1; i <= 5; i++)
            {
                newData[i] = new Dictionary<int, int>();
            }

            // 填充新数据
            foreach (int enhanceId in activatedEnhanceIds)
            {
                if (!MainManager.Instance.TalentSkillEnhanceDataDic.TryGetValue(enhanceId, out var data))
                    continue;

                foreach (int talentId in data.talent_ids)
                {
                    if (talentId < 1 || talentId > 5)
                        continue;

                    // 添加参数到新数据中
                    var talentDict = newData[talentId];
                    int currentValue = talentDict.GetValueOrDefault(data.parameter_type);
                    talentDict[data.parameter_type] = currentValue + data.enhance_value;
                }
            }

            // 一次性设置所有数据并触发更新
            MainManager.Instance.TeamEnhanceData.SetData(TeamEnhanceParameter.DataType.TalentSkill, newData);
        }
        public void SaveTalentSkillsToPlayerSetting()
        {
            var activeSkills = GetActiveTalentSkills();

            // 存入当前激活的属性技能
            MainManager.Instance.PlayerSetting.talentSkillDic = activeSkills;
        }
    }
}
