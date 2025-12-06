using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Elements;
using PCRCaculator.SQL;
using PCRCaculator.Guild;

namespace PCRCaculator
{
    public class TeamSkillTreeManager : MonoBehaviour
    {
#if ENABLE_THREADING
        [ThreadStatic]
#endif
        public static TeamSkillTreeManager Instance;
        public GameObject talentSkillPrefab;
        public RectTransform skillTreeContainer;
        public ScrollRect scrollRect;
        public InputField masterPointInput;
        private Dictionary<int, TeamSkillNode> NodeDataDict => MainManager.Instance.TeamSkillNodeDic;
        private Dictionary<int, TeamSkillEnhanceData> enhanceDataDict;
        private Dictionary<int, List<int>> nodeChildrenDict;
        private Dictionary<int, TeamSkillTreeNode> NodeUIDict = new();
        private Dictionary<int, param_type> paramTypeDict;
        private GuildPlayerGroupData PlayerGroup => GuildManager.Instance?.SettingData?.GetCurrentPlayerGroup();
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
        private const float SPACING_X = 110f;
        private const float SPACING_Y = 80f;
        private const float PADDING = 50f;
        private bool isUpdatingSkills = false;
        public void Awake()
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
        }
        void OnEnable()
        {
            // GuildManager.LoadAddedPlayerData();
            skillLevels = PlayerGroup.teamSkill;
            UpdateAllNodeUI();
            UpdateTalentInfoData();
            SetupMasterPointInput();

        }
        public void ReLoad()
        {
            GuildManager.LoadAddedPlayerData();
            if ((PlayerGroup.talentLevel?.Count ?? 0) == 0 &&
                (PlayerGroup.talentSkill?.Count ?? 0) == 0 &&
                (PlayerGroup.teamSkill?.Count ?? 0) == 0)
            {
                PlayerGroup.teamSkill = new Dictionary<int, int>();
            }
            OnEnable();
        }
        public void LoadGlobalSettings()
        {
            PlayerGroup.teamSkill = MainManager.Instance.PlayerSetting.teamSkill;
            OnEnable();
        }
        public void InitializeData()
        {
            enhanceDataDict = MainManager.Instance.TeamSkillEnhanceDataDic;
            paramTypeDict = MainManager.Instance.ParamTypeDic;
            skillLevels = PlayerGroup.teamSkill;
            BuildNodeRelationships();
            UpdateTalentInfoData();
        }
        private void SetupMasterPointInput()
        {
            if (masterPointInput != null)
            {
                masterPointInput.onValueChanged.AddListener(OnMasterPointInputChanged);
                UpdateMasterPointInput();
            }
        }
        private void OnMasterPointInputChanged(string value)
        {
            if (int.TryParse(value, out int node_id))
            {
                int maxNodeId = NodeDataDict.Keys.Max();
                if (node_id < maxNodeId && node_id > 0)
                {
                    EnhanceSkill(node_id, 1);
                }
                else if (node_id == 0)
                {
                    EnhanceSkill(1, 0);
                }
                else
                {
                    UpdateMasterPointInput();
                }
            }
        }
        private int GetMasterNodeId()
        {
            if (skillLevels.Count > 0)
            {
                var enhanceSkills = skillLevels.Where(kvp => kvp.Value > 0).Select(kvp => kvp.Key);
                if (enhanceSkills.Any())
                {
                    return enhanceSkills.Max();
                }
            }
            return 0;
        }
        public void UpdateMasterPointInput()
        {
            if (masterPointInput != null)
            {
                int masterNodeId = GetMasterNodeId();
                masterPointInput.text = masterNodeId.ToString();
            }
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
                if (node.pre_node_id > 0 && nodeChildrenDict.ContainsKey(node.pre_node_id))
                    nodeChildrenDict[node.pre_node_id].Add(node.node_id);
            }
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
                if (NodeDataDict.TryGetValue(nodeId, out TeamSkillNode node))
                {
                    if (node.pre_node_id > 0)
                        queue.Enqueue(node.pre_node_id);
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
                        if (NodeDataDict.TryGetValue(currentId, out TeamSkillNode parentNode))
                        {
                            if (parentNode.pre_node_id > 0 && !relatedNodes.Contains(parentNode.pre_node_id))
                                queue.Enqueue(parentNode.pre_node_id);
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

        private void BuildSkillTree()
        {
            ClearSkillTree();

            var nodesToShow = NodeDataDict.Values
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

        private void CreateSkillNode(TeamSkillNode nodeData)
        {
            GameObject nodeObj = Instantiate(talentSkillPrefab, skillTreeContainer);
            TeamSkillTreeNode nodeUI = nodeObj.GetComponent<TeamSkillTreeNode>();

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
            string iconName = $"team_node_icon_{icon_id:D6}";

            Sprite icon = Resources.Load<Sprite>($"nodeicon/{iconName}");

            return icon;
        }

        private int GetMaxSkillLevel(int nodeId)
        {
            if (nodeId == 0 || !NodeDataDict.TryGetValue(nodeId, out TeamSkillNode node) || node.enhance_level_ids == null)
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

                // 1. 设置祖先节点为最大等级
                foreach (int ancestorId in GetRelatedNodes(nodeId, true))
                {
                    int maxLevel = GetMaxSkillLevel(ancestorId);
                    SetSkillLevel(ancestorId, maxLevel);
                }

                // 2. 设置当前节点等级
                SetSkillLevel(nodeId, newLevel);

                // 3. 设置后代节点为 0 级
                foreach (int descendantId in GetRelatedNodes(nodeId, false))
                {
                    SetSkillLevel(descendantId, 0);
                }
                UpdateAllNodeUI();
                UpdateTalentInfoData();
                UpdateMasterPointInput();
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
                TeamSkillTreeNode nodeUI = kvp.Value;

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

        public Dictionary<int, int> GetActiveTeamSkills()
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
                TeamEnhanceParameter.DataType.TeamSkill);
        }
        public void SaveTeamSkills(bool global = false)
        {
            var activeSkills = GetActiveTeamSkills();
            if (global) MainManager.Instance.PlayerSetting.teamSkill = activeSkills; //公共
            PlayerGroup.teamSkill = activeSkills; //队内
            GuildManager.Instance.SaveDataToJson();
        }

        public void SetSkillLevels(Dictionary<int, int> newSkillLevels)
        {
            skillLevels.Clear();
            foreach (var kvp in newSkillLevels)
            {
                skillLevels[kvp.Key] = kvp.Value;
            }
        }
    }
}
