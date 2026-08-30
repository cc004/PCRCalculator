using System.Collections.Generic;
using Elements;
using Elements.Battle;
using PCRCaculator.Guild;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
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
            var i = 1;
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
                UnitUBTimes[i].FinishEdit();
                groupData.UBExecTimeData.Add(UnitUBTimes[i].GetUBTimes());
            }
            GuildManager.SaveSettingData(MyGameCtrl.Instance.tempData.SettingData);
            Exit();
        }
        public void OverRideButton()
        {
            ReplaceUBTime();
        }
        public void Auto2SemanButton()
        {
            if (MyGameCtrl.Instance.IsSemanMode)
            {
                MainManager.Instance.WindowMessage("语义UB模式下无法转换成语义UB");
                return;
            }
            groupData.SemanUBExecTimeData = BattleManager.Instance.semanubmanager.Auto2Seman();
            groupData.useSequenceUB = MyGameCtrl.Instance.tempData.useSequenceUB;
            if (groupData.useSequenceUB)
            {
                groupData.SemanUBSequenceData = BattleManager.Instance.semanubmanager.Auto2SemanSequence();
                MainManager.Instance.WindowMessage("已覆盖为此次战斗序列语义UB时间");
            }
            else
            {
                groupData.SemanUBSequenceData = new List<int>();
                MainManager.Instance.WindowMessage("已覆盖为此次战斗语义UB时间");
            }
            GuildManager.SaveSettingData(MyGameCtrl.Instance.tempData.SettingData);
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
        public void AddUBTime(int i)
        {
            float max_value = UnitUBTimes.SelectMany(ubTime => ubTime.inputField.text.Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).Select(float.Parse).DefaultIfEmpty(100)).Max();
            UnitUBTimes[i].inputField.text += $"\n{max_value + 0.001:F3}";
        }
        public void BatchRemoveUBTime()
        {
            MainManager.Instance.WindowInputMessage("清除该帧之后的所有UB\n（包含该帧，默认为空即清空所有UB）", BatchRemove);
        }
        public void BatchRemove(string frame)
        {
            if (string.IsNullOrWhiteSpace(frame))
            {
                foreach (var ubTime in UnitUBTimes)
                    ubTime.inputField.text = string.Empty;
                return;
            }

            if (!float.TryParse(frame, out float targetFrame))
            {
                MainManager.Instance.WindowMessage("请输入有效的数字作为帧值");
                return;
            }

            foreach (var ubTime in UnitUBTimes)
            {
                var values = ubTime.inputField.text
                    .Split('\n')
                    .Where(s => !string.IsNullOrWhiteSpace(s) && float.TryParse(s, out _))
                    .Select(float.Parse)
                    .Where(v => v < targetFrame);

                ubTime.inputField.text = string.Join("\n", values);
            }
        }
    }
}
