namespace XFramework
{
    public interface IAwake
    {
        void Initialize();
    }

    public interface IAwake<P1>
    {
        void Initialize(P1 args);
    }

    public interface IAwake<P1, P2>
    {
        void Initialize(P1 arg1, P2 arg2);
    }

    public interface IAwake<P1, P2, P3>
    {
        void Initialize(P1 arg1, P2 arg2, P3 arg3);
    }

    public interface IAwake<P1, P2, P3, P4>
    {
        void Initialize(P1 arg1, P2 arg2, P3 arg3, P4 arg4);
    }

    public interface IAwake<P1, P2, P3, P4, P5>
    {
        void Initialize(P1 arg1, P2 arg2, P3 arg3, P4 arg4, P5 arg5);
    }
}