using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elements;
using Elements.Battle;
using PCRCaculator.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;
using Debug = UnityEngine.Debug;
using eStateIconType = Elements.eStateIconType;

namespace PCRCaculator
{


    public class CharacterPageButton : MonoBehaviour
    {
        private static readonly Color grayColor = Color.gray; 
        public enum ButtonType { typeA = 0, typeB = 1 ,typeC = 2,typeD=3}//按钮样式，A-长条，B-图标,C-战斗场景用角色状态图标,D-截图用
        public ButtonType buttonType = ButtonType.typeA;
        public bool showPosition;//是否显示位置图标
        public Stars stars;
        public Text levelText, hpPercent, tpPercent;
        public TextMeshProUGUI levelText_B;
        public Image characterImage;
        public List<Sprite> backgrounds;//0-4
        public static List<Sprite> backgrounds1;
        public Image characterPositionImage_B;
        public List<Sprite> positionSprites_B;//0-2
        public Image backImage;
        public Slider HPSlider;
        public Slider TPSlider;
        public Image TPFillImage;
        public Color TPFullColor;
        //public List<Image> abnormalIcons;
        public GameObject buffUIPrefab;
        public Transform buffUIParent;
        public Vector3 buffUIBasePos;
        public Vector3 buffUIAddPos;
        public SkillUI skillUI;
        public RectTransform ScreenShotTrans;
        
        private UnitCtrl owner2;
        private BattleUIManager uIManager;
        //private List<eStateIconType> currentBuffs = new List<eStateIconType>();
        private List<BattleBuffUI> battleBuffUIs = new List<BattleBuffUI>();

        public Dictionary<eStateIconType, BattleBuffUI>
            battleBuffUIDic = new Dictionary<eStateIconType, BattleBuffUI>();

        public void Awake()
        {
            backgrounds1 = new List<Sprite>();
            var sprites = Resources.LoadAll<Sprite>("AtlasCommon_3")
                .ToDictionary(x => x.name, x => x);
            backgrounds1.Add(sprites["common_unit_frame_blue"]);
            backgrounds1.Add(sprites["common_unit_frame_bronze"]);
            backgrounds1.Add(sprites["common_unit_frame_silver"]);
            backgrounds1.Add(sprites["common_unit_frame_gold"]);
            backgrounds1.Add(sprites["common_unit_frame_purple"]);
            backgrounds1.Add(sprites["common_unit_frame_red"]);
            backgrounds1.Add(sprites["common_unit_frame_green"]);
            backgrounds1.Add(sprites["common_unit_frame_orange"]);
        }

        /// <summary>
        /// 设置组件
        /// </summary>
        /// <param name="unitid">角色序号</param>
        public void SetButton(int unitid, Sprite sprite = null)
        {
            if (unitid >= 200000 || unitid <= 0)
            {
                stars.SetStars(-1);
                if (buttonType == ButtonType.typeA)
                {
                    levelText.text = "";
                }
                else if (buttonType == ButtonType.typeB)
                {
                    levelText_B.text = "";
                    if (characterPositionImage_B != null)
                    {
                        characterPositionImage_B.gameObject.SetActive(false);
                    }
                }
                backImage.sprite = backgrounds1[0];
                characterImage.sprite = sprite;
                if(unitid/100000!=4)
                return;
            }
            UnitData data = MainManager.Instance.unitDataDic.TryGetValue(unitid, out UnitData unitData) ? unitData : new UnitData(unitid,1);
            SetButton(data);
        }

        /// <summary>
        /// 设置组件
        /// </summary>
        /// <param name="data">角色详情</param>
        public void SetButton(UnitData data)
        {
            int unitid = data.unitId;
            if (unitid == 0) return; // XXX: temp fix
            int charid = data.rarity >= 3 ? unitid + 30 : unitid + 10;
            if (unitid >= 400000)
            {
                charid = unitid + 30;
            }
            //string path = "";
            Sprite sprite = null;
            if (buttonType == ButtonType.typeA)
            {
                //path = "pictures/unit_plate_" + charid;
                sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色长条, unitid);
            }
            else if (buttonType == ButtonType.typeB || buttonType == ButtonType.typeC||buttonType== ButtonType.typeD)
            {
                //path = "charicons/fav_push_notif_" + unitid;
                sprite = ABExTool.GetSprites(ABExTool.SpriteType.角色图标, unitid);
            }
            //Sprite sprite = MainManager.LoadSourceSprite(path);
            //if (sprite != null)
            //{
            //   characterImage.sprite = MainManager.LoadSourceSprite(path);
            //}
            characterImage.sprite = sprite;
            RefreshData(data);
        }

        private long current;

