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
            string atlasName = "AtlasTalentQuest";
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
            else if (GuildManager.Instance.IsMirageBoss)
            {
                battleEffects = MainManager.Instance.MirageBattleEffectDic
                    .Values
                    .Where(x => x.quest_id == questId)
                    .OrderBy(x => x.id)
                    .Cast<object>()
                    .ToList();
            }
            else if (GuildManager.Instance.IsLabyrinthBoss)
            {
                atlasName = "AtlasLabyrinthBattleEffect";
                int difficulty = GuildManager.Instance.SettingData.GetCurrentPlayerGroup().labyrinthDifficulty;
                battleEffects = MainManager.Instance.LabyrinthDifficultyEffectDic
                    .Values
                    .Where(x => x.difficulty == difficulty)
                    .OrderBy(x => x.id)
                    .Cast<object>()
                    .ToList();
            }
            if (battleEffects.Count > 0)
            {
                var firstEffect = battleEffects[0];
                var iconProperty = firstEffect.GetType().GetProperty("icon_name");
                var effectProperty = firstEffect.GetType().GetProperty("effect_name");
                var descProperty = firstEffect.GetType().GetProperty("description");

                // 加载精灵图集
                var sprites = Resources.LoadAll<Sprite>(atlasName)
                    .ToDictionary(x => x.name, x => x);

                // 按顺序设置TalentQuestBattleEffectPictures的图片
                foreach (var effectData in battleEffects)
                {
                    GameObject a = Instantiate(extraDetailPrefab);
                    a.transform.SetParent(Content, false);
                    ExtraEffecDetail detailComponent = a.GetComponent<ExtraEffecDetail>();
                    if (detailComponent != null)
                    {
                        string iconName = iconProperty != null ? iconProperty.GetValue(effectData)?.ToString() : "";
                        string effectName = effectProperty != null ? effectProperty.GetValue(effectData)?.ToString() : "";
                        string description = descProperty != null ? descProperty.GetValue(effectData)?.ToString() : "";

                        detailComponent.nameText.text = effectName ?? "";
                        detailComponent.describeText.text = description ?? "";

                        if (!string.IsNullOrEmpty(iconName) && sprites.ContainsKey(iconName))
                        {
                            detailComponent.Icon.sprite = sprites[iconName];
                            detailComponent.Icon.gameObject.SetActive(true);
                        }
                        else
                        {
                            detailComponent.Icon.gameObject.SetActive(false);
                        }
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
