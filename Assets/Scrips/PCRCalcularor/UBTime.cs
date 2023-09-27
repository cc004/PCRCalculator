using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{


    public class UBTime : MonoBehaviour
    {
        public InputField inputField;

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
            return inputField.text.Split('\n').Select(float.Parse).ToList();
        }

        public void SetUBTimes(List<float> times)
        {
            inputField.text = string.Join('\n', times);
        }
    }
}