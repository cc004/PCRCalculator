using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ObjectiveC;

namespace UnityEngine;

public static class IlEmitInstantiator
{
    private static ConcurrentCache<object, Func<GameObject>> instantiator = new ();

    // 递归版本
    private static void EmitValueInternal(ILGenerator il, object o, Type t, Dictionary<object, LocalBuilder> context)
    {
        if (o == null)
        {
            il.Emit(OpCodes.Ldnull);
            return;
        }

        if (o is UnityEngine.Object && context.TryGetValue(o, out var loc))
        {
            il.Emit(OpCodes.Ldloc, loc);
            return;
        }

        // ---------- 基础类型可以直接压栈 ----------
        if (t == typeof(int))
        {
            il.Emit(OpCodes.Ldc_I4, (int)o);
            return;
        }

        if (t == typeof(long))
        {
            il.Emit(OpCodes.Ldc_I8, (long)o);
            return;
        }

        if (t == typeof(float))
        {
            il.Emit(OpCodes.Ldc_R4, (float)o);
            return;
        }

        if (t == typeof(double))
        {
            il.Emit(OpCodes.Ldc_R8, (double)o);
            return;
        }

        if (t == typeof(string))
        {
            il.Emit(OpCodes.Ldstr, (string)o);
            return;
        }

        // ---------- bool ----------
        if (t == typeof(bool))
        {
            bool bv = (bool)o;
            il.Emit(bv ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0); // push 0/1
            il.Emit(OpCodes.Conv_U1); // convert to unsigned 1-byte (System.Boolean)
            return;
        }

        // ---------- short (Int16) ----------
        if (t == typeof(short))
        {
            short sv = (short)o;
            il.Emit(OpCodes.Ldc_I4, (int)sv); // push as int32
            il.Emit(OpCodes.Conv_I2); // convert to int16
            return;
        }

        // ---------- ushort (UInt16) ----------
        if (t == typeof(ushort))
        {
            ushort usv = (ushort)o;
            il.Emit(OpCodes.Ldc_I4, (int)usv); // push as int32
            il.Emit(OpCodes.Conv_U2); // convert to unsigned int16
            return;
        }

        // ---------- uint (UInt32) ----------
        if (t == typeof(uint))
        {
            uint uv = (uint)o;
            // ldc.i4 accepts int32; if value > int.MaxValue the unchecked cast will preserve bits.
            il.Emit(OpCodes.Ldc_I4, unchecked((int)uv));
            il.Emit(OpCodes.Conv_U4); // convert to unsigned int32
            return;
        }

        // ---------- ulong (UInt64) ----------
        if (t == typeof(ulong))
        {
            ulong ulv = (ulong)o;
            // ldc.i8 takes a Int64; use unchecked cast to keep the bitpattern
            il.Emit(OpCodes.Ldc_I8, unchecked((long)ulv));
            il.Emit(OpCodes.Conv_U8); // convert to unsigned int64
            return;
        }

        if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>))
        {
            Type elemType = t.GetGenericArguments()[0];
            var listCtor = t.GetConstructor(Type.EmptyTypes);
            if (listCtor == null)
                throw new Exception($"{t} 无无参构造，无法创建 List");

            // new List<T>()
            il.Emit(OpCodes.Newobj, listCtor);
            LocalBuilder listLocal = il.DeclareLocal(t);
            il.Emit(OpCodes.Stloc, listLocal);

            var list = (IList)o;
            var addMethod = t.GetMethod("Add");

            foreach (var item in list)
            {
                il.Emit(OpCodes.Ldloc, listLocal); // list
                EmitValueInternal(il, item, item?.GetType(), context); // deep copy of item
                il.Emit(OpCodes.Callvirt, addMethod); // list.Add(itemCopy)
            }

            il.Emit(OpCodes.Ldloc, listLocal);
            return;
        }

        if (t.IsArray)
        {
            Type elemType = t.GetElementType();
            Array arr = (Array)o;
            int len = arr.Length;

            // 生成长度常量
            il.Emit(OpCodes.Ldc_I4, len);

            // new T[len]
            il.Emit(OpCodes.Newarr, elemType);
            LocalBuilder arrLocal = il.DeclareLocal(t);
            il.Emit(OpCodes.Stloc, arrLocal);

            for (int i = 0; i < len; i++)
            {
                il.Emit(OpCodes.Ldloc, arrLocal); // array
                il.Emit(OpCodes.Ldc_I4, i); // index

                object elem = arr.GetValue(i);
                EmitValueInternal(il, elem, elem?.GetType(), context); // deep copy of element

                il.Emit(OpCodes.Stelem, elemType); // array[i] = elemCopy;
            }

            il.Emit(OpCodes.Ldloc, arrLocal);
            return;
        }

        // ---------- enum ----------
        if (t.IsEnum)
        {
            Type underlying = Enum.GetUnderlyingType(t);
            object raw = Convert.ChangeType(o, underlying);

            // 压入底层整数
            if (underlying == typeof(int))
                il.Emit(OpCodes.Ldc_I4, (int)raw);
            else if (underlying == typeof(short))
                il.Emit(OpCodes.Ldc_I4, (short)raw);
            else if (underlying == typeof(byte))
                il.Emit(OpCodes.Ldc_I4, (byte)raw);
            else if (underlying == typeof(long))
                il.Emit(OpCodes.Ldc_I8, (long)raw);
            else
                throw new NotSupportedException("暂不支持该枚举底层类型: " + underlying);

            // 转换到 enum 类型
            il.Emit(OpCodes.Conv_I4); // 如果底层是 int / short / byte
            // 若 underlying == long 用 Conv_I8

            return;
        }

        // ---------- struct（值类型） ----------
        if (t.IsValueType)
        {
            // 分配一个 struct 局部变量
            LocalBuilder local = il.DeclareLocal(t);

            // local = default(T)   (initobj)
            il.Emit(OpCodes.Ldloca, local);
            il.Emit(OpCodes.Initobj, t);

            // 填充所有序列化字段
            foreach (var f in t.GetAllSerializedFields())
            {
                object fv = f.GetValue(o);
                Type ft = f.FieldType;

                // local.field = deepCopy(fv)
                il.Emit(OpCodes.Ldloca, local);      // struct 地址（必须用 Ldloca 才能写入字段）
                EmitValueInternal(il, fv, fv?.GetType(), context);       // 压入字段值（递归）
                il.Emit(OpCodes.Stfld, f);           // 写入字段
            }

            // 返回 struct 值（装载到栈顶）
            il.Emit(OpCodes.Ldloc, local);
        }
        else
        {
            LocalBuilder local = il.DeclareLocal(t);

            // ---------- 引用类型（深拷贝） ----------
            // newobj .ctor()
            var ctor = t.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
                throw new Exception($"{t} 不存在无参构造，无法深拷贝");

            il.Emit(OpCodes.Newobj, ctor);


            il.Emit(OpCodes.Stloc, local);

            if (o is UnityEngine.GameObject)
                context[o] = local;

            // 获取所有需要复制的字段
            foreach (var f in t.GetAllSerializedFields())
            {
                object fv = f.GetValue(o);

                il.Emit(OpCodes.Ldloc, local);

                // 再把 fv 的值压栈（递归）
                EmitValueInternal(il, fv, fv?.GetType(), context);

                // 赋值
                il.Emit(OpCodes.Stfld, f);
            }

            // 最后把 local load 回栈顶
            il.Emit(OpCodes.Ldloc, local);
        }
    }
    public static void EmitValue(ILGenerator il, object o)
    {
        EmitValueInternal(il, o, o.GetType(), new Dictionary<object, LocalBuilder>());
    }

    private static Func<GameObject> BuildObjectInstantiator(GameObject original)
    {
        var dm = new DynamicMethod("ObjectInstantiator_" + Guid.NewGuid(), typeof(GameObject), Type.EmptyTypes, true);

        var il = dm.GetILGenerator();

        EmitValue(il, original);
        il.Emit(OpCodes.Ret);

        return dm.CreateDelegate<Func<GameObject>>();
    }

    public static GameObject Instantiate(GameObject original)
    {
        if (original == null) return null;
        if (!instantiator.TryGetValue(original, out var val))
        {
            val = BuildObjectInstantiator(original);
            instantiator[original] = val;
        }
        lock (val)
            return val();
    }
}