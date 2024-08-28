using cfg;
using cfg.config;
using Cysharp.Threading.Tasks;
using HotFix_UI;
using SimpleJSON;
using UnityEngine;
using YooAsset;


namespace XFramework
{
    public class DemoEntry : XFEntry
    {
        public override void Dispose()
        {
            base.Dispose();
            ConfigManager.Instance.Dispose();
            //WebMessageHandler.Instance.Dispose();
            NetWorkManager.Instance.Dispose();
            ResourcesSingleton.Instance.Dispose();
            JsonManager.Instance.Dispose();
            RedDotManager.Instance.Dispose();
            PlayerSingleton.Instance.Dispose();
        }

        public override void Start()
        {
            Log.Debug($"DemoEntry");

            LoadAsync();
        }


        private async void LoadAsync()
        {
            var tables = new Tables();
            await tables.LoadAsync(Loader);
            ConfigManager.Instance.InitTables(tables);
            ConfigManager.Instance.SwitchLanguages(Tblanguage.L10N.zh_cn);

            base.Init();
            NetWorkManager.Instance.Init();
            ResourcesSingleton.Instance.Init();
            JsonManager.Instance.Init();
            RedDotManager.Instance.Init();
            PlayerSingleton.Instance.Init();

            var sceneController = Common.Instance.Get<SceneController>(); // 场景控制
            var sceneObj = sceneController.LoadSceneAsync<Login>(SceneName.Login);

            await SceneResManager.WaitForCompleted(sceneObj).ToCoroutine(); // 等待场景加载完毕
            //JiYuUIHelper.LoadSceneAsyncYoo(SceneName.Login).Forget();
        }

        private async UniTask<JSONNode> Loader(string name)
        {
            var package = YooAssets.GetPackage("DefaultPackage");

            AssetHandle handle =
                package.LoadAssetAsync<TextAsset>($"Assets/{Application.productName}/ConfigJsonData/" + name + ".json");

            await handle.ToUniTask();
            var textAsset = handle.AssetObject as TextAsset;

            string fileText = textAsset.text;


            return JSON.Parse(fileText);
        }
    }
}