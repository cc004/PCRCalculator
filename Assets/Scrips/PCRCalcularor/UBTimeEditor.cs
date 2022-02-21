using System.Collections.Generic;
using Elements;
using PCRCaculator.Guild;
using UnityEngine;

namespace PCRCaculator
{
    public class UBTimeEditor : MonoBehaviour
    {
        public List<UBTime> UnitUBTimes;

        private GuildPlayerGroupData groupData;

        public void Init(GuildPlayerGroupData data)
        {
            groupData = data;
            Set(data.UBExecTimeData);
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
            MainManager.Instance.WindowConfigMessage("是否将预设阵容的UB时间改为当前的UB时间？", ReplaceUBTime_0);
        }
        private void ReplaceUBTime_0()
        {
            Set(GuildCalculator.Instance.CreateUBExecTimeData());
        }
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
