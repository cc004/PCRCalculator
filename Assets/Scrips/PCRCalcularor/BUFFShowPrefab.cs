using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class BUFFShowPrefab : MonoBehaviour
    {
        public Text nameText;
        public Toggle toggle;
        public eStateIconType stateIconType;

        public void Init(eStateIconType type,bool enable)
        {
            stateIconType = type;
            nameText.text = stateIconType.GetDescription();
            if (nameText.text == stateIconType.ToString())
            {
                var buff = UnitCtrl.BUFF_DEBUFF_ICON_DIC.FirstOrDefault(pair => pair.Value.BuffIcon == stateIconType);
                if (buff.Value != null)
                {
                    nameText.text = buff.Key.GetDescription() + "buff";
                }
                else
                {
                    var buff2 = UnitCtrl.BUFF_DEBUFF_ICON_DIC.FirstOrDefault(pair => pair.Value.DebuffIcon == stateIconType);
                    if (buff2.Value != null)
                    {
                        nameText.text = buff2.Key.GetDescription() + "debuff";
                    }
                    else
                    {
                        var abn = UnitCtrl.ABNORMAL_CONST_DATA.FirstOrDefault(p => p.Value.IconType == type);
                        if (abn.Value != null)
                        {
                            nameText.text = abn.Key.GetDescription();
                        }
                    }
                }
            }
            toggle.isOn = enable;
        }
    }
    
}