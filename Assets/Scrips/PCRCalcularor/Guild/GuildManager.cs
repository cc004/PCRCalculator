using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using Elements;
using Elements.Battle;
using Newtonsoft.Json;
using PCRCaculator.Update;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using SFB;
namespace PCRCaculator.Guild
{
    public class GuildManager : MonoBehaviour
    {
        public static GuildManager Instance;
        //public TextAsset enemyDataDicTxt;
        public List<CharacterPageButton> charactersUI;
        public Text characterGroupNameText;
        public Toggle AutoModeToggle;
        public Toggle SetModeToggle;
        public Toggle SemanModeToggle;
        public List<UBTime> UnitUBTimes;
        public Text UBTimeEditButtonText;
        public List<Toggle> characterGroupToggles;
        public List<Text> characterGruopTexts;
        public Text characterGroupPageText;
        public List<Text> timeLineTexts;

    public GameObject characterDetailPage;
        public GameObject characterDetailPageChild;
        public CharacterPageButton characterDetailButton;
        public Action execTimeButtonAction;
        public List<Text> characterDetailTexts;
        public Text characterDetailTPR;

        public Image bossPicture;
        public List<Text> bossDetailTexts;

        public List<Dropdown> dropdowns_ChooseBoss;
        public Image bossImage_ChooseBoss;
        public List<Toggle> toggles_ChooseBoss;
        public InputField currentHPinput;
        public List<Toggle> toggles_ChooseType;
        public Text show_text;
        public InputField specialInput;
        public List<string> show_str;


        public GuildBossDataEditdr BossDataEditdr;

        public List<Slider> SettingSliders;
        public List<Text> SettingTexts;
        public List<Toggle> SettingToggles;
        public List<InputField> SettingInputs;

        public List<Text> calSettingTexts;
        public Slider calSlider;
        public GameObject bossDetailPrefab;


        //public List<Sprite> bossSprites;
        //public List<string> bossNames;
        //public List<int> enemy_ids;
        public Dictionary<int, GuildEnemyData> guildEnemyDatas => MainManager.Instance.GuildEnemyDatas;
        //public List<int> specialEnemyDatas;

        //public GuildExecTimeSetting GuildExecTimeSetting;
        public GuildRandomManager RandomManager;
        public Update.UpdateManager updateManager;
        public GameObject guildDataInportPannel;

        public GameObject autoCalculatePagePrefab;

        //private List<AddedPlayerData> addedPlayerDatas;
        private static Dictionary<int, EnemyData> enemyDataDic;
        //private Dictionary<int, MasterEnemyMParts.EnemyMParts> enemyMPartsDic = new Dictionary<int, MasterEnemyMParts.EnemyMParts>();
        public GuildSettingData SettingData => StaticsettingData;

