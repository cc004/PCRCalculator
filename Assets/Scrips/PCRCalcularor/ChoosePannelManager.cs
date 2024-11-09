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
namespace PCRCaculator
{
  public class ChoosePannelManager : MonoBehaviour
  {
    private class ExEquipOption : Dropdown.OptionData
    {
      public int equip_id;

      public ExEquipOption(ex_equipment_data data)
      {
        equip_id = data.ex_equipment_id;
        text = data.name;
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
    public InputField Search;
    public Toggle Favriote;
    public Toggle Unused;

    private void Start()
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
      CallChooseBack_0(type, player);
    }
    private void CallChooseBack_0(int type, AddedPlayerData player = null)
    {
      Favriote.isOn = GuildManager.Instance.SettingData.Favriote;
      Unused.isOn = GuildManager.Instance.SettingData.Unused;
      this.type = type;
      baseBack.SetActive(true);
      chooseBack_A.SetActive(true);
      settingBack.SetActive(false);
      if (player == null || type == 4)
      {
        // enemyGroups.SetActive(false);
      }
      else
      {
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
      }
      //selectedCharId.Clear();
      if (type == 4)
      {
        selectedCharId.Clear();
        for (int i = 0; i < 5; i++)
        {
          if (i < player.playrCharacters.Count)
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
      if (type != 4) RefreshBasePage(0);
      switchToggles[0].isOn = true;
      RefreshSelectedButtons();

      if (type == 1)
      {
        nextButtonText.text = "战斗开始";
      }
      else if (type == 2 || type == 3 || type == 4)
      {
        nextButtonText.text = "下一步";
      }
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
        for (int i = 0; i < 4; i++)
        {
          if (switchToggles[i].isOn)
          {
            RefreshBasePage(i);
            // Search.text="";
          }
        }
      }
    }

    public void OpenProperty()
    {
      GuildManager.Instance.ActivateCharacterDetailPage(new Vector3(260, 0, 0), 0.6f);
      GuildManager.Instance.RefreshCharacterDetailPage(selectedCharacterId_setting);
    }
    public void CloseProperty()
    {
      GuildManager.Instance.HideCharacterDetailPage();
    }
    public void CancalButton()
    {
      parents[0].gameObject.SetActive(false);
      parents[1].gameObject.SetActive(false);
      parents[2].gameObject.SetActive(false);
      last = -1;
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
      this.OpenProperty();
      if (selectedCharId.Count == 0)
      {
        MainManager.Instance.WindowMessage("请选择至少一个角色！");
        return;
      }
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
      if (type == 1)
      {
        //MainManager.Instance.WindowMessage("战斗系统还没做好！");
        //CancalButton();
        JJCManager.Instance.ReadyAttack(selectedCharId);
      }
      else if (type >= 2)
      {
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
        RefreshSettingPage();
        RefreshSettingValues(true);
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
          RefreshSettingPage();
          RefreshSettingValues();

      }*/
    }
    public void BackButton()
    {
      CloseProperty();
      RefreshBasePage(0);
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
        togglePerferbs[unitid].enabled = false;
        throw new Exception("最多选5个!");
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
          RefreshSettingValues(true);
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
      RefreshSettingPage();

      if (data.rank < 2) data.skillLevel[1] = 0;
      if (data.rank < 4) data.skillLevel[2] = 0;
      if (data.rank < 7)
      {
        data.skillLevel[3] = 0;
        data.uniqueEqLv = 0;
      }

      for (int i = 0; i < 3; ++i)
      {
        data.exEquip[i] = (ExEquip[i].options[ExEquip[i].value] as ExEquipOption).equip_id;
        data.exEquipLevel[i] = (int)detailSliders_setting[15 + i].value;
      }
      RefreshSettingValues(false);

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
      RefreshSettingValues(false);
      RefreshSettingPage();
      MainManager.Instance.WindowMessage("成功！");
    }
    public void SetAllUnitMax()
    {
      foreach (UnitData a in playerData.playrCharacters)
      {
        a.SetMax();
      }
      RefreshSettingValues(false);
      RefreshSettingPage();
      MainManager.Instance.WindowMessage("成功！");
    }
    public void OpenEXSettingPannel()
    {
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
        if (!toggle.isOn&&toggle!=null)
        {
          toggle.enabled = k;
        }
      }
    }

    public Dictionary<int, CharacterPageButton> buttons;
    public Dictionary<int, CharacterPageButton> searchButtons;
    public Transform[] parents;
    public int last = -1;

    private void RefreshBasePage(int type)
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

      if (buttons == null)
      {

        buttons = new Dictionary<int, CharacterPageButton>();
        searchButtons= new Dictionary<int, CharacterPageButton>();
        foreach (Toggle a in togglePerferbs.Values)
        {
          Destroy(a.gameObject);
        }

        togglePerferbs.Clear();
        parent.localPosition = new Vector3();
        parent.sizeDelta = new Vector2(100, original_hight);
        foreach (int id in MainManager.Instance.UnitRarityDic.Keys)
        {
          /*if (type == 0 || MainManager.Instance.UnitRarityDic[id].unitPositionType == positionType)*/
          {
            if (MainManager.Instance.JudgeWeatherShowThisUnit(id)) // || (id>=400000&&id<=499999))
            {
              GameObject b = Instantiate(togglePerferb);
              b.transform.SetParent(parents[(int)MainManager.Instance.UnitRarityDic[id].unitPositionType]);
              b.transform.localScale = new Vector3(1, 1, 1);
              int id0 = id;

              b.GetComponent<Toggle>().onValueChanged.AddListener(value => OnToggleSwitched(value, id0));
              b.GetComponent<CharacterPageButton>().SetButton(id);
              togglePerferbs[id0] = b.GetComponent<Toggle>();
              buttons[id0] = b.GetComponent<CharacterPageButton>();
              //showUnitIDs.Add(id);
            }
          }
        }
      }

