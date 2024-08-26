using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    [UIEvent(UIType.UILoading)]
    internal sealed class UILoadingEvent : AUIEvent, IUILayer
    {
        public override string Key => UIPathSet.UILoading;

        public override bool IsFromPool => true;

        public override bool AllowManagement => true;

        public UILayer Layer => UILayer.High;

        public override UI OnCreate()
        {
            return UI.Create<UILoading>(true);
        }
    }

    public partial class UILoading : UI, IAwake<ILoading>, IWaitObject
	{
        private ILoading m_loading;

        /// <summary>
        /// 场景加载的最大进度
        /// </summary>
        private const int SceneMaxProgress = 10;

        /// <summary>
        /// 场景加载当前进度
        /// </summary>
        private float sceneProgress;

        /// <summary>
        /// 当前加载的资源数
        /// </summary>
        private int curCount;

        /// <summary>
        /// 当前加载的总个数
        /// </summary>
        private int totalCount;

        /// <summary>
        /// 上一次更新的进度
        /// </summary>
        private float beforeProgress;

        /// <summary>
        /// 要实例化的预制的key
        /// </summary>
        private List<string> objKeys = new List<string>();

        private long timerId;

        private MiniTween tween;

        Dictionary<System.Type, object> IWaitObject.WaitDict { get; set; }

        public void Initialize(ILoading loadArg)
		{
            this.m_loading = loadArg;
            loadArg.GetObjects(objKeys);
            this.totalCount = objKeys.Count + SceneMaxProgress;

            this.GetImage(KFill).SetFillAmount(0);
            this.GetText(KProgress).SetText(string.Empty);

            //开启一个每帧执行的任务，相当于Update
            var timerMgr = TimerManager.Instance;
            timerId = timerMgr.RepeatedFrameTimer(this.Update);

            //开始加载资源
            this.LoadAssets().Coroutine();
		} 
        
        private void Update()
        {
            float progress = this.m_loading.SceneProgress();
            this.sceneProgress = progress * SceneMaxProgress;
            this.DoFillAmount().Coroutine();

            if (this.sceneProgress >= SceneMaxProgress)
            {
                this.RemoveTimer();
            }
        }

        /// <summary>
        /// 加载所有资源
        /// </summary>
        /// <returns></returns>
        private async XFTask LoadAssets()
        {
            using var tasks = XList<XFTask>.Create();
            var timerMgr = TimerManager.Instance;
            var tagId = this.TagId;

            Transform parent = Common.Instance.Get<Global>().GameRoot;
            foreach (var key in this.objKeys)
            {
                tasks.Add(this.LoadObjectAsync(key, parent));
            }

            //等待所有资源加载完成
            await XFTaskHelper.WaitAll(tasks);
            //因为是异步操作，可能在异步时这个类被释放了，所以要用tagId判断一下
            //如果这个类来自对象池，那么tagId每次取出来都会变化
            //所以用tagId判断这个类是否有效是稳妥的方式
            if (tagId != this.TagId)    
                return;

            //等待场景的进度满
            while (this.sceneProgress < SceneMaxProgress)
            {
                await timerMgr.WaitFrameAsync();
                if (tagId != this.TagId)
                    return;
            }

            if (this.tween != null)
            {
                //等待进度条动画完成
                if (!this.tween.IsCompelted)
                {
                    await this.tween.Task;
                    if (tagId != this.TagId)
                        return;
                }
            }

            //延迟50毫秒
            await timerMgr.WaitAsync(50);
            if (tagId != this.TagId)
                return;

            //资源加载完毕，关闭Loading
            this.Close();
        }

        /// <summary>
        /// 实例化GameObject
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private async XFTask LoadObjectAsync(string key, Transform parent)
        {
            var tagId = this.TagId;
            GameObject obj = await ResourcesManager.InstantiateAsync(this, key, parent, true);
            ResourcesManager.ReleaseInstance(obj);
            if (tagId != this.TagId)
                return;

            ++curCount;
            this.DoFillAmount().Coroutine();
        }

        /// <summary>
        /// 丝滑变化进度条
        /// </summary>
        /// <returns></returns>
        private async XFTask DoFillAmount()
        {
            float count = this.curCount + this.sceneProgress;
            if (count == this.beforeProgress)
                return;

            this.beforeProgress = count;
            float t = this.beforeProgress / this.totalCount;

            this.tween?.Cancel(this);
            var fill = this.GetImage(KFill);
            var txt = this.GetText(KProgress);
            var tweenMgr = Common.Instance.Get<MiniTweenManager>();
            var miniTween = tweenMgr.To(this, fill.GetFillAmount(), t, 10f, MiniTweenMode.Speed);
            miniTween.AddListener(v =>
            {
                fill.SetFillAmount(v);
                txt.SetTextWithKey("{0:F0}%", v * 100);
            });
            this.tween = miniTween;
            await this.tween.Task;
        }

        /// <summary>
        /// 移除定时器
        /// </summary>
        private void RemoveTimer()
        {
            var timerMgr = TimerManager.Instance;
            timerMgr?.RemoveTimerId(ref this.timerId);
            this.timerId = 0;
        }
		
		protected override void OnClose()
		{
            this.m_loading = null;
            this.RemoveTimer();
            this.curCount = 0;
            this.totalCount = 0;
            this.beforeProgress = 0;
            this.sceneProgress = 0;
            this.objKeys.Clear();
            this.tween = null;
        }
    }
}
