using Newtonsoft.Json;
using System;

namespace XFramework
{
    public static class JsonHelper
    {
        public static string ToJson(object obj, Formatting formatting)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static object ToObject(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
