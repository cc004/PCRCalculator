using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{
    public class GuildSpecialSetting : MonoBehaviour
    {
        public GuildRandomManager RandomManager;
        public Toggle fixTimeToggle;
        public InputField startTimeInput;
        public InputField endTimeInput;
        public Toggle fixCountToggle;
        public InputField countInput;
        public List<Dropdown> sourceDropdowns;
        public Dropdown targetDropDown;
        public Dropdown resultDropdown;

        private GuildRandomSpecialData specialData;
        public void CancelButton()
        {
            gameObject.SetActive(false);
        }
        public void DeleteButton()
        {
            RandomManager.RandomData.randomSpecialDatas.Remove(specialData);
            CancelButton();
            RandomManager.Reflash();
        }
        public void SaveButton()
        {
            try
            {
                specialData.fixTimeExec = fixTimeToggle.isOn;
                specialData.startFream = int.Parse(startTimeInput.text);
                specialData.endFream = int.Parse(endTimeInput.text);
                specialData.fixCountExec = fixCountToggle.isOn;
                specialData.countEcexNum = int.Parse(countInput.text);
                specialData.sourceNum = (GuildRandomSpecialData.UnitType)sourceDropdowns[0].value;
                specialData.sourceSkillNum = (GuildRandomSpecialData.skillNameType)sourceDropdowns[1].value;
                specialData.sourceSkillType = (GuildRandomSpecialData.skillType)sourceDropdowns[2].value;
                specialData.targetNum = (GuildRandomSpecialData.UnitType)targetDropDown.value;
                specialData.resuleType = (GuildRandomSpecialData.ResultType)resultDropdown.value;
                CancelButton();
                RandomManager.Reflash();
            }
            catch (Exception e)
            {
                MainManager.Instance.WindowConfigMessage("错误：" + e.Message, null);
            }
        }
        public void OpenAndReflash(GuildRandomSpecialData specialData)
        {
            gameObject.SetActive(true);
            this.specialData = specialData;
            fixTimeToggle.isOn = specialData.fixTimeExec;
            startTimeInput.text = "" + specialData.startFream;
            endTimeInput.text = "" + specialData.endFream;
            fixCountToggle.isOn = specialData.fixCountExec;
            countInput.text = "" + specialData.countEcexNum;
            sourceDropdowns[0].value = (int)specialData.sourceNum;
            sourceDropdowns[1].value = (int)specialData.sourceSkillNum;
            sourceDropdowns[2].value = (int)specialData.sourceSkillType;
            targetDropDown.value = (int)specialData.targetNum;
            resultDropdown.value = (int)specialData.resuleType;
        }
    }
}