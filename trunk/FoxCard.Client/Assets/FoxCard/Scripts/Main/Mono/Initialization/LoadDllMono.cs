//---------------------------------------------------------------------
// JiYuStudio
// Author: 格伦
// Time: 2023-07-12 12:15:10
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;

namespace Main
{
    //cdn资源加载初始化&&热更初始化脚本，放到AOT层
    public class LoadDllMono : MonoBehaviour
    {
        // 资源系统运行模式
        private EPlayMode PlayMode = EPlayMode.HostPlayMode;

        private bool localTest = false;

        //public static bool isStandAlone = false;

        //MyCDN
        //CDN地址
        //https://cdn.jsdelivr.net/gh/jacobe0012/FoxCardCdn
        private string DefaultHostServer = "https://gleen-test-0012.oss-cn-beijing.aliyuncs.com";

        private string localDefaultHostServer = "http://192.168.31.12/Packages";

        //
        // private string FallbackHostServer = "https://gleen-test-0012.oss-cn-beijing.aliyuncs.com/Dev/Android";
        //public string DefaultHostServer = "http://192.168.2.16/Packages/";

        private string FallbackHostServer = "http://192.168.31.12/Packages/";


        //弹窗对象,此对象当前为AOT层中的预制体对象，不放入热更新
        private GameObject tx;

        //热更新dll的列表，Yooasset中不需要带后缀
        public static List<string> HotDllNames { get; } = new List<string>()
        {
            "FoxCard.HotFix_Logic.dll",
            "FoxCard.HotFix_UI.dll"
        };

        public static List<System.Reflection.Assembly> AssList { get; } = new List<System.Reflection.Assembly>();

        //补充元数据dll的列表，Yooasset中不需要带后缀
        // public static List<string> AOTMetaAssemblyNames { get; } = new List<string>()
        // {
        //     "mscorlib.dll",
        //     "System.dll",
        //     "System.Core.dll",
        //     "UnityEngine.JSONSerializeModule.dll",
        //     "UniTask.dll",
        //     "Unity.Entities.dll",
        //     "Unity.Collections.dll",
        //     "Unity.Transforms.dll",
        //     "Unity.Mathematics.dll",
        //     "Unity.Burst.dll"
        //};


        //获取资源二进制
        private static Dictionary<string, byte[]> s_assetDatas = new Dictionary<string, byte[]>();

        public static byte[] GetAssetData(string dllName)
        {
            return s_assetDatas[dllName];
        }


        async void Start()
        {
            //return;
            await DownloadAssetsAndStartGame();
        }

        async UniTask DownloadAssetsAndStartGame()
        {
            Debug.Log($"[{GetType().FullName}] StartLoadDll.cs!");

#if UNITY_EDITOR
            PlayMode = EPlayMode.EditorSimulateMode;
#else
            PlayMode = EPlayMode.HostPlayMode;
#endif

            InitHostServerURL();
            //初始化BetterStreamingAssets插件
            BetterStreamingAssets.Initialize();
            await DownLoadAssetsByYooAssets();
        }

        private void InitHostServerURL()
        {
#if UNITY_STANDALONE
           if (localTest)
            {
                DefaultHostServer = $"{localDefaultHostServer}/Win64";
                //FallbackHostServer = "http://192.168.31.12/Packages/Win64";
            }
            else
            {
                DefaultHostServer = $"{DefaultHostServer}/{Application.productName}/Win64";
                //FallbackHostServer = "https://gleen-test-0012.oss-cn-beijing.aliyuncs.com/Dev/Win64";
            }


#elif UNITY_ANDROID
           if (localTest)
            {
                DefaultHostServer = $"{localDefaultHostServer}/Android";
                //FallbackHostServer = "http://192.168.31.12/Packages/Android";
            }
            else
            {
                DefaultHostServer = $"{DefaultHostServer}/{Application.productName}/Android";
                //FallbackHostServer = "https://gleen-test-0012.oss-cn-beijing.aliyuncs.com/Dev/Android";
            }
#elif UNITY_WEBGL
            if (localTest)
            {
                DefaultHostServer = $"{localDefaultHostServer}/WebGL/YooAsset";
                //FallbackHostServer = "http://192.168.31.12/Packages/Android";
            }
            else
            {
                DefaultHostServer = $"{DefaultHostServer}/{Application.productName}/WebGL/YooAsset";
                //DefaultHostServer = $"https://cdn.jsdelivr.net/gh/jacobe0012/FoxCardCdn/WebGL/YooAsset";
                //FallbackHostServer = "https://gleen-test-0012.oss-cn-beijing.aliyuncs.com/Dev/Android";
            }
#elif UNITY_IOS
            if (localTest)
            {
                DefaultHostServer = $"{localDefaultHostServer}/IOS";
                //FallbackHostServer = "http://192.168.31.12/Packages/IOS";
            }
            else
            {
                DefaultHostServer = $"{DefaultHostServer}/{Application.productName}/IOS";
                //FallbackHostServer = "https://gleen-test-0012.oss-cn-beijing.aliyuncs.com/Dev/Android";
            }
#endif
            FallbackHostServer = DefaultHostServer;
        }


