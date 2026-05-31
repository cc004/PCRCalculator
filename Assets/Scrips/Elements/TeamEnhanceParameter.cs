using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using PCRCaculator;
using PCRCaculator.Guild;
using System.ComponentModel;

namespace Elements
{

    public enum eRoleParamType // TypeDefIndex: 3107
    {
        // Fields
        NONE = 0,
        [Description("HP")]
        HP = 1,
        [Description("攻击力")]
        ATTACK = 2,
        [Description("物防")]
        DEF = 3,
        [Description("魔防")]
        MAGIC_DEF = 4,
        [Description("暴击")]
        CRITICAL = 5,
        [Description("暴击伤害")]
        CRITICAL_DAMAGE_RATE = 6,
        [Description("伤害提升")]
        DAMAGE_UP_PERCENT = 7,
        [Description("状态异常命中")]
        ABNORMAL_STATE_DODGE_HIT = 8,
        [Description("状态异常抵抗")]
        ABNORMAL_STATE_DODGE_RESIST = 9,
        [Description("平A")]
        ATTACK_ENHANCE = 10,
        [Description("增益强化")]
        BUFF_ENHANCE = 11,
        [Description("减益强化")]
        DEBUFF_ENHANCE = 12,
        [Description("恢复强化")]
        HEAL_ENHANCE = 13,
        [Description("初动TP")]
        ENERGY_CHARGE = 14,
    }

    public class TeamEnhanceParameter
    {
        public Dictionary<int, int> talentLevelParameter = new Dictionary<int, int>();
        public Dictionary<int, Dictionary<eParamType, int>> talentBuffParameter = new Dictionary<int, Dictionary<eParamType, int>>();
        public Dictionary<int, Dictionary<eRoleParamType, int>> roleBuffParameter = new Dictionary<int, Dictionary<eRoleParamType, int>>();

        public Dictionary<int, Dictionary<int, int>> TalentSkillData { get; set; } = new Dictionary<int, Dictionary<int, int>>();
        public Dictionary<int, Dictionary<int, int>> TeamSkillData { get; set; } = new Dictionary<int, Dictionary<int, int>>();
        private GuildPlayerGroupData PlayerGroup => GuildManager.Instance.SettingData.GetCurrentPlayerGroup();
        private PlayerSetting PlayerSetting => MainManager.Instance.PlayerSetting;
        // 事件：当数据变化时触发 UI 更新等操作
        public event Action OnDataChanged;

        public TeamEnhanceParameter()
        {
            InitializeDictionaries();
        }

        private void InitializeDictionaries()
        {
            for (int i = 1; i <= 5; i++)
            {
                talentLevelParameter = new Dictionary<int, int>();
                TalentSkillData[i] = new Dictionary<int, int>();
                TeamSkillData[i] = new Dictionary<int, int>();
            }
        }

        /// <summary>
        /// 从PlayerSetting加载所有数据
        /// </summary>
        public void LoadFromPlayerSetting(bool isGlobal = false)
        {
            if (isGlobal)
            {
                MainManager.Instance.LoadPlayerSettings();
                PlayerGroup.talentLevel = PlayerSetting.talentLevel;
                PlayerGroup.talentSkill = PlayerSetting.talentSkill;
                PlayerGroup.teamSkill = PlayerSetting.teamSkill;
            }
            talentLevelParameter = PlayerGroup.talentLevel;

            var talentSkillLevels = ExpandTalentSkillLevels(PlayerGroup.talentSkill, MainManager.Instance.TalentSkillNodeDic);
            var teamSkillLevels = ExpandTeamSkillLevels(PlayerGroup.teamSkill, MainManager.Instance.TeamSkillNodeDic);

            UpdateTalentInfoData(talentSkillLevels, MainManager.Instance.TalentSkillNodeDic, DataType.TalentSkill);
            UpdateTalentInfoData(teamSkillLevels, MainManager.Instance.TeamSkillNodeDic, DataType.TeamSkill);
            UpdateRoleMasteryData();
            UpdateCombinedData();
        }

