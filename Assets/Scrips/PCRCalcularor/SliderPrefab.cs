﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

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
            valueText.text = Mathf.RoundToInt(slider.value) + "";
            if(k)
                onValueChangedAction?.Invoke(Mathf.RoundToInt(slider.value));
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