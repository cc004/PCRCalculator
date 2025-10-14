using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elements;
using PCRCaculator.Guild;
using PCRCaculator.SQL;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using TinyPinyin;
using UnityEngine.Networking;
namespace PCRCaculator
{
    public class ChoosePannelManager : MonoBehaviour
    {
        private class ExEquipOption : Dropdown.OptionData
        {
            public int equip_id;

            public ExEquipOption(ex_equipment_data data)
            {
                var color = data.rarity switch
                {
                    1 => "铜",
                    2 => "银",
                    3 => "金",
                    4 => "粉",
                    5 => "彩",
                    _ => "未知"
                };
                equip_id = data.ex_equipment_id;
                text = $"{color}-{data.name}";
                image = Resources.Load<Sprite>($"extraequip/icon_extra_equip_{equip_id}");
            }

            private ExEquipOption()
            {
                equip_id = 0;
                text = "未装备";
            }

            public static ExEquipOption None = new ExEquipOption();
        }

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
        public List<Toggle> talentToggles;

        // public GameObject enemyGroups;
        // public Text enemyTotalPointText;
        //public List<CharacterPageButton> enemyChars;

        public List<CharacterPageButton> selectedChars;
        public Sprite defaultSprite;
        public Text nextButtonText;

        public Text totalPointText_setting;
        public List<CharacterPageButton> chars_setting;
        public List<Toggle> charToggles_setting;
        public List<TextMeshProUGUI> detailTexts_setting;
        public List<Slider> detailSliders_setting;
        public List<Button> detailButtons_setting;

        public Dropdown[] ExEquip;

        public GameObject EXsettingPannel;
        public List<SliderPrefab> EXsettingSliders;

        private List<int> selectedCharId = new List<int>();
        private Dictionary<int, Toggle> togglePerferbs = new Dictionary<int, Toggle>();
        private bool togglesEnable;
        private AddedPlayerData playerData = new AddedPlayerData();
        private int selectedCharacterId_setting;

        private AddedPlayerData playerDataForGuild;

        private bool isinstating = true;//是否正在初始化滑动条，是则忽略滑动条的回调事件
        private int type;
        private Dictionary<int, UnitData> unitDataDic => MainManager.Instance.unitDataDic;
        public InputField Search;
        public Toggle filterToggle;
        public Text filterType;
        public GameObject position;
        public GameObject talent;
        public Toggle isSetMaxExEquip;
        private bool initialized = false;
        [System.Serializable]
        public class SubItemData
        {
            public Dropdown typeOption;
            public Dropdown valueOption;
        }

        [System.Serializable]
        public class SubGroup
        {
            public SubItemData sub1 = new SubItemData();
            public SubItemData sub2 = new SubItemData();
            public SubItemData sub3 = new SubItemData();
            public SubItemData sub4 = new SubItemData();
        }

        public List<SubGroup> subs = new List<SubGroup>();
        public GameObject subGroupsContainer;
        private Dictionary<int, param_type> paramTypeDict => MainManager.Instance.ParamTypeDic;
        private void Start()
        {
            Instance = this;
            StartCoroutine(SearchCharaInitialize());
            // StartCoroutine(WaitForLoadFinished());
            for (int i = 0; i < ExEquip.Length; i++)
            {
                int index = i;
                ExEquip[i].onValueChanged.AddListener(delegate { OnExEquipChanged(index); });
            }
        }

        private IEnumerator WaitForLoadFinished(Action callback = null)
        {
            while (!MainManager.Instance.LoadFinished)
            {
            yield return null;
            }
            var wait = MainManager.Instance.OpenWaitUI();
            yield return StartCoroutine(InitialButtonCoroutine(wait));
            wait.Close();
            initialized = true;
            callback?.Invoke();
        }

