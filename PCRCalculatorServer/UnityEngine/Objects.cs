// UnityEngine.Object
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Numerics;
using System.Reflection;
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

    private static object Instantiate(object original, Dictionary<object, object> context)
    {
        if (original == null) return null;

        var type = original.GetType();
        object result;

        if (type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(byte[]))
            return original;

        if (original is UnityEngine.Object)
        {
            if (context.TryGetValue(original, out var val))
                return val;
            result = Activator.CreateInstance(type);
            context[original] = result;
        }
        else
            result = Activator.CreateInstance(type);

        foreach (var field in type.GetAllSerializedFields())
        {
            var fieldType = field.FieldType;

            var src = field.GetValue(original);

            if (fieldType.IsGenericType &&
                fieldType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var newList = (IList)Activator.CreateInstance(fieldType);
                var srcList = (IList)src;

                foreach (var item in srcList)
                    newList.Add(Instantiate(item, context));

                field.SetValue(result, newList);
            }
            else if (fieldType.IsArray)
            {
                var srcList = (Array)src;
                var newList = Array.CreateInstance(fieldType.GetElementType(), srcList.Length);

                for (var i = 0; i < srcList.Length; ++i)
                    newList.SetValue(Instantiate(srcList.GetValue(i), context), i);

                field.SetValue(result, newList);
            }
            else
            {
                field.SetValue(result, Instantiate(src, context));
            }
        }

        return result;
    }

    public static UnityEngine.GameObject Instantiate(UnityEngine.GameObject original)
    {
        return Instantiate(original, new Dictionary<object, object>()) as GameObject;
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
    public T FindObjectOfType<T>()
    {
        throw new NotImplementedException();
    }

}
