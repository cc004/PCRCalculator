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
            int questId = GuildManager.Instance.TalentQuestWaveGroupId / 10;

            // 从数据表中找到匹配quest_id的数据
            var battleEffects = MainManager.Instance.TalentQuestBattleEffectDic
                .Values
                .Where(x => x.quest_id == questId)
                .OrderBy(x => x.id)
                .ToList();

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
                    if (sprites.ContainsKey(effectData.icon_name.ToString()))
                    {
                        detailComponent.Icon.sprite = sprites[effectData.icon_name.ToString()];
                        detailComponent.Icon.gameObject.SetActive(true);
                        detailComponent.nameText.text = effectData.effect_name;
                        detailComponent.describeText.text = effectData.description;
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