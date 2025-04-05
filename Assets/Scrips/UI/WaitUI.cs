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

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Close()
    {
        Destroy(gameObject);
    }


    void Update()
    {
        // if (m_CurProgressValue2 < m_ProgressValue2)
        // {
        //     m_CurProgressValue2++;
        // }
        // if (m_CurProgressValue2 == 100)
        // {
        //     m_CurProgressValue2 = 0;
        // }
        // m_CurProgressValueTemp = m_CurProgressValue2 / 100f;
        // if (m_CurProgressValueTemp > 0.1f && m_CurProgressValueTemp <= 0.3f)
        // {
        //     text.text = "??????";
        // }
        // else if (m_CurProgressValueTemp > 0.3f && m_CurProgressValueTemp <= 0.5f)
        // {
        //     text.text = "??????";
        // }
        // else if (m_CurProgressValueTemp > 0.5f && m_CurProgressValueTemp <= 0.7f)
        // {
        //     text.text = "??????";
        // }
        // else if (m_CurProgressValueTemp > 0.7f && m_CurProgressValueTemp <= 0.9f)
        // {
        //     text.text = "??????";
        // }
        // else
        // {
        //     text.text = "??????";
        // }
    }
}
