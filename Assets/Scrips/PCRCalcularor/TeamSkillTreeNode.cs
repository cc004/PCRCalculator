using UnityEngine;
using UnityEngine.UI;
namespace PCRCaculator
{
    public class TeamSkillTreeNode : MonoBehaviour
    {
        public Image skillIcon;
        public Image backgroundImage;
        public Text levelText;
        public Text talentText;
        public Text titleText;
        public Text valueText;

        public Slider enhanceSlider;

        private int nodeId;
        private TeamSkillTreeManager treeManager;
        private bool isUpdatingSlider = false;
        private void Start()
        {
            treeManager = FindObjectOfType<TeamSkillTreeManager>();
            if (enhanceSlider != null)
                enhanceSlider.onValueChanged.AddListener(OnEnhanceValueChanged);
        }
        public void SetNodeData(int currentLevel, int maxLevel, string value)
        {
            isUpdatingSlider = true;
            try
            {
                valueText.text = value;
                levelText.text = $"{currentLevel}/{maxLevel}";
                enhanceSlider.maxValue = maxLevel;
                enhanceSlider.value = currentLevel;
            }
            finally
            {
                isUpdatingSlider = false;
            }
        }

        public void SetNodeData(int nodeId, Sprite icon, int currentLevel, int maxLevel, string talent, string skill, string value)
        {
            this.nodeId = nodeId;

            // 设置技能图标
            if (skillIcon != null && icon != null)
                skillIcon.sprite = icon;

            // 设置等级文本
            levelText.text = $"{currentLevel}/{maxLevel}";
            talentText.text = talent;
            titleText.text = skill;
            valueText.text = value;
            // 设置 Slider 最大值和当前值
            if (enhanceSlider != null)
            {
                enhanceSlider.maxValue = maxLevel;
                enhanceSlider.value = currentLevel;
            }
        }

        private void OnEnhanceValueChanged(float value)
        {
            if (isUpdatingSlider) return;

            int newLevel = (int)value;

            if (newLevel == treeManager.GetSkillLevel(nodeId)) return;

            treeManager.EnhanceSkill(nodeId, newLevel);
        }
        
        public void UpdateUIState(bool isUnlocked, bool isLearned)
        {
            // 设置图标颜色
            if (skillIcon != null)
                skillIcon.color = isLearned ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1f);

            // 设置背景图片颜色
            if (backgroundImage != null)
            {
                if (isLearned)
                    backgroundImage.color = Color.white; // 激活状态
                else if (isUnlocked)
                    backgroundImage.color = new Color(0.8f, 0.8f, 0.8f, 1f); // 可学习状态
                else
                    backgroundImage.color = new Color(0.5f, 0.5f, 0.5f, 1f); // 锁定状态
            }
        }
    }
}
