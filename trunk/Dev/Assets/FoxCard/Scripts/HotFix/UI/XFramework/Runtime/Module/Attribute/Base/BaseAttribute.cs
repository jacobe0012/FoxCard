using System;

namespace XFramework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BaseAttribute : Attribute
    {
        public Type AttributeType { get; private set; }

        public BaseAttribute()
        {
            this.AttributeType = GetType();
        }
    }
}