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
        public GuildSettingData SettingData => GuildManager.Instance.SettingData;


        private bool isLoading;
        private void Awake()
        {
            Instance = this;
            if (MainManager.Instance.UnitRoleDataDic.Count == 0)
            {
                talentToggles.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// 主界面设置按钮
        /// </summary>
        public void OpenTalentButton()
        {
            isLoading = true;
            talentPanel.SetActive(true);
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
            RoleMasteryManager.Instance.SaveRoleMastery(global);
            if (global)
            {
                MainManager.Instance.SavePlayerSetting();
            }
            else
            {
                GuildManager.Instance.SaveDataToJson();
            }

            MainManager.Instance.WindowMessage("保存成功！");
        }
        public void UsePlayerSetting()
        {
            MainManager.Instance.LoadPlayerSettings();
            if ((PlayerSetting.teamSkill?.Count ?? 0) > 0 ||
                (PlayerSetting.talentSkill?.Count ?? 0) > 0 ||
                (PlayerSetting.talentLevel?.Count ?? 0) > 0)
            {
                TalnetInfoData.LoadFromPlayerSetting(true);
                TalentLevelManager.Instance.LoadGlobalSettings();
                TalentSkillTreeManager.Instance.LoadGlobalSettings();
                TeamSkillTreeManager.Instance.LoadGlobalSettings();
                RoleMasteryManager.Instance.LoadGlobalSettings();
                MainManager.Instance.WindowMessage("已恢复成全局配置！");
            }
            else
            {
                MainManager.Instance.WindowMessage("未找到全局配置！");
            }
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