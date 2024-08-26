using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    [MiniTweenType(nameof(Vector2))]
    public class Vector2Tween : MiniTween<Vector2>
    {
        protected override void AddElapsedTimeAfter()
        {
            var progress = base.Progress;
            var value = Vector2.Lerp(base.startValue, base.endValue, progress);

            base.setValue_Action?.Invoke(value);
        }

        protected override float GetDiffValue()
        {
            return Vector2.Distance(base.startValue, base.endValue);
        }
    }
}
