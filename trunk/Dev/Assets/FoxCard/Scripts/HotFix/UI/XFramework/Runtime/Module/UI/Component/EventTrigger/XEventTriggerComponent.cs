using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace XFramework
{
    public abstract class XEventTriggerComponent<T> : UComponent<T> where T : XEventTrigger
    {
        protected override void Destroy()
        {
            this.RemoveAllListeners();
            this.ClearTriggers();
            base.Destroy();
        }

        public void AddListener(PointerEventTriggerType triggerType, UnityAction<PointerEventData> action)
        {
            this.Get().AddListener(triggerType, action);
        }

        public void AddListener(AxisEventTriggerType triggerType, UnityAction<AxisEventData> action)
        {
            this.Get().AddListener(triggerType, action);
        }

        public void AddListener(BaseEventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            this.Get().AddListener(triggerType, action);
        }

        public void RemoveListener(PointerEventTriggerType triggerType, UnityAction<PointerEventData> action)
        {
            this.Get().RemoveListener(triggerType, action);
        }

        public void RemoveListener(AxisEventTriggerType triggerType, UnityAction<AxisEventData> action)
        {
            this.Get().RemoveListener(triggerType, action);
        }

        public void RemoveListener(BaseEventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            this.Get().RemoveListener(triggerType, action);
        }

        public void RemoveListener(EventTriggerType triggerType)
        {
            this.Get().RemoveListener(triggerType);
        }

        public void RemoveAllListeners()
        {
            this.Get().RemoveAllListeners();
        }

        public void AddTrigger(EventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            this.Get().AddTrigger(triggerType, action);
        }

        public void RemoveTrigger(EventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            this.Get().RemoveTrigger(triggerType, action);
        }

        public void RemoveTriggers(EventTriggerType triggerType)
        {
            this.Get().RemoveTriggers(triggerType);
        }

        public void ClearTriggers()
        {
            this.Get().ClearTriggers();
        }
    }

    public class XEventTriggerComponent : XEventTriggerComponent<XEventTrigger>
    {
    }

    public static class EventTriggerExtensions
    {
        public static XEventTriggerComponent GetXEventTrigger(this UI self)
        {
            return self.TakeComponent<XEventTriggerComponent, XEventTrigger>(true);
        }

        public static XEventTriggerComponent GetXEventTrigger(this UI self, string key)
        {
            UI ui = self.GetFromKeyOrPath(key);
            return ui?.GetXEventTrigger();
        }
    }
}