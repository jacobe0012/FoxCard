using UnityEngine.Events;
using UnityEngine.UI;

namespace XFramework
{
    public abstract class ToggleComponent<T> : UIBehaviourComponent<T> where T : Toggle
    {
        public bool IsOn => this.Get().isOn;

        public UnityEvent<bool> OnValueChanged => this.Get().onValueChanged;

        protected override void Destroy()
        {
            this.OnValueChanged.RemoveAllListeners();
            base.Destroy();
        }

        /// <summary>
        /// 直接触发事件，但不会改变isOn的值
        /// </summary>
        /// <param name="value"></param>
        public void ClickInvoke(bool value)
        {
            this.Get().ClickInvoke(value);
        }

        /// <summary>
        /// 设置IsOn，如果设置失败则直接触发事件
        /// </summary>
        /// <param name="value"></param>
        public void SetIsOnOrInvoke(bool value)
        {
            this.Get().SetIsOnOrInvoke(value);
        }

        /// <summary>
        /// 设置IsOn
        /// <para>如果设置成功并且你添加了onValueChanged事件则也会触发</para>
        /// </summary>
        /// <param name="value"></param>
        public void SetIsOn(bool value)
        {
            this.Get().SetIsOn(value);
        }

        /// <summary>
        /// 设置IsOn但不会触发事件
        /// </summary>
        /// <param name="value"></param>
        public void SetIsOnWithoutNotify(bool value)
        {
            this.Get().SetIsOnWithoutNotify(value);
        }
    }

    public class ToggleComponent : ToggleComponent<Toggle>
    {
    }

    public static class UIToggleExtensions
    {
        public static ToggleComponent GetToggle(this UI self)
        {
            return self.TakeComponent<ToggleComponent, Toggle>(true);
        }

        public static ToggleComponent GetToggle(this UI self, string key)
        {
            UI ui = self.GetFromKeyOrPath(key);
            return ui?.GetToggle();
        }

        /// <summary>
        /// 直接触发事件，但不会改变isOn的值
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        public static void ClickInvoke(this Toggle self, bool value)
        {
            self.onValueChanged.Invoke(value);
        }

        public static void SetIsOn(this Toggle self, bool value)
        {
            self.isOn = value;
        }

        /// <summary>
        /// 设置isOn，如果没有设置成功则直接触发事件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        public static void SetIsOnOrInvoke(this Toggle self, bool value)
        {
            if (self.isOn != value)
            {
                self.SetIsOn(value);
            }
            else
            {
                self.ClickInvoke(value);
            }
        }
    }
}