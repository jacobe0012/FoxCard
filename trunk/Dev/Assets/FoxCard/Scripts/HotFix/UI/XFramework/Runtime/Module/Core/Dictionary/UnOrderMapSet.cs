using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public class UnOrderMapSet<TKey, TValue> : Dictionary<TKey, HashSet<TValue>>
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

        public bool Contains(TKey key, TValue value)
        {
            if (base.TryGetValue(key, out var list))
            {
                return list.Contains(value);
            }

            return false;
        }
    }
}