        private static GuildSettingData staticsettingData;
        public static GuildSettingData StaticsettingData
        {
            get
            {
                if (staticsettingData == null)
                {
                    LoadAddedPlayerData();
                }
                return staticsettingData;
            }
        }
        private int[] selectedBossEnemyids => SettingData.GetCurrentPlayerGroup().selectedEnemyIDs;
        private bool isEditingUBTime;
        private static string[] guildMonthNames = new string[12] { "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "摩羯座", "水瓶座", "双鱼座" };
        private int currentPage;
        private bool isInit;
        private int[] specialEnemyid;
        private int specialInputValue;
        public Image backgroundImage;
        public GameObject setImageButton;
        public GameObject clearImageButton;
        public Button clearUbButton;
        public GameObject infoPanel;
        public Text version;
        public InputField RandomSeed;
        public Toggle UseFixedRandomSeed;
        public static Dictionary<int, EnemyData> EnemyDataDic
        {
            get
            {
                /*if (enemyDataDic == null)
                {
                    string enemyDataDicTxt = MainManager.Instance.LoadJsonDatas("Datas/EnemyDataDic");
                    enemyDataDic = JsonConvert.DeserializeObject<Dictionary<int, EnemyData>>(enemyDataDicTxt);
                }*/
                return enemyDataDic;
            }
            set
            {
                enemyDataDic = value;
            }
        }
        public Dictionary<int, MasterEnemyMParts.EnemyMParts> EnemyMPartsDic
        {
            get => MainManager.Instance.EnemyMPartsDic;
        }
        public bool isGuildBoss { get => toggles_ChooseType[0].isOn; }
        private void Awake()
        {
            Instance = this;

            //System.IO.File.WriteAllText("fieldnames.txt",
            //    string.Join("\n", typeof(UnitCtrl).GetProperties().Select(p => p.Name)));
            LoadAddedPlayerData();
        }
        private void Start()
        {
            StartCoroutine(StartAfterWait());
            version.text = "—   v" + Application.version+"   —";
            foreach (Transform child in infoPanel.transform)
            {
              Button button = child.GetComponent<Button>();
              Text text = child.GetComponentInChildren<Text>();
              if (button != null && text != null)
              {
                  button.onClick.AddListener(() => OnInfoButtonClick(text));
              }
            }
            SettingInputs[11].onValueChanged.AddListener(OnInputField1ValueChanged);
            SettingInputs[12].onValueChanged.AddListener(OnInputField2ValueChanged);
        }
        private void OnInfoButtonClick(Text text)
        { 
            string textContent = text.text;
            if (!string.IsNullOrEmpty(SettingInputs[11].text))
            {
                textContent = "/" + textContent;
            }
            textContent = textContent.Replace("\n", "").Replace("\r", "");
            GUIUtility.systemCopyBuffer = textContent;
            MainManager.Instance.WindowMessage("文本已复制并添加到末尾");
            SettingInputs[11].text += textContent.Replace("\n", "").Replace("\r", "");
        }
        private void OnInputField1ValueChanged(string value)
        {
            SettingInputs[12].onValueChanged.RemoveListener(OnInputField2ValueChanged);
            SettingInputs[12].text = value;
            SettingInputs[12].onValueChanged.AddListener(OnInputField2ValueChanged);
        }

        private void OnInputField2ValueChanged(string value)
        {
            SettingInputs[11].onValueChanged.RemoveListener(OnInputField1ValueChanged);
            SettingInputs[11].text = value;
            SettingInputs[11].onValueChanged.AddListener(OnInputField1ValueChanged);
        }
        private IEnumerator StartAfterWait()
        {
            while (!MainManager.Instance.LoadFinished)
                yield return null;
            yield return null;
            //Load();
            //LoadAddedPlayerData();
            RefreshOnStart();
            if (MainManager.Instance.AutoCalculatorData.isCalculating)
            {
                OpenAutoCalculatePage();
                if (MainManager.Instance.AutoCalculatorData.isFinish)
                {
                    MainManager.Instance.AutoCalculatorData.isCalculating = false;
                }
                else
                {
                    StartAutoCalculate();
                }
            }
        }
        private void Load()
        {
            //TextAsset enemyDataDicTxt = Resources.Load<TextAsset>("Datas/EnemyDataDic");
            //string enemyDataDicTxt = MainManager.Instance.LoadJsonDatas("Datas/EnemyDataDic");
            //enemyDataDic = JsonConvert.DeserializeObject<Dictionary<int, EnemyData>>(enemyDataDicTxt);
            /*try
            {
                guildEnemyDatas = 
            }
            catch(Exception ex)
            {
                Debug.LogError($"读取会战怪物数据出错！{ex}");
                string guildenemyDatasStr = MainManager.Instance.LoadJsonDatas("Datas/GuildEnemyDatas");
                if (!string.IsNullOrEmpty(guildenemyDatasStr))
                {
                    guildEnemyDatas = JsonConvert.DeserializeObject<Dictionary<int, GuildEnemyData>>(guildenemyDatasStr);
                }
            }*/
            //string enemyMParts = MainManager.Instance.LoadJsonDatas("Datas/EnemyMPartsDic");
            //enemyMPartsDic = JsonConvert.DeserializeObject<Dictionary<int, MasterEnemyMParts.EnemyMParts>>(enemyMParts);
            //CreateSpecialEnemyData();
        }
        public void SelectCharacterButton()
        {
            ChoosePannelManager.Instance.CallChooseBack(3);
        }

        public void ActivateCharacterDetailPage(Vector3 pos, float scale)
        {
            characterDetailPage.SetActive(true);
            // characterDetailPageChild.transform.localPosition = pos;
            // characterDetailPageChild.transform.localScale = Vector3.one * scale;
        }
        public void HideCharacterDetailPage()
        {
            characterDetailPage.SetActive(false);
        }
        public void RefreshCharacterDetailPage(int idx,UnitData unitData=null)
        {
            if (unitData == null && idx >= SettingData.GetCurrentPlayerData().playrCharacters.Count) 
                return;
            if (unitData == null)
                unitData = SettingData.GetCurrentPlayerData().playrCharacters[idx];
            characterDetailButton.SetButton(unitData);
            BaseData baseData = MainManager.Instance.UnitRarityDic[unitData.unitId].GetBaseData(unitData);
            var baseDataEX = MainManager.Instance.UnitRarityDic[unitData.unitId].GetEXSkillValue(unitData);//,MyGameCtrl.Instance.tempData.isGuildBattle);
            characterDetailTexts[0].text = "" + baseData.RealHp + (baseDataEX.RealHp == 0 ? string.Empty : $"<color=#FF80C0>+{baseDataEX.RealHp}</color>");
            long accuracy = Mathf.RoundToInt(EnemyDataDic.ContainsKey(selectedBossEnemyids[0]) ? EnemyDataDic[selectedBossEnemyids[0]].baseData.Accuracy : 0);//boss命中
            long dodge = baseData.dataint[9] / 100;//角色闪避
            double dodgerate = dodge <= accuracy ? 0 : 1 / (1 + 100.0 / (dodge - accuracy));//闪避率
            for (int i = 1; i < baseData.dataint.Length; i++)
            {
                if (i == 9)
                {
                    // 处理闪避率
                    characterDetailTexts[i].text = "" + baseData.dataint[i] / 100 + (baseDataEX.dataint[i] == 0 ? string.Empty : $"<color=#FF80C0>+{(baseData.dataint[i] + baseDataEX.dataint[i]) / 100.0 - baseData.dataint[i] / 100.0}</color>") + $"<color=#FF80C0> ({(dodgerate * 100).ToString("F2") + "%"})</color>";
                }
                else
                {
                    //处理一下四舍五入的问题
                    characterDetailTexts[i].text = "" + baseData.dataint[i] / 100 + (baseDataEX.dataint[i] == 0 ? string.Empty : $"<color=#FF80C0>+{(baseData.dataint[i] + baseDataEX.dataint[i]) / 100 - baseData.dataint[i] / 100}</color>");
                }
            }
            characterDetailTPR.text = $"{BattleManager.CalcPlayerDamageTpReduceRate(unitData.rank)}";

            execTimeButtonAction = () => { GuildExecTimeSettingButton(unitData); };
        }

        public void ShowCharacterDetailPage(int idx)
        {
            if(idx>= SettingData.GetCurrentPlayerData().playrCharacters.Count)
            {
                return;
            }
            EditCharacterdetail(idx);
        }
        public void EditCharacterdetail(int idx)
        {
            ChoosePannelManager.Instance.CallChooseBack(4, SettingData.GetCurrentPlayerData(), idx);
        }
        public void FinishEditCharacterdetail()
        {
            RefreshCharacterGroupToggle();
            ChooseCharacterGroup();
            SaveDataToJson();
        }
        public void ChooseCharacterGroup()
        {
            if (isInit)
                return;
            int idx = 0;
            foreach (Toggle toggle in characterGroupToggles)
            {
                if (toggle.isOn)
                {
                    SettingData.currentPlayerGroupNum = idx + currentPage*5-5;
                    AutoModeToggle.isOn = SettingData.GetCurrentPlayerGroup().useAutoMode;
                    SetModeToggle.isOn = SettingData.GetCurrentPlayerGroup().useSetMode;
                }
                idx++;
            }
            Refresh();
        }
        public void FinishEditingPlayers(AddedPlayerData playerData)
        {
            //addedPlayerDatas = new List<AddedPlayerData>() { playerData };
            SettingData.SetCurrentPlayerData(playerData);
            Refresh();
            SaveDataToJson();
        }
        private void CreateMonthDropDown()
        {
            Dropdown dp = dropdowns_ChooseBoss[2];
            List<string> list = new List<string>();
            foreach(var key in guildEnemyDatas.Keys)
            {
                if (key >= 1024)
                {
                    string str = $"{guildMonthNames[(key - 1000 + 11) % 12]}({key})";
                    list.Add(str);
                }
            }
            dp.ClearOptions();
            dp.AddOptions(list);
        }
        private int GetClanBattleID()
        {
            return dropdowns_ChooseBoss[2].value + 1024;
        }
        private void SetChooseBossDropDown(int clanBattleID)
        {
            dropdowns_ChooseBoss[2].value  = clanBattleID - 1024;
        }
        public void OnChooseBossDropDownChanged()
        {
            if (!MainManager.Instance.LoadFinished)
                return;
            //bossImage_ChooseBoss.sprite = bossSprites[dropdowns_ChooseBoss[0].value];
            int enemyid_0 = 0;
            string Path;
            try
            {
                if (isGuildBoss)
                {
                    enemyid_0 = guildEnemyDatas[GetClanBattleID()].enemyIds[0][dropdowns_ChooseBoss[0].value];
                    //Path = "GuildEnemy/icon_unit_" + enemyDataDic[enemyid_0].unit_id;
                    //bossImage_ChooseBoss.sprite = MainManager.LoadSourceSprite(Path);
                    bossImage_ChooseBoss.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, EnemyDataDic[enemyid_0].unit_id);
                }
                else
                {
                    specialInput.text = "401042401";
                    /*
                    show_text.text = show_str[dropdowns_ChooseBoss[3].value];
                    switch (dropdowns_ChooseBoss[3].value)
                    {
                        case 0://木桩1
                            enemyid_0 = specialEnemyDatas[dropdowns_ChooseBoss[3].value];
                            specialInput.text = "100";
                            //Path = "GuildEnemy/icon_unit_" + enemyDataDic[enemyid_0].unit_id;
                            //bossImage_ChooseBoss.sprite = MainManager.LoadSourceSprite(Path);
                            bossImage_ChooseBoss.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, enemyDataDic[enemyid_0].unit_id);

                            break;
                        case 1://木桩2
                            enemyid_0 = specialEnemyDatas[dropdowns_ChooseBoss[3].value];
                            specialInput.text = "100";
                            //Path = "GuildEnemy/icon_unit_" + enemyDataDic[enemyid_0].unit_id;
                            //bossImage_ChooseBoss.sprite = MainManager.LoadSourceSprite(Path);
                            bossImage_ChooseBoss.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, enemyDataDic[enemyid_0].unit_id);

                            break;
                        case 2:
                            break;
                    }*/
                }

                specialEnemyid = new[] {enemyid_0};
                specialInputValue = 100;
            }
            catch(Exception _)
            {
                MainManager.Instance.WindowConfigMessage($"会战{GetClanBattleID()}的配置缺失！", null);
            }
        }
        public void OnInputFinished()
        {
            if(!isGuildBoss)
            {
                var bossid = specialInput.text.Split(',').Select(x => int.Parse(x, NumberStyles.Any)).ToArray();
                EnemyData data = null;
                if (bossid.Reverse().All(x => EnemyDataDic.TryGetValue(x, out data)))
                {
                    //string Path = "GuildEnemy/icon_unit_" + data.unit_id;
                    //bossImage_ChooseBoss.sprite = MainManager.LoadSourceSprite(Path);
                    bossImage_ChooseBoss.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, data.unit_id);
                    specialEnemyid = bossid;
                }
                else
                {
                    MainManager.Instance.WindowConfigMessage("bossID无效！", null);
                    specialEnemyid = new[] {0};
                }
                specialInputValue = 0;
            }
        }
        public void FinishChooseBossButton()
        {
            int[] enemyId = new int[] {0};
            if (isGuildBoss)
            {
                //int month = dropdowns_ChooseBoss[2].value;
                int clanBattleID = GetClanBattleID();
                int month = clanBattleID - 1000;
                SettingData.GetCurrentPlayerGroup().currentGuildMonth = GetClanBattleID();
                bossDetailTexts[3].text = $"{guildMonthNames[(month + 11) % 12]}\n<size=10>({GetClanBattleID()})</size>";
                int num = dropdowns_ChooseBoss[0].value; 
                SettingData.GetCurrentPlayerGroup().currentGuildEnemyNum = num;
                int turn = dropdowns_ChooseBoss[1].value + 1; 
                SettingData.GetCurrentPlayerGroup().currentTurn = turn;
                enemyId = new [] {GetGuildBossID(clanBattleID, num, turn)};
            }
            else
            {
                enemyId = specialEnemyid;
            }
            //selectedBossEnemyid = SettingData.currentTurn == 1 ? guildEnemyData.enemyIds_1[SettingData.currentGuildEnemyNum] : guildEnemyData.enemyIds_2[SettingData.currentGuildEnemyNum];

            var group = SettingData.GetCurrentPlayerGroup();
            group.selectedEnemyIDs = isGuildBoss ? enemyId : specialEnemyid;
            group.isSpecialBoss = false;//!isGuildBoss;
            //group.specialBossID = specialEnemyid;
            //group.specialInputValue = specialInputValue;
            group.useLogBarrierNew = GuildPlayerGroupData.LogBarrierType.FullBarrier; // (GuildPlayerGroupData.LogBarrierType)dropdowns_ChooseBoss[4].value;
            SaveDataToJson();
            Refresh();
        }
        public int GetGuildBossID(int clanBattleID, int num, int turn)
        {
            int enemyId = 0;
            enemyId = guildEnemyDatas[clanBattleID].enemyIds[turn][num];
            /*
            switch (turn)
            {
                case 1:
                    enemyId = guildEnemyData.enemyIds_1[num];
                    break;
                case 2:
                    enemyId = guildEnemyData.enemyIds_2[num];
                    break;
                case 3:
                    if (guildEnemyData.enemyIds_3 != null && guildEnemyData.enemyIds_3.Count > num)
                        enemyId = guildEnemyData.enemyIds_3[num];
                    else
                    {
                        enemyId = guildEnemyData.enemyIds_2[num];
                        MainManager.Instance.WindowConfigMessage("该次会战没有三周目或者三周目数据丢失！", null, null);
                    }
                    break;
                case 4:
                    if (guildEnemyData.enemyIds_4 != null && guildEnemyData.enemyIds_4.Count > num)
                        enemyId = guildEnemyData.enemyIds_4[num];
                    else
                    {
                        enemyId = guildEnemyData.enemyIds_4[num];
                        MainManager.Instance.WindowConfigMessage("该次会战没有四周目或者四周目数据丢失！", null, null);
                    }
                    break;
                case 5:
                    if (guildEnemyData.enemyIds_5 != null && guildEnemyData.enemyIds_5.Count > num)
                        enemyId = guildEnemyData.enemyIds_5[num];
                    else
                    {
                        enemyId = guildEnemyData.enemyIds_5[num];
                        MainManager.Instance.WindowConfigMessage("该次会战没有五周目或者五周目数据丢失！", null, null);
                    }
                    break;

                default:
                    enemyId = guildEnemyDatas[0].enemyIds_1[0];
                    break;
            }*/
            return enemyId;
        }
        public void OnCalSliderDraged()
        {
            int scaleRate = Mathf.RoundToInt(calSlider.value);
            SettingData.calSpeed = (int) Mathf.Pow(2, scaleRate - 3);
            calSettingTexts[0].text = "x" + SettingData.calSpeed;
        }
        public void StartCalButton() => StartCalButton(false);
        public void ReStartCalButton() => ReStartCalButton(false);

