namespace XFramework
{
    public interface IEvent
    {
    }

    public interface IEvent<T> : IEvent where T : struct
    {
        void HandleEvent(T args);
    }
}