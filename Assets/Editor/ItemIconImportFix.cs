using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 把 Assets/Resources/item 下的道具图标统一导入为 Sprite（Single），
/// 并为尚未生成 .meta 的图片自动补齐导入设置。
/// 用法：菜单 PCR Build/Fix Item Icon Import Settings，或命令行
///   Unity.exe -batchmode -quit -projectPath &lt;项目&gt; -executeMethod ItemIconImportFix.FixAll
/// </summary>
public static class ItemIconImportFix
{
    private const string ItemFolder = "Assets/Resources/item";
    private const int MaxTextureSize = 2048;

    [MenuItem("PCR Build/Fix Item Icon Import Settings")]
    public static void FixAll()
    {
        // 先把磁盘上新加进来的 png 导入一次（这一步会生成缺失的 .meta）
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

        var guids = AssetDatabase.FindAssets("t:Texture2D", new[] { ItemFolder });
        var reimport = new List<string>();
        int scanned = 0;

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(path)) continue;
            if (!path.StartsWith(ItemFolder, StringComparison.OrdinalIgnoreCase)) continue;
            if (!path.EndsWith(".png", StringComparison.OrdinalIgnoreCase)) continue;
            scanned++;

            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null) continue;

            bool dirty = false;
            if (importer.textureType != TextureImporterType.Sprite) { importer.textureType = TextureImporterType.Sprite; dirty = true; }
            if (importer.spriteImportMode != SpriteImportMode.Single) { importer.spriteImportMode = SpriteImportMode.Single; dirty = true; }
            if (importer.mipmapEnabled) { importer.mipmapEnabled = false; dirty = true; }
            if (!importer.alphaIsTransparency) { importer.alphaIsTransparency = true; dirty = true; }
            if (importer.maxTextureSize != MaxTextureSize) { importer.maxTextureSize = MaxTextureSize; dirty = true; }
            if (importer.filterMode != FilterMode.Bilinear) { importer.filterMode = FilterMode.Bilinear; dirty = true; }
            if (importer.isReadable) { importer.isReadable = false; dirty = true; }
            if (importer.spritePixelsPerUnit != 100f) { importer.spritePixelsPerUnit = 100f; dirty = true; }
            if (dirty) reimport.Add(path);
        }

        foreach (var path in reimport)
        {
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // 再校验一遍：确认每个 png 都能拿到 Sprite 对象
        var missing = new List<string>();
        foreach (var guid in AssetDatabase.FindAssets("t:Texture2D", new[] { ItemFolder }))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(path) || !path.StartsWith(ItemFolder, StringComparison.OrdinalIgnoreCase)) continue;
            if (!path.EndsWith(".png", StringComparison.OrdinalIgnoreCase)) continue;
            if (AssetDatabase.LoadAssetAtPath<Sprite>(path) == null) missing.Add(path);
        }

        Debug.Log($"[ItemIconImport] scanned={scanned}, reimported={reimport.Count}, notSprite={missing.Count}");
        foreach (var path in missing)
        {
            Debug.LogWarning($"[ItemIconImport] 仍不是 Sprite 资源: {path}");
        }
    }
}
