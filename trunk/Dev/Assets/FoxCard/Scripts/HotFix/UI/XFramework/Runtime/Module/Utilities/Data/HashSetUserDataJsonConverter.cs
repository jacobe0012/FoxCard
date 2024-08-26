using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace XFramework
{
    /// <summary>
    /// 自定义HashSet(UserData)序列化以及反序列化
    /// </summary>
    internal class HashSetUserDataJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jArray = JArray.Load(reader);
            HashSet<UserData> list = new HashSet<UserData>();
            foreach (var property in jArray)
            {
                var typeName = property["typeName"].ToString();
                var assmblyName = property["assemblyName"].ToString();
                Assembly ass = TypesManager.Instance.GetAssembly(assmblyName);
                ass ??= Assembly.Load(assmblyName);
                var type = ass.GetType(typeName);
                var value = property.ToObject(type);
                list.Add((UserData)value);
            }
            return list;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, JToken.FromObject(value));
        }
    }
}
