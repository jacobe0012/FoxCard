﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XFramework
{
    [AsyncMethodBuilder(typeof(AsyncETVoidMethodBuilder))]
    internal struct XFVoid : ICriticalNotifyCompletion
    {
        [DebuggerHidden]
        public void Coroutine()
        {
        }

        [DebuggerHidden] public bool IsCompleted => true;

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