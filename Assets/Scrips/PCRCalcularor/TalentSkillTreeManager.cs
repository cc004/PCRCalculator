using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Elements;
using PCRCaculator.SQL;
using PCRCaculator.Guild;

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

        private Dictionary<int, TalentSkillNode> NodeDataDict => MainManager.Instance.TalentSkillNodeDic;
        private Dictionary<int, TalentSkillEnhanceData> enhanceDataDict;
        public Dictionary<int, List<int>> nodeChildrenDict;
        private Dictionary<int, TalentSkillTreeNode> NodeUIDict = new();
        private Dictionary<int, param_type> paramTypeDict;
        public GuildSettingData SettingData => GuildManager.Instance.SettingData;
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
        private const float SPACING_X = 60f;
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
            UpdateTalentInfoData();
        }
        void Start()
        {
            BuildSkillTree();
            SetScrollViewToBottom();
            UpdatePageText();
        }
        void OnEnable()
        {
            // GuildManager.LoadAddedPlayerData();
            skillLevels = SettingData.GetCurrentPlayerGroup().talentSkill;
            UpdateAllNodeUI();
            UpdateTalentInfoData();
        }
        public void LoadGlobalSettings()
        {
            SettingData.GetCurrentPlayerGroup().talentSkill = MainManager.Instance.PlayerSetting.talentSkill;
            OnEnable();
        }
        public void ReLoad()
        {
            GuildManager.LoadAddedPlayerData();
            if ((SettingData.GetCurrentPlayerGroup().talentLevel?.Count ?? 0) > 0 ||
                (SettingData.GetCurrentPlayerGroup().talentSkill?.Count ?? 0) > 0 ||
                (SettingData.GetCurrentPlayerGroup().teamSkill?.Count ?? 0) > 0)
            {
                OnEnable();
            }
            else
            {
                MainManager.Instance.WindowMessage("初始队伍无数据可还原!");
            }
        }
        public void InitializeData()
        {
            enhanceDataDict = MainManager.Instance.TalentSkillEnhanceDataDic;
            paramTypeDict = MainManager.Instance.paramTypeDic;
            skillLevels = SettingData.GetCurrentPlayerGroup().talentSkill;
            BuildNodeRelationships();
            UpdateTalentInfoData();
        }

        private void BuildNodeRelationships()
        {
            nodeChildrenDict = new Dictionary<int, List<int>>();

            // 初始化子节点列表
            foreach (var node in NodeDataDict.Values)
            {
                if (node.node_id > 0)
                    nodeChildrenDict[node.node_id] = new List<int>();
            }

            // 建立父子关系
            foreach (var node in NodeDataDict.Values)
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
            return NodeDataDict.Values
                .Where(n => n.page_num < currentPage && n.node_id > 0)
                .Select(n => n.node_id)
                .ToList();
        }
        private List<int> GetAllNodesFromNextPages(int currentPage)
        {
            return NodeDataDict.Values
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
                if (NodeDataDict.TryGetValue(nodeId, out TalentSkillNode node))
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
                        if (NodeDataDict.TryGetValue(currentId, out TalentSkillNode parentNode))
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
            if (NodeDataDict == null || NodeDataDict.Count == 0)
                return 1;

            // 取所有节点中的最大 page_num
            return NodeDataDict.Values
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

            var nodesToShow = NodeDataDict.Values
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

            foreach (var nodeUI in NodeUIDict.Values)
            {
                RectTransform nodeRect = nodeUI.GetComponent<RectTransform>();
                Vector2 pos = nodeRect.anchoredPosition;

                float rightX = pos.x + nodeRect.rect.width / 2;
                float topY = pos.y + nodeRect.rect.height / 2;

                maxX = Mathf.Max(maxX, rightX);
                maxY = Mathf.Max(maxY, topY);
            }

            skillTreeContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxX);
            skillTreeContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxY + PADDING);
        }

        private void ClearSkillTree()
        {
            foreach (var nodeUI in NodeUIDict.Values)
                Destroy(nodeUI.gameObject);

            NodeUIDict.Clear();
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
                nodeUI.SetNodeData(nodeData.node_id, GetSkillIcon(nodeData.icon_id), currentLevel, maxLevel, TalentType, title_text, enhanceValue);

                // 添加到字典中
                NodeUIDict[nodeData.node_id] = nodeUI;
                UpdateAllNodeUI();
            }
        }

        private Sprite GetSkillIcon(int icon_id)
        {
            string iconName = $"node_icon_{icon_id:D6}";

            Sprite icon = Resources.Load<Sprite>($"nodeicon/{iconName}");

            return icon;
        }

        private int GetMaxSkillLevel(int nodeId)
        {
            if (nodeId == 0 || !NodeDataDict.TryGetValue(nodeId, out TalentSkillNode node) || node.enhance_level_ids == null)
                return 0;

            return node.enhance_level_ids.Count;
        }

        public void EnhanceSkill(int nodeId, int newLevel)
        {
            if (isUpdatingSkills) return;

            isUpdatingSkills = true;

            try
            {
                if (!NodeDataDict.TryGetValue(nodeId, out var node))
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
            foreach (var kvp in NodeUIDict)
            {
                int nodeId = kvp.Key;
                TalentSkillTreeNode nodeUI = kvp.Value;

                int currentLevel = GetSkillLevel(nodeId);
                int maxLevel = GetMaxSkillLevel(nodeId);
                NodeDataDict.TryGetValue(nodeId, out var nodeData);
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
        /// <summary>
        /// 获取最终强化节点的位置描述文本
        /// 格式：第a页 第b合流 左x[等级] 中y[等级] 右z[等级]
        /// </summary>
        /// <returns>节点位置描述文本</returns>
        public string GetFinalTalentSkillNodeDescription()
        {
            var talentSkill = SettingData.GetCurrentPlayerGroup().talentSkill;

            // 找到所有最终节点（已激活且子节点未激活或无子节点）
            var finalNodes = talentSkill
                .Where(node => node.Value > 0 &&
                               (!nodeChildrenDict.TryGetValue(node.Key, out var children) ||
                                !children.Any(childId => talentSkill.TryGetValue(childId, out int childLevel) && childLevel > 0)))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // 如果没有最终节点，返回提示信息
            if (finalNodes.Count == 0)
                return "未找到最终强化节点";

            // 找到最高页面编号上的最终节点
            var targetNode = finalNodes
                .Where(kvp => NodeDataDict.ContainsKey(kvp.Key))
                .OrderByDescending(kvp => NodeDataDict[kvp.Key].page_num)
                .First();

            if (!NodeDataDict.TryGetValue(targetNode.Key, out TalentSkillNode nodeData))
                return "未找到最终强化节点";

            int pageNum = nodeData.page_num;
            int targetNodeId = targetNode.Key;
            int targetNodeLevel = targetNode.Value;

            // 计算合流编号
            var confluenceNodes = NodeDataDict.Values
                .Where(n => n.page_num == pageNum &&
                           n.pre_node_ids?.Count(id => id > 0) > 1 &&
                           n.pre_node_ids.Where(id => id > 0).Distinct().Count() > 1)
                .OrderBy(n => n.node_id)
                .ToList();

            int confluenceNum = confluenceNodes.FindIndex(n => n.node_id >= targetNodeId) + 1;
            if (confluenceNum <= 0) confluenceNum = confluenceNodes.Count + 1;

            // 获取页面上左中右节点等级
            var pageNodeLevels = finalNodes
                .Where(kvp => NodeDataDict.TryGetValue(kvp.Key, out TalentSkillNode node) &&
                           node.page_num == pageNum && node.node_id > 0)
                .ToDictionary(kvp => NodeDataDict[kvp.Key].pos_x, kvp => kvp.Value);

            int leftLevel = pageNodeLevels.GetValueOrDefault(2, 0);
            int middleLevel = pageNodeLevels.GetValueOrDefault(5, 0);
            int rightLevel = pageNodeLevels.GetValueOrDefault(8, 0);

            // 计算在最近合流点之后的位置计数（左中右各有多少个节点）
            float targetNodeY = nodeData.pos_y;
            var precedingConfluenceNodes = confluenceNodes.Where(c => c.pos_y < targetNodeY).OrderByDescending(c => c.pos_y);

            float confluenceY = precedingConfluenceNodes.FirstOrDefault()?.pos_y ?? float.MinValue;

            // 计算在合流点之后的节点数量
            int lastConfluenceNodeId = precedingConfluenceNodes.FirstOrDefault()?.node_id ?? 0;

            var counts = NodeDataDict.Values
                .Where(n => n.page_num == pageNum &&
                           n.node_id >= lastConfluenceNodeId &&
                           n.pos_y > confluenceY &&
                           n.pos_y <= targetNodeY &&
                           targetNodeLevel > 0)
                .GroupBy(n => n.pos_x)
                .ToDictionary(g => g.Key, g => g.Count());

            int leftCount = counts.GetValueOrDefault(2, 0);
            int middleCount = counts.GetValueOrDefault(5, 0);
            int rightCount = counts.GetValueOrDefault(8, 0);

            // 构造返回文本
            return confluenceNodes.Any(n => n.node_id == targetNodeId)
                ? $"第{pageNum}页 第{confluenceNum}合流 交点[{targetNodeLevel}级]"
                : $"第{pageNum}页 第{confluenceNum}合流 左{leftCount}[{leftLevel}级] 中{middleCount}[{middleLevel}级] 右{rightCount}[{rightLevel}级]";
        }
        public List<int> GetActivatedEnhanceIds()
        {
            List<int> activatedEnhanceIds = new();

            foreach (var nodeId in skillLevels.Keys)
            {
                int level = skillLevels[nodeId];
                if (level <= 0 || !NodeDataDict.TryGetValue(nodeId, out var nodeData))
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
            MainManager.Instance.TeamEnhanceData.UpdateTalentInfoData(
                skillLevels,
                NodeDataDict,
                TeamEnhanceParameter.DataType.TalentSkill);
        }
        public void SaveTalentSkills(bool global = false)
        {
            var activeSkills = GetActiveTalentSkills();
            if (global) MainManager.Instance.PlayerSetting.talentSkill = activeSkills; //公共
            SettingData.GetCurrentPlayerGroup().talentSkill = activeSkills; //队内
            GuildManager.Instance.SaveDataToJson();
        }
    }
}