        public void RefreshData(UnitData data)
        {
            int unitid = data.unitId;

            if (data.GetHashCode() == current) return;

            current = data.GetHashCode();

            stars.SetStars(unitid >= 200000 ? -1 : data.rarity, data.GetMaxRarity(), true);
            int backtype = 0;
            if (data.rank <= 1)
            {
                backtype = 0;
            }
            else if (data.rank <= 3)
            {
                backtype = 1;
            }
            else if (data.rank <= 6)
            {
                backtype = 2;
            }
            else if (data.rank <= 10)
            {
                backtype = 3;
            }
            else if (data.rank <= 17)
            {
                backtype = 4;
            }
            else if (data.rank <= 20)
            {
                backtype = 5;
            }
            else if (data.rank <= 23)
            {
                backtype = 6;
            }
            else if (data.rank <= 27)
            {
                backtype = 7;
            }
            else
            {
                backtype = 8;
            }
            backImage.sprite = backgrounds1[backtype < backgrounds1.Count ? backtype : (backgrounds1.Count - 1)];
            if (buttonType == ButtonType.typeA)
            {
                levelText.text = "等级" + data.level;
            }
            else if (buttonType == ButtonType.typeB || buttonType == ButtonType.typeC || buttonType == ButtonType.typeD)
            {
                if (unitid <= 200000)
                {
                    levelText_B.gameObject.SetActive(true);
                    UnitRarityData rarityData = MainManager.Instance.UnitRarityDic[unitid];
                    //levelText_B.text = "lv:" + data.level;
                    levelText_B.text = "" + Mathf.RoundToInt(rarityData.GetPowerValue(data));
                    if (showPosition)
                    {
                        PositionType k = rarityData.unitPositionType;
                        characterPositionImage_B.sprite = positionSprites_B[(int)k];
                    }
                }
                else
                    levelText_B.gameObject.SetActive(false);

            }
        }

        public void SetButton(UnitCtrl unitCtrl)
        {
            if (buttonType != ButtonType.typeC&&buttonType!= ButtonType.typeD)
            {
                Debug.LogError("按钮样式错误！");
                return;
            }
            SetButton(unitCtrl.unitData);
            if (buttonType == ButtonType.typeD)
            {
                backImage.gameObject.SetActive(unitCtrl.UnitId <= 200000);
                return;
            }
            owner2 = unitCtrl;
            uIManager = BattleUIManager.Instance;
            //owner2.OnChangeState += SetAbnormalIcons_2;
            //SetAbnormalIcons(owner, eStateIconType.NONE, false);
            owner2.MyOnChangeAbnormalState += SetAbnormalIcons;
            owner2.button = this;
            SetHPAndTP(1, 0);
            skillUI.Init(unitCtrl.unitParameter.SkillData.MainSkillIds.ToArray(),unitCtrl.unitParameter.SkillData.main_skill_evolution_1, unitCtrl.unitParameter.SkillData.main_skill_evolution_2);
            skillUI.Init(unitCtrl.unitParameter.SkillData.SpSkillIds.ToArray(), unitCtrl.unitParameter.SkillData.sp_skill_evolution_1, unitCtrl.unitParameter.SkillData.sp_skill_evolution_2);
            owner2.MyOnChangeSkillID += skillUI.SetSkillState;
            owner2.MyOnSkillCD += skillUI.SetCastTime;
            owner2.MyOnUsingSkill += skillUI.SetPlayTime;
        }

        public void SetHPAndTP(float normalizedHPRate,float normalizedTPRate)
        {
            HPSlider.value = normalizedHPRate;
            SetTP(normalizedTPRate);
        }
        public void SetHP(float normalizedHPRate)
        {
            HPSlider.value = normalizedHPRate;
            hpPercent.text = $"{(int)(10000 * normalizedHPRate) / 10000f:P2}";
            if (normalizedHPRate <= 0)
            {
                SetDie();
            }
        }
        public void SetTP(float normalizedTPRate)
        {
            TPSlider.value = normalizedTPRate;
            tpPercent.text = $"{(int)(10000 * normalizedTPRate) / 10000f:P2}";
            if (normalizedTPRate >= 1)
            {
                TPFillImage.color = TPFullColor;
            }
            else
            {
                TPFillImage.color = Color.white;
            }
        }
        private void SetDie()
        {
            characterImage.color = grayColor;
            backImage.color = grayColor;
            TPSlider.value = 0;
            stars.SetStarColor(grayColor);
        }

        public static Dictionary<eStateIconType, int> SetAbnormalIconsCounter = new Dictionary<eStateIconType, int>();

