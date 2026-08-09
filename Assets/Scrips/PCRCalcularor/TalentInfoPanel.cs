using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using Elements;
using PCRCaculator.SQL;

namespace PCRCaculator
{
    public class TalentInfoPanel : MonoBehaviour
    {
        public GameObject rowPrefab;
        public Transform tableContainer;

        private MainManager mainManager => MainManager.Instance;
        private TeamEnhanceParameter teamEnhanceData => TalentEnhanceContext.ActiveTeamEnhanceData;
        private Dictionary<int, param_type> paramTypeDict => MainManager.Instance.ParamTypeDic;
        private TeamEnhanceParameter subscribedTeamEnhanceData;
        private TalentLevelManager subscribedTalentLevelManager;

        private void OnEnable()
        {
            teamEnhanceData.LoadFromPlayerSetting();
            SubscribeEvents();
            RefreshTable();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UnsubscribeEvents();

            subscribedTeamEnhanceData = teamEnhanceData;
            if (subscribedTeamEnhanceData != null)
                subscribedTeamEnhanceData.OnDataChanged += RefreshTable;

            subscribedTalentLevelManager = TalentLevelManager.Instance;
            if (subscribedTalentLevelManager != null)
                subscribedTalentLevelManager.OnTalentLevelChanged += RefreshTable;
        }

        private void UnsubscribeEvents()
        {
            if (subscribedTeamEnhanceData != null)
                subscribedTeamEnhanceData.OnDataChanged -= RefreshTable;
            subscribedTeamEnhanceData = null;

            if (subscribedTalentLevelManager != null)
                subscribedTalentLevelManager.OnTalentLevelChanged -= RefreshTable;
            subscribedTalentLevelManager = null;
        }

        /// <summary>
        /// 刷新表格显示
        /// </summary>
        private void RefreshTable()
        {
            if (tableContainer == null || rowPrefab == null || teamEnhanceData == null)
                return;

            ClearTable();
            RenderTable();
        }

        /// <summary>
        /// 清空现有表格
        /// </summary>
        private void ClearTable()
        {
            if (tableContainer == null)
                return;

            for (int i = tableContainer.childCount - 1; i >= 1; i--)
            {
                DestroyImmediate(tableContainer.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 渲染属性信息表格
        /// </summary>
        private void RenderTable()
        {
            // 显示属性攻击力行
            string[] talentLevelStrings = new string[5];
            for (int i = 0; i < 5; i++)
            {
                int attackPower = teamEnhanceData.GetAttributeAttackPower(i + 1);
                talentLevelStrings[i] = attackPower.ToString();
            }
            CreateRow("属性攻击力", talentLevelStrings);

            // 获取所有使用的参数类型并排序
            var usedParamTypes = new HashSet<eParamType>();
            foreach (var levelDict in teamEnhanceData.talentBuffParameter.Values)
            {
                foreach (var paramType in levelDict.Keys)
                    usedParamTypes.Add(paramType);
            }

            // 对参数类型进行排序
            var sortedParamTypes = usedParamTypes.OrderBy(p => p);

            // 为每个参数类型创建一行
            foreach (eParamType paramType in sortedParamTypes)
            {
                string[] values = new string[5];
                for (int i = 1; i <= 5; i++)
                {
                    int rawValue = teamEnhanceData.talentBuffParameter.GetValueOrDefault(i)?
                        .GetValueOrDefault(paramType) ?? 0;

                    // 获取参数配置并应用百分比格式
                    if (paramTypeDict.TryGetValue((int)paramType, out var paramConfig))
                    {
                        float formattedValue;
                        if (paramConfig.ratio > 0)
                            formattedValue = (float)rawValue / paramConfig.ratio;
                        else
                            formattedValue = rawValue;

                        if (paramConfig.is_percentage == 1)
                            values[i - 1] = $"{formattedValue * 100}%";
                        else
                            values[i - 1] = formattedValue.ToString("F0");
                    }
                    else
                    {
                        values[i - 1] = rawValue.ToString();
                    }
                }
                // 使用GetDescription()获取参数名称
                CreateRow(paramType.GetDescription(), values);
            }
        }

        /// <summary>
        /// 创建表格行
        /// </summary>
        private void CreateRow(string typeName, string[] values)
        {
            GameObject row = Instantiate(rowPrefab, tableContainer);
            Text[] texts = row.GetComponentsInChildren<Text>();

            texts[0].text = typeName;
            for (int i = 0; i < 5 && i < values.Length; i++)
            {
                texts[i + 1].text = values[i].ToString();
            }
        }
    }
}
