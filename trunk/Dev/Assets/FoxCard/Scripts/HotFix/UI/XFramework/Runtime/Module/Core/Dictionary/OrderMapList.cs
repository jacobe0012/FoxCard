using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public class OrderMapList<TKey, TValue> : SortedDictionary<TKey, List<TValue>>
    {
        /// <summary>
        /// 尝试添加，若包含了则不添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAdd(TKey key, TValue value)
        {
            if (!base.TryGetValue(key, out var list))
            {
                list = new List<TValue>();
                base.Add(key, list);
            }

            if (!list.Contains(value))
            {
                list.Add(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 直接添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if (!base.TryGetValue(key, out var list))
            {
                list = new List<TValue>();
                base.Add(key, list);
            }
            list.Add(value);
        }

        /// <summary>
        /// 返回第一个value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue GetOne(TKey key)
        {
            if (base.TryGetValue(key, out var list))
            {
                return list.Count > 0 ? list[0] : default;
            }

            return default;
        }

        public bool Remove(TKey key, TValue value)
        {
            if (base.TryGetValue(key, out var list))
            {
                if (list.Remove(value))
                {
                    if (list.Count == 0)
                        base.Remove(key);

                    return true;
                }
            }

            return false;
        }

        public void RemoveAt(TKey key, int index)
        {
            if (base.TryGetValue(key, out var list))
            {
                list.RemoveAt(index);
                if (list.Count == 0)
                    base.Remove(key);
            }
        }
    }
}
