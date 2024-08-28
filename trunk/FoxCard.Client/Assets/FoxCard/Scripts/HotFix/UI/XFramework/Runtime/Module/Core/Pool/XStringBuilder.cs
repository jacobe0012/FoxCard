using System;
using System.Text;

namespace XFramework
{
    public class XStringBuilder : IDisposable
    {
        private StringBuilder sb = new StringBuilder();

        public StringBuilder Get() => sb;

        public static XStringBuilder Create()
        {
            return ObjectPool.Instance.Fetch<XStringBuilder>();
        }

        public void Dispose()
        {
            sb.Clear();
            ObjectPool.Instance.Recycle(this);
        }
    }
}