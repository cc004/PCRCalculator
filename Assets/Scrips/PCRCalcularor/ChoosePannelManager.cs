using System.Collections.Generic;
using PCRCaculator.Guild;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class ChoosePannelManager : MonoBehaviour
    {
        public static ChoosePannelManager Instance;
        public GameObject baseBack;
        public GameObject chooseBack_A;
        public GameObject settingBack;

        public RectTransform parent;
        public GameObject togglePerferb;
        public int original_hight;
        public Vector2 baseRange;//第一个按钮的位置
        public Vector2 range;//相邻按钮之间的距离
        public List<Toggle> switchToggles;

        public GameObject enemyGroups;
        public Text enemyTotalPointText;
        public List<CharacterPageButton> enemyChars;

        public List<CharacterPageButton> selectedChars;
        public Sprite defaultSprite;
        public Text nextButtonText;

        public Text totalPointText_setting;
        public List<CharacterPageButton> chars_setting;
        public List<Toggle> charToggles_setting;
        public List<TextMeshProUGUI> detailTexts_setting;
        public List<Slider> detailSliders_setting;
        public List<Button> detailButtons_setting;

        public GameObject EXsettingPannel;
        public List<SliderPrefab> EXsettingSliders;

        //private List<Toggle> togglePerferbs = new List<Toggle>();
        private List<int> selectedCharId = new List<int>();
        private Dictionary<int, Toggle> togglePerferbs = new Dictionary<int, Toggle>();
        private bool togglesEnable;
        private AddedPlayerData playerData = new AddedPlayerData();
        private int selectedCharacterId_setting;

        private AddedPlayerData playerDataForGuild;

        private bool isinstating = true;//是否正在初始化滑动条，是则忽略滑动条的回调事件
        private int type;
        private Dictionary<int, UnitData> unitDataDic => MainManager.Instance.unitDataDic;
        private void Awake()
        {
            Instance = this;
        }
        /// <summary>
        /// 调用选人面板的函数
        /// </summary>
        /// <param name="type">1-JJC开战选人，2-JJC编辑敌方队伍选人,3-工会战出战选人,4-公会战更改配置</param>
        /// <param name="playerData">敌人队伍/会战要更改的队伍，为空则不显示</param>
        public void CallChooseBack(int type, AddedPlayerData player = null)
        {
            this.type = type;
            baseBack.SetActive(true);
            chooseBack_A.SetActive(true);
            settingBack.SetActive(false);
            if (player == null || type == 4)
            {
                enemyGroups.SetActive(false);
            }
            else
            {
                enemyGroups.SetActive(true);
                enemyTotalPointText.text = player.totalpoint + "";
                for (int i = 0; i < 5; i++)
                {
                    if (player.playrCharacters.Count > i)
                    {
                        enemyChars[i].SetButton(player.playrCharacters[i]);
                    }
                    else
                    {
                        enemyChars[i].SetButton(-1);
                    }
                }
            }
            //selectedCharId.Clear();
            if(type == 4)
            {
                selectedCharId.Clear();
                for(int i = 0; i < 5; i++)
                {
                    if(i< player.playrCharacters.Count)
                    {
                        selectedCharId.Add(player.playrCharacters[i].unitId);
                    }
                }
                this.OpenProperty();
            }
            else if (PlayerPrefs.HasKey("selectedCharId"))
            {
                string id = PlayerPrefs.GetString("selectedCharId");
                try
                {
                    string[] ids = id.Split('-');
                    selectedCharId.Clear();
                    for (int i = 0; i < ids.Length; i++)
                    {
                        selectedCharId.Add(int.Parse(ids[i]));
                    }

                }
                catch
                {
                    Debug.LogError("读取预设阵容失败！");
                    selectedCharId.Clear();
                }
            }
            ReflashBasePage(0);
            switchToggles[0].isOn = true;
            ReflashSelectedButtons();
            if (type == 1)
            {
                nextButtonText.text = "战斗开始";
            }
            else if (type == 2 || type == 3 || type == 4)
            {
                nextButtonText.text = "下一步";
            }
            if(type == 4)
            {
                playerData = player;
                playerDataForGuild = player;
                NextButton(player);
            }
        }
        public void OnToggleSwitched(bool k)
        {
            if (k)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (switchToggles[i].isOn)
                    {
                        ReflashBasePage(i);
                    }
                }
            }
        }

        public void OpenProperty()
        {
            GuildManager.Instance.ActivateCharacterDetailPage(new Vector3(260, 0, 0), 0.6f);
            GuildManager.Instance.RefreshCharacterDetailPage(selectedCharacterId_setting);
        }

        public void CancalButton()
        {
            baseBack.SetActive(false);
            chooseBack_A.SetActive(false);
            settingBack.SetActive(false);
        }
        public void NextButton()
        {
            NextButton(null);
        }
        public void NextButton(AddedPlayerData playerData0 = null)
        {
            this.OpenProperty();
            if (selectedCharId.Count == 0)
            {
                MainManager.Instance.WindowMessage("请选择至少一个角色！");
                return;
            }
            string saveID = "";
            for(int i =0;i<selectedCharId.Count;i++)
            {
                saveID += selectedCharId[i];
                if (i < selectedCharId.Count - 1)
                {
                    saveID += "-";
                }
            }
            PlayerPrefs.SetString("selectedCharId", saveID);
            if (type == 1)
            {
                //MainManager.Instance.WindowMessage("战斗系统还没做好！");
                //CancalButton();
                JJCManager.Instance.ReadyAttack(selectedCharId);
            }
            else if (type >=2)
            {
                chooseBack_A.SetActive(false);
                settingBack.SetActive(true);
                isinstating = true;
                if (type == 4 && playerData0 != null)
                    playerData = playerData0;
                else if(type == 4)
                {
                    playerData = playerDataForGuild;
                    playerDataForGuild.playrCharacters.Clear();
                    foreach (int unitid in selectedCharId)
                    {
                        playerData.playrCharacters.Add(unitDataDic[unitid]);
                    }
                }
                else
                {
                    playerData = new AddedPlayerData();
                    foreach (int unitid in selectedCharId)
                    {
                        playerData.playrCharacters.Add(unitDataDic[unitid]);
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    charToggles_setting[i].interactable = i < selectedCharId.Count;
                    //charToggles_setting[i].isOn = i == 0;
                }
                ReflashSettingPage();
                ReflashSettingValues();
            }
            /*else if(type == 4)
            {
                chooseBack_A.SetActive(false);
                settingBack.SetActive(true);
                if (type == 4 && playerData == null)
                    playerData = playerData0;
                else
                    playerData = new AddedPlayerData();
                isinstating = true;
                for (int i = 0; i < 5; i++)
                {
                    charToggles_setting[i].interactable = i < selectedCharId.Count;
                    //charToggles_setting[i].isOn = i == 0;
                }
                ReflashSettingPage();
                ReflashSettingValues();

            }*/
        }
        public void BackButton()
        {
            chooseBack_A.SetActive(true);
            settingBack.SetActive(false);
        }
        public void FinishEditButton_setting()
        {
            CancalButton();
            if (type == 2)
            {
                JJCManager.Instance.FinishAddingNewPlayer(playerData);
            }
            else if(type == 3)
            {
                GuildManager.Instance.FinishEditingPlayers(playerData);
            }
            else if(type == 4)
            {
                GuildManager.Instance.FinishEditCharacterdetail();
            }

            foreach (var c in playerData.playrCharacters)
            {
                if (MainManager.Instance.unitDataDic.ContainsKey(c.unitId))
                    MainManager.Instance.unitDataDic[c.unitId] = c;
                MainManager.Instance.SaveUnitData();
            }
        }
        public void OnToggleSwitched(bool k, int unitid)
        {
            if (selectedCharId.Contains(unitid))
            {
                selectedCharId.Remove(unitid);
                if (!togglesEnable)
                {
                    TurnAllToggles(true);
                }
            }
            else if (selectedCharId.Count < 5)
            {
                selectedCharId.Add(unitid);
                if (selectedCharId.Count == 5)
                {
                    TurnAllToggles(false);
                }
            }
            else
            {
                MainManager.Instance.WindowMessage("最多选5个！");
            }
            ReflashSelectedButtons();
        }
        public void OnToggleSwitched_setting(int id)
        {
            foreach (Toggle a in charToggles_setting)
            {
                if (a.interactable && a.isOn)
                {
                    selectedCharacterId_setting = id;
                    ReflashSettingValues();
                }
            }
        }
        public void OnButtonPressed(int buttonid)
        {
            if (selectedCharId.Count > buttonid)
            {
                int id = selectedCharId[buttonid];
                if (togglePerferbs.ContainsKey(id))
                    togglePerferbs[id].isOn = false;
            }
            else
            {
                MainManager.Instance.WindowMessage("请选择角色");
            }
        }
        public void OnSliderDraged()
        {
            if (isinstating) { return; }
            //UnitData data = playerData.playrCharacters[selectedCharacterId_setting];
            Dictionary<int, int> loveDic = playerData.playrCharacters[selectedCharacterId_setting].playLoveDic;
            UnitData data = new UnitData(selectedCharId[selectedCharacterId_setting]);
            data.level = (int)detailSliders_setting[0].value;
            data.rarity = (int)detailSliders_setting[1].value;
            //data.love = (int)detailSliders_setting[2].value;
            for (int i = 3; i < 7; i++)
            {
                data.skillLevel[i - 3] = (int)detailSliders_setting[i].value;
            }
            data.rank = (int)detailSliders_setting[7].value;
            for (int i = 8; i < 14; i++)
            {
                data.equipLevel[i - 8] = (int)detailSliders_setting[i].value;
            }
            data.playLoveDic = loveDic;
            playerData.playrCharacters[selectedCharacterId_setting] = data;
            if (MainManager.Instance.JudgeWeatherAllowUniqueEq(data.unitId))
            {
                data.uniqueEqLv = (int)detailSliders_setting[14].value;
            }
            ReflashSettingPage();

            if (data.rank < 2) data.skillLevel[1] = 0;
            if (data.rank < 4) data.skillLevel[2] = 0;
            if (data.rank < 7)
            {
                data.skillLevel[3] = 0;
                data.uniqueEqLv = 0;
            }
            ReflashSettingValues();

        }
        public void AddButton_setting(int buttonid)
        {
            if (detailSliders_setting[buttonid].value < detailSliders_setting[buttonid].maxValue)
            {
                detailSliders_setting[buttonid].value++;
            }
            OnSliderDraged();
        }
        public void MinusButton_setting(int buttonid)
        {
            if (detailSliders_setting[buttonid].value > detailSliders_setting[buttonid].minValue)
            {
                detailSliders_setting[buttonid].value--;
            }
            OnSliderDraged();
        }
        public void SetSelectedUnitMax()
        {
            playerData.playrCharacters[selectedCharacterId_setting].SetMax();
            ReflashSettingValues();
            ReflashSettingPage();
            MainManager.Instance.WindowMessage("成功！");
        }
        public void SetAllUnitMax()
        {
            foreach(UnitData a in playerData.playrCharacters)
            {
                a.SetMax();
            }
            ReflashSettingValues();
            ReflashSettingPage();
            MainManager.Instance.WindowMessage("成功！");
        }
        public void OpenEXSettingPannel()
        {
            EXsettingPannel.SetActive(true);
            List<int> effectUnitList = MainManager.Instance.UnitStoryEffectDic[playerData.playrCharacters[selectedCharacterId_setting].unitId];
            for(int i = 0; i < EXsettingSliders.Count; i++)
            {
                if (i < effectUnitList.Count)
                {
                    var unitRarityData = MainManager.Instance.UnitRarityDic[effectUnitList[i]];
                    string unitname = unitRarityData.unitName;
                    if(MainManager.Instance.UnitName_cn.TryGetValue(effectUnitList[i],out string name0))
                    {
                        unitname = name0;
                    }
                    EXsettingSliders[i].SetSliderPrefab(
                        unitname,
                        playerData.playrCharacters[selectedCharacterId_setting].playLoveDic[effectUnitList[i]],
                        unitRarityData.GetMaxLoveLevel(),
                        0,
                        null);
                }
                else
                {
                    EXsettingSliders[i].SetSliderPrefab("???", 0, 0, 0, null);
                }
            }
        }
        public void OnFinishEXSettings()
        {
            EXsettingPannel.SetActive(false);
            List<int> effectUnitList = MainManager.Instance.UnitStoryEffectDic[playerData.playrCharacters[selectedCharacterId_setting].unitId];
            Dictionary<int, int> loveDic = new Dictionary<int, int>();
            for (int i = 0; i < EXsettingSliders.Count; i++)
            {
                if (i < effectUnitList.Count)
                {
                    loveDic.Add(effectUnitList[i], EXsettingSliders[i].GetValue());
                }
            }
            playerData.playrCharacters[selectedCharacterId_setting].playLoveDic = loveDic;
            GuildManager.Instance.RefreshCharacterDetailPage(selectedCharacterId_setting);
        }

        private void TurnAllToggles(bool k)
        {
            togglesEnable = k;
            foreach (Toggle toggle in togglePerferbs.Values)
            {
                if (!toggle.isOn)
                {
                    toggle.enabled = k;
                }
            }
        }
        private void ReflashBasePage(int type)
        {
            PositionType positionType = PositionType.frount;
            switch (type)
            {
                case 2:
                    positionType = PositionType.middle;
                    break;
                case 3:
                    positionType = PositionType.backword;
                    break;
            }
            foreach (Toggle a in togglePerferbs.Values)
            {
                Destroy(a.gameObject);
            }
            togglePerferbs.Clear();
            parent.localPosition = new Vector3();
            parent.sizeDelta = new Vector2(100, original_hight);
            int count = 1;
            foreach (int id in MainManager.Instance.UnitRarityDic.Keys)
            {
                if (type == 0 || MainManager.Instance.UnitRarityDic[id].unitPositionType == positionType)
                {
                    if (MainManager.Instance.JudgeWeatherShowThisUnit(id))// || (id>=400000&&id<=499999))
                    {
                        GameObject b = Instantiate(togglePerferb);
                        b.transform.SetParent(parent);
                        b.transform.localScale = new Vector3(1, 1, 1);
                        b.transform.localPosition = new Vector3(baseRange.x + range.x * ((count - 1) % 8), -1 * (baseRange.y + range.y * (Mathf.FloorToInt((count - 1) / 8))), 0);
                        int id0 = id;
                        if (selectedCharId.Contains(id))
                        {
                            b.GetComponent<Toggle>().isOn = true;
                        }
                        b.GetComponent<Toggle>().onValueChanged.AddListener(value => OnToggleSwitched(value, id0));
                        b.GetComponent<CharacterPageButton>().SetButton(id);
                        togglePerferbs.Add(id0, b.GetComponent<Toggle>());
                        count++;
                        //showUnitIDs.Add(id);

                    }
                }
            }
            if (parent.sizeDelta.y <= Mathf.CeilToInt(count / 8) * 95 + 105)
            {
                parent.sizeDelta = new Vector2(100, Mathf.CeilToInt(count / 8) * 95 + 105);
            }
        }
        private void ReflashSelectedButtons()
        {
            selectedCharId.Sort((x, y) => MainManager.Instance.UnitRarityDic[x].CompareTo(MainManager.Instance.UnitRarityDic[y]));
            for (int i = 0; i < 5; i++)
            {
                if (selectedCharId.Count > i)
                {
                    selectedChars[i].SetButton(selectedCharId[i]);
                }
                else
                {
                    selectedChars[i].SetButton(-1, defaultSprite);
                }
            }
        }
        private void ReflashSettingPage()
        {
            for (int i = 0; i < 5; i++)
            {
                if (playerData.playrCharacters.Count > i)
                {
                    chars_setting[i].SetButton(playerData.playrCharacters[i]);
                }
                else
                {
                    chars_setting[i].SetButton(-1);
                }
            }

        }
        private void ReflashSettingValues()
        {
            isinstating = true;
            UnitData data = playerData.playrCharacters[selectedCharacterId_setting];
            detailTexts_setting[0].text = data.level + "";
            detailSliders_setting[0].maxValue = MainManager.Instance.levelMax;
            detailSliders_setting[0].value = data.level;
            detailTexts_setting[1].text = data.rarity + "";
            detailSliders_setting[1].maxValue = data.GetMaxRarity();
            detailSliders_setting[1].value = data.rarity;
            //detailTexts_setting[2].text = data.love + "";
            //detailSliders_setting[2].maxValue = data.GetMaxRarity() == 6 ? 12 : 8;
            //detailSliders_setting[2].value = data.love;
            for (int i = 3; i < 7; i++)
            {
                detailTexts_setting[i].text = data.skillLevel[i - 3] + "";
                detailSliders_setting[i].maxValue = data.level;
                detailSliders_setting[i].value = data.skillLevel[i - 3];

            }
            detailTexts_setting[7].text = data.rank + "";
            detailSliders_setting[7].maxValue = MainManager.Instance.PlayerSetting.GetMaxRank();
            detailSliders_setting[7].value = data.rank;
            for (int i = 8; i < 14; i++)
            {
                detailTexts_setting[i].text = data.equipLevel[i - 8] + "";
                detailSliders_setting[i].value = data.equipLevel[i - 8];

            }
            if (MainManager.Instance.JudgeWeatherAllowUniqueEq(data.unitId))
            {
                detailTexts_setting[14].text = data.uniqueEqLv + "";
                detailSliders_setting[14].maxValue = MainManager.Instance.PlayerSetting.maxUniqueEqLv;
                detailSliders_setting[14].value = data.uniqueEqLv;
                detailSliders_setting[14].interactable = true;
                detailButtons_setting[0].interactable = true;
                detailButtons_setting[1].interactable = true;
            }
            else
            {
                detailTexts_setting[14].text = "null";
                detailSliders_setting[14].maxValue = 0;
                detailSliders_setting[14].value = 0;
                detailSliders_setting[14].interactable = false;
                detailButtons_setting[0].interactable = false;
                detailButtons_setting[1].interactable = false;
            }

            float to = 0;
            foreach (UnitData a in playerData.playrCharacters)
            {
                to += MainManager.Instance.UnitRarityDic[a.unitId].GetPowerValue(a,false,true);
            }
            totalPointText_setting.text = (int)to + "";
            playerData.totalpoint = (int)to;
            isinstating = false;

            if (data.rank < 2) detailSliders_setting[4].maxValue = 0;
            if (data.rank < 4) detailSliders_setting[5].maxValue = 0;
            if (data.rank < 7)
            {
                detailSliders_setting[6].maxValue = 0;
                detailSliders_setting[14].maxValue = 0;
            }
            GuildManager.Instance.RefreshCharacterDetailPage(selectedCharacterId_setting);
        }
    }
}