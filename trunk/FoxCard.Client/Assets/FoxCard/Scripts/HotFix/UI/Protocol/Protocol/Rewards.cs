using System.Collections.Generic;
using System.Numerics;
using HotFix_UI;
using MessagePack;

namespace HotFix_UI
{
    [MessagePackObject]
    public class Rewards : IMessagePack
    {
        [Key(0)] public List<Vector3> rewards { get; set; }
    }
}