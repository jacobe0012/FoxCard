using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace XFramework
{
    public class XTextMeshPro : TextMeshPro, IMultilingual
    {
        [Tooltip("忽略多语言")]
        [SerializeField]
        private bool m_IgnoreLanguage;

        [Tooltip("多语言的Key")]
        [SerializeField]
        private string m_Key = string.Empty;

        private List<object> _objs = new List<object>();

        private object[] _args;

        private int _instanceId;

        /// <summary>
        /// 多语言key
        /// </summary>
        public string Key => m_Key.Trim();

        /// <summary>
        /// 多语言参数
        /// </summary>
        public object[] Args => _args ?? _objs.ToArray();

        /// <summary>
        /// 忽略多语言
        /// </summary>
        public bool IgnoreLanguage => m_IgnoreLanguage;

        protected override void Awake()
        {
            base.Awake();
            _instanceId = this.GetInstanceID();
            m_Key = m_Key.Trim();

            if (!m_IgnoreLanguage)
                UIReference.AddText(_instanceId, this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (!m_IgnoreLanguage)
                UIReference.RemoveText(_instanceId);
        }

        void IMultilingual.SetKey(string key, params object[] args)
        {
            if (m_IgnoreLanguage)
                return;

            m_Key = key?.Trim();
            _args = null;

            if (args != null && args.Length == 0)
                _args = System.Array.Empty<object>();

            _objs.Clear();
            _objs.AddRange(args);
        }

        void IMultilingual.SetContent(string content)
        {
            text = content;
        }
    }
}
