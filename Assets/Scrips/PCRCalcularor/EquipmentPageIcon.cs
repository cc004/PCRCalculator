using System.Collections.Generic;
using System.Linq;
using Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class EquipmentPageIcon : MonoBehaviour
    {
        public Image equipmentIcon;
        //public Text numText;
        public List<Text> subText;
        public RectTransform ScreenShotTrans;
        public List<Sprite> levelIcons;
        public Image levelIcon;
        public void SetEquipmentIcon(int equipmentid, int num, int alreadyHave = -1)
        {
            //string equip_path = "equipments/icon_equipment_" + equipmentid;
            //Sprite im = MainManager.LoadSourceSprite(equip_path);
            // Sprite im = ABExTool.GetSprites(ABExTool.SpriteType.装备图标, equipmentid);
            // if (im != null)
            // {
            //     equipmentIcon.sprite = im;
            // }
            // else
            // {
            //     equipmentIcon.sprite = ABExTool.GetSprites(ABExTool.SpriteType.装备图标, 999999);
            // }

            // if (num >= 2)
            // {
            //     string showSTR = "x" + num;
            //     if (alreadyHave > 0)
            //     {
            //         showSTR += alreadyHave >= num ? $"有({alreadyHave})" : $"<color=#FF0000>缺{num - alreadyHave}</color>";
            //     }
            //     else
            //     {
            //         showSTR = $"<color=#FF0000>{showSTR}</color>";
            //     }
            //     numText.text = showSTR;

            //     numText.gameObject.SetActive(true);
            // }
            // else
            // {
            //     numText.gameObject.SetActive(false);
            // }
        }
        public void SetExEquipmentIcon(int unitId, int equipmentid, int exlevel, int[] exEquipSubs, int j = 0)
        {
            Sprite loadedSprite = ABExTool.GetSprites(ABExTool.SpriteType.EX装备, equipmentid);
            if (loadedSprite != null)
            {
                equipmentIcon.sprite = loadedSprite;
                levelIcon.sprite = levelIcons[exlevel];
                int exRarity = (equipmentid / 100) % 10;

                // 根据exRarity决定显示模式：>=5时隐藏levelIcon，<5时显示levelIcon
                levelIcon.SetActive(exRarity < 5);

                // 如果exRarity>=5且exEquipSubs不为空，则显示属性信息
                if (exRarity >= 5 && exEquipSubs != null && exEquipSubs.Length > 0)
                {
                    // 获取对应装备的数据，通过equipmentid精确获取
                    if (MainManager.Instance.UnitExEquips.TryGetValue(unitId, out var exEquipDict) && exEquipDict.Length > j)
                    {
                        // 根据equipmentid获取对应的装备数据
                        if (exEquipDict[j].TryGetValue(equipmentid, out var exEquipData) && exEquipData.status.Count > 0)
                        {
                            var statusKeys = exEquipData.status.Keys.ToList();

                            // 创建一个字典来存储合并后的属性
                            var mergedStatus = new Dictionary<eParamType, int>();

                            // 遍历每个属性对（类型和等级）
                            for (int k = 0; k < exEquipSubs.Length; k += 2)
                            {
                                int statusIndex = exEquipSubs[k];     // 状态类型索引
                                int statusLevel = exEquipSubs[k + 1]; // 状态等级

                                if (statusIndex >= 0 && statusIndex < statusKeys.Count &&
                                    statusLevel >= 0 && statusLevel < exEquipData.status[statusKeys[statusIndex]].Length)
                                {
                                    var selectedKey = statusKeys[statusIndex];

                                    if (selectedKey is eParamType enumKey && exEquipData.status.TryGetValue(selectedKey, out var values) && statusLevel < values.Length)
                                    {
                                        int rawValue = values[statusLevel];

                                        // 如果该属性已存在于合并字典中，则累加值；否则添加新项
                                        if (mergedStatus.ContainsKey(enumKey))
                                        {
                                            mergedStatus[enumKey] += rawValue;
                                        }
                                        else
                                        {
                                            mergedStatus[enumKey] = rawValue;
                                        }
                                    }
                                }
                            }

                            // 显示合并后的属性
                            int displayIndex = 0;
                            foreach (var kvp in mergedStatus)
                            {
                                eParamType paramType = kvp.Key;
                                int totalValue = kvp.Value;

                                // 格式化数值
                                string formattedValue = totalValue.ToString();

                                // 获取参数配置并应用百分比格式
                                if (MainManager.Instance.ParamTypeDic.TryGetValue((int)paramType, out var paramConfig))
                                {
                                    float processedValue;
                                    if (paramConfig.ratio > 0)
                                        processedValue = (float)totalValue / paramConfig.ratio;
                                    else
                                        processedValue = totalValue;

                                    if (paramConfig.is_percentage == 1)
                                        formattedValue = $"{processedValue * 100}%";
                                    else
                                        formattedValue = processedValue.ToString("F0");
                                }

                                // 获取枚举描述
                                string description = paramType.GetDescription();

                                // 设置文本内容为描述+格式化的值
                                if (displayIndex < subText.Count)
                                {
                                    subText[displayIndex].text = $"{description}+{formattedValue}";
                                    subText[displayIndex].gameObject.SetActive(true); // 确保文本对象是激活的
                                    displayIndex++;
                                }
                            }

                            // 对于没有属性的槽位，隐藏对应的文本组件
                            for (int i = displayIndex; i < subText.Count; i++)
                            {
                                if (i < subText.Count)
                                {
                                    subText[i].gameObject.SetActive(false);
                                }
                            }
                        }
                        else
                        {
                            // 如果没有状态数据，隐藏所有subText
                            foreach (var text in subText)
                            {
                                text.gameObject.SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        // 如果无法获取装备数据，隐藏所有subText
                        foreach (var text in subText)
                        {
                            text.gameObject.SetActive(false);
                        }
                    }
                }
                else if (exRarity < 5)
                {
                    // exRarity < 5 时隐藏所有subText，只显示levelIcon
                    foreach (var text in subText)
                    {
                        text.gameObject.SetActive(false);
                    }
                }
                else
                {
                    // exRarity >= 5 但没有有效的exEquipSubs数据时，也隐藏subText
                    foreach (var text in subText)
                    {
                        text.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                // 如果找不到指定的装备图标，则使用默认图标
                int category = MainManager.Instance.UnitExEquips[unitId][j].FirstOrDefault().Value.category;
                Sprite defaultSprite = ABExTool.GetSprites(ABExTool.SpriteType.EX装备类型, category);
                equipmentIcon.sprite = defaultSprite;
                levelIcon.SetActive(false);

                // 隐藏所有subText
                foreach (var text in subText)
                {
                    text.gameObject.SetActive(false);
                }
            }
        }
    }
}
