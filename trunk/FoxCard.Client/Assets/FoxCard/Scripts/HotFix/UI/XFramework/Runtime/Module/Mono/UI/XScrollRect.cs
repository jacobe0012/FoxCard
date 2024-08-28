using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XFramework
{
    public class XScrollRect : ScrollRect, IPointerMoveHandler
    {
        public UnityEvent<Vector2> m_OnDrag = new UnityEvent<Vector2>();
        public UnityEvent m_OnEndDrag = new UnityEvent();
        public UnityEvent m_OnBeginDrag = new UnityEvent();

        public override void OnEndDrag(PointerEventData eventData)
        {
            m_OnEndDrag.Invoke();
            base.OnEndDrag(eventData);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            m_OnBeginDrag.Invoke();
            base.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            m_OnDrag.Invoke(eventData.delta);
            base.OnDrag(eventData);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
        }
    }
}