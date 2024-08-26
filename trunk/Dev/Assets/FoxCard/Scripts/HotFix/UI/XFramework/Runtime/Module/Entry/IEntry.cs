using System;


namespace XFramework
{
    public interface IEntry : IDisposable, IUpdate, ILateUpdate, IFixedUpdate
    {
        void Start();
    }
}