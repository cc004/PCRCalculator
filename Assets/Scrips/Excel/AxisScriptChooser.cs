#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{
    /// <summary>轴脚本格式选择弹窗：asm（模拟器内置脚本）/ atl（autotimeline + minitouch 的 python 脚本）。</summary>
    public static class AxisScriptChooser
    {
        public const string Asm = "asm";
        public const string Atl = "atl";

        private static GameObject root;

        /// <summary>atl 导出选项（对应网页版的 调试模式 / 偏移量 / 速度 三个开关）。</summary>
        public sealed class AtlExportOptions
        {
            public bool Debug;
            public int OffsetFrames = 30;
            public bool X1;
            public bool X2;
            public bool X4;

            public string[] SpeedModes
            {
                get
                {
                    var modes = new System.Collections.Generic.List<string>();
                    if (X1) modes.Add("x1");
                    if (X2) modes.Add("x2");
                    if (X4) modes.Add("x4");
                    if (modes.Count == 0) modes.Add("x1");
                    return modes.ToArray();
                }
            }
        }

        public static void Show(Transform parent, Font font, Action<string> picked)
        {
            if (parent == null || picked == null) return;
            Close();

            GameObject overlay = CreateOverlay(parent, out Image mask, out Button maskButton);
            maskButton.onClick.AddListener(Close);

            RectTransform dialog = CreateDialog(overlay.transform, new Vector2(460, 264));

            Text title = CreateText("Title", dialog, font, 24);
            title.text = "导出轴脚本";
            Place(title.rectTransform, new Vector2(0, 92), new Vector2(420, 36));

            Text desc = CreateText("Desc", dialog, font, 15);
            desc.text = "asm：战斗内可直接读取执行的轴脚本\natl：autotimeline / minitouch 的 python 脚本";
            desc.color = new Color(0.35f, 0.35f, 0.35f, 1f);
            Place(desc.rectTransform, new Vector2(0, 44), new Vector2(420, 52));

            Button asmButton = CreateButton("asm 脚本", dialog, font,
                new Vector2(-88, -14), new Vector2(170, 52), new Color(0.23f, 0.48f, 0.72f, 1f));
            asmButton.onClick.AddListener(() => Pick(Asm, picked));

            Button atlButton = CreateButton("atl 脚本", dialog, font,
                new Vector2(88, -14), new Vector2(170, 52), new Color(0.23f, 0.48f, 0.72f, 1f));
            atlButton.onClick.AddListener(() => Pick(Atl, picked));

            Button cancelButton = CreateButton("取消", dialog, font,
                new Vector2(0, -98), new Vector2(120, 38), new Color(0.68f, 0.68f, 0.68f, 1f));
            cancelButton.onClick.AddListener(Close);
        }

        /// <summary>atl 二级设置：调试模式 / 偏移量 / 速度选项。</summary>
        public static void ShowAtlOptions(Transform parent, Font font, Action<AtlExportOptions> confirmed)
        {
            if (parent == null || confirmed == null) return;
            Close();

            var options = new AtlExportOptions();

            GameObject overlay = CreateOverlay(parent, out Image mask, out Button maskButton);
            // 三级面板不允许点空白处关闭：实机上很容易点歪误关，只能点「取消」。
            if (maskButton != null)
                maskButton.interactable = false;

            RectTransform dialog = CreateDialog(overlay.transform, new Vector2(500, 470));

            Text title = CreateText("Title", dialog, font, 24);
            title.text = "atl 脚本设置";
            Place(title.rectTransform, new Vector2(0, 200), new Vector2(440, 34));

            // ---------------- 调试模式 ----------------
            Text debugLabel = CreateText("DebugLabel", dialog, font, 18, TextAnchor.MiddleLeft);
            debugLabel.text = "调试模式";
            Place(debugLabel.rectTransform, new Vector2(-140, 140), new Vector2(150, 34));

            Button debugButton = CreateButton("关", dialog, font,
                new Vector2(90, 140), new Vector2(120, 36), new Color(0.72f, 0.72f, 0.72f, 1f));
            Text debugText = debugButton.GetComponentInChildren<Text>();
            debugButton.onClick.AddListener(() =>
            {
                options.Debug = !options.Debug;
                debugText.text = options.Debug ? "开" : "关";
                debugButton.GetComponent<Image>().color = options.Debug
                    ? new Color(0.23f, 0.48f, 0.72f, 1f)
                    : new Color(0.72f, 0.72f, 0.72f, 1f);
            });

            // ---------------- 偏移量 ----------------
            Text offsetLabel = CreateText("OffsetLabel", dialog, font, 18, TextAnchor.MiddleLeft);
            offsetLabel.text = "偏移量（帧）";
            Place(offsetLabel.rectTransform, new Vector2(-140, 84), new Vector2(150, 34));

            Text offsetValue = CreateText("OffsetValue", dialog, font, 20, TextAnchor.MiddleCenter);
            offsetValue.text = options.OffsetFrames.ToString();
            Place(offsetValue.rectTransform, new Vector2(36, 84), new Vector2(64, 34));

            void BumpOffset(int delta)
            {
                options.OffsetFrames = Mathf.Clamp(options.OffsetFrames + delta, 3, 120);
                offsetValue.text = options.OffsetFrames.ToString();
            }

            Button minus5 = CreateButton("-5", dialog, font, new Vector2(-52, 84),
                new Vector2(46, 34), new Color(0.72f, 0.72f, 0.72f, 1f));
            minus5.onClick.AddListener(() => BumpOffset(-5));
            Button minus1 = CreateButton("-1", dialog, font, new Vector2(-2, 84),
                new Vector2(46, 34), new Color(0.72f, 0.72f, 0.72f, 1f));
            minus1.onClick.AddListener(() => BumpOffset(-1));
            Button plus1 = CreateButton("+1", dialog, font, new Vector2(78, 84),
                new Vector2(46, 34), new Color(0.23f, 0.48f, 0.72f, 1f));
            plus1.onClick.AddListener(() => BumpOffset(1));
            Button plus5 = CreateButton("+5", dialog, font, new Vector2(128, 84),
                new Vector2(46, 34), new Color(0.23f, 0.48f, 0.72f, 1f));
            plus5.onClick.AddListener(() => BumpOffset(5));

            // ---------------- 速度选项 ----------------
            Text speedLabel = CreateText("SpeedLabel", dialog, font, 18, TextAnchor.MiddleLeft);
            speedLabel.text = "速度选项";
            Place(speedLabel.rectTransform, new Vector2(-140, 24), new Vector2(150, 34));

            Button x1 = CreateButton("x1", dialog, font, new Vector2(-8, 24),
                new Vector2(70, 36), new Color(0.23f, 0.48f, 0.72f, 1f));
            Button x2 = CreateButton("x2", dialog, font, new Vector2(70, 24),
                new Vector2(70, 36), new Color(0.23f, 0.48f, 0.72f, 1f));
            Button x4 = CreateButton("x4", dialog, font, new Vector2(148, 24),
                new Vector2(70, 36), new Color(0.23f, 0.48f, 0.72f, 1f));

            Text x1Text = x1.GetComponentInChildren<Text>();
            Text x2Text = x2.GetComponentInChildren<Text>();
            Text x4Text = x4.GetComponentInChildren<Text>();

            void RefreshSpeed()
            {
                x1Text.text = options.X1 ? "x1 开" : "x1 关";
                x2Text.text = options.X2 ? "x2 开" : "x2 关";
                x4Text.text = options.X4 ? "x4 开" : "x4 关";
                x1.GetComponent<Image>().color = options.X1
                    ? new Color(0.23f, 0.48f, 0.72f, 1f) : new Color(0.72f, 0.72f, 0.72f, 1f);
                x2.GetComponent<Image>().color = options.X2
                    ? new Color(0.23f, 0.48f, 0.72f, 1f) : new Color(0.72f, 0.72f, 0.72f, 1f);
                x4.GetComponent<Image>().color = options.X4
                    ? new Color(0.23f, 0.48f, 0.72f, 1f) : new Color(0.72f, 0.72f, 0.72f, 1f);
            }

            x1.onClick.AddListener(() => { options.X1 = !options.X1; RefreshSpeed(); });
            x2.onClick.AddListener(() => { options.X2 = !options.X2; RefreshSpeed(); });
            x4.onClick.AddListener(() => { options.X4 = !options.X4; RefreshSpeed(); });
            RefreshSpeed();

            Text hint = CreateText("Hint", dialog, font, 14);
            hint.color = new Color(0.45f, 0.45f, 0.45f, 1f);
            hint.text =
                "每种速度导出一个 .py（取消勾选可少导）\n" +
                "偏移量 = AUTO 的提前帧数，默认 30；容错 ≤3 帧的角色改为直接点击\n" +
                "调试模式会在每次 UB 后校验帧数，不符则点暂停";
            Place(hint.rectTransform, new Vector2(0, -46), new Vector2(440, 76));

            Button confirmButton = CreateButton("导出", dialog, font,
                new Vector2(-70, -150), new Vector2(160, 48), new Color(0.23f, 0.48f, 0.72f, 1f));
            confirmButton.onClick.AddListener(() =>
            {
                AtlExportOptions result = options;
                Close();
                confirmed(result);
            });

            Button cancelButton = CreateButton("取消", dialog, font,
                new Vector2(96, -150), new Vector2(120, 40), new Color(0.68f, 0.68f, 0.68f, 1f));
            cancelButton.onClick.AddListener(Close);
        }

        public static void Close()
        {
            if (root != null) UnityEngine.Object.Destroy(root);
            root = null;
        }

        private static void Pick(string scriptType, Action<string> picked)
        {
            Close();
            picked(scriptType);
        }

        private static GameObject CreateOverlay(Transform parent, out Image mask, out Button maskButton)
        {
            var overlay = new GameObject("AxisScriptChooser", typeof(RectTransform), typeof(Image), typeof(Button));
            overlay.layer = parent.gameObject.layer;
            overlay.transform.SetParent(parent, false);
            root = overlay;

            var overlayRect = (RectTransform)overlay.transform;
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;
            overlayRect.SetAsLastSibling();

            mask = overlay.GetComponent<Image>();
            mask.color = new Color(0f, 0f, 0f, 0.55f);
            maskButton = overlay.GetComponent<Button>();
            maskButton.targetGraphic = mask;
            maskButton.transition = Selectable.Transition.None;
            return overlay;
        }

        private static RectTransform CreateDialog(Transform parent, Vector2 size)
        {
            var dialog = new GameObject("Dialog", typeof(RectTransform), typeof(Image));
            dialog.layer = parent.gameObject.layer;
            dialog.transform.SetParent(parent, false);
            var rect = (RectTransform)dialog.transform;
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = size;
            Image back = dialog.GetComponent<Image>();
            back.color = new Color(0.97f, 0.97f, 0.97f, 1f);
            back.raycastTarget = true;
            return rect;
        }

        private static void Place(RectTransform rect, Vector2 position, Vector2 size)
        {
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
        }

        private static Text CreateText(string name, Transform parent, Font font, int size,
            TextAnchor anchor = TextAnchor.MiddleCenter)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            go.layer = parent.gameObject.layer;
            go.transform.SetParent(parent, false);
            Text text = go.GetComponent<Text>();
            if (font != null) text.font = font;
            text.fontSize = size;
            text.color = new Color(0.196f, 0.196f, 0.196f, 1f);
            text.alignment = anchor;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.raycastTarget = false;
            return text;
        }

        private static Button CreateButton(string label, Transform parent, Font font, Vector2 position, Vector2 size, Color color)
        {
            var go = new GameObject(label, typeof(RectTransform), typeof(Image), typeof(Button));
            go.layer = parent.gameObject.layer;
            go.transform.SetParent(parent, false);
            var rect = (RectTransform)go.transform;
            Place(rect, position, size);

            Image image = go.GetComponent<Image>();
            image.color = color;
            Button button = go.GetComponent<Button>();
            button.targetGraphic = image;

            Text text = CreateText("Label", go.transform, font, 18);
            text.text = label;
            text.color = Color.white;
            RectTransform textRect = text.rectTransform;
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            return button;
        }
    }
}
#endif
