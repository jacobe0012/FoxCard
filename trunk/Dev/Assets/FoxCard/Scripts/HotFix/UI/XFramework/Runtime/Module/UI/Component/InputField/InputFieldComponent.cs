using UnityEngine.Events;
using UnityEngine.UI;

namespace XFramework
{
    public abstract class InputFieldComponent<T> : UIBehaviourComponent<T> where T : InputField
    {
        public string Content => this.Get().text;

        public UnityEvent<string> OnValueChanged => this.Get().onValueChanged;

        public UnityEvent<string> OnEndEdit => this.Get().onEndEdit;

        protected override void Destroy()
        {
            this.OnValueChanged.RemoveAllListeners();
            this.OnEndEdit.RemoveAllListeners();
            base.Destroy();
        }

        public void SetText(string value)
        {
            this.Get().SetText(value);
        }

        public void SetText(string key, params object[] args)
        {
            this.Get().SetText(key, args);
        }
    }

    public class InputFieldComponent : UIBehaviourComponent<InputField>
    {
    }

    public static class UIInputFieldExtensions
    {
        public static InputFieldComponent GetInputField(this UI self)
        {
            return self.TakeComponent<InputFieldComponent, InputField>(true);
        }

        public static InputFieldComponent GetInputField(this UI self, string key)
        {
            UI ui = self.GetFromKeyOrPath(key);
            return ui?.GetInputField();
        }

        public static void SetText(this InputField self, string value)
        {
            self.text = value;
        }

        public static void SetText(this InputField self, string key, params object[] args)
        {
            self.text = string.Format(key, args);
        }
    }
}