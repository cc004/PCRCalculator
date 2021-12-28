using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft0.Json;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using Elements;

namespace PCRCaculator.Guild
{
    public class GuildManager : MonoBehaviour
    {
        public static GuildManager Instance;
        //public TextAsset enemyDataDicTxt;
        public List<CharacterPageButton> charactersUI;
        public Text characterGroupNameText;
        public Toggle AutoModeToggle;
        public List<UBTime> UnitUBTimes;
        public Text UBTimeEditButtonText;
        public List<Toggle> characterGroupToggles;
        public List<Text> characterGruopTexts;
        public Text characterGroupPageText;
        public List<Text> timeLineTexts;

        public GameObject characterDetailPage;
        public CharacterPageButton characterDetailButton;
        public Action execTimeButtonAction;
        public List<Text> characterDetailTexts;

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


        //public List<Sprite> bossSprites;
        //public List<string> bossNames;
        //public List<int> enemy_ids;
        public List<GuildEnemyData> guildEnemyDatas;
        public List<int> specialEnemyDatas;

        //public GuildExecTimeSetting GuildExecTimeSetting;
        public GuildRandomManager RandomManager;
        public GameObject guildDataInportPannel;

        public GameObject autoCalculatePagePrefab;

        //private List<AddedPlayerData> addedPlayerDatas;
        private static Dictionary<int, EnemyData> enemyDataDic;
        private Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts> enemyMPartsDic = new Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts>();
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
        private int selectedBossEnemyid => SettingData.GetCurrentPlayerGroup().selectedEnemyID;
        private bool isEditongUBTime;
        private static string[] guildMonthNames = new string[12] { "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "摩羯座", "水瓶座", "双鱼座" };
        private int currentPage;
        private bool isInit = false;
        private int specialEnemyid;
        private int specialInputValue;

        public static Dictionary<int, EnemyData> EnemyDataDic
        {
            get
            {
                if (enemyDataDic == null)
                {
                    string enemyDataDicTxt = MainManager.Instance.LoadJsonDatas("Datas/EnemyDataDic");
                    enemyDataDic = JsonConvert.DeserializeObject<Dictionary<int, EnemyData>>(enemyDataDicTxt);
                }
                return enemyDataDic;
            }
        }
        public Dictionary<int, MasterEnemyMParts.EnemyMParts> EnemyMPartsDic { get => enemyMPartsDic;}
        public bool isGuildBoss { get => toggles_ChooseType[0].isOn; }

        private void Awake()
        {
            Instance = this;
            LoadAddedPlayerData();
        }
        private void Start()
        {
            StartCoroutine(StartAfterWait());
        }
        private IEnumerator StartAfterWait()
        {
            yield return null;
            Load();
            //LoadAddedPlayerData();
            ReflashOnStart();
            FixGuildEnemyErrors();
            if (MainManager.Instance.AutoCalculatorData.isCalculating && !MainManager.Instance.AutoCalculatorData.isConfig)
            {
                OpenAutoCalculatePage();
                StartAutoCalculate();
                if (!MainManager.Instance.AutoCalculatorData.isGoing)
                    MainManager.Instance.AutoCalculatorData.isConfig = true;
            }
        }
        private void Load()
        {
            //TextAsset enemyDataDicTxt = Resources.Load<TextAsset>("Datas/EnemyDataDic");
            //string enemyDataDicTxt = MainManager.Instance.LoadJsonDatas("Datas/EnemyDataDic");
            //enemyDataDic = JsonConvert.DeserializeObject<Dictionary<int, EnemyData>>(enemyDataDicTxt);
            string guildenemyDatasStr = MainManager.Instance.LoadJsonDatas("Datas/GuildEnemyDatas");
            if (!string.IsNullOrEmpty(guildenemyDatasStr))
            {
                guildEnemyDatas = JsonConvert.DeserializeObject<List<GuildEnemyData>>(guildenemyDatasStr);
            }
            string enemyMParts = MainManager.Instance.LoadJsonDatas("Datas/EnemyMPartsDic");
            enemyMPartsDic = JsonConvert.DeserializeObject<Dictionary<int, Elements.MasterEnemyMParts.EnemyMParts>>(enemyMParts);
            CreateSpecialEnemyData();
        }
        public void SelectCharacterButton()
        {
            ChoosePannelManager.Instance.CallChooseBack(3, null);
        }
        public void ShowCharacterDetailPage(int idx)
        {
            if(idx>= SettingData.GetCurrentPlayerData().playrCharacters.Count)
            {
                return;
            }
            characterDetailPage.SetActive(true);
            UnitData unitData = SettingData.GetCurrentPlayerData().playrCharacters[idx];
            characterDetailButton.SetButton(unitData);
            BaseData baseData = PCRCaculator.MainManager.Instance.UnitRarityDic[unitData.unitId].GetBaseData(unitData,false,false);
            characterDetailTexts[0].text = "" + baseData.Hp;
            for (int i = 1; i < baseData.dataint.Length; i++)
            {
                characterDetailTexts[i].text = "" + baseData.dataint[i] / 100.0f;
            }
            execTimeButtonAction = () => { GuildExecTimeSettingButton(unitData); };
        }
        public void EditCharacterdetail()
        {
            ChoosePannelManager.Instance.CallChooseBack(4, SettingData.GetCurrentPlayerData());
        }
        public void FinishEditCharacterdetail()
        {
            ReflashCharacterGroupToggle();
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
                    Reflash();
                }
                idx++;
            }
        }
        public void FinishEditingPlayers(AddedPlayerData playerData)
        {
            //addedPlayerDatas = new List<AddedPlayerData>() { playerData };
            SettingData.SetCurrentPlayerData(playerData);
            Reflash();
            SaveDataToJson();
        }
        public void OnChooseBossDropDownChanged()
        {
            //bossImage_ChooseBoss.sprite = bossSprites[dropdowns_ChooseBoss[0].value];
            int enemyid_0 = 0;
            string Path;
            if (isGuildBoss)
            {
                enemyid_0 = guildEnemyDatas[dropdowns_ChooseBoss[2].value].enemyIds_1[dropdowns_ChooseBoss[0].value];
                //Path = "GuildEnemy/icon_unit_" + enemyDataDic[enemyid_0].unit_id;
                //bossImage_ChooseBoss.sprite = MainManager.LoadSourceSprite(Path);
                bossImage_ChooseBoss.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, enemyDataDic[enemyid_0].unit_id);
            }
            else
            {
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
                }
            }
            specialEnemyid = enemyid_0;
            specialInputValue = 100;
        }
        public void OnInputFinished()
        {
            if(!isGuildBoss && dropdowns_ChooseBoss[3].value == 2)
            {
                int bossid = int.Parse(specialInput.text, System.Globalization.NumberStyles.Any);
                if(enemyDataDic.TryGetValue(bossid,out var data))
                {
                    //string Path = "GuildEnemy/icon_unit_" + data.unit_id;
                    //bossImage_ChooseBoss.sprite = MainManager.LoadSourceSprite(Path);
                    bossImage_ChooseBoss.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, data.unit_id);
                    specialEnemyid = bossid;
                }
                else
                {
                    MainManager.Instance.WindowConfigMessage("bossID无效！", null);
                    specialEnemyid = 0;
                }
                specialInputValue = 0;
            }
            else if (!isGuildBoss)
            {
                specialInputValue = int.Parse(specialInput.text, System.Globalization.NumberStyles.Any);
            }
        }
        public void FinishChooseBossButton()
        {
            int enemyId = 0;
            if (isGuildBoss)
            {
                int month = dropdowns_ChooseBoss[2].value;
                SettingData.GetCurrentPlayerGroup().currentGuildMonth = month;
                bossDetailTexts[3].text = guildMonthNames[month];
                int num = dropdowns_ChooseBoss[0].value; ;
                SettingData.GetCurrentPlayerGroup().currentGuildEnemyNum = num;
                int turn = dropdowns_ChooseBoss[1].value + 1; 
                SettingData.GetCurrentPlayerGroup().currentTurn = turn;
                enemyId = GetGuildBossID(month, num, turn);
            }
            else
            {
                enemyId = specialEnemyid;
            }
            //selectedBossEnemyid = SettingData.currentTurn == 1 ? guildEnemyData.enemyIds_1[SettingData.currentGuildEnemyNum] : guildEnemyData.enemyIds_2[SettingData.currentGuildEnemyNum];

            var group = SettingData.GetCurrentPlayerGroup();
            group.selectedEnemyID = enemyId;
            group.isSpecialBoss = !isGuildBoss;
            group.specialBossID = specialEnemyid;
            group.specialInputValue = specialInputValue;
            group.useLogBarrier = toggles_ChooseBoss[3].isOn;
            SaveDataToJson();
            Reflash();
        }
        public int GetGuildBossID(int month, int num, int turn)
        {
            int enemyId = 0;
            GuildEnemyData guildEnemyData = guildEnemyDatas[month];
            enemyId = int.Parse($"4010{turn}{13 + month}0{num + 1}");
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
            SettingData.calSpeed = Mathf.RoundToInt(calSlider.value);
            calSettingTexts[0].text = "x" + SettingData.calSpeed;
            calSettingTexts[1].text = Mathf.RoundToInt(90.0f / SettingData.calSpeed) + "s";
        }
        public void StartCalButton()
        {
            /*if (!CheckDataIsReady(SettingData..currentGuildMonth))
            {
                return;
            }*/
            SaveDataToJson();
            EnemyData enemyData = GetEnemyDataByID(selectedBossEnemyid);
            GuildBattleData battleData = new GuildBattleData();
            battleData.players = SettingData.GetCurrentPlayerData();
            battleData.enemyData = enemyData;
            if(enemyMPartsDic.TryGetValue(enemyData.enemy_id,out var value))
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
            battleData.UBExecTimeList = SettingData.GetCurrentPlayerGroup().UBExecTimeData;
            battleData.isAutoMode = AutoModeToggle.isOn;
            /*battleData.isViolent = SettingData.isViolent;
            battleData.FPSforLogic = SettingData.FPSforLogic;
            battleData.FPSforAnimaton = SettingData.FPSforAnimation;
            battleData.playAnimation = SettingData.calcSpineAnimation && SettingData.calSpeed == 1;
            battleData.calSpeed = SettingData.calSpeed;
            battleData.UBTryingCount = SettingData.UBTryingCount;
            battleData.useFixedRandomSeed = SettingData.UseFixedRandomSeed;
            battleData.RandomSeed = SettingData.RandomSeed;
            battleData.changedATKPatternDic = SettingData.changedEnemyAttackPatternDic;*/
            battleData.SettingData = SettingData;
            MainManager.Instance.ChangeSceneToBalttle(battleData);
        }
        private EnemyData GetEnemyDataByID(int enemy_id)
        {
            return SettingData.changedEnemyDataDic.TryGetValue(enemy_id, out EnemyData data) ? data : enemyDataDic[enemy_id];
        }
        /*private bool CheckDataIsReady(int month)
        {
            if (SettingData.unlockGuilds[month]) { return true; }
            MainManager.Instance.WindowConfigMessage(guildMonthNames[month] + "的怪物数据丢失，无法继续！",null, null);
            return false;
        }*/
        private void ReflashOnStart()
        {
            isInit = true;
            currentPage = Mathf.FloorToInt(SettingData.currentPlayerGroupNum / 5.0f) + 1;
            if (currentPage < 1 || currentPage > 20)
            {
                currentPage = 1;
            }
            ReflashCharacterGroupToggle();
            var data = SettingData.GetCurrentPlayerGroup();
            dropdowns_ChooseBoss[2].value = data.currentGuildMonth;
            dropdowns_ChooseBoss[0].value = data.currentGuildEnemyNum;
            dropdowns_ChooseBoss[1].value = data.currentTurn - 1;
            //toggles_ChooseBoss.isOn = SettingData.isViolent;
            if (data.isViolent)
                toggles_ChooseBoss[0].isOn = true;
            else if (data.usePlayerSettingHP)
            {
                toggles_ChooseBoss[2].isOn = true;
                currentHPinput.text = "" + data.playerSetingHP;
            }
            else
                toggles_ChooseBoss[1].isOn = true;
            //FinishChooseBossButton();
            calSlider.value = SettingData.calSpeed;
            if(data.timeLineData == null)
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
            Reflash();
            ReflashCalcSettings_start();
            EditingUBTime(false);
            AutoModeToggle.isOn = data.useAutoMode;
            isInit = false;
        }
        private void ReflashCharacterGroupToggle()
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
        private void Reflash()
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
                if (i < playerData.playrCharacters.Count)
                {
                    charactersUI[i].SetButton(playerData.playrCharacters[i]);
                    UnitUBTimes[i].SetUBTimes(data.UBExecTimeData[i]);
                }
                else
                {
                    charactersUI[i].SetButton(0, null);
                    UnitUBTimes[i].SetUBTimes(new List<float>());
                }
            }
            AutoModeToggle.isOn = data.useAutoMode;

            timeLineTexts[0].text = data.timeLineData.DataName;
            timeLineTexts[1].text = data.timeLineData.GetDescribe();

            var group = SettingData.GetCurrentPlayerGroup();

            if (selectedBossEnemyid == 0)
                data.selectedEnemyID = GetGuildBossID(data.currentGuildMonth, data.currentGuildEnemyNum, data.currentTurn);
            //string Path = "GuildEnemy/icon_unit_" + enemyDataDic[selectedBossEnemyid].unit_id;
            //bossPicture.sprite = MainManager.LoadSourceSprite(Path);
            bossPicture.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, enemyDataDic[selectedBossEnemyid].unit_id);
            bossDetailTexts[0].text = group.isSpecialBoss ? "自定义": enemyDataDic[selectedBossEnemyid].name;
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
            if (toggles_ChooseBoss[2].isOn)
            {
                group.usePlayerSettingHP = true;
                group.playerSetingHP = int.Parse(currentHPinput.text);
            }
            else
            {
                group.usePlayerSettingHP = false;
                group.playerSetingHP = 0;
            }
            bossDetailTexts[2].text = group.isViolent ? "狂暴" : (group.usePlayerSettingHP ? "自定义HP" : "--");
            //group.useLogBarrier = toggles_ChooseBoss[3].isOn;
        }
        private void ReflashCalcSettings_start()
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
            SettingInputs[1].text = SettingData.BodyColliderWidth + "";
            SettingInputs[2].text = SettingData.BossAbnormalMultValue + "";
            SettingInputs[3].text = SettingData.BossAbnormalAddValue + "";
            RandomManager.Reflash();
            SettingToggles[7].isOn = SettingData.usePhysics;
            SettingToggles[8].isOn = SettingData.useSkillEffects;
            SettingInputs[4].text = SettingData.skillEffeckFix + "";
        }
        public void ReflashCalcUI()
        {
            SettingTexts[0].text = (int)SettingSliders[0].value * 10 + "";
            SettingTexts[1].text = (int)SettingSliders[1].value * 10 + "";
            SettingTexts[2].text = (int)SettingSliders[2].value * 10 + "";
            SettingToggles[0].interactable = SettingData.calSpeed == 1;
            SettingToggles[7].isOn = SettingData.usePhysics;
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
                SettingData.BodyColliderWidth = float.Parse(SettingInputs[1].text);
            }
            catch
            {
                MainManager.Instance.WindowConfigMessage("输入错误！", null, null);
                //SettingData.RandomSeed = 666;

            }
            /*SettingData.ForceNoCritical_player = SettingToggles[3].isOn;
            SettingData.ForceNoCritical_enemy = SettingToggles[4].isOn;
            SettingData.ForceIgnoreDodge_player = SettingToggles[5].isOn;
            SettingData.ForceIgnoreDodge_enemy = SettingToggles[6].isOn;*/
            SettingData.BossAbnormalMultValue = float.Parse(SettingInputs[2].text);
            SettingData.BossAbnormalAddValue = float.Parse(SettingInputs[3].text);
            MainManager.Instance.ChangeBodyWidth(SettingData.BodyColliderWidth);
            SettingData.usePhysics = SettingToggles[7].isOn;
            SettingData.useSkillEffects = SettingToggles[8].isOn;
            SettingData.skillEffeckFix = float.Parse(SettingInputs[4].text);
            SaveDataToJson();
        }
        private static void LoadAddedPlayerData()
        {
            string filePath = PCRCaculator.MainManager.GetSaveDataPath() + "/Guild/GuildSettingData.json";
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string jsonStr = sr.ReadToEnd();
                sr.Close();
                if (jsonStr != "")
                {
                    staticsettingData = JsonConvert.DeserializeObject<GuildSettingData>(jsonStr);
                    return;
                }
            }
            staticsettingData = new GuildSettingData(true);
        }
        private void FixGuildEnemyErrors()
        {
            foreach (UnitAttackPattern unitAttackPattern in MainManager.Instance.AllUnitAttackPatternDic.Values)
            {
                if (unitAttackPattern.unit_id == 302000)
                {
                    unitAttackPattern.atk_patterns = new int[20] { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                }
                if (unitAttackPattern.unit_id == 301101)
                {
                    unitAttackPattern.atk_patterns = new int[20] { 1, 1, 1001, 1, 1001, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                }
                if (unitAttackPattern.unit_id == 303900)
                {
                    unitAttackPattern.atk_patterns = new int[20] { 1, 1001, 1001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                }
            }
            //EnemyDataDic[401010504].skillData.MainSkills[1]=(3011013);
           // EnemyDataDic[401011504].skillData.MainSkills[1]=(3011013);



        }
        public void SaveDataToJson()
        {
            SaveSettingData(SettingData);
        }
        public void EditingUBTime(bool isButton)
        {
            if (isButton)
            {
                isEditongUBTime = !isEditongUBTime;
            }
            else
            {
                isEditongUBTime = false;
                AutoModeToggle.interactable = false;
            }
            UBTimeEditButtonText.text = isEditongUBTime ? "保存" : "修改";
            int idx = 0;
            foreach(UBTime uBTime in UnitUBTimes)
            {
                if (isEditongUBTime)
                {
                    uBTime.StartEdit();
                    AutoModeToggle.interactable = true;
                }
                else
                {
                    uBTime.FinishEdit();
                    if (isButton)
                    {
                        SettingData.GetCurrentPlayerGroup().UBExecTimeData[idx] = uBTime.GetUBTimes();
                    }
                }
                idx++;
            }
            if(isButton && !isEditongUBTime)
            {
                AutoModeToggle.interactable = false;
                SettingData.GetCurrentPlayerGroup().useAutoMode = AutoModeToggle.isOn;
                SaveDataToJson();
            }
        }
        public void UBTimeHelpButton()
        {
            string msg = "输入UB时间，可以输入帧（>100）或秒(<90),秒支持小数\n帧从0开始到5400结束\n秒从90秒开始倒计时到0结束\n例如输入60表示在倒计时60秒末跳到59秒前释放UB\n勾选自动模式则手动输入的UB时间无效";
            MainManager.Instance.WindowConfigMessage(msg, null, null);
        }
        public void GuildBossDataEditButton()
        {
            BossDataEditdr.SetData(selectedBossEnemyid);
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
            //MainManager.Instance.WindowConfigMessage("在做了", null, null);
            if (ExcelHelper.ExcelHelper.ReadExcelTimeLineData(out GuildTimelineData guildTimelineData,()=> { LoadDataFromexcel_failed(); }))
            {
                MainManager.Instance.WindowConfigMessage("从EXCEL导入的阵容将替换掉当前阵容，是否继续？", () => { LoadDataFromExcel_0(guildTimelineData); }, null);
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
                MainManager.Instance.WindowConfigMessage("无效的存档！" , null, null);
                return;
            }
            int currentNum = SettingData.currentPlayerGroupNum;
            //int guild_currentNum = guildTimelineData.currentSettingData.currentPlayerGroupNum;
            //SettingData.addedPlayerDatas[currentNum] = guildTimelineData.currentSettingData.addedPlayerDatas[guild_currentNum];
            //SettingData.ubExecTimeDataDic[currentNum] = guildTimelineData.currentSettingData.ubExecTimeDataDic[guild_currentNum];
            //SettingData.useAutoModeDic[currentNum] = guildTimelineData.currentSettingData.useAutoModeDic[guild_currentNum];
            var timeLine = guildTimelineData.playerGroupData.timeLineData;
            timeLine.UseFixedRandomSeed = true;
            timeLine.RandomSeed = guildTimelineData.currentRandomSeed;
            SettingData.SetCurrentRandomData(timeLine);
            SettingData.SetCurrentPlayerGroup(guildTimelineData.playerGroupData);
            Reflash();
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
                MainManager.Instance.WindowConfigMessage("从EXCEL导入的阵容将替换掉当前阵容，是否继续？", () => { LoadDataFromExcel_0(guildTimelineData); }, null);

            }
            catch(System.Exception e)
            {
                MainManager.Instance.WindowConfigMessage("发生错误：" + e.Message, null, null);

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
            ReflashCharacterGroupToggle();
            ChooseCharacterGroup();
        }
        public void PlayerDatasNextButton()
        {
            if (currentPage >= 20)
            {
                MainManager.Instance.WindowConfigMessage("已达上限！", null, null);
            }
            if (SettingData.guildPlayerGroupDatas.Count < currentPage * 5 + 5)
            {
                MainManager.Instance.WindowConfigMessage("是否增加5组预设阵容？", EnlargePlayerDatas, null);
            }
            else
            {
                currentPage++;
                SettingData.currentPlayerGroupNum += 5;
                ReflashCharacterGroupToggle();
                ChooseCharacterGroup();
            }
        }
        public void EnlargePlayerDatas()
        {
            currentPage++;
            SettingData.currentPlayerGroupNum += 5;
            SettingData.AddOnePlayerGroup();
            ReflashCharacterGroupToggle();
            ChooseCharacterGroup();

        }
        public void OpenRandomSettingPage()
        {
            RandomManager.OpenSettingPage();
        }
        public void ReNameButton()
        {
            MainManager.Instance.WindowInputMessage("请输入自定义名字（不要输入特殊符号）", ReNameButton_1);
        }
        public void ReNameButton_1(string name)
        {
            SettingData.RenameGroupName(name);
            Reflash();
            SaveDataToJson();
        }
        public void HideButton()
        {
            MainManager.Instance.WindowConfigMessage("是否切换到jjc模拟器？", SwitchScene, null);
        }
        public void SwitchScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BeginScene");
        }
        public void CopyButon()
        {
            MainManager.Instance.WindowInputMessage("请输入要复制的队伍的序号（比如要将队伍1复制到这里就输入1）", CopyButton_0);
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
                    Reflash();

                }
                else
                    MainManager.Instance.WindowMessage("无效的队伍序号！");
            }
            catch
            {
                MainManager.Instance.WindowMessage("输入的信息无效！");
                return;
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
            Reflash();
        }
        public void OpenUpdatePage()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Test");
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
            StartCoroutine(AutoCalculate());
        }
        public void StartAutoCalculateByButton(int time)
        {
            if (time <= 1 || time >= 20)
            {
                MainManager.Instance.WindowConfigMessage("输入错误！", null);
                return;
            }
            MainManager.Instance.AutoCalculatorData.Reset();
            MainManager.Instance.AutoCalculatorData.execTime = time;
            MainManager.Instance.AutoCalculatorData.isCalculating = true;
            StartAutoCalculate();
        }
        private IEnumerator AutoCalculate()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(2);
                if (MainManager.Instance.AutoCalculatorData.isPaues)
                    continue;
                else if (MainManager.Instance.AutoCalculatorData.isGoing)
                {                    
                    StartCalButton();
                }
                break;
            }

        }

        public static void SaveSettingData(GuildSettingData settingData)
        {
            string filePath = PCRCaculator.MainManager.GetSaveDataPath() + "/Guild/GuildSettingData.json";
            if (!Directory.Exists(PCRCaculator.MainManager.GetSaveDataPath() + "/Guild"))
            {
                Directory.CreateDirectory(PCRCaculator.MainManager.GetSaveDataPath() + "/Guild");
            }
            string saveJsonStr = JsonConvert.SerializeObject(settingData);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
        }


    }
    [System.Serializable]
    public class GuildEnemyData
    {
        public List<int> enemyIds_1;//一周目数据
        public List<int> enemyIds_2;//二周目数据
        public List<int> enemyIds_3;//三周目数据
        public List<int> enemyIds_4;//三周目数据
        public List<int> enemyIds_5;//三周目数据
        public GuildEnemyData() { }
        public void SetLapData(int lap,List<int> ids)
        {
            switch (lap)
            {
                case 1:
                    enemyIds_1 = ids;
                    break;
                case 2:
                    enemyIds_2 = ids;
                    break;
                case 3:
                    enemyIds_3 = ids;
                    break;
                case 4:
                    enemyIds_4 = ids;
                    break;
                case 5:
                    enemyIds_5 = ids;
                    break;

            }
        }
    }
    [System.Serializable]
    public class GuildSettingData
    {
        /*public List<AddedPlayerData> addedPlayerDatas;
        public Dictionary<int, List<List<float>>> ubExecTimeDataDic;
        public Dictionary<int, bool> useAutoModeDic = new Dictionary<int, bool>();// = new Dictionary<int, bool> { { 0, false }, { 0, false }, { 0, false }, { 0, false }, { 0, false } };
        public List<GuildRandomData> guildRandomDatas;
        public int currentGuildMonth;
        public int currentGuildEnemyNum;
        public int currentTurn;*/

        public List<GuildPlayerGroupData> guildPlayerGroupDatas;

        public Dictionary<int, UnitAttackPattern> changedEnemyAttackPatternDic = new Dictionary<int, UnitAttackPattern>();
        public Dictionary<int, EnemyData> changedEnemyDataDic;

        public int currentPlayerGroupNum;
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
        public float BodyColliderWidth = 100;
        //public bool usePlayerSettingHP;
        //public int playerSetingHP;
        public Dictionary<int, float> bossAppearDelayDic = new Dictionary<int, float>();
        public Dictionary<int, float> bossSkillCastTimeDic = new Dictionary<int, float>();
        public Dictionary<int, float> bossBodyWidthDic = new Dictionary<int, float>();
        public float BossAbnormalAddValue = 0.867f;
        public float BossAbnormalMultValue = 1;
        public bool usePhysics = true;
        public bool usePlayerSQL = true;
        public bool useSkillEffects = true;
        public float skillEffeckFix = 35;
        //public bool[] unlockGuilds = new bool[12] { false, false, false, true, true, true, true, false, false, false, false, false };

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
            return guildPlayerGroupDatas[currentPlayerGroupNum].playerData;
        }
        public GuildPlayerGroupData GetCurrentPlayerGroup()
        {
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
            switch (data.currentTurn)
            {
                case 1:
                    des += "A";
                    break;
                case 2:
                    des += "B";
                    break;
                case 3:
                    des += "C";
                    break;
                case 4:
                    des += "D";
                    break;
            }
            if (data.currentGuildEnemyNum == 4 && data.isViolent)
                des += "6";
            else
                des += (data.currentGuildEnemyNum+1);
            if (data.usePlayerSettingHP)
                des += "*";
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
    [System.Serializable]
    public class GuildPlayerGroupData
    {
        public AddedPlayerData playerData;
        public List<List<float>> UBExecTimeData;
        public bool useAutoMode;
        public GuildRandomData timeLineData;
        public int currentGuildMonth=9;
        public int currentGuildEnemyNum=1;
        public int currentTurn=1;
        public int selectedEnemyID = 0;
        public bool isViolent;
        public bool usePlayerSettingHP;
        public int playerSetingHP;
        public bool useLogBarrier;

        public bool isSpecialBoss;
        public int specialBossID;
        public int specialInputValue;
        public GuildPlayerGroupData()
        {
        }
        public void Init()
        {
            playerData = new AddedPlayerData();
            playerData.playerName = "新建队伍";
            UBExecTimeData = new List<List<float>> { new List<float>(), new List<float>(), new List<float>(), new List<float>(), new List<float>() };
            timeLineData = new GuildRandomData();
        }
    }
}