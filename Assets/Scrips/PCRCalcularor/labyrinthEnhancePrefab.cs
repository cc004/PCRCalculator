using PCRCaculator.SQL;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class labyrinthEnhancePrefab : MonoBehaviour
    {
        public Text enhanceTitle;
        public Image enhanceIcon;
        public Toggle enhanceToggle;
        public int NodeId { get; private set; }
        public event System.Action<int, bool> SelectionChanged;

        private static readonly Color SelectedColor = new Color32(0x79, 0xE0, 0x99, 0xFF);
        private Color originalBackgroundColor;
        private ColorBlock originalToggleColors;
        private bool toggleColorsInitialized;

        private void Awake()
        {
            InitializeToggle();
        }

        private void InitializeToggle()
        {
            if (enhanceToggle == null)
                enhanceToggle = GetComponent<Toggle>();
            if (enhanceToggle == null)
                return;

            // 科技树允许同时选择多个节点。
            enhanceToggle.group = null;
            if (!toggleColorsInitialized && enhanceToggle.targetGraphic != null)
            {
                originalBackgroundColor = enhanceToggle.targetGraphic.color;
                originalToggleColors = enhanceToggle.colors;
                toggleColorsInitialized = true;
            }
        }

        private void OnEnable()
        {
            InitializeToggle();
            if (enhanceToggle == null)
                return;

            enhanceToggle.onValueChanged.AddListener(OnEnhanceToggleChanged);
            UpdateToggleColor(enhanceToggle.isOn);
        }

        private void OnDisable()
        {
            if (enhanceToggle != null)
                enhanceToggle.onValueChanged.RemoveListener(OnEnhanceToggleChanged);
        }

        public void SetSelected(bool isSelected)
        {
            InitializeToggle();
            if (enhanceToggle == null)
                return;

            enhanceToggle.SetIsOnWithoutNotify(isSelected);
            UpdateToggleColor(isSelected);
        }

        private void OnEnhanceToggleChanged(bool isOn)
        {
            UpdateToggleColor(isOn);
            SelectionChanged?.Invoke(NodeId, isOn);
        }

        private void UpdateToggleColor(bool isOn)
        {
            InitializeToggle();
            if (enhanceToggle == null || enhanceToggle.targetGraphic == null)
                return;

            enhanceToggle.targetGraphic.color = isOn ? SelectedColor : originalBackgroundColor;
            if (enhanceToggle.transition == Selectable.Transition.ColorTint)
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
                enhanceToggle.colors = colors;
            }
        }

        public void SetData(labyrinth_enhance_node node)
        {
            NodeId = node?.node_id ?? 0;
            if (enhanceTitle != null)
            {
                var information = MainManager.Instance?.LabyrinthEnhanceInformationDic;
                string title = string.Empty;
                if (node != null && information != null &&
                    information.TryGetValue(node.node_id, out var detail))
                    title = detail?.title ?? string.Empty;

                enhanceTitle.text = title.Replace("\\n", "\n");
            }

            if (enhanceIcon != null)
            {
                enhanceIcon.sprite = node == null
                    ? null
                    : ABExTool.GetSprites(ABExTool.SpriteType.黎明界科技树, node.icon_id);
                enhanceIcon.enabled = enhanceIcon.sprite != null;
            }
        }
    }
}
