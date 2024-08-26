using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace XFramework
{
    public abstract class DropdownComponent<T> : UIBehaviourComponent<T> where T : Dropdown
    {
        public int Value => this.Get().value;

        public UnityEvent<int> OnValueChanged => this.Get().onValueChanged;

        protected override void Destroy()
        {
            this.OnValueChanged.RemoveAllListeners();
            base.Destroy();
        }

        public void AddOptions(List<string> options)
        {
            this.Get().AddOptions(options);
        }

        public void AddOptions(string[] options)
        {
            using var list = XList<string>.Create();
            list.AddRange(options);
            this.AddOptions(list);
        }

        public void AddOptinos(List<Dropdown.OptionData> options)
        {
            this.Get().AddOptions(options);
        }

        public void AddOptions(List<Sprite> options)
        {
            this.Get().AddOptions(options);
        }

        public void ClearOptions()
        {
            this.Get().ClearOptions();
        }

        public void RefreshShownValue()
        {
            this.Get().RefreshShownValue();
        }

        /// <summary>
        /// 直接触发事件，但不会改变value的值
        /// </summary>
        /// <param name="value"></param>
        public void ValueInvoke(int value)
        {
            this.Get().ValueInvoke(value);
        }

        /// <summary>
        /// 设置value但会触发事件
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(int value)
        {
            this.Get().SetValue(value);
        }

        /// <summary>
        /// 设置value但不会触发事件
        /// </summary>
        /// <param name="value"></param>
        public void SetValueWithoutNotify(int value)
        {
            this.Get().SetValueWithoutNotify(value);
        }

        /// <summary>
        /// 设置value，如果没有设置成功则直接触发事件
        /// </summary>
        /// <param name="value"></param>
        public void SetValueOrInvoke(int value)
        {
            this.Get().SetValueOrInvoke(value);
        }
    }

    public class DropdownComponent : DropdownComponent<Dropdown>
    {
        
    }

    public static class DropdownExtensions
    {
        public static DropdownComponent GetDropdown(this UI self)
        {
            return self.TakeComponent<DropdownComponent, Dropdown>(true);
        }

        public static DropdownComponent GetDropdown(this UI self, string key)
        {
            var ui = self.GetFromKeyOrPath(key);
            return ui?.GetDropdown();
        }

        /// <summary>
        /// 直接触发事件，但不会改变value的值
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        public static void ValueInvoke(this Dropdown self, int value)
        {
            self.onValueChanged.Invoke(value);
        }

        public static void SetValue(this Dropdown self, int value)
        {
            self.value = value;
        }

        /// <summary>
        /// 设置value，如果没有设置成功则直接触发事件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="value"></param>
        public static void SetValueOrInvoke(this Dropdown self, int value)
        {
            if (self.value != value)
            {
                self.SetValue(value);
            }
            else
            {
                self.ValueInvoke(value);
            }
        }
    }
}
