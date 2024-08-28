using System;
using System.Collections.Generic;

namespace XFramework
{
    /// <summary>
    /// MiniTween基于什么模式驱动
    /// </summary>
    public enum MiniTweenMode
    {
        /// <summary>
        /// 持续时间
        /// </summary>
        Duration = 1,

        /// <summary>
        /// 速度
        /// </summary>
        Speed = 2,
    }

    public sealed class MiniTweenProvider : IDisposable
    {
        /// <summary>
        /// 所有支持的动画参数类型
        /// </summary>
        private readonly static Dictionary<string, Type> tweenTypes = new Dictionary<string, Type>();

        private ITimeNow timeGetter;

        private Dictionary<long, MiniTween> tweenDict = new Dictionary<long, MiniTween>();

        /// <summary>
        /// 所有正在执行的动画Id
        /// </summary>
        private Queue<long> allTweens = new Queue<long>();

        /// <summary>
        /// 上次执行的时间
        /// </summary>
        private long lastNow;

        static MiniTweenProvider()
        {
            var types = TypesManager.Instance.GetTypes(typeof(MiniTweenTypeAttribute));
            foreach (var type in types)
            {
                var attris = type.GetCustomAttributes(typeof(MiniTweenTypeAttribute), false);
                if (attris.Length == 0)
                    continue;

                MiniTweenTypeAttribute attri = attris[0] as MiniTweenTypeAttribute;
                tweenTypes[attri.TypeName] = type;
            }
        }

        public MiniTweenProvider(ITimeNow args)
        {
            timeGetter = args;
            lastNow = timeGetter.GetTime();
        }

        public void Dispose()
        {
            using var list = XList<MiniTween>.Create();
            list.AddRange(tweenDict.Values);
            foreach (var tween in list)
            {
                tween?.Dispose();
            }

            this.tweenDict.Clear();
            this.allTweens.Clear();
            this.lastNow = 0;
            this.timeGetter = null;
        }

        public void Update()
        {
            var now = timeGetter.GetTime();
            float deltaTime = (now - lastNow) / 1000f;
            lastNow = now;

            int count = allTweens.Count;
            while (count-- > 0)
            {
                var id = allTweens.Dequeue();
                var tween = Get(id);
                if (tween != null && !tween.IsDisposed)
                {
                    allTweens.Enqueue(id);
                    try
                    {
                        tween.AddElapsedTime(deltaTime);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }
        }

        /// <summary>
        /// 获取一个MiniTween，如果Tween被取消或者已完成则返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MiniTween Get(long id)
        {
            return tweenDict.Get(id);
        }

        /// <summary>
        /// 获取一个MiniTween，如果Tween被取消/已完成/类型不匹配则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public MiniTween<T> Get<T>(long id)
        {
            return Get(id) as MiniTween<T>;
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
            string typeName = typeof(T).Name;
            if (!tweenTypes.TryGetValue(typeName, out var tweenType))
            {
                Log.Error($"MiniTween.To This type({typeof(T)}) is not supported");
                return null;
            }

            switch (tweenMode)
            {
                case MiniTweenMode.Duration:
                    if (arg <= 0)
                    {
                        Log.Error("MiniTween.To duration <= 0.");
                        return null;
                    }

                    break;
                case MiniTweenMode.Speed:
                    if (arg == 0)
                    {
                        Log.Error("MiniTween.To Speed == 0.");
                        return null;
                    }

                    break;
                default:
                    Log.Error($"不支持的TweenMode -> {tweenMode.ToString()}");
                    return null;
            }

            MiniTween<T> tween =
                ObjectFactory.Create(tweenType, parent, startValue, endValue, arg, tweenMode, true) as MiniTween<T>;
            this.Add(tween);

            return tween;
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
            return To(parent, startValue, endValue, duration, MiniTweenMode.Duration);
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
            return To(parent, default, endValue, duration);
        }

        private void Add(MiniTween tween)
        {
            if (tween.IsDisposed)
                return;

            tween.SetProvider(this);
            this.tweenDict.Add(tween.InstanceId, tween);
            this.allTweens.Enqueue(tween.InstanceId);
        }

        internal void Remove(MiniTween tween)
        {
            this.tweenDict.Remove(tween.InstanceId);
        }
    }
}