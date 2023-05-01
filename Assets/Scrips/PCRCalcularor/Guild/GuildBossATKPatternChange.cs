using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{
    public class GuildBossATKPatternChange : MonoBehaviour
    {
        public InputField startText;
        public InputField endText;
        public List<Dropdown> Dropdowns;
        public UnitAttackPattern attackPattern;
        public Action<UnitAttackPattern> callBackAction;


        public void Initialize(UnitAttackPattern unitAttackPattern,Action<UnitAttackPattern> callBack)
        {
            attackPattern = unitAttackPattern;
            callBackAction = callBack;
            Refresh();
        }
        public void ResetButton()
        {
            attackPattern = MainManager.Instance.AllUnitAttackPatternDic[attackPattern.pattern_id];
            Refresh();
        }
        public void ExitButton()
        {
            Destroy(gameObject);
        }
        public void SaveButton()
        {
            UnitAttackPattern unitAttackPattern = new UnitAttackPattern();
            unitAttackPattern.pattern_id = attackPattern.pattern_id;
            unitAttackPattern.unit_id = attackPattern.unit_id;
            try
            {
                unitAttackPattern.loop_start = int.Parse(startText.text);
                unitAttackPattern.loop_end = int.Parse(endText.text);
                for(int i = 0; i < 20; i++)
                {
                    unitAttackPattern.atk_patterns[i] = SwitchPatternValue(Dropdowns[i].value, true);
                }
                callBackAction?.Invoke(unitAttackPattern);
                ExitButton();
            }
            catch
            {
                MainManager.Instance.WindowConfigMessage("输入错误！", null);
            }
        }
        public void Refresh()
        {
            startText.text = "" + attackPattern.loop_start;
            endText.text = "" + attackPattern.loop_end;
            for(int i = 0; i < 20; i++)
            {
                Dropdowns[i].value = SwitchPatternValue(attackPattern.atk_patterns[i]);
            }
        }
        private int SwitchPatternValue(int value,bool reverse = false)
        {
            if (!reverse)
            {
                switch (value)
                {
                    case 0:
                        return 0;
                    case 1:
                        return 1;
                    case 1001:
                        return 2;
                    case 1002:
                        return 3;
                    case 1003:
                        return 4;
                    case 1004:
                        return 5;
                    case 1005:
                        return 6;
                }
                return 0;
            }

            switch (value)
            {
                case 0:
                    return 0;
                case 1:
                    return 1;
                case 2:
                    return 1001;
                case 3:
                    return 1002;
                case 4:
                    return 1003;
                case 5:
                    return 1004;
                case 6:
                    return 1005;
            }
            return 0;
        }
    }
}