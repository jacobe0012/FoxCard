// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using UnityEngine;
// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.AsyncOperations;
//
// namespace XFramework
// {
//     public class AAResourcesLoader : ResourcesLoader
//     {
//         public override async XFTask InitAsync()
//         {
//             var op = Addressables.InitializeAsync();
//             await op.Task;
//         }
//
//         public override GameObject Instantiate(string path, Transform parent)
//         {
//             var op = Addressables.InstantiateAsync(path, parent);
//             var obj = op.WaitForCompletion();
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {path}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源实例化失败, key is {path}");
//                 return null;
//             }
//
//             return obj;
//         }
//
//         public override GameObject Instantiate(string path, Transform parent, Vector3 position)
//         {
//             var op = Addressables.InstantiateAsync(path, parent);
//             var obj = op.WaitForCompletion();
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {path}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源实例化失败, key is {path}");
//                 return null;
//             }
//
//             if (obj)
//             {
//                 obj.transform.position = position;
//             }
//
//             return obj;
//         }
//
//         public override GameObject Instantiate(string path, Transform parent, Vector3 position, Quaternion quaternion)
//         {
//             var op = Addressables.InstantiateAsync(path, parent);
//             var obj = op.WaitForCompletion();
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {path}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源实例化失败, key is {path}");
//                 return null;
//             }
//
//             if (obj)
//             {
//                 obj.transform.position = position;
//                 obj.transform.rotation = quaternion;
//             }
//
//             return obj;
//         }
//
//         public override async XFTask<GameObject> InstantiateAsync(string path, Transform parent)
//         {
//             var op = Addressables.InstantiateAsync(path, parent);
//             var obj = await op.Task;
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {path}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源实例化失败, key is {path}");
//                 return null;
//             }
//
//             return obj;
//         }
//
//         public async override XFTask<GameObject> InstantiateAsync(string path, Transform parent, Vector3 position)
//         {
//             var op = Addressables.InstantiateAsync(path, parent);
//             var obj = await op.Task;
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {path}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源实例化失败, key is {path}");
//                 return null;
//             }
//
//             if (obj)
//             {
//                 obj.transform.position = position;
//             }
//
//             return obj;
//         }
//
//         public async override XFTask<GameObject> InstantiateAsync(string path, Transform parent, Vector3 position, Quaternion quaternion)
//         {
//             var op = Addressables.InstantiateAsync(path, parent);
//             var obj = await op.Task;
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {path}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源实例化失败, key is {path}");
//                 return null;
//             }
//
//             if (obj)
//             {
//                 obj.transform.position = position;
//                 obj.transform.rotation = quaternion;
//             }
//
//             return obj;
//         }
//
//         public override UnityEngine.Object LoadAsset(string key, Type type)
//         {
//             var op = Addressables.LoadAssetAsync<UnityEngine.Object>(key);
//             var asset = op.WaitForCompletion();
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {key}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源加载失败, key is {key}, type is {type}");
//                 return null;
//             }
//
//             return asset;
//         }
//
//         public override T LoadAsset<T>(string key)
//         {
//             var op = Addressables.LoadAssetAsync<T>(key);
//             var asset = op.WaitForCompletion();
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {key}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源加载失败, key is {key}, type is {typeof(T)}");
//                 return null;
//             }
//
//             return asset;
//         }
//
//         public async override XFTask<T> LoadAssetAsync<T>(string key)
//         {
//             var op = Addressables.LoadAssetAsync<T>(key);
//             await op.Task;
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {key}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源加载失败, key is {key}, type is {typeof(T)}");
//                 return null;
//             }
//
//             return op.Result;
//         }
//
//         public async override XFTask<UnityEngine.Object> LoadAssetAsync(string key, Type type)
//         {
//             var op = Addressables.LoadAssetAsync<UnityEngine.Object>(key);
//             await op.Task;
//
//             if (!op.IsValid())
//             {
//                 Log.Error($"资源无效, key is {key}");
//                 return null;
//             }
//
//             if (op.Status != AsyncOperationStatus.Succeeded)
//             {
//                 Log.Error($"资源加载失败, key is {key}, type is {type}");
//                 return null;
//             }
//
//             return op.Result;
//         }
//
//         public override void ReleaseAsset(UnityEngine.Object asset)
//         {
//             try
//             {
//                 if (asset)
//                 {
//                     Addressables.Release(asset);
//                 }
//             }
//             catch (Exception e)
//             {
//                 Log.Error($"资源卸载失败\n{e}");
//             }
//         }
//
//         public override void ReleaseInstance(GameObject obj)
//         {
//             try
//             {
//                 if (obj)
//                 {
//                     Addressables.ReleaseInstance(obj);
//                 }
//             }
//             catch (Exception e)
//             {
//                 Log.Error($"实例化对象卸载失败\n{e}");
//             }
//         }
//
//         public override void UnloadUnusedAssets()
//         {
//             Resources.UnloadUnusedAssets();
//         }
//     }
// }

