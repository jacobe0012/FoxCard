using System;
using System.Collections.Generic;

namespace XFramework
{
    public static class ListExtensions
    {
        /// <summary>
        /// 尝试添加一个值，不重复添加
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryAdd<TValue>(this IList<TValue> self, TValue value)
        {
            if (self.IsReadOnly)
                return false;

            if (!self.Contains(value))
            {
                self.Add(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 将index值转换为有效的index
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int ConvertIndex<TValue>(this IList<TValue> self, int index)
        {
            if (self.Count == 0)
            {
                throw new IndexOutOfRangeException("index");
            }

            index %= self.Count;
            if (index < 0)
                index += self.Count;

            return index;
        }

        /// <summary>
        /// 通过索引获取一个值，可以为负数，-1为最后一个，-2为倒数第二个，以此类推
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static TValue GetAt<TValue>(this IList<TValue> self, int index)
        {
            return self[self.ConvertIndex(index)];
        }

        /// <summary>
        /// 以升序添加到列表里，前提是这个列表本来就是有序的，不然有问题
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="value"></param>
        public static void OrderlyAdd<TValue>(this IList<TValue> self, TValue value) where TValue : IComparable<TValue>
        {
            if (self.IsReadOnly) // 只读的不能添加
                return;

            if (self.Count == 0) // 直接添加
            {
                self.Add(value);
                return;
            }

            if (self.GetAt(0).CompareTo(value) > 0) // 第一个值比要添加的值大，直接插入到第一个
            {
                self.Insert(0, value);
                return;
            }

            if (self.GetAt(-1).CompareTo(value) <= 0) // 最后一个值不超过要添加的值，直接添加到末尾
            {
                self.Add(value);
                return;
            }

            // 二分查找找到最后一个大于value的值的位置
            int left = 0, right = self.Count;
            while (left < right)
            {
                int midIndex = left + ((right - left) >> 1);
                TValue midValue = self.GetAt(midIndex);
                int ret = midValue.CompareTo(value);
                if (ret > 0)
                {
                    right = midIndex;
                }
                else
                {
                    left = midIndex + 1;
                }
            }

            // right一定小于Count，因为前面有判断
            for (int i = right; i < self.Count; i++)
            {
                if (self.GetAt(i).CompareTo(value) > 0)
                {
                    self.Insert(i, value);
                    break;
                }
            }
        }
    }
}