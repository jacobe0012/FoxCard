using System;
using System.Collections.Generic;

namespace XFramework
{
    public class XList<T> : List<T>, IDisposable
    {
        public static XList<T> Create()
        {
            return ObjectPool.Instance.Fetch<XList<T>>();
        }

        public void Dispose()
        {
            this.Clear();
            ObjectPool.Instance.Recycle(this);
        }
    }
}