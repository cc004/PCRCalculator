using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using PCRCaculator.Guild;
using PCRCaculator.SQL;

namespace PCRCaculator
{
    public class RoleMasteryManager : MonoBehaviour
    {
#if ENABLE_THREADING
        [ThreadStatic]
#endif
        public static RoleMasteryManager Instance;
        public List<Toggle> unitRoleToggle;
        public GameObject roleMasteryPanel;
        private Dictionary<int, unit_role_mastery_id> UnitRoleMasteryIdDic => MainManager.Instance.UnitRoleMasteryIdDic;
        private Dictionary<int, UnitRoleMasterySlotEnhanceData> UnitRoleMasterySlotEnhanceDataDic => MainManager.Instance.UnitRoleMasterySlotEnhanceDataDic;
        private TeamEnhanceConfiguration PlayerGroup => TalentEnhanceContext.ActiveConfiguration;
        public Dictionary<int, (int slotLevel, int enhanceLevel)> roleMastery = new Dictionary<int, (int, int)>();
        public List<GameObject> roleSlotPrefabs;
        public Text roleLevelBatchText;
        public Text roleStarBatchText;
        public Slider roleLevelBatchSlider;
        public Slider roleStarBatchSlider;
        public Button roleSingleBatchButton;
        public Button roleAllBatchButton;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            UpdateRoleMasteryData();
        }

        void OnEnable()
        {
            roleMastery = PlayerGroup?.roleMastery ?? new Dictionary<int, (int, int)>();
            RefreshRoleSlotData();
            UpdateRoleMasteryData();
        }

        public void ReLoad()
        {
            GuildManager.LoadAddedPlayerData();
            OnEnable();
            UpdateRoleMasteryData();
        }

        public void RefreshRoleSlotData()
        {
            int selectedIndex = unitRoleToggle.FindIndex(toggle => toggle != null && toggle.isOn);
            if (selectedIndex == -1) selectedIndex = 0;

            int unitRoleId = selectedIndex + 1;

            var masteryDataList = UnitRoleMasteryIdDic.Values
                .Where(data => data.unit_role_id == unitRoleId)
                .OrderBy(data => data.slot_id)
                .ToList();

            ApplyBatchSliderLimits(masteryDataList);

            for (int i = 0; i < masteryDataList.Count && i < roleSlotPrefabs.Count; i++)
            {
                var masteryData = masteryDataList[i];
                RoleSlotPrefab roleSlotUI = roleSlotPrefabs[i].GetComponent<RoleSlotPrefab>();

                // 获取当前等级和星级
                int slotLevel = roleMastery?.ContainsKey(masteryData.mastery_id) == true
                    ? roleMastery[masteryData.mastery_id].slotLevel : 0;
                int enhanceLevel = roleMastery?.ContainsKey(masteryData.mastery_id) == true
                    ? roleMastery[masteryData.mastery_id].enhanceLevel : 0;

                if (UnitRoleMasterySlotEnhanceDataDic.TryGetValue(masteryData.mastery_id, out var slotEnhanceData))
                {
                    EnhanceLevelInfo enhanceInfo = null;
                    if (slotEnhanceData.slot_level_info?.ContainsKey(slotLevel) == true &&
                        slotEnhanceData.slot_level_info[slotLevel].enhance_level_info?.ContainsKey(enhanceLevel) == true)
                    {
                        enhanceInfo = slotEnhanceData.slot_level_info[slotLevel].enhance_level_info[enhanceLevel];
                    }

                    int enhanceValue = enhanceInfo?.enhance_value_1 ?? 0;
                    roleSlotUI.SetRoleSlotData(slotLevel, enhanceLevel, masteryData.mastery_id);
                }
            }
        }
        public void OnLevelSliderValueChanged()
        {
            int level = Mathf.RoundToInt(roleLevelBatchSlider.value);
            roleLevelBatchText.text = level.ToString();

            // 星级上限取决于当前等级（不同等级的强化层数不一定相同），等级变了要重新取数据库上限
            ApplyStarBatchLimit();

            // 当等级为0时，星级必须为0
            if (level == 0)
            {
                roleStarBatchSlider.value = 0;
                roleStarBatchText.text = "0";
            }
        }

