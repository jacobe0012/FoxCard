using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Main;
using UnityEngine;

namespace XFramework
{
    public static class Log
    {
        public static ILog ILog { get; set; }


        /// <summary>
        /// Log的最低级别，小于这个的不显示Log
        /// </summary>
        public static int LogLevel { get; set; } = DebugLevel;

        private const int DebugLevel = 1;
        private const int InfoLevel = 2;
        private const int WarnningLevel = 3;

        private static bool CheckLogLevel(int level)
        {
            return LogLevel <= level;
        }

        public static void Debug(string msg, Color color)
        {
            if (!CheckLogLevel(DebugLevel))
                return;
            var hex = UnityHelper.Color2HexRGB(color);
            string str = $"<color={hex}>{msg}</color>";
            ILog.Debug(str);
        }

        public static void Debug(string msg)
        {
            if (!CheckLogLevel(DebugLevel))
                return;

            ILog.Debug(msg);
        }

        public static void Debug(string msg, params object[] args)
        {
            if (!CheckLogLevel(DebugLevel))
                return;

            ILog.Debug(msg, args);
        }

        public static void Info(string msg)
        {
            if (!CheckLogLevel(InfoLevel))
                return;

            ILog.Info(msg);
        }

        public static void Info(string msg, params object[] args)
        {
            if (!CheckLogLevel(InfoLevel))
                return;

            ILog.Info(msg, args);
        }

        public static void Warnning(string msg)
        {
            if (!CheckLogLevel(WarnningLevel))
                return;

            ILog.Warnning(msg);
        }

        public static void Warnning(string msg, params object[] args)
        {
            if (!CheckLogLevel(WarnningLevel))
                return;

            ILog.Warnning(msg, args);
        }

        public static void Error(string msg)
        {
            StackTrace st = new StackTrace(1, true);
            string content = $"{msg}\n{st}";
            ILog.Error(content);
        }

        public static void Error(Exception ex)
        {
            if (ex.Data.Contains("StackTrace"))
            {
                ILog.Error($"{ex.Data["StackTrace"]}\n{ex}");
                return;
            }

            ILog.Error(ex);
        }

        public static void Error(string msg, params object[] args)
        {
            StackTrace st = new StackTrace(1, true);
            string content = string.Format(msg, args);
            content = $"{content}\n{st}";
            ILog.Error(content);
        }
    }
}