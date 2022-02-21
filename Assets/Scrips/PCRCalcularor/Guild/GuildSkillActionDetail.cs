using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{
    public class GuildSkillActionDetail : MonoBehaviour
    {
        public List<Text> DetailTexts;
        public void SetTexts(List<string> data)
        {
            for(int i = 0; i < 6; i++)
            {
                DetailTexts[i].text = data[i];
            }
        }
    }
}