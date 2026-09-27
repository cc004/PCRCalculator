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
        public Text maxDamageText;
        public Text minDamageText;
        public Text buttonText;
        public ScrollEx scroll;
        public AutoCalculatorData calculatorData;
        public InputField count;
        public Text win;
        public Text lose;
        public Text timeout;
        public Toggle useRemote;
        public void Init(AutoCalculatorData data)
        {
            calculatorData = data;
            timesText.text = "【次数】" + data.execedTime + "/" + data.execTime;
            averageDamageText.text = "【平均】" + data.Average;
            maxDamageText.text = "【最高】" + data.MaxDamage;
            minDamageText.text = "【最低】" + data.MinDamage;
            var winCount = calculatorData.resultDatas.Count(r => r.BattleResult == Elements.eBattleResult.WIN);
            var loseCount = calculatorData.resultDatas.Count(r => r.BattleResult == Elements.eBattleResult.LOSE);
            var timeoutCount = calculatorData.resultDatas.Count(r => r.BattleResult == Elements.eBattleResult.TIME_OVER);
            win.text = $"<color=#4F9F6C>【胜利】：{winCount}次</color>";
            lose.text = $"<color=#C64E51>【失败】：{loseCount}次</color>";
            timeout.text = $"<color=#5EA5C6>【超时】：{timeoutCount}次</color>";
            buttonText.text = (data.isCalculating && !data.isCanceled && !data.isFinish) ? "取消" : "开始批量计算";
            scroll.ClearAll();
            // 仅实例化可见区数量，按需重绑
            data.resultDatas.Sort((left, right) =>
            {
                var damageCompare = right.currentDamage.CompareTo(left.currentDamage);
                return damageCompare != 0 ? damageCompare : left.id.CompareTo(right.id);
            });
            var datas = data.resultDatas;
            scroll.EnableVirtualization(datas.Count, (idx, go) =>
            {
                var result = go.GetComponent<AutoCalculatorPrefab>();
                result.Init(datas[idx]);
                result.count.text = "第" + (idx + 1) + "条";
            }, bufferRows: 15);
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
                    string.Join("\n", calculatorData.resultDatas
                        .OrderByDescending(data => data.currentDamage)
                        .ThenBy(data => data.id)
                        .Select(
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
            MainManager.Instance.AutoCalculatorData.useRemote = useRemote.isOn;
            GuildManager.Instance.StartAutoCalculateByButton(num);
        }

    }
}
