using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    /// <summary>
    /// 时间类
    /// </summary>
    public class TimeInfo : Singleton<TimeInfo>, IDisposable
    {
        protected readonly DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        protected DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private int timeZone;

        /// <summary>
        /// 时区，按小时设置
        /// </summary>
        public int TimeZone
        {
            get
            {
                return this.timeZone;
            }
            set
            {
                this.timeZone = value;
                dt = dt1970.AddHours(value);
            }
        }

        public void Init()
        {
            TimeZone = 8;
        }

        /// <summary>
        /// 客户端的时间戳（毫秒）
        /// </summary>
        /// <returns></returns>
        public long ClientNow()
        {
            return (this.DateTimeNow().Ticks - this.dt.Ticks) / 10000;
        }

        /// <summary>
        /// 客户端的时间戳（秒）
        /// </summary>
        /// <returns></returns>
        public long ClientNowSeconds()
        {
            return this.ClientNow() / 1000;
        }

        /// <summary>
        /// 根据时间戳（毫秒）获取时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public DateTime ToDateTime(long timeStamp)
        {
            return this.dt.AddTicks(timeStamp * 10000);
        }

        /// <summary>
        /// 时间差（毫秒）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="utc"></param>
        /// <returns></returns>
        public long Transition(DateTime dateTime, bool utc)
        {
            var time = this.dt1970;
            if (!utc)
                time = this.dt;

            return (dateTime.Ticks - time.Ticks) / 10000;
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        /// <returns></returns>
        public virtual DateTime DateTimeNow()
        {
            return DateTime.Now;
        }

        public virtual void Dispose()
        {
            
        }
    }
}
