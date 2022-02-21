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
    private easingFunc func;

    public CustomEasing(
      eType type,
      float beginValue,
      float endValue,
      float durationTime)
    {
      switch (type)
      {
        case eType.linear:
          func = linear;
          break;
        case eType.inQuad:
          func = inQuad;
          break;
        case eType.outQuad:
          func = outQuad;
          break;
        case eType.inOutQuad:
          func = inOutQuad;
          break;
        case eType.inCubic:
          func = inCubic;
          break;
        case eType.outCubic:
          func = outCubic;
          break;
        case eType.inOutCubic:
          func = inOutCubic;
          break;
        case eType.inQuart:
          func = inQuart;
          break;
        case eType.outQuart:
          func = outQuart;
          break;
        case eType.inOutQuart:
          func = inOutQuart;
          break;
        case eType.inSine:
          func = inSine;
          break;
        case eType.outSine:
          func = outSine;
          break;
        case eType.inOutSine:
          func = inOutSine;
          break;
        case eType.inExpo:
          func = inExpo;
          break;
        case eType.outExpo:
          func = outExpo;
          break;
        case eType.inOutExpo:
          func = inOutExpo;
          break;
        case eType.inCirc:
          func = inCirc;
          break;
        case eType.outCirc:
          func = outCirc;
          break;
        case eType.inOutCirc:
          func = inOutCirc;
          break;
        case eType.inElastic:
          func = inElastic;
          break;
        case eType.outElastic:
          func = outElastic;
          break;
        case eType.inOutElastic:
          func = inOutElastic;
          break;
        case eType.inBack:
          func = inBack;
          break;
        case eType.outBack:
          func = outBack;
          break;
        case eType.inOutBack:
          func = inOutBack;
          break;
        case eType.inBounce:
          func = inBounce;
          break;
        case eType.outBounce:
          func = outBounce;
          break;
        case eType.inOutBounce:
          func = inOutBounce;
          break;
      }
      beginVal = beginValue;
      endVal = endValue;
      duration = durationTime;
      curTime = 0.0f;
      changeVal = endValue - beginValue;
      IsMoving = true;
    }

    public bool IsMoving { get; private set; }

    public float GetCurVal(float deltaTime, bool canOver = false)
    {
      curTime += deltaTime;
      if (curTime < (double) duration || canOver)
        return func(curTime) + beginVal;
      IsMoving = false;
      return endVal;
    }

    private float linear(float t) => changeVal * t / duration;

    private float inQuad(float t)
    {
      float num = t / duration;
      return changeVal * num * num;
    }

    private float outQuad(float t)
    {
      float num = t / duration;
      return (float) (-(double) changeVal * num * (num - 2.0));
    }

    private float inOutQuad(float t)
    {
      float num = t * 2f / duration;
      return num < 1.0 ? changeVal / 2f * num * num : (float) (-(double) changeVal / 2.0 * ((num - 1.0) * (num - 3.0) - 1.0));
    }

    private float inCubic(float t)
    {
      float num = t / duration;
      return changeVal * num * num * num;
    }

    private float outCubic(float t)
    {
      float num = (float) (t / (double) duration - 1.0);
      return changeVal * (float) (num * (double) num * num + 1.0);
    }

    private float inOutCubic(float t)
    {
      float num1 = t * 2f / duration;
      if (num1 < 1.0)
        return changeVal / 2f * num1 * num1 * num1;
      float num2 = num1 - 2f;
      return (float) (changeVal / 2.0 * (num2 * (double) num2 * num2 + 2.0));
    }

    private float inQuart(float t)
    {
      float num = t / duration;
      return changeVal * num * num * num * num;
    }

    private float outQuart(float t)
    {
      float num = (float) (t / (double) duration - 1.0);
      return (float) (-(double) changeVal * (num * (double) num * num * num - 1.0));
    }

    private float inOutQuart(float t)
    {
      float num1 = t * 2f / duration;
      if (num1 < 1.0)
        return changeVal / 2f * num1 * num1 * num1 * num1;
      float num2 = num1 - 2f;
      return (float) (-(double) changeVal / 2.0 * (num2 * (double) num2 * num2 * num2 - 2.0));
    }

    private float inSine(float t) => -changeVal * (float) Math.Cos(t / (double) duration * (Math.PI / 2.0)) + changeVal;

    private float outSine(float t) => changeVal * (float) Math.Sin(t / (double) duration * (Math.PI / 2.0));

    private float inOutSine(float t) => (float) (-(double) changeVal / 2.0 * (Math.Cos(t / (double) duration * Math.PI) - 1.0));

    private float inExpo(float t) => t == 0.0 ? 0.0f : changeVal * (float) Math.Pow(2.0, 10.0 * (t / (double) duration - 1.0));

    private float outExpo(float t) => changeVal * (float) (-Math.Pow(2.0, -10.0 * t / duration) + 1.0);

    private float inOutExpo(float t)
    {
      if (t == 0.0)
        return 0.0f;
      float num = t * 2f / duration;
      return num < 1.0 ? changeVal / 2f * (float) Math.Pow(2.0, 10.0 * (num - 1.0)) : (float) (changeVal / 2.0 * (-Math.Pow(2.0, -10.0 * (num - 1.0)) + 2.0));
    }

    private float inCirc(float t)
    {
      float num = t / duration;
      return (float) (-(double) changeVal * (Math.Sqrt(1.0 - num * (double) num) - 1.0));
    }

    private float outCirc(float t)
    {
      float num = (float) (t / (double) duration - 1.0);
      return changeVal * (float) Math.Sqrt(1.0 - num * (double) num);
    }

    private float inOutCirc(float t)
    {
      float num1 = t * 2f / duration;
      if (num1 < 1.0)
        return (float) (-(double) changeVal / 2.0 * (Math.Sqrt(1.0 - num1 * (double) num1) - 1.0));
      float num2 = num1 - 2f;
      return (float) (changeVal / 2.0 * (Math.Sqrt(1.0 - num2 * (double) num2) + 1.0));
    }

    private float inElastic(float t)
    {
      float num1 = t / duration;
      float changeVal = this.changeVal;
      if (num1 == 0.0)
        return 0.0f;
      float num2 = duration * 0.3f;
      float num3 = changeVal >= (double) Math.Abs(this.changeVal) ? num2 / 6.283185f * (float) Math.Asin(this.changeVal / (double) changeVal) : num2 / 4f;
      float num4 = num1 - 1f;
      return (float) -(changeVal * Math.Pow(2.0, 10.0 * num4) * Math.Sin((num4 * (double) duration - num3) * 6.28318548202515 / num2));
    }

    private float outElastic(float t)
    {
      float num1 = t / duration;
      float changeVal = this.changeVal;
      if (num1 == 0.0)
        return 0.0f;
      float num2 = duration * 0.3f;
      float num3 = changeVal >= (double) Math.Abs(this.changeVal) ? num2 / 6.283185f * (float) Math.Asin(this.changeVal / (double) changeVal) : num2 / 4f;
      return changeVal * (float) Math.Pow(2.0, -10.0 * num1) * (float) Math.Sin((num1 * (double) duration - num3) * 6.28318548202515 / num2) + this.changeVal;
    }

    private float inOutElastic(float t)
    {
      float num1 = t * 2f / duration;
      float changeVal = this.changeVal;
      if (num1 == 0.0)
        return 0.0f;
      float num2 = duration * 0.45f;
      float num3 = changeVal >= (double) Math.Abs(this.changeVal) ? num2 / 6.283185f * (float) Math.Asin(this.changeVal / (double) changeVal) : num2 / 4f;
      if (num1 < 1.0)
      {
        float num4 = num1 - 1f;
        return (float) (-0.5 * (changeVal * Math.Pow(2.0, 10.0 * num4) * Math.Sin((num4 * (double) duration - num3) * 6.28318548202515 / num2)));
      }
      float num5 = num1 - 1f;
      return (float) (changeVal * Math.Pow(2.0, -10.0 * num5) * Math.Sin((num5 * (double) duration - num3) * (6.28318548202515 / num2)) * 0.5) + this.changeVal;
    }

    private float inBack(float t)
    {
      float num1 = t / duration;
      float num2 = 1.70158f;
      return (float) (changeVal * (double) num1 * num1 * ((num2 + 1.0) * num1 - num2));
    }

    private float outBack(float t)
    {
      float num1 = (float) (t / (double) duration - 1.0);
      float num2 = 1.70158f;
      return changeVal * (float) (num1 * (double) num1 * ((num2 + 1.0) * num1 + num2) + 1.0);
    }

    private float inOutBack(float t)
    {
      float num1 = t * 2f / duration;
      float num2 = 2.594909f;
      if (num1 < 1.0)
        return (float) (changeVal / 2.0 * num1 * num1 * ((num2 + 1.0) * num1 - num2));
      float num3 = num1 - 2f;
      return (float) (changeVal / 2.0 * (num3 * (double) num3 * ((num2 + 1.0) * num3 + num2) + 2.0));
    }

    private float inBounce(float t) => changeVal - outBounce(duration - t);

    private float outBounce(float t)
    {
      float num1 = t / duration;
      if (num1 < 0.363636374473572)
        return changeVal * (121f / 16f * num1 * num1);
      if (num1 < 0.727272748947144)
      {
        float num2 = num1 - 0.5454546f;
        return changeVal * (float) (121.0 / 16.0 * num2 * num2 + 0.75);
      }
      if (num1 < 0.909090936183929)
      {
        float num2 = num1 - 0.8181818f;
        return changeVal * (float) (121.0 / 16.0 * num2 * num2 + 15.0 / 16.0);
      }
      float num3 = num1 - 0.9545454f;
      return changeVal * (float) (121.0 / 16.0 * num3 * num3 + 63.0 / 64.0);
    }

    private float inOutBounce(float t) => t * 2.0 < duration ? inBounce(t * 2f) * 0.5f : (float) (0.5 * outBounce(t * 2f - duration) + changeVal * 0.5);

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
