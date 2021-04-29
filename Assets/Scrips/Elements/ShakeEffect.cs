// Decompiled with JetBrains decompiler
// Type: Elements.ShakeEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using UnityEngine;

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
      this.timer = 0.0f;
      this.numberOfShake = 0;
      this.CurrentAmplutude = this.StartAmplitude;
      this.CurrentShakePos = Vector3.zero;
      this.EasingFrequency = new CustomEasing(CustomEasing.eType.outQuad, this.StartFrequency, this.EndFrequency, this.Duration);
      this.EasingAmplitude = new CustomEasing(CustomEasing.eType.outQuad, this.StartAmplitude, this.EndAmplitude, this.Duration);
    }

    public bool GetOwnerPause() => (UnityEngine.Object) this.owner != (UnityEngine.Object) null && this.owner.Pause;

    public bool Simulate(float deltaTime)
    {
      if (this.ShakeLoopEnd)
        return true;
      if (this.skill != null && this.skill.Cancel)
      {
        this.ResetStart((Skill) null, (UnitCtrl) null);
        return true;
      }
      if ((UnityEngine.Object) this.owner != (UnityEngine.Object) null && this.owner.Pause)
        return false;
      this.timer += deltaTime;
      int numberOfShake1 = this.numberOfShake;
      float curVal = this.EasingFrequency.GetCurVal(deltaTime);
      this.CurrentAmplutude = this.EasingAmplitude.GetCurVal(deltaTime);
      this.numberOfShake = (int) ((double) curVal * 2.0 * 3.14159274101257 * (double) this.timer / 3.14159274101257 / 2.0);
      int numberOfShake2 = this.numberOfShake;
      if (numberOfShake1 != numberOfShake2 && this.ShakeType == ShakeType.RANDOM)
        this.Rotation = UnityEngine.Random.Range(0.0f, 360f);
      this.CurrentShakePos = new Vector3(this.CurrentAmplutude * Mathf.Sin((float) ((double) curVal * 2.0 * 3.14159274101257) * this.timer) * Mathf.Cos((float) ((double) this.Rotation / 180.0 * 3.14159274101257)), this.CurrentAmplutude * Mathf.Sin((float) ((double) curVal * 2.0 * 3.14159274101257) * this.timer) * Mathf.Sin((float) ((double) this.Rotation / 180.0 * 3.14159274101257)), 0.0f);
      if ((double) this.timer <= (double) this.Duration)
        return false;
      this.ResetStart((Skill) null, (UnitCtrl) null);
      return true;
    }
  }
}
