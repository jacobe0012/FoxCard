using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    public class UnityLogger : ILog
    {
        public void Debug(string msg)
        {
            UnityEngine.Debug.Log(msg);
        }

        public void Debug(string msg, params object[] args)
        {
            UnityEngine.Debug.LogFormat(msg, args);
        }

        public void Error(string msg)
        {
            UnityEngine.Debug.LogError(msg);
        }

        public void Error(Exception ex)
        {
            UnityEngine.Debug.LogError(ex);
        }

        public void Info(string msg)
        {
            UnityEngine.Debug.Log(msg);
        }

        public void Info(string msg, params object[] args)
        {
            UnityEngine.Debug.LogFormat(msg, args);
        }

        public void Warnning(string msg)
        {
            UnityEngine.Debug.LogWarning(msg);
        }

        public void Warnning(string msg, params object[] args)
        {
            UnityEngine.Debug.LogWarningFormat(msg);
        }
    }
}
