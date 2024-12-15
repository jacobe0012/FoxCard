using MessagePack;
using System.Collections.Generic;

namespace HotFix_UI
{
    [MessagePackObject]
    public class GameMail : IMessagePack
    {
        [Key(0)] public List<MailItem> MailItems { get; set; }
    }


    [MessagePackObject]
    public class MailItem : IMessagePack
    {
        /// <summary>
        /// 邮件id
        /// </summary>
        [Key(0)]
        public int Id { get; set; }

        /// <summary>
        /// 邮件模板id
        /// </summary>
        [Key(1)]
        public int TemplateId { get; set; }

        /// <summary>
        /// 邮件状态  0未读/未领 1已读/已领
        /// </summary>
        [Key(2)]
        public int State { get; set; }

        /// <summary>
        /// 邮件发送时间戳 /ms
        /// </summary>
        [Key(3)]
        public long SendTimeStamp { get; set; }
    }
}