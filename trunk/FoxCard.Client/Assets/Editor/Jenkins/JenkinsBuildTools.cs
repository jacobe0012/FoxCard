using System;
using System.Reflection;
using Cysharp.Threading.Tasks;
using HybridCLR.Editor;
using HybridCLR.Editor.Commands;
using Main;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using YooAsset;
using YooAsset.Editor;
using BuildResult = UnityEditor.Build.Reporting.BuildResult;


public class JenkinsBuildTools
{
    [MenuItem("BuildTools/Build APK")]
    public static void Build()
    {
        // 解析命令行参数
        string[] args = System.Environment.GetCommandLineArgs();
        string productName = "Test";
        string version = "0.1.0";
        string cdnURL1 = "gsagsdg";
        string cdnURL2 = "sdgasg";
        //0:Apk  1:HotUpdatePatch
        int typeInt = 1;
        //0:Full  1:Incremental
        int buildTypeInt = 0;

        if (typeInt == 0)
        {
            //TODO:1.更新svn 解决冲突  2.运行代码清理 命名空间 3.Generate All 4.执行打包
            if (buildTypeInt == 0)
            {
                PrebuildCommand.GenerateAll();
            }

            //4.执行打包
            string[] scenePaths = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                scenePaths[i] = EditorBuildSettings.scenes[i].path;
                Debug.Log($"scenePaths {scenePaths[i]}");
            }


            BuildPlayerOptions opt = new BuildPlayerOptions();
            // 获取 Build Settings 中的默认场景列表

            opt.scenes = scenePaths;
            string parentFolderPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
            opt.locationPathName = parentFolderPath + $"/Builds/Android/{productName}.apk";
            opt.target = BuildTarget.Android;
            opt.options = BuildOptions.None;
            opt.targetGroup = BuildTargetGroup.Android;
            var buildReport = BuildPipeline.BuildPlayer(opt);
            if (buildReport.summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build Success!");
            }
            else
            {
                Debug.Log("Build Failure!");
            }
        }
        else
        {
            //TODO:1.更新svn 解决冲突 2.运行代码清理 命名空间 3.更新dlls 4.YooAsset打包热更资源
            //1.更新FMOD 和dlls 
            //BuildAssetsCommand.CopyFMODBanksToArtResources();
            BuildAssetsCommand.BuildAndCopyABAOTHotUpdateDlls();
            DateTime now = DateTime.Now;

            // 格式化为 "yyyy-MM-dd-HHmm" 格式
            version = now.ToString("yyyy-MM-dd-HHmm");
            //2.YooAsset打包热更资源
            ExecuteBuild(version, buildTypeInt);
        }


        void ExecuteBuild(string hotUpdateVersion, int buildTypeInt)
        {
            EBuildPipeline eBuildPipeline = EBuildPipeline.BuiltinBuildPipeline;
            string PackageName = "DefaultPackage";
            // var buildMode = AssetBundleBuilderSetting.GetPackageBuildMode(PackageName, eBuildPipeline);
            //
            var buildMode = buildTypeInt == 1 ? EBuildMode.IncrementalBuild : EBuildMode.ForceRebuild;
            // var fileNameStyle = AssetBundleBuilderSetting.GetPackageFileNameStyle(PackageName, eBuildPipeline);
            // var buildinFileCopyOption =
            //     AssetBundleBuilderSetting.GetPackageBuildinFileCopyOption(PackageName, eBuildPipeline);
            // var buildinFileCopyParams =
            //     AssetBundleBuilderSetting.GetPackageBuildinFileCopyParams(PackageName, eBuildPipeline);
            // var compressOption = AssetBundleBuilderSetting.GetPackageCompressOption(PackageName, eBuildPipeline);

            BuiltinBuildParameters buildParameters = new BuiltinBuildParameters();
            buildParameters.BuildOutputRoot = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot();
            buildParameters.BuildinFileRoot = AssetBundleBuilderHelper.GetStreamingAssetsRoot();
            buildParameters.BuildPipeline = EBuildPipeline.BuiltinBuildPipeline.ToString();
            buildParameters.BuildTarget = BuildTarget.WebGL;
            buildParameters.BuildMode = buildMode;
            buildParameters.PackageName = PackageName;
            buildParameters.PackageVersion = hotUpdateVersion;
            buildParameters.EnableSharePackRule = true;
            buildParameters.VerifyBuildingResult = true;
            buildParameters.FileNameStyle = EFileNameStyle.BundleName_HashName;
            buildParameters.BuildinFileCopyOption = EBuildinFileCopyOption.None;
            buildParameters.BuildinFileCopyParams = string.Empty;
            buildParameters.EncryptionServices = CreateEncryptionInstance(PackageName, eBuildPipeline);
            buildParameters.CompressOption = ECompressOption.LZ4;

            BuiltinBuildPipeline pipeline = new BuiltinBuildPipeline();
            var buildResult = pipeline.Run(buildParameters, true);
            if (buildResult.Success)
            {
                Debug.Log("Build Success!");
            }
            else
            {
                Debug.Log("Build Failure!");
            }
        }

        IEncryptionServices CreateEncryptionInstance(string PackageName, EBuildPipeline eBuildPipeline)
        {
            var encyptionClassName =
                AssetBundleBuilderSetting.GetPackageEncyptionClassName(PackageName, eBuildPipeline);
            var encryptionClassTypes = EditorTools.GetAssignableTypes(typeof(IEncryptionServices));
            var classType = encryptionClassTypes.Find(x => x.FullName.Equals(encyptionClassName));
            if (classType != null)
                return (IEncryptionServices)Activator.CreateInstance(classType);
            else
                return null;
        }
    }
}