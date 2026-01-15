// UnityEngine.MonoBehaviour
using Elements;
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Elements.Battle;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

public class MonoBehaviour : Behaviour
{
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        CoroutineRunner.AppendCoroutine(routine);
        return null;
    }

    public void StopCoroutine(IEnumerator routine)
    {
        throw new NotImplementedException();
    }
}
