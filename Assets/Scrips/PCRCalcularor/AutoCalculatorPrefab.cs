using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class AutoCalculatorPrefab : MonoBehaviour
    {
        public Text text;
        public Image resultImage;
        public List<Sprite> resultSprites;
        public OnceResultData resultData;
        public Text resultText;
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
            text.text = "第" + data.id + "次" + (data.warnings.Count>0? "   <color=#FF0000>" + data.currentDamage+"</color>":"   "+data.currentDamage)
                 + (resultData.backTime == 0 ? string.Empty : $"   {resultData.backTimeText}");
        }
        public void DetailButton()
        {
            MainManager.Instance.WindowConfigMessage(resultData.GetDetail(), null);
        }
    }
}