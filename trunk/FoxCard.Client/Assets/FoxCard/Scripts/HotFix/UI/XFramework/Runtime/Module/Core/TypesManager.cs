using System;
using System.Collections.Generic;
using System.Reflection;

namespace XFramework
{
    /// <summary>
    /// 类型集合
    /// </summary>
    public sealed class TypesManager : Singleton<TypesManager>, IDisposable
    {
        /// <summary>
        /// 注册过的程序集
        /// </summary>
        private Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

        /// <summary>
        /// 存储所有的被标记了ClassBaseAttribute的类
        /// <para>Key : 标记的Attribute</para>
        /// <para>Value : 被标记的类的Type列表</para>
        /// </summary>
        private UnOrderMapSet<Type, Type> allTypes = new UnOrderMapSet<Type, Type>();

        /// <summary>
        /// 所有继承了IEvent(T)接口的类
        /// <para>Key : 类的Type</para>
        /// <para>Value : T类型列表</para>
        /// </summary>
        private UnOrderMapSet<Type, Type> allEvents = new UnOrderMapSet<Type, Type>();

        /// <summary>
        /// 所有继承了IInternalEvent(T)接口的类
        /// <para>Key : 类的Type</para>
        /// <para>Value : T类型列表</para>
        /// </summary>
        private UnOrderMapSet<Type, Type> allInternalEvents = new UnOrderMapSet<Type, Type>();

        /// <summary>
        /// 所有标记了ObjectSystem类
        /// </summary>
        private Dictionary<Type, UnOrderMapSet<Type, object>> allSystem =
            new Dictionary<Type, UnOrderMapSet<Type, object>>();

        /// <summary>
        /// 继承了IDestroy接口的接口类型
        /// </summary>
        private UnOrderMapSet<Type, Type> allSystem2 = new UnOrderMapSet<Type, Type>();

        public void Init()
        {
            Add(this.GetType().Assembly);
        }

        /// <summary>
        /// 添加程序集
        /// </summary>
        /// <param name="ass"></param>
        public void Add(Assembly ass)
        {
            string assName = ass.GetName().Name;
            assemblies[assName] = ass;

            allTypes.Clear();
            allEvents.Clear();
            foreach (Assembly assembly in assemblies.Values)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsAbstract)
                        continue;

                    //找到所有标记了BaseAttribute特性的类，包括其继承的特性
                    //然后存到allType里面去，方便后续获取标记特性的类型
                    var clasAttris = type.GetCustomAttributes(typeof(BaseAttribute), true);
                    if (clasAttris.Length > 0)
                    {
                        foreach (BaseAttribute attri in clasAttris)
                        {
                            allTypes.Add(attri.AttributeType, type);
                        }
                    }

                    var interfaces = type.GetInterfaces();
                    if (interfaces.Length > 0)
                    {
                        foreach (Type interfaceType in interfaces)
                        {
                            //Log.Info(interfaceType.Name);
                            //此处是将继承了IEvent<T>接口的类里面的T的类型存在，方便后续一次性注册事件
                            if (interfaceType.Name == "IEvent`1")
                            {
                                Type[] genericArguments = interfaceType.GetGenericArguments();
                                foreach (Type argumentType in genericArguments)
                                {
                                    allEvents.Add(type, argumentType);
                                }
                            }

                            if (interfaceType.Name == "IInternalEvent`1")
                            {
                                Type[] genericArguments = interfaceType.GetGenericArguments();
                                foreach (Type argumentType in genericArguments)
                                {
                                    allInternalEvents.Add(type, argumentType);
                                }
                            }

                            if (interfaceType.Name != nameof(IDestroy))
                            {
                                if (typeof(IDestroy).IsAssignableFrom(interfaceType))
                                {
                                    allSystem2.Add(type, interfaceType);
                                }
                            }
                        }
                    }
                }
            }

            CreateSystem();
        }

        /// <summary>
        /// 创建系统类
        /// </summary>
        private void CreateSystem()
        {
            allSystem.Clear();
            var types = GetTypes(typeof(ObjectSystemAttribute));
            foreach (var type in types)
            {
                var obj = Activator.CreateInstance(type);
                if (obj is ISystemType systemType)
                {
                    if (!allSystem.TryGetValue(systemType.GetSystemType(), out var dict))
                    {
                        dict = new UnOrderMapSet<Type, object>();
                        allSystem.Add(systemType.GetSystemType(), dict);
                    }

                    if (obj is IObjectType objectType)
                    {
                        dict.Add(objectType.GetObjectType(), obj);
                    }
                }
            }
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public Assembly GetAssembly(string assemblyName)
        {
            return assemblies.Get(assemblyName);
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="result"></param>
        public void GetAssemblies(ICollection<Assembly> result)
        {
            foreach (var item in assemblies.Values)
            {
                result.Add(item);
            }
        }

        /// <summary>
        /// 获取标记了指定特性的类
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public HashSet<Type> GetTypes(Type attributeType)
        {
            if (allTypes.TryGetValue(attributeType, out var set))
                return set;

            return new HashSet<Type>();
        }

        /// <summary>
        /// 获取所有继承了IEvent(T)其中T的类型
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public HashSet<Type> GetEvents(Type objectType)
        {
            if (allEvents.TryGetValue(objectType, out var set))
                return set;

            return new HashSet<Type>();
        }

        /// <summary>
        /// 尝试获取所有继承了IEvent(T)其中T的类型
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetEvents(Type objectType, out HashSet<Type> result)
        {
            result = null;

            if (allEvents.TryGetValue(objectType, out var set))
                result = set;

            return result != null;
        }

        /// <summary>
        /// 尝试获取所有继承了InternalIEvent(T)其中T的类型
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetInternalEvents(Type objectType, out HashSet<Type> result)
        {
            result = null;

            if (allInternalEvents.TryGetValue(objectType, out var set))
                result = set;

            return result != null;
        }

        /// <summary>
        /// 尝试获取某系统列表
        /// </summary>
        /// <param name="systemType"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetSystems(Type systemType, Type objType, List<object> result)
        {
            if (allSystem.TryGetValue(systemType, out var dict))
            {
                if (dict.TryGetValue(objType, out var ret))
                {
                    result.AddRange(ret);
                }

                if (allSystem2.TryGetValue(objType, out var ret1))
                {
                    foreach (var type in ret1)
                    {
                        if (dict.TryGetValue(type, out ret))
                            result.AddRange(ret);
                    }
                }
            }

            return result.Count > 0;
        }

        public void Dispose()
        {
            assemblies.Clear();
            allTypes.Clear();
            allSystem.Clear();
            allSystem2.Clear();
            Instance = null;
        }
    }
}