        private void ReStartCalButton(bool skipping)
        {
            SaveDataToJson();
            GuildBattleData battleData = new GuildBattleData();
            battleData.skipping = skipping;
            battleData.players = SettingData.GetCurrentPlayerData();
            battleData.enemyData = new List<EnemyData>();
            foreach (var selectedBossEnemyid in selectedBossEnemyids)
            {
                EnemyData enemyData = GetEnemyDataByID(selectedBossEnemyid);
                if(EnemyMPartsDic.TryGetValue(enemyData.enemy_id,out var value))
                {
                    battleData.mParts = value;
                    battleData.MPartsDataDic = new Dictionary<int, EnemyData>();
                    Action<int> action = a =>
                    {
                        if (a != 0)
                        {
                            battleData.MPartsDataDic.Add(a, GetEnemyDataByID(a));                        
                        }
                    };
                    action(value.child_enemy_parameter_1);
                    action(value.child_enemy_parameter_2);
                    action(value.child_enemy_parameter_3);
                    action(value.child_enemy_parameter_4);
                    action(value.child_enemy_parameter_5);

                }
                battleData.enemyData.Add(enemyData);
            }
            battleData.UBExecTimeList = SettingData.GetCurrentPlayerGroup().UBExecTimeData;
            battleData.SemanUBExecTimeList = SettingData.GetCurrentPlayerGroup().SemanUBExecTimeData;
            battleData.SettingData = SettingData;
            MainManager.Instance.guildBattleData = battleData;
            MyGameCtrl.Instance.ReStart();
        }

