using System.Globalization;
using System.Linq;
using System.Text;
using PCRCaculator.Guild;
using SFB;
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
        public InputField count;

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
                // MainManager.Instance.WindowInputMessage("输入计算次数（2到2147483647）", StartNew_0);
                if (count.text != "") 
                {
                    StartNew_0(count.text);
                }
                else{
                    MainManager.Instance.WindowMessage("请输入批量次数");
                }
      }

        }
        public void Button2Press()
        {
            {
                var defaultName = "跑批结果";
                string filePath;
#if PLATFORM_ANDROID
                string name = (defaultName == "" ? "ExportTimeLine" : defaultName);
                filePath = Application.persistentDataPath + "/" + name + ".csv";
#else
                var ststrr = StandaloneFileBrowser.SaveFilePanel(
                "保存结果", string.Empty, defaultName, "csv");
                if (!string.IsNullOrEmpty(ststrr))
                {
                    filePath = ststrr;
                    filePath = filePath.Replace("\\", "/");
                }
                else return;

#endif
                var content =
                    "id,返秒,伤害,蓝字,种子,警告\n" +
                    string.Join("\n", calculatorData.resultDatas.Select(
                        data => $"{data.id},{data.backTime}," +
                                $"{data.currentDamage},{data.exceptDamage}," +
                                $"{data.randomSeed},{string.Join(",", data.warnings)}"));
                System.IO.File.WriteAllText(filePath, content, Encoding.GetEncoding("GBK"));
#if PLATFORM_ANDROID
                MainManager.Instance.WindowConfigMessage("EXCEL保存路径为：" + filePath + "\n是否打开？",()=> AndroidTool.ShowExcelFile(filePath));
#endif
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
            buttonText.text="开始新的计算";
        }
        private void Cancel_1()
        {
            calculatorData.isPaues = false;
        }
        private void StartNew_0(string input)
        {
            int num = int.Parse(input, NumberStyles.Any);
            GuildManager.Instance.StartAutoCalculateByButton(num);
        }

    }
}
