using System;
using System.Collections.Generic;
using Elements;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{
    public class GuildExecTimeSetting : MonoBehaviour
    {
        public GameObject basePannel;
        public GameObject pannelA;
        public GameObject skillButtonPrefab;
        public RectTransform parentA;
        public Vector3 basePosA;
        public Vector2 parentSizeBase;

        public GameObject pannelB;
        public List<Text> pannelBHeadTexts;
        public GameObject actionNormalPrefab;
        public GameObject actionComboPrefab;
        public GameObject actionFirearmPrefab;
        public RectTransform parentB;
        public Vector3 basePosB;
        public Vector2 parentSizeBaseB;

        private int unitId;
        //private UnitData unitData;
        //private EnemyData enemyData;
        private SystemIdDefine.eWeaponMotionType motionType;
        //private Elements.UnitPrefabData prefabData;
        //private Elements.UnitActionControllerData2 actionControllerData;
        private UnitActionController actionController;

        private Dictionary<string, List<FirearmCtrlData>> unitFirearmDatas = new Dictionary<string, List<FirearmCtrlData>>();
        private int currentPosY;
        private List<GameObject> skillButtonPrefabList = new List<GameObject>();

        private int currentPosY_2;
        private List<GuildExecTimeButton> actionPrefabList = new List<GuildExecTimeButton>();

        private GameObject unitPrefab;

        public UnitActionController ActionController { get => actionController; set => actionController = value; }

        public void Init(int unitid)
        {
            basePannel.SetActive(true);
            pannelA.SetActive(true);
            pannelB.SetActive(false);
            unitId = unitid;
            //this.unitData = unitData;
            motionType = (SystemIdDefine.eWeaponMotionType)MainManager.Instance.UnitRarityDic[unitId].detailData.motionType;
            LoadActionControllerData();
            foreach(GameObject a in skillButtonPrefabList)
            {
                Destroy(a);
            }
            skillButtonPrefabList.Clear();
            currentPosY = (int)basePosA.y;
            parentA.sizeDelta = parentSizeBase;
            AddButton("普攻", 1, "攻击类", 1, () => { OpenDetailPage(1,0,1,new[] { "普攻", "1", "攻击类", "1" }); });
            int[] skillList = MainManager.Instance.UnitRarityDic[unitId].skillData.GetSkillList();
            //SkillData skillData_UB = MainManager.Instance.SkillDataDic[skillList[0]];
            //string skillName = MainManager.Instance.SkillNameAndDescribe_cn.TryGetValue(skillList[0], out string[] names) ? names[0] : skillData_UB.name;
            void action(int a,int SkillType)
            {
                SkillData skillData = MainManager.Instance.SkillDataDic[a];
                string skillName = MainManager.Instance.SkillNameAndDescribe_cn.TryGetValue(a, out string[] names) ? names[0] : skillData.name;
                foreach (int actionid in skillData.skillactions)
                {
                    if (actionid > 0)
                    {
                        int actonType = MainManager.Instance.SkillActionDic[actionid].type;
                        AddButton(skillName, a, ((eActionType)actonType).GetDescription(), actionid,
                            () => { OpenDetailPage(actionid,SkillType,actonType,new[] { skillName,a+"", ((eActionType)actonType).GetDescription() ,actionid + ""}); });
                    }
                }
            }
            action(skillList[0],1);
            action(skillList[1],2);
            action(skillList[2],3);

        }
        public void Init(EnemyData enemyData)
        {
            basePannel.SetActive(true);
            pannelA.SetActive(true);
            pannelB.SetActive(false);
            unitId = enemyData.unit_id;
            //this.enemyData = enemyData;
            motionType = (SystemIdDefine.eWeaponMotionType)enemyData.detailData.motion_type;
            LoadActionControllerData();
            foreach (GameObject a in skillButtonPrefabList)
            {
                Destroy(a);
            }
            skillButtonPrefabList.Clear();
            currentPosY = (int)basePosA.y;
            parentA.sizeDelta = parentSizeBase;
            AddButton("普攻", 1, "攻击类", 1, () => { OpenDetailPage(1, 0, 1, new[] { "普攻", "1", "攻击类", "1" }); });
            int[] skillList = enemyData.skillData.MainSkills.ToArray();
            //SkillData skillData_UB = MainManager.Instance.SkillDataDic[skillList[0]];
            //string skillName = MainManager.Instance.SkillNameAndDescribe_cn.TryGetValue(skillList[0], out string[] names) ? names[0] : skillData_UB.name;
            void action(int a, int SkillType)
            {
                SkillData skillData = MainManager.Instance.SkillDataDic[a];
                string skillName = MainManager.Instance.SkillNameAndDescribe_cn.TryGetValue(a, out string[] names) ? names[0] : skillData.name;
                foreach (int actionid in skillData.skillactions)
                {
                    if (actionid > 0)
                    {
                        int actonType = MainManager.Instance.SkillActionDic[actionid].type;
                        AddButton(skillName, a, ((eActionType)actonType).GetDescription(), actionid,
                            () => { OpenDetailPage(actionid, SkillType, actonType, new[] { skillName, a + "", ((eActionType)actonType).GetDescription(), actionid + "" }); });
                    }
                }
            }
            action(enemyData.skillData.UB, 1);
            for (int j = 0;j < skillList.Length;j++)
            {
                if (skillList[j] > 0)
                    action(skillList[j], j+2);
            }
            /*
            action(skillList[0], 1);
            action(skillList[1], 2);
            action(skillList[2], 3);
            */

        }
        public void ExitButton()
        {
            if (unitPrefab != null)
                Destroy(unitPrefab);
            basePannel.SetActive(false);
        }
        public void ResetButton()
        {
            /*string filePath = PCRCaculator.MainManager.GetSaveDataPath() + "/Guild/UnitPrefabData/" + unitId + ".json";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                MainManager.Instance.WindowConfigMessage("成功！", null, null);
                return;
            }
            MainManager.Instance.WindowConfigMessage("无需重置！", null, null);*/
            if (MainManager.Instance.FirearmData.ResetData(unitId))
                MainManager.Instance.WindowConfigMessage("成功！", null);
            else
                MainManager.Instance.WindowConfigMessage("无需重置！", null);


        }
        public void SaveButton()
        {
            SaveDataToJson();
            ExitButton();
            MainManager.Instance.WindowConfigMessage("成功！", null);
        }
        public void ExitButton_B()
        {
            pannelB.SetActive(false);
        }
        public void SaveButton_B()
        {
            foreach(var a in actionPrefabList)
            {
                a.Save();
            }
            ExitButton_B();
            SaveDataToJson();
            Init(unitId);
            MainManager.Instance.WindowConfigMessage("保存成功！", null);
        }
        /*public void SaveDataToJson()
        {
            string filePath = PCRCaculator.MainManager.GetSaveDataPath() + "/Guild/UnitPrefabData/" + unitId + ".json";
            if (!Directory.Exists(PCRCaculator.MainManager.GetSaveDataPath() + "/Guild"))
            {
                Directory.CreateDirectory(PCRCaculator.MainManager.GetSaveDataPath() + "/Guild");
            }
            if (!Directory.Exists(PCRCaculator.MainManager.GetSaveDataPath() + "/Guild/UnitPrefabData"))
            {
                Directory.CreateDirectory(PCRCaculator.MainManager.GetSaveDataPath() + "/Guild/UnitPrefabData");
            }
            string saveJsonStr = JsonConvert.SerializeObject(prefabData);
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
        }*/
        public void SaveDataToJson()
        {
            MainManager.Instance.FirearmData.SetData(unitId, unitFirearmDatas,null,true);
            MainManager.Instance.SaveAllUnitFriearmData();
        }
        public void OpenDetailPage(int actionId,int skillType,int actionType,string[] headDetails)//0-普攻，1-UB，2-技能1，3-技能2
        {
            pannelB.SetActive(true);
            for(int i=0;i<pannelBHeadTexts.Count;i++)
            {
                pannelBHeadTexts[i].text = headDetails[i];
            }
            //LoadActionControllerData();
            foreach (var a in actionPrefabList)
            {
                Destroy(a.gameObject);
            }
            actionPrefabList.Clear();
            currentPosY_2 = (int)basePosB.y;
            parentB.sizeDelta = parentSizeBaseB;
            int id = 1;
            Skill skill = null;
            string key = "";
            switch (skillType)
            {
                case 0:
                    Dictionary<int,float[]> fireActions = FindFirearmAction(actionController.Attack, actionController.Attack.SkillEffects, true);
                    if (fireActions == null)
                    {
                        return;
                    }
                    if (fireActions.ContainsKey(1))
                    {
                        if(unitFirearmDatas.TryGetValue("attack",out List<FirearmCtrlData> value))
                        {
                            foreach(var a in value)
                            {
                                AddDetailButton(2).Init(a,id++, "弹道技能", fireActions[1], a.MoveRate, 1, a.HitDelay, (int)a.MoveType, a.duration);
                            }
                        }
                    }
                    else
                    {
                        float execTime = 0;
                        if (actionController.UseDefaultDelay && BattleDefine.WEAPON_HIT_DELAY_DIC.TryGetValue(
                           motionType, out execTime))
                        {
                            AddDetailButton(0).Init(new ActionExecTime(),id++, "默认延时", execTime, "普通", 1, "正常", false);
                        }
                        else
                        {
                            foreach(var combo in actionController.AttackDetail.ExecTimeCombo)
                            {
                                AddDetailButton(1).Init(combo,id++, "连击", combo.StartTime, combo.OffsetTime, (int)combo.Weight, combo.Count,(int)combo.InterporationType);
                            }
                            foreach(var normal in actionController.AttackDetail.ExecTimeForPrefab)
                            {
                                AddDetailButton(0).Init(normal,id++, "常规", normal.Time, normal.DamageNumType.GetDescription(), (int)normal.Weight, (normal.DamageNumScale == 1 ? "正常" : "2倍大"), true);
                            }
                        }
                    }
                    return;
                case 1:
                    skill = actionController.UnionBurstList[0];
                    key = "skill0";
                    break;
                /*case 2:
                    skill = actionControllerData.MainSkillList[0];
                    key = "skill1";
                    break;
                case 3:
                    skill = actionControllerData.MainSkillList[1];
                    key = "skill2";
                    break;*/
                default:
                    if (actionController.MainSkillList.Count > skillType - 2)
                    {
                        skill = actionController.MainSkillList[skillType - 2];
                        key = "skill" + (skillType - 1);
                    }
                    else
                    {
                        return;
                    }
                    break;
            }
            Dictionary<int, float[]> fireActions2 = FindFirearmAction(skill,skill.SkillEffects, false);
            if (fireActions2.ContainsKey(actionId))
            {
                if (unitFirearmDatas.TryGetValue(key, out List<FirearmCtrlData> value))
                {
                    foreach (var a in value)
                    {
                        AddDetailButton(2).Init(a,id++, "弹道技能", fireActions2[actionId], a.MoveRate, 1, a.HitDelay, (int)a.MoveType, a.duration);
                    }
                }
            }
            else
            {
                var detail = skill.ActionParametersOnPrefab.Find(a => (int)a.ActionType == actionType).Details.Find(b=> b.ActionId == actionId);
                if (detail == null)
                    return;
                foreach (var combo in detail.ExecTimeCombo)
                {
                    AddDetailButton(1).Init(combo,id++, "连击", combo.StartTime, combo.OffsetTime, (int)combo.Weight, combo.Count, (int)combo.InterporationType);
                }
                foreach (var normal in detail.ExecTimeForPrefab)
                {
                    AddDetailButton(0).Init(normal,id++, "常规", normal.Time, normal.DamageNumType.GetDescription(), (int)normal.Weight, (normal.DamageNumScale == 1 ? "正常" : "2倍大"), true);
                }
            }
        }
        private Dictionary<int,float[]> FindFirearmAction(Skill _skill,
          List<NormalSkillEffect> _skillList,bool isAttack)
        {
            Dictionary<int, float[]> firearmActionDelayDic = new Dictionary<int, float[]>();
            int index1 = 0;
            for (int count = _skillList.Count; index1 < count; ++index1)
            {
                NormalSkillEffect effect = _skillList[index1];                
                switch (effect.EffectBehavior)
                {
                    case eEffectBehavior.FIREARM:
                    case eEffectBehavior.SERIES:
                    case eEffectBehavior.SERIES_FIREARM:
                        float[] delayTime = new float[2] { effect.ExecTime[0], 0 };
                        if (isAttack)
                        {
                            //effect.FireAction = _skill.ActionParameters[0];
                            //effect.FireAction.ReferencedByEffect = true;
                            if(actionController.UseDefaultDelay && effect.EffectBehavior == eEffectBehavior.FIREARM)
                            {
                                if (BattleDefine.WEAPON_EFFECT_DELAY_DIC.TryGetValue(motionType, out delayTime[0]))
                                {
                                    delayTime[1] = 1;
                                }
                            }
                            firearmActionDelayDic.Add(1,delayTime);
                            break;
                        }
                        if (effect.FireActionId != -1)
                        {
                            //effect.FireAction = _skill.ActionParameters.Find((Predicate<ActionParameter>)(e => e.ActionId == effect.FireActionId));
                            //effect.FireAction.ReferencedByEffect = true;
                            firearmActionDelayDic.Add(effect.FireActionId,delayTime);
                        }
                        break;
                }
            }
            return firearmActionDelayDic;
        }
        private void LoadActionControllerData()
        {
            /*string filePath = PCRCaculator.MainManager.GetSaveDataPath() + "/Guild/UnitPrefabData/" + unitId + ".json";
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string jsonStr = sr.ReadToEnd();
                sr.Close();
                if (jsonStr != "")
                {
                    prefabData = JsonConvert.DeserializeObject<Elements.UnitPrefabData>(jsonStr);
                    actionControllerData = prefabData.UnitActionControllerData;
                    unitFirearmDatas = prefabData.unitFirearmDatas;
                    return;
                }
            }
            TextAsset text_0 = Resources.Load<TextAsset>("unitPrefabDatas/UNIT_" + unitId);
            if (text_0 != null && text_0.text != "")
            {
                string json_0 = text_0.text;
                prefabData = JsonConvert.DeserializeObject<Elements.UnitPrefabData>(json_0);
                actionControllerData = prefabData.UnitActionControllerData;
                unitFirearmDatas = prefabData.unitFirearmDatas;
            }
            else
            {
                MainManager.Instance.WindowConfigMessage("角色信息不存在！", null, null);
            }
            */
            /*GameObject a = ABExTool.LoadUnitPrefab(unitId);
            if (a != null)
            {
                unitPrefab = Instantiate(a);
                unitPrefab.SetActive(false);
                actionController = unitPrefab.GetComponent<Elements.UnitActionController>();
            }
            else
            {
                MainManager.Instance.WindowConfigMessage("角色信息不存在！", null, null);
            }*/
            unitFirearmDatas = MainManager.Instance.FirearmData.GetData(unitId);
        }
        private void AddButton(string name,int skillid,string actionname,int actionid,Action buttonAction)
        {
            GameObject a = Instantiate(skillButtonPrefab);
            a.transform.SetParent(parentA, false);
            a.transform.localPosition = new Vector3(basePosA.x, currentPosY, 0);
            currentPosY -= a.GetComponent<GuildExecTimeButton>().ButtonHight;
            if (parentA.sizeDelta.y < Mathf.Abs(currentPosY))
            {
                parentA.sizeDelta = new Vector2(parentA.sizeDelta.x, Mathf.Abs(currentPosY));
            }
            skillButtonPrefabList.Add(a);
            a.GetComponent<GuildExecTimeButton>().Init(name, skillid, actionname, actionid, buttonAction);
        }
        private GuildExecTimeButton AddDetailButton(int buttonType)//0-mormal,1-combo,2-firearm
        {
            GameObject b = null;
            switch (buttonType)
            {
                case 0:
                    b = Instantiate(actionNormalPrefab);
                    break;
                case 1:
                    b = Instantiate(actionComboPrefab);
                    break;
                case 2:
                    b = Instantiate(actionFirearmPrefab);
                    break;
                default:
                    b = Instantiate(actionNormalPrefab);
                    break;
            }
            b.transform.SetParent(parentB, false);
            b.transform.localPosition = new Vector3(basePosB.x, currentPosY_2, 0);
            var b0 = b.GetComponent<GuildExecTimeButton>();
            currentPosY_2 -= b0.ButtonHight;
            if (parentB.sizeDelta.y < Mathf.Abs(currentPosY_2))
            {
                parentB.sizeDelta = new Vector2(parentB.sizeDelta.x, Mathf.Abs(currentPosY_2));
            }
            actionPrefabList.Add(b0);
            return b0;
        }


    }
}