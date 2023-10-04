using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{
    public class UBTime : MonoBehaviour
    {
        public InputField inputField;
        private float originalHeight;
        public RectTransform inputTrans;

        public void Awake()
        {
            originalHeight = inputTrans.sizeDelta.y;
            inputField.textComponent.GetComponent<TextEvent>().OnTextChanged += CaretMoved;
        }

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

        public void InputValueChange()
        {   
            inputTrans.sizeDelta = new Vector2(inputTrans.sizeDelta.x, Mathf.Max(inputField.preferredHeight, originalHeight));
        }

        private bool ignoreOne = false;
        private void CaretMoved()
        {
            if (!inputField.isFocused) // 聚焦后会自动select all 屏蔽这次滚动
            {
                ignoreOne = true;
                return;
            }

            var lines = inputField.textComponent.cachedTextGenerator.lines;
            var idx = inputField.selectionFocusPosition;

            if (idx == 0) idx = inputField.caretPosition;
            if (idx == 0) return;

            if (ignoreOne)
            {
                ignoreOne = false;
                return;
            }

            float h = 0f, dh = 0f;

            foreach (var line in lines)
            {
                if (line.startCharIdx > idx)
                    break;
                h = lines[0].topY - line.topY;
                dh = line.height;
            }


            h /= inputField.textComponent.pixelsPerUnit;
            dh /= inputField.textComponent.pixelsPerUnit;

            h -= (inputTrans.sizeDelta.y - originalHeight) / 2;
            
            inputTrans.anchoredPosition =
                new Vector2(0f, Math.Clamp(inputTrans.anchoredPosition.y, h + dh - originalHeight, h));
        }
    }
}