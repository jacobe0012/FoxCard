using System;

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