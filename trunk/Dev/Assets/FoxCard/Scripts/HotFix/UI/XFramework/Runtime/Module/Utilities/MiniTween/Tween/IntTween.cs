using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    [MiniTweenType(nameof(Int32))]
    public class IntTween : MiniTween<int>
    {
        protected override void AddElapsedTimeAfter()
        {
            var value = startValue + (endValue - startValue) * Progress;
            base.setValue_Action?.Invoke((int)value);
        }

        protected override float GetDiffValue()
        {
            return Math.Abs(base.startValue - base.endValue);
        }
    }
}
