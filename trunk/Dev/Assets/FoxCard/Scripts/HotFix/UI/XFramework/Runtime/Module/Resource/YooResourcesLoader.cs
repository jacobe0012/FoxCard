using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;
using Object = UnityEngine.Object;

namespace XFramework
{
    public class YooResourcesLoader : ResourcesLoader
    {
        public override async UniTask InitAsync()
        {
            //await YooInitAsync();
            Log.Debug("YooInitAsync");
        }

        public override Object LoadAsset(string key, Type type)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetAsync<UnityEngine.Object>(key);

            op.WaitForAsyncComplete();

            var asset = op.AssetObject;


            if (!op.IsValid)
            {
                Log.Error($"资源无效, key is {key}");
                return null;
            }

            if (op.Status != EOperationStatus.Succeed)
            {
                Log.Error($"资源加载失败, key is {key}, type is {type}");
                return null;
            }

            return asset;
        }

        public override T LoadAsset<T>(string key)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetAsync<T>(key);
            op.WaitForAsyncComplete();
            var asset = op.AssetObject as T;
            if (!op.IsValid)
            {
                Log.Error($"资源无效, key is {key}");
                return null;
            }

            if (op.Status != EOperationStatus.Succeed)
            {
                Log.Error($"资源加载失败, key is {key}, type is {typeof(T)}");
                return null;
            }

            return asset;
        }

        public async override UniTask<T> LoadAssetAsync<T>(string key)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetAsync<T>(key);
            await op.ToUniTask();

            var asset = op.AssetObject as T;

            if (!op.IsValid)
            {
                Log.Error($"资源无效, key is {key}");
                return null;
            }

            if (op.Status != EOperationStatus.Succeed)
            {
                Log.Error($"资源加载失败, key is {key}, type is {typeof(T)}");
                return null;
            }

            return asset;
        }

        public async override UniTask<Object> LoadAssetAsync(string key, Type type)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetAsync<UnityEngine.Object>(key);
            await op.ToUniTask();

            var asset = op.AssetObject;

            if (!op.IsValid)
            {
                Log.Error($"资源无效, key is {key}");
                return null;
            }

            if (op.Status != EOperationStatus.Succeed)
            {
                Log.Error($"资源加载失败, key is {key}, type is {type}");
                return null;
            }

            return asset;
        }

        public override GameObject Instantiate(string key, Transform parent)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetSync<GameObject>(key);

            var obj = op.InstantiateSync(parent);

            return obj;
        }

        public override GameObject Instantiate(string key, Transform parent, Vector3 position)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetSync<GameObject>(key);

            var obj = op.InstantiateSync(parent);
            obj.transform.position = position;
            return obj;
        }

        public override GameObject Instantiate(string key, Transform parent, Vector3 position, Quaternion quaternion)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetSync<GameObject>(key);

            var obj = op.InstantiateSync(position, quaternion, parent);

            return obj;
        }

        public async override UniTask<GameObject> InstantiateAsync(string key,
            CancellationToken cct = default)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetAsync<GameObject>(key).InstantiateAsync();

            if (cct != CancellationToken.None)
            {
                cct.Register(() => { op.Cancel(); });
            }

            await op.ToUniTask();

            if (cct.IsCancellationRequested)
            {
                throw new OperationCanceledException(cct);
            }


            return op.Result;
        }

        public async override UniTask<GameObject> InstantiateAsync(string key, Transform parent,
            CancellationToken cct = default)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetAsync<GameObject>(key).InstantiateAsync(parent);

            if (cct != CancellationToken.None)
            {
                cct.Register(() => { op.Cancel(); });
            }

            await op.ToUniTask();

            if (cct.IsCancellationRequested)
            {
                throw new OperationCanceledException(cct);
            }


            return op.Result;
        }

        public async override UniTask<GameObject> InstantiateAsync(string key, Transform parent, Vector3 position,
            CancellationToken cct = default)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetAsync<GameObject>(key).InstantiateAsync(parent);

            if (cct != CancellationToken.None)
            {
                cct.Register(() => { op.Cancel(); });
            }

            await op.ToUniTask();

            if (cct.IsCancellationRequested)
            {
                throw new OperationCanceledException(cct);
            }

            op.Result.transform.position = position;
            return op.Result;
        }

        public async override UniTask<GameObject> InstantiateAsync(string key, Transform parent, Vector3 position,
            Quaternion quaternion, CancellationToken cct = default)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            var op =
                package.LoadAssetAsync<GameObject>(key).InstantiateAsync(position, quaternion, parent);

            if (cct != CancellationToken.None)
            {
                cct.Register(() => { op.Cancel(); });
            }

            await op.ToUniTask();

            if (cct.IsCancellationRequested)
            {
                throw new OperationCanceledException(cct);
            }

            return op.Result;
        }

        public override void ReleaseInstance(GameObject obj)
        {
            if (obj != null)
            {
                UnityEngine.GameObject.DestroyImmediate(obj);
            }
        }

        public override void ReleaseAsset(Object asset)
        {
            //package.TryUnloadUnusedAsset();
            //Log.Debug("NotImplementedException : ReleaseAsset");
            // try
            // {
            //     if (asset)
            //     {
            //         var package = YooAssets.GetPackage("DefaultPackage");
            //         package.TryUnloadUnusedAsset(key);
            //     }
            // }
            // catch (Exception e)
            // {
            //     Log.Error($"资源卸载失败\n{e}");
            // }
        }

        public override void UnloadUnusedAssets()
        {
            Log.Debug("UnloadUnusedAssets");
            var package = YooAssets.GetPackage("DefaultPackage");
            package.UnloadUnusedAssets();
        }

        /// <summary>
        /// 远端资源地址查询服务类
        /// </summary>
        // class RemoteServices : IRemoteServices
        // {
        //     private readonly string _defaultHostServer;
        //     private readonly string _fallbackHostServer;
        //
        //     public RemoteServices(string defaultHostServer, string fallbackHostServer)
        //     {
        //         _defaultHostServer = defaultHostServer;
        //         _fallbackHostServer = fallbackHostServer;
        //     }
        //
        //     string IRemoteServices.GetRemoteFallbackURL(string fileName)
        //     {
        //         return $"{_defaultHostServer}/{fileName}";
        //     }
        //
        //     string IRemoteServices.GetRemoteMainURL(string fileName)
        //     {
        //         return $"{_fallbackHostServer}/{fileName}";
        //     }
        // }

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
        //     // public Stream LoadFromStream(DecryptFileInfo fileInfo)
        //     // {
        //     //     BundleStream bundleStream =
        //     //         new BundleStream(fileInfo.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        //     //     return bundleStream;
        //     // }
        //
        //     public uint GetManagedReadBufferSize()
        //     {
        //         return 1024;
        //     }
        // }
    }
}