        public void OnStarSliderValueChanged()
        {
            int level = roleLevelBatchSlider != null ? Mathf.RoundToInt(roleLevelBatchSlider.value) : 0;
            int star = Mathf.RoundToInt(roleStarBatchSlider.value);

            // 当等级为0时，星级不能增加
            if (level == 0 && star > 0)
            {
                star = 0;
                roleStarBatchSlider.value = 0;
            }

            roleStarBatchText.text = star.ToString();
        }

        /// <summary>
        /// 批量滑块的上限改成从数据库读取：
        /// 等级上限 = 当前职能所有槽位里最大的 slot_level；星级上限 = 当前等级下最大的 enhance_level。
        /// 数据库里取不到时退回 MainManager 缓存的全局上限，仍为 0 就保留场景里原有的值。
        /// </summary>
        private void ApplyBatchSliderLimits(List<unit_role_mastery_id> masteryDataList)
        {
            int slotLevelLimit = GetMaxSlotLevel(masteryDataList);
            if (slotLevelLimit <= 0 && MainManager.Instance != null)
                slotLevelLimit = MainManager.Instance.RoleMasterySlotLevelLimit;

            if (roleLevelBatchSlider != null && slotLevelLimit > 0)
            {
                roleLevelBatchSlider.maxValue = slotLevelLimit;
                if (roleLevelBatchSlider.value > slotLevelLimit)
                    roleLevelBatchSlider.value = slotLevelLimit;
                if (roleLevelBatchText != null)
                    roleLevelBatchText.text = Mathf.RoundToInt(roleLevelBatchSlider.value).ToString();
            }

            ApplyStarBatchLimit();
        }

        /// <summary>星级（强化等级）滑块上限 = 当前等级下数据库里的最大 enhance_level。</summary>
        private void ApplyStarBatchLimit()
        {
            if (roleStarBatchSlider == null)
                return;

            int slotLevel = roleLevelBatchSlider != null ? Mathf.RoundToInt(roleLevelBatchSlider.value) : 0;
            int enhanceLevelLimit = slotLevel > 0 ? GetMaxEnhanceLevel(slotLevel) : 0;
            if (enhanceLevelLimit <= 0 && MainManager.Instance != null)
                enhanceLevelLimit = MainManager.Instance.RoleMasteryEnhanceLevelLimit;

            if (enhanceLevelLimit <= 0)
                return;

            roleStarBatchSlider.maxValue = enhanceLevelLimit;
            if (roleStarBatchSlider.value > enhanceLevelLimit)
                roleStarBatchSlider.value = enhanceLevelLimit;
            if (roleStarBatchText != null)
                roleStarBatchText.text = Mathf.RoundToInt(roleStarBatchSlider.value).ToString();
        }

        private static int GetMaxSlotLevel(IEnumerable<unit_role_mastery_id> masteryDataList)
        {
            var dic = MainManager.Instance?.UnitRoleMasterySlotEnhanceDataDic;
            if (dic == null || masteryDataList == null)
                return 0;

            int max = 0;
            foreach (var data in masteryDataList)
            {
                if (data == null || !dic.TryGetValue(data.mastery_id, out var slotEnhance) || slotEnhance?.slot_level_info == null)
                    continue;

                foreach (var level in slotEnhance.slot_level_info.Keys)
                    if (level > max) max = level;
            }

            return max;
        }

        private static int GetMaxEnhanceLevel(int slotLevel)
        {
            var dic = MainManager.Instance?.UnitRoleMasterySlotEnhanceDataDic;
            if (dic == null || slotLevel <= 0)
                return 0;

            int max = 0;
            foreach (var slotEnhance in dic.Values)
            {
                if (slotEnhance?.slot_level_info == null ||
                    !slotEnhance.slot_level_info.TryGetValue(slotLevel, out var levelInfo) ||
                    levelInfo?.enhance_level_info == null)
                    continue;

                foreach (var star in levelInfo.enhance_level_info.Keys)
                    if (star > max) max = star;
            }

            return max;
        }

