using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace PCRCaculator.Guild
{
    public class GuildCalcButton : MonoBehaviour
    {
        public enum GuildButtonType { typeA,typeB}
        public GuildButtonType buttonType;
        public Image backImage;
        public Text wordText;
        public Text startNumText;
        public Text endNumText;
        public Button button;
        public Button UBButton;
        public bool isUBButton;

        public void SetButton(Color backColor,string word,int startCount,Action action = null)
        {
            backImage.color = backColor;
            wordText.text = word;
            startNumText.text = "" + startCount;
            if(action!= null)
            {
                button.onClick.AddListener(() => { action(); });
                if (isUBButton)
                    UBButton.onClick.AddListener(() => { action(); });
            }
        }
        public void SetButton(Color backColor, string word, int startCount,int endCount,bool middle=true,Action action = null)
        {
            backImage.color = backColor;
            wordText.text = word;
            startNumText.text = "" + startCount;
            endNumText.text = "" + endCount;
            wordText.alignment = middle ? TextAnchor.MiddleCenter : TextAnchor.MiddleLeft;
            if (action != null)
            {
                button.onClick.AddListener(() => { action(); });
            }
        }

    }
}