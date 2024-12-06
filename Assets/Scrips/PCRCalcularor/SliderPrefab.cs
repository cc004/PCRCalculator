using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace PCRCaculator
{
    public class SliderPrefab : MonoBehaviour
    {
        public Text headText;
        public TextMeshProUGUI valueText;
        public Slider slider;
        public Action<int> onValueChangedAction;

        private bool k;
        public void SetSliderPrefab(string headName,int value,int max,int min,Action<int> action)
        {
            headText.text = headName;
            slider.minValue = min;
            slider.maxValue = max;
            slider.value = value;
            valueText.text = Mathf.RoundToInt(slider.value) + "";
            onValueChangedAction = action;
            k = true;
        }
        public void OnSliderdraged()
        {
            if (!ChoosePannelManager.Instance.isSwitchingRole) 
            {
                valueText.text = Mathf.RoundToInt(slider.value) + "";
                if (k)
                    onValueChangedAction?.Invoke(Mathf.RoundToInt(slider.value));
                ChoosePannelManager.Instance.OnFinishEXSettings();
                ChoosePannelManager.Instance.RefreshSettingValues();
            }
        }
        public int GetValue()
        {
            return Mathf.RoundToInt(slider.value);
        }
        public void Add()
        {
            if (slider.value <= slider.maxValue - 1)
            {
                slider.value++;
            }
        }
        public void Minus()
        {
            if(slider.value >= slider.minValue + 1)
            {
                slider.value--;
            }
        }
    }
}