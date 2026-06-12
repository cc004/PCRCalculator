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
        private BundleFileInstance bundleInst;
        private AssetsFileInstance impl;
        private Dictionary<string, List<long>> pathMap;
        private string name;

        private static Dictionary<string, AssetBundle> loadedBundle = new Dictionary<string, AssetBundle>();
        private static HashSet<string> serverCompactedBattleunitBundleNames = new();

        public static bool UseServerLightweightRootEffectPrefabs { get; set; }

        public static bool UseServerCompactBattleunitBundleData { get; set; }

        public static bool CaptureServerEffectPrefabAudit { get; set; }

        internal static int ServerCompactedBattleunitBundleCount
        {
            get
            {
#if ENABLE_THREADING
                lock (loaderLock)
                {
#endif
                    return serverCompactedBattleunitBundleNames.Count;
#if ENABLE_THREADING
                }
#endif
            }
        }

        internal static string[] GetServerCompactedBattleunitBundleNames(int sampleLimit)
        {
#if ENABLE_THREADING
            lock (loaderLock)
            {
#endif
                return serverCompactedBattleunitBundleNames
                    .OrderBy(item => item)
                    .Take(Math.Max(0, sampleLimit))
                    .ToArray();
#if ENABLE_THREADING
            }
#endif
        }

        internal static object GetServerEffectPrefabAuditSnapshot(int sampleLimit = 32)
        {
            return ObjectManager.GetEffectPrefabAuditSnapshot(sampleLimit);
        }

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
                    bundleInst = bunInst,
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
            return pathMap?.Keys ?? Enumerable.Empty<string>();
        }

        private AssetBundle[] dependencies;

        internal static bool TryCompactServerBattleunitBundleData(string bundleName)
        {
#if ENABLE_THREADING
            lock (loaderLock)
            {
#endif
                return TryCompactServerBattleunitBundleDataCore(bundleName);
#if ENABLE_THREADING
            }
#endif
        }

        internal static string[] TryCompactServerBattleunitDependencies(string ownerBundleName)
        {
#if ENABLE_THREADING
            lock (loaderLock)
            {
#endif
                if (!UseServerCompactBattleunitBundleData ||
                    string.IsNullOrEmpty(ownerBundleName) ||
                    !loadedBundle.TryGetValue(ownerBundleName, out var owner) ||
                    owner.dependencies == null)
                {
                    return Array.Empty<string>();
                }

                var dependencyNames = owner.dependencies
                    .Select(dependency => dependency?.name)
                    .Where(name => name != null && IsServerCompactBattleunitBundleName(name))
                    .Distinct()
                    .ToArray();
                var compactedNames = new List<string>();
                foreach (var dependencyName in dependencyNames)
                {
                    if (TryCompactServerBattleunitBundleDataCore(dependencyName))
                    {
                        compactedNames.Add(dependencyName);
                    }
                }

                return compactedNames.ToArray();
#if ENABLE_THREADING
            }
#endif
        }

        private static bool TryCompactServerBattleunitBundleDataCore(string bundleName)
        {
            if (!UseServerCompactBattleunitBundleData ||
                string.IsNullOrEmpty(bundleName) ||
                !IsServerCompactBattleunitBundleName(bundleName) ||
                !loadedBundle.TryGetValue(bundleName, out var bundle))
            {
                return false;
            }

            foreach (var loaded in loadedBundle.Values.ToArray())
            {
                loaded?.ClearDependencyReference(bundleName, bundle);
            }

            loadedBundle.Remove(bundleName);
            bundle.ReleaseParsedBundleData();
            serverCompactedBattleunitBundleNames.Add(bundleName);
            return true;
        }

        private static bool IsServerCompactBattleunitBundleName(string bundleName)
        {
            return bundleName.StartsWith("all_battleunit_", StringComparison.Ordinal) &&
                   bundleName.EndsWith(".unity3d", StringComparison.Ordinal);
        }

        private void ClearDependencyReference(string bundleName, AssetBundle bundle)
        {
            if (dependencies == null)
            {
                return;
            }

            for (var i = 0; i < dependencies.Length; i++)
            {
                var dependency = dependencies[i];
                if (ReferenceEquals(dependency, bundle) ||
                    string.Equals(dependency?.name, bundleName, StringComparison.Ordinal))
                {
                    dependencies[i] = null;
                }
            }
        }

        private void ReleaseParsedBundleData()
        {
            var assetsFile = impl;
            var bundleFile = bundleInst ?? impl?.parentBundle;

            impl = null;
            pathMap = null;
            dependencies = null;
            bundleInst = null;

            if (assetsFile != null)
            {
                try
                {
                    am.UnloadAssetsFile(assetsFile);
                }
                catch
                {
                }
            }

            if (bundleFile != null)
            {
                try
                {
                    am.UnloadBundleFile(bundleFile);
                }
                catch
                {
                }
            }
        }

        private void LoadDeps()
        {
            if (this.dependencies != null) return;
            if (impl == null) throw new InvalidOperationException($"asset bundle {name} has been compacted");


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
            private static Dictionary<(string bundleName, long pathId), GameObject> lightweightEffectPrefabPool = new();
            private static HashSet<(string bundleName, long pathId)> effectPrefabReferenceKeys = new();
            private static Dictionary<Type, Action<object, Dictionary<string, AssetTypeValueField>, Func<(long fileId, long pathId), Type, object>>> serializeFieldCache = new();

            private sealed class FullEffectPrefabReference
            {
            }

            private sealed class RootOnlyLightweightEffectPrefabReference
            {
            }

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
                        var useLightweightEffectPrefab =
                            type == typeof(NormalSkillEffect) &&
                            fieldType == typeof(GameObject) &&
                            (name == nameof(NormalSkillEffect.Prefab) ||
                             name == nameof(NormalSkillEffect.PrefabLeft));
                        result += (obj, fields, resolver) =>
                        {
                            if (fields.TryGetValue(name, out var val))
                            {
                                var resolvedType =
                                    UseServerLightweightRootEffectPrefabs && useLightweightEffectPrefab
                                        ? typeof(RootOnlyLightweightEffectPrefabReference)
                                        : CaptureServerEffectPrefabAudit && useLightweightEffectPrefab
                                            ? typeof(FullEffectPrefabReference)
                                            : fieldType;
                                field.SetValue(obj, resolver((val["m_FileID"].AsLong, val["m_PathID"].AsLong), resolvedType));
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

                serializeFieldCache[type] = result;
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

            private static bool TryGetChild(AssetTypeValueField root, string name, out AssetTypeValueField child)
            {
                child = root?.Children?.FirstOrDefault(c => c.FieldName == name);
                return child != null;
            }

            private static string ReadStringField(AssetTypeValueField root, string name, string fallback = "")
            {
                return TryGetChild(root, name, out var child) ? child.AsString : fallback;
            }

            private static float ReadFloatField(AssetTypeValueField root, string name, float fallback = 0)
            {
                return TryGetChild(root, name, out var child) ? child.AsFloat : fallback;
            }

            private static int ReadIntField(AssetTypeValueField root, string name, int fallback = 0)
            {
                return TryGetChild(root, name, out var child) ? child.AsInt : fallback;
            }

            private static Vector3 ReadVector3(AssetTypeValueField root, Vector3 fallback)
            {
                if (root == null)
                {
                    return fallback;
                }

                return new Vector3(
                    ReadFloatField(root, "x", fallback.x),
                    ReadFloatField(root, "y", fallback.y),
                    ReadFloatField(root, "z", fallback.z));
            }

            private static Quaternion ReadQuaternion(AssetTypeValueField root, Quaternion fallback)
            {
                if (root == null)
                {
                    return fallback;
                }

                return new Quaternion(
                    ReadFloatField(root, "x", fallback.x),
                    ReadFloatField(root, "y", fallback.y),
                    ReadFloatField(root, "z", fallback.z),
                    ReadFloatField(root, "w", fallback.w));
            }

            private static void ApplyLightweightTransformFields(Transform transform, AssetTypeValueField root)
            {
                if (transform == null || root == null)
                {
                    return;
                }

                if (TryGetChild(root, "m_LocalPosition", out var localPosition))
                {
                    transform.localPosition = ReadVector3(localPosition, Vector3.zero);
                }

                if (TryGetChild(root, "m_LocalScale", out var localScale))
                {
                    transform.localScale = ReadVector3(localScale, Vector3.one);
                }
                else
                {
                    transform.localScale = Vector3.one;
                }

                if (TryGetChild(root, "m_LocalRotation", out var localRotation))
                {
                    transform.localRotation = ReadQuaternion(localRotation, Quaternion.identity);
                }
            }

            private static void ApplyLightweightFirearmFields(FirearmCtrl firearm, AssetTypeValueField root)
            {
                if (firearm == null || root == null)
                {
                    return;
                }

                firearm.name = ReadStringField(root, "m_Name", firearm.name);
                firearm.IsRepeat = true;
                SetInstanceField(firearm, typeof(FirearmCtrl), "HitDelay", ReadFloatField(root, "HitDelay", 0.1f));
                SetInstanceField(firearm, typeof(FirearmCtrl), "MoveRate", ReadFloatField(root, "MoveRate", 4f));
                SetInstanceField(firearm, typeof(FirearmCtrl), "duration", ReadFloatField(root, "duration", 0.8f));
                SetEnumInstanceField(firearm, typeof(FirearmCtrl), "MoveType", ReadIntField(root, "MoveType", 0));
                SetInstanceField(firearm, typeof(FirearmCtrl), "startRotate", ReadFloatField(root, "startRotate", 60f));
                SetInstanceField(firearm, typeof(FirearmCtrl), "endRotate", ReadFloatField(root, "endRotate", -60f));
                SetInstanceField(firearm, typeof(FirearmCtrl), "ColliderBox", ReadBoundsField(root, "ColliderBox"));
            }

            private static void ApplyLightweightRootComponentFields(Component component, GameObject gameObject, AssetTypeValueField root)
            {
                if (component == null || root == null)
                {
                    return;
                }

                try
                {
                    DeserializeSingle(root, component, (_, _) => null);
                }
                catch
                {
                    // Some visual-only component fields have unsupported shapes in the server shim.
                }

                component.gameObject = gameObject;
            }

            private static Bounds ReadBoundsField(AssetTypeValueField root, string name)
            {
                if (!TryGetChild(root, name, out var bounds))
                {
                    return new Bounds();
                }

                var center = TryGetChild(bounds, "m_Center", out var centerField)
                    ? ReadVector3(centerField, Vector3.zero)
                    : Vector3.zero;
                var extents = TryGetChild(bounds, "m_Extent", out var extentField)
                    ? ReadVector3(extentField, Vector3.zero)
                    : Vector3.zero;
                return new Bounds(center, extents);
            }

            private static void SetInstanceField(object target, Type ownerType, string name, object value)
            {
                ownerType.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    ?.SetValue(target, value);
            }

            private static void SetEnumInstanceField(object target, Type ownerType, string name, int rawValue)
            {
                var field = ownerType.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (field == null)
                {
                    return;
                }

                field.SetValue(target, Enum.ToObject(field.FieldType, rawValue));
            }

            public static object GetEffectPrefabAuditSnapshot(int sampleLimit)
            {
                sampleLimit = Math.Max(0, sampleLimit);
                var keys = effectPrefabReferenceKeys
                    .OrderBy(key => key.bundleName)
                    .ThenBy(key => key.pathId)
                    .ToArray();

                var rawRootComponentCounts = new Dictionary<string, int>();
                var rawAllComponentCounts = new Dictionary<string, int>();
                var lightComponentCounts = new Dictionary<string, int>();
                var fullComponentCounts = new Dictionary<string, int>();
                var skippedRootComponentCounts = new Dictionary<string, int>();
                var skippedAllComponentCounts = new Dictionary<string, int>();
                var rawRootSignatureCounts = new Dictionary<string, int>();
                var rawAllSignatureCounts = new Dictionary<string, int>();
                var samples = new List<Dictionary<string, object>>();

                var missingBundles = 0;
                var rawErrors = 0;
                var fullLoadedPrefabs = 0;
                var lightLoadedPrefabs = 0;
                var rawRootSkillEffectPrefabs = 0;
                var rawRootFirearmPrefabs = 0;
                var rawRootNonFirearmSkillEffectPrefabs = 0;
                var lightMissingRawRootSkillEffectPrefabs = 0;
                var runtimeMissingRawRootSkillEffectPrefabs = 0;

                foreach (var key in keys)
                {
                    var sample = new Dictionary<string, object>
                    {
                        ["key"] = $"({key.bundleName}, {key.pathId})"
                    };

                    if (!loadedBundle.TryGetValue(key.bundleName, out var bundle))
                    {
                        missingBundles++;
                        sample["status"] = "missing_bundle";
                        AddAuditSample(samples, sample, sampleLimit);
                        continue;
                    }

                    try
                    {
                        var rawRootComponents = CollectRawComponentTypeNames(
                            bundle,
                            key.pathId,
                            includeChildren: false,
                            out var rawRootGameObjects,
                            out var rootErrors);
                        var rawAllComponents = CollectRawComponentTypeNames(
                            bundle,
                            key.pathId,
                            includeChildren: true,
                            out var rawAllGameObjects,
                            out var allErrors);
                        rawErrors += rootErrors + allErrors;

                        var rawRootComponentSet = rawRootComponents.ToHashSet();
                        var rawAllComponentSet = rawAllComponents.ToHashSet();
                        AddCounts(rawRootComponentCounts, rawRootComponents);
                        AddCounts(rawAllComponentCounts, rawAllComponents);
                        Increment(rawRootSignatureCounts, FormatSignature(rawRootComponents));
                        Increment(rawAllSignatureCounts, FormatSignature(rawAllComponents));

                        var lightComponents = Array.Empty<string>();
                        if (lightweightEffectPrefabPool.TryGetValue(key, out var lightGameObject))
                        {
                            lightLoadedPrefabs++;
                            lightComponents = CollectRuntimeComponentTypeNames(lightGameObject, includeChildren: true).ToArray();
                            AddCounts(lightComponentCounts, lightComponents);
                        }

                        var fullComponents = Array.Empty<string>();
                        if (pool.TryGetValue(key, out var fullObject) && fullObject is GameObject fullGameObject)
                        {
                            fullLoadedPrefabs++;
                            fullComponents = CollectRuntimeComponentTypeNames(fullGameObject, includeChildren: true).ToArray();
                            AddCounts(fullComponentCounts, fullComponents);
                        }

                        var lightComponentSet = lightComponents.ToHashSet();
                        AddCounts(skippedRootComponentCounts, rawRootComponents.Where(type => !lightComponentSet.Contains(type)));
                        AddCounts(skippedAllComponentCounts, rawAllComponents.Where(type => !lightComponentSet.Contains(type)));

                        var rawRootHasSkillEffect = rawRootComponentSet.Any(IsSkillEffectTypeName);
                        var rawRootHasFirearm = rawRootComponentSet.Any(IsFirearmTypeName);
                        if (rawRootHasSkillEffect)
                        {
                            rawRootSkillEffectPrefabs++;
                        }

                        if (rawRootHasFirearm)
                        {
                            rawRootFirearmPrefabs++;
                        }

                        if (rawRootHasSkillEffect && !rawRootHasFirearm)
                        {
                            rawRootNonFirearmSkillEffectPrefabs++;
                        }

                        if (rawRootHasSkillEffect && !lightComponentSet.Any(IsSkillEffectTypeName))
                        {
                            lightMissingRawRootSkillEffectPrefabs++;
                        }

                        var runtimeComponentSet = lightComponentSet
                            .Union(fullComponents)
                            .ToHashSet();
                        if (rawRootHasSkillEffect && !runtimeComponentSet.Any(IsSkillEffectTypeName))
                        {
                            runtimeMissingRawRootSkillEffectPrefabs++;
                        }

                        sample["status"] = "ok";
                        sample["name"] = ReadStringField(am.GetBaseField(bundle.impl, key.pathId), "m_Name");
                        sample["raw_root_game_objects"] = rawRootGameObjects;
                        sample["raw_all_game_objects"] = rawAllGameObjects;
                        sample["raw_root_components"] = rawRootComponentSet.OrderBy(type => type).ToArray();
                        sample["raw_all_component_counts"] = TopCounts(CountTypes(rawAllComponents), 12);
                        sample["light_components"] = lightComponentSet.OrderBy(type => type).ToArray();
                        sample["full_components"] = fullComponents.ToHashSet().OrderBy(type => type).ToArray();
                        sample["raw_root_has_skill_effect"] = rawRootHasSkillEffect;
                        sample["raw_root_has_firearm"] = rawRootHasFirearm;
                    }
                    catch (Exception ex)
                    {
                        rawErrors++;
                        sample["status"] = "error";
                        sample["error_type"] = ex.GetType().Name;
                        sample["error"] = ex.Message;
                    }

                    AddAuditSample(samples, sample, sampleLimit);
                }

                return new Dictionary<string, object>
                {
                    ["referenced_prefabs"] = keys.Length,
                    ["full_pool_count"] = pool.Count,
                    ["lightweight_pool_count"] = lightweightEffectPrefabPool.Count,
                    ["full_loaded_referenced_prefabs"] = fullLoadedPrefabs,
                    ["light_loaded_referenced_prefabs"] = lightLoadedPrefabs,
                    ["missing_bundles"] = missingBundles,
                    ["raw_errors"] = rawErrors,
                    ["raw_root_skill_effect_prefabs"] = rawRootSkillEffectPrefabs,
                    ["raw_root_firearm_prefabs"] = rawRootFirearmPrefabs,
                    ["raw_root_non_firearm_skill_effect_prefabs"] = rawRootNonFirearmSkillEffectPrefabs,
                    ["light_missing_raw_root_skill_effect_prefabs"] = lightMissingRawRootSkillEffectPrefabs,
                    ["runtime_missing_raw_root_skill_effect_prefabs"] = runtimeMissingRawRootSkillEffectPrefabs,
                    ["raw_root_components_by_type"] = TopCounts(rawRootComponentCounts, 32),
                    ["raw_all_components_by_type"] = TopCounts(rawAllComponentCounts, 32),
                    ["light_components_by_type"] = TopCounts(lightComponentCounts, 32),
                    ["full_components_by_type"] = TopCounts(fullComponentCounts, 32),
                    ["skipped_root_components_by_type"] = TopCounts(skippedRootComponentCounts, 32),
                    ["skipped_all_components_by_type"] = TopCounts(skippedAllComponentCounts, 32),
                    ["raw_root_signatures"] = TopCounts(rawRootSignatureCounts, 16),
                    ["raw_all_signatures"] = TopCounts(rawAllSignatureCounts, 16),
                    ["samples"] = samples.ToArray()
                };
            }

            private static void AddAuditSample(List<Dictionary<string, object>> samples, Dictionary<string, object> sample, int sampleLimit)
            {
                if (samples.Count < sampleLimit)
                {
                    samples.Add(sample);
                }
            }

            private static List<string> CollectRawComponentTypeNames(
                AssetBundle bundle,
                long gameObjectPathId,
                bool includeChildren,
                out int gameObjectCount,
                out int errorCount)
            {
                var componentTypes = new List<string>();
                var visitedGameObjects = new HashSet<(string bundleName, long pathId)>();
                var visitedTransforms = new HashSet<(string bundleName, long pathId)>();
                var localGameObjectCount = 0;
                var localErrorCount = 0;

                void VisitGameObject(AssetBundle context, long pathId)
                {
                    if (context == null || pathId == 0)
                    {
                        return;
                    }

                    var key = (context.name, pathId);
                    if (!visitedGameObjects.Add(key))
                    {
                        return;
                    }

                    if (visitedGameObjects.Count > 4096)
                    {
                        localErrorCount++;
                        return;
                    }

                    var gameObjectField = am.GetBaseField(context.impl, pathId);
                    if (gameObjectField.TypeName != "GameObject")
                    {
                        localErrorCount++;
                        return;
                    }

                    localGameObjectCount++;
                    if (!TryGetChild(gameObjectField, "m_Component", out var componentsField) ||
                        !TryGetChild(componentsField, "Array", out var componentArray))
                    {
                        return;
                    }

                    foreach (var componentEntry in componentArray.Children)
                    {
                        if (!TryGetChild(componentEntry, "component", out var componentPtr) ||
                            !TryReadPPtr(componentPtr, out var fileId, out var componentPathId) ||
                            componentPathId == 0)
                        {
                            continue;
                        }

                        var componentContext = ResolveDependencyContext(context, fileId);
                        if (componentContext == null)
                        {
                            localErrorCount++;
                            continue;
                        }

                        var componentField = am.GetBaseField(componentContext.impl, componentPathId);
                        componentTypes.Add(ResolveRawComponentTypeName(componentContext, componentField));

                        if (includeChildren && componentField.TypeName == "Transform")
                        {
                            VisitTransformChildren(componentContext, componentPathId);
                        }
                    }
                }

                void VisitTransformChildren(AssetBundle context, long transformPathId)
                {
                    var transformKey = (context.name, transformPathId);
                    if (!visitedTransforms.Add(transformKey))
                    {
                        return;
                    }

                    var transformField = am.GetBaseField(context.impl, transformPathId);
                    if (!TryGetChild(transformField, "m_Children", out var childrenField) ||
                        !TryGetChild(childrenField, "Array", out var childrenArray))
                    {
                        return;
                    }

                    foreach (var childTransformPtr in childrenArray.Children)
                    {
                        if (!TryReadPPtr(childTransformPtr, out var childTransformFileId, out var childTransformPathId) ||
                            childTransformPathId == 0)
                        {
                            continue;
                        }

                        var childTransformContext = ResolveDependencyContext(context, childTransformFileId);
                        if (childTransformContext == null)
                        {
                            localErrorCount++;
                            continue;
                        }

                        var childTransformField = am.GetBaseField(childTransformContext.impl, childTransformPathId);
                        if (!TryGetChild(childTransformField, "m_GameObject", out var childGameObjectPtr) ||
                            !TryReadPPtr(childGameObjectPtr, out var childGameObjectFileId, out var childGameObjectPathId) ||
                            childGameObjectPathId == 0)
                        {
                            continue;
                        }

                        var childGameObjectContext = ResolveDependencyContext(childTransformContext, childGameObjectFileId);
                        if (childGameObjectContext == null)
                        {
                            localErrorCount++;
                            continue;
                        }

                        VisitGameObject(childGameObjectContext, childGameObjectPathId);
                    }
                }

                VisitGameObject(bundle, gameObjectPathId);
                gameObjectCount = localGameObjectCount;
                errorCount = localErrorCount;
                return componentTypes;
            }

            private static bool TryReadPPtr(AssetTypeValueField field, out long fileId, out long pathId)
            {
                fileId = 0;
                pathId = 0;
                if (field == null)
                {
                    return false;
                }

                if (TryGetChild(field, "m_FileID", out var fileIdField) &&
                    TryGetChild(field, "m_PathID", out var pathIdField))
                {
                    fileId = fileIdField.AsLong;
                    pathId = pathIdField.AsLong;
                    return true;
                }

                if (TryGetChild(field, "data", out var dataField))
                {
                    return TryReadPPtr(dataField, out fileId, out pathId);
                }

                return false;
            }

            private static AssetBundle ResolveDependencyContext(AssetBundle context, long fileId)
            {
                if (context == null)
                {
                    return null;
                }

                if (fileId == 0)
                {
                    return context;
                }

                context.LoadDeps();
                return fileId > 0 && fileId < context.dependencies.Length
                    ? context.dependencies[fileId]
                    : null;
            }

            private static string ResolveRawComponentTypeName(AssetBundle context, AssetTypeValueField field)
            {
                if (field == null)
                {
                    return "null";
                }

                return field.TypeName switch
                {
                    "GameObject" => typeof(GameObject).FullName,
                    "MonoBehaviour" => GetScriptTypeName(context, field["m_Script"]),
                    "Transform" => typeof(Transform).FullName,
                    "TextAsset" => typeof(TextAsset).FullName,
                    "ParticleSystem" => typeof(ParticleSystem).FullName,
                    "ParticleSystemRenderer" => typeof(ParticleSystemRenderer).FullName,
                    "Animator" => typeof(Animator).FullName,
                    _ => field.TypeName
                };
            }

            private static string GetScriptTypeName(AssetBundle context, AssetTypeValueField scriptPtr)
            {
                if (scriptPtr == null ||
                    !TryReadPPtr(scriptPtr, out var fileId, out var pathId) ||
                    pathId == 0)
                {
                    return typeof(MonoBehaviour).FullName;
                }

                var scriptContext = ResolveDependencyContext(context, fileId);
                if (scriptContext == null)
                {
                    return "missing_script_context";
                }

                var scriptField = am.GetBaseField(scriptContext.impl, pathId);
                var scriptNamespace = scriptField["m_Namespace"].AsString;
                var scriptClassName = scriptField["m_ClassName"].AsString;
                return string.IsNullOrEmpty(scriptNamespace)
                    ? scriptClassName
                    : $"{scriptNamespace}.{scriptClassName}";
            }

            private static List<string> CollectRuntimeComponentTypeNames(GameObject gameObject, bool includeChildren)
            {
                var componentTypes = new List<string>();
                var visited = new HashSet<GameObject>();

                void Visit(GameObject current)
                {
                    if (current == null || !visited.Add(current))
                    {
                        return;
                    }

                    componentTypes.AddRange(GetRuntimeRootComponentTypeNames(current));
                    if (!includeChildren)
                    {
                        return;
                    }

                    var transform = current.GetComponent<Transform>();
                    if (transform == null)
                    {
                        return;
                    }

                    foreach (var child in transform)
                    {
                        if (child is Transform childTransform)
                        {
                            Visit(childTransform.gameObject);
                        }
                    }
                }

                Visit(gameObject);
                return componentTypes;
            }

            private static IEnumerable<string> GetRuntimeRootComponentTypeNames(GameObject gameObject)
            {
                var componentField = typeof(GameObject).GetField("m_Component", BindingFlags.Instance | BindingFlags.NonPublic);
                if (componentField?.GetValue(gameObject) is not IEnumerable components)
                {
                    yield break;
                }

                foreach (var pair in components)
                {
                    var pairField = pair.GetType().GetField("component", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (pairField?.GetValue(pair) is Component component)
                    {
                        yield return component.GetType().FullName;
                    }
                }
            }

            private static Dictionary<string, int> CountTypes(IEnumerable<string> types)
            {
                var counts = new Dictionary<string, int>();
                AddCounts(counts, types);
                return counts;
            }

            private static void AddCounts(Dictionary<string, int> counts, IEnumerable<string> types)
            {
                foreach (var type in types)
                {
                    Increment(counts, type);
                }
            }

            private static void Increment(Dictionary<string, int> counts, string key)
            {
                key ??= "null";
                counts.TryGetValue(key, out var value);
                counts[key] = value + 1;
            }

            private static object[] TopCounts(Dictionary<string, int> counts, int limit)
            {
                return counts
                    .OrderByDescending(item => item.Value)
                    .ThenBy(item => item.Key)
                    .Take(limit)
                    .Select(item => new Dictionary<string, object>
                    {
                        ["type"] = item.Key,
                        ["count"] = item.Value
                    })
                    .ToArray();
            }

            private static string FormatSignature(IEnumerable<string> componentTypes)
            {
                return string.Join(";", componentTypes.OrderBy(type => type));
            }

            private static bool IsSkillEffectTypeName(string typeName)
            {
                var type = Type.GetType(typeName);
                return type != null && typeof(SkillEffectCtrl).IsAssignableFrom(type);
            }

            private static bool IsFirearmTypeName(string typeName)
            {
                var type = Type.GetType(typeName);
                return type != null && typeof(FirearmCtrl).IsAssignableFrom(type);
            }

            public static T Deserialize<T>(long rootPath, AssetBundle context) where T : UnityEngine.Object
            {
                var deserializeQueue = new Queue<(AssetTypeValueField root, object obj, AssetBundle context)>();

                Type GetTypeFromMonoscript(AssetBundle scriptContext, AssetTypeValueField root)
                {
                    if (root["m_PathID"].AsLong == 0) return typeof(MonoBehaviour);

                    var fileId = root["m_FileID"].AsLong;
                    root = am.GetBaseField((fileId == 0 ? scriptContext : scriptContext.dependencies[fileId]).impl,
                        root["m_PathID"].AsLong);
                    var scriptType = root["m_Namespace"].AsString + "." + root["m_ClassName"].AsString;
                    var typeInst = Type.GetType(scriptType);
                    //  if (typeInst == null)
                    //     Console.WriteLine("script type load failed: " + scriptType);
                    return typeInst;
                }

                Type ResolveAssetType(AssetBundle assetContext, AssetTypeValueField field)
                {
                    return field.TypeName switch
                    {
                        "GameObject" => typeof(GameObject),
                        "MonoBehaviour" => GetTypeFromMonoscript(assetContext, field["m_Script"]),
                        "Transform" => typeof(Transform),
                        "TextAsset" => typeof(TextAsset),
                        "ParticleSystem" => typeof(ParticleSystem),
                        "ParticleSystemRenderer" => typeof(ParticleSystemRenderer),
                        "Animator" => typeof(Animator),
                        _ => throw new NotImplementedException()
                    };
                }

                object CreateRootOnlyLightweightEffectPrefab(AssetBundle prefabContext, long pathId)
                {
                    var key = (prefabContext.name, pathId);
                    effectPrefabReferenceKeys.Add(key);
                    if (lightweightEffectPrefabPool.TryGetValue(key, out var cached))
                    {
                        return cached;
                    }

                    var field = am.GetBaseField(prefabContext.impl, pathId);
                    if (field.TypeName != "GameObject")
                    {
                        return EnqueueObject(prefabContext, pathId, typeof(GameObject));
                    }

                    var gameObject = new GameObject(ReadStringField(field, "m_Name"));
                    lightweightEffectPrefabPool[key] = gameObject;

                    if (!TryGetChild(field, "m_Component", out var componentsField) ||
                        !TryGetChild(componentsField, "Array", out var componentArray))
                    {
                        return gameObject;
                    }

                    foreach (var componentEntry in componentArray.Children)
                    {
                        if (!TryGetChild(componentEntry, "component", out var componentPtr))
                        {
                            continue;
                        }

                        var fileId = componentPtr["m_FileID"].AsLong;
                        var componentPathId = componentPtr["m_PathID"].AsLong;
                        if (componentPathId == 0)
                        {
                            continue;
                        }

                        var componentContext = ResolveDependencyContext(prefabContext, fileId);
                        if (componentContext == null)
                        {
                            continue;
                        }

                        var componentField = am.GetBaseField(componentContext.impl, componentPathId);
                        var componentType = ResolveAssetType(componentContext, componentField);
                        if (componentType == typeof(Transform))
                        {
                            ApplyLightweightTransformFields(gameObject.transform, componentField);
                        }
                        else if (componentType != null &&
                                 typeof(FirearmCtrl).IsAssignableFrom(componentType))
                        {
                            var firearm = gameObject.AddComponent(componentType) as FirearmCtrl;
                            ApplyLightweightFirearmFields(firearm, componentField);
                        }
                        else if (componentType != null &&
                                 typeof(SkillEffectCtrl).IsAssignableFrom(componentType))
                        {
                            var component = gameObject.AddComponent(componentType);
                            ApplyLightweightRootComponentFields(component, gameObject, componentField);
                        }
                    }

                    return gameObject;
                }

                object EnqueueObject(AssetBundle context, long pathId, Type type)
                {
                    if (type == typeof(RootOnlyLightweightEffectPrefabReference))
                    {
                        return CreateRootOnlyLightweightEffectPrefab(context, pathId);
                    }

                    if (type == typeof(FullEffectPrefabReference))
                    {
                        effectPrefabReferenceKeys.Add((context.name, pathId));
                        type = typeof(GameObject);
                    }

                    var field = am.GetBaseField(context.impl, pathId);
                    type = ResolveAssetType(context, field);
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
