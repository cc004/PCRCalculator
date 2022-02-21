using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Battle
{
    public class SkillUI : MonoBehaviour
    {
        public Image iconImage_current;
        public Image iconImage_next;
        public TextMeshProUGUI iconText_current;
        public TextMeshProUGUI iconText_next;
        public Image mask_A;
        public Image mask_B;
        public Sprite atkicon;

        
        private Dictionary<int, Sprite> skillImageDic = new Dictionary<int, Sprite>();
        private float allcastTime;
        

        public void Init(int[] skilllist,int skillEV1,int skillEV2)
        {
            Action<int> action = a =>
            {
                //string path = "skills/icon_skill_" + MainManager.Instance.SkillDataDic[a].icon;
                //Sprite im = MainManager.LoadSourceSprite(path);
                Sprite im = ABExTool.GetSprites(ABExTool.SpriteType.技能图标, MainManager.Instance.SkillDataDic[a].icon);
                skillImageDic.Add(a, im);
            };
            for (int i = 0; i < skilllist.Length; i++)
            {
                if (skilllist[i] > 0)
                {
                    action(skilllist[i]);
                }
            }
            if (skillEV1 != 0)
                action(skillEV1);
            if (skillEV2 != 0)
                action(skillEV2);

        }
        public void SetSkillState(int currentSkillid,int nextSkillid,float castTime,float animeTime)
        {
            if (currentSkillid == 1)
            {
                iconImage_current.sprite = atkicon;
                iconText_current.gameObject.SetActive(false);
            }
            else
            {
                iconImage_current.sprite = skillImageDic[currentSkillid];
                iconText_current.gameObject.SetActive(true);
                int num = currentSkillid % 10 - 1;
                iconText_current.text = num + "";
            }
            if (nextSkillid == 1)
            {
                iconImage_next.sprite = atkicon;
                iconText_next.gameObject.SetActive(false);
            }
            else
            {
                iconImage_next.sprite = skillImageDic[nextSkillid];
                iconText_next.gameObject.SetActive(true);
                int num = nextSkillid % 10 - 1;
                iconText_next.text = num + "";
            }
            allcastTime = castTime;
            mask_A.fillAmount = 0;
        }

        public void SetCastTime(float time)
        {
            //if (time >= allcastTime) { return; }
            mask_A.fillAmount = Mathf.Min(1, Mathf.Max(0, time / allcastTime));
        }
        public void SetPlayTime(float time)
        {

        }
    }
}