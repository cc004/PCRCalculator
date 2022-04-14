using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Guild
{


    public class GuildBossDataEditdr : MonoBehaviour
    {
        public GameObject baseBack;
        public InputField inputField_HP;
        public InputField inputField_lv;
        public InputField inputField_def;
        public InputField inputField_def_mg;
        public InputField inputField_atk;
        public InputField inputField_atk_mg;
        public InputField inputField_cri;
        public InputField inputField_cri_mg;
        public InputField inputField_dodge;
        public InputField inputField_acc;
        public InputField inputField_tp_up;
        public InputField inputField_tp_reduce;
        public InputField inputField_skillLv_ub;
        public InputField inputField_skillLv_1;
        public InputField inputField_skillLv_2;
        public InputField inputField_skillLv_3;
        public InputField inputField_atk_range;
        public InputField inputField_atk_cast_time;
        public GameObject PatternEditPrefab;
        public InputField inputField_appear_delay;
        public InputField inputField_body_width;
        public List<InputField> inputFields_skillCastTime;
        public InputField inputField_virturl_hp;

        private EnemyData enemyData;
        private int enemyId;
        private int unit_id;
        private float bossAppearDelay;
        private float bossBodyWidth;
        private float[] bossSkillCastTime;
        private List<int> bossMainSkillIDs;
        public void CancelButton()
        {
            baseBack.SetActive(false);
        }
        public void ResetButton()
        {
            if (GuildManager.Instance.SettingData.changedEnemyDataDic.ContainsKey(enemyId))
            {
                GuildManager.Instance.SettingData.changedEnemyDataDic.Remove(enemyId);
            }
            if(GuildManager.Instance.SettingData.bossAppearDelayDic!=null && GuildManager.Instance.SettingData.bossAppearDelayDic.ContainsKey(unit_id))
            {
                GuildManager.Instance.SettingData.bossAppearDelayDic.Remove(unit_id);
            }
            if(GuildManager.Instance.SettingData.bossSkillCastTimeDic != null)
            {
                for(int i = 0; i < bossMainSkillIDs.Count; i++)
                {
                    if (GuildManager.Instance.SettingData.bossSkillCastTimeDic.ContainsKey(bossMainSkillIDs[i]))
                        GuildManager.Instance.SettingData.bossSkillCastTimeDic.Remove(bossMainSkillIDs[i]);
                }
            }
            if (GuildManager.Instance.SettingData.bossBodyWidthDic != null)
            {
                if (GuildManager.Instance.SettingData.bossBodyWidthDic.ContainsKey(unit_id))
                {
                    GuildManager.Instance.SettingData.bossBodyWidthDic.Remove(unit_id);
                }
            }
            MainManager.Instance.WindowMessage("重置成功！");
            GuildManager.Instance.SaveDataToJson();
            CancelButton();
            //SetData(enemyId);
        }
        public void SaveButton()
        {
            EnemyData newEnemyData = enemyData.Copy();
            try
            {
                newEnemyData.baseData.Hp = long.Parse(inputField_HP.text, NumberStyles.AllowExponent);
            }
            catch (FormatException e)
            {
                MainManager.Instance.WindowConfigMessage("HP只能输入整数，请不要用科学计数法。", null);
                return;
            }
            newEnemyData.level = int.Parse(inputField_lv.text);
            newEnemyData.baseData.Def = int.Parse(inputField_def.text);
            newEnemyData.baseData.Magic_def = int.Parse(inputField_def_mg.text);
            newEnemyData.baseData.Atk = int.Parse(inputField_atk.text);
            newEnemyData.baseData.Magic_str = int.Parse(inputField_atk_mg.text);
            newEnemyData.baseData.Physical_critical = int.Parse(inputField_cri.text);
            newEnemyData.baseData.Magic_critical = int.Parse(inputField_cri_mg.text);
            newEnemyData.baseData.Dodge = int.Parse(inputField_dodge.text);
            newEnemyData.baseData.Accuracy = int.Parse(inputField_acc.text);
            newEnemyData.baseData.Energy_recovery_rate = int.Parse(inputField_tp_up.text);
            newEnemyData.baseData.Enerey_reduce_rate = int.Parse(inputField_tp_reduce.text);
            newEnemyData.union_burst_level = int.Parse(inputField_skillLv_ub.text);
            newEnemyData.main_skill_lvs[0] = int.Parse(inputField_skillLv_1.text);
            newEnemyData.main_skill_lvs[1] = int.Parse(inputField_skillLv_2.text);
            newEnemyData.main_skill_lvs[2] = int.Parse(inputField_skillLv_3.text);
            newEnemyData.union_burst_level = int.Parse(inputField_skillLv_ub.text);
            newEnemyData.detailData.search_area_width = int.Parse(inputField_atk_range.text);
            newEnemyData.detailData.normal_atk_cast_time = float.Parse(inputField_atk_cast_time.text);
            newEnemyData.virtual_hp = int.Parse(inputField_virturl_hp.text);

            if (GuildManager.Instance.SettingData.changedEnemyDataDic.ContainsKey(enemyId))
            {
                GuildManager.Instance.SettingData.changedEnemyDataDic[enemyId] = newEnemyData;
            }
            else
            {
                GuildManager.Instance.SettingData.changedEnemyDataDic.Add(enemyId,newEnemyData);
            }
            bossAppearDelay = float.Parse(inputField_appear_delay.text);
            if (GuildManager.Instance.SettingData.bossAppearDelayDic == null)
            {
                GuildManager.Instance.SettingData.bossAppearDelayDic = new Dictionary<int, float> { {unit_id, bossAppearDelay } };
            }
            else
            {
                if (GuildManager.Instance.SettingData.bossAppearDelayDic.ContainsKey(unit_id))
                {
                    GuildManager.Instance.SettingData.bossAppearDelayDic[unit_id] = bossAppearDelay;
                }
                else
                {
                    GuildManager.Instance.SettingData.bossAppearDelayDic.Add(unit_id, bossAppearDelay);
                }
            }
            bossBodyWidth = float.Parse(inputField_body_width.text);
            if (GuildManager.Instance.SettingData.bossBodyWidthDic == null)
            {
                GuildManager.Instance.SettingData.bossBodyWidthDic = new Dictionary<int, float> { { unit_id, bossBodyWidth } };
            }
            else
            {
                if (GuildManager.Instance.SettingData.bossBodyWidthDic.ContainsKey(unit_id))
                {
                    GuildManager.Instance.SettingData.bossBodyWidthDic[unit_id] = bossBodyWidth;
                }
                else
                {
                    GuildManager.Instance.SettingData.bossBodyWidthDic.Add(unit_id, bossBodyWidth);
                }
            }
            /*if(GuildManager.Instance.SettingData.bossSkillCastTimeDic == null)
            {
                GuildManager.Instance.SettingData.bossSkillCastTimeDic = new Dictionary<int, float>();
            }
            for(int i = 0; i < inputFields_skillCastTime.Count; i++)
            {
                if (GuildManager.Instance.SettingData.bossSkillCastTimeDic.ContainsKey(bossMainSkillIDs[i]))
                {
                    GuildManager.Instance.SettingData.bossSkillCastTimeDic[bossMainSkillIDs[i]] = float.Parse(inputFields_skillCastTime[i].text);
                }
                else
                {
                    GuildManager.Instance.SettingData.bossSkillCastTimeDic.Add(bossMainSkillIDs[i], float.Parse(inputFields_skillCastTime[i].text));
                }
            }*/

            baseBack.SetActive(false);
            GuildManager.Instance.SaveDataToJson();
        }
        public void SetData(int enemyId)
        {
            baseBack.SetActive(true);
            this.enemyId = enemyId;
            EnemyData enemyData = GuildManager.EnemyDataDic[enemyId];
            if (GuildManager.Instance.SettingData.changedEnemyDataDic.ContainsKey(enemyId))
            {
                enemyData = GuildManager.Instance.SettingData.changedEnemyDataDic[enemyId];
            }
            this.enemyData = enemyData;
            unit_id = enemyData.unit_id;
            inputField_HP.text = "" + (long)enemyData.baseData.Hp;
            inputField_lv.text = "" + enemyData.level;
            inputField_def.text = "" + Mathf.RoundToInt(enemyData.baseData.Def);
            inputField_def_mg.text = "" + Mathf.RoundToInt(enemyData.baseData.Magic_def);
            inputField_atk.text = "" + Mathf.RoundToInt(enemyData.baseData.Atk);
            inputField_atk_mg.text = "" + Mathf.RoundToInt(enemyData.baseData.Magic_str);
            inputField_cri.text = "" + Mathf.RoundToInt(enemyData.baseData.Physical_critical);
            inputField_cri_mg.text = "" + Mathf.RoundToInt(enemyData.baseData.Magic_critical);
            inputField_dodge.text = "" + Mathf.RoundToInt(enemyData.baseData.Dodge);
            inputField_acc.text = "" + Mathf.RoundToInt(enemyData.baseData.Accuracy);
            inputField_tp_up.text = "" + Mathf.RoundToInt(enemyData.baseData.Energy_recovery_rate);
            inputField_tp_reduce.text = "" + Mathf.RoundToInt(enemyData.baseData.Enerey_reduce_rate);
            inputField_skillLv_ub.text = "" + enemyData.union_burst_level;
            inputField_skillLv_1.text = "" + enemyData.main_skill_lvs[0];
            inputField_skillLv_2.text = "" + enemyData.main_skill_lvs[1];
            inputField_skillLv_3.text = "" + enemyData.main_skill_lvs[2];
            inputField_skillLv_ub.text = "" + enemyData.union_burst_level;
            inputField_atk_range.text = "" + enemyData.detailData.search_area_width;
            inputField_atk_cast_time.text = "" + enemyData.detailData.normal_atk_cast_time;
            GetBossAppearDelay();
            inputField_appear_delay.text = "" + bossAppearDelay;
            GetBossBodyWidth();
            inputField_body_width.text = "" + bossBodyWidth;
            GetBossSkillCastTime();
            inputField_virturl_hp.text = "" + enemyData.virtual_hp;
            for(int i = 0; i < inputFields_skillCastTime.Count; i++)
            {
                inputFields_skillCastTime[i].text = "" + bossSkillCastTime[i];
            }

        }
        public void EditAttackPattern(int i)
        {
            int patternid = enemyData.unit_id * 100 + i;
            UnitAttackPattern unitAttackPattern = null; //MainManager.Instance.AllUnitAttackPatternDic[patternid];
            if(GuildManager.Instance.SettingData.changedEnemyAttackPatternDic.TryGetValue(patternid,out unitAttackPattern) || 
                MainManager.Instance.AllUnitAttackPatternDic.TryGetValue(patternid, out unitAttackPattern))
            {
                GameObject a = Instantiate(PatternEditPrefab);
                a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform,false);
                if(i==1)
                    a.GetComponent<GuildBossATKPatternChange>().Initialize(unitAttackPattern,SetAttackPatternA);
                else
                    a.GetComponent<GuildBossATKPatternChange>().Initialize(unitAttackPattern, SetAttackPatternB);

            }
        }
        public void SetAttackPatternA(UnitAttackPattern unitAttackPattern)
        {
            int patternid = enemyData.unit_id * 100 + 1;
            if (GuildManager.Instance.SettingData.changedEnemyAttackPatternDic.ContainsKey(patternid))
            {
                GuildManager.Instance.SettingData.changedEnemyAttackPatternDic[patternid] = unitAttackPattern;
            }
            else
            {
                GuildManager.Instance.SettingData.changedEnemyAttackPatternDic.Add(patternid, unitAttackPattern);
            }

        }
        public void SetAttackPatternB(UnitAttackPattern unitAttackPattern)
        {
            int patternid = enemyData.unit_id * 100 + 2;
            if (GuildManager.Instance.SettingData.changedEnemyAttackPatternDic.ContainsKey(patternid))
            {
                GuildManager.Instance.SettingData.changedEnemyAttackPatternDic[patternid] = unitAttackPattern;
            }
            else
            {
                GuildManager.Instance.SettingData.changedEnemyAttackPatternDic.Add(patternid, unitAttackPattern);
            }

        }
        public void EditBossSkillDelay()
        {
            //GuildManager.Instance.GuildExecTimeSetting.Init(enemyData);
            MainManager.Instance.WindowConfigMessage("请前往数据更新页面设置！", null);
        }
        private void GetBossAppearDelay()
        {
            if (GuildManager.Instance.SettingData.bossAppearDelayDic != null &&
                GuildManager.Instance.SettingData.bossAppearDelayDic.TryGetValue(enemyData.unit_id, out float value))
            {
                bossAppearDelay = value;
            }
        }
        private void GetBossBodyWidth()
        {
            if (GuildManager.Instance.SettingData.bossBodyWidthDic != null &&
                GuildManager.Instance.SettingData.bossBodyWidthDic.TryGetValue(enemyData.unit_id, out float value))
            {
                bossBodyWidth = value;
            }
        }
        private void GetBossSkillCastTime()
        {
            bossMainSkillIDs = enemyData.skillData.MainSkills;
            bossSkillCastTime = new float[10];
            for (int i = 0; i < bossMainSkillIDs.Count; i++)
            {
                if (bossMainSkillIDs[i] > 0)
                {
                    if (GuildManager.Instance.SettingData.bossSkillCastTimeDic != null && GuildManager.Instance.SettingData.bossSkillCastTimeDic.TryGetValue(bossMainSkillIDs[i], out float value))
                    {
                        bossSkillCastTime[i] = value;
                    }
                    else
                    {
                        bossSkillCastTime[i] = MainManager.Instance.SkillDataDic[bossMainSkillIDs[i]].casttime;
                    }
                }
                else
                {
                    bossSkillCastTime[i] = 0;
                }
            }
        }
    }
}