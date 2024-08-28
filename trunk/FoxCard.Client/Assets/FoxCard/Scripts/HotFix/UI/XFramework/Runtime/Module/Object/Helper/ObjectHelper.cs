using System;

namespace XFramework
{
    public static class ObjectHelper
    {
        public static void Awake(XObject obj)
        {
            try
            {
                if (obj is IAwake o)
                    o.Initialize();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Awake<P1>(XObject obj, P1 p1)
        {
            try
            {
                if (obj is IAwake<P1> o)
                    o.Initialize(p1);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Awake<P1, P2>(XObject obj, P1 p1, P2 p2)
        {
            try
            {
                if (obj is IAwake<P1, P2> o)
                    o.Initialize(p1, p2);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Awake<P1, P2, P3>(XObject obj, P1 p1, P2 p2, P3 p3)
        {
            try
            {
                if (obj is IAwake<P1, P2, P3> o)
                    o.Initialize(p1, p2, p3);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Awake<P1, P2, P3, P4>(XObject obj, P1 p1, P2 p2, P3 p3, P4 p4)
        {
            try
            {
                if (obj is IAwake<P1, P2, P3, P4> o)
                    o.Initialize(p1, p2, p3, p4);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Awake<P1, P2, P3, P4, P5>(XObject obj, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            try
            {
                if (obj is IAwake<P1, P2, P3, P4, P5> o)
                    o.Initialize(p1, p2, p3, p4, p5);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Deserialize(XObject obj)
        {
            try
            {
                obj.Awake();
                if (obj is IDeserialize o)
                    o.Deserialize();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}