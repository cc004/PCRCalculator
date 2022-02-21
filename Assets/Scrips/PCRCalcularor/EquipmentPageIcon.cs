using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class EquipmentPageIcon : MonoBehaviour
    {
        public Image equipmentIcon;
        //public Text numText;
        public TextMeshProUGUI numText;
        public void SetEquipmentIcon(int equipmentid, int num,int alreadyHave=-1)
        {
            //string equip_path = "equipments/icon_equipment_" + equipmentid;
            //Sprite im = MainManager.LoadSourceSprite(equip_path);
            Sprite im = ABExTool.GetSprites(ABExTool.SpriteType.装备图标, equipmentid);
            if (im != null) 
            { 
                equipmentIcon.sprite = im;
            }
            else 
            { 
                equipmentIcon.sprite = ABExTool.GetSprites(ABExTool.SpriteType.装备图标, 999999);
            }
            
            if (num >= 2)
            {
                string showSTR = "x" + num;
                if (alreadyHave > 0)
                {
                    showSTR += alreadyHave >= num ? $"有({alreadyHave})" : $"<color=#FF0000>缺{num - alreadyHave}</color>";
                }
                else
                {
                    showSTR = $"<color=#FF0000>{showSTR}</color>";
                }
                numText.text = showSTR;

                numText.gameObject.SetActive(true);
            }
            else
            {
                numText.gameObject.SetActive(false);
            }
        }
    }
}