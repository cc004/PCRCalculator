using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PCRCaculator.Guild;
namespace PCRCaculator
{
    public class TalentLevelManager : MonoBehaviour
    {
        public event System.Action OnTalentLevelChanged;
        public static TalentLevelManager Instance;
        private bool loadSliderEvents = false;
        public List<Slider> talentLevelSliders;
        public GuildSettingData SettingData => GuildManager.Instance.SettingData;
        public PlayerSetting PlayerSetting { get => MainManager.Instance.PlayerSetting; }

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            LoadTalentLevelSliders();
        }
        public void LoadGlobalSettings()
        {
            SettingData.GetCurrentPlayerGroup().talentLevel = MainManager.Instance.PlayerSetting.talentLevel;
            LoadTalentLevelSliders();
        }
        public void ReLoad()
        {
            GuildManager.LoadAddedPlayerData();
            if ((SettingData.GetCurrentPlayerGroup().talentLevel?.Count ?? 0) > 0 ||
                (SettingData.GetCurrentPlayerGroup().talentSkill?.Count ?? 0) > 0 ||
                (SettingData.GetCurrentPlayerGroup().teamSkill?.Count ?? 0) > 0)
            {
                OnEnable();
            }
            else
            {
                MainManager.Instance.WindowMessage("初始队伍无数据可还原!");
            }
        }
        /// <summary>
        /// 加载属性等级滑动条
        /// </summary>
        public void LoadTalentLevelSliders()
        {
            loadSliderEvents = true;
            for (int i = 0; i < talentLevelSliders.Count; i++)
            {
                int key = i + 1;
                if (SettingData.GetCurrentPlayerGroup().talentLevel.TryGetValue(key, out int savedValue))
                {
                    talentLevelSliders[i].value = savedValue;
                }
            }
            loadSliderEvents = false;
            OnTalentSliderChanged();
        }

        /// <summary>
        /// 处理滑动条变化
        /// </summary>
        public void OnTalentSliderChanged()
        {
            if (loadSliderEvents) return;
            var talentLevels = new Dictionary<int, int>();

            for (int i = 0; i < talentLevelSliders.Count; i++)
            {
                float sliderValue = talentLevelSliders[i].value;
                int key = i + 1;
                int oldValue = SettingData.GetCurrentPlayerGroup().talentLevel.GetValueOrDefault(key, 0);

                if (SettingData.GetCurrentPlayerGroup().talentLevel.ContainsKey(key))
                {
                    SettingData.GetCurrentPlayerGroup().talentLevel[key] = (int)sliderValue;
                }
                else
                {
                    SettingData.GetCurrentPlayerGroup().talentLevel.Add(key, (int)sliderValue);
                }
                talentLevels[key] = (int)sliderValue;

            }
            MainManager.Instance.TeamEnhanceData.SetTalentLevels(talentLevels);
            OnTalentLevelChanged?.Invoke();

        }
        /// <summary>
        /// 保存属性技能到玩家设置
        /// </summary>
        public void SaveTalentSkills(bool global = false)
        {
            var talentLevels = new Dictionary<int, int>();
            for (int i = 0; i < talentLevelSliders.Count; i++)
            {
                float sliderValue = talentLevelSliders[i].value;
                int key = i + 1;
                if (SettingData.GetCurrentPlayerGroup().talentLevel.ContainsKey(key))
                {
                    SettingData.GetCurrentPlayerGroup().talentLevel[key] = (int)sliderValue;
                }
                else
                {
                    SettingData.GetCurrentPlayerGroup().talentLevel.Add(key, (int)sliderValue);
                }

                talentLevels[key] = (int)sliderValue;
            }
            if (global)
            {
                MainManager.Instance.PlayerSetting.talentLevel = talentLevels;
            }
            SettingData.GetCurrentPlayerGroup().talentLevel = talentLevels;
            GuildManager.Instance.SaveDataToJson();
            // 直接将数据设置到PrincessKnightInfo
            MainManager.Instance.TeamEnhanceData.SetTalentLevels(talentLevels);
        }
    }
}