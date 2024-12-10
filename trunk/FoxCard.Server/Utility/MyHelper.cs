using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;

namespace FoxCard.Server.Utility;

public class MyHelper
{
    public static byte[] SerializeMessagePack<T>(string value)
    {
        var options =
            MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block);

        return MessagePackSerializer.Serialize(JsonConvert.DeserializeObject<T>(value), options);
    }
}