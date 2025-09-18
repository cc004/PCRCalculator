using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections;
namespace PCRCaculator.Guild
{
    public class GuildSkillActionDetail : MonoBehaviour
    {
        public List<Text> DetailTexts;
        public RectTransform View;
        public void SetTexts(List<string> data)
        {
            for (int i = 0; i < 5; i++)
            {
                DetailTexts[i].text = data[i];
            }
        }
    }
}