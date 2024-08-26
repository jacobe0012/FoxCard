using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace XFramework
{
    public abstract class ImageComponent<T> : GraphicComponent<T> where T : Image
    {
        protected override void Destroy()
        {
            this.SetOverrideSprite(null, false);
            base.Destroy();
        }

        public void SetSprite(Sprite sprite, bool setNativeSize)
        {
            this.Get().SetSprite(sprite, setNativeSize);
        }

        public void SetOverrideSprite(Sprite sprite, bool setNativeSize)
        {
            this.Get().SetOverrideSprite(sprite, setNativeSize);
        }

        public async UniTask SetSpriteAsync(string key, bool setNativeSize)
        {
            await this.Get().SetSpriteAsync(this, key, setNativeSize);
        }

        public void SetSprite(string key, bool setNativeSize)
        {
            this.Get().SetSprite(this, key, setNativeSize);
        }

        public void SetNativeSize()
        {
            this.Get().SetNativeSize();
        }

        public void SetFillAmount(float fillAmount)
        {
            this.Get().fillAmount = fillAmount;
        }

        public float GetFillAmount()
        {
            return this.Get().fillAmount;
        }

        public MiniTween DoFillAmount(float endValue, float duration)
        {
            return this.Get().DoFillAmount(this, endValue, duration);
        }

        public MiniTween DoFillAmount(float startValue, float endValue, float duration)
        {
            return this.Get().DoFillAmount(this, startValue, endValue, duration);
        }
    }

    public class ImageComponent : ImageComponent<Image>
    {
    }

    public static class UIImageExtensions
    {
        public static ImageComponent GetImage(this UI self)
        {
            return self.TakeComponent<ImageComponent, Image>(true);
        }

        public static ImageComponent GetImage(this UI self, string key)
        {
            UI ui = self.GetFromKeyOrPath(key);
            return ui?.GetImage();
        }

        public static void SetSprite(this Image self, Sprite sprite, bool setNativeSize)
        {
            self.sprite = sprite;
            if (setNativeSize)
                self.SetNativeSize();
        }

        public static void SetOverrideSprite(this Image self, Sprite sprite, bool setNativeSize)
        {
            self.overrideSprite = sprite;
            if (setNativeSize)
                self.SetNativeSize();
        }

        public async static UniTask SetSpriteAsync(this Image self, XObject parent, string key, bool setNativeSize)
        {
            Sprite sprite = await ResourcesManager.LoadAssetAsync<Sprite>(parent, key);
            if (sprite == null)
                return;

            self.SetOverrideSprite(sprite, setNativeSize);
        }

        public async static void SetSprite(this Image self, XObject parent, string key, bool setNativeSize)
        {
            Sprite sprite = await ResourcesManager.LoadAssetAsync<Sprite>(parent, key);
            if (sprite == null)
                return;

            self.SetOverrideSprite(sprite, setNativeSize);
        }

        public static void SetFillAmount(this Image self, float fillAmount)
        {
            self.fillAmount = fillAmount;
        }

        #region MiniTween

        public static MiniTween DoFillAmount(this Image self, XObject parent, float endValue, float duration)
        {
            return self.DoFillAmount(parent, self.fillAmount, endValue, duration);
        }

        public static MiniTween DoFillAmount(this Image self, XObject parent, float startValue, float endValue,
            float duration)
        {
            return self.DoFloat(parent, startValue, endValue, duration, self.SetFillAmount);
        }

        #endregion
    }
}