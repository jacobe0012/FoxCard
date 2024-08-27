using System;
using TMPro;

namespace XFramework
{
    public abstract class TMPTextComponent<T> : GraphicComponent<T> where T : TMP_Text
    {
        public string Content => this.Get().text;

        public void SetTMPText(string content)
        {
            this.Get().SetTMPText(content);
        }

        public void SetTextWithKey(string key, params object[] args)
        {
            this.Get().SetTextWithKey(key, args);
        }

        public void SetCount(long count)
        {
            this.Get().SetCount(count);
        }

        public void SetNumber(long number)
        {
            this.Get().SetNumber(number);
        }

        public void SetFontSize(float size)
        {
            this.Get().SetFontSize(size);
        }

        public void SetFontStyle(FontStyles fontStyles)
        {
            this.Get().SetFontStyle(fontStyles);
        }

        public MiniTween DoCount(long endValue, float duration)
        {
            return this.Get().DoCount(this, endValue, duration);
        }

        public MiniTween DoCount(long startValue, long endValue, float duration)
        {
            return this.Get().DoCount(this, startValue, endValue, duration);
        }

        public MiniTween DoNumber(long endValue, float duration)
        {
            return this.Get().DoNumber(this, endValue, duration);
        }

        public MiniTween DoNumber(long startValue, long endValue, float duration)
        {
            return this.Get().DoNumber(this, startValue, endValue, duration);
        }
    }

    public class TextMeshProUGUIComponent : TMPTextComponent<TextMeshProUGUI>
    {
    }

    public static class TextMeshProExtensions
    {
        public static TextMeshProUGUIComponent GetTextMeshPro(this UI self)
        {
            return self.TakeComponent<TextMeshProUGUIComponent, TextMeshProUGUI>(true);
        }

        public static TextMeshProUGUIComponent GetTextMeshPro(this UI self, string key)
        {
            UI ui = self.GetFromKeyOrPath(key);
            return ui?.GetTextMeshPro();
        }

        #region TMP_Text

        public static void SetTMPText(this TMP_Text self, string content)
        {
            if (self is IMultilingual xt)
                xt.SetText(content);
            else
                self.text = content;
        }

        /// <summary>
        /// 仅这个接口支持多语言
        /// <para>如果是要访问多语言的key，字符串前后需要用$包围，例如key: TestKey，则需传入$TestKey$</para>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="args"></param>
        public static void SetTextWithKey(this TMP_Text self, string key, params object[] args)
        {
            if (self is IMultilingual xt)
                xt.SetTextWithKey(key, args);
            else
                self.text = string.Format(key, args);
        }

        public static void SetCount(this TMP_Text self, long count)
        {
            if (self is IMultilingual xt)
                xt.SetKey(string.Empty);

            self.text = count.ToString();
        }

        public static void SetNumber(this TMP_Text self, long number)
        {
            if (self is IMultilingual xt)
                xt.SetKey(string.Empty);

            self.text = number.ToString();
        }

        public static void SetFontSize(this TMP_Text self, float size)
        {
            self.fontSize = size;
        }

        public static void SetFontStyle(this TMP_Text self, FontStyles fontStyles)
        {
            self.fontStyle = fontStyles;
        }

        public static void SetTime(this TMP_Text self, long time, bool ms)
        {
            LanguageHelper.ConversionTime(time, ms, out var hour, out var minute, out var second);

            // ToString可以避免值类型转object的装箱操作
            self.SetTextWithKey("{0}:{1}:{2}", hour.ToString("D2"), minute.ToString("D2"), second.ToString("D2"));
        }

        #endregion

        #region MiniTween

        public static MiniTween DoCount(this TMP_Text self, XObject parent, long endValue, float duration)
        {
            long.TryParse(self.text, out long startValue);
            return self.DoCount(parent, startValue, endValue, duration);
        }

        public static MiniTween DoCount(this TMP_Text self, XObject parent, long startValue, long endValue,
            float duration)
        {
            return self.DoLong(parent, startValue, endValue, duration, self.SetCount);
        }

        public static MiniTween DoNumber(this TMP_Text self, XObject parent, long endValue, float duration)
        {
            long.TryParse(self.text, out long startValue);
            return self.DoNumber(parent, startValue, endValue, duration);
        }

        public static MiniTween DoNumber(this TMP_Text self, XObject parent, long startValue, long endValue,
            float duration)
        {
            return self.DoLong(parent, startValue, endValue, duration, self.SetNumber);
        }

        private static MiniTween DoLong(this TMP_Text self, XObject parent, long startValue, long endValue,
            float duration, Action<long> setValue)
        {
            var tweenMgr = Common.Instance.Get<MiniTweenManager>();
            if (tweenMgr is null)
                return null;

            var tween = tweenMgr.To(parent, startValue, endValue, duration);
            tween.AddListener(n =>
            {
                if (!self)
                {
                    tween.Cancel(parent);
                    return;
                }

                setValue.Invoke(n);
            });

            return tween;
        }

        #endregion
    }
}