        public IEnumerator InitialButtonCoroutine(WaitUI wait)
        {
            parent.localPosition = Vector3.zero;
            parent.sizeDelta = new Vector2(100, original_hight);

            int total = MainManager.Instance.UnitRarityDic.Count;
            int cnt = 1;
            int batchSize = 5; // 每帧加载多少个角色，可根据实际情况调整

            foreach (int id in MainManager.Instance.UnitRarityDic.Keys)
            {

                if (MainManager.Instance.JudgeWeatherShowThisUnit(id))
                {
                    GameObject b = Instantiate(togglePerferb);
                    b.transform.SetParent(parent, false); // 使用 false 避免本地坐标错乱
                    b.transform.localScale = Vector3.one;
                    int bundleCountBefore = ABExTool.AssetBundleDic.Count;

                    int id0 = id;
                    b.GetComponent<Toggle>().onValueChanged.AddListener(value => OnToggleSwitched(value, id0));
                    b.GetComponent<CharacterPageButton>().SetButton(id);

                    togglePerferbs[id0] = b.GetComponent<Toggle>();
                    buttons[id0] = b.GetComponent<CharacterPageButton>();
                    int bundleCountAfter = ABExTool.AssetBundleDic.Count;
                    if (bundleCountAfter > bundleCountBefore)
                    {
                        cnt++;
                        try
                        {
                            wait?.SetMessage($"正在加载角色头像 ({cnt}/{total})");
                        }
                        catch { }
                    }
                }

                // **关键：每加载一批后等待一帧，避免卡顿**
                if (cnt % batchSize == 0)
                {
                    yield return null;
                }
            }
        }



        // public void InitialButton(WaitUI wait)
        // {
        //     parent.localPosition = new Vector3();
        //     parent.sizeDelta = new Vector2(100, original_hight);
        //     int cnt = 0;
        //     foreach (int id in MainManager.Instance.UnitRarityDic.Keys)
        //     {
        //         /*if (type == 0 || MainManager.Instance.UnitRarityDic[id].unitPositionType == positionType)*/
        //         {
        //             try { wait?.SetMessage($"正在加载角色头像({cnt}/{MainManager.Instance.UnitRarityDic.Count})"); } catch { }
        //             if (MainManager.Instance.JudgeWeatherShowThisUnit(id)) // || (id>=400000&&id<=499999))
        //             {
        //                 GameObject b = Instantiate(togglePerferb);
        //                 b.transform.SetParent(parent);
        //                 b.transform.localScale = new Vector3(1, 1, 1);
        //                 int id0 = id;

        //                 b.GetComponent<Toggle>().onValueChanged.AddListener(value => OnToggleSwitched(value, id0));
        //                 b.GetComponent<CharacterPageButton>().SetButton(id);
        //                 togglePerferbs[id0] = b.GetComponent<Toggle>();
        //                 buttons[id0] = b.GetComponent<CharacterPageButton>();
        //                 //showUnitIDs.Add(id);
        //             }
        //         }
        //     }

        // }


        /// <summary>
        /// 调用选人面板的函数
        /// </summary>
        /// <param name="type">1-JJC开战选人，2-JJC编辑敌方队伍选人,3-工会战出战选人,4-公会战更改配置</param>
        /// <param name="playerData">敌人队伍/会战要更改的队伍，为空则不显示</param>
        public void CallChooseBack(int type, AddedPlayerData player = null, int selectedId = 0)
        {
            if (type != 4 && !initialized)
            {
                StartCoroutine(WaitForLoadFinished(() => CallChooseBack_0(type, player, selectedId)));
                return;
            }
            CallChooseBack_0(type, player, selectedId);
        }
        private void CallChooseBack_0(int type, AddedPlayerData player = null, int selectedId = 0)
        {
            this.type = type;
            baseBack.SetActive(true);
            if (type == 4)
            {
                if (selectedId >= player.playrCharacters.Count)
                {
                    selectedId = player.playrCharacters.Count - 1;
                }
                selectedCharacterId_setting = selectedId;
                selectedCharId.Clear();
                for (int i = 0; i < 5; i++)
                {
                    if (i < player.playrCharacters.Count)
                    {
                        selectedCharId.Add(player.playrCharacters[i].unitId);
                    }
                }
            }
            else
            {
                chooseBack_A.SetActive(true);
                settingBack.SetActive(false);

                if (PlayerPrefs.HasKey("selectedCharId"))
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
                RefreshBasePage();
            }
            // if (player == null || type == 4)
            // {
            // enemyGroups.SetActive(false);
            // }
            // else
            // {
            // enemyGroups.SetActive(true);
            /*enemyTotalPointText.text = player.totalpoint + "";
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
            }*/
            // }
            RefreshSelectedButtons();
            nextButtonText.text = "下一步";
            if (type == 4)
            {
                playerData = player;
                playerDataForGuild = player;
                NextButton(player);
            }
        }
        public void OnToggleSwitched(bool k)
        {
            if (!isActiveAndEnabled) return;
            if (k)
            {
                RefreshBasePage();
            }
        }

