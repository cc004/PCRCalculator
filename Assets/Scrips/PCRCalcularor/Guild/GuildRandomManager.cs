using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;

namespace PCRCaculator.Guild
{
    public class GuildRandomManager : MonoBehaviour
    {
       // public Dropdown calPageDropDown;
        //public Text calPageText;
        public GameObject BasePage;     
        //public Dropdown TimeLineDropDown;
        public Text timeLineNameText;
        public InputField RandomSeed;
        public List<Toggle> RandomSettingToggles;
        public GameObject SpecialSettingPrefab;
        public RectTransform SettingParent;
        public Vector3 basePos;
        public Vector3 addPos;
        public Vector2 parentBaseSize;
        public GameObject AddNewTimelinePrefab;

        public GuildSpecialSetting GuildSpecialSetting;
        

        private List<GameObject> prefabs = new List<GameObject>();
        private GuildRandomData randomData;

        public GuildRandomData RandomData { get => randomData; set => randomData = value; }

        public void CancelButton()
        {
            BasePage.SetActive(false);
        }
        public void DeleteButton()
        {
            /*if (GuildManager.Instance.SettingData.guildRandomDatas.Count <= 1 || GuildManager.Instance.SettingData.currentRandomLine == 0)
            {
                MainManager.Instance.WindowConfigMessage("默认世界性无法删除！",null,null);
            }
            else
            {
                MainManager.Instance.WindowConfigMessage("确定要删除当前世界线吗？", DeleteTimeLine_0, null);
            }*/
        }
        private void DeleteTimeLine_0()
        {
            /*GuildManager.Instance.SettingData.guildRandomDatas.RemoveAt(GuildManager.Instance.SettingData.currentRandomLine);
            GuildManager.Instance.SettingData.currentRandomLine = 0;
            Reflash();*/
        }
        public void SaveButton()
        {
            randomData.UseFixedRandomSeed = RandomSettingToggles[0].isOn;
            randomData.RandomSeed = int.Parse(RandomSeed.text);
            randomData.ForceNoCritical_player = RandomSettingToggles[1].isOn;
            randomData.ForceNoCritical_enemy = RandomSettingToggles[2].isOn;
            randomData.ForceIgnoreDodge_player = RandomSettingToggles[3].isOn;
            randomData.ForceIgnoreDodge_enemy = RandomSettingToggles[4].isOn;
            randomData.ForceCritical_player = RandomSettingToggles[5].isOn;

            BasePage.SetActive(false);
            GuildManager.Instance.SaveDataToJson();
        }

        public void AddNewTimeLine()
        {
            /*GameObject a = Instantiate(AddNewTimelinePrefab);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            a.GetComponent<OpenInputFieldPrefab>().OnFinish = AddNewTimeLine_0;*/
        }
        private void AddNewTimeLine_0(string name)
        {
            /*GuildManager.Instance.SettingData.guildRandomDatas.Add(new GuildRandomData(name));
            GuildManager.Instance.SettingData.currentRandomLine = GuildManager.Instance.SettingData.guildRandomDatas.Count - 1;
            Reflash();*/
        }
        public void RenameButton()
        {
            MainManager.Instance.WindowInputMessage("请输入新名字", Rename_0);
        }
        private void Rename_0(string name)
        {
            randomData.DataName = name;
        }
        public void AddNewSpecialSetting()
        {
            //MainManager.Instance.WindowConfigMessage("在做了", null, null);
            //return;
            if(randomData.randomSpecialDatas == null)
            {
                randomData.randomSpecialDatas = new List<GuildRandomSpecialData>();
            }
            randomData.randomSpecialDatas.Add(new GuildRandomSpecialData());
            Reflash();
        }
        public void OpenSettingPage()
        {
            BasePage.SetActive(true);
            Reflash();
        }
        /*public void OnDropDownChoosed()
        {
            GuildManager.Instance.SettingData.currentRandomLine = calPageDropDown.value;
            randomData = GuildManager.Instance.SettingData.GetCurrentRandomData();
            calPageText.text = randomData.GetDescribe();
            timeLineNameText.text = randomData.DataName;

        }*/
        public void Reflash()
        {
            randomData = GuildManager.Instance.SettingData.GetCurrentRandomData();
            //calPageText.text = randomData.DataName;
            timeLineNameText.text = randomData.DataName;
            //calPageDropDown.options.Clear();
            /*Dropdown.OptionData optionData;
            for(int i=0;i< GuildManager.Instance.SettingData.guildRandomDatas.Count; i++)
            {
                optionData = new Dropdown.OptionData();
                optionData.text = GuildManager.Instance.SettingData.guildRandomDatas[i].DataName;
                calPageDropDown.options.Add(optionData);                
            }
            calPageDropDown.value = GuildManager.Instance.SettingData.currentRandomLine;*/
            RandomSettingToggles[0].isOn = randomData.UseFixedRandomSeed;
            RandomSeed.text = "" + randomData.RandomSeed;
            RandomSettingToggles[1].isOn = randomData.ForceNoCritical_player;
            RandomSettingToggles[2].isOn = randomData.ForceNoCritical_enemy;
            RandomSettingToggles[3].isOn = randomData.ForceIgnoreDodge_player;
            RandomSettingToggles[4].isOn = randomData.ForceIgnoreDodge_enemy;
            RandomSettingToggles[5].isOn = randomData.ForceCritical_player;
            ReSetSpecialButtons(randomData.randomSpecialDatas);

        }
        private void ReSetSpecialButtons(List<GuildRandomSpecialData> list)
        { 
            foreach(GameObject a in prefabs)
            {
                Destroy(a);
            }
            prefabs.Clear();
            SettingParent.sizeDelta = parentBaseSize;
            if (list == null) { list = new List<GuildRandomSpecialData>(); }
            int i;
            for(i = 0; i < list.Count; i++)
            {
                GameObject a = Instantiate(SpecialSettingPrefab);
                a.transform.SetParent(SettingParent, false);
                a.transform.localPosition = basePos + i * addPos;
                int idx = i;
                a.GetComponent<Button>().onClick.AddListener(delegate { EditSpecialData(idx); });
                a.GetComponentInChildren<Text>().text = list[i].GetDescribe();
                prefabs.Add(a);
            }
            float height = Mathf.Abs((basePos + i * addPos).y);
            if (SettingParent.sizeDelta.y<height)
            {
                SettingParent.sizeDelta = new Vector2(parentBaseSize.x, height);
            }
        }
        public void EditSpecialData(int idx)
        {
            GuildSpecialSetting.OpenAndReflash(randomData.randomSpecialDatas[idx]);
        }
    }
    public class GuildRandomData
    {
        public string DataName = "默认世界线";
        public bool UseFixedRandomSeed = true;
        public int RandomSeed = 666;
        public bool ForceNoCritical_player;
        public bool ForceNoCritical_enemy;
        public bool ForceIgnoreDodge_player;
        public bool ForceIgnoreDodge_enemy;
        public bool ForceCritical_player;
        public List<GuildRandomSpecialData> randomSpecialDatas = new List<GuildRandomSpecialData>();

