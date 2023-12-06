using System;
using System.Collections;
using Elements;
using Elements.Battle;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Battle
{
    public class BattleBuffUI : MonoBehaviour
    {
        public Image buffIcon;
        public Slider timeCountSlider;
        public Text detailText;
        public Image fieldImage;

        public Elements.eStateIconType stateIconType;
        public string describe;
        private Action<BattleBuffUI> removeThis;

        public void Init(Sprite icon, Elements.eStateIconType stateIconType)
        {
            buffIcon.sprite = icon;
            this.stateIconType = stateIconType;
        }

        public void Init(Sprite icon, Elements.eStateIconType stateIconType,float stayTime,string detail,Action<BattleBuffUI> remove)
        {
            buffIcon.sprite = icon;
            this.stateIconType = stateIconType;
            detailText.text = detail;
            describe = detail;
            removeThis = remove;
            StartCoroutine(RefreshTime(stayTime, BattleHeaderController.CurrentFrameCount));
        }
        public IEnumerator RefreshTime(float stayTime,int startFrame)
        {
            int stayFrame =(int)(stayTime * 60);
            while (!BattleManager.Instance.battleFinished && !BattleManager.Instance.skipping)
            {
                int currentFrame = BattleHeaderController.CurrentFrameCount;
                float v = (currentFrame - startFrame) / (float)stayFrame;
                if (v <= 1 && v >= 0)
                    timeCountSlider.value = v;
                else
                    break;
                yield return null;
            }
            yield return null;
            removeThis?.Invoke(this);
        }
    }
}