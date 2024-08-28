// using System;
// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using UnityEngine;
// using Object = UnityEngine.Object;
//
// namespace XFramework
// {
//     public sealed class BuiltinResourcesManager : ResourcesLoader
//     {
//         private class AssetInfo : IDisposable
//         {
//             public static AssetInfo Create()
//             {
//                 return ObjectPool.Instance.Fetch<AssetInfo>();
//             }
//
//             public string Key { get; set; }
//
//             public Object Object { get; set; }
//
//             public int RefCount { get; set; }
//
//             public void Dispose()
//             {
//                 this.Key = null;
//                 this.Object = null;
//                 this.RefCount = 0;
//                 ObjectPool.Instance.Recycle(this);
//             }
//         }
//
//         private class ObjInfo : IDisposable
//         {
//             public static ObjInfo Create()
//             {
//                 return ObjectPool.Instance.Fetch<ObjInfo>();
//             }
//
//             public string Key { get; set; }
//
//             public GameObject GameObject { get; set; }
//
//             public void Dispose()
//             {
//                 Object.Destroy(this.GameObject);
//                 this.Key = null;
//                 this.GameObject = null;
//                 ObjectPool.Instance.Recycle(this);
//             }
//         }
//
//         /// <summary>
//         /// 所有的资源
//         /// <para>Key -> 资源的Key</para>
//         /// <para>Value -> 资源信息</para>
//         /// </summary>
//         private Dictionary<string, AssetInfo> allAssets = new Dictionary<string, AssetInfo>();
//
//         /// <summary>
//         /// 所有的资源key
//         /// <para>Key -> 资源的InstanceId</para>
//         /// <para>Value -> 资源的key</para>
//         /// </summary>
//         private Dictionary<int, string> allAssetsInstanceId = new Dictionary<int, string>();
//
//         /// <summary>
//         /// 所有的实例化对象
//         /// <para>Key -> InstanceId</para>
//         /// <para>Value -> GameObject信息</para>
//         /// </summary>
//         private Dictionary<int, ObjInfo> allObjs = new Dictionary<int, ObjInfo>();
//
//         private AssetInfo AddAssetInfo(string key, Object asset)
//         {
//             if (allAssets.TryGetValue(key, out var assetInfo))
//                 return assetInfo;
//
//             assetInfo = AssetInfo.Create();
//             assetInfo.Key = key;
//             assetInfo.Object = asset;
//             allAssets.Add(key, assetInfo);
//             int instanceId = asset.GetInstanceID();
//             allAssetsInstanceId[instanceId] = key;
//
//             return assetInfo;
//         }
//
//         private ObjInfo AddObjInfo(string key, GameObject obj)
//         {
//             int instanceId = obj.GetInstanceID();
//             if (allObjs.TryGetValue(instanceId, out var info))
//                 return info;
//
//             info = ObjInfo.Create();
//             info.Key = key;
//             info.GameObject = obj;
//
//             allObjs.Add(instanceId, info);
//
//             return info;
//         }
//
//         /// <summary>
//         /// 实例化GameObject
//         /// </summary>
//         /// <param name="prefab"></param>
//         /// <param name="key"></param>
//         /// <param name="parent"></param>
//         /// <param name="isFromPool"></param>
//         /// <returns></returns>
//         private GameObject Instantiate(GameObject prefab, string key, Transform parent)
//         {
//             GameObject obj = Object.Instantiate<GameObject>(prefab);
//             AddObjInfo(key, obj);
//             obj.transform.SetParent(parent, false);
//
//             return obj;
//         }
//
//         public override Object LoadAsset(string key, Type type)
//         {
//             if (key.IsNullOrEmpty())
//                 return null; 
//
//             if (!allAssets.TryGetValue(key, out AssetInfo assetInfo))
//             {
//                 Object asset = Resources.Load(key, type);
//                 if (asset == null)
//                 {
//                     Log.Error($"资源加载失败，Key is {key}, Type is {type}");
//                     return null;
//                 }
//                 assetInfo = AddAssetInfo(key, asset);
//             }
//
//             ++assetInfo.RefCount;
//
//             return assetInfo.Object;
//         }
//
//         public override T LoadAsset<T>(string key)
//         {
//             return this.LoadAsset(key, typeof(T)) as T;
//         }
//
//         public async override XFTask<Object> LoadAssetAsync(string key, Type type)
//         {
//             try
//             {
//                 if (key.IsNullOrEmpty())
//                     return null;
//
//                 if (!allAssets.TryGetValue(key, out AssetInfo assetInfo))
//                 {
//                     ResourceRequest request = Resources.LoadAsync(key, type);
//                     var tcs = XFTask<Object>.Create(true);
//                     request.completed += (o) =>
//                     {
//                         tcs.SetResult(((ResourceRequest)o).asset);
//                     };
//                     Object asset = await tcs;
//                     if (asset == null)
//                     {
//                         Log.Error($"资源加载失败，Key is {key}, Type is {type}");
//                         return null;
//                     }
//
//                     if (!allAssets.TryGetValue(key, out assetInfo))
//                         assetInfo = AddAssetInfo(key, asset);
//                 }
//
//                 ++assetInfo.RefCount;
//
//                 return assetInfo.Object;
//             }
//             catch (Exception e)
//             {
//                 Log.Error(e);
//                 return null;
//             }
//         }
//
//         public async override XFTask<T> LoadAssetAsync<T>(string key)
//         {
//             try
//             {
//                 return await this.LoadAssetAsync(key, typeof(T)) as T;
//             }
//             catch (Exception e)
//             {
//                 Log.Error(e);
//                 return null;
//             }
//         }
//
//         public override GameObject Instantiate(string key, Transform parent)
//         {
//             GameObject prefab = LoadAsset<GameObject>(key);
//             if (prefab == null)
//                 return null;
//
//             GameObject obj = Instantiate(prefab, key, parent);
//             return obj;
//         }
//
//         public override GameObject Instantiate(string path, Transform parent, Vector3 position)
//         {
//             GameObject obj = Instantiate(path, parent);
//             if (!obj)
//                 return null;
//
//             obj.transform.position = position;
//             return obj;
//         }
//
//         public override GameObject Instantiate(string path, Transform parent, Vector3 position, Quaternion quaternion)
//         {
//             GameObject obj = Instantiate(path, parent);
//             if (!obj)
//                 return null;
//
//             obj.transform.position = position;
//             obj.transform.rotation = quaternion;
//             return obj;
//         }
//
//         public override Task<GameObject> InstantiateAsync(string key)
//         {
//             throw new NotImplementedException();
//         }
//
//
//         public async override XFTask<GameObject> InstantiateAsync(string key, Transform parent)
//         {
//             GameObject prefab = await LoadAssetAsync<GameObject>(key);
//             if (prefab == null)
//                 return null;
//
//             GameObject obj = Instantiate(prefab, key, parent);
//             return obj;
//         }
//
//         public async override XFTask<GameObject> InstantiateAsync(string path, Transform parent, Vector3 position)
//         {
//             GameObject obj = await InstantiateAsync(path, parent);
//             if (!obj)
//                 return null;
//
//             obj.transform.position = position;
//             return obj;
//         }
//
//         public async override XFTask<GameObject> InstantiateAsync(string path, Transform parent, Vector3 position, Quaternion quaternion)
//         {
//             GameObject obj = await InstantiateAsync(path, parent);
//             if (!obj)
//                 return null;
//
//             obj.transform.position = position;
//             obj.transform.rotation = quaternion;
//             return obj;
//         }
//
//         public override void ReleaseInstance(GameObject obj)
//         {
//             if (!obj)
//                 return;
//
//             int instanceId = obj.GetInstanceID();
//             if (allObjs.TryGetValue(instanceId, out var objInfo))
//             {
//                 string key = objInfo.Key;
//                 objInfo.Dispose();
//                 allObjs.Remove(instanceId);
//
//                 if (allAssets.TryGetValue(key, out var assetInfo))
//                 {
//                     --assetInfo.RefCount;
//                     if (assetInfo.RefCount <= 0)
//                     {
//                         //Object o = assetInfo.Object;
//                         assetInfo.Dispose();
//                         allAssets.Remove(key);
//                         //Resources.UnloadAsset(o);
//                     }
//                 }
//             }
//             else
//             {
//                 Log.Error("ReleaseInstance Faild! Object is not managed, please handle it yourself!");
//             }
//         }
//
//         public override void ReleaseAsset(Object asset)
//         {
//             if (!asset)
//                 return;
//
//             int instanceId = asset.GetInstanceID();
//             if (!allAssetsInstanceId.TryGetValue(instanceId, out var key))
//             {
//                 Log.Error("ReleaseAsset Faild! Object is not managed, please handle it yourself!");
//                 return;
//             }
//
//             if (allAssets.TryGetValue(key, out var assetInfo))
//             {
//                 --assetInfo.RefCount;
//                 if (assetInfo.RefCount <= 0)
//                 {
//                     //Object o = assetInfo.Object;
//                     assetInfo.Dispose();
//                     allAssets.Remove(key);
//                     //Resources.UnloadAsset(o);
//                 }
//             }
//             else
//             {
//                 Log.Error("ReleaseAsset Faild! Object is not managed, please handle it yourself!");
//                 return;
//             }
//         }
//
//         public override void UnloadUnusedAssets()
//         {
//             //foreach (var objInfo in allObjs.Values)
//             //{
//             //    objInfo.Dispose();
//             //}
//             //allObjs.Clear();
//
//             using var invalidList = XList<int>.Create();
//             foreach (var asset in allAssetsInstanceId)
//             {
//                 int id = asset.Key;
//                 string key = asset.Value;
//                 if (allAssets.TryGetValue(key, out var assetInfo))
//                 {
//                     if (assetInfo.RefCount <= 0)
//                     {
//                         allAssets.Remove(key);
//                         assetInfo.Dispose();
//                         invalidList.Add(id);
//                     }
//                 }
//             }
//             foreach (var id in invalidList)
//             {
//                 allAssetsInstanceId.Remove(id);
//             }
//             Resources.UnloadUnusedAssets();
//         }
//
//         public override void Dispose()
//         {
//             base.Dispose();
//             UnloadUnusedAssets();
//         }
//     }
// }

