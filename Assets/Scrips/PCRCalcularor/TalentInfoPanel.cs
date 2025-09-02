using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
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
        private PrincessKnightInfo talentInfoData => MainManager.Instance.TalentInfoData;
        private Dictionary<int, param_type> paramTypeDict => MainManager.Instance.paramTypeDic;

        private void OnEnable()
        {
            talentInfoData.LoadFromPlayerSetting(mainManager.PlayerSetting);
            SubscribeEvents();
            RefreshTable();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            if (talentInfoData != null)
                talentInfoData.OnDataChanged += RefreshTable;

            if (TalentLevelManager.Instance != null)
                TalentLevelManager.Instance.OnTalentLevelChanged += RefreshTable;
        }

        private void UnsubscribeEvents()
        {
            if (talentInfoData != null)
                talentInfoData.OnDataChanged -= RefreshTable;

            if (TalentLevelManager.Instance != null)
                TalentLevelManager.Instance.OnTalentLevelChanged -= RefreshTable;
        }

        /// <summary>
        /// 刷新表格显示
        /// </summary>
        private void RefreshTable()
        {
            ClearTable();
            RenderTable();
        }

        /// <summary>
        /// 清空现有表格
        /// </summary>
        private void ClearTable()
        {
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
            int[] talentLevels = new int[5];
            for (int i = 0; i < 5; i++)
                talentLevels[i] = talentInfoData.GetAttributeAttackPower(i + 1);

            CreateRow("属性攻击力", talentLevels);

            // 获取所有使用的参数类型
            var usedParamTypes = new HashSet<int>();
            foreach (var levelDict in talentInfoData.TalentInfoData.Values)
            {
                foreach (var paramType in levelDict.Keys)
                    usedParamTypes.Add(paramType);
            }

            // 为每个参数类型创建一行
            foreach (int paramType in usedParamTypes)
            {
                if (paramTypeDict.TryGetValue(paramType, out param_type param))
                {
                    int[] values = new int[5];
                    for (int i = 1; i <= 5; i++)
                    {
                        values[i - 1] = talentInfoData.TalentInfoData.GetValueOrDefault(i)?
                            .GetValueOrDefault(paramType) ?? 0;
                    }
                    CreateRow(param.parameter_name, values);
                }
            }
        }

        /// <summary>
        /// 创建表格行
        /// </summary>
        private void CreateRow(string typeName, int[] values)
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