        #region Yooasset下载

        /// <summary>
        /// 获取下载信息
        /// </summary>
        /// <param name="onDownloadComplete"></param>
        /// <returns></returns>
        async UniTask DownLoadAssetsByYooAssets()
        {
            // 1.初始化资源系统
            if (!YooAssets.Initialized)
            {
                YooAssets.Initialize();
            }

// #if UNITY_WEBGL
//             YooAssets.SetCacheSystemDisableCacheOnWebGL();
//             Debug.Log($"UNITY_WEBGL_YooAssets");
// #endif

            ResourcePackage package = YooAssets.TryGetPackage("DefaultPackage");
            if (!YooAssets.ContainsPackage("DefaultPackage"))
            {
                // 创建默认的资源包
                package = YooAssets.CreatePackage("DefaultPackage");
                // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
                YooAssets.SetDefaultPackage(package);
            }

            if (package.InitializeStatus == EOperationStatus.None)
            {
                if (PlayMode == EPlayMode.EditorSimulateMode)
                {
                    //编辑器模拟模式
                    var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline;
                    var simulateBuildResult = EditorSimulateModeHelper.SimulateBuild(buildPipeline, "DefaultPackage");
                    var editorFileSystem =
                        FileSystemParameters.CreateDefaultEditorFileSystemParameters(simulateBuildResult);
                    var initParameters = new EditorSimulateModeParameters();
                    initParameters.EditorFileSystemParameters = editorFileSystem;

                    // var initParameters = new EditorSimulateModeParameters();
                    // initParameters.EditorFileSystemParameters =
                    //     EditorSimulateModeHelper.SimulateBuild("BuiltinBuildPipeline",
                    //         "DefaultPackage");
                    //package.InitializeStatus == EOperationStatus.Succeed
                    await package.InitializeAsync(initParameters).ToUniTask();
                }
                else if (PlayMode == EPlayMode.HostPlayMode)
                {
#if UNITY_WEBGL
#if WEIXINMINIGAME
                    IRemoteServices remoteServices = new RemoteServices(DefaultHostServer, FallbackHostServer);
                    var webFileSystem = WechatFileSystemCreater.CreateWechatFileSystemParameters(remoteServices);
                    var initParameters = new WebPlayModeParameters();
                    initParameters.WebFileSystemParameters = webFileSystem;


#else
                    var webFileSystem = FileSystemParameters.CreateDefaultWebFileSystemParameters();
                    var initParameters = new WebPlayModeParameters();
                    initParameters.WebFileSystemParameters = webFileSystem;
#endif


#else
                    IRemoteServices remoteServices = new RemoteServices(DefaultHostServer, FallbackHostServer);
                    var cacheFileSystem = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
                    var buildinFileSystem = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();   
                    var initParameters = new HostPlayModeParameters();
                    initParameters.BuildinFileSystemParameters = buildinFileSystem; 
                    initParameters.CacheFileSystemParameters = cacheFileSystem;
#endif


                    // var initParameters = new HostPlayModeParameters();
                    // initParameters.BuildinQueryServices = new GameQueryServices(); //太空战机DEMO的脚本类，详细见StreamingAssetsHelper
                    // initParameters.DecryptionServices = new GameDecryptionServices();
                    // initParameters.RemoteServices = new RemoteServices(DefaultHostServer, FallbackHostServer);

                    await package.InitializeAsync(initParameters).ToUniTask();
                }
                else if (PlayMode == EPlayMode.OfflinePlayMode)
                {
                    //单机模式
                    var buildinFileSystem = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                    var initParameters = new OfflinePlayModeParameters();
                    initParameters.BuildinFileSystemParameters = buildinFileSystem;


                    await package.InitializeAsync(initParameters).ToUniTask();
                }
            }

            //2.获取资源版本
            var operation = package.RequestPackageVersionAsync();
            await operation.ToUniTask();
            if (operation.Status != EOperationStatus.Succeed)
            {
                //更新失败、显示弹窗窗口
                tx = Instantiate(Resources.Load<GameObject>("ShowMsgBox"));
                tx.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "资源下载失败,资源服务端未开启";
                tx.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "退出";
                tx.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>

                {
#if !UNITY_EDITOR
        Application.Quit();
#else
                    EditorApplication.isPlaying = false;
#endif
                    DestroyImmediate(tx);
                });
                Debug.LogError(operation.Error);
                return;
            }

