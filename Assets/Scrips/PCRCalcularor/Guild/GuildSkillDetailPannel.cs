using System.Collections.Generic;
using Elements;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{
    public class GuildSkillDetailPannel : MonoBehaviour
    {
        public List<Text> detailTexts;
        public RectTransform prefabParent;
        public GameObject actionPrefab;
        public Vector3 prefabBasePos;
        public Vector3 prefabAddPos;

        private List<GameObject> prefabs = new List<GameObject>();

        public void ExitButton()
        {
            Destroy(gameObject);
        }

        public void OnPointerClick()
        {

        }

        private float posy;
        public void Setdetails(UnitSkillExecData data)
        {
            detailTexts[0].text = $"技能来源：{data.UnitName}【{data.skillName}】";
            // detailTexts[2].text = data.skillState.GetDescription();
            detailTexts[1].text = $"执行帧数：{data.startTime}~{data.endTime}";
            detailTexts[2].text = $"TP值：{(data.energy == 0f ? "无" : data.energy)}";
            detailTexts[3].text = data.skillName;
            posy = prefabBasePos.y;
            for (int i = 0; i < data.actionExecDatas.Count; i++)
            {
                AddActionDetails(data.actionExecDatas[i], i);
            }
            Vector2 size = prefabParent.sizeDelta;
            float y = Mathf.Abs((prefabBasePos + data.actionExecDatas.Count * prefabAddPos).y);
            if (size.y<y)
            {
                prefabParent.sizeDelta = new Vector2(size.x, y);
            }
        }
        private void AddActionDetails(UnitActionExecData data,int idx)
        {
            GameObject a = Instantiate(actionPrefab);
            a.transform.SetParent(prefabParent, false);
            a.transform.localPosition = Vector3.up * posy;
            var view = a.GetComponent<GuildSkillActionDetail>().View;
            List<string> details = new List<string>
            {
                $"{data.actionID}",
                $"({data.actionType})",
                string.Join(" ", data.targetNames),
                $"[{data.execTime}]\n{data.result.GetDescription()}",
                data.describe
            };
            a.GetComponent<GuildSkillActionDetail>().SetTexts(details);
            Color backgroundColor = (idx % 2 == 0) ?
                new Color(255f, 255f, 255f, 1.0f) :  // 白色
                new Color(0.85f, 0.85f, 0.85f, 1.0f);   // 浅灰色

            var imageComponent = a.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.color = backgroundColor;
            }
            else
            {
                var bgImage = a.AddComponent<Image>();
                bgImage.color = backgroundColor;
            }
            prefabs.Add(a);
            posy += (view.localPosition.y - view.sizeDelta.y - 30);
        }
    }
}