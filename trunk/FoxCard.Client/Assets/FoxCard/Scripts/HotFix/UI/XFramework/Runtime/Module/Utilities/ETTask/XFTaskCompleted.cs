using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XFramework
{
    [AsyncMethodBuilder(typeof(AsyncETTaskCompletedMethodBuilder))]
    public struct XFTaskCompleted : ICriticalNotifyCompletion
    {
        [DebuggerHidden]
        public XFTaskCompleted GetAwaiter()
        {
            return this;
        }

        [DebuggerHidden] public bool IsCompleted => true;

        [DebuggerHidden]
        public void GetResult()
        {
        }

        [DebuggerHidden]
        public void OnCompleted(Action continuation)
        {
        }

        [DebuggerHidden]
        public void UnsafeOnCompleted(Action continuation)
        {
        }
    }
}