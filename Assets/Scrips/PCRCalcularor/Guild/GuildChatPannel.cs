using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;
using XCharts.Runtime;

namespace PCRCaculator.Guild
{
    public class GuildChatPannel : MonoBehaviour
    {
        // public LineChart lineChart;
        public List<Toggle> GuildTypeToggles;
        public List<Toggle> GuildUnitToggles;
        public List<Text> GuildUnitTexts;

        public LineChart chart;

        // public GameObject posPrefab;
        // public Transform PosXParent;
        // public Transform PosYParent;

        private List<List<ValueChangeData>> AllUnitDamageDates;
        private List<double> DamageMax;
        private List<List<ValueChangeData>> AllUnitTotalDamageDates;
        private List<ValueChangeData>[] BossDefData;
        private List<ValueChangeData>[] BossMagicDefData;
        private const float Max_Y_Rate = 1.1f;
        private static List<ValueChangeData> emptyList;

        private List<GameObject> lineChatXPrefabList = new List<GameObject>();
        private List<GameObject> lineChatYPrefabList = new List<GameObject>();

        private string[] textForUnits;

        private string[] textForBoss;
        private int partCount;
        public InputField pauseTime;
#if UNITY_5_3_OR_NEWER
        private ScrollRect unitToggleScroll;
        private float unitToggleHeight;
        private float unitToggleSpacing;
        private int visibleUnitToggleCount;
#endif
        //private int currentType = 1;

        public void Init()
        {
            SetLineChat();
            AllUnitDamageDates = new List<List<ValueChangeData>>();
            AllUnitTotalDamageDates = new List<List<ValueChangeData>>();
            DamageMax = new List<double>();
            BossDefData = Array.Empty<List<ValueChangeData>>();
            BossMagicDefData = Array.Empty<List<ValueChangeData>>();
            emptyList = new List<ValueChangeData> { new ValueChangeData(0, 0), new ValueChangeData(1, 0) };
            // clear old object
            foreach (GameObject a in lineChatXPrefabList)
            {
                Destroy(a);
            }
            lineChatXPrefabList.Clear();
            foreach (GameObject b in lineChatYPrefabList)
            {
                Destroy(b);
            }
            lineChatYPrefabList.Clear();
            var damageByUnit = GuildCalculator.Instance.playerUnitDamageDic;
            var players = MyGameCtrl.Instance.playerUnitCtrl;
            var playerIds = players.Select(unit => unit.UnitId).ToArray();
            // Use the same ordering for values and labels; key 0 is the total, not a unit.
            var unitIds = playerIds.Where(damageByUnit.ContainsKey)
                .Concat(damageByUnit.Keys.Where(id => id != 0 && !playerIds.Contains(id)))
                .Prepend(0).ToArray();
            foreach (var unitId in unitIds)
            {
                var value = damageByUnit[unitId];
                List<ValueChangeData> value2 = GuildCalculator.CreateLineChatData2(value);
                AllUnitDamageDates.Add(value2);
                AllUnitTotalDamageDates.Add(GuildCalculator.CreateTotalChatData(value));
                DamageMax.Add(value2.Select(a => a.yValue).DefaultIfEmpty().Max());
            }
            BossDefData = GuildCalculator.Instance.bossDefChangeDic
                .GroupBy(x => x.target).OrderBy(x => x.Key)
                .Select(x => x.ToList()).ToArray();
            BossMagicDefData = GuildCalculator.Instance.bossMgcDefChangeDic
                .GroupBy(x => x.target).OrderBy(x => x.Key)
                .Select(x => x.ToList()).ToArray();

            textForUnits = unitIds.Select(id => id == 0 ? "全部" :
                players.FirstOrDefault(unit => unit.UnitId == id)?.UnitName
                ?? $"{MainManager.Instance.GetUnitNickName(id)}（召唤物）").ToArray();

            partCount = GuildCalculator.Instance.bossDefChangeDic.Select(x => x.target).DefaultIfEmpty().Max();
            textForBoss = partCount == 0
                ? new[] {"本体"}
                : Enumerable.Range(1, partCount).Select(x => $"部位{x}").ToArray();

        }

        public void Refresh()
        {
            for(int i=0;i<GuildTypeToggles.Count;i++)
            {
                if (GuildTypeToggles[i].isOn)
                {
                    SwitchChatType(i + 1);
                    return;
                }
            }
        }

        private void AddLineSerie(string title = null, LineType? lineType = null)
        {
#if UNITY_5_3_OR_NEWER
            var serie = chart.AddSerie<Line>();
            if (title != null)
                serie.serieName = title;
            serie.lineStyle.width = 1f;
            if (lineType != null)
                serie.lineType = lineType.Value;
#endif
        }
        
