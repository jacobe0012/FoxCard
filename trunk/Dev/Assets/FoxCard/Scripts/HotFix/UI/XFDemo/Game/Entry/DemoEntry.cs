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
        }

        public override void Start()
        {
            //NetWorkManager.Instance.Init();
            LoadAsync();
            InitUnitySetting();
            //NetWorkManager.Instance.Init();
        }


        void InitUnitySetting()
        {
            Input.multiTouchEnabled = false;
            Application.runInBackground = true;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = -1;
#if UNITY_ANDROID || UNITY_IOS
            int maxRefreshRate = Screen.currentResolution.refreshRate;
            Application.targetFrameRate = maxRefreshRate;
#endif
        }

        private async void LoadAsync()
        {
            // var cover = UnityEngine.GameObject.Find("Cover");
            // cover?.SetViewActive(true);
            //
            // var initBackGround = UnityEngine.GameObject.Find("InitBackGround");
            // initBackGround?.SetActive(false);
            //
            // var reporter = UnityEngine.GameObject.Find("Reporter");
            // reporter?.SetActive(false);




            var tables = new Tables();
            await tables.LoadAsync(Loader);

            ConfigManager.Instance.InitTables(tables);

            ConfigManager.Instance.SwitchLanguages(Tblanguage.L10N.zh_cn);

            base.Init();
            // entityManager.AddComponent<WorldBlackBoardTag>(entity);
            // var configMgr = Common.Instance.Get<ConfigManager>(); // 配置表管理
            // configMgr.SetLoader(new AAConfigLoader()); // 设置配置加载方式
            // await configMgr.LoadAllConfigsAsync(); // 等待配置表加载完毕才能执行下面的内容

            // Common.Instance.Get<LanguageManager>().SetLoader(new XLanguageLoader()); // 设置多语言加载方式
            // UserDataHelper.SetCreateUser(UserHelper.CreateUser); // 设置创建数据方法，只有在没有存档的时候才会执行创建User
            // await UserDataHelper.LoadAsync(); //加载存档，不一定要写在这，写在你需要的地方即可
            //
            //Common.Instance.Get<RedDotManager>().InitAllRedNodes(); //初始化红点系统，红点刷新肯定依赖数据，所以得在数据初始化执行
            Log.Debug($"DemoEntry1");

            var sceneController = Common.Instance.Get<SceneController>(); // 场景控制
            var sceneObj = sceneController.LoadSceneAsync<Login>(SceneName.Login);

            await SceneResManager.WaitForCompleted(sceneObj).ToCoroutine(); // 等待场景加载完毕
            //JiYuUIHelper.LoadSceneAsyncYoo(SceneName.Login).Forget();

            Log.Debug($"DemoEntry2");
            //cover?.SetViewActive(false);
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