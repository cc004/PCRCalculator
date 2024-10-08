﻿using System.Collections.Generic;
using Elements;
using PCRCaculator.Guild;
using UnityEngine;
using UnityEngine.UI;
namespace PCRCaculator
{
    public class UBTimeEditor : MonoBehaviour
    {
        public List<UBTime> UnitUBTimes;

        private GuildPlayerGroupData groupData;
        public List<Text> characterTexts;

        public void Init(GuildPlayerGroupData data)
        {
            groupData = data;
            Set(data.UBExecTimeData);
            var playerUnits = MyGameCtrl.Instance.playerUnitCtrl;
            var i=1;
            foreach (var unit in playerUnits)
            {
                characterTexts[i].text = unit.UnitName;
                i++;
                if (i > 5) return;
            }

    }
        public void Save()
        {
            groupData.UBExecTimeData.Clear();
            for (int i = 0; i < 6; i++)
            {
                groupData.UBExecTimeData.Add(UnitUBTimes[i].GetUBTimes());
                UnitUBTimes[i].FinishEdit();
            }
            GuildManager.SaveSettingData(MyGameCtrl.Instance.tempData.SettingData);
            Exit();
        }
        public void OverRideButton()
        {
            ReplaceUBTime();
        }
        public void ReplaceUBTime()
        {
            Set(GuildCalculator.Instance.CreateUBExecTimeData());
            MainManager.Instance.WindowMessage("已覆盖为此次战斗UB时间");
        }
        // private void ReplaceUBTime_0()
        // {
        //     Set(GuildCalculator.Instance.CreateUBExecTimeData());
        // }
        public void Set(List<List<float>> list)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i < list.Count)
                {
                    UnitUBTimes[i].SetUBTimes(list[i]);
                    UnitUBTimes[i].StartEdit();
                }
                else
                {
                    UnitUBTimes[i].SetUBTimes(new List<float>());
                }
            }
        }
        public void Exit()
        {
            Destroy(gameObject);
        }
    }
}
