// Decompiled with JetBrains decompiler
// Type: Elements.BattleUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

//using Elements.Data;

using System.Collections.Generic;
using PCRCaculator.SQL;
using UnityEngine;

namespace Elements
{
  public class BattleUtil
  {
    public const long MaxBattleParameter = 2_000_000_000L;

    public static double ApplyRecoveryAttenuation(RecoveryAttenuation[] tiers, double previous, double amount)
    {
      if (tiers.Length == 0)
        return amount;

      // The -1 row gives the rate beyond the last finite threshold.
      double total = 0.0;
      for (int i = 1; i < tiers.Length; i++)
      {
        var tier = tiers[i];
        if (tier.limit_value <= previous)
          continue;
        if (previous + amount < tier.limit_value)
          return total + amount * tier.correction_value;

        double portion = tier.limit_value - previous;
        total += portion * tier.correction_value;
        amount -= portion;
        previous = tier.limit_value;
      }
      return total + amount * tiers[0].correction_value;
    }

    public static LongWithEx GetEffectiveParameter(LongWithEx baseValue, LongWithEx additionalBuff) =>
        (baseValue.Max(0) + additionalBuff).Min(MaxBattleParameter);

    public static float GetCriticalRate(float critical, int level, int targetLevel) =>
        critical * 0.05f * ((float)level / targetLevel) * 0.01f;

    public static float GetDodgeByLevelDiff(int level, BasePartsData _target)
    {
      float ret = 1.0f;
      if (!_target.Owner.IsBoss)
      {
        ret = 1f - Mathf.Max(_target.GetLevel() - level, 0f) / 100f;
      }
      return ret;
    }

    public static float CalcDodgeByLevelDiffUseParam(int _level, UnitCtrl _source, BasePartsData _target, bool _isExEquipSkillAction, float _coefficient = 1)
    {
      var owner = _target.Owner;

      if (owner.IsBoss)
          return 1f;

      float baseDodge = Mathf.Min(GetDodgeByLevelDiff(_level, _target) * _coefficient, 1f);

      long targetResist = owner.AbnormalStateDodgeResistZero;
      long sourceHit = _source.AbnormalStateDodgeHitZero;

      if (!_source.IsBonusEnemy && !_source.IsBoss && !_isExEquipSkillAction)
          return baseDodge / (1f + targetResist / 10000f) * (1f + sourceHit / 10000f);

      return baseDodge;
    }


    /*public static bool IsCheckSupportChara(eBattleCategory _battleCategory)
    {
      switch (_battleCategory)
      {
        case eBattleCategory.QUEST:
        case eBattleCategory.TRAINING:
        case eBattleCategory.DUNGEON:
        case eBattleCategory.CLAN_BATTLE:
        case eBattleCategory.HATSUNE_BATTLE:
        case eBattleCategory.TOWER:
        case eBattleCategory.TOWER_EX:
        case eBattleCategory.TOWER_REHEARSAL:
        case eBattleCategory.GLOBAL_RAID:
        case eBattleCategory.TOWER_CLOISTER:
        case eBattleCategory.KAISER_BATTLE_MAIN:
        case eBattleCategory.KAISER_BATTLE_SUB:
        case eBattleCategory.SPACE_BATTLE:
          return true;
        default:
          return false;
      }
    }

    public static bool IsQuestSupportUnit(eBattleCategory _battleCategory, int _unitId)
    {
      EHPLBCOOOPK instance = Singleton<EHPLBCOOOPK>.Instance;
      switch (_battleCategory)
      {
        case eBattleCategory.QUEST:
        case eBattleCategory.TRAINING:
        case eBattleCategory.HATSUNE_BATTLE:
          if (instance.LBHMKBLOJOB != null && instance.LBHMKBLOJOB.ViewerId != 0 && instance.LBHMKBLOJOB.UnitId != 0)
            return _unitId == instance.LBHMKBLOJOB.UnitId;
          break;
      }
      return false;
    }*/

    public static bool Approximately(float _a, float _b) => Mathf.Abs(_b - _a) < Mathf.Max(1E-05f * Mathf.Max(Mathf.Abs(_a), Mathf.Abs(_b)), Mathf.Epsilon * 8f);

    public static bool Approximately(double _a, double _b) => System.Math.Abs(_b - _a) < System.Math.Max(1E-14 * System.Math.Max(System.Math.Abs(_a), System.Math.Abs(_b)), 2.220446049250313E-16);

    public static bool LessThanOrApproximately(float _lhs, float _rhs) => _lhs < _rhs || Approximately(_lhs, _rhs);

    public static bool LessThanOrApproximately(double _lhs, double _rhs) => _lhs < _rhs || Approximately(_lhs, _rhs);
    public static int FloatToInt(float _num)
    {
      if (_num >= 0.0)
      {
        int num = Mathf.CeilToInt(_num);
        return Approximately(_num, num) ? num : (int)_num;
      }
      int num1 = Mathf.FloorToInt(_num);
      return Approximately(_num, num1) ? num1 : (int)_num;
    }

    public static int DoubleToInt(double _num)
    {
      if (_num >= 0.0)
      {
        int num = (int)System.Math.Ceiling(_num);
        return Approximately(_num, num) ? num : (int)_num;
      }
      int num1 = (int)System.Math.Floor(_num);
      return Approximately(_num, num1) ? num1 : (int)_num;
    }