        /// <summary>把星级夹到该槽位在该等级下的数据库上限，避免批量设置越界。</summary>
        private static int ClampStarToDatabase(int masteryId, int slotLevel, int star)
        {
            if (star <= 0 || slotLevel <= 0)
                return 0;

            var dic = MainManager.Instance?.UnitRoleMasterySlotEnhanceDataDic;
            if (dic == null || !dic.TryGetValue(masteryId, out var slotEnhance) || slotEnhance?.slot_level_info == null)
                return star;

            if (!slotEnhance.slot_level_info.TryGetValue(slotLevel, out var levelInfo) ||
                levelInfo?.enhance_level_info == null || levelInfo.enhance_level_info.Count == 0)
                return 0;

            return Mathf.Clamp(star, 0, levelInfo.enhance_level_info.Keys.Max());
        }

        public void RoleSingleBatch()
        {
            // 获取当前滑块的值作为批量设置的等级和星级
            int batchLevel = roleLevelBatchSlider != null ? Mathf.RoundToInt(roleLevelBatchSlider.value) : 0;
            int batchStar = roleStarBatchSlider != null ? Mathf.RoundToInt(roleStarBatchSlider.value) : 0;

            int selectedIndex = unitRoleToggle.FindIndex(toggle => toggle != null && toggle.isOn);
            if (selectedIndex == -1) selectedIndex = 0;
            int unitRoleId = selectedIndex + 1;

            // 获取当前unitRoleId对应的所有mastery_id
            var masteryDataList = UnitRoleMasteryIdDic.Values
                .Where(data => data.unit_role_id == unitRoleId)
                .ToList();

            // 更新roleMastery字典
            foreach (var masteryData in masteryDataList)
            {
                int masteryId = masteryData.mastery_id;
                roleMastery[masteryId] = (batchLevel, ClampStarToDatabase(masteryId, batchLevel, batchStar));
            }

            RefreshRoleSlotData();
            UpdateRoleMasteryData();
        }

        public void RoleAllBatch()
        {
            // 获取当前滑块的值作为批量设置的等级和星级
            int batchLevel = roleLevelBatchSlider != null ? Mathf.RoundToInt(roleLevelBatchSlider.value) : 0;
            int batchStar = roleStarBatchSlider != null ? Mathf.RoundToInt(roleStarBatchSlider.value) : 0;

            // 为所有mastery_id设置相同的等级和星级
            foreach (var masteryData in UnitRoleMasteryIdDic.Values)
            {
                int masteryId = masteryData.mastery_id;
                roleMastery[masteryId] = (batchLevel, ClampStarToDatabase(masteryId, batchLevel, batchStar));
            }

            RefreshRoleSlotData();
            UpdateRoleMasteryData();
        }

        public void LoadGlobalSettings()
        {
            if (MainManager.Instance?.PlayerSetting?.roleMastery != null)
            {
                PlayerGroup.roleMastery = MainManager.Instance.PlayerSetting.roleMastery;
            }
            OnEnable();
        }

        public void SaveRoleMastery(bool global = false)
        {
            if (global && MainManager.Instance?.PlayerSetting != null)
            {
                MainManager.Instance.PlayerSetting.roleMastery = PlayerGroup.roleMastery;
            }
        }

        public void SetRoleMastery(Dictionary<int, (int slotLevel, int enhanceLevel)> newRoleMastery)
        {
            roleMastery.Clear();
            if (newRoleMastery == null)
            {
                return;
            }

            foreach (var kvp in newRoleMastery)
            {
                roleMastery[kvp.Key] = kvp.Value;
            }
        }

        public void UpdateRoleMasteryData()
        {
            TalentEnhanceContext.ActiveTeamEnhanceData.UpdateRoleMasteryData(roleMastery);
        }
    }
}
