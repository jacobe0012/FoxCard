using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public class XStack<T> : Stack<T>, IDisposable
    {
        public static XStack<T> Create()
        {
            return ObjectPool.Instance.Fetch<XStack<T>>();
        }

        public void Dispose()
        {
            this.Clear();
            ObjectPool.Instance.Recycle(this);
        }
    }
}
