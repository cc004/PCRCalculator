// Decompiled with JetBrains decompiler
// Type: Elements.ExtensionMethods
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Cute;
using UnityEngine;

namespace Elements
{
  public static class ExtensionMethods
  {
    public static readonly WaitForEndOfFrame WAIT_FOR_END_OF_FRAME = new WaitForEndOfFrame();

    public static string ToStringCurrency(this int _self) => _self.ToString("N0");
        
    public static string ToStringCurrency(this long _self) => _self.ToString("N0");

    public static bool IsNullOrEmpty(this string _self) => string.IsNullOrEmpty(_self);

    public static string Format(this string _self, params object[] _args) => string.Format(_self, _args);

    public static void InitTransform(this MonoBehaviour _self)
    {
      _self.transform.localPosition = Vector3.zero;
      _self.transform.localRotation = Quaternion.identity;
      _self.transform.localScale = Vector3.one;
    }

    public static void InitTransform(this Transform _self)
    {
      _self.localPosition = Vector3.zero;
      _self.localRotation = Quaternion.identity;
      _self.localScale = Vector3.one;
    }

    public static void InitTransform(this GameObject _self)
    {
      _self.transform.localPosition = Vector3.zero;
      _self.transform.localRotation = Quaternion.identity;
      _self.transform.localScale = Vector3.one;
    }

    public static void SetLocalPosX(this MonoBehaviour _self, float _x)
    {
      Vector3 localPosition = _self.transform.localPosition;
      localPosition.x = _x;
      _self.transform.localPosition = localPosition;
    }

    public static void SetLocalPosY(this MonoBehaviour _self, float _y)
    {
      Vector3 localPosition = _self.transform.localPosition;
      localPosition.y = _y;
      _self.transform.localPosition = localPosition;
    }

    public static void SetLocalPosZ(this MonoBehaviour _self, float _z)
    {
      Vector3 localPosition = _self.transform.localPosition;
      localPosition.z = _z;
      _self.transform.localPosition = localPosition;
    }

    public static void SetLocalPosX(this Transform _self, float _x)
    {
      Vector3 localPosition = _self.localPosition;
      localPosition.x = _x;
      _self.localPosition = localPosition;
    }

    public static void SetLocalPosY(this Transform _self, float _y)
    {
      Vector3 localPosition = _self.localPosition;
      localPosition.y = _y;
      _self.localPosition = localPosition;
    }

    public static void SetLocalPosZ(this Transform _self, float _z)
    {
      Vector3 localPosition = _self.localPosition;
      localPosition.z = _z;
      _self.localPosition = localPosition;
    }

    public static void SetLocalPosX(this GameObject _self, float _x)
    {
      Vector3 localPosition = _self.transform.localPosition;
      localPosition.x = _x;
      _self.transform.localPosition = localPosition;
    }

    public static void SetLocalPosY(this GameObject _self, float _y)
    {
      Vector3 localPosition = _self.transform.localPosition;
      localPosition.y = _y;
      _self.transform.localPosition = localPosition;
    }

    public static void SetLocalPosZ(this GameObject _self, float _z)
    {
      Vector3 localPosition = _self.transform.localPosition;
      localPosition.z = _z;
      _self.transform.localPosition = localPosition;
    }

    public static void SetWorldPosX(this MonoBehaviour _self, float _x)
    {
      Vector3 position = _self.transform.position;
      position.x = _x;
      _self.transform.position = position;
    }

    public static void SetWorldPosY(this MonoBehaviour _self, float _y)
    {
      Vector3 position = _self.transform.position;
      position.y = _y;
      _self.transform.position = position;
    }

    public static void SetWorldPosZ(this MonoBehaviour _self, float _z)
    {
      Vector3 position = _self.transform.position;
      position.z = _z;
      _self.transform.position = position;
    }

    public static void SetWorldPosX(this Transform _self, float _x)
    {
      Vector3 position = _self.position;
      position.x = _x;
      _self.position = position;
    }

    public static void SetWorldPosY(this Transform _self, float _y)
    {
      Vector3 position = _self.position;
      position.y = _y;
      _self.position = position;
    }

    public static void SetWorldPosZ(this Transform _self, float _z)
    {
      Vector3 position = _self.position;
      position.z = _z;
      _self.position = position;
    }

