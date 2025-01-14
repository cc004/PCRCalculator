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
        private List<float> ubTimes = new List<float>();
        private List<int> semanUbTimes = new List<int>();
        private bool semanUB = false;
        
        public void SetSemanMode(bool semanUB)
        {
            this.semanUB = semanUB;
            if (semanUB)
            {
                inputField.text = string.Join('\n', semanUbTimes);
            }
            else
            {
                inputField.text = string.Join('\n', ubTimes);
            }
        }

        public void StartEdit()
        {
            inputField.interactable = true;
        }
        public void FinishEdit()
        {
            inputField.interactable = false;
            if (semanUB)
            {
                SetSemanUBTimes(inputField.text.Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToList());
            }
            else
            {
                SetUBTimes(inputField.text.Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).Select(float.Parse).ToList());
            }
        }
        
        public List<float> GetUBTimes()
        {
            return ubTimes;
        }

        public List<int> GetSemanUBTimes()
        {
            return semanUbTimes;
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
            ubTimes = times;
            if (!semanUB)
                inputField.text = string.Join('\n', times);
        }

        public void SetSemanUBTimes(List<int> times)
        {
            semanUbTimes = times;
            if (semanUB)
                inputField.text = string.Join('\n', times);
        }
    }
}