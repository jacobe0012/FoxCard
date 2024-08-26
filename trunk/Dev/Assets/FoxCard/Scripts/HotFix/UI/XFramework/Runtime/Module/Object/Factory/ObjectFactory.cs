using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    /// <summary>
    /// 创建XObject对象的工厂类
    /// </summary>
    public static class ObjectFactory
    {
        private static XObject Get(Type type, bool isFromPool = false)
        {
            XObject obj = null;
            if (isFromPool)
                obj = ObjectPool.Instance.Fetch(type) as XObject;
            else
                obj = Activator.CreateInstance(type) as XObject;

            obj?.SetFromPool(isFromPool);
            return obj;
        }

        private static void Awake(XObject obj)
        {
            if (obj == null)
                return;

            obj.Awake();
        }

        public static T CreateNoInit<T>(bool isFromPool = false) where T : XObject, new()
        {
            return CreateNoInit(typeof(T), isFromPool) as T;
        }

        public static T Create<T>(bool isFromPool = false) where T : XObject, new()
        {
            return Create(typeof(T), isFromPool) as T;
        }

        public static T Create<T, P1>(P1 p1, bool isFromPool = false) where T : XObject, IAwake<P1>, new()
        {
            return Create(typeof(T), p1, isFromPool) as T;
        }

        public static T Create<T, P1, P2>(P1 p1, P2 p2, bool isFromPool = false) where T : XObject, IAwake<P1, P2>, new()
        {
            return Create(typeof(T), p1, p2, isFromPool) as T;
        }

        public static T Create<T, P1, P2, P3>(P1 p1, P2 p2, P3 p3, bool isFromPool = false) where T : XObject, IAwake<P1, P2, P3>, new()
        {
            return Create(typeof(T), p1, p2, p3, isFromPool) as T;
        }

        public static T Create<T, P1, P2, P3, P4>(P1 p1, P2 p2, P3 p3, P4 p4, bool isFromPool = false) where T : XObject, IAwake<P1, P2, P3, P4>, new()
        {
            return Create(typeof(T), p1, p2, p3, p4, isFromPool) as T;
        }

        public static T Create<T, P1, P2, P3, P4, P5>(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, bool isFromPool = false) where T : XObject, IAwake<P1, P2, P3, P4, P5>, new()
        {
            return Create(typeof(T),p1, p2, p3, p4 ,p5,isFromPool) as T;
        }

        public static XObject CreateNoInit(Type type, bool isFromPool = false)
        {
            var obj = Get(type, isFromPool);

            if (obj != null)
            {
                Awake(obj);
            }

            return obj;
        }

        public static XObject Create(Type type, bool isFromPool = false)
        {
            var obj = Get(type, isFromPool);

            if (obj != null)
            {
                Awake(obj);
            }

            ObjectHelper.Awake(obj);

            return obj;
        }

        public static XObject Create<P1>(Type type, P1 p1, bool isFromPool = false)
        {
            var obj = Get(type, isFromPool);

            if (obj != null)
            {
                Awake(obj);
            }

            ObjectHelper.Awake(obj, p1);

            return obj;
        }

        public static XObject Create<P1, P2>(Type type, P1 p1, P2 p2, bool isFromPool = false)
        {
            var obj = Get(type, isFromPool);

            if (obj != null)
            {
                Awake(obj);
            }

            ObjectHelper.Awake(obj, p1, p2);

            return obj;
        }

        public static XObject Create<P1, P2, P3>(Type type, P1 p1, P2 p2, P3 p3, bool isFromPool = false)
        {
            var obj = Get(type, isFromPool);

            if (obj != null)
            {
                Awake(obj);
            }

            ObjectHelper.Awake(obj, p1, p2, p3);

            return obj;
        }

        public static XObject Create<P1, P2, P3, P4>(Type type, P1 p1, P2 p2, P3 p3, P4 p4, bool isFromPool = false)
        {
            var obj = Get(type, isFromPool);

            if (obj != null)
            {
                Awake(obj);
            }

            ObjectHelper.Awake(obj, p1, p2, p3, p4);

            return obj;
        }

        public static XObject Create<P1, P2, P3, P4, P5>(Type type, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, bool isFromPool = false)
        {
            var obj = Get(type, isFromPool);

            if (obj != null)
            {
                Awake(obj);
            }

            ObjectHelper.Awake(obj, p1, p2, p3, p4, p5);

            return obj;
        }
    }
}
