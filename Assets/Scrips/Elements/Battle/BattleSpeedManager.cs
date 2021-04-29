// Decompiled with JetBrains decompiler
// Type: Elements.Battle.FHDHEIDIPOI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    public float BaseTimeScale => this.baseTimeScale;

    public bool SpeedUpFlag
    {
      set
      {
        this.speedupFlag = value;
        //SoundManager instance = ManagerSingleton<SoundManager>.Instance;
        if (this.speedupFlag)
        {
          Time.timeScale = this.speedUpRate * this.BaseTimeScale;
          //instance.SetSoundSpeed(this.speedUpRate * this.BaseTimeScale);
        }
        else
        {
          Time.timeScale = this.BaseTimeScale;
          //instance.SetSoundSpeed(this.BaseTimeScale);
        }
      }
      get => this.speedupFlag;
    }

    public float SpeedUpRate
        {
            get => speedUpRate;
            set
            {
                if (value >= 0.124f && value<=20.1f)
                {
                    speedUpRate = value;
                }
                else
                {
                    speedUpRate = 1;
                }
            }
        }

    public bool IsSpeedQuadruple
    {
      get => this.isSpeedQuadruple;
      set
      {
        this.isSpeedQuadruple = value;
        if (this.isSpeedQuadruple)
          this.speedUpRate = 4f;
        else
          this.speedUpRate = 2f;
      }
    }

    public BattleSpeedManager(BattleManager_Time KHPJAFEGFBO) => this.battleManager = KHPJAFEGFBO;

    public void SetBaseTimeScale(float EGAMDNELEIL) => this.baseTimeScale = EGAMDNELEIL;

    //[Conditional("UNITY_STANDALONE_LINUX")]
    //[Conditional("CYG_DEBUG")]
    public void SetSpeedUpRate(float JKKFBGMONMB) => this.speedUpRate = JKKFBGMONMB;

    public void StartSlowEffect(
      BattleDefine.SlowEffect SlowEffect,
      UnitCtrl Owner,
      float Time,
      bool ignoreFPS)
    {
      if (ignoreFPS)
        this.battleManager.StartCoroutineIgnoreFps(this.updateSlowEffect(SlowEffect, Owner, Time));
      else
        this.battleManager.AppendCoroutine(this.updateSlowEffect(SlowEffect, Owner, Time), ePauseType.VISUAL, Owner);
    }

    public void StopSlowEffect()
    {
      List<UnitCtrl> unitCtrlList = new List<UnitCtrl>((IEnumerable<UnitCtrl>) this.slowStopFlagDic.Keys);
      for (int index = 0; index < unitCtrlList.Count; ++index)
        this.slowStopFlagDic[unitCtrlList[index]] = true;
      this.baseTimeScale = 1f;
      Time.timeScale = (this.speedupFlag ? this.speedUpRate : 1f) * this.BaseTimeScale;
    }

    private IEnumerator updateSlowEffect(
      BattleDefine.SlowEffect SlowEffect,
      UnitCtrl Source,
      float Time)
    {
      float time = Time;
      while (true)
      {
        time += this.battleManager.DeltaTime_60fps;
        if ((double) time <= (double) SlowEffect.StartDelay)
          yield return (object) null;
        else
          break;
      }
      List<UnitCtrl> unitCtrlList = new List<UnitCtrl>((IEnumerable<UnitCtrl>) this.slowStopFlagDic.Keys);
      for (int index = 0; index < unitCtrlList.Count; ++index)
        this.slowStopFlagDic[unitCtrlList[index]] = true;
      if (this.slowStopFlagDic.ContainsKey(Source))
        this.slowStopFlagDic[Source] = false;
      else
        this.slowStopFlagDic.Add(Source, false);
      SlowEffect.StartEasing = new CustomEasing(CustomEasing.eType.outQuad, this.BaseTimeScale, SlowEffect.To, SlowEffect.StartDuration);
      SlowEffect.EndEasing = new CustomEasing(CustomEasing.eType.outQuad, SlowEffect.To, 1f, SlowEffect.EndDuration);
      time = 0.0f;
      while (!this.slowStopFlagDic[Source])
      {
        time += this.battleManager.DeltaTime_60fps;
        this.baseTimeScale = SlowEffect.StartEasing.GetCurVal(this.battleManager.DeltaTime_60fps);
                UnityEngine.Time.timeScale = (this.speedupFlag ? this.speedUpRate : 1f) * this.BaseTimeScale;
        if ((double) time <= (double) SlowEffect.StartDuration)
        {
          yield return (object) null;
        }
        else
        {
          time = 0.0f;
          while (!this.slowStopFlagDic[Source])
          {
            time += this.battleManager.DeltaTime_60fps;
            if ((double) time <= (double) SlowEffect.Duration)
            {
              yield return (object) null;
            }
            else
            {
              time = 0.0f;
              while (!this.slowStopFlagDic[Source])
              {
                time += this.battleManager.DeltaTime_60fps;
                this.baseTimeScale = SlowEffect.EndEasing.GetCurVal(this.battleManager.DeltaTime_60fps);
                                UnityEngine.Time.timeScale = (this.speedupFlag ? this.speedUpRate : 1f) * this.BaseTimeScale;
                if ((double) time <= (double) SlowEffect.EndDuration)
                {
                  yield return (object) null;
                }
                else
                {
                  this.baseTimeScale = 1f;
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
