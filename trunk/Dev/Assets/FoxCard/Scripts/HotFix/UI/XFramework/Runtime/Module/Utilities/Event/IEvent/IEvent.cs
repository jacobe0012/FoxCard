using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public interface IEvent
    {

    }

    public interface IEvent<T> : IEvent where T : struct
    {
        void HandleEvent(T args);
    }
}
