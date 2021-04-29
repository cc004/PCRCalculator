// Decompiled with JetBrains decompiler
// Type: Elements.SkillDefine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public static class SkillDefine
  {
    public const int FREE_SKILL_SLOT_NUM = 3;
    public const int ACTION_VALUE4_RARITY_ID = 100;
    public const int UPGRADE_SKILL_NUM = 10;
    private const float NO_DISPLAY = 0.0f;
    private const float NO_VALUE_DISPLAY = -1f;
    public static readonly Dictionary<eActionType, Func<List<float>, List<float>, bool, float>> SkillActionDictionary = new Dictionary<eActionType, Func<List<float>, List<float>, bool, float>>()
    {
      {
        eActionType.ATTACK,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcAttackBaseParam)
      },
      {
        eActionType.MOVE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.KNOCK,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.HEAL,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcHealBaseParam)
      },
      {
        eActionType.CURE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.BARRIER,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParam)
      },
      {
        eActionType.REFLEXIVE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.CHANGE_SPEED,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcSkillLevelParam)
      },
      {
        eActionType.SLIP_DAMAGE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcChargeParam)
      },
      {
        eActionType.BUFF_DEBUFF,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueSecondParam2)
      },
      {
        eActionType.CHARM,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcSkillLevelParam)
      },
      {
        eActionType.BLIND,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcSkillLevelParam)
      },
      {
        eActionType.SILENCE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcSkillLevelParam)
      },
      {
        eActionType.MODE_CHANGE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.SUMMON,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueSecondParam)
      },
      {
        eActionType.CHARGE_ENERGY,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParam)
      },
      {
        eActionType.TRIGER,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.DAMAGE_CHARGE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.CHARGE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParam)
      },
      {
        eActionType.DECOY,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParam)
      },
      {
        eActionType.NO_DAMAGE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParamNoCeil)
      },
      {
        eActionType.CHANGE_PATTERN,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.IF_FOR_CHILDREN,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.REVIVAL,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParam)
      },
      {
        eActionType.CONTINUOUS_ATTACK,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcAttackBaseParam)
      },
      {
        eActionType.GIVE_VALUE_AS_ADDITIVE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.GIVE_VALUE_AS_MULTIPLE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.IF_FOR_ALL,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.SEARCH_AREA_CHANGE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.DESTROY,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.CONTINUOUS_ATTACK_NEARBY,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.ENCHANT_LIFE_STEAL,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.ENCHANT_STRIKE_BACK,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParam)
      },
      {
        eActionType.ACCUMULATIVE_DAMAGE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParam)
      },
      {
        eActionType.SEAL,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.ATTACK_FIELD,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcAttackBaseParam)
      },
      {
        eActionType.HEAL_FIELD,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcHealFieldBaseParam)
      },
      {
        eActionType.CHANGE_PARAMETER_FIELD,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParam2)
      },
      {
        eActionType.ABNORMAL_STATE_FIELD,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.KETSUBAN,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.UB_CHANGE_TIME,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.LOOP_TRIGGER,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.IF_HAS_TARGET,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.WAVE_START_IDLE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.SKILL_EXEC_COUNT,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.RATIO_DAMAGE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.UPPER_LIMIT_ATTACK,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.REGENERATION,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcRegeneParam)
      },
      {
        eActionType.BUFF_DEBUFF_CLEAR,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.LOOP_MOTION_BUFF_DEBUFF,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueSecondParam2)
      },
      {
        eActionType.DIVISION,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.CHANGE_BODY_WIDTH,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.IF_EXISTS_FIELD_FOR_ALL,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.STEALTH,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.COUNT_BLIND,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcSkillLevelParam)
      },
      {
        eActionType.MOVE_PARTS,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.COUNT_DOWN,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.STOP_FIELD,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.INHIBIT_HEAL,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.ATTACK_SEAL,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.FEAR,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcSkillLevelParam)
      },
      {
        eActionType.AWE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.TOAD,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.LOOP_MOTION_REPEAT,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.KNGHT_GUARD,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcKnightGuardParam)
      },
      {
        eActionType.DAMAGE_CUT,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParamRound)
      },
      {
        eActionType.LOG_BARRIER,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueFirstBaseParamRound)
      },
      {
        eActionType.GIVE_VALUE_AS_DIVIDE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.ACTION_BY_HIT_COUNT,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.IGNORE_DECOY,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.getNoDisplay)
      },
      {
        eActionType.PASSIVE,
        new Func<List<float>, List<float>, bool, float>(SkillDefine.calcValueSecondParam)
      }
    };
        public static readonly Dictionary<eActionType, System.Type> SkillActionTypeDictionary = new Dictionary<eActionType, System.Type>()
    {
      {
        eActionType.ATTACK,
        typeof (AttackAction)
      },
      {
        eActionType.MOVE,
        typeof (MoveAction)
      },
      {
        eActionType.KNOCK,
        typeof (KnockAction)
      },
      {
        eActionType.HEAL,
        typeof (HealAction)
      },
      {
        eActionType.CURE,
        typeof (CureAction)
      },
      {
        eActionType.BARRIER,
        typeof (BarrierAction)
      },
      {
        eActionType.REFLEXIVE,
        typeof (ActionParameter)
      },
      {
        eActionType.CHANGE_SPEED,
        typeof (ChangeSpeedAction)
      },
      {
        eActionType.SLIP_DAMAGE,
        typeof (SlipDamageAction)
      },
      {
        eActionType.BUFF_DEBUFF,
        typeof (BuffDebuffAction)
      },
      {
        eActionType.CHARM,
        typeof (CharmAction)
      },
      {
        eActionType.BLIND,
        typeof (BlindAction)
      },
      {
        eActionType.SILENCE,
        typeof (SilenceAction)
      },
      {
        eActionType.MODE_CHANGE,
        typeof (ModeChangeAction)
      },
      {
        eActionType.SUMMON,
        typeof (SummonAction)
      },
      {
        eActionType.CHARGE_ENERGY,
        typeof (ChargeEnergyAction)
      },
      {
        eActionType.TRIGER,
        typeof (TriggerAction)
      },
      {
        eActionType.DAMAGE_CHARGE,
        typeof (DamageChargeAction)
      },
      {
        eActionType.CHARGE,
        typeof (ChargeAction)
      },
      {
        eActionType.DECOY,
        typeof (DecoyAction)
      },
      {
        eActionType.NO_DAMAGE,
        typeof (NoDamageAction)
      },
      {
        eActionType.CHANGE_PATTERN,
        typeof (ChangePatternAction)
      },
      {
        eActionType.IF_FOR_CHILDREN,
        typeof (IfForChildlenAction)
      },
      {
        eActionType.REVIVAL,
        typeof (RevivalAction)
      },
      {
        eActionType.CONTINUOUS_ATTACK,
        typeof (ContinuousAttackAction)
      },
      {
        eActionType.GIVE_VALUE_AS_ADDITIVE,
        typeof (GiveValueAdditiveAction)
      },
      {
        eActionType.GIVE_VALUE_AS_MULTIPLE,
        typeof (GiveValueMultipleAction)
      },
      {
        eActionType.IF_FOR_ALL,
        typeof (IfForAllAction)
      },
      {
        eActionType.SEARCH_AREA_CHANGE,
        typeof (SearchAreaChangeAction)
      },
      {
        eActionType.DESTROY,
        typeof (DestroyAction)
      },
      {
        eActionType.CONTINUOUS_ATTACK_NEARBY,
        typeof (ContinuousAttackNearByAction)
      },
      {
        eActionType.ENCHANT_LIFE_STEAL,
        typeof (EnchantLifeStealAction)
      },
      {
        eActionType.ENCHANT_STRIKE_BACK,
        typeof (EnchantStrikeBackAction)
      },
      {
        eActionType.ACCUMULATIVE_DAMAGE,
        typeof (AccumulativeDamageAction)
      },
      {
        eActionType.SEAL,
        typeof (SealAction)
      },
      {
        eActionType.ATTACK_FIELD,
        typeof (AttackFieldAction)
      },
      {
        eActionType.ABNORMAL_STATE_FIELD,
        typeof (AbnormalStateFieldAction)
      },
      {
        eActionType.HEAL_FIELD,
        typeof (HealFieldAction)
      },
      {
        eActionType.CHANGE_PARAMETER_FIELD,
        typeof (ChangeParameterFieldAction)
      },
      {
        eActionType.UB_CHANGE_TIME,
        typeof (UbChangeSpeedAction)
      },
      {
        eActionType.LOOP_TRIGGER,
        typeof (LoopTriggerAction)
      },
      {
        eActionType.IF_HAS_TARGET,
        typeof (IfHasTargetAction)
      },
      {
        eActionType.WAVE_START_IDLE,
        typeof (WaveStartIdleAction)
      },
      {
        eActionType.SKILL_EXEC_COUNT,
        typeof (SkillExecCountAction)
      },
      {
        eActionType.RATIO_DAMAGE,
        typeof (RatioDamageAction)
      },
      {
        eActionType.UPPER_LIMIT_ATTACK,
        typeof (UpperLimitAttackAction)
      },
      {
        eActionType.REGENERATION,
        typeof (RegenerationAction)
      },
      {
        eActionType.BUFF_DEBUFF_CLEAR,
        typeof (BuffDebuffClearAction)
      },
      {
        eActionType.LOOP_MOTION_BUFF_DEBUFF,
        typeof (LoopMotionBuffDebuffAction)
      },
      {
        eActionType.DIVISION,
        typeof (DivisionAction)
      },
      {
        eActionType.IF_EXISTS_FIELD_FOR_ALL,
        typeof (IfExistsFieldForAllAction)
      },
      {
        eActionType.STEALTH,
        typeof (StealthAction)
      },
      {
        eActionType.COUNT_DOWN,
        typeof (CountDownAction)
      },
      {
        eActionType.MOVE_PARTS,
        typeof (MovePartsAction)
      },
      {
        eActionType.COUNT_BLIND,
        typeof (CountBlindAction)
      },
      {
        eActionType.CHANGE_BODY_WIDTH,
        typeof (ChangeBodyWidthAction)
      },
      {
        eActionType.STOP_FIELD,
        typeof (StopFieldAction)
      },
      {
        eActionType.INHIBIT_HEAL,
        typeof (InhibitHealAction)
      },
      {
        eActionType.ATTACK_SEAL,
        typeof (AttackSealAction)
      },
      {
        eActionType.FEAR,
        typeof (FearAction)
      },
      {
        eActionType.AWE,
        typeof (AweAction)
      },
      {
        eActionType.TOAD,
        typeof (ToadAction)
      },
      {
        eActionType.LOOP_MOTION_REPEAT,
        typeof (LoopMotionRepeatAction)
      },
      {
        eActionType.FORCE_HP_CHANGE,
        typeof (ForceHpChangeAction)
      },
      {
        eActionType.KNGHT_GUARD,
        typeof (KnightGuardAction)
      },
      {
        eActionType.DAMAGE_CUT,
        typeof (DamageCutAction)
      },
      {
        eActionType.LOG_BARRIER,
        typeof (LogBarrierAction)
      },
      {
        eActionType.GIVE_VALUE_AS_DIVIDE,
        typeof (GiveValueDivideAction)
      },
      {
        eActionType.ACTION_BY_HIT_COUNT,
        typeof (ActionByHitCountAction)
      },
      {
        eActionType.HEAL_DOWN,
        typeof (HealDownAction)
      },
      {
        eActionType.PASSIVE_SEAL,
        typeof (PassiveSealAction)
      },
      {
        eActionType.PASSIVE_DAMAGE_UP,
        typeof (PassiveDamageUpAction)
      },
      {
        eActionType.PASSIVE,
        typeof (ActionParameter)
      },
      {
        eActionType.CHANGE_ENERGY_RECOVERY_RATIO_BY_DAMAGE,
        typeof (ChangeEnergyRecoveryRatioByDamage)
      },
      {
        eActionType.IGNORE_DECOY,
        typeof (IgnoreDecoyAction)
      }
    };
        /*public static readonly Dictionary<int, eUbResponceVoiceType> UbResponceVoiceDictionary = new Dictionary<int, eUbResponceVoiceType>()
        {
          {
            1001,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2001,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1002,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2002,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1003,
            eUbResponceVoiceType.THANKS
          },
          {
            2003,
            eUbResponceVoiceType.THANKS
          },
          {
            1004,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2004,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1005,
            eUbResponceVoiceType.THANKS
          },
          {
            2005,
            eUbResponceVoiceType.THANKS
          },
          {
            1006,
            eUbResponceVoiceType.THANKS
          },
          {
            2006,
            eUbResponceVoiceType.THANKS
          },
          {
            1007,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2007,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1008,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2008,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1009,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2009,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1010,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2010,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1011,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2011,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1012,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2012,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1013,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2013,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1014,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2014,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1015,
            eUbResponceVoiceType.APPLOUD
          },
          {
            2015,
            eUbResponceVoiceType.APPLOUD
          },
          {
            1016,
            eUbResponceVoiceType.THANKS
          },
          {
            2016,
            eUbResponceVoiceType.THANKS
          },
          {
            1017,
            eUbResponceVoiceType.NONE
          },
          {
            2017,
            eUbResponceVoiceType.NONE
          }
        };*/

        private static float getNoDisplay(
      List<float> _parameters,
      List<float> _subParameters,
      bool _isLevelUp) => 0.0f;

    private static float calcSkillLevelParam(
      List<float> _parameters,
      List<float> _subParameters,
      bool _isLevelUp) => !_isLevelUp ? -1f : _parameters[0];

    private static float calcAttackBaseParam(
      List<float> _parameters,
      List<float> _subParameters,
      bool _isLevelUp)
    {
      float parameter1 = _parameters[0];
      double parameter2 = (double) _parameters[1];
      float parameter3 = _parameters[2];
      float parameter4 = _parameters[3];
      float parameter5 = _parameters[4];
      float subParameter1 = _subParameters[0];
      float subParameter2 = _subParameters[1];
      float subParameter3 = _subParameters[2];
      double num = (double) parameter3 * (double) parameter1;
      return Mathf.Floor((float) (parameter2 + num + ((double) subParameter1 == 1.0 || (double) subParameter1 == 3.0 ? (double) subParameter2 : (double) subParameter3) * ((double) parameter4 + (double) parameter5 * (double) parameter1)));
    }

    private static float calcHealBaseParam(
      List<float> _parameters,
      List<float> _subParameters,
      bool _isLevelUp)
    {
      float parameter1 = _parameters[0];
      double parameter2 = (double) _parameters[1];
      double parameter3 = (double) _parameters[2];
      float parameter4 = _parameters[3];
      float parameter5 = _parameters[4];
      float parameter6 = _parameters[5];
      float subParameter1 = _subParameters[0];
      float subParameter2 = _subParameters[1];
      float subParameter3 = _subParameters[2];
      double num = (double) parameter4 * (double) parameter1;
      return Mathf.Floor((float) (parameter3 + num + ((double) subParameter1 == 1.0 ? (double) subParameter2 : (double) subParameter3) * ((double) parameter5 + (double) parameter6 * (double) parameter1)));
    }

    private static float calcKnightGuardParam(
      List<float> _parameters,
      List<float> _subParameters,
      bool _isLevelUp)
    {
      float parameter1 = _parameters[0];
      double parameter2 = (double) _parameters[1];
      float parameter3 = _parameters[2];
      float parameter4 = _parameters[3];
      float parameter5 = _parameters[4];
      float parameter6 = _parameters[5];
      double subParameter1 = (double) _subParameters[0];
      float subParameter2 = _subParameters[1];
      float subParameter3 = _subParameters[2];
      float num = (int) parameter2 == 1 ? subParameter2 : subParameter3;
      return (float) (int) ((double) parameter3 + (double) parameter4 * (double) parameter1 + ((double) parameter5 + (double) parameter6 * (double) parameter1) * (double) num);
    }

    private static float calcHealFieldBaseParam(
      List<float> _parameters,
      List<float> _subParameters,
      bool _levelUp)
    {
      float parameter1 = _parameters[0];
      double parameter2 = (double) _parameters[1];
      float parameter3 = _parameters[2];
      float parameter4 = _parameters[3];
      float parameter5 = _parameters[4];
      double parameter6 = (double) _parameters[5];
      float subParameter1 = _subParameters[0];
      float subParameter2 = _subParameters[1];
      float subParameter3 = _subParameters[2];
      bool flag = (double) subParameter1 == 2.0 || (double) subParameter1 == 4.0;
      double num = (double) parameter3 * (double) parameter1;
      return Mathf.Floor((float) (parameter2 + num + (flag ? (double) subParameter3 : (double) subParameter2) * ((double) parameter4 + (double) parameter5 * (double) parameter1)));
    }

    private static float calcValueFirstBaseParam(
      List<float> _parameters,
      List<float> _subParameters,
      bool _levelUp)
    {
      float parameter = _parameters[0];
      return Mathf.Ceil(_parameters[1] + _parameters[2] * parameter);
    }

    private static float calcValueFirstBaseParam2(
      List<float> _parameters,
      List<float> _subParameters,
      bool _levelUp)
    {
      float parameter = _parameters[0];
      return (float) BattleUtil.FloatToIntReverseTruncate(_parameters[1] + _parameters[2] * parameter);
    }

    private static float calcValueFirstBaseParamNoCeil(
      List<float> _parameters,
      List<float> _subParameters,
      bool _levelUp)
    {
      float parameter = _parameters[0];
      return _parameters[1] + _parameters[2] * parameter;
    }

    private static float calcValueFirstBaseParamRound(
      List<float> _parameters,
      List<float> _subParameters,
      bool _levelUp)
    {
      float parameter = _parameters[0];
      return (float) Mathf.RoundToInt(_parameters[1] + _parameters[2] * parameter);
    }

    private static float calcValueSecondParam(
      List<float> _parameters,
      List<float> _subParameters,
      bool _isSkillLevelUp)
    {
      float parameter = _parameters[0];
      return Mathf.Ceil(_parameters[2] + _parameters[3] * parameter);
    }

    private static float calcValueSecondParam2(
      List<float> _parameters,
      List<float> _subParameters,
      bool _isSkillLevelUp)
    {
      float parameter = _parameters[0];
      return (float) BattleUtil.FloatToIntReverseTruncate(_parameters[2] + _parameters[3] * parameter);
    }

    private static float calcChargeParam(
      List<float> _parameters,
      List<float> _subParameters,
      bool _isLevelUp)
    {
      float parameter = _parameters[0];
      return Mathf.Floor(_parameters[1] + _parameters[2] * parameter);
    }

    private static float calcRegeneParam(
      List<float> _parameters,
      List<float> _subParameters,
      bool _isLevelUp)
    {
      float parameter1 = _parameters[0];
      float parameter2 = _parameters[1];
      float parameter3 = _parameters[2];
      float parameter4 = _parameters[3];
      float parameter5 = _parameters[4];
      float subParameter1 = _subParameters[0];
      float subParameter2 = _subParameters[1];
      float subParameter3 = _subParameters[2];
      return (double) _subParameters[3] != 1.0 ? 0.0f : Mathf.Floor((float) ((double) parameter2 + (double) parameter3 * (double) parameter1 + ((double) parameter4 + (double) parameter5 * (double) parameter1) * ((double) subParameter1 == 1.0 ? (double) subParameter2 : (double) subParameter3)));
    }

    public static bool IsEvolutionSkill(int _skillId) => _skillId / 10 % 10 == 1;

    public enum eBarrierType
    {
      PHYSIC_INVALID = 1,
      MAGIC_INVALID = 2,
      PHYSIC_ABSORB = 3,
      MAGIC_ABSORB = 4,
      PHYSIC_AND_MAGIC_INVALID = 5,
      PHYSIC_AND_MAGIC_ABSORB = 6,
    }
  }
}
