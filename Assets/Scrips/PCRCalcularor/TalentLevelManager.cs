using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PCRCaculator.Guild;

namespace PCRCaculator
{
    public class TalentLevelManager : MonoBehaviour
    {
        public event System.Action OnTalentLevelChanged;
#if ENABLE_THREADING
        [ThreadStatic]
#endif
        public static TalentLevelManager Instance;
        private const int TalentElementCount = 5;
        private bool loadSliderEvents;
        public List<Slider> talentLevelSliders;
        private TeamEnhanceConfiguration PlayerGroup => TalentEnhanceContext.ActiveConfiguration;
        public PlayerSetting PlayerSetting { get => MainManager.Instance.PlayerSetting; }

        public void Awake()
        {
            Instance = this;
#if !UNITY_5_3_OR_NEWER
            SyncTalentLevelsByCSharp();
#endif
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

            LoadTalentLevelSliders();
        }

        /// <summary>
        /// 加载属性等级滑动条
        /// </summary>
        public void LoadTalentLevelSliders()
        {
#if UNITY_5_3_OR_NEWER
            if (talentLevelSliders == null || talentLevelSliders.Count == 0)
            {
                SyncTalentLevelsByCSharp();
                return;
            }

            var normalizedLevels = BuildTalentLevels(PlayerGroup.talentLevel);
            loadSliderEvents = true;
            for (int i = 0; i < talentLevelSliders.Count; i++)
            {
                int key = i + 1;
                talentLevelSliders[i].value = normalizedLevels.TryGetValue(key, out int savedValue) ? savedValue : 0;
            }

            loadSliderEvents = false;
            ApplyTalentLevelsDirect(normalizedLevels, true);
#else
            SyncTalentLevelsByCSharp();
#endif
        }

        /// <summary>
        /// 处理滑动条变化
        /// </summary>
        public void OnTalentSliderChanged()
        {
            if (loadSliderEvents)
            {
                return;
            }

#if UNITY_5_3_OR_NEWER
            if (talentLevelSliders == null || talentLevelSliders.Count == 0)
            {
                SyncTalentLevelsByCSharp(true);
                return;
            }

            var talentLevels = new Dictionary<int, int>(TalentElementCount);
            for (int i = 0; i < talentLevelSliders.Count; i++)
            {
                int key = i + 1;
                talentLevels[key] = (int)talentLevelSliders[i].value;
            }

            ApplyTalentLevels(talentLevels, true);
#else
            SyncTalentLevelsByCSharp(true);
#endif
        }

        /// <summary>
        /// 保存属性技能到玩家设置
        /// </summary>
        public void SaveTalentSkills(bool global = false)
        {
#if UNITY_5_3_OR_NEWER
            Dictionary<int, int> talentLevels;
            if (talentLevelSliders != null && talentLevelSliders.Count > 0)
            {
                talentLevels = new Dictionary<int, int>(TalentElementCount);
                for (int i = 0; i < talentLevelSliders.Count; i++)
                {
                    int key = i + 1;
                    talentLevels[key] = (int)talentLevelSliders[i].value;
                }
            }
            else
            {
                talentLevels = PlayerGroup.talentLevel;
            }
#else
            var talentLevels = PlayerGroup.talentLevel;
#endif

            ApplyTalentLevels(talentLevels);

            if (global)
            {
                MainManager.Instance.PlayerSetting.talentLevel = PlayerGroup.talentLevel;
            }

            GuildManager.Instance?.SaveDataToJson();
        }

        private void SyncTalentLevelsByCSharp(bool notify = false)
        {
            ApplyTalentLevelsDirect(BuildTalentLevels(PlayerGroup?.talentLevel), notify);
        }

        private void ApplyTalentLevels(Dictionary<int, int> talentLevels, bool notify = false)
        {
            ApplyTalentLevelsDirect(BuildTalentLevels(talentLevels), notify);
        }

        private void ApplyTalentLevelsDirect(Dictionary<int, int> normalizedLevels, bool notify = false)
        {
            PlayerGroup.talentLevel = normalizedLevels;
            TalentEnhanceContext.ActiveTeamEnhanceData.SetTalentLevels(normalizedLevels);

            if (notify)
            {
                OnTalentLevelChanged?.Invoke();
            }
        }

        private Dictionary<int, int> BuildTalentLevels(Dictionary<int, int> source)
        {
            var talentLevels = new Dictionary<int, int>(TalentElementCount);
            for (int i = 1; i <= TalentElementCount; i++)
            {
                talentLevels[i] = source != null && source.TryGetValue(i, out var value) ? value : 0;
            }

            return talentLevels;
        }
    }
}
