using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
#if ENABLE_THREADING
        [ThreadStatic]
#endif
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
        public List<Dropdown> dropdowns_ChooseTalentQuestBoss;
        public List<Dropdown> dropdowns_ChooseAbyssBoss;
        public List<Dropdown> dropdowns_ChooseSecretDungeonBoss;
        public List<Dropdown> dropdowns_ChooseMirageBoss;
        public List<Image> bossImage_ChooseBoss;
        public List<Toggle> toggles_ChooseBoss;
        public Toggle usePlayerSettingHP;
        public InputField currentHPinput;
        public List<Toggle> toggles_ChooseType;
        public Text show_text;
        public InputField specialInput;
        public List<string> show_str;


        public GuildBossDataEditdr BossDataEditdr;

        public List<Slider> SettingSliders;
        public List<Text> SettingTexts;
        public List<Toggle> SettingToggles;
        public List<Toggle> SemanSkillToggles;
        public List<Toggle> SemanUBToggles;
        public List<InputField> SettingInputs;

        public List<Text> calSettingTexts;
        public Slider calSlider;
        public GameObject bossDetailPrefab;
        public bool isLightStart = false;


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
        //private Dictionary<int, MasterEnemyMParts.EnemyMParts> enemyMPartsDic = new Dictionary<int, MasterEnemyMParts.EnemyMParts>();
        public GuildSettingData SettingData => StaticsettingData;
#if ENABLE_THREADING
        [ThreadStatic]
