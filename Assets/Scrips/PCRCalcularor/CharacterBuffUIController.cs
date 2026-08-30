using System.Collections;
using System.Collections.Generic;
using Elements;
using UnityEngine;

namespace PCRCaculator.Battle
{

    public class CharacterBuffUIController : MonoBehaviour
    {
        private const float PartsDisplayMinimumSpacing = 0.6f;

        public List<SpriteRenderer> abnormalIcons;
        public SpriteRenderer rular;
        public TextMesh posText;
        public GameObject hpBar;
        public TextMesh hpHeadText;
        public GameObject partsDataPrefab;
        public float poszFix;
        
        private UnitCtrl owner2;
        private BattleUIManager uIManager;
        private List<eStateIconType> currentBuffs = new List<eStateIconType>();


        private List<PartsData> bossPartsList = new List<PartsData>();
        
        public void SetBuffUI(Sprite sprite, UnitCtrl owner,bool isSummon = false)
        {
            //rular.sprite = sprite;
            owner2 = owner;
            uIManager = BattleUIManager.Instance;
            transform.SetParent(owner2.gameObject.transform, false);
            SetAbnormalIcons(null, eStateIconType.NONE, false);
            owner.OnChangeState += SetAbnormalIcons;
            if (isSummon)
            {
                //owner.OnLifeAmmountChange += SetHPBar;
                //hpBar.SetActive(true);
            }
            if (owner.IsPartsBoss)
            {
                SetPartsBossData();
            }
            StartCoroutine(ShowPosition_2());
        }
        public void SetAbnormalIcons(object unitCtrl,eStateIconType stateIconType,bool enable)
        {
            if(stateIconType == eStateIconType.NONE)
            {
                Refresh();
                return;
            }
            if (currentBuffs.Contains(stateIconType))
            {
                currentBuffs.Remove(stateIconType);
            }
            if (enable)
            {
                currentBuffs.Add(stateIconType);
            }
            Refresh();
        }
        public void SetHPBar(float normalizedHP)
        {
            int hpInt = Mathf.RoundToInt(Mathf.Min(1, Mathf.Max(0, normalizedHP)) * 100);
            hpHeadText.text = "HP:" + hpInt + "%";
        }
        public void SetAbnormalIcons(UnitCtrl unitCtrl, Elements.eStateIconType stateIconType_2, bool enable)
        {
            eStateIconType stateIconType = (eStateIconType)(int)stateIconType_2;
            if (stateIconType == eStateIconType.NONE)
            {
                Refresh();
                return;
            }
            if (currentBuffs.Contains(stateIconType))
            {
                currentBuffs.Remove(stateIconType);
            }
            if (enable)
            {
                currentBuffs.Add(stateIconType);
            }
            Refresh();
        }
        private void Refresh()
        {
            
            for(int i = 0; i < 4; i++)
            {
                abnormalIcons[i].gameObject.SetActive(false);
                /*
                if (i < currentBuffs.Count)
                {
                    abnormalIcons[i].sprite = uIManager.GetAbnormalIconSprite(currentBuffs[i]);
                    abnormalIcons[i].gameObject.SetActive(true);
                }
                else
                {
                    abnormalIcons[i].sprite = null;
                }*/
            }
        }
        public void SetLeftDir(bool isReversed = false)
        {
            transform.localScale = new Vector3(isReversed ? -1 : 1, 1, 1);
        }
        
        private IEnumerator ShowPosition_2()
        {
            while (true)
            {
                try
                {
#if DONT_USE_EX
                    posText.text = $"{owner2.GetInfo()}\n{(float)(owner2.Hp) / Mathf.Max(1, owner2.MaxHp):P2}\n{(float)(owner2.Energy) / UnitDefine.MAX_ENERGY:P2}";
#else
                    posText.text = $"{owner2.GetInfo()}\n{(float)(owner2.Hp ?? 0f) / Mathf.Max(1, owner2.MaxHp):P2}\n{(float)(owner2.Energy ?? 0f) / UnitDefine.MAX_ENERGY:P2}";
#endif
                }
                catch
                {

                }
                yield return null;
            }
        }
        public void SetPartsBossData()
        {
            bossPartsList = owner2.BossPartsList;
            var orderedParts = new List<PartsData>(bossPartsList);
            orderedParts.Sort((left, right) => left.PositionX.CompareTo(right.PositionX));

            var displayXByPart = new Dictionary<PartsData, float>();
            float displayXSum = 0f;
            float previousDisplayX = 0f;
            for (int index = 0; index < orderedParts.Count; index++)
            {
                PartsData partsData = orderedParts[index];
                float sourceX = owner2.gameObject.transform.position.x + partsData.PositionX / 540.0f;
                float displayX = index == 0
                    ? sourceX
                    : Mathf.Max(sourceX, previousDisplayX + PartsDisplayMinimumSpacing);

                displayXByPart.Add(partsData, displayX);
                displayXSum += displayX;
                previousDisplayX = displayX;
            }

            float centerOffset = orderedParts.Count == 0
                ? 0f
                : displayXSum / orderedParts.Count - owner2.gameObject.transform.position.x;
            foreach(PartsData partsData in bossPartsList)
            {
                GameObject a = Instantiate(partsDataPrefab);
                a.transform.SetParent(owner2.gameObject.transform, false);
                a.transform.position = new Vector3(displayXByPart[partsData] - centerOffset, poszFix, transform.position.z);
                a.GetComponent<PartsdataUI>().Init(partsData);
            }
        }

    }
}