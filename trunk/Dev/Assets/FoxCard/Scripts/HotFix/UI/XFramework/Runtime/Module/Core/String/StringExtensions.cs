using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        public static bool IsNullOrWhiteSpace(this string self)
        {
            return string.IsNullOrWhiteSpace(self);
        }

        public static char GetAt(this string self, int index)
        {
            if (self.Length == 0)
            {
                throw new System.IndexOutOfRangeException("index");
            }

            index %= self.Length;
            if (index < 0)
                index += self.Length;

            return self[index];
        }

        /// <summary>
        /// 首尾的字符是否相同
        /// </summary>
        /// <param name="self"></param>
        /// <param name="ch">需要检测的字符</param>
        /// <param name="length">需要满足的长度(min: 2)</param>
        /// <returns></returns>
        public static bool IsSameBeginAndEnd(this string self, char ch, int length = 2)
        {
            if (length < 2)
                length = 2;

            if (self.IsNullOrEmpty() || self.Length < length)
                return false;

            return self[0] == ch && ch == self.GetAt(-1);
        }
    }
}
