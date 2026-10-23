using PCRCaculator.SQL;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class LabyrinthRelicSoltPrefab : MonoBehaviour
    {
        public Text relicNameText;
        public Text relicEffectText;
        public Text relicmarkText;
        public Image relicIcon;
        public Toggle relicToggle;
        public int RelicId { get; private set; }
        public event System.Action<int, bool> SelectionChanged;

        public void SetSelected(bool isSelected)
        {
            if (relicToggle == null)
                return;

            InitializeToggleColors();
            relicToggle.SetIsOnWithoutNotify(isSelected);
            UpdateToggleColor(relicToggle.isOn);
        }

        private static readonly Color SelectedColor = new Color32(0x79, 0xE0, 0x99, 0xFF);
        private Color originalBackgroundColor;
        private ColorBlock originalToggleColors;
        private bool toggleColorsInitialized;

        private void Awake()
        {
            InitializeToggleColors();
        }

        private void InitializeToggleColors()
        {
            if (!toggleColorsInitialized && relicToggle != null && relicToggle.targetGraphic != null)
            {
                originalBackgroundColor = relicToggle.targetGraphic.color;
                originalToggleColors = relicToggle.colors;
                toggleColorsInitialized = true;
            }
        }

        private void OnEnable()
        {
            if (relicToggle == null)
                return;

            relicToggle.onValueChanged.AddListener(OnRelicToggleChanged);
            UpdateToggleColor(relicToggle.isOn);
        }

        private void OnDisable()
        {
            if (relicToggle != null)
                relicToggle.onValueChanged.RemoveListener(OnRelicToggleChanged);
        }

        private void OnRelicToggleChanged(bool isOn)
        {
            UpdateToggleColor(isOn);
            SelectionChanged?.Invoke(RelicId, isOn);
        }

        private void UpdateToggleColor(bool isOn)
        {
            if (relicToggle.targetGraphic == null)
                return;

            InitializeToggleColors();
            relicToggle.targetGraphic.color = isOn ? SelectedColor : originalBackgroundColor;

            // 选中时保持指定颜色，避免 Toggle 的悬停、按下和焦点颜色将背景染暗。
            if (relicToggle.transition == Selectable.Transition.ColorTint)
            {
                var colors = originalToggleColors;
                if (isOn)
                {
                    colors.normalColor = Color.white;
                    colors.highlightedColor = Color.white;
                    colors.pressedColor = Color.white;
                    colors.selectedColor = Color.white;
                    colors.disabledColor = Color.white;
                    colors.colorMultiplier = 1f;
                    colors.fadeDuration = 0f;
                }
                relicToggle.colors = colors;
            }
        }

        public void SetData(labyrinth_relic relic)
        {
            RelicId = relic?.relic_id ?? 0;

            if (relicNameText != null)
                relicNameText.text = relic?.name ?? string.Empty;

            if (relicEffectText != null)
                relicEffectText.text = (relic?.effect_description ?? string.Empty).Replace("\\n", "\n");

            if (relicmarkText != null)
            {
                string markName = relic?.relic_mark_id switch
                {
                    1 => "加速",
                    2 => "削弱",
                    3 => "暴击",
                    4 => "强化",
                    5 => "守备",
                    _ => string.Empty
                };
                relicmarkText.text = string.IsNullOrEmpty(markName)
                    ? string.Empty
                    : $"【{markName}+{relic.relic_mark_count}】";
            }

            if (relicIcon != null)
            {
                relicIcon.sprite = relic == null
                    ? null
                    : ABExTool.GetSprites(ABExTool.SpriteType.黎明界遗物, relic.relic_id);
                relicIcon.enabled = relicIcon.sprite != null;
            }
        }
    }
}
