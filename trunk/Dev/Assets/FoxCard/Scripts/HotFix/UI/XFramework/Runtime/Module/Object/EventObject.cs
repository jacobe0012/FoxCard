using System;

namespace XFramework
{
    /// <summary>
    /// 用于类的内部通信
    /// </summary>
    public class EventObject : XObject
    {
        private InternalEvent _events = new InternalEvent();

        protected override void OnStart()
        {
            base.OnStart();
            _events.AddTarget(this);
            AddOnDisposed(Clear);
        }

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="target"></param>
        public void AddTarget(object target)
        {
            _events.AddTarget(target);
        }

        /// <summary>
        /// 移除目标监听
        /// </summary>
        /// <param name="target"></param>
        public void RemoveTarget(object target)
        {
            _events.RemoveTarget(target);
        }

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="target"></param>
        /// <param name="argType"></param>
        public void AddListener(object target, Type argType)
        {
            _events.AddListener(target, argType);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="target"></param>
        /// <param name="argType"></param>
        /// <returns></returns>
        public bool RemoveListener(object target, Type argType)
        {
            return _events.RemoveListener(target, argType);
        }

        /// <summary>
        /// 移除所有监听
        /// </summary>
        public void RemoveAllListeners()
        {
            _events.RemoveAllListeners();
        }

        /// <summary>
        /// 推送事件
        /// </summary>
        /// <param name="args"></param>
        public void Publish<T>(T args) where T : struct
        {
            _events.Publish(args);
        }

        private void Clear()
        {
            _events.RemoveAllListeners();
        }
    }
}