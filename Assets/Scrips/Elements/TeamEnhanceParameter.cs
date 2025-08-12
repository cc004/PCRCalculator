using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Elements
{

    public enum eRoleParamType // TypeDefIndex: 3107
    {
        // Fields
        NONE = 0,
        HP = 1,
        ATTACK = 2,
        DEF = 3,
        MAGIC_DEF = 4,
        CRITICAL = 5,
        CRITICAL_DAMAGE_RATE = 6,
        DAMAGE_UP_PERCENT = 7,
        ABNORMAL_STATE_DODGE_HIT = 8,
        ABNORMAL_STATE_DODGE_RESIST = 9,
        ATTACK_ENHANCE = 10,
        BUFF_ENHANCE = 11,
        DEBUFF_ENHANCE = 12,
        HEAL_ENHANCE = 13,
        ENERGY_CHARGE = 14,
    }

    public class TeamEnhanceParameter
    {
        public Dictionary<int, int> talentLevelParameter = new Dictionary<int, int>();
        private Dictionary<int, Dictionary<eParamType, int>> talentBuffParameter = new Dictionary<int, Dictionary<eParamType, int>>();
        private Dictionary<int, Dictionary<eRoleParamType, int>> roleBuffParameter = new Dictionary<int, Dictionary<eRoleParamType, int>>();

        public Dictionary<eParamType, int> GetTalentEnhance(int talentId)
        {
            if (talentBuffParameter == null || !talentBuffParameter.ContainsKey(talentId))
                return null;

            return talentBuffParameter[talentId];
        }

        public int GetAdditiveAtkForTalentLevel(int talentId)
        {
            if (talentLevelParameter == null || !talentLevelParameter.ContainsKey(talentId))
                return 0;

            return talentLevelParameter[talentId];
        }

        public Dictionary<eParamType, int> GetRoleEnhance(int roleId, UnitCtrl owner)
        {
            if (this == null)
                return null;

            var result = new Dictionary<eParamType, int>();

            if (roleBuffParameter == null || !roleBuffParameter.TryGetValue(roleId, out var roleParams))
                return result;

            bool isPhysical = owner.AtkType != 2;

            foreach (var kvp in roleParams)
            {
                var roleParamType = kvp.Key;
                var value = kvp.Value;

                var paramType = GetParamType(roleParamType, isPhysical);

                result[paramType] = value;
            }

            return result;
        }

        private eParamType GetParamType(eRoleParamType roleParamType, bool isAtk)
        {
            switch (roleParamType)
            {
                case eRoleParamType.HP:
                    return eParamType.HP;
                    
                case eRoleParamType.ATTACK:
                    return isAtk ? eParamType.ATK : eParamType.MAGIC_ATK;
                    
                case eRoleParamType.DEF:
                    return eParamType.DEF;
                    
                case eRoleParamType.MAGIC_DEF:
                    return eParamType.MAGIC_DEF;
                    
                case eRoleParamType.CRITICAL:
                    return isAtk ? eParamType.PHYSICAL_CRITICAL : eParamType.MAGIC_CRITICAL;
                    
                case eRoleParamType.CRITICAL_DAMAGE_RATE:
                    return isAtk ? eParamType.PHYSICAL_CRITICAL_DAMAGE_RATE : eParamType.MAGIC_CRITICAL_DAMAGE_RATE;
                    
                case eRoleParamType.DAMAGE_UP_PERCENT:
                    return isAtk ? eParamType.PHYSICAL_DAMAGE_UP_PERCENT : eParamType.MAGIC_DAMAGE_UP_PERCENT;
                    
                case eRoleParamType.ABNORMAL_STATE_DODGE_HIT:
                    return eParamType.ABNORMAL_STATE_DODGE_HIT;
                    
                case eRoleParamType.ABNORMAL_STATE_DODGE_RESIST:
                    return eParamType.ABNORMAL_STATE_DODGE_RESIST;
                    
                case eRoleParamType.ATTACK_ENHANCE:
                    return eParamType.ATTACK_ENHANCE;
                    
                case eRoleParamType.BUFF_ENHANCE:
                    return eParamType.BUFF_ENHANCE;
                    
                case eRoleParamType.DEBUFF_ENHANCE:
                    return eParamType.DEBUFF_ENHANCE;
                    
                case eRoleParamType.HEAL_ENHANCE:
                    return eParamType.HEAL_ENHANCE;
                    
                case eRoleParamType.ENERGY_CHARGE:
                    return eParamType.ENERGY_CHARGE;
                    
                default:
                    return eParamType.NONE;
            }
        }

    }

}