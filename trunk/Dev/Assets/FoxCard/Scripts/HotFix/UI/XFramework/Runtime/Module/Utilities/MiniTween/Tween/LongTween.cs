using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    [MiniTweenType(nameof(Int64))]
    public class LongTween : MiniTween<long>
    {
        protected override void AddElapsedTimeAfter()
        {
            var value = startValue + (endValue - startValue) * Progress;
            base.setValue_Action?.Invoke((long)value);
        }

        protected override float GetDiffValue()
        {
            return Math.Abs(base.startValue - base.endValue);
        }
    }
}
