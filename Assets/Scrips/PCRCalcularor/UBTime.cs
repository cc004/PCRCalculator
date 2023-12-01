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
            return inputField.text.Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).Select(float.Parse).ToList();
        }

        public void SetUBTimes(List<float> times)
        {
            inputField.text = string.Join('\n', times);
        }
    }
}