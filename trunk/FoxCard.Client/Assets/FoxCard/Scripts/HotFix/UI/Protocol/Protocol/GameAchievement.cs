using MessagePack;
using System.Collections.Generic;

namespace HotFix_UI
{
    [MessagePackObject]
    public class GameAchievement : IMessagePack
    {
        /// <summary>
        /// 用户成就信息
        /// </summary>
        [Key(0)]
        public List<AchieveItem> AchieveItemList { get; set; }

        /// <summary>
        /// 用户成就点数
        /// </summary>
        [Key(1)]
        public int Score { get; set; }

        /// <summary>
        /// 用户成就宝箱列表
        /// </summary>
        [Key(2)]
        public List<int> AchieveRewardBoxList { get; set; }

        /// <summary>
        /// 如果同类型则设置其成就点数
        /// </summary>
        /// <param name="type"></param>
        public void SetAchievePara(int type, int deltaPara = 1)
        {
            foreach (var achieveItem in AchieveItemList)
            {
                if (achieveItem.Type == type)
                {
                    achieveItem.CurPara += deltaPara;
                }
            }
        }
    }


    [MessagePackObject]
    public class AchieveItem : IMessagePack
    {
        /// <summary>
        /// 成就组id
        /// </summary>
        [Key(0)]
        public int GroupId { get; set; }

        /// <summary>
        /// 当前参数
        /// </summary>
        [Key(1)]
        public int CurPara { get; set; }

        /// <summary>
        /// 成就组类型
        /// </summary>
        [Key(2)]
        public int Type { get; set; }
    }
}