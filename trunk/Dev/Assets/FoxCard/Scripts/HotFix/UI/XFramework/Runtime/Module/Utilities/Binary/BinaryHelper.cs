using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFramework
{
    public static class BinaryHelper
    {
        public static bool IsOne(int value, int index)
        {
            return (value & 1 << index) != 0;
        }

        public static bool IsZero(int value, int index)
        {
            return !IsOne(value, index);
        }

        public static int SetValue(int value, int index, bool flag)
        {
            if (flag)
                return SetOne(value, index);
            else
                return SetZero(value, index);
        }

        public static int SetOne(int value, int index)
        {
            return value |= 1 << index;
        }

        public static int SetZero(int value, int index)
        {
            return value & ~(1 << index);
        }

        public static string ToBitStr(int value)
        {
            if (value <= 0)
                return "0";

            using var xsb = XStringBuilder.Create();
            var sb = xsb.Get();
            while (value > 0)
            {
                sb.Insert(0, value & 1);
                value >>= 1;
            }

            return sb.ToString();
        }
    }
}
