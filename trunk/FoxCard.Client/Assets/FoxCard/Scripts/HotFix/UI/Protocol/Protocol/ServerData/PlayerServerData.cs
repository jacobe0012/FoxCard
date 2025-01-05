using MessagePack;


namespace HotFix_UI
{
    [MessagePackObject]
    public class PlayerServerData
    {
        /// <summary>
        /// 签到时间戳 ms
        /// </summary>
        [Key(0)]
        public long LastSignTimeStamp { get; set; }

        /// <summary>
        /// 签到总次数
        /// </summary>
        [Key(1)]
        public int SignCount { get; set; }

        /// <summary>
        /// 上次登录时间戳 ms
        /// </summary>
        [Key(2)]
        public long LastLoginTimeStamp { get; set; }

        /// <summary>
        /// 累计登录总次数
        /// </summary>
        [Key(3)]
        public int LoginCount { get; set; }

        /// <summary>
        /// 连续登录次数
        /// </summary>
        [Key(4)]
        public int ContinuousLoginCount { get; set; }


        /// <summary>
        /// 7日签到上次签到时间戳 ms
        /// </summary>
        [Key(5)]
        public long Last7SignTimeStamp { get; set; }
    }
}