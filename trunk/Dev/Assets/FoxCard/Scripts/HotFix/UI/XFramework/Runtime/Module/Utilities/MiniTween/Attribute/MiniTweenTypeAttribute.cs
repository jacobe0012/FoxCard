using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MiniTweenTypeAttribute : BaseAttribute
    {
        public string TypeName { get; private set; }

        public MiniTweenTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}
