using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace XFramework
{
    public abstract class SceneLoader : IDisposable
    {
        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="key"></param>
        /// <param name="loadSceneMode"></param>
        /// <returns></returns>
        public abstract object LoadScene(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single);

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="key"></param>
        /// <param name="loadSceneMode"></param>
        /// <returns></returns>
        public abstract object LoadSceneAsync(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single);

        /// <summary>
        /// 卸载一个场景
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public abstract UniTask UnloadSceneAsync(object handle);

        /// <summary>
        /// 等待场景加载完成
        /// </summary>
        /// <param name="sceneObjet"></param>
        /// <returns></returns>
        public abstract UniTask WaitForCompleted(SceneObject sceneObjet);

        /// <summary>
        /// 场景加载进度
        /// </summary>
        /// <param name="sceneObjet"></param>
        /// <returns></returns>
        public abstract float Progress(SceneObject sceneObjet);

        /// <summary>
        /// 场景加载是否完成
        /// </summary>
        /// <param name="sceneObjet"></param>
        /// <returns></returns>
        public abstract bool IsDone(SceneObject sceneObjet);

        public virtual void Dispose()
        {
        }
    }
}