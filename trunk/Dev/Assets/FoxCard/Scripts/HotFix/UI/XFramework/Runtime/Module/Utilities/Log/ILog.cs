using System.Collections;
using System.Collections.Generic;
using System;

namespace XFramework
{
    public interface ILog
    {
        void Debug(string msg);

        void Debug(string msg, params object[] args);

        void Info(string msg);

        void Info(string msg, params object[] args);

        void Warnning(string msg);

        void Warnning(string msg, params object[] args);

        void Error(string msg);

        void Error(Exception ex);
    }
}
