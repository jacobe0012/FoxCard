using System;
using System.Collections.Generic;

namespace XFramework
{
    public class XHashSet<T> : HashSet<T>, IDisposable
    {
        public static XHashSet<T> Create()
        {
            return ObjectPool.Instance.Fetch<XHashSet<T>>();
        }

        public void Dispose()
        {
            this.Clear();
            ObjectPool.Instance.Recycle(this);
        }
    }
}