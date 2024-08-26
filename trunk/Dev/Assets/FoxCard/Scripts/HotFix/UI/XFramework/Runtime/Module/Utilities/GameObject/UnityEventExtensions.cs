using UnityEngine.Events;

namespace XFramework
{
    public static class UnityEventExtensions
    {
        public static void Set(this UnityEvent self, UnityAction action)
        {
            self.Clear();
            if (action != null)
                self.Add(action);
        }

        public static void Set<T>(this UnityEvent<T> self, UnityAction<T> action)
        {
            self.Clear();
            if (action != null)
                self.Add(action);
        }

        public static void Add(this UnityEvent self, UnityAction action)
        {
            self.AddListener(action);
        }

        public static void Add<T>(this UnityEvent<T> self, UnityAction<T> action)
        {
            self.AddListener(action);
        }

        public static void Remove(this UnityEvent self, UnityAction action)
        {
            self.RemoveListener(action);
        }

        public static void Remove<T>(this UnityEvent<T> self, UnityAction<T> action)
        {
            self.RemoveListener(action);
        }

        public static void Clear(this UnityEvent self)
        {
            self.RemoveAllListeners();
        }

        public static void Clear<T>(this UnityEvent<T> self)
        {
            self.RemoveAllListeners();
        }
    }
}
