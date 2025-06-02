using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PCRCaculator.Guild;

public class TimelinePanel : MonoBehaviour
{
    public GameObject TimelineCell;
    public ScrollRect scrollRect;
    private List<GameObject> cells = new();
    public static GuildTimelineData TimelineData = new();
    public List<UBDetail> uBDetails;
    private float savedScrollPosition = 1f;

    void OnEnable()
    {
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
        foreach (var detail in uBDetails)
        {
            GameObject cell = Instantiate(TimelineCell, transform);
            SetCellText(cell, CreateContent(detail));
            cells.Add(cell);
        }
        RestoreScrollPosition(0f);
    }

    private string[] CreateContent(UBDetail detail)
    {
        string[] content = new string[6];
        int seconds = GuildManager.Instance.SettingData.limitTime - detail.UBTime / 60;
        content[0] = seconds.ToString();
        content[1] = detail.UBTime.ToString();

        if (detail.unitData != null)
        {
            content[2] = detail.unitData.GetUnitName();
            content[3] = detail.description;
            content[4] = detail.Critical ? $"<color=#FF0000>{detail.Damage}</color>" : $"{detail.Damage}";
            content[5] = $"<color=#6600CC>{detail.totalDamage}</color>";
        }
        else
        {
            content[2] = "<color=#0024FF>BOSS</color>";
            content[3] = "<color=#0024FF>UB</color>";
            content[4] = "";
            content[5] = "";
        }
        return content;
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