      int count = 1;

      foreach (int id in MainManager.Instance.UnitRarityDic.Keys)
      {
        if (MainManager.Instance.JudgeWeatherShowThisUnit(id))
        {
          if (type == 0 || MainManager.Instance.UnitRarityDic[id].unitPositionType == positionType)
          {
            UnitData unitData;
            if (MainManager.Instance.unitDataDic.TryGetValue(id, out unitData))
            {
              unitData = unitData;
            }
            else
            {
              unitData = new UnitData(id, 1);
            }

            buttons[id].RefreshData(unitData);

            bool shouldDisplay = (unitData.fav && Favriote.isOn) || (!unitData.fav && Unused.isOn);
            if (shouldDisplay && (Search.text == "" || results.Contains(id)))
            {
              buttons[id].transform.localPosition = new Vector3(
                  baseRange.x + range.x * ((count - 1) % 8),
                  -1 * (baseRange.y + range.y * Mathf.FloorToInt((count - 1) / 8)), 0);
              togglePerferbs[id].isOn = selectedCharId.Contains(id);
              count++;
            }
            else
            {
              buttons[id].gameObject.SetActive(false);
            }
          }
        }
      }
      if (last != type)
      {
        parents[0].gameObject.SetActive(type == 0 || type == 1);
        parents[1].gameObject.SetActive(type == 0 || type == 2);
        parents[2].gameObject.SetActive(type == 0 || type == 3);
      }

      if (parent.sizeDelta.y < Mathf.CeilToInt(count / 8) * 95 + 105)
      {
        parent.sizeDelta = new Vector2(100, Mathf.CeilToInt(count / 8) * 95 + 105);
      }

      if (parent.localPosition.y > Mathf.CeilToInt(count / 8 - 2) * 95 + 105)
      {
        parent.SetLocalPosY(Mathf.CeilToInt(count / 8 - 2) * 95 + 105);
      }
      TurnAllToggles(selectedCharId.Count != 5);
      last = type;
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
    public void RefreshSettingValues(bool changingId)
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

      // ex equip

      if (MainManager.Instance.unitExEquips.TryGetValue(data.unitId, out var temp))
      {
        for (int i = 0; i < 3; ++i)
        {
          if (changingId)
          {
            ExEquip[i].ClearOptions();
            ExEquip[i].AddOptions(temp[i].Values
                .Select(x => new ExEquipOption(x)).Prepend(ExEquipOption.None)
                .ToList<Dropdown.OptionData>());
            ExEquip[i].value = Math.Max(0,
                ExEquip[i].options.FindIndex(e => e is ExEquipOption e2 && e2.equip_id == data.exEquip[i]));
          }

          detailSliders_setting[15 + i].maxValue =
              data.exEquip[i] == 0 ? 0 : temp[i][data.exEquip[i]].levelMax;
          detailSliders_setting[15 + i].minValue = 0;
          detailSliders_setting[15 + i].value = data.exEquipLevel[i];
          detailTexts_setting[15 + i].text = detailSliders_setting[15 + i].value.ToString();
        }
      }
      else
      {
        for (int i = 0; i < 3; ++i)
        {
          ExEquip[i].ClearOptions();
          ExEquip[i].AddOptions(new List<Dropdown.OptionData> { ExEquipOption.None });
          detailSliders_setting[15 + i].maxValue = 0;
          detailSliders_setting[15 + i].minValue = 0;
          detailSliders_setting[15 + i].value = 0;
          detailTexts_setting[15 + i].text = "0";
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
      GuildManager.Instance.RefreshCharacterDetailPage(selectedCharacterId_setting, data);
    }
    private List<int> results;
    public void SearchChara()
    {
      string filePath = Application.streamingAssetsPath + "/Datas/_pcr_data.json";
      string jsonString = File.ReadAllText(filePath);
      var data = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonString);
      results = new List<int>();
      foreach (var entry in data)
      {
        int parsedKey = int.Parse(entry.Key + "01");
        foreach (var value in entry.Value)
        {
          if (value.Contains(Search.text))
          {
            results.Add(parsedKey);
            break;
          }
        }
      }
      if (buttons != null) buttons = null;
      RefreshBasePage(0);
    }
    public void FavIsShow()
    {
      GuildManager.Instance.SettingData.Favriote = Favriote.isOn;
      GuildManager.Instance.SettingData.Unused = Unused.isOn;
      SaveManager.Save(GuildManager.Instance.SettingData);
      if (buttons != null) buttons = null;
      RefreshBasePage(0);
      switchToggles[0].isOn = true;
    }
  }
}