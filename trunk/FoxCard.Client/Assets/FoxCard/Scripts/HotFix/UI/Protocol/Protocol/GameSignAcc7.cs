using MessagePack;
using System.Collections.Generic;

namespace HotFix_UI
{
    [MessagePackObject]
    public class GameSignAcc7 : IMessagePack
    {
        /// <summary>
        /// 七天签到奖励组id
        /// </summary>
        [Key(0)]
        public int Signed7GroupId { get; set; }

        /// <summary>
        /// 七天签到到第几天了   0-7
        /// </summary>
        [Key(1)]
        public int SignedDay { get; set; }

        /// <summary>
        /// 今日是否已经签到过
        /// </summary>
        [Key(2)]
        public bool isSignedToday { get; set; }

        /// <summary>
        /// 服务器签到最大天数   1-7
        /// </summary>
        [Key(3)]
        public int MaxSignedDay { get; set; }
    }
}