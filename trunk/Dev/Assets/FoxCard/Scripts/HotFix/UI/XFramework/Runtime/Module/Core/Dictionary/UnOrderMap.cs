using System.Collections.Generic;

namespace XFramework
{
    public class UnOrderMap<TKey1, TKey2, TValue> : Dictionary<TKey1, Dictionary<TKey2, TValue>>
    {
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get
            {
                TValue value;
                TryGetValue(key1, key2, out value);

                return value;
            }
        }

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            if (!base.TryGetValue(key1, out var dict))
            {
                dict = new Dictionary<TKey2, TValue>();
                base.Add(key1, dict);
            }

            if (!dict.ContainsKey(key2))
            {
                dict.Add(key2, value);
            }
        }

        public bool Remove(TKey1 key1, TKey2 key2)
        {
            if (TryGetValue(key1, out var dict))
            {
                if (dict.Remove(key2))
                {
                    if (dict.Count == 0)
                        base.Remove(key1);

                    return true;
                }
            }

            return false;
        }

        public bool TryGetValue(TKey1 key1, TKey2 key2, out TValue result)
        {
            result = default;

            if (base.TryGetValue(key1, out var dict))
            {
                if (dict.TryGetValue(key2, out result))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2)
        {
            if (base.TryGetValue(key1, out var dict))
            {
                return dict.ContainsKey(key2);
            }

            return false;
        }
    }
}