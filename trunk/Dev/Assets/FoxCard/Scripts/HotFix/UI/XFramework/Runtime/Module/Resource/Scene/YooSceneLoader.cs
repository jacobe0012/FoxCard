using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using YooAsset;

namespace XFramework
{
    internal class YooSceneInstance
    {
        public string Key { get; private set; }

        public SceneHandle Operation { get; private set; }

        public YooSceneInstance(string key, SceneHandle op)
        {
            this.Key = key;
            this.Operation = op;
        }
    }

    public class YooSceneLoader : SceneLoader
    {
        public override bool IsDone(SceneObject sceneObjet)
        {
            YooSceneInstance instance = (YooSceneInstance)sceneObjet.SceneHandle;
            if (instance is null)
                return true;

            return instance.Operation.IsDone;
        }

        public override object LoadScene(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            SceneHandle op = package.LoadSceneSync(key, loadSceneMode);

            var sceneInstance = new YooSceneInstance(key, op);
            return sceneInstance;
        }

        public override object LoadSceneAsync(string key, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            SceneHandle op = package.LoadSceneAsync(key, loadSceneMode);

            var sceneInstance = new YooSceneInstance(key, op);
            return sceneInstance;
        }

        public override float Progress(SceneObject sceneObjet)
        {
            YooSceneInstance instance = (YooSceneInstance)sceneObjet.SceneHandle;
            if (instance is null)
                return 1f;

            return instance.Operation.Progress;
        }

        public override async UniTask UnloadSceneAsync(object handle)
        {
            YooSceneInstance instance = (YooSceneInstance)handle;
            var op = instance.Operation.UnloadAsync();
            await op.ToUniTask();
        }

        public async override UniTask WaitForCompleted(SceneObject sceneObjet)
        {
            YooSceneInstance instance = (YooSceneInstance)sceneObjet.SceneHandle;
            if (instance is null)
                return;

            if (instance.Operation.IsDone)
                return;

            await instance.Operation.ToUniTask();
        }
    }
}