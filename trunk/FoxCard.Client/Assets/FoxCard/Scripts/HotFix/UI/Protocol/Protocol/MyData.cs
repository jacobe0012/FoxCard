using HotFix_UI;
using MessagePack;

namespace HotFix_UI
{

    [MessagePackObject]
    public class MyData : IMessagePack
    {
        [Key(0)] public int Age { get; set; }

        [Key(1)] public string FirstName { get; set; }

        [Key(2)] public string LastName { get; set; }

        // All fields or properties that should not be serialized must be annotated with [IgnoreMember].
        [IgnoreMember]
        public string FullName
        {
            get { return FirstName + LastName; }
        }
    }
}