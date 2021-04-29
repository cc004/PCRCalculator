using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpringGUI;
using System.Linq;

namespace PCRCaculator.Guild
{
    public class GuildChatPannel : MonoBehaviour
    {
        public LineChart lineChart;
        public List<Toggle> GuildTypeToggles;
        public List<Toggle> GuildUnitToggles;
        public List<Text> GuildUnitTexts;
        public GameObject posPrefab;
        public Transform PosXParent;
        public Transform PosYParent;

        private List<List<ValueChangeData>> AllUnitDamageDates = new List<List<ValueChangeData>>();
        private List<float> DamageMax = new List<float>();
        private List<List<ValueChangeData>> AllUnitTotalDamageDates = new List<List<ValueChangeData>>();
        private List<ValueChangeData> BossDefData = new List<ValueChangeData>();
        private float bossDefMax;
        private List<ValueChangeData> BossMagicDefData = new List<ValueChangeData>();
        private float bossMagicDefMax;
        private const float Max_Y_Rate = 1.1f;
        private static List<ValueChangeData> emptyList = new List<ValueChangeData> { new ValueChangeData(0, 0), new ValueChangeData(1, 0) };

        private List<GameObject> lineChatXPrefabList = new List<GameObject>();
        private List<GameObject> lineChatYPrefabList = new List<GameObject>();

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
            BossDefData = GuildCalculator.CreateLineChatData(GuildCalculator.Instance.bossDefChangeDic);
            bossDefMax = BossDefData.Max(a => a.yValue);
            BossMagicDefData = GuildCalculator.CreateLineChatData(GuildCalculator.Instance.bossMgcDefChangeDic);
            bossMagicDefMax = BossMagicDefData.Max(a => a.yValue);
            Reflash();
        }
        public void Reflash()
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
        public void SwitchChatType(int TypeId)
        {
            SetUnitToggles(TypeId);
            bool[] showChat = new bool[6] { false, false, false, false, false, false };
            float TotalMax = 1;
            switch (TypeId)
            {
                case 1:
                    for(int i = 0; i < 6; i++)
                    {
                        if(GuildUnitToggles[i].interactable && GuildUnitToggles[i].isOn)
                        {
                            showChat[i] = true;
                            if (TotalMax < DamageMax[i])
                            {
                                TotalMax = DamageMax[i];
                            }
                        }
                    }
                    bool k1 = true;
                    for (int i = 0; i < 6; i++)
                    {
                        if (showChat[i])
                        {
                            List<ValueChangeData> data1 = GuildCalculator.NormalizeLineChatData(AllUnitDamageDates[i], TotalMax);
                            lineChart.Replace<ValueChangeData>(i, data1);
                            if (k1)
                            {
                                SetPos(data1, TotalMax);
                                k1 = false;
                            }
                        }
                        else
                        {
                            lineChart.Replace<ValueChangeData>(i, emptyList);
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < 6; i++)
                    {
                        if (GuildUnitToggles[i].interactable && GuildUnitToggles[i].isOn)
                        {
                            showChat[i] = true;
                            if (TotalMax < AllUnitTotalDamageDates[i][AllUnitTotalDamageDates[i].Count - 1].yValue)
                            {
                                TotalMax = AllUnitTotalDamageDates[i][AllUnitTotalDamageDates[i].Count - 1].yValue;
                            }
                        }
                    }
                    bool k2 = true;
                    for (int i = 0; i < 6; i++)
                    {
                        if (showChat[i])
                        {
                            List<ValueChangeData> data2 = GuildCalculator.NormalizeLineChatData(AllUnitTotalDamageDates[i], TotalMax);
                            lineChart.Replace<ValueChangeData>(i, data2);
                            if (k2)
                            {
                                SetPos(data2, TotalMax);
                                k2 = false;
                            }
                        }
                        else
                        {
                            lineChart.Replace<ValueChangeData>(i, emptyList);
                        }
                    }
                    break;
                case 3:
                    List<ValueChangeData> data3 = GuildCalculator.NormalizeLineChatData(BossDefData, bossDefMax);
                    lineChart.Replace<ValueChangeData>(0, data3);
                    SetPos(data3, bossDefMax);
                    for (int i = 1; i < 6; i++)
                    {
                        lineChart.Replace<ValueChangeData>(i, emptyList);
                    }
                    break;
                case 4:
                    List<ValueChangeData> data4 = GuildCalculator.NormalizeLineChatData(BossMagicDefData, bossMagicDefMax);
                    lineChart.Replace<ValueChangeData>(0, data4);
                    SetPos(data4, bossMagicDefMax);
                    for (int i = 1; i < 6; i++)
                    {
                        lineChart.Replace<ValueChangeData>(i, emptyList);
                    }
                    break;

            }

        }
        public void SetUnitToggles(int TypeId)
        {
            switch (TypeId)
            {
                case 1:
                case 2:
                    GuildUnitTexts[0].text = "全部";
                    GuildUnitToggles[0].interactable = true;                    
                    for(int i = 0; i < 5; i++)
                    {
                        if(i < Elements.MyGameCtrl.Instance.playerUnitCtrl.Count)
                        {
                            GuildUnitTexts[i + 1].text = Elements.MyGameCtrl.Instance.playerUnitCtrl[i].UnitName;
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
                    GuildUnitTexts[0].text = Elements.MyGameCtrl.Instance.enemyUnitCtrl[0].UnitName;
                    GuildUnitToggles[0].interactable = true;
                    for (int i = 0; i < 5; i++)
                    {
                        GuildUnitTexts[i + 1].text = "???";
                        GuildUnitToggles[i + 1].interactable = false;
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
            }

        }
        private void SetLineChat()
        {
            for(int i = 0; i < 6; i++)
            {
                lineChart.Inject<ValueChangeData>(emptyList);
            }
            lineChart.ShowUnit();
        }

    }
}
