using System.Collections.Generic;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;

namespace HotFix_UI
{
    [MessagePackObject]
    public class PlayerResource : IMessagePack
    {
        /// <summary>
        /// 用户item资产
        /// </summary>
        [Key(0)]
        public List<UnityEngine.Vector3> ItemList { get; set; }

        /// <summary>
        /// 玩家成就信息
        /// </summary>
        [Key(1)]
        public GameAchievement? GameAchieve { get; set; }

        /// <summary>
        /// 玩家邮件信息
        /// </summary>
        [Key(2)]
        public GameMail? GameMail { get; set; }

        /// <summary>
        /// 玩家邮件信息
        /// </summary>
        [Key(3)]
        public GameSign? GameSign { get; set; }

        /// <summary>
        /// 玩家邮件信息
        /// </summary>
        [Key(4)]
        public PlayerServerData? PlayerServerData { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
            //return base.ToString();
        }
    }

    // [MessagePackObject]
    // public class ItemInfo : IMessagePack
    // {
    //     /// <summary>
    //     /// 用户itemId
    //     /// </summary>
    //     [Key(0)]
    //     public int ItemId { get; set; }
    //
    //     /// <summary>
    //     /// 用户item数量
    //     /// </summary>
    //     [Key(1)]
    //     public int Count { get; set; }
    // }
}