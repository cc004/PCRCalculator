using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpringGUI;


namespace PCRCaculator.Guild
{
    public class GuildSkillGroupPrefab : MonoBehaviour
    {
        public GameObject skillButtonPrefab;
        public GameObject skillButtonPrefab_UB;
        public GameObject skillAbnormalPrefab;
        public LineChart lineChart;
        public GameObject lineChatPosXPrefab;
        public GameObject lineChatPosXParent;
        public Transform prefabParent_A;
        public Transform prefabParent_B;
        public Text nameText;
        public Image BackImage;
        public Vector3 originalScale;
        public List<Color> stateColors;
        public List<Color> buffColors;
        public List<Color> abnormalStateColors;
        public static string[] stateNames = new string[8] { "等待", "普攻", "UB", "技能", "走路", "无法行动", "死亡", "开局" };
        public float scaleValue;
        public int minLength;
        public float prefabTransformY;
        public int prefabHight;
        public int abnormalPrefabHight;
        public const float deltaXforChat = 1 / 5400.0f;

        private List<GameObject> prefabs = new List<GameObject>();
        private List<GameObject> prefabs_2 = new List<GameObject>();
        private List<int> currentAbnormalButtonEndPosList = new List<int>();
        private List<ValueChangeData> hpValueList = new List<ValueChangeData>();
        private List<ValueChangeData> tpValueList = new List<ValueChangeData>();
        private static List<ValueChangeData> emptyList = new List<ValueChangeData> { new ValueChangeData(0, 0), new ValueChangeData(1, 0) };
        private bool showHPLine;
        private bool showTPLine;
        private List<GameObject> lineChatXPrefabList = new List<GameObject>();
        private List<ToEndAbnormalState> toEndAbnormalData = new List<ToEndAbnormalState>();
        private int MaxHight = 0;

        public void Initialize(string name, Color color)
        {
            BackImage.color = color;
            nameText.text = name;
            SetLineChat();
        }
        /// <summary>
        /// 动态添加按钮
        /// </summary>
        /// <param name="frameCount">帧数</param>
        /// <param name="stateInt">状态</param>
        public void AddButtons(int frameCount, int endCount, int stateInt,System.Action action = null)
        {
            GameObject a = Instantiate(stateInt == 2 ? skillButtonPrefab_UB : skillButtonPrefab);
            a.transform.SetParent(prefabParent_A, false);
            a.transform.localPosition = new Vector3(frameCount * scaleValue, prefabTransformY, 0);
            int length = (int)(Mathf.Max(minLength, endCount - frameCount) * scaleValue);
            //if(stateInt == 2) { length = minLength; }
            a.GetComponent<RectTransform>().sizeDelta = new Vector2(length, prefabHight);
            a.GetComponent<GuildCalcButton>().SetButton(stateColors[stateInt], stateNames[stateInt], frameCount,action);
            prefabs.Add(a);
        }
        public void AddAbnormalStateButtons(UnitAbnormalStateChangeData changeData,System.Action action = null)
        {
            int i = 0;
            bool flag1 = false;
            bool flag2 = changeData.endFrameCount == 5500;
            if (flag2)
            {
                var toendData = toEndAbnormalData.Find(a0 => a0.changeData.GetDescription() == changeData.GetDescription());
                if (toendData != null)
                {
                    i = toendData.position;
                    flag1 = true;
                }              
            }
            if (!flag1)
            {
                for (; i < currentAbnormalButtonEndPosList.Count; i++)
                {
                    if (currentAbnormalButtonEndPosList[i] <= changeData.startFrameCount)
                    {
                        currentAbnormalButtonEndPosList[i] = changeData.endFrameCount;
                        break;
                    }
                }
                if (i >= currentAbnormalButtonEndPosList.Count)
                {
                    currentAbnormalButtonEndPosList.Add(changeData.endFrameCount);
                }
            }
            if (flag2 && !flag1)
            {
                toEndAbnormalData.Add(new ToEndAbnormalState(changeData, i));
            }
            GameObject a = Instantiate(skillAbnormalPrefab);
            a.transform.SetParent(prefabParent_B, false);
            a.transform.localPosition = new Vector3(changeData.startFrameCount * scaleValue, abnormalPrefabHight * i, 0);
            MaxHight = Mathf.Max(MaxHight, abnormalPrefabHight * i);
            int length = (int)(Mathf.Max(minLength, changeData.endFrameCount - changeData.startFrameCount) * scaleValue);
            //if(stateInt == 2) { length = minLength; }
            a.GetComponent<RectTransform>().sizeDelta = new Vector2(length, abnormalPrefabHight);
            a.GetComponent<GuildCalcButton>().SetButton(GetButtonColor(changeData), changeData.GetDescription(), changeData.startFrameCount, changeData.endFrameCount,!flag2,changeData.ShowDetail);
            prefabs_2.Add(a);
            
        }
        public float Resize(bool change)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            float sizeX = rectTransform.sizeDelta.x;
            float sizeY = 50 + MaxHight;
            if (!change)
            {
                rectTransform.sizeDelta = new Vector2(sizeX, 35);
                return 35;
            }
            if (MaxHight >= 0)
            {
                sizeX = rectTransform.sizeDelta.x;
                sizeY = 50 + MaxHight;
                rectTransform.sizeDelta = new Vector2(sizeX,sizeY);
                return sizeY;
            }
            else
            {
                return rectTransform.sizeDelta.y;
            }
        }
        public void SwitchPage(int idx)
        {
            prefabParent_A.gameObject.SetActive(idx < 2);
            prefabParent_B.gameObject.SetActive(idx == 1);
            lineChart.gameObject.SetActive(idx == 2 || idx == 3);
            showHPLine = idx == 2;
            showTPLine = idx == 3;
            ReflashLineChat();
        }

