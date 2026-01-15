// UnityEngine.Component
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[RequiredByNativeCode]
public class Component : UnityEngine.Object
{
    [SerializeField]
    private GameObject m_GameObject;

    public Transform transform => gameObject.transform;

    public GameObject gameObject
    {
        get => m_GameObject;
        set => m_GameObject = value;
    }

    public unsafe T GetComponent<T>() where T : Component
    {
        return gameObject.GetComponent<T>();
    }

    public T GetComponentInChildren<T>()
    {
        throw new NotImplementedException();
    }

    public T GetComponentInParent<T>()
    {
        throw new NotImplementedException();
    }

}
