﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class CalculatePageButton : MonoBehaviour
    {
        public Text headText;
        public List<Image> equipmentIcons;
        public List<Text> oddTexts;
        public Text countText;
        public Text costAPText;


        public void SetButton(QuestRewardData data,int count,int costAP)
        {
            headText.text = data.quest_name;
            for(int i = 0; i < equipmentIcons.Count; i++)
            {
                if (data.rewardEquips.Count > i)
                {
                    //string equip_path = "equipments/icon_equipment_" + data.rewardEquips[i];
                    //Sprite sprite = MainManager.LoadSourceSprite(equip_path);
                    Sprite sprite = ABExTool.GetSprites(ABExTool.SpriteType.装备图标, data.rewardEquips[i]);
                    if(sprite == null)
                    {
                        sprite = ABExTool.GetSprites(ABExTool.SpriteType.装备图标, 999999);
                    }
                    equipmentIcons[i].sprite = sprite;
                    oddTexts[i].text = data.odds[i] + "%";
                }
                else
                {
                    equipmentIcons[i].sprite = ABExTool.GetSprites(ABExTool.SpriteType.装备图标, 999999);
                    oddTexts[i].text = "???";
                }
            }
            countText.text = count + "";
            costAPText.text = costAP + "";

        }
    }
}