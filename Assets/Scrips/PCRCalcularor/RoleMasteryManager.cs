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
        public static RoleMasteryManager Instance;
        public List<Toggle> unitRoleToggle;
        public GameObject roleMasteryPanel;
        private Dictionary<int, unit_role_mastery_id> UnitRoleMasteryIdDic => MainManager.Instance.UnitRoleMasteryIdDic;
        private Dictionary<int, UnitRoleMasterySlotEnhanceData> UnitRoleMasterySlotEnhanceDataDic => MainManager.Instance.UnitRoleMasterySlotEnhanceDataDic;
        private GuildPlayerGroupData PlayerGroup => GuildManager.Instance?.SettingData?.GetCurrentPlayerGroup();
        public Dictionary<int, (int slotLevel, int enhanceLevel)> roleMastery = new Dictionary<int, (int, int)>();
        public List<GameObject> roleSlotPrefabs;

        private void Awake()
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
            MainManager.Instance.TeamEnhanceData.UpdateRoleMasteryData();
        }

        void OnEnable()
        {
            roleMastery = PlayerGroup?.roleMastery ?? new Dictionary<int, (int, int)>();
            RefreshRoleSlotData();
            MainManager.Instance.TeamEnhanceData.UpdateRoleMasteryData();
        }

        public void ReLoad()
        {
            GuildManager.LoadAddedPlayerData();
            OnEnable();
            MainManager.Instance.TeamEnhanceData.UpdateRoleMasteryData();
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
            foreach (GameObject roleSlotPrefabObj in roleSlotPrefabs)
            {
                if (roleSlotPrefabObj == null) continue;

                RoleSlotPrefab roleSlotUI = roleSlotPrefabObj.GetComponent<RoleSlotPrefab>();
                if (roleSlotUI == null) continue;

                int masteryId = roleSlotUI.MasteryId;
                int slotLevel = roleSlotUI.roleSlotLevelSlider != null
                    ? Mathf.RoundToInt(roleSlotUI.roleSlotLevelSlider.value) : 0;

                if (roleMastery.ContainsKey(masteryId))
                {
                    roleMastery[masteryId] = (slotLevel, roleMastery[masteryId].enhanceLevel);
                }
                else
                {
                    roleMastery.Add(masteryId, (slotLevel, 0));
                }
            }

            if (global && MainManager.Instance?.PlayerSetting != null)
            {
                MainManager.Instance.PlayerSetting.roleMastery = roleMastery;
            }
        }
    }
}