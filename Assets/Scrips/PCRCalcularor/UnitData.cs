using System;
using System.Collections.Generic;
using System.Linq;
using Elements;
using ExcelDataReader.Log;
using Newtonsoft.Json;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using PCRCalcularor;
using SQLite4Unity3d;
using UnityEngine;
using DirectionType = PCRCaculator.Battle.DirectionType;
using eMoveTypes = PCRCaculator.Battle.eMoveTypes;
using eStateIconType = Elements.eStateIconType;
using eTargetAssignment = PCRCaculator.Battle.eTargetAssignment;

/// <summary>
/// 每个角色的所有数据类
/// </summary>
namespace PCRCaculator
{
    public enum PositionType { frount = 0, middle = 1, backword = 2 }//站位，前中后
    /// <summary>
    /// 角色基础面板数据，数据由数据库中的unit_rarity表以及其他类生成~
    /// </summary>
    [Serializable]
    public class UnitRarityData : IComparer<UnitRarityData>, IComparable<UnitRarityData>, IOverride<UnitRarityData>, IOverride2<UnitRarityData>
    {
        public int unitId;
        public string unitName;
        public PositionType unitPositionType;
        public List<BaseData> raritydatas = new List<BaseData>();
        public List<BaseData> ratitydatas_rate = new List<BaseData>();
        public int unit_material_id { get => (unitId - 100001) / 100 + 31000; }//这个材质id暂时没用
        public UnitRankData rankData;
        public UnitDetailData detailData;
        public UnitSkillData skillData;

        public BaseData GetBonusData(UnitData a)
        {
            var result = new BaseData();
            if (unitId < 200000)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (a.equipLevel[i] >= 0)
                    {
                        int key = ((a.rank <= rankData.rankEquipments.Count) ? rankData.rankEquipments[a.rank - 1][i] : 999999);
                        if (MainManager.Instance.EquipmentDic.TryGetValue(key, out var value))
                        {
                            result += value.GetEquipmentData(a.equipLevel[i]);
                        }
                    }
                }
            }
            if (a.love >= 0 && unitId <= 200000)
            {
                foreach (int item in MainManager.Instance.UnitStoryEffectDic[unitId])
                {
                    if (a.playLoveDic != null && a.playLoveDic.TryGetValue(item, out var value2))
                    {
                        result += MainManager.Instance.UnitStoryDic[item].GetLoveValues(value2);
                    }
                    else if (MainManager.Instance.JudgeWeatherShowThisUnit(item) && MainManager.Instance.unitDataDic.ContainsKey(item))
                    {
                        result += MainManager.Instance.UnitStoryDic[item].GetLoveValues(MainManager.Instance.unitDataDic[item].love);
                    }
                }
            }
            if (a.uniqueEqLv > 0 && unitId <= 200000 && MainManager.Instance.UniqueEquipmentDataDic.TryGetValue(a.unitId, out var value3))
            {
                result += BaseData.CeilToInt(value3.CalcUniqueValue(a.uniqueEqLv));
            }

