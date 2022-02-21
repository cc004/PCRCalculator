// Decompiled with JetBrains decompiler
// Type: Elements.ShakeEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Elements
{
  [Serializable]
  public class ShakeEffect
  {
    public ShakeType ShakeType;
    public float StartTime;
    public float Duration;
    public int TargetMotion;
    public float StartAmplitude;
    public float EndAmplitude;
    public float StartFrequency;
    public float EndFrequency;
    public float Rotation;
    public CustomEasing EasingFrequency;
    public CustomEasing EasingAmplitude;
    [NonSerialized]
    private Skill skill;
    private UnitCtrl owner;

    private float timer { get; set; }

    private int numberOfShake { get; set; }

    public float CurrentAmplutude { get; set; }

    public Vector3 CurrentShakePos { get; set; }

    public bool ShakeLoopEnd { get; set; }

    public void ResetStart(Skill skill, UnitCtrl owner)
    {
      this.skill = skill;
      this.owner = owner;
      timer = 0.0f;
      numberOfShake = 0;
      CurrentAmplutude = StartAmplitude;
      CurrentShakePos = Vector3.zero;
      EasingFrequency = new CustomEasing(CustomEasing.eType.outQuad, StartFrequency, EndFrequency, Duration);
      EasingAmplitude = new CustomEasing(CustomEasing.eType.outQuad, StartAmplitude, EndAmplitude, Duration);
    }

    public bool GetOwnerPause() => owner != null && owner.Pause;

    public bool Simulate(float deltaTime)
    {
      if (ShakeLoopEnd)
        return true;
      if (skill != null && skill.Cancel)
      {
        ResetStart(null, null);
        return true;
      }
      if (owner != null && owner.Pause)
        return false;
      timer += deltaTime;
      int numberOfShake1 = numberOfShake;
      float curVal = EasingFrequency.GetCurVal(deltaTime);
      CurrentAmplutude = EasingAmplitude.GetCurVal(deltaTime);
      numberOfShake = (int) (curVal * 2.0 * 3.14159274101257 * timer / 3.14159274101257 / 2.0);
      int numberOfShake2 = numberOfShake;
      if (numberOfShake1 != numberOfShake2 && ShakeType == ShakeType.RANDOM)
        Rotation = Random.Range(0.0f, 360f);
      CurrentShakePos = new Vector3(CurrentAmplutude * Mathf.Sin((float) (curVal * 2.0 * 3.14159274101257) * timer) * Mathf.Cos((float) (Rotation / 180.0 * 3.14159274101257)), CurrentAmplutude * Mathf.Sin((float) (curVal * 2.0 * 3.14159274101257) * timer) * Mathf.Sin((float) (Rotation / 180.0 * 3.14159274101257)), 0.0f);
      if (timer <= (double) Duration)
        return false;
      ResetStart(null, null);
      return true;
    }
  }
}
