namespace XFramework
{
    public sealed class MiniTweenManager : CommonObject, IAwake, IAwake<ITimeNow>, IUpdate
    {
        /// <summary>
        /// tween提供者
        /// </summary>
        private MiniTweenProvider tweenProvider;

        private static MiniTweenManager _instance;

        public static MiniTweenManager Instance => _instance;

        void IAwake.Initialize()
        {
            (this as IAwake<ITimeNow>).Initialize(new ActualTime());
        }

        void IAwake<ITimeNow>.Initialize(ITimeNow timeNow)
        {
            tweenProvider = new MiniTweenProvider(timeNow);
        }

        protected override void Init()
        {
            base.Init();
            _instance = this;
        }

        protected override void Destroy()
        {
            _instance = null;
            tweenProvider?.Dispose();
            tweenProvider = null;
        }

        void IUpdate.Update()
        {
            tweenProvider?.Update();
        }

        public MiniTween Get(long id)
        {
            return tweenProvider?.Get(id);
        }

        public MiniTween<T> Get<T>(long id)
        {
            return tweenProvider?.Get<T>(id);
        }

        /// <summary>
        /// 执行动画
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">依赖的父类</param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">目标值</param>
        /// <param name="arg">参数</param>
        /// <param name="tweenMode">驱动模式</param>
        /// <returns></returns>
        public MiniTween<T> To<T>(XObject parent, T startValue, T endValue, float arg, MiniTweenMode tweenMode)
        {
            return tweenProvider.To(parent, startValue, endValue, arg, tweenMode);
        }

        /// <summary>
        /// 以持续时间模式来执行动画
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">依赖的父类</param>
        /// <param name="startValue">起始值</param>
        /// <param name="endValue">目标值</param>
        /// <param name="duration">持续时间</param>
        /// <returns></returns>
        public MiniTween<T> To<T>(XObject parent, T startValue, T endValue, float duration)
        {
            return tweenProvider.To(parent, startValue, endValue, duration, MiniTweenMode.Duration);
        }

        /// <summary>
        /// 以持续时间模式来执行动画, startValue为默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">依赖的父类</param>
        /// <param name="endValue">目标值</param>
        /// <param name="duration">持续时间</param>
        /// <returns></returns>
        public MiniTween<T> To<T>(XObject parent, T endValue, float duration)
        {
            return tweenProvider.To(parent, default, endValue, duration);
        }
    }
}