    public static void SetWorldPosX(this GameObject _self, float _x)
    {
      Vector3 position = _self.transform.position;
      position.x = _x;
      _self.transform.position = position;
    }

    public static void SetWorldPosY(this GameObject _self, float _y)
    {
      Vector3 position = _self.transform.position;
      position.y = _y;
      _self.transform.position = position;
    }

    public static void SetWorldPosZ(this GameObject _self, float _z)
    {
      Vector3 position = _self.transform.position;
      position.z = _z;
      _self.transform.position = position;
    }

    public static void SetActiveWithCheck(this GameObject _self, bool _isActive)
    {
      if (_self == null)
        return;
      _self.SetActive(_isActive);
    }

    public static void SetActive(this MonoBehaviour _self, bool _isActive) => _self.gameObject.SetActive(_isActive);

    public static void SetActiveWithCheck(this MonoBehaviour _self, bool _isActive)
    {
      if (_self == null)
        return;
      _self.gameObject.SetActive(_isActive);
    }

    /*public static float Linear(ref AnimationCurve curve, float time)
    {
      int length = curve.length;
      int index1 = -1;
      Keyframe keyframe;
      for (int index2 = 0; index2 < length; ++index2)
      {
        keyframe = curve.get_Item(index2);
        if ((double) keyframe.time > (double) time)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 < 0)
      {
        keyframe = curve.get_Item(length - 1);
        return keyframe.value;
      }
      if (index1 == 0)
      {
        keyframe = curve.get_Item(0);
        return keyframe.value;
      }
      keyframe = curve.get_Item(index1);
      double time1 = (double) keyframe.time;
      keyframe = curve.get_Item(index1 - 1);
      double time2 = (double) keyframe.time;
      float num1 = (float) (time1 - time2);
      double num2 = (double) time;
      keyframe = curve.get_Item(index1 - 1);
      double time3 = (double) keyframe.time;
      float num3 = (float) (num2 - time3) / num1;
      keyframe = curve.get_Item(index1);
      double num4 = (double) keyframe.value;
      keyframe = curve.get_Item(index1 - 1);
      double num5 = (double) keyframe.value;
      float num6 = (float) (num4 - num5);
      keyframe = curve.get_Item(index1 - 1);
      return keyframe.value + num6 * num3;
    }*/

    /*public static void Shuffle<T>(this List<T> _self)
    {
      int num = 0;
      for (int count = _self.Count; num < count; ++num)
      {
        int index = UnityEngine.Random.Range(num, _self.Count);
        T obj = _self[num];
        _self[num] = _self[index];
        _self[index] = obj;
      }
    }*/

    public static IEnumerable<IEnumerable<T>> Chunk<T>(
      this IEnumerable<T> _self,
      int _chunkSize)
    {
      for (; _self.Any(); _self = _self.Skip(_chunkSize))
        yield return _self.Take(_chunkSize);
    }

    /*public static IEnumerator GcCollectAsync(this MonoBehaviour _self, System.Action _callback)
    {
      IEnumerator routine = ExtensionMethods.gcCollectAsyncCoroutine(_callback);
      _self.StartCoroutine(routine);
      return routine;
    }*/

    /*private static IEnumerator gcCollectAsyncCoroutine(System.Action _callback)
    {
      LKAODOBDKHA threadJob = LKAODOBDKHA.Dispatch((System.Action) (() => GC.Collect()));
      while (!threadJob.NEIOJNCKGLL)
        yield return (object) ExtensionMethods.WAIT_FOR_END_OF_FRAME;
      yield return (object) ExtensionMethods.WAIT_FOR_END_OF_FRAME;
      _callback.Call();
    }*/

    /*public static IEnumerator WaitNormalProcess(
      this MonoBehaviour obj,
      System.Action onComplete)
    {
      IEnumerator routine = ExtensionMethods.waitNormalProcessCoroutine(obj, onComplete);
      obj.StartCoroutine(routine);
      return routine;
    }*/

    public static IEnumerator Timer(this MonoBehaviour obj, Action onComplete)
    {
      IEnumerator routine = timerFrameCoroutine(obj, onComplete);
      obj.StartCoroutine(routine);
      return routine;
    }