#endif
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
        private int[] selectedBossEnemyids => SettingData?.GetCurrentPlayerGroup()?.selectedEnemyIDs ?? new int[1]{0};
        public void setSelectedBossEnemyids(List<int> enemyids)
        {
            var group = SettingData.GetCurrentPlayerGroup();
            if (group != null)
            {
                group.selectedEnemyIDs = enemyids.ToArray();
            }
        }
        private bool isEditingUBTime;
        private static string[] guildMonthNames = new string[12] { "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "摩羯座", "水瓶座", "双鱼座" };
        private static string[] guildTurn = new string[5] { "一阶段", "二阶段", "三阶段", "四阶段", "五阶段", };
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
        public InputField RandomSeedTalent;
        public Toggle UseFixedRandomSeedTalent;
        public string[] TalentNames = { "火", "水", "风", "光", "暗" };
        public List<Sprite> TalentSprites;
        public List<Image> WeaknessSprites;
        public List<Text> TalentTexts;
        public List<GameObject> TalentBonus;
        public int WaveGroupId;
        public Dictionary<int, long> UnitTalentFormationBonusDictionary { get; set; } = new Dictionary<int, long>();
        private Color[] elementColors = {
            new Color(0xE7/255f, 0x55/255f, 0x4A/255f), // "#E7554A" 火
            new Color(0x44/255f, 0x9C/255f, 0xE9/255f), // "#449CE9" 水
            new Color(0x59/255f, 0xAF/255f, 0x41/255f), // "#59AF41" 风
            new Color(0xF1/255f, 0xB1/255f, 0x2F/255f), // "#F1B12F" 光
            new Color(0x7C/255f, 0x4D/255f, 0xBF/255f)  // "#7C4DBF" 暗
        };
        public List<Image> TalentQuestBossPictures;
        public Text TalentQuestBossText;
        public GameObject BossObject;
        public GameObject TalentBossObject;
        public List<Image> TalentQuestBattleEffectPictures;
        public GameObject ExtraEffectPrefab;
        public static Dictionary<int, EnemyData> EnemyDataDic
        {
            get
            {
                /*if (enemyDataDic == null)
                {
                    string enemyDataDicTxt = MainManager.Instance.LoadJsonDatas("Datas/EnemyDataDic");
                    enemyDataDic = JsonConvert.DeserializeObject<Dictionary<int, EnemyData>>(enemyDataDicTxt);
                }*/
                return MainManager.Instance.EnemyDataDic;
            }
        }
        public static Dictionary<int, EnemyData> TalentQuestEnemyParameterDic
        {
            get
            {
                return MainManager.Instance.TalentQuestEnemyParameterDic;
            }
        }
        public static Dictionary<int, WaveGroupDataBase> SecretDungeonWaveGroupDataDic
        {
            get
            {
                return MainManager.Instance.SecretDungeonWaveGroupDataDic;
            }
        }
        public static Dictionary<int, WaveGroupDataBase> TalentQuestWaveGroupDataDic
        {
            get
            {
                return MainManager.Instance.TalentQuestWaveGroupDataDic;
            }
        }
        public static Dictionary<int, EnemyData> AbyssEnemyParameterDic
        {
            get
            {
                return MainManager.Instance.AbyssEnemyParameterDic;
            }
        }
        public static Dictionary<int, WaveGroupDataBase> AbyssWaveGroupDataDic
        {
            get
            {
                return MainManager.Instance.AbyssWaveGroupDataDic;
            }
        }
        public static Dictionary<int, int> AbyssQuestFilterDic
        {
            get
            {
                return MainManager.Instance.AbyssQuestFilterDic;
            }
        }
        public static Dictionary<int, int> AbyssBossFilterDic
        {
            get
            {
                return MainManager.Instance.AbyssBossFilterDic;
            }
        }
        public static Dictionary<int, WaveGroupDataBase> MirageWaveGroupDataDic
        {
            get
            {
                return MainManager.Instance.MirageWaveGroupDataDic;
            }
        }
        public static Dictionary<int, EnemyData> MirageEnemyParameterDic
        {
            get
            {
                return MainManager.Instance.MirageEnemyParameterDic;
            }
        }
        public Dictionary<int, MasterEnemyMParts.EnemyMParts> EnemyMPartsDic
        {
            get => MainManager.Instance.EnemyMPartsDic;
        }
        public int BossType => selectedBossEnemyids[0] / 100000000;
        public bool IsGuildBoss { get => BossType == 4; }
        public bool IsDungeonBoss { get => BossType == 5; }
        public bool IsMirageBoss { get => BossType == 7; }
        public bool IsTalentQuestBoss { get => BossType == 8; }
        public bool IsAbyssBoss { get => BossType == 9; }
        public bool IsGuildBossId(int id)
        {
            return id / 100000000 == 4;
        }
        public bool IsTalentQuestBossId(int id)
        {
            return id / 100000000 == 8;
        }
        public bool IsAbyssBossId(int id)
        {
            return id / 100000000 == 9;
        }
        public bool IsDungeonBossId(int Id)
        {
            return Id / 100000000 == 5;
        }
        public bool IsMirageBossId(int Id)
        {
            return Id / 100000000 == 7;
        }
        public Dropdown dungeonDropdown;
        private Dictionary<int, int> dungeonEnemy = new Dictionary<int, int>()
        {
            { 0, 501010901 },
            { 1, 501010902 },
            { 2, 501010903 },
            { 3, 501010904 },
            { 4, 501010905 },
            { 5, 501011001 },
            { 6, 501011002 },
            { 7, 501011003 },
            { 8, 501011004 },
        };
        public void Awake()
        {
            Instance = this;

            //System.IO.File.WriteAllText("fieldnames.txt",
            //    string.Join("\n", typeof(UnitCtrl).GetProperties().Select(p => p.Name)));
            LoadAddedPlayerData();
        }
        public void Start()
        {
            if (!isLightStart)
            {
                StartCoroutine(StartAfterWait());
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

                if (RandomSeed != null && RandomSeedTalent != null)
                {
                    RandomSeed.onValueChanged.AddListener((value) => RandomSeedTalent.text = value);
                    RandomSeedTalent.onValueChanged.AddListener((value) => RandomSeed.text = value);
                }

                if (UseFixedRandomSeed != null && UseFixedRandomSeedTalent != null)
                {
                    UseFixedRandomSeed.onValueChanged.AddListener((value) => UseFixedRandomSeedTalent.isOn = value);
                    UseFixedRandomSeedTalent.onValueChanged.AddListener((value) => UseFixedRandomSeed.isOn = value);
                }
            }
            version.text = "—   v" + Application.version + "   —";
        }
        private void OnInputField2ValueChanged(string value)
        {
            SettingInputs[11].onValueChanged.RemoveListener(OnInputField1ValueChanged);
            SettingInputs[11].text = value;
            SettingInputs[11].onValueChanged.AddListener(OnInputField1ValueChanged);
        }
        private void OnInfoButtonClick(Text text)
        { 
            string textContent = text.text;
            if (!string.IsNullOrEmpty(SettingInputs[11].text))
            {
                textContent = "/" + textContent;
            }
            textContent = textContent.Replace("\n", "").Replace("\r", "");
#if UNITY_5_3_OR_NEWER
            GUIUtility.systemCopyBuffer = textContent;
#endif
            MainManager.Instance.WindowMessage("文本已复制并添加到末尾");
            SettingInputs[11].text += textContent.Replace("\n", "").Replace("\r", "");
        }
        private void OnInputField1ValueChanged(string value)
        {
            SettingInputs[12].onValueChanged.RemoveListener(OnInputField2ValueChanged);
            SettingInputs[12].text = value;
            SettingInputs[12].onValueChanged.AddListener(OnInputField2ValueChanged);
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
            BaseData baseData = MainManager.Instance.UnitRarityDic[unitData.unitId].GetBaseData(unitData);
            var baseDataEX = MainManager.Instance.UnitRarityDic[unitData.unitId].GetEXSkillValue(unitData);//,MyGameCtrl.Instance.tempData.isGuildBattle);
            characterDetailTexts[0].text = "" + baseData.RealHp + (baseDataEX.RealHp == 0 ? string.Empty : $"<color=#FF80C0>+{baseDataEX.RealHp}</color>");
            long accuracy = Mathf.RoundToInt(EnemyDataDic.ContainsKey(selectedBossEnemyids[0]) ? EnemyDataDic[selectedBossEnemyids[0]].baseData.Accuracy : 0);//boss命中
            long dodge = baseData.dataint[7] / 100;//角色闪避
            double dodgerate = dodge <= accuracy ? 0 : 1 / (1 + 100.0 / (dodge - accuracy));//闪避率
            for (int i = 1; i < baseData.dataint.Length; i++)
            {
                if (i == 7)
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
                    SettingData.currentPlayerGroupNum = idx + currentPage * 5 - 5;
                    AutoModeToggle.isOn = SettingData.GetCurrentPlayerGroup().useAutoMode;
                    SetModeToggle.isOn = SettingData.GetCurrentPlayerGroup().useSetMode;
                }
                idx++;
            }
            Refresh();
            SaveDataToJson();
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
            foreach (var key in guildEnemyDatas.Keys)
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
            dropdowns_ChooseBoss[2].value = clanBattleID - 1024;
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
                if (toggles_ChooseType[0].isOn)
                {
                    enemyid_0 = guildEnemyDatas[GetClanBattleID()].enemyIds[0][dropdowns_ChooseBoss[0].value];
                    //Path = "GuildEnemy/icon_unit_" + enemyDataDic[enemyid_0].unit_id;
                    //bossImage_ChooseBoss.sprite = MainManager.LoadSourceSprite(Path);
                    for (int i = 0; i < 5; i++)
                    {
                        bossImage_ChooseBoss[0].sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, EnemyDataDic[enemyid_0].unit_id);
                        bossImage_ChooseBoss[i].SetActive(i == 0);
                    }
                }
                else
                {
                    // specialInput.text = "401042401";
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
        public void OnChooseTalentQuestDropDownChanged()
        {
            int firstOption = dropdowns_ChooseTalentQuestBoss[0].value + 1;
            int secondOption = dropdowns_ChooseTalentQuestBoss[1].value + 1;
            int thirdOption = dropdowns_ChooseTalentQuestBoss[2].value + 1;

            // 构造wave_group_id
            // 格式: 8 + firstOption(第2位) + 001(第3-5位固定) + secondOption-1*10 + thirdOption(第5-7位) + 1(最后1位)
            string waveGroupIdStr = $"8{firstOption}001{(secondOption - 1) * 10 + thirdOption:D3}1";

            if (int.TryParse(waveGroupIdStr, out int waveGroupId))
            {
                WaveGroupId = waveGroupId;
                var matchingData = TalentQuestWaveGroupDataDic?.Values
                    .FirstOrDefault(data => data.wave_group_id == waveGroupId);
                specialEnemyid = matchingData.enemy_ids.ToArray();
                SetBossImages(specialEnemyid, TalentQuestEnemyParameterDic);
            }
            else
            {
                Debug.LogError($"无法解析wave_group_id字符串: {waveGroupIdStr}");
            }
        }
        public void UpdateTalentQuestDropdownOptions()
        {
            Dropdown secondDropdown = dropdowns_ChooseTalentQuestBoss[1];
            secondDropdown.ClearOptions();

            List<string> secondDropdownOptions = new List<string>();

            if (TalentQuestWaveGroupDataDic != null)
            {
                int optionCount = Mathf.CeilToInt(TalentQuestWaveGroupDataDic.Count / 50.0f);

                for (int i = 1; i <= optionCount; i++)
                {
                    secondDropdownOptions.Add(i.ToString());
                }
            }

            secondDropdown.AddOptions(secondDropdownOptions);
        }
        // 深渊讨伐战期数选项
        public void UpdateAbyssFirstDropdownOptions()
        {
            Dropdown firstDropdown = dropdowns_ChooseAbyssBoss[0];
            firstDropdown.ClearOptions();

            List<string> firstDropdownOptions = new List<string>();

            if (AbyssWaveGroupDataDic != null)
            {
                int optionCount = Mathf.CeilToInt(AbyssWaveGroupDataDic.Count / 15.0f);

                for (int i = 1; i <= optionCount; i++)
                {
                    firstDropdownOptions.Add($"第{i}期");
                }
            }

            firstDropdown.AddOptions(firstDropdownOptions);
        }

        public void UpdateAbyssThirdDropdownOptions()
        {
            Dropdown thirdDropdown = dropdowns_ChooseAbyssBoss[2];
            thirdDropdown.ClearOptions();

            List<string> thirdDropdownOptions = new List<string>();

            if (dropdowns_ChooseAbyssBoss[1].value == 0)
            {
                thirdDropdownOptions.Add("N");
                thirdDropdownOptions.Add("H");
                thirdDropdownOptions.Add("VH");
            }
            else
            {
                thirdDropdownOptions.Add("N");
                thirdDropdownOptions.Add("H");
                thirdDropdownOptions.Add("V");
                thirdDropdownOptions.Add("EX");
            }

            thirdDropdown.AddOptions(thirdDropdownOptions);
        }
        public void OnChooseAbyssDropDownChanged()
        {
            int firstOption = dropdowns_ChooseAbyssBoss[0].value;
            int secondOption = dropdowns_ChooseAbyssBoss[1].value;
            int thirdOption = dropdowns_ChooseAbyssBoss[2].value + 1;

            Dictionary<int, int> sourceDic;
            if (secondOption == 0)
            {
                // 当 firstOption 为 0 时，使用 AbyssQuestFilterDic
                sourceDic = AbyssQuestFilterDic;
            }
            else
            {
                // 当 firstOption 大于 0 时，使用 AbyssBossFilterDic
                sourceDic = AbyssBossFilterDic;
            }
            var sortedAbyssIds = sourceDic.Keys
                .OrderBy(x => x)
                .ToArray();

            int selectedAbyssId = sortedAbyssIds[firstOption];
            int abyssWaveGroupIdPrefix = sourceDic[selectedAbyssId];

            // 构造wave_group_id
            // 格式: abyssWaveGroupIdPrefix(第1-5位) + secondOption(第6位) + thirdOption(第7位) + 00(最后2位)
            int waveGroupId = abyssWaveGroupIdPrefix + secondOption * 1000 + thirdOption * 100;

            WaveGroupId = waveGroupId;
            var matchingData = AbyssWaveGroupDataDic?.Values
                .FirstOrDefault(data => data.wave_group_id == waveGroupId);
            if (matchingData != null)
            {
                specialEnemyid = matchingData.enemy_ids.ToArray();
                SetBossImages(specialEnemyid, AbyssEnemyParameterDic);
            }
            else
            {
                Debug.LogError($"未找到匹配的wave group数据: {waveGroupId}");
            }
        }
        public void UpdateSecretDungeonFirstDropdownOptions()
        {
            Dropdown firstDropdown = dropdowns_ChooseSecretDungeonBoss[0];
            firstDropdown.ClearOptions();

            List<string> firstDropdownOptions = new List<string>();

            if (SecretDungeonWaveGroupDataDic != null)
            {
                int optionCount = Mathf.CeilToInt(SecretDungeonWaveGroupDataDic.Count / 25.0f);

                for (int i = 1; i <= optionCount; i++)
                {
                    firstDropdownOptions.Add($"第{i}期");
                }
            }

            firstDropdown.AddOptions(firstDropdownOptions);
        }
        public void OnChooseSecretDungeonDropDownChanged()
        {
            int firstOption = dropdowns_ChooseSecretDungeonBoss[0].value + 1;
            int secondOption = dropdowns_ChooseSecretDungeonBoss[1].value + 1;
            int thirdOption = dropdowns_ChooseSecretDungeonBoss[2].value + 3;

            // 构造wave_group_id
            // 格式: 5020 + firstOption(第5-6位) + secondOption(第7位) + thirdOption(第8位) + 1(最后1位)
            string waveGroupIdStr = $"5020{firstOption:D2}{secondOption}{thirdOption}1";

            if (int.TryParse(waveGroupIdStr, out int waveGroupId))
            {
                WaveGroupId = waveGroupId;
                var matchingData = SecretDungeonWaveGroupDataDic?.Values
                    .FirstOrDefault(data => data.wave_group_id == waveGroupId);
                specialEnemyid = matchingData.enemy_ids.ToArray();
                SetBossImages(specialEnemyid, EnemyDataDic);
            }
            else
            {
                Debug.LogError($"无法解析wave_group_id字符串: {waveGroupIdStr}");
            }
        }
        public void UpdateMirageAreaDropdownOptions()
        {
            Dropdown dropdown = dropdowns_ChooseMirageBoss[0];
            if (dropdown == null) return;
            dropdown.ClearOptions();

            List<string> options = new()
            {
                "追忆战日常"
            };

            if (MainManager.Instance.MirageNemesisAreaDic != null)
            {
                foreach (var kvp in MainManager.Instance.MirageNemesisAreaDic.OrderBy(x => x.Key))
                {
                    options.Add(kvp.Value.nemesis_area_name);
                }
            }

            dropdown.AddOptions(options);
        }
        public void UpdateMirageDropdownOptions()
        {
            Dropdown floorDropdown = dropdowns_ChooseMirageBoss[1];
            floorDropdown.ClearOptions();

            List<string> floorDropdownOptions = new List<string>();

            if (MirageWaveGroupDataDic != null)
            {
                int selectedFirstOption = dropdowns_ChooseMirageBoss[0].value;
                int targetPrefix;

                // 根据dropdowns_ChooseMirageBoss[0]的值确定目标前缀
                if (selectedFirstOption == 0)
                {
                    targetPrefix = 7500;
                }
                else
                {
                    targetPrefix = 7600 + selectedFirstOption;
                }

                // 筛选符合条件的wave_group_id
                var optionCount = MirageWaveGroupDataDic.Values
                    .Where(data => data.wave_group_id / 100000 == targetPrefix)
                    .ToList().Count;

                for (int i = 1; i <= optionCount; i++)
                {
                    floorDropdownOptions.Add($"第{i}层");
                }
            }

            floorDropdown.AddOptions(floorDropdownOptions);
        }
        public void OnChooseMirageDropDownChanged()
        {
            int firstOption = dropdowns_ChooseMirageBoss[0].value;
            int secondOption = dropdowns_ChooseMirageBoss[1].value + 1;
            string waveGroupIdStr = "";
            if (firstOption == 0)
            {
                waveGroupIdStr += $"75";
            }
            else
            {
                waveGroupIdStr += $"76";
            }
            waveGroupIdStr += $"{firstOption:D2}00{secondOption:D2}1";

            if (int.TryParse(waveGroupIdStr, out int waveGroupId))
            {
                WaveGroupId = waveGroupId;
                var matchingData = MirageWaveGroupDataDic?.Values
                    .FirstOrDefault(data => data.wave_group_id == waveGroupId);
                specialEnemyid = matchingData.enemy_ids.ToArray();
                SetBossImages(specialEnemyid, MirageEnemyParameterDic);
            }
            else
            {
                Debug.LogError($"无法解析wave_group_id字符串: {waveGroupIdStr}");
            }
        }
        public void UpdateDropdownOptions()
        {
            Dropdown dp = dropdowns_ChooseBoss[1];

            // 获取 guildTurn 中的前 turnCount 个值
            int turnCount = guildEnemyDatas[GetClanBattleID()].enemyIds.Length - 1;
            if (turnCount == dp.options.Count)
            {
                return;
            }
            // 清空当前选项
            dp.ClearOptions();
            List<string> selectedOptions = new List<string>();
            for (int i = 0; i < turnCount; i++)
            {
                selectedOptions.Add(guildTurn[i]);
            }

            // 创建新的选项列表
            List<Dropdown.OptionData> optionDataList = new List<Dropdown.OptionData>();
            foreach (string option in selectedOptions)
            {
                optionDataList.Add(new Dropdown.OptionData(option));
            }

            // 更新 Dropdown 的选项
            dp.AddOptions(optionDataList);
        }
        public void OnDungeonDropdownValueChanged()
        {
            if (!toggles_ChooseType[0].isOn && dungeonEnemy.ContainsKey(dungeonDropdown.value))
            {
                int enemyId = dungeonEnemy[dungeonDropdown.value];

                if (EnemyDataDic.TryGetValue(enemyId, out EnemyData data))
                {
                    specialInput.text = enemyId.ToString();
                    OnInputFinished();
                }
                else
                {
                    specialInput.text = "";
                }
            }
        }
        public void OnInputFinished()
        {
            if (!toggles_ChooseType[0].isOn)
            {
                var bossid = specialInput.text.Split(',').Select(x => int.Parse(x, NumberStyles.Any)).ToArray();
                EnemyData data = null;
                if (bossid.Reverse().All(x => EnemyDataDic.TryGetValue(x, out data)))
                {
                    //string Path = "GuildEnemy/icon_unit_" + data.unit_id;
                    //bossImage_ChooseBoss.sprite = MainManager.LoadSourceSprite(Path);
                    // bossImage_ChooseBoss.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, data.unit_id);
                    specialEnemyid = bossid;
                    SetBossImages(specialEnemyid, EnemyDataDic);
                }
                else
                {
                    MainManager.Instance.WindowConfigMessage("bossID无效！", null);
                    specialEnemyid = new[] { 0 };
                }
                specialInputValue = 0;
            }
        }
        public void FinishChooseBossButton()
        {
            int[] enemyId = new int[] {0};
            if (toggles_ChooseType[0].isOn)
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
                turn = clanBattleID >= 1067 && turn == 1 ? turn - 1 : turn;
                enemyId = new[] { GetGuildBossID(clanBattleID, num, turn) };
            }
            else
            {
                if (toggles_ChooseType[1].isOn)
                {
                    OnChooseTalentQuestDropDownChanged();
                }
                else if (toggles_ChooseType[2].isOn)
                {
                    OnChooseAbyssDropDownChanged();
                }
                else if (toggles_ChooseType[4].isOn)
                {
                    OnChooseMirageDropDownChanged();
                }
                enemyId = specialEnemyid;
            }
            //selectedBossEnemyid = SettingData.currentTurn == 1 ? guildEnemyData.enemyIds_1[SettingData.currentGuildEnemyNum] : guildEnemyData.enemyIds_2[SettingData.currentGuildEnemyNum];

            var group = SettingData.GetCurrentPlayerGroup();
            group.selectedEnemyIDs = toggles_ChooseType[0].isOn ? enemyId : specialEnemyid;
            group.isSpecialBoss = false;//!isGuildBoss;
            group.talentQuestMap = dropdowns_ChooseTalentQuestBoss[0].value;
            group.talentQuestArea = dropdowns_ChooseTalentQuestBoss[1].value;
            group.talentQuestIndex = dropdowns_ChooseTalentQuestBoss[2].value;
            group.abyssMonth = dropdowns_ChooseAbyssBoss[0].value;
            group.abyssEnemyNum = dropdowns_ChooseAbyssBoss[1].value;
            group.abyssTurn = dropdowns_ChooseAbyssBoss[2].value;
            group.dungeonMonth = dropdowns_ChooseSecretDungeonBoss[0].value;
            group.dungeonDay = dropdowns_ChooseSecretDungeonBoss[1].value;
            group.dungeonFloor = dropdowns_ChooseSecretDungeonBoss[2].value;
            group.mirageArea = dropdowns_ChooseMirageBoss[0].value;
            group.mirageFloor = dropdowns_ChooseMirageBoss[1].value;
            //group.specialBossID = specialEnemyid;
            //group.specialInputValue = specialInputValue;
            group.useLogBarrierNew = GuildPlayerGroupData.LogBarrierType.FullBarrier; // (GuildPlayerGroupData.LogBarrierType)dropdowns_ChooseBoss[4].value;
            group.waveGroupId = WaveGroupId;
            SaveDataToJson();
            Refresh();
        }
        private void SetBossImages(int[] enemyIds, Dictionary<int, EnemyData> enemyDataDic)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < enemyIds.Length)
                {
                    var enemyData = enemyDataDic[enemyIds[i]];
                    int prefabId = enemyData.detailData.prefab_id;
                    if (enemyData.detailData.visual_change_flag == 1)
                    {
                        prefabId = enemyData.rarity > 5 ? prefabId + 60 : prefabId + 30;
                    }
                    bossImage_ChooseBoss[i].sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, prefabId);
                    bossImage_ChooseBoss[i].SetActive(i <= enemyIds.Length);
                }
                else
                {
                    bossImage_ChooseBoss[i].SetActive(false);
                }
            }
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

        public GuildBattleData GetBattleData(bool skipping)
        {
            SaveDataToJson();
            GuildBattleData battleData = new GuildBattleData();
            battleData.skipping = skipping;
            battleData.players = SettingData.GetCurrentPlayerData();
            battleData.enemyData = new List<EnemyData>();
            foreach (var selectedBossEnemyid in selectedBossEnemyids)
            {
                EnemyData enemyData = GetEnemyDataByID(selectedBossEnemyid);
                if (EnemyMPartsDic.TryGetValue(enemyData.enemy_id, out var value))
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
            return battleData;
        }
        private void StartCalButton(bool skipping)
        {
            /*if (!CheckDataIsReady(SettingData..currentGuildMonth))
            {
                return;
            }*/
            MainManager.Instance.ChangeSceneToBalttle(GetBattleData(skipping));
        }
        public EnemyData GetEnemyDataByID(int enemy_id, int boss_id = -1)
        {
            bool isTalentQuestBoss = boss_id == -1 ? IsTalentQuestBoss : IsTalentQuestBossId(boss_id);
            bool isAbyssBoss = boss_id == -1 ? IsAbyssBoss : IsAbyssBossId(boss_id);
            bool isMirageBoss = boss_id == -1 ? IsMirageBoss : IsMirageBossId(boss_id);

            return SettingData.changedEnemyDataDic.TryGetValue(enemy_id, out EnemyData data) ? data :
                   isTalentQuestBoss ? TalentQuestEnemyParameterDic[enemy_id] :
                   isAbyssBoss ? AbyssEnemyParameterDic[enemy_id] :
                   isMirageBoss ? MirageEnemyParameterDic[enemy_id] :
                   EnemyDataDic[enemy_id];
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
            if (MirageWaveGroupDataDic == null || MirageWaveGroupDataDic.Count == 0)
            {
                toggles_ChooseType[4].SetActive(false);
            }
            UpdateMirageAreaDropdownOptions();
            SetChooseBossDropDown(data.currentGuildMonth);
            //dropdowns_ChooseBoss[2].value = data.currentGuildMonth >= 1000 ? data.currentGuildMonth - 1000 : (data.currentGuildMonth + 1) % 12;
            dropdowns_ChooseBoss[0].value = data.currentGuildEnemyNum;
            dropdowns_ChooseBoss[1].value = data.currentTurn - 1;
            dropdowns_ChooseBoss[4].value = (int)data.useLogBarrierNew;
            dropdowns_ChooseTalentQuestBoss[0].value = data.talentQuestMap;
            dropdowns_ChooseTalentQuestBoss[1].value = data.talentQuestArea;
            dropdowns_ChooseTalentQuestBoss[2].value = data.talentQuestIndex;
            dropdowns_ChooseAbyssBoss[0].value = data.abyssMonth;
            dropdowns_ChooseAbyssBoss[1].value = data.abyssEnemyNum;
            dropdowns_ChooseAbyssBoss[2].value = data.abyssTurn;
            dropdowns_ChooseSecretDungeonBoss[0].value = data.dungeonMonth;
            dropdowns_ChooseSecretDungeonBoss[1].value = data.dungeonDay;
            dropdowns_ChooseSecretDungeonBoss[2].value = data.dungeonFloor;
            dropdowns_ChooseMirageBoss[0].value = data.mirageArea;
            dropdowns_ChooseMirageBoss[1].value = data.mirageFloor;
            //toggles_ChooseBoss.isOn = SettingData.isViolent;
            if (data.isViolent)
                toggles_ChooseBoss[0].isOn = true;
            else
                toggles_ChooseBoss[1].isOn = true;
            switch (BossType)
            {
                case 8:
                    toggles_ChooseType[1].isOn = true;
                    break;
                case 9:
                    toggles_ChooseType[2].isOn = true;
                    break;
                case 5:
                    toggles_ChooseType[3].isOn = true;
                    break;
                case 7:
                    toggles_ChooseType[4].isOn = true;
                    break;
            }
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
        private void SetBattleEffectPictures()
        {
            // 计算quest_id值
            int questId = WaveGroupId / 10;

            // 从数据表中找到匹配quest_id的数据
            List<object> battleEffects = new List<object>();
            if (IsTalentQuestBoss)
            {
                battleEffects = MainManager.Instance.TalentQuestBattleEffectDic
                    .Values
                    .Where(x => x.quest_id == questId)
                    .OrderBy(x => x.id)
                    .Cast<object>()
                    .ToList();
            }
            else if (IsAbyssBoss)
            {
                battleEffects = MainManager.Instance.AbyssBattleEffectDic
                    .Values
                    .Where(x => x.quest_id == questId)
                    .OrderBy(x => x.id)
                    .Cast<object>()
                    .ToList();
            }
            else if (IsMirageBoss)
            {
                battleEffects = MainManager.Instance.MirageBattleEffectDic
                    .Values
                    .Where(x => x.quest_id == questId)
                    .OrderBy(x => x.id)
                    .Cast<object>()
                    .ToList();
            }

            // 加载精灵图集
            var sprites = Resources.LoadAll<Sprite>("AtlasTalentQuest")
                .ToDictionary(x => x.name, x => x);

            // 按顺序设置TalentQuestBattleEffectPictures的图片
            for (int i = 0; i < TalentQuestBattleEffectPictures.Count; i++)
            {
                if (i < battleEffects.Count)
                {
                    var effectData = battleEffects[i];
                    var iconProperty = effectData.GetType().GetProperty("icon_name");
                    string iconName = (string)iconProperty.GetValue(effectData);

                    if (!string.IsNullOrEmpty(iconName) && sprites.ContainsKey(iconName))
                    {
                        TalentQuestBattleEffectPictures[i].sprite = sprites[iconName];
                        TalentQuestBattleEffectPictures[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        TalentQuestBattleEffectPictures[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    TalentQuestBattleEffectPictures[i].gameObject.SetActive(false);
                }
            }
        }
        public void UpdateTooglesChooseType()
        {
            switch (BossType)
            {
                case 8:
                    toggles_ChooseType[1].isOn = true;
                    OnChooseTalentQuestDropDownChanged();
                    break;
                case 9:
                    toggles_ChooseType[2].isOn = true;
                    OnChooseAbyssDropDownChanged();
                    break;
                case 5:
                    toggles_ChooseType[3].isOn = true;
                    OnChooseSecretDungeonDropDownChanged();
                    break;
                case 7:
                    toggles_ChooseType[4].isOn = true;
                    OnChooseMirageDropDownChanged();
                    break;
                default:
                    toggles_ChooseType[0].isOn = true;
                    OnChooseBossDropDownChanged();
                    break;
            }
        }
        private void Refresh()
        {
            var data = SettingData.GetCurrentPlayerGroup();
            AddedPlayerData playerData = data.playerData;
            /*if (SettingData.addedPlayerDatas != null && SettingData.addedPlayerDatas.Count > SettingData.currentPlayerGroupNum)
            {
                playerData = SettingData.addedPlayerDatas[SettingData.currentPlayerGroupNum];
            }*/
            UpdateTooglesChooseType();
            bool isMuiltBoss = IsTalentQuestBoss || IsAbyssBoss || IsDungeonBoss || IsMirageBoss;
            BossObject.SetActive(!isMuiltBoss);
            TalentBossObject.SetActive(isMuiltBoss);

            MainManager.Instance.TeamEnhanceData.LoadFromPlayerSetting();
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
                    TalentTexts[i].text = playerData.playrCharacters[i].TalentLevelDescribe();
                }
                else
                {
                    charactersUI[i].SetButton(0);
                    UnitUBTimes[i].SetUBTimes(new List<float>());
                    UnitUBTimes[i].SetSemanUBTimes(new List<int>());
                    TalentTexts[i].text = "无";
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
            if (IsTalentQuestBoss)
            {
                dropdowns_ChooseTalentQuestBoss[0].value = data.talentQuestMap;
                dropdowns_ChooseTalentQuestBoss[1].value = data.talentQuestArea;
                dropdowns_ChooseTalentQuestBoss[2].value = data.talentQuestIndex;
            }
            else if (IsAbyssBoss)
            {
                dropdowns_ChooseAbyssBoss[0].value = data.abyssMonth;
                dropdowns_ChooseAbyssBoss[1].value = data.abyssEnemyNum;
                dropdowns_ChooseAbyssBoss[2].value = data.abyssTurn;
            }
            else if (IsDungeonBoss)
            {
                dropdowns_ChooseSecretDungeonBoss[0].value = data.dungeonMonth;
                dropdowns_ChooseSecretDungeonBoss[1].value = data.dungeonDay;
                dropdowns_ChooseSecretDungeonBoss[2].value = data.dungeonFloor;
            }
            else if (IsMirageBoss)
            {
                dropdowns_ChooseMirageBoss[0].value = data.mirageArea;
                dropdowns_ChooseMirageBoss[1].value = data.mirageFloor;
            }
            if (IsTalentQuestBoss || IsAbyssBoss || IsDungeonBoss || IsMirageBoss)
            {
                data.waveGroupId = data.waveGroupId == 0 ? WaveGroupId : data.waveGroupId;
            }
            TalentTexts[5].text = "属性技能：" + GetFinalTalentSkillNodeDescription();
            TalentTexts[6].text = "大师技能：" + GetFinalTeamSkillNodeDescription();
            if (data.currentGuildMonth < 1000) data.currentGuildMonth += 1024;
            if (data.currentGuildMonth < 1024) data.currentGuildMonth += 24;

            if (selectedBossEnemyids == null || selectedBossEnemyids.Length == 0 || selectedBossEnemyids[0] == 0)
                data.selectedEnemyIDs = new[]
                    {GetGuildBossID(data.currentGuildMonth, data.currentGuildEnemyNum, data.currentTurn)};
            //string Path = "GuildEnemy/icon_unit_" + enemyDataDic[selectedBossEnemyid].unit_id;
            //bossPicture.sprite = MainManager.LoadSourceSprite(Path);
            if (isMuiltBoss)
            {
                for (int i = 0; i < selectedBossEnemyids.Length && i < TalentQuestBossPictures.Count; i++)
                {
                    if (selectedBossEnemyids[i] > 0)
                    {
                        int prefab_id = 0;
                        EnemyData enemyParam = null;
                        if (IsTalentQuestBoss && TalentQuestEnemyParameterDic != null && TalentQuestEnemyParameterDic.ContainsKey(selectedBossEnemyids[i]))
                        {
                            enemyParam = TalentQuestEnemyParameterDic[selectedBossEnemyids[i]];
                        }
                        else if (IsAbyssBoss && AbyssEnemyParameterDic != null && AbyssEnemyParameterDic.ContainsKey(selectedBossEnemyids[i]))
                        {
                            enemyParam = AbyssEnemyParameterDic[selectedBossEnemyids[i]];
                        }
                        else if (IsDungeonBoss && EnemyDataDic != null && EnemyDataDic.ContainsKey(selectedBossEnemyids[i]))
                        {
                            enemyParam = EnemyDataDic[selectedBossEnemyids[i]];
                        }
                        else if (IsMirageBoss && MirageEnemyParameterDic != null && MirageEnemyParameterDic.ContainsKey(selectedBossEnemyids[i]))
                        {
                            enemyParam = MirageEnemyParameterDic[selectedBossEnemyids[i]];
                        }
                        if (enemyParam != null)
                        {
                            prefab_id = enemyParam.detailData.prefab_id;
                            if (enemyParam.detailData.visual_change_flag == 1)
                            {
                                var rarity = enemyParam.rarity;
                                prefab_id = rarity > 5 ? prefab_id + 60 : prefab_id + 30;
                            }
                        }
                        TalentQuestBossPictures[i].sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, prefab_id);
                        TalentQuestBossPictures[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        TalentQuestBossPictures[i].gameObject.SetActive(false);
                    }
                }
                for (int i = selectedBossEnemyids.Length; i < TalentQuestBossPictures.Count; i++)
                {
                    TalentQuestBossPictures[i].gameObject.SetActive(false);
                }
                SetBattleEffectPictures();
            }
            else
            {
                bossPicture.sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, EnemyDataDic[selectedBossEnemyids[0]].unit_id);
            }
            bossDetailTexts[0].text = data.isSpecialBoss ? "自定义" :
                                     (IsTalentQuestBoss ? dropdowns_ChooseTalentQuestBoss[0].options[dropdowns_ChooseTalentQuestBoss[0].value].text :
                                     (IsAbyssBoss ? dropdowns_ChooseAbyssBoss[0].options[dropdowns_ChooseAbyssBoss[0].value].text :
                                     (IsMirageBoss ? dropdowns_ChooseMirageBoss[0].options[dropdowns_ChooseMirageBoss[0].value].text :
                                     EnemyDataDic[selectedBossEnemyids[0]].name)));
            if (data.isSpecialBoss)
            {
                bossDetailTexts[1].text = "--";
            }
            else if (isMuiltBoss)
            {
                bossDetailTexts[1].text = "";
            }
            else
            {
                switch (data.currentTurn)
                {
                    case 1:
                        bossDetailTexts[1].text = "A";
                        break;
                    case 2:
                        bossDetailTexts[1].text = "B";
                        break;
                    case 3:
                        bossDetailTexts[1].text = "C";
                        break;
                    case 4:
                        bossDetailTexts[1].text = "D";
                        break;
                    case 5:
                        bossDetailTexts[1].text = "E";
                        break;
                }
                bossDetailTexts[1].text += $"{data.currentGuildEnemyNum + 1}";
                bossDetailTexts[2].text = $"{data.currentGuildMonth}期";
                bossDetailTexts[3].text = $"{guildMonthNames[(data.currentGuildMonth - 1000 + 11) % 12]}";
            }
            if (data.currentGuildEnemyNum == 4 && toggles_ChooseBoss[0].isOn)
            {
                data.isViolent = true;
            }
            else
            {
                data.isViolent = false;
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
            if (IsTalentQuestBoss)
            {
                TalentQuestBossText.text = $"{dropdowns_ChooseTalentQuestBoss[0].options[dropdowns_ChooseTalentQuestBoss[0].value].text} {dropdowns_ChooseTalentQuestBoss[1].value + 1}-{dropdowns_ChooseTalentQuestBoss[2].value + 1}";
            }
            else if (IsAbyssBoss)
            {
                TalentQuestBossText.text = $"{dropdowns_ChooseAbyssBoss[0].options[dropdowns_ChooseAbyssBoss[0].value].text} {dropdowns_ChooseAbyssBoss[2].options[dropdowns_ChooseAbyssBoss[2].value].text}{dropdowns_ChooseAbyssBoss[1].value}";
            }
            else if (selectedBossEnemyids[0] / 100000 == 5020)
            {
                TalentQuestBossText.text = $"特别地下城 第{data.dungeonMonth + 1}期 D{data.dungeonDay + 1}-{data.dungeonFloor + 8}";
            }
            else if (IsMirageBoss)
            {
                TalentQuestBossText.text = $"{dropdowns_ChooseMirageBoss[0].options[dropdowns_ChooseMirageBoss[0].value].text} {dropdowns_ChooseMirageBoss[1].options[dropdowns_ChooseMirageBoss[1].value].text}";
            }
            RandomSeed.text = "" + SettingData.GetCurrentRandomData().RandomSeed;
            UseFixedRandomSeed.isOn = SettingData.GetCurrentRandomData().UseFixedRandomSeed;
            usePlayerSettingHP.isOn = SettingData.GetCurrentPlayerGroup().usePlayerSettingHP;
            UpdateWeaknessSprites();
            if (selectedBossEnemyids[0] > 0)
            {
                GetTalentFormationBonusDescription();
            }
        }
        private void RefreshCalcSettings_start()
        {
            SettingSliders[0].value = SettingData.FPSforLogic / 10;
            SettingTexts[0].text = SettingData.FPSforLogic + "";
            SettingSliders[1].value = SettingData.FPSforAnimation / 10;
            SettingTexts[1].text = SettingData.FPSforAnimation + "";
            SettingSliders[2].value = SettingData.UBTryingCount / 10 > 18 ? 19 : SettingData.UBTryingCount / 10;
            SettingTexts[2].text = SettingData.UBTryingCount + "";
            SettingToggles[0].isOn = SettingData.calcSpineAnimation;
            SettingToggles[0].interactable = true; //SettingData.calSpeed == 1;
            SemanSkillToggles[SettingData.SemanSkillOn].isOn = true;
            SemanUBToggles[SettingData.SemanUBOn].isOn = true;
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
            SettingTexts[2].text = (int)SettingSliders[2].value > 18 ? "9999" : (int)SettingSliders[2].value * 10 + "";
            SettingToggles[0].interactable = SettingData.calSpeed == 1;
            //SettingToggles[7].isOn = SettingData.usePhysics;
        }
        public void SaveCalcSettings()
        {
            SettingData.FPSforLogic = (int)SettingSliders[0].value * 10;
            SettingData.FPSforAnimation = (int)SettingSliders[1].value * 10;
            SettingData.calcSpineAnimation = SettingToggles[0].isOn;
            SettingData.UBTryingCount = (int)SettingSliders[2].value > 18 ? 9999 : (int)SettingSliders[2].value * 10;
            SettingData.SemanSkillOn = SemanSkillToggles.FindIndex(t => t.isOn);
            SettingData.SemanUBOn = SemanUBToggles.FindIndex(t => t.isOn);
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
        public static void LoadAddedPlayerData()
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
        public void GuildBossDataEditButton(int index)
        {
            BossDataEditdr.SetData(selectedBossEnemyids[index]);
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
            if (guildTimelineData.UBExecTime.Count > 6)
            {
                guildTimelineData.UBExecTime.RemoveRange(5, guildTimelineData.UBExecTime.Count - 5);
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
            MainManager.Instance.IsGuildBattle = false;
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

        public AutoCalculate autoCalculate;

        public void OpenAutoCalculatePage()
        {
            GameObject a = Instantiate(autoCalculatePagePrefab);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            autoCalculate = a.GetComponent<AutoCalculate>();
            autoCalculate.Init(MainManager.Instance.AutoCalculatorData);
        }

        public void StartAutoCalculate()
        {
            // StartCoroutine(AutoCalculate());
            StartCalButton(true);
        }

        public static RemoteSimulator simulator;

        private IEnumerator WaitRemoteCalculation()
        {
            const int EXEC_ONCE = 10;
            while (!MainManager.Instance.AutoCalculatorData.isFinish)
            {
                var request = simulator.Simulate(EXEC_ONCE);

                while (!request.IsCompleted)
                    yield return null;

                foreach (var item in request.Result)
                {
                    item.OnceResult.id = MainManager.Instance.AutoCalculatorData.resultDatas.Count;
                    MainManager.Instance.AutoCalculatorData.resultDatas.Add(
                        item.OnceResult);
                }
                autoCalculate.Init(MainManager.Instance.AutoCalculatorData);
            }
            MainManager.Instance.AutoCalculatorData.isCalculating = false;
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
            if (MainManager.Instance.AutoCalculatorData.useRemote)
            {
                if (simulator != null) simulator.Dispose();
                simulator = new RemoteSimulator();
                simulator.CreateRequest();
                this.StartCoroutine(WaitRemoteCalculation());
            }
            else
            {
                StartAutoCalculate();
            }
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
        public void BossImageButton(int index = 0)
        {
            string detailstr = "    请选择BOSS!";
            EnemyData enemyData = GetEnemyDataByID(selectedBossEnemyids[index]);
            if (enemyData != null)
            {
                detailstr = $"  {enemyData.detailData.unit_name}({selectedBossEnemyids[index]})/({enemyData.detailData.unit_id})\n";
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
                Action<int, int, string> action = (skID, skLV, str) =>
                 {
                     detailstr += $" {str}:\n {skID}({skLV})\n";
                     if (MainManager.Instance.SkillDataDic.TryGetValue(skID, out SkillData sk))
                     {
                         detailstr += $" {sk.GetSkillDetailsEnemy(skLV, Math.Max(enemyData.baseData.Atk, enemyData.baseData.Magic_str))}\n";
                     }
                 };
                if (enemyData.detailData.visual_change_flag == 1)
                {
                    var un = MainManager.Instance.UnitRarityDic[enemyData.detailData.prefab_id];
                    int rarity = enemyData.rarity;
                    int UnitEquip = enemyData.unique_equipment_flag_1;
                    action(rarity == 6 ? enemyData.skillData.raw_data.union_burst_evolution : enemyData.skillData.raw_data.union_burst, enemyData.level, rarity == 6 ? "UB+" : "UB");
                    action(UnitEquip >= 1 ? enemyData.skillData.raw_data.main_skill_evolution_1 : enemyData.skillData.raw_data.main_skill_1, enemyData.level, UnitEquip >= 1 ? "SK1+" : "SK1");
                    action(UnitEquip >= 2 ? enemyData.skillData.raw_data.main_skill_evolution_2 : enemyData.skillData.raw_data.main_skill_2, enemyData.level, UnitEquip >= 2 ? "SK2+" : "SK2");
                    action(rarity >= 5 ? enemyData.skillData.raw_data.ex_skill_evolution_1 : enemyData.skillData.raw_data.ex_skill_1, enemyData.level, rarity >= 5 ? "EX+" : "EX");
                    if (enemyData.skillData.raw_data.sp_skill_1 > 0)
                    {
                        action(enemyData.skillData.raw_data.sp_skill_1, enemyData.level, $"SP");
                    }
                }
                else
                {
                    action(enemyData.skillData.UB, enemyData.union_burst_level, "UB");
                    for (int i = 0; i < enemyData.main_skill_lvs.Count; i++)
                    {
                        int skID = enemyData.skillData.MainSkills[i];
                        int skLV = enemyData.main_skill_lvs[i];
                        if (skID > 0 && skLV > 0)
                        {
                            action(skID, skLV, $"技能{i + 1}");
                        }
                    }
                }
            }


            GameObject a = Instantiate(bossDetailPrefab, BaseBackManager.Instance.latestUIback.transform);
            a.GetComponent<ShowTextUI>().Set(detailstr);
            a.GetComponent<ShowTextUI>().index = index;
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
#if UNITY_5_3_OR_NEWER
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
#else
            yield return null;
#endif
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
            if (RandomSeedTalent != null)
                SettingData.GetCurrentRandomData().RandomSeed = int.Parse(RandomSeedTalent.text);
            if (UseFixedRandomSeedTalent != null)
                SettingData.GetCurrentRandomData().UseFixedRandomSeed = UseFixedRandomSeedTalent.isOn;
            SettingData.GetCurrentPlayerGroup().playerSetingHP = long.Parse(currentHPinput.text);
            SettingData.GetCurrentPlayerGroup().usePlayerSettingHP = usePlayerSettingHP.isOn;
            SaveDataToJson();
        }
        private void UpdateWeaknessSprites()
        {
            int currentBossId = selectedBossEnemyids[0];

            if (MainManager.Instance.WeaknessDic.TryGetValue(currentBossId, out int[] weaknessValues))
            {
                int spriteIndex = 0;

                for (int i = 0; i < 5 && spriteIndex < 5; i++)
                {
                    int value = weaknessValues[i];

                    if (value > 100)
                    {
                        WeaknessSprites[spriteIndex].sprite = TalentSprites[i];
                        WeaknessSprites[spriteIndex].gameObject.SetActive(true); 
                        spriteIndex++;
                    }
                }

                for (int i = spriteIndex; i < WeaknessSprites.Count; i++)
                {
                    WeaknessSprites[i].gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (var weaknessSprite in WeaknessSprites)
                {
                    weaknessSprite.gameObject.SetActive(false);
                }
            }
        }
        public string GetTalentLevelDescription()
        {
            // var talentLevel = SettingData.GetCurrentPlayerGroup().talentLevel;
#if ENABLE_THREADING
            var talentLevel = MainManager.Instance.TeamEnhanceData.talentLevelParameter;
#else
            var talentLevel = MainManager.Instance.teamEnhanceData.talentLevelParameter;
#endif
            string description = $"{TalentNames[0]}{talentLevel.GetValueOrDefault(1, 0)}，" +
                               $"{TalentNames[1]}{talentLevel.GetValueOrDefault(2, 0)}，" +
                               $"{TalentNames[2]}{talentLevel.GetValueOrDefault(3, 0)}，" +
                               $"{TalentNames[3]}{talentLevel.GetValueOrDefault(4, 0)}，" +
                               $"{TalentNames[4]}{talentLevel.GetValueOrDefault(5, 0)}";

            return description;
        }
        public string GetFinalTalentSkillNodeDescription()
        {
            var talentSkill = SettingData.GetCurrentPlayerGroup().talentSkill;
            var nodeChildrenDict = new Dictionary<int, List<int>>();
            var NodeDataDict = MainManager.Instance.TalentSkillNodeDic;
            foreach (var node in NodeDataDict.Values)
            {
                if (node.node_id > 0)
                    nodeChildrenDict[node.node_id] = new List<int>();
            }

            foreach (var node in NodeDataDict.Values)
            {
                if (node.node_id == 0) continue;

                foreach (int parentId in node.pre_node_ids)
                {
                    if (parentId > 0 && nodeChildrenDict.ContainsKey(parentId))
                        nodeChildrenDict[parentId].Add(node.node_id);
                }
            }
            // 找到所有最终节点（已激活且子节点未激活或无子节点）
            var finalNodes = talentSkill
                .Where(node => node.Value > 0 &&
                               (!nodeChildrenDict.TryGetValue(node.Key, out var children) ||
                                !children.Any(childId => talentSkill.TryGetValue(childId, out int childLevel) && childLevel > 0)))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // 如果没有最终节点，返回提示信息
            if (finalNodes.Count == 0)
                return "无";

            // 找到最高页面编号上的最终节点
            var targetNode = finalNodes
                .Where(node => NodeDataDict.ContainsKey(node.Key))
                .OrderByDescending(node => node.Key)
                .First();

            if (!NodeDataDict.TryGetValue(targetNode.Key, out TalentSkillNode nodeData))
                return "无";

            int pageNum = nodeData.page_num;
            int targetNodeId = targetNode.Key;
            int targetNodeLevel = targetNode.Value;

            // 计算合流编号
            var confluenceNodes = NodeDataDict.Values
                .Where(n => n.page_num == pageNum &&
                           n.pre_node_ids?.Count(id => id > 0) > 1 &&
                           n.pre_node_ids.Where(id => id > 0).Distinct().Count() > 1)
                .OrderBy(n => n.node_id)
                .ToList();

            int confluenceNum = confluenceNodes.FindIndex(n => n.node_id > targetNodeId);
            if (confluenceNum < 0) confluenceNum = confluenceNodes.Count;

            // 获取页面上左中右节点等级
            var pageNodeLevels = finalNodes
                .Where(kvp => NodeDataDict.TryGetValue(kvp.Key, out TalentSkillNode node) &&
                           node.page_num == pageNum && node.node_id > 0)
                .ToDictionary(kvp => NodeDataDict[kvp.Key].pos_x, kvp => kvp.Value);

            int leftLevel = pageNodeLevels.GetValueOrDefault(2, 0);
            int middleLevel = pageNodeLevels.GetValueOrDefault(5, 0);
            int rightLevel = pageNodeLevels.GetValueOrDefault(8, 0);

            // 计算在最近合流点之后的位置计数（左中右各有多少个节点）
            float targetNodeY = nodeData.pos_y;
            var precedingConfluenceNodes = confluenceNodes.Where(c => c.pos_y < targetNodeY).OrderByDescending(c => c.pos_y);

            float confluenceY = precedingConfluenceNodes.FirstOrDefault()?.pos_y ?? float.MinValue;

            // 计算在合流点之后的节点数量
            int lastConfluenceNodeId = precedingConfluenceNodes.FirstOrDefault()?.node_id ?? 0;

            var enhancedNodes = NodeDataDict.Values
                .Where(n => n.page_num == pageNum &&
                           n.node_id >= lastConfluenceNodeId &&
                           n.pos_y > confluenceY &&
                           n.pos_y <= targetNodeY &&
                           talentSkill.ContainsKey(n.node_id) && // 确保节点在天赋技能中存在
                           talentSkill[n.node_id] > 0) // 确保强化等级大于0
                .ToList();

            // 按位置分组并统计数量和等级
            var leftNodes = enhancedNodes.Where(n => n.pos_x == 2).ToList();
            var middleNodes = enhancedNodes.Where(n => n.pos_x == 5).ToList();
            var rightNodes = enhancedNodes.Where(n => n.pos_x == 8).ToList();

            int leftCount = leftNodes.Count;
            int middleCount = middleNodes.Count;
            int rightCount = rightNodes.Count;

            // 构造返回文本
            return confluenceNodes.Any(n => n.node_id == targetNodeId)
                ? $"第{pageNum}页 第{confluenceNum}合流 交点[{targetNodeLevel}级]"
                : $"第{pageNum}页 第{confluenceNum}合流 左{leftCount}[{leftLevel}级] 中{middleCount}[{middleLevel}级] 右{rightCount}[{rightLevel}级]";
        }
        public string GetFinalTeamSkillNodeDescription()
        {
            var teamSkill = SettingData.GetCurrentPlayerGroup().teamSkill;
            if (teamSkill == null || teamSkill.Count == 0)
            {
                return "无";
            }
            return $"第{teamSkill.Keys.Max()}个";
        }
        public void GetTalentFormationBonusDescription()
        {
            CalcTalentFormationBonus();

            foreach (var bonusObj in TalentBonus)
            {
                bonusObj.SetActive(false);
            }

            List<KeyValuePair<int, long>> validBonuses = new List<KeyValuePair<int, long>>();
            foreach (var kvp in UnitTalentFormationBonusDictionary)
            {
                long bonusValue = kvp.Value;
                if (bonusValue > 0)
                {
                    validBonuses.Add(kvp);
                }
            }

            for (int i = 0; i < validBonuses.Count && i < 2; i++)
            {
                var kvp = validBonuses[i];
                int elementType = kvp.Key;
                long bonusValue = kvp.Value;

                GameObject bonusObj = TalentBonus[i];
                bonusObj.SetActive(true);

                // 设置颜色
                if (bonusObj.TryGetComponent<Image>(out var image))
                {
                    image.color = elementColors[elementType-1];
                }

                // 设置文本
                Text text = bonusObj.GetComponentInChildren<Text>();
                if (text != null)
                {
                    // bonusValue是万分比，所以需要除以100得到百分比值
                    text.text = TalentNames[elementType-1] + (bonusValue / 100) + "%";
                }
            }
        }
        /// <summary>
        /// 计算当前编队的天赋阵型加成
        /// </summary>
        public void CalcTalentFormationBonus(bool isBattle = false)
        {
            // 如果未开启"骑士增强"功能，则直接返回
            // if (!_IsReleasedKnightEnhance)
            //     return;

            // 创建一个字典来统计每个天赋ID的出现次数
            Dictionary<int, int> talentCountDict = new Dictionary<int, int>();
            // List<int> talentIds = new List<int>();
            // 遍历当前我方编队中的所有单位
            var unitList = SettingData.GetCurrentPlayerGroup().playerData.playrCharacters;
            if (isBattle)
            {
                unitList = MyGameCtrl.Instance.tempData.playerList;
            }
            for (int i = 1; i <= 5; i++)
            {
                talentCountDict[i] = 0;
            }
            if (unitList != null)
            {
                for (int i = 0; i < unitList.Count; i++)
                {
                    var unit = unitList[i];
                    if (unit == null)
                        continue;
                    // List<int> talentIds = MainManager.Instance.GetTalentIds(unit.unitId);
                    // 获取单位的天赋列表
                    var talents = MainManager.Instance.GetTalentIds(unit.unitId);
                    if (talents == null)
                        continue;

                    // 统计每个天赋的出现次数
                    for (int j = 0; j < talents.Count; j++)
                    {
                        int talentId = talents[j];

                        // 获取当前天赋的计数，如果不存在则为0
                        talentCountDict.TryGetValue(talentId, out int currentCount);
                        // 更新计数+1
                        talentCountDict[talentId] = currentCount + 1;
                    }
                }
            }

            // 遍历统计好的天赋次数字典
            foreach (var pair in talentCountDict)
            {
                int talentId = pair.Key;
                int count = pair.Value;

                long bonus = 0;
                bonus = GetTalentBonusPermyriad(talentId, count, isBattle);
                if (UnitTalentFormationBonusDictionary != null)
                {
                    UnitTalentFormationBonusDictionary[talentId] = bonus;
                }
            }
        }
        public long GetTalentBonusPermyriad(int talentId, int count, bool isBattle = false) // present
        {
            var bossId = isBattle ? MyGameCtrl.Instance.tempData.guildEnemy[0].enemy_id : selectedBossEnemyids[0];
            var waveGroupId = isBattle ? MyGameCtrl.Instance.tempData.waveGroupId : WaveGroupId;
            var formationData = IsTalentQuestBossId(bossId) || IsAbyssBossId(bossId)
                ? (MainManager.Instance.TalentFormationDic.TryGetValue(waveGroupId, out var talentData) ? talentData : null)
                : MainManager.Instance.TalentFormationDic.TryGetValue(bossId, out var bossData)
                    ? bossData
                    : MainManager.Instance.TalentFormationDic.TryGetValue(1, out var defaultData) ? defaultData : null;
            // Debug.Log($"GetTalentBonusPermyriad: talentId={talentId}, count={count}, formationData={formationData.formation_bonus_values[talentId - 1][count-1]}");
            return count > 0 && formationData != null ? formationData.formation_bonus_values[talentId - 1][count-1] : 0;
        }
        public void ExtraEffectButton()
        {
            GameObject a = Instantiate(ExtraEffectPrefab);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
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
        public List<GuildPlayerGroupData> guildPlayerGroupDatas = new List<GuildPlayerGroupData>();

        public Dictionary<int, UnitAttackPattern> changedEnemyAttackPatternDic = new Dictionary<int, UnitAttackPattern>();
        public Dictionary<int, EnemyData> changedEnemyDataDic;

        public int currentPlayerGroupNum;
        //public bool isViolent;
        public int calSpeed;
        public int FPSforLogic = 60;
        public int FPSforAnimation = 60;
        public bool calcSpineAnimation = true;
        public int UBTryingCount = 9999;
        public int SemanSkillOn = 0;
        public int SemanUBOn = 0;
        // public bool UseFixedRandomSeed = true;

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
            if (data.selectedEnemyIDs[0] / 100000000 == 8)
            {
                des += data.talentQuestMap switch
                {
                    0 => "火",
                    1 => "水",
                    2 => "风",
                    3 => "光",
                    4 => "暗",
                    _ => data.talentQuestMap,
                };
                des += $"{data.talentQuestArea + 1}-{data.talentQuestIndex + 1}";
            }
            else if (data.selectedEnemyIDs[0] / 100000000 == 9)
            {
                des += $"{data.abyssMonth + 1}-";
                des += data.abyssTurn switch
                {
                    0 => "N",
                    1 => "H",
                    2 => "VH",
                    3 => "EX",
                    _ => data.abyssTurn,
                };
                des += $"{data.abyssEnemyNum}";
            }
            else if (data.selectedEnemyIDs[0] / 100000 == 5020)
            {
                des += $"D{data.dungeonDay + 1}-";
                des += $"{data.dungeonFloor + 8}";
            }
            else if (data.selectedEnemyIDs[0] / 100000000 == 7)
            {
                des += data.mirageArea switch
                {
                    0 => "日常",
                    1 => "霸瞳",
                    2 => "赞恩",
                    3 => "弥勒",
                    _ => data.mirageArea,
                };
                des += $"{data.mirageFloor + 1}";
            }
            else
            {
                des += (char)('A' + data.currentTurn - 1);
                if (data.currentGuildEnemyNum == 4 && data.isViolent)
                    des += "6";
                else
                    des += data.currentGuildEnemyNum + 1;
                if (data.usePlayerSettingHP)
                    des += "#";
            }
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
        public Dictionary<int, int> talentLevel = new Dictionary<int, int>();
        public Dictionary<int, int> talentSkill = new Dictionary<int, int>();
        public Dictionary<int, int> teamSkill = new Dictionary<int, int>();
        [JsonConverter(typeof(PCRCaculator.RoleMasteryJsonConverter))]
        public Dictionary<int, (int slotLevel, int enhanceLevel)> roleMastery = new Dictionary<int, (int, int)>();
        public List<List<float>> UBExecTimeData = new List<List<float>> { };
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
        public long playerSetingHP;
        public LogBarrierType useLogBarrierNew;
        public int dungeonMonth;
        public int dungeonDay;
        public int dungeonFloor;
        public int talentQuestMap;
        public int talentQuestArea;
        public int talentQuestIndex;
        public int abyssMonth;
        public int abyssEnemyNum;
        public int abyssTurn;
        public int mirageArea;
        public int mirageFloor;
        public int waveGroupId;

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