        public void SetSealIcons(UnitCtrl unitCtrl, eStateIconType stateIconType_2)
        {
            if (SetAbnormalIconsCounter.ContainsKey(stateIconType_2))
                SetAbnormalIconsCounter[stateIconType_2]++;
            else
                SetAbnormalIconsCounter[stateIconType_2] = 1;

            if (BattleManager.Instance.skipping) return;

            if (BattleUIManager.Instance.ShowBuffDic.TryGetValue(eStateIconType.NONE, out bool value))
            {
                if (value)
                    return;
            }
            if (BattleUIManager.Instance.ShowBuffDic.TryGetValue(stateIconType_2, out bool value2))
            {
                if (!value2)
                    return;
            }

            if (battleBuffUIDic.ContainsKey(stateIconType_2)) return;

            GameObject a = Instantiate(buffUIPrefab);
            a.transform.SetParent(buffUIParent, false);
            BattleBuffUI buffUI = a.GetComponent<BattleBuffUI>();
            buffUI.Init(uIManager.GetAbnormalIconSprite((Battle.eStateIconType)(int)stateIconType_2), stateIconType_2);
            battleBuffUIDic[stateIconType_2] = buffUI;
            battleBuffUIs.Add(buffUI);
            RefreshBattleBuffUIs();
        }

        public void SetAbnormalIcons(UnitCtrl unitCtrl, eStateIconType stateIconType_2, bool enable,float stayTime = 90,string describe = "???")
        {
            if (SetAbnormalIconsCounter.ContainsKey(stateIconType_2))
                SetAbnormalIconsCounter[stateIconType_2]++;
            else
                SetAbnormalIconsCounter[stateIconType_2] = 1;

            if (BattleManager.Instance.skipping) return;
            if (stayTime == 0) stayTime = unitCtrl.fieldTime;
            if(stateIconType_2 == eStateIconType.NONE) { return; }
            if (BattleUIManager.Instance.ShowBuffDic.TryGetValue(eStateIconType.NONE, out bool value))
            {
                if (value)
                    return;
            }
            if (BattleUIManager.Instance.ShowBuffDic.TryGetValue(stateIconType_2,out bool value2))
            {
                if (!value2)
                    return;
            }

            if (enable)
            {
                GameObject a = Instantiate(buffUIPrefab);
                a.transform.SetParent(buffUIParent,false);
                BattleBuffUI buffUI = a.GetComponent<BattleBuffUI>();
                buffUI.Init(uIManager.GetAbnormalIconSprite((Battle.eStateIconType)(int)stateIconType_2),stateIconType_2,stayTime,describe,RemoveBuffIcon);
                battleBuffUIs.Add(buffUI);
                RefreshBattleBuffUIs();
            }
            else
            {
                int idx = 0;
                if (stateIconType_2 == eStateIconType.SLOW || stateIconType_2 == eStateIconType.HASTE)
                {
                    idx = battleBuffUIs.FindIndex(a => a.stateIconType == eStateIconType.SLOW || a.stateIconType == eStateIconType.HASTE);
                }
                else
                {
                    idx = battleBuffUIs.FindIndex(
                    a => a.stateIconType == stateIconType_2 &&
                    a.describe == describe);
                }
                if (idx >= 0)
                {
                    Destroy(battleBuffUIs[idx].gameObject);
                    battleBuffUIs.RemoveAt(idx);
                    RefreshBattleBuffUIs();
                }
            }
        }
        public void RefreshBattleBuffUIs()
        {
            for(int i = 0; i < battleBuffUIs.Count; i++)
            {
                battleBuffUIs[i].transform.localPosition = buffUIBasePos + buffUIAddPos * i;
            }
        }
        public void RemoveBuffIcon(BattleBuffUI obj)
        {
            if (battleBuffUIs.Contains(obj))
            {
                Destroy(obj.gameObject);
                battleBuffUIs.Remove(obj);
                RefreshBattleBuffUIs();
            }
        }
        public void ShowContinousPress(bool show)
        {
            levelText.gameObject.SetActive(show);
        }
        public void HideBackImageTemp()
        {
            StartCoroutine(HideBack_0());
        }
        private IEnumerator HideBack_0()
        {
            backImage.gameObject.SetActive(false);
            for (int i = 0; i < 4; i++)
                yield return 0;
            backImage.gameObject.SetActive(true);
        }
        /*public void SetAbnormalIcons(UnitCtrl unitCtrl, eStateIconType stateIconType, bool enable)
        {
            Debug.LogError("改方法已经过时！");
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
        }*/
        /*public void SetAbnormalIcons_2(Elements.UnitCtrl unitCtrl, Elements.eStateIconType stateIconType_2, bool enable)
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
        }*/
        /*private void Refresh()
        {
            for (int i = 0; i < 4; i++)
            {
                if (i < currentBuffs.Count)
                {
                    abnormalIcons[i].sprite = uIManager.GetAbnormalIconSprite(currentBuffs[i]);
                    abnormalIcons[i].gameObject.SetActive(true);
                }
                else
                {
                    abnormalIcons[i].sprite = null;
                    abnormalIcons[i].gameObject.SetActive(false);
                }
            }
        }*/

    }
}