        public static Dictionary<int, int> ExpandTalentSkillLevels(
            Dictionary<int, int> savedSkillNodes,
            Dictionary<int, TalentSkillNode> nodeDataDict)
        {
            var result = new Dictionary<int, int>();
            if (savedSkillNodes == null || savedSkillNodes.Count == 0 || nodeDataDict == null)
                return result;

            var children = BuildTalentChildren(nodeDataDict);
            foreach (var kvp in savedSkillNodes
                .Where(kvp => kvp.Key > 0 && kvp.Value > 0)
                .OrderBy(kvp => nodeDataDict.TryGetValue(kvp.Key, out var node) ? node.page_num : int.MaxValue)
                .ThenBy(kvp => kvp.Key))
            {
                if (!nodeDataDict.TryGetValue(kvp.Key, out var node))
                    continue;

                foreach (var prevNode in nodeDataDict.Values.Where(n => n.node_id > 0 && n.page_num < node.page_num))
                    result[prevNode.node_id] = GetMaxSkillLevel(prevNode);

                foreach (var nextNode in nodeDataDict.Values.Where(n => n.node_id > 0 && n.page_num > node.page_num))
                    result[nextNode.node_id] = 0;

                foreach (int ancestorId in GetTalentAncestors(kvp.Key, nodeDataDict))
                {
                    if (nodeDataDict.TryGetValue(ancestorId, out var ancestor))
                        result[ancestorId] = GetMaxSkillLevel(ancestor);
                }

                result[kvp.Key] = kvp.Value;

                foreach (int descendantId in GetDescendants(kvp.Key, children))
                    result[descendantId] = 0;
            }

            return result;
        }

        public static Dictionary<int, int> ExpandTeamSkillLevels(
            Dictionary<int, int> savedSkillNodes,
            Dictionary<int, TeamSkillNode> nodeDataDict)
        {
            var result = new Dictionary<int, int>();
            if (savedSkillNodes == null || savedSkillNodes.Count == 0 || nodeDataDict == null)
                return result;

            var children = BuildTeamChildren(nodeDataDict);
            foreach (var kvp in savedSkillNodes
                .Where(kvp => kvp.Key > 0 && kvp.Value > 0)
                .OrderBy(kvp => kvp.Key))
            {
                if (!nodeDataDict.ContainsKey(kvp.Key))
                    continue;

                foreach (int ancestorId in GetTeamAncestors(kvp.Key, nodeDataDict))
                {
                    if (nodeDataDict.TryGetValue(ancestorId, out var ancestor))
                        result[ancestorId] = GetMaxSkillLevel(ancestor);
                }

                result[kvp.Key] = kvp.Value;

                foreach (int descendantId in GetDescendants(kvp.Key, children))
                    result[descendantId] = 0;
            }

            return result;
        }

        private static Dictionary<int, List<int>> BuildTalentChildren(Dictionary<int, TalentSkillNode> nodeDataDict)
        {
            var children = nodeDataDict.Values
                .Where(n => n.node_id > 0)
                .ToDictionary(n => n.node_id, _ => new List<int>());

            foreach (var node in nodeDataDict.Values)
            {
                if (node.node_id <= 0 || node.pre_node_ids == null)
                    continue;

                foreach (int parentId in node.pre_node_ids)
                {
                    if (parentId > 0 && children.TryGetValue(parentId, out var childList))
                        childList.Add(node.node_id);
                }
            }

            return children;
        }

        private static Dictionary<int, List<int>> BuildTeamChildren(Dictionary<int, TeamSkillNode> nodeDataDict)
        {
            var children = nodeDataDict.Values
                .Where(n => n.node_id > 0)
                .ToDictionary(n => n.node_id, _ => new List<int>());

            foreach (var node in nodeDataDict.Values)
            {
                if (node.node_id > 0 && node.pre_node_id > 0 && children.TryGetValue(node.pre_node_id, out var childList))
                    childList.Add(node.node_id);
            }

            return children;
        }

