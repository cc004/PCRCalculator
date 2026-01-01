using System.Collections.Generic;
using Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class EquipmentPageIcon : MonoBehaviour
    {
        public Image equipmentIcon;
        //public Text numText;
        public Text numText;
        public RectTransform ScreenShotTrans;
        public List<Sprite> levelIcons;
        public Image levelIcon;
        public void SetEquipmentIcon(int equipmentid, int num, int alreadyHave = -1)
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
        public void SetExEquipmentIcon(int equipmentid, int exlevel, int j = 0)
        {
            Sprite loadedSprite = Resources.Load<Sprite>($"extraequip/icon_extra_equip_{equipmentid}");
            if (loadedSprite != null)
            {
                equipmentIcon.sprite = loadedSprite;
                levelIcon.sprite = levelIcons[exlevel];
                int exRarity = (equipmentid / 100) % 10;
                levelIcon.SetActive(exRarity < 5);
            }
            else
            {
                // 如果找不到指定的装备图标，则使用默认图标
                Sprite defaultSprite = Resources.Load<Sprite>($"extraequip/icon_extra_equip_category_{j + 1}01");
                equipmentIcon.sprite = defaultSprite;
                levelIcon.SetActive(false);
            }
        }
    }
}