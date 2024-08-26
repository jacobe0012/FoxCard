//---------------------------------------------------------------------
// JiYuStudio
// Author: 格伦
// Time: 2023-08-24 12:25:25
//---------------------------------------------------------------------

using System.Runtime.CompilerServices;
// using FMOD.Studio;
// using FMODUnity;
using Unity.Collections;
//using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Main
{
    public static class UnityHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            var sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            var cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            var tx = v.x;
            var ty = v.y;
            v.x = cos * tx - sin * ty;
            v.y = sin * tx + cos * ty;
            return v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color HexRGB2Color(string hexRGB)
        {
            Color color;
            ColorUtility.TryParseHtmlString(hexRGB, out color);
            return color;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Color2HexRGB(Color color)
        {
            int red = (int)math.round(color.r * 255);
            int green = (int)math.round(color.g * 255);
            int blue = (int)math.round(color.b * 255);
            int alpha = (int)math.round(color.a * 255);

            string hex = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", red, green, blue, alpha);
            return hex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        /// <summary>
        /// 富文本改变文字大小
        /// </summary>
        /// <param name="input">文本</param>
        /// <param name="size">修改后的文本字体大小</param>
        public static string RichTextSize(string input, float size)
        {
            return "<size=" + size.ToString() + ">" + input + "</size>";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        /// <summary>
        /// 富文本改变文字颜色
        /// </summary>
        /// <param name="input">文本</param>
        /// <param name="hex">修改后字体颜色</param>
        public static string RichTextColor(string input, string hex)
        {
            string trimmedString = input.Trim();

            if (trimmedString.Length > 0 && trimmedString[0] == '#')
            {
                input = "<color=" + hex + ">" + input + "</color>";
            }
            else
            {
                input = "<color=#" + hex + ">" + input + "</color>";
            }

            return input;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        /// <summary>
        /// 富文本改变文字大小和颜色
        /// </summary>
        /// <param name="input">文本</param>
        /// <param name="size">修改后的文本字体大小</param>
        /// <param name="hex">修改后字体颜色</param>
        public static string RichTextSizeAndColor(string input, float size, string hex)
        {
            input = RichTextSize(input, size);
            input = RichTextColor(input, hex);
            return input;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ParticleSystemLength(Transform transform)
        {
            var pts = transform.GetComponentsInChildren<ParticleSystem>();
            float maxDuration = 0f;
            foreach (var p in pts)
            {
                if (p.enableEmission)
                {
                    if (p.loop)
                    {
                        return -1f;
                    }

                    float dunration = 0f;
                    if (p.emissionRate <= 0)
                    {
                        dunration = p.startDelay + p.startLifetime;
                    }
                    else
                    {
                        dunration = p.startDelay + math.max(p.duration, p.startLifetime);
                    }

                    if (dunration > maxDuration)
                    {
                        maxDuration = dunration;
                    }
                }
            }

            return maxDuration;
        }


      
      
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StopTime()
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BeginTime()
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }
        }

        /// <summary>
        /// float转 分钟：秒数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToTimeFormat(float time)
        {
            //秒数取整
            int seconds = (int)time;
            //一小时为3600秒 秒数对3600取整即为小时
            int hour = seconds / 3600;
            //一分钟为60秒 秒数对3600取余再对60取整即为分钟
            int minute = seconds % 3600 / 60;
            //对3600取余再对60取余即为秒数
            seconds = seconds % 3600 % 60;
            //返回00:00:00时间格式
            return string.Format("{0:D2}:{1:D2}", minute, seconds);
        }


        /// <summary>
        /// float转小时分钟秒
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="seconds"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OutTime(float time, out int hour, out int minute, out int seconds)
        {
            //秒数取整
            seconds = (int)time;
            //一小时为3600秒 秒数对3600取整即为小时
            hour = seconds / 3600;
            //一分钟为60秒 秒数对3600取余再对60取整即为分钟
            minute = seconds % 3600 / 60;
            //对3600取余再对60取余即为秒数
            seconds = seconds % 3600 % 60;
            //返回00:00:00时间格式
        }


     
        /// <summary>
        /// 迅捷蟹修改
        /// 用于Reward str转实际的Vector3
        /// </summary>
        /// <param name="str">Reward str</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 StrToVector3(string str)
        {
            Vector3 reslut = new Vector3();
            if (str == "" || !str.Contains(";"))
            {
                Debug.LogError(string.Format("后端字符串有误,返回的字符串为:{0}", str));
                return reslut;
            }

            var strings = str.Split(";");

            if (int.TryParse(strings[0], out var result0))
            {
                reslut.x = result0;
            }
            else
            {
                Debug.LogError(string.Format("reslut.x:{0}", str));
            }

            if (int.TryParse(strings[1], out var result1))
            {
                reslut.y = result1;
            }
            else
            {
                Debug.LogError(string.Format("reslut.y:{0}", str));
            }

            if (int.TryParse(strings[2], out var result2))
            {
                reslut.z = result2;
            }
            else
            {
                Debug.LogError(string.Format("reslut.z:{0}", str));
            }

            return reslut;
        }

      
    }
}