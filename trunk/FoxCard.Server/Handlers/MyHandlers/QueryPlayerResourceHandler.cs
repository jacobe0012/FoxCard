using System.Collections.Concurrent;
using System.Net.WebSockets;
using HotFix_UI;
using MessagePack;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FoxCard.Server.Handlers;

public class QueryPlayerResourceHandler : HandleBase, ICommandHandler
{
    public QueryPlayerResourceHandler(IConnectionMultiplexer redis,
        ConcurrentDictionary<WebSocket, string> connections) :
        base(redis, connections)
    {
    }

    public async Task<Context> HandleAsync(MyMessage message, WebSocket webSocket)
    {
        PlayerResource playerRes;
        if (!_connections.TryGetValue(webSocket, out var openId))
        {
            //Console.WriteLine($"webSocket:{webSocket.} not found");
            //用户未登记
            message.ErrorCode = 1001;
        }

        var db = _redis.GetDatabase();
        var rv = await db.StringGetAsync(GetRedisDBStr(1, openId));
        playerRes = JsonConvert.DeserializeObject<PlayerResource>(rv);
        playerRes.PlayerServerData = null;
        message.Content =
            MessagePackSerializer.Serialize(playerRes, options);

        var outputContentStr = rv.ToString();

        var context = new Context
        {
            message = message,
            inputContentStr = "",
            outputContentStr = outputContentStr
        };
        return context;
    }
}