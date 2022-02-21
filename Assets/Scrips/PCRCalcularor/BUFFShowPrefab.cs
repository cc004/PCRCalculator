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
            toggle.isOn = enable;
        }
    }
    
}