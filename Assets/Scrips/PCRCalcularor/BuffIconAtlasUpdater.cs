#if UNITY_5_3_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AssetsTools.NET.Extra;
using UnityEngine;

namespace PCRCaculator.Battle
{
    /// <summary>
    /// 用游戏内的异常状态图集（atlasngui_atlasabnormalstate.unity3d）在线替换内置的 buff/debuff 图标。
    ///
    /// 链路：manifest 里本来就有这个包（a/atlasngui_atlasabnormalstate.unity3d），
    /// 所以走 ABExTool 的下载/校验/缓存，再在运行时用 Unity 自己解 DXT5 纹理，
    /// 结合包内 NGUI 的 mSprites 矩形切片，启动时把切片准备好（StartPrefetch），
    /// 进战斗时再合并进 BattleUIManager.buffDebuffIcons（ApplyTo / RequestUpdate）。
    ///
    /// 编号约定：eStateIconType 的 [Description("N")] 就是游戏里的 icon_status_N，
    /// 因此默认按编号一一对应；若个别图标编号对不上，可在
    /// {persistentDataPath}/buff-icon-map.txt 里写 "枚举索引=图集编号" 覆盖（每行一条，# 开头为注释）。
    /// 拿不到包 / 解析失败时静默保留内置图集，不影响战斗。
    /// </summary>
    public static class BuffIconAtlasUpdater
    {
        public const string BundleName = "atlasngui_atlasabnormalstate.unity3d";
        private const string TextureAssetName = "AtlasAbnormalState";
        private const string MapFileName = "buff-icon-map.txt";
        private const string SpriteNamePrefix = "icon_status_";

        /// <summary>状态号超过这个值的不按图标处理（例如 EVERLAST_BUFF = 100000 只是开关）。</summary>
        private const int MaxIconIndex = 512;

        private static bool started;
        private static bool applied;
        private static Texture2D atlasTexture;
        private static Dictionary<int, Sprite> preparedIcons;
        private static int preparedMaxIndex;

        /// <summary>是否已经成功把游戏内图标合并进 BattleUIManager。</summary>
        public static bool Applied => applied;

        /// <summary>启动阶段调用：后台把图集下载并切片好，等进战斗时直接用。</summary>
        public static void StartPrefetch(MonoBehaviour host)
        {
            if (started || host == null)
            {
                return;
            }

            started = true;
            host.StartCoroutine(PrepareRoutine());
        }

        /// <summary>进战斗时调用：开始预热（如果还没开始）+ 把已准备好的切片合并进去。</summary>
        public static void RequestUpdate(MonoBehaviour host)
        {
            StartPrefetch(host);
            ApplyTo(BattleUIManager.Instance);
        }

        /// <summary>把准备好的切片合并进指定 UI 管理器的 buffDebuffIcons。</summary>
        public static bool ApplyTo(BattleUIManager manager)
        {
            if (manager == null || preparedIcons == null || preparedIcons.Count == 0)
            {
                return false;
            }

            var sprites = manager.buffDebuffIcons;
            if (sprites == null || sprites.Count == 0)
            {
                return false;
            }

            Sprite placeholder = sprites[0];

            // 先按「数据里出现过的最大状态号」把列表铺满占位图，避免运行期索引越界刷「未设置…图标」错误
            int maxIndex = preparedMaxIndex;
            foreach (var pair in preparedIcons)
            {
                if (pair.Key > maxIndex)
                {
                    maxIndex = pair.Key;
                }
            }

            while (sprites.Count <= maxIndex)
            {
                sprites.Add(placeholder);
            }

            int replaced = 0;
            foreach (var pair in preparedIcons)
            {
                int index = pair.Key;
                if (index < 0 || index >= sprites.Count)
                {
                    continue;
                }

                sprites[index] = pair.Value;
                replaced++;
            }

            manager.buffDebuffIcons = sprites;
            applied = replaced > 0;
            if (applied)
            {
                Debug.Log($"[BuffIconAtlas] 已用游戏内图集替换 {replaced} 个状态图标");
            }

            return applied;
        }

        private static IEnumerator PrepareRoutine()
        {
            // 让出一帧，避免和场景加载 / 首帧 UI 初始化抢时间
            yield return null;

            byte[] raw = null;
            try
            {
                raw = ABExTool.GetCachedBundleBytes(BundleName);
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"[BuffIconAtlas] 读取 {BundleName} 失败: {exception.Message}");
            }

            if (raw == null || raw.Length == 0)
            {
                Debug.Log($"[BuffIconAtlas] 当前版本没有可用的 {BundleName}，继续使用内置 buff 图标");
                yield break;
            }

            Dictionary<int, SpriteRect> rects = null;
            try
            {
                rects = ReadSpriteRects(raw);
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"[BuffIconAtlas] 解析 {BundleName} 的 NGUI 切片失败: {exception.Message}");
            }

            if (rects == null || rects.Count == 0)
            {
                Debug.LogWarning($"[BuffIconAtlas] {BundleName} 里没有解析到任何 icon_status_* 切片，继续使用内置 buff 图标");
                yield break;
            }

            var request = AssetBundle.LoadFromMemoryAsync(raw);
            while (!request.isDone)
            {
                yield return null;
            }

            var bundle = request.assetBundle;
            if (bundle == null)
            {
                Debug.LogWarning($"[BuffIconAtlas] {BundleName} 载入失败，继续使用内置 buff 图标");
                yield break;
            }

            Texture2D texture = null;
            try
            {
                texture = bundle.LoadAsset<Texture2D>(TextureAssetName);
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"[BuffIconAtlas] 取纹理 {TextureAssetName} 失败: {exception.Message}");
            }

            if (texture == null)
            {
                bundle.Unload(true);
                Debug.LogWarning($"[BuffIconAtlas] {BundleName} 里没有 {TextureAssetName}，继续使用内置 buff 图标");
                yield break;
            }

            Dictionary<int, int> overrides = LoadOverrideMap();

            var icons = new Dictionary<int, Sprite>();
            int maxIndex = 0;
            foreach (var pair in rects)
            {
                int iconNumber = pair.Key;
                if (iconNumber < 0 || iconNumber > MaxIconIndex)
                {
                    continue;
                }

                // buff-icon-map.txt 里可以写 "枚举索引=图集编号" 做个别纠正
                if (overrides.TryGetValue(iconNumber, out int overriddenNumber) && rects.ContainsKey(overriddenNumber))
                {
                    iconNumber = overriddenNumber;
                }

                var rect = pair.Value;
                if (rect.Width <= 0 || rect.Height <= 0)
                {
                    continue;
                }

                // NGUI 的 y 自上而下，Unity 的 Rect 自左下角起算，需要翻一下
                var spriteRect = new Rect(rect.X, texture.height - rect.Y - rect.Height, rect.Width, rect.Height);
                Sprite sprite = Sprite.Create(texture, spriteRect, new Vector2(0.5f, 0.5f), 100f);
                sprite.name = SpriteNamePrefix + pair.Key;
                sprite.hideFlags = HideFlags.DontUnloadUnusedAsset;
                icons[iconNumber] = sprite;
                if (iconNumber > maxIndex)
                {
                    maxIndex = iconNumber;
                }
            }

            // 枚举里存在但图集里没有的编号，也把位置留出来（用占位图），免得上层索引越界报错
            foreach (var value in Enum.GetValues(typeof(eStateIconType)))
            {
                int number = (int)value;
                if (number > 0 && number <= MaxIconIndex && number > maxIndex)
                {
                    maxIndex = number;
                }
            }

            // Sprite 引用着纹理，保留引用避免被资源清理回收
            atlasTexture = texture;
            preparedIcons = icons;
            preparedMaxIndex = maxIndex;
            bundle.Unload(false);

            Debug.Log($"[BuffIconAtlas] 图集切片就绪: 可用 {icons.Count} 个（图集共 {rects.Count} 个切片），列表将铺到索引 {maxIndex}");

            // 如果此时战斗界面已经存在（例如从战斗内重载），立刻合并
            ApplyTo(BattleUIManager.Instance);
        }

        private struct SpriteRect
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
        }

        /// <summary>读包内 MonoBehaviour(mSprites) 里每个 icon_status_N 的矩形。</summary>
        private static Dictionary<int, SpriteRect> ReadSpriteRects(byte[] raw)
        {
            var result = new Dictionary<int, SpriteRect>();

            using var stream = new MemoryStream(raw);
            var assetsManager = new AssetsManager();
            try
            {
                var bundle = assetsManager.LoadBundleFile(stream, BundleName);
                if (bundle == null)
                {
                    return result;
                }

                var instance = assetsManager.LoadAssetsFileFromBundle(bundle, 0);
                if (instance == null)
                {
                    return result;
                }

                foreach (var assetInfo in instance.file.GetAssetsOfType(AssetClassID.MonoBehaviour))
                {
                    var baseField = assetsManager.GetBaseField(instance, assetInfo);
                    if (baseField == null)
                    {
                        continue;
                    }

                    var spritesField = baseField["mSprites"];
                    if (spritesField == null || spritesField.IsDummy)
                    {
                        continue;
                    }

                    var array = spritesField["Array"];
                    if (array == null || array.IsDummy)
                    {
                        continue;
                    }

                    foreach (var child in array)
                    {
                        var nameField = child["name"];
                        if (nameField == null || nameField.IsDummy)
                        {
                            continue;
                        }

                        int number = ParseIconNumber(nameField.AsString);
                        if (number < 0)
                        {
                            continue;
                        }

                        result[number] = new SpriteRect
                        {
                            X = child["x"].AsInt,
                            Y = child["y"].AsInt,
                            Width = child["width"].AsInt,
                            Height = child["height"].AsInt,
                        };
                    }
                }
            }
            finally
            {
                assetsManager.UnloadAll();
            }

            return result;
        }

        private static int ParseIconNumber(string name)
        {
            if (string.IsNullOrEmpty(name) || !name.StartsWith(SpriteNamePrefix, StringComparison.Ordinal))
            {
                return -1;
            }

            string tail = name.Substring(SpriteNamePrefix.Length);
            return int.TryParse(tail, NumberStyles.Integer, CultureInfo.InvariantCulture, out int number) ? number : -1;
        }

        private static Dictionary<int, int> LoadOverrideMap()
        {
            var result = new Dictionary<int, int>();
            try
            {
                string root = string.IsNullOrEmpty(ABExTool.persistentDataPath)
                    ? Application.persistentDataPath
                    : ABExTool.persistentDataPath;
                string path = Path.Combine(root, MapFileName);
                if (!File.Exists(path))
                {
                    return result;
                }

                foreach (string line in File.ReadAllLines(path))
                {
                    string text = line.Trim();
                    if (text.Length == 0 || text.StartsWith("#", StringComparison.Ordinal))
                    {
                        continue;
                    }

                    var parts = text.Split('=');
                    if (parts.Length != 2)
                    {
                        continue;
                    }

                    if (int.TryParse(parts[0].Trim(), out int index) &&
                        int.TryParse(parts[1].Trim(), out int number))
                    {
                        result[index] = number;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"[BuffIconAtlas] 读取 {MapFileName} 失败: {exception.Message}");
            }

            return result;
        }

    }
}
#endif

