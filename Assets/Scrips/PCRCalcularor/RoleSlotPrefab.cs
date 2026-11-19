using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using PCRCaculator.SQL;
using PCRCaculator.Guild;
using System.Linq;
using System;

namespace PCRCaculator
{
    public class RoleSlotPrefab : MonoBehaviour
    {
        public Slider roleSlotLevelSlider;
        public Slider roleSlotStarSlider;
        public Text roleSlotLevelText;
        public Text roleSlotStarText;
        public Text roleSlotNameText;
        public Text roleSlotValueText;
        public Image roleSlotIcon;
        private int masteryId;
        public int MasteryId => masteryId;
        private Dictionary<int, UnitRoleMasterySlotEnhanceData> UnitRoleMasterySlotEnhanceDataDic => MainManager.Instance.UnitRoleMasterySlotEnhanceDataDic;

        public void OnLevelSliderValueChanged()
        {
            int level = Mathf.RoundToInt(roleSlotLevelSlider.value);
            roleSlotLevelText.text = level.ToString();

            // 当等级为0时，星级必须为0
            if (level == 0)
            {
                roleSlotStarSlider.value = 0;
                roleSlotStarText.text = "0";
            }

            UpdateRoleMasteryData(level, null, true);
            RoleMasteryManager.Instance?.UpdateRoleMasteryData();
        }

        public void OnStarSliderValueChanged()
        {
            int level = roleSlotLevelSlider != null ? Mathf.RoundToInt(roleSlotLevelSlider.value) : 0;
            int star = Mathf.RoundToInt(roleSlotStarSlider.value);

            // 当等级为0时，星级不能增加
            if (level == 0 && star > 0)
            {
                star = 0;
                roleSlotStarSlider.value = 0;
            }

            roleSlotStarText.text = star.ToString();
            UpdateRoleMasteryData(null, star, true);
            RoleMasteryManager.Instance?.UpdateRoleMasteryData();
        }

        private void UpdateRoleMasteryData(int? level, int? star, bool isUIOperation = false)
        {
            var playerGroup = TalentEnhanceContext.ActiveConfiguration;
            if (playerGroup?.roleMastery == null) return;

            if (!playerGroup.roleMastery.ContainsKey(masteryId))
            {
                playerGroup.roleMastery[masteryId] = (0, 0);
            }

            var (slotLevel, enhanceLevel) = playerGroup.roleMastery[masteryId];
            int newSlotLevel = level ?? slotLevel;
            int newEnhanceLevel = star ?? enhanceLevel;

            // 只有在UI操作且等级变为0时，才强制星级为0
            if (isUIOperation && newSlotLevel == 0)
            {
                newEnhanceLevel = 0;
            }

            playerGroup.roleMastery[masteryId] = (newSlotLevel, newEnhanceLevel);
            RefreshDisplayData(newSlotLevel, newEnhanceLevel);
        }

