using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XFramework
{
    public abstract class SliderComponent<T> : UIBehaviourComponent<T> where T : Slider
    {
        public float Value => this.Get().value;

        public UnityEvent<float> OnValueChanged => this.Get().onValueChanged;

        protected override void Destroy()
        {
            this.OnValueChanged.RemoveAllListeners();
            base.Destroy();
        }

        /// <summary>
        /// 设置value，同时会触发onValueChanged事件
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(float value)
        {
            this.Get().SetValue(value);
        }

        /// <summary>
        /// 设置value，并不会触发onValueChanged事件
        /// </summary>
        /// <param name="value"></param>
        public void SetValueWithoutNotify(float value)
        {
            this.Get().SetValueWithoutNotify(value);
        }
    }

    public class SliderComponent : SliderComponent<Slider>
    {
        
    }

    public static class UISliderExtensions
    {
        public static SliderComponent GetSlider(this UI self)
        {
            return self.TakeComponent<SliderComponent, Slider>(true);
        }

        public static SliderComponent GetSlider(this UI self, string key)
        {
            UI ui = self.GetFromKeyOrPath(key);
            return ui?.GetSlider();
        }

        public static void SetValue(this Slider self, float value)
        {
            self.value = value;
        }
    }
}
