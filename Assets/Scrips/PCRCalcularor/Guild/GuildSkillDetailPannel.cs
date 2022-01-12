using System.Collections;
using System.Collections.Generic;
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
        public void Setdetails(UnitSkillExecData data)
        {
            detailTexts[0].text = data.skillName;
            detailTexts[1].text = data.UnitName;
            detailTexts[2].text = data.skillState.GetDescription();
            detailTexts[3].text = data.startTime + "~" + data.endTime;
            detailTexts[4].text = data.energy == 0f ? "无" : $"{data.energy}";
            for(int i = 0; i < data.actionExecDatas.Count; i++)
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
            a.transform.localPosition = prefabBasePos + idx*prefabAddPos;
            List<string> details = new List<string>();
            details.Add(data.actionID + "");
            details.Add(data.actionType);
            details.Add(string.Join("/", data.targetNames));
            details.Add(data.execTime + "");
            details.Add(data.result.GetDescription());
            details.Add(data.describe);
            a.GetComponent<GuildSkillActionDetail>().SetTexts(details);
            prefabs.Add(a);
        }
    }
}