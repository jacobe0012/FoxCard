using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace XFramework
{
    public enum PointerEventTriggerType
    {
        PointerEnter = EventTriggerType.PointerEnter,
        PointerExit = EventTriggerType.PointerExit,
        PointerDown = EventTriggerType.PointerDown,
        PointerUp = EventTriggerType.PointerUp,
        PointerClick = EventTriggerType.PointerClick,
        Drag = EventTriggerType.Drag,
        Drop = EventTriggerType.Drop,
        Scroll = EventTriggerType.Scroll,
        InitializePotentialDrag = EventTriggerType.InitializePotentialDrag,
        BeginDrag = EventTriggerType.BeginDrag,
        EndDrag = EventTriggerType.EndDrag,
    }

    public enum AxisEventTriggerType
    {
        Move = EventTriggerType.Move,
    }

    public enum BaseEventTriggerType
    {
        UpdateSelected = EventTriggerType.UpdateSelected,
        Select = EventTriggerType.Select,
        Deselect = EventTriggerType.Deselect,
        Submit = EventTriggerType.Submit,
        Cancel = EventTriggerType.Cancel
    }

    public class XEventTrigger : EventTrigger       
    {
        private Dictionary<int, object> m_Delegates = new Dictionary<int, object>();

        private Dictionary<EventTriggerType, Entry> m_Entrys = new Dictionary<EventTriggerType, Entry>();

        public void AddTrigger(EventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            var entry = m_Entrys.Get(triggerType);
            if (entry == null)
            {
                entry = new Entry { eventID = triggerType };
                m_Entrys.Add(triggerType, entry);
            }
            if (!triggers.Contains(entry))
            {
                triggers.Add(entry);
            }
            entry.callback.AddListener(action);
        }

        public void RemoveTrigger(EventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            var entry = m_Entrys.Get(triggerType);
            if (entry == null)
                return;

            entry.callback.RemoveListener(action);
        }

        public void RemoveTriggers(EventTriggerType triggerType)
        {
            var entry = m_Entrys.Get(triggerType);
            if (entry == null)
                return;

            entry.callback.RemoveAllListeners();
            triggers.Remove(entry);
        }

        public void ClearTriggers()
        {
            triggers.Clear();
        }

        public void AddListener(PointerEventTriggerType triggerType, UnityAction<PointerEventData> action)
        {
            if (action == null)
                return;

            m_Delegates.TryGetValue((int)triggerType, out var o);
            var callback = o as UnityAction<PointerEventData>;
            callback += action;
            m_Delegates[(int)triggerType] = callback;
        }

        public void AddListener(AxisEventTriggerType triggerType, UnityAction<AxisEventData> action)
        {
            if (action == null)
                return;

            m_Delegates.TryGetValue((int)triggerType, out var o);
            var callback = o as UnityAction<AxisEventData>;
            callback += action;
            m_Delegates[(int)triggerType] = callback;
        }

        public void AddListener(BaseEventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            if (action == null)
                return;

            m_Delegates.TryGetValue((int)triggerType, out var o);
            var callback = o as UnityAction<BaseEventData>;
            callback += action;
            m_Delegates[(int)triggerType] = callback;
        }

        public void RemoveListener(PointerEventTriggerType triggerType, UnityAction<PointerEventData> action)
        {
            if (action == null)
                return;

            m_Delegates.TryGetValue((int)triggerType, out var o);
            var callback = o as UnityAction<PointerEventData>;
            callback -= action;
            m_Delegates[(int)triggerType] = callback;
        }

        public void RemoveListener(AxisEventTriggerType triggerType, UnityAction<AxisEventData> action)
        {
            if (action == null)
                return;

            m_Delegates.TryGetValue((int)triggerType, out var o);
            var callback = o as UnityAction<AxisEventData>;
            callback -= action;
            m_Delegates[(int)triggerType] = callback;
        }

        public void RemoveListener(BaseEventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            if (action == null)
                return;

            m_Delegates.TryGetValue((int)triggerType, out var o);
            var callback = o as UnityAction<BaseEventData>;
            callback -= action;
            m_Delegates[(int)triggerType] = callback;
        }

        public void RemoveListener(EventTriggerType triggerType)
        {
            m_Delegates.Remove((int)triggerType);
        }

        public void RemoveAllListeners()
        {
            m_Delegates.Clear();
        }

        public override void OnScroll(PointerEventData eventData)
        {
            base.OnScroll(eventData);
            this.Call((int)EventTriggerType.Scroll, eventData);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            this.Call((int)EventTriggerType.BeginDrag, eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            this.Call((int)EventTriggerType.Drag, eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            this.Call((int)EventTriggerType.EndDrag, eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            this.Call((int)EventTriggerType.PointerClick, eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            this.Call((int)EventTriggerType.PointerUp, eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            this.Call((int)EventTriggerType.PointerDown, eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            this.Call((int)EventTriggerType.PointerEnter, eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            this.Call((int)EventTriggerType.PointerExit, eventData);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            this.Call((int)EventTriggerType.Drop, eventData);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            this.Call((int)EventTriggerType.Select, eventData);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            this.Call((int)EventTriggerType.Deselect, eventData);
        }

        public override void OnMove(AxisEventData eventData)
        {
            base.OnMove(eventData);
            this.Call((int)EventTriggerType.Move, eventData);
        }

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            base.OnUpdateSelected(eventData);
            this.Call((int)EventTriggerType.UpdateSelected, eventData);
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            base.OnInitializePotentialDrag(eventData);
            this.Call((int)EventTriggerType.InitializePotentialDrag, eventData);
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            base.OnSubmit(eventData);
            this.Call((int)EventTriggerType.Submit, eventData);
        }

        public override void OnCancel(BaseEventData eventData)
        {
            base.OnCancel(eventData);
            this.Call((int)EventTriggerType.Cancel, eventData);
        }

        private void Call<T>(int type, T eventData) where T : BaseEventData
        {
            if (m_Delegates.TryGetValue(type, out var action))
            {
                if (action is UnityAction<T> callback)
                    callback?.Invoke(eventData);
            }
        }

        private void OnDestroy()
        {
            m_Delegates.Clear();
            m_Entrys.Clear();
        }
    }
}
