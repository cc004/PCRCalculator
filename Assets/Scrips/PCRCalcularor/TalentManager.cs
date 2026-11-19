using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Elements;
using PCRCaculator.Guild;
namespace PCRCaculator
{


    public class TalentManager : MonoBehaviour
    {
        public static TalentManager Instance;
        public GameObject talentPanel;
        public ToggleGroup talentToggles;
        public List<GameObject> talentManagers;
        public PlayerSetting PlayerSetting { get => MainManager.Instance.PlayerSetting; }
        public TeamEnhanceParameter TalnetInfoData { get => MainManager.Instance.TeamEnhanceData; }
        public GuildSettingData SettingData => GuildManager.Instance.SettingData;
        public bool HasLiveTalentPanel => talentPanel != null;


        private void Awake()
        {
            Instance = this;
#if UNITY_5_3_OR_NEWER
            if (talentPanel != null)
            {
                AddShareButton("导入", -1174, () => MainManager.Instance.WindowInputMessage("粘贴公主骑士分享码", ImportShareCode));
                AddShareButton("导出", -1046, ExportShareCode);
            }
#endif
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
            TalentEnhanceContext.EndArenaEnemyEditing();
            OpenTalentPanel();
        }

        public void OpenArenaTalentPanel(bool isEnemy)
        {
            if (isEnemy)
                TalentEnhanceContext.BeginArenaEnemyEditing();
            else
                TalentEnhanceContext.EndArenaEnemyEditing();

            OpenTalentPanel();
        }

