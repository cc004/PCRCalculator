// UnityEngine.Debug
using ActionParameterSerializer.Actions;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using static SQLite4Unity3d.SQLite3;

namespace UnityEngine;

public class Debug
{
    public static void Log(object message)
    {
        Console.WriteLine(message);
    }

    public static void LogError(object message)
    {
        Console.WriteLine(message);
    }

    public static void LogWarning(object message)
    {
        Console.WriteLine(message);
    }

    public static void LogWarning(object message, UnityEngine.Object context)
    {
        Console.WriteLine(message);
    }
}
