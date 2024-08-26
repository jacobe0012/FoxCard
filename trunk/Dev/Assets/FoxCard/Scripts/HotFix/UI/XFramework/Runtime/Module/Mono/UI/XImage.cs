using UnityEngine;
using UnityEngine.UI;

namespace XFramework
{
    [DisallowMultipleComponent]
    public class XImage : Image, IUIGrayed
    {
        private bool defaultOverrdeGrayed;

        private bool defalutGrayed;

        /// <summary>
        /// 覆盖的灰色
        /// </summary>
        [SerializeField]
        protected bool overrideGrayed;

        /// <summary>
        /// 灰色
        /// </summary>
        [SerializeField]
        protected bool grayed;

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
            this.overrideGrayed = this.defaultOverrdeGrayed;
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

        protected override void Awake()
        {
            base.Awake();

            defaultOverrdeGrayed = overrideGrayed;
            defalutGrayed = grayed;
            if (defalutGrayed || defaultOverrdeGrayed)
            {
                SetGrayedMaterial(true);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (Application.isPlaying)
            {
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
    }
}