        private void OpenTalentPanel()
        {
            talentPanel.SetActive(true);
            TalentLevelManager.Instance.LoadTalentLevelSliders();
            TalentSkillTreeManager.Instance.InitializeData();
            TeamSkillTreeManager.Instance.InitializeData();
            OnTalentToggleChanged();
        }
        /// <summary>
        /// 设置界面保存按钮
        /// </summary>
        public void SaveButton(bool global = false)
        {
            if (global && TalentEnhanceContext.IsEditingArenaEnemy)
            {
                MainManager.Instance.WindowMessage("敌方独立属性强化仅保存到当前配置，请使用普通保存。");
                return;
            }

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
            if (TalentEnhanceContext.IsEditingArenaEnemy)
            {
                TalentEnhanceContext.ClearArenaEnemyConfiguration();
                GuildManager.Instance.SaveDataToJson();
                OpenTalentPanel();
                MainManager.Instance.WindowMessage("已清除敌方独立属性强化，恢复继承我方配置！");
                return;
            }

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

#if UNITY_5_3_OR_NEWER
        private void AddShareButton(string label, float x, UnityEngine.Events.UnityAction action)
        {
            var template = talentPanel.transform.Find("保存").GetComponent<Button>();
            var button = Instantiate(template, talentPanel.transform, false);
            button.name = label + "分享码";
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(action);
            button.GetComponentInChildren<Text>(true).text = label;
            var rect = (RectTransform)button.transform;
            rect.anchoredPosition = new Vector2(x, rect.anchoredPosition.y);
        }

        private void ExportShareCode()
        {
            try
            {
                string code = CurrentShareCode().Encode();
                GUIUtility.systemCopyBuffer = code;
                MainManager.Instance.WindowMessage("公主骑士分享码已复制到剪贴板。");
            }
            catch (FormatException ex) { MainManager.Instance.WindowMessage(ex.Message); }
        }

        private PrincessKnightShareCode CurrentShareCode()
        {
            var group = TalentEnhanceContext.ActiveConfiguration;
            var manager = MainManager.Instance;
            var data = new PrincessKnightShareCode();
            for (int i = 0; i < 5; i++)
            {
                data.Spheres[i] = group.talentLevel.GetValueOrDefault(i + 1);
                if (data.Spheres[i] < 1) throw new FormatException("分享码要求五系属性等级均至少为 1。");
            }
            var frontier = TalentSkillTreeManager.Instance.GetHighestActiveTalentSkills();
            foreach (var id in manager.TalentSkillNodeDic.Keys)
                if (frontier.TryGetValue(id, out var level)) data.Nodes.Add(id, level);
            data.Master = TeamSkillTreeManager.Instance.GetMasterNodeId();
            foreach (var entry in manager.UnitRoleMasteryIdDic.Values)
            {
                if (entry.unit_role_id < 1 || entry.unit_role_id > 8 || entry.slot_id < 1 || entry.slot_id > 4)
                    continue;
                int index = (entry.unit_role_id - 1) * 4 + entry.slot_id - 1;
                if (group.roleMastery.TryGetValue(entry.mastery_id, out var slot) && slot.slotLevel > 0)
                    data.Mastery[index] = (slot.slotLevel - 1) * 6 + slot.enhanceLevel + 1;
            }
            return data;
        }

        private void ImportShareCode(string code)
        {
            try
            {
                var data = PrincessKnightShareCode.Decode(code);
                var imported = ReadShareConfiguration(data);
                var group = TalentEnhanceContext.ActiveConfiguration;
                group.talentLevel = imported.talentLevel;
                group.talentSkill = imported.talentSkill;
                group.teamSkill = imported.teamSkill;
                group.roleMastery = imported.roleMastery;
                OpenTalentPanel();
                var tree = TalentSkillTreeManager.Instance;
                tree.GoToPage(data.Nodes.Keys.Select(id => MainManager.Instance.TalentSkillNodeDic[id].page_num).DefaultIfEmpty(1).Max());
                var roles = RoleMasteryManager.Instance;
                if (roles != null)
                {
                    roles.roleMastery = group.roleMastery;
                    if (roles.isActiveAndEnabled) roles.RefreshRoleSlotData();
                }
                TalentEnhanceContext.ActiveTeamEnhanceData.UpdateRoleMasteryData(group.roleMastery);
                MainManager.Instance.WindowMessage("分享码已导入。点击保存可保留当前配置。");
            }
            catch (FormatException ex) { MainManager.Instance.WindowMessage("导入失败。" + ex.Message); }
        }

        private static TeamEnhanceConfiguration ReadShareConfiguration(PrincessKnightShareCode data)
        {
            var manager = MainManager.Instance;
            var group = new TeamEnhanceConfiguration();
            for (int i = 0; i < 5; i++)
            {
                if (manager.TalentLevelLimit > 0 && data.Spheres[i] > manager.TalentLevelLimit)
                    throw new FormatException("属性等级超出当前数据库上限。");
                group.talentLevel.Add(i + 1, data.Spheres[i]);
            }
            int page = 0;
            foreach (var pair in data.Nodes)
            {
                if (!manager.TalentSkillNodeDic.TryGetValue(pair.Key, out var node) || pair.Key <= 0 ||
                    node.enhance_level_ids == null || pair.Value > node.enhance_level_ids.Count ||
                    (page != 0 && page != node.page_num))
                    throw new FormatException("天赋节点或等级与当前数据库不符。");
                page = node.page_num;
                group.talentSkill.Add(pair.Key, pair.Value);
            }
            if (data.Master != 0)
            {
                if (!manager.TeamSkillNodeDic.ContainsKey(data.Master))
                    throw new FormatException("当前数据库没有该大师技能节点。");
                group.teamSkill.Add(data.Master, 1);
            }
            for (int i = 0; i < data.Mastery.Length; i++)
            {
                int value = data.Mastery[i];
                if (value == 0) continue;
                int slotLevel = (value - 1) / 6 + 1;
                int enhanceLevel = (value - 1) % 6;
                var entry = manager.UnitRoleMasteryIdDic.Values.FirstOrDefault(e =>
                    e.unit_role_id == i / 4 + 1 && e.slot_id == i % 4 + 1);
                if (entry == null || !manager.UnitRoleMasterySlotEnhanceDataDic.TryGetValue(entry.mastery_id, out var role) ||
                    !role.slot_level_info.TryGetValue(slotLevel, out var info) || !info.enhance_level_info.ContainsKey(enhanceLevel))
                    throw new FormatException("职能专精等级与当前数据库不符。");
                group.roleMastery.Add(entry.mastery_id, (slotLevel, enhanceLevel));
            }
            return group;
        }
#endif
        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

    }
}
