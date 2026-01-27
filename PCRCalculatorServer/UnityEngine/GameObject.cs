// UnityEngine.GameObject
using AssetsTools.NET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine;

public sealed class GameObject : UnityEngine.Object
{
    [SerializeField] private bool m_IsActive = true;
    [SerializeField] private List<ComponentPair> m_Component = new();

    public Transform transform => GetComponent<Transform>() ?? AddComponent<Transform>();

    public bool activeSelf
    {
        get => m_IsActive;
        set => m_IsActive = value;
    }

    [Serializable]
    private class ComponentPair
    {
        public Component component;
    }

    public T GetComponent<T>() where T : Component
    {
        foreach (var pair in m_Component)
            if (pair.component is T t)
                return t;
        return null;
    }

    public T GetComponentInChildren<T>()
    {
        throw new NotImplementedException();
    }

    public T[] GetComponentsInChildren<T>(bool includeInactive)
    {
        throw new NotImplementedException();
    }

    public T[] GetComponentsInChildren<T>()
    {
        throw new NotImplementedException();
    }

    public bool TryGetComponent<T>(out T component)
    {
        throw new NotImplementedException();
    }

    public Component AddComponent(Component component)
    {
        m_Component.Add(new ComponentPair { component = component });
        if (component.gameObject != null)
            throw new InvalidOperationException();
        component.gameObject = this;
        return component;
    }

    public Component AddComponent(Type componentType)
    {
        var component = Activator.CreateInstance(componentType) as Component;
        return AddComponent(component);
    }

    public T AddComponent<T>() where T : Component
    {
        return AddComponent(typeof(T)) as T;
    }

    public void SetActive(bool value)
    {
        activeSelf = value;
    }

    public GameObject(string name)
    {
        this.name = name;
    }

    public GameObject() : this(string.Empty)
    {
    }
}
