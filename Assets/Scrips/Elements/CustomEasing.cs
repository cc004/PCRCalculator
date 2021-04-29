// Decompiled with JetBrains decompiler
// Type: Elements.CustomEasing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;

namespace Elements
{
  public class CustomEasing
  {
    private float beginVal;
    private float endVal;
    private float changeVal;
    private float duration;
    private float curTime;
    private CustomEasing.easingFunc func;

    public CustomEasing(
      CustomEasing.eType type,
      float beginValue,
      float endValue,
      float durationTime)
    {
      switch (type)
      {
        case CustomEasing.eType.linear:
          this.func = new CustomEasing.easingFunc(this.linear);
          break;
        case CustomEasing.eType.inQuad:
          this.func = new CustomEasing.easingFunc(this.inQuad);
          break;
        case CustomEasing.eType.outQuad:
          this.func = new CustomEasing.easingFunc(this.outQuad);
          break;
        case CustomEasing.eType.inOutQuad:
          this.func = new CustomEasing.easingFunc(this.inOutQuad);
          break;
        case CustomEasing.eType.inCubic:
          this.func = new CustomEasing.easingFunc(this.inCubic);
          break;
        case CustomEasing.eType.outCubic:
          this.func = new CustomEasing.easingFunc(this.outCubic);
          break;
        case CustomEasing.eType.inOutCubic:
          this.func = new CustomEasing.easingFunc(this.inOutCubic);
          break;
        case CustomEasing.eType.inQuart:
          this.func = new CustomEasing.easingFunc(this.inQuart);
          break;
        case CustomEasing.eType.outQuart:
          this.func = new CustomEasing.easingFunc(this.outQuart);
          break;
        case CustomEasing.eType.inOutQuart:
          this.func = new CustomEasing.easingFunc(this.inOutQuart);
          break;
        case CustomEasing.eType.inSine:
          this.func = new CustomEasing.easingFunc(this.inSine);
          break;
        case CustomEasing.eType.outSine:
          this.func = new CustomEasing.easingFunc(this.outSine);
          break;
        case CustomEasing.eType.inOutSine:
          this.func = new CustomEasing.easingFunc(this.inOutSine);
          break;
        case CustomEasing.eType.inExpo:
          this.func = new CustomEasing.easingFunc(this.inExpo);
          break;
        case CustomEasing.eType.outExpo:
          this.func = new CustomEasing.easingFunc(this.outExpo);
          break;
        case CustomEasing.eType.inOutExpo:
          this.func = new CustomEasing.easingFunc(this.inOutExpo);
          break;
        case CustomEasing.eType.inCirc:
          this.func = new CustomEasing.easingFunc(this.inCirc);
          break;
        case CustomEasing.eType.outCirc:
          this.func = new CustomEasing.easingFunc(this.outCirc);
          break;
        case CustomEasing.eType.inOutCirc:
          this.func = new CustomEasing.easingFunc(this.inOutCirc);
          break;
        case CustomEasing.eType.inElastic:
          this.func = new CustomEasing.easingFunc(this.inElastic);
          break;
        case CustomEasing.eType.outElastic:
          this.func = new CustomEasing.easingFunc(this.outElastic);
          break;
        case CustomEasing.eType.inOutElastic:
          this.func = new CustomEasing.easingFunc(this.inOutElastic);
          break;
        case CustomEasing.eType.inBack:
          this.func = new CustomEasing.easingFunc(this.inBack);
          break;
        case CustomEasing.eType.outBack:
          this.func = new CustomEasing.easingFunc(this.outBack);
          break;
        case CustomEasing.eType.inOutBack:
          this.func = new CustomEasing.easingFunc(this.inOutBack);
          break;
        case CustomEasing.eType.inBounce:
          this.func = new CustomEasing.easingFunc(this.inBounce);
          break;
        case CustomEasing.eType.outBounce:
          this.func = new CustomEasing.easingFunc(this.outBounce);
          break;
        case CustomEasing.eType.inOutBounce:
          this.func = new CustomEasing.easingFunc(this.inOutBounce);
          break;
      }
      this.beginVal = beginValue;
      this.endVal = endValue;
      this.duration = durationTime;
      this.curTime = 0.0f;
      this.changeVal = endValue - beginValue;
      this.IsMoving = true;
    }

    public bool IsMoving { get; private set; }

    public float GetCurVal(float deltaTime, bool canOver = false)
    {
      this.curTime += deltaTime;
      if ((double) this.curTime < (double) this.duration || canOver)
        return this.func(this.curTime) + this.beginVal;
      this.IsMoving = false;
      return this.endVal;
    }

    private float linear(float t) => this.changeVal * t / this.duration;

