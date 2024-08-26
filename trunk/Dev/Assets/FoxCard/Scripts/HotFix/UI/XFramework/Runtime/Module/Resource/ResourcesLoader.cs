using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace XFramework
{
    public abstract class ResourcesLoader : IDisposable
    {
        public async virtual UniTask InitAsync()
        {
            await UniTask.CompletedTask;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract Object LoadAsset(string key, Type type);

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract T LoadAsset<T>(string key) where T : Object;

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract UniTask<T> LoadAssetAsync<T>(string key) where T : Object;

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract UniTask<Object> LoadAssetAsync(string key, Type type);

        /// <summary>
        /// 同步加载并实例化
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public abstract GameObject Instantiate(string key, Transform parent);

        /// <summary>
        /// 同步加载并实例化
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public abstract GameObject Instantiate(string key, Transform parent, Vector3 position);

        /// <summary>
        /// 同步加载并实例化
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <param name="quaternion"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public abstract GameObject Instantiate(string key, Transform parent, Vector3 position, Quaternion quaternion);

        /// <summary>
        /// 异步加载并实例化
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public abstract UniTask<GameObject> InstantiateAsync(string key, CancellationToken cct = default);

        /// <summary>
        /// 异步加载并实例化
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public abstract UniTask<GameObject> InstantiateAsync(string key, Transform parent,
            CancellationToken cct = default);

        /// <summary>
        /// 异步加载并实例化
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public abstract UniTask<GameObject> InstantiateAsync(string key, Transform parent, Vector3 position,
            CancellationToken cct = default);

        /// <summary>
        /// 异步加载并实例化
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <param name="quaternion"></param>
        /// <param name="isFromPool"></param>
        /// <returns></returns>
        public abstract UniTask<GameObject> InstantiateAsync(string key, Transform parent, Vector3 position,
            Quaternion quaternion,
            CancellationToken cct = default);

        /// <summary>
        /// 释放实例化对象
        /// </summary>
        /// <param name="obj"></param>
        public abstract void ReleaseInstance(GameObject obj);

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="asset"></param>
        public abstract void ReleaseAsset(Object asset);

        /// <summary>
        /// 释放未使用的资源
        /// </summary>
        public abstract void UnloadUnusedAssets();

        public virtual void Dispose()
        {
        }
    }
}