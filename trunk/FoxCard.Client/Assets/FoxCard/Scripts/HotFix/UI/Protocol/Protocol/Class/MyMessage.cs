using MessagePack;

namespace HotFix_UI
{
    [MessagePackObject]
    public class MyMessage : IMessagePack
    {
        [Key(0)] public string MethodName { get; set; }

        [Key(1)] public byte[] Content { get; set; }

        [Key(2)] public int ErrorCode { get; set; }
    }
}