            return result;
        }

        public UnitRarityData() { }
        public UnitRarityData(int id, List<BaseData> d1, List<BaseData> d2, UnitRankData unitRankData, UnitDetailData detailData, UnitSkillData skillData)
        {
            unitId = id;
            raritydatas = d1;
            ratitydatas_rate = d2;
            rankData = unitRankData;
            this.detailData = detailData;
            unitName = detailData.name;
            this.skillData = skillData;
            if (detailData.searchAreaWidth < 300)
            {
                unitPositionType = PositionType.frount;
            }
            else if (detailData.searchAreaWidth < 600)
            {
                unitPositionType = PositionType.middle;
            }
            else
            {
                unitPositionType = PositionType.backword;
            }
        }
        /*
        public UnitRarityData(string data)
        {
            string[] data0 = data.Split('~');
            unitId = int.Parse(data0[0]);
            unitName = data0[1];
            raritydatas = new List<BaseData>();
            List<string> s1 = JsonConvert.DeserializeObject<List<string>>(data0[2]);
            foreach (string a in s1)
            {
                raritydatas.Add(new BaseData(a));
            }
            ratitydatas_rate = new List<BaseData>();
            List<string> s2 = JsonConvert.DeserializeObject<List<string>>(data0[3]);
            foreach (string a in s2)
            {
                ratitydatas_rate.Add(new BaseData(a));
            }
            rankData = new UnitRankData(data0[4]);
            detailData = new UnitDetailData(data0[5]);
            //skillData = new UnitSkillData(data0[6]);
            skillData = JsonConvert.DeserializeObject<UnitSkillData>(data0[6]);
            if (detailData.searchAreaWidth < 300)
            {
                unitPositionType = PositionType.frount;
            }
            else if (detailData.searchAreaWidth < 600)
            {
                unitPositionType = PositionType.middle;
            }
            else
            {
                unitPositionType = PositionType.backword;
            }
        }*/
        /// <summary>
        /// 获取角色的详细数据（随等级等改变的数据）
        /// </summary>
        /// <param name="a">玩家可更改的数据</param>
        /// <param name="usedic_save">使用的数据集，默认用当前数据集，选是用最近的存档数据</param>
        /// <returns>全部详细数据</returns>
        public BaseData GetBaseData(UnitData a, bool usedic_save = false, bool jjc_otherside = false)
        {
            try
            {


                if (a.unitId != unitId)
                {
                    Debug.LogError("角色id不匹配！" + a.unitId + "!=" + unitId);
                }
                if (a.rarity > raritydatas.Count||a.rarity<=0)
                {
                    a.rarity = 5;
                }
                if (a.rank - 2 >= rankData.datas.Count)
                {
                    a.rank = 1 + rankData.datas.Count;
                }

                BaseData result = new BaseData();

                BaseData d1 = raritydatas[a.rarity - 1] + (a.level + a.rank) * ratitydatas_rate[a.rarity - 1];//计算等级与星级、rank
                if (a.rank >= 2)
                {
                    d1 += rankData.datas[a.rank - 2];
                }

                result += BaseData.Round(d1);

                d1 = new BaseData();
                if (unitId < 200000)
                {
                    for (int i = 0; i < 6; i++)//计算装备属性
                    {
                        if (a.equipLevel[i] < 0)
                        {
                            continue;
                        }
                        int rankid = a.rank<=rankData.rankEquipments.Count? rankData.rankEquipments[a.rank - 1][i]:999999;
                        if (MainManager.Instance.EquipmentDic.TryGetValue(rankid, out var equipmentData))
                        {
                            d1 += equipmentData.GetEquipmentData(a.equipLevel[i]);
                        }
                    }
                }
                //计算专武加成
                if (a.uniqueEqLv > 0 && unitId <= 200000)
                {
                    if (MainManager.Instance.UniqueEquipmentDataDic.TryGetValue(a.unitId, out var equipmentData))
                    {
                        d1 += BaseData.CeilToInt(equipmentData.CalcUniqueValue(a.uniqueEqLv));
                    }
                }

                result += BaseData.Round(d1);
                d1 = new BaseData();

                //计算羁绊加成
                if (a.love >= 0 && unitId <= 200000)
                {
                    foreach (int id0 in MainManager.Instance.UnitStoryEffectDic[unitId])
                    {
                        if (a.playLoveDic != null && a.playLoveDic.TryGetValue(id0, out int loveLevel))
                        {
                            if (!MainManager.Instance.UnitStoryDic.ContainsKey(id0))
                            {
                                Debug.LogError($"{id0}: key not found");
                            }
                            d1 += MainManager.Instance.UnitStoryDic[id0].GetLoveValues(loveLevel);
                        }
                        else if (MainManager.Instance.JudgeWeatherShowThisUnit(id0) && MainManager.Instance.unitDataDic.ContainsKey(id0))
                        {
                            if (!jjc_otherside)
                            {
                                d1 += MainManager.Instance.UnitStoryDic[id0].GetLoveValues(
                                    usedic_save ? MainManager.Instance.unitDataDic_save[id0].love2 :
                                    MainManager.Instance.unitDataDic[id0].love2);
                            }
                            else
                            {

                                d1 += MainManager.Instance.UnitStoryDic[id0].GetLoveValues(a.love2);

                            }
                        }
                    }
                }

                result += BaseData.Round(d1);


                d1 = MainManager.Instance.rbs.TryGetValue((a.unitId, a.rank), out var val) ? val.GetBaseData() : new BaseData();
                
                result += BaseData.Round(d1);

                var temp = result;

                for (int i = 0; i < 3; ++i)
                {
                    if (a.exEquip[i] == 0) continue;
                    var equip = MainManager.Instance.unitExEquips[a.unitId][i][a.exEquip[i]];
                    var def = equip.GetDefaultBaseData();
                    var max = equip.GetMaxBaseData();
                    var param = new BaseData();
                    for (int j = 0; j < 17; ++j)
                    {
                        var d = def.dataint[j] / 100.0;
                        var m = max.dataint[j] / 100.0;
                        param.dataint[j] = 100 * (int)Math.Ceiling(d + (m - d) * ((double)a.exEquipLevel[i] / equip.levelMax));
                    }
                    temp += result * param;
                }
                // ex slot
                return temp;
            }
#if UNITY_EDITOR
            catch(NotImplementedException e)
#else
            catch(Exception e)
#endif
            {
                Debug.LogError("加载角色" + a.unitId + "时发生错误：" + e);
                //MainManager.Instance.WindowConfigMessage("加载角色" + a.unitId + "时发生错误：" + e.Message, null);
                return new BaseData();
            }
        }
        /// <summary>
        /// 计算角色战斗力
        /// </summary>
        /// <param name="a">基础数据</param>
        /// <returns>战斗力</returns>
        public float GetPowerValue(UnitData a, bool usedic_save = false,bool jjc_otherside = false)
        {
            return GetBaseData(a, usedic_save,jjc_otherside).GetPowerValue(a);
        }
        public int[] GetRankEquipments(int rank)
        {
            if (rank >rankData.rankEquipments.Count || rank <= 0 || rankData.rankEquipments == null)
            {
                return new int[6] { 999999, 999999, 999999, 999999, 999999, 999999 };
            }
            return rankData.rankEquipments[rank - 1];
        }
        public int[] GetSkillList(UnitData a)
        {
            return skillData.GetSkillList(a);
        }
        public int[] GetFullSkillList()
        {
            return skillData.GetFullSkillList();
        }
        /// <summary>
        /// 获取角色攻击力（物理/魔法）
        /// </summary>
        /// <param name="a">角色属性</param>
        /// <returns>物理/魔法攻击力</returns>
        public float GetAtk(UnitData a)
        {
            BaseData b = GetBaseData(a);
            if (b.Atk >= b.Magic_str)
            {
                return b.Atk;
            }
            return b.Magic_str;
        }
        public void ChangeRankData(UnitRankData rankData)
        {
            this.rankData = rankData;
        }
        public UnitRankData GetRankData()
        {
            return rankData;
        }
        public int GetMaxRarity()
        {
            if (raritydatas.Count == 6)
            {
                return 6;
            }
            return 5;
        }
        public int GetMaxLoveLevel()
        {
            if (GetMaxRarity() == 6)
                return 12;
            return 8;
        }
        public BaseData GetBattleValuesApplyedEX(UnitData a,bool jjc_otherside = false)
        {
            BaseData b = GetBaseData(a,false,jjc_otherside);
            int[] exSkillActionList = MainManager.Instance.SkillDataDic[skillData.GetSkillList(a)[3]].skillactions;
            foreach (int exActionId in exSkillActionList)
            {
                if (exActionId > 1000)
                {
                    b += MainManager.Instance.SkillActionDic[exActionId].GetEXSkillApplyedValue(a.skillLevel[3]);
                }
            }
            return b;
        }
        public BaseData GetEXSkillValue(UnitData a, bool jjc_otherside = false)
        {
            BaseData b = new BaseData();
            var exskill = skillData.GetSkillList(a)[3];
            if (exskill == 0) return b;
            int[] exSkillActionList = MainManager.Instance.SkillDataDic[exskill].skillactions;
            foreach (int exActionId in exSkillActionList)
            {
                if (exActionId > 1000)
                {
                    b += MainManager.Instance.SkillActionDic[exActionId].GetEXSkillApplyedValue(a.skillLevel[3]);
                }
            }
            return b;

        }
        public BaseData GetEXSkillValueNoEv(UnitData a, bool jjc_otherside = false)
        {
            BaseData b = new BaseData();
            int[] exSkillActionList = MainManager.Instance.SkillDataDic[skillData.EXskill].skillactions;
            foreach (int exActionId in exSkillActionList)
            {
                if (exActionId > 1000)
                {
                    b += MainManager.Instance.SkillActionDic[exActionId].GetEXSkillApplyedValue(a.skillLevel[3]);
                }
            }
            return b;

        }

        public void OverrideWith(UnitRarityData other)
        {
            unitName = other.unitName;
            detailData = other.detailData;
            skillData = other.skillData;
            for (int i = 0; i < other.rankData.datas.Count; ++i)
            {
                var d = other.rankData.datas[i];
                rankData.datas[i] = d;
            }

            for (int i = 0; i < other.rankData.rankEquipments.Count; ++i)
            {
                var r = other.rankData.rankEquipments[i];
                if (r.Max() != 999999) rankData.rankEquipments[i] = r;
            }
        }

        public void Override2With(UnitRarityData other)
        {
            other.detailData.name = detailData.name;
            other.unitName = unitName;
        }

        public override string ToString()
        {
            List<string> s1 = new List<string>();
            foreach (BaseData a in raritydatas)
            {
                s1.Add(a.ToString());
            }
            List<string> s2 = new List<string>();
            foreach (BaseData b in ratitydatas_rate)
            {
                s2.Add(b.ToString());
            }
            string[] da = new string[7]
            {
            unitId+"",unitName,
            JsonConvert.SerializeObject(s1),
            JsonConvert.SerializeObject(s2),
            rankData.ToString(),
            detailData.ToString(),
            skillData.ToString()
            };
            return string.Join("~", da);
        }
        public int Compare(UnitRarityData x, UnitRarityData y)
        {
            return x.detailData.searchAreaWidth - y.detailData.searchAreaWidth;
        }
        public int CompareTo(UnitRarityData other)
        {
            if (detailData.searchAreaWidth == other.detailData.searchAreaWidth)
                return detailData.unitid - other.detailData.unitid;
            return detailData.searchAreaWidth - other.detailData.searchAreaWidth;

        }

        public BaseData GetBaseDataForEnergyCalc(UnitData a)
        {

            if (a.unitId != unitId)
            {
                Debug.LogError("角色id不匹配！" + a.unitId + "!=" + unitId);
            }
            if (a.rarity > raritydatas.Count || a.rarity <= 0)
            {
                a.rarity = 5;
            }
            if (a.rank - 2 >= rankData.datas.Count)
            {
                a.rank = 1 + rankData.datas.Count;
            }

            var rank = a.rank;

            while (MainManager.Instance.rbs.ContainsKey((unitId, rank - 1)))
                --rank;

            BaseData result = new BaseData();

            BaseData d1 = raritydatas[a.rarity - 1] + (a.level + rank) * ratitydatas_rate[a.rarity - 1];//计算等级与星级、rank

            if (rank >= 2)
            {
                d1 += rankData.datas[rank - 2];
            }

            result += BaseData.Round(d1);

            if (unitId < 200000 && !MainManager.Instance.rbs.ContainsKey((unitId, rank)))
            {
                d1 = new BaseData();

                for (int i = 0; i < 6; i++)//计算装备属性
                {
                    if (a.equipLevel[i] < 0)
                    {
                        continue;
                    }
                    int rankid = rank <= rankData.rankEquipments.Count ? rankData.rankEquipments[rank - 1][i] : 999999;
                    if (MainManager.Instance.EquipmentDic.TryGetValue(rankid, out var equipmentData))
                    {
                        d1 += equipmentData.GetEquipmentData(a.equipLevel[i]);
                    }
                }

                result += BaseData.Round(d1);
            }

            d1 = new BaseData();

            result += BaseData.Round(d1);

            return result;
        }
    }
    /// <summary>
    /// 玩家可以更改的角色数据，用于存档
    /// </summary>
    [Serializable]
    public class UnitData
    {
        public int enemyid;
        private int _unitId;
        public int unitId
        {
            get => _unitId;
            set
            {
                _unitId = value;
                SetDefaultLoveDict();
            }
        }
        //public PositionType positionType = PositionType.frount;
        public int level = 1;
        public int rarity = 1;
        public int love => 114514;//好感度
        public int rank = 1;
        public int[] equipLevel = new int[6] { -1, -1, -1, -1, -1, -1 };//装备等级，-1-未装备，0~5表示强化等级
        public int[] skillLevel = new int[4] { 1, 1, 1, 1 };//技能等级，0123对应UB/技能1/技能2/EX技能
        public Dictionary<int, int> playLoveDic = new Dictionary<int, int>();
        public int uniqueEqLv;//专武等级
        public int[] exEquip = new int[] { 0, 0, 0 };
        public int[] exEquipLevel = new int[] { 0, 0, 0 };

        [JsonIgnore]
        public string name;
        public int love2 => playLoveDic == null ? 0 : playLoveDic.TryGetValue(unitId, out var val) ? val : 0;
        
        public void SetDefaultLoveDict()
        {
            if (playLoveDic != null && playLoveDic.ContainsKey(unitId)) return;
            playLoveDic = new Dictionary<int, int>();
            if (!MainManager.Instance.UnitStoryEffectDic.ContainsKey(unitId)) return;
            List<int> effectUnitList = MainManager.Instance.UnitStoryEffectDic[unitId];
            foreach (var unit in effectUnitList)
            {
                playLoveDic.Add(unit, 0);
            }
        }

        public UnitData()
        {
        }
        public UnitData(int id)
        {
            unitId = id;
        }
        public UnitData(int id, int rarity)
        {
            unitId = id;
            if (rarity >= 1 && rarity <= 6)
                this.rarity = rarity;
            else
                this.rarity = 1;
        }
        public UnitData(int id, int lv, int ra)
        {
            unitId = id;
            level = lv;
            rarity = ra;
        }
        /// <summary>
        /// 召唤物使用，技能等级随召唤物等级
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lv"></param>
        /// <param name="ra"></param>
        /// <param name="rank"></param>
        public UnitData(int id, int lv, int ra,int rank)
        {
            unitId = id;
            level = lv;
            rarity = ra;
            this.rank = rank;
            skillLevel = new int[4] { lv, lv, lv, lv };
        }

        public UnitData(int id, int lv, int ra, int lov, int rank, int[] eqlv, int[] sklv, Dictionary<int, int> playLoveDic = null,int uniqueeqlv = 0)
        {
            unitId = id;
            level = lv;
            rarity = ra;
            //love = lov;
            this.rank = rank;
            equipLevel = eqlv;
            skillLevel = sklv;
            if (playLoveDic != null)
            {
                this.playLoveDic = new Dictionary<int, int>();
                foreach(KeyValuePair<int,int> keyValuePair in playLoveDic)
                {
                    this.playLoveDic.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            uniqueEqLv = uniqueeqlv;
        }

        public void SetMax()
        {
            if (MainManager.Instance.UnitRarityDic.TryGetValue(unitId, out UnitRarityData un))
            {
                PlayerSetting playerSetting = MainManager.Instance.PlayerSetting;
                int playerLevel = playerSetting.playerLevel;
                level = playerLevel;
                skillLevel[0] = playerLevel;
                skillLevel[1] = playerLevel;
                skillLevel[2] = playerLevel;
                skillLevel[3] = playerLevel;
                //love = GetMaxRarity() == 6 ? 12 : 8;
                rarity = GetMaxRarity();
                rank = playerSetting.GetMaxRank();
                for (int i = 0; i < 6; i++)
                {
                    if (playerSetting.GetEquipmentIsAble()[i])
                    {
                        int equipmentID = un.GetRankEquipments(rank)[i];
                        if (MainManager.Instance.EquipmentDic.ContainsKey(equipmentID))
                            equipLevel[i] = MainManager.Instance.EquipmentDic[equipmentID].GetMaxLevel();
                        else
                            equipLevel[i] = -1;
                    }
                    else
                    {
                        equipLevel[i] = -1;
                    }
                }

                uniqueEqLv = MainManager.Instance.JudgeWeatherAllowUniqueEq(unitId) ? playerSetting.maxUniqueEqLv : -1;
            }
            else
            {
                Debug.LogError("未找到" + unitId + "的对应角色数据！");
            }
        }
        public int GetMaxRarity()
        {
            if (MainManager.Instance.PlayerSetting.allowRarity6 && MainManager.Instance.UnitRarityDic.TryGetValue(unitId,out var data))
            {
                return data.GetMaxRarity();
            }
            return 5;
        }
        /// <summary>
        /// 深度复制当前类
        /// </summary>
        /// <returns></returns>
        public UnitData Copy()
        {
            return new UnitData(unitId, level, rarity, love, rank, equipLevel, skillLevel,playLoveDic,uniqueEqLv);
        }
        /// <summary>
        /// 比较相同角色是否有所提升
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public bool CompairIsUp(UnitData old)
        {
            if(unitId != old.unitId)
            {
                Debug.LogError("不能比较不同的角色！");
                return false;
            }
            if (level > old.level) { return true; }
            if (rank > old.rank) { return true; }
            if(rank == old.rank)
            {
                for(int i = 0; i < 6; i++)
                {
                    if (equipLevel[i] > old.equipLevel[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public List<int> GetRequiredEquipment(UnitData old)
        {
            if (unitId != old.unitId)
            {
                Debug.LogError("不能比较不同的角色！");
                return new List<int>();
            }
            UnitRarityData rarityData = MainManager.Instance.UnitRarityDic[unitId];
            List<int> equipList = new List<int>();
            if (old.rank < rank)
            {
                for(int k = 0; k < 6; k++)
                {
                    if (old.equipLevel[k] < 0)
                    {
                        equipList.Add(rarityData.GetRankEquipments(old.rank)[k]);
                    }
                }
                for (int i = old.rank + 1; i < rank; i++)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        equipList.Add(rarityData.GetRankEquipments(i)[k]);
                    }
                }
            }
            for (int k = 0; k < 6; k++)
            {
                if (equipLevel[k] >= 0)
                {
                    if (old.rank < rank || old.equipLevel[k]<0)
                    {
                        equipList.Add(rarityData.GetRankEquipments(rank)[k]);
                    }
                }
            }
            return equipList;
        }

        public string GetUnitName()
        {
            if (!string.IsNullOrEmpty(name)) { return name; }
            return name = MainManager.Instance.GetUnitNickName(unitId);
        }

        public string GetUnitNameInternal()
        {
            if (MainManager.Instance.UnitName_cn.TryGetValue(unitId, out string name0))
            {
                return name0;
            }
            return MainManager.Instance.UnitRarityDic[unitId].unitName;
        }
        public string GetNicName()
        {
            return MainManager.Instance.GetUnitNickName(unitId);
        }
        public string GetRankDescribe()
        {
            string result = "R" + rank + "-";
            int count = 0;
            bool special = false;
            for(int i=0;i<equipLevel.Length;i++)
            {
                if (equipLevel[i] == 5)
                    count++;
                else if (equipLevel[i] > 0)
                    special = true;
            }
            result += count;
            if (special)
                result += "*";
            if (uniqueEqLv > 0)
                result += "(" + uniqueEqLv + ")";
            return result;
        }
        public string GetLevelDescribe()
        {
            string result = "" + level;
            bool special = false;
            foreach (int lv in skillLevel)
                if (lv != level)
                    special = true;
            if (special)
                result += "*";
            return result;
        }
        public int[] GetEvlotionSkillLv()
        {
            int[] li = new int[3] { 0, 0, 0 };
            if (rarity == 6)
                li[0] = skillLevel[0];
            if (uniqueEqLv > 0)
                li[1] = skillLevel[1];

            return li;
        }
        
        public new long GetHashCode()
        {
            unchecked
            {
                long hashCode = enemyid;
                hashCode = (hashCode * 397) ^ _unitId;
                hashCode = (hashCode * 397) ^ level;
                hashCode = (hashCode * 397) ^ rarity;
                hashCode = (hashCode * 397) ^ rank;
                hashCode = (hashCode * 397) ^ equipLevel[0];
                hashCode = (hashCode * 397) ^ equipLevel[1];
                hashCode = (hashCode * 397) ^ equipLevel[2];
                hashCode = (hashCode * 397) ^ equipLevel[3];
                hashCode = (hashCode * 397) ^ equipLevel[4];
                hashCode = (hashCode * 397) ^ equipLevel[5];
                hashCode = (hashCode * 397) ^ skillLevel[0];
                hashCode = (hashCode * 397) ^ skillLevel[1];
                hashCode = (hashCode * 397) ^ skillLevel[2];
                hashCode = (hashCode * 397) ^ skillLevel[3];
                hashCode = (hashCode * 397) ^ uniqueEqLv;
                hashCode = (hashCode * 397) ^ exEquip[0];
                hashCode = (hashCode * 397) ^ exEquip[1];
                hashCode = (hashCode * 397) ^ exEquip[2];
                hashCode = (hashCode * 397) ^ exEquipLevel[0];
                hashCode = (hashCode * 397) ^ exEquipLevel[1];
                hashCode = (hashCode * 397) ^ exEquipLevel[2];
                foreach (var unit in playLoveDic.Keys.OrderBy(x => x))
                    hashCode = (hashCode * 397) ^ playLoveDic[unit];
                return hashCode;
            }
        }
    }
    /// <summary>
    /// 角色的RANK数据，对应数据库中的unit_promotion_status表和unit_promotion表%
    /// </summary>
    [Serializable]
    public class UnitRankData
    {
        public List<int[]> rankEquipments = new List<int[]>();
        public List<BaseData> datas = new List<BaseData>();

        public UnitRankData() { }
        public UnitRankData(List<int[]> eq, List<BaseData> datas)
        {
            rankEquipments = eq;
            this.datas = datas;
        }
        public UnitRankData(string data)
        {
            string[] data0 = data.Split('%');
            rankEquipments = JsonConvert.DeserializeObject<List<int[]>>(data0[0]);
            List<string> datastr = JsonConvert.DeserializeObject<List<string>>(data0[1]);
            datas = new List<BaseData>();
            foreach (string s in datastr)
            {
                datas.Add(new BaseData(s));
            }
        }
        public override string ToString()
        {
            List<string> de = new List<string>();
            foreach (BaseData b in datas)
            {
                de.Add(b.ToString());
            }
            return JsonConvert.SerializeObject(rankEquipments) + "%" + JsonConvert.SerializeObject(de);
        }
    }
    /// <summary>
    /// 角色技能数据，对应数据库中的unit_skill_data表和unit_attack_pattern表?
    /// </summary>
    [Serializable]
    public class UnitSkillData
    {
        public int UB;
        public int UB_ev;
        public int skill_1;
        public int skill_1_ev;
        public int skill_2;
        public int skill_2_ev;
        public int skill_3;
        public int skill_4;
        public int skill_5;
        public int skill_6;
        public int skill_7;
        public int skill_8;
        public int skill_9;
        public int skill_10;

        public int EXskill;
        public int EXskill_ev;
        public int EXskill_2;
        public int EXskill_ev2;
        public int EXskill_3;
        public int EXskill_ev3;
        public int EXskill_4;
        public int EXskill_ev4;
        public int EXskill_5;
        public int EXskill_ev5;

        public int SPskill_1;
        public int SPskill_2;
        public int SPskill_3;
        public int SPskill_4;
        public int SPskill_5;
        public int SPskill_1_ev;
        public int SPskill_2_ev;

        public int SPUB;


        public int loopStart;
        public int loopEnd;
        public int[] atkPatterns;//长度为20
        /// <summary>
        /// 不要用这个构造函数！
        /// </summary>
        public UnitSkillData() { }
        /// <summary>
        /// 角色技能构造函数
        /// </summary>
        /// <param name="skills">数组长度为10</param>
        /// <param name="atkpatterns">数组长度20</param>
        public UnitSkillData(int[] skills, int a, int b, int[] atkpatterns)
        {
            UB = skills[0];
            UB_ev = skills[1];
            skill_1 = skills[2];
            skill_1_ev = skills[3];
            skill_2 = skills[4];
            skill_2_ev = skills[5];
            EXskill = skills[6];
            EXskill_ev = skills[7];
            SPskill_1 = skills[8];
            SPskill_2 = skills[9];
            loopStart = a;
            loopEnd = b;
            atkPatterns = atkpatterns;
        }
        /*public UnitSkillData(string data)
        {
            string[] skills = data.Split('?');
            UB = int.Parse(skills[0]);
            UB_ev = int.Parse(skills[1]);
            skill_1 = int.Parse(skills[2]);
            skill_1_ev = int.Parse(skills[3]);
            skill_2 = int.Parse(skills[4]);
            skill_2_ev = int.Parse(skills[5]);
            EXskill = int.Parse(skills[6]);
            EXskill_ev = int.Parse(skills[7]);
            SPskill_1 = int.Parse(skills[8]);
            SPskill_2 = int.Parse(skills[9]);
            loopStart = int.Parse(skills[10]);
            loopEnd = int.Parse(skills[11]);
            atkPatterns = JsonConvert.DeserializeObject<int[]>(skills[12]);
        }*/
        /// <summary>
        /// 获取技能列表
        /// </summary>
        /// <param name="rarity">星级（5*开ex+，6*开ub+）</param>
        /// <param name="isUnitEquip">是否开专武</param>
        /// <returns>返回一个长度为4的数组，对应UB,SKILL1,SKILL2,EXSKILL的skillid</returns>
        public int[] GetSkillList(UnitData unitData=null)
        {
            if (unitData == null)
            {
                unitData = new UnitData(0, 5, 5, 5);
            }
            int rarity = unitData.rarity;
            bool isUnitEquip = unitData.uniqueEqLv > 0;
            int[] li = new int[4] { UB, skill_1, skill_2, EXskill };
            if (rarity >= 5) { li[3] = EXskill_ev; }
            if (rarity == 6 && UB_ev != 0) { li[0] = UB_ev; }
            if (isUnitEquip)
            {
                li[1] = skill_1_ev == 0 ? li[1] : skill_1_ev;
                li[2] = skill_2_ev == 0 ? li[2] : skill_2_ev;
            }
            return li;
        }
        public int[] GetFullSkillList()
        {
            int[] li = new int[8] { UB, skill_1, skill_2, EXskill, UB_ev, skill_1_ev, skill_2_ev,EXskill_ev };
            return li;
        }
        public override string ToString()
        {            
            return JsonConvert.SerializeObject(this);
        }
    }
    /// <summary>
    /// 所有的技能数据，对应数据库中的skill_data表@
    /// </summary>
    [Serializable]
    public class SkillData : IOverride2<SkillData>
    {
        public int skillid;
        public string name = "空技能";
        public int type;
        public int areawidth;
        public float casttime;
        public int[] skillactions = new int[7] { 0,0,0,0,0,0,0};//长度为7
        public int[] dependactions = new int[7] { 0,0,0,0,0,0,0};//长度为7
        public string describes = "";
        public int icon;
        public static string[] num = new string[10] { "①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "⑨", "⑩" };
        /// <summary>
        /// 不要用这个构造函数！
        /// </summary>
        public SkillData() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="ty"></param>
        /// <param name="area"></param>
        /// <param name="time"></param>
        /// <param name="actions">长度为7</param>
        /// <param name="deactions">长度为7</param>
        /// <param name="des"></param>
        /// <param name="ir">图标</param>
        public SkillData(int id, string name, int ty, int area, float time, int[] actions, int[] deactions, string des, int ir)
        {
            skillid = id;
            this.name = name;
            type = ty;
            areawidth = area;
            casttime = time;
            skillactions = actions;
            dependactions = deactions;
            describes = des;
            icon = ir;
        }
        public SkillData(string data)
        {
            string[] data0 = data.Split('@');
            skillid = int.Parse(data0[0]);
            name = data0[1];
            type = int.Parse(data0[2]);
            areawidth = int.Parse(data0[3]);
            casttime = float.Parse(data0[4]);
            skillactions = JsonConvert.DeserializeObject<int[]>(data0[5]);
            dependactions = JsonConvert.DeserializeObject<int[]>(data0[6]);
            describes = data0[7];
            icon = int.Parse(data0[8]);
        }
        /// <summary>
        /// 获取技能描述文字
        /// </summary>
        /// <param name="lv">技能等级</param>
        /// <returns>描述字符串</returns>
        public string GetSkillDetails(int pos, UnitData data)
        {
            float atk = MainManager.Instance.UnitRarityDic[data.unitId].GetAtk(data);
            string detail = "释放时间：" + casttime + "s\n";
            int i = 0;
            foreach (int action in skillactions)
            {
                if (action != 0)
                {
                    string acdetail = MainManager.Instance.SkillActionDic[action].GetDetail(data.skillLevel[pos], atk);
                    if (acdetail != "")
                    {
                        detail += num[i] + acdetail + "\n";
                        i++;
                    }
                }
            }
            return detail;
        }
        /// <summary>
        /// 获取技能描述文字
        /// </summary>
        /// <param name="lv">技能等级</param>
        /// <returns>描述字符串</returns>
        public string GetSkillDetailsEnemy(int lv, float atk)
        {
            string detail = "释放时间：" + casttime + "s\n";
            int i = 0;
            foreach (int action in skillactions)
            {
                if (action != 0)
                {
                    string acdetail = MainManager.Instance.SkillActionDic[action].GetDetail(lv, atk);
                    if (dependactions[i] > 0)
                    {
                        List<int> list = new List<int>(skillactions);
                        acdetail = $"依赖{num[list.FindIndex(a=>a==dependactions[i])]}" + acdetail;
                    }
                    if (acdetail != "")
                    {
                        detail += num[i] + acdetail + "\n";
                        i++;
                    }
                }
            }
            return detail;
        }

        public void Override2With(SkillData other)
        {
            other.name = name;
            other.describes = describes;
        }

        public override string ToString()
        {
            string[] da = new string[9]
            {
            skillid+"",name,
            type+"",areawidth+"",
            casttime+"",JsonConvert.SerializeObject(skillactions),
            JsonConvert.SerializeObject(dependactions),describes,
            icon+""
            };
            return string.Join("@", da);
        }

    }
    /// <summary>
    /// 所有技能基础数据，对应数据库中的skill_action表#
    /// </summary>
    [Serializable]
    public class SkillAction : IOverride2<SkillAction>
    {
        public int actionid;
        public int classid;//1或2，一般为1，只有yls为2
        public int type;//18到92
        public int[] details;//长度为3
        public double[] values;//长度为7
        public int target_assigment;//对方-1，己方-2，所有-3，未知-0 OTHER_SIDE=1,OWNER_SIDE=2,ALL=3 eTargetAssignment
        public int target_area;//FRONT=1,FRONT_AND_BACK=2,ALL=3  DirectionType
        public int target_range;//-1到4320以及9999
        public int target_type;//排序优先度（枚举PirorityPattern)
        public int target_number;//目标优先顺序，0-第一，1-第二……
        public int target_count;//目标数量
        public string description = "";//小技能描述
        public string levelupdes = "";//小技能升级描述
        public static string[] skillTypeName = new string[94]
            {"?","ATTACK","MOVE","KNOCK","HEAL","CURE","BARRIER","REFLEXIVE","CHANGE_SPEED","SLIP_DAMAGE",//10
        "BUFF_DEBUFF","CHARM","BLIND","SILENCE","MODE_CHANGE","SUMMON","CHARGE_ENERGY","TRIGER","DAMAGE_CHANGE","CHARGE","DECOY",//20
        "NO_DAMAGE","CHANGE_PATTERN","IF_FOR_CHILDREN","REVIVAL","CONTINUOUS_ATTACK","GIVE_VALUE_AS_ADDITIVE","GIVE_VALUE_AS_MULTIPLE",//27
        "IF_FOR_ALL","SEARCH_AREA_CHANGE","DESTROY","CONTINUOUS_ATTACK_NEARBY","ENCHANT_LIFE_STEAL","ENCHANT_STRIKE_BACK",//33
        "ACCUMULATIVE_DAMAGE","SEAL","ATTACK_FIELD","HEAL_FIELD","CHANGE_PARAMETER_FIELD","ABNORMAL_STATE_FIELD","KETSUBAN",//40
        "UB_CHANGE_TIME","LOOP_TRIGGER","IF_HAS_TARGET","WAVE_START_IDLE","SKILL_EXEC_COUNT","RATIO_DAMAGE","UPPER_LIMIT_ATTACK",//47
        "REGENERATION","BUFF_DEBUFF_CLEAR","LOOP_MOTION_BUFF_DEBUFF","DIVISION","CHANGE_BODY_WIDTH","IF_EXISTS_FIELD_FOR_ALL",//53
        "STEALTH","MOVE_PARTS","COUNT_BLIND","COUNT_DOWN","STOP_FIELD","INHIBIT_HEAL","ATTACK_SEAL",//60
        "FEAR","AWE","LOOP_MOTION_REPEAT","?","?","?","?","?","TOAD","FORCE_HP_CHANGE",//70
        "KNGHT_GUARD","DAMAGE_CUT","LOG_BARRIER","GIVE_VALUE_AS_DIVIDE","?","?","?","?","?","?",//80
        "?","?","?","?","?","?","?","?","?","PASSIVE",//90
        "PASSIVE_INTERMITTENT","?","?"//93
            };
        /// <summary>
        /// 不要用这个构造函数！
        /// </summary>
        public SkillAction() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ty"></param>
        /// <param name="de">长度为3</param>
        /// <param name="val">长度为7</param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <param name="t4"></param>
        /// <param name="t5"></param>
        /// <param name="t6"></param>
        /// <param name="des"></param>
        /// <param name="lvdes"></param>
        public SkillAction(int acid, int id, int ty, int[] de, double[] val, int t1, int t2, int t3, int t4, int t5, int t6, string des, string lvdes)
        {
            actionid = acid;
            classid = id;
            type = ty;
            details = de;
            values = val;
            target_assigment = t1;
            target_area = t2;
            target_range = t3;
            target_type = t4;
            target_number = t5;
            target_count = t6;
            description = des;
            levelupdes = lvdes;
        }
        public SkillAction(string dat)
        {
            string[] data = dat.Split('#');
            actionid = int.Parse(data[12]);
            classid = int.Parse(data[0]);
            type = int.Parse(data[1]);
            details = JsonConvert.DeserializeObject<int[]>(data[2]);
            values = JsonConvert.DeserializeObject<double[]>(data[3]);
            target_assigment = int.Parse(data[4]);
            target_area = int.Parse(data[5]);
            target_range = int.Parse(data[6]);
            target_type = int.Parse(data[7]);
            target_number = int.Parse(data[8]);
            target_count = int.Parse(data[9]);
            description = data[10];
            levelupdes = data[11];
        }
        public string GetTargetDescription()
        {
            if(MainManager.Instance.SkillDataDic.TryGetValue(actionid/100,out SkillData skillData))
            {
                for(int i = 0; i < skillData.skillactions.Length; i++)
                {
                    if(skillData.skillactions[i] == actionid)
                    {
                        if (skillData.dependactions[i] != 0)
                        {
                            int num = skillData.dependactions[i] % 10;
                            return SkillData.num[num - 1] + "的目标";
                        }
                    }
                }
            }
            return  ((DirectionType)target_area).GetDescription() + "范围" + (target_range < 1 ? "普攻范围" : target_range.ToString())
                + "按照" + ((PirorityPattern)target_type).GetDescription() + "排列的从第" + (target_number + 1) + "个开始的" + target_count
                + "个" + ((eTargetAssignment)target_assigment).GetDescription() +"目标"; 
        }
        /// <summary>
        /// 获取技能片段的详情
        /// </summary>
        /// <param name="skilllevel">技能等级</param>
        /// <param name="atk">角色攻击力（物理/魔法）</param>
        /// <returns>技能详情描述字符串</returns>
        public string GetDetail(int skilllevel, float atk)
        {
            string description_old = this.description;
            if (MainManager.Instance.SkillActionDescribe_cn.ContainsKey(actionid))
            {
                description_old = MainManager.Instance.SkillActionDescribe_cn[actionid];
            }
            string description = description_old == "" ? "" : description_old + "\n";
            string detailstr = "";
            switch (type)
            {
                case 1://(ATTACK)伤害类技能，单体/群体物理/魔法攻击类
                    detailstr = string.Format(description, values[0] + values[1] * skilllevel + values[2] * atk) +
                    "<color=#002DFF>(" + values[0] + "+" + values[1] + "✕技能等级+" + values[2] + "✕ATK";
                    break;
                case 2://(MOVE)
                    detailstr = description + "<color=#002DFF>(" + MoveAction.GetActionDescription(details[0]) + ",目标:" + GetTargetDescription();
                    break;
                case 3://(KNOCK)击退类技能
                    if(details[0] == 1)
                    {
                        detailstr = description + "<color=#002DFF>(将目标沿Y轴以" + values[2] + "的速度击飞" + values[0] + ",再以"
                            + values[3] + "的速度降落至原处";
                    }
                    else
                    {
                        string des_2 = values[0] > 0 ? ("击退目标，击退距离："  + values[0]):( "拉近目标，拉近距离：" + Math.Abs(values[0]));
                        detailstr = description + "<color=#002DFF>(" + des_2 + ",速度：" + values[2] +"，并打断对方技能";
                    }
                    detailstr += "，目标：" + GetTargetDescription();
                    break;
                case 4://(HEAL)治疗类技能，包括单奶和群奶等
                    string detail_heal;
                    if(values[0] == 1)
                    {
                        detail_heal = values[1] + "+" + values[2] + "✕技能等级+" + values[3] + "✕ATK";
                    }
                    else
                    {
                        detail_heal = "回复比例：" + values[1] + "%";
                    }
                    detailstr = string.Format(description, values[1] + values[2] * skilllevel + values[3] * atk) +
                    "<color=#002DFF>(" + detail_heal;
                    break;
                case 5://(CURE)只有一个怪有这个技能
                    detailstr = description +
                    "<color=#002DFF>(未知技能片段[CURE]";
                    break;
                case 6://(BARRIER)物理/魔法伤害无效/吸收盾
                    detailstr = string.Format(description, values[0] + values[1] * skilllevel) +
                    "<color=#002DFF>(" + values[0] + "+" + values[1] + "✕技能等级,持续时间：" + values[2];
                    break;
                case 7://(REFLEXIVE)切换视角
                    detailstr = description +
                    "<color=#002DFF>(切换视角至距离目标 " + values[0] + "处，目标：" + GetTargetDescription();
                    break;
                case 8://(CHANGE_SPEED)加速、敌方行动不能
                    string acdetailname = ((ChangeSpeedAction.eChangeSpeedType)details[0]).GetDescription();
                    switch (details[0])
                    {
                        case 1://敌方行动速度降低
                            detailstr = description +
                            "<color=#002DFF>(" + acdetailname +",降低倍率：" + values[0] + "持续时间：" + values[2];
                            break;
                        case 2://己方行动速度提升
                            detailstr = description +
                            "<color=#002DFF>(" + acdetailname + ",提升倍率：" + values[0] + "持续时间：" + values[2];
                            break;
                        case 3://麻痹
                        case 4://冻结
                        case 5://束缚
                        case 6://???
                        case 8://???
                        case 9://???
                        case 10://???
                        case 11://时停
                        case 7://敌方行动不能
                            detailstr = description +
                            "<color=#002DFF>(" + acdetailname + ",成功率等级：" + skilllevel + "持续时间：" + values[2];
                            break;
                        default:
                            detailstr = description +
                            "<color=#002DFF>(未知异常类buff[" + details[0] + "]";
                            break;

                    }
                    break;
                case 9://(SLIP_DAMAGE)DOT伤害
                    detailstr = string.Format(description, values[0] + values[1] * skilllevel) +
                    "<color=#002DFF>(" + values[0] + "+" + values[1] + "✕技能等级,DOT持续时间：" + values[2];
                    break;
                case 10://(BUFF_DEBUFF)
                    //detail1为BUFF类型，尾数0为BUFF，1为DEBUFF，detail2为BUFF开始释放种类，玩家技能都不为2，detail3未知，似乎没啥用
                    //
                    detailstr = string.Format(description, values[1] + values[2] * skilllevel) +
                    "<color=#002DFF>(" + values[1] + "+" + values[2] + "✕技能等级,持续时间：" + values[3] + values[4]*skilllevel;
                    break;
                case 11://(CHARM)混乱/诱惑
                case 12://(BLIND)黑暗
                    detailstr = description +
                    "<color=#002DFF>(成功率等级：" + skilllevel + "持续时间：" + values[0];
                    break;
                case 13://(SCIENCE)沉默 只有敌方有
                    detailstr = description +
                    "<color=#002DFF>(沉默";
                    break;
                case 14://(MODE_CHANGE)变身
                    detailstr = description +
                    "<color=#002DFF>(更改状态";
                    break;
                case 15://(SUMMON)召唤
                    detailstr = description +
                    "<color=#002DFF>(召唤物等级：" + skilllevel;
                    break;
                case 16://(CHARGE_ENERGY)TP回复/流失
                    detailstr = string.Format(description, values[0] + (int)Math.Ceiling(values[1] * skilllevel)) +
                    "<color=#002DFF>(" + values[0] + "+" + values[1] + "✕技能等级(向上取整)";
                    break;
                case 17://(TRIGER)触发特殊技能(比如中二自爆)
                    detailstr = description +
                    "<color=#002DFF>(HP触发比例：" + values[2];
                    break;
                case 18://(DAMAGE_CHARGE)
                    detailstr = description +
                    "<color=#002DFF>(未知技能片段[DAMAGE_CHARGE]";
                    break;
                case 19://(CHARGE)
                    detailstr = description +
                    "<color=#002DFF>(未知技能片段[CHARGE]";
                    break;
                case 20://(DECOY)嘲讽
                    detailstr = description +
                    "<color=#002DFF>(持续时间:" + values[0] + "+" + values[1] + "✕技能等级";
                    break;
                case 21://(NO_DAMAGE)无敌
                    detailstr = description +
                    "<color=#002DFF>(无敌,持续时间:" + values[0] + "+" + values[1] + "✕技能等级";
                    break;
                case 26://加法赋值
                    detailstr = description + "<color=#002DFF>(使" + (details[0] % 10) + "的效果加上" + ((GiveValueAction.eAdditiveValueType)values[0]).GetDescription()
                        + "✕(" + values[1] + "+" + values[2] + "✕技能等级)";
                    break;
                case 27:
                    detailstr = description + "<color=#002DFF>(使" + (details[0] % 10) + "的效果乘以" + ((GiveValueAction.eAdditiveValueType)values[0]).GetDescription()
                        + "比例"+ "✕(" + values[1] + "+" + values[2] + "✕技能等级)";
                    break;
                case 28:
                    detailstr = description +"<color=#002DFF>(排他条件分支";
                    break;
                case 30:
                    detailstr = description +
                    "<color=#002DFF>(技能片段:即死";
                    break;
                case 32:
                    detailstr = "<color=#002DFF>(生命偷取，" + values[0] + "+" + values[1] + "✕技能等级,次数：" + values[3];
                    break;
                case 34://伤害累加，只有狗拳有
                    detailstr = string.Format(description, values[1] + values[2] * skilllevel) +
                    "<color=#002DFF>(" + values[1] + "+" + values[2] + "✕技能等级,追加上限：" + values[3];
                    break;
                case 35:
                    detailstr = description +
                    "<color=#002DFF>(持续时间:" + values[2];
                    break;
                case 36:
                    detailstr = string.Format(description, values[0] + values[1] * skilllevel + values[2] * atk) +
                    "<color=#002DFF>(" + values[0] + "+" + values[1] + "✕技能等级+" + values[2] + "✕ATK,持续时间:" + values[4] + "范围:" + values[6];
                    break;
                case 37:
                    detailstr = string.Format(description, values[0] + values[1] * skilllevel + values[2] * atk) +
                    "<color=#002DFF>(" + values[0] + "+" + values[1] + "✕技能等级+" + values[2] + "✕ATK,持续时间:" + values[4] + "范围:" + values[6];
                    break;
                case 38:
                    detailstr = string.Format(description, values[0] + values[1] * skilllevel) +
                    "<color=#002DFF>(" + values[0] + "+" + values[1] + "✕技能等级,持续时间：" + values[2] + "范围:" + values[4];
                    break;
                case 44:
                    detailstr = description +"<color=#002DFF>(回合开始后在绝对坐标1400处等待" + values[0] + "秒";
                    break;
                case 45:
                    detailstr = description + "<color=#002DFF>(计数器+1，上限" + values[0];
                    break;
                case 46://百分比扣血
                    detailstr = description +
                    "<color=#002DFF>(扣血比例：" + values[0];
                    break;
                case 48://每秒恢复类
                    detailstr = string.Format(description, values[0] + values[1] * skilllevel + values[2] * atk) +
                    "<color=#002DFF>(" + values[0] + "+" + values[1] + "✕技能等级+" + values[2] + "✕ATK,持续时间:" + values[4];
                    break;

                case 50:
                    detailstr = string.Format(description, values[1] + values[2] * skilllevel) +
                    "<color=#002DFF>(" + values[1] + "+" + values[2] + "✕技能等级,持续时间:" + values[3];
                    break;
                case 56:
                    detailstr = description +
                    "<color=#002DFF>(成功率等级：" + skilllevel;
                    break;

                case 90:
                    detailstr = string.Format(description, values[1] + values[2] * skilllevel) +
                    "<color=#002DFF>(" + values[1] + "+" + values[2] + "✕技能等级";
                    break;
                default:
                    detailstr = description +
                    "<color=#002DFF>(未知技能片段[" + type + "]";
                    break;

            }
            //if(description == "") { return ""; }

            if (target_range > 0 && target_count > 1)
            {
                detailstr += ",范围：" + target_range;
            }
            detailstr += ")</color>";
            return detailstr;
        }
        /// <summary>
        /// 获取被动技能加的数值
        /// </summary>
        /// <param name="skilllevel">技能等级</param>
        /// <returns>对应数值</returns>
        public BaseData GetEXSkillApplyedValue(int skilllevel)
        {
            BaseData b = new BaseData();
            switch (details[0])
            {
                case 1:
                    b.Hp += (float)(values[1] + values[2] * skilllevel);
                    break;
                case 2:
                    b.Atk += (float)(values[1] + values[2] * skilllevel);
                    break;
                case 3:
                    b.Def += (float)(values[1] + values[2] * skilllevel);
                    break;
                case 4:
                    b.Magic_str += (float)(values[1] + values[2] * skilllevel);
                    break;
                case 5:
                    b.Magic_def += (float)(values[1] + values[2] * skilllevel);
                    break;
                case 6:
                    b.Physical_critical += (float)(values[1] + values[2] * skilllevel);
                    break;
                case 7:
                    b.Magic_critical += (float)(values[1] + values[2] * skilllevel);
                    break;
                default:
                    Debug.LogError("未设置类型为:" + details[0] + "的被动技能应用！");
                    break;
            }
            return b;
        }

        public void Override2With(SkillAction other)
        {
            other.description = description;
            other.levelupdes = levelupdes;
        }

        public override string ToString()
        {
            string[] dat = new string[13]
            {
            classid+"",type+"",
            JsonConvert.SerializeObject(details),
            JsonConvert.SerializeObject(values),
            target_assigment+"",target_area+"",
            target_range+"",target_type+"",
            target_number+"",target_count+"",
            description,levelupdes,actionid+""
            };
            return string.Join("#", dat);
        }
    }
    /// <summary>
    /// 装备基础数据，对应数据库中的equipment_data 和 equipment_enhance_rate表,忽视部分鸡肋属性-
    /// </summary>
    [Serializable]
    public class EquipmentData : IOverride2<EquipmentData>
    {
        public int equipment_id;
        public string equipment_name;
        public int promotion_level;
        public string description;
        public bool craftFlg;
        public int equipmentEnhancePoint;
        public int salePrice;
        public int requireLevel;
        public BaseData data;
        public BaseData data_rate;
        public static EquipmentData EMPTYDATA { get => GetEmptyData(); }
        public EquipmentData() { }
        public EquipmentData(int id, string name, int prolevel, BaseData data, BaseData data_rate)
        {
            equipment_id = id;
            equipment_name = name;
            promotion_level = prolevel;
            this.data = data;
            this.data_rate = data_rate;
        }
        public EquipmentData(string data)
        {
            string[] data0 = data.Split('-');
            equipment_id = int.Parse(data0[0]);
            equipment_name = data0[1];
            promotion_level = int.Parse(data0[2]);
            description = data0[3];
            craftFlg = data0[4] == "1" ? true : false;
            equipmentEnhancePoint = int.Parse(data0[5]);
            salePrice = int.Parse(data0[6]);
            requireLevel = int.Parse(data0[7]);
            this.data = new BaseData(data0[8]);
            data_rate = new BaseData(data0[9]);
        }
        public EquipmentData(int equipment_id, string equipment_name, int promotion_level, string description, bool craftFlg,
            int equipmentEnhancePoint, int salePrice, int requireLevel, BaseData data, BaseData data_rate)
        {
            this.equipment_id = equipment_id;
            this.equipment_name = equipment_name;
            this.promotion_level = promotion_level;
            this.description = description;
            this.craftFlg = craftFlg;
            this.equipmentEnhancePoint = equipmentEnhancePoint;
            this.salePrice = salePrice;
            this.requireLevel = requireLevel;
            this.data = data;
            this.data_rate = data_rate;
        }
        public static EquipmentData GetEmptyData()
        {
            return new EquipmentData(999999, "空", 1, "空", false, 0, 0, 1, new BaseData(), new BaseData());
        }

        /// <summary>
        /// 获取装备的最大强化等级
        /// </summary>
        /// <returns></returns>
        public int GetMaxLevel()
        {
            return promotion_level;
        }
        /// <summary>
        /// 获取装备的具体数据
        /// </summary>
        /// <param name="level">装备等级</param>
        /// <returns></returns>
        public BaseData GetEquipmentData(int level)
        {
            if (level >= GetMaxLevel())
            {
                return BaseData.CeilToInt(data + GetMaxLevel() * data_rate);
            }

            if (level > 0)
            {
                return BaseData.CeilToInt(data + level * data_rate);
            }

            return data;
        }

        public void Override2With(EquipmentData other)
        {
            other.description = description;
            other.equipment_name = equipment_name;
        }

        public override string ToString()
        {
            string[] str = new string[10] { equipment_id + "", equipment_name, promotion_level + "",description,(craftFlg?"1":"0"),
                equipmentEnhancePoint + "",salePrice + "",requireLevel + "", data.ToString(), data_rate.ToString() };
            return string.Join("-", str);
        }

    }
    /// <summary>
    /// 角色羁绊类，对应数据库中story_detail和chara_story_status表$
    /// </summary>
    [Serializable]
    public class UnitStoryData
    {
        static int[] loveTrans = new int[13] { 0, 1, 1, 1, 2, 3, 3, 3, 4 ,4,4,4,4};
        public int unitid;
        public int loveMax;
        public List<int> effectCharacters;//受影响的角色id列表
        public List<List<int[]>> addValues;//增加的属性，【属性序号，属性值】
        /// <summary>
        /// 不要使用这个构造函数
        /// </summary>
        public UnitStoryData()
        {
            unitid = 0;
            effectCharacters = new List<int>();
            effectCharacters.Add(0);
            addValues = new List<List<int[]>>();
            addValues.Add(new List<int[]>());
            addValues[0].Add(new int[2] { 0, 0 });
        }
        /// <summary>
        /// 角色羁绊类构造函数
        /// </summary>
        /// <param name="id">角色id</param>
        /// <param name="lovemax">羁绊上限</param>
        /// <param name="eff_ch">受影响角色列表</param>
        /// <param name="add_v">增加的属性【序号，值】</param>
        public UnitStoryData(int id, int lovemax, List<int> eff_ch, List<List<int[]>> add_v)
        {
            unitid = id;
            loveMax = lovemax;
            effectCharacters = eff_ch;
            addValues = add_v;
        }
        public UnitStoryData(string data)
        {
            string[] data0 = data.Split('$');
            unitid = int.Parse(data0[0]);
            loveMax = int.Parse(data0[1]);
            effectCharacters = JsonConvert.DeserializeObject<List<int>>(data0[2]);
            addValues = JsonConvert.DeserializeObject<List<List<int[]>>>(data0[3]);
        }
        /// <summary>
        /// 获取角色羁绊对应的奖励属性
        /// </summary>
        /// <param name="love">羁绊等级</param>
        /// <returns></returns>
        public BaseData GetLoveValues(int love)
        {
            if (love < 0 || love > 12)
            {
                string word = "羁绊值设置错误！";
                MainManager.Instance.WindowConfigMessage("<color=#FF0000>" + word + "</color>", null);
                love = love < 0 ? 0 : 8;
            }
            if(loveMax == 4)
            {
                love = loveTrans[love];
            }
            //int k = Mathf.Min(love - 2,addValues.Count-1);
            BaseData bt = new BaseData();
            //string message = "";
            for (int k = 1; k < love; ++k)
            {
                if (k >= addValues.Count+1)
                    break;
                //message += "第" +k + "轮：";
                foreach (int[] values in addValues[k-1])
                {
                    //message += "属性：" + values[0] + "+" + values[1]+",";
                    int val = values[1];
                    switch (values[0])
                    {
                        case 1: bt.Hp += val; break;
                        case 2: bt.Atk += val; break;
                        case 3: bt.Def += val; break;
                        case 4: bt.Magic_str += val; break;
                        case 5: bt.Magic_def += val; break;
                        case 6: bt.Physical_critical += val; break;
                        case 7: bt.Magic_critical += val; break;
                        case 8: bt.Dodge += val; break;
                        case 9: bt.Life_steal += val;break;
                        case 10: bt.Atk += val; break;//HP自动回复
                        case 11: bt.Wave_energy_recovery += val; break;//技能自动回复
                        case 12: bt.Physical_penetrate += val;break;
                        case 13: bt.Magic_penetrate += val;break;
                        case 14: bt.Energy_recovery_rate += val; break;//TP上升
                        case 15: bt.Hp_recovery_rate += val; break;//回复量上升
                        case 16: bt.Enerey_reduce_rate += val;break;

                        case 0:
                            break;

                        case 17://妹弓12级羁绊有这个，暂时不知道是啥
                            //Debug.LogError("未知属性……"); 
                            bt.Accuracy += val;
                            break;
                        default:
                            Debug.LogError("未定义该属性！"); break;
                    }
                }
            }
            //Debug.Log(message + "共" + bt.magic_def + "&" + bt.def + "&" + bt.dodge);
            return bt;
        }
        public override string ToString()
        {
            return unitid + "$" + loveMax + "$" + JsonConvert.SerializeObject(effectCharacters) + "$" +
                JsonConvert.SerializeObject(addValues);
        }
    }
    /// <summary>
    /// 角色基础信息以及战斗相关数据*
    /// </summary>
    public class UnitDetailData
    {
        public int unitid; 
        public int prefabIdBattle;
        public string name;
        public int minrarity;//初始星级
        public int motionType;//武器动作类型
        public int seType;//武器类型
        public int searchAreaWidth;//普攻距离
        public int moveSpeed; // 移速 有的角色移速是0！
        public int atkType;//攻击方式
        public float normalAtkCastTime;//普攻间隔
        public int guildId;
        /// <summary>
        /// 不要用这个构造函数！
        /// </summary>
        public UnitDetailData() { }
        public UnitDetailData(string data)
        {
            string[] data0 = data.Split('*');
            unitid = int.Parse(data0[0]);
            name = data0[1];
            minrarity = int.Parse(data0[2]);
            motionType = int.Parse(data0[3]);
            seType = int.Parse(data0[4]);
            searchAreaWidth = int.Parse(data0[5]);
            atkType = int.Parse(data0[6]);
            normalAtkCastTime = float.Parse(data0[7]);
            guildId = int.Parse(data0[8]);

        }
        public UnitDetailData(int id, string name, int rarity, int motion, int se, int searchWidth,
            int atk, float casetime, int guid)
        {
            unitid = id; this.name = name; minrarity = rarity; motionType = motion;
            seType = se; searchAreaWidth = searchWidth; atkType = atk;
            normalAtkCastTime = casetime; guildId = guid;
        }
        public override string ToString()
        {
            return unitid + "*" + name + "*" + minrarity + "*" + motionType + "*" + seType
                + "*" + searchAreaWidth + "*" + atkType + "*" + normalAtkCastTime + "*" + guildId;
        }
    }
    /// <summary>
    /// 数据基类，+
    /// </summary>
    [Serializable]
    public class BaseData
    {
        /*public float hp, atk, magic_str, def, magic_def, physical_critical,//0-5
            magic_critical, wave_hp_recovery, wave_energy_recovery, dodge,//6-9
            physical_penetrate, magic_penetrate, life_steal, hp_recovery_rate,//10-13
            energy_recovery_rate, enerey_reduce_rate, accuracy;//14-16*/
        public long[] dataint;//数据为*100后的整数，计算升级数据等时用整数计算避免浮点数误差累积。(用decimal不就好了）
        public int realhp = 0;
        [JsonIgnore]
        public int RealHp => realhp == 0 ? Mathf.RoundToInt(Hp) : realhp;
        [JsonIgnore]
        public float Hp
        { 
            get => dataint[0]/100.0f; 
            set{
                if (value>int.MaxValue/100)
                    dataint[0] = (long)(value * 100.0);
                else
                    dataint[0] = Mathf.RoundToInt((value * 100));
            }
        }
        [JsonIgnore]
        public float Atk { get => dataint[1] / 100.0f; set => dataint[1] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Magic_str { get => dataint[2] / 100.0f; set => dataint[2] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Def { get => dataint[3] / 100.0f; set => dataint[3] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Magic_def { get => dataint[4] / 100.0f; set => dataint[4] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Physical_critical { get => dataint[5] / 100.0f; set => dataint[5] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Magic_critical { get => dataint[6] / 100.0f; set => dataint[6] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Wave_hp_recovery { get => dataint[7] / 100.0f; set => dataint[7] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Wave_energy_recovery { get => dataint[8] / 100.0f; set => dataint[8] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Dodge { get => dataint[9] / 100.0f; set => dataint[9] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Physical_penetrate { get => dataint[10] / 100.0f; set => dataint[10] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Magic_penetrate { get => dataint[11] / 100.0f; set => dataint[11] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Life_steal { get => dataint[12] / 100.0f; set => dataint[12] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Hp_recovery_rate { get => dataint[13] / 100.0f; set => dataint[13] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Energy_recovery_rate { get => dataint[14] / 100.0f; set => dataint[14] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Enerey_reduce_rate { get => dataint[15] / 100.0f; set => dataint[15] = Mathf.RoundToInt((value * 100)); }
        [JsonIgnore]
        public float Accuracy { get => dataint[16] / 100.0f; set => dataint[16] = Mathf.RoundToInt((value * 100)); }
        //public int[] Dataint { get => dataint; set => dataint = value; }

        //public int[] Dataint { get => dataint;private set => dataint = value; }

        public BaseData()
        {
            dataint = new long[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }
        public BaseData(double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j,
            double k, double l, double m, double n,
            double o, double p, double q)
        {
            dataint = new long[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Hp = (float)a; Atk = (float)b; Magic_str = (float)c; Def = (float)d; Magic_def = (float)e; Physical_critical = (float)f;
            Magic_critical = (float)g; Wave_hp_recovery = (float)h; Wave_energy_recovery = (float)i; Dodge = (float)j;
            Physical_penetrate = (float)k; Magic_penetrate = (float)l; Life_steal = (float)m; Hp_recovery_rate = (float)n;
            Energy_recovery_rate = (float)o; Enerey_reduce_rate = (float)p; Accuracy = (float)q;
        }
        /*public BaseData(int[] data)
        {
            dataint = data;
        }*/
        public BaseData(string data)
        {
            BaseData baseData = JsonConvert.DeserializeObject<BaseData>(data);
            dataint = baseData.dataint;
            /*dataint = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            string[] data0 = data.Split('+');
            for (int i = 0; i < 17; i++)
            {
                this.dataint[i] = int.Parse(data0[i]);
            }*/
        }
        public float[] Data2FloatGroup()
        {
            return new float[17]
            {
            Hp,Atk,Magic_str,Def, Magic_def,Physical_critical,
        Magic_critical,Wave_hp_recovery, Wave_energy_recovery,Dodge,
        Physical_penetrate ,Magic_penetrate,Life_steal,Hp_recovery_rate ,
        Energy_recovery_rate,Enerey_reduce_rate,Accuracy
            };
        }
        public float GetPowerValue(UnitData unit)// int[] skillLevel, int rarity,bool hasUneq)
        {
            int[] skillLevel = unit.skillLevel;
            float pow = Mathf.RoundToInt(Hp) * 0.1f;
            pow += Mathf.RoundToInt(Atk) + Mathf.RoundToInt(Magic_str);
            pow += (Mathf.RoundToInt(Def) + Mathf.RoundToInt(Magic_def)) * 4.5f;
            pow += (Physical_critical + Magic_critical) * 0.5f;
            pow += (Dodge + Physical_penetrate + Magic_penetrate) * 6;
            pow += Wave_hp_recovery * 0.1f;
            pow += Wave_energy_recovery * 0.3f;
            pow += Life_steal * 4.5f;
            pow += Hp_recovery_rate;
            pow += Energy_recovery_rate * 1.5f;
            pow += Enerey_reduce_rate * 3;
            pow += Accuracy * 2;
            pow += (skillLevel[0] + skillLevel[1] + skillLevel[2] + skillLevel[3]) * 10;
            if (unit.rarity>=5)
            {
                pow += 150;
            }
            if (unit.uniqueEqLv>0)
            {
                pow += skillLevel[1] * 2 + 100;
            }
            if (unit.rarity >= 6)
            {
                pow += skillLevel[0] * 5 + 2000;
            }
            return pow;

        }
        public static BaseData operator +(BaseData a, BaseData b)
        {
            BaseData data = new BaseData();
            for (int i = 0; i < 17; i++)
            {
                data.dataint[i] = a.dataint[i] + b.dataint[i];
            }

            if (a.realhp != 0 || b.realhp != 0)
                data.realhp = a.RealHp + b.RealHp;
            return data;
        }
        public static BaseData operator -(BaseData a, BaseData b)
        {
            BaseData data = new BaseData();
            for (int i = 0; i < 17; i++)
            {
                data.dataint[i] = a.dataint[i] - b.dataint[i];
            }
            return data;
        }
        public static BaseData operator *(int k, BaseData a)
        {
            BaseData data = new BaseData();
            for (int i = 0; i < 17; i++)
            {
                data.dataint[i] = a.dataint[i] * k;
            }
            return data;
        }
        public static BaseData operator *(BaseData a, int k)
        {
            return k * a;
        }


        public BaseData CeilMul(double k)
        {
            BaseData data = new BaseData();
            for (int i = 0; i < 17; i++)
            {
                data.dataint[i] = (long) Math.Ceiling(dataint[i] * k);
            }
            return data;
        }


        // 高rank
        public static BaseData operator *(BaseData a, BaseData b)
        {
            BaseData data = new BaseData();
            for (int i = 0; i < 17; i++)
            {
                data.dataint[i] = (long) Math.Ceiling(a.dataint[i] * b.dataint[i] / 100000000.0) * 100;
            }
            return data;
        }

        public static BaseData Ceiling(BaseData a)
        {
            for (int i = 1; i < a.dataint.Length; i++)
            {
                a.dataint[i] = (int) Math.Ceiling(a.dataint[i] / 100.0);
            }
            return a;
        }

        public static BaseData CeilToInt(BaseData a)
        {
            for (int i = 1; i < a.dataint.Length; i++)
            {
                a.dataint[i] = 100 * Mathf.CeilToInt(a.dataint[i] / 100.0f);
            }
            return a;
        }
        private static int round(float _f)
        {
            int num = 0;
            if (_f - (float)(int)_f >= 0.5f || Mathf.Approximately((float)(int)_f + 0.5f, _f))
            {
                return (int)_f + 1;
            }
            return (int)_f;
        }

        public static BaseData Round(BaseData a)
        {
            for (int i = 0; i < a.dataint.Length; i++)
            {
                a.dataint[i] = 100 * round(a.dataint[i] / 100.0f);
            }
            return a;
        }
        public string[] Compare(BaseData b)
        {
            float[] miss = (this - b).Data2FloatGroup();
            string[] result = new string[17]
            {"","","","","","","","","","","","","","","","",""};
            for (int i = 0; i < 17; i++)
            {
                if (miss[i] > 0.01f) { result[i] = "\n<size=10><color=#FF0000>(+" + miss[i] + ")</color></size>"; }
                if (miss[i] < -0.01f) { result[i] = "\n<size=10><color=#0000FF>(" + miss[i] + ")</color></size>"; }
            }
            return result;
        }
        public static string Compare_2(int a, int b)
        {
            string result = "";
            int miss = a - b;
            if (miss > 0) { result = "\n<size=10><color=#FF0000>(+" + miss + ")</color></size>"; }
            if (miss < 0) { result = "\n<size=10><color=#0000FF>(" + miss + ")</color></size>"; }
            return result;
        }
        public override string ToString()
        {
            /*List<string> str = new List<string>();
            foreach (int i in dataint)
            {
                str.Add(i + "");
            }
            return string.Join("+", str.ToArray());*/
            return JsonConvert.SerializeObject(this);
        }

    }

    [Serializable]
    public class PlayerSetting
    {
        public bool tpCalculationChanged = true;
        public int playerLevel = 240;
        public int playerProcess = 55;
        //public int maxLove = 8;
        public int maxUniqueEqLv = 240;
        //public float bodyWidth = 100;
        public bool allowRarity6 = true;
        public bool showAllUnits = true;
        public int pixFix = 0;
        public bool useDMMpath = false;
        public string dmmPath = "";
        public bool ignoreLogBarrierCritical = false;

        public int maxTPUpValue = 250;//TP上限

        public Dictionary<eStateIconType, bool> ShowBuffDic = new Dictionary<eStateIconType, bool>();

        public int GetMaxRank()
        {
            switch (playerProcess)
            {
                case 10:
                    return 8;
                case 11:
                case 12:
                    return 9;
                /*case 13:
                case 14:
                case 15:
                    return 10;
                case 16:
                case 17:
                case 18:
                    return 11;
                case 19:
                case 20:
                case 21:
                    return 12;
                case 22:
                case 23:
                case 24:
                    return 13;
                case 25:
                case 26:
                case 27:
                    return 14;
                case 28:
                case 29:
                case 30:
                    return 15;
                case 31:
                case 32:
                case 33:
                    return 16;
                case 34: 
                case 35: 
                case 36:
                    return 17;
                case 37: 
                case 38: 
                case 39:
                    return 18;*/
                default:
                    return (playerProcess - 13) / 3 + 10;
            }
        }
        public bool[] GetEquipmentIsAble()
        {
            bool[] max = new bool[6] { true,true,true,true,true,true };
            int type = (playerProcess - 15) % 3;
            switch (playerProcess)
            {
                case 10://8-5
                case 12://9-5
                /*case 15://10-5
                case 18://11-5
                case 21://12-5
                case 24://13-5
                case 27://14-5
                case 30://15-5
                case 33://16-5
                case 36://17-5
                case 39://18-5*/
                    max = new bool[6] { false, true, true, true, true, true };
                    break;
                case 11://9-3
                case 13://10-3
                /*case 16://11-3
                case 19://12-3
                case 22://13-3
                case 25://14-3
                case 28://15-3
                case 31://16-3
                case 34://17-3
                case 37://18-3*/
                    max = new bool[6] { false, true, false, true, false, true };
                    break;
                case 14://10-4
                /*case 17://11-4
                case 20://12-4
                case 23://13-4
                case 26://14-4
                case 29://15-4
                case 32://16-4
                case 35://17-4
                case 38://18-4*/
                    max = new bool[6] { false, true, false, true, true, true };
                    break;
                default:
                    switch (type)
                    {
                        case 0:
                            return new bool[6] { false, true, true, true, true, true };
                        case 1:
                            return new bool[6] { false, true, false, true, false, true };
                        case 2:
                            return new bool[6] { false, true, false, true, true, true };
                    }
                    break;
            }            
            return max;
        }
    }
    /*
    [Serializable]
    public class AllData
    {
        public Dictionary<int, EquipmentData> equipmentDic;//装备类与装备id的对应字典
        public Dictionary<int, UnitRarityData> unitRarityDic;//角色基础数据与角色id的对应字典 
        public Dictionary<int, UnitStoryData> unitStoryDic;//角色羁绊奖励列表
        public Dictionary<int, List<int>> unitStoryEffectDic;//角色的马甲列表
        public Dictionary<int, SkillData> skillDataDic;//所有的技能列表
        public Dictionary<int, SkillAction> skillActionDic;//所有小技能列表
        public Dictionary<int, string> unitName_cn = new Dictionary<int, string>();//角色中文名字
        public Dictionary<int, string[]> skillNameAndDescribe_cn = new Dictionary<int, string[]>();//技能中文名字和描述
        public Dictionary<int, string> skillActionDescribe_cn = new Dictionary<int, string>();//技能片段中文描述

        public AllData() { }*/
        /*
        public AllData(List<string> equipmentDic,
            List<string> unitRarityDic,
            List<string> unitStoryDic,
            Dictionary<int, List<int>> unitStoryEffectDic,
            List<string> skillDataDic,
            List<string> skillActionDic,
            Dictionary<int, string> unitName_cn,
            Dictionary<int, string[]> skillNameAndDescribe_cn,
            Dictionary<int, string> skillActionDescribe_cn
            )
        {
            this.equipmentDic = equipmentDic;
            this.unitRarityDic = unitRarityDic;
            this.unitStoryDic = unitStoryDic;
            this.unitStoryEffectDic = unitStoryEffectDic;
            this.skillDataDic = skillDataDic;
            this.skillActionDic = skillActionDic;
            this.unitName_cn = unitName_cn;
            this.skillNameAndDescribe_cn = skillNameAndDescribe_cn;
            this.skillActionDescribe_cn = skillActionDescribe_cn;
        }*/
    //}
    [Serializable]
    public class UnitPrefabData
    {
        public Dictionary<string, List<FirearmCtrlData>> unitFirearmDatas;
    }
    [Serializable]
    public class UnitSkillTimeData
    {
        public int unitId;
        public float spineTime_Attack;
        public float spineTime_UB;
        public float spineTime_MainSkill1;
        public float spineTime_MainSkill2;
        public Dictionary<string, float> all_spineTime_Dic = new Dictionary<string, float>();
        public bool useDefaultDelay;
        public Dictionary<int, Dictionary<int,bool>> skillExecDependParentaction;
        public Dictionary<int, Dictionary<int, bool>> skillExecDependEffect;
        public Dictionary<int, float[]> actionExecTime_Attack;
        public Dictionary<int, float[]> actionExecTime_UB;
        public Dictionary<int, float[]> actionExecTime_MainSkill1;
        public Dictionary<int, float[]> actionExecTime_MainSkill2;
        public Dictionary<int, List<EffectData>> actionEffectDic;
    }
    [Serializable]
    public class EffectData
    {
        public float[] execTime;
        public float moveRate;
        public float hitDelay;
        public eMoveTypes moveType;
    }
    [Serializable]
    public class UnitAttackPattern
    {
        public int pattern_id;
        public int unit_id;
        public int loop_start;
        public int loop_end;
        public int[] atk_patterns = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public UnitAttackPattern() { }

        public UnitAttackPattern(int pattern_id, int unit_id, int loop_start, int loop_end, int[] atk_patterns)
        {
            this.pattern_id = pattern_id;
            this.unit_id = unit_id;
            this.loop_start = loop_start;
            this.loop_end = loop_end;
            this.atk_patterns = atk_patterns;
        }
    }

    [Serializable, Table("e_reduction")]
    public class EReduction
    {
        public int border { get; set; }
        public int threshold_1 { get; set; }
        public int threshold_2 { get; set; }
        public int threshold_3 { get; set; }
        public int threshold_4 { get; set; }
        public int threshold_5 { get; set; }
        public double value_1 { get; set; }
        public double value_2 { get; set; }
        public double value_3 { get; set; }
        public double value_4 { get; set; }
        public double value_5 { get; set; }

        public IEnumerable<(int, int, double)> GetThresholdPair()
        {
            yield return (threshold_1, threshold_2, value_1);
            yield return (threshold_2, threshold_3, value_2);
            yield return (threshold_3, threshold_4, value_3);
            yield return (threshold_4, threshold_5, value_4);
            yield return (threshold_5, 0, value_5);
        }
    }

    [Serializable]
    public class UniqueEquipmentData
    {
        public int equipment_id;
        public BaseData baseValue;
        public BaseData enhanceValue;

        public BaseData CalcUniqueValue(int level)
        {
            if (level > 0)
            {
                if (enhanceValue == null) return baseValue;
                return baseValue + enhanceValue * (level - 1);
            }
            return new BaseData();
        }
    }

    [Serializable]
    public class GuildBattleData
    {
        public AddedPlayerData players;
        public List<EnemyData> enemyData;
        public Dictionary<int, EnemyData> MPartsDataDic;
        public MasterEnemyMParts.EnemyMParts mParts;
        public List<List<float>> UBExecTimeList = null;
        public bool isAutoMode = true;
        public bool forceAutoMode = false;
        //public bool isViolent = false;
        //public int calSpeed;
        //public int FPSforAnimaton;
        //public int FPSforLogic;
        //public bool playAnimation;
        //public int UBTryingCount;
        //public bool useFixedRandomSeed;
       // public int RandomSeed;
        //public Dictionary<int, UnitAttackPattern> changedATKPatternDic = new Dictionary<int, UnitAttackPattern>();
        public GuildSettingData SettingData;
        public bool skipping;
    }

}