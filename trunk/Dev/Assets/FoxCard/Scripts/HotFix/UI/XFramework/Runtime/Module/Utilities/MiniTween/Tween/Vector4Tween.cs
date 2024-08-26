using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    [MiniTweenType(nameof(Vector4))]
    public class Vector4Tween : MiniTween<Vector4>
    {
        protected override void AddElapsedTimeAfter()
        {
            var progress = base.Progress;
            var value = Vector4.Lerp(base.startValue, base.endValue, progress);

            base.setValue_Action?.Invoke(value);
        }

        protected override float GetDiffValue()
        {
            return Vector3.Distance(base.startValue, base.endValue);
        }
    }
}
