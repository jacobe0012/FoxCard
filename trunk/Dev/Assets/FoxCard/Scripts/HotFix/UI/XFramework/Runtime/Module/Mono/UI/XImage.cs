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
        [SerializeField] protected bool overrideGrayed;

        /// <summary>
        /// 灰色
        /// </summary>
        [SerializeField] protected bool grayed;


        [SerializeField] public bool myfilledAround = false;

        //上下左右保留的比例
        //[SerializeField] public Vector4 myfillAmountPadding = new Vector4(0.8f, 0.9f, 0.8f, 0.9f);

        [SerializeField] public Vector4 myfillAmountPadding = Vector4.one;

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

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            if (myfilledAround)
            {
                GenerateFilledSprite(vh, preserveAspect);
            }
            else
            {
                base.OnPopulateMesh(vh);
            }
        }

        static readonly Vector3[] s_Xy = new Vector3[4];
        static readonly Vector3[] s_Uv = new Vector3[4];


        /// <summary>
        /// Generate vertices for a filled Image.
        /// </summary>
        void GenerateFilledSprite(VertexHelper toFill, bool preserveAspect)
        {
            toFill.Clear();

            // if (fillAmount < 0.001f)
            //     return;

            Vector4 v0 = GetDrawingDimensions(preserveAspect);
            Vector4 outer = overrideSprite != null
                ? UnityEngine.Sprites.DataUtility.GetOuterUV(overrideSprite)
                : Vector4.zero;


            UIVertex uiv = UIVertex.simpleVert;
            uiv.color = color;

            float tx0 = outer.x;
            float ty0 = outer.y;
            float tx1 = outer.z;
            float ty1 = outer.w;

            float tx00 = outer.x;
            float ty00 = outer.y;
            float tx10 = outer.z;
            float ty10 = outer.w;
            //Horizontal and vertical filled sprites are simple-- just end the Image prematurely
            Vector4 v = v0;
            Vector4 v1 = v0;
            if (myfilledAround)
            {
                if ((1 - myfillAmountPadding.x) + (1 - myfillAmountPadding.y) > 1 ||
                    (1 - myfillAmountPadding.z) + (1 - myfillAmountPadding.w) > 1)
                {
                    Debug.LogError($"图片裁剪的上+下或者左+右不可大于1！！");
                }

                float fillDown = (ty1 - ty0) * myfillAmountPadding.x;
                float fillLeft = (tx1 - tx0) * myfillAmountPadding.z;

                float fillRight = (tx10 - tx00) * myfillAmountPadding.w;
                float fillUp = (ty10 - ty00) * myfillAmountPadding.y;

                v.w = v.y + (v.w - v.y) * myfillAmountPadding.x;
                ty1 = ty0 + fillDown;
                v.x = v.z - (v.z - v.x) * myfillAmountPadding.z;
                tx0 = tx1 - fillLeft;


                v.y = v1.w - (v1.w - v1.y) * myfillAmountPadding.y;
                ty0 = ty10 - fillUp;

                v.z = v1.x + (v1.z - v1.x) * myfillAmountPadding.w;
                tx1 = tx00 + fillRight;
            }

            s_Xy[0] = new Vector2(v.x, v.y);
            s_Xy[1] = new Vector2(v.x, v.w);
            s_Xy[2] = new Vector2(v.z, v.w);
            s_Xy[3] = new Vector2(v.z, v.y);

            s_Uv[0] = new Vector2(tx0, ty0);
            s_Uv[1] = new Vector2(tx0, ty1);
            s_Uv[2] = new Vector2(tx1, ty1);
            s_Uv[3] = new Vector2(tx1, ty0);


            AddQuad(toFill, s_Xy, color, s_Uv);
        }


        /// Image's dimensions used for drawing. X = left, Y = bottom, Z = right, W = top.
        private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
        {
            var activeSprite = overrideSprite;
            var padding = activeSprite == null
                ? Vector4.zero
                : UnityEngine.Sprites.DataUtility.GetPadding(activeSprite);
            var size = activeSprite == null
                ? Vector2.zero
                : new Vector2(activeSprite.rect.width, activeSprite.rect.height);

            Rect r = GetPixelAdjustedRect();
            // Debug.Log(string.Format("r:{2}, size:{0}, padding:{1}", size, padding, r));

            int spriteW = Mathf.RoundToInt(size.x);
            int spriteH = Mathf.RoundToInt(size.y);

            var v = new Vector4(
                padding.x / spriteW,
                padding.y / spriteH,
                (spriteW - padding.z) / spriteW,
                (spriteH - padding.w) / spriteH);

            if (shouldPreserveAspect && size.sqrMagnitude > 0.0f)
            {
                PreserveSpriteAspectRatio(ref r, size);
            }

            v = new Vector4(
                r.x + r.width * v.x,
                r.y + r.height * v.y,
                r.x + r.width * v.z,
                r.y + r.height * v.w
            );

            return v;
        }

        private void PreserveSpriteAspectRatio(ref Rect rect, Vector2 spriteSize)
        {
            var spriteRatio = spriteSize.x / spriteSize.y;
            var rectRatio = rect.width / rect.height;

            if (spriteRatio > rectRatio)
            {
                var oldHeight = rect.height;
                rect.height = rect.width * (1.0f / spriteRatio);
                rect.y += (oldHeight - rect.height) * rectTransform.pivot.y;
            }
            else
            {
                var oldWidth = rect.width;
                rect.width = rect.height * spriteRatio;
                rect.x += (oldWidth - rect.width) * rectTransform.pivot.x;
            }
        }

        static void AddQuad(VertexHelper vertexHelper, Vector3[] quadPositions, Color32 color, Vector3[] quadUVs)
        {
            int startIndex = vertexHelper.currentVertCount;

            for (int i = 0; i < 4; ++i)
                vertexHelper.AddVert(quadPositions[i], color, quadUVs[i]);

            vertexHelper.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
            vertexHelper.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
        }
    }
}