        private void DrawData(List<ValueChangeData>[] data, LineType type, string[] serieTitle)
        {
#if UNITY_5_3_OR_NEWER
            var tooltip = chart.GetChartComponent<Tooltip>();
            if (tooltip != null)
            {
                tooltip.trigger = Tooltip.Trigger.Axis;
                tooltip.itemFormatter = "{.}|{a}|{c} ({e})";
            }
#endif
            chart.RemoveData();

            if (data.Length == 0) return;

            var xpos = 
                data.Length > 1 ? 
                    Enumerable.Range(0, data.SelectMany(x => x).Max(x => x.xValue) + 1).ToArray() :
                    data[0].Select(x => x.xValue).Distinct().ToArray();

            var nowx = new int[data.Length];

            for (var i = 0; i < serieTitle.Length; i++)
            {
                AddLineSerie(serieTitle[i], type);
            }

            foreach (var x in xpos)
            {
                chart.AddXAxisData(x.ToString());
                for (var i = 0; i < data.Length; ++i)
                {
                    while (nowx[i] + 1 < data[i].Count && data[i][nowx[i] + 1].xValue <= x)
                        ++nowx[i];
                    var t = data[i][nowx[i]];   
                    chart.AddData(i, x, t.yValue, $"[{t.xValue}] {t.describe}");
                }
            }
        }

        private void SwitchChatType(int TypeId)
        {
            SetUnitToggles(TypeId);
            var serieTitle = TypeId <= 2 ? textForUnits : textForBoss;
            var data = Enumerable.Range(0, serieTitle.Length).Select(_ => new List<ValueChangeData>()).ToArray();
            float TotalMax = 1;
            LineType type = LineType.StepEnd;
            switch (TypeId)
            {
                case 1:
                case 2:
                {
                    var t = TypeId == 1 ? AllUnitDamageDates : AllUnitTotalDamageDates;
                    for (int i = 0; i < data.Length; i++)
                        data[i] = GuildCalculator.NormalizeLineChatData(t[i], TotalMax);
                    // type = LineType.Normal;
                    break;
                }
                case 3:
                case 4:
                {
                    var t = TypeId == 3 ? BossDefData : BossMagicDefData;
                    foreach (var (k, l) in t.Where(x => x.Count > 0).Select(x => (x[0].target, x)))
                        data[k - (partCount > 0 ? 1 : 0)] = l;
                    break;
                }
            }

            for (var i = 0; i < data.Length; ++i)
                if (!(GuildUnitToggles[i].interactable && GuildUnitToggles[i].isOn))
                    data[i] = null;

            var t0 = serieTitle.Select((title, index) => (title, value: data[index], index))
                .Where(p => p.value != null && p.value.Count > 0).ToArray();
            DrawData(t0.Select(x => x.value).ToArray(),
                type, t0.Select(x => x.title).ToArray());
        }

        private void SetUnitToggles(int TypeId)
        {
            var titles = TypeId <= 2 ? textForUnits : textForBoss;
#if UNITY_5_3_OR_NEWER
            EnsureUnitToggleCount(titles.Length);
#endif
            for (var i = 0; i < GuildUnitToggles.Count; i++)
            {
                var visible = i < titles.Length;
                GuildUnitTexts[i].text = visible ? titles[i] : "???";
                GuildUnitToggles[i].interactable = visible;
                GuildUnitToggles[i].gameObject.SetActive(visible);
            }
        }

#if UNITY_5_3_OR_NEWER
        private void EnsureUnitToggleCount(int count)
        {
            if (count > GuildUnitToggles.Count && unitToggleScroll == null)
            {
                // Keep the existing sidebar bounds and scroll when summons add more rows.
                var first = (RectTransform)GuildUnitToggles[0].transform;
                var last = (RectTransform)GuildUnitToggles[GuildUnitToggles.Count - 1].transform;
                var viewport = (RectTransform)first.parent;
                unitToggleHeight = first.rect.height;
                unitToggleSpacing = GuildUnitToggles.Count > 1
                    ? (first.anchoredPosition.y - last.anchoredPosition.y) / (GuildUnitToggles.Count - 1)
                    : unitToggleHeight;
                var top = first.anchoredPosition.y + unitToggleHeight * (1 - first.pivot.y);
                var bottom = last.anchoredPosition.y - last.rect.height * last.pivot.y;
                viewport.anchoredPosition += new Vector2(0, (top + bottom) / 2);
                viewport.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, top - bottom);
                viewport.gameObject.AddComponent<RectMask2D>();
                var background = viewport.gameObject.AddComponent<Image>();
                background.color = Color.clear;

                var content = new GameObject("UnitToggleContent", typeof(RectTransform)).GetComponent<RectTransform>();
                content.gameObject.layer = viewport.gameObject.layer;
                content.SetParent(viewport, false);
                content.anchorMin = new Vector2(0, 1);
                content.anchorMax = new Vector2(1, 1);
                content.pivot = new Vector2(0.5f, 1);
                content.sizeDelta = Vector2.zero;
                content.anchoredPosition = Vector2.zero;
                foreach (var toggle in GuildUnitToggles)
                    toggle.transform.SetParent(content, false);

                unitToggleScroll = viewport.gameObject.AddComponent<ScrollRect>();
                unitToggleScroll.viewport = viewport;
                unitToggleScroll.content = content;
                unitToggleScroll.horizontal = false;
                unitToggleScroll.movementType = ScrollRect.MovementType.Clamped;
                unitToggleScroll.scrollSensitivity = unitToggleSpacing;
            }

