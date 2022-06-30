// Decompiled with JetBrains decompiler
// Type: Elements.Battle.FHDHEIDIPOI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements.Battle
{
  public class BattleSpeedManager :  BattleSpeedInterface
  {
    private BattleManager_Time battleManager;
    private float baseTimeScale = 1f;
    private Dictionary<UnitCtrl, bool> slowStopFlagDic = new Dictionary<UnitCtrl, bool>();
    private bool speedupFlag;
    private float speedUpRate = 2f;
    private bool isSpeedQuadruple;

    public float BaseTimeScale => baseTimeScale;

    public bool SpeedUpFlag
    {
      set
      {
        speedupFlag = value;
        //SoundManager instance = ManagerSingleton<SoundManager>.Instance;
        if (speedupFlag)
        {
          Time.timeScale = speedUpRate * BaseTimeScale;
          //instance.SetSoundSpeed(this.speedUpRate * this.BaseTimeScale);
        }
        else
        {
          Time.timeScale = BaseTimeScale;
          //instance.SetSoundSpeed(this.BaseTimeScale);
        }
      }
      get => speedupFlag;
    }

    public float SpeedUpRate
        {
            get => speedUpRate;
            set
            {
                //if (value >= 0.124f && value<=20.1f)
                //{
                    speedUpRate = value;
                //}
                //else
                //{
                //    speedUpRate = 1;
                //}
            }
        }

    public bool IsSpeedQuadruple
    {
      get => isSpeedQuadruple;
      set
      {
        isSpeedQuadruple = value;
        if (isSpeedQuadruple)
          speedUpRate = 4f;
        else
          speedUpRate = 2f;
      }
    }

    public BattleSpeedManager(BattleManager_Time KHPJAFEGFBO) => battleManager = KHPJAFEGFBO;

    public void SetBaseTimeScale(float EGAMDNELEIL) => baseTimeScale = EGAMDNELEIL;

    //[Conditional("UNITY_STANDALONE_LINUX")]
    //[Conditional("CYG_DEBUG")]
    public void SetSpeedUpRate(float JKKFBGMONMB) => speedUpRate = JKKFBGMONMB;

    public void StartSlowEffect(
      BattleDefine.SlowEffect SlowEffect,
      UnitCtrl Owner,
      float Time,
      bool ignoreFPS)
    {
      if (ignoreFPS)
        battleManager.StartCoroutineIgnoreFps(updateSlowEffect(SlowEffect, Owner, Time));
      else
        battleManager.AppendCoroutine(updateSlowEffect(SlowEffect, Owner, Time), ePauseType.VISUAL, Owner);
    }

    public void StopSlowEffect()
    {
      List<UnitCtrl> unitCtrlList = new List<UnitCtrl>(slowStopFlagDic.Keys);
      for (int index = 0; index < unitCtrlList.Count; ++index)
        slowStopFlagDic[unitCtrlList[index]] = true;
      baseTimeScale = 1f;
      Time.timeScale = (speedupFlag ? speedUpRate : 1f) * BaseTimeScale;
    }

    private IEnumerator updateSlowEffect(
      BattleDefine.SlowEffect SlowEffect,
      UnitCtrl Source,
      float Time)
    {
      float time = Time;
      while (true)
      {
        time += battleManager.DeltaTime_60fps;
        if (time <= (double) SlowEffect.StartDelay)
          yield return null;
        else
          break;
      }
      List<UnitCtrl> unitCtrlList = new List<UnitCtrl>(slowStopFlagDic.Keys);
      for (int index = 0; index < unitCtrlList.Count; ++index)
        slowStopFlagDic[unitCtrlList[index]] = true;
      if (slowStopFlagDic.ContainsKey(Source))
        slowStopFlagDic[Source] = false;
      else
        slowStopFlagDic.Add(Source, false);
      SlowEffect.StartEasing = new CustomEasing(CustomEasing.eType.outQuad, BaseTimeScale, SlowEffect.To, SlowEffect.StartDuration);
      SlowEffect.EndEasing = new CustomEasing(CustomEasing.eType.outQuad, SlowEffect.To, 1f, SlowEffect.EndDuration);
      time = 0.0f;
      while (!slowStopFlagDic[Source])
      {
        time += battleManager.DeltaTime_60fps;
        baseTimeScale = SlowEffect.StartEasing.GetCurVal(battleManager.DeltaTime_60fps);
                UnityEngine.Time.timeScale = (speedupFlag ? speedUpRate : 1f) * BaseTimeScale;
        if (time <= (double) SlowEffect.StartDuration)
        {
          yield return null;
        }
        else
        {
          time = 0.0f;
          while (!slowStopFlagDic[Source])
          {
            time += battleManager.DeltaTime_60fps;
            if (time <= (double) SlowEffect.Duration)
            {
              yield return null;
            }
            else
            {
              time = 0.0f;
              while (!slowStopFlagDic[Source])
              {
                time += battleManager.DeltaTime_60fps;
                baseTimeScale = SlowEffect.EndEasing.GetCurVal(battleManager.DeltaTime_60fps);
                                UnityEngine.Time.timeScale = (speedupFlag ? speedUpRate : 1f) * BaseTimeScale;
                if (time <= (double) SlowEffect.EndDuration)
                {
                  yield return null;
                }
                else
                {
                  baseTimeScale = 1f;
                  break;
                }
              }
              break;
            }
          }
          break;
        }
      }
    }

    public void Release()
    {
    }
  }
}