    public static IEnumerator Timer(
      this MonoBehaviour obj,
      float time,
      Action onComplete) => obj.Timer(time, 0.0f, onComplete);

    public static IEnumerator Timer(
      this MonoBehaviour obj,
      float time,
      float delay,
      Action onComplete)
    {
      IEnumerator routine = timerCoroutine(obj, time, delay, onComplete);
      obj.StartCoroutine(routine);
      return routine;
    }

    /*private static IEnumerator waitNormalProcessCoroutine(
      MonoBehaviour obj,
      System.Action onComplete)
    {
      float WAIT_TIME = 1.5f / (float) PABCCELMCAJ.IKMGFNGHDPJ;
      int count;
      for (count = 0; count < 3; ++count)
        yield return (object) null;
      for (count = 0; (double) WAIT_TIME <= (double) Time.deltaTime && count < 20; ++count)
        yield return (object) ExtensionMethods.WAIT_FOR_END_OF_FRAME;
      yield return (object) ExtensionMethods.WAIT_FOR_END_OF_FRAME;
      onComplete.Call();
    }*/

    private static IEnumerator timerFrameCoroutine(MonoBehaviour obj, Action onComplete)
    {
      yield return WAIT_FOR_END_OF_FRAME;
      onComplete.Call();
    }

    private static IEnumerator timerCoroutine(
      MonoBehaviour obj,
      float time,
      float delay,
      Action onComplete)
    {
      if (delay > 0.0)
        yield return new WaitForSeconds(delay);
      yield return new WaitForSeconds(time);
      onComplete.Call();
    }

    public static void StopCoroutine(this IEnumerator _ie, MonoBehaviour _obj)
    {
      if (_ie == null || !(_obj != null))
        return;
      _obj.StopCoroutine(_ie);
      _ie = null;
    }

    public static void AddIfNoContains<T>(this List<T> _list, T _value)
    {
      if (_list.Contains(_value))
        return;
      _list.Add(_value);
    }

    public static void AddIfNonNull<T>(this List<T> _list, T _value)
    {
      if (_value == null)
        return;
      _list.Add(_value);
    }

    public static void AddRangeIfNoContains<T>(this List<T> _list, List<T> _add)
    {
      for (int index = 0; index < _add.Count; ++index)
        _list.AddIfNoContains(_add[index]);
    }

    public static void ForEach<T>(this List<T> _list, Action<T> _callback)
    {
      for (int index = 0; index < _list.Count; ++index)
        _callback.Call(_list[index]);
    }

    public static void ForEach<T>(this HashSet<T> _hashSet, Action<T> _callback)
    {
      foreach (T hash in _hashSet)
        _callback(hash);
    }

    public static void AddIfNoContains<T>(this HashSet<T> _hashSet, T _value)
    {
      if (_hashSet.Contains(_value))
        return;
      _hashSet.Add(_value);
    }

    public static void ForEach<T>(this T[] _array, Action<T> _callback)
    {
      for (int index = 0; index < _array.Length; ++index)
        _callback(_array[index]);
    }

    public static void ForEachArrayArray<T>(this T[][] _arrayArray, Action<T> _action) => _arrayArray.ForEach((Action<T[]>) (_array =>
    {
      if (_array == null)
        return;
      _array.ForEach(_action);
    }));

    public static int CountArrayArray<T>(this T[][] _arrayArray)
    {
      int total = 0;
      _arrayArray.ForEach((Action<T[]>) (_array => total += _array.Length));
      return total;
    }

    public static T[] ArrayArrayToArray<T>(this T[][] _arrayArray)
    {
      T[] array = new T[_arrayArray.CountArrayArray()];
      int index = 0;
      _arrayArray.ForEachArrayArray((Action<T>) (_it => array[index++] = _it));
      return array;
    }

    public static void ArrayArrayForEach<T>(this T[][] _arrayArray, Action<T> _action)
    {
      for (int index1 = 0; index1 < _arrayArray.Length; ++index1)
      {
        if (_arrayArray[index1] != null)
        {
          for (int index2 = 0; index2 < _arrayArray[index1].Length; ++index2)
            _action(_arrayArray[index1][index2]);
        }
      }
    }

