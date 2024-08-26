using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    /// <summary>
    /// 检测资源是否有所引用
    /// </summary>
    public sealed class ResourcesRefDetection : CommonObject, ILateUpdate
    {
        private class AssetRef : XObject, IAwake<Object, XObject>, IAwake<GameObject, XObject, string, bool>
        {
            /// <summary>
            /// 是否来自实例化
            /// </summary>
            public bool IsInstantiate { get; private set; }

            public Object Object { get; private set; }

            public XObject Ref { get; private set; }

            public long RefId { get; private set; }

            public string Key { get; private set; }

            public bool IsFromPool { get; private set; }

            public void Initialize(Object o, XObject parent)
            {
                this.Object = o;
                this.Ref = parent;
                this.RefId = parent.TagId;
                this.Key = null;
                this.IsFromPool = false;
                this.IsInstantiate = false;
            }

            public void Initialize(GameObject obj, XObject parent, string key, bool isFromPool)
            {
                this.Object = obj;
                this.Ref = parent;
                this.RefId = parent.TagId;
                this.Key = key;
                this.IsFromPool = isFromPool;
                this.IsInstantiate = true;
            }

            /// <summary>
            /// 改变所依赖的父对象
            /// </summary>
            /// <param name="parent"></param>
            public void ChangeRef(XObject parent, bool isFromPool)
            {
                this.RefId = parent.TagId;
                this.Ref = parent;
                this.IsFromPool = isFromPool;
            }

            /// <summary>
            /// 检查资源是否有效
            /// </summary>
            /// <returns></returns>
            public bool Check()
            {
                if (this.Object == null)
                    return false;

                if (this.Ref.IsDisposed)
                    return false;

                if (this.Ref.TagId != this.RefId)
                    return false;

                return true;
            }

            /// <summary>
            /// 释放资源
            /// </summary>
            private void Clear()
            {
                if (this.Object)
                {
                    if (this.IsInstantiate && this.Object is GameObject gameObject)
                    {
                        if (this.IsFromPool)
                            GameObjectPool.Instance.Recycle(this.Key, gameObject);
                        else
                            ResourcesManager.Instance.Loader?.ReleaseInstance(gameObject);
                    }
                    else
                    {
                        ResourcesManager.Instance.Loader?.ReleaseAsset(this.Object);
                    }

                    this.Object = null;
                }
            }

            protected override void OnDestroy()
            {
                this.Clear();
                this.Ref = null;
                this.RefId = 0;
                this.Object = null;
                this.Key = null;
                this.IsInstantiate = false;
                this.IsFromPool = false;
                base.OnDestroy();
            }
        }

        /// <summary>
        /// 实例化资源
        /// </summary>
        private Dictionary<int, AssetRef> assets = new Dictionary<int, AssetRef>();

        /// <summary>
        /// 引用资源
        /// </summary>
        private UnOrderMapSet<int, AssetRef> refAssets = new UnOrderMapSet<int, AssetRef>();

        /// <summary>
        /// 所有加载的资源的Key -> Set(InstanceId)
        /// </summary>
        private UnOrderMapSet<string, int> assetsKeyDict = new UnOrderMapSet<string, int>();

        /// <summary>
        /// 检查所有的资源是否有效，无效的则移除
        /// </summary>
        public void VerifyAllResources()
        {
            using var assetIdList = XList<int>.Create();
            assetIdList.AddRange(this.assets.Keys);
            foreach (var id in assetIdList)
            {
                if (this.assets.TryGetValue(id, out var asset))
                {
                    if (!asset.Check())
                    {
                        this.Remove(id);
                    }
                }
            }

            assetIdList.Clear();
            assetIdList.AddRange(this.refAssets.Keys);
            using var refAssetList = XList<AssetRef>.Create();
            foreach (var id in assetIdList)
            {
                if (this.refAssets.TryGetValue(id, out var list))
                {
                    refAssetList.Clear();
                    refAssetList.AddRange(list);
                    foreach (var asset in refAssetList)
                    {
                        if (!asset.Check())
                        {
                            list.Remove(asset);
                            if (list.Count == 0)
                                this.refAssets.Remove(id);

                            asset.Dispose();
                        }
                    }
                }
            }
        }

        public void LateUpdate()
        {
            this.VerifyAllResources();
        }

        /// <summary>
        /// 添加资源进行管理，使asset关联parent，如果parent被释放了，则asset也会被释放
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="asset"></param>
        public int AddAsset(XObject parent, Object asset)
        {
            if (!asset)
                return 0;

            int id = asset.GetInstanceID();
            AssetRef assetRef = ObjectFactory.Create<AssetRef, Object, XObject>(asset, parent, true);
            this.refAssets.Add(id, assetRef);

            return id;
        }

        /// <summary>
        /// 添加实例化对象，使obj关联parent，如果parent被释放了，则obj也会被释放
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="isFromPool"></param>
        public int AddInstantiate(XObject parent, GameObject obj, string key, bool isFromPool)
        {
            if (!obj)
                return 0;

            int id = obj.GetInstanceID();
            AssetRef assetRef =
                ObjectFactory.Create<AssetRef, GameObject, XObject, string, bool>(obj, parent, key, isFromPool, true);
            
            if (!this.assets.TryAdd(id, assetRef))
            {
                //Log.Debug($"id{id}assetRef{assetRef} not added");
            }

            this.assetsKeyDict.Add(key, id);

            return id;
        }

        /// <summary>
        /// 拿出一个失效的实例化对象并改变绑定的父类
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="key"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public GameObject FetchInvalidObject(XObject parent, string key, bool isFromPool)
        {
            if (!this.assetsKeyDict.TryGetValue(key, out var set))
                return null;

            foreach (var id in set)
            {
                if (this.assets.TryGetValue(id, out var asset))
                {
                    if (!asset.Check())
                    {
                        asset.ChangeRef(parent, isFromPool);
                        return asset.Object as GameObject;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 释放失效的实例化对象
        /// </summary>
        /// <param name="key"></param>
        public void DisposeInvalidObjects(string key)
        {
            if (!this.assetsKeyDict.TryGetValue(key, out var set))
                return;

            using var list = XList<int>.Create();
            list.AddRange(set);
            foreach (var id in list)
            {
                if (this.assets.TryGetValue(id, out var asset))
                {
                    if (!asset.Check())
                    {
                        this.Remove(id);
                    }
                }
            }
        }

        public void Remove(GameObject asset)
        {
            int id = asset.GetInstanceID();
            this.Remove(id);
        }

        public void Remove(Object asset)
        {
            int id = asset.GetInstanceID();
            this.Remove(id, asset);
        }

        private void Remove(int id)
        {
            if (this.assets.TryRemove(id, out AssetRef assetRef))
            {
                this.assetsKeyDict.Remove(assetRef.Key, id);
                assetRef.Dispose();
            }
        }

        private void Remove(int id, Object asset)
        {
            if (this.refAssets.TryGetValue(id, out var list))
            {
                AssetRef assetRef = null;
                foreach (var r in list)
                {
                    if (r.Object == asset)
                    {
                        assetRef = r;
                        break;
                    }
                }

                if (assetRef != null)
                {
                    this.refAssets.Remove(id, assetRef);
                    assetRef.Dispose();
                }
            }
        }

        protected override void Destroy()
        {
            foreach (var asset in this.assets.Values)
            {
                asset.Dispose();
            }

            foreach (var list in this.refAssets.Values)
            {
                foreach (var asset in list)
                {
                    asset.Dispose();
                }
            }

            this.assets.Clear();
            this.refAssets.Clear();
            this.assetsKeyDict.Clear();
        }
    }
}