        private static List<int> GetTalentAncestors(int nodeId, Dictionary<int, TalentSkillNode> nodeDataDict)
        {
            var result = new List<int>();
            var queue = new Queue<int>();

            if (nodeDataDict.TryGetValue(nodeId, out var node) && node.pre_node_ids != null)
            {
                foreach (int parentId in node.pre_node_ids)
                {
                    if (parentId > 0)
                        queue.Enqueue(parentId);
                }
            }

            while (queue.Count > 0)
            {
                int currentId = queue.Dequeue();
                if (currentId <= 0 || result.Contains(currentId))
                    continue;

                result.Add(currentId);
                if (nodeDataDict.TryGetValue(currentId, out var parentNode) && parentNode.pre_node_ids != null)
                {
                    foreach (int parentId in parentNode.pre_node_ids)
                    {
                        if (parentId > 0 && !result.Contains(parentId))
                            queue.Enqueue(parentId);
                    }
                }
            }

            return result;
        }

        private static List<int> GetTeamAncestors(int nodeId, Dictionary<int, TeamSkillNode> nodeDataDict)
        {
            var result = new List<int>();
            var queue = new Queue<int>();

            if (nodeDataDict.TryGetValue(nodeId, out var node) && node.pre_node_id > 0)
                queue.Enqueue(node.pre_node_id);

            while (queue.Count > 0)
            {
                int currentId = queue.Dequeue();
                if (currentId <= 0 || result.Contains(currentId))
                    continue;

                result.Add(currentId);
                if (nodeDataDict.TryGetValue(currentId, out var parentNode) && parentNode.pre_node_id > 0 && !result.Contains(parentNode.pre_node_id))
                    queue.Enqueue(parentNode.pre_node_id);
            }

            return result;
        }

        private static List<int> GetDescendants(int nodeId, Dictionary<int, List<int>> children)
        {
            var result = new List<int>();
            var queue = new Queue<int>();

            if (children.TryGetValue(nodeId, out var childList))
            {
                foreach (int childId in childList)
                    queue.Enqueue(childId);
            }

            while (queue.Count > 0)
            {
                int currentId = queue.Dequeue();
                if (currentId <= 0 || result.Contains(currentId))
                    continue;

                result.Add(currentId);
                if (children.TryGetValue(currentId, out var nextChildren))
                {
                    foreach (int childId in nextChildren)
                    {
                        if (childId > 0 && !result.Contains(childId))
                            queue.Enqueue(childId);
                    }
                }
            }

            return result;
        }

        private static int GetMaxSkillLevel(TalentSkillNode node)
        {
            return node?.enhance_level_ids?.Count ?? 0;
        }

        private static int GetMaxSkillLevel(TeamSkillNode node)
        {
            return node?.enhance_level_ids?.Count ?? 0;
        }