    private float inQuad(float t)
    {
      float num = t / this.duration;
      return this.changeVal * num * num;
    }

    private float outQuad(float t)
    {
      float num = t / this.duration;
      return (float) (-(double) this.changeVal * (double) num * ((double) num - 2.0));
    }

    private float inOutQuad(float t)
    {
      float num = t * 2f / this.duration;
      return (double) num < 1.0 ? this.changeVal / 2f * num * num : (float) (-(double) this.changeVal / 2.0 * (((double) num - 1.0) * ((double) num - 3.0) - 1.0));
    }

    private float inCubic(float t)
    {
      float num = t / this.duration;
      return this.changeVal * num * num * num;
    }

    private float outCubic(float t)
    {
      float num = (float) ((double) t / (double) this.duration - 1.0);
      return this.changeVal * (float) ((double) num * (double) num * (double) num + 1.0);
    }

    private float inOutCubic(float t)
    {
      float num1 = t * 2f / this.duration;
      if ((double) num1 < 1.0)
        return this.changeVal / 2f * num1 * num1 * num1;
      float num2 = num1 - 2f;
      return (float) ((double) this.changeVal / 2.0 * ((double) num2 * (double) num2 * (double) num2 + 2.0));
    }

    private float inQuart(float t)
    {
      float num = t / this.duration;
      return this.changeVal * num * num * num * num;
    }

    private float outQuart(float t)
    {
      float num = (float) ((double) t / (double) this.duration - 1.0);
      return (float) (-(double) this.changeVal * ((double) num * (double) num * (double) num * (double) num - 1.0));
    }

    private float inOutQuart(float t)
    {
      float num1 = t * 2f / this.duration;
      if ((double) num1 < 1.0)
        return this.changeVal / 2f * num1 * num1 * num1 * num1;
      float num2 = num1 - 2f;
      return (float) (-(double) this.changeVal / 2.0 * ((double) num2 * (double) num2 * (double) num2 * (double) num2 - 2.0));
    }

    private float inSine(float t) => -this.changeVal * (float) Math.Cos((double) t / (double) this.duration * (Math.PI / 2.0)) + this.changeVal;

    private float outSine(float t) => this.changeVal * (float) Math.Sin((double) t / (double) this.duration * (Math.PI / 2.0));

    private float inOutSine(float t) => (float) (-(double) this.changeVal / 2.0 * (Math.Cos((double) t / (double) this.duration * Math.PI) - 1.0));

    private float inExpo(float t) => (double) t == 0.0 ? 0.0f : this.changeVal * (float) Math.Pow(2.0, 10.0 * ((double) t / (double) this.duration - 1.0));

    private float outExpo(float t) => this.changeVal * (float) (-Math.Pow(2.0, -10.0 * (double) t / (double) this.duration) + 1.0);

    private float inOutExpo(float t)
    {
      if ((double) t == 0.0)
        return 0.0f;
      float num = t * 2f / this.duration;
      return (double) num < 1.0 ? this.changeVal / 2f * (float) Math.Pow(2.0, 10.0 * ((double) num - 1.0)) : (float) ((double) this.changeVal / 2.0 * (-Math.Pow(2.0, -10.0 * ((double) num - 1.0)) + 2.0));
    }

    private float inCirc(float t)
    {
      float num = t / this.duration;
      return (float) (-(double) this.changeVal * (Math.Sqrt(1.0 - (double) num * (double) num) - 1.0));
    }

    private float outCirc(float t)
    {
      float num = (float) ((double) t / (double) this.duration - 1.0);
      return this.changeVal * (float) Math.Sqrt(1.0 - (double) num * (double) num);
    }

    private float inOutCirc(float t)
    {
      float num1 = t * 2f / this.duration;
      if ((double) num1 < 1.0)
        return (float) (-(double) this.changeVal / 2.0 * (Math.Sqrt(1.0 - (double) num1 * (double) num1) - 1.0));
      float num2 = num1 - 2f;
      return (float) ((double) this.changeVal / 2.0 * (Math.Sqrt(1.0 - (double) num2 * (double) num2) + 1.0));
    }

    private float inElastic(float t)
    {
      float num1 = t / this.duration;
      float changeVal = this.changeVal;
      if ((double) num1 == 0.0)
        return 0.0f;
      float num2 = this.duration * 0.3f;
      float num3 = (double) changeVal >= (double) Math.Abs(this.changeVal) ? num2 / 6.283185f * (float) Math.Asin((double) this.changeVal / (double) changeVal) : num2 / 4f;
      float num4 = num1 - 1f;
      return (float) -((double) changeVal * Math.Pow(2.0, 10.0 * (double) num4) * Math.Sin(((double) num4 * (double) this.duration - (double) num3) * 6.28318548202515 / (double) num2));
    }

