// UnityEngine.Behaviour
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine;

public class Behaviour : Component
{
    [SerializeField] private bool m_Enabled = true;
    public bool enabled
    {

        get => m_Enabled;

        set => m_Enabled = value;
    }

    public bool isActiveAndEnabled => enabled && gameObject.activeSelf;
}
