using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitUI : MonoBehaviour
{
    public Text text;
    float m_CurProgressValue2 = 0;
    float m_ProgressValue2 = 100;
    float m_CurProgressValueTemp = 0;
    [Header("Ellipsis")]
    public int maxDots = 3; // 最大点数，例如 3 -> ...
    public float dotInterval = 0.5f; // 每次点数变化时间
    private float dotTimer = 0f;
    private int currentDots = 0;
    private string baseMessage = "请稍候";

    private RectTransform progressRoot;
    private RectTransform progressFillRect;
    private bool progressActive;
    private string progressCaption;
    private double progressFraction;
    private string progressDetail;

    public void Show()
    {
        gameObject.SetActive(true);
        MatchTextRectToParent();
    }

    /// <summary>
    /// 设置加载面板的提示文字
    /// </summary>
    public void SetMessage(string msg)
    {
        if (text != null)
        {
            baseMessage = msg;
            currentDots = 0;
            dotTimer = 0f;
            text.text = baseMessage;
            MatchTextRectToParent();
        }
    }

    /// <summary>
    /// 显示进度条并更新文字（会在提示文字后面追加「12/34 (35%)」）。
    /// done/total 均为 0 时只显示进度条不显示数字，避免总数为 0 时出现 NaN。
    /// </summary>
    public void SetProgress(int done, int total, string caption = null)
    {
        var fraction = total > 0 ? (double)done / total : 0d;
        SetProgress(fraction, caption, total > 0 ? $"{done}/{total}" : null);
    }

    /// <summary>按 0~1 的比例更新进度条。</summary>
    public void SetProgress(double fraction, string caption = null, string detail = null)
    {
        EnsureProgressBar();
        progressActive = true;
        progressCaption = caption;
        progressFraction = double.IsNaN(fraction) ? 0d : System.Math.Max(0d, System.Math.Min(1d, fraction));
        progressDetail = detail;

        if (progressFillRect != null)
            progressFillRect.anchorMax = new Vector2((float)progressFraction, 1f);

        RefreshProgressText();
    }

    /// <summary>隐藏进度条，回到「文字 + 省略号」的原始样式。</summary>
    public void ClearProgress()
    {
        progressActive = false;
        progressCaption = null;
        progressDetail = null;
        progressFraction = 0d;
        if (progressFillRect != null)
            progressFillRect.anchorMax = new Vector2(0f, 1f);
        if (progressRoot != null)
            progressRoot.gameObject.SetActive(false);
        if (text != null)
            text.text = baseMessage;
    }

    private void RefreshProgressText()
    {
        if (text == null)
            return;

        var percent = (int)System.Math.Round(progressFraction * 100d);
        var head = string.IsNullOrEmpty(progressCaption) ? baseMessage : progressCaption;
        var tail = string.IsNullOrEmpty(progressDetail)
            ? $"({percent}%)"
            : $"{progressDetail} ({percent}%)";
        text.text = $"{head} {tail}";
    }

    private void EnsureProgressBar()
    {
        if (progressFillRect != null)
        {
            if (progressRoot != null && !progressRoot.gameObject.activeSelf)
                progressRoot.gameObject.SetActive(true);
            return;
        }

        progressRoot = new GameObject("ProgressRoot", typeof(RectTransform)).GetComponent<RectTransform>();
        progressRoot.SetParent(transform, false);
        progressRoot.anchorMin = new Vector2(0.12f, 0.16f);
        progressRoot.anchorMax = new Vector2(0.88f, 0.16f);
        progressRoot.offsetMin = new Vector2(0f, -8f);
        progressRoot.offsetMax = new Vector2(0f, 8f);

        CreateProgressImage(progressRoot, "Background", new Color(1f, 1f, 1f, 0.15f));

        var fill = CreateProgressImage(progressRoot, "Fill", new Color(0.94f, 0.55f, 0.7f, 0.95f));
        progressFillRect = fill.rectTransform;
        // 进度用 anchorMax.x 表示：0 全空、1 全满，不需要额外的 Slider 组件
        progressFillRect.anchorMin = new Vector2(0f, 0f);
        progressFillRect.anchorMax = new Vector2(0f, 1f);
        progressFillRect.offsetMin = new Vector2(2f, 2f);
        progressFillRect.offsetMax = new Vector2(-2f, -2f);
    }

    private static Image CreateProgressImage(Transform parent, string name, Color color)
    {
        var child = new GameObject(name, typeof(RectTransform), typeof(Image));
        child.transform.SetParent(parent, false);
        var rect = (RectTransform)child.transform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        var image = child.GetComponent<Image>();
        image.color = color;
        image.raycastTarget = false;
        return image;
    }

    void Start()
    {
        if (text != null)
        {
            text.text = baseMessage;
            MatchTextRectToParent();
        }
        dotTimer = 0f;
        currentDots = 0;
    }

    void Update()
    {
        // ellipsis animation（进度条显示时文字由 SetProgress 直接驱动，不再加省略号）
        if (!progressActive && text != null && maxDots > 0 && dotInterval > 0)
        {
            dotTimer += Time.unscaledDeltaTime;
            if (dotTimer >= dotInterval)
            {
                dotTimer = 0f;
                currentDots = (currentDots + 1) % (maxDots + 1);
                string dots = new string('.', currentDots);
                text.text = baseMessage + dots;
            }
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Close()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 让 text 的 RectTransform 和父物体一样大
    /// </summary>
    private void MatchTextRectToParent()
    {
        if (text != null && text.transform.parent != null)
        {
            RectTransform textRect = text.GetComponent<RectTransform>();
            RectTransform parentRect = text.transform.parent.GetComponent<RectTransform>();
            if (textRect != null && parentRect != null)
            {
                textRect.anchorMin = Vector2.zero;
                textRect.anchorMax = Vector2.one;
                textRect.offsetMin = Vector2.zero;
                textRect.offsetMax = Vector2.zero;
                textRect.pivot = parentRect.pivot;
            }
        }
    }
}