    public static void ForEach<Tkey, TValue>(
      this Dictionary<Tkey, TValue> _dictionary,
      Action<Tkey, TValue> _callback)
    {
      foreach (KeyValuePair<Tkey, TValue> keyValuePair in _dictionary)
        _callback(keyValuePair.Key, keyValuePair.Value);
    }

    public static void ForEachValue<Tkey, TValue>(
      this Dictionary<Tkey, TValue> _dictionary,
      Action<TValue> _callback)
    {
      foreach (KeyValuePair<Tkey, TValue> keyValuePair in _dictionary)
        _callback(keyValuePair.Value);
    }

    public static void AddIfNoContains<TKey, TValue>(
      this Dictionary<TKey, TValue> _dictionary,
      TKey _key,
      TValue _value)
    {
      if (_dictionary.ContainsKey(_key))
        return;
      _dictionary.Add(_key, _value);
    }

    public static void AddOrSetIfNoContains<TKey, TValue>(
      this Dictionary<TKey, TValue> _dictionary,
      TKey _key,
      TValue _value)
    {
      if (_dictionary.ContainsKey(_key))
        _dictionary[_key] = _value;
      else
        _dictionary.Add(_key, _value);
    }

    public static void AddIfNoContains<TKey, TValue>(
      this Dictionary<TKey, TValue> _dictionary,
      TKey _key,
      TValue _value,
      Action<TKey, TValue> _contains)
    {
      if (!_dictionary.ContainsKey(_key))
        _dictionary.Add(_key, _value);
      else
        _contains.Call(_key, _value);
    }

    public static void Deconstruct<TKey, TValue>(
      this KeyValuePair<TKey, TValue> _kvp,
      out TKey _key,
      out TValue _value)
    {
      _key = _kvp.Key;
      _value = _kvp.Value;
    }

    public static int ToInt(this bool _self) => !_self ? 0 : 1;

    public static bool IsPlaying(this Animator _self, int _layerIndex = 0) => _self.GetCurrentAnimatorStateInfo(_layerIndex).normalizedTime < 1.0;

    /*public static void PlayPingPong(this TweenAlpha _self, bool _isIncreaseAlpha)
    {
      float num1 = _self.from;
      float num2 = _self.to;
      if (_isIncreaseAlpha && (double) num1 > (double) num2 || !_isIncreaseAlpha && (double) num1 < (double) num2)
      {
        double num3 = (double) num1;
        num1 = num2;
        num2 = (float) num3;
      }
      bool flag = _self.direction == AnimationOrTween.Direction.Forward;
      _self.from = flag ? num1 : num2;
      _self.to = flag ? num2 : num1;
      _self.ResetToBeginning();
      _self.PlayForward();
    }

    public static void PlayPingPong(this TweenScale _self, bool _isIncreaseScale)
    {
      Vector3 vector3_1 = _self.from;
      Vector3 vector3_2 = _self.to;
      if (_isIncreaseScale && (double) vector3_1.x > (double) vector3_2.x || !_isIncreaseScale && (double) vector3_1.x < (double) vector3_2.x)
      {
        Vector3 vector3_3 = vector3_1;
        vector3_1 = vector3_2;
        vector3_2 = vector3_3;
      }
      bool flag = _self.direction == AnimationOrTween.Direction.Forward;
      _self.from = flag ? vector3_1 : vector3_2;
      _self.to = flag ? vector3_2 : vector3_1;
      _self.ResetToBeginning();
      _self.PlayForward();
    }*/

    /*public static void CopyComponent(this TweenAlpha _self, TweenAlpha _copyUITweener)
    {
      _self.animationCurve = _copyUITweener.animationCurve;
      _self.from = _copyUITweener.from;
      _self.to = _copyUITweener.to;
      _self.duration = _copyUITweener.duration;
      _self.delay = _copyUITweener.delay;
      _self.enabled = false;
    }

    public static void CopyComponent(this TweenScale _self, TweenScale _copyUITweener)
    {
      _self.animationCurve = _copyUITweener.animationCurve;
      _self.from = _copyUITweener.from;
      _self.to = _copyUITweener.to;
      _self.duration = _copyUITweener.duration;
      _self.delay = _copyUITweener.delay;
      _self.enabled = false;
    }

    public static void CopyComponent(this TweenPosition _self, TweenPosition _copyUITweener)
    {
      _self.animationCurve = _copyUITweener.animationCurve;
      _self.from = _copyUITweener.from;
      _self.to = _copyUITweener.to;
      _self.duration = _copyUITweener.duration;
      _self.delay = _copyUITweener.delay;
      _self.enabled = false;
    }*/

