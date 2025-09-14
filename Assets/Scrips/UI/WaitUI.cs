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
    }

    /// <summary>
    /// 设置加载面板的提示文字
    /// </summary>
    public void SetMessage(string msg)
    {
        if (text != null)
        {
            baseMessage = msg;
            text.alignment = TextAnchor.MiddleCenter; // 保证文本居中
            currentDots = 0;
            dotTimer = 0f;
            text.text = baseMessage;
        }
    }

    /// <summary>
    /// 设置 0/1 指示（例如：0 = 未完成，1 = 已完成）
    /// </summary>
    // SetIndicator 已移除，使用纯文字更新。

    void Start()
    {
        if (text != null)
        {
            // 确保默认居中
            text.alignment = TextAnchor.MiddleCenter;
            text.text = baseMessage;
            // 强制调整 RectTransform 到中间，防止 prefab 中位置错误
            var rt = text.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchorMin = new Vector2(0.5f, 0.5f);
                rt.anchorMax = new Vector2(0.5f, 0.5f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                rt.anchoredPosition = Vector2.zero;
            }
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


}
