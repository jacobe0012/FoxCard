using System;

namespace XFramework
{
    public interface IDestroy
    {
    }

    public interface IDestroySystem : ISystemType, IObjectType
    {
        void Run(object obj);
    }

    [ObjectSystem]
    public abstract class DestroySystem<T> : IDestroySystem where T : IDestroy
    {
        public Type GetObjectType()
        {
            return typeof(T);
        }

        public Type GetSystemType()
        {
            return typeof(IDestroySystem);
        }

        public void Run(object obj)
        {
            Destroy((T)obj);
        }

        protected abstract void Destroy(T self);
    }
}