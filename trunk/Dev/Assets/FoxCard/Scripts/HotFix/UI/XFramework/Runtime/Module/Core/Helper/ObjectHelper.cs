using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public static class ObjectUtils
    {
        /// <summary>
        /// 换值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="l"></param>
        /// <param name="r"></param>
        public static void Swap<T>(ref T l, ref T r)
        {
            (l, r) = (r, l);
        }
    }
}