    public static long DoubleToLong(double _num)
    {
      if (_num >= 0.0)
      {
        long num = (long)System.Math.Ceiling(_num);
        return Approximately(_num, num) ? num : (long)_num;
      }
      long num1 = (long)System.Math.Floor(_num);
      return Approximately(_num, num1) ? num1 : (long)_num;
    }

    public static LongWithEx DoubleToLong(DoubleWithEx _num)
    {
      return _num.ToLongWithEx();
    }

    public static int FloatToIntReverseTruncate(float _num)
    {
      int num = (int)_num;
      if (Approximately(num, _num))
        return num;
      return _num <= 0.0 ? Mathf.FloorToInt(_num) : Mathf.CeilToInt(_num);
    }
    public static int DoubleToIntReverseTruncate(double _num)
    {
      int num = (int)_num;
      if (Approximately(num, _num))
        return num;
      return DoubleToInt(_num <= 0.0 ? System.Math.Floor(_num) : System.Math.Ceiling(_num));
    }
    public static long DoubleToLongReverseTruncate(double _num)
    {
      long num = (long)_num;
      if (Approximately(num, _num))
        return num;
      return DoubleToLong(_num <= 0.0 ? System.Math.Floor(_num) : System.Math.Ceiling(_num));
    }

    public static LongWithEx DoubleToLongReverseTruncate(DoubleWithEx _num)
    {
      long num = (long)_num;
      if (Approximately(num, _num))
        return num;
      return _num <= 0.0 ? (LongWithEx)_num.Floor() : (LongWithEx)_num.Ceil();
    }

    public static float FloatToPerMille(float _value)
    {
      return (float)FloatToInt(_value * 1000f) / 1000f;
    }

    public static double DoubleToPerMille(double _value)
    {
      return DoubleToLong(_value * 1000.0) / 1000.0;
    }
    public static FloatWithEx FloatToIntReverseTruncate(FloatWithEx _num)
    {
      return _num.Select(x => FloatToIntReverseTruncate(x), "ceil");
    }
    public static FloatWithEx FloatToInt(FloatWithEx _num)
    {
      return _num.Select(x => FloatToInt(x), "floor");
    }
    public static eUnitRespawnPos SearchRespawnPos(
      eUnitRespawnPos _basePos,
      List<UnitCtrl> _unitList)
    {
      if (_unitList.FindIndex(e => e.RespawnPos == _basePos) == -1)
        return _basePos;
      int basePos = (int)_basePos;
      int index1 = 0;
      bool flag1 = _basePos == eUnitRespawnPos.MAIN_POS_1;
      int index2 = 0;
      bool flag2 = _basePos == eUnitRespawnPos.SUB_POS_5;
      for (index1 = 1; index1 <= basePos && _unitList.FindIndex(e => e.RespawnPos == (eUnitRespawnPos)(basePos - index1)) != -1; index1++)
      {
        if (index1 == basePos)
          flag1 = true;
      }
      for (index2 = 1; index2 <= 9 - basePos && _unitList.FindIndex(e => e.RespawnPos == (eUnitRespawnPos)(basePos + index2)) != -1; index2++)
      {
        if (index2 == 9 - basePos)
          flag2 = true;
      }
      if (!flag2 && !flag1)
        return index2 < index1 ? (eUnitRespawnPos)(basePos + index2) : (eUnitRespawnPos)(basePos - index1);
      if (flag2 && !flag1)
        return (eUnitRespawnPos)(basePos - index1);
      return flag1 && !flag2 ? (eUnitRespawnPos)(basePos + index2) : _basePos;
    }

    public static void SortPosition(
      List<UnitCtrl> _unitList,
      float[] _searchAreaList,
      int[] _posList,
      bool _isWithId)
    {
      int num1 = _unitList.Count - 1;
      for (int index1 = 0; index1 < num1; ++index1)
      {
        for (int index2 = num1; index2 > index1; --index2)
        {
          float searchArea1 = _searchAreaList[index2 - 1];
          float searchArea2 = _searchAreaList[index2];
          int num2 = searchArea1 > (double)searchArea2 ? 1 : 0;
          bool flag = false;
          if (_isWithId)
            flag = searchArea1 == (double)searchArea2 && _unitList[_posList[index2 - 1]].UnitId > _unitList[_posList[index2]].UnitId;
          int num3 = flag ? 1 : 0;
          if ((num2 | num3) != 0)
          {
            int pos = _posList[index2];
            _posList[index2] = _posList[index2 - 1];
            _posList[index2 - 1] = pos;
            float searchArea3 = _searchAreaList[index2];
            _searchAreaList[index2] = _searchAreaList[index2 - 1];
            _searchAreaList[index2 - 1] = searchArea3;
          }
        }
      }
    }

    /*public static void CreateVeryHardBackgroundEffect()
    {
      if (!QuestDefine.IsVeryHardQuest(Singleton<EHPLBCOOOPK>.Instance.OIPDBCEJAPK))
        return;
      ResourceManager instance = ManagerSingleton<ResourceManager>.Instance;
      instance.AddLoadBundleId(eBundleId.BATTLE_VERY_HARD_FX_BG);
      instance.AddLoadResourceId(eResourceId.FX_BATTLE_VERY_HARD_BG);
      instance.AddInstantiateGameObject(eResourceId.FX_BATTLE_VERY_HARD_BG, ExceptNGUIRoot.Transform);
    }*/
  }
}
