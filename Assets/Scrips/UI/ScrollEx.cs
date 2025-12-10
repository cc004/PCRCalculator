using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollEx : MonoBehaviour
{
    public ScrollView targetScrollView;
    public enum ScrollType { typeA = 0, typeB = 1 }
    public ScrollType scrollType = ScrollType.typeA;
    public GameObject prefab;
    public int layNum;
    public Vector3 basePos;
    public int addPosWidth;
    public int addPosHight;
    public RectTransform prefabParent;

    private Vector2 parentOriginSize;
    private List<GameObject> prefabs = new List<GameObject>();
    private int prefabCount;
    private bool init;
    public IReadOnlyList<GameObject> Prefabs => prefabs;
    private bool virtualized;
    private int itemCount;
    private int totalRows;
    private int visibleRows;
    private int startIndex;
    private float viewportHeight;
    private Action<int, GameObject> onBindVirtualized;
    private int bufferRows;

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
                prefabParent.sizeDelta = new Vector2(hight, parentOriginSize.y);
            }
        }
    }

    public void EnableVirtualization(int itemCount, Action<int, GameObject> onBind, int bufferRows = 2)
    {
        ClearAll();
        virtualized = true;
        this.itemCount = itemCount;
        this.bufferRows = bufferRows; // 存储缓冲行数
        onBindVirtualized = onBind;

        var viewport = prefabParent.parent as RectTransform;
        viewportHeight = viewport != null ? viewport.rect.height : Screen.height;

        totalRows = Mathf.CeilToInt(itemCount / (float)layNum);

        // 计算可视行数（不加缓冲）
        int visibleRowsWithoutBuffer = Mathf.Max(1, Mathf.CeilToInt(viewportHeight / addPosHight));
        visibleRows = visibleRowsWithoutBuffer; // 存储可视行数（不加缓冲）

        startIndex = 0;

        // 设置内容总高度
        float height = Mathf.Abs(basePos.y + (totalRows + 1f) * addPosHight);
        if (prefabParent.sizeDelta.y < height)
        {
            prefabParent.sizeDelta = new Vector2(prefabParent.sizeDelta.x, height);
        }

        int totalRowsToCreate = visibleRowsWithoutBuffer + 2 * bufferRows; // 上下各加缓冲行
        int itemsToCreate = totalRowsToCreate * layNum;

        itemsToCreate = Mathf.Max(itemsToCreate, layNum);

        itemsToCreate = Mathf.Min(itemsToCreate, itemCount);

        for (int i = 0; i < itemsToCreate; i++)
        {
            CreatePrefab(go => { });
        }

        RebindRange(startIndex);
    }

    private void RebindRange(int startIndex)
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            int globalIndex = startIndex + i;
            int posx = layNum > 1 ? (globalIndex % layNum) : 0;
            int posy = Mathf.CeilToInt((globalIndex + 1) / (float)layNum);
            prefabs[i].transform.localPosition = basePos + new Vector3(posx * addPosWidth, posy * addPosHight, 0);

            if (globalIndex >= 0 && globalIndex < itemCount)
            {
                prefabs[i].SetActive(true);
                onBindVirtualized?.Invoke(globalIndex, prefabs[i]);
            }
            else
            {
                prefabs[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (!virtualized || scrollType != ScrollType.typeA) return;

        if (prefabs.Count == 0) return;

        float scrollTop = -prefabParent.anchoredPosition.y;

        float relativeScrollTop = scrollTop - basePos.y;

        int newFirstRow = Mathf.FloorToInt(relativeScrollTop / addPosHight);

        newFirstRow = Mathf.Max(0, newFirstRow);
        newFirstRow = Mathf.Min(newFirstRow, totalRows - 1);

        int bufferedFirstRow = Mathf.Max(0, newFirstRow - bufferRows);
        int newStartIndex = bufferedFirstRow * layNum;

        int maxStartIndex = Mathf.Max(0, itemCount - prefabs.Count);
        newStartIndex = Mathf.Clamp(newStartIndex, 0, maxStartIndex);

        if (newStartIndex == startIndex) return;

        int delta = newStartIndex - startIndex;

        if (Mathf.Abs(delta) >= prefabs.Count * 0.8f)
        {
            startIndex = newStartIndex;
            RebindRange(startIndex);
            return;
        }

        if (delta > 0)
        {
            for (int k = 0; k < delta; k++)
            {
                var go = prefabs[0];
                prefabs.RemoveAt(0);
                prefabs.Add(go);
            }
        }
        else if (delta < 0)
        {
            for (int k = 0; k < -delta; k++)
            {
                var go = prefabs[prefabs.Count - 1];
                prefabs.RemoveAt(prefabs.Count - 1);
                prefabs.Insert(0, go);
            }
        }

        startIndex = newStartIndex;
        RebindRange(startIndex);
    }
}