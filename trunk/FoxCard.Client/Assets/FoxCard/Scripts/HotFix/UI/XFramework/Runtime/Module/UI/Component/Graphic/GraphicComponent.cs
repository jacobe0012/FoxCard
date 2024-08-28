using System;
using UnityEngine;
using UnityEngine.UI;

namespace XFramework
{
    public abstract class GraphicComponent<T> : UIBehaviourComponent<T> where T : Graphic
    {
        protected Material defaultMaterial;

        protected override void EndInitialize()
        {
            base.EndInitialize();
            defaultMaterial = this.Get().material;
        }

        protected override void Destroy()
        {
            if (this.Get() is IUIGrayed g)
                g.ResetGrayed();

            this.Get().material = defaultMaterial;
            defaultMaterial = null;

            base.Destroy();
        }

        public void SetMaterial(Material material)
        {
            this.Get().SetMaterial(material);
        }

        public void SetMaterial(string key)
        {
            this.Get().SetMaterial(this, key);
        }

        public void SetColor(Color color)
        {
            this.Get().SetColor(color);
        }

        public void SetColor(string hexColor)
        {
            this.Get().SetColor(hexColor);
        }

        public void SetAlpha(float a)
        {
            this.Get().SetAlpha(a);
        }

        public Color Color()
        {
            return this.Get().color;
        }

        /// <summary>
        /// 是否忽略此渲染器发出的几何图形
        /// </summary>
        /// <param name="cull"></param>
        public void Cull(bool cull)
        {
            this.Get().Cull(cull);
        }

        public MiniTween DoFade(float endValue, float duration)
        {
            return this.Get().DoFade(this, endValue, duration);
        }

        public MiniTween DoFade(float startValue, float endValue, float duration)
        {
            return this.Get().DoFade(this, startValue, endValue, duration);
        }
    }

    public static class GraphicComponentExtensions
    {
        public static void SetMaterial(this Graphic self, Material material)
        {
            self.material = material;
        }

        public static void SetMaterial(this Graphic self, XObject parent, string key)
        {
            Material material = ResourcesManager.LoadAsset<Material>(parent, key);
            if (material == null)
                return;

            self.SetMaterial(material);
        }

        /// <summary>
        /// 是否忽略此渲染器发出的几何图形
        /// </summary>
        /// <param name="self"></param>
        /// <param name="cull"></param>
        public static void Cull(this Graphic self, bool cull)
        {
            self.canvasRenderer.cull = cull;
        }

        #region MiniTween

        public static MiniTween DoFloat(this Graphic self, XObject parent, float startValue, float endValue,
            float duration, Action<float> setter)
        {
            var tweenMgr = Common.Instance.Get<MiniTweenManager>();
            if (tweenMgr is null)
                return null;

            var tween = tweenMgr.To(parent, startValue, endValue, duration);
            tween.AddListener(v =>
            {
                if (!self)
                {
                    tween.Cancel(parent);
                    return;
                }

                setter.Invoke(v);
            });

            return tween;
        }

        public static MiniTween DoFade(this Graphic self, XObject parent, float endValue, float duration)
        {
            return self.DoFade(parent, self.color.a, endValue, duration);
        }

        public static MiniTween DoFade(this Graphic self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoFloat(parent, startValue, endValue, duration, self.SetAlpha);
        }

        public static void SetColor(this Graphic self, Color color)
        {
            self.color = color;
        }

        public static void SetColor(this Graphic self, string hexColor)
        {
            string trimmedString = hexColor.Trim();

            if (trimmedString.Length > 0 && trimmedString[0] != '#')
            {
                hexColor = $"#{hexColor}";
            }

            if (ColorUtility.TryParseHtmlString(hexColor, out var color))
            {
                self.SetColor(color);
            }
        }

        public static void SetAlpha(this Graphic self, float a)
        {
            a = Mathf.Clamp01(a);
            Color color = self.color;
            color.a = a;
            self.SetColor(color);
        }

        #endregion
    }
}