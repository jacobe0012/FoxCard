//using ProtoBuf;
using System;
using System.IO;

namespace XFramework
{
    public static class ProtobufHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="obj"></param>
        public static void ToBytes(Stream stream, object obj)
        {
            //Serializer.Serialize(stream, obj);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public static void ToBytes(string path, object obj)
        {
            using FileStream file = File.Create(path);
            ToBytes(file, obj);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        public static object FromBytes(byte[] bytes, Type objType)
        {
            // using MemoryStream memory = new MemoryStream(bytes);
            // //object obj = Serializer.Deserialize(objType, memory);
            // if (obj is System.ComponentModel.ISupportInitialize o)
            // {
            //     o.EndInit();
            // }
            //
            return null;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="modelTypeFullName"></param>
        /// <returns></returns>
        public static object FromBytes(byte[] bytes, string modelTypeFullName)
        {
            Type objType = Type.GetType(modelTypeFullName);
            return FromBytes(bytes, objType);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        // public static T FromBytes<T>(byte[] bytes) where T : new()
        // {
        //     // using MemoryStream memory = new MemoryStream(bytes);
        //     // var obj = Serializer.Deserialize<T>(memory);
        //     // if (obj is System.ComponentModel.ISupportInitialize o)
        //     // {
        //     //     o.EndInit();
        //     // }
        //
        //     return null;
        // }
    }
}
