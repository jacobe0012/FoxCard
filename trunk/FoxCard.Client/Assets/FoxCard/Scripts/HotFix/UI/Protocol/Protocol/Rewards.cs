using MessagePack;
using System.Collections.Generic;

namespace HotFix_UI
{
    [MessagePackObject]
    public class Rewards : IMessagePack
    {
        [Key(0)] public List<UnityEngine.Vector3> rewards { get; set; }
    }
}