        public void OpenProperty()
        {
            GuildManager.Instance.ActivateCharacterDetailPage(new Vector3(260, 0, 0), 0.6f); // 应该放到mainManager
        }
        public void CloseProperty()
        {
            GuildManager.Instance.HideCharacterDetailPage(); // 应该放到mainManager
        }
        public void CancalButton()
        {
            baseBack.SetActive(false);
            chooseBack_A.SetActive(false);
            settingBack.SetActive(false);
            CloseProperty();
        }
        public void NextButton()
        {
            NextButton(null);
        }
        public void NextButton(AddedPlayerData playerData0 = null)
        {
            if (selectedCharId.Count == 0)
            {
                MainManager.Instance.WindowMessage("请选择至少一个角色！");
                return;
            }
            this.OpenProperty();
            string saveID = "";
            for (int i = 0; i < selectedCharId.Count; i++)
            {
                saveID += selectedCharId[i];
                if (i < selectedCharId.Count - 1)
                {
                    saveID += "-";
                }
            }
            PlayerPrefs.SetString("selectedCharId", saveID);
            chooseBack_A.SetActive(false);
            settingBack.SetActive(true);
            isinstating = true;
            if (type == 4 && playerData0 != null)
                playerData = playerData0;
            else if (type == 4)
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
            if (type == 4 && !charToggles_setting[selectedCharacterId_setting].isOn)
            {
                charToggles_setting[selectedCharacterId_setting].isOn = true; // auto refresh
            }
            else
            {
                RefreshSettingValues();
            }
            RefreshSettingPage();
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
                RefreshSettingPage();
                RefreshSettingValues();

            }*/
        }
        public void BackButton()
        {
            if (!initialized)
            {
                StartCoroutine(WaitForLoadFinished(() => RealBackButton()));
                return;
            }
            RealBackButton();
        }

        private void RealBackButton()
        {
            CloseProperty();
            RefreshBasePage();
            chooseBack_A.SetActive(true);
            settingBack.SetActive(false);
        }