        public void ReflashHPChat(List<ValueChangeData> hpList)
        {
            if(hpList!=null)
            hpValueList = GuildCalculator.CreateLineChatData(hpList);
            ReflashLineChat();
        }
        public void ReflashTPChat(List<ValueChangeData> tpList)
        {
            if(tpList!=null)
            tpValueList = GuildCalculator.CreateLineChatData(tpList);
            ReflashLineChat();
        }
        private void ReflashLineChat()
        {
            if(showHPLine)
            {
                lineChart.Replace<ValueChangeData>(0, hpValueList);
                SetLineChatXpos(hpValueList);
            }
            else
            {
                lineChart.Replace<ValueChangeData>(0, emptyList);
            }
            if (showTPLine)
            {
                lineChart.Replace<ValueChangeData>(1, tpValueList);
                SetLineChatXpos(tpValueList);
            }
            else
            {
                lineChart.Replace<ValueChangeData>(1, emptyList);
            }
            
        }
        private void SetLineChatXpos(List<ValueChangeData> data)
        {
            float deltaX = 1 / 100.0f;
            float deltaY = 0.03f;
            ValueChangeData changeDataOld = new ValueChangeData(0,0);
            foreach(GameObject a in lineChatXPrefabList)
            {
                Destroy(a);
            }
            lineChatXPrefabList.Clear();
            for(int i = 0; i < data.Count; i++)
            {
                if(Mathf.Abs(data[i].xValue-changeDataOld.xValue)>=deltaX && Mathf.Abs(data[i].yValue - changeDataOld.yValue) >= deltaY)
                {
                    GameObject a = Instantiate(lineChatPosXPrefab);
                    a.transform.SetParent(lineChatPosXParent.transform, false);
                    a.transform.localPosition = new Vector3(4000 * data[i].xValue, 0, 0);
                    int frame = Mathf.RoundToInt(data[i].xValue * 5400);
                    a.GetComponent<Text>().text = frame + "";
                    lineChatXPrefabList.Add(a);
                    changeDataOld = data[i];
                }
            }
        }
        private void SetLineChat()
        {
            List<ValueChangeData> data1 = new List<ValueChangeData> { new ValueChangeData(0, 1), new ValueChangeData(1, 1) };
            List<ValueChangeData> data2 = new List<ValueChangeData> { new ValueChangeData(0, 0), new ValueChangeData(1, 0) };
            lineChart.Inject<ValueChangeData>(data1);
            lineChart.Inject<ValueChangeData>(data2);
            lineChart.ShowUnit();
        }
        private Color GetButtonColor(UnitAbnormalStateChangeData data)
        {
            if (data.isBuff)
            {
                return buffColors[data.BUFF_Type % buffColors.Count];
            }
            else
            {
                return abnormalStateColors[(int)data.CurrentAbnormalState % abnormalStateColors.Count];
            }
        }
    }
    public class ToEndAbnormalState
    {
        public UnitAbnormalStateChangeData changeData;
        public int position;

        public ToEndAbnormalState(UnitAbnormalStateChangeData changeData, int position)
        {
            this.changeData = changeData;
            this.position = position;
        }
    }
}