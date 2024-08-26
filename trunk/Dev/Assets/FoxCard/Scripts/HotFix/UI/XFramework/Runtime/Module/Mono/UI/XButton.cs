using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XFramework
{
    public class XButton : Button
    {
        [System.Serializable]
        public class ButtonLongPressEvent : UnityEvent<int>
        {
        }

        /// <summary>
        /// 当前积累的间隔时间
        /// </summary>
        private float m_CurLongPressInterval = 0;

        /// <summary>
        /// 长按次数
        /// </summary>
        private int m_LongPressCount = 0;

        /// <summary>
        /// 最大长按次数
        /// </summary>
        [SerializeField] [Tooltip("最大长按次数, 小于0为无限次")]
        private int m_MaxLongPressCount = 0;

        /// <summary>
        /// 长按间隔时间
        /// </summary>
        [SerializeField, Min(0.01f)] [Tooltip("长按间隔")]
        private float m_LongPressInterval = 0.3f;

        /// <summary>
        /// 按下
        /// </summary>
        private bool m_IsPointerDown = false;

        /// <summary>
        /// 长按结束的事件是否执行过
        /// </summary>
        private bool m_IsLongPressEndExecuted = false;

        /// <summary>
        /// 是否激活（按下后没有离开对象并且没有抬起）
        /// </summary>
        private bool m_IsPointerActive = false;

        /// <summary>
        /// 按钮激活状态，激活后才支持长按
        /// </summary>
        public bool IsPointerActive
        {
            get
            {
                return m_IsPointerActive && m_IsPointerDown &&
                       (m_MaxLongPressCount >= 0 ? m_LongPressCount < m_MaxLongPressCount : true);
            }
            set
            {
                if (m_IsPointerActive != value)
                {
                    m_IsPointerActive = value;
                    if (value)
                    {
                        m_CurLongPressInterval = 0;
                        m_LongPressCount = 0;
                        m_IsLongPressEndExecuted = false;
                    }
                }
            }
        }

        /// <summary>
        /// 是否在长按
        /// </summary>
        public bool IsLongPress => m_LongPressCount > 0;

        /// <summary>
        /// 长按次数
        /// </summary>
        public int LongPressCount => m_LongPressCount;

        /// <summary>
        /// 最大长按次数, 小于0为无限次
        /// </summary>
        public int MaxLongPressCount
        {
            get => m_MaxLongPressCount;
            set => m_MaxLongPressCount = value;
        }

        /// <summary>
        /// 长按间隔
        /// </summary>
        public float LongPressInterval
        {
            get => m_LongPressInterval;
            set { m_LongPressInterval = System.Math.Max(value, 0.01f); }
        }

        [UnityEngine.Serialization.FormerlySerializedAs("onLongPress")] [SerializeField]
        private ButtonLongPressEvent m_OnLongPress = new ButtonLongPressEvent();

        [UnityEngine.Serialization.FormerlySerializedAs("onLongPressEnd")] [SerializeField]
        private ButtonLongPressEvent m_OnLongPressEnd = new ButtonLongPressEvent();

        private UnityEvent m_OnPointerExit = new UnityEvent();

        /// <summary>
        /// 长按时执行的事件
        /// 参数的长按的次数
        /// </summary>
        public ButtonLongPressEvent onLongPress
        {
            get => m_OnLongPress;
            set => m_OnLongPress = value;
        }

        /// <summary>
        /// 长按结束后执行的事件
        /// 参数是总长按次数
        /// </summary>
        public ButtonLongPressEvent onLongPressEnd
        {
            get => m_OnLongPressEnd;
            set => m_OnLongPressEnd = value;
        }

        public UnityEvent onPointerExit
        {
            get => m_OnPointerExit;
            set => m_OnPointerExit = value;
        }

        protected virtual void Update()
        {
            if (!IsPointerActive)
                return;

            float interval = Time.unscaledDeltaTime;
            m_CurLongPressInterval += interval;
            if (m_CurLongPressInterval >= LongPressInterval)
            {
                m_CurLongPressInterval -= LongPressInterval;
                ++m_LongPressCount;
                LongPress();
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (m_LongPressCount > 0)
            {
                LongPressEnd();
                return;
            }

            base.OnPointerClick(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            IsPointerActive = false;
            if (m_LongPressCount > 0)
            {
                if (!m_IsLongPressEndExecuted && IsActive() && IsInteractable())
                {
                    //m_OnLongPressEnd.Invoke(m_LongPressCount);
                    onPointerExit.Invoke();
                    m_IsLongPressEndExecuted = true;
                    onPointerExit.RemoveAllListeners();
                }

                m_LongPressCount = 0;
            }

            base.OnPointerExit(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            IsPointerActive = false;
            m_IsPointerDown = false;
            base.OnPointerUp(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            IsPointerActive = true;
            m_IsPointerDown = true;
            base.OnPointerDown(eventData);
        }

        /// <summary>
        /// 长按事件
        /// </summary>
        private void LongPress()
        {
            if (IsActive() && IsInteractable())
            {
                m_OnLongPress.Invoke(m_LongPressCount);
            }
        }

        /// <summary>
        /// 长按结束事件
        /// </summary>
        private void LongPressEnd()
        {
            if (!m_IsLongPressEndExecuted && IsActive() && IsInteractable())
            {
                m_OnLongPressEnd.Invoke(m_LongPressCount);
                m_IsLongPressEndExecuted = true;
                onPointerExit.RemoveAllListeners();
            }

            m_LongPressCount = 0;
        }
    }
}