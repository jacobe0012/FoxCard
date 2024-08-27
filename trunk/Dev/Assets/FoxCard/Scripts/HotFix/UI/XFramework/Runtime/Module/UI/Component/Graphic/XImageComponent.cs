using UnityEngine;

namespace XFramework
{
    public abstract class XImageComponent<T> : ImageComponent<T> where T : XImage
    {
        protected override void Destroy()
        {
            this.Get().myfillAmountPadding = Vector4.one;
            this.Get().myfilledAround = false;
            base.Destroy();
        }

        public void SetGrayed(bool grayed)
        {
            this.Get().SetGrayed(grayed);
        }

        /// <summary>
        /// 设置裁剪参数 xyzw：上下左右 数值：裁剪比例(0-1)
        /// </summary>
        /// <param name="paras"></param>
        public void SetCutting(Vector4 paras)
        {
            if (paras != Vector4.one)
            {
                this.Get().myfillAmountPadding = paras;
                this.Get().myfilledAround = true;
            }
        }
    }

    public class XImageComponent : XImageComponent<XImage>
    {
    }

    public static class XImageExtensions
    {
        public static XImageComponent GetXImage(this UI self)
        {
            return self.TakeComponent<XImageComponent, XImage>(true);
        }

        public static XButtonComponent GetXImage(this UI self, string key)
        {
            var ui = self.GetFromKeyOrPath(key);
            return ui?.GetXButton();
        }
    }
}