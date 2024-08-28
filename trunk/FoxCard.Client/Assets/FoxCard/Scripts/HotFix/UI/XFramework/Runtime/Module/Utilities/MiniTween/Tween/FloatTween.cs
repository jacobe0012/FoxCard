using System;

namespace XFramework
{
    [MiniTweenType(nameof(Single))]
    public class FloatTween : MiniTween<float>
    {
        protected override void AddElapsedTimeAfter()
        {
            var value = startValue + (endValue - startValue) * Progress;
            base.setValue_Action?.Invoke(value);
        }

        protected override float GetDiffValue()
        {
            return Math.Abs(base.startValue - base.endValue);
        }
    }
}