        public List<int> GetActivatedEnhanceIds<T>(Dictionary<int, int> skillLevels, Dictionary<int, T> skillNodeDict)
            where T : class
        {
            List<int> activatedEnhanceIds = new List<int>();

            foreach (var nodeId in skillLevels.Keys)
            {
                int level = skillLevels[nodeId];
                if (level <= 0 || !skillNodeDict.TryGetValue(nodeId, out var nodeData))
                    continue;

                // 使用反射安全地获取 enhance_level_ids 属性
                var enhanceLevelIdsProp = typeof(T).GetProperty("enhance_level_ids");
                if (enhanceLevelIdsProp != null &&
                    enhanceLevelIdsProp.GetValue(nodeData) is Dictionary<int, int[]> enhanceLevelIds)
                {
                    var sortedEnhanceLevelIds = enhanceLevelIds.OrderBy(kvp => kvp.Key).ToList();

                    if (sortedEnhanceLevelIds.Count >= level)
                    {
                        var kvp = sortedEnhanceLevelIds[level - 1];
                        foreach (int enhanceId in kvp.Value)
                        {
                            if (enhanceId > 0)
                            {
                                activatedEnhanceIds.Add(enhanceId);
                            }
                        }
                    }
                }
            }

            return activatedEnhanceIds;
        }
        public void UpdateTalentInfoData<T>(Dictionary<int, int> skillLevels, Dictionary<int, T> skillNodeDict, DataType type)
            where T : class
        {
            List<int> activatedEnhanceIds = GetActivatedEnhanceIds(skillLevels, skillNodeDict);
            var newData = new Dictionary<int, Dictionary<int, int>>();
            for (int i = 1; i <= 5; i++)
            {
                newData[i] = new Dictionary<int, int>();
            }
            foreach (int enhanceId in activatedEnhanceIds)
            {
                if (type == DataType.TalentSkill)
                {
                    if (!MainManager.Instance.TalentSkillEnhanceDataDic.TryGetValue(enhanceId, out var data))
                        continue;
                    foreach (int talentId in data.talent_ids)
                    {
                        if (talentId < 1 || talentId > 5)
                            continue;
                        var talentDict = newData[talentId];
                        int currentValue = talentDict.GetValueOrDefault(data.parameter_type);
                        talentDict[data.parameter_type] = currentValue + data.enhance_value;
                    }
                }
                else if (type == DataType.TeamSkill)
                {
                    if (!MainManager.Instance.TeamSkillEnhanceDataDic.TryGetValue(enhanceId, out var data))
                        continue;
                    foreach (int talentId in data.talent_ids)
                        {
                            if (talentId < 1 || talentId > 5)
                                continue;
                            var talentDict = newData[talentId];
                            int currentValue = talentDict.GetValueOrDefault(data.parameter_type);
                            talentDict[data.parameter_type] = currentValue + data.enhance_value;
                        }
                }
            }
            SetData(type, newData);
        }

        /// <summary>
        /// 设置属性等级
        /// </summary>
        public void SetTalentLevels(Dictionary<int, int> levels)
        {
            foreach (var kvp in levels)
            {
                talentLevelParameter[kvp.Key] = kvp.Value;
            }
            OnDataChanged?.Invoke();
        }

        /// <summary>
        /// 获取属性等级
        /// </summary>
        public int GetTalentLevel(int talentId)
        {
            return talentLevelParameter.GetValueOrDefault(talentId, 0);
        }

        /// <summary>
        /// 获取属性攻击力（属性等级 * 20）
        /// </summary>
        public int GetAttributeAttackPower(int talentId)
        {
            return GetTalentLevel(talentId) * 20;
        }