    private float outElastic(float t)
    {
      float num1 = t / this.duration;
      float changeVal = this.changeVal;
      if ((double) num1 == 0.0)
        return 0.0f;
      float num2 = this.duration * 0.3f;
      float num3 = (double) changeVal >= (double) Math.Abs(this.changeVal) ? num2 / 6.283185f * (float) Math.Asin((double) this.changeVal / (double) changeVal) : num2 / 4f;
      return changeVal * (float) Math.Pow(2.0, -10.0 * (double) num1) * (float) Math.Sin(((double) num1 * (double) this.duration - (double) num3) * 6.28318548202515 / (double) num2) + this.changeVal;
    }

    private float inOutElastic(float t)
    {
      float num1 = t * 2f / this.duration;
      float changeVal = this.changeVal;
      if ((double) num1 == 0.0)
        return 0.0f;
      float num2 = this.duration * 0.45f;
      float num3 = (double) changeVal >= (double) Math.Abs(this.changeVal) ? num2 / 6.283185f * (float) Math.Asin((double) this.changeVal / (double) changeVal) : num2 / 4f;
      if ((double) num1 < 1.0)
      {
        float num4 = num1 - 1f;
        return (float) (-0.5 * ((double) changeVal * Math.Pow(2.0, 10.0 * (double) num4) * Math.Sin(((double) num4 * (double) this.duration - (double) num3) * 6.28318548202515 / (double) num2)));
      }
      float num5 = num1 - 1f;
      return (float) ((double) changeVal * Math.Pow(2.0, -10.0 * (double) num5) * Math.Sin(((double) num5 * (double) this.duration - (double) num3) * (6.28318548202515 / (double) num2)) * 0.5) + this.changeVal;
    }

    private float inBack(float t)
    {
      float num1 = t / this.duration;
      float num2 = 1.70158f;
      return (float) ((double) this.changeVal * (double) num1 * (double) num1 * (((double) num2 + 1.0) * (double) num1 - (double) num2));
    }

    private float outBack(float t)
    {
      float num1 = (float) ((double) t / (double) this.duration - 1.0);
      float num2 = 1.70158f;
      return this.changeVal * (float) ((double) num1 * (double) num1 * (((double) num2 + 1.0) * (double) num1 + (double) num2) + 1.0);
    }

    private float inOutBack(float t)
    {
      float num1 = t * 2f / this.duration;
      float num2 = 2.594909f;
      if ((double) num1 < 1.0)
        return (float) ((double) this.changeVal / 2.0 * (double) num1 * (double) num1 * (((double) num2 + 1.0) * (double) num1 - (double) num2));
      float num3 = num1 - 2f;
      return (float) ((double) this.changeVal / 2.0 * ((double) num3 * (double) num3 * (((double) num2 + 1.0) * (double) num3 + (double) num2) + 2.0));
    }

    private float inBounce(float t) => this.changeVal - this.outBounce(this.duration - t);

    private float outBounce(float t)
    {
      float num1 = t / this.duration;
      if ((double) num1 < 0.363636374473572)
        return this.changeVal * (121f / 16f * num1 * num1);
      if ((double) num1 < 0.727272748947144)
      {
        float num2 = num1 - 0.5454546f;
        return this.changeVal * (float) (121.0 / 16.0 * (double) num2 * (double) num2 + 0.75);
      }
      if ((double) num1 < 0.909090936183929)
      {
        float num2 = num1 - 0.8181818f;
        return this.changeVal * (float) (121.0 / 16.0 * (double) num2 * (double) num2 + 15.0 / 16.0);
      }
      float num3 = num1 - 0.9545454f;
      return this.changeVal * (float) (121.0 / 16.0 * (double) num3 * (double) num3 + 63.0 / 64.0);
    }

    private float inOutBounce(float t) => (double) t * 2.0 < (double) this.duration ? this.inBounce(t * 2f) * 0.5f : (float) (0.5 * (double) this.outBounce(t * 2f - this.duration) + (double) this.changeVal * 0.5);

    public enum eType
    {
      linear,
      inQuad,
      outQuad,
      inOutQuad,
      inCubic,
      outCubic,
      inOutCubic,
      inQuart,
      outQuart,
      inOutQuart,
      inSine,
      outSine,
      inOutSine,
      inExpo,
      outExpo,
      inOutExpo,
      inCirc,
      outCirc,
      inOutCirc,
      inElastic,
      outElastic,
      inOutElastic,
      inBack,
      outBack,
      inOutBack,
      inBounce,
      outBounce,
      inOutBounce,
    }

    private delegate float easingFunc(float curTime);
  }
}
