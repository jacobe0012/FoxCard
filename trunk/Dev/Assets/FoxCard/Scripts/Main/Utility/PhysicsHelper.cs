//---------------------------------------------------------------------
// JiYuStudio
// Author: 格伦
// Time: 2023-08-24 12:25:25
//---------------------------------------------------------------------

using System.Runtime.CompilerServices;
using Unity.Collections;
//using Unity.Entities;
using Unity.Mathematics;

//using Unity.Physics;
//using Unity.Transforms;

namespace Main
{
    public static class PhysicsHelper
    {
        private const int FieldWidth = 4000;
        private const int FieldWidthHalf = FieldWidth / 2;
        private const int FieldHeight = 4000;
        private const int FieldHeightHalf = FieldHeight / 2;
        private const float Step = 1f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Hash(float2 position)
        {
            var quantized = new int2(math.floor(position / Step));
            return quantized.x + FieldWidthHalf + (quantized.y + FieldHeightHalf) * FieldWidth;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float2 Correction(float2 position)
        {
            var quantized = new int2(math.floor(position / Step));
            return new float2(quantized.x * Step + Step / 2, quantized.y * Step + Step / 2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 QuardaticBezier(float t, float3 points0, float3 points1, float3 points2)
        {
            float3 a = points0;
            float3 b = points1;
            float3 c = points2;

            float3 aa = a + (b - a) * t;
            float3 bb = b + (c - b) * t;
            return aa + (bb - aa) * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 CubicBezier(float t, float3 points0, float3 points1, float3 points2, float3 points3)
        {
            float3 a = points0;
            float3 b = points1;
            float3 c = points2;
            float3 d = points3;

            float3 aa = a + (b - a) * t;
            float3 bb = b + (c - b) * t;
            float3 cc = c + (d - c) * t;

            float3 aaa = aa + (bb - aa) * t;
            float3 bbb = bb + (cc - bb) * t;
            return aaa + (bbb - aaa) * t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LoopToClipTime(float time, float duration)
        {
            float wrappedTime = math.fmod(time, duration);
            wrappedTime += math.select(0f, duration, wrappedTime < 0f);
            return wrappedTime;
        }
    }
}