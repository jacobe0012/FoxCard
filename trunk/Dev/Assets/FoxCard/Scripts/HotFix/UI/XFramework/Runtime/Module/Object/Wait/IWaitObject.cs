using System;
using System.Collections.Generic;

namespace XFramework
{
    public interface IWaitType
    {
        int Error { get; set; }
    }

    public static class WaitTypeError
    {
        public const int Success = 0;
        public const int Destroy = 1;
        public const int Cancel = 2;
        public const int Timeout = 3;
    }

    internal sealed class WaitObjectDestroy : DestroySystem<IWaitObject>
    {
        protected override void Destroy(IWaitObject self)
        {
            self.Destory();
        }
    }

    internal interface IWaitDestroy
    {
        void Destroy();
    }

    internal sealed class WaitCallback<T> : IWaitDestroy where T : struct, IWaitType
    {
        private XFTask<T> tcs = XFTask<T>.Create(true);

        internal XFTask<T> Task => tcs;

        internal bool IsDisposed => tcs is null;

        internal void Notify(T value)
        {
            if (IsDisposed)
                return;

            var tcs = this.tcs;
            this.tcs = null;
            tcs.SetResult(value);
        }

        public void Destroy()
        {
            Notify(new T() { Error = WaitTypeError.Destroy });
        }
    }

    public interface IWaitObject : IDestroy
    {
        Dictionary<Type, object> WaitDict { get; set; }
    }

    public static class WaitObjectExtensions
    {
        /// <summary>
        /// 开始等待
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async XFTask<TResult> Wait<TResult>(this IWaitObject self, XCancellationToken cancellationToken = null) where TResult : struct, IWaitType
        {
            self.WaitDict ??= new Dictionary<Type, object>();
            var type = typeof(TResult);
            var tcs = new WaitCallback<TResult>();
            self.AddWaitInfo(type, tcs);

            void Cancel()
            {
                self.Notify(new TResult { Error = WaitTypeError.Cancel });
            }

            TResult ret;
            try
            {
                cancellationToken?.Register(Cancel);
                ret = await tcs.Task;
            }
            finally
            {
                cancellationToken?.Remove(Cancel);
            }

            return ret;
        }

        /// <summary>
        /// 开始等待
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async XFTask<TResult> Wait<TResult>(this IWaitObject self, long timeout, XCancellationToken cancellationToken = null) where TResult : struct, IWaitType
        {
            self.WaitDict ??= new Dictionary<Type, object>();
            var type = typeof(TResult);
            var tcs = new WaitCallback<TResult>();
            self.AddWaitInfo(type, tcs);

            async XFTask WaitTimeout()
            {
                bool ret = await TimerManager.Instance.WaitAsync(timeout, cancellationToken);
                if (!ret)
                    return;

                if (tcs.IsDisposed)
                    return;

                self.Notify(new TResult { Error = WaitTypeError.Timeout });
            }

            // 等待超时
            WaitTimeout().Coroutine();

            void Cancel()
            {
                self.Notify(new TResult { Error = WaitTypeError.Cancel });
            }

            TResult ret;
            try
            {
                cancellationToken?.Register(Cancel);
                ret = await tcs.Task;
            }
            finally
            {
                cancellationToken?.Remove(Cancel);
            }

            return ret;
        }

        /// <summary>
        /// 通知完成
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="value"></param>
        public static void Notify<TResult>(this IWaitObject self, TResult value) where TResult : struct, IWaitType
        {
            if (self.WaitDict is null)
                return;

            if (self.WaitDict.TryRemove(typeof(TResult), out var tcs))
            {
                ((WaitCallback<TResult>)tcs).Notify(value);
            }
        }

        public static void Destory(this IWaitObject self)
        {
            if (self.WaitDict is null)
                return;

            foreach (var value in self.WaitDict.Values)
            {
                try
                {
                    ((IWaitDestroy)value).Destroy();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            self.WaitDict.Clear();
        }

        private static void AddWaitInfo(this IWaitObject self, Type type, object obj)
        {
            self.WaitDict ??= new Dictionary<Type, object>();
            self.WaitDict.Add(type, obj);
        }
    }
}
