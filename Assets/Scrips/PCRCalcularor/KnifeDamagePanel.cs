using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PCRCaculator.Guild;
using System.Linq;
using System;
using PCRCaculator;

public class KnifeDamagePanel : MonoBehaviour
{
    public GameObject KnifeDamageCell;
    public ScrollRect scrollRect;
    private List<GameObject> cells = new();
    public static GuildTimelineData TimelineData;
    public List<UBDetail> uBDetails;
    private float savedScrollPosition = 1f;
    private double totalUBDamage = 0;
    void OnEnable()
    {
        TimelineData = GuildCalculator.Instance.CreateTimeLineData();
        uBDetails = GuildCalculator.Instance.CreateUBDetailList();
        GenerateTimeline();
        RestoreScrollPosition(savedScrollPosition);
    }

    public void OnDisable()
    {
        foreach (GameObject cell in cells)
        {
            Destroy(cell);
        }
        cells.Clear();
        savedScrollPosition = scrollRect?.verticalNormalizedPosition ?? 0f;
    }

    private void GenerateTimeline()
    {
        bool firstIteration = true;
        for (int i = 21; i <= GuildManager.Instance.SettingData.limitTime; i++)
        {
            int frame = i * 60 -2;

            if (firstIteration && TimelineData.allUnitHPDic.Count > 0)
            {
                var hpDataList = TimelineData.allUnitHPDic.First().Value;
                if (hpDataList.Count > 0)
                {
                    ValueChangeData currentData = hpDataList.FindLast(data => data.xValue <= frame);
                    if (firstIteration && currentData != null && currentData.yValue <= 0)
                    {
                        firstIteration = false;
                    }
                }
            }
            else
            {
                break;
            }

            GameObject cell = Instantiate(KnifeDamageCell, transform);
            SetCellText(cell, CreateContent(i));
            cells.Add(cell);
        }
        RestoreScrollPosition(0f);
    }

    private string[] CreateContent(int i)
    {
        string[] content = new string[6];
        int seconds = i;
        int frame = seconds * 60;
        content[0] = seconds.ToString();
        content[1] = frame.ToString();
        content[2] = TotalDamgeText(frame);
        string remainingDamageText = RemainingDamageText(frame);
        string estimatedTotalDamage = EstimatedTotalDamage(content[2], totalUBDamage);
        content[3] = string.IsNullOrEmpty(remainingDamageText) ?
            $"{estimatedTotalDamage}" :
            $"{estimatedTotalDamage}={content[2]}+{remainingDamageText}";

        return content;
    }

    private string TotalDamgeText(int frame)
    {
        if (TimelineData.allUnitHPDic.Count > 0)
        {
            var hpDataList = TimelineData.allUnitHPDic.First().Value;
            if (hpDataList.Count > 0)
            {
                ValueChangeData currentData = hpDataList.FindLast(data => data.xValue <= frame);
                double totalDamage = currentData != null ? currentData.hp - currentData.yValue : 0;
                return Math.Round(totalDamage / 10000.0) + "w";
            }
            else
            {
                return "0w";
            }
        }
        else
        {
            return "0w";
        }
    }
    private string RemainingDamageText(int frame)
    {
        List<string> combinedTexts = new List<string>();
        totalUBDamage = 0;

        foreach (var unitTPData in TimelineData.allUnitTPDic.Skip(1))
        {
            int unitId = unitTPData.Key;
            var tpDataList = unitTPData.Value;

            if (tpDataList.Count > 0)
            {
                ValueChangeData currentTPData = tpDataList.FindLast(data => data.xValue <= frame);
                if (currentTPData != null && currentTPData.yValue >= 1000)
                {
                    if (TimelineData.uBDetails != null)
                    {
                        var ubDetail = TimelineData.uBDetails.Find(ub => ub != null && ub.unitData != null && ub.unitData.unitId == unitId && ub.UBTime > frame);
                        string unitName = MainManager.Instance.GetUnitNickName(unitId);
                        if (ubDetail != null)
                        {
                            string damageText = Math.Round(ubDetail.totalDamage / 10000.0) + "w";
                            combinedTexts.Add($"{damageText}(<color=#FF0000>{unitName}</color>)");
                            totalUBDamage += ubDetail.totalDamage;
                        }
                        else
                        {
                            combinedTexts.Add($"无(<color=#FF0000>{unitName}</color>)");
                        }
                    }
                }
            }
        }
        return string.Join(" + ", combinedTexts);
    }

    private string EstimatedTotalDamage(string totalDamageText, double totalUBDamage)
    {
        double currentTotalDamage = 0;
        if (double.TryParse(totalDamageText.Replace("w", ""), out double damageValue))
        {
            currentTotalDamage = damageValue * 10000;
        }

        // 计算总伤害（当前伤害 + UB伤害）
        double combinedDamage = currentTotalDamage + totalUBDamage;
        return $"<color=#6600CC>{Math.Round(combinedDamage / 10000.0) + "w"}</color>";
    }

    private void SetCellText(GameObject cell, string[] content)
    {
        Text[] texts = cell.GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.Length && i < content.Length; i++)
            texts[i].text = content[i];
    }

    private void RestoreScrollPosition(float position)
    {
        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = position;
        }
    }
}