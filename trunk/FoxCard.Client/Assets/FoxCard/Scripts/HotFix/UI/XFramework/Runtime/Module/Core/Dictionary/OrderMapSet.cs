using System.Collections.Generic;

namespace XFramework
{
    public class OrderMapSet<TKey, TValue> : SortedDictionary<TKey, HashSet<TValue>>
    {
        public bool Add(TKey key, TValue value)
        {
            if (!base.TryGetValue(key, out var list))
            {
                list = new HashSet<TValue>();
                base.Add(key, list);
            }

            return list.Add(value);
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
    }
}