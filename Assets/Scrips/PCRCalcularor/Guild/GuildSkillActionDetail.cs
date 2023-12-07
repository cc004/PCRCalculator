using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace PCRCaculator.Guild
{
    public class GuildSkillActionDetail : MonoBehaviour
    {
        public List<Text> DetailTexts;
        public RectTransform View;
        public void SetTexts(List<string> data) 
        {
            for(int i = 0; i < 4; i++)
            {
                DetailTexts[i].text = data[i];
            }

            var delta = View.sizeDelta;
            View.sizeDelta = new Vector2(delta.x, Mathf.Min(delta.y, DetailTexts[3].preferredHeight + 10));
        }
    }
}