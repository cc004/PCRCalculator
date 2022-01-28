using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Battle
{

    public class CharacterBuffUIController : MonoBehaviour
    {
        public List<SpriteRenderer> abnormalIcons;
        public SpriteRenderer rular;
        public TextMesh posText;
        public GameObject hpBar;
        public TextMesh hpHeadText;
        public GameObject partsDataPrefab;
        public float poszFix;
        
        private Elements.UnitCtrl owner2;
        private BattleUIManager uIManager;
        private List<eStateIconType> currentBuffs = new List<eStateIconType>();


        private List<Elements.PartsData> bossPartsList = new List<Elements.PartsData>();
        
        public void SetBuffUI(Sprite sprite, Elements.UnitCtrl owner,bool isSummon = false)
        {
            //rular.sprite = sprite;
            this.owner2 = owner;
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
                Reflash();
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
            Reflash();
        }
        public void SetHPBar(float normalizedHP)
        {
            int hpInt = Mathf.RoundToInt(Mathf.Min(1, Mathf.Max(0, normalizedHP)) * 100);
            hpHeadText.text = "HP:" + hpInt + "%";
        }
        public void SetAbnormalIcons(Elements.UnitCtrl unitCtrl, Elements.eStateIconType stateIconType_2, bool enable)
        {
            eStateIconType stateIconType = (eStateIconType)(int)stateIconType_2;
            if (stateIconType == eStateIconType.NONE)
            {
                Reflash();
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
            Reflash();
        }
        private void Reflash()
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
                    var mid = owner2.transform.position.x / owner2.transform.lossyScale.x;
                    var r = mid + owner2.BodyWidth / 2;
                    var l = mid - owner2.BodyWidth / 2;
                    posText.text = $"{Mathf.RoundToInt(l)}/{Mathf.RoundToInt(r)}\n{(float)(owner2.Hp ?? 0f) / Mathf.Max(1, owner2.MaxHp):P2}\n{(float)(owner2.Energy ?? 0f) / Elements.UnitDefine.MAX_ENERGY:P2}";
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
            foreach(Elements.PartsData partsData in bossPartsList)
            {
                GameObject a = Instantiate(partsDataPrefab);
                Vector3 tansFix = new Vector3(partsData.PositionX / 540.0f,0, 0 );
                a.transform.SetParent(owner2.gameObject.transform, false);
                a.transform.position = owner2.gameObject.transform.position+ tansFix;
                a.transform.position = new Vector3(a.transform.position.x, poszFix, a.transform.position.z);
                a.GetComponent<PartsdataUI>().Init(partsData);
            }
        }

    }
}