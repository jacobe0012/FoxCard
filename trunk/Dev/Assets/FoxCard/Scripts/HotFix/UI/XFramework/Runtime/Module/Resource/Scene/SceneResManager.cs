using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace XFramework
{
    /// <summary>
    /// 场景资源管理
    /// </summary>
    public sealed class SceneResManager : Singleton<SceneResManager>, IDisposable
    {
        private SceneLoader loader;

        public SceneResManager()
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else
            {
                Log.Error("SceneResManager已经存在，请勿重复创建");
            }
        }

        public void SetLoader(SceneLoader loader)
        {
            this.loader = loader;
        }

        private static bool Check()
        {
            return Instance != null && Instance.loader != null;
        }

        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="key"></param>
        /// <param name="loadSceneMode"></param>
        /// <returns></returns>
        public static SceneObject LoadScene(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            if (!Check())
                return null;

            var handle = Instance.loader.LoadScene(key, loadSceneMode);
            if (handle is null)
                return null;

            SceneObject sceneObject = ObjectFactory.Create<SceneObject, string, object>(key, handle, true);
            return sceneObject;
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="key"></param>
        /// <param name="loadSceneMode"></param>
        /// <returns></returns>
        public static SceneObject LoadSceneAsync(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
        {
            if (!Check())
                return null;

            var handle = Instance.loader.LoadSceneAsync(key, loadSceneMode);
            if (handle is null)
                return null;

            SceneObject sceneObject = ObjectFactory.Create<SceneObject, string, object>(key, handle, true);
            return sceneObject;
        }

        /// <summary>
        /// 异步卸载场景
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static async UniTask UnloadSceneAsync(object handle)
        {
            if (!Check())
                return;

            await Instance.loader.UnloadSceneAsync(handle);
        }

        /// <summary>
        /// 等待场景加载完成
        /// </summary>
        /// <param name="sceneObject"></param>
        /// <returns></returns>
        public static async UniTask WaitForCompleted(SceneObject sceneObject)
        {
            if (!Check())
                return;

            if (sceneObject is null || sceneObject.IsDisposed)
                return;

            await Instance.loader.WaitForCompleted(sceneObject);
        }

        /// <summary>
        /// 场景加载进度
        /// </summary>
        /// <param name="sceneObject"></param>
        /// <returns></returns>
        public static float Progress(SceneObject sceneObject)
        {
            if (!Check())
                return default;

            return Instance.loader.Progress(sceneObject);
        }

        /// <summary>
        /// 场景是否加载完成
        /// </summary>
        /// <param name="sceneObject"></param>
        /// <returns></returns>
        public static bool IsDone(SceneObject sceneObject)
        {
            if (!Check())
                return default;

            return Instance.loader.IsDone(sceneObject);
        }

        public void Dispose()
        {
            this.loader?.Dispose();
            this.loader = null;
            if (Instance == this)
                Instance = null;
        }
    }
}