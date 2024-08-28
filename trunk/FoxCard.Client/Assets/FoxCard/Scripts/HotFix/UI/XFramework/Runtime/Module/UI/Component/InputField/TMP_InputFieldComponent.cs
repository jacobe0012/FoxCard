using TMPro;
using UnityEngine.Events;

namespace XFramework
{
    public abstract class TMP_InputFieldComponent<T> : UIBehaviourComponent<T> where T : TMP_InputField
    {
        public string Content => this.Get().text;

        public UnityEvent<string> OnValueChanged => this.Get().onValueChanged;

        public UnityEvent<string> OnEndEdit => this.Get().onEndEdit;
        public UnityEvent<string> OnSelect => this.Get().onSelect;

        protected override void Destroy()
        {
            this.OnValueChanged.RemoveAllListeners();
            this.OnEndEdit.RemoveAllListeners();
            this.OnSelect.RemoveAllListeners();
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

    public class TMP_InputFieldComponent : TMP_InputFieldComponent<TMP_InputField>
    {
    }

    public static class UITMP_InputFieldExtensions
    {
        public static TMP_InputFieldComponent GetTMP_InputField(this UI self)
        {
            return self.TakeComponent<TMP_InputFieldComponent, TMP_InputField>(true);
        }

        public static TMP_InputFieldComponent GetTMP_InputField(this UI self, string key)
        {
            UI ui = self.GetFromKeyOrPath(key);
            return ui?.GetTMP_InputField();
        }

        public static void SetText(this TMP_InputField self, string value)
        {
            self.text = value;
        }

        public static void SetText(this TMP_InputField self, string key, params object[] args)
        {
            self.text = string.Format(key, args);
        }
    }
}