using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace XFramework
{
    public class SceneObject : XObject, IAwake<string, object>
    {
        public string Key { get; private set; }

        public object SceneHandle { get; private set; }

        public Scene Scene { get; private set; }

        public void Initialize(string key, object handle)
        {
            this.Key = key;
            this.SceneHandle = handle;
        }

        public void SetScene(Scene scene)
        {
            this.Scene = scene;
        }

        protected override void OnDestroy()
        {
            this.Scene?.Dispose();
            this.Scene = null;

            var handle = this.SceneHandle;
            this.SceneHandle = null;
            this.Key = null;

            //SceneResManager.UnloadSceneAsync(handle).ToCoroutine();
            base.OnDestroy();
        }
    }
}