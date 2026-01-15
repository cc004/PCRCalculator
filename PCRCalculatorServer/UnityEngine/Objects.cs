// UnityEngine.Object
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[StructLayout(LayoutKind.Sequential)]
public class Object
{
    [SerializeField]
    private string m_Name;

    public string name
    {
        get => m_Name;
        set => m_Name = value;
    }

    public static UnityEngine.GameObject Instantiate(UnityEngine.GameObject original)
    {
        return IlEmitInstantiator.Instantiate(original) as GameObject;
    }

    public static GameObject Instantiate(UnityEngine.GameObject original, Transform parent, bool worldPositionStays = false)
    {
        GameObject result = Instantiate(original);
        result.transform.SetParent(parent, worldPositionStays);
        return result;
    }

    public static void Destroy(UnityEngine.Object obj)
    {
        if (obj is GameObject obj2)
        {
            var transform = obj2.GetComponent<Transform>();
            if (transform.parent != null)
                transform.SetParent(null);
        }
    }

    public static void DestroyImmediate(UnityEngine.Object obj)
    {
    }

    public static UnityEngine.Object[] FindObjectsOfType(Type type, bool includeInactive)
    {
        throw new NotImplementedException();
    }

    public static void DontDestroyOnLoad(UnityEngine.Object target) { }

    public static explicit operator bool(UnityEngine.Object obj)
    {
        return obj != null;
    }
    public static bool operator!(UnityEngine.Object obj)
    {
        return obj == null;
    }
    public static T FindObjectOfType<T>()
    {
        throw new NotImplementedException();
    }

}
