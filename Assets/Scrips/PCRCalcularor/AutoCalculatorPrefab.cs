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

        public void Init(OnceResultData data)
        {
            resultData = data;
            resultImage.sprite = resultData.isVictory ? resultSprites[0] : resultSprites[1];
            text.text = "第" + data.id + "次" + (data.warnings.Count>0? "   <color=#FF0000>" + data.currentDamage+"</color>":"   "+data.currentDamage)
                 + (resultData.backTime == 0 ? string.Empty : $"{resultData.backTimeText}");
        }
        public void DetailButton()
        {
            MainManager.Instance.WindowConfigMessage(resultData.GetDetail(), null);
        }
    }
}