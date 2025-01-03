using MessagePack;
using System.Collections.Generic;

namespace HotFix_UI
{
    [MessagePackObject]
    public class GameSign : IMessagePack
    {
        /// <summary>
        /// 今日是否已经签到过
        /// </summary>
        [Key(0)]
        public bool isSignedToday { get; set; }
    }
}