// UnityEngine.Transform
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

public class Transform : Component, IEnumerable
{
    [SerializeField]
    private Quaternion m_LocalRotation;
    [SerializeField]
    private Vector3 m_LocalPosition;
    [SerializeField]
    private Vector3 m_LocalScale;
    [SerializeField]
    private List<Transform> m_Children = new List<Transform>();
    [SerializeField]
    private Transform m_Father;

    public Vector3 position
    {
        get => parent == null ? localPosition : parent.position + Vector3.Scale(localPosition, parent.lossyScale);
        set => localPosition = parent == null ? value : new Vector3(
            (value.x - parent.position.x) / parent.lossyScale.x,
            (value.y - parent.position.y) / parent.lossyScale.y,
            (value.z - parent.position.z) / parent.lossyScale.z
        );
    }

    public Vector3 localPosition
    {
        get => m_LocalPosition;
        set => m_LocalPosition = value;
    }

    public Vector3 eulerAngles
    {
        get
        {
            return rotation.eulerAngles;
        }
        set
        {
            rotation = Quaternion.Euler(value);
        }
    }

    public Vector3 localEulerAngles
    {
        get
        {
            return localRotation.eulerAngles;
        }
        set
        {
            localRotation = Quaternion.Euler(value);
        }
    }

    public Quaternion rotation { get; set; }

    public Quaternion localRotation
    {
        get => m_LocalRotation;
        set => m_LocalRotation = value;
    }

    public Vector3 localScale
    {
        get => m_LocalScale;
        set => m_LocalScale = value;
    }

    public struct Matrix4x4
    {
        public float m00;
    }

    public Matrix4x4 localToWorldMatrix => new Matrix4x4
    {
        m00 = lossyScale.x,
    };

    public Transform parent
    {
        get => m_Father;
        set => m_Father = value;
    }

    public int childCount => m_Children.Count;

    public Vector3 lossyScale => parent == null ? localScale : Vector3.Scale(localScale, parent.lossyScale);

    public void SetParent(Transform parent, bool worldPositionStays = true)
    {
        m_Father?.RemoveChild(this);
        m_Father = parent;
        parent?.m_Children.Add(this);
    }

    private void RemoveChild(Transform child)
    {
        m_Children.Remove(child);
    }
    public Vector3 TransformPoint(Vector3 localPoint)
    {
        // localPoint → apply scale
        Vector3 scaled = new Vector3(
            localPoint.x * localScale.x,
            localPoint.y * localScale.y,
            localPoint.z * localScale.z
        );

        // apply rotation
        Vector3 rotated = rotation * scaled; // rotation is Quaternion

        // apply translation
        return position + rotated;
    }

    public Vector3 InverseTransformPoint(Vector3 position)
    {
        throw new NotImplementedException();
    }

    public void SetAsLastSibling()
    {
        throw new NotImplementedException();
    }

    public Transform Find(string n)
    {
        throw new NotImplementedException();
    }

    public IEnumerator GetEnumerator()
    {
        return m_Children.ToArray().GetEnumerator();
    }

    internal void Translate(Vector3 vector3)
    {
        throw new NotImplementedException();
    }

    internal Transform GetChild(int v)
    {
        return m_Children[v];
    }
}

public struct Rect { public float width, height;
    internal float xMin;
    internal float yMin;
    private int v1;
    private int v2;
    private object width1;
    private object height1;

    public Rect(int v1, int v2, object width1, object height1) : this()
    {
        this.v1 = v1;
        this.v2 = v2;
        this.width1 = width1;
        this.height1 = height1;
    }
}
public class RectTransform : Transform
{
    public enum Axis { Horizontal, Vertical }
    internal Rect rect;
    internal Vector2 anchoredPosition;
    internal Vector2 sizeDelta;
    internal Vector2 anchorMin;
    internal Vector2 anchorMax;
    internal Vector2 offsetMin;
    internal Vector2 offsetMax;
    internal object pivot;

    internal void SetSizeWithCurrentAnchors(object horizontal, float maxX)
    {
        throw new NotImplementedException();
    }
}
