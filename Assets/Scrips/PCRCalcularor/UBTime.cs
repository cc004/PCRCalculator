using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{
    public class UBTime : MonoBehaviour
    {
        public TMP_InputField inputField;
        
        public void StartEdit()
        {
            inputField.interactable = true;
        }
        public void FinishEdit()
        {
            inputField.interactable = false;
        }
        
        public List<float> GetUBTimes()
        {
            SetUBTimes(inputField.text.Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).Select(float.Parse).ToList());
            return inputField.text.Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).Select(float.Parse).ToList();
        }

        public void SetUBTimes(List<float> times)
        {
            for (int i = 0; i < times.Count; i++)
            {
                if (times[i] < 90)
                {
                    int integerPart = (int)times[i];
                    float decimalPart = times[i] - integerPart;
                    times[i] = GuildManager.Instance.SettingData.limitTime * 60 - integerPart * 60 + decimalPart;
                }
            }
          inputField.text = string.Join('\n', times);
        }
    }
}