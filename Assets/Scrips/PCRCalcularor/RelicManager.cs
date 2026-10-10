using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class RelicManager : MonoBehaviour
    {
        public static RelicManager Instance;
        public GameObject relicPanel;
        public ToggleGroup relicToggles;
        public List<GameObject> relicManagers = new List<GameObject>();

        private LabyrinthRelicManager SelectionManager
        {
            get
            {
#if UNITY_5_3_OR_NEWER
                if (relicPanel != null)
                {
                    var manager = relicPanel.GetComponentInChildren<LabyrinthRelicManager>(true);
                    if (manager != null)
                        return manager;
                }
#endif
                return LabyrinthRelicManager.Instance;
            }
        }

        private LabyrinthEnhanceManager EnhanceManager
        {
            get
            {
#if UNITY_5_3_OR_NEWER
                if (relicPanel != null)
                {
                    var manager = relicPanel.GetComponentInChildren<LabyrinthEnhanceManager>(true);
                    if (manager != null)
                        return manager;
                }
#endif
                return LabyrinthEnhanceManager.Instance;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            LoadPanel();
        }

        public void OpenRelicButton()
        {
            if (relicPanel == null)
                return;

            relicPanel.SetActive(true);
            LoadPanel();
        }

        private void LoadPanel()
        {
            SelectionManager?.LoadSavedSelection();
            EnhanceManager?.LoadSavedSelection();
            OnRelicToggleChanged();
        }

        public void SaveButton(bool global = false)
        {
            var mainManager = MainManager.Instance;
            var guildManager = Guild.GuildManager.Instance;
            var group = guildManager?.SettingData?.GetCurrentPlayerGroup();
            if (mainManager == null || group == null)
                return;

            SelectionManager?.SaveSelectedRelics();
            EnhanceManager?.SaveSelectedNodes();
            if (global)
            {
                mainManager.PlayerSetting.labyrinthRelicIds = new List<int>(
                    group.labyrinthRelicIds ?? new List<int>());
                mainManager.PlayerSetting.labyrinthAlphaRupee = group.labyrinthAlphaRupee;
                mainManager.PlayerSetting.labyrinthEnhancedNodeIds = new List<int>(
                    group.labyrinthEnhancedNodeIds ?? new List<int>());
                mainManager.SavePlayerSetting();
            }

            // 全局保存同样写入当前队伍，避免退出后丢失队伍的遗物选择。
            guildManager.SaveDataToJson();
            mainManager.WindowMessage("遗物配置保存成功！");
        }

        public void UsePlayerSetting()
        {
            var mainManager = MainManager.Instance;
            var guildManager = Guild.GuildManager.Instance;
            var group = guildManager?.SettingData?.GetCurrentPlayerGroup();
            if (mainManager == null || group == null)
                return;

            mainManager.LoadPlayerSettings();
            // 空列表也是有效配置，表示取消当前队伍的全部遗物。
            group.labyrinthRelicIds = new List<int>(
                mainManager.PlayerSetting.labyrinthRelicIds ?? new List<int>());
            group.labyrinthAlphaRupee = Mathf.Max(0, mainManager.PlayerSetting.labyrinthAlphaRupee);
            group.labyrinthEnhancedNodeIds = new List<int>(
                mainManager.PlayerSetting.labyrinthEnhancedNodeIds ?? new List<int>());
            SelectionManager?.LoadSavedSelection();
            EnhanceManager?.LoadSavedSelection();
            guildManager.SaveDataToJson();
            mainManager.WindowMessage("已恢复成全局遗物配置！");
        }

        public void OnRelicToggleChanged()
        {
            if (relicManagers == null || relicManagers.Count == 0)
                return;

            int selectedIndex = 0;
            if (relicToggles != null)
            {
                for (int i = 0; i < relicToggles.transform.childCount; i++)
                {
                    var toggle = relicToggles.transform.GetChild(i).GetComponent<Toggle>();
                    if (toggle != null && toggle.isOn)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }

            selectedIndex = Mathf.Min(selectedIndex, relicManagers.Count - 1);
            for (int i = 0; i < relicManagers.Count; i++)
            {
                if (relicManagers[i] != null)
                    relicManagers[i].SetActive(i == selectedIndex);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}
