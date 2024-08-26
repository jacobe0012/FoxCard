//using ProtoBuf;
using System;
using System.ComponentModel;

namespace XFramework
{
    public interface IConfig
    {
        public int ConfigId { get; }
    }

    public abstract class ConfigObject : ProtoObject
    {

    }

    public abstract class ProtoObject : IDisposable, ISupportInitialize
    {
        public virtual void BeginInit()
        {
            
        }

        public virtual void EndInit()
        {
            
        }

        protected virtual void AfterEndInit()
        {

        }

        public virtual void Dispose()
        {
            
        }
    }
}
