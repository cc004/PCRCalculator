using System.Collections.Generic;
using PCRCaculator.Guild;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
namespace PCRCaculator
{
    public class ExtraEffectSkill : MonoBehaviour
    {
        // public Image bigIcon;
        // public Text nameText;
        // public Text describeText;
        public GameObject extraDetailPrefab;
        public RectTransform Content;
        void OnEnable()
        {
            // 计算quest_id值
            int questId = GuildManager.Instance.WaveGroupId / 10;

            // 从数据表中找到匹配quest_id的数据
            List<object> battleEffects = new List<object>();
            if (GuildManager.Instance.IsTalentQuestBoss)
            {
                battleEffects = MainManager.Instance.TalentQuestBattleEffectDic
                    .Values
                    .Where(x => x.quest_id == questId)
                    .OrderBy(x => x.id)
                    .Cast<object>()
                    .ToList();
            }
            else if (GuildManager.Instance.IsAbyssBoss)
            {
                battleEffects = MainManager.Instance.AbyssBattleEffectDic
                    .Values
                    .Where(x => x.quest_id == questId)
                    .OrderBy(x => x.id)
                    .Cast<object>()
                    .ToList();
            }

            // 加载精灵图集
            var sprites = Resources.LoadAll<Sprite>("AtlasTalentQuest")
                .ToDictionary(x => x.name, x => x);

            // 按顺序设置TalentQuestBattleEffectPictures的图片
            for (int i = 0; i < battleEffects.Count; i++)
            {
                GameObject a = Instantiate(extraDetailPrefab);
                a.transform.SetParent(Content);
                ExtraEffecDetail detailComponent = a.GetComponent<ExtraEffecDetail>();
                if (detailComponent != null && i < battleEffects.Count)
                {
                    var effectData = battleEffects[i];
                    // 使用dynamic简化类型处理
                    dynamic dynamicEffect = effectData;
                    string iconName = dynamicEffect.icon_name;
                    string effectName = dynamicEffect.effect_name;
                    string description = dynamicEffect.description;

                    if (!string.IsNullOrEmpty(iconName) && sprites.ContainsKey(iconName))
                    {
                        detailComponent.Icon.sprite = sprites[iconName];
                        detailComponent.Icon.gameObject.SetActive(true);
                        detailComponent.nameText.text = effectName;
                        detailComponent.describeText.text = description;
                    }
                    else
                    {
                        detailComponent.Icon.gameObject.SetActive(false);
                    }
                }
            }
        }
        /// <summary>
        /// 退出
        /// </summary>
        public void Exit()
        {
            Destroy(gameObject);
        }
    }
}