            while (GuildUnitToggles.Count < count)
            {
                var templateIndex = GuildUnitToggles.Count - 1;
                var template = GuildUnitToggles[templateIndex];
                var toggle = Instantiate(template, template.transform.parent, false);
                toggle.targetGraphic.color = new Color32(255, 255, 255, 255);
                toggle.name = $"UnitToggle{GuildUnitToggles.Count}";
                // Replace the cloned event so each click refreshes exactly once.
                toggle.onValueChanged = new Toggle.ToggleEvent();
                toggle.onValueChanged.AddListener(_ => Refresh());
                toggle.SetIsOnWithoutNotify(false);
                GuildUnitToggles.Add(toggle);
                var text = toggle.GetComponentInChildren<Text>(true);
                GuildUnitTexts.Add(text);
            }

            if (unitToggleScroll != null && visibleUnitToggleCount != count)
            {
                var containerHeight = unitToggleHeight + (count - 1) * unitToggleSpacing;
                for (var i = 0; i < GuildUnitToggles.Count; i++)
                {
                    var rect = (RectTransform)GuildUnitToggles[i].transform;
                    rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 1);
                    rect.anchoredPosition = new Vector2(0, -unitToggleHeight / 2 - i * unitToggleSpacing);
                }
                unitToggleScroll.viewport.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, containerHeight);
                unitToggleScroll.content.sizeDelta = new Vector2(0, containerHeight);
                unitToggleScroll.StopMovement();
                unitToggleScroll.content.anchoredPosition = Vector2.zero;
            }
            visibleUnitToggleCount = count;
        }
#endif
        private void SetPos(List<ValueChangeData> data,float MaxValue)
        {
            float deltaX = 1 / 20.0f;
            float deltaY = 0.1f;
            ValueChangeData changeDataOld = new ValueChangeData(0, 0);
            foreach (GameObject a in lineChatXPrefabList)
            {
                Destroy(a);
            }
            lineChatXPrefabList.Clear();
            foreach (GameObject b in lineChatYPrefabList)
            {
                Destroy(b);
            }
            lineChatYPrefabList.Clear();
            /*
            for (int i = 0; i < data.Count; i++)
            {
                if (Mathf.Abs(data[i].xValue - changeDataOld.xValue) >= deltaX && Mathf.Abs(data[i].yValue - changeDataOld.yValue) >= deltaY)
                {
                    GameObject a = Instantiate(posPrefab);
                    a.transform.SetParent(PosXParent, false);
                    a.transform.localPosition = new Vector3(425 * data[i].xValue, 0, 0);
                    int frame = Mathf.RoundToInt(data[i].xValue * 5400);
                    a.GetComponent<Text>().text = frame + "";
                    lineChatXPrefabList.Add(a);

                    GameObject b = Instantiate(posPrefab);
                    b.transform.SetParent(PosYParent, false);
                    b.transform.localPosition = new Vector3(0,200*data[i].yValue, 0);
                    int value = Mathf.RoundToInt(data[i].yValue * MaxValue);
                    b.GetComponent<Text>().text = value + "";
                    lineChatYPrefabList.Add(b);

                    changeDataOld = data[i];
                }
            }*/

        }
        private void SetLineChat()
        {
            /*
            for(int i = 0; i < 6; i++)
            {
                lineChart.Inject(emptyList);
            }
            lineChart.ShowUnit();*/
        }

    }
}
