using Newtonsoft.Json;
//using ProtoBuf;
using System.Collections.Generic;

namespace XFramework
{
    [Config]
    public abstract class ConfigInstance<T> : ConfigObject where T : ProtoObject, IConfig
    {
        //[ProtoIgnore]
        protected Dictionary<int, T> dictConfigs = new Dictionary<int, T>();

        protected abstract List<T> _list { get; }

        private string configName;

        public sealed override void EndInit()
        {
            configName = typeof(T).Name;

            if (_list is null)
                return;

            foreach (T config in _list)
            {
                dictConfigs.Add(config.ConfigId, config);
                config.EndInit();
            }

            _list.Clear();

            AfterEndInit();
        }

        public int Count()
        {
            return dictConfigs.Count;
        }

        public IEnumerable<int> GetAllKeys()
        {
            return dictConfigs.Keys;
        }

        public IEnumerable<T> GetAllValues()
        {
            return dictConfigs.Values;
        }

        public T Get(int id)
        {
            if (dictConfigs.TryGetValue(id, out var config))
                return config;

            throw new System.Exception($"{configName} 配置为 {id} 不存在");
        }

        public bool Contain(int id)
        {
            return dictConfigs.ContainsKey(id);
        }
    }
}
