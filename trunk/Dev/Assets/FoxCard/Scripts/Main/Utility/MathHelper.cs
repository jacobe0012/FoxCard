//---------------------------------------------------------------------
// JiYuStudio
// Author: 格伦
// Time: 2023-08-24 12:25:25
//---------------------------------------------------------------------


using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main
{
    public static class MathHelper
    {
        #region Random

        private const int A = 16807;
        private const int M = 2147483647;
        private const int Q = 127773;
        private const int R = 2836;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetRandomSeed()
        {
            return Random.Range(1, int.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetRandomBySeed(this ref int seed)
        {
            var hi = seed / Q;
            var lo = seed % Q;
            seed = A * lo - R * hi;
            if (seed <= 0) seed += M;
            return seed * 1.0f / M;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetRandomSeedBySeed(this ref int seed)
        {
            return (int)(int.MaxValue * seed.GetRandomBySeed());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetRandomRange(this ref int seed, int min, int max)
        {
            return (int)math.floor(seed.GetRandomBySeed() * (max - min) + min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetRandomRange(this ref int seed, float min, float max)
        {
            return seed.GetRandomBySeed() * (max - min) + min;
        }

        #endregion

        #region float2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float VectorAngleUnsign(float3 from, float3 to)
        {
            float num = (float)math.sqrt((double)(math.length(from) * math.length(from)) * (double)(math.length(to) *
                math.length(to)));
            return (double)num < 1.00000000362749E-15
                ? 0.0f
                : (float)math.acos((double)math.clamp(math.dot(from, to) / num, -1f, 1f)) * 57.29578f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float VectorAngleSign(float3 from, float3 to)
        {
            float angle;
            float3 cross = math.cross(from, to);
            angle = VectorAngleUnsign(from, to);
            return cross.y > 0 ? -angle : angle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ToFloat3(this float2 v)
        {
            return new float3(v.x, 0, v.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ToFloat2(this float3 v)
        {
            return new float2(v.x, v.z);
        }

        #endregion

        #region float

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckMax(this ref float f, float max)
        {
            if (f > max) f = max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckMin(this ref float f, float min)
        {
            if (f < min) f = min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetMax(this ref float f, float other)
        {
            if (other > f) f = other;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetMin(this ref float f, float other)
        {
            if (other < f) f = other;
        }

        #endregion

        #region int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckMax(this ref int i, int max)
        {
            if (i > max) i = max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckMin(this ref int i, int min)
        {
            if (i < min) i = min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetMax(this ref int i, int other)
        {
            if (other > i) i = other;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetMin(this ref int i, int other)
        {
            if (other < i) i = other;
        }

        #endregion

        #region toEuler

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 Qua2Euler(quaternion q, math.RotationOrder order = math.RotationOrder.Default)
        {
            const float epsilon = 1e-6f;

            //prepare the data
            var qv = q.value;
            var d1 = qv * qv.wwww * new float4(2.0f); //xw, yw, zw, ww
            var d2 = qv * qv.yzxw * new float4(2.0f); //xy, yz, zx, ww
            var d3 = qv * qv;
            var euler = new float3(0.0f);

            const float CUTOFF = (1.0f - 2.0f * epsilon) * (1.0f - 2.0f * epsilon);

            switch (order)
            {
                case math.RotationOrder.ZYX:
                {
                    var y1 = d2.z + d1.y;
                    if (y1 * y1 < CUTOFF)
                    {
                        var x1 = -d2.x + d1.z;
                        var x2 = d3.x + d3.w - d3.y - d3.z;
                        var z1 = -d2.y + d1.x;
                        var z2 = d3.z + d3.w - d3.y - d3.x;
                        euler = new float3(math.atan2(x1, x2), math.asin(y1), math.atan2(z1, z2));
                    }
                    else //zxz
                    {
                        y1 = math.clamp(y1, -1.0f, 1.0f);
                        var abcd = new float4(d2.z, d1.y, d2.y, d1.x);
                        var x1 = 2.0f * (abcd.x * abcd.w + abcd.y * abcd.z); //2(ad+bc)
                        var x2 = math.csum(abcd * abcd * new float4(-1.0f, 1.0f, -1.0f, 1.0f));
                        euler = new float3(math.atan2(x1, x2), math.asin(y1), 0.0f);
                    }

                    break;
                }

                case math.RotationOrder.ZXY:
                {
                    var y1 = d2.y - d1.x;
                    if (y1 * y1 < CUTOFF)
                    {
                        var x1 = d2.x + d1.z;
                        var x2 = d3.y + d3.w - d3.x - d3.z;
                        var z1 = d2.z + d1.y;
                        var z2 = d3.z + d3.w - d3.x - d3.y;
                        euler = new float3(math.atan2(x1, x2), -math.asin(y1), math.atan2(z1, z2));
                    }
                    else //zxz
                    {
                        y1 = math.clamp(y1, -1.0f, 1.0f);
                        var abcd = new float4(d2.z, d1.y, d2.y, d1.x);
                        var x1 = 2.0f * (abcd.x * abcd.w + abcd.y * abcd.z); //2(ad+bc)
                        var x2 = math.csum(abcd * abcd * new float4(-1.0f, 1.0f, -1.0f, 1.0f));
                        euler = new float3(math.atan2(x1, x2), -math.asin(y1), 0.0f);
                    }

                    break;
                }

                case math.RotationOrder.YXZ:
                {
                    var y1 = d2.y + d1.x;
                    if (y1 * y1 < CUTOFF)
                    {
                        var x1 = -d2.z + d1.y;
                        var x2 = d3.z + d3.w - d3.x - d3.y;
                        var z1 = -d2.x + d1.z;
                        var z2 = d3.y + d3.w - d3.z - d3.x;
                        euler = new float3(math.atan2(x1, x2), math.asin(y1), math.atan2(z1, z2));
                    }
                    else //yzy
                    {
                        y1 = math.clamp(y1, -1.0f, 1.0f);
                        var abcd = new float4(d2.x, d1.z, d2.y, d1.x);
                        var x1 = 2.0f * (abcd.x * abcd.w + abcd.y * abcd.z); //2(ad+bc)
                        var x2 = math.csum(abcd * abcd * new float4(-1.0f, 1.0f, -1.0f, 1.0f));
                        euler = new float3(math.atan2(x1, x2), math.asin(y1), 0.0f);
                    }

                    break;
                }

                case math.RotationOrder.YZX:
                {
                    var y1 = d2.x - d1.z;
                    if (y1 * y1 < CUTOFF)
                    {
                        var x1 = d2.z + d1.y;
                        var x2 = d3.x + d3.w - d3.z - d3.y;
                        var z1 = d2.y + d1.x;
                        var z2 = d3.y + d3.w - d3.x - d3.z;
                        euler = new float3(math.atan2(x1, x2), -math.asin(y1), math.atan2(z1, z2));
                    }
                    else //yxy
                    {
                        y1 = math.clamp(y1, -1.0f, 1.0f);
                        var abcd = new float4(d2.x, d1.z, d2.y, d1.x);
                        var x1 = 2.0f * (abcd.x * abcd.w + abcd.y * abcd.z); //2(ad+bc)
                        var x2 = math.csum(abcd * abcd * new float4(-1.0f, 1.0f, -1.0f, 1.0f));
                        euler = new float3(math.atan2(x1, x2), -math.asin(y1), 0.0f);
                    }

                    break;
                }

                case math.RotationOrder.XZY:
                {
                    var y1 = d2.x + d1.z;
                    if (y1 * y1 < CUTOFF)
                    {
                        var x1 = -d2.y + d1.x;
                        var x2 = d3.y + d3.w - d3.z - d3.x;
                        var z1 = -d2.z + d1.y;
                        var z2 = d3.x + d3.w - d3.y - d3.z;
                        euler = new float3(math.atan2(x1, x2), math.asin(y1), math.atan2(z1, z2));
                    }
                    else //xyx
                    {
                        y1 = math.clamp(y1, -1.0f, 1.0f);
                        var abcd = new float4(d2.x, d1.z, d2.z, d1.y);
                        var x1 = 2.0f * (abcd.x * abcd.w + abcd.y * abcd.z); //2(ad+bc)
                        var x2 = math.csum(abcd * abcd * new float4(-1.0f, 1.0f, -1.0f, 1.0f));
                        euler = new float3(math.atan2(x1, x2), math.asin(y1), 0.0f);
                    }

                    break;
                }

                case math.RotationOrder.XYZ:
                {
                    var y1 = d2.z - d1.y;
                    if (y1 * y1 < CUTOFF)
                    {
                        var x1 = d2.y + d1.x;
                        var x2 = d3.z + d3.w - d3.y - d3.x;
                        var z1 = d2.x + d1.z;
                        var z2 = d3.x + d3.w - d3.y - d3.z;
                        euler = new float3(math.atan2(x1, x2), -math.asin(y1), math.atan2(z1, z2));
                    }
                    else //xzx
                    {
                        y1 = math.clamp(y1, -1.0f, 1.0f);
                        var abcd = new float4(d2.z, d1.y, d2.x, d1.z);
                        var x1 = 2.0f * (abcd.x * abcd.w + abcd.y * abcd.z); //2(ad+bc)
                        var x2 = math.csum(abcd * abcd * new float4(-1.0f, 1.0f, -1.0f, 1.0f));
                        euler = new float3(math.atan2(x1, x2), -math.asin(y1), 0.0f);
                    }

                    break;
                }
            }

            return eulerReorderBack(euler, order);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float3 eulerReorderBack(float3 euler, math.RotationOrder order)
        {
            switch (order)
            {
                case math.RotationOrder.XZY:
                    return euler.xzy;
                case math.RotationOrder.YZX:
                    return euler.yzx;
                case math.RotationOrder.YXZ:
                    return euler.yxz;
                case math.RotationOrder.ZXY:
                    return euler.zxy;
                case math.RotationOrder.ZYX:
                    return euler.zyx;
                case math.RotationOrder.XYZ:
                    return euler.xyz;
                default:
                    return euler;
            }
        }


        /// <summary>
        ///   <para>Calculates the signed angle between vectors from and to in relation to axis.</para>
        /// </summary>
        /// <param name="from">The vector from which the angular difference is measured.</param>
        /// <param name="to">The vector to which the angular difference is measured.</param>
        /// <param name="axis">A vector around which the other vectors are rotated.</param>
        /// <returns>
        ///   <para>Returns the signed angle between from and to in degrees.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(float3 from, float3 to, float3 axis = default)
        {
            //项目的默认z轴正方向
            axis = new float3(0, 0, -1);
            float num1 = Angle(from, to);
            float num2 = (float)((double)from.y * (double)to.z - (double)from.z * (double)to.y);
            float num3 = (float)((double)from.z * (double)to.x - (double)from.x * (double)to.z);
            float num4 = (float)((double)from.x * (double)to.y - (double)from.y * (double)to.x);
            float num5 = Sign((float)((double)axis.x * (double)num2 + (double)axis.y * (double)num3 +
                                      (double)axis.z * (double)num4));
            return num1 * num5;
        }


        /// <summary>
        ///   <para>Calculates the angle between vectors from and.</para>
        /// </summary>
        /// <param name="from">The vector from which the angular difference is measured.</param>
        /// <param name="to">The vector to which the angular difference is measured.</param>
        /// <returns>
        ///   <para>The angle in degrees between the two vectors.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(float3 from, float3 to)
        {
            float num = (float)math.sqrt((double)from.SqrMagnitude() * (double)to.SqrMagnitude());
            return (double)num < 1.0000000036274937E-15
                ? 0.0f
                : (float)math.acos((double)math.clamp(math.dot(from, to) / num, -1f, 1f)) * 57.29578f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrMagnitude(this float3 flo3)
        {
            return (float)((double)flo3.x * (double)flo3.x + (double)flo3.y * (double)flo3.y +
                           (double)flo3.z * (double)flo3.z);
        }

        /// <summary>
        ///   <para>Returns the sign of f.</para>
        /// </summary>
        /// <param name="f"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sign(float f) => (double)f >= 0.0 ? 1f : -1f;

        #endregion

        #region damageNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ConvertNumberisKOrM(int number)
        {
            float result = number;

            if (number >= 1000 && number < 1000000)
            {
                result = number / 1000f;
                result = math.round(result * 100) / 100f;
            }
            else if (number >= 1000000)
            {
                result = number / 1000000f;
                result = math.round(result * 100) / 100f;
            }

            return result;
        }

        public static int2x4 isKOrM(int sumNumber)
        {
            return new int2x4
            {
                c0 = default,
                c1 = default,
                c2 = default,
                c3 = default
            };
        }


        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        public static void QuickSortAlgorithm(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high);

                QuickSortAlgorithm(arr, low, pivotIndex - 1);
                QuickSortAlgorithm(arr, pivotIndex + 1, high);
            }
        }

        public static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;
            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, high);
            return i + 1;
        }

        public static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        #endregion

        #region Rotation

        /// <summary>
        /// 根据方向算出Quaternion
        /// </summary>
        /// <param name="dir">方向</param>
        /// <returns>quaternion</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion DirToQuan(float3 dir, float3 orginalDir = default)
        {
            if (orginalDir.Equals(float3.zero))
            {
                orginalDir = new float3(0, 1, 0);
            }

            float needAngel = MathHelper.SignedAngle(math.normalize(dir),
                orginalDir);
            return quaternion.AxisAngle(new float3(0, 0, 1), math.radians(needAngel));
        }

        #endregion
    }
}