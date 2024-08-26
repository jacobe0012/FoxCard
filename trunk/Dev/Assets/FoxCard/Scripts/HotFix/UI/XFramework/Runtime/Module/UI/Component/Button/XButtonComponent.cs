using UnityEngine;
using UnityEngine.Events;

namespace XFramework
{
    public abstract class XButtonComponent<T> : ButtonComponent<T> where T : XButton
    {
        /// <summary>
        /// 长按事件
        /// </summary>
        public UnityEvent<int> OnLongPress => this.Get().onLongPress;

        /// <summary>
        /// 长按结束时事件
        /// </summary>
        public UnityEvent<int> OnLongPressEnd => this.Get().onLongPressEnd;

        /// <summary>
        /// 长按结束时事件
        /// </summary>
        public UnityEvent OnPointerExit => this.Get().onPointerExit;

        /// <summary>
        /// 长按间隔
        /// </summary>
        public float LongPressInterval => this.Get().LongPressInterval;

        /// <summary>
        /// 是否正在长按中
        /// </summary>
        public bool IsLongPress => this.Get().IsLongPress;

        /// <summary>
        /// 当前长按次数
        /// </summary>
        public int LongPressCount => this.Get().LongPressCount;

        /// <summary>
        /// 最大长按次数
        /// </summary>
        public int MaxLongPressCount => this.Get().MaxLongPressCount;

        protected override void Destroy()
        {
            this.OnLongPress.RemoveAllListeners();
            this.OnLongPressEnd.RemoveAllListeners();
            this.OnPointerExit.RemoveAllListeners();
            //格伦新增
            this.SetEnabled(true);

            base.Destroy();
        }

        /// <summary>
        /// 设置按钮的激活状态，激活后才支持长按
        /// </summary>
        /// <param name="active"></param>
        public void SetPointerActive(bool active)
        {
            this.Get().IsPointerActive = active;
        }

        /// <summary>
        /// 设置长按间隔
        /// </summary>
        /// <param name="interval"></param>
        public void SetLongPressInterval(float interval)
        {
            this.Get().LongPressInterval = interval;
        }

        /// <summary>
        /// 设置最大长按次数, 小于0为无限次
        /// </summary>
        /// <param name="count"></param>
        public void SetMaxLongPressCount(int count)
        {
            this.Get().MaxLongPressCount = count;
        }
    }

    public class XButtonComponent : XButtonComponent<XButton>
    {
    }

    public static class XButtonExtensions
    {
        public static XButtonComponent GetXButton(this UI self)
        {
            return self.TakeComponent<XButtonComponent, XButton>(true);
        }

        public static XButtonComponent GetXButton(this UI self, string key)
        {
            var ui = self.GetFromKeyOrPath(key);
            return ui?.GetXButton();
        }
    }
}