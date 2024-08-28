using System.Collections.Generic;

namespace XFramework
{
    public static class DictionaryExtensions
    {
        public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, out TValue result)
        {
            result = default;

            if (self.IsReadOnly)
                return false;

            if (self.TryGetValue(key, out result))
                return self.Remove(key);

            return false;
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
        {
            self.TryGetValue(key, out TValue value);
            return value;
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value)
        {
            if (self.IsReadOnly)
                return false;

            if (self.ContainsKey(key))
                return false;

            self.Add(key, value);
            return true;
        }

        public static void AddValue<TKey>(this IDictionary<TKey, int> self, TKey key, int value)
        {
            self.TryGetValue(key, out var v);
            self[key] = v + value;
        }

        public static void AddValue<TKey>(this IDictionary<TKey, long> self, TKey key, long value)
        {
            self.TryGetValue(key, out var v);
            self[key] = v + value;
        }

        public static void AddValue<TKey>(this IDictionary<TKey, float> self, TKey key, float value)
        {
            self.TryGetValue(key, out var v);
            self[key] = v + value;
        }

        public static void AddValue<TKey>(this IDictionary<TKey, double> self, TKey key, double value)
        {
            self.TryGetValue(key, out var v);
            self[key] = v + value;
        }
    }
}