        private void RefreshDisplayData(int level, int star)
        {
            if (!UnitRoleMasterySlotEnhanceDataDic.TryGetValue(masteryId, out var slotEnhanceData)) return;

            // 等级/星级上限改成从数据库读取：
            // 等级上限 = 该槽位 slot_level_info 的最大键；星级上限 = 当前等级下 enhance_level_info 的最大键。
            int maxSlotLevel = slotEnhanceData.slot_level_info != null && slotEnhanceData.slot_level_info.Count > 0
                ? slotEnhanceData.slot_level_info.Keys.Max()
                : level;
            level = Mathf.Clamp(level, 0, Mathf.Max(0, maxSlotLevel));

            int maxEnhanceLevel = 0;
            if (level > 0 && slotEnhanceData.slot_level_info != null &&
                slotEnhanceData.slot_level_info.TryGetValue(level, out var selectedLevelInfo) &&
                selectedLevelInfo?.enhance_level_info != null && selectedLevelInfo.enhance_level_info.Count > 0)
            {
                maxEnhanceLevel = selectedLevelInfo.enhance_level_info.Keys.Max();
            }

            star = Mathf.Clamp(star, 0, Mathf.Max(0, maxEnhanceLevel));
            if (roleSlotLevelSlider != null) roleSlotLevelSlider.maxValue = Mathf.Max(0, maxSlotLevel);
            if (roleSlotStarSlider != null) roleSlotStarSlider.maxValue = Mathf.Max(0, maxEnhanceLevel);

            // 获取图标ID和名称
            int iconId = 0;
            string name = "";

            if (slotEnhanceData.slot_level_info?.ContainsKey(level) == true)
            {
                var slotLevelInfo = slotEnhanceData.slot_level_info[level];
                iconId = slotLevelInfo.icon_id;
                name = slotLevelInfo.name;
            }
            else if (slotEnhanceData.slot_level_info?.Count > 0)
            {
                // 如果找不到对应level的数据，使用第一个slot_level的数据
                var firstSlotLevelInfo = slotEnhanceData.slot_level_info.First().Value;
                iconId = firstSlotLevelInfo.icon_id;
                name = firstSlotLevelInfo.name;
            }

            string iconName = level > 0 ? $"icon_item_{iconId:D5}" : $"icon_item_invalid_{iconId:D5}";
            Sprite icon = Resources.Load<Sprite>($"item/{iconName}");
            if (icon == null && iconId > 0)
            {
                // 职能精通 6 级以上的图标没有打进 Resources（只到 5 级），改为从 AB 包动态取
                icon = ABExTool.GetSprites(
                    level > 0 ? ABExTool.SpriteType.道具图标 : ABExTool.SpriteType.道具图标无效,
                    iconId);
            }

            if (icon == null && level > 5 && iconId > 0)
            {
                // 当前角色数据版本对应的 AB manifest 里可能还没有 6 级以上的图标（旧版本资源包），
                // 退回同系列 5 级图标占位；把角色数据版本更新到最新后即可显示真实图标。
                int fallbackIconId = iconId / 100 * 100 + 5;
                if (fallbackIconId != iconId)
                {
                    icon = ABExTool.GetSprites(ABExTool.SpriteType.道具图标, fallbackIconId);
                }
            }

            // 查找对应等级和星级的增强数据
            unit_role_mastery_enhance_data enhanceData = null;
            if (slotEnhanceData.slot_level_info?.ContainsKey(level) == true &&
                slotEnhanceData.slot_level_info[level].enhance_level_info?.ContainsKey(star) == true)
            {
                var enhanceInfo = slotEnhanceData.slot_level_info[level].enhance_level_info[star];
                enhanceData = new unit_role_mastery_enhance_data
                {
                    role_param_type_1 = enhanceInfo.role_param_type_1,
                    role_param_type_2 = enhanceInfo.role_param_type_2,
                    enhance_value_1 = enhanceInfo.enhance_value_1,
                    enhance_value_2 = enhanceInfo.enhance_value_2
                };
            }
            else
            {
                var enhanceInfo = slotEnhanceData.slot_level_info[1].enhance_level_info[0];
                enhanceData = new unit_role_mastery_enhance_data
                {
                    role_param_type_1 = enhanceInfo.role_param_type_1,
                    role_param_type_2 = enhanceInfo.role_param_type_2,
                    enhance_value_1 = 0,
                    enhance_value_2 = 0
                };
            }

            // 获取强化数值并更新UI显示
            int enhanceValue = enhanceData?.enhance_value_1 ?? 0;
            float value = GetFormatEnhanceValue(enhanceData?.role_param_type_1 ?? 0, enhanceValue);

            roleSlotNameText.text = name;
            roleSlotValueText.text = enhanceData?.role_param_type_1 > 7 ? $"{value}" : $"{value}%" ;
            roleSlotLevelText.text = level.ToString();
            roleSlotStarText.text = star.ToString();
            roleSlotIcon.sprite = icon;

            roleSlotLevelSlider.SetValueWithoutNotify(level);
            roleSlotStarSlider.SetValueWithoutNotify(star);
        }

        public void SetRoleSlotData(int level, int star, int masteryId)
        {
            this.masteryId = masteryId;

            var playerGroup = TalentEnhanceContext.ActiveConfiguration;
            if (playerGroup?.roleMastery != null)
            {
                playerGroup.roleMastery[masteryId] = (level, star);
            }

            RefreshDisplayData(level, star);
        }

        public static float GetFormatEnhanceValue(int roleParamType, int enhanceValue)
        {
            double divisor = roleParamType == 14 ? 10.0 : 100.0;
            double value = enhanceValue / divisor;

            // 先保留一位小数（乘10再舍入）
            double temp = value * 10.0;

            // 拆分整数与小数部分
            double integerPart = Math.Floor(temp);
            double fractionalPart = temp - integerPart;

            double rounded;

            // 银行家舍入（round half to even）
            if (fractionalPart == 0.5 || fractionalPart == -0.5)
            {
                // 如果是 .5，则判断整数部分是奇还是偶
                if (((long)integerPart & 1) != 0)
                    rounded = integerPart + Math.Sign(fractionalPart); // 奇数 → 进位
                else
                    rounded = integerPart; // 偶数 → 不进位
            }
            else
            {
                // 普通四舍五入
                rounded = Math.Floor(temp + 0.5 * Math.Sign(temp));
            }

            // 恢复到一位小数
            return (float)(rounded / 10.0);
        }
    }
}
