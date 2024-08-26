using System;
using Newtonsoft.Json;

namespace XFramework
{
    public class XObject : IDisposable
    {
        /// <summary>
        /// 标记Id，释放后归0，每次初始化后也会改变
        /// <para>异步操作时这很有用</para>
        /// </summary>
        [JsonIgnore]
        public long TagId { get; private set; }

        /// <summary>
        /// 是否已经销毁了，如果是走的对象池这个就不会准确
        /// </summary>
        [JsonIgnore]
        public bool IsDisposed => TagId == 0;

        /// <summary>
        /// 是否已经初始化过了
        /// </summary>
        [JsonIgnore]
        private bool isAwake = false;

        /// <summary>
        /// 是否来自对象池
        /// </summary>
        [JsonIgnore]
        private bool isFromPool = false;

        /// <summary>
        /// 是否设置过池子
        /// </summary>
        [JsonIgnore]
        private bool setFromPool = false;

        /// <summary>
        /// 之前的标记Id
        /// </summary>
        [JsonIgnore]
        private long beforeTagId = 0;

        /// <summary>
        /// 即将释放时
        /// </summary>
        [JsonIgnore]
        private Action _onDisposed;

        protected XObject()
        {

        }

        protected virtual void OnStart()
        {
            AddEvent();    //这句话写在这的原因是不强求预先注册事件
        }

        protected virtual void OnDestroy()
        {

        }

        internal void Awake()
        {
            if (isAwake)
                return;

            isAwake = true;
            TagId = beforeTagId + 1;
            if (TagId == 0)
                ++TagId;

            OnStart();
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            isAwake = false;
            beforeTagId = TagId;
            TagId = 0;

            if (this is IEvent)
                EventManager.Instance?.RemoveTarget(this);

            var cb = _onDisposed;
            ClearOnDisposed();  //先移除，再执行cb
            try
            {
                cb?.Invoke();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            try
            {
                OnDestroy();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            if (this is IDestroy o)
                EventManager.Instance?.Destroy(o);

            if (isFromPool)
            {
                ObjectPool.Instance.Recycle(this);
            }

            setFromPool = false;
        }

        /// <summary>
        /// 设置是否来自池子
        /// </summary>
        /// <param name="fromPool"></param>
        internal void SetFromPool(bool fromPool)
        {
            if (setFromPool)
                return;

            setFromPool = true;
            isFromPool = fromPool;
        }

        /// <summary>
        /// 添加本类的所有监听
        /// </summary>
        protected void AddEvent()
        {
            if (this is IEvent)
                EventManager.Instance?.AddTarget(this);
        }

        /// <summary>
        /// 添加释放时的监听，这个将会比<see cref="OnDestroy"/>先执行
        /// </summary>
        /// <param name="action"></param>
        protected void AddOnDisposed(Action action)
        {
            if (action != null)
                _onDisposed += action;
        }

        /// <summary>
        /// 移除释放时的监听
        /// </summary>
        /// <param name="action"></param>
        protected void RemoveOnDisposed(Action action)
        {
            if (action != null)
                _onDisposed -= action;
        }

        /// <summary>
        /// 清空释放时的监听
        /// </summary>
        protected void ClearOnDisposed()
        {
            _onDisposed = null;
        }
    }
}
