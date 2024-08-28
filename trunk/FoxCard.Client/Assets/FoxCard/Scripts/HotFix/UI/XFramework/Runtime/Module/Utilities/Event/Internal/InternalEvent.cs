using System;

namespace XFramework
{
    public interface IInternalEvent
    {
    }

    public interface IInternalEvent<T> : IInternalEvent where T : struct
    {
        void HandleEvent(T args);
    }

    public class InternalEvent
    {
        private UnOrderMapList<Type, object> allEvents = new UnOrderMapList<Type, object>();

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="target"></param>
        public void AddTarget(object target)
        {
            if (!(target is IInternalEvent))
                return;

            Type targetType = target.GetType();
            if (TypesManager.Instance.TryGetInternalEvents(targetType, out var types))
            {
                foreach (var argType in types)
                {
                    allEvents.TryAdd(argType, target);
                }
            }
        }

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="target"></param>
        /// <param name="argType"></param>
        public void AddListener(object target, Type argType)
        {
            if (!(target is IInternalEvent))
                return;

            Type targetType = target.GetType();
            if (!TypesManager.Instance.TryGetInternalEvents(targetType, out var types))
                return;

            if (types.Contains(argType))
            {
                allEvents.TryAdd(argType, target);
            }
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="target"></param>
        /// <param name="argType"></param>
        /// <returns></returns>
        public bool RemoveListener(object target, Type argType)
        {
            if (!(target is IInternalEvent))
                return false;

            Type targetType = target.GetType();
            if (!TypesManager.Instance.TryGetInternalEvents(targetType, out _))
                return false;

            return allEvents.Remove(argType, targetType);
        }

        /// <summary>
        /// 移除目标监听
        /// </summary>
        /// <param name="target"></param>
        public void RemoveTarget(object target)
        {
            if (!(target is IInternalEvent))
                return;

            Type targetType = target.GetType();
            if (TypesManager.Instance.TryGetInternalEvents(targetType, out var types))
            {
                foreach (var type in types)
                {
                    allEvents.Remove(type, target);
                }
            }
        }

        /// <summary>
        /// 移除所有监听
        /// </summary>
        public void RemoveAllListeners()
        {
            allEvents.Clear();
        }

        /// <summary>
        /// 推送事件
        /// </summary>
        /// <param name="args"></param>
        public void Publish<T>(T args) where T : struct
        {
            Type type = typeof(T);
            if (!allEvents.TryGetValue(type, out var list))
                return;

            using var handleList = XList<object>.Create();
            handleList.AddRange(list);

            foreach (var o in handleList)
            {
                try
                {
                    // 防止在执行事件的时候同时添加移除事件，导致报错
                    // 如果在执行第1个事件的时候，删除了第3个事件，那么第3个事件就不能被执行了
                    if (!list.Contains(o))
                        continue;

                    (o as IInternalEvent<T>).HandleEvent(args);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }
    }
}