        /// <summary>
        /// 清空所有数据
        /// </summary>
        public void Reset()
        {
            foreach (var dict in TalentSkillData.Values)
                dict.Clear();

            foreach (var dict in TeamSkillData.Values)
                dict.Clear();

            UpdateCombinedData();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        public void SetData(DataType type, Dictionary<int, Dictionary<int, int>> data)
        {
            var target = type == DataType.TalentSkill ? TalentSkillData : TeamSkillData;

            foreach (var dict in target.Values)
                dict.Clear();

            foreach (var talentKvp in data)
            {
                if (target.TryGetValue(talentKvp.Key, out var paramDict))
                {
                    foreach (var paramKvp in talentKvp.Value)
                    {
                        paramDict[paramKvp.Key] = paramKvp.Value;
                    }
                }
            }

            UpdateCombinedData();
        }

        /// <summary>
        /// 更新合并后的数据
        /// </summary>
        public void UpdateCombinedData()
        {
            talentBuffParameter = Merge(TalentSkillData, TeamSkillData);
            OnDataChanged?.Invoke();
        }

        /// <summary>
        /// 合并属性技能和大师技能数据
        /// </summary>
        public static Dictionary<int, Dictionary<eParamType, int>> Merge(
            Dictionary<int, Dictionary<int, int>> talentData,
            Dictionary<int, Dictionary<int, int>> teamData)
        {
            var merged = new Dictionary<int, Dictionary<eParamType, int>>();

            for (int i = 1; i <= 5; i++)
            {
                merged[i] = new Dictionary<eParamType, int>();

                // 合并talentData
                MergeData(merged[i], talentData, i);

                // 合并teamData
                MergeData(merged[i], teamData, i);
            }

            return merged;
        }
        private static void MergeData(Dictionary<eParamType, int> target, Dictionary<int, Dictionary<int, int>> source, int index)
        {
            if (source != null && source.TryGetValue(index, out var sourceParams))
            {
                foreach (var kvp in sourceParams)
                {
                    if (Enum.IsDefined(typeof(eParamType), kvp.Key))
                    {
                        eParamType paramType = (eParamType)kvp.Key;
                        target[paramType] = target.GetValueOrDefault(paramType, 0) + kvp.Value;
                    }
                }
            }
        }
        public Dictionary<eParamType, int> GetTalentEnhance(int talentId)
        {
            if (talentBuffParameter == null || !talentBuffParameter.ContainsKey(talentId))
                return null;

            return talentBuffParameter[talentId];
        }

        public int GetAdditiveAtkForTalentLevel(int talentId)
        {
            if (talentLevelParameter == null || !talentLevelParameter.ContainsKey(talentId))
                return 0;

            return talentLevelParameter[talentId] * 20;
        }

        public Dictionary<eParamType, int> GetRoleEnhance(int roleId, UnitCtrl owner)
        {
            if (this == null)
                return null;

            var result = new Dictionary<eParamType, int>();

            if (roleBuffParameter == null || !roleBuffParameter.TryGetValue(roleId, out var roleParams))
                return result;

            bool isPhysical = owner.AtkType != 2;

            foreach (var kvp in roleParams)
            {
                var roleParamType = kvp.Key;
                var value = kvp.Value;

                var paramType = GetParamType(roleParamType, isPhysical);

                result[paramType] = value;
            }

            return result;
        }
        /// <summary>
        /// 更新职业数据
        /// </summary>
        public void UpdateRoleMasteryData(Dictionary<int, (int slotLevel, int enhanceLevel)> roleMasteryData = null)
        {
            // 清空现有的roleBuffParameter数据
            foreach (var dict in roleBuffParameter.Values)
            {
                dict.Clear();
            }

            try
            {
                var roleMastery = roleMasteryData
                    ?? GuildManager.Instance?.SettingData?.GetCurrentPlayerGroup()?.roleMastery;
                if (roleMastery == null) return;

                // 遍历所有unit_role_id (假设是1-7)
                for (int unitRoleId = 1; unitRoleId <= 8; unitRoleId++)
                {
                    // 获取该unit_role_id对应的所有mastery_id
                    var masteryIds = MainManager.Instance.UnitRoleMasteryIdDic
                        .Where(kv => kv.Value.unit_role_id == unitRoleId)
                        .Select(kv => kv.Value.mastery_id)
                        .ToList();

                    // 为每个unit_role_id创建新的字典
                    var roleParams = new Dictionary<eRoleParamType, int>();

                    // 遍历所有mastery_id
                    foreach (int masteryId in masteryIds)
                    {
                        // 检查是否有该mastery_id的数据
                        if (roleMastery.ContainsKey(masteryId))
                        {
                            var (slotLevel, enhanceLevel) = roleMastery[masteryId];

                            // 获取强化数据
                            if (MainManager.Instance.UnitRoleMasterySlotEnhanceDataDic.TryGetValue(masteryId, out var slotEnhanceData))
                            {
                                // 获取对应等级和星级的增强数据
                                if (slotEnhanceData.slot_level_info?.ContainsKey(slotLevel) == true &&
                                    slotEnhanceData.slot_level_info[slotLevel].enhance_level_info?.ContainsKey(enhanceLevel) == true)
                                {
                                    var enhanceInfo = slotEnhanceData.slot_level_info[slotLevel].enhance_level_info[enhanceLevel];

                                    // 添加第一个参数类型和值
                                    if (enhanceInfo.role_param_type_1 > 0)
                                    {
                                        var paramType1 = (eRoleParamType)enhanceInfo.role_param_type_1;
                                        if (roleParams.ContainsKey(paramType1))
                                        {
                                            roleParams[paramType1] += enhanceInfo.enhance_value_1;
                                        }
                                        else
                                        {
                                            roleParams[paramType1] = enhanceInfo.enhance_value_1;
                                        }
                                    }

                                    // 添加第二个参数类型和值
                                    if (enhanceInfo.role_param_type_2 > 0)
                                    {
                                        var paramType2 = (eRoleParamType)enhanceInfo.role_param_type_2;
                                        if (roleParams.ContainsKey(paramType2))
                                        {
                                            roleParams[paramType2] += enhanceInfo.enhance_value_2;
                                        }
                                        else
                                        {
                                            roleParams[paramType2] = enhanceInfo.enhance_value_2;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // 将计算结果保存到roleBuffParameter中
                    roleBuffParameter[unitRoleId] = roleParams;
                }
            }
            catch (Exception ex)
            {
                // 如果出现异常，确保roleBuffParameter至少是空的字典
                for (int i = 1; i <= 7; i++)
                {
                    if (!roleBuffParameter.ContainsKey(i))
                    {
                        roleBuffParameter[i] = new Dictionary<eRoleParamType, int>();
                    }
                }
            }
        }
        
        private eParamType GetParamType(eRoleParamType roleParamType, bool isAtk)
        {
            switch (roleParamType)
            {
                case eRoleParamType.HP:
                    return eParamType.HP;

                case eRoleParamType.ATTACK:
                    return isAtk ? eParamType.ATK : eParamType.MAGIC_ATK;

                case eRoleParamType.DEF:
                    return eParamType.DEF;

                case eRoleParamType.MAGIC_DEF:
                    return eParamType.MAGIC_DEF;

                case eRoleParamType.CRITICAL:
                    return isAtk ? eParamType.PHYSICAL_CRITICAL : eParamType.MAGIC_CRITICAL;

                case eRoleParamType.CRITICAL_DAMAGE_RATE:
                    return isAtk ? eParamType.PHYSICAL_CRITICAL_DAMAGE_RATE : eParamType.MAGIC_CRITICAL_DAMAGE_RATE;

                case eRoleParamType.DAMAGE_UP_PERCENT:
                    return isAtk ? eParamType.PHYSICAL_DAMAGE_UP_PERCENT : eParamType.MAGIC_DAMAGE_UP_PERCENT;

                case eRoleParamType.ABNORMAL_STATE_DODGE_HIT:
                    return eParamType.ABNORMAL_STATE_DODGE_HIT;

                case eRoleParamType.ABNORMAL_STATE_DODGE_RESIST:
                    return eParamType.ABNORMAL_STATE_DODGE_RESIST;

                case eRoleParamType.ATTACK_ENHANCE:
                    return eParamType.ATTACK_ENHANCE;

                case eRoleParamType.BUFF_ENHANCE:
                    return eParamType.BUFF_ENHANCE;

                case eRoleParamType.DEBUFF_ENHANCE:
                    return eParamType.DEBUFF_ENHANCE;

                case eRoleParamType.HEAL_ENHANCE:
                    return eParamType.HEAL_ENHANCE;

                case eRoleParamType.ENERGY_CHARGE:
                    return eParamType.ENERGY_CHARGE;

                default:
                    return eParamType.NONE;
            }
        }

        /// <summary>
        /// 数据来源枚举
        /// </summary>
        public enum DataType
        {
            TalentSkill,
            TeamSkill
        }
    }

}
