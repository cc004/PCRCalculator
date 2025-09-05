using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using PCRCaculator;
using PCRCaculator.Guild;

namespace Elements
{

    public enum eRoleParamType // TypeDefIndex: 3107
    {
        // Fields
        NONE = 0,
        HP = 1,
        ATTACK = 2,
        DEF = 3,
        MAGIC_DEF = 4,
        CRITICAL = 5,
        CRITICAL_DAMAGE_RATE = 6,
        DAMAGE_UP_PERCENT = 7,
        ABNORMAL_STATE_DODGE_HIT = 8,
        ABNORMAL_STATE_DODGE_RESIST = 9,
        ATTACK_ENHANCE = 10,
        BUFF_ENHANCE = 11,
        DEBUFF_ENHANCE = 12,
        HEAL_ENHANCE = 13,
        ENERGY_CHARGE = 14,
    }

    public class TeamEnhanceParameter
    {
        public Dictionary<int, int> talentLevelParameter = new Dictionary<int, int>();
        public Dictionary<int, Dictionary<eParamType, int>> talentBuffParameter = new Dictionary<int, Dictionary<eParamType, int>>();
        public Dictionary<int, Dictionary<eRoleParamType, int>> roleBuffParameter = new Dictionary<int, Dictionary<eRoleParamType, int>>();

        public Dictionary<int, Dictionary<int, int>> TalentSkillData { get; set; } = new Dictionary<int, Dictionary<int, int>>();
        public Dictionary<int, Dictionary<int, int>> TeamSkillData { get; set; } = new Dictionary<int, Dictionary<int, int>>();

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
            if (GuildManager.Instance.SettingData.GetCurrentPlayerGroup().playerData.playrCharacters.Count == 0 || isGlobal)
            {
                MainManager.Instance.ReLoadPlayerSetting();
                GuildManager.Instance.SettingData.GetCurrentPlayerGroup().talentLevel = MainManager.Instance.PlayerSetting.talentLevel;
                GuildManager.Instance.SettingData.GetCurrentPlayerGroup().talentSkill = MainManager.Instance.PlayerSetting.talentSkill;
                GuildManager.Instance.SettingData.GetCurrentPlayerGroup().teamSkill = MainManager.Instance.PlayerSetting.teamSkill;
            }
            talentLevelParameter = GuildManager.Instance.SettingData.GetCurrentPlayerGroup().talentLevel;
            UpdateTalentInfoData(GuildManager.Instance.SettingData.GetCurrentPlayerGroup().talentSkill, MainManager.Instance.TalentSkillNodeDic, DataType.TalentSkill);
            UpdateTalentInfoData(GuildManager.Instance.SettingData.GetCurrentPlayerGroup().teamSkill, MainManager.Instance.TeamSkillNodeDic, DataType.TeamSkill);
            UpdateCombinedData();
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