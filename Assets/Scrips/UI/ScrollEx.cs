using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;

public class ScrollEx : MonoBehaviour
{
    public ScrollView targetScrollView;
    public enum ScrollType { typeA = 0,typeB = 1}
    public ScrollType scrollType = ScrollType.typeA;
    public GameObject prefab;
    public int layNum;
    public Vector3 basePos;
    public int addPosWidth;
    public int addPosHight;
    public RectTransform prefabParent;

    private Vector2 parentOriginSize;
    private List<GameObject> prefabs = new List<GameObject>();
    private int prefabCount = 0;
    private bool init = false;
    public IReadOnlyList<GameObject> Prefabs => prefabs;
    public void ClearAll()
    {
        if (!init)
        {
            parentOriginSize = prefabParent.sizeDelta;
            init = true;
        }
        else
        {
            prefabParent.sizeDelta = parentOriginSize;
        }
        foreach (GameObject a in prefabs)
        {
            Destroy(a);
        }
        prefabs.Clear();
        prefabCount = 0;
    }
    public void CreatePrefab(Action<GameObject> onCreate)
    {
        GameObject a = Instantiate(prefab);
        a.transform.SetParent(prefabParent, false);
        int posx, posy;
        if (scrollType == ScrollType.typeA)
        {
            posx = layNum > 1 ? (prefabCount % layNum) : 0;
            posy = Mathf.CeilToInt((prefabCount + 1) / (float)layNum);
        }
        else
        {
            posy = layNum > 1 ? (prefabCount % layNum) : 0;
            posx = Mathf.CeilToInt((prefabCount + 1) / (float)layNum);

        }
        a.transform.localPosition = basePos + new Vector3(posx * addPosWidth, posy * addPosHight, 0);
        prefabs.Add(a);
        prefabCount++;
        onCreate?.Invoke(a);
    }
    public void AutoFit()
    {
        float pos;
        float hight;
        if (scrollType == ScrollType.typeA)
        {
            pos = Mathf.CeilToInt((prefabCount + 1) / (float)layNum) + 0.5f;
            hight = Mathf.Abs(basePos.y + pos * addPosHight);
            if (prefabParent.sizeDelta.y < hight)
            {
                prefabParent.sizeDelta = new Vector2(prefabParent.sizeDelta.x, hight);
            }
        }
        else
        {
            pos = Mathf.CeilToInt((prefabCount + 1) / (float)layNum) + 0.5f;
            hight = Mathf.Abs(basePos.x + pos * addPosWidth);
            if (prefabParent.sizeDelta.x < hight)
            {
                prefabParent.sizeDelta = new Vector2(hight,parentOriginSize.y);
            }

        }
    }
}
