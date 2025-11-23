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
        private GuildPlayerGroupData PlayerGroup => GuildManager.Instance?.SettingData?.GetCurrentPlayerGroup();
        public PlayerSetting PlayerSetting { get => MainManager.Instance.PlayerSetting; }

        public void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            LoadTalentLevelSliders();
        }
        public void LoadGlobalSettings()
        {
            PlayerGroup.talentLevel = MainManager.Instance.PlayerSetting.talentLevel;
            LoadTalentLevelSliders();
        }
        public void ReLoad()
        {
            GuildManager.LoadAddedPlayerData();
            if ((PlayerGroup.talentLevel?.Count ?? 0) == 0 &&
                (PlayerGroup.talentSkill?.Count ?? 0) == 0 &&
                (PlayerGroup.teamSkill?.Count ?? 0) == 0)
            {
                PlayerGroup.talentLevel = new Dictionary<int, int>();
            }
            OnEnable();
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
                if (PlayerGroup.talentLevel.TryGetValue(key, out int savedValue))
                {
                    talentLevelSliders[i].value = savedValue;
                }
                else
                {
                    talentLevelSliders[i].value = 0;
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
                int oldValue = PlayerGroup.talentLevel.GetValueOrDefault(key, 0);

                if (PlayerGroup.talentLevel.ContainsKey(key))
                {
                    PlayerGroup.talentLevel[key] = (int)sliderValue;
                }
                else
                {
                    PlayerGroup.talentLevel.Add(key, (int)sliderValue);
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
                if (PlayerGroup.talentLevel.ContainsKey(key))
                {
                    PlayerGroup.talentLevel[key] = (int)sliderValue;
                }
                else
                {
                    PlayerGroup.talentLevel.Add(key, (int)sliderValue);
                }

                talentLevels[key] = (int)sliderValue;
            }
            if (global)
            {
                MainManager.Instance.PlayerSetting.talentLevel = talentLevels;
            }
            PlayerGroup.talentLevel = talentLevels;
            GuildManager.Instance.SaveDataToJson();
            // 直接将数据设置到PrincessKnightInfo
            MainManager.Instance.TeamEnhanceData.SetTalentLevels(talentLevels);
        }
    }
}