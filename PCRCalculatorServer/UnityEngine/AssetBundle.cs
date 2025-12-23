using AssetsTools.NET;
using AssetsTools.NET.Extra;
using Elements;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PCRCaculator;
using static SQLite4Unity3d.SQLite3;

namespace UnityEngine
{
    public class AssetBundle
    {
        private static AssetsManager am = new AssetsManager();
        private AssetsFileInstance impl;
        private Dictionary<string, List<long>> pathMap;
        private string name;

        private static Dictionary<string, AssetBundle> loadedBundle = new Dictionary<string, AssetBundle>();
#if ENABLE_THREADING
        private static object loaderLock = new();
#endif
        static AssetBundle()
        {
            am.LoadClassPackage(Path.Combine(AppContext.BaseDirectory, "classdata.tpk"));
            am.LoadClassDatabaseFromPackage("2021.3.21f1");
        }

        internal static AssetBundle LoadFromMemory(byte[] bytes)
        {
#if ENABLE_THREADING
            lock (loaderLock)
            {
#endif
                if (bytes == null) return null;
                var bunInst = am.LoadBundleFile(new MemoryStream(bytes), Guid.NewGuid().ToString());
                var impl = am.LoadAssetsFileFromBundle(bunInst, 0, false);
                var info = am.GetBaseField(impl, impl.file.GetAssetsOfType(AssetClassID.AssetBundle).Single());
                var container = info["m_Container"]["Array"];
                var map = new Dictionary<string, List<long>>();
                foreach (var pair in container)
                {
                    var path = pair["first"].AsString;
                    var pathId = pair["second"]["asset"]["m_PathID"].AsLong;
                    if (!map.ContainsKey(path))
                        map[path] = new List<long>();
                    map[path].Add(pathId);
                }

                var result = new AssetBundle()
                {
                    impl = impl,
                    pathMap = map,
                    name = info["m_Name"].AsString
                };
                loadedBundle[result.name] = result;
                return result;

#if ENABLE_THREADING
            }
#endif
        }

        internal static void UnloadAllAssetBundles(bool v)
        {
        }

        internal IEnumerable<string> GetAllAssetNames()
        {
            return pathMap.Keys;
        }

        private AssetBundle[] dependencies;

        private void LoadDeps()
        {
            if (this.dependencies != null) return;


            var dependencies = new List<AssetBundle>();
            dependencies.Add(this);

            var cabCache = new Dictionary<string, AssetBundle>();

            var info = am.GetBaseField(impl, impl.file.GetAssetsOfType(AssetClassID.AssetBundle).Single());

            foreach (var val in info["m_Dependencies"]["Array"])
            {
                var depName = val.AsString;
                if (loadedBundle.TryGetValue(depName, out var ab))
                {
                    cabCache[$"archive:/{ab.impl.name}/{ab.impl.name}"] = ab;
                }
                else
                {
                    ab = ABExTool.TryGetAssetBundleByName(depName, false);
                    if (ab == null)
                        Console.WriteLine($"assetbundle loader: dependency {depName} not found for bundle {name}");
                    else
                        cabCache[$"archive:/{ab.impl.name}/{ab.impl.name}"] = ab;
                }
            }

            foreach (var ext in impl.file.Metadata.Externals)
            {
                if (cabCache.TryGetValue(ext.PathName, out var val))
                {
                    dependencies.Add(val);
                }
                else
                {
                    // Console.WriteLine($"assetbundle loader: external {ext.PathName} not found for bundle {result.name}");
                    dependencies.Add(null);
                }
            }

            this.dependencies = dependencies.ToArray();

        }
        internal T LoadAsset<T>(string path_0) where T : Object
        {

#if ENABLE_THREADING
            lock (loaderLock)
            {
#endif
                LoadDeps();
                return ObjectManager.Deserialize<T>(pathMap[path_0].Single(), this);
#if ENABLE_THREADING
            }
#endif
        }

        internal void Unload(bool v)
        {
        }


        private static class ObjectManager
        {
            private static Dictionary<(string bundleName, long pathId), object> pool = new();
            private static Dictionary<Type, Action<object, Dictionary<string, AssetTypeValueField>, Func<(long fileId, long pathId), Type, object>>> serializeFieldCache = new();

