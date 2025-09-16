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
        // ellipsis animation
        if (text != null && maxDots > 0 && dotInterval > 0)
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