        public void FinishEditButton_setting()
        {
            CancalButton();
            if (type == 1)
            {
                JJCManager.Instance.ReadyAttack(selectedCharId);
            }
            else if (type == 2)
            {
                JJCManager.Instance.FinishAddingNewPlayer(playerData);
            }
            else if (type == 3)
            {
                GuildManager.Instance.FinishEditingPlayers(playerData);
            }
            else if (type == 4)
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
            if (selectedCharId.Contains(unitid) ^ !k) return;

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
                togglePerferbs[unitid].isOn = false;
                MainManager.Instance.WindowMessage("最多选择5名角色！");
            }
            RefreshSelectedButtons();
        }
        public bool isSwitchingRole = false;
        public void OnToggleSwitched_setting(int id)
        {
            isSwitchingRole = true;
            if (charToggles_setting[id].isOn)
            {
                selectedCharacterId_setting = id;
                RefreshSettingValues();
            }
            isSwitchingRole = false;
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
            if (MainManager.Instance.JudgeWeatherAllowUniqueEq2(data.unitId))
            {
                data.uniqueEq2Lv = (int)detailSliders_setting[15].value;
            }
            if (data.rank < 2) data.skillLevel[1] = 0;
            if (data.rank < 4) data.skillLevel[2] = 0;
            if (data.rank < 7)
            {
                data.skillLevel[3] = 0;
                data.uniqueEqLv = 0;
            }
            if (data.uniqueEqLv == 0)
            {
                data.uniqueEq2Lv = -1;
            }

            if (MainManager.Instance.UnitExEquips.TryGetValue(data.unitId, out var temp))
            {
                for (int i = 0; i < 3; ++i)
                {
                    data.exEquip[i] = (ExEquip[i].options[ExEquip[i].value] as ExEquipOption).equip_id;
                    var maxStar = data.exEquip[i] == 0 ? 0 : temp[i][data.exEquip[i]].levelMax;
                    data.exEquipLevel[i] = Math.Min((int)detailSliders_setting[16 + i].value, maxStar);
                    if (i < subs.Count)
                    {
                        var subGroup = subs[i];
                        var subItems = new[] { subGroup.sub1, subGroup.sub2, subGroup.sub3, subGroup.sub4 };

                        // 格式: [type0, value0, type1, value1, type2, value2, type3, value3]
                        int[] subItemValues = new int[8];

                        for (int j = 0; j < 4; j++)
                        {
                            if (j < subItems.Length)
                            {
                                var subItem = subItems[j];
                                subItemValues[j * 2] = subItem.typeOption.value;     // typeOption值
                                subItemValues[j * 2 + 1] = subItem.valueOption.value; // valueOption值
                            }
                            else
                            {
                                subItemValues[j * 2] = -1;
                                subItemValues[j * 2 + 1] = -1;
                            }
                        }

                        data.exEquipSubs[i] = subItemValues;
                    }
                    else
                    {
                        data.exEquipSubs[i] = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };
                    }
                }
            }
            RefreshSettingPage();
            RefreshSettingValues();

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
        private void SetMaxLoveForUnit(UnitData unitData)
        {
            if (MainManager.Instance.UnitStoryEffectDic2.TryGetValue(unitData.unitId, out var effectUnitList))
            {
                foreach (int effectUnitId in effectUnitList)
                {
                    if (MainManager.Instance.UnitStoryEffectDic2.ContainsKey(effectUnitId) &&
                        MainManager.Instance.UnitRarityDic.TryGetValue(effectUnitId, out var unitRarityData))
                    {
                        unitData.playLoveDic[effectUnitId] = unitRarityData.GetMaxLoveLevel();
                    }
                }
            }
        }
        public void SetSelectedUnitSecondMax()
        {
            playerData.playrCharacters[selectedCharacterId_setting].SetMax(GuildManager.Instance.IsGuildBoss, isSetMaxExEquip.isOn, equip_second_full: true);
            SetMaxLoveForUnit(playerData.playrCharacters[selectedCharacterId_setting]);
            RefreshSettingValues();
            RefreshSettingPage();
            MainManager.Instance.WindowMessage("成功！");
        }
        public void SetSelectedUnitMax()
        {
            playerData.playrCharacters[selectedCharacterId_setting].SetMax(GuildManager.Instance.IsGuildBoss, isSetMaxExEquip.isOn);
            SetMaxLoveForUnit(playerData.playrCharacters[selectedCharacterId_setting]);
            RefreshSettingValues();
            RefreshSettingPage();
            MainManager.Instance.WindowMessage("成功！");
        }
        public void SetAllUnitMax()
        {
            foreach (UnitData a in playerData.playrCharacters)
            {
                a.SetMax(GuildManager.Instance.IsGuildBoss, isSetMaxExEquip.isOn);
                SetMaxLoveForUnit(a);
            }
            RefreshSettingValues();
            RefreshSettingPage();
            MainManager.Instance.WindowMessage("成功！");
        }
        public void OpenEXSettingPannel()
        {
            isSwitchingRole = true;
            UnitData unit = playerData.playrCharacters[selectedCharacterId_setting];
            List<int> effectUnitList = MainManager.Instance.UnitStoryEffectDic[unit.unitId];
            for (int i = 0; i < EXsettingSliders.Count; i++)
            {
                EXsettingSliders[i].SetActive(true);
                if (i < effectUnitList.Count)
                {
                    var unitRarityData = MainManager.Instance.UnitRarityDic[effectUnitList[i]];
                    string unitname = unitRarityData.unitName;
                    if (MainManager.Instance.UnitName_cn.TryGetValue(effectUnitList[i], out string name0))
                    {
                        unitname = name0;
                    }
                    unit.SetDefaultLoveDict();
                    int value = unit.playLoveDic.TryGetValue(effectUnitList[i], out var val) ? val : 0;
                    EXsettingSliders[i].SetSliderPrefab(
                        unitname,
                        value,
                        unitRarityData.GetMaxLoveLevel(),
                        0,
                        null);
                }
                else
                {
                    EXsettingSliders[i].SetSliderPrefab("???", 0, 0, 0, null);
                    EXsettingSliders[i].SetActive(false);
                }
            }
            isSwitchingRole = false;
        }
        public void OnFinishEXSettings()
        {
            List<int> effectUnitList = MainManager.Instance.UnitStoryEffectDic[playerData.playrCharacters[selectedCharacterId_setting].unitId];
            Dictionary<int, int> loveDic = new Dictionary<int, int>();
            for (int i = 0; i < EXsettingSliders.Count; i++)
            {
                if (i < effectUnitList.Count)
                {
                    loveDic.Add(effectUnitList[i], EXsettingSliders[i].GetValue());
                }
            }
            var unitdata = playerData.playrCharacters[selectedCharacterId_setting];
            unitdata.playLoveDic = loveDic;
            GuildManager.Instance.RefreshCharacterDetailPage(selectedCharacterId_setting, unitdata);
            RefreshSettingPage();
        }

        private void TurnAllToggles(bool k)
        {
            togglesEnable = k;
            foreach (Toggle toggle in togglePerferbs.Values)
            {
                if (!toggle.isOn && toggle != null)
                {
                    toggle.enabled = k;
                }
            }
        }

        public Dictionary<int, CharacterPageButton> buttons = new Dictionary<int, CharacterPageButton>();
        public Dictionary<int, CharacterPageButton> searchButtons = new Dictionary<int, CharacterPageButton>();

        private bool shouldDisplay(int unitid, UnitData unitData)
        {
            var pinyin = PinyinHelper.GetPinyin(Search.text, "").ToUpper();
            bool search = Search.text == "" 
                || (id2alias.ContainsKey(unitid) && id2alias[unitid]?.Contains(Search.text) == true) 
                || (id2pinyin.ContainsKey(unitid) && id2pinyin[unitid]?.Contains(pinyin) == true);
            MainManager.Instance.UnitRarityDic.TryGetValue(unitid, out var rarityData);
            if (filterToggle.isOn)
            {
                int talent_id = rarityData.detailData.talent_id?.FirstOrDefault() ?? 0;
                bool hasTalent = talent_id >= 0 && talentToggles[talent_id].isOn || talentToggles[0].isOn;
                return hasTalent && search;
            }
            else
            {
                int pos = (int)rarityData.unitPositionType;
                bool position = switchToggles[0].isOn || switchToggles[pos + 1].isOn;
                return position && search;
            }
        }

        private int firstShowUnitId;

        private void RefreshBasePage()
        {
            int count = 1;
            int displayCount = 0;
            firstShowUnitId = -1;

            foreach (int id in buttons.Keys)
            {
                UnitData unitData;
                if (!MainManager.Instance.unitDataDic.TryGetValue(id, out unitData))
                {
                    unitData = new UnitData(id, 1);
                }

                if (shouldDisplay(id, unitData))
                {
                    buttons[id].RefreshData(unitData);
                    buttons[id].transform.localPosition = new Vector3(
                        baseRange.x + range.x * ((count - 1) % 8),
                        -1 * (baseRange.y + range.y * Mathf.FloorToInt((count - 1) / 8)), 0);
                    togglePerferbs[id].isOn = selectedCharId.Contains(id);
                    buttons[id].gameObject.SetActive(true);
                    if (count == 1)
                        firstShowUnitId = id;
                    count++;
                    displayCount++;
                }
                else
                {
                    buttons[id].gameObject.SetActive(false);
                }
            }
            int neededHeight = Mathf.CeilToInt(displayCount / 8f) * 95;
            parent.sizeDelta = new Vector2(100, neededHeight);

            if (parent.localPosition.y > Mathf.CeilToInt(count / 8 - 2) * 95)
            {
                parent.SetLocalPosY(Mathf.CeilToInt(count / 8 - 2) * 95);
            }
            Canvas.ForceUpdateCanvases();
            TurnAllToggles(selectedCharId.Count != 5);
        }

        private void RefreshSelectedButtons()
        {
            if (selectedCharId.Any(x => !MainManager.Instance.UnitRarityDic.ContainsKey(x)))
                selectedCharId.Clear();
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
        private void RefreshSettingPage()
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
        int lastRefreshedId = -1;
        public void RefreshSettingValues()
        {
            isinstating = true;
            if (selectedCharacterId_setting >= playerData.playrCharacters.Count)
            {
                selectedCharacterId_setting = playerData.playrCharacters.Count - 1;
            }
            UnitData data = playerData.playrCharacters[selectedCharacterId_setting];
            bool changingId = lastRefreshedId != data.unitId;
            lastRefreshedId = data.unitId;
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

            if (MainManager.Instance.JudgeWeatherAllowUniqueEq2(data.unitId))
            {
                detailTexts_setting[15].text = data.uniqueEq2Lv + "";
                detailSliders_setting[15].maxValue = 5;
                detailSliders_setting[15].value = data.uniqueEq2Lv;
                detailSliders_setting[15].interactable = true;
                detailButtons_setting[2].interactable = true;
                detailButtons_setting[3].interactable = true;
            }
            else
            {
                detailTexts_setting[15].text = "null";
                detailSliders_setting[15].maxValue = -1;
                detailSliders_setting[15].value = -1;
                detailSliders_setting[15].interactable = false;
                detailButtons_setting[2].interactable = false;
                detailButtons_setting[3].interactable = false;
            }

            // ex equip

            if (MainManager.Instance.UnitExEquips.TryGetValue(data.unitId, out var temp))
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (changingId)
                    {
                        ExEquip[i].ClearOptions();
                        var optionsEx = temp[i].Values.ToList();
                        optionsEx.Sort((x, y) =>
                        {
                            int flagComparison = y.clan_battle_equip_flag.CompareTo(x.clan_battle_equip_flag);
                            if (flagComparison == 0)
                            {
                                return y.rarity.CompareTo(x.rarity);
                            }
                            return flagComparison;
                        });
                        ExEquip[i].AddOptions(optionsEx
                            .Select(x => new ExEquipOption(x)).Prepend(ExEquipOption.None)
                            .ToList<Dropdown.OptionData>());
                        OnExEquipChanged(i);
                    }
                    ExEquip[i].value = Math.Max(0, ExEquip[i].options.FindIndex(e => e is ExEquipOption e2 && e2.equip_id == data.exEquip[i]));
                    var subItems = new[] { subs[i].sub1, subs[i].sub2, subs[i].sub3, subs[i].sub4 };
                    if (ExEquip[i].value > 0 && data.exEquipSubs.Count > 0)
                    {
                        for (int k = 0; k < 4; ++k)
                        {
                            int statusIndex = data.exEquipSubs[i][k * 2];
                            int statusLevel = data.exEquipSubs[i][k * 2 + 1];
                            subItems[k].typeOption.value = statusIndex;
                            subItems[k].valueOption.value = statusLevel;
                        }
                    }
                    else
                    {
                        for (int k = 0; k < 4; ++k)
                        {
                            subItems[k].typeOption.SetActive(false);
                            subItems[k].valueOption.SetActive(false);
                        }
                    }
                    detailSliders_setting[16 + i].maxValue = data.exEquip[i] == 0 ? 0 : temp[i][data.exEquip[i]].levelMax;
                    detailSliders_setting[16 + i].minValue = 0;
                    detailSliders_setting[16 + i].value = data.exEquipLevel[i];
                    detailTexts_setting[16 + i].text = detailSliders_setting[16 + i].value.ToString();
                }
            }
            else
            {
                for (int i = 0; i < 3; ++i)
                {
                    ExEquip[i].ClearOptions();
                    ExEquip[i].AddOptions(new List<Dropdown.OptionData> { ExEquipOption.None });
                    detailSliders_setting[16 + i].maxValue = 0;
                    detailSliders_setting[16 + i].minValue = 0;
                    detailSliders_setting[16 + i].value = 0;
                    detailTexts_setting[16 + i].text = "0";
                }
            }

            float to = 0;
            foreach (UnitData a in playerData.playrCharacters)
            {
                to += MainManager.Instance.UnitRarityDic[a.unitId].GetPowerValue(a, false, true);
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
            OpenEXSettingPannel();
            GuildManager.Instance?.RefreshCharacterDetailPage(selectedCharacterId_setting, data);
        }
        // 当ExEquip下拉框选项改变时调用
        public void OnExEquipChanged(int equipIndex)
        {
            UpdateTypeOptions(equipIndex);

            for (int i = 0; i < 4; i++)
            {
                UpdateValueOptions(equipIndex, i);
            }
        }

        // 更新typeOption选项
        private void UpdateTypeOptions(int equipIndex)
        {
            if (equipIndex < 0 || equipIndex >= ExEquip.Length || equipIndex >= subs.Count) return;

            var exEquipDropdown = ExEquip[equipIndex];
            var subGroup = subs[equipIndex];
            var subItems = new[] { subGroup.sub1, subGroup.sub2, subGroup.sub3, subGroup.sub4 };

            // 获取当前选中的装备选项
            var selectedOption = exEquipDropdown.options[exEquipDropdown.value] as ExEquipOption;
            if (selectedOption.equip_id == 0) // 未装备
            {
                // 清空所有typeOption和valueOption
                foreach (var subItem in subItems)
                {
                    subItem.typeOption.ClearOptions();
                    subItem.valueOption.ClearOptions();
                }
                return;
            }

            // 获取对应装备的数据
            if (MainManager.Instance.UnitExEquips.TryGetValue(
                playerData.playrCharacters[selectedCharacterId_setting].unitId,
                out var exEquipDict) && exEquipDict.Length > equipIndex)
            {
                if (exEquipDict[equipIndex].TryGetValue(selectedOption.equip_id, out var exEquipData))
                {
                    if (exEquipData.status.Count > 0)
                    {
                        // 为每个subItem更新typeOption
                        for (int i = 0; i < subItems.Length; i++)
                        {
                            var subItem = subItems[i];
                            var statusKeys = exEquipData.status.Keys.Select(k =>
                            {
                                if (k is Enum enumKey)
                                {
                                    return enumKey.GetDescription();
                                }
                                return k.ToString();
                            }).ToList();
                            subItem.typeOption.SetActive(true);
                            subItem.valueOption.SetActive(true);
                            subItem.typeOption.ClearOptions();
                            subItem.typeOption.AddOptions(statusKeys);

                            // 添加typeOption变更监听
                            subItem.typeOption.onValueChanged.RemoveAllListeners();
                            int subItemIndex = i; // 闭包副本
                            subItem.typeOption.onValueChanged.AddListener(delegate
                            {
                                UpdateValueOptions(equipIndex, subItemIndex);
                            });
                        }
                    }
                    else
                    {
                        // 没有status数据，清空选项
                        foreach (var subItem in subItems)
                        {
                            subItem.typeOption.ClearOptions();
                            subItem.valueOption.ClearOptions();
                            subItem.typeOption.SetActive(false);
                            subItem.valueOption.SetActive(false);
                        }
                    }
                }
            }
        }

        // 更新valueOption选项
        private void UpdateValueOptions(int equipIndex, int subItemIndex)
        {
            if (equipIndex < 0 || equipIndex >= ExEquip.Length ||
                equipIndex >= subs.Count || subItemIndex < 0 ||
                subItemIndex >= 4) return;

            var exEquipDropdown = ExEquip[equipIndex];
            var subGroup = subs[equipIndex];
            var subItem = subItemIndex switch
            {
                0 => subGroup.sub1,
                1 => subGroup.sub2,
                2 => subGroup.sub3,
                3 => subGroup.sub4,
                _ => null
            };

            if (subItem == null) return;

            // 获取当前选中的装备选项
            var selectedOption = exEquipDropdown.options[exEquipDropdown.value] as ExEquipOption;
            if (selectedOption.equip_id == 0) // 未装备
            {
                subItem.valueOption.ClearOptions();
                return;
            }

            // 获取对应装备的数据
            if (MainManager.Instance.UnitExEquips.TryGetValue(
                playerData.playrCharacters[selectedCharacterId_setting].unitId,
                out var exEquipDict) && exEquipDict.Length > equipIndex)
            {
                if (exEquipDict[equipIndex].TryGetValue(selectedOption.equip_id, out var exEquipData))
                {
                    if (exEquipData.status != null && exEquipData.status.Count > 0)
                    {
                        // 获取当前选中的typeOption键
                        var statusKeys = exEquipData.status.Keys.ToList();
                        if (subItem.typeOption.value < statusKeys.Count)
                        {
                            var selectedKey = statusKeys[subItem.typeOption.value];
                            if (exEquipData.status.TryGetValue(selectedKey, out var values))
                            {
                                // 使用paramTypeDict来格式化值
                                var valueOptions = new List<string>();

                                for (int i = 0; i < values.Length; i++)
                                {
                                    int rawValue = values[i];
                                    string formattedValue = rawValue.ToString();

                                    // 获取参数配置并应用百分比格式
                                    if (paramTypeDict.TryGetValue((int)selectedKey, out var paramConfig))
                                    {
                                        float processedValue;
                                        if (paramConfig.ratio > 0)
                                            processedValue = (float)rawValue / paramConfig.ratio;
                                        else
                                            processedValue = rawValue;

                                        if (paramConfig.is_percentage == 1)
                                            formattedValue = $"{processedValue * 100}%";
                                        else
                                            formattedValue = processedValue.ToString("F0");
                                    }

                                    valueOptions.Add(formattedValue);
                                }

                                subItem.valueOption.ClearOptions();
                                subItem.valueOption.AddOptions(valueOptions);
                            }
                        }
                    }
                    else
                    {
                        subItem.valueOption.ClearOptions();
                    }
                }
            }
        }

        private Dictionary<int, string> id2alias;
        private Dictionary<int, string> id2pinyin;
        private IEnumerator SearchCharaInitialize()
        {
            id2alias = new Dictionary<int, string>();
            id2pinyin = new Dictionary<int, string>();

            string filePath = Path.Combine(Application.streamingAssetsPath, "Datas", "chara_name.json");

            string jsonString = null;

            if (filePath.Contains("jar:file:")) // Android
            {
                using (UnityWebRequest www = UnityWebRequest.Get(filePath))
                {
                    yield return www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError("Failed to load file: " + www.error);
                        yield break;
                    }

                    jsonString = www.downloadHandler.text;
                }
            }
            else //PC
            {
                jsonString = File.ReadAllText(filePath);
            }

            var data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonString);
            foreach (var entry in data)
            {
                int parsedKey = int.Parse(entry.Key + "01");
                id2alias[parsedKey] = string.Join("|", entry.Value);
                id2pinyin[parsedKey] = string.Join("|", entry.Value.Select(x => PinyinHelper.GetPinyin(x, "").ToUpper()));
            }
        }
        private void SelectFirstUnit()
        {
            if (firstShowUnitId != -1)
            {
                OnToggleSwitched(!togglePerferbs[firstShowUnitId].isOn, firstShowUnitId);
            }
        }

        public void SearchChara()
        {
            if (Search.text.EndsWith(" "))
            {
                SelectFirstUnit();
                Search.text = Search.text.TrimEnd(' ');
            }
            else
            {
                RefreshBasePage();
            }
        }

        public void SwitchFilterToggle()
        {
            filterType.text = filterToggle.isOn ? "属性" : "站位";
            talent.SetActive(filterToggle.isOn);
            position.SetActive(!filterToggle.isOn);
        }
    }
}