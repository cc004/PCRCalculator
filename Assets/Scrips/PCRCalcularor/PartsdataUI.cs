using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PCRCaculator.Battle
{
    public class PartsdataUI : MonoBehaviour
    {
        public SpriteRenderer rular;
        public TextMesh posText;
        public TextMesh hpHeadText;
        public TextMesh hpDetauil;

        private Elements.PartsData partsData;
        private int recoverTime;
        public void Init(Elements.PartsData partsData)
        {
            this.partsData = partsData;
            StartCoroutine(upDateHP());
        }
        private IEnumerator upDateHP()
        {
            yield return (object)null;
            yield return (object)null;
            yield return (object)null;
            while (true)
            {
                if ((int)partsData.BreakPoint == 0 && partsData.IsBreak)
                {
                    hpHeadText.text = string.Format("{0:N2}s后恢复", partsData.BreakTime + partsData.RecoverTime);
                }
                else
                {
                    hpHeadText.text = $"{partsData.BreakPoint}({partsData.BreakPoint / (float)partsData.MaxBreakPoint:P2})";
                }
                hpDetauil.text = $"{partsData.GetDefZero()}-{partsData.GetMagicDefZero()}";
                //transform.position = partsData.GetBottomTransformPosition() + new Vector3(0.0f, partsData.PositionY, 0.0f);
                if (partsData.Owner != null)
                    posText.text = "" + Mathf.RoundToInt(partsData.Owner.transform.localPosition.x + partsData.PositionX);
                else
                    posText.text = "???";
                yield return (object)null;
            }

        }
    }
}