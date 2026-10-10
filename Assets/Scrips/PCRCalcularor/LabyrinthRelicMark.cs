using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCRCaculator.SQL;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class LabyrinthRelicMark : MonoBehaviour
    {
        public GameObject extraDetailPrefab;
        public RectTransform Content;

        private static readonly string[] MarkNames = { "加速", "削弱", "暴击", "强化", "守备" };
        private static readonly string[] MarkColors = { "#36BBCB", "#8D4AAF", "#EFC739", "#C65E29", "#73CC4F" };
        private readonly ExtraEffecDetail[] details = new ExtraEffecDetail[MarkNames.Length];
        private readonly float[] minimumHeights = new float[MarkNames.Length];
        private readonly float[] bottomPaddings = new float[MarkNames.Length];
        private LabyrinthRelicManager sourceManager;

        private void OnEnable()
        {
            Initialize(sourceManager != null ? sourceManager : LabyrinthRelicManager.Instance);
        }

        public void Initialize(LabyrinthRelicManager manager)
        {
            Unsubscribe();
            sourceManager = manager;
            if (isActiveAndEnabled && sourceManager != null)
                sourceManager.MarkCountsChanged += OnMarkCountsChanged;
            Refresh();
        }

        public void Refresh()
        {
            Render(sourceManager?.GetRelicMarkCounts());
        }

        private void OnMarkCountsChanged(IReadOnlyList<int> counts)
        {
            Render(counts);
        }

        private void Render(IReadOnlyList<int> counts)
        {
            if (extraDetailPrefab == null || Content == null)
            {
                Debug.LogWarning("请绑定印记说明的 extraDetailPrefab 和 Content。", this);
                return;
            }

            var template = extraDetailPrefab.GetComponent<ExtraEffecDetail>();
            if (template == null)
            {
                Debug.LogWarning("extraDetailPrefab 需要挂载 ExtraEffecDetail。", this);
                return;
            }

            var effects = MainManager.Instance?.LabyrinthRelicMarkEffectDic;
            var sprites = sourceManager != null ? sourceManager.relicMarkSprites : null;
            for (int i = 0; i < MarkNames.Length; i++)
            {
                if (details[i] == null)
                {
                    details[i] = Instantiate(template, Content, false);
                    ConfigureDetailLayout(i);
                }

                var detail = details[i];
                detail.transform.SetSiblingIndex(i);
                detail.gameObject.SetActive(true);
                if (detail.Icon != null)
                {
                    detail.Icon.sprite = sprites != null && i < sprites.Count ? sprites[i] : null;
                    detail.Icon.enabled = detail.Icon.sprite != null;
                }
                if (detail.nameText != null)
                    detail.nameText.text = MarkNames[i];
                if (detail.describeText != null)
                {
                    detail.describeText.supportRichText = true;
                    int count = counts != null && i < counts.Count ? counts[i] : 0;
                    List<labyrinth_relic_mark_effect> markEffects = null;
                    effects?.TryGetValue(i + 1, out markEffects);
                    detail.describeText.text = BuildDescription(MarkNames[i], MarkColors[i], count, markEffects);
                    ResizeDetail(i);
                }
            }
        }

        private void ConfigureDetailLayout(int index)
        {
            var detail = details[index];
            var row = detail.transform as RectTransform;
            if (row == null || detail.describeText == null || detail.describeText.transform.parent != row)
                return;

            minimumHeights[index] = row.rect.height;
            // 固定在条目顶部，增加正文高度时标题和图标不会随条目中心下移。
            foreach (Transform child in row)
            {
                var rect = child as RectTransform;
                if (rect == null || rect.anchorMin.y != rect.anchorMax.y)
                    continue;

                var position = rect.anchoredPosition;
                position.y -= row.rect.height * (1f - rect.anchorMin.y);
                rect.anchorMin = new Vector2(rect.anchorMin.x, 1f);
                rect.anchorMax = new Vector2(rect.anchorMax.x, 1f);
                rect.anchoredPosition = position;
            }

            var textRect = detail.describeText.rectTransform;
            var textPosition = textRect.anchoredPosition;
            textPosition.y += textRect.rect.height * (1f - textRect.pivot.y);
            textRect.pivot = new Vector2(textRect.pivot.x, 1f);
            textRect.anchoredPosition = textPosition;
            bottomPaddings[index] = Mathf.Max(0f, row.rect.height + textPosition.y - textRect.rect.height);
            detail.describeText.alignment = TextAnchor.UpperLeft;
        }

        private void ResizeDetail(int index)
        {
            if (minimumHeights[index] <= 0f)
                return;

            var detail = details[index];
            var row = detail.transform as RectTransform;
            var textRect = detail.describeText.rectTransform;
            float height = detail.describeText.preferredHeight;
            textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            row.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                Mathf.Max(minimumHeights[index], -textRect.anchoredPosition.y + height + bottomPaddings[index]));
        }

        private static string BuildDescription(
            string markName, string markColor, int count, IEnumerable<labyrinth_relic_mark_effect> effects)
        {
            if (effects == null)
                return string.Empty;

            var text = new StringBuilder();
            foreach (var effect in effects.Where(effect => effect != null).OrderBy(effect => effect.trigger_count))
            {
                string description = (effect.description ?? string.Empty)
                    .Replace("\\n", "\n").Replace("\r\n", "\n").Trim();
                // 有些数据的 description 已包含门槛标题，去掉后统一生成，避免重复显示。
                int lineEnd = description.IndexOf('\n');
                string firstLine = lineEnd >= 0 ? description.Substring(0, lineEnd) : description;
                if (firstLine.StartsWith("【", StringComparison.Ordinal) && firstLine.Contains("以上") &&
                    (firstLine.EndsWith("：", StringComparison.Ordinal) || firstLine.EndsWith(":", StringComparison.Ordinal)))
                {
                    description = lineEnd >= 0 ? description.Substring(lineEnd + 1) : string.Empty;
                }

                string heading = $"【{markName}】{effect.trigger_count}以上：";
                if (text.Length > 0)
                    text.AppendLine();

                string color = count >= effect.trigger_count ? markColor : "#1D1D1D";
                text.Append("<color=").Append(color).Append('>');

                text.Append(heading);
                if (!string.IsNullOrEmpty(description))
                    text.Append('\n').Append(description);

                text.Append("</color>");
            }
            return text.ToString();
        }

        private void Unsubscribe()
        {
            if (sourceManager != null)
                sourceManager.MarkCountsChanged -= OnMarkCountsChanged;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        public void Exit()
        {
            Destroy(gameObject);
        }
    }
}
