using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public class XQueue<T> : Queue<T>, IDisposable
    {
        public static XQueue<T> Create()
        {
            return ObjectPool.Instance.Fetch<XQueue<T>>();
        }

        public void Dispose()
        {
            this.Clear();
            ObjectPool.Instance.Recycle(this);
        }
    }
}
