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
        /// 签到时间戳 ms
        /// </summary>
        [Key(1)]
        public long LastSignTimeStamp { get; set; }

        /// <summary>
        /// 签到总次数
        /// </summary>
        [Key(2)]
        public int SignCount { get; set; }

        /// <summary>
        /// 上次登录时间戳 ms
        /// </summary>
        [Key(3)]
        public long LastLoginTimeStamp { get; set; }

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

        /// <summary>
        /// 玩家邮件信息
        /// </summary>
        [Key(7)]
        public GameMail? GameMail { get; set; }

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