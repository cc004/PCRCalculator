using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class AutoCalculate : MonoBehaviour
    {
        public Text timesText;
        public Text averageDamageText;
        public Text buttonText;
        public ScrollEx scroll;
        public AutoCalculatorData calculatorData;

        public void Init(AutoCalculatorData data)
        {
            calculatorData = data;
            timesText.text = data.execedTime + "/" + data.execTime;
            averageDamageText.text = data.Average + "";
            buttonText.text = (data.isCalculating&&!data.isCanceled&&!data.isFinish) ? "取消" : "开始新的计算";
            scroll.ClearAll();
            foreach (var d in data.resultDatas)
            {
                scroll.CreatePrefab(a => a.GetComponent<AutoCalculatorPrefab>().Init(d));
            }
            scroll.AutoFit();
        }
        public void ButtonPress()
        {
            if(calculatorData.isCalculating && !calculatorData.isCanceled && !calculatorData.isFinish)
            {
                calculatorData.isPaues = true;
                MainManager.Instance.WindowConfigMessage("是否终止计算？", Cancel_0, Cancel_1);
            }
            else
            {
                MainManager.Instance.WindowInputMessage("输入计算次数（2到20）", StartNew_0);
            }

        }
        public void ExitButton()
        {
            if (calculatorData.isCalculating && !calculatorData.isCanceled && !calculatorData.isFinish)
                return;
            Destroy(gameObject);
        }
        private void Cancel_0()
        {
            calculatorData.isCanceled = true;
            calculatorData.isPaues = false;
        }
        private void Cancel_1()
        {
            calculatorData.isPaues = false;
        }
        private void StartNew_0(string  input)
        {
            int num = int.Parse(input, System.Globalization.NumberStyles.Any);
            Guild.GuildManager.Instance.StartAutoCalculateByButton(num);
        }

    }
}