            string packageVersion = operation.PackageVersion;
            //3.更新补丁清单
            var operation2 = package.UpdatePackageManifestAsync(packageVersion);
            await operation2.ToUniTask();
            if (operation2.Status != EOperationStatus.Succeed)
            {
                //更新失败
                Debug.LogError(operation2.Error);
                //TODO:
                return;
            }

            //4.下载补丁包信息，反馈到弹窗
            await Download();
        }

        /// <summary>
        /// 获取下载的信息大小，显示弹窗上
        /// </summary>
        /// <returns></returns>
        async UniTask Download()
        {
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            int timeout = 60;
            var package = YooAssets.GetPackage("DefaultPackage");
            var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain, timeout);
            //没有需要下载的资源
            if (downloader.TotalDownloadCount == 0)
            {
                Debug.Log($"没有资源更新，直接进入游戏加载环节");
                await GotoStart();
                return;
            }

            //需要下载的文件总数和总大小
            int totalDownloadCount = downloader.TotalDownloadCount;
            long totalDownloadBytes = downloader.TotalDownloadBytes;
            Debug.Log($"文件总数:{totalDownloadCount}:::总大小:{totalDownloadBytes}");
            //显示更新提示UI界面 
            tx = Instantiate(Resources.Load<GameObject>("ShowMsgBox"));
            tx.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text =
                $"文件总数:{totalDownloadCount},总大小:{totalDownloadBytes}KB";
            tx.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "下载";
            tx.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick
                .AddListener(() => GetDownload());
        }

        /// <summary>
        /// 按键回调下载
        /// </summary>
        /// <returns></returns>
        async UniTask GetDownload()
        {
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            int timeout = 60;
            var package = YooAssets.GetPackage("DefaultPackage");
            var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain, timeout);
            //注册回调方法
            downloader.OnDownloadErrorCallback = OnDownloadErrorFunction;
            downloader.OnDownloadProgressCallback = OnDownloadProgressUpdateFunction;
            downloader.OnDownloadOverCallback = OnDownloadOverFunction;
            downloader.OnStartDownloadFileCallback = OnStartDownloadFileFunction;
            //开启下载
            downloader.BeginDownload();
            await downloader.ToUniTask();
            //检测下载结果
            if (downloader.Status == EOperationStatus.Succeed)
            {
                //下载成功
                Debug.Log($"更新完成!");
                DestroyImmediate(tx);
                await GotoStart();
            }
            else
            {
                //下载失败
                Debug.LogError($"更新失败！");
                DestroyImmediate(tx);
                return;
                //TODO:
            }
        }

        /// <summary>
        /// 开始下载
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sizeBytes"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnStartDownloadFileFunction(string fileName, long sizeBytes)
        {
            Debug.Log(string.Format("开始下载：文件名：{0}, 文件大小：{1}", fileName, sizeBytes));
        }

        /// <summary>
        /// 下载完成
        /// </summary>
        /// <param name="isSucceed"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnDownloadOverFunction(bool isSucceed)
        {
            Debug.Log("下载" + (isSucceed ? "成功" : "失败"));
        }

        /// <summary>
        /// 更新中
        /// </summary>
        /// <param name="totalDownloadCount"></param>
        /// <param name="currentDownloadCount"></param>
        /// <param name="totalDownloadBytes"></param>
        /// <param name="currentDownloadBytes"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnDownloadProgressUpdateFunction(int totalDownloadCount, int currentDownloadCount,
            long totalDownloadBytes, long currentDownloadBytes)
        {
            tx.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = string.Format(
                "文件总数：{0}, 已下载文件数：{1}, 下载总大小：{2}, 已下载大小：{3}", totalDownloadCount, currentDownloadCount,
                totalDownloadBytes,
                currentDownloadBytes);
            Debug.Log(string.Format("文件总数：{0}, 已下载文件数：{1}, 下载总大小：{2}, 已下载大小：{3}", totalDownloadCount,
                currentDownloadCount,
                totalDownloadBytes, currentDownloadBytes));
        }

        /// <summary>
        /// 下载出错
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="error"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnDownloadErrorFunction(string fileName, string error)
        {
            Debug.LogError(string.Format("下载出错：文件名：{0}, 错误信息：{1}", fileName, error));
        }

        /// <summary>
        /// 完成下载验证开始进入游戏
        /// </summary>
        /// <returns></returns>
        async UniTask GotoStart()
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            //热更新Dll名称
            //var Allassets = HotDllNames.Concat(AOTMetaAssemblyNames);

            foreach (var asset in HotDllNames)
            {
                // RawFileHandle handle =
                //     package.LoadRawFileAsync($"Assets/{Application.productName}/HotFixDlls/" + asset + ".bytes");
                AssetHandle handle =
                    package.LoadAssetAsync<TextAsset>(
                        $"Assets/{Application.productName}/HotFixDlls/" + asset + ".bytes");

                await handle.ToUniTask();
                var textAsset = handle.AssetObject as TextAsset;

                byte[] fileData = textAsset.bytes;

                s_assetDatas[asset] = fileData;
                Debug.Log($"[{GetType().FullName}] dll:{asset} size:{fileData.Length}");
            }

            //DestroyImmediate(tx);
            await StartGame();
        }

        // 内置文件查询服务类
        // private class QueryStreamingAssetsFileServices : IQueryServices
        // {
        //     public bool QueryStreamingAssets(string fileName)
        //     {
        //         // 注意：使用了BetterStreamingAssets插件，使用前需要初始化该插件！
        //         string buildinFolderName = YooAssets.GetStreamingAssetBuildinFolderName();
        //         return BetterStreamingAssets.FileExists($"{buildinFolderName}/{fileName}");
        //     }
        // }

        #endregion

        async UniTask StartGame()
        {
            //LoadMetadataForAOTAssemblies();

#if !UNITY_EDITOR
           foreach (var HotDll in HotDllNames)
            {
                AssList.Add(System.Reflection.Assembly.Load(GetAssetData(HotDll)));
            }
#endif
            //委托加载方式，加载prefab
            var package = YooAssets.GetPackage("DefaultPackage");
            AssetHandle handle =
                package.LoadAssetAsync<GameObject>(
                    $"Assets/{Application.productName}/Scripts/HotFix/HotFixPrefab/HotUpdatePrefab.prefab");
            await handle.ToUniTask();

            GameObject go = handle.AssetObject as GameObject;
            Instantiate(go);
            Debug.Log($"[{GetType().FullName}] Prefab name is {go.name}");
            Debug.Log($"[{GetType().FullName}] FinishLoadDll.cs!");
        }
    }

    /// <summary>
    /// 远端资源地址查询服务类
    /// </summary>
    class RemoteServices : IRemoteServices
    {
        private readonly string _defaultHostServer;
        private readonly string _fallbackHostServer;

        public RemoteServices(string defaultHostServer, string fallbackHostServer)
        {
            _defaultHostServer = defaultHostServer;
            _fallbackHostServer = fallbackHostServer;
        }

        string IRemoteServices.GetRemoteFallbackURL(string fileName)
        {
            return $"{_defaultHostServer}/{fileName}";
        }

        string IRemoteServices.GetRemoteMainURL(string fileName)
        {
            return $"{_fallbackHostServer}/{fileName}";
        }
    }

    /// <summary>
    /// 资源文件解密服务类
    /// </summary>
    // class GameDecryptionServices : IDecryptionServices
    // {
    //     public ulong LoadFromFileOffset(DecryptFileInfo fileInfo)
    //     {
    //         return 32;
    //     }
    //
    //     public byte[] LoadFromMemory(DecryptFileInfo fileInfo)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public Stream LoadFromStream(DecryptFileInfo fileInfo)
    //     {
    //         BundleStream bundleStream =
    //             new BundleStream(fileInfo.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
    //         return bundleStream;
    //     }
    //
    //     public uint GetManagedReadBufferSize()
    //     {
    //         return 1024;
    //     }
    // }

    /// <summary>
    /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
    /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
    /// </summary>
    // private static void LoadMetadataForAOTAssemblies()
    // {
    //     /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
    //     /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
    //     HomologousImageMode mode = HomologousImageMode.SuperSet;
    //     foreach (var aotDllName in AOTMetaAssemblyNames)
    //     {
    //         byte[] dllBytes = GetAssetData(aotDllName);
    //         // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
    //         LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);
    //         Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
    //     }
    // }
}