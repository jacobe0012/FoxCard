using System;
using HotFix_UI;

namespace XFramework
{
    public static class TimeHelper
    {
        /// <summary>
        /// 一天的时间戳（毫秒）
        /// </summary>
        public const long OneDay = 86400000;

        /// <summary>
        /// 一个小时的时间戳（毫秒）
        /// </summary>
        public const long Hour = 3600000;

        /// <summary>
        /// 一分钟的时间戳（毫秒）
        /// </summary>
        public const long Minute = 60000;

        /// <summary>
        /// 一秒钟的时间戳（毫秒）
        /// </summary>
        public const long Second = 1000;

        /// <summary>
        /// 客户端的时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        public static long ClientNow()
        {
            return TimeInfo.Instance.ClientNow();
        }

        /// <summary>
        /// 客户端的时间戳（秒）
        /// </summary>
        /// <returns></returns>
        public static long ClientNowSeconds()
        {
            return TimeInfo.Instance.ClientNowSeconds();
        }

        /// <summary>
        /// 根据时间戳（毫秒）获取时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(long timeStamp)
        {
            return TimeInfo.Instance.ToDateTime(timeStamp);
        }

        /// <summary>
        /// 时间差（毫秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static long Transition(DateTime dateTime, bool utc)
        {
            return TimeInfo.Instance.Transition(dateTime, utc);
        }

        /// <summary>
        /// 获取今天的时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        public static long GetTodayTime()
        {
            long now = ClientNow();
            var dateTime = ToDateTime(now);
            var todayTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).ToUniversalTime();
            return Transition(todayTime, true);
        }

        /// <summary>
        /// 是否处于今天
        /// </summary>
        /// <param name="timeStamp">毫秒级时间戳</param>
        /// <returns></returns>
        public static bool IsToday(long timeStamp)
        {
            long today = GetTodayTime();
            long tomorrow = today + OneDay;
            return timeStamp >= today && timeStamp < tomorrow;
        }

        /// <summary>
        /// 获得今天0点到当前时间的时间戳,考虑时区
        /// </summary>
        /// <returns></returns>
        public static long GetNowToTodayTime()
        {
            //拿到1970到今天0.00的时间戳
            long todayTime = GetTodayTime();
            //拿到1970到当前时间的时间戳
            long NowTime = ClientNowSeconds();

            long NowToTodayTime = NowTime - todayTime / 1000;
            return NowToTodayTime;
        }

        /// <summary>
        /// 获得明天0点到现在的时间戳 秒
        /// </summary>
        /// <returns></returns>
        public static long GetToTomorrowTime()
        {
            DateTime now = DateTime.Now;

            // 获取明天的日期
            DateTime tomorrow = now.AddDays(1).Date;

            // 设置明天的时间为0点0分0秒
            DateTime tomorrowMidnight = tomorrow.Add(new TimeSpan(0, 0, 0));

            // 计算明天0点0分0秒距离当前时间的时间差
            TimeSpan timeDifference = tomorrowMidnight - now;

            // 将时间差转换为时间戳（以秒为单位）
            long timestamp = (long)timeDifference.TotalSeconds;

            return timestamp;
        }

        

        /// <summary>
        /// 获得下周一0点到现在的时间戳 秒
        /// </summary>
        /// <returns></returns>
        public static long GetToNextWeekTime()
        {
            DateTime now = DateTime.Now;

            // 获取下周一的日期
            DateTime nextMonday = now.AddDays((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7).Date;

            // 设置明天的时间为0点0分0秒
            DateTime nextMondayMidnight = nextMonday.Add(new TimeSpan(0, 0, 0));

            // 计算明天0点0分0秒距离当前时间的时间差
            TimeSpan timeDifference = nextMondayMidnight - now;

            // 将时间差转换为时间戳（以秒为单位）
            long timestamp = (long)timeDifference.TotalSeconds;

            return timestamp;
        }
    }
}