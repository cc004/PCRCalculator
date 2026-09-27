using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class AutoCalculatorPrefab : MonoBehaviour
    {
        public Image resultImage;
        public OnceResultData resultData;
        public Text resultText;
        public Text count;
        public Text damage;
        public Text backTime;
        public Text seed;
        public Button copySeedButton;
        public void Init(OnceResultData data)
        {
            resultData = data;
            resultImage.color = resultData.BattleResult switch
            {
                Elements.eBattleResult.WIN => new Color(0x4F / 255f, 0x9F / 255f, 0x6C / 255f),
                Elements.eBattleResult.LOSE => new Color(0xC6 / 255f, 0x4E / 255f, 0x51 / 255f),
                _ => new Color(0x5E / 255f, 0xA5 / 255f, 0xC6 / 255f)
            };
            resultText.text = resultData.BattleResult switch
            {
                Elements.eBattleResult.WIN => "胜利",
                Elements.eBattleResult.LOSE => "失败",
                _ => "超时"
            };
            count.text = "第" + data.id + "次";
            damage.text = "<color=#FF0000>" + data.currentDamage+"</color>";
            backTime.text = resultData.backTime == 0 ? string.Empty : $"{resultData.backTimeText}";
            seed.text = "种子:" + resultData.randomSeed;
        }
        public void CopySeedButton()
        {
            #if UNITY_2017_2_OR_NEWER
            GUIUtility.systemCopyBuffer = $"{resultData.randomSeed}";
            #endif
            MainManager.Instance.WindowMessage("已复制种子到剪贴板");
        }
        public void DetailButton()
        {
            MainManager.Instance.WindowConfigMessage(resultData.GetDetail(), null);
        }
    }
}
