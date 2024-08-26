using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public static class LanguageHelper
    {
        private static LanguageManager GetLanguageManager()
        {
            return Common.Instance.Get<LanguageManager>();
        }

        public static void SetText(this IMultilingual self, string content)
        {
            self.SetKey(string.Empty);
            self.SetContent(content);
        }

        /// <summary>
        /// 仅这个接口支持多语言
        /// <para>如果是要访问多语言的key，字符串前后需要用$包围，例如key: TestKey，则需传入$TestKey$</para>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="args"></param>
        public static void SetTextWithKey(this IMultilingual self, string key, params object[] args)
        {
            if (!self.IgnoreLanguage)
            {
                self.SetKey(key, args);
                self.SetContent(LanguageHelper.FormatText(key, args));
            }
            else
            {
                self.SetContent(string.Format(key, args));
            }
        }

        public static void RefreshText(this IMultilingual self)
        {
            if (self.IgnoreLanguage)
                return;

            self.SetContent(LanguageHelper.FormatText(self.Key, self.Args));
        }

        /// <summary>
        /// 多语言的key转换为真实的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            var mgr = GetLanguageManager();
            if (mgr is null)
                return key;

            return mgr.GetValue(key);
        }

        /// <summary>
        /// 多语言转换
        /// <para>如果是要访问多语言的key，字符串前后需要用$包围，例如key: TestKey，则需传入$TestKey$</para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatText(string key, params object[] args)
        {
            var mgr = GetLanguageManager();

            key ??= string.Empty;
            if (CheckKey(key))
                key = mgr.GetValue(key);

            if (args != null && args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] is string str)
                    {
                        if (CheckKey(str))
                            args[i] = mgr.GetValue(str);
                    }
                }
            }

            return string.Format(key, args);
        }

        public static bool CheckKey(string key)
        {
            return key.IsSameBeginAndEnd('$', 3);
        }

        /// <summary>
        /// 转换时间
        /// </summary>
        /// <param name="time"></param>
        /// <param name="ms"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public static void ConversionTime(long time, bool ms, out int hour, out int minute, out int second)
        {
            time = ms ? time / 1000 : time;
            hour = (int)(time / 3600);
            minute = (int)(time / 60 % 60);
            second = (int)(time % 60);
        }
    }
}