        private void StartCalButton(bool skipping)
        {
            /*if (!CheckDataIsReady(SettingData..currentGuildMonth))
            {
                return;
            }*/
            SaveDataToJson();
            GuildBattleData battleData = new GuildBattleData();
            battleData.skipping = skipping;
            battleData.players = SettingData.GetCurrentPlayerData();
            battleData.enemyData = new List<EnemyData>();
            foreach (var selectedBossEnemyid in selectedBossEnemyids)
            {
                EnemyData enemyData = GetEnemyDataByID(selectedBossEnemyid);
                if(EnemyMPartsDic.TryGetValue(enemyData.enemy_id,out var value))
                {
                    battleData.mParts = value;
                    battleData.MPartsDataDic = new Dictionary<int, EnemyData>();
                    Action<int> action = a =>
                    {
                        if (a != 0)
                        {
                            battleData.MPartsDataDic.Add(a, GetEnemyDataByID(a));                        
                        }
                    };
                    action(value.child_enemy_parameter_1);
                    action(value.child_enemy_parameter_2);
                    action(value.child_enemy_parameter_3);
                    action(value.child_enemy_parameter_4);
                    action(value.child_enemy_parameter_5);

                }
                battleData.enemyData.Add(enemyData);
            }
            battleData.UBExecTimeList = SettingData.GetCurrentPlayerGroup().UBExecTimeData;
            battleData.SemanUBExecTimeList = SettingData.GetCurrentPlayerGroup().SemanUBExecTimeData;
            battleData.isAutoMode = AutoModeToggle.isOn;
            battleData.isSetMode = SetModeToggle.isOn;
            battleData.isSemanMode = SemanModeToggle.isOn;
            battleData.stopFrame = int.TryParse(SettingInputs[8].text, out var val) ? val : -1;
            battleData.SettingData = SettingData;
            MainManager.Instance.ChangeSceneToBalttle(battleData);
        }
        private EnemyData GetEnemyDataByID(int enemy_id)
        {
            return SettingData.changedEnemyDataDic.TryGetValue(enemy_id, out EnemyData data) ? data : EnemyDataDic[enemy_id];
        }
        /*private bool CheckDataIsReady(int month)
        {
            if (SettingData.unlockGuilds[month]) { return true; }
            MainManager.Instance.WindowConfigMessage(guildMonthNames[month] + "的怪物数据丢失，无法继续！",null, null);
            return false;
        }*/
        private void RefreshOnStart()
        {
            isInit = true;
            currentPage = Mathf.FloorToInt(SettingData.currentPlayerGroupNum / 5.0f) + 1;
            if (currentPage < 1 || currentPage > 20)
            {
                //currentPage = 1;
            }
            RefreshCharacterGroupToggle();
            CreateMonthDropDown();

            var data = SettingData.GetCurrentPlayerGroup();
            SetChooseBossDropDown(data.currentGuildMonth);
            //dropdowns_ChooseBoss[2].value = data.currentGuildMonth >= 1000 ? data.currentGuildMonth - 1000 : (data.currentGuildMonth + 1) % 12;
            dropdowns_ChooseBoss[0].value = data.currentGuildEnemyNum;
            dropdowns_ChooseBoss[1].value = data.currentTurn - 1;
            dropdowns_ChooseBoss[4].value = (int)data.useLogBarrierNew;
            //toggles_ChooseBoss.isOn = SettingData.isViolent;
            if (data.isViolent)
                toggles_ChooseBoss[0].isOn = true;
            else 
                toggles_ChooseBoss[1].isOn = true;
            //FinishChooseBossButton();
            calSlider.value = (int)(Math.Log(SettingData.calSpeed, 2) + 3);
            if (data.timeLineData == null)
            {
                data.timeLineData = new GuildRandomData
                {
                    /*randomData.ForceIgnoreDodge_enemy = SettingData.ForceIgnoreDodge_enemy;
                    randomData.ForceIgnoreDodge_player = SettingData.ForceIgnoreDodge_player;
                    randomData.ForceNoCritical_enemy = SettingData.ForceNoCritical_enemy;
                    randomData.ForceNoCritical_player = SettingData.ForceNoCritical_player;
                    randomData.UseFixedRandomSeed = SettingData.UseFixedRandomSeed;*/
                    RandomSeed = 666,
                    DataName = "默认世界线"
                };
                //SettingData.guildRandomDatas.Add(randomData);
            }
            Refresh();
            RefreshCalcSettings_start();
            EditingUBTime(false);
            AutoModeToggle.isOn = data.useAutoMode;
            SetModeToggle.isOn = data.useSetMode;
            SemanModeToggle.isOn = data.useSemanMode;
            isInit = false;
        }
        private void RefreshCharacterGroupToggle()
        {
            for(int i = 0; i < 5; i++)
            {
                characterGruopTexts[i].text = "队伍" + (5 * currentPage - 4 + i);
            }
            int toggleIdx = SettingData.currentPlayerGroupNum - 5 * (currentPage - 1);
            if (toggleIdx < 0 || toggleIdx > 4)
            {
                toggleIdx = 0;
            }
            characterGroupToggles[toggleIdx].isOn = true;
            characterGroupPageText.text = "" + currentPage;
        }
        private void Refresh()
        {
            var data = SettingData.GetCurrentPlayerGroup();
            AddedPlayerData playerData = data.playerData;
            /*if (SettingData.addedPlayerDatas != null && SettingData.addedPlayerDatas.Count > SettingData.currentPlayerGroupNum)
            {
                playerData = SettingData.addedPlayerDatas[SettingData.currentPlayerGroupNum];
            }*/
            characterGroupNameText.text = playerData.playerName;
            for (int i = 0; i < 5; i++)
            {
                UnitUBTimes[i].SetSemanMode(data.useSemanMode);
                if (i < playerData.playrCharacters.Count)
                {
                    charactersUI[i].SetButton(playerData.playrCharacters[i]);
                    if (data.UBExecTimeData?.Count > i)
                        UnitUBTimes[i].SetUBTimes(data.UBExecTimeData[i]);
                    if (data.SemanUBExecTimeData?.Count > i)
                        UnitUBTimes[i].SetSemanUBTimes(data.SemanUBExecTimeData[i]);
                }
                else
                {
                    charactersUI[i].SetButton(0);
                    UnitUBTimes[i].SetUBTimes(new List<float>());
                    UnitUBTimes[i].SetSemanUBTimes(new List<int>());
                }
            }
            if (data.UBExecTimeData?.Count >= 6)
                UnitUBTimes[5].SetUBTimes(data.UBExecTimeData[5]);
            if (data.SemanUBExecTimeData?.Count >= 6) 
                UnitUBTimes[5].SetSemanUBTimes(data.SemanUBExecTimeData[5]);
            AutoModeToggle.isOn = data.useAutoMode;
            SetModeToggle.isOn = data.useSetMode;
            SemanModeToggle.isOn = data.useSemanMode;
            timeLineTexts[0].text = data.timeLineData.DataName;
            timeLineTexts[1].text = data.timeLineData.GetDescribe();

            var group = SettingData.GetCurrentPlayerGroup();
            if (data.currentGuildMonth < 1000) data.currentGuildMonth += 1024;
            if (data.currentGuildMonth < 1024) data.currentGuildMonth += 24;

            if (selectedBossEnemyids == null || selectedBossEnemyids.Length == 0 || selectedBossEnemyids[0] == 0)
                data.selectedEnemyIDs = new []
                    {GetGuildBossID(data.currentGuildMonth, data.currentGuildEnemyNum, data.currentTurn)};
            //string Path = "GuildEnemy/icon_unit_" + enemyDataDic[selectedBossEnemyid].unit_id;
            //bossPicture.sprite = MainManager.LoadSourceSprite(Path);
            bossPicture.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, EnemyDataDic[selectedBossEnemyids[0]].unit_id);
            bossDetailTexts[0].text = group.isSpecialBoss ? "自定义": EnemyDataDic[selectedBossEnemyids[0]].name;
            string str = "";
            if (group.isSpecialBoss)
            {
                str = "--";
            }
            else
            {
                switch (group.currentTurn)
                {
                    case 1:
                        str = "一阶段";
                        break;
                    case 2:
                        str = "二阶段";
                        break;
                    case 3:
                        str = "三阶段";
                        break;
                    case 4:
                        str = "四阶段";
                        break;
                    case 5:
                        str = "五阶段";
                        break;

                }
            }
            bossDetailTexts[1].text = str;
            if (group.currentGuildEnemyNum == 4 && toggles_ChooseBoss[0].isOn)
            {
                group.isViolent = true;
            }
            else
            {
                group.isViolent = false;
            }
            // if (toggles_ChooseBoss[2].isOn)
            // {
            //     group.usePlayerSettingHP = true;
            //     group.playerSetingHP = int.Parse(currentHPinput.text);
            // }
            // else
            // {
            //     group.usePlayerSettingHP = false;
            //     group.playerSetingHP = 0;
            // }
            if (data.playerSetingHP == 0) 
            {
                currentHPinput.text = " " + GetEnemyDataByID(selectedBossEnemyids[0]).baseData.RealHp;
            }
            else
            {
                currentHPinput.text = " " + data.playerSetingHP;
            }
            bossDetailTexts[2].text = group.isViolent ? "狂暴" : (group.usePlayerSettingHP ? "自定义HP" : "--");
            bossDetailTexts[3].text = $"{guildMonthNames[(data.currentGuildMonth - 1000 + 11) % 12]}" +
                $"\n<size=10>({data.currentGuildMonth})</size>";
            //group.useLogBarrier = toggles_ChooseBoss[3].isOn;
            RandomSeed.text = "" + SettingData.GetCurrentRandomData().RandomSeed;
            UseFixedRandomSeed.isOn = SettingData.GetCurrentRandomData().UseFixedRandomSeed;
            toggles_ChooseBoss[2].isOn = SettingData.GetCurrentPlayerGroup().usePlayerSettingHP;
        }
        private void RefreshCalcSettings_start()
        {
            SettingSliders[0].value = SettingData.FPSforLogic / 10;
            SettingTexts[0].text = SettingData.FPSforLogic + "";
            SettingSliders[1].value = SettingData.FPSforAnimation / 10;
            SettingTexts[1].text = SettingData.FPSforAnimation + "";
            SettingSliders[2].value = SettingData.UBTryingCount / 10;
            SettingTexts[2].text = SettingData.UBTryingCount + "";
            SettingToggles[0].isOn = SettingData.calcSpineAnimation;
            SettingToggles[0].interactable = true; //SettingData.calSpeed == 1;
            /*SettingInputs[0].text = SettingData.RandomSeed + "";
            SettingToggles[2].isOn = SettingData.UseFixedRandomSeed;
            SettingToggles[3].isOn = SettingData.ForceNoCritical_player;
            SettingToggles[4].isOn = SettingData.ForceNoCritical_enemy;
            SettingToggles[5].isOn = SettingData.ForceIgnoreDodge_player;
            SettingToggles[6].isOn = SettingData.ForceIgnoreDodge_enemy;*/
            //SettingInputs[1].text = SettingData.BodyColliderWidth + "";
            //SettingInputs[2].text = SettingData.BossAbnormalMultValue + "";
            //SettingInputs[3].text = SettingData.BossAbnormalAddValue + "";
            RandomManager.Refresh();
            // SettingToggles[7].isOn = SettingData.usePhysics;
            //SettingToggles[8].isOn = SettingData.useSkillEffects;
            //SettingInputs[4].text = SettingData.skillEffeckFix + "";
            SettingInputs[1].text = SettingData.start_hp;
            SettingInputs[2].text = SettingData.start_tp;
            SettingInputs[5].text = SettingData.limitTime.ToString();
            SettingInputs[6].text = SettingData.author;
            SettingInputs[7].text = SettingData.format;
            SettingInputs[9].text = SettingData.n1.ToString();
            SettingInputs[10].text = SettingData.n2.ToString();
            SettingInputs[11].text = SettingData.dispFields;
            SettingInputs[12].text = SettingData.dispFields;
        }
        public void RefreshCalcUI()
        {
            SettingTexts[0].text = (int)SettingSliders[0].value * 10 + "";
            SettingTexts[1].text = (int)SettingSliders[1].value * 10 + "";
            SettingTexts[2].text = (int)SettingSliders[2].value * 10 + "";
            SettingToggles[0].interactable = SettingData.calSpeed == 1;
            //SettingToggles[7].isOn = SettingData.usePhysics;
        }
        public void SaveCalcSettings()
        {
            SettingData.FPSforLogic = (int)SettingSliders[0].value * 10;
            SettingData.FPSforAnimation = (int)SettingSliders[1].value * 10;
            SettingData.calcSpineAnimation = SettingToggles[0].isOn;
            SettingData.UBTryingCount = (int)SettingSliders[2].value * 10;
            //SettingData.UseFixedRandomSeed = SettingToggles[2].isOn;
            try
            {
                //SettingData.RandomSeed = int.Parse(SettingInputs[0].text);
                //SettingData.BodyColliderWidth = float.Parse(SettingInputs[1].text);
            }
            catch
            {
                MainManager.Instance.WindowConfigMessage("输入错误！", null);
                //SettingData.RandomSeed = 666;

            }
            /*SettingData.ForceNoCritical_player = SettingToggles[3].isOn;
            SettingData.ForceNoCritical_enemy = SettingToggles[4].isOn;
            SettingData.ForceIgnoreDodge_player = SettingToggles[5].isOn;
            SettingData.ForceIgnoreDodge_enemy = SettingToggles[6].isOn;*/
            //SettingData.BossAbnormalMultValue = float.Parse(SettingInputs[2].text);
            //SettingData.BossAbnormalAddValue = float.Parse(SettingInputs[3].text);
            //MainManager.Instance.ChangeBodyWidth(SettingData.BodyColliderWidth);
            //SettingData.usePhysics = SettingToggles[7].isOn;
            //SettingData.useSkillEffects = SettingToggles[8].isOn;
            //SettingData.skillEffeckFix = float.Parse(SettingInputs[4].text);
            SettingData.start_hp = SettingInputs[1].text;
            SettingData.start_tp = SettingInputs[2].text;
            SettingData.limitTime = int.Parse(SettingInputs[5].text);
            SettingData.author = SettingInputs[6].text;
            SettingData.format = SettingInputs[7].text;
            SettingData.n1 = int.Parse(SettingInputs[9].text);
            SettingData.n2 = int.Parse(SettingInputs[10].text);
            SettingData.dispFields = SettingInputs[11].text;
            UnitCtrl.infoGetter = null;
            SaveDataToJson();
        }
        private static void LoadAddedPlayerData()
        {
            try
            {
                staticsettingData = SaveManager.Load<GuildSettingData>();
            }
            catch
            {
                staticsettingData = new GuildSettingData(true);
            }
        }
        public void SaveDataToJson()
        {
            SaveSettingData(SettingData);
        }
        public void OnToggleSwitched(){
            SettingData.GetCurrentPlayerGroup().useAutoMode = AutoModeToggle.isOn;
            SettingData.GetCurrentPlayerGroup().useSetMode = SetModeToggle.isOn;
            SaveDataToJson();
    }

        public void ToggleSemanMode()
        {
            SettingData.GetCurrentPlayerGroup().useSemanMode = SemanModeToggle.isOn;
            SaveDataToJson();
            foreach(var ubTime in UnitUBTimes)
            {
                ubTime.SetSemanMode(SemanModeToggle.isOn);
            }
        }

        public void EditingUBTime(bool isButton)
        {
            if (isButton)
            {
                isEditingUBTime = !isEditingUBTime;
            }
            else
            {
                isEditingUBTime = false;
                // AutoModeToggle.interactable = false;
            }
            UBTimeEditButtonText.text = isEditingUBTime ? "保存" : "修改";
            int idx = 0;
            if (!isEditingUBTime && isButton)
            {
                if (SemanModeToggle.isOn)
                    SettingData.GetCurrentPlayerGroup().SemanUBExecTimeData.Clear();
                else
                    SettingData.GetCurrentPlayerGroup().UBExecTimeData.Clear();
            }
            foreach (UBTime uBTime in UnitUBTimes)
            {
                if (isEditingUBTime)
                {
                    uBTime.StartEdit();
                    // AutoModeToggle.interactable = true;
                }
                else
                {
                    uBTime.FinishEdit();
                    if (isButton)
                    {
                        if (SemanModeToggle.isOn)
                        {
                            SettingData.GetCurrentPlayerGroup().SemanUBExecTimeData.Add(uBTime.GetSemanUBTimes());
                        }
                        else
                        {
                            SettingData.GetCurrentPlayerGroup().UBExecTimeData.Add(uBTime.GetUBTimes());
                        }
                    }
                }
                idx++;
            }
            if(isButton && !isEditingUBTime)
            {
                // AutoModeToggle.interactable = false;
                // SettingData.GetCurrentPlayerGroup().useAutoMode = AutoModeToggle.isOn;
                // SettingData.GetCurrentPlayerGroup().useSetMode = SetModeToggle.isOn;
                SaveDataToJson();
            }
        }
        public void UBTimeHelpButton()
        {
            string msg = "可以输入帧(>100)或秒(<90)\n帧从0开始到5400结束\n秒从90秒开始倒计时到0结束\n例如输入61.001保存则自动转成1860.001帧\nAuto和Set按钮为改变开场状态(非以前强制)";
            MainManager.Instance.WindowConfigMessage(msg, null);
        }
        public void GuildBossDataEditButton()
        {
            BossDataEditdr.SetData(selectedBossEnemyids[0]);
        }
        public void GuildExecTimeSettingButton(UnitData unitData)
        {
            //GuildExecTimeSetting.Init(unitData);
            MainManager.Instance.WindowConfigMessage("请前往数据更新页面设置！",null);

        }
        public void GuildExecTimeButton()
        {
            execTimeButtonAction?.Invoke();
        }
        public void LoadDataFromExcel()
        {
#if PLATFORM_ANDROID
                AndroidTool.OpenAndroidFileBrower();
                return;
#endif
                //MainManager.Instance.WindowConfigMessage("在做了", null, null);
            if (ExcelHelper.ExcelHelper.ReadExcelTimeLineData(out GuildTimelineData guildTimelineData,()=> { LoadDataFromexcel_failed(); }))
            {
                MainManager.Instance.WindowConfigMessage("从EXCEL导入的阵容将替换掉当前阵容，是否继续？", () => { LoadDataFromExcel_0(guildTimelineData); });
            }
        }
        private void LoadDataFromexcel_failed()
        {
            GameObject a = Instantiate(guildDataInportPannel);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            a.GetComponent<OpenInputFieldPrefab>().OnFinish = LoadDataFromExcel_1;
        }
        private void LoadDataFromExcel_0(GuildTimelineData guildTimelineData)
        {
            if(guildTimelineData == null || guildTimelineData.playerGroupData == null)
            {
                MainManager.Instance.WindowConfigMessage("无效的存档！" , null);
                return;
            }
            if (guildTimelineData.UBExecTime.Count == 5)
                guildTimelineData.UBExecTime.Add(new List<float>());
            int currentNum = SettingData.currentPlayerGroupNum;
            //int guild_currentNum = guildTimelineData.currentSettingData.currentPlayerGroupNum;
            //SettingData.addedPlayerDatas[currentNum] = guildTimelineData.currentSettingData.addedPlayerDatas[guild_currentNum];
            //SettingData.ubExecTimeDataDic[currentNum] = guildTimelineData.currentSettingData.ubExecTimeDataDic[guild_currentNum];
            //SettingData.useAutoModeDic[currentNum] = guildTimelineData.currentSettingData.useAutoModeDic[guild_currentNum];
            var timeLine = guildTimelineData.playerGroupData.timeLineData;
            if(timeLine == null)
            {
                guildTimelineData.playerGroupData.timeLineData = new GuildRandomData();
                timeLine = guildTimelineData.playerGroupData.timeLineData;
            }
            timeLine.UseFixedRandomSeed = true;
            timeLine.RandomSeed = guildTimelineData.currentRandomSeed;
            SettingData.SetCurrentRandomData(timeLine);
            SettingData.SetCurrentPlayerGroup(guildTimelineData.playerGroupData);
            SettingData.GetCurrentPlayerGroup().UBExecTimeData = guildTimelineData.UBExecTime;
            Refresh();
            SaveDataToJson();
        }
        private void LoadDataFromExcel_1(string dataStr)
        {
            if (dataStr.Contains("unlock"))
            {
                SystemOrder(dataStr);
                return;
            }
            try
            {
                string jsonStr = MainManager.DecryptDES(dataStr);
                GuildTimelineData guildTimelineData = JsonConvert.DeserializeObject<GuildTimelineData>(jsonStr);
                MainManager.Instance.WindowConfigMessage("从EXCEL导入的阵容将替换掉当前阵容，是否继续？", () => { LoadDataFromExcel_0(guildTimelineData); });

            }
            catch(Exception e)
            {
                MainManager.Instance.WindowConfigMessage("发生错误：" + e.Message, null);

            }
        }
        private void SystemOrder(string dataStr)
        {
            /*try
            {
                string[] order = dataStr.Split(' ');
                if (order[0] == "unlock" && order.Length >= 3)
                {
                    if(order[1] == "0163")
                    {
                        int month = int.Parse(order[2])%100;
                        if (month >= 4 && month <= 11)
                        {
                            if (SettingData.unlockGuilds[month])
                            {
                                MainManager.Instance.WindowConfigMessage("当月会战数据已经准备好了！", null, null);
                            }
                            else
                            {
                                SettingData.unlockGuilds[month] = true;
                                SaveDataToJson();
                                MainManager.Instance.WindowConfigMessage("正在尝试准备" + guildMonthNames[month] + "的数据，请重启程序！", null, null);
                            }
                        }
                    }
                }
            }
            catch
            {
                MainManager.Instance.WindowConfigMessage("无效的指令！", null, null);
            }*/
        }

        public void PlayerDatasPriviousButton()
        {
            if (currentPage <= 1)
            {
                return;
            }
            currentPage--;
            SettingData.currentPlayerGroupNum -= 5;
            RefreshCharacterGroupToggle();
            ChooseCharacterGroup();
        }
        public void PlayerDatasNextButton()
        {
            if (currentPage >= int.MaxValue)
            {
                MainManager.Instance.WindowConfigMessage("已达上限！", null);
            }
            if (SettingData.guildPlayerGroupDatas.Count < currentPage * 5 + 5)
            {
                EnlargePlayerDatas();
                MainManager.Instance.WindowMessage("已新增5支队伍");
                // MainManager.Instance.WindowConfigMessage("是否增加5组预设阵容？", EnlargePlayerDatas);
            }
            else
            {
                currentPage++;
                SettingData.currentPlayerGroupNum += 5;
                RefreshCharacterGroupToggle();
                ChooseCharacterGroup();
            }
        }
        public void EnlargePlayerDatas()
        {
            currentPage++;
            SettingData.currentPlayerGroupNum += 5;
            SettingData.AddOnePlayerGroup();
            RefreshCharacterGroupToggle();
            ChooseCharacterGroup();

        }
        public void OpenRandomSettingPage()
        {
            RandomManager.OpenSettingPage();
        }
        public void ReNameButton()
        {
            MainManager.Instance.WindowInputMessage("请输入自定义名字\n（不要输入特殊符号）", ReNameButton_1);
        }
        public void ReNameButton_1(string name)
        {
            SettingData.RenameGroupName(name);
            Refresh();
            SaveDataToJson();
        }
        public void HideButton()
        {
            MainManager.Instance.WindowConfigMessage("是否切换到jjc模拟器？", SwitchScene);
        }
        public void SwitchScene()
        {
            SceneManager.LoadScene("BeginScene");
        }
        public void CopyButon()
        {
            MainManager.Instance.WindowInputMessage("请输入要复制的队伍的序号\n（比如要将队伍1复制到这里就输入1）", CopyButton_0);
        }
        public void CopyButton_0(string numstr)
        {
            int id = 0;
            try
            {
                id = int.Parse(numstr);
                id--;
                if (id >= 0 && id < SettingData.guildPlayerGroupDatas.Count)
                {
                    //AddedPlayerData addedPlayerData = SettingData.addedPlayerDatas[id];
                    var playerGuildData = SettingData.guildPlayerGroupDatas[id];
                    string jsonStr = JsonConvert.SerializeObject(playerGuildData);
                    var deepCopy = JsonConvert.DeserializeObject<GuildPlayerGroupData>(jsonStr);
                    //var timeLine = SettingData.ubExecTimeDataDic[id];
                    //string jsonStr2 = JsonConvert.SerializeObject(timeLine);
                    //var deepCopy2 = JsonConvert.DeserializeObject<List<List<float>>>(jsonStr2);
                    //SettingData.ubExecTimeDataDic[SettingData.currentPlayerGroupNum] = deepCopy2;
                    //FinishEditingPlayers(deepCopy);
                    SettingData.SetCurrentPlayerGroup(deepCopy);
                    SaveDataToJson();
                    Refresh();

                }
                else
                    MainManager.Instance.WindowMessage("无效的队伍序号！");
            }
            catch
            {
                MainManager.Instance.WindowMessage("输入的信息无效！");
            }
        }
        public void DeleteButton()
        {
            MainManager.Instance.WindowConfigMessage("是否清空当前队伍？", DeleteGroup_0);
        }
        private void DeleteGroup_0()
        {
            SettingData.ClearCurrentPlayerGroup();
            SaveDataToJson();
            Refresh();
        }
        public void OpenUpdatePage()
        {
            SceneManager.LoadScene("Test");
        }
        private void CreateSpecialEnemyData()
        {
            string jsonData = JsonConvert.SerializeObject(EnemyDataDic[401010804]);
            if (!EnemyDataDic.ContainsKey(666666))
            {
                EnemyData enemyData = JsonConvert.DeserializeObject<EnemyData>(jsonData);
                enemyData.enemy_id = 666666;
                enemyData.unit_id = 304100;
                EnemyDataDic.Add(666666, enemyData);
            }
            if (!EnemyDataDic.ContainsKey(666667))
            {
                EnemyData enemyData2 = JsonConvert.DeserializeObject<EnemyData>(jsonData);
                enemyData2.enemy_id = 666667;
                enemyData2.unit_id = 304100;
                EnemyDataDic.Add(666667, enemyData2);
            }
        }
        public void OpenAutoCalculatePage()
        {
            GameObject a = Instantiate(autoCalculatePagePrefab);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            AutoCalculate autoCalculate = a.GetComponent<AutoCalculate>();
            autoCalculate.Init(MainManager.Instance.AutoCalculatorData);
        }
        public void StartAutoCalculate()
        {
            // StartCoroutine(AutoCalculate());
            StartCalButton(true);
        }
        public void StartAutoCalculateByButton(int time)
        {
            if (time <= 1)
            {
                MainManager.Instance.WindowConfigMessage("输入错误！", null);
                return;
            }
            MainManager.Instance.AutoCalculatorData.Reset();
            MainManager.Instance.AutoCalculatorData.execTime = time;
            MainManager.Instance.AutoCalculatorData.isCalculating = true;
            StartAutoCalculate();
        }
        // private IEnumerator AutoCalculate()
        // {
            // RandomManager.RandomData.UseFixedRandomSeed = false;
            // while (true)
            // {
            //     yield return new WaitForSecondsRealtime(1);
            //     if (MainManager.Instance.AutoCalculatorData.isPaues)
            //         continue;
            //     if (MainManager.Instance.AutoCalculatorData.isGoing)
            //     {                    
            //         StartCalButton(true);
            //     }
            //     break;
            // }
        // }

        public void UpdateButton()
        {
            updateManager.StartCheck();
            return;
            /*
            MainManager.Instance.WindowConfigMessage($"是否立即检查更新？", updateManager.StartCheck);
            return;
#if PLATFORM_ANDROID
            try
            {
                string path = Application.persistentDataPath + "/PCRCalculator6.08.apk";
                if (System.IO.File.Exists(path))
                {
                    AndroidTool.UpdateTotalAPK(path);
                }
            }
            catch(Exception ex)
            {
                Debug.LogError($"{ex.Message}\n{ex.StackTrace}");
                MainManager.Instance.WindowConfigMessage($"更新失败！{ex.Message}", null);
            }
#else
            MainManager.Instance.WindowConfigMessage($"请手动下载新版本", null);

#endif*/
        }
        //由java调用
        public void ImportExcelFileByAndroidNative(string filePath)
        {
            if (ExcelHelper.ExcelHelper.ReadExcelTimeLineData(out GuildTimelineData guildTimelineData, () => { LoadDataFromexcel_failed(); },filePath))
            {
                MainManager.Instance.WindowConfigMessage("从EXCEL导入的阵容将替换掉当前阵容，是否继续？", () => { LoadDataFromExcel_0(guildTimelineData); });
            }
        }

        //显示BOSS详细数据
        public void BossImageButton()
        {
            string detailstr = "    请选择BOSS!";
            EnemyData enemyData = GetEnemyDataByID(selectedBossEnemyids[0]);
            if (enemyData != null)
            {
                detailstr = $"  {enemyData.detailData.unit_name}({selectedBossEnemyids[0]})/({enemyData.detailData.unit_id})\n";
                detailstr += $" HP：{enemyData.baseData.RealHp}\n";
                detailstr += $" ATK：{enemyData.baseData.Atk}\n";
                detailstr += $" MGSTR：{enemyData.baseData.Magic_str}\n";
                detailstr += $" DEF：{enemyData.baseData.Def}\n";
                detailstr += $" MDEF：{enemyData.baseData.Magic_def}\n";
                int idx = 1;
                foreach (var pattern in enemyData.skillData.enemyAttackPatterns)
                {
                    string begin = "";
                    string loop = "";
                    for (int i = 0; i < pattern.loop_start - 1; i++)
                    {
                        begin += CharacterDetailManager.SkillInt2Str(pattern.atk_patterns[i]) + "-";
                    }
                    for (int i = pattern.loop_start - 1; i < pattern.loop_end; i++)
                    {
                        loop += CharacterDetailManager.SkillInt2Str(pattern.atk_patterns[i]) + "-";
                    }
                    detailstr += $" 攻击模式{idx++}:\n 起手：{begin}\n 循环：{loop}\n";
                }
                //int skID = enemyData.skillData.UB;
                //int skLV = enemyData.union_burst_level;
                Action<int, int,string> action = (skID, skLV,str) =>
                 {
                     detailstr += $" {str}:\n {skID}({skLV})\n";
                     SkillData sk = MainManager.Instance.SkillDataDic[skID];
                     detailstr += $" {sk.GetSkillDetailsEnemy(skLV, Math.Max(enemyData.baseData.Atk, enemyData.baseData.Magic_str))}\n";
                 };
                action(enemyData.skillData.UB, enemyData.union_burst_level,"UB");
                for(int i = 0; i < enemyData.main_skill_lvs.Count; i++)
                {
                    int skID = enemyData.skillData.MainSkills[i];
                    int skLV = enemyData.main_skill_lvs[i];
                    if(skID>0 && skLV > 0)
                    {
                        action(skID, skLV,$"技能{i+1}");
                    }
                }

            }


            GameObject a = Instantiate(bossDetailPrefab, BaseBackManager.Instance.latestUIback.transform);
            a.GetComponent<ShowTextUI>().Set(detailstr);
        }



        public static void SaveSettingData(GuildSettingData settingData)
        {
            SaveManager.Save(settingData);
        }


        //根据路径或者URL读取本地文件并转换为Texture2D文件
        public void GetBookSprite(string url)
        {
            StartCoroutine(DownSprite(url));
        }

        IEnumerator DownSprite(string url)
        {
            var uri = new System.Uri(Path.Combine(url));
            UnityWebRequest www = UnityWebRequest.Get(uri);
            DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
            www.downloadHandler = texDl;

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Texture2D tex = new Texture2D(1, 1);
                tex = texDl.texture;
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                backgroundImage.sprite = sprite;
            }
        }
        public void SelectImage()
        {
#if PLATFORM_ANDROID
            MainManager.Instance.WindowConfigMessage("Android端暂时不支持！", null);
            return ;
#else
            var imagePath = StandaloneFileBrowser.OpenFilePanel(
                  "选择背景图片", string.Empty, new ExtensionFilter[]
                  {
                                    new ExtensionFilter("", "png", "jpeg", "jpg"),
                  }, false);
              if (imagePath.Length > 0) {
                  _ = imagePath[0];
                  string file = imagePath[0];
                  file = file.Replace("\\", "/");
                  GetBookSprite(file);
              }
#endif
        }
        public void ClearImage(){
            backgroundImage.sprite = null;
        }
        public void clearUb()
        {
            MainManager.Instance.WindowConfigMessage("是否清空当前队伍UB？", clearUb_0);
        }
        public void clearUb_0(){
          SettingData.ClearUBExecTimeData(SemanModeToggle.isOn);
          SaveDataToJson();
          Refresh();
        }
        public void ClearAllTeamButton()
        {
          MainManager.Instance.WindowConfigMessage("是否清空所有队伍信息？", ClearAllTeamButton_0);
        }
        public void ClearAllTeamButton_0()
        {
            SettingData.ClearGuildPlayerGroupDatas();
            SaveDataToJson();
            currentPage = 1;
            SettingData.currentPlayerGroupNum = 0;
            RefreshCharacterGroupToggle();
            ChooseCharacterGroup();
            Refresh();
        }
        public void isShowInfoPanelButton(){
          infoPanel.SetActive(!infoPanel.activeSelf);
        }
        public void CustomBoss()
        {
            SettingData.GetCurrentRandomData().RandomSeed = int.Parse(RandomSeed.text);
            SettingData.GetCurrentRandomData().UseFixedRandomSeed = UseFixedRandomSeed.isOn;
            SettingData.GetCurrentPlayerGroup().playerSetingHP = int.Parse(currentHPinput.text);
            SettingData.GetCurrentPlayerGroup().usePlayerSettingHP = toggles_ChooseBoss[2].isOn;
            SaveDataToJson();
        }
    }
    [Serializable]
    public class GuildEnemyData
    {
        public int[][] enemyIds;
    }
    [Serializable]
    public class GuildSettingData
    {
        /*public List<AddedPlayerData> addedPlayerDatas;
        public Dictionary<int, List<List<float>>> ubExecTimeDataDic;
        public Dictionary<int, bool> useAutoModeDic = new Dictionary<int, bool>();// = new Dictionary<int, bool> { { 0, false }, { 0, false }, { 0, false }, { 0, false }, { 0, false } };
        public List<GuildRandomData> guildRandomDatas;
        public int currentGuildMonth;
        public int currentGuildEnemyNum;
        public int currentTurn;*/

        public string start_hp, start_tp;
        public int n1 = 1000, n2 = 10000;
        public List<GuildPlayerGroupData> guildPlayerGroupDatas;

        public Dictionary<int, UnitAttackPattern> changedEnemyAttackPatternDic = new Dictionary<int, UnitAttackPattern>();
        public Dictionary<int, EnemyData> changedEnemyDataDic;

        public int currentPlayerGroupNum;
        public bool Favriote = true;
        public bool Unused = true;
        //public bool isViolent;
        public int calSpeed;
        public int FPSforLogic = 60;
        public int FPSforAnimation = 60;
        public bool calcSpineAnimation = true;
        public int UBTryingCount = 30;
        public bool UseFixedRandomSeed = true;
        /*public int RandomSeed = 666;
        public bool ForceNoCritical_player;
        public bool ForceNoCritical_enemy;
        public bool ForceIgnoreDodge_player;
        public bool ForceIgnoreDodge_enemy;*/
        //public int currentRandomLine = 0;
        //public float BodyColliderWidth = 100;
        //public bool usePlayerSettingHP;
        //public int playerSetingHP;
        public Dictionary<int, float> bossAppearDelayDic = new Dictionary<int, float>();
        public Dictionary<int, float> bossSkillCastTimeDic = new Dictionary<int, float>();
        public Dictionary<int, float> bossBodyWidthDic = new Dictionary<int, float>();
        //public float BossAbnormalAddValue = 0.867f;
        //public float BossAbnormalMultValue = 1;
        //public bool usePhysics = true;
        public bool usePlayerSQL = true;
        //public bool useSkillEffects = true;
        //public float skillEffeckFix = 35;
        public int limitTime = 90;
        //public bool[] unlockGuilds = new bool[12] { false, false, false, true, true, true, true, false, false, false, false, false };
        public string author = "", format = "m:ss";
        public string dispFields = "Left/Mid/Right";
        public GuildSettingData() { }
        public GuildSettingData(bool createNew)
        {
            guildPlayerGroupDatas = new List<GuildPlayerGroupData>();
            AddOnePlayerGroup();
            //addedPlayerDatas = new List<AddedPlayerData> { new AddedPlayerData(), new AddedPlayerData(), new AddedPlayerData(), new AddedPlayerData(), new AddedPlayerData() };
            changedEnemyDataDic = new Dictionary<int, EnemyData>();
           // ubExecTimeDataDic = new Dictionary<int, List<List<float>>>();
            //useAutoModeDic = new Dictionary<int, bool>();
            /*for(int i = 0; i < 5; i++)
            {
                List<List<float>> delist = new List<List<float>> { new List<float>(), new List<float>(), new List<float>(), new List<float>(), new List<float>() };
                ubExecTimeDataDic.Add(i, delist);
                useAutoModeDic.Add(i, false);
            }*/
            currentPlayerGroupNum = 0;
            //currentGuildMonth = 5;
            //currentGuildEnemyNum = 0;
            //currentTurn = 0;
            //isViolent = false;
            calSpeed = 4;
            //guildRandomDatas = new List<GuildRandomData>();
            //guildRandomDatas.Add(new GuildRandomData());
        }
        public void AddOnePlayerGroup()
        {
           //int pr = addedPlayerDatas.Count;
            for (int i = 0; i < 5; i++)
            {
                //addedPlayerDatas.Add(new AddedPlayerData());
                //List<List<float>> delist = new List<List<float>> { new List<float>(), new List<float>(), new List<float>(), new List<float>(), new List<float>() };
                //ubExecTimeDataDic.Add(i + pr, delist);
                //if (!useAutoModeDic.ContainsKey(i + pr))
                //    useAutoModeDic.Add(i + pr, false);
                GuildPlayerGroupData data = new GuildPlayerGroupData();
                data.Init();
                guildPlayerGroupDatas.Add(data);
            }

        }
        public AddedPlayerData GetCurrentPlayerData()
        {
            while (currentPlayerGroupNum >= guildPlayerGroupDatas.Count)
            {
                GuildPlayerGroupData data = new GuildPlayerGroupData();
                data.Init();
                guildPlayerGroupDatas.Add(data);
            }

            if (currentPlayerGroupNum < 0) currentPlayerGroupNum = 0;
            return guildPlayerGroupDatas[currentPlayerGroupNum].playerData;
        }
        public GuildPlayerGroupData GetCurrentPlayerGroup()
        {
            while (currentPlayerGroupNum >= guildPlayerGroupDatas.Count)
            {
                GuildPlayerGroupData data = new GuildPlayerGroupData();
                data.Init();
                guildPlayerGroupDatas.Add(data);
            }

            if (currentPlayerGroupNum < 0) currentPlayerGroupNum = 0;
            return guildPlayerGroupDatas[currentPlayerGroupNum];
        }
        public void SetCurrentPlayerData(AddedPlayerData playerData)
        {
            guildPlayerGroupDatas[currentPlayerGroupNum].playerData = playerData;
        }
        public void SetCurrentPlayerGroup(GuildPlayerGroupData playerGroupData)
        {
            guildPlayerGroupDatas[currentPlayerGroupNum] = playerGroupData;
        }
        public void ClearCurrentPlayerGroup()
        {
            var emptyData = new GuildPlayerGroupData();
            emptyData.Init();
            guildPlayerGroupDatas[currentPlayerGroupNum] = emptyData;
        }
        public void ClearGuildPlayerGroupDatas()
        {
            guildPlayerGroupDatas.Clear();
        }
        public void ClearUBExecTimeData(bool semanUB)
        {
            if (semanUB)
            {
                guildPlayerGroupDatas[currentPlayerGroupNum].SemanUBExecTimeData = new List<List<int>> { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>() };
            }
            else
            {
                guildPlayerGroupDatas[currentPlayerGroupNum].UBExecTimeData = new List<List<float>> { new List<float>(), new List<float>(), new List<float>(), new List<float>(), new List<float>(), new List<float>() };
            }
        }
        public GuildRandomData GetCurrentRandomData()
        {
            /*if(guildRandomDatas == null)
            {
                guildRandomDatas = new List<GuildRandomData>();
            }
            int idx = Mathf.Max(0, Mathf.Min(currentRandomLine, guildRandomDatas.Count));
            return guildRandomDatas[idx];*/
            return GetCurrentPlayerGroup().timeLineData;
        }
        public void SetCurrentRandomData(GuildRandomData guildTimelineData)
        {
            /*if (guildRandomDatas == null)
            {
                guildRandomDatas = new List<GuildRandomData>();
            }
            int idx = Mathf.Max(0, Mathf.Min(currentRandomLine, guildRandomDatas.Count));
            guildRandomDatas[idx] = guildTimelineData;*/
            guildPlayerGroupDatas[currentPlayerGroupNum].timeLineData = guildTimelineData;
        }
        public string GetCurrentBossDes()
        {
            string des = "";
            var data = GetCurrentPlayerGroup();
            des += (char)('A' + data.currentTurn - 1);
            if (data.currentGuildEnemyNum == 4 && data.isViolent)
                des += "6";
            else
                des += (data.currentGuildEnemyNum+1);
            if (data.usePlayerSettingHP)
                des += "#";
            return des;
        }
        public void RenameGroupName(string name)
        {
            GetCurrentPlayerData().playerName = name;
        }
        public void ReplaceUBTimeData(List<List<float>> data)
        {
            GetCurrentPlayerGroup().UBExecTimeData = data;
        }
    }
    [Serializable]
    public class GuildPlayerGroupData
    {
        public AddedPlayerData playerData;
        public List<List<float>> UBExecTimeData = new List<List<float>> {};
        public List<List<int>> SemanUBExecTimeData = new List<List<int>> {};
        public bool useAutoMode;
        public bool useSetMode;
        public bool useSemanMode;
        public GuildRandomData timeLineData;
        public int currentGuildMonth=9;
        public int currentGuildEnemyNum=1;
        public int currentTurn=1;
        public int[] selectedEnemyIDs;
        public bool isViolent;
        public bool usePlayerSettingHP;
        public int playerSetingHP;
        public LogBarrierType useLogBarrierNew;

        public enum LogBarrierType
        {
            [Description("无盾")]
            NoBarrier = 0,
            [Description("无盾有tp")]
            TpOnly = 1,
            [Description("有盾")]
            FullBarrier = 2
        }

        public bool isSpecialBoss;
        public int specialBossID;
        public int specialInputValue;

        public void Init()
        {
            playerData = new AddedPlayerData();
            playerData.playerName = "新建队伍";
            timeLineData = new GuildRandomData();
        }
    }
}