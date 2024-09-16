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