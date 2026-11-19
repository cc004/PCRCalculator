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
        

        public void Init(IEnumerable<int> skillIds)
        {
            if (skillIds == null)
                return;

            foreach (int skillId in skillIds)
            {
                AddSkillIcon(skillId);
            }
        }

        private void AddSkillIcon(int skillId)
        {
            if (skillId <= 0)
                return;

            skillImageDic[skillId] = ResolveSkillIcon(skillId);
        }

        /// <summary>
        /// 从技能表里取图标；取不到返回 null（调用方自己兜底）。
        /// 技能革命（++ 档）这类运行期才会出现的 id 也能在这里补。
        /// </summary>
        private static Sprite ResolveSkillIcon(int skillId)
        {
            MainManager mainManager = MainManager.Instance;
            if (mainManager == null || mainManager.SkillDataDic == null)
                return null;

            if (!mainManager.SkillDataDic.TryGetValue(skillId, out SkillData skillData) || skillData == null)
                return null;

            return ABExTool.GetSprites(ABExTool.SpriteType.技能图标, skillData.icon);
        }

        public void SetSkillState(int currentSkillid,int nextSkillid,float castTime,float animeTime)
        {
            ApplySkillState(iconImage_current, iconText_current, currentSkillid);
            ApplySkillState(iconImage_next, iconText_next, nextSkillid);
            allcastTime = castTime;
            mask_A.fillAmount = 0;
        }

        private void ApplySkillState(Image iconImage, TextMeshProUGUI iconText, int skillId)
        {
            if (iconImage != null)
            {
                iconImage.sprite = skillId == 1 ? atkicon : GetSkillIcon(skillId);
            }

            if (iconText == null)
                return;

            bool showLevel = skillId != 1;
            iconText.gameObject.SetActive(showLevel);
            if (showLevel)
            {
                iconText.text = (skillId % 10 - 1) + "";
            }
        }

        /// <summary>
        /// 取技能图标；字典里没有就现场解析一次并缓存，仍取不到则用普攻图标兜底。
        /// 以前这里直接索引字典，遇到未预装的技能 id（如技能革命 ++ 档）会抛 KeyNotFoundException。
        /// </summary>
        private Sprite GetSkillIcon(int skillId)
        {
            if (skillImageDic.TryGetValue(skillId, out Sprite sprite) && sprite != null)
                return sprite;

            sprite = ResolveSkillIcon(skillId);
            if (sprite == null)
            {
                sprite = atkicon;
                Debug.LogWarning($"[SkillUI] skill {skillId} 在技能表里找不到图标，先用普攻图标兜底");
            }

            skillImageDic[skillId] = sprite;
            return sprite;
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