using System.Collections.Generic;
using HotFix_UI;
using MessagePack;

namespace HotFix_UI
{
    [MessagePackObject]
    public class PlayerResource : IMessagePack
    {
        /// <summary>
        /// 用户item资产
        /// </summary>
        [Key(0)]
        public List<ItemInfo> ItemList { get; set; }

        /// <summary>
        /// 签到时间戳 ms
        /// </summary>
        [Key(1)]
        public long LastSignTime { get; set; }

        /// <summary>
        /// 签到总次数
        /// </summary>
        [Key(2)]
        public int SignCount { get; set; }

        /// <summary>
        /// 上次登录时间戳 ms
        /// </summary>
        [Key(3)]
        public long LastLoginTime { get; set; }

        /// <summary>
        /// 累计登录总次数
        /// </summary>
        [Key(4)]
        public int LoginCount { get; set; }

        /// <summary>
        /// 连续登录次数
        /// </summary>
        [Key(5)]
        public int ContinuousLoginCount { get; set; }

        /// <summary>
        /// 玩家成就信息
        /// </summary>
        [Key(6)]
        public GameAchievement? GameAchieve { get; set; }
    }

    [MessagePackObject]
    public class ItemInfo : IMessagePack
    {
        /// <summary>
        /// 用户itemId
        /// </summary>
        [Key(0)]
        public int ItemId { get; set; }

        /// <summary>
        /// 用户item数量
        /// </summary>
        [Key(1)]
        public int Count { get; set; }
    }
}