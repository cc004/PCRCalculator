using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace PCRCaculator.Guild
{
    public class GuildExecTimeButton : MonoBehaviour
    {
        public enum ButtonType { SkillButton,ActionNormal,ActionCombo,Firearm}
        public ButtonType buttonType;
        public int ButtonHight;
        public List<Text> skillButtonTexts;
        public Action skillButtonAction;
        public List<Text> actionNormalTexts;
        public InputField actionNormalInput;
        //public Action onFinishEdit;
        public List<Text> actionComboTexts;
        public List<InputField> actionComboInputs;
        public Dropdown actionComboDrop;

        public Toggle firearmToggle;
        public InputField firearmExecInput;

        private Elements.ActionExecTime actionNormal;
        private Elements.ActionExecTimeCombo actionCombo;
        private Elements.FirearmCtrlData firearm;

        public void Init(string skillName, int skillID, string actionType, int actionid,Action action)
        {
            if(buttonType != ButtonType.SkillButton)
            {
                Debug.LogError("按钮样式错误！");
                return;
            }
            skillButtonTexts[0].text = skillName;
            skillButtonTexts[1].text = skillID + "";
            skillButtonTexts[2].text = actionType;
            skillButtonTexts[3].text = actionid + "";
            skillButtonAction = action;
        }
        public void OnButtonClick()
        {
            skillButtonAction?.Invoke();
        }
        public void Init(Elements.ActionExecTime actionNormal,int id,string type,float execTime,string effectType,int weight,string size,bool canEdit)
        {
            if (buttonType != ButtonType.ActionNormal)
            {
                Debug.LogError("按钮样式错误！");
                return;
            }
            this.actionNormal = actionNormal;
            actionNormalTexts[0].text = id + "";
            actionNormalTexts[1].text = type;
            actionNormalInput.text = execTime + "";
            actionNormalInput.interactable = canEdit;
            actionNormalTexts[2].text = effectType;
            actionNormalTexts[3].text = weight + "";
            actionNormalTexts[4].text = size;
        }
        public float GetValue()
        {
            try
            {
                float time = float.Parse(actionNormalInput.text);
                return time;
            }
            catch
            {
                MainManager.Instance.WindowConfigMessage("输入错误！", null, null);                
            }
            return 0;
        }
        public void Init(Elements.ActionExecTimeCombo actionCombo,int id,string type,float startTime,float offsetTime,int weight,int count,int Timetype)
        {
            if (buttonType != ButtonType.ActionCombo)
            {
                Debug.LogError("按钮样式错误！");
                return;
            }
            this.actionCombo = actionCombo;
            actionComboTexts[0].text = id + "";
            actionComboTexts[1].text = type;
            actionComboInputs[0].text = startTime + "";
            actionComboInputs[1].text = offsetTime + "";
            actionComboTexts[2].text = weight + "";
            actionComboInputs[2].text = count + "";
            actionComboDrop.value = Timetype;
        }
        public void GetValue(out float startTime,out float offsetTime,out int count,out int TimeType)
        {
            startTime = 0;
            offsetTime = 0;
            TimeType = 0;
            count = 1;
            try
            {
                startTime = float.Parse(actionComboInputs[0].text);
                offsetTime = float.Parse(actionComboInputs[1].text);
                count = int.Parse(actionComboInputs[2].text);
                TimeType = actionComboDrop.value;
            }
            catch
            {
                MainManager.Instance.WindowConfigMessage("输入错误！", null, null);
            }

        }
        public void Init(Elements.FirearmCtrlData firearm,int id, string type, float[] startdelay, float moveRate,int weight, float hitdelay, int FirearmType,float duration)
        {
            if (buttonType != ButtonType.Firearm)
            {
                Debug.LogError("按钮样式错误！");
                return;
            }
            this.firearm = firearm;
            actionComboTexts[0].text = id + "";
            actionComboTexts[1].text = type;
            actionComboInputs[0].text = startdelay[0] + "";
            actionComboInputs[0].interactable = false;
            actionComboInputs[1].text = moveRate + "";
            actionComboTexts[2].text = weight + "";
            actionComboInputs[2].text = hitdelay + "";
            actionComboDrop.value = FirearmType;
            actionComboInputs[3].text = duration + "";
            firearmToggle.isOn = firearm.ignoreFirearm;
            firearmExecInput.text = firearm.fixedExecTime + "";
        }
        public void Save()
        {
            try
            {
                switch (buttonType)
                {
                    case ButtonType.ActionNormal:
                        if (!actionNormalInput.interactable)
                        {
                            return;
                        }
                        actionNormal.Time = float.Parse(actionNormalInput.text);
                        break;
                    case ButtonType.ActionCombo:
                        actionCombo.StartTime = float.Parse(actionComboInputs[0].text);
                        actionCombo.OffsetTime = float.Parse(actionComboInputs[1].text);
                        actionCombo.Count = int.Parse(actionComboInputs[2].text);
                        break;
                    case ButtonType.Firearm:
                        firearm.MoveRate = float.Parse(actionComboInputs[1].text);
                        firearm.HitDelay = float.Parse(actionComboInputs[2].text);
                        firearm.duration = float.Parse(actionComboInputs[3].text);
                        firearm.MoveType = (Battle.eMoveTypes)actionComboDrop.value;
                        firearm.ignoreFirearm = firearmToggle.isOn;
                        firearm.fixedExecTime = float.Parse(firearmExecInput.text);
                        break;
                }
            }
            catch
            {
                MainManager.Instance.WindowConfigMessage("输入错误！", null, null);
            }
        }

    }
}