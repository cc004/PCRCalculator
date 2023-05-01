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

        private List<List<ValueChangeData>> AllUnitDamageDates = new List<List<ValueChangeData>>();
        private List<float> DamageMax = new List<float>();
        private List<List<ValueChangeData>> AllUnitTotalDamageDates = new List<List<ValueChangeData>>();
        private List<ValueChangeData>[] BossDefData = Array.Empty<List<ValueChangeData>>();
        private List<ValueChangeData>[] BossMagicDefData = Array.Empty<List<ValueChangeData>>();
        private const float Max_Y_Rate = 1.1f;
        private static List<ValueChangeData> emptyList = new List<ValueChangeData> { new ValueChangeData(0, 0), new ValueChangeData(1, 0) };

        private List<GameObject> lineChatXPrefabList = new List<GameObject>();
        private List<GameObject> lineChatYPrefabList = new List<GameObject>();

        private string[] textForUnits;

        private string[] textForBoss;
        private int partCount;

        //private int currentType = 1;

        public void Init()
        {
            SetLineChat();
            List<ValueChangeData> value0 = GuildCalculator.CreateLineChatData2(GuildCalculator.Instance.playerUnitDamageDic[0]);
            AllUnitDamageDates.Add(value0);
            AllUnitTotalDamageDates.Add(GuildCalculator.CreateTotalChatData(GuildCalculator.Instance.playerUnitDamageDic[0]));
            DamageMax.Add(AllUnitDamageDates[0].Max(a => a.yValue));
            foreach (List<ValueChangeData> value in GuildCalculator.Instance.playerUnitDamageDic.Values)
            {
                List<ValueChangeData> value2 = GuildCalculator.CreateLineChatData2(value);
                AllUnitDamageDates.Add(value2);
                AllUnitTotalDamageDates.Add(GuildCalculator.CreateTotalChatData(value));
                DamageMax.Add(value2.Max(a => a.yValue));
            }
            BossDefData = GuildCalculator.Instance.bossDefChangeDic
                .GroupBy(x => x.target).OrderBy(x => x.Key)
                .Select(x => x.ToList()).ToArray();
            BossMagicDefData = GuildCalculator.Instance.bossMgcDefChangeDic
                .GroupBy(x => x.target).OrderBy(x => x.Key)
                .Select(x => x.ToList()).ToArray();

            textForUnits = GuildCalculator.Instance.playerUnitDamageDic.Keys
                .Select(unit => MainManager.Instance.GetUnitNickName(unit))
                .Prepend("全部").ToArray();

            partCount = GuildCalculator.Instance.bossDefChangeDic.Max(x => x.target);
            textForBoss = partCount == 0
                ? new[] {"本体"}
                : Enumerable.Range(1, partCount).Select(x => $"部位{x}").ToArray();

            Reflash();
        }

        private void Reflash()
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

        private void DrawData(List<ValueChangeData>[] data, LineType type, string[] serieTitle)
        {
            chart.RemoveData();

            if (data.Length == 0) return;

            var xpos = 
                data.Length > 1 ? 
                    Enumerable.Range(0, data.SelectMany(x => x).Max(x => x.xValue)).ToArray() :
                    data[0].Select(x => x.xValue).Distinct().ToArray();

            var nowx = new int[data.Length];

            foreach (var title in serieTitle)
            {
                var serie = chart.AddSerie<Line>();
                serie.serieName = title;
                if (data.Length == 1) serie.lineType = type;
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
            var data = Enumerable.Range(0, 6).Select(_ => new List<ValueChangeData>()).ToArray();
            float TotalMax = 1;
            LineType type = LineType.StepEnd;
            var serieTitle = textForUnits.ToArray();
            switch (TypeId)
            {
                case 1:
                case 2:
                {
                    var t = TypeId == 1 ? AllUnitDamageDates : AllUnitTotalDamageDates;
                    for (int i = 0; i < 6; i++)
                        data[i] = GuildCalculator.NormalizeLineChatData(AllUnitDamageDates[i], TotalMax);
                    type = LineType.Normal;
                    break;
                }
                case 3:
                case 4:
                {
                    var t = TypeId == 3 ? BossDefData : BossMagicDefData;
                    foreach (var (k, l) in t.Where(x => x.Count > 0).Select(x => (x[0].target, x)))
                        data[k - (partCount > 0 ? 1 : 0)] = l;
                    serieTitle = textForBoss.ToArray();
                    break;
                }
            }

            for (var i = 0; i < 6; ++i)
                if (!(GuildUnitToggles[i].interactable && GuildUnitToggles[i].isOn))
                    data[i] = null;

            var t0 = serieTitle.Zip(data, (x, y) => (x, y))
                .Where(p => p.y != null && p.y.Count > 0).ToArray();
            DrawData(t0.Select(x => x.y).ToArray(),
                type, t0.Select(x => x.x).ToArray());
        }

        private void SetUnitToggles(int TypeId)
        {
            switch (TypeId)
            {
                case 1:
                case 2:
                    GuildUnitTexts[0].text = "全部";
                    GuildUnitToggles[0].interactable = true;                    
                    for(int i = 0; i < 5; i++)
                    {
                        if(i < MyGameCtrl.Instance.playerUnitCtrl.Count)
                        {
                            GuildUnitTexts[i + 1].text = MyGameCtrl.Instance.playerUnitCtrl[i].UnitName;
                            GuildUnitToggles[i + 1].interactable = true;
                        }
                        else
                        {
                            GuildUnitTexts[i + 1].text = "???";
                            GuildUnitToggles[i + 1].interactable = false;
                        }
                    }
                    break;
                case 3:
                case 4:
                    if (partCount == 0)
                    {
                        GuildUnitTexts[0].text = MyGameCtrl.Instance.enemyUnitCtrl[0].UnitName;
                        GuildUnitToggles[0].interactable = true;
                    }
                    else
                        for (var i = 0; i < 6; i++)
                        {
                            if (i >= partCount)
                            {
                                GuildUnitTexts[i].text = "???";
                                GuildUnitToggles[i].interactable = false;
                            }
                            else
                            {
                                GuildUnitTexts[i].text = $"部位{i + 1}";
                                GuildUnitToggles[i].interactable = true;
                            }
                        }
                    break;
            }
        }
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
