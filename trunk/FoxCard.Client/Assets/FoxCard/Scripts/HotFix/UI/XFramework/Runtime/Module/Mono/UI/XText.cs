using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XFramework
{
    public class XText : Text, IMultilingual, IUIGrayed
    {
        #region gray

        private bool defaultOverrideGrayed;

        private bool defalutGrayed;

        /// <summary>
        /// 覆盖的灰色
        /// </summary>
        [SerializeField] protected bool overrideGrayed;

        /// <summary>
        /// 灰色
        /// </summary>
        [SerializeField] protected bool grayed;

        public virtual bool Grayed
        {
            get => overrideGrayed || grayed;
            set => SetGrayed(value);
        }

        bool IUIGrayed.OverrideGrayed
        {
            get => overrideGrayed;
            set => ((IUIGrayed)this).SetOverrideGrayed(value);
        }

        void IUIGrayed.SetOverrideGrayed(bool grayed)
        {
            var _grayed = this.Grayed;
            if (overrideGrayed != grayed)
            {
                overrideGrayed = grayed;
                if (_grayed != this.Grayed)
                    SetGrayedMaterial(this.Grayed);
            }
        }

        void IUIGrayed.ResetGrayed()
        {
            var _grayed = this.Grayed;
            this.grayed = this.defalutGrayed;
            this.overrideGrayed = this.defaultOverrideGrayed;
            if (_grayed != this.Grayed)
                this.SetGrayedMaterial(this.Grayed);
        }

        public void SetGrayed(bool grayed)
        {
            var _grayed = this.Grayed;
            if (grayed != this.grayed)
            {
                this.grayed = grayed;
                if (_grayed != this.Grayed)
                    this.SetGrayedMaterial(this.Grayed);
            }
        }

        protected void SetGrayedMaterial(bool grayed)
        {
            // 如果之前是灰色
            if (!grayed && this.m_Material && this.m_Material != defaultMaterial)
            {
                if (Application.isPlaying)
                    Destroy(this.m_Material);
                else
                    DestroyImmediate(this.m_Material, true);
            }

            this.m_Material = grayed ? new Material(Shader.Find("UI/GrayedX")) : defaultMaterial;
            SetMaterialDirty();
        }

        public override Material material
        {
            get => base.material;
            set
            {
                if (grayed)
                    return;

                base.material = value;
            }
        }

        #endregion

        [Tooltip("忽略多语言")] [SerializeField] private bool m_IgnoreLanguage;

        [Tooltip("多语言的Key")] [SerializeField] private string m_Key = string.Empty;

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

            if (Application.isPlaying)
            {
                _instanceId = this.GetInstanceID();
                m_Key = m_Key.Trim();

                if (!m_IgnoreLanguage)
                    UIReference.AddText(_instanceId, this);
            }

            defaultOverrideGrayed = overrideGrayed;
            defalutGrayed = grayed;
            if (defalutGrayed || defaultOverrideGrayed)
            {
                SetGrayedMaterial(true);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (Application.isPlaying)
            {
                if (!m_IgnoreLanguage)
                    UIReference.RemoveText(_instanceId);

                if (grayed && this.m_Material && this.m_Material != defaultMaterial)
                {
                    Destroy(this.m_Material);
                    this.m_Material = null;
                }

                overrideGrayed = false;
                grayed = false;
                GameObjectExtensions.ChangeGrayedList(gameObject.GetInstanceID(), false);
            }
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