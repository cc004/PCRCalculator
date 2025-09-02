using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PCRCaculator
{
    public class TalentLevelManager : MonoBehaviour
    {
        public event System.Action OnTalentLevelChanged;
        public static TalentLevelManager Instance;
        private bool loadSliderEvents = false;
        public List<Slider> talentLevelSliders;
        public PlayerSetting PlayerSetting { get => MainManager.Instance.PlayerSetting; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            LoadTalentLevelSliders();
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
                if (PlayerSetting.talentLevelDic.TryGetValue(key, out int savedValue))
                {
                    talentLevelSliders[i].value = savedValue;
                }
            }
            loadSliderEvents = false;
        }

        /// <summary>
        /// 处理滑动条变化
        /// </summary>
        public void OnTalentSliderChanged()
        {
            if (loadSliderEvents) return;
            bool hasChanged = false;
            var talentLevels = new Dictionary<int, int>();

            for (int i = 0; i < talentLevelSliders.Count; i++)
            {
                float sliderValue = talentLevelSliders[i].value;
                int key = i + 1;
                int oldValue = PlayerSetting.talentLevelDic.GetValueOrDefault(key, 0);

                if (PlayerSetting.talentLevelDic.ContainsKey(key))
                {
                    PlayerSetting.talentLevelDic[key] = (int)sliderValue;
                }
                else
                {
                    PlayerSetting.talentLevelDic.Add(key, (int)sliderValue);
                }

                // 收集要设置到PrincessKnightInfo的数据
                talentLevels[key] = (int)sliderValue;

                // 检查值是否发生变化
                if (oldValue != (int)sliderValue)
                {
                    hasChanged = true;
                }
            }

            // 如果有变化，更新PrincessKnightInfo并通知更新
            if (hasChanged)
            {
                MainManager.Instance.TeamEnhanceData.SetTalentLevels(talentLevels);
                OnTalentLevelChanged?.Invoke();
            }
        }
        /// <summary>
        /// 保存属性技能到玩家设置
        /// </summary>
        public void SaveTalentSkillsToPlayerSetting()
        {
            // 保存属性等级滑动条的值并直接更新到PrincessKnightInfo
            var talentLevels = new Dictionary<int, int>();
            for (int i = 0; i < talentLevelSliders.Count; i++)
            {
                float sliderValue = talentLevelSliders[i].value;
                int key = i + 1;
                if (PlayerSetting.talentLevelDic.ContainsKey(key))
                {
                    PlayerSetting.talentLevelDic[key] = (int)sliderValue;
                }
                else
                {
                    PlayerSetting.talentLevelDic.Add(key, (int)sliderValue);
                }

                // 同时准备要设置到PrincessKnightInfo的数据
                talentLevels[key] = (int)sliderValue;
            }

            // 直接将数据设置到PrincessKnightInfo
            MainManager.Instance.TeamEnhanceData.SetTalentLevels(talentLevels);
        }
    }
}