using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PCRCaculator
{
    [Serializable]
    public class EnemyData
    {
        public int enemy_id;
        public int unit_id;
        public string name;
        public int level;
        public int rarity;
        public int promotion_level;
        public BaseData baseData;
        public int union_burst_level;
        public List<int> main_skill_lvs = new List<int>();//从0开始，长度10
        public List<int> ex_skill_lvs = new List<int>();//从0开始，长度5
        public int resist_status_id;
        public int unique_equipment_flag_1;
        public int break_durability;
        public int virtual_hp;

        public EnemyDetailData detailData;
        public EnemySkillData skillData;
        public UnitData CreateUnitData()
        {
            return new UnitData(unit_id, level, 1);
        }
        public EnemyData Copy()
        {
            string tostr = JsonConvert.SerializeObject(this);
            EnemyData newData = JsonConvert.DeserializeObject<EnemyData>(tostr);
            return newData;
        }
    }
    [Serializable]
    public class EnemyDetailData
    {
        public int unit_id = 0;
        public string unit_name;
        public int prefab_id;
        public int motion_type;
        public int se_type;
        public int move_speed;
        public int search_area_width;
        public int atk_type;
        public float normal_atk_cast_time;
        public int cutin;
        public int visual_change_flag;
        public string comment;
    }
    [Serializable]
    public class EnemySkillData
    {
        public int UB;
        public List<int> MainSkills = new List<int>();
        public List<EnemyAttackPattern> enemyAttackPatterns = new List<EnemyAttackPattern>();
    }
    [Serializable]
    public class EnemyAttackPattern
    {
        public int pattern_id;
        public int unit_id;
        public int loopStart;
        public int loopEnd;
        public int[] atkPatterns;//长度为20
    }

}
