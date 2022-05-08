using System.Collections;
using Elements;
using UnityEngine;

namespace PCRCaculator.Battle
{
    public class PartsdataUI : MonoBehaviour
    {
        public SpriteRenderer rular;
        public TextMesh posText;
        public TextMesh hpHeadText;
        public TextMesh hpDetauil;

        private PartsData partsData;
        private int recoverTime;
        public void Init(PartsData partsData)
        {
            this.partsData = partsData;
            StartCoroutine(upDateHP());
        }
        private IEnumerator upDateHP()
        {
            yield return null;
            yield return null;
            yield return null;
            while (true)
            {
                if (partsData.BreakPoint == 0 && partsData.IsBreak)
                {
                    hpHeadText.text = string.Format("{0:N2}s后恢复", partsData.BreakTime + partsData.RecoverTime);
                }
                else
                {
                    hpHeadText.text = $"{partsData.BreakPoint}({partsData.BreakPoint / (float)partsData.MaxBreakPoint:P2})";
                }
                hpDetauil.text = $"{partsData.GetDefZero()}/{partsData.GetMagicDefZero()}";
                //transform.position = partsData.GetBottomTransformPosition() + new Vector3(0.0f, partsData.PositionY, 0.0f);
                if (partsData.Owner != null)
                {
                    var owner2 = partsData.Owner;
                    var mid = partsData.GetPosition().x / owner2.transform.lossyScale.x;
                    var r = mid + partsData.GetBodyWidth()/ 2;
                    var l = mid - partsData.GetBodyWidth() / 2;
                    posText.text = $"{Mathf.RoundToInt(l)}/{mid}/{Mathf.RoundToInt(r)}";
                }
                else
                    posText.text = "???";
                yield return null;
            }

        }
    }
}