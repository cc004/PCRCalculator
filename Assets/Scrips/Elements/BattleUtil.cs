// Decompiled with JetBrains decompiler
// Type: Elements.BattleUtil
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

//using Elements.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class BattleUtil
  {
    public static float GetCriticalRate(float critical, int level, int targetLevel) => (float) ((double) critical * 0.0500000007450581 * ((double) level / (double) targetLevel) * 0.00999999977648258);

    public static float GetDodgeByLevelDiff(int level, int targetLevel) => (float) (1.0 - (double) Mathf.Max((float) (targetLevel - level), 0.0f) / 100.0);

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

    public static bool Approximately(float _a, float _b) => (double) Mathf.Abs(_b - _a) < (double) Mathf.Max(1E-05f * Mathf.Max(Mathf.Abs(_a), Mathf.Abs(_b)), Mathf.Epsilon * 8f);

    public static int FloatToInt(float _num)
    {
      if ((double) _num >= 0.0)
      {
        int num = Mathf.CeilToInt(_num);
        return BattleUtil.Approximately(_num, (float) num) ? num : (int) _num;
      }
      int num1 = Mathf.FloorToInt(_num);
      return BattleUtil.Approximately(_num, (float) num1) ? num1 : (int) _num;
    }

    public static int FloatToIntReverseTruncate(float _num)
    {
      int num = (int) _num;
      if (BattleUtil.Approximately((float) num, _num))
        return num;
      return (double) _num <= 0.0 ? Mathf.FloorToInt(_num) : Mathf.CeilToInt(_num);
    }

    public static FloatWithEx FloatToIntReverseTruncate(FloatWithEx _num)
    {
        return new FloatWithEx { ex = FloatToIntReverseTruncate(_num.ex), value = FloatToIntReverseTruncate(_num.value) };
    }
    public static FloatWithEx FloatToInt(FloatWithEx _num)
    {
        return new FloatWithEx{ ex = FloatToInt(_num.ex), value = FloatToInt(_num.value) };
    }
    public static eUnitRespawnPos SearchRespawnPos(
      eUnitRespawnPos _basePos,
      List<UnitCtrl> _unitList)
    {
      if (_unitList.FindIndex((Predicate<UnitCtrl>) (e => e.RespawnPos == _basePos)) == -1)
        return _basePos;
      int basePos = (int) _basePos;
      int index1 = 0;
      bool flag1 = _basePos == eUnitRespawnPos.MAIN_POS_1;
      int index2 = 0;
      bool flag2 = _basePos == eUnitRespawnPos.SUB_POS_5;
      for (index1 = 1; index1 <= basePos && _unitList.FindIndex((Predicate<UnitCtrl>) (e => e.RespawnPos == (eUnitRespawnPos) (basePos - index1))) != -1; index1++)
      {
        if (index1 == basePos)
          flag1 = true;
      }
      for (index2 = 1; index2 <= 9 - basePos && _unitList.FindIndex((Predicate<UnitCtrl>) (e => e.RespawnPos == (eUnitRespawnPos) (basePos + index2))) != -1; index2++)
      {
        if (index2 == 9 - basePos)
          flag2 = true;
      }
      if (!flag2 && !flag1)
        return index2 < index1 ? (eUnitRespawnPos) (basePos + index2) : (eUnitRespawnPos) (basePos - index1);
      if (flag2 && !flag1)
        return (eUnitRespawnPos) (basePos - index1);
      return flag1 && !flag2 ? (eUnitRespawnPos) (basePos + index2) : _basePos;
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
          int num2 = (double) searchArea1 > (double) searchArea2 ? 1 : 0;
          bool flag = false;
          if (_isWithId)
            flag = (double) searchArea1 == (double) searchArea2 && _unitList[_posList[index2 - 1]].UnitId > _unitList[_posList[index2]].UnitId;
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
