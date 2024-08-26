using System;
using System.Collections;
using System.Collections.Generic;

namespace XFramework
{
    public class XDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IDisposable
    {
        public static XDictionary<TKey, TValue> Create()
        {
            return ObjectPool.Instance.Fetch<XDictionary<TKey, TValue>>();
        }

        public void Dispose()
        {
            this.Clear();
            ObjectPool.Instance.Recycle(this);
        }
    }
}
