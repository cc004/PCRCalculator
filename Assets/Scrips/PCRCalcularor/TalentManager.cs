using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Elements;
using PCRCaculator.Guild;
namespace PCRCaculator
{


    public class TalentManager : MonoBehaviour
    {
        public static TalentManager Instance;
        public List<TextMeshProUGUI> texts;
        public GameObject talentPanel;
        public ToggleGroup talentToggles;
        public List<GameObject> talentManagers;
        public PlayerSetting PlayerSetting { get => MainManager.Instance.PlayerSetting; }
        public TeamEnhanceParameter TalnetInfoData { get => MainManager.Instance.TeamEnhanceData; }


        private bool isLoading;
        private void Awake()
        {
            Instance = this;
        }
        /// <summary>
        /// 主界面设置按钮
        /// </summary>
        public void OpenTalentButton()
        {
            isLoading = true;
            talentPanel.SetActive(true);
            GuildManager.LoadAddedPlayerData();
            for (int i = 0; i < talentManagers.Count; i++)
            {
                talentManagers[i].SetActive(true);
            }
            TalentLevelManager.Instance.LoadTalentLevelSliders();
            TalentSkillTreeManager.Instance.InitializeData();
            TeamSkillTreeManager.Instance.InitializeData();
            isLoading = false;
            OnTalentToggleChanged();
        }
        /// <summary>
        /// 设置界面保存按钮
        /// </summary>
        public void SaveButton(bool global = false)
        {
            TalentLevelManager.Instance.SaveTalentSkills(global);
            TalentSkillTreeManager.Instance.SaveTalentSkills(global);
            TeamSkillTreeManager.Instance.SaveTeamSkills(global);
            if (global) {
                MainManager.Instance.SavePlayerSetting();
            } else {
                GuildManager.Instance.SaveDataToJson();
            }

            MainManager.Instance.WindowMessage("保存成功！");
        }
        public void UsePlayerSetting()
        {
            MainManager.Instance.ReLoadPlayerSetting();
            TalnetInfoData.LoadFromPlayerSetting(true);
            MainManager.Instance.WindowMessage("已恢复成全局配置！");
        }

        public void OnTalentToggleChanged()
        {
            int selectedIndex = 0;
            for (int i = 0; i < talentToggles.transform.childCount; i++)
            {
                Transform child = talentToggles.transform.GetChild(i);
                if (child.GetComponent<Toggle>().isOn)
                {
                    selectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < talentManagers.Count; i++)
            {
                talentManagers[i].SetActive(i == selectedIndex);
            }
        }
    }
}