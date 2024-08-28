using UnityEngine.Events;
using UnityEngine.UI;

namespace XFramework
{
    public abstract class ButtonComponent<T> : UIBehaviourComponent<T> where T : Button
    {
        /// <summary>
        /// 点击事件
        /// </summary>
        public UnityEvent OnClick => this.Get().onClick;

        protected override void Destroy()
        {
            this.OnClick.RemoveAllListeners();

            base.Destroy();
        }

        /// <summary>
        /// 设置是否可以交互
        /// </summary>
        /// <param name="value"></param>
        public void SetInteractable(bool value)
        {
            this.Get().interactable = value;
        }
    }

    public class ButtonComponent : ButtonComponent<Button>
    {
    }

    public static class UIButtonExtensions
    {
        public static ButtonComponent GetButton(this UI self)
        {
            return self.TakeComponent<ButtonComponent, Button>(true);
        }

        public static ButtonComponent GetButton(this UI self, string key)
        {
            UI ui = self.GetFromKeyOrPath(key);
            return ui?.GetButton();
        }
    }
}