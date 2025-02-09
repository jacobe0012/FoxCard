﻿using HybridCLR.Editor.Commands;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XFramework;

namespace HybridCLR.Editor
{
    public static class BuildAssetsCommand
    {
        public static string HybridCLRBuildCacheDir => Application.dataPath + "/HybridCLRBuildCache";

        public static string AssetBundleOutputDir => $"{HybridCLRBuildCacheDir}/AssetBundleOutput";

        public static string AssetBundleSourceDataTempDir => $"{HybridCLRBuildCacheDir}/AssetBundleSourceData";


        public static string GetAssetBundleOutputDirByTarget(BuildTarget target)
        {
            return $"{AssetBundleOutputDir}/{target}";
        }

        public static string GetAssetBundleTempDirByTarget(BuildTarget target)
        {
            return $"{AssetBundleSourceDataTempDir}/{target}";
        }

        public static string ToRelativeAssetPath(string s)
        {
            return s.Substring(s.IndexOf("Assets/"));
        }

        /// <summary>
        /// 将HotFix.dll和HotUpdatePrefab.prefab打入common包.
        /// 将HotUpdateScene.unity打入scene包.
        /// </summary>
        /// <param name="tempDir"></param>
        /// <param name="outputDir"></param>
        /// <param name="target"></param>
        private static void BuildAssetBundles(string tempDir, string outputDir, BuildTarget target)
        {
            Directory.CreateDirectory(tempDir);
            Directory.CreateDirectory(outputDir);

            List<AssetBundleBuild> abs = new List<AssetBundleBuild>();

            {
                var prefabAssets = new List<string>();
                string testPrefab = $"{Application.dataPath}/Prefabs/HotUpdatePrefab.prefab";
                prefabAssets.Add(testPrefab);
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                abs.Add(new AssetBundleBuild
                {
                    assetBundleName = "prefabs",
                    assetNames = prefabAssets.Select(s => ToRelativeAssetPath(s)).ToArray(),
                });
            }

            BuildPipeline.BuildAssetBundles(outputDir, abs.ToArray(), BuildAssetBundleOptions.None, target);
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        public static void BuildAssetBundleByTarget(BuildTarget target)
        {
            BuildAssetBundles(GetAssetBundleTempDirByTarget(target), GetAssetBundleOutputDirByTarget(target), target);
        }

        [MenuItem("HybridCLR/Build/CopyHotUpdateAndAOTsToTargetPath")]
        public static void BuildAndCopyABAOTHotUpdateDlls()
        {
            BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
            //BuildAssetBundleByTarget(target);
            CompileDllCommand.CompileDll(target);
            CopyABAOTHotUpdateDlls(target);
            AssetDatabase.Refresh();
        }

        public static void CopyABAOTHotUpdateDlls(BuildTarget target)
        {
            //CopyAssetBundlesToStreamingAssets(target);
            CopyAOTAssembliesToDllDataPath();
            CopyHotUpdateAssembliesToDllDataPath();
        }

        [MenuItem("HybridCLR/Build/CopyFMODBanksToArtResources")]
        public static void CopyFMODBanksToArtResources()
        {
            string banksSrcDir = $"{Application.dataPath}/{Application.productName}/FMOD/Banks/Desktop";
            string banksDstDir = $"{Application.dataPath}/{Application.productName}/Art_Resources/AudioClips/Banks";
            foreach (var bank in XFramework.AudioManager.banks)
            {
                string dllPath = $"{banksSrcDir}/{bank}";
                string dllBytesPath = $"{banksDstDir}/{bank}.bytes";
                File.Copy(dllPath, dllBytesPath, true);
                Debug.Log($"[CopyFMODBanksToArtResources] copy banks {dllPath} -> {dllBytesPath}");
            }

            AssetDatabase.Refresh();
        }

        public static void CopyHotUpdateAssembliesToDllDataPath()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;

            string hotfixDllSrcDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
            string hotfixAssembliesDstDir = $"{Application.dataPath}/{Application.productName}/HotFixDlls";
            foreach (var dll in SettingsUtil.HotUpdateAssemblyFilesExcludePreserved)
            {
                string dllPath = $"{hotfixDllSrcDir}/{dll}";
                string dllBytesPath = $"{hotfixAssembliesDstDir}/{dll}.bytes";
                File.Copy(dllPath, dllBytesPath, true);
                Debug.Log($"[CopyHotUpdateAssembliesToStreamingAssets] copy hotfix dll {dllPath} -> {dllBytesPath}");
            }
        }

        public static void CopyAOTAssembliesToDllDataPath()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            string aotAssembliesSrcDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
            string aotAssembliesDstDir = $"{Application.dataPath}/{Application.productName}/HotFixDlls";

            foreach (var dll in SettingsUtil.AOTAssemblyNames)
            {
                string srcDllPath = $"{aotAssembliesSrcDir}/{dll}.dll";
                if (!File.Exists(srcDllPath))
                {
                    Debug.LogError(
                        $"ab中添加AOT补充元数据dll:{srcDllPath} 时发生错误,文件不存在。裁剪后的AOT dll在BuildPlayer时才能生成，因此需要你先构建一次游戏App后再打包。");
                    continue;
                }

                string dllBytesPath = $"{aotAssembliesDstDir}/{dll}.dll.bytes";
                File.Copy(srcDllPath, dllBytesPath, true);
                Debug.Log($"[CopyAOTAssembliesToStreamingAssets] copy AOT dll {srcDllPath} -> {dllBytesPath}");
            }
        }

        //[MenuItem("HybridCLR/Build/BuildAssetbundle")]
        public static void BuildSceneAssetBundleActiveBuildTargetExcludeAOT()
        {
            BuildAssetBundleByTarget(EditorUserBuildSettings.activeBuildTarget);
        }


        public static void CopyAssetBundlesToStreamingAssets(BuildTarget target)
        {
            string streamingAssetPathDst = Application.streamingAssetsPath;
            Directory.CreateDirectory(streamingAssetPathDst);
            string outputDir = GetAssetBundleOutputDirByTarget(target);
            var abs = new string[] { "prefabs" };
            foreach (var ab in abs)
            {
                string srcAb = ToRelativeAssetPath($"{outputDir}/{ab}");
                string dstAb = ToRelativeAssetPath($"{streamingAssetPathDst}/{ab}");
                Debug.Log($"[CopyAssetBundlesToStreamingAssets] copy assetbundle {srcAb} -> {dstAb}");
                AssetDatabase.CopyAsset(srcAb, dstAb);
            }
        }
    }
}