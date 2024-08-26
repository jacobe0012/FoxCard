//---------------------------------------------------------------------
// JiYuStudio
// Author: 格伦
// Time: 2023-08-24 12:25:25
//---------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;


namespace Main
{
    // public struct BuffArgsList
    // {
    //     public int buffId;
    //
    //     public NativeArray<int> buffargs;
    //
    //     public int target;
    // }

    public static class BuffHelper
    {
        public static void DrawWireCircle(float3 center, float3 forward, float3 right, float radius, int divide,
            Color color)
        {
            radius = math.abs(radius);
            forward = math.normalize(forward);
            right = math.normalize(right);
            divide = math.abs(divide);
            if (radius == 0 || divide < 3 || Vector3.Dot(forward, right) > 0.0001f) return;
            if (divide > 32) divide = 32;
            List<float3> vertices = new List<float3>();
            float3 startPos = center + forward * radius;
            vertices.Add(startPos);
            float stepAngle = 360.0f / divide;
            float angle = 0f;
            while (angle < 360)
            {
                angle += stepAngle;
                if (angle <= 360)
                {
                    float x = radius * math.cos(math.radians(angle));
                    float y = radius * math.sin(math.radians(angle));
                    float3 vertex = center + right * y + forward * x;
                    vertices.Add(vertex);
                }
                else
                {
                    vertices.Add(startPos);
                }
            }

            for (int i = 1; i < vertices.Count; i++)
            {
                Debug.DrawLine(vertices[i - 1], vertices[i], color);
            }
        }


        public static bool SpiritCirclePos(ref NativeArray<float3> nativeArray, int num, float r)
        {
            for (var i = 0; i < nativeArray.Length; i++)
            {
                var angle = i * 360 / num;
                var x = r * math.cos(angle * math.PI / 180);
                var y = r * math.sin(angle * math.PI / 180);
                // nativeArray.
            }

            return true;
        }
    }
}