        public GuildRandomData() { }
        public GuildRandomData(string dataName)
        {
            DataName = dataName;
        }
        public string GetDescribe()
        {
            string des = UseFixedRandomSeed ? "随机固定" : "正常随机";
            if (ForceNoCritical_player || ForceNoCritical_enemy)
            {
                if (ForceNoCritical_player)
                    des += "-" + (ForceNoCritical_enemy ? "双" : "己") + "方不暴击";
                else
                    des += "-敌方不暴击";
            }
            if (ForceIgnoreDodge_enemy || ForceIgnoreDodge_player)
            {
                if (ForceIgnoreDodge_player)
                    des += "-" + (ForceIgnoreDodge_enemy ? "双" : "己") + "方伤害必中";
                else
                    des += "-敌方伤害必中";
            }
            return des;
        }
        public bool TryJudgeRandomSpecialSetting(Elements.UnitCtrl source,Elements.UnitCtrl target,Elements.Skill skill,Elements.eActionType actionType,int currrentFrame,out float randomResult)
        {
            randomResult = 0;
            bool result = false;
            if (randomSpecialDatas == null || randomSpecialDatas.Count <= 0)
            {
                return result;
            }
            foreach(var a in randomSpecialDatas)
            {
                if(a.TryJudgeRandomSpecialSetting(source,target,skill,actionType,currrentFrame,out float randomOut))
                {
                    randomResult = randomOut;
                    result = true;
                }
            }
            return result;
        }
    }
    public class GuildRandomSpecialData
    {
        public bool fixTimeExec;
        public int startFream;
        public int endFream;
        public bool fixCountExec;
        public int countEcexNum;
        public enum UnitType
        {
            [Description("BOSS")]
            BOSS = 0,
            [Description("己方一号位")]
            PLAYER1 = 1,
            [Description("己方二号位")]
            PLAYER2 = 2,
            [Description("己方三号位")]
            PLAYER3 = 3,
            [Description("己方四号位")]
            PLAYER4 = 4,
            [Description("己方五号位")]
            PLAYER5 = 5,
            [Description("己方全体")]
            ALLPLAYER = 6 }
        public UnitType sourceNum;
        public enum skillNameType
        {
            UB = 0,
            [Description("技能1")]
            SKILL1 = 1,
            [Description("技能2")]
            SKILL2 = 2,
            [Description("普攻")]
            ATK = 3
        }
        public skillNameType sourceSkillNum;
        public enum skillType
        {
            [Description("攻击类")]
            ATK = 0,
            [Description("减速类")]
            ChangeSpeed = 1,
            [Description("咕咕咕")]
            KNOCK = 2,
            [Description("DOT伤害")]
            SLIP_DAMAGE = 3,
            [Description("致盲黑暗类")]
            BLIND=4

        }
        public skillType sourceSkillType;
        public UnitType targetNum;
        public enum ResultType
        {
            [Description("正常随机")]
            RANDOM = 0,
            [Description("必中")]
            FORCE_ACC = 1,
            [Description("必爆")]
            FORCE_CRI = 2,
            [Description("必MISS")]
            FORCE_MISS = 3,
            [Description("必不爆")]
            FORCE_NORMAL = 4
        }
        public ResultType resuleType;

        public GuildRandomSpecialData()
        {

        }

        public GuildRandomSpecialData(bool fixTimeExec, int startFream, int endFream, bool fixCountExec, int countEcexNum, UnitType sourceNum, skillNameType sourceSkillNum, skillType sourceSkillType, UnitType targetNum, ResultType resuleType)
        {
            this.fixTimeExec = fixTimeExec;
            this.startFream = startFream;
            this.endFream = endFream;
            this.fixCountExec = fixCountExec;
            this.countEcexNum = countEcexNum;
            this.sourceNum = sourceNum;
            this.sourceSkillNum = sourceSkillNum;
            this.sourceSkillType = sourceSkillType;
            this.targetNum = targetNum;
            this.resuleType = resuleType;
        }
        public string GetDescribe()
        {
            string des = "";
            if (fixTimeExec)
            {
                des += startFream + "-" + endFream + "帧" + "-";
            }
            if (fixCountExec)
            {
                des += "第" + countEcexNum + "次";
            }
            des += sourceNum.GetDescription() + "的" + sourceSkillNum.GetDescription();
            des += "对" + targetNum.GetDescription() + "的" + sourceSkillType.GetDescription() + "效果";
            des += resuleType.GetDescription();
            return des;
        }
        public bool TryJudgeRandomSpecialSetting(Elements.UnitCtrl source, Elements.UnitCtrl target, Elements.Skill skill, Elements.eActionType actionType, int currrentFrame, out float randomResult)
        {
            randomResult = 0;
            if(fixTimeExec)
            if (currrentFrame < startFream || currrentFrame > endFream)
            {
                return false;
            }
            if (fixCountExec)
            {
                if (source.MySkillExecDic.TryGetValue(skill.SkillId, out int count))
                {
                    if (count != countEcexNum)
                        return false;
                }
                else
                    return false;
            }
            switch (sourceNum)
            {
                case UnitType.BOSS:
                    if (!source.IsBoss)
                    {
                        return false;
                    }
                    break;
                case UnitType.PLAYER1:
                case UnitType.PLAYER2:
                case UnitType.PLAYER3:
                case UnitType.PLAYER4:
                case UnitType.PLAYER5:
                    if (source.IsBoss)
                    {
                        return false;
                    }
                    if ((int)sourceNum - 1 != source.posIdx)
                    {
                        return false;
                    }
                    break;
                case UnitType.ALLPLAYER:
                    if (source.IsBoss)
                    {
                        return false;
                    }
                    break;
            }
            if(skill == null)
            {
                return false;
            }
            if (!source.JudgeIsTargetSkill(skill.SkillId, (int)sourceSkillNum))
            {
                return false;
            }
            switch (sourceSkillType)
            {
                case skillType.ATK:
                    if(actionType!= Elements.eActionType.ATTACK)
                    {
                        return false;
                    }
                    break;
                case skillType.ChangeSpeed:
                    if (actionType != Elements.eActionType.CHANGE_SPEED)
                    {
                        return false;
                    }
                    break;
                case skillType.KNOCK:
                    if (actionType != Elements.eActionType.KNOCK)
                    {
                        return false;
                    }
                    break;
                case skillType.SLIP_DAMAGE:
                    if(actionType != Elements.eActionType.SLIP_DAMAGE)
                    {
                        return false;
                    }
                    break;
                case skillType.BLIND:
                    if(actionType != Elements.eActionType.BLIND)
                    {
                        return false;
                    }
                    break;
            }
            switch (targetNum)
            {
                case UnitType.BOSS:
                    if (!target.IsBoss)
                    {
                        return false;
                    }
                    break;
                case UnitType.PLAYER1:
                case UnitType.PLAYER2:
                case UnitType.PLAYER3:
                case UnitType.PLAYER4:
                case UnitType.PLAYER5:
                    if (target.IsBoss)
                    {
                        return false;
                    }
                    if ((int)targetNum - 1 != target.posIdx)
                    {
                        return false;
                    }
                    break;
                case UnitType.ALLPLAYER:
                    if (target.IsBoss)
                    {
                        return false;
                    }
                    break;
            }
            switch (resuleType)
            {
                case ResultType.RANDOM:
                    return false;
                case ResultType.FORCE_ACC:
                case ResultType.FORCE_NORMAL:
                    randomResult = 1;
                    break;
                case ResultType.FORCE_MISS:
                case ResultType.FORCE_CRI:
                    randomResult = 0;
                    break;


            }

            return true;
        }

    }
}