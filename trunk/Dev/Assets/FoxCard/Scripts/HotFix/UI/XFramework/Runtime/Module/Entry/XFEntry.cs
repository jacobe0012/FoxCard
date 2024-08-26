
using HotFix_UI;


namespace XFramework
{
    public class XFEntry : IEntry
    {
        public virtual void Dispose()
        {
            EventManager.Instance.Dispose();
            TypesManager.Instance.Dispose();
            Common.Instance.Dispose();
            ObjectPool.Instance.Dispose();
            GameObjectPool.Instance.Dispose();
            TimeInfo.Instance.Dispose();
            ResourcesManager.Instance.Dispose();
            SceneResManager.Instance.Dispose();
            // ConfigManager.Instance.Dispose();
            // WebMessageHandler.Instance.Dispose();
            // NetWorkManager.Instance.Dispose();
          
            //RedPointMgr.instance.Dispose();
            ResourcesSingleton.Instance.Dispose();
            JsonManager.Instance.Dispose();
            PlayerSingleton.Instance.Dispose();
        }

        public virtual void Update()
        {
            Common.Instance.Update();
        }

        public virtual void LateUpdate()
        {
            Common.Instance.LateUpdate();
        }

        public virtual void FixedUpdate()
        {
            Common.Instance.FixedUpdate();
        }

        public virtual void Start()
        {
            Init();
        }

        protected void Init()
        {
            ResourcesManager.Instance.SetLoader(new YooResourcesLoader()); // 资源管理，设置加载方式
            SceneResManager.Instance.SetLoader(new YooSceneLoader()); // 场景资源管理，设置加载方式


            TimeInfo.Instance.Init(); // 时间管理
            TypesManager.Instance.Init(); // 类型集合，比如存储标记了特性的类
            TypesManager.Instance.Add(this.GetType().Assembly); // 将自己的程序集添加进去，这一步很重要，自己有多少个程序集就添加几个
            ObjectPool.Instance.Init(); // 类对象池
            GameObjectPool.Instance.Init(); // GameObject对象池


            //加载资源前要先创建这两个
            ObjectFactory.Create<ResourcesRefDetection>(); // 资源绑定和自动回收，资源管理的关键类
            //ObjectFactory.Create<ConfigManager1>(); // 配置表管理
            ObjectFactory.Create<Global>(); // 全局对象
            ObjectFactory.Create<TimerManager>(); // 定时器
            ObjectFactory.Create<AudioManager>(); // 音频管理
            ObjectFactory.Create<MiniTweenManager>(); // 补间动画
            ObjectFactory.Create<LanguageManager>(); // 多语言
            ObjectFactory.Create<UIEventManager>(); // UIEvent集合
            ObjectFactory.Create<UIManager>(); // UI管理
            ObjectFactory.Create<SceneController>(); // 场景控制
            //ObjectFactory.Create<UserDataManager>(); // 本地存档管理
            //ObjectFactory.Create<RedDotManager>(); // 红点管理
            ResourcesSingleton.Instance.Init();
            PlayerSingleton.Instance.Init();
            JsonManager.Instance.Init();
        }
    }
}