using System.Collections.Generic;
using System.Linq;
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
        public void SetExEquipmentIcon(int unitId, int equipmentid, int exlevel, int j = 0)
        {
            Sprite loadedSprite = ABExTool.GetSprites(ABExTool.SpriteType.EX装备, equipmentid);
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
                int category = MainManager.Instance.UnitExEquips[unitId][j].FirstOrDefault().Value.category;
                Sprite defaultSprite = ABExTool.GetSprites(ABExTool.SpriteType.EX装备类型, category);
                equipmentIcon.sprite = defaultSprite;
                levelIcon.SetActive(false);
            }
        }
    }
}