            private static Action<object, Dictionary<string, AssetTypeValueField>, Func<(long fileId, long pathId), Type, object>>
                GetFieldProcessor(Type type)
            {
                if (serializeFieldCache.TryGetValue(type, out var val)) return val;

                Action<object, Dictionary<string, AssetTypeValueField>, Func<(long fileId, long pathId), Type, object>> result = null;

                foreach (var field in type.GetAllSerializedFields())
                {
                    var name = field.Name;
                    var fieldType = field.FieldType;

                    if (fieldType == typeof(int))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsInt);
                        };
                    else if (fieldType == typeof(uint))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsUInt);
                        };
                    else if (fieldType == typeof(long))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsLong);
                        };
                    else if (fieldType == typeof(ulong))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsULong);
                        };
                    else if (fieldType == typeof(string))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsString);
                        };
                    else if (fieldType == typeof(byte))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsByte);
                        };
                    else if (fieldType == typeof(sbyte))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsSByte);
                        };
                    else if (fieldType == typeof(short))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsShort);
                        };
                    else if (fieldType == typeof(ushort))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsUShort);
                        };
                    else if (fieldType == typeof(int))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsInt);
                        };
                    else if (fieldType == typeof(float))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsFloat);
                        };
                    else if (fieldType == typeof(double))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsDouble);
                        };
                    else if (fieldType == typeof(bool))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsBool);
                        };
                    else if (fieldType == typeof(byte[]))
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                                field.SetValue(obj, val.AsByteArray);
                        };
                    else if (fieldType.IsEnum)
                    {
                        Type underlying = Enum.GetUnderlyingType(fieldType);
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                            {
                                object raw = Convert.ChangeType(val.AsLong, underlying);
                                field.SetValue(obj, raw);
                            }
                        };

                    }
                    else if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var elementType = fieldType.GenericTypeArguments[0];
                        if (elementType.IsValueType)
                        {
                            result += (obj, fields, resolver) =>
                            {
                                if (fields.TryGetValue(name, out var val))
                                {
                                    var result = (IList)Activator.CreateInstance(fieldType);
                                    foreach (var child in val["Array"].Children)
                                    {
                                        result.Add(DeserializePrimitive(child, elementType));
                                    }

                                    field.SetValue(obj, result);
                                }
                            };
                        }
                        else
                        {
                            result += (obj, fields, resolver) =>
                            {
                                if (fields.TryGetValue(name, out var val))
                                {
                                    var result = (IList)Activator.CreateInstance(fieldType);
                                    foreach (var child in val["Array"].Children)
                                    {
                                        var o = Activator.CreateInstance(elementType);
                                        DeserializeSingle(child, o, resolver);
                                        result.Add(o);
                                    }

                                    field.SetValue(obj, result);
                                }
                            };
                        }
                    }
                    else if (fieldType.IsArray)
                    {
                        var elementType = fieldType.GetElementType();
                        if (elementType.IsValueType)
                        {
                            result += (obj, fields, resolver) =>
                            {
                                if (fields.TryGetValue(name, out var val))
                                {
                                    var arr = val["Array"].Children;
                                    var result = Array.CreateInstance(elementType, arr.Count);
                                    for (var i = 0; i < arr.Count; i++)
                                    {
                                        result.SetValue(DeserializePrimitive(arr[i], elementType), i);
                                    }

                                    field.SetValue(obj, result);
                                }
                            };
                        }
                        else
                        {
                            result += (obj, fields, resolver) =>
                            {
                                if (fields.TryGetValue(name, out var val))
                                {
                                    var arr = val["Array"].Children;
                                    var result = Array.CreateInstance(elementType, arr.Count);
                                    for (var i = 0; i < arr.Count; i++)
                                    {
                                        var o = Activator.CreateInstance(elementType);
                                        DeserializeSingle(arr[i], o, resolver);
                                        result.SetValue(o, i);
                                    }

                                    field.SetValue(obj, result);
                                }
                            };
                        }
                    }
                    else if (fieldType.IsSubclassOf(typeof(UnityEngine.Object)))
                    {
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                            {
                                field.SetValue(obj, resolver((val["m_FileID"].AsLong, val["m_PathID"].AsLong), fieldType));
                            }
                        };
                    }
                    else
                    {
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                            {
                                var o = Activator.CreateInstance(fieldType);
                                DeserializeSingle(val, o, resolver);
                                field.SetValue(obj, o);
                            }
                        };
                    }
                }

                return result;
            }

            private static object DeserializePrimitive(AssetTypeValueField child, Type elementType)
            {
                if (elementType == typeof(int))
                    return child.AsInt;
                if (elementType == typeof(uint))
                    return child.AsUInt;
                if (elementType == typeof(long))
                    return child.AsLong;
                if (elementType == typeof(ulong))
                    return child.AsULong;
                if (elementType == typeof(short))
                    return child.AsShort;
                if (elementType == typeof(ushort))
                    return child.AsUShort;
                if (elementType == typeof(byte))
                    return child.AsByte;
                if (elementType == typeof(sbyte))
                    return child.AsSByte;
                if (elementType == typeof(float))
                    return child.AsFloat;
                if (elementType == typeof(double))
                    return child.AsDouble;
                if (elementType == typeof(bool))
                    return child.AsBool;
                if (elementType == typeof(string))
                    return child.AsString;
                if (elementType.IsEnum)
                {
                    Type underlying = Enum.GetUnderlyingType(elementType);
                    object raw = Convert.ChangeType(child.AsLong, underlying);
                    return Enum.ToObject(elementType, raw);
                }
                throw new NotImplementedException();
            }

            private static void DeserializeSingle(AssetTypeValueField root, object result, Func<(long fileId, long pathId), Type, object> resolver)
            {
                var processor = GetFieldProcessor(result.GetType());
                processor?.Invoke(result, root.Children.ToDictionary(c => c.FieldName), resolver);
            }

            public static T Deserialize<T>(long rootPath, AssetBundle context) where T : UnityEngine.Object
            {
                var deserializeQueue = new Queue<(AssetTypeValueField root, object obj, AssetBundle context)>();

                object EnqueueObject(AssetBundle context, long pathId, Type type)
                {
                    Type GetTypeFromMonoscript(AssetTypeValueField root)
                    {
                        if (root["m_PathID"].AsLong == 0) return typeof(MonoBehaviour);

                        var fileId = root["m_FileID"].AsLong;
                        root = am.GetBaseField((fileId == 0 ? context : context.dependencies[fileId]).impl,
                            root["m_PathID"].AsLong);
                        var scriptType = root["m_Namespace"].AsString + "." + root["m_ClassName"].AsString;
                        var typeInst = Type.GetType(scriptType);
                        //  if (typeInst == null)
                        //     Console.WriteLine("script type load failed: " + scriptType);
                        return typeInst;
                    }

                    var field = am.GetBaseField(context.impl, pathId);
                    type = field.TypeName switch
                    {
                        "GameObject" => typeof(GameObject),
                        "MonoBehaviour" => GetTypeFromMonoscript(field["m_Script"]),
                        "Transform" => typeof(Transform),
                        "TextAsset" => typeof(TextAsset),
                        "ParticleSystem" => typeof(ParticleSystem),
                        "ParticleSystemRenderer" => typeof(ParticleSystemRenderer),
                        "Animator" => typeof(Animator),
                        _ => throw new NotImplementedException()
                    };
                    if (type == null)
                    {
                        pool[(context.name, pathId)] = null;
                        return null;
                    }

                    var obj = Activator.CreateInstance(type);
                    pool[(context.name, pathId)] = obj;
                    deserializeQueue.Enqueue((field, obj, context));
                    return obj;
                }

                if (pool.TryGetValue((context.name, rootPath), out var v)) return v as T;
                var result = EnqueueObject(context, rootPath, typeof(T));

                while (deserializeQueue.TryDequeue(out var pair))
                {
                    var t = pair.context;
                    DeserializeSingle(pair.root, pair.obj, (p, type) =>
                    {
                        if (p.pathId == 0) return null;
                        var ctx = p.fileId == 0 ? t : t.dependencies[p.fileId];
                        if (ctx == null) return null;
                        if (pool.TryGetValue((ctx.name, p.pathId), out var val)) return val;

                        return EnqueueObject(ctx, p.pathId, type);

                    });
                }

                return result as T;
            }


        }
    }

}
