using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Elements;
using PCRCaculator.Guild;

public class StatusPanel : MonoBehaviour
{
    public GameObject CellPrefab;

    private List<GameObject> cells = new List<GameObject>();

    void Update()
    {

        if (GuildCalculator.Instance.isFinishCalc)
        {
            return;
        }
        UnitStatus();

    }
    private void UnitStatus()
    {
        var enemyUnits = MyGameCtrl.Instance.enemyUnitCtrl;
        List<PartsData> bossPartsList = UnitCtrl.Instance.BossPartsListForBattle;
        var playerUnits = MyGameCtrl.Instance.playerUnitCtrl;

        ClearCells();

        // 先处理敌方单位
        HandleUnits(enemyUnits);
        if (bossPartsList != null && bossPartsList.Count > 0)
        {
            HandleUnits(bossPartsList);
        }

        // 再处理玩家单位
        HandleUnits(playerUnits);
    }
    private void ClearCells()
    {
        foreach (GameObject cell in cells)
        {
            Destroy(cell);
        }
        cells.Clear();
    }
    private void HandleUnits(List<UnitCtrl> units)
    {
        foreach (var unit in units)
        {
            GridLayoutGroup gridLayout = GetComponentInChildren<GridLayoutGroup>();
            GameObject cell = Instantiate(CellPrefab);
            Text[] cellTexts = cell.GetComponentsInChildren<Text>();
            UpdateCellTexts(unit, cellTexts);
            cell.transform.SetParent(gridLayout.transform, false);
            cells.Add(cell);
        }
    }

    private void HandleUnits(List<PartsData> parts)
    {
        foreach (var part in parts)
        {
            GridLayoutGroup gridLayout = GetComponentInChildren<GridLayoutGroup>();
            GameObject cell = Instantiate(CellPrefab);
            Text[] cellTexts = cell.GetComponentsInChildren<Text>();
            UpdateCellTexts(part, cellTexts);
            cell.transform.SetParent(gridLayout.transform, false);
            cells.Add(cell);
        }
    }
    private void UpdateCellTexts(UnitCtrl unitCtrl, Text[] cellTexts)
    {
        var HpPencent = (float)unitCtrl.Hp != 0 ? string.Format("{0:P2}", (float)unitCtrl.Hp / (float)unitCtrl.MaxHp) : "0.00%";
        string[] properties = new string[]
        {
            unitCtrl.UnitName,
            $"{(float)unitCtrl.Hp}\n<color=#f75291>{HpPencent}</color>",
            $"{(float)unitCtrl.Energy}",
            $"{unitCtrl.EnergyRecoveryRate}",
            $"{(float)unitCtrl.Atk}",
            $"{(float)unitCtrl.MagicStr}",
            $"{(float)unitCtrl.Def}",
            $"{(float)unitCtrl.MagicDef}",
        };

        for (int i = 0; i < cellTexts.Length; i++)
        {
            cellTexts[i].text = properties[i];
        }
    }
    private void UpdateCellTexts(PartsData part, Text[] cellTexts)
    {
        var HpPencent = (float)part.BreakPoint != 0 ? string.Format("{0:P2}", (float)part.BreakPoint / (float)part.MaxBreakPoint) : "0.00%";
        string[] properties = new string[]
        {
            "部位"+part.Index.ToString(),
            $"{(float)part.BreakPoint}\n<color=#f75291>{HpPencent}</color>",
            part.IsBreak?"<color=#f75291>Break</color>":"-",
            "-",
            $"{(float)part.Atk}",
            $"{(float)part.MagicStr}",
            $"{(float)part.Def}",
            $"{(float)part.MagicDef}",
        };

        for (int i = 0; i < cellTexts.Length; i++)
        {
              cellTexts[i].text = properties[i];
        }
    }
}