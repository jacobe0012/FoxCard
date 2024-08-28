using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace XFramework.Editor
{
    public class CreateSpriteAtlas : EditorWindow
    {
        private const string SavePath = "Assets/Res/SpriteAtlas";

        [MenuItem("Tools/CreateSpriteAtals")]
        public static void CreateUniformBuilding()
        {
            Object[] arr = Selection.GetFiltered<Object>(SelectionMode.Assets);
            foreach (var item in arr)
            {
                CreateSpriteAtlasEditor(item);
            }
        }

        static void CreateSpriteAtlasEditor(Object srcObj)
        {
            var targetFilePath = AssetDatabase.GetAssetPath(srcObj);
            if (targetFilePath == null || targetFilePath.Length <= 0)
            {
                return;
            }

            SpriteAtlas atlas = new SpriteAtlas();

            SpriteAtlasPackingSettings packSetting = new SpriteAtlasPackingSettings()
            {
                blockOffset = 1,
                enableRotation = false,
                enableTightPacking = false,
                padding = 2,
            };
            atlas.SetPackingSettings(packSetting);

            SpriteAtlasTextureSettings textureSetting = new SpriteAtlasTextureSettings()
            {
                readable = false,
                generateMipMaps = false,
                sRGB = true,
                filterMode = FilterMode.Bilinear,
            };
            atlas.SetTextureSettings(textureSetting);

            TextureImporterPlatformSettings platformSetting = new TextureImporterPlatformSettings()
            {
                maxTextureSize = 2048,
                format = TextureImporterFormat.Automatic,
                crunchedCompression = true,
                textureCompression = TextureImporterCompression.Compressed,
                compressionQuality = 50,
            };
            atlas.SetPlatformSettings(platformSetting);
            atlas.SetIncludeInBuild(true);

            var strs = targetFilePath.Split('/');
            var savePath = Path.Combine(SavePath, strs[strs.Length - 1]);

            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);

            savePath = savePath + ".spriteatlas";
            Debug.Log($"{savePath} ");

            AssetDatabase.CreateAsset(atlas, savePath);

            //DirectoryInfo dir = new DirectoryInfo(_targetFilePath);
            //FileInfo[] files = dir.GetFiles("*.png");
            //foreach (FileInfo file in files)
            //{
            //    atlas.Add(new[] { AssetDatabase.LoadAssetAtPath<Sprite>($"{_targetFilePath}/{file.Name}") });
            //}


            Object obj = AssetDatabase.LoadAssetAtPath(targetFilePath, typeof(Object));
            atlas.Add(new[] { obj });

            AssetDatabase.SaveAssets();
            AssetDatabase.OpenAsset(atlas);
        }
    }
}

