using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowTextUI : MonoBehaviour
{
    public Text text;
    public void Set(string str)
    {
        text.text = str;
    }
    public void Close()
    {
        Destroy(gameObject);
    }
}
