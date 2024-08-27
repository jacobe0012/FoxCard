namespace XFramework
{
    public interface ITimeNow
    {
        /// <summary>
        /// 毫秒级的时间
        /// </summary>
        /// <returns></returns>
        long GetTime();
    }

    /// <summary>
    /// 真实时间
    /// </summary>
    public class ActualTime : ITimeNow
    {
        public long GetTime()
        {
            return TimeHelper.ClientNow();
        }
    }
}