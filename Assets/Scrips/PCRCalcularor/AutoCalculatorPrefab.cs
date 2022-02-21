using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class AutoCalculatorPrefab : MonoBehaviour
    {
        public Text text;
        public OnceResultData resultData;

        public void Init(OnceResultData data)
        {
            resultData = data;
            text.text = "第" + data.id + "次" + (data.warnings.Count>0? "   <color=#FF0000>" + data.currentDamage+"</color>":"   "+data.currentDamage);
        }
        public void DetailButton()
        {            
            MainManager.Instance.WindowConfigMessage(resultData.GetDetail(), null);
        }
    }
}