    /*public static void PlayForwardWithCheck(this UITweener _self)
    {
      if (!((UnityEngine.Object) _self != (UnityEngine.Object) null))
        return;
      _self.ResetToBeginning();
      _self.PlayForward();
    }

    public static void PlayReverseWithCheck(this UITweener _self)
    {
      if (!((UnityEngine.Object) _self != (UnityEngine.Object) null))
        return;
      _self.PlayReverse();
    }*/

    public static void SetParentWithChangeLayer(
      this Transform _self,
      Transform _parent,
      bool _worldPositionStays = true)
    {
      _self.SetParent(_parent, _worldPositionStays);
      _self.ChangeChildObjectLayer(_parent.gameObject.layer);
    }

    public static void ChangeChildObjectLayer(this Transform _self, int _layer)
    {
      _self.gameObject.layer = _layer;
      recursivChildObjectLayer(_self, _layer);
    }

    private static void recursivChildObjectLayer(Transform _transform, int _layer)
    {
      for (int index = 0; index < _transform.childCount; ++index)
      {
        Transform child = _transform.GetChild(index);
        child.gameObject.layer = _layer;
        recursivChildObjectLayer(child, _layer);
      }
    }

    [Conditional("UNITY_EDITOR")]
    public static void setGameObjectNameForEditor(this GameObject currentObj, string objName)
    {
    }

    [Conditional("UNITY_EDITOR")]
    public static void appendGameObjectNameForEditor(this GameObject currentObj, string objName)
    {
    }

    public static void GetAllChildren<TComponent>(
      this GameObject _currentObj,
      ref List<TComponent> _allChildren)
      where TComponent : Component
    {
      if (_currentObj.transform.childCount == 0)
        return;
      foreach (TComponent componentsInChild in _currentObj.GetComponentsInChildren<TComponent>())
      {
        if (!(componentsInChild.gameObject == _currentObj))
        {
          _allChildren.Add(componentsInChild);
          componentsInChild.gameObject.GetAllChildren(ref _allChildren);
        }
      }
    }

    /*public static string ToRemainingTimeText(this JMFOFKDCHEC.GNCIJGDDKIA _param)
    {
      int day = _param.day;
      int hour = _param.hour;
      int minute = _param.minute;
      return _param.isEnd ? eTextId.MINUTES_FOR_FORMAT.Format((object) 0) : (day > 0 ? eTextId.DAYS_FOR_FORMAT.Format((object) day) : (hour > 0 ? (minute > 0 ? eTextId.HOURS_MINUTES_FOR_FORMAT.Format((object) hour, (object) minute) : eTextId.HOURS_FOR_FORMAT.Format((object) hour)) : eTextId.MINUTES_FOR_FORMAT.Format((object) minute)));
    }

    public static string ToRemainingTimeTextZero(this JMFOFKDCHEC.GNCIJGDDKIA _param)
    {
      int num1 = _param.hour;
      int num2 = _param.minute;
      int num3 = _param.second;
      if (_param.isEnd)
      {
        num1 = 0;
        num2 = 0;
        num3 = 0;
      }
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(num1.ToString("D2"));
      stringBuilder.Append(":");
      stringBuilder.Append(num2.ToString("D2"));
      stringBuilder.Append(":");
      stringBuilder.Append(num3.ToString("D2"));
      return stringBuilder.ToString();
    }*/

    public static T[] ConvertToArray<T>(this HashSet<T> _source)
    {
      T[] objArray = new T[_source.Count];
      int num = 0;
      foreach (T obj in _source)
        objArray[num++] = obj;
      return objArray;
    }

    public static void ReStart(this Stopwatch _stopwatch)
    {
      _stopwatch.Stop();
      _stopwatch.Reset();
      _